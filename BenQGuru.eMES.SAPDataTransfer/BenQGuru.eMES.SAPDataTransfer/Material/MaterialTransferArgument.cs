using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class MaterialTransferArgument
    {
        public MaterialTransferArgument(IDomainDataProvider dataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(dataProvider);
            DateTime wantedDateTime = FormatHelper.ToDateTime(currentDateTime.DBDate, currentDateTime.DBTime).AddDays(InternalVariables.MS_DateOffSet);

            this.m_MaintainDate_B = wantedDateTime.Date;
            this.m_MaintainDate_E = wantedDateTime.Date;
            this.m_OrgID = InternalVariables.MS_OrganizationID;

            this.m_TransactionCode = TransferFacade.MaterialTransferJobID + "_" 
                + currentDateTime.DBDate.ToString() + "_" + currentDateTime.DBTime.ToString() + DateTime.Now.Millisecond.ToString();
        }

        public void GenerateNewTransactionCode(IDomainDataProvider dataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(dataProvider);
            this.m_TransactionCode = TransferFacade.MaterialTransferJobID + "_"
                + currentDateTime.DBDate.ToString() + "_" + currentDateTime.DBTime.ToString() + DateTime.Now.Millisecond.ToString();
        }

        private DateTime m_MaintainDate_B;
        [Description("开始日期")]
        [DisplayName("维护日期--开始日期")]
        [Category("运行参数")]
        public DateTime MaintainDate_B
        {
            get { return m_MaintainDate_B; }
            set { m_MaintainDate_B = value; }
        }

        private DateTime m_MaintainDate_E;
        [Description("结束日期")]
        [DisplayName("维护日期--结束日期")]
        [Category("运行参数")]
        public DateTime MaintainDate_E
        {
            get { return m_MaintainDate_E; }
            set { m_MaintainDate_E = value; }
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
