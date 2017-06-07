using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.MutiLanguage;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.BaseDataModel
{

    public class ImportDataEngine
    {
        public class DataMappingType
        {
            public static string ByName = "ByName";
            public static string ByIndex = "ByIndex";
        }

        public class DataRepeatDeal
        {
            public static string Update = "Update";
            public static string Ignore = "Ignore";
            public static string Cancel = "Cancel";
        }

        public class OnErrorDeal
        {
            public static string Ignore = "Ignore";
            public static string Cancel = "Cancel";
        }

        public string ImportDataMapping = DataMappingType.ByIndex;

        private IDomainDataProvider dataProvider = null;
        private List<ConfigObject> listConfigObj = null;
        private MatchType matchType = null;
        private string userCode = "";
        private DBDateTime dbDateTime = null;

        public ImportDataEngine(IDomainDataProvider _dataProvider, List<ConfigObject> _listConfigObject, MatchType _matchType, string _userCode, DBDateTime _dbDateTime)
        {
            dataProvider = _dataProvider;
            listConfigObj = _listConfigObject;
            matchType = _matchType;
            userCode = _userCode.Trim().ToUpper();
            dbDateTime = _dbDateTime;
        }

        public string UpdateRepeatData = DataRepeatDeal.Update;   // 已存在数据的处理方式
        private bool RowDataExist = false;      // 标示记录是否已存在，在CheckUniqueGroup中赋值

        /// <summary>
        /// 导入一行数据，前提：rowData已经是真实数据(不是空行)，结果：将数据插入数据库，由外部调用者进行事务控制
        /// </summary>
        /// <param name="importType"></param>
        /// <param name="rowHeader"></param>
        /// <param name="rowData"></param>
        /// <returns></returns>
        public string ImportRow(string importType, string[] rowHeader, string[] rowData)
        {
            if (listConfigObj == null)
                return "$CS_CONFIG_NOT_EXIST";
            ConfigObject cfgObj = null;
            for (int i = 0; i < listConfigObj.Count; i++)
            {
                if (listConfigObj[i].Name == importType)
                {
                    cfgObj = listConfigObj[i];
                    break;
                }
            }
            if (cfgObj == null)
            {
                return "$CS_CONFIG_NOT_EXIST";
            }

            try
            {
                object objIns = CreateObjectInstance(cfgObj);
                AssignData(objIns, cfgObj, rowHeader, rowData);
                CheckEntityTotal(ref objIns, cfgObj);
                InsertData(objIns, cfgObj, rowHeader, rowData);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }

        /// <summary>
        /// 建立实体对象
        /// </summary>
        /// <returns></returns>
        private object CreateObjectInstance(ConfigObject cfgObj)
        {
            System.Type type = GetTypeFromDomain(cfgObj.Type);
            object objIns = DomainObjectUtility.CreateTypeInstance(type);
            // 对配置的默认字段赋值
            if (cfgObj.DefaultFieldList != null)
            {
                for (int i = 0; i < cfgObj.DefaultFieldList.Count; i++)
                {
                    object strValue = null;
                    if (cfgObj.DefaultFieldList[i].DefaultValue != "")
                        strValue = cfgObj.DefaultFieldList[i].DefaultValue;
                    string strFieldName = cfgObj.DefaultFieldList[i].Name;
                    string strDataFrom = cfgObj.DefaultFieldList[i].DataFrom;
                    if (strDataFrom == FieldDataFrom.UserCode)
                        strValue = userCode;
                    else if (strDataFrom == FieldDataFrom.Date)
                        strValue = dbDateTime.DBDate;
                    else if (strDataFrom == FieldDataFrom.Time)
                        strValue = dbDateTime.DBTime;
                    else if (strDataFrom == FieldDataFrom.GUID)
                        strValue = System.Guid.NewGuid().ToString();
                    if (strValue != null)
                    {
                        DomainObjectUtility.SetValue(objIns, strFieldName, strValue);
                    }
                }
            }
            // 对默认的三个字段赋值
            if (DomainObjectUtility.GetFieldName(type, "MaintainUser") != null)
                DomainObjectUtility.SetValue(objIns, "MaintainUser", userCode);
            if (DomainObjectUtility.GetFieldName(type, "MaintainDate") != null)
                DomainObjectUtility.SetValue(objIns, "MaintainDate", dbDateTime.DBDate);
            if (DomainObjectUtility.GetFieldName(type, "MaintainTime") != null)
                DomainObjectUtility.SetValue(objIns, "MaintainTime", dbDateTime.DBTime);

            return objIns;
        }

        /// <summary>
        /// 对实体赋值
        /// </summary>
        private void AssignData(object objIns, ConfigObject cfgObj, string[] rowHeader, string[] rowData)
        {
            for (int i = 0; i < cfgObj.FieldList.Count; i++)
            {
                ConfigField cfgFld = cfgObj.FieldList[i];
                string strHeader = cfgFld.Text;
                string strValue = "";
                GetMappingFieldValue(cfgObj, cfgFld, objIns, rowHeader, rowData, i, ref strHeader, ref strValue);

                if (cfgFld.AllowNull == false && strValue == "")
                {
                    throw new Exception("$Error_Input_Empty [" + strHeader + "]");
                }
                CheckData(objIns, cfgFld, strValue, strHeader);      // 检查数据格式

                if (cfgFld.IncludeItem == true)
                {
                    if (objIns is BenQGuru.eMES.Domain.BaseSetting.User
                        &&
                        cfgFld.Name == "UserPassword")
                    {
                        strValue = BenQGuru.eMES.Common.Helper.EncryptionHelper.MD5Encryption(strValue);
                    }

                    //2#条码流水号做特殊处理
                    if (objIns is BenQGuru.eMES.Domain.MOModel.LabelRCard2Seq)
                    {
                        if(cfgFld.Name == "ModelCode")
                        {
                            if(strValue.Length >=1)
                            {
                                DomainObjectUtility.SetValue(objIns, "Year", strValue.Substring(0,1));
                            }

                            if (strValue.Length >= 4)
                                strValue = strValue.Substring(1, 3);
              
                        }
                        else if (cfgFld.Name == "Year")
                        {
                            continue;
                        }
                    }
                    DomainObjectUtility.SetValue(objIns, cfgFld.Name, strValue);
                }
            }
        }
        private void AssignDataSubItem(object objIns, ConfigObject cfgObj, string[] rowHeader, string[] rowData, object parentObj, ConfigObject parentCfgObj)
        {
            for (int i = 0; i < cfgObj.FieldList.Count; i++)
            {
                ConfigField cfgFld = cfgObj.FieldList[i];
                string strValue = "";
                string strHeader = "";
                if (cfgFld.DataFrom == FieldDataFrom.ParentItem)
                {
                    if (DomainObjectUtility.GetFieldName(parentObj.GetType(), cfgFld.ParentNodeField) != null)
                    {
                        object objValueTmp = DomainObjectUtility.GetValue(parentObj, cfgFld.ParentNodeField, null);
                        if (objValueTmp != null)
                            strValue = objValueTmp.ToString();
                        else
                            strValue = "";
                    }
                    strHeader = cfgFld.Text;
                }
                else if (cfgFld.DataFrom == FieldDataFrom.ParentNode)
                {
                    ConfigField cfgParentFld = null;
                    for (int n = 0; n < parentCfgObj.FieldList.Count; n++)
                    {
                        if (parentCfgObj.FieldList[n].Name == cfgFld.ParentNodeField)
                        {
                            cfgParentFld = parentCfgObj.FieldList[n];
                            break;
                        }
                    }
                    if (cfgParentFld != null)
                    {
                        int dataIndex = -1;
                        if (ImportDataMapping == DataMappingType.ByIndex)
                        {
                            dataIndex = parentCfgObj.FieldList.IndexOf(cfgParentFld);
                        }
                        else
                        {
                            for (int n = 0; n < rowHeader.Length; n++)
                            {
                                if (rowHeader[n] == cfgFld.Text ||
                                    UserControl.MutiLanguages.ParserMessage(cfgFld.Name) == rowHeader[n])
                                {
                                    dataIndex = n;
                                    break;
                                }
                            }
                        }
                        if (dataIndex == -1)
                            return;
                        strValue = rowData[dataIndex].Trim().ToUpper();
                        if (cfgFld.MatchType != "")
                            strValue = matchType.GetValue(cfgFld.MatchType, strValue);
                        strHeader = rowHeader[dataIndex];
                    }
                }

                if (strValue == "" && cfgFld.DefaultValue != "")
                    strValue = cfgFld.DefaultValue;
                if (cfgFld.AllowNull == false && strValue == "")
                {
                    throw new Exception("$Error_Input_Empty [" + strHeader + "]");
                }
                if (strValue == "" && cfgFld.DataFrom != null)
                {
                    strValue = cfgFld.DataFrom.ToString();
                }
                CheckData(objIns, cfgFld, strValue, strHeader);      // 检查数据格式

                if (cfgFld.IncludeItem == true)
                {
                    DomainObjectUtility.SetValue(objIns, cfgFld.Name, strValue);
                }
            }
        }
        private void GetMappingFieldValue(ConfigObject cfgObj, ConfigField cfgFld, object objIns, string[] rowHeader, string[] rowData, int fieldIndex, ref string strHeader, ref string strValue)
        {
            strHeader = cfgFld.Text;
            if (cfgFld.DataFrom == FieldDataFrom.Query)
            {
                string strQuerySql = cfgFld.DataQuerySql;

                string[] strParamFieldList = cfgFld.DataQueryParam.Split(',');
                List<string> paramList = new List<string>();
                for (int i = 0; i < strParamFieldList.Length; i++)
                {
                    if (strParamFieldList[i].Trim() != "")
                        paramList.Add(strParamFieldList[i].Trim());
                }
                if (paramList.Count > 0)
                {
                    object[] objsParam = new object[paramList.Count];
                    for (int i = 0; i < paramList.Count; i++)
                    {
                        //objsParam[i] = DomainObjectUtility.GetValue(objIns, paramList[i], null);
                        int iParamFieldIndex = -1;
                        for (int n = 0; n < cfgObj.FieldList.Count; n++)
                        {
                            if (cfgObj.FieldList[n].Name == paramList[i])
                            {
                                iParamFieldIndex = n;
                                break;
                            }
                        }
                        int dataIndex = -1;
                        if (ImportDataMapping == DataMappingType.ByIndex)
                        {
                            if (iParamFieldIndex < rowHeader.Length)
                                dataIndex = iParamFieldIndex;
                        }
                        else
                        {
                            for (int n = 0; n < rowHeader.Length; n++)
                            {
                                if (rowHeader[n] == cfgObj.FieldList[iParamFieldIndex].Text ||
                                    UserControl.MutiLanguages.ParserMessage(cfgObj.FieldList[iParamFieldIndex - 1].Name) == rowHeader[n])
                                {
                                    dataIndex = n;
                                    break;
                                }
                            }
                        }
                        if (dataIndex != -1)
                            objsParam[i] = rowData[dataIndex].Trim().ToUpper();
                    }
                    strQuerySql = string.Format(strQuerySql, objsParam);
                }
                DataSet ds = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.dataProvider).PersistBroker.Query(strQuerySql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0] != System.DBNull.Value)
                        strValue = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            else if (cfgFld.DataFrom == "")
            {
                int dataIndex = -1;
                if (ImportDataMapping == DataMappingType.ByIndex)
                {
                    if (fieldIndex < rowHeader.Length)
                        dataIndex = fieldIndex;
                }
                else
                {
                    for (int n = 0; n < rowHeader.Length; n++)
                    {
                        if (rowHeader[n] == cfgFld.Text ||
                            UserControl.MutiLanguages.ParserMessage(cfgFld.Name) == rowHeader[n])
                        {
                            dataIndex = n;
                            break;
                        }
                    }
                }
                if (dataIndex == -1)
                    return;
                strValue = rowData[dataIndex].Trim().ToUpper();
                strHeader = rowHeader[dataIndex];
            }
            if (cfgFld.MatchType != "")
                strValue = matchType.GetValue(cfgFld.MatchType, strValue);

            if (strValue == "" && cfgFld.DefaultValue != "")
                strValue = cfgFld.DefaultValue;
        }

        /// <summary>
        /// 插入数据库
        /// </summary>
        private void InsertData(object objIns, ConfigObject cfgObj, string[] rowHeader, string[] rowData)
        {
            // 插入主记录
            if (this.RowDataExist == true)  // 数据已存在
            {
                if (this.UpdateRepeatData == DataRepeatDeal.Update)     // 更新
                    if (objIns is BenQGuru.eMES.Domain.Package.PKRuleStep)//包装规则做特殊检查 joe song 20080220
                    {
                        BenQGuru.eMES.Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(this.dataProvider);
                        pf.UpdatePKRuleStep(objIns as BenQGuru.eMES.Domain.Package.PKRuleStep);
                    }
                    else
                        this.dataProvider.Update(objIns);

                else if (this.UpdateRepeatData == DataRepeatDeal.Ignore)    // 忽略
                    throw new Exception("$Import_Unique_Data_Exist");
                else if (this.UpdateRepeatData == DataRepeatDeal.Cancel)    // 终止
                    throw new Exception("$DATA_REPEAT_CANCEL");
            }
            else    // 数据不存在，则Insert
                if (objIns is BenQGuru.eMES.Domain.Package.PKRuleStep) //包装规则做特殊检查 joe song 20080220
                {
                    BenQGuru.eMES.Package.PackageFacade pf = new BenQGuru.eMES.Package.PackageFacade(this.dataProvider);
                    pf.AddPKRuleStep(objIns as BenQGuru.eMES.Domain.Package.PKRuleStep);
                }
                else
                    this.dataProvider.Insert(objIns);

            // 插入外部记录
            for (int i = 0; i < cfgObj.FieldList.Count; i++)
            {
                if (cfgObj.FieldList[i].ForeignItem == true)
                {
                    if (cfgObj.FieldList[i].IgnoreForeignWhenNull == true)      // 数据为空则忽略子项
                    {
                        int dataIndex = -1;
                        if (ImportDataMapping == DataMappingType.ByIndex)
                        {
                            if (i < rowHeader.Length)
                                dataIndex = i;
                        }
                        else
                        {
                            for (int n = 0; n < rowHeader.Length; n++)
                            {
                                if (rowHeader[n] == cfgObj.FieldList[i].Text ||
                                    UserControl.MutiLanguages.ParserMessage(cfgObj.FieldList[i].Name) == rowHeader[n])
                                {
                                    dataIndex = n;
                                    break;
                                }
                            }
                        }
                        if (dataIndex == -1)
                            return;
                        string strValue = rowData[dataIndex].Trim().ToUpper();
                        if (strValue == "")
                            break;
                    }

                    ConfigObject foreignCfgObj = cfgObj.FieldList[i].ForeignObject;
                    object objForeignIns = CreateObjectInstance(foreignCfgObj);
                    AssignDataSubItem(objForeignIns, foreignCfgObj, rowHeader, rowData, objIns, cfgObj);
                    CheckEntityTotal(ref objForeignIns, foreignCfgObj);

                    if (this.RowDataExist == true)  // 数据已存在
                    {
                        if (this.UpdateRepeatData == DataRepeatDeal.Update)     // 更新
                        {
                            //针对用户默认组织
                            if (foreignCfgObj.Type == typeof(BenQGuru.eMES.Domain.BaseSetting.User2Org).ToString())
                                UpdateUser2Org(objForeignIns);
                            else 
                                this.dataProvider.Update(objForeignIns);
                        }
                        else if (this.UpdateRepeatData == DataRepeatDeal.Ignore)    // 忽略
                            return;
                        else if (this.UpdateRepeatData == DataRepeatDeal.Cancel)    // 终止
                            throw new Exception("DATA_REPEAT");
                    }
                    else    // 数据不存在，则Insert
                        this.dataProvider.Insert(objForeignIns);
                }
            }
        }

        private void UpdateUser2Org(object user2OrgToUpdate)
        {
            BaseSetting.UserFacade userFacade = new BaseSetting.UserFacade(this.dataProvider);
            object[] user2OrgList = userFacade.GetUser2OrgList(((BenQGuru.eMES.Domain.BaseSetting.User2Org)user2OrgToUpdate).UserCode);
            if (user2OrgList != null)
            {
                foreach (BenQGuru.eMES.Domain.BaseSetting.User2Org user2Org in user2OrgList)
                {
                    user2Org.IsDefaultOrg = 0;
                    this.dataProvider.Update(user2Org);
                }
            }
            this.dataProvider.Update(user2OrgToUpdate);
       }

        /// <summary>
        /// 检查数据值
        /// </summary>
        /// <returns></returns>
        private void CheckData(object objIns, ConfigField cfgFld, string value, string headerText)
        {
            if (cfgFld.CheckList == null || cfgFld.CheckList.Count == 0)
                return;
            for (int i = 0; i < cfgFld.CheckList.Count; i++)
            {
                ConfigCheck chk = cfgFld.CheckList[i];
                if (chk.Type == FieldCheckType.Length)
                {
                    string strExp = chk.LengthExpression;
                    if (strExp.IndexOf("{0}") >= 0)
                        strExp = strExp.Replace("{0}", value.Length.ToString());
                    else
                        strExp = value.Length.ToString() + strExp;
                    object objResult = Microsoft.JScript.Eval.JScriptEvaluate(strExp, Microsoft.JScript.Vsa.VsaEngine.CreateEngine());
                    if (Convert.ToBoolean(objResult) == false)
                        throw new Exception("$Import_Length_Error [" + headerText + ": " + value + "]");
                }
                else if (chk.Type == FieldCheckType.DataRange)
                {
                    CheckDataRange(objIns, cfgFld, chk, value, headerText);
                }
                else if (chk.Type == FieldCheckType.DataType)
                {
                    CheckDataType(cfgFld, chk, value, headerText);
                }
                else if (chk.Type == FieldCheckType.Exist)
                {
                    if(value != null && value != string.Empty)
                        CheckDataExist(cfgFld, chk, value, headerText);
                }
            }
        }


        private void CheckDataRange(object objIns, ConfigField cfgFld, ConfigCheck chk, string value, string headerText)
        {
            string strRngExp = chk.DataRangeExpression;
            if (strRngExp.IndexOf("{0}") >= 0)
                strRngExp = strRngExp.Replace("{0}", value);
            else
                strRngExp = value + strRngExp;
            int iIdxFrom = 0;
            while (true)
            {
                int iFrom = strRngExp.IndexOf("{", iIdxFrom);
                if (iFrom < 0)
                    break;
                int iTo = strRngExp.IndexOf("}", iFrom);
                if (iTo < 0)
                    break;
                string strFieldName = strRngExp.Substring(iFrom + 1, iTo - iFrom - 1);
                if (DomainObjectUtility.GetFieldName(objIns.GetType(), strFieldName) != null)
                {
                    string strRepVal = DomainObjectUtility.GetValue(objIns, strFieldName, null).ToString();
                    strRngExp = strRngExp.Substring(0, iFrom) + strRepVal + strRngExp.Substring(iTo + 1);
                    iIdxFrom = iFrom;
                }
                else
                {
                    iIdxFrom = iTo;
                }
            }
            string strExp = strRngExp;
            object objResult = Microsoft.JScript.Eval.JScriptEvaluate(strExp, Microsoft.JScript.Vsa.VsaEngine.CreateEngine());
            if (Convert.ToBoolean(objResult) == false)
                throw new Exception("$Import_DataRange_Error [" + headerText + ": " + value + "]");
        }
        private void CheckDataType(ConfigField cfgFld, ConfigCheck chk, string value, string headerText)
        {
            if (chk.CheckDataType == "integer")
            {
                try
                {
                    if ((Convert.ToDecimal(value) * 10) / 10 != Convert.ToInt32(value))
                    {
                        throw new Exception("$Import_CheckDataType_Integer_Error [" + headerText + ": " + value + "]");
                    }
                }
                catch
                {
                    throw new Exception("$Import_CheckDataType_Integer_Error [" + headerText + ": " + value + "]");
                }
            }
            else if (chk.CheckDataType == "numeric")
            {
                try
                {
                    decimal d = Convert.ToDecimal(value);
                }
                catch
                {
                    throw new Exception("$Import_DataFormat_Error [" + headerText + ": " + value + "]");
                }
            }
        }
        private void CheckDataExist(ConfigField cfgFld, ConfigCheck chk, string value, string headerText)
        {
            if (chk.ParentObjectType != "" && chk.ParentObjectField != "")
            {
                System.Type type = GetTypeFromDomain(chk.ParentObjectType);
                object[] objsTmp = this.dataProvider.CustomSearch(type, new string[] { chk.ParentObjectField }, new object[] { value });
                if (objsTmp == null || objsTmp.Length == 0)
                {
                    throw new Exception("$Data_Not_Exist [" + headerText + ": " + value + "]");
                }
            }
            else if (chk.ExistCheckSql != "")
            {
                string strSql = string.Format(chk.ExistCheckSql, value);
                if (this.dataProvider.GetCount(new SQLCondition(strSql)) <= 0)
                {
                    throw new Exception("$Data_Not_Exist [" + headerText + ": " + value + "]");
                }
            }
        }

        /// <summary>
        /// 检查整体数据
        /// </summary>
        /// <param name="objIns"></param>
        /// <param name="cfgObj"></param>
        private void CheckEntityTotal(ref object objIns, ConfigObject cfgObj)
        {
            CheckAllowNullField(objIns, cfgObj);
            CheckUniqueGroup(ref objIns, cfgObj);
        }
        /// <summary>
        /// 检查不为空的数据
        /// </summary>
        private void CheckAllowNullField(object objIns, ConfigObject cfgObj)
        {
            for (int i = 0; i < cfgObj.FieldList.Count; i++)
            {
                if (cfgObj.FieldList[i].AllowNull == false)
                {
                    if (DomainObjectUtility.GetFieldName(objIns.GetType(), cfgObj.FieldList[i].Name) != null)
                    {
                        object objVal = DomainObjectUtility.GetValue(objIns, cfgObj.FieldList[i].Name, null);
                        if (objVal == null || objVal.ToString() == "")
                            throw new Exception("$Error_Input_Empty [" + cfgObj.FieldList[i].Text + "]");
                    }
                }
            }
        }
        /// <summary>
        /// 检查唯一键
        /// </summary>
        /// <returns></returns>
        private void CheckUniqueGroup(ref object objIns, ConfigObject cfgObj)
        {
            RowDataExist = false;
            Dictionary<string, List<string>> listUniqueGroup = new Dictionary<string, List<string>>();
            for (int i = 0; i < cfgObj.FieldList.Count; i++)
            {
                if (cfgObj.FieldList[i].UniqueGroup != "")
                {
                    List<string> listGroup = null;
                    if (listUniqueGroup.ContainsKey(cfgObj.FieldList[i].UniqueGroup) == true)
                        listGroup = listUniqueGroup[cfgObj.FieldList[i].UniqueGroup];
                    else
                    {
                        listGroup = new List<string>();
                        listUniqueGroup.Add(cfgObj.FieldList[i].UniqueGroup, listGroup);
                    }
                    listGroup.Add(cfgObj.FieldList[i].Name);
                }
            }
            foreach (List<string> listGroup in listUniqueGroup.Values)
            {
                string[] strFieldName = new string[listGroup.Count];
                listGroup.CopyTo(strFieldName);
                object[] objFieldValue = new object[strFieldName.Length];
                for (int i = 0; i < strFieldName.Length; i++)
                {
                    objFieldValue[i] = DomainObjectUtility.GetValue(objIns, strFieldName[i], null);
                }
                object[] objsTmp = this.dataProvider.CustomSearch(objIns.GetType(), strFieldName, objFieldValue);
                if (objsTmp != null && objsTmp.Length > 0)
                {
                    if (this.UpdateRepeatData == DataRepeatDeal.Update)  // 如果允许更新记录，则不用报错
                    {
                        object objDB = objsTmp[0];

                        this.RowDataExist = true;
                        //用Excel中的数据更新数据库中的数据
                        for (int i = 0; i < cfgObj.FieldList.Count; i++)
                        {
                            ConfigField cfgFld = cfgObj.FieldList[i];

                            if (cfgFld.IncludeItem == true)
                            {
                                object v = DomainObjectUtility.GetValue(objIns, cfgFld.Name, null);

                                //产品的ROHS字段为空时不更新
                                if (objIns is BenQGuru.eMES.Domain.MOModel.Item && cfgFld.Name == "ROHS" && v.ToString().Trim() == string.Empty)
                                    continue;

                                //产品的ROHS字段为空时不更新
                                if (objIns is BenQGuru.eMES.Domain.MOModel.Item && v != null && v.ToString().Trim() == string.Empty)
                                    continue;

                                if (objIns is BenQGuru.eMES.Domain.MOModel.Item && v != null 
                                    && 
                                    (v is int || v is decimal || v is double)
                                    &&
                                    v.ToString().Trim() == "0")
                                    continue;

                                if (objIns is BenQGuru.eMES.Domain.MOModel.ItemRoute2OP && cfgFld.Name == "OPControl")
                                {
                                    int seq = (int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading;
                                    string con = v.ToString();

                                    string v_old = DomainObjectUtility.GetValue(objDB, cfgFld.Name, null).ToString();

                                    System.Text.StringBuilder sb = new StringBuilder(v_old);
                                    sb[seq] = con[0];

                                    v = sb.ToString();
                                 
                                }
                                DomainObjectUtility.SetValue(objDB, cfgFld.Name, v);
                            }
                        }
                        objIns = objDB;
                    }
                    else if (this.UpdateRepeatData == DataRepeatDeal.Ignore)
                        throw new Exception("$Import_Unique_Data_Exist");
                    else
                        throw new Exception("$DATA_REPEAT_CANCEL");
                    
                }
            }
        }

        private Dictionary<string, System.Reflection.Assembly> loadedAssembly = null;
        private Type GetTypeFromDomain(string typeFullName)
        {
            string strFile = System.Reflection.Assembly.GetExecutingAssembly().Location;
            strFile = strFile.Substring(0, strFile.LastIndexOf("\\") + 1) + "BenQGuru.eMES.Domain.dll";
            return GetTypeFromDomain(typeFullName, strFile);
        }
        private Type GetTypeFromDomain(string typeFullName, string dllFileName)
        {
            if (loadedAssembly == null)
                loadedAssembly = new Dictionary<string, System.Reflection.Assembly>();
            System.Reflection.Assembly assembly = null;
            if (loadedAssembly.ContainsKey(dllFileName) == true)
                assembly = loadedAssembly[dllFileName];
            else
            {
                assembly = System.Reflection.Assembly.LoadFrom(dllFileName);
                loadedAssembly.Add(dllFileName, assembly);
            }
            return assembly.GetType(typeFullName, false, true);
        }

    }
}
