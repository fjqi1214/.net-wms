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
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "MI_MES_POBOM_REQBinding", Namespace = "urn:sap2mes:pobom")]
    public partial class MOBOMServiceClientProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        /// <remarks/>
        public MOBOMServiceClientProxy()
        {
            this.Url = "http://172.16.41.107:50000/XISOAPAdapter/MessageServlet?channel=:BS_MESDEV:CC_SOA" +
                "P_POBOM&version=3.0&Sender.Service=BS_MESDEV&Interface=urn%3Asap2mes%3Apobom%5E" +
                "MI_MES_POBOM_REQ";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("MT_MES_POBOM_RESP", Namespace = "urn:sap2mes:pobom")]
        public DT_MES_POBOM_RESP MI_MES_POBOM_REQ([System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:sap2mes:pobom")] DT_MES_POBOM_REQ MT_MES_POBOM_REQ)
        {
            object[] results = this.Invoke("MI_MES_POBOM_REQ", new object[] {
                        MT_MES_POBOM_REQ});
            return ((DT_MES_POBOM_RESP)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginMI_MES_POBOM_REQ(DT_MES_POBOM_REQ MT_MES_POBOM_REQ, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MI_MES_POBOM_REQ", new object[] {
                        MT_MES_POBOM_REQ}, callback, asyncState);
        }

        /// <remarks/>
        public DT_MES_POBOM_RESP EndMI_MES_POBOM_REQ(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DT_MES_POBOM_RESP)(results[0]));
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:sap2mes:pobom")]
    public partial class DT_MES_POBOM_REQ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Mocode", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string[] Mocode;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Transaction_code;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:sap2mes:pobom")]
    public partial class DT_MES_POBOM_RESP
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("POCONFIRM_LIST", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_MES_POBOM_RESPPOCONFIRM_LIST[] POCONFIRM_LIST;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Transaction_code;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FLAG;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string message;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:sap2mes:pobom")]
    public partial class DT_MES_POBOM_RESPPOCONFIRM_LIST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOCODE;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ITEMCODE;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBOM;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBOMLINE;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBITEMCODE;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBITEMDESC;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBITEMQTY;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBOMITEMUOM;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOFAC;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MORESOURCE;
    }
}
