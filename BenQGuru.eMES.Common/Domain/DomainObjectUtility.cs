using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Reflection;
using System.Text;

namespace BenQGuru.eMES.Common.Domain
{
    public class DomainObjectUtility
    {
        public static void CheckDomainObject(object domainObject, IDomainDataProvider domainObjectDataProvider)
        {
            if (!(domainObject is DomainObject))
            {
                ExceptionManager.Raise(typeof(DomainObject), "$Error_Should_Be_DomainObject", string.Format("[$DomainObject={0}]", domainObject), null);
            }
        }
        public static object CreateTypeInstance(Type type)
        {
            return type.Assembly.CreateInstance(type.FullName);
        }
        public static ArrayList GetMemberInfos(Type type)
        {
            ArrayList list1 = new ArrayList();
            MemberInfo[] infoArray1 = type.GetMembers();
            for (int num1 = 0; num1 < infoArray1.Length; num1++)
            {
                MemberInfo info1 = infoArray1[num1];

                if (((info1.MemberType == MemberTypes.Field) || (info1.MemberType == MemberTypes.Property)) && (info1.IsDefined(typeof(FieldMapAttribute), true) || info1.IsDefined(typeof(FieldMapAttribute), false)))
                {
                    list1.Add(info1);
                }
            }
            return list1;
        }

        public static ArrayList GetMemberInfos(object obj)
        {
            return DomainObjectUtility.GetMemberInfos(obj.GetType());
        }

        public static Hashtable GetKeyAttributeMemberInfos(object obj)
        {
            return DomainObjectUtility.GetKeyAttributeMemberInfos(DomainObjectUtility.GetTableMapAttribute(obj), DomainObjectUtility.GetMemberInfos(obj));
        }

        public static Hashtable GetKeyAttributeMemberInfos(Type type)
        {
            return DomainObjectUtility.GetKeyAttributeMemberInfos(DomainObjectUtility.GetTableMapAttribute(type), DomainObjectUtility.GetMemberInfos(type));
        }

        public static Hashtable GetKeyAttributeMemberInfos(TableMapAttribute tableMapAttribute, ArrayList memberInfos)
        {
            StringCollection keyFields = new StringCollection();
            keyFields.AddRange(tableMapAttribute.GetKeyFields());
            Hashtable hs = new Hashtable();

            foreach (MemberInfo info1 in memberInfos)
            {
                FieldMapAttribute attribute = (FieldMapAttribute)System.Attribute.GetCustomAttribute(info1, typeof(FieldMapAttribute));
                if (keyFields.IndexOf(attribute.FieldName) != -1)
                {
                    hs.Add(attribute, info1);
                }
            }
            return hs;
        }

        public static Hashtable GetNonKeyAttributeMemberInfos(object obj)
        {
            return DomainObjectUtility.GetNonKeyAttributeMemberInfos(DomainObjectUtility.GetTableMapAttribute(obj), DomainObjectUtility.GetMemberInfos(obj));
        }

        public static Hashtable GetNonKeyAttributeMemberInfos(Type type)
        {
            return DomainObjectUtility.GetNonKeyAttributeMemberInfos(DomainObjectUtility.GetTableMapAttribute(type), DomainObjectUtility.GetMemberInfos(type));
        }

        public static Hashtable GetNonKeyAttributeMemberInfos(TableMapAttribute tableMapAttribute, ArrayList memberInfos)
        {
            StringCollection keyFields = new StringCollection();
            keyFields.AddRange(tableMapAttribute.GetKeyFields());
            Hashtable hs = new Hashtable();

            foreach (MemberInfo info1 in memberInfos)
            {
                FieldMapAttribute attribute = (FieldMapAttribute)System.Attribute.GetCustomAttribute(info1, typeof(FieldMapAttribute));
                if (keyFields.IndexOf(attribute.FieldName) == -1)
                {
                    hs.Add(attribute, info1);
                }
            }
            return hs;
        }

        private static Hashtable hashtableAttributeMember = new Hashtable();
        public static Hashtable GetAttributeMemberInfos(object obj)
        {
            lock (hashtableAttributeMember)
            {
                if (hashtableAttributeMember.ContainsKey(obj.GetType().FullName) == false)
                {
                    hashtableAttributeMember.Add(obj.GetType().FullName, DomainObjectUtility.GetAttributeMemberInfos(DomainObjectUtility.GetMemberInfos(obj)));
                }
            }
            return (Hashtable)hashtableAttributeMember[obj.GetType().FullName];
        }

        public static Hashtable GetAttributeMemberInfos(Type type)
        {
            lock (hashtableAttributeMember)
            {
                if (hashtableAttributeMember.ContainsKey(type.FullName) == false)
                {
                    hashtableAttributeMember.Add(type.FullName, DomainObjectUtility.GetAttributeMemberInfos(DomainObjectUtility.GetMemberInfos(type)));
                }
            }
            return (Hashtable)hashtableAttributeMember[type.FullName];
        }

        private static Hashtable GetAttributeMemberInfos(ArrayList memberInfos)
        {
            Hashtable hs = new Hashtable();

            foreach (MemberInfo info1 in memberInfos)
            {
                FieldMapAttribute attribute = (FieldMapAttribute)System.Attribute.GetCustomAttribute(info1, typeof(FieldMapAttribute));
                hs.Add(attribute, info1);
            }
            return hs;
        }

        public static TableMapAttribute GetTableMapAttribute(Type type)
        {
            object attribute = System.Attribute.GetCustomAttribute(type, typeof(TableMapAttribute));
            return ((attribute != null) ? (TableMapAttribute)attribute : null);
        }

        public static TableMapAttribute GetTableMapAttribute(object obj)
        {
            return DomainObjectUtility.GetTableMapAttribute(obj.GetType());
        }


        public static DomainObject[] FillDomainObject(Type type, DataSet ds)
        {
            if (ds == null)
            {
                return null;
            }

            if (ds.Tables[0].Rows.Count < 1)
            {
                return null;
            }

            return DomainObjectUtility.FillDomainObject(type, ds.Tables[0]);
        }

        private static DomainObject[] FillDomainObject(Type type, DataTable table)
        {
            DomainObject[] domainObjects = new DomainObject[table.Rows.Count];
            int num1 = 0;
            foreach (DataRow dr in table.Rows)
            {
                domainObjects[num1] = DomainObjectUtility.FillDomainObject(type.Assembly.CreateInstance(type.FullName), dr);
                num1 = num1 + 1;
            }

            return domainObjects;
        }

        public static DomainObject FillDomainObject(object obj, DataRow dataRow)
        {
            Hashtable hs = DomainObjectUtility.GetAttributeMemberInfos(obj);
            Hashtable hsFields = GetFieldNameMapAttribute(obj.GetType().FullName, hs);

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                if (hsFields.Contains(column.ColumnName.ToUpper()))
                {
                    DomainObjectUtility.SetValue(obj, (MemberInfo)hsFields[column.ColumnName.ToUpper()], dataRow[column.ColumnName]);
                }
            }

            return (DomainObject)obj;
        }
        private static Hashtable hashtableFieldNameMap = new Hashtable();
        private static Hashtable GetFieldNameMapAttribute(string typeFullName, Hashtable hs)
        {
            lock (hashtableFieldNameMap)
            {
                if (hashtableFieldNameMap.ContainsKey(typeFullName) == false)
                {
                    Hashtable hsFields = new Hashtable(hs.Count);
                    foreach (FieldMapAttribute fa in hs.Keys)
                    {
                        hsFields.Add(fa.FieldName.ToUpper(), hs[fa]);
                    }
                    hashtableFieldNameMap.Add(typeFullName, hsFields);
                }
            }
            return (Hashtable)hashtableFieldNameMap[typeFullName];
        }

        public static Hashtable GetFieldNameMemberInfoMap(Type type)
        {
            Hashtable returnValue = new Hashtable();

            foreach (MemberInfo info in type.GetMembers())
            {
                FieldMapAttribute attribute = (FieldMapAttribute)System.Attribute.GetCustomAttribute(info, typeof(FieldMapAttribute));

                if (attribute != null)
                {
                    returnValue.Add(attribute.FieldName.Trim().ToLower(), info);
                }
            }

            return returnValue;
        }

        public static object CSharpValue2DbValue(Type dataType, Type propertyType, object value)
        {
            if (propertyType == typeof(int))
            {
                int num1 = Convert.ToInt32(value);
                return num1;
            }

            if (propertyType == typeof(decimal))
            {
                Decimal num1 = Convert.ToDecimal(value);
                return num1;
            }

            if (propertyType == typeof(long))
            {
                long num1 = (long)value;
                return num1.ToString();
            }

            if (propertyType == typeof(string))
            {
                if (value == null)
                {
                    return "";
                }
                return value.ToString();
            }

            if (propertyType == typeof(bool))
            {
                if (!((bool)value))
                {
                    return "N";
                }
                return "Y";
            }

            if (propertyType == typeof(DateTime))
            {
                DateTime time1 = (DateTime)value;
                return time1.ToString("yyyy/MM/dd HH:mm:ss");
            }

            if (propertyType == typeof(byte[]))
            {
                return value;
            }

            return value.ToString();

        }

        public static string XMLEncodeValue(Type propertyType, object value)
        {
            if (propertyType == typeof(int))
            {
                int num1 = (int)value;
                return num1.ToString();
            }
            if (propertyType == typeof(string))
            {
                if (value == null)
                {
                    return "";
                }
                return value.ToString();
            }
            if (propertyType == typeof(bool))
            {
                if (!((bool)value))
                {
                    return "N";
                }
                return "Y";
            }

            if (propertyType == typeof(DateTime))
            {
                DateTime time1 = (DateTime)value;
                return (time1.ToString("yyyy/MM/dd HH:mm:ss"));
            }
            return value.ToString();
        }

        public static object GetValue(object obj, MemberInfo mi, object[] index)
        {
            if (mi is FieldInfo)
            {
                return ((FieldInfo)mi).GetValue(obj);
            }
            if (mi is PropertyInfo)
            {
                return ((PropertyInfo)mi).GetValue(obj, index);
            }

            return null;
        }

        public static object GetValue(object domainObject, string propertyName, object[] index)
        {
            MemberInfo[] infos = domainObject.GetType().GetMember(propertyName);

            if (infos == null)
            {
                ExceptionManager.Raise(typeof(DomainObject), "$Error_Property_Name_Not_Exist", null, null);
                return null;
            }

            return GetValue(domainObject, infos[0], index);
        }

        public static void SetValue(object domainObject, string propertyName, object value)
        {
            ArrayList infos = DomainObjectUtility.GetMemberInfos(domainObject);
            MemberInfo info1 = null;

            foreach (MemberInfo info in infos)
            {
                if (info.Name == propertyName)
                {
                    info1 = info;
                    break;
                }
            }

            if (info1 == null)
            {
                ExceptionManager.Raise(typeof(DomainObject), "$Error_Property_Name_Not_Exist");
            }

            DomainObjectUtility.SetValue(domainObject, info1, value);
        }

        private static void SetValue(object domainObject, MemberInfo info, object value)
        {
            if (info == null)
            {
                return;
            }

            FieldMapAttribute fa = (FieldMapAttribute)System.Attribute.GetCustomAttribute(info, typeof(FieldMapAttribute));

            if (fa == null)
            {
                return;
            }

            Type type1 = (info is FieldInfo) ? ((FieldInfo)info).FieldType : ((PropertyInfo)info).PropertyType;
            if (fa.BlobType == BlobTypes.Binary)
            {
                if (!((DomainObject)domainObject).IsBlobIgnored)
                {
                    DomainObjectUtility.SetValue(domainObject, info, value, null);
                    return;
                }
            }

            if (type1 == typeof(int))
            {
                if (value is System.DBNull)
                {
                    return;
                }
                else
                {
                    DomainObjectUtility.SetValue(domainObject, info, System.Int32.Parse(value.ToString()), null);
                    return;
                }
            }

            if (type1 == typeof(long))
            {
                if (value is System.DBNull)
                {
                    return;
                }

                DomainObjectUtility.SetValue(domainObject, info, System.Int64.Parse(value.ToString()), null);
                return;
            }

            if (type1 == typeof(double))
            {
                if (value is System.DBNull)
                {
                    return;
                }

                DomainObjectUtility.SetValue(domainObject, info, System.Double.Parse(value.ToString()), null);
                return;
            }

            if (type1 == typeof(float))
            {
                if (value is System.DBNull)
                {
                    return;
                }

                DomainObjectUtility.SetValue(domainObject, info, System.Single.Parse(value.ToString()), null);
                return;
            }

            if (type1 == typeof(decimal))
            {
                if (value is System.DBNull)
                {
                    return;
                }

                //DomainObjectUtility.SetValue(domainObject, info, System.Decimal.Parse(DomainObjectUtility.GetValueString(value)), null);		
                //说明： 以前的方法在高精度的double数据转换成decimal数据的时候，报参数类型不对的错误，原因是高精度的double的数据在ToString之后会编程科学记数法，因此decimal.parse的时候会出错
                //解决方法： 私有方法专门对
                DomainObjectUtility.SetValue(domainObject, info, DomainObjectUtility.Getdecimal(value), null);
                return;
            }

            if (type1 == typeof(bool))
            {
                DomainObjectUtility.SetValue(domainObject, info, value.ToString() == "Y", null);
                return;
            }
            if (type1 == typeof(string))
            {
                DomainObjectUtility.SetValue(domainObject, info, value.ToString(), null);
                return;
            }

            if (type1 == typeof(DateTime))
            {
                DateTime result;
                DateTime.TryParse(value.ToString(), out result);
                DomainObjectUtility.SetValue(domainObject, info, result, null);

                return;
            }

            if (type1.IsEnum)
            {
                DomainObjectUtility.SetValue(domainObject, info, Enum.Parse(type1, value.ToString()), null);
                return;
            }

            DomainObjectUtility.SetValue(domainObject, info, value, null);
        }

        private static decimal Getdecimal(object obj)
        {
            string returnStr = obj.ToString();
            if (obj.GetType() == typeof(double))
            {
                return (decimal)Math.Round((double)obj, 8);
            }

            return decimal.Parse(returnStr);
        }

        public static void SetValue(object obj, MemberInfo mi, object value, object[] index)
        {
            if (mi is FieldInfo)
            {
                if (value is System.DBNull)
                {
                    ((FieldInfo)mi).SetValue(obj, null);
                }
                else
                {

                    ((FieldInfo)mi).SetValue(obj, value);
                }
            }
            else if (mi is PropertyInfo)
            {
                if (value is System.DBNull)
                {
                    ((PropertyInfo)mi).SetValue(obj, null, index);
                }
                else
                {
                    ((PropertyInfo)mi).SetValue(obj, value, index);
                }
            }
        }

        /// <summary>
        /// 获得DomainObject中定义了FieldMapAttribute的属性名或字段名
        /// </summary>
        /// <param name="type">DomainObject的类型</param>
        /// <returns></returns>
        public static ArrayList GetDomainObjectScheme(Type type)
        {
            ArrayList array = DomainObjectUtility.GetMemberInfos(type);
            ArrayList scheme = new ArrayList(array.Count);

            foreach (MemberInfo info in array)
            {
                scheme.Add(info.Name);
            }

            return scheme;
        }

        /// <summary>
        /// 获得DomainObject中定义了FieldMapAttribute，且为主键的属性名或字段名
        /// </summary>
        /// <param name="type">DomainObject的类型</param>
        /// <returns></returns>
        public static string[] GetDomainObjectKeyScheme(Type type)
        {
            Hashtable ht = DomainObjectUtility.GetKeyAttributeMemberInfos(type);
            ArrayList scheme = new ArrayList(ht.Count);

            foreach (MemberInfo info in ht.Values)
            {
                scheme.Add(info.Name);
            }

            return (string[])scheme.ToArray(typeof(string));
        }

        /// <summary>
        ///  获得DomainObject中定义了FieldMapAttribute的属性名或字段名的值
        /// </summary>
        /// <param name="obj">DomainObject</param>
        /// <returns></returns>
        public static ArrayList GetDomainObjectValues(DomainObject obj)
        {
            ArrayList array = DomainObjectUtility.GetMemberInfos(obj);
            ArrayList values = new ArrayList(array.Count);

            foreach (MemberInfo info in array)
            {
                values.Add(DomainObjectUtility.GetValue(obj, info, null));
            }

            return values;
        }

        /// <summary>
        ///  获得DomainObject中定义了FieldMapAttribute，且为主键的属性名或字段名的值
        /// </summary>
        /// <param name="obj">DomainObject</param>
        /// <returns></returns>
        public static object[] GetDomainObjectKeyValues(DomainObject obj)
        {
            Hashtable ht = DomainObjectUtility.GetKeyAttributeMemberInfos(obj);
            ArrayList values = new ArrayList(ht.Count);

            foreach (MemberInfo info in ht.Values)
            {
                values.Add(DomainObjectUtility.GetValue(obj, info, null));
            }

            return (object[])values.ToArray(typeof(object));
        }

        /// <summary>
        /// 获得DomainObject对应的所有数据库字段名
        /// </summary>
        /// <param name="type">DomainObject的类型</param>
        /// <returns></returns>
        public static ArrayList GetDomainObjectFieldsName(Type type)
        {
            Hashtable hs = DomainObjectUtility.GetAttributeMemberInfos(type);
            ArrayList array = new ArrayList(hs.Count);

            foreach (FieldMapAttribute attr in hs.Keys)
            {
                array.Add(attr.FieldName);
            }

            return array;
        }

        /// <summary>
        /// 获得DomainObject对应的所有数据库字段名，用','拼成的字符串
        /// </summary>
        /// <param name="type">DomainObject的类型</param>
        /// <returns></returns>
        public static string GetDomainObjectFieldsString(Type type)
        {
            return String.Join(", ", (string[])DomainObjectUtility.GetDomainObjectFieldsName(type).ToArray(typeof(string)));
        }

        /// <summary>
        /// 获得DomainObject对应的 数据库表名.字段名，用','拼成的字符串
        /// </summary>
        /// <param name="type">DomainObject的类型</param>
        /// <returns></returns>
        public static string GetDomainObjectFieldsStringWithTableName(Type type)
        {
            string tableName = DomainObjectUtility.GetTableMapAttribute(type).TableName;
            string[] fieldNames = (string[])DomainObjectUtility.GetDomainObjectFieldsName(type).ToArray(typeof(string));

            for (int i = 0; i < fieldNames.Length; i++)
            {
                fieldNames[i] = string.Format("{0}.{1}", tableName, fieldNames[i]);
            }

            return String.Join(", ", fieldNames);
        }

        /// <summary>
        /// 将src中各属性的值复制到des中
        /// </summary>
        /// <param name="src"></param>
        /// <param name="des"></param>
        public static void MembersClone(DomainObject src, DomainObject des)
        {
            foreach (MemberInfo info in DomainObjectUtility.GetMemberInfos(src))
            {
                DomainObjectUtility.SetValue(des, info.Name, DomainObjectUtility.GetValue(src, info, null));
            }
        }

        /// <summary>
        /// 根据对象的字段名取出数据库中的字段名
        /// </summary>
        /// <param name="type">domain对象</param>
        /// <param name="attributeName">domain对象的字段名</param>
        /// <returns>数据库中的字段名</returns>
        public static string GetFieldName(Type type, string attributeName)
        {
            System.Reflection.MemberInfo[] mems = type.GetMembers();
            foreach (System.Reflection.MemberInfo mem in mems)
            {
                if (mem.Name == attributeName)
                {
                    object[] objs = mem.GetCustomAttributes(typeof(BenQGuru.eMES.Common.Domain.FieldMapAttribute), true);
                    if (objs != null && objs.Length > 0)
                    {
                        BenQGuru.eMES.Common.Domain.FieldMapAttribute fieldMap = objs[0] as BenQGuru.eMES.Common.Domain.FieldMapAttribute;
                        if (fieldMap != null)
                        {
                            return fieldMap.FieldName;
                        }
                    }
                }
            }
            return null;
        }

        public static string GetPropertyNameByFieldName(Type domainObjectType, string fieldName)
        {
            string returnValue = string.Empty;

            if (fieldName.IndexOf(".") >= 0)
            {
                fieldName = fieldName.Substring(fieldName.IndexOf(".") + 1);
            }

            if (fieldName.Trim().Length > 0)
            {
                Hashtable fieldNameMemberInfoMap = GetFieldNameMemberInfoMap(domainObjectType);
                MemberInfo info = (MemberInfo)fieldNameMemberInfoMap[fieldName.Trim().ToLower()];

                if (info != null)
                {
                    returnValue = info.Name;
                }
            }

            return returnValue;
        }
    }
}
