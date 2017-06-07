using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.SAP.SAPNcoLib;
using System.Data;
using SAP.Middleware.Connector;
using BenQGuru.eMES.SAPRFCService.Domain;

namespace BenQGuru.eMES.SAPRFCService
{
    public class DNToSAP
    {
        private IDomainDataProvider _domainDataProvider = null;

        public DNToSAP() { }

        public DNToSAP(IDomainDataProvider dataProvider)
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

        public SAPRfcReturn DNPGIToSAP(List<DN> dnList, bool isAll)
        {
            NcoFunction ncoClient = new NcoFunction();
            try
            {
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);

                ncoClient.FunctionName = MES2SAPRfcFunctionName.PGI;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                DataTable dtDetail = new DataTable();
                dtDetail.Columns.Add("PCBH", typeof(string));
                dtDetail.Columns.Add("VBELN", typeof(string));
                if (!isAll)
                {
                    dtDetail.Columns.Add("POSNR", typeof(int));
                    dtDetail.Columns.Add("KWMENG", typeof(decimal));
                }
                foreach (var dn in dnList)
                {
                    DataRow row = dtDetail.NewRow();
                    row["PCBH"] = dn.BatchNO;
                    row["VBELN"] = dn.DNNO;
                    if (!isAll)
                    {
                        row["POSNR"] = dn.DNLine;
                        row["KWMENG"] = dn.Qty;
                    }
                    dtDetail.Rows.Add(row);
                }

                IRfcTable rfcTable = ncoClient.ConvertDataTabletoRFCTable(dtDetail, MES2SAPRfcInTableName.DN);

                importParameters.Add(MES2SAPRfcInTableName.DN, rfcTable);
                string flag = isAll ? "X" : string.Empty;//是否整单过账，=是，不需要给DN明细,空=否，需要给DN明细
                importParameters.Add("I_ZFLAG", flag);

                ncoClient.ImportParameters = importParameters;
                Dictionary<string, object> exportParameters = new Dictionary<string, object>();
                ncoClient.Execute(ref exportParameters);
                //return exportParameters;

                object parameter = null;
                SAPRfcReturn re = new SAPRfcReturn();
                if (exportParameters != null)
                {
                    if (exportParameters.TryGetValue("ES_RESULT", out parameter))
                    {
                        IRfcStructure ROFStrcture = parameter as IRfcStructure;
                        re.Result = ROFStrcture["RETUN"].GetValue().ToString();
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

        public SAPRfcReturn DNRePGIToSAP(DN dn)
        {
            NcoFunction ncoClient = new NcoFunction();
            try
            {
                string[] rfcArray = ConfigHelper.LoadRFCConfig(ConfigHelper.strDestinationName);
                ncoClient.Connect(rfcArray[0], "", rfcArray[3], rfcArray[4], "ZH", rfcArray[5], rfcArray[8], 2, 10, "", rfcArray[9]);

                ncoClient.FunctionName = MES2SAPRfcFunctionName.RePGI;

                Dictionary<string, object> importParameters = new Dictionary<string, object>();

                importParameters.Add("I_VBELN", dn.DNNO);

                ncoClient.ImportParameters = importParameters;
                Dictionary<string, object> exportParameters = new Dictionary<string, object>();
                ncoClient.Execute(ref exportParameters);
                //return exportParameters;

                object parameter = null;
                SAPRfcReturn re = new SAPRfcReturn();
                if (exportParameters != null)
                {
                    if (exportParameters.TryGetValue("ES_RESULT", out parameter))
                    {
                        IRfcStructure ROFStrcture = parameter as IRfcStructure;
                        re.Result = ROFStrcture["RETUN"].GetValue().ToString();
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
