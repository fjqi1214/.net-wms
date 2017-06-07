using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using System.ComponentModel;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class DNConfrimArgument
    {
        public DNConfrimArgument(IDomainDataProvider dataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(dataProvider);

            this.m_TransactionCode = TransferFacade.DNConfirmJobID + "_"
                + currentDateTime.DBDate.ToString() 
                + "_" + currentDateTime.DBTime.ToString() 
                + DateTime.Now.Millisecond.ToString();
        }

        public void GenerateNewTransactionCode(IDomainDataProvider dataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(dataProvider);
            this.m_TransactionCode = TransferFacade.DNConfirmJobID + "_"
                + currentDateTime.DBDate.ToString() 
                + "_" + currentDateTime.DBTime.ToString() 
                + DateTime.Now.Millisecond.ToString();
        }

        private string m_TransactionCode = "";
        [Description("每次发送请求固定发送该资料，不可修改")]
        [Category("固定传递参数")]
        [DisplayName("TransactionCode")]
        [ReadOnly(true)]
        public string TransactionCode
        {
            get
            {
                return this.m_TransactionCode;
            }
        }

        private List<DT_MES_DNPOST> m_DNList = new List<DT_MES_DNPOST>();
        [Description("交货单列表")]
        [Category("运行参数")]
        [DisplayName("交货单列表")]
        public List<DT_MES_DNPOST> DNList
        {
            get
            {
                return this.m_DNList;
            }
            set
            {
                this.m_DNList = value;
            }
        }

    }
}
