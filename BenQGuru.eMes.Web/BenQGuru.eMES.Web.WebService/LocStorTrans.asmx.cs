using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// LocStorTrans 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class LocStorTrans : System.Web.Services.WebService
    {
        //WarehouseFacade facade = null;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        public LocStorTrans()
        {
            InitDbItems();
        }
        private WarehouseFacade _WarehouseFacade = null;
        private IDomainDataProvider _domainDataProvider;
        private string m_DbName;
        private BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = null;

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(m_DbName);
                }
                return _domainDataProvider;
            }
        }
        private void InitDbItems()
        {
            foreach (var item in BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings)
            {
                if (item.Default)
                    m_DbName = item.Name;

            }
        }


        #region 创建

        [WebMethod(EnableSession = true)]
        public string CreateAutoDocmentsNo()
        {
            //DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string stno = dbTime.DBDate.ToString().Substring(2, 6);
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
            string prefix = "M" + stno;
            string maxserial = _WarehouseFacade.GetMaxSerial(prefix);//通过前缀获取最大流水号

            //如果已是最大值就返回为空
            if (maxserial == "999")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();

            if (maxserial == "")
            {
                serialbook.SNPrefix = prefix; /// 序列号前缀 物料代码
                serialbook.MaxSerial = "001";     /// 序列号最大Serial号码

                //序列号(3位)
                return prefix + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = prefix;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                //序列号(3位)
                return prefix + string.Format("{0:000}", int.Parse(serialbook.MaxSerial));
            }
        }


        [WebMethod(EnableSession = true)]
        private void SaveDocmentsNo(string newKzCode, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            _WarehouseFacade = new WarehouseFacade(DataProvider);

            SERIALBOOK serialbook = new SERIALBOOK();
            serialbook.SNPrefix = newKzCode.Substring(0, 7);
            serialbook.MaxSerial = newKzCode.Substring(7, 3);
            serialbook.MDate = dbDateTime.DBDate;//当前日期 
            serialbook.MTime = dbDateTime.DBTime;//当前时间
            serialbook.MUser = userCode;//维护人=登录用户

            string oldserial = _WarehouseFacade.GetMaxSerial(serialbook.SNPrefix);//通过前缀获取流水号
            if (oldserial == "")
            {
                _WarehouseFacade.AddSerialBook(serialbook);//新增到serialbook表
            }
            else
            {
                _WarehouseFacade.UpdateSerialBook(serialbook);//更新到serialbook表
            }
        }
        #endregion

        #region 新增

        [WebMethod(EnableSession = true)]
        public string Add(string userCode, string newIqcNo, string fromCarton)
        {
            #region check
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            InventoryFacade facade = new InventoryFacade(this.DataProvider);
            ItemFacade itemFacade = new ItemFacade(DataProvider);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string msg = "";

            StorageDetail storageCarton = (StorageDetail)facade.GetStorageDetail(fromCarton);
            if (storageCarton == null)
            {  //A 根据原箱号和数量操作。根据原箱号（cartonno）到TBLStorageDetail中查找数据。没有报错。
                msg = "转储单中没有对应的SAP物料号";
                return msg;
            }
            string dqmCode = storageCarton.DQMCode;
            if (string.IsNullOrEmpty(dqmCode))
            {
                msg = "鼎桥物料编码不能为空";
                return msg;
            }
            BenQGuru.eMES.Domain.MOModel.Material material = (BenQGuru.eMES.Domain.MOModel.Material)itemFacade.GetMaterialByDQMCode(dqmCode);
            if (material == null)
            {
                msg = "鼎桥物料编码不存在";
                return msg;
            }
            if (string.IsNullOrEmpty(newIqcNo))
            {
                msg = "移转单号不能为空";
                return msg;
            }

            #endregion
            #region try
            try
            {
                this.DataProvider.BeginTransaction();
                Storloctrans oldStorloctrans = (Storloctrans)_WarehouseFacade.GetStorloctrans(newIqcNo);
                if (oldStorloctrans == null)
                {
                    #region 货位移动单信息TBLStorLocTrans表增加一笔数据
                    Storloctrans storloctrans = new Storloctrans();
                    storloctrans.Transno = newIqcNo;
                    storloctrans.TransType = TransType.TransType_Move;//类型(Transfer:转储；Move:货位移动)
                    storloctrans.Status = Pick_STATUS.Status_Release;
                    storloctrans.Invno = " ";
                    storloctrans.StorageCode = "";
                    storloctrans.CDate = dbTime.DBDate;
                    storloctrans.CTime = dbTime.DBTime;
                    storloctrans.CUser = userCode;// this.GetUserCode();
                    storloctrans.MaintainDate = dbTime.DBDate;
                    storloctrans.MaintainTime = dbTime.DBTime;
                    storloctrans.MaintainUser = userCode;// this.GetUserCode();
                    _WarehouseFacade.AddStorloctrans(storloctrans);
                    #endregion
                }
                StorloctransDetail oldstorloctransDetail = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(newIqcNo, material.MCode);
                if (oldstorloctransDetail == null)
                {
                    //检查移转单下表TBLStorLocTransDetail是否存在，如果存在提示已经包含物料信息。
                    //    this.DataProvider.RollbackTransaction();
                    //   msg= "移转单号已经包含物料信息" ;
                    //   return msg;
                    //}
                    //else
                    //{
                    #region 货位移动单信息StorloctransDetail表增加一笔数据
                    StorloctransDetail storloctransDetail = new StorloctransDetail();
                    storloctransDetail.Transno = newIqcNo;
                    storloctransDetail.Status = Pick_STATUS.Status_Release;
                    storloctransDetail.DqmCode = dqmCode;
                    storloctransDetail.MCode = material.MCode;
                    storloctransDetail.MDesc = material.MenlongDesc;
                    storloctransDetail.CustmCode = "";//  
                    storloctransDetail.Unit = "";// 
                    storloctransDetail.Qty = 0;
                    storloctransDetail.CDate = dbTime.DBDate;
                    storloctransDetail.CTime = dbTime.DBTime;
                    storloctransDetail.CUser = userCode;// this.GetUserCode();
                    storloctransDetail.MaintainDate = dbTime.DBDate;
                    storloctransDetail.MaintainTime = dbTime.DBTime;
                    storloctransDetail.MaintainUser = userCode;// this.GetUserCode();
                    _WarehouseFacade.AddStorloctransdetail(storloctransDetail);
                    #endregion
                }




                //货位移动单据号
                //string date = dbTime.DBDate.ToString().Substring(2, 6);
                //string documentno = CreateAutoDocmentsNo();
                //SaveDocmentsNo(documentno, userCode);


                this.DataProvider.CommitTransaction();
                msg = "新增成功";
                return msg;

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                msg = ex.Message;
                return msg;
                //WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
            #endregion
        }

        #endregion


        #region GetDqmCode

        [WebMethod(EnableSession = true)]
        public string GetDqmCode(string fromCarton)
        {
            #region facade
            InventoryFacade facade = new InventoryFacade(this.DataProvider);
            #endregion
            StorageDetail storageCarton = (StorageDetail)facade.GetStorageDetail(fromCarton);
            if (storageCarton == null)
            {
                return "";
            }
            string dqmCode = storageCarton.DQMCode;
            return dqmCode;

        }

        #endregion

        #region 提交

        [WebMethod(EnableSession = true)]
        public string Commit_Click(string userCode, string statusList, string transNo, string tLocationCode, string fromCarton, string tcarton, string inputsn, string inputqty)
        {
            string msg;
            #region facade

            InventoryFacade facade = new InventoryFacade(this.DataProvider);
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(this.DataProvider);
            }
            ItemFacade itemFacade = new ItemFacade();
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            #endregion

            #region check
            if (string.IsNullOrEmpty(transNo))
            {
                msg = "移转单号不能为空";
                return msg;
            }
            StorageDetail storageCarton = (StorageDetail)facade.GetStorageDetail(fromCarton);
            if (storageCarton == null)
            {  //A 根据原箱号和数量操作。根据原箱号（cartonno）到TBLStorageDetail中查找数据。没有报错。
                msg = "转储单中没有对应的原箱号";
                return msg;
            }

            if (string.IsNullOrEmpty(tLocationCode))
            {
                msg = "目标货位不能为空";
                return msg;
            }
            if (string.IsNullOrEmpty(tcarton))
            {
                msg = "目标箱号不能为空";
                return msg;
            }

            Storloctrans storloctrans = (Storloctrans)_WarehouseFacade.GetStorloctrans(transNo);
            if (storloctrans == null)
            {
                msg = "移转单号不存在";
                return msg;
            }
            #endregion
            string msgsn = "";


            StorageDetail fromStorageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(fromCarton);
            StorageDetail toStorageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(tcarton);
            if (fromStorageDetail == null)
                return "原箱号在库存中不存在！";
            StorloctransDetail storloctransdetailObj = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transNo, fromStorageDetail.MCode);
            if (storloctransdetailObj == null)
                return "转储单中没有对应的物料号";

            Location tLocation = (Location)facade.GetLocation(tLocationCode, 1);
            if (fromStorageDetail.StorageCode != tLocation.StorageCode)
                return "原货位的库位必须与目标货位的库位一致！";
            if (toStorageDetail != null && toStorageDetail.StorageCode != fromStorageDetail.StorageCode)
                return "目标箱号的库位必须原箱号的库位一样！";

            if (tcarton != fromCarton && toStorageDetail != null && toStorageDetail.LocationCode != tLocation.LocationCode)
                return "目标箱号的货位必须与填写的目标货位一致！";

            if (toStorageDetail != null && toStorageDetail.DQMCode != fromStorageDetail.DQMCode)
                return "目标箱号的物料必须与原箱号的物料一致！";
            if ((statusList == CartonType.CartonType_SplitCarton) && (tcarton == fromCarton))
                return "拆箱必须原箱号和目标箱号不同！";
            try
            {
                this.DataProvider.BeginTransaction();


                InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                trans.CartonNO = tcarton;
                trans.DqMCode = fromStorageDetail.DQMCode;
                trans.FacCode = fromStorageDetail.FacCode;
                trans.FromFacCode = fromStorageDetail.FacCode;
                trans.FromStorageCode = fromStorageDetail.StorageCode;
                trans.InvNO = " ";
                trans.InvType = " ";
                trans.LotNo = " ";
                trans.MaintainDate = dbDateTime.DBDate;
                trans.MaintainTime = dbDateTime.DBTime;
                trans.MaintainUser = userCode;
                trans.MCode = fromStorageDetail.MCode;
                trans.ProductionDate = fromStorageDetail.ProductionDate;
                trans.Qty = fromStorageDetail.StorageQty;
                trans.Serial = 0;
                trans.StorageAgeDate = fromStorageDetail.StorageAgeDate;
                trans.StorageCode = fromStorageDetail.StorageCode;
                trans.SupplierLotNo = fromStorageDetail.SupplierLotNo;
                trans.TransNO = transNo;
                trans.TransType = "IN";
                trans.ProcessType = "LocationTrans";
                trans.Unit = fromStorageDetail.Unit;
                _WarehouseFacade.AddInvInOutTrans(trans);


                #region【整箱】：
                if (statusList == CartonType.CartonType_AllCarton)
                {
                    msgsn = "箱号:" + fromCarton;

                    if (fromStorageDetail.FreezeQty > 0)
                        return "此箱号以被占用不能移动";

                    if (_WarehouseFacade.GetStorageDetailSNPickBlockCount(fromCarton) > 0)
                        return "此箱号SN已被被占用不能移动";
                    if (_WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton,
                                                                          tcarton) != null)
                    {
                        return transNo + "此单下已存在" + fromCarton + "移动到" + tcarton + "，请另行创建单据！";
                    }


                    facade.DeleteStorageDetail(fromStorageDetail);
                    toStorageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(tcarton);
                    if (toStorageDetail == null)
                    {

                        toStorageDetail = new StorageDetail();
                        toStorageDetail.AvailableQty = 0;
                        toStorageDetail.CartonNo = tcarton;
                        toStorageDetail.CDate = FormatHelper.TODateInt(DateTime.Now);
                        toStorageDetail.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                        toStorageDetail.CUser = userCode;
                        toStorageDetail.DQMCode = fromStorageDetail.DQMCode;
                        toStorageDetail.FacCode = fromStorageDetail.FacCode;
                        toStorageDetail.FreezeQty = 0;
                        toStorageDetail.LastStorageAgeDate = fromStorageDetail.LastStorageAgeDate;
                        toStorageDetail.LocationCode = tLocationCode;
                        toStorageDetail.Lotno = fromStorageDetail.Lotno;
                        toStorageDetail.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                        toStorageDetail.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                        toStorageDetail.MaintainUser = userCode;
                        toStorageDetail.MCode = fromStorageDetail.MCode;
                        toStorageDetail.MDesc = fromStorageDetail.MDesc;
                        toStorageDetail.ProductionDate = fromStorageDetail.ProductionDate;
                        toStorageDetail.ReworkApplyUser = fromStorageDetail.ReworkApplyUser;
                        toStorageDetail.StorageAgeDate = fromStorageDetail.StorageAgeDate;
                        toStorageDetail.StorageCode = fromStorageDetail.StorageCode;
                        toStorageDetail.StorageQty = 0;
                        toStorageDetail.SupplierLotNo = fromStorageDetail.SupplierLotNo;
                        toStorageDetail.Unit = fromStorageDetail.Unit;
                        toStorageDetail.ValidStartDate = fromStorageDetail.ValidStartDate;
                        facade.AddStorageDetail(toStorageDetail);

                    }

                    toStorageDetail.StorageQty += fromStorageDetail.StorageQty;
                    toStorageDetail.AvailableQty += fromStorageDetail.AvailableQty;



                    facade.UpdateStorageDetail(toStorageDetail);

                    StorloctransDetail storloctransDetail = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transNo, fromStorageDetail.MCode);
                    storloctransDetail.Qty += fromStorageDetail.AvailableQty;
                    _WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);

                    #region 4，	向TBLStorLocTransDetailCarton插入一笔数据。
                    StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                    newstorlocDetailCarton.Transno = transNo;   //storlocDetailCarton.Transno = StorLocTransDetail.TransNo;
                    newstorlocDetailCarton.MCode = fromStorageDetail.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                    newstorlocDetailCarton.DqmCode = fromStorageDetail.DQMCode;
                    newstorlocDetailCarton.FacCode = "10Y2";
                    newstorlocDetailCarton.Qty = fromStorageDetail.StorageQty;//QTY: TBLStorageDetail. storageQTY
                    newstorlocDetailCarton.LocationCode = tLocationCode;//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                    newstorlocDetailCarton.Cartonno = tcarton;//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                    newstorlocDetailCarton.FromlocationCode = fromStorageDetail.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                    newstorlocDetailCarton.Fromcartonno = fromStorageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                    newstorlocDetailCarton.Lotno = fromStorageDetail.Lotno; //LotNo：TBLStorageDetail. LotNo
                    newstorlocDetailCarton.CUser = userCode;	//	CUSER
                    newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                    newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                    newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                    newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                    newstorlocDetailCarton.MaintainUser = userCode;
                    _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                    #endregion
                    //5，	如果原箱号在TBLStorageDetailSN有SN信息，将SN信息插入到TBLStorLocTransDetailSN表
                    object[] storageDetailSnlist = facade.GetStorageDetailSnbyCarton(fromCarton);
                    if (storageDetailSnlist != null)
                    {
                        foreach (StorageDetailSN newStorageDetailSn in storageDetailSnlist)
                        {
                            #region add TBLStorageDetailSN
                            StorloctransDetailSN newStorloctransDetailSn = new StorloctransDetailSN();
                            newStorloctransDetailSn.Sn = newStorageDetailSn.SN;//SN：TBLStorageDetailSN.SN
                            newStorloctransDetailSn.Transno = transNo;    // TBLStorLocTransDetail. TransNo
                            newStorloctransDetailSn.Fromcartonno = fromCarton;//FromCARTONNO：TBLStorageDetail.LocationCode
                            newStorloctransDetailSn.Cartonno = tcarton; //FormatHelper.CleanString(txtTLocationCodeEdit.Text);//CARTONNO：TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                            newStorloctransDetailSn.MaintainDate = dbDateTime.DBDate;	//	MDATE
                            newStorloctransDetailSn.MaintainTime = dbDateTime.DBTime;	//	MTIME
                            newStorloctransDetailSn.MaintainUser = userCode;
                            _WarehouseFacade.AddStorloctransdetailsn(newStorloctransDetailSn);

                            newStorageDetailSn.CartonNo = tcarton;
                            newStorageDetailSn.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            newStorageDetailSn.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            newStorageDetailSn.MaintainUser = userCode;
                            facade.UpdateStorageDetailSN(newStorageDetailSn);
                            #endregion
                        }
                    }


                }
                #endregion

                #region【拆箱】：
                else if (statusList == CartonType.CartonType_SplitCarton)
                {

                    Domain.MOModel.Material material = (Domain.MOModel.Material)itemFacade.GetMaterial(storageCarton.MCode);
                    if (material != null)
                    {
                        //2单件管控IsInt
                        if (material.MCONTROLTYPE == BOMItemControlType.ITEM_CONTROL_KEYPARTS)
                        {
                            #region	单件管控：

                            msgsn = "SN:" + inputsn;
                            if (string.IsNullOrEmpty(inputsn))
                            {

                                msg = "此箱为单件管控，请输入SN。";
                                return msg;
                            }


                            StorageDetailSN storageDetailSn = (StorageDetailSN)facade.GetStorageDetailSN(inputsn);

                            if (storageDetailSn == null)
                                return "此SN不存在！";
                            if (storageDetailSn.CartonNo != fromCarton)
                                return "原箱中不存在此SN！";




                            decimal storageQTY = fromStorageDetail.StorageQty - fromStorageDetail.FreezeQty;
                            if (storageQTY <= 0)
                                return "原箱号的库存数量不足:" + storageQTY;


                          
                            if (toStorageDetail == null)
                            {

                                toStorageDetail = new StorageDetail();
                                toStorageDetail.AvailableQty = 0;
                                toStorageDetail.CartonNo = tcarton;
                                toStorageDetail.CDate = FormatHelper.TODateInt(DateTime.Now);
                                toStorageDetail.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                                toStorageDetail.CUser = userCode;
                                toStorageDetail.DQMCode = fromStorageDetail.DQMCode;
                                toStorageDetail.FacCode = fromStorageDetail.FacCode;
                                toStorageDetail.FreezeQty = 0;
                                toStorageDetail.LastStorageAgeDate = fromStorageDetail.LastStorageAgeDate;
                                toStorageDetail.LocationCode = tLocationCode;
                                toStorageDetail.Lotno = fromStorageDetail.Lotno;
                                toStorageDetail.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                                toStorageDetail.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                                toStorageDetail.MaintainUser = userCode;
                                toStorageDetail.MCode = fromStorageDetail.MCode;
                                toStorageDetail.MDesc = fromStorageDetail.MDesc;
                                toStorageDetail.ProductionDate = fromStorageDetail.ProductionDate;
                                toStorageDetail.ReworkApplyUser = fromStorageDetail.ReworkApplyUser;
                                toStorageDetail.StorageAgeDate = fromStorageDetail.StorageAgeDate;
                                toStorageDetail.StorageCode = fromStorageDetail.StorageCode;
                                toStorageDetail.StorageQty = 0;
                                toStorageDetail.SupplierLotNo = fromStorageDetail.SupplierLotNo;
                                toStorageDetail.Unit = fromStorageDetail.Unit;
                                toStorageDetail.ValidStartDate = fromStorageDetail.ValidStartDate;
                                facade.AddStorageDetail(toStorageDetail);

                            }


                            #region add by sam 2016年4月27日
                            storageDetailSn.CartonNo = tcarton;
                            facade.UpdateStorageDetailSN(storageDetailSn);

                            fromStorageDetail.StorageQty -= 1;
                            fromStorageDetail.AvailableQty -= 1;
                            facade.UpdateStorageDetail(fromStorageDetail);

                            toStorageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(tcarton);
                            toStorageDetail.StorageQty += 1;
                            toStorageDetail.AvailableQty += 1;
                            facade.UpdateStorageDetail(toStorageDetail);
                            #endregion


                            StorloctransDetail storloctransDetail = (StorloctransDetail)_WarehouseFacade.GetStorloctransdetail(transNo, fromStorageDetail.MCode);
                            storloctransDetail.Qty += fromStorageDetail.AvailableQty;
                            _WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);
                            StorloctransDetailCarton storloctransDetailCarton =
                                (StorloctransDetailCarton)
                                _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton,
                                                                            tcarton);
                            if (storloctransDetailCarton != null)
                            {// 如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                                storloctransDetailCarton.Qty += 1;
                                storloctransDetailCarton.MaintainDate = dbDateTime.DBDate;
                                storloctransDetailCarton.MaintainTime = dbDateTime.DBTime;
                                storloctransDetailCarton.MaintainUser = userCode;
                                _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                            }
                            else
                            {
                                #region 4，	向TBLStorLocTransDetailCarton插入一笔数据。


                                StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                                newstorlocDetailCarton.Transno = transNo;// storloctransdetailObj.Transno;   //storlocDetailCarton.Transno = StorLocTransDetail.TransNo;
                                newstorlocDetailCarton.DqmCode = storloctransdetailObj.DqmCode;
                                newstorlocDetailCarton.FacCode = "10Y2";
                                newstorlocDetailCarton.MCode = storloctransdetailObj.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                                newstorlocDetailCarton.Qty = 1;//   //QTY: 1
                                newstorlocDetailCarton.LocationCode = tLocationCode;// FormatHelper.CleanString(txtTLocationCodeEdit.Text);//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                newstorlocDetailCarton.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCartonEdit.Text);//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                                newstorlocDetailCarton.FromlocationCode = fromStorageDetail.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                                newstorlocDetailCarton.Fromcartonno = fromCarton;// rageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                                newstorlocDetailCarton.Lotno = fromStorageDetail.Lotno; //LotNo：TBLStorageDetail. LotNo
                                newstorlocDetailCarton.CUser = userCode;	//	CUSER
                                newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                                newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                                newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                newstorlocDetailCarton.MaintainUser = userCode;
                                _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                                #endregion

                                //说明：表中没有的字段来自TBLStorageDetail中对应字段
                                //F  向表TBLStorLocTransDetailSN插入一条数据。


                                StorloctransDetailSN newStorloctransDetailSn = new StorloctransDetailSN();
                                newStorloctransDetailSn.Sn = inputsn;//SN：TBLStorageDetailSN.SN
                                newStorloctransDetailSn.Transno = storloctransdetailObj.Transno; // TBLStorLocTransDetail. TransNo
                                newStorloctransDetailSn.Fromcartonno = fromCarton;//TBLStorageDetail.LocationCode
                                newStorloctransDetailSn.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCodeEdit.Text);//CARTONNO：TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                newStorloctransDetailSn.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                newStorloctransDetailSn.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                newStorloctransDetailSn.MaintainUser = userCode;
                                _WarehouseFacade.AddStorloctransdetailsn(newStorloctransDetailSn);



                            }
                            #endregion
                        }
                        else
                        {
                            #region 和非单件管控


                            //B 检查TBLStorageDetail. AvailableQTY>PDA页面填的数量。如果否，提示：输入的数量大于库存可用数量。
                            #region Check 数量
                            int qty;
                            if (!string.IsNullOrEmpty(inputqty))
                            {
                                try
                                {
                                    qty = Int32.Parse(inputqty);
                                }
                                catch (Exception ex)
                                {
                                    msg = "数量只能输入大于0的数字";
                                    return msg;
                                }
                                if (qty <= 0)
                                {
                                    msg = "数量只能输入大于0的数字";
                                    return msg;
                                }
                            }
                            else
                            {
                                msg = "数量不能为空";
                                return msg;

                            }
                            #endregion

                            decimal stroageQTY = fromStorageDetail.StorageQty - fromStorageDetail.FreezeQty;

                            if (stroageQTY < qty)
                            {

                                msg = "输入的数量大于库存可用数量";
                                return msg;
                            }

                            fromStorageDetail.AvailableQty -= qty;
                            fromStorageDetail.StorageQty -= qty;
                            facade.UpdateStorageDetail(fromStorageDetail);


                         
                            if (toStorageDetail == null)
                            {

                                toStorageDetail = new StorageDetail();
                                toStorageDetail.AvailableQty = 0;
                                toStorageDetail.CartonNo = tcarton;
                                toStorageDetail.CDate = FormatHelper.TODateInt(DateTime.Now);
                                toStorageDetail.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                                toStorageDetail.CUser = userCode;
                                toStorageDetail.DQMCode = fromStorageDetail.DQMCode;
                                toStorageDetail.FacCode = fromStorageDetail.FacCode;
                                toStorageDetail.FreezeQty = 0;
                                toStorageDetail.LastStorageAgeDate = fromStorageDetail.LastStorageAgeDate;
                                toStorageDetail.LocationCode = tLocationCode;
                                toStorageDetail.Lotno = fromStorageDetail.Lotno;
                                toStorageDetail.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                                toStorageDetail.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                                toStorageDetail.MaintainUser = userCode;
                                toStorageDetail.MCode = fromStorageDetail.MCode;
                                toStorageDetail.MDesc = fromStorageDetail.MDesc;
                                toStorageDetail.ProductionDate = fromStorageDetail.ProductionDate;
                                toStorageDetail.ReworkApplyUser = fromStorageDetail.ReworkApplyUser;
                                toStorageDetail.StorageAgeDate = fromStorageDetail.StorageAgeDate;
                                toStorageDetail.StorageCode = fromStorageDetail.StorageCode;
                                toStorageDetail.StorageQty = 0;
                                toStorageDetail.SupplierLotNo = fromStorageDetail.SupplierLotNo;
                                toStorageDetail.Unit = fromStorageDetail.Unit;
                                toStorageDetail.ValidStartDate = fromStorageDetail.ValidStartDate;
                                facade.AddStorageDetail(toStorageDetail);

                            }

                            toStorageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(tcarton);
                            toStorageDetail.StorageQty += qty;
                            toStorageDetail.AvailableQty += qty;

                            facade.UpdateStorageDetail(toStorageDetail);

                            StorloctransDetailCarton storloctransDetailCarton =
                            (StorloctransDetailCarton)
                            _WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCarton,
                                                                           tcarton);
                            if (storloctransDetailCarton != null)
                            {// 如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                                storloctransDetailCarton.Qty += qty;// int.Parse(txtNumEdit.Text);
                                storloctransDetailCarton.MaintainDate = dbDateTime.DBDate;
                                storloctransDetailCarton.MaintainTime = dbDateTime.DBTime;
                                storloctransDetailCarton.MaintainUser = userCode;
                                _WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                            }
                            else
                            {
                                #region 如果没有：插入一条数据 	向TBLStorLocTransDetailCarton插入一笔数据。
                                StorloctransDetailCarton newstorlocDetailCarton = new StorloctransDetailCarton();
                                newstorlocDetailCarton.Transno = transNo;// storloctransdetailObj.Transno;
                                newstorlocDetailCarton.DqmCode = fromStorageDetail.DQMCode;// storloctransdetailObj.DqmCode;
                                newstorlocDetailCarton.FacCode = fromStorageDetail.FacCode;
                                newstorlocDetailCarton.MCode = storageCarton.MCode;// storloctransdetailObj.MCode;//MCODE：TBLStorLocTransDetail.MCODE
                                newstorlocDetailCarton.Qty = qty; //QTY: 1
                                newstorlocDetailCarton.LocationCode = tLocationCode;//FormatHelper.CleanString(txtTLocationCodeEdit.Text);//LocationCode: TBLLOCATION.LOCATIONCODE(根据PDA页面的目标货位找)
                                newstorlocDetailCarton.Cartonno = tcarton;// FormatHelper.CleanString(txtTLocationCartonEdit.Text);//CARTONNO：PDA页面的目标箱号 txtTLocationCartonEdit
                                newstorlocDetailCarton.FromlocationCode = fromStorageDetail.LocationCode;  //FromLocationCode：TBLStorageDetail.LocationCode
                                newstorlocDetailCarton.Fromcartonno = fromCarton;// storageDetail.CartonNo; //FromCARTONNO：TBLStorageDetail. CARTONNO
                                newstorlocDetailCarton.Lotno = storageCarton.Lotno; //LotNo：TBLStorageDetail. LotNo
                                newstorlocDetailCarton.CUser = userCode;	//	CUSER
                                newstorlocDetailCarton.CDate = dbDateTime.DBDate;	//	CDATE
                                newstorlocDetailCarton.CTime = dbDateTime.DBTime;//	CTIME
                                newstorlocDetailCarton.MaintainDate = dbDateTime.DBDate;	//	MDATE
                                newstorlocDetailCarton.MaintainTime = dbDateTime.DBTime;	//	MTIME
                                newstorlocDetailCarton.MaintainUser = userCode;
                                _WarehouseFacade.AddStorloctransdetailcarton(newstorlocDetailCarton);
                                #endregion
                            }
                            #endregion
                        }

                    }
                }
                #endregion




                fromStorageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(fromCarton);
                if (fromStorageDetail != null && fromStorageDetail.StorageQty == 0)
                {
                    _WarehouseFacade.DeleteStorageDetail(fromStorageDetail);
                }






                string documentno = CreateAutoDocmentsNo();
                SaveDocmentsNo(documentno, userCode);

                this.DataProvider.CommitTransaction();
                msg = "提交成功";
                return msg;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                msg = ex.Message;
                return msg;
            }
        }

        #endregion

        #region Gird
        [WebMethod(EnableSession = true)]
        public DataTable LocStorTransView(string transNo, string dqmCode, string fromCartonno, string locationCode, string cartonno)
        {
            InventoryFacade inventoryFacade = new InventoryFacade(DataProvider);
            object[] objs = inventoryFacade.QueryStorTransDetailCarton(transNo, dqmCode, fromCartonno, locationCode, cartonno);//add 
            DataTable dt = new DataTable("ExampleDataTable");
            dt.Columns.Add("鼎桥物料编码", typeof(string));  // DQMCODE
            dt.Columns.Add("源货位", typeof(string));  // FromLocationCode
            dt.Columns.Add("目标货位", typeof(string));  // LocationCode
            dt.Columns.Add("已移数量", typeof(string));  // QTY
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    StorloctransDetailCarton carton = objs[i] as StorloctransDetailCarton;
                    if (carton != null)
                        dt.Rows.Add(carton.DqmCode, carton.FromlocationCode, carton.LocationCode,
                                             carton.Qty.ToString("G0"));
                }
            }
            return dt;
        }
        #endregion
    }
}
