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
    public class StockScrapToSAP
    {
        private IDomainDataProvider _domainDataProvider = null;

        public StockScrapToSAP() { }

        public StockScrapToSAP(IDomainDataProvider dataProvider)
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

        public SAPRfcReturn PostStockScrapToSAP(List<StockScrap> ssList)
        {
            NcoFunction ncoClient = new NcoFunction();
            try
            {
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);

                ncoClient.FunctionName = MES2SAPRfcFunctionName.StockScrap;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();

                DataTable dtDetail = new DataTable();
                dtDetail.Columns.Add("ZNUMBER", typeof(string));
                dtDetail.Columns.Add("ZEILE", typeof(int));
                dtDetail.Columns.Add("MATNR", typeof(string));
                dtDetail.Columns.Add("BWART", typeof(string));
                dtDetail.Columns.Add("WERKS", typeof(string));
                dtDetail.Columns.Add("LGORT", typeof(string));
                dtDetail.Columns.Add("MENGE", typeof(decimal));
                dtDetail.Columns.Add("MEINS", typeof(string));
                dtDetail.Columns.Add("KOSTL", typeof(string));
                dtDetail.Columns.Add("SGTXT", typeof(string));
                dtDetail.Columns.Add("DATUM", typeof(int));
                dtDetail.Columns.Add("UNAME", typeof(string));

                foreach (var item in ssList)
                {
                    DataRow row = dtDetail.NewRow();
                    row["ZNUMBER"] = item.MESScrapNO;
                    row["ZEILE"] = item.LineNO;
                    row["MATNR"] = item.MCode;
                    row["BWART"] = item.ScrapCode;
                    row["WERKS"] = item.FacCode;
                    row["LGORT"] = item.StorageCode;
                    row["MENGE"] = item.Qty;
                    row["MEINS"] = item.Unit;
                    row["KOSTL"] = item.CC;
                    row["SGTXT"] = item.Remark;
                    row["DATUM"] = item.DocumentDate;
                    row["UNAME"] = item.Operator;
                    dtDetail.Rows.Add(row);
                }

                IRfcTable rfcTable = ncoClient.ConvertDataTabletoRFCTable(dtDetail, MES2SAPRfcInTableName.StockScrap);

                importParameters.Add(MES2SAPRfcInTableName.StockScrap, rfcTable);
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
