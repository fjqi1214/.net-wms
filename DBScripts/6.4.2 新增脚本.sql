ALTER TABLE TBLSERIALBOOK ADD SERIALTYPE VARCHAR2(40);
/
alter table Tblsysparam
    add constraint PK_Tblsysparam_01 primary key (PARAMCODE,PARAMGROUPCODE);
/
CREATE OR REPLACE PACKAGE PKG_UPDATEMOQTY IS
  PROCEDURE UPDATEMOQTY;

END PKG_UPDATEMOQTY;
/
CREATE OR REPLACE PACKAGE BODY PKG_UPDATEMOQTY
IS
       V_SQL            VARCHAR2(5000) := '';
       v_Cur_Date       NUMBER;
      -- v_Cur_Time       NUMBER;

       --记录joblog
       procedure RecordJobResult(p_JobID IN VARCHAR2,
                         p_StartDateTime IN DATE,
                           p_EndDateTime IN DATE,
                          p_ProcessCount IN NUMBER,
                                p_Result IN VARCHAR2,
                              p_ErrorMsg IN VARCHAR2) is
              v_UsedSeconds NUMBER;
       begin
           v_UsedSeconds := (p_EndDateTime - p_StartDateTime) * 24 * 60 * 60;

           INSERT INTO tbljoblog(jobid,startdatetime,enddatetime,usedtime,processcount,result,errormsg)
           VALUES(p_JobID,p_StartDateTime,p_EndDateTime,v_UsedSeconds,p_ProcessCount,p_Result,p_ErrorMsg);
       commit;
       return;
       exception
       when others then
           return;

       end RecordJobResult;

       --更新moqty的数量
       PROCEDURE UPDATEMOQTY IS

       V_MOCODE                               VARCHAR2(40);
       V_MO_INPUT                             NUMBER(22);
       V_MO_OUTPUT                            NUMBER(22);
       I_PROCESSCOUNT                         NUMBER :=0;
       V_STARTDATETIME                        DATE;
       UPDATEMOQTY_CURSOR                     Sys_Refcursor;
       V_CUR_LASTDAY_DATE                     NUMBER(8);
       v_JobID                                VARCHAR2(40)    :=  'JOB_UpdateMoQty';
       v_ErrorID                              VARCHAR2(100);

       BEGIN

             SELECT sysdate INTO V_STARTDATETIME FROM dual;

             --获取昨天和今天的所有的有产量的工单
             v_Cur_Date                       :=  TO_NUMBER(TO_CHAR(sysdate,'yyyymmdd'));
             V_CUR_LASTDAY_DATE               :=  TO_NUMBER(TO_CHAR(sysdate-1,'yyyymmdd'));

             V_SQL                            :=  ' ';
             V_SQL                            := V_SQL || ' select distinct mocode from tblrptsoqty ';
             V_SQL                            := V_SQL ||' where shiftday >= '''||V_CUR_LASTDAY_DATE||''' ';

            OPEN  UPDATEMOQTY_CURSOR FOR V_SQL;
            LOOP
            FETCH UPDATEMOQTY_CURSOR
            INTO  V_MOCODE;
            EXIT WHEN UPDATEMOQTY_CURSOR%NOTFOUND;

            --获取该工单下的投入与产出的数量
            SELECT sum(MOINPUTCOUNT) AS INPUT ,sum(MOOUTPUTCOUNT) as OUTPUQTY INTO V_MO_INPUT,V_MO_OUTPUT
            FROM tblrptsoqty
            where mocode = V_MOCODE
            group by mocode
            order by mocode;


            I_PROCESSCOUNT           := I_ProcessCount+1;
            --更新共单的投入与产出数量
            update tblmo set MOINPUTQTY=V_MO_INPUT,MOACTQTY=V_MO_OUTPUT,
            MOSTATUS=(case  mostatus  when 'mostatus_release' then 'mostatus_open' else mostatus end),
            MOACTSTARTDATE=(case  mostatus  when 'mostatus_release' then v_Cur_Date else MOACTSTARTDATE end)
            where mocode=V_MOCODE;

            end loop;
          close UPDATEMOQTY_CURSOR;
          commit;
          RecordJobResult(v_JobID,v_StartDateTime,sysdate,
                            I_ProcessCount,'OK','');
          exception
              when others then
              v_ErrorID  := substrb(sqlerrm,1,100);
              rollback;
              RecordJobResult(v_JobID,v_StartDateTime,sysdate,
                               I_ProcessCount,'FAIL',v_ErrorID);
              commit;
       END UPDATEMOQTY;

END PKG_UPDATEMOQTY;
/

insert into tblmdl (MDLCODE, PMDLCODE, MDLVER, MDLTYPE, MDLSTATUS, MDLDESC, MDLSEQ, MDLHFNAME, MUSER, MDATE, MTIME, ISSYS, ISACTIVE, FORMURL, EATTRIBUTE1, ISRESTRAIN)
values ('ROUTE2OP', 'ROUTE', '', 'B/S', 'Alpha', '', 10, '', 'ADMIN', 20130703, 140145, '1', '1', 'BenQGuru.eMES.Web.Graphical/Route2Op.aspx', '', '0');

alter table TBLSOLDERPASTEPRO modify returntimespan number(15,5);
alter table TBLSOLDERPASTEPRO modify veiltimespan number(15,5);
alter table TBLSOLDERPASTEPRO modify unveiltimespan number(15,5);