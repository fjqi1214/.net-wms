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
    [System.Web.Services.WebServiceBindingAttribute(Name = "MI_MES_PO_REQBinding", Namespace = "urn:sap2mes:productionorder")]
    public partial class MOServiceClientProxy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        /// <remarks/>
        public MOServiceClientProxy()
        {
            this.Url = "http://172.16.41.107:50000/XISOAPAdapter/MessageServlet?channel=:BS_MESDEV:CC_SOA" +
                "P_Productionorder&version=3.0&Sender.Service=BS_MESDEV&Interface=urn%3Asap2mes%3" +
                "Aproductionorder%5EMI_MES_PO_REQ";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("MT_MES_PO_RESP", Namespace = "urn:sap2mes:productionorder")]
        public DT_MES_PO_RESP MI_MES_PO_REQ([System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:sap2mes:productionorder")] DT_MES_PO_REQ MT_MES_PO_REQ)
        {
            object[] results = this.Invoke("MI_MES_PO_REQ", new object[] {
                        MT_MES_PO_REQ});
            return ((DT_MES_PO_RESP)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginMI_MES_PO_REQ(DT_MES_PO_REQ MT_MES_PO_REQ, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("MI_MES_PO_REQ", new object[] {
                        MT_MES_PO_REQ}, callback, asyncState);
        }

        /// <remarks/>
        public DT_MES_PO_RESP EndMI_MES_PO_REQ(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((DT_MES_PO_RESP)(results[0]));
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:sap2mes:productionorder")]
    public partial class DT_MES_PO_REQ
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Trsaction_Code;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MaintainDate_B;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MaintainDate_E;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OrgID;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Mocode;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:sap2mes:productionorder")]
    public partial class DT_MES_PO_RESP
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Trsaction_Code;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FLAG;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("POLIST", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public DT_MES_PO_RESPPOLIST[] POLIST;
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:sap2mes:productionorder")]
    public partial class DT_MES_PO_RESPPOLIST
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOCODE;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemCode;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MoType;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOPlanQty;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOPlanstart;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOPlanEndDate;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOBOM;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ORGID;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOOP;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string flag;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string StorNo;
    }
}
