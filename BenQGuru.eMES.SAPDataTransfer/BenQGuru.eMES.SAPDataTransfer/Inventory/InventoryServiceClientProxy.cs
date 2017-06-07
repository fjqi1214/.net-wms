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
    [System.Web.Services.WebServiceBindingAttribute(Name = "MI_MES_MATSTARCE_REQBinding", Namespace = "urn:sap2mes:matstorace_ii")]
    public partial class InvertoryServiceClientProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        /// <remarks/>
        public InvertoryServiceClientProxy()
        {
            this.Url = "http://172.16.41.107:50000/XISOAPAdapter/MessageServlet?channel=:BS_MESDEV:CC_SOA" +
                "P_MATSTARCE&version=3.0&Sender.Service=BS_MESDEV&Interface=urn%3Asap2mes%3Amatst" +
                "orace_ii%5EMI_MES_MATSTARCE_REQ";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", OneWay = true, Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        public void MI_MES_MATSTARCE_REQ([System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:sap2mes:matstorace_ii")] DT_MES_MATSTARCE_REQ MT_MES_MATSTARCE_REQ)
        {
            this.Invoke("MI_MES_MATSTARCE_REQ", new object[] {
                        MT_MES_MATSTARCE_REQ});
        }

        /// <remarks/>
        public System.IAsyncResult BeginMI_MES_MATSTARCE_REQ(DT_MES_MATSTARCE_REQ MT_MES_MATSTARCE_REQ, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MI_MES_MATSTARCE_REQ", new object[] {
                        MT_MES_MATSTARCE_REQ}, callback, asyncState);
        }

        /// <remarks/>
        public void EndMI_MES_MATSTARCE_REQ(System.IAsyncResult asyncResult)
        {
            this.EndInvoke(asyncResult);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:sap2mes:matstorace_ii")]
    public partial class DT_MES_MATSTARCE_REQ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("WERKS", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] WERKS;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATNR;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("LGORT", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] LGORT;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TRANS;
    }
}
