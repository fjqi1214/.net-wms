#region System
using System;
using System.Collections;
using System.Runtime.Remoting;
#endregion

#region project
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.LotDataCollect;
#endregion


/// ItemLotFacade 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2012 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Jarvis Chen
/// 创建日期:2012/03/08
/// 修改人:
/// 修改日期:
/// 描 述: 
/// 版 本:	
/// </summary>

namespace BenQGuru.eMES.MOModel
{
    public class ItemLotFacade : MarshalByRefObject
    {
        //private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(ItemFacade));
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public ItemLotFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        public ItemLotFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        protected IDomainDataProvider DataProvider
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

        //add by Jarvis
        #region Item2LotCheck
        /// <summary>
        /// 
        /// </summary>
        public Item2LotCheck CreateNewItem2LotCheck()
        {
            return new Item2LotCheck();
        }

        public void AddItem2LotCheck(Item2LotCheck item2LotCheck)
        {
            this._helper.AddDomainObject(item2LotCheck);
        }

        public void UpdateItem2LotCheck(Item2LotCheck item2LotCheck)
        {
            this._helper.UpdateDomainObject(item2LotCheck);
        }

        public void DeleteItem2LotCheck(Item2LotCheck item2LotCheck)
        {
            this._helper.DeleteDomainObject(item2LotCheck);
        }

        public void DeleteItem2LotCheck(Item2LotCheck[] item2LotCheck)
        {
            this._helper.DeleteDomainObject(item2LotCheck);
        }

        public object GetItem2LotCheck(string itemCode)
        {
            return this.DataProvider.CustomSearch(typeof(Item2LotCheck), new object[] { itemCode });
        }

        public object[] QueryItem2LotCheck(string itemCode)
        {
            string sql = string.Format("SELECT * FROM tblitem2LotCheck WHERE itemCode='{0}'", itemCode);
            return this.DataProvider.CustomQuery(typeof(Item2LotCheck), new SQLCondition(sql));
        }

        //根据序列号长度和序列号前缀获取itemCode
        public object[] GetItemCodeForLotCheck(string snPrefix, string snLength)
        {
            string sql = string.Format("SELECT DISTINCT itemCode AS ItemCode FROM tblitem2LotCheck WHERE snprefix='{0}' AND snlength='{1}'", snPrefix, snLength);
            return this.DataProvider.CustomQuery(typeof(Item2LotCheck), new SQLCondition(sql));
        }

        private string GenerateQueryItem2LotCheckSQL(string itemCode, string itemDesc, string itemType, string exportImport, string snPrefix, string snLength, bool ifForCount)
        {
            string sql = "";

            if (ifForCount)
            {
                sql = "SELECT COUNT(*) FROM tblitem2LotCheck t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode";
            }
            else
            {
                sql = "SELECT t1.itemcode AS ItemCode, t1.snprefix AS SNPrefix, t1.snlength AS SNLength,T1.CREATETYPE AS CreateType, t1.MUSER AS MUser, t1.MDATE AS MDate, t1.MTIME AS MTime, t2.mdesc AS ItemDesc FROM tblitem2LotCheck t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode";
            }

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} AND t1.itemCode like '%{1}%' ", sql, itemCode);
            }

            if ((itemType != null && itemType.Length != 0) ||
                (exportImport != null && exportImport.Length != 0) ||
                (itemDesc != null && itemDesc.Length != 0))
            {
                string condition = "";
                if ((itemType != null && itemType.Length != 0))
                {
                    condition += " AND mtype='" + itemType + "' ";
                }
                if ((exportImport != null && exportImport.Length != 0))
                {
                    condition += " AND mexportimport='" + exportImport + "' ";
                }
                if ((itemDesc != null && itemDesc.Length != 0))
                {
                    condition += " AND UPPER(mdesc) LIKE '%" + itemDesc.ToUpper() + "%' ";
                }
                sql = string.Format("{0} AND t1.itemcode IN (SELECT DISTINCT mcode FROM tblmaterial WHERE 1=1 {1})", sql, condition);
            }

            if (snPrefix != null && snPrefix.Length != 0)
            {
                sql = string.Format("{0} AND UPPER(t1.snprefix) LIKE '%{1}%'", sql, snPrefix.ToUpper());
            }

            if (snLength != null && snLength.Length != 0)
            {
                int snLen = int.Parse(snLength);
                if (snLen >= 0)
                {
                    sql = string.Format("{0} AND t1.snlength={1}", sql, snLen);
                }
            }


            return sql;
        }

        /// <summary>
        /// ** 功能描述:	分页查询Item2LotCheck
        /// ** 作 者:		Created by Jarvis Chen
        /// ** 日 期:		2012-03-05
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Item2LotCheck数组</returns>
        public object[] QueryItem2LotCheck(string itemCode, string itemDesc, string itemType, string exportImport, string snPrefix, string snLength, int inclusive, int exclusive)
        {
            string sql = this.GenerateQueryItem2LotCheckSQL(itemCode, itemDesc, itemType, exportImport, snPrefix, snLength, false);

            return this.DataProvider.CustomQuery(typeof(Item2LotCheckMP), new PagerCondition(sql, "ItemCode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	查询Item2LotCheck的总行数
        /// ** 作 者:		Created by Jarvis Chen
        /// ** 日 期:		2012-03-05
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">ItemCode，模糊查询</param>
        /// <returns> Item2LotCheck的总记录数</returns>
        public int QueryItem2LotCheckCount(string itemCode, string itemDesc, string itemType, string exportImport, string snPrefix, string snLength)
        {
            string sql = this.GenerateQueryItem2LotCheckSQL(itemCode, itemDesc, itemType, exportImport, snPrefix, snLength, true);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion
       
    }

    public class MOLotFacade : MarshalByRefObject
    {
        //private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(MOFacade));
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public const string ISMAINROUTE_TRUE = "1";

        public MOLotFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        //Laws Lu,max life time to unlimited
        public override object InitializeLifetimeService()
        {
            return null;
        }


        public MO CreateNewMO()
        {
            MO mo = new MO();
            mo.MOStatus = MOManufactureStatus.MOSTATUS_INITIAL;
            return mo;
        }
        //Laws Lu,2006/07/19 recover the default construction function
        public MOLotFacade()
        {
        }

        protected IDomainDataProvider DataProvider
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
        private string[] canModifyStatus = new string[] { MOManufactureStatus.MOSTATUS_INITIAL, MOManufactureStatus.MOSTATUS_PENDING };

        //add by Jarvis
        #region MO2LotLink
        /// <summary>
        /// 
        /// </summary>
        public MO2LotLink CreateNewMO2LotLink()
        {
            return new MO2LotLink();
        }

        public void AddMO2LotLink(MO2LotLink mo2RCardLink)
        {
            this._helper.AddDomainObject(mo2RCardLink);
        }

        public void UpdateMO2LotLink(MO2LotLink mo2RCardLink)
        {
            this._helper.UpdateDomainObject(mo2RCardLink);
        }

        public void DeleteMO2LotLink(MO2LotLink mo2RCardLink)
        {
            this._helper.DeleteDomainObject(mo2RCardLink);
        }

        public void DeleteMO2LotLink(MO2LotLink[] mo2RCardLink)
        {
            this._helper.DeleteDomainObject(mo2RCardLink);
        }

        public object GetMO2LotLink(string lotNo, string moCode)
        {
            return this.DataProvider.CustomSearch(typeof(MO2LotLink), new object[] { lotNo,moCode });
        }

        public object[] GetMO2LotLink(string lotNo)
        {
            string sqlQueryString = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2LotLink)) + " from TBLMO2LOTLINK where  LOTNO='" + lotNo + "' ";

            return this.DataProvider.CustomQuery(typeof(MO2LotLink), new SQLCondition(sqlQueryString));
        }

        public object[] GetMO2LotLinkByMoCode(string moCode)
        {
            object[] obj = null;
            string sqlQueryString = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2LotLink)) + " from TBLMO2LOTLINK where  moCode='" + moCode + "' ";

            sqlQueryString += " order by LOTNO";
            obj = this.DataProvider.CustomQuery(typeof(MO2LotLink), new SQLCondition(sqlQueryString));

            return obj;
        }

        public object GetMO2LotLinkByLotNo(string lotNo, string moCode)
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(MO2LotLink), new SQLCondition("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2LotLink))
                + " from TBLMO2LOTLINK where LOTNO='" + lotNo + "' and mocode='" + moCode + "'"));

            object objReturn = null;

            if (obj != null && obj.Length > 0)
            {
                objReturn = obj[0];
            }

            return objReturn;
        }

        public object[] QueryMO2LotLink(string lotNo, string moCode)
        {

            string sql = "SELECT * FROM TBLMO2LOTLINK WHERE 1=1 ";
            if (lotNo != "")
            {
                sql += " AND LOTNO like '%" + FormatHelper.CleanString(lotNo) + "%'";
            }
            if (moCode != "")
            {
                sql += " AND mocode='" + FormatHelper.CleanString(moCode).ToUpper() + "'";
            }

            sql += " ORDER BY LOTNO ";

            object[] obj = this.DataProvider.CustomQuery(typeof(MO2LotLink), new SQLCondition(sql));

            return obj;

        }

        public object[] GetMO2LotLinkByMoCode(string moCode, string beginLotNo, string endLotNo, string printTimes)
        {
            object[] obj = null;
            string sqlQueryString = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2LotLink)) + " from TBLMO2LOTLINK where  moCode='" + moCode + "' ";

            if (beginLotNo != "")
            {
                sqlQueryString += " and LOTNO >= '" + beginLotNo + "'";
            }
            if (endLotNo != "")
            {
                sqlQueryString += " and LOTNO <= '" + endLotNo + "'";
            }
            if (printTimes != "")
            {
                sqlQueryString += " and PrintTimes = '" + printTimes + "' ";
            }

            sqlQueryString += " order by LOTNO";
            obj = this.DataProvider.CustomQuery(typeof(MO2LotLink), new SQLCondition(sqlQueryString));

            return obj;
        }

        //根据工单汇总批内所含的数量 20120321
        public decimal SumLotQtyByMoCode(string moCode)
        {
            object[] obj = null;
            string sqlQueryString = "select nvl(sum(lotqty),0) as lotqty from TBLMO2LOTLINK where lotstatus in ('NEW','USE') AND moCode='" + moCode + "' ";            
            obj = this.DataProvider.CustomQuery(typeof(MO2LotLink), new SQLCondition(sqlQueryString));

            return ((MO2LotLink)obj[0]).LotQty;
        }
        
        #endregion

        #region MO2LotAndLotSimulation
        public object[] QueryMO2LotForQuery(string moCode, string itemCode, string lotNo, bool isOnlyValid)
        {
            string sql = string.Empty;
            sql += " SELECT a.MOCODE,c.ITEMCODE,a.LOTNO AS LOTCODE,a.LOTQTY,b.GOODQTY,b.NGQTY,a.LOTSTATUS,b.STATUS AS PRODUCTSTATUS,b.ROUTECODE,b.OPCODE,b.COLLECTSTATUS,b.ISCOM,a.MDATE,a.MTIME";
            sql += "  FROM TBLMO2LOTLINK a";
            sql += "  LEFT JOIN TBLLOTSIMULATIONREPORT b ON a.MOCODE = b.MOCODE";
            sql += "                              AND a.LOTNO = b.LOTCODE";
            sql += "  LEFT JOIN TBLMO c ON a.MOCODE = c.MOCODE";
            sql += " WHERE 1 = 1";

            if (moCode != "")
            {
                sql += " AND a.MOCODE = '" + FormatHelper.CleanString(moCode) + "'";
            }
            if (itemCode != "")
            {
                if (itemCode.Contains(","))
                {
                    sql += " and c.ITEMCODE in (";
                    string[] itemCodes = itemCode.Split(',');
                    foreach (string str in itemCodes)
                    {
                        sql += "'" + FormatHelper.CleanString(str) + "',";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")";
                }
                else
                {
                    sql += " and c.ITEMCODE in ('" + FormatHelper.CleanString(itemCode) + "')";
                }
            }
            if (lotNo != "")
            {
                sql += " AND a.LOTNO = '" + FormatHelper.CleanString(lotNo) + "'";
            }
            if (isOnlyValid)
            {
                sql += " AND a.LOTSTATUS <> '" + LotStatusForMO2LotLink.LOTSTATUS_STOP+ "'";
            }
            sql += " ORDER BY a.MOCODE,c.ITEMCODE,b.ROUTECODE,b.OPCODE,a.LOTNO";

            return this.DataProvider.CustomQuery(typeof(LotSimulationForQuery), new SQLCondition(sql));

        }

        public string GenerateNewLotNo(string moCode, string itemCode, int nextSeq) 
        {
            string sql = string.Empty;
            sql = " SELECT NVL(MAX(LOTNO),'') AS LOTNO FROM TBLMO2LOTLINK WHERE MOCODE = '" + moCode + "'";
            object[] mo2LotLink = this.DataProvider.CustomQuery(typeof(MO2LotLink), new SQLCondition(sql));
            if (mo2LotLink != null)
            {
                ItemLotFacade itemLotFacade = new ItemLotFacade(this._domainDataProvider);
                object item2LotCheck = itemLotFacade.GetItem2LotCheck(itemCode);
                if (item2LotCheck != null)
                {
                    NumberScale scale = NumberScale.Scale34;
                    if (((Item2LotCheck)item2LotCheck).CreateType == CreateType.CREATETYPE_DECIMAL)
                        scale = NumberScale.Scale10;
                    else if (((Item2LotCheck)item2LotCheck).CreateType == CreateType.CREATETYPE_HEXADECIMAL)
                        scale = NumberScale.Scale16;
                    else if (((Item2LotCheck)item2LotCheck).CreateType == CreateType.CREATETYPE_THIRTYFOUR)
                        scale = NumberScale.Scale34;
                    int prefixLength = ((Item2LotCheck)item2LotCheck).SNPrefix.Length;
                    int lotLength = ((Item2LotCheck)item2LotCheck).SNLength;
                    long maxLotNo = long.Parse(NumberScaleHelper.ChangeNumber(((MO2LotLink)mo2LotLink[0]).LotNo.Substring(prefixLength), scale, NumberScale.Scale10));
                    string newLotNo = NumberScaleHelper.ChangeNumber((maxLotNo + nextSeq).ToString(), NumberScale.Scale10, scale).PadLeft(lotLength - prefixLength, '0');
                    if (newLotNo.Length > lotLength - prefixLength)
                    {
                        return "False$CS_LotNoLength_ERROR";
                    }
                    return ((Item2LotCheck)item2LotCheck).SNPrefix + newLotNo;
                }
            }            

            return null;
        }
        #endregion

    }
}

