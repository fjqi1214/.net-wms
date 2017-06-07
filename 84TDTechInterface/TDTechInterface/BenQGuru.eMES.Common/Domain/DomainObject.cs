using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

using BenQGuru.eMES.Common.PersistBroker;

namespace BenQGuru.eMES.Common.Domain
{
    [Serializable]
    public class DomainObject
    {
        private bool _isBlobIgnored = true;

        public DomainObject()
        {
        }

        public bool IsBlobIgnored
        {
            get
            {
                return _isBlobIgnored;
            }
            set
            {
                _isBlobIgnored = value;
            }
        }

        public bool Check()
        {
            return true;
        }

        public virtual string GetDisplayText(string fieldName)
        {
            string returnValue = string.Empty;

            FieldInfo info = this.GetType().GetField(fieldName);
            if (info != null)
            {
                returnValue = info.GetValue(this).ToString();

                if (info.IsDefined(typeof(FieldDisplayAttribute), true))
                {
                    object[] fieldDisplayList = info.GetCustomAttributes(typeof(FieldDisplayAttribute), true);
                    foreach (FieldDisplayAttribute fieldDisplay in fieldDisplayList)
                    {
                        string descString = string.Empty;

                        if (fieldDisplay.RemoteTable.Trim().Length <= 0
                            && fieldDisplay.RemoteField.Trim().Length > 0)
                        {
                            FieldInfo[] fieldInfoList = this.GetType().GetFields();
                            if (fieldInfoList != null)
                            {
                                foreach (FieldInfo fieldInfo in fieldInfoList)
                                {
                                    if (fieldInfo.IsDefined(typeof(FieldMapAttribute), true))
                                    {
                                        FieldMapAttribute fieldMap = (FieldMapAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(FieldMapAttribute));
                                        if (string.Compare(fieldMap.FieldName.Trim(), fieldDisplay.RemoteField.Trim(), true) == 0)
                                        {
                                            descString = fieldInfo.GetValue(this).ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            string sql = "SELECT {0} FROM {1} WHERE {2} = '{3}' ";
                            sql = string.Format(sql, fieldDisplay.RemoteField, fieldDisplay.RemoteTable, fieldDisplay.RemoteKey, info.GetValue(this).ToString());

                            IPersistBroker persistBroker = PersistBrokerManager.PersistBroker(true);
                            DataSet result = persistBroker.Query(sql);
                            if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                            {
                                descString = result.Tables[0].Rows[0][0].ToString();
                            }
                        }

                        if (descString.Trim().Length > 0)
                        {
                            if (fieldDisplay.ModifyType == FieldDisplayModifyType.Append)
                            {
                                returnValue += " - " + descString;
                            }
                            else if (fieldDisplay.ModifyType == FieldDisplayModifyType.Replace)
                            {
                                returnValue = descString;
                            }
                        }
                    }
                }
            }

            return returnValue;
        }
    }
}
