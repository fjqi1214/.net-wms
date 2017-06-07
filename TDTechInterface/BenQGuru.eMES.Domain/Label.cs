using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for ATE
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jessie Lee
/// ** 日 期:		2006-5-22 10:24:05
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.MOModel
{

    #region Item2Label
    /// <summary>
    /// 产品对应的标签模板
    /// </summary>
    [Serializable, TableMap("tblItem2Label", "ItemCode,LabelCode")]
    public class Item2Label : DomainObject
    {
        public Item2Label()
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
        [FieldMapAttribute("ItemDesc", typeof(string), 100, true)]
        public string ItemDesc;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LabelCode", typeof(string), 40, true)]
        public string LabelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LabelDesc", typeof(string), 40, true)]
        public string LabelDesc;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MaintainTime;

    }
    #endregion

    #region LabelRCardPrintLog
    /// <summary>
    /// 产品序列打印Log
    /// </summary>
    [Serializable, TableMap("tblLabelRCardPrintLog", "RCard3")]
    public class LabelRCardPrintLog : DomainObject
    {
        public LabelRCardPrintLog()
        {
        }

        /// <summary>
        /// 工单代码
        /// </summary>
        [FieldMapAttribute("MoCode", typeof(string), 40, true)]
        public string MoCode;

        /// <summary>
        /// 工单序号
        /// </summary>
        [FieldMapAttribute("MoSeq", typeof(int), 10, false)]
        public decimal MoSeq;

        /// <summary>
        /// LotNo
        /// </summary>
        [FieldMapAttribute("MoLotNo", typeof(string), 40, false)]
        public string MoLotNo;

        /// <summary>
        /// 3#条码流水号
        /// </summary>
        [FieldMapAttribute("RCard3Seq", typeof(int), 10, false)]
        public int RCard3Seq;

        /// <summary>
        /// 2#条码流水号
        /// </summary>
        [FieldMapAttribute("RCard2Seq", typeof(int), 10, true)]
        public int RCard2Seq;

        /// <summary>
        /// 3#条码
        /// </summary>
        [FieldMapAttribute("RCard3", typeof(string), 40, true)]
        public string RCard3;

        /// <summary>
        /// 2#条码
        /// </summary>
        [FieldMapAttribute("RCard2", typeof(string), 40, true)]
        public string RCard2;

        /// <summary>
        /// ModelCode（不是产品别）
        /// </summary>
        [FieldMapAttribute("ModelCode", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUser", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MaintainTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PrintDate", typeof(int), 8, true)]
        public int PrintDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PrintTime", typeof(int), 6, true)]
        public int PrintTime;

        /// <summary>
        /// 年（2#条码序列号规则中的年）
        /// </summary>
        [FieldMapAttribute("Year", typeof(string), 40, true)]
        public string Year;

    }
    #endregion

    #region LabelRCardPrintLog_MinMaxSeq
    /// <summary>
    /// 产品序列打印Log
    /// </summary>
    [Serializable, TableMap("tblLabelRCardPrintLog", "RCard3")]
    public class LabelRCardPrintLog_MinMaxSeq : DomainObject
    {
        public LabelRCardPrintLog_MinMaxSeq()
        {
        }
                
        [FieldMapAttribute("MinSeq", typeof(int), 10, true)]
        public int MinSeq;

        [FieldMapAttribute("MaxSeq", typeof(int), 10, true)]
        public int MaxSeq;

    }
    #endregion

    #region LabelRCardSeq2
    /// <summary>
    /// 2#条码流水规则
    /// </summary>
    [Serializable, TableMap("tblLabelRCard2Seq", "Year,ModelCode")]
    public class LabelRCard2Seq : DomainObject
    {
        public LabelRCard2Seq()
        {
        }

        /// <summary>
        /// 一个字符，07年是R，以次类推
        /// </summary>
        [FieldMapAttribute("Year", typeof(string), 40, true)]
        public string Year;

        /// <summary>
        /// 产品表中的Model Code,不是产品别
        /// </summary>
        [FieldMapAttribute("ModelCode", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// 最后一个流水号
        /// </summary>
        [FieldMapAttribute("CurrSeq", typeof(int), 10, false)]
        public int CurrSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUser", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MaintainTime;

    }
    #endregion

    #region LabelRCard3Seq
    /// <summary>
    /// 3#条码流水规则
    /// </summary>
    [Serializable, TableMap("TblLabelRCardSeq3", "ModelCode,MoLotNo")]
    public class LabelRCard3Seq : DomainObject
    {
        public LabelRCard3Seq()
        {
        }

        /// <summary>
        /// 产品表中的Model Code
        /// </summary>
        [FieldMapAttribute("ModelCode", typeof(string), 40, true)]
        public string ModelCode;

        /// <summary>
        /// MO LOT NO
        /// </summary>
        [FieldMapAttribute("MoLotNo", typeof(string), 40, true)]
        public string MoLotNo;

        /// <summary>
        /// 当前流水号
        /// </summary>
        [FieldMapAttribute("CurrSeq", typeof(decimal), 10, false)]
        public decimal CurrSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUser", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MaintainTime;

    }
    #endregion

    #region LabelVAPrintLog
    /// <summary>
    /// VA序列号打印Log
    /// </summary>
    [Serializable, TableMap("tblLabelVAPrintLog", "Seq,MoCode")]
    public class LabelVAPrintLog : DomainObject
    {
        public LabelVAPrintLog()
        {
        }

        /// <summary>
        /// 流水号
        /// </summary>
        [FieldMapAttribute("Seq", typeof(int), 10, true)]
        public decimal Seq;

        /// <summary>
        /// 工单代码
        /// </summary>
        [FieldMapAttribute("MoCode", typeof(string), 40, true)]
        public string MoCode;

        /// <summary>
        /// 工单序号
        /// </summary>
        [FieldMapAttribute("MoSeq", typeof(int), 10, true)]
        public decimal MoSeq;

        /// <summary>
        /// Model Code
        /// </summary>
        [FieldMapAttribute("ItemModelCode", typeof(string), 40, true)]
        public string ItemModelCode;

        /// <summary>
        /// 年
        /// </summary>
        [FieldMapAttribute("Year", typeof(string), 40, true)]
        public string Year;

        /// <summary>
        /// 周
        /// </summary>
        [FieldMapAttribute("Week", typeof(string), 40, true)]
        public string Week;

        /// <summary>
        /// 第一次打印日期
        /// </summary>
        [FieldMapAttribute("PrintDate", typeof(int), 8, true)]
        public int PrintDate;

        /// <summary>
        /// 第一次打印时间
        /// </summary>
        [FieldMapAttribute("PrintTime", typeof(int), 6, true)]
        public int PrintTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MaintainUser", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MaintainDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MaintainTime", typeof(int), 6, true)]
        public int MaintainTime;

    }
    #endregion

    #region LabelVASeq
    /// <summary>
    /// VA序列号流水规则
    /// </summary>
    [Serializable, TableMap("tblLabelVASeq", "ItemModelCode,Year,Week")]
    public class LabelVASeq : DomainObject
    {
        public LabelVASeq()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemModelCode", typeof(string), 40, true)]
        public string ItemModelCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Year", typeof(string), 40, true)]
        public string Year;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Week", typeof(string), 40, true)]
        public string Week;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CurrSeq", typeof(int), 10, true)]
        public int CurrSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUser", typeof(string), 40, true)]
        public string MaintainUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDate", typeof(int), 8, true)]
        public int MaintainDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MTime", typeof(int), 6, true)]
        public int MaintainTime;

    }
    #endregion

    #region LabelDBSeq
    /// <summary>
    /// 子板流水号规则
    /// </summary>
    [Serializable, TableMap("tblLabelDBSeq", "MoLotNo,MoCode")]
    public class LabelDBSeq : DomainObject
    {
        public LabelDBSeq()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MoLotNo", typeof(string), 40, true)]
        public string MoLotNo;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MoCode", typeof(string), 40, true)]
        public string MoCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 子板工单序号
        /// </summary>
        [FieldMapAttribute("DBMoSeq", typeof(int), 40, true)]
        public int DBMoSeq;

        /// <summary>
        /// 此工单的3号条码流水号的开始
        /// </summary>
        [FieldMapAttribute("StartSeq", typeof(int), 10, true)]
        public int StartSeq;

        /// <summary>
        /// 此工间的3号流水号的结束
        /// </summary>
        [FieldMapAttribute("EndSeq", typeof(int), 10, true)]
        public int EndSeq;

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

    }
    #endregion

    #region SS2MaxCartonSeq
    /// <summary>
    /// 子板流水号规则
    /// </summary>
    [Serializable, TableMap("TBLSS2MAXCARTONSEQ", "SSCODE")]
    public class SS2MaxCartonSeq : DomainObject
    {
        public SS2MaxCartonSeq()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSCode", typeof(string), 40, false)]
        public string SSCode;

        /// <summary>
        /// 子板工单序号
        /// </summary>
        [FieldMapAttribute("MaxCartonSeq", typeof(int), 10, false)]
        public int MaxCartonSeq;
        
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

    }
    #endregion

    #region 设定可以加到配置文件中，用户以后可以自己修改
    public class LabelSufix
    {
        public static string RCard = Properties.Settings.Default.No3_CheckChar;
        public static string DB = Properties.Settings.Default.DB_CheckChar;
        public static string SMT = Properties.Settings.Default.SMT_CheckChar;
        public static string No5 = Properties.Settings.Default.No5_CheckChar;
    }

    public class LabelSeqLength
    {
        public static int No3 = 4;
        public static int No2 = 6;
        public static int VA = 4;
        public static int Carton = 5;
    }
    #endregion
}

