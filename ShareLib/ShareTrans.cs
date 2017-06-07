using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using System.IO;

namespace ShareLib
{
    public class ShareTrans
    {
        public string Submit(string transNo, string fromCartonNo, string qty, string sn, string locationCode, string rdoSelectType, string mUser, IDomainDataProvider DataProvider)
        {
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;
            try
            {
                DataProvider.BeginTransaction();

                #region Check
                Storloctrans storloctrans = (Storloctrans)_WarehouseFacade.GetStorloctrans(transNo);


                if (storloctrans == null)
                {
                    DataProvider.RollbackTransaction();
                    return "无此转储单信息";
                }
                //原箱号：后台获取其库位和货位（TBLStorageDetail），查找条件：根据箱号到TBLStorageDetail中查找库存信息；
                //若库位和出库库位（TBLStorLocTrans. FromStorageCode）不符则报错。
                StorageDetail storageDetail = (StorageDetail)_InventoryFacade.GetStorageDetail(fromCartonNo);
                if (storageDetail == null)
                {
                    DataProvider.RollbackTransaction();
                    return "库存明细信息表里没有对应箱号的数据";
                }

                if (!storageDetail.StorageCode.Equals(storloctrans.FromstorageCode))
                {
                    DataProvider.RollbackTransaction();
                    return "库位和出库库位不符";

                }

                //一 根据箱号到TBLStorageDetail查找mcode，根据（转单号，mcode）查找TBLStorLocTransDetail 中数据，如果没有则报错：转储单中没有对应的SAP物料号
                StorloctransDetail storloctransDetail = _WarehouseFacade.GetStorloctransdetail(transNo, storageDetail.MCode)
                    as StorloctransDetail;
                if (storloctransDetail == null)
                {
                    DataProvider.RollbackTransaction();
                    return "转储单中没有对应的SAP物料号";

                }
                //二  判断（CartonNO对应的MCode），（TransNo）在TBLStorLocTransDetail中的状态是否为Close:完成，
                //如果是提示该料转储已经完成，如果是Release（初始化），更新状态为（Pick，拣料中） 。
                if (storloctransDetail.Status == "Close")
                {
                    DataProvider.RollbackTransaction();
                    return "该料转储已经完成";

                }

                decimal total = _WarehouseFacade.GetStorloctransDetailDqmCodeQtySum(transNo, storageDetail.DQMCode);
                decimal smaller = _WarehouseFacade.GetStorloctransDetailCartonDqmCodeQtySum(transNo, storageDetail.DQMCode);

                if (rdoSelectType == "整箱")
                    smaller += storageDetail.StorageQty;
                else
                    smaller += string.IsNullOrEmpty(sn) ? decimal.Parse(qty) : 1;


                if (smaller > total)
                {
                    DataProvider.RollbackTransaction();
                    return "此物料的转储数量已经超过转储单" + transNo + "规定的数量！";

                }

                if (storloctransDetail.Status == "Release")
                {
                    storloctransDetail.Status = "Pick";
                    storloctransDetail.MaintainUser = mUser;
                    storloctransDetail.MaintainDate = mDate;
                    storloctransDetail.MaintainTime = mTime;
                    _WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);
                }





                #endregion
                if (rdoSelectType == "整箱")
                {
                    #region 整箱
                    //1，	检查TBLStorageDetail.FreezeQTY是否为零？不为零，检查是否TBLStorageDetail.FreezeQTY= 
                    //TBLStorageDetail. STORAGEQTY?如果是：提示此箱在拣料中；如果不是提示此箱SN部分拣料中，请拆箱拣料。
                    #region TBLStorageDetail
                    if (storageDetail.FreezeQty != 0)
                    {
                        if (storageDetail.FreezeQty == storageDetail.StorageQty)
                        {
                            DataProvider.RollbackTransaction();
                            return "此箱在拣料中";

                        }
                        else
                        {
                            DataProvider.RollbackTransaction();
                            return "此箱SN部分拣料中，请拆箱拣料";

                        }
                    }

                    //2，	更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. STORAGEQTY，TBLStorageDetail. AvailableQTY=0。
                    storageDetail.FreezeQty = storageDetail.StorageQty;
                    storageDetail.AvailableQty = 0;
                    storageDetail.MaintainUser = mUser;
                    storageDetail.MaintainDate = mDate;
                    storageDetail.MaintainTime = mTime;
                    _InventoryFacade.UpdateStorageDetail(storageDetail);
                    #endregion

                    //3，	更新TBLStorageDetailSN. PICKBLOCK=Y。
                    //StorageDetail[] storageDetails = _WarehouseFacade.GetStorageDetailsFromCARTONNO(fromCartonNo);
                    Material mar = _WarehouseFacade.GetMaterialFromDQMCode(storageDetail.DQMCode);
                    if (mar == null)
                    {
                        DataProvider.RollbackTransaction();
                        return "原箱号物料信息不存在";

                    }
                    bool issn = false || mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS;

                    //单件管控
                    List<string> snList = new List<string>();
                    #region storageDetailSN
                    if (issn)
                    {
                        object[] objs = _WarehouseFacade.GetStorageDetailSnbyCartonNo(fromCartonNo);
                        if (objs == null)
                        {
                            DataProvider.RollbackTransaction();
                            return "库存明细SN信息表里没有对应箱号的数据";

                        }

                        foreach (StorageDetailSN storageDetailSN in objs)
                        {
                            snList.Add(storageDetailSN.SN);//记sn号
                            storageDetailSN.PickBlock = "Y";
                            storageDetailSN.MaintainUser = mUser;
                            storageDetailSN.MaintainDate = mDate;
                            storageDetailSN.MaintainTime = mTime;
                            _InventoryFacade.UpdateStorageDetailSN(storageDetailSN);
                        }
                    }
                    #endregion

                    #region StorloctransDetailCarton LocationCode为空


                    StorloctransDetailCarton storCarton = _WarehouseFacade.GetStorLocTransDetailCarton(transNo, fromCartonNo);
                    if (storCarton == null)
                    {
                        StorloctransDetailCarton storloctransDetailCarton = new StorloctransDetailCarton();
                        storloctransDetailCarton.Transno = transNo;
                        storloctransDetailCarton.MCode = storloctransDetail.MCode;
                        storloctransDetailCarton.DqmCode = storageDetail.DQMCode;
                        storloctransDetailCarton.MDesc = storageDetail.MDesc;
                        storloctransDetailCarton.Unit = storageDetail.Unit;
                        storloctransDetailCarton.Supplier_lotno = storageDetail.SupplierLotNo;
                        storloctransDetailCarton.Production_Date = storageDetail.ProductionDate;
                        storloctransDetailCarton.StorageageDate = storageDetail.StorageAgeDate;
                        storloctransDetailCarton.LaststorageageDate = storageDetail.LastStorageAgeDate;
                        storloctransDetailCarton.ValidStartDate = storageDetail.ValidStartDate;
                        storloctransDetailCarton.FacCode = storageDetail.FacCode;
                        storloctransDetailCarton.Qty = storageDetail.StorageQty;
                        storloctransDetailCarton.LocationCode = locationCode;
                        storloctransDetailCarton.Cartonno = " ";
                        storloctransDetailCarton.FromlocationCode = storageDetail.LocationCode;
                        storloctransDetailCarton.Fromcartonno = storageDetail.CartonNo;
                        storloctransDetailCarton.Lotno = storageDetail.Lotno;
                        storloctransDetailCarton.CUser = mUser;
                        storloctransDetailCarton.CDate = mDate;
                        storloctransDetailCarton.CTime = mTime;
                        storloctransDetailCarton.MaintainUser = mUser;
                        storloctransDetailCarton.MaintainDate = mDate;
                        storloctransDetailCarton.MaintainTime = mTime;
                        _WarehouseFacade.AddStorloctransdetailcarton(storloctransDetailCarton);
                    }

                    #endregion

                    #region 6.如果原箱号在TBLStorageDetailSN有SN信息，将SN信息插入到TBLStorLocTransDetailSN表
                    if (issn)
                    {
                        if (snList.Count > 0)
                        {
                            StorloctransDetailSN storloctransDetailSN = new StorloctransDetailSN();
                            foreach (string storageDetailSn in snList)
                            {
                                storloctransDetailSN.Transno = transNo;
                                storloctransDetailSN.Cartonno = " ";
                                storloctransDetailSN.Fromcartonno = fromCartonNo;
                                storloctransDetailSN.Sn = storageDetailSn;// storageDetailSN.SN;
                                storloctransDetailSN.MaintainUser = mUser;
                                storloctransDetailSN.MaintainDate = mDate;
                                storloctransDetailSN.MaintainTime = mTime;
                                _WarehouseFacade.AddStorloctransdetailsn(storloctransDetailSN);

                            }
                        }
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    #region 拆箱
                    //1，	无论输入是【原箱号】，【SN】，首先判断管控类型，如果是单件管控，已输入SN为条件进行录入；
                    //如果是批管控或不管控则以【原箱号】和【数量】为录入条件。
                    StorageDetail[] storageDetails = _WarehouseFacade.GetStorageDetailsFromCARTONNO(fromCartonNo);
                    if (storageDetails == null)
                    {
                        DataProvider.RollbackTransaction();
                        return "原箱号无库存信息";

                    }

                    Material mar = _WarehouseFacade.GetMaterialFromDQMCode(storageDetails[0].DQMCode);
                    if (mar == null)
                    {
                        DataProvider.RollbackTransaction();
                        return "原箱号物料信息不存在";

                    }
                    bool issn = false || mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS;

                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS) //单件管控
                    {
                        #region 单件管控

                        if (string.IsNullOrEmpty(sn))
                        {
                            DataProvider.RollbackTransaction();
                            return "必须输入SN号码";
                        }
                        if (!string.IsNullOrEmpty(qty))
                        {
                            DataProvider.RollbackTransaction();
                            return "单件管控料不需要输入数量";
                        }
                        //A 根据SN在TBLStorageDetail查看是否TBLStorageDetail.FreezeQTY= TBLStorageDetail. STORAGEQTY？如果等于，提示：此箱在拣料中；如果不等于0，检查TBLStorageDetailSN. PICKBLOCK是否是Y，如果是提示：此SN正在拣料中。
                        StorageDetailSN storageDetailSN = _InventoryFacade.GetStorageDetailSN(sn) as StorageDetailSN;
                        if (storageDetailSN == null)
                        {
                            DataProvider.RollbackTransaction();
                            return "刷入SN条码不存在";

                        }
                        StorageDetail _storageDetail = _InventoryFacade.GetStorageDetail(storageDetailSN.CartonNo) as StorageDetail;
                        if (_storageDetail == null)
                        {
                            DataProvider.RollbackTransaction();
                            return "输入的SN找不到库存信息";

                        }
                        if (_storageDetail.FreezeQty == _storageDetail.StorageQty)
                        {
                            DataProvider.RollbackTransaction();
                            return "此箱在拣料中";

                        }
                        else
                        {
                            if (storageDetailSN.PickBlock == "Y")
                            {
                                DataProvider.RollbackTransaction();
                                return "此SN正在拣料中";

                            }
                        }

                        //B 根据SN在TBLStorageDetail中查找mcode信息，再根据（mcode+TransNo）信息在TBLStorLocTransDetail中是否有数据，如果没有则报错：转储单中没有对应的SAP物料号。
                        StorloctransDetail _storloctransDetail = _WarehouseFacade.GetStorloctransdetail(transNo,
                                                                                                   _storageDetail.MCode) as StorloctransDetail;
                        if (_storloctransDetail == null)
                        {
                            DataProvider.RollbackTransaction();
                            return "转储单中没有对应的SAP物料号";

                        }
                        //C 更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. FreezeQTY+1，TBLStorageDetail. AvailableQTY= TBLStorageDetail.STORAGEQTY-TBLStorageDetail. FreezeQTY。
                        _storageDetail.FreezeQty += 1;
                        _storageDetail.AvailableQty = _storageDetail.StorageQty - _storageDetail.FreezeQty;
                        _storageDetail.MaintainUser = mUser;
                        _storageDetail.MaintainDate = mDate;
                        _storageDetail.MaintainTime = mTime;
                        _InventoryFacade.UpdateStorageDetail(_storageDetail);

                        //D 更新TBLStorageDetailSN. PICKBLOCK=Y。
                        storageDetailSN.PickBlock = "Y";
                        storageDetailSN.MaintainUser = mUser;
                        storageDetailSN.MaintainDate = mDate;
                        storageDetailSN.MaintainTime = mTime;
                        _InventoryFacade.UpdateStorageDetailSN(storageDetailSN);


                        //E  需要输入目标箱号，根据SN信息在TBLStorageDetailSN中查找原箱号信息；
                        //根据（目标箱号+TransNo+FromCARTONNO原箱号）在TBLStorLocTransDetailCarton是否有记录。
                        #region objStorLocTransDetailCarton
                        StorloctransDetailCarton storloctransDetailCarton = _WarehouseFacade.GetStorLocTransDetailCarton(transNo, storageDetailSN.CartonNo);

                        if (storloctransDetailCarton == null)
                        {
                            storloctransDetailCarton = new StorloctransDetailCarton();
                            storloctransDetailCarton.Transno = transNo;
                            storloctransDetailCarton.MCode = _storloctransDetail.MCode;
                            storloctransDetailCarton.DqmCode = _storageDetail.DQMCode;
                            storloctransDetailCarton.MDesc = _storageDetail.MDesc;
                            storloctransDetailCarton.Unit = _storageDetail.Unit;
                            storloctransDetailCarton.Supplier_lotno = _storageDetail.SupplierLotNo;
                            storloctransDetailCarton.Production_Date = _storageDetail.ProductionDate;
                            storloctransDetailCarton.StorageageDate = _storageDetail.StorageAgeDate;
                            storloctransDetailCarton.LaststorageageDate = _storageDetail.LastStorageAgeDate;
                            storloctransDetailCarton.ValidStartDate = _storageDetail.ValidStartDate;
                            storloctransDetailCarton.FacCode = _storageDetail.FacCode;
                            storloctransDetailCarton.Qty = 1;
                            storloctransDetailCarton.LocationCode = locationCode;
                            storloctransDetailCarton.Cartonno = " ";
                            storloctransDetailCarton.FromlocationCode = _storageDetail.LocationCode;
                            storloctransDetailCarton.Fromcartonno = _storageDetail.CartonNo;
                            storloctransDetailCarton.Lotno = _storageDetail.Lotno;
                            storloctransDetailCarton.CUser = mUser;
                            storloctransDetailCarton.CDate = mDate;
                            storloctransDetailCarton.CTime = mTime;
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            _WarehouseFacade.AddStorloctransdetailcarton(storloctransDetailCarton);
                        }
                        else
                        {
                            //如果有记录，检查记录中的料号是否有与原箱的料号一致，如果不一致则提示：（目标箱号物料不一致）
                            object objStorageDetail = _InventoryFacade.GetStorageDetail(fromCartonNo);
                            if (!storloctransDetailCarton.MCode.Equals((objStorageDetail as StorageDetail).MCode))
                            {
                                DataProvider.RollbackTransaction();
                                return "目标箱号物料不一致";

                            }

                            //如果有： 更新TBLStorLocTransDetailCarton.QTY+1，MDate，MTime，MUser。
                            storloctransDetailCarton.Qty += 1;
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                        }
                        #endregion

                        //F  向表TBLStorLocTransDetailSN插入一条数据。
                        StorloctransDetailSN storloctransDetailSN = new StorloctransDetailSN();
                        storloctransDetailSN.Transno = transNo;
                        storloctransDetailSN.Cartonno = " ";
                        storloctransDetailSN.Fromcartonno = fromCartonNo;
                        storloctransDetailSN.Sn = storageDetailSN.SN;
                        storloctransDetailSN.MaintainUser = mUser;
                        storloctransDetailSN.MaintainDate = mDate;
                        storloctransDetailSN.MaintainTime = mTime;
                        _WarehouseFacade.AddStorloctransdetailsn(storloctransDetailSN);

                        #endregion
                    }
                    else
                    {
                        #region 非单件管控
                        if (string.IsNullOrEmpty(qty))
                        {
                            DataProvider.RollbackTransaction();
                            return "必须输入数量";
                        }
                        #region 判断数量是否是数字格式
                        try
                        {
                            decimal _qty = decimal.Parse(qty);
                            if (_qty <= 0)
                            {
                                DataProvider.RollbackTransaction();
                                return "数量必须为大于零的数字";

                            }
                        }
                        catch (Exception ex)
                        {
                            DataProvider.RollbackTransaction();
                            return "数量必须为大于零的数字";

                        }
                        #endregion

                        //B 检查TBLStorageDetail. AvailableQTY>PDA页面填的数量。如果否，提示：输入的数量大于库存可用数量。
                        StorageDetail _storageDetail = storageDetails[0] as StorageDetail;
                        if (_storageDetail.AvailableQty < decimal.Parse(qty))
                        {
                            DataProvider.RollbackTransaction();
                            return "输入的数量大于库存可用数量";

                        }

                        //C 更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. FreezeQTY+ PDA页面填的数量，
                        //TBLStorageDetail. AvailableQTY= TBLStorageDetail.STORAGEQTY-TBLStorageDetail. FreezeQTY。
                        _storageDetail.FreezeQty += int.Parse(qty);
                        _storageDetail.AvailableQty = _storageDetail.StorageQty - _storageDetail.FreezeQty;
                        _storageDetail.MaintainUser = mUser;
                        _storageDetail.MaintainDate = mDate;
                        _storageDetail.MaintainTime = mTime;
                        _InventoryFacade.UpdateStorageDetail(_storageDetail);


                        #region D需要输入目标箱号，根据（目标箱号+TransNo+FromCARTONNO原箱号）在TBLStorLocTransDetailCarton是否有记录。
                        StorloctransDetailCarton objStorLocTransDetailCarton = _WarehouseFacade.GetStorLocTransDetailCarton(transNo, _storageDetail.CartonNo);
                        if (objStorLocTransDetailCarton == null)
                        {
                            //    //如果没有：插入一条数据
                            //    if (!_WarehouseFacade.IsExistLocationCode(locationCode))
                            //    {
                            //        DataProvider.RollbackTransaction();
                            //       return"输入的目标货位不存在" ;
                            //    }
                            StorloctransDetailCarton storloctransDetailCarton = new StorloctransDetailCarton();
                            storloctransDetailCarton.Transno = transNo;
                            storloctransDetailCarton.MCode = storloctransDetail.MCode;
                            storloctransDetailCarton.DqmCode = _storageDetail.DQMCode;
                            storloctransDetailCarton.MDesc = _storageDetail.MDesc;
                            storloctransDetailCarton.Unit = _storageDetail.Unit;
                            storloctransDetailCarton.Supplier_lotno = _storageDetail.SupplierLotNo;
                            storloctransDetailCarton.Production_Date = _storageDetail.ProductionDate;
                            storloctransDetailCarton.StorageageDate = _storageDetail.StorageAgeDate;
                            storloctransDetailCarton.LaststorageageDate = _storageDetail.LastStorageAgeDate;
                            storloctransDetailCarton.ValidStartDate = _storageDetail.ValidStartDate;
                            storloctransDetailCarton.FacCode = _storageDetail.FacCode;
                            storloctransDetailCarton.Qty = decimal.Parse(qty);
                            storloctransDetailCarton.LocationCode = locationCode;
                            storloctransDetailCarton.Cartonno = " ";
                            storloctransDetailCarton.FromlocationCode = _storageDetail.LocationCode;
                            storloctransDetailCarton.Fromcartonno = _storageDetail.CartonNo;
                            storloctransDetailCarton.Lotno = _storageDetail.Lotno;
                            storloctransDetailCarton.CUser = mUser;
                            storloctransDetailCarton.CDate = mDate;
                            storloctransDetailCarton.CTime = mTime;
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            _WarehouseFacade.AddStorloctransdetailcarton(storloctransDetailCarton);
                        }
                        else
                        {
                            //如果有记录，检查记录中的料号是否有与原箱的料号一致，如果不一致则提示：（目标箱号物料不一致）
                            StorloctransDetailCarton storloctransDetailCarton = objStorLocTransDetailCarton as StorloctransDetailCarton;
                            object _objStorageDetail = _InventoryFacade.GetStorageDetail(fromCartonNo);
                            if (!storloctransDetailCarton.MCode.Equals((_objStorageDetail as StorageDetail).MCode))
                            {
                                DataProvider.RollbackTransaction();
                                return "目标箱号物料不一致";
                            }

                            //如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                            storloctransDetailCarton.Qty += decimal.Parse(qty);
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                        }
                        #endregion

                        #endregion
                    }

                    #endregion
                }

                ////三 判断条件TransNo,MCODE 下sum（TBLStorLocTransDetailCarton.QTY）是否等于需求数量TBLStorLocTransDetail.QTY。
                /////如果等于则更新装填TBLStorLocTransDetail.Status=Close:完成状态。
                //decimal sum = 0;
                //object[] _objs = _WarehouseFacade.GetStorloctransdetailcarton(transNo, storageDetail.MCode);
                //foreach (StorloctransDetailCarton storloctransDetailCarton in _objs)
                //{
                //    sum += storloctransDetailCarton.Qty;
                //}
                //if (sum == storloctransDetail.Qty)
                //{
                //    storloctransDetail.Status = "Close";
                //    storloctransDetail.MaintainUser = mUser;
                //    storloctransDetail.MaintainDate = mDate;
                //    storloctransDetail.MaintainTime = mTime;
                //    _WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);
                //}
                //else if (sum > storloctransDetail.Qty)
                //{
                //    DataProvider.RollbackTransaction();
                //    return "拣料数量超出需求数量";
                //}

                DataProvider.CommitTransaction();
                return "提交成功";

            }
            catch (Exception ex)
            {
                DataProvider.RollbackTransaction();
                return "提交失败：" + ex.Message;
            }

        }

    }
}
