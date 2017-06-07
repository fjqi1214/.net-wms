using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.Common.Domain
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldDisplayAttribute : Attribute
    {
        #region Fields

        private FieldDisplayModifyType _ModifyType;
        private string _RemoteTable;
        private string _RemoteKey;
        private string _RemoteField;

        #endregion

        public FieldDisplayAttribute(FieldDisplayModifyType modifyType, string localField)
        {
            _ModifyType = modifyType;
            _RemoteTable = string.Empty;
            _RemoteKey = string.Empty;
            _RemoteField = localField;
        }

        public FieldDisplayAttribute(FieldDisplayModifyType modifyType, string remoteTable, string remoteKey, string remoteField)
        {
            _ModifyType = modifyType;
            _RemoteTable = remoteTable;
            _RemoteKey = remoteKey;
            _RemoteField = remoteField;
        }

        #region Properties

        public FieldDisplayModifyType ModifyType
        {
            get { return _ModifyType; }
            set { _ModifyType = value; }
        }

        public string RemoteTable
        {
            get { return _RemoteTable; }
            set { _RemoteTable = value; }
        }

        public string RemoteKey
        {
            get { return _RemoteKey; }
            set { _RemoteKey = value; }
        }

        public string RemoteField
        {
            get { return _RemoteField; }
            set { _RemoteField = value; }
        }

        #endregion
    }

    public enum FieldDisplayModifyType
    {
        Replace,
        Append
    }
}
