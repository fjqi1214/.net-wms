using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using System.ComponentModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.IQC;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MaterialPOTransferArgument
    {
        public MaterialPOTransferArgument(IDomainDataProvider domainDataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(domainDataProvider);

            this.m_TransactionCode = TransferFacade.MaterialPOJobID + "_"
                + currentDateTime.DBDate.ToString()
                + "_" + currentDateTime.DBTime.ToString()
                + DateTime.Now.Millisecond.ToString();
        }

        public void GenerateNewTransactionCode(IDomainDataProvider domainDataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(domainDataProvider);

            this.m_TransactionCode = TransferFacade.MaterialPOJobID + "_"
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

        private string m_IQCNo = string.Empty;
        [Description("IQC单号")]
        [Category("运行参数 - IQC信息")]
        [DisplayName("IQC单号")]
        public string IQCNo
        {
            get { return m_IQCNo; }
            set { m_IQCNo = value; }
        }

        private int m_STLine = 0;
        [Description("IQC行号")]
        [Category("运行参数 - IQC信息")]
        [DisplayName("IQC行号")]
        public int STLine
        {
            get { return m_STLine; }
            set { m_STLine = value; }
        }                
    }
}
