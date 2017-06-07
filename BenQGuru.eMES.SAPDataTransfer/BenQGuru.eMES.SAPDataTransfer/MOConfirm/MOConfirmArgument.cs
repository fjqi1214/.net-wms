using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MOConfirmArgument
    {
        public MOConfirmArgument(IDomainDataProvider dataProvider)
        {
            this.GenerateNewTransactionCode(dataProvider);

        }

        public void GenerateNewTransactionCode(IDomainDataProvider dataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(dataProvider);
            this.m_TransactionCode = TransferFacade.MOCompleteTransferJobID + "_"
                + currentDateTime.DBDate.ToString() + "_" + currentDateTime.DBTime.ToString() + DateTime.Now.Millisecond.ToString();
        }

        private List<DT_MES_POCONFIRM_REQPOLIST> m_MOList;
        [Description("工单列表")]
        [Category("运行参数")]
        public List<DT_MES_POCONFIRM_REQPOLIST> MOList
        {
            get { return m_MOList; }
            set { m_MOList = value; }
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

        private int m_OrgID;
        [Description("组织代码")]
        [DisplayName("组织代码")]
        [Category("运行参数")]
        public int OrgID
        {
            get { return m_OrgID; }
            set { m_OrgID = value; }
        }
    }
}
