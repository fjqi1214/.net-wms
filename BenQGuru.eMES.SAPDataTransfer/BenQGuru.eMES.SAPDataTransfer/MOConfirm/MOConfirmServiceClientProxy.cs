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
    [System.Web.Services.WebServiceBindingAttribute(Name = "MI_MES_POCONFIRM_REQBinding", Namespace = "urn:mes2sap:poconfirm")]
    public partial class MOConfirmServiceClientProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        /// <remarks/>
        public MOConfirmServiceClientProxy()
        {
            this.Url = "http://172.16.41.107:50000/XISOAPAdapter/MessageServlet?channel=:BS_MESDEV:CC_SOA" +
                "P_PoConfirm&version=3.0&Sender.Service=BS_MESDEV&Interface=urn%3Ames2sap%3Apocon" +
                "firm%5EMI_MES_POCONFIRM_REQ";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("MT_MES_POCONFIRM_RESP", Namespace = "urn:mes2sap:poconfirm")]
        public DT_MES_POCONFIRM_RESP MI_MES_POCONFIRM_REQ([System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:mes2sap:poconfirm")] DT_MES_POCONFIRM_REQ MT_MES_POCONFIRM_REQ)
        {
            object[] results = this.Invoke("MI_MES_POCONFIRM_REQ", new object[] {
                        MT_MES_POCONFIRM_REQ});
            return ((DT_MES_POCONFIRM_RESP)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginMI_MES_POCONFIRM_REQ(DT_MES_POCONFIRM_REQ MT_MES_POCONFIRM_REQ, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MI_MES_POCONFIRM_REQ", new object[] {
                        MT_MES_POCONFIRM_REQ}, callback, asyncState);
        }

        /// <remarks/>
        public DT_MES_POCONFIRM_RESP EndMI_MES_POCONFIRM_REQ(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DT_MES_POCONFIRM_RESP)(results[0]));
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:mes2sap:poconfirm")]
    public partial class DT_MES_POCONFIRM_REQ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("POLIST", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_MES_POCONFIRM_REQPOLIST[] POLIST;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Trsaction_code;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:mes2sap:poconfirm")]
    public partial class DT_MES_POCONFIRM_REQPOLIST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOCode;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostSeq;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOProducet;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOScrap;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOconfirm;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOManHour;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOMachineHour;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOCloseDate;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOLocation;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOGrade;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOOP;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:mes2sap:poconfirm")]
    public partial class DT_MES_POCONFIRM
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOCode;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PostSeq;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FLAG;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string message;
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TestWebServiceDynamic", "1.0.0.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:mes2sap:poconfirm")]
    public partial class DT_MES_POCONFIRM_RESP
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Transaction_code;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("PO_lIST", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_MES_POCONFIRM[] PO_lIST;
    }
}
