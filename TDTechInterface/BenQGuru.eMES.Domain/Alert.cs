using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.Alert
{
    #region 这些实体类在Hisense MES中已经过期作废，不再使用

    #region Alert
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLALERT", "ALERTID")]
    public class Alert : DomainObject
    {
        public Alert()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTID", typeof(decimal), 10, true)]
        public decimal AlertID;

        [FieldMapAttribute("BILLID", typeof(int), 9, true)]
        public int BillId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTTYPE", typeof(string), 40, true)]
        public string AlertType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTITEM", typeof(string), 40, false)]
        public string AlertItem;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTMSG", typeof(string), 1000, false)]
        public string AlertMsg;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTDATE", typeof(int), 8, true)]
        public int AlertDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTTIME", typeof(int), 6, true)]
        public int AlertTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTVALUE", typeof(decimal), 15, true)]
        public decimal AlertValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SENDUSER", typeof(string), 40, true)]
        public string SendUser;

        /// <summary>
        /// Important
        /// Primary
        /// Severity
        /// </summary>
        [FieldMapAttribute("ALERTLEVEL", typeof(string), 40, false)]
        public string AlertLevel;

        /// <summary>
        /// Unhandled
        /// Closed
        /// Observing
        /// Handling
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTSTATUS", typeof(string), 40, false)]
        public string AlertStatus;

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
        /// Y
        /// N
        /// </summary>
        [FieldMapAttribute("MAILNOTIFY", typeof(string), 1, true)]
        public string MailNotify;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VALIDDATE", typeof(int), 8, true)]
        public int ValidDate;

        [FieldMapAttribute("ALERTDESC", typeof(string), 1000, false)]
        public string Description;

        //产品代码
        [FieldMapAttribute("PRODUCTCODE", typeof(string), 40, false)]
        public string ProductCode;

        //产线代码
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        [FieldMapAttribute("SHIFTTIME", typeof(int), 8, false)]
        public int ShiftTime;

    }
    #endregion

    #region AlertBill
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLALERTBILL", "BILLID")]
    public class AlertBill : DomainObject
    {
        public AlertBill()
        {
        }

        [FieldMapAttribute("BILLID", typeof(int), 9, true)]
        public int BillId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("productcode", typeof(string), 40, true)]
        public string ProductCode = string.Empty;

        /// <summary>
        /// NG
        /// PPM
        /// DirectPass
        /// CPK
        /// First
        /// Manual
        /// </summary>
        [FieldMapAttribute("ALERTTYPE", typeof(string), 40, true)]
        public string AlertType;

        /// <summary>
        /// Item
        /// Model
        /// SS
        /// Segment
        /// </summary>
        [FieldMapAttribute("ALERTITEM", typeof(string), 40, true)]
        public string AlertItem;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("STARTNUM", typeof(decimal), 10, true)]
        public decimal StartNum;

        /// <summary>
        /// BW {介于}
        /// LE  {小于等于}
        /// GE {大于等于}
        /// 
        /// </summary>
        [FieldMapAttribute("OP", typeof(string), 40, true)]
        public string Operator;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("LOWVALUE", typeof(decimal), 15, true)]
        public decimal LowValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("UPVALUE", typeof(decimal), 15, true)]
        public decimal UpValue;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("VALIDDATE", typeof(int), 8, true)]
        public int ValidDate;

        /// <summary>
        /// Y
        /// N
        /// </summary>
        [FieldMapAttribute("MAILNOTIFY", typeof(string), 1, true)]
        public string MailNotify;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTMSG", typeof(string), 1000, false)]
        public string AlertMsg;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTDESC", typeof(string), 1000, false)]
        public string Description;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

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

    }

    /// <summary>
    /// 资源不良预警设定
    /// </summary>
    [Serializable, TableMap("TBLALERTRESBILL", "BILLID")]
    public class AlertResBill : AlertBill
    {
        public AlertResBill()
        {
        }

        /// <summary>
        /// 预警的资源
        /// </summary>
        [FieldMapAttribute("ALERTRES", typeof(string), 40, true)]
        public string ResourceCode;

        /// <summary>
        /// 不良代码组和不良代码
        /// </summary>
        [FieldMapAttribute("ALERTERECG2EC", typeof(string), 100, true)]
        public string ErrorGroup2Code;
    }
    #endregion

    #region AlertHandleLog
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLALERTHANDLELOG", "HANDLESEQ,ALERTID")]
    public class AlertHandleLog : DomainObject
    {
        public AlertHandleLog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLESEQ", typeof(decimal), 10, true)]
        public decimal HandleSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLEMSG", typeof(string), 1000, false)]
        public string HandleMsg;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLEUSER", typeof(string), 40, true)]
        public string HandleUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USEREMAIL", typeof(string), 40, true)]
        public string UserEmail;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTLEVEL", typeof(string), 40, true)]
        public string AlertLevel;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTSTATUS", typeof(string), 40, true)]
        public string AlertStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLEDATE", typeof(int), 8, true)]
        public int HandleDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLETIME", typeof(int), 6, true)]
        public int HandleTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTID", typeof(decimal), 10, true)]
        public decimal AlertID;

    }
    #endregion

    #region 实现公告栏主界面和子界面一起导出
    public class Alert2Handle
        : BenQGuru.eMES.Domain.Alert.Alert
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLESEQ", typeof(decimal), 10, true)]
        public decimal HandleSeq;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLEMSG", typeof(string), 1000, false)]
        public string HandleMsg;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLEUSER", typeof(string), 40, true)]
        public string HandleUser;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USEREMAIL", typeof(string), 40, true)]
        public string UserEmail;

        /// <summary>
        /// 处理记录中的预警级别
        /// </summary>
        [FieldMapAttribute("HandlAlertLevel", typeof(string), 40, true)]
        public string HandlAlertLevel;

        /// <summary>
        /// 处理记录中的状态
        /// </summary>
        [FieldMapAttribute("HandlAlertStatus", typeof(string), 40, true)]
        public string HandlAlertStatus;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLEDATE", typeof(int), 8, true)]
        public int HandleDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("HANDLETIME", typeof(int), 6, true)]
        public int HandleTime;

        /// <summary>
        /// 
        /// </summary>
        //[FieldMapAttribute("ALERTID", typeof(decimal), 10, true)]
        //public decimal  AlertID;
    }
    #endregion

    #region AlertMailLog
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLALERTMAILLOG", "ALERTID,Seq")]
    public class AlertMailLog : DomainObject
    {
        public AlertMailLog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USERCODE", typeof(string), 40, true)]
        public string UserCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USERMAIL", typeof(string), 40, true)]
        public string UserMail;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SENDDATE", typeof(int), 8, true)]
        public int SendDate;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SENDTIME", typeof(int), 6, true)]
        public int SendTime;

        /// <summary>
        /// GUID
        /// </summary>
        [FieldMapAttribute("ALERTID", typeof(decimal), 10, true)]
        public decimal AlertID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Seq", typeof(decimal), 10, true)]
        public decimal Seq;

    }
    #endregion

    #region AlertManualNotifier
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLALERTMANUALNOTIFIER", "ALERTID,USERCODE")]
    public class AlertManualNotifier : DomainObject
    {
        public AlertManualNotifier()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USERMAIL", typeof(string), 40, false)]
        public string UserMail;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USERCODE", typeof(string), 40, true)]
        public string UserCode;

        /// <summary>
        /// GUID
        /// </summary>
        [FieldMapAttribute("ALERTID", typeof(int), 10, true)]
        public int AlertID;

    }
    #endregion

    #region AlertNotifier
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLALERTNOTIFIER", "USERCODE,BILLID")]
    public class AlertNotifier : DomainObject
    {
        public AlertNotifier()
        {
        }

        [FieldMapAttribute("BILLID", typeof(int), 9, true)]
        public int BillId;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("USERCODE", typeof(string), 40, true)]
        public string UserCode;

        [FieldMapAttribute("EMAIL", typeof(string), 40, true)]
        public string EMail;
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTTYPE", typeof(string), 40, true)]
        public string AlertType;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALERTITEM", typeof(string), 40, true)]
        public string AlertItem;

    }
    #endregion

    #region AlertCheckObjectNG
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("", "")]
    public class AlertCheckObjectNG : DomainObject
    {
        public AlertCheckObjectNG()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("SSCode", typeof(string), 40, true)]
        public string SSCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NGTimes", typeof(string), 40, true)]
        public string NGTimes;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OUTPUTQTY", typeof(string), 40, true)]
        public string OutputQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("NotYieldPercent", typeof(string), 40, true)]
        public string NotYieldPercent;

    }
    #endregion

    #region AlertCheckObjectPPM
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("", "")]
    public class AlertCheckObjectPPM : DomainObject
    {
        public AlertCheckObjectPPM()
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
        [FieldMapAttribute("ErrorLocationCount", typeof(decimal), 40, true)]
        public decimal ErrorLocationCount;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TotalLocation", typeof(decimal), 40, true)]
        public decimal TotalLocation;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PPM", typeof(decimal), 40, true)]
        public decimal PPM;

    }
    #endregion

    #region AlertCheckObjectCPK
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("", "")]
    public class AlertCheckObjectCPK : DomainObject
    {
        public AlertCheckObjectCPK()
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
        [FieldMapAttribute("TotalRecord", typeof(decimal), 40, true)]
        public decimal TotalRecord;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("TestItem", typeof(string), 40, true)]
        public string TestItem;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("CPK", typeof(decimal), 40, true)]
        public decimal CPK;

    }
    #endregion

    #region AlertCheckObjectPPM
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("", "")]
    public class AlertCheckObjectDirectPass : DomainObject
    {
        public AlertCheckObjectDirectPass()
        {
        }

        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string SSCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ALLGOODQTY", typeof(decimal), 40, true)]
        public decimal AllGoodQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OUTPUTQTY", typeof(decimal), 40, true)]
        public decimal OutputQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("AllGoodYield", typeof(decimal), 40, true)]
        public decimal AllGoodYield;

    }
    #endregion

    #region AlertCheckObjectResouserNG 资源不良数

    //tblrpthisopqty
    //itemcode,rescode,ecg2ec,shiftcode,tpcode,shiftday,COUNT (ngtimes)

    /// <summary>
    /// 资源不良数预警对象
    /// </summary>
    public class AlertCheckObjectResouserNG : DomainObject
    {
        public AlertCheckObjectResouserNG()
        {
        }

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string Rescode;

        /// <summary>
        /// 不良代码组：不良代码
        /// </summary>
        [FieldMapAttribute("ECG2EC", typeof(string), 100, true)]
        public string Ecg2ec;

        /// <summary>
        /// 工作天 
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(string), 40, true)]
        public string Shiftday;

        /// <summary>
        /// 班次
        /// </summary>
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string Shiftcode;

        /// <summary>
        /// 时段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string Tpcode;

        /// <summary>
        /// 不良次数
        /// </summary>
        [FieldMapAttribute("NGTIMES", typeof(string), 40, true)]
        public string NGTimes;

        /// <summary>
        /// 不良代码组:不良代码List
        /// </summary>
        public string[] ListEcg2Ec
        {
            get
            {
                if (_listecg2ec != null)
                {
                    return _listecg2ec;
                }
                else
                {
                    if (this.Ecg2ec != null && this.Ecg2ec != string.Empty)
                    {
                        char splitchar = ';';
                        this._listecg2ec = this.Ecg2ec.Split(splitchar); //分隔符为半角分号
                        return _listecg2ec;
                    }
                }
                return new string[] { };
            }
        }
        private string[] _listecg2ec;

    }
    #endregion

    #region AlertSample
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLALERTSAMPLE", "ID")]
    public class AlertSample : DomainObject
    {
        public AlertSample()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ID", typeof(string), 40, true)]
        public string ID;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SAMPLEDESC", typeof(string), 1000, false)]
        public string SampleDesc;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

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

    }
    #endregion

    #region FirstOnline
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLFIRSTONLINE", "SSCODE,MDATE,ITEMCODE,SHIFTCODE,ShiftTime")]
    public class FirstOnline : DomainObject
    {
        public FirstOnline()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string SSCode;

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
        [FieldMapAttribute("ONLINETIME", typeof(int), 6, true)]
        public int OnLineTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("OFFLINETIME", typeof(int), 6, true)]
        public int OffLineTime;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        public string MaintianUser;

        /// <summary>
        /// ON {上线}
        /// OFF {下线}
        /// </summary>
        [FieldMapAttribute("ActionType", typeof(string), 40, true)]
        public string ActionType;

        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("MODELCODE", typeof(string), 40, true)]
        public string ModelCode;

        /// </summary>
        [FieldMapAttribute("OFFRCARD", typeof(string), 40, true)]
        public string OfflineRuningCard;

        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, true)]
        public string ShiftCode;

        //上班时间
        [FieldMapAttribute("ShiftTime", typeof(int), 6, true)]
        public int ShiftTime;

        //班次是不是跨天
        [FieldMapAttribute("ISOVERDAY ", typeof(string), 40, true)]
        public string IsOverDay;

        //末件类型
        [FieldMapAttribute("LASTTYPE ", typeof(string), 40, true)]
        public string LastType;

        //末件上线RCard
        [FieldMapAttribute("LASTONRCARD ", typeof(string), 40, true)]
        public string LastOnRCard;

        //末件下线RCard
        [FieldMapAttribute("LASTOFFRCARD ", typeof(string), 40, true)]
        public string LastOffRCard;

        //末件上线时间
        [FieldMapAttribute("LASTONTIME", typeof(int), 6, true)]
        public int LastOnTime;

        //末件下线时间
        [FieldMapAttribute("LASTOFFTIME", typeof(int), 6, true)]
        public int LastOffTime;

        //下班时间
        [FieldMapAttribute("ENDTIME", typeof(int), 6, true)]
        public int EndTime;


    }
    #endregion

    #endregion

    #region AlertItem

    /// <summary>
    ///	AlertItem
    /// </summary>
    [Serializable, TableMap("TBLALERTITEM", "ITEMSEQUENCE")]
    public class AlertItem : DomainObject
    {
        public AlertItem()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///Description
        ///</summary>	
        [FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
        public string Description;

        ///<summary>
        ///AlertType
        ///</summary>	
        [FieldMapAttribute("ALERTTYPE", typeof(string), 40, false)]
        public string AlertType;

        ///<summary>
        ///MailSubject
        ///</summary>	
        [FieldMapAttribute("MAILSUBJECT", typeof(string), 150, false)]
        public string MailSubject;

        ///<summary>
        ///MailContent
        ///</summary>	
        [FieldMapAttribute("MAILCONTENT", typeof(string), 2000, false)]
        public string MailContent;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region AlertError

    /// <summary>
    ///	AlertError
    /// </summary>
    [Serializable, TableMap("TBLALERTERROR", "SUBITEMSEQUENCE")]
    public class AlertError : DomainObject
    {
        public AlertError()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///ItemType
        ///</summary>	
        [FieldMapAttribute("ITEMTYPE", typeof(string), 40, false)]
        public string ItemType;

        ///<summary>
        ///ErrorCode
        ///</summary>	
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ErrorCode;

        ///<summary>
        ///TimeDimension
        ///</summary>	
        [FieldMapAttribute("TIMEDIMENSION", typeof(string), 40, false)]
        public string TimeDimension;

        ///<summary>
        ///LineDivision
        ///</summary>	
        [FieldMapAttribute("LINEDIVISION", typeof(string), 1, false)]
        public string LineDivision;

        ///<summary>
        ///AlertValue
        ///</summary>	
        [FieldMapAttribute("ALERTVALUE", typeof(int), 8, false)]
        public int AlertValue;

        ///<summary>
        ///GenerateNotice
        ///</summary>	
        [FieldMapAttribute("GENERATENOTICE", typeof(string), 1, false)]
        public string GenerateNotice;

        ///<summary>
        ///SendMail
        ///</summary>	
        [FieldMapAttribute("SENDMAIL", typeof(string), 1, false)]
        public string SendMail;

        ///<summary>
        ///LinePause
        ///</summary>	
        [FieldMapAttribute("LINEPAUSE", typeof(string), 1, false)]
        public string LinePause;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region AlertErrorCode

    /// <summary>
    ///	AlertErrorCode
    /// </summary>
    [Serializable, TableMap("TBLALERTERRORCODE", "SUBITEMSEQUENCE")]
    public class AlertErrorCode : DomainObject
    {
        public AlertErrorCode()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///ItemType
        ///</summary>	
        [FieldMapAttribute("ITEMTYPE", typeof(string), 40, false)]
        public string ItemType;

        ///<summary>
        ///ErrorCauseCode
        ///</summary>	
        [FieldMapAttribute("ECSCODE", typeof(string), 40, false)]
        public string ErrorCauseCode;

        ///<summary>
        ///TimeDimension
        ///</summary>	
        [FieldMapAttribute("TIMEDIMENSION", typeof(string), 40, false)]
        public string TimeDimension;

        ///<summary>
        ///LineDivision
        ///</summary>	
        [FieldMapAttribute("LINEDIVISION", typeof(string), 1, false)]
        public string LineDivision;

        ///<summary>
        ///AlertValue
        ///</summary>	
        [FieldMapAttribute("ALERTVALUE", typeof(int), 38, false)]
        public int AlertValue;

        ///<summary>
        ///GenerateNotice
        ///</summary>	
        [FieldMapAttribute("GENERATENOTICE", typeof(string), 1, false)]
        public string GenerateNotice;

        ///<summary>
        ///SendMail
        ///</summary>	
        [FieldMapAttribute("SENDMAIL", typeof(string), 1, false)]
        public string SendMail;

        ///<summary>
        ///LinePause
        ///</summary>	
        [FieldMapAttribute("LINEPAUSE", typeof(string), 1, false)]
        public string LinePause;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region AlertDirectPass

    /// <summary>
    ///	AlertDirectPass
    /// </summary>
    [Serializable, TableMap("TBLALERTDIRECTPASS", "SUBITEMSEQUENCE")]
    public class AlertDirectPass : DomainObject
    {
        public AlertDirectPass()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///ItemType
        ///</summary>	
        [FieldMapAttribute("ITEMTYPE", typeof(string), 40, false)]
        public string ItemType;

        ///<summary>
        ///BaseOutput
        ///</summary>	
        [FieldMapAttribute("BASEOUTPUT", typeof(int), 38, false)]
        public int BaseOutput;

        ///<summary>
        ///TimeDimension
        ///</summary>	
        [FieldMapAttribute("TIMEDIMENSION", typeof(string), 40, false)]
        public string TimeDimension;

        ///<summary>
        ///AlertValue
        ///</summary>	
        [FieldMapAttribute("ALERTVALUE", typeof(decimal), 38, false)]
        public decimal AlertValue;

        ///<summary>
        ///GenerateNotice
        ///</summary>	
        [FieldMapAttribute("GENERATENOTICE", typeof(string), 1, false)]
        public string GenerateNotice;

        ///<summary>
        ///SendMail
        ///</summary>	
        [FieldMapAttribute("SENDMAIL", typeof(string), 1, false)]
        public string SendMail;

        ///<summary>
        ///LinePause
        ///</summary>	
        [FieldMapAttribute("LINEPAUSE", typeof(string), 1, false)]
        public string LinePause;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region AlertLinePause

    /// <summary>
    ///	AlertLinePause
    /// </summary>
    [Serializable, TableMap("TBLALERTLINEPAUSE", "SUBITEMSEQUENCE")]
    public class AlertLinePause : DomainObject
    {
        public AlertLinePause()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///OPCode
        ///</summary>	
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        ///<summary>
        ///AlertValue
        ///</summary>	
        [FieldMapAttribute("ALERTVALUE", typeof(int), 38, false)]
        public int AlertValue;

        ///<summary>
        ///GenerateNotice
        ///</summary>	
        [FieldMapAttribute("GENERATENOTICE", typeof(string), 1, false)]
        public string GenerateNotice;

        ///<summary>
        ///SendMail
        ///</summary>	
        [FieldMapAttribute("SENDMAIL", typeof(string), 1, false)]
        public string SendMail;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region AlertOQCNG

    /// <summary>
    ///	AlertOQCNG
    /// </summary>
    [Serializable, TableMap("TBLALERTOQCNG", "SUBITEMSEQUENCE")]
    public class AlertOQCNG : DomainObject
    {
        public AlertOQCNG()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///ItemType
        ///</summary>	
        [FieldMapAttribute("ITEMTYPE", typeof(string), 40, false)]
        public string ItemType;

        ///<summary>
        ///ErrorCode
        ///</summary>	
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ErrorCode;

        ///<summary>
        ///StartDate
        ///</summary>	
        [FieldMapAttribute("STARTDATE", typeof(int), 8, false)]
        public int StartDate;

        ///<summary>
        ///StartTime
        ///</summary>	
        [FieldMapAttribute("STARTTIME", typeof(int), 8, false)]
        public int StartTime;

        ///<summary>
        ///AlertValue
        ///</summary>	
        [FieldMapAttribute("ALERTVALUE", typeof(int), 38, false)]
        public int AlertValue;

        ///<summary>
        ///GenerateNotice
        ///</summary>	
        [FieldMapAttribute("GENERATENOTICE", typeof(string), 1, false)]
        public string GenerateNotice;

        ///<summary>
        ///SendMail
        ///</summary>	
        [FieldMapAttribute("SENDMAIL", typeof(string), 1, false)]
        public string SendMail;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region AlertMailSetting

    /// <summary>
    ///	AlertMailSetting
    /// </summary>
    [Serializable, TableMap("TBLALERTMAILSETTING", "SERIAL")]
    public class AlertMailSetting : DomainObject
    {
        public AlertMailSetting()
        {
        }

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///BIGSSCode
        ///</summary>	
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, true)]
        public string BIGSSCode;

        ///<summary>
        ///ItemFirstClass
        ///</summary>	
        [FieldMapAttribute("ITEMFIRSTCLASS", typeof(string), 40, true)]
        public string ItemFirstClass;

        ///<summary>
        ///ItemSecondClass
        ///</summary>	
        [FieldMapAttribute("ITEMSECONDCLASS", typeof(string), 40, true)]
        public string ItemSecondClass;

        ///<summary>
        ///Recipients
        ///</summary>	
        [FieldMapAttribute("RECIPIENTS", typeof(string), 2000, false)]
        public string Recipients;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region AlertNotice

    /// <summary>
    ///	AlertNotice
    /// </summary>
    [Serializable, TableMap("TBLALERTNOTICE", "SERIAL")]
    public class AlertNotice : DomainObject
    {
        public AlertNotice()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///Description
        ///</summary>	
        [FieldMapAttribute("DESCRIPTION", typeof(string), 100, true)]
        public string Description;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///NoticeSerial
        ///</summary>	
        [FieldMapAttribute("NOTICESERIAL", typeof(int), 38, false)]
        public int NoticeSerial;

        ///<summary>
        ///AlertType
        ///</summary>	
        [FieldMapAttribute("ALERTTYPE", typeof(string), 40, false)]
        public string AlertType;

        ///<summary>
        ///Status
        ///</summary>	
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///MOList
        ///</summary>	
        [FieldMapAttribute("MOLIST", typeof(string), 2000, true)]
        public string MOList;

        ///<summary>
        ///NoticeContent
        ///</summary>	
        [FieldMapAttribute("NOTICECONTENT", typeof(string), 4000, false)]
        public string NoticeContent;

        ///<summary>
        ///AnalysisReason
        ///</summary>	
        [FieldMapAttribute("ANALYSISREASON", typeof(string), 2000, true)]
        public string AnalysisReason;

        ///<summary>
        ///DealMethods
        ///</summary>	
        [FieldMapAttribute("DEALMETHODS", typeof(string), 2000, true)]
        public string DealMethods;

        ///<summary>
        ///NoticeDate
        ///</summary>	
        [FieldMapAttribute("NOTICEDATE", typeof(int), 8, false)]
        public int NoticeDate;

        ///<summary>
        ///NoticeTime
        ///</summary>	
        [FieldMapAttribute("NOTICETIME", typeof(int), 6, false)]
        public int NoticeTime;

        ///<summary>
        ///DealUser
        ///</summary>	
        [FieldMapAttribute("DEALUSER", typeof(string), 40, true)]
        public string DealUser;

        ///<summary>
        ///DealDate
        ///</summary>	
        [FieldMapAttribute("DEALDATE", typeof(int), 8, true)]
        public int DealDate;

        ///<summary>
        ///DealTime
        ///</summary>	
        [FieldMapAttribute("DEALTIME", typeof(int), 6, true)]
        public int DealTime;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region NoticeError

    /// <summary>
    ///	NoticeError
    /// </summary>
    [Serializable, TableMap("TBLNOTICEERROR", "SERIAL")]
    public class NoticeError : DomainObject
    {
        public NoticeError()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///ErrorCode
        ///</summary>	
        [FieldMapAttribute("ECODE", typeof(string), 40, false)]
        public string ErrorCode;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///ShiftDay
        ///</summary>	
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, false)]
        public int ShiftDay;

        ///<summary>
        ///BIGSSCode
        ///</summary>	
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BIGSSCode;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region NoticeErrorCode

    /// <summary>
    ///	NoticeErrorCode
    /// </summary>
    [Serializable, TableMap("TBLNOTICEERRORCODE", "SERIAL")]
    public class NoticeErrorCode : DomainObject
    {
        public NoticeErrorCode()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///ErrorCauseCode
        ///</summary>	
        [FieldMapAttribute("ECSCODE", typeof(string), 40, false)]
        public string ErrorCauseCode;

        ///<summary>
        ///Location
        ///</summary>	
        [FieldMapAttribute("LOCATION", typeof(string), 40, false)]
        public string Location;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///ShiftDay
        ///</summary>	
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, false)]
        public int ShiftDay;

        ///<summary>
        ///BIGSSCode
        ///</summary>	
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BIGSSCode;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region NoticeDirectPass

    /// <summary>
    ///	NoticeDirectPass
    /// </summary>
    [Serializable, TableMap("TBLNOTICEDIRECTPASS", "SERIAL")]
    public class NoticeDirectPass : DomainObject
    {
        public NoticeDirectPass()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///ItemCode
        ///</summary>	
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///ShiftCode
        ///</summary>	
        [FieldMapAttribute("SHIFTCODE", typeof(string), 40, false)]
        public string ShiftCode;

        ///<summary>
        ///ShiftDay
        ///</summary>	
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, false)]
        public int ShiftDay;

        ///<summary>
        ///BIGSSCode
        ///</summary>	
        [FieldMapAttribute("BIGSSCODE", typeof(string), 40, false)]
        public string BIGSSCode;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion

    #region NoticeLinePause

    /// <summary>
    ///	NoticeLinePause
    /// </summary>
    [Serializable, TableMap("TBLNOTICELINEPAUSE", "SERIAL")]
    public class NoticeLinePause : DomainObject
    {
        public NoticeLinePause()
        {
        }

        ///<summary>
        ///Serial
        ///</summary>	
        [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
        public int Serial;

        ///<summary>
        ///ItemSequence
        ///</summary>	
        [FieldMapAttribute("ITEMSEQUENCE", typeof(string), 40, false)]
        public string ItemSequence;

        ///<summary>
        ///SubItemSequence
        ///</summary>	
        [FieldMapAttribute("SUBITEMSEQUENCE", typeof(string), 40, false)]
        public string SubItemSequence;

        ///<summary>
        ///SSCode
        ///</summary>	
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string SSCode;

        ///<summary>
        ///OPCode
        ///</summary>	
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        ///<summary>
        ///ShiftDay
        ///</summary>	
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, false)]
        public int ShiftDay;

        ///<summary>
        ///ONWIPSerial
        ///</summary>	
        [FieldMapAttribute("ONWIPSERIAL", typeof(int), 38, false)]
        public int ONWIPSerial;

        ///<summary>
        ///MaintainUser
        ///</summary>	
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MaintainDate
        ///</summary>	
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MaintainTime
        ///</summary>	
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

    }

    #endregion
}