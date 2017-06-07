using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Document;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Web.Helper;
using System.Collections;

namespace BenQGuru.eMES.BaseSetting
{

    public class DocumentFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;
        private UserFacade _userFacade = null;

        public DocumentFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public DocumentFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        #region DOCDIR2USERGROUP
        /// <summary>
        /// TBLDOCDIR2USERGROUP
        /// </summary>
        public Docdir2UserGroup CreateNewDocdir2UserGroup()
        {
            return new Docdir2UserGroup();
        }

        public void AddDocdir2UserGroup(Docdir2UserGroup docdir2usergroup)
        {
            this.DataProvider.Insert(docdir2usergroup);
        }

        public void DeleteDocdir2UserGroup(Docdir2UserGroup docdir2usergroup)
        {
            this.DataProvider.Delete(docdir2usergroup);
        }

        public void UpdateDocdir2UserGroup(Docdir2UserGroup docdir2usergroup)
        {
            this.DataProvider.Update(docdir2usergroup);
        }

        public object GetDocdir2UserGroup(int DIRSERIAL, string usergroupcode, string type)
        {
            return this.DataProvider.CustomSearch(typeof(Docdir2UserGroup), new object[] { DIRSERIAL, usergroupcode, type });
        }

        public object[] QueryDocdir2UserGroups(string usergroupcode)
        {
            return this.DataProvider.CustomQuery(typeof(Docdir2UserGroup), new SQLCondition("select * from tblDocdir2UserGroup where usergroupcode='" + usergroupcode + "'"));
        }

        public object[] QueryDocdir2UserGroups(string dirSerial, string usergroupcode)
        {
            return this.DataProvider.CustomQuery(typeof(Docdir2UserGroup), new SQLCondition("select * from tblDocdir2UserGroup where usergroupcode='" + usergroupcode + "' and DIRSERIAL =" + dirSerial + ""));
        }

        public object[] QueryDocdir2UserGroups(string dirSerial, string usergroupcode, string dirType)
        {
            string sqlStr = " select * from tblDocdir2UserGroup where 1 = 1 ";
            if (dirSerial != string.Empty)
            {
                sqlStr += " and DIRSERIAL =" + dirSerial + "";
            }
            if (usergroupcode != string.Empty)
            {
                sqlStr += " and USERGROUPCODE ='" + usergroupcode + "'";
            }
            if (dirType != string.Empty)
            {
                sqlStr += " and DIRTYPE ='" + dirType + "'";
            }

            return this.DataProvider.CustomQuery(typeof(Docdir2UserGroup), new SQLCondition(sqlStr));
        }


        public bool GetDocDirRight(int docDir, string userCode, string type)
        {
            string[] docRight = new string[2];

            if (_userFacade == null)
            {
                _userFacade = new UserFacade(this.DataProvider);
            }
            //根据当前用户获取用户所在用户组，可能有多个
            object[] userGroups = _userFacade.GetAllUserGroup(userCode);

            if (userGroups != null && userGroups.Length > 0)
            {
                foreach (Domain.BaseSetting.UserGroup userGroup in userGroups)
                {
                    //根据用户组找到相信文档操作的权限,一个用户组可能有多个文档目录的权限(上传，查阅)
                    object docdir2UserGroups = this.GetDocdir2UserGroup(docDir, userGroup.UserGroupCode, type);

                    if (docdir2UserGroups != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void DeleteDocdir2UserGroupByDIRSerial(int dirSerial)
        {
            string sql = string.Format("delete from tblDocdir2UserGroup where dirserial = {0}", dirSerial);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region DOCDIR
        /// <summary>
        /// TBLDOCDIR
        /// </summary>
        public DocDir CreateNewDocDir()
        {
            return new DocDir();
        }

        public void AddDOCDIR(DocDir docdir)
        {
            this.DataProvider.Insert(docdir);
        }

        public void DeleteDOCDIR(DocDir docdir)
        {
            this.DataProvider.Delete(docdir);
        }

        public void DeleteDOCDIR(DocDir[] docdir)
        {
            this._helper.DeleteDomainObject(docdir);
        }

        public void UpdateDOCDIR(DocDir docdir)
        {
            this.DataProvider.Update(docdir);
        }

        public object GetDOCDIR(int DIRSERIAL)
        {
            return this.DataProvider.CustomSearch(typeof(DocDir), new object[] { DIRSERIAL });
        }

        public object[] GetAllDOCDIR()
        {
            return this.DataProvider.CustomQuery(typeof(DocDir), new SQLCondition(string.Format("select {0} from TBLDOCDIR order by DIRNAME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DocDir)))));
        }

        public object[] GetAllDOCDIROrderBySequence()
        {
            return this.DataProvider.CustomQuery(typeof(DocDir), new SQLCondition(string.Format("select {0} from TBLDOCDIR order by PDIRSERIAL, DIRSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DocDir)))));
        }

        public string GetDirNameBySerial(int DIRSERIAL)
        {
            object obj = this.DataProvider.CustomQuery(typeof(DocDir), new SQLCondition(string.Format("select DirName from TBLDOCDIR where dirserial = {0}", DIRSERIAL)));
            if (obj != null)
            {
                return ((DocDir)obj).Dirname;
            }
            return "";
        }

        public int GetMaxSerial()
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(DocDir), new SQLCondition("select max(dirserial) as dirserial from TBLDOCDIR"));
            if (obj != null)
            {
                return ((DocDir)obj[0]).Dirserial;
            }
            return -1;
        }

        public bool CheckHasSubDIR(int dirSerial)
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(DocDir), new SQLCondition(string.Format("select dirserial,dirname from TBLDOCDIR where pdirserial = {0}", dirSerial)));
            if (obj != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckDirSeq(int pdirSerial, int dirSeq)
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(DocDir), new SQLCondition(string.Format("select dirserial,dirname from TBLDOCDIR where pdirserial = {0} and dirseq = {1}", pdirSerial, dirSeq)));
            if (obj != null)
            {
                return true;
            }
            return false;
        }

        public object[] QuerySubDOCDIR(int parentDocDir, int inclusive, int exclusive)
        {
            string sql = "select a.dirserial,a.dirname,a.pdirserial,a.dirdesc,a.dirseq,a.muser,a.mdate,a.mtime, ";
            sql += "b.dirname as pdirname,c.uploadusergroupcode,c.queryusergroupcode,c.checkusergroupcode from TBLDOCDIR a ";
            sql += "left join TBLDOCDIR b on a.pdirserial = b.dirserial ";
            sql += "left join (select dirserial,max(decode(dirtype,'UPLOAD',usergroupcode,'')) as uploadusergroupcode, ";
            sql += "max(decode(dirtype,'QUERY',usergroupcode,'')) as queryusergroupcode,max(decode(dirtype,'CHECK',usergroupcode,'')) as checkusergroupcode from ( ";
            sql += "SELECT dirserial,dirtype,SUBSTR(MAX(SYS_CONNECT_BY_PATH(usergroupcode, ',')), 2) usergroupcode ";
            sql += "FROM (SELECT dirserial,dirtype,usergroupcode,rn,LEAD(rn) OVER(PARTITION BY dirserial,dirtype ORDER BY rn) rn1 ";
            sql += "FROM (SELECT a.dirserial,a.dirtype,b.usergroupcode as usergroupcode,ROW_NUMBER() OVER(ORDER BY a.usergroupcode) rn ";
            sql += "FROM TBLDOCDIR2USERGROUP a left join tblusergroup b on a.usergroupcode = b.usergroupcode)) START WITH rn1 IS NULL CONNECT BY rn1 = PRIOR rn ";
            sql += "GROUP BY dirserial,dirtype) GROUP BY dirserial ) c on a.dirserial = c.dirserial  ";
            if (parentDocDir == 0)
            {

                sql += " where a.pdirserial = 0 ";
                return this.DataProvider.CustomQuery(typeof(DocDirForQuery), new PagerCondition(sql, "a.DIRSEQ", inclusive, exclusive));
            }
            else
            {
                sql += " where a.pdirserial = {0} ";
                return this.DataProvider.CustomQuery(typeof(DocDirForQuery), new PagerCondition(string.Format(sql, parentDocDir), "a.DIRSEQ", inclusive, exclusive));
            }
        }

        public int QuerySubDOCDIRCount(int parentDocDir)
        {
            if (parentDocDir == 0)
            {
                return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLDOCDIR where PDIRSERIAL = 0")));
            }
            else
            {
                return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLDOCDIR where PDIRSERIAL = {0}", parentDocDir)));
            }
        }

        public object QuerySubDOCDIR(int dirserial)
        {
            string sql = "select a.dirserial,a.dirname,a.pdirserial,a.dirdesc,a.dirseq,a.muser,a.mdate,a.mtime, ";
            sql += "b.dirname as pdirname,c.uploadusergroupcode,c.queryusergroupcode,c.checkusergroupcode from TBLDOCDIR a ";
            sql += "left join TBLDOCDIR b on a.pdirserial = b.dirserial ";
            sql += "left join (select dirserial,max(decode(dirtype,'UPLOAD',usergroupcode,'')) as uploadusergroupcode, ";
            sql += "max(decode(dirtype,'QUERY',usergroupcode,'')) as queryusergroupcode, max(decode(dirtype,'CHECK',usergroupcode,'')) as checkusergroupcode from ( ";
            sql += "SELECT dirserial,dirtype,SUBSTR(MAX(SYS_CONNECT_BY_PATH(usergroupcode, ',')), 2) usergroupcode ";
            sql += "FROM (SELECT dirserial,dirtype,usergroupcode,rn,LEAD(rn) OVER(PARTITION BY dirserial,dirtype ORDER BY rn) rn1 ";
            sql += "FROM (SELECT dirserial,dirtype,usergroupcode,ROW_NUMBER() OVER(ORDER BY usergroupcode) rn ";
            sql += "FROM TBLDOCDIR2USERGROUP)) START WITH rn1 IS NULL CONNECT BY rn1 = PRIOR rn ";
            sql += "GROUP BY dirserial,dirtype) GROUP BY dirserial ) c on a.dirserial = c.dirserial  ";
            sql += " where a.dirserial = {0}";

            object[] objs = this.DataProvider.CustomQuery(typeof(DocDirForQuery), new SQLCondition(string.Format(sql, dirserial)));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }

        #endregion

        #region DOC
        /// <summary>
        /// TBLDOC
        /// </summary>
        public Doc CreateNewDOC()
        {
            return new Doc();
        }

        public void AddDOC(Doc doc)
        {
            this.DataProvider.Insert(doc);
        }

        public void DeleteDOC(Doc doc)
        {
            this.DataProvider.Delete(doc);
        }

        public void DeleteDOC(Doc[] doc)
        {
            this._helper.DeleteDomainObject(doc);
        }

        public void UpdateDOC(Doc doc)
        {
            this.DataProvider.Update(doc);
        }

        public object GetDOC(int DOCSERIAL)
        {
            return this.DataProvider.CustomSearch(typeof(Doc), new object[] { DOCSERIAL });
        }

        public int GetDocMaxSerial()
        {
            string sqlStr = "select MAX(DOCSERIAL) from TBLDOC doc where 1=1  ";
            return this.DataProvider.GetCount(new SQLCondition(sqlStr));
        }

        public object[] QueryDocuments(string dirserial, int inclusive, int exclusive)
        {
            string sqlStr = "select doc.* from tbldoc doc where 1= 1";
            if (dirserial.Trim() != string.Empty)
            {
                sqlStr += " And doc.dirserial = '" + dirserial + "'";
            }

            return this.DataProvider.CustomQuery(typeof(Doc), new PagerCondition(sqlStr, "docname , docver ", inclusive, exclusive));
        }


        public int QueryDocumentsCount(string dirserial)
        {
            string sqlStr = "select count(*) from TBLDOC doc where 1=1  ";

            if ((dirserial != string.Empty) && (dirserial.Trim() != string.Empty))
            {
                sqlStr += " and doc.dirserial = '" + dirserial.Trim() + "'";
            }

            return this.DataProvider.GetCount(new SQLCondition(sqlStr));
        }


        public int QueryDocumentsCount(string docName, string docNum, string itemCodeList, string opList, string keyword,
                    string memo, string docType, string validStatus, string checkedStatus)
        {
            string sql = "SELECT  Count(*) FROM tbldoc WHERE 1=1 ";

            if (docName != null && docName.Length != 0)
            {
                sql = string.Format("{0} and Docname like '%{1}%'", sql, docName.Trim());
            }

            if ((docNum != null) && (docNum.Trim() != string.Empty))
            {
                sql = string.Format(" {0} and Docnum like '%{1}%'", sql, docNum.Trim());
            }

            if (itemCodeList.Trim().Length > 0)
            {
                string[] itemCodes = itemCodeList.Split(',');
                string itemCodesInSql = string.Empty;
                foreach (string mCode in itemCodes)
                {
                    itemCodesInSql += " itemList like '%" + mCode + "%' or";
                }
                itemCodesInSql = itemCodesInSql.Substring(0, itemCodesInSql.Length - 2);
                sql = sql + string.Format(" and ({0}) ", itemCodesInSql);
            }

            if (opList.Trim().Length > 0)
            {
                string[] ops = opList.Split(',');
                string mOpsInSql = string.Empty;
                foreach (string op in ops)
                {
                    mOpsInSql += " oplist like '%" + op + "%' or";
                }
                mOpsInSql = mOpsInSql.Substring(0, mOpsInSql.Length - 2);
                sql = sql + string.Format(" and ({0}) ", mOpsInSql);
            }

            if (keyword != null && keyword.Length != 0)
            {
                sql = string.Format("{0} and Keyword LIKE '%{1}%'", sql, keyword.Trim());
            }

            if (memo != null && memo.Length != 0)
            {
                sql = string.Format("{0} and Memo LIKE '%{1}%'", sql, memo.Trim());
            }

            if (docType != null && docType.Length != 0)
            {
                sql = string.Format("{0} and DocType ='{1}'", sql, docType.Trim());
            }

            if (validStatus != null && validStatus.Length != 0)
            {
                sql = string.Format("{0} and validStatus ='{1}'", sql, validStatus.Trim());
            }

            if (checkedStatus != null && checkedStatus.Length != 0)
            {
                sql = string.Format("{0} and checkedStatus ='{1}'", sql, checkedStatus.Trim());
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryDocuments(string docName, string docNum, string itemCodeList, string opList, string keyword,
            string memo, string docType, string validStatus, string checkedStatus, int inclusive, int exclusive)
        {
            string sql = "SELECT doc.* ,docdir.dirname from TBLDOC doc left join tbldocdir docdir on doc.dirserial = docdir.dirserial WHERE 1=1 ";

            if (docName != null && docName.Length != 0)
            {
                sql = string.Format("{0} and UPPER(doc.Docname) like '%{1}%'", sql, docName);
            }

            if ((docNum != null) && (docNum.Trim() != string.Empty))
            {
                sql = string.Format(" {0} and UPPER(doc.Docnum) like '%{1}%'", sql, docNum);
            }

            if (itemCodeList.Trim().Length > 0)
            {
                string[] itemCodes = itemCodeList.Split(',');
                string itemCodesInSql = string.Empty;
                foreach (string mCode in itemCodes)
                {
                    itemCodesInSql += " itemList like '%" + mCode + "%' or";
                }
                itemCodesInSql = itemCodesInSql.Substring(0, itemCodesInSql.Length - 2);
                sql = sql + string.Format(" and ({0}) ", itemCodesInSql);
            }

            if (opList.Trim().Length > 0)
            {
                string[] ops = opList.Split(',');
                string mOpsInSql = string.Empty;
                foreach (string op in ops)
                {
                    mOpsInSql += " doc.Oplist like '%" + op + "%' or";
                }
                mOpsInSql = mOpsInSql.Substring(0, mOpsInSql.Length - 2);
                sql = sql + string.Format(" and ({0}) ", mOpsInSql);
            }

            if (keyword != null && keyword.Length != 0)
            {
                sql = string.Format("{0} and UPPER(doc.Keyword) LIKE '%{1}%'", sql, keyword.Trim());
            }

            if (memo != null && memo.Length != 0)
            {
                sql = string.Format("{0} and UPPER(doc.Memo) LIKE '%{1}%'", sql, memo);
            }

            if (docType != null && docType.Length != 0)
            {
                sql = string.Format("{0} and doc.DocType ='{1}'", sql, docType);
            }

            if (validStatus != null && validStatus.Length != 0)
            {
                sql = string.Format("{0} and doc.validStatus ='{1}'", sql, validStatus);
            }

            if (checkedStatus != null && checkedStatus.Length != 0)
            {
                sql = string.Format("{0} and doc.checkedStatus ='{1}'", sql, checkedStatus);
            }

            return this.DataProvider.CustomQuery(typeof(DocForQuery), new PagerCondition(sql, "  doc.Docname, doc.docver ", inclusive, exclusive));
        }


        #region add by sam
        public int QueryInDocumentsCount(string invdocno, string docName, string dirName,
                string remark1, string docType)
        {
            string sql = "SELECT  Count(*) FROM TBLINVDOC WHERE 1=1 ";
            if (invdocno != null && invdocno.Length != 0)
            {
                sql = string.Format("{0} and invdocno like '%{1}%'", sql, invdocno.Trim());
            }

            if (docName != null && docName.Length != 0)
            {
                sql = string.Format("{0} and Docname like '%{1}%'", sql, docName.Trim());
            }

            if (dirName != null && dirName.Length != 0)
            {
                sql = string.Format("{0} and dirName ='{1}'", sql, dirName.Trim());
            }

            if (remark1 != null && remark1.Length != 0)
            {
                sql = string.Format("{0} and remark1 like '%{1}%'", sql, remark1.Trim());
            }

            if (docType != null && docType.Length != 0)
            {
                sql = string.Format("{0} and DocType ='{1}'", sql, docType.Trim());
            }


            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public object[] QueryInDocuments1(string invdocno, string docName, string dirName,
            string remark1, string docType, string pickNo, string[] userCode, int inclusive, int exclusive)
        {
            string sql = @"select t3.* from TBLINVDOC t3,
(
select distinct t1.invdocno from  (
(select a.invdocno, b.storagecode from TBLINVDOC a,tblasn b where a.invdocno=b.stno and b.storagecode is not null) union
(select a.invdocno, b.fromstoragecode storagecode from TBLINVDOC a,tblasn b where a.invdocno=b.stno and b.fromstoragecode is not null) union
(select a.invdocno,b.storagecode from TBLINVDOC a,tblpick b where a.invdocno=b.pickno and b.storagecode is not null)  union 
(select a.invdocno,b.instoragecode from TBLINVDOC a,tblpick b where a.invdocno=b.pickno and b.instoragecode is not null)  union 
(select c.invdocno,b.storagecode from TBLCartonInvoices a,tblpick b,TBLINVDOC c where a.pickno=b.pickno and a.CARINVNO=c.invdocno and b.storagecode is not null) union
(select c.invdocno,b.instoragecode from TBLCartonInvoices a,tblpick b,TBLINVDOC c where a.pickno=b.pickno and a.CARINVNO=c.invdocno and b.instoragecode is not null) union
(select a.iqcno,b.storagecode from tblasniqc a,tblasn b ,tblinvdoc c where a.iqcno=c.invdocno and a.stno=b.stno and b.storagecode is not null ) union
(select a.iqcno,b.fromstoragecode from tblasniqc a,tblasn b ,tblinvdoc c where a.iqcno=c.invdocno and a.stno=b.stno and b.fromstoragecode is not null ) union
(select a.oqcno,b.storagecode from tbloqc a,tblpick b ,tblinvdoc c where a.oqcno=c.invdocno and a.pickno=b.pickno and b.storagecode is not null ) union
(select a.oqcno,b.instoragecode from tbloqc a,tblpick b ,tblinvdoc c where a.oqcno=c.invdocno and a.pickno=b.pickno and b.instoragecode is not null )
) t1 where t1.storagecode in (select paramcode from tblsysparam where paramgroupcode in(" + SqlFormat(userCode) + @"))
) t2 where T3.INVDOCNO=T2.INVDOCNO";// ,docdir.dirname  left join tbldocdir docdir on doc.dirserial = docdir.dirserial
            if (invdocno != null && invdocno.Length != 0)
            {
                sql = string.Format("{0} and t3.invdocno like '%{1}%'", sql, invdocno.Trim());
            }

            if (docName != null && docName.Length != 0)
            {
                sql = string.Format("{0} and t3.Docname like '%{1}%'", sql, docName.Trim());
            }

            if (dirName != null && dirName.Length != 0)
            {
                sql = string.Format("{0} and t3.dirName ='{1}'", sql, dirName.Trim());
            }

            if (remark1 != null && remark1.Length != 0)
            {
                sql = string.Format("{0} and t3.remark1 like '%{1}%'", sql, remark1.Trim());
            }

            if (docType != null && docType.Length != 0)
            {
                sql = string.Format("{0} and t3.DocType ='{1}'", sql, docType.Trim());
            }
            if (!string.IsNullOrEmpty(pickNo))
            {

                sql = string.Format("{0} and t3.pickno ='{1}'", sql, pickNo);
            }
            return this.DataProvider.CustomQuery(typeof(InDocForQuery), new PagerCondition(sql, "  t3.Docname ", inclusive, exclusive));
        }

        public int QueryInDocuments1Count(string invdocno, string docName, string dirName,
          string remark1, string docType, string pickNo, string[] userCode)
        {
            string sql = @"select count(*) from TBLINVDOC t3,
(
select distinct t1.invdocno from  (
(select a.invdocno, b.storagecode from TBLINVDOC a,tblasn b where a.invdocno=b.stno and b.storagecode is not null) union
(select a.invdocno, b.fromstoragecode storagecode from TBLINVDOC a,tblasn b where a.invdocno=b.stno and b.fromstoragecode is not null) union
(select a.invdocno,b.storagecode from TBLINVDOC a,tblpick b where a.invdocno=b.pickno and b.storagecode is not null)  union 
(select a.invdocno,b.instoragecode from TBLINVDOC a,tblpick b where a.invdocno=b.pickno and b.instoragecode is not null)  union 
(select c.invdocno,b.storagecode from TBLCartonInvoices a,tblpick b,TBLINVDOC c where a.pickno=b.pickno and a.CARINVNO=c.invdocno and b.storagecode is not null) union
(select c.invdocno,b.instoragecode from TBLCartonInvoices a,tblpick b,TBLINVDOC c where a.pickno=b.pickno and a.CARINVNO=c.invdocno and b.instoragecode is not null) union
(select a.iqcno,b.storagecode from tblasniqc a,tblasn b ,tblinvdoc c where a.iqcno=c.invdocno and a.stno=b.stno and b.storagecode is not null ) union
(select a.iqcno,b.fromstoragecode from tblasniqc a,tblasn b ,tblinvdoc c where a.iqcno=c.invdocno and a.stno=b.stno and b.fromstoragecode is not null ) union
(select a.oqcno,b.storagecode from tbloqc a,tblpick b ,tblinvdoc c where a.oqcno=c.invdocno and a.pickno=b.pickno and b.storagecode is not null ) union
(select a.oqcno,b.instoragecode from tbloqc a,tblpick b ,tblinvdoc c where a.oqcno=c.invdocno and a.pickno=b.pickno and b.instoragecode is not null )
) t1 where t1.storagecode in (select paramcode from tblsysparam where paramgroupcode in (" + SqlFormat(userCode) + @"))
) t2 where T3.INVDOCNO=T2.INVDOCNO";// ,docdir.dirname  left join tbldocdir docdir on doc.dirserial = docdir.dirserial
            if (invdocno != null && invdocno.Length != 0)
            {
                sql = string.Format("{0} and t3.invdocno like '%{1}%'", sql, invdocno.Trim());
            }

            if (docName != null && docName.Length != 0)
            {
                sql = string.Format("{0} and t3.Docname like '%{1}%'", sql, docName.Trim());
            }

            if (dirName != null && dirName.Length != 0)
            {
                sql = string.Format("{0} and t3.dirName ='{1}'", sql, dirName.Trim());
            }

            if (remark1 != null && remark1.Length != 0)
            {
                sql = string.Format("{0} and t3.remark1 like '%{1}%'", sql, remark1.Trim());
            }

            if (docType != null && docType.Length != 0)
            {
                sql = string.Format("{0} and t3.DocType ='{1}'", sql, docType.Trim());
            }
            if (!string.IsNullOrEmpty(pickNo))
            {

                sql = string.Format("{0} and t3.pickno ='{1}'", sql, pickNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string SqlFormat(string[] strs)
        {
            if (strs.Length == 0)
                return "''";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            foreach (string str in strs)
            {
                sb.Append("'");
                sb.Append(str);
                sb.Append("',");

            }
            return sb.ToString().TrimEnd(',');
        }

        public object[] QueryInDocuments(string invdocno, string docName, string dirName,
            string remark1, string docType, string pickNo, int inclusive, int exclusive)
        {
            string sql = "SELECT doc.* from TBLINVDOC doc WHERE 1=1 ";// ,docdir.dirname  left join tbldocdir docdir on doc.dirserial = docdir.dirserial
            if (invdocno != null && invdocno.Length != 0)
            {
                sql = string.Format("{0} and invdocno like '%{1}%'", sql, invdocno.Trim());
            }

            if (docName != null && docName.Length != 0)
            {
                sql = string.Format("{0} and Docname like '%{1}%'", sql, docName.Trim());
            }

            if (dirName != null && dirName.Length != 0)
            {
                sql = string.Format("{0} and dirName ='{1}'", sql, dirName.Trim());
            }

            if (remark1 != null && remark1.Length != 0)
            {
                sql = string.Format("{0} and remark1 like '%{1}%'", sql, remark1.Trim());
            }

            if (docType != null && docType.Length != 0)
            {
                sql = string.Format("{0} and DocType ='{1}'", sql, docType.Trim());
            }
            if (!string.IsNullOrEmpty(pickNo))
            {

                sql = string.Format("{0} and pickno ='{1}'", sql, pickNo);
            }

            return this.DataProvider.CustomQuery(typeof(InDocForQuery), new PagerCondition(sql, "  doc.Docname ", inclusive, exclusive));
        }


        #endregion
        public bool CheckVertion(string docVer, string docName, string docNum)
        {
            string sql = string.Format("select * from tbldoc where (docname = '{1}' and docver = '{0}') or (docnum = '{2}' and docver = '{0}')",
                docVer, docName, docNum);
            object[] result = this.DataProvider.CustomQuery(typeof(Doc), new SQLCondition(sql));
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckVertion(string serial, string docVer, string docName, string docNum)
        {
            string sql = string.Format("select * from tbldoc where ((docname = '{1}' and docver = '{0}') or (docnum = '{2}' and docver = '{0}'))  and docserial != '{3}'",
                docVer, docName, docNum, serial);
            object[] result = this.DataProvider.CustomQuery(typeof(Doc), new SQLCondition(sql));
            if (result != null)
            {
                return true;
            }
            return false;
        }

        public bool CheckHasDocuments(int dirSerial)
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(Doc), new SQLCondition(string.Format("select docserial,dirserial,docname from tbldoc where dirserial = {0}", dirSerial)));
            if (obj != null)
            {
                return true;
            }
            return false;
        }

        #endregion


        #region Document Tree
        /// <summary>
        /// ** 功能描述:	构建Document树
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-18
        /// ** 修 改:
        /// ** 日 期: 
        /// ** nunit
        /// </summary>
        /// <returns>根节点，ModuleCode为""</returns>
        public ITreeObjectNode BuildDocumentTree()
        {
            DocDir document = new DocDir();
            document.Dirserial = 0;

            DocumentTreeNode node = new DocumentTreeNode(document);

            object[] objs = this.GetAllDOCDIROrderBySequence();

            node.AddSubTreeObjectNodeRange(this.buildSubDocumentTree(node, objs));

            return node;
        }

        /// <summary>
        /// ** 功能描述:	构建parent的下一级模块
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-18
        /// ** 修 改:			一次取出所有的Module
        /// ** 日 期:  		2005-04-19
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        private ITreeObjectNode[] buildSubDocumentTree(DocumentTreeNode parent, object[] documents)
        {
            object[] objs = this.GetSubDocumentFromDocumentList(parent.docDir.Dirserial.ToString(), documents);

            if (objs != null)
            {
                ArrayList array = new ArrayList(objs.Length);
                DocumentTreeNode node = null;

                foreach (DocDir document in objs)
                {
                    node = new DocumentTreeNode(document, parent);
                    node.AddSubTreeObjectNodeRange(this.buildSubDocumentTree(node, documents));

                    array.Add(node);
                }

                return (ITreeObjectNode[])array.ToArray(typeof(DocumentTreeNode));
            }

            return null;
        }

        /// <summary>
        /// ** 功能描述:	由父模块代码和包含所有模块的数组获得父模块下一级的模块
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-04-19
        /// ** 修 改:		
        /// ** 日 期:  		
        /// </summary>
        /// <param name="parentModuleCode"></param>
        /// <param name="modules"></param>
        /// <returns></returns>
        private DocDir[] GetSubDocumentFromDocumentList(string parentdocDir, object[] documents)
        {
            ArrayList array = new ArrayList();

            if (documents != null)
            {
                foreach (DocDir document in documents)
                {
                    if (document.Pdirserial.ToString() == parentdocDir.ToUpper())
                    {
                        array.Add(document);
                    }
                }

                return (DocDir[])array.ToArray(typeof(DocDir));
            }

            return null;
        }
        #endregion
    }

    #region DocumentTreeNode
    [Serializable]
    public class DocumentTreeNode : ITreeObjectNode
    {
        private DocDir _docDir = null;
        [NonSerialized]
        private TreeObjectNodeHelper _helper = null;
        private DocumentTreeNode _parent = null;

        public DocDir docDir
        {
            get
            {
                return this._docDir;
            }
            set
            {
                this._docDir = value;
            }
        }

        public TreeObjectNodeHelper TreeObjectNodeHelper
        {
            get
            {
                return this._helper;
            }
            set
            {
                this._helper = value;
            }
        }

        public DocumentTreeNode(DocDir docDir)
        {
            this._docDir = docDir;
            this._parent = null;
            this._helper = new TreeObjectNodeHelper();
        }

        public DocumentTreeNode(DocDir docDir, DocumentTreeNode parentNode)
        {
            this._docDir = docDir;
            this._parent = parentNode;
            this._helper = parentNode.TreeObjectNodeHelper;
        }

        #region ITreeObjectNode 成员

        public string ID
        {
            get
            {
                return this._docDir.Dirserial.ToString();
            }
            set
            {
                // 不支持
            }
        }

        public string Text
        {
            get
            {
                if (this._docDir.Dirname != null)
                {
                    return this._docDir.Dirname.ToString();
                }
                else
                {
                    return "";
                }
            }
            set
            {
                // 不支持
            }
        }

        public ITreeObjectNode Parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = (DocumentTreeNode)value;
                this._helper = this._parent._helper;
            }
        }

        public ITreeObjectNode Root
        {
            get
            {
                return this._helper.GetRoot();
            }
        }

        public void AddSubTreeObjectNode(ITreeObjectNode node)
        {
            this._helper.AddSubTreeObjectNode(this, node);
        }

        public void AddSubTreeObjectNodeRange(ITreeObjectNode[] nodes)
        {
            this._helper.AddSubTreeObjectNodeRange(this, nodes);
        }

        public void DeleteSubTreeObjectNode(ITreeObjectNode node)
        {
            this._helper.DeleteSubTreeObjectNode(this, node);
        }

        public void MoveTreeObjectNode(ITreeObjectNode parent)
        {
            this._helper.MoveTreeObjectNode(this, parent);
        }

        public void Update()
        {
            this._helper.Update(this);
        }

        public TreeObjectNodeSet GetSubLevelChildrenNodes()
        {
            return this._helper.GetSubLevelChildrenNodes(this);
        }

        public ITreeObjectNode GetTreeObjectNodeByID(string id)
        {
            return this._helper.GetTreeObjectNodeByID(id);
        }

        public TreeObjectNodeSet GetChainFromRoot()
        {
            return this._helper.GetChainFromRoot(this);
        }

        public bool IsEqual(ITreeObjectNode node)
        {
            return this._helper.IsEqual(this, node);
        }

        public TreeObjectNodeSet GetAllNodes()
        {
            return this._helper.GetAllNodes(this);
        }

        #endregion
    }
    #endregion
}
