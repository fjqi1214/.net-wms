--------------------------------------------------------------------------
-- Play this script in DIFF@YELLOW to make it look like HISENSE_MES@PINK
--                                                                      --
-- Please review the script before using it to make sure it won't       --
-- cause any unacceptable data loss.                                    --
--                                                                      --
-- DIFF@YELLOW Schema Extracted by User DIFF 
-- HISENSE_MES@PINK Schema Extracted by User HISENSE_MES 
CREATE OR REPLACE TYPE productimpl AS OBJECT (
   product_result   NUMBER,
   STATIC FUNCTION odciaggregateinitialize (sctx IN OUT productimpl)
      RETURN NUMBER,
   MEMBER FUNCTION odciaggregateiterate (SELF IN OUT productimpl, VALUE IN NUMBER)
      RETURN NUMBER,
   MEMBER FUNCTION odciaggregateterminate (SELF IN productimpl, returnvalue OUT NUMBER, flags IN NUMBER)
      RETURN NUMBER,
   MEMBER FUNCTION odciaggregatemerge (SELF IN OUT productimpl, ctx2 IN productimpl)
      RETURN NUMBER
)

/

SHOW ERRORS;

CREATE OR REPLACE type TYPE_ALERT_NOTICE as object
(
  -- Author  : WINDY.XU
  -- Created : 2009-4-10 13:43:31
  -- Purpose : ????Ì§???±Ì‡‡

    SERIAL          NUMBER,
    ITEMSEQUENCE    VARCHAR2(40),
    DESCRIPTION     VARCHAR2(100),
    SUBITEMSEQUENCE VARCHAR2(40),
    NOTICESERIAL    NUMBER,
    ALERTTYPE       VARCHAR2(40),
    STATUS          VARCHAR2(40),
    MOLIST          VARCHAR2(2000),
    NOTICECONTENT   VARCHAR2(4000),
    ANALYSISREASON  VARCHAR2(2000),
    DEALMETHODS     VARCHAR2(2000),
    NOTICEDATE      NUMBER(8),
    NOTICETIME      NUMBER(6),
    DEALUSER        VARCHAR2(40),
    DEALDATE        NUMBER(8),
    DEALTIME        NUMBER(6),
    MUSER           VARCHAR2(40),
    MDATE           NUMBER(8),
    MTIME           NUMBER(6),

    CONSTRUCTOR FUNCTION TYPE_ALERT_NOTICE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        DESCRIPTION     IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        NOTICESERIAL    IN  NUMBER   DEFAULT 0,
        ALERTTYPE       IN  VARCHAR2 DEFAULT ' ',
        STATUS          IN  VARCHAR2 DEFAULT ' ',
        MOLIST          IN  VARCHAR2 DEFAULT ' ',
        NOTICECONTENT   IN  VARCHAR2 DEFAULT ' ',
        ANALYSISREASON  IN  VARCHAR2 DEFAULT ' ',
        DEALMETHODS     IN  VARCHAR2 DEFAULT ' ',
        NOTICEDATE      IN  NUMBER   DEFAULT 0,
        NOTICETIME      IN  NUMBER   DEFAULT 0,
        DEALUSER        IN  VARCHAR2 DEFAULT ' ',
        DEALDATE        IN  NUMBER   DEFAULT 0,
        DEALTIME        IN  NUMBER   DEFAULT 0,
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT,

    MEMBER PROCEDURE WRITE
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE type_joblog
AS OBJECT

(
   JOBID           VARCHAR2(40),
    SERIAL          NUMBER,
    STARTDATETIME   DATE,
    ENDDATETIME     DATE,
    USEDTIME        NUMBER,
    PROCESSCOUNT    NUMBER,
    RESULT          VARCHAR2(40),
    ERRORMESSAGE    VARCHAR2(500),
    CONSTRUCTOR FUNCTION type_JobLog (
        JOBID           IN  VARCHAR2    DEFAULT ' ',
        SERIAL          IN  NUMBER      DEFAULT 0,
        STARTDATETIME   IN  DATE        DEFAULT SYSDATE,
        ENDDATETIME     IN  DATE        DEFAULT SYSDATE,
        USEDTIME        IN  NUMBER      DEFAULT 0,
        PROCESSCOUNT    IN  NUMBER      DEFAULT 0,
        RESULT          IN  VARCHAR2    DEFAULT ' ',
        ERRORMESSAGE    IN  VARCHAR2    DEFAULT ' '
    )
        RETURN SELF AS RESULT,
    MEMBER PROCEDURE WRITE,
    MEMBER PROCEDURE ClearHistoryLog

)

/

SHOW ERRORS;

CREATE OR REPLACE type TYPE_MAIL as object
(
  -- Author  : WINDY.XU
  -- Created : 2009-4-10 14:19:03
  -- Purpose : Mail‡‡

    SERIAL       NUMBER,
    MAILSUBJECT  VARCHAR2(150),
    RECIPIENTS   VARCHAR2(4000),
    MAILCONTENT  VARCHAR2(4000),
    ISSEND       VARCHAR2(1),
    SENDTIMES    NUMBER,
    SENDRESULT   VARCHAR2(40),
    ERRORMESSAGE VARCHAR2(2000),
    MUSER        VARCHAR2(40),
    MDATE        NUMBER(8),
    MTIME        NUMBER(6),
    EATTRIBUTE1  VARCHAR2(100),
    EATTRIBUTE2  VARCHAR2(100),
    EATTRIBUTE3  VARCHAR2(100),

    CONSTRUCTOR FUNCTION TYPE_MAIL (
        SERIAL       IN  NUMBER   DEFAULT 0,
        MAILSUBJECT  IN  VARCHAR2 DEFAULT ' ',
        RECIPIENTS   IN  VARCHAR2 DEFAULT ' ',
        MAILCONTENT  IN  VARCHAR2 DEFAULT ' ',
        ISSEND       IN  VARCHAR2 DEFAULT ' ',
        SENDTIMES    IN  NUMBER   DEFAULT 0,
        SENDRESULT   IN  VARCHAR2 DEFAULT ' ',
        ERRORMESSAGE IN  VARCHAR2 DEFAULT ' ',
        MUSER        IN  VARCHAR2 DEFAULT ' ',
        MDATE        IN  NUMBER   DEFAULT 0,
        MTIME        IN  NUMBER   DEFAULT 0,
        EATTRIBUTE1  IN  VARCHAR2 DEFAULT ' ',
        EATTRIBUTE2  IN  VARCHAR2 DEFAULT ' ',
        EATTRIBUTE3  IN  VARCHAR2 DEFAULT ' '
    )
        RETURN SELF AS RESULT,

    MEMBER PROCEDURE WRITE
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE "TYPE_MESENTITYLIST"                                                                                                                                                    AS OBJECT (
    SERIAL          NUMBER,
    BigSSCode       VARCHAR2(40),
    ModelCode       VARCHAR2(40),
    OPCode          VARCHAR2(40),
    SegmentCode     VARCHAR2(40),
    SSCode          VARCHAR2(40),
    ResourceCode    VARCHAR2(40),
    ShiftTypeCode   VARCHAR2(40),
    ShiftCode       VARCHAR2(40),
    TimePeriodCode  VARCHAR2(40),
    FactoryCode     VARCHAR2(40),
    OrgID           NUMBER,
    EAttribute1     VARCHAR2(40),
    CONSTRUCTOR FUNCTION type_MESEntityList (
        SERIAL          IN  NUMBER      DEFAULT 0,
        BigSSCode       IN  VARCHAR2    DEFAULT ' ',
        ModelCode       IN  VARCHAR2    DEFAULT ' ',
        OPCode          IN  VARCHAR2    DEFAULT ' ',
        SegmentCode     IN  VARCHAR2    DEFAULT ' ',
        SSCode          IN  VARCHAR2    DEFAULT ' ',
        ResourceCode    IN  VARCHAR2    DEFAULT ' ',
        ShiftTypeCode   IN  VARCHAR2    DEFAULT ' ',
        ShiftCode       IN  VARCHAR2    DEFAULT ' ',
        TimePeriodCode  IN  VARCHAR2    DEFAULT ' ',
        FactoryCode     IN  VARCHAR2    DEFAULT ' ',
        OrgID           IN  NUMBER      DEFAULT 0,
        EAttribute1     IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT,
    MEMBER PROCEDURE SAVE,
    MEMBER FUNCTION GetMESEntitySerial
        RETURN NUMBER
)

/

SHOW ERRORS;

CREATE OR REPLACE type TYPE_NOTICE_DIRECTPASS as object
(
  -- Author  : WINDY.XU
  -- Created : 2009-4-10 13:19:21
  -- Purpose : ?±Ì?????‡‡

    SERIAL          NUMBER,
    ITEMSEQUENCE    VARCHAR2(40),
    SUBITEMSEQUENCE VARCHAR2(40),
    ITEMCODE        VARCHAR2(40),
    SHIFTCODE       VARCHAR2(40),
    SHIFTDAY        NUMBER(8),
    BIGSSCODE       VARCHAR2(40),
    MUSER           VARCHAR2(40),
    MDATE           NUMBER(8),
    MTIME           NUMBER(6),

    CONSTRUCTOR FUNCTION TYPE_NOTICE_DIRECTPASS (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT,

    MEMBER PROCEDURE WRITE,
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
)

/

SHOW ERRORS;

CREATE OR REPLACE type TYPE_NOTICE_ERROR as object
(
  -- Author  : WINDY.XU
  -- Created : 2009-4-10 11:18:01
  -- Purpose : 1??????‡‡
    SERIAL          NUMBER,
    ITEMSEQUENCE    VARCHAR2(40),
    SUBITEMSEQUENCE VARCHAR2(40),
    ITEMCODE        VARCHAR2(40),
    ECODE           VARCHAR2(40),
    SHIFTCODE       VARCHAR2(40),
    SHIFTDAY        NUMBER(8),
    BIGSSCODE       VARCHAR2(40),
    MUSER           VARCHAR2(40),
    MDATE           NUMBER(8),
    MTIME           NUMBER(6),

    CONSTRUCTOR FUNCTION TYPE_NOTICE_ERROR (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        ECODE           IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
    RETURN SELF AS RESULT,

    MEMBER PROCEDURE WRITE,

		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER

)

/

SHOW ERRORS;

CREATE OR REPLACE type TYPE_NOTICE_ERROR_CODE as object
(
  -- Author  : WINDY.XU
  -- Created : 2009-4-10 13:01:43
  -- Purpose : 2????ÚÚ‡‡

    SERIAL          NUMBER,
    ITEMSEQUENCE    VARCHAR2(40),
    SUBITEMSEQUENCE VARCHAR2(40),
    ITEMCODE        VARCHAR2(40),
    ECSCODE         VARCHAR2(40),
    LOCATION        VARCHAR2(40),
    SHIFTCODE       VARCHAR2(40),
    SHIFTDAY        NUMBER(8),
    BIGSSCODE       VARCHAR2(40),
    MUSER           VARCHAR2(40),
    MDATE           NUMBER(8),
    MTIME           NUMBER(6),

    CONSTRUCTOR FUNCTION TYPE_NOTICE_ERROR_CODE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        ECSCODE         IN  VARCHAR2 DEFAULT ' ',
        LOCATION        IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT,

    MEMBER PROCEDURE WRITE,
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
)

/

SHOW ERRORS;

CREATE OR REPLACE type TYPE_NOTICE_LINEPAUSE as object
(
  -- Author  : WINDY.XU
  -- Created : 2009-4-10 13:29:51
  -- Purpose : ???????‡‡

    SERIAL          NUMBER,
    ITEMSEQUENCE    VARCHAR2(40),
    SUBITEMSEQUENCE VARCHAR2(40),
    SSCODE          VARCHAR2(40),
    OPCODE          VARCHAR2(40),
    SHIFTDAY        NUMBER(8),
    ONWIPSERIAL     NUMBER,
    MUSER           VARCHAR2(40),
    MDATE           NUMBER(8),
    MTIME           NUMBER(6),

    CONSTRUCTOR FUNCTION TYPE_NOTICE_LINEPAUSE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        SSCODE          IN  VARCHAR2 DEFAULT ' ',
        OPCODE          IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        ONWIPSERIAL     IN  NUMBER   DEFAULT 0,
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT,

    MEMBER PROCEDURE WRITE,

		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE "TYPE_ONWIPDATA"                                                                                                                                                    AS OBJECT (
    RCard                   VARCHAR2(40),
    RCardSeq                NUMBER,    
    MOCode                  VARCHAR2(40),
    ModelCode               VARCHAR2(40),
    ItemCode                VARCHAR2(40),
    RouteCode               VARCHAR2(40),
    OPCode                  VARCHAR2(40),
    SegCode                 VARCHAR2(40),
    SSCode                  VARCHAR2(40),
    ResCode                 VARCHAR2(40),
    ShiftTypeCode           VARCHAR2(40),
    ShiftCode               VARCHAR2(40),
    TPCode                  VARCHAR2(40),
    ShiftDay                NUMBER,
    Action                  VARCHAR2(40),
    ActionResult            VARCHAR2(40),
    NGTimes                 NUMBER,
    EAttribute1             VARCHAR2(40),
    Serial                  NUMBER,
    CONSTRUCTOR FUNCTION type_ONWIPData (
        RCard               IN  VARCHAR2    DEFAULT ' ',
        RCardSeq            IN  NUMBER      DEFAULT 0,
        MOCode              IN  VARCHAR2    DEFAULT ' ',
        ModelCode           IN  VARCHAR2    DEFAULT ' ',
        ItemCode            IN  VARCHAR2    DEFAULT ' ',
        RouteCode           IN  VARCHAR2    DEFAULT ' ',
        OPCode              IN  VARCHAR2    DEFAULT ' ',
        SegCode             IN  VARCHAR2    DEFAULT ' ',
        SSCode              IN  VARCHAR2    DEFAULT ' ',
        ResCode             IN  VARCHAR2    DEFAULT ' ',
        ShiftTypeCode       IN  VARCHAR2    DEFAULT ' ',
        ShiftCode           IN  VARCHAR2    DEFAULT ' ',
        TPCode              IN  VARCHAR2    DEFAULT ' ',
        ShiftDay            IN  NUMBER      DEFAULT 0,
        Action              IN  VARCHAR2    DEFAULT ' ',
        ActionResult        IN  VARCHAR2    DEFAULT ' ',
        NGTimes             IN  NUMBER      DEFAULT 0,
        EAttribute1         IN  VARCHAR2    DEFAULT '',
        Serial              IN  NUMBER      DEFAULT 0
    )
        RETURN SELF AS RESULT
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE "TYPE_ONWIP_LIST"                                                                                                                                                   AS TABLE OF type_ONWIPData;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE "TYPE_RPTLINEQTY"                                                                                                                                                    AS OBJECT (
    MOCode              VARCHAR2(40),
    ShiftDay            NUMBER,
    ItemCode            VARCHAR2(40),
    MESEntity_Serial    NUMBER,
    LineWhiteCardCount  NUMBER,
    ResWhiteCardCount   NUMBER,
    EAttribute1         VARCHAR2(40),
    CONSTRUCTOR FUNCTION type_RPTLINEQTY (
        MOCode              IN  VARCHAR2    DEFAULT ' ',
        ShiftDay            IN  NUMBER      DEFAULT 0,
        ItemCode            IN  VARCHAR2    DEFAULT ' ',
        MESEntity_Serial    IN  NUMBER      DEFAULT 0,
        LineWhiteCardCount  IN  NUMBER      DEFAULT 0,
        ResWhiteCardCount   IN  NUMBER      DEFAULT 0,
        EAttribute1         IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT,
    MEMBER PROCEDURE ADDNew
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE "TYPE_RPTOPQTY"                                                                                                                                                    AS OBJECT (
    MOCode              VARCHAR2(40),
    ShiftDay            NUMBER,
    ItemCode            VARCHAR2(40),
    MESEntity_Serial    NUMBER,
    InputTimes          NUMBER,
    OutputTimes         NUMBER,
    NGTimes             NUMBER,
    EAttribute1         VARCHAR2(40),
    CONSTRUCTOR FUNCTION type_RPTOPQTY (
        MOCode              IN  VARCHAR2    DEFAULT ' ',
        ShiftDay            IN  NUMBER      DEFAULT 0,
        ItemCode            IN  VARCHAR2    DEFAULT ' ',
        MESEntity_Serial    IN  NUMBER      DEFAULT 0,
        InputTimes          IN  NUMBER      DEFAULT 0,
        OutputTimes         IN  NUMBER      DEFAULT 0,
        NGTimes             IN  NUMBER      DEFAULT 0,
        EAttribute1         IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT,
    MEMBER PROCEDURE ADDNew
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE "TYPE_RPTSOQTY"                                                                                                                                                    AS OBJECT (
    MOCode                  VARCHAR2(40),
    ShiftDay                NUMBER,
    ItemCode                VARCHAR2(40),
    MESEntity_Serial        NUMBER,
    MOInputCount            NUMBER,
    MOOutputCount           NUMBER,
    MOLineOutputCount       NUMBER,
    MOWhiteCardCount        NUMBER,
    MOOutputWhiteCardCount  NUMBER,
    LineInputCount          NUMBER,
    LineOutputCount         NUMBER,
    OPCount                 NUMBER,
    OPWhiteCardCount        NUMBER,
    EAttribute1             VARCHAR2(40),
    CONSTRUCTOR FUNCTION type_RPTSOQTY (
        MOCode                  IN  VARCHAR2    DEFAULT ' ',
        ShiftDay                IN  NUMBER      DEFAULT 0,
        ItemCode                IN  VARCHAR2    DEFAULT ' ',
        MESEntity_Serial        IN  NUMBER      DEFAULT 0,
        MOInputCount            IN  NUMBER      DEFAULT 0,
        MOOutputCount           IN  NUMBER      DEFAULT 0,
        MOLineOutputCount       IN  NUMBER      DEFAULT 0,
        MOWhiteCardCount        IN  NUMBER      DEFAULT 0,
        MOOutputWhiteCardCount  IN  NUMBER      DEFAULT 0,
        LineInputCount          IN  NUMBER      DEFAULT 0,
        LineOutputCount         IN  NUMBER      DEFAULT 0,
        OPCount                 IN  NUMBER      DEFAULT 0,
        OPWhiteCardCount        IN  NUMBER      DEFAULT 0,
        EAttribute1             IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT,
    MEMBER PROCEDURE ADDNew
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE "TYPE_SUMMARYLOG"                                                                                                                                                    AS OBJECT (
    SERIAL          NUMBER,
    STARTDATETIME   DATE,
    ENDDATETIME     DATE,
    USEDTIME        NUMBER,
    PROCESSCOUNT    NUMBER,
    RESULT          VARCHAR2(40),
    ERRORMESSAGE    VARCHAR2(500),
    CONSTRUCTOR FUNCTION type_SummaryLog (
        SERIAL          IN  NUMBER      DEFAULT 0,
        STARTDATETIME   IN  DATE        DEFAULT SYSDATE,
        ENDDATETIME     IN  DATE        DEFAULT SYSDATE,
        USEDTIME        IN  NUMBER      DEFAULT 0,
        PROCESSCOUNT    IN  NUMBER      DEFAULT 0,
        RESULT          IN  VARCHAR2    DEFAULT ' ',
        ERRORMESSAGE    IN  VARCHAR2    DEFAULT ' '
    )
        RETURN SELF AS RESULT,
    MEMBER PROCEDURE WRITE,
    MEMBER PROCEDURE ClearHistoryLog
)

/

SHOW ERRORS;

CREATE OR REPLACE type TYPE_VARCHAR_LIST as table of varchar2(2000)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE type_WorkPlan AS OBJECT
(
    BigSSCode         VARCHAR2(40),
    PlanDate          NUMBER(8),
    MOCode            VARCHAR2(40),
    MOSeq             NUMBER(10),
    PlanSeq           NUMBER(10),
    ItemCode          VARCHAR2(40),
    PlanQty           NUMBER(10,2),
    ActQty            NUMBER(10,2),
    MaterialQty       NUMBER(10,2),
    PlanStartTime     NUMBER(6),
    PlanEndTime       NUMBER(6),
    LastReceiveTime   NUMBER(6),
    LastReqTime       NUMBER(6),
    PromiseTime       NUMBER(6),
    ActionStatus      VARCHAR2(40),
    MaterialStatus    VARCHAR2(40),
    MUser             VARCHAR2(40),
    MDate             NUMBER(8),
    MTime             NUMBER(6),
    EAttribute1       VARCHAR2(100),

    CONSTRUCTOR FUNCTION type_WorkPlan (
        BigSSCode         IN VARCHAR2  DEFAULT ' ',
        PlanDate          IN NUMBER    DEFAULT 0,
        MOCode            IN VARCHAR2  DEFAULT ' ',
        MOSeq             IN NUMBER    DEFAULT 0,
        PlanSeq           IN NUMBER    DEFAULT 0,
        ItemCode          IN VARCHAR2  DEFAULT ' ',
        PlanQty           IN NUMBER    DEFAULT 0,
        ActQty            IN NUMBER    DEFAULT 0,
        MaterialQty       IN NUMBER    DEFAULT 0,
        PlanStartTime     IN NUMBER    DEFAULT 0,
        PlanEndTime       IN NUMBER    DEFAULT 0,
        LastReceiveTime   IN NUMBER    DEFAULT 0,
        LastReqTime       IN NUMBER    DEFAULT 0,
        PromiseTime       IN NUMBER    DEFAULT 0,
        ActionStatus      IN VARCHAR2  DEFAULT ' ',
        MaterialStatus    IN VARCHAR2  DEFAULT ' ',
        MUser             IN VARCHAR2  DEFAULT ' ',
        MDate             IN NUMBER    DEFAULT 0,
        MTime             IN NUMBER    DEFAULT 0,
        EAttribute1       IN VARCHAR2  DEFAULT ' '
    )
    RETURN SELF AS RESULT
)

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY type_WorkPlan
AS
    CONSTRUCTOR FUNCTION type_WorkPlan(
        BigSSCode         IN VARCHAR2,
        PlanDate          IN NUMBER,
        MOCode            IN VARCHAR2,
        MOSeq             IN NUMBER,
        PlanSeq           IN NUMBER,
        ItemCode          IN VARCHAR2,
        PlanQty           IN NUMBER,
        ActQty            IN NUMBER,
        MaterialQty       IN NUMBER,
        PlanStartTime     IN NUMBER,
        PlanEndTime       IN NUMBER,
        LastReceiveTime   IN NUMBER,
        LastReqTime       IN NUMBER,
        PromiseTime       IN NUMBER,
        ActionStatus      IN VARCHAR2,
        MaterialStatus    IN VARCHAR2,
        MUser             IN VARCHAR2,
        MDate             IN NUMBER,
        MTime             IN NUMBER,
        EAttribute1       IN VARCHAR2
    )
    RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.BigSSCode := BigSSCode;        
        SELF.PlanDate := PlanDate;
        SELF.MOCode := MOCode;
        SELF.MOSeq := MOSeq;
        SELF.PlanSeq := PlanSeq;
        SELF.ItemCode := ItemCode;        
        SELF.PlanQty := PlanQty;
        SELF.ActQty := ActQty;
        SELF.MaterialQty := MaterialQty;
        SELF.PlanStartTime := PlanStartTime;
        SELF.PlanEndTime := PlanEndTime;
        SELF.LastReceiveTime := LastReceiveTime;
        SELF.LastReqTime := LastReqTime;        
        SELF.PromiseTime := PromiseTime;
        SELF.ActionStatus := ActionStatus;
        SELF.MaterialStatus := MaterialStatus;
        SELF.MUser := MUser;
        SELF.MDate := MDate;
        SELF.MTime := MTime;
        SELF.EAttribute1 := EAttribute1;
        
        RETURN;
    END type_WorkPlan;
END;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY type_ONWIPData
AS
    CONSTRUCTOR FUNCTION type_ONWIPData (
        RCard               IN  VARCHAR2    DEFAULT ' ',
        RCardSeq            IN  NUMBER      DEFAULT 0,
        MOCode              IN  VARCHAR2    DEFAULT ' ',
        ModelCode           IN  VARCHAR2    DEFAULT ' ',
        ItemCode            IN  VARCHAR2    DEFAULT ' ',
        RouteCode           IN  VARCHAR2    DEFAULT ' ',
        OPCode              IN  VARCHAR2    DEFAULT ' ',
        SegCode             IN  VARCHAR2    DEFAULT ' ',
        SSCode              IN  VARCHAR2    DEFAULT ' ',
        ResCode             IN  VARCHAR2    DEFAULT ' ',
        ShiftTypeCode       IN  VARCHAR2    DEFAULT ' ',
        ShiftCode           IN  VARCHAR2    DEFAULT ' ',
        TPCode              IN  VARCHAR2    DEFAULT ' ',
        ShiftDay            IN  NUMBER      DEFAULT 0,
        Action              IN  VARCHAR2    DEFAULT ' ',
        ActionResult        IN  VARCHAR2    DEFAULT ' ',
        NGTimes             IN  NUMBER      DEFAULT 0,
        EAttribute1         IN  VARCHAR2    DEFAULT '',
        Serial              IN  NUMBER      DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.RCard := RCard;
        SELF.RCardSeq := RCardSeq;
        SELF.MOCode := MOCode;
        SELF.ModelCode := ModelCode;        
        SELF.ItemCode := ItemCode;
        SELF.RouteCode := RouteCode;
        SELF.OPCode := OPCode;
        SELF.SegCode := SegCode;
        SELF.SSCode := SSCode;
        SELF.ResCode := ResCode;
        SELF.ShiftTypeCode := ShiftTypeCode;
        SELF.ShiftCode := ShiftCode;
        SELF.TPCode := TPCode;        
        SELF.ShiftDay := ShiftDay;
        SELF.Action := Action;
        SELF.ActionResult := ActionResult;
        SELF.NGTimes := NGTimes;
        SELF.EAttribute1 := EAttribute1;
        SELF.Serial := Serial;
        RETURN;
    END type_ONWIPData;
END;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY productimpl
IS
   STATIC FUNCTION odciaggregateinitialize (sctx IN OUT productimpl)
      RETURN NUMBER
   IS
   BEGIN
      sctx := productimpl (0);
      RETURN odciconst.success;
   END;
   MEMBER FUNCTION odciaggregateiterate (SELF IN OUT productimpl, VALUE IN NUMBER)
      RETURN NUMBER
   IS
   BEGIN
      IF SELF.product_result = 0
      THEN
         SELF.product_result := 1;
      END IF;

      IF VALUE = 0
      THEN
         SELF.product_result := 0;
         RETURN odciconst.success;
      ELSIF VALUE <> 0
      THEN
         SELF.product_result := SELF.product_result * VALUE;
      END IF;

      RETURN odciconst.success;
   END;
   MEMBER FUNCTION odciaggregateterminate (SELF IN productimpl, returnvalue OUT NUMBER, flags IN NUMBER)
      RETURN NUMBER
   IS
   BEGIN
      returnvalue := SELF.product_result;
      RETURN odciconst.success;
   END;
   MEMBER FUNCTION odciaggregatemerge (SELF IN OUT productimpl, ctx2 IN productimpl)
      RETURN NUMBER
   IS
   BEGIN
      RETURN odciconst.success;
   END;
END;

/

SHOW ERRORS;

CREATE SEQUENCE WSCGROUP_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCLANGUAGE_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCMENU_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCMENULANGUAGE_ID_S
  START WITH 41
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCPARAMETER_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCPARAMETERTYPE_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCRELATEDMENU_ID_S
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCROLEELEMENT_ID_S
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCROLEMENU_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCUSER_USERID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCUSERGROUP_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTPROGD_PROGDID
  START WITH 21
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTPROGH_PROGHID
  START WITH 21
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTSO_SOID
  START WITH 41
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTSOHIS_SOHID
  START WITH 41
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTSOLDER_SOLDERID
  START WITH 21
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTSOREQ_REQID
  START WITH 41
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTSYS_SEQ
  START WITH 1
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTVFYLOG_LOGID
  START WITH 41
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLINVINTRANSACTION
  START WITH 1101
  MAXVALUE 9999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLONWIP_TEST
  START WITH 2301
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLINVOUTTRANSACTION
  START WITH 741
  MAXVALUE 9999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLRAWRECEIVE2SAP_POSTSEQ
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE DCTMESSAGE_SERIALNO
  START WITH 8501
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTCUSTOMER_ID
  START WITH 1
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQUENCE_TBLJOBLOG_SERIAL
  START WITH 1081
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLNOTICELINEPAUSE_SERIAL
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLALERTNOTICE_SERIAL
  START WITH 61
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLMAIL_SERIAL
  START WITH 61
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLPALLET2RCARDLOG
  START WITH 561
  MAXVALUE 9999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLCARTON2RCARDLOG
  START WITH 721
  MAXVALUE 9999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLNOTICEERRORCODE_SERIAL
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLNOTICEERROR_SERIAL
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLNOTICEDIRECTPASS_SERIAL
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE EPSMAILTYPE_ID_S
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE EPSMAILFORMAT_ID_S
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE WSCELEMENT_ID_S
  START WITH 21
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SMTLOGSEQUENCE
  START WITH 16
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  NOCACHE
  NOORDER;

CREATE SEQUENCE SMTINNOSEQUENCE
  START WITH 4
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  NOCACHE
  NOORDER;

CREATE SEQUENCE PUB_SEQ_ID
  START WITH 161
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SPCSEQUENCE
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  NOCACHE
  NOORDER;

CREATE SEQUENCE SEQ_TBLMESENTITYLIST
  START WITH 1081
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLRPTSUMMARYLOG
  START WITH 181
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLONWIP
  START WITH 12205
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SCCDEPT_ID
  START WITH 61
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SCCFGRP_ID
  START WITH 1
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SCCFPN_FOID
  START WITH 41
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SCCLOG_ID
  START WITH 521
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SCCFUNC_ID
  START WITH 1
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTLINE_LINEID
  START WITH 21
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTMACH_MACHID
  START WITH 21
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTPCBA_PCBAID
  START WITH 21
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_SMTPN_PARTID
  START WITH 21
  MAXVALUE 99999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLINDIRECTMANCOUNT_SERIAL
  START WITH 1
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLLINE2MANDETAIL_SERIAL
  START WITH 2001
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLPRODDETAIL_SERIAL
  START WITH 1001
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLLINEPAUSE_SERIAL
  START WITH 201
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLEXCEPTION_SERIAL
  START WITH 401
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLALERTMAILSETTING_SERIAL
  START WITH 81
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE SEQUENCE SEQ_TBLMATERIALTRANS
  START WITH 341
  MAXVALUE 999999999999999999999999999
  MINVALUE 1
  NOCYCLE
  CACHE 20
  NOORDER;

CREATE TABLE TT5
(
  A1  DATE
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SMTPROGD
(
  PROGDID    NUMBER(10)                         NOT NULL,
  PROGHID    NUMBER(10)                         NOT NULL,
  MACHNAME   NVARCHAR2(20)                      NOT NULL,
  SLOT       NVARCHAR2(20)                      NOT NULL,
  PARTNO     NVARCHAR2(100)                     NOT NULL,
  ISREPLACE  NVARCHAR2(1)                       NOT NULL,
  ITEMDESC   NVARCHAR2(255),
  ITEMQTY    NUMBER(10)                         NOT NULL,
  ACTIVE     NVARCHAR2(1)                       NOT NULL,
  FLAG       NVARCHAR2(1)                       NOT NULL,
  LMUSER     NVARCHAR2(20),
  LMDATE     NUMBER(10)                         NOT NULL,
  LMTIME     NUMBER(10)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTPROGD.PROGDID IS 'ProgramD ID';

COMMENT ON COLUMN SMTPROGD.PROGHID IS 'ProgramH ID';

COMMENT ON COLUMN SMTPROGD.MACHNAME IS 'Machine ID';

COMMENT ON COLUMN SMTPROGD.SLOT IS 'Slot ID';

COMMENT ON COLUMN SMTPROGD.PARTNO IS 'Part No in Program';

COMMENT ON COLUMN SMTPROGD.ISREPLACE IS '???????Y/N)';

COMMENT ON COLUMN SMTPROGD.ITEMDESC IS 'Item Description';

COMMENT ON COLUMN SMTPROGD.ITEMQTY IS 'Item Quantity';

COMMENT ON COLUMN SMTPROGD.ACTIVE IS '?????Y/N)';

COMMENT ON COLUMN SMTPROGD.FLAG IS '?????A:Auto?
M?Manul?';

COMMENT ON COLUMN SMTPROGD.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTPROGD.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTPROGD.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTPROGD
 ADD PRIMARY KEY
 (PROGDID);

CREATE TABLE SMTPROGH
(
  PROGHID    NUMBER(10)                         NOT NULL,
  PCBAMODEL  NVARCHAR2(100)                     NOT NULL,
  PCBAPN     NVARCHAR2(100)                     NOT NULL,
  LINENAME   NVARCHAR2(20)                      NOT NULL,
  TBFLAG     NVARCHAR2(1)                       DEFAULT ('S')                 NOT NULL,
  ACTIVE     NVARCHAR2(1)                       NOT NULL,
  LMUSER     NVARCHAR2(20),
  LMDATE     NUMBER(10)                         NOT NULL,
  LMTIME     NUMBER(10)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTPROGH.PROGHID IS 'Program ID';

COMMENT ON COLUMN SMTPROGH.PCBAMODEL IS 'Model Name';

COMMENT ON COLUMN SMTPROGH.PCBAPN IS 'Pcba Partno';

COMMENT ON COLUMN SMTPROGH.LINENAME IS 'Line';

COMMENT ON COLUMN SMTPROGH.TBFLAG IS 'T/B ???????A/B?';

COMMENT ON COLUMN SMTPROGH.ACTIVE IS '?????Y/N)';

COMMENT ON COLUMN SMTPROGH.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTPROGH.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTPROGH.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTPROGH
 ADD PRIMARY KEY
 (PROGHID);

CREATE TABLE SMTREEL
(
  REELLOT   NVARCHAR2(100)                      NOT NULL,
  PARTNO    NVARCHAR2(100)                      NOT NULL,
  SO        NVARCHAR2(20)                       NOT NULL,
  QTY       NUMBER(22)                          NOT NULL,
  FQTY      NUMBER(22)                          NOT NULL,
  STATUS    NVARCHAR2(1)                        NOT NULL,
  LMUSER    NVARCHAR2(20),
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL,
  REELTYPE  VARCHAR2(30 BYTE),
  PARSEWAY  VARCHAR2(30 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTREEL.REELLOT IS 'Unique ID of each reel';

COMMENT ON COLUMN SMTREEL.PARTNO IS 'Part No';

COMMENT ON COLUMN SMTREEL.SO IS 'Assign S/O No.';

COMMENT ON COLUMN SMTREEL.QTY IS '?????????';

COMMENT ON COLUMN SMTREEL.FQTY IS '????';

COMMENT ON COLUMN SMTREEL.STATUS IS '??????:N:????/Y:???/F:???';

COMMENT ON COLUMN SMTREEL.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTREEL.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTREEL.LMTIME IS 'Last Miantain Time';

CREATE TABLE SMTSO
(
  SOID       NUMBER(10)                         NOT NULL,
  LINENAME   NVARCHAR2(20)                      NOT NULL,
  SO         NVARCHAR2(20)                      NOT NULL,
  PCBAMODEL  NVARCHAR2(100)                     NOT NULL,
  PCBAPN     NVARCHAR2(100)                     NOT NULL,
  SOQTY      NUMBER(10)                         NOT NULL,
  PSDATE     NUMBER(10)                         NOT NULL,
  PEDATE     NUMBER(10)                         NOT NULL,
  STATUS     NVARCHAR2(1)                       DEFAULT ('P')                 NOT NULL,
  TBFLAG     NVARCHAR2(1)                       DEFAULT ('S')                 NOT NULL,
  PRGFLAG    NVARCHAR2(1)                       DEFAULT ('N')                 NOT NULL,
  LMUSER     NVARCHAR2(20),
  LMDATE     NUMBER(10)                         NOT NULL,
  LMTIME     NUMBER(10)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTSO.SOID IS 'So ID';

COMMENT ON COLUMN SMTSO.LINENAME IS 'Line';

COMMENT ON COLUMN SMTSO.SO IS 'S/O no.';

COMMENT ON COLUMN SMTSO.PCBAMODEL IS 'Model Name';

COMMENT ON COLUMN SMTSO.PCBAPN IS 'Part No';

COMMENT ON COLUMN SMTSO.SOQTY IS '????';

COMMENT ON COLUMN SMTSO.PSDATE IS 'Plan Start date';

COMMENT ON COLUMN SMTSO.PEDATE IS 'Plan End date';

COMMENT ON COLUMN SMTSO.STATUS IS 'Status "P":Plan "R":Running "C":Close';

COMMENT ON COLUMN SMTSO.TBFLAG IS 'TopBottom?A,B?';

COMMENT ON COLUMN SMTSO.PRGFLAG IS '????????Y?N?';

COMMENT ON COLUMN SMTSO.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTSO.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTSO.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTSO
 ADD PRIMARY KEY
 (SOID);

CREATE TABLE SMTSOHIS
(
  SOHID   NUMBER(10)                            NOT NULL,
  SOID    NVARCHAR2(20)                         NOT NULL,
  STATUS  NVARCHAR2(1)                          NOT NULL,
  LMUSER  NVARCHAR2(20),
  LMDATE  NUMBER(10)                            NOT NULL,
  LMTIME  NUMBER(10)                            NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTSOHIS.SOHID IS 'SoH ID';

COMMENT ON COLUMN SMTSOHIS.SOID IS 'So ID';

COMMENT ON COLUMN SMTSOHIS.STATUS IS 'Status
"R":Running "C":Close';

COMMENT ON COLUMN SMTSOHIS.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTSOHIS.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTSOHIS.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTSOHIS
 ADD PRIMARY KEY
 (SOHID);

CREATE TABLE SMTSOLDER
(
  SOLDERID    NUMBER(10)                        NOT NULL,
  SO          NVARCHAR2(20)                     NOT NULL,
  SOLDERLOT   NVARCHAR2(100)                    NOT NULL,
  SOLDERTYPE  NVARCHAR2(1)                      DEFAULT ('S')                 NOT NULL,
  LMUSER      NVARCHAR2(20),
  LMDATE      NUMBER(10)                        NOT NULL,
  LMTIME      NUMBER(10)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE SMTSOLDER
 ADD PRIMARY KEY
 (SOLDERID);

CREATE TABLE SMTSOREQ
(
  REQID      NUMBER(10)                         NOT NULL,
  SOID       NVARCHAR2(20)                      NOT NULL,
  MACHNAME   NVARCHAR2(20)                      NOT NULL,
  SLOT       NVARCHAR2(20)                      NOT NULL,
  PARTNO     NVARCHAR2(100)                     NOT NULL,
  ACTIVE     NVARCHAR2(1)                       NOT NULL,
  ISREPLACE  NVARCHAR2(1)                       NOT NULL,
  FLAG       NVARCHAR2(1)                       NOT NULL,
  LMUSER     NVARCHAR2(20),
  LMDATE     NUMBER(10)                         NOT NULL,
  LMTIME     NUMBER(10)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTSOREQ.REQID IS 'Req ID';

COMMENT ON COLUMN SMTSOREQ.SOID IS 'So ID';

COMMENT ON COLUMN SMTSOREQ.MACHNAME IS 'Machine ID';

COMMENT ON COLUMN SMTSOREQ.SLOT IS 'Slot ID';

COMMENT ON COLUMN SMTSOREQ.PARTNO IS 'Part No in Program';

COMMENT ON COLUMN SMTSOREQ.ACTIVE IS '?????Y/N)';

COMMENT ON COLUMN SMTSOREQ.ISREPLACE IS '???????Y/N)';

COMMENT ON COLUMN SMTSOREQ.FLAG IS '?????A:Auto?
M?Manul?';

COMMENT ON COLUMN SMTSOREQ.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTSOREQ.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTSOREQ.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTSOREQ
 ADD PRIMARY KEY
 (REQID);

CREATE TABLE SMTSYS
(
  SEQ          NUMBER(10)                       NOT NULL,
  PARATYPE     NVARCHAR2(20)                    NOT NULL,
  TERM         NVARCHAR2(50)                    NOT NULL,
  DESCRIPTION  NVARCHAR2(20)                    NOT NULL,
  VALUE1       NVARCHAR2(30)                    NOT NULL,
  VALUE2       NVARCHAR2(30)                    NOT NULL,
  VALUE3       NVARCHAR2(30)                    NOT NULL,
  VALUE4       NVARCHAR2(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTSYS.SEQ IS 'seq';

COMMENT ON COLUMN SMTSYS.PARATYPE IS 'Paremeter Type';

COMMENT ON COLUMN SMTSYS.TERM IS 'Paremeter Term';

COMMENT ON COLUMN SMTSYS.DESCRIPTION IS 'description';

COMMENT ON COLUMN SMTSYS.VALUE1 IS 'Paremeter value';

COMMENT ON COLUMN SMTSYS.VALUE2 IS 'Paremeter value';

COMMENT ON COLUMN SMTSYS.VALUE3 IS 'Paremeter value';

ALTER TABLE SMTSYS
 ADD PRIMARY KEY
 (SEQ);

CREATE TABLE SMTVFYLOG
(
  LOGID      NUMBER(10)                         NOT NULL,
  LINENAME   NVARCHAR2(20)                      NOT NULL,
  SO         NVARCHAR2(20)                      NOT NULL,
  PCBAMODEL  NVARCHAR2(100)                     NOT NULL,
  PCBAPN     NVARCHAR2(100)                     NOT NULL,
  TBFLAG     NVARCHAR2(1)                       DEFAULT ('N')                 NOT NULL,
  MACHNAME   NVARCHAR2(20)                      NOT NULL,
  SLOT       NVARCHAR2(20)                      NOT NULL,
  PARTNO     NVARCHAR2(100)                     NOT NULL,
  REELLOT    NVARCHAR2(100),
  REELQTY    NUMBER(10)                         NOT NULL,
  RETURNQTY  NUMBER(10)                         NOT NULL,
  TYPE       NVARCHAR2(1)                       NOT NULL,
  STATUS     NVARCHAR2(1)                       NOT NULL,
  LMUSER     NVARCHAR2(20),
  LMDATE     NUMBER(10)                         NOT NULL,
  LMTIME     NUMBER(10)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTVFYLOG.LOGID IS 'LogID';

COMMENT ON COLUMN SMTVFYLOG.LINENAME IS 'Line';

COMMENT ON COLUMN SMTVFYLOG.SO IS 'S/O no.';

COMMENT ON COLUMN SMTVFYLOG.PCBAMODEL IS 'Model';

COMMENT ON COLUMN SMTVFYLOG.PCBAPN IS 'Part No';

COMMENT ON COLUMN SMTVFYLOG.TBFLAG IS 'TopBottom?A,B?';

COMMENT ON COLUMN SMTVFYLOG.MACHNAME IS 'Machine ID';

COMMENT ON COLUMN SMTVFYLOG.SLOT IS 'Slot ID';

COMMENT ON COLUMN SMTVFYLOG.PARTNO IS 'Part No in Program';

COMMENT ON COLUMN SMTVFYLOG.REELLOT IS 'Realid???Lot';

COMMENT ON COLUMN SMTVFYLOG.REELQTY IS '????';

COMMENT ON COLUMN SMTVFYLOG.RETURNQTY IS '????';

COMMENT ON COLUMN SMTVFYLOG.TYPE IS 'Type:"F"or"R" or?C? "F":First Time "R":Refill "C":Recheck';

COMMENT ON COLUMN SMTVFYLOG.STATUS IS 'Status "C":Complete "E":Error';

COMMENT ON COLUMN SMTVFYLOG.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTVFYLOG.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTVFYLOG.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTVFYLOG
 ADD PRIMARY KEY
 (LOGID);

CREATE TABLE SQLN_EXPLAIN_PLAN
(
  STATEMENT_ID     VARCHAR2(30 BYTE),
  TIMESTAMP        DATE,
  REMARKS          VARCHAR2(80 BYTE),
  OPERATION        VARCHAR2(30 BYTE),
  OPTIONS          VARCHAR2(30 BYTE),
  OBJECT_NODE      VARCHAR2(128 BYTE),
  OBJECT_OWNER     VARCHAR2(30 BYTE),
  OBJECT_NAME      VARCHAR2(30 BYTE),
  OBJECT_INSTANCE  INTEGER,
  OBJECT_TYPE      VARCHAR2(30 BYTE),
  OPTIMIZER        VARCHAR2(255 BYTE),
  SEARCH_COLUMNS   INTEGER,
  ID               INTEGER,
  PARENT_ID        INTEGER,
  POSITION         INTEGER,
  COST             INTEGER,
  CARDINALITY      INTEGER,
  BYTES            INTEGER,
  OTHER_TAG        VARCHAR2(255 BYTE),
  PARTITION_START  VARCHAR2(255 BYTE),
  PARTITION_STOP   VARCHAR2(255 BYTE),
  PARTITION_ID     INTEGER,
  OTHER            LONG,
  DISTRIBUTION     VARCHAR2(30 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SRMX
(
  SONO    NUMBER(12)                            NOT NULL,
  FINQTY  NUMBER(15,5)                          NOT NULL,
  SRMXNO  CHAR(12 BYTE)                         NOT NULL,
  "uid"   CHAR(10 BYTE)                         NOT NULL,
  ENDATE  NUMBER(8)                             NOT NULL,
  STA     CHAR(1 BYTE)                          NOT NULL,
  SRPROD  CHAR(12 BYTE)                         NOT NULL,
  SRDESC  CHAR(30 BYTE)                         NOT NULL,
  SRFXD   CHAR(12 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE SRMX
 ADD PRIMARY KEY
 (SRMXNO, SONO);

CREATE TABLE TBLALERT
(
  ALERTID      NUMBER(10)                       NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE),
  ALERTTYPE    VARCHAR2(40 BYTE)                NOT NULL,
  ALERTITEM    VARCHAR2(40 BYTE),
  ALERTDATE    NUMBER(8)                        NOT NULL,
  ALERTTIME    NUMBER(6)                        NOT NULL,
  ALERTVALUE   NUMBER(15,5)                     NOT NULL,
  SENDUSER     VARCHAR2(40 BYTE)                NOT NULL,
  ALERTLEVEL   VARCHAR2(40 BYTE),
  ALERTSTATUS  VARCHAR2(40 BYTE),
  ALERTMSG     VARCHAR2(1000 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  MAILNOTIFY   VARCHAR2(1 BYTE)                 NOT NULL,
  VALIDDATE    NUMBER(8)                        NOT NULL,
  ALERTDESC    VARCHAR2(1000 BYTE),
  PRODUCTCODE  VARCHAR2(40 BYTE),
  SSCODE       VARCHAR2(40 BYTE),
  SHIFTCODE    VARCHAR2(40 BYTE),
  SHIFTTIME    NUMBER(8),
  BILLID       NUMBER(9)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK371 ON TBLALERT
(ALERTID)
LOGGING
NOPARALLEL;

CREATE INDEX INDALERT03 ON TBLALERT
(PRODUCTCODE, SSCODE)
LOGGING
NOPARALLEL;

CREATE INDEX IND02_TBLALERT ON TBLALERT
(ALERTDATE, ALERTSTATUS)
LOGGING
NOPARALLEL;

CREATE INDEX IND01_TBLALERT ON TBLALERT
(ALERTTYPE, ALERTITEM, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERT
 ADD CONSTRAINT PK371
 PRIMARY KEY
 (ALERTID);

CREATE TABLE TBLALERTBILL
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ALERTTYPE    VARCHAR2(40 BYTE)                NOT NULL,
  ALERTITEM    VARCHAR2(40 BYTE)                NOT NULL,
  STARTNUM     NUMBER(10)                       NOT NULL,
  OP           VARCHAR2(40 BYTE)                NOT NULL,
  LOWVALUE     NUMBER(15,5)                     NOT NULL,
  UPVALUE      NUMBER(15,5)                     NOT NULL,
  VALIDDATE    NUMBER(8)                        NOT NULL,
  MAILNOTIFY   VARCHAR2(1 BYTE)                 NOT NULL,
  ALERTMSG     VARCHAR2(1000 BYTE),
  ALERTDESC    VARCHAR2(1000 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  BILLID       NUMBER(9),
  PRODUCTCODE  VARCHAR2(40 BYTE)                DEFAULT ''
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLALERTBILL
 ADD PRIMARY KEY
 (BILLID);

CREATE TABLE TBLALERTHANDLELOG
(
  ALERTID      NUMBER(10)                       NOT NULL,
  HANDLESEQ    NUMBER(10)                       NOT NULL,
  ALERTLEVEL   VARCHAR2(40 BYTE)                NOT NULL,
  ALERTSTATUS  VARCHAR2(40 BYTE)                NOT NULL,
  HANDLEMSG    VARCHAR2(1000 BYTE),
  HANDLEUSER   VARCHAR2(40 BYTE)                NOT NULL,
  USEREMAIL    VARCHAR2(40 BYTE),
  HANDLEDATE   NUMBER(8)                        NOT NULL,
  HANDLETIME   NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK372 ON TBLALERTHANDLELOG
(ALERTID, HANDLESEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTHANDLELOG
 ADD CONSTRAINT PK372
 PRIMARY KEY
 (ALERTID, HANDLESEQ);

CREATE TABLE TBLALERTMAILLOG
(
  ALERTID   NUMBER(10)                          NOT NULL,
  SEQ       NUMBER(10)                          NOT NULL,
  USERCODE  VARCHAR2(40 BYTE)                   NOT NULL,
  USERMAIL  VARCHAR2(40 BYTE)                   NOT NULL,
  SENDDATE  NUMBER(8)                           NOT NULL,
  SENDTIME  NUMBER(6)                           NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK382 ON TBLALERTMAILLOG
(ALERTID, SEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTMAILLOG
 ADD CONSTRAINT PK382
 PRIMARY KEY
 (ALERTID, SEQ);

CREATE TABLE TBLALERTMANUALNOTIFIER
(
  ALERTID   NUMBER(10)                          NOT NULL,
  USERCODE  VARCHAR2(40 BYTE)                   NOT NULL,
  USERMAIL  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK376 ON TBLALERTMANUALNOTIFIER
(ALERTID, USERCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTMANUALNOTIFIER
 ADD CONSTRAINT PK376
 PRIMARY KEY
 (ALERTID, USERCODE);

CREATE TABLE TBLINVBUSINESS
(
  BUSINESSCODE    VARCHAR2(40 BYTE)             NOT NULL,
  BUSINESSDESC    VARCHAR2(100 BYTE)            NOT NULL,
  BUSINESSTYPE    VARCHAR2(40 BYTE)             NOT NULL,
  BUSINESSREASON  VARCHAR2(40 BYTE),
  ORGID           NUMBER(8)                     NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLINVBUSINESS_DESC ON TBLINVBUSINESS
(BUSINESSDESC)
LOGGING
NOPARALLEL;

ALTER TABLE TBLINVBUSINESS
 ADD PRIMARY KEY
 (BUSINESSCODE, ORGID);

CREATE TABLE TBLINVBUSINESS2FORMULA
(
  BUSINESSCODE  VARCHAR2(40 BYTE)               NOT NULL,
  FORMULACODE   VARCHAR2(100 BYTE)              NOT NULL,
  ORGID         NUMBER(8)                       NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLINVBUSINESS2FORMULA
 ADD PRIMARY KEY
 (BUSINESSCODE, FORMULACODE, ORGID);

CREATE TABLE TBLINVFORMULA
(
  FORMULACODE  VARCHAR2(40 BYTE)                NOT NULL,
  FORMULADESC  VARCHAR2(100 BYTE)               NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLINVFORMULA
 ADD PRIMARY KEY
 (FORMULACODE);

CREATE TABLE TBLINVINTRANSACTION
(
  TRANSCODE        VARCHAR2(40 BYTE),
  RCARD            VARCHAR2(40 BYTE),
  CARTONCODE       VARCHAR2(100 BYTE),
  PALLETCODE       VARCHAR2(40 BYTE),
  ITEMCODE         VARCHAR2(40 BYTE),
  MOCODE           VARCHAR2(40 BYTE),
  BUSINESSCODE     VARCHAR2(40 BYTE)            NOT NULL,
  STACKCODE        VARCHAR2(40 BYTE)            NOT NULL,
  STORAGECODE      VARCHAR2(40 BYTE)            NOT NULL,
  ITEMGRADE        VARCHAR2(40 BYTE)            NOT NULL,
  SSCODE           VARCHAR2(40 BYTE),
  COMPANY          VARCHAR2(100 BYTE)           NOT NULL,
  BUSINESSREASON   VARCHAR2(40 BYTE)            NOT NULL,
  SERIAL           NUMBER(10)                   NOT NULL,
  ORGID            NUMBER(8)                    NOT NULL,
  DELIVERUSER      VARCHAR2(40 BYTE),
  MEMO             VARCHAR2(100 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  RELATEDDOCUMENT  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLINVINTRANSACTION
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLINVOUTTRANSACTION
(
  TRANSCODE       VARCHAR2(40 BYTE),
  RCARD           VARCHAR2(40 BYTE),
  CARTONCODE      VARCHAR2(100 BYTE),
  PALLETCODE      VARCHAR2(40 BYTE),
  ITEMCODE        VARCHAR2(40 BYTE),
  MOCODE          VARCHAR2(40 BYTE),
  BUSINESSCODE    VARCHAR2(40 BYTE)             NOT NULL,
  STACKCODE       VARCHAR2(40 BYTE)             NOT NULL,
  STORAGECODE     VARCHAR2(40 BYTE)             NOT NULL,
  ITEMGRADE       VARCHAR2(40 BYTE)             NOT NULL,
  COMPANY         VARCHAR2(100 BYTE)            NOT NULL,
  BUSINESSREASON  VARCHAR2(40 BYTE)             NOT NULL,
  SERIAL          NUMBER(10)                    NOT NULL,
  ORGID           NUMBER(8)                     NOT NULL,
  MEMO            VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  DNLINE          VARCHAR2(6 BYTE)              NOT NULL,
  TRANSINSERIAL   NUMBER(22)                    DEFAULT 0                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLINVOUTTRANSACTION
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLINVPERIOD
(
  INVPERIODCODE  VARCHAR2(40 BYTE)              NOT NULL,
  DATEFROM       NUMBER(8)                      NOT NULL,
  DATETO         NUMBER(8)                      NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  PEIODGROUP     VARCHAR2(40 BYTE)              NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLINVPERIOD_DATE ON TBLINVPERIOD
(DATEFROM, DATETO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLINVPERIOD
 ADD PRIMARY KEY
 (INVPERIODCODE, PEIODGROUP);

CREATE TABLE TBLINVRCARD
(
  RCARD            VARCHAR2(40 BYTE)            NOT NULL,
  RECNO            VARCHAR2(40 BYTE)            NOT NULL,
  RECSEQ           NUMBER(10),
  SHIPNO           VARCHAR2(40 BYTE),
  SHIPSEQ          NUMBER(10)                   NOT NULL,
  RCARDSTATUS      VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  MOCODE           VARCHAR2(40 BYTE),
  RECDATE          NUMBER(8)                    NOT NULL,
  RECTIME          NUMBER(6)                    NOT NULL,
  RECUSER          VARCHAR2(40 BYTE)            NOT NULL,
  SHIPDATE         NUMBER(8),
  SHIPTIME         NUMBER(6),
  SHIPUSER         VARCHAR2(40 BYTE),
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  RECCOLLECTTYPE   VARCHAR2(40 BYTE),
  SHIPCOLLECTTYPE  VARCHAR2(40 BYTE),
  ORDERNUMBER      VARCHAR2(40 BYTE),
  CARTONCODE       VARCHAR2(40 BYTE),
  INVRARDTYPE      VARCHAR2(40 BYTE),
  MOSEQ            NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK397_1 ON TBLINVRCARD
(RCARD, RECNO)
LOGGING
NOPARALLEL;

CREATE INDEX INVRCARD_CARTON2 ON TBLINVRCARD
(CARTONCODE, SHIPNO)
LOGGING
NOPARALLEL;

CREATE INDEX INVRCARD_CARTON1 ON TBLINVRCARD
(CARTONCODE, RECNO)
LOGGING
NOPARALLEL;

CREATE INDEX INDINVRCARD009 ON TBLINVRCARD
(RCARD, RCARDSTATUS)
LOGGING
NOPARALLEL;

CREATE INDEX INDINVRCARD008 ON TBLINVRCARD
(RCARD, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDINVRCARD007 ON TBLINVRCARD
(RECNO, RECDATE, RCARD, RECSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX INDINVRCARD006 ON TBLINVRCARD
(SHIPNO, RCARD, SHIPSEQ, SHIPDATE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLINVRCARD
 ADD CONSTRAINT PK397_1
 PRIMARY KEY
 (RCARD, RECNO);

CREATE TABLE TBLINVREC
(
  RECNO      VARCHAR2(40 BYTE)                  NOT NULL,
  RECSEQ     NUMBER(10)                         NOT NULL,
  RECSTATUS  VARCHAR2(40 BYTE)                  NOT NULL,
  RECTYPE    VARCHAR2(40 BYTE)                  NOT NULL,
  INNERTYPE  VARCHAR2(40 BYTE)                  NOT NULL,
  MODELCODE  VARCHAR2(40 BYTE)                  NOT NULL,
  ITEMCODE   VARCHAR2(40 BYTE)                  NOT NULL,
  ITEMDESC   VARCHAR2(100 BYTE),
  PLANQTY    NUMBER(10)                         NOT NULL,
  ACTQTY     NUMBER(10)                         NOT NULL,
  RECDATE    NUMBER(8)                          NOT NULL,
  RECTIME    NUMBER(6)                          NOT NULL,
  RECUSER    VARCHAR2(40 BYTE),
  RECDESC    VARCHAR2(100 BYTE),
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MOCODE     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK405 ON TBLINVREC
(RECNO, RECSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX INDINVREC01 ON TBLINVREC
(RECNO, ITEMCODE, MODELCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLINVREC
 ADD CONSTRAINT PK405
 PRIMARY KEY
 (RECNO, RECSEQ);

CREATE TABLE TBLINVSHIP
(
  SHIPNO           VARCHAR2(40 BYTE)            NOT NULL,
  SHIPSEQ          NUMBER(10)                   NOT NULL,
  SHIPINNERTYPE    VARCHAR2(40 BYTE),
  SHIPTYPE         VARCHAR2(40 BYTE)            NOT NULL,
  PARTNERCODE      VARCHAR2(40 BYTE),
  PARTNERDESC      VARCHAR2(100 BYTE),
  SHIPDATE         NUMBER(8)                    NOT NULL,
  SHIPTIME         NUMBER(6)                    NOT NULL,
  SHIPUSER         VARCHAR2(40 BYTE),
  MODELCODE        VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  ITEMDESC         VARCHAR2(100 BYTE),
  SHIPSTATUS       VARCHAR2(40 BYTE)            NOT NULL,
  PLANQTY          NUMBER(10)                   NOT NULL,
  ACTQTY           NUMBER(10)                   NOT NULL,
  SHIPDESC         VARCHAR2(100 BYTE),
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  PRINTDATE        NUMBER(8),
  CUSTOMERORDERNO  VARCHAR2(40 BYTE),
  SHIPMETHOD       VARCHAR2(40 BYTE),
  MOCODE           VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX TBLINVSHIP ON TBLINVSHIP
(SHIPNO, SHIPSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX INDINVSHIP01 ON TBLINVSHIP
(SHIPNO, ITEMCODE, PARTNERCODE, MODELCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLINVSHIP
 ADD CONSTRAINT TBLINVSHIP
 PRIMARY KEY
 (SHIPNO, SHIPSEQ);

CREATE TABLE TBLIQCDETAIL
(
  IQCNO             VARCHAR2(50 BYTE)           NOT NULL,
  STNO              VARCHAR2(40 BYTE)           NOT NULL,
  STLINE            NUMBER(6)                   NOT NULL,
  ITEMCODE          VARCHAR2(40 BYTE)           NOT NULL,
  ORDERNO           VARCHAR2(40 BYTE),
  ORDERLINE         NUMBER(22),
  PLANDATE          NUMBER(8)                   NOT NULL,
  PLANQTY           NUMBER(18,5)                NOT NULL,
  STDSTATUS         VARCHAR2(40 BYTE)           NOT NULL,
  RECEIVEQTY        NUMBER(18,5),
  CHECKSTATUS       VARCHAR2(40 BYTE),
  UNIT              VARCHAR2(40 BYTE),
  MEMO              VARCHAR2(2000 BYTE),
  MEMOEX            VARCHAR2(1000 BYTE),
  SAMPLEQTY         NUMBER(18,5),
  NGQTY             NUMBER(18,5),
  PIC               VARCHAR2(100 BYTE),
  ACTION            VARCHAR2(1000 BYTE),
  MDATE             NUMBER(8)                   NOT NULL,
  MTIME             NUMBER(6)                   NOT NULL,
  MUSER             VARCHAR2(40 BYTE)           NOT NULL,
  ATTRIBUTE         VARCHAR2(40 BYTE),
  TYPE              VARCHAR2(40 BYTE),
  ORGID             NUMBER(8)                   NOT NULL,
  STORAGEID         VARCHAR2(4 BYTE)            NOT NULL,
  CONCESSIONSTATUS  VARCHAR2(15 BYTE)           DEFAULT 'N'                   NOT NULL,
  CONCESSIONQTY     NUMBER(18,5),
  CONCESSIONNO      VARCHAR2(100 BYTE),
  CONCESSIONMEMO    VARCHAR2(1000 BYTE),
  SRMFLAG           VARCHAR2(10 BYTE),
  SRMERRORMSG       VARCHAR2(2000 BYTE),
  DINSPECTOR        VARCHAR2(100 BYTE),
  DINSPDATE         NUMBER(8),
  DINSPTIME         NUMBER(8)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLIQCDETAIL
 ADD PRIMARY KEY
 (IQCNO, STLINE);

CREATE TABLE TBLITEM
(
  ITEMCODE                 VARCHAR2(40 BYTE)    NOT NULL,
  ITEMNAME                 VARCHAR2(100 BYTE),
  ITEMDESC                 VARCHAR2(100 BYTE),
  ITEMUOM                  VARCHAR2(40 BYTE),
  ITEMVER                  VARCHAR2(40 BYTE),
  ITEMTYPE                 VARCHAR2(40 BYTE)    NOT NULL,
  ITEMCONTROL              VARCHAR2(40 BYTE),
  ITEMUSER                 VARCHAR2(40 BYTE)    NOT NULL,
  ITEMDATE                 NUMBER(8)            NOT NULL,
  MUSER                    VARCHAR2(40 BYTE)    NOT NULL,
  MDATE                    NUMBER(8)            NOT NULL,
  MTIME                    NUMBER(6)            NOT NULL,
  EATTRIBUTE1              VARCHAR2(40 BYTE),
  ITEMCONFIG               VARCHAR2(40 BYTE),
  ITEMCARTONQTY            VARCHAR2(40 BYTE),
  ITEMBURNINQTY            NUMBER(10),
  ELECTRICCURRENTMINVALUE  NUMBER(15,5),
  ELECTRICCURRENTMAXVALUE  NUMBER(15,5),
  SHIPBOXCAPACITY          NUMBER(10),
  PKRULECODE               VARCHAR2(40 BYTE),
  ORGID                    NUMBER(8)            DEFAULT -1                    NOT NULL,
  CHKITEMOP                VARCHAR2(40 BYTE),
  LOTSIZE                  NUMBER(8),
  PRODUCTCODE              VARCHAR2(100 BYTE),
  NEEDCHKCARTON            VARCHAR2(40 BYTE),
  NEEDCHKACCESSORY         VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLITEM
 ADD PRIMARY KEY
 (ITEMCODE, ORGID);

CREATE TABLE TBLITEM2CONFIG
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCONFIG   VARCHAR2(40 BYTE)                NOT NULL,
  PARENTCODE   VARCHAR2(40 BYTE)                NOT NULL,
  CONFIGCODE   VARCHAR2(40 BYTE)                NOT NULL,
  CONFIGVALUE  VARCHAR2(40 BYTE)                NOT NULL,
  PARENTNAME   VARCHAR2(40 BYTE)                NOT NULL,
  CONFIGNAME   VARCHAR2(40 BYTE)                NOT NULL,
  NEEDCHECK    VARCHAR2(1 BYTE)                 NOT NULL,
  LEVELCODE    NUMBER(10)                       NOT NULL,
  ISLEAF       VARCHAR2(1 BYTE)                 NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLITEM2CONFIG
 ADD PRIMARY KEY
 (ITEMCODE, ITEMCONFIG, PARENTCODE, CONFIGCODE, CONFIGVALUE, ORGID);

CREATE TABLE TBLITEM2DIM
(
  ITEMCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  PARAMNAME   VARCHAR2(40 BYTE)                 NOT NULL,
  PARAMVALUE  NUMBER(15,5)                      NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  ORGID       NUMBER(8)                         DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLITEM2DIM
 ADD PRIMARY KEY
 (ITEMCODE, PARAMNAME, ORGID);

CREATE TABLE TBLITEM2OQCCKLIST
(
  CKITEMCODE   VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SEQ          NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLITEM2OQCCKLIST
 ADD PRIMARY KEY
 (CKITEMCODE, ITEMCODE, ORGID);

CREATE TABLE TBLITEM2ROUTE
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  ISREF        VARCHAR2(1 BYTE)                 NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ROUTETYPE    VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT 0                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK252_1 ON TBLITEM2ROUTE
(ITEMCODE, ROUTECODE, ORGID)
LOGGING
NOPARALLEL;

CREATE INDEX REF266614 ON TBLITEM2ROUTE
(ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLITEM2ROUTE
 ADD CONSTRAINT PK252_1
 PRIMARY KEY
 (ITEMCODE, ROUTECODE, ORGID);

CREATE TABLE TBLITEM2SNCHECK
(
  ITEMCODE        VARCHAR2(40 BYTE),
  SNPREFIX        VARCHAR2(40 BYTE),
  SNLENGTH        NUMBER(6),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  SNCONTENTCHECK  VARCHAR2(40 BYTE),
  TYPE            VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLITEM2SNCHECK ON TBLITEM2SNCHECK
(SNPREFIX, SNLENGTH)
LOGGING
NOPARALLEL;

ALTER TABLE TBLITEM2SNCHECK
 ADD PRIMARY KEY
 (ITEMCODE, TYPE);

CREATE TABLE TBLITEM2SPCTBL
(
  OID          VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SPCTBLNAME   VARCHAR2(40 BYTE)                NOT NULL,
  STARTDATE    NUMBER(8)                        NOT NULL,
  ENDDATE      NUMBER(8)                        NOT NULL,
  SPCDESC      VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLITEM2SPCTBL ON TBLITEM2SPCTBL
(OID)
LOGGING
NOPARALLEL;

CREATE INDEX IND_SPCTBL03 ON TBLITEM2SPCTBL
(ITEMCODE, SPCTBLNAME)
LOGGING
NOPARALLEL;

CREATE INDEX IND_SPCTBL02 ON TBLITEM2SPCTBL
(ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX IND_SPCTBL01 ON TBLITEM2SPCTBL
(ITEMCODE, STARTDATE, ENDDATE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLITEM2SPCTBL
 ADD CONSTRAINT PK_TBLITEM2SPCTBL
 PRIMARY KEY
 (OID);

CREATE TABLE TBLITEM2SPCTEST
(
  OID       VARCHAR2(40 BYTE)                   NOT NULL,
  ITEMCODE  VARCHAR2(40 BYTE)                   NOT NULL,
  TESTNAME  VARCHAR2(40 BYTE)                   NOT NULL,
  SEQ       NUMBER(10)                          NOT NULL,
  AUTOCL    VARCHAR2(1 BYTE)                    NOT NULL,
  LOWONLY   VARCHAR2(1 BYTE)                    NOT NULL,
  UPONLY    VARCHAR2(1 BYTE)                    NOT NULL,
  USL       NUMBER(10,3),
  LSL       NUMBER(10,3),
  UCL       NUMBER(10,3),
  LCL       NUMBER(10,3),
  TESTDESC  VARCHAR2(100 BYTE),
  MUSER     VARCHAR2(40 BYTE)                   NOT NULL,
  MDATE     NUMBER(8)                           NOT NULL,
  MTIME     NUMBER(6)                           NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLITEM2SPCTEST ON TBLITEM2SPCTEST
(OID)
LOGGING
NOPARALLEL;

CREATE INDEX IND_SPCTEST01 ON TBLITEM2SPCTEST
(ITEMCODE, TESTNAME)
LOGGING
NOPARALLEL;

ALTER TABLE TBLITEM2SPCTEST
 ADD CONSTRAINT PK_TBLITEM2SPCTEST
 PRIMARY KEY
 (OID);

CREATE TABLE TBLITEMCLASS
(
  ITEMGROUP    VARCHAR2(40 BYTE)                NOT NULL,
  FIRSTCLASS   VARCHAR2(40 BYTE)                NOT NULL,
  SECONDCLASS  VARCHAR2(40 BYTE),
  THIRDCLASS   VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLITEMCLASS
 ADD PRIMARY KEY
 (ITEMGROUP);

CREATE TABLE TBLITEMLOCATION
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  AB           VARCHAR2(40 BYTE)                NOT NULL,
  QTY          NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  SEGCODE      VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLITEMLOCATION
 ADD PRIMARY KEY
 (ITEMCODE, AB, ORGID);

CREATE TABLE TBLITEMOP2OQCCKLIST
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  CKITEMCODE   VARCHAR2(40 BYTE)                NOT NULL,
  SEQ          NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLITEMOP2OQCCKLIST
 ADD PRIMARY KEY
 (ITEMCODE, OPCODE, CKITEMCODE, ORGID);

CREATE TABLE TBLITEMROUTE2OP
(
  OPID         VARCHAR2(100 BYTE)               NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  OPSEQ        NUMBER(10)                       NOT NULL,
  OPCONTROL    VARCHAR2(40 BYTE)                NOT NULL,
  IDMERGETYPE  VARCHAR2(40 BYTE)                NOT NULL,
  IDMERGERULE  NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  OPTIONALOP   VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT 0                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX REF333615 ON TBLITEMROUTE2OP
(ROUTECODE, ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX ITEM2OP_INDEX ON TBLITEMROUTE2OP
(ROUTECODE, OPCODE, ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX IND_ITEMRT2OP_ITEM_ROUTE_OP ON TBLITEMROUTE2OP
(ITEMCODE, ROUTECODE, OPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLITEMROUTE2OP
 ADD PRIMARY KEY
 (OPID, ORGID);

CREATE TABLE TBLITEMROUTEOP2ECSG
(
  OPID         VARCHAR2(40 BYTE)                NOT NULL,
  ECSGCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_ITEMROUTEOP2ECSG ON TBLITEMROUTEOP2ECSG
(OPID, ECSGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLITEMROUTEOP2ECSG
 ADD CONSTRAINT PK_ITEMROUTEOP2ECSG
 PRIMARY KEY
 (OPID, ECSGCODE);

CREATE TABLE TBLLOG
(
  MTIME    VARCHAR2(15 BYTE),
  SQLTEXT  VARCHAR2(1000 BYTE),
  MUSER    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLLOT
(
  LOTNO           VARCHAR2(40 BYTE)             NOT NULL,
  LOTSEQ          NUMBER(10)                    NOT NULL,
  OQCLOTTYPE      VARCHAR2(40 BYTE)             NOT NULL,
  LOTSIZE         NUMBER(10)                    NOT NULL,
  SSIZE           NUMBER(10)                    NOT NULL,
  AQL             NUMBER(20,5)                  NOT NULL,
  AQL1            NUMBER(20,5)                  NOT NULL,
  AQL2            NUMBER(20,5)                  NOT NULL,
  ACCSIZE         NUMBER(10)                    NOT NULL,
  ACCSIZE1        NUMBER(10)                    NOT NULL,
  ACCSIZE2        NUMBER(10)                    NOT NULL,
  RJTSIZE         NUMBER(10)                    NOT NULL,
  RJTSIZE1        NUMBER(10)                    NOT NULL,
  RJTSIZE2        NUMBER(10)                    NOT NULL,
  LOTSTATUS       VARCHAR2(40 BYTE)             NOT NULL,
  LOTTIMES        NUMBER(10)                    NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(100 BYTE),
  DDATE           NUMBER(8),
  DTIME           NUMBER(6),
  DUSER           VARCHAR2(40 BYTE),
  PRODUCTIONTYPE  VARCHAR2(40 BYTE),
  OLDLOTNO        VARCHAR2(40 BYTE),
  AQL3            NUMBER(20,5),
  ACCSIZE3        NUMBER(10),
  RJTSIZE3        NUMBER(10),
  ORGID           NUMBER(8),
  LOTCAPACITY     NUMBER(10),
  LOTFROZEN       VARCHAR2(40 BYTE),
  MEMO            VARCHAR2(100 BYTE),
  CUSER           VARCHAR2(40 BYTE),
  CDATE           NUMBER(8),
  CTIME           NUMBER(6),
  SSCODE          VARCHAR2(40 BYTE),
  ITEMCODE        VARCHAR2(40 BYTE),
  FROZENSTATUS    VARCHAR2(40 BYTE),
  FROZENREASON    VARCHAR2(100 BYTE),
  FROZENDATE      NUMBER(8),
  FROZENTIME      NUMBER(6),
  FROZENBY        VARCHAR2(40 BYTE),
  UNFROZENREASON  VARCHAR2(100 BYTE),
  UNFROZENDATE    NUMBER(8),
  UNFROZENTIME    NUMBER(6),
  UNFROZENBY      VARCHAR2(40 BYTE),
  RESCODE         VARCHAR2(40 BYTE),
  SHIFTDAY        NUMBER(8),
  SHIFTCODE       VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLLOT_LOTNO ON TBLLOT
(LOTNO)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK44_3_1 ON TBLLOT
(LOTNO, LOTSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX LOT_STATUS ON TBLLOT
(LOTSTATUS)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLLOT_MDATE ON TBLLOT
(MDATE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLLOT
 ADD CONSTRAINT PK44_3_1
 PRIMARY KEY
 (LOTNO, LOTSEQ);

CREATE TABLE TBLMENU
(
  MENUCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MDLCODE      VARCHAR2(40 BYTE),
  PMENUCODE    VARCHAR2(40 BYTE),
  MENUDESC     VARCHAR2(100 BYTE),
  MENUSEQ      NUMBER(10)                       NOT NULL,
  MENUTYPE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  VISIBILITY   VARCHAR2(1 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK246 ON TBLMENU
(MENUCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMENU
 ADD CONSTRAINT PK246
 PRIMARY KEY
 (MENUCODE);

CREATE TABLE TBLMENU2
(
  MENUCODE   VARCHAR2(40 BYTE),
  MDLCODE    VARCHAR2(40 BYTE),
  PMENUCODE  VARCHAR2(40 BYTE),
  MENUSEQ    NUMBER(5),
  PMDLCODE   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLMESENTITYLIST
(
  SERIAL         NUMBER(38)                     NOT NULL,
  BIGSSCODE      VARCHAR2(40 BYTE)              NOT NULL,
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  OPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  FACCODE        VARCHAR2(40 BYTE)              NOT NULL,
  ORGID          NUMBER(8)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLMESENTITYLIST ON TBLMESENTITYLIST
(BIGSSCODE, MODELCODE, OPCODE, SEGCODE, SSCODE, 
RESCODE, SHIFTTYPECODE, SHIFTCODE, TPCODE, FACCODE, 
ORGID)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK_TBLMESENTITYLIST ON TBLMESENTITYLIST
(SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMESENTITYLIST
 ADD CONSTRAINT PK_TBLMESENTITYLIST
 PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLMINNO
(
  INNO            VARCHAR2(40 BYTE)             NOT NULL,
  SEQ             NUMBER(10)                    NOT NULL,
  MOBSITEMCODE    VARCHAR2(40 BYTE),
  MITEMCODE       VARCHAR2(40 BYTE),
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE),
  OPBOMCODE       VARCHAR2(40 BYTE)             NOT NULL,
  OPBOMVER        VARCHAR2(40 BYTE)             NOT NULL,
  ROUTECODE       VARCHAR2(40 BYTE)             NOT NULL,
  OPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  RESCODE         VARCHAR2(40 BYTE)             NOT NULL,
  ISTRY           VARCHAR2(40 BYTE)             NOT NULL,
  TRYITEMCODE     VARCHAR2(40 BYTE),
  LOTNO           VARCHAR2(40 BYTE),
  PCBA            VARCHAR2(100 BYTE),
  BIOS            VARCHAR2(100 BYTE),
  VERSION         VARCHAR2(100 BYTE),
  VENDORITEMCODE  VARCHAR2(100 BYTE),
  VENDORCODE      VARCHAR2(100 BYTE),
  DATECODE        VARCHAR2(100 BYTE),
  QTY             NUMBER(15,5)                  NOT NULL,
  ISLAST          VARCHAR2(1 BYTE)              NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  MITEMNAME       VARCHAR2(80 BYTE),
  MITEMPACKEDNO   VARCHAR2(40 BYTE)             NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX UI_FOR_TBLMINNO_PACKEDNO ON TBLMINNO
(MOCODE, ROUTECODE, OPCODE, RESCODE, OPBOMVER, 
MITEMCODE)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK42_2 ON TBLMINNO
(INNO, SEQ)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLMINNO_MOITEMCODE ON TBLMINNO
(MOCODE, ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLMINNO_INNO ON TBLMINNO
(INNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMINNO
 ADD CONSTRAINT PK42_2
 PRIMARY KEY
 (INNO, SEQ);

CREATE TABLE TBLMKEYPART
(
  MITEMCODE       VARCHAR2(40 BYTE)             NOT NULL,
  SEQ             NUMBER(38)                    NOT NULL,
  RCARDSTART      VARCHAR2(40 BYTE)             NOT NULL,
  RCARDEND        VARCHAR2(40 BYTE)             NOT NULL,
  LOTNO           VARCHAR2(40 BYTE),
  PCBA            VARCHAR2(100 BYTE),
  BIOS            VARCHAR2(100 BYTE),
  VERSION         VARCHAR2(100 BYTE),
  VENDORITEMCODE  VARCHAR2(100 BYTE),
  VENDORCODE      VARCHAR2(100 BYTE),
  DATECODE        VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  MOCODE          VARCHAR2(40 BYTE),
  MITEMNAME       VARCHAR2(100 BYTE),
  RCARDPREFIX     VARCHAR2(100 BYTE)            NOT NULL,
  SNSCALE         VARCHAR2(40 BYTE)             NOT NULL,
  TEMPLATENAME    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK42_1 ON TBLMKEYPART
(MITEMCODE, SEQ)
LOGGING
NOPARALLEL;

CREATE INDEX MKEYPARTS_MOCODE ON TBLMKEYPART
(MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_RCARDSTART_TBLMKEYPART ON TBLMKEYPART
(RCARDSTART)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_RCARDEND_TBLMKEYPART ON TBLMKEYPART
(RCARDEND)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMKEYPART
 ADD CONSTRAINT PK42_1
 PRIMARY KEY
 (MITEMCODE, SEQ);

CREATE TABLE TBLMKEYPARTDETAIL
(
  MITEMCODE     VARCHAR2(40 BYTE)               NOT NULL,
  SEQ           NUMBER(8)                       NOT NULL,
  SERIALNO      VARCHAR2(40 BYTE)               NOT NULL,
  PRINTTIMES    NUMBER(10)                      NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)               NOT NULL,
  TEMPLATENAME  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMKEYPARTDETAIL
 ADD PRIMARY KEY
 (MITEMCODE, SERIALNO);

CREATE TABLE TBLMO
(
  MOCODE           VARCHAR2(40 BYTE)            NOT NULL,
  MOMEMO           VARCHAR2(100 BYTE),
  MOTYPE           VARCHAR2(40 BYTE)            NOT NULL,
  MODESC           VARCHAR2(100 BYTE),
  MOBIOSVER        VARCHAR2(40 BYTE),
  MOPCBAVER        VARCHAR2(40 BYTE),
  MOPLANQTY        NUMBER(10,2)                 NOT NULL,
  MOINPUTQTY       NUMBER(10,2)                 NOT NULL,
  MOSCRAPQTY       NUMBER(10,2)                 NOT NULL,
  MOACTQTY         NUMBER(10,2)                 NOT NULL,
  MOPLANSTARTDATE  NUMBER(8)                    NOT NULL,
  MOPLANENDDATE    NUMBER(8)                    NOT NULL,
  MOACTSTARTDATE   NUMBER(8)                    NOT NULL,
  MOACTENDDATE     NUMBER(8)                    NOT NULL,
  FACTORY          VARCHAR2(40 BYTE),
  CUSCODE          VARCHAR2(40 BYTE),
  CUSNAME          VARCHAR2(100 BYTE),
  CUSORDERNO       VARCHAR2(40 BYTE),
  CUSITEMCODE      VARCHAR2(40 BYTE),
  ORDERNO          VARCHAR2(40 BYTE),
  ORDERSEQ         NUMBER(10)                   NOT NULL,
  MOUSER           VARCHAR2(40 BYTE),
  MODOWNDATE       NUMBER(8)                    NOT NULL,
  MOSTATUS         VARCHAR2(40 BYTE)            NOT NULL,
  MOVER            VARCHAR2(40 BYTE)            NOT NULL,
  ISCONINPUT       VARCHAR2(1 BYTE)             NOT NULL,
  ISBOMPASS        VARCHAR2(1 BYTE)             NOT NULL,
  IDMERGERULE      NUMBER(10)                   NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  MORELEASEDATE    NUMBER(8),
  MORELEASETIME    NUMBER(6),
  MOPENDINGCAUSE   NVARCHAR2(50),
  MOIMPORTDATE     NUMBER(8),
  MOIMPORTTIME     NUMBER(6),
  OFFMOQTY         NUMBER(10,2),
  ISCOMPARESOFT    NUMBER(2),
  RMABILLCODE      VARCHAR2(40 BYTE),
  MOSEQ            NUMBER(10),
  REMOCODE         VARCHAR2(40 BYTE),
  REMOITEMCODE     VARCHAR2(40 BYTE),
  REMOITEMDESC     VARCHAR2(200 BYTE),
  REMOLOTNO        VARCHAR2(40 BYTE),
  REMOENABLED      VARCHAR2(40 BYTE),
  ORGID            NUMBER(8)                    NOT NULL,
  MOBOM            VARCHAR2(40 BYTE)            NOT NULL,
  MOOP             VARCHAR2(40 BYTE),
  ITEMDESC         VARCHAR2(200 BYTE),
  MOPLANSTARTTIME  NUMBER(22),
  MOPLANENDTIME    NUMBER(22),
  MOPLANLINE       VARCHAR2(40 BYTE),
  EATTRIBUTE2      VARCHAR2(100 BYTE),
  EATTRIBUTE3      VARCHAR2(100 BYTE),
  EATTRIBUTE4      VARCHAR2(100 BYTE),
  EATTRIBUTE5      VARCHAR2(100 BYTE),
  EATTRIBUTE6      VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX REF10489 ON TBLMO
(ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX PK11 ON TBLMO
(MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_MO_STATUS ON TBLMO
(MOSTATUS)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMO
 ADD CONSTRAINT PK11
 PRIMARY KEY
 (MOCODE);

CREATE TABLE TBLMO2ROUTE
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  ROUTETYPE    VARCHAR2(40 BYTE),
  OPBOMCODE    VARCHAR2(40 BYTE)                NOT NULL,
  OPBOMVER     VARCHAR2(40 BYTE)                NOT NULL,
  ISMROUTE     VARCHAR2(1 BYTE)                 NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX REF11488 ON TBLMO2ROUTE
(MOCODE)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK34 ON TBLMO2ROUTE
(MOCODE, ROUTECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMO2ROUTE
 ADD CONSTRAINT PK34
 PRIMARY KEY
 (MOCODE, ROUTECODE);

CREATE TABLE TBLMO2SAP
(
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  POSTSEQ        NUMBER(10)                     NOT NULL,
  MOPRODUCED     NUMBER(13)                     NOT NULL,
  MOSCRAP        NUMBER(13)                     NOT NULL,
  MOCONFIRM      VARCHAR2(10 BYTE),
  MOMANHOUR      NUMBER(13),
  MOMACHINEHOUR  NUMBER(13),
  MOCLOSEDATE    NUMBER(8),
  MOLOCATION     VARCHAR2(100 BYTE),
  MOGRADE        VARCHAR2(10 BYTE),
  MOOP           VARCHAR2(40 BYTE),
  FLAG           VARCHAR2(10 BYTE),
  ERRORMESSAGE   VARCHAR2(2000 BYTE),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  ORGID          NUMBER(8)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLMO2SAP ON TBLMO2SAP
(MOCODE, POSTSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMO2SAP
 ADD CONSTRAINT PK_TBLMO2SAP
 PRIMARY KEY
 (MOCODE, POSTSEQ);

CREATE TABLE TBLMO2SAPDETAIL
(
  MOCODE   VARCHAR2(40 BYTE)                    NOT NULL,
  POSTSEQ  NUMBER(22)                           NOT NULL,
  RCARD    VARCHAR2(40 BYTE),
  MUSER    VARCHAR2(40 BYTE)                    NOT NULL,
  MDATE    NUMBER(8)                            NOT NULL,
  MTIME    NUMBER(6)                            NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMO2SAPDETAIL
 ADD PRIMARY KEY
 (MOCODE, RCARD);

CREATE TABLE TBLMO2SAPLOG
(
  SEQ           NUMBER(8),
  MOCODE        VARCHAR2(40 BYTE)               NOT NULL,
  POSTSEQ       NUMBER(10)                      NOT NULL,
  ERRORMESSAGE  VARCHAR2(2000 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8),
  MTIME         NUMBER(6),
  ACTIVE        VARCHAR2(2 BYTE),
  ORGID         NUMBER(8)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLMO2SAPLOG ON TBLMO2SAPLOG
(MOCODE, POSTSEQ, SEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMO2SAPLOG
 ADD CONSTRAINT PK_TBLMO2SAPLOG
 PRIMARY KEY
 (MOCODE, POSTSEQ, SEQ);

CREATE TABLE TBLMOBOM
(
  MOCODE           VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  SEQ              NUMBER(10)                   NOT NULL,
  MOBITEMCODE      VARCHAR2(40 BYTE)            DEFAULT NULL                  NOT NULL,
  MOBITEMECN       VARCHAR2(40 BYTE),
  MOBITEMNAME      VARCHAR2(100 BYTE),
  MOBITEMDESC      VARCHAR2(100 BYTE),
  MOBITEMSTATUS    VARCHAR2(1 BYTE)             NOT NULL,
  MOBITEMLOCATION  VARCHAR2(100 BYTE),
  MOBITEMEFFDATE   NUMBER(8)                    NOT NULL,
  MOBITEMEFFTIME   NUMBER(6)                    NOT NULL,
  MOBITEMINVDATE   NUMBER(8)                    NOT NULL,
  MOBITEMINVTIME   NUMBER(6)                    NOT NULL,
  MOBITEMQTY       NUMBER(15,5)                 NOT NULL,
  MOBSITEMCODE     VARCHAR2(40 BYTE),
  MOBITEMVER       VARCHAR2(40 BYTE),
  MOBITEMCONTYPE   VARCHAR2(40 BYTE)            DEFAULT null,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  MOBOMITEMUOM     VARCHAR2(20 BYTE),
  OPCODE           VARCHAR2(40 BYTE),
  MOBOM            VARCHAR2(40 BYTE),
  MOBOMLINE        VARCHAR2(40 BYTE),
  MOFAC            VARCHAR2(40 BYTE),
  MORESOURCE       VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PKMOBOM71 ON TBLMOBOM
(MOCODE, ITEMCODE, MOBITEMCODE, SEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMOBOM
 ADD CONSTRAINT PKMOBOM71
 PRIMARY KEY
 (MOCODE, ITEMCODE, MOBITEMCODE, SEQ);

CREATE TABLE TBLONWIPPALLET
(
  PALLETCARD   VARCHAR2(40 BYTE)                NOT NULL,
  CARTONCARD   VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK44_1_1 ON TBLONWIPPALLET
(PALLETCARD, CARTONCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPPALLET
 ADD CONSTRAINT PK44_1_1
 PRIMARY KEY
 (PALLETCARD, CARTONCARD);

CREATE TABLE TBLONWIPSOFTVER
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10)                     NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SOFTVER        VARCHAR2(40 BYTE)              NOT NULL,
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  SOFTNAME       VARCHAR2(40 BYTE),
  ROUTECODE      VARCHAR2(40 BYTE)              NOT NULL,
  OPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK44_2 ON TBLONWIPSOFTVER
(RCARD, RCARDSEQ, MOCODE, SOFTVER)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIPSOFTVER_RCARD ON TBLONWIPSOFTVER
(RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIPSOFTVER_IM ON TBLONWIPSOFTVER
(MOCODE, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPSOFTVER
 ADD CONSTRAINT PK44_2
 PRIMARY KEY
 (RCARD, RCARDSEQ, MOCODE, SOFTVER);

CREATE TABLE TBLONWIPTRY
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10)                     NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  TRYNO          VARCHAR2(40 BYTE)              NOT NULL,
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ROUTECODE      VARCHAR2(40 BYTE)              NOT NULL,
  OPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK44_2_1_1 ON TBLONWIPTRY
(RCARD, MOCODE, RCARDSEQ, TRYNO)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIPTRY_RCARD ON TBLONWIPTRY
(RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIPTRY_IM ON TBLONWIPTRY
(MOCODE, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPTRY
 ADD CONSTRAINT PK44_2_1_1
 PRIMARY KEY
 (RCARD, RCARDSEQ, MOCODE, TRYNO);

CREATE TABLE TBLONWIPUNDO
(
  RCARD          VARCHAR2(40 BYTE),
  RCARDSEQ       NUMBER(10),
  MOCODE         VARCHAR2(40 BYTE),
  TCARD          VARCHAR2(40 BYTE),
  TCARDSEQ       NUMBER(10),
  SCARD          VARCHAR2(40 BYTE),
  SCARDSEQ       NUMBER(10),
  MODELCODE      VARCHAR2(40 BYTE),
  ITEMCODE       VARCHAR2(40 BYTE),
  ROUTECODE      VARCHAR2(40 BYTE),
  OPCODE         VARCHAR2(40 BYTE),
  SEGCODE        VARCHAR2(40 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  SHIFTTYPECODE  VARCHAR2(40 BYTE),
  SHIFTCODE      VARCHAR2(40 BYTE),
  TPCODE         VARCHAR2(40 BYTE),
  SHIFTDAY       NUMBER(8),
  ACTION         VARCHAR2(40 BYTE),
  ACTIONRESULT   VARCHAR2(40 BYTE),
  NGTIMES        NUMBER(10),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  SHELFNO        VARCHAR2(40 BYTE),
  RMABILLCODE    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLONWIP_TEST
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10)                     NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  TCARD          VARCHAR2(40 BYTE)              NOT NULL,
  TCARDSEQ       NUMBER(10),
  SCARD          VARCHAR2(40 BYTE)              NOT NULL,
  SCARDSEQ       NUMBER(10),
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ROUTECODE      VARCHAR2(40 BYTE),
  OPCODE         VARCHAR2(40 BYTE),
  SEGCODE        VARCHAR2(40 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTDAY       NUMBER(8),
  ACTION         VARCHAR2(40 BYTE)              NOT NULL,
  ACTIONRESULT   VARCHAR2(40 BYTE)              NOT NULL,
  NGTIMES        NUMBER(10),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  SHELFNO        VARCHAR2(40 BYTE),
  RMABILLCODE    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10),
  SERIAL         NUMBER(38)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX TBLONWIP_TEST_SHELF ON TBLONWIP_TEST
(SHELFNO)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIP_TEST_SHIFTDAY ON TBLONWIP_TEST
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIP_TEST_RCARD ON TBLONWIP_TEST
(RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIP_TEST_MOCODE ON TBLONWIP_TEST
(MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIP_TEST_MDAY ON TBLONWIP_TEST
(MDATE, MTIME)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIP_TEST_MDATE ON TBLONWIP_TEST
(MDATE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIP_TEST_ITEMCODE ON TBLONWIP_TEST
(ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIP_TEST_ACTION ON TBLONWIP_TEST
(ACTION, EATTRIBUTE1)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_SERIAL_TBLONWIP_TEST ON TBLONWIP_TEST
(SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIP_TEST
 ADD PRIMARY KEY
 (RCARD, RCARDSEQ, MOCODE);

CREATE TABLE TBLOP
(
  OPCODE        VARCHAR2(40 BYTE)               NOT NULL,
  OPDESC        VARCHAR2(100 BYTE),
  OPCOLLECTION  VARCHAR2(40 BYTE)               NOT NULL,
  OPCONTROL     VARCHAR2(40 BYTE)               NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK7 ON TBLOP
(OPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOP
 ADD CONSTRAINT PK7
 PRIMARY KEY
 (OPCODE);

CREATE TABLE TBLOP2RES
(
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  RESCODE      VARCHAR2(40 BYTE)                NOT NULL,
  RESSEQ       NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX UI_TBLOP2RES_RESCODE ON TBLOP2RES
(RESCODE)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK258 ON TBLOP2RES
(OPCODE, RESCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOP2RES
 ADD CONSTRAINT PK258
 PRIMARY KEY
 (OPCODE, RESCODE);

CREATE TABLE TBLOPBOM
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  OBCODE       VARCHAR2(40 BYTE)                NOT NULL,
  OPBOMVER     VARCHAR2(40 BYTE)                NOT NULL,
  OBROUTE      VARCHAR2(40 BYTE),
  OPDESC       VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  CHAR(10 BYTE),
  AVIALABLE    NUMBER(2)                        NOT NULL,
  ORGID        NUMBER(8)                        DEFAULT 0                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK256 ON TBLOPBOM
(ITEMCODE, OBCODE, OPBOMVER, ORGID)
LOGGING
NOPARALLEL;

CREATE INDEX REF10499 ON TBLOPBOM
(ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOPBOM
 ADD CONSTRAINT PK256
 PRIMARY KEY
 (ITEMCODE, OBCODE, OPBOMVER, ORGID);

CREATE TABLE TBLOQCPARA
(
  TEMPLATENAME  VARCHAR2(40 BYTE)               NOT NULL,
  ISTEMPLATE    VARCHAR2(40 BYTE)               NOT NULL,
  NODENAME      VARCHAR2(40 BYTE)               NOT NULL,
  NODEVALUE     VARCHAR2(40 BYTE)               NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(8)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLOQCPARA
 ADD PRIMARY KEY
 (TEMPLATENAME, NODENAME);

CREATE TABLE TBLORDER
(
  ORDERNUMBER   VARCHAR2(40 BYTE)               NOT NULL,
  PLANSHIPDATE  NUMBER(8)                       NOT NULL,
  ACTSHIPDATE   NUMBER(8),
  ACTSHIPWEEK   NUMBER(10),
  ACTSHIPMONTH  NUMBER(10),
  ORDERSTATUS   VARCHAR2(40 BYTE)               NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK425 ON TBLORDER
(ORDERNUMBER)
LOGGING
NOPARALLEL;

ALTER TABLE TBLORDER
 ADD CONSTRAINT PK425
 PRIMARY KEY
 (ORDERNUMBER);

CREATE TABLE TBLORDERDETAIL
(
  ORDERNUMBER  VARCHAR2(40 BYTE)                NOT NULL,
  PARTNERCODE  VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ITEMNAME     VARCHAR2(40 BYTE),
  PARTNERDESC  VARCHAR2(100 BYTE),
  PLANQTY      NUMBER(10)                       NOT NULL,
  PLANDATE     NUMBER(8)                        NOT NULL,
  ACTQTY       NUMBER(10),
  ACTDATE      NUMBER(8),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK426 ON TBLORDERDETAIL
(ORDERNUMBER, PARTNERCODE, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLORDERDETAIL
 ADD CONSTRAINT PK426
 PRIMARY KEY
 (ORDERNUMBER, PARTNERCODE, ITEMCODE);

CREATE TABLE TBLORG
(
  ORGID    NUMBER(8)                            NOT NULL,
  ORGDESC  VARCHAR2(40 BYTE)                    NOT NULL,
  MUSER    VARCHAR2(40 BYTE)                    NOT NULL,
  MDATE    NUMBER(8)                            NOT NULL,
  MTIME    NUMBER(6)                            NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLORG ON TBLORG
(ORGID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLORG
 ADD CONSTRAINT PK_TBLORG
 PRIMARY KEY
 (ORGID);

CREATE TABLE TBLOUTMATERIAL
(
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  MATERIALCODE   VARCHAR2(40 BYTE)              NOT NULL,
  REELNO         VARCHAR2(40 BYTE)              NOT NULL,
  QTY            NUMBER(15,5),
  ISSPECIAL      VARCHAR2(1 BYTE),
  ISOUTMATERIAL  VARCHAR2(1 BYTE),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_REELNO ON TBLOUTMATERIAL
(REELNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOUTMATERIAL
 ADD CONSTRAINT PK_REELNO
 PRIMARY KEY
 (REELNO);

CREATE TABLE TBLOUTMO
(
  RNDID         VARCHAR2(40 BYTE)               NOT NULL,
  TYPE          VARCHAR2(40 BYTE),
  MOCODE        VARCHAR2(40 BYTE),
  ITEMCODE      VARCHAR2(40 BYTE),
  QTY           NUMBER(15,5)                    NOT NULL,
  OUTFACTORY    VARCHAR2(40 BYTE),
  COMPLETEDATE  NUMBER(8)                       NOT NULL,
  STARTSN       VARCHAR2(40 BYTE),
  ENDSN         VARCHAR2(40 BYTE),
  MEMO          VARCHAR2(100 BYTE),
  IMPORTUSER    VARCHAR2(40 BYTE),
  IMPORTDATE    NUMBER(8)                       NOT NULL,
  IMPORTTIME    NUMBER(6)                       NOT NULL,
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(100 BYTE),
  PLANQTY       NUMBER(15,5)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLOUTMO
 ADD PRIMARY KEY
 (RNDID);

CREATE TABLE TBLOUTWIP
(
  RNDID          VARCHAR2(40 BYTE)              NOT NULL,
  TYPE           VARCHAR2(40 BYTE),
  MOCODE         VARCHAR2(40 BYTE),
  STARTSN        VARCHAR2(40 BYTE),
  ENDSN          VARCHAR2(40 BYTE),
  QTY            NUMBER(10)                     NOT NULL,
  OPCODE         VARCHAR2(40 BYTE),
  PRODUCTSTATUS  VARCHAR2(40 BYTE),
  ERRORDESC      VARCHAR2(100 BYTE),
  MEMO           VARCHAR2(100 BYTE),
  IMPORTUSER     VARCHAR2(40 BYTE),
  IMPORTDATE     NUMBER(8)                      NOT NULL,
  IMPORTTIME     NUMBER(6)                      NOT NULL,
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(100 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  SHIFTDATE      VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLOUTWIP
 ADD PRIMARY KEY
 (RNDID);

CREATE TABLE TBLOUTWIPMATERIAL
(
  RNDID         VARCHAR2(40 BYTE)               NOT NULL,
  TYPE          VARCHAR2(40 BYTE),
  MOCODE        VARCHAR2(40 BYTE),
  STARTSN       VARCHAR2(40 BYTE),
  ENDSN         VARCHAR2(40 BYTE),
  MATERIALCODE  VARCHAR2(40 BYTE),
  DATECODE      VARCHAR2(40 BYTE),
  SUPPLIER      VARCHAR2(40 BYTE),
  MEMO          VARCHAR2(100 BYTE),
  IMPORTUSER    VARCHAR2(40 BYTE),
  IMPORTDATE    NUMBER(8)                       NOT NULL,
  IMPORTTIME    NUMBER(6)                       NOT NULL,
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLOUTWIPMATERIAL
 ADD PRIMARY KEY
 (RNDID);

CREATE TABLE TBLPACKINGCHK
(
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  CHKPRODUCTCODE  VARCHAR2(40 BYTE)             NOT NULL,
  CHKACCESSORY    VARCHAR2(40 BYTE)             NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)             NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLPACKINGCHK
 ADD PRIMARY KEY
 (RCARD);

CREATE TABLE TBLPALLET
(
  PALLETCODE   VARCHAR2(40 BYTE)                NOT NULL,
  RCARDCOUNT   NUMBER(8)                        NOT NULL,
  CAPACITY     NUMBER(8)                        NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)                NOT NULL,
  ORGID        NUMBER(8)                        NOT NULL,
  RESCODE      VARCHAR2(40 BYTE)                DEFAULT (' ')                 NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLPALLET
 ADD PRIMARY KEY
 (PALLETCODE);

CREATE TABLE TBLPALLET2RCARD
(
  PALLETCODE   VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLPALLET2RCARD
 ADD PRIMARY KEY
 (PALLETCODE, RCARD);

CREATE TABLE TBLPAUSE
(
  PAUSECODE     VARCHAR2(40 BYTE)               NOT NULL,
  PAUSEREASON   VARCHAR2(200 BYTE),
  STATUS        VARCHAR2(40 BYTE)               NOT NULL,
  CANCELREASON  VARCHAR2(200 BYTE),
  PUSER         VARCHAR2(40 BYTE)               NOT NULL,
  PDATE         NUMBER(8)                       NOT NULL,
  PTIME         NUMBER(6)                       NOT NULL,
  CUSER         VARCHAR2(40 BYTE),
  CDATE         NUMBER(8),
  CTIME         NUMBER(6),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLPAUSE
 ADD PRIMARY KEY
 (PAUSECODE);

CREATE TABLE TBLPAUSE2RCARD
(
  PAUSECODE     VARCHAR2(40 BYTE)               NOT NULL,
  SERIALNO      VARCHAR2(40 BYTE)               NOT NULL,
  ITEMCODE      VARCHAR2(40 BYTE)               NOT NULL,
  CANCELSEQ     VARCHAR2(40 BYTE),
  BOM           VARCHAR2(40 BYTE),
  MOCODE        VARCHAR2(40 BYTE),
  STATUS        VARCHAR2(40 BYTE)               NOT NULL,
  CANCELREASON  VARCHAR2(200 BYTE),
  PUSER         VARCHAR2(40 BYTE)               NOT NULL,
  PDATE         NUMBER(8)                       NOT NULL,
  PTIME         NUMBER(6)                       NOT NULL,
  CUSER         VARCHAR2(40 BYTE),
  CDATE         NUMBER(8),
  CTIME         NUMBER(6),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLPAUSE2RCARD_STATUS ON TBLPAUSE2RCARD
(SERIALNO, STATUS)
LOGGING
NOPARALLEL;

ALTER TABLE TBLPAUSE2RCARD
 ADD PRIMARY KEY
 (SERIALNO, PAUSECODE);

CREATE TABLE TBLMATERIALREQINFO
(
  BIGSSCODE   VARCHAR2(40 BYTE)                 NOT NULL,
  PLANDATE    NUMBER(8)                         NOT NULL,
  MOCODE      VARCHAR2(40 BYTE)                 NOT NULL,
  MOSEQ       NUMBER(10)                        NOT NULL,
  REQUESTSEQ  NUMBER(10)                        NOT NULL,
  PLANSEQ     NUMBER(10)                        NOT NULL,
  ITEMCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  REQUESTQTY  NUMBER(10,2)                      NOT NULL,
  MAYBEQTY    NUMBER(10,2)                      NOT NULL,
  STATUS      VARCHAR2(40 BYTE)                 NOT NULL,
  REQTYPE     VARCHAR2(40 BYTE)                 NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMATERIALREQINFO
 ADD PRIMARY KEY
 (BIGSSCODE, PLANDATE, MOCODE, MOSEQ, REQUESTSEQ);

CREATE TABLE TBLMATERIALISSUE
(
  BIGSSCODE    VARCHAR2(40 BYTE)                NOT NULL,
  PLANDATE     NUMBER(8)                        NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MOSEQ        NUMBER(10)                       NOT NULL,
  ISSUESEQ     NUMBER(10)                       NOT NULL,
  ISSUEQTY     NUMBER(10,2)                     NOT NULL,
  ISSUETYPE    VARCHAR2(40 BYTE)                NOT NULL,
  ISSUESTATUS  VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMATERIALISSUE
 ADD PRIMARY KEY
 (BIGSSCODE, PLANDATE, MOCODE, MOSEQ, ISSUESEQ);

CREATE TABLE TBLRES2MO
(
  SEQ                  NUMBER(10)               NOT NULL,
  RESCODE              VARCHAR2(40 BYTE),
  STARTDATE            NUMBER(8)                NOT NULL,
  STARTTIME            NUMBER(6)                NOT NULL,
  ENDDATE              NUMBER(8)                NOT NULL,
  ENDTIME              NUMBER(6)                NOT NULL,
  MOGETTYPE            VARCHAR2(40 BYTE),
  STATICMOCODE         VARCHAR2(40 BYTE),
  MOCODERCARDSTARTIDX  NUMBER(10)               NOT NULL,
  MOCODELEN            NUMBER(10)               NOT NULL,
  MOCODEPREFIX         VARCHAR2(40 BYTE),
  MOCODEPOSTFIX        VARCHAR2(40 BYTE),
  CHKRCARDFORMAT       VARCHAR2(1 BYTE)         NOT NULL,
  RCARDPREFIX          VARCHAR2(40 BYTE),
  RCARDLEN             NUMBER(10)               NOT NULL,
  MUSER                VARCHAR2(40 BYTE),
  MDATE                NUMBER(8)                NOT NULL,
  MTIME                NUMBER(6)                NOT NULL,
  EATTRIBUTE1          VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX MRES2MO_RES ON TBLRES2MO
(RESCODE, STARTDATE, STARTTIME, ENDDATE, ENDTIME)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRES2MO
 ADD PRIMARY KEY
 (SEQ);

CREATE TABLE TBLRES2REWORKSHEET
(
  REWORKCODE  VARCHAR2(40 BYTE)                 NOT NULL,
  RESCODE     VARCHAR2(40 BYTE)                 NOT NULL,
  ITEMCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  LOTNO       VARCHAR2(40 BYTE),
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRES2REWORKSHEET
 ADD PRIMARY KEY
 (RESCODE, REWORKCODE);

CREATE TABLE TBLRPTLINEQTY
(
  MOCODE                   VARCHAR2(40 BYTE)    NOT NULL,
  SHIFTDAY                 NUMBER(8)            NOT NULL,
  ITEMCODE                 VARCHAR2(40 BYTE)    NOT NULL,
  TBLMESENTITYLIST_SERIAL  NUMBER(38)           NOT NULL,
  LINEWHITECARDCOUNT       NUMBER(22)           NOT NULL,
  RESWHITECARDCOUNT        NUMBER(22)           NOT NULL,
  EATTRIBUTE1              VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLRPTLINEQTY ON TBLRPTLINEQTY
(MOCODE, SHIFTDAY, ITEMCODE, TBLMESENTITYLIST_SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTLINEQTY
 ADD CONSTRAINT PK_TBLRPTLINEQTY
 PRIMARY KEY
 (MOCODE, SHIFTDAY, ITEMCODE, TBLMESENTITYLIST_SERIAL);

CREATE TABLE TBLRPTOPQTY
(
  MOCODE                   VARCHAR2(40 BYTE)    NOT NULL,
  SHIFTDAY                 NUMBER(8)            NOT NULL,
  ITEMCODE                 VARCHAR2(40 BYTE)    NOT NULL,
  TBLMESENTITYLIST_SERIAL  NUMBER(38)           NOT NULL,
  INPUTTIMES               NUMBER(22)           NOT NULL,
  OUTPUTTIMES              NUMBER(22)           NOT NULL,
  NGTIMES                  NUMBER(22)           NOT NULL,
  EATTRIBUTE1              VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLRPTOPQTY ON TBLRPTOPQTY
(MOCODE, SHIFTDAY, ITEMCODE, TBLMESENTITYLIST_SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTOPQTY
 ADD CONSTRAINT PK_TBLRPTOPQTY
 PRIMARY KEY
 (MOCODE, SHIFTDAY, ITEMCODE, TBLMESENTITYLIST_SERIAL);

CREATE TABLE TBLRPTREALLINEECEQTY
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SHIFTDAY     NUMBER(8)                        NOT NULL,
  SHIFTCODE    VARCHAR2(40 BYTE)                NOT NULL,
  TPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ECCODE       VARCHAR2(40 BYTE)                NOT NULL,
  DAY          NUMBER(8)                        NOT NULL,
  ECTIMES      NUMBER(10)                       NOT NULL,
  EATTRIBUTE1  NUMBER(10)                       NOT NULL,
  EATTRIBUTE2  NUMBER(10)                       NOT NULL,
  EATTRIBUTE3  NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314_2_2_1_1_1 ON TBLRPTREALLINEECEQTY
(MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, 
TPCODE, SEGCODE, SSCODE, ECCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTREALLINEECEQTY_SD ON TBLRPTREALLINEECEQTY
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_REALLINEECEQTY_CODE1 ON TBLRPTREALLINEECEQTY
(MODELCODE, ITEMCODE, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTREALLINEECEQTY
 ADD CONSTRAINT PK314_2_2_1_1_1
 PRIMARY KEY
 (MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, TPCODE, SEGCODE, SSCODE, ECCODE);

CREATE TABLE TBLRPTREALLINEECQTY
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SHIFTDAY     NUMBER(8)                        NOT NULL,
  SHIFTCODE    VARCHAR2(40 BYTE)                NOT NULL,
  TPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  DAY          NUMBER(8)                        NOT NULL,
  ECTIMES      NUMBER(10)                       NOT NULL,
  EATTRIBUTE1  NUMBER(10)                       NOT NULL,
  EATTRIBUTE2  NUMBER(10)                       NOT NULL,
  EATTRIBUTE3  NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314_2_2_1 ON TBLRPTREALLINEECQTY
(MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, 
TPCODE, SEGCODE, SSCODE, ECODE, ECGCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTREALLINEECQTY_SD ON TBLRPTREALLINEECQTY
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_REALLINEECQTY_CODE1 ON TBLRPTREALLINEECQTY
(MODELCODE, ITEMCODE, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTREALLINEECQTY
 ADD CONSTRAINT PK314_2_2_1
 PRIMARY KEY
 (MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, TPCODE, SEGCODE, SSCODE, ECODE, ECGCODE);

CREATE TABLE TBLRPTREALLINEELQTY
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SHIFTDAY     NUMBER(8)                        NOT NULL,
  SHIFTCODE    VARCHAR2(40 BYTE)                NOT NULL,
  TPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ELOC         VARCHAR2(40 BYTE)                NOT NULL,
  DAY          NUMBER(8)                        NOT NULL,
  ELOCTIMES    VARCHAR2(1 BYTE)                 NOT NULL,
  EATTRIBUTE1  NUMBER(10)                       NOT NULL,
  EATTRIBUTE2  NUMBER(10)                       NOT NULL,
  EATTRIBUTE3  NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314_2_2 ON TBLRPTREALLINEELQTY
(MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, 
TPCODE, SEGCODE, SSCODE, ELOC)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTREALLINEELQTY_SD ON TBLRPTREALLINEELQTY
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_REALLINEELQTY_CODE1 ON TBLRPTREALLINEELQTY
(MODELCODE, ITEMCODE, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTREALLINEELQTY
 ADD CONSTRAINT PK314_2_2
 PRIMARY KEY
 (MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, TPCODE, SEGCODE, SSCODE, ELOC);

CREATE TABLE TBLRPTREALLINEEPQTY
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SHIFTDAY     NUMBER(8)                        NOT NULL,
  SHIFTCODE    VARCHAR2(40 BYTE)                NOT NULL,
  TPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  EPART        VARCHAR2(40 BYTE)                NOT NULL,
  DAY          NUMBER(8)                        NOT NULL,
  EPTIMES      NUMBER(10)                       NOT NULL,
  EATTRIBUTE1  NUMBER(10)                       NOT NULL,
  EATTRIBUTE2  NUMBER(10)                       NOT NULL,
  EATTRIBUTE3  NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314_2_2_1_1 ON TBLRPTREALLINEEPQTY
(MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, 
TPCODE, SEGCODE, SSCODE, EPART)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TREALLINEEPQTY_CODE1 ON TBLRPTREALLINEEPQTY
(MODELCODE, ITEMCODE, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTREALLINEEPQTY_SD ON TBLRPTREALLINEEPQTY
(SHIFTDAY)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTREALLINEEPQTY
 ADD CONSTRAINT PK314_2_2_1_1
 PRIMARY KEY
 (MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, TPCODE, SEGCODE, SSCODE, EPART);

CREATE TABLE TBLRPTREALLINEQTY
(
  MODELCODE     VARCHAR2(40 BYTE)               NOT NULL,
  ITEMCODE      VARCHAR2(40 BYTE)               NOT NULL,
  MOCODE        VARCHAR2(40 BYTE)               NOT NULL,
  SHIFTDAY      NUMBER(8)                       NOT NULL,
  SHIFTCODE     VARCHAR2(40 BYTE)               NOT NULL,
  TPCODE        VARCHAR2(40 BYTE)               NOT NULL,
  SEGCODE       VARCHAR2(40 BYTE)               NOT NULL,
  SSCODE        VARCHAR2(40 BYTE)               NOT NULL,
  QTYFLAG       VARCHAR2(1 BYTE)                NOT NULL,
  DAY           NUMBER(8)                       NOT NULL,
  WEEK          NUMBER(10)                      NOT NULL,
  MONTH         NUMBER(10)                      NOT NULL,
  TPBTIME       NUMBER(6)                       NOT NULL,
  TPETIME       NUMBER(6)                       NOT NULL,
  OUTPUTQTY     NUMBER(10)                      NOT NULL,
  ALLGOODQTY    NUMBER(10)                      NOT NULL,
  SCRAPQTY      NUMBER(10)                      NOT NULL,
  INPUTQTY      NUMBER(10)                      NOT NULL,
  NGTIMES       NUMBER(10)                      NOT NULL,
  EATTRIBUTE1   NUMBER(10)                      NOT NULL,
  EATTRIBUTE2   NUMBER(10)                      NOT NULL,
  EATTRIBUTE3   NUMBER(10)                      NOT NULL,
  MOALLGOODQTY  NUMBER(10),
  NGQTYS        NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314 ON TBLRPTREALLINEQTY
(MOCODE, TPCODE, SSCODE, SEGCODE, ITEMCODE, 
SHIFTCODE, MODELCODE, SHIFTDAY, QTYFLAG)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTREALLINEQTY_SD ON TBLRPTREALLINEQTY
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTREALLINEQTY_CODE ON TBLRPTREALLINEQTY
(MODELCODE, ITEMCODE, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTREALLINEQTY
 ADD CONSTRAINT PK314
 PRIMARY KEY
 (MOCODE, TPCODE, SSCODE, SEGCODE, ITEMCODE, SHIFTCODE, MODELCODE, SHIFTDAY, QTYFLAG);

CREATE TABLE TBLRPTRESECG
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SHIFTDAY     NUMBER(8)                        NOT NULL,
  SHIFTCODE    VARCHAR2(40 BYTE)                NOT NULL,
  TPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  RESCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECCODE       VARCHAR2(40 BYTE)                NOT NULL,
  TPBTIME      NUMBER(6)                        NOT NULL,
  TPETIME      NUMBER(6)                        NOT NULL,
  MONTH        NUMBER(10)                       NOT NULL,
  WEEK         NUMBER(10)                       NOT NULL,
  DAY          NUMBER(8)                        NOT NULL,
  NGTIMES      NUMBER(10)                       NOT NULL,
  EATTRIBUTE1  NUMBER(10)                       NOT NULL,
  EATTRIBUTE2  NUMBER(10)                       NOT NULL,
  EATTRIBUTE3  NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLRPTRESECG ON TBLRPTRESECG
(MODELCODE, ITEMCODE, MOCODE, SHIFTDAY, SHIFTCODE, 
TPCODE, OPCODE, RESCODE, SEGCODE, SSCODE, 
ECGCODE, ECCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTRESECG_SHIFTDAY ON TBLRPTRESECG
(SHIFTDAY)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTRESECG
 ADD CONSTRAINT PK_TBLRPTRESECG
 PRIMARY KEY
 (MODELCODE, ITEMCODE, MOCODE, SHIFTDAY, SHIFTCODE, TPCODE, OPCODE, RESCODE, SEGCODE, SSCODE, ECGCODE, ECCODE);

CREATE TABLE TBLRPTSOQTY
(
  MOCODE                   VARCHAR2(40 BYTE)    NOT NULL,
  SHIFTDAY                 NUMBER(8)            NOT NULL,
  ITEMCODE                 VARCHAR2(40 BYTE)    NOT NULL,
  TBLMESENTITYLIST_SERIAL  NUMBER(38)           NOT NULL,
  MOINPUTCOUNT             NUMBER(22)           NOT NULL,
  MOOUTPUTCOUNT            NUMBER(22)           NOT NULL,
  MOLINEOUTPUTCOUNT        NUMBER(22)           NOT NULL,
  MOWHITECARDCOUNT         NUMBER(22)           NOT NULL,
  MOOUTPUTWHITECARDCOUNT   NUMBER(22)           NOT NULL,
  LINEINPUTCOUNT           NUMBER(22)           NOT NULL,
  LINEOUTPUTCOUNT          NUMBER(22)           NOT NULL,
  OPCOUNT                  NUMBER(22)           NOT NULL,
  OPWHITECARDCOUNT         NUMBER(22)           NOT NULL,
  EATTRIBUTE1              VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLRPTSOQTY ON TBLRPTSOQTY
(MOCODE, SHIFTDAY, ITEMCODE, TBLMESENTITYLIST_SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTSOQTY
 ADD CONSTRAINT PK_TBLRPTSOQTY
 PRIMARY KEY
 (MOCODE, SHIFTDAY, ITEMCODE, TBLMESENTITYLIST_SERIAL);

CREATE TABLE TBLRPTSUMMARYLOG
(
  SERIAL         NUMBER(38)                     NOT NULL,
  STARTDATETIME  DATE                           NOT NULL,
  ENDDATETIME    DATE                           NOT NULL,
  USEDTIME       NUMBER(22)                     NOT NULL,
  PROCESSCOUNT   NUMBER(22)                     NOT NULL,
  RESULT         VARCHAR2(40 BYTE)              NOT NULL,
  ERRORMSG       VARCHAR2(500 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLRPTSUMMARYLOG ON TBLRPTSUMMARYLOG
(STARTDATETIME)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK_TBLRPTSUMMARYLOG ON TBLRPTSUMMARYLOG
(SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTSUMMARYLOG
 ADD CONSTRAINT PK_TBLRPTSUMMARYLOG
 PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLRPTVCHARTCATE
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  CHARTSEQ      NUMBER(10)                      NOT NULL,
  CATESEQ       NUMBER(10)                      NOT NULL,
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVCHARTCATE
 ADD PRIMARY KEY
 (RPTID, CHARTSEQ, CATESEQ);

CREATE TABLE TBLRPTVCHARTDATA
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  CHARTSEQ      NUMBER(10)                      NOT NULL,
  DATASEQ       NUMBER(10)                      NOT NULL,
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE),
  DESCRIPTION   VARCHAR2(100 BYTE),
  TOTALTYPE     VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVCHARTDATA
 ADD PRIMARY KEY
 (RPTID, CHARTSEQ, DATASEQ);

CREATE TABLE TBLRPTVCHARTMAIN
(
  RPTID          VARCHAR2(40 BYTE)              NOT NULL,
  CHARTSEQ       NUMBER(10)                     NOT NULL,
  DATASOURCEID   NUMBER(10)                     NOT NULL,
  CHARTTYPE      VARCHAR2(40 BYTE),
  CHARTSUBTYPE   VARCHAR2(40 BYTE),
  SHOWLEGEND     VARCHAR2(1 BYTE)               NOT NULL,
  SHOWMARKER     VARCHAR2(1 BYTE)               NOT NULL,
  MARKERTYPE     VARCHAR2(40 BYTE),
  SHOWLABEL      VARCHAR2(1 BYTE)               NOT NULL,
  LABELFORMATID  VARCHAR2(40 BYTE),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVCHARTMAIN
 ADD PRIMARY KEY
 (RPTID, CHARTSEQ);

CREATE TABLE TBLRPTVCHARTSER
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  CHARTSEQ      NUMBER(10)                      NOT NULL,
  SERSEQ        NUMBER(10)                      NOT NULL,
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE),
  DESCRIPTION   VARCHAR2(100 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVCHARTSER
 ADD PRIMARY KEY
 (RPTID, CHARTSEQ, SERSEQ);

CREATE TABLE TBLRPTVCONNECT
(
  DATACONNECTID    NUMBER(10)                   NOT NULL,
  CONNECTNAME      VARCHAR2(100 BYTE),
  DESCRIPTION      VARCHAR2(100 BYTE),
  SERVERTYPE       VARCHAR2(40 BYTE),
  SERVICENAME      VARCHAR2(40 BYTE),
  USERNAME         VARCHAR2(40 BYTE),
  PASSWORD         VARCHAR2(100 BYTE),
  DEFAULTDATABASE  VARCHAR2(40 BYTE),
  MUSER            VARCHAR2(40 BYTE),
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVCONNECT
 ADD PRIMARY KEY
 (DATACONNECTID);

CREATE TABLE TBLRPTVDATAFMT
(
  FORMATID        VARCHAR2(40 BYTE)             NOT NULL,
  FONTFAMILY      VARCHAR2(40 BYTE),
  FONTSIZE        NUMBER(10)                    NOT NULL,
  TEXTDECORATION  VARCHAR2(40 BYTE),
  FONTWEIGHT      VARCHAR2(40 BYTE),
  FONTSTYLE       VARCHAR2(40 BYTE),
  COLOR           VARCHAR2(40 BYTE),
  BCOLOR          VARCHAR2(40 BYTE),
  TEXTALIGN       VARCHAR2(40 BYTE),
  VERTICALALIGN   VARCHAR2(40 BYTE),
  TEXTFORMAT      VARCHAR2(40 BYTE),
  MUSER           VARCHAR2(40 BYTE),
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  COLUMNWIDTH     NUMBER(15,5),
  BORDERSTYLE     VARCHAR2(40 BYTE),
  TEXTEXPRESS     VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVDATAFMT
 ADD PRIMARY KEY
 (FORMATID);

CREATE TABLE TBLRPTVDATASRC
(
  DATASOURCEID   NUMBER(10)                     NOT NULL,
  NAME           VARCHAR2(40 BYTE),
  DESCRIPTION    VARCHAR2(100 BYTE),
  DATACONNECTID  NUMBER(10)                     NOT NULL,
  SOURCETYPE     VARCHAR2(40 BYTE),
  SQL            VARCHAR2(1000 BYTE),
  DLLFILENAME    VARCHAR2(100 BYTE),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVDATASRC
 ADD PRIMARY KEY
 (DATASOURCEID);

CREATE TABLE TBLRPTVDATASRCCOLUMN
(
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  COLUMNSEQ     NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE),
  DESCRIPTION   VARCHAR2(100 BYTE),
  DATATYPE      VARCHAR2(40 BYTE),
  VISIBLE       VARCHAR2(1 BYTE)                NOT NULL,
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVDATASRCCOLUMN
 ADD PRIMARY KEY
 (DATASOURCEID, COLUMNSEQ, COLUMNNAME);

CREATE TABLE TBLRPTVDATASRCPARAM
(
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  PARAMSEQ      NUMBER(10)                      NOT NULL,
  PARAMNAME     VARCHAR2(40 BYTE),
  DESCRIPTION   VARCHAR2(100 BYTE),
  DATATYPE      VARCHAR2(40 BYTE),
  DEFAULTVALUE  VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVDATASRCPARAM
 ADD PRIMARY KEY
 (DATASOURCEID, PARAMSEQ);

CREATE TABLE TBLRPTVDESIGNMAIN
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  RPTNAME       VARCHAR2(40 BYTE),
  DESCRIPTION   VARCHAR2(100 BYTE),
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  RPTBUILDER    VARCHAR2(40 BYTE),
  DISPLAYTYPE   VARCHAR2(40 BYTE),
  STATUS        VARCHAR2(40 BYTE),
  RPTFILENAME   VARCHAR2(100 BYTE),
  PRPTFOLDER    VARCHAR2(40 BYTE),
  DESIGNUSER    VARCHAR2(40 BYTE),
  DESIGNDATE    NUMBER(8)                       NOT NULL,
  DESIGNTIME    NUMBER(6)                       NOT NULL,
  PUBLISHUSER   VARCHAR2(40 BYTE),
  PUBLISHDATE   NUMBER(8)                       NOT NULL,
  PUBLISHTIME   NUMBER(6)                       NOT NULL,
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVDESIGNMAIN
 ADD PRIMARY KEY
 (RPTID);

CREATE TABLE TBLRPTVENTRY
(
  ENTRYCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ENTRYNAME    VARCHAR2(40 BYTE),
  PENTRYCODE   VARCHAR2(40 BYTE),
  DESCRIPTION  VARCHAR2(100 BYTE),
  SEQ          NUMBER(10)                       NOT NULL,
  VISIBLE      VARCHAR2(1 BYTE)                 NOT NULL,
  ENTRYTYPE    VARCHAR2(40 BYTE),
  RPTID        VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLRPTVEXTTEXT
(
  RPTID     VARCHAR2(40 BYTE)                   NOT NULL,
  SEQ       NUMBER(10)                          NOT NULL,
  LOCATION  VARCHAR2(40 BYTE),
  FORMATID  VARCHAR2(40 BYTE),
  MUSER     VARCHAR2(40 BYTE),
  MDATE     NUMBER(8)                           NOT NULL,
  MTIME     NUMBER(6)                           NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVEXTTEXT
 ADD PRIMARY KEY
 (RPTID, SEQ);

CREATE TABLE TBLRPTVFILEPARAM
(
  RPTID          VARCHAR2(40 BYTE)              NOT NULL,
  SEQ            NUMBER(10)                     NOT NULL,
  FILEPARAMNAME  VARCHAR2(40 BYTE),
  DESCRIPTION    VARCHAR2(100 BYTE),
  DATATYPE       VARCHAR2(40 BYTE),
  DEFAULTVALUE   VARCHAR2(40 BYTE),
  VIEWERINPUT    VARCHAR2(1 BYTE)               NOT NULL,
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVFILEPARAM
 ADD PRIMARY KEY
 (RPTID, SEQ);

CREATE TABLE TBLRPTVFILTERUI
(
  RPTID          VARCHAR2(40 BYTE)              NOT NULL,
  SEQ            NUMBER(10)                     NOT NULL,
  INPUTTYPE      VARCHAR2(40 BYTE),
  INPUTNAME      VARCHAR2(40 BYTE),
  SQLFLTSEQ      NUMBER(10)                     NOT NULL,
  UITYPE         VARCHAR2(40 BYTE),
  SELQTYPE       VARCHAR2(40 BYTE),
  LISTSRCTYPE    VARCHAR2(40 BYTE),
  LISTSVAL       VARCHAR2(100 BYTE),
  LISTDSRC       NUMBER(10)                     NOT NULL,
  LISTDTEXTCOL   VARCHAR2(40 BYTE),
  LISTDVALUECOL  VARCHAR2(40 BYTE),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(100 BYTE),
  CHECKEXIST     VARCHAR2(1 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVFILTERUI
 ADD PRIMARY KEY
 (RPTID, SEQ);

CREATE TABLE TBLRPTVGRIDCOLUMN
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  DISPLAYSEQ    NUMBER(10)                      NOT NULL,
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVGRIDCOLUMN
 ADD PRIMARY KEY
 (RPTID, DISPLAYSEQ);

CREATE TABLE TBLRPTVGRIDDATAFMT
(
  RPTID       VARCHAR2(40 BYTE)                 NOT NULL,
  COLUMNNAME  VARCHAR2(40 BYTE)                 NOT NULL,
  STYLETYPE   VARCHAR2(40 BYTE)                 NOT NULL,
  GRPSEQ      NUMBER(10)                        NOT NULL,
  FORMATID    VARCHAR2(40 BYTE),
  MUSER       VARCHAR2(40 BYTE),
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVGRIDDATAFMT
 ADD PRIMARY KEY
 (RPTID, COLUMNNAME, STYLETYPE, GRPSEQ);

CREATE TABLE TBLRPTVGRIDDATASTYLE
(
  RPTID    VARCHAR2(40 BYTE)                    NOT NULL,
  STYLEID  NUMBER(10)                           NOT NULL,
  MUSER    VARCHAR2(40 BYTE),
  MDATE    NUMBER(8)                            NOT NULL,
  MTIME    NUMBER(6)                            NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVGRIDDATASTYLE
 ADD PRIMARY KEY
 (RPTID);

CREATE TABLE TBLRPTVGRIDFLT
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  FLTSEQ        NUMBER(10)                      NOT NULL,
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE),
  PARAMNAME     VARCHAR2(40 BYTE),
  DESCRIPTION   VARCHAR2(100 BYTE),
  FLTOPERAT     VARCHAR2(40 BYTE),
  DEFAULTVALUE  VARCHAR2(100 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVGRIDFLT
 ADD PRIMARY KEY
 (RPTID, FLTSEQ);

CREATE TABLE TBLRPTVGRIDGRP
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  GRPSEQ        NUMBER(10)                      NOT NULL,
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVGRIDGRP
 ADD PRIMARY KEY
 (RPTID, GRPSEQ);

CREATE TABLE TBLRPTVGRIDGRPTOTAL
(
  RPTID         VARCHAR2(40 BYTE)               NOT NULL,
  GRPSEQ        NUMBER(10)                      NOT NULL,
  COLUMNNAME    VARCHAR2(40 BYTE)               NOT NULL,
  DATASOURCEID  NUMBER(10)                      NOT NULL,
  TOTALTYPE     VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVGRIDGRPTOTAL
 ADD PRIMARY KEY
 (RPTID, GRPSEQ, COLUMNNAME);

CREATE TABLE TBLRPTVRPTSECURITY
(
  RPTID              VARCHAR2(40 BYTE)          NOT NULL,
  SEQ                NUMBER(10)                 NOT NULL,
  USERGROUPCODE      VARCHAR2(40 BYTE),
  RIGHTACCESS        VARCHAR2(40 BYTE),
  FUNCTIONGROUPCODE  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVRPTSECURITY
 ADD PRIMARY KEY
 (RPTID, SEQ);

CREATE TABLE TBLRPTVSTYLE
(
  STYLEID      NUMBER(10)                       NOT NULL,
  NAME         VARCHAR2(40 BYTE),
  DESCRIPTION  VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVSTYLE
 ADD PRIMARY KEY
 (STYLEID);

CREATE TABLE TBLRPTVSTYLEDTL
(
  STYLEID    NUMBER(10)                         NOT NULL,
  SEQ        NUMBER(10)                         NOT NULL,
  STYLETYPE  VARCHAR2(40 BYTE),
  FORMATID   VARCHAR2(40 BYTE),
  MUSER      VARCHAR2(40 BYTE),
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVSTYLEDTL
 ADD PRIMARY KEY
 (STYLEID, STYLETYPE);

CREATE TABLE TBLRPTVSYSERROR
(
  SYSERRCODE   VARCHAR2(40 BYTE)                NOT NULL,
  ERRMSG       VARCHAR2(100 BYTE)               NOT NULL,
  INNERERRMSG  VARCHAR2(100 BYTE),
  TRGMDLCODE   VARCHAR2(40 BYTE),
  TRIGACTION   VARCHAR2(40 BYTE),
  SENDUSER     VARCHAR2(40 BYTE)                NOT NULL,
  SENDDATE     NUMBER(8)                        NOT NULL,
  SENDTIME     NUMBER(6)                        NOT NULL,
  ISRES        VARCHAR2(1 BYTE)                 NOT NULL,
  RESNOTES     VARCHAR2(100 BYTE),
  RESUSER      VARCHAR2(40 BYTE),
  RESDATE      NUMBER(8),
  RESTIME      NUMBER(6),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX RPTVSYSERROR_PK1 ON TBLRPTVSYSERROR
(SYSERRCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTVSYSERROR
 ADD CONSTRAINT RPTVSYSERROR_PK1
 PRIMARY KEY
 (SYSERRCODE);

CREATE TABLE TBLRPTVUSERDFT
(
  USERCODE      VARCHAR2(40 BYTE)               NOT NULL,
  DEFAULTRPTID  VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVUSERDFT
 ADD PRIMARY KEY
 (USERCODE);

CREATE TABLE TBLRPTVUSERSUBSCR
(
  USERCODE     VARCHAR2(40 BYTE)                NOT NULL,
  RPTID        VARCHAR2(40 BYTE)                NOT NULL,
  SEQ          NUMBER(10)                       NOT NULL,
  INPUTTYPE    VARCHAR2(40 BYTE),
  INPUTNAME    VARCHAR2(40 BYTE),
  INPUTVALUE   VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE),
  SQLFLTSEQ    NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRPTVUSERSUBSCR
 ADD PRIMARY KEY
 (USERCODE, RPTID, SEQ);

CREATE TABLE TBLEXCEPTION
(
  SERIAL         NUMBER                         NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTDATE      NUMBER(8)                      NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  BEGINTIME      NUMBER(6)                      NOT NULL,
  ENDTIME        NUMBER(6)                      NOT NULL,
  EXCEPTIONCODE  VARCHAR2(40 BYTE)              NOT NULL,
  MEMO           VARCHAR2(500 BYTE),
  COMFIRMMEMO    VARCHAR2(500 BYTE),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLEXCEPTION1 ON TBLEXCEPTION
(SSCODE, SHIFTDATE, SHIFTCODE, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLEXCEPTION
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLSAPSTORAGEINFO
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  DIVISION     VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        NOT NULL,
  STORAGEID    VARCHAR2(40 BYTE)                NOT NULL,
  STORAGENAME  VARCHAR2(100 BYTE),
  ITEMGRADE    VARCHAR2(40 BYTE),
  CLABSQTY     NUMBER(13)                       NOT NULL,
  CINSMQTY     NUMBER(13)                       NOT NULL,
  CSPEMQTY     NUMBER(13)                       NOT NULL,
  CUMLQTY      NUMBER(13)                       NOT NULL,
  ITEMDESC     VARCHAR2(100 BYTE),
  MODELCODE    VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8),
  MTIME        NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSAPSTORAGEINFO ON TBLSAPSTORAGEINFO
(ITEMCODE, ORGID, STORAGEID, ITEMGRADE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSAPSTORAGEINFO
 ADD CONSTRAINT PK_TBLSAPSTORAGEINFO
 PRIMARY KEY
 (ITEMCODE, ORGID, STORAGEID, ITEMGRADE);

CREATE TABLE TBLSAPSTORAGEQUERY
(
  SERIAL           NUMBER(8)                    NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE),
  ORGID            VARCHAR2(500 BYTE)           NOT NULL,
  STORAGEID        VARCHAR2(500 BYTE)           NOT NULL,
  FLAG             VARCHAR2(10 BYTE)            NOT NULL,
  TRANSACTIONCODE  VARCHAR2(100 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSAPSTORAGEQUERY
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLSBOM
(
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  SBITEMCODE      VARCHAR2(40 BYTE)             NOT NULL,
  SBSITEMCODE     VARCHAR2(40 BYTE)             NOT NULL,
  SBITEMQTY       NUMBER(16,6)                  NOT NULL,
  SBITEMEFFDATE   NUMBER(8)                     NOT NULL,
  SBWH            VARCHAR2(40 BYTE),
  SEQ             NUMBER(10),
  SBITEMECN       VARCHAR2(40 BYTE),
  SBITEMNAME      VARCHAR2(100 BYTE),
  SBITEMDESC      VARCHAR2(100 BYTE),
  SBITEMSTATUS    VARCHAR2(1 BYTE)              NOT NULL,
  SBITEMLOCATION  VARCHAR2(100 BYTE),
  SBITEMEFFTIME   NUMBER(6)                     NOT NULL,
  SBITEMINVDATE   NUMBER(8)                     NOT NULL,
  SBITEMINVTIME   NUMBER(6)                     NOT NULL,
  SBITEMUOM       VARCHAR2(40 BYTE),
  SBITEMVER       VARCHAR2(40 BYTE),
  SBITEMCONTYPE   VARCHAR2(40 BYTE),
  SBPITEMCODE     VARCHAR2(40 BYTE),
  ALPGR           VARCHAR2(10 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  ORGID           NUMBER(8)                     DEFAULT NULL                  NOT NULL,
  SBOMVER         VARCHAR2(40 BYTE)             NOT NULL,
  ITEMDESC        VARCHAR2(40 BYTE),
  SBFACTORY       VARCHAR2(40 BYTE)             NOT NULL,
  SBUSAGE         VARCHAR2(40 BYTE),
  SBITEMPROJECT   VARCHAR2(40 BYTE)             NOT NULL,
  SBITEMSEQ       VARCHAR2(40 BYTE)             NOT NULL,
  LOCATION        VARCHAR2(400 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSBOM
 ADD PRIMARY KEY
 (ITEMCODE, SBITEMCODE, SBOMVER, SBITEMPROJECT, SBITEMSEQ, ORGID);

CREATE TABLE TBLSMTALERT
(
  ALERTSEQ            NUMBER(10)                NOT NULL,
  ALERTTYPE           VARCHAR2(40 BYTE),
  PRODUCTCODE         VARCHAR2(40 BYTE),
  MOCODE              VARCHAR2(40 BYTE),
  MACHINECODE         VARCHAR2(40 BYTE),
  MACHINESTATIONCODE  VARCHAR2(40 BYTE),
  FEEDERCODE          VARCHAR2(40 BYTE),
  FEEDERMAXCOUNT      NUMBER(10)                NOT NULL,
  FEEDERALERTCOUNT    NUMBER(10)                NOT NULL,
  FEEDERUSEDCOUNT     NUMBER(10)                NOT NULL,
  REELNO              VARCHAR2(40 BYTE),
  REELQTY             NUMBER(15,5)              NOT NULL,
  REELUSEDQTY         NUMBER(15,5)              NOT NULL,
  ALERTDATE           NUMBER(8)                 NOT NULL,
  ALERTTIME           NUMBER(6)                 NOT NULL,
  ALERTSTATUS         VARCHAR2(40 BYTE),
  ALERTLEVEL          VARCHAR2(40 BYTE),
  MAINTAINUSER        VARCHAR2(40 BYTE),
  MAINTAINDATE        NUMBER(8)                 NOT NULL,
  MAINTAINTIME        NUMBER(6)                 NOT NULL,
  SSCODE              VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTALERT
 ADD PRIMARY KEY
 (ALERTSEQ);

CREATE TABLE TBLSMTCHECKMATERIAL
(
  CHECKID      NUMBER(10)                       NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  ITEMCODE     VARCHAR2(40 BYTE),
  CHECKRESULT  VARCHAR2(40 BYTE),
  CHECKUSER    VARCHAR2(40 BYTE),
  CHECKDATE    NUMBER(8)                        NOT NULL,
  CHECKTIME    NUMBER(6)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE),
  SSCODE       VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTCHECKMATERIAL
 ADD PRIMARY KEY
 (CHECKID);

CREATE TABLE TBLSMTCHECKMATERIALDTL
(
  CHECKID             NUMBER(10)                NOT NULL,
  SEQ                 NUMBER(10)                NOT NULL,
  TYPE                VARCHAR2(40 BYTE),
  MACHINECODE         VARCHAR2(40 BYTE),
  MACHINESTATIONCODE  VARCHAR2(40 BYTE),
  PRODUCTCODE         VARCHAR2(40 BYTE),
  MATERIALCODE        VARCHAR2(40 BYTE),
  SOURCEMATERIALCODE  VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  SMTQTY              NUMBER(15,5)              NOT NULL,
  MATERIALNAME        VARCHAR2(100 BYTE),
  BOMQTY              NUMBER(15,5)              NOT NULL,
  BOMUOM              VARCHAR2(100 BYTE),
  CHECKRESULT         VARCHAR2(1 BYTE)          NOT NULL,
  CHECKDESC           VARCHAR2(100 BYTE),
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTCHECKMATERIALDTL
 ADD PRIMARY KEY
 (CHECKID, SEQ);

CREATE TABLE TBLSMTFEEDERMATERIAL
(
  MACHINECODE         VARCHAR2(40 BYTE)         NOT NULL,
  MACHINESTATIONCODE  VARCHAR2(40 BYTE)         NOT NULL,
  PRODUCTCODE         VARCHAR2(40 BYTE)         NOT NULL,
  MATERIALCODE        VARCHAR2(40 BYTE)         NOT NULL,
  SOURCEMATERIALCODE  VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  QTY                 NUMBER(15,5)              NOT NULL,
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE)         NOT NULL,
  TBLGRP              VARCHAR2(40 BYTE)         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_SMTFEEDERMATERIAL_SPEC ON TBLSMTFEEDERMATERIAL
(FEEDERSPECCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSMTFEEDERMATERIAL
 ADD PRIMARY KEY
 (MACHINECODE, MACHINESTATIONCODE, PRODUCTCODE, MATERIALCODE, SSCODE, TBLGRP);

CREATE TABLE TBLSMTFEEDERMATERIALIMPLOG
(
  LOGNO               NUMBER(10)                NOT NULL,
  SEQ                 NUMBER(10)                NOT NULL,
  IMPUSER             VARCHAR2(40 BYTE),
  IMPDATE             NUMBER(8)                 NOT NULL,
  IMPTIME             NUMBER(6)                 NOT NULL,
  CHECKRESULT         VARCHAR2(1 BYTE)          NOT NULL,
  CHECKDESC           VARCHAR2(100 BYTE),
  MACHINECODE         VARCHAR2(40 BYTE),
  MACHINESTATIONCODE  VARCHAR2(40 BYTE),
  PRODUCTCODE         VARCHAR2(40 BYTE),
  MATERIALCODE        VARCHAR2(40 BYTE),
  SOURCEMATERIALCODE  VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  QTY                 NUMBER(15,5)              NOT NULL,
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE),
  TBLGRP              VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTFEEDERMATERIALIMPLOG
 ADD PRIMARY KEY
 (LOGNO, SEQ);

CREATE TABLE TBLSMTLINECTLLOG
(
  LOGID               NUMBER(10)                NOT NULL,
  PRODUCTCODE         VARCHAR2(40 BYTE),
  MOCODE              VARCHAR2(40 BYTE),
  MACHINECODE         VARCHAR2(40 BYTE),
  MACHINESTATIONCODE  VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  FEEDERCODE          VARCHAR2(40 BYTE),
  REELNO              VARCHAR2(40 BYTE),
  MATERIALCODE        VARCHAR2(40 BYTE),
  UNITQTY             NUMBER(15,5)              NOT NULL,
  REELQTY             NUMBER(15,5)              NOT NULL,
  REELUSEDQTY         NUMBER(15,5)              NOT NULL,
  NEXTREELNO          VARCHAR2(40 BYTE),
  OPERATIONTYPE       VARCHAR2(40 BYTE),
  ENABLED             VARCHAR2(1 BYTE)          NOT NULL,
  REELCEASEFLAG       VARCHAR2(1 BYTE)          NOT NULL,
  OPERESCODE          VARCHAR2(40 BYTE),
  OPESSCODE           VARCHAR2(40 BYTE),
  LINESTATUSOLD       VARCHAR2(1 BYTE)          NOT NULL,
  LINESTATUS          VARCHAR2(1 BYTE)          NOT NULL,
  CHGLINESTATUS       VARCHAR2(1 BYTE)          NOT NULL,
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTLINECTLLOG
 ADD PRIMARY KEY
 (LOGID);

CREATE TABLE TBLSMTLOG
(
  LOGDATE   VARCHAR2(40 BYTE),
  LINECODE  VARCHAR2(40 BYTE),
  QTY       VARCHAR2(40 BYTE),
  UNITQTY   VARCHAR2(40 BYTE),
  COSTTIME  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLSMTMACHINEACTIVEINNO
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MACHINECODE  VARCHAR2(40 BYTE)                NOT NULL,
  INNO         NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTMACHINEACTIVEINNO
 ADD PRIMARY KEY
 (MOCODE, SSCODE, MACHINECODE);

CREATE TABLE TBLSMTMACHINEDISCARD
(
  MOCODE              VARCHAR2(40 BYTE)         NOT NULL,
  SSCODE              VARCHAR2(40 BYTE)         NOT NULL,
  MATERIALCODE        VARCHAR2(40 BYTE)         NOT NULL,
  MACHINESTATIONCODE  VARCHAR2(40 BYTE)         NOT NULL,
  PICKUPCOUNT         NUMBER(15,5)              NOT NULL,
  REJECTPARTS         NUMBER(15,5)              NOT NULL,
  NOPICKUP            NUMBER(15,5)              NOT NULL,
  ERRORPARTS          NUMBER(15,5)              NOT NULL,
  DISLODGEDPARTS      NUMBER(15,5)              NOT NULL,
  MUSER               VARCHAR2(40 BYTE),
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTMACHINEDISCARD
 ADD PRIMARY KEY
 (MOCODE, SSCODE, MATERIALCODE, MACHINESTATIONCODE);

CREATE TABLE TBLSMTMACHINEINNO
(
  INNO                NUMBER(10)                NOT NULL,
  INNOSEQ             NUMBER(10)                NOT NULL,
  MACHINECODE         VARCHAR2(40 BYTE),
  MACHINESTATIONCODE  VARCHAR2(40 BYTE),
  PRODUCTCODE         VARCHAR2(40 BYTE),
  MOCODE              VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  FEEDERCODE          VARCHAR2(40 BYTE),
  REELNO              VARCHAR2(40 BYTE),
  MATERIALCODE        VARCHAR2(40 BYTE),
  UNITQTY             NUMBER(15,5)              NOT NULL,
  LOTNO               VARCHAR2(40 BYTE),
  DATECODE            VARCHAR2(40 BYTE),
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_SMTMACHINEINNO_QUERY ON TBLSMTMACHINEINNO
(MATERIALCODE, REELNO, LOTNO, DATECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSMTMACHINEINNO
 ADD PRIMARY KEY
 (INNO, INNOSEQ);

CREATE TABLE TBLSMTRCARDINNO
(
  INNO      NUMBER(10)                          NOT NULL,
  RCARD     VARCHAR2(40 BYTE)                   NOT NULL,
  RCARDSEQ  NUMBER(10)                          NOT NULL,
  MDATE     NUMBER(8),
  MOSEQ     NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTRCARDINNO
 ADD PRIMARY KEY
 (INNO, RCARD, RCARDSEQ);

CREATE TABLE TBLSMTRCARDMATERIAL
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(15,5)                   NOT NULL,
  MOCODE         VARCHAR2(40 BYTE),
  ITEMCODE       VARCHAR2(40 BYTE),
  MODELCODE      VARCHAR2(40 BYTE),
  SEGCODE        VARCHAR2(40 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  ROUTECODE      VARCHAR2(40 BYTE),
  OPCODE         VARCHAR2(40 BYTE),
  SHIFTTYPECODE  VARCHAR2(40 BYTE),
  SHIFTCODE      VARCHAR2(40 BYTE),
  TPCODE         VARCHAR2(40 BYTE),
  PRODUCTSTATUS  VARCHAR2(40 BYTE),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(100 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_SMTRCARDMATERIA ON TBLSMTRCARDMATERIAL
(RCARD, RCARDSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX IDX_SMTRCARDMATERIAL_QUERY ON TBLSMTRCARDMATERIAL
(RCARD, MDATE, ITEMCODE, MOCODE, SSCODE, 
RESCODE)
LOGGING
NOPARALLEL;

CREATE INDEX IDX_MES_SMTRCARDMATERIAL_QUERY ON TBLSMTRCARDMATERIAL
(ITEMCODE, MOCODE, MDATE, SSCODE, RESCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSMTRCARDMATERIAL
 ADD CONSTRAINT PK_SMTRCARDMATERIA
 PRIMARY KEY
 (RCARD, RCARDSEQ);

CREATE TABLE TBLSMTSENSORQTY
(
  PRODUCTCODE    VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTDAY       NUMBER(8)                      NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  TPBTIME        NUMBER(6)                      NOT NULL,
  TPETIME        NUMBER(6)                      NOT NULL,
  TPSEQ          NUMBER(10)                     NOT NULL,
  MOBDATE        NUMBER(8)                      NOT NULL,
  MOBTIME        NUMBER(6)                      NOT NULL,
  MOEDATE        NUMBER(8)                      NOT NULL,
  MOETIME        NUMBER(6)                      NOT NULL,
  QTY            NUMBER(15,5)                   NOT NULL,
  MAINTAINUSER   VARCHAR2(40 BYTE),
  MAINTAINDATE   NUMBER(8)                      NOT NULL,
  MAINTAINTIME   NUMBER(6)                      NOT NULL,
  DIFFREASON     VARCHAR2(100 BYTE),
  DIFFMUSER      VARCHAR2(40 BYTE),
  DIFFMDATE      NUMBER(8),
  DIFFMTIME      NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTSENSORQTY
 ADD PRIMARY KEY
 (PRODUCTCODE, SSCODE, MOCODE, SHIFTDAY, SHIFTTYPECODE, SHIFTCODE, TPCODE);

CREATE TABLE TBLSMTTARGETQTY
(
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  PRODUCTCODE    VARCHAR2(40 BYTE),
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE),
  SHIFTCODE      VARCHAR2(40 BYTE),
  TPBTIME        NUMBER(6)                      NOT NULL,
  TPETIME        NUMBER(6)                      NOT NULL,
  TPSEQ          NUMBER(10)                     NOT NULL,
  TPDESC         VARCHAR2(100 BYTE),
  TPQTY          NUMBER(15,5)                   NOT NULL,
  QTYPERHOUR     NUMBER(15,5)                   NOT NULL,
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSMTTARGETQTY
 ADD PRIMARY KEY
 (MOCODE, SSCODE, TPCODE);

CREATE TABLE TBLSOFTVER
(
  VERSIONCODE  VARCHAR2(40 BYTE)                NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  EFFDATE      NUMBER(8)                        NOT NULL,
  INVDATE      NUMBER(8)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8),
  MTIME        NUMBER(6),
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSOFTVER ON TBLSOFTVER
(VERSIONCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSOFTVER
 ADD CONSTRAINT PK_TBLSOFTVER
 PRIMARY KEY
 (VERSIONCODE);

CREATE TABLE TBLSOLDERPASTE
(
  SPID         VARCHAR2(40 BYTE)                NOT NULL,
  PARTNO       VARCHAR2(40 BYTE)                NOT NULL,
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  PRODATE      NUMBER(8)                        NOT NULL,
  EXDATE       NUMBER(8)                        NOT NULL,
  USED         VARCHAR2(1 BYTE)                 NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSOLDERPASTE ON TBLSOLDERPASTE
(SPID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSOLDERPASTE
 ADD CONSTRAINT PK_TBLSOLDERPASTE
 PRIMARY KEY
 (SPID);

CREATE TABLE TBLSOLDERPASTE2ITEM
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SPTYPE       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSOLDERPASTE2ITEM ON TBLSOLDERPASTE2ITEM
(ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSOLDERPASTE2ITEM
 ADD CONSTRAINT PK_TBLSOLDERPASTE2ITEM
 PRIMARY KEY
 (ITEMCODE);

CREATE TABLE TBLSOLDERPASTECONTROL
(
  PARTNO           VARCHAR2(40 BYTE)            NOT NULL,
  TYPE             VARCHAR2(40 BYTE)            NOT NULL,
  RETURNTIMESPAN   NUMBER(15,5)                 NOT NULL,
  OPENTS           NUMBER(15,5)                 NOT NULL,
  UNOPENTS         NUMBER(15,5)                 NOT NULL,
  GUARANTEEPERIOD  NUMBER(10)                   NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSOLDERPASTECONTROL ON TBLSOLDERPASTECONTROL
(PARTNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSOLDERPASTECONTROL
 ADD CONSTRAINT PK_TBLSOLDERPASTECONTROL
 PRIMARY KEY
 (PARTNO);

CREATE TABLE TBLSOLDERPASTEPRO
(
  SPPKID           VARCHAR2(40 BYTE)            NOT NULL,
  SOLDERPASTEID    VARCHAR2(40 BYTE),
  SEQUENCE         NUMBER(6),
  LINECODE         VARCHAR2(40 BYTE),
  MOCODE           VARCHAR2(40 BYTE),
  STATUS           VARCHAR2(40 BYTE),
  SPTYPE           VARCHAR2(40 BYTE),
  LOTNO            VARCHAR2(40 BYTE)            NOT NULL,
  OPENUSER         VARCHAR2(40 BYTE),
  RETRUNUSER       VARCHAR2(40 BYTE),
  RETURNDATE       NUMBER(8),
  RETURNTIME       NUMBER(6),
  OPENDATE         NUMBER(8),
  OPENTIME         NUMBER(6),
  AGITAEUSER       VARCHAR2(40 BYTE),
  AGITATEDATE      NUMBER(8),
  AGITATETIME      NUMBER(6),
  UNVEILUSER       VARCHAR2(40 BYTE),
  UNVEILMDATE      NUMBER(8),
  UNVEILTIME       NUMBER(6),
  RESAVEUSER       VARCHAR2(40 BYTE),
  RESAVEDATE       NUMBER(8),
  RESAVETIME       NUMBER(6),
  UNAVIALUSER      VARCHAR2(40 BYTE),
  UNAVIALDATE      NUMBER(8),
  UNAVIALTIME      NUMBER(6),
  MUSER            VARCHAR2(40 BYTE),
  RETURNTIMESPAN   NUMBER(10),
  RETURNCOUNTTIME  NUMBER(15,5),
  VEILTIMESPAN     NUMBER(10),
  VEILCOUNTTIME    NUMBER(15,5),
  UNVEILTIMESPAN   NUMBER(10),
  UNVEILCOUNTTIME  NUMBER(15,5),
  EXPIREDDATE      NUMBER(8)                    NOT NULL,
  MEMO             VARCHAR2(100 BYTE),
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK476_1 ON TBLSOLDERPASTEPRO
(SPPKID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSOLDERPASTEPRO
 ADD CONSTRAINT PK476_1
 PRIMARY KEY
 (SPPKID);

CREATE TABLE TBLSOLUTION
(
  SOLCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SOLDESC      VARCHAR2(100 BYTE),
  SOLIMP       VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK76 ON TBLSOLUTION
(SOLCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSOLUTION
 ADD CONSTRAINT PK76
 PRIMARY KEY
 (SOLCODE);

CREATE TABLE TBLSPCCONFIG
(
  PARAMNAME    VARCHAR2(40 BYTE)                NOT NULL,
  PARAMVALUE   VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSPCCONFIG ON TBLSPCCONFIG
(PARAMNAME)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSPCCONFIG
 ADD CONSTRAINT PK_TBLSPCCONFIG
 PRIMARY KEY
 (PARAMNAME);

CREATE TABLE TBLSPCDATA
(
  SEQID        NUMBER(15)                       NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE),
  ITEMCODE     VARCHAR2(40 BYTE),
  MOCODE       VARCHAR2(40 BYTE),
  RCARD        VARCHAR2(40 BYTE),
  RCARDSEQ     NUMBER(15),
  SEGCODE      VARCHAR2(40 BYTE),
  LINECODE     VARCHAR2(40 BYTE),
  RESCODE      VARCHAR2(40 BYTE),
  OPCODE       VARCHAR2(40 BYTE),
  TESTRESULT   VARCHAR2(100 BYTE),
  MACHINETOOL  VARCHAR2(100 BYTE),
  TESTUSER     VARCHAR2(40 BYTE),
  TESTDATE     NUMBER(8),
  TESTTIME     NUMBER(6),
  F1           NUMBER(15,5),
  F2           NUMBER(15,5),
  F3           NUMBER(15,5),
  F4           NUMBER(15,5),
  F5           NUMBER(15,5),
  F6           NUMBER(15,5),
  F7           NUMBER(15,5),
  F8           NUMBER(15,5),
  F9           NUMBER(15,5),
  F10          NUMBER(15,5),
  F11          NUMBER(15,5),
  F12          NUMBER(15,5),
  F13          NUMBER(15,5),
  F14          NUMBER(15,5),
  F15          NUMBER(15,5),
  F16          NUMBER(15,5),
  F17          NUMBER(15,5),
  F18          NUMBER(15,5),
  F19          NUMBER(15,5),
  F20          NUMBER(15,5),
  F21          NUMBER(15,5),
  F22          NUMBER(15,5),
  F23          NUMBER(15,5),
  F24          NUMBER(15,5),
  F25          NUMBER(15,5),
  F26          NUMBER(15,5),
  F27          NUMBER(15,5),
  F28          NUMBER(15,5),
  F29          NUMBER(15,5),
  F30          NUMBER(15,5),
  F31          NUMBER(15,5),
  F32          NUMBER(15,5),
  F33          NUMBER(15,5),
  F34          NUMBER(15,5),
  F35          NUMBER(15,5),
  F36          NUMBER(15,5),
  F37          NUMBER(15,5),
  F38          NUMBER(15,5),
  F39          NUMBER(15,5),
  F40          NUMBER(15,5),
  F41          NUMBER(15,5),
  F42          NUMBER(15,5),
  F43          NUMBER(15,5),
  F44          NUMBER(15,5),
  F45          NUMBER(15,5),
  F46          NUMBER(15,5),
  F47          NUMBER(15,5),
  F48          NUMBER(15,5),
  F49          NUMBER(15,5),
  F50          NUMBER(15,5),
  F51          NUMBER(15,5),
  F52          NUMBER(15,5),
  F53          NUMBER(15,5),
  F54          NUMBER(15,5),
  F55          NUMBER(15,5),
  F56          NUMBER(15,5),
  F57          NUMBER(15,5),
  F58          NUMBER(15,5),
  F59          NUMBER(15,5),
  F60          NUMBER(15,5),
  F61          NUMBER(15,5),
  F62          NUMBER(15,5),
  F63          NUMBER(15,5),
  F64          NUMBER(15,5),
  F65          NUMBER(15,5),
  F66          NUMBER(15,5),
  F67          NUMBER(15,5),
  F68          NUMBER(15,5),
  F69          NUMBER(15,5),
  F70          NUMBER(15,5),
  F71          NUMBER(15,5),
  F72          NUMBER(15,5),
  F73          NUMBER(15,5),
  F74          NUMBER(15,5),
  F75          NUMBER(15,5),
  F76          NUMBER(15,5),
  F77          NUMBER(15,5),
  F78          NUMBER(15,5),
  F79          NUMBER(15,5),
  F80          NUMBER(15,5),
  F81          NUMBER(15,5),
  F82          NUMBER(15,5),
  F83          NUMBER(15,5),
  F84          NUMBER(15,5),
  F85          NUMBER(15,5),
  F86          NUMBER(15,5),
  F87          NUMBER(15,5),
  F88          NUMBER(15,5),
  F89          NUMBER(15,5),
  F90          NUMBER(15,5),
  F91          NUMBER(15,5),
  F92          NUMBER(15,5),
  F93          NUMBER(15,5),
  F94          NUMBER(15,5),
  F95          NUMBER(15,5),
  F96          NUMBER(15,5),
  F97          NUMBER(15,5),
  F98          NUMBER(15,5),
  F99          NUMBER(15,5),
  F100         NUMBER(15,5),
  F101         NUMBER(15,5),
  F102         NUMBER(15,5),
  F103         NUMBER(15,5),
  F104         NUMBER(15,5),
  F105         NUMBER(15,5),
  F106         NUMBER(15,5),
  F107         NUMBER(15,5),
  F108         NUMBER(15,5),
  F109         NUMBER(15,5),
  F110         NUMBER(15,5),
  F111         NUMBER(15,5),
  F112         NUMBER(15,5),
  F113         NUMBER(15,5),
  F114         NUMBER(15,5),
  F115         NUMBER(15,5),
  F116         NUMBER(15,5),
  F117         NUMBER(15,5),
  F118         NUMBER(15,5),
  F119         NUMBER(15,5),
  F120         NUMBER(15,5),
  F121         NUMBER(15,5),
  F122         NUMBER(15,5),
  F123         NUMBER(15,5),
  F124         NUMBER(15,5),
  F125         NUMBER(15,5),
  F126         NUMBER(15,5),
  F127         NUMBER(15,5),
  F128         NUMBER(15,5),
  F129         NUMBER(15,5),
  F130         NUMBER(15,5),
  F131         NUMBER(15,5),
  F132         NUMBER(15,5),
  F133         NUMBER(15,5),
  F134         NUMBER(15,5),
  F135         NUMBER(15,5),
  F136         NUMBER(15,5),
  F137         NUMBER(15,5),
  F138         NUMBER(15,5),
  F139         NUMBER(15,5),
  F140         NUMBER(15,5),
  F141         NUMBER(15,5),
  F142         NUMBER(15,5),
  F143         NUMBER(15,5),
  F144         NUMBER(15,5),
  F145         NUMBER(15,5),
  F146         NUMBER(15,5),
  F147         NUMBER(15,5),
  F148         NUMBER(15,5),
  F149         NUMBER(15,5),
  F150         NUMBER(15,5),
  F151         NUMBER(15,5),
  F152         NUMBER(15,5),
  F153         NUMBER(15,5),
  F154         NUMBER(15,5),
  F155         NUMBER(15,5),
  F156         NUMBER(15,5),
  F157         NUMBER(15,5),
  F158         NUMBER(15,5),
  F159         NUMBER(15,5),
  F160         NUMBER(15,5),
  F161         NUMBER(15,5),
  F162         NUMBER(15,5),
  F163         NUMBER(15,5),
  F164         NUMBER(15,5),
  F165         NUMBER(15,5),
  F166         NUMBER(15,5),
  F167         NUMBER(15,5),
  F168         NUMBER(15,5),
  F169         NUMBER(15,5),
  F170         NUMBER(15,5),
  F171         NUMBER(15,5),
  F172         NUMBER(15,5),
  F173         NUMBER(15,5),
  F174         NUMBER(15,5),
  F175         NUMBER(15,5),
  F176         NUMBER(15,5),
  F177         NUMBER(15,5),
  F178         NUMBER(15,5),
  F179         NUMBER(15,5),
  F180         NUMBER(15,5),
  F181         NUMBER(15,5),
  F182         NUMBER(15,5),
  F183         NUMBER(15,5),
  F184         NUMBER(15,5),
  F185         NUMBER(15,5),
  F186         NUMBER(15,5),
  F187         NUMBER(15,5),
  F188         NUMBER(15,5),
  F189         NUMBER(15,5),
  F190         NUMBER(15,5),
  F191         NUMBER(15,5),
  F192         NUMBER(15,5),
  F193         NUMBER(15,5),
  F194         NUMBER(15,5),
  F195         NUMBER(15,5),
  F196         NUMBER(15,5),
  F197         NUMBER(15,5),
  F198         NUMBER(15,5),
  F199         NUMBER(15,5),
  F200         NUMBER(15,5),
  F201         NUMBER(15,5),
  F202         NUMBER(15,5),
  F203         NUMBER(15,5),
  F204         NUMBER(15,5),
  F205         NUMBER(15,5),
  F206         NUMBER(15,5),
  F207         NUMBER(15,5),
  F208         NUMBER(15,5),
  F209         NUMBER(15,5),
  F210         NUMBER(15,5),
  F211         NUMBER(15,5),
  F212         NUMBER(15,5),
  F213         NUMBER(15,5),
  F214         NUMBER(15,5),
  F215         NUMBER(15,5),
  F216         NUMBER(15,5),
  F217         NUMBER(15,5),
  F218         NUMBER(15,5),
  F219         NUMBER(15,5),
  F220         NUMBER(15,5),
  F221         NUMBER(15,5),
  F222         NUMBER(15,5),
  F223         NUMBER(15,5),
  F224         NUMBER(15,5),
  F225         NUMBER(15,5),
  F226         NUMBER(15,5),
  F227         NUMBER(15,5),
  F228         NUMBER(15,5),
  F229         NUMBER(15,5),
  F230         NUMBER(15,5),
  F231         NUMBER(15,5),
  F232         NUMBER(15,5),
  F233         NUMBER(15,5),
  F234         NUMBER(15,5),
  F235         NUMBER(15,5),
  F236         NUMBER(15,5),
  F237         NUMBER(15,5),
  F238         NUMBER(15,5),
  F239         NUMBER(15,5),
  F240         NUMBER(15,5),
  F241         NUMBER(15,5),
  F242         NUMBER(15,5),
  F243         NUMBER(15,5),
  F244         NUMBER(15,5),
  F245         NUMBER(15,5),
  F246         NUMBER(15,5),
  F247         NUMBER(15,5),
  F248         NUMBER(15,5),
  F249         NUMBER(15,5),
  F250         NUMBER(15,5),
  F251         NUMBER(15,5),
  F252         NUMBER(15,5),
  F253         NUMBER(15,5),
  F254         NUMBER(15,5),
  F255         NUMBER(15,5),
  F256         NUMBER(15,5),
  F257         NUMBER(15,5),
  F258         NUMBER(15,5),
  F259         NUMBER(15,5),
  F260         NUMBER(15,5),
  F261         NUMBER(15,5),
  F262         NUMBER(15,5),
  F263         NUMBER(15,5),
  F264         NUMBER(15,5),
  F265         NUMBER(15,5),
  F266         NUMBER(15,5),
  F267         NUMBER(15,5),
  F268         NUMBER(15,5),
  F269         NUMBER(15,5),
  F270         NUMBER(15,5),
  F271         NUMBER(15,5),
  F272         NUMBER(15,5),
  F273         NUMBER(15,5),
  F274         NUMBER(15,5),
  F275         NUMBER(15,5),
  F276         NUMBER(15,5),
  F277         NUMBER(15,5),
  F278         NUMBER(15,5),
  F279         NUMBER(15,5),
  F280         NUMBER(15,5),
  F281         NUMBER(15,5),
  F282         NUMBER(15,5),
  F283         NUMBER(15,5),
  F284         NUMBER(15,5),
  F285         NUMBER(15,5),
  F286         NUMBER(15,5),
  F287         NUMBER(15,5),
  F288         NUMBER(15,5),
  F289         NUMBER(15,5),
  F290         NUMBER(15,5),
  F291         NUMBER(15,5),
  F292         NUMBER(15,5),
  F293         NUMBER(15,5),
  F294         NUMBER(15,5),
  F295         NUMBER(15,5),
  F296         NUMBER(15,5),
  F297         NUMBER(15,5),
  F298         NUMBER(15,5),
  F299         NUMBER(15,5),
  F300         NUMBER(15,5),
  F301         NUMBER(15,5),
  F302         NUMBER(15,5),
  F303         NUMBER(15,5),
  F304         NUMBER(15,5),
  F305         NUMBER(15,5),
  F306         NUMBER(15,5),
  F307         NUMBER(15,5),
  F308         NUMBER(15,5),
  F309         NUMBER(15,5),
  F310         NUMBER(15,5),
  F311         NUMBER(15,5),
  F312         NUMBER(15,5),
  F313         NUMBER(15,5),
  F314         NUMBER(15,5),
  F315         NUMBER(15,5),
  F316         NUMBER(15,5),
  F317         NUMBER(15,5),
  F318         NUMBER(15,5),
  F319         NUMBER(15,5),
  F320         NUMBER(15,5),
  F321         NUMBER(15,5),
  F322         NUMBER(15,5),
  F323         NUMBER(15,5),
  F324         NUMBER(15,5),
  F325         NUMBER(15,5),
  F326         NUMBER(15,5),
  F327         NUMBER(15,5),
  F328         NUMBER(15,5),
  F329         NUMBER(15,5),
  F330         NUMBER(15,5),
  F331         NUMBER(15,5),
  F332         NUMBER(15,5),
  F333         NUMBER(15,5),
  F334         NUMBER(15,5),
  F335         NUMBER(15,5),
  F336         NUMBER(15,5),
  F337         NUMBER(15,5),
  F338         NUMBER(15,5),
  F339         NUMBER(15,5),
  F340         NUMBER(15,5),
  F341         NUMBER(15,5),
  F342         NUMBER(15,5),
  F343         NUMBER(15,5),
  F344         NUMBER(15,5),
  F345         NUMBER(15,5),
  F346         NUMBER(15,5),
  F347         NUMBER(15,5),
  F348         NUMBER(15,5),
  F349         NUMBER(15,5),
  F350         NUMBER(15,5),
  F351         NUMBER(15,5),
  F352         NUMBER(15,5),
  F353         NUMBER(15,5),
  F354         NUMBER(15,5),
  F355         NUMBER(15,5),
  F356         NUMBER(15,5),
  F357         NUMBER(15,5),
  F358         NUMBER(15,5),
  F359         NUMBER(15,5),
  F360         NUMBER(15,5),
  F361         NUMBER(15,5),
  F362         NUMBER(15,5),
  F363         NUMBER(15,5),
  F364         NUMBER(15,5),
  F365         NUMBER(15,5),
  F366         NUMBER(15,5),
  F367         NUMBER(15,5),
  F368         NUMBER(15,5),
  F369         NUMBER(15,5),
  F370         NUMBER(15,5),
  F371         NUMBER(15,5),
  F372         NUMBER(15,5),
  F373         NUMBER(15,5),
  F374         NUMBER(15,5),
  F375         NUMBER(15,5),
  F376         NUMBER(15,5),
  F377         NUMBER(15,5),
  F378         NUMBER(15,5),
  F379         NUMBER(15,5),
  F380         NUMBER(15,5),
  F381         NUMBER(15,5),
  F382         NUMBER(15,5),
  F383         NUMBER(15,5),
  F384         NUMBER(15,5),
  F385         NUMBER(15,5),
  F386         NUMBER(15,5),
  F387         NUMBER(15,5),
  F388         NUMBER(15,5),
  F389         NUMBER(15,5),
  F390         NUMBER(15,5),
  F391         NUMBER(15,5),
  F392         NUMBER(15,5),
  F393         NUMBER(15,5),
  F394         NUMBER(15,5),
  F395         NUMBER(15,5),
  F396         NUMBER(15,5),
  F397         NUMBER(15,5),
  F398         NUMBER(15,5),
  F399         NUMBER(15,5),
  F400         NUMBER(15,5),
  LOTNO        VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLSPCDATA ON TBLSPCDATA
(ITEMCODE, MOCODE, RCARD, RCARDSEQ, TESTDATE, 
TESTTIME, LINECODE, RESCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSPCDATA
 ADD PRIMARY KEY
 (SEQID);

CREATE TABLE TBLSPCOBJECT
(
  OBJECTCODE   VARCHAR2(40 BYTE)                NOT NULL,
  OBJECTNAME   VARCHAR2(100 BYTE),
  GRAPHTYPE    VARCHAR2(40 BYTE)                NOT NULL,
  DATERANGE    VARCHAR2(40 BYTE)                NOT NULL,
  MAXDAY       NUMBER(10)                       NOT NULL,
  OPLIST       VARCHAR2(100 BYTE)               NOT NULL,
  TESTTIMES    NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSPCOBJECT ON TBLSPCOBJECT
(OBJECTCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSPCOBJECT
 ADD CONSTRAINT PK_TBLSPCOBJECT
 PRIMARY KEY
 (OBJECTCODE);

CREATE TABLE TBLSPCOBJECTSTORE
(
  OBJECTCODE   VARCHAR2(40 BYTE)                NOT NULL,
  GROUPSEQ     NUMBER(10)                       NOT NULL,
  STORECOLUMN  NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSPCOBJECTSTORE
 ADD PRIMARY KEY
 (OBJECTCODE, GROUPSEQ);

CREATE TABLE TBLSS
(
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SSSEQ          NUMBER(10)                     NOT NULL,
  SSDESC         VARCHAR2(100 BYTE),
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  SSTYPE         VARCHAR2(40 BYTE),
  CARTONNOCODE   VARCHAR2(40 BYTE),
  ORGID          NUMBER(8),
  SHIFTTYPECODE  VARCHAR2(40 BYTE),
  BIGSSCODE      VARCHAR2(40 BYTE),
  SAVEINSTOCK    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX UI_TBLSS ON TBLSS
(BIGSSCODE, SSSEQ)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK30 ON TBLSS
(SSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSS
 ADD CONSTRAINT PK30
 PRIMARY KEY
 (SSCODE);

CREATE TABLE TBLSTACK
(
  STACKCODE    VARCHAR2(40 BYTE)                NOT NULL,
  STORAGECODE  VARCHAR2(40 BYTE)                NOT NULL,
  STACKDESC    VARCHAR2(100 BYTE)               NOT NULL,
  CAPACITY     NUMBER(10)                       NOT NULL,
  ORGID        NUMBER(8)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  ISONEITEM    VARCHAR2(40 BYTE)                DEFAULT 'Y'
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLSTACK_DESC ON TBLSTACK
(STACKDESC)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLSTACK_STORAGESTACK ON TBLSTACK
(STORAGECODE, STACKCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSTACK
 ADD PRIMARY KEY
 (STACKCODE);

CREATE TABLE TBLSTACK2RCARD
(
  STORAGECODE     VARCHAR2(40 BYTE)             NOT NULL,
  STACKCODE       VARCHAR2(40 BYTE)             NOT NULL,
  SERIALNO        VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  BUSINESSREASON  VARCHAR2(40 BYTE)             NOT NULL,
  COMPANY         VARCHAR2(100 BYTE)            NOT NULL,
  ITEMGRADE       VARCHAR2(40 BYTE)             NOT NULL,
  INUSER          VARCHAR2(40 BYTE)             NOT NULL,
  INDATE          NUMBER(8)                     NOT NULL,
  INTIME          NUMBER(6)                     NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  TRANSINSERIAL   NUMBER(22)                    DEFAULT 0                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLSTACKTORCARD_INDATE ON TBLSTACK2RCARD
(INDATE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLSTACKTORCARD_STKSTG ON TBLSTACK2RCARD
(STACKCODE, STORAGECODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLSTACKTORCARD_ITEMCODE ON TBLSTACK2RCARD
(ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSTACK2RCARD
 ADD PRIMARY KEY
 (SERIALNO);

CREATE TABLE TBLSTATION
(
  STATIONCODE  VARCHAR2(40 BYTE)                NOT NULL,
  STATIONDESC  VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  RESCODE      VARCHAR2(40 BYTE)                NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK90 ON TBLSTATION
(STATIONCODE, RESCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSTATION
 ADD CONSTRAINT PK90
 PRIMARY KEY
 (STATIONCODE, RESCODE);

CREATE TABLE TBLSTORAGE
(
  STORAGECODE  VARCHAR2(40 BYTE)                NOT NULL,
  STORAGENAME  VARCHAR2(100 BYTE),
  ORGID        NUMBER(8)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8),
  MTIME        NUMBER(6),
  EATTRIBUTE1  VARCHAR2(100 BYTE),
  SAPSTORAGE   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSTORAGE ON TBLSTORAGE
(ORGID, STORAGECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSTORAGE
 ADD CONSTRAINT PK_TBLSTORAGE
 PRIMARY KEY
 (ORGID, STORAGECODE);

CREATE TABLE TBLSYSERROR
(
  SYSERRCODE   VARCHAR2(40 BYTE)                NOT NULL,
  ERRMSG       VARCHAR2(100 BYTE)               NOT NULL,
  INNERERRMSG  VARCHAR2(100 BYTE),
  TRGMDLCODE   VARCHAR2(40 BYTE),
  TRIGACTION   VARCHAR2(40 BYTE),
  SENDUSER     VARCHAR2(40 BYTE)                NOT NULL,
  SENDDATE     NUMBER(8)                        NOT NULL,
  SENDTIME     NUMBER(6)                        NOT NULL,
  ISRES        VARCHAR2(1 BYTE)                 NOT NULL,
  RESNOTES     VARCHAR2(100 BYTE),
  RESUSER      VARCHAR2(40 BYTE),
  RESDATE      NUMBER(8),
  RESTIME      NUMBER(6),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK267 ON TBLSYSERROR
(SYSERRCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSYSERROR
 ADD CONSTRAINT PK267
 PRIMARY KEY
 (SYSERRCODE);

CREATE TABLE TBLSYSPARAM
(
  PARAMCODE       VARCHAR2(40 BYTE)             NOT NULL,
  PARAMGROUPCODE  VARCHAR2(40 BYTE)             NOT NULL,
  PARAMALIAS      VARCHAR2(40 BYTE),
  PARAMDESC       VARCHAR2(100 BYTE),
  PARAMVALUE      VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  ISACTIVE        VARCHAR2(1 BYTE)              NOT NULL,
  ISSYS           VARCHAR2(1 BYTE)              NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  PARENTPARAM     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLSYSPARAMGROUP
(
  PARAMGROUPCODE  VARCHAR2(40 BYTE)             NOT NULL,
  PARAMGROUPTYPE  VARCHAR2(40 BYTE)             NOT NULL,
  PARAMGROUPDESC  VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  ISSYS           VARCHAR2(1 BYTE)              NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK54 ON TBLSYSPARAMGROUP
(PARAMGROUPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSYSPARAMGROUP
 ADD CONSTRAINT PK54
 PRIMARY KEY
 (PARAMGROUPCODE);

CREATE TABLE TBLTEMPREWORKLOTNO
(
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTEMPREWORKLOTNO
 ADD PRIMARY KEY
 (LOTNO);

CREATE TABLE TBLTEMPREWORKRCARD
(
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  PALLETCODE   VARCHAR2(40 BYTE)                NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTEMPREWORKRCARD
 ADD PRIMARY KEY
 (LOTNO, RCARD);

CREATE TABLE TBLTICKETSEQ
(
  NEXTSEQ  NUMBER(10)                           NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTICKETSEQ
 ADD PRIMARY KEY
 (NEXTSEQ);

CREATE TABLE TBLTIMEDIMENSION
(
  DDATE     NUMBER(8)                           NOT NULL,
  DWEEK     NUMBER(8)                           NOT NULL,
  DMONTH    NUMBER(8)                           NOT NULL,
  DQUARTER  NUMBER(8)                           NOT NULL,
  YEAR      NUMBER(8)                           NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTIMEDIMENSION
 ADD PRIMARY KEY
 (DDATE);

CREATE TABLE TBLTP
(
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPDESC         VARCHAR2(100 BYTE),
  TPSEQ          NUMBER(6)                      NOT NULL,
  TPTYPE         VARCHAR2(40 BYTE)              NOT NULL,
  TPBTIME        NUMBER(6)                      NOT NULL,
  TPETIME        NUMBER(6)                      NOT NULL,
  ISOVERDATE     VARCHAR2(1 BYTE)               NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK28 ON TBLTP
(TPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTP
 ADD CONSTRAINT PK28
 PRIMARY KEY
 (TPCODE);

CREATE TABLE TBLTRANSTYPE
(
  TRANSTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  TRANSTYPENAME  VARCHAR2(40 BYTE),
  TRANSTYPEESC   VARCHAR2(100 BYTE),
  ISBYMO         VARCHAR2(1 BYTE),
  TRANSPREFIX    VARCHAR2(40 BYTE),
  ISINIT         VARCHAR2(1 BYTE),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTRANSTYPE
 ADD PRIMARY KEY
 (TRANSTYPECODE);

CREATE TABLE TBLTRY
(
  TRYCODE         VARCHAR2(40 BYTE)             NOT NULL,
  STATUS          VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE),
  PLANQTY         NUMBER(22),
  ACTUALQTY       NUMBER(22),
  TRYDOCUMENT     VARCHAR2(500 BYTE),
  DEPT            VARCHAR2(100 BYTE),
  VENDORNAME      VARCHAR2(100 BYTE),
  RESULT          VARCHAR2(200 BYTE),
  MEMO            VARCHAR2(1000 BYTE),
  LINKLOT         VARCHAR2(40 BYTE),
  CUSER           VARCHAR2(40 BYTE)             NOT NULL,
  CDATE           NUMBER(8)                     NOT NULL,
  CTIME           NUMBER(6)                     NOT NULL,
  RUSER           VARCHAR2(40 BYTE),
  RDATE           NUMBER(8),
  RTIME           NUMBER(6),
  FUSER           VARCHAR2(40 BYTE),
  FDATE           NUMBER(8),
  FTIME           NUMBER(6),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  TRYTYPE         VARCHAR2(40 BYTE)             DEFAULT ' '                   NOT NULL,
  TRYREASON       VARCHAR2(1000 BYTE),
  SOFTVERSION     VARCHAR2(40 BYTE),
  WAITTRY         VARCHAR2(40 BYTE),
  CHANGE          VARCHAR2(40 BYTE),
  BURNINDURATION  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX TBLTRY_VENDORNAME ON TBLTRY
(VENDORNAME)
LOGGING
NOPARALLEL;

CREATE INDEX TBLTRY_DEPT ON TBLTRY
(DEPT)
LOGGING
NOPARALLEL;

CREATE INDEX TBLTRY_ITEMCODE ON TBLTRY
(ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX TBLTRY_CDATE_CUSER ON TBLTRY
(CDATE, CUSER)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTRY
 ADD PRIMARY KEY
 (TRYCODE);

CREATE TABLE TBLTRY2LOT
(
  TRYCODE      VARCHAR2(40 BYTE)                NOT NULL,
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTRY2LOT
 ADD PRIMARY KEY
 (LOTNO, TRYCODE);

CREATE TABLE TBLTRY2RCARD
(
  TRYCODE      VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTRY2RCARD
 ADD PRIMARY KEY
 (RCARD, ITEMCODE, TRYCODE);

CREATE TABLE TBLTS
(
  TSID             VARCHAR2(40 BYTE)            NOT NULL,
  RCARD            VARCHAR2(40 BYTE)            NOT NULL,
  RCARDSEQ         NUMBER(10)                   NOT NULL,
  TCARD            VARCHAR2(40 BYTE),
  TCARDSEQ         NUMBER(10)                   NOT NULL,
  SCARD            VARCHAR2(40 BYTE),
  SCARDSEQ         NUMBER(10)                   NOT NULL,
  CARDTYPE         VARCHAR2(40 BYTE)            NOT NULL,
  RRCARD           VARCHAR2(40 BYTE),
  MODELCODE        VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  MOCODE           VARCHAR2(40 BYTE),
  FRMROUTECODE     VARCHAR2(40 BYTE),
  FRMOPCODE        VARCHAR2(40 BYTE),
  FRMSEGCODE       VARCHAR2(40 BYTE),
  FRMSSCODE        VARCHAR2(40 BYTE),
  FRMRESCODE       VARCHAR2(40 BYTE),
  SHIFTTYPECODE    VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTCODE        VARCHAR2(40 BYTE)            NOT NULL,
  TPCODE           VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTDAY         NUMBER(8)                    NOT NULL,
  FRMUSER          VARCHAR2(40 BYTE)            NOT NULL,
  FRMMEMO          VARCHAR2(100 BYTE),
  FRMDATE          NUMBER(8)                    NOT NULL,
  FRMTIME          NUMBER(6)                    NOT NULL,
  FRMINPUTTYPE     VARCHAR2(40 BYTE)            NOT NULL,
  TSTIMES          NUMBER(10)                   NOT NULL,
  TSSTATUS         VARCHAR2(40 BYTE)            NOT NULL,
  TSUSER           VARCHAR2(40 BYTE),
  TSDATE           NUMBER(8),
  TSTIME           NUMBER(6),
  TSMEMO           VARCHAR2(100 BYTE),
  CRESCODE         VARCHAR2(40 BYTE),
  COPCODE          VARCHAR2(40 BYTE),
  CONFIRMUSER      VARCHAR2(40 BYTE),
  CONFIRMTIME      NUMBER(6),
  CONFIRMDATE      NUMBER(8),
  REFMOCODE        VARCHAR2(40 BYTE),
  REFROUTECODE     VARCHAR2(40 BYTE),
  REFOPCODE        VARCHAR2(40 BYTE),
  REFRESCODE       VARCHAR2(40 BYTE),
  TRANSSTATUS      VARCHAR2(40 BYTE)            NOT NULL,
  TFFULLPATH       VARCHAR2(100 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  TSRESCODE        VARCHAR2(40 BYTE),
  SCRAPCAUSE       VARCHAR2(200 BYTE),
  FRMMONTH         NUMBER(2),
  FRMWEEK          NUMBER(2),
  RMABILLCODE      VARCHAR2(40 BYTE),
  FRMOUTROUTECODE  VARCHAR2(40 BYTE),
  MOSEQ            NUMBER(10),
  TSTYPE           VARCHAR2(40 BYTE),
  TSREPAIRUSER     VARCHAR2(40 BYTE),
  TSREPAIRMDATE    NUMBER(8),
  TSREPAIRMTIME    NUMBER(6)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX TSCRARD ON TBLTS
(RCARD, RCARDSEQ, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX PK40_1_1 ON TBLTS
(TSID, RCARDSEQ, TRANSSTATUS, RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TS_STATUS ON TBLTS
(TSSTATUS)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTS
 ADD PRIMARY KEY
 (TSID);

CREATE TABLE TBLUSER2ORG
(
  USERCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  ORGID       NUMBER(8)                         NOT NULL,
  DEFAULTORG  NUMBER(1)                         NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLUSER2ORG
 ADD PRIMARY KEY
 (USERCODE, ORGID);

CREATE TABLE TBLUSERGROUP
(
  USERGROUPCODE  VARCHAR2(40 BYTE)              NOT NULL,
  USERGROUPDESC  VARCHAR2(100 BYTE),
  USERGROUPTYPE  VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK22 ON TBLUSERGROUP
(USERGROUPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLUSERGROUP
 ADD CONSTRAINT PK22
 PRIMARY KEY
 (USERGROUPCODE);

CREATE TABLE TBLUSERGROUP2FUNCTIONGROUP
(
  USERGROUPCODE      VARCHAR2(40 BYTE)          NOT NULL,
  FUNCTIONGROUPCODE  VARCHAR2(40 BYTE)          NOT NULL,
  VIEWVALUE          VARCHAR2(40 BYTE),
  MUSER              VARCHAR2(40 BYTE)          NOT NULL,
  MTIME              NUMBER(6)                  NOT NULL,
  MDATE              NUMBER(8)                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLUSERGROUP2FUNCTIONGROUP
 ADD PRIMARY KEY
 (USERGROUPCODE, FUNCTIONGROUPCODE);

CREATE TABLE TBLUSERGROUP2ITEM
(
  USERGROUPCODE  VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  ISAVAILABLE    NUMBER(10)                     NOT NULL,
  EATTRIBUTE1    VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PKUGITEIM ON TBLUSERGROUP2ITEM
(USERGROUPCODE, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLUSERGROUP2ITEM
 ADD CONSTRAINT PKUGITEIM
 PRIMARY KEY
 (USERGROUPCODE, ITEMCODE);

CREATE TABLE TBLUSERGROUP2MODULE_OLD
(
  MDLCODE        VARCHAR2(40 BYTE)              NOT NULL,
  USERGROUPCODE  VARCHAR2(40 BYTE)              NOT NULL,
  VIEWVALUE      VARCHAR2(40 BYTE),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLUSERGROUP2RES
(
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  USERGROUPCODE  VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK250_1 ON TBLUSERGROUP2RES
(RESCODE, USERGROUPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLUSERGROUP2RES
 ADD CONSTRAINT PK250_1
 PRIMARY KEY
 (RESCODE, USERGROUPCODE);

CREATE TABLE TBLUSERGROUP2USER
(
  USERCODE       VARCHAR2(40 BYTE)              NOT NULL,
  USERGROUPCODE  VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK23 ON TBLUSERGROUP2USER
(USERCODE, USERGROUPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLUSERGROUP2USER
 ADD CONSTRAINT PK23
 PRIMARY KEY
 (USERCODE, USERGROUPCODE);

CREATE TABLE TBLVENDOR
(
  VENDORCODE   VARCHAR2(40 BYTE)                NOT NULL,
  VENDORNAME   VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8),
  MTIME        NUMBER(6),
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLVENDOR
 ADD PRIMARY KEY
 (VENDORCODE);

CREATE TABLE TBLVERSIONCOLLECT
(
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  VERSIONINFO  VARCHAR2(100 BYTE)               NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK416 ON TBLVERSIONCOLLECT
(RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLVERSIONCOLLECT
 ADD CONSTRAINT PK416
 PRIMARY KEY
 (RCARD);

CREATE TABLE TBLVERSIONERROR
(
  PKID           VARCHAR2(40 BYTE)              NOT NULL,
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE),
  VERSIONINFO    VARCHAR2(100 BYTE)             NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(100 BYTE),
  MOVERSIONINFO  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK416_2 ON TBLVERSIONERROR
(PKID, RCARD, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLVERSIONERROR
 ADD CONSTRAINT PK416_2
 PRIMARY KEY
 (PKID, RCARD, MOCODE);

CREATE TABLE TBLWAREHOURSE
(
  WHCODE       VARCHAR2(40 BYTE)                NOT NULL,
  FACCODE      VARCHAR2(40 BYTE)                NOT NULL,
  WHDESC       VARCHAR2(100 BYTE),
  WHTYPE       VARCHAR2(40 BYTE),
  MEMO         VARCHAR2(100 BYTE),
  LCYCLECODE   VARCHAR2(40 BYTE),
  LCYCLEDATE   NUMBER(8),
  LCYCLETIME   NUMBER(6),
  WHSTATUS     VARCHAR2(40 BYTE),
  USECOUNT     NUMBER(10),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ISCTRL       VARCHAR2(1 BYTE)                 NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWAREHOURSE
 ADD PRIMARY KEY
 (WHCODE, FACCODE);

CREATE TABLE TBLWH2SSCODE
(
  WHCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  FACCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWH2SSCODE
 ADD PRIMARY KEY
 (WHCODE, SSCODE, FACCODE);

CREATE TABLE TBLWH2SSCODE2
(
  WHCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE),
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  FACCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWH2SSCODE2
 ADD PRIMARY KEY
 (WHCODE, SEGCODE, SSCODE, FACCODE);

CREATE TABLE TBLWHCYCLE
(
  CYCLECODE    VARCHAR2(40 BYTE)                NOT NULL,
  CYCLETYPE    VARCHAR2(40 BYTE),
  SHIFTCODE    VARCHAR2(40 BYTE),
  CFMUSER      VARCHAR2(40 BYTE),
  CFMDATE      NUMBER(8),
  CFMTIME      NUMBER(6),
  WHCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE),
  FACCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWHCYCLE
 ADD PRIMARY KEY
 (CYCLECODE);

CREATE TABLE TBLWHCYLCEDETAIL
(
  CYCLECODE          VARCHAR2(40 BYTE)          NOT NULL,
  ITEMCODE           VARCHAR2(40 BYTE)          NOT NULL,
  FACCODE            VARCHAR2(40 BYTE),
  SEGCODE            VARCHAR2(40 BYTE),
  WHCODE             VARCHAR2(40 BYTE),
  QTY                NUMBER(15,5),
  PHQTY              NUMBER(15,5),
  ADJQTY             NUMBER(15,5),
  ADJUSER            VARCHAR2(40 BYTE),
  ADJDATE            NUMBER(8),
  ADJTIME            NUMBER(6),
  CFMUSER            VARCHAR2(40 BYTE),
  CFMDATE            NUMBER(8),
  CFMTIME            NUMBER(6),
  MUSER              VARCHAR2(40 BYTE)          NOT NULL,
  MDATE              NUMBER(8)                  NOT NULL,
  MTIME              NUMBER(6)                  NOT NULL,
  EATTRIBUTE1        VARCHAR2(40 BYTE),
  LINEQTY            NUMBER(10),
  WAREHOUSE2LINEQTY  NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWHCYLCEDETAIL
 ADD PRIMARY KEY
 (CYCLECODE, ITEMCODE);

CREATE TABLE TBLWHITEM
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ITEMNAME     VARCHAR2(100 BYTE),
  ITEMUOM      VARCHAR2(40 BYTE),
  ITEMCONTROL  VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWHITEM
 ADD PRIMARY KEY
 (ITEMCODE);

CREATE TABLE TBLWHSTOCK
(
  WHCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  FACCODE      VARCHAR2(40 BYTE)                NOT NULL,
  OPENQTY      NUMBER(15,5),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWHSTOCK
 ADD PRIMARY KEY
 (WHCODE, ITEMCODE, FACCODE);

CREATE TABLE TBLWHSTOCK2
(
  WHCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  FACCODE      VARCHAR2(40 BYTE)                NOT NULL,
  OPENQTY      NUMBER(15,5),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWHSTOCK2
 ADD PRIMARY KEY
 (WHCODE, SEGCODE, ITEMCODE, FACCODE);

CREATE TABLE TBLWHTKT
(
  TKTNO            VARCHAR2(40 BYTE)            NOT NULL,
  STCKNO           VARCHAR2(40 BYTE),
  WHCODE           VARCHAR2(40 BYTE),
  SEGCODE          VARCHAR2(40 BYTE),
  FACCODE          VARCHAR2(40 BYTE),
  TOWHCODE         VARCHAR2(40 BYTE),
  TOSEGCODE        VARCHAR2(40 BYTE),
  TOFACCODE        VARCHAR2(40 BYTE),
  TRANSTYPECODE    VARCHAR2(40 BYTE),
  TRANSSTATUS      VARCHAR2(40 BYTE),
  TRANSUSER        VARCHAR2(40 BYTE),
  TRANSACTIONDATE  NUMBER(8),
  TRANSACTIONTIME  NUMBER(6),
  TICKETUSER       VARCHAR2(40 BYTE),
  TICKETDATE       NUMBER(8),
  TICKETTIME       NUMBER(6),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  REFCODE          VARCHAR2(40 BYTE),
  MOCODE           VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWHTKT
 ADD PRIMARY KEY
 (TKTNO);

CREATE TABLE TBLWHTKTDETAIL
(
  TKTNO            VARCHAR2(40 BYTE)            NOT NULL,
  SEQ              NUMBER(10)                   NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE),
  ITEMNANE         VARCHAR2(40 BYTE),
  QTY              NUMBER(15,5),
  ACTQTY           NUMBER(15,5),
  MOCODE           VARCHAR2(40 BYTE),
  TRANSSTATUS      VARCHAR2(40 BYTE),
  TRANSUSER        VARCHAR2(40 BYTE),
  TRANSACTIONDATE  NUMBER(8),
  TRANSACTIONTIME  NUMBER(6),
  TICKETUSER       VARCHAR2(40 BYTE),
  TICKETDATE       NUMBER(8),
  TICKETTIME       NUMBER(6),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  FRMWHQTY         NUMBER(15,5),
  TOWHQTY          NUMBER(15,5)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLWHTKTDETAIL
 ADD PRIMARY KEY
 (TKTNO, SEQ);

CREATE TABLE TBLWORKINGERROR
(
  SEGCODE        VARCHAR2(40 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  INPUTCONTENT   VARCHAR2(200 BYTE),
  FUNCTION       VARCHAR2(200 BYTE),
  FUNCTIONTYPE   VARCHAR2(40 BYTE)              NOT NULL,
  ERRORMSG       VARCHAR2(500 BYTE),
  ERRORMSGCODE   VARCHAR2(200 BYTE),
  SHIFTTYPECODE  VARCHAR2(40 BYTE),
  SHIFTCODE      VARCHAR2(40 BYTE),
  TPCODE         VARCHAR2(40 BYTE),
  SHIFTDAY       NUMBER(8),
  CUSER          VARCHAR2(40 BYTE)              NOT NULL,
  CDATE          NUMBER(8)                      NOT NULL,
  CTIME          NUMBER(6)                      NOT NULL,
  STATUS         VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX TBLWORKINGERROR_INDEX ON TBLWORKINGERROR
(RESCODE, CUSER, CDATE, CTIME)
LOGGING
NOPARALLEL;

CREATE TABLE TCMCS008BF
(
  CCUR  VARCHAR2(40 BYTE),
  RATE  NUMBER(9,5)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TINLC140BF
(
  PDNO  VARCHAR2(9 BYTE),
  ITEM  VARCHAR2(8 BYTE),
  QRDR  NUMBER(7),
  CWAR  VARCHAR2(6 BYTE),
  ORDT  DATE,
  PRDT  DATE,
  DLDT  DATE,
  LINE  VARCHAR2(4 BYTE),
  CLOT  VARCHAR2(6 BYTE),
  ORNO  VARCHAR2(40 BYTE),
  PONO  VARCHAR2(40 BYTE),
  PRIC  NUMBER(14,4),
  CCUR  VARCHAR2(40 BYTE),
  CFRW  VARCHAR2(40 BYTE),
  OFAD  VARCHAR2(40 BYTE),
  STAD  VARCHAR2(40 BYTE),
  COFC  VARCHAR2(40 BYTE),
  CORN  VARCHAR2(40 BYTE),
  DRIC  NUMBER(14,4),
  DCUR  VARCHAR2(40 BYTE),
  EORN  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TINLC141BF
(
  ITEM  VARCHAR2(40 BYTE),
  DSCA  VARCHAR2(40 BYTE),
  CUNI  VARCHAR2(40 BYTE),
  DSCC  VARCHAR2(40 BYTE),
  CITG  VARCHAR2(40 BYTE),
  ITYP  VARCHAR2(40 BYTE),
  CYTM  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TINLC143BF
(
  CADR  VARCHAR2(40 BYTE),
  NAMA  VARCHAR2(40 BYTE),
  NAMB  VARCHAR2(40 BYTE),
  NAMC  VARCHAR2(40 BYTE),
  NAMD  VARCHAR2(40 BYTE),
  HONO  VARCHAR2(40 BYTE),
  SEAK  VARCHAR2(40 BYTE),
  CCTY  VARCHAR2(40 BYTE),
  DSCA  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TTCMCS008300
(
  T$CCUR  VARCHAR2(40 BYTE),
  T$RATE  NUMBER(9,5)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TTINLC140300
(
  T$PDNO  VARCHAR2(9 BYTE),
  T$ITEM  VARCHAR2(8 BYTE),
  T$QRDR  NUMBER(7),
  T$CWAR  VARCHAR2(6 BYTE),
  T$ORDT  DATE,
  T$PRDT  DATE,
  T$DLDT  DATE,
  T$LINE  VARCHAR2(4 BYTE),
  T$CLOT  VARCHAR2(6 BYTE),
  T$ORNO  VARCHAR2(40 BYTE),
  T$PONO  VARCHAR2(40 BYTE),
  T$PRIC  NUMBER(14,4),
  T$CCUR  VARCHAR2(40 BYTE),
  T$CFRW  VARCHAR2(40 BYTE),
  T$OFAD  VARCHAR2(40 BYTE),
  T$STAD  VARCHAR2(40 BYTE),
  T$COFC  VARCHAR2(40 BYTE),
  T$CORN  VARCHAR2(40 BYTE),
  T$DRIC  NUMBER(14,4),
  T$DCUR  VARCHAR2(40 BYTE),
  T$EORN  VARCHAR2(40 BYTE),
  T$OFBP  VARCHAR2(40 BYTE),
  T$STBP  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TTINLC141300
(
  T$ITEM  VARCHAR2(40 BYTE),
  T$DSCA  VARCHAR2(40 BYTE),
  T$CUNI  VARCHAR2(40 BYTE),
  T$DSCC  VARCHAR2(40 BYTE),
  T$CITG  VARCHAR2(40 BYTE),
  T$ITYP  VARCHAR2(40 BYTE),
  T$CYTM  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TTINLC143
(
  T$ITEM  CHAR(47 BYTE),
  T$DSCA  CHAR(30 BYTE),
  T$CUNI  CHAR(3 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TTINLC143300
(
  T$CADR  VARCHAR2(40 BYTE),
  T$NAMA  VARCHAR2(40 BYTE),
  T$NAMB  VARCHAR2(40 BYTE),
  T$NAMC  VARCHAR2(40 BYTE),
  T$NAMD  VARCHAR2(40 BYTE),
  T$HONO  VARCHAR2(40 BYTE),
  T$SEAK  VARCHAR2(40 BYTE),
  T$CCTY  VARCHAR2(40 BYTE),
  T$DSCA  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TTINLC145
(
  T$PDNO  CHAR(9 BYTE),
  T$SITM  CHAR(47 BYTE),
  T$LCID  CHAR(16 BYTE),
  T$SUB1  CHAR(16 BYTE),
  T$SUB2  CHAR(16 BYTE),
  T$SUB3  CHAR(16 BYTE),
  T$SUB4  CHAR(16 BYTE),
  T$SUB5  CHAR(16 BYTE),
  T$SUB6  CHAR(16 BYTE),
  T$PONO  VARCHAR2(10 BYTE)                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TTINLC146
(
  T$ILOT  CHAR(25 BYTE),
  T$ITEM  CHAR(47 BYTE),
  T$LOCA  CHAR(10 BYTE),
  T$SLOT  CHAR(30 BYTE),
  T$EDAT  DATE
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE USERGROUP2ITEM
(
  USERGROUPCODE  VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ISAVAILABLE    NUMBER(6)                      NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE USERGROUP2ITEM
 ADD PRIMARY KEY
 (USERGROUPCODE, ITEMCODE);

CREATE TABLE TBLTSERRORCAUSE
(
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ECSCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  TSID         VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10)                       NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  RRESCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROPCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SOLCODE      VARCHAR2(40 BYTE),
  DUTYCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SOLMEMO      VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  SHIFTDAY     NUMBER(8),
  ECSGCODE     VARCHAR2(40 BYTE)                DEFAULT ' '                   NOT NULL,
  MOSEQ        NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TSERRORCAUSE ON TBLTSERRORCAUSE
(ECODE, ECGCODE, ECSCODE, ECSGCODE, TSID)
LOGGING
NOPARALLEL;

CREATE INDEX PK83 ON TBLTSERRORCAUSE
(ECODE, ECGCODE, TSID, ECSCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TSERRORCAUSE_TSID ON TBLTSERRORCAUSE
(TSID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSERRORCAUSE
 ADD CONSTRAINT PK_TSERRORCAUSE
 PRIMARY KEY
 (ECODE, ECGCODE, ECSCODE, ECSGCODE, TSID);

CREATE TABLE TBLTSERRORCAUSE2COM
(
  ECSCODE         VARCHAR2(40 BYTE)             NOT NULL,
  ECGCODE         VARCHAR2(40 BYTE)             NOT NULL,
  ECODE           VARCHAR2(40 BYTE)             NOT NULL,
  TSID            VARCHAR2(40 BYTE)             NOT NULL,
  ECSGCODE        VARCHAR2(40 BYTE)             NOT NULL,
  ERRORCOMPONENT  VARCHAR2(40 BYTE)             NOT NULL,
  RCARD           VARCHAR2(40 BYTE),
  RCARDSEQ        NUMBER(10),
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  MOCODE          VARCHAR2(40 BYTE),
  RRESCODE        VARCHAR2(40 BYTE)             NOT NULL,
  ROPCODE         VARCHAR2(40 BYTE)             NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  MOSEQ           NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLTSERRORCAUSE2COM ON TBLTSERRORCAUSE2COM
(TSID, ECSCODE, ECGCODE, ECODE, ECSGCODE, 
ERRORCOMPONENT)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSERRORCAUSE2COM
 ADD CONSTRAINT PK_TBLTSERRORCAUSE2COM
 PRIMARY KEY
 (TSID, ECSCODE, ECGCODE, ECODE, ECSGCODE, ERRORCOMPONENT);

CREATE TABLE TBLTSERRORCAUSE2EPART
(
  TSID         VARCHAR2(40 BYTE)                NOT NULL,
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ECSCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  EPART        VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE),
  RCARDSEQ     NUMBER(10),
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  RRESCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROPCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ECSGCODE     VARCHAR2(40 BYTE)                DEFAULT ' '                   NOT NULL,
  MOSEQ        NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TSERRORCAUSE2EPART ON TBLTSERRORCAUSE2EPART
(TSID, ECODE, ECSCODE, ECSGCODE, ECGCODE, 
EPART)
LOGGING
NOPARALLEL;

CREATE INDEX PK81_1_1_1 ON TBLTSERRORCAUSE2EPART
(TSID, EPART, ECGCODE, ECODE, ECSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSERRORCAUSE2EPART
 ADD CONSTRAINT PK_TSERRORCAUSE2EPART
 PRIMARY KEY
 (TSID, ECODE, ECSCODE, ECSGCODE, ECGCODE, EPART);

CREATE TABLE TBLTSERRORCAUSE2LOC
(
  ECSCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ELOC         VARCHAR2(40 BYTE)                NOT NULL,
  AB           VARCHAR2(40 BYTE)                NOT NULL,
  TSID         VARCHAR2(40 BYTE)                NOT NULL,
  SUBELOC      VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE),
  RCARDSEQ     NUMBER(10),
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  RRESCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROPCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ECSGCODE     VARCHAR2(40 BYTE)                DEFAULT ' '                   NOT NULL,
  MOSEQ        NUMBER(10),
  EPART        VARCHAR2(40 BYTE)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TSERRORCAUSE2LOC_TSID ON TBLTSERRORCAUSE2LOC
(TSID)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK_TSERRORCAUSE2LOC ON TBLTSERRORCAUSE2LOC
(ECSCODE, ECSGCODE, ECGCODE, ECODE, ELOC, 
AB, TSID)
LOGGING
NOPARALLEL;

CREATE INDEX PK81_1_1 ON TBLTSERRORCAUSE2LOC
(ECSCODE, ELOC, AB, ECODE, TSID, 
ECGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSERRORCAUSE2LOC
 ADD CONSTRAINT PK_TSERRORCAUSE2LOC
 PRIMARY KEY
 (ECSCODE, ECSGCODE, ECGCODE, ECODE, ELOC, AB, TSID);

CREATE TABLE TBLTSERRORCODE
(
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  TSID         VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE),
  RCARDSEQ     NUMBER(10),
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  MOSEQ        NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK81 ON TBLTSERRORCODE
(ECODE, ECGCODE, TSID)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TSERRORCODE_TSID ON TBLTSERRORCODE
(TSID)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TSERRORCODE_RCARD ON TBLTSERRORCODE
(RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSERRORCODE
 ADD CONSTRAINT PK81
 PRIMARY KEY
 (ECODE, ECGCODE, TSID);

CREATE TABLE TBLTSERRORCODE2LOC
(
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ELOC         VARCHAR2(40 BYTE)                NOT NULL,
  AB           VARCHAR2(40 BYTE)                NOT NULL,
  TSID         VARCHAR2(40 BYTE)                NOT NULL,
  SUBELOC      VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10),
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  MEMO         VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  SHIFTDAY     NUMBER(8),
  MOSEQ        NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLTSERRORCODE2LOC_RCARD ON TBLTSERRORCODE2LOC
(RCARD, ECODE)
LOGGING
NOPARALLEL;

CREATE INDEX PK81_1 ON TBLTSERRORCODE2LOC
(ECODE, ELOC, AB, TSID, ECGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSERRORCODE2LOC
 ADD CONSTRAINT PK81_1
 PRIMARY KEY
 (ECODE, ECGCODE, ELOC, AB, TSID);

CREATE TABLE TBLTSITEM
(
  TSID            VARCHAR2(40 BYTE)             NOT NULL,
  ITEMSEQ         NUMBER(10)                    NOT NULL,
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  MOCODE          VARCHAR2(40 BYTE),
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  RCARDSEQ        NUMBER(10),
  SITEMCODE       VARCHAR2(40 BYTE),
  MSCARD          VARCHAR2(40 BYTE),
  LOCPOINT        NUMBER(10)                    NOT NULL,
  LOC             VARCHAR2(40 BYTE),
  MITEMCODE       VARCHAR2(40 BYTE)             NOT NULL,
  MCARD           VARCHAR2(40 BYTE),
  MCARDTYPE       VARCHAR2(40 BYTE)             NOT NULL,
  QTY             NUMBER(10)                    NOT NULL,
  RRESCODE        VARCHAR2(40 BYTE)             NOT NULL,
  ROPCODE         VARCHAR2(40 BYTE)             NOT NULL,
  LOTNO           VARCHAR2(40 BYTE),
  ISRETS          VARCHAR2(40 BYTE)             NOT NULL,
  DATECODE        VARCHAR2(40 BYTE),
  PCBA            VARCHAR2(40 BYTE),
  BIOS            VARCHAR2(40 BYTE),
  REVERSION       VARCHAR2(40 BYTE),
  VENDORITEMCODE  VARCHAR2(40 BYTE),
  VENDORCODE      VARCHAR2(40 BYTE),
  MEMO            VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  MOSEQ           NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK84 ON TBLTSITEM
(TSID, ITEMSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSITEM
 ADD CONSTRAINT PK84
 PRIMARY KEY
 (TSID, ITEMSEQ);

CREATE TABLE TBLTSSMARTCFG
(
  SEQ            NUMBER(10)                     NOT NULL,
  ECODE          VARCHAR2(40 BYTE),
  ECGCODE        VARCHAR2(40 BYTE),
  ENABLED        VARCHAR2(1 BYTE)               NOT NULL,
  SORTBY         VARCHAR2(40 BYTE),
  DATERANGE      NUMBER(15,5)                   NOT NULL,
  DATERANGETYPE  VARCHAR2(40 BYTE),
  SHOWITEMCOUNT  NUMBER(10)                     NOT NULL,
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTSSMARTCFG
 ADD PRIMARY KEY
 (SEQ);

CREATE TABLE TBLTSSMTITEM
(
  TSID            VARCHAR2(40 BYTE)             NOT NULL,
  ITEMSEQ         NUMBER(10)                    NOT NULL,
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  MOCODE          VARCHAR2(40 BYTE),
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  RCARDSEQ        NUMBER(10),
  SITEMCODE       VARCHAR2(40 BYTE),
  MSCARD          VARCHAR2(40 BYTE),
  LOCPOINT        NUMBER(10),
  LOC             VARCHAR2(40 BYTE),
  MITEMCODE       VARCHAR2(40 BYTE)             NOT NULL,
  MCARD           VARCHAR2(40 BYTE),
  MCARDTYPE       VARCHAR2(40 BYTE),
  QTY             NUMBER(10)                    NOT NULL,
  RRESCODE        VARCHAR2(40 BYTE)             NOT NULL,
  ROPCODE         VARCHAR2(40 BYTE)             NOT NULL,
  LOTNO           VARCHAR2(40 BYTE),
  ISRETS          VARCHAR2(40 BYTE),
  DATECODE        VARCHAR2(40 BYTE),
  PCBA            VARCHAR2(40 BYTE),
  BIOS            VARCHAR2(40 BYTE),
  REVERSION       VARCHAR2(40 BYTE),
  VENDORITEMCODE  VARCHAR2(40 BYTE),
  VENDORCODE      VARCHAR2(40 BYTE),
  MEMO            VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLTSSMTITEM
 ADD PRIMARY KEY
 (TSID, ITEMSEQ);

CREATE TABLE TBLTSSPLITITEM
(
  TSID            VARCHAR2(40 BYTE)             NOT NULL,
  ITEMSEQ         NUMBER(10)                    NOT NULL,
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  RCARDSEQ        NUMBER(10)                    NOT NULL,
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE),
  MOCODE          VARCHAR2(40 BYTE),
  LOCPOINT        NUMBER(10)                    NOT NULL,
  RRESCODE        VARCHAR2(40 BYTE)             NOT NULL,
  ROPCODE         VARCHAR2(40 BYTE)             NOT NULL,
  MITEMCODE       VARCHAR2(40 BYTE)             NOT NULL,
  MCARD           VARCHAR2(40 BYTE),
  MCARDTYPE       VARCHAR2(40 BYTE)             NOT NULL,
  QTY             NUMBER(10)                    NOT NULL,
  DATECODE        VARCHAR2(40 BYTE),
  PCBA            VARCHAR2(40 BYTE),
  BIOS            VARCHAR2(40 BYTE),
  REVERSION       VARCHAR2(40 BYTE),
  VENDORITEMCODE  VARCHAR2(40 BYTE),
  VENDORCODE      VARCHAR2(40 BYTE),
  MEMO            VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  OPENQTY         NUMBER(10),
  SCRAPQTY        NUMBER(10),
  MOSEQ           NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK84_1 ON TBLTSSPLITITEM
(TSID, ITEMSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLTSSPLITITEM
 ADD CONSTRAINT PK84_1
 PRIMARY KEY
 (TSID, ITEMSEQ);

CREATE TABLE TBLUPDATELOG
(
  UPDATELOGID  VARCHAR2(40 BYTE)                NOT NULL,
  FILENAME     VARCHAR2(100 BYTE)               NOT NULL,
  VERSION      VARCHAR2(40 BYTE)                NOT NULL,
  MACHINENAME  VARCHAR2(40 BYTE)                NOT NULL,
  MACHINEIP    VARCHAR2(40 BYTE)                NOT NULL,
  RESULT       VARCHAR2(40 BYTE)                NOT NULL,
  UPDATETIME   VARCHAR2(40 BYTE)                NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PKUPDATELOG ON TBLUPDATELOG
(UPDATELOGID)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_VERSION_RESULT ON TBLUPDATELOG
(VERSION, RESULT)
LOGGING
NOPARALLEL;

ALTER TABLE TBLUPDATELOG
 ADD CONSTRAINT PKUPDATELOG
 PRIMARY KEY
 (UPDATELOGID);

CREATE TABLE TBLUSER
(
  USERCODE     VARCHAR2(40 BYTE)                NOT NULL,
  USERPWD      VARCHAR2(200 BYTE)               NOT NULL,
  USERNAME     VARCHAR2(40 BYTE),
  USERTEL      VARCHAR2(40 BYTE),
  USEREMAIL    VARCHAR2(100 BYTE),
  USERDEPART   VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  USERSTAT     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK3 ON TBLUSER
(USERCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLUSER
 ADD CONSTRAINT PK3
 PRIMARY KEY
 (USERCODE);

CREATE TABLE TBLSCRAPCAUSE
(
  SCCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SCDESC       VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(38)                       NOT NULL,
  MTIME        NUMBER(38)                       NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSCRAPCAUSE
 ADD PRIMARY KEY
 (SCCODE);

CREATE TABLE TBLSEG
(
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SEGSEQ         NUMBER(10)                     NOT NULL,
  SEGDESC        VARCHAR2(100 BYTE),
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  ORGID          NUMBER(8),
  FACCODE        VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK29 ON TBLSEG
(SEGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSEG
 ADD CONSTRAINT PK29
 PRIMARY KEY
 (SEGCODE);

CREATE TABLE TBLSERIAL
(
  SERIAL  NUMBER(13)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLSHELF
(
  SHELFNO      VARCHAR2(40 BYTE)                NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  MEMO         VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ITEMCODE     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX SHELF ON TBLSHELF
(SHELFNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSHELF
 ADD CONSTRAINT SHELF
 PRIMARY KEY
 (SHELFNO);

CREATE TABLE TBLSHELFACTIONLIST
(
  PKID         VARCHAR2(40 BYTE)                NOT NULL,
  SHELFNO      VARCHAR2(40 BYTE)                NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  BIDATE       NUMBER(8)                        NOT NULL,
  BITIME       NUMBER(6)                        NOT NULL,
  BITP         VARCHAR2(40 BYTE)                NOT NULL,
  BODATE       NUMBER(8),
  BOTIME       NUMBER(6)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  BIUSER       VARCHAR2(40 BYTE)                DEFAULT ( ' ' )               NOT NULL,
  BOUSER       VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_SHELFTACTLST_SHELFSTATUS ON TBLSHELFACTIONLIST
(SHELFNO, STATUS)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX SHELFACTIONLIST ON TBLSHELFACTIONLIST
(PKID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSHELFACTIONLIST
 ADD CONSTRAINT SHELFACTIONLIST
 PRIMARY KEY
 (PKID);

CREATE TABLE TBLSHIFT
(
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTSEQ       NUMBER(10)                     NOT NULL,
  SHIFTDESC      VARCHAR2(100 BYTE),
  SHIFTBTIME     NUMBER(6)                      NOT NULL,
  SHIFTETIME     NUMBER(6)                      NOT NULL,
  ISOVERDAY      VARCHAR2(1 BYTE)               NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK27 ON TBLSHIFT
(SHIFTCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSHIFT
 ADD CONSTRAINT PK27
 PRIMARY KEY
 (SHIFTCODE);

CREATE TABLE TBLSHIFTTYPE
(
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPEDESC  VARCHAR2(100 BYTE),
  EFFDATE        NUMBER(10)                     NOT NULL,
  IVLDATE        NUMBER(8)                      NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK1 ON TBLSHIFTTYPE
(SHIFTTYPECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSHIFTTYPE
 ADD CONSTRAINT PK1
 PRIMARY KEY
 (SHIFTTYPECODE);

CREATE TABLE TBLSHIPTOSTOCK
(
  ITEMCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  ORGID       NUMBER(8)                         NOT NULL,
  VENDORCODE  VARCHAR2(40 BYTE)                 NOT NULL,
  EFFDATE     NUMBER(8)                         NOT NULL,
  IVLDATE     NUMBER(8)                         NOT NULL,
  ACTIVE      VARCHAR2(2 BYTE)                  NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(22)                        NOT NULL,
  MTIME       NUMBER(22)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLSHIPTOSTOCK ON TBLSHIPTOSTOCK
(ITEMCODE, ORGID, VENDORCODE)
LOGGING
NOPARALLEL;

CREATE TABLE TBLSIMULATION
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10),
  TCARD          VARCHAR2(40 BYTE)              NOT NULL,
  TCARDSEQ       NUMBER(10),
  SCARD          VARCHAR2(40 BYTE)              NOT NULL,
  SCARDSEQ       NUMBER(10),
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  FROMROUTE      VARCHAR2(40 BYTE),
  FROMOP         VARCHAR2(40 BYTE),
  ROUTECODE      VARCHAR2(40 BYTE),
  OPCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  IDMERGERULE    NUMBER(10),
  LOTNO          VARCHAR2(40 BYTE),
  CARTONCODE     VARCHAR2(40 BYTE),
  PALLETCODE     VARCHAR2(40 BYTE),
  PRODUCTSTATUS  VARCHAR2(40 BYTE)              NOT NULL,
  LACTION        VARCHAR2(40 BYTE)              NOT NULL,
  ACTIONLIST     VARCHAR2(100 BYTE),
  NGTIMES        NUMBER(10),
  ISCOM          VARCHAR2(40 BYTE),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE2    VARCHAR2(40 BYTE),
  ISHOLD         NUMBER(10),
  SHELFNO        VARCHAR2(40 BYTE),
  RMABILLCODE    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX SIM_SHELFNO ON TBLSIMULATION
(SHELFNO)
LOGGING
NOPARALLEL;

CREATE INDEX SIM_LOT ON TBLSIMULATION
(EATTRIBUTE2)
LOGGING
NOPARALLEL;

CREATE INDEX PK40_4 ON TBLSIMULATION
(RCARD, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_SIM_RES ON TBLSIMULATION
(RESCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_SIMULATION_MOCODE ON TBLSIMULATION
(MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_SIMULATION_CARTONNO ON TBLSIMULATION
(CARTONCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSIMULATION
 ADD CONSTRAINT PK40_4
 PRIMARY KEY
 (RCARD, MOCODE);

CREATE TABLE TBLSIMULATION222
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10),
  TCARD          VARCHAR2(40 BYTE)              NOT NULL,
  TCARDSEQ       NUMBER(10),
  SCARD          VARCHAR2(40 BYTE)              NOT NULL,
  SCARDSEQ       NUMBER(10),
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  FROMROUTE      VARCHAR2(40 BYTE),
  FROMOP         VARCHAR2(40 BYTE),
  ROUTECODE      VARCHAR2(40 BYTE),
  OPCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  IDMERGERULE    NUMBER(10),
  LOTNO          VARCHAR2(40 BYTE),
  CARTONCODE     VARCHAR2(40 BYTE),
  PALLETCODE     VARCHAR2(40 BYTE),
  PRODUCTSTATUS  VARCHAR2(40 BYTE)              NOT NULL,
  LACTION        VARCHAR2(40 BYTE)              NOT NULL,
  ACTIONLIST     VARCHAR2(100 BYTE),
  NGTIMES        NUMBER(10),
  ISCOM          VARCHAR2(40 BYTE),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE2    VARCHAR2(40 BYTE),
  ISHOLD         NUMBER(10),
  SHELFNO        VARCHAR2(40 BYTE),
  RMABILLCODE    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSIMULATION222
 ADD PRIMARY KEY
 (RCARD, MOCODE);

CREATE TABLE TBLSIMULATIONREPORT
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10),
  TCARD          VARCHAR2(40 BYTE)              NOT NULL,
  TCARDSEQ       NUMBER(10),
  SCARD          VARCHAR2(40 BYTE)              NOT NULL,
  SCARDSEQ       NUMBER(10),
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  OPCODE         VARCHAR2(40 BYTE),
  ROUTECODE      VARCHAR2(40 BYTE),
  SEGCODE        VARCHAR2(40 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  LACTION        VARCHAR2(40 BYTE)              NOT NULL,
  IDMERGERULE    NUMBER(10),
  LOTNO          VARCHAR2(40 BYTE),
  CARTONCODE     VARCHAR2(40 BYTE),
  PALLETCODE     VARCHAR2(40 BYTE),
  NGTIMES        NUMBER(10),
  ISCOM          VARCHAR2(40 BYTE),
  STATUS         VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTDAY       NUMBER(8),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  EATTRIBUTE2    VARCHAR2(40 BYTE),
  SHELFNO        VARCHAR2(40 BYTE),
  RMABILLCODE    VARCHAR2(40 BYTE),
  ISLOADEDPART   VARCHAR2(40 BYTE),
  LOADEDRCARD    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX SIMREP_SSCODE ON TBLSIMULATIONREPORT
(SSCODE)
LOGGING
NOPARALLEL;

CREATE INDEX PK40_3 ON TBLSIMULATIONREPORT
(RCARD, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TSR_SHIFTDAY ON TBLSIMULATIONREPORT
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TSR_OPSHIFTDAY ON TBLSIMULATIONREPORT
(OPCODE, SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_SIMRPT_MOCODE ON TBLSIMULATIONREPORT
(MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSIMULATIONREPORT
 ADD PRIMARY KEY
 (RCARD, MOCODE);

CREATE TABLE TBLSKDCARTONDETAIL
(
  MOCODE      VARCHAR2(40 BYTE)                 NOT NULL,
  CARTONNO    VARCHAR2(40 BYTE)                 NOT NULL,
  ITEMCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  SBITEMCODE  VARCHAR2(40 BYTE)                 NOT NULL,
  MCARD       VARCHAR2(40 BYTE),
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TABLE_MCARD ON TBLSKDCARTONDETAIL
(MCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TABLE_CARTON ON TBLSKDCARTONDETAIL
(CARTONNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSKDCARTONDETAIL
 ADD PRIMARY KEY
 (MOCODE, MCARD);

CREATE TABLE TBLPKRULE
(
  PKRULECODE  VARCHAR2(40 BYTE)                 NOT NULL,
  PKRULEDESC  VARCHAR2(100 BYTE),
  ISDEFAULT   VARCHAR2(1 BYTE)                  NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_PKRULE ON TBLPKRULE
(PKRULECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLPKRULE
 ADD CONSTRAINT PK_PKRULE
 PRIMARY KEY
 (PKRULECODE);

CREATE TABLE TBLPKRULESTEP
(
  PKRULECODE  VARCHAR2(40 BYTE)                 NOT NULL,
  STEP        NUMBER(10)                        NOT NULL,
  STEPCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  STEPNAME    VARCHAR2(100 BYTE),
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_PKRULESTEP ON TBLPKRULESTEP
(PKRULECODE, STEP)
LOGGING
NOPARALLEL;

ALTER TABLE TBLPKRULESTEP
 ADD CONSTRAINT PK_PKRULESTEP
 PRIMARY KEY
 (PKRULECODE, STEP);

CREATE TABLE TBLPKRULESTEPCHECK
(
  PKRULECODE         VARCHAR2(40 BYTE)          NOT NULL,
  STEP               NUMBER(10)                 NOT NULL,
  CHECKSEQ           NUMBER(10)                 NOT NULL,
  CHECKTYPE          VARCHAR2(40 BYTE)          NOT NULL,
  STARTPOS           NUMBER(10)                 NOT NULL,
  ENDPOS             NUMBER(10)                 NOT NULL,
  STRINGTYPE         VARCHAR2(40 BYTE),
  CHECKVALUE         VARCHAR2(40 BYTE),
  CHECKSTEP          NUMBER(10),
  CHECKSTEPSTARTPOS  NUMBER(10),
  CHECKSTEPENDPOS    NUMBER(10),
  ITEMFIELD          VARCHAR2(40 BYTE),
  MUSER              VARCHAR2(40 BYTE),
  MDATE              NUMBER(8)                  NOT NULL,
  MTIME              NUMBER(6)                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_PKRULESTEPCHECK ON TBLPKRULESTEPCHECK
(PKRULECODE, STEP, CHECKSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLPKRULESTEPCHECK
 ADD CONSTRAINT PK_PKRULESTEPCHECK
 PRIMARY KEY
 (PKRULECODE, STEP, CHECKSEQ);

CREATE TABLE TBLPRINTTEMPLATE
(
  TEMPLATENAME  VARCHAR2(40 BYTE)               NOT NULL,
  TEMPLATEDESC  VARCHAR2(100 BYTE)              NOT NULL,
  TEMPLATEPATH  VARCHAR2(300 BYTE)              NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)               NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLPRINTTEMPLATE
 ADD PRIMARY KEY
 (TEMPLATENAME);

CREATE TABLE TBLRAWISSUE2SAP
(
  VENDORCODE        VARCHAR2(40 BYTE),
  FLAG              VARCHAR2(40 BYTE)           NOT NULL,
  ERRORMESSAGE      VARCHAR2(100 BYTE),
  MATDOCUMENTYEAR   VARCHAR2(40 BYTE),
  MATERIALDOCUMENT  VARCHAR2(40 BYTE),
  MUSER             VARCHAR2(40 BYTE),
  MDATE             NUMBER(8),
  MTIME             NUMBER(6),
  TRANSACTIONCODE   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLRAWISSUE2SAP ON TBLRAWISSUE2SAP
(TRANSACTIONCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRAWISSUE2SAP ON TBLRAWISSUE2SAP
(VENDORCODE)
LOGGING
NOPARALLEL;

CREATE TABLE TBLRAWRECEIVE2SAP
(
  PONO              VARCHAR2(40 BYTE)           NOT NULL,
  POSTSEQ           NUMBER(10)                  NOT NULL,
  FLAG              VARCHAR2(10 BYTE)           NOT NULL,
  ERRORMESSAGE      VARCHAR2(2000 BYTE),
  MATDOCUMENTYEAR   VARCHAR2(10 BYTE),
  MATERIALDOCUMENT  VARCHAR2(40 BYTE),
  MUSER             VARCHAR2(40 BYTE)           NOT NULL,
  MDATE             NUMBER(8)                   NOT NULL,
  MTIME             NUMBER(6)                   NOT NULL,
  TRANSACTIONCODE   VARCHAR2(100 BYTE)          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRAWRECEIVE2SAP
 ADD PRIMARY KEY
 (PONO, POSTSEQ);

CREATE TABLE TBLRCARD2RMABILL
(
  RMABILLCODE  VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  CUSCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX RCARD2RMABILL ON TBLRCARD2RMABILL
(RMABILLCODE, ITEMCODE, CUSCODE, MODELCODE, RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRCARD2RMABILL
 ADD CONSTRAINT RCARD2RMABILL
 PRIMARY KEY
 (RMABILLCODE, ITEMCODE, CUSCODE, MODELCODE, RCARD);

CREATE TABLE TBLRCARDCHANGE
(
  RCARDFROM    VARCHAR2(40 BYTE)                NOT NULL,
  RCARDTO      VARCHAR2(40 BYTE)                NOT NULL,
  REASON       VARCHAR2(200 BYTE)               NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_RCARDTO_TBLRCARDCHANGE ON TBLRCARDCHANGE
(RCARDTO)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_RCARDFROM_TBLRCARDCHANGE ON TBLRCARDCHANGE
(RCARDFROM)
LOGGING
NOPARALLEL;

CREATE TABLE TBLREEL
(
  REELNO        VARCHAR2(40 BYTE)               NOT NULL,
  PARTNO        VARCHAR2(40 BYTE),
  QTY           NUMBER(15,5)                    NOT NULL,
  USEDQTY       NUMBER(15,5)                    NOT NULL,
  LOTNO         VARCHAR2(100 BYTE),
  DATECODE      VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE),
  USEDFLAG      VARCHAR2(1 BYTE),
  MOCODE        VARCHAR2(40 BYTE),
  SSCODE        VARCHAR2(40 BYTE),
  ISSPECIAL     VARCHAR2(40 BYTE),
  MEMO          VARCHAR2(100 BYTE),
  CHECKDIFFQTY  NUMBER(15,5)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_REEL_MOSS ON TBLREEL
(MOCODE, SSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREEL
 ADD PRIMARY KEY
 (REELNO);

CREATE TABLE TBLREELCHKLOG
(
  CHECKID       NUMBER(10)                      NOT NULL,
  MOCODE        VARCHAR2(40 BYTE),
  SSCODE        VARCHAR2(40 BYTE),
  REELNO        VARCHAR2(40 BYTE),
  MATERIALCODE  VARCHAR2(40 BYTE),
  REELQTY       NUMBER(15,5)                    NOT NULL,
  REELCURRQTY   NUMBER(15,5)                    NOT NULL,
  ISSPECIAL     VARCHAR2(1 BYTE)                NOT NULL,
  MEMO          VARCHAR2(100 BYTE),
  GETOUTUSER    VARCHAR2(40 BYTE),
  GETOUTDATE    NUMBER(8)                       NOT NULL,
  GETOUTTIME    NUMBER(6)                       NOT NULL,
  REELLEFTQTY   NUMBER(15,5)                    NOT NULL,
  REELACTQTY    NUMBER(15,5)                    NOT NULL,
  ISCHECKED     VARCHAR2(1 BYTE)                NOT NULL,
  CHECKUSER     VARCHAR2(40 BYTE),
  CHECKDATE     NUMBER(8)                       NOT NULL,
  CHECKTIME     NUMBER(6)                       NOT NULL,
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_REELCHECKLOG_REELNO ON TBLREELCHKLOG
(REELNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREELCHKLOG
 ADD PRIMARY KEY
 (CHECKID);

CREATE TABLE TBLREELQTY
(
  REELNO              VARCHAR2(40 BYTE)         NOT NULL,
  MOCODE              VARCHAR2(40 BYTE)         NOT NULL,
  MACHINECODE         VARCHAR2(40 BYTE),
  MACHINESTATIONCODE  VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  FEEDERCODE          VARCHAR2(40 BYTE),
  QTY                 NUMBER(15,5)              NOT NULL,
  USEDQTY             NUMBER(15,5)              NOT NULL,
  UPDATEDQTY          NUMBER(15,5)              NOT NULL,
  MUSER               VARCHAR2(40 BYTE),
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE),
  ALERTQTY            NUMBER(15,5),
  UNITQTY             NUMBER(15,5),
  MATERIALCODE        VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLREELQTY
 ADD PRIMARY KEY
 (REELNO, MOCODE);

CREATE TABLE TBLREELVALIDITY
(
  MATERIALPREFIX  VARCHAR2(40 BYTE)             NOT NULL,
  VALIDITYMONTH   NUMBER(15,5)                  NOT NULL,
  MUSER           VARCHAR2(40 BYTE),
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLREELVALIDITY
 ADD PRIMARY KEY
 (MATERIALPREFIX);

CREATE TABLE TBLREJECT
(
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10)                       NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  TCARD        VARCHAR2(40 BYTE)                NOT NULL,
  TCARDSEQ     NUMBER(10)                       NOT NULL,
  SCARD        VARCHAR2(40 BYTE)                NOT NULL,
  SCARDSEQ     NUMBER(10)                       NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  LOTNO        VARCHAR2(40 BYTE),
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  OPID         VARCHAR2(40 BYTE),
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  RESCODE      VARCHAR2(40 BYTE)                NOT NULL,
  REWORKCODE   VARCHAR2(40 BYTE),
  REJSTATUS    VARCHAR2(40 BYTE)                NOT NULL,
  RUSER        VARCHAR2(40 BYTE),
  RDATE        NUMBER(8)                        NOT NULL,
  RTIME        NUMBER(6)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  MOSEQ        NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK40_2 ON TBLREJECT
(RCARD, RCARDSEQ, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREJECT
 ADD CONSTRAINT PK40_2
 PRIMARY KEY
 (RCARD, RCARDSEQ, MOCODE);

CREATE TABLE TBLREJECT2ERRORCODE
(
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  RCARDSEQ        NUMBER(10)                    NOT NULL,
  ERRORCODEGROUP  VARCHAR2(40 BYTE)             NOT NULL,
  ECODE           VARCHAR2(40 BYTE)             NOT NULL,
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  LOTNO           VARCHAR2(40 BYTE),
  LOTSEQ          NUMBER(10),
  MOSEQ           NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK121 ON TBLREJECT2ERRORCODE
(RCARD, ERRORCODEGROUP, ECODE, MOCODE, RCARDSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREJECT2ERRORCODE
 ADD CONSTRAINT PK121
 PRIMARY KEY
 (RCARD, RCARDSEQ, ERRORCODEGROUP, ECODE, MOCODE);

CREATE TABLE TBLRES
(
  RESCODE          VARCHAR2(40 BYTE)            NOT NULL,
  RESDESC          VARCHAR2(100 BYTE),
  RESGROUP         VARCHAR2(40 BYTE),
  RESTYPE          VARCHAR2(40 BYTE)            NOT NULL,
  SEGCODE          VARCHAR2(40 BYTE),
  SSCODE           VARCHAR2(40 BYTE),
  SHIFTTYPECODE    VARCHAR2(40 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  REWORKROUTECODE  VARCHAR2(40 BYTE),
  ORGID            NUMBER(8)                    NOT NULL,
  REWORKCODE       VARCHAR2(40 BYTE),
  DCTCODE          VARCHAR2(40 BYTE),
  CREWCODE         VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLRES_CREWCODE ON TBLRES
(CREWCODE)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK5 ON TBLRES
(RESCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRES
 ADD CONSTRAINT PK5
 PRIMARY KEY
 (RESCODE);

CREATE TABLE TBLRESREWRKLOG
(
  RESCODE             VARCHAR2(40 BYTE)         NOT NULL,
  SEQ                 NUMBER(10)                NOT NULL,
  REWORKROUTECODE     VARCHAR2(40 BYTE),
  OLDREWORKROUTECODE  VARCHAR2(40 BYTE),
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLRESREWRKLOG
 ADD PRIMARY KEY
 (RESCODE, SEQ);

CREATE TABLE TBLREWORKCAUSE
(
  REWORKCCODE  VARCHAR2(40 BYTE)                NOT NULL,
  REWORKCDESC  VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK110 ON TBLREWORKCAUSE
(REWORKCCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREWORKCAUSE
 ADD CONSTRAINT PK110
 PRIMARY KEY
 (REWORKCCODE);

CREATE TABLE TBLREWORKPASS
(
  SEQ          NUMBER(10)                       NOT NULL,
  REWORKCODE   VARCHAR2(40 BYTE)                NOT NULL,
  PSEQ         NUMBER(10)                       NOT NULL,
  USERCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ISPASS       NUMBER(10)                       NOT NULL,
  PCONTENT     VARCHAR2(100 BYTE),
  STATUS       NUMBER(10)                       NOT NULL,
  PUSER        VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        VARCHAR2(40 BYTE)                NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK124 ON TBLREWORKPASS
(SEQ, REWORKCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREWORKPASS
 ADD CONSTRAINT PK124
 PRIMARY KEY
 (SEQ, REWORKCODE);

CREATE TABLE TBLREWORKRANGE
(
  REWORKCODE   VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK123 ON TBLREWORKRANGE
(REWORKCODE, RCARD, RCARDSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREWORKRANGE
 ADD CONSTRAINT PK123
 PRIMARY KEY
 (REWORKCODE, RCARD, RCARDSEQ);

CREATE TABLE TBLREWORKSHEET
(
  REWORKCODE     VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE),
  CUSER          VARCHAR2(40 BYTE)              NOT NULL,
  CDATE          NUMBER(8)                      NOT NULL,
  CTIME          NUMBER(6)                      NOT NULL,
  STATUS         VARCHAR2(40 BYTE)              NOT NULL,
  REWORKSCODE    VARCHAR2(40 BYTE)              NOT NULL,
  REWORKTYPE     VARCHAR2(40 BYTE)              NOT NULL,
  NEWMOCODE      VARCHAR2(40 BYTE),
  NEWROUTECODE   VARCHAR2(40 BYTE),
  NEWOPCODE      VARCHAR2(40 BYTE),
  NEWOPBOMCODE   VARCHAR2(40 BYTE),
  NEWOPBOMVER    VARCHAR2(40 BYTE),
  REWORKHC       NUMBER(10)                     NOT NULL,
  DEPARTMENT     VARCHAR2(40 BYTE),
  CONTENT        VARCHAR2(100 BYTE),
  REWORKDATE     NUMBER(8),
  REWORKTIME     NUMBER(6),
  REWORKQTY      NUMBER(10)                     NOT NULL,
  REWORKMAXQTY   NUMBER(10)                     NOT NULL,
  REWORKREALQTY  NUMBER(10)                     NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  REWORKREASON   VARCHAR2(1000 BYTE),
  REASONANALYSE  VARCHAR2(1000 BYTE),
  SOLUTION       VARCHAR2(1000 BYTE),
  NEEDCHECK      VARCHAR2(40 BYTE),
  LOTLIST        VARCHAR2(1000 BYTE),
  AUTOLOTNO      VARCHAR2(40 BYTE),
  DUTYCODE       VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK119 ON TBLREWORKSHEET
(REWORKCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREWORKSHEET
 ADD CONSTRAINT PK119
 PRIMARY KEY
 (REWORKCODE);

CREATE TABLE TBLREWORKSHEET2CAUSE
(
  REWORKCODE   VARCHAR2(40 BYTE)                NOT NULL,
  REWORKCCODE  VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK122 ON TBLREWORKSHEET2CAUSE
(REWORKCODE, REWORKCCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREWORKSHEET2CAUSE
 ADD CONSTRAINT PK122
 PRIMARY KEY
 (REWORKCODE, REWORKCCODE);

CREATE TABLE TBLREWORKSOURCE
(
  REWORKSCODE  VARCHAR2(40 BYTE)                NOT NULL,
  REWORKSDESC  VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK109 ON TBLREWORKSOURCE
(REWORKSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLREWORKSOURCE
 ADD CONSTRAINT PK109
 PRIMARY KEY
 (REWORKSCODE);

CREATE TABLE TBLRLOT
(
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  OPCODE       VARCHAR2(40 BYTE),
  RESCODE      VARCHAR2(40 BYTE),
  STDQTY       NUMBER(10)                       NOT NULL,
  ACTQTY       NUMBER(10),
  LOTSTATUS    VARCHAR2(40 BYTE)                NOT NULL,
  MEMO         VARCHAR2(100 BYTE),
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK429 ON TBLRLOT
(LOTNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRLOT
 ADD CONSTRAINT PK429
 PRIMARY KEY
 (LOTNO);

CREATE TABLE TBLRMABILL
(
  RMABILLCODE     VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  CUSCODE         VARCHAR2(40 BYTE)             NOT NULL,
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  FACCODE         VARCHAR2(40 BYTE)             NOT NULL,
  RTCODE          VARCHAR2(40 BYTE),
  CUSITEMCODE     VARCHAR2(40 BYTE)             NOT NULL,
  ITEMISSUE       VARCHAR2(40 BYTE)             NOT NULL,
  ISSUEKIDE       VARCHAR2(40 BYTE),
  SYMPTOMCODE     VARCHAR2(40 BYTE)             NOT NULL,
  CUSSYMPTOMCODE  VARCHAR2(40 BYTE)             NOT NULL,
  DUTYCODE        VARCHAR2(40 BYTE)             NOT NULL,
  QTY             NUMBER(10)                    NOT NULL,
  HANDLECODE      VARCHAR2(40 BYTE)             NOT NULL,
  REMOCODE        VARCHAR2(40 BYTE),
  STATUS          VARCHAR2(40 BYTE)             NOT NULL,
  DESCCODE        VARCHAR2(100 BYTE),
  OPENDATE        NUMBER(8)                     NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  OPENWEEK        NUMBER(6)                     DEFAULT 1                     NOT NULL,
  OPENMONTH       NUMBER(6)                     DEFAULT 1                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX RMABILL ON TBLRMABILL
(RMABILLCODE, ITEMCODE, CUSCODE, MODELCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRMABILL
 ADD CONSTRAINT RMABILL
 PRIMARY KEY
 (RMABILLCODE, ITEMCODE, CUSCODE, MODELCODE);

CREATE TABLE TBLRMACODERULE
(
  BUCODE       VARCHAR2(40 BYTE)                NOT NULL,
  RMABILLFL    VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX RMACODERULE ON TBLRMACODERULE
(BUCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRMACODERULE
 ADD CONSTRAINT RMACODERULE
 PRIMARY KEY
 (BUCODE);

CREATE TABLE TBLRMARCARD
(
  RMAPKID       VARCHAR2(40 BYTE)               NOT NULL,
  RMABILLNO     VARCHAR2(40 BYTE)               NOT NULL,
  RCARD         VARCHAR2(40 BYTE),
  MODELCODE     VARCHAR2(40 BYTE),
  ITEMCODE      VARCHAR2(40 BYTE),
  REWORKMOCODE  VARCHAR2(40 BYTE),
  RMATYPE       VARCHAR2(40 BYTE)               NOT NULL,
  SEGCODE       VARCHAR2(40 BYTE),
  SSCOE         VARCHAR2(40 BYTE),
  RESCODE       VARCHAR2(40 BYTE),
  SHIFTDAY      NUMBER(8),
  ROUTECODE     VARCHAR2(40 BYTE),
  OPCODE        VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PKRMARCARD ON TBLRMARCARD
(RMAPKID)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_RMA_RCARD ON TBLRMARCARD
(RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_RMA_MOCODE ON TBLRMARCARD
(REWORKMOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRMARCARD
 ADD CONSTRAINT PKRMARCARD
 PRIMARY KEY
 (RMAPKID);

CREATE TABLE TBLROUTE
(
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  ROUTEDESC    VARCHAR2(100 BYTE),
  ROUTETYPE    VARCHAR2(40 BYTE)                NOT NULL,
  EFFDATE      NUMBER(8)                        NOT NULL,
  IVLDATE      NUMBER(8)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ENABLED      VARCHAR2(1 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK6 ON TBLROUTE
(ROUTECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLROUTE
 ADD CONSTRAINT PK6
 PRIMARY KEY
 (ROUTECODE);

CREATE TABLE TBLROUTE2OP
(
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  OPSEQ        NUMBER(10)                       NOT NULL,
  OPCONTROL    VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK258_1 ON TBLROUTE2OP
(ROUTECODE, OPCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLROUTE2OP
 ADD CONSTRAINT PK258_1
 PRIMARY KEY
 (ROUTECODE, OPCODE);

CREATE TABLE TBLRPTHISCEQTY
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SHIFTDAY     NUMBER(8)                        NOT NULL,
  SHIFTCODE    VARCHAR2(40 BYTE)                NOT NULL,
  TPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      CHAR(10 BYTE)                    NOT NULL,
  ECQTY        NUMBER(10)                       NOT NULL,
  EATTRIBUTE1  NUMBER(10)                       NOT NULL,
  EATTRIBUTE2  NUMBER(10)                       NOT NULL,
  EATTRIBUTE3  NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314_2_1 ON TBLRPTHISCEQTY
(MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, 
TPCODE, SEGCODE, SSCODE, ECODE, ECGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTHISCEQTY
 ADD CONSTRAINT PK314_2_1
 PRIMARY KEY
 (MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, TPCODE, SEGCODE, SSCODE, ECODE, ECGCODE);

CREATE TABLE TBLRPTHISLINEQTY
(
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SHIFTDAY     NUMBER(8)                        NOT NULL,
  SHIFTCODE    VARCHAR2(40 BYTE)                NOT NULL,
  TPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  OUTPUTQTY    NUMBER(10)                       NOT NULL,
  ALLGOODQTY   NUMBER(10)                       NOT NULL,
  NGTIMES      NUMBER(10)                       NOT NULL,
  EATTRIBUTE1  NUMBER(10)                       NOT NULL,
  EATTRIBUTE2  NUMBER(10)                       NOT NULL,
  EATTRIBUTE3  NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314_1 ON TBLRPTHISLINEQTY
(MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, 
TPCODE, SEGCODE, SSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTHISLINEQTY
 ADD CONSTRAINT PK314_1
 PRIMARY KEY
 (MOCODE, MODELCODE, ITEMCODE, SHIFTDAY, SHIFTCODE, TPCODE, SEGCODE, SSCODE);

CREATE TABLE TBLRPTHISOPQTY
(
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  SHIFTDAY        NUMBER(8)                     NOT NULL,
  SHIFTCODE       VARCHAR2(40 BYTE)             NOT NULL,
  TPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  OPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  RESCODE         VARCHAR2(40 BYTE)             NOT NULL,
  SEGCODE         VARCHAR2(40 BYTE)             NOT NULL,
  SSCODE          VARCHAR2(40 BYTE)             NOT NULL,
  MONTH           NUMBER(10)                    NOT NULL,
  QTYFLAG         VARCHAR2(1 BYTE)              NOT NULL,
  DAY             NUMBER(8)                     NOT NULL,
  WEEK            NUMBER(10)                    NOT NULL,
  OUTPUTQTY       NUMBER(10)                    NOT NULL,
  ALLGOODQTY      NUMBER(10)                    NOT NULL,
  NGTIMES         NUMBER(10)                    NOT NULL,
  EATTRIBUTE1     NUMBER(10)                    NOT NULL,
  EATTRIBUTE2     NUMBER(10)                    NOT NULL,
  EATTRIBUTE3     NUMBER(10)                    NOT NULL,
  LASTUPDATETIME  NUMBER(6),
  ECG2EC          VARCHAR2(4000 BYTE),
  OPSEQ           NUMBER(10),
  OPCONTROL       VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK314_1_1 ON TBLRPTHISOPQTY
(MODELCODE, SHIFTDAY, MOCODE, TPCODE, SSCODE, 
SEGCODE, ITEMCODE, SHIFTCODE, OPCODE, RESCODE, 
QTYFLAG)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTHISOPQTY_SHIFTDAY ON TBLRPTHISOPQTY
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTHISOPQTY_CODE2 ON TBLRPTHISOPQTY
(OPCODE, RESCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLRPTHISOPQTY_CODE1 ON TBLRPTHISOPQTY
(MODELCODE, ITEMCODE, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLRPTHISOPQTY
 ADD CONSTRAINT PK314_1_1
 PRIMARY KEY
 (MODELCODE, SHIFTDAY, MOCODE, TPCODE, SSCODE, SEGCODE, ITEMCODE, SHIFTCODE, OPCODE, RESCODE, QTYFLAG);

CREATE TABLE TBLMODEL
(
  MODELCODE        VARCHAR2(40 BYTE)            NOT NULL,
  MODELDESC        VARCHAR2(100 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  ISINV            VARCHAR2(1 BYTE),
  ISREFLOW         VARCHAR2(1 BYTE)             DEFAULT '0',
  ISCHECKDATALINK  VARCHAR2(1 BYTE)             DEFAULT '0',
  DATALINKQTY      NUMBER(10)                   DEFAULT 0,
  ISDIM            VARCHAR2(1 BYTE)             DEFAULT '0',
  DIMQTY           NUMBER(8)                    DEFAULT 0,
  ORGID            NUMBER(8)                    DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMODEL
 ADD PRIMARY KEY
 (MODELCODE, ORGID);

CREATE TABLE TBLMODEL2ECG
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE),
  OPCODE       VARCHAR2(40 BYTE),
  RESCODE      VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK73_1_1 ON TBLMODEL2ECG
(MODELCODE, ECGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMODEL2ECG
 ADD CONSTRAINT PK73_1_1
 PRIMARY KEY
 (MODELCODE, ECGCODE);

CREATE TABLE TBLMODEL2ECS
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ECSCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK73_1_1_1 ON TBLMODEL2ECS
(MODELCODE, ECSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMODEL2ECS
 ADD CONSTRAINT PK73_1_1_1
 PRIMARY KEY
 (MODELCODE, ECSCODE);

CREATE TABLE TBLMODEL2ECSG
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ECSGCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_MODEL2ECSG ON TBLMODEL2ECSG
(MODELCODE, ECSGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMODEL2ECSG
 ADD CONSTRAINT PK_MODEL2ECSG
 PRIMARY KEY
 (MODELCODE, ECSGCODE);

CREATE TABLE TBLMODEL2ERRSYM
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  SYMPTOMCODE  VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX MODEL2ERRORSYMPTOM ON TBLMODEL2ERRSYM
(MODELCODE, SYMPTOMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMODEL2ERRSYM
 ADD CONSTRAINT MODEL2ERRORSYMPTOM
 PRIMARY KEY
 (MODELCODE, SYMPTOMCODE);

CREATE TABLE TBLMODEL2ITEM
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX REF265518 ON TBLMODEL2ITEM
(MODELCODE)
LOGGING
NOPARALLEL;

CREATE INDEX REF10519 ON TBLMODEL2ITEM
(ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMODEL2ITEM
 ADD PRIMARY KEY
 (MODELCODE, ITEMCODE, ORGID);

CREATE TABLE TBLMODEL2OP
(
  OPID         VARCHAR2(100 BYTE)               NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE),
  ROUTECODE    VARCHAR2(40 BYTE),
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  OPSEQ        NUMBER(10)                       NOT NULL,
  OPCONTROL    VARCHAR2(40 BYTE)                NOT NULL,
  IDMERGETYPE  VARCHAR2(40 BYTE)                NOT NULL,
  IDMERGERULE  NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX REF252496 ON TBLMODEL2OP
(MODELCODE, ROUTECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMODEL2OP
 ADD PRIMARY KEY
 (OPID, ORGID);

CREATE TABLE TBLMODEL2ROUTE
(
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        DEFAULT NULL                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMODEL2ROUTE
 ADD PRIMARY KEY
 (ROUTECODE, MODELCODE, ORGID);

CREATE TABLE TBLMODEL2SOLUTION
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  SOLCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK73_1_1_1_1 ON TBLMODEL2SOLUTION
(MODELCODE, SOLCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMODEL2SOLUTION
 ADD CONSTRAINT PK73_1_1_1_1
 PRIMARY KEY
 (MODELCODE, SOLCODE);

CREATE TABLE TBLMORCARD
(
  MOCODE        VARCHAR2(40 BYTE)               NOT NULL,
  SEQ           NUMBER(10)                      NOT NULL,
  MORCARDSTART  VARCHAR2(40 BYTE),
  MORCARDEND    VARCHAR2(30 BYTE),
  MORCARDMEMO   VARCHAR2(100 BYTE),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE),
  MOSEQ         NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX REF11490 ON TBLMORCARD
(MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX PK37 ON TBLMORCARD
(MOCODE, SEQ)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_MORCARD_RCARD_START ON TBLMORCARD
(MORCARDSTART)
LOGGING
NOPARALLEL;

CREATE INDEX CARD_RANGE ON TBLMORCARD
(MORCARDSTART, MORCARDEND)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMORCARD
 ADD PRIMARY KEY
 (MOCODE, SEQ);

CREATE TABLE TBLMORCARDRANGE
(
  MOCODE        VARCHAR2(40 BYTE)               NOT NULL,
  SEQ           NUMBER(10)                      NOT NULL,
  RCARDTYPE     VARCHAR2(40 BYTE),
  MORCARDSTART  VARCHAR2(40 BYTE),
  MORCARDEND    VARCHAR2(40 BYTE),
  MORCARDMEMO   VARCHAR2(100 BYTE),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDXMORCARDRANGETYPE ON TBLMORCARDRANGE
(MOCODE, RCARDTYPE, MORCARDSTART, MORCARDEND)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMORCARDRANGE
 ADD PRIMARY KEY
 (MOCODE, SEQ);

CREATE TABLE TBLMOSTOCK
(
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  RECQTY          NUMBER(15,5),
  ISSUEQTY        NUMBER(15,5),
  SCRAPQTY        NUMBER(15,5),
  GAINLOSE        NUMBER(15,5),
  RETURNQTY       NUMBER(15,5),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  RETURNSCRAPQTY  NUMBER(15,5)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMOSTOCK
 ADD PRIMARY KEY
 (MOCODE, ITEMCODE);

CREATE TABLE TBLMOVIEWFIELD
(
  USERCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SEQ          NUMBER(10)                       NOT NULL,
  FIELDNAME    VARCHAR2(40 BYTE),
  DESCRIPTION  VARCHAR2(200 BYTE),
  ISDEFAULT    VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMOVIEWFIELD
 ADD PRIMARY KEY
 (USERCODE, SEQ);

CREATE TABLE TBLMSTOCKIN
(
  TKTNO        VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  CLTNO        VARCHAR2(40 BYTE),
  CLTTYPE      VARCHAR2(40 BYTE)                NOT NULL,
  STKTNO       VARCHAR2(40 BYTE),
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  ITEMCODE     VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  DELMEMO      VARCHAR2(100 BYTE),
  DELUSER      VARCHAR2(40 BYTE),
  DELDATE      NUMBER(8),
  DELTIME      NUMBER(8),
  STOCKMEMO    VARCHAR2(100 BYTE),
  MODELCODE    VARCHAR2(40 BYTE),
  OID          VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK297_1 ON TBLMSTOCKIN
(TKTNO, RCARD, OID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMSTOCKIN
 ADD CONSTRAINT PK297_1
 PRIMARY KEY
 (TKTNO, RCARD, OID);

CREATE TABLE TBLMSTOCKOUT
(
  TKTNO        VARCHAR2(40 BYTE)                NOT NULL,
  SEQ          NUMBER(10)                       NOT NULL,
  QTY          NUMBER(10)                       NOT NULL,
  STKTNO       VARCHAR2(40 BYTE),
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  DEALER       VARCHAR2(40 BYTE)                NOT NULL,
  OUTDATE      NUMBER(8)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  DELMEMO      VARCHAR2(100 BYTE),
  DELUSER      VARCHAR2(40 BYTE),
  DELDATE      NUMBER(8),
  DELTIME      NUMBER(8),
  STOCKMEMO    VARCHAR2(100 BYTE),
  OID          VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK297_2 ON TBLMSTOCKOUT
(TKTNO, SEQ, OID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMSTOCKOUT
 ADD CONSTRAINT PK297_2
 PRIMARY KEY
 (TKTNO, SEQ, OID);

CREATE TABLE TBLMSTOCKOUTDETAIL
(
  TKTNO        VARCHAR2(40 BYTE)                NOT NULL,
  SEQ          NUMBER(10)                       NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  CLTNO        VARCHAR2(40 BYTE),
  CLTTYPE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK349 ON TBLMSTOCKOUTDETAIL
(TKTNO, SEQ, RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMSTOCKOUTDETAIL
 ADD CONSTRAINT PK349
 PRIMARY KEY
 (TKTNO, SEQ, RCARD);

CREATE TABLE TBLOFFMOCARD
(
  PK           VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MOTYPE       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK415 ON TBLOFFMOCARD
(PK)
LOGGING
NOPARALLEL;

CREATE INDEX OFFMORCARD_MO ON TBLOFFMOCARD
(MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX OFFMOCARD_RCARD ON TBLOFFMOCARD
(RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOFFMOCARD
 ADD CONSTRAINT PK415
 PRIMARY KEY
 (PK);

CREATE TABLE TBLONWIP
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10)                     NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  TCARD          VARCHAR2(40 BYTE)              NOT NULL,
  TCARDSEQ       NUMBER(10),
  SCARD          VARCHAR2(40 BYTE)              NOT NULL,
  SCARDSEQ       NUMBER(10),
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ROUTECODE      VARCHAR2(40 BYTE),
  OPCODE         VARCHAR2(40 BYTE),
  SEGCODE        VARCHAR2(40 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTDAY       NUMBER(8),
  ACTION         VARCHAR2(40 BYTE)              NOT NULL,
  ACTIONRESULT   VARCHAR2(40 BYTE)              NOT NULL,
  NGTIMES        NUMBER(10),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  SHELFNO        VARCHAR2(40 BYTE),
  RMABILLCODE    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10),
  SERIAL         NUMBER(38),
  PROCESSED      NUMBER(1)                      DEFAULT (0)                   NOT NULL
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_SERIAL_TBLONWIP ON TBLONWIP
(SERIAL)
LOGGING
NOPARALLEL;

CREATE INDEX ONWIP_SHELF ON TBLONWIP
(SHELFNO)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIP_SHIFTDAY ON TBLONWIP
(SHIFTDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIP_RCARD ON TBLONWIP
(RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIP_MOCODE ON TBLONWIP
(MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIP_MDAY ON TBLONWIP
(MDATE, MTIME)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIP_MDATE ON TBLONWIP
(MDATE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIP_ITEMCODE ON TBLONWIP
(ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIP_ACTION ON TBLONWIP
(ACTION, EATTRIBUTE1)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIP
 ADD PRIMARY KEY
 (RCARD, RCARDSEQ, MOCODE);

CREATE TABLE TBLONWIPCARDTRANS
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10)                     NOT NULL,
  TCARD          VARCHAR2(40 BYTE)              NOT NULL,
  TCARDSEQ       NUMBER(10)                     NOT NULL,
  SCARD          VARCHAR2(40 BYTE)              NOT NULL,
  SCARDSEQ       NUMBER(10)                     NOT NULL,
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ROUTECODE      VARCHAR2(40 BYTE),
  OPCODE         VARCHAR2(40 BYTE),
  SEGCODE        VARCHAR2(40 BYTE),
  SSCODE         VARCHAR2(40 BYTE),
  RESCODE        VARCHAR2(40 BYTE),
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  IDMERGETYPE    VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK26_1_1 ON TBLONWIPCARDTRANS
(RCARD, MOCODE, RCARDSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX NDONWIPCARDTRANS01 ON TBLONWIPCARDTRANS
(SCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIPCARDTRANS_CARD ON TBLONWIPCARDTRANS
(RCARD, TCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ONWIPCARDTRANS_TCARD ON TBLONWIPCARDTRANS
(TCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPCARDTRANS
 ADD CONSTRAINT PK26_1_1
 PRIMARY KEY
 (RCARD, MOCODE, RCARDSEQ);

CREATE TABLE TBLONWIPCARTON
(
  CARTONCARD   VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10),
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK44_1 ON TBLONWIPCARTON
(CARTONCARD, RCARD, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPCARTON
 ADD CONSTRAINT PK44_1
 PRIMARY KEY
 (CARTONCARD, RCARD, MOCODE);

CREATE TABLE TBLONWIPCFGCOLLECT
(
  PKID            VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCONFIG      VARCHAR2(40 BYTE)             NOT NULL,
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  RCARDSEQ        NUMBER(10)                    NOT NULL,
  CATERGORYCODE   VARCHAR2(40 BYTE),
  CHECKITEMCODE   VARCHAR2(40 BYTE),
  ACTVALUE        VARCHAR2(40 BYTE),
  CHECKITEMVLAUE  VARCHAR2(40 BYTE),
  PARENTCODE      VARCHAR2(40 BYTE),
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ROUTECODE       VARCHAR2(40 BYTE),
  OPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  SEGCODE         VARCHAR2(40 BYTE)             NOT NULL,
  SSCODE          VARCHAR2(40 BYTE)             NOT NULL,
  RESCODE         VARCHAR2(40 BYTE)             NOT NULL,
  SHIFTTYPECODE   VARCHAR2(40 BYTE)             NOT NULL,
  SHIFTCODE       VARCHAR2(40 BYTE)             NOT NULL,
  TPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK44_2_2 ON TBLONWIPCFGCOLLECT
(PKID)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ITEMCODE_CONFIG ON TBLONWIPCFGCOLLECT
(ITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_COMPLEXINEX_CONFIG ON TBLONWIPCFGCOLLECT
(RCARD, MOCODE, CHECKITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPCFGCOLLECT
 ADD CONSTRAINT PK44_2_2
 PRIMARY KEY
 (PKID);

CREATE TABLE TBLONWIPECN
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10)                     NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  ECNNO          VARCHAR2(40 BYTE)              NOT NULL,
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ROUTECODE      VARCHAR2(40 BYTE)              NOT NULL,
  OPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK44_2_1 ON TBLONWIPECN
(RCARDSEQ, MOCODE, ECNNO, RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIPECN_RCARD ON TBLONWIPECN
(RCARD)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_TBLONWIPECN_IM ON TBLONWIPECN
(MOCODE, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPECN
 ADD CONSTRAINT PK44_2_1
 PRIMARY KEY
 (RCARD, RCARDSEQ, MOCODE, ECNNO);

CREATE TABLE TBLONWIPITEM
(
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  RCARDSEQ        NUMBER(10)                    NOT NULL,
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  MSEQ            NUMBER(10)                    NOT NULL,
  MCARD           VARCHAR2(40 BYTE),
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  MITEMCODE       VARCHAR2(40 BYTE),
  MCARDTYPE       VARCHAR2(40 BYTE)             NOT NULL,
  QTY             NUMBER(10)                    NOT NULL,
  LOTNO           VARCHAR2(40 BYTE),
  PCBA            VARCHAR2(40 BYTE),
  BIOS            VARCHAR2(40 BYTE),
  VERSION         VARCHAR2(40 BYTE),
  VENDORITEMCODE  VARCHAR2(40 BYTE),
  VENDORCODE      VARCHAR2(40 BYTE),
  DATECODE        VARCHAR2(40 BYTE),
  ROUTECODE       VARCHAR2(40 BYTE)             NOT NULL,
  OPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  SEGCODE         VARCHAR2(40 BYTE)             NOT NULL,
  SSCODE          VARCHAR2(40 BYTE)             NOT NULL,
  RESCODE         VARCHAR2(40 BYTE)             NOT NULL,
  SHIFTTYPECODE   VARCHAR2(40 BYTE)             NOT NULL,
  SHIFTCODE       VARCHAR2(40 BYTE)             NOT NULL,
  TPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  TRANSSTATUS     VARCHAR2(40 BYTE)             NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  DROPUSER        VARCHAR2(40 BYTE),
  DROPDATE        NUMBER(10),
  DROPTIME        NUMBER(10),
  ACTIONTYPE      NUMBER(10),
  DROPOP          VARCHAR2(40 BYTE),
  MOSEQ           NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX TBLONWIPITEM_ACTIONTYPE_DROPOP ON TBLONWIPITEM
(ACTIONTYPE, DROPOP)
LOGGING
NOPARALLEL;

CREATE INDEX TBLONWIPITEMMITEMCODE ON TBLONWIPITEM
(MITEMCODE)
LOGGING
NOPARALLEL;

CREATE INDEX TBLONWIPITEMMDATE ON TBLONWIPITEM
(MDATE)
LOGGING
NOPARALLEL;

CREATE INDEX PK44 ON TBLONWIPITEM
(RCARD, MOCODE, RCARDSEQ, MSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX ONWIPITEM_MOCODE_RCARD ON TBLONWIPITEM
(RCARD, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX ONWIPITEMMCARD ON TBLONWIPITEM
(MCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLONWIPITEM
 ADD CONSTRAINT PK44
 PRIMARY KEY
 (RCARD, RCARDSEQ, MOCODE, MSEQ);

CREATE TABLE TBLOPBOMDETAIL
(
  OPID            VARCHAR2(100 BYTE)            NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  OBCODE          VARCHAR2(40 BYTE)             NOT NULL,
  OPBOMVER        VARCHAR2(40 BYTE)             NOT NULL,
  OBITEMCODE      VARCHAR2(40 BYTE)             NOT NULL,
  OPCODE          VARCHAR2(40 BYTE),
  OBITEMNAME      VARCHAR2(100 BYTE),
  OBITEMECN       VARCHAR2(40 BYTE),
  OBITEMUOM       VARCHAR2(40 BYTE)             NOT NULL,
  OBITEMQTY       NUMBER(15,5)                  NOT NULL,
  OBSITEMCODE     VARCHAR2(40 BYTE),
  OBITEMVER       VARCHAR2(40 BYTE),
  OBITEMTYPE      VARCHAR2(40 BYTE)             NOT NULL,
  OBITEMCONTYPE   VARCHAR2(40 BYTE)             NOT NULL,
  OBITEMEFFDATE   NUMBER(8)                     NOT NULL,
  OBITEMEFFTIME   NUMBER(6)                     NOT NULL,
  OBITEMINVDATE   NUMBER(8)                     NOT NULL,
  OBITEMINVTIME   NUMBER(6)                     NOT NULL,
  ISITEMCHECK     VARCHAR2(1 BYTE)              NOT NULL,
  ITEMCHECKVALUE  VARCHAR2(40 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  ACTIONTYPE      NUMBER(10),
  CHECKSTATUS     VARCHAR2(40 BYTE),
  ORGID           NUMBER(8),
  OBITEMSEQ       NUMBER(8)                     NOT NULL,
  OBPARSETYPE     VARCHAR2(100 BYTE)            NOT NULL,
  OBCHECKTYPE     VARCHAR2(40 BYTE)             NOT NULL,
  OBVALID         NUMBER(8)                     NOT NULL,
  SNLENGTH        NUMBER(8),
  NEEDVENDOR      VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX SYS_C006064 ON TBLOPBOMDETAIL
(OBITEMCODE, ITEMCODE, OBCODE, OPBOMVER, OPID, 
ACTIONTYPE, ORGID)
LOGGING
NOPARALLEL;

CREATE INDEX REF256500 ON TBLOPBOMDETAIL
(OBCODE, OPBOMVER, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOPBOMDETAIL
 ADD PRIMARY KEY
 (OBITEMCODE, ITEMCODE, OBCODE, OPBOMVER, OPID, ACTIONTYPE, ORGID);

CREATE TABLE TBLOPITEMCONTROL
(
  OPID         VARCHAR2(100 BYTE)               NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  OBCODE       VARCHAR2(40 BYTE)                NOT NULL,
  OPBOMVER     VARCHAR2(40 BYTE)                NOT NULL,
  OBITEMCODE   VARCHAR2(40 BYTE)                NOT NULL,
  SEQ          NUMBER(10)                       NOT NULL,
  DCSTART      VARCHAR2(40 BYTE),
  DCEND        VARCHAR2(40 BYTE),
  VCODE        VARCHAR2(100 BYTE),
  VITEMCODE    VARCHAR2(100 BYTE),
  ITEMVER      VARCHAR2(100 BYTE),
  BIOSVER      VARCHAR2(100 BYTE),
  PCBAVER      VARCHAR2(100 BYTE),
  CARDSTART    VARCHAR2(40 BYTE),
  CARDEND      VARCHAR2(50 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  MEMO         VARCHAR2(100 BYTE),
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX REF255498 ON TBLOPITEMCONTROL
(OBITEMCODE, OPID, OBCODE, OPBOMVER, ITEMCODE)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK38 ON TBLOPITEMCONTROL
(OPID, ITEMCODE, OBCODE, OPBOMVER, OBITEMCODE, 
SEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOPITEMCONTROL
 ADD CONSTRAINT PK38
 PRIMARY KEY
 (OPID, ITEMCODE, OBCODE, OPBOMVER, OBITEMCODE, SEQ);

CREATE TABLE TBLOQCCARDLOTCKLIST
(
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10)                       NOT NULL,
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  LOTSEQ       NUMBER(10)                       NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  CKITEMCODE   VARCHAR2(40 BYTE)                NOT NULL,
  MDLCODE      VARCHAR2(40 BYTE)                NOT NULL,
  GRADE        VARCHAR2(40 BYTE),
  RESULT       VARCHAR2(40 BYTE)                NOT NULL,
  MEMO         VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  CKGROUP      VARCHAR2(40 BYTE)                NOT NULL,
  CHECKSEQ     NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLOQCCARDLOTCKLIST ON TBLOQCCARDLOTCKLIST
(RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE, 
ITEMCODE, CKITEMCODE, CKGROUP, CHECKSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCCARDLOTCKLIST
 ADD CONSTRAINT PK_TBLOQCCARDLOTCKLIST
 PRIMARY KEY
 (RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE, ITEMCODE, CKITEMCODE, CKGROUP, CHECKSEQ);

CREATE TABLE TBLOQCCKGROUP
(
  CKGROUP      VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLOQCCKGROUP ON TBLOQCCKGROUP
(CKGROUP)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCCKGROUP
 ADD CONSTRAINT PK_TBLOQCCKGROUP
 PRIMARY KEY
 (CKGROUP);

CREATE TABLE TBLOQCCKGROUP2LIST
(
  CKGROUP      VARCHAR2(40 BYTE)                NOT NULL,
  CKITEMCODE   VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLOQCCKGROUP2LIST ON TBLOQCCKGROUP2LIST
(CKGROUP, CKITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCCKGROUP2LIST
 ADD CONSTRAINT PK_TBLOQCCKGROUP2LIST
 PRIMARY KEY
 (CKGROUP, CKITEMCODE);

CREATE TABLE TBLOQCCKLIST
(
  CKITEMCODE   VARCHAR2(40 BYTE)                NOT NULL,
  CKITEMDESC   VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  CKGROUP      VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK109_1 ON TBLOQCCKLIST
(CKITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCCKLIST
 ADD CONSTRAINT PK109_1
 PRIMARY KEY
 (CKITEMCODE);

CREATE TABLE TBLOQCDIM
(
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10)                       NOT NULL,
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  LOTSEQ       NUMBER(10)                       NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  SEGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  RESCODE      VARCHAR2(40 BYTE)                NOT NULL,
  TESTRESULT   VARCHAR2(40 BYTE)                NOT NULL,
  TESTUSER     VARCHAR2(40 BYTE)                NOT NULL,
  TESTDATE     NUMBER(8)                        NOT NULL,
  TESTTIME     NUMBER(6)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_OQCDIM ON TBLOQCDIM
(RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCDIM
 ADD CONSTRAINT PK_OQCDIM
 PRIMARY KEY
 (RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE);

CREATE TABLE TBLOQCDIMVALUE
(
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  RCARDSEQ     NUMBER(10)                       NOT NULL,
  LOTNO        VARCHAR2(40 BYTE)                NOT NULL,
  LOTSEQ       NUMBER(10)                       NOT NULL,
  MOCODE       VARCHAR2(40 BYTE)                NOT NULL,
  PARAMNAME    VARCHAR2(40 BYTE)                NOT NULL,
  MINVALUE     NUMBER(15,5)                     NOT NULL,
  MAXVALUE     NUMBER(15,5)                     NOT NULL,
  ACTVALUE     NUMBER(15,5)                     NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_OQCDIMVALUE ON TBLOQCDIMVALUE
(RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE, 
PARAMNAME)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCDIMVALUE
 ADD CONSTRAINT PK_OQCDIMVALUE
 PRIMARY KEY
 (RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE, PARAMNAME);

CREATE TABLE TBLOQCFUNCTEST
(
  ITEMCODE            VARCHAR2(40 BYTE)         NOT NULL,
  FUNCTESTGROUPCOUNT  NUMBER(10),
  MINDUTYRATOMIN      NUMBER(15,5)              NOT NULL,
  MINDUTYRATOMAX      NUMBER(15,5)              NOT NULL,
  BURSTMDFREMIN       NUMBER(15,5)              NOT NULL,
  BURSTMDFREMAX       NUMBER(15,5)              NOT NULL,
  ELECTRICTESTCOUNT   NUMBER(10),
  MUSER               VARCHAR2(40 BYTE),
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLOQCFUNCTEST
 ADD PRIMARY KEY
 (ITEMCODE);

CREATE TABLE TBLOQCFUNCTESTSPEC
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  GROUPSEQ     NUMBER(10)                       NOT NULL,
  FREMIN       NUMBER(15,5)                     NOT NULL,
  FREMAX       NUMBER(15,5)                     NOT NULL,
  ELECTRICMIN  NUMBER(15,5)                     NOT NULL,
  ELECTRICMAX  NUMBER(15,5)                     NOT NULL,
  MUSER        VARCHAR2(40 BYTE),
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLOQCFUNCTESTSPEC
 ADD PRIMARY KEY
 (ITEMCODE, GROUPSEQ);

CREATE TABLE TBLOQCFUNCTESTVALUE
(
  RCARD               VARCHAR2(40 BYTE)         NOT NULL,
  RCARDSEQ            NUMBER(10)                NOT NULL,
  MODELCODE           VARCHAR2(40 BYTE),
  ITEMCODE            VARCHAR2(40 BYTE),
  MOCODE              VARCHAR2(40 BYTE),
  LOTNO               VARCHAR2(40 BYTE),
  LOTSEQ              NUMBER(10)                NOT NULL,
  FUNCTESTGROUPCOUNT  NUMBER(10),
  MINDUTYRATOMIN      NUMBER(15,5)              NOT NULL,
  MINDUTYRATOMAX      NUMBER(15,5)              NOT NULL,
  MINDUTYRATOVALUE    NUMBER(15,5)              NOT NULL,
  BURSTMDFREMIN       NUMBER(15,5)              NOT NULL,
  BURSTMDFREMAX       NUMBER(15,5)              NOT NULL,
  BURSTMDFREVALUE     NUMBER(15,5)              NOT NULL,
  ELECTRICTESTCOUNT   NUMBER(10),
  OPCODE              VARCHAR2(40 BYTE),
  RESCODE             VARCHAR2(40 BYTE),
  SEGCODE             VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE),
  PRODUCTSTATUS       VARCHAR2(40 BYTE),
  MUSER               VARCHAR2(40 BYTE),
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLOQCFUNCTESTVALUE
 ADD PRIMARY KEY
 (RCARD, RCARDSEQ);

CREATE TABLE TBLOQCLOT2CKGROUP
(
  LOTNO           VARCHAR2(40 BYTE)             NOT NULL,
  LOTSEQ          NUMBER(10)                    NOT NULL,
  CKGROUP         VARCHAR2(40 BYTE)             NOT NULL,
  CHECKEDCOUNT    NUMBER(8)                     NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  NEEDCHECKCOUNT  NUMBER(8)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLOQCLOT2CKGROUP ON TBLOQCLOT2CKGROUP
(LOTNO, LOTSEQ, CKGROUP)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCLOT2CKGROUP
 ADD CONSTRAINT PK_TBLOQCLOT2CKGROUP
 PRIMARY KEY
 (LOTNO, LOTSEQ, CKGROUP);

CREATE TABLE TBLOQCLOT2ERRORCODE
(
  LOTNO           VARCHAR2(40 BYTE)             NOT NULL,
  LOTSEQ          NUMBER(10)                    NOT NULL,
  ERRORCODEGROUP  VARCHAR2(40 BYTE)             NOT NULL,
  ECODE           VARCHAR2(40 BYTE)             NOT NULL,
  TIMES           NUMBER(10)                    NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK99_21 ON TBLOQCLOT2ERRORCODE
(LOTNO, LOTSEQ, ERRORCODEGROUP, ECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCLOT2ERRORCODE
 ADD CONSTRAINT PK99_21
 PRIMARY KEY
 (LOTNO, LOTSEQ, ERRORCODEGROUP, ECODE);

CREATE TABLE TBLOQCLOTCARD2ERRORCODE
(
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  RCARDSEQ        NUMBER(10)                    NOT NULL,
  LOTNO           VARCHAR2(40 BYTE)             NOT NULL,
  LOTSEQ          NUMBER(10)                    NOT NULL,
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  ERRORCODEGROUP  VARCHAR2(40 BYTE)             NOT NULL,
  ECODE           VARCHAR2(40 BYTE)             NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  MOSEQ           NUMBER(10),
  CHECKSEQ        NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX PK_TBLOQCLOTCARD2ERRORCODE ON TBLOQCLOTCARD2ERRORCODE
(RCARD, RCARDSEQ, LOTNO, ECODE, ERRORCODEGROUP, 
MOCODE, LOTSEQ, CHECKSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCLOTCARD2ERRORCODE
 ADD CONSTRAINT PK_TBLOQCLOTCARD2ERRORCODE
 PRIMARY KEY
 (RCARD, RCARDSEQ, LOTNO, LOTSEQ, ERRORCODEGROUP, ECODE, MOCODE, CHECKSEQ);

CREATE TABLE TBLOQCLOTCKLIST
(
  LOTNO         VARCHAR2(40 BYTE)               NOT NULL,
  LOTSEQ        NUMBER(10)                      NOT NULL,
  AGRADETIMES   NUMBER(10)                      NOT NULL,
  BGGRADETIMES  NUMBER(10)                      NOT NULL,
  CGRADETIMES   NUMBER(10)                      NOT NULL,
  RESULT        VARCHAR2(40 BYTE)               NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE),
  ZGRADETIMES   NUMBER(10)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK109_1_2 ON TBLOQCLOTCKLIST
(LOTNO, LOTSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX LOT_OQC_CHKLIST_LOTSEQ ON TBLOQCLOTCKLIST
(LOTSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLOQCLOTCKLIST
 ADD CONSTRAINT PK109_1_2
 PRIMARY KEY
 (LOTNO, LOTSEQ);

CREATE TABLE TBLLOT2CARD
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  LOTNO          VARCHAR2(40 BYTE)              NOT NULL,
  LOTSEQ         NUMBER(10)                     NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10),
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ROUTECODE      VARCHAR2(40 BYTE)              NOT NULL,
  OPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  STATUS         VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  COLLECTTYPE    CHAR(20 BYTE),
  MOSEQ          NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX LOT_INDEX_ON_LOT2CARD ON TBLLOT2CARD
(LOTNO)
LOGGING
NOPARALLEL;

CREATE INDEX PK44_3 ON TBLLOT2CARD
(RCARD, LOTNO, LOTSEQ, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX LOTNO_INDEX ON TBLLOT2CARD
(LOTNO, LOTSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX BUILD_TBLLOT2CARD ON TBLLOT2CARD
(MOCODE, RCARD, LOTNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLLOT2CARD
 ADD CONSTRAINT PK44_3
 PRIMARY KEY
 (RCARD, LOTNO, LOTSEQ, MOCODE);

CREATE TABLE TBLLOT2CARDCHECK
(
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  RCARDSEQ       NUMBER(10)                     NOT NULL,
  LOTNO          VARCHAR2(40 BYTE)              NOT NULL,
  LOTSEQ         NUMBER(10)                     NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ROUTECODE      VARCHAR2(40 BYTE)              NOT NULL,
  OPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  SEGCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTTYPECODE  VARCHAR2(40 BYTE)              NOT NULL,
  SHIFTCODE      VARCHAR2(40 BYTE)              NOT NULL,
  TPCODE         VARCHAR2(40 BYTE)              NOT NULL,
  STATUS         VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  ISDATALINK     VARCHAR2(1 BYTE)               DEFAULT '1',
  CHECKSEQ       NUMBER(10)                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLLOT2CARDCHECK ON TBLLOT2CARDCHECK
(RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE, 
CHECKSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX TBLLOT2CARDCHECKLOTNO ON TBLLOT2CARDCHECK
(LOTNO, LOTSEQ, MOCODE)
LOGGING
NOPARALLEL;

CREATE INDEX LOT_INDEX_ON_LOT2CARDCHECK ON TBLLOT2CARDCHECK
(LOTNO, LOTSEQ, STATUS)
LOGGING
NOPARALLEL;

ALTER TABLE TBLLOT2CARDCHECK
 ADD CONSTRAINT PK_TBLLOT2CARDCHECK
 PRIMARY KEY
 (RCARD, RCARDSEQ, LOTNO, LOTSEQ, MOCODE, CHECKSEQ);

CREATE TABLE TBLLOTSR
(
  SEQ           NUMBER(10)                      NOT NULL,
  LOTSIZESTART  NUMBER(10),
  LOTSIZEEND    NUMBER(10)                      NOT NULL,
  SSIZE         NUMBER(10)                      NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLLOTSR
 ADD PRIMARY KEY
 (SEQ);

CREATE TABLE TBLMACHINEFEEDER
(
  MOCODE              VARCHAR2(40 BYTE)         NOT NULL,
  MACHINECODE         VARCHAR2(40 BYTE)         NOT NULL,
  MACHINESTATIONCODE  VARCHAR2(40 BYTE)         NOT NULL,
  PRODUCTCODE         VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  FEEDERCODE          VARCHAR2(40 BYTE),
  REELNO              VARCHAR2(40 BYTE),
  LOADUSER            VARCHAR2(40 BYTE),
  LOADDATE            NUMBER(8)                 NOT NULL,
  LOADTIME            NUMBER(6)                 NOT NULL,
  MATERIALCODE        VARCHAR2(40 BYTE),
  UNITQTY             NUMBER(15,5)              NOT NULL,
  LOTNO               VARCHAR2(40 BYTE),
  DATECODE            VARCHAR2(40 BYTE),
  CHECKRESULT         VARCHAR2(1 BYTE)          NOT NULL,
  FAILREASON          VARCHAR2(100 BYTE),
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE),
  NEXTREELNO          VARCHAR2(40 BYTE),
  OPERESCODE          VARCHAR2(40 BYTE),
  OPESSCODE           VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE),
  ENABLED             VARCHAR2(1 BYTE),
  REELCEASEFLAG       VARCHAR2(1 BYTE),
  STATIONENABLED      VARCHAR2(1 BYTE),
  TBLGRP              VARCHAR2(40 BYTE)         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_MACHINEFEEDER_CHECK ON TBLMACHINEFEEDER
(CHECKRESULT, FEEDERCODE, REELNO, NEXTREELNO, FEEDERSPECCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMACHINEFEEDER
 ADD PRIMARY KEY
 (MOCODE, MACHINECODE, MACHINESTATIONCODE, TBLGRP);

CREATE TABLE TBLMACHINEFEEDERLOG
(
  LOGNO               NUMBER(10)                NOT NULL,
  PRODUCTCODE         VARCHAR2(40 BYTE),
  MOCODE              VARCHAR2(40 BYTE),
  MACHINECODE         VARCHAR2(40 BYTE),
  MACHINESTATIONCODE  VARCHAR2(40 BYTE),
  FEEDERSPECCODE      VARCHAR2(40 BYTE),
  FEEDERCODE          VARCHAR2(40 BYTE),
  REELNO              VARCHAR2(40 BYTE),
  LOADUSER            VARCHAR2(40 BYTE),
  LOADDATE            NUMBER(8)                 NOT NULL,
  LOADTIME            NUMBER(6)                 NOT NULL,
  MATERIALCODE        VARCHAR2(40 BYTE),
  UNITQTY             NUMBER(15,5)              NOT NULL,
  LOTNO               VARCHAR2(40 BYTE),
  DATECODE            VARCHAR2(40 BYTE),
  CHECKRESULT         VARCHAR2(1 BYTE)          NOT NULL,
  FAILREASON          VARCHAR2(100 BYTE),
  OPERATIONTYPE       VARCHAR2(40 BYTE),
  REELUSEDQTY         NUMBER(15,5)              NOT NULL,
  MUSER               VARCHAR2(40 BYTE)         NOT NULL,
  MDATE               NUMBER(8)                 NOT NULL,
  MTIME               NUMBER(6)                 NOT NULL,
  EATTRIBUTE1         VARCHAR2(40 BYTE),
  OPERESCODE          VARCHAR2(40 BYTE),
  OPESSCODE           VARCHAR2(40 BYTE),
  UNLOADUSER          VARCHAR2(40 BYTE),
  UNLOADDATE          NUMBER(8),
  UNLOADTIME          NUMBER(6),
  UNLOADTYPE          VARCHAR2(40 BYTE),
  EXCHGFEEDERCODE     VARCHAR2(40 BYTE),
  EXCHGREELNO         VARCHAR2(40 BYTE),
  SSCODE              VARCHAR2(40 BYTE),
  REELCHKDIFFQTY      NUMBER(15,5),
  STATIONENABLED      VARCHAR2(1 BYTE),
  TBLGRP              VARCHAR2(40 BYTE),
  MOSEQ               NUMBER(10)
)
NOLOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLMACHINEFEEDERLOG ON TBLMACHINEFEEDERLOG
(LOGNO)
LOGGING
NOPARALLEL;

CREATE INDEX IDX_MACHINEFEEDERLOG_QUERY_T ON TBLMACHINEFEEDERLOG
(MOCODE, MACHINECODE, MACHINESTATIONCODE, FEEDERCODE, REELNO, 
CHECKRESULT)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMACHINEFEEDERLOG
 ADD CONSTRAINT PK_TBLMACHINEFEEDERLOG
 PRIMARY KEY
 (LOGNO);

CREATE TABLE TBLMACINFO
(
  MOCODE      VARCHAR2(40 BYTE)                 NOT NULL,
  RCARD       VARCHAR2(40 BYTE)                 NOT NULL,
  RCARDSEQ    NUMBER(10)                        NOT NULL,
  MACID       VARCHAR2(100 BYTE)                NOT NULL,
  MACADDRESS  VARCHAR2(100 BYTE)                NOT NULL,
  ROUTECODE   VARCHAR2(40 BYTE)                 NOT NULL,
  OPCODE      VARCHAR2(40 BYTE)                 NOT NULL,
  SEGCODE     VARCHAR2(40 BYTE)                 NOT NULL,
  SSCODE      VARCHAR2(40 BYTE)                 NOT NULL,
  RESCODE     VARCHAR2(40 BYTE)                 NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMACINFO
 ADD PRIMARY KEY
 (MOCODE, RCARD);

CREATE TABLE TBLMATERIAL
(
  MCODE          VARCHAR2(40 BYTE)              NOT NULL,
  MNAME          VARCHAR2(40 BYTE),
  MDESC          VARCHAR2(200 BYTE),
  MUOM           VARCHAR2(40 BYTE),
  MTYPE          VARCHAR2(40 BYTE)              NOT NULL,
  MMACHINETYPE   VARCHAR2(40 BYTE),
  MVOLUME        VARCHAR2(40 BYTE),
  MMODELCODE     VARCHAR2(40 BYTE),
  MEXPORTIMPORT  VARCHAR2(40 BYTE)              NOT NULL,
  MMODELGROUP    VARCHAR2(40 BYTE),
  MGROUP         VARCHAR2(40 BYTE),
  MGROUPDESC     VARCHAR2(40 BYTE),
  MCONTROLTYPE   VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  ORGID          NUMBER(8)                      NOT NULL,
  MPARSETYPE     VARCHAR2(100 BYTE),
  CHECKSTATUS    VARCHAR2(40 BYTE),
  MCHECKTYPE     VARCHAR2(40 BYTE),
  SNLENGTH       NUMBER(8),
  VENDORCODE     VARCHAR2(40 BYTE),
  ROHS           VARCHAR2(40 BYTE),
  NEEDVENDOR     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX I_TBLMATERIAL_MCODE ON TBLMATERIAL
(MCODE)
LOGGING
NOPARALLEL;

CREATE INDEX I_TBLMATERIAL_MNAME ON TBLMATERIAL
(MNAME)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMATERIAL
 ADD PRIMARY KEY
 (MCODE, ORGID);

CREATE TABLE TBLMATERIALLOT
(
  MATERIALLOT  VARCHAR2(40 BYTE)                NOT NULL,
  IQCNO        VARCHAR2(50 BYTE),
  STLINE       NUMBER(6),
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  VENDORCODE   VARCHAR2(40 BYTE)                NOT NULL,
  ORGID        NUMBER(8)                        NOT NULL,
  STORAGEID    VARCHAR2(30 BYTE)                NOT NULL,
  UNIT         VARCHAR2(40 BYTE)                NOT NULL,
  CREATEDATE   NUMBER(8)                        NOT NULL,
  LOTINQTY     NUMBER(13)                       NOT NULL,
  LOTQTY       NUMBER(13)                       NOT NULL,
  FIFOFLAG     VARCHAR2(1 BYTE)                 NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMATERIALLOT
 ADD PRIMARY KEY
 (MATERIALLOT);

CREATE TABLE TBLMATERIALRECEIVE
(
  IQCNO            VARCHAR2(50 BYTE)            NOT NULL,
  STNO             VARCHAR2(40 BYTE)            NOT NULL,
  STLINE           NUMBER(6)                    NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  REALRECEIVEQTY   NUMBER(13),
  RECEIVEMEMO      VARCHAR2(1000 BYTE),
  ACCOUNTDATE      NUMBER(8)                    NOT NULL,
  VOUCHERDATE      NUMBER(8)                    NOT NULL,
  ORDERNO          VARCHAR2(40 BYTE),
  ORDERLINE        NUMBER(22),
  ORGID            NUMBER(8)                    NOT NULL,
  STORAGEID        VARCHAR2(4 BYTE)             NOT NULL,
  UNIT             VARCHAR2(40 BYTE),
  FLAG             VARCHAR2(10 BYTE),
  TRANSACTIONCODE  VARCHAR2(100 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMATERIALRECEIVE
 ADD PRIMARY KEY
 (IQCNO, STLINE);

CREATE TABLE TBLMATERIALRETURN
(
  MATERIALLOT  VARCHAR2(40 BYTE)                NOT NULL,
  POSTSEQ      NUMBER(8)                        NOT NULL,
  TRANSQTY     NUMBER(13),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMATERIALRETURN
 ADD PRIMARY KEY
 (MATERIALLOT, POSTSEQ);

CREATE TABLE TBLMATERIALSTORAGEINFO
(
  ITEMCODE   VARCHAR2(40 BYTE)                  NOT NULL,
  ORGID      NUMBER(8)                          NOT NULL,
  STORAGEID  VARCHAR2(4 BYTE)                   NOT NULL,
  CLABSQTY   NUMBER(13)                         NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMATERIALSTORAGEINFO
 ADD PRIMARY KEY
 (ITEMCODE, ORGID, STORAGEID);

CREATE TABLE TBLMAXWIPSERIAL
(
  TBLONWIP_SERIAL  NUMBER(38)                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLMAXWIPSERIAL ON TBLMAXWIPSERIAL
(TBLONWIP_SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMAXWIPSERIAL
 ADD CONSTRAINT PK_TBLMAXWIPSERIAL
 PRIMARY KEY
 (TBLONWIP_SERIAL);

CREATE TABLE TBLMDL
(
  MDLCODE      VARCHAR2(40 BYTE)                NOT NULL,
  PMDLCODE     VARCHAR2(40 BYTE),
  MDLVER       VARCHAR2(40 BYTE),
  MDLTYPE      VARCHAR2(40 BYTE)                NOT NULL,
  MDLSTATUS    VARCHAR2(40 BYTE)                NOT NULL,
  MDLDESC      VARCHAR2(100 BYTE),
  MDLSEQ       NUMBER(10)                       NOT NULL,
  MDLHFNAME    VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  ISSYS        VARCHAR2(1 BYTE)                 NOT NULL,
  ISACTIVE     VARCHAR2(1 BYTE)                 NOT NULL,
  FORMURL      VARCHAR2(100 BYTE),
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ISRESTRAIN   VARCHAR2(1 BYTE)                 DEFAULT '0'
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK52 ON TBLMDL
(MDLCODE)
LOGGING
NOPARALLEL;

CREATE INDEX MDL_PMAL ON TBLMDL
(PMDLCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMDL
 ADD CONSTRAINT PK52
 PRIMARY KEY
 (MDLCODE);

CREATE TABLE TBLCREW
(
  CREWCODE     VARCHAR2(40 BYTE)                NOT NULL,
  CREWDESC     VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLCREW
 ADD PRIMARY KEY
 (CREWCODE);

CREATE TABLE TBLCSUPDATER
(
  CSVERSION      VARCHAR2(30 BYTE)              NOT NULL,
  LOCATION       VARCHAR2(100 BYTE),
  LOGINUSER      VARCHAR2(30 BYTE),
  LOGINPASSWORD  VARCHAR2(20 BYTE),
  ISAVIABLE      NUMBER(1)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK ON TBLCSUPDATER
(CSVERSION, ISAVIABLE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLCSUPDATER
 ADD CONSTRAINT PK
 PRIMARY KEY
 (CSVERSION, ISAVIABLE);

CREATE TABLE TBLDATATRANSFERLOG
(
  JOBID                VARCHAR2(100 BYTE)       NOT NULL,
  TRANSACTIONCODE      VARCHAR2(100 BYTE)       NOT NULL,
  TRANSACTIONSEQUENCE  NUMBER(8)                NOT NULL,
  REQUESTDATE          NUMBER(8)                NOT NULL,
  REQUESTTIME          NUMBER(6)                NOT NULL,
  REQUESTCONTENT       VARCHAR2(500 BYTE)       NOT NULL,
  RESPONSEDATE         NUMBER(8),
  RESPONSETIME         NUMBER(6),
  RESPONSECONTENT      VARCHAR2(500 BYTE),
  FINISHEDDATE         NUMBER(8),
  FINISHEDTIME         NUMBER(6),
  RESULT               VARCHAR2(40 BYTE),
  ERRORMESSAGE         VARCHAR2(2000 BYTE),
  SENDRECORDCOUNT      NUMBER(9)                DEFAULT 1,
  RECEIVEDRECORDCOUNT  NUMBER(9)                DEFAULT 1,
  ORGID                NUMBER(8)                NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_DATATRANSFERLOG ON TBLDATATRANSFERLOG
(TRANSACTIONCODE, TRANSACTIONSEQUENCE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLDATATRANSFERLOG
 ADD CONSTRAINT PK_DATATRANSFERLOG
 PRIMARY KEY
 (TRANSACTIONCODE, TRANSACTIONSEQUENCE);

CREATE TABLE TBLDCT
(
  DCTCODE      VARCHAR2(40 BYTE)                NOT NULL,
  DCTDESC      VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLDCT
 ADD PRIMARY KEY
 (DCTCODE);

CREATE TABLE TBLDEFAULTITEM2ROUTE
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLDEFAULTITEM2ROUTE
 ADD PRIMARY KEY
 (ITEMCODE);

CREATE TABLE TBLDN
(
  DNNO             VARCHAR2(40 BYTE)            NOT NULL,
  SHIPTOPARTY      VARCHAR2(40 BYTE)            NOT NULL,
  DNLINE           VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  ITEMDESC         VARCHAR2(100 BYTE),
  ORGID            NUMBER(8)                    NOT NULL,
  FRMSTORAGE       VARCHAR2(40 BYTE),
  DNQUANTITY       NUMBER(13)                   NOT NULL,
  REALQUANTITY     NUMBER(13),
  UNIT             VARCHAR2(40 BYTE)            NOT NULL,
  MOVEMENTTYPE     VARCHAR2(40 BYTE),
  ITEMGRADE        VARCHAR2(40 BYTE),
  MOCODE           VARCHAR2(40 BYTE),
  ORDERNO          VARCHAR2(40 BYTE),
  CUSORDERNO       VARCHAR2(40 BYTE),
  CUSORDERNOTYPE   VARCHAR2(40 BYTE),
  TOSTORAGE        VARCHAR2(40 BYTE),
  STATUS           VARCHAR2(40 BYTE),
  MUSER            VARCHAR2(40 BYTE),
  MDATE            NUMBER(8),
  MTIME            NUMBER(6),
  DNFROM           VARCHAR2(40 BYTE),
  DNSTATUS         VARCHAR2(40 BYTE),
  RELATEDDOCUMENT  VARCHAR2(100 BYTE),
  BUSINESSCODE     VARCHAR2(40 BYTE),
  DEPT             VARCHAR2(40 BYTE),
  MEMO             VARCHAR2(200 BYTE),
  REWORKMOCODE     VARCHAR2(40 BYTE),
  SAPSTORAGE       VARCHAR2(40 BYTE),
  FLAG             VARCHAR2(10 BYTE),
  TRANSACTIONCODE  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLDN ON TBLDN
(DNNO, DNLINE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLDN
 ADD CONSTRAINT PK_TBLDN
 PRIMARY KEY
 (DNNO, DNLINE);

CREATE TABLE TBLDN2SAP
(
  DNNO          VARCHAR2(40 BYTE)               NOT NULL,
  FLAG          VARCHAR2(40 BYTE)               NOT NULL,
  ERRORMESSAGE  VARCHAR2(100 BYTE),
  ACTIVE        VARCHAR2(40 BYTE)               NOT NULL,
  MUSER         VARCHAR2(40 BYTE),
  MDATE         NUMBER(8),
  MTIME         NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLDN2SAP ON TBLDN2SAP
(DNNO)
LOGGING
NOPARALLEL;

CREATE TABLE TBLDOWN
(
  DOWNCODE       VARCHAR2(40 BYTE)              NOT NULL,
  RCARD          VARCHAR2(40 BYTE)              NOT NULL,
  MOCODE         VARCHAR2(40 BYTE)              NOT NULL,
  MODELCODE      VARCHAR2(40 BYTE)              NOT NULL,
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  SSCODE         VARCHAR2(40 BYTE)              NOT NULL,
  RESCODE        VARCHAR2(40 BYTE)              NOT NULL,
  DOWNSTATUS     VARCHAR2(40 BYTE)              NOT NULL,
  DOWNREASON     VARCHAR2(100 BYTE)             NOT NULL,
  DOWNDATE       NUMBER(8)                      NOT NULL,
  DOWNSHIFTDATE  NUMBER(8)                      NOT NULL,
  DOWNTIME       NUMBER(6)                      NOT NULL,
  DOWNBY         VARCHAR2(40 BYTE)              NOT NULL,
  UPREASON       VARCHAR2(100 BYTE),
  UPDAY          NUMBER(8),
  UPTIME         NUMBER(6),
  UPBY           VARCHAR2(40 BYTE),
  MUSER          VARCHAR2(40 BYTE),
  MDATE          NUMBER(8),
  MTIME          NUMBER(6),
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  ORGID          NUMBER(8)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLDOWN ON TBLDOWN
(DOWNCODE, RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLDOWN
 ADD CONSTRAINT PK_TBLDOWN
 PRIMARY KEY
 (DOWNCODE, RCARD);

CREATE TABLE TBLDUTY
(
  DUTYCODE     VARCHAR2(40 BYTE)                NOT NULL,
  DUTYDESC     VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(38)                       NOT NULL,
  MTIME        NUMBER(38)                       NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK77 ON TBLDUTY
(DUTYCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLDUTY
 ADD CONSTRAINT PK77
 PRIMARY KEY
 (DUTYCODE);

CREATE TABLE TBLEC
(
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ECDESC       VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK72 ON TBLEC
(ECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLEC
 ADD CONSTRAINT PK72
 PRIMARY KEY
 (ECODE);

CREATE TABLE TBLEC2OPREWORK
(
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE),
  TOOPCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLEC2OPREWORK ON TBLEC2OPREWORK
(OPCODE, ECODE, ORGID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLEC2OPREWORK
 ADD CONSTRAINT PK_TBLEC2OPREWORK
 PRIMARY KEY
 (OPCODE, ECODE, ORGID);

CREATE TABLE TBLECG
(
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECGDESC      VARCHAR2(100 BYTE),
  ROUTECODE    VARCHAR2(40 BYTE),
  OPCODE       VARCHAR2(40 BYTE),
  RESCODE      VARCHAR2(40 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK73 ON TBLECG
(ECGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLECG
 ADD CONSTRAINT PK73
 PRIMARY KEY
 (ECGCODE);

CREATE TABLE TBLECG2EC
(
  ECGCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECODE        VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK73_1 ON TBLECG2EC
(ECGCODE, ECODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLECG2EC
 ADD CONSTRAINT PK73_1
 PRIMARY KEY
 (ECGCODE, ECODE);

CREATE TABLE TBLECITEM2ROUTE
(
  ECGCODE      VARCHAR2(40 BYTE),
  ECCODE       VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ROUTECODE    VARCHAR2(40 BYTE),
  OPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_ECITEM2ROUTE ON TBLECITEM2ROUTE
(ECCODE, ITEMCODE, ORGID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLECITEM2ROUTE
 ADD CONSTRAINT PK_ECITEM2ROUTE
 PRIMARY KEY
 (ECCODE, ITEMCODE, ORGID);

CREATE TABLE TBLECS
(
  ECSCODE      VARCHAR2(40 BYTE)                NOT NULL,
  ECSDESC      VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK75 ON TBLECS
(ECSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLECS
 ADD CONSTRAINT PK75
 PRIMARY KEY
 (ECSCODE);

CREATE TABLE TBLECSG
(
  ECSGCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ECSGDESC     VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_ECSG ON TBLECSG
(ECSGCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLECSG
 ADD CONSTRAINT PK_ECSG
 PRIMARY KEY
 (ECSGCODE);

CREATE TABLE TBLECSG2ECS
(
  ECSGCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ECSCODE      VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(8)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_ECSG2ECS ON TBLECSG2ECS
(ECSGCODE, ECSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLECSG2ECS
 ADD CONSTRAINT PK_ECSG2ECS
 PRIMARY KEY
 (ECSGCODE, ECSCODE);

CREATE TABLE TBLERPBOM
(
  SEQUENCE   NUMBER(10)                         NOT NULL,
  BITEMCODE  VARCHAR2(40 BYTE)                  NOT NULL,
  BQTY       NUMBER(10)                         NOT NULL,
  LOTNO      VARCHAR2(40 BYTE)                  NOT NULL,
  MOCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PKERPBOM ON TBLERPBOM
(SEQUENCE)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_ERPBOM_LOTNO ON TBLERPBOM
(LOTNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLERPBOM
 ADD CONSTRAINT PKERPBOM
 PRIMARY KEY
 (SEQUENCE);

CREATE TABLE TBLERPINVINTERFACE
(
  RECNO       VARCHAR2(40 BYTE)                 NOT NULL,
  MOCODE      VARCHAR2(40 BYTE)                 NOT NULL,
  STATUS      VARCHAR2(40 BYTE)                 NOT NULL,
  SRNO        VARCHAR2(40 BYTE),
  ITEMCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  QTY         NUMBER(10)                        NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL,
  UPLOADDATE  NUMBER(8),
  UPLOADTIME  NUMBER(6),
  UPLOADUSER  VARCHAR2(40 BYTE),
  LINKSRNO    VARCHAR2(40 BYTE),
  EATTIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLERPINVINTERFACE
 ADD PRIMARY KEY
 (RECNO, MOCODE, STATUS, SRNO);

CREATE TABLE TBLES
(
  SYMPTOMCODE  VARCHAR2(40 BYTE)                NOT NULL,
  ESDESC       VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX ERRORSYMPTOM ON TBLES
(SYMPTOMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLES
 ADD CONSTRAINT ERRORSYMPTOM
 PRIMARY KEY
 (SYMPTOMCODE);

CREATE TABLE TBLFACTORY
(
  FACCODE      VARCHAR2(40 BYTE)                NOT NULL,
  FACDESC      VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ORGID        NUMBER(8)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLFACTORY
 ADD PRIMARY KEY
 (FACCODE);

CREATE TABLE TBLFEEDER
(
  FEEDERCODE       VARCHAR2(40 BYTE)            NOT NULL,
  FEEDERTYPE       VARCHAR2(40 BYTE),
  FEEDERSPECCODE   VARCHAR2(40 BYTE),
  MAXCOUNT         NUMBER(10)                   NOT NULL,
  USEDCOUNT        NUMBER(10)                   NOT NULL,
  ALERTCOUNT       NUMBER(10)                   NOT NULL,
  TOTALCOUNT       NUMBER(10)                   NOT NULL,
  STATUS           VARCHAR2(40 BYTE),
  STATUSCHGREASON  VARCHAR2(40 BYTE),
  STATUSCHGDATE    NUMBER(8)                    NOT NULL,
  STATUSCHGTIME    NUMBER(6)                    NOT NULL,
  RETREASON        VARCHAR2(100 BYTE),
  USEFLAG          VARCHAR2(1 BYTE)             NOT NULL,
  MOCODE           VARCHAR2(40 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  CURRUNITQTY      NUMBER(18,5),
  SSCODE           VARCHAR2(40 BYTE),
  OPEUSER          VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLFEEDER_SPEC ON TBLFEEDER
(FEEDERSPECCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLFEEDER
 ADD PRIMARY KEY
 (FEEDERCODE);

CREATE TABLE TBLFEEDERMAINTAIN
(
  FEEDERCODE      VARCHAR2(40 BYTE)             NOT NULL,
  SEQ             CHAR(10 BYTE)                 NOT NULL,
  FEEDERTYPE      VARCHAR2(40 BYTE),
  FEEDERSPECCODE  VARCHAR2(40 BYTE),
  STATUS          VARCHAR2(40 BYTE),
  MAXCOUNT        NUMBER(10)                    NOT NULL,
  USEDCOUNT       NUMBER(10)                    NOT NULL,
  TOTALCOUNT      NUMBER(10)                    NOT NULL,
  OLDSTATUS       VARCHAR2(40 BYTE),
  MAINTAINTYPE    VARCHAR2(40 BYTE),
  RETREASON       VARCHAR2(100 BYTE),
  ANALYSEREASON   VARCHAR2(100 BYTE),
  OPERMESSAGE     VARCHAR2(100 BYTE),
  SCRAPFLAG       VARCHAR2(1 BYTE)              NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLFEEDERMAINTAIN
 ADD PRIMARY KEY
 (FEEDERCODE, SEQ);

CREATE TABLE TBLFEEDERSPEC
(
  FEEDERSPECCODE  VARCHAR2(40 BYTE)             NOT NULL,
  NAME            VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLFEEDERSPEC
 ADD PRIMARY KEY
 (FEEDERSPECCODE);

CREATE TABLE TBLFEEDERSTATUSLOG
(
  FEEDERCODE       VARCHAR2(40 BYTE)            NOT NULL,
  SEQ              NUMBER(10)                   NOT NULL,
  FEEDERTYPE       VARCHAR2(40 BYTE),
  FEEDERSPECCODE   VARCHAR2(40 BYTE),
  STATUS           VARCHAR2(40 BYTE),
  OLDSTATUS        VARCHAR2(40 BYTE),
  STATUSCHGREASON  VARCHAR2(40 BYTE),
  STATUSCHGDATE    NUMBER(8)                    NOT NULL,
  STATUSCHGTIME    NUMBER(6)                    NOT NULL,
  MOCODE           VARCHAR2(40 BYTE),
  OTHERMESSAGE     VARCHAR2(100 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(40 BYTE),
  SSCODE           VARCHAR2(40 BYTE),
  OPEUSER          VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLFEEDERSTATUSLOG
 ADD PRIMARY KEY
 (FEEDERCODE, SEQ);

CREATE TABLE TBLFIRSTONLINE
(
  SSCODE        VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  ACTIONTYPE    VARCHAR2(40 BYTE)               NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  RCARD         VARCHAR2(40 BYTE)               NOT NULL,
  ONLINETIME    NUMBER(6)                       NOT NULL,
  OFFLINETIME   NUMBER(6)                       NOT NULL,
  ITEMCODE      VARCHAR2(40 BYTE)               NOT NULL,
  OFFRCARD      VARCHAR2(40 BYTE),
  SHIFTCODE     VARCHAR2(40 BYTE)               NOT NULL,
  SHIFTTIME     NUMBER(6),
  ISOVERDAY     CHAR(1 BYTE),
  LASTTYPE      VARCHAR2(40 BYTE),
  LASTONRCARD   VARCHAR2(40 BYTE),
  LASTOFFRCARD  VARCHAR2(40 BYTE),
  LASTONTIME    NUMBER(6),
  LASTOFFTIME   NUMBER(6),
  ENDTIME       NUMBER(6),
  MODELCODE     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDFIRSTONLINE02 ON TBLFIRSTONLINE
(SSCODE, MDATE, LASTTYPE, ITEMCODE, MODELCODE, 
SHIFTCODE, ENDTIME, ISOVERDAY)
LOGGING
NOPARALLEL;

CREATE INDEX INDFIRSTONLINE01 ON TBLFIRSTONLINE
(SSCODE, ITEMCODE, MODELCODE, SHIFTTIME, SHIFTCODE, 
MDATE)
LOGGING
NOPARALLEL;

CREATE INDEX INDFIRSTONLINE002 ON TBLFIRSTONLINE
(SSCODE, ENDTIME, ISOVERDAY, SHIFTCODE, MDATE, 
ITEMCODE, LASTTYPE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLFIRSTONLINE
 ADD PRIMARY KEY
 (SSCODE, MDATE, ITEMCODE, SHIFTCODE, SHIFTTIME);

CREATE TABLE TBLFROZEN
(
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  LOTNO           VARCHAR2(40 BYTE)             NOT NULL,
  LOTSEQ          NUMBER(10)                    NOT NULL,
  MOCODE          VARCHAR2(40 BYTE)             NOT NULL,
  MODELCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  FROZENSEQ       NUMBER(10)                    NOT NULL,
  FROZENSTATUS    VARCHAR2(40 BYTE)             NOT NULL,
  FROZENREASON    VARCHAR2(100 BYTE),
  FROZENDATE      NUMBER(8),
  FROZENTIME      NUMBER(6),
  FROZENBY        VARCHAR2(40 BYTE),
  UNFROZENREASON  VARCHAR2(100 BYTE),
  UNFROZENDATE    NUMBER(8),
  UNFROZENTIME    NUMBER(6),
  UNFROZENBY      VARCHAR2(40 BYTE),
  MUSER           VARCHAR2(40 BYTE),
  MDATE           NUMBER(8),
  MTIME           NUMBER(6),
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLFROZEN
 ADD PRIMARY KEY
 (RCARD, LOTNO, LOTSEQ, MOCODE, ITEMCODE, FROZENSEQ);

CREATE TABLE TBLFT
(
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  TESTSEQ      NUMBER(10)                       NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  RESCODE      VARCHAR2(40 BYTE)                NOT NULL,
  LINECODE     VARCHAR2(40 BYTE)                NOT NULL,
  MACHINETOOL  VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  TESTRESULT   VARCHAR2(1 BYTE)                 NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK1_1_1 ON TBLFT
(RCARD, TESTSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLFT
 ADD CONSTRAINT PK1_1_1
 PRIMARY KEY
 (RCARD, TESTSEQ);

CREATE TABLE TBLFUNCTIONGROUP
(
  FUNCTIONGROUPCODE  VARCHAR2(40 BYTE)          NOT NULL,
  FUNCTIONGROUPDESC  VARCHAR2(40 BYTE),
  MUSER              VARCHAR2(40 BYTE)          NOT NULL,
  MTIME              NUMBER(6)                  NOT NULL,
  MDATE              NUMBER(8)                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLFUNCTIONGROUP
 ADD PRIMARY KEY
 (FUNCTIONGROUPCODE);

CREATE TABLE TBLFUNCTIONGROUP2FUNCTION
(
  MDLCODE            VARCHAR2(40 BYTE)          NOT NULL,
  FUNCTIONGROUPCODE  VARCHAR2(40 BYTE)          NOT NULL,
  VIEWVALUE          VARCHAR2(40 BYTE),
  MUSER              VARCHAR2(40 BYTE)          NOT NULL,
  MTIME              NUMBER(6)                  NOT NULL,
  MDATE              NUMBER(8)                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLFUNCTIONGROUP2FUNCTION
 ADD PRIMARY KEY
 (MDLCODE, FUNCTIONGROUPCODE);

CREATE TABLE TBLIDINFO
(
  MOCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  RCARD      VARCHAR2(40 BYTE)                  NOT NULL,
  RCARDSEQ   VARCHAR2(22 BYTE)                  NOT NULL,
  ID1        VARCHAR2(100 BYTE),
  ID2        VARCHAR2(100 BYTE),
  ID3        VARCHAR2(100 BYTE),
  ROUTECODE  VARCHAR2(40 BYTE)                  NOT NULL,
  OPCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  SEGCODE    VARCHAR2(40 BYTE)                  NOT NULL,
  SSCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  RESCODE    VARCHAR2(40 BYTE)                  NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_OF_TBLIDINFO ON TBLIDINFO
(MOCODE, RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLIDINFO
 ADD PRIMARY KEY
 (MOCODE, RCARD);

CREATE TABLE DCTMESSAGE
(
  SERIALNO        NUMBER(8)                     NOT NULL,
  FROMADDRESS     VARCHAR2(40 CHAR),
  FROMPORT        NUMBER(8),
  TOADDRESS       VARCHAR2(40 CHAR),
  TOPORT          NUMBER(8),
  DIRECTION       VARCHAR2(40 CHAR),
  MESSAGETYPE     VARCHAR2(40 CHAR),
  MESSAGECONTENT  VARCHAR2(1000 CHAR),
  STATUS          VARCHAR2(40 CHAR),
  MUSER           VARCHAR2(40 CHAR),
  MDATE           NUMBER(8),
  MTIME           NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE DCTMESSAGE
 ADD PRIMARY KEY
 (SERIALNO);

CREATE TABLE ITMATERIALRECNO
(
  POCODE    VARCHAR2(40 BYTE)                   NOT NULL,
  RECDATE   NUMBER(8),
  SPCODE    VARCHAR2(40 BYTE),
  SPNAME    VARCHAR2(40 BYTE),
  MRNO      VARCHAR2(40 BYTE),
  SEQ       NUMBER(10)                          NOT NULL,
  PARTNO    VARCHAR2(40 BYTE),
  PARTSPEC  VARCHAR2(40 BYTE),
  RECNUM    NUMBER(10),
  MOCODE    VARCHAR2(40 BYTE),
  WH        VARCHAR2(40 BYTE),
  WHLOC     VARCHAR2(40 BYTE),
  MDATE     NUMBER(8),
  MTIME     NUMBER(6),
  FACTORY   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE ITMATERIALRECNO
 ADD PRIMARY KEY
 (POCODE, SEQ);

CREATE TABLE ITMATERIALRECNOCK
(
  POCODE    VARCHAR2(40 BYTE)                   NOT NULL,
  RECDATE   NUMBER(8),
  SPCODE    VARCHAR2(40 BYTE),
  SPNAME    VARCHAR2(40 BYTE),
  MRNO      VARCHAR2(40 BYTE),
  SEQ       NUMBER(10)                          NOT NULL,
  PARTNO    VARCHAR2(40 BYTE),
  PARTSPEC  VARCHAR2(40 BYTE),
  RECNUM    NUMBER(10),
  MOCODE    VARCHAR2(40 BYTE),
  WH        VARCHAR2(40 BYTE),
  WHLOC     VARCHAR2(40 BYTE),
  MDATE     NUMBER(8),
  MTIME     NUMBER(6),
  FACTORY   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE ITMATERIALRECNOCK
 ADD PRIMARY KEY
 (POCODE, SEQ);

CREATE TABLE ITMO
(
  MOCODE           VARCHAR2(20 BYTE),
  ITEMCODE         VARCHAR2(30 BYTE),
  MOTYPE           VARCHAR2(12 BYTE),
  MOPLANQTY        NUMBER,
  MOPLANSTARTDATE  VARCHAR2(8 BYTE),
  MOPLANENDDATE    VARCHAR2(8 BYTE),
  CUSCODE          VARCHAR2(30 BYTE),
  CUSORDERNO       VARCHAR2(30 BYTE),
  MODESC           VARCHAR2(60 BYTE),
  FACTORY          VARCHAR2(12 BYTE),
  MDATE            VARCHAR2(8 BYTE),
  MTIME            VARCHAR2(8 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE ITMOFEEDBACK
(
  ID           NUMBER                           NOT NULL,
  MOCODE       VARCHAR2(20 BYTE)                NOT NULL,
  MOBITEMCODE  VARCHAR2(30 BYTE)                NOT NULL,
  QTY          NUMBER                           NOT NULL,
  WH           VARCHAR2(12 BYTE)                NOT NULL,
  WHLOC        VARCHAR2(12 BYTE)                NOT NULL,
  FBDATA       VARCHAR2(8 BYTE)                 NOT NULL,
  FACTORY      VARCHAR2(12 BYTE)                NOT NULL,
  UPDATEUSER   VARCHAR2(12 BYTE),
  YN           VARCHAR2(1 BYTE)                 NOT NULL,
  CREATETIME   DATE                             DEFAULT sysdate               NOT NULL,
  UPLOADTIME   DATE,
  ERROR_MEMO   VARCHAR2(255 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE ITMOISS
(
  ID           NUMBER(10)                       NOT NULL,
  MOCODE       VARCHAR2(40 BYTE),
  MOBITEMCODE  VARCHAR2(40 BYTE),
  QTY          NUMBER(10,6),
  WH           VARCHAR2(40 BYTE),
  LOC          VARCHAR2(40 BYTE),
  ISSDATE      NUMBER(8),
  FACTORY      VARCHAR2(40 BYTE),
  UPDATEUSER   VARCHAR2(40 BYTE),
  YN           VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE ITMOISS
 ADD PRIMARY KEY
 (ID);

CREATE TABLE ITPICKLIST
(
  PICKLISTNO       VARCHAR2(40 BYTE)            NOT NULL,
  MOCODE           VARCHAR2(40 BYTE)            NOT NULL,
  SEQ              NUMBER(10),
  ITEMCODE         VARCHAR2(40 BYTE),
  ITEMSPEC         VARCHAR2(40 BYTE),
  MOPLANQTY        NUMBER(10),
  MOPLANSTARTDATE  NUMBER(8),
  MOPLANENDDATE    NUMBER(8),
  PARTNO           VARCHAR2(40 BYTE),
  PARTDESC         VARCHAR2(40 BYTE),
  UOM              VARCHAR2(40 BYTE),
  WH               VARCHAR2(40 BYTE),
  WHLOC            VARCHAR2(40 BYTE),
  ALLOCQTY         NUMBER(10),
  ISSQTY           NUMBER(10),
  NOALLOCQTY       NUMBER(10),
  MDATE            NUMBER(8),
  MTIME            NUMBER(6),
  SUMNUM           NUMBER(10),
  FACTORY          VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE ITPICKLIST
 ADD PRIMARY KEY
 (PICKLISTNO, PARTNO);

CREATE TABLE ITPICKLISTCK
(
  PICKLISTNO       VARCHAR2(40 BYTE)            NOT NULL,
  MOCODE           VARCHAR2(40 BYTE)            NOT NULL,
  SEQ              NUMBER(10),
  ITEMCODE         VARCHAR2(40 BYTE),
  ITEMSPEC         VARCHAR2(40 BYTE),
  MOPLANQTY        NUMBER(10),
  MOPLANSTARTDATE  NUMBER(8),
  MOPLANENDDATE    NUMBER(8),
  PARTNO           VARCHAR2(40 BYTE),
  PARTDESC         VARCHAR2(40 BYTE),
  UOM              VARCHAR2(40 BYTE),
  WH               VARCHAR2(40 BYTE),
  WHLOC            VARCHAR2(40 BYTE),
  ALLOCQTY         NUMBER(10),
  ISSQTY           NUMBER(10),
  NOALLOCQTY       NUMBER(10),
  MDATE            NUMBER(8),
  MTIME            NUMBER(6),
  SUMNUM           NUMBER(10),
  FACTORY          VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE ITPICKLISTCK
 ADD PRIMARY KEY
 (PICKLISTNO, PARTNO);

CREATE TABLE ITPOREC
(
  ID          NUMBER(10)                        NOT NULL,
  MRNO        VARCHAR2(40 BYTE),
  SEQ         NUMBER(10),
  PARTNO      VARCHAR2(40 BYTE),
  RECDATE     NUMBER(8),
  DISPCD      VARCHAR2(40 BYTE),
  REASONCD    VARCHAR2(40 BYTE),
  RECNUM      NUMBER(10,6),
  QTY         NUMBER(10,6),
  WH          VARCHAR2(40 BYTE),
  LOC         VARCHAR2(40 BYTE),
  FACTORY     VARCHAR2(40 BYTE),
  UPDATEUSER  VARCHAR2(40 BYTE),
  YN          VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE ITPOREC
 ADD PRIMARY KEY
 (ID);

CREATE TABLE SCCDEPT
(
  ID      NUMBER(10)                            NOT NULL,
  ACTIVE  NVARCHAR2(1)                          DEFAULT ('Y')                 NOT NULL,
  NAME    NVARCHAR2(30)                         NOT NULL,
  "DESC"  NVARCHAR2(50),
  LMUSER  NVARCHAR2(30)                         NOT NULL,
  LMDATE  NUMBER(10)                            NOT NULL,
  LMTIME  NUMBER(10)                            NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCFGRP
(
  ID      NUMBER(10)                            NOT NULL,
  ACTIVE  NVARCHAR2(1)                          DEFAULT ('Y')                 NOT NULL,
  NAME    NVARCHAR2(30)                         NOT NULL,
  "DESC"  NVARCHAR2(50)                         NOT NULL,
  LMUSER  NVARCHAR2(30)                         NOT NULL,
  LMDATE  NUMBER(10)                            NOT NULL,
  LMTIME  NUMBER(10)                            NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCFPN
(
  FOID       NUMBER(10)                         NOT NULL,
  PLATFORM   NVARCHAR2(1)                       NOT NULL,
  FUNGROUP   NVARCHAR2(30)                      NOT NULL,
  FUNCTION   NVARCHAR2(50)                      NOT NULL,
  SEQ        NUMBER(10)                         NOT NULL,
  FORM       NVARCHAR2(50)                      NOT NULL,
  OPERATION  NVARCHAR2(30),
  LMUSER     NVARCHAR2(30)                      NOT NULL,
  LMDATE     NUMBER(10)                         NOT NULL,
  LMTIME     NUMBER(10)                         NOT NULL,
  PRODSYS    NVARCHAR2(30)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCFUNC
(
  ID        NUMBER(10)                          NOT NULL,
  ACTIVE    NVARCHAR2(1)                        DEFAULT ('Y')                 NOT NULL,
  GROUP_ID  NUMBER(10)                          NOT NULL,
  NAME      NVARCHAR2(30)                       NOT NULL,
  "DESC"    NVARCHAR2(50)                       NOT NULL,
  LMUSER    NVARCHAR2(30)                       NOT NULL,
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCLOG
(
  ID             NUMBER(10)                     NOT NULL,
  "USER"         NVARCHAR2(50)                  NOT NULL,
  PC             NVARCHAR2(50)                  NOT NULL,
  OS             NVARCHAR2(50)                  NOT NULL,
  SYSTEM         NVARCHAR2(50)                  NOT NULL,
  SYSTEMVERSION  NVARCHAR2(50)                  NOT NULL,
  FUNCTION       NVARCHAR2(50)                  NOT NULL,
  BEGINDATE      NUMBER(10)                     NOT NULL,
  BEGINTIME      NUMBER(10)                     NOT NULL,
  DURATION       NUMBER(10)                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCPRIVI
(
  USRNAME   NVARCHAR2(30)                       NOT NULL,
  FOID      NUMBER(10)                          NOT NULL,
  ROLENAME  NVARCHAR2(30)                       NOT NULL,
  LMUSER    NVARCHAR2(30)                       NOT NULL,
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCRFP
(
  ROLENAME  NVARCHAR2(30)                       NOT NULL,
  FOID      NUMBER(10)                          NOT NULL,
  LMUSER    NVARCHAR2(30)                       NOT NULL,
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCROLE
(
  ROLENAME  NVARCHAR2(30)                       NOT NULL,
  ROLEDESC  NVARCHAR2(50),
  LMUSER    NVARCHAR2(30)                       NOT NULL,
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCURL
(
  USERID    NVARCHAR2(30)                       NOT NULL,
  ROLENAME  NVARCHAR2(30)                       NOT NULL,
  LMUSER    NVARCHAR2(30)                       NOT NULL,
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCCUSR
(
  USERID       NVARCHAR2(30)                    NOT NULL,
  ACTIVE       NVARCHAR2(1)                     DEFAULT ('Y')                 NOT NULL,
  PASSWD       NVARCHAR2(50)                    NOT NULL,
  DEPT         NVARCHAR2(30),
  SUBDEPT      NVARCHAR2(30),
  EXT          NVARCHAR2(10),
  EMAIL        NVARCHAR2(30),
  CLPNO        NVARCHAR2(15),
  EMPID        NVARCHAR2(15),
  GENDER       NVARCHAR2(1)                     NOT NULL,
  CNAME        NVARCHAR2(60)                    NOT NULL,
  CAREER_TYPE  NVARCHAR2(15)                    NOT NULL,
  DEFAULTPC    NVARCHAR2(30),
  AUTH_TYPE    NVARCHAR2(15),
  LOGINDATE    NUMBER(10)                       NOT NULL,
  QUIT         NVARCHAR2(1)                     NOT NULL,
  LMUSER       NVARCHAR2(30)                    NOT NULL,
  LMDATE       NUMBER(10)                       NOT NULL,
  LMTIME       NUMBER(10)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SMTLINE
(
  LINEID    NUMBER(10)                          NOT NULL,
  LINENAME  NVARCHAR2(20)                       NOT NULL,
  LMUSER    NVARCHAR2(20),
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTLINE.LINEID IS 'Line??';

COMMENT ON COLUMN SMTLINE.LINENAME IS 'Line??';

COMMENT ON COLUMN SMTLINE.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTLINE.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTLINE.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTLINE
 ADD PRIMARY KEY
 (LINEID);

CREATE TABLE SMTMACH
(
  MACHID    NUMBER(10)                          NOT NULL,
  MACHNAME  NVARCHAR2(20)                       NOT NULL,
  LINENAME  NVARCHAR2(20)                       NOT NULL,
  SEQ       NUMBER(10)                          NOT NULL,
  BRAND     NVARCHAR2(20),
  LMUSER    NVARCHAR2(20),
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTMACH.MACHID IS 'Machine ID';

COMMENT ON COLUMN SMTMACH.MACHNAME IS '????';

COMMENT ON COLUMN SMTMACH.LINENAME IS 'Line??';

COMMENT ON COLUMN SMTMACH.SEQ IS 'Routing Seq of Line';

COMMENT ON COLUMN SMTMACH.BRAND IS 'Brand';

COMMENT ON COLUMN SMTMACH.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTMACH.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTMACH.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTMACH
 ADD PRIMARY KEY
 (MACHID);

CREATE TABLE SMTPCBA
(
  PCBAID     NUMBER(10)                         NOT NULL,
  PCBA       NVARCHAR2(100)                     NOT NULL,
  SO         NVARCHAR2(20)                      NOT NULL,
  PCBAMODEL  NVARCHAR2(100)                     NOT NULL,
  PCBAPN     NVARCHAR2(100)                     NOT NULL,
  LINENAME   NVARCHAR2(20)                      NOT NULL,
  CDATE      NUMBER(10)                         NOT NULL,
  CTIME      NUMBER(10)                         NOT NULL,
  LMUSER     NVARCHAR2(20),
  LMDATE     NUMBER(10)                         NOT NULL,
  LMTIME     NUMBER(10)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTPCBA.PCBAID IS 'LogID';

COMMENT ON COLUMN SMTPCBA.PCBA IS 'PCBA';

COMMENT ON COLUMN SMTPCBA.SO IS 'S/O no.';

COMMENT ON COLUMN SMTPCBA.PCBAMODEL IS 'Model';

COMMENT ON COLUMN SMTPCBA.PCBAPN IS 'Part No';

COMMENT ON COLUMN SMTPCBA.LINENAME IS 'Line??';

COMMENT ON COLUMN SMTPCBA.CDATE IS 'Create Date';

COMMENT ON COLUMN SMTPCBA.CTIME IS 'Create Time';

COMMENT ON COLUMN SMTPCBA.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTPCBA.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTPCBA.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTPCBA
 ADD PRIMARY KEY
 (PCBAID);

CREATE TABLE SMTPN
(
  PARTID      NUMBER(10)                        NOT NULL,
  PCBAMODEL   NVARCHAR2(100)                    NOT NULL,
  PCBAPN      NVARCHAR2(100)                    NOT NULL,
  CUSTOMERPN  NVARCHAR2(100)                    NOT NULL,
  ITEMDESC    NVARCHAR2(255),
  ACTIVE      NVARCHAR2(1)                      NOT NULL,
  LMUSER      NVARCHAR2(20),
  LMDATE      NUMBER(10)                        NOT NULL,
  LMTIME      NUMBER(10)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

COMMENT ON COLUMN SMTPN.PARTID IS 'Partno ID';

COMMENT ON COLUMN SMTPN.PCBAMODEL IS 'Model Name';

COMMENT ON COLUMN SMTPN.PCBAPN IS 'PCBA Part No';

COMMENT ON COLUMN SMTPN.CUSTOMERPN IS '??Part No';

COMMENT ON COLUMN SMTPN.ITEMDESC IS 'Item Description';

COMMENT ON COLUMN SMTPN.ACTIVE IS '?????Y/N)';

COMMENT ON COLUMN SMTPN.LMUSER IS 'Last Miantain user';

COMMENT ON COLUMN SMTPN.LMDATE IS 'Last Miantain Date';

COMMENT ON COLUMN SMTPN.LMTIME IS 'Last Miantain Time';

ALTER TABLE SMTPN
 ADD PRIMARY KEY
 (PARTID);

CREATE TABLE TBLALERTNOTIFIER
(
  ITEMCODE   VARCHAR2(40 BYTE)                  NOT NULL,
  ALERTTYPE  VARCHAR2(40 BYTE)                  NOT NULL,
  ALERTITEM  VARCHAR2(40 BYTE)                  NOT NULL,
  USERCODE   VARCHAR2(40 BYTE)                  NOT NULL,
  EMAIL      VARCHAR2(40 BYTE),
  BILLID     NUMBER(9)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLALERTNOTIFIER
 ADD PRIMARY KEY
 (USERCODE, BILLID);

CREATE TABLE TBLALERTRESBILL
(
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  ALERTTYPE      VARCHAR2(40 BYTE)              NOT NULL,
  ALERTITEM      VARCHAR2(40 BYTE)              NOT NULL,
  ALERTRES       VARCHAR2(40 BYTE)              NOT NULL,
  ALERTERECG2EC  VARCHAR2(100 BYTE)             NOT NULL,
  STARTNUM       NUMBER(10)                     NOT NULL,
  OP             VARCHAR2(40 BYTE)              NOT NULL,
  LOWVALUE       NUMBER(15,5)                   NOT NULL,
  UPVALUE        NUMBER(15,5)                   NOT NULL,
  VALIDDATE      NUMBER(8)                      NOT NULL,
  MAILNOTIFY     VARCHAR2(1 BYTE)               NOT NULL,
  ALERTMSG       VARCHAR2(1000 BYTE),
  ALERTDESC      VARCHAR2(1000 BYTE),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  BILLID         NUMBER(9),
  PRODUCTCODE    VARCHAR2(40 BYTE)              DEFAULT ''
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PKRES397 ON TBLALERTRESBILL
(ITEMCODE, ALERTTYPE, ALERTITEM, ALERTRES, ALERTERECG2EC)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTRESBILL
 ADD CONSTRAINT PKRES397
 PRIMARY KEY
 (ITEMCODE, ALERTTYPE, ALERTITEM, ALERTRES, ALERTERECG2EC);

CREATE TABLE TBLALERTSAMPLE
(
  ID          VARCHAR2(40 BYTE)                 NOT NULL,
  SAMPLEDESC  VARCHAR2(1000 BYTE),
  MDATE       NUMBER(8)                         NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK391 ON TBLALERTSAMPLE
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTSAMPLE
 ADD CONSTRAINT PK391
 PRIMARY KEY
 (ID);

CREATE TABLE TBLAP2ITEM
(
  APID         VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EARRTIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLAP2ITEM ON TBLAP2ITEM
(APID, ITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLAP2ITEM
 ADD CONSTRAINT PK_TBLAP2ITEM
 PRIMARY KEY
 (APID, ITEMCODE);

CREATE TABLE TBLAPSTATUSLIST
(
  OID          VARCHAR2(40 BYTE)                NOT NULL,
  APID         VARCHAR2(40 BYTE)                NOT NULL,
  BPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  OLDSTATUS    VARCHAR2(40 BYTE)                NOT NULL,
  MEMO         VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLAPSTATUSLIST ON TBLAPSTATUSLIST
(OID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLAPSTATUSLIST
 ADD CONSTRAINT PK_TBLAPSTATUSLIST
 PRIMARY KEY
 (OID);

CREATE TABLE TBLAPVCHANGELIST
(
  OID          VARCHAR2(40 BYTE)                NOT NULL,
  APID         VARCHAR2(40 BYTE)                NOT NULL,
  BPCODE       VARCHAR2(40 BYTE)                NOT NULL,
  VERSION      VARCHAR2(40 BYTE)                NOT NULL,
  OLDVERSION   VARCHAR2(40 BYTE)                NOT NULL,
  MEMO         VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLAPVCHANGELIST ON TBLAPVCHANGELIST
(OID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLAPVCHANGELIST
 ADD CONSTRAINT PK_TBLAPVCHANGELIST
 PRIMARY KEY
 (OID);

CREATE TABLE TBLARMORPLATE
(
  APID            VARCHAR2(40 BYTE)             NOT NULL,
  BPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  VERSION         VARCHAR2(40 BYTE)             NOT NULL,
  THICKNESS       NUMBER(15,5)                  NOT NULL,
  USEDTIMES       NUMBER(10)                    NOT NULL,
  STATUS          VARCHAR2(40 BYTE)             NOT NULL,
  MANUFACTURERSN  VARCHAR2(40 BYTE),
  LBRATE          NUMBER(10)                    NOT NULL,
  TENSIONA        NUMBER(15,5)                  NOT NULL,
  TENSIONB        NUMBER(15,5)                  NOT NULL,
  TENSIONC        NUMBER(15,5)                  NOT NULL,
  TENSIOND        NUMBER(15,5)                  NOT NULL,
  TENSIONE        NUMBER(15,5)                  NOT NULL,
  MEMO            VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE),
  INFACTORYDATE   NUMBER(8),
  INFACTORYTIME   NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLARMORPLATE ON TBLARMORPLATE
(APID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLARMORPLATE
 ADD CONSTRAINT PK_TBLARMORPLATE
 PRIMARY KEY
 (APID);

CREATE TABLE TBLARMORPLATECONTROL
(
  OID             VARCHAR2(40 BYTE)             NOT NULL,
  APID            VARCHAR2(40 BYTE)             NOT NULL,
  BPCODE          VARCHAR2(40 BYTE)             NOT NULL,
  VERSION         VARCHAR2(40 BYTE)             NOT NULL,
  THICKNESS       NUMBER(15,5)                  NOT NULL,
  USEDTIMES       NUMBER(10)                    NOT NULL,
  STATUS          VARCHAR2(40 BYTE)             NOT NULL,
  MANUFACTURERSN  VARCHAR2(40 BYTE),
  LBRATE          NUMBER(10)                    NOT NULL,
  TENSIONA        NUMBER(15,5)                  NOT NULL,
  TENSIONB        NUMBER(15,5)                  NOT NULL,
  TENSIONC        NUMBER(15,5)                  NOT NULL,
  TENSIOND        NUMBER(15,5)                  NOT NULL,
  TENSIONE        NUMBER(15,5)                  NOT NULL,
  USEDMOCODE      VARCHAR2(40 BYTE)             NOT NULL,
  USEDSSCODE      VARCHAR2(40 BYTE)             NOT NULL,
  UUSER           VARCHAR2(40 BYTE)             NOT NULL,
  UDATE           NUMBER(8)                     NOT NULL,
  UTIME           NUMBER(6)                     NOT NULL,
  USEDTIMESINMO   NUMBER(10)                    NOT NULL,
  RUSER           VARCHAR2(40 BYTE),
  RDATE           NUMBER(8),
  RTIME           NUMBER(6),
  MEMO            VARCHAR2(100 BYTE),
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL,
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLAPVSTATUSLIST ON TBLARMORPLATECONTROL
(OID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLARMORPLATECONTROL
 ADD CONSTRAINT PK_TBLAPVSTATUSLIST
 PRIMARY KEY
 (OID);

CREATE TABLE TBLASN
(
  STNO        VARCHAR2(40 BYTE)                 NOT NULL,
  ORGID       NUMBER(8)                         NOT NULL,
  VENDORCODE  VARCHAR2(40 BYTE)                 NOT NULL,
  STSTATUS    VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  SYNCSTATUS  VARCHAR2(40 BYTE),
  FLAG        VARCHAR2(40 BYTE)                 NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLASN
 ADD PRIMARY KEY
 (STNO);

CREATE TABLE TBLASNIQC
(
  IQCNO        VARCHAR2(50 BYTE)                NOT NULL,
  STNO         VARCHAR2(40 BYTE)                NOT NULL,
  STNO_SEQ     NUMBER(8)                        NOT NULL,
  STATUS       VARCHAR2(40 BYTE)                NOT NULL,
  INVUSER      VARCHAR2(100 BYTE),
  APPLICANT    VARCHAR2(100 BYTE),
  APPDATE      NUMBER(8)                        NOT NULL,
  APPTIME      NUMBER(6)                        NOT NULL,
  LOTNO        VARCHAR2(100 BYTE),
  INSPDATE     NUMBER(8),
  PRODDATE     NUMBER(8),
  STANDARD     VARCHAR2(100 BYTE),
  METHOD       VARCHAR2(100 BYTE),
  RESULT       VARCHAR2(100 BYTE),
  RECEIVEDATE  NUMBER(8),
  PIC          VARCHAR2(100 BYTE),
  INSPECTOR    VARCHAR2(100 BYTE),
  ROHS         VARCHAR2(15 BYTE),
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  ATTRIBUTE    VARCHAR2(40 BYTE),
  STS          VARCHAR2(15 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLASNIQC
 ADD PRIMARY KEY
 (IQCNO);

CREATE TABLE TBLATETESTINFO
(
  PKID         VARCHAR2(40 BYTE)                NOT NULL,
  RCARD        VARCHAR2(40 BYTE)                NOT NULL,
  TESTRESULT   VARCHAR2(40 BYTE)                NOT NULL,
  FAILCARD     VARCHAR2(255 BYTE),
  RESCODE      VARCHAR2(40 BYTE)                NOT NULL,
  LINECODE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_ATETESTINFO ON TBLATETESTINFO
(PKID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLATETESTINFO
 ADD CONSTRAINT PK_ATETESTINFO
 PRIMARY KEY
 (PKID);

CREATE TABLE TBLBARCODERULE
(
  MODELCODE    VARCHAR2(40 BYTE)                NOT NULL,
  AMODELCODE   VARCHAR2(40 BYTE)                NOT NULL,
  ADESC        VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK99_3 ON TBLBARCODERULE
(MODELCODE, AMODELCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLBARCODERULE
 ADD CONSTRAINT PK99_3
 PRIMARY KEY
 (MODELCODE, AMODELCODE);

CREATE TABLE TBLBSRPTVIEW
(
  USERCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  SEQ         NUMBER(10)                        NOT NULL,
  RPTCODE     VARCHAR2(40 BYTE),
  HEIGHT      NUMBER(15,5)                      NOT NULL,
  HEIGHTTYPE  VARCHAR2(40 BYTE),
  WIDTH       NUMBER(15,5)                      NOT NULL,
  WIDTHTYPE   VARCHAR2(40 BYTE),
  ISDEFAULT   VARCHAR2(1 BYTE)                  NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLBSRPTVIEW
 ADD PRIMARY KEY
 (USERCODE, SEQ);

CREATE TABLE TBLBURNVOLUMN
(
  PKID         VARCHAR2(40 BYTE)                NOT NULL,
  TOTAL        NUMBER(10)                       NOT NULL,
  USED         NUMBER(10)                       NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX BURNVOLUMN ON TBLBURNVOLUMN
(PKID)
LOGGING
NOPARALLEL;

ALTER TABLE TBLBURNVOLUMN
 ADD CONSTRAINT BURNVOLUMN
 PRIMARY KEY
 (PKID);

CREATE TABLE TBLCARTONINFO
(
  PKCARTONID   VARCHAR2(40 BYTE)                NOT NULL,
  CARTONNO     VARCHAR2(40 BYTE)                NOT NULL,
  CAPACITY     NUMBER(4)                        NOT NULL,
  COLLECTED    NUMBER(4)                        NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PKCARTONID ON TBLCARTONINFO
(PKCARTONID)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_CARTON_CARTONNO ON TBLCARTONINFO
(CARTONNO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLCARTONINFO
 ADD CONSTRAINT PKCARTONID
 PRIMARY KEY
 (PKCARTONID);

CREATE TABLE TBLCITEMCODECL
(
  ITEMCODE      VARCHAR2(40 BYTE)               NOT NULL,
  CUSCODE       VARCHAR2(40 BYTE)               NOT NULL,
  MODELCODE     VARCHAR2(40 BYTE)               NOT NULL,
  CUSMODELCODE  VARCHAR2(40 BYTE)               NOT NULL,
  CUSITEMCODE   VARCHAR2(40 BYTE)               NOT NULL,
  CHARCODE      VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX CUSITEMCODECHECKLIST ON TBLCITEMCODECL
(ITEMCODE, CUSCODE, MODELCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLCITEMCODECL
 ADD CONSTRAINT CUSITEMCODECHECKLIST
 PRIMARY KEY
 (ITEMCODE, CUSCODE, MODELCODE);

CREATE TABLE TBLCONFIGINFO
(
  RCARD           VARCHAR2(40 BYTE)             NOT NULL,
  CATERGORYCODE   VARCHAR2(40 BYTE)             NOT NULL,
  CHECKITEMCODE   VARCHAR2(40 BYTE)             NOT NULL,
  CHECKITEMVLAUE  VARCHAR2(40 BYTE),
  MUSER           VARCHAR2(40 BYTE),
  MDATE           NUMBER(8),
  MTIME           NUMBER(6),
  EATTRIBUTE1     VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK44_2_2_1 ON TBLCONFIGINFO
(RCARD, CATERGORYCODE, CHECKITEMCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLCONFIGINFO
 ADD CONSTRAINT PK44_2_2_1
 PRIMARY KEY
 (RCARD, CATERGORYCODE, CHECKITEMCODE);

CREATE TABLE TBLPLANWORKTIME
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  SSCODE       VARCHAR2(40 BYTE)                NOT NULL,
  CYCLETIME    NUMBER                           NOT NULL,
  WORKINGTIME  NUMBER                           NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLPLANWORKTIME
 ADD PRIMARY KEY
 (ITEMCODE, SSCODE);

CREATE TABLE TBLLINE2CREW
(
  SHIFTDATE  NUMBER(8)                          NOT NULL,
  SSCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  SHIFTCODE  VARCHAR2(40 BYTE)                  NOT NULL,
  CREWCODE   VARCHAR2(40 BYTE)                  NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLLINE2CREW
 ADD PRIMARY KEY
 (SHIFTDATE, SSCODE, SHIFTCODE);

CREATE TABLE TBLLINE2MANDETAIL
(
  SERIAL     NUMBER                             NOT NULL,
  USERCODE   VARCHAR2(40 BYTE)                  NOT NULL,
  SSCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  OPCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  RESCODE    VARCHAR2(40 BYTE)                  NOT NULL,
  STATUS     VARCHAR2(40 BYTE)                  NOT NULL,
  SHIFTDATE  NUMBER(8)                          NOT NULL,
  SHIFTCODE  VARCHAR2(40 BYTE)                  NOT NULL,
  ONDATE     NUMBER(8)                          NOT NULL,
  ONTIME     NUMBER(6)                          NOT NULL,
  OFFDATE    NUMBER(8)                          NOT NULL,
  OFFTIME    NUMBER(6)                          NOT NULL,
  MOCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  DURATION   NUMBER(10)                         NOT NULL,
  MANACTQTY  NUMBER(10,2)                       NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLLINE2MANDETAIL1 ON TBLLINE2MANDETAIL
(USERCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLLINE2MANDETAIL
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLNOTICELINEPAUSE
(
  SERIAL           NUMBER                       NOT NULL,
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  SSCODE           VARCHAR2(40 BYTE)            NOT NULL,
  OPCODE           VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTDAY         NUMBER(8)                    NOT NULL,
  ONWIPSERIAL      NUMBER                       NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLNOTICELINEPAUSE1 ON TBLNOTICELINEPAUSE
(ITEMSEQUENCE, SUBITEMSEQUENCE, SHIFTDAY, ONWIPSERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLNOTICELINEPAUSE
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLALERTERROR
(
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  ITEMTYPE         VARCHAR2(40 BYTE)            NOT NULL,
  ECODE            VARCHAR2(40 BYTE)            NOT NULL,
  TIMEDIMENSION    VARCHAR2(40 BYTE)            NOT NULL,
  LINEDIVISION     VARCHAR2(1 BYTE)             NOT NULL,
  ALERTVALUE       NUMBER(8)                    NOT NULL,
  GENERATENOTICE   VARCHAR2(1 BYTE)             NOT NULL,
  SENDMAIL         VARCHAR2(1 BYTE)             NOT NULL,
  LINEPAUSE        VARCHAR2(1 BYTE)             NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLALERTERROR1 ON TBLALERTERROR
(ITEMSEQUENCE, SUBITEMSEQUENCE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTERROR
 ADD PRIMARY KEY
 (SUBITEMSEQUENCE);

CREATE TABLE TBLNOTICEERROR
(
  SERIAL           NUMBER                       NOT NULL,
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  ECODE            VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTCODE        VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTDAY         NUMBER(8)                    NOT NULL,
  BIGSSCODE        VARCHAR2(40 BYTE)            NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLNOTICEERROR1 ON TBLNOTICEERROR
(ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECODE, SHIFTCODE, 
SHIFTDAY, BIGSSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLNOTICEERROR
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLALERTERRORCODE
(
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  ITEMTYPE         VARCHAR2(40 BYTE)            NOT NULL,
  ECSCODE          VARCHAR2(40 BYTE)            NOT NULL,
  TIMEDIMENSION    VARCHAR2(40 BYTE)            NOT NULL,
  LINEDIVISION     VARCHAR2(1 BYTE)             NOT NULL,
  ALERTVALUE       NUMBER                       NOT NULL,
  GENERATENOTICE   VARCHAR2(1 BYTE)             NOT NULL,
  SENDMAIL         VARCHAR2(1 BYTE)             NOT NULL,
  LINEPAUSE        VARCHAR2(1 BYTE)             NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLALERTERRORCODE1 ON TBLALERTERRORCODE
(ITEMSEQUENCE, SUBITEMSEQUENCE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTERRORCODE
 ADD PRIMARY KEY
 (SUBITEMSEQUENCE);

CREATE TABLE TBLNOTICEERRORCODE
(
  SERIAL           NUMBER                       NOT NULL,
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  ECSCODE          VARCHAR2(40 BYTE)            NOT NULL,
  LOCATION         VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTCODE        VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTDAY         NUMBER(8)                    NOT NULL,
  BIGSSCODE        VARCHAR2(40 BYTE)            NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLNOTICEERRORCODE1 ON TBLNOTICEERRORCODE
(ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECSCODE, LOCATION, 
SHIFTCODE, SHIFTDAY, BIGSSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLNOTICEERRORCODE
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLALERTOQCNG
(
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  ITEMTYPE         VARCHAR2(40 BYTE)            NOT NULL,
  ECODE            VARCHAR2(40 BYTE)            NOT NULL,
  STARTDATE        NUMBER(8)                    NOT NULL,
  STARTTIME        NUMBER(8)                    NOT NULL,
  ALERTVALUE       NUMBER                       NOT NULL,
  GENERATENOTICE   VARCHAR2(1 BYTE)             NOT NULL,
  SENDMAIL         VARCHAR2(1 BYTE)             NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLALERTOQCNG1 ON TBLALERTOQCNG
(ITEMSEQUENCE, SUBITEMSEQUENCE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTOQCNG
 ADD PRIMARY KEY
 (SUBITEMSEQUENCE);

CREATE TABLE TBLALERTDIRECTPASS
(
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  ITEMTYPE         VARCHAR2(40 BYTE)            NOT NULL,
  BASEOUTPUT       NUMBER                       NOT NULL,
  TIMEDIMENSION    VARCHAR2(40 BYTE)            NOT NULL,
  ALERTVALUE       NUMBER                       NOT NULL,
  GENERATENOTICE   VARCHAR2(1 BYTE)             NOT NULL,
  SENDMAIL         VARCHAR2(1 BYTE)             NOT NULL,
  LINEPAUSE        VARCHAR2(1 BYTE)             NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLALERTDIRECTPASS1 ON TBLALERTDIRECTPASS
(ITEMSEQUENCE, SUBITEMSEQUENCE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTDIRECTPASS
 ADD PRIMARY KEY
 (SUBITEMSEQUENCE);

CREATE TABLE TBLNOTICEDIRECTPASS
(
  SERIAL           NUMBER                       NOT NULL,
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTCODE        VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTDAY         NUMBER(8)                    NOT NULL,
  BIGSSCODE        VARCHAR2(40 BYTE)            NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLNOTICEDIRECTPASS1 ON TBLNOTICEDIRECTPASS
(ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, SHIFTCODE, SHIFTDAY, 
BIGSSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLNOTICEDIRECTPASS
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLALERTLINEPAUSE
(
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  SSCODE           VARCHAR2(40 BYTE)            NOT NULL,
  OPCODE           VARCHAR2(40 BYTE)            NOT NULL,
  ALERTVALUE       NUMBER                       NOT NULL,
  GENERATENOTICE   VARCHAR2(1 BYTE)             NOT NULL,
  SENDMAIL         VARCHAR2(1 BYTE)             NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLALERTLINEPAUSE1 ON TBLALERTLINEPAUSE
(ITEMSEQUENCE, SUBITEMSEQUENCE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTLINEPAUSE
 ADD PRIMARY KEY
 (SUBITEMSEQUENCE);

CREATE TABLE TBLALERTMAILSETTING
(
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  SERIAL           NUMBER                       NOT NULL,
  BIGSSCODE        VARCHAR2(40 BYTE),
  ITEMFIRSTCLASS   VARCHAR2(40 BYTE),
  ITEMSECONDCLASS  VARCHAR2(40 BYTE),
  RECIPIENTS       VARCHAR2(2000 BYTE)          NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLALERTMAILSETTING1 ON TBLALERTMAILSETTING
(ITEMSEQUENCE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLALERTMAILSETTING
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE T1
(
  A1  VARCHAR2(10 BYTE),
  A2  TIMESTAMP(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE T_DATE
(
  C1  NUMBER,
  C2  DATE
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE PARTT
(
  LOGID      NUMBER                             NOT NULL,
  NAME       VARCHAR2(100 BYTE),
  LASTERDDL  DATE
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLMATERIALTRANS
(
  SERIAL           NUMBER(38)                   NOT NULL,
  FRMATERIALLOT    VARCHAR2(40 BYTE),
  FRMITEMCODE      VARCHAR2(40 BYTE),
  FRMSTORAGEID     VARCHAR2(30 BYTE),
  TOMATERIALLOT    VARCHAR2(40 BYTE),
  TOITEMCODE       VARCHAR2(40 BYTE),
  TOSTORAGEID      VARCHAR2(30 BYTE),
  TRANSQTY         NUMBER(22,13)                NOT NULL,
  MEMO             VARCHAR2(1000 BYTE),
  UNIT             VARCHAR2(40 BYTE),
  VENDORCODE       VARCHAR2(40 BYTE),
  ISSUETYPE        VARCHAR2(40 BYTE)            NOT NULL,
  TRANSACTIONCODE  VARCHAR2(100 BYTE)           NOT NULL,
  BUSINESSCODE     VARCHAR2(40 BYTE)            NOT NULL,
  ORGID            NUMBER(22,8)                 NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLMATERIALTRANS ON TBLMATERIALTRANS
(SERIAL)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMATERIALTRANS
 ADD CONSTRAINT PK_TBLMATERIALTRANS
 PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLMATERIALBUSINESS
(
  BUSINESSCODE  VARCHAR2(40 BYTE)               NOT NULL,
  BUSINESSDESC  VARCHAR2(100 BYTE)              NOT NULL,
  BUSINESSTYPE  VARCHAR2(40 BYTE)               NOT NULL,
  SAPCODE       VARCHAR2(40 BYTE),
  ORGID         NUMBER(22)                      NOT NULL,
  ISFIFO        VARCHAR2(40 BYTE),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLMATERIALBUSINESS ON TBLMATERIALBUSINESS
(BUSINESSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLMATERIALBUSINESS
 ADD CONSTRAINT PK_TBLMATERIALBUSINESS
 PRIMARY KEY
 (BUSINESSCODE);

CREATE TABLE EPSMAILTYPE
(
  ID           NUMBER                           NOT NULL,
  TYPENAME     VARCHAR2(300 BYTE)               DEFAULT ' '                   NOT NULL,
  ACTIVE       NUMBER(1)                        DEFAULT 1                     NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT sysdate               NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_MAILTYPE ON EPSMAILTYPE
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE EPSMAILTYPE
 ADD CONSTRAINT PK_MAILTYPE
 PRIMARY KEY
 (ID);

CREATE TABLE WSCELEMENT
(
  ID           NUMBER                           NOT NULL,
  ELEMENTNO    VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  DESCRIPTION  VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT SYSDATE               NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCELEMENT ON WSCELEMENT
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCELEMENT
 ADD CONSTRAINT PK_WSCELEMENT
 PRIMARY KEY
 (ID);

CREATE TABLE WSCGROUP
(
  ID           NUMBER                           NOT NULL,
  NAME         VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  DESCRIPTION  VARCHAR2(600 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT SYSDATE               NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCGROUP ON WSCGROUP
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCGROUP
 ADD CONSTRAINT PK_WSCGROUP
 PRIMARY KEY
 (ID);

CREATE TABLE WSCLANGUAGE
(
  ID                   NUMBER                   NOT NULL,
  LANGUAGENAME         VARCHAR2(150 BYTE)       DEFAULT (' ')                 NOT NULL,
  LANGUAGEDESCRIPTION  VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ACTIVE               NUMBER(1)                DEFAULT (1)                   NOT NULL,
  DEFAULTLANGUAGE      NUMBER(1)                DEFAULT (0)                   NOT NULL,
  CREATETIME           DATE                     DEFAULT (SYSDATE)             NOT NULL,
  CREATEUSER           VARCHAR2(150 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE1           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE2           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE3           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE4           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE5           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE6           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE7           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE8           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE9           VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE10          VARCHAR2(750 BYTE)       DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE11          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE12          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE13          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE14          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE15          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE16          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE17          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE18          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE19          VARCHAR2(4000 BYTE)      DEFAULT (' ')                 NOT NULL,
  ATTRIBUTE20          VARCHAR2(4000 BYTE)      DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCLANGUAGE ON WSCLANGUAGE
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCLANGUAGE
 ADD CONSTRAINT PK_WSCLANGUAGE
 PRIMARY KEY
 (ID);

CREATE TABLE WSCMENU
(
  ID           NUMBER                           NOT NULL,
  PARENTID     NUMBER                           DEFAULT 0                     NOT NULL,
  URL          VARCHAR2(1500 BYTE)              DEFAULT ' '                   NOT NULL,
  SORT         NUMBER                           DEFAULT 0                     NOT NULL,
  PICTURE      VARCHAR2(1500 BYTE)              DEFAULT ' '                   NOT NULL,
  SHOWPIC      NUMBER(1)                        DEFAULT 0                     NOT NULL,
  MENUTYPE     VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT sysdate               NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCMENU ON WSCMENU
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCMENU
 ADD CONSTRAINT PK_WSCMENU
 PRIMARY KEY
 (ID);

CREATE TABLE WSCPARAMETERTYPE
(
  ID           NUMBER                           NOT NULL,
  PTNAME       VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  DESCRIPTION  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT sysdate               NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE1   VARCHAR2(1500 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(1500 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCPARAMETERTYPE ON WSCPARAMETERTYPE
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCPARAMETERTYPE
 ADD CONSTRAINT PK_WSCPARAMETERTYPE
 PRIMARY KEY
 (ID);

CREATE TABLE V$RESERVED_WORDS
(
  KEYWORD  VARCHAR2(64 BYTE),
  LENGTH   NUMBER
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLLOSTMANHOUR
(
  SSCODE          VARCHAR2(40 BYTE)             NOT NULL,
  SHIFTDATE       NUMBER(8)                     NOT NULL,
  SHIFTCODE       VARCHAR2(40 BYTE)             NOT NULL,
  ITEMCODE        VARCHAR2(40 BYTE)             NOT NULL,
  ACTMANHOUR      NUMBER(10)                    NOT NULL,
  ACTOUTPUT       NUMBER(10)                    NOT NULL,
  ACQUIREMANHOUR  NUMBER(10)                    NOT NULL,
  LOSTMANHOUR     NUMBER(10)                    NOT NULL,
  MUSER           VARCHAR2(40 BYTE)             NOT NULL,
  MDATE           NUMBER(8)                     NOT NULL,
  MTIME           NUMBER(6)                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLLOSTMANHOUR
 ADD PRIMARY KEY
 (SSCODE, SHIFTDATE, SHIFTCODE, ITEMCODE);

CREATE TABLE TBLSAPMATERIALTRANS
(
  MATERIALLOT      VARCHAR2(40 BYTE)            NOT NULL,
  POSTSEQ          NUMBER(8)                    NOT NULL,
  ORGID            NUMBER(8)                    NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  ACCOUNTDATE      NUMBER(8),
  VOUCHERDATE      NUMBER(8),
  FRMSTORAGEID     VARCHAR2(4 BYTE)             NOT NULL,
  TOSTORAGEID      VARCHAR2(4 BYTE)             NOT NULL,
  TRANSQTY         NUMBER(13)                   NOT NULL,
  RECEIVEMEMO      VARCHAR2(1000 BYTE),
  UNIT             VARCHAR2(40 BYTE)            NOT NULL,
  VENDORCODE       VARCHAR2(40 BYTE),
  MOCODE           VARCHAR2(40 BYTE),
  FLAG             VARCHAR2(10 BYTE)            NOT NULL,
  TRANSACTIONCODE  VARCHAR2(100 BYTE)           NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  TOITEMCODE       VARCHAR2(40 BYTE),
  SAPCODE          VARCHAR2(40 BYTE)            DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLSAPMATERIALTRANS
 ADD PRIMARY KEY
 (MATERIALLOT, POSTSEQ);

CREATE TABLE "Sheet1$"
(
  A   VARCHAR2(255 BYTE),
  A1  VARCHAR2(255 BYTE),
  A2  VARCHAR2(255 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLPRODDETAIL
(
  SERIAL     NUMBER                             NOT NULL,
  SSCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  SHIFTDATE  NUMBER(8)                          NOT NULL,
  SHIFTCODE  VARCHAR2(40 BYTE)                  NOT NULL,
  MOCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  MANCOUNT   NUMBER(10)                         NOT NULL,
  BEGINDATE  NUMBER(8)                          NOT NULL,
  BEGINTIME  NUMBER(6)                          NOT NULL,
  ENDDATE    NUMBER(8)                          NOT NULL,
  ENDTIME    NUMBER(6)                          NOT NULL,
  DURATION   NUMBER(10)                         NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL,
  STATUS     VARCHAR2(40 BYTE)                  DEFAULT 'CLOSE'               NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLPRODDETAIL
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLLINEPAUSE
(
  SERIAL     NUMBER                             NOT NULL,
  SSCODE     VARCHAR2(40 BYTE)                  NOT NULL,
  MANCOUNT   NUMBER(10)                         NOT NULL,
  BEGINDATE  NUMBER(8)                          NOT NULL,
  BEGINTIME  NUMBER(6)                          NOT NULL,
  ENDDATE    NUMBER(8)                          NOT NULL,
  ENDTIME    NUMBER(6)                          NOT NULL,
  DURATION   NUMBER(10)                         NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX IDX_TBLLINEPAUSE1 ON TBLLINEPAUSE
(SSCODE)
LOGGING
NOPARALLEL;

ALTER TABLE TBLLINEPAUSE
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLEXCEPTIONCODE
(
  EXCEPTIONCODE  VARCHAR2(40 BYTE)              NOT NULL,
  EXCEPTIONNAME  VARCHAR2(40 BYTE)              NOT NULL,
  EXCEPTIONDESC  VARCHAR2(200 BYTE)             NOT NULL,
  EXCEPTIONTYPE  VARCHAR2(40 BYTE)              NOT NULL,
  EXCEPTIONFLAG  VARCHAR2(40 BYTE)              NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLEXCEPTIONCODE
 ADD PRIMARY KEY
 (EXCEPTIONCODE);

CREATE TABLE TBLDNTEMPOUT
(
  STACKCODE  VARCHAR2(40 BYTE)                  NOT NULL,
  ITEMCODE   VARCHAR2(40 BYTE)                  NOT NULL,
  DNNO       VARCHAR2(40 BYTE)                  NOT NULL,
  DNLINE     VARCHAR2(6 BYTE)                   NOT NULL,
  TEMPQTY    NUMBER(10)                         NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER(8)                          NOT NULL,
  MTIME      NUMBER(6)                          NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLDNTEMPOUT
 ADD PRIMARY KEY
 (STACKCODE, ITEMCODE, DNNO, DNLINE);

CREATE TABLE TBLUSERSCHEDULERJOB
(
  JOB_NAME              VARCHAR2(30 BYTE)       NOT NULL,
  JOB_SUBNAME           VARCHAR2(30 BYTE),
  JOB_CREATOR           VARCHAR2(30 BYTE),
  CLIENT_ID             VARCHAR2(64 BYTE),
  GLOBAL_UID            VARCHAR2(32 BYTE),
  PROGRAM_OWNER         VARCHAR2(4000 BYTE),
  PROGRAM_NAME          VARCHAR2(4000 BYTE),
  JOB_TYPE              VARCHAR2(16 BYTE),
  JOB_ACTION            VARCHAR2(4000 BYTE),
  NUMBER_OF_ARGUMENTS   NUMBER,
  SCHEDULE_OWNER        VARCHAR2(4000 BYTE),
  SCHEDULE_NAME         VARCHAR2(4000 BYTE),
  SCHEDULE_TYPE         VARCHAR2(12 BYTE),
  START_DATE            TIMESTAMP(6) WITH TIME ZONE,
  REPEAT_INTERVAL       VARCHAR2(4000 BYTE),
  EVENT_QUEUE_OWNER     VARCHAR2(30 BYTE),
  EVENT_QUEUE_NAME      VARCHAR2(30 BYTE),
  EVENT_QUEUE_AGENT     VARCHAR2(30 BYTE),
  EVENT_CONDITION       VARCHAR2(4000 BYTE),
  EVENT_RULE            VARCHAR2(65 BYTE),
  END_DATE              TIMESTAMP(6) WITH TIME ZONE,
  JOB_CLASS             VARCHAR2(30 BYTE),
  ENABLED               VARCHAR2(5 BYTE),
  AUTO_DROP             VARCHAR2(5 BYTE),
  RESTARTABLE           VARCHAR2(5 BYTE),
  STATE                 VARCHAR2(15 BYTE),
  JOB_PRIORITY          NUMBER,
  RUN_COUNT             NUMBER,
  MAX_RUNS              NUMBER,
  FAILURE_COUNT         NUMBER,
  MAX_FAILURES          NUMBER,
  RETRY_COUNT           NUMBER,
  LAST_START_DATE       TIMESTAMP(6) WITH TIME ZONE,
  LAST_RUN_DURATION     INTERVAL DAY(9) TO SECOND(6),
  NEXT_RUN_DATE         TIMESTAMP(6) WITH TIME ZONE,
  SCHEDULE_LIMIT        INTERVAL DAY(3) TO SECOND(0),
  MAX_RUN_DURATION      INTERVAL DAY(3) TO SECOND(0),
  LOGGING_LEVEL         VARCHAR2(4 BYTE),
  STOP_ON_WINDOW_CLOSE  VARCHAR2(5 BYTE),
  INSTANCE_STICKINESS   VARCHAR2(5 BYTE),
  RAISE_EVENTS          VARCHAR2(4000 BYTE),
  SYSTEM                VARCHAR2(5 BYTE),
  JOB_WEIGHT            NUMBER,
  NLS_ENV               VARCHAR2(4000 BYTE),
  SOURCE                VARCHAR2(128 BYTE),
  DESTINATION           VARCHAR2(128 BYTE),
  COMMENTS              VARCHAR2(240 BYTE),
  FLAGS                 NUMBER
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SMTCUSTOMER
(
  ID            NUMBER(10)                      NOT NULL,
  ACTIVE        VARCHAR2(1 BYTE)                DEFAULT ('Y')                 NOT NULL,
  CUSTOMERNAME  VARCHAR2(30 BYTE)               NOT NULL,
  CUSTOMERCODE  VARCHAR2(30 BYTE)               NOT NULL,
  LMUSER        VARCHAR2(30 BYTE),
  LMDATE        NUMBER(10)                      NOT NULL,
  LMTIME        NUMBER(10)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SMTCUSTOMERBARCODEPARSE
(
  CUSTOMERCODE          VARCHAR2(30 BYTE)       NOT NULL,
  ITEMSOURCE            VARCHAR2(30 BYTE)       NOT NULL,
  ITEMFROM              NUMBER(10)              NOT NULL,
  ITEMTO                NUMBER(10)              NOT NULL,
  ITEMDEALERRORWAY      VARCHAR2(30 BYTE)       NOT NULL,
  QUANTITYSOURCE        VARCHAR2(30 BYTE)       NOT NULL,
  QUANTITYFROM          NUMBER(10)              NOT NULL,
  QUANTITYTO            NUMBER(10)              NOT NULL,
  QUANTITYDEALERRORWAY  VARCHAR2(30 BYTE)       NOT NULL,
  LMUSER                VARCHAR2(30 BYTE),
  LMDATE                NUMBER(10)              NOT NULL,
  LMTIME                NUMBER(10)              NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SMTOWNBARCODEPARSE
(
  CUSTOMERCODE          VARCHAR2(30 BYTE)       NOT NULL,
  ITEMSOURCE            VARCHAR2(30 BYTE)       NOT NULL,
  ITEMFROM              NUMBER(10)              NOT NULL,
  ITEMTO                NUMBER(10)              NOT NULL,
  ITEMDEALERRORWAY      VARCHAR2(30 BYTE)       NOT NULL,
  QUANTITYSOURCE        VARCHAR2(30 BYTE)       NOT NULL,
  QUANTITYFROM          NUMBER(10)              NOT NULL,
  QUANTITYTO            NUMBER(10)              NOT NULL,
  QUANTITYDEALERRORWAY  VARCHAR2(30 BYTE)       NOT NULL,
  LMUSER                VARCHAR2(30 BYTE),
  LMDATE                NUMBER(10)              NOT NULL,
  LMTIME                NUMBER(10)              NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SMTLINESWITCH
(
  LINEID        NUMBER(10)                      NOT NULL,
  UNIQUEREELID  NVARCHAR2(20)                   NOT NULL,
  LMUSER        NVARCHAR2(20),
  LMDATE        NUMBER(10)                      NOT NULL,
  LMTIME        NUMBER(10)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE SMTLINESWITCH
 ADD PRIMARY KEY
 (LINEID);

CREATE TABLE SMTLINESWITCHHIS
(
  LINEID        NUMBER(10)                      NOT NULL,
  UNIQUEREELID  NVARCHAR2(20)                   NOT NULL,
  LMUSER        NVARCHAR2(20),
  LMDATE        NUMBER(10)                      NOT NULL,
  LMTIME        NUMBER(10)                      NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SMTREELNURID
(
  REELLOT   NVARCHAR2(100)                      NOT NULL,
  PARTNO    NVARCHAR2(100)                      NOT NULL,
  SO        NVARCHAR2(20)                       NOT NULL,
  QTY       NUMBER(22)                          NOT NULL,
  FQTY      NUMBER(22)                          NOT NULL,
  STATUS    NVARCHAR2(1)                        NOT NULL,
  LMUSER    NVARCHAR2(20),
  LMDATE    NUMBER(10)                          NOT NULL,
  LMTIME    NUMBER(10)                          NOT NULL,
  REELTYPE  VARCHAR2(30 BYTE),
  PARSEWAY  VARCHAR2(30 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLALERTNOTICE
(
  SERIAL           NUMBER                       NOT NULL,
  ITEMSEQUENCE     VARCHAR2(40 BYTE)            NOT NULL,
  DESCRIPTION      VARCHAR2(100 BYTE),
  SUBITEMSEQUENCE  VARCHAR2(40 BYTE)            NOT NULL,
  NOTICESERIAL     NUMBER                       NOT NULL,
  ALERTTYPE        VARCHAR2(40 BYTE)            NOT NULL,
  STATUS           VARCHAR2(40 BYTE)            NOT NULL,
  MOLIST           VARCHAR2(2000 BYTE),
  NOTICECONTENT    VARCHAR2(4000 BYTE)          NOT NULL,
  ANALYSISREASON   VARCHAR2(2000 BYTE),
  DEALMETHODS      VARCHAR2(2000 BYTE),
  NOTICEDATE       NUMBER(8)                    NOT NULL,
  NOTICETIME       NUMBER(6)                    NOT NULL,
  DEALUSER         VARCHAR2(40 BYTE),
  DEALDATE         NUMBER(8),
  DEALTIME         NUMBER(6),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLALERTNOTICE
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLJOBLOG
(
  JOBID          VARCHAR2(40 BYTE)              NOT NULL,
  STARTDATETIME  DATE                           NOT NULL,
  ENDDATETIME    DATE                           NOT NULL,
  USEDTIME       NUMBER(22)                     NOT NULL,
  PROCESSCOUNT   NUMBER(22)                     NOT NULL,
  RESULT         VARCHAR2(40 BYTE)              NOT NULL,
  ERRORMSG       VARCHAR2(500 BYTE),
  SERIAL         NUMBER(38)                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLJOBLOG
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLPALLET2RCARDLOG
(
  SERIAL      NUMBER(38)                        NOT NULL,
  PALLETCODE  VARCHAR2(40 BYTE)                 NOT NULL,
  RCARD       VARCHAR2(40 BYTE)                 NOT NULL,
  PACKUSER    VARCHAR2(40 BYTE),
  PACKDATE    NUMBER(8),
  PACKTIME    NUMBER(6),
  REMOVEUSER  VARCHAR2(40 BYTE),
  REMOVDATE   NUMBER(8),
  REMOVTIME   NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLPALLET2RCARDLOG ON TBLPALLET2RCARDLOG
(PALLETCODE, RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLPALLET2RCARDLOG
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLCARTON2RCARDLOG
(
  SERIAL      NUMBER(38)                        NOT NULL,
  CARTONNO    VARCHAR2(40 BYTE)                 NOT NULL,
  RCARD       VARCHAR2(40 BYTE)                 NOT NULL,
  PACKUSER    VARCHAR2(40 BYTE),
  PACKDATE    NUMBER(8),
  PACKTIME    NUMBER(6),
  REMOVEUSER  VARCHAR2(40 BYTE),
  REMOVDATE   NUMBER(8),
  REMOVTIME   NUMBER(6)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE INDEX INDEX_TBLCARTON2RCARDLOG ON TBLCARTON2RCARDLOG
(CARTONNO, RCARD)
LOGGING
NOPARALLEL;

ALTER TABLE TBLCARTON2RCARDLOG
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLMATERIALREQSTD
(
  ITEMCODE     VARCHAR2(40 BYTE)                NOT NULL,
  ORGID        NUMBER(8)                        NOT NULL,
  REQUESTQTY   NUMBER(13),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        VARCHAR2(22 BYTE)                NOT NULL,
  MTIME        VARCHAR2(22 BYTE)                NOT NULL,
  EATTRIBUTE1  VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMATERIALREQSTD
 ADD PRIMARY KEY
 (ITEMCODE, ORGID);

CREATE TABLE TBLWORKPLAN
(
  BIGSSCODE        VARCHAR2(40 BYTE)            NOT NULL,
  PLANDATE         NUMBER(8)                    NOT NULL,
  MOCODE           VARCHAR2(40 BYTE)            NOT NULL,
  MOSEQ            NUMBER(10)                   NOT NULL,
  PLANSEQ          NUMBER(10)                   NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  PLANQTY          NUMBER(10,2)                 NOT NULL,
  ACTQTY           NUMBER(10,2)                 NOT NULL,
  MATERIALQTY      NUMBER(10,2)                 NOT NULL,
  PLANSTARTTIME    NUMBER(6)                    NOT NULL,
  PLANENDTIME      NUMBER(6),
  LASTRECEIVETIME  NUMBER(6),
  LASTREQTIME      NUMBER(6),
  PROMISETIME      NUMBER(6),
  ACTIONSTATUS     VARCHAR2(40 BYTE)            NOT NULL,
  MATERIALSTATUS   VARCHAR2(40 BYTE)            NOT NULL,
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL,
  EATTRIBUTE1      VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX SYS_2009031811 ON TBLWORKPLAN
(PLANDATE, BIGSSCODE, PLANSEQ)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX SYS_2009031810 ON TBLWORKPLAN
(BIGSSCODE, PLANDATE, MOCODE, MOSEQ)
LOGGING
NOPARALLEL;

ALTER TABLE TBLWORKPLAN
 ADD CONSTRAINT SYS_2009031810
 PRIMARY KEY
 (BIGSSCODE, PLANDATE, MOCODE, MOSEQ);

CREATE TABLE TBLALERTITEM
(
  ITEMSEQUENCE  VARCHAR2(40 BYTE)               NOT NULL,
  DESCRIPTION   VARCHAR2(100 BYTE)              NOT NULL,
  ALERTTYPE     VARCHAR2(40 BYTE)               NOT NULL,
  MAILSUBJECT   VARCHAR2(150 BYTE)              NOT NULL,
  MAILCONTENT   VARCHAR2(2000 BYTE)             NOT NULL,
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLALERTITEM
 ADD PRIMARY KEY
 (ITEMSEQUENCE);

CREATE TABLE TBLMAIL
(
  SERIAL        NUMBER                          NOT NULL,
  MAILSUBJECT   VARCHAR2(150 BYTE)              NOT NULL,
  RECIPIENTS    VARCHAR2(4000 BYTE)             NOT NULL,
  MAILCONTENT   VARCHAR2(4000 BYTE)             NOT NULL,
  ISSEND        VARCHAR2(1 BYTE)                NOT NULL,
  SENDTIMES     NUMBER                          NOT NULL,
  SENDRESULT    VARCHAR2(40 BYTE)               NOT NULL,
  ERRORMESSAGE  VARCHAR2(2000 BYTE),
  MUSER         VARCHAR2(40 BYTE)               NOT NULL,
  MDATE         NUMBER(8)                       NOT NULL,
  MTIME         NUMBER(6)                       NOT NULL,
  EATTRIBUTE1   VARCHAR2(100 BYTE),
  EATTRIBUTE2   VARCHAR2(100 BYTE),
  EATTRIBUTE3   VARCHAR2(100 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLMAIL
 ADD PRIMARY KEY
 (SERIAL);

CREATE TABLE TBLLOSTMANHOURDETAIL
(
  SSCODE           VARCHAR2(40 BYTE)            NOT NULL,
  SHIFTDATE        NUMBER(8)                    NOT NULL,
  SHIFTCODE        VARCHAR2(40 BYTE)            NOT NULL,
  ITEMCODE         VARCHAR2(40 BYTE)            NOT NULL,
  SEQ              NUMBER(10)                   NOT NULL,
  LOSTMANHOUR      NUMBER(10)                   NOT NULL,
  EXCEPTIONCODE    VARCHAR2(40 BYTE)            NOT NULL,
  EXCEPTIONSERIAL  NUMBER                       NOT NULL,
  DUTYCODE         VARCHAR2(40 BYTE)            NOT NULL,
  MEMO             VARCHAR2(500 BYTE),
  MUSER            VARCHAR2(40 BYTE)            NOT NULL,
  MDATE            NUMBER(8)                    NOT NULL,
  MTIME            NUMBER(6)                    NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLLOSTMANHOURDETAIL
 ADD PRIMARY KEY
 (SSCODE, SHIFTDATE, SHIFTCODE, ITEMCODE, SEQ);

CREATE TABLE TBLINVPERIODSTD
(
  INVTYPE        VARCHAR2(40 BYTE)              NOT NULL,
  PERIODGROUP    VARCHAR2(40 BYTE)              NOT NULL,
  INVPERIODCODE  VARCHAR2(40 BYTE)              NOT NULL,
  PERCENTAGESTD  NUMBER(8,2)                    NOT NULL,
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(22)                     NOT NULL,
  MTIME          NUMBER(22)                     NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLINVPERIODSTD
 ADD PRIMARY KEY
 (INVTYPE, PERIODGROUP, INVPERIODCODE);

CREATE TABLE SCOTT_OLD_TBLMDL
(
  MDLCODE      VARCHAR2(40 BYTE)                NOT NULL,
  PMDLCODE     VARCHAR2(40 BYTE),
  MDLVER       VARCHAR2(40 BYTE),
  MDLTYPE      VARCHAR2(40 BYTE)                NOT NULL,
  MDLSTATUS    VARCHAR2(40 BYTE)                NOT NULL,
  MDLDESC      VARCHAR2(100 BYTE),
  MDLSEQ       NUMBER(10)                       NOT NULL,
  MDLHFNAME    VARCHAR2(100 BYTE),
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  ISSYS        VARCHAR2(1 BYTE)                 NOT NULL,
  ISACTIVE     VARCHAR2(1 BYTE)                 NOT NULL,
  FORMURL      VARCHAR2(100 BYTE),
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  ISRESTRAIN   VARCHAR2(1 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE SCOTT_OLD_TBLMENU
(
  MENUCODE     VARCHAR2(40 BYTE)                NOT NULL,
  MDLCODE      VARCHAR2(40 BYTE),
  PMENUCODE    VARCHAR2(40 BYTE),
  MENUDESC     VARCHAR2(100 BYTE),
  MENUSEQ      NUMBER(10)                       NOT NULL,
  MENUTYPE     VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE),
  VISIBILITY   VARCHAR2(1 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE TABLE TBLINDIRECTMANCOUNT
(
  SHIFTDATE   NUMBER(8)                         NOT NULL,
  SHIFTCODE   VARCHAR2(40 BYTE)                 NOT NULL,
  CREWCODE    VARCHAR2(40 BYTE)                 NOT NULL,
  FACCODE     VARCHAR2(40 BYTE)                 NOT NULL,
  FIRSTCLASS  VARCHAR2(40 BYTE)                 NOT NULL,
  MANCOUNT    NUMBER(10)                        NOT NULL,
  DURATION    NUMBER(10)                        NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER(8)                         NOT NULL,
  MTIME       NUMBER(6)                         NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLINDIRECTMANCOUNT
 ADD PRIMARY KEY
 (SHIFTDATE, SHIFTCODE, CREWCODE, FACCODE, FIRSTCLASS);

CREATE TABLE TBLBSHOMESETTING
(
  REPORTSEQ  NUMBER                             NOT NULL,
  MDLCODE    VARCHAR2(40 BYTE)                  NOT NULL,
  CHARTTYPE  VARCHAR2(40 BYTE)                  NOT NULL,
  MUSER      VARCHAR2(40 BYTE)                  NOT NULL,
  MDATE      NUMBER                             NOT NULL,
  MTIME      NUMBER                             NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLBSHOMESETTING
 ADD PRIMARY KEY
 (REPORTSEQ);

CREATE TABLE TBLBSHOMESETTINGDETAIL
(
  REPORTSEQ   NUMBER                            NOT NULL,
  PARAMNAME   VARCHAR2(100 BYTE)                NOT NULL,
  PARAMVALUE  VARCHAR2(2000 BYTE)               NOT NULL,
  MUSER       VARCHAR2(40 BYTE)                 NOT NULL,
  MDATE       NUMBER                            NOT NULL,
  MTIME       NUMBER                            NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

ALTER TABLE TBLBSHOMESETTINGDETAIL
 ADD PRIMARY KEY
 (REPORTSEQ, PARAMNAME);

CREATE TABLE EPSMAILFORMAT
(
  ID           NUMBER                           NOT NULL,
  TYPEID       NUMBER                           DEFAULT 0                     NOT NULL,
  SUBJECT      VARCHAR2(300 BYTE)               DEFAULT ' '                   NOT NULL,
  LANGUAGEID   NUMBER                           DEFAULT 0                     NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT sysdate               NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  CONTENT      CLOB                             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_MAILFORMAT ON EPSMAILFORMAT
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE EPSMAILFORMAT
 ADD CONSTRAINT PK_MAILFORMAT
 PRIMARY KEY
 (ID);

CREATE TABLE PICSLNE
(
  SEGMENT     VARCHAR2(15 BYTE)                 NOT NULL,
  LINE        VARCHAR2(15 BYTE)                 NOT NULL,
  LMUSER      VARCHAR2(30 BYTE)                 NOT NULL,
  LMDATE      NUMBER(10)                        NOT NULL,
  LMTIME      NUMBER(10)                        NOT NULL,
  NEEDNPL     VARCHAR2(1 BYTE)                  NOT NULL,
  REPORTLINE  VARCHAR2(15 BYTE)                 NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE GLOBAL TEMPORARY TABLE QUEST_SL_TEMP_EXPLAIN1
(
  STATEMENT_ID       VARCHAR2(30 BYTE),
  PLAN_ID            NUMBER,
  TIMESTAMP          DATE,
  REMARKS            VARCHAR2(80 BYTE),
  OPERATION          VARCHAR2(30 BYTE),
  OPTIONS            VARCHAR2(255 BYTE),
  OBJECT_NODE        VARCHAR2(128 BYTE),
  OBJECT_OWNER       VARCHAR2(30 BYTE),
  OBJECT_NAME        VARCHAR2(30 BYTE),
  OBJECT_ALIAS       VARCHAR2(65 BYTE),
  OBJECT_INSTANCE    NUMBER,
  OBJECT_TYPE        VARCHAR2(30 BYTE),
  OPTIMIZER          VARCHAR2(255 BYTE),
  SEARCH_COLUMNS     NUMBER,
  ID                 NUMBER,
  PARENT_ID          NUMBER,
  DEPTH              NUMBER,
  POSITION           NUMBER,
  COST               NUMBER,
  CARDINALITY        NUMBER,
  BYTES              NUMBER,
  OTHER_TAG          VARCHAR2(255 BYTE),
  PARTITION_START    VARCHAR2(255 BYTE),
  PARTITION_STOP     VARCHAR2(255 BYTE),
  PARTITION_ID       NUMBER,
  OTHER              LONG,
  DISTRIBUTION       VARCHAR2(30 BYTE),
  CPU_COST           NUMBER(38),
  IO_COST            NUMBER(38),
  TEMP_SPACE         NUMBER(38),
  ACCESS_PREDICATES  VARCHAR2(4000 BYTE),
  FILTER_PREDICATES  VARCHAR2(4000 BYTE)
)
ON COMMIT PRESERVE ROWS
NOCACHE;

CREATE TABLE SPPARTT
(
  LOGID      NUMBER                             NOT NULL,
  NAME       VARCHAR2(100 BYTE),
  LASTERDDL  DATE
)
PARTITION BY RANGE (LOGID)
(  
  PARTITION P0801 VALUES LESS THAN (1500)
    LOGGING
    NOCOMPRESS,  
  PARTITION P0901 VALUES LESS THAN (3000)
    LOGGING
    NOCOMPRESS,  
  PARTITION P1001 VALUES LESS THAN (5000)
    LOGGING
    NOCOMPRESS,  
  PARTITION PMAX VALUES LESS THAN (MAXVALUE)
    LOGGING
    NOCOMPRESS
)
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;


CREATE OR REPLACE PACKAGE pkg_mesreportengine
IS
    -- The Main Flow
    PROCEDURE CollectReportDataByMO(i_MOCode IN VARCHAR2);
    PROCEDURE CollectReportData (o_Return OUT NUMBER);

END;

/

SHOW ERRORS;

CREATE OR REPLACE PACKAGE pkg_MESWarning
IS
    PROCEDURE ProcessWorkPlanByBigSS(p_BigSSCode IN VARCHAR2, p_DateTime IN DATE);
    PROCEDURE ProcessWorkPlanMain;
    FUNCTION GetShiftDay(p_ShiftTypeCode IN VARCHAR2, p_CurrDateTime IN DATE)
    RETURN NUMBER;
END pkg_MESWarning;

/

SHOW ERRORS;

CREATE OR REPLACE PACKAGE PKG_ALERTENGINE IS
  PROCEDURE AlertError(input_shiftday IN VARCHAR2 := to_char(SYSDATE,'yyyymmdd'));
  PROCEDURE AlertErrorCode(input_shiftday IN VARCHAR2 := to_char(SYSDATE,'yyyymmdd'));
  PROCEDURE AlertOQCNG;
  PROCEDURE AlertDirectPass(input_shiftday IN VARCHAR2 := to_char(SYSDATE,'yyyymmdd'));
  PROCEDURE AlertLinePause(input_shiftday IN DATE := SYSDATE);
END PKG_ALERTENGINE;

/

SHOW ERRORS;

CREATE OR REPLACE package PKG_COMPUTELOSTMANHOUR is

  -- Author  : HIRO.CHEN
  -- Created : 2009-4-21 13:39:28
  -- Purpose : ????È˙2??Íß1?Í±

  -- Public function and procedure declarations
  PROCEDURE COMPUTELOSTMANHOUR(runDate IN DATE := SYSDATE);

end PKG_COMPUTELOSTMANHOUR;

/

SHOW ERRORS;

CREATE OR REPLACE PACKAGE BODY pkg_mesreportengine
IS
    PROCEDURE MainProcess(v_ONWIPList IN type_ONWIP_List);
    FUNCTION IsItemRouteLastOP(i_ItemCode IN VARCHAR2, i_RouteCode IN VARCHAR2, i_OPCode IN VARCHAR2)
        RETURN BOOLEAN;
    FUNCTION GetLastMESEntitySerial(i_LastONWIPData IN type_ONWIPData)
        RETURN NUMBER;
    FUNCTION GetMinSerialNotProcessed
        RETURN NUMBER;
    FUNCTION GetBigSSCode(i_SSCode IN VARCHAR2)
        RETURN VARCHAR2;
    FUNCTION GetOrgID(i_SSCode IN VARCHAR2)
        RETURN NUMBER;
    FUNCTION GetFactoryCode(i_SegCode IN VARCHAR2)
        RETURN VARCHAR2;
    FUNCTION GetOPControl(i_ItemCode IN VARCHAR2, i_RouteCode IN VARCHAR2, i_OPCode IN VARCHAR2)
        RETURN VARCHAR2;
    FUNCTION GetWIPDataList(i_Seq IN NUMBER)
        RETURN type_ONWIP_List;
    FUNCTION GetWIPDataListByMO(i_Seq IN NUMBER, i_MOCode IN VARCHAR2)
        RETURN type_ONWIP_List;
    FUNCTION GetRPTSOQTY(
        i_MOCode    IN VARCHAR2,i_ShiftDay  IN NUMBER,
        i_ItemCode  IN VARCHAR2,i_Serial    IN NUMBER
        )
        RETURN type_RPTSOQTY;
    PROCEDURE UpdateRPTSOQTY(i_SOQTY IN type_RPTSOQTY);
    FUNCTION GetRPTLINEQTY(
        i_MOCode    IN VARCHAR2,i_ShiftDay  IN NUMBER,
        i_ItemCode  IN VARCHAR2,i_Serial    IN NUMBER
        )
        RETURN type_RPTLINEQTY;
    PROCEDURE UpdateRPTLineQTY(i_LineQTY IN type_RPTLINEQTY);
    FUNCTION GetRPTOPQTY(
        i_MOCode    IN VARCHAR2,i_ShiftDay  IN NUMBER,
        i_ItemCode  IN VARCHAR2,i_Serial    IN NUMBER
        )
        RETURN type_RPTOPQTY;
    PROCEDURE UpdateRPTOPQTY(i_OPQTY IN type_RPTOPQTY);

    PROCEDURE CollectReportDataByMO(i_MOCode IN VARCHAR2)
    IS
        v_BeginTime             NUMBER;
        v_EndTime               NUMBER;
        v_SummaryLog            type_joblog;
        v_ONWIPList             type_ONWIP_List;
        v_ProcessCounter        INT;
        v_MinMOSerial           NUMBER;
        v_MaxMOSerial           NUMBER;
    BEGIN
        IF NVL(i_MOCode, ' ') = ' ' THEN
            RETURN;
        END IF;

        v_ProcessCounter := 0;

/

SHOW ERRORS;

CREATE OR REPLACE PACKAGE BODY pkg_MESWarning
IS
    --3??
    c_WPActionStatus_Init             VARCHAR2(40) := 'workplanactionstatus_init';           --?????Ë∑?
    c_WPActionStatus_Ready            VARCHAR2(40) := 'workplanactionstatus_ready';          --???2?
    c_WPActionStatus_Open             VARCHAR2(40) := 'workplanactionstatus_open';           --È˙2??
    c_WPActionStatus_Close            VARCHAR2(40) := 'workplanactionstatus_close';          --?·Í?
    
    c_MWarningStatus_No               VARCHAR2(40) := 'materialwarningstatus_no';            --??3?
    c_MWarningStatus_Delivery         VARCHAR2(40) := 'materialwarningstatus_delivery';      --?????
    c_MWarningStatus_Responsed        VARCHAR2(40) := 'materialwarningstatus_responsed';     --??ÏÛ?
    c_MWarningStatus_Lack             VARCHAR2(40) := 'materialwarningstatus_lack';          --Ë±?    
    
    c_MIssueType_Issue                VARCHAR2(40) := 'materialissuetype_issue';             --???????
    c_MIssueType_Receive              VARCHAR2(40) := 'materialissuetype_receive';          --2????
    c_MIssueType_LineTransferOut      VARCHAR2(40) := 'materialissuetype_linetransferout';  --2??????3?
    c_MIssueType_LineTransferIn       VARCHAR2(40) := 'materialissuetype_linetransferin';   --2??????a?

    c_MIssueStatus_Delivered          VARCHAR2(40) := 'materialissuestatus_delivered';      --????
    c_MIssueStatus_Close              VARCHAR2(40) := 'materialissuestatus_close';          --ÌÍ3?

    c_MReqType_Delivery               VARCHAR2(40) := 'materialreqtype_delivery';           --???
    c_MReqType_Lack                   VARCHAR2(40) := 'materialreqtype_lack';               --Ë±?  
          
    c_MReqStatus_Requesting           VARCHAR2(40) := 'materialreqstatus_requesting';       --?????
    c_MReqStatus_Responsed            VARCHAR2(40) := 'materialreqstatus_responsed';         --???‡Ì        
    
    c_MaintainUser                    VARCHAR2(40) := 'JOB';
    

    --GetShiftDay
    FUNCTION GetShiftDay(p_ShiftTypeCode IN VARCHAR2, p_CurrDateTime IN DATE)
    RETURN NUMBER
    IS
        v_CurrTime              NUMBER;
        v_IsOverDay             NUMBER;
    BEGIN
        v_CurrTime := TO_NUMBER(TO_CHAR(p_CurrDateTime, 'HH24MISS'));        

        SELECT COUNT(*) INTO v_IsOverDay 
        FROM tbltp
        WHERE shifttypecode = p_shiftTypeCode
        AND isoverdate = 1
        AND (CASE WHEN tpbtime > tpetime THEN tpbtime - 240000 ELSE tpbtime END) <= v_CurrTime
        AND tpetime >= v_CurrTime;
        v_IsOverDay := NVL(v_IsOverDay, 0);
        
        IF (v_IsOverDay > 0) THEN
            RETURN TO_NUMBER(TO_CHAR(p_CurrDateTime - 1, 'YYYYMMDD'));
        ELSE
            RETURN TO_NUMBER(TO_CHAR(p_CurrDateTime, 'YYYYMMDD'));
        END IF;
        
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[GetShiftDay]' || SQLERRM);
    END GetShiftDay;

    
    --GetNextWorkPlan
    FUNCTION GetNextWorkPlan(p_BigSSCode IN VARCHAR2, p_PlanDate IN NUMBER, 
        p_MOCode IN VARCHAR2, p_PlanSeqBase IN NUMBER, p_ActionStatusList IN VARCHAR2)
    RETURN type_WorkPlan
    IS
        v_SQL                             VARCHAR2(2000);
        TYPE cursor_DynamicSelect         IS REF CURSOR;
        v_ResultList                      cursor_DynamicSelect;    
        
        v_ReturnValue                     type_WorkPlan;
    BEGIN

        v_SQL := ' ';
        v_SQL := v_SQL || 'SELECT planseq, mocode, moseq, itemcode, planqty, materialqty, actqty, actionstatus, materialstatus ';
        v_SQL := v_SQL || 'FROM tblworkplan ';
        v_SQL := v_SQL || 'WHERE bigsscode = ''' || p_BigSSCode || ''' ';
        v_SQL := v_SQL || 'AND plandate = ' || p_PlanDate || ' ';
        
        IF (NVL(p_MOCode, ' ') <> ' ') THEN
            v_SQL := v_SQL || 'AND mocode = ''' || p_MOCode || ''' ';
        END IF;
        
        IF (p_PlanSeqBase > -1) THEN
            v_SQL := v_SQL || 'AND planseq > ' || TO_CHAR(p_PlanSeqBase) || ' ';
        END IF;        
        
        IF (NVL(p_ActionStatusList, ' ') <> ' ') THEN
            v_SQL := v_SQL || 'AND actionstatus IN (' || p_ActionStatusList || ') ';
        END IF;
        
        v_SQL := v_SQL || 'ORDER BY planseq ';
        
        v_ReturnValue := NEW type_WorkPlan();
        OPEN v_ResultList FOR v_SQL;        
        LOOP
            FETCH v_ResultList
            INTO v_ReturnValue.PlanSeq, v_ReturnValue.MOCode, v_ReturnValue.MOSeq, v_ReturnValue.ItemCode, 
                v_ReturnValue.PlanQty, v_ReturnValue.MaterialQty, v_ReturnValue.ActQty, v_ReturnValue.ActionStatus, v_ReturnValue.MaterialStatus;
            
            EXIT WHEN v_ResultList%NOTFOUND OR NVL(v_ReturnValue.MOCode, ' ') <> ' ';
        END LOOP;
        CLOSE v_ResultList;  
        
        IF (NVL(v_ReturnValue.MOCode, ' ') <> ' ') THEN
            v_ReturnValue.BigSSCode := p_BigSSCode;
            v_ReturnValue.PlanDate := p_PlanDate;
        END IF;       
        
        RETURN v_ReturnValue;
    
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[GetNextWorkPlan]' || SQLERRM);
    END GetNextWorkPlan;


    --OpenWorkPlan
    PROCEDURE OpenWorkPlan(p_WorkPlan IN OUT type_WorkPlan)
    IS
        v_DateTime        DATE;
        v_MaintainDate    NUMBER;
        v_MaintainTime    NUMBER;
    BEGIN
        v_DateTime := SYSDATE;
        v_MaintainDate := TO_NUMBER(TO_CHAR(v_DateTime, 'YYYYMMDD'));
        v_MaintainTime := TO_NUMBER(TO_CHAR(v_DateTime, 'HH24MISS'));
        
        p_WorkPlan.ActionStatus := c_WPActionStatus_Open;
        
        UPDATE tblworkplan
        SET actionstatus = c_WPActionStatus_Open,
            muser = c_MaintainUser,
            mdate = v_MaintainDate,
            mtime = v_MaintainTime
        WHERE bigsscode = p_WorkPlan.BigSSCode
        AND plandate = p_WorkPlan.PlanDate
        AND mocode = p_WorkPlan.MOCode
        AND moseq = p_WorkPlan.MOSeq;    
    
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[OpenWorkPlan]' || SQLERRM);
    END OpenWorkPlan;


    --CloseWorkPlan
    PROCEDURE CloseWorkPlan(p_WorkPlan IN OUT type_WorkPlan)
    IS
        v_DateTime        DATE;
        v_MaintainDate    NUMBER;
        v_MaintainTime    NUMBER;
    BEGIN
        v_DateTime := SYSDATE;
        v_MaintainDate := TO_NUMBER(TO_CHAR(v_DateTime, 'YYYYMMDD'));
        v_MaintainTime := TO_NUMBER(TO_CHAR(v_DateTime, 'HH24MISS'));
        
        p_WorkPlan.ActionStatus := c_WPActionStatus_CLose;
        
        UPDATE tblworkplan
        SET actionstatus = c_WPActionStatus_CLose,
            muser = c_MaintainUser,
            mdate = v_MaintainDate,
            mtime = v_MaintainTime
        WHERE bigsscode = p_WorkPlan.BigSSCode
        AND plandate = p_WorkPlan.PlanDate
        AND mocode = p_WorkPlan.MOCode
        AND moseq = p_WorkPlan.MOSeq;    
    
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[CloseWorkPlan]' || SQLERRM);
    END CloseWorkPlan;   


    --UpdateWorkPlanQty
    PROCEDURE UpdateWorkPlanQty(p_WorkPlan IN type_WorkPlan)
    IS
        v_DateTime        DATE;
        v_MaintainDate    NUMBER;
        v_MaintainTime    NUMBER;
    BEGIN
        v_DateTime := SYSDATE;
        v_MaintainDate := TO_NUMBER(TO_CHAR(v_DateTime, 'YYYYMMDD'));
        v_MaintainTime := TO_NUMBER(TO_CHAR(v_DateTime, 'HH24MISS'));
        
        UPDATE tblworkplan
        SET materialqty = p_WorkPlan.MaterialQty,
            actqty = p_WorkPlan.ActQty,
            muser = c_MaintainUser,
            mdate = v_MaintainDate,
            mtime = v_MaintainTime
        WHERE bigsscode = p_WorkPlan.BigSSCode
        AND plandate = p_WorkPlan.PlanDate
        AND mocode = p_WorkPlan.MOCode
        AND moseq = p_WorkPlan.MOSeq;    
    
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[UpdateWorkPlanQty]' || SQLERRM);
    END UpdateWorkPlanQty;   


    --RecordTrans
    PROCEDURE RecordTrans(p_WorkPlan IN type_WorkPlan, p_TransType IN VARCHAR2, p_TransQty IN NUMBER)
    IS 
        v_DateTime        DATE;
        v_MaintainDate    NUMBER;
        v_MaintainTime    NUMBER;    
            
        v_IssueSeq        NUMBER;    
    BEGIN
        v_DateTime := SYSDATE;
        v_MaintainDate := TO_NUMBER(TO_CHAR(v_DateTime, 'YYYYMMDD'));
        v_MaintainTime := TO_NUMBER(TO_CHAR(v_DateTime, 'HH24MISS'));
        
        SELECT COUNT(*) INTO v_IssueSeq
        FROM tblmaterialissue
        WHERE bigsscode = p_WorkPlan.BigSSCode
        AND plandate = p_WorkPlan.PlanDate
        AND mocode = p_WorkPlan.MOCode
        AND moseq = p_WorkPlan.MOSeq;
        
        IF (v_IssueSeq <= 0) THEN
            v_IssueSeq := 1;
        ELSE        
            SELECT issueseq + 1 INTO v_IssueSeq
            FROM tblmaterialissue
            WHERE bigsscode = p_WorkPlan.BigSSCode
            AND plandate = p_WorkPlan.PlanDate
            AND mocode = p_WorkPlan.MOCode
            AND moseq = p_WorkPlan.MOSeq
            AND issueseq = (
                SELECT MAX(issueseq)
                FROM tblmaterialissue
                WHERE bigsscode = p_WorkPlan.BigSSCode
                AND plandate = p_WorkPlan.PlanDate
                AND mocode = p_WorkPlan.MOCode
                AND moseq = p_WorkPlan.MOSeq)
            FOR UPDATE;
            v_IssueSeq := NVL(v_IssueSeq, 1);
        END IF;
        
        INSERT INTO tblmaterialissue(bigsscode, plandate, mocode, moseq, issueseq, 
            issueqty, issuetype, issuestatus, muser, mdate, mtime)
        VALUES(p_WorkPlan.BigSSCode, p_WorkPlan.PlanDate, p_WorkPlan.MOCode, p_WorkPlan.MOSeq, v_IssueSeq, 
            p_TransQty, p_TransType, c_MIssueStatus_Close, c_MaintainUser, v_MaintainDate, v_MaintainTime);    
    
        RETURN;
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[RecordTrans]' || SQLERRM);
    END RecordTrans;   


    --GenerateWarning
    PROCEDURE GenerateWarning(p_WorkPlan IN type_WorkPlan, p_WarningType IN VARCHAR2)
    IS
        v_DateTime           DATE;
        v_MaintainDate       NUMBER;
        v_MaintainTime       NUMBER;  
        
        v_MWarningStatus     VARCHAR2(40);
        v_RequestQty         NUMBER;
        v_MayBeQty           NUMBER;   
          
        v_RequestSeq         NUMBER;                   
    BEGIN
        v_DateTime := SYSDATE;
        v_MaintainDate := TO_NUMBER(TO_CHAR(v_DateTime, 'YYYYMMDD'));
        v_MaintainTime := TO_NUMBER(TO_CHAR(v_DateTime, 'HH24MISS')); 
        
        IF (p_WarningType = c_MReqType_Delivery) THEN
            v_MWarningStatus := c_MWarningStatus_Delivery;
            v_RequestQty := p_WorkPlan.PlanQty - p_WorkPlan.MaterialQty;
        END IF;
        
        IF (p_WarningType = c_MReqType_Lack) THEN
            v_MWarningStatus := c_MWarningStatus_Lack;
            v_RequestQty := p_WorkPlan.ActQty - p_WorkPlan.MaterialQty;
        END IF;           
        
        UPDATE tblworkplan
        SET materialstatus = v_MWarningStatus,
            lastreqtime = v_MaintainTime,
            promisetime = 0,
            muser = c_MaintainUser,
            mdate = v_MaintainDate,
            mtime = v_MaintainTime
        WHERE bigsscode = p_WorkPlan.BigSSCode
        AND plandate = p_WorkPlan.PlanDate
        AND mocode = p_WorkPlan.MOCode
        AND moseq = p_WorkPlan.MOSeq;

        SELECT SUM(materialqty) - SUM(Actqty) INTO v_MayBeQty
        FROM tblworkplan
        WHERE itemcode = p_WorkPlan.ItemCode
        AND (bigsscode, plandate, mocode, moseq) 
            <> (SELECT p_WorkPlan.BigSSCode, p_WorkPlan.PlanDate, p_WorkPlan.MOCode, p_WorkPlan.MOSeq FROM dual);
        v_MayBeQty := NVL(v_MayBeQty, 0);  
        
        SELECT COUNT(*) INTO v_RequestSeq
        FROM tblmaterialreqinfo
        WHERE bigsscode = p_WorkPlan.BigSSCode
        AND plandate = p_WorkPlan.PlanDate
        AND mocode = p_WorkPlan.MOCode
        AND moseq = p_WorkPlan.MOSeq;
        
        IF (v_RequestSeq <= 0) THEN
            v_RequestSeq := 1;
        ELSE       
            SELECT requestseq + 1 INTO v_RequestSeq
            FROM tblmaterialreqinfo
            WHERE bigsscode = p_WorkPlan.BigSSCode
            AND plandate = p_WorkPlan.PlanDate
            AND mocode = p_WorkPlan.MOCode
            AND moseq = p_WorkPlan.MOSeq
            AND requestseq = (
                SELECT MAX(requestseq)
                FROM tblmaterialreqinfo
                WHERE bigsscode = p_WorkPlan.BigSSCode
                AND plandate = p_WorkPlan.PlanDate
                AND mocode = p_WorkPlan.MOCode
                AND moseq = p_WorkPlan.MOSeq)
            FOR UPDATE;   
            v_RequestSeq := NVL(v_RequestSeq, 1);
        END IF;
        
        INSERT INTO tblmaterialreqinfo(bigsscode, plandate, mocode, moseq, requestseq, 
            planseq, itemcode, requestqty, maybeqty, status, reqtype, 
            muser, mdate, mtime)
        VALUES(p_WorkPlan.BigSSCode, p_WorkPlan.PlanDate, p_WorkPlan.MOCode, p_WorkPlan.MOSeq, v_RequestSeq,
            p_WorkPlan.PlanSeq, p_WorkPlan.ItemCode, v_RequestQty, v_MayBeQty, c_MReqStatus_Requesting, p_WarningType, 
            c_MaintainUser, v_MaintainDate, v_MaintainTime);
            
        RETURN;
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[GenerateWarning]' || SQLERRM);
    END GenerateWarning;  
    
    
    --RecordJobResult
    PROCEDURE RecordJobResult(p_JobID IN VARCHAR2, p_StartDateTime IN DATE, p_EndDateTime IN DATE, 
        p_ProcessCount IN NUMBER, p_Result IN VARCHAR2, p_ErrorMsg IN VARCHAR2)
    IS 
        v_UsedSeconds     NUMBER;
    BEGIN
        v_UsedSeconds:= (p_EndDateTime - p_StartDateTime) * 24 * 60 *60;
    
        INSERT INTO tbljoblog(jobid, startdatetime, enddatetime, usedtime, processcount, result, errormsg) 
        VALUES(p_JobID, p_StartDateTime, p_EndDateTime, v_UsedSeconds, p_ProcessCount, p_Result, p_ErrorMsg);
        COMMIT;  
    
        RETURN;
    EXCEPTION
        WHEN OTHERS THEN
        
        RETURN;
    END RecordJobResult;      


    --ProcessWorkPlan
    PROCEDURE ProcessWorkPlan(p_WorkPlan IN type_WorkPlan, p_InputQty IN NUMBER)
    IS
        v_CurrWorkPlan                    type_WorkPlan;
        p_NotUsedInputQty                 NUMBER; 
    
        v_SQL                             VARCHAR2(2000);
        TYPE cursor_DynamicSelect         IS REF CURSOR;
        v_ResultList                      cursor_DynamicSelect;
        
        v_NextWorkPlan                    type_WorkPlan;               
        v_TransferQty                     NUMBER;        
        v_WarningStandard                 NUMBER;
        v_ResultCount                     NUMBER;    
    BEGIN  
        v_CurrWorkPlan := p_WorkPlan;     
        p_NotUsedInputQty :=  p_InputQty;        


        --Step.1--µ˙??????ActQty 
        IF (v_CurrWorkPlan.ActQty + p_NotUsedInputQty < v_CurrWorkPlan.PlanQty) THEN
            v_CurrWorkPlan.ActQty := v_CurrWorkPlan.ActQty + p_NotUsedInputQty;
            p_NotUsedInputQty := 0;

            UpdateWorkPlanQty(v_CurrWorkPlan);            
        ELSE
            p_NotUsedInputQty := v_CurrWorkPlan.ActQty + p_NotUsedInputQty - v_CurrWorkPlan.PlanQty;
            v_CurrWorkPlan.ActQty := v_CurrWorkPlan.PlanQty; 
            
            UpdateWorkPlanQty(v_CurrWorkPlan);
        END IF;
        
        --?1?µ±?∞WorkPlan??????      
        IF (v_CurrWorkPlan.ActQty = v_CurrWorkPlan.PlanQty) THEN        
            v_NextWorkPlan := GetNextWorkPlan(v_CurrWorkPlan.BigSSCode, v_CurrWorkPlan.PlanDate, v_CurrWorkPlan.MOCode, v_CurrWorkPlan.PlanSeq, '''' || c_WPActionStatus_Init || ''', ''' || c_WPActionStatus_Ready || '''');           
            IF (NVL(v_NextWorkPlan.MOCode, ' ') <> ' ') THEN
                --Step.2--?1??MO??????WorkPlan?????1??WorkPlan
                CloseWorkPlan(v_CurrWorkPlan);
            ELSE
                --Step.3--µ˙???????ActQty?o?1??MO??????WorkPlan2???????a??‡Û‡µ?NotUsedInputQty?µ±?∞WorkPlan
                IF (p_NotUsedInputQty > 0) THEN
                   v_CurrWorkPlan.ActQty := v_CurrWorkPlan.ActQty + p_NotUsedInputQty;
                   
                   UpdateWorkPlanQty(v_CurrWorkPlan);
                END IF;
            END IF;         
        END IF; 

  
        --Step.4--???MaterialQty
        ----------?1?BigSS+MO?????WorkPlan???˙materialqty <> actqty???ÛÍ????‡Û‡µ?MaterialQty?a?1??
        v_SQL := ' ';
        v_SQL := v_SQL || 'SELECT bigsscode, plandate, mocode, moseq, materialqty, actqty FROM tblworkplan ';
        v_SQL := v_SQL || 'WHERE bigsscode = ''' || v_CurrWorkPlan.BigSSCode || ''' ';
        v_SQL := v_SQL || 'AND mocode = ''' || v_CurrWorkPlan.MOCode || ''' ';
        v_SQL := v_SQL || 'AND materialqty <> actqty ';
        v_SQL := v_SQL || 'AND (bigsscode, plandate, mocode, moseq) <> ';
        v_SQL := v_SQL || '(SELECT ''' || v_CurrWorkPlan.BigSSCode || ''', ' 
            || TO_CHAR(v_CurrWorkPlan.PlanDate) || ', ''' 
            || v_CurrWorkPlan.MOCode || ''', ' 
            || TO_CHAR(v_CurrWorkPlan.MOSeq) || ' FROM dual) ';
            
        OPEN v_ResultList FOR v_SQL;        
        LOOP
            --????v_NextWorkPlan??????????materialqty <> actqty?WorkPlan
            v_NextWorkPlan := NEW type_WorkPlan();
         
            FETCH v_ResultList
            INTO v_NextWorkPlan.BigSSCode, v_NextWorkPlan.PlanDate, v_NextWorkPlan.MOCode, v_NextWorkPlan.MOSeq,  
                v_NextWorkPlan.MaterialQty, v_NextWorkPlan.ActQty;
            EXIT WHEN v_ResultList%NOTFOUND;

            v_TransferQty := v_NextWorkPlan.MaterialQty - v_NextWorkPlan.ActQty;
            v_NextWorkPlan.MaterialQty := v_NextWorkPlan.MaterialQty - v_TransferQty;
            v_CurrWorkPlan.MaterialQty := v_CurrWorkPlan.MaterialQty + v_TransferQty;
             
            UpdateWorkPlanQty(v_NextWorkPlan); 
            RecordTrans(v_NextWorkPlan, c_MIssueType_LineTransferOut, v_TransferQty);
            
            UpdateWorkPlanQty(v_CurrWorkPlan);
            RecordTrans(v_CurrWorkPlan, c_MIssueType_LineTransferIn, v_TransferQty);

        END LOOP;
        CLOSE v_ResultList;            
        
        SELECT MAX(requestqty) INTO v_WarningStandard
        FROM tblmaterialreqstd
        WHERE itemcode = v_CurrWorkPlan.ItemCode;
        v_WarningStandard := NVL(v_WarningStandard, -1);


        --Step.5--?WorkPlan???????Û˙???Close?2???????
        IF (v_CurrWorkPlan.ActionStatus <> c_WPActionStatus_Close AND v_WarningStandard >= 0) THEN            
            --MaterialQty < PlanQty2??ÚMaterialQty - ActQty < ±Í????2??????±§????±§???????
            IF (v_CurrWorkPlan.MaterialQty < v_CurrWorkPlan.PlanQty 
                AND v_CurrWorkPlan.MaterialQty - v_CurrWorkPlan.ActQty < v_WarningStandard
                AND v_CurrWorkPlan.MaterialStatus = c_MWarningStatus_No) THEN
                
                GenerateWarning(v_CurrWorkPlan, c_MReqType_Delivery);
            END IF;
            
            --MaterialQty - ActQty < 0??2??????Ë±???????±§Ë±?????
            IF (v_CurrWorkPlan.MaterialQty - v_CurrWorkPlan.ActQty < 0
               AND (v_CurrWorkPlan.MaterialStatus = c_MWarningStatus_No 
                   OR v_CurrWorkPlan.MaterialStatus = c_MWarningStatus_Delivery)) THEN
                GenerateWarning(v_CurrWorkPlan, c_MReqType_Lack);
            END IF;                    
        END IF;


        --Step.6--?BigSS+PlanDate????WorkPlan?????
        ----------MaterialQty >= PlanQty2??ÚPlanQty - ActQty < ±Í??Í±?????????WorkPlan??????ÍÏa
        ----------???WorkPlan??MO????¸MaterialQty - ?MO???¸ActQty < ±Í????2?????WorkPlan??????????±§???WorkPlan???????
        IF (v_CurrWorkPlan.MaterialQty >= v_CurrWorkPlan.PlanQty 
            AND v_WarningStandard >= 0
            AND v_CurrWorkPlan.PlanQty - v_CurrWorkPlan.ActQty < v_WarningStandard) THEN

            v_NextWorkPlan := GetNextWorkPlan(v_CurrWorkPlan.BigSSCode, v_CurrWorkPlan.PlanDate, '', v_CurrWorkPlan.PlanSeq, '''' || c_WPActionStatus_Init || ''', ''' || c_WPActionStatus_Ready || ''''); 
            
            IF (NVL(v_NextWorkPlan.MOCode, ' ') <> ' ') THEN    
                SELECT MAX(requestqty) INTO v_WarningStandard
                FROM tblmaterialreqstd
                WHERE itemcode = v_NextWorkPlan.ItemCode;
                v_WarningStandard := NVL(v_WarningStandard, -1);
                
                IF (v_WarningStandard >= 0) THEN                
                    SELECT SUM(materialqty) - SUM(actqty) INTO v_ResultCount
                    FROM tblworkplan
                    WHERE bigsscode = v_NextWorkPlan.BigSSCode
                    AND mocode = v_NextWorkPlan.MOCode; 
                    v_ResultCount := NVL(v_ResultCount, 0);          
                
                    IF (v_ResultCount < v_WarningStandard AND v_NextWorkPlan.MaterialStatus = c_MWarningStatus_No) THEN
                        GenerateWarning(v_NextWorkPlan, c_MReqType_Delivery);
                    END IF;
                END IF;                    
            END IF;
        END IF;
        
        RETURN;
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[ProcessWorkPlan]' || SQLERRM);
    END ProcessWorkPlan;   


    --ProcessWorkPlanByBigSS
    PROCEDURE ProcessWorkPlanByBigSS(p_BigSSCode IN VARCHAR2, p_DateTime IN DATE)
    IS
        v_StartDateTime                   DATE;
        v_EndDateTime                     DATE;
        v_ProcessCount                    NUMBER;
        
        v_CurrShiftCodeType               VARCHAR2(40);
        v_LastShiftDay                    NUMBER;
        v_CurrShiftDay                    NUMBER;
        v_AddShiftDay                     NUMBER;  
        
        v_SQL                             VARCHAR2(2000);
        TYPE cursor_DynamicSelect         IS REF CURSOR;
        v_ResultList                      cursor_DynamicSelect;    

        v_MOCode                          VARCHAR2(40);
        v_MOInputQty                      NUMBER;   
        v_UsedInputQty                    NUMBER;  
        v_NotUsedInputQty                 NUMBER;  
        v_WorkPlan                        type_WorkPlan; 
        
        v_WarningStandard                 NUMBER;
        v_ResultCount                     NUMBER;             
    BEGIN
        v_StartDateTime := SYSDATE;
        
        --Step.1--???BigSS?µ˙??SS?ShiftCodeType?????µ±?∞Í???ShiftDay?????2??Í±oÛµ?ShiftDay
        SELECT COUNT(*) INTO v_ResultCount 
        FROM (SELECT * FROM tblss WHERE bigsscode = p_BigSSCode ORDER BY ssseq) 
        WHERE ROWNUM <= 1;
        v_ResultCount := NVL(v_ResultCount, 0);
        
        IF (v_ResultCount > 0) THEN
            SELECT shifttypecode INTO v_CurrShiftCodeType 
            FROM (SELECT * FROM tblss WHERE bigsscode = p_BigSSCode ORDER BY ssseq) 
            WHERE ROWNUM <= 1;
            v_CurrShiftCodeType := NVL(v_CurrShiftCodeType, ' ');
        ELSE
            v_CurrShiftCodeType := ' ';
        END IF;

        v_LastShiftDay := GetShiftDay(v_CurrShiftCodeType, p_DateTime - 1);
        v_CurrShiftDay := GetShiftDay(v_CurrShiftCodeType, p_DateTime);
        v_AddShiftDay := GetShiftDay(v_CurrShiftCodeType, p_DateTime + 2 / 24);        
        
        --Step.2--1??BigSS???˘Û??ÚÏÏµ?WorkPlan
        UPDATE tblworkplan
        SET actionstatus = c_WPActionStatus_Close
        WHERE bigsscode = p_BigSSCode
        AND plandate = v_LastShiftDay;        


        --Step.3--??aBigSS???˘Û???ÏÏtblrptsoqty???????MO?µ˙???WorkPlan
        v_SQL := ' ';
        v_SQL := v_SQL || 'SELECT mocode, SUM(moinputcount) AS moinputqty ';
        v_SQL := v_SQL || 'FROM tblrptsoqty, tblmesentitylist ';
        v_SQL := v_SQL || 'WHERE tblrptsoqty.tblmesentitylist_serial = tblmesentitylist.serial ';
        v_SQL := v_SQL || 'AND bigsscode = ''' || p_BigSSCode || ''' ';
        v_SQL := v_SQL || 'AND shiftday = ' || v_CurrShiftDay || ' ';
        v_SQL := v_SQL || 'GROUP BY mocode ';       
        v_SQL := v_SQL || 'ORDER BY mocode ';
        
        OPEN v_ResultList FOR v_SQL;        
        LOOP
            FETCH v_ResultList INTO v_MOCode, v_MOInputQty; 
            EXIT WHEN v_ResultList%NOTFOUND;           
            
            IF (NVL(v_MOCode, ' ') <> ' ') THEN                
                --???µ±?∞MO??ÈÛ?Qty
                SELECT SUM(actqty) INTO v_UsedInputQty
                FROM tblworkplan
                WHERE bigsscode = p_BigSSCode
                AND plandate = v_CurrShiftDay 
                AND mocode = v_MOCode; 
                v_UsedInputQty := NVL(v_UsedInputQty, 0);  
             
                v_NotUsedInputQty := v_MOInputQty - v_UsedInputQty; 
                
                --2?ÚBigSSCode+MOCode???ActioniStatus=Open?WorkPlan
                v_WorkPlan := GetNextWorkPlan(p_BigSSCode, v_CurrShiftDay, v_MOCode, -1, '''' || c_WPActionStatus_Open || '''');
                
                --?1?????????ÚBigSSCode+MOCode??????PlanSeq????aWorkPlan????a 
                IF (NVL(v_WorkPlan.MOCode, ' ') = ' ' AND v_NotUsedInputQty > 0) THEN
                    v_WorkPlan := GetNextWorkPlan(p_BigSSCode, v_CurrShiftDay, v_MOCode, -1, '''' || c_WPActionStatus_Init || ''', ''' || c_WPActionStatus_Ready || '''');        
                    IF (NVL(v_WorkPlan.MOCode, ' ') <> ' ') THEN
                        OpenWorkPlan(v_WorkPlan);
                    END IF;             
                END IF;
                
                --??WorkPlan???‡Ì?§Ì??????????a?????????
                IF (NVL(v_WorkPlan.MOCode, ' ') <> ' ' AND v_WorkPlan.ActionStatus = c_WPActionStatus_Open) THEN 
                    ProcessWorkPlan(v_WorkPlan, v_NotUsedInputQty);                    
                END IF;
            END IF;
        END LOOP;
        CLOSE v_ResultList;


        --Step.4--???ÏÏ?????
        IF (v_CurrShiftDay < v_AddShiftDay) THEN        
            --?1?2??Í±oÛµ?ShiftDay?µ±?∞ShiftDay2???????????ShiftDay?????PlanSeq?WorkPlan
            v_WorkPlan := GetNextWorkPlan(p_BigSSCode, v_AddShiftDay, '', -1, '''' || c_WPActionStatus_Init || ''', ''' || c_WPActionStatus_Ready || ''''); 

            --?MO????¸MaterialQty - ?MO???¸ActQty < ±Í????2?????WorkPlan??????????±§???WorkPlan???????
            IF (NVL(v_WorkPlan.MOCode, ' ') <> ' ') THEN            
                SELECT MAX(requestqty) INTO v_WarningStandard
                FROM tblmaterialreqstd
                WHERE itemcode = v_WorkPlan.ItemCode;
                v_WarningStandard := NVL(v_WarningStandard, -1);
                
                IF (v_WarningStandard >= 0) THEN
                    SELECT SUM(materialqty) - SUM(actqty) INTO v_ResultCount
                    FROM tblworkplan
                    WHERE bigsscode = v_WorkPlan.BigSSCode
                    AND mocode = v_WorkPlan.MOCode;
                    v_ResultCount := NVL(v_ResultCount, 0);           
                
                    IF (v_ResultCount < v_WarningStandard AND v_WorkPlan.MaterialStatus = c_MWarningStatus_No) THEN
                        GenerateWarning(v_WorkPlan, c_MReqType_Delivery);
                    END IF;                
                END IF;                
            END IF;  
        END IF;    
        
        COMMIT; 
        v_EndDateTime := SYSDATE;        
        RecordJobResult('Job_ProcessWorkPlan', v_StartDateTime, v_EndDateTime, 1, 'OK', '');            
        
        RETURN;
    EXCEPTION
        WHEN OTHERS THEN
        ROLLBACK;  
        
        v_EndDateTime := SYSDATE;        
        RecordJobResult('Job_ProcessWorkPlan', v_StartDateTime, v_EndDateTime, 0, 'FAIL', SUBSTR(SQLERRM,1,250));
        
        RETURN;
    END ProcessWorkPlanByBigSS;
        

    --ProcessWorkPlanMain
    PROCEDURE ProcessWorkPlanMain
    IS
        v_SQL                             VARCHAR2(2000);
        TYPE cursor_DynamicSelect         IS REF CURSOR;
        v_ResultList                      cursor_DynamicSelect; 
              
        v_BigSSCode                       VARCHAR2(40);            
    BEGIN
        --????˘Û?BigSS??????ProcessWorkPlanByBigSS
        v_SQL := ' ';
        v_SQL := v_SQL || 'SELECT DISTINCT paramalias AS bigsscode ';
        v_SQL := v_SQL || 'FROM tblsysparam ';
        v_SQL := v_SQL || 'WHERE paramgroupcode = ''BIGLINEGROUP'' ';
        v_SQL := v_SQL || 'AND paramalias IN (SELECT bigsscode FROM tblss) ';

        OPEN v_ResultList FOR v_SQL;        
        LOOP
            FETCH v_ResultList INTO v_BigSSCode;
            EXIT WHEN v_ResultList%NOTFOUND;
            
            IF (NVL(v_BigSSCode, ' ') <> ' ') THEN
                ProcessWorkPlanByBigSS(v_BigSSCode, SYSDATE);
            END IF;
            
        END LOOP;
        CLOSE v_ResultList;        
        
        RETURN;
    EXCEPTION
        WHEN OTHERS THEN
        RETURN;
    END ProcessWorkPlanMain;    

END pkg_MESWarning;

/

SHOW ERRORS;

CREATE OR REPLACE PACKAGE BODY pkg_alertengine IS
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : 3????
	------------------------------------------------
  c_MaintainUser                    VARCHAR2(40) := 'ALERTJOB';
	c_DealStatus                      VARCHAR2(40) := 'dealstatus_notdeal';
	c_ItemType_FG                     VARCHAR2(40) := 'itemtype_finishedproduct';
	c_ItemType_NFG                    VARCHAR2(40) := 'itemtype_semimanufacture';
	c_mailtype_alerterror             VARCHAR2(40) := 'c_mailtype_alerterror';
	c_mailtype_errorcode              VARCHAR2(40) := 'c_mailtype_errorcode';
	c_mailtype_OQCNG                  VARCHAR2(40) := 'c_mailtype_OQCNG';
	c_mailtype_directpass             VARCHAR2(40) := 'c_mailtype_directpass';
	c_mailtype_linepause              VARCHAR2(40) := 'c_mailtype_linepause';
	
  ------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : o??È˘????
	------------------------------------------------
	
	--**********************************************
	PROCEDURE LinePauseByMO(MoCodeList IN VARCHAR2);
	
	--**********************************************
	PROCEDURE GetFirstSecondClass(mgroup IN VARCHAR2,
                               firstclass  OUT VARCHAR2,
                               secondclass OUT VARCHAR2);
															 
  PROCEDURE GetFirstSecondClassByItemCode(i_ItemCode IN VARCHAR2,
	                                        firstclass  OUT VARCHAR2,
                                          secondclass OUT VARCHAR2);
															 
	--**********************************************				 
  FUNCTION GetMOListToPause(ItemCode  IN VARCHAR2,
                            shiftday  IN NUMBER,
                            shiftcode IN VARCHAR2,
                            bigsscode IN VARCHAR2) 
	RETURN VARCHAR2;
	
  --**********************************************
	FUNCTION split(p_list VARCHAR2,is_distinct BOOLEAN := FALSE,p_sep VARCHAR2 := ',')
  RETURN type_varchar_list;
	
	--**********************************************
	FUNCTION GetRecipients(ItemSequence   IN VARCHAR2,
                            bigsscode   IN VARCHAR2,
                            firstclass  IN VARCHAR2,
                            secondclass IN VARCHAR2) 
	RETURN VARCHAR2;
	
	--**********************************************
	FUNCTION FormatMailContent(mailContent IN VARCHAR2,mailCategory IN VARCHAR2,
	                           replaceList IN type_varchar_list) 
	RETURN VARCHAR2;
	
	--**********************************************
	FUNCTION GetBigSSCode(i_SSCode IN VARCHAR2) RETURN VARCHAR2;
	
  ------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : 1??????
	------------------------------------------------
  PROCEDURE AlertError(input_shiftday IN VARCHAR2 := to_char(SYSDATE,'yyyymmdd')) IS   
     v_SQL                             VARCHAR2(2000);
		 v_AlertSql                        VARCHAR2(2000);
     TYPE cursor_DynamicSelect         IS REF CURSOR;
     v_ResultList                      cursor_DynamicSelect;  
		 v_AlertList                       cursor_DynamicSelect;
		 
		 --????Log
		 v_RunLog                          type_joblog;
		 v_BeginTime                       NUMBER;
     v_EndTime                         NUMBER;
		 v_ProcessCounter                  INT;
				
		 --tblalerterror???
		 v_AlertType                       VARCHAR2(40);
		 v_Description                     VARCHAR2(100);
		 v_MailSubject                     VARCHAR2(150);
		 v_MailContent                     VARCHAR2(2000);
		 v_ItemSequence                    VARCHAR2(40);
		 v_SubItemSequence                 VARCHAR2(40);
		 v_ItemType                        VARCHAR2(40);
		 v_ECode                           VARCHAR2(40);
		 v_ECodeDesc                       VARCHAR2(100);
     v_TimeDimension                   VARCHAR2(40);
		 v_LineDevision                    VARCHAR2(1);
		 v_AlertValue                      NUMBER;
		 v_GenerateNotice                  VARCHAR2(1);
		 v_SendMail                        VARCHAR2(1);
		 v_LinePause                       VARCHAR2(1);
		 
		 --???????
		 v_mcode                           VARCHAR2(40);
     v_itemCodeAndDesc                 VARCHAR2(200);
		 v_mmodelcode                      VARCHAR2(40);
		 v_mgroup                          VARCHAR2(40);
		 v_ShiftCode                       VARCHAR2(40);
		 v_shiftday                        NUMBER;
		 v_bigsscode                       VARCHAR2(40);
		 v_sumCount                        NUMBER;
		 
		 --?????????????1?
		 v_AlertedSql                      VARCHAR2(2000);
		 v_AlertedList                     cursor_DynamicSelect;
		 v_alertedCount                    NUMBER;
		 
		 --2??‡˙Í∑±Ì
		 v_NoticeError                     type_notice_error;
		 --2˙È˙Ì??????
		 v_AlertNotice                     type_alert_notice;
		 --2˙È˙Mail????
		 v_AlertMail                       type_mail;
		 v_itemFirstClass                  VARCHAR2(40);
		 v_itemSecondClass                 VARCHAR2(40);
		 v_MailContent_Formated            VARCHAR2(2000);
		 v_MailContent_Replace_List        type_varchar_list;
		 
    BEGIN
		  --3????Log
		  v_RunLog := NEW type_joblog();
      v_RunLog.jobid := 'AlertError';
      v_RunLog.startdatetime := SYSDATE;
      v_BeginTime := DBMS_UTILITY.get_time;
		  v_ProcessCounter := 0;
			
			--???????????
			v_SQL := 'SELECT b.alerttype, b.description, b.mailsubject, b.mailcontent,'
			      || '       a.itemsequence, a.subitemsequence, a.itemtype, a.ecode,'
						|| '       nvl(c.ecdesc, '' '') ecdesc, a.timedimension, a.linedivision,'
						|| '       a.alertvalue, a.generatenotice, a.sendmail, a.linepause'
            || '  FROM tblalerterror a, tblalertitem b, tblec c'
            || ' WHERE a.ecode = c.ecode(+) AND a.itemsequence = b.itemsequence'
						|| ' ORDER BY SubItemSequence';

      OPEN v_ResultList FOR v_SQL;        
      LOOP
          FETCH v_ResultList INTO 
					  v_AlertType,v_Description,v_MailSubject,v_MailContent,v_ItemSequence,
						v_SubItemSequence,v_ItemType,v_ECode,v_ECodeDesc,v_TimeDimension,
						v_LineDevision,v_AlertValue,v_GenerateNotice,v_SendMail,v_LinePause;
          EXIT WHEN v_ResultList%NOTFOUND;
					
					IF v_TimeDimension = 'byshiftcode' THEN
						v_AlertSql := 'SELECT mcode, mmodelcode, mgroup, ShiftCode, shiftday, ';
						
						IF v_LineDevision = 'Y' THEN
							v_AlertSql := v_AlertSql ||'    bigsscode,';
						ELSE
						  v_AlertSql := v_AlertSql ||'    '' '','; --?a????????
						END IF;
						
						v_AlertSql := v_AlertSql ||'    SUM (COUNTTEMP) as sumCount';
						v_AlertSql := v_AlertSql || '    FROM (SELECT c.mcode, c.mmodelcode, b.shiftcode, b.shiftday,';
						v_AlertSql := v_AlertSql || '                 1 AS COUNTTEMP, d.bigsscode, c.mgroup';
					  v_AlertSql := v_AlertSql || '            FROM tbltserrorcode a, tblts b, tblmaterial c, tblss d';
						v_AlertSql := v_AlertSql || '           WHERE a.tsid = b.tsid';
						v_AlertSql := v_AlertSql || '             AND a.itemcode = c.mcode';
						v_AlertSql := v_AlertSql || '             AND b.frmsscode = d.sscode';
						v_AlertSql := v_AlertSql || '             AND b.shiftday between ' || to_char(to_date(input_shiftday,'yyyymmdd')-1,'yyyymmdd');
						v_AlertSql := v_AlertSql || '             AND ' || to_char(to_date(input_shiftday,'yyyymmdd'),'yyyymmdd');
						v_AlertSql := v_AlertSql || '             AND a.ecode = '''|| v_ECode ||''' ';
						v_AlertSql := v_AlertSql || '             AND c.mtype = '''|| v_ItemType ||''')';
						v_AlertSql := v_AlertSql || ' GROUP BY mcode, mmodelcode, mgroup, ShiftCode, shiftday';
						
						IF v_LineDevision = 'Y' THEN
								v_AlertSql := v_AlertSql || ', bigsscode';
						END IF;
						
						v_AlertSql := v_AlertSql || ' HAVING SUM(COUNTTEMP)>=' || v_AlertValue;
						
					ELSE 
					  IF v_TimeDimension = 'byshiftday' THEN
							v_AlertSql := 'SELECT mcode, mmodelcode, mgroup,'' '', shiftday, ';--?ÛË??????,???????
							
							IF v_LineDevision = 'Y' THEN
								v_AlertSql := v_AlertSql ||'    bigsscode,';
							ELSE
						    v_AlertSql := v_AlertSql ||'    '' '','; --?a????????
							END IF;
							
							v_AlertSql := v_AlertSql ||'    SUM (COUNTTEMP) as sumCount';
							v_AlertSql := v_AlertSql || '    FROM (SELECT c.mcode, c.mmodelcode, b.shiftcode, b.shiftday,';
							v_AlertSql := v_AlertSql || '                 1 AS COUNTTEMP, d.bigsscode, c.mgroup';
							v_AlertSql := v_AlertSql || '            FROM tbltserrorcode a, tblts b, tblmaterial c, tblss d';
							v_AlertSql := v_AlertSql || '           WHERE a.tsid = b.tsid';
							v_AlertSql := v_AlertSql || '             AND a.itemcode = c.mcode';
							v_AlertSql := v_AlertSql || '             AND b.frmsscode = d.sscode';
							v_AlertSql := v_AlertSql || '             AND b.shiftday between ' || to_char(to_date(input_shiftday,'yyyymmdd')-1,'yyyymmdd');
						  v_AlertSql := v_AlertSql || '             AND ' || to_char(to_date(input_shiftday,'yyyymmdd'),'yyyymmdd');
							v_AlertSql := v_AlertSql || '             AND a.ecode = '''|| v_ECode ||''' ';
							v_AlertSql := v_AlertSql || '             AND c.mtype = '''|| v_ItemType ||''')';
							v_AlertSql := v_AlertSql || ' GROUP BY mcode, mmodelcode, mgroup, shiftday';
							
							IF v_LineDevision = 'Y' THEN
									v_AlertSql := v_AlertSql || ', bigsscode';
							END IF;
							
							v_AlertSql := v_AlertSql || ' HAVING SUM(COUNTTEMP)>=' || v_AlertValue;
							
						END IF;
					END IF;
					
					--????ËÚa??????????
					OPEN v_AlertList FOR v_AlertSql;
					LOOP
					  <<next_loop>>
						FETCH v_AlertList INTO 
							v_mcode,v_mmodelcode,v_mgroup,v_ShiftCode,v_shiftday,v_bigsscode,v_sumCount;
						EXIT WHEN v_AlertList%NOTFOUND;
							
						--1.?????????????1?
						v_AlertedSql := ' ';
						v_AlertedSql := v_AlertedSql || 'SELECT COUNT(*) AS sumCount';
						v_AlertedSql := v_AlertedSql || '  FROM tblnoticeerror a';
						v_AlertedSql := v_AlertedSql || ' WHERE a.itemsequence = ''' || v_ItemSequence || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.subitemsequence = ''' || v_SubItemSequence ||''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.itemcode = ''' || v_mcode || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.ecode = ''' || v_ECode || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.shiftday = ' || v_shiftday;
						
						IF v_TimeDimension = 'byshiftcode' THEN
						  v_AlertedSql := v_AlertedSql || '   AND a.ShiftCode = ''' || v_ShiftCode || ''' ';
						ELSE
						  v_AlertedSql := v_AlertedSql || '   AND a.ShiftCode ='' '' ';
						END IF;
						
						IF v_LineDevision = 'Y' THEN
						  v_AlertedSql := v_AlertedSql || '   AND a.bigsscode = ''' || v_bigsscode || ''' ';
						ELSE
						  v_AlertedSql := v_AlertedSql || '   AND a.bigsscode ='' '' ';
						END IF;
						
						OPEN v_AlertedList FOR v_AlertedSql;
						FETCH v_AlertedList INTO v_alertedCount;
						CLOSE v_AlertedList;
						
						--?1????????1????????±Í
						IF v_alertedCount > 0 THEN
						  GOTO next_loop; 
						END IF;

						--2.2??‡˙Í∑±Ì
						v_NoticeError                 := NEW type_notice_error();
						v_NoticeError.SERIAL          := 0;
						v_NoticeError.ITEMSEQUENCE    := v_ItemSequence;
						v_NoticeError.SUBITEMSEQUENCE := v_SubItemSequence;
						v_NoticeError.ITEMCODE        := v_mcode;
						v_NoticeError.ECODE           := v_ECode;
						v_NoticeError.SHIFTCODE       := v_ShiftCode;
						v_NoticeError.SHIFTDAY        := v_shiftday;
						v_NoticeError.BIGSSCODE       := v_bigsscode;
						v_NoticeError.MUSER           := c_MaintainUser;
						v_NoticeError.MDATE           := to_char(SYSDATE,'yyyymmdd');
						v_NoticeError.MTIME           := to_char(SYSDATE,'HH24MISS');
						v_NoticeError.WRITE();
						
						--2.1?????Mail Content
            SELECT mcode || ' - ' || mdesc INTO v_itemCodeAndDesc FROM tblmaterial WHERE mcode=v_mcode;
            
						v_MailContent_Replace_List := NEW type_varchar_list();
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := to_char(sysdate,'yyyy"??mm"??"dd"?" HH24:MI:SS');
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_itemCodeAndDesc;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_bigsscode;
						v_MailContent_Replace_List.EXTEND;
						SELECT v_ECode || '-' ||decode(v_ECodeDesc,' ',v_ECode,v_ECodeDesc)  --ÚÚ?aDecodeo????????˙SQL??,?˘Ú??a?˘Í1?
						  INTO v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT)   
							FROM dual;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_sumCount;
						
						GetFirstSecondClass(v_mgroup,v_itemFirstClass,v_itemSecondClass);
						v_MailContent_Formated := FormatMailContent(v_MailContent,c_mailtype_alerterror,v_MailContent_Replace_List);
						
						--3.2˙È˙Ì??????
						IF v_GenerateNotice = 'Y' THEN
						  v_AlertNotice                 := NEW type_alert_notice();
							v_AlertNotice.SERIAL          := 0;
							v_AlertNotice.ITEMSEQUENCE    := v_ItemSequence;
							v_AlertNotice.DESCRIPTION     := v_Description;
							v_AlertNotice.SUBITEMSEQUENCE := v_SubItemSequence;
							v_AlertNotice.NOTICESERIAL    := v_NoticeError.GetEntitySerial();
							v_AlertNotice.ALERTTYPE       := v_AlertType;
							v_AlertNotice.STATUS          := c_DealStatus;
							v_AlertNotice.MOLIST          := ' ';
							IF v_LinePause = 'Y' THEN
							  v_AlertNotice.MOLIST        := GetMOListToPause(v_mcode,v_shiftday,v_ShiftCode,v_bigsscode);
							END IF;
							
							v_AlertNotice.NOTICECONTENT   := v_MailContent_Formated;
							v_AlertNotice.ANALYSISREASON  := ' ';
							v_AlertNotice.DEALMETHODS     := ' ';
							v_AlertNotice.NOTICEDATE      := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.NOTICETIME      := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.DEALUSER        := ' ';
							v_AlertNotice.DEALDATE        := 0;
							v_AlertNotice.DEALTIME        := 0;
							v_AlertNotice.MUSER           := c_MaintainUser;
							v_AlertNotice.MDATE           := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.MTIME           := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.WRITE();
							
							--3.1.?????‡Ì
							IF v_LinePause = 'Y' THEN
							  LinePauseByMO(v_AlertNotice.MOLIST);
							END IF;
							
						END IF;
						
						--5.2˙È˙Mail????
						IF v_SendMail = 'Y' THEN
						  v_AlertMail := NEW type_mail();
							v_AlertMail.SERIAL       := 0;
							v_AlertMail.MAILSUBJECT  := v_MailSubject;
							v_AlertMail.RECIPIENTS   := GetRecipients(v_ItemSequence,v_bigsscode,v_itemFirstClass,v_itemSecondClass);
							v_AlertMail.MAILCONTENT  := v_MailContent_Formated;
							v_AlertMail.ISSEND       := 'N';
							v_AlertMail.SENDTIMES    := 0;
							v_AlertMail.SENDRESULT   := ' ';
							v_AlertMail.ERRORMESSAGE := ' ';
							v_AlertMail.MUSER        := c_MaintainUser;
							v_AlertMail.MDATE        := to_char(SYSDATE,'yyyymmdd');
							v_AlertMail.MTIME        := to_char(SYSDATE,'HH24MISS');
							v_AlertMail.EATTRIBUTE1  := v_ItemSequence;
							v_AlertMail.EATTRIBUTE2  := v_AlertType;
							v_AlertMail.EATTRIBUTE3  := v_SubItemSequence;
							
							--?1???2???????,?????ÌMail.
							IF v_AlertMail.RECIPIENTS <> ' ' THEN
							  v_AlertMail.WRITE();
							END IF;
						END IF;
						
					END LOOP;
					CLOSE v_AlertList;
          v_ProcessCounter := v_ProcessCounter + 1;
      END LOOP;
      CLOSE v_ResultList;        
      COMMIT;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'OK';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := '';
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();
			
      RETURN;
    EXCEPTION
      WHEN OTHERS THEN
			ROLLBACK;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'FAIL';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := SQLERRM;
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();

      RETURN;
  END AlertError;
	
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : 2????ÚÚ????
	------------------------------------------------
  PROCEDURE AlertErrorCode(input_shiftday IN VARCHAR2 := to_char(SYSDATE,'yyyymmdd')) IS
     v_SQL                             VARCHAR2(2000);
		 v_AlertSql                        VARCHAR2(2000);
     TYPE cursor_DynamicSelect         IS REF CURSOR;
     v_ResultList                      cursor_DynamicSelect;  
		 v_AlertList                       cursor_DynamicSelect;
		 
		 --????Log
		 v_RunLog                          type_joblog;
		 v_BeginTime                       NUMBER;
     v_EndTime                         NUMBER;
		 v_ProcessCounter                  INT;
				
		 --tblalerterror???
		 v_AlertType                       VARCHAR2(40);
		 v_Description                     VARCHAR2(100);
		 v_MailSubject                     VARCHAR2(150);
		 v_MailContent                     VARCHAR2(2000);
		 v_ItemSequence                    VARCHAR2(40);
		 v_SubItemSequence                 VARCHAR2(40);
		 v_ItemType                        VARCHAR2(40);
		 v_ECSCode                         VARCHAR2(40);
		 v_ECSCodeDesc                     VARCHAR2(100);
     v_TimeDimension                   VARCHAR2(40);
		 v_LineDevision                    VARCHAR2(1);
		 v_AlertValue                      NUMBER;
		 v_GenerateNotice                  VARCHAR2(1);
		 v_SendMail                        VARCHAR2(1);
		 v_LinePause                       VARCHAR2(1);
		 
		 --???????
		 v_eloc                            VARCHAR2(40);
		 v_mcode                           VARCHAR2(40);
     v_itemCodeAndDesc                 VARCHAR2(200);
		 v_mmodelcode                      VARCHAR2(40);
		 v_mgroup                          VARCHAR2(40);
		 v_ShiftCode                       VARCHAR2(40);
		 v_shiftday                        NUMBER;
		 v_bigsscode                       VARCHAR2(40);
		 v_sumCount                        NUMBER;
		 
		 --?????????????1?
		 v_AlertedSql                      VARCHAR2(2000);
		 v_AlertedList                     cursor_DynamicSelect;
		 v_alertedCount                    NUMBER;
		 
		 --2??‡˙Í∑±Ì
		 v_NoticeErrorCode                 type_notice_error_code;
		 --2˙È˙Ì??????
		 v_AlertNotice                     type_alert_notice;
		 --2˙È˙Mail????
		 v_AlertMail                       type_mail;
		 v_itemFirstClass                  VARCHAR2(40);
		 v_itemSecondClass                 VARCHAR2(40);
		 v_MailContent_Formated            VARCHAR2(2000);
		 v_MailContent_Replace_List        type_varchar_list;
		 
    BEGIN
		  --3????Log
		  v_RunLog := NEW type_joblog();
      v_RunLog.jobid := 'AlertErrorCode';
      v_RunLog.startdatetime := SYSDATE;
      v_BeginTime := DBMS_UTILITY.get_time;
		  v_ProcessCounter := 0;
			
			--???????????
			v_SQL := 'SELECT b.alerttype, b.description, b.mailsubject, b.mailcontent,'
			      || '       a.itemsequence, a.subitemsequence, a.itemtype, a.ecscode,'
						|| '       nvl(c.ecsdesc, '' '') ecsdesc, a.timedimension, a.linedivision,'
						|| '       a.alertvalue, a.generatenotice, a.sendmail, a.linepause'
            || '  FROM tblalerterrorcode a, tblalertitem b, tblecs c'
            || ' WHERE a.ecscode = c.ecscode(+) AND a.itemsequence = b.itemsequence'
						|| ' ORDER BY SubItemSequence';

      OPEN v_ResultList FOR v_SQL;        
      LOOP
          FETCH v_ResultList INTO 
					  v_AlertType,v_Description,v_MailSubject,v_MailContent,v_ItemSequence,
						v_SubItemSequence,v_ItemType,v_ECSCode,v_ECSCodeDesc,v_TimeDimension,
						v_LineDevision,v_AlertValue,v_GenerateNotice,v_SendMail,v_LinePause;
          EXIT WHEN v_ResultList%NOTFOUND;
					
					IF v_TimeDimension = 'byshiftcode' THEN
						v_AlertSql := 'SELECT mcode, mmodelcode, mgroup, eloc, ShiftCode, shiftday, ';
						
						IF v_LineDevision = 'Y' THEN
							v_AlertSql := v_AlertSql ||'    bigsscode,';
						ELSE
						  v_AlertSql := v_AlertSql ||'    '' '','; --?a????????
						END IF;
						
						v_AlertSql := v_AlertSql ||'    SUM (COUNTTEMP) as sumCount';
						v_AlertSql := v_AlertSql || '    FROM (SELECT NVL(a.eloc, '' '') AS eloc, c.mcode, c.mmodelcode, b.shiftcode, b.shiftday,';
						v_AlertSql := v_AlertSql || '                 1 AS COUNTTEMP, d.bigsscode, c.mgroup';
					  v_AlertSql := v_AlertSql || '            FROM tbltserrorcause e, tbltserrorcause2loc a, tblts b, tblmaterial c, tblss d';
						v_AlertSql := v_AlertSql || '           WHERE e.tsid = b.tsid';
						v_AlertSql := v_AlertSql || '             AND e.itemcode = c.mcode';
						v_AlertSql := v_AlertSql || '             AND b.frmsscode = d.sscode';
						v_AlertSql := v_AlertSql || '             AND b.shiftday between ' || to_char(to_date(input_shiftday,'yyyymmdd')-1,'yyyymmdd');
						v_AlertSql := v_AlertSql || '             AND ' || to_char(to_date(input_shiftday,'yyyymmdd'),'yyyymmdd');
						v_AlertSql := v_AlertSql || '             AND e.ecscode = a.ecscode(+) ';
						v_AlertSql := v_AlertSql || '             AND e.tsid = a.tsid(+) ';
						v_AlertSql := v_AlertSql || '             AND e.ecscode = '''|| v_ECSCode ||''' ';
						v_AlertSql := v_AlertSql || '             AND c.mtype = '''|| v_ItemType ||''')';
						v_AlertSql := v_AlertSql || ' GROUP BY mcode, mmodelcode, mgroup, eloc, ShiftCode, shiftday';
						
						IF v_LineDevision = 'Y' THEN
								v_AlertSql := v_AlertSql || ', bigsscode';
						END IF;
						
						v_AlertSql := v_AlertSql || ' HAVING SUM(COUNTTEMP)>=' || v_AlertValue;
						
					ELSE 
					  IF v_TimeDimension = 'byshiftday' THEN
							v_AlertSql := 'SELECT mcode, mmodelcode, mgroup, eloc, '' '', shiftday, ';--?ÛË??????,???????
							
							IF v_LineDevision = 'Y' THEN
								v_AlertSql := v_AlertSql ||'    bigsscode,';
							ELSE
						    v_AlertSql := v_AlertSql ||'    '' '','; --?a????????
							END IF;
							
							v_AlertSql := v_AlertSql ||'    SUM (COUNTTEMP) as sumCount';
							v_AlertSql := v_AlertSql || '    FROM (SELECT NVL(a.eloc, '' '') AS eloc, c.mcode, c.mmodelcode, b.shiftcode, b.shiftday,';
							v_AlertSql := v_AlertSql || '                 1 AS COUNTTEMP, d.bigsscode, c.mgroup';
							v_AlertSql := v_AlertSql || '            FROM tbltserrorcause e, tbltserrorcause2loc a, tblts b, tblmaterial c, tblss d';
							v_AlertSql := v_AlertSql || '           WHERE e.tsid = b.tsid';
							v_AlertSql := v_AlertSql || '             AND e.itemcode = c.mcode';
							v_AlertSql := v_AlertSql || '             AND b.frmsscode = d.sscode';
							v_AlertSql := v_AlertSql || '             AND b.shiftday between ' || to_char(to_date(input_shiftday,'yyyymmdd')-1,'yyyymmdd');
						    v_AlertSql := v_AlertSql || '             AND ' || to_char(to_date(input_shiftday,'yyyymmdd'),'yyyymmdd');
						  	v_AlertSql := v_AlertSql || '             AND e.ecscode = a.ecscode(+) ';
						    v_AlertSql := v_AlertSql || '             AND e.tsid = a.tsid(+) ';
							v_AlertSql := v_AlertSql || '             AND e.ecscode = '''|| v_ECSCode ||''' ';
							v_AlertSql := v_AlertSql || '             AND c.mtype = '''|| v_ItemType ||''')';
							v_AlertSql := v_AlertSql || ' GROUP BY mcode, mmodelcode, mgroup, eloc, shiftday';
							
							IF v_LineDevision = 'Y' THEN
									v_AlertSql := v_AlertSql || ', bigsscode';
							END IF;
							
							v_AlertSql := v_AlertSql || ' HAVING SUM(COUNTTEMP)>=' || v_AlertValue;
							
						END IF;
					END IF;
					
					--????ËÚa??????????
					OPEN v_AlertList FOR v_AlertSql;
					LOOP
					  <<next_loop>>
						FETCH v_AlertList INTO 
							v_mcode,v_mmodelcode,v_mgroup,v_eloc,v_ShiftCode,v_shiftday,v_bigsscode,v_sumCount;
						EXIT WHEN v_AlertList%NOTFOUND;
							
						--1.?????????????1?
						v_AlertedSql := ' ';
						v_AlertedSql := v_AlertedSql || 'SELECT COUNT(*) AS sumCount';
						v_AlertedSql := v_AlertedSql || '  FROM tblnoticeerrorcode a';
						v_AlertedSql := v_AlertedSql || ' WHERE a.itemsequence = ''' || v_ItemSequence || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.subitemsequence = ''' || v_SubItemSequence ||''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.itemcode = ''' || v_mcode || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.ecscode = ''' || v_ECSCode || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.shiftday = ' || v_shiftday;
						v_AlertedSql := v_AlertedSql || '   AND a.location = ''' || v_eloc || ''' ';
						
						IF v_TimeDimension = 'byshiftcode' THEN
						  v_AlertedSql := v_AlertedSql || '   AND a.ShiftCode = ''' || v_ShiftCode || ''' ';
						ELSE
						  v_AlertedSql := v_AlertedSql || '   AND a.ShiftCode ='' '' ';
						END IF;
						
						IF v_LineDevision = 'Y' THEN
						  v_AlertedSql := v_AlertedSql || '   AND a.bigsscode = ''' || v_bigsscode || ''' ';
						ELSE
						  v_AlertedSql := v_AlertedSql || '   AND a.bigsscode ='' '' ';
						END IF;
						
						OPEN v_AlertedList FOR v_AlertedSql;
						FETCH v_AlertedList INTO v_alertedCount;
						CLOSE v_AlertedList;
						
						--?1????????1????????±Í
						IF v_alertedCount > 0 THEN
						  GOTO next_loop; 
						END IF;

						--2.2??‡˙Í∑±Ì
						v_NoticeErrorCode                 := NEW type_notice_error_code();
						v_NoticeErrorCode.SERIAL          := 0;
						v_NoticeErrorCode.ITEMSEQUENCE    := v_ItemSequence;
						v_NoticeErrorCode.SUBITEMSEQUENCE := v_SubItemSequence;
						v_NoticeErrorCode.ITEMCODE        := v_mcode;
						v_NoticeErrorCode.ECSCODE         := v_ECSCode;
						v_NoticeErrorCode.LOCATION        := v_eloc;
						v_NoticeErrorCode.SHIFTCODE       := v_ShiftCode;
						v_NoticeErrorCode.SHIFTDAY        := v_shiftday;
						v_NoticeErrorCode.BIGSSCODE       := v_bigsscode;
						v_NoticeErrorCode.MUSER           := c_MaintainUser;
						v_NoticeErrorCode.MDATE           := to_char(SYSDATE,'yyyymmdd');
						v_NoticeErrorCode.MTIME           := to_char(SYSDATE,'HH24MISS');
						v_NoticeErrorCode.WRITE();
						
						--2.1?????Mail Content
             SELECT mcode || ' - ' || mdesc INTO v_itemCodeAndDesc FROM tblmaterial WHERE mcode=v_mcode;
             
						v_MailContent_Replace_List := NEW type_varchar_list();
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := to_char(sysdate,'yyyy"??mm"??"dd"?" HH24:MI:SS');
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_itemCodeAndDesc;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_bigsscode;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_eloc;
						v_MailContent_Replace_List.EXTEND;
						SELECT v_ECSCode || '-' ||decode(v_ECSCodeDesc,' ',v_ECSCode,v_ECSCodeDesc)  --ÚÚ?aDecodeo????????˙SQL??,?˘Ú??a?˘Í1?
						  INTO v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT)   
							FROM dual;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_sumCount;
						
						GetFirstSecondClass(v_mgroup,v_itemFirstClass,v_itemSecondClass);
						v_MailContent_Formated := FormatMailContent(v_MailContent,c_mailtype_errorcode,v_MailContent_Replace_List);
						
						--3.2˙È˙Ì??????
						IF v_GenerateNotice = 'Y' THEN
						  v_AlertNotice                 := NEW type_alert_notice();
							v_AlertNotice.SERIAL          := 0;
							v_AlertNotice.ITEMSEQUENCE    := v_ItemSequence;
							v_AlertNotice.DESCRIPTION     := v_Description;
							v_AlertNotice.SUBITEMSEQUENCE := v_SubItemSequence;
							v_AlertNotice.NOTICESERIAL    := v_NoticeErrorCode.GetEntitySerial();
							v_AlertNotice.ALERTTYPE       := v_AlertType;
							v_AlertNotice.STATUS          := c_DealStatus;
							v_AlertNotice.MOLIST          := ' ';
							IF v_LinePause = 'Y' THEN
							  v_AlertNotice.MOLIST        := GetMOListToPause(v_mcode,v_shiftday,v_ShiftCode,v_bigsscode);
							END IF;
							
							v_AlertNotice.NOTICECONTENT   := v_MailContent_Formated;
							v_AlertNotice.ANALYSISREASON  := ' ';
							v_AlertNotice.DEALMETHODS     := ' ';
							v_AlertNotice.NOTICEDATE      := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.NOTICETIME      := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.DEALUSER        := ' ';
							v_AlertNotice.DEALDATE        := 0;
							v_AlertNotice.DEALTIME        := 0;
							v_AlertNotice.MUSER           := c_MaintainUser;
							v_AlertNotice.MDATE           := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.MTIME           := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.WRITE();
							
							--3.1.?????‡Ì
							IF v_LinePause = 'Y' THEN
							  LinePauseByMO(v_AlertNotice.MOLIST);
							END IF;
							
						END IF;
						
						--5.2˙È˙Mail????
						IF v_SendMail = 'Y' THEN
						  v_AlertMail := NEW type_mail();
							v_AlertMail.SERIAL       := 0;
							v_AlertMail.MAILSUBJECT  := v_MailSubject;
							v_AlertMail.RECIPIENTS   := GetRecipients(v_ItemSequence,v_bigsscode,v_itemFirstClass,v_itemSecondClass);
							v_AlertMail.MAILCONTENT  := v_MailContent_Formated;
							v_AlertMail.ISSEND       := 'N';
							v_AlertMail.SENDTIMES    := 0;
							v_AlertMail.SENDRESULT   := ' ';
							v_AlertMail.ERRORMESSAGE := ' ';
							v_AlertMail.MUSER        := c_MaintainUser;
							v_AlertMail.MDATE        := to_char(SYSDATE,'yyyymmdd');
							v_AlertMail.MTIME        := to_char(SYSDATE,'HH24MISS');
							v_AlertMail.EATTRIBUTE1  := v_ItemSequence;
							v_AlertMail.EATTRIBUTE2  := v_AlertType;
							v_AlertMail.EATTRIBUTE3  := v_SubItemSequence;
							
							--?1???2???????,?????ÌMail.
							IF v_AlertMail.RECIPIENTS <> ' ' THEN
							  v_AlertMail.WRITE();
							END IF;
						END IF;
						
					END LOOP;
					CLOSE v_AlertList;
          v_ProcessCounter := v_ProcessCounter + 1;
      END LOOP;
      CLOSE v_ResultList;        
      COMMIT;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'OK';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := '';
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();
			
      RETURN;
    EXCEPTION
      WHEN OTHERS THEN
			ROLLBACK;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'FAIL';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := SQLERRM;
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();

      RETURN;
  END AlertErrorCode;
  
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : OQC3????????????
	------------------------------------------------
  PROCEDURE AlertOQCNG IS  
     v_SQL                             VARCHAR2(2000);
		 v_AlertSql                        VARCHAR2(2000);
     TYPE cursor_DynamicSelect         IS REF CURSOR;
     v_ResultList                      cursor_DynamicSelect;  
		 v_AlertList                       cursor_DynamicSelect;
		 
		 --?a?????Í±??
		 v_startDate                       NUMBER := to_char(SYSDATE,'yyyymmdd');
		 v_startTime                       NUMBER := to_char(SYSDATE,'HH24MISS');
		 
		 --????Log
		 v_RunLog                          type_joblog;
		 v_BeginTime                       NUMBER;
     v_EndTime                         NUMBER;
		 v_ProcessCounter                  INT;
				
		 --tblalerterror???
		 v_AlertType                       VARCHAR2(40);
		 v_Description                     VARCHAR2(100);
		 v_MailSubject                     VARCHAR2(150);
		 v_MailContent                     VARCHAR2(2000);
		 v_ItemSequence                    VARCHAR2(40);
		 v_SubItemSequence                 VARCHAR2(40);
		 v_ItemType                        VARCHAR2(40);
		 v_ECode                           VARCHAR2(40);
		 v_ECodeDesc                       VARCHAR2(100);
		 v_AlertDate                       NUMBER;
		 v_AlertTime                       NUMBER;
		 v_AlertValue                      NUMBER;
		 v_GenerateNotice                  VARCHAR2(1);
		 v_SendMail                        VARCHAR2(1);
		 
		 --???????
		 v_mcode                           VARCHAR2(40);
     v_itemCodeAndDesc                 VARCHAR2(200);
		 v_mgroup                          VARCHAR2(40);
		 v_sumCount                        NUMBER;
		 
		 --2˙È˙Ì??????
		 v_AlertNotice                     type_alert_notice;
		 --2˙È˙Mail????
		 v_AlertMail                       type_mail;
		 v_itemFirstClass                  VARCHAR2(40);
		 v_itemSecondClass                 VARCHAR2(40);
		 v_MailContent_Formated            VARCHAR2(2000);
		 v_MailContent_Replace_List        type_varchar_list;
		 
    BEGIN
		  --3????Log
		  v_RunLog := NEW type_joblog();
      v_RunLog.jobid := 'AlertOQCNG';
      v_RunLog.startdatetime := SYSDATE;
      v_BeginTime := DBMS_UTILITY.get_time;
		  v_ProcessCounter := 0;
			
			--???????????
			v_SQL := 'SELECT b.alerttype, b.description, b.mailsubject, b.mailcontent,'
			      || '       a.itemsequence, a.subitemsequence, a.itemtype, a.ecode,'
						|| '       nvl(c.ecdesc, '' '') ecdesc,a.startdate,a.starttime, '
						|| '       a.alertvalue, a.generatenotice, a.sendmail'
            || '  FROM tblalertoqcng a, tblalertitem b, tblec c'
            || ' WHERE a.ecode = c.ecode(+) AND a.itemsequence = b.itemsequence'
						|| ' ORDER BY SubItemSequence';

      OPEN v_ResultList FOR v_SQL;        
      LOOP
          FETCH v_ResultList INTO 
					  v_AlertType,v_Description,v_MailSubject,v_MailContent,v_ItemSequence,
						v_SubItemSequence,v_ItemType,v_ECode,v_ECodeDesc,v_AlertDate,v_AlertTime,
						v_AlertValue,v_GenerateNotice,v_SendMail;
          EXIT WHEN v_ResultList%NOTFOUND;
					
					--???????‡·?a.frmopcode='AQ'1??????ÚÚ?oÛÈÛ˙?????3???ËÚa'AQ' 1???˙È??NG?????2???????????
					v_AlertSql := 'SELECT mcode, mgroup, ';
					v_AlertSql := v_AlertSql ||'    SUM (COUNTTEMP) as sumCount';
					v_AlertSql := v_AlertSql || '    FROM (SELECT c.mcode,c.mgroup,1 AS COUNTTEMP';
					v_AlertSql := v_AlertSql || '            FROM tblts a, tbltserrorcode b, tblmaterial c';
					v_AlertSql := v_AlertSql || '           WHERE a.frmopcode = ''AQ'' ';
					v_AlertSql := v_AlertSql || '             AND a.frmdate*1000000 + a.frmtime >= ' || (v_AlertDate*1000000+v_AlertTime);
					v_AlertSql := v_AlertSql || '             AND a.tsid = b.tsid';
					v_AlertSql := v_AlertSql || '             AND a.itemcode = c.mcode ';
					v_AlertSql := v_AlertSql || '             AND b.ecode = '''|| v_ECode ||''' ';
					v_AlertSql := v_AlertSql || '             AND c.mtype = '''|| v_ItemType ||''')';
					v_AlertSql := v_AlertSql || ' GROUP BY mcode, mgroup';

					v_AlertSql := v_AlertSql || ' HAVING SUM(COUNTTEMP)>=' || v_AlertValue;
					
					--????ËÚa??????????
					OPEN v_AlertList FOR v_AlertSql;
					LOOP
						FETCH v_AlertList INTO 
							v_mcode,v_mgroup,v_sumCount;
						EXIT WHEN v_AlertList%NOTFOUND;
						
						--2.1?????Mail Content
            SELECT mcode || ' - ' || mdesc INTO v_itemCodeAndDesc FROM tblmaterial WHERE mcode=v_mcode;
            
						v_MailContent_Replace_List := NEW type_varchar_list();
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := to_char(sysdate,'yyyy"??mm"??"dd"?" HH24:MI:SS');
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_itemCodeAndDesc;
						v_MailContent_Replace_List.EXTEND;
						SELECT v_ECode || '-' ||decode(v_ECodeDesc,' ',v_ECode,v_ECodeDesc)  --ÚÚ?aDecodeo????????˙SQL??,?˘Ú??a?˘Í1?
						  INTO v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT)   
							FROM dual;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_sumCount;
						
						GetFirstSecondClass(v_mgroup,v_itemFirstClass,v_itemSecondClass);
						v_MailContent_Formated := FormatMailContent(v_MailContent,c_mailtype_OQCNG,v_MailContent_Replace_List);
						
						--3.2˙È˙Ì??????
						IF v_GenerateNotice = 'Y' THEN
						  v_AlertNotice                 := NEW type_alert_notice();
							v_AlertNotice.SERIAL          := 0;
							v_AlertNotice.ITEMSEQUENCE    := v_ItemSequence;
							v_AlertNotice.DESCRIPTION     := v_Description;
							v_AlertNotice.SUBITEMSEQUENCE := v_SubItemSequence;
							v_AlertNotice.NOTICESERIAL    := 0;
							v_AlertNotice.ALERTTYPE       := v_AlertType;
							v_AlertNotice.STATUS          := c_DealStatus;
							v_AlertNotice.MOLIST          := ' ';
							v_AlertNotice.NOTICECONTENT   := v_MailContent_Formated;
							v_AlertNotice.ANALYSISREASON  := ' ';
							v_AlertNotice.DEALMETHODS     := ' ';
							v_AlertNotice.NOTICEDATE      := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.NOTICETIME      := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.DEALUSER        := ' ';
							v_AlertNotice.DEALDATE        := 0;
							v_AlertNotice.DEALTIME        := 0;
							v_AlertNotice.MUSER           := c_MaintainUser;
							v_AlertNotice.MDATE           := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.MTIME           := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.WRITE();
						END IF;
						
						--5.2˙È˙Mail????
						IF v_SendMail = 'Y' THEN
						  v_AlertMail := NEW type_mail();
							v_AlertMail.SERIAL       := 0;
							v_AlertMail.MAILSUBJECT  := v_MailSubject;
							v_AlertMail.RECIPIENTS   := GetRecipients(v_ItemSequence,' ',v_itemFirstClass,v_itemSecondClass);
							v_AlertMail.MAILCONTENT  := v_MailContent_Formated;
							v_AlertMail.ISSEND       := 'N';
							v_AlertMail.SENDTIMES    := 0;
							v_AlertMail.SENDRESULT   := ' ';
							v_AlertMail.ERRORMESSAGE := ' ';
							v_AlertMail.MUSER        := c_MaintainUser;
							v_AlertMail.MDATE        := to_char(SYSDATE,'yyyymmdd');
							v_AlertMail.MTIME        := to_char(SYSDATE,'HH24MISS');
							v_AlertMail.EATTRIBUTE1  := v_ItemSequence;
							v_AlertMail.EATTRIBUTE2  := v_AlertType;
							v_AlertMail.EATTRIBUTE3  := v_SubItemSequence;
							
							--?1???2???????,?????ÌMail.
							IF v_AlertMail.RECIPIENTS <> ' ' THEN
							  v_AlertMail.WRITE();
							END IF;
						END IF;
						
					END LOOP;
					CLOSE v_AlertList;
					
					--????a???˙oÌÍ??
					UPDATE tblalertoqcng
						 SET startdate = v_startDate,
								 starttime = v_startTime
					 WHERE subitemsequence = v_SubItemSequence;
			 
          v_ProcessCounter := v_ProcessCounter + 1;
      END LOOP;
      CLOSE v_ResultList;     
      COMMIT;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'OK';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := '';
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();
			
      RETURN;
    EXCEPTION
      WHEN OTHERS THEN
			ROLLBACK;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'FAIL';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := SQLERRM;
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();

      RETURN;
  END AlertOQCNG;
  
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ?±Ì?????
	------------------------------------------------
  PROCEDURE AlertDirectPass (input_shiftday IN VARCHAR2 := to_char(SYSDATE,'yyyymmdd')) IS
     v_SQL                             VARCHAR2(2000);
		 v_AlertSql                        VARCHAR2(2000);
     TYPE cursor_DynamicSelect         IS REF CURSOR;
     v_ResultList                      cursor_DynamicSelect;  
		 v_AlertList                       cursor_DynamicSelect;
		 
		 --????Log
		 v_RunLog                          type_joblog;
		 v_BeginTime                       NUMBER;
     v_EndTime                         NUMBER;
		 v_ProcessCounter                  INT;
				
		 --tblalerterror???
		 v_AlertType                       VARCHAR2(40);
		 v_Description                     VARCHAR2(100);
		 v_MailSubject                     VARCHAR2(150);
		 v_MailContent                     VARCHAR2(2000);
		 v_ItemSequence                    VARCHAR2(40);
		 v_SubItemSequence                 VARCHAR2(40);
		 v_ItemType                        VARCHAR2(40);
     v_TimeDimension                   VARCHAR2(40);
		 v_BaseOutPut                      NUMBER;
		 v_AlertValue                      NUMBER;
		 v_GenerateNotice                  VARCHAR2(1);
		 v_SendMail                        VARCHAR2(1);
		 v_LinePause                       VARCHAR2(1);
		 
		 --???????
		 v_mcode                           VARCHAR2(40);
     v_itemCodeAndDesc                 VARCHAR2(200);
		 v_MolineOutputCount               NUMBER;
		 v_MoOutputWhitecardCount          NUMBER;
		 v_mgroup                          VARCHAR2(40);
		 v_ShiftCode                       VARCHAR2(40);
		 v_shiftday                        NUMBER;
		 v_bigsscode                       VARCHAR2(40);
		 
		 --?????????????1?
		 v_AlertedSql                      VARCHAR2(2000);
		 v_AlertedList                     cursor_DynamicSelect;
		 v_alertedCount                    NUMBER;
		 
		 --?±Ì??
		 v_DirectPass                      NUMBER;
		 
		 --2??‡˙Í∑±Ì
		 v_NoticeDirectPass                type_notice_directpass;
		 --2˙È˙Ì??????
		 v_AlertNotice                     type_alert_notice;
		 --2˙È˙Mail????
		 v_AlertMail                       type_mail;
		 v_itemFirstClass                  VARCHAR2(40);
		 v_itemSecondClass                 VARCHAR2(40);
		 v_MailContent_Formated            VARCHAR2(2000);
		 v_MailContent_Replace_List        type_varchar_list;
		 
    BEGIN
		  --3????Log
		  v_RunLog := NEW type_joblog();
      v_RunLog.jobid := 'AlertDirectPass';
      v_RunLog.startdatetime := SYSDATE;
      v_BeginTime := DBMS_UTILITY.get_time;
		  v_ProcessCounter := 0;
			
			--???????????
			v_SQL := 'SELECT b.alerttype, b.description, b.mailsubject, b.mailcontent,'
			      || '       a.itemsequence, a.subitemsequence, a.itemtype, a.timedimension,'
						|| '       a.baseoutput, a.alertvalue, a.generatenotice, a.sendmail, a.linepause'
            || '  FROM tblalertdirectpass a, tblalertitem b'
            || ' WHERE a.itemsequence = b.itemsequence'
						|| ' ORDER BY SubItemSequence';

      OPEN v_ResultList FOR v_SQL;        
      LOOP
          FETCH v_ResultList INTO 
					  v_AlertType,v_Description,v_MailSubject,v_MailContent,v_ItemSequence,
						v_SubItemSequence,v_ItemType,v_TimeDimension,
						v_BaseOutPut,v_AlertValue,v_GenerateNotice,v_SendMail,v_LinePause;
          EXIT WHEN v_ResultList%NOTFOUND;
					
					IF v_TimeDimension = 'byshiftcode' THEN
						v_AlertSql := 'SELECT mcode, mgroup, bigsscode, shiftcode,shiftday,SUM (molineoutputcount),';
						v_AlertSql := v_AlertSql ||'    SUM (mooutputwhitecardcount)';
						v_AlertSql := v_AlertSql || '    FROM (SELECT a.molineoutputcount, a.mooutputwhitecardcount, c.mcode,';
						v_AlertSql := v_AlertSql || '                 c.mtype, c.mgroup, b.bigsscode,b.shiftcode,a.shiftday';
					  v_AlertSql := v_AlertSql || '            FROM tblrptsoqty a, tblmesentitylist b, tblmaterial c';
						v_AlertSql := v_AlertSql || '           WHERE a.tblmesentitylist_serial = b.serial';
						v_AlertSql := v_AlertSql || '             AND a.itemcode = c.mcode';
						v_AlertSql := v_AlertSql || '             AND a.shiftday between ' || to_char(to_date(input_shiftday,'yyyymmdd')-1,'yyyymmdd');
						v_AlertSql := v_AlertSql || '             AND ' || to_char(to_date(input_shiftday,'yyyymmdd'),'yyyymmdd');
						v_AlertSql := v_AlertSql || '             AND c.mtype = '''|| v_ItemType ||''')';
						v_AlertSql := v_AlertSql || ' GROUP BY mcode, mgroup, bigsscode,shiftcode,shiftday';
						v_AlertSql := v_AlertSql || ' HAVING SUM (molineoutputcount) > ' || v_BaseOutPut;
						
					ELSE 
					  IF v_TimeDimension = 'byshiftday' THEN
							v_AlertSql := 'SELECT mcode, mgroup, bigsscode, '' '',shiftday,SUM (molineoutputcount),';
							v_AlertSql := v_AlertSql ||'    SUM (mooutputwhitecardcount)';
							v_AlertSql := v_AlertSql || '    FROM (SELECT a.molineoutputcount, a.mooutputwhitecardcount, c.mcode,';
							v_AlertSql := v_AlertSql || '                 c.mtype, c.mgroup, b.bigsscode,b.shiftcode,a.shiftday';
							v_AlertSql := v_AlertSql || '            FROM tblrptsoqty a, tblmesentitylist b, tblmaterial c';
							v_AlertSql := v_AlertSql || '           WHERE a.tblmesentitylist_serial = b.serial';
							v_AlertSql := v_AlertSql || '             AND a.itemcode = c.mcode';
							v_AlertSql := v_AlertSql || '             AND a.shiftday between ' || to_char(to_date(input_shiftday,'yyyymmdd')-1,'yyyymmdd');
							v_AlertSql := v_AlertSql || '             AND ' || to_char(to_date(input_shiftday,'yyyymmdd'),'yyyymmdd');
							v_AlertSql := v_AlertSql || '             AND c.mtype = '''|| v_ItemType ||''')';
							v_AlertSql := v_AlertSql || ' GROUP BY mcode, mgroup, bigsscode,shiftday';
							v_AlertSql := v_AlertSql || ' HAVING SUM (molineoutputcount) > ' || v_BaseOutPut;
							
						END IF;
					END IF;
					
					--????ËÚa??????????
					OPEN v_AlertList FOR v_AlertSql;
					LOOP
					  <<next_loop>>
						FETCH v_AlertList INTO 
							v_mcode,v_mgroup,v_bigsscode,v_ShiftCode,v_shiftday,v_MolineOutputCount,v_MoOutputWhitecardCount;
						EXIT WHEN v_AlertList%NOTFOUND;
							
						--1.?????????????1?
						v_AlertedSql := ' ';
						v_AlertedSql := v_AlertedSql || 'SELECT COUNT(*) AS sumCount';
						v_AlertedSql := v_AlertedSql || '  FROM tblnoticedirectpass a';
						v_AlertedSql := v_AlertedSql || ' WHERE a.itemsequence = ''' || v_ItemSequence || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.subitemsequence = ''' || v_SubItemSequence ||''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.itemcode = ''' || v_mcode || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.bigsscode = ''' || v_bigsscode || ''' ';
						v_AlertedSql := v_AlertedSql || '   AND a.shiftday = ' || v_shiftday;
						
						IF v_TimeDimension = 'byshiftcode' THEN
						  v_AlertedSql := v_AlertedSql || '   AND a.ShiftCode = ''' || v_ShiftCode || ''' ';
						ELSE
						  v_AlertedSql := v_AlertedSql || '   AND a.ShiftCode ='' '' ';
						END IF;
						
						OPEN v_AlertedList FOR v_AlertedSql;
						FETCH v_AlertedList INTO v_alertedCount;
						CLOSE v_AlertedList;
						
						--?1????????1????????±Í
						IF v_alertedCount > 0 THEN
						  GOTO next_loop; 
						END IF;
						
						--1.?????±Ì??
						IF v_ItemType = c_ItemType_FG THEN
						  v_DirectPass := round(v_MoOutputWhitecardCount/v_MolineOutputCount,4);
						ELSE
							
						  BEGIN
								--by shiftcode
								IF v_TimeDimension = 'byshiftcode' THEN
								
									SELECT product(PASSRCARDRATE) INTO v_DirectPass
										FROM (SELECT TBLMATERIAL.MCODE, TBLMATERIAL.MGROUP, CT.BIGSSCODE,
																	CT.SHIFTCODE, CT.SHIFTDAY, CT.OPCODE,
																	DECODE(SUM(CT.OPCOUNT), 0, 0,
																					round(SUM(CT.OPWHITECARDCOUNT) / SUM(CT.OPCOUNT), 4)) AS PASSRCARDRATE
														 FROM (SELECT TBLRPTSOQTY.SHIFTDAY, TBLRPTSOQTY.OPCOUNT,
																					 TBLRPTSOQTY.OPWHITECARDCOUNT, TBLRPTSOQTY.ITEMCODE,
																					 TBLMESENTITYLIST.SHIFTCODE, TBLMESENTITYLIST.OPCODE,
																					 TBLMESENTITYLIST.BIGSSCODE
																			FROM TBLRPTSOQTY
																		 INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL =
																																		TBLMESENTITYLIST.SERIAL
																		 INNER JOIN TBLSYSPARAM ON TBLSYSPARAM.PARAMALIAS =
																															 TBLMESENTITYLIST.OPCODE
																													 AND TBLSYSPARAM.PARAMGROUPCODE =
																															 'SEMIFINISHEDSTR') CT
														 LEFT OUTER JOIN TBLMATERIAL TBLMATERIAL ON TBLMATERIAL.MCODE =
																																				CT.ITEMCODE
														WHERE 1 = 1
															AND TBLMATERIAL.MTYPE = c_ItemType_NFG
															AND TBLMATERIAL.MCODE = v_mcode
															AND TBLMATERIAL.MGROUP = v_mgroup
															AND CT.BIGSSCODE = v_bigsscode
															AND CT.SHIFTCODE = v_ShiftCode
															AND CT.SHIFTDAY = v_shiftday
														GROUP BY TBLMATERIAL.MCODE, TBLMATERIAL.MGROUP, CT.BIGSSCODE,
																		 CT.SHIFTCODE, CT.SHIFTDAY, CT.OPCODE) TT
									 GROUP BY TT.MCODE, TT.MGROUP, TT.BIGSSCODE, TT.SHIFTCODE, TT.SHIFTDAY
									 ORDER BY TT.MCODE, TT.MGROUP, TT.BIGSSCODE, TT.SHIFTCODE, TT.SHIFTDAY;
									END IF;
									
									--by shiftday
									IF v_TimeDimension = 'byshiftday' THEN
										SELECT product(PASSRCARDRATE) INTO v_DirectPass
										FROM (SELECT TBLMATERIAL.MCODE, TBLMATERIAL.MGROUP, CT.BIGSSCODE,
																	CT.SHIFTDAY, CT.OPCODE,
																	DECODE(SUM(CT.OPCOUNT), 0, 0,
																					round(SUM(CT.OPWHITECARDCOUNT) / SUM(CT.OPCOUNT), 4)) AS PASSRCARDRATE
														 FROM (SELECT TBLRPTSOQTY.SHIFTDAY, TBLRPTSOQTY.OPCOUNT,
																					 TBLRPTSOQTY.OPWHITECARDCOUNT, TBLRPTSOQTY.ITEMCODE,
																					 TBLMESENTITYLIST.SHIFTCODE, TBLMESENTITYLIST.OPCODE,
																					 TBLMESENTITYLIST.BIGSSCODE
																			FROM TBLRPTSOQTY
																		 INNER JOIN TBLMESENTITYLIST ON TBLRPTSOQTY.TBLMESENTITYLIST_SERIAL =
																																		TBLMESENTITYLIST.SERIAL
																		 INNER JOIN TBLSYSPARAM ON TBLSYSPARAM.PARAMALIAS =
																															 TBLMESENTITYLIST.OPCODE
																													 AND TBLSYSPARAM.PARAMGROUPCODE =
																															 'SEMIFINISHEDSTR') CT
														 LEFT OUTER JOIN TBLMATERIAL TBLMATERIAL ON TBLMATERIAL.MCODE =
																																				CT.ITEMCODE
														WHERE 1 = 1
															AND TBLMATERIAL.MTYPE = c_ItemType_NFG
															AND TBLMATERIAL.MCODE = v_mcode
															AND TBLMATERIAL.MGROUP = v_mgroup
															AND CT.BIGSSCODE = v_bigsscode
															AND CT.SHIFTDAY = v_shiftday
														GROUP BY TBLMATERIAL.MCODE, TBLMATERIAL.MGROUP, CT.BIGSSCODE,
																		 CT.SHIFTDAY, CT.OPCODE) TT
									 GROUP BY TT.MCODE, TT.MGROUP, TT.BIGSSCODE, TT.SHIFTDAY
									 ORDER BY TT.MCODE, TT.MGROUP, TT.BIGSSCODE, TT.SHIFTDAY;
								END IF;
							
							EXCEPTION --??????????????±Í????
						  WHEN OTHERS THEN
							 GOTO next_loop;
							END;   
               
						END IF;
						
						v_DirectPass := ROUND(v_DirectPass, 4);
						
						--?1??±Ì???1????§±?????????
						IF v_DirectPass > v_AlertValue THEN
						   GOTO next_loop;
						END IF;

						--2.2??‡˙Í∑±Ì
						v_NoticeDirectPass                 := NEW type_notice_directpass();
						v_NoticeDirectPass.SERIAL          := 0;
						v_NoticeDirectPass.ITEMSEQUENCE    := v_ItemSequence;
						v_NoticeDirectPass.SUBITEMSEQUENCE := v_SubItemSequence;
						v_NoticeDirectPass.ITEMCODE        := v_mcode;
						v_NoticeDirectPass.SHIFTCODE       := v_ShiftCode;
						v_NoticeDirectPass.SHIFTDAY        := v_shiftday;
						v_NoticeDirectPass.BIGSSCODE       := v_bigsscode;
						v_NoticeDirectPass.MUSER           := c_MaintainUser;
						v_NoticeDirectPass.MDATE           := to_char(SYSDATE,'yyyymmdd');
						v_NoticeDirectPass.MTIME           := to_char(SYSDATE,'HH24MISS');
						v_NoticeDirectPass.WRITE();
						
						--2.1?????Mail Content
            SELECT mcode || ' - ' || mdesc INTO v_itemCodeAndDesc FROM tblmaterial WHERE mcode=v_mcode;
            
						v_MailContent_Replace_List := NEW type_varchar_list();
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := to_char(sysdate,'yyyy"??mm"??"dd"?" HH24:MI:SS');
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_itemCodeAndDesc;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_bigsscode;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_MolineOutputCount;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := TRIM(TO_CHAR(v_DirectPass * 100, '9990.99')) || '%' ;
						
						GetFirstSecondClass(v_mgroup,v_itemFirstClass,v_itemSecondClass);
						v_MailContent_Formated := FormatMailContent(v_MailContent,c_mailtype_directpass,v_MailContent_Replace_List);
						
						--3.2˙È˙Ì??????
						IF v_GenerateNotice = 'Y' THEN
						  v_AlertNotice                 := NEW type_alert_notice();
							v_AlertNotice.SERIAL          := 0;
							v_AlertNotice.ITEMSEQUENCE    := v_ItemSequence;
							v_AlertNotice.DESCRIPTION     := v_Description;
							v_AlertNotice.SUBITEMSEQUENCE := v_SubItemSequence;
							v_AlertNotice.NOTICESERIAL    := v_NoticeDirectPass.GetEntitySerial();
							v_AlertNotice.ALERTTYPE       := v_AlertType;
							v_AlertNotice.STATUS          := c_DealStatus;
							v_AlertNotice.MOLIST          := ' ';
							IF v_LinePause = 'Y' THEN
							  v_AlertNotice.MOLIST        := GetMOListToPause(v_mcode,v_shiftday,v_ShiftCode,v_bigsscode);
							END IF;
							
							v_AlertNotice.NOTICECONTENT   := v_MailContent_Formated;
							v_AlertNotice.ANALYSISREASON  := ' ';
							v_AlertNotice.DEALMETHODS     := ' ';
							v_AlertNotice.NOTICEDATE      := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.NOTICETIME      := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.DEALUSER        := ' ';
							v_AlertNotice.DEALDATE        := 0;
							v_AlertNotice.DEALTIME        := 0;
							v_AlertNotice.MUSER           := c_MaintainUser;
							v_AlertNotice.MDATE           := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.MTIME           := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.WRITE();
							
							--3.1.?????‡Ì
							IF v_LinePause = 'Y' THEN
							  LinePauseByMO(v_AlertNotice.MOLIST);
							END IF;
							
						END IF;
						
						--5.2˙È˙Mail????
						IF v_SendMail = 'Y' THEN
						  v_AlertMail := NEW type_mail();
							v_AlertMail.SERIAL       := 0;
							v_AlertMail.MAILSUBJECT  := v_MailSubject;
							v_AlertMail.RECIPIENTS   := GetRecipients(v_ItemSequence,v_bigsscode,v_itemFirstClass,v_itemSecondClass);
							v_AlertMail.MAILCONTENT  := v_MailContent_Formated;
							v_AlertMail.ISSEND       := 'N';
							v_AlertMail.SENDTIMES    := 0;
							v_AlertMail.SENDRESULT   := ' ';
							v_AlertMail.ERRORMESSAGE := ' ';
							v_AlertMail.MUSER        := c_MaintainUser;
							v_AlertMail.MDATE        := to_char(SYSDATE,'yyyymmdd');
							v_AlertMail.MTIME        := to_char(SYSDATE,'HH24MISS');
							v_AlertMail.EATTRIBUTE1  := v_ItemSequence;
							v_AlertMail.EATTRIBUTE2  := v_AlertType;
							v_AlertMail.EATTRIBUTE3  := v_SubItemSequence;
							
							--?1???2???????,?????ÌMail.
							IF v_AlertMail.RECIPIENTS <> ' ' THEN
							  v_AlertMail.WRITE();
							END IF;
						END IF;
						
					END LOOP;
					CLOSE v_AlertList;
          v_ProcessCounter := v_ProcessCounter + 1;
      END LOOP;
      CLOSE v_ResultList;        
      COMMIT;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'OK';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := '';
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();
			
      RETURN;
    EXCEPTION
      WHEN OTHERS THEN
			ROLLBACK;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'FAIL';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := SQLERRM;
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();

      RETURN;
  END AlertDirectPass;
  
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ???Í±3?????
	------------------------------------------------
  PROCEDURE AlertLinePause (input_shiftday IN DATE := SYSDATE) IS
     v_SQL                             VARCHAR2(2000);
     TYPE cursor_DynamicSelect         IS REF CURSOR;
     v_ResultList                      cursor_DynamicSelect;  
		 v_currentdatetime                 DATE := input_shiftday;
     v_shiftdayime                     VARCHAR2(2000) := to_char(input_shiftday,'yyyymmdd');
		 
		 --????Log
		 v_RunLog                          type_joblog;
		 v_BeginTime                       NUMBER;
     v_EndTime                         NUMBER;
		 v_ProcessCounter                  INT;
				
		 --tblalertlinepause???
		 v_AlertType                       VARCHAR2(40);
		 v_Description                     VARCHAR2(100);
		 v_MailSubject                     VARCHAR2(150);
		 v_MailContent                     VARCHAR2(2000);
		 v_ItemSequence                    VARCHAR2(40);
		 v_SubItemSequence                 VARCHAR2(40);
		 v_sscode                          VARCHAR2(40);
		 c_opcode                          VARCHAR2(40);
		 v_AlertValue                      NUMBER;
		 v_GenerateNotice                  VARCHAR2(1);
		 v_SendMail                        VARCHAR2(1);
		 
		 
		 --??????????????
		 v_shiftday                        NUMBER;
		 v_onwipserial                     NUMBER;
		 v_Noticeonwipserial               NUMBER;
		 
		 --??????????∞Ì??1??????oÛÚ?±Í???
		 v_ItemCode                        VARCHAR2(40);
		 v_onwipmdate                      NUMBER;
		 v_onwipmtime                      NUMBER;
		 
		 --???????????????
		 v_pauselinecount                  NUMBER;
		 
		 --??????????Í±3?
		 v_onwipduration                   NUMBER;
		 v_pauseduration                   NUMBER;
		 
		 --2??‡˙Í∑±Ì
		 v_NoticeLinePause                 type_notice_linepause;
		 --2˙È˙Ì??????
		 v_AlertNotice                     type_alert_notice;
		 --2˙È˙Mail????
		 v_AlertMail                       type_mail;
		 v_itemFirstClass                  VARCHAR2(40);
		 v_itemSecondClass                 VARCHAR2(40);
		 v_MailContent_Formated            VARCHAR2(2000);
		 v_MailContent_Replace_List        type_varchar_list;
		 
    BEGIN
		  --3????Log
		  v_RunLog := NEW type_joblog();
      v_RunLog.jobid := 'AlertLinePause';
      v_RunLog.startdatetime := SYSDATE;
      v_BeginTime := DBMS_UTILITY.get_time;
		  v_ProcessCounter := 0;
			
			--???????????
			v_SQL := 'SELECT b.alerttype, b.description, b.mailsubject, b.mailcontent,'
			      || '       a.itemsequence, a.subitemsequence,a.sscode, a.opcode,'
						|| '       a.alertvalue, a.generatenotice, a.sendmail'
            || '  FROM tblalertlinepause a, tblalertitem b'
            || '  WHERE a.itemsequence = b.itemsequence'
						|| ' ORDER BY SubItemSequence';

      OPEN v_ResultList FOR v_SQL;        
      LOOP
			    <<next_loop>>
          FETCH v_ResultList INTO 
					  v_AlertType,v_Description,v_MailSubject,v_MailContent,v_ItemSequence,
						v_SubItemSequence,v_sscode,c_opcode,
						v_AlertValue,v_GenerateNotice,v_SendMail;
          EXIT WHEN v_ResultList%NOTFOUND;
							
						--1.??????????????
					/*	BEGIN
						  SELECT shiftday,onwipserial
								INTO v_shiftday,v_onwipserial
								FROM (SELECT   shiftday,onwipserial
													FROM tblnoticelinepause
												 WHERE itemsequence = v_ItemSequence
													 AND subitemsequence = v_SubItemSequence
													 AND sscode = v_sscode
													 AND opcode = c_opcode
											ORDER BY shiftday DESC)
							 WHERE ROWNUM = 1;
						EXCEPTION
						  WHEN OTHERS THEN
							 v_shiftday := v_shiftdayime;
							 v_onwipSerial := 0;
						END; */
						
	                   BEGIN
						  SELECT shiftday,onwipserial
								INTO v_shiftday,v_Noticeonwipserial
								FROM (SELECT   shiftday,onwipserial
													FROM tblnoticelinepause
												 WHERE itemsequence = v_ItemSequence
													 AND subitemsequence = v_SubItemSequence
													 AND sscode = v_sscode
													 AND opcode = c_opcode
											ORDER BY shiftday DESC)
							 WHERE ROWNUM = 1;
						EXCEPTION
						  WHEN OTHERS THEN
							 v_shiftday := v_shiftdayime;
							 v_Noticeonwipserial := 0;
						END;
						
						
						
						
						
						 
						--??????????∞Ì??1??????oÛÚ?±Í???
						BEGIN
						
						/*
						  SELECT mdate,mtime,itemcode,shiftday,serial 
							  INTO v_onwipmdate,v_onwipmtime,v_ItemCode,v_shiftday,v_onwipserial
								FROM (SELECT *
												 FROM tblonwip
												WHERE serial > v_onwipSerial
													AND opcode = c_opcode
													AND sscode = v_sscode
													AND shiftday BETWEEN to_char(to_date(v_shiftdayime,'yyyymmdd')-1,'yyyymmdd')
													AND to_char(to_date(v_shiftdayime,'yyyymmdd'),'yyyymmdd')
												ORDER BY mdate DESC, mtime DESC, serial DESC)
							 WHERE ROWNUM = 1;
							 */
							 
 			                SELECT mdate,mtime,itemcode,shiftday,serial 
							  INTO v_onwipmdate,v_onwipmtime,v_ItemCode,v_shiftday,v_onwipserial
							  FROM tblonwip
								WHERE serial =
                                    (select max(serial) from tblonwip 
								      WHERE opcode = c_opcode
								      AND sscode = v_sscode
								      AND shiftday BETWEEN to_char(to_date(v_shiftdayime,'yyyymmdd')-1,'yyyymmdd')
								      AND to_char(to_date(v_shiftdayime,'yyyymmdd'),'yyyymmdd')
                                     );
						EXCEPTION
						   WHEN OTHERS THEN
							 GOTO next_loop;
						END;
						
						--?1???oÛÚ?±Í??Serial?ÌÍ?Notice????oÛÚ?±Í??2?????????? 
						
						IF v_Noticeonwipserial = v_onwipserial THEN
						
						      GOTO next_loop;
						END IF;
						
						
						
						--???????????????
            SELECT COUNT(*) INTO v_pauselinecount
              FROM TBLLinePause
             WHERE sscode = v_sscode
               AND begindate * 1000000 + begintime <= to_char(v_currentdatetime,'yyyymmddHH24MIss')
               AND enddate = 0
               AND endtime = 0;
							 
						IF v_pauselinecount > 0 THEN
						  GOTO next_loop;
						END IF;
						
						--??????????Í±3?
						v_onwipduration := ROUND((v_currentdatetime - to_date(v_onwipmdate*1000000+v_onwipmtime,'yyyymmddHH24MIss'))*24*60*60);
            
            SELECT nvl(SUM(duration),0) INTO v_pauseduration
              FROM TBLLinePause
             WHERE sscode = v_sscode
               AND begindate * 1000000 + begintime >= v_onwipmdate * 1000000 + v_onwipmtime
               AND enddate * 1000000 + endtime <= to_char(v_currentdatetime, 'yyyymmddHH24MIss');
						
						v_pauseduration := v_onwipduration - v_pauseduration;
						
						IF v_pauseduration < v_AlertValue THEN
						  GOTO next_loop;
						END IF;
						
						--2.2??‡˙Í∑±Ì
						v_NoticeLinePause                 := NEW type_notice_linepause();
						v_NoticeLinePause.SERIAL          := 0;
						v_NoticeLinePause.ITEMSEQUENCE    := v_ItemSequence;
						v_NoticeLinePause.SUBITEMSEQUENCE := v_SubItemSequence;
						v_NoticeLinePause.SSCODE          := v_sscode;
						v_NoticeLinePause.OPCODE          := c_opcode;
						v_NoticeLinePause.SHIFTDAY        := v_shiftday;
						v_NoticeLinePause.ONWIPSERIAL     := v_OnwipSerial;
						v_NoticeLinePause.MUSER           := c_MaintainUser;
						v_NoticeLinePause.MDATE           := to_char(SYSDATE,'yyyymmdd');
						v_NoticeLinePause.MTIME           := to_char(SYSDATE,'HH24MISS');
						v_NoticeLinePause.WRITE();
						
						--2.1?????Mail Content
						v_MailContent_Replace_List := NEW type_varchar_list();
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := to_char(v_currentdatetime,'yyyy"??mm"??"dd"?" HH24:MI:SS');
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := v_sscode;
						v_MailContent_Replace_List.EXTEND;
						v_MailContent_Replace_List(v_MailContent_Replace_List.COUNT) := trunc(v_pauseduration/60)||'?'||MOD(v_pauseduration,60)||'??';
						
						
						GetFirstSecondClassByItemCode(v_ItemCode,v_itemFirstClass,v_itemSecondClass);
						v_MailContent_Formated := FormatMailContent(v_MailContent,c_mailtype_linepause,v_MailContent_Replace_List);
						
						--3.2˙È˙Ì??????
						IF v_GenerateNotice = 'Y' THEN
						  v_AlertNotice                 := NEW type_alert_notice();
							v_AlertNotice.SERIAL          := 0;
							v_AlertNotice.ITEMSEQUENCE    := v_ItemSequence;
							v_AlertNotice.DESCRIPTION     := v_Description;
							v_AlertNotice.SUBITEMSEQUENCE := v_SubItemSequence;
							v_AlertNotice.NOTICESERIAL    := v_NoticeLinePause.GetEntitySerial();
							v_AlertNotice.ALERTTYPE       := v_AlertType;
							v_AlertNotice.STATUS          := c_DealStatus;
							v_AlertNotice.MOLIST          := ' ';
							v_AlertNotice.NOTICECONTENT   := v_MailContent_Formated;
							v_AlertNotice.ANALYSISREASON  := ' ';
							v_AlertNotice.DEALMETHODS     := ' ';
							v_AlertNotice.NOTICEDATE      := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.NOTICETIME      := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.DEALUSER        := ' ';
							v_AlertNotice.DEALDATE        := 0;
							v_AlertNotice.DEALTIME        := 0;
							v_AlertNotice.MUSER           := c_MaintainUser;
							v_AlertNotice.MDATE           := to_char(SYSDATE,'yyyymmdd');
							v_AlertNotice.MTIME           := to_char(SYSDATE,'HH24MISS');
							v_AlertNotice.WRITE();
							
						END IF;
						
						--5.2˙È˙Mail????
						IF v_SendMail = 'Y' THEN
						  v_AlertMail := NEW type_mail();
							v_AlertMail.SERIAL       := 0;
							v_AlertMail.MAILSUBJECT  := v_MailSubject;
							v_AlertMail.RECIPIENTS   := GetRecipients(v_ItemSequence,GetBigSSCode(v_sscode),v_itemFirstClass,v_itemSecondClass);
							v_AlertMail.MAILCONTENT  := v_MailContent_Formated;
							v_AlertMail.ISSEND       := 'N';
							v_AlertMail.SENDTIMES    := 0;
							v_AlertMail.SENDRESULT   := ' ';
							v_AlertMail.ERRORMESSAGE := ' ';
							v_AlertMail.MUSER        := c_MaintainUser;
							v_AlertMail.MDATE        := to_char(SYSDATE,'yyyymmdd');
							v_AlertMail.MTIME        := to_char(SYSDATE,'HH24MISS');
							v_AlertMail.EATTRIBUTE1  := v_ItemSequence;
							v_AlertMail.EATTRIBUTE2  := v_AlertType;
							v_AlertMail.EATTRIBUTE3  := v_SubItemSequence;
							
							--?1???2???????,?????ÌMail.
							IF v_AlertMail.RECIPIENTS <> ' ' THEN
							  v_AlertMail.WRITE();
							END IF;
						END IF;
						
					
          v_ProcessCounter := v_ProcessCounter + 1;
      END LOOP;
      CLOSE v_ResultList;        
      COMMIT;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'OK';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := '';
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();
			
      RETURN;
    EXCEPTION
      WHEN OTHERS THEN
			ROLLBACK;
			
			v_EndTime := DBMS_UTILITY.get_time;
			v_RunLog.enddatetime := SYSDATE;
			v_RunLog.result := 'FAIL';
			v_RunLog.processcount := v_ProcessCounter;
			v_RunLog.errormessage := SQLERRM;
			v_RunLog.usedtime := (v_EndTime - v_BeginTime) * 10;
			v_RunLog.write();

			-- Clear History Log
			v_RunLog.clearhistorylog();

      RETURN;
  END AlertLinePause;

  ------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ?????????1???o?,?oo????
	------------------------------------------------
  FUNCTION GetMOListToPause(ItemCode  IN VARCHAR2,
                            shiftday  IN NUMBER,
                            shiftcode IN VARCHAR2,
                            bigsscode IN VARCHAR2) 
	RETURN VARCHAR2 IS
	  v_Sql    VARCHAR2(2000);
    v_MoList VARCHAR2(2000);
		TYPE cursor_DynamicSelect IS REF CURSOR;
		
		v_ResultList cursor_DynamicSelect;
		v_MoTemp VARCHAR2(40);
  BEGIN
	  v_MoList := ' ';
	  v_Sql    := ' ';
		
		v_Sql := v_Sql || 'SELECT DISTINCT mocode';
		v_Sql := v_Sql || '  FROM tblsimulationreport';
		v_Sql := v_Sql || ' WHERE itemcode = ''' || ItemCode || '''';
		v_Sql := v_Sql || '   AND iscom = ''0''';
		v_Sql := v_Sql || '   AND shiftday = ' || shiftday;
		IF bigsscode <> ' ' THEN
  		v_Sql := v_Sql || '   AND sscode IN (SELECT DISTINCT sscode FROM tblss WHERE bigsscode = ''' || bigsscode || ''')';
	  END IF;
		IF shiftcode <> ' ' THEN
  		v_Sql := v_Sql || '   AND shiftcode = ''' || shiftcode || '''';
		END IF;

	  OPEN v_ResultList FOR v_SQL;        
    LOOP
      FETCH v_ResultList INTO v_MoTemp;
      EXIT WHEN v_ResultList%NOTFOUND;
					
			v_MoList := v_MoList || v_MoTemp || ',';
					
		END LOOP;
		CLOSE v_ResultList;
	  
		IF v_MoList <> ' ' THEN
      v_MoList := trim(substr(v_MoList,0,length(v_MoList) - 1));
		END IF;
    
		RETURN(v_MoList);
		EXCEPTION
      WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20000, '[GetMOListToPause]' || SQLERRM);
  END GetMOListToPause;
	
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ?????????1???o?,?oo????
	------------------------------------------------
  PROCEDURE LinePauseByMO(MoCodeList IN VARCHAR2) 
	 IS
		v_int_i                   INT;
		
		--?????o?‡‡??
		v_MoList                  type_varchar_list;
  BEGIN
	  
    v_MoList := split(MoCodeList);
		
		IF v_MoList.COUNT > 0 THEN
			FOR v_int_i IN 1..v_MoList.COUNT
			LOOP
			
				UPDATE tblmo
					 SET MOSTATUS = 'mostatus_pending', eattribute3 = 'AlertJob',
							 mopendingcause = 'ALERTJOB'
				 WHERE mostatus = 'mostatus_open' 
					 AND mocode = v_MoList(v_int_i);
			
			END LOOP;
		END IF;
		
		EXCEPTION
      WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20000, '[LinePauseByMO]' || SQLERRM);
  END LinePauseByMO;
	
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : splito??
	------------------------------------------------
	FUNCTION split(p_list VARCHAR2,is_distinct BOOLEAN := FALSE,p_sep VARCHAR2 := ',')
		RETURN type_varchar_list IS
		j         INT := 0;
		i         INT := 1;
		len       INT := 0;
		len1      INT := 0;
		str       VARCHAR2(4000);
		str_split type_varchar_list := type_varchar_list();
		strCount  NUMBER := 0;
	BEGIN
		len  := length(p_list);
		len1 := length(p_sep);
		WHILE j < len LOOP
			j := instr(p_list, p_sep, i);
			IF j = 0 THEN
				j   := len;
				str := substr(p_list, i);
				
				IF is_distinct THEN
				  SELECT COUNT(*) INTO strCount
					FROM TABLE(str_split)
					WHERE COLUMN_VALUE = str;
					
					IF strCount = 0 THEN
					  str_split.EXTEND;
				    str_split(str_split.COUNT) := str;
					END IF;
				ELSE
				  str_split.EXTEND;
				  str_split(str_split.COUNT) := str;
				END IF;
				
				
				
				IF i >= len THEN
					EXIT;
				END IF;
			ELSE
				str := substr(p_list, i, j - i);
				i   := j + len1;
				
				IF is_distinct THEN
				  SELECT COUNT(*) INTO strCount
					FROM TABLE(str_split)
					WHERE COLUMN_VALUE =str;
					
					IF strCount = 0 THEN
					  str_split.EXTEND;
				    str_split(str_split.COUNT) := str;
					END IF;
				ELSE
				  str_split.EXTEND;
				  str_split(str_split.COUNT) := str;
				END IF;
			END IF;
		END LOOP;
		RETURN str_split;
		
		EXCEPTION
      WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20000, '[split]' || SQLERRM);
	END split;
  
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ????Mail To?±Ì
	------------------------------------------------
  PROCEDURE GetFirstSecondClass(mgroup IN VARCHAR2,
                               firstclass  OUT VARCHAR2,
                               secondclass OUT VARCHAR2) 
	 IS
  BEGIN

		SELECT firstclass,secondclass 
		INTO firstclass,secondclass
		FROM tblItemClass WHERE itemgroup=mgroup;

    EXCEPTION
		  WHEN NO_DATA_FOUND THEN
			  firstclass  := ' ';
		    secondclass := ' ';
      WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20000, '[GetFirstSecondClass]' || SQLERRM);
  END GetFirstSecondClass;
	
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ????Mail To?±Ì
	------------------------------------------------
  FUNCTION GetRecipients(ItemSequence   IN VARCHAR2,
                            bigsscode   IN VARCHAR2,
                            firstclass  IN VARCHAR2,
                            secondclass IN VARCHAR2) 
	RETURN VARCHAR2 IS
	
		v_ItemSequence			VARCHAR2(2000);
		v_bigsscode				VARCHAR2(2000);
		v_firstclass			VARCHAR2(2000);
		v_secondclass			VARCHAR2(2000);		
			
	  	v_Sql                     VARCHAR2(2000);
    	v_MailList                VARCHAR2(2000);
		v_int_i                   INT;
		TYPE cursor_DynamicSelect IS REF CURSOR;
		
		v_ResultList              cursor_DynamicSelect;
		v_MailListTemp            VARCHAR2(2000);
		
		v_type_maillist           type_varchar_list;
  BEGIN
	  	v_MailList := ' ';
	  	v_Sql    := ' ';	  

	  	v_ItemSequence := NVL(TRIM(ItemSequence), ' ');
	  	v_bigsscode := NVL(TRIM(bigsscode), ' ');
	  	v_firstclass := NVL(TRIM(firstclass), ' ');
	  	v_secondclass := NVL(TRIM(secondclass), ' ');	  
		
		v_Sql := v_Sql || 'SELECT recipients FROM tblalertmailsetting';
		v_Sql := v_Sql || ' WHERE NVL(TRIM(itemsequence), '' '') = ''' || v_ItemSequence || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(bigsscode), '' '') = ''' || v_bigsscode || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemfirstclass), '' '') = ''' || v_firstclass || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemsecondclass), '' '') = ''' || v_secondclass || '''';
		v_Sql := v_Sql || ' UNION ';
		v_Sql := v_Sql || 'SELECT recipients FROM tblalertmailsetting';
		v_Sql := v_Sql || ' WHERE NVL(TRIM(itemsequence), '' '') = ''' || v_ItemSequence || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(bigsscode), '' '') = '' '' ';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemfirstclass), '' '') = ''' || v_firstclass || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemsecondclass), '' '') = ''' || v_secondclass || '''';
		v_Sql := v_Sql || ' UNION ';
		v_Sql := v_Sql || 'SELECT recipients FROM tblalertmailsetting';
		v_Sql := v_Sql || ' WHERE NVL(TRIM(itemsequence), '' '') = ''' || v_ItemSequence || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(bigsscode), '' '') = '' '' ';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemfirstclass), '' '') = '' '' ';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemsecondclass), '' '') = '' '' ';
		v_Sql := v_Sql || ' UNION ';
		v_Sql := v_Sql || 'SELECT recipients FROM tblalertmailsetting';
		v_Sql := v_Sql || ' WHERE NVL(TRIM(itemsequence), '' '') = ''' || v_ItemSequence || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(bigsscode), '' '') = ''' || v_bigsscode || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemfirstclass), '' '') = '' '' ';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemsecondclass), '' '') = '' '' ';
		v_Sql := v_Sql || ' UNION ';
		v_Sql := v_Sql || 'SELECT recipients FROM tblalertmailsetting';
		v_Sql := v_Sql || ' WHERE NVL(TRIM(itemsequence), '' '') = ''' || v_ItemSequence || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(bigsscode), '' '') = ''' || v_bigsscode || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemfirstclass), '' '') = ''' || v_firstclass || '''';
		v_Sql := v_Sql || '   AND NVL(TRIM(itemsecondclass), '' '') = '' '' ';

	  OPEN v_ResultList FOR v_SQL;        
    LOOP
      FETCH v_ResultList INTO v_MailListTemp;
      EXIT WHEN v_ResultList%NOTFOUND;
					
			v_MailList := v_MailList || v_MailListTemp || ';';
					
		END LOOP;
		CLOSE v_ResultList;
	  
		IF v_MailList <> ' ' THEN
      v_MailList := trim(substr(v_MailList,0,length(v_MailList) - 1));
		ELSE
		  RETURN(v_MailList); 
		END IF;
		
		--Distinct Mail?±Ì
		v_type_maillist := split(v_MailList,TRUE,';');
    
		IF v_type_maillist.COUNT > 0 THEN
		    v_MailList := ' ';
			FOR v_int_i IN 1..v_type_maillist.COUNT
			LOOP
			  v_MailList := v_MailList || v_type_maillist(v_int_i) || ';';
			END LOOP;
		END IF;
		
		IF v_MailList <> ' ' THEN
      v_MailList := trim(substr(v_MailList,0,length(v_MailList) - 1));
		END IF;
		
		RETURN(v_MailList);
		
		EXCEPTION
      WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20000, '[GetRecipients]' || SQLERRM);
  END GetRecipients;
	
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ????Mail To?±Ì
	------------------------------------------------
  FUNCTION FormatMailContent(mailContent IN VARCHAR2,mailCategory IN VARCHAR2,
	                           replaceList IN type_varchar_list) 
	 RETURN VARCHAR2 IS
	   return_content        VARCHAR2(2000) := ' ';
	 
  BEGIN
    IF mailCategory = c_mailtype_alerterror THEN
		  return_content :=REPLACE(mailContent,'$$datetime$$',replaceList(1));
			return_content :=REPLACE(return_content,'$$mcode$$',replaceList(2));
			return_content :=REPLACE(return_content,'$$bigsscode$$',replaceList(3));
			return_content :=REPLACE(return_content,'$$ecode$$',replaceList(4));
			return_content :=REPLACE(return_content,'$$alertvalue$$',replaceList(5));
			
			RETURN(return_content);
		END IF;
		
		IF mailCategory = c_mailtype_errorcode THEN
		  return_content :=REPLACE(mailContent,'$$datetime$$',replaceList(1));
			return_content :=REPLACE(return_content,'$$mcode$$',replaceList(2));
			return_content :=REPLACE(return_content,'$$bigsscode$$',replaceList(3));
			return_content :=REPLACE(return_content,'$$eloc$$',replaceList(4));
			return_content :=REPLACE(return_content,'$$ecscode$$',replaceList(5));
			return_content :=REPLACE(return_content,'$$alertvalue$$',replaceList(6));
			
			RETURN(return_content);
		END IF;
		
		IF mailCategory = c_mailtype_OQCNG THEN
		  return_content :=REPLACE(mailContent,'$$datetime$$',replaceList(1));
			return_content :=REPLACE(return_content,'$$mcode$$',replaceList(2));
			return_content :=REPLACE(return_content,'$$ecode$$',replaceList(3));
			return_content :=REPLACE(return_content,'$$alertvalue$$',replaceList(4));
			
			RETURN(return_content);
		END IF;
		
		IF mailCategory = c_mailtype_directpass THEN
		  return_content :=REPLACE(mailContent,'$$datetime$$',replaceList(1));
			return_content :=REPLACE(return_content,'$$mcode$$',replaceList(2));
			return_content :=REPLACE(return_content,'$$bigsscode$$',replaceList(3));
			return_content :=REPLACE(return_content,'$$molineoutputcount$$',replaceList(4));
			return_content :=REPLACE(return_content,'$$directpass$$',replaceList(5));
			
			RETURN(return_content);
		END IF;
		
		IF mailCategory = c_mailtype_linepause THEN
		  return_content :=REPLACE(mailContent,'$$datetime$$',replaceList(1));
			return_content :=REPLACE(return_content,'$$bigsscode$$',replaceList(2));
			return_content :=REPLACE(return_content,'$$pauseduration$$',replaceList(3));
			
			RETURN(return_content);
		END IF;

    EXCEPTION
      WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20000, '[FormatMailContent]' || SQLERRM);
  END FormatMailContent;
	
	------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ????????????????
	------------------------------------------------
	FUNCTION GetBigSSCode(i_SSCode IN VARCHAR2)
        RETURN VARCHAR2
    IS
        v_BigSSCode     VARCHAR2(40);
    BEGIN
        v_BigSSCode := ' ';
        SELECT bigsscode
          INTO v_BigSSCode
          FROM tblss
         WHERE sscode = i_SSCode;
        RETURN v_BigSSCode;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            raise_application_error(-20000,'Can not get Big SSCode by SSCode [' || i_SSCode || ']');
        WHEN OTHERS THEN
            raise_application_error(-20000,'[GetBigSSCode]' || SQLERRM);
    END GetBigSSCode;
		
		------------------------------------------------
  -- Author  : WINDY.XU
  -- Created : 2009-4-13 10:04:43
  -- Purpose : ????????????????
	------------------------------------------------
	PROCEDURE GetFirstSecondClassByItemCode(i_ItemCode IN VARCHAR2,
	                                        firstclass  OUT VARCHAR2,
                                          secondclass OUT VARCHAR2)
  IS
  BEGIN

		SELECT a.firstclass, a.secondclass
		  INTO firstclass,secondclass
			FROM tblItemClass a, tblmaterial b
		 WHERE a.itemgroup = b.mgroup
			 AND b.mcode = i_ItemCode;

    EXCEPTION
		  WHEN NO_DATA_FOUND THEN
			  firstclass  := ' ';
		    secondclass := ' ';
      WHEN OTHERS THEN
      RAISE_APPLICATION_ERROR(-20000, '[GetFirstSecondClassByItemCode]' || SQLERRM);
    END GetFirstSecondClassByItemCode;
		
		
		

END PKG_ALERTENGINE;



CREATE OR REPLACE TYPE BODY productimpl
IS
   STATIC FUNCTION odciaggregateinitialize (sctx IN OUT productimpl)
      RETURN NUMBER
   IS
   BEGIN
      sctx := productimpl (0);
      RETURN odciconst.success;
   END;
   MEMBER FUNCTION odciaggregateiterate (SELF IN OUT productimpl, VALUE IN NUMBER)
      RETURN NUMBER
   IS
   BEGIN
      IF SELF.product_result = 0
      THEN
         SELF.product_result := 1;
      END IF;

      IF VALUE = 0
      THEN
         SELF.product_result := 0;
         RETURN odciconst.success;
      ELSIF VALUE <> 0
      THEN
         SELF.product_result := SELF.product_result * VALUE;
      END IF;

      RETURN odciconst.success;
   END;
   MEMBER FUNCTION odciaggregateterminate (SELF IN productimpl, returnvalue OUT NUMBER, flags IN NUMBER)
      RETURN NUMBER
   IS
   BEGIN
      returnvalue := SELF.product_result;
      RETURN odciconst.success;
   END;
   MEMBER FUNCTION odciaggregatemerge (SELF IN OUT productimpl, ctx2 IN productimpl)
      RETURN NUMBER
   IS
   BEGIN
      RETURN odciconst.success;
   END;
END;
/

create or replace type body TYPE_ALERT_NOTICE is
  
  CONSTRUCTOR FUNCTION TYPE_ALERT_NOTICE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        DESCRIPTION     IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        NOTICESERIAL    IN  NUMBER   DEFAULT 0,
        ALERTTYPE       IN  VARCHAR2 DEFAULT ' ',
        STATUS          IN  VARCHAR2 DEFAULT ' ',
        MOLIST          IN  VARCHAR2 DEFAULT ' ',
        NOTICECONTENT   IN  VARCHAR2 DEFAULT ' ',
        ANALYSISREASON  IN  VARCHAR2 DEFAULT ' ',
        DEALMETHODS     IN  VARCHAR2 DEFAULT ' ',
        NOTICEDATE      IN  NUMBER   DEFAULT 0,
        NOTICETIME      IN  NUMBER   DEFAULT 0,
        DEALUSER        IN  VARCHAR2 DEFAULT ' ',
        DEALDATE        IN  NUMBER   DEFAULT 0,
        DEALTIME        IN  NUMBER   DEFAULT 0,
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.DESCRIPTION := DESCRIPTION;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.NOTICESERIAL := NOTICESERIAL;
        SELF.ALERTTYPE := ALERTTYPE;
        SELF.STATUS := STATUS;
        SELF.MOLIST := MOLIST;
        SELF.NOTICECONTENT := NOTICECONTENT;
        SELF.ANALYSISREASON := ANALYSISREASON;
        SELF.DEALMETHODS := DEALMETHODS;
        SELF.NOTICEDATE := NOTICEDATE;
        SELF.NOTICETIME := NOTICETIME;
        SELF.DEALUSER := DEALUSER;
        SELF.DEALDATE := DEALDATE;
        SELF.DEALTIME := DEALTIME;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_ALERT_NOTICE;
    
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLALERTNOTICE
          (SERIAL, ITEMSEQUENCE, DESCRIPTION, SUBITEMSEQUENCE, NOTICESERIAL, ALERTTYPE, 
          STATUS, MOLIST, NOTICECONTENT, ANALYSISREASON, DEALMETHODS, NOTICEDATE, 
          NOTICETIME, DEALUSER, DEALDATE, DEALTIME, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, DESCRIPTION, SUBITEMSEQUENCE, NOTICESERIAL, ALERTTYPE, 
          STATUS, MOLIST, NOTICECONTENT, ANALYSISREASON, DEALMETHODS, NOTICEDATE, 
          NOTICETIME, DEALUSER, DEALDATE, DEALTIME, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
  
end;
/

create or replace type body TYPE_MAIL is
  
  CONSTRUCTOR FUNCTION TYPE_MAIL (
        SERIAL       IN  NUMBER   DEFAULT 0,
        MAILSUBJECT  IN  VARCHAR2 DEFAULT ' ',
        RECIPIENTS   IN  VARCHAR2 DEFAULT ' ',
        MAILCONTENT  IN  VARCHAR2 DEFAULT ' ',
        ISSEND       IN  VARCHAR2 DEFAULT ' ',
        SENDTIMES    IN  NUMBER   DEFAULT 0,
        SENDRESULT   IN  VARCHAR2 DEFAULT ' ',
        ERRORMESSAGE IN  VARCHAR2 DEFAULT ' ',
        MUSER        IN  VARCHAR2 DEFAULT ' ',
        MDATE        IN  NUMBER   DEFAULT 0,
        MTIME        IN  NUMBER   DEFAULT 0,
        EATTRIBUTE1  IN  VARCHAR2 DEFAULT ' ',
        EATTRIBUTE2  IN  VARCHAR2 DEFAULT ' ',
        EATTRIBUTE3  IN  VARCHAR2 DEFAULT ' '
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.MAILSUBJECT := MAILSUBJECT;
        SELF.RECIPIENTS := RECIPIENTS;
        SELF.MAILCONTENT := MAILCONTENT;
        SELF.ISSEND := ISSEND;
        SELF.SENDTIMES := SENDTIMES;
        SELF.SENDRESULT := SENDRESULT;
        SELF.ERRORMESSAGE := ERRORMESSAGE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        SELF.EATTRIBUTE1 := EATTRIBUTE1;
        SELF.EATTRIBUTE2 := EATTRIBUTE2;
        SELF.EATTRIBUTE3 := EATTRIBUTE3;
        RETURN;
    END TYPE_MAIL;
    
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLMAIL
          (SERIAL, MAILSUBJECT, RECIPIENTS, MAILCONTENT, ISSEND, SENDTIMES, 
          SENDRESULT, ERRORMESSAGE, MUSER, MDATE, MTIME, EATTRIBUTE1, EATTRIBUTE2, EATTRIBUTE3)
        VALUES
          (SERIAL, MAILSUBJECT, RECIPIENTS, MAILCONTENT, ISSEND, SENDTIMES, 
          SENDRESULT, ERRORMESSAGE, MUSER, MDATE, MTIME, EATTRIBUTE1, EATTRIBUTE2, EATTRIBUTE3);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
  
end;
/

create or replace type body TYPE_NOTICE_DIRECTPASS is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_DIRECTPASS (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.ITEMCODE := ITEMCODE;
        SELF.SHIFTCODE := SHIFTCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.BIGSSCODE := BIGSSCODE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_DIRECTPASS;
    
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLNOTICEDIRECTPASS
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		----------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM TBLNOTICEDIRECTPASS a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.ITEMCODE = ITEMCODE
           AND a.SHIFTCODE = SHIFTCODE
           AND a.SHIFTDAY = SHIFTDAY
					 AND a.BIGSSCODE = BIGSSCODE
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;
/

create or replace type body TYPE_NOTICE_ERROR is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_ERROR (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        ECODE           IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.ITEMCODE := ITEMCODE;
        SELF.ECODE := ECODE;
        SELF.SHIFTCODE := SHIFTCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.BIGSSCODE := BIGSSCODE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_ERROR;
    ---------------------------------
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO tblnoticeerror
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECODE, SHIFTCODE,
           SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECODE, SHIFTCODE,
           SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		-----------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM tblnoticeerror a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.ITEMCODE = ITEMCODE
           AND a.ECODE = ECODE
           AND a.SHIFTCODE = SHIFTCODE
           AND a.SHIFTDAY = SHIFTDAY
           AND a.BIGSSCODE = BIGSSCODE
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;
/

create or replace type body TYPE_NOTICE_ERROR_CODE is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_ERROR_CODE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        ECSCODE         IN  VARCHAR2 DEFAULT ' ',
        LOCATION        IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.ITEMCODE := ITEMCODE;
        SELF.ECSCODE := ECSCODE;
        SELF.LOCATION := LOCATION;
        SELF.SHIFTCODE := SHIFTCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.BIGSSCODE := BIGSSCODE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_ERROR_CODE;
    -----------------------------
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLNOTICEERRORCODE
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECSCODE, LOCATION,
           SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECSCODE, LOCATION,
           SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		-------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM TBLNOTICEERRORCODE a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.ITEMCODE = ITEMCODE
           AND a.ECSCODE = ECSCODE
           AND a.LOCATION = LOCATION
           AND a.SHIFTCODE = SHIFTCODE
           AND a.SHIFTDAY = SHIFTDAY
					 AND a.BIGSSCODE = BIGSSCODE
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;
/

create or replace type body TYPE_NOTICE_LINEPAUSE is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_LINEPAUSE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        SSCODE          IN  VARCHAR2 DEFAULT ' ',
        OPCODE          IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        ONWIPSERIAL     IN  NUMBER   DEFAULT 0,
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.SSCODE := SSCODE;
        SELF.OPCODE := OPCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.ONWIPSERIAL := ONWIPSERIAL;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_LINEPAUSE;
    
		------------------------------
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLNOTICELINEPAUSE
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, SSCODE, OPCODE, SHIFTDAY, ONWIPSERIAL, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, SSCODE, OPCODE, SHIFTDAY, ONWIPSERIAL, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		
		------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM TBLNOTICELINEPAUSE a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.SSCODE = SSCODE
           AND a.OPCODE = OPCODE
           AND a.SHIFTDAY = SHIFTDAY
           AND a.ONWIPSERIAL = ONWIPSERIAL
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;
/

/

SHOW ERRORS;

CREATE OR REPLACE PACKAGE BODY PKG_COMPUTELOSTMANHOUR IS
  --??????∞Í????????
  FUNCTION GETLASTSHIFT(T_SHIFTCODE IN VARCHAR2,
                        T_SHIFTTYPE IN VARCHAR2,
                        T_SHIFTSEQ  IN INT) RETURN VARCHAR2 AS
    V_RETURNSHIFTCODE VARCHAR2(40);
    I_ROWCOUNT        INT;
    I_SHIFTSEQ        INT;
  BEGIN
    SELECT COUNT(SHIFTCODE)
      INTO I_ROWCOUNT
      FROM TBLSHIFT
     WHERE SHIFTTYPECODE = T_SHIFTTYPE
       AND SHIFTSEQ = T_SHIFTSEQ - 1;
  
    IF I_ROWCOUNT <= 0 THEN
      SELECT MAX(SHIFTSEQ)
        INTO I_SHIFTSEQ
        FROM TBLSHIFT
       WHERE SHIFTTYPECODE = T_SHIFTTYPE;
    
      IF I_SHIFTSEQ = T_SHIFTSEQ THEN
        V_RETURNSHIFTCODE := T_SHIFTCODE;
      ELSE
        SELECT SHIFTCODE
          INTO V_RETURNSHIFTCODE
          FROM TBLSHIFT
         WHERE SHIFTTYPECODE = T_SHIFTTYPE
           AND SHIFTSEQ = I_SHIFTSEQ;
      END IF;
    ELSE
      SELECT SHIFTCODE
        INTO V_RETURNSHIFTCODE
        FROM TBLSHIFT
       WHERE SHIFTTYPECODE = T_SHIFTTYPE
         AND SHIFTSEQ = T_SHIFTSEQ - 1;
    END IF;
    RETURN V_RETURNSHIFTCODE;
  END GETLASTSHIFT;

  --???SQL
  FUNCTION GETLOSTMANHOURSQL RETURN VARCHAR2 IS
    V_LOSTMANHOURSQL VARCHAR2(2000);
  BEGIN
    V_LOSTMANHOURSQL := '';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        ' SELECT A.ACTMANHOUR,B.ACQUIREMANHOUR,DECODE(B.ACQUIREMANHOUR,NULL,A.ACTMANHOUR,';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        ' (A.ACTMANHOUR - B.ACQUIREMANHOUR)) AS LOSTMANHOUR,B.ACTOUTPUT,';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        ' A.SSCODE,A.SHIFTDATE,A.SHIFTCODE,A.ITEMCODE';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '   FROM (SELECT SUM(D.MANCOUNT * D.DURATION) AS ACTMANHOUR,D.SSCODE,D.SHIFTDATE,';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '             D.SHIFTCODE,MO.ITEMCODE';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '          FROM TBLPRODDETAIL D, TBLMO MO WHERE D.MOCODE = MO.MOCODE';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '          GROUP BY D.SSCODE, D.SHIFTDATE, D.SHIFTCODE, MO.ITEMCODE) A';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '  LEFT JOIN (SELECT SUM(Q.LINEOUTPUTCOUNT * NVL(P.WORKINGTIME, 0)) AS ACQUIREMANHOUR,';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '                     SUM(Q.LINEOUTPUTCOUNT) AS ACTOUTPUT,T.SSCODE,Q.SHIFTDAY,T.SHIFTCODE,';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '                     Q.ITEMCODE FROM TBLRPTSOQTY Q';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '               INNER JOIN TBLMESENTITYLIST T ON Q.TBLMESENTITYLIST_SERIAL = T.SERIAL ';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '               LEFT JOIN TBLPLANWORKTIME P ON P.ITEMCODE = Q.ITEMCODE ';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '                                              AND  T.SSCODE = P.SSCODE';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '              GROUP BY T.SSCODE, Q.SHIFTDAY, T.SHIFTCODE, Q.ITEMCODE) B';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '  ON A.SSCODE =B.SSCODE  AND A.SHIFTDATE =B.SHIFTDAY';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                        '     AND A.SHIFTCODE = B.SHIFTCODE   AND A.ITEMCODE =B.ITEMCODE';
    V_LOSTMANHOURSQL := V_LOSTMANHOURSQL || '  WHERE 1=1';
    RETURN V_LOSTMANHOURSQL;
  END GETLOSTMANHOURSQL;
  
  
    --GetShiftDay
    FUNCTION GetShiftDay(p_ShiftTypeCode IN VARCHAR2, p_CurrDateTime IN DATE)
    RETURN NUMBER
    IS
        v_CurrTime              NUMBER;
        v_IsOverDay             NUMBER;
    BEGIN
        v_CurrTime := TO_NUMBER(TO_CHAR(p_CurrDateTime, 'HH24MISS'));        

        SELECT COUNT(*) INTO v_IsOverDay 
        FROM tbltp
        WHERE shifttypecode = p_shiftTypeCode
        AND isoverdate = 1
        AND (CASE WHEN tpbtime > tpetime THEN tpbtime - 240000 ELSE tpbtime END) <= v_CurrTime
        AND tpetime >= v_CurrTime;
        v_IsOverDay := NVL(v_IsOverDay, 0);
        
        IF (v_IsOverDay > 0) THEN
            RETURN TO_NUMBER(TO_CHAR(p_CurrDateTime - 1, 'YYYYMMDD'));
        ELSE
            RETURN TO_NUMBER(TO_CHAR(p_CurrDateTime, 'YYYYMMDD'));
        END IF;
        
    EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20000, '[GetShiftDay]' || SQLERRM);
    END GetShiftDay;

    
  --????È˙2??Íß1?Í±
  PROCEDURE COMPUTELOSTMANHOUR(runDate IN DATE := SYSDATE) IS
    V_NOWSHIFTCODE     VARCHAR2(40);
    V_NOWSHIFTTYPECODE VARCHAR(40);
    I_NOWSHIFTSEQ      INT;
    I_MINSHIFTSEQ      INT;
    V_LSATSHIFTCODE    VARCHAR2(40);
  
    V_SSCODE            VARCHAR2(40);
    V_SHIFTTYPECODEINSS VARCHAR2(40);
    I_GETROWS           INT;
    I_SHIFTDAY          INT;
    V_LOSTMANHOURSQL    VARCHAR2(2000);
    TYPE CURSOR_DYNAMICSELECT IS REF CURSOR;
    LOSTMANHOURLIST CURSOR_DYNAMICSELECT;
  
    --TBLLOSTMANHOUR
    V_SSCODE_INLOSTMANHOUR    VARCHAR2(40);
    V_SHIFTDAY_INLOSTMANHOUR  VARCHAR2(40);
    V_SHIFTCODE_INLOSTMANHOUR VARCHAR2(40);
    V_ITEMCODE                VARCHAR2(40);
    N_ACTMANHOUR              NUMBER(10);
    N_ACTOUTPUT               NUMBER(10);
    N_ACQUIREMANHOUR          NUMBER(10);
    N_LOSTMANHOUR             NUMBER(10);
  
    --TBLJOBLOG
    V_JOBID         VARCHAR2(40);
    D_STARTDATETIME DATE := SYSDATE;
    D_ENDDATETIME   DATE;
    I_USEDTIME      NUMBER(22);
    I_PROCESSCOUNT  NUMBER(22);
    V_RESULT        VARCHAR2(40);
    V_ERRORMSG      VARCHAR2(500);
    V_BEGINTIME     NUMBER(20) := DBMS_UTILITY.GET_TIME;
    V_ENDTIME       NUMBER(20);
    I_ROWCOUNT      INT := 0;
  
  --GetshiftCode
    CURSOR SHIFT_CURSOR IS
      SELECT DISTINCT shiftcode,shifttypecode
      FROM TBLTP
      WHERE ((TPBTIME < TPETIME AND  TO_NUMBER(TO_CHAR(runDate, 'hh24miss')) BETWEEN TPBTIME AND TPETIME) 
            OR (TPBTIME > TPETIME AND  TO_NUMBER(TO_CHAR(runDate, 'hh24miss')) < TPBTIME 
               AND TO_NUMBER(TO_CHAR(runDate, 'hh24miss')) + 240000 BETWEEN   TPBTIME AND TPETIME + 240000) 
            OR (TPBTIME > TPETIME  AND  TO_NUMBER(TO_CHAR(runDate, 'hh24miss')) > TPBTIME 
            AND TO_NUMBER(TO_CHAR(runDate, 'hh24miss')) BETWEEN TPBTIME AND TPETIME + 240000));
  
    CURSOR SS_CURSOR IS
      SELECT DISTINCT SSCODE, SHIFTTYPECODE FROM TBLSS;
  
  BEGIN
    OPEN SHIFT_CURSOR;
    LOOP
      FETCH SHIFT_CURSOR
        INTO V_NOWSHIFTCODE, V_NOWSHIFTTYPECODE;
      EXIT WHEN SHIFT_CURSOR%NOTFOUND;
      
      SELECT nvl(shiftseq,0) INTO I_NOWSHIFTSEQ FROM tblshift WHERE shiftcode=V_NOWSHIFTCODE;
      --GetLastShiftCode
      V_LSATSHIFTCODE := GETLASTSHIFT(V_NOWSHIFTCODE,
                                      V_NOWSHIFTTYPECODE,
                                      I_NOWSHIFTSEQ);
      --end      
      --GetLastShiftDay      
       I_SHIFTDAY := GETSHIFTDAY(V_NOWSHIFTTYPECODE,runDate);
        SELECT MIN(SHIFTSEQ)
          INTO I_MINSHIFTSEQ
          FROM TBLSHIFT
         WHERE SHIFTTYPECODE = V_NOWSHIFTTYPECODE;
      
        IF I_MINSHIFTSEQ = I_NOWSHIFTSEQ THEN
          I_SHIFTDAY := I_SHIFTDAY - 1;
        END IF;                               
      --end
                                     
      OPEN SS_CURSOR;
      LOOP
        FETCH SS_CURSOR
          INTO V_SSCODE, V_SHIFTTYPECODEINSS;
        EXIT WHEN SS_CURSOR%NOTFOUND;
            
        --????˘Û???∞‡?????2??
        IF V_SHIFTTYPECODEINSS = V_NOWSHIFTTYPECODE THEN
          SELECT COUNT(*)
            INTO I_GETROWS
            FROM TBLLOSTMANHOUR
           WHERE SSCODE = V_SSCODE
             AND SHIFTCODE = V_LSATSHIFTCODE
             AND shiftdate=I_SHIFTDAY;
          IF I_GETROWS <= 0 THEN
            --GetSQL
            V_LOSTMANHOURSQL := GETLOSTMANHOURSQL();
          
            IF NVL(V_SSCODE, ' ') <> ' ' THEN
              V_LOSTMANHOURSQL := V_LOSTMANHOURSQL || ' AND A.SSCODE =''' ||
                                  V_SSCODE || '''  ';
            END IF;
          
            IF NVL(V_LSATSHIFTCODE, ' ') <> ' ' THEN
              V_LOSTMANHOURSQL := V_LOSTMANHOURSQL ||
                                  ' AND A.SHIFTCODE =''' || V_LSATSHIFTCODE ||
                                  '''  ';
            END IF;
          
            IF I_SHIFTDAY > 0 THEN
              V_LOSTMANHOURSQL := V_LOSTMANHOURSQL || ' AND A.SHIFTDATE =' ||
                                  I_SHIFTDAY || '  ';
            END IF;
          
            OPEN LOSTMANHOURLIST FOR V_LOSTMANHOURSQL;
            LOOP
              FETCH LOSTMANHOURLIST
                INTO N_ACTMANHOUR, N_ACQUIREMANHOUR, N_LOSTMANHOUR, N_ACTOUTPUT, V_SSCODE_INLOSTMANHOUR, V_SHIFTDAY_INLOSTMANHOUR, V_SHIFTCODE_INLOSTMANHOUR, V_ITEMCODE;
              EXIT WHEN LOSTMANHOURLIST%NOTFOUND;
            
              INSERT INTO TBLLOSTMANHOUR
                (SSCODE,
                 SHIFTDATE,
                 SHIFTCODE,
                 ITEMCODE,
                 ACTMANHOUR,
                 ACTOUTPUT,
                 ACQUIREMANHOUR,
                 LOSTMANHOUR,
                 MUSER,
                 MDATE,
                 MTIME)
              VALUES
                (NVL(V_SSCODE_INLOSTMANHOUR, ' '),
                 NVL(V_SHIFTDAY_INLOSTMANHOUR, ' '),
                 NVL(V_SHIFTCODE_INLOSTMANHOUR, ' '),
                 NVL(V_ITEMCODE, ' '),
                 NVL(N_ACTMANHOUR, 0),
                 NVL(N_ACTOUTPUT, 0),
                 NVL(N_ACQUIREMANHOUR, 0),
                 NVL(N_LOSTMANHOUR, 0),
                 'SYSTEM',
                 TO_NUMBER(TO_CHAR(SYSDATE, 'yyyyMMdd')),
                 TO_NUMBER(TO_CHAR(SYSDATE, 'hh24miss')));
              I_ROWCOUNT := I_ROWCOUNT + 1;
            END LOOP;
            CLOSE LOSTMANHOURLIST;
          END IF;
        END IF;
      
      END LOOP;
      CLOSE SS_CURSOR;
    END LOOP;
    CLOSE SHIFT_CURSOR;
    COMMIT;
  
    V_JOBID        := 'JOB_ComputeLostManHour';
    D_ENDDATETIME  := SYSDATE;
    V_ENDTIME      := DBMS_UTILITY.GET_TIME;
    I_USEDTIME     := (V_ENDTIME - V_BEGINTIME) * 10;
    I_PROCESSCOUNT := I_ROWCOUNT;
    V_RESULT       := 'OK';
    V_ERRORMSG     := '';
  
    INSERT INTO TBLJOBLOG
      (JOBID,
       STARTDATETIME,
       ENDDATETIME,
       USEDTIME,
       PROCESSCOUNT,
       RESULT,
       ERRORMSG)
    VALUES
      (V_JOBID,
       D_STARTDATETIME,
       D_ENDDATETIME,
       I_USEDTIME,
       I_PROCESSCOUNT,
       V_RESULT,
       V_ERRORMSG);
    COMMIT;
  
  EXCEPTION
    WHEN OTHERS THEN
      ROLLBACK;
    
      V_JOBID        := 'JOB_ComputeLostManHour';
      D_ENDDATETIME  := SYSDATE;
      V_ENDTIME      := DBMS_UTILITY.GET_TIME;
      I_USEDTIME     := (V_ENDTIME - V_BEGINTIME) * 10;
      I_PROCESSCOUNT := I_ROWCOUNT;
      V_RESULT       := 'FAIL';
      V_ERRORMSG     := SQLERRM;
    
      INSERT INTO TBLJOBLOG
        (JOBID,
         STARTDATETIME,
         ENDDATETIME,
         USEDTIME,
         PROCESSCOUNT,
         RESULT,
         ERRORMSG)
      VALUES
        (V_JOBID,
         D_STARTDATETIME,
         D_ENDDATETIME,
         I_USEDTIME,
         I_PROCESSCOUNT,
         V_RESULT,
         V_ERRORMSG);
      COMMIT;
    
      RETURN;
  END COMPUTELOSTMANHOUR;
END PKG_COMPUTELOSTMANHOUR;

/

SHOW ERRORS;

CREATE OR REPLACE PROCEDURE Venus_Test_PRO
AS
    i   NUMBER;
    v_rcard VARCHAR2(40);
    v_rcardseq  NUMBER;
    v_mocode    VARCHAR2(40);
    TYPE cursor_test  IS REF CURSOR;
    cursor1     cursor_test;
    v_SQL   VARCHAR2(2000);
    begindate   NUMBER;
    enddate     NUMBER;
BEGIN
    v_SQL := '';
    begindate := TO_NUMBER(TO_CHAR(current_timestamp,'YYYYMMDDHH24MISSFF'))/1000000;
    FOR i IN 1..3000
    LOOP
        BEGIN        
            SELECT DISTINCT RCARD,MOCODE
              INTO v_rcard,v_mocode
              FROM tblonwip
             WHERE serial=i;
        EXCEPTION
            WHEN NO_DATA_FOUND THEN
                GOTO endloop;
        END;
        
        v_SQL :=
            'SELECT   rcard, rcardseq, mocode'
         || '   FROM tblonwip'
         || '  WHERE rcard=''' || v_rcard || ''' AND mocode=''' || v_mocode || ''' AND serial <= '
         || i
         || '  ORDER BY rcardseq DESC';
         
        OPEN cursor1 FOR v_SQL;
        LOOP
            FETCH cursor1
             INTO v_rcard,v_rcardseq,v_mocode;
            
            EXIT WHEN cursor1%NOTFOUND;
        END LOOP;
		CLOSE cursor1;
		
		<<endloop>>
		  NULL;
    END LOOP;
    enddate:=TO_NUMBER(TO_CHAR(current_timestamp,'YYYYMMDDHH24MISSFF'))/1000000;
    
    DBMS_OUTPUT.Put_Line( TO_CHAR((enddate-begindate)) || 'ms' );  

END Venus_Test_PRO;

/

SHOW ERRORS;

CREATE OR REPLACE PROCEDURE CHANGERCARD(i_FromRCard IN VARCHAR2,i_ToRCard IN VARCHAR2,o_Result OUT VARCHAR2)
AS
    v_intCount        INT;
BEGIN
    v_intCount := 0;
	/* 
    SELECT COUNT(*)
      INTO v_intCount
      FROM TBLONWIP
     WHERE (rcard <> tcard
        OR rcard <> scard
        OR tcard <> scard) AND rcard = i_FromRCard;
    IF v_intCount > 0 THEN
        o_Result := 'Error:tblonwip';
        RETURN;
    END IF;
    
    SELECT COUNT(*)
      INTO v_intCount
      FROM TBLREJECT
     WHERE (rcard <> tcard
        OR rcard <> scard
        OR tcard <> scard) AND rcard = i_FromRCard;
    IF v_intCount > 0 THEN
        o_Result := 'Error:tblreject';
        RETURN;
    END IF;
    
   SELECT COUNT(*)
      INTO v_intCount
      FROM TBLONWIPCARDTRANS
     WHERE (rcard <> tcard
        OR rcard <> scard
        OR tcard <> scard) AND rcard = i_FromRCard;
    IF v_intCount > 0 THEN
        o_Result := 'Error:tblonwipcardtrans';
        RETURN;
    END IF;
    
    SELECT COUNT(*)
      INTO v_intCount
      FROM TBLSIMULATION
     WHERE (rcard <> tcard
        OR rcard <> scard
        OR tcard <> scard) AND rcard = i_FromRCard;
    IF v_intCount > 0 THEN
        o_Result := 'Error:tblsimulation';
        RETURN;
    END IF;
    
    SELECT COUNT(*)
      INTO v_intCount
      FROM TBLSIMULATIONREPORT
     WHERE (rcard <> tcard
        OR rcard <> scard
        OR tcard <> scard) AND rcard = i_FromRCard;
    IF v_intCount > 0 THEN
        o_Result := 'Error:tblsimulationreport';
        RETURN;
    END IF;
    
    SELECT COUNT(*)
      INTO v_intCount
      FROM TBLTS
     WHERE (rcard <> tcard
        OR rcard <> scard
        OR tcard <> scard) AND rcard = i_FromRCard;
    IF v_intCount > 0 THEN
        o_Result := 'Error:tblts';
        RETURN;
		*/
    END IF;
    
    UPDATE TBLFROZEN SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLINVRCARD SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLLOT2CARD SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLLOT2CARDCHECK SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLMORCARD SET MORCARDEND=i_ToRCard WHERE MORCARDEND=i_FromRCard;
    UPDATE TBLMORCARD SET MORCARDSTART=i_ToRCard WHERE MORCARDSTART=i_FromRCard;
    UPDATE TBLOFFMOCARD SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    UPDATE TBLONWIP SET RCARD=i_ToRCard,TCARD=i_ToRCard,SCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLONWIPCARDTRANS SET SCARD=i_ToRCard WHERE SCARD=i_FromRCard;
	UPDATE TBLONWIPCARDTRANS SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
	UPDATE TBLONWIPCARDTRANS SET TCARD=i_ToRCard WHERE TCARD=i_FromRCard;
    
    UPDATE TBLONWIPCARTON SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLONWIPCFGCOLLECT SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLONWIPECN SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLONWIPITEM SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLONWIPSOFTVER SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLONWIPTRY SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    UPDATE TBLONWIPUNDO SET RCARD=i_ToRCard,TCARD=i_ToRCard,SCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    UPDATE TBLOQCCARDLOTCKLIST SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLOQCLOTCARD2ERRORCODE SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLPACKINGCHK SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLPALLET2RCARD SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    UPDATE TBLREJECT SET RCARD=i_ToRCard,TCARD=i_ToRCard,SCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    UPDATE TBLREJECT2ERRORCODE SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLREWORKRANGE SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    UPDATE TBLSIMULATION SET RCARD=i_ToRCard,TCARD=i_ToRCard,SCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLSIMULATIONREPORT SET RCARD=i_ToRCard,TCARD=i_ToRCard,SCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    UPDATE TBLSIMULATIONREPORT SET LOADEDRCARD=i_ToRCard WHERE LOADEDRCARD=i_FromRCard;

    UPDATE TBLTS SET RCARD=i_ToRCard,TCARD=i_ToRCard,SCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLTS SET RRCARD=i_ToRCard WHERE RRCARD=i_FromRCard;
    
    UPDATE TBLTSERRORCAUSE SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLTSERRORCAUSE2COM SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLTSERRORCAUSE2EPART SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLTSERRORCAUSE2LOC SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLTSERRORCODE SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLTSERRORCODE2LOC SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLVERSIONCOLLECT SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLVERSIONERROR SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLDOWN SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLSTACK2RCARD SET SERIALNO=i_ToRCard WHERE SERIALNO=i_FromRCard;
    UPDATE TBLPAUSE2RCARD SET SERIALNO=i_ToRCard WHERE SERIALNO=i_FromRCard;
    UPDATE TBLTEMPREWORKRCARD SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    UPDATE TBLTRY2RCARD SET RCARD=i_ToRCard WHERE RCARD=i_FromRCard;
    
    o_Result:='OK';
    RETURN;
EXCEPTION
    WHEN OTHERS THEN
        o_Result := SQLERRM;
        RETURN;
END ChangeRCard;

/

SHOW ERRORS;

CREATE OR REPLACE PROCEDURE ASN_2_IQC
IS
   v_stno               VARCHAR2 (40);
   v_vendorcode         VARCHAR2 (40);
   v_vendorname         VARCHAR2 (100);
   v_ststatus           VARCHAR2 (40);
   v_orgid              NUMBER;
   v_invuser            VARCHAR2 (100);
   v_stline             NUMBER;
   v_itemcode           VARCHAR2 (40);
   v_itemname           VARCHAR2 (100);
   v_orderno            VARCHAR2 (40);
   v_orderline          NUMBER;
   v_plandate           NUMBER;
   v_planqty            NUMBER;
   v_stdstatus          VARCHAR2 (40);
   v_receiveqty         NUMBER;
   v_checkstatus        VARCHAR2 (15);
   v_unit               VARCHAR2 (40);
   v_memo               VARCHAR2 (100);
   v_previous_invuser   VARCHAR2 (100);
   v_current_invuser    VARCHAR2 (100);
   v_iqcno              VARCHAR2 (50);

   CURSOR asn_cursor
   IS
      SELECT stno, vendorcode, vendorname, ststatus, orgid
        FROM asn@mes2 originalasn, tblorg
       WHERE tblorg.orgid = originalasn.plantcode
         AND UPPER (ststatus) = 'WAITCHECK'
         AND NOT EXISTS (SELECT stno
                           FROM tblasn
                          WHERE stno = originalasn.stno);

   CURSOR iqc_cursor
   IS
      SELECT DISTINCT i.invuser, stline, itemcode, itemname, snd.orderno,
                      snd.orderline, plandate, planqty, stdstatus, receiveqty,
                      checkstatus, unit, memo
                 FROM asndetail@mes2 snd INNER JOIN asn@mes2 sn ON sn.stno =
                                                                      snd.stno
                      LEFT JOIN orderdetail@mes2 od ON od.orderno =
                                                                   snd.orderno
                                                  AND od.orderline =
                                                                 snd.orderline
                      LEFT JOIN inv@mes2 i ON i.plantcode = sn.plantcode
                                         AND i.companycode = sn.companycode
                                         AND i.itemcode = snd.itemcode
                                         AND od.shipinv = i.wh
                WHERE 1 = 1 AND sn.stno = v_stno
             ORDER BY invuser, snd.orderno ASC, snd.orderline ASC;
BEGIN
   OPEN asn_cursor;

--asn ??
   LOOP
      FETCH asn_cursor
       INTO v_stno, v_vendorcode, v_vendorname, v_ststatus, v_orgid;

      EXIT WHEN asn_cursor%NOTFOUND;
      v_previous_invuser := ' ';
      v_current_invuser := ' ';

      --insert asn ??
      INSERT INTO tblasn
                  (stno, vendorcode, vendorname, ststatus, orgid,
                   mdate,
                   mtime, muser, syncstatus
                  )
           VALUES (v_stno, v_vendorcode, v_vendorname, v_ststatus, v_orgid,
                   TO_NUMBER (TO_CHAR (SYSDATE, 'yyyyMMdd')),
                   TO_NUMBER (TO_CHAR (SYSDATE, 'hh24mmss')), 'ADMIN', 'NEW'
                  );

--?????
      OPEN iqc_cursor;

      LOOP
         FETCH iqc_cursor
          INTO v_invuser, v_stline, v_itemcode, v_itemname, v_orderno,
               v_orderline, v_plandate, v_planqty, v_stdstatus, v_receiveqty,
               v_checkstatus, v_unit, v_memo;

         EXIT WHEN iqc_cursor%NOTFOUND;
         v_current_invuser := v_invuser;

         IF (v_current_invuser <> v_previous_invuser)
         THEN
--?????
            v_previous_invuser := v_current_invuser;
            v_iqcno := v_stno || '_' || v_stline;

            INSERT INTO tblasniqc
                        (iqcno, stno, invuser, status,
                         appdate,
                         apptime,
                         mdate,
                         mtime, muser,
                         rohs
                        )
                 VALUES (v_iqcno, v_stno, v_invuser, v_ststatus,
                         TO_NUMBER (TO_CHAR (SYSDATE, 'yyyyMMdd')),
                         TO_NUMBER (TO_CHAR (SYSDATE, 'hh24mmss')),
                         TO_NUMBER (TO_CHAR (SYSDATE, 'yyyyMMdd')),
                         TO_NUMBER (TO_CHAR (SYSDATE, 'hh24mmss')), 'ADMIN',
                         'False'
                        );
         END IF;

         INSERT INTO tbliqcdetail
                     (iqcno, stno, stline, itemcode, itemname,
                      orderno, orderline, plandate, planqty,
                      stdstatus, receiveqty, checkstatus, unit,
                      memo, muser,
                      mdate,
                      mtime
                     )
              VALUES (v_iqcno, v_stno, v_stline, v_itemcode, v_itemname,
                      v_orderno, v_orderline, v_plandate, v_planqty,
                      v_stdstatus, v_receiveqty, v_checkstatus, v_unit,
                      v_memo, 'ADMIN',
                      TO_NUMBER (TO_CHAR (SYSDATE, 'yyyyMMdd')),
                      TO_NUMBER (TO_CHAR (SYSDATE, 'hh24mmss'))
                     );
      END LOOP;

      CLOSE iqc_cursor;
   END LOOP;

   CLOSE asn_cursor;

   DBMS_OUTPUT.put_line ('execute end!');
END asn_2_iqc;

/

SHOW ERRORS;

CREATE OR REPLACE PROCEDURE AUTOCLOSEMO IS
  V_MOCODE        VARCHAR2(40);
  I_MOCOUNT       NUMBER(10);
  V_JOBID         VARCHAR2(40);
  D_STARTDATETIME DATE;
  D_ENDDATETIME   DATE;
  I_USEDTIME      NUMBER(22);
  I_PROCESSCOUNT  NUMBER(22);
  V_RESULT        VARCHAR2(40);
  V_ERRORMSG      VARCHAR2(500);
  V_BEGINTIME     NUMBER(20);
  V_ENDTIME       NUMBER(20);

  CURSOR MOCODE_CURSOR IS
    SELECT DISTINCT MOCODE
      FROM TBLMO
     WHERE MOSTATUS = 'mostatus_open'
       AND MOPLANQTY = MOACTQTY + MOSCRAPQTY + OFFMOQTY
       AND MOINPUTQTY>=MOPLANQTY
       AND MOCODE IN
           (SELECT A.MOCODE
              FROM (SELECT SUM(TBLMO2SAP.MOPRODUCED) AS MOPRODUCEDTOTAL, MOCODE
                      FROM TBLMO2SAP
                     GROUP BY MOCODE) A,
                   TBLMO B
             WHERE A.MOCODE = B.MOCODE
               AND A.MOPRODUCEDTOTAL = B.MOACTQTY)
     ORDER BY MOCODE;

BEGIN
  D_STARTDATETIME := SYSDATE;
  V_BEGINTIME     := DBMS_UTILITY.GET_TIME;
  I_MOCOUNT       := 0;

  OPEN MOCODE_CURSOR;
  LOOP
    FETCH MOCODE_CURSOR
      INTO V_MOCODE;
    EXIT WHEN MOCODE_CURSOR%NOTFOUND;

    UPDATE TBLMO
       SET MOSTATUS     = 'mostatus_close',
           MOACTENDDATE = TO_NUMBER(TO_CHAR(SYSDATE, 'yyyyMMdd')),
           Mdate=TO_NUMBER(TO_CHAR(SYSDATE, 'yyyyMMdd')),
           mtime=TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss')),
           muser='SYSTEM'
     WHERE MOCODE = V_MOCODE;

    DELETE FROM TBLSIMULATION WHERE MOCODE = V_MOCODE;
    DELETE FROM TBLMOBOM WHERE MOCODE = V_MOCODE;

    I_MOCOUNT := I_MOCOUNT + 1;
  END LOOP;
  CLOSE MOCODE_CURSOR;

  COMMIT;

  V_JOBID        := 'JOBAUTOCLOSEMO';
  D_ENDDATETIME  := SYSDATE;
  V_ENDTIME      := DBMS_UTILITY.GET_TIME;
  I_USEDTIME     := (V_ENDTIME - V_BEGINTIME) * 10;
  I_PROCESSCOUNT := I_MOCOUNT;
  V_RESULT       := 'OK';
  V_ERRORMSG     := '';

  INSERT INTO TBLJOBLOG
    (JOBID,
     STARTDATETIME,
     ENDDATETIME,
     USEDTIME,
     PROCESSCOUNT,
     RESULT,
     ERRORMSG)
  VALUES
    (V_JOBID,
     D_STARTDATETIME,
     D_ENDDATETIME,
     I_USEDTIME,
     I_PROCESSCOUNT,
     V_RESULT,
     V_ERRORMSG);
  COMMIT;

  RETURN;

EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;

    V_JOBID        := 'JOBAUTOCLOSEMO';
    D_ENDDATETIME  := SYSDATE;
    V_ENDTIME      := DBMS_UTILITY.GET_TIME;
    I_USEDTIME     := (V_ENDTIME - V_BEGINTIME) * 10;
    I_PROCESSCOUNT := I_MOCOUNT;
    V_RESULT       := 'FAIL';
    V_ERRORMSG     := 'SQLERRM';

    INSERT INTO TBLJOBLOG
      (JOBID,
       STARTDATETIME,
       ENDDATETIME,
       USEDTIME,
       PROCESSCOUNT,
       RESULT,
       ERRORMSG)
    VALUES
      (V_JOBID,
       D_STARTDATETIME,
       D_ENDDATETIME,
       I_USEDTIME,
       I_PROCESSCOUNT,
       V_RESULT,
       V_ERRORMSG);
    COMMIT;

    RETURN;
END AUTOCLOSEMO;

/

SHOW ERRORS;

CREATE OR REPLACE PROCEDURE UPDATEMKEYPATT IS
  V_MITEMCODE VARCHAR2(40);
  V_MITEMNAME VARCHAR2(100);

  CURSOR MITEMCODE_CURSOR IS
   SELECT DISTINCT MITEMCODE
  FROM TBLMKEYPART
 WHERE NVL(MITEMNAME, ' ') = ' '
 ORDER BY MITEMCODE;
    
     BEGIN
    
     OPEN MITEMCODE_CURSOR;
  LOOP FETCH MITEMCODE_CURSOR INTO V_MITEMCODE;
  EXIT WHEN MITEMCODE_CURSOR%NOTFOUND;

  SELECT mname INTO V_MITEMNAME FROM tblmaterial WHERE mcode=V_MITEMCODE;
  UPDATE TBLMKEYPART SET MITEMNAME = V_MITEMNAME WHERE MITEMCODE = V_MITEMCODE;

END LOOP; CLOSE MITEMCODE_CURSOR; COMMIT; RETURN;

EXCEPTION
WHEN OTHERS THEN ROLLBACK;

END UPDATEMKEYPATT;

/

SHOW ERRORS;

CREATE OR REPLACE PROCEDURE COMPUTELOSTMANHOUR IS
  V_NOWSHIFTCODE                VARCHAR2(40);
  V_NOWSHIFTTYPECODE            VARCHAR(40);
  I_NOWSHIFTSEQ                 INT;  
  I_MINSHIFTSEQ                 INT;
  V_LSATSHIFTCODE               VARCHAR2(40);
  
  V_SSCODE                      VARCHAR2(40);
  V_SHIFTTYPECODEINSS           VARCHAR2(40);
  I_GETROWS                     INT;
  I_SHIFTDAY                    INT;
  V_LOSTMANHOURSQL              VARCHAR2(1000);
  TYPE cursor_DynamicSelect     IS REF CURSOR;
  LOSTMANHOURLIST               cursor_DynamicSelect;
  
  --TBLLOSTMANHOUR
  V_SSCODE_INLOSTMANHOUR        VARCHAR2(40);
  V_SHIFTDAY_INLOSTMANHOUR      VARCHAR2(40);
  V_SHIFTCODE_INLOSTMANHOUR     VARCHAR2(40);
  V_ITEMCODE                    VARCHAR2(40);
  N_ACTMANHOUR                  NUMBER(10);
  N_ACTOUTPUT                   NUMBER(10);
  N_ACQUIREMANHOUR              NUMBER(10);
  N_LOSTMANHOUR                 NUMBER(10);  
  
  --TBLJOBLOG
  V_JOBID                       VARCHAR2(40);
  D_STARTDATETIME               DATE;
  D_ENDDATETIME                 DATE;
  I_USEDTIME                    NUMBER(22);
  I_PROCESSCOUNT                NUMBER(22);
  V_RESULT                      VARCHAR2(40);
  V_ERRORMSG                    VARCHAR2(500);
  V_BEGINTIME                   NUMBER(20);
  V_ENDTIME                     NUMBER(20);
  I_ROWCOUNT                    INT:=0;
  
  CURSOR SHIFT_CURSOR IS
    SELECT DISTINCT SHIFTCODE, SHIFTTYPECODE, SHIFTSEQ
      FROM TBLSHIFT
     WHERE SHIFTBTIME <= TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss'))
       AND SHIFTETIME >= TO_NUMBER(TO_CHAR(SYSDATE, 'hh24mmss'));
    
  CURSOR SS_CURSOR IS
    SELECT DISTINCT SSCODE, SHIFTTYPECODE  FROM TBLSS; 
         
 BEGIN             
    OPEN SHIFT_CURSOR; 
    LOOP 
      FETCH SHIFT_CURSOR
      INTO V_NOWSHIFTCODE, V_NOWSHIFTTYPECODE, I_NOWSHIFTSEQ;
      EXIT  WHEN SHIFT_CURSOR%NOTFOUND;
    --???????∞‡??
      V_LSATSHIFTCODE:=GETLASTSHIFT(V_NOWSHIFTCODE,V_NOWSHIFTTYPECODE,I_NOWSHIFTSEQ);
      OPEN SS_CURSOR;
      LOOP 
        FETCH SS_CURSOR
        INTO V_SSCODE,V_SHIFTTYPECODEINSS;
        EXIT WHEN SS_CURSOR%NOTFOUND;  
        --???∞‡???Û˙?a?2???Í±2????˙È???Íß1?Í±
        IF V_SHIFTTYPECODEINSS=V_NOWSHIFTTYPECODE THEN
           SELECT COUNT(*) INTO I_GETROWS FROM TBLLOSTMANHOUR WHERE  SSCODE=V_SSCODE AND SHIFTCODE=V_LSATSHIFTCODE;           
           IF I_GETROWS<=0 THEN
             I_SHIFTDAY:=pkg_MESWarning.GetShiftDay(V_NOWSHIFTTYPECODE,Sysdate);
             SELECT MIN(SHIFTSEQ) INTO I_MINSHIFTSEQ FROM TBLSHIFT WHERE SHIFTTYPECODE=V_NOWSHIFTTYPECODE;
             
             IF I_MINSHIFTSEQ=I_NOWSHIFTSEQ THEN
                 I_SHIFTDAY:=I_SHIFTDAY-1;
             END IF;
             --??SQL
             V_LOSTMANHOURSQL:=GETLOSTMANHOURSQL(); 
             
             IF NVL(V_SSCODE,' ')<>' ' THEN
             V_LOSTMANHOURSQL:=V_LOSTMANHOURSQL || ' AND A.SSCODE =''' || V_SSCODE || '''  ';
             END IF;
             
             IF NVL(V_LSATSHIFTCODE,' ')<>' ' THEN
             V_LOSTMANHOURSQL:=V_LOSTMANHOURSQL || ' AND A.SHIFTCODE =''' || V_LSATSHIFTCODE || '''  ';
             END IF;
             
             IF I_SHIFTDAY>0 THEN
             V_LOSTMANHOURSQL:=V_LOSTMANHOURSQL || ' AND A.SHIFTDATE =' || I_SHIFTDAY || '  ';
             END IF;
          
             OPEN  LOSTMANHOURLIST FOR V_LOSTMANHOURSQL;
             LOOP FETCH   LOSTMANHOURLIST INTO N_ACTMANHOUR,N_ACQUIREMANHOUR,N_LOSTMANHOUR,N_ACTOUTPUT,
                                               V_SSCODE_INLOSTMANHOUR,V_SHIFTDAY_INLOSTMANHOUR,V_SHIFTCODE_INLOSTMANHOUR,V_ITEMCODE;
             EXIT WHEN LOSTMANHOURLIST%NOTFOUND;
             
             INSERT INTO TBLLOSTMANHOUR (SSCODE,
                                        SHIFTDATE,
                                        SHIFTCODE,
                                        ITEMCODE,
                                        ACTManHour,
                                        ACTOutPut,
                                        AcquireManHour,
                                        LOSTMANHOUR,
                                        MUSER,
                                        MDATE,
                                        MTIME)
                                 VALUES(
                                        NVL(V_SSCODE_INLOSTMANHOUR,' '),
                                        NVL(V_SHIFTDAY_INLOSTMANHOUR,' '),
                                        NVL(V_SHIFTCODE_INLOSTMANHOUR,' '),
                                        NVL(V_ITEMCODE,' '),
                                        NVL(N_ACTMANHOUR,0),
                                        NVL(N_ACTOUTPUT,0),
                                        NVL(N_ACQUIREMANHOUR,0),
                                        NVL(N_LOSTMANHOUR,0),
                                        'SYSTEM',
                                        TO_NUMBER(TO_CHAR(SYSDATE,'yyyyMMdd')),
                                        TO_NUMBER(TO_CHAR(SYSDATE,'hh24mmss'))
                                        ); 
               I_ROWCOUNT:=I_ROWCOUNT+1;
             END LOOP;
             CLOSE LOSTMANHOURLIST;
           END IF;
        END IF;
       
      END LOOP;
      CLOSE SS_CURSOR;
    END LOOP;
    CLOSE SHIFT_CURSOR;
    COMMIT;
    
    V_JOBID        := 'COMPUTELOSTMANHOUR';
    D_ENDDATETIME  := SYSDATE;
    V_ENDTIME      := DBMS_UTILITY.GET_TIME;
    I_USEDTIME     := (V_ENDTIME - V_BEGINTIME) * 10;
    I_PROCESSCOUNT := I_ROWCOUNT;
    V_RESULT       := 'OK';
    V_ERRORMSG     := '';
  
    INSERT INTO TBLJOBLOG
      (JOBID,
       STARTDATETIME,
       ENDDATETIME,
       USEDTIME,
       PROCESSCOUNT,
       RESULT,
       ERRORMSG)
    VALUES
      (V_JOBID,
       D_STARTDATETIME,
       D_ENDDATETIME,
       I_USEDTIME,
       I_PROCESSCOUNT,
       V_RESULT,
       V_ERRORMSG);
    COMMIT;
  
    EXCEPTION
  WHEN OTHERS THEN
    ROLLBACK;
   
    V_JOBID        := 'COMPUTELOSTMANHOUR';
    D_ENDDATETIME  := SYSDATE;
    V_ENDTIME      := DBMS_UTILITY.GET_TIME;
    I_USEDTIME     := (V_ENDTIME - V_BEGINTIME) * 10;
    I_PROCESSCOUNT := I_ROWCOUNT;
    V_RESULT       := 'FAIL';
    V_ERRORMSG     := 'SQLERRM';

    INSERT INTO TBLJOBLOG
      (JOBID,
       STARTDATETIME,
       ENDDATETIME,
       USEDTIME,
       PROCESSCOUNT,
       RESULT,
       ERRORMSG)
    VALUES
      (V_JOBID,
       D_STARTDATETIME,
       D_ENDDATETIME,
       I_USEDTIME,
       I_PROCESSCOUNT,
       V_RESULT,
       V_ERRORMSG);
    COMMIT;

    RETURN;    
END COMPUTELOSTMANHOUR;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION GETBOMVERSIONBYRCARD(p_rcard in VARCHAR2) 
RETURN VARCHAR2
AS
  M_VERSION VARCHAR2(40);
BEGIN
  SELECT mobom into M_VERSION FROM (SELECT mobom FROM tblsimulationreport,tblmo WHERE tblsimulationreport.mocode=tblmo.mocode AND rcard=p_rcard ORDER BY tblsimulationreport.mdate desc, tblsimulationreport.mtime desc) WHERE ROWNUM=1;
  RETURN M_VERSION;
END GETBOMVERSIONBYRCARD;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION getcartoncodebyrcard(p_rcard in VARCHAR2)
RETURN VARCHAR2
AS
  M_CARTONCODE VARCHAR2(40);
BEGIN
  select cartoncode into M_CARTONCODE from (select cartoncode from tblsimulationreport where rcard=p_rcard OR cartoncode=p_rcard order by mdate desc,mtime desc) where rownum=1;
  RETURN M_CARTONCODE;
END getcartoncodebyrcard;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION HIRO_TEST(p_rcard in VARCHAR2,RETURNTYPE IN VARCHAR2)
RETURN VARCHAR2
AS
MO_CODE VARCHAR2(40);
CUS_ORDERNO VARCHAR2(40);
TEMP_CARD VARCHAR2(40);
M_COUNT INT;
BEGIN

SELECT COUNT(*)
  INTO M_COUNT
  FROM (SELECT * FROM TBLSIMULATIONREPORT WHERE CARTONCODE=P_RCARD);
IF M_COUNT>0 THEN
    SELECT RCARD
        INTO TEMP_CARD
        FROM (SELECT * FROM TBLSIMULATIONREPORT WHERE CARTONCODE=P_RCARD);
    M_COUNT:=0;
END IF;

SELECT MOCODE,CUSORDERNO
  INTO MO_CODE,CUS_ORDERNO
  FROM (SELECT * FROM TBLMO WHERE MOCODE IN (SELECT MOCODE FROM (SELECT * FROM tblsimulationreport WHERE rcard IN (p_rcard,TEMP_CARD) ORDER BY mdate DESC, mtime DESC) WHERE ROWNUM = 1));

IF RETURNTYPE='MOCODE' THEN
    RETURN MO_CODE;
ELSE
    RETURN CUS_ORDERNO;
END IF;
END HIRO_TEST;




/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION GETMOCODEBYRCARD(p_rcard in VARCHAR2) 
RETURN VARCHAR2
AS
  M_MOCODE VARCHAR2(40);
BEGIN
  SELECT mocode into M_MOCODE FROM (SELECT mocode  FROM tblsimulationreport WHERE rcard=p_rcard ORDER BY mdate DESC,mtime DESC ) WHERE ROWNUM=1;
  RETURN M_MOCODE;
END GETMOCODEBYRCARD;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION GETFINISHEDDATEBYRCARD(p_rcard in VARCHAR2) 
RETURN NUMBER
AS
  M_DATE VARCHAR2(40);
BEGIN
  SELECT mdate into M_DATE FROM (SELECT mdate  FROM tblsimulationreport WHERE rcard=p_rcard AND iscom=1 ORDER BY mdate DESC,mtime DESC ) WHERE ROWNUM=1;
  RETURN M_DATE;
END GETFINISHEDDATEBYRCARD;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION product (input NUMBER)
   RETURN NUMBER PARALLEL_ENABLE
   AGGREGATE USING productimpl;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION GETLOTNOBYRCARD(p_rcard in VARCHAR2)
RETURN VARCHAR2
AS
  V_LOTNO VARCHAR2(40);
BEGIN
  SELECT lotno into V_LOTNO FROM (SELECT lotno FROM tblsimulationreport WHERE rcard=p_rcard ORDER BY tblsimulationreport.mdate desc, tblsimulationreport.mtime desc) WHERE ROWNUM=1;
  RETURN V_LOTNO;
END GETLOTNOBYRCARD;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION GetShiftDate(p_ShiftTypeCode IN VARCHAR2, p_Date IN NUMBER, p_Time IN NUMBER)
RETURN NUMBER
IS
  v_IsOverDate      NUMBER;
  v_IsBackwardOver  NUMBER;
  v_Date            DATE;
BEGIN
  v_Date := TO_DATE(TO_CHAR(p_Date), 'YYYYMMDD');
  
  --???????TimePeriod???TimePeriod?????
  SELECT COUNT(*) INTO v_IsOverDate 
  FROM tbltp
  WHERE shifttypecode = p_shiftTypeCode
  AND ((tpbtime <= tpetime AND p_Time BETWEEN tpbtime AND tpetime)
    OR (tpbtime > tpetime AND p_Time BETWEEN tpbtime AND 235959)
    OR (tpbtime > tpetime AND p_Time BETWEEN 0 AND tpetime))
  AND isoverdate = '1';
  
  IF (v_IsOverDate > 0) THEN
    --?????????????????????????
    SELECT COUNT(*) INTO v_IsBackwardOver
    FROM tblsysparam
    WHERE paramgroupcode = 'SHIFTSETTING'
    AND paramcode = 'OVERDATETYPE'
    AND UPPER(paramalias) = 'B';
    
    IF (v_IsBackwardOver > 0) THEN
      v_Date := v_Date + 1;
    ELSE
      v_Date := v_Date - 1;
    END IF;
  END IF;  
  
  RETURN TO_NUMBER(TO_CHAR(v_Date, 'YYYYMMDD'));
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(-20000, '[GetShiftDate]' || SQLERRM);
END GetShiftDate;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION SS_CURSOR(T_SHIFTCODE IN VARCHAR2,
                                        T_SHIFTTYPE IN VARCHAR2,
                                        T_SHIFTSEQ  IN INT)
RETURN VARCHAR2 AS
   V_RETURNSHIFTCODE  VARCHAR2(40);
   i_shiftSeq INT;
BEGIN
  SELECT SHIFTCODE  
    INTO V_RETURNSHIFTCODE    
        FROM TBLSHIFT   WHERE SHIFTTYPECODE = T_SHIFTTYPE   AND SHIFTSEQ = T_SHIFTSEQ - 1;
     
IF V_RETURNSHIFTCODE=NULL THEN
   SELECT MAX(shiftseq)  
     INTO i_shiftSeq
     FROM TBLSHIFT  WHERE SHIFTTYPECODE = T_SHIFTTYPE;
   IF i_shiftSeq=T_SHIFTSEQ THEN
     V_RETURNSHIFTCODE:=T_SHIFTCODE;
   ELSE 
     SELECT SHIFTCODE  
        INTO V_RETURNSHIFTCODE    
        FROM TBLSHIFT  WHERE SHIFTTYPECODE = T_SHIFTTYPE   AND SHIFTSEQ = i_shiftSeq;
   END IF;
END IF; 
RETURN  V_RETURNSHIFTCODE;     
END GETLASTSHIFT;

/

SHOW ERRORS;

CREATE OR REPLACE FUNCTION GETRCARDSTATUSONTIME (p_rcard IN VARCHAR2)
   RETURN VARCHAR2
AS
   all_status       VARCHAR2 (40);
   m_count          INT;
   is_com           VARCHAR2 (40);
   product_status   VARCHAR2 (40);
   ts_status        VARCHAR2 (40);
   m_mocode         VARCHAR2 (40);
   temp_card        VARCHAR2 (40);
BEGIN
   m_count := 0;
   all_status := '';
   temp_card := '';

   BEGIN
      SELECT rcard
        INTO temp_card
        FROM (SELECT   rcard
                  FROM tblsimulationreport
                 WHERE cartoncode = p_rcard
              ORDER BY mdate DESC, mtime DESC)
       WHERE ROWNUM = 1;
   EXCEPTION
      WHEN OTHERS
      THEN
         temp_card := '';
   END;

   SELECT COUNT (*)
     INTO m_count
     FROM (SELECT *
             FROM tblstack2rcard
            WHERE businessreason = 'type_noneproduce'
              AND serialno IN (p_rcard, temp_card));

   IF m_count > 0
   THEN
      all_status := 'SAPÌÍ1?';
   ELSE
      SELECT iscom, status, mocode
        INTO is_com, product_status, m_mocode
        FROM (SELECT *
                FROM (SELECT   *
                          FROM tblsimulationreport
                         WHERE rcard IN (p_rcard, temp_card)
                      ORDER BY mdate DESC, mtime DESC)
               WHERE ROWNUM = 1);

      IF is_com = '1' AND product_status = 'GOOD'
      THEN
         m_count := '0';

         SELECT COUNT (*)
           INTO m_count
           FROM (SELECT a.*
                   FROM tblmo2sapdetail a, tblmo2sap b
                  WHERE a.mocode = b.mocode
                    AND a.postseq = b.postseq
                    AND b.flag = 'SAP'
                    AND a.mocode = m_mocode
                    AND a.rcard IN (p_rcard, temp_card));

         IF m_count > 0
         THEN
            all_status := 'SAPÌÍ1?';
         ELSE
            all_status := 'ÌÍ1?';
         END IF;
      END IF;
   END IF;

   RETURN all_status;
END getrcardstatusontime;

/

SHOW ERRORS;

CREATE OR REPLACE FORCE VIEW TBLUSERGROUP2MODULE
(MDLCODE, USERGROUPCODE, VIEWVALUE, MUSER, MDATE, 
 MTIME, EATTRIBUTE1)
AS 
SELECT b.mdlcode, a.usergroupcode, b.viewvalue, a.muser, a.mdate, a.mtime,  ' ' AS eattribute1
FROM tblusergroup2functiongroup a, tblfunctiongroup2function b
WHERE a.functiongroupcode = b.functiongroupcode;

CREATE OR REPLACE FORCE VIEW SMTREELALL
(REELLOT, PARTNO, SO, QTY, FQTY, 
 STATUS, LMUSER, LMDATE, LMTIME, REELTYPE, 
 PARSEWAY)
AS 
SELECT "REELLOT","PARTNO","SO","QTY","FQTY","STATUS","LMUSER","LMDATE","LMTIME","REELTYPE","PARSEWAY" FROM smtreel
UNION ALL
SELECT "REELLOT","PARTNO","SO","QTY","FQTY","STATUS","LMUSER","LMDATE","LMTIME","REELTYPE","PARSEWAY" FROM smtreelnurid;

CREATE OR REPLACE FORCE VIEW A2
(A)
AS 
SELECT "A" FROM a;

CREATE OR REPLACE FORCE VIEW VENUS_TEST_V1
(BIGSSCODE, MODELCODE, OPCODE, SEGCODE, SSCODE, 
 RESCODE, SHIFTCODE, TPCODE, MOCODE, SHIFTDAY, 
 ITEMCODE, TBLMESENTITYLIST_SERIAL, MOINPUTCOUNT, MOOUTPUTCOUNT, MOLINEOUTPUTCOUNT, 
 MOWHITECARDCOUNT, MOOUTPUTWHITECARDCOUNT, LINEINPUTCOUNT, LINEOUTPUTCOUNT, OPCOUNT, 
 OPWHITECARDCOUNT, EATTRIBUTE1)
AS 
SELECT b.bigsscode, b.modelcode, b.opcode, b.segcode, b.sscode, b.rescode,
       b.shiftcode, b.tpcode, a."MOCODE",a."SHIFTDAY",a."ITEMCODE",a."TBLMESENTITYLIST_SERIAL",a."MOINPUTCOUNT",a."MOOUTPUTCOUNT",a."MOLINEOUTPUTCOUNT",a."MOWHITECARDCOUNT",a."MOOUTPUTWHITECARDCOUNT",a."LINEINPUTCOUNT",a."LINEOUTPUTCOUNT",a."OPCOUNT",a."OPWHITECARDCOUNT",a."EATTRIBUTE1"
  FROM tblrptsoqty a, tblmesentitylist b
 WHERE a.tblmesentitylist_serial = b.serial;

CREATE OR REPLACE TRIGGER tr_smtpn_partid
BEFORE INSERT
ON smtpn
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtpn_partid.NEXTVAL INTO :new.partid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtprogd_progdid
BEFORE INSERT
ON smtprogd
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtprogd_progdid.NEXTVAL INTO :new.progdid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtprogh_proghid
BEFORE INSERT
ON smtprogh
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtprogh_proghid.NEXTVAL INTO :new.proghid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtso_proghid
BEFORE INSERT
ON smtso
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtso_soid.NEXTVAL INTO :new.soid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtsohis_sohid
BEFORE INSERT
ON smtsohis
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtsohis_sohid.NEXTVAL INTO :new.sohid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtsolder_solderid
BEFORE INSERT
ON smtsolder
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtsolder_solderid.NEXTVAL INTO :new.solderid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtsoreq_solderid
BEFORE INSERT
ON smtsoreq
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtsoreq_reqid.NEXTVAL INTO :new.reqid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtsys_seq
BEFORE INSERT
ON smtsys
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtsys_seq.NEXTVAL INTO :new.seq FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtvfylog_logid
BEFORE INSERT
ON smtvfylog
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtvfylog_logid.NEXTVAL INTO :new.logid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_tblonwip_test
 BEFORE
  INSERT
 ON tblonwip_test
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_tblonwip_test.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER "BIN$ucIZ9I8eR6ix9WDWls+/pw==$0"
 BEFORE
  INSERT
 ON tblonwip_test
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_tblonwip_test.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER "BIN$y+1TkLK0TEGLilzuhKUC1A==$0"
 BEFORE
  INSERT
 ON tblonwip_test
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_tblonwip_test.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_tblonwip
   BEFORE INSERT
   ON tblonwip
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT seq_tblonwip.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_TBLMESEntityList
   BEFORE INSERT
   ON TBLMESEntityList
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT seq_TBLMESEntityList.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_TBLRptSummaryLOG
   BEFORE INSERT
   ON TBLRptSummaryLOG
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT seq_TBLRptSummaryLOG.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER "BIN$RidH4iTARJau3e/95Uh+oA==$0"
 BEFORE
  INSERT
 ON TBLInvInTransaction
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_TBLInvInTransaction.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER "BIN$EWYWVsviQmeCvUYhccIUkw==$0"
 BEFORE
  INSERT
 ON TBLInvOutTransaction
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_TBLInvOutTransaction.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_TBLInvInTransaction
 BEFORE
  INSERT
 ON TBLInvInTransaction
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_TBLInvInTransaction.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_TBLInvOutTransaction
 BEFORE
  INSERT
 ON TBLInvOutTransaction
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_TBLInvOutTransaction.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_sccdept_id
BEFORE INSERT
ON sccdept
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_sccdept_id.NEXTVAL INTO :new.id FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_sccfgrp_id
BEFORE INSERT
ON sccfgrp
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_sccfgrp_id.NEXTVAL INTO :new.id FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_sccfpn_foid
BEFORE INSERT
ON sccfpn
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_sccfpn_foid.NEXTVAL INTO :new.foid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_scclog_id
BEFORE INSERT
ON scclog
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_scclog_id.NEXTVAL INTO :new.id FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_sccfunc_id
BEFORE INSERT
ON sccfunc
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_sccfunc_id.NEXTVAL INTO :new.id FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtline_lineid
BEFORE INSERT
ON smtline
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtline_lineid.NEXTVAL INTO :new.lineid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtmach_machid
BEFORE INSERT
ON smtmach
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtmach_machid.NEXTVAL INTO :new.machid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtpcba_pcbaid
BEFORE INSERT
ON smtpcba
REFERENCING NEW AS NEW OLD AS OLD
FOR EACH ROW
BEGIN
SELECT SEQ_smtpcba_pcbaid.NEXTVAL INTO :new.pcbaid FROM dual;
END
;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_smtcustomer_id
	BEFORE INSERT
 	ON smtcustomer
	REFERENCING NEW AS NEW OLD AS OLD
 	FOR EACH ROW
BEGIN
	SELECT seq_smtcustomer_id.NEXTVAL INTO :new.id FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER trigger_tbljoblog_serial
   BEFORE INSERT
   ON tbljoblog
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT sequence_tbljoblog_serial.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_TBLPALLET2RCARDLog
 BEFORE
  INSERT
 ON TBLPALLET2RCARDLog
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_TBLPALLET2RCARDLog.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER tr_seq_TBLCarton2RCARDLog
 BEFORE
  INSERT
 ON TBLCarton2RCARDLog
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
BEGIN
   SELECT seq_TBLCarton2RCARDLog.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLLINE2MANDETAIL_SERIAL
   BEFORE INSERT
   ON TBLLINE2MANDETAIL
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLLINE2MANDETAIL_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLPRODDETAIL_SERIAL
   BEFORE INSERT
   ON TBLPRODDETAIL
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLPRODDETAIL_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLLINEPAUSE_SERIAL
   BEFORE INSERT
   ON TBLLINEPAUSE
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLLINEPAUSE_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLEXCEPTION_SERIAL
   BEFORE INSERT
   ON TBLEXCEPTION
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLEXCEPTION_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_DCTMESSAGE_SERIALNO
   BEFORE INSERT
   ON DCTMESSAGE
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT DCTMESSAGE_SERIALNO.NEXTVAL INTO :NEW.serialno
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER epsmailformat_bri1
 BEFORE
  INSERT
 ON epsmailformat
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select EPSMAILFORMAT_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER epsmailtype_bri1
 BEFORE
  INSERT
 ON epsmailtype
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select EPSMAILTYPE_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscelement_bri1
 BEFORE
  INSERT
 ON wscelement
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCELEMENT_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscgroup_bri1
 BEFORE
  INSERT
 ON wscgroup
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCGROUP_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wsclanguage_bri1
 BEFORE
  INSERT
 ON wsclanguage
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCLANGUAGE_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscmenu_bri1
 BEFORE
  INSERT
 ON wscmenu
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCMENU_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscparametertype_bri1
 BEFORE
  INSERT
 ON wscparametertype
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCPARAMETERTYPE_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;


CREATE OR REPLACE TRIGGER tr_tblmaterialtrans
   BEFORE INSERT
   ON tblmaterialtrans
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT seq_tblmaterialtrans.NEXTVAL
     INTO :NEW.serial
     FROM DUAL;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLNOTICEERROR_SERIAL
   BEFORE INSERT
   ON TBLNOTICEERROR
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLNOTICEERROR_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLNOTICEERRORCODE_SERIAL
   BEFORE INSERT
   ON TBLNOTICEERRORCODE
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLNOTICEERRORCODE_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLNOTICEDIRECTPASS_SERIAL
   BEFORE INSERT
   ON TBLNOTICEDIRECTPASS
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLNOTICEDIRECTPASS_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLNOTICELINEPAUSE_SERIAL
   BEFORE INSERT
   ON TBLNOTICELINEPAUSE
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLNOTICELINEPAUSE_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLALERTMAILSETTING_SERIAL
   BEFORE INSERT
   ON TBLALERTMAILSETTING
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLALERTMAILSETTING_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLALERTNOTICE_SERIAL
   BEFORE INSERT
   ON TBLALERTNOTICE
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLALERTNOTICE_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER TRI_TBLMAIL_SERIAL
   BEFORE INSERT
   ON TBLMAIL
   REFERENCING NEW AS NEW OLD AS OLD
   FOR EACH ROW
BEGIN
   SELECT SEQ_TBLMAIL_SERIAL.NEXTVAL INTO :NEW.serial
   FROM dual;
END;
/
SHOW ERRORS;

CREATE OR REPLACE TYPE BODY type_joblog
IS

CONSTRUCTOR FUNCTION type_JobLog (
        JOBID           IN  VARCHAR2    DEFAULT ' ',
        SERIAL          IN  NUMBER      DEFAULT 0,
        STARTDATETIME   IN  DATE        DEFAULT SYSDATE,
        ENDDATETIME     IN  DATE        DEFAULT SYSDATE,
        USEDTIME        IN  NUMBER      DEFAULT 0,
        PROCESSCOUNT    IN  NUMBER      DEFAULT 0,
        RESULT          IN  VARCHAR2    DEFAULT ' ',
        ERRORMESSAGE    IN  VARCHAR2    DEFAULT ' '
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.JOBID  := JOBID;
        SELF.SERIAL := SERIAL;
        SELF.STARTDATETIME := STARTDATETIME;
        SELF.ENDDATETIME := ENDDATETIME;
        SELF.USEDTIME := USEDTIME;
        SELF.PROCESSCOUNT := PROCESSCOUNT;
        SELF.RESULT := RESULT;
        SELF.ERRORMESSAGE := ERRORMESSAGE;
        RETURN;
    END type_JobLog;
    MEMBER PROCEDURE WRITE
    IS
        --Log auto commit
        PRAGMA AUTONOMOUS_TRANSACTION;
    BEGIN
        INSERT INTO TBLJobLog(JOBID,SERIAL,STARTDATETIME,ENDDATETIME,USEDTIME,PROCESSCOUNT,RESULT,ERRORMSG)
        VALUES (JOBID,SERIAL,STARTDATETIME,ENDDATETIME,USEDTIME,PROCESSCOUNT,RESULT,ERRORMESSAGE);
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE;
    END WRITE;

    MEMBER PROCEDURE ClearHistoryLog
    IS
        PRAGMA AUTONOMOUS_TRANSACTION;
    BEGIN
        DELETE FROM TBLJobLog
         WHERE STARTDATETIME < (SYSDATE - 90);
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE;
    END ClearHistoryLog;


END;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY TYPE_SUMMARYLOG
AS
    CONSTRUCTOR FUNCTION type_SummaryLog (
        SERIAL          IN  NUMBER      DEFAULT 0,
        STARTDATETIME   IN  DATE        DEFAULT SYSDATE,
        ENDDATETIME     IN  DATE        DEFAULT SYSDATE,
        USEDTIME        IN  NUMBER      DEFAULT 0,
        PROCESSCOUNT    IN  NUMBER      DEFAULT 0,
        RESULT          IN  VARCHAR2    DEFAULT ' ',
        ERRORMESSAGE    IN  VARCHAR2    DEFAULT ' '
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL := SERIAL;
        SELF.STARTDATETIME := STARTDATETIME;
        SELF.ENDDATETIME := ENDDATETIME;
        SELF.USEDTIME := USEDTIME;
        SELF.PROCESSCOUNT := PROCESSCOUNT;
        SELF.RESULT := RESULT;
        SELF.ERRORMESSAGE := ERRORMESSAGE;
        RETURN;
    END type_SummaryLog;    
    MEMBER PROCEDURE WRITE
    IS
        --Log auto commit
        PRAGMA AUTONOMOUS_TRANSACTION;
    BEGIN
        INSERT INTO TBLRptSummaryLOG(SERIAL,STARTDATETIME,ENDDATETIME,USEDTIME,PROCESSCOUNT,RESULT,ERRORMSG)
        VALUES (SERIAL,STARTDATETIME,ENDDATETIME,USEDTIME,PROCESSCOUNT,RESULT,ERRORMESSAGE);
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE;
    END WRITE;
    
    MEMBER PROCEDURE ClearHistoryLog
    IS
        PRAGMA AUTONOMOUS_TRANSACTION;
    BEGIN
        DELETE FROM TBLRptSummaryLOG
         WHERE STARTDATETIME < (SYSDATE - 90);
        COMMIT;
    EXCEPTION
        WHEN OTHERS THEN
            ROLLBACK;
            RAISE;
    END ClearHistoryLog;
END;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY TYPE_MESENTITYLIST
AS
    CONSTRUCTOR FUNCTION type_MESEntityList (
        SERIAL          IN  NUMBER      DEFAULT 0,
        BigSSCode       IN  VARCHAR2    DEFAULT ' ',
        ModelCode       IN  VARCHAR2    DEFAULT ' ',
        OPCode          IN  VARCHAR2    DEFAULT ' ',
        SegmentCode     IN  VARCHAR2    DEFAULT ' ',
        SSCode          IN  VARCHAR2    DEFAULT ' ',
        ResourceCode    IN  VARCHAR2    DEFAULT ' ',
        ShiftTypeCode   IN  VARCHAR2    DEFAULT ' ',
        ShiftCode       IN  VARCHAR2    DEFAULT ' ',
        TimePeriodCode  IN  VARCHAR2    DEFAULT ' ',
        FactoryCode     IN  VARCHAR2    DEFAULT ' ',
        OrgID           IN  NUMBER      DEFAULT 0,
        EAttribute1     IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL := SERIAL;
        SELF.BigSSCode := BigSSCode;
        SELF.ModelCode := ModelCode;
        SELF.OPCode := OPCode;
        SELF.SegmentCode := SegmentCode;
        SELF.SSCode := SSCode;
        SELF.ResourceCode := ResourceCode;
        SELF.ShiftTypeCode := ShiftTypeCode;
        SELF.ShiftCode := ShiftCode;
        SELF.TimePeriodCode := TimePeriodCode;
        SELF.FactoryCode := FactoryCode;
        SELF.OrgID := OrgID;
        SELF.EAttribute1 := EAttribute1;
        RETURN;
    END type_MESEntityList;    
    
    MEMBER PROCEDURE SAVE
    IS
    BEGIN
        INSERT INTO TBLMESEntityList(SERIAL,BigSSCode,ModelCode,OPCode,SegCode,SSCode,ResCode,ShiftTypeCode,ShiftCode,TPCode,FacCode,OrgID,EAttribute1)
        VALUES (SERIAL,BigSSCode,ModelCode,OPCode,SegmentCode,SSCode,ResourceCode,ShiftTypeCode,ShiftCode,TimePeriodCode,FactoryCode,OrgID,EAttribute1);
    END SAVE;
    
    MEMBER FUNCTION GetMESEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT a.SERIAL
          INTO v_Serial
          FROM TBLMESEntityList a
         WHERE a.bigsscode = BigSSCode
           AND a.modelcode = ModelCode
           AND a.opcode = OPCode
           AND a.segcode = SegmentCode
           AND a.sscode = SSCode
           AND a.rescode = ResourceCode
           AND a.shifttypecode = ShiftTypeCode
           AND a.shiftcode = ShiftCode
           AND a.tpcode = TimePeriodCode
           AND a.faccode = FactoryCode
           AND a.orgid = OrgID;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetMESEntitySerial;
END;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY TYPE_RPTOPQTY
AS
    CONSTRUCTOR FUNCTION type_RPTOPQTY (
        MOCode              IN  VARCHAR2    DEFAULT ' ',
        ShiftDay            IN  NUMBER      DEFAULT 0,
        ItemCode            IN  VARCHAR2    DEFAULT ' ',
        MESEntity_Serial    IN  NUMBER      DEFAULT 0,
        InputTimes          IN  NUMBER      DEFAULT 0,
        OutputTimes         IN  NUMBER      DEFAULT 0,
        NGTimes             IN  NUMBER      DEFAULT 0,
        EAttribute1         IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.MOCode := MOCode;
        SELF.ShiftDay := ShiftDay;
        SELF.ItemCode := ItemCode;
        SELF.MESEntity_Serial := MESEntity_Serial;
        SELF.InputTimes := InputTimes;
        SELF.OutputTimes := OutputTimes;
        SELF.NGTimes := NGTimes;
        SELF.EAttribute1 := EAttribute1;
        RETURN;
    END type_RPTOPQTY;    
    
    MEMBER PROCEDURE ADDNew
    IS
    BEGIN
        INSERT INTO TBLRPTOPQTY(MOCODE,SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL,INPUTTIMES,OUTPUTTIMES,NGTIMES,EAttribute1)
        VALUES (MOCode,ShiftDay,ItemCode,MESEntity_Serial,InputTimes,OutputTimes,NGTimes,EAttribute1);
    END ADDNew;
END;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY TYPE_RPTLINEQTY
AS
    CONSTRUCTOR FUNCTION type_RPTLINEQTY (
        MOCode              IN  VARCHAR2    DEFAULT ' ',
        ShiftDay            IN  NUMBER      DEFAULT 0,
        ItemCode            IN  VARCHAR2    DEFAULT ' ',
        MESEntity_Serial    IN  NUMBER      DEFAULT 0,
        LineWhiteCardCount  IN  NUMBER      DEFAULT 0,
        ResWhiteCardCount   IN  NUMBER      DEFAULT 0,
        EAttribute1         IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.MOCode := MOCode;
        SELF.ShiftDay := ShiftDay;
        SELF.ItemCode := ItemCode;
        SELF.MESEntity_Serial := MESEntity_Serial;
        SELF.LineWhiteCardCount := LineWhiteCardCount;
        SELF.ResWhiteCardCount := ResWhiteCardCount;
        SELF.EAttribute1 := EAttribute1;
        RETURN;
    END type_RPTLINEQTY;
    
    MEMBER PROCEDURE ADDNew
    IS
    BEGIN
        INSERT INTO TBLRPTLINEQTY(MOCODE,SHIFTDAY,ITEMCODE,TBLMESENTITYLIST_SERIAL,LineWhiteCardCount,ResWhiteCardCount,EAttribute1)
        VALUES (MOCode,ShiftDay,ItemCode,MESEntity_Serial,LineWhiteCardCount,ResWhiteCardCount,EAttribute1);
    END ADDNew;
END;

/

SHOW ERRORS;

CREATE OR REPLACE TYPE BODY TYPE_RPTSOQTY
AS
    CONSTRUCTOR FUNCTION type_RPTSOQTY (
        MOCode                  IN  VARCHAR2    DEFAULT ' ',
        ShiftDay                IN  NUMBER      DEFAULT 0,
        ItemCode                IN  VARCHAR2    DEFAULT ' ',
        MESEntity_Serial        IN  NUMBER      DEFAULT 0,
        MOInputCount            IN  NUMBER      DEFAULT 0,
        MOOutputCount           IN  NUMBER      DEFAULT 0,
        MOLineOutputCount       IN  NUMBER      DEFAULT 0,
        MOWhiteCardCount        IN  NUMBER      DEFAULT 0,
        MOOutputWhiteCardCount  IN  NUMBER      DEFAULT 0,
        LineInputCount          IN  NUMBER      DEFAULT 0,
        LineOutputCount         IN  NUMBER      DEFAULT 0,
        OPCount                 IN  NUMBER      DEFAULT 0,
        OPWhiteCardCount        IN  NUMBER      DEFAULT 0,
        EAttribute1             IN  VARCHAR2    DEFAULT ''
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.MOCode := MOCode;
        SELF.ShiftDay := ShiftDay;
        SELF.ItemCode := ItemCode;
        SELF.MESEntity_Serial := MESEntity_Serial;        
        SELF.MOInputCount := MOInputCount;
        SELF.MOOutputCount := MOOutputCount;
        SELF.MOLineOutputCount := MOLineOutputCount;
        SELF.MOWhiteCardCount := MOWhiteCardCount;
        SELF.MOOutputWhiteCardCount := MOOutputWhiteCardCount;
        SELF.LineInputCount := LineInputCount;
        SELF.LineOutputCount := LineOutputCount;
        SELF.OPCount := OPCount;
        SELF.OPWhiteCardCount := OPWhiteCardCount;        
        SELF.EAttribute1 := EAttribute1;
        RETURN;
    END type_RPTSOQTY;
    
    MEMBER PROCEDURE ADDNew
    IS
    BEGIN
        INSERT INTO tblrptsoqty
                    (mocode, shiftday, itemcode, tblmesentitylist_serial,
                     moinputcount, mooutputcount, molineoutputcount,
                     mowhitecardcount, mooutputwhitecardcount, lineinputcount,
                     lineoutputcount, opcount, opwhitecardcount, eattribute1
                    )
             VALUES (MOCode, ShiftDay, ItemCode, MESEntity_Serial,
                     MOInputCount, MOOutputCount, MOLineOutputCount,
                     MOWhiteCardCount, MOOutputWhiteCardCount, LineInputCount,
                     LineOutputCount, OPCount, OPWhiteCardCount, eattribute1
                    );
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END ADDNew;
END;

/

SHOW ERRORS;

CREATE OR REPLACE type body TYPE_NOTICE_ERROR is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_ERROR (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        ECODE           IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.ITEMCODE := ITEMCODE;
        SELF.ECODE := ECODE;
        SELF.SHIFTCODE := SHIFTCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.BIGSSCODE := BIGSSCODE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_ERROR;
    ---------------------------------
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO tblnoticeerror
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECODE, SHIFTCODE,
           SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECODE, SHIFTCODE,
           SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		-----------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM tblnoticeerror a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.ITEMCODE = ITEMCODE
           AND a.ECODE = ECODE
           AND a.SHIFTCODE = SHIFTCODE
           AND a.SHIFTDAY = SHIFTDAY
           AND a.BIGSSCODE = BIGSSCODE
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;

/

SHOW ERRORS;

CREATE OR REPLACE type body TYPE_NOTICE_ERROR_CODE is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_ERROR_CODE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        ECSCODE         IN  VARCHAR2 DEFAULT ' ',
        LOCATION        IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.ITEMCODE := ITEMCODE;
        SELF.ECSCODE := ECSCODE;
        SELF.LOCATION := LOCATION;
        SELF.SHIFTCODE := SHIFTCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.BIGSSCODE := BIGSSCODE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_ERROR_CODE;
    -----------------------------
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLNOTICEERRORCODE
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECSCODE, LOCATION,
           SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, ECSCODE, LOCATION,
           SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		-------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM TBLNOTICEERRORCODE a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.ITEMCODE = ITEMCODE
           AND a.ECSCODE = ECSCODE
           AND a.LOCATION = LOCATION
           AND a.SHIFTCODE = SHIFTCODE
           AND a.SHIFTDAY = SHIFTDAY
					 AND a.BIGSSCODE = BIGSSCODE
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;

/

SHOW ERRORS;

CREATE OR REPLACE type body TYPE_NOTICE_DIRECTPASS is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_DIRECTPASS (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        ITEMCODE        IN  VARCHAR2 DEFAULT ' ',
        SHIFTCODE       IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        BIGSSCODE       IN  VARCHAR2 DEFAULT ' ',
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.ITEMCODE := ITEMCODE;
        SELF.SHIFTCODE := SHIFTCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.BIGSSCODE := BIGSSCODE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_DIRECTPASS;
    
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLNOTICEDIRECTPASS
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, ITEMCODE, SHIFTCODE, SHIFTDAY, BIGSSCODE, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		----------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM TBLNOTICEDIRECTPASS a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.ITEMCODE = ITEMCODE
           AND a.SHIFTCODE = SHIFTCODE
           AND a.SHIFTDAY = SHIFTDAY
					 AND a.BIGSSCODE = BIGSSCODE
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;

/

SHOW ERRORS;

CREATE OR REPLACE type body TYPE_NOTICE_LINEPAUSE is
  
  CONSTRUCTOR FUNCTION TYPE_NOTICE_LINEPAUSE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        SSCODE          IN  VARCHAR2 DEFAULT ' ',
        OPCODE          IN  VARCHAR2 DEFAULT ' ',
        SHIFTDAY        IN  NUMBER   DEFAULT 0,
        ONWIPSERIAL     IN  NUMBER   DEFAULT 0,
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.SSCODE := SSCODE;
        SELF.OPCODE := OPCODE;
        SELF.SHIFTDAY := SHIFTDAY;
        SELF.ONWIPSERIAL := ONWIPSERIAL;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_NOTICE_LINEPAUSE;
    
		------------------------------
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLNOTICELINEPAUSE
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, SSCODE, OPCODE, SHIFTDAY, ONWIPSERIAL, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, SUBITEMSEQUENCE, SSCODE, OPCODE, SHIFTDAY, ONWIPSERIAL, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
		
		------------------------------
		MEMBER FUNCTION GetEntitySerial
        RETURN NUMBER
    IS
        v_Serial    NUMBER;
    BEGIN
        v_Serial := 0;
        SELECT max(a.SERIAL)
          INTO v_Serial
          FROM TBLNOTICELINEPAUSE a
         WHERE a.ITEMSEQUENCE = ITEMSEQUENCE
           AND a.SUBITEMSEQUENCE = SUBITEMSEQUENCE
           AND a.SSCODE = SSCODE
           AND a.OPCODE = OPCODE
           AND a.SHIFTDAY = SHIFTDAY
           AND a.ONWIPSERIAL = ONWIPSERIAL
           AND a.MUSER = MUSER
           AND a.MDATE = MDATE
           AND a.MTIME = MTIME;

        RETURN v_Serial;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            v_Serial := 0;
            RETURN v_Serial;
        WHEN OTHERS THEN
            RAISE;
    END GetEntitySerial;
  
end;

/

SHOW ERRORS;

CREATE OR REPLACE type body TYPE_ALERT_NOTICE is
  
  CONSTRUCTOR FUNCTION TYPE_ALERT_NOTICE (
        SERIAL          IN  NUMBER   DEFAULT 0,
        ITEMSEQUENCE    IN  VARCHAR2 DEFAULT ' ',
        DESCRIPTION     IN  VARCHAR2 DEFAULT ' ',
        SUBITEMSEQUENCE IN  VARCHAR2 DEFAULT ' ',
        NOTICESERIAL    IN  NUMBER   DEFAULT 0,
        ALERTTYPE       IN  VARCHAR2 DEFAULT ' ',
        STATUS          IN  VARCHAR2 DEFAULT ' ',
        MOLIST          IN  VARCHAR2 DEFAULT ' ',
        NOTICECONTENT   IN  VARCHAR2 DEFAULT ' ',
        ANALYSISREASON  IN  VARCHAR2 DEFAULT ' ',
        DEALMETHODS     IN  VARCHAR2 DEFAULT ' ',
        NOTICEDATE      IN  NUMBER   DEFAULT 0,
        NOTICETIME      IN  NUMBER   DEFAULT 0,
        DEALUSER        IN  VARCHAR2 DEFAULT ' ',
        DEALDATE        IN  NUMBER   DEFAULT 0,
        DEALTIME        IN  NUMBER   DEFAULT 0,
        MUSER           IN  VARCHAR2 DEFAULT ' ',
        MDATE           IN  NUMBER   DEFAULT 0,
        MTIME           IN  NUMBER   DEFAULT 0
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.ITEMSEQUENCE := ITEMSEQUENCE;
        SELF.DESCRIPTION := DESCRIPTION;
        SELF.SUBITEMSEQUENCE := SUBITEMSEQUENCE;
        SELF.NOTICESERIAL := NOTICESERIAL;
        SELF.ALERTTYPE := ALERTTYPE;
        SELF.STATUS := STATUS;
        SELF.MOLIST := MOLIST;
        SELF.NOTICECONTENT := NOTICECONTENT;
        SELF.ANALYSISREASON := ANALYSISREASON;
        SELF.DEALMETHODS := DEALMETHODS;
        SELF.NOTICEDATE := NOTICEDATE;
        SELF.NOTICETIME := NOTICETIME;
        SELF.DEALUSER := DEALUSER;
        SELF.DEALDATE := DEALDATE;
        SELF.DEALTIME := DEALTIME;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        RETURN;
    END TYPE_ALERT_NOTICE;
    
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLALERTNOTICE
          (SERIAL, ITEMSEQUENCE, DESCRIPTION, SUBITEMSEQUENCE, NOTICESERIAL, ALERTTYPE, 
          STATUS, MOLIST, NOTICECONTENT, ANALYSISREASON, DEALMETHODS, NOTICEDATE, 
          NOTICETIME, DEALUSER, DEALDATE, DEALTIME, MUSER, MDATE, MTIME)
        VALUES
          (SERIAL, ITEMSEQUENCE, DESCRIPTION, SUBITEMSEQUENCE, NOTICESERIAL, ALERTTYPE, 
          STATUS, MOLIST, NOTICECONTENT, ANALYSISREASON, DEALMETHODS, NOTICEDATE, 
          NOTICETIME, DEALUSER, DEALDATE, DEALTIME, MUSER, MDATE, MTIME);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
  
end;

/

SHOW ERRORS;

CREATE OR REPLACE type body TYPE_MAIL is
  
  CONSTRUCTOR FUNCTION TYPE_MAIL (
        SERIAL       IN  NUMBER   DEFAULT 0,
        MAILSUBJECT  IN  VARCHAR2 DEFAULT ' ',
        RECIPIENTS   IN  VARCHAR2 DEFAULT ' ',
        MAILCONTENT  IN  VARCHAR2 DEFAULT ' ',
        ISSEND       IN  VARCHAR2 DEFAULT ' ',
        SENDTIMES    IN  NUMBER   DEFAULT 0,
        SENDRESULT   IN  VARCHAR2 DEFAULT ' ',
        ERRORMESSAGE IN  VARCHAR2 DEFAULT ' ',
        MUSER        IN  VARCHAR2 DEFAULT ' ',
        MDATE        IN  NUMBER   DEFAULT 0,
        MTIME        IN  NUMBER   DEFAULT 0,
        EATTRIBUTE1  IN  VARCHAR2 DEFAULT ' ',
        EATTRIBUTE2  IN  VARCHAR2 DEFAULT ' ',
        EATTRIBUTE3  IN  VARCHAR2 DEFAULT ' '
    )
        RETURN SELF AS RESULT
    IS
    BEGIN
        SELF.SERIAL  := SERIAL;
        SELF.MAILSUBJECT := MAILSUBJECT;
        SELF.RECIPIENTS := RECIPIENTS;
        SELF.MAILCONTENT := MAILCONTENT;
        SELF.ISSEND := ISSEND;
        SELF.SENDTIMES := SENDTIMES;
        SELF.SENDRESULT := SENDRESULT;
        SELF.ERRORMESSAGE := ERRORMESSAGE;
        SELF.MUSER := MUSER;
        SELF.MDATE := MDATE;
        SELF.MTIME := MTIME;
        SELF.EATTRIBUTE1 := EATTRIBUTE1;
        SELF.EATTRIBUTE2 := EATTRIBUTE2;
        SELF.EATTRIBUTE3 := EATTRIBUTE3;
        RETURN;
    END TYPE_MAIL;
    
    MEMBER PROCEDURE WRITE
    IS
    BEGIN
        INSERT INTO TBLMAIL
          (SERIAL, MAILSUBJECT, RECIPIENTS, MAILCONTENT, ISSEND, SENDTIMES, 
          SENDRESULT, ERRORMESSAGE, MUSER, MDATE, MTIME, EATTRIBUTE1, EATTRIBUTE2, EATTRIBUTE3)
        VALUES
          (SERIAL, MAILSUBJECT, RECIPIENTS, MAILCONTENT, ISSEND, SENDTIMES, 
          SENDRESULT, ERRORMESSAGE, MUSER, MDATE, MTIME, EATTRIBUTE1, EATTRIBUTE2, EATTRIBUTE3);
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
    END WRITE;
  
end;

/

SHOW ERRORS;

CREATE TABLE TBLSPCDATASTORE
(
  ID           VARCHAR2(40 BYTE)                NOT NULL,
  ITEMCODE     VARCHAR2(40 BYTE),
  OBJECTCODE   VARCHAR2(40 BYTE),
  DATEFROM     NUMBER(8)                        NOT NULL,
  DATETO       NUMBER(8)                        NOT NULL,
  TABLENAME    VARCHAR2(40 BYTE)                NOT NULL,
  MUSER        VARCHAR2(40 BYTE)                NOT NULL,
  MDATE        NUMBER(8)                        NOT NULL,
  MTIME        NUMBER(6)                        NOT NULL,
  EATTRIBUTE1  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSPCDATESTORE ON TBLSPCDATASTORE
(ID)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_SPCOBJECTSTORE ON TBLSPCDATASTORE
(ITEMCODE, OBJECTCODE, DATEFROM, DATETO)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSPCDATASTORE
 ADD CONSTRAINT PK_TBLSPCDATESTORE
 PRIMARY KEY
 (ID);

CREATE TABLE TBLSPCITEMSPEC
(
  ITEMCODE       VARCHAR2(40 BYTE)              NOT NULL,
  OBJECTCODE     VARCHAR2(40 BYTE)              NOT NULL,
  GROUPSEQ       NUMBER(10)                     NOT NULL,
  STORECOLUMN    NUMBER(10)                     NOT NULL,
  CONDITIONNAME  VARCHAR2(40 BYTE)              NOT NULL,
  UCL            NUMBER(15,5)                   NOT NULL,
  LCL            NUMBER(15,5)                   NOT NULL,
  USL            NUMBER(15,5)                   NOT NULL,
  LSL            NUMBER(15,5)                   NOT NULL,
  LIMITUPONLY    VARCHAR2(1 BYTE)               NOT NULL,
  LIMITLOWONLY   VARCHAR2(1 BYTE)               NOT NULL,
  AUTOCL         VARCHAR2(1 BYTE)               NOT NULL,
  MEMO           VARCHAR2(100 BYTE),
  MUSER          VARCHAR2(40 BYTE)              NOT NULL,
  MDATE          NUMBER(8)                      NOT NULL,
  MTIME          NUMBER(6)                      NOT NULL,
  EATTRIBUTE1    VARCHAR2(40 BYTE),
  TESTDATACOUNT  VARCHAR2(40 BYTE)
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_TBLSPCITEMSPEC ON TBLSPCITEMSPEC
(ITEMCODE, OBJECTCODE, GROUPSEQ)
LOGGING
NOPARALLEL;

CREATE INDEX INDEX_SPCITEMSPEC ON TBLSPCITEMSPEC
(ITEMCODE, OBJECTCODE, CONDITIONNAME)
LOGGING
NOPARALLEL;

ALTER TABLE TBLSPCITEMSPEC
 ADD CONSTRAINT PK_TBLSPCITEMSPEC
 PRIMARY KEY
 (ITEMCODE, OBJECTCODE, GROUPSEQ);

CREATE TABLE WSCROLEELEMENT
(
  ID          NUMBER                            NOT NULL,
  ROLEID      NUMBER                            DEFAULT 0                     NOT NULL,
  ELEMENTID   NUMBER                            DEFAULT 0                     NOT NULL,
  ROLETYPE    VARCHAR2(150 BYTE)                DEFAULT ' '                   NOT NULL,
  CREATETIME  DATE                              DEFAULT SYSDATE               NOT NULL,
  CREATEUSER  VARCHAR2(150 BYTE)                DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCROLEELEMENT ON WSCROLEELEMENT
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCROLEELEMENT
 ADD CONSTRAINT PK_WSCROLEELEMENT
 PRIMARY KEY
 (ID);

CREATE TABLE WSCMENULANGUAGE
(
  ID           NUMBER                           NOT NULL,
  MENUID       NUMBER                           DEFAULT 0                     NOT NULL,
  LANGUAGEID   NUMBER                           DEFAULT 0                     NOT NULL,
  MENUNAME     VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  DESCRIPTION  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT SYSDATE               NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX UK_WSCMENULANGUAGE ON WSCMENULANGUAGE
(MENUID, LANGUAGEID)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK_WSCMENULANGUAGE ON WSCMENULANGUAGE
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCMENULANGUAGE
 ADD CONSTRAINT PK_WSCMENULANGUAGE
 PRIMARY KEY
 (ID);

CREATE TABLE WSCUSER
(
  USERID         NUMBER                         NOT NULL,
  ACCOUNT        VARCHAR2(150 BYTE)             DEFAULT ' '                   NOT NULL,
  DOMAIN         VARCHAR2(150 BYTE)             DEFAULT 'BenQ'                NOT NULL,
  EMPNO          VARCHAR2(150 BYTE)             DEFAULT ' '                   NOT NULL,
  EMAIL          VARCHAR2(300 BYTE)             DEFAULT ' '                   NOT NULL,
  PHONE          VARCHAR2(150 BYTE)             DEFAULT ' '                   NOT NULL,
  DESCRIPTION    VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  PASSWORD       VARCHAR2(150 BYTE)             DEFAULT ' '                   NOT NULL,
  LANGUAGEID     NUMBER                         DEFAULT 0                     NOT NULL,
  DEFAULTURL     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ACTIVE         NUMBER(1)                      DEFAULT 1                     NOT NULL,
  LOGONTIMES     NUMBER                         DEFAULT 0                     NOT NULL,
  LASTLOGONTIME  DATE                           DEFAULT sysdate               NOT NULL,
  CREATETIME     DATE                           DEFAULT SYSDATE               NOT NULL,
  CREATEUSER     VARCHAR2(150 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE1     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9     VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10    VARCHAR2(750 BYTE)             DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20    VARCHAR2(4000 BYTE)            DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX UK_WSCUSER ON WSCUSER
(ACCOUNT)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK_WSCUSER ON WSCUSER
(USERID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCUSER
 ADD CONSTRAINT PK_WSCUSER
 PRIMARY KEY
 (USERID);

CREATE TABLE WSCROLEMENU
(
  ID          NUMBER                            NOT NULL,
  ROLEID      NUMBER                            DEFAULT 0                     NOT NULL,
  MENUID      NUMBER                            DEFAULT 0                     NOT NULL,
  ROLETYPE    VARCHAR2(150 BYTE)                DEFAULT ' '                   NOT NULL,
  CREATETIME  DATE                              DEFAULT SYSDATE               NOT NULL,
  CREATEUSER  VARCHAR2(150 BYTE)                DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCROLEMENU ON WSCROLEMENU
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCROLEMENU
 ADD CONSTRAINT PK_WSCROLEMENU
 PRIMARY KEY
 (ID);

CREATE TABLE WSCRELATEDMENU
(
  ID           NUMBER                           NOT NULL,
  RID          NUMBER                           DEFAULT 0                     NOT NULL,
  MNAME        VARCHAR2(300 BYTE)               DEFAULT ' '                   NOT NULL,
  DESCRIPTION  VARCHAR2(300 BYTE)               DEFAULT ' '                   NOT NULL,
  URL          VARCHAR2(300 BYTE)               DEFAULT ' '                   NOT NULL,
  CREATETIME   DATE                             DEFAULT SYSDATE               NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX PK_WSCRELATEDMENU ON WSCRELATEDMENU
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCRELATEDMENU
 ADD CONSTRAINT PK_WSCRELATEDMENU
 PRIMARY KEY
 (ID);

CREATE TABLE WSCPARAMETER
(
  ID           NUMBER                           NOT NULL,
  PNAME        VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  PVALUE       VARCHAR2(450 BYTE)               DEFAULT ' '                   NOT NULL,
  PTID         NUMBER                           DEFAULT 0                     NOT NULL,
  CREATETIME   DATE                             DEFAULT sysdate               NOT NULL,
  CREATEUSER   VARCHAR2(150 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE1   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE2   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE3   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE4   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE5   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE6   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE7   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE8   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE9   VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE10  VARCHAR2(750 BYTE)               DEFAULT ' '                   NOT NULL,
  ATTRIBUTE11  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE13  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE12  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE14  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE15  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE16  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE17  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE18  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE19  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL,
  ATTRIBUTE20  VARCHAR2(4000 BYTE)              DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX UK_WSCPARAMETER ON WSCPARAMETER
(PNAME, PTID)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK_WSCPARAMETER ON WSCPARAMETER
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCPARAMETER
 ADD CONSTRAINT PK_WSCPARAMETER
 PRIMARY KEY
 (ID);

CREATE OR REPLACE TRIGGER wscroleelement_bri1
 BEFORE
  INSERT
 ON wscroleelement
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCROLEELEMENT_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscmenulanguage_bri1
 BEFORE
  INSERT
 ON wscmenulanguage
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCMENULANGUAGE_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscuser_bri1
 BEFORE
  INSERT
 ON wscuser
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCUSER_USERID_S.nextval into :new.USERID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscrolemenu_bri1
 BEFORE
  INSERT
 ON wscrolemenu
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCROLEMENU_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscrelatedmenu_bri1
 BEFORE
  INSERT
 ON wscrelatedmenu
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCRELATEDMENU_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE OR REPLACE TRIGGER wscparameter_bri1
 BEFORE
  INSERT
 ON wscparameter
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCPARAMETER_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

CREATE TABLE WSCUSERGROUP
(
  ID          NUMBER                            NOT NULL,
  USERID      NUMBER                            DEFAULT 0                     NOT NULL,
  GROUPID     NUMBER                            DEFAULT 0                     NOT NULL,
  CREATETIME  DATE                              DEFAULT SYSDATE               NOT NULL,
  CREATEUSER  VARCHAR2(150 BYTE)                DEFAULT ' '                   NOT NULL
)
LOGGING 
NOCOMPRESS 
NOCACHE
NOPARALLEL
MONITORING;

CREATE UNIQUE INDEX UK_WSCUSERGROUP ON WSCUSERGROUP
(USERID, GROUPID)
LOGGING
NOPARALLEL;

CREATE UNIQUE INDEX PK_WSCUSERGROUP ON WSCUSERGROUP
(ID)
LOGGING
NOPARALLEL;

ALTER TABLE WSCUSERGROUP
 ADD CONSTRAINT PK_WSCUSERGROUP
 PRIMARY KEY
 (ID);

CREATE OR REPLACE TRIGGER wscusergroup_bri1
 BEFORE
  INSERT
 ON wscusergroup
REFERENCING NEW AS NEW OLD AS OLD
 FOR EACH ROW
begin
select WSCUSERGROUP_ID_S.nextval into :new.ID from dual
; END;
/
SHOW ERRORS;

ALTER TABLE TBLALERTHANDLELOG
 ADD CONSTRAINT REFTBLALERT639 
 FOREIGN KEY (ALERTID) 
 REFERENCES TBLALERT (ALERTID);

ALTER TABLE TBLALERTMAILLOG
 ADD CONSTRAINT REFTBLALERT644 
 FOREIGN KEY (ALERTID) 
 REFERENCES TBLALERT (ALERTID);

ALTER TABLE TBLALERTMANUALNOTIFIER
 ADD CONSTRAINT REFTBLALERT640 
 FOREIGN KEY (ALERTID) 
 REFERENCES TBLALERT (ALERTID);

ALTER TABLE EPSMAILFORMAT
 ADD CONSTRAINT FK_EPSMAILFORMAT_WSCLANGUAGE 
 FOREIGN KEY (LANGUAGEID) 
 REFERENCES WSCLANGUAGE (ID);

ALTER TABLE EPSMAILFORMAT
 ADD CONSTRAINT FK_EPSMAILFORMAT_EPSMAILTYPE 
 FOREIGN KEY (TYPEID) 
 REFERENCES EPSMAILTYPE (ID);

ALTER TABLE TBLSPCDATASTORE
 ADD CONSTRAINT REFTBLSPCOBJECT682 
 FOREIGN KEY (OBJECTCODE) 
 REFERENCES TBLSPCOBJECT (OBJECTCODE);

ALTER TABLE TBLSPCITEMSPEC
 ADD CONSTRAINT REFTBLSPCOBJECT681 
 FOREIGN KEY (OBJECTCODE) 
 REFERENCES TBLSPCOBJECT (OBJECTCODE);

ALTER TABLE WSCROLEELEMENT
 ADD CONSTRAINT FK_WSCROLEELEMENT_WSCELEMENT 
 FOREIGN KEY (ELEMENTID) 
 REFERENCES WSCELEMENT (ID);

ALTER TABLE WSCMENULANGUAGE
 ADD CONSTRAINT FK_WSCMENULANGUAGE_WSCMENU 
 FOREIGN KEY (MENUID) 
 REFERENCES WSCMENU (ID);

ALTER TABLE WSCMENULANGUAGE
 ADD CONSTRAINT FK_WSCMENULANGUAGE_WSCLANGUAGE 
 FOREIGN KEY (LANGUAGEID) 
 REFERENCES WSCLANGUAGE (ID);

ALTER TABLE WSCUSER
 ADD CONSTRAINT FK_WSCUSER_WSCLANGUAGE 
 FOREIGN KEY (LANGUAGEID) 
 REFERENCES WSCLANGUAGE (ID);

ALTER TABLE WSCROLEMENU
 ADD CONSTRAINT FK_WSCROLEMENU_WSCMENU 
 FOREIGN KEY (MENUID) 
 REFERENCES WSCMENU (ID);

ALTER TABLE WSCRELATEDMENU
 ADD CONSTRAINT FK_WSCRELATEDMENU_WSCMENU 
 FOREIGN KEY (RID) 
 REFERENCES WSCMENU (ID);

ALTER TABLE WSCPARAMETER
 ADD CONSTRAINT FK_WSCPARAMETER_WSCPARAMETERTY 
 FOREIGN KEY (PTID) 
 REFERENCES WSCPARAMETERTYPE (ID);

ALTER TABLE WSCUSERGROUP
 ADD CONSTRAINT FK_WSCUSERGROUP_WSCUSER 
 FOREIGN KEY (USERID) 
 REFERENCES WSCUSER (USERID);

ALTER TABLE WSCUSERGROUP
 ADD CONSTRAINT FK_WSCUSERGROUP_WSCGROUP 
 FOREIGN KEY (GROUPID) 
 REFERENCES WSCGROUP (ID);

