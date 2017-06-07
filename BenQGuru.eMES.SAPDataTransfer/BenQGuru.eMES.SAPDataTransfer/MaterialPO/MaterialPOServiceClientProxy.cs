using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace BenQGuru.eMES.SAPDataTransfer
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "MI_MES_SOURCESTOCK_REQBinding", Namespace = "urn:mes2sap:SourceStock")]
    public partial class MaterialPOServiceClientProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        /// <remarks/>
        public MaterialPOServiceClientProxy()
        {
            this.Url = "http://172.16.41.107:50000/XISOAPAdapter/MessageServlet?channel=:BS_MESDEV:CC_MES" +
                "_SOURCESTOCK_REQ&version=3.0&Sender.Service=BS_MESDEV&Interface=urn%3Ames2sap%3A" +
                "SourceStock%5EMI_MES_SOURCESTOCK_REQ";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", OneWay = true, Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        public void MI_MES_SOURCESTOCK_REQ([System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:mes2sap:SourceStock")] DT_MES_SOURCESTOCK_REQ MT_MES_SOURCESTOCK_REQ)
        {
            this.Invoke("MI_MES_SOURCESTOCK_REQ", new object[] {
                        MT_MES_SOURCESTOCK_REQ});
        }

        /// <remarks/>
        public System.IAsyncResult BeginMI_MES_SOURCESTOCK_REQ(DT_MES_SOURCESTOCK_REQ MT_MES_SOURCESTOCK_REQ, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MI_MES_SOURCESTOCK_REQ", new object[] {
                        MT_MES_SOURCESTOCK_REQ}, callback, asyncState);
        }

        /// <remarks/>
        public void EndMI_MES_SOURCESTOCK_REQ(System.IAsyncResult asyncResult)
        {
            this.EndInvoke(asyncResult);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:mes2sap:SourceStock")]
    public partial class DT_MES_SOURCESTOCK_REQ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LIST", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_MES_SOURCESTOCK_REQLIST[] LIST;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TRANSCODE;

        public DT_MES_SOURCESTOCK_REQ()
        {
            this.TRANSCODE = "TRANSCODE";
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:mes2sap:SourceStock")]
    public partial class DT_MES_SOURCESTOCK_REQLIST
    {
        private string m_PSTNG_DATE = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("记账日期")]
        [Category("运行参数 - 收货信息")]
        public string PSTNG_DATE
        {
            get { return m_PSTNG_DATE; }
            set { m_PSTNG_DATE = value; }
        }

        private string m_DOC_DATE = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("凭证日期")]
        [Category("运行参数 - 收货信息")]
        public string DOC_DATE
        {
            get { return m_DOC_DATE; }
            set { m_DOC_DATE = value; }
        }

        private string m_PO_NUMBER = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("采购订单号")]
        [Category("运行参数 - 收货信息")]
        public string PO_NUMBER
        {
            get { return m_PO_NUMBER; }
            set { m_PO_NUMBER = value; }
        }

        private string m_PO_ITEM = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("订单行项目")]
        [Category("运行参数 - 收货信息")]
        public string PO_ITEM
        {
            get { return m_PO_ITEM; }
            set { m_PO_ITEM = value; }
        }

        private string m_MATERIAL = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("物料号")]
        [Category("运行参数 - 收货信息")]
        public string MATERIAL
        {
            get { return m_MATERIAL; }
            set { m_MATERIAL = value; }
        }

        private string m_PLANT = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("订单工厂")]
        [Category("运行参数 - 收货信息")]
        public string PLANT
        {
            get { return m_PLANT; }
            set { m_PLANT = value; }
        }

        private string m_STGE_LOC = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("收货地点")]
        [Category("运行参数 - 收货信息")]
        public string STGE_LOC
        {
            get { return m_STGE_LOC; }
            set { m_STGE_LOC = value; }
        }

        private string m_ENTRY_QNT = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("收货数量")]
        [Category("运行参数 - 收货信息")]
        public string ENTRY_QNT
        {
            get { return m_ENTRY_QNT; }
            set { m_ENTRY_QNT = value; }
        }

        private string m_ENTRY_UOM = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("单位")]
        [Category("运行参数 - 收货信息")]
        public string ENTRY_UOM
        {
            get { return m_ENTRY_UOM; }
            set { m_ENTRY_UOM = value; }
        }

        private string m_HEADER_TXT = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("抬头文本")]
        [Category("运行参数 - 收货信息")]
        public string HEADER_TXT
        {
            get { return m_HEADER_TXT; }
            set { m_HEADER_TXT = value; }
        }

        private string m_MOVE_TYPE = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("移动类型")]
        [Category("运行参数 - 收货信息")]
        public string MOVE_TYPE
        {
            get { return m_MOVE_TYPE; }
            set { m_MOVE_TYPE = value; }
        }

        public DT_MES_SOURCESTOCK_REQLIST()
        {
            this.PSTNG_DATE = DateTime.Now.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            this.DOC_DATE = DateTime.Now.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            this.PO_NUMBER = "";
            this.PO_ITEM = "";
            this.MATERIAL = "";
            this.PLANT = "";
            this.STGE_LOC = "";
            this.ENTRY_QNT = "";
            this.ENTRY_UOM = "";
            this.HEADER_TXT = "";
            this.MOVE_TYPE = "101";
        }
    }
}
