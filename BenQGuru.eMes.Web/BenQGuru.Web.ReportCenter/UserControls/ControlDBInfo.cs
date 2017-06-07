using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace BenQGuru.Web.ReportCenter.UserControls
{
    public class ControlWhereInfo
    {
        private Control _Control = null;
        private List<string> _TableFieldList = null;
        private string _SQLOperation = string.Empty;
        private bool _IsString = true;
        private bool _ToUpper = true;
        private bool _EmptyMeansAll = true;

        public ControlWhereInfo(Control control, List<string> tableFieldList, bool emptyMeansAll, bool isString, bool toUpper, string sqlOperation)
        {
            _Control = control;
            _TableFieldList = tableFieldList;
            _SQLOperation = sqlOperation;
            _IsString = isString;
            _ToUpper = toUpper;
            _EmptyMeansAll = emptyMeansAll;
        }

        public Control Control
        {
            get { return _Control; }
            set { _Control = value; }
        }

        public List<string> TableFieldList
        {
            get { return _TableFieldList; }
            set { _TableFieldList = value; }
        }

        public string SQLOperation
        {
            get { return _SQLOperation; }
            set { _SQLOperation = value; }
        }

        public bool IsString
        {
            get { return _IsString; }
            set { _IsString = value; }
        }

        public bool ToUpper
        {
            get { return _ToUpper; }
            set { _ToUpper = value; }
        }

        public bool EmptyMeansAll
        {
            get { return _EmptyMeansAll; }
            set { _EmptyMeansAll = value; }
        }
    }

    public class ControlGroupInfo
    {
        private Control _Control = null;
        private string _ControlValue = string.Empty;
        private List<DBFieldInfo> _TableFieldList;

        public ControlGroupInfo(Control control, string controlValue, List<DBFieldInfo> tableFieldList)
        {
            _Control = control;
            _ControlValue = controlValue;
            _TableFieldList = tableFieldList;
        }

        public Control Control
        {
            get { return _Control; }
            set { _Control = value; }
        }

        public string ControlValue
        {
            get { return _ControlValue; }
            set { _ControlValue = value; }
        }

        public List<DBFieldInfo> TableFieldList
        {
            get { return _TableFieldList; }
            set { _TableFieldList = value; }
        }
    }

    public class DBFieldInfo
    {
        private string _Alias = string.Empty;
        private string _FieldString = string.Empty;

        public DBFieldInfo(string fieldString, string alias)
        {
            _Alias = alias;
            _FieldString = fieldString;
        }

        public string Alias
        {
            get { return _Alias; }
            set { _Alias = value; }
        }

        public string FieldString
        {
            get { return _FieldString; }
            set { _FieldString = value; }
        }
    }
}
