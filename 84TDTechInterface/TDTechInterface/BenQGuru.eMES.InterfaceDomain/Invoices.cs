using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.InterfaceDomain
{
    #region I_Sappo
    /// <summary>
    /// I_SAPPO
    /// </summary>
    [Serializable, TableMap("I_SAPPO", "ID")]
    public class I_Sappo : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sappo()
        {
        }

        ///<summary>
        ///Receiveruser
        ///</summary>
        [FieldMapAttribute("RECEIVERUSER", typeof(string), 200, true)]
        public string Receiveruser;

        ///<summary>
        ///Receiveraddr
        ///</summary>
        [FieldMapAttribute("RECEIVERADDR", typeof(string), 960, true)]
        public string Receiveraddr;

        ///<summary>
        ///Eattribute3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///Eattribute2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///Eattribute1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///Ptime
        ///</summary>
        [FieldMapAttribute("PTIME", typeof(int), 22, false)]
        public int Ptime;

        ///<summary>
        ///Pdate
        ///</summary>
        [FieldMapAttribute("PDATE", typeof(int), 22, false)]
        public int Pdate;

        ///<summary>
        ///Stime
        ///</summary>
        [FieldMapAttribute("STIME", typeof(int), 22, false)]
        public int Stime;

        ///<summary>
        ///Sdate
        ///</summary>
        [FieldMapAttribute("SDATE", typeof(int), 22, false)]
        public int Sdate;

        ///<summary>
        ///Mesflag
        ///</summary>
        [FieldMapAttribute("MESFLAG", typeof(string), 1, true)]
        public string Mesflag;

        ///<summary>
        ///Ccno
        ///</summary>
        [FieldMapAttribute("CCNO", typeof(string), 40, true)]
        public string Ccno;

        ///<summary>
        ///Sowbsno
        ///</summary>
        [FieldMapAttribute("SOWBSNO", typeof(string), 32, true)]
        public string Sowbsno;

        ///<summary>
        ///Soitemno
        ///</summary>
        [FieldMapAttribute("SOITEMNO", typeof(string), 24, true)]
        public string Soitemno;

        ///<summary>
        ///So
        ///</summary>
        [FieldMapAttribute("SO", typeof(string), 40, true)]
        public string So;

        ///<summary>
        ///Itemcategory
        ///</summary>
        [FieldMapAttribute("ITEMCATEGORY", typeof(string), 1, true)]
        public string Itemcategory;

        ///<summary>
        ///Accountassignment
        ///</summary>
        [FieldMapAttribute("ACCOUNTASSIGNMENT", typeof(string), 1, true)]
        public string Accountassignment;

        ///<summary>
        ///Returnflag
        ///</summary>
        [FieldMapAttribute("RETURNFLAG", typeof(string), 1, true)]
        public string Returnflag;

        ///<summary>
        ///Prno
        ///</summary>
        [FieldMapAttribute("PRNO", typeof(string), 40, true)]
        public string Prno;

        ///<summary>
        ///Vendormcode
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 140, true)]
        public string Vendormcode;

        ///<summary>
        ///Detailremark
        ///</summary>
        [FieldMapAttribute("DETAILREMARK", typeof(string), 880, true)]
        public string Detailremark;

        ///<summary>
        ///Storagecode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string Storagecode;

        ///<summary>
        ///Shipaddr
        ///</summary>
        [FieldMapAttribute("SHIPADDR", typeof(string), 960, true)]
        public string Shipaddr;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///Plandate
        ///</summary>
        [FieldMapAttribute("PLANDATE", typeof(int), 22, true)]
        public int Plandate;

        ///<summary>
        ///Planqty
        ///</summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 22, false)]
        public decimal Planqty;

        ///<summary>
        ///Mlongdesc
        ///</summary>
        [FieldMapAttribute("MLONGDESC", typeof(string), 880, true)]
        public string Mlongdesc;

        ///<summary>
        ///Menshortdesc
        ///</summary>
        [FieldMapAttribute("MENSHORTDESC", typeof(string), 160, true)]
        public string Menshortdesc;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///Faccode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
        public string Faccode;

        ///<summary>
        ///Invlinestatus
        ///</summary>
        [FieldMapAttribute("INVLINESTATUS", typeof(string), 40, true)]
        public string Invlinestatus;

        ///<summary>
        ///Invline
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(int), 22, false)]
        public int Invline;

        ///<summary>
        ///Purchasegroup
        ///</summary>
        [FieldMapAttribute("PURCHASEGROUP", typeof(string), 72, true)]
        public string Purchasegroup;

        ///<summary>
        ///Purchugcode
        ///</summary>
        [FieldMapAttribute("PURCHUGCODE", typeof(string), 12, true)]
        public string Purchugcode;

        ///<summary>
        ///Purchorgcode
        ///</summary>
        [FieldMapAttribute("PURCHORGCODE", typeof(string), 16, true)]
        public string Purchorgcode;

        ///<summary>
        ///Remark5
        ///</summary>
        [FieldMapAttribute("REMARK5", typeof(string), 48, true)]
        public string Remark5;

        ///<summary>
        ///Remark4
        ///</summary>
        [FieldMapAttribute("REMARK4", typeof(string), 48, true)]
        public string Remark4;

        ///<summary>
        ///Remark3
        ///</summary>
        [FieldMapAttribute("REMARK3", typeof(string), 200, true)]
        public string Remark3;

        ///<summary>
        ///Remark2
        ///</summary>
        [FieldMapAttribute("REMARK2", typeof(string), 200, true)]
        public string Remark2;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 880, true)]
        public string Remark1;

        ///<summary>
        ///Buyerphone
        ///</summary>
        [FieldMapAttribute("BUYERPHONE", typeof(string), 120, true)]
        public string Buyerphone;

        ///<summary>
        ///Poupdatetime
        ///</summary>
        [FieldMapAttribute("POUPDATETIME", typeof(int), 22, true)]
        public int Poupdatetime;

        ///<summary>
        ///Poupdatedate
        ///</summary>
        [FieldMapAttribute("POUPDATEDATE", typeof(int), 22, true)]
        public int Poupdatedate;

        ///<summary>
        ///Createuser
        ///</summary>
        [FieldMapAttribute("CREATEUSER", typeof(string), 48, true)]
        public string Createuser;

        ///<summary>
        ///Pocreatedate
        ///</summary>
        [FieldMapAttribute("POCREATEDATE", typeof(int), 22, true)]
        public int Pocreatedate;

        ///<summary>
        ///Ordertype
        ///</summary>
        [FieldMapAttribute("ORDERTYPE", typeof(string), 16, true)]
        public string Ordertype;

        ///<summary>
        ///Orderstatus
        ///</summary>
        [FieldMapAttribute("ORDERSTATUS", typeof(string), 40, true)]
        public string Orderstatus;

        ///<summary>
        ///Vendorcode
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 160, true)]
        public string Vendorcode;

        ///<summary>
        ///Companycode
        ///</summary>
        [FieldMapAttribute("COMPANYCODE", typeof(string), 16, true)]
        public string Companycode;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

        ///<summary>
        ///Id
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion

    #region I_Sapdnbatch
    /// <summary>
    /// I_Sapdnbatch
    /// </summary>
    [Serializable, TableMap("I_SAPDNBATCH", "ID")]
    public class I_Sapdnbatch : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapdnbatch()
        {
        }

        //add by sam
        [FieldMapAttribute("DETAILREMARK", typeof(string), 880, true)]
        public string Detailremark;

        [FieldMapAttribute("NOTOUTCHECKFLAG", typeof(string), 1, true)]
        public string NotOutCheckFlag;

        ///<summary>
        ///Packingwayno
        ///</summary>
        [FieldMapAttribute("PACKINGWAYNO", typeof(string), 880, true)]
        public string Packingwayno;

        ///<summary>
        ///Dqsmcode
        ///</summary>
        [FieldMapAttribute("DQSMCODE", typeof(string), 88, true)]
        public string Dqsmcode;

        ///<summary>
        ///Mesflag
        ///</summary>
        [FieldMapAttribute("MESFLAG", typeof(string), 1, true)]
        public string Mesflag;

        ///<summary>
        ///Sdate
        ///</summary>
        [FieldMapAttribute("SDATE", typeof(int), 22, false)]
        public int Sdate;

        ///<summary>
        ///Stime
        ///</summary>
        [FieldMapAttribute("STIME", typeof(int), 22, false)]
        public int Stime;

        ///<summary>
        ///Pdate
        ///</summary>
        [FieldMapAttribute("PDATE", typeof(int), 22, false)]
        public int Pdate;

        ///<summary>
        ///Ptime
        ///</summary>
        [FieldMapAttribute("PTIME", typeof(int), 22, false)]
        public int Ptime;

        ///<summary>
        ///Eattribute1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///Eattribute2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///Eattribute3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///Id
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

        ///<summary>
        ///Dnbatchno
        ///</summary>
        [FieldMapAttribute("DNBATCHNO", typeof(string), 20, true)]
        public string Dnbatchno;

        ///<summary>
        ///Dnbatchstatus
        ///</summary>
        [FieldMapAttribute("DNBATCHSTATUS", typeof(string), 10, true)]
        public string Dnbatchstatus;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 10, true)]
        public string Invno;

        ///<summary>
        ///Invtype
        ///</summary>
        [FieldMapAttribute("INVTYPE", typeof(string), 10, true)]
        public string Invtype;

        ///<summary>
        ///Shiptoparty
        ///</summary>
        [FieldMapAttribute("SHIPTOPARTY", typeof(string), 40, true)]
        public string Shiptoparty;

        ///<summary>
        ///Orderno
        ///</summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 140, true)]
        public string Orderno;

        ///<summary>
        ///Cusorderno
        ///</summary>
        [FieldMapAttribute("CUSORDERNO", typeof(string), 40, true)]
        public string Cusorderno;

        ///<summary>
        ///Cusordernotype
        ///</summary>
        [FieldMapAttribute("CUSORDERNOTYPE", typeof(string), 16, true)]
        public string Cusordernotype;

        ///<summary>
        ///Dnmuser
        ///</summary>
        [FieldMapAttribute("DNMUSER", typeof(string), 48, true)]
        public string Dnmuser;

        ///<summary>
        ///Dnmdate
        ///</summary>
        [FieldMapAttribute("DNMDATE", typeof(int), 22, true)]
        public int Dnmdate;

        ///<summary>
        ///Dnmtime
        ///</summary>
        [FieldMapAttribute("DNMTIME", typeof(int), 22, true)]
        public int Dnmtime;

        ///<summary>
        ///Cusbatchno
        ///</summary>
        [FieldMapAttribute("CUSBATCHNO", typeof(string), 240, true)]
        public string Cusbatchno;

        ///<summary>
        ///Shippinglocation
        ///</summary>
        [FieldMapAttribute("SHIPPINGLOCATION", typeof(string), 528, true)]
        public string Shippinglocation;

        ///<summary>
        ///Plangidate
        ///</summary>
        [FieldMapAttribute("PLANGIDATE", typeof(int), 22, true)]
        public int Plangidate;

        ///<summary>
        ///Gfcontractno
        ///</summary>
        [FieldMapAttribute("GFCONTRACTNO", typeof(string), 48, true)]
        public string Gfcontractno;

        ///<summary>
        ///Orderreason
        ///</summary>
        [FieldMapAttribute("ORDERREASON", typeof(string), 12, true)]
        public string Orderreason;

        ///<summary>
        ///Postway
        ///</summary>
        [FieldMapAttribute("POSTWAY", typeof(string), 56, true)]
        public string Postway;

        ///<summary>
        ///Pickcondition
        ///</summary>
        [FieldMapAttribute("PICKCONDITION", typeof(string), 32, true)]
        public string Pickcondition;

        ///<summary>
        ///Gfflag
        ///</summary>
        [FieldMapAttribute("GFFLAG", typeof(string), 1, true)]
        public string Gfflag;

        ///<summary>
        ///Invline
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(int), 22, true)]
        public int Invline;

        ///<summary>
        ///Hignlevelitem
        ///</summary>
        [FieldMapAttribute("HIGNLEVELITEM", typeof(int), 22, true)]
        public int Hignlevelitem;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, true)]
        public string Mcode;

        ///<summary>
        ///Faccode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
        public string Faccode;

        ///<summary>
        ///Storagecode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string Storagecode;

        ///<summary>
        ///Planqty
        ///</summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 22, true)]
        public decimal Planqty;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///Movementtype
        ///</summary>
        [FieldMapAttribute("MOVEMENTTYPE", typeof(string), 12, true)]
        public string Movementtype;

        ///<summary>
        ///Cusmcode
        ///</summary>
        [FieldMapAttribute("CUSMCODE", typeof(string), 100, true)]
        public string Cusmcode;

        ///<summary>
        ///Cusitemspec
        ///</summary>
        [FieldMapAttribute("CUSITEMSPEC", typeof(string), 200, true)]
        public string Cusitemspec;

        ///<summary>
        ///Cusitemdesc
        ///</summary>
        [FieldMapAttribute("CUSITEMDESC", typeof(string), 960, true)]
        public string Cusitemdesc;

        ///<summary>
        ///Vendermcode
        ///</summary>
        [FieldMapAttribute("VENDERMCODE", typeof(string), 140, true)]
        public string Vendermcode;

        ///<summary>
        ///Gfhwmcode
        ///</summary>
        [FieldMapAttribute("GFHWMCODE", typeof(string), 100, true)]
        public string Gfhwmcode;

        ///<summary>
        ///Gfpackingseq
        ///</summary>
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 24, true)]
        public string Gfpackingseq;

        ///<summary>
        ///Gfhwdesc
        ///</summary>
        [FieldMapAttribute("GFHWDESC", typeof(string), 960, true)]
        public string Gfhwdesc;

        ///<summary>
        ///Hwcodeqty
        ///</summary>
        [FieldMapAttribute("HWCODEQTY", typeof(decimal), 22, true)]
        public decimal Hwcodeqty;

        ///<summary>
        ///Hwcodeunit
        ///</summary>
        [FieldMapAttribute("HWCODEUNIT", typeof(string), 12, true)]
        public string Hwcodeunit;

        ///<summary>
        ///Hwtypeinfo
        ///</summary>
        [FieldMapAttribute("HWTYPEINFO", typeof(string), 200, true)]
        public string Hwtypeinfo;

        ///<summary>
        ///Packingway
        ///</summary>
        [FieldMapAttribute("PACKINGWAY", typeof(string), 800, true)]
        public string Packingway;

        ///<summary>
        ///Packingno
        ///</summary>
        [FieldMapAttribute("PACKINGNO", typeof(string), 80, true)]
        public string Packingno;

        ///<summary>
        ///Packingspec
        ///</summary>
        [FieldMapAttribute("PACKINGSPEC", typeof(string), 200, true)]
        public string Packingspec;

    }
    #endregion

    #region I_Sapub
    /// <summary>
    /// I_SAPUB
    /// </summary>
    [Serializable, TableMap("I_SAPUB", "ID")]
    public class I_Sapub : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapub()
        {
        }

        ///<summary>
        ///Eattribute3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///Eattribute2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///Eattribute1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///Ptime
        ///</summary>
        [FieldMapAttribute("PTIME", typeof(int), 22, false)]
        public int Ptime;

        ///<summary>
        ///Pdate
        ///</summary>
        [FieldMapAttribute("PDATE", typeof(int), 22, false)]
        public int Pdate;

        ///<summary>
        ///Stime
        ///</summary>
        [FieldMapAttribute("STIME", typeof(int), 22, false)]
        public int Stime;

        ///<summary>
        ///Sdate
        ///</summary>
        [FieldMapAttribute("SDATE", typeof(int), 22, false)]
        public int Sdate;

        ///<summary>
        ///Mesflag
        ///</summary>
        [FieldMapAttribute("MESFLAG", typeof(string), 1, true)]
        public string Mesflag;

        ///<summary>
        ///Demandarrivaldate
        ///</summary>
        [FieldMapAttribute("DEMANDARRIVALDATE", typeof(int), 22, true)]
        public int Demandarrivaldate;

        ///<summary>
        ///Receivemcode
        ///</summary>
        [FieldMapAttribute("RECEIVEMCODE", typeof(string), 140, true)]
        public string Receivemcode;

        ///<summary>
        ///Custmcode
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 140, true)]
        public string Custmcode;

        ///<summary>
        ///Receiveruser
        ///</summary>
        [FieldMapAttribute("RECEIVERUSER", typeof(string), 200, true)]
        public string Receiveruser;

        ///<summary>
        ///Receiveraddr
        ///</summary>
        [FieldMapAttribute("RECEIVERADDR", typeof(string), 960, true)]
        public string Receiveraddr;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Storagecode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 16, true)]
        public string Storagecode;

        ///<summary>
        ///Fromstoragecode
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 48, true)]
        public string Fromstoragecode;

        ///<summary>
        ///Mdesc
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 160, true)]
        public string Mdesc;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///Faccode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
        public string Faccode;

        ///<summary>
        ///Invlinestatus
        ///</summary>
        [FieldMapAttribute("INVLINESTATUS", typeof(string), 10, true)]
        public string Invlinestatus;

        ///<summary>
        ///Invline
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(int), 22, false)]
        public int Invline;

        ///<summary>
        ///Fromfaccode
        ///</summary>
        [FieldMapAttribute("FROMFACCODE", typeof(string), 4, true)]
        public string Fromfaccode;

        ///<summary>
        ///Reworkapplyuser
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 48, true)]
        public string Reworkapplyuser;

        ///<summary>
        ///Pocreatedate
        ///</summary>
        [FieldMapAttribute("POCREATEDATE", typeof(int), 22, true)]
        public int Pocreatedate;

        ///<summary>
        ///Createuser
        ///</summary>
        [FieldMapAttribute("CREATEUSER", typeof(string), 48, true)]
        public string Createuser;

        ///<summary>
        ///Remark
        ///</summary>
        [FieldMapAttribute("REMARK", typeof(string), 880, true)]
        public string Remark;

        ///<summary>
        ///Logistics
        ///</summary>
        [FieldMapAttribute("LOGISTICS", typeof(string), 880, true)]
        public string Logistics;

        ///<summary>
        ///Oano
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 48, true)]
        public string Oano;

        ///<summary>
        ///Poupdatetime
        ///</summary>
        [FieldMapAttribute("POUPDATETIME", typeof(int), 22, true)]
        public int Poupdatetime;

        ///<summary>
        ///Poupdatedate
        ///</summary>
        [FieldMapAttribute("POUPDATEDATE", typeof(int), 22, true)]
        public int Poupdatedate;

        ///<summary>
        ///Ubtype
        ///</summary>
        [FieldMapAttribute("UBTYPE", typeof(string), 16, true)]
        public string Ubtype;

        ///<summary>
        ///Purchorgcode
        ///</summary>
        [FieldMapAttribute("PURCHORGCODE", typeof(string), 16, true)]
        public string Purchorgcode;

        ///<summary>
        ///Companycode
        ///</summary>
        [FieldMapAttribute("COMPANYCODE", typeof(string), 16, true)]
        public string Companycode;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

        ///<summary>
        ///Id
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion


    #region I_Saprs
    /// <summary>
    /// I_SAPUB
    /// </summary>
    [Serializable, TableMap("I_SAPRS", "ID")]
    public class I_Saprs : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Saprs()
        {
        }

        ///<summary>
        ///Eattribute3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///Eattribute2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///Eattribute1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///Ptime
        ///</summary>
        [FieldMapAttribute("PTIME", typeof(int), 22, false)]
        public int Ptime;

        ///<summary>
        ///Pdate
        ///</summary>
        [FieldMapAttribute("PDATE", typeof(int), 22, false)]
        public int Pdate;

        ///<summary>
        ///Stime
        ///</summary>
        [FieldMapAttribute("STIME", typeof(int), 22, false)]
        public int Stime;

        ///<summary>
        ///Sdate
        ///</summary>
        [FieldMapAttribute("SDATE", typeof(int), 22, false)]
        public int Sdate;

        ///<summary>
        ///Mesflag
        ///</summary>
        [FieldMapAttribute("MESFLAG", typeof(string), 1, true)]
        public string Mesflag;

        ///<summary>
        ///Demandarrivaldate
        ///</summary>
        [FieldMapAttribute("DEMANDARRIVALDATE", typeof(int), 22, true)]
        public int Demandarrivaldate;

        ///<summary>
        ///Receivemcode
        ///</summary>
        [FieldMapAttribute("RECEIVEMCODE", typeof(string), 140, true)]
        public string Receivemcode;

        ///<summary>
        ///Custmcode
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 140, true)]
        public string Custmcode;

        ///<summary>
        ///Receiveruser
        ///</summary>
        [FieldMapAttribute("RECEIVERUSER", typeof(string), 200, true)]
        public string Receiveruser;

        ///<summary>
        ///Receiveraddr
        ///</summary>
        [FieldMapAttribute("RECEIVERADDR", typeof(string), 960, true)]
        public string Receiveraddr;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Storagecode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 16, true)]
        public string Storagecode;

        ///<summary>
        ///Fromstoragecode
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 48, true)]
        public string Fromstoragecode;

        ///<summary>
        ///Mdesc
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 160, true)]
        public string Mdesc;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///Faccode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
        public string Faccode;

        ///<summary>
        ///Invlinestatus
        ///</summary>
        [FieldMapAttribute("INVLINESTATUS", typeof(string), 10, true)]
        public string Invlinestatus;

        ///<summary>
        ///Invline
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(int), 22, false)]
        public int Invline;

        ///<summary>
        ///Fromfaccode
        ///</summary>
        [FieldMapAttribute("FROMFACCODE", typeof(string), 4, true)]
        public string Fromfaccode;

        ///<summary>
        ///Reworkapplyuser
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 48, true)]
        public string Reworkapplyuser;

        ///<summary>
        ///Pocreatedate
        ///</summary>
        [FieldMapAttribute("POCREATEDATE", typeof(int), 22, true)]
        public int Pocreatedate;

        ///<summary>
        ///Createuser
        ///</summary>
        [FieldMapAttribute("CREATEUSER", typeof(string), 48, true)]
        public string Createuser;

        [FieldMapAttribute("SAPCUSER", typeof(string), 40, false)]
        public string SAPCuser;

        ///<summary>
        ///Remark
        ///</summary>
        [FieldMapAttribute("REMARK", typeof(string), 880, true)]
        public string Remark;

        ///<summary>
        ///Logistics
        ///</summary>
        [FieldMapAttribute("LOGISTICS", typeof(string), 880, true)]
        public string Logistics;

        ///<summary>
        ///Oano
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 48, true)]
        public string Oano;

        ///<summary>
        ///Poupdatetime
        ///</summary>
        [FieldMapAttribute("POUPDATETIME", typeof(int), 22, true)]
        public int Poupdatetime;

        ///<summary>
        ///Poupdatedate
        ///</summary>
        [FieldMapAttribute("POUPDATEDATE", typeof(int), 22, true)]
        public int Poupdatedate;

        ///<summary>
        ///Ubtype
        ///</summary>
        [FieldMapAttribute("TYPE", typeof(string), 16, true)]
        public string Type;

        ///<summary>
        ///Purchorgcode
        ///</summary>
        [FieldMapAttribute("PURCHORGCODE", typeof(string), 16, true)]
        public string Purchorgcode;

        ///<summary>
        ///Companycode
        ///</summary>
        [FieldMapAttribute("COMPANYCODE", typeof(string), 16, true)]
        public string Companycode;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

        ///<summary>
        ///Id
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;


        [FieldMapAttribute("APPLYDATE", typeof(int), 22, true)]
        public int Applydate;

        [FieldMapAttribute("PLANQTY", typeof(decimal), 22, false)]
        public decimal Planqty;

        [FieldMapAttribute("NEEDDATE", typeof(int), 22, true)]
        public int Needdate;

        [FieldMapAttribute("PRNO", typeof(string), 40, true)]
        public string Prno;
        ///<summary>
        ///Companycode
        ///</summary>
        [FieldMapAttribute("CC", typeof(string), 40, true)]
        public string Cc;

    }
    #endregion

    #region I_Sapstoragecheck--SAP库存盘点中间表
    /// <summary>
    /// I_SAPSTORAGECHECK--SAP库存盘点中间表
    /// </summary>
    [Serializable, TableMap("I_SAPSTORAGECHECK", "ID")]
    public class I_Sapstoragecheck : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public I_Sapstoragecheck()
        {
        }

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///处理时间
        ///</summary>
        [FieldMapAttribute("PTIME", typeof(int), 22, false)]
        public int Ptime;

        ///<summary>
        ///处理日期
        ///</summary>
        [FieldMapAttribute("PDATE", typeof(int), 22, false)]
        public int Pdate;

        ///<summary>
        ///同步时间
        ///</summary>
        [FieldMapAttribute("STIME", typeof(int), 22, false)]
        public int Stime;

        ///<summary>
        ///同步日期
        ///</summary>
        [FieldMapAttribute("SDATE", typeof(int), 22, false)]
        public int Sdate;

        ///<summary>
        ///同步时间
        ///</summary>
        [FieldMapAttribute("SAPCTIME", typeof(int), 22, false)]
        public int SAPCTime;

        ///<summary>
        ///同步日期
        ///</summary>
        [FieldMapAttribute("SAPCDATE", typeof(int), 22, false)]
        public int SAPCDate;

        ///<summary>
        ///MES处理标识
        ///</summary>
        [FieldMapAttribute("MESFLAG", typeof(string), 1, true)]
        public string Mesflag;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///盘亏/盘盈数量（绝对值）
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///物料编号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///盘亏/盘盈标识
        ///</summary>
        [FieldMapAttribute("TYPE", typeof(string), 3, true)]
        public string Type;

        ///<summary>
        ///库位代码
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 4, true)]
        public string Storagecode;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
        public string Faccode;

        ///<summary>
        ///行项目号
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(int), 22, false)]
        public int Invline;

        ///<summary>
        ///盘点凭证号（盘点凭证inventory number）
        ///</summary>
        [FieldMapAttribute("INVENTORYNO", typeof(string), 64, true)]
        public string Inventoryno;

        ///<summary>
        ///备注（盘点凭证 inventory ref）
        ///</summary>
        [FieldMapAttribute("REMARK", typeof(string), 64, true)]
        public string Remark;

        ///<summary>
        ///凭证日期
        ///</summary>
        [FieldMapAttribute("VOUCHERDATE", typeof(int), 22, true)]
        public int Voucherdate;

        ///<summary>
        ///物料凭证号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 10, false)]
        public string Invno;

        ///<summary>
        ///自增列
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion

    #region DNBatchBak--SAP单据表
    /// <summary>
    /// DNBatchBak--SAP单据表
    /// </summary>
    [Serializable, TableMap("TBLDNBATCHBAK", "")]
    public class DNBatchBak : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public DNBatchBak()
        {
        }

        ///<summary>
        ///供货工厂(10Y2)(UB)
        ///</summary>
        [FieldMapAttribute("FROMFACCODE", typeof(string), 16, true)]
        public string Fromfaccode;

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        ///<summary>
        ///盘点凭证号（盘点凭证Phys. inventory number）(SC)
        ///</summary>
        [FieldMapAttribute("INVENTORYNO", typeof(string), 64, true)]
        public string Inventoryno;

        ///<summary>
        ///凭证日期(SC)
        ///</summary>
        [FieldMapAttribute("VOUCHERDATE", typeof(int), 22, true)]
        public int Voucherdate;

        ///<summary>
        ///申请日期（基准日期）(RS)
        ///</summary>
        [FieldMapAttribute("APPLYDATE", typeof(int), 22, true)]
        public int Applydate;

        ///<summary>
        ///出/入标识(201=出库记入CC,202=从CC入库，241=出库记入固定资产)(RS)
        ///</summary>
        [FieldMapAttribute("MOVETYPE", typeof(string), 12, true)]
        public string Movetype;

        ///<summary>
        ///成本中心(RS)
        ///</summary>
        [FieldMapAttribute("CC", typeof(string), 40, true)]
        public string Cc;

        ///<summary>
        ///检测返工申请人（head our reference）(UB)
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 48, true)]
        public string Reworkapplyuser;

        ///<summary>
        ///物流信息（Head text）(UB)
        ///</summary>
        [FieldMapAttribute("LOGISTICS", typeof(string), 880, true)]
        public string Logistics;

        ///<summary>
        ///OA流水号（head Your Reference）(UB)
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 48, true)]
        public string Oano;

        ///<summary>
        ///调拨类型（UB=库存调拨；ZC=转储；ZJCR=检测返工入；ZJCC=检测返工出；ZBLR=不良品入库；ZCAR=Claim入库）(UB)
        ///</summary>
        [FieldMapAttribute("UBTYPE", typeof(string), 16, true)]
        public string Ubtype;

        ///<summary>
        ///是否硬件光伏（光伏标识From SAP SO field）(Y，N)(DN)
        ///</summary>
        [FieldMapAttribute("GFFLAG", typeof(string), 1, true)]
        public string Gfflag;

        ///<summary>
        ///光伏合同号 Header your reference (SAP，光伏特有)(DN)
        ///</summary>
        [FieldMapAttribute("GFCONTRACTNO", typeof(string), 48, true)]
        public string Gfcontractno;

        ///<summary>
        ///计划交货日期 Planed GI date (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("PLANGIDATE", typeof(int), 22, true)]
        public int Plangidate;

        ///<summary>
        ///收货地址 shipping  location （SAP) (DN)
        ///</summary>
        [FieldMapAttribute("SHIPPINGLOCATION", typeof(string), 528, true)]
        public string Shippinglocation;

        ///<summary>
        ///客户批次号(SAP)\项目名称(DN)
        ///</summary>
        [FieldMapAttribute("CUSBATCHNO", typeof(string), 240, true)]
        public string Cusbatchno;

        ///<summary>
        ///维护时间(DN)
        ///</summary>
        [FieldMapAttribute("DNMTIME", typeof(int), 22, true)]
        public int Dnmtime;

        ///<summary>
        ///维护日期(DN)
        ///</summary>
        [FieldMapAttribute("DNMDATE", typeof(int), 22, true)]
        public int Dnmdate;

        ///<summary>
        ///维护人(DN)
        ///</summary>
        [FieldMapAttribute("DNMUSER", typeof(string), 48, true)]
        public string Dnmuser;

        ///<summary>
        ///订单类型（SO/PO)（SAP_SO订单类型）(DN)
        ///</summary>
        [FieldMapAttribute("CUSORDERNOTYPE", typeof(string), 16, true)]
        public string Cusordernotype;

        ///<summary>
        ///订单号（SO/PO)（SAP SO号）(DN)
        ///</summary>
        [FieldMapAttribute("CUSORDERNO", typeof(string), 40, true)]
        public string Cusorderno;

        ///<summary>
        ///合同号(SAP_SO PO号）(DN)
        ///</summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 140, true)]
        public string Orderno;

        ///<summary>
        ///拣料条件 （SAP)(B，B S)(DN)
        ///</summary>
        [FieldMapAttribute("PICKCONDITION", typeof(string), 32, true)]
        public string Pickcondition;

        ///<summary>
        ///交付方式 （SAP)(物料直发，软件纸件，软件电子件，空)(DN)
        ///</summary>
        [FieldMapAttribute("POSTWAY", typeof(string), 56, true)]
        public string Postway;

        ///<summary>
        ///订单原因 Order reason (SAP) (DN)
        ///</summary>
        [FieldMapAttribute("ORDERREASON", typeof(string), 12, true)]
        public string Orderreason;

        ///<summary>
        ///送达方(DN)
        ///</summary>
        [FieldMapAttribute("SHIPTOPARTY", typeof(string), 40, true)]
        public string Shiptoparty;

        ///<summary>
        ///A=空pending，B=取消cancel，C=发布release(DN)
        ///</summary>
        [FieldMapAttribute("DNBATCHSTATUS", typeof(string), 10, true)]
        public string Dnbatchstatus;

        ///<summary>
        ///发货批次号
        ///</summary>
        [FieldMapAttribute("DNBATCHNO", typeof(string), 20, true)]
        public string Dnbatchno;

        ///<summary>
        ///退货标识(Return PO标识)(X=退货PO，空=正常PO)(PO)
        ///</summary>
        [FieldMapAttribute("RETURNFLAG", typeof(string), 4, true)]
        public string Returnflag;

        ///<summary>
        ///PO备注5--our reference(PO)
        ///</summary>
        [FieldMapAttribute("REMARK5", typeof(string), 48, true)]
        public string Remark5;

        ///<summary>
        ///PO备注4--your reference(PO)
        ///</summary>
        [FieldMapAttribute("REMARK4", typeof(string), 48, true)]
        public string Remark4;

        ///<summary>
        ///PO备注3--header remarks(PO)
        ///</summary>
        [FieldMapAttribute("REMARK3", typeof(string), 200, true)]
        public string Remark3;

        ///<summary>
        ///PO备注2--header note(PO)
        ///</summary>
        [FieldMapAttribute("REMARK2", typeof(string), 200, true)]
        public string Remark2;

        ///<summary>
        ///PO备注1--header text(PO,UB,SC)
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 880, true)]
        public string Remark1;

        ///<summary>
        ///采购组描述（purchase group）(PO)
        ///</summary>
        [FieldMapAttribute("PURCHASEGROUP", typeof(string), 72, true)]
        public string Purchasegroup;

        ///<summary>
        ///采购组(purchase group)(PO)
        ///</summary>
        [FieldMapAttribute("PURCHUGCODE", typeof(string), 12, true)]
        public string Purchugcode;

        ///<summary>
        ///采购组织（purchase org.）(PO)
        ///</summary>
        [FieldMapAttribute("PURCHORGCODE", typeof(string), 16, true)]
        public string Purchorgcode;

        ///<summary>
        ///跟单员电话(PO)
        ///</summary>
        [FieldMapAttribute("BUYERPHONE", typeof(string), 120, true)]
        public string Buyerphone;

        ///<summary>
        ///订单更新时间（时分秒）(PO)
        ///</summary>
        [FieldMapAttribute("POUPDATETIME", typeof(int), 22, true)]
        public int Poupdatetime;

        ///<summary>
        ///订单更新日期（年月日）(PO)
        ///</summary>
        [FieldMapAttribute("POUPDATEDATE", typeof(int), 22, true)]
        public int Poupdatedate;

        ///<summary>
        ///订单创建人员(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("CREATEUSER", typeof(string), 48, true)]
        public string Createuser;

        ///<summary>
        ///订单创建日期（年月日）(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("POCREATEDATE", typeof(int), 22, true)]
        public int Pocreatedate;

        ///<summary>
        ///订单类型(NB)(PO)
        ///</summary>
        [FieldMapAttribute("ORDERTYPE", typeof(string), 16, true)]
        public string Ordertype;

        ///<summary>
        ///供应商代码(PO)
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 160, true)]
        public string Vendorcode;

        ///<summary>
        ///公司代码(取TD28)(PO)
        ///</summary>
        [FieldMapAttribute("COMPANYCODE", typeof(string), 16, true)]
        public string Companycode;

        ///<summary>
        ///订单状态(G=Release,非G=Pending)(PO)
        ///</summary>
        [FieldMapAttribute("ORDERSTATUS", typeof(string), 40, true)]
        public string Orderstatus;

        ///<summary>
        ///是否可创建入库指令(Y:可创建；N：不可创建)
        ///</summary>
        [FieldMapAttribute("ASNAVAILABLE", typeof(string), 160, true)]
        public string Asnavailable;

        ///<summary>
        ///SAP单据完成状态(N:未完成；Y：完成)
        ///</summary>
        [FieldMapAttribute("FINISHFLAG", typeof(string), 1, false)]
        public string Finishflag;

        ///<summary>
        ///SAP单据类型(POR:PO入库；DNR:退货入库；UB:调拨；JCR:检测返工入库；BLR:不良品入库；CAR:CLAIM入库；YFR:研发入库；PD:盘点)
        ///</summary>
        [FieldMapAttribute("INVTYPE", typeof(string), 10, false)]
        public string Invtype;

        ///<summary>
        ///SAP单据状态(Release:发布；Pending:冻结；Cancel:取消)
        ///</summary>
        [FieldMapAttribute("INVSTATUS", typeof(string), 10, true)]
        public string Invstatus;

        ///<summary>
        ///SAP单据号(PO)
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

    }
    #endregion


    #region DNBatchDetailBak-- SAP单据明细表
    /// <summary>
    /// DNBatchDetailBak-- SAP单据明细表 
    /// </summary>
    [Serializable, TableMap("TBLDNBATCHDETAILBAK", "")]
    public class DNBatchDetailBak : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public DNBatchDetailBak()
        {
        }

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        ///<summary>
        ///盘亏/盘盈标识（702/701）(SC)
        ///</summary>
        [FieldMapAttribute("TYPE", typeof(string), 3, true)]
        public string Type;

        ///<summary>
        ///希望到货日期（需求日期）(RS)
        ///</summary>
        [FieldMapAttribute("NEEDDATE", typeof(int), 22, true)]
        public int Needdate;

        ///<summary>
        ///要求到货时间(UB)
        ///</summary>
        [FieldMapAttribute("DEMANDARRIVALDATE", typeof(int), 22, true)]
        public int Demandarrivaldate;

        ///<summary>
        ///收货方物料号（Item text）(UB)
        ///</summary>
        [FieldMapAttribute("RECEIVEMCODE", typeof(string), 140, true)]
        public string Receivemcode;

        ///<summary>
        ///华为物料号（44118421）(UB,RS)
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 140, true)]
        public string Custmcode;

        ///<summary>
        ///收货联系人及联系方式(UB,RS)
        ///</summary>
        [FieldMapAttribute("RECEIVERUSER", typeof(string), 200, true)]
        public string Receiveruser;

        ///<summary>
        ///入库库位地址,收货/发货地址(UB,RS)
        ///</summary>
        [FieldMapAttribute("RECEIVERADDR", typeof(string), 960, true)]
        public string Receiveraddr;

        ///<summary>
        ///鼎桥S编码{物料号}（SAP 需要考虑取数逻辑）——加标识位区分光伏在SO(DN)
        ///</summary>
        [FieldMapAttribute("DQSMCODE", typeof(string), 88, true)]
        public string Dqsmcode;

        ///<summary>
        ///包装箱方式取数 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGWAYNO", typeof(string), 880, true)]
        public string Packingwayno;

        ///<summary>
        ///包装箱规格 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGSPEC", typeof(string), 200, true)]
        public string Packingspec;

        ///<summary>
        ///包装箱编号 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGNO", typeof(string), 80, true)]
        public string Packingno;

        ///<summary>
        ///包装方式 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGWAY", typeof(string), 800, true)]
        public string Packingway;

        ///<summary>
        ///华为型号标签信息 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("HWTYPEINFO", typeof(string), 200, true)]
        public string Hwtypeinfo;

        ///<summary>
        ///华为编码数量单位(DN)
        ///</summary>
        [FieldMapAttribute("HWCODEUNIT", typeof(string), 12, true)]
        public string Hwcodeunit;

        ///<summary>
        ///华为编码数量 （SAP计算出来的，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("HWCODEQTY", typeof(decimal), 22, true)]
        public decimal Hwcodeqty;

        ///<summary>
        ///光伏华为描述(DN)
        ///</summary>
        [FieldMapAttribute("GFHWDESC", typeof(string), 960, true)]
        public string Gfhwdesc;

        ///<summary>
        ///光伏包装序号 Purchase order item(SAP，光伏特有)(DN)
        ///</summary>
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 24, true)]
        public string Gfpackingseq;

        ///<summary>
        ///光伏华为编码 item your reference (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("GFHWMCODE", typeof(string), 100, true)]
        public string Gfhwmcode;

        ///<summary>
        ///供应商物料编码_verdor material number (SAP，取数逻辑参考download报告) (DN)
        ///</summary>
        [FieldMapAttribute("VENDERMCODE", typeof(string), 140, true)]
        public string Vendermcode;

        ///<summary>
        ///客户物料描述（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSITEMDESC", typeof(string), 960, true)]
        public string Cusitemdesc;

        ///<summary>
        ///客户物料型号（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSITEMSPEC", typeof(string), 200, true)]
        public string Cusitemspec;

        ///<summary>
        ///客户物料编码（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSMCODE", typeof(string), 100, true)]
        public string Cusmcode;

        ///<summary>
        ///移动类型(DN)
        ///</summary>
        [FieldMapAttribute("MOVEMENTTYPE", typeof(string), 12, true)]
        public string Movementtype;

        ///<summary>
        ///上层行项目号(Hign Level Item)(DN)
        ///</summary>
        [FieldMapAttribute("HIGNLEVELITEM", typeof(int), 22, true)]
        public int Hignlevelitem;

        ///<summary>
        ///成本中心(CC号)(PO)
        ///</summary>
        [FieldMapAttribute("CCNO", typeof(string), 40, true)]
        public string Ccno;

        ///<summary>
        ///SO WBS号(PO)
        ///</summary>
        [FieldMapAttribute("SOWBSNO", typeof(string), 32, true)]
        public string Sowbsno;

        ///<summary>
        ///销售订单行项目号(SO item号)(PO)
        ///</summary>
        [FieldMapAttribute("SOITEMNO", typeof(string), 24, true)]
        public string Soitemno;

        ///<summary>
        ///销售订单号(SO号)(PO)
        ///</summary>
        [FieldMapAttribute("SO", typeof(string), 40, true)]
        public string So;

        ///<summary>
        ///国际贸易条件描述(incoterms 2)(PO)
        ///</summary>
        [FieldMapAttribute("INCOTERMS2", typeof(string), 112, true)]
        public string Incoterms2;

        ///<summary>
        ///国际贸易条件(incoterms 1)(PO)
        ///</summary>
        [FieldMapAttribute("INCOTERMS1", typeof(string), 12, true)]
        public string Incoterms1;

        ///<summary>
        ///行项目类别（item category）(PO)
        ///</summary>
        [FieldMapAttribute("ITEMCATEGORY", typeof(string), 1, true)]
        public string Itemcategory;

        ///<summary>
        ///科目分配（account assignment）(PO)
        ///</summary>
        [FieldMapAttribute("ACCOUNTASSIGNMENT", typeof(string), 1, true)]
        public string Accountassignment;

        ///<summary>
        ///退货标识(Return PO标识)(X=退货PO，空=正常PO)(PO)
        ///</summary>
        [FieldMapAttribute("RETURNFLAG", typeof(string), 1, true)]
        public string Returnflag;

        ///<summary>
        ///采购申请号(PR号)(PO,RS)
        ///</summary>
        [FieldMapAttribute("PRNO", typeof(string), 40, true)]
        public string Prno;

        ///<summary>
        ///供应商物料编码(PO)
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 140, true)]
        public string Vendormcode;

        ///<summary>
        ///行项目备注（item text）(PO)
        ///</summary>
        [FieldMapAttribute("DETAILREMARK", typeof(string), 880, true)]
        public string Detailremark;

        ///<summary>
        ///行项目状态(PO,UB,RS)
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        ///<summary>
        ///送货地址（行项目）(PO)
        ///</summary>
        [FieldMapAttribute("SHIPADDR", typeof(string), 960, true)]
        public string Shipaddr;

        ///<summary>
        ///物料单位(PO,DN)
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///需求日期（年月日）(PO)
        ///</summary>
        [FieldMapAttribute("PLANDATE", typeof(int), 22, true)]
        public int Plandate;

        ///<summary>
        ///已出库数量
        ///</summary>
        [FieldMapAttribute("OUTQTY", typeof(decimal), 22, true)]
        public decimal Outqty;

        ///<summary>
        ///已入库数量
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 22, true)]
        public decimal Actqty;

        ///<summary>
        ///需求数量(PO,DN)
        ///</summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 22, false)]
        public decimal Planqty;

        ///<summary>
        ///物料长描述(PO)
        ///</summary>
        [FieldMapAttribute("MLONGDESC", typeof(string), 880, true)]
        public string Mlongdesc;

        ///<summary>
        ///物料短描述(PO)
        ///</summary>
        [FieldMapAttribute("MENSHORTDESC", typeof(string), 160, true)]
        public string Menshortdesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string Dqmcode;

        ///<summary>
        ///物料编码(PO,DN)
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///库位(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string Storagecode;

        ///<summary>
        ///工厂代码(Plant)(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
        public string Faccode;

        ///<summary>
        ///出库库位( item requisitioner)(UB)
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 12, true)]
        public string Fromstoragecode;

        ///<summary>
        ///SAP单据号行状态(Release:发布；Pending:冻结；Cancel:取消)
        ///</summary>
        [FieldMapAttribute("INVLINESTATUS", typeof(string), 40, true)]
        public string Invlinestatus;

        ///<summary>
        ///SAP单据号行项目号
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(int), 22, false)]
        public int Invline;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 10, false)]
        public string Invno;

    }
    #endregion


    #region Invoices--SAP单据表
    /// <summary>
    /// TBLINVOICES--SAP单据表
    /// </summary>
    [Serializable, TableMap("TBLINVOICES", "INVNO")]
    public class Invoices : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Invoices()
        {
        }

        [FieldMapAttribute("NOTOUTCHECKFLAG", typeof(string), 1, true)]
        public string NotOutCheckFlag;

        ///<summary>
        ///供货工厂(10Y2)(UB)
        ///</summary>
        [FieldMapAttribute("FROMFACCODE", typeof(string), 16, true)]
        public string Fromfaccode;

        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 160, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 160, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 160, true)]
        public string Eattribute1;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        [FieldMapAttribute("SAPCUSER", typeof(string), 40, false)]
        public string SAPCuser;

        ///<summary>
        ///同步时间
        ///</summary>
        [FieldMapAttribute("SAPCTIME", typeof(int), 22, false)]
        public int SAPCTime;

        ///<summary>
        ///同步日期
        ///</summary>
        [FieldMapAttribute("SAPCDATE", typeof(int), 22, false)]
        public int SAPCDate;

        ///<summary>
        ///盘点凭证号（盘点凭证Phys. inventory number）(SC)
        ///</summary>
        [FieldMapAttribute("INVENTORYNO", typeof(string), 64, true)]
        public string Inventoryno;

        ///<summary>
        ///凭证日期(SC)
        ///</summary>
        [FieldMapAttribute("VOUCHERDATE", typeof(int), 22, true)]
        public int Voucherdate;

        ///<summary>
        ///申请日期（基准日期）(RS)
        ///</summary>
        [FieldMapAttribute("APPLYDATE", typeof(int), 22, true)]
        public int Applydate;

        ///<summary>
        ///出/入标识(201=出库记入CC,202=从CC入库，241=出库记入固定资产)(RS)
        ///</summary>
        [FieldMapAttribute("MOVETYPE", typeof(string), 12, true)]
        public string Movetype;

        ///<summary>
        ///成本中心(RS)
        ///</summary>
        [FieldMapAttribute("CC", typeof(string), 40, true)]
        public string Cc;

        ///<summary>
        ///检测返工申请人（head our reference）(UB)
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 48, true)]
        public string Reworkapplyuser;

        ///<summary>
        ///物流信息（Head text）(UB)
        ///</summary>
        [FieldMapAttribute("LOGISTICS", typeof(string), 880, true)]
        public string Logistics;

        ///<summary>
        ///OA流水号（head Your Reference）(UB)
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 48, true)]
        public string Oano;

        ///<summary>
        ///调拨类型（UB=库存调拨；ZC=转储；ZJCR=检测返工入；ZJCC=检测返工出；ZBLR=不良品入库；ZCAR=Claim入库）(UB)
        ///</summary>
        [FieldMapAttribute("UBTYPE", typeof(string), 16, true)]
        public string Ubtype;

        ///<summary>
        ///是否硬件光伏（光伏标识From SAP SO field）(Y，N)(DN)
        ///</summary>
        [FieldMapAttribute("GFFLAG", typeof(string), 1, true)]
        public string Gfflag;

        ///<summary>
        ///光伏合同号 Header your reference (SAP，光伏特有)(DN)
        ///</summary>
        [FieldMapAttribute("GFCONTRACTNO", typeof(string), 48, true)]
        public string Gfcontractno;

        ///<summary>
        ///计划交货日期 Planed GI date (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("PLANGIDATE", typeof(int), 22, true)]
        public int Plangidate;

        ///<summary>
        ///收货地址 shipping  location （SAP) (DN)
        ///</summary>
        [FieldMapAttribute("SHIPPINGLOCATION", typeof(string), 528, true)]
        public string Shippinglocation;

        ///<summary>
        ///客户批次号(SAP)\项目名称(DN)
        ///</summary>
        [FieldMapAttribute("CUSBATCHNO", typeof(string), 240, true)]
        public string Cusbatchno;

        ///<summary>
        ///维护时间(DN)
        ///</summary>
        [FieldMapAttribute("DNMTIME", typeof(int), 22, true)]
        public int Dnmtime;

        ///<summary>
        ///维护日期(DN)
        ///</summary>
        [FieldMapAttribute("DNMDATE", typeof(int), 22, true)]
        public int Dnmdate;

        ///<summary>
        ///维护人(DN)
        ///</summary>
        [FieldMapAttribute("DNMUSER", typeof(string), 48, true)]
        public string Dnmuser;

        ///<summary>
        ///订单类型（SO/PO)（SAP_SO订单类型）(DN)
        ///</summary>
        [FieldMapAttribute("CUSORDERNOTYPE", typeof(string), 16, true)]
        public string Cusordernotype;

        ///<summary>
        ///订单号（SO/PO)（SAP SO号）(DN)
        ///</summary>
        [FieldMapAttribute("CUSORDERNO", typeof(string), 40, true)]
        public string Cusorderno;

        ///<summary>
        ///合同号(SAP_SO PO号）(DN)
        ///</summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 140, true)]
        public string Orderno;

        ///<summary>
        ///拣料条件 （SAP)(B，B S)(DN)
        ///</summary>
        [FieldMapAttribute("PICKCONDITION", typeof(string), 32, true)]
        public string Pickcondition;

        ///<summary>
        ///交付方式 （SAP)(物料直发，软件纸件，软件电子件，空)(DN)
        ///</summary>
        [FieldMapAttribute("POSTWAY", typeof(string), 56, true)]
        public string Postway;

        ///<summary>
        ///订单原因 Order reason (SAP) (DN)
        ///</summary>
        [FieldMapAttribute("ORDERREASON", typeof(string), 12, true)]
        public string Orderreason;

        ///<summary>
        ///送达方(DN)
        ///</summary>
        [FieldMapAttribute("SHIPTOPARTY", typeof(string), 40, true)]
        public string Shiptoparty;

        ///<summary>
        ///A=空pending，B=取消cancel，C=发布release(DN)
        ///</summary>
        [FieldMapAttribute("DNBATCHSTATUS", typeof(string), 10, true)]
        public string Dnbatchstatus;

        ///<summary>
        ///发货批次号
        ///</summary>
        [FieldMapAttribute("DNBATCHNO", typeof(string), 20, true)]
        public string Dnbatchno;

        ///<summary>
        ///退货标识(Return PO标识)(X=退货PO，空=正常PO)(PO)
        ///</summary>
        [FieldMapAttribute("RETURNFLAG", typeof(string), 4, true)]
        public string Returnflag;

        ///<summary>
        ///PO备注5--our reference(PO)
        ///</summary>
        [FieldMapAttribute("REMARK5", typeof(string), 48, true)]
        public string Remark5;

        ///<summary>
        ///PO备注4--your reference(PO)
        ///</summary>
        [FieldMapAttribute("REMARK4", typeof(string), 48, true)]
        public string Remark4;

        ///<summary>
        ///PO备注3--header remarks(PO)
        ///</summary>
        [FieldMapAttribute("REMARK3", typeof(string), 200, true)]
        public string Remark3;

        ///<summary>
        ///PO备注2--header note(PO)
        ///</summary>
        [FieldMapAttribute("REMARK2", typeof(string), 200, true)]
        public string Remark2;

        ///<summary>
        ///PO备注1--header text(PO,UB,SC)
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 880, true)]
        public string Remark1;

        ///<summary>
        ///采购组描述（purchase group）(PO)
        ///</summary>
        [FieldMapAttribute("PURCHASEGROUP", typeof(string), 72, true)]
        public string Purchasegroup;

        ///<summary>
        ///采购组(purchase group)(PO)
        ///</summary>
        [FieldMapAttribute("PURCHUGCODE", typeof(string), 12, true)]
        public string Purchugcode;

        ///<summary>
        ///采购组织（purchase org.）(PO)
        ///</summary>
        [FieldMapAttribute("PURCHORGCODE", typeof(string), 16, true)]
        public string Purchorgcode;

        ///<summary>
        ///跟单员电话(PO)
        ///</summary>
        [FieldMapAttribute("BUYERPHONE", typeof(string), 120, true)]
        public string Buyerphone;

        ///<summary>
        ///订单更新时间（时分秒）(PO)
        ///</summary>
        [FieldMapAttribute("POUPDATETIME", typeof(int), 22, true)]
        public int Poupdatetime;

        ///<summary>
        ///订单更新日期（年月日）(PO)
        ///</summary>
        [FieldMapAttribute("POUPDATEDATE", typeof(int), 22, true)]
        public int Poupdatedate;

        ///<summary>
        ///订单创建人员(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("CREATEUSER", typeof(string), 48, true)]
        public string Createuser;

        ///<summary>
        ///订单创建日期（年月日）(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("POCREATEDATE", typeof(int), 22, true)]
        public int Pocreatedate;

        ///<summary>
        ///订单类型(NB)(PO)
        ///</summary>
        [FieldMapAttribute("ORDERTYPE", typeof(string), 16, true)]
        public string Ordertype;

        ///<summary>
        ///供应商代码(PO)
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 160, true)]
        public string Vendorcode;

        ///<summary>
        ///公司代码(取TD28)(PO)
        ///</summary>
        [FieldMapAttribute("COMPANYCODE", typeof(string), 16, true)]
        public string Companycode;

        ///<summary>
        ///订单状态(G=Release,非G=Pending)(PO)
        ///</summary>
        [FieldMapAttribute("ORDERSTATUS", typeof(string), 40, true)]
        public string Orderstatus;

        ///<summary>
        ///是否可创建入库指令(Y:可创建；N：不可创建)
        ///</summary>
        [FieldMapAttribute("ASNAVAILABLE", typeof(string), 160, true)]
        public string Asnavailable;

        ///<summary>
        ///SAP单据完成状态(N:未完成；Y：完成)
        ///</summary>
        [FieldMapAttribute("FINISHFLAG", typeof(string), 1, false)]
        public string Finishflag;

        ///<summary>
        ///SAP单据类型(POR:PO入库；DNR:退货入库；UB:调拨；JCR:检测返工入库；BLR:不良品入库；CAR:CLAIM入库；YFR:研发入库；PD:盘点)
        ///</summary>
        [FieldMapAttribute("INVTYPE", typeof(string), 10, false)]
        public string Invtype;

        ///<summary>
        ///SAP单据状态(Release:发布；Pending:冻结；Cancel:取消)
        ///</summary>
        [FieldMapAttribute("INVSTATUS", typeof(string), 10, true)]
        public string Invstatus;

        ///<summary>
        ///SAP单据号(PO)
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

    }
    #endregion

    #region Invoicesdetail-- SAP单据明细表
    /// <summary>
    /// TBLINVOICESDETAIL-- SAP单据明细表 
    /// </summary>
    [Serializable, TableMap("TBLINVOICESDETAIL", "INVNO,INVLINE")]
    public class Invoicesdetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Invoicesdetail()
        {
        }


        ///<summary>
        ///预留字段3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///预留字段2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;

        ///<summary>
        ///预留字段1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        ///<summary>
        ///盘亏/盘盈标识（702/701）(SC)
        ///</summary>
        [FieldMapAttribute("TYPE", typeof(string), 3, true)]
        public string Type;

        ///<summary>
        ///希望到货日期（需求日期）(RS)
        ///</summary>
        [FieldMapAttribute("NEEDDATE", typeof(int), 22, true)]
        public int Needdate;

        ///<summary>
        ///要求到货时间(UB)
        ///</summary>
        [FieldMapAttribute("DEMANDARRIVALDATE", typeof(int), 22, true)]
        public int Demandarrivaldate;

        ///<summary>
        ///收货方物料号（Item text）(UB)
        ///</summary>
        [FieldMapAttribute("RECEIVEMCODE", typeof(string), 140, true)]
        public string Receivemcode;

        ///<summary>
        ///华为物料号（44118421）(UB,RS)
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 140, true)]
        public string Custmcode;

        ///<summary>
        ///收货联系人及联系方式(UB,RS)
        ///</summary>
        [FieldMapAttribute("RECEIVERUSER", typeof(string), 200, true)]
        public string Receiveruser;

        ///<summary>
        ///入库库位地址,收货/发货地址(UB,RS)
        ///</summary>
        [FieldMapAttribute("RECEIVERADDR", typeof(string), 960, true)]
        public string Receiveraddr;

        ///<summary>
        ///鼎桥S编码{物料号}（SAP 需要考虑取数逻辑）——加标识位区分光伏在SO(DN)
        ///</summary>
        [FieldMapAttribute("DQSMCODE", typeof(string), 88, true)]
        public string Dqsmcode;

        ///<summary>
        ///包装箱方式取数 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGWAYNO", typeof(string), 880, true)]
        public string Packingwayno;

        ///<summary>
        ///包装箱规格 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGSPEC", typeof(string), 200, true)]
        public string Packingspec;

        ///<summary>
        ///包装箱编号 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGNO", typeof(string), 80, true)]
        public string Packingno;

        ///<summary>
        ///包装方式 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGWAY", typeof(string), 800, true)]
        public string Packingway;

        ///<summary>
        ///华为型号标签信息 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("HWTYPEINFO", typeof(string), 200, true)]
        public string Hwtypeinfo;

        ///<summary>
        ///华为编码数量单位(DN)
        ///</summary>
        [FieldMapAttribute("HWCODEUNIT", typeof(string), 12, true)]
        public string Hwcodeunit;

        ///<summary>
        ///华为编码数量 （SAP计算出来的，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("HWCODEQTY", typeof(decimal), 22, true)]
        public decimal Hwcodeqty;

        ///<summary>
        ///光伏华为描述(DN)
        ///</summary>
        [FieldMapAttribute("GFHWDESC", typeof(string), 960, true)]
        public string Gfhwdesc;

        ///<summary>
        ///光伏包装序号 Purchase order item(SAP，光伏特有)(DN)
        ///</summary>
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 24, true)]
        public string Gfpackingseq;

        ///<summary>
        ///光伏华为编码 item your reference (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("GFHWMCODE", typeof(string), 100, true)]
        public string Gfhwmcode;

        ///<summary>
        ///供应商物料编码_verdor material number (SAP，取数逻辑参考download报告) (DN)
        ///</summary>
        [FieldMapAttribute("VENDERMCODE", typeof(string), 140, true)]
        public string Vendermcode;

        ///<summary>
        ///客户物料描述（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSITEMDESC", typeof(string), 960, true)]
        public string Cusitemdesc;

        ///<summary>
        ///客户物料型号（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSITEMSPEC", typeof(string), 200, true)]
        public string Cusitemspec;

        ///<summary>
        ///客户物料编码（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSMCODE", typeof(string), 100, true)]
        public string Cusmcode;

        ///<summary>
        ///移动类型(DN)
        ///</summary>
        [FieldMapAttribute("MOVEMENTTYPE", typeof(string), 12, true)]
        public string Movementtype;

        ///<summary>
        ///上层行项目号(Hign Level Item)(DN)
        ///</summary>
        [FieldMapAttribute("HIGNLEVELITEM", typeof(int), 22, true)]
        public int Hignlevelitem;

        ///<summary>
        ///成本中心(CC号)(PO)
        ///</summary>
        [FieldMapAttribute("CCNO", typeof(string), 40, true)]
        public string Ccno;

        ///<summary>
        ///SO WBS号(PO)
        ///</summary>
        [FieldMapAttribute("SOWBSNO", typeof(string), 32, true)]
        public string Sowbsno;

        ///<summary>
        ///销售订单行项目号(SO item号)(PO)
        ///</summary>
        [FieldMapAttribute("SOITEMNO", typeof(string), 24, true)]
        public string Soitemno;

        ///<summary>
        ///销售订单号(SO号)(PO)
        ///</summary>
        [FieldMapAttribute("SO", typeof(string), 40, true)]
        public string So;

        ///<summary>
        ///国际贸易条件描述(incoterms 2)(PO)
        ///</summary>
        [FieldMapAttribute("INCOTERMS2", typeof(string), 112, true)]
        public string Incoterms2;

        ///<summary>
        ///国际贸易条件(incoterms 1)(PO)
        ///</summary>
        [FieldMapAttribute("INCOTERMS1", typeof(string), 12, true)]
        public string Incoterms1;

        ///<summary>
        ///行项目类别（item category）(PO)
        ///</summary>
        [FieldMapAttribute("ITEMCATEGORY", typeof(string), 1, true)]
        public string Itemcategory;

        ///<summary>
        ///科目分配（account assignment）(PO)
        ///</summary>
        [FieldMapAttribute("ACCOUNTASSIGNMENT", typeof(string), 1, true)]
        public string Accountassignment;

        ///<summary>
        ///退货标识(Return PO标识)(X=退货PO，空=正常PO)(PO)
        ///</summary>
        [FieldMapAttribute("RETURNFLAG", typeof(string), 1, true)]
        public string Returnflag;

        ///<summary>
        ///采购申请号(PR号)(PO,RS)
        ///</summary>
        [FieldMapAttribute("PRNO", typeof(string), 40, true)]
        public string Prno;

        ///<summary>
        ///供应商物料编码(PO)
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 140, true)]
        public string Vendormcode;

        ///<summary>
        ///行项目备注（item text）(PO)
        ///</summary>
        [FieldMapAttribute("DETAILREMARK", typeof(string), 880, true)]
        public string Detailremark;

        ///<summary>
        ///行项目状态(PO,UB,RS)
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        ///<summary>
        ///送货地址（行项目）(PO)
        ///</summary>
        [FieldMapAttribute("SHIPADDR", typeof(string), 960, true)]
        public string Shipaddr;

        ///<summary>
        ///物料单位(PO,DN)
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 3, true)]
        public string Unit;

        ///<summary>
        ///需求日期（年月日）(PO)
        ///</summary>
        [FieldMapAttribute("PLANDATE", typeof(int), 22, true)]
        public int Plandate;

        ///<summary>
        ///已出库数量
        ///</summary>
        [FieldMapAttribute("OUTQTY", typeof(decimal), 22, true)]
        public decimal Outqty;

        ///<summary>
        ///已入库数量
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 22, true)]
        public decimal Actqty;

        ///<summary>
        ///需求数量(PO,DN)
        ///</summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 22, false)]
        public decimal Planqty;

        ///<summary>
        ///物料长描述(PO)
        ///</summary>
        [FieldMapAttribute("MLONGDESC", typeof(string), 880, true)]
        public string Mlongdesc;

        ///<summary>
        ///物料短描述(PO)
        ///</summary>
        [FieldMapAttribute("MENSHORTDESC", typeof(string), 160, true)]
        public string Menshortdesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string Dqmcode;

        ///<summary>
        ///物料编码(PO,DN)
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, false)]
        public string Mcode;

        ///<summary>
        ///库位(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string Storagecode;

        ///<summary>
        ///工厂代码(Plant)(PO,DN,UB)
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 4, true)]
        public string Faccode;

        ///<summary>
        ///出库库位( item requisitioner)(UB)
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 12, true)]
        public string Fromstoragecode;

        ///<summary>
        ///SAP单据号行状态(Release:发布；Pending:冻结；Cancel:取消)
        ///</summary>
        [FieldMapAttribute("INVLINESTATUS", typeof(string), 40, true)]
        public string Invlinestatus;

        ///<summary>
        ///SAP单据号行项目号
        ///</summary>
        [FieldMapAttribute("INVLINE", typeof(int), 22, false)]
        public int Invline;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 10, false)]
        public string Invno;

    }
    #endregion


    #region TBLSERIALBOOK
    /// <summary>
    /// 工单关联的序列号
    /// </summary>
    [Serializable, TableMap("TBLSERIALBOOK", "SNPREFIX")]
    public class SERIALBOOK : DomainObject
    {
        public SERIALBOOK()
        {
        }

        /// <summary>
        /// 序列号前缀
        /// </summary>
        [FieldMapAttribute("SNPREFIX", typeof(string), 40, false)]
        public string SNPrefix;

        ///// <summary>
        ///// 日期
        ///// </summary>
        //[FieldMapAttribute("DATECODE", typeof(string), 40, false)]
        //public string DateCode;

        /// <summary>
        /// 序列号最大Serial号码
        /// </summary>
        [FieldMapAttribute("MAXSERIAL", typeof(string), 40, false)]
        public string MaxSerial;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MUser;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MTime;

        /// <summary>
        /// 进制
        /// </summary>
        [FieldMapAttribute("SERIALTYPE", typeof(string), 40, true)]
        public string SerialType;

    }
    #endregion


    #region Pick-- 拣货任务令头
    /// <summary>
    /// TBLPICK-- 拣货任务令头 
    /// </summary>
    [Serializable, TableMap("TBLPICK", "PICKNO")]
    public class Pick : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Pick()
        {
        }

        [FieldMapAttribute("VENDERCODE", typeof(string), 40, false)]
        public string VenderCode;

        ///<summary>
        ///Orderno
        ///</summary>
        [FieldMapAttribute("ORDERNO", typeof(string), 20, true)]
        public string Orderno;

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 200, true)]
        public string Stno;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期 
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        ///<summary>
        ///备注
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string Reworkapplyuser;

        ///<summary>
        ///收货地址
        ///</summary>
        [FieldMapAttribute("RECEIVERADDR", typeof(string), 100, true)]
        public string Receiveraddr;

        ///<summary>
        ///收货人
        ///</summary>
        [FieldMapAttribute("RECEIVERUSER", typeof(string), 40, true)]
        public string Receiveruser;

        ///<summary>
        ///出库时间
        ///</summary>
        [FieldMapAttribute("DELIVERY_TIME", typeof(int), 22, true)]
        public int Delivery_time;

        ///<summary>
        ///出库日期
        ///</summary>
        [FieldMapAttribute("DELIVERY_DATE", typeof(int), 22, true)]
        public int Delivery_date;

        ///<summary>
        ///箱单完成时间
        ///</summary>
        [FieldMapAttribute("PACKING_LIST_TIME", typeof(int), 22, true)]
        public int Packing_list_time;

        ///<summary>
        ///箱单完成日期
        ///</summary>
        [FieldMapAttribute("PACKING_LIST_DATE", typeof(int), 22, true)]
        public int Packing_list_date;

        ///<summary>
        ///OQC完成时间
        ///</summary>
        [FieldMapAttribute("OQC_TIME", typeof(int), 22, true)]
        public int Oqc_time;

        ///<summary>
        ///OQC完成日期
        ///</summary>
        [FieldMapAttribute("OQC_DATE", typeof(int), 22, true)]
        public int Oqc_date;

        ///<summary>
        ///捡料完成时间
        ///</summary>
        [FieldMapAttribute("FINISH_TIME", typeof(int), 22, true)]
        public int Finish_time;

        ///<summary>
        ///捡料完成日期
        ///</summary>
        [FieldMapAttribute("FINISH_DATE", typeof(int), 22, true)]
        public int Finish_date;

        ///<summary>
        ///客户唛头完成时间
        ///</summary>
        [FieldMapAttribute("SHIPPING_MARK_TIME", typeof(int), 22, true)]
        public int Shipping_mark_time;

        ///<summary>
        ///客户唛头完成日期
        ///</summary>
        [FieldMapAttribute("SHIPPING_MARK_DATE", typeof(int), 22, true)]
        public int Shipping_mark_date;

        ///<summary>
        ///客户唛头完成人
        ///</summary>
        [FieldMapAttribute("SHIPPING_MARK_USER", typeof(string), 40, true)]
        public string Shipping_mark_user;

        ///<summary>
        ///下发时间
        ///</summary>
        [FieldMapAttribute("DOWN_TIME", typeof(int), 22, true)]
        public int Down_time;

        ///<summary>
        ///下发日期
        ///</summary>
        [FieldMapAttribute("DOWN_DATE", typeof(int), 22, true)]
        public int Down_date;

        ///<summary>
        ///下发人
        ///</summary>
        [FieldMapAttribute("DOWN_USER", typeof(string), 40, true)]
        public string Down_user;

        ///<summary>
        ///OA流水号（head Your Reference）
        ///</summary>
        [FieldMapAttribute("OANO", typeof(string), 40, true)]
        public string Oano;

        ///<summary>
        ///创建拣货任务令时间
        ///</summary>
        [FieldMapAttribute("CREATE_PICK_TIME", typeof(int), 22, true)]
        public int Create_pick_time;

        ///<summary>
        ///创建拣货任务令日期
        ///</summary>
        [FieldMapAttribute("CREATE_PICK_DATE", typeof(int), 22, true)]
        public int Create_pick_date;

        ///<summary>
        ///光伏标识（From SAP SO field）(DN)
        ///</summary>
        [FieldMapAttribute("GFFLAG", typeof(string), 40, true)]
        public string Gfflag;

        ///<summary>
        ///客户批次号（SAP 项目名称)(DN)
        ///</summary>
        [FieldMapAttribute("CUSBATCHNO", typeof(string), 40, true)]
        public string Cusbatchno;

        ///<summary>
        ///计划交货日期 Planed GI date (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("PLANGIDATE", typeof(string), 40, true)]
        public string Plangidate;

        ///<summary>
        ///计划发货日期
        ///</summary>
        [FieldMapAttribute("PLAN_DATE", typeof(int), 22, true)]
        public int Plan_date;

        ///<summary>
        ///出库库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string Storagecode;

        ///<summary>
        ///出库工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string Faccode;

        ///<summary>
        ///入库库位
        ///</summary>
        [FieldMapAttribute("INSTORAGECODE", typeof(string), 40, true)]
        public string Instoragecode;

        ///<summary>
        ///入库工厂
        ///</summary>
        [FieldMapAttribute("INFACCODE", typeof(string), 40, true)]
        public string Infaccode;

        ///<summary>
        ///PO号
        ///</summary>
        [FieldMapAttribute("PONO", typeof(string), 40, true)]
        public string Pono;

        ///<summary>
        ///序号
        ///</summary>
        //[FieldMapAttribute("SERIAL_NUMBER", typeof(int), 22, true)]
        //public int Serial_number;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string Invno;

        ///<summary>
        ///状态:初始化，已下发，捡料完成，包装完成，OQC校验完成，箱单完成，已出库，取消
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///拣货任务令号类型(对应SAP单据类型)
        ///</summary>
        [FieldMapAttribute("PICKTYPE", typeof(string), 40, false)]
        public string Picktype;

        ///<summary>
        ///拣货任务令号
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, false)]
        public string Pickno;

    }
    #endregion
    #region Pickdetail--拣货任务令明细表
    /// <summary>
    /// TBLPICKDETAIL--拣货任务令明细表
    /// </summary>
    [Serializable, TableMap("TBLPICKDETAIL", "PICKNO,PICKLINE")]
    public class Pickdetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Pickdetail()
        {
        }

        [FieldMapAttribute("DETAILREMARK", typeof(string), 880, true)]
        public string Detailremark;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, true)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MaintainDate;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, true)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, true)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, true)]
        public string Cuser;

        ///<summary>
        ///鼎桥S编码{物料号}（SAP 需要考虑取数逻辑）——加标识位区分光伏在SO(DN)
        ///</summary>
        [FieldMapAttribute("DQSITEMCODE", typeof(string), 40, true)]
        public string Dqsitemcode;

        ///<summary>
        ///包装箱方式取数 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGWAYNO", typeof(string), 40, true)]
        public string Packingwayno;

        ///<summary>
        ///包装箱规格 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGSPEC", typeof(string), 40, true)]
        public string Packingspec;

        ///<summary>
        ///包装箱编号 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGNO", typeof(string), 40, true)]
        public string Packingno;

        ///<summary>
        ///包装方式 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("PACKINGWAY", typeof(string), 40, true)]
        public string Packingway;

        ///<summary>
        ///华为型号标签信息 （SAP自建表取数，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("HWTYPEINFO", typeof(string), 40, true)]
        public string Hwtypeinfo;

        ///<summary>
        ///华为编码数量 （SAP计算出来的，光伏特有）(DN)
        ///</summary>
        [FieldMapAttribute("HWCODEQTY", typeof(string), 40, true)]
        public string Hwcodeqty;

        ///<summary>
        ///拣料条件 （SAP)(DN)
        ///</summary>
        [FieldMapAttribute("PICKCONDITION", typeof(string), 40, true)]
        public string Pickcondition;

        ///<summary>
        ///交付方式 （SAP)(DN)
        ///</summary>
        [FieldMapAttribute("POSTWAY", typeof(string), 40, true)]
        public string Postway;

        ///<summary>
        ///光伏包装序号 Purchase order item(SAP，光伏特有)(DN)
        ///</summary>
        [FieldMapAttribute("GFPACKINGSEQ", typeof(string), 40, true)]
        public string Gfpackingseq;

        ///<summary>
        ///光伏华为编码 item your reference (SAP)(DN)
        ///</summary>
        [FieldMapAttribute("GFHWITEMCODE", typeof(string), 40, true)]
        public string Gfhwitemcode;

        ///<summary>
        ///光伏合同号 Header your reference (SAP，光伏特有)(DN)
        ///</summary>
        [FieldMapAttribute("GFCONTRACTNO", typeof(string), 40, true)]
        public string Gfcontractno;

        ///<summary>
        ///供应商物料编码_verdor material number (SAP，取数逻辑参考download报告) (DN)
        ///</summary>
        [FieldMapAttribute("VENDERITEMCODE", typeof(string), 40, true)]
        public string Venderitemcode;

        ///<summary>
        ///客户物料描述（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSITEMDESC", typeof(string), 40, true)]
        public string Cusitemdesc;

        ///<summary>
        ///客户物料型号（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSITEMSPEC", typeof(string), 40, true)]
        public string Cusitemspec;

        ///<summary>
        ///客户物料编码（SAP) from SO(DN)
        ///</summary>
        [FieldMapAttribute("CUSITEMCODE", typeof(string), 40, true)]
        public string Cusitemcode;

        ///<summary>
        ///订单类型（SO/PO)（SAP_SO订单类型）(DN)
        ///</summary>
        [FieldMapAttribute("CUSORDERNOTYPE", typeof(string), 2, true)]
        public string Cusordernotype;

        ///<summary>
        ///订单号（SO/PO)（SAP SO号）(DN)
        ///</summary>
        [FieldMapAttribute("CUSORDERNO", typeof(string), 10, true)]
        public string Cusorderno;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///SQE通过的数量
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(decimal), 22, true)]
        public decimal Qcpassqty;

        ///<summary>
        ///已出库数量
        ///</summary>
        [FieldMapAttribute("OUTQTY", typeof(decimal), 22, true)]
        public decimal Outqty;

        ///<summary>
        ///已包装数量
        ///</summary>
        [FieldMapAttribute("PQTY", typeof(decimal), 22, true)]
        public decimal Pqty;

        ///<summary>
        ///已拣货数量
        ///</summary>
        [FieldMapAttribute("SQTY", typeof(decimal), 22, true)]
        public decimal Sqty;

        ///<summary>
        ///欠料发货数量
        ///</summary>
        [FieldMapAttribute("OWEQTY", typeof(decimal), 22, true)]
        public decimal Oweqty;

        ///<summary>
        ///需求数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, true)]
        public decimal Qty;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string Mdesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, true)]
        public string Dqmcode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string Mcode;

        ///<summary>
        ///华为物料号
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string Custmcode;

        ///<summary>
        ///状态:Release:初始化，WaitPick:待拣料，Pick:拣料中，ClosePick:拣料完成， Pack:包装中，ClosePack:包装完成，Cancel:取消；Owe:欠料
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        ///<summary>
        ///拣货任务令行项目号
        ///</summary>
        [FieldMapAttribute("PICKLINE", typeof(string), 6, true)]
        public string Pickline;

        ///<summary>
        ///拣货任务令号
        ///</summary>
        [FieldMapAttribute("PICKNO", typeof(string), 40, true)]
        public string Pickno;

    }
    #endregion

    #region Storloctrans--货位移动单信息
    /// <summary>
    /// TBLSTORLOCTRANS--货位移动单信息
    /// </summary>
    [Serializable, TableMap("TBLSTORLOCTRANS", "TRANSNO")]
    public class Storloctrans : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Storloctrans()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        ///<summary>
        ///原库位
        ///</summary>
        [FieldMapAttribute("FROMSTORAGECODE", typeof(string), 40, true)]
        public string Fromstoragecode;

        ///<summary>
        ///目标库位（货位移动建立单据时为空）
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string Storagecode;

        ///<summary>
        ///SAP单据号
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, false)]
        public string Invno;

        ///<summary>
        ///状态:Release:初始化；Pick:拣料；Close:完成
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///类型(Transfer:转储；Move:货位移动)
        ///</summary>
        [FieldMapAttribute("TRANSTYPE", typeof(string), 40, false)]
        public string Transtype;

        ///<summary>
        ///货位移动单号
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, false)]
        public string Transno;

    }
    #endregion
    #region Storloctransdetail--货位移动单明细信息
    /// <summary>
    /// TBLSTORLOCTRANSDETAIL--货位移动单明细信息
    /// </summary>
    [Serializable, TableMap("TBLSTORLOCTRANSDETAIL", "TRANSNO,MCODE")]
    public class Storloctransdetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Storloctransdetail()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int Ctime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int Cdate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string Cuser;

        ///<summary>
        ///需求数量
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string Mdesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string Dqmcode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string Mcode;

        ///<summary>
        ///华为物料号
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string Custmcode;

        ///<summary>
        ///状态:Release:初始化；Pick:拣料中；Close:完成
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///货位移动单号
        ///</summary>
        [FieldMapAttribute("TRANSNO", typeof(string), 40, false)]
        public string Transno;

    }
    #endregion

    #region Parameter 参数
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLSYSPARAM", "PARAMCODE,PARAMGROUPCODE")]
    public class Parameter : DomainObject
    {
        public Parameter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMCODE", typeof(string), 40, true)]
        public string ParameterCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMDESC", typeof(string), 100, false)]
        public string ParameterDescription;

        /// <summary>
        /// 1 -  使用中
        /// 0 -  正常
        /// 
        /// </summary>
        [FieldMapAttribute("ISACTIVE", typeof(string), 1, true)]
        public string IsActive;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 1  系统
        /// 0  用户
        /// </summary>
        [FieldMapAttribute("ISSYS", typeof(string), 1, true)]
        public string IsSystem;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMALIAS", typeof(string), 40, false)]
        public string ParameterAlias;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMVALUE", typeof(string), 100, false)]
        public string ParameterValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PARAMGROUPCODE", typeof(string), 40, true)]
        public string ParameterGroupCode;

        /// <summary>
        /// sequence 参数顺序
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string ParameterSequence;

        /// <summary>
        /// 父级参数代码
        /// </summary>
        [FieldMapAttribute("PARENTPARAM", typeof(string), 40, false)]
        public string ParentParameterCode;

    }
    #endregion

    #region Sapjoblog
    /// <summary>
    /// TBLSAPJOBLOG
    /// </summary>
    [Serializable, TableMap("TBLSAPJOBLOG", "ID")]
    public class Sapjoblog : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Sapjoblog()
        {
        }

        ///<summary>
        ///Eattribute3
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE3", typeof(string), 40, true)]
        public string Eattribute3;

        ///<summary>
        ///Eattribute2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string Eattribute2;

        ///<summary>
        ///Eattribute1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///Content
        ///</summary>
        [FieldMapAttribute("CONTENT", typeof(string), 2000, true)]
        public string Content;

        ///<summary>
        ///Location
        ///</summary>
        [FieldMapAttribute("LOCATION", typeof(string), 200, true)]
        public string Location;

        ///<summary>
        ///Jobname
        ///</summary>
        [FieldMapAttribute("JOBNAME", typeof(string), 20, true)]
        public string Jobname;

        ///<summary>
        ///Id
        ///</summary>
        [FieldMapAttribute("ID", typeof(int), 22, false)]
        public int Id;

    }
    #endregion

    #region Storagesap2mes
    /// <summary>
    /// TBLSTORAGESAP2MES
    /// </summary>
    [Serializable, TableMap("TBLSTORAGESAP2MES", "SERIAL")]
    public class Storagesap2mes : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Storagesap2mes()
        {
        }

        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 18, true)]
        public string Mcode;

        ///<summary>
        ///Dqmcode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, true)]
        public string Dqmcode;

        ///<summary>
        ///Messtoragecode
        ///</summary>
        [FieldMapAttribute("MESSTORAGECODE", typeof(string), 40, true)]
        public string Messtoragecode;

        ///<summary>
        ///Sapstoragecode
        ///</summary>
        [FieldMapAttribute("SAPSTORAGECODE", typeof(string), 40, true)]
        public string Sapstoragecode;

        ///<summary>
        ///Mesqty
        ///</summary>
        [FieldMapAttribute("MESQTY", typeof(decimal), 22, true)]
        public decimal Mesqty;

        ///<summary>
        ///Sapqty
        ///</summary>
        [FieldMapAttribute("SAPQTY", typeof(decimal), 22, true)]
        public decimal Sapqty;

        ///<summary>
        ///Disqty
        ///</summary>
        [FieldMapAttribute("DISQTY", typeof(decimal), 22, true)]
        public decimal Disqty;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, true)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, true)]
        public int MaintainTime;

    }
    #endregion

    #region Storagedetail-- 库存明细信息 add by jinger 2016-01-18
    /// <summary>
    /// TBLSTORAGEDETAIL-- 库存明细信息 
    /// </summary>
    [Serializable, TableMap("TBLSTORAGEDETAIL", "CARTONNO")]
    public class StorageDetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StorageDetail()
        {
        }

        ///<summary>
        ///维护时间
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///维护日期
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///维护人
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///创建时间
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///创建日期
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///创建人
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///检测返工申请人
        ///</summary>
        [FieldMapAttribute("REWORKAPPLYUSER", typeof(string), 40, true)]
        public string ReworkApplyUser;

        ///<summary>
        ///工厂
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///最后一次入库时间
        ///</summary>
        [FieldMapAttribute("LASTSTORAGEAGEDATE", typeof(int), 22, true)]
        public int LastStorageAgeDate;

        ///<summary>
        ///第一次入库时间
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(int), 22, true)]
        public int StorageAgeDate;

        ///<summary>
        ///生产日期
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int ProductionDate;

        ///<summary>
        ///鼎桥批次号
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///供应商批次号
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string SupplierLotNo;

        ///<summary>
        ///冻结数量
        ///</summary>
        [FieldMapAttribute("FREEZEQTY", typeof(int), 22, false)]
        public int FreezeQty;

        ///<summary>
        ///可用数量
        ///</summary>
        [FieldMapAttribute("AVAILABLEQTY", typeof(int), 22, false)]
        public int AvailableQty;

        ///<summary>
        ///库存数量
        ///</summary>
        [FieldMapAttribute("STORAGEQTY", typeof(int), 22, false)]
        public int StorageQty;

        ///<summary>
        ///单位
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///物料描述
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///鼎桥物料号
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DQMCode;

        ///<summary>
        ///箱号条码
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///货位条码
        ///</summary>
        [FieldMapAttribute("LOCATIONCODE", typeof(string), 40, false)]
        public string LocationCode;

        ///<summary>
        ///库位
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, false)]
        public string StorageCode;

        ///<summary>
        ///SAP物料号
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///有效期起算时间
        ///</summary>
        [FieldMapAttribute("ValidStartDate", typeof(int), 22, false)]
        public int ValidStartDate;


    }

    #endregion

    public class MaterialSum
    {
        public string MCode { get; set; }
        public decimal Qty { get; set; }
    }

    #region Log2Sap

    #region Rs2Sap
    /// <summary>
    /// TBLRSLOG
    /// </summary>
    [Serializable, TableMap("TBLRS2SAP", "SERIAL")]
    public class Rs2Sap : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Rs2Sap()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///DocumentDate
        ///</summary>
        [FieldMapAttribute("DOCUMENTDATE", typeof(int), 22, false)]
        public int DocumentDate;

        ///<summary>
        ///FacCode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, false)]
        public string FacCode;

        ///<summary>
        ///Inoutflag
        ///</summary>
        [FieldMapAttribute("INOUTFLAG", typeof(string), 40, false)]
        public string InOutFlag;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCode;

        ///<summary>
        ///Mestransno
        ///</summary>
        [FieldMapAttribute("MESTRANSNO", typeof(string), 40, false)]
        public string MesTransNO;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Rsline
        ///</summary>
        [FieldMapAttribute("RSLINE", typeof(int), 22, false)]
        public int RSLine;

        ///<summary>
        ///Rsno
        ///</summary>
        [FieldMapAttribute("RSNO", typeof(string), 40, false)]
        public string RSNO;

        ///<summary>
        ///StorageCode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("SAPRETURN", typeof(string), 40, true)]
        public string SapReturn;

        ///<summary>
        ///Sapmaterialinvoice
        ///</summary>
        [FieldMapAttribute("SAPMATERIALINVOICE", typeof(string), 40, true)]
        public string Sapmaterialinvoice;

        ///<summary>
        ///Message
        ///</summary>
        [FieldMapAttribute("MESSAGE", typeof(string), 40, true)]
        public string Message;

        ///<summary>
        ///tblrslog
        ///</summary>
        [FieldMapAttribute("IsPBack", typeof(string), 40, true)]
        public string IsPBack;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region Dn_in2Sap
    /// <summary>
    /// TBLDNLOG_IN
    /// </summary>
    [Serializable, TableMap("TBLDN_IN2SAP", "SERIAL")]
    public class Dn_in2Sap : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Dn_in2Sap()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///Batchno
        ///</summary>
        [FieldMapAttribute("BATCHNO", typeof(string), 50, false)]
        public string BatchNO;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Rsline
        ///</summary>
        [FieldMapAttribute("DNLINE", typeof(int), 22, false)]
        public int DNLine;

        ///<summary>
        ///Rsno
        ///</summary>
        [FieldMapAttribute("DNNO", typeof(string), 40, false)]
        public string DNno;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Sapmaterialinvoice
        ///</summary>
        [FieldMapAttribute("SAPMATERIALINVOICE", typeof(string), 40, true)]
        public string Sapmaterialinvoice;

        ///<summary>
        ///Ispback
        ///</summary>
        [FieldMapAttribute("ISPBACK", typeof(string), 40, true)]
        public string Ispback;

        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("SAPRETURN", typeof(string), 40, true)]
        public string Sapreturn;

        ///<summary>
        ///Message
        ///</summary>
        [FieldMapAttribute("MESSAGE", typeof(string), 200, true)]
        public string Message;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region Wwpo2Sap
    /// <summary>
    /// TBLWWPOLOG
    /// </summary>
    [Serializable, TableMap("TBLWWPO2SAP", "SERIAL")]
    public class Wwpo2Sap : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Wwpo2Sap()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///Pono
        ///</summary>
        [FieldMapAttribute("PONO", typeof(string), 40, false)]
        public string PONO;

        ///<summary>
        ///Poline
        ///</summary>
        [FieldMapAttribute("POLINE", typeof(int), 22, false)]
        public int POLine;

        ///<summary>
        ///VEndorCode
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, false)]
        public string VendorCode;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///Inoutflag
        ///</summary>
        [FieldMapAttribute("INOUTFLAG", typeof(string), 40, true)]
        public string InOutFlag;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, true)]
        public decimal Qty;

        ///<summary>
        ///FacCode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FacCode;

        ///<summary>
        ///StorageCode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///Mestransno
        ///</summary>
        [FieldMapAttribute("MESTRANSNO", typeof(string), 40, true)]
        public string MesTransNO;

        ///<summary>
        ///MestransDate
        ///</summary>
        [FieldMapAttribute("MESTRANSDATE", typeof(int), 22, true)]
        public int MesTransDate;

        ///<summary>
        ///Remark
        ///</summary>
        [FieldMapAttribute("REMARK", typeof(string), 40, true)]
        public string Remark;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Sapmaterialinvoice
        ///</summary>
        [FieldMapAttribute("SAPMATERIALINVOICE", typeof(string), 40, true)]
        public string Sapmaterialinvoice;

        ///<summary>
        ///Ispback
        ///</summary>
        [FieldMapAttribute("ISPBACK", typeof(string), 40, true)]
        public string Ispback;

        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("SAPRETURN", typeof(string), 40, true)]
        public string SapReturn;

        ///<summary>
        ///Message
        ///</summary>
        [FieldMapAttribute("MESSAGE", typeof(string), 200, true)]
        public string Message;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region Ub2Sap
    /// <summary>
    /// TBLUBLOG
    /// </summary>
    [Serializable, TableMap("TBLUB2SAP", "SERIAL")]
    public class Ub2Sap : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Ub2Sap()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///Ubno
        ///</summary>
        [FieldMapAttribute("UBNO", typeof(string), 40, false)]
        public string UBNO;

        ///<summary>
        ///Ubline
        ///</summary>
        [FieldMapAttribute("UBLINE", typeof(int), 22, false)]
        public int UBLine;

        ///<summary>
        ///ContactUser
        ///</summary>
        [FieldMapAttribute("CONTACTUSER", typeof(string), 40, false)]
        public string ContactUser;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///Inoutflag
        ///</summary>
        [FieldMapAttribute("INOUTFLAG", typeof(string), 40, true)]
        public string InOutFlag;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, true)]
        public decimal Qty;

        ///<summary>
        ///FacCode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FacCode;

        ///<summary>
        ///StorageCode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///Mestransno
        ///</summary>
        [FieldMapAttribute("MESTRANSNO", typeof(string), 40, true)]
        public string MesTransNO;

        ///<summary>
        ///DocumentDate
        ///</summary>
        [FieldMapAttribute("DOCUMENTDATE", typeof(int), 40, true)]
        public int DocumentDate;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Sapmaterialinvoice
        ///</summary>
        [FieldMapAttribute("SAPMATERIALINVOICE", typeof(string), 40, true)]
        public string Sapmaterialinvoice;

        ///<summary>
        ///Ispback
        ///</summary>
        [FieldMapAttribute("ISPBACK", typeof(string), 40, true)]
        public string Ispback;

        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("SAPRETURN", typeof(string), 40, true)]
        public string SapReturn;

        ///<summary>
        ///Message
        ///</summary>
        [FieldMapAttribute("MESSAGE", typeof(string), 200, true)]
        public string Message;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

    }
    #endregion

    #region DN2Sap
    [Serializable, TableMap("TBLDN2SAP", "SERIAL")]
    public class DN2Sap : DomainObject
    {
        public DN2Sap()
        {
        }


        [FieldMapAttribute("Serial", typeof(int), 22, false)]
        public int Serial;

        [FieldMapAttribute("DNLINE", typeof(int), 40, true)]
        public int DNLine;


        [FieldMapAttribute("DNNO", typeof(string), 40, true)]
        public string DNNO;

        [FieldMapAttribute("TRANSTYPE", typeof(string), 60, false)]
        public string TRANSTYPE;
        [FieldMapAttribute("Qty", typeof(decimal), 40, false)]
        public decimal Qty;

        [FieldMapAttribute("Unit", typeof(string), 40, false)]
        public string Unit;
        [FieldMapAttribute("RESULT", typeof(string), 40, false)]
        public string Result;
        [FieldMapAttribute("ISALL", typeof(string), 40, false)]
        public string IsAll;

        [FieldMapAttribute("DNBATCHNO", typeof(string), 40, false)]
        public string BatchNO;

        [FieldMapAttribute("MESSAGE", typeof(string), 60, false)]
        public string Message;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;
    }
    #endregion

    #region Po2Sap
    /// <summary>
    /// TBLPOLOG
    /// </summary>
    [Serializable, TableMap("TBLPO2SAP", "SERIAL")]
    public class Po2Sap : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Po2Sap()
        {
        }

        [FieldMapAttribute("Serial", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///Pono
        ///</summary>
        [FieldMapAttribute("PONO", typeof(string), 40, false)]
        public string PONO;

        ///<summary>
        ///Poline
        ///</summary>
        [FieldMapAttribute("POLINE", typeof(int), 40, false)]
        public int POLine;

        ///<summary>
        ///Faccode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FacCode;

        ///<summary>
        ///Serialno
        ///</summary>
        [FieldMapAttribute("SERIALNO", typeof(string), 40, false)]
        public string SerialNO;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        ///<summary>
        ///Sapmaterialinvoice
        ///</summary>
        [FieldMapAttribute("SAPMATERIALINVOICE", typeof(string), 40, true)]
        public string SAPMaterialInvoice;

        ///<summary>
        ///Operator
        ///</summary>
        [FieldMapAttribute("OPERATOR", typeof(string), 40, true)]
        public string Operator;

        ///<summary>
        ///Vendorinvoice
        ///</summary>
        [FieldMapAttribute("VENDORINVOICE", typeof(string), 40, true)]
        public string VendorInvoice;

        ///<summary>
        ///Storagecode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///Remark
        ///</summary>
        [FieldMapAttribute("REMARK", typeof(string), 40, true)]
        public string Remark;

        ///<summary>
        ///Invoicedate
        ///</summary>
        [FieldMapAttribute("INVOICEDATE", typeof(int), 22, false)]
        public int InvoiceDate;

        ///<summary>
        ///Ispback
        ///</summary>
        [FieldMapAttribute("ISPBACK", typeof(string), 40, true)]
        public string IsPBack;

        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("SAPRETURN", typeof(string), 40, true)]
        public string SapReturn;

        ///<summary>
        ///Saptimestamp
        ///</summary>
        [FieldMapAttribute("SapDateStamp", typeof(int), 22, false)]
        public int SapDateStamp;

        ///<summary>
        ///Saptimestamp
        ///</summary>
        [FieldMapAttribute("SAPTIMESTAMP", typeof(int), 22, false)]
        public int SapTimeStamp;


        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("Message", typeof(string), 200, true)]
        public string Message;

        [FieldMapAttribute("ZNUMBER", typeof(string), 40, true)]
        public string ZNUMBER;

        [FieldMap("STNO", typeof(String), 40, true)]
        public string STNO;
    }


    [Serializable, TableMap("TBLPOLOG", "")]
    public class PoLog : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public PoLog()
        {
        }

        [FieldMapAttribute("Serial", typeof(int), 22, false)]
        public int Serial;


        [FieldMapAttribute("ZNUMBER", typeof(string), 22, false)]
        public string ZNUMBER;

        ///<summary>
        ///Pono
        ///</summary>
        [FieldMapAttribute("PONO", typeof(string), 40, false)]
        public string PONO;

        ///<summary>
        ///Poline
        ///</summary>
        [FieldMapAttribute("POLINE", typeof(string), 40, false)]
        public string PoLine;

        ///<summary>
        ///Faccode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FacCode;

        ///<summary>
        ///Serialno
        ///</summary>
        [FieldMapAttribute("SERIALNO", typeof(string), 40, false)]
        public string SerialNO;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        ///<summary>
        ///Sapmaterialinvoice
        ///</summary>
        [FieldMapAttribute("SAPMATERIALINVOICE", typeof(string), 40, true)]
        public string SAPMaterialInvoice;

        ///<summary>
        ///Operator
        ///</summary>
        [FieldMapAttribute("OPERATOR", typeof(string), 40, true)]
        public string Operator;

        ///<summary>
        ///Vendorinvoice
        ///</summary>
        [FieldMapAttribute("VENDORINVOICE", typeof(string), 40, true)]
        public string VendorInvoice;

        ///<summary>
        ///Storagecode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///Remark
        ///</summary>
        [FieldMapAttribute("REMARK", typeof(string), 40, true)]
        public string Remark;

        ///<summary>
        ///Invoicedate
        ///</summary>
        [FieldMapAttribute("INVOICEDATE", typeof(int), 22, false)]
        public int InvoiceDate;

        ///<summary>
        ///Ispback
        ///</summary>
        [FieldMapAttribute("ISPBACK", typeof(string), 40, true)]
        public string IsPBack;

        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("SAPRETURN", typeof(string), 40, true)]
        public string SapReturn;

        ///<summary>
        ///Saptimestamp
        ///</summary>
        [FieldMapAttribute("SapDateStamp", typeof(int), 22, false)]
        public int SapDateStamp;

        ///<summary>
        ///Saptimestamp
        ///</summary>
        [FieldMapAttribute("SAPTIMESTAMP", typeof(int), 22, false)]
        public int SapTimeStamp;


        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("Message", typeof(string), 200, true)]
        public string Message;


    }
    #endregion

    #region StockScrap2Sap
    /// <summary>
    /// TBLSTOCKSCRAP2SAP
    /// </summary>
    [Serializable, TableMap("TBLSTOCKSCRAP2SAP", "SERIAL")]
    public class StockScrap2Sap : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public StockScrap2Sap()
        {
        }


        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int SERIAL;
        ///<summary>
        ///Lineno
        ///</summary>
        [FieldMapAttribute("LINENO", typeof(int), 22, false)]
        public int LineNO;

        ///<summary>
        ///Messcrapno
        ///</summary>
        [FieldMapAttribute("MESSCRAPNO", typeof(string), 40, true)]
        public string MESScrapNO;

        ///<summary>
        ///Mcode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, true)]
        public string MCode;

        ///<summary>
        ///Scrapcode
        ///</summary>
        [FieldMapAttribute("SCRAPCODE", typeof(string), 40, true)]
        public string ScrapCode;

        ///<summary>
        ///Faccode
        ///</summary>
        [FieldMapAttribute("FACCODE", typeof(string), 40, true)]
        public string FacCode;

        ///<summary>
        ///Storagecode
        ///</summary>
        [FieldMapAttribute("STORAGECODE", typeof(string), 40, true)]
        public string StorageCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, true)]
        public decimal Qty;


        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Cc
        ///</summary>
        [FieldMapAttribute("CC", typeof(string), 40, true)]
        public string CC;

        ///<summary>
        ///Remark
        ///</summary>
        [FieldMapAttribute("REMARK", typeof(string), 40, true)]
        public string Remark;

        ///<summary>
        ///Documentdate
        ///</summary>
        [FieldMapAttribute("DOCUMENTDATE", typeof(int), 22, false)]
        public int DocumentDate;

        ///<summary>
        ///Operator
        ///</summary>
        [FieldMapAttribute("OPERATOR", typeof(string), 40, true)]
        public string Operator;

        ///<summary>
        ///Message
        ///</summary>
        [FieldMapAttribute("MESSAGE", typeof(string), 40, true)]
        public string Message;

        ///<summary>
        ///Sapreturn
        ///</summary>
        [FieldMapAttribute("SAPRETURN", typeof(string), 40, true)]
        public string SapReturn;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

    }
    #endregion


    #endregion


    #region Asndetail   Amy add
    /// <summary>
    /// TBLASNDETAIL
    /// </summary>
    [Serializable, TableMap("TBLASNDETAIL", "STLINE,STNO")]
    public class Asndetail : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Asndetail()
        {
        }

        ///<summary>
        ///StorageageDate
        ///</summary>
        [FieldMapAttribute("STORAGEAGEDATE", typeof(string), 22, true)]
        public string StorageageDate;

        ///<summary>
        ///InitreceiveStatus
        ///</summary>
        [FieldMapAttribute("INITRECEIVESTATUS", typeof(string), 40, true)]
        public string InitreceiveStatus;

        ///<summary>
        ///InitreceiveDesc
        ///</summary>
        [FieldMapAttribute("INITRECEIVEDESC", typeof(string), 200, true)]
        public string InitreceiveDesc;

        ///<summary>
        ///VEndormCodeDesc
        ///</summary>
        [FieldMapAttribute("VENDORMCODEDESC", typeof(string), 100, true)]
        public string VEndormCodeDesc;

        ///<summary>
        ///VEndormCode
        ///</summary>
        [FieldMapAttribute("VENDORMCODE", typeof(string), 40, true)]
        public string VEndormCode;

        ///<summary>
        ///Stno
        ///</summary>
        [FieldMapAttribute("STNO", typeof(string), 40, false)]
        public string Stno;

        ///<summary>
        ///Stline
        ///</summary>
        [FieldMapAttribute("STLINE", typeof(string), 40, false)]
        public string Stline;

        ///<summary>
        ///Status
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///Cartonno
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, true)]
        public string Cartonno;

        ///<summary>
        ///Cartonbigseq
        ///</summary>
        [FieldMapAttribute("CARTONBIGSEQ", typeof(string), 40, true)]
        public string Cartonbigseq;

        ///<summary>
        ///Cartonseq
        ///</summary>
        [FieldMapAttribute("CARTONSEQ", typeof(string), 40, true)]
        public string Cartonseq;

        ///<summary>
        ///CustmCode
        ///</summary>
        [FieldMapAttribute("CUSTMCODE", typeof(string), 40, true)]
        public string CustmCode;

        ///<summary>
        ///MCode
        ///</summary>
        [FieldMapAttribute("MCODE", typeof(string), 40, false)]
        public string MCode;

        ///<summary>
        ///DqmCode
        ///</summary>
        [FieldMapAttribute("DQMCODE", typeof(string), 40, false)]
        public string DqmCode;

        ///<summary>
        ///MDesc
        ///</summary>
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MDesc;

        ///<summary>
        ///ReceivemCode
        ///</summary>
        [FieldMapAttribute("RECEIVEMCODE", typeof(string), 40, true)]
        public string ReceivemCode;

        ///<summary>
        ///Qty
        ///</summary>
        [FieldMapAttribute("QTY", typeof(int), 22, false)]
        public int Qty;

        ///<summary>
        ///ReceiveQty
        ///</summary>
        [FieldMapAttribute("RECEIVEQTY", typeof(int), 22, true)]
        public int ReceiveQty;

        ///<summary>
        ///ActQty
        ///</summary>
        [FieldMapAttribute("ACTQTY", typeof(int), 22, true)]
        public int ActQty;

        ///<summary>
        ///QcpassQty
        ///</summary>
        [FieldMapAttribute("QCPASSQTY", typeof(int), 22, true)]
        public int QcpassQty;

        ///<summary>
        ///Unit
        ///</summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, true)]
        public string Unit;

        ///<summary>
        ///Production_Date
        ///</summary>
        [FieldMapAttribute("PRODUCTION_DATE", typeof(int), 22, true)]
        public int Production_Date;

        ///<summary>
        ///Supplier_lotno
        ///</summary>
        [FieldMapAttribute("SUPPLIER_LOTNO", typeof(string), 40, true)]
        public string Supplier_lotno;

        ///<summary>
        ///Lotno
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string Lotno;

        ///<summary>
        ///Remark1
        ///</summary>
        [FieldMapAttribute("REMARK1", typeof(string), 200, true)]
        public string Remark1;

        ///<summary>
        ///CUser
        ///</summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CUser;

        ///<summary>
        ///CDate
        ///</summary>
        [FieldMapAttribute("CDATE", typeof(int), 22, false)]
        public int CDate;

        ///<summary>
        ///CTime
        ///</summary>
        [FieldMapAttribute("CTIME", typeof(int), 22, false)]
        public int CTime;

        ///<summary>
        ///MaintainDate
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int MaintainTime;

        ///<summary>
        ///MaintainUser
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        [FieldMapAttribute("LocationCode1", typeof(string), 22, false)]
        public string LocationCode1;

        [FieldMapAttribute("Remark2", typeof(string), 200, false)]
        public string Remark2;
    }
    #endregion

    public class AsndetailEX : Asndetail
    {
        public AsndetailEX()
        {
        }

        ///<summary>
        ///入库类型
        ///</summary>
        [FieldMapAttribute("MControlType", typeof(string), 40, true)]
        public string MControlType;


        //add by bela 20160222
        ///<summary>
        ///StType
        ///</summary>
        [FieldMapAttribute("STTYPE", typeof(string), 40, false)]
        public string StType;

        ///<summary>
        ///Invno
        ///</summary>
        [FieldMapAttribute("INVNO", typeof(string), 40, true)]
        public string Invno;


        [FieldMapAttribute("InitRejectQty", typeof(string), 40, true)]
        public int InitRejectQty;
    }

}
