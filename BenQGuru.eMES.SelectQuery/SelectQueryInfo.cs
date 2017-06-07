using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.SelectQuery
{
    public class SelectQueryInfo
    {
        private Type _DomainObjectType;
        private string _CodeFieldName;
        private string _DescFieldName;

        public SelectQueryInfo(string domainObjectType, string codePropertyName, string descPropertyName)
        {
            _DomainObjectType = Type.GetType(domainObjectType);
            _CodeFieldName = codePropertyName;
            _DescFieldName = descPropertyName;
        }

        public Type DomainObjectType
        {
            get { return _DomainObjectType; }
            set { _DomainObjectType = value; }
        }

        public string CodeFieldName
        {
            get { return _CodeFieldName; }
            set { _CodeFieldName = value; }
        }

        public string DescFieldName
        {
            get { return _DescFieldName; }
            set { _DescFieldName = value; }
        }

        public string GetTableName()
        {
            string returnValue = string.Empty;

            TableMapAttribute tableMap = DomainObjectUtility.GetTableMapAttribute(_DomainObjectType);
            if (tableMap != null)
            {
                returnValue = tableMap.TableName;
            }

            return returnValue;
        }

        public string GetCodeTableFieldName()
        {
            return DomainObjectUtility.GetFieldName(_DomainObjectType, _CodeFieldName);
        }

        public string GetDescTableFieldName()
        {
            return DomainObjectUtility.GetFieldName(_DomainObjectType, _DescFieldName);
        }
    }
}
