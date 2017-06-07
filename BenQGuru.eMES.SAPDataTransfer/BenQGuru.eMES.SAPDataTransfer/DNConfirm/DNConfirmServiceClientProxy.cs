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
    [System.Web.Services.WebServiceBindingAttribute(Name = "MI_MES_DNPOST_REQBinding", Namespace = "urn:mes2sap:dnpost")]
    public partial class DNConfirmServiceClientProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        /// <remarks/>
        public DNConfirmServiceClientProxy()
        {
            this.Url = "http://172.16.41.107:50000/XISOAPAdapter/MessageServlet?channel=:BS_MESDEV:CC_SOA" +
                "P_DEVLIVERY_POST&version=3.0&Sender.Service=BS_MESDEV&Interface=urn%3Ames2sap%3A" +
                "dnpost%5EMI_MES_DNPOST_REQ";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", OneWay = true, Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        public void MI_MES_DNPOST_REQ([System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:mes2sap:dnpost")] DT_MES_DNPOST_REQ MT_MES_DNPOST_REQ)
        {
            this.Invoke("MI_MES_DNPOST_REQ", new object[] {
                        MT_MES_DNPOST_REQ});
        }

        /// <remarks/>
        public System.IAsyncResult BeginMI_MES_DNPOST_REQ(DT_MES_DNPOST_REQ MT_MES_DNPOST_REQ, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MI_MES_DNPOST_REQ", new object[] {
                        MT_MES_DNPOST_REQ}, callback, asyncState);
        }

        /// <remarks/>
        public void EndMI_MES_DNPOST_REQ(System.IAsyncResult asyncResult)
        {
            this.EndInvoke(asyncResult);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:mes2sap:dnpost")]
    public partial class DT_MES_DNPOST_REQ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TRANS;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DNPARAM", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_MES_DNPOST[] DNPARAM;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:mes2sap:dnpost")]
    public partial class DT_MES_DNPOST
    {
        private string m_VBELN = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("交货单号")]
        public string VBELN
        {
            get { return m_VBELN; }
            set { m_VBELN = value; }
        }

        private string m_POSNR = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("交货单行项目")]
        public string POSNR
        {
            get { return m_POSNR; }
            set { m_POSNR = value; }
        }

        private string m_MATNR = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("物料编号")]
        public string MATNR
        {
            get { return m_MATNR; }
            set { m_MATNR = value; }
        }

        private string m_G_LFIMG = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("交货数量")]
        public string G_LFIMG
        {
            get { return m_G_LFIMG; }
            set { m_G_LFIMG = value; }
        }

        private string m_LGORT = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [DisplayName("库存地点")]
        public string LGORT
        {
            get { return m_LGORT; }
            set { m_LGORT = value; }
        }
    }
}
