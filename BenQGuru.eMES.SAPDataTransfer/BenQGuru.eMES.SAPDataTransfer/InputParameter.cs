using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.SAPDataTransfer
{

    [Serializable()]
    public class MES_DN
    {
        public MES_DN()
        {

        }

        private string m_DNCode = string.Empty;
        /// <summary>
        /// LIKP-VBELN
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DNCode
        {
            get { return m_DNCode; }
            set { m_DNCode = value; }
        }

        private string m_ShipTo = string.Empty;
        /// <summary>
        /// LIKP-KUNNR
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ShipTo
        {
            get { return m_ShipTo; }
            set { m_ShipTo = value; }
        }

        private string m_DNLine = string.Empty;
        /// <summary>
        /// LIPS-POSNR
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DNLine
        {
            get { return m_DNLine; }
            set { m_DNLine = value; }
        }

        private string m_ItemCode = string.Empty;
        /// <summary>
        /// LIPS-MATNR
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemCode
        {
            get { return m_ItemCode; }
            set { m_ItemCode = value; }
        }

        private string m_ItemDescription = string.Empty;
        /// <summary>
        /// LIPS-ARKTX
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemDescription
        {
            get { return m_ItemDescription; }
            set { m_ItemDescription = value; }
        }

        private int m_OrgID = 0;
        /// <summary>
        /// LIPS-WERKS
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int OrgID
        {
            get { return m_OrgID; }
            set { m_OrgID = value; }
        }

        private string m_FromStorage = string.Empty;
        /// <summary>
        /// LIPS-LGORT
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FromStorage
        {
            get { return m_FromStorage; }
            set { m_FromStorage = value; }
        }

        private decimal m_DNQuantity = 0;
        /// <summary>
        /// LIPSD-LFIMG
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal DNQuantity
        {
            get { return m_DNQuantity; }
            set { m_DNQuantity = value; }
        }

        private string m_Unit = string.Empty;
        /// <summary>
        /// LIPS-VRKME
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Unit
        {
            get { return m_Unit; }
            set { m_Unit = value; }
        }

        private string m_MovementType = string.Empty;
        /// <summary>
        /// LIPS-BWART
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MovementType
        {
            get { return m_MovementType; }
            set { m_MovementType = value; }
        }

        private string m_ItemGrade = string.Empty;
        /// <summary>
        /// LIPS-CHARG
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemGrade
        {
            get { return m_ItemGrade; }
            set { m_ItemGrade = value; }
        }

        private string m_MOCode = string.Empty;
        /// <summary>
        /// LIPS-KDMAT
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MOCode
        {
            get { return m_MOCode; }
            set { m_MOCode = value; }
        }

        private string m_OrderNo = string.Empty;
        /// <summary>
        /// VBKD-BSTKD
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string OrderNo
        {
            get { return m_OrderNo; }
            set { m_OrderNo = value; }
        }

        private string m_CustomerOrderNo = string.Empty;
        /// <summary>
        /// VBAK-VBELN
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CustomerOrderNo
        {
            get { return m_CustomerOrderNo; }
            set { m_CustomerOrderNo = value; }
        }

        private string m_CustomerOrderNoType = string.Empty;
        /// <summary>
        /// EKKO-EBELN
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CustomerOrderNoType
        {
            get { return m_CustomerOrderNoType; }
            set { m_CustomerOrderNoType = value; }
        }

        private string m_ToStorage = string.Empty;
        /// <summary>
        /// T001L-LGOBE
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ToStorage
        {
            get { return m_ToStorage; }
            set { m_ToStorage = value; }
        }

        private string m_Status = string.Empty;
        /// <summary>
        /// WBSTA
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

    }

    [Serializable()]
    public class DNRecive_REQ
    {
        public DNRecive_REQ()
        {

        }

        private List<MES_DN> m_DNList = new List<MES_DN>();
        [System.Xml.Serialization.XmlElementAttribute("DNList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<MES_DN> DNList
        {
            get { return m_DNList; }
            set { m_DNList = value; }
        }

        private string m_TransactionCode = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }

        private string m_Flag = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Flag
        {
            get { return m_Flag; }
            set { m_Flag = value; }
        }

        private string m_Message = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
    }

    [Serializable()]
    public class MES_INV
    {
        public MES_INV()
        {

        }

        private string m_ItemCode = string.Empty;
        /// <summary>
        /// MATNR
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemCode
        {
            get { return m_ItemCode; }
            set { m_ItemCode = value; }
        }

        private string m_Division = string.Empty;
        /// <summary>
        /// SPART
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Division
        {
            get { return m_Division; }
            set { m_Division = value; }
        }

        private int m_OrgID = 0;
        /// <summary>
        /// WERKS
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int OrgID
        {
            get { return m_OrgID; }
            set { m_OrgID = value; }
        }

        private string m_StorageID = string.Empty;
        /// <summary>
        /// LGORT
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string StorageID
        {
            get { return m_StorageID; }
            set { m_StorageID = value; }
        }

        private string m_StorageName = string.Empty;
        /// <summary>
        /// LGOBE
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string StorageName
        {
            get { return m_StorageName; }
            set { m_StorageName = value; }
        }

        private string m_ItemGrade = string.Empty;
        /// <summary>
        /// CHARG
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemGrade
        {
            get { return m_ItemGrade; }
            set { m_ItemGrade = value; }
        }

        private decimal m_CLABSQTY = 0;
        /// <summary>
        /// CLABS
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CLABSQTY
        {
            get { return m_CLABSQTY; }
            set { m_CLABSQTY = value; }
        }

        private decimal m_CINSMQTY = 0;
        /// <summary>
        /// CINSM
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CINSMQTY
        {
            get { return m_CINSMQTY; }
            set { m_CINSMQTY = value; }
        }

        private decimal m_CSPEMQTY = 0;
        /// <summary>
        /// CSPEM
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CSPEMQTY
        {
            get { return m_CSPEMQTY; }
            set { m_CSPEMQTY = value; }
        }

        private decimal m_CUMLQTY = 0;
        /// <summary>
        /// CUML
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal CUMLQTY
        {
            get { return m_CUMLQTY; }
            set { m_CUMLQTY = value; }
        }

        private string m_ItemDescription = string.Empty;
        /// <summary>
        /// MAKTX
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ItemDescription
        {
            get { return m_ItemDescription; }
            set { m_ItemDescription = value; }
        }

        private string m_ModelCode = string.Empty;
        /// <summary>
        /// TYPES
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ModelCode
        {
            get { return m_ModelCode; }
            set { m_ModelCode = value; }
        }

    }

    [Serializable()]
    public class INVReceive_REQ
    {
        public INVReceive_REQ()
        {

        }

        private List<MES_INV> m_INVList = new List<MES_INV>();
        [System.Xml.Serialization.XmlElementAttribute("INVList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<MES_INV> INVList
        {
            get { return m_INVList; }
            set { m_INVList = value; }
        }

        private string m_TransactionCode = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }

        private string m_Flag = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Flag
        {
            get { return m_Flag; }
            set { m_Flag = value; }
        }

        private string m_Message = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
    }

    [Serializable()]
    public class MES_MATPO
    {
        public MES_MATPO()
        {

        }

        private string m_PONo = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PONo
        {
            get { return m_PONo; }
            set { m_PONo = value; }
        }

        private string m_Flag = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Flag
        {
            get { return m_Flag; }
            set { m_Flag = value; }
        }

        private string m_Message = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }

        private string m_MaterialDocumentYear = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MaterialDocumentYear
        {
            get { return m_MaterialDocumentYear; }
            set { m_MaterialDocumentYear = value; }
        }

        private string m_MaterialDocument = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MaterialDocument
        {
            get { return m_MaterialDocument; }
            set { m_MaterialDocument = value; }
        }
    }

    [Serializable()]
    public class MATPO_REQ
    {
        public MATPO_REQ()
        {

        }

        private List<MES_MATPO> m_POList = new List<MES_MATPO>();
        [System.Xml.Serialization.XmlElementAttribute("POList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<MES_MATPO> POList
        {
            get { return m_POList; }
            set { m_POList = value; }
        }

        private string m_TransactionCode = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }
    }

    [Serializable()]
    public class MES_DNConfirm
    {
        public MES_DNConfirm()
        {

        }

        private string m_DNNo = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DNNo
        {
            get { return m_DNNo; }
            set { m_DNNo = value; }
        }

        private string m_Flag = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Flag
        {
            get { return m_Flag; }
            set { m_Flag = value; }
        }

        private string m_Message = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Message
        {
            get { return m_Message; }
            set { m_Message = value; }
        }
    }

    [Serializable()]
    public class DNCONFIRM_REQ
    {
        public DNCONFIRM_REQ()
        {

        }

        private string m_TransactionCode = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }

        private List<MES_DNConfirm> m_DNConfirmList = new List<MES_DNConfirm>();
        [System.Xml.Serialization.XmlElementAttribute("DNConfirmList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<MES_DNConfirm> DNConfirmList
        {
            get { return m_DNConfirmList; }
            set { m_DNConfirmList = value; }
        }

    }

    [Serializable()]
    public class MES_MATISSUE
    {
        public MES_MATISSUE()
        {

        }

        private string m_VendorCode = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VendorCode
        {
            get { return m_VendorCode; }
            set { m_VendorCode = value; }
        }

        private string m_Flag = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Flag
        {
            get { return m_Flag; }
            set { m_Flag = value; }
        }

        private string m_ErrorMessage = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ErrorMessage
        {
            get { return m_ErrorMessage; }
            set { m_ErrorMessage = value; }
        }

        private string m_MaterialDocumentYear = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MaterialDocumentYear
        {
            get { return m_MaterialDocumentYear; }
            set { m_MaterialDocumentYear = value; }
        }

        private string m_MaterialDocument = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MaterialDocument
        {
            get { return m_MaterialDocument; }
            set { m_MaterialDocument = value; }
        }

        

    }

    [Serializable()]
    public class MATISSUE_REQ
    {
        public MATISSUE_REQ()
        {

        }

        private List<MES_MATISSUE> m_IssueList = new List<MES_MATISSUE>();
        [System.Xml.Serialization.XmlElementAttribute("IssueList", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public List<MES_MATISSUE> IssueList
        {
            get { return m_IssueList; }
            set { m_IssueList = value; }
        }


        private string m_TransactionCode = string.Empty;
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TransactionCode
        {
            get { return m_TransactionCode; }
            set { m_TransactionCode = value; }
        }
    }
}
