using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System;
using System.Xml.Serialization;

namespace BenQGuru.eMES.SAPDataTransfer
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "MI_MES_TRANSFERPOST_REQBinding", Namespace = "urn:sap2mes:transferpost_ii")]
    public partial class MaterialIssueServiceClientProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        /// <remarks/>
        public MaterialIssueServiceClientProxy()
        {
            this.Url = "http://172.16.41.107:50000/XISOAPAdapter/MessageServlet?channel=:BS_MESDEV:CC_SOA" +
                "P_TANSFERPOST&version=3.0&Sender.Service=BS_MESDEV&Interface=urn%3Asap2mes%3Atra" +
                "nsferpost_ii%5EMI_MES_TRANSFERPOST_REQ";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", OneWay = true, Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        public void MI_MES_TRANSFERPOST_REQ([System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:sap2mes:transferpost_ii")] DT_MES_TRANSFERPOST_REQ MT_MES_TRANSFERPOST_REQ)
        {
            this.Invoke("MI_MES_TRANSFERPOST_REQ", new object[] {
                        MT_MES_TRANSFERPOST_REQ});
        }

        /// <remarks/>
        public System.IAsyncResult BeginMI_MES_TRANSFERPOST_REQ(DT_MES_TRANSFERPOST_REQ MT_MES_TRANSFERPOST_REQ, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MI_MES_TRANSFERPOST_REQ", new object[] {
                        MT_MES_TRANSFERPOST_REQ}, callback, asyncState);
        }

        /// <remarks/>
        public void EndMI_MES_TRANSFERPOST_REQ(System.IAsyncResult asyncResult)
        {
            this.EndInvoke(asyncResult);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:sap2mes:transferpost_ii")]
    public partial class DT_MES_TRANSFERPOST_REQ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TRANSCODE;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("TRANSFERITEM", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_MES_TRANSFERPOST_REQTRANSFERITEM[] TRANSFERITEM;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:sap2mes:transferpost_ii")]
    public partial class DT_MES_TRANSFERPOST_REQTRANSFERITEM
    {
        private string m_PSTNG_DATE = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("记账日期")]
        public string PSTNG_DATE
        {
            get { return m_PSTNG_DATE; }
            set { m_PSTNG_DATE = value; }
        }

        private string m_DOC_DATE = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("凭证日期")]
        public string DOC_DATE
        {
            get { return m_DOC_DATE; }
            set { m_DOC_DATE = value; }
        }

        private string m_MATERIAL = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("物料号")]
        public string MATERIAL
        {
            get { return m_MATERIAL; }
            set { m_MATERIAL = value; }
        }

        private string m_PLANT = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("订单工厂")]
        public string PLANT
        {
            get { return m_PLANT; }
            set { m_PLANT = value; }
        }

        private string m_STGE_LOC = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("发货地点")]
        public string STGE_LOC
        {
            get { return m_STGE_LOC; }
            set { m_STGE_LOC = value; }
        }

        private string m_MOVE_STLOC = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("收货地点")]
        public string MOVE_STLOC
        {
            get { return m_MOVE_STLOC; }
            set { m_MOVE_STLOC = value; }
        }

        private string m_ENTRY_QNT = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("发货数量")]
        public string ENTRY_QNT
        {
            get { return m_ENTRY_QNT; }
            set { m_ENTRY_QNT = value; }
        }

        private string m_ENTRY_UOM = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("发货单位")]
        public string ENTRY_UOM
        {
            get { return m_ENTRY_UOM; }
            set { m_ENTRY_UOM = value; }
        }

        private string m_HEADER_TXT = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("抬头文本")]
        public string HEADER_TXT
        {
            get { return m_HEADER_TXT; }
            set { m_HEADER_TXT = value; }
        }

        private string m_MOVE_TYPE = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("移动类型")]
        public string MOVE_TYPE
        {
            get { return m_MOVE_TYPE; }
            set { m_MOVE_TYPE = value; }
        }

        private string m_VENDOR = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("供应商编码")]
        public string VENDOR
        {
            get { return m_VENDOR; }
            set { m_VENDOR = value; }
        }

        private string m_REF_DOC_NO = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("物料单")]
        public string REF_DOC_NO
        {
            get { return m_REF_DOC_NO; }
            set { m_REF_DOC_NO = value; }
        }

        public DT_MES_TRANSFERPOST_REQTRANSFERITEM()
        {
            this.PSTNG_DATE = DateTime.Now.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            this.DOC_DATE = DateTime.Now.Date.ToString("yyyyMMdd", new System.Globalization.CultureInfo("en-US"));
            this.MOVE_TYPE = "411";
        }
    }
}
