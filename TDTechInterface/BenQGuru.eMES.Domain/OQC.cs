using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for OQC
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2005-05-21 15:50:58
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.OQC
{

    #region Item2OQCCheckList
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLITEM2OQCCKLIST", "ITEMCODE,CKITEMCODE,ORGID")]
    public class Item2OQCCheckList : DomainObject
    {
        public Item2OQCCheckList()
        {
        }

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
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 40, true)]
        public string CheckItemCode;

        /// <summary>
        /// ORGID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region ItemOP2OQCCheckList
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLITEMOP2OQCCKLIST", "CKITEMCODE,ITEMCODE,OPCODE,ORGID")]
    public class ItemOP2OQCCheckList : DomainObject
    {
        public ItemOP2OQCCheckList()
        {
        }

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
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
        public decimal Sequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 40, true)]
        public string CheckItemCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// ORGID
        /// </summary>
        [FieldMapAttribute("ORGID", typeof(int), 8, false)]
        public int OrganizationID;
    }
    #endregion

    #region OQCCheckList
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCCKLIST", "CKITEMCODE")]
    public class OQCCheckList : DomainObject
    {
        public OQCCheckList()
        {
        }

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
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 40, true)]
        public string CheckItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKITEMDESC", typeof(string), 100, false)]
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 检验类型维护
        /// </summary>
        [FieldMapAttribute("UNIT", typeof(string), 40, false)]
        public string CheckIemUnit;

        /// <summary>
        /// 检验类型维护
        /// </summary>
        [FieldMapAttribute("SETVALUEMAX", typeof(string), 40, false)]
        public string CheckValueMax;
        /// <summary>
        /// 检验类型维护
        /// </summary>
        [FieldMapAttribute("SETVALUEMIN", typeof(string), 40, false)]
        public string CheckValueMin;

    }
    #endregion

    #region OQCCheckListQuery

    public class OQCCheckListQuery : OQCCheckList
    {
        /// <summary>
        /// 检验类型维护
        /// </summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 40, false)]
        public string CheckGroupCode;
    }
     #endregion

    #region OQCCheckGroup
    /// <summary>
    /// 检验类型
    /// </summary>
    [Serializable, TableMap("TBLOQCCKGROUP", "CKGROUP")]
    public class OQCCheckGroup : DomainObject
    {
        public OQCCheckGroup()
        {
        }

        /// <summary>
        /// 检验类型
        /// </summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 40, false)]
        public string CheckGroupCode;

        /// <summary>
        /// 最后维护用户
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 保留
        /// </summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;
    }
    #endregion

    #region OQCFuncTest
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCFUNCTEST", "ItemCode")]
    public class OQCFuncTest : DomainObject
    {
        public OQCFuncTest()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FuncTestGroupCount", typeof(decimal), 10, false)]
        public decimal FuncTestGroupCount;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MinDutyRatoMin", typeof(decimal), 15, true)]
        public decimal MinDutyRatoMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MinDutyRatoMax", typeof(decimal), 15, true)]
        public decimal MinDutyRatoMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BurstMdFreMin", typeof(decimal), 15, true)]
        public decimal BurstMdFreMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BurstMdFreMax", typeof(decimal), 15, true)]
        public decimal BurstMdFreMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricTestCount", typeof(decimal), 10, false)]
        public decimal ElectricTestCount;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

    }
    #endregion

    #region OQCFuncTestSpec
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCFUNCTESTSPEC", "ItemCode,GROUPSEQ")]
    public class OQCFuncTestSpec : DomainObject
    {
        public OQCFuncTestSpec()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FreMin", typeof(decimal), 15, true)]
        public decimal FreMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("GROUPSEQ", typeof(decimal), 10, true)]
        public decimal GroupSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FreMax", typeof(decimal), 15, true)]
        public decimal FreMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricMin", typeof(decimal), 15, true)]
        public decimal ElectricMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricMax", typeof(decimal), 15, true)]
        public decimal ElectricMax;

    }
    #endregion

    #region OQCFuncTestValue
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCFUNCTESTVALUE", "RCARD,RCARDSEQ")]
    public class OQCFuncTestValue : DomainObject
    {
        public OQCFuncTestValue()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FuncTestGroupCount", typeof(decimal), 10, false)]
        public decimal FuncTestGroupCount;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MinDutyRatoMin", typeof(decimal), 15, true)]
        public decimal MinDutyRatoMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MinDutyRatoMax", typeof(decimal), 15, true)]
        public decimal MinDutyRatoMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BurstMdFreMin", typeof(decimal), 15, true)]
        public decimal BurstMdFreMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BurstMdFreMax", typeof(decimal), 15, true)]
        public decimal BurstMdFreMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricTestCount", typeof(decimal), 10, false)]
        public decimal ElectricTestCount;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCode", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MinDutyRatoValue", typeof(decimal), 15, true)]
        public decimal MinDutyRatoValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BurstMdFreValue", typeof(decimal), 15, true)]
        public decimal BurstMdFreValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
        public string SegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string StepSequenceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PRODUCTSTATUS", typeof(string), 40, false)]
        public string ProductStatus;

    }
    #endregion

    #region OQCFuncTestValueDetail
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCFUNCTESTVALUEDTL", "RCARD,RCARDSEQ,GROUPSEQ")]
    public class OQCFuncTestValueDetail : DomainObject
    {
        public OQCFuncTestValueDetail()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricMin", typeof(decimal), 15, true)]
        public decimal ElectricMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricMax", typeof(decimal), 15, true)]
        public decimal ElectricMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCode", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FreValue", typeof(decimal), 15, true)]
        public decimal FreValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricValue", typeof(decimal), 15, true)]
        public decimal ElectricValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
        public string SegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string StepSequenceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FreMin", typeof(decimal), 15, true)]
        public decimal FreMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FreMax", typeof(decimal), 15, true)]
        public decimal FreMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("GROUPSEQ", typeof(decimal), 10, true)]
        public decimal GroupSequence;

    }
    #endregion

    #region OQCFuncTestValueEleDetail
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCFUNCTESTVALUEELEDTL", "RCARD,RCARDSEQ,GROUPSEQ,ElectricSequence")]
    public class OQCFuncTestValueEleDetail : DomainObject
    {
        public OQCFuncTestValueEleDetail()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricMin", typeof(decimal), 15, true)]
        public decimal ElectricMin;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricMax", typeof(decimal), 15, true)]
        public decimal ElectricMax;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCode", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricValue", typeof(decimal), 15, true)]
        public decimal ElectricValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResourceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
        public string SegmentCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string StepSequenceCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("GROUPSEQ", typeof(decimal), 10, true)]
        public decimal GroupSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ElectricSequence", typeof(decimal), 10, true)]
        public decimal ElectricSequence;

    }
    #endregion

    #region OQCLot
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLLOT", "LOTNO,LOTSEQ")]
    public class OQCLot : DomainObject
    {
        public OQCLot()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 处理人员
        /// </summary>
        [FieldMapAttribute("DUSER", typeof(string), 40, true)]
        public string DealUser;

        /// <summary>
        /// 处理时间
        /// </summary>
        [FieldMapAttribute("DTIME", typeof(int), 6, true)]
        public int DealTime;

        /// <summary>
        /// 处理日期
        /// </summary>
        [FieldMapAttribute("DDATE", typeof(int), 8, true)]
        public int DealDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OQCLotType", typeof(string), 40, true)]
        public string OQCLotType;

        [FieldMapAttribute("OrgID", typeof(int), 8, true)]
        public int OrganizationID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSIZE", typeof(decimal), 10, true)]
        public decimal LotSize;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSIZE", typeof(decimal), 10, true)]
        public decimal SampleSize;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACCSIZE", typeof(decimal), 10, true)]
        public decimal AcceptSize;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RJTSIZE", typeof(decimal), 10, true)]
        public decimal RejectSize;

        /// <summary>
        /// PASS
        /// REJECT
        /// REWORK
        /// </summary>
        [FieldMapAttribute("LOTSTATUS", typeof(string), 40, true)]
        public string LOTStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTTIMES", typeof(decimal), 10, true)]
        public decimal LOTTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACCSIZE1", typeof(decimal), 10, true)]
        public decimal AcceptSize1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACCSIZE2", typeof(decimal), 10, true)]
        public decimal AcceptSize2;

        [FieldMapAttribute("ACCSIZE3", typeof(decimal), 10, true)]
        public decimal AcceptSize3;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AQL1", typeof(decimal), 10, true)]
        public decimal AQL1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AQL", typeof(decimal), 10, true)]
        public decimal AQL;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AQL2", typeof(decimal), 10, true)]
        public decimal AQL2;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AQL3", typeof(decimal), 10, true)]
        public decimal AQL3;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RJTSIZE1", typeof(decimal), 10, true)]
        public decimal RejectSize1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RJTSIZE2", typeof(decimal), 10, true)]
        public decimal RejectSize2;

        [FieldMapAttribute("RJTSIZE3", typeof(decimal), 10, true)]
        public decimal RejectSize3;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 生产类型
        /// </summary>
        [FieldMapAttribute("ProductionType", typeof(string), 40, true)]
        public string ProductionType = "productiontype_mass";

        /// <summary>
        /// 原来的LotNo
        /// </summary>
        [FieldMapAttribute("OLDLOTNO", typeof(string), 40, true)]
        public string OldLotNo;

        [FieldMapAttribute("LOTCAPACITY", typeof(decimal), 10, true)]
        public decimal LotCapacity;

        [FieldMapAttribute("LOTFROZEN", typeof(string), 40, true)]
        public string LotFrozen;

        [FieldMapAttribute("MEMO", typeof(string), 100, true)]
        public string Memo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CUSER", typeof(string), 40, false)]
        public string CreateUser;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("CTIME", typeof(int), 6, false)]
        public int CreateTime;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("CDATE", typeof(int), 8, false)]
        public int CreateDate;

        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLSS", "SSCODE", "SSDESC")]
        public string SSCode;
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLMATERIAL", "MCODE", "MCHSHORTDESC")]
        public string ItemCode;
        [FieldMapAttribute("FROZENSTATUS", typeof(string), 40, true)]
        public string FrozenStatus;
        [FieldMapAttribute("FROZENREASON", typeof(string), 100, true)]
        public string FrozenReason;
        [FieldMapAttribute("FROZENDATE", typeof(int), 8, true)]
        public int FrozenDate;
        [FieldMapAttribute("FROZENTIME", typeof(int), 6, true)]
        public int FrozenTime;
        [FieldMapAttribute("FROZENBY", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string FrozenBy;
        [FieldMapAttribute("UNFROZENREASON", typeof(string), 100, true)]
        public string UnFrozenReason;
        [FieldMapAttribute("UNFROZENDATE", typeof(int), 8, true)]
        public int UnFrozenDate;
        [FieldMapAttribute("UNFROZENTIME", typeof(int), 6, true)]
        public int UnFrozenTime;
        [FieldMapAttribute("UNFROZENBY", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string UnFrozenBy;

        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;
    }
    #endregion

    #region OQCLot2Card
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLLOT2CARD", "RCARD,MOCODE,LOTNO,LOTSEQ")]
    public class OQCLot2Card : DomainObject
    {
        public OQCLot2Card()
        {
        }

        /// <summary>
        /// MNID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, false)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 成品料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 班制代码
        /// </summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        public string ShiftTypeCode;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 工序代码
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 生产途程代码
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// PASS
        /// REJECT
        /// REWORK
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CollectType", typeof(string), 20, false)]
        public string CollectType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, false)]
        public decimal MOSeq;

    }
    #endregion

    #region OQCLot2CardCheck
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLLOT2CARDCHECK", "RCARD,RCARDSEQ,MOCODE,LOTNO,LOTSEQ,CHECKSEQ")]
    public class OQCLot2CardCheck : DomainObject
    {
        public OQCLot2CardCheck()
        {
        }
        /// <summary>
        /// 重载ToSting()方法，返回对象的RunningCard值
        /// Laws Lu，2005/08/16，新增
        /// </summary>
        /// <returns>RunningCard</returns>
        public override string ToString()
        {
            if (Status == "GOOD")
            {
                return RunningCard.Trim();
            }
            else
            {
                return RunningCard.Trim() + "( NG )";
            }
        }

        /// <summary>
        /// MNID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 成品料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 时间段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TimePeriodCode;

        /// <summary>
        /// 班次代码
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        /// <summary>
        /// 班制代码
        /// </summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, true)]
        public string ShiftTypeCode;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 工序代码
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 生产途程代码
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// PASS
        /// REJECT
        /// REWORK
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, true)]
        public string Status;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 样本数据是否是来自数据连线，0 表示是在CS界面录入的样本，1代表来自数据连线
        /// </summary>
        [FieldMapAttribute("ISDATALINK", typeof(string), 1, true)]
        public string IsDataLink = "1";

        [FieldMapAttribute("CHECKSEQ", typeof(decimal), 10, false)]
        public decimal CheckSequence;

    }
    #endregion

    #region OQCLot2ErrorCode
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCLOT2ERRORCODE", "ErrorCodeGroup,ECODE,LOTNO,LOTSEQ")]
    public class OQCLot2ErrorCode : DomainObject
    {
        public OQCLot2ErrorCode()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ErrorCodeGroup", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TIMES", typeof(decimal), 10, true)]
        public decimal Times;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

    }
    #endregion

    #region OQCLot2CheckGroup
    [Serializable, TableMap("TBLOQCLOT2CKGROUP", "LOTNO,LOTSEQ,CKGROUP")]
    public class OQCLot2CheckGroup : DomainObject
    {
        public OQCLot2CheckGroup()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 40, true)]
        public string CheckGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CHECKEDCOUNT", typeof(int), 8, true)]
        public int CheckedCount;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NEEDCHECKCOUNT", typeof(int), 8, true)]
        public int NeedCheckCount;


    }
    #endregion

    #region OQCLotCard2ErrorCode
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCLOTCARD2ERRORCODE", "ErrorCodeGroup,ECODE,RCARD,RCARDSEQ,LOTNO,MOCODE,LOTSEQ,CHECKSEQ")]
    public class OQCLotCard2ErrorCode : DomainObject
    {
        public OQCLotCard2ErrorCode()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ErrorCodeGroup", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECODE", typeof(string), 40, true)]
        public string ErrorCode;

        /// <summary>
        /// MNID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        [FieldMapAttribute("CHECKSEQ", typeof(decimal), 10, false)]
        public decimal CheckSequence;

    }
    #endregion

    #region OQCLOTCardCheckList
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCCARDLOTCKLIST", "ITEMCODE,CKITEMCODE,RCARD,RCARDSEQ,LOTNO,MOCODE,LOTSEQ,CKGROUP,CHECKSEQ")]
    public class OQCLOTCardCheckList : DomainObject
    {
        public OQCLOTCardCheckList()
        {
        }

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
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// A
        /// B
        /// C
        /// </summary>
        [FieldMapAttribute("GRADE", typeof(string), 40, true)]
        public string Grade;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESULT", typeof(string), 40, true)]
        public string Result;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MEMO", typeof(string), 100, false)]
        public string MEMO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDLCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 料号[ItemCode]
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 40, true)]
        public string CheckItemCode;

        /// <summary>
        /// MNID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        [FieldMapAttribute("CKGROUP", typeof(string), 40, false)]
        public string CheckGroup;

        [FieldMapAttribute("CHECKSEQ", typeof(decimal), 10, false)]
        public decimal CheckSequence;
    }
    #endregion

    #region OQCLOTCheckList
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCLOTCKLIST", "LOTNO,LOTSEQ")]
    public class OQCLOTCheckList : DomainObject
    {
        public OQCLOTCheckList()
        {
        }

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
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AGRADETIMES", typeof(decimal), 10, true)]
        public decimal AGradeTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("BGGRADETIMES", typeof(decimal), 10, true)]
        public decimal BGradeTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CGRADETIMES", typeof(decimal), 10, true)]
        public decimal CGradeTimes;

        [FieldMapAttribute("ZGRADETIMES", typeof(decimal), 10, true)]
        public decimal ZGradeTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESULT", typeof(string), 40, true)]
        public string Result;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

    }
    #endregion

    #region OQCDIMENTION
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCDIM", "RCARD,RCARDSEQ,MOCODE,LOTNO,LOTSEQ")]
    public class OQCDimention : DomainObject
    {
        public OQCDimention()
        {
        }

        /// <summary>
        /// MNID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 成品料号
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 工序代码
        /// </summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        /// <summary>
        /// 工序组代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        /// <summary>
        /// 工段代码
        /// </summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmnetCode;

        /// <summary>
        /// 生产途程代码
        /// </summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        [FieldMapAttribute("TestResult", typeof(string), 40, true)]
        public string TestResult;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TestDate", typeof(int), 8, true)]
        public int TestDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TestTime", typeof(int), 6, true)]
        public int TestTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TestUser", typeof(string), 40, true)]
        public string TestUser;

    }
    #endregion

    #region OQCDIMENTIONVALUE
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCDIMVALUE", "LOTNO,LOTSEQ,RCARD,RCARDSEQ,MOCODE,PARAMNAME")]
    public class OQCDimentionValue : DomainObject
    {
        public OQCDimentionValue()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSequence;

        /// <summary>
        /// MNID
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, true)]
        public string RunningCard;

        /// <summary>
        /// 序号
        /// </summary>
        [FieldMapAttribute("RCARDSEQ", typeof(decimal), 10, true)]
        public decimal RunningCardSequence;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        [FieldMapAttribute("PARAMNAME", typeof(string), 40, false)]
        public string ParamName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MinValue", typeof(decimal), 15, true)]
        public decimal MinValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MaxValue", typeof(decimal), 15, true)]
        public decimal MaxValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ActValue", typeof(decimal), 15, true)]
        public decimal ActualValue;

    }
    #endregion

    #region OQCCheckGroup2List
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLOQCCKGROUP2LIST", "CKGROUP, CKITEMCODE")]
    public class OQCCheckGroup2List : DomainObject
    {
        public OQCCheckGroup2List()
        {
        }

        /// <summary>
        /// 检验类型维护
        /// </summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 40, false)]
        public string CheckGroupCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 40, true)]
        public string CheckItemCode;

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
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;


    }
    #endregion

    #region OQCPara
    [Serializable, TableMap("TBLOQCPARA", "TEMPLATENAME,NODENAME")]
    public class OQCPara : DomainObject
    {
        public OQCPara()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
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
        /// 
        /// </summary>
        [FieldMapAttribute("EAttribute1", typeof(string), 40, false)]
        public string EAttribute1;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TEMPLATENAME", typeof(string), 40, true)]
        public string TemplateName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ISTEMPLATE", typeof(string), 40, true)]
        public string ISTemplate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NODENAME", typeof(string), 40, true)]
        public string NodeName;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NODEVALUE", typeof(string), 40, true)]
        public string NodeValue;


    }
    #endregion

    #region Frozen

    /// <summary>
    ///	Frozen
    /// </summary>
    [Serializable, TableMap("TBLFROZEN", "RCARD,LOTNO,LOTSEQ,MOCODE,ITEMCODE,FROZENSEQ")]
    public class Frozen : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Frozen()
        {
        }

        ///<summary>
        ///RCard
        ///</summary>	
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RCard;

        ///<summary>
        ///LotNo
        ///</summary>	
        [FieldMapAttribute("LOTNO", typeof(string), 40, false)]
        public string LotNo;

        ///<summary>
        ///LotSequence
        ///</summary>	
        [FieldMapAttribute("LOTSEQ", typeof(int), 10, false)]
        public int LotSequence;

        ///<summary>
        ///MOCode
        ///</summary>	
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///ModelCode
        ///</summary>	
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLMATERIAL", "MCODE", "MCHSHORTDESC")]
        public string ItemCode;

        ///<summary>
        ///FROZENSeq
        ///</summary>	
        [FieldMapAttribute("FROZENSEQ", typeof(int), 10, false)]
        public int FrozenSequence;

        ///<summary>
        ///FROZENStatus
        ///</summary>	
        [FieldMapAttribute("FROZENSTATUS", typeof(string), 40, false)]
        public string FrozenStatus;

        ///<summary>
        ///FROZENREASON
        ///</summary>	
        [FieldMapAttribute("FROZENREASON", typeof(string), 100, true)]
        public string FrozenReason;

        ///<summary>
        ///FROZENDate
        ///</summary>	
        [FieldMapAttribute("FROZENDATE", typeof(int), 8, true)]
        public int FrozenDate;

        ///<summary>
        ///FROZENTime
        ///</summary>	
        [FieldMapAttribute("FROZENTIME", typeof(int), 6, true)]
        public int FrozenTime;

        ///<summary>
        ///FROZENBY
        ///</summary>	
        [FieldMapAttribute("FROZENBY", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string FrozenBy;

        ///<summary>
        ///UNFROZENREASON
        ///</summary>	
        [FieldMapAttribute("UNFROZENREASON", typeof(string), 100, true)]
        public string UnfrozenReason;

        ///<summary>
        ///UNFROZENDate
        ///</summary>	
        [FieldMapAttribute("UNFROZENDATE", typeof(int), 8, true)]
        public int UnfrozenDate;

        ///<summary>
        ///UNFROZENTime
        ///</summary>	
        [FieldMapAttribute("UNFROZENTIME", typeof(int), 6, true)]
        public int UnfrozenTime;

        ///<summary>
        ///UNFROZENBY
        ///</summary>	
        [FieldMapAttribute("UNFROZENBY", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string UnfrozenBy;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        ///<summary>
        ///EAttribute1
        ///</summary>	
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

    }

    #endregion

    #region   不良样本数 样本总数
    //tbllot2cardcheck
    //ngsamplecount,samplecount
    public class OQCCheckSample : DomainObject
    {
        public OQCCheckSample()
        {
        }
        /// <summary>
        /// 不良样本数
        /// </summary>
        [FieldMapAttribute("NGSAMPLECOUNT", typeof(int), 8, true)]
        public int NGSampleCount;

        /// <summary>
        /// 样本总数
        /// </summary>
        [FieldMapAttribute("SAMPLECOUNT", typeof(int), 8, true)]
        public int SampleCount;
    }
    #endregion


    #region  缺陷等级 缺陷个数
    public class OQCCheckGradeAndCount : OQCLOTCardCheckList
    {
        /// <summary>
        /// 缺陷个数
        /// </summary>
        [FieldMapAttribute("NGCOUNT", typeof(int), 8, true)]
        public int NGCount;
    }
    #endregion

    #region  检验类型 检验项目 数目
    public class OQCCheckListAndCount : OQCLOTCardCheckList
    {
        /// <summary>
        /// 数目
        /// </summary>
        [FieldMapAttribute("NGCOUNT", typeof(int), 8, true)]
        public int NGCount;
    }
    #endregion


    #region  ckgroup checkseq
    public class OQCLot2CardCheckAndCheckGroup : OQCLot2CardCheck
    {
        /// <summary>
        /// 检验类型
        /// </summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 40, true)]
        public string CheckGroup;
        /// <summary>
        /// 
        /// </summary>
        //[FieldMapAttribute("RCARD", typeof(string), 40, true)]
        //public string Rcard;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("RESULT", typeof(string), 40, true)]
        public string Result;
    }
    #endregion

    #region ecdesc
    public class OQCLotCard2ErrorCodeAndECDESC : OQCLotCard2ErrorCode
    {

        /// <summary>
        /// 
        /// </summary>
        //[FieldMapAttribute("ECODE", typeof(string), 40, true)]
        //public string ECODE;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ECDESC", typeof(string), 100, true)]
        public string ECDESC;

    }
    #endregion

    #region OQCCardCartonAndPallet

    public class OQCCardCartonAndPallet : OQCLot2Card
    {
        [FieldMapAttribute("CARTONCODE", typeof(string), 40, true)]
        public string CartonCode;

        [FieldMapAttribute("PALLETCODE", typeof(string), 40, true)]
        public string PalletCode;

        [FieldMapAttribute("MDATE2", typeof(int), 8, true)]
        public int PalletDate;

        [FieldMapAttribute("MTIME2", typeof(int), 6, true)]
        public int PalletTime;

        [FieldMapAttribute("FLAG", typeof(string), 2, true)]
        public string IsSample;
    }

    #endregion

    #region OQCLOTAndMaterial

    public class OQCLOTAndMaterial : OQCLot
    {
        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MmodelCode;
        [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
        public string MoCdoe;
        [FieldMapAttribute("MDESC", typeof(string), 200, true)]
        public string MaterialDesc;
    }

    #endregion

    #region FrozenAndMmodelCode

    public class FrozenAndMmodelCode : Frozen
    {
        [FieldMapAttribute("mmodelcode", typeof(string), 40, true)]
        public string MmodelCode;
    }

    #endregion

    #region OQCLot2CardCheckQuery

    public class OQCLot2CardCheckQuery : OQCLot2CardCheck
    {
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigStepSequenceCode;

        [FieldMapAttribute("mmodelcode", typeof(string), 40, true)]
        public string MmodelCode;

        [FieldMapAttribute("CREWCODE", typeof(string), 40, true)]
        public string CrewCode;

        [FieldMapAttribute("ProductionType", typeof(string), 40, true)]
        public string ProductionType;

        [FieldMapAttribute("OQCLotType", typeof(string), 40, true)]
        public string OQCLotType;

        [FieldMapAttribute("REWORKCODE", typeof(string), 40, true)]
        public string ReworkCode;

        [FieldMapAttribute("OQCUSER", typeof(string), 40, true)]
        public string OQCUser;
    }

    #endregion

    #region OQCLotCountForAlert
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("", "ITEMCODE,MMODELCODE,BIGSSCODE")]
    public class OQCLotCountForAlert : DomainObject
    {
        public OQCLotCountForAlert()
        {
        }

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("MMODELCODE", typeof(string), 40, true)]
        public string MaterialModelCode;

        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BigSSCode;

        [FieldMapAttribute("OQCLOTCOUNT", typeof(int), 6, true)]
        public int OQCLotCount;
    }
    #endregion

    #region  Lot2Carton
    /// <summary>
    /// </summary>
    [Serializable, TableMap("TBLLOT2CARTON", "SERIAL")]
    public class Lot2Carton : DomainObject
    {
        public Lot2Carton()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OQCLOT", typeof(string), 40, false)]
        public string OQCLot;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ADDUSER", typeof(string), 40, true)]
        public string AddUser;


        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ADDDATE", typeof(int), 8, true)]
        public int AddDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ADDTIME", typeof(int), 6, true)]
        public int AddTime;

    }
    #endregion

    #region LOT2CARTONLOG
    /// <summary>
    /// TBLLOT2CARTONLOG
    /// </summary>
    [Serializable, TableMap("TBLLOT2CARTONLOG", "SERIAL")]
    public class Lot2CartonLog : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Lot2CartonLog()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///OQCLOT
        ///</summary>
        [FieldMapAttribute("OQCLOT", typeof(string), 40, false)]
        public string OQCLot;

        ///<summary>
        ///CARTONNO
        ///</summary>
        [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
        public string CartonNo;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///ADDUSER
        ///</summary>
        [FieldMapAttribute("ADDUSER", typeof(string), 40, true)]
        public string AddUser;

        ///<summary>
        ///ADDDATE
        ///</summary>
        [FieldMapAttribute("ADDDATE", typeof(int), 22, true)]
        public int AddDate;

        ///<summary>
        ///ADDTIME
        ///</summary>
        [FieldMapAttribute("ADDTIME", typeof(int), 22, true)]
        public int AddTime;

        ///<summary>
        ///REMOVEUSER
        ///</summary>
        [FieldMapAttribute("REMOVEUSER", typeof(string), 40, true)]
        public string RemoveUser;

        ///<summary>
        ///REMOVDATE
        ///</summary>
        [FieldMapAttribute("REMOVEDATE", typeof(int), 22, true)]
        public int RemoveDate;

        ///<summary>
        ///REMOVTIME
        ///</summary>
        [FieldMapAttribute("REMOVETIME", typeof(int), 22, true)]
        public int RemoveTime;

    }
    #endregion

    #region TestData
    /// <summary>
    /// TBLTESTDATA
    /// </summary>
    [Serializable, TableMap("TBLTESTDATA", "SERIAL")]
    public class TestData : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public TestData()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, false)]
        public int Serial;

        ///<summary>
        ///RCARD
        ///</summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RCard;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///SHIFTDAY
        ///</summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 22, false)]
        public int ShiftDay;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///TBLMESENTITYLIST_SERIAL
        ///</summary>
        [FieldMapAttribute("TBLMESENTITYLIST_SERIAL", typeof(int), 22, false)]
        public int Tblmesentitylist_Serial;

        ///<summary>
        ///DEVICENO
        ///</summary>
        [FieldMapAttribute("DEVICENO", typeof(string), 40, true)]
        public string DeviceNO;

        ///<summary>
        ///CKGROUP
        ///</summary>
        [FieldMapAttribute("CKGROUP", typeof(string), 400, true)]
        public string CheckGroup;

        ///<summary>
        ///CKITEMCODE
        ///</summary>
        [FieldMapAttribute("CKITEMCODE", typeof(string), 400, false)]
        public string CheckItemCode;

        ///<summary>
        ///PARAM
        ///</summary>
        [FieldMapAttribute("PARAM", typeof(string), 200, true)]
        public string Param;

        ///<summary>
        ///USL
        ///</summary>
        [FieldMapAttribute("USL", typeof(decimal), 22, true)]
        public decimal USL;

        ///<summary>
        ///LSL
        ///</summary>
        [FieldMapAttribute("LSL", typeof(decimal), 22, true)]
        public decimal LSL;

        ///<summary>
        ///TESTINGVALUE
        ///</summary>
        [FieldMapAttribute("TESTINGVALUE", typeof(string), 200, true)]
        public string TestingValue;

        ///<summary>
        ///TESTINGRESULT
        ///</summary>
        [FieldMapAttribute("TESTINGRESULT", typeof(string), 40, true)]
        public string TestingResult;

        ///<summary>
        ///TESTINGDATE
        ///</summary>
        [FieldMapAttribute("TESTINGDATE", typeof(int), 22, false)]
        public int TestingDate;

        ///<summary>
        ///TESTINGTIME
        ///</summary>
        [FieldMapAttribute("TESTINGTIME", typeof(int), 22, false)]
        public int TestingTime;

        /// <summary>
        /// 最后维护用户[LastMaintainUser]
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 最后维护时间[LastMaintainTime]
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 最后维护日期[LastMaintainDate]
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

    }
    #endregion


    #region AQL
    /// <summary>
    /// TBLAQL
    /// </summary>
    [Serializable, TableMap("TBLAQL", "AQLSEQ,AQLLEVEL")]
    public class AQL : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public AQL()
        {
        }

        ///<summary>
        ///AQLSEQ
        ///</summary>
        [FieldMapAttribute("AQLSEQ", typeof(int), 22, false)]
        public int AQLSeq;

        ///<summary>
        ///AQLLEVEL
        ///</summary>
        [FieldMapAttribute("AQLLEVEL", typeof(string), 40, false)]
        public string AqlLevel;

        ///<summary>
        ///LOTSIZEMIN
        ///</summary>
        [FieldMapAttribute("LOTSIZEMIN", typeof(int), 22, false)]
        public int LotSizeMin;

        ///<summary>
        ///LOTSIZEMAX
        ///</summary>
        [FieldMapAttribute("LOTSIZEMAX", typeof(int), 22, false)]
        public int LotSizeMax;

        ///<summary>
        ///SAMPLESIZE
        ///</summary>
        [FieldMapAttribute("SAMPLESIZE", typeof(int), 22, false)]
        public int SampleSize;

        ///<summary>
        ///REJECTSIZE
        ///</summary>
        [FieldMapAttribute("REJECTSIZE", typeof(int), 22, false)]
        public int RejectSize;

        //add by jinger 20160219
        ///<summary>
        ///检验标准描述
        ///</summary>
        [FieldMapAttribute("AQLLEVELDESC", typeof(string), 200, false)]
        public string AqlLevelDesc;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

    }
    #endregion


}

