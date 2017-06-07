using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.SAP.SAPNcoLib;
using SAP.Middleware.Connector;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using TestRFC;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.InterfaceDomain;
using BenQGuru.eMES.InterfaceFacade;

namespace TestSAPRFC
{
    public partial class TestSAPRFC_Frm : Form
    {
        private IDomainDataProvider _domainDataProvider = null;
        //private const string saprouter = "/H/218.30.179.199/S/3299/H/192.168.129.249";//外网
        private const string saprouter = "/H/192.168.129.252/S/3299/H/192.168.129.249";//内网
        //输出
        private const string OUTTABLE = "IS_ITEM";//输出的Table
        private const string VENDOROUTTABLE = "ET_VENDOR";
        private const string STORAGEOUTTABLE = "ET_LGORT";
        private const string MATERIALOUTTABLE = "ET_MATERNAL";
        private const string POOUTTABLE = "ET_EKPO";
        private const string STOCKOUTTABLE = "ET_STOCK";
        private const string UBOUTTABLE = "ET_EKPO";
        private const string RSOUTTABLE = "ET_RESB";
        private const string DNOUTTABLE = "OT_PCBH";

        private InvoicesFacade _InvoicesFacade = null;
        private MainDataFacade _MainDataFacade = null;

        public TestSAPRFC_Frm()
        {
            InitializeComponent();
        }

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

        private void ShowMessage(string msg)
        {
            rMsg.AppendText(msg + "\r\n");
            rMsg.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_SD_CUSTOMER_GET";//Rfc function name

                DataTable dtDetail = new DataTable();
                dtDetail.Columns.Add("KUNNR", typeof(string));
                DataRow row = dtDetail.NewRow();
                row["KUNNR"] = kunnr;
                dtDetail.Rows.Add(row);

                IRfcStructure rfcStructure = ncoClient.ConvertDataTabletoRFCStructure(row, "IS_HEADER");
                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("IS_HEADER", rfcStructure);
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveCustomerData(ds);

                //if (ds != null && ds.Tables["IS_ITEM"].Rows.Count > 0)
                //{
                //    this.dataGridView1.DataSource = ds.Tables["IS_ITEM"].DefaultView;
                //}

                //Dictionary<string, object> exportParameters = new Dictionary<string, object>();
                //exportParameters = ncoClient.ExportParameters;
                //object parameter = null;
                //if (exportParameters != null)
                //{
                //    if (exportParameters.TryGetValue("ES_RESULT", out parameter))
                //    {
                //        IRfcStructure ROFStrcture = parameter as IRfcStructure;
                //        ShowMessage(ROFStrcture["RETUN"].GetValue().ToString());
                //        ShowMessage(ROFStrcture["MESSG"].GetValue().ToString());
                //    }
                //}
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void SaveCustomerData(DataSet ds)
        {
            if (ds != null && ds.Tables[OUTTABLE] != null)
            {
                if (ds.Tables[OUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate,out intTime, this.DataProvider);
                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        Customer ct = new Customer();
                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[OUTTABLE].Rows)
                        {
//                            string strCusCode = Convert.ToString(row["KUNNR"]);
//                            string strCusName = Convert.ToString(row["NAME"]);
//                            string strAddress = Convert.ToString(row["STRAS"]);
//                            string strTel = Convert.ToString(row["TELF1"]);
//                            string strFlag = Convert.ToString(row["AUFSD"]);

//                            string strInsert = @"INSERT INTO  TBLCUSTOMER
//                                         (customercode,customername,address,tel,flag,muser,mdate,mtime) VALUES(
//                                            '" + strCusCode + "','" + strCusName + "','" + strAddress + "','" + strTel + "','" + strFlag + "','JOB'," + intDate + "," + intTime + ")";

//                            SQLCondition SC = new SQLCondition(strInsert);
//                            this.DataProvider.CustomExecute(SC);

                            ct.Customercode = Convert.ToString(row["KUNNR"]);
                            ct.Customername = Convert.ToString(row["NAME"]);
                            ct.Address = Convert.ToString(row["STRAS"]);
                            ct.Tel = Convert.ToString(row["TELF1"]);
                            ct.Flag = Convert.ToString(row["AUFSD"]);
                            ct.MaintainUser = "JOB";
                            ct.MaintainDate = intDate;
                            ct.MaintainTime = intTime;

                            _MainDataFacade.AddCustomer(ct);
                        }
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

        private void btnVendor_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_MM_VENDOR_GET";//Rfc function name

                DataSet ds = ncoClient.Execute();
                SaveVendorData(ds);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void SaveVendorData(DataSet ds)
        {
            if (ds != null && ds.Tables[VENDOROUTTABLE] != null)
            {
                if (ds.Tables[VENDOROUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                        DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
                        int intDate = FormatHelper.TODateInt(dtNow);
                        int intTime = FormatHelper.TOTimeInt(dtNow);

                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        Vendor vd = new Vendor();
                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[VENDOROUTTABLE].Rows)
                        {
//                            string strVendorCode = Convert.ToString(row["LIFNR"]);
//                            string strVendorName = Convert.ToString(row["NAME1"]).Replace("\'", string.Empty);
//                            string strAlias = Convert.ToString(row["SORTL"]).Replace("\'", string.Empty);
//                            string strVendorUser = Convert.ToString(row["NAMEV"]);
//                            string strVendorAddr = Convert.ToString(row["STRAS"]).Replace("\'", string.Empty);
//                            string strFaxNO = Convert.ToString(row["TELFX"]);
//                            string strMobileNO = Convert.ToString(row["TELF2"]);

//                            string strInsert = @"INSERT INTO  TBLVENDOR
//                                         (vendorcode,vendorname,alias,vendoruser,vendoraddr,faxno,mobileno,muser,mdate,mtime) VALUES(
//                                            '" + strVendorCode + "','" + strVendorName + "','" + strAlias + "','" + strVendorUser + "','"
//                                                + strVendorAddr + "','" + strFaxNO + "','" + strMobileNO + "','JOB'," + intDate + "," + intTime + ")";

//                            SQLCondition SC = new SQLCondition(strInsert);
//                            this.DataProvider.CustomExecute(SC);

                            vd.Vendorcode = Convert.ToString(row["LIFNR"]);
                            vd.Vendorname = Convert.ToString(row["NAME1"]);
                            vd.Alias = Convert.ToString(row["SORTL"]);
                            vd.Vendoruser = Convert.ToString(row["NAMEV"]);
                            vd.Vendoraddr = Convert.ToString(row["STRAS"]);
                            vd.Faxno = Convert.ToString(row["TELFX"]);
                            vd.Mobileno = Convert.ToString(row["TELF2"]);
                            vd.MaintainUser = "JOB";
                            vd.MaintainDate = intDate;
                            vd.MaintainTime = intTime;

                            _MainDataFacade.AddVendor(vd);
                        }
                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                }
            }
        }

        private void btnStorage_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_MM_LGORT_INFO";//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_WERKS", "10Y2");
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveStorageData(ds);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void SaveStorageData(DataSet ds)
        {
            if (ds != null && ds.Tables[STORAGEOUTTABLE] != null)
            {
                if (ds.Tables[STORAGEOUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[STORAGEOUTTABLE].Rows)
                        {
//                            string strStoCode = Convert.ToString(row["LGORT"]);
//                            string strStoName = Convert.ToString(row["LGOBE"]).Replace("\'", string.Empty);
//                            string strAddress1 = Convert.ToString(row["ADDR1"]).Replace("\'", string.Empty);
//                            string strAddress2 = Convert.ToString(row["ADDR2"]).Replace("\'", string.Empty);
//                            string strAddress3 = Convert.ToString(row["ADDR3"]).Replace("\'", string.Empty);
//                            string strAddress4 = Convert.ToString(row["ADDR4"]).Replace("\'", string.Empty);
//                            string strContactUser1 = Convert.ToString(row["NAME1"]).Replace("\'", string.Empty);
//                            string strContactUser2 = Convert.ToString(row["NAME2"]).Replace("\'", string.Empty);
//                            string strContactUser3 = Convert.ToString(row["NAME3"]).Replace("\'", string.Empty);
//                            string strContactUser4 = Convert.ToString(row["NAME4"]).Replace("\'", string.Empty);

//                            string strInsert = @"INSERT INTO  TBLSTORAGE
//                                             (storagecode,storagename,orgid,address1,address2,address3,address4,contactuser1,contactuser2,
//                                        contactuser3,contactuser4,virtualflag,sourceflag,muser,mdate,mtime) VALUES(
//                                        '" + strStoCode + "','" + strStoName + "',1,'" + strAddress1 + "','" + strAddress2 + "','" + strAddress3
//                                       + "','" + strAddress4 + "','" + strContactUser1 + "','" + strContactUser2 + "','" + strContactUser3
//                                        + "','" + strContactUser4 + "','N','SAP','JOB'," + intDate + "," + intTime + ")";

//                            SQLCondition SC = new SQLCondition(strInsert);
//                            this.DataProvider.CustomExecute(SC);

                            Storage st = new Storage();
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
                            st.Orgid = 1;
                            st.Virtualflag = "N";
                            st.Sourceflag = "SAP";
                            st.MaintainUser = "JOB";
                            st.MaintainDate = intDate;
                            st.MaintainTime = intTime;

                            _MainDataFacade.AddStorage(st);
                        }
                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                }
            }
        }

        private void btnMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_MM_MATERNAL_GET";//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_WERKS", "10Y2");
                importParameters.Add("I_VKORG", "62Y2");
                importParameters.Add("I_VTWEG", "O1");
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveMaterialData(ds);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void SaveMaterialData(DataSet ds)
        {
            if (ds != null && ds.Tables[MATERIALOUTTABLE] != null)
            {
                if (ds.Tables[MATERIALOUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[MATERIALOUTTABLE].Rows)
                        {
//                            string MCODE = Convert.ToString(row["MATNR"]);
//                            string DQMCODE = Convert.ToString(row["ZEINR"]).Replace("\'", string.Empty);
//                            string MUOM = Convert.ToString(row["MEINS"]).Replace("\'", string.Empty);
//                            string MTYPE = "itemtype_finishedproduct";
//                            //string MCONTROLTYPE = "";
//                            string ROHS = Convert.ToString(row["KZUMW"]).Replace("\'", string.Empty);
//                            string MCHSHORTDESC = Convert.ToString(row["CMAKTX"]).Replace("\'", string.Empty);
//                            string MENSHORTDESC = Convert.ToString(row["MAKTX"]).Replace("\'", string.Empty);
//                            string MENLONGDESC = Convert.ToString(row["LETXT"]).Replace("\'", string.Empty);
//                            string MCHLONGDESC = Convert.ToString(row["LCTXT"]).Replace("\'", string.Empty);
//                            string MODELCODE = Convert.ToString(row["MATKL"]).Replace("\'", string.Empty);
//                            int MSTATE = 0;
//                            string SOURCEFLAG = "SAP";
//                            string MATERIALTYPE = Convert.ToString(row["MTART"]).Replace("\'", string.Empty);
//                            string MSTATE1 = Convert.ToString(row["LVORM"]).Replace("\'", string.Empty);
//                            string MSTATE2 = Convert.ToString(row["MSTAE"]).Replace("\'", string.Empty);
//                            string MSTATE3 = Convert.ToString(row["MSTAV"]).Replace("\'", string.Empty);
//                            string VALIDFROM = Convert.ToString(row["MSTDV"]).Replace("\'", string.Empty);
//                            string EATTRIBUTE8 = Convert.ToString(row["MTPOS"]).Replace("\'", string.Empty);
//                            string CDQTY = Convert.ToString(row["CDMNG"]).Replace("\'", string.Empty);
//                            string CDFOR = Convert.ToString(row["CDTYP"]).Replace("\'", string.Empty);
//                            string PURCHASINGGROUP = Convert.ToString(row["EKGRP"]).Replace("\'", string.Empty);
//                            string ABCINDICATOR = Convert.ToString(row["MAABC"]).Replace("\'", string.Empty);
//                            string MRPTYPE = Convert.ToString(row["DISMM"]).Replace("\'", string.Empty);
//                            string REORDERPOINT = Convert.ToString(row["MINBE"]).Replace("\'", string.Empty);
//                            string MRPCONTORLLER = Convert.ToString(row["DISPO"]).Replace("\'", string.Empty);
//                            string MINIMUMLOTSIZE = Convert.ToString(row["BSTMI"]).Replace("\'", string.Empty);
//                            string ROUNDINGVALUE = Convert.ToString(row["BSTRF"]).Replace("\'", string.Empty);
//                            string SPECIALPROCYREMENT = Convert.ToString(row["SOBSL"]).Replace("\'", string.Empty);
//                            string SAFETYSTOCK = Convert.ToString(row["EISBE"]).Replace("\'", string.Empty);
//                            string BULKMATERIAL = Convert.ToString(row["SCHGT"]).Replace("\'", string.Empty);

//                            string strInsert = @"INSERT INTO  TBLMATERIAL
//                                         (MCODE,DQMCODE,MUOM,MTYPE,ROHS,MCHSHORTDESC,MENSHORTDESC,MENLONGDESC,MCHLONGDESC,
//                                    MODELCODE,MSTATE,SOURCEFLAG,MATERIALTYPE,MSTATE1,MSTATE2,MSTATE3,
//                                    VALIDFROM,EATTRIBUTE8,CDQTY,CDFOR,PURCHASINGGROUP,ABCINDICATOR,MRPTYPE,REORDERPOINT,MRPCONTORLLER,
//                                    MINIMUMLOTSIZE,ROUNDINGVALUE,SPECIALPROCYREMENT,SAFETYSTOCK,BULKMATERIAL,
//                                    CUSER,CDATE,CTIME,MUSER,MDATE,MTIME) VALUES(
//                                    '" + MCODE + "','" + DQMCODE + "','" + MUOM + "','" + MTYPE + "','" + ROHS + "','" + MCHSHORTDESC
//                                       + "','" + MENSHORTDESC + "','" + MENLONGDESC + "','" + MCHLONGDESC + "','" + MODELCODE
//                                        + "'," + MSTATE + ",'" + SOURCEFLAG + "','" + MATERIALTYPE + "','" + MSTATE1 + "','" + MSTATE2 + "','" + MSTATE3
//                                        + "','" + VALIDFROM + "','" + EATTRIBUTE8 + "','" + CDQTY + "','" + CDFOR + "','" + PURCHASINGGROUP + "','" + ABCINDICATOR
//                                        + "','" + MRPTYPE + "','" + REORDERPOINT + "','" + MRPCONTORLLER + "','" + MINIMUMLOTSIZE + "','" + ROUNDINGVALUE + "','" + SPECIALPROCYREMENT
//                                        + "','" + SAFETYSTOCK + "','" + BULKMATERIAL + "','JOB'," + intDate + "," + intTime + ",'JOB'," + intDate + "," + intTime + ")";

//                            SQLCondition SC = new SQLCondition(strInsert);
//                            this.DataProvider.CustomExecute(SC);
                            if (_MainDataFacade.QueryMaterialCount(Convert.ToString(row["MATNR"])) > 0)
                            {
                                continue;
                            }
                            Material ma = new Material();
                            ma.Mcode = Convert.ToString(row["MATNR"]);
                            ma.Dqmcode = Convert.ToString(row["ZEINR"]);
                            ma.Muom = Convert.ToString(row["MEINS"]);
                            ma.Mtype = "itemtype_finishedproduct";
                            //string MCONTROLTYPE = "";
                            ma.Rohs = Convert.ToString(row["KZUMW"]);
                            ma.Mchshortdesc = Convert.ToString(row["CMAKTX"]);
                            ma.Menshortdesc = Convert.ToString(row["MAKTX"]);
                            ma.Menlongdesc = Convert.ToString(row["LETXT"]);
                            ma.Mchlongdesc = Convert.ToString(row["LCTXT"]);
                            ma.Modelcode = Convert.ToString(row["MATKL"]);
                            ma.Mstate = 0;
                            ma.Sourceflag = "SAP";
                            ma.Materialtype = Convert.ToString(row["MTART"]);
                            ma.Mstate1 = Convert.ToString(row["LVORM"]);
                            ma.Mstate2 = Convert.ToString(row["MSTAE"]);
                            ma.Mstate3 = Convert.ToString(row["MSTAV"]);
                            ma.Validfrom = Convert.ToInt32(row["MSTDV"]);
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
                            ma.MaintainUser = "JOB";
                            ma.MaintainDate = intDate;
                            ma.MaintainTime = intTime;
                            ma.Cuser = "JOB";
                            ma.Cdate = intDate;
                            ma.Ctime = intTime;
                            //_MainDataFacade.AddMaterial(ma);
                        }
                        this.DataProvider.CommitTransaction();
                        }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                }
            }
        }

        private void btnPO_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_MM_PO_GET";//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_DATUM", "20160301");
                importParameters.Add("I_UZEIT", "000000");
                importParameters.Add("I_BUKRS", "TD28"); //not necessary
                importParameters.Add("I_EKORG", "61Y2"); //not necessary
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                //SavePOData(ds);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void SavePOData(DataSet ds)
        {
            if (ds != null && ds.Tables[POOUTTABLE] != null)
            {
                if (ds.Tables[POOUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        List<Invoices> list = new List<Invoices>();
                        if (_InvoicesFacade == null)
                        {
                            _InvoicesFacade = new InvoicesFacade(this.DataProvider);
                        }
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[POOUTTABLE].Rows)
                        {
                            if (_InvoicesFacade.QueryInvoicesCount(Convert.ToString(row["EBELN"])) > 0)
                            {
                                continue;
                            }
                            if (list.Exists(m => m.Invno == Convert.ToString(row["EBELN"])))
                            {
                                continue;
                            }
                            Invoices item = new Invoices();
                            item.Invno = Convert.ToString(row["EBELN"]);
                            if (item.Invno == "4503301955")
                            {
                                continue;
                            }
                            item.Invtype = "POR";
                            item.Invstatus = "Release";
                            item.Companycode = Convert.ToString(row["BUKRS"]);
                            item.Vendorcode = Convert.ToString(row["LIFNR"]);
                            item.Orderstatus = Convert.ToString(row["FRGKX"]) == "G" ? "Release" : "Pending";
                            //item.Orderstatus = Convert.ToString(row["FRGKX"]);
                            item.Ordertype = Convert.ToString(row["BSART"]);
                            item.Pocreatedate = Convert.ToInt32(row["ERDAT"]);
                            item.Createuser = Convert.ToString(row["ERNAM"]);
                            item.Poupdatedate = Convert.ToInt32(row["AEDAT"]);
                            item.Poupdatetime = Convert.ToInt32(row["AEZET"]);
                            item.Buyerphone = Convert.ToString(row["TEL_NUMBER"]);
                            item.Remark1 = Convert.ToString(row["HTEXT"]);
                            item.Remark2 = Convert.ToString(row["HNOTE"]);
                            item.Remark3 = Convert.ToString(row["HMARK"]);
                            item.Remark4 = Convert.ToString(row["IHREZ"]);
                            item.Remark5 = Convert.ToString(row["UNSEZ"]);
                            item.Purchorgcode = Convert.ToString(row["EKORG"]);
                            item.Purchugcode = Convert.ToString(row["EKGRP"]);
                            item.Purchasegroup = Convert.ToString(row["EKNAM"]);
                            item.Finishflag = "N";
                            item.MaintainUser = "JOB";
                            item.MaintainDate = intDate;
                            item.MaintainTime = intTime;
                            item.Cuser = "JOB";
                            item.Cdate = intDate;
                            item.Ctime = intTime;
                            list.Add(item);

                            _InvoicesFacade.AddInvoices(item);
                        }
                        int intDate1, intTime1;
                        Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                        List<Material> materialList = this.GetAllMaterialList();
                        foreach (DataRow row in ds.Tables[POOUTTABLE].Rows)
                        {
                            if (_InvoicesFacade.QueryInvoicesdetailCount(Convert.ToString(row["EBELN"]), Convert.ToInt32(row["EBELP"])) > 0)
                            {
                                continue;
                            }
                            Invoicesdetail detail = new Invoicesdetail();
                            detail.Invno = Convert.ToString(row["EBELN"]);
                            if (detail.Invno == "4503301955")
                            {
                                continue;
                            }
                            detail.Invline = Convert.ToInt32(row["EBELP"]);
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
                            detail.Invlinestatus = strLineStatus;
                            detail.Faccode = Convert.ToString(row["WERKS"]);
                            detail.Mcode = Convert.ToString(row["MATNR"]);
                            detail.Menshortdesc = Convert.ToString(row["TXZ01"]);
                            detail.Mlongdesc = Convert.ToString(row["MAKTX"]);
                            detail.Planqty = Convert.ToDecimal(row["MENGE"]);
                            detail.Plandate = Convert.ToInt32(row["EINDT"]);
                            detail.Unit = Convert.ToString(row["MEINS"]);
                            detail.Shipaddr = Convert.ToString(row["STRAS"]);
                            detail.Storagecode = Convert.ToString(row["LGORT"]);
                            detail.Detailremark = Convert.ToString(row["STEXT"]);
                            detail.Vendormcode = Convert.ToString(row["IDNLF"]);
                            detail.Prno = Convert.ToString(row["BANFN"]);
                            detail.Returnflag = Convert.ToString(row["RETPO"]);
                            detail.Accountassignment = Convert.ToString(row["KNTTP"]);
                            detail.Itemcategory = Convert.ToString(row["PSTYP"]);
                            detail.So = Convert.ToString(row["VBELN"]);
                            detail.Soitemno = Convert.ToString(row["VBELP"]);
                            detail.Sowbsno = Convert.ToString(row["PS_PSP_PNR"]);
                            detail.Ccno = Convert.ToString(row["KOSTL"]);
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

//                            string strInsert = @"INSERT INTO  TBLINVOICESDETAIL
//                                                (Invno,MCODE,DQMCODE,MUOM,MTYPE,ROHS,MCHSHORTDESC,MENSHORTDESC,MENLONGDESC,MCHLONGDESC,
//                                        MODELCODE,MSTATE,SOURCEFLAG,MATERIALTYPE,MSTATE1,MSTATE2,MSTATE3,
//                                        VALIDFROM,EATTRIBUTE8,CDQTY,CDFOR,PURCHASINGGROUP,ABCINDICATOR,MRPTYPE,REORDERPOINT,MRPCONTORLLER,
//                                        MINIMUMLOTSIZE,ROUNDINGVALUE,SPECIALPROCYREMENT,SAFETYSTOCK,BULKMATERIAL,
//                                        CUSER,CDATE,CTIME,MUSER,MDATE,MTIME) VALUES(
//                                                                '" + MCODE + "','" + DQMCODE + "','" + MUOM + "','" + MTYPE + "','" + ROHS + "','" + MCHSHORTDESC
//                                       + "','" + MENSHORTDESC + "','" + MENLONGDESC + "','" + MCHLONGDESC + "','" + MODELCODE
//                                        + "'," + MSTATE + ",'" + SOURCEFLAG + "','" + MATERIALTYPE + "','" + MSTATE1 + "','" + MSTATE2 + "','" + MSTATE3
//                                        + "','" + VALIDFROM + "','" + EATTRIBUTE8 + "','" + CDQTY + "','" + CDFOR + "','" + PURCHASINGGROUP + "','" + ABCINDICATOR
//                                        + "','" + MRPTYPE + "','" + REORDERPOINT + "','" + MRPCONTORLLER + "','" + MINIMUMLOTSIZE + "','" + ROUNDINGVALUE + "','" + SPECIALPROCYREMENT
//                                        + "','" + SAFETYSTOCK + "','" + BULKMATERIAL + "','JOB'," + intDate + "," + intTime + ",'JOB'," + intDate + "," + intTime + ")";
//                            SQLCondition SC = new SQLCondition(strInsert);
//                            this.DataProvider.CustomExecute(SC);
                        }
                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                }
            }
        }

        private List<Material> GetAllMaterialList()
        {
            if (_InvoicesFacade == null)
            {
                _InvoicesFacade = new InvoicesFacade(this.DataProvider);
            }
            if (_MainDataFacade == null)
            {
                _MainDataFacade = new MainDataFacade(this.DataProvider);
            }
            object[] ma = _MainDataFacade.GetAllMaterial();
            return Common.Array2MaterialList(ma);
        }

        private void btnKCMX_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_STOCK_DETAIL";//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_WERKS", "10Y2");
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveStockData(ds);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void SaveStockData(DataSet ds)
        {
            if (ds != null && ds.Tables[STOCKOUTTABLE] != null)
            {
                if (ds.Tables[STOCKOUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        //List<Invoices> list = new List<Invoices>();
                        if (_InvoicesFacade == null)
                        {
                            _InvoicesFacade = new InvoicesFacade(this.DataProvider);
                        }
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[STOCKOUTTABLE].Rows)
                        {
                            //if (list.Exists(m => m.Invno == Convert.ToString(row["EBELN"])))
                            //{
                            //    continue;
                            //}
                            Sapstorageinfo item = new Sapstorageinfo();
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
                            item.MaintainUser = "JOB";
                            item.MaintainDate = intDate;
                            item.MaintainTime = intTime;
                            item.Cuser = "JOB";
                            item.Cdate = intDate;
                            item.Ctime = intTime;

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
            }
        }

        private void btnUB_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_MM_STO";//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_DATUM", "20160101");
                importParameters.Add("I_UZEIT", "000000");
                importParameters.Add("I_BUKRS", "TD28"); //not necessary
                importParameters.Add("I_EKORG", "61Y2"); //not necessary
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveUBData(ds);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void SaveUBData(DataSet ds)
        {
            if (ds != null && ds.Tables[UBOUTTABLE] != null)
            {
                if (ds.Tables[UBOUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        List<Invoices> list = new List<Invoices>();
                        if (_InvoicesFacade == null)
                        {
                            _InvoicesFacade = new InvoicesFacade(this.DataProvider);
                        }
                        if (_MainDataFacade == null)
                        {
                            _MainDataFacade = new MainDataFacade(this.DataProvider);
                        }
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);

                        List<Material> maList = Common.Array2MaterialList(_MainDataFacade.GetAllMaterial());

                        this.DataProvider.BeginTransaction();
                        foreach (DataRow row in ds.Tables[UBOUTTABLE].Rows)
                        {
                            if (list.Exists(m => m.Invno == Convert.ToString(row["EBELN"])))
                            {
                                continue;
                            }
                            Invoices item = new Invoices();
                            item.Invno = Convert.ToString(row["EBELN"]);
                            item.Invstatus = "Release";
                            item.Companycode = Convert.ToString(row["BUKRS"]);
                            item.Purchorgcode = Convert.ToString(row["EKORG"]);
                            item.Invtype = Convert.ToString(row["BSART"]); //???
                            item.Ubtype = Convert.ToString(row["BSART"]);
                            item.Poupdatedate = Convert.ToInt32(row["AEDAT"]);
                            item.Poupdatetime = Convert.ToInt32(row["AEZET"]);
                            item.Oano = Convert.ToString(row["IHREZ"]);
                            item.Logistics = Convert.ToString(row["HTEXT"]);
                            item.Remark1 = Convert.ToString(row["DTEXT"]);//备注
                            item.Createuser = Convert.ToString(row["UNAME"]);
                            item.Pocreatedate = Convert.ToInt32(row["DATUM"]);//UB创建日期
                            item.Reworkapplyuser = Convert.ToString(row["UNSEZ"]);
                            item.Finishflag = "N";
                            item.MaintainUser = "JOB";
                            item.MaintainDate = intDate;
                            item.MaintainTime = intTime;
                            item.Cuser = "JOB";
                            item.Cdate = intDate;
                            item.Ctime = intTime;
                            list.Add(item);

                            _InvoicesFacade.AddInvoices(item);
                        }
                        int intDate1, intTime1;
                        Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                        List<Material> materialList = this.GetAllMaterialList();
                        foreach (DataRow row in ds.Tables[UBOUTTABLE].Rows)
                        {
                            Invoicesdetail detail = new Invoicesdetail();
                            detail.Invno = Convert.ToString(row["EBELN"]);
                            detail.Invline = Convert.ToInt32(row["EBELP"]);
                            detail.Mcode = Convert.ToString(row["MATNR"]);
                            detail.Menshortdesc = Convert.ToString(row["TXZ01"]);//物料描述
                            string strLineStatus = default(string);
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
                            detail.Invlinestatus = strLineStatus;//调拨行状态
                            detail.Faccode = Convert.ToString(row["WERKS"]);
                            detail.Fromstoragecode = Convert.ToString(row["AFNAM"]);
                            detail.Storagecode = Convert.ToString(row["LGORT"]);
                            detail.Receiveraddr = Convert.ToString(row["STRAS"]);
                            detail.Receiveruser = Convert.ToString(row["RINFO"]);
                            detail.Planqty = Convert.ToDecimal(row["MENGE"]);//调拨数量
                            detail.Unit = Convert.ToString(row["MEINS"]);
                            detail.Custmcode = Convert.ToString(row["IDNLF"]);
                            detail.Receivemcode = Convert.ToString(row["STEXT"]);
                            detail.Demandarrivaldate = Convert.ToInt32(row["EINDT"]);
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
                        this.DataProvider.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        this.DataProvider.RollbackTransaction();
                        throw ex;
                    }
                }
            }

        }

        private void btnRS_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_RESEARCH_PR_RESB";//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_DATUM", "20100101");
                importParameters.Add("I_UZEIT", "100000");
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveRSData(ds);

            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }

        }
        private void SaveRSData(DataSet ds)
        {
            if (ds != null && ds.Tables[RSOUTTABLE] != null)
            {
                if (ds.Tables[RSOUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        List<Invoices> list = new List<Invoices>();
                        if (_InvoicesFacade == null)
                        {
                            _InvoicesFacade = new InvoicesFacade(this.DataProvider);
                        }
                        int intDate, intTime;
                        Common.GetDBDateTime(out intDate, out intTime, this.DataProvider);
                        foreach (DataRow row in ds.Tables[RSOUTTABLE].Rows)
                        {
                            string strRSNO = Convert.ToString(row["RSNUM"]);
                            if (string.IsNullOrEmpty(Convert.ToString(row["MATNR"])))
                            {
                                continue;
                            }
                            if (_InvoicesFacade.QueryInvoicesCount(strRSNO) > 0)
                            {
                                continue;
                            }
                            if (list.Exists(m => m.Invno == strRSNO))
                            {
                                continue;
                            }
                            Invoices item = new Invoices();
                            item.Invno = strRSNO;
                            item.Cc = Convert.ToString(row["KOSTL"]);
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
                            item.Invtype = strInvType;
                            item.Invstatus = "Release";
                            item.Applydate = Convert.ToInt32(row["RSDAT"]);
                            item.Finishflag = "N";
                            item.MaintainUser = "JOB";
                            item.MaintainDate = intDate;
                            item.MaintainTime = intTime;
                            item.Cuser = "JOB";
                            item.Cdate = intDate;
                            item.Ctime = intTime;
                            list.Add(item);

                            _InvoicesFacade.AddInvoices(item);
                        }

                        int intDate1, intTime1;
                        Common.GetDBDateTime(out intDate1, out intTime1, this.DataProvider);
                        List<Material> materialList = this.GetAllMaterialList();
                        foreach (DataRow row in ds.Tables[RSOUTTABLE].Rows)
                        {
                            if (string.IsNullOrEmpty(Convert.ToString(row["MATNR"])))
                            {
                                continue;
                            }
                            string strRSNO = Convert.ToString(row["RSNUM"]);
                            int intInvLine = Convert.ToInt32(row["RSPOS"]);
                            if (_InvoicesFacade.QueryInvoicesdetailCount(strRSNO, intInvLine) > 0)
                            {
                                continue;
                            }
                            Invoicesdetail detail = new Invoicesdetail();
                            detail.Invno = strRSNO;
                            detail.Invline = intInvLine;
                            detail.Invlinestatus = Convert.ToString(row["XLOEK"]);//X=删除，空=未删除
                            detail.Mcode = Convert.ToString(row["MATNR"]);
                            detail.Custmcode = Convert.ToString(row["IDNLF"]);
                            detail.Storagecode = Convert.ToString(row["UMLGO"]);
                            detail.Planqty = Convert.ToDecimal(row["BDMNG"]);//缺少数量
                            detail.Unit = Convert.ToString(row["MEINS"]);
                            List<Material> maTempList = materialList.Where(m => m.Mcode == detail.Mcode).ToList();
                            if (maTempList.Count > 0)
                            {
                                detail.Dqmcode = maTempList[0].Dqmcode;
                            }
                            detail.Needdate = Convert.ToInt32(row["BDTER"]);
                            detail.Prno = Convert.ToString(row["WEMPF"]);
                            detail.Receiveruser = Convert.ToString(row["ABLAD"]);
                            detail.Receiveraddr = Convert.ToString(row["SGTXT"]);
                            detail.MaintainUser = "JOB";
                            detail.MaintainDate = intDate1;
                            detail.MaintainTime = intTime1;
                            detail.Cuser = "JOB";
                            detail.Cdate = intDate1;
                            detail.Ctime = intTime1;

                            _InvoicesFacade.AddInvoicesdetail(detail);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtNAME.Text;
                string user = txtUSER.Text;
                string passwd = txtPASSWD.Text;
                string client = txtCLIENT.Text;
                string ashost = txtASHOST.Text;
                string sysnr = txtSYSNR.Text;
                //string saprouter = txtSAPROUTER.Text;
                string kunnr = txtKUNNR.Text;
                NcoFunction ncoClient = new NcoFunction();
                ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                ncoClient.FunctionName = "ZCHN_SD_PCBH_GET";//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("I_DATUM", "20160301");
                importParameters.Add("I_UZEIT", "000000");
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                SaveDNData(ds);
            }
            catch (Exception ex)
            {
                ShowMessage("Exception:" + ex.Message);
            }
        }
        private void SaveDNData(DataSet ds)
        {
            if (ds != null && ds.Tables[DNOUTTABLE] != null)
            {
                if (ds.Tables[DNOUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        #region head
                        List<Invoices> list = new List<Invoices>();
                        if (_InvoicesFacade == null)
                        {
                            _InvoicesFacade = new InvoicesFacade(this.DataProvider);
                        }
                        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                        DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
                        int intDate = FormatHelper.TODateInt(dtNow);
                        int intTime = FormatHelper.TOTimeInt(dtNow);
                        foreach (DataRow row in ds.Tables[DNOUTTABLE].Rows)
                        {
                            string strDNNO = Convert.ToString(row["VBELN"]);
                            if (_InvoicesFacade.QueryInvoicesCount(strDNNO) > 0)
                            {
                                continue;
                            }
                            if (list.Exists(m => m.Invno == strDNNO))
                            {
                                continue;
                            }
                            Invoices item = new Invoices();
                            item.Invno = strDNNO;
                            item.Dnbatchno = Convert.ToString(row["PCBH"]);
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
                            if (strDNBatchStatus == "Cancel" || strDNBatchStatus == "Pending") //Cancel和Pending的不处理
                            {
                                continue;
                            }
                            item.Dnbatchstatus = strDNBatchStatus;
                            string strInvtype = Convert.ToString(row["VBTYP"]);
                            if (strInvtype == "J")
                            {
                                strInvtype = "DNC";//发货
                            }
                            else if (strInvtype == "T")
                            {
                                strInvtype = "DNR";//退货
                            }
                            if (Convert.ToString(row["JFFS"]) == "物料直发")
                            {
                                strInvtype = "DNZC";//供应商直发DN
                            }
                            item.Invtype = strInvtype;
                            item.Invstatus = "Release";
                            item.Shiptoparty = Convert.ToString(row["KUNNR"]);
                            item.Orderno = Convert.ToString(row["BSTKD"]);
                            item.Cusorderno = Convert.ToString(row["VBELV"]);
                            item.Cusordernotype = Convert.ToString(row["AUART"]);
                            item.Dnmuser = Convert.ToString(row["AENAM"]);
                            item.Dnmdate = Common.ParseInt(row["AEDAT"]);
                            item.Dnmtime = Common.ParseInt(row["AEZET"]);
                            item.Cusbatchno = Convert.ToString(row["KHPCH"]);
                            item.Shippinglocation = Convert.ToString(row["SHIPLOC"]);
                            item.Plangidate = Convert.ToInt32(row["WADAT"]);
                            item.Gfcontractno = Convert.ToString(row["IHREZ_PV"]);
                            item.Orderreason = Convert.ToString(row["AUGRU"]);
                            item.Postway = Convert.ToString(row["JFFS"]);
                            item.Pickcondition = Convert.ToString(row["JLTJ"]);
                            item.Gfflag = Convert.ToString(row["DN_YJGF"]);

                            item.Finishflag = "N";
                            item.MaintainUser = "JOB";
                            item.MaintainDate = intDate;
                            item.MaintainTime = intTime;
                            item.Cuser = "JOB";
                            item.Cdate = intDate;
                            item.Ctime = intTime;
                            list.Add(item);

                            _InvoicesFacade.AddInvoices(item);
                        }
                        #endregion

                        #region line
                        DBDateTime dbDateTime1 = FormatHelper.GetNowDBDateTime(DataProvider);
                        DateTime dtNow1 = FormatHelper.ToDateTime(dbDateTime1.DBDate, dbDateTime1.DBTime);
                        int intDate1 = FormatHelper.TODateInt(dtNow1);
                        int intTime1 = FormatHelper.TOTimeInt(dtNow1);
                        List<Material> materialList = this.GetAllMaterialList();
                        foreach (DataRow row in ds.Tables[DNOUTTABLE].Rows)
                        {
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
                            if (strDNBatchStatus == "Cancel" || strDNBatchStatus == "Pending") //Cancel和Pending的不处理
                            {
                                continue;
                            }
                            string strDNNO = Convert.ToString(row["VBELN"]);
                            if (row["POSNR"] == DBNull.Value)
                            {
                                continue;
                            }
                            int intInvLine = Convert.ToInt32(row["POSNR"]);
                            if (_InvoicesFacade.QueryInvoicesdetailCount(strDNNO, intInvLine) > 0)
                            {
                                continue;
                            }

                            Invoicesdetail detail = new Invoicesdetail();
                            detail.Invno = strDNNO;
                            detail.Invline = intInvLine;
                            if (row["UEPOS"] != DBNull.Value)
                            {
                                detail.Hignlevelitem = Convert.ToInt32(row["UEPOS"]);//有的记录是空值
                            }
                            detail.Mcode = Convert.ToString(row["MATNR"]);
                            detail.Faccode = Convert.ToString(row["WERKS"]);
                            detail.Storagecode = Convert.ToString(row["LGORT"]);
                            detail.Planqty = Convert.ToDecimal(row["LFIMG"]);
                            detail.Unit = Convert.ToString(row["VRKME"]);
                            detail.Movementtype = Convert.ToString(row["BWART"]);
                            detail.Cusmcode = Convert.ToString(row["EMPST"]);
                            detail.Cusitemspec = Convert.ToString(row["CMTART"]);
                            detail.Cusitemdesc = Convert.ToString(row["CMAKTX"]);
                            detail.Vendermcode = Convert.ToString(row["IDNLF"]);
                            detail.Gfhwmcode = Convert.ToString(row["EMPST_H"]);
                            detail.Gfpackingseq = Convert.ToString(row["POSEX"]);
                            detail.Gfhwdesc = Convert.ToString(row["CONTENT"]);
                            detail.Hwcodeqty = Convert.ToDecimal(row["B1_QTY"]);
                            detail.Hwcodeunit = Convert.ToString(row["UNITQTY"]);
                            detail.Hwtypeinfo = Convert.ToString(row["PARTID"]);
                            detail.Packingway = Convert.ToString(row["PAITEMTYPE"]);
                            detail.Packingno = Convert.ToString(row["BOX_POBJID"]);
                            detail.Packingspec = Convert.ToString(row["BOX_SIZE"]);
                            detail.Packingwayno = Convert.ToString(row["BOX_MTHOD"]);
                            detail.Dqsmcode = Convert.ToString(row["ZEINR"]);

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
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            #region 产生拣货任务令
            try
            {
                if (_InvoicesFacade == null)
                {
                    _InvoicesFacade = new InvoicesFacade(this.DataProvider);
                }
                this.DataProvider.BeginTransaction();
                object[] arrInvoices = _InvoicesFacade.GetDNInvoices();
                List<Invoices> invoicesList = Common.Array2InvoicesList(arrInvoices);
                var batchnoList = invoicesList.GroupBy(p => p.Dnbatchno).Select(p => p.Key).ToList();
                string tmpPickno = default(string);
                foreach (var batchno in batchnoList)
                {
                    //已经产生拣货任务令的发货批，不再处理
                    if (_InvoicesFacade.QueryPickCountByDNBatchNO(batchno) > 0)
                    {
                        continue;
                    }
                    List<Invoices> currInvoicesList = invoicesList.Where(o => o.Dnbatchno == batchno).ToList();
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
                    pick.Picktype = currInvoicesList.First().Invtype;
                    pick.Status = "Release";
                    pick.Invno = batchno;
                    //pick.Faccode = ""; //在DN行上
                    //pick.Storagecode = ""; //在DN行上
                    pick.Plangidate = currInvoicesList.OrderBy(o => o.Plangidate).First().Plangidate.ToString();
                    pick.Cusbatchno = currInvoicesList.First().Cusbatchno;
                    pick.Gfflag = currInvoicesList.First().Gfflag;

                    pick.MaintainUser = "JOB";
                    pick.MaintainDate = intDate;
                    pick.MaintainTime = intTime;
                    pick.Cuser = "JOB";
                    pick.Cdate = intDate;
                    pick.Ctime = intTime;

                    object[] arrInvoicesDetail = _InvoicesFacade.GetDNInvoicesDetailByDNBatchNO(batchno);
                    List<Invoicesdetail> invoicesDetailList = Common.Array2InvoicesDetailList(arrInvoicesDetail);
                    pick.Faccode = invoicesDetailList.First().Faccode;
                    //if (invoicesDetailList.First(o => string.IsNullOrEmpty(o.Storagecode) == false) == null)//
                    //{
                    //    pick.Storagecode = "";
                    //}
                    //else
                    //{
                    //    pick.Storagecode = invoicesDetailList.First(o => string.IsNullOrEmpty(o.Storagecode) == false).Storagecode;
                    //}
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


                        pickdetail.MaintainUser = "JOB";
                        pickdetail.MaintainDate = intDate1;
                        pickdetail.MaintainTime = intTime1;
                        pickdetail.Cuser = "JOB";
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
    }



}

