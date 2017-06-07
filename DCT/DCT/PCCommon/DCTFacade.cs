using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.Common.DCT.PC
{
    public class DCTFacade
    {
        private SQLDomainDataProvider _DataProvider = null;

        public DCTFacade(SQLDomainDataProvider dataProvider)
        {
            _DataProvider = dataProvider;
        }

        public SQLDomainDataProvider DataProvider
        {
            set
            {
                _DataProvider = value;
            }
            get
            {
                return _DataProvider;
            }
        }

        public DCTMessage CreateNewDCTMessage()
        {
            DCTMessage dctMessage = new DCTMessage();

            dctMessage.SerialNo = this.DataProvider.GetCount(new SQLCondition("SELECT SEQ_TBLDCTMESSAGE.nextval FROM dual "));
            
            return dctMessage;
        }

        public void AddDCTMessage(DCTMessage dctMessage)
        {
            this.DataProvider.Insert(dctMessage);
        }

        public void UpdateDCTMessage(DCTMessage dctMessage)
        {
            this.DataProvider.Update(dctMessage);
        }

        public void DeleteDCTMessage(DCTMessage dctMessage)
        {
            this.DataProvider.Delete(dctMessage);
        }

        public object GetDCTMessage(int serialNo)
        {
            return this.DataProvider.CustomSearch(typeof(DCTMessage), new object[] { serialNo });
        }

        public object[] QueryNewDCTMessage(string direction, string toAddress, int toPort, int count)
        {
            string sql = "SELECT {0} FROM tbldctmessage WHERE status = '{1}' AND direction = '{2}' AND toaddress = '{3}' AND toport = {4} ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(DCTMessage)), DCTMessageStatus.New, direction, toAddress, toPort.ToString());

            object[] list = this.DataProvider.CustomQuery(typeof(DCTMessage), new PagerCondition(sql, "serialno", 1, count));

            if (list == null)
            {
                return null;
            }
            else
            {
                return list;
            }
        }

        public void ClearAllDCTMessage()
        {
            this.DataProvider.CustomExecute(new SQLCondition("DELETE FROM tbldctmessage "));
        }
    }
}
