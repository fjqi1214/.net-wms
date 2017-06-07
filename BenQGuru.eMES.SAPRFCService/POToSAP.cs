using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.SAP.SAPNcoLib;
using BenQGuru.eMES.SAPRFCService.Domain;
using System.Data;
using SAP.Middleware.Connector;

namespace BenQGuru.eMES.SAPRFCService
{
    public class POToSAP
    {
        private IDomainDataProvider _domainDataProvider = null;

        public POToSAP() { }

        public POToSAP(IDomainDataProvider dataProvider)
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

        public SAPRfcReturn POReceiveToSAP(List<PO> poList)
        {
            NcoFunction ncoClient = new NcoFunction();
            try
            {
                //string name = "Q97";
                //string user = "MESUSER";
                //string passwd = "start567";
                //string client = "601";
                //string ashost = "218.30.179.199";
                //string sysnr = "00";
                ////string saprouter = "/H/218.30.179.199/S/3299/H/192.168.129.249";
                //string saprouter = "/H/192.168.129.252/S/3299/H/192.168.129.249";
                //string kunnr = "";
                //ncoClient.Connect(name, "", user, passwd, "ZH", client, sysnr, 2, 10, "", saprouter);
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);
                //ncoClient.Connect();

                ncoClient.FunctionName = MES2SAPRfcFunctionName.PO;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                //importParameters.Add("EBELN", po.PONO);
                //importParameters.Add("EBELP", po.POLine);
                //importParameters.Add("WERKS", po.FacCode);
                //importParameters.Add("ZNUMBER", po.SerialNO);
                //importParameters.Add("MATNR", po.MCode);
                //importParameters.Add("MENGE", po.Qty);
                //importParameters.Add("MEINS", po.Unit);
                //importParameters.Add("BWART", po.Status);
                //importParameters.Add("REDOC", po.SAPMaterialInvoice);
                //importParameters.Add("SGTXT", po.Operator);
                //importParameters.Add("XBLNR", po.VendorInvoice);
                //importParameters.Add("LGORT", po.StorageCode);
                //importParameters.Add("BKTXT", po.Remark);
                //importParameters.Add("DATUM", po.InvoiceDate);

                DataTable dtDetail = new DataTable();
                dtDetail.Columns.Add("EBELN", typeof(string));
                dtDetail.Columns.Add("EBELP", typeof(int));
                dtDetail.Columns.Add("WERKS", typeof(string));
                dtDetail.Columns.Add("ZNUMBER", typeof(string));
                dtDetail.Columns.Add("MATNR", typeof(string));
                dtDetail.Columns.Add("MENGE", typeof(decimal));
                dtDetail.Columns.Add("MEINS", typeof(string));
                dtDetail.Columns.Add("BWART", typeof(string));
                dtDetail.Columns.Add("REDOC", typeof(string));
                dtDetail.Columns.Add("SGTXT", typeof(string));
                dtDetail.Columns.Add("XBLNR", typeof(string));
                dtDetail.Columns.Add("LGORT", typeof(string));
                dtDetail.Columns.Add("BKTXT", typeof(string));
                dtDetail.Columns.Add("DATUM", typeof(int));
                foreach (var po in poList)
                {
                    DataRow row = dtDetail.NewRow();
                    row["EBELN"] = po.PONO;
                    row["EBELP"] = po.POLine;
                    row["WERKS"] = po.FacCode;
                    row["ZNUMBER"] = po.SerialNO;
                    row["MATNR"] = po.MCode;
                    row["MENGE"] = po.Qty;
                    row["MEINS"] = po.Unit;
                    row["BWART"] = po.Status;
                    row["REDOC"] = po.SAPMaterialInvoice;
                    row["SGTXT"] = po.Operator;
                    row["XBLNR"] = po.VendorInvoice;
                    row["LGORT"] = po.StorageCode;
                    row["BKTXT"] = po.Remark;
                    row["DATUM"] = po.InvoiceDate;
                    dtDetail.Rows.Add(row);
                }

                IRfcTable rfcTable = ncoClient.ConvertDataTabletoRFCTable(dtDetail, MES2SAPRfcInTableName.PO);

                importParameters.Add(MES2SAPRfcInTableName.PO, rfcTable);
                ncoClient.ImportParameters = importParameters;
                Dictionary<string, object> exportParameters = new Dictionary<string, object>();
                ncoClient.Execute(ref exportParameters);
                //ncoClient.DISConncet();
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
                //ncoClient.DISConncet();
                throw ex;
            }
        }

    }
}
