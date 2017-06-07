using System;
using BenQGuru.eMES.Common.Domain;
using System.Collections;

/// <summary>
/// ** 功能描述:	DomainObject for LotDataCollect
/// ** 作 者:		Created by Jarvis Chen
/// ** 日 期:		2012-03-28 11:01:01
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.LotDataCollect
{

    #region LotSimulation
    /// <summary>
    /// TBLLOTSIMULATION
    /// </summary>
    [Serializable, TableMap("TBLLOTSIMULATION", "MOCODE,LOTCODE")]
    public class LotSimulation : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public LotSimulation()
        {
        }

        ///<summary>
        ///LOTCODE
        ///</summary>
        [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
        public string LotCode;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///LOTSEQ
        ///</summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSeq;

        ///<summary>
        ///LOTQTY
        ///</summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 15, false)]
        public decimal LotQty;

        ///<summary>
        ///GOODQTY
        ///</summary>
        [FieldMapAttribute("GOODQTY", typeof(decimal), 15, false)]
        public decimal GoodQty;

        ///<summary>
        ///NGQTY
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(decimal), 15, false)]
        public decimal NGQty;

        ///<summary>
        ///LOTSTATUS
        ///</summary>
        [FieldMapAttribute("LOTSTATUS", typeof(string), 40, false)]
        public string LotStatus;

        ///<summary>
        ///MODELCODE
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///FROMROUTE
        ///</summary>
        [FieldMapAttribute("FROMROUTE", typeof(string), 40, true)]
        public string FromRoute;

        ///<summary>
        ///FROMOP
        ///</summary>
        [FieldMapAttribute("FROMOP", typeof(string), 40, true)]
        public string FromOP;

        ///<summary>
        ///ROUTECODE
        ///</summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        ///<summary>
        ///OPCODE
        ///</summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        ///<summary>
        ///RESCODE
        ///</summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResCode;

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LotNo;

        ///<summary>
        ///CARTONCODE
        ///</summary>
        [FieldMapAttribute("CARTONCODE", typeof(string), 40, true)]
        public string CartonCode;

        ///<summary>
        ///PALLETCODE
        ///</summary>
        [FieldMapAttribute("PALLETCODE", typeof(string), 40, true)]
        public string PalletCode;

        ///<summary>
        ///PRODUCTSTATUS
        ///</summary>
        [FieldMapAttribute("PRODUCTSTATUS", typeof(string), 40, false)]
        public string ProductStatus;

        ///<summary>
        ///LACTION
        ///</summary>
        [FieldMapAttribute("LACTION", typeof(string), 40, false)]
        public string LastAction;

        ///<summary>
        ///ACTIONLIST
        ///</summary>
        [FieldMapAttribute("ACTIONLIST", typeof(string), 100, true)]
        public string ActionList;

        ///<summary>
        ///NGTIMES
        ///</summary>
        [FieldMapAttribute("NGTIMES", typeof(decimal), 10, true)]
        public decimal NGTimes;

        ///<summary>
        ///ISCOM
        ///</summary>
        [FieldMapAttribute("ISCOM", typeof(string), 40, true)]
        public string IsComplete;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///EATTRIBUTE2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string EAttribute2;

        ///<summary>
        ///ISHOLD
        ///</summary>
        [FieldMapAttribute("ISHOLD", typeof(int), 10, true)]
        public int IsHold;

        ///<summary>
        ///SHELFNO
        ///</summary>
        [FieldMapAttribute("SHELFNO", typeof(string), 40, true)]
        public string ShelfNo;

        ///<summary>
        ///RMABILLCODE
        ///</summary>
        [FieldMapAttribute("RMABILLCODE", typeof(string), 40, true)]
        public string RMABillCode;

        ///<summary>
        ///MOSEQ
        ///</summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        ///<summary>
        ///COLLECTSTATUS
        ///</summary>
        [FieldMapAttribute("COLLECTSTATUS", typeof(string), 40, true)]
        public string CollectStatus;

        ///<summary>
        ///BEGINDATE
        ///</summary>
        [FieldMapAttribute("BEGINDATE", typeof(int), 8, true)]
        public int BeginDate;

        ///<summary>
        ///BEGINTIME
        ///</summary>
        [FieldMapAttribute("BEGINTIME", typeof(int), 6, true)]
        public int BeginTime;

        ///<summary>
        ///ENDDATE
        ///</summary>
        [FieldMapAttribute("ENDDATE", typeof(int), 8, true)]
        public int EndDate;

        ///<summary>
        ///ENDTIME
        ///</summary>
        [FieldMapAttribute("ENDTIME", typeof(int), 6, true)]
        public int EndTime;
    }
    #endregion

    #region LotSimulationForQuery
    public class LotSimulationForQuery : LotSimulation
    {
        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, true)]
        public int MaintainDate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, true)]
        public int MaintainTime;
    }
    #endregion

    #region LOTONWIP
    /// <summary>
    /// TBLLOTONWIP
    /// </summary>
    [Serializable, TableMap("TBLLOTONWIP", "MOCODE,LOTCODE,LOTSEQ")]
    public class LotOnWip : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public LotOnWip()
        {
        }

        ///<summary>
        ///LOTQTY
        ///</summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 15, false)]
        public decimal LotQty;

        ///<summary>
        ///GOODQTY
        ///</summary>
        [FieldMapAttribute("GOODQTY", typeof(decimal), 15, false)]
        public decimal GoodQty;

        ///<summary>
        ///NGQTY
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(decimal), 15, false)]
        public decimal NGQty;

        ///<summary>
        ///ISCOLLECT
        ///</summary>
        [FieldMapAttribute("COLLECTSTATUS", typeof(string), 40, true)]
        public string CollectStatus;

        ///<summary>
        ///BEGINDATE
        ///</summary>
        [FieldMapAttribute("BEGINDATE", typeof(int), 22, true)]
        public int BeginDate;

        ///<summary>
        ///BEGINTIME
        ///</summary>
        [FieldMapAttribute("BEGINTIME", typeof(int), 22, true)]
        public int BeginTime;

        ///<summary>
        ///ENDDATE
        ///</summary>
        [FieldMapAttribute("ENDDATE", typeof(int), 22, true)]
        public int EndDate;

        ///<summary>
        ///ENDTIME
        ///</summary>
        [FieldMapAttribute("ENDTIME", typeof(int), 22, true)]
        public int EndTime;

        ///<summary>
        ///LOTCODE
        ///</summary>
        [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
        public string LotCode;

        ///<summary>
        ///LOTSEQ
        ///</summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 22, false)]
        public decimal LotSeq;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///MODELCODE
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///ROUTECODE
        ///</summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        ///<summary>
        ///OPCODE
        ///</summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        ///<summary>
        ///SEGCODE
        ///</summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmentCode;

        ///<summary>
        ///SSCODE
        ///</summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        ///<summary>
        ///RESCODE
        ///</summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResCode;

        ///<summary>
        ///SHIFTTYPECODE
        ///</summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
        public string ShiftTypeCode;

        ///<summary>
        ///SHIFTCODE
        ///</summary>
        [FieldMapAttribute("BEGINSHIFTCODE", typeof(string), 40, false)]
        public string BeginShiftCode;

        ///<summary>
        ///SHIFTCODE
        ///</summary>
        [FieldMapAttribute("ENDSHIFTCODE", typeof(string), 40, false)]
        public string EndShiftCode;

        ///<summary>
        ///TPCODE
        ///</summary>
        [FieldMapAttribute("BEGINTPCODE", typeof(string), 40, false)]
        public string BeginTimePeriodCode;

        ///<summary>
        ///TPCODE
        ///</summary>
        [FieldMapAttribute("ENDTPCODE", typeof(string), 40, false)]
        public string EndTimePeriodCode;


        ///<summary>
        ///SHIFTDAY
        ///</summary>
        [FieldMapAttribute("BEGINSHIFTDAY", typeof(int), 22, true)]
        public int BeginShiftDay;

        ///<summary>
        ///SHIFTDAY
        ///</summary>
        [FieldMapAttribute("ENDSHIFTDAY", typeof(int), 22, true)]
        public int EndShiftDay;

        ///<summary>
        ///ACTION
        ///</summary>
        [FieldMapAttribute("ACTION", typeof(string), 40, false)]
        public string Action;

        ///<summary>
        ///ACTIONRESULT
        ///</summary>
        [FieldMapAttribute("ACTIONRESULT", typeof(string), 40, false)]
        public string ActionResult;

        ///<summary>
        ///NGTIMES
        ///</summary>
        [FieldMapAttribute("NGTIMES", typeof(decimal), 22, true)]
        public decimal NGTimes;


        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;


        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///SHELFNO
        ///</summary>
        [FieldMapAttribute("SHELFNO", typeof(string), 40, true)]
        public string ShelfNo;

        ///<summary>
        ///RMABILLCODE
        ///</summary>
        [FieldMapAttribute("RMABILLCODE", typeof(string), 40, true)]
        public string RMABillCode;

        ///<summary>
        ///MOSEQ
        ///</summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 22, true)]
        public decimal MoSeq;

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(int), 22, true)]
        public int Serial;

        ///<summary>
        ///PROCESSED
        ///</summary>
        [FieldMapAttribute("PROCESSED", typeof(int), 22, false)]
        public int Processed;

    }
    #endregion

    #region LotSimulationReport
    /// <summary>
    /// TBLLOTSIMULATIONREPORT
    /// </summary>
    [Serializable, TableMap("TBLLOTSIMULATIONREPORT", "MOCODE,LOTCODE")]
    public class LotSimulationReport : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public LotSimulationReport()
        {
        }

        ///<summary>
        ///LOTCODE
        ///</summary>
        [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
        public string LotCode;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///LOTSEQ
        ///</summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 10, true)]
        public decimal LotSeq;

        ///<summary>
        ///LOTQTY
        ///</summary>
        [FieldMapAttribute("LOTQTY", typeof(decimal), 15, false)]
        public decimal LotQty;

        ///<summary>
        ///GOODQTY
        ///</summary>
        [FieldMapAttribute("GOODQTY", typeof(decimal), 15, false)]
        public decimal GoodQty;

        ///<summary>
        ///NGQTY
        ///</summary>
        [FieldMapAttribute("NGQTY", typeof(decimal), 15, false)]
        public decimal NGQty;

        ///<summary>
        ///LOTSTATUS
        ///</summary>
        [FieldMapAttribute("LOTSTATUS", typeof(string), 40, false)]
        public string LotStatus;

        ///<summary>
        ///MODELCODE
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;


        ///<summary>
        ///SHIFTTYPECODE
        ///</summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
        public string ShiftTypeCode;

        ///<summary>
        ///BEGINSHIFTCODE
        ///</summary>
        [FieldMapAttribute("BEGINSHIFTCODE", typeof(string), 40, false)]
        public string BeginShiftCode;

        ///<summary>
        ///BEGINSHIFTCODE
        ///</summary>
        [FieldMapAttribute("ENDSHIFTCODE", typeof(string), 40, false)]
        public string EndShiftCode;



        ///<summary>
        ///TPCODE
        ///</summary>
        [FieldMapAttribute("BEGINTPCODE", typeof(string), 40, false)]
        public string BeginTimePeriodCode;

        ///<summary>
        ///TPCODE
        ///</summary>
        [FieldMapAttribute("ENDTPCODE", typeof(string), 40, false)]
        public string EndTimePeriodCode;

        ///<summary>
        ///OPCODE
        ///</summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        ///<summary>
        ///ROUTECODE
        ///</summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        ///<summary>
        ///SEGCODE
        ///</summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, true)]
        public string SegmentCode;

        ///<summary>
        ///SSCODE
        ///</summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string StepSequenceCode;

        ///<summary>
        ///RESCODE
        ///</summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResCode;

        ///<summary>
        ///LACTION
        ///</summary>
        [FieldMapAttribute("LACTION", typeof(string), 40, false)]
        public string LastAction;

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LotNo;

        ///<summary>
        ///CARTONCODE
        ///</summary>
        [FieldMapAttribute("CARTONCODE", typeof(string), 40, true)]
        public string CartonCode;

        ///<summary>
        ///PALLETCODE
        ///</summary>
        [FieldMapAttribute("PALLETCODE", typeof(string), 40, true)]
        public string PalletCode;

        ///<summary>
        ///NGTIMES
        ///</summary>
        [FieldMapAttribute("NGTIMES", typeof(decimal), 10, true)]
        public decimal NGTimes;

        ///<summary>
        ///ISCOM
        ///</summary>
        [FieldMapAttribute("ISCOM", typeof(string), 40, true)]
        public string IsComplete;



        ///<summary>
        ///ISCOLLECT
        ///</summary>
        [FieldMapAttribute("COLLECTSTATUS", typeof(string), 40, true)]
        public string CollectStatus;

        ///<summary>
        ///STATUS
        ///</summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        ///<summary>
        ///BEGINSHIFTDAY
        ///</summary>
        [FieldMapAttribute("BEGINSHIFTDAY", typeof(int), 8, true)]
        public int BeginShiftDay;

        ///<summary>
        ///ENDSHIFTDAY
        ///</summary>
        [FieldMapAttribute("ENDSHIFTDAY", typeof(int), 8, true)]
        public int EndShiftDay;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;


        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

        ///<summary>
        ///EATTRIBUTE2
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE2", typeof(string), 40, true)]
        public string EAttribute2;

        ///<summary>
        ///SHELFNO
        ///</summary>
        [FieldMapAttribute("SHELFNO", typeof(string), 40, true)]
        public string ShelfNo;

        ///<summary>
        ///RMABILLCODE
        ///</summary>
        [FieldMapAttribute("RMABILLCODE", typeof(string), 40, true)]
        public string RMABillCode;

        ///<summary>
        ///MOSEQ
        ///</summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 10, true)]
        public decimal MOSeq;

        ///<summary>
        ///BEGINDATE
        ///</summary>
        [FieldMapAttribute("BEGINDATE", typeof(int), 8, true)]
        public int BeginDate;

        ///<summary>
        ///BEGINTIME
        ///</summary>
        [FieldMapAttribute("BEGINTIME", typeof(int), 6, true)]
        public int BeginTime;

        ///<summary>
        ///ENDDATE
        ///</summary>
        [FieldMapAttribute("ENDDATE", typeof(int), 8, true)]
        public int EndDate;

        ///<summary>
        ///ENDTIME
        ///</summary>
        [FieldMapAttribute("ENDTIME", typeof(int), 6, true)]
        public int EndTime;
    }
    #endregion

    #region OnWIPLotTransfer
    /// <summary>
    /// TBLONWIPLOTTRANS
    /// </summary>
    [Serializable, TableMap("TBLONWIPLOTTRANS", "SERIAL")]
    public class OnWipLotTrans : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public OnWipLotTrans()
        {
        }

        ///<summary>
        ///SERIAL
        ///</summary>
        [FieldMapAttribute("SERIAL", typeof(decimal), 10, false)]
        public decimal Serial;

        ///<summary>
        ///LOTCODE
        ///</summary>
        [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
        public string LotCode;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///LOTSEQ
        ///</summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 22, false)]
        public decimal LotSeq;

        ///<summary>
        ///TLOTCODE
        ///</summary>
        [FieldMapAttribute("TLOTCODE", typeof(string), 40, false)]
        public string TLotCode;

        ///<summary>
        ///TLOTSEQ
        ///</summary>
        [FieldMapAttribute("TLOTSEQ", typeof(decimal), 10, true)]
        public decimal TLotSeq;

        /////<summary>
        /////SLOTCODE
        /////</summary>
        //[FieldMapAttribute("SLOTCODE", typeof(string), 40, false)]
        //public string SLotCode;

        /////<summary>
        /////SLOTSEQ
        /////</summary>
        //[FieldMapAttribute("SLOTSEQ", typeof(decimal), 10, true)]
        //public decimal SLotSeq;

        /////<summary>
        /////SLOTQTY
        /////</summary>
        //[FieldMapAttribute("SLOTQTY", typeof(decimal), 15, false)]
        //public decimal SLotQty;

        ///<summary>
        ///TLOTQTY
        ///</summary>
        [FieldMapAttribute("TLOTQTY", typeof(decimal), 15, false)]
        public decimal TLotQty;

        ///<summary>
        ///QTY
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 15, false)]
        public decimal Qty;

        ///<summary>
        ///ROUTECODE
        ///</summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, true)]
        public string RouteCode;

        ///<summary>
        ///OPCODE
        ///</summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, true)]
        public string OPCode;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string EAttribute1;

        ///<summary>
        ///OPERATIONTYPE
        ///</summary>
        [FieldMapAttribute("OPERATIONTYPE", typeof(string), 40, true)]
        public string OperationType;
    }
    #endregion

    #region LOTONWIPITEM
    /// <summary>
    /// TBLLOTONWIPITEM
    /// </summary>
    [Serializable, TableMap("TBLLOTONWIPITEM", "MOCODE,LOTCODE,LOTSEQ,MSEQ")]
    public class LotOnWipItem : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public LotOnWipItem()
        {
        }

        ///<summary>
        ///BEGINDATE
        ///</summary>
        [FieldMapAttribute("MSEQ", typeof(int), 22, true)]
        public int MSeq;

        ///<summary>
        ///RESCODE
        ///</summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
        public string ResCode;

        ///<summary>
        ///SHIFTTYPECODE
        ///</summary>
        [FieldMapAttribute("SHIFTTYPECODE", typeof(string), 40, false)]
        public string ShiftTypeCode;


        ///<summary>
        ///SHIFTCODE
        ///</summary>
        [FieldMapAttribute("BEGINSHIFTCODE", typeof(string), 40, false)]
        public string BeginShiftCode;

        ///<summary>
        ///SHIFTCODE
        ///</summary>
        [FieldMapAttribute("ENDSHIFTCODE", typeof(string), 40, false)]
        public string EndShiftCode;

        ///<summary>
        ///TPCODE
        ///</summary>
        [FieldMapAttribute("BEGINTPCODE", typeof(string), 40, false)]
        public string BeginTimePeriodCode;

        ///<summary>
        ///TPCODE
        ///</summary>
        [FieldMapAttribute("ENDTPCODE", typeof(string), 40, false)]
        public string EndTimePeriodCode;


        ///<summary>
        ///TRANSSTATUS
        ///</summary>
        [FieldMapAttribute("TRANSSTATUS", typeof(string), 40, false)]
        public string TransStatus;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        ///<summary>
        ///BEGINDATE
        ///</summary>
        [FieldMapAttribute("BEGINDATE", typeof(int), 22, true)]
        public int BeginDate;

        ///<summary>
        ///BEGINTIME
        ///</summary>
        [FieldMapAttribute("BEGINTIME", typeof(int), 22, true)]
        public int BeginTime;

        ///<summary>
        ///ENDDATE
        ///</summary>
        [FieldMapAttribute("ENDDATE", typeof(int), 22, true)]
        public int EndDate;

        ///<summary>
        ///ENDTIME
        ///</summary>
        [FieldMapAttribute("ENDTIME", typeof(int), 22, true)]
        public int EndTime;

        ///<summary>
        ///EATTRIBUTE1
        ///</summary>
        [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
        public string Eattribute1;

        ///<summary>
        ///DROPUSER
        ///</summary>
        [FieldMapAttribute("DROPUSER", typeof(string), 40, true)]
        public string DropUser;

        ///<summary>
        ///DROPDATE
        ///</summary>
        [FieldMapAttribute("DROPDATE", typeof(int), 22, true)]
        public int DropDate;

        ///<summary>
        ///DROPTIME
        ///</summary>
        [FieldMapAttribute("DROPTIME", typeof(int), 22, true)]
        public int DropTime;

        ///<summary>
        ///ACTIONTYPE
        ///</summary>
        [FieldMapAttribute("ACTIONTYPE", typeof(int), 22, true)]
        public int ActionType;

        ///<summary>
        ///DROPOP
        ///</summary>
        [FieldMapAttribute("DROPOP", typeof(string), 40, true)]
        public string Dropop;

        ///<summary>
        ///MOSEQ
        ///</summary>
        [FieldMapAttribute("MOSEQ", typeof(decimal), 22, true)]
        public decimal MOSeq;

        ///<summary>
        ///LOTCODE
        ///</summary>
        [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
        public string LotCode;

        ///<summary>
        ///LOTSEQ
        ///</summary>
        [FieldMapAttribute("LOTSEQ", typeof(decimal), 22, false)]
        public decimal LotSeq;

        ///<summary>
        ///MOCODE
        ///</summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        ///<summary>
        ///MODELCODE
        ///</summary>
        [FieldMapAttribute("MODELCODE", typeof(string), 40, false)]
        public string ModelCode;

        ///<summary>
        ///ITEMCODE
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        ///<summary>
        ///MITEMCODE
        ///</summary>
        [FieldMapAttribute("MITEMCODE", typeof(string), 40, true)]
        public string MItemCode;

        ///<summary>
        ///MCARDTYPE
        ///</summary>
        [FieldMapAttribute("MCARDTYPE", typeof(string), 40, false)]
        public string MCardType;

        ///<summary>
        ///QTY
        ///</summary>
        [FieldMapAttribute("QTY", typeof(decimal), 22, false)]
        public decimal Qty;

        ///<summary>
        ///LOTNO
        ///</summary>
        [FieldMapAttribute("LOTNO", typeof(string), 40, true)]
        public string LOTNO;

        ///<summary>
        ///VERSION
        ///</summary>
        [FieldMapAttribute("VERSION", typeof(string), 40, true)]
        public string Version;

        ///<summary>
        ///VENDORITEMCODE
        ///</summary>
        [FieldMapAttribute("VENDORITEMCODE", typeof(string), 40, true)]
        public string VendorItemCode;

        ///<summary>
        ///VENDORCODE
        ///</summary>
        [FieldMapAttribute("VENDORCODE", typeof(string), 40, true)]
        public string VendorCode;

        ///<summary>
        ///DATECODE
        ///</summary>
        [FieldMapAttribute("DATECODE", typeof(string), 40, true)]
        public string DateCode;

        ///<summary>
        ///ROUTECODE
        ///</summary>
        [FieldMapAttribute("ROUTECODE", typeof(string), 40, false)]
        public string RouteCode;

        ///<summary>
        ///OPCODE
        ///</summary>
        [FieldMapAttribute("OPCODE", typeof(string), 40, false)]
        public string OPCode;

        ///<summary>
        ///SEGCODE
        ///</summary>
        [FieldMapAttribute("SEGCODE", typeof(string), 40, false)]
        public string SegmentCode;

        ///<summary>
        ///SSCODE
        ///</summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
        public string StepSequenceCode;


        ///<summary>
        ///ISCOLLECT
        ///</summary>
        [FieldMapAttribute("COLLECTSTATUS", typeof(string), 40, true)]
        public string CollectStatus;

    }
    #endregion

    #region ErrorCode
    public class ErrorCodeForLot : DomainObject
    {
        public ErrorCodeForLot()
        {
        }

        /// <summary>
        /// ECGCode
        /// </summary>
        [FieldMapAttribute("ECGCode", typeof(string), 40, true)]
        public string ECGCode;

        /// <summary>
        /// ECode
        /// </summary>
        [FieldMapAttribute("ECode", typeof(string), 40, true)]
        public string ECode;

        /// <summary>
        /// ECGDesc
        /// </summary>
        [FieldMapAttribute("ECGDesc", typeof(string), 40, true)]
        public string ECGDesc;

    }
    #endregion

}