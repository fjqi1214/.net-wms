using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.SAP.SAPNcoLib;
using System.Data;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.SAPRFCService.MainData
{
    public class Customer
    {
        private const string FunctionName = "ZCHN_SD_CUSTOMER_GET";//方法名称

        //输出
        private const string OUTTABLE = "Z_OUT";//输出的Table
        private const string OUT_ReceiptNO = "MBLNR";//入库凭证号
        private const string OUT_FacLotNo = "CHARG";//厂内批次号


        private IDomainDataProvider _domainDataProvider = null;

        public Customer() { }

        public Customer(IDomainDataProvider dataProvider) 
        {
            this._domainDataProvider = dataProvider;
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

        public DataSet OrderBackFromSAP(string ReceiptNO, string year)
        {
            NcoFunction ncoClient = new NcoFunction();

            try
            {

                ncoClient.Connect();
                ncoClient.FunctionName = FunctionName;//Rfc function name

                Dictionary<string, object> importParameters = new Dictionary<string, object>();
                importParameters.Add("RECEIPTNO", ReceiptNO);
                importParameters.Add("YEAR", year);
                ncoClient.ImportParameters = importParameters;

                DataSet ds = ncoClient.Execute();
                ds = RFCUtil.ReplaceDataSet(ds);

                ncoClient.DISConncet();

                LogData(ds);

                return ds;
            }
            catch (Exception e)
            {
                ncoClient.DISConncet();
                throw e;
            }
        }

        private void LogData(DataSet ds)
        {
            if (ds != null && ds.Tables[OUTTABLE] != null)
            {
                if (ds.Tables[OUTTABLE].Rows.Count > 0)
                {
                    try
                    {
                        string strInsert;
                        string time = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        foreach (DataRow row in ds.Tables[OUTTABLE].Rows)
                        {
                            string receiptNO = Convert.ToString(row[OUT_ReceiptNO]);
                            string facLotNo = Convert.ToString(row[OUT_FacLotNo]);

                            strInsert = @"INSERT INTO  i_tblinvfaclot
                                         (receiptno,faclotno,batchno) VALUES(
                                            '" + receiptNO + "','" + facLotNo + "','" + time + "')";

                            SQLCondition SC = new SQLCondition(strInsert);
                            this.DataProvider.CustomExecute(SC);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
