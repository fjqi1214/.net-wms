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
    public class RSToSAP
    {
        private IDomainDataProvider _domainDataProvider = null;

        public RSToSAP() { }

        public RSToSAP(IDomainDataProvider dataProvider)
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

        public SAPRfcReturn PostRSToSAP(List<RS> rsList)
        {
            NcoFunction ncoClient = new NcoFunction();
            try
            {
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);

                ncoClient.FunctionName = MES2SAPRfcFunctionName.RS;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();

                DataTable dtDetail = new DataTable();
                dtDetail.Columns.Add("RSNUM", typeof(string));
                dtDetail.Columns.Add("RSPOS", typeof(int));
                dtDetail.Columns.Add("BWART", typeof(string));
                dtDetail.Columns.Add("WERKS", typeof(string));
                dtDetail.Columns.Add("MATNR", typeof(string));
                dtDetail.Columns.Add("LGORT", typeof(string));
                dtDetail.Columns.Add("MENGE", typeof(decimal));
                dtDetail.Columns.Add("MEINS", typeof(string));
                dtDetail.Columns.Add("ZNUMBER", typeof(string));
                dtDetail.Columns.Add("DATUM", typeof(int));

                foreach (var rs in rsList)
                {
                    DataRow row = dtDetail.NewRow();
                    row["RSNUM"] = rs.RSNO;
                    row["RSPOS"] = rs.RSLine;
                    row["BWART"] = rs.InOutFlag;
                    row["WERKS"] = rs.FacCode;
                    row["MATNR"] = rs.MCode;
                    row["LGORT"] = rs.StorageCode;
                    row["MENGE"] = rs.Qty;
                    row["MEINS"] = rs.Unit;
                    row["ZNUMBER"] = rs.MesTransNO;
                    row["DATUM"] = rs.DocumentDate;
                    dtDetail.Rows.Add(row);
                }

                IRfcTable rfcTable = ncoClient.ConvertDataTabletoRFCTable(dtDetail, MES2SAPRfcInTableName.RS);

                importParameters.Add(MES2SAPRfcInTableName.RS, rfcTable);
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
