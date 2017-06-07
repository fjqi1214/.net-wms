using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using System.Data;
using BenQGuru.SAP.SAPNcoLib;
using BenQGuru.eMES.SAPRFCService.Domain;
using SAP.Middleware.Connector;

namespace BenQGuru.eMES.SAPRFCService
{
    public class WWPO2SAP
    {
        private IDomainDataProvider _domainDataProvider = null;

        public WWPO2SAP() { }

        public WWPO2SAP(IDomainDataProvider dataProvider)
        {
            _domainDataProvider = dataProvider;
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

        public WWPOComponentResult GetWWPOList(List<WWPOComponentPara> list)
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                ncoClient.FunctionName = MES2SAPRfcFunctionName.WWPOComponent;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();

                DataTable dt = new DataTable();
                dt.Columns.Add("EBELN", typeof(string));
                dt.Columns.Add("EBELP", typeof(int));

                foreach (var item in list)
                {
                    DataRow row = dt.NewRow();
                    row["EBELN"] = item.PONO;
                    //row["EBELP"] = item.POLine;
                    dt.Rows.Add(row);
                }

                IRfcTable rfcTable = ncoClient.ConvertDataTabletoRFCTable(dt, MES2SAPRfcInTableName.WWPOComponent);
                importParameters.Add(MES2SAPRfcInTableName.WWPOComponent, rfcTable);
                ncoClient.ImportParameters = importParameters;

                Dictionary<string, object> exportParameters = new Dictionary<string, object>();
                DataSet ds = ncoClient.Execute(ref exportParameters);
                WWPOComponentResult result = new WWPOComponentResult();

                List<WWPOComponent> componentList = new List<WWPOComponent>();
                if (ds != null && ds.Tables[MES2SAPRfcOutTableName.WWPOComponent] != null && ds.Tables[MES2SAPRfcOutTableName.WWPOComponent].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[MES2SAPRfcOutTableName.WWPOComponent].Rows)
                    {
                        WWPOComponent co = new WWPOComponent();
                        co.PONO = Convert.ToString(row["EBELN"]);
                        co.POLine = Convert.ToInt32(row["EBELP"]);
                        co.SubLine = Convert.ToInt32(row["RSPOS"]);
                        co.MCode = Convert.ToString(row["MATNR"]);
                        co.HWMCode = Convert.ToString(row["IDNLF"]);
                        co.Qty = Convert.ToDecimal(row["BDMNG"]);
                        co.Unit = Convert.ToString(row["MEINS"]);
                        componentList.Add(co);
                    }
                }
                result.WWPOComponentList = componentList;

                object parameter = null;
                SAPRfcReturn re = new SAPRfcReturn();
                if (exportParameters != null)
                {
                    if (exportParameters.TryGetValue("ES_RESULT", out parameter))
                    {
                        IRfcStructure ROFStrcture = parameter as IRfcStructure;
                        re.Result = ROFStrcture["RETUN"].GetValue().ToString();
                        re.MaterialDocument = null;
                        re.Message = ROFStrcture["MESSG"].GetValue().ToString();
                    }
                }
                result.RfcResult = re;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SAPRfcReturn PostWWPOToSAP(List<WWPO> wwpoList)
        {
            try
            {
                NcoFunction ncoClient = new NcoFunction();
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);

                ncoClient.FunctionName = MES2SAPRfcFunctionName.WWPO;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();

                DataTable dtDetail = new DataTable();
                dtDetail.Columns.Add("EBELN", typeof(string));
                dtDetail.Columns.Add("EBELP", typeof(int));
                dtDetail.Columns.Add("LIFNR", typeof(string));
                dtDetail.Columns.Add("MATNR", typeof(string));
                dtDetail.Columns.Add("BWART", typeof(string));
                dtDetail.Columns.Add("MENGE", typeof(decimal));
                dtDetail.Columns.Add("MEINS", typeof(string));
                dtDetail.Columns.Add("WERKS", typeof(string));
                dtDetail.Columns.Add("LGORT", typeof(string));
                dtDetail.Columns.Add("ZNUMBER", typeof(string));
                dtDetail.Columns.Add("DATUM", typeof(int));
                dtDetail.Columns.Add("SGTXT", typeof(string));

                foreach (var wwpo in wwpoList)
                {
                    DataRow row = dtDetail.NewRow();
                    row["EBELN"] = wwpo.PONO;
                    row["EBELP"] = wwpo.POLine;
                    row["LIFNR"] = wwpo.VendorCode;
                    row["MATNR"] = wwpo.MCode;
                    row["BWART"] = wwpo.InOutFlag;
                    row["MENGE"] = wwpo.Qty;
                    row["MEINS"] = wwpo.Unit;
                    row["WERKS"] = wwpo.FacCode;
                    row["LGORT"] = wwpo.StorageCode;
                    row["ZNUMBER"] = wwpo.MesTransNO;
                    row["DATUM"] = wwpo.MesTransDate;
                    row["SGTXT"] = wwpo.Remark;
                    dtDetail.Rows.Add(row);
                }

                IRfcTable rfcTable = ncoClient.ConvertDataTabletoRFCTable(dtDetail, MES2SAPRfcInTableName.WWPO);

                importParameters.Add(MES2SAPRfcInTableName.WWPO, rfcTable);
                ncoClient.ImportParameters = importParameters;
                Dictionary<string, object> exportParameters = new Dictionary<string, object>();
                ncoClient.Execute(ref exportParameters);
                object parameter = null;
                SAPRfcReturn re = new SAPRfcReturn();
                if (exportParameters != null)
                {
                    if (exportParameters.TryGetValue("ES_RESULT", out parameter))
                    {
                        IRfcStructure ROFStrcture = parameter as IRfcStructure;
                        re.Result = ROFStrcture["RETUN"].GetValue().ToString();
                        re.MaterialDocument = ROFStrcture["MBLNR"].GetValue().ToString();
                        re.Message = ROFStrcture["MESSG"].GetValue().ToString();
                    }
                }
                return re;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
