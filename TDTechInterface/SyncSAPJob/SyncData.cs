using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using BenQGuru.eMES.SAPRFCService;
using BenQGuru.SAP.SAPNcoLib;
using System.Data;
using SAP.Middleware.Connector;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.InterfaceFacade;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.InterfaceDomain;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using ShareLib;
using System.Configuration;

namespace SyncSAPJob
{
    public class SyncData
    {
        private IDomainDataProvider _domainDataProvider = null;
        private MainDataFacade _MainDataFacade = null;
        private InvoicesFacade _InvoicesFacade = null;

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new Exception("必须提供命令行参数！");
                SyncData sd = new SyncData();

                sd.InitFacade();
                switch (args[0].ToLower())
                {
                    case JobType.Customer:
                        sd.SyncCustomer();
                        break;
                    case JobType.Vendor:
                        sd.SyncVendor();
                        break;
                    case JobType.Storage:
                        sd.SyncStorage();
                        break;
                    case JobType.Material:
                        sd.SyncMaterial();
                        break;
                    case JobType.PO:
                        sd.SyncPO();
                        break;
                    case JobType.DN:
                        sd.SyncDN();
                        break;
                    case JobType.UB:
                        sd.SyncUB();
                        break;
                    case JobType.RS:
                        sd.SyncRS();
                        break;
                    case JobType.Stock:
                        sd.SyncStock();
                        break;
                    case JobType.StockCheck:
                        sd.SyncStockCheck();
                        break;
                    case JobType.SapLog:
                        sd.SyncSapLog();
                        break;
                    case "qcexpiredscan":
                        sd.QCExpiredScan();
                        break;
                    case "receivedexpiredscan":
                        sd.ReceiveExpiredScan();
                        break;
                    case "yfrexpiredscan":
                        sd.YFRExpiredScan();
                        break;
                    case "sapclosingscan":
                        sd.SapClosingScan();
                        break;
                    case "mcodevalidityscan":
                        sd.MCodeValidityScan();
                        break;
                    case ShareLib.ShareKit.MailName.ReceiveExpiredMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.ReceiveExpiredMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.ReceiveRejectMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.ReceiveRejectMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.IQCSQEMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.IQCSQEMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.OQCSQEMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.OQCSQEMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.IQCExceptionMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.IQCExceptionMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.OQCExceptionMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.OQCExceptionMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.PickingExceptionMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.PickingExceptionMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.MaterialExpiredMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.MaterialExpiredMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.OQCExpiredMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.OQCExpiredMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.IQCExpiredMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.IQCExpiredMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.IQC_SQEExpiredMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.IQC_SQEExpiredMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.OQC_SQEExpiredMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.OQC_SQEExpiredMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.YFRExpiredMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.YFRExpiredMail); ;
                        break;
                    case ShareLib.ShareKit.MailName.Sap2MESDiversityMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.Sap2MESDiversityMail);
                        //sd.SendMail2(ShareLib.ShareKit.MailName.Sap2MESDiversityMail);
                        break;
                    case ShareLib.ShareKit.MailName.SapClosingErrorMail:
                        sd.SendMail(ShareLib.ShareKit.MailName.SapClosingErrorMail); ;
                        break;
                    default:
                        break;


                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);

            }
        }

        private void InitFacade()
        {
            if (_MainDataFacade == null)
            {
                _MainDataFacade = new MainDataFacade(this.DataProvider);
            }
            if (_InvoicesFacade == null)
            {
                _InvoicesFacade = new InvoicesFacade(this.DataProvider);
            }
        }




        private void SyncCustomer()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.Customer;

                DataSet ds = ncoClient.Execute();
                SaveCustomerData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveCustomerData(DataSet ds)
        {
            if (ds != null && ds.Tables[SAPRfcOutTableName.Customer] != null)
            {
                if (ds.Tables[SAPRfcOutTableName.Customer].Rows.Count > 0)
                {
                    try
                    {
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        I_Sapcustomer ct = new I_Sapcustomer();
                        this.DataProvider.BeginTransaction();
                        int date = Convert.ToInt32(Convert.ToDateTime(intDate).AddDays(-30));
                        _MainDataFacade.DeleteSapCustomer(date);
                        foreach (DataRow row in ds.Tables[SAPRfcOutTableName.Customer].Rows)
                        {
                            ct.Id = 0;//ID是自增列，可以不用赋值
                            ct.Customercode = Convert.ToString(row["KUNNR"]);
                            ct.Customername = Convert.ToString(row["NAME"]);
                            ct.Address = Convert.ToString(row["STRAS"]);
                            ct.Tel = Convert.ToString(row["TELF1"]);
                            ct.Flag = Convert.ToString(row["AUFSD"]);
                            ct.Sdate = intDate;
                            ct.Stime = intTime;
                            ct.Pdate = intDate;
                            ct.Ptime = intTime;
                            ct.Mesflag = MiddleTableFlag.Wait;

                            _MainDataFacade.AddSapcustomer(ct);

                        }
                        _MainDataFacade.MergeCustomer();
                        _MainDataFacade.UpdateSapcustomerSuccess();

                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
                    }
                }
            }
        }



        private void SyncVendor()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.Vendor;

                DataSet ds = ncoClient.Execute();
                SaveVendorData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveVendorData(DataSet ds)
        {
            if (ds != null && ds.Tables[SAPRfcOutTableName.Vendor] != null)
            {
                if (ds.Tables[SAPRfcOutTableName.Vendor].Rows.Count > 0)
                {
                    try
                    {
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        I_Sapvendor vd = new I_Sapvendor();
                        this.DataProvider.BeginTransaction();
                        int date = FormatHelper.TODateInt(FormatHelper.ToDateTime(intDate).AddDays(-30));
                        _MainDataFacade.DeleteSapVendor(date);
                        foreach (DataRow row in ds.Tables[SAPRfcOutTableName.Vendor].Rows)
                        {
                            vd.Id = 0;//ID是自增列，可以不用赋值
                            vd.Vendorcode = Convert.ToString(row["LIFNR"]);
                            vd.Vendorname = Convert.ToString(row["NAME1"]);
                            vd.Alias = Convert.ToString(row["SORTL"]);
                            vd.Vendoruser = Convert.ToString(row["NAMEV"]);
                            vd.Vendoraddr = Convert.ToString(row["STRAS"]);
                            vd.Faxno = Convert.ToString(row["TELFX"]);
                            vd.Mobileno = Convert.ToString(row["TELF2"]);
                            vd.Sdate = intDate;
                            vd.Stime = intTime;
                            vd.Pdate = intDate;
                            vd.Ptime = intTime;
                            vd.Mesflag = MiddleTableFlag.Wait;

                            _MainDataFacade.AddSapvendor(vd);

                        }
                        _MainDataFacade.MergeVendor();
                        _MainDataFacade.UpdateSapvendorSuccess();

                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
                    }
                }
            }
        }

        private void SyncStorage()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.Storage;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_WERKS", SAPRfcDefaultPara.FacCode);
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveStorageData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveStorageData(DataSet ds)
        {
            if (ds != null && ds.Tables[SAPRfcOutTableName.Storage] != null)
            {
                if (ds.Tables[SAPRfcOutTableName.Storage].Rows.Count > 0)
                {
                    try
                    {
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        I_Sapstorage st = new I_Sapstorage();
                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[SAPRfcOutTableName.Storage].Rows)
                        {
                            st.Id = 0;//ID是自增列，可以不用赋值
                            st.Storagecode = Convert.ToString(row["LGORT"]);
                            st.Storagename = Convert.ToString(row["LGOBE"]);
                            st.Address1 = Convert.ToString(row["ADDR1"]);
                            st.Address2 = Convert.ToString(row["ADDR2"]);
                            st.Address3 = Convert.ToString(row["ADDR3"]);
                            st.Address4 = Convert.ToString(row["ADDR4"]);
                            st.Contactuser1 = Convert.ToString(row["NAME1"]);
                            st.Contactuser2 = Convert.ToString(row["NAME2"]);
                            st.Contactuser3 = Convert.ToString(row["NAME3"]);
                            st.Contactuser4 = Convert.ToString(row["NAME4"]);
                            st.Sdate = intDate;
                            st.Stime = intTime;
                            st.Pdate = intDate;
                            st.Ptime = intTime;
                            st.Mesflag = MiddleTableFlag.Wait;

                            _MainDataFacade.AddSapstorage(st);

                        }
                        _MainDataFacade.MergeStorage();
                        _MainDataFacade.UpdateSapstorageSuccess();

                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
                    }
                }
            }
        }

        private void SyncMaterial()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.Material;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_WERKS", SAPRfcDefaultPara.FacCode);
                importParameters.Add("I_VKORG", "62Y2");
                importParameters.Add("I_VTWEG", "O1");
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveMaterialData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveMaterialData(DataSet ds)
        {
            if (ds != null && ds.Tables[SAPRfcOutTableName.Material] != null)
            {
                if (ds.Tables[SAPRfcOutTableName.Material].Rows.Count > 0)
                {
                    try
                    {
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }

                        I_Sapmaterial ma = new I_Sapmaterial();
                        this.DataProvider.BeginTransaction();
                        int date = Convert.ToInt32(Convert.ToDateTime(intDate).AddDays(-30));
                        _MainDataFacade.DeleteSapmaterial(date);

                        foreach (DataRow row in ds.Tables[SAPRfcOutTableName.Material].Rows)
                        {
                            ma.Id = 0;//ID是自增列，可以不用赋值
                            ma.Mcode = Convert.ToString(row["MATNR"]);
                            ma.Dqmcode = Convert.ToString(row["ZEINR"]);
                            ma.Muom = Convert.ToString(row["MEINS"]);
                            ma.Rohs = Convert.ToString(row["KZUMW"]);
                            ma.Mchshortdesc = Convert.ToString(row["CMAKTX"]);
                            ma.Menshortdesc = Convert.ToString(row["MAKTX"]);
                            ma.Menlongdesc = Convert.ToString(row["LETXT"]);
                            ma.Mchlongdesc = Convert.ToString(row["LCTXT"]);
                            ma.Modelcode = Convert.ToString(row["MATKL"]);
                            ma.Materialtype = Convert.ToString(row["MTART"]);
                            ma.Mstate1 = Convert.ToString(row["LVORM"]);
                            ma.Mstate2 = Convert.ToString(row["MSTAE"]);
                            ma.Mstate3 = Convert.ToString(row["MSTAV"]);
                            if (row["MSTDV"] == DBNull.Value)
                            {
                                ma.Validfrom = 0;
                            }
                            else
                            {
                                ma.Validfrom = Convert.ToInt32(row["MSTDV"]);
                            }
                            ma.Eattribute8 = Convert.ToString(row["MTPOS"]);
                            ma.Cdqty = Convert.ToString(row["CDMNG"]);
                            ma.Cdfor = Convert.ToString(row["CDTYP"]);
                            ma.Purchasinggroup = Convert.ToString(row["EKGRP"]);
                            ma.Abcindicator = Convert.ToString(row["MAABC"]);
                            ma.Mrptype = Convert.ToString(row["DISMM"]);
                            ma.Reorderpoint = Convert.ToDecimal(row["MINBE"]);
                            ma.Mrpcontorller = Convert.ToString(row["DISPO"]);
                            ma.Minimumlotsize = Convert.ToDecimal(row["BSTMI"]);
                            ma.Roundingvalue = Convert.ToDecimal(row["BSTRF"]);
                            ma.Specialprocyrement = Convert.ToString(row["SOBSL"]);
                            ma.Safetystock = Convert.ToDecimal(row["EISBE"]);
                            ma.Bulkmaterial = Convert.ToString(row["SCHGT"]);
                            ma.Sdate = intDate;
                            ma.Stime = intTime;
                            ma.Pdate = intDate;
                            ma.Ptime = intTime;
                            ma.Mesflag = MiddleTableFlag.Wait;

                            _MainDataFacade.AddSapmaterial(ma);

                        }
                        _MainDataFacade.MergeMaterial();
                        _MainDataFacade.UpdateSapmaterialSuccess();

                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                    finally
                    {
                        ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
                    }
                }
            }
        }

        private void SyncPO()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.PO;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();

                //importParameters.Add("I_DATUM", "20160301");
                //importParameters.Add("I_UZEIT", "000000");
                #region add by sam
                string strDate = "";
                string strTime = "";
                if (!Common.TryGetStampDbDateTime(ref strDate, ref strTime, SAPJOBTIMESTAMP.PO))
                {
                    Log.Error("没有维护PO的时间戳参数");
                    return;
                }
                importParameters.Add("I_DATUM", strDate);
                importParameters.Add("I_UZEIT", strTime);
                #endregion

                importParameters.Add("I_BUKRS", SAPRfcDefaultPara.CompanyCode); //not necessary
                importParameters.Add("I_EKORG", SAPRfcDefaultPara.PurchOrgCode); //not necessary
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SavePOData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SavePOData(DataSet ds)
        {
            //将之前JOB未处理的记录状态置为"L"
            _InvoicesFacade.UpdateSappoFlag(MiddleTableFlag.Last);
            _InvoicesFacade.UpdateInvoicesFlag(MiddleTableFlag.Last, " ( 'POC' ) ");
            #region Insert I_Sappo then commit
            if (ds != null && ds.Tables[SAPRfcOutTableName.PO] != null && ds.Tables[SAPRfcOutTableName.PO].Rows.Count > 0)
            {
                try
                {
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                    this.DataProvider.BeginTransaction();
                    foreach (DataRow row in ds.Tables[SAPRfcOutTableName.PO].Rows)
                    {
                        I_Sappo ipo = new I_Sappo();
                        ipo.Invno = Convert.ToString(row["EBELN"]);
                        ipo.Companycode = Convert.ToString(row["BUKRS"]);
                        ipo.Vendorcode = Convert.ToString(row["LIFNR"]);
                        ipo.Orderstatus = Convert.ToString(row["FRGKX"]) == "G" ? "Release" : "Pending";
                        ipo.Ordertype = Convert.ToString(row["BSART"]);
                        ipo.Pocreatedate = Convert.ToInt32(row["ERDAT"]);
                        ipo.Createuser = Convert.ToString(row["ERNAM"]);
                        ipo.Poupdatedate = Convert.ToInt32(row["AEDAT"]);
                        ipo.Poupdatetime = Convert.ToInt32(row["AEZET"]);
                        ipo.Buyerphone = Convert.ToString(row["TEL_NUMBER"]);
                        ipo.Remark1 = Convert.ToString(row["HTEXT"]);
                        ipo.Remark2 = Convert.ToString(row["HNOTE"]);
                        ipo.Remark3 = Convert.ToString(row["HMARK"]);
                        ipo.Remark4 = Convert.ToString(row["IHREZ"]);
                        ipo.Remark5 = Convert.ToString(row["UNSEZ"]);
                        ipo.Purchorgcode = Convert.ToString(row["EKORG"]);
                        ipo.Purchugcode = Convert.ToString(row["EKGRP"]);
                        ipo.Purchasegroup = Convert.ToString(row["EKNAM"]);

                        ipo.Invline = Convert.ToInt32(row["EBELP"]);
                        string strLineStatus = string.Empty;
                        switch (Convert.ToString(row["LOEKZ"]))
                        {
                            case "L":
                                strLineStatus = "Cancel";
                                break;
                            case "S":
                                strLineStatus = "Pending";
                                break;
                            default:
                                strLineStatus = "Release";
                                break;
                        }
                        ipo.Invlinestatus = strLineStatus;
                        ipo.Faccode = Convert.ToString(row["WERKS"]);
                        ipo.Mcode = Convert.ToString(row["MATNR"]);
                        ipo.Menshortdesc = Convert.ToString(row["TXZ01"]);
                        ipo.Mlongdesc = Convert.ToString(row["MAKTX"]);
                        ipo.Planqty = Convert.ToDecimal(row["MENGE"]);
                        ipo.Plandate = Convert.ToInt32(row["EINDT"]);
                        ipo.Unit = Convert.ToString(row["MEINS"]);

                        ipo.Receiveraddr = Convert.ToString(row["STRAS"]);
                        ipo.Receiveruser = Convert.ToString(row["RINFO"]);
                        //ipo.Shipaddr = Convert.ToString(row["STRAS"]);
                        ipo.Storagecode = Convert.ToString(row["LGORT"]);
                        ipo.Detailremark = Convert.ToString(row["STEXT"]);
                        ipo.Vendormcode = Convert.ToString(row["IDNLF"]);
                        ipo.Prno = Convert.ToString(row["BANFN"]);
                        ipo.Returnflag = Convert.ToString(row["RETPO"]);
                        ipo.Accountassignment = Convert.ToString(row["KNTTP"]);
                        ipo.Itemcategory = Convert.ToString(row["PSTYP"]);
                        ipo.So = Convert.ToString(row["VBELN"]);
                        ipo.Soitemno = Convert.ToString(row["VBELP"]);
                        ipo.Sowbsno = Convert.ToString(row["PS_PSP_PNR"]);
                        ipo.Ccno = Convert.ToString(row["KOSTL"]);

                        ipo.Mesflag = MiddleTableFlag.Wait;
                        ipo.Sdate = intDate;
                        ipo.Stime = intTime;
                        ipo.Pdate = intDate;
                        ipo.Ptime = intTime;

                        _InvoicesFacade.AddSappo(ipo);
                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
            #endregion

            #region merge po into invoice table, commit by po
            object[] arr = _InvoicesFacade.GetSappoByFlag(MiddleTableFlag.Wait);
            //没有待处理的记录，退出
            if (arr == null)
            {
                return;
            }
            List<I_Sappo> sappoList = Common.Array2SappoList(arr);
            if (sappoList.Count <= 0)
            {
                return;
            }

            #region new PO
            var ponoList = sappoList.GroupBy(p => p.Invno).Select(p => p.Key).ToList();
            foreach (var pono in ponoList)
            {
                List<I_Sappo> currList = sappoList.Where(p => p.Invno == pono).ToList();
                string currHeadStatus = currList.First().Orderstatus;
                //Invoices invoice = _InvoicesFacade.GetInvoices(pono) as Invoices;
                bool isUpdate = _InvoicesFacade.QueryInvoicesCount(pono) > 0;


                try
                {
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                    this.DataProvider.BeginTransaction();

                    #region Invoices

                    if (currHeadStatus == POHeadStatus.Release)
                    {
                        //Invoices item = new Invoices();
                        Invoices item = _InvoicesFacade.GetInvoices(pono) as Invoices;
                        if (item == null)
                        {
                            item = new Invoices();
                        }
                        item.Invno = pono;
                        item.Returnflag = currList.First().Returnflag;
                        if (item.Returnflag == "X")
                        {
                            item.Invtype = "POC";
                        }
                        else
                        {
                            item.Invtype = "POR";
                        }
                        item.Plangidate = currList.First().Plandate;

                        item.Invstatus = POHeadStatus.Release;
                        item.Orderstatus = POHeadStatus.Release;
                        item.Companycode = currList.First().Companycode;
                        item.Vendorcode = currList.First().Vendorcode;
                        item.Orderstatus = currList.First().Orderstatus;
                        item.Ordertype = currList.First().Ordertype;
                        item.Pocreatedate = currList.First().Pocreatedate;
                        item.Createuser = currList.First().Createuser;
                        item.Poupdatedate = currList.First().Poupdatedate;
                        item.Poupdatetime = currList.First().Poupdatetime;
                        item.Buyerphone = currList.First().Buyerphone;
                        item.Remark1 = currList.First().Remark1;
                        item.Remark2 = currList.First().Remark2;
                        item.Remark3 = currList.First().Remark3;
                        item.Remark4 = currList.First().Remark4;
                        item.Remark5 = currList.First().Remark5;
                        item.Purchorgcode = currList.First().Purchorgcode;
                        item.Purchugcode = currList.First().Purchugcode;
                        item.Purchasegroup = currList.First().Purchasegroup;
                        item.Finishflag = "N";
                        item.MaintainUser = CommonConstants.MaintainUser;
                        item.MaintainDate = intDate;
                        item.MaintainTime = intTime;
                        item.Cuser = CommonConstants.MaintainUser;
                        item.Cdate = intDate;
                        item.Ctime = intTime;
                        item.Eattribute1 = "W";
                        //_InvoicesFacade.AddInvoices(item);
                        if (isUpdate)
                        {
                            _InvoicesFacade.UpdateInvoices(item);
                        }
                        else
                        {
                            _InvoicesFacade.AddInvoices(item);
                        }
                    }
                    else if (currHeadStatus == POHeadStatus.Pending)
                    {
                        Invoices invoice = _InvoicesFacade.GetInvoices(pono) as Invoices;
                        if (invoice != null)
                        {
                            invoice.Orderstatus = POHeadStatus.Pending;
                            invoice.Invstatus = POHeadStatus.Pending;
                            invoice.Eattribute1 = "W";
                            _InvoicesFacade.UpdateInvoices(invoice);
                        }
                    }

                    #endregion

                    #region Invoicesdetail
                    bool isAllCancel = true;
                    bool isPending = false;
                    foreach (var currDetail in currList)
                    {
                        if (currDetail.Invlinestatus != POHeadStatus.Cancel)
                        {
                            isAllCancel = false;
                        }
                        if (currDetail.Invlinestatus == POHeadStatus.Pending || currDetail.Orderstatus == POHeadStatus.Pending)
                        {
                            isPending = true;
                        }
                        if (currDetail.Invlinestatus == POHeadStatus.Release)
                        {
                            #region detail
                            List<Material> materialList = this.GetAllMaterialList();
                            int intDate1, intTime1;
                            Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                            bool isdelUpdate = _InvoicesFacade.QueryInvoicesdetailCount(currDetail.Invno, currDetail.Invline) > 0;
                            //Invoicesdetail detail = new Invoicesdetail();
                            Invoicesdetail detail = _InvoicesFacade.GetInvoicesdetail(currDetail.Invno,
                                                                                   currDetail.Invline.ToString()) as Invoicesdetail;
                            if (detail == null)
                            {
                                detail = new Invoicesdetail();
                            }
                            detail.Invno = currDetail.Invno;
                            detail.Invline = currDetail.Invline;
                            detail.Status = currDetail.Invlinestatus;
                            detail.Invlinestatus = currDetail.Invlinestatus;
                            detail.Faccode = currDetail.Faccode;
                            detail.Mcode = currDetail.Mcode;
                            detail.Menshortdesc = currDetail.Menshortdesc;
                            detail.Mlongdesc = currDetail.Mlongdesc;
                            detail.Planqty = currDetail.Planqty;
                            detail.Plandate = currDetail.Plandate;


                            detail.Unit = currDetail.Unit;
                            detail.Shipaddr = currDetail.Shipaddr;
                            //add by sam 2016年7月14日
                            detail.Receiveraddr = currDetail.Receiveraddr;
                            detail.Receiveruser = currDetail.Receiveruser;

                            detail.Storagecode = currDetail.Storagecode;
                            detail.Detailremark = currDetail.Detailremark;
                            detail.Vendormcode = currDetail.Vendormcode;
                            detail.Prno = currDetail.Prno;
                            detail.Returnflag = currDetail.Returnflag;
                            detail.Accountassignment = currDetail.Accountassignment;
                            detail.Itemcategory = currDetail.Itemcategory;
                            detail.So = currDetail.So;
                            detail.Soitemno = currDetail.Soitemno;
                            detail.Sowbsno = currDetail.Sowbsno;
                            detail.Ccno = currDetail.Ccno;
                            List<Material> maTempList = materialList.Where(m => m.Mcode == detail.Mcode).ToList();
                            if (maTempList.Count > 0)
                            {
                                detail.Dqmcode = maTempList[0].Dqmcode;
                            }

                            detail.MaintainUser = CommonConstants.MaintainUser;
                            detail.MaintainDate = intDate1;
                            detail.MaintainTime = intTime1;
                            detail.Cuser = CommonConstants.MaintainUser;
                            detail.Cdate = intDate1;
                            detail.Ctime = intTime1;
                            if (isdelUpdate)
                            {
                                _InvoicesFacade.UpdateInvoicesdetail(detail);
                            }
                            else
                            {
                                _InvoicesFacade.AddInvoicesdetail(detail);
                            }
                            #endregion
                        }
                        else
                        {
                            #region detail
                            _InvoicesFacade.UpdateInvoicesdetailStatusByDnNo(currDetail.Invlinestatus,
                                                                             currDetail.Invno,
                                                                             currDetail.Invline);
                            #endregion
                        }
                    }
                    #endregion

                    if (isPending)
                    {
                        #region pend

                        _InvoicesFacade.UpdateInvoicesStatusByInvNO(pono, UBHeadStatus.Pending, "W");
                        _InvoicesFacade.UpdateInvoicesDetailStatusByInvNO(pono, UBHeadStatus.Pending, "W");
                        #endregion
                    }
                    else
                    {
                        #region Updatehead

                        if (isAllCancel)
                        {
                            #region isAllCancel

                            _InvoicesFacade.UpdateInvoicesStatusByInvNO(pono, UBHeadStatus.Cancel, "W");

                            #endregion
                        }
                        else
                        {
                            #region isNotAllCancel

                            _InvoicesFacade.UpdateInvoicesStatusByInvNO(pono, UBHeadStatus.Release, "W");

                            #endregion
                        }

                        #endregion
                    }

                    _InvoicesFacade.UpdateSappoFlagByPONO(pono, MiddleTableFlag.Success);
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    _InvoicesFacade.UpdateSappoFlagByPONO(pono, MiddleTableFlag.Fail);
                    throw ex;
                }

            }
            #endregion

            #endregion

            #region 产生拣货任务令
            try
            {
                object[] arrInvoices = _InvoicesFacade.GetPOInvoices();
                List<Invoices> invoicesList = Common.Array2InvoicesList(arrInvoices);
                if (invoicesList == null)
                {
                    return;
                }
                var invnoList = invoicesList.GroupBy(p => p.Invno).Select(p => p.Key).ToList();
                this.DataProvider.BeginTransaction();
                foreach (var invno in invnoList)
                {
                    #region  Head
                    bool isUpdate = _InvoicesFacade.QueryPickCountByDNBatchNO(invno) > 0;
                    List<Invoices> currInvoicesList = invoicesList.Where(o => o.Invno == invno).ToList();
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                    Pick pick = null;
                    if (isUpdate)
                    {
                        pick = (Pick)_InvoicesFacade.GetPickByDNBatchNO(invno);
                    }
                    else
                    {
                        pick = new Pick();
                        pick.Pickno = _InvoicesFacade.GetPickNO();
                    }
                    pick.Picktype = currInvoicesList.First().Invtype;
                    pick.Invno = invno;
                    pick.Plangidate = currInvoicesList.OrderBy(o => o.Plangidate).First().Plangidate.ToString();
                    pick.Cusbatchno = currInvoicesList.First().Cusbatchno;
                    pick.Gfflag = currInvoicesList.First().Gfflag;
                    string orderstatus = currInvoicesList.First().Orderstatus;
                    pick.MaintainUser = CommonConstants.MaintainUser;
                    pick.MaintainDate = intDate;
                    pick.MaintainTime = intTime;

                    #region add by sam 2016年5月5日
                    //拣货任务令只汇总B层物料 STATUS not in ('Cancel')  
                    object[] arrInvoicesDetail = _InvoicesFacade.GetUBInvoicesDetailByUBNO(invno);
                    List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                    if (invoicesDetailList == null)
                    {
                        continue;
                    }
                    pick.Faccode = invoicesDetailList.First().Faccode;
                    pick.VenderCode = currInvoicesList.First().Vendorcode;
                    //pick.Receiveraddr = invoicesDetailList.First().Shipaddr;
                    foreach (Invoicesdetail invoicesdetail in invoicesDetailList)
                    {
                        if (!string.IsNullOrEmpty(invoicesdetail.Receiveraddr))
                        {
                            pick.Receiveraddr = invoicesdetail.Receiveraddr;
                        }
                        if (!string.IsNullOrEmpty(invoicesdetail.Receiveruser))
                        {
                            pick.Receiveruser = invoicesdetail.Receiveruser;
                        }
                    }
                    try
                    {
                        pick.Storagecode =
                            invoicesDetailList.First(o => string.IsNullOrEmpty(o.Storagecode) == false).Storagecode;
                    }
                    catch
                    {
                        pick.Storagecode = "";
                    }
                    #endregion


                    if (isUpdate)
                    {
                        //pick = (Pick)_InvoicesFacade.GetPickByDNBatchNO(invno);
                        pick.Status = "Pick";

                        _InvoicesFacade.UpdatePick(pick);
                    }
                    else
                    {
                        pick.Status = "Release";
                        pick.Cuser = CommonConstants.MaintainUser;
                        pick.Cdate = intDate;
                        pick.Ctime = intTime;
                        _InvoicesFacade.AddPick(pick);
                    }
                    string pickno = pick.Pickno;
                    _InvoicesFacade.UpdateOQCStatusByPickno("Cancel", pickno);
                    _InvoicesFacade.UpdateCartonInvoicesStatusByDnBatchNo("Release", invno);//CartonInvoicesDetail
                    _InvoicesFacade.UpdateCartonInvoicesDetailStatusByInvno("Pack", pickno);//CartonInvoicesDetail





                    #endregion

                    #region  line

                    int intDate1, intTime1;
                    Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                    List<MaterialSum> msList =
                        invoicesDetailList.GroupBy(d => d.Mcode,
                                                   (key, group) =>
                                                   new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) })
                                          .ToList();

                    foreach (var ms in msList)
                    {

                        Pickdetail pickdetail = (Pickdetail)_InvoicesFacade.GetPickdetail(pick.Pickno, ms.MCode);
                        Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);//not Cancel
                        bool isUpdateLine = pickdetail != null;
                        if (!isUpdateLine)
                        {
                            pickdetail = new Pickdetail
                            {
                                Pickno = pick.Pickno,
                                Pickline = _InvoicesFacade.GetMaxPickLine(pick.Pickno),//新行号
                                Status = "Pick"
                            };
                        }
                        else
                        {
                            //更新
                            if (pickdetail.Status != "Cancel")
                            {
                                pickdetail.Status = "Pick";
                            }
                            else
                            {
                                pickdetail.Status = firstInvDe.Status;
                            }
                        }
                        pickdetail.Pickno = pick.Pickno;
                        pickdetail.Mcode = ms.MCode;
                        pickdetail.Dqmcode = firstInvDe.Dqmcode;
                        pickdetail.Qty = ms.Qty;
                        pickdetail.Unit = firstInvDe.Unit;
                        //确认是否是这样获取
                        pickdetail.Cusorderno = currInvoicesList.First().Cusorderno;
                        pickdetail.Cusordernotype = currInvoicesList.First().Cusordernotype;
                        pickdetail.Cusitemcode = firstInvDe.Cusmcode;
                        pickdetail.Cusitemspec = firstInvDe.Cusitemspec;
                        pickdetail.Cusitemdesc = firstInvDe.Cusitemdesc;
                        pickdetail.Venderitemcode = firstInvDe.Vendermcode;
                        pickdetail.Gfcontractno = currInvoicesList.First().Gfcontractno;
                        //光伏合同号 Header your reference (SAP，光伏特有)(DN)
                        pickdetail.Gfhwitemcode = firstInvDe.Gfhwmcode;
                        pickdetail.Gfpackingseq = firstInvDe.Gfpackingseq;
                        pickdetail.Postway = currInvoicesList.First().Postway;
                        pickdetail.Pickcondition = currInvoicesList.First().Pickcondition;
                        pickdetail.Hwcodeqty = firstInvDe.Hwcodeqty.ToString();
                        pickdetail.Hwtypeinfo = firstInvDe.Hwtypeinfo;
                        pickdetail.Packingway = firstInvDe.Packingway;
                        pickdetail.Packingno = firstInvDe.Packingno;
                        pickdetail.Packingspec = firstInvDe.Packingspec;
                        pickdetail.Packingwayno = firstInvDe.Packingwayno;
                        pickdetail.Dqsitemcode = firstInvDe.Dqsmcode;
                        pickdetail.Detailremark = firstInvDe.Detailremark;

                        pickdetail.MaintainUser = CommonConstants.MaintainUser;
                        pickdetail.MaintainDate = intDate1;
                        pickdetail.MaintainTime = intTime1;
                        pickdetail.Cuser = CommonConstants.MaintainUser;
                        pickdetail.Cdate = intDate1;
                        pickdetail.Ctime = intTime1;

                        if (!isUpdateLine)
                        {
                            _InvoicesFacade.AddPickdetail(pickdetail);
                        }
                        else
                        {
                            _InvoicesFacade.UpdatePickdetail(pickdetail);
                        }


                    }

                    #endregion

                    #region Cancel
                    if (orderstatus == POHeadStatus.Pending)
                    {
                        _InvoicesFacade.UpdatePickStatusByPickNo("Pending", pickno);
                        _InvoicesFacade.UpdatePickDetailStatusByPickNo("Pending", pickno);
                        _InvoicesFacade.UpdateOQCStatusByPickno("Pending", pickno);
                        _InvoicesFacade.UpdateCartonInvoicesStatusByDnBatchNo("Pending", invno);//CartonInvoicesDetail
                        _InvoicesFacade.UpdateCartonInvoicesDetailStatusByInvno("Pending", pickno);//CartonInvoicesDetail
                    }
                    if (orderstatus == POHeadStatus.Cancel)
                    {
                        _InvoicesFacade.UpdatePickStatusByPickNo("Cancel", pickno);
                        _InvoicesFacade.UpdatePickDetailStatusByPickNo("Cancel", pickno);
                        _InvoicesFacade.UpdatePickStatusByPickNo("Cancel", pickno);
                        _InvoicesFacade.UpdatePickDetailStatusByPickNo("Cancel", pickno);
                    }

                    #endregion
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            #endregion
        }

        private void SyncDN()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.DN;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();

                string strDate = "";
                string strTime = "";
                if (!Common.TryGetStampDbDateTime(ref strDate, ref strTime, SAPJOBTIMESTAMP.DN))
                {
                    Log.Error("没有维护DN的时间戳参数");
                    return;
                }
                importParameters.Add("I_DATUM", strDate);
                importParameters.Add("I_UZEIT", strTime);
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveNewDNData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveNewDNData(DataSet ds)
        {

            //将之前JOB未处理的记录状态置为"F"
            _InvoicesFacade.UpdateSapDnFlag(MiddleTableFlag.Last);
            _InvoicesFacade.UpdateInvoicesFlag(MiddleTableFlag.Last, " ('DNC','DNR','DNZC') ");
            #region Insert I_Sapdn then commit
            if (ds != null && ds.Tables[SAPRfcOutTableName.DN] != null &&
                ds.Tables[SAPRfcOutTableName.DN].Rows.Count > 0)
            {
                try
                {
                    this.DataProvider.BeginTransaction();
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                    foreach (DataRow row in ds.Tables[SAPRfcOutTableName.DN].Rows)
                    {
                        I_Sapdnbatch idn = new I_Sapdnbatch();
                        idn.NotOutCheckFlag = Convert.ToString(row["OUT_CHK"]);
                        idn.Detailremark = Convert.ToString(row["SO_ITEXT"]);
                        //add 2016年7月14日
                        idn.Invno = Convert.ToString(row["VBELN"]);
                        idn.Dnbatchno = Convert.ToString(row["PCBH"]);
                        string strDNBatchStatus = Convert.ToString(row["WBSTK"]);
                        switch (strDNBatchStatus)
                        {
                            case "C":
                                strDNBatchStatus = "Release";
                                break;
                            case "B":
                                strDNBatchStatus = "Cancel";
                                break;
                            default:
                                strDNBatchStatus = "Pending";
                                break;
                        }
                        idn.Dnbatchstatus = strDNBatchStatus;// Convert.ToString(row["WBSTK"]);
                        idn.Invtype = Convert.ToString(row["VBTYP"]);
                        idn.Shiptoparty = Convert.ToString(row["KUNNR"]);
                        idn.Orderno = Convert.ToString(row["BSTKD"]);
                        idn.Cusorderno = Convert.ToString(row["VBELV"]);
                        idn.Cusordernotype = Convert.ToString(row["AUART"]);
                        idn.Dnmuser = Convert.ToString(row["AENAM"]);
                        idn.Dnmdate = Common.ParseInt(row["AEDAT"]);
                        idn.Dnmtime = Common.ParseInt(row["AEZET"]);
                        idn.Cusbatchno = Convert.ToString(row["KHPCH"]);
                        idn.Shippinglocation = Convert.ToString(row["SHIPLOC"]);
                        idn.Plangidate = Common.ParseInt(row["WADAT"]);
                        //idn.Gfcontractno = Convert.ToString(row["IHREZ_PV"]);
                        idn.Orderreason = Convert.ToString(row["AUGRU"]);
                        idn.Postway = Convert.ToString(row["JFFS"]);
                        idn.Pickcondition = Convert.ToString(row["JLTJ"]);
                        idn.Gfflag = Convert.ToString(row["DN_YJGF"]);

                        idn.Invline = Common.ParseInt(row["POSNR"]);
                        idn.Hignlevelitem = Common.ParseInt(row["UEPOS"]);
                        idn.Mcode = Convert.ToString(row["MATNR"]);
                        idn.Faccode = Convert.ToString(row["WERKS"]);
                        idn.Storagecode = Convert.ToString(row["LGORT"]);
                        idn.Planqty = Convert.ToDecimal(row["LFIMG"]);
                        idn.Unit = Convert.ToString(row["VRKME"]);
                        idn.Movementtype = Convert.ToString(row["BWART"]);
                        idn.Cusmcode = Convert.ToString(row["EMPST"]);
                        idn.Cusitemspec = Convert.ToString(row["CMTART"]);
                        idn.Cusitemdesc = Convert.ToString(row["CMAKTX"]);
                        idn.Vendermcode = Convert.ToString(row["IDNLF"]);
                        idn.Gfhwmcode = Convert.ToString(row["EMPST_H"]);
                        idn.Gfpackingseq = Convert.ToString(row["POSEX"]);
                        idn.Gfhwdesc = Convert.ToString(row["CONTENT"]);
                        idn.Hwcodeqty = Convert.ToDecimal(row["B1_QTY"]);
                        idn.Hwcodeunit = Convert.ToString(row["UNITQTY"]);
                        idn.Hwtypeinfo = Convert.ToString(row["PARTID"]);
                        idn.Packingway = Convert.ToString(row["PAITEMTYPE"]);
                        idn.Packingno = Convert.ToString(row["BOX_POBJID"]);
                        idn.Packingspec = Convert.ToString(row["BOX_SIZE"]);
                        idn.Packingwayno = Convert.ToString(row["BOX_MTHOD"]);
                        idn.Dqsmcode = Convert.ToString(row["ZEINR"]);
                        idn.Mesflag = "W";
                        idn.Sdate = intDate;
                        idn.Stime = intTime;
                        idn.Pdate = intDate;
                        idn.Ptime = intTime;
                        _InvoicesFacade.AddSapdnbatch(idn);
                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
            #endregion


            #region merge dn into invoice table, commit by dn
            object[] arr = _InvoicesFacade.GetSapDnByFlag(MiddleTableFlag.Wait);
            //没有待处理的记录，退出
            if (arr == null)
            {
                return;
            }
            List<I_Sapdnbatch> sapdnList = Common.Array2SapdnList(arr);
            //var dnnoList = sapdnList.GroupBy(p => p.Invno).Select(p => p.Key).ToList();
            try
            {
                this.DataProvider.BeginTransaction();

                #region 按 Dnbatchno
                var batchnoList = sapdnList.GroupBy(p => p.Dnbatchno).Select(p => p.Key).ToList();
                foreach (string batchno in batchnoList)
                {
                    List<I_Sapdnbatch> currbatchList = sapdnList.Where(p => p.Dnbatchno == batchno).OrderByDescending(p => p.Sdate).ThenByDescending(p => p.Stime).ToList();
                    string currbatchListStatus = currbatchList.First().Dnbatchstatus;
                    List<I_Sapdnbatch> currRList = currbatchList.Where(p => p.Dnbatchstatus == "Release").ToList();
                    if (currRList.Count > 0)
                    {
                        currbatchListStatus = "Release";
                    }

                    var dnnoList = currbatchList.GroupBy(p => p.Invno).Select(p => p.Key).ToList();

                    #region Release
                    if (currbatchListStatus == "Release")
                    {
                        _InvoicesFacade.DeleteInvoicesdetailByDNBatchNo(batchno);
                        _InvoicesFacade.DeleteInvoicesByDNBatchNo(batchno);
                        foreach (var dnno in dnnoList)
                        {
                            List<I_Sapdnbatch> currList = currbatchList.Where(p => p.Invno == dnno).OrderByDescending(p => p.Sdate).ThenByDescending(p => p.Stime).ToList();
                            string currHeadStatus = currList.First().Dnbatchstatus;
                            if (currHeadStatus == "Release")
                            {
                                //_InvoicesFacade.DeleteInvoices(dnno);
                                //_InvoicesFacade.DeleteInvoicesdetail(dnno);

                                #region add

                                int intDate, intTime;
                                Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                                //_InvoicesFacade.DeleteInvoices(dnno);
                                //_InvoicesFacade.DeleteInvoicesdetail(dnno);
                                Invoices item = new Invoices();
                                item.NotOutCheckFlag = currList.First().NotOutCheckFlag;//add by sam 2016年7月14日
                                item.Invno = dnno;
                                item.Dnbatchno = currList.First().Dnbatchno;
                                item.Dnbatchstatus = currList.First().Dnbatchstatus;

                                string strInvtype = currList.First().Invtype;
                                if (strInvtype == "J")
                                {
                                    strInvtype = "DNC"; //发货
                                }
                                else if (strInvtype == "T")
                                {
                                    strInvtype = "DNR"; //退货
                                }
                                if (currList.First().Postway == "物料直发")
                                {
                                    strInvtype = "DNZC"; //供应商直发DN
                                }
                                item.Invtype = strInvtype;

                                item.Invstatus = "Release"; // currList.First().Invstatus;
                                item.Shiptoparty = currList.First().Shiptoparty;
                                item.Orderno = currList.First().Orderno;
                                item.Cusorderno = currList.First().Cusorderno;
                                item.Cusordernotype = currList.First().Cusordernotype;
                                item.Dnmuser = currList.First().Dnmuser;
                                item.Dnmdate = currList.First().Dnmdate;
                                item.Dnmtime = currList.First().Dnmtime;
                                item.Cusbatchno = currList.First().Cusbatchno;
                                item.Shippinglocation = currList.First().Shippinglocation;
                                item.Plangidate = currList.First().Plangidate;
                                //item.Gfcontractno = currList.First().Gfcontractno;
                                item.Orderreason = currList.First().Orderreason;
                                item.Postway = currList.First().Postway;
                                item.Pickcondition = currList.First().Pickcondition;
                                item.Gfflag = currList.First().Gfflag;

                                item.Finishflag = "N";
                                item.MaintainUser = "JOB";
                                item.MaintainDate = intDate;
                                item.MaintainTime = intTime;
                                item.Cuser = "JOB";
                                item.Cdate = intDate;
                                item.Ctime = intTime;
                                item.Eattribute1 = "W";
                                _InvoicesFacade.AddInvoices(item);

                                #endregion


                                #region line



                                List<Material> materialList = this.GetAllMaterialList();
                                int intDate1, intTime1;
                                Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                                foreach (var currDetail in currList)
                                {
                                    #region intInvLine
                                    int intInvLine;
                                    if (currDetail.Invline == 0)
                                    {
                                        //光伏硬件，DN没有行号，接口自动赋行号
                                        if (currDetail.Gfflag == "X")
                                        {
                                            intInvLine = _InvoicesFacade.GetMaxInvoicesLine(currDetail.Invno);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        intInvLine = currDetail.Invline;
                                    }
                                    #endregion

                                    Invoicesdetail detail = new Invoicesdetail();
                                    detail.Invno = currDetail.Invno;
                                    detail.Invline = intInvLine;// currDetail.Invline;
                                    detail.Detailremark = currDetail.Detailremark;
                                    //detail.LineRemark = currDetail.LineRemark;//add by sam 2016年7月14日
                                    detail.Status = currDetail.Dnbatchstatus;
                                    detail.Invlinestatus = currDetail.Dnbatchstatus;
                                    detail.Hignlevelitem = currDetail.Hignlevelitem; //有的记录是空值
                                    detail.Mcode = currDetail.Mcode;
                                    detail.Faccode = currDetail.Faccode;
                                    detail.Storagecode = currDetail.Storagecode;
                                    detail.Planqty = currDetail.Planqty;
                                    detail.Unit = currDetail.Unit;
                                    detail.Movementtype = currDetail.Movementtype;
                                    detail.Cusmcode = currDetail.Cusmcode;
                                    detail.Cusitemspec = currDetail.Cusitemspec;
                                    detail.Cusitemdesc = currDetail.Cusitemdesc;
                                    detail.Vendermcode = currDetail.Vendermcode;
                                    detail.Gfhwmcode = currDetail.Gfhwmcode;
                                    detail.Gfpackingseq = currDetail.Gfpackingseq;
                                    detail.Gfhwdesc = currDetail.Gfhwdesc;
                                    detail.Hwcodeqty = currDetail.Hwcodeqty;
                                    detail.Hwcodeunit = currDetail.Hwcodeunit;
                                    detail.Hwtypeinfo = currDetail.Hwtypeinfo;
                                    detail.Packingway = currDetail.Packingway;
                                    detail.Packingno = currDetail.Packingno;
                                    detail.Packingspec = currDetail.Packingspec;
                                    detail.Packingwayno = currDetail.Packingwayno;
                                    detail.Dqsmcode = currDetail.Dqsmcode;
                                    List<Material> maTempList = materialList.Where(m => m.Mcode == detail.Mcode).ToList();
                                    if (maTempList.Count > 0)
                                    {
                                        detail.Dqmcode = maTempList[0].Dqmcode;
                                    }
                                    detail.MaintainUser = "JOB";
                                    detail.MaintainDate = intDate1;
                                    detail.MaintainTime = intTime1;
                                    detail.Cuser = "JOB";
                                    detail.Cdate = intDate1;
                                    detail.Ctime = intTime1;
                                    _InvoicesFacade.AddInvoicesdetail(detail);
                                }

                                #endregion

                            }
                        }
                        _InvoicesFacade.UpdateCartonInvoicesStatusByDnBatchNo("Release", batchno);
                    }
                    #endregion

                    #region "Pending"
                    else if (currbatchListStatus == "Pending")
                    {
                        //将tblInvoices表DNBATCHSTATUS更新为“Pending”，
                        //DN批对应的拣货任务令Head状态更新为Pending，此时MES不允许创建发货箱单，OQC和出库动作。

                        _InvoicesFacade.UpdatePickStatusByDnBatchNo("Pending", batchno);
                        _InvoicesFacade.UpdatePickDetailStatusByDnBatchNo("Pending", batchno);
                        _InvoicesFacade.UpdateCartonInvoicesStatusByDnBatchNo("Pending", batchno);
                        _InvoicesFacade.UpdateInvoicesStatusByDnBatchNo("Pending", batchno);
                        _InvoicesFacade.UpdateInvoicesdetailStatusByDnBatchNo("Pending", batchno);

                    }
                    #endregion

                    #region "Cancel"
                    else if (currbatchListStatus == "Cancel")
                    {
                        #region "Cancel"
                        object[] invoiceslist = _InvoicesFacade.GetInvoicesBybatchno(batchno);
                        if (invoiceslist != null)
                        {
                            foreach (Invoices invoice in invoiceslist)
                            {
                                #region backup
                                #region DNBatchBak
                                DNBatchBak dnBatchBak = new DNBatchBak();
                                dnBatchBak.Invno = invoice.Invno;
                                dnBatchBak.Dnbatchno = invoice.Dnbatchno;
                                dnBatchBak.Dnbatchstatus = invoice.Dnbatchstatus;
                                dnBatchBak.Invtype = invoice.Invtype;
                                dnBatchBak.Invstatus = "Cancel"; // invoice.Invstatus;
                                dnBatchBak.Shiptoparty = invoice.Shiptoparty;
                                dnBatchBak.Orderno = invoice.Orderno;
                                dnBatchBak.Cusorderno = invoice.Cusorderno;
                                dnBatchBak.Cusordernotype = invoice.Cusordernotype;
                                dnBatchBak.Dnmuser = invoice.Dnmuser;
                                dnBatchBak.Dnmdate = invoice.Dnmdate;
                                dnBatchBak.Dnmtime = invoice.Dnmtime;
                                dnBatchBak.Cusbatchno = invoice.Cusbatchno;
                                dnBatchBak.Shippinglocation = invoice.Shippinglocation;
                                dnBatchBak.Plangidate = invoice.Plangidate;
                                dnBatchBak.Gfcontractno = invoice.Gfcontractno;
                                dnBatchBak.Orderreason = invoice.Orderreason;
                                dnBatchBak.Postway = invoice.Postway;
                                dnBatchBak.Pickcondition = invoice.Pickcondition;
                                dnBatchBak.Gfflag = invoice.Gfflag;

                                dnBatchBak.Finishflag = invoice.Finishflag;
                                dnBatchBak.MaintainUser = invoice.MaintainUser;
                                dnBatchBak.MaintainDate = invoice.MaintainDate;
                                dnBatchBak.MaintainTime = invoice.MaintainTime;
                                dnBatchBak.Cuser = invoice.Cuser;
                                dnBatchBak.Cdate = invoice.Cdate;
                                dnBatchBak.Ctime = invoice.Ctime;
                                _InvoicesFacade.AddDNBatchBak(dnBatchBak);
                                #endregion
                                #endregion
                            }
                            object[] currList = _InvoicesFacade.GetDNInvoicesDetailBybatchno(batchno);
                            #region DNBatchDetailBak
                            foreach (Invoicesdetail detail in currList)
                            {
                                DNBatchDetailBak dnBatchDetailBak = new DNBatchDetailBak();
                                dnBatchDetailBak.Invno = detail.Invno;
                                dnBatchDetailBak.Invline = detail.Invline;
                                dnBatchDetailBak.Hignlevelitem = detail.Hignlevelitem; //有的记录是空值
                                dnBatchDetailBak.Mcode = detail.Mcode;
                                dnBatchDetailBak.Faccode = detail.Faccode;
                                dnBatchDetailBak.Storagecode = detail.Storagecode;
                                dnBatchDetailBak.Planqty = detail.Planqty;
                                dnBatchDetailBak.Unit = detail.Unit;
                                dnBatchDetailBak.Movementtype = detail.Movementtype;
                                dnBatchDetailBak.Cusmcode = detail.Cusmcode;
                                dnBatchDetailBak.Cusitemspec = detail.Cusitemspec;
                                dnBatchDetailBak.Cusitemdesc = detail.Cusitemdesc;
                                dnBatchDetailBak.Vendermcode = detail.Vendermcode;
                                dnBatchDetailBak.Gfhwmcode = detail.Gfhwmcode;
                                dnBatchDetailBak.Gfpackingseq = detail.Gfpackingseq;
                                dnBatchDetailBak.Gfhwdesc = detail.Gfhwdesc;
                                dnBatchDetailBak.Hwcodeqty = detail.Hwcodeqty;
                                dnBatchDetailBak.Hwcodeunit = detail.Hwcodeunit;
                                dnBatchDetailBak.Hwtypeinfo = detail.Hwtypeinfo;
                                dnBatchDetailBak.Packingway = detail.Packingway;
                                dnBatchDetailBak.Packingno = detail.Packingno;
                                dnBatchDetailBak.Packingspec = detail.Packingspec;
                                dnBatchDetailBak.Packingwayno = detail.Packingwayno;
                                dnBatchDetailBak.Dqsmcode = detail.Dqsmcode;
                                dnBatchDetailBak.Dqmcode = detail.Dqmcode;
                                dnBatchDetailBak.MaintainUser = detail.MaintainUser;
                                dnBatchDetailBak.MaintainDate = detail.MaintainDate;
                                dnBatchDetailBak.MaintainTime = detail.MaintainTime;
                                dnBatchDetailBak.Cuser = detail.Cuser;
                                dnBatchDetailBak.Cdate = detail.Cdate;
                                dnBatchDetailBak.Ctime = detail.Ctime;
                                _InvoicesFacade.AddDNBatchDetailBak(dnBatchDetailBak);

                            }
                            #endregion

                            _InvoicesFacade.DeleteInvoicesdetailByDNBatchNo(batchno);
                            _InvoicesFacade.DeleteInvoicesByDNBatchNo(batchno);
                            //_InvoicesFacade.DeleteInvoicesdetail(detail);
                            //_InvoicesFacade.DeleteInvoices(invoice);
                        }
                        else
                        {

                            dnnoList =
                                sapdnList.Where(p => p.Dnbatchno == batchno)
                                         .GroupBy(p => p.Invno)
                                         .Select(p => p.Key)
                                         .ToList();
                            foreach (var dnno in dnnoList)
                            {
                                List<I_Sapdnbatch> currList =
                                    sapdnList.Where(p => p.Invno == dnno)
                                             .OrderByDescending(p => p.Sdate)
                                             .ThenByDescending(p => p.Stime)
                                             .ToList();

                                #region add dnBatchBak

                                int intDate, intTime;
                                Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                                DNBatchBak dnBatchBak = new DNBatchBak();
                                dnBatchBak.Invno = dnno;
                                dnBatchBak.Dnbatchno = currList.First().Dnbatchno;
                                dnBatchBak.Dnbatchstatus = currList.First().Dnbatchstatus;
                                dnBatchBak.Invtype = currList.First().Invtype;
                                dnBatchBak.Invstatus = "Cancel"; // currList.First().Invstatus;
                                dnBatchBak.Shiptoparty = currList.First().Shiptoparty;
                                dnBatchBak.Orderno = currList.First().Orderno;
                                dnBatchBak.Cusorderno = currList.First().Cusorderno;
                                dnBatchBak.Cusordernotype = currList.First().Cusordernotype;
                                dnBatchBak.Dnmuser = currList.First().Dnmuser;
                                dnBatchBak.Dnmdate = currList.First().Dnmdate;
                                dnBatchBak.Dnmtime = currList.First().Dnmtime;
                                dnBatchBak.Cusbatchno = currList.First().Cusbatchno;
                                dnBatchBak.Shippinglocation = currList.First().Shippinglocation;
                                dnBatchBak.Plangidate = currList.First().Plangidate;
                                dnBatchBak.Gfcontractno = currList.First().Gfcontractno;
                                dnBatchBak.Orderreason = currList.First().Orderreason;
                                dnBatchBak.Postway = currList.First().Postway;
                                dnBatchBak.Pickcondition = currList.First().Pickcondition;
                                dnBatchBak.Gfflag = currList.First().Gfflag;

                                dnBatchBak.Finishflag = "N";
                                dnBatchBak.MaintainUser = "JOB";
                                dnBatchBak.MaintainDate = intDate;
                                dnBatchBak.MaintainTime = intTime;
                                dnBatchBak.Cuser = "JOB";
                                dnBatchBak.Cdate = intDate;
                                dnBatchBak.Ctime = intTime;
                                _InvoicesFacade.AddDNBatchBak(dnBatchBak);

                                #endregion

                                #region line

                                List<Material> materialList = this.GetAllMaterialList();
                                int intDate1, intTime1;
                                Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                                foreach (var currDetail in currList)
                                {

                                    DNBatchDetailBak dnBatchDetailBak = new DNBatchDetailBak();
                                    dnBatchDetailBak.Invno = currDetail.Invno;
                                    dnBatchDetailBak.Invline = currDetail.Invline;
                                    dnBatchDetailBak.Hignlevelitem = currDetail.Hignlevelitem; //有的记录是空值
                                    dnBatchDetailBak.Mcode = currDetail.Mcode;
                                    dnBatchDetailBak.Faccode = currDetail.Faccode;
                                    dnBatchDetailBak.Storagecode = currDetail.Storagecode;
                                    dnBatchDetailBak.Planqty = currDetail.Planqty;
                                    dnBatchDetailBak.Unit = currDetail.Unit;
                                    dnBatchDetailBak.Movementtype = currDetail.Movementtype;
                                    dnBatchDetailBak.Cusmcode = currDetail.Cusmcode;
                                    dnBatchDetailBak.Cusitemspec = currDetail.Cusitemspec;
                                    dnBatchDetailBak.Cusitemdesc = currDetail.Cusitemdesc;
                                    dnBatchDetailBak.Vendermcode = currDetail.Vendermcode;
                                    dnBatchDetailBak.Gfhwmcode = currDetail.Gfhwmcode;
                                    dnBatchDetailBak.Gfpackingseq = currDetail.Gfpackingseq;
                                    dnBatchDetailBak.Gfhwdesc = currDetail.Gfhwdesc;
                                    dnBatchDetailBak.Hwcodeqty = currDetail.Hwcodeqty;
                                    dnBatchDetailBak.Hwcodeunit = currDetail.Hwcodeunit;
                                    dnBatchDetailBak.Hwtypeinfo = currDetail.Hwtypeinfo;
                                    dnBatchDetailBak.Packingway = currDetail.Packingway;
                                    dnBatchDetailBak.Packingno = currDetail.Packingno;
                                    dnBatchDetailBak.Packingspec = currDetail.Packingspec;
                                    dnBatchDetailBak.Packingwayno = currDetail.Packingwayno;
                                    dnBatchDetailBak.Dqsmcode = currDetail.Dqsmcode;
                                    List<Material> maTempList =
                                        materialList.Where(m => m.Mcode == dnBatchDetailBak.Mcode).ToList();
                                    if (maTempList.Count > 0)
                                    {
                                        dnBatchDetailBak.Dqmcode = maTempList[0].Dqmcode;
                                    }
                                    dnBatchDetailBak.MaintainUser = "JOB";
                                    dnBatchDetailBak.MaintainDate = intDate1;
                                    dnBatchDetailBak.MaintainTime = intTime1;
                                    dnBatchDetailBak.Cuser = "JOB";
                                    dnBatchDetailBak.Cdate = intDate1;
                                    dnBatchDetailBak.Ctime = intTime1;
                                    _InvoicesFacade.AddDNBatchDetailBak(dnBatchDetailBak);
                                }

                                #endregion
                            }
                        }
                        _InvoicesFacade.UpdateOQCStatusByBatchno("Cancel", batchno);
                        _InvoicesFacade.UpdatePickStatusByDnBatchNo("Cancel", batchno);
                        _InvoicesFacade.UpdatePickDetailStatusByDnBatchNo("Cancel", batchno);
                        _InvoicesFacade.UpdateCartonInvoicesStatusByDnBatchNo("Cancel", batchno);
                        #endregion
                    }

                    #endregion

                    #region add by sam delete
                    //string dnnolist = "";
                    //if (dnnoList.Count>0)
                    //{
                    //    foreach (string dnno in dnnoList)
                    //    {

                    //        dnnolist += "'" + dnno + "',";
                    //    }
                    //    dnnolist = dnnolist.Substring(0, dnnolist.Length - 1);
                    //    _InvoicesFacade.DeleteInvoicesByDNBatchNo(batchno, dnnolist);
                    //    _InvoicesFacade.DeleteInvoicesdetailByDNBatchNo(batchno, dnnolist);
                    //} 
                    #endregion

                }
                #endregion

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }


            #endregion


            #region 产生拣货任务令

            try
            {
                object[] arrInvoices = _InvoicesFacade.GetDNInvoices();
                List<Invoices> invoicesList = Common.Array2InvoicesList(arrInvoices);
                if (invoicesList == null)
                {
                    return;
                }
                var batchnoList = invoicesList.OrderBy(p => p.Dnbatchno).GroupBy(p => p.Dnbatchno).Select(p => p.Key).ToList();
                //string tmpPickno = default(string);

                this.DataProvider.BeginTransaction();
                foreach (var batchno in batchnoList)
                {

                    List<Invoices> currInvoicesList = invoicesList.Where(o => o.Dnbatchno == batchno).ToList();
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                    #region invoicesDetailList
                    object[] arrInvoicesDetail = _InvoicesFacade.GetDNInvoicesDetailByDNBatchNO(batchno);
                    if (arrInvoicesDetail == null)
                    {
                        continue;
                    }
                    List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                    #endregion

                    //已经产生拣货任务令的发货批，不再处理
                    string status = currInvoicesList.First().Invstatus;
                    #region add by sam
                    if (_InvoicesFacade.QueryPickCountByDNBatchNO(batchno) > 0)
                    {
                        #region
                        if (currInvoicesList.First().Invstatus == "Release")
                        {
                            _InvoicesFacade.UpdateCartonInvoicesStatusByDnBatchNo("Release", batchno);

                            Pick oldpick = (Pick)_InvoicesFacade.GetPickByDNBatchNO(batchno);
                            if (oldpick != null)
                            {
                                #region Pickdetail
                                List<MaterialSum> newmsList = invoicesDetailList.GroupBy(d => d.Mcode, (key, group) => new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) }).ToList();
                                if (!(oldpick.Status == PickHeadStatus.PickHeadStatus_Close
                                    || oldpick.Status == PickHeadStatus.PickHeadStatus_Cancel))
                                {
                                    #region invoicesDetail中的

                                    foreach (var ms in newmsList)
                                    {
                                        Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);
                                        Pickdetail pickdetail =
                                            (Pickdetail)_InvoicesFacade.GetPickdetail(oldpick.Pickno, ms.MCode);
                                        bool isUpdateLine = pickdetail != null;
                                        if (!isUpdateLine)
                                        {
                                            pickdetail = new Pickdetail
                                                {
                                                    Pickno = oldpick.Pickno,
                                                    Pickline = _InvoicesFacade.GetMaxPickLine(oldpick.Pickno),
                                                    Status = "Pick"
                                                };
                                            if (oldpick.Status == PickHeadStatus.PickHeadStatus_Release)
                                            {
                                                pickdetail.Status = PickHeadStatus.PickHeadStatus_Release;
                                            }
                                        }
                                        else
                                        {
                                            //更新
                                            if (pickdetail.Status == PickHeadStatus.PickHeadStatus_Release)
                                            {
                                                pickdetail.Status = PickHeadStatus.PickHeadStatus_Release;
                                            }
                                            else if (pickdetail.Status == "Cancel")
                                            {
                                                pickdetail.Status = firstInvDe.Status;
                                            }
                                            else
                                            {
                                                pickdetail.Status = "Pick";
                                            }
                                        }

                                        pickdetail.Mcode = ms.MCode;
                                        pickdetail.Dqmcode = firstInvDe.Dqmcode;
                                        pickdetail.Qty = ms.Qty;
                                        pickdetail.Unit = firstInvDe.Unit;
                                        //确认是否是这样获取
                                        pickdetail.Cusorderno = currInvoicesList.First().Cusorderno;
                                        pickdetail.Cusordernotype = currInvoicesList.First().Cusordernotype;
                                        pickdetail.Cusitemcode = firstInvDe.Cusmcode;
                                        pickdetail.Cusitemspec = firstInvDe.Cusitemspec;
                                        pickdetail.Cusitemdesc = firstInvDe.Cusitemdesc;
                                        pickdetail.Venderitemcode = firstInvDe.Vendermcode;
                                        pickdetail.Gfcontractno = currInvoicesList.First().Gfcontractno;
                                        //光伏合同号 Header your reference (SAP，光伏特有)(DN)
                                        pickdetail.Gfhwitemcode = firstInvDe.Gfhwmcode;
                                        pickdetail.Gfpackingseq = firstInvDe.Gfpackingseq;
                                        pickdetail.Postway = currInvoicesList.First().Postway;
                                        pickdetail.Pickcondition = currInvoicesList.First().Pickcondition;
                                        pickdetail.Hwcodeqty = firstInvDe.Hwcodeqty.ToString();
                                        pickdetail.Hwtypeinfo = firstInvDe.Hwtypeinfo;
                                        pickdetail.Packingway = firstInvDe.Packingway;
                                        pickdetail.Packingno = firstInvDe.Packingno;
                                        pickdetail.Packingspec = firstInvDe.Packingspec;
                                        pickdetail.Packingwayno = firstInvDe.Packingwayno;
                                        pickdetail.Dqsitemcode = firstInvDe.Dqsmcode;
                                        pickdetail.Detailremark = firstInvDe.Detailremark;

                                        pickdetail.MaintainUser = CommonConstants.MaintainUser;
                                        pickdetail.MaintainDate = intDate;
                                        pickdetail.MaintainTime = intTime;
                                        pickdetail.Cuser = CommonConstants.MaintainUser;
                                        pickdetail.Cdate = intDate;
                                        pickdetail.Ctime = intTime;
                                        if (!isUpdateLine)
                                        {
                                            _InvoicesFacade.AddPickdetail(pickdetail);
                                        }
                                        else
                                        {
                                            _InvoicesFacade.UpdatePickdetail(pickdetail);
                                        }
                                    }

                                    #endregion
                                }

                                #region 区分 invoicesDetailList中没有的 update Cancel
                                List<string> mList = invoicesDetailList.GroupBy(d => d.Mcode).Select(p => p.Key).ToList();// tblinvoicesDetail
                                object[] arrPickDetail = _InvoicesFacade.GetPickDetailByDNBatchNO(batchno);
                                List<Pickdetail> pickDetailList = Common.Array2PickdetailList(arrPickDetail);
                                List<string> mpickList = pickDetailList.GroupBy(d => d.Mcode).Select(p => p.Key).ToList();//tblPickdetail
                                List<string> moldpickList = mpickList.Except(mList).ToList();//取差集() tblPickdetail有的，tblinvoicesDetail没有的
                                foreach (string mcode in moldpickList)
                                {
                                    Pickdetail pickdetail = (Pickdetail)_InvoicesFacade.GetPickdetail(oldpick.Pickno, mcode);
                                    if (pickdetail != null)
                                    {
                                        pickdetail.Status = "Cancel";
                                        _InvoicesFacade.UpdatePickdetail(pickdetail);
                                    }
                                }
                                #endregion


                                #endregion

                                if (oldpick.Status == PickHeadStatus.PickHeadStatus_Close
                                    || oldpick.Status == PickHeadStatus.PickHeadStatus_Cancel)
                                {
                                    continue;
                                }

                                _InvoicesFacade.UpdateOQCStatusByPickno("Cancel", oldpick.Pickno);

                                #region update  oldpick
                                oldpick.Picktype = currInvoicesList.First().Invtype;
                                if (oldpick.Status != PickHeadStatus.PickHeadStatus_Release)
                                {
                                    oldpick.Status = "Pick";
                                }
                                oldpick.Invno = batchno;
                                //oldpick.Faccode = ""; //在DN行上
                                //oldpick.Storagecode = ""; //在DN行上
                                oldpick.Plangidate = currInvoicesList.OrderBy(o => o.Plangidate).First().Plangidate.ToString();
                                oldpick.Cusbatchno = currInvoicesList.First().Cusbatchno;
                                //oldpick.Gfflag = currInvoicesList.First().Gfflag;

                                bool isGf = currInvoicesList.GroupBy(p => p.Gfflag).Select(p => p.Key).ToList().Contains("X"); //.First().Gfflag;
                                if (isGf)
                                {
                                    oldpick.Gfflag = "X";
                                }
                                oldpick.MaintainUser = CommonConstants.MaintainUser;
                                oldpick.MaintainDate = intDate;
                                oldpick.MaintainTime = intTime;
                                oldpick.Cuser = CommonConstants.MaintainUser;
                                oldpick.Cdate = intDate;
                                oldpick.Ctime = intTime;
                                //拣货任务令只汇总B层物料
                                oldpick.Faccode = invoicesDetailList.First().Faccode;
                                Invoices invoices = (Invoices)_InvoicesFacade.GetInvoices(currInvoicesList.First().Invno);
                                if (invoices != null)
                                {
                                    oldpick.Receiveraddr = invoices.Shippinglocation;
                                }

                                try
                                {
                                    oldpick.Storagecode = invoicesDetailList.First(o => string.IsNullOrEmpty(o.Storagecode) == false).Storagecode;
                                }
                                catch
                                {
                                    oldpick.Storagecode = "";
                                }
                                _InvoicesFacade.UpdatePick(oldpick);
                                #endregion


                            }
                        }
                        #endregion
                    }
                    #endregion
                    else
                    {
                        #region Release
                        if (status == "Release")
                        {
                            #region
                            Pick pick = new Pick();
                            pick.Pickno = _InvoicesFacade.GetPickNO();
                            pick.Picktype = currInvoicesList.First().Invtype;
                            pick.Status = "Release";
                            pick.Invno = batchno;
                            //pick.Faccode = ""; //在DN行上
                            //pick.Storagecode = ""; //在DN行上
                            pick.Plangidate = currInvoicesList.OrderBy(o => o.Plangidate).First().Plangidate.ToString();
                            pick.Cusbatchno = currInvoicesList.First().Cusbatchno;
                            //pick.Gfflag = currInvoicesList.First().Gfflag;
                            bool isGf = currInvoicesList.GroupBy(p => p.Gfflag).Select(p => p.Key).ToList().Contains("X"); //.First().Gfflag;
                            if (isGf)
                            {
                                pick.Gfflag = "X";
                            }
                            pick.MaintainUser = CommonConstants.MaintainUser;
                            pick.MaintainDate = intDate;
                            pick.MaintainTime = intTime;
                            pick.Cuser = CommonConstants.MaintainUser;
                            pick.Cdate = intDate;
                            pick.Ctime = intTime;
                            //拣货任务令只汇总B层物料
                            pick.Faccode = invoicesDetailList.First().Faccode;
                            //pick.Receiveraddr = invoicesDetailList.First().Shipaddr;

                            Invoices invoices = (Invoices)_InvoicesFacade.GetInvoices(currInvoicesList.First().Invno);
                            if (invoices != null)
                            {
                                pick.Receiveraddr = invoices.Shippinglocation;
                            }
                            try
                            {
                                pick.Storagecode = invoicesDetailList.First(o => string.IsNullOrEmpty(o.Storagecode) == false).Storagecode;
                            }
                            catch
                            {
                                pick.Storagecode = "";
                            }
                            _InvoicesFacade.AddPick(pick);
                            #endregion

                            #region line
                            int intDate1, intTime1;
                            Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                            List<MaterialSum> msList = invoicesDetailList.GroupBy(d => d.Mcode, (key, group) => new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) }).ToList();
                            int lineNO = 1;
                            foreach (var ms in msList)
                            {
                                Pickdetail pickdetail = new Pickdetail();
                                Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);
                                pickdetail.Pickno = pick.Pickno;
                                pickdetail.Pickline = lineNO.ToString();
                                pickdetail.Status = "Release";
                                pickdetail.Mcode = ms.MCode;
                                pickdetail.Dqmcode = firstInvDe.Dqmcode;
                                pickdetail.Qty = ms.Qty;
                                pickdetail.Unit = firstInvDe.Unit;
                                //确认是否是这样获取
                                pickdetail.Cusorderno = currInvoicesList.First().Cusorderno;
                                pickdetail.Cusordernotype = currInvoicesList.First().Cusordernotype;
                                pickdetail.Cusitemcode = firstInvDe.Cusmcode;
                                pickdetail.Cusitemspec = firstInvDe.Cusitemspec;
                                pickdetail.Cusitemdesc = firstInvDe.Cusitemdesc;
                                pickdetail.Venderitemcode = firstInvDe.Vendermcode;
                                pickdetail.Gfcontractno = currInvoicesList.First().Gfcontractno;//光伏合同号 Header your reference (SAP，光伏特有)(DN)
                                pickdetail.Gfhwitemcode = firstInvDe.Gfhwmcode;
                                pickdetail.Gfpackingseq = firstInvDe.Gfpackingseq;
                                pickdetail.Postway = currInvoicesList.First().Postway;
                                pickdetail.Pickcondition = currInvoicesList.First().Pickcondition;
                                pickdetail.Hwcodeqty = firstInvDe.Hwcodeqty.ToString();
                                pickdetail.Hwtypeinfo = firstInvDe.Hwtypeinfo;
                                pickdetail.Packingway = firstInvDe.Packingway;
                                pickdetail.Packingno = firstInvDe.Packingno;
                                pickdetail.Packingspec = firstInvDe.Packingspec;
                                pickdetail.Packingwayno = firstInvDe.Packingwayno;
                                pickdetail.Dqsitemcode = firstInvDe.Dqsmcode;


                                pickdetail.MaintainUser = CommonConstants.MaintainUser;
                                pickdetail.MaintainDate = intDate1;
                                pickdetail.MaintainTime = intTime1;
                                pickdetail.Cuser = CommonConstants.MaintainUser;
                                pickdetail.Cdate = intDate1;
                                pickdetail.Ctime = intTime1;

                                _InvoicesFacade.AddPickdetail(pickdetail);

                                lineNO += 1;
                            }
                            #endregion

                        }
                        #endregion
                    }


                    //tmpPickno = pick.Pickno;

                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            #endregion

        }

        private void SyncUB()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.UB;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                //importParameters.Add("I_DATUM", "20160101");
                //importParameters.Add("I_UZEIT", "000000");
                string strDate = "";
                string strTime = "";
                if (!Common.TryGetStampDbDateTime(ref strDate, ref strTime, SAPJOBTIMESTAMP.UB))
                {
                    Log.Error("没有维护UB的时间戳参数");
                    return;
                }
                importParameters.Add("I_DATUM", strDate);
                importParameters.Add("I_UZEIT", strTime);
                importParameters.Add("I_BUKRS", SAPRfcDefaultPara.CompanyCode); //not necessary
                importParameters.Add("I_EKORG", SAPRfcDefaultPara.PurchOrgCode); //not necessary
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveNewUBData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveNewUBData(DataSet ds)
        {
            //将之前JOB未处理的记录状态置为"L"
            _InvoicesFacade.UpdateSapubFlag(MiddleTableFlag.Last);
            _InvoicesFacade.UpdateInvoicesFlag(MiddleTableFlag.Last, " ('UB','JCC','BLC','ZC') ");

            #region Insert I_Sapub by batch then commit
            if (ds != null && ds.Tables[SAPRfcOutTableName.UB] != null && ds.Tables[SAPRfcOutTableName.UB].Rows.Count > 0)
            {
                try
                {
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                    this.DataProvider.BeginTransaction();
                    foreach (DataRow row in ds.Tables[SAPRfcOutTableName.UB].Rows)
                    {
                        I_Sapub item = new I_Sapub();
                        item.Invno = Convert.ToString(row["EBELN"]);
                        item.Companycode = Convert.ToString(row["BUKRS"]);
                        item.Purchorgcode = Convert.ToString(row["EKORG"]);
                        string type = string.Empty;
                        switch (Convert.ToString(row["BSART"]))
                        {
                            case "ZJCR":
                                type = "JCR";
                                break;
                            case "ZJCC":
                                type = "JCC";
                                break;
                            case "ZBLR":
                                type = "BLR";
                                break;
                            case "ZBLC":
                                type = "BLC";
                                break;
                            case "ZCAR":
                                type = "CAR";
                                break;
                            default:
                                type = Convert.ToString(row["BSART"]);
                                break;
                        }
                        item.Ubtype = type;
                        item.Poupdatedate = Convert.ToInt32(row["AEDAT"]);
                        item.Poupdatetime = Convert.ToInt32(row["AEZET"]);
                        item.Oano = Convert.ToString(row["IHREZ"]);
                        item.Logistics = Convert.ToString(row["HTEXT"]);
                        item.Remark = Convert.ToString(row["DTEXT"]);//备注
                        item.Createuser = Convert.ToString(row["UNAME"]);
                        item.Pocreatedate = Convert.ToInt32(row["DATUM"]);//UB创建日期
                        item.Reworkapplyuser = Convert.ToString(row["UNSEZ"]);
                        item.Fromfaccode = Convert.ToString(row["RESWK"]);

                        item.Invline = Convert.ToInt32(row["EBELP"]);
                        item.Mcode = Convert.ToString(row["MATNR"]);
                        item.Mdesc = Convert.ToString(row["TXZ01"]);//物料描述
                        string strLineStatus = default(string);
                        switch (Convert.ToString(row["LOEKZ"]))
                        {
                            case "L":
                                strLineStatus = UBDetailStatus.Cancel;
                                break;
                            case "S":
                                strLineStatus = UBDetailStatus.Pending;
                                break;
                            default:
                                strLineStatus = UBDetailStatus.Release;
                                break;
                        }
                        item.Invlinestatus = strLineStatus;//调拨行状态
                        item.Faccode = Convert.ToString(row["WERKS"]);
                        item.Fromstoragecode = Convert.ToString(row["AFNAM"]);
                        item.Storagecode = Convert.ToString(row["LGORT"]);
                        item.Receiveraddr = Convert.ToString(row["STRAS"]);
                        item.Receiveruser = Convert.ToString(row["RINFO"]);
                        item.Qty = Convert.ToDecimal(row["MENGE"]);//调拨数量
                        item.Unit = Convert.ToString(row["MEINS"]);
                        item.Custmcode = Convert.ToString(row["IDNLF"]);
                        item.Receivemcode = Convert.ToString(row["STEXT"]);
                        item.Demandarrivaldate = Convert.ToInt32(row["EINDT"]);

                        item.Mesflag = MiddleTableFlag.Wait;
                        item.Sdate = intDate;
                        item.Stime = intTime;
                        item.Pdate = intDate;
                        item.Ptime = intTime;

                        _InvoicesFacade.AddSapub(item);
                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
            #endregion

            #region insert ub into invoice table, commit by invno
            object[] arr = _InvoicesFacade.GetSapubByFlag(MiddleTableFlag.Wait);
            //UB中间表有待处理的记录
            if (arr != null)
            {
                List<Material> materialList = this.GetAllMaterialList();

                List<I_Sapub> sapubList = Common.Array2SapubList(arr);
                var ubnoList = sapubList.GroupBy(p => p.Invno).Select(p => p.Key).ToList();

                foreach (var ubno in ubnoList)
                {
                    List<I_Sapub> currList = sapubList.Where(p => p.Invno == ubno).ToList();
                    if (currList.Count > 0)
                    {
                        try
                        {
                            this.DataProvider.BeginTransaction();

                            #region  head
                            int intDate, intTime;
                            Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                            bool isUpdate = _InvoicesFacade.QueryInvoicesCount(ubno) > 0;
                            Invoices head = new Invoices();
                            head.Invno = ubno;
                            head.Invstatus = CommonConstants.InvoicesInitSatus;
                            head.Companycode = currList.First().Companycode;
                            head.Purchorgcode = currList.First().Purchorgcode;
                            head.Invtype = currList.First().Ubtype; //???
                            head.Ubtype = currList.First().Ubtype;
                            head.Poupdatedate = currList.First().Poupdatedate;
                            head.Poupdatetime = currList.First().Poupdatetime;
                            head.Oano = currList.First().Oano;
                            head.Logistics = currList.First().Logistics;
                            head.Remark1 = currList.First().Remark;//备注
                            head.Createuser = currList.First().Createuser;
                            head.Pocreatedate = currList.First().Pocreatedate;//UB创建日期
                            head.Reworkapplyuser = currList.First().Reworkapplyuser;
                            head.Fromfaccode = currList.First().Fromfaccode;

                            head.Finishflag = CommonConstants.InvoicesInitFinishFlag;
                            head.MaintainUser = CommonConstants.MaintainUser;
                            head.MaintainDate = intDate;
                            head.MaintainTime = intTime;

                            head.Eattribute1 = "W";
                            head.Cuser = CommonConstants.MaintainUser;
                            head.Cdate = intDate;
                            head.Ctime = intTime;
                            if (!isUpdate)
                            {
                                _InvoicesFacade.AddInvoices(head);
                            }
                            else
                            {
                                _InvoicesFacade.UpdateInvoices(head);
                            }
                            #endregion

                            #region line
                            int intDate1, intTime1;
                            Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                            bool isPending = false;
                            bool isAllCancel = true;
                            foreach (var item in currList)
                            {
                                if (item.Invlinestatus == UBDetailStatus.Pending)
                                {
                                    isPending = true;
                                }
                                if (item.Invlinestatus != UBDetailStatus.Cancel)
                                {
                                    isAllCancel = false;
                                }
                                bool isdelUpdate = _InvoicesFacade.QueryInvoicesdetailCount(item.Invno, item.Invline) > 0;
                                Invoicesdetail detail = new Invoicesdetail();

                                detail.Invno = item.Invno;
                                detail.Invline = item.Invline;
                                detail.Status = item.Invlinestatus;
                                detail.Invlinestatus = item.Invlinestatus;
                                detail.Mcode = item.Mcode;
                                detail.Menshortdesc = item.Mdesc;//物料描述
                                detail.Faccode = item.Faccode;
                                detail.Fromstoragecode = item.Fromstoragecode;
                                detail.Storagecode = item.Storagecode;
                                detail.Receiveraddr = item.Receiveraddr;
                                detail.Receiveruser = item.Receiveruser;
                                detail.Planqty = item.Qty;//调拨数量
                                detail.Unit = item.Unit;
                                detail.Custmcode = item.Custmcode;
                                detail.Receivemcode = item.Receivemcode;
                                detail.Demandarrivaldate = item.Demandarrivaldate;
                                List<Material> maTempList = materialList.Where(m => m.Mcode == detail.Mcode).ToList();
                                if (maTempList.Count > 0)
                                {
                                    detail.Dqmcode = maTempList[0].Dqmcode;
                                }

                                detail.MaintainUser = CommonConstants.MaintainUser;
                                detail.MaintainDate = intDate1;
                                detail.MaintainTime = intTime1;
                                detail.Cuser = CommonConstants.MaintainUser;
                                detail.Cdate = intDate1;
                                detail.Ctime = intTime1;

                                if (isdelUpdate)
                                {
                                    _InvoicesFacade.UpdateInvoicesdetail(detail);
                                }
                                else
                                {
                                    _InvoicesFacade.AddInvoicesdetail(detail);
                                }
                            }

                            #endregion

                            #region Updatehead

                            if (isPending)
                            {
                                #region pend
                                _InvoicesFacade.UpdateInvoicesStatusByInvNO(ubno, UBHeadStatus.Pending, "W");
                                #endregion
                            }
                            else
                            {
                                if (isAllCancel)
                                {
                                    #region isNotAllCancel
                                    _InvoicesFacade.UpdateInvoicesStatusByInvNO(ubno, UBHeadStatus.Cancel, "W");
                                    #endregion
                                }
                                else
                                {
                                    #region AllCancel
                                    _InvoicesFacade.UpdateInvoicesStatusByInvNO(ubno, UBHeadStatus.Release, "W");
                                    #endregion
                                }

                            }
                            #endregion



                            _InvoicesFacade.UpdateSapubFlagByUBNO(ubno, MiddleTableFlag.Success);
                            this.DataProvider.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            _InvoicesFacade.UpdateSapubFlagByUBNO(ubno, MiddleTableFlag.Fail);
                            throw ex;
                        }
                    }

                }
            }
            #endregion

            //将之前JOB未处理的记录状态置为"S"
            _InvoicesFacade.UpdateSapstoragecheckFlag(MiddleTableFlag.Success);

            #region 产生拣货任务令
            try
            {
                //invtype in ('UB','JCC','BLC')
                object[] arrInvoices = _InvoicesFacade.GetUBInvoices();
                //没有需要产生拣货任务令的UB单
                if (arrInvoices != null)
                {
                    List<Invoices> invoicesList = Common.Array2InvoicesList(arrInvoices);

                    this.DataProvider.BeginTransaction();
                    foreach (var invoices in invoicesList)
                    {
                        if (_InvoicesFacade.QueryPickCountByDNBatchNO(invoices.Invno) > 0)
                        {
                            #region Update
                            string batchno = invoices.Invno;
                            _InvoicesFacade.UpdateCartonInvoicesStatusByDnBatchNo("Release", batchno);

                            Pick oldpick = (Pick)_InvoicesFacade.GetPickByDNBatchNO(batchno);
                            if (oldpick != null)
                            {
                                object[] arrInvoicesDetail = _InvoicesFacade.GetUBInvoicesDetailByUBNO(invoices.Invno);
                                List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                                if (arrInvoicesDetail == null)
                                {
                                    //add by sam
                                    _InvoicesFacade.DeletePickDetailByPickno(oldpick.Pickno);
                                    continue;
                                }
                                List<MaterialSum> newmsList = invoicesDetailList.GroupBy(d => d.Mcode, (key, group) => new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) }).ToList();

                                if (!(oldpick.Status == PickHeadStatus.PickHeadStatus_Close ||
                                    oldpick.Status == PickHeadStatus.PickHeadStatus_Cancel))
                                {
                                    #region Pickdetail
                                    int intDate1, intTime1;
                                    Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                                    #region invoicesDetail中的

                                    foreach (var ms in newmsList)
                                    {
                                        Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);
                                        Pickdetail pickdetail =
                                            (Pickdetail)_InvoicesFacade.GetPickdetail(oldpick.Pickno, ms.MCode);
                                        bool isUpdateLine = pickdetail != null;
                                        if (!isUpdateLine)
                                        {
                                            pickdetail = new Pickdetail
                                                {
                                                    Pickno = oldpick.Pickno,
                                                    Pickline = _InvoicesFacade.GetMaxPickLine(oldpick.Pickno),
                                                    Status = "Pick"
                                                };
                                            if (oldpick.Status == PickHeadStatus.PickHeadStatus_Release)
                                            {
                                                pickdetail.Status = PickHeadStatus.PickHeadStatus_Release;
                                            }
                                        }
                                        else
                                        {
                                            //更新
                                            if (pickdetail.Status == PickHeadStatus.PickHeadStatus_Release)
                                            {
                                                pickdetail.Status = PickHeadStatus.PickHeadStatus_Release;
                                            }
                                            else if (pickdetail.Status == "Cancel")
                                            {
                                                pickdetail.Status = firstInvDe.Status;
                                            }
                                            else
                                            {
                                                pickdetail.Status = "Pick";
                                            }
                                        }

                                        pickdetail.Custmcode = firstInvDe.Custmcode;
                                        pickdetail.Mcode = ms.MCode;
                                        pickdetail.Dqmcode = firstInvDe.Dqmcode;
                                        pickdetail.Qty = ms.Qty;
                                        pickdetail.Unit = firstInvDe.Unit;

                                        pickdetail.MaintainUser = CommonConstants.MaintainUser;
                                        pickdetail.MaintainDate = intDate1;
                                        pickdetail.MaintainTime = intTime1;
                                        pickdetail.Cuser = CommonConstants.MaintainUser;
                                        pickdetail.Cdate = intDate1;
                                        pickdetail.Ctime = intTime1;
                                        if (!isUpdateLine)
                                        {
                                            _InvoicesFacade.AddPickdetail(pickdetail);
                                        }
                                        else
                                        {
                                            _InvoicesFacade.UpdatePickdetail(pickdetail);
                                        }
                                    }

                                    #endregion
                                    #endregion
                                }

                                #region 区分 invoicesDetailList中没有的 update Cancel

                                List<string> mList =
                                    invoicesDetailList.GroupBy(d => d.Mcode).Select(p => p.Key).ToList();
                                // tblinvoicesDetail
                                object[] arrPickDetail = _InvoicesFacade.GetPickDetailByDNBatchNO(batchno);
                                List<Pickdetail> pickDetailList = Common.Array2PickdetailList(arrPickDetail);
                                List<string> mpickList =
                                    pickDetailList.GroupBy(d => d.Mcode).Select(p => p.Key).ToList();
                                //tblPickdetail
                                List<string> moldpickList = mpickList.Except(mList).ToList();
                                //取差集() tblPickdetail有的，tblinvoicesDetail没有的
                                foreach (string mcode in moldpickList)
                                {
                                    Pickdetail pickdetail =
                                        (Pickdetail)_InvoicesFacade.GetPickdetail(oldpick.Pickno, mcode);
                                    if (pickdetail != null)
                                    {
                                        pickdetail.Status = "Cancel";
                                        _InvoicesFacade.UpdatePickdetail(pickdetail);
                                    }
                                }

                                #endregion

                                if (oldpick.Status == PickHeadStatus.PickHeadStatus_Close ||
                                 oldpick.Status == PickHeadStatus.PickHeadStatus_Cancel)
                                {
                                    continue;
                                }

                                _InvoicesFacade.UpdateOQCStatusByPickno("Cancel", oldpick.Pickno);

                                #region update  oldpick
                                int intDate, intTime;
                                Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                                if (oldpick.Status != PickHeadStatus.PickHeadStatus_Release)
                                {
                                    oldpick.Status = "Pick";
                                }

                                oldpick.Picktype = invoices.Invtype;
                                oldpick.Invno = invoices.Invno;
                                oldpick.Oano = invoices.Oano;
                                oldpick.Remark1 = invoices.Remark1;
                                oldpick.Reworkapplyuser = invoices.Reworkapplyuser;
                                oldpick.Faccode = invoices.Fromfaccode;

                                oldpick.MaintainUser = CommonConstants.MaintainUser;
                                oldpick.MaintainDate = intDate;
                                oldpick.MaintainTime = intTime;
                                oldpick.Cuser = CommonConstants.MaintainUser;
                                oldpick.Cdate = intDate;
                                oldpick.Ctime = intTime;


                                oldpick.Receiveraddr = invoicesDetailList.First().Receiveraddr;
                                oldpick.Receiveruser = invoicesDetailList.First().Receiveruser;
                                oldpick.Storagecode = invoicesDetailList.First().Fromstoragecode;
                                oldpick.Infaccode = invoicesDetailList.First().Faccode;
                                oldpick.Instoragecode = invoicesDetailList.First().Storagecode;
                                _InvoicesFacade.UpdatePick(oldpick);
                                #endregion


                            }

                            #endregion
                        }
                        else
                        {
                            #region add Pick
                            int intDate, intTime;
                            Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                            Pick pick = new Pick();
                            pick.Pickno = _InvoicesFacade.GetPickNO();
                            pick.Picktype = invoices.Invtype;
                            pick.Status = CommonConstants.PickInitSatus;
                            pick.Invno = invoices.Invno;
                            pick.Oano = invoices.Oano;
                            pick.Remark1 = invoices.Remark1;
                            pick.Reworkapplyuser = invoices.Reworkapplyuser;
                            pick.Faccode = invoices.Fromfaccode;

                            pick.MaintainUser = CommonConstants.MaintainUser;
                            pick.MaintainDate = intDate;
                            pick.MaintainTime = intTime;
                            pick.Cuser = CommonConstants.MaintainUser;
                            pick.Cdate = intDate;
                            pick.Ctime = intTime;

                            object[] arrInvoicesDetail = _InvoicesFacade.GetUBInvoicesDetailByUBNO(invoices.Invno);
                            List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                            if (invoicesDetailList == null)
                            {
                                continue;
                            }
                            pick.Receiveraddr = invoicesDetailList.First().Receiveraddr;
                            pick.Receiveruser = invoicesDetailList.First().Receiveruser;
                            pick.Storagecode = invoicesDetailList.First().Fromstoragecode;
                            pick.Infaccode = invoicesDetailList.First().Faccode;
                            pick.Instoragecode = invoicesDetailList.First().Storagecode;
                            _InvoicesFacade.AddPick(pick);

                            #endregion

                            #region pickdetail

                            int intDate1, intTime1;
                            Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                            //按物料编码合并
                            List<MaterialSum> msList = invoicesDetailList.GroupBy(d => d.Mcode, (key, group) => new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) }).ToList();
                            int lineNO = 1;
                            foreach (var ms in msList)
                            {
                                Pickdetail pickdetail = new Pickdetail();
                                Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);
                                pickdetail.Pickno = pick.Pickno;
                                pickdetail.Pickline = lineNO.ToString();
                                pickdetail.Status = CommonConstants.PickDetailInitSatus;
                                pickdetail.Mcode = ms.MCode;
                                pickdetail.Dqmcode = firstInvDe.Dqmcode;
                                pickdetail.Qty = ms.Qty;
                                pickdetail.Unit = firstInvDe.Unit;
                                pickdetail.Custmcode = firstInvDe.Custmcode;
                                pickdetail.MaintainUser = CommonConstants.MaintainUser;
                                pickdetail.MaintainDate = intDate1;
                                pickdetail.MaintainTime = intTime1;
                                pickdetail.Cuser = CommonConstants.MaintainUser;
                                pickdetail.Cdate = intDate1;
                                pickdetail.Ctime = intTime1;

                                _InvoicesFacade.AddPickdetail(pickdetail);

                                lineNO += 1;
                            }
                            #endregion
                        }


                    }
                    this.DataProvider.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            #endregion

            #region UB insert TBLStorLocTrans&TBLStorLocTransDetail
            try
            {
                //invtype='ZC'
                object[] arrInvoices = _InvoicesFacade.GetUBInvoicesNoStorLocTrans();
                //没有需要产生货位移动单的UB单(ZC)
                if (arrInvoices == null)
                {
                    return;
                }
                List<Invoices> invoicesList = Common.Array2InvoicesList(arrInvoices);
                string tmpTransNO = default(string);

                this.DataProvider.BeginTransaction();
                foreach (var invoices in invoicesList)
                {
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                    string invno = invoices.Invno;



                    Storloctrans storloctrans = _InvoicesFacade.GetStorloctransByInvNO(invno) as Storloctrans;
                    if (storloctrans != null)
                    {

                        object[] arrInvoicesDetail = _InvoicesFacade.GetUBInvoicesDetailByUBNO(invno);
                        List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                        if (invoicesDetailList == null)
                        {
                            continue;
                        }



                        int intDate1, intTime1;
                        Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                        //按物料编码合并
                        List<MaterialSum> msList =
                            invoicesDetailList.GroupBy(d => d.Mcode,
                                                       (key, group) =>
                                                       new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) })
                                              .ToList();

                        string transno = storloctrans.Transno;

                        if (!(storloctrans.Status == PickHeadStatus.PickHeadStatus_Close ||
                              storloctrans.Status == PickHeadStatus.PickHeadStatus_Cancel))
                        {

                            #region Detail

                            foreach (var ms in msList)
                            {
                                Storloctransdetail sdetail =
                                    _InvoicesFacade.GetStorloctransdetail(transno, ms.MCode) as Storloctransdetail;
                                bool isUpdateDetail = true;
                                if (sdetail == null)
                                {
                                    sdetail = new Storloctransdetail();
                                    isUpdateDetail = false;
                                }

                                Material material = (Material)_InvoicesFacade.GetMaterial(ms.MCode);
                                Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);
                                sdetail.Transno = storloctrans.Transno;
                                //sdetail.Pickline = lineNO.ToString();
                                if (invoices.Invstatus == "Cancel" || invoices.Invstatus == "Pending")
                                {
                                    sdetail.Status = invoices.Invstatus;
                                }
                                else
                                {
                                    sdetail.Status = CommonConstants.StorLocTransDetailSatus;
                                }


                                sdetail.Custmcode = firstInvDe.Custmcode;
                                sdetail.Mcode = ms.MCode;
                                sdetail.Dqmcode = firstInvDe.Dqmcode;
                                sdetail.Unit = firstInvDe.Unit;
                                sdetail.Qty = ms.Qty;
                                sdetail.Mdesc = material.Mchshortdesc;

                                sdetail.MaintainUser = CommonConstants.MaintainUser;
                                sdetail.MaintainDate = intDate1;
                                sdetail.MaintainTime = intTime1;


                                if (!isUpdateDetail)
                                {
                                    sdetail.Cuser = CommonConstants.MaintainUser;
                                    sdetail.Cdate = intDate1;
                                    sdetail.Ctime = intTime1;
                                    _InvoicesFacade.AddStorloctransdetail(sdetail);
                                }
                                else
                                {
                                    _InvoicesFacade.UpdateStorloctransdetail(sdetail);
                                }

                            }

                            #endregion
                        }
                        #region 区分 invoicesDetailList中没有的 update Cancel

                        List<string> mList =
                            invoicesDetailList.GroupBy(d => d.Mcode).Select(p => p.Key).ToList();
                        // tblinvoicesDetail
                        object[] arrTransDetail = _InvoicesFacade.GetTransDetailByDNBatchNO(invno);
                        List<Storloctransdetail> transDetailList = Common.Array2StorloctransdetailList(arrTransDetail);
                        List<string> mpickList =
                            transDetailList.GroupBy(d => d.Mcode).Select(p => p.Key).ToList();
                        //tblPickdetail
                        List<string> moldpickList = mpickList.Except(mList).ToList();
                        //取差集() tblPickdetail有的，tblinvoicesDetail没有的
                        foreach (string mcode in moldpickList)
                        {
                            Storloctransdetail storloctransdetail =
                                (Storloctransdetail)_InvoicesFacade.GetStorloctransdetail(transno, mcode);
                            if (storloctransdetail != null)
                            {
                                storloctransdetail.Status = "Cancel";
                                _InvoicesFacade.UpdateStorloctransdetail(storloctransdetail);
                            }
                        }

                        #endregion

                        if (storloctrans.Status == PickHeadStatus.PickHeadStatus_Close ||
                            storloctrans.Status == PickHeadStatus.PickHeadStatus_Release
                            || storloctrans.Status == PickHeadStatus.PickHeadStatus_Cancel)
                        {
                            continue;
                        }

                        #region Update oldHead
                        storloctrans.Transtype = StorLocTransTypes.ZC;

                        if (invoices.Invstatus == "Cancel" || invoices.Invstatus == "Pending")
                        {
                            storloctrans.Status = invoices.Invstatus;
                        }
                        else
                        {
                            storloctrans.Status = PickHeadStatus.PickHeadStatus_Release;
                        }
                        storloctrans.Invno = invoices.Invno;
                        storloctrans.MaintainUser = CommonConstants.MaintainUser;
                        storloctrans.MaintainDate = intDate;
                        storloctrans.MaintainTime = intTime;
                        storloctrans.Storagecode = invoicesDetailList.First().Storagecode;
                        storloctrans.Fromstoragecode = invoicesDetailList.First().Fromstoragecode;
                        _InvoicesFacade.UpdateStorloctrans(storloctrans);
                        #endregion

                        //tmpTransNO = storloctrans.Transno;

                    }
                    else
                    {
                        #region AddHead

                        #region Transno
                        storloctrans = new Storloctrans();
                        if (string.IsNullOrEmpty(tmpTransNO))
                        {
                            storloctrans.Transno = _InvoicesFacade.GetStorLocTransNO();
                        }
                        else
                        {
                            string tmpStr = tmpTransNO.Substring(10);
                            storloctrans.Transno = string.Format("{0}{1}", tmpTransNO.Substring(0, 10),
                                                                 (int.Parse(tmpStr) + 1).ToString().PadLeft(4, '0'));
                        }
                        #endregion


                        storloctrans.Transtype = StorLocTransTypes.ZC;

                        storloctrans.Status = PickHeadStatus.PickHeadStatus_Release;

                        storloctrans.Invno = invoices.Invno;

                        storloctrans.MaintainUser = CommonConstants.MaintainUser;
                        storloctrans.MaintainDate = intDate;
                        storloctrans.MaintainTime = intTime;


                        object[] arrInvoicesDetail = _InvoicesFacade.GetUBInvoicesDetailByUBNO(invoices.Invno);
                        List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                        if (invoicesDetailList == null)
                        {
                            continue;
                        }

                        storloctrans.Storagecode = invoicesDetailList.First().Storagecode;
                        storloctrans.Fromstoragecode = invoicesDetailList.First().Fromstoragecode;

                        storloctrans.Cuser = CommonConstants.MaintainUser;
                        storloctrans.Cdate = intDate;
                        storloctrans.Ctime = intTime;
                        _InvoicesFacade.AddStorloctrans(storloctrans);

                        #endregion

                        int intDate1, intTime1;
                        Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                        //按物料编码合并
                        List<MaterialSum> msList =
                            invoicesDetailList.GroupBy(d => d.Mcode,
                                                       (key, group) =>
                                                       new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) })
                                              .ToList();
                        #region Detail

                        foreach (var ms in msList)
                        {
                            string transno = storloctrans.Transno;
                            //Storloctransdetail sdetail =
                            //    _InvoicesFacade.GetStorloctransdetail(transno, ms.MCode) as Storloctransdetail;
                            Storloctransdetail sdetail = new Storloctransdetail();
                            Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);
                            sdetail.Transno = storloctrans.Transno;
                            sdetail.Status = CommonConstants.StorLocTransDetailSatus;
                            sdetail.Custmcode = firstInvDe.Custmcode;
                            sdetail.Mcode = ms.MCode;
                            sdetail.Dqmcode = firstInvDe.Dqmcode;
                            sdetail.Unit = firstInvDe.Unit;
                            sdetail.Qty = ms.Qty;
                            sdetail.Mdesc = firstInvDe.Mlongdesc;

                            sdetail.MaintainUser = CommonConstants.MaintainUser;
                            sdetail.MaintainDate = intDate1;
                            sdetail.MaintainTime = intTime1;


                            sdetail.Cuser = CommonConstants.MaintainUser;
                            sdetail.Cdate = intDate1;
                            sdetail.Ctime = intTime1;
                            _InvoicesFacade.AddStorloctransdetail(sdetail);
                        }

                        #endregion

                        tmpTransNO = storloctrans.Transno;
                    }

                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            #endregion
        }

        private void SyncRS()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.RS;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                //importParameters.Add("I_DATUM", "20160101");
                //importParameters.Add("I_UZEIT", "100000");
                string strDate = "";
                string strTime = "";
                if (!Common.TryGetStampDbDateTime(ref strDate, ref strTime, SAPJOBTIMESTAMP.RS))
                {
                    Log.Error("没有维护RS的时间戳参数");
                    return;
                }
                importParameters.Add("I_DATUM", strDate);
                importParameters.Add("I_UZEIT", strTime);
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveNewRSData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveNewRSData(DataSet ds)
        {

            _InvoicesFacade.UpdateSaprsFlag(MiddleTableFlag.Last);

            #region Insert I_Sapub by batch then commit
            if (ds != null && ds.Tables[SAPRfcOutTableName.RS] != null && ds.Tables[SAPRfcOutTableName.RS].Rows.Count > 0)
            {
                try
                {
                    List<I_Sapub> list = new List<I_Sapub>();
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                    this.DataProvider.BeginTransaction();
                    foreach (DataRow row in ds.Tables[SAPRfcOutTableName.RS].Rows)
                    {
                        I_Saprs item = new I_Saprs();
                        item.Invno = Convert.ToString(row["RSNUM"]);
                        item.Cc = Convert.ToString(row["KOSTL"]);//CC
                        item.Applydate = Convert.ToInt32(row["RSDAT"]);
                        string strInvType = default(string);
                        switch (Convert.ToString(row["BWART"]))
                        {
                            case "201":
                                strInvType = "PRC"; //出库记入CC,PR领料
                                break;
                            case "202":
                                strInvType = "YFR"; //从CC入库,研发入库
                                break;
                            case "241":
                                strInvType = "GZC"; //出库记入固定资产
                                break;
                            default:
                                strInvType = Convert.ToString(row["BWART"]);
                                break;
                        }

                        item.Type = strInvType;
                        item.Invline = Convert.ToInt32(row["RSPOS"]);
                        item.Invlinestatus = Convert.ToString(row["XLOEK"]);//状态 删除标识
                        item.Mcode = Convert.ToString(row["MATNR"]);//物料编码
                        item.Custmcode = Convert.ToString(row["IDNLF"]);
                        item.Storagecode = Convert.ToString(row["UMLGO"]);
                        item.Planqty = Convert.ToDecimal(row["BDMNG"]);
                        item.Unit = Convert.ToString(row["MEINS"]);
                        item.Needdate = Convert.ToInt32(row["BDTER"]);
                        item.Prno = Convert.ToString(row["WEMPF"]);
                        item.Receiveruser = Convert.ToString(row["ABLAD"]);
                        item.Receiveraddr = Convert.ToString(row["SGTXT"]);

                        #region add by sam 2016年6月30日

                        item.SAPCuser = Convert.ToString(row["USNAM"]);//创建人员
                        item.Createuser = Convert.ToString(row["USNAM"]);//创建人员
                        item.Poupdatedate = Convert.ToInt32(row["AEDAT"]);//更新日期（年月日）
                        item.Poupdatetime = Convert.ToInt32(row["AEZET"]);//更新时间（时分秒）

                        #endregion

                        item.Mesflag = MiddleTableFlag.Wait;
                        item.Sdate = intDate;
                        item.Stime = intTime;
                        item.Pdate = intDate;
                        item.Ptime = intTime;

                        _InvoicesFacade.AddSaprs(item);

                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
            #endregion

            #region insert ub into invoice table, commit by invno
            object[] arr = _InvoicesFacade.GetSaprsByFlag(MiddleTableFlag.Wait);
            //UB中间表有待处理的记录
            if (arr != null)
            {
                List<Material> materialList = this.GetAllMaterialList();

                List<I_Saprs> sapubList = Common.Array2SaprsList(arr);
                var ubnoList = sapubList.GroupBy(p => p.Invno).Select(p => p.Key).ToList();

                foreach (var ubno in ubnoList)
                {

                    List<I_Saprs> currList = sapubList.Where(p => p.Invno == ubno).ToList();
                    if (currList.Count > 0)
                    {
                        try
                        {
                            this.DataProvider.BeginTransaction();

                            #region  head
                            int intDate, intTime;
                            Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                            bool isUpdate = _InvoicesFacade.QueryInvoicesCount(ubno) > 0;


                            Invoices head = new Invoices();
                            head.Invno = currList.First().Invno;
                            head.Cc = currList.First().Cc;
                            head.Invtype = currList.First().Type;
                            head.Invstatus = "Release";
                            head.Applydate = currList.First().Applydate;
                            head.Finishflag = "N";
                            head.MaintainUser = CommonConstants.MaintainUser;
                            head.MaintainDate = intDate;
                            head.MaintainTime = intTime;

                            #region add 2016年6月30日
                            head.SAPCuser = currList.First().SAPCuser;
                            head.Poupdatedate = currList.First().Poupdatedate;
                            head.Poupdatetime = currList.First().Poupdatetime;

                            head.Createuser = currList.First().SAPCuser;//创建人
                            head.Pocreatedate = currList.First().Poupdatedate;//UB创建日期

                            head.Cuser = CommonConstants.MaintainUser;
                            head.Cdate = intDate;
                            head.Ctime = intTime;
                            #endregion
                            if (!isUpdate)
                            {
                                _InvoicesFacade.AddInvoices(head);
                            }
                            else
                            {
                                _InvoicesFacade.UpdateInvoices(head);
                            }
                            #endregion

                            #region line
                            int intDate1, intTime1;
                            Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                            bool isAllCancel = true;
                            foreach (var item in currList)
                            {
                                if (item.Invlinestatus != UBDetailStatus.Cancel)
                                {
                                    isAllCancel = false;
                                }
                                bool isdelUpdate = _InvoicesFacade.QueryInvoicesdetailCount(item.Invno, item.Invline) > 0;
                                Invoicesdetail detail = new Invoicesdetail();
                                detail.Invno = item.Invno;
                                detail.Invline = item.Invline;
                                detail.Invlinestatus = item.Invlinestatus == "X" ? "Cancel" : "Release";//X=删除，空=未删除
                                detail.Status = item.Invlinestatus == "X" ? "Cancel" : "Release";//X=删除，空=未删除
                                detail.Mcode = item.Mcode;

                                detail.Custmcode = item.Custmcode;
                                detail.Storagecode = item.Storagecode;
                                detail.Planqty = item.Planqty;
                                detail.Unit = item.Unit;
                                List<Material> maTempList = materialList.Where(m => m.Mcode == detail.Mcode).ToList();
                                if (maTempList.Count > 0)
                                {
                                    detail.Dqmcode = maTempList[0].Dqmcode;
                                }
                                detail.Needdate = item.Needdate;
                                detail.Prno = item.Prno;
                                detail.Receiveruser = item.Receiveruser;
                                detail.Receiveraddr = item.Receiveraddr;
                                detail.MaintainUser = CommonConstants.MaintainUser;
                                detail.MaintainDate = intDate1;
                                detail.MaintainTime = intTime1;
                                detail.Cuser = CommonConstants.MaintainUser;
                                detail.Cdate = intDate1;
                                detail.Ctime = intTime1;

                                if (isdelUpdate)
                                {
                                    _InvoicesFacade.UpdateInvoicesdetail(detail);
                                }
                                else
                                {
                                    _InvoicesFacade.AddInvoicesdetail(detail);
                                }
                            }

                            #endregion

                            #region Updatehead
                            if (isAllCancel)
                            {
                                #region isAllCancel
                                _InvoicesFacade.UpdateInvoicesStatusByInvNO(ubno, UBHeadStatus.Cancel, "W");
                                #endregion
                            }
                            else
                            {
                                #region isNotAllCancel
                                _InvoicesFacade.UpdateInvoicesStatusByInvNO(ubno, UBHeadStatus.Release, "W");
                                #endregion
                            }
                            #endregion



                            _InvoicesFacade.UpdateSapubFlagByUBNO(ubno, MiddleTableFlag.Success);
                            this.DataProvider.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            _InvoicesFacade.UpdateSapubFlagByUBNO(ubno, MiddleTableFlag.Fail);
                            throw ex;
                        }
                    }

                }
            }
            #endregion


        }


        private void SyncStock()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.Stock;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_WERKS", SAPRfcDefaultPara.FacCode);
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveStockData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveStockData(DataSet ds)
        {
            //将之前JOB未处理的记录状态置为"L"
            _InvoicesFacade.UpdateISapstorageinfoFlag(MiddleTableFlag.Last);

            #region Insert I_Sapstorageinfo by batch then commit

            if (ds != null && ds.Tables[SAPRfcOutTableName.Stock] != null && ds.Tables[SAPRfcOutTableName.Stock].Rows.Count > 0)
            {
                try
                {
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                    this.DataProvider.BeginTransaction();
                    foreach (DataRow row in ds.Tables[SAPRfcOutTableName.Stock].Rows)
                    {
                        I_Sapstorageinfo item = new I_Sapstorageinfo();
                        item.Faccode = Convert.ToString(row["WERKS"]);
                        item.Mcode = Convert.ToString(row["MATNR"]);
                        item.Storagecode = Convert.ToString(row["LGORT"]);
                        item.Type = Convert.ToString(row["SOBKZ"]);
                        item.Typeno = Convert.ToString(row["SSNUM"]);
                        item.Availableqty = Convert.ToDecimal(row["LABST"]);
                        item.Qualityqty = Convert.ToDecimal(row["INSME"]);
                        item.Freezeqty = Convert.ToDecimal(row["SPEME"]);
                        item.Transitqty = Convert.ToDecimal(row["VMUML"]);
                        item.Freezereturnqty = Convert.ToDecimal(row["RETME"]);
                        item.Unit = Convert.ToString(row["MEINS"]);
                        item.Mesflag = MiddleTableFlag.Wait;
                        item.Sdate = intDate;
                        item.Stime = intTime;
                        item.Pdate = intDate;
                        item.Ptime = intTime;

                        _InvoicesFacade.AddSapstorageinfo(item);
                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
            #endregion

            #region copy to business table from middle table
            try
            {
                this.DataProvider.BeginTransaction();
                _InvoicesFacade.DeleteAllSapstorageinfo();
                _InvoicesFacade.InsertSapstorageinfoFromMiddleTable();
                _InvoicesFacade.UpdateISapstorageinfoFlag(MiddleTableFlag.Success);

                #region 物料比对 add by sam

                _InvoicesFacade.DeleteAllStorageSap2Mes();
                object[] arrSapstorageinfos = _InvoicesFacade.GetAllSapstorageinfo();//TBLSAPSTORAGEINFO  
                object[] arrStorageDetail = _InvoicesFacade.GetAllStorageDetail(); //StorageDetail
                List<Sapstorageinfo> sapstorageinfo = Common.Array2SapstorageinfoList(arrSapstorageinfos);
                List<StorageDetail> storageDetailList = Common.Array2StorageDetailfoList(arrStorageDetail);
                int intDate, intTime;
                Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                var storagecodeList = sapstorageinfo.GroupBy(p => p.Storagecode).Select(p => p.Key).ToList();
                var mesStoragecodeList = storageDetailList.GroupBy(p => p.StorageCode).Select(p => p.Key).ToList();
                StringBuilder sb = new StringBuilder(10000);

                foreach (var sapstoragecode in storagecodeList)
                {
                    List<Sapstorageinfo> currInvoicesList = sapstorageinfo.Where(o => o.Storagecode == sapstoragecode).ToList();
                    List<MaterialSum> msList = currInvoicesList.GroupBy(d => d.Mcode, (key, group) => new MaterialSum
                    {
                        MCode = key,
                        Qty = group.Sum(p => p.Availableqty + p.Qualityqty + p.Freezeqty)
                    }).ToList();
                    foreach (var ms in msList)
                    {
                        #region SAP有的
                        bool ishasStorage = storageDetailList.GroupBy(p => p.StorageCode).Select(p => p.Key).ToList().Contains(sapstoragecode);//MES是否存该库位
                        Storagesap2mes storagesap2Mes = new Storagesap2mes();
                        storagesap2Mes.Sapstoragecode = sapstoragecode;
                        storagesap2Mes.Mesqty = 0;
                        storagesap2Mes.MaintainDate = intDate;
                        storagesap2Mes.MaintainTime = intTime;
                        storagesap2Mes.MaintainUser = CommonConstants.MaintainUser;
                        if (ishasStorage)
                        {
                            //SAP有该库位，Mes有该库位,
                            storagesap2Mes.Messtoragecode = sapstoragecode;
                            storagesap2Mes.Mesqty = _InvoicesFacade.GetStorageDetailQty(sapstoragecode, ms.MCode);
                            //List<StorageDetail> currInvoicesDetialList = storageDetailList.Where(o =>o.MCode==ms.MCode).ToList();
                            //if (currInvoicesDetialList.Any())
                            //{
                            //    storagesap2Mes.Dqmcode = currInvoicesDetialList.First().DQMCode;
                            //}
                            Material m = _InvoicesFacade.GetMaterial(ms.MCode) as Material;
                            if (m != null)
                            {
                                storagesap2Mes.Dqmcode = m.Dqmcode;
                            }
                        }

                        storagesap2Mes.Mcode = ms.MCode;
                        storagesap2Mes.Sapqty = ms.Qty;
                        storagesap2Mes.Disqty = ms.Qty - storagesap2Mes.Mesqty;
                        if (storagesap2Mes.Disqty != 0)
                        {
                            _InvoicesFacade.AddStoragesap2mes(storagesap2Mes);
                        }

                        sb.Append(storagesap2Mes.Mcode + "  " + storagesap2Mes.Dqmcode + "  " + storagesap2Mes.Sapstoragecode + "  " + storagesap2Mes.Messtoragecode + "  " + storagesap2Mes.Sapqty + "  " + storagesap2Mes.Mesqty + "  " + storagesap2Mes.Disqty + @"\r\n");

                        #endregion
                    }
                }

                foreach (var storagecode in mesStoragecodeList)
                {
                    List<StorageDetail> currInvoicesList = storageDetailList.Where(o => o.StorageCode == storagecode).ToList();
                    List<MaterialSum> msList = currInvoicesList.GroupBy(d => d.MCode, (key, group) => new MaterialSum
                    {
                        MCode = key,
                        Qty = group.Sum(p => p.StorageQty)
                    }).ToList();

                    foreach (var ms in msList)
                    {
                        #region MES有的
                        bool ishas = sapstorageinfo.GroupBy(p => p.Storagecode).Select(p => p.Key).ToList().Contains(storagecode);
                        Storagesap2mes storagesap2Mes = new Storagesap2mes();
                        List<StorageDetail> currInvoicesDetialList = storageDetailList.Where(o => o.MCode == ms.MCode).ToList();
                        storagesap2Mes.Messtoragecode = storagecode;
                        storagesap2Mes.Mesqty = ms.Qty;
                        if (currInvoicesDetialList.Any())
                        {
                            storagesap2Mes.Dqmcode = currInvoicesDetialList.First().DQMCode;
                        }
                        storagesap2Mes.Sapqty = 0;
                        storagesap2Mes.Mcode = ms.MCode;
                        storagesap2Mes.MaintainDate = intDate;
                        storagesap2Mes.MaintainTime = intTime;
                        storagesap2Mes.MaintainUser = CommonConstants.MaintainUser;
                        if (!ishas)
                        {
                            //MES有该库位，SAP没有该库位
                            storagesap2Mes.Disqty = 0 - storagesap2Mes.Mesqty;
                            sb.Append(storagesap2Mes.Mcode + "  " + storagesap2Mes.Dqmcode + "  " + storagesap2Mes.Sapstoragecode + "  " + storagesap2Mes.Messtoragecode + "  " + storagesap2Mes.Sapqty + "  " + storagesap2Mes.Mesqty + "  " + storagesap2Mes.Disqty + @"\r\n");
                            _InvoicesFacade.AddStoragesap2mes(storagesap2Mes);
                        }
                        else
                        {
                            //MES有该库位，SAP有该库位
                            //MES有此物料，SAP没有
                            bool ishasMcode = sapstorageinfo.Where(o => o.Storagecode == storagecode).ToList().GroupBy(p => p.Mcode).Select(p => p.Key).ToList().Contains(ms.MCode);
                            if (!ishasMcode)
                            {
                                storagesap2Mes.Sapstoragecode = storagecode;
                                storagesap2Mes.Disqty = 0 - storagesap2Mes.Mesqty;
                                sb.Append(storagesap2Mes.Mcode + "  " + storagesap2Mes.Dqmcode + "  " + storagesap2Mes.Sapstoragecode + "  " + storagesap2Mes.Messtoragecode + "  " + storagesap2Mes.Sapqty + "  " + storagesap2Mes.Mesqty + "  " + storagesap2Mes.Disqty + @"\r\n");
                                _InvoicesFacade.AddStoragesap2mes(storagesap2Mes);
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                List<SendMail> mails = ToMailBlocks(sb, ShareLib.ShareKit.MailName.Sap2MESDiversityMail); ;
                foreach (SendMail mail in mails)
                    _InvoicesFacade.AddSendMail(mail);
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            #endregion
        }

        private List<SendMail> ToMailBlocks(StringBuilder sb, string mailName)
        {
            string mailBody = sb.ToString();
            List<string> mailBodyPartys = new List<string>();

            if (mailBody.Length > 0)
            {
                while (mailBody.Length > 1000)
                {
                    mailBodyPartys.Add(mailBody.Substring(0, 1000));
                    mailBody = mailBody.Substring(1000);
                }

                if (mailBody.Length > 0 && mailBody.Length <= 1000)
                    mailBodyPartys.Add(mailBody);

            }
            string blockName = DateTime.Now.ToString("yyMMddHHmmss");
            List<SendMail> mails = new List<SendMail>();
            if (mailBodyPartys.Count > 0)
            {
                int index = 1;
                foreach (string mailBodyParty in mailBodyPartys)
                {
                    SendMail mail = new SendMail();
                    mail.BLOCKNAME = blockName;
                    mail.BLOCKINX = index;
                    mail.MAILCONTENT = mailBody;
                    mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                    mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                    mail.CUSER = "JOB";
                    mail.MAILCONTENT = mailBodyParty;
                    mail.MAILTYPE = mailName;
                    mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                    mail.SENDFLAG = "N";
                    index++;
                    mails.Add(mail);
                }
            }
            return mails;
        }

        private void SyncStockCheck()
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = SAPRfcFunctionName.StockCheck;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                //importParameters.Add("I_CPUDT", "20160101");
                string strDate = "";
                string strTime = "";
                if (!Common.TryGetStampDbDateTime(ref strDate, ref strTime, SAPJOBTIMESTAMP.StockCheck))
                {
                    Log.Error("没有维护库存盘点的时间戳参数");
                    return;
                }
                importParameters.Add("I_CPUDT", strDate);
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveStockCheckData(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SaveStockCheckData(DataSet ds)
        {
            //将之前JOB未处理的记录状态置为"L"
            _InvoicesFacade.UpdateInvoicesFlag(MiddleTableFlag.Last, " ( 'PD' ) ");
            _InvoicesFacade.UpdateSapstoragecheckFlag(MiddleTableFlag.Last);

            #region Insert I_Sapstoragecheck by batch then commit

            if (ds != null && ds.Tables[SAPRfcOutTableName.StockCheck] != null && ds.Tables[SAPRfcOutTableName.StockCheck].Rows.Count > 0)
            {
                try
                {
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                    this.DataProvider.BeginTransaction();
                    foreach (DataRow row in ds.Tables[SAPRfcOutTableName.StockCheck].Rows)
                    {
                        I_Sapstoragecheck item = new I_Sapstoragecheck();
                        item.Invno = Convert.ToString(row["MBLNR"]);
                        //item.Voucherdate = Convert.ToInt32(row["BLDAT"]);//凭证日期

                        item.Remark = Convert.ToString(row["XBLNI"]);
                        item.Inventoryno = Convert.ToString(row["INVNU"]);
                        item.Invline = Convert.ToInt32(row["ZEILE"]);
                        item.SAPCDate = Convert.ToInt32(row["CPUDT"]);
                        item.SAPCTime = Convert.ToInt32(row["CPUTM"]);

                        item.Faccode = Convert.ToString(row["WERKS"]);
                        item.Storagecode = Convert.ToString(row["LGORT"]);
                        item.Type = Convert.ToString(row["BWART"]) == "701" ? StockCheckTypes.Gain : StockCheckTypes.Loss;
                        item.Mcode = Convert.ToString(row["MATNR"]);
                        item.Qty = Convert.ToDecimal(row["MENGE"]);
                        item.Unit = Convert.ToString(row["MEINS"]);
                        item.Mesflag = MiddleTableFlag.Wait;
                        item.Sdate = intDate;
                        item.Stime = intTime;
                        item.Pdate = intDate;
                        item.Ptime = intTime;

                        _InvoicesFacade.AddSapstoragecheck(item);
                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    throw ex;
                }
            }
            #endregion

            #region insert stock check into invoice table, commit by invno
            object[] arr = _InvoicesFacade.GetSapstoragecheckByFlag(MiddleTableFlag.Wait);
            //有记录才处理
            if (arr != null)
            {
                List<I_Sapstoragecheck> sapscList = Common.Array2SapstoragecheckList(arr);
                var scnoList = sapscList.GroupBy(p => p.Invno).Select(p => p.Key).ToList();
                foreach (var scno in scnoList)
                {
                    if (_InvoicesFacade.QueryInvoicesCount(scno) > 0)
                    {
                        continue;
                    }
                    List<I_Sapstoragecheck> currList = sapscList.Where(p => p.Invno == scno).ToList();
                    if (currList.Count > 0)
                    {
                        try
                        {
                            this.DataProvider.BeginTransaction();
                            //if (_InvoicesFacade.QueryInvoicesCount(scno) <= 0)
                            //{
                            int intDate, intTime;
                            Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                            Invoices head = new Invoices();
                            head.Invno = currList.First().Invno;
                            head.Voucherdate = currList.First().Voucherdate;
                            head.Remark1 = currList.First().Remark;
                            head.Inventoryno = currList.First().Inventoryno;
                            head.Invtype = InvoicesTypes.StockCheck;
                            head.Invstatus = CommonConstants.InvoicesInitSatus;
                            head.Pocreatedate = currList.First().SAPCDate;
                            head.Poupdatetime = currList.First().SAPCTime;

                            head.Finishflag = CommonConstants.InvoicesInitFinishFlag;
                            head.MaintainUser = CommonConstants.MaintainUser;
                            head.MaintainDate = intDate;
                            head.MaintainTime = intTime;

                            head.Cuser = CommonConstants.MaintainUser;
                            head.Cdate = intDate;
                            head.Ctime = intTime;
                            head.Eattribute1 = "W";
                            _InvoicesFacade.AddInvoices(head);
                            //}

                            int intDate1, intTime1;
                            Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                            List<Material> materialList = this.GetAllMaterialList();
                            foreach (var item in currList)
                            {
                                Invoicesdetail detail = new Invoicesdetail();
                                detail.Invno = item.Invno;
                                detail.Invline = item.Invline;
                                detail.Invlinestatus = CommonConstants.InvoicesInitSatus;
                                detail.Status = CommonConstants.InvoicesInitSatus;
                                detail.Faccode = item.Faccode;
                                detail.Storagecode = item.Storagecode;
                                detail.Type = item.Type;
                                detail.Mcode = item.Mcode;
                                detail.Planqty = item.Qty;
                                detail.Unit = item.Unit;
                                List<Material> maTempList = materialList.Where(m => m.Mcode == detail.Mcode).ToList();
                                if (maTempList.Count > 0)
                                {
                                    detail.Dqmcode = maTempList[0].Dqmcode;
                                }

                                detail.MaintainUser = CommonConstants.MaintainUser;
                                detail.MaintainDate = intDate1;
                                detail.MaintainTime = intTime1;
                                detail.Cuser = CommonConstants.MaintainUser;
                                detail.Cdate = intDate1;
                                detail.Ctime = intTime1;

                                _InvoicesFacade.AddInvoicesdetail(detail);
                            }

                            _InvoicesFacade.UpdateSapstoragecheckFlagBySCNO(scno, MiddleTableFlag.Success);
                            this.DataProvider.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            _InvoicesFacade.UpdateSapstoragecheckFlagBySCNO(scno, MiddleTableFlag.Fail);
                            throw ex;
                        }
                    }

                }
            }
            #endregion

            //将之前JOB未处理的记录状态置为"S"
            _InvoicesFacade.UpdateSapstoragecheckFlag(MiddleTableFlag.Success);

            #region 产生拣货任务令
            try
            {
                object[] arrInvoices = _InvoicesFacade.GetStockCheckInvoicesNoPick();
                //没有需要产生拣货任务令的库存盘亏凭证
                if (arrInvoices == null)
                {
                    return;
                }
                List<Invoices> invoicesList = Common.Array2InvoicesList(arrInvoices);
                string tmpPickno = default(string);

                this.DataProvider.BeginTransaction();
                foreach (var invoices in invoicesList)
                {
                    int intDate, intTime;
                    Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                    Pick pick = new Pick();
                    if (string.IsNullOrEmpty(tmpPickno))
                    {
                        pick.Pickno = _InvoicesFacade.GetPickNO();
                    }
                    else
                    {
                        string tmpStr = tmpPickno.Substring(10);
                        pick.Pickno = string.Format("{0}{1}", tmpPickno.Substring(0, 10), (int.Parse(tmpStr) + 1).ToString().PadLeft(4, '0'));
                    }
                    pick.Picktype = invoices.Invtype;
                    pick.Status = CommonConstants.PickInitSatus;
                    pick.Invno = invoices.Invno;
                    //pick.Faccode = ""; //在Stock Check行上
                    //pick.Storagecode = ""; //在Stock Check行上
                    pick.Remark1 = invoices.Remark1;

                    pick.MaintainUser = CommonConstants.MaintainUser;
                    pick.MaintainDate = intDate;
                    pick.MaintainTime = intTime;
                    pick.Cuser = CommonConstants.MaintainUser;
                    pick.Cdate = intDate;
                    pick.Ctime = intTime;

                    object[] arrInvoicesDetail = _InvoicesFacade.GetStockCheckInvoicesDetailBySCNO(invoices.Invno);
                    List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                    pick.Faccode = invoicesDetailList.First().Faccode;
                    try
                    {
                        pick.Storagecode = invoicesDetailList.First(o => string.IsNullOrEmpty(o.Storagecode) == false).Storagecode;
                    }
                    catch
                    {
                        pick.Storagecode = "";
                    }
                    _InvoicesFacade.AddPick(pick);

                    int intDate1, intTime1;
                    Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                    //按物料编码合并，同一盘点单盘亏出库的物料编码不会有多行
                    List<MaterialSum> msList = invoicesDetailList.GroupBy(d => d.Mcode, (key, group) => new MaterialSum { MCode = key, Qty = group.Sum(p => p.Planqty) }).ToList();
                    int lineNO = 1;
                    foreach (var ms in msList)
                    {
                        Pickdetail pickdetail = new Pickdetail();
                        Invoicesdetail firstInvDe = invoicesDetailList.First(i => i.Mcode == ms.MCode);
                        pickdetail.Pickno = pick.Pickno;
                        pickdetail.Pickline = lineNO.ToString();
                        pickdetail.Status = CommonConstants.PickDetailInitSatus;
                        pickdetail.Mcode = ms.MCode;
                        pickdetail.Dqmcode = firstInvDe.Dqmcode;
                        pickdetail.Qty = ms.Qty;
                        pickdetail.Unit = firstInvDe.Unit;

                        pickdetail.MaintainUser = CommonConstants.MaintainUser;
                        pickdetail.MaintainDate = intDate1;
                        pickdetail.MaintainTime = intTime1;
                        pickdetail.Cuser = CommonConstants.MaintainUser;
                        pickdetail.Cdate = intDate1;
                        pickdetail.Ctime = intTime1;

                        _InvoicesFacade.AddPickdetail(pickdetail);

                        lineNO += 1;
                    }

                    tmpPickno = pick.Pickno;

                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            #endregion

        }


        private void SyncSapLog()
        {

            //DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            //bool is2Sap = _InvoicesFacade.GetRecordCount(dbTime.DBDate, dbTime.DBTime) > 0;

            //if (is2Sap)
            //    return;

            int intDate, intTime;
            Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

            ReBackDnOutToSAP();
            ReBackDnInToSAP();


            Dictionary<string, string> po103Identity = null;
            RebackPo103ToSap(intDate, intTime, out  po103Identity);
            ReBackPo104ToSap(intDate, intTime, po103Identity);
            RebackPo105ToSap(intDate, intTime, po103Identity);
            RebackPo101ToSap(intDate, intTime);

            ReBackRSToSap(intDate, intTime);

            ReBackPGIToSap(intDate, intTime);

            ReBackWWPOToSap(intDate, intTime);

            ReBackUBToSap(intDate, intTime);

            ReBackSSToSap(intDate, intTime);

        }

        private void ReBackSSToSap(int intDate, int intTime)
        {
            object[] stockScrapLogList = _InvoicesFacade.GetAllStockScrap2Sap();
            if (stockScrapLogList == null || stockScrapLogList.Length == 0) return;

            Dictionary<string, List<StockScrap2Sap>> ssDic = new Dictionary<string, List<StockScrap2Sap>>();
            foreach (StockScrap2Sap ssLog in stockScrapLogList)
            {
                if (!ssDic.ContainsKey(ssLog.MESScrapNO))
                    ssDic.Add(ssLog.MESScrapNO, new List<StockScrap2Sap> { ssLog });
                else
                    ssDic[ssLog.MESScrapNO].Add(ssLog);

            }

            foreach (string pickNo in ssDic.Keys)
            {
                if (ssDic[pickNo].Count == 0) continue;
                List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap> ssList = new List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap>();
                foreach (StockScrap2Sap log in ssDic[pickNo])
                {
                    BenQGuru.eMES.SAPRFCService.Domain.StockScrap ss = new BenQGuru.eMES.SAPRFCService.Domain.StockScrap();
                    ss.CC = log.CC;
                    ss.FacCode = log.FacCode;
                    ss.DocumentDate = log.DocumentDate;
                    ss.MCode = log.MCode;
                    ss.MESScrapNO = log.MESScrapNO;
                    ss.Operator = log.Operator;
                    ss.Unit = log.Unit;
                    ss.ScrapCode = log.ScrapCode;
                    ss.Remark = log.Remark;
                    ss.StorageCode = log.StorageCode;
                    ss.Qty = log.Qty;
                    ssList.Add(ss);
                }

                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendStockScrapToSAP(ssList);
                foreach (StockScrap2Sap ssLog in ssDic[pickNo])
                {
                    ssLog.SapReturn = ret.Result;
                    ssLog.Message = ret.Message;
                    ssLog.MaintainUser = CommonConstants.MaintainUser;
                    ssLog.MaintainDate = intDate;
                    ssLog.MaintainTime = intTime;
                    _InvoicesFacade.UpdateStockScrap2Sap(ssLog);

                }

            }



        }

        private void ReBackWWPOToSap(int intDate, int intTime)
        {
            object[] wwpoLogs = _InvoicesFacade.GetAllWwpo2Sap();
            if (wwpoLogs == null || wwpoLogs.Length == 0) return;

            Dictionary<string, List<Wwpo2Sap>> wwpoDic = new Dictionary<string, List<Wwpo2Sap>>();
            foreach (Wwpo2Sap log in wwpoLogs)
            {
                if (!wwpoDic.ContainsKey(log.MesTransNO))
                    wwpoDic.Add(log.MesTransNO, new List<Wwpo2Sap> { log });
                else
                    wwpoDic[log.MesTransNO].Add(log);
            }
            foreach (string stno in wwpoDic.Keys)
            {
                if (wwpoDic[stno].Count == 0) continue;
                List<BenQGuru.eMES.SAPRFCService.Domain.WWPO> lists = new List<BenQGuru.eMES.SAPRFCService.Domain.WWPO>();
                foreach (Wwpo2Sap log in wwpoDic[stno])
                {
                    BenQGuru.eMES.SAPRFCService.Domain.WWPO wwPo = new BenQGuru.eMES.SAPRFCService.Domain.WWPO();

                    wwPo.Qty = log.Qty;
                    wwPo.PONO = log.PONO;
                    wwPo.POLine = log.POLine;
                    wwPo.Unit = log.Unit;
                    wwPo.InOutFlag = log.InOutFlag;
                    wwPo.FacCode = log.FacCode;//inv.FacCode;
                    wwPo.MCode = log.MCode;
                    wwPo.MesTransNO = log.MesTransNO;
                    wwPo.MesTransDate = log.MesTransDate;
                    wwPo.StorageCode = log.StorageCode;
                    wwPo.VendorCode = log.VendorCode;
                    lists.Add(wwPo);
                }
                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendWWPOToSAP(lists);
                foreach (Wwpo2Sap wwpoLog in wwpoDic[stno])
                {
                    if (ret != null)
                    {
                        wwpoLog.SapReturn = ret.Result;
                        wwpoLog.Message = ret.Message;
                        wwpoLog.MaintainUser = CommonConstants.MaintainUser;
                        wwpoLog.MaintainDate = intDate;
                        wwpoLog.MaintainTime = intTime;
                        _InvoicesFacade.UpdateWwpo2Sap(wwpoLog);
                    }
                }
            }
        }

        private void ReBackPGIToSap(int intDate, int intTime)
        {
            object[] dnInLogs = _InvoicesFacade.GetAllDnIn2Sap();
            if (dnInLogs == null || dnInLogs.Length == 0) return;
            Dictionary<string, List<Dn_in2Sap>> logDic = new Dictionary<string, List<Dn_in2Sap>>();
            foreach (Dn_in2Sap dn_inLog in dnInLogs)
            {
                if (!logDic.ContainsKey(dn_inLog.DNno))
                    logDic.Add(dn_inLog.DNno, new List<Dn_in2Sap> { dn_inLog });
                else
                    logDic[dn_inLog.DNno].Add(dn_inLog);

            }
            BenQGuru.eMES.SAPRFCService.DNToSAP dnToSAP = new DNToSAP(this.DataProvider);

            foreach (string invNo in logDic.Keys)
            {
                if (logDic[invNo].Count == 0) continue;
                List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                foreach (Dn_in2Sap log in logDic[invNo])
                {
                    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                    dn.DNNO = log.DNno;
                    dn.DNLine = 0;
                    dn.Unit = " ";
                    dn.BatchNO = " ";

                    BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = dnToSAP.DNRePGIToSAP(dn);
                    log.Message = ret.Message;
                    log.Sapreturn = ret.Result;
                    log.MaintainDate = intDate;
                    log.MaintainTime = intTime;
                    _InvoicesFacade.UpdateDn_in2Sap(log);
                }
            }
        }

        private void RebackPo101ToSap(int intDate, int intTime)
        {
            object[] po101Logs = _InvoicesFacade.GetAll101Po2Sap();
            if (po101Logs == null || po101Logs.Length == 0) return;
            Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> sapRets = ReBackPoToSapGroupInvno(po101Logs, null);
            foreach (Po2Sap log in po101Logs)
            {
                log.SapReturn = sapRets[log.PONO].Result;
                log.Message = sapRets[log.PONO].Message;
                log.SapDateStamp = intDate;
                log.SapTimeStamp = intTime;
                _InvoicesFacade.UpdatePo2Sap(log);
            }
        }

        private Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> ReBackPoToSapGroupInvno(object[] poLogList, Dictionary<string, string> sapIndentitys)
        {
            Dictionary<string, List<Po2Sap>> poDic = new Dictionary<string, List<Po2Sap>>();
            foreach (Po2Sap poLog in poLogList)
            {
                if (poDic.ContainsKey(poLog.PONO))
                    poDic[poLog.PONO].Add(poLog);
                else
                    poDic.Add(poLog.PONO, new List<Po2Sap> { poLog });
            }
            return ReBackPoToSap(poDic, sapIndentitys);

        }

        private void RebackPo105ToSap(int intDate, int intTime, Dictionary<string, string> po103IdentityGroup)
        {
            object[] poLogList = _InvoicesFacade.GetAll105Po2Sap();
            if (poLogList == null || poLogList.Length == 0) return;
            Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> sapRets = ReBackPoToSapGroupStNo(poLogList, po103IdentityGroup);
            foreach (Po2Sap poLog in poLogList)
            {
                poLog.SapReturn = sapRets[poLog.SerialNO].Result;
                poLog.Message = sapRets[poLog.SerialNO].Message;
                poLog.SapDateStamp = intDate;
                poLog.SapTimeStamp = intTime;
                poLog.SAPMaterialInvoice = sapRets[poLog.SerialNO].MaterialDocument;
                _InvoicesFacade.UpdatePo2Sap(poLog);
            }
        }

        private void ReBackPo104ToSap(int intDate, int intTime, Dictionary<string, string> identitys)
        {

            object[] po104List = _InvoicesFacade.GetAll104Po2Sap();

            if (po104List == null || po104List.Length == 0) return;
            Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> po104Rets = ReBackPoToSapGroupStNo(po104List, identitys);
            foreach (Po2Sap log in po104List)
            {
                log.Message = po104Rets[log.SerialNO].Message;
                log.SapReturn = po104Rets[log.SerialNO].Result;
                log.SapDateStamp = intDate;
                log.SapTimeStamp = intTime;
                _InvoicesFacade.UpdatePo2Sap(log);
            }
        }



        private void RebackPo103ToSap(int intDate, int intTime, out Dictionary<string, string> po103IdentityGroupByInvno)
        {

            object[] po103List = _InvoicesFacade.GetAll103Po2Sap();
            po103IdentityGroupByInvno = new Dictionary<string, string>();
            if (po103List == null || po103List.Length == 0) return;
            Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> returns = ReBackPoToSapGroupStNo(po103List, null);
            foreach (Po2Sap poLog in po103List)
            {
                if (!po103IdentityGroupByInvno.ContainsKey(poLog.SerialNO))
                    po103IdentityGroupByInvno.Add(poLog.STNO, returns[poLog.SerialNO].MaterialDocument);
                poLog.SapReturn = returns[poLog.SerialNO].Result;
                poLog.Message = returns[poLog.SerialNO].Message;
                poLog.SAPMaterialInvoice = returns[poLog.SerialNO].MaterialDocument;
                poLog.SapDateStamp = intDate;
                poLog.SapTimeStamp = intTime;
                _InvoicesFacade.UpdatePo2Sap(poLog);
            }
        }



        private Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> ReBackPoToSap(Dictionary<string, List<Po2Sap>> poDic, Dictionary<string, string> sapIndentitys)
        {
            Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> retDic = new Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn>();

            foreach (string groupName in poDic.Keys)
            {
                if (poDic[groupName].Count == 0) continue;
                List<BenQGuru.eMES.SAPRFCService.Domain.PO> poList = new List<BenQGuru.eMES.SAPRFCService.Domain.PO>();
                foreach (Po2Sap poLog in poDic[groupName])
                {
                    BenQGuru.eMES.SAPRFCService.Domain.PO po = new BenQGuru.eMES.SAPRFCService.Domain.PO();
                    po.Qty = poLog.Qty;
                    po.PONO = poLog.PONO;
                    po.POLine = poLog.POLine;
                    po.Unit = poLog.Unit;
                    po.FacCode = poLog.FacCode;
                    po.MCode = poLog.MCode;
                    po.Operator = poLog.Operator;
                    po.InvoiceDate = poLog.InvoiceDate;
                    po.Status = poLog.Status;
                    po.VendorInvoice = poLog.VendorInvoice;
                    po.StorageCode = poLog.StorageCode;
                    po.Remark = poLog.Remark;
                    po.ZNUMBER = poLog.ZNUMBER;
                    if (sapIndentitys != null && sapIndentitys.ContainsKey(groupName))
                        po.SAPMaterialInvoice = sapIndentitys[groupName];
                    else
                        po.SAPMaterialInvoice = _InvoicesFacade.GetIdentityFromPo103Log(poLog.STNO, "103");

                    poList.Add(po);
                }
                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendPoToSAP(poList);
                retDic.Add(groupName, ret);

            }

            return retDic;
        }


        private Dictionary<string, BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn> ReBackPoToSapGroupStNo(object[] poLogList, Dictionary<string, string> sapIndentitys)
        {
            Dictionary<string, List<Po2Sap>> poDic = new Dictionary<string, List<Po2Sap>>();
            foreach (Po2Sap poLog in poLogList)
            {
                if (poDic.ContainsKey(poLog.SerialNO))
                    poDic[poLog.SerialNO].Add(poLog);
                else
                    poDic.Add(poLog.SerialNO, new List<Po2Sap> { poLog });
            }
            return ReBackPoToSap(poDic, sapIndentitys);

        }

        private void ReBackDnInToSAP()
        {
            object[] dnLogList = _InvoicesFacade.GetAllDn2SapIn();

            if (dnLogList != null)
            {
                Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>> dnsOk = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>>();

                foreach (DN2Sap dnLog in dnLogList)
                {
                    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                    dn.DNNO = dnLog.DNNO;
                    dn.DNLine = dnLog.DNLine;
                    dn.Unit = dnLog.Unit;
                    dn.BatchNO = dnLog.BatchNO;
                    dn.Qty = dnLog.Qty;


                    if (dnsOk.ContainsKey(dnLog.DNNO))
                        dnsOk[dnLog.DNNO].Add(dn);
                    else
                    {
                        dnsOk[dnLog.DNNO] = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                        dnsOk[dnLog.DNNO].Add(dn);
                    }

                }

                foreach (string key in dnsOk.Keys)
                {

                    if (dnsOk[key].Count > 0)
                    {
                        BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendDNToSap(dnsOk[key], true);
                        if (ret != null)
                        {
                            string Result = Getstring(ret.Result);
                            string Message = Getstring(ret.Message);
                            _InvoicesFacade.UpdateDN2Sap(key, Message, Result);
                        }
                    }
                }

            }
        }

        private void ReBackDnOutToSAP()
        {
            #region dnLogList
            object[] dnLogList = _InvoicesFacade.GetAllDn2SapOut();
            if (dnLogList != null)
            {
                Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>> dnsOk = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>>();
                Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>> dnsBad = new Dictionary<string, List<BenQGuru.eMES.SAPRFCService.Domain.DN>>();


                foreach (DN2Sap dnLog in dnLogList)
                {
                    BenQGuru.eMES.SAPRFCService.Domain.DN dn = new BenQGuru.eMES.SAPRFCService.Domain.DN();
                    dn.DNNO = dnLog.DNNO;
                    dn.DNLine = dnLog.DNLine;
                    dn.Unit = dnLog.Unit;
                    dn.BatchNO = dnLog.BatchNO;
                    dn.Qty = dnLog.Qty;

                    if (dnLog.IsAll == "Y")
                    {
                        if (dnsOk.ContainsKey(dnLog.DNNO))
                            dnsOk[dnLog.DNNO].Add(dn);
                        else
                        {
                            dnsOk[dnLog.DNNO] = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                            dnsOk[dnLog.DNNO].Add(dn);
                        }
                    }
                    else if (dnLog.IsAll == "N")
                    {
                        if (dnsBad.ContainsKey(dnLog.DNNO))
                            dnsBad[dnLog.DNNO].Add(dn);
                        else
                        {
                            dnsBad[dnLog.DNNO] = new List<BenQGuru.eMES.SAPRFCService.Domain.DN>();
                            dnsBad[dnLog.DNNO].Add(dn);
                        }
                    }
                }
                foreach (string key in dnsBad.Keys)
                {
                    if (dnsBad[key].Count > 0)
                    {
                        BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendDNToSap(dnsBad[key], false);
                        if (ret != null)
                        {
                            string Result = Getstring(ret.Result);
                            string Message = Getstring(ret.Message);
                            _InvoicesFacade.UpdateDN2Sap(key, Message, Result);
                        }
                    }
                }
                foreach (string key in dnsOk.Keys)
                {

                    if (dnsOk[key].Count > 0)
                    {
                        BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendDNToSap(dnsOk[key], true);
                        if (ret != null)
                        {
                            string Result = Getstring(ret.Result);
                            string Message = Getstring(ret.Message);
                            _InvoicesFacade.UpdateDN2Sap(key, Message, Result);
                        }
                    }
                }

            }
            #endregion
        }

        private void ReBackRSToSap(int intDate, int intTime)
        {
            object[] rsLogList = _InvoicesFacade.GetAllRs2Sap();
            if (rsLogList == null || rsLogList.Length == 0) return;

            Dictionary<string, List<Rs2Sap>> rsDicGroupByInvno = new Dictionary<string, List<Rs2Sap>>();

            foreach (Rs2Sap rsLog in rsLogList)
            {
                if (!rsDicGroupByInvno.ContainsKey(rsLog.RSNO))
                    rsDicGroupByInvno.Add(rsLog.RSNO, new List<Rs2Sap> { rsLog });
                else
                    rsDicGroupByInvno[rsLog.RSNO].Add(rsLog);
            }

            foreach (string invno in rsDicGroupByInvno.Keys)
            {
                List<Rs2Sap> rsLogs = rsDicGroupByInvno[invno];

                if (rsLogs.Count == 0) continue;
                List<BenQGuru.eMES.SAPRFCService.Domain.RS> rsList = new List<BenQGuru.eMES.SAPRFCService.Domain.RS>();
                foreach (Rs2Sap rsLog in rsLogs)
                {
                    BenQGuru.eMES.SAPRFCService.Domain.RS rs = new BenQGuru.eMES.SAPRFCService.Domain.RS();
                    rs.Qty = rsLog.Qty;
                    rs.RSNO = rsLog.RSNO;
                    rs.RSLine = rsLog.RSLine;
                    rs.Unit = rsLog.Unit;
                    rs.DocumentDate = rsLog.DocumentDate;
                    rs.FacCode = rsLog.FacCode;
                    rs.InOutFlag = rsLog.InOutFlag;
                    rs.MCode = rsLog.MCode;
                    rs.MesTransNO = rsLog.MesTransNO;
                    rs.StorageCode = rsLog.StorageCode;
                    rsList.Add(rs);
                }

                BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendRSToSAP(rsList);
                foreach (Rs2Sap rsLog in rsLogs)
                {
                    rsLog.SapReturn = Getstring(ret.Result);
                    rsLog.Message = Getstring(ret.Message);
                    rsLog.MaintainUser = CommonConstants.MaintainUser;
                    rsLog.MaintainDate = intDate;
                    rsLog.MaintainTime = intTime;
                    _InvoicesFacade.UpdateRs2Sap(rsLog);
                }
            }
        }

        private void ReBackUBToSap(int intDate, int intTime)
        {
            object[] ubLogList = _InvoicesFacade.GetAllUb2Sap();
            if (ubLogList == null || ubLogList.Length == 0)
                return;
            Dictionary<string, List<Ub2Sap>> invNoToUb = new Dictionary<string, List<Ub2Sap>>();

            foreach (Ub2Sap ubLog in ubLogList)
            {
                if (!invNoToUb.ContainsKey(ubLog.UBNO))
                    invNoToUb.Add(ubLog.UBNO, new List<Ub2Sap> { ubLog });
                else
                    invNoToUb[ubLog.UBNO].Add(ubLog);
            }

            foreach (string invno in invNoToUb.Keys)
            {
                List<Ub2Sap> ub351Logs = invNoToUb[invno].Where(i => i.InOutFlag == "351").ToList();
                List<Ub2Sap> ub101Logs = invNoToUb[invno].Where(i => i.InOutFlag == "101").ToList();

                ReBackUB351ToSap(intDate, intTime, ub351Logs);
                ReBackUB101ToSap(intDate, intTime, ub101Logs);
            }

        }

        private void ReBackUB101ToSap(int intDate, int intTime, List<Ub2Sap> ub101Logs)
        {
            if (ub101Logs.Count == 0) return;
            List<BenQGuru.eMES.SAPRFCService.Domain.UB> ubList = ToUB(ub101Logs);
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendUBToSAP(ubList);
            foreach (Ub2Sap log in ub101Logs)
            {
                log.SapReturn = ret.Result;
                log.Message = ret.Message;
                log.MaintainUser = CommonConstants.MaintainUser;
                log.MaintainDate = intDate;
                log.MaintainTime = intTime;
                _InvoicesFacade.UpdateUb2Sap(log);

            }

        }

        private void ReBackUB351ToSap(int intDate, int intTime, List<Ub2Sap> ub351Logs)
        {
            if (ub351Logs.Count == 0) return;
            List<BenQGuru.eMES.SAPRFCService.Domain.UB> ubList = ToUB(ub351Logs);
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn ret = SendUBToSAP(ubList);

            foreach (Ub2Sap log in ub351Logs)
            {
                log.SapReturn = ret.Result;
                log.Message = ret.Message;
                log.MaintainUser = CommonConstants.MaintainUser;
                log.MaintainDate = intDate;
                log.MaintainTime = intTime;
                _InvoicesFacade.UpdateUb2Sap(log);

            }

        }

        private static List<BenQGuru.eMES.SAPRFCService.Domain.UB> ToUB(List<Ub2Sap> ub351Logs)
        {
            List<BenQGuru.eMES.SAPRFCService.Domain.UB> ubList = new List<BenQGuru.eMES.SAPRFCService.Domain.UB>();
            foreach (Ub2Sap ubLog in ub351Logs)
            {
                BenQGuru.eMES.SAPRFCService.Domain.UB ub = new BenQGuru.eMES.SAPRFCService.Domain.UB();
                ub.Qty = ubLog.Qty;
                ub.UBNO = ubLog.UBNO;
                ub.Unit = ubLog.Unit;
                ub.UBLine = ubLog.UBLine;
                ub.InOutFlag = ubLog.InOutFlag;
                ub.MCode = ubLog.MCode;
                ub.ContactUser = ubLog.ContactUser;
                ub.DocumentDate = ubLog.DocumentDate;
                ub.FacCode = ubLog.FacCode;
                ub.MesTransNO = ubLog.MesTransNO;
                ub.StorageCode = ubLog.StorageCode;
                ubList.Add(ub);

            }
            return ubList;
        }


        #region ToMail

        //private void SyncSendEmail()
        //{
        //    AlertMailSetting[] settings = _InvoicesFacade.QueryAlertMailSetting();
        //    DateTime now = _InvoicesFacade.timeNow();
        //    foreach (AlertMailSetting setting in settings)
        //    {
        //        if (setting.ItemSequence == "到货初检拒收通知")
        //        {
        //            string bodyhtml = "邮件内容:";
        //            object[] asnlist = _InvoicesFacade.GetASNReject(now, -15);
        //            if (asnlist != null)
        //            {
        //                foreach (AsndetailEX asn in asnlist)
        //                {
        //                    bodyhtml += string.Format(@"PO号 {0}  ASN号 {1}  鼎桥物料号 {2} 物料号 {3} 在初检被拒收{4} 箱，数量 {5} 。", asn.Invno,
        //                        asn.Stno, asn.DqmCode, asn.MCode, asn.InitRejectQty, asn.Qty);
        //                    bodyhtml += "\r\n";
        //                }
        //            }

        //            SendEmail(setting.ItemSequence, bodyhtml, setting.Recipients);
        //        }
        //        else if (setting.ItemSequence == "SQE拒收通知")
        //        {
        //            string bodyhtml = "邮件内容:";
        //            object[] asnlist = _InvoicesFacade.GetSQERejects(now, -15);
        //            if (asnlist != null)
        //            {
        //                foreach (SQEReject asn in asnlist)
        //                {
        //                    bodyhtml += string.Format(@"IQC检验单号:{0}，ASN号:{1}，SAP单号:{2}，送检日期:{3}，送检数量:{4}，缺陷品数:{5}，缺陷描述:{6}、退换货数量:{7}，现场整改数量:{8}，让步接收数量:{9}，特采数量:{10} 。",
        //                        asn.IQCNO,
        //                        asn.STNO,
        //                        asn.INVNO,
        //                        FormatHelper.ToDateString(asn.CDATE),
        //                        asn.QTY,
        //                        asn.NGQTY,
        //                       string.Empty,
        //                       asn.ReturnQTY,
        //                       asn.ReformQTY,
        //                       asn.GiveQTY,
        //                       asn.AcceptQTY

        //                        );
        //                    bodyhtml += "\r\n";
        //                }
        //            }

        //            SendEmail(setting.ItemSequence, bodyhtml, setting.Recipients);

        //        }
        //    }
        //}
        private void YFRExpiredScan()
        {
            DateTime now = DateTime.Now;
            Asn[] asns = _InvoicesFacade.QueryNoFinishYFRAsns();
            List<Asn> YFRExpiredList = new List<Asn>();
            foreach (Asn asn in asns)
            {
                if (asn.Predict_Date == 0)
                    continue;
                DateTime predictDate = FormatHelper.ToDateTime(asn.Predict_Date);
                TimeSpan span = now - predictDate;
                if (span.Days >= 15)
                {
                    YFRExpiredList.Add(asn);
                }
            }

            if (YFRExpiredList.Count == 0)
                return;
            StringBuilder sb = new StringBuilder(2000);
            string template = "预留单号:{0}，状态:{1}，创建日期:{2}，物料编码:{3}，描述:{4}，数量:{5}，希望到货日期:{0}。\r\n";
            for (int i = 0; i < YFRExpiredList.Count; i++)
            {
                Asn asn = YFRExpiredList[i];
                BenQGuru.eMES.InterfaceFacade.Asndetail[] details = _InvoicesFacade.QueryAsnDetailsSummaryYFR(asn.Stno);

                foreach (BenQGuru.eMES.InterfaceFacade.Asndetail detail in details)
                {
                    Material m = _InvoicesFacade.GetMaterialFromDQMCode(detail.DqmCode);
                    sb.Append(string.Format(template, asn.Invno, asn.Status, FormatHelper.ToDateString(detail.CDate), detail.DqmCode, m.Mchshortdesc, detail.Qty, FormatHelper.ToDateString(asn.Predict_Date)));
                }
            }

            string mailBody = sb.ToString();
            if (mailBody.Length > 0)
            {
                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(now);
                mail.CTIME = FormatHelper.TOTimeInt(now);
                mail.CUSER = "JOB";
                mail.MAILCONTENT = mailBody;
                mail.MAILTYPE = "研发入库超期未完成入库预警";
                mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                _InvoicesFacade.AddSendMail(mail);
            }
        }

        private void ReceiveExpiredScan()
        {
            DateTime dateNow = DateTime.Now;
            Asn[] asns = _InvoicesFacade.QueryNoReceiveAsns();
            List<Asn> expiredAsnList = new List<Asn>();
            List<int> expiredDays = new List<int>();
            foreach (Asn asn in asns)
            {
                if (asn.Predict_Date == 0)
                    continue;
                DateTime predictDate = FormatHelper.ToDateTime(asn.Predict_Date);

                TimeSpan span = dateNow - predictDate;

                if (span.Days >= 1)
                {
                    expiredAsnList.Add(asn);
                    expiredDays.Add(span.Days);
                }
            }
            if (expiredAsnList.Count == 0)
                return;
            StringBuilder sb = new StringBuilder(2000);
            string template = @"ASN单号:{0}，SAP单据号:{1}，状态:{2}，入库指令创建时间:{3}，预计到货日期:{4}，箱单出具日期:{5}，超期天数:{6}。\r\n";
            for (int i = 0; i < expiredAsnList.Count; i++)
            {
                Asn asn = expiredAsnList[i];
                sb.Append(string.Format(template, asn.Stno, asn.Invno, asn.Status, FormatHelper.TODateTimeString(asn.CDate, asn.CTime), FormatHelper.ToDateString(asn.Predict_Date), FormatHelper.ToDateString(_InvoicesFacade.GetAsnDetailCDate(asn.Stno)), expiredDays[i]));
            }
            string mailBody = sb.ToString();
            if (mailBody.Length > 0)
            {
                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(dateNow);
                mail.CTIME = FormatHelper.TOTimeInt(dateNow);
                mail.CUSER = "JOB";
                mail.MAILCONTENT = mailBody;
                mail.MAILTYPE = ShareLib.ShareKit.MailName.ReceiveExpiredMail;
                mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                _InvoicesFacade.AddSendMail(mail);
            }

        }

        private void QCExpiredScan()
        {
            DateTime dateNow = DateTime.Now;
            List<OQC> oqcExpireList = GetExpiredOQCS(dateNow);
            List<AsnIQC> iqcExpiredList = GetExpiredIQCs(dateNow);
            List<OQCDetailEC> oqcNoSQEList = GetExpiredOQCSQEs(dateNow);
            List<AsnIQCDetailEc> iqcNoSQEList = GetExpiredIQCSQEs(dateNow);



            List<SendMail> mails = new List<SendMail>();
            mails.AddRange(GenOQCExpireMails(oqcExpireList));
            mails.AddRange(GenIQCExpiredMails(iqcExpiredList, mails));
            mails.AddRange(GenOQCSQEExpiredMails(oqcNoSQEList));
            mails.AddRange(GenIQCSQEExpiredMails(iqcNoSQEList));

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (SendMail mail in mails)
                {
                    _InvoicesFacade.AddSendMail(mail);

                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;

            }


        }

        private List<AsnIQCDetailEc> GetExpiredIQCSQEs(DateTime dateNow)
        {
            AsnIQCDetailEc[] iqcNoSQEs = _InvoicesFacade.QueryIQCNoHandleEcs();
            List<AsnIQCDetailEc> iqcNoSQEList = new List<AsnIQCDetailEc>();
            foreach (AsnIQCDetailEc ec in iqcNoSQEs)
            {
                DateTime cdateTime = FormatHelper.ToDateTime(ec.CDate, ec.CTime);
                TimeSpan span = dateNow - cdateTime;
                if (span.Days >= 3)
                    iqcNoSQEList.Add(ec);
            }
            return iqcNoSQEList;
        }

        private List<SendMail> GenIQCSQEExpiredMails(List<AsnIQCDetailEc> iqcNoSQEList)
        {
            StringBuilder sb = new StringBuilder(1000);
            List<string> iqcNos = new List<string>();
            List<SendMail> localMails = new List<SendMail>();
            foreach (AsnIQCDetailEc ec in iqcNoSQEList)
            {
                if (!iqcNos.Contains(ec.IqcNo))
                {
                    AsnIQC iqc = (AsnIQC)_InvoicesFacade.GetAsnIQC(ec.IqcNo);
                    Material m = _InvoicesFacade.GetMaterialFromDQMCode(iqc.DQMCode);
                    Asn asn = (Asn)_InvoicesFacade.GetASN(iqc.StNo);
                    sb.Append(string.Format(@"订单号:{0}、IQC单号:{1}、鼎桥物料编码:{2}、鼎桥物料描述:{3}、供应商名称:{4}、物料数量:{5} 。 \r\n", ec.StNo, ec.IqcNo, iqc.DQMCode, m.Mchshortdesc, asn.VendorName, iqc.Qty));
                }
            }

            List<SendMail> mails = ToMailBlocks(sb, ShareLib.ShareKit.MailName.IQC_SQEExpiredMail);
            return mails;

        }

        private List<OQCDetailEC> GetExpiredOQCSQEs(DateTime dateNow)
        {
            OQCDetailEC[] oqcNoSQEs = _InvoicesFacade.QueryOQCNoHandleEcs();
            List<OQCDetailEC> oqcNoSQEList = new List<OQCDetailEC>();
            foreach (OQCDetailEC ec in oqcNoSQEs)
            {
                DateTime cdateTime = FormatHelper.ToDateTime(ec.CDate, ec.CTime);
                TimeSpan span = dateNow - cdateTime;
                if (span.Days >= 3)
                    oqcNoSQEList.Add(ec);
            }
            return oqcNoSQEList;
        }

        private List<SendMail> GenOQCSQEExpiredMails(List<OQCDetailEC> oqcNoSQEList)
        {
            List<SendMail> localMails = new List<SendMail>();
            Dictionary<string, List<OQCDetailEC>> ecDic = new Dictionary<string, List<OQCDetailEC>>();
            foreach (OQCDetailEC ec in oqcNoSQEList)
            {
                if (ecDic.ContainsKey(ec.DQMCode))
                    ecDic[ec.DQMCode].Add(ec);
                else
                {
                    ecDic.Add(ec.DQMCode, new List<OQCDetailEC>());
                    ecDic[ec.DQMCode].Add(ec);
                }
            }
            StringBuilder sb = new StringBuilder(1000);
            foreach (string dqmCode in ecDic.Keys)
            {
                List<string> oqcNos = new List<string>();

                foreach (OQCDetailEC ec1 in ecDic[dqmCode])
                {
                    if (!oqcNos.Contains(ec1.OqcNo))
                    {
                        Material m = _InvoicesFacade.GetMaterialFromDQMCode(dqmCode);
                        string pickNo = ecDic[dqmCode][0].PickNo;
                        OQCDetail oqcdetail = _InvoicesFacade.GetOQCDetailSummary(ec1.OqcNo, dqmCode);
                        sb.Append(string.Format(@"订单号:{0}、OQC单号:{1}、鼎桥物料编码:{2}、鼎桥物料描述:{3}、供应商名称:{4}、物料数量:{5} 。 \r\n", pickNo, ec1.OqcNo, dqmCode, m.Mchshortdesc, string.Empty, oqcdetail.Qty));
                        oqcNos.Add(ec1.OqcNo);
                    }
                }



            }
            string mailBody = sb.ToString();
            if (!string.IsNullOrEmpty(mailBody))
            {
                SendMail mail = new SendMail();
                mail.MAILCONTENT = mailBody;
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = "JOB";
                mail.MAILTYPE = ShareKit.MailName.OQCExpiredMail;
                mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                localMails.Add(mail);
            }
            return localMails;
        }

        private List<SendMail> GenIQCExpiredMails(List<AsnIQC> iqcExpiredList, List<SendMail> mails)
        {
            List<SendMail> localMails = new List<SendMail>();
            StringBuilder sb = new StringBuilder(1000);
            foreach (AsnIQC iqc in iqcExpiredList)
            {
                Asn asn = (Asn)_InvoicesFacade.GetASN(iqc.StNo);
                Material m = _InvoicesFacade.GetMaterialFromDQMCode(iqc.DQMCode);
                sb.Append(string.Format(@"订单号:{0}、IQC单号:{1}、鼎桥物料编码:{2}、鼎桥物料描述:{3}、供应商名称:{4}、物料数量:{5} 。 \r\n", iqc.StNo, iqc.IqcNo, iqc.DQMCode, m.Mchshortdesc, asn.VendorName, iqc.Qty));

            }
            string mailBody = sb.ToString();
            if (!string.IsNullOrEmpty(mailBody))
            {
                SendMail mail = new SendMail();
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = "JOB";
                mail.MAILCONTENT = mailBody;
                mail.MAILTYPE = ShareLib.ShareKit.MailName.IQCExpiredMail;
                mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                localMails.Add(mail);
            }
            return localMails;
        }

        private List<AsnIQC> GetExpiredIQCs(DateTime dateNow)
        {
            AsnIQC[] iqcs = _InvoicesFacade.QueryIQCReleases();
            List<AsnIQC> iqcExpireList = new List<AsnIQC>();
            foreach (AsnIQC iqc in iqcs)
            {
                DateTime cdateTime = FormatHelper.ToDateTime(iqc.CDate, iqc.CTime);
                TimeSpan span = dateNow - cdateTime;
                if (span.Days >= 3)
                    iqcExpireList.Add(iqc);
            }
            return iqcExpireList;
        }

        private List<OQC> GetExpiredOQCS(DateTime dateNow)
        {
            OQC[] oqcs = _InvoicesFacade.QueryOQCReleases();
            List<OQC> oqcExpireList = new List<OQC>();
            foreach (OQC oqc in oqcs)
            {
                DateTime cdateTime = FormatHelper.ToDateTime(oqc.CDate, oqc.CTime);
                TimeSpan span = dateNow - cdateTime;
                if (span.Days >= 3)
                    oqcExpireList.Add(oqc);
            }
            return oqcExpireList;
        }

        private List<SendMail> GenOQCExpireMails(List<OQC> oqcExpireList)
        {
            List<SendMail> localMails = new List<SendMail>();
            if (oqcExpireList.Count > 0)
            {
                StringBuilder sb = new StringBuilder(1000);
                foreach (OQC oqc in oqcExpireList)
                {
                    OQCDetail[] details = _InvoicesFacade.GetOQCDetails(oqc.OqcNo);

                    if (details.Length > 0)
                    {
                        foreach (OQCDetail detail in details)
                        {
                            Material m = _InvoicesFacade.GetMaterialFromDQMCode(detail.DQMCode);
                            sb.Append(string.Format(@"订单号:{0}、OQC单号:{1}、鼎桥物料编码:{2}、鼎桥物料描述:{3}、供应商名称:{4}、物料数量:{5} 。 \r\n", oqc.PickNo, oqc.OqcNo, detail.DQMCode, m.Mchshortdesc, string.Empty, detail.Qty));
                        }
                    }
                }


                List<SendMail> mails = ToMailBlocks(sb, ShareLib.ShareKit.MailName.OQCExpiredMail);
                localMails.AddRange(mails);
            }
            return localMails;
        }

        private void SapClosingScan()
        {
            DateTime now = DateTime.Now;
            List<ClosingSapMailClass> mailList = new List<ClosingSapMailClass>();

            DN2Sap[] dn2saps = _InvoicesFacade.QueryDN2Sap(now);
            foreach (DN2Sap dn2sap in dn2saps)
                mailList.Add(new ClosingSapMailClass { Interface = "DN2Sap", Invno = dn2sap.DNNO, LogID = dn2sap.MaintainUser, Message = dn2sap.Message });

            Po2Sap[] po2Arr = _InvoicesFacade.QueryPO2Saps(now);
            foreach (Po2Sap po2 in po2Arr)
                mailList.Add(new ClosingSapMailClass { Interface = "PO2Sap", Invno = po2.PONO, LogID = po2.Operator, Message = po2.Message });

            Rs2Sap[] rs2Arr = _InvoicesFacade.QueryRS2Saps(now);
            foreach (Rs2Sap rs2 in rs2Arr)
                mailList.Add(new ClosingSapMailClass { Interface = "RS2Sap", Invno = rs2.RSNO, LogID = rs2.MaintainUser, Message = rs2.Message });

            Dn_in2Sap[] dnInArr = _InvoicesFacade.QueryDN_IN2Saps(now);
            foreach (Dn_in2Sap dnIn in dnInArr)
                mailList.Add(new ClosingSapMailClass { Interface = "DN_IN2Sap", Invno = dnIn.DNno, LogID = dnIn.MaintainUser, Message = dnIn.Message });

            Wwpo2Sap[] wwpoArr = _InvoicesFacade.QueryWWPO2Sap(now);
            foreach (Wwpo2Sap wwpo in wwpoArr)
                mailList.Add(new ClosingSapMailClass { Interface = "WWPO2SAP", Invno = wwpo.PONO, LogID = wwpo.MaintainUser, Message = wwpo.Message });

            Ub2Sap[] ubArr = _InvoicesFacade.QueryUB2Sap(now);
            foreach (Ub2Sap ub in ubArr)
                mailList.Add(new ClosingSapMailClass { Interface = "UB2SAP", Invno = ub.UBNO, LogID = ub.MaintainUser, Message = ub.Message });

            StockScrap2Sap[] ssArr = _InvoicesFacade.QueryStockScrap2Sap(now);
            foreach (StockScrap2Sap ss in ssArr)
                mailList.Add(new ClosingSapMailClass { Interface = "StockScrap2Sap", Invno = ss.MESScrapNO, LogID = ss.MaintainUser, Message = ss.Message });
            StringBuilder sb = new StringBuilder(2000);
            string template = "LogId:{0}、错误接口:{1}、错误单据号:{2}、错误消息:{3}。\r\n";
            foreach (ClosingSapMailClass mailClass in mailList)
                sb.Append(string.Format(template, mailClass.LogID, mailClass.Interface, mailClass.Invno, mailClass.Message));
            if (sb.Length > 0)
            {
                SendMail mail = new SendMail();
                mail.MAILCONTENT = sb.ToString();
                mail.MAILTYPE = ShareLib.ShareKit.MailName.SapClosingErrorMail;
                mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                mail.SENDFLAG = "N";
                mail.CDATE = FormatHelper.TODateInt(DateTime.Now);
                mail.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
                mail.CUSER = "JOB";
                _InvoicesFacade.AddSendMail(mail);
            }
        }


        private void MCodeValidityScan()
        {
            DateTime now = DateTime.Now;
            StorageDetailMe[] sds = _InvoicesFacade.QueryStorageDetails();
            List<StorageDetailMe> _1In2List = new List<StorageDetailMe>();
            List<StorageDetailMe> _3In4List = new List<StorageDetailMe>();
            List<StorageDetailMe> _4In4List = new List<StorageDetailMe>();
            foreach (StorageDetailMe d in sds)
            {
                if (d.LastStorageAgeDate == 0)
                    continue;
                if (d.VALIDITY == 0)
                    continue;
                DateTime lastD = FormatHelper.ToDateTime(d.LastStorageAgeDate);
                TimeSpan span = now - lastD;

                double percent = span.Days / d.VALIDITY;
                if (percent >= 0.5)
                    _1In2List.Add(d);
                else if (percent >= 0.75)
                    _3In4List.Add(d);
                else if (percent >= 1)
                    _4In4List.Add(d);
            }
            StringBuilder _1In2ListBuilder = new StringBuilder(2000);
            StringBuilder _3In4ListBuilder = new StringBuilder(2000);
            StringBuilder _4In4ListBuilder = new StringBuilder(2000);
            string template = "库位:{0},货位:{1},箱号:{2},鼎桥物料:{3},超期次数:{4},超期数量:{5}。\r\n";
            foreach (BenQGuru.eMES.InterfaceFacade.StorageDetailMe s in _1In2List)
                _1In2ListBuilder.Append(string.Format(template, s.StorageCode, s.LocationCode, s.CartonNo, s.DQMCode, 1, s.StorageQty));
            foreach (BenQGuru.eMES.InterfaceFacade.StorageDetailMe s in _3In4List)
                _3In4ListBuilder.Append(string.Format(template, s.StorageCode, s.LocationCode, s.CartonNo, s.DQMCode, 2, s.StorageQty));
            foreach (BenQGuru.eMES.InterfaceFacade.StorageDetailMe s in _4In4List)
                _4In4ListBuilder.Append(string.Format(template, s.StorageCode, s.LocationCode, s.CartonNo, s.DQMCode, 3, s.StorageQty));
            try
            {
                this.DataProvider.BeginTransaction();
                if (_1In2ListBuilder.Length > 0)
                {
                    SendMail mail = new SendMail();
                    mail.CDATE = FormatHelper.TODateInt(now);
                    mail.CTIME = FormatHelper.TOTimeInt(now);
                    mail.CUSER = "JOB";
                    mail.MAILCONTENT = _1In2ListBuilder.ToString();
                    mail.MAILTYPE = ShareLib.ShareKit.MailName.MaterialExpiredMail;
                    mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                    mail.SENDFLAG = "N";
                    _InvoicesFacade.AddSendMail(mail);
                }
                if (_3In4ListBuilder.Length > 0)
                {
                    SendMail mail = new SendMail();
                    mail.CDATE = FormatHelper.TODateInt(now);
                    mail.CTIME = FormatHelper.TOTimeInt(now);
                    mail.CUSER = "JOB";
                    mail.MAILCONTENT = _3In4ListBuilder.ToString();
                    mail.MAILTYPE = ShareLib.ShareKit.MailName.MaterialExpiredMail;
                    mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                    mail.SENDFLAG = "N";
                    _InvoicesFacade.AddSendMail(mail);
                }
                if (_4In4ListBuilder.Length > 0)
                {
                    SendMail mail = new SendMail();
                    mail.CDATE = FormatHelper.TODateInt(now);
                    mail.CTIME = FormatHelper.TOTimeInt(now);
                    mail.CUSER = "JOB";
                    mail.MAILCONTENT = _4In4ListBuilder.ToString();
                    mail.MAILTYPE = ShareLib.ShareKit.MailName.MaterialExpiredMail;
                    mail.Recipients = _InvoicesFacade.Recipients(mail.MAILTYPE);
                    mail.SENDFLAG = "N";
                    _InvoicesFacade.AddSendMail(mail);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        private void SendMail2(string mailName)
        {


            SendMail m = new BenQGuru.eMES.InterfaceFacade.SendMail();
            m.CDATE = FormatHelper.TODateInt(DateTime.Now);
            m.CTIME = FormatHelper.TOTimeInt(DateTime.Now);
            m.CUSER = "JOB";
            m.MAILTYPE = mailName;
            //m.MAILCONTENT = @"1\r\n,2\r\n,3\r\n4\r\n5\r6\n\r\n";

            m.MAILCONTENT = @"1\r\n,2\r\n,3\r\n4\r\n5\r6\n\r\n";

            //List<string> parts = new List<string>();
            //while (m.MAILCONTENT.Length > 1)
            //{
            //    parts.Add(m.MAILCONTENT.Substring(0, 1));
            //    m.MAILCONTENT = m.MAILCONTENT.Substring(1);

            //}
            //if (m.MAILCONTENT.Length == 1 && m.MAILCONTENT.Length > 0)
            //{
            //    parts.Add(m.MAILCONTENT);
            //}
            //StringBuilder sb = new StringBuilder(100);
            //foreach (string part in parts)
            //    sb.Append(part);

            //m.MAILCONTENT = sb.ToString();
            m.Recipients = "498351584@qq.com,1743198929@qq.com";
            m.SENDFLAG = "N";

            //SendEmailInternal(m.MAILTYPE, m.MAILCONTENT.Replace(@"\r\n", "\r\n"), m.Recipients);
            SendEmailInternal(m.MAILTYPE, m.MAILCONTENT, m.Recipients);
        }


        private void SendMail(string mailName)
        {
            SendMail[] mails = _InvoicesFacade.GetPendingMails(mailName);

            foreach (SendMail mail in mails)
                SendEmailInternal(mailName, mail.MAILCONTENT, mail.Recipients);

            try
            {
                this.DataProvider.BeginTransaction();

                foreach (SendMail mail in mails)
                {
                    if (string.IsNullOrEmpty(mail.BLOCKNAME))
                        _InvoicesFacade.UpdateSingleMailsSendStatus(mail);
                    else
                        _InvoicesFacade.UpdateBlocksMailSendStatus(mail);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {

                this.DataProvider.RollbackTransaction();

            }

        }



        private void SendEmailInternal(string title, string bodyhtml, string toemail)
        {
            try
            {
                string mailHost = ConfigurationManager.AppSettings["MailHost"];
                string sendMailUser = ConfigurationManager.AppSettings["SendMailUser"];
                string sendMailPwd = ConfigurationManager.AppSettings["SendMailPWD"];
                MailAddress from = new MailAddress(sendMailUser, sendMailUser, System.Text.Encoding.UTF8);
                MailMessage mail = new MailMessage();


                //设置邮件的标题
                mail.Subject = title;
                mail.From = from;//设置邮件的发件人
                mail.To.Clear();
                string toMail = toemail.Replace(';', ',');
                //string toMail = toemail;
                mail.To.Add(toMail);
                mail.Body = bodyhtml;// "邮件内容";
                //设置邮件的格式
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = false;

                mail.Priority = MailPriority.Normal;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

                SmtpClient client = new SmtpClient();
                //设置用于 SMTP 事务的主机的名称，填IP地址也可以了
                client.Host = mailHost;
                //设置用于 SMTP 事务的端口，默认的是 25
                int port = 25;
                client.Port = port;
                client.EnableSsl = false;
                client.UseDefaultCredentials = true;
                //这里才是真正的邮箱登陆名和密码
                client.Credentials = new System.Net.NetworkCredential(sendMailUser, sendMailPwd);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);


            }
            catch (Exception e)
            {
                Log.Error("Mail Erro -----" + e.Message);
                throw e;

            }


        }
        #endregion


        #region SAP回写
        class UBComparer : IComparer<Ub2Sap>
        {

            #region IComparer<Ub2Sap> Members

            public int Compare(Ub2Sap x, Ub2Sap y)
            {
                int one = int.Parse(x.InOutFlag);
                int two = int.Parse(y.InOutFlag);
                if (one > two)
                    return -1;
                else if (one < two)
                    return 1;
                else
                    return 0;
            }

            #endregion
        }
        class SAPException : Exception
        {
            public SAPException() { }
            public SAPException(string message)
                : base(message)
            {

            }


        }
        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendPoToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.PO> Pos)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.POToSAP PoToS = new BenQGuru.eMES.SAPRFCService.POToSAP(this.DataProvider);

            try
            {

                r = PoToS.POReceiveToSAP(Pos);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendStockScrapToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.StockScrap> ss)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.StockScrapToSAP ssToSap = new BenQGuru.eMES.SAPRFCService.StockScrapToSAP(this.DataProvider);

            try
            {

                r = ssToSap.PostStockScrapToSAP(ss);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendWWPOToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.WWPO> wwPOs)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.WWPO2SAP wwPoToS = new BenQGuru.eMES.SAPRFCService.WWPO2SAP(this.DataProvider);

            try
            {

                r = wwPoToS.PostWWPOToSAP(wwPOs);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendRSToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.RS> rss)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.RSToSAP rToS = new BenQGuru.eMES.SAPRFCService.RSToSAP(this.DataProvider);

            try
            {

                r = rToS.PostRSToSAP(rss);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendUBToSAP(List<BenQGuru.eMES.SAPRFCService.Domain.UB> ubs)
        {
            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.UBToSAP uToS = new BenQGuru.eMES.SAPRFCService.UBToSAP(this.DataProvider);

            try
            {

                r = uToS.PostUBToSAP(ubs);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }

            return r;

        }

        private BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn SendDNToSap(List<BenQGuru.eMES.SAPRFCService.Domain.DN> dns, bool isALL)
        {

            BenQGuru.eMES.SAPRFCService.Domain.SAPRfcReturn r = null;
            BenQGuru.eMES.SAPRFCService.DNToSAP d = new BenQGuru.eMES.SAPRFCService.DNToSAP(this.DataProvider);

            try
            {

                r = d.DNPGIToSAP(dns, isALL);
            }
            catch (Exception ex)
            {
                throw new SAPException(ex.Message);
            }



            return r;
        }

        #endregion

        private string Getstring(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }
            return "";
        }
        private List<Material> GetAllMaterialList()
        {
            object[] ma = _MainDataFacade.GetAllMaterial();
            return Common.Array2MaterialList(ma);
        }


    }




    class ClosingSapMailClass
    {
        public string LogID { get; set; }
        public string Interface { get; set; }
        public string Invno { get; set; }
        public string Message { get; set; }
    }
}
