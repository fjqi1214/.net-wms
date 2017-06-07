---TBLMSDLevel
CREATE TABLE TBLMSDLevel
    (   MHumidityLevel                VARCHAR2(40) NOT NULL,
		MHumidityLevelDesc            VARCHAR2(100),
		FLOORLIFE                     NUMBER(10,0) NOT NULL,
		DRYINGTIME                    NUMBER(10,0) NOT NULL,
		MUSER                         VARCHAR2(40) NOT NULL,
		MDATE                         NUMBER(8,0) NOT NULL,
		MTIME                         NUMBER(6,0) NOT NULL,
		EATTRIBUTE1                   VARCHAR2(40))
		PCTFREE     10
		INITRANS    1
		MAXTRANS    255
/

-- Constraints for TBLMSDLevel
ALTER TABLE TBLMSDLevel
ADD PRIMARY KEY (MHumidityLevel)
USING INDEX
  PCTFREE     10
  INITRANS    2
  MAXTRANS    255
/



---TBLMaterialMSL
CREATE TABLE TBLMaterialMSL
    (   MCODE                VARCHAR2(40) NOT NULL,
		ORGID              NUMBER(8,0) NOT NULL,
		MHumidityLevel                  VARCHAR2(40) NOT NULL,
		INDRYINGTIME                    NUMBER(10,0) NOT NULL,
		MUSER                         VARCHAR2(40) NOT NULL,
		MDATE                         NUMBER(8,0) NOT NULL,
		MTIME                         NUMBER(6,0) NOT NULL,
		EATTRIBUTE1                   VARCHAR2(40))
		PCTFREE     10
		INITRANS    1
		MAXTRANS    255
/

-- Constraints for TBLMaterialMSL
ALTER TABLE TBLMaterialMSL
ADD PRIMARY KEY (MCODE,ORGID)
USING INDEX
  PCTFREE     10
  INITRANS    2
  MAXTRANS    255
/


---TBLMSDLOT
CREATE TABLE TBLMSDLOT
    (   LotNo                VARCHAR2(40) NOT NULL,
		Status               VARCHAR2(40) NOT NULL,
		Floorlife            NUMBER(15,5) NOT NULL,
		OverFloorlife        NUMBER(15,5) NOT NULL,
		MUSER                VARCHAR2(40) NOT NULL,
		MDATE                NUMBER(8,0),
		MTIME                NUMBER(6,0))
		PCTFREE     10
		INITRANS    1
		MAXTRANS    255
/

-- Constraints for TBLMSDLOT
ALTER TABLE TBLMSDLOT
ADD PRIMARY KEY (LotNo)
USING INDEX
  PCTFREE     10
  INITRANS    2
  MAXTRANS    255
/




---TBLITEMLot
CREATE TABLE TBLITEMLot
    (   LotNO               VARCHAR2(100) NOT NULL,
		MCODE               VARCHAR2(40) NOT NULL,
		ORGID               NUMBER(8,0) NOT NULL,
		TransNO             VARCHAR2(50),
	    TransLine	        NUMBER(8,0),
        VENDORITEMCODE	    VARCHAR2(100),
        VENDORCODE	        VARCHAR2(100),
        VenderLotNO	        VARCHAR2(40),
        DATECODE            NUMBER(8,0)  NOT NULL,
        LOTQTY	            NUMBER(13,0) NOT NULL,
        ACTIVE              VARCHAR2(1)  NOT NULL,
        Exdate	            NUMBER(8,0)  NOT NULL,
        PrintTimes	        NUMBER(6,0)  NOT NULL,
        lastPrintUSER       VARCHAR2(40) NOT NULL,
        lastPrintDate	    NUMBER(8,0)  NOT NULL,
        lastPrintTime	    NUMBER(6,0)  NOT NULL,
		MUSER               VARCHAR2(40) NOT NULL,
		MDATE               NUMBER(8,0)  NOT NULL,
		MTIME               NUMBER(6,0)  NOT NULL)
		PCTFREE     10
		INITRANS    1
		MAXTRANS    255
/

-- Constraints for TBLITEMLot
ALTER TABLE TBLITEMLot
ADD PRIMARY KEY (LotNo)
/


---TBLMSDWIP
CREATE TABLE TBLMSDWIP
	(
	SERIAL     NUMBER (38) NOT NULL,
	LOTNO   VARCHAR2 (40) NOT NULL,
	Status      VARCHAR2 (40) NOT NULL,
	MUSER   VARCHAR2 (40)NOT NULL,
	MDATE   NUMBER (8),
	MTIME   NUMBER (6)
	);
  alter table TBLMSDWIP
  add constraint PK_TBLMSDWIP primary key (serial);
	
CREATE SEQUENCE TBLMSDWIP_seq;
CREATE  TRIGGER TBLMSDWIP_BRI1
  BEFORE INSERT ON TBLMSDWIP
  FOR EACH ROW
BEGIN
  SELECT TBLMSDWIP_seq.NEXTVAL INTO :NEW.serial FROM DUAL;
END
/


insert into tblmdl (MDLCODE, PMDLCODE, MDLVER, MDLTYPE, MDLSTATUS, MDLDESC, MDLSEQ, MDLHFNAME, MUSER, MDATE, MTIME, ISSYS, ISACTIVE, FORMURL, EATTRIBUTE1, ISRESTRAIN)
values ('CS_MOISTURESENSITIVEDEVICES', 'CS', 'CS', 'C/S', 'Alpha', '湿敏元件', 55, '', 'ADMIN', 20110621, 113140, '1', '1', '', '', '0')
/

insert into tblmdl (MDLCODE, PMDLCODE, MDLVER, MDLTYPE, MDLSTATUS, MDLDESC, MDLSEQ, MDLHFNAME, MUSER, MDATE, MTIME, ISSYS, ISACTIVE, FORMURL, EATTRIBUTE1, ISRESTRAIN)
values ('MATERIALMSD', 'CS_MOISTURESENSITIVEDEVICES', '', 'C/S', 'Alpha', '湿敏元件处理', 1, '', 'ADMIN', 20110705, 90517, '1', '1', 'BenQGuru.eMES.Client.FMaterialMSD', '', '0')
/

insert into tblmdl (MDLCODE, PMDLCODE, MDLVER, MDLTYPE, MDLSTATUS, MDLDESC, MDLSEQ, MDLHFNAME, MUSER, MDATE, MTIME, ISSYS, ISACTIVE, FORMURL, EATTRIBUTE1, ISRESTRAIN)
values ('MSDALERT', 'CS_MOISTURESENSITIVEDEVICES', '', 'C/S', 'Alpha', '湿敏元件预警', 2, '', 'ADMIN', 20110705, 90535, '1', '1', 'BenQGuru.eMES.Client.FMSDAlter', '', '0')
/

alter table TBLMSDLEVEL add INDRYINGTIME NUMBER(10) not null
/

alter table TBLMaterialMSL drop column INDRYINGTIME
/
