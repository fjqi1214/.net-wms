using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MOBOMTransferArgument
    {
        public MOBOMTransferArgument(IDomainDataProvider dataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(dataProvider);
            this.m_TransactionCode = TransferFacade.MOMaterialTransferJobID + "_"
                + currentDateTime.DBDate.ToString() + "_" + currentDateTime.DBTime.ToString() + DateTime.Now.Millisecond.ToString();
        }

        public void GenerateNewTransactionCode(IDomainDataProvider dataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(dataProvider);
            this.m_TransactionCode = TransferFacade.MOMaterialTransferJobID + "_"
                + currentDateTime.DBDate.ToString() + "_" + currentDateTime.DBTime.ToString() + DateTime.Now.Millisecond.ToString();
        }

        private string[] m_MOCodeList;
        [Description("工单列表")]
        [DisplayName("工单列表")]
        [Category("运行参数")]
        public string[] MOCodeList
        {
            get { return m_MOCodeList; }
            set { m_MOCodeList = value; }
        }

        private string m_TransactionCode = "";
        [Description("每次发送请求固定发送该资料，不可修改")]
        [Category("固定传递参数")]
        [ReadOnly(true)]
        public string TransactionCode
        {
            get
            {
                return this.m_TransactionCode;
            }
        }
    }
}
