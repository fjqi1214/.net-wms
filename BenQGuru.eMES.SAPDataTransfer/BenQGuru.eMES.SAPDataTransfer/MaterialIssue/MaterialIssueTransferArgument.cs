using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using System.ComponentModel;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MaterialIssueTransferArgument
    {
        public MaterialIssueTransferArgument(IDomainDataProvider domainDataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(domainDataProvider);

            this.m_TransactionCode = TransferFacade.MaterialIssueJobID + "_"
                + currentDateTime.DBDate.ToString() 
                + "_" + currentDateTime.DBTime.ToString() 
                + DateTime.Now.Millisecond.ToString(); 
        }

        public void GenerateNewTransactionCode(IDomainDataProvider domainDataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(domainDataProvider);

            this.m_TransactionCode = TransferFacade.MaterialIssueJobID + "_"
                + currentDateTime.DBDate.ToString() 
                + "_" + currentDateTime.DBTime.ToString() 
                + DateTime.Now.Millisecond.ToString(); 
        
        }

        private string m_TransactionCode = "";
        [Description("每次发送请求固定发送该资料，不可修改")]
        [Category("固定传递参数")]
        [ReadOnly(true)]
        [DisplayName("TransactionCode")]
        public string TransactionCode
        {
            get { return m_TransactionCode; }
        }

        private List<DT_MES_TRANSFERPOST_REQTRANSFERITEM> m_InventoryList = new List<DT_MES_TRANSFERPOST_REQTRANSFERITEM>();
        [Description("出库信息列表")]
        [Category("运行参数")]
        [DisplayName("出库信息列表")]
        public List<DT_MES_TRANSFERPOST_REQTRANSFERITEM> InventoryList
        {
            get { return m_InventoryList; }
            set { m_InventoryList = value; }
        }
    }
}
