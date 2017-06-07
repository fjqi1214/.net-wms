using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;
namespace BenQGuru.eMES.Domain.Document
{
    #region DOCDIR
    /// <summary>
    /// TBLDOCDIR
    /// </summary>
    [Serializable, TableMap("TBLDOCDIR", "DIRSERIAL")]
    public class DocDir : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public DocDir()
        {
        }

        ///<summary>
        ///DIRSERIAL
        ///</summary>
        [FieldMapAttribute("DIRSERIAL", typeof(int), 22, false)]
        public int Dirserial;

        ///<summary>
        ///DIRNAME
        ///</summary>
        [FieldMapAttribute("DIRNAME", typeof(string), 40, false)]
        public string Dirname;

        ///<summary>
        ///PDIRSERIAL
        ///</summary>
        [FieldMapAttribute("PDIRSERIAL", typeof(int), 22, false)]
        public int Pdirserial;

        ///<summary>
        ///DIRDESC
        ///</summary>
        [FieldMapAttribute("DIRDESC", typeof(string), 100, true)]
        public string Dirdesc;

        ///<summary>
        ///DIRSEQ
        ///</summary>
        [FieldMapAttribute("DIRSEQ", typeof(int), 22, false)]
        public int Dirseq;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region DOCDIR2USERGROUP
    /// <summary>
    /// TBLDOCDIR2USERGROUP
    /// </summary>
    [Serializable, TableMap("TBLDOCDIR2USERGROUP", "DIRSERIAL,USERGROUPCODE,DIRTYPE")]
    public class Docdir2UserGroup : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Docdir2UserGroup()
        {
        }

        ///<summary>
        ///DIRSERIAL
        ///</summary>
        [FieldMapAttribute("DIRSERIAL", typeof(int), 22, false)]
        public int Dirserial;

        ///<summary>
        ///USERGROUPCODE
        ///</summary>
        [FieldMapAttribute("USERGROUPCODE", typeof(string), 40, false)]
        public string Usergroupcode;

        ///<summary>
        ///DIRTYPE
        ///</summary>
        [FieldMapAttribute("DIRTYPE", typeof(string), 40, false)]
        public string Dirtype;

        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, true)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;

    }
    #endregion

    #region DOC
    /// <summary>
    /// TBLDOC
    /// </summary>
    [Serializable, TableMap("TBLDOC", "DOCSERIAL")]
    public class Doc : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Doc()
        {
        }

        ///<summary>
        ///DOCSERIAL
        ///</summary>
        [FieldMapAttribute("DOCSERIAL", typeof(int), 22, false)]
        public int Docserial;

        ///<summary>
        ///DIRSERIAL
        ///</summary>
        [FieldMapAttribute("DIRSERIAL", typeof(int), 22, false)]
        public int Dirserial;

        ///<summary>
        ///DOCNAME
        ///</summary>
        [FieldMapAttribute("DOCNAME", typeof(string), 40, false)]
        public string Docname;

        ///<summary>
        ///DOCNUM
        ///</summary>
        [FieldMapAttribute("DOCNUM", typeof(string), 40, true)]
        public string Docnum;

        ///<summary>
        ///DOCVER
        ///</summary>
        [FieldMapAttribute("DOCVER", typeof(string), 40, true)]
        public string Docver;

        ///<summary>
        ///DOCCHGNUM
        ///</summary>
        [FieldMapAttribute("DOCCHGNUM", typeof(string), 100, true)]
        public string Docchgnum;

        ///<summary>
        ///DOCCHGFILE
        ///</summary>
        [FieldMapAttribute("DOCCHGFILE", typeof(string), 100, true)]
        public string Docchgfile;

        ///<summary>
        ///MCODELIST
        ///</summary>
        [FieldMapAttribute("ITEMLIST", typeof(string), 4000, true)]
        public string Itemlist;

        ///<summary>
        ///OPLIST
        ///</summary>
        [FieldMapAttribute("OPLIST", typeof(string), 4000, true)]
        public string Oplist;

        ///<summary>
        ///MEMO
        ///</summary>
        [FieldMapAttribute("MEMO", typeof(string), 2000, true)]
        public string Memo;

        ///<summary>
        ///KEYWORD
        ///</summary>
        [FieldMapAttribute("KEYWORD", typeof(string), 2000, true)]
        public string Keyword;

        ///<summary>
        ///DOCTYPE
        ///</summary>
        [FieldMapAttribute("DOCTYPE", typeof(string), 40, true)]
        public string Doctype;

        ///<summary>
        ///UPUSER
        ///</summary>
        [FieldMapAttribute("UPUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string Upuser;

        ///<summary>
        ///UPFILEDATE
        ///</summary>
        [FieldMapAttribute("UPFILEDATE", typeof(int), 22, false)]
        public int Upfiledate;


        ///<summary>
        ///CHECKEDSTATUS
        ///</summary>
        [FieldMapAttribute("CHECKEDSTATUS", typeof(string), 40, false)]
        public string Checkedstatus;

        ///<summary>
        ///VALIDSTATUS
        ///</summary>
        [FieldMapAttribute("VALIDSTATUS", typeof(string), 40, false)]
        public string Validstatus;


        ///<summary>
        ///MUSER
        ///</summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        [FieldDisplay(FieldDisplayModifyType.Append, "TBLUSER", "USERCODE", "USERNAME")]
        public string MaintainUser;

        ///<summary>
        ///MDATE
        ///</summary>
        [FieldMapAttribute("MDATE", typeof(int), 22, false)]
        public int Mdate;

        ///<summary>
        ///MTIME
        ///</summary>
        [FieldMapAttribute("MTIME", typeof(int), 22, false)]
        public int Mtime;


         ///<summary>
        ///SERVERFULLNAME
        ///</summary>
        [FieldMapAttribute("SERVERFILENAME", typeof(string), 40,true)]
        public string ServerFileName;
        

    }
    #endregion

    #region DocForQuery
    public class DocForQuery : Doc
    {
        public DocForQuery()
        {
        }

        [FieldMapAttribute("DIRNAME", typeof(string), 40, true)]
        public string DirName;
    }
    #endregion


    #region DocDirForQuery
    public class DocDirForQuery : DocDir
    {
        public DocDirForQuery()
        {
        }

        [FieldMapAttribute("PDIRNAME", typeof(string), 40, true)]
        public string PDirName;

        [FieldMapAttribute("UPLOADUSERGROUPCODE", typeof(string), 200, true)]
        public string UploadUsergroupcode;

        [FieldMapAttribute("QUERYUSERGROUPCODE", typeof(string), 200, true)]
        public string QueryUsergroupcode;

        [FieldMapAttribute("CHECKUSERGROUPCODE", typeof(string), 200, true)]
        public string CheckUsergroupcode;

    }
    #endregion


}
