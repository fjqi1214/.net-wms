insert into TBLMDL (MDLCODE, PMDLCODE, MDLVER, MDLTYPE, MDLSTATUS, MDLDESC, MDLSEQ, MDLHFNAME, MUSER, MDATE, MTIME, ISSYS, ISACTIVE, FORMURL, EATTRIBUTE1, ISRESTRAIN)
values ('RMA_QUERY_RMAREWORK', 'RMA_QUERY', '', 'B/S', 'Alpha', 'RMA返工记录查询', 0, '', 'ADMIN', 20080701, 80000, '1', '1', 'WebQuery/FRMAReworkQP.aspx', '', '0');

insert into TBLMENU (MENUCODE, MDLCODE, PMENUCODE, MENUDESC, MENUSEQ, MENUTYPE, MUSER, MDATE, MTIME, EATTRIBUTE1, VISIBILITY)
values ('RMA_QUERY_RMAREWORK', 'RMA_QUERY_RMAREWORK', 'RMA_QUERY', 'RMA返工记录查询', 8, 'B/S', 'ADMIN', 20080701, 80000, '', '');


-- Create table TBLRMABILL
create table TBLRMABILL
(
  RMABILLCODE VARCHAR2(40) not null,
  STATUS      VARCHAR2(40) not null,
  MEMO        VARCHAR2(200),
  MUSER       VARCHAR2(40) not null,
  MDATE       NUMBER(8) not null,
  MTIME       NUMBER(6) not null,
  EATTRIBUTE1 VARCHAR2(40)
);
-- Create/Recreate primary, unique and foreign key constraints 
alter table TBLRMABILL
  add constraint TBLRMABILL_PK primary key (RMABILLCODE);
  
  
  
  -- Create table TBLRMADETIAL
create table TBLRMADETIAL
(
  RMABILLCODE   VARCHAR2(40) not null,
  SERVERCODE    VARCHAR2(40) not null,
  MODELCODE     VARCHAR2(40) not null,
  ITEMCODE      VARCHAR2(40) not null,
  RCARD         VARCHAR2(40) not null,
  HANDELCODE    VARCHAR2(40) not null,
  COMPISSUE     VARCHAR2(200) not null,
  CUSTOMCODE    VARCHAR2(40) not null,
  COMFROM       VARCHAR2(100) not null,
  MAINTENANCE   NUMBER(4) not null,
  WHRECEIVEDATE NUMBER(8),
  SUBCOMPANY    VARCHAR2(40),
  REMOCODE      VARCHAR2(40),
  ERRORCODE     VARCHAR2(40),
  ISINSHELFLIFE VARCHAR2(2),
  MEMO          VARCHAR2(200),
  MUSER         VARCHAR2(40) not null,
  MDATE         NUMBER(8) not null,
  MTIME         NUMBER(6) not null,
  EATTRIBUTE1   VARCHAR2(40)
);
-- Create/Recreate primary, unique and foreign key constraints 
alter table TBLRMADETIAL
  add constraint TBLRMADETIAL_PK primary key (RMABILLCODE, RCARD);
