using System;
using System.Collections.Generic;
using System.Text;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.BaseSetting;
using System.ComponentModel;

namespace BenQGuru.eMES.SAPDataTransfer
{
    public class InventoryTransferArgument
    {
        public InventoryTransferArgument(IDomainDataProvider domainDataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(domainDataProvider);

            this.m_TransactionCode = TransferFacade.InvertoryJobID + "_"
                + currentDateTime.DBDate.ToString() 
                + "_" + currentDateTime.DBTime.ToString() 
                + DateTime.Now.Millisecond.ToString(); 
        }

        public void GenerateNewTransactionCode(IDomainDataProvider domainDataProvider)
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(domainDataProvider);

            this.m_TransactionCode = TransferFacade.InvertoryJobID + "_"
                + currentDateTime.DBDate.ToString() 
                + "_" + currentDateTime.DBTime.ToString() 
                + DateTime.Now.Millisecond.ToString(); 
        }

        private string[] m_OrgList;
        [Description("工厂列表")]
        [DisplayName("工厂列表")]
        [Category("运行参数")]
        public string[] OrgList
        {
            get { return m_OrgList; }
            set { m_OrgList = value; }
        }

        private string m_MaterialNumber = string.Empty;
        [Description("物料号")]
        [DisplayName("物料号")]
        [Category("运行参数")]
        public string MaterialNumber
        {
            get { return m_MaterialNumber; }
            set { m_MaterialNumber = value; }
        }

        private string[] m_Location;
        [Description("库存地点")]
        [DisplayName("库存地点")]
        [Category("运行参数")]
        public string[] Location
        {
            get { return m_Location; }
            set { m_Location = value; }
        }


        private string m_TransactionCode = "";
        [Description("每次发送请求固定发送该资料，不可修改")]
        [Category("固定传递参数")]
        [ReadOnly(true)]
        public string TransactionCode
        {
            get { return m_TransactionCode; }
        }	
        
    }
}
