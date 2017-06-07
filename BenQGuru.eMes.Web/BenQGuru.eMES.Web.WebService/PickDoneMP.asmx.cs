using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Common.Domain; 

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// MaterialOnShelves 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class PickDoneMP : System.Web.Services.WebService
    {
        public PickDoneMP()
        {
            InitDbItems();
        }
        private string m_DbName;
        private IDomainDataProvider _domainDataProvider;
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

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string GetActOnShelvesQTY(string CartonNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            return facade.QueryActOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo))).ToString();
        }
        [WebMethod]
        public string GetPlanOnShelvesQTY(string CartonNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            return facade.QueryPlanOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo))).ToString();
        }
        [WebMethod]
        public string GetCUSMcode(string CartonNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            string result= facade.GetCUSCodebyCartonNo(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo)));
            return result;
        }
        [WebMethod]
        public DataTable GetDataGrid(string CartonNo,string LocationNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            object[] objs= facade.QueryOnshelvesDetail( FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo)),FormatHelper.PKCapitalFormat(FormatHelper.CleanString(LocationNo)));
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (objs != null)
            {
                //dt.Columns.Add("选中", typeof(string));
                //dt.Columns.Add("入库指令号", typeof(string));
                //dt.Columns.Add("箱号", typeof(string));
                //dt.Columns.Add("推荐货位", typeof(string));
                //dt.Columns.Add("货位号", typeof(string));
                //dt.Columns.Add("鼎桥物料编码", typeof(string));
                //dt.Columns.Add("行号", typeof(string));
                dt.Columns.Add("Check", typeof(string));
                dt.Columns.Add("STNO", typeof(string));
                dt.Columns.Add("cartonno", typeof(string));
                dt.Columns.Add("relocaNo", typeof(string));
                dt.Columns.Add("locaNo", typeof(string));
                dt.Columns.Add("DQMCode", typeof(string));
                dt.Columns.Add("stline", typeof(string));
                for (int i = 0; i < objs.Length; i++)
                {
                    Asndetailexp Asndetail = objs[i] as Asndetailexp;
                    dt.Rows.Add("",Asndetail.Stno, Asndetail.Cartonno, Asndetail.ReLocationCode, Asndetail.LocationCode, Asndetail.DqmCode,Asndetail.Stline.ToString());
                }
                              
            }
            return dt;
        }
        [WebMethod]
        public DataTable GetDataTable()
        {
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            dt.Columns.Add("选中", typeof(string));
            dt.Columns.Add("入库指令号", typeof(string));
            dt.Columns.Add("箱号", typeof(string));
            dt.Columns.Add("推荐货位", typeof(string));
            dt.Columns.Add("货位号", typeof(string));
            dt.Columns.Add("鼎桥物料编码", typeof(string));
            dt.Columns.Add("行号", typeof(string));
            dt.Rows.Add("","1", "Tom", "1", "Tom", "Tom", "Tom");
            dt.Rows.Add("", "2", "Tom", "Tom", "2", "Tom", "Tom");
            dt.Rows.Add("", "3", "Jim", "Tom", "Tom", "3", "Tom");
            dt.Rows.Add("", "4", "Tom", "Tom", "Tom", "Tom", "4");
            dt.Rows.Add("", "5", "Tom", "5", "Tom", "Tom", "Tom");
            return dt;
        }
        [WebMethod]
        public string OnShelves(DataTable dt,string cartonNO,string UserCode)
        {
            WarehouseFacade facade = null;
            InventoryFacade _Invenfacade = null;
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(DataProvider);
            object obj = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            if(_Invenfacade==null)
            {
                _Invenfacade=new InventoryFacade(DataProvider);
            }
            this.DataProvider.BeginTransaction();
            try
            {
                string Stno = string.Empty;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    obj = facade.GetAsndetail(int.Parse(dt.Rows[j]["stline"].ToString()),dt.Rows[j]["stno"].ToString());
                    if (obj != null)
                    {
                        Stno = (obj as Asndetail).Stno;
                        #region 更新asndetail表 更新actqty和状态
                        //update actqty and status
                        Asndetail asndetail = obj as Asndetail;
                        asndetail.ActQty = asndetail.QcpassQty;
                        asndetail.Status =ASNDetail_STATUS.ASNDetail_Close;
                        asndetail.MaintainDate = dbTime.DBDate;
                        asndetail.MaintainTime = dbTime.DBTime;
                        asndetail.MaintainUser = UserCode;
                        facade.UpdateAsndetail(asndetail);
                        #endregion
                        #region 更新asndetailitem表 更新 actqty 和更新invoicedetail表，更新actqty
                        object[] itemobjs = facade.GetASNDetailItembyStnoAndStline(asndetail.Stno, asndetail.Stline.ToString());
                        if (itemobjs != null)
                        {
                            for (int i = 0; i < itemobjs.Length; i++)
                            {
                                Asndetailitem asnitem = itemobjs[i] as Asndetailitem;
                                asnitem.ActQty = asnitem.QcpassQty;
                                asnitem.MaintainDate = dbTime.DBDate;
                                asnitem.MaintainTime = dbTime.DBTime;
                                asnitem.MaintainUser = UserCode;
                                facade.UpdateAsndetailitem(asnitem);

                                object invoiobj = _Invenfacade.GetInvoicesDetail(asnitem.Invno, int.Parse(asnitem.Invline));
                                InvoicesDetail inv = invoiobj as InvoicesDetail;
                                inv.ActQty += Convert.ToInt32(asnitem.QcpassQty);
                                inv.MaintainDate = dbTime.DBDate;
                                inv.MaintainTime = dbTime.DBTime;
                                inv.MaintainUser = UserCode;

                                _Invenfacade.UpdateInvoicesDetail(inv);
                            }
                        }
                        #endregion

                        //新增数据tblstoragedetail
                        object asnobj = facade.GetAsn(asndetail.Stno);
                        if (asnobj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                           // WebInfoPublish.Publish(this, "$Error_ASN_NO_DATA", this.languageComponent1);

                            return "ASN 中没有数据:" + asndetail.Stno;
                        }
                        Asn asn = asnobj as Asn;
                        #region 在storagedetail表中增加一条数据
                        StorageDetail stordetail = _Invenfacade.CreateNewStorageDetail();
                        stordetail.AvailableQty = asndetail.ActQty;
                        stordetail.CartonNo = asndetail.Cartonno;
                        stordetail.CDate = dbTime.DBDate;
                        stordetail.CTime = dbTime.DBTime;
                        stordetail.CUser = UserCode;
                        stordetail.DQMCode = asndetail.DqmCode;
                        stordetail.FacCode = (asnobj as Asn).FacCode;
                        stordetail.FreezeQty = 0;
                        stordetail.LastStorageAgeDate = dbTime.DBDate;

                        stordetail.LocationCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(cartonNO));
                        stordetail.Lotno = asndetail.Lotno;
                        stordetail.MaintainDate = dbTime.DBDate;
                        stordetail.MaintainTime = dbTime.DBTime;
                        stordetail.MaintainUser = UserCode;
                        stordetail.MCode = asndetail.MCode;
                        stordetail.MDesc = asndetail.MDesc;
                        stordetail.ProductionDate = asndetail.Production_Date;

                        stordetail.ReworkApplyUser = (asnobj as Asn).ReworkapplyUser;
                        stordetail.StorageAgeDate = string.IsNullOrEmpty(asndetail.StorageageDate.ToString()) ? dbTime.DBDate : int.Parse(asndetail.StorageageDate);
                        stordetail.StorageCode = (asnobj as Asn).StorageCode;
                        stordetail.StorageQty = asndetail.ActQty;
                        stordetail.SupplierLotNo = asndetail.Supplier_lotno;
                        stordetail.Unit = asndetail.Unit;

                        _Invenfacade.AddStorageDetail(stordetail);
                        #endregion
                        #region 在StorageDetailSN表中增加数据
                        //新增数据tblStorageDetailSN

                        object[] snobj = facade.GetASNDetailSNbyStnoandStline(asndetail.Stno, asndetail.Stline.ToString());
                        if (snobj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                           // WebInfoPublish.Publish(this, "$Error_ASNDetail_NO_DATA", this.languageComponent1);

                            return "ASNDetail表中没有数据:" + asndetail.Stno + "/" + asndetail.Stline.ToString();
                        }
                        for (int i = 0; i < snobj.Length; i++)
                        {
                            StorageDetailSN storDetailSN = _Invenfacade.CreateNewStorageDetailSN();
                            Asndetailsn detailSN = snobj[i] as Asndetailsn;
                            storDetailSN.CartonNo = detailSN.Cartonno;
                            storDetailSN.CDate = dbTime.DBDate;
                            storDetailSN.CTime = dbTime.DBTime;
                            storDetailSN.CUser = UserCode;
                            storDetailSN.MaintainDate = dbTime.DBDate;
                            storDetailSN.MaintainTime = dbTime.DBTime;
                            storDetailSN.MaintainUser = UserCode;
                            storDetailSN.PickBlock = "N";
                            storDetailSN.SN = detailSN.Sn;

                            _Invenfacade.AddStorageDetailSN(storDetailSN);

                        }
                        #endregion
                        #region 在invinouttrans表中增加一条数据
                        InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                        trans.CartonNO = asndetail.Cartonno;
                        trans.DqMCode = asndetail.DqmCode;
                        trans.FacCode = asn.FacCode;
                        trans.FromFacCode = asn.FromfacCode;
                        trans.FromStorageCode = asn.FromstorageCode;
                        trans.InvNO = asn.Invno;
                        trans.InvType = asn.StType;
                        trans.LotNo = asndetail.Lotno;
                        trans.MaintainDate = dbTime.DBDate;
                        trans.MaintainTime = dbTime.DBTime;
                        trans.MaintainUser = UserCode;
                        trans.MCode = asndetail.MCode;
                        trans.ProductionDate = asndetail.Production_Date;
                        trans.Qty = asndetail.ActQty;
                        trans.Serial = 0;
                        trans.StorageAgeDate = int.Parse(asndetail.StorageageDate);
                        trans.StorageCode = asn.StorageCode;
                        trans.SupplierLotNo = asndetail.Supplier_lotno;
                        trans.TransNO = asn.Stno;
                        trans.TransType = "IN";
                        trans.Unit = asndetail.Unit;
                        facade.AddInvInOutTrans(trans);
                        #endregion
                    }
                }
                #region  上架完成后，检查这个stno在asndetail中的状态是否都是close，cancel，如果是将asn表的状态更新为close，cancel
                if (facade.JudgeASNDetailStatus(Stno,ASNDetail_STATUS.ASNDetail_Close))
                {
                    object asnobj = facade.GetAsn(Stno);
                    if (asnobj == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        //WebInfoPublish.Publish(this, "$Error_ASN_NO_DATA", this.languageComponent1);

                        return "ASN 中没有数据:" + Stno;
                    }
                    Asn asn = asnobj as Asn;
                    asn.Status =ASN_STATUS.ASN_Close;
                    asn.MaintainDate = dbTime.DBDate;
                    asn.MaintainTime = dbTime.DBTime;
                    asn.MaintainUser = UserCode;

                    facade.UpdateAsn(asn);
                }
                if (facade.JudgeASNDetailStatus(Stno,ASNDetail_STATUS.ASNDetail_Cancel))
                {
                    object asnobj = facade.GetAsn(Stno);
                    if (asnobj == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        //WebInfoPublish.Publish(this, "$Error_ASN_NO_DATA", this.languageComponent1);

                        return "ASN中没有数据:" + Stno;
                    }
                    Asn asn = asnobj as Asn;
                    asn.Status =ASN_STATUS.ASN_Cancel;
                    asn.MaintainDate = dbTime.DBDate;
                    asn.MaintainTime = dbTime.DBTime;
                    asn.MaintainUser = UserCode;

                    facade.UpdateAsn(asn);
                }
                #endregion
                #region  通过入库指令号，查找invno，检查actqty是否等于planqty，如果等于将finishflag=Y
                object asnobj1 = facade.GetAsn(Stno);
                if (asnobj1 == null)
                {
                    this.DataProvider.RollbackTransaction();
                    //WebInfoPublish.Publish(this, "$Error_ASN_NO_DATA", this.languageComponent1);

                    return "ASN中没有数据:" + Stno;
                }
                Asn asn1 = asnobj1 as Asn;
                if (facade.JudgeInvoiceDetailStatus(asn1.Invno))
                {
                    object invobj = _Invenfacade.GetInvoices(asn1.Invno);
                    if (invobj == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        //WebInfoPublish.Publish(this, "$Error_INV_NO_DATA", this.languageComponent1);

                        return "INV中没有数据:" + asn1.Invno;
                    }
                    Invoices inv = invobj as Invoices;
                    inv.FinishFlag = "Y";
                    inv.MaintainDate = dbTime.DBDate;
                    inv.MaintainTime = dbTime.DBTime;
                    inv.MaintainUser = UserCode;
                    _Invenfacade.UpdateInvoices(inv);
                }
                #endregion

                this.DataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
               
            }

            return "上架成功";
                              
        }

        
    }
}
