using System;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.MOModel;
using System.Collections;
using BenQGuru.eMES.Domain.BaseSetting;
using System.Data;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.OQC
{
    /// <summary>
    /// OQCFacade 的摘要说明。
    /// 文件名:		OQCFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
    /// 创建日期:	2005-05-21 15:53:24
    /// 修改人:
    /// 修改日期:
    /// 描 述:	
    /// 版 本:	
    /// $History
    /// $Author
    /// $Date
    /// </summary>
    public class OQCFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public const string OQC_ExameObject_PlanarCode = "planarcode";
        public const string OQC_ExameObject_Carton = "carton";
        public const string OQC_ExameObject_PCS = "pcs";

        public const string OQC_AGrade = "AGrade";
        public const string OQC_BGrade = "BGrade";
        public const string OQC_CGrade = "CGrade";
        public const string OQC_ZGrade = "ZGrade";

        public const int Lot_Sequence_Default = 0;
        public const int RunningCard_Sequence_Default = 0;
        public const decimal Decimal_Default_value = 0;

        public OQCFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        public OQCFacade(IDomainDataProvider domainDataProvider)
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

        #region Item2OQCCheckList
        /// <summary>
        /// 
        /// </summary>
        public Item2OQCCheckList CreateNewItem2OQCCheckList()
        {
            return new Item2OQCCheckList();
        }

        public void AddItem2OQCCheckList(Item2OQCCheckList item2OQCCheckList)
        {
            this._helper.AddDomainObject(item2OQCCheckList);
        }

        public void UpdateItem2OQCCheckList(Item2OQCCheckList item2OQCCheckList)
        {
            this._helper.UpdateDomainObject(item2OQCCheckList);
        }

        public void DeleteItem2OQCCheckList(Item2OQCCheckList item2OQCCheckList)
        {
            this._helper.DeleteDomainObject(item2OQCCheckList,
                                new ICheck[]{ new DeleteAssociateCheck( item2OQCCheckList,
														this.DataProvider, 
														new Type[]{
																typeof(OQCLOTCardCheckList)	})});
        }

        public void DeleteItem2OQCCheckList(Item2OQCCheckList[] item2OQCCheckList)
        {
            this._helper.DeleteDomainObject(item2OQCCheckList);
        }

        public object GetItem2OQCCheckList(string itemCode, string checkItemCode, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(Item2OQCCheckList), new object[] { itemCode, checkItemCode, orgID });
        }

        /// <summary>
        /// ** 功能描述:	查询Item2OQCCheckList的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，精确查询</param>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <returns> Item2OQCCheckList的总记录数</returns>
        public int QueryItem2OQCCheckListCount(string itemCode, string checkItemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEM2OQCCKLIST where 1=1 and ITEMCODE = '{0}'  and CKITEMCODE like '{1}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), itemCode, checkItemCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Item2OQCCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，精确查询</param>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// 		/// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Item2OQCCheckList数组</returns>
        public object[] QueryItem2OQCCheckList(string itemCode, string checkItemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item2OQCCheckList), new PagerCondition(string.Format("select {0} from TBLITEM2OQCCKLIST where 1=1 and ITEMCODE = '{1}'  and CKITEMCODE like '{2}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2OQCCheckList)), itemCode, checkItemCode), "SEQ,ITEMCODE,CKITEMCODE", inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	分页查询Item2OQCCheckList
        /// ** 作 者:		Laws Lu
        /// ** 日 期:		2005-10-18 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，精确查询</param>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Item2OQCCheckList数组</returns>
        public object[] QueryItem2OQCCheckListNoPager(string itemCode, string checkItemCode)
        {
            return this.DataProvider.CustomQuery(typeof(Item2OQCCheckList)
                , new SQLCondition(
                string.Format("select {0} from TBLITEM2OQCCKLIST where 1=1 and ITEMCODE = '{1}'  and CKITEMCODE like '{2}%' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by seq", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2OQCCheckList)), itemCode, checkItemCode)));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Item2OQCCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Item2OQCCheckList的总记录数</returns>
        public object[] GetAllItem2OQCCheckList()
        {
            return this.DataProvider.CustomQuery(typeof(Item2OQCCheckList), new SQLCondition(string.Format("select {0} from TBLITEM2OQCCKLIST where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by ITEMCODE,CKITEMCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2OQCCheckList)))));
        }

        public void AddItem2OQCCheckList(Item2OQCCheckList[] item2OQCCheckLists)
        {
            this._helper.AddDomainObject(item2OQCCheckLists);
        }

        #region OQCCheckList --> Item
        /// <summary>
        /// ** 功能描述:	由CheckItemCode获得Item
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode,精确查询</param>
        /// <returns>Item数组</returns>
        public object[] GetItemByCheckItemCode(string checkItemCode)
        {
            return this.DataProvider.CustomQuery(typeof(Item), new SQLCondition(string.Format("select {0} from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE in ( select ITEMCODE from TBLITEM2OQCCKLIST where CKITEMCODE='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)), checkItemCode)));
        }

        /// <summary>
        /// ** 功能描述:	由CheckItemCode获得属于OQCCheckList的Item的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode,精确查询</param>
        /// <param name="itemCode">ItemCode,模糊查询</param>
        /// <returns>Item的数量</returns>
        public int GetSelectedItemByCheckItemCodeCount(string checkItemCode, string itemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEM2OQCCKLIST where CKITEMCODE ='{0}' and ITEMCODE like '{1}%'" + GlobalVariables.CurrentOrganizations.GetSQLCondition(), checkItemCode, itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	由CheckItemCode获得属于OQCCheckList的Item，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode,精确查询</param>
        /// <param name="itemCode">ItemCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Item数组</returns>
        public object[] GetSelectedItemByCheckItemCode(string checkItemCode, string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item),
                            new PagerCondition(string.Format("select {0} from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE in ( select ITEMCODE from TBLITEM2OQCCKLIST where CKITEMCODE ='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and ITEMCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)), checkItemCode, itemCode), "ITEMCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	由CheckItemCode获得不属于OQCCheckList的Item的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode,精确查询</param>
        /// <param name="itemCode">ItemCode,模糊查询</param>
        /// <returns>Item的数量</returns>
        public int GetUnselectedItemByCheckItemCodeCount(string checkItemCode, string itemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE not in ( select ITEMCODE from TBLITEM2OQCCKLIST where CKITEMCODE ='{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and ITEMCODE like '{1}%'", checkItemCode, itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	由CheckItemCode获得不属于OQCCheckList的Item，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode,精确查询</param>
        /// <param name="itemCode">ItemCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Item数组</returns>
        public object[] GetUnselectedItemByCheckItemCode(string checkItemCode, string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item),
                            new PagerCondition(string.Format("select {0} from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE not in ( select ITEMCODE from TBLITEM2OQCCKLIST where CKITEMCODE ='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and ITEMCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)), checkItemCode, itemCode), "ITEMCODE", inclusive, exclusive));
        }
        #endregion

        #region Item --> OQCCheckList
        /// <summary>
        /// ** 功能描述:	由ItemCode获得OQCCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode,精确查询</param>
        /// <returns>OQCCheckList数组</returns>
        public object[] GetOQCCheckListByItemCode(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(OQCCheckList), new SQLCondition(string.Format("select {0} from TBLOQCCKLIST where CKITEMCODE in ( select CKITEMCODE from TBLITEM2OQCCKLIST where ITEMCODE='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckList)), itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	由ItemCode获得属于Item的OQCCheckList的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode,精确查询</param>
        /// <param name="checkItemCode">CheckItemCode,模糊查询</param>
        /// <returns>OQCCheckList的数量</returns>
        public int GetSelectedOQCCheckListByItemCodeCount(string itemCode, string checkItemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEM2OQCCKLIST where ITEMCODE ='{0}' and CKITEMCODE like '{1}%'" + GlobalVariables.CurrentOrganizations.GetSQLCondition(), itemCode, checkItemCode)));
        }

        /// <summary>
        /// ** 功能描述:	由ItemCode获得属于Item的OQCCheckList，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode,精确查询</param>
        /// <param name="checkItemCode">CheckItemCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>OQCCheckList数组</returns>
        public object[] GetSelectedOQCCheckListByItemCode(string itemCode, string checkItemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCCheckList),
                            new PagerCondition(string.Format("select {0} from TBLOQCCKLIST where CKITEMCODE in ( select CKITEMCODE from TBLITEM2OQCCKLIST where ITEMCODE ='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and CKITEMCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckList)), itemCode, checkItemCode), "CKITEMCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	由ItemCode获得不属于Item的OQCCheckList的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode,精确查询</param>
        /// <param name="checkItemCode">CheckItemCode,模糊查询</param>
        /// <returns>OQCCheckList的数量</returns>
        public int GetUnselectedOQCCheckListByItemCodeCount(string itemCode, string checkItemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCCKLIST where CKITEMCODE not in ( select CKITEMCODE from TBLITEM2OQCCKLIST where ITEMCODE ='{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and CKITEMCODE like '{1}%'", itemCode, checkItemCode)));
        }

        /// <summary>
        /// ** 功能描述:	由ItemCode获得不属于Item的OQCCheckList，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode,精确查询</param>
        /// <param name="checkItemCode">CheckItemCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>OQCCheckList数组</returns>
        public object[] GetUnselectedOQCCheckListByItemCode(string itemCode, string checkItemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCCheckList),
                            new PagerCondition(string.Format("select {0} from TBLOQCCKLIST where CKITEMCODE not in ( select CKITEMCODE from TBLITEM2OQCCKLIST where ITEMCODE ='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and CKITEMCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckList)), itemCode, checkItemCode), "CKITEMCODE", inclusive, exclusive));
        }
        #endregion


        #endregion

        #region ItemOP2OQCCheckList
        /// <summary>
        /// 
        /// </summary>
        public ItemOP2OQCCheckList CreateNewItemOP2OQCCheckList()
        {
            return new ItemOP2OQCCheckList();
        }

        public void AddItemOP2OQCCheckList(ItemOP2OQCCheckList itemOP2OQCCheckList)
        {
            this._helper.AddDomainObject(itemOP2OQCCheckList);
        }

        public void UpdateItemOP2OQCCheckList(ItemOP2OQCCheckList itemOP2OQCCheckList)
        {
            this._helper.UpdateDomainObject(itemOP2OQCCheckList);
        }

        public void DeleteItemOP2OQCCheckList(ItemOP2OQCCheckList itemOP2OQCCheckList)
        {
            this._helper.DeleteDomainObject(itemOP2OQCCheckList);
        }

        public void DeleteItemOP2OQCCheckList(ItemOP2OQCCheckList[] itemOP2OQCCheckList)
        {
            this._helper.DeleteDomainObject(itemOP2OQCCheckList);
        }

        public object GetItemOP2OQCCheckList(string checkItemCode, string itemCode, string oPCode, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(ItemOP2OQCCheckList), new object[] { checkItemCode, itemCode, oPCode, orgID });
        }

        /// <summary>
        /// ** 功能描述:	查询ItemOP2OQCCheckList的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="oPCode">OPCode，模糊查询</param>
        /// <returns> ItemOP2OQCCheckList的总记录数</returns>
        public int QueryItemOP2OQCCheckListCount(string checkItemCode, string itemCode, string oPCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEMOP2OQCCKLIST where 1=1 and CKITEMCODE like '{0}%'  and ITEMCODE like '{1}%'  and OPCODE like '{2}%' and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID, checkItemCode, itemCode, oPCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ItemOP2OQCCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="oPCode">OPCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> ItemOP2OQCCheckList数组</returns>
        public object[] QueryItemOP2OQCCheckList(string checkItemCode, string itemCode, string oPCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ItemOP2OQCCheckList), new PagerCondition(string.Format("select {0} from TBLITEMOP2OQCCKLIST where 1=1 and CKITEMCODE like '{1}%'  and ITEMCODE like '{2}%'  and OPCODE like '{3}%' and orgID=" + GlobalVariables.CurrentOrganizations.First().OrganizationID, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemOP2OQCCheckList)), checkItemCode, itemCode, oPCode), "SEQ,CKITEMCODE,ITEMCODE,OPCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的ItemOP2OQCCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>ItemOP2OQCCheckList的总记录数</returns>
        public object[] GetAllItemOP2OQCCheckList()
        {
            return this.DataProvider.CustomQuery(typeof(ItemOP2OQCCheckList), new SQLCondition(string.Format("select {0} from TBLITEMOP2OQCCKLIST where orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID + " order by CKITEMCODE,ITEMCODE,OPCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemOP2OQCCheckList)))));
        }
        #endregion

        #region OQCCheckList
        /// <summary>
        /// 
        /// </summary>
        public OQCCheckList CreateNewOQCCheckList()
        {
            return new OQCCheckList();
        }

        public void AddOQCCheckList(OQCCheckList oQCCheckList)
        {
            this._helper.AddDomainObject(oQCCheckList);
        }

        public void UpdateOQCCheckList(OQCCheckList oQCCheckList)
        {
            this._helper.UpdateDomainObject(oQCCheckList);
        }

        public void DeleteOQCCheckList(OQCCheckList oQCCheckList)
        {
            this._helper.DeleteDomainObject(oQCCheckList,
                                new ICheck[]{ new DeleteAssociateCheck( oQCCheckList,
														this.DataProvider, 
														new Type[]{
																typeof(Item2OQCCheckList), typeof(OQCCheckGroup2List)	})});
        }

        public void DeleteOQCCheckList(OQCCheckList[] oQCCheckList)
        {
            this._helper.DeleteDomainObject(oQCCheckList,
                                new ICheck[]{ new DeleteAssociateCheck( oQCCheckList,
														this.DataProvider, 
														new Type[]{
																typeof(Item2OQCCheckList), typeof(OQCCheckGroup2List)	})});
        }

        public object GetOQCCheckList(string checkItemCode)
        {
            return this.DataProvider.CustomSearch(typeof(OQCCheckList), new object[] { checkItemCode });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCCheckList的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <returns> OQCCheckList的总记录数</returns>
        public int QueryOQCCheckListCount(string checkItemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCCKLIST where 1=1 and CKITEMCODE like '{0}%' ", checkItemCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCCheckList数组</returns>
        public object[] QueryOQCCheckList(string checkItemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCCheckList), new PagerCondition(string.Format("select {0} from TBLOQCCKLIST where 1=1 and CKITEMCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckList)), checkItemCode), "CKITEMCODE", inclusive, exclusive));
        }




        /// <summary>
        /// ** 功能描述:	获得所有的OQCCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCCheckList的总记录数</returns>
        public object[] GetAllOQCCheckList()
        {
            return this.DataProvider.CustomQuery(typeof(OQCCheckList), new SQLCondition(string.Format("select {0} from TBLOQCCKLIST order by CKITEMCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckList)))));
        }

        public object[] GetOQCCheckListByCheckGroup(string checkGroupCodeList)
        {
            string sql;
            sql = "";
            sql += "SELECT a.ckitemcode AS ckitemcode, b.ckgroup AS ckgroup";
            sql += "  FROM tbloqccklist a, tbloqcckgroup2list b";
            sql += " WHERE a.ckitemcode = b.ckitemcode AND b.ckgroup IN ('" + checkGroupCodeList + "')";

            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckListQuery)));
            return this.DataProvider.CustomQuery(typeof(OQCCheckListQuery), new SQLCondition(sql));
        }
        #endregion

        #region OQCCheckGroup
        public OQCCheckGroup CreateNewOQCCheckGroup()
        {
            return new OQCCheckGroup();
        }

        public void AddOQCCheckGroup(OQCCheckGroup checkGroup)
        {
            this._helper.AddDomainObject(checkGroup);
        }

        public void UpdateOQCCheckGroup(OQCCheckGroup checkGroup)
        {
            this._helper.UpdateDomainObject(checkGroup);
        }

        public void DeleteOQCCheckGroup(OQCCheckGroup checkGroup)
        {
            this._helper.DeleteDomainObject(checkGroup, new ICheck[] { new DeleteAssociateCheck(checkGroup,
                                                                            this.DataProvider, 
                                                                            new Type[]{ typeof(OQCCheckGroup2List) })});
        }

        public void DeleteOQCCheckGroup(OQCCheckGroup[] checkGroups)
        {
            this._helper.DeleteDomainObject(checkGroups, new ICheck[] { new DeleteAssociateCheck(checkGroups,
                                                                            this.DataProvider, 
                                                                            new Type[]{ typeof(OQCCheckGroup2List) })});
        }

        public object GetOQCCheckGroup(string chkGroup)
        {
            return this.DataProvider.CustomSearch(typeof(OQCCheckGroup), new object[] { chkGroup });
        }

        /// <summary>
        /// ** 功能描述:	查询CheckGroup的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="chkGroup">ChkGroup，模糊查询</param>
        /// <returns> CheckGroup的总记录数</returns>
        public int QueryOQCCheckGroupCount(string chkGroup)
        {
            string qSql = string.Format("select count(*) from TBLOQCCKGROUP where 1=1 and CKGROUP like '{0}%' ", chkGroup);

            return this.DataProvider.GetCount(new SQLCondition(qSql));
        }

        /// <summary>
        /// ** 功能描述:	分页查询CheckGroup
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        public object[] QueryOQCCheckGroup(string chkGroup, int inclusive, int exclusive)
        {
            string qSql = string.Format("select {0} from TBLOQCCKGROUP where 1=1 and CKGROUP like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckGroup)), chkGroup);

            return this.DataProvider.CustomQuery(typeof(OQCCheckGroup), new PagerCondition(qSql, "ckgroup", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的CKGROUP
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>CKGROUP的总记录数</returns>
        public object[] GetAllOQCCheckGroup()
        {
            // Added by HI1/Roger.xue on 20080715 for Hisense Version : Add Organization ID
            string sql = "";
            sql = "select {0} from TBLOQCCKGROUP WHERE 1=1 ";
            sql += " order by CKGROUP ";
            // End Added

            return this.DataProvider.CustomQuery(typeof(OQCCheckGroup), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckGroup)))));
        }

        /// <summary>
        /// Get All OQC Check Group List by Item Code and OrgId
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public object[] GetAllOQCCheckGroupByItemCode(string itemCode, int orgID)
        {
            string strsql = "";
            strsql += "SELECT {0}";
            strsql += "  FROM tbloqcckgroup";
            strsql += " WHERE ckgroup IN (SELECT DISTINCT ckgroup";
            strsql += "                              FROM tbloqccklist";
            strsql += "                             WHERE ckitemcode IN (";
            strsql += "                                             SELECT DISTINCT ckitemcode";
            strsql += "                                                        FROM tblitem2oqccklist";
            strsql += "                                                       WHERE itemcode = '" + itemCode + "'";
            strsql += "                                                         AND orgid = " + orgID + "))";

            return this.DataProvider.CustomQuery(typeof(OQCCheckGroup),
                                    new SQLCondition(string.Format(strsql,
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckGroup)))));
        }

        #endregion

        #region OQCFuncTest
        /// <summary>
        /// 
        /// </summary>
        public OQCFuncTest CreateNewOQCFuncTest()
        {
            return new OQCFuncTest();
        }

        public void AddOQCFuncTest(OQCFuncTest oQCFuncTest)
        {
            this._helper.AddDomainObject(oQCFuncTest);
        }

        public void UpdateOQCFuncTest(OQCFuncTest oQCFuncTest)
        {
            this._helper.UpdateDomainObject(oQCFuncTest);
        }

        public void DeleteOQCFuncTest(OQCFuncTest oQCFuncTest)
        {
            this._helper.DeleteDomainObject(oQCFuncTest);
        }

        public void DeleteOQCFuncTest(OQCFuncTest[] oQCFuncTest)
        {
            this._helper.DeleteDomainObject(oQCFuncTest);
        }

        public object GetOQCFuncTest(string itemCode)
        {
            return this.DataProvider.CustomSearch(typeof(OQCFuncTest), new object[] { itemCode });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCFuncTest的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <returns> OQCFuncTest的总记录数</returns>
        public int QueryOQCFuncTestCount(string itemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCFUNCTEST where 1=1 and ItemCode like '{0}%' ", itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCFuncTest
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCFuncTest数组</returns>
        public object[] QueryOQCFuncTest(string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTest), new PagerCondition(string.Format("select {0} from TBLOQCFUNCTEST where 1=1 and ItemCode like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTest)), itemCode), "ItemCode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCFuncTest
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCFuncTest的总记录数</returns>
        public object[] GetAllOQCFuncTest()
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTest), new SQLCondition(string.Format("select {0} from TBLOQCFUNCTEST order by ItemCode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTest)))));
        }


        #endregion

        #region OQCFuncTestSpec
        /// <summary>
        /// 
        /// </summary>
        public OQCFuncTestSpec CreateNewOQCFuncTestSpec()
        {
            return new OQCFuncTestSpec();
        }

        public void AddOQCFuncTestSpec(OQCFuncTestSpec oQCFuncTestSpec)
        {
            this._helper.AddDomainObject(oQCFuncTestSpec);
        }

        public void UpdateOQCFuncTestSpec(OQCFuncTestSpec oQCFuncTestSpec)
        {
            this._helper.UpdateDomainObject(oQCFuncTestSpec);
        }

        public void DeleteOQCFuncTestSpec(OQCFuncTestSpec oQCFuncTestSpec)
        {
            this._helper.DeleteDomainObject(oQCFuncTestSpec);
        }

        public void DeleteOQCFuncTestSpec(OQCFuncTestSpec[] oQCFuncTestSpec)
        {
            this._helper.DeleteDomainObject(oQCFuncTestSpec);
        }

        public object GetOQCFuncTestSpec(string itemCode, decimal groupSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCFuncTestSpec), new object[] { itemCode, groupSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCFuncTestSpec的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="groupSequence">GroupSequence，模糊查询</param>
        /// <returns> OQCFuncTestSpec的总记录数</returns>
        public int QueryOQCFuncTestSpecCount(string itemCode, decimal groupSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCFUNCTESTSPEC where 1=1 and ItemCode like '{0}%'  and GROUPSEQ like '{1}%' ", itemCode, groupSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCFuncTestSpec
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="groupSequence">GroupSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCFuncTestSpec数组</returns>
        public object[] QueryOQCFuncTestSpec(string itemCode, decimal groupSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestSpec), new PagerCondition(string.Format("select {0} from TBLOQCFUNCTESTSPEC where 1=1 and ItemCode like '{1}%'  and GROUPSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestSpec)), itemCode, groupSequence), "ItemCode,GROUPSEQ", inclusive, exclusive));
        }
        public object[] QueryOQCFuncTestSpec(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestSpec), new SQLCondition(string.Format("select {0} from TBLOQCFUNCTESTSPEC where 1=1 and ItemCode='{1}' order by GROUPSEQ ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestSpec)), itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCFuncTestSpec
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCFuncTestSpec的总记录数</returns>
        public object[] GetAllOQCFuncTestSpec()
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestSpec), new SQLCondition(string.Format("select {0} from TBLOQCFUNCTESTSPEC order by ItemCode,GROUPSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestSpec)))));
        }


        #endregion

        #region OQCFuncTestValue
        /// <summary>
        /// 
        /// </summary>
        public OQCFuncTestValue CreateNewOQCFuncTestValue()
        {
            return new OQCFuncTestValue();
        }

        public void AddOQCFuncTestValue(OQCFuncTestValue oQCFuncTestValue)
        {
            this._helper.AddDomainObject(oQCFuncTestValue);
        }

        public void UpdateOQCFuncTestValue(OQCFuncTestValue oQCFuncTestValue)
        {
            this._helper.UpdateDomainObject(oQCFuncTestValue);
        }

        public void DeleteOQCFuncTestValue(OQCFuncTestValue oQCFuncTestValue)
        {
            this._helper.DeleteDomainObject(oQCFuncTestValue);
        }

        public void DeleteOQCFuncTestValue(OQCFuncTestValue[] oQCFuncTestValue)
        {
            this._helper.DeleteDomainObject(oQCFuncTestValue);
        }

        public object GetOQCFuncTestValue(string runningCard, decimal runningCardSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCFuncTestValue), new object[] { runningCard, runningCardSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCFuncTestValue的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <returns> OQCFuncTestValue的总记录数</returns>
        public int QueryOQCFuncTestValueCount(string runningCard, decimal runningCardSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCFUNCTESTVALUE where 1=1 and RCARD like '{0}%'  and RCARDSEQ like '{1}%' ", runningCard, runningCardSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCFuncTestValue
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCFuncTestValue数组</returns>
        public object[] QueryOQCFuncTestValue(string runningCard, decimal runningCardSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValue), new PagerCondition(string.Format("select {0} from TBLOQCFUNCTESTVALUE where 1=1 and RCARD like '{1}%'  and RCARDSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValue)), runningCard, runningCardSequence), "RCARD,RCARDSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCFuncTestValue
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCFuncTestValue的总记录数</returns>
        public object[] GetAllOQCFuncTestValue()
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValue), new SQLCondition(string.Format("select {0} from TBLOQCFUNCTESTVALUE order by RCARD,RCARDSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValue)))));
        }

        public object[] GetOQCFuncTestValueByLotNo(string lotNo, decimal lotSequence)
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValue), new SQLCondition(string.Format("select {0} from TBLOQCFUNCTESTVALUE where 1=1 and LOTNO = '{1}'  and LOTSEQ = '{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValue)), lotNo, lotSequence)));
        }

        #endregion

        #region OQCFuncTestValueDetail
        /// <summary>
        /// 
        /// </summary>
        public OQCFuncTestValueDetail CreateNewOQCFuncTestValueDetail()
        {
            return new OQCFuncTestValueDetail();
        }

        public void AddOQCFuncTestValueDetail(OQCFuncTestValueDetail oQCFuncTestValueDetail)
        {
            this._helper.AddDomainObject(oQCFuncTestValueDetail);
        }

        public void UpdateOQCFuncTestValueDetail(OQCFuncTestValueDetail oQCFuncTestValueDetail)
        {
            this._helper.UpdateDomainObject(oQCFuncTestValueDetail);
        }

        public void DeleteOQCFuncTestValueDetail(OQCFuncTestValueDetail oQCFuncTestValueDetail)
        {
            this._helper.DeleteDomainObject(oQCFuncTestValueDetail);
        }

        public void DeleteOQCFuncTestValueDetail(OQCFuncTestValueDetail[] oQCFuncTestValueDetail)
        {
            this._helper.DeleteDomainObject(oQCFuncTestValueDetail);
        }

        public object GetOQCFuncTestValueDetail(string runningCard, decimal runningCardSequence, decimal groupSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCFuncTestValueDetail), new object[] { runningCard, runningCardSequence, groupSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCFuncTestValueDetail的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="groupSequence">GroupSequence，模糊查询</param>
        /// <returns> OQCFuncTestValueDetail的总记录数</returns>
        public int QueryOQCFuncTestValueDetailCount(string runningCard, decimal runningCardSequence, decimal groupSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCFUNCTESTVALUEDTL where 1=1 and RCARD like '{0}%'  and RCARDSEQ like '{1}%'  and GROUPSEQ like '{2}%' ", runningCard, runningCardSequence, groupSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCFuncTestValueDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="groupSequence">GroupSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCFuncTestValueDetail数组</returns>
        public object[] QueryOQCFuncTestValueDetail(string runningCard, decimal runningCardSequence, decimal groupSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValueDetail), new PagerCondition(string.Format("select {0} from TBLOQCFUNCTESTVALUEDTL where 1=1 and RCARD like '{1}%'  and RCARDSEQ like '{2}%'  and GROUPSEQ like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValueDetail)), runningCard, runningCardSequence, groupSequence), "RCARD,RCARDSEQ,GROUPSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCFuncTestValueDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCFuncTestValueDetail的总记录数</returns>
        public object[] GetAllOQCFuncTestValueDetail()
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValueDetail), new SQLCondition(string.Format("select {0} from TBLOQCFUNCTESTVALUEDTL order by RCARD,RCARDSEQ,GROUPSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValueDetail)))));
        }


        #endregion

        #region OQCFuncTestValueEleDetail
        /// <summary>
        /// 
        /// </summary>
        public OQCFuncTestValueEleDetail CreateNewOQCFuncTestValueEleDetail()
        {
            return new OQCFuncTestValueEleDetail();
        }

        public void AddOQCFuncTestValueEleDetail(OQCFuncTestValueEleDetail oQCFuncTestValueEleDetail)
        {
            this._helper.AddDomainObject(oQCFuncTestValueEleDetail);
        }

        public void UpdateOQCFuncTestValueEleDetail(OQCFuncTestValueEleDetail oQCFuncTestValueEleDetail)
        {
            this._helper.UpdateDomainObject(oQCFuncTestValueEleDetail);
        }

        public void DeleteOQCFuncTestValueEleDetail(OQCFuncTestValueEleDetail oQCFuncTestValueEleDetail)
        {
            this._helper.DeleteDomainObject(oQCFuncTestValueEleDetail);
        }

        public void DeleteOQCFuncTestValueEleDetail(OQCFuncTestValueEleDetail[] oQCFuncTestValueEleDetail)
        {
            this._helper.DeleteDomainObject(oQCFuncTestValueEleDetail);
        }

        public object GetOQCFuncTestValueEleDetail(string runningCard, decimal runningCardSequence, decimal groupSequence, decimal electricSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCFuncTestValueEleDetail), new object[] { runningCard, runningCardSequence, groupSequence, electricSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCFuncTestValueEleDetail的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="groupSequence">GroupSequence，模糊查询</param>
        /// <param name="electricSequence">ElectricSequence，模糊查询</param>
        /// <returns> OQCFuncTestValueEleDetail的总记录数</returns>
        public int QueryOQCFuncTestValueEleDetailCount(string runningCard, decimal runningCardSequence, decimal groupSequence, decimal electricSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCFUNCTESTVALUEELEDTL where 1=1 and RCARD like '{0}%'  and RCARDSEQ like '{1}%'  and GROUPSEQ like '{2}%'  and ElectricSequence like '{3}%' ", runningCard, runningCardSequence, groupSequence, electricSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCFuncTestValueEleDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="groupSequence">GroupSequence，模糊查询</param>
        /// <param name="electricSequence">ElectricSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCFuncTestValueEleDetail数组</returns>
        public object[] QueryOQCFuncTestValueEleDetail(string runningCard, decimal runningCardSequence, decimal groupSequence, decimal electricSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValueEleDetail), new PagerCondition(string.Format("select {0} from TBLOQCFUNCTESTVALUEELEDTL where 1=1 and RCARD like '{1}%'  and RCARDSEQ like '{2}%'  and GROUPSEQ like '{3}%'  and ElectricSequence like '{4}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValueEleDetail)), runningCard, runningCardSequence, groupSequence, electricSequence), "RCARD,RCARDSEQ,GROUPSEQ,ElectricSequence", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCFuncTestValueEleDetail
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-6-8 8:48:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCFuncTestValueEleDetail的总记录数</returns>
        public object[] GetAllOQCFuncTestValueEleDetail()
        {
            return this.DataProvider.CustomQuery(typeof(OQCFuncTestValueEleDetail), new SQLCondition(string.Format("select {0} from TBLOQCFUNCTESTVALUEELEDTL order by RCARD,RCARDSEQ,GROUPSEQ,ElectricSequence", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValueEleDetail)))));
        }


        #endregion

        #region OQCLot
        /// <summary>
        /// 
        /// </summary>
        public OQCLot CreateNewOQCLot()
        {
            return new OQCLot();
        }

        public void AddOQCLot(OQCLot oQCLot)
        {
            this._helper.AddDomainObject(oQCLot);
        }

        public void UpdateOQCLot(OQCLot oqcLot)
        {
            this.DataProvider.Update(oqcLot);
        }

        public object GetNewLotNO(string lineCode, string shiftDay)
        {
            string Sql = "select {0} from (select {1} from  TBLLOT where lotno like '" + lineCode + shiftDay + "L%' and length(lotno)=15 ) order by lotno desc";

            object[] objs = this.DataProvider.CustomQuery(typeof(OQCLot)
                , new SQLCondition(String.Format(Sql
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot))
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot)))));


            if (objs != null)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Laws Lu,2005/10/21
        /// 更新样本计划，不需要修改批数量
        /// </summary>
        /// <param name="oQCLot">批实体</param>
        public void UpdateOQCLotSample(OQCLot oQCLot)
        {
            string updateSql = "update TBLLOT set LOTSTATUS = '"
                + oQCLot.LOTStatus + "' "
                + ",aql = " + oQCLot.AQL
                + ",aql1 = " + oQCLot.AQL1
                + ",aql2 = " + oQCLot.AQL2
                + ",aql3 = " + oQCLot.AQL3
                + ",ssize = " + oQCLot.SampleSize
                + ",rjtsize = " + oQCLot.RejectSize
                + ",rjtsize1 = " + oQCLot.RejectSize1
                + ",rjtsize2 = " + oQCLot.RejectSize2
                + ",rjtsize3 = " + oQCLot.RejectSize3
                + ",accsize = " + oQCLot.AcceptSize
                + ",accsize1 = " + oQCLot.AcceptSize1
                + ",accsize2 = " + oQCLot.AcceptSize2
                + ",accsize3 = " + oQCLot.AcceptSize3
                + ",EAttribute1 = '" + oQCLot.EAttribute1 + "'"
                + ",productiontype='" + oQCLot.ProductionType + "'"
                + ",oqclottype='" + oQCLot.OQCLotType + "'"
                + " where LOTNO='" + oQCLot.LOTNO + "' AND LOTSEQ=" + oQCLot.LotSequence + "";

            this.DataProvider.CustomExecute(new SQLCondition(updateSql));
        }

        public void LockOQCLotByLotNO(string lotNo)
        {
            string sql = " Select * from tbllot where lotno='" + lotNo.Trim().ToUpper() + "'  for update";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        /// <summary>
        /// 更新批数量
        /// </summary>
        /// <param name="oQCLot">批实体</param>
        public void UpdateOQCLotSize(OQCLot oQCLot)
        {
            string updateSql = "update TBLLOT set LOTSIZE = LOTSIZE + " + oQCLot.LotSize + " where LOTNO='" + oQCLot.LOTNO + "' AND LOTSEQ=" + oQCLot.LotSequence + "";

            int iReturn = 0;
            iReturn = (DataProvider as SQLDomainDataProvider).CustomExecuteWithReturn(new SQLCondition(updateSql));

            if (iReturn <= 0)
            {
                throw new Exception("$CS_FQC_LOT_USED");
            }
        }
        /// <summary>
        /// 更新批状态
        /// </summary>
        /// <param name="oQCLot">批实体</param>
        public void UpdateOQCLotStatus(OQCLot oQCLot)
        {
            string updateSql = "update TBLLOT set LOTSTATUS = '"
                + oQCLot.LOTStatus
                + "',DDate="
                + oQCLot.DealDate
                + ",Dtime="
                + oQCLot.DealTime
                + ",DUSER='"
                + oQCLot.DealUser
                + "',memo='"
                + oQCLot.Memo
                + "' where LOTNO='" + oQCLot.LOTNO + "' AND LOTSEQ=" + oQCLot.LotSequence + "";

            int iReturn = 0;
            iReturn = (DataProvider as SQLDomainDataProvider).CustomExecuteWithReturn(new SQLCondition(updateSql));

            if (iReturn <= 0)
            {
                throw new Exception("$CS_FQC_LOT_USED");
            }
        }

        public void UpdateOQCLotFrozen(OQCLot lot)
        {
            string updateSql = "update TBLLOT set lotfrozen = '"
                + lot.LotFrozen.Trim()
                + "',FrozenStatus ='"
                + lot.FrozenStatus
                + "', FrozenReason='"
                + lot.FrozenReason
                + "',FrozenDate='"
                + lot.FrozenDate
                + "',FrozenTime='"
                + lot.FrozenTime
                + "',FrozenBy='"
                + lot.FrozenBy
                + "' where LOTNO='" + lot.LOTNO + "' AND LOTSEQ=" + lot.LotSequence + "";

            int iReturn = 0;
            iReturn = (DataProvider as SQLDomainDataProvider).CustomExecuteWithReturn(new SQLCondition(updateSql));

            if (iReturn <= 0)
            {
                throw new Exception("$CS_FQC_LOT_USED");
            }
        }

        public void DeleteOQCLot(OQCLot oQCLot)
        {
            this._helper.DeleteDomainObject(oQCLot,
                                new ICheck[]{ new DeleteAssociateCheck( oQCLot,
														this.DataProvider, 
														new Type[]{
																typeof(OQCLot2ErrorCode),
																typeof(OQCLot2Card),
																typeof(OQCLOTCheckList)	})});
        }

        public void DeleteOQCLot(OQCLot[] oQCLot)
        {
            this._helper.DeleteDomainObject(oQCLot,
                                new ICheck[]{ new DeleteAssociateCheck( oQCLot,
														this.DataProvider, 
														new Type[]{
																typeof(OQCLot2ErrorCode),
																typeof(OQCLot2Card),
																typeof(OQCLOTCheckList)	})});
        }

        public object[] GetExamingNoExameInitialOQCLot()
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCLot),
                new SQLCondition(string.Format("select {0} from TBLLOT "
                + " WHERE  LOTSTATUS IN('{1}','{2}','{3}','{4}') ORDER BY LOTNO"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot))
                , Web.Helper.OQCLotStatus.OQCLotStatus_Examing
                , Web.Helper.OQCLotStatus.OQCLotStatus_NoExame
                , Web.Helper.OQCLotStatus.OQCLotStatus_Initial
                , Web.Helper.OQCLotStatus.OQCLotStatus_SendExame)));//Laws Lu,2006/07/12 support send examination

            if (objs == null)
            {
                return new object[] { };
            }
            else
            {
                return objs;
            }
        }

        public object GetOQCLot(string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLot), new object[] { lOTNO, lotSequence });
        }

        public object GetLatestRejectedOQCLot(string runningCard)
        {
            string sql = string.Empty;
            sql += "SELECT {0} ";
            sql += "FROM tbllot ";
            sql += "WHERE lotno IN (SELECT lotno FROM tbllot2card WHERE rcard = '{1}') ";
            sql += "AND lotseq = " + Lot_Sequence_Default.ToString() + " ";
            sql += "AND lotstatus = '" + OQCLotStatus.OQCLotStatus_Reject + "' ";
            sql += "ORDER BY ddate DESC, dtime DESC ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot)), runningCard);

            object[] lotList = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(sql));
            if (lotList == null)
            {
                return null;
            }
            else
            {
                return lotList[0];
            }
        }

        public object GetLatestOQCLot(string runningCard)
        {
            string sql = string.Empty;
            sql += "SELECT {0} ";
            sql += "FROM tbllot ";
            sql += "WHERE lotno IN (SELECT lotno FROM tbllot2card WHERE rcard = '{1}') ";
            sql += "AND lotseq = " + Lot_Sequence_Default.ToString() + " ";
            sql += "ORDER BY cdate DESC, ctime DESC ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot)), runningCard);

            object[] lotList = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(sql));
            if (lotList == null)
            {
                return null;
            }
            else
            {
                return lotList[0];
            }
        }

        public object GetExamingOQCLot(string lOTNO, decimal lotSequence)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCLot),
                new SQLCondition(string.Format("select {0} from TBLLOT "
                + " WHERE LOTNO ='{1}'and lotseq={2} and  LOTSTATUS ='{3}' for update nowait"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot))
                , lOTNO
                , lotSequence
                , Web.Helper.OQCLotStatus.OQCLotStatus_Examing)));

            if (objs != null)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        public object GetUnExameOQCLot(string lOTNO, decimal lotSequence)
        {
            object obj = this.DataProvider.CustomSearch(typeof(OQCLot), new object[] { lOTNO, lotSequence });
            if (obj == null)
            {
                return null;
            }
            else
            {
                OQCLot oqcLot = obj as OQCLot;
                if ((oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_NoExame) || (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial) || (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Examing))
                {
                    return obj;
                }
                else
                {
                    return null;
                }
            }
        }

        public object GetUnCompleteOQCLotByRunningID(string runningID)
        {
            //string selectSql = "SELECT "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot))+" FROM TBLLOT"
            //                 +" WHERE LOTNO IN(SELECT LOTNO FROM TBLLOT2CARD WHERE RCARD='"+runningID.Trim().ToUpper()+"') "
            //                 +" AND LOTSTATUS NOT IN('"+OQCLotStatus.OQCLotStatus_Pass+"','"+OQCLotStatus.OQCLotStatus_Reject+"')";
            //object[] objs = this.DataProvider.CustomSearch(typeof(OQCLot),new SQLCondition(selectSql));
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCLot),
                new SQLParamCondition(string.Format("select {0} from TBLLOT "
                  + " WHERE LOTNO IN(SELECT LOTNO FROM TBLLOT2CARD WHERE RCARD= $RCARD) "
                  + " AND LOTSTATUS NOT IN('" + OQCLotStatus.OQCLotStatus_Pass + "','" + OQCLotStatus.OQCLotStatus_Reject + "')",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot))),
                new SQLParameter[] { new SQLParameter("RCARD", typeof(string), runningID.ToUpper()) }));

            if (objs != null)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ** 功能描述:	查询OQCLot的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCLot的总记录数</returns>
        public int QueryOQCLotCount(string lOTNO, decimal lotSequence)
        {
            //return this.DataProvider.GetCount(new SQLCondition(string.Format("SELECT COUNT(*) FROM TBLLOT WHERE 1=1 AND LOTNO LIKE '{0}%'  AND LOTSEQ LIKE '{1}%' " , lOTNO, lotSequence)));
            return this.DataProvider.GetCount(new SQLCondition(string.Format(
                "SELECT COUNT(*) FROM TBLLOT WHERE LOTNO LIKE '{0}%'  AND LOTSEQ LIKE '{1}%' ", lOTNO, lotSequence)));
        }

        public object[] QueryOQCLotCountForAlert(string itemCode, string ssCode, string lotStatus)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string sql = string.Empty;
            sql += "SELECT ddate, mmodelcode, bigsscode, SUM(count) AS oqclotcount ";
            sql += "FROM ( ";
            sql += "  SELECT a.itemcode,a.ddate, b.mmodelcode, c.bigsscode, 1 AS count ";
            sql += "  FROM tbllot a, tblmaterial b, tblss c ";
            sql += "  WHERE a.itemcode = b.mcode ";
            sql += "  AND a.sscode = c.sscode ";
            sql += "  AND a.itemcode = '{0}' ";
            sql += "  AND a.ddate =" + dbDateTime.DBDate + "  ";
            sql += "  AND a.sscode = '{1}' ";
            sql += "  AND a.lotstatus = '{2}' ";
            sql += ") ";
            sql += "GROUP BY ddate, mmodelcode, bigsscode ";
            //修改成当天＋机型＋产线的累积数量
            sql = string.Format(sql, itemCode, ssCode, lotStatus);

            return this.DataProvider.CustomQuery(typeof(OQCLotCountForAlert), new SQLCondition(sql));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCLot
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLot数组</returns>
        public object[] QueryOQCLot(string lOTNO, decimal lotSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot), new PagerCondition(
                string.Format("SELECT {0} FROM TBLLOT WHERE LOTNO LIKE '{1}%'  AND LOTSEQ LIKE '{2}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot)), lOTNO, lotSequence),
                "LOTNO,LOTSEQ", inclusive, exclusive));
        }

        public int QueryOQCLotFrozenCount(string lOTNO)
        {
            string sql = "SELECT count(*) FROM tbllot ";
            sql += "WHERE 1=1 ";
            sql += "AND lotno = '" + lOTNO.Trim().ToUpper() + "' ";
            sql += "AND lotseq = " + Lot_Sequence_Default.ToString() + " ";
            sql += "AND frozenstatus = '" + FrozenStatus.STATUS_FRONZEN + "' ";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCLot
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCLot的总记录数</returns>
        public object[] GetAllOQCLot()
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(
                string.Format("SELECT {0} FROM TBLLOT ORDER BY LOTNO,LOTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot)))));
        }

        public int GetOQCLotSizeFromOQCLot2Card(string oqcLOTNO)
        {
            return this.DataProvider.GetCount(
                new SQLParamCondition(@"SELECT COUNT(*) FROM TBLLOT2CARD WHERE LOTNO =$lotno  AND LOTSEQ = $lotseq ",
                new SQLParameter[] { new SQLParameter("lotno",typeof(string),oqcLOTNO),
									   new SQLParameter("lotseq",typeof(decimal),
									   OQCFacade.Lot_Sequence_Default)}));
        }

        public void UpdateOQCLotCapacity(string lotNo, decimal lotCapacity)
        {
            DBDateTime now = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string sql = string.Empty;
            sql += "UPDATE tbllot ";
            sql += "SET lotcapacity = {0}, mdate = {1}, mtime = {2} ";
            sql += "WHERE lotno = '{3}' ";
            sql += "AND lotseq = {4} ";
            sql = string.Format(sql, lotCapacity.ToString(), now.DBDate.ToString(), now.DBTime.ToString(), lotNo, Lot_Sequence_Default.ToString());

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region OQCLot2Card
        /// <summary>
        /// 
        /// </summary>
        public OQCLot2Card CreateNewOQCLot2Card()
        {
            return new OQCLot2Card();
        }

        public void AddOQCLot2Card(OQCLot2Card oQCLot2Card)
        {
            //this._helper.AddDomainObject( oQCLot2Card );
            this.DataProvider.Insert(oQCLot2Card);
        }

        public void UpdateOQCLot2Card(OQCLot2Card oQCLot2Card)
        {
            this._helper.UpdateDomainObject(oQCLot2Card);
        }

        public void UpdateOQCLot2CardByOQCResult(string oqcLotNO, string oqcLotSequence, string moCode, string runningCard, string status, bool isReject)
        {
            if (status.Trim().Length == 0)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            string updateSql = " UPDATE TBLLOT2CARD SET STATUS='" + status + "'";
            if (isReject)
            {
                updateSql += " ,EATTRIBUTE1=''";
            }
            updateSql += " WHERE 1=1 {0}";
            string tmpString = string.Empty;
            if (oqcLotNO.Trim().Length != 0)
            {
                tmpString += " AND LOTNO ='" + oqcLotNO.ToUpper() + "'";
            }
            if (oqcLotSequence.Trim().Length != 0)
            {
                tmpString += " AND LOTSEQ =" + oqcLotSequence;
            }
            if (moCode.Trim().Length != 0)
            {
                tmpString += " AND MOCODE ='" + moCode.ToUpper() + "'";
            }
            if (runningCard.Trim().Length != 0)
            {
                tmpString += " AND RCARD='" + runningCard.ToUpper() + "'";
            }
            tmpString += " AND STATUS<>'SCRAP'";
            this.DataProvider.CustomExecute(new SQLCondition(String.Format(updateSql, tmpString)));
        }
        public void DeleteOQCLot2Card(OQCLot2Card oQCLot2Card)
        {
            this._helper.DeleteDomainObject(oQCLot2Card);
        }

        public void DeleteOQCLot2Card(OQCLot2Card[] oQCLot2Card)
        {
            this._helper.DeleteDomainObject(oQCLot2Card);
        }

        public object GetOQCLot2Card(string runningCard, string mOCode, string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLot2Card),
                new object[] { runningCard, mOCode, lOTNO, lotSequence });
        }

        #region Added By Hi1/Venus.Feng on 20080717 for Hisense Version
        public bool IsCardUsedByAnyLot(string rCard, string moCode, string itemCode)
        {
            /*
            string sql = "SELECT COUNT(*) FROM tbllot2card WHERE rcard='" + rCard.ToUpper()
                + "' AND mocode='" + moCode.ToUpper() + "' AND itemcode='" + itemCode.ToUpper() + "' "
                + "  AND lotno IN (SELECT DISTINCT lotno FROM tbllot WHERE lotstatus NOT IN ('"
                + OQCLotStatus.OQCLotStatus_Pass + "','" + OQCLotStatus.OQCLotStatus_Reject + "'))";
            */
            //允许混工单，不允许混产品
            string sql = "SELECT COUNT(*) FROM tbllot2card WHERE rcard='" + rCard.ToUpper()
                + "' AND itemcode='" + itemCode.ToUpper() + "' "
                + "  AND lotno IN (SELECT DISTINCT lotno FROM tbllot WHERE lotstatus NOT IN ('"
                + OQCLotStatus.OQCLotStatus_Pass + "','" + OQCLotStatus.OQCLotStatus_Reject + "'))";

            if (this.DataProvider.GetCount(new SQLCondition(sql)) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdateLot2CardCartonCode(string rCard, string moCode, string itemCode, string cartonCode)
        {
            string sql = "UPDATE tbllot2card SET eattribute1='" + cartonCode.ToUpper()
                + "' WHERE rcard='" + rCard.ToUpper()
                + "' AND mocode='" + moCode.ToUpper()
                + "' AND itemcode='" + itemCode.ToUpper() + "' "
                + "  AND lotno IN (SELECT DISTINCT lotno FROM tbllot WHERE lotstatus NOT IN ('"
                + OQCLotStatus.OQCLotStatus_Pass + "','" + OQCLotStatus.OQCLotStatus_Reject + "'))";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public OQCLot GetMaxLot4NormalCase(string itemCode, string ssCode, string lotType, string productionType, string markCode)
        {
            string beginLot = ssCode + "00010101" + markCode + "000";
            string endLot = ssCode + "99991231" + markCode + "999";

            string strSql = "";
            strSql += "SELECT MAX(lotno) AS LotNo";
            strSql += "  FROM tbllot";
            strSql += " WHERE lotno BETWEEN '" + beginLot + "'";
            strSql += "                 AND '" + endLot + "'";
            strSql += "   AND LENGTH(lotno)=LENGTH('" + beginLot + "')";
            strSql += "   AND sscode = '" + ssCode + "'";
            strSql += "   AND itemcode = '" + itemCode + "'";
            strSql += "   AND lotsize < lotcapacity";
            strSql += "   AND NVL(lotfrozen,' ') <> 'Y'";
            strSql += "   AND productiontype = '" + productionType + "'";
            strSql += "   AND oqclottype = '" + lotType + "'";
            strSql += "   AND lotstatus IN ('" + OQCLotStatus.OQCLotStatus_Initial + "','"
                                               + OQCLotStatus.OQCLotStatus_Examing + "','"
                                               + OQCLotStatus.OQCLotStatus_NoExame + "','"
                                               + OQCLotStatus.OQCLotStatus_SendExame + "')";
            strSql += "   AND lotno NOT IN (SELECT DISTINCT lotno";
            strSql += "                                FROM tbltry2lot)";

            object[] lot = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(strSql));
            return (OQCLot)lot[0];
        }

        public object[] GetLotList4TryCase(string itemCode, string ssCode, string lotType, string productionType, string markCode)
        {
            string beginLot = ssCode + "00010101" + markCode + "000";
            string endLot = ssCode + "99991231" + markCode + "999";

            string strSql = "";
            strSql += "SELECT {0}";
            strSql += "  FROM tbllot";
            strSql += " WHERE lotno BETWEEN '" + beginLot + "'";
            strSql += "                 AND '" + endLot + "'";
            strSql += "   AND LENGTH(lotno)=LENGTH('" + beginLot + "')";
            strSql += "   AND sscode = '" + ssCode + "'";
            strSql += "   AND itemcode = '" + itemCode + "'";
            strSql += "   AND lotsize < lotcapacity";
            strSql += "   AND NVL(lotfrozen,' ') <> 'Y'";
            strSql += "   AND productiontype = '" + productionType + "'";
            strSql += "   AND oqclottype = '" + lotType + "'";
            strSql += "   AND lotstatus IN ('" + OQCLotStatus.OQCLotStatus_Initial + "','"
                                               + OQCLotStatus.OQCLotStatus_Examing + "','"
                                               + OQCLotStatus.OQCLotStatus_NoExame + "','"
                                               + OQCLotStatus.OQCLotStatus_SendExame + "')";
            strSql += "   AND lotno IN (SELECT DISTINCT lotno";
            strSql += "                            FROM tbltry2lot)";
            strSql += " ORDER BY lotno DESC";

            strSql = string.Format(strSql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot)));

            object[] lot = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(strSql));

            return lot;
        }

        public OQCLot GetMaxLot(string sscode, int date, string markCode)
        {
            string beginLot = sscode + date.ToString() + markCode + "000";
            string endLot = sscode + date.ToString() + markCode + "999";
            int markCodeIndex = sscode.Length + 8 + 1;

            string strsql = "";
            strsql += "SELECT MAX (lotno) AS LotNo";
            strsql += "  FROM tbllot";
            strsql += " WHERE lotno BETWEEN '" + beginLot + "'";
            strsql += "                 AND '" + endLot + "'";
            strsql += "   AND LENGTH(lotno)=LENGTH('" + beginLot + "')";
            strsql += "   AND SUBSTR(lotno," + markCodeIndex + ",1)='" + markCode + "'";

            object[] lot = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(strsql));
            return (OQCLot)lot[0];
        }

        private string GetMaxLotOfSplit(string beginLotNo, string endLotNo, int markCodeIndex, string markCode)
        {
            string strSql = "";
            strSql = " SELECT MAX (lotno) AS LotNo FROM tbllot";
            strSql += " WHERE lotno BETWEEN '" + beginLotNo + "' AND '" + endLotNo + "'";
            strSql += "   AND LENGTH(lotno)=LENGTH('" + beginLotNo + "')";
            //strSql += "   AND SUBSTR(lotno," + markCodeIndex + ",1)='" + markCode + "'";
            strSql += "   AND oqclottype='" + OQCLotType.OQCLotType_Split + "'";

            object[] lot = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(strSql));

            OQCLot oqcLot = (OQCLot)lot[0];

            if (string.IsNullOrEmpty(oqcLot.LOTNO))
            {
                return beginLotNo;
            }
            else
            {
                return oqcLot.LOTNO;
            }
        }

        public OQCLot GetMaxLotForOffLot(string beginLotNo, string endLotNo, string userCode,
            OQCLot oldLot, int orgID)
        {
            DBDateTime currentDBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string strsql = "";
            string newLotNo = "";
            string ssCode = oldLot.SSCode;
            OQCLot maxLot;
            string oldLotNo = oldLot.LOTNO;

            //string markCode = oldLotNo.Substring(ssCode.Length + 8, 1);
            //int markCodeIndex = ssCode.Length + 8 + 1;
            string lotType = OQCLotType.OQCLotType_Split;

            strsql += "SELECT MAX (lotno) AS LotNo FROM tbllot";
            strsql += " WHERE LENGTH(lotno)=LENGTH('" + beginLotNo + "')";
            //strsql += "   AND SUBSTR(lotno," + markCodeIndex + ",1)='" + markCode + "'";
            strsql += "   AND oqclottype='" + OQCLotType.OQCLotType_Split + "'";
            strsql += "   AND itemcode='" + oldLot.ItemCode + "'";
            strsql += "   AND oldlotno='" + oldLotNo + "'";
            strsql += "   AND NVL(lotfrozen,' ')<>'Y'";
            strsql += "   AND lotstatus IN ('" + OQCLotStatus.OQCLotStatus_Initial + "','"
                                               + OQCLotStatus.OQCLotStatus_Examing + "','"
                                               + OQCLotStatus.OQCLotStatus_NoExame + "','"
                                               + OQCLotStatus.OQCLotStatus_SendExame + "')";

            object[] lot = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(strsql));

            OQCLot oqcLot = (OQCLot)lot[0];

            // No Record
            if (string.IsNullOrEmpty(oqcLot.LOTNO))
            {
                newLotNo = this.GetNewOQCLotNo(this.GetMaxLotOfSplit(beginLotNo, endLotNo, 0, string.Empty));
                maxLot = this.CreateNewOQCLot(newLotNo, userCode, currentDBDateTime, orgID, oldLot.LotCapacity, oldLot.SSCode, oldLot.ItemCode,
                                            oldLotNo, lotType, oldLot.ProductionType, oldLot.ResourceCode, oldLot.ShiftDay, oldLot.ShiftCode);
            }
            else  // Has Record
            {
                maxLot = this.GetOQCLot(oqcLot.LOTNO, OQCFacade.Lot_Sequence_Default) as OQCLot;
            }
            return maxLot;
        }

        private string GetNewOQCLotNo(string beginLotNo)
        {
            int countNo = Convert.ToInt32(beginLotNo.Substring(beginLotNo.Length - 2, 2)) + 1;
            string tempLot = beginLotNo.Substring(0, beginLotNo.Length - 2);
            string flowNo = countNo.ToString().PadLeft(2, '0');

            return tempLot + flowNo;
        }

        public OQCLot CreateNewOQCLot(string lotNo, string userCode, DBDateTime currentDBDateTime,
            int orgID, decimal lotCapacity, string ssCode,
            string itemCode, string oldLotNo, string lotType,
            string productionType, string resourceCode, int shiftDay, string shiftCode)
        {
            OQCLot newOQCLot = new OQCLot();

            newOQCLot.LOTNO = lotNo;
            newOQCLot.LotSequence = 0;
            newOQCLot.OQCLotType = FormatHelper.CleanString(lotType);
            newOQCLot.LotSize = 0;
            newOQCLot.SampleSize = 0;

            newOQCLot.AcceptSize = 0;
            newOQCLot.AcceptSize1 = 0;
            newOQCLot.AcceptSize2 = 0;
            newOQCLot.AcceptSize3 = 0;
            newOQCLot.AQL = 0;
            newOQCLot.AQL1 = 0;
            newOQCLot.AQL2 = 0;
            newOQCLot.AQL3 = 0;
            newOQCLot.RejectSize = 0;
            newOQCLot.RejectSize1 = 0;
            newOQCLot.RejectSize2 = 0;
            newOQCLot.RejectSize3 = 0;

            newOQCLot.LOTStatus = OQCLotStatus.OQCLotStatus_Initial;
            newOQCLot.LOTTimes = 0;
            newOQCLot.MaintainUser = userCode;
            newOQCLot.MaintainDate = currentDBDateTime.DBDate;
            newOQCLot.MaintainTime = currentDBDateTime.DBTime;
            newOQCLot.EAttribute1 = "";
            //newOQCLot.DealDate = 0;
            //newOQCLot.DealTime = 0;
            //newOQCLot.DealUser = "";
            newOQCLot.ProductionType = productionType;
            newOQCLot.OldLotNo = oldLotNo;
            newOQCLot.OrganizationID = orgID;
            newOQCLot.LotCapacity = lotCapacity;
            //newOQCLot.LotFrozen = "";
            //newOQCLot.Memo = "";

            newOQCLot.CreateUser = userCode;
            newOQCLot.CreateDate = currentDBDateTime.DBDate;
            newOQCLot.CreateTime = currentDBDateTime.DBTime;

            newOQCLot.SSCode = ssCode;
            newOQCLot.ItemCode = itemCode;

            //newOQCLot.FrozenStatus = "";
            //newOQCLot.FrozenReason = "";
            //newOQCLot.FrozenDate = 0;
            //newOQCLot.FrozenTime = 0;
            //newOQCLot.FrozenBy = "";

            //newOQCLot.UnFrozenReason = "";
            //newOQCLot.UnFrozenDate = 0;
            //newOQCLot.UnFrozenTime = 0;
            //newOQCLot.UnFrozenBy = "";
            newOQCLot.ResourceCode = resourceCode;
            newOQCLot.ShiftDay = shiftDay;
            newOQCLot.ShiftCode = shiftCode;

            return newOQCLot;
        }

        public void UpdateOQCLot2CardLotNo(string oldLotNo, string rCard, int lotSeq, string moCode, string newLotNo)
        {
            string sql = "UPDATE tbllot2card SET lotno='" + newLotNo
                       + "' WHERE rcard='" + rCard + "' AND lotno='" + oldLotNo
                       + "' AND lotseq=" + lotSeq + " AND mocode='" + moCode + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateOQCLotSizeForOffLot(string oldLotNo, string newLotNo, int lotSeq)
        {
            string sqlNewLot = "UPDATE tbllot SET lotsize=lotsize+1 WHERE lotno='" + newLotNo + "' AND lotseq=" + lotSeq;
            string sqlOldLot = "UPDATE tbllot SET lotsize=lotsize-1 WHERE lotno='" + oldLotNo + "' AND lotseq=" + lotSeq;

            this.DataProvider.CustomExecute(new SQLCondition(sqlNewLot));
            this.DataProvider.CustomExecute(new SQLCondition(sqlOldLot));
        }

        public object[] GetOQCLot2CardByLotNoAndSeq(string lotNo, decimal lotSeq)
        {
            string sql = "SELECT {0} FROM tbllot2card WHERE lotno='" + lotNo + "' and lotseq=" + lotSeq;
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card),
                new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)))));
        }

        public object GetOQCLotByOldLotNo(string oldLotNo)
        {
            string sql = "SELECT {0} FROM tbllot WHERE oldlotno = '" + oldLotNo
                                               + "' AND lotsize < lotcapacity"
                                               + "  AND NVL(lotfrozen,' ') <> 'Y'"
                                               + "  AND oqclottype <> '" + OQCLotType.OQCLotType_Split + "'"
                                               + "  AND lotstatus IN ('"
                                               + OQCLotStatus.OQCLotStatus_Initial + "', '"
                                               + OQCLotStatus.OQCLotStatus_Examing + "', '"
                                               + OQCLotStatus.OQCLotStatus_NoExame + "', '"
                                               + OQCLotStatus.OQCLotStatus_SendExame + "')";

            object[] lots = this.DataProvider.CustomQuery(typeof(OQCLot),
                new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot)))));

            if (lots != null && lots.Length > 0)
            {
                return lots[0];
            }
            else
            {
                return null;
            }
        }

        #endregion

        public object GetLastOQCLot2CardByRCard(string runningCard)
        {
            string sql = "SELECT {0} FROM tbllot2card WHERE rcard = '{1}' ORDER BY mdate DESC,mtime DESC ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)), runningCard);
            object[] lot2RcardList = this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(sql));
            if (lot2RcardList == null)
            {
                return null;
            }

            return lot2RcardList[0];
        }
        /// <summary>
        /// 这段代码有问题
        /// </summary>
        /// <param name="runningCard"></param>
        /// <param name="moCode"></param>
        /// <param name="lotNO"></param>
        /// <param name="lotsequence"></param>
        /// <returns></returns>
        public bool IsCardUsedByAnotherLot(string runningCard, string moCode, string lotNO, decimal lotsequence)
        {
            //Nanjing  CrystalChu  20050729  修改
            //Nanjing End
            string selectSql = "SELECT COUNT(DISTINCT(LOTNO)) FROM TBLLOT WHERE LOTNO IN "
                + " (SELECT LOTNO FROM TBLLOT2CARD WHERE RCARD=$rcard AND MOCODE =$mocode) AND LOTSTATUS IN ( "
                //             +"'"+OQCLotStatus.OQCLotStatus_Pass+"','"+OQCLotStatus.OQCLotStatus_Reject+"')";
                + "'" + OQCLotStatus.OQCLotStatus_Examing + "','" + OQCLotStatus.OQCLotStatus_Initial
                + "','" + OQCLotStatus.OQCLotStatus_NoExame + "')";
            if (this.DataProvider.GetCount(new SQLParamCondition(
                selectSql, new SQLParameter[] {new SQLParameter("rcard",typeof(string),runningCard),
												 new SQLParameter("mocode",typeof(string),moCode) })) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ** 功能描述:	查询OQCLot2Card的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCLot2Card的总记录数</returns>
        public int QueryOQCLot2CardCount(string runningCard, string mOCode, string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(
                string.Format("SELECT COUNT(*) FROM TBLLOT2CARD WHERE RCARD LIKE '{0}%'  AND MOCODE LIKE '{1}%'  AND LOTNO LIKE '{2}%'  AND LOTSEQ LIKE '{3}%' ", runningCard, mOCode, lOTNO, lotSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCLot2Card
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLot2Card数组</returns>
        public object[] QueryOQCLot2Card(string runningCard, string mOCode, string lOTNO, decimal lotSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new PagerCondition(
                string.Format("SELECT {0} FROM TBLLOT2CARD WHERE RCARD LIKE '{1}%'  AND MOCODE LIKE '{2}%'  AND LOTNO LIKE '{3}%'  AND LOTSEQ LIKE '{4}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)), runningCard, mOCode, lOTNO, lotSequence), "RCARD,MOCODE,LOTNO,LOTSEQ", inclusive, exclusive));
        }

        public object GetOQCLot2Card(string runningCard, string moCode, string lotNo, string lotSequence)
        {
            string sql = string.Format("SELECT {0} FROM tbllot2card WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)));
            if (runningCard.Trim().Length != 0)
            {
                sql += "  and rcard ='" + runningCard.ToUpper() + "'";
            }
            if (lotNo.Trim().Length != 0)
            {
                sql += " and lotno='" + lotNo.ToUpper() + "'";
            }
            if (lotSequence.Trim().Length != 0)
            {
                sql += " and lotseq='" + lotSequence.ToUpper() + "'";
            }
            if (moCode.Trim().Length != 0)
            {
                sql += " and mocode ='" + moCode.ToUpper() + "'";
            }

            sql += "order by mdate desc,mtime desc";

            object[] lot2RcardList = this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(sql));
            if (lot2RcardList == null)
            {
                return null;
            }

            return lot2RcardList[0];
        }

        public object[] QueryOQCLot2Card(string runningCard)
        {
            string sql = "SELECT {0} FROM tbllot2card WHERE rcard LIKE '{1}%' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)), runningCard);
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(sql));
        }

        public object[] ExactQueryOQCLot2Card(string runningCard)
        {
            string sql = "SELECT {0} FROM tbllot2card WHERE rcard = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)), runningCard);
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(sql));
        }

        public object[] QueryOQCLot2CardByLotNo(string lotNo)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new PagerCondition(
                string.Format("SELECT {0} FROM TBLLOT2CARD WHERE LOTNO = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)), lotNo.Trim().ToUpper()), "RCARD", int.MinValue, int.MaxValue));
        }

        public int QueryOQCLot2CardCountByLotNo(string lotNo)
        {
            return this.DataProvider.GetCount(new SQLCondition(
                string.Format("SELECT Count(*) FROM TBLLOT2CARD WHERE LOTNO = '{0}' ", lotNo.Trim().ToUpper())));
        }

        public object[] ExactQueryOQCLot2Card(string lOTNO, decimal lotSequence)
        {
            //			return this.DataProvider.CustomQuery(typeof(OQCLot2Card), 
            //				new PagerCondition(string.Format("select {0} from TBLLOT2CARD where 1=1 and LOTNO = '{1}'  and LOTSEQ = '{2}' ",
            //				DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)) , lOTNO,lotSequence), 
            //                "RCARD,MOCODE,LOTNO,LOTSEQ",int.MinValue, int.MaxValue)); 
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card),
                new SQLParamCondition(
                string.Format("SELECT {0} FROM TBLLOT2CARD WHERE LOTNO = $lotno  AND LOTSEQ = $lotseq "
                + " order by mdate*1000000+mtime desc",// RCARD,MOCODE,LOTNO,LOTSEQ ", 
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card))),
                new SQLParameter[]{new SQLParameter("lotno",typeof(string),lOTNO),
									  new SQLParameter("lotseq",typeof(decimal),lotSequence)}
                ));
        }


        public object[] QueryMoCodeINOLot2CardByLotNo(string lotNo)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(
                string.Format("SELECT distinct mocode FROM TBLLOT2CARD WHERE LOTNO = '" + lotNo.Trim().ToUpper() + "'")));
        }


        public int ExactQueryCountOQCLot2Card(string lotNO, decimal lotSequence)
        {
            return this.DataProvider.GetCount(
                new SQLParamCondition(@"SELECT COUNT(*) FROM TBLLOT2CARD WHERE  "
                + "LOTNO = $lotno AND LOTSEQ = $lotseq ",
                new SQLParameter[]{new SQLParameter("lotno",typeof(string),lotNO),
									  new SQLParameter("lotseq",typeof(decimal),lotSequence)}));
        }
        /// <summary>
        /// 此段SQL需要进一步优化
        /// </summary>
        /// <param name="lotNo"></param>
        /// <param name="lotSequence"></param>
        /// <returns></returns>
        public object[] GetUnExameOQCLot2Card(string lotNo, decimal lotSequence)
        {
            //			return this.DataProvider.CustomQuery(typeof(OQCLot2Card), 
            //				new PagerCondition(string.Format(
            //                    "SELECT {0} FROM TBLLOT2CARD WHERE LOTNO = '{1}'  AND LOTSEQ = '{2}'"
            //				    +" AND RCARD NOT IN(SELECT RCARD FROM TBLLOT2CARDCHECK  WHERE LOTNO = '{1}'  AND LOTSEQ = '{2}') ",
            //				DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)) , lotNo,lotSequence), 
            //                "RCARD,MOCODE,LOTNO,LOTSEQ",int.MinValue, int.MaxValue)); 
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card),
                new SQLParamCondition(
                string.Format("SELECT {0} FROM TBLLOT2CARD WHERE LOTNO = $lotno  AND LOTSEQ = $lotseq "
                + " AND RCARD NOT IN(SELECT RCARD FROM TBLLOT2CARDCHECK  WHERE LOTNO = $lotno1  AND LOTSEQ =$lotseq1 ) "
                ,
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card))),
                new SQLParameter[]{new SQLParameter("lotno",typeof(string),lotNo),
									  new SQLParameter("lotseq",typeof(decimal),lotSequence),
									  new SQLParameter("lotno1",typeof(string),lotNo),
									  new SQLParameter("lotseq1", typeof(decimal),lotSequence)}
                ));
        }

        public object[] GetOQCLot2CardInTS(string runningCard, string modelCode, string itemCode, string moCode, string lotNO, string lotSequence)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card))
                + " from tbllot2card where Rcard in ("
                + "  select rcard from tblts where "
                + " tsstatus <> '" + TSStatus.TSStatus_New + "'"
                + " and tsstatus <>'" + TSStatus.TSStatus_Complete + "'"
                + " and tsstatus <>'" + TSStatus.TSStatus_Reflow + "'"
                + " and tsstatus <>'" + TSStatus.TSStatus_Scrap + "'"
                + " and tsstatus <>'" + TSStatus.TSStatus_Split + "'"
                + " and tsstatus <> '" + TSStatus.TSStatus_RepeatNG + "'"
                + "{0} ) {1}";
            string tmpString = string.Empty;
            string tmpString1 = string.Empty;
            if (runningCard.Trim().Length != 0)
            {
                tmpString += "  and rcard ='" + runningCard.ToUpper() + "'";
            }
            if (modelCode.Trim().Length != 0)
            {
                tmpString += " and modelcode='" + modelCode.ToUpper() + "'";
            }
            if (itemCode.Trim().Length != 0)
            {
                tmpString += " and itemcode='" + itemCode.ToUpper() + "'";
            }
            if (moCode.Trim().Length != 0)
            {
                tmpString += " and mocode ='" + moCode.ToUpper() + "'";
            }
            if (lotNO.Trim().Length != 0)
            {
                tmpString1 += " and lotno ='" + lotNO.ToUpper() + "'";
            }
            if (lotSequence.Trim().Length != 0)
            {
                tmpString1 += " and lotseq =" + lotSequence;
            }
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(String.Format(selectSql, tmpString, tmpString1)));
        }
        //Laws Lu,2005/08/25,新增	获取所有在TS中的RunningCard
        public object[] GetAllOQCLot2CardInTS(string runningCard, string modelCode, string itemCode, string moCode, string lotNO, string lotSequence)
        {
            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card))
                + " FROM TBLLOT2CARD WHERE RCARD IN ("
                + "  SELECT RCARD FROM TBLTS WHERE 1=1 {0}) {1}";
            string tmpString = string.Empty;
            string tmpString1 = string.Empty;
            if (runningCard.Trim().Length != 0)
            {
                tmpString += "  AND RCARD ='" + runningCard.ToUpper() + "'";
            }
            if (modelCode.Trim().Length != 0)
            {
                tmpString += " AND MODELCODE='" + modelCode.ToUpper() + "'";
            }
            if (itemCode.Trim().Length != 0)
            {
                tmpString += " AND ITEMCODE='" + itemCode.ToUpper() + "'";
            }
            if (moCode.Trim().Length != 0)
            {
                tmpString += " AND MOCODE ='" + moCode.ToUpper() + "'";
            }
            if (lotNO.Trim().Length != 0)
            {
                tmpString1 += " AND LOTNO ='" + lotNO.ToUpper() + "'";
            }
            if (lotSequence.Trim().Length != 0)
            {
                tmpString1 += " AND LOTSEQ =" + lotSequence;
            }
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(String.Format(selectSql, tmpString, tmpString1)));
        }



        /// <summary>
        /// ** 功能描述:	获得所有的OQCLot2Card
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCLot2Card的总记录数</returns>
        public object[] GetAllOQCLot2Card()
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card),
                new SQLCondition(string.Format("SELECT {0} FROM TBLLOT2CARD ORDER BY RCARD,MOCODE,LOTNO,LOTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)))));
        }

        public int GetUnSelectOQCLot2Card(string oqcLotNO, string moCode, string itemCode)
        {
            string SelectSql = "SELECT COUNT(ITEMCODE)   FROM TBLLOT2CARD WHERE 1=1 {0}";
            string tmpString = string.Empty;
            ArrayList arraylist = new ArrayList();
            if ((moCode != string.Empty) || (moCode.Trim() != string.Empty))
            {
                tmpString += "  AND MOCODE <> $mocode";
                arraylist.Add(new SQLParameter("mocode", typeof(string), moCode.Trim()));
            }
            if ((itemCode != string.Empty) || (itemCode.Trim() != string.Empty))
            {
                tmpString += "  AND ITEMCODE <> $itemcode";
                arraylist.Add(new SQLParameter("itemcode", typeof(string), itemCode.Trim()));
            }
            if (oqcLotNO.Trim().Length != 0)
            {
                tmpString += " AND LOTNO=$lotno";
                arraylist.Add(new SQLParameter("lotno", typeof(string), oqcLotNO.Trim()));
            }
            return this.DataProvider.GetCount(new SQLParamCondition(String.Format(SelectSql, tmpString), (SQLParameter[])arraylist.ToArray(typeof(SQLParameter))));
        }

        public object[] GetOQCLot2CardByLotNo(string lotNo, decimal lotSeq, string status)
        {
            string sql = "SELECT {0} FROM tbllot2card WHERE 1=1";
            if (lotNo.Trim() != string.Empty)
            {
                sql += " and lotNo='" + lotNo + "'";
            }
            if (lotSeq > 0)
            {
                sql += " and lotseq=" + lotSeq;
            }
            if (status.Trim() != string.Empty)
            {
                sql += "and status ='" + status + "'";
            }
            return this.DataProvider.CustomQuery(typeof(OQCLot2Card),
                new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)))));
        }

        #endregion

        #region OQCLot2CardCheck
        /// <summary>
        /// 
        /// </summary>
        public OQCLot2CardCheck CreateNewOQCLot2CardCheck()
        {
            return new OQCLot2CardCheck();
        }

        public void AddOQCLot2CardCheck(OQCLot2CardCheck oQCLot2CardCheck)
        {
            //this._helper.AddDomainObject( oQCLot2CardCheck );
            this.DataProvider.Insert(oQCLot2CardCheck);
        }

        private int GetOQCLot2CardSequence(OQCLot2CardCheck oQCLot2CardCheck)
        {
            object[] objs = ExtraQueryOQCLot2CardCheck(oQCLot2CardCheck.RunningCard, string.Empty, oQCLot2CardCheck.MOCode, oQCLot2CardCheck.LOTNO, oQCLot2CardCheck.LotSequence.ToString());
            if (objs == null)
            {
                return RunningCard_Sequence_Default;
            }
            else
            {
                return objs.Length;
            }

        }

        private int GetOQCLot2CardSequence(string runningCard, string moCode, string lotNO, string lotSequence)
        {
            object[] objs = ExtraQueryOQCLot2CardCheck(runningCard, string.Empty, moCode, lotNO, lotSequence);
            if (objs == null)
            {
                return RunningCard_Sequence_Default;
            }
            else
            {
                return objs.Length;
            }
        }

        public void UpdateOQCLot2CardCheck(OQCLot2CardCheck oQCLot2CardCheck)
        {
            this._helper.UpdateDomainObject(oQCLot2CardCheck);
        }

        public void DeleteOQCLot2CardCheck(OQCLot2CardCheck oQCLot2CardCheck)
        {
            this._helper.DeleteDomainObject(oQCLot2CardCheck,
                                new ICheck[]{ new DeleteAssociateCheck( oQCLot2CardCheck,
														this.DataProvider, 
														new Type[]{
																typeof(OQCLOTCardCheckList),
																typeof(OQCLotCard2ErrorCode)	})});
        }

        public void DeleteOQCLot2CardCheck(OQCLot2CardCheck[] oQCLot2CardCheck)
        {
            this._helper.DeleteDomainObject(oQCLot2CardCheck,
                                new ICheck[]{ new DeleteAssociateCheck( oQCLot2CardCheck,
														this.DataProvider,
                                                        new Type[]{
																typeof(OQCLOTCardCheckList),
																typeof(OQCLotCard2ErrorCode)	})});
        }

        public object GetOQCLot2CardCheck(string runningCard, decimal runningCardSequence, string mOCode, string lOTNO, decimal lotSequence, decimal checkSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLot2CardCheck),
                new object[] { runningCard, runningCardSequence, mOCode, lOTNO, lotSequence, checkSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCLot2CardCheck的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCLot2CardCheck的总记录数</returns>
        public int QueryOQCLot2CardCheckCount(string runningCard, decimal runningCardSequence, string mOCode, string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format(
               "SELECT COUNT(*) FROM TBLLOT2CARDCHECK WHERE RCARD LIKE '{0}%'  AND RCARDSEQ LIKE '{1}%'  AND MOCODE LIKE '{2}%'  AND LOTNO LIKE '{3}%'  AND LOTSEQ LIKE '{4}%' ", runningCard, runningCardSequence, mOCode, lOTNO, lotSequence)));
        }

        //取得样本数量
        public int ExactQueryOQCLot2CardCheckCount(string lOTNO, decimal lotSequence)
        {
            string selectSql = "SELECT count(*) FROM (select distinct rcard from TBLLOT2CARDCHECK WHERE "
                                + " LOTNO = '" + lOTNO.ToUpper() + "' "
                                + " AND  LOTSEQ = " + lotSequence
                                + " )";

            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }

        //取得样本NG数量
        public int ExactQueryOQCLot2CardCheckNgCount(string lOTNO, decimal lotSequence)
        {
            string selectSql = @"SELECT COUNT(*)
                                  FROM TBLLOT2CARDCHECK
                                 INNER JOIN (SELECT RCARD, MAX(CHECKSEQ) CHECKSEQ
                                               FROM TBLLOT2CARDCHECK
                                              WHERE LOTNO = '{0}'
                                                AND LOTSEQ = {1}
                                              GROUP BY RCARD) CT
                                    ON CT.RCARD = TBLLOT2CARDCHECK.RCARD
                                   AND CT.CHECKSEQ = TBLLOT2CARDCHECK.CHECKSEQ
                                 WHERE STATUS = 'NG'";
            selectSql = string.Format(selectSql, lOTNO, lotSequence);

            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }

        //取得数据连线数量
        public int GetLotDataLinkCardCount(string lOTNO, decimal lotSequence)
        {
            //			string selectSql = "SELECT count(*) FROM (select distinct rcard from TBLLOT2CARDCHECK WHERE "
            //				+ " LOTNO = '"+lOTNO.ToUpper()+"' "
            //				+ " AND  LOTSEQ = "+lotSequence
            //				+ " and isdatalink='" + BenQGuru.eMES.Web.Helper.FormatHelper.TRUE_STRING + "'"
            //				+ " )";

            string selectSql = "SELECT count(*) FROM (select distinct rcard from tbloqcfunctestvalue WHERE "
                            + " LOTNO = '" + lOTNO.ToUpper() + "' "
                            + " AND  LOTSEQ = " + lotSequence
                            + " )";

            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }

        //取得尺寸数量
        public int GetLotDimentionCount(string lOTNO, decimal lotSequence)
        {
            string selectSql = "SELECT count(*) FROM (select distinct rcard from tbloqcdim WHERE "
                + " LOTNO = '" + lOTNO.ToUpper() + "' "
                + " AND  LOTSEQ = " + lotSequence
                + " )";

            return this.DataProvider.GetCount(new SQLCondition(selectSql));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCLot2CardCheck
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLot2CardCheck数组</returns>
        public object[] QueryOQCLot2CardCheck(string runningCard, decimal runningCardSequence, string mOCode, string lOTNO, decimal lotSequence, int inclusive, int exclusive)
        {
            if (lOTNO == string.Empty || lOTNO.IndexOf(",") < 0)
            {
                return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheck), new PagerCondition(
                    string.Format("SELECT {0} FROM TBLLOT2CARDCHECK WHERE RCARD LIKE '{1}%'  AND RCARDSEQ LIKE '{2}%'  AND MOCODE LIKE '{3}%'  AND LOTNO LIKE '{4}%'  AND LOTSEQ LIKE '{5}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2CardCheck)), runningCard, runningCardSequence, mOCode, lOTNO, lotSequence), "RCARD,RCARDSEQ,MOCODE,LOTNO,LOTSEQ", inclusive, exclusive));
            }
            else
            {
                return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheck), new PagerCondition(
                    string.Format("SELECT {0} FROM TBLLOT2CARDCHECK WHERE RCARD LIKE '{1}%'  AND RCARDSEQ LIKE '{2}%'  AND MOCODE LIKE '{3}%'  AND LOTNO IN ('{4}')  AND LOTSEQ LIKE '{5}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2CardCheck)), runningCard, runningCardSequence, mOCode, lOTNO.Replace(",", "','"), lotSequence), "RCARD,RCARDSEQ,MOCODE,LOTNO,LOTSEQ", inclusive, exclusive));
            }
        }

        /// <summary>
        /// 精确查询
        /// </summary>
        /// <param name="runningCard">可为空，不包括次过滤项目</param>
        /// <param name="runningCardSequence">可为空，不包括次过滤项目</param>
        /// <param name="mOCode">可为空，不包括次过滤项目</param>
        /// <param name="lOTNO">可为空，不包括次过滤项目</param>
        /// <param name="lotSequence">可为空，不包括次过滤项目</param>
        /// <returns></returns>
        public object[] ExtraQueryOQCLot2CardCheck(string runningCard, string runningCardSequence, string moCode, string lOTNO, string lotSequence)
        {
            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2CardCheck))
                + " FROM TBLLOT2CARDCHECK WHERE 1=1 {0} order by mdate desc,mtime desc,rcard";
            string tmpString = string.Empty;
            if (runningCard.Trim().Length != 0)
            {
                tmpString += " AND RCARD = '" + runningCard.ToUpper() + "' ";
            }
            if (runningCardSequence.Trim().Length != 0)
            {
                tmpString += " AND  RCARDSEQ = " + runningCardSequence;
            }
            if (moCode.Trim().Length != 0)
            {
                tmpString += " AND  MOCODE = '" + moCode.ToUpper() + "' ";
            }
            if (lOTNO.Trim().Length != 0)
            {
                if (lOTNO.IndexOf(",") < 0)
                    tmpString += " AND  LOTNO = '" + lOTNO.ToUpper() + "' ";
                else
                    tmpString += " AND LOTNO IN ('" + lOTNO.ToUpper().Replace(",", "','") + "') ";
            }
            if (lotSequence.Trim().Length != 0)
            {
                tmpString += " AND  LOTSEQ = " + lotSequence;
            }

            return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheck), new PagerCondition(string.Format(selectSql, tmpString), "mdate desc,mtime desc,rcard", 0, int.MaxValue));
        }


        public object[] QueryOQCLot2CardCheckLastRecord(string runningCard, string runningCardSequence, string moCode, string lotNO, string lotSequence, string status)
        {
            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2CardCheck)) + " FROM TBLLOT2CARDCHECK A"
                //                             +"  WHERE EXISTS(SELECT 1"
                //                             +" FROM (SELECT MAX(MDATE * 1000000 + MTIME) AS MAXDTTIME,"
                //                             +" RCARD,LOTNO,MOCODE FROM TBLLOT2CARDCHECK"
                //                             +" WHERE lotno='" + lotNO + "' and status='" + status + "'and rcardseq = (select max(rcardseq)"
                                 + " WHERE  rcardseq = (select max(rcardseq)"
                             + " from tbllot2cardcheck B where B.RCARD=A.RCARD and A.STatus=B.Status and B.LOTno = A.lotno "
                //							 +" and lotno='" + lotNO + "' and status='" + status +  "' {0}) {1} ";
                             + " {0}) {1} ";
            //Laws Lu,2005/10/24,注释	执行错误
            //                             +" GROUP BY RCARD, LOTNO, MOCODE"
            //							 +" ) B"
            //                             +" WHERE A.RCARD = B.RCARD AND A.LOTNO = B.LOTNO AND"
            //Karron Qiu,2005-10-24 修改。序列号按照检验的时间排序
            //Laws Lu,2005/10/24,注释	无需排序
            //                             +" A.MOCODE = B.MOCODE AND MDATE * 1000000 + MTIME = B.MAXDTTIME) order by MDATE ,MTIME ,RCARD";
            //							 +" A.MOCODE = B.MOCODE AND MDATE * 1000000 + MTIME = B.MAXDTTIME)";
            string tmpString = string.Empty;

            if (runningCard.Trim().Length != 0)
            {
                tmpString += " AND RCARD ='" + runningCard.ToUpper() + "'";
            }
            if (runningCardSequence.Trim().Length != 0)
            {
                tmpString += " AND RCARDSEQ =" + runningCardSequence;
            }
            if (moCode.Trim().Length != 0)
            {
                tmpString += " AND MOCODE ='" + moCode.ToUpper() + "'";
            }
            if (lotNO.Trim().Length != 0)
            {
                tmpString += " AND LOTNO ='" + lotNO.ToUpper() + "'";
            }
            if (lotSequence.Trim().Length != 0)
            {
                tmpString += " AND LOTSEQ =" + lotSequence;
            }
            if (status.Trim().Length != 0)
            {
                tmpString += " AND STATUS ='" + status + "' ";
            }

            return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheck), new SQLCondition(String.Format(selectSql, tmpString, tmpString)));
        }

        //		//功能测试的RCard的最后一条记录
        //		public object[] QueryFuncTestLastRecord(string lotNO,string lotSequence,string status)
        //		{
        //			string selectSql = "SELECT "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCFuncTestValue))+" FROM tbloqcfunctestvalue A"
        //				+" WHERE  rcardseq = (select max(rcardseq)"
        //				+" from tbloqcfunctestvalue B where B.RCARD=A.RCARD and A.ProductStatus=B.ProductStatus and B.LOTno = A.lotno "
        //				+" {0}) {1} ";
        //			
        //			string tmpString = string.Empty;
        //			if( lotNO.Trim().Length != 0)
        //			{
        //				tmpString += " AND LOTNO ='"+lotNO.ToUpper()+"'";
        //			}
        //			if(lotSequence.Trim().Length != 0)
        //			{
        //				tmpString += " AND LOTSEQ ="+lotSequence;
        //			}
        //			if( status.Trim().Length != 0)
        //			{
        //				tmpString += " AND ProductStatus ='"+status+"' ";
        //			}
        //
        //			return this.DataProvider.CustomQuery(typeof(OQCFuncTestValue), new SQLCondition(String.Format(selectSql,tmpString,tmpString)));
        //		}
        //功能测试的RCard的最后一条记录不良数
        public int QueryFuncTesCount(string lotNO, string lotSequence, string status)
        {
            string selectSql = "SELECT count(*) FROM tbloqcfunctestvalue A"
                + " WHERE  rcardseq = (select max(rcardseq)"
                + " from tbloqcfunctestvalue B where B.RCARD=A.RCARD and A.ProductStatus=B.ProductStatus and B.LOTno = A.lotno "
                + " {0}) {1} ";

            string tmpString = string.Empty;
            if (lotNO.Trim().Length != 0)
            {
                tmpString += " AND LOTNO ='" + lotNO.ToUpper() + "'";
            }
            if (lotSequence.Trim().Length != 0)
            {
                tmpString += " AND LOTSEQ =" + lotSequence;
            }
            if (status.Trim().Length != 0)
            {
                tmpString += " AND ProductStatus ='" + status + "' ";
            }

            return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql, tmpString, tmpString)));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCLot2CardCheck
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCLot2CardCheck的总记录数</returns>
        public object[] GetAllOQCLot2CardCheck()
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheck),
                new SQLCondition(string.Format("SELECT {0} FROM TBLLOT2CARDCHECK ORDER BY RCARD,RCARDSEQ,MOCODE,LOTNO,LOTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2CardCheck)))));
        }

        /// <summary>
        /// Update tbloqclot2card.lotno for offlot
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="rcard"></param>
        /// <param name="oldLotNo"></param>
        /// <param name="newLotNo"></param>
        /// <param name="lotSequence"></param>
        public void UpdateOQCLot2CardForOffLot(string moCode, string rcard, string oldLotNo, string newLotNo, int lotSequence)
        {
            string sql = "UPDATE tbllot2cardcheck SET lotno='" + newLotNo + "' WHERE mocode='" + moCode
                + "' AND rcard='" + rcard + "' AND lotno='" + oldLotNo + "' AND lotseq=" + lotSequence;

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #region BS 查询

        /// <summary>
        /// ** 功能描述:	分页查询OQCLot2CardCheck
        /// ** 日 期:		2006-01-11 
        /// ** 修 改:
        /// ** 日 期:

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemCode">产品代码</param>
        /// <param name="moCode">工单代码</param>
        /// <param name="lOTNO">送检批号</param>
        /// <param name="runningCard">产品序列号</param>
        /// <param name="oqcBeginDate">抽检开始日期</param>
        /// <param name="oqcEndDate">抽检结束日期</param>
        /// <returns></returns>
        public object[] QueryOQCLot2CardCheck(string itemCode,
            string moCode,
            string lOTNO,
            string startsn, string endsn,
            string ssCode,
            string bigLine, string materialModelCode,
            string oqcLotType, string reWorkMO,
            string productiontype, string CrewCode,
            string mUser,
            int oqcBeginDate, int oqcBeginTime,
            int oqcEndDate, int oqcEndTime,
            int inclusive, int exclusive)
        {
            //产品,工单,批号,产品序列号,抽检日期
            string itemCodition = string.Empty;
            if (itemCode.Trim().Length > 0 && itemCode != null)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] lists = itemCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCode = string.Join(",", lists);
                    itemCodition += string.Format(@" and TBLLOT2CARDCHECK.itemcode in ({0})", itemCode.ToUpper());
                }
                else
                {
                    itemCodition += string.Format(@" and TBLLOT2CARDCHECK.itemcode like '{0}%'", itemCode.ToUpper());
                }
            }

            string moCodition = string.Empty;
            if (moCode.Trim().Length > 0 && moCode != null)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    moCodition += string.Format(@" and TBLLOT2CARDCHECK.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    moCodition += string.Format(@" and TBLLOT2CARDCHECK.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            string lotCodition = string.Empty;
            if (lOTNO != null && lOTNO.Trim().Length > 0)
            {
                if (lOTNO.IndexOf(",") >= 0)
                {
                    string[] lists = lOTNO.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    lOTNO = string.Join(",", lists);
                    lotCodition += string.Format(@" and TBLLOT.LOTNO in ({0})", lOTNO.ToUpper());
                }
                else
                {
                    lotCodition += string.Format(@" and TBLLOT.LOTNO like '{0}%'", lOTNO.ToUpper());
                }
            }

            string rcardCodition = string.Empty;
            if (startsn != null && startsn != string.Empty)
            {
                rcardCodition = FormatHelper.GetCodeRangeSql("TBLLOT2CARDCHECK.rcard", startsn, endsn);
            }

            string sscodeCondition = string.Empty;
            if (ssCode != null && ssCode.Trim().Length > 0)
            {
                if (ssCode.IndexOf(",") >= 0)
                {
                    string[] lists = ssCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    ssCode = string.Join(",", lists);
                    sscodeCondition += string.Format(@" and tblss.sscode in ({0})", ssCode.ToUpper());
                }
                else
                {
                    sscodeCondition += string.Format(@" and tblss.sscode like '{0}%'", ssCode.ToUpper());
                }
            }

            string bigsscodeCondition = string.Empty;
            if (bigLine.Trim().Length > 0)
            {
                if (bigLine.IndexOf(",") >= 0)
                {
                    string[] lists = bigLine.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigLine = string.Join(",", lists);

                    bigsscodeCondition += string.Format(" and tblss.bigsscode in ({0})", bigLine);
                }
                else
                {
                    bigsscodeCondition += string.Format(" and tblss.bigsscode like '{0}%'", bigLine.ToUpper());
                }
            }

            string mmodelcodeCondition = string.Empty;
            if (materialModelCode.Trim().Length > 0)
            {
                if (materialModelCode.IndexOf(",") >= 0)
                {
                    string[] lists = materialModelCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    materialModelCode = string.Join(",", lists);
                    mmodelcodeCondition += string.Format(" and tblmaterial.mmodelcode in ({0})", materialModelCode);
                }
                else
                {
                    mmodelcodeCondition += string.Format(" and tblmaterial.mmodelcode like '{0}%'", materialModelCode.ToUpper());
                }
            }
            string productionTypeCondition = string.Empty;
            if (productiontype.Trim().Length > 0)
            {
                if (productiontype.IndexOf(",") >= 0)
                {
                    string[] lists = productiontype.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    productiontype = string.Join(",", lists);
                    productionTypeCondition += string.Format(" and TBLLOT.productiontype in ({0})", productiontype);
                }
                else
                {
                    productionTypeCondition += string.Format(" and TBLLOT.productiontype like '{0}%'", productiontype);
                }
            }

            string OQCLotTypeCondition = string.Empty;
            if (oqcLotType.Trim().Length > 0)
            {
                if (oqcLotType.IndexOf(",") >= 0)
                {
                    string[] lists = oqcLotType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    oqcLotType = string.Join(",", lists);
                    OQCLotTypeCondition += string.Format(" and TBLLOT.oqcLotType in ({0})", oqcLotType);
                }
                else
                {
                    OQCLotTypeCondition += string.Format(" and TBLLOT.oqcLotType like '{0}%'", oqcLotType);
                }
            }

            string reWorkMOCondition = string.Empty;
            if (reWorkMO.Trim().Length > 0)
            {
                reWorkMOCondition += string.Format(" and tblreworksheet.reworkcode like '{0}%'", reWorkMO.ToUpper());
            }

            string mUserCondition = string.Empty;
            if (mUser.Trim().Length > 0)
            {
                mUserCondition += string.Format(" and tbloqccardlotcklist.muser like  '{0}%'", mUser.ToUpper());
            }

            string CrewCodeCondition = string.Empty;
            if (CrewCode.Trim().Length > 0)
            {
                //OQC抽检明细样本的查询的班组(TBLRES-TBLCREW)，修改成从(TBLLine2Crew- TBLCREW)中得到
                //CrewCodeCondition += string.Format(" and tblres.crewcode like  '{0}%'", CrewCode.ToUpper());
                CrewCodeCondition += string.Format(" and tblline2crew.crewcode like  '{0}%'", CrewCode.ToUpper());
            }

            string columCondition = FormatHelper.GetAllFieldWithDesc(typeof(OQCLot2CardCheck), "TBLLOT2CARDCHECK", new string[] { "itemcode" }, new string[] { "tblitem.itemdesc" });

            //OQC抽检明细样本的查询的班组(TBLRES-TBLCREW)，修改成从(TBLLine2Crew- TBLCREW)中得到
            //columCondition += ",tblss.bigsscode,tblmaterial.mmodelcode,tblres.crewcode || ' - ' ||  tblcrew.crewdesc as crewcode,tbllot.productiontype, tbllot.oqclottype, tblreworksheet.reworkcode, tbloqccardlotcklist.muser  as oqcuser  ";
            columCondition += ",tblss.bigsscode,tblmaterial.mmodelcode,tblline2crew.crewcode || ' - ' || tblcrew.crewdesc AS crewcode,tbllot.productiontype, tbllot.oqclottype, tblreworksheet.reworkcode, tbloqccardlotcklist.muser  as oqcuser  ";

            string joinCondition = string.Empty;
            joinCondition += "  left outer join tblmaterial on tbllot2cardcheck.itemcode = tblmaterial.mcode ";
            joinCondition += "  left outer join tblitem on TBLLOT2CARDCHECK.itemcode =tblitem.itemcode ";
            joinCondition += "  left outer join tbllot on tbllot2cardcheck.lotno = tbllot.lotno ";
            joinCondition += "  left outer join tblss on tbllot.sscode = tblss.sscode ";

            //OQC抽检明细样本的查询的班组(TBLRES-TBLCREW)，修改成从(TBLLine2Crew- TBLCREW)中得到
            //joinCondition += "  left outer join tblres on tbllot.rescode = tblres.rescode ";
            //joinCondition += "  left outer join tblcrew on tblres.crewcode =tblcrew.crewcode ";
            joinCondition += " LEFT OUTER JOIN TBLLine2Crew ON tblline2crew.sscode = tbllot.sscode  AND tblline2crew.shiftdate = tbllot.shiftday AND tblline2crew.shiftcode = tbllot.shiftcode ";
            joinCondition += " LEFT OUTER JOIN tblcrew ON tblline2crew.crewcode = tblcrew.crewcode ";

            joinCondition += "  left outer join tblreworksheet on tblreworksheet.lotlist = tbllot2cardcheck.lotno AND TBLLOT2CARDCHECK.Mocode=TBLREWORKSHEET.Mocode AND TBLLOT2CARDCHECK.itemcode=TBLREWORKSHEET.Itemcode ";
            joinCondition += "  left outer join tbloqccardlotcklist on tbloqccardlotcklist.lotno =tbllot2cardcheck.lotno ";
            joinCondition += "  and tbloqccardlotcklist.rcard  =tbllot2cardcheck.rcard  ";
            joinCondition += "  and tbloqccardlotcklist.rcardseq  = tbllot2cardcheck.rcardseq ";
            joinCondition += "  and tbloqccardlotcklist.checkseq  = tbllot2cardcheck.checkseq ";

            string dateCondition = FormatHelper.GetDateRangeSql("tbllot.shiftday", oqcBeginDate, oqcEndDate);
            string sql = string.Format(" SELECT distinct {0} FROM TBLLOT2CARDCHECK {1} WHERE 1=1  {2}{3}{4}{5} {6} {7}{8}{9}{10}{11} {12} {13} {14} order by  mdate desc,mtime desc,lotno ",
                columCondition, joinCondition, itemCodition, moCodition, lotCodition, rcardCodition, dateCondition, sscodeCondition,
                bigsscodeCondition, mmodelcodeCondition, productionTypeCondition, OQCLotTypeCondition,
                reWorkMOCondition, mUserCondition, CrewCodeCondition);

            string columCondition1 = FormatHelper.GetAllFieldWithDesc(typeof(OQCLot2CardCheckQuery), "a", new string[] { "oqcuser" }, new string[] { "tbluser.username" });
            string joinCondition1 = "  left outer join tbluser on a.oqcuser =tbluser.usercode ";

            sql = string.Format("SELECT  {0} from (" + sql + ") a {1}", columCondition1, joinCondition1);
            return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheckQuery), new PagerCondition(sql, inclusive, exclusive));

        }


        public int QueryOQCLot2CardCheckCount(string itemCode,
            string moCode,
            string lOTNO,
            string startsn, string endsn,
            string ssCode,
             string bigLine, string materialModelCode,
            string oqcLotType, string reWorkMO,
            string productiontype, string CrewCode,
            string mUser,
            int oqcBeginDate, int oqcBeginTime,
            int oqcEndDate, int oqcEndTime)
        {
            //产品,工单,批号,产品序列号,抽检日期
            string itemCodition = string.Empty;
            if (itemCode.Trim().Length > 0 && itemCode != null)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] lists = itemCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    itemCode = string.Join(",", lists);
                    itemCodition += string.Format(@" and TBLLOT2CARDCHECK.itemcode in ({0})", itemCode.ToUpper());
                }
                else
                {
                    itemCodition += string.Format(@" and TBLLOT2CARDCHECK.itemcode like '{0}%'", itemCode.ToUpper());
                }
            }

            string moCodition = string.Empty;
            if (moCode.Trim().Length > 0 && moCode != null)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] lists = moCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    moCode = string.Join(",", lists);
                    moCodition += string.Format(@" and TBLLOT2CARDCHECK.mocode in ({0})", moCode.ToUpper());
                }
                else
                {
                    moCodition += string.Format(@" and TBLLOT2CARDCHECK.mocode like '{0}%'", moCode.ToUpper());
                }
            }

            string lotCodition = string.Empty;
            if (lOTNO != null && lOTNO.Trim().Length > 0)
            {
                if (lOTNO.IndexOf(",") >= 0)
                {
                    string[] lists = lOTNO.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    lOTNO = string.Join(",", lists);
                    lotCodition += string.Format(@" and TBLLOT.LOTNO in ({0})", lOTNO.ToUpper());
                }
                else
                {
                    lotCodition += string.Format(@" and TBLLOT.LOTNO like '{0}%'", lOTNO.ToUpper());
                }
            }

            string sscodeCondition = string.Empty;
            if (ssCode != null && ssCode.Trim().Length > 0)
            {
                if (ssCode.IndexOf(",") >= 0)
                {
                    string[] lists = ssCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    ssCode = string.Join(",", lists);
                    sscodeCondition += string.Format(@" and tblss.sscode in ({0})", ssCode.ToUpper());
                }
                else
                {
                    sscodeCondition += string.Format(@" and tblss.sscode like '{0}%'", ssCode.ToUpper());
                }
            }

            string rcardCodition = string.Empty;
            if (startsn != null && startsn != string.Empty)
            {
                rcardCodition = FormatHelper.GetCodeRangeSql("TBLLOT2CARDCHECK.rcard", startsn, endsn);
            }
            string dateCondition = FormatHelper.GetDateRangeSql("tbllot.shiftday", oqcBeginDate, oqcEndDate);

            string bigsscodeCondition = string.Empty;
            if (bigLine.Trim().Length > 0)
            {
                if (bigLine.IndexOf(",") >= 0)
                {
                    string[] lists = bigLine.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    bigLine = string.Join(",", lists);
                    bigsscodeCondition += string.Format(" and tblss.bigsscode in ({0})", bigLine);
                }
                else
                {
                    bigsscodeCondition += string.Format(" and tblss.bigsscode like '{0}%'", bigLine.ToUpper());
                }
            }

            string mmodelcodeCondition = string.Empty;
            if (materialModelCode.Trim().Length > 0)
            {
                if (materialModelCode.IndexOf(",") >= 0)
                {
                    string[] lists = materialModelCode.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    materialModelCode = string.Join(",", lists);
                    mmodelcodeCondition += string.Format(" and tblmaterial.mmodelcode in ({0})", materialModelCode);
                }
                else
                {
                    mmodelcodeCondition += string.Format(" and tblmaterial.mmodelcode like '{0}%'", materialModelCode.ToUpper());
                }
            }
            string productionTypeCondition = string.Empty;
            if (productiontype.Trim().Length > 0)
            {
                if (productiontype.IndexOf(",") >= 0)
                {
                    string[] lists = productiontype.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    productiontype = string.Join(",", lists);
                    productionTypeCondition += string.Format(" and TBLLOT.productiontype in ({0})", productiontype);
                }
                else
                {
                    productionTypeCondition += string.Format(" and TBLLOT.productiontype like '{0}%'", productiontype);
                }
            }

            string OQCLotTypeCondition = string.Empty;
            if (oqcLotType.Trim().Length > 0)
            {
                if (oqcLotType.IndexOf(",") >= 0)
                {
                    string[] lists = oqcLotType.Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    oqcLotType = string.Join(",", lists);
                    OQCLotTypeCondition += string.Format(" and TBLLOT.oqcLotType in ({0})", oqcLotType);
                }
                else
                {
                    OQCLotTypeCondition += string.Format(" and TBLLOT.oqcLotType like '{0}%'", oqcLotType);
                }
            }

            string reWorkMOCondition = string.Empty;
            if (reWorkMO.Trim().Length > 0)
            {
                reWorkMOCondition += string.Format(" and tblreworksheet.reworkcode like '{0}%'", reWorkMO.ToUpper());
            }

            string mUserCondition = string.Empty;
            if (mUser.Trim().Length > 0)
            {
                mUserCondition += string.Format(" and tbloqccardlotcklist.muser like  '{0}%'", mUser.ToUpper());
            }

            string CrewCodeCondition = string.Empty;
            if (CrewCode.Trim().Length > 0)
            {
                //OQC抽检明细样本的查询的班组(TBLRES-TBLCREW)，修改成从(TBLLine2Crew- TBLCREW)中得到
                //CrewCodeCondition += string.Format(" and tblres.crewcode like  '{0}%'", CrewCode.ToUpper());
                CrewCodeCondition += string.Format(" and tblline2crew.crewcode like  '{0}%'", CrewCode.ToUpper());
            }

            //string columCondition = "TBLSS.BIGSSCODE,TBLMATERIAL.MMODELCODE,TBLRES.CREWCODE,TBLLOT.PRODUCTIONTYPE, TBLLOT.OQCLOTTYPE, TBLREWORKSHEET.REWORKCODE, TBLOQCCARDLOTCKLIST.MUSER as OQCUSER  ";
            string columCondition = "TBLSS.BIGSSCODE,TBLMATERIAL.MMODELCODE,tblline2crew.crewcode,TBLLOT.PRODUCTIONTYPE, TBLLOT.OQCLOTTYPE, TBLREWORKSHEET.REWORKCODE, TBLOQCCARDLOTCKLIST.MUSER as OQCUSER  ";

            string joinCondition = string.Empty;
            joinCondition += "  LEFT JOIN TBLMATERIAL ON TBLLOT2CARDCHECK.ITEMCODE = TBLMATERIAL.MCODE ";
            joinCondition += "  INNER JOIN TBLLOT ON TBLLOT2CARDCHECK.LOTNO = TBLLOT.LOTNO ";
            joinCondition += "  LEFT JOIN TBLSS ON TBLLOT.SSCODE = TBLSS.SSCODE ";

            //OQC抽检明细样本的查询的班组(TBLRES-TBLCREW)，修改成从(TBLLine2Crew- TBLCREW)中得到
            //joinCondition += "  LEFT JOIN TBLRES ON TBLLOT.RESCODE = TBLRES.RESCODE ";
            joinCondition += " LEFT OUTER JOIN TBLLine2Crew ON tblline2crew.sscode = tbllot.sscode  AND tblline2crew.shiftdate = tbllot.shiftday AND tblline2crew.shiftcode = tbllot.shiftcode ";

            joinCondition += "  LEFT JOIN TBLREWORKSHEET ON TBLREWORKSHEET.LOTLIST = TBLLOT2CARDCHECK.LOTNO AND TBLLOT2CARDCHECK.Mocode=TBLREWORKSHEET.Mocode AND TBLLOT2CARDCHECK.itemcode=TBLREWORKSHEET.Itemcode ";
            joinCondition += "  LEFT JOIN TBLOQCCARDLOTCKLIST ON TBLOQCCARDLOTCKLIST.LOTNO =TBLLOT2CARDCHECK.LOTNO ";
            joinCondition += "  and tbloqccardlotcklist.rcard  =tbllot2cardcheck.rcard  ";
            joinCondition += "  and tbloqccardlotcklist.rcardseq  = tbllot2cardcheck.rcardseq ";
            joinCondition += "  and tbloqccardlotcklist.checkseq  = tbllot2cardcheck.checkseq ";
            //string sql = string.Format(" SELECT count(rcard) FROM TBLLOT2CARDCHECK WHERE 1=1 {0}{1}{2}{3}{4} {5} ",
            //    itemCodition, moCodition, lotCodition, rcardCodition, dateCondition, sscodeCondition);
            //return this.DataProvider.GetCount(new SQLCondition(sql));

            string sql = string.Format(" SELECT distinct TBLLOT2CARDCHECK.*,{0} FROM TBLLOT2CARDCHECK {1} WHERE 1=1  {2}{3}{4}{5} {6} {7}{8}{9}{10}{11} {12} {13} {14}  ",
                columCondition, joinCondition, itemCodition, moCodition, lotCodition, rcardCodition, dateCondition, sscodeCondition,
                bigsscodeCondition, mmodelcodeCondition, productionTypeCondition, OQCLotTypeCondition,
                reWorkMOCondition, mUserCondition, CrewCodeCondition);

            sql = string.Format("SELECT  count(rcard) from (" + sql + ")", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2CardCheckQuery)));
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        #endregion

        #endregion

        #region OQCLot2ErrorCode
        /// <summary>
        /// 
        /// </summary>
        public OQCLot2ErrorCode CreateNewOQCLot2ErrorCode()
        {
            return new OQCLot2ErrorCode();
        }

        public void AddOQCLot2ErrorCode(OQCLot2ErrorCode oQCLot2ErrorCode)
        {
            //this._helper.AddDomainObject( oQCLot2ErrorCode );
            this.DataProvider.Insert(oQCLot2ErrorCode);
        }

        public void UpdateOQCLot2ErrorCode(OQCLot2ErrorCode oQCLot2ErrorCode)
        {
            this._helper.UpdateDomainObject(oQCLot2ErrorCode);
        }

        public void DeleteOQCLot2ErrorCode(OQCLot2ErrorCode oQCLot2ErrorCode)
        {
            this._helper.DeleteDomainObject(oQCLot2ErrorCode);
        }

        public void DeleteOQCLot2ErrorCode(string oqcLotNo, string oqcLotSequence)
        {
            string deleteSql = " DELETE FROM TBLOQCLOT2ERRORCODE WHERE 1=1 {0}";
            string tmpString = string.Empty;
            if (oqcLotNo.Trim().Length > 0)
            {
                tmpString += "  AND LOTNO='" + oqcLotNo.ToUpper() + "'";
            }
            if (oqcLotSequence.Trim().Length > 0)
            {
                tmpString += " AND LOTSEQ=" + oqcLotSequence;
            }
            this.DataProvider.CustomExecute(new SQLCondition(String.Format(deleteSql, tmpString)));
        }

        public void DeleteOQCLot2ErrorCode(OQCLot2ErrorCode[] oQCLot2ErrorCode)
        {
            this._helper.DeleteDomainObject(oQCLot2ErrorCode);
        }

        public object GetOQCLot2ErrorCode(string errorCodeGroup, string errorCode, string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLot2ErrorCode), new object[] { errorCodeGroup, errorCode, lOTNO, lotSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCLot2ErrorCode的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
        /// <param name="errorCode">ErrorCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCLot2ErrorCode的总记录数</returns>
        public int QueryOQCLot2ErrorCodeCount(string errorCodeGroup, string errorCode, string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(
                string.Format("SELECT COUNT(*) FROM TBLOQCLOT2ERRORCODE WHERE ERRORCODEGROUP LIKE '{0}%'  AND ECODE LIKE '{1}%'  AND LOTNO LIKE '{2}%'  AND LOTSEQ LIKE '{3}%' ", errorCodeGroup, errorCode, lOTNO, lotSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCLot2ErrorCode
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
        /// <param name="errorCode">ErrorCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLot2ErrorCode数组</returns>
        public object[] QueryOQCLot2ErrorCode(string errorCodeGroup, string errorCode, string lOTNO, decimal lotSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot2ErrorCode), new PagerCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOT2ERRORCODE WHERE ERRORCODEGROUP LIKE '{1}%'  AND ECODE LIKE '{2}%'  AND LOTNO LIKE '{3}%'  AND LOTSEQ LIKE '{4}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2ErrorCode)), errorCodeGroup, errorCode, lOTNO, lotSequence), "ErrorCodeGroup,ECODE,LOTNO,LOTSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCLot2ErrorCode
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCLot2ErrorCode的总记录数</returns>
        public object[] GetAllOQCLot2ErrorCode()
        {
            return this.DataProvider.CustomQuery(typeof(OQCLot2ErrorCode),
                new SQLCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOT2ERRORCODE ORDER BY ERRORCODEGROUP,ECODE,LOTNO,LOTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2ErrorCode)))));
        }


        #endregion

        #region OQCLotCard2ErrorCode
        /// <summary>
        /// 
        /// </summary>
        public OQCLotCard2ErrorCode CreateNewOQCLotCard2ErrorCode()
        {
            return new OQCLotCard2ErrorCode();
        }

        public void AddOQCLotCard2ErrorCode(OQCLotCard2ErrorCode oQCLotCard2ErrorCode)
        {
            //this._helper.AddDomainObject( oQCLotCard2ErrorCode );
            this.DataProvider.Insert(oQCLotCard2ErrorCode);
        }

        public void UpdateOQCLotCard2ErrorCode(OQCLotCard2ErrorCode oQCLotCard2ErrorCode)
        {
            this._helper.UpdateDomainObject(oQCLotCard2ErrorCode);
        }

        public void DeleteOQCLotCard2ErrorCode(OQCLotCard2ErrorCode oQCLotCard2ErrorCode)
        {
            this._helper.DeleteDomainObject(oQCLotCard2ErrorCode);
        }

        public void DeleteOQCLotCard2ErrorCode(OQCLotCard2ErrorCode[] oQCLotCard2ErrorCode)
        {
            this._helper.DeleteDomainObject(oQCLotCard2ErrorCode);
        }

        public object GetOQCLotCard2ErrorCode(string errorCodeGroup, string errorCode, string runningCard, decimal runningCardSequence, string lOTNO, string mOCode, decimal lotSequence, decimal checkSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLotCard2ErrorCode), new object[] { errorCodeGroup, errorCode, runningCard, runningCardSequence, lOTNO, mOCode, lotSequence, checkSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCLotCard2ErrorCode的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
        /// <param name="errorCode">ErrorCode，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCLotCard2ErrorCode的总记录数</returns>
        public int QueryOQCLotCard2ErrorCodeCount(string errorCodeGroup, string errorCode, string runningCard, decimal runningCardSequence, string lOTNO, string mOCode, decimal lotSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format
                ("SELECT COUNT(*) FROM TBLOQCLOTCARD2ERRORCODE WHERE ERRORCODEGROUP LIKE '{0}%'  AND ECODE LIKE '{1}%'  AND RCARD LIKE '{2}%'  AND RCARDSEQ LIKE '{3}%'  AND LOTNO LIKE '{4}%'  AND MOCODE LIKE '{5}%'  AND LOTSEQ LIKE '{6}%' ", errorCodeGroup, errorCode, runningCard, runningCardSequence, lOTNO, mOCode, lotSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCLotCard2ErrorCode
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="errorCodeGroup">ErrorCodeGroup，模糊查询</param>
        /// <param name="errorCode">ErrorCode，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLotCard2ErrorCode数组</returns>
        public object[] QueryOQCLotCard2ErrorCode(string errorCodeGroup, string errorCode, string runningCard, decimal runningCardSequence, string lOTNO, string mOCode, decimal lotSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLotCard2ErrorCode),
                new PagerCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOTCARD2ERRORCODE WHERE ERRORCODEGROUP LIKE '{1}%'  AND ECODE LIKE '{2}%'  AND RCARD LIKE '{3}%'  AND RCARDSEQ LIKE '{4}%'  AND LOTNO LIKE '{5}%'  AND MOCODE LIKE '{6}%'  AND LOTSEQ LIKE '{7}%' ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLotCard2ErrorCode)), errorCodeGroup, errorCode, runningCard, runningCardSequence, lOTNO, mOCode, lotSequence), "ErrorCodeGroup,ECODE,RCARD,RCARDSEQ,LOTNO,MOCODE,LOTSEQ", inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	查询OQCLotCard2ErrorCode
        /// ** 作 者:		Laws Lu
        /// ** 日 期:		2005-08-13 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLotCard2ErrorCode数组</returns>
        public object[] QueryOQCLotCard2ErrorCode(string runningCard, decimal runningCardSequence, string lOTNO, string mOCode, decimal lotSequence)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLotCard2ErrorCode)
                , new SQLCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOTCARD2ERRORCODE WHERE 1=1 AND RCARD LIKE '{1}%'  AND RCARDSEQ LIKE '{2}%'  AND LOTNO LIKE '{3}%'  AND MOCODE LIKE '{4}%'  AND LOTSEQ LIKE '{5}%' "
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLotCard2ErrorCode))
                , runningCard, runningCardSequence, lOTNO, mOCode, lotSequence)));
        }

        public object[] ExtraQueryOQCLotCard2ErrorCode(string lotNO, string lotSequence)
        {
            if ((lotNO.Trim().Length == 0) || (lotNO.Trim().Length == 0))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            return this.DataProvider.CustomQuery(typeof(OQCLotCard2ErrorCode)
                , new SQLParamCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOTCARD2ERRORCODE WHERE LOTNO=$Lotno AND LOTSEQ=$LotSeq"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLotCard2ErrorCode)))
                , new SQLParameter[]{ new SQLParameter("Lotno", typeof(string), lotNO.ToUpper()),
									   new SQLParameter("LotSeq", typeof(string), lotSequence)}
                ));
        }


        /// <summary>
        /// ** 功能描述:	获得所有的OQCLotCard2ErrorCode
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCLotCard2ErrorCode的总记录数</returns>
        public object[] GetAllOQCLotCard2ErrorCode()
        {
            return this.DataProvider.CustomQuery(typeof(OQCLotCard2ErrorCode), new SQLCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOTCARD2ERRORCODE ORDER BY ERRORCODEGROUP,ECODE,RCARD,RCARDSEQ,LOTNO,MOCODE,LOTSEQ",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLotCard2ErrorCode)))));
        }

        public void UpdateOQCLotCard2ErrorCodeForOffLot(string rcard, string oldLotNo, string newLotNo, string moCode, int lotSequence)
        {
            string sql = "UPDATE tbloqclotcard2errorcode SET lotno='" + newLotNo + "' WHERE rcard='" + rcard
                + "' AND lotno='" + oldLotNo + "' AND mocode='" + moCode + "' AND lotseq=" + lotSequence;

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region OQCLOTCardCheckList
        /// <summary>
        /// 
        /// </summary>
        public OQCLOTCardCheckList CreateNewOQCLOTCardCheckList()
        {
            return new OQCLOTCardCheckList();
        }

        public void AddOQCLOTCardCheckList(OQCLOTCardCheckList oQCLOTCardCheckList)
        {
            //this._helper.AddDomainObject( oQCLOTCardCheckList );
            this.DataProvider.Insert(oQCLOTCardCheckList);
        }

        public void UpdateOQCLOTCardCheckList(OQCLOTCardCheckList oQCLOTCardCheckList)
        {
            this._helper.UpdateDomainObject(oQCLOTCardCheckList);
        }

        public void DeleteOQCLOTCardCheckList(OQCLOTCardCheckList oQCLOTCardCheckList)
        {
            this._helper.DeleteDomainObject(oQCLOTCardCheckList);
        }

        public void DeleteOQCLOTCardCheckList(OQCLOTCardCheckList[] oQCLOTCardCheckList)
        {
            this._helper.DeleteDomainObject(oQCLOTCardCheckList);
        }

        public object GetOQCLOTCardCheckList(string itemCode, string checkItemCode, string runningCard, decimal runningCardSequence, string lOTNO, string mOCode, decimal lotSequence, decimal checkSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLOTCardCheckList), new object[] { itemCode, checkItemCode, runningCard, runningCardSequence, lOTNO, mOCode, lotSequence, checkSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCLOTCardCheckList的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCLOTCardCheckList的总记录数</returns>
        public int QueryOQCLOTCardCheckListCount(string itemCode, string checkItemCode, string runningCard, decimal runningCardSequence, string lOTNO, string mOCode, decimal lotSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format
                ("SELECT COUNT(*) FROM TBLOQCCARDLOTCKLIST WHERE 1=1 AND ITEMCODE LIKE '{0}%'  AND CKITEMCODE LIKE '{1}%'  AND RCARD LIKE '{2}%'  AND RCARDSEQ LIKE '{3}%'  AND LOTNO LIKE '{4}%'  AND MOCODE LIKE '{5}%'  AND LOTSEQ LIKE '{6}%' ", itemCode, checkItemCode, runningCard, runningCardSequence, lOTNO, mOCode, lotSequence)));
        }

        #region BS查询

        public object[] QueryOQCLOTCardCheckList(string mOCode, string runningCard, decimal runningCardSequence, string lOTNO, decimal lotSequence, int inclusive, int exclusive)
        {
            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLOTCardCheckList)) + " FROM TBLOQCCARDLOTCKLIST WHERE 1=1 {0}";
            string tmpString = string.Empty;

            if (mOCode != null && mOCode != string.Empty)
            {
                tmpString += " AND MOCODE='" + mOCode.Trim().ToUpper() + "'";
            }
            if (runningCard != null && runningCard != string.Empty)
            {
                tmpString += " AND RCARD='" + runningCard.Trim().ToUpper() + "'";
            }
            if (runningCardSequence >= 0)
            {
                tmpString += " AND  RCARDSEQ=" + runningCardSequence;
            }
            if (lOTNO != null && lOTNO != string.Empty)
            {
                tmpString += " AND LOTNO ='" + lOTNO.Trim().ToUpper() + "'";
            }

            if (lotSequence >= 0)
            {
                tmpString += " AND LOTSEQ=" + lotSequence;
            }
            selectSql = String.Format(selectSql, tmpString);
            return this.DataProvider.CustomQuery(typeof(OQCLOTCardCheckList), new PagerCondition(selectSql, inclusive, exclusive));
        }

        public int QueryOQCLOTCardCheckListCount(string mOCode, string runningCard, decimal runningCardSequence, string lOTNO, decimal lotSequence)
        {
            string selectSql = "SELECT count(rcard) FROM TBLOQCCARDLOTCKLIST WHERE 1=1 {0}";
            string tmpString = string.Empty;
            if (mOCode != null && mOCode != string.Empty)
            {
                tmpString += " AND MOCODE='" + mOCode.Trim().ToUpper() + "'";
            }
            if (runningCard != null && runningCard != string.Empty)
            {
                tmpString += " AND RCARD='" + runningCard.Trim().ToUpper() + "'";
            }
            if (runningCardSequence >= 0)
            {
                tmpString += " AND  RCARDSEQ=" + runningCardSequence;
            }
            if (lOTNO != null && lOTNO != string.Empty)
            {
                tmpString += " AND LOTNO ='" + lOTNO.Trim().ToUpper() + "'";
            }

            if (lotSequence >= 0)
            {
                tmpString += " AND LOTSEQ=" + lotSequence;
            }
            return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql, tmpString)));
        }


        public object[] QueryOQCLOTCardCheckList2(string itemCode, string moCode, string runningCard, decimal runningCardSequence, string lOTNO, decimal lotSequence, decimal checkSequence, int inclusive, int exclusive)
        {
            // Modified By Hi1/Venus.Feng on 20080818 for Hisense Version : not need load checklist by itemcode
            /*
            //根据产品代码取得检验项目
            string checklistSql = string.Format(" select tblitem2oqccklist.CKITEMCODE from tblitem2oqccklist  where itemcode='{0}'" + GlobalVariables.CurrentOrganizations.GetSQLCondition(), itemCode);
            object[] ObjCheckLists = this.DataProvider.CustomQuery(typeof(Item2OQCCheckList), new PagerCondition(checklistSql, inclusive, exclusive));
            */
            string checklistSql = "SELECT CKITEMCODE FROM tbloqccklist";
            object[] ObjCheckLists = this.DataProvider.CustomQuery(typeof(Item2OQCCheckList), new PagerCondition(checklistSql, inclusive, exclusive));
            // End Modified

            //从 TBLLOT2CARDCHECK 中取得检验结果.
            string fileds = FormatHelper.GetAllFieldWithDesc(typeof(OQCLot2CardCheck), "TBLLOT2CARDCHECK", new string[] { "muser" }, new string[] { "tbluser.username" });
            string cardcheckSql = string.Format(@"select {5} from TBLLOT2CARDCHECK
                                                            left outer join tbluser on TBLLOT2CARDCHECK.muser=tbluser.usercode
															WHERE 1 = 1
															AND mocode = '{0}'
															AND rcard = '{1}'
															AND rcardseq = {2}
															AND lotno = '{3}'
															AND lotseq = {4}",
                                                            moCode, runningCard, runningCardSequence, lOTNO, lotSequence, fileds);
            object[] ObjsCardChecks = this.DataProvider.CustomQuery(typeof(OQCLot2CardCheck), new SQLCondition(cardcheckSql));

            //从tbloqccardlotcklist 中获取抽检样本不良明细结果.
            string cardlotcheckSql = string.Format(@"SELECT distinct tbloqccardlotcklist.lotno, tbloqccardlotcklist.rcardseq, tbloqccardlotcklist.rcard, tbloqccardlotcklist.ckitemcode,
                                                            tbloqccardlotcklist.itemcode || ' - ' || tblitem.itemdesc as itemcode, tbloqccardlotcklist.mdlcode, tbloqccardlotcklist.RESULT, tbloqccardlotcklist.grade,
                                                            tbloqccardlotcklist.lotseq, tbloqccardlotcklist.muser, tbloqccardlotcklist.mocode, tbloqccardlotcklist.mtime, 
                                                            tbloqccardlotcklist.memo, tbloqccardlotcklist.mdate, tbloqccardlotcklist.eattribute1
															FROM tbloqccardlotcklist
                                                            left outer join tblitem on tbloqccardlotcklist.itemcode=tblitem.itemcode
															WHERE 1 = 1
															AND mocode = '{0}'
															AND rcard = '{1}'
															AND rcardseq = {2}
															AND lotno = '{3}'
															AND lotseq = {4}
                                                            AND checkseq={5} 
                                                            order by ckitemcode ",
                moCode, runningCard, runningCardSequence, lOTNO, lotSequence, checkSequence);//ForSimone 此处runningCardSequence暂时修改为runningCardSequence-1,作兼容处理,待CS部分修改完毕,修改回runningCardSequence.此工作应在结案前完成
            object[] ObjsSampleNGs = this.DataProvider.CustomQuery(typeof(OQCLOTCardCheckList), new SQLCondition(cardlotcheckSql));

            //组合对象
            ArrayList returnObjList = new ArrayList();
            Hashtable sampleNGHT = new Hashtable();
            OQCLot2CardCheck cardcheck = new OQCLot2CardCheck();
            if (ObjsCardChecks != null && ObjsCardChecks.Length > 0)
            {
                cardcheck = (OQCLot2CardCheck)ObjsCardChecks[0];
            }

            if (ObjsSampleNGs != null)
            {
                foreach (OQCLOTCardCheckList sampleNG in ObjsSampleNGs)
                {
                    sampleNG.MaintainDate = cardcheck.MaintainDate;
                    sampleNG.MaintainTime = cardcheck.MaintainTime;
                    sampleNG.MaintainUser = cardcheck.MaintainUser;
                    //sampleNGHT.Add(sampleNG.CheckItemCode, sampleNG);
                    returnObjList.Add(sampleNG);
                }
            }

            //foreach (Item2OQCCheckList checklist in ObjCheckLists)
            //{
            //    if (!sampleNGHT.Contains(checklist.CheckItemCode))
            //    {
            //        OQCLOTCardCheckList sampleGood = new OQCLOTCardCheckList();
            //        sampleGood.ItemCode = cardcheck.ItemCode;
            //        sampleGood.MOCode = cardcheck.MOCode;
            //        sampleGood.ModelCode = cardcheck.ModelCode;
            //        sampleGood.RunningCard = cardcheck.RunningCard;
            //        sampleGood.RunningCardSequence = cardcheck.RunningCardSequence;
            //        sampleGood.LOTNO = cardcheck.LOTNO;
            //        sampleGood.LotSequence = cardcheck.LotSequence;
            //        sampleGood.MaintainDate = cardcheck.MaintainDate;
            //        sampleGood.MaintainTime = cardcheck.MaintainTime;
            //        sampleGood.MaintainUser = cardcheck.MaintainUser;
            //        sampleGood.Result = "GOOD";

            //        sampleGood.CheckItemCode = checklist.CheckItemCode;

            //        returnObjList.Add(sampleGood);
            //    }
            //}

            return (OQCLOTCardCheckList[])returnObjList.ToArray(typeof(OQCLOTCardCheckList));
        }

        public int QueryOQCLOTCardCheckListCount2(string itemCode, string moCode, string runningCard, decimal runningCardSequence, string lOTNO, decimal lotSequence, decimal checkSequence)
        {
            //string checklistSql = string.Format(" select count(CKITEMCODE) from tblitem2oqccklist  where itemcode='{0}'" + GlobalVariables.CurrentOrganizations.GetSQLCondition(), itemCode);
            //return this.DataProvider.GetCount(new SQLCondition(checklistSql));
            string checklistSql = "SELECT CKITEMCODE FROM tbloqccklist";
            object[] ObjCheckLists = this.DataProvider.CustomQuery(typeof(Item2OQCCheckList), new SQLCondition(checklistSql));
            // End Modified

            //从 TBLLOT2CARDCHECK 中取得检验结果.
            string cardcheckSql = string.Format(@"select TBLLOT2CARDCHECK.* from TBLLOT2CARDCHECK
															WHERE 1 = 1
															AND mocode = '{0}'
															AND rcard = '{1}'
															AND rcardseq = {2}
															AND lotno = '{3}'
															AND lotseq = {4}",
                                                            moCode, runningCard, runningCardSequence, lOTNO, lotSequence);
            object[] ObjsCardChecks = this.DataProvider.CustomQuery(typeof(OQCLot2CardCheck), new SQLCondition(cardcheckSql));

            //从tbloqccardlotcklist 中获取抽检样本不良明细结果.
            string cardlotcheckSql = string.Format(@"SELECT lotno, rcardseq, rcard, ckitemcode, itemcode, mdlcode, RESULT, grade,lotseq, muser, mocode, mtime, memo, mdate, eattribute1
															FROM tbloqccardlotcklist
															WHERE 1 = 1
															AND mocode = '{0}'
															AND rcard = '{1}'
															AND rcardseq = {2}
															AND lotno = '{3}'
															AND lotseq = {4}
                                                            AND checkseq={5} ",
                moCode, runningCard, runningCardSequence, lOTNO, lotSequence, checkSequence);//ForSimone 此处runningCardSequence暂时修改为runningCardSequence-1,作兼容处理,待CS部分修改完毕,修改回runningCardSequence.此工作应在结案前完成
            object[] ObjsSampleNGs = this.DataProvider.CustomQuery(typeof(OQCLOTCardCheckList), new SQLCondition(cardlotcheckSql));

            //组合对象
            ArrayList returnObjList = new ArrayList();
            Hashtable sampleNGHT = new Hashtable();
            OQCLot2CardCheck cardcheck = new OQCLot2CardCheck();
            if (ObjsCardChecks != null && ObjsCardChecks.Length > 0)
            {
                cardcheck = (OQCLot2CardCheck)ObjsCardChecks[0];
            }

            if (ObjsSampleNGs != null)
            {
                foreach (OQCLOTCardCheckList sampleNG in ObjsSampleNGs)
                {
                    sampleNG.MaintainDate = cardcheck.MaintainDate;
                    sampleNG.MaintainTime = cardcheck.MaintainTime;
                    sampleNG.MaintainUser = cardcheck.MaintainUser;
                    //sampleNGHT.Add(sampleNG.CheckItemCode, sampleNG);
                    returnObjList.Add(sampleNG);
                }
            }

            //foreach (Item2OQCCheckList checklist in ObjCheckLists)
            //{
            //    if (!sampleNGHT.Contains(checklist.CheckItemCode))
            //    {
            //        OQCLOTCardCheckList sampleGood = new OQCLOTCardCheckList();
            //        sampleGood.ItemCode = cardcheck.ItemCode;
            //        sampleGood.MOCode = cardcheck.MOCode;
            //        sampleGood.ModelCode = cardcheck.ModelCode;
            //        sampleGood.RunningCard = cardcheck.RunningCard;
            //        sampleGood.RunningCardSequence = cardcheck.RunningCardSequence;
            //        sampleGood.LOTNO = cardcheck.LOTNO;
            //        sampleGood.LotSequence = cardcheck.LotSequence;
            //        sampleGood.MaintainDate = cardcheck.MaintainDate;
            //        sampleGood.MaintainTime = cardcheck.MaintainTime;
            //        sampleGood.MaintainUser = cardcheck.MaintainUser;
            //        sampleGood.Result = "GOOD";

            //        sampleGood.CheckItemCode = checklist.CheckItemCode;

            //        returnObjList.Add(sampleGood);
            //    }
            //}

            return returnObjList.Count;
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCLOTCardCheckList
        /// ** 作 者:		Laws Lu,2005/08/13
        /// ** 日 期:		2005-08-13 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLOTCardCheckList数组</returns>
        public object[] QueryOQCLOTCardCheckList(string itemCode, string runningCard, decimal runningCardSequence, string lOTNO, string mOCode, decimal lotSequence)
        {
            string selectSql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLOTCardCheckList)) + " FROM TBLOQCCARDLOTCKLIST WHERE 1=1 {0}";
            string tmpString = string.Empty;
            if (itemCode.Trim().Length > 0)
            {
                tmpString += " AND ITEMCODE ='" + itemCode.Trim().ToUpper() + "'";
            }
            if (runningCard.Trim().Length > 0)
            {
                tmpString += " AND RCARD='" + runningCard.Trim().ToUpper() + "'";
            }
            if (runningCardSequence > 0)
            {
                tmpString += " AND  RCARDSEQ=" + runningCardSequence;
            }
            if (lOTNO.Trim().Length > 0)
            {
                tmpString += " AND LOTNO ='" + lOTNO.Trim().ToUpper() + "'";
            }
            if (mOCode.Trim().Length > 0)
            {
                tmpString += " AND MOCODE='" + mOCode.Trim().ToUpper() + "'";
            }
            if (lotSequence > 0)
            {
                tmpString += " AND LOTSEQ=" + lotSequence;
            }
            return this.DataProvider.CustomQuery(typeof(OQCLOTCardCheckList)
                , new SQLCondition(String.Format(selectSql, tmpString)));
        }

        #endregion

        /// <summary>
        /// ** 功能描述:	获得所有的OQCLOTCardCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCLOTCardCheckList的总记录数</returns>
        public object[] GetAllOQCLOTCardCheckList()
        {
            return this.DataProvider.CustomQuery(typeof(OQCLOTCardCheckList), new SQLCondition(string.Format
                ("SELECT {0} FROM TBLOQCCARDLOTCKLIST ORDER BY ITEMCODE,CKITEMCODE,RCARD,RCARDSEQ,LOTNO,MOCODE,LOTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLOTCardCheckList)))));
        }

        public void UpdateOQCLotCardCheckListForOffLot(string moCode, string rcard, string oldLotNo, string newLotNo, int lotSequence)
        {
            string sql = "UPDATE tbloqccardlotcklist SET lotno='" + newLotNo + "' WHERE mocode='" + moCode
                + "' AND lotno='" + oldLotNo + "' AND rcard='" + rcard + "' AND lotseq=" + lotSequence;

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region OQCLot2CheckGroup
        public OQCLot2CheckGroup CreateNewOQCLot2CheckGroup()
        {
            return new OQCLot2CheckGroup();
        }

        public void AddOQCLot2CheckGroup(OQCLot2CheckGroup oqcLot2CheckGroup)
        {
            this._helper.AddDomainObject(oqcLot2CheckGroup);
        }

        public void UpdateOQCLot2CheckGroup(OQCLot2CheckGroup oqcLot2CheckGroup)
        {
            this.DataProvider.Update(oqcLot2CheckGroup);
        }

        public void DeleteOQCLot2CheckGroup(OQCLot2CheckGroup oqcLot2CheckGroup)
        {
            this._helper.DeleteDomainObject(oqcLot2CheckGroup);
        }

        public void DeleteOQCLot2CheckGroup(OQCLot2CheckGroup[] oqcLot2CheckGroup)
        {
            this._helper.DeleteDomainObject(oqcLot2CheckGroup);
        }

        public object GetOQCLot2CheckGroup(string lotNo, decimal lotSeq, string checkGroupCode)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLot2CheckGroup), new object[] { lotNo, lotSeq, checkGroupCode });
        }

        public object[] GetOQCLot2CheckGroupOfLot(string lotNo, decimal lotSeq)
        {
            string sql = "SELECT {0} FROM tbloqclot2ckgroup WHERE lotno='" + lotNo + "' AND lotseq=" + lotSeq;
            return this.DataProvider.CustomQuery(typeof(OQCLot2CheckGroup),
                new SQLCondition(string.Format(sql,
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2CheckGroup)))));

        }
        #endregion

        #region OQCPara
        public OQCPara CreateNewOQCPara()
        {
            return new OQCPara();
        }

        public void AddOQCPara(OQCPara oqcPara)
        {
            this._helper.AddDomainObject(oqcPara);
        }
        public void UpdateOQCPara(OQCPara oqcPara)
        {
            this._helper.UpdateDomainObject(oqcPara);
        }

        public void DeleteOQCPara(OQCPara oqcPara)
        {
            this._helper.DeleteDomainObject(oqcPara);
        }

        public void AddOQCPara(OQCPara[] oqcPara)
        {
            if (oqcPara != null)
            {
                foreach (OQCPara newoqcPara in oqcPara)
                {
                    this._helper.AddDomainObject(newoqcPara);
                }
            }
        }

        public void DeleteOQCPara(OQCPara[] oqcPara)
        {
            foreach (OQCPara DeleteOQCPara in oqcPara)
            {
                this._helper.DeleteDomainObject(DeleteOQCPara);
            }
        }

        public object GetOQCPara(string templateName, string isTemplate)
        {
            return this.DataProvider.CustomSearch(typeof(OQCPara), new object[] { templateName, isTemplate });
        }

        public object[] GetOQCPara(string templateName)
        {
            string sql = "SELECT {0} FROM tbloqcpara WHERE templatename='" + templateName + "'";
            return this.DataProvider.CustomQuery(typeof(OQCPara),
                new SQLCondition(string.Format(sql,
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCPara)))));

        }


        #endregion

        #region OQCLOTCheckList
        /// <summary>
        /// 
        /// </summary>
        public OQCLOTCheckList CreateNewOQCLOTCheckList()
        {
            return new OQCLOTCheckList();
        }

        public void AddOQCLOTCheckList(OQCLOTCheckList oQCLOTCheckList)
        {
            //this._helper.AddDomainObject( oQCLOTCheckList );
            this.DataProvider.Insert(oQCLOTCheckList);
        }

        public void UpdateOQCLOTCheckList(OQCLOTCheckList oQCLOTCheckList)
        {
            //this._helper.UpdateDomainObject( oQCLOTCheckList );
            this.DataProvider.Update(oQCLOTCheckList);
        }

        public void DeleteOQCLOTCheckList(OQCLOTCheckList oQCLOTCheckList)
        {
            this._helper.DeleteDomainObject(oQCLOTCheckList);
        }

        public void DeleteOQCLOTCheckList(OQCLOTCheckList[] oQCLOTCheckList)
        {
            this._helper.DeleteDomainObject(oQCLOTCheckList);
        }

        public object GetOQCLOTCheckList(string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCLOTCheckList), new object[] { lOTNO, lotSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCLOTCheckList的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCLOTCheckList的总记录数</returns>
        public int QueryOQCLOTCheckListCount(string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format
                ("SELECT COUNT(*) FROM TBLOQCLOTCKLIST WHERE  LOTNO LIKE '{0}%'  AND LOTSEQ LIKE '{1}%' ", lOTNO, lotSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCLOTCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCLOTCheckList数组</returns>
        public object[] QueryOQCLOTCheckList(string lOTNO, decimal lotSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCLOTCheckList), new PagerCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOTCKLIST WHERE LOTNO LIKE '{1}%'  AND LOTSEQ LIKE '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLOTCheckList)), lOTNO, lotSequence), "LOTNO,LOTSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCLOTCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-05-21 15:53:24
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCLOTCheckList的总记录数</returns>
        public object[] GetAllOQCLOTCheckList()
        {
            return this.DataProvider.CustomQuery(typeof(OQCLOTCheckList), new SQLCondition(string.Format
                ("SELECT {0} FROM TBLOQCLOTCKLIST ORDER BY LOTNO,LOTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLOTCheckList)))));
        }

        public void UpdateOQCLotCheckListForOffLot(string lotNo)
        {
            string sql = "";
            sql += "UPDATE tbloqclotcklist";
            sql += "   SET agradetimes =";
            sql += "          (SELECT COUNT (rcard)";
            sql += "             FROM tbloqccardlotcklist";
            sql += "            WHERE lotno = '" + lotNo + "'";
            sql += "              AND grade = '" + OQC_AGrade + "'";
            sql += "              AND lotseq = " + Lot_Sequence_Default + "),";
            sql += "       bggradetimes =";
            sql += "          (SELECT COUNT (rcard)";
            sql += "             FROM tbloqccardlotcklist";
            sql += "            WHERE lotno = '" + lotNo + "'";
            sql += "              AND grade = '" + OQC_BGrade + "'";
            sql += "              AND lotseq = " + Lot_Sequence_Default + "),";
            sql += "       cgradetimes =";
            sql += "          (SELECT COUNT (rcard)";
            sql += "             FROM tbloqccardlotcklist";
            sql += "            WHERE lotno = '" + lotNo + "'";
            sql += "              AND grade = '" + OQC_CGrade + "'";
            sql += "              AND lotseq = " + Lot_Sequence_Default + "),";
            sql += "       zgradetimes =";
            sql += "          (SELECT COUNT (rcard)";
            sql += "             FROM tbloqccardlotcklist";
            sql += "            WHERE lotno = '" + lotNo + "'";
            sql += "              AND grade = '" + OQC_ZGrade + "'";
            sql += "              AND lotseq = " + Lot_Sequence_Default + ")";
            sql += " WHERE lotno = '" + lotNo + "' AND lotseq = " + Lot_Sequence_Default;

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region OQCDimention
        /// <summary>
        /// 
        /// </summary>
        public OQCDimention CreateNewOQCDimention()
        {
            return new OQCDimention();
        }

        public void AddOQCDimention(OQCDimention OQCDimention)
        {
            this._helper.AddDomainObject(OQCDimention);
        }

        public void UpdateOQCDimention(OQCDimention OQCDimention)
        {
            this._helper.UpdateDomainObject(OQCDimention);
        }

        public void DeleteOQCDimention(OQCDimention OQCDimention)
        {
            this._helper.DeleteDomainObject(OQCDimention);
        }

        public void DeleteOQCDimention(OQCDimention[] OQCDimention)
        {
            this._helper.DeleteDomainObject(OQCDimention);
        }

        public object GetOQCDimention(string runningCard, decimal runningCardSequence, string mOCode, string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.CustomSearch(typeof(OQCDimention), new object[] { runningCard, runningCardSequence, mOCode, lOTNO, lotSequence });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCDimention的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 19:07:58
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <returns> OQCDimention的总记录数</returns>
        public int QueryOQCDimentionCount(string runningCard, decimal runningCardSequence, string mOCode, string lOTNO, decimal lotSequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCDIM where 1=1 and RCARD like '{0}%'  and RCARDSEQ like '{1}%'  and MOCODE like '{2}%'  and LOTNO like '{3}%'  and LOTSEQ like '{4}%' ", runningCard, runningCardSequence, mOCode, lOTNO, lotSequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCDimention
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 19:07:58
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCDimention数组</returns>
        public object[] QueryOQCDimention(string runningCard, decimal runningCardSequence, string mOCode, string lOTNO, decimal lotSequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCDimention), new PagerCondition(string.Format("select {0} from TBLOQCDIM where 1=1 and RCARD like '{1}%'  and RCARDSEQ like '{2}%'  and MOCODE like '{3}%'  and LOTNO like '{4}%'  and LOTSEQ like '{5}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDimention)), runningCard, runningCardSequence, mOCode, lOTNO, lotSequence), "RCARD,RCARDSEQ,MOCODE,LOTNO,LOTSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCDimention
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 19:07:58
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCDimention的总记录数</returns>
        public object[] GetAllOQCDimention()
        {
            return this.DataProvider.CustomQuery(typeof(OQCDimention), new SQLCondition(string.Format("select {0} from TBLOQCDIM order by RCARD,RCARDSEQ,MOCODE,LOTNO,LOTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDimention)))));
        }


        #endregion

        #region OQCDimentionVALUE
        /// <summary>
        /// 
        /// </summary>
        public OQCDimentionValue CreateNewOQCDimentionValue()
        {
            return new OQCDimentionValue();
        }
        public void AddOQCDimentionVALUE(OQCDimentionValue OQCDimentionVALUE)
        {
            this._helper.AddDomainObject(OQCDimentionVALUE);
        }

        public void UpdateOQCDimentionVALUE(OQCDimentionValue OQCDimentionVALUE)
        {
            this._helper.UpdateDomainObject(OQCDimentionVALUE);
        }

        public void DeleteOQCDimentionVALUE(OQCDimentionValue OQCDimentionVALUE)
        {
            this._helper.DeleteDomainObject(OQCDimentionVALUE);
        }

        public void DeleteOQCDimentionVALUE(OQCDimentionValue[] OQCDimentionVALUE)
        {
            this._helper.DeleteDomainObject(OQCDimentionVALUE);
        }

        public object GetOQCDimentionVALUE(string lOTNO, decimal lotSequence, string runningCard, decimal runningCardSequence, string mOCode, string paramname)
        {
            return this.DataProvider.CustomSearch(typeof(OQCDimentionValue), new object[] { lOTNO, lotSequence, runningCard, runningCardSequence, mOCode, paramname });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCDimentionVALUE的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 19:07:58
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <returns> OQCDimentionVALUE的总记录数</returns>
        public int QueryOQCDimentionValueCount(string lOTNO, decimal lotSequence, string runningCard, decimal runningCardSequence, string mOCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCDIMVALUE where 1=1 and LOTNO like '{0}%'  and LOTSEQ like '{1}%'  and RCARD like '{2}%'  and RCARDSEQ like '{3}%'  and MOCODE like '{4}%' ", lOTNO, lotSequence, runningCard, runningCardSequence, mOCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCDimentionVALUE
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 19:07:58
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="lOTNO">LOTNO，模糊查询</param>
        /// <param name="lotSequence">LotSequence，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="runningCardSequence">RunningCardSequence，模糊查询</param>
        /// <param name="mOCode">MOCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCDimentionVALUE数组</returns>
        public object[] QueryOQCDimentionValue(string lOTNO, decimal lotSequence, string runningCard, decimal runningCardSequence, string mOCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCDimentionValue), new PagerCondition(string.Format("select {0} from TBLOQCDIMVALUE where 1=1 and LOTNO like '{1}%'  and LOTSEQ like '{2}%'  and RCARD like '{3}%'  and RCARDSEQ like '{4}%'  and MOCODE like '{5}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDimentionValue)), lOTNO, lotSequence, runningCard, runningCardSequence, mOCode), "LOTNO,LOTSEQ,RCARD,RCARDSEQ,MOCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OQCDimentionVALUE
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 19:07:58
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OQCDimentionVALUE的总记录数</returns>
        public object[] GetAllOQCDimentionVALUE()
        {
            return this.DataProvider.CustomQuery(typeof(OQCDimentionValue), new SQLCondition(string.Format("select {0} from TBLOQCDIMVALUE order by LOTNO,LOTSEQ,RCARD,RCARDSEQ,MOCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDimentionValue)))));
        }


        #endregion

        #region OQCCheckGroup2List
        /// <summary>
        /// 
        /// </summary>
        public OQCCheckGroup2List CreateNewOQCCheckGroup2List()
        {
            return new OQCCheckGroup2List();
        }

        public void AddOQCCheckGroup2List(OQCCheckGroup2List[] oQCCheckGroup2List)
        {
            this._helper.AddDomainObject(oQCCheckGroup2List);
        }

        public void DeleteOQCCheckGroup2List(OQCCheckGroup2List[] oQCCheckGroup2List)
        {
            this._helper.DeleteDomainObject(oQCCheckGroup2List);
        }


        public object GetOQCCheckGroup2List(string checkGroup, string checkItemCode)
        {
            return this.DataProvider.CustomSearch(typeof(OQCCheckGroup2List), new object[] { checkGroup, checkItemCode });
        }

        /// <summary>
        /// ** 功能描述:	查询OQCCheckGroup2List的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Roger Xue
        /// ** 日 期:		2008-09-10
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckGroup</param>
        /// <returns> OQCCheckGroup2List的总记录数</returns>
        public int QueryOQCCheckGroup2ListCount(string checkGroup)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCCKGROUP2LIST where 1=1 and CKGROUP = '{0}' ", checkGroup)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCCheckGroup2List
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Roger Xue
        /// ** 日 期:		2008-09-10
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCCheckGroup2List数组</returns>
        public object[] QueryOQCCheckGroup2List(string checkGroup, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCCheckGroup2List), new PagerCondition(string.Format("select {0} from TBLOQCCKGROUP2LIST where CKGROUP = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckGroup2List)), checkGroup), "CKITEMCODE", inclusive, exclusive));
        }


        /// <summary>
        /// ** 功能描述:	查询OQCCheckGroup2ListAdd的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Roger Xue
        /// ** 日 期:		2005-09-10
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckGroup</param>
        /// <returns> OQCCheckGroup2ListAdd的总记录数</returns>
        public int QueryOQCCheckItemByCheckGroupCount(string checkGroup, string checkItemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOQCCKLIST where CKITEMCODE not in(" +
                "select ckitemcode from TBLOQCCKGROUP2List where ckgroup = '{0}') and CKITEMCODE like '%{1}%' ", checkGroup, checkItemCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OQCCheckGroup2ListAdd
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Roger Xue
        /// ** 日 期:		2005-09-10
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">CheckGroup</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OQCCheckGroup2ListAdd数组</returns>
        public object[] QueryOQCCheckItemByCheckGroup(string checkGroup, string checkItemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OQCCheckGroup2List), new PagerCondition(string.Format("select {0} from TBLOQCCKLIST where CKITEMCODE not in (" +
                "select ckitemcode from TBLOQCCKGROUP2List where ckgroup = '{1}') and CKITEMCODE like '%{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCCheckList)), checkGroup, checkItemCode), "CKITEMCODE", inclusive, exclusive));
        }


        #endregion

        #region Frozen

        public Frozen CreateNewFrozen()
        {
            return new Frozen();
        }

        public void AddFrozen(Frozen frozen)
        {
            this._helper.AddDomainObject(frozen);
        }

        public void DeleteFrozen(Frozen frozen)
        {
            this._helper.DeleteDomainObject(frozen);
        }

        public void UpdateFrozen(Frozen frozen)
        {
            this._helper.UpdateDomainObject(frozen);
        }

        public object GetFrozen(string rCard, string lotNo, int lotSequence, string moCode, string itemCode, int frozenSequence)
        {
            return this.DataProvider.CustomSearch(typeof(Frozen), new object[] { rCard, lotNo, lotSequence, moCode, itemCode, frozenSequence });
        }

        public object[] QueryFrozenRCard(string lotNo)
        {
            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Frozen)) + " FROM tblfrozen ";
            sql += "WHERE 1=1 ";

            if (lotNo.Trim().Length > 0)
            {
                sql += "AND lotno = '" + lotNo.Trim().ToUpper() + "' ";
            }

            sql += "AND lotseq = " + Lot_Sequence_Default.ToString() + " ";
            sql += "AND frozenstatus = '" + FrozenStatus.STATUS_FRONZEN + "' ";

            return this.DataProvider.CustomQuery(typeof(Frozen), new PagerCondition(sql, "rcard,frozenseq,frozendate,frozentime,unfrozendate,unfrozentime", int.MinValue, int.MaxValue));
        }

        public int QueryFrozenRCardCount(string lotNo)
        {
            string sql = "SELECT count(*) FROM tblfrozen ";
            sql += "WHERE 1=1 ";
            sql += "AND lotno = '" + lotNo.Trim().ToUpper() + "' ";
            sql += "AND lotseq = " + Lot_Sequence_Default.ToString() + " ";
            sql += "AND frozenstatus = '" + FrozenStatus.STATUS_FRONZEN + "' ";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public object[] QueryFrozen(string rCardStart, string rCardEnd,
           string lotNo, string moCode, string itemCode, string frozenStatus, int frozenDateStart,
            int frozenDateEnd, int unfrozenDateStart, int unfrozenDateEnd, int inclusive, int exclusive)
        {
            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Frozen)) + " FROM tblfrozen ";
            sql += "WHERE 1=1 ";

            if (rCardStart.Trim().Length > 0)
            {
                sql += "AND rcard >= '" + rCardStart.Trim().ToUpper() + "' ";
            }
            if (rCardEnd.Trim().Length > 0)
            {
                sql += "AND rcard <= '" + rCardEnd.Trim().ToUpper() + "' ";
            }

            if (lotNo.Trim().Length > 0)
            {
                sql += "AND lotno = '" + lotNo.Trim().ToUpper() + "' ";
            }
            if (moCode.Trim().Length > 0)
            {
                sql += "AND mocode = '" + moCode.Trim().ToUpper() + "' ";
            }
            if (itemCode.Trim().Length > 0)
            {
                sql += "AND itemcode = '" + itemCode.Trim().ToUpper() + "' ";
            }

            if (frozenStatus.Trim().Length > 0)
            {
                sql += "AND frozenstatus = '" + frozenStatus.Trim() + "' ";
            }
            if (frozenDateStart > 0)
            {
                sql += "AND frozendate >= " + frozenDateStart.ToString() + " ";
            }
            if (frozenDateEnd > 0)
            {
                sql += "AND frozendate <= " + frozenDateEnd.ToString() + " ";
            }
            if (unfrozenDateStart > 0)
            {
                sql += "AND unfrozendate >= " + unfrozenDateStart.ToString() + " ";
            }
            if (unfrozenDateEnd > 0)
            {
                sql += "AND unfrozendate >= " + unfrozenDateEnd.ToString() + " ";
            }

            return this.DataProvider.CustomQuery(typeof(Frozen), new PagerCondition(sql, "rcard,frozenseq,frozendate,frozentime,unfrozendate,unfrozentime", inclusive, exclusive));
        }


        public object[] QueryFrozenAndMmodelcode(string rCardStart, string rCardEnd,
            string lotNo, string moCode, string itemCode, string Mmodelcode, string sscode, string bigLine,
            string frozenStatus, int frozenDateStart, int frozenDateEnd, int unfrozenDateStart, int unfrozenDateEnd,
            int inclusive, int exclusive)
        {
            string sql = "SELECT  a.*,b.mmodelcode FROM tblfrozen a,tblmaterial b ";
            sql += "WHERE 1=1 and  a.itemcode=b.mcode";

            if (rCardStart.Trim().Length > 0)
            {
                sql += " and a.rcard >= '" + rCardStart.Trim().ToUpper() + "' ";
            }
            if (rCardEnd.Trim().Length > 0)
            {
                sql += " and  a.rcard <= '" + rCardEnd.Trim().ToUpper() + "' ";
            }
            if (lotNo.Trim().Length > 0)
            {
                if (lotNo.IndexOf(",") > 0)
                {
                    sql += " and  a.lotno in  ('" + lotNo.Trim().ToUpper() + "') ";
                }
                else
                {
                    sql += " and  a.lotno like '%" + lotNo.Trim().ToUpper() + "%' ";
                }
            }
            if (moCode.Trim().Length > 0)
            {
                sql += " and  a.mocode  like  '%" + moCode.Trim().ToUpper() + "%' ";
            }
            if (itemCode.Trim().Length > 0)
            {
                sql += " and a.itemcode  like  '%" + itemCode.Trim().ToUpper() + "%' ";
            }
            if (Mmodelcode.Length > 0)
            {
                Mmodelcode = Mmodelcode.ToUpper();
                if (Mmodelcode.IndexOf(",") > 0)
                {
                    sql += " and b.mmodelcode in ('" + Mmodelcode + "')";
                }
                else
                {
                    sql += " and b.mmodelcode like ('%" + Mmodelcode + "%')";
                }
            }
            if (sscode.Length > 0)
            {
                if (sscode.IndexOf(",") > 0)
                {
                    sql += " and a.itemcode in (select distinct itemcode from tbllot2card where sscode in ('" + sscode.ToUpper() + "'))";
                }
                else
                {
                    sql += " and a.itemcode in (select distinct itemcode from tbllot2card where sscode like  ('%" + sscode.ToUpper() + "%'))";
                }

            }

            if (bigLine.Length > 0)
            {
                if (bigLine.IndexOf(",") > 0)
                {
                    sql += " and a.itemcode in  (select distinct itemcode from tbllot2card where sscode in ( select distinct sscode  from tblss where bigsscode in  ('" + bigLine.ToUpper() + "')))";
                }
                else
                {
                    sql += " and a.itemcode in  (select distinct itemcode from tbllot2card where sscode in ( select distinct sscode  from tblss where bigsscode like ('%" + bigLine.ToUpper() + "%')))";
                }

            }

            if (frozenStatus.Trim().Length > 0)
            {
                sql += " and  a.frozenstatus = '" + frozenStatus.Trim() + "' ";
            }
            if (frozenDateStart > 0)
            {
                sql += " and a.frozendate >= " + frozenDateStart.ToString() + " ";
            }
            if (frozenDateEnd > 0)
            {
                sql += "  and a.frozendate <= " + frozenDateEnd.ToString() + " ";
            }
            if (unfrozenDateStart > 0)
            {
                sql += " and a.unfrozendate >= " + unfrozenDateStart.ToString() + " ";
            }
            if (unfrozenDateEnd > 0)
            {
                sql += " and a.unfrozendate >= " + unfrozenDateEnd.ToString() + " ";
            }

            return this.DataProvider.CustomQuery(typeof(FrozenAndMmodelCode), new PagerCondition(sql, "a.frozendate desc,a.frozentime desc,a.rcard,a.frozenseq,a.unfrozendate,a.unfrozentime", inclusive, exclusive));
        }


        public int QueryFrozenCount(string rCardStart, string rCardEnd,
           string lotNo, string moCode, string itemCode,
           string frozenStatus, int frozenDateStart, int frozenDateEnd, int unfrozenDateStart, int unfrozenDateEnd)
        {
            string sql = "SELECT COUNT(*) FROM tblfrozen ";
            sql += "WHERE 1=1 ";

            if (rCardStart.Trim().Length > 0)
            {
                sql += " and rcard >= '" + rCardStart.Trim().ToUpper() + "' ";
            }
            if (rCardEnd.Trim().Length > 0)
            {
                sql += " and rcard <= '" + rCardEnd.Trim().ToUpper() + "' ";
            }

            if (lotNo.Trim().Length > 0)
            {
                if (lotNo.IndexOf(",") > 0)
                {
                    sql += " and lotno in  ('" + lotNo.Trim().ToUpper() + "') ";
                }
                else
                {
                    sql += " and lotno like  '%" + lotNo.Trim().ToUpper() + "%' ";
                }
            }
            if (moCode.Trim().Length > 0)
            {
                sql += " and mocode like  '%" + moCode.Trim().ToUpper() + "%' ";
            }
            if (itemCode.Trim().Length > 0)
            {
                sql += " and itemcode like  '%" + itemCode.Trim().ToUpper() + "%' ";
            }

            if (frozenStatus.Trim().Length > 0)
            {
                sql += " and frozenstatus = '" + frozenStatus.Trim() + "' ";
            }
            if (frozenDateStart > 0)
            {
                sql += " and frozendate >= " + frozenDateStart.ToString() + " ";
            }
            if (frozenDateEnd > 0)
            {
                sql += " and frozendate <= " + frozenDateEnd.ToString() + " ";
            }
            if (unfrozenDateStart > 0)
            {
                sql += " and unfrozendate >= " + unfrozenDateStart.ToString() + " ";
            }
            if (unfrozenDateEnd > 0)
            {
                sql += " and unfrozendate <= " + unfrozenDateEnd.ToString() + " ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryFrozenCount(string rCardStart, string rCardEnd,
            string lotNo, string moCode, string itemCode, string Mmodelcode, string sscode, string bigLine,
            string frozenStatus, int frozenDateStart, int frozenDateEnd, int unfrozenDateStart, int unfrozenDateEnd)
        {
            string sql = "SELECT  count(*)  FROM tblfrozen a,tblmaterial b ";
            sql += "WHERE 1=1 and  a.itemcode=b.mcode";

            if (rCardStart.Trim().Length > 0)
            {
                sql += " and a.rcard >= '" + rCardStart.Trim().ToUpper() + "' ";
            }
            if (rCardEnd.Trim().Length > 0)
            {
                sql += " and a.rcard <= '" + rCardEnd.Trim().ToUpper() + "' ";
            }

            if (lotNo.Trim().Length > 0)
            {
                if (lotNo.IndexOf(",") > 0)
                {
                    sql += " and lotno in  ('" + lotNo.Trim().ToUpper() + "') ";
                }
                else
                {
                    sql += " and lotno like  '%" + lotNo.Trim().ToUpper() + "%' ";
                }
            }

            if (moCode.Trim().Length > 0)
            {
                if (moCode.IndexOf(",") > 0)
                {
                    sql += " and a.mocode in ('" + moCode.Trim().ToUpper() + "') ";
                }
                else
                {
                    sql += " and a.mocode like '%" + moCode.Trim().ToUpper() + "%' ";
                }
            }

            if (itemCode.Trim().Length > 0)
            {
                if (itemCode.IndexOf(",") > 0)
                {
                    sql += " and itemcode in ('" + itemCode.Trim().ToUpper() + "') ";
                }
                else
                {
                    sql += " and itemcode like '%" + itemCode.Trim().ToUpper() + "%' ";
                }
            }

            if (Mmodelcode.Length > 0)
            {
                if (Mmodelcode.IndexOf(",") > 0)
                {
                    sql += " and b.mmodelcode in ('" + Mmodelcode.ToUpper() + "')";
                }
                else
                {
                    sql += " and b.mmodelcode like ('%" + Mmodelcode.ToUpper() + "%')";
                }
            }
            if (sscode.Length > 0)
            {
                if (sscode.IndexOf(",") > 0)
                {
                    sql += " and a.itemcode in (select distinct itemcode from tbllot2card where sscode in ('" + sscode.ToUpper() + "')) ";
                }
                else
                {
                    sql += " and a.itemcode in (select distinct itemcode from tbllot2card where sscode like ('%" + sscode.ToUpper() + "%')) ";
                }

            }

            if (bigLine.Length > 0)
            {
                if (bigLine.IndexOf(",") > 0)
                {
                    sql += " and a.itemcode in  (select distinct itemcode from tbllot2card where sscode in ( select distinct sscode  from tblss where bigsscode in ('" + bigLine.ToUpper() + "'))) ";
                }
                else
                {
                    sql += " and a.itemcode in  (select distinct itemcode from tbllot2card where sscode in ( select distinct sscode  from tblss where bigsscode like ('%" + bigLine.ToUpper() + "%'))) ";
                }

            }

            if (frozenStatus.Trim().Length > 0)
            {
                sql += " and a.frozenstatus = '" + frozenStatus.Trim() + "' ";
            }
            if (frozenDateStart > 0)
            {
                sql += " and a.frozendate >= " + frozenDateStart.ToString() + " ";
            }
            if (frozenDateEnd > 0)
            {
                sql += " and a.frozendate <= " + frozenDateEnd.ToString() + " ";
            }
            if (unfrozenDateStart > 0)
            {
                sql += " and a.unfrozendate >= " + unfrozenDateStart.ToString() + " ";
            }
            if (unfrozenDateEnd > 0)
            {
                sql += " and a.unfrozendate <= " + unfrozenDateEnd.ToString() + " ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryFrozenAndMaterial(string lotno, string Mmodelcode, string Stepsequence,
                                                string BigLine, int frozenDateStart, int frozenDateEnd,
                                                int unfrozenDateEnd, int unfrozenDateStart, string statusList,
                                                int inclusive, int exclusive)
        {
            string sql = "select a.*,b.mmodelcode,b.mdesc from tbllot a,tblmaterial b where 1=1 ";
            sql += " and a.itemcode= b.mcode ";

            if (statusList.Trim().Length > 0)
            {
                sql += " and a.FROZENSTATUS IN (" + FormatHelper.ProcessQueryValues(statusList).ToLower() + ") ";
            }

            if (lotno.Length > 0)
            {
                if (lotno.IndexOf(",") > 0)
                {
                    sql += " and  a.lotno in ('" + lotno.ToUpper() + "') ";
                }
                else
                {
                    sql += " and  a.lotno like '%" + lotno.ToUpper() + "%' ";
                }
            }
            if (frozenDateStart > 0)
            {
                sql += " and a.frozendate >= " + frozenDateStart.ToString() + " ";
            }
            if (frozenDateEnd > 0)
            {
                sql += " and a.frozendate <= " + frozenDateEnd.ToString() + " ";
            }

            if (unfrozenDateStart > 0)
            {
                sql += " and a.unfrozendate >= " + unfrozenDateStart.ToString() + " ";
            }
            if (unfrozenDateEnd > 0)
            {
                sql += " and a.unfrozendate <= " + unfrozenDateEnd.ToString() + " ";
            }

            if (Mmodelcode.Length > 0)
            {
                Mmodelcode = Mmodelcode.ToUpper();
                if (Mmodelcode.IndexOf(",") > 0)
                {
                    sql += " and b.mmodelcode in ('" + Mmodelcode.ToUpper() + "')";
                }
                else
                {
                    sql += " and b.mmodelcode like ('%" + Mmodelcode.ToUpper() + "%')";
                }
            }
            if (Stepsequence.Length > 0)
            {
                if (Stepsequence.IndexOf(",") > 0)
                {
                    sql += " and a.sscode in ('" + Stepsequence.ToUpper() + "') ";
                }
                else
                {
                    sql += " and a.sscode like  ('%" + Stepsequence.ToUpper() + "%') ";
                }

            }
            if (BigLine.Length > 0)
            {
                if (BigLine.IndexOf(",") > 0)
                {
                    sql += " and a.sscode in (select sscode from tblss where bigsscode in ('" + BigLine.ToUpper() + "')) ";
                }
                else
                {
                    sql += " and a.sscode in (select sscode from tblss where bigsscode like ('%" + BigLine.ToUpper() + "%')) ";
                }
            }
            return this.DataProvider.CustomQuery(typeof(OQCLOTAndMaterial), new PagerCondition(sql, "lotno", inclusive, exclusive));

        }

        public int QueryFrozenAndMaterialCount(string lotno, string Mmodelcode, string Stepsequence, string BigLine, int frozenDateStart, int frozenDateEnd, int unfrozenDateStart, int unfrozenDateEnd, string statusList)
        {
            string sql = "select count(*) from tbllot a,tblmaterial b where 1=1 ";
            sql += " and a.itemcode= b.mcode ";

            if (statusList.Trim().Length > 0)
            {
                sql += " and a.FROZENSTATUS IN (" + FormatHelper.ProcessQueryValues(statusList).ToLower() + ") ";
            }

            if (lotno.Length > 0)
            {
                if (lotno.IndexOf(",") > 0)
                {
                    sql += " and  a.lotno in ('" + lotno.ToUpper() + "') ";
                }
                else
                {
                    sql += " and  a.lotno like '%" + lotno.ToUpper() + "%' ";
                }
            }
            if (frozenDateStart > 0)
            {
                sql += " and a.frozendate >= " + frozenDateStart.ToString() + " ";
            }
            if (frozenDateEnd > 0)
            {
                sql += " and a.frozendate <= " + frozenDateEnd.ToString() + " ";
            }

            if (unfrozenDateStart > 0)
            {
                sql += " and a.unfrozendate >= " + unfrozenDateStart.ToString() + " ";
            }
            if (unfrozenDateEnd > 0)
            {
                sql += " and a.unfrozendate <= " + unfrozenDateEnd.ToString() + " ";
            }

            if (Mmodelcode.Length > 0)
            {
                if (Mmodelcode.IndexOf(",") > 0)
                {
                    sql += " and b.mmodelcode in ('" + Mmodelcode.ToUpper() + "')";
                }
                else
                {
                    sql += " and b.mmodelcode like ('%" + Mmodelcode.ToUpper() + "%')";
                }
            }

            if (Stepsequence.Length > 0)
            {
                if (Stepsequence.IndexOf(",") > 0)
                {
                    sql += " and a.sscode in ('" + Stepsequence.ToUpper() + "') ";
                }
                else
                {
                    sql += " and a.sscode like  ('%" + Stepsequence.ToUpper() + "%') ";
                }
            }
            if (BigLine.Length > 0)
            {
                if (BigLine.IndexOf(",") > 0)
                {
                    sql += " and a.sscode in (select sscode from tblss where bigsscode in ('" + BigLine.ToUpper() + "')) ";
                }
                else
                {
                    sql += " and a.sscode in (select sscode from tblss where bigsscode like ('%" + BigLine.ToUpper() + "%')) ";
                }
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        public void FreezeLot(OQCLot lot, string freezeReason, bool freezeLot, string userCode)
        {
            if (lot == null)
                return;

            object[] lot2rcards = QueryOQCLot2CardByLotNo(lot.LOTNO);

            DBDateTime currentDBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            //将产品加入到tblfrozen
            if (lot2rcards != null)
            {
                foreach (OQCLot2Card rCard in lot2rcards)
                {
                    Frozen frozenRCard = CreateNewFrozen();

                    frozenRCard.RCard = rCard.RunningCard;
                    frozenRCard.LotNo = rCard.LOTNO;
                    frozenRCard.LotSequence = Convert.ToInt32(rCard.LotSequence);
                    frozenRCard.MOCode = rCard.MOCode;
                    frozenRCard.ModelCode = rCard.ModelCode;
                    frozenRCard.ItemCode = rCard.ItemCode;

                    frozenRCard.FrozenStatus = FrozenStatus.STATUS_FRONZEN;
                    frozenRCard.FrozenDate = currentDBDateTime.DBDate;
                    frozenRCard.FrozenTime = currentDBDateTime.DBTime;
                    frozenRCard.FrozenReason = freezeReason;
                    frozenRCard.FrozenBy = userCode;

                    frozenRCard.MaintainDate = currentDBDateTime.DBDate;
                    frozenRCard.MaintainTime = currentDBDateTime.DBTime;
                    frozenRCard.MaintainUser = userCode;

                    int seq = 0;
                    object[] oldFrozenRCard = QueryFrozen(rCard.RunningCard, rCard.RunningCard,
                        string.Empty, string.Empty, string.Empty, string.Empty,
                        -1, -1, -1, -1, int.MinValue, int.MaxValue);

                    if (oldFrozenRCard != null)
                    {
                        foreach (Frozen frozen in oldFrozenRCard)
                        {
                            seq = Math.Max(seq, frozen.FrozenSequence);
                        }
                    }

                    frozenRCard.FrozenSequence = seq + 1;
                    AddFrozen(frozenRCard);
                }
            }

            //更新tbllot.lotfrozen
            lot.LotFrozen = freezeLot ? "Y" : "N";
            lot.FrozenStatus = FrozenStatus.STATUS_FRONZEN;
            lot.FrozenReason = freezeReason;
            lot.FrozenTime = currentDBDateTime.DBTime;
            lot.FrozenDate = currentDBDateTime.DBDate;
            lot.FrozenBy = userCode;
            UpdateOQCLotFrozen(lot);
        }

        public void UnFreezeFrozen(string lotNo, string unFrozenReason, int unFrozenDate,
            int unFrozenTime, string unFrozenBy, decimal lotSeq)
        {
            string sql = "UPDATE tblfrozen SET frozenstatus='" + FrozenStatus.STATUS_UNFRONZEN
                 + "',unfrozenreason='" + unFrozenReason
                 + "',muser='" + unFrozenBy
                 + "',mdate=" + unFrozenDate
                 + ",mtime=" + unFrozenTime
                 + ",unfrozenby='" + unFrozenBy
                 + "',unfrozendate=" + unFrozenDate
                 + ",unfrozentime=" + unFrozenTime
                 + " WHERE frozenstatus='" + FrozenStatus.STATUS_FRONZEN + "' AND lotno='" + lotNo + "' and lotseq=" + lotSeq + " ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UnfreezeRCard(Frozen frozen, string unfreezeReason, string userCode)
        {
            if (frozen == null)
                return;

            DBDateTime currentDBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            frozen.UnfrozenBy = userCode;
            frozen.UnfrozenDate = currentDBDateTime.DBDate;
            frozen.UnfrozenTime = currentDBDateTime.DBTime;
            frozen.UnfrozenReason = unfreezeReason;
            frozen.FrozenStatus = FrozenStatus.STATUS_UNFRONZEN;

            frozen.MaintainDate = currentDBDateTime.DBDate;
            frozen.MaintainTime = currentDBDateTime.DBTime;
            frozen.MaintainUser = userCode;

            UpdateFrozen(frozen);
        }

        public void UnfreezeRCard(string rcard, string lotNo,
            int lotSequence, string moCode, string itemCode,
            string unfreezeReason, string userCode, DBDateTime currentDBDateTime)
        {
            string sql = "UPDATE tblfrozen SET frozenstatus='" + FrozenStatus.STATUS_UNFRONZEN
                + "',unfrozenreason='" + unfreezeReason
                + "',muser='" + userCode
                + "',mdate=" + currentDBDateTime.DBDate
                + ",mtime=" + currentDBDateTime.DBTime
                + ",unfrozenby='" + userCode
                + "',unfrozendate=" + currentDBDateTime.DBDate
                + ",unfrozentime=" + currentDBDateTime.DBTime
                + " WHERE rcard='" + rcard + "' AND lotno='" + lotNo
                + "' AND lotseq=" + lotSequence + " AND mocode='" + moCode + "' AND itemcode='" + itemCode + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateFrozenForOffLot(string rcard, string oldLotNo, int lotSequence,
            string newLotNo, string moCode, string itemCode, string userCode, DBDateTime currentDBDateTime)
        {
            string sql = "UPDATE tblfrozen SET lotno='" + newLotNo
                + "',muser='" + userCode
                + "',mdate=" + currentDBDateTime.DBDate
                + ",mtime=" + currentDBDateTime.DBTime
                + " WHERE rcard='" + rcard + "' AND lotno='" + oldLotNo
                + "' AND lotseq=" + lotSequence + " AND mocode='" + moCode + "' AND itemcode='" + itemCode + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateUnFrozenOnLot(string lotNo, string unFrozenReason, int unFrozenDate,
            int unFrozenTime, string unFrozenBy, decimal lotSeq)
        {
            string sql = "UPDATE tbllot SET unfrozenreason = '" + unFrozenReason + "', unfrozendate = '" + unFrozenDate +
                "', unfrozentime = '" + unFrozenTime + "', frozenstatus='" + FrozenStatus.STATUS_UNFRONZEN + "',unfrozenby = '" + unFrozenBy +
                "' WHERE lotno = '" + lotNo + "' AND lotseq=" + lotSeq;

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        public object[] GetCheckGroupList4OQCCollect(string lotNo, string resourceCode)
        {
            string sql;
            sql = "";
            sql += "SELECT a.lotno as lotno, a.ckgroup as  ckgroup, a.needcheckcount as needcheckcount, NVL(b.checkedcount,0) as checkedcount";
            sql += "  FROM (SELECT a.lotno, a.ckgroup, a.needcheckcount";
            sql += "          FROM tbloqclot2ckgroup a";
            sql += "         WHERE a.lotno = '" + lotNo + "'";
            sql += "           AND a.ckgroup IN (";
            sql += "                  SELECT DISTINCT nodename";
            sql += "                             FROM tbloqcpara";
            sql += "                            WHERE templatename = '" + resourceCode + "'";
            sql += "                              AND nodename IN (SELECT DISTINCT ckgroup";
            sql += "                                                          FROM tbloqcckgroup))) a,";
            sql += "       (SELECT   ckgroup, COUNT (DISTINCT rcard) AS checkedcount";
            sql += "            FROM tbloqccardlotcklist";
            sql += "           WHERE lotno = '" + lotNo + "'";
            sql += "        GROUP BY ckgroup) b";
            sql += " WHERE a.ckgroup = b.ckgroup(+)";
            sql += " ORDER BY a.ckgroup";

            return this.DataProvider.CustomQuery(typeof(OQCLot2CheckGroup), new SQLCondition(sql));
        }

        public decimal GetNextCheckSequence(string rcard)
        {
            string sql = "";
            sql += "SELECT NVL (MAX (checkseq), 0) + 1 AS checkseq";
            sql += "  FROM tbloqccardlotcklist";
            sql += " WHERE rcard = '" + rcard + "'";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetCheckGroupList4LotCheck(string lotNo)
        {
            string sql = "";
            sql += "SELECT a.lotno AS lotno, a.ckgroup AS ckgroup,";
            sql += "       a.needcheckcount AS needcheckcount,";
            sql += "       NVL (b.checkedcount, 0) AS checkedcount";
            sql += "  FROM (SELECT a.lotno, a.ckgroup, a.needcheckcount";
            sql += "          FROM tbloqclot2ckgroup a";
            sql += "         WHERE a.lotno = '" + lotNo + "') a,";
            sql += "       (SELECT   ckgroup, COUNT (DISTINCT rcard) AS checkedcount";
            sql += "            FROM tbloqccardlotcklist";
            sql += "           WHERE lotno = '" + lotNo + "'";
            sql += "        GROUP BY ckgroup) b";
            sql += " WHERE a.ckgroup = b.ckgroup(+)";
            sql += " ORDER BY a.ckgroup";

            return this.DataProvider.CustomQuery(typeof(OQCLot2CheckGroup), new SQLCondition(sql));
        }

        public object[] GetDataSourceForOffLot(string lotNo)
        {
            string sql = "";
            sql += "SELECT DISTINCT a.rcard AS rcard, a.cartoncode AS cartoncode,";
            sql += "       NVL (b.palletcode, '') AS palletcode, NVL (c.mdate, 0) AS mdate2,";
            sql += "       NVL (c.mtime, 0) AS mtime2, d.mdate AS mdate, d.mtime AS mtime,";
            sql += "       CASE NVL(e.checkseq,0)";
            sql += "          WHEN 0";
            sql += "             THEN 'N'";
            sql += "          ELSE 'Y'";
            sql += "       END AS flag, e.status as status";
            sql += "  FROM tblsimulationreport a,";
            sql += "       tblpallet2rcard b,";
            sql += "       tblpallet c,";
            sql += "       tbllot2card d,";
            sql += "       tbllot2cardcheck e";
            sql += " WHERE a.lotno = '" + lotNo + "'";
            sql += "   AND a.rcard = b.rcard(+)";
            sql += "   AND b.palletcode = c.palletcode(+)";
            sql += "   AND a.rcard = d.rcard";
            sql += "   AND a.lotno = d.lotno";
            sql += "   AND a.rcard = e.rcard(+)";
            sql += "   AND a.lotno = e.lotno(+)";
            sql += " ORDER BY palletcode,rcard";

            return this.DataProvider.CustomQuery(typeof(OQCCardCartonAndPallet), new SQLCondition(sql));
        }

        #region OQCQuery DataSource
        public object[] GetCheckGroupListInOQCQuery(string lotNo)
        {
            string sql = "";
            sql += " select a.lotno AS lotno, a.ckgroup AS ckgroup, a.needcheckcount AS needcheckcount, NVL (b.checkedcount, 0) AS checkedcount ";
            sql += "   FROM (SELECT DISTINCT a.lotno, a.ckgroup, a.needcheckcount";
            sql += "                FROM tbloqclot2ckgroup a";
            sql += "               WHERE a.lotno = '" + lotNo + "'  AND a.ckgroup IN (SELECT DISTINCT ckgroup FROM tbloqcckgroup)) a,";
            sql += "        (SELECT   DISTINCT ckgroup, COUNT (DISTINCT rcard) AS checkedcount";
            sql += "                FROM tbloqccardlotcklist  ";
            sql += "               WHERE lotno ='" + lotNo + "'";
            sql += "              GROUP BY ckgroup) b ";
            sql += "   WHERE a.ckgroup = b.ckgroup(+)";
            sql += "   order by a.ckgroup";
            return this.DataProvider.CustomQuery(typeof(OQCLot2CheckGroup), new SQLCondition(sql));
        }

        //public object[] GetOQCcardlotcklist22InOQCQuery(string lotNo)
        //{
        //    string sql = "";
        //    sql += " SELECT DISTINCT a.ckgroup AS CHECKGROUP, b.checkseq AS checkseq,";
        //    sql += "                 b.rcard AS rcard,b.rcardseq as rcardseq, b.status AS status, b.mdate AS mdate,";
        //    sql += "                 b.mtime AS mtime, b.muser AS muser ";
        //    sql += "        FROM tbloqccardlotcklist a, tbllot2cardcheck b";
        //    sql += "        WHERE b.rcard(+) = a.rcard   AND b.lotno(+) = '" + lotNo + "'   AND a.lotno = '" + lotNo + "'";
        //    sql += "            and a.checkseq=b.checkseq(+)  AND a.rcardseq=b.rcardseq(+)";
        //    sql += "        ORDER BY a.ckgroup, b.checkseq, b.rcard, b.rcardseq";
        //    return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheckAndCheckGroup), new SQLCondition(sql));
        //}

        //public object[] GettOQCLotcard2ErrorCode23InOQCQuery(string lotNo)
        //{
        //    string sql = "";
        //    sql += " SELECT DISTINCT a.ckgroup, a.checkseq, a.rcard, a.rcardseq, b.ecode, c.ecdesc";
        //    sql += "        FROM tbloqccardlotcklist a, tbloqclotcard2errorcode b, tblec c ";
        //    sql += "      WHERE a.rcard = b.rcard(+) and a.lotno = '" + lotNo + "' AND a.rcardseq = b.rcardseq(+)";
        //    sql += "             AND a.lotno = b.lotno(+)   AND a.lotseq = b.lotseq(+)   AND a.mocode = b.mocode(+) ";
        //    sql += "                 AND a.checkseq = b.checkseq(+)  AND b.ecode = c.ecode(+)";
        //    sql += "     ORDER BY  a.ckgroup, a.checkseq";
        //    return this.DataProvider.CustomQuery(typeof(OQCLotCard2ErrorCodeAndECDESC), new SQLCondition(sql));
        //}

        public object Gettbllot2cardcheckInOQCQuery(string lotNo)
        {
            string sql = "";
            sql += " SELECT a.ngsamplecount AS ngsamplecount, b.samplecount as samplecount";
            sql += "       FROM (SELECT COUNT (DISTINCT rcard) AS ngsamplecount";
            sql += "                FROM tbllot2cardcheck";
            sql += "             WHERE lotno = '" + lotNo + "' AND status = 'NG') a,";
            sql += "            (SELECT COUNT (DISTINCT rcard) AS samplecount";
            sql += "                 FROM tbllot2cardcheck";
            sql += "             WHERE lotno = '" + lotNo + "') b";
            return this.DataProvider.CustomQuery(typeof(OQCCheckSample), new SQLCondition(sql))[0];
        }

        public object[] GetGradeAndNGCountINOQCQuery(string lotNo)
        {
            string sql = "";
            sql += " SELECT grade, COUNT (rcard) as NGCount";
            sql += "     FROM tbloqccardlotcklist ";
            sql += "    WHERE lotno = '" + lotNo + "'";
            sql += " GROUP BY grade";
            sql += " ORDER BY grade";
            return this.DataProvider.CustomQuery(typeof(OQCCheckGradeAndCount), new SQLCondition(sql));
        }

        public object[] GetOQCcardLotcklistINOQCQuery(string lotNo)
        {
            string sql = "";
            sql += "SELECT grade,ckgroup, ckitemcode, COUNT (rcard) as NGCount";
            sql += "     FROM tbloqccardlotcklist";
            sql += "    WHERE lotno = '" + lotNo + "'";
            sql += "     AND grade IN ('AGrade', 'BGrade', 'CGrade', 'ZGrade')";
            sql += " GROUP BY grade,ckgroup, ckitemcode";
            sql += " ORDER BY grade,COUNT (rcard) DESC, ckgroup, ckitemcode";
            return this.DataProvider.CustomQuery(typeof(OQCCheckListAndCount), new SQLCondition(sql));
        }


        public object[] GetRcardFromOQCLotCard2ErrorCodeByLotNo(string lotNo)
        {
            string sql = "";
            sql += "select distinct rcard from tbloqclotcard2errorcode where lotno='" + lotNo + "' order by rcard";
            return this.DataProvider.CustomQuery(typeof(OQCLotCard2ErrorCode), new SQLCondition(sql));
        }


        public object[] GetCheckGroupAndRcard(string lotNo)
        {
            string sql = "";
            sql += "SELECT DISTINCT tbloqccardlotcklist.ckgroup, tbloqccardlotcklist.rcard,";
            sql += "                tbloqccardlotcklist.RESULT, tbllot2cardcheck.mdate, tbllot2cardcheck.mtime,tbllot2cardcheck.muser";
            sql += "         FROM tbllot2cardcheck, tbloqccardlotcklist";
            sql += "        WHERE tbllot2cardcheck.lotno = '" + lotNo + "'   AND tbloqccardlotcklist.lotno = '" + lotNo + "'";
            sql += "                AND tbllot2cardcheck.rcard = tbloqccardlotcklist.rcard  AND tbllot2cardcheck.checkseq = tbloqccardlotcklist.checkseq";
            sql += "        ORDER BY tbloqccardlotcklist.ckgroup, tbllot2cardcheck.mdate,  tbllot2cardcheck.mtime";
            return this.DataProvider.CustomQuery(typeof(OQCLot2CardCheckAndCheckGroup), new SQLCondition(sql));
        }


        public object[] GetErrorCodeFromTblecAndtbloqclotcard2errorcodeByLotNo(string lotNo)
        {
            string sql = "";
            sql += "select distinct a.rcard,a.ecode,a.muser,a.mdate,a.mtime,b.ecdesc";
            sql += "            from tbloqclotcard2errorcode a,tblec b";
            sql += " where a.lotno='" + lotNo + "' and a.ecode = b.ecode";
            sql += "        ORDER BY a.rcard,a.ecode";
            return this.DataProvider.CustomQuery(typeof(OQCLotCard2ErrorCodeAndECDESC), new SQLCondition(sql));
        }
        #endregion

        #region Lot2Carton
        public Lot2Carton CreateNewLot2Carton()
        {
            return new Lot2Carton();
        }

        public void AddLot2Carton(Lot2Carton lot2Carton)
        {
            this._helper.AddDomainObject(lot2Carton);
        }
        public void UpdateLot2Carton(Lot2Carton lot2Carton)
        {
            this._helper.UpdateDomainObject(lot2Carton);
        }

        public void DeleteLot2Carton(Lot2Carton lot2Carton)
        {
            this._helper.DeleteDomainObject(lot2Carton);
        }

        public void AddLot2Carton(Lot2Carton[] lot2Carton)
        {
            if (lot2Carton != null)
            {
                foreach (Lot2Carton newlot2Carton in lot2Carton)
                {
                    this._helper.AddDomainObject(newlot2Carton);
                }
            }
        }

        public void DeleteLot2Carton(Lot2Carton[] lot2Carton)
        {
            foreach (Lot2Carton DeleteLot2Carton in lot2Carton)
            {
                this._helper.DeleteDomainObject(DeleteLot2Carton);
            }
        }

        public object GetLot2Carton(string serial)
        {
            return this.DataProvider.CustomSearch(typeof(Lot2Carton), new object[] { serial });
        }

        public object[] GetLot2CartonByLot(string lot)
        {
            string sql = "SELECT {0} FROM tbllot2carton WHERE oqclot='" + lot + "' order by cartonno";
            return this.DataProvider.CustomQuery(typeof(Lot2Carton),
                new SQLCondition(string.Format(sql,
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(Lot2Carton)))));

        }

        public object GetLot2CartonByCartonNo(string cartonNo)
        {
            string sql = "SELECT {0} FROM tbllot2carton WHERE cartonno='" + cartonNo + "' ORDER BY adddate DESC,addtime DESC";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Lot2Carton)));
            object[] lot2CartonList = this.DataProvider.CustomQuery(typeof(Lot2Carton), new SQLCondition(sql));
            if (lot2CartonList == null)
            {
                return null;
            }

            return lot2CartonList[0];

        }



        #endregion

        #region Lot2CartonLog
        public Lot2CartonLog CreateNewLot2CartonLog()
        {
            return new Lot2CartonLog();
        }

        public void AddLot2CartonLog(Lot2CartonLog lot2CartonLog)
        {
            this._helper.AddDomainObject(lot2CartonLog);
        }
        public void UpdateLot2CartonLog(Lot2CartonLog lot2CartonLog)
        {
            this._helper.UpdateDomainObject(lot2CartonLog);
        }

        public void DeleteLot2CartonLog(Lot2CartonLog lot2CartonLog)
        {
            this._helper.DeleteDomainObject(lot2CartonLog);
        }

        public void AddLot2CartonLog(Lot2CartonLog[] lot2CartonLog)
        {
            if (lot2CartonLog != null)
            {
                foreach (Lot2CartonLog newlot2CartonLog in lot2CartonLog)
                {
                    this._helper.AddDomainObject(newlot2CartonLog);
                }
            }
        }

        public void DeleteLot2CartonLog(Lot2CartonLog[] lot2CartonLog)
        {
            foreach (Lot2CartonLog Deletelot2CartonLog in lot2CartonLog)
            {
                this._helper.DeleteDomainObject(Deletelot2CartonLog);
            }
        }

        public object GetLot2CartonLog(string serial)
        {
            return this.DataProvider.CustomSearch(typeof(Lot2CartonLog), new object[] { serial });
        }

        public object[] GetLot2CartonLogByLot(string lot)
        {
            string sql = "SELECT {0} FROM tbllot2cartonlog WHERE oqclot='" + lot + "' order by cartonno";
            return this.DataProvider.CustomQuery(typeof(Lot2CartonLog),
                new SQLCondition(string.Format(sql,
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(Lot2CartonLog)))));

        }

        public object GetLot2CartonLogByCartonNo(string cartonNo)
        {
            string sql = "SELECT {0} FROM tbllot2cartonlog WHERE cartonno='" + cartonNo + "' ORDER BY adddate DESC,addtime DESC";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Lot2CartonLog)));
            object[] lot2CartonLogList = this.DataProvider.CustomQuery(typeof(Lot2CartonLog), new SQLCondition(sql));
            if (lot2CartonLogList == null)
            {
                return null;
            }

            return lot2CartonLogList[0];

        }

        public void UpdateLot2CartonLogWhenRemove(string LotNo, string cartonNo, string removeUser)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            string sql = string.Format(@"update tblLot2CartonLog set removeuser='{0}',removedate={1},
            removetime={2} where oqclot='{3}' and cartonNo='{4}' and removeuser is null", removeUser, dbDateTime.DBDate, dbDateTime.DBTime,
                                                                LotNo, cartonNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region AQL
        /// <summary>
        /// 
        /// </summary>
        public AQL CreateNewAQL()
        {
            return new AQL();
        }

        public void AddAQL(AQL aql)
        {
            this._helper.AddDomainObject(aql);
        }

        public void UpdateAQL(AQL aql)
        {
            this._helper.UpdateDomainObject(aql);
        }


        public void DeleteAQL(AQL aql)
        {
            this._helper.DeleteDomainObject(aql);
        }


        public void DeleteAQL(AQL[] aql)
        {
            foreach (AQL item in aql)
            {
                this._helper.DeleteDomainObject(aql);
            }
        }

        public object GetAQL(int seq, string aqlLevel)
        {
            return this.DataProvider.CustomSearch(typeof(AQL), new object[] { seq, aqlLevel });
        }
        #region
        public object GetOQCDetailByIQCNoAndCartonNo(string OQCNo, string CartonNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLOQCDETAIL WHERE OQCNO='{1}' AND CARTONNO='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetail)), OQCNo, CartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCDetail), new SQLCondition(sql));
            if (objs != null)
                return objs[0];
            return null;
        }
        #endregion


        //获取所有AQL标准 add by jinger 20160219
        public object[] GetAllAQL()
        {
            string sql = string.Format("SELECT {0} FROM TBLAQL ORDER BY AQLLEVEL", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)));
            return this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
        }

        public object GetAQLByLotSize(int lotSize, string sscode)
        {
            string sql = "SELECT a.* FROM tblaql a left join tblaql2Line b on a.aqllevel=b.aqllevel WHERE LOTSIZEMIN<=" + lotSize + " AND LOTSIZEMAX>=" + lotSize + " AND b.sscode='" + sscode + "'";
            object[] aqlList = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (aqlList != null)
            {
                return aqlList[0];
            }

            return null;
        }

        public object GetAQLByLevelAndLotSize(int lotSize, string aqlLevel)
        {
            string sql = string.Format("SELECT {0} FROM tblaql WHERE LOTSIZEMIN<=" + lotSize + " AND LOTSIZEMAX>=" + lotSize + " and aqllevel='" + aqlLevel + "'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)));
            object[] aqlList = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (aqlList != null)
            {
                return aqlList[0];
            }

            return null;
        }

        public object[] QueryAllAql(int inclusive, int exclusive)
        {
            string sql = string.Format("select  from tblaql ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)));

            return this.DataProvider.CustomQuery(typeof(AQL), new PagerCondition(sql, "aqllevel,aqlseq", inclusive, exclusive));
        }



        public object[] GetAqlForQuery(string aqllevel, int inclusive, int exclusive)
        {
            string sql = string.Format("select {0} from tblaql where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)));

            if (!String.IsNullOrEmpty(aqllevel))
            {
                sql += string.Format(" and UPPER(aqllevel) like '%{0}%'", aqllevel);
            }
            //sql += string.Format(" order by aqllevel ,lotsizemin");
            return this.DataProvider.CustomQuery(typeof(AQL), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }

        public int GetAqlCountForQuery(string aqllevel)
        {
            string sql = "select count(*) from tblaql where 1=1 ";
            if (!String.IsNullOrEmpty(aqllevel))
            {
                sql += string.Format(" and UPPER(aqllevel) like '%{0}%'", aqllevel);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        //public object[] QueryAllAql(int AqlSeq)
        //    //检查新增批量是否在别的批量区间，返回序号不再当前批序号的所有对象
        //{
        //    string sql = string.Format("select {0} from tblaql ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)));
        //    sql +=string .Format ( " where aqlseq not in {0}",AqlSeq );
        //    return this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
        //}
        public int GetIsRepeatLotSizeMin2MaxForUpdate(int LotSizeMin, int LotSizeMax, int AqlSeq, string AqlLevel)
        {
            string sql = " select count(*) from tblaql ";
            sql += string.Format(" where  (( lotsizemin <={0} and lotsizemax>={1} )", LotSizeMin, LotSizeMin);
            sql += string.Format(" or (lotsizemin <={0} and lotsizemax>={1}) ", LotSizeMax, LotSizeMax);
            sql += string.Format(" or (lotsizemin >={0} and lotsizemax<= {1})) ", LotSizeMin, LotSizeMax);
            sql += string.Format(" and aqlseq <> {0} ", AqlSeq);
            sql += string.Format(" and aqllevel = '{0}'", AqlLevel);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetIsRepeatLotSizeMin2MaxForAdd(int LotSizeMin, int LotSizeMax, string AqlLevel)
        {
            string sql = " select count(*) from tblaql ";
            sql += string.Format(" where  (( lotsizemin <={0} and lotsizemax>={1} )", LotSizeMin, LotSizeMin);
            sql += string.Format(" or (lotsizemin <={0} and lotsizemax>={1}) ", LotSizeMax, LotSizeMax);
            sql += string.Format(" or (lotsizemin >={0} and lotsizemax<= {1})) ", LotSizeMin, LotSizeMax);
            sql += string.Format(" and aqllevel = '{0}'", AqlLevel);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        public int QuerypAqlCount()
        {
            string sql = "select count(*) from tblaql ";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
        public int QueryAqlSeqIsRepeat(string AqlSeq)
        {
            string sql = string.Format("select count(*) from tblaql where aqlseq={0} ", AqlSeq);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetCheckItemByCheckGroupNew(string chGroup)
        {
            string sql = "select group2Item.ckgroup, list.ckitemcode,list.ckitemdesc,list.unit,list.setvaluemax,list.setvaluemin,list.eattribute1, list.MUser,list.MDate,list.Mtime from TBLOQCCKLIST list ";
            sql += " left join tbloqcckgroup2list group2Item on group2Item.Ckitemcode = list.ckitemcode  ";
            sql += " where ckgroup ='" + chGroup + "'";

            return this.DataProvider.CustomQuery(typeof(OQCCheckListQuery), new SQLCondition(sql));
        }

        public object[] GetAllAQLLevel()
        {
            return this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition("select distinct aqllevel from TBLAQL order by aqllevel"));
        }

        #endregion

        #region OQC-- OQC单  add by jinger 2016-03-01
        /// <summary>
        /// TBLOQC-- OQC单 
        /// </summary>
        public Domain.OQC.OQC CreateNewOqc()
        {
            return new Domain.OQC.OQC();
        }

        public void AddOQC(Domain.OQC.OQC oqc)
        {
            this._helper.AddDomainObject(oqc);
        }

        public void DeleteOQC(Domain.OQC.OQC oqc)
        {
            this._helper.DeleteDomainObject(oqc);
        }

        public void UpdateOQC(Domain.OQC.OQC oqc)
        {
            this._helper.UpdateDomainObject(oqc);
        }

        public object GetOQC(string Oqcno)
        {
            return this.DataProvider.CustomSearch(typeof(Domain.OQC.OQC), new object[] { Oqcno });
        }

        public object[] QueryOQC(string pickNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno,
          string dqmCode, string cusMCode, string gfhwItemCode, int inclusive, int exclusive)
        {

            return this.QueryOQC(pickNo, "", oqcNo, status, bAppDate, eAppDate, cartonno, sn, invno, dqmCode, cusMCode, gfhwItemCode, inclusive, exclusive);
        }

        public object[] QueryOQC2(string[] userCodes, string pickNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno,
        string dqmCode, string cusMCode, string gfhwItemCode, string storageCode, int inclusive, int exclusive)
        {
            return QueryOQC2(userCodes, string.Empty, pickNo, oqcNo, status, bAppDate, eAppDate, cartonno, sn, invno, dqmCode, cusMCode, gfhwItemCode, storageCode, inclusive, exclusive);

        }

        public int QueryOQC2Count(string[] userCodes, string pickNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno,
      string dqmCode, string cusMCode, string gfhwItemCode, string storageCode)
        {
            return QueryOQC2Count(userCodes, string.Empty, pickNo, oqcNo, status, bAppDate, eAppDate, cartonno, sn, invno, dqmCode, cusMCode, gfhwItemCode, storageCode);

        }


        public object[] QueryOQC2(string[] userCodes, string carinvno, string pickNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno,
         string dqmCode, string cusMCode, string gfhwItemCode, string storageCode, int inclusive, int exclusive)
        {


            string sql = string.Empty;


            sql = @"SELECT A.QCSTATUS,A.OQCNO,A.OQCTYPE,A.PICKNO,A.CARINVNO,A.STATUS,A.APPDATE,APPTIME,A.CUSER,B.PICKTYPE,B.INVNO,B.GFFLAG,C.APPQTY,C.NGQTY,C.RETURNQTY,B.storagecode FROM TBLOQC A inner join TBLPICK B ON A.PICKNO=B.PICKNO LEFT JOIN
(SELECT OQCNO,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL group by OQCNO ) C ON A.OQCNO=C.OQCNO  WHERE
A.PICKNO IN (
 SELECT DISTINCT PICKNO FROM
 (SELECT PICKNO FROM TBLPICK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userCodes) + @")) UNION 
 SELECT PICKNO FROM TBLPICK WHERE INSTORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userCodes) + @"))) T 
 ) ";

            if (!string.IsNullOrEmpty(cartonno))
            {

                sql += @" AND A.OQCno in (select distinct OQCNO from TBLOQCDETAIL where cartonno='" + cartonno + "' )";
            }

            if (!string.IsNullOrEmpty(sn))
            {

                sql += @"AND  A.OQCNO in( select distinct OQCNO from TBLOQCDETAILSN where SN='" + sn + "')";
            }



            //  LEFT JOIN (SELECT DISTINCT PICKNO PICKCODE, CUSTMCODE HWMCODE,DQMCODE FROM TBLPICKDETAIL) PM ON A.PICKNO = PM.PICKCODE 
            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select distinct OQCNO from TBLOQCDETAIL where dqmCode='{0}' ) ", dqmCode);
            }
            if (!string.IsNullOrEmpty(gfhwItemCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select distinct OQCNO from TBLOQCDETAIL where gfhwitemcode='{0}' ) ", gfhwItemCode);
            }
            if (!string.IsNullOrEmpty(cusMCode))
            {
                sql += string.Format(" AND a.PICKNO   in ( select distinct PICKNO from TBLPICKDETAIL where (cusitemcode='{0}' or VENDERITEMCODE='{0}' or custmcode='{0}') ) ", cusMCode);
            }
            #endregion

            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND B.invno ='{0}'", invno);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND B.storageCode ='{0}'", storageCode);
            }

            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND A.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(@" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (!string.IsNullOrEmpty(carinvno))
                sql += string.Format(@" AND A.CARINVNO = '{0}'", carinvno);
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.CustomQuery(typeof(OQCExt1), new PagerCondition(sql, "A.OQCNO DESC", inclusive, exclusive));
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
        public int QueryOQC2Count(string[] userGroups, string carinvno, string pickNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno
          , string dqmCode, string cusMCode, string gfhwItemCode, string storageCode)
        {
            string sql = string.Empty;


            sql = @"SELECT COUNT(*) FROM TBLOQC A inner join TBLPICK B ON A.PICKNO=B.PICKNO LEFT JOIN
(SELECT OQCNO,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL group by OQCNO ) C ON A.OQCNO=C.OQCNO  WHERE
A.PICKNO IN (
 SELECT DISTINCT PICKNO FROM
 (SELECT PICKNO FROM TBLPICK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in(" + SqlFormat(userGroups) + @")) UNION 
 SELECT PICKNO FROM TBLPICK WHERE INSTORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @"))) T 
 ) ";

            if (!string.IsNullOrEmpty(cartonno))
            {

                sql += @" AND A.OQCno in (select distinct OQCNO from TBLOQCDETAIL where cartonno='" + cartonno + "' )";
            }

            if (!string.IsNullOrEmpty(sn))
            {

                sql += @"AND  A.OQCNO in( select distinct OQCNO from TBLOQCDETAILSN where SN='" + sn + "')";
            }

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND B.storageCode ='{0}'", storageCode);
            }

            //  LEFT JOIN (SELECT DISTINCT PICKNO PICKCODE, CUSTMCODE HWMCODE,DQMCODE FROM TBLPICKDETAIL) PM ON A.PICKNO = PM.PICKCODE 
            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select distinct OQCNO from TBLOQCDETAIL where dqmCode='{0}' ) ", dqmCode);
            }
            if (!string.IsNullOrEmpty(gfhwItemCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select distinct OQCNO from TBLOQCDETAIL where gfhwitemcode='{0}' ) ", gfhwItemCode);
            }
            if (!string.IsNullOrEmpty(cusMCode))
            {
                sql += string.Format(" AND a.PICKNO   in ( select distinct PICKNO from TBLPICKDETAIL where cusitemcode='{0}' ) ", cusMCode);
            }
            #endregion

            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND B.invno ='{0}'", invno);
            }
            if (!string.IsNullOrEmpty(carinvno))
                sql += string.Format(@" AND A.CARINVNO = '{0}'", carinvno);
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND A.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(@" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public int QueryOQCCount(string pickNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno
            , string dqmCode, string cusMCode, string gfhwItemCode)
        {
            return this.QueryOQCCount(pickNo, "", oqcNo, status, bAppDate, eAppDate, cartonno, sn, invno, dqmCode, cusMCode, gfhwItemCode);
        }


        public object[] QueryOQC(string pickNo, string carInvNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno,
              string dqmCode, string cusMCode, string gfhwItemCode, int inclusive, int exclusive)
        {
            string oqcNostr = "";
            if (!string.IsNullOrEmpty(oqcNo))
            {
                oqcNostr += string.Format(" AND OQCNO ='{0}' ", oqcNo);
            }
            string sql = string.Empty;

            if (!string.IsNullOrEmpty(cartonno) && string.IsNullOrEmpty(sn))
            {
                sql = string.Format(@" SELECT {0} FROM TBLOQC A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE 1=1 {1} and cartonno='{2}' GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                
                                                WHERE 1=1 ",
                                         DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCExt1)), oqcNostr, cartonno);

            }

            if (string.IsNullOrEmpty(cartonno) && !string.IsNullOrEmpty(sn))
            {
                sql = string.Format(@" SELECT {0} FROM TBLOQC A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY  ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE 1=1 {1}  GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                
                                                WHERE 1=1 and  OQCNO in( select OQCNO from TBLOQCDETAILSN where SN='{2}') ",
                                         DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCExt1)), oqcNostr, sn);

            }

            if (!string.IsNullOrEmpty(cartonno) && !string.IsNullOrEmpty(sn))
            {
                sql = string.Format(@" SELECT {0} FROM TBLOQC  A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY  ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE 1=1 {1} and cartonno='{2}' GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                
                                                WHERE 1=1 and  OQCNO in( select OQCNO from TBLOQCDETAILSN where SN='{3}')",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCExt1)), oqcNostr, cartonno, sn);

            }

            if (string.IsNullOrEmpty(sql))
                sql = string.Format(@" SELECT {0} FROM TBLOQC A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY  ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE 1=1 {1} GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                                WHERE 1=1 ",
                                       DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCExt1)), oqcNostr);

            //  LEFT JOIN (SELECT DISTINCT PICKNO PICKCODE, CUSTMCODE HWMCODE,DQMCODE FROM TBLPICKDETAIL) PM ON A.PICKNO = PM.PICKCODE 
            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select OQCNO from TBLOQCDETAIL where dqmCode='{0}' ) ", dqmCode);
            }
            if (!string.IsNullOrEmpty(gfhwItemCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select OQCNO from TBLOQCDETAIL where gfhwitemcode='{0}' ) ", gfhwItemCode);
            }
            if (!string.IsNullOrEmpty(cusMCode))
            {
                sql += string.Format(" AND a.PICKNO   in ( select PICKNO from TBLPICKDETAIL where cusitemcode='{0}' ) ", cusMCode);
            }
            #endregion

            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND p.invno ='{0}'", invno);
            }

            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND A.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO ='{0}'", carInvNo);
            }

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(@" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.CustomQuery(typeof(OQCExt1), new PagerCondition(sql, "A.OQCNO DESC", inclusive, exclusive));
        }

        public int QueryOQCCount(string pickNo, string carInvNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonno, string sn, string invno,
              string dqmCode, string cusMCode, string gfhwItemCode)
        {
            string oqcNostr = "";
            if (!string.IsNullOrEmpty(oqcNo))
            {
                oqcNostr += string.Format(" AND OQCNO ='{0}' ", oqcNo);
            }
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(cartonno) && string.IsNullOrEmpty(sn))
            {
                sql = string.Format(@" SELECT cOUNT(1) FROM TBLOQC A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY  ,  NVL(SUM(ReturnQty),0) ReturnQty  FROM TBLOQCDETAIL WHERE 1=1 {0} and cartonno='{1}' GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                
                                                WHERE 1=1 ",
                                         oqcNostr, cartonno);

            }

            if (string.IsNullOrEmpty(cartonno) && !string.IsNullOrEmpty(sn))
            {
                sql = string.Format(@" SELECT count(1) FROM TBLOQC A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY   ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE 1=1 {0}  GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                
                                                WHERE 1=1 and  A.OQCNO in( select OQCNO from TBLOQCDETAILSN where SN='{1}')",
                                         oqcNostr, sn);

            }

            if (!string.IsNullOrEmpty(cartonno) && !string.IsNullOrEmpty(sn))
            {
                sql = string.Format(@" SELECT COUNT(1) FROM TBLOQC A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY  ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE 1=1 {0} and cartonno='{1}' GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                
                                                WHERE 1=1 AND A.OQCNO in( select OQCNO from TBLOQCDETAILSN where SN='{2}') A",
                                         oqcNostr, cartonno, sn);

            }

            if (string.IsNullOrEmpty(sql))
                sql = string.Format(@" SELECT COUNT(1) FROM TBLOQC A
                                                inner JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                inner  JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY  ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE 1=1 {0} GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                                WHERE 1=1 ",
                                        oqcNostr);


            #region add by sam 2016年8月4日
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select OQCNO from TBLOQCDETAIL where dqmCode='{0}' ) ", dqmCode);
            }
            if (!string.IsNullOrEmpty(gfhwItemCode))
            {
                sql += string.Format(" AND a.OQCNO   in ( select OQCNO from TBLOQCDETAIL where gfhwitemcode='{0}' ) ", gfhwItemCode);
            }
            if (!string.IsNullOrEmpty(cusMCode))
            {
                sql += string.Format(" AND a.PICKNO   in ( select PICKNO from TBLPICKDETAIL where cusitemcode='{0}' ) ", cusMCode);
            }
            #endregion

            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(" AND p.invno ='{0}'", invno);
            }

            //LEFT JOIN (SELECT DISTINCT PICKNO PICKCODE, CUSTMCODE HWMCODE,DQMCODE FROM TBLPICKDETAIL) PM ON A.PICKNO = PM.PICKCODE 
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND A.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO ='{0}'", carInvNo);
            }

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(@" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        public object[] QueryOQC1(string pickNo, string carInvNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonNo, string sn, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT {0} FROM TBLOQC A
                                                LEFT JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCExt1)));
            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(@"  LEFT JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY ,  NVL(SUM(ReturnQty),0) ReturnQty FROM TBLOQCDETAIL WHERE OQCNO = '{0}' GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE WHERE 1=1 ", oqcNo);
            }
            else
            {
                sql += string.Format(@"  LEFT JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY,  NVL(SUM(ReturnQty),0) ReturnQty  FROM TBLOQCDETAIL GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE WHERE 1=1 ");
            }
            //sql+=string.Format(@"  LEFT JOIN (SELECT DISTINCT PICKNO PICKCODE, CUSTMCODE HWMCODE,DQMCODE FROM TBLPICKDETAIL) PM ON A.PICKNO = PM.PICKCODE 
            //WHERE 1=1 ");
            //  LEFT JOIN (SELECT DISTINCT PICKNO PICKCODE, CUSTMCODE HWMCODE,DQMCODE FROM TBLPICKDETAIL) PM ON A.PICKNO = PM.PICKCODE 

            #region MyRegion
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format("   A.OQCNO in (select OQCNO from TBLOQCDETAIL where CARTONNO='{0}' )", cartonNo);
            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" A.OQCNO in (select OQCNO from TBLOQCDETAILSN where sn='{0}' ) ", sn);
            }
            #endregion

            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND A.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO ='{0}'", carInvNo);
            }

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(@" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.CustomQuery(typeof(OQCExt1), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }
        //OQC送检单总行数
        /// <summary>
        ///OQC送检单总行数
        /// </summary>
        /// <param name="pickNo">拣货任务令号</param>
        /// <param name="carInvNo">发货箱单号</param>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="status">OQC检验单状态</param>
        /// <param name="bAppDate">送检日期开始</param>
        /// <param name="eAppDate">送检日期结束</param>
        /// <returns></returns>
        public int QueryOQCCount1(string pickNo, string carInvNo, string oqcNo, string status, int bAppDate, int eAppDate, string cartonNo, string sn)
        {
            string sql = string.Format(@" SELECT COUNT(1) FROM TBLOQC A
                                                LEFT JOIN (SELECT PICKNO PICKCODE,PICKTYPE,INVNO,GFFLAG FROM TBLPICK) P ON A.PICKNO = P.PICKCODE
                                                LEFT JOIN (SELECT OQCNO OQCCODE,NVL(SUM(QTY),0) APPQTY,NVL(SUM(NGQTY),0) NGQTY ,  NVL(SUM(ReturnQty),0) ReturnQty  FROM TBLOQCDETAIL WHERE OQCNO = '{0}' GROUP BY OQCNO) B ON A.OQCNO=B.OQCCODE 
                                               
                                                WHERE 1=1 ", oqcNo);
            //LEFT JOIN (SELECT DISTINCT PICKNO PICKCODE, CUSTMCODE HWMCODE,DQMCODE FROM TBLPICKDETAIL) PM ON A.PICKNO = PM.PICKCODE 
            #region MyRegion
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format("   A.OQCNO in (select OQCNO from TBLOQCDETAIL where CARTONNO='{0}' )", cartonNo);
            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" A.OQCNO in (select OQCNO from TBLOQCDETAILSN where sn='{0}' ) ", sn);
            }
            #endregion
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND A.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO ='{0}'", carInvNo);
            }

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(@" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND A.STATUS = '{0}'", status);
            }
            if (bAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE >= {0}", bAppDate);
            }
            if (eAppDate > 0)
            {
                sql += string.Format(@" AND A.APPDATE <= {0}", eAppDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        //查询OQC送检单
        /// <summary>
        ///查询OQC送检单
        /// </summary>
        /// <param name="pickNo">拣货任务令号</param>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="status">OQC检验单状态</param>
        /// <param name="bAppDate">送检日期开始</param>
        /// <param name="eAppDate">送检日期结束</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>


        //OQC送检单总行数
        /// <summary>
        ///OQC送检单总行数
        /// </summary>
        /// <param name="pickNo">拣货任务令号</param>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="status">OQC检验单状态</param>
        /// <param name="bAppDate">送检日期开始</param>
        /// <param name="eAppDate">送检日期结束</param>
        /// <returns></returns>


        // 根据条件更新OQC单中OQCTYPE，status，QCSTATUS
        /// <summary>
        /// 根据条件更新OQC单中OQCTYPE，status，QCSTATUS
        /// </summary>
        /// <param name="oqcType">检验方式</param>
        /// <param name="status">单据状态</param>
        /// <param name="qcStatus">OQC状态</param>
        /// <param name="oqcNoCondition">OQC单号(条件)</param>
        /// <param name="ocStatusCondition">OQC状态（条件）</param>
        public void UpdateOQC(string oqcType, string status, string qcStatus, string oqcNoCondition)
        {
            string sql = string.Format(@"UPDATE TBLOQC SET OQCTYPE='{0}',STATUS='{1}', QCSTATUS='{2}' WHERE OQCNO='{3}'", oqcType, status, qcStatus, oqcNoCondition);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        // 根据OQC送检单号找到发货箱单号获取OQC送检单
        /// <summary>
        /// 根据OQC送检单号找到发货箱单号获取OQC送检单
        /// </summary>
        /// <param name="oqcNo">OQC送检单号</param>
        /// <returns></returns>
        public object[] GetOQCByOqcNo(string oqcNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLOQC WHERE CARINVNO= (SELECT CARINVNO FROM TBLOQC WHERE OQCNO ='{1}')",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.OQC.OQC)), oqcNo);
            object[] objOQC = this.DataProvider.CustomQuery(typeof(Domain.OQC.OQC), new SQLCondition(sql));
            if (objOQC != null && objOQC.Length > 0)
            {
                return objOQC;
            }
            return null;
        }

        #endregion

        #region OQCDetail-- OQC单明细  add by jinger 2016-03-01
        /// <summary>
        /// TBLOQCDETAIL-- OQC单明细 
        /// </summary>
        public OQCDetail CreateNewOQCDetail()
        {
            return new OQCDetail();
        }

        public void AddOQCDetail(OQCDetail oqcdetail)
        {
            this._helper.AddDomainObject(oqcdetail);
        }

        public void DeleteOQCDetail(OQCDetail oqcdetail)
        {
            this._helper.DeleteDomainObject(oqcdetail);
        }

        public void UpdateOQCDetail(OQCDetail oqcdetail)
        {
            this._helper.UpdateDomainObject(oqcdetail);
        }

        public object GetOQCDetail(string Carinvno, string Oqcno, string Cartonno, string MCode)
        {
            return this.DataProvider.CustomSearch(typeof(OQCDetail), new object[] { Carinvno, Oqcno, Cartonno, MCode });
        }

        //根据OQC检验单号获取送检单明细信息
        /// <summary>
        /// 根据OQC检验单号获取送检单明细信息
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <returns></returns>
        public object[] GetOQCDetailByOqcNo(string oqcNo)
        {
            return this.DataProvider.CustomQuery(typeof(OQCDetail), new SQLCondition(string.Format("SELECT {0} FROM TBLOQCDETAIL WHERE OQCNO='{1}' ",
                                                                                                            DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetail)),
                                                                                                            oqcNo))
                                               );
        }

        //查询OQC送检单明细
        /// <summary>
        /// 查询OQC送检单明细
        /// </summary>
        /// <param name="oqcNo">OQC送检单号</param>
        /// <param name="pickNo">拣货任务令号</param>
        /// <param name="pickType">出库类型</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="carInvNo">发货箱单号</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        //        public object[] QueryOQCDetail(string oqcNo,string pickNo,string pickType, string cartonNo,string carInvNo, int inclusive, int exclusive)
        //        {
        //            string sql = string.Format(@"SELECT A.*,B.OQCTYPE,M.MCONTROLTYPE,P.GFFLAG,D.DQSITEMCODE,D.HWCODEQTY 
        //                                FROM TBLOQCDETAIL A
        //                                LEFT JOIN TBLOQC B ON A.OQCNO=B.OQCNO
        //                                LEFT JOIN TBLMATERIAL M ON A.DQMCODE=M.DQMCODE
        //                                LEFT JOIN TBLPICK P ON B.PICKNO = P.PICKNO
        //                                LEFT JOIN (SELECT DISTINCT PICKNO,DQSITEMCODE,HWCODEQTY FROM TBLPICKDETAIL WHERE PICKNO='{0}') D ON B.PICKNO =D.PICKNO 
        //                                WHERE 1=1 ",pickNo);

        //            if (!string.IsNullOrEmpty(oqcNo))
        //            {
        //                sql += string.Format(" AND A.OQCNO ='{0}'", oqcNo);
        //            }
        //            if (!string.IsNullOrEmpty(pickNo))
        //            {
        //                sql += string.Format(" AND B.PICKNO='{0}'", pickNo);
        //            }
        //            if (!string.IsNullOrEmpty(pickType))
        //            {
        //                sql += string.Format(" AND P.PICKTYPE ='{0}'", pickType);
        //            }
        //            if (!string.IsNullOrEmpty(cartonNo))
        //            {
        //                sql += string.Format(" AND A.CARTONNO LIKE '%{0}%'", cartonNo);
        //            }

        //            if (!string.IsNullOrEmpty(carInvNo))
        //            {
        //                sql += string.Format(" AND A.CARINVNO LIKE '%{0}%'", carInvNo);
        //            }
        //            return this.DataProvider.CustomQuery(typeof(OQCDetailExt), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        //        }





        public object[] QueryOQCDetail(string oqcNo, string pickNo, string pickType, string cartonNo, string carInvNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT A.*,B.OQCTYPE,M.MCONTROLTYPE,P.GFFLAG,pd.Cusitemcode  HwItemCode
                                FROM TBLOQCDETAIL A
                                LEFT JOIN TBLOQC B ON A.OQCNO=B.OQCNO
                                LEFT JOIN TBLMATERIAL M ON A.DQMCODE=M.DQMCODE
                                LEFT JOIN TBLPICK P ON B.PICKNO = P.PICKNO
                                LEFT JOIN (SELECT DISTINCT PICKNO FROM TBLPICKDETAIL WHERE PICKNO='{0}') D ON B.PICKNO =D.PICKNO 
                                LEFT JOIN tblpickdetail pd ON B.PICKNO =pd.pickno and a.dqmcode=pd.dqmcode
                                WHERE 1=1 ", pickNo);

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(" AND A.OQCNO ='{0}'", oqcNo);
            }
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND B.PICKNO='{0}'", pickNo);
            }
            if (!string.IsNullOrEmpty(pickType))
            {
                sql += string.Format(" AND P.PICKTYPE ='{0}'", pickType);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO LIKE '%{0}%'", cartonNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO LIKE '%{0}%'", carInvNo);
            }
            return this.DataProvider.CustomQuery(typeof(OQCDetailExt), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }

        //OQC送检单明细总行数
        /// <summary>
        ///OQC送检单明细总行数
        /// </summary>
        /// <param name="oqcNo">OQC送检单号</param>
        /// <param name="pickNo">拣货任务令号</param>
        /// <param name="pickType">出库类型</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="carInvNo">发货箱单号</param>
        /// <returns></returns>
        public int QueryOQCDetailCount(string oqcNo, string pickNo, string pickType, string cartonNo, string carInvNo)
        {
            string sql = string.Format(@" SELECT COUNT(1) 
                                 FROM TBLOQCDETAIL A
                                LEFT JOIN TBLOQC B ON A.OQCNO=B.OQCNO
                                LEFT JOIN TBLMATERIAL M ON A.DQMCODE=M.DQMCODE
                                LEFT JOIN TBLPICK P ON B.PICKNO = P.PICKNO
                                LEFT JOIN (SELECT DISTINCT PICKNO FROM TBLPICKDETAIL WHERE PICKNO='{0}') D ON B.PICKNO =D.PICKNO 
                                WHERE 1=1 ", pickNo);
            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(" AND A.OQCNO ='{0}'", oqcNo);
            }
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND B.PICKNO='{0}'", pickNo);
            }
            if (!string.IsNullOrEmpty(pickType))
            {
                sql += string.Format(" AND P.PICKTYPE ='{0}'", pickType);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO LIKE '%{0}%'", cartonNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO LIKE '%{0}%'", carInvNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //根据OQC检验单号获取送检单明细中缺陷品数总数量
        /// <summary>
        /// 根据OQC检验单号获取送检单明细中缺陷品数总数量
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <returns></returns>
        public int GetSumNgQtyFromOQCDetail(string oqcNo)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLOQCDETAIL WHERE OQCNO='{0}'", oqcNo);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        // 根据条件更新送检单明细中QCSTATUS和QCPASSQTY
        /// <summary>
        /// 根据条件更新送检单明细中QCSTATUS和QCPASSQTY
        /// </summary>
        /// <param name="qcStatus">OQC状态</param>
        /// <param name="oqcNoCondition">OQC单号</param>
        /// <param name="qcStatusCondition">OQC状态(条件)</param>
        public void UpdateOQCDetail(string qcStatus, string oqcNoCondition, string qcStatusCondition)
        {
            string sql = string.Format(@"UPDATE TBLOQCDETAIL SET QCSTATUS='{0}',QCPASSQTY = QTY WHERE OQCNO='{1}' AND ( QCSTATUS <> '{2}'  or QCSTATUS is null )", qcStatus, oqcNoCondition, qcStatusCondition);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        // 根据条件更新送检单明细中QCSTATUS和QCPASSQTY
        /// <summary>
        /// 根据条件更新送检单明细中QCSTATUS和QCPASSQTY
        /// </summary>
        /// <param name="qcStatus">OQC状态</param>
        /// <param name="oqcNoCondition">OQC单号</param>
        public void UpdateOQCDetail(string qcStatus, string oqcNoCondition)
        {
            string sql = string.Format(@"UPDATE TBLOQCDETAIL SET QCSTATUS='{0}',QCPASSQTY = QTY WHERE OQCNO='{1}'", qcStatus, oqcNoCondition);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        #endregion

        #region OQCDetailEC--OQC单明细对应缺陷明细表 add by jinger 2016-03-02
        /// <summary>
        /// TBLOQCDETAILEC--OQC单明细对应缺陷明细表
        /// </summary>
        public OQCDetailEC CreateNewOQCDetailEC()
        {
            return new OQCDetailEC();
        }

        public void AddOQCDetailEC(OQCDetailEC oqcdetailec)
        {
            this._helper.AddDomainObject(oqcdetailec);
        }

        public void DeleteOQCDetailEC(OQCDetailEC oqcdetailec)
        {
            this._helper.DeleteDomainObject(oqcdetailec);
        }

        public void DeleteOQCDetailEC(OQCDetailEC[] oqcdetailecs)
        {
            //this._helper.DeleteDomainObject(oqcdetailecs);
            foreach (OQCDetailEC ec in oqcdetailecs)
            {
                DeleteOQCDetailEC(ec);
            }
        }

        public void UpdateOQCDetailEC(OQCDetailEC oqcdetailec)
        {
            this._helper.UpdateDomainObject(oqcdetailec);
        }

        public object GetOQCDetailEC(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(OQCDetailEC), new object[] { Serial });
        }

        //获取OQC送检单明细对应缺陷明细
        /// <summary>
        /// 获取OQC送检单明细对应缺陷明细
        /// </summary>
        /// <param name="ECode">缺陷代码</param>
        /// <param name="Carinvno">发货箱单号</param>
        /// <param name="Oqcno">OQC送检单号</param>
        /// <param name="Cartonno">箱号条码</param>
        /// <param name="MCode">SAP物料号</param>
        /// <param name="Sn">sn</param>
        /// <returns></returns>
        public object[] GetOQCDetailEC(string ECode, string Carinvno, string Oqcno, string Cartonno, string MCode, string Sn)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLOQCDETAILEC 
WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)));
            if (!string.IsNullOrEmpty(ECode))
            {
                sql += string.Format(" AND ECODE='{0}'", ECode);
            }
            if (!string.IsNullOrEmpty(Carinvno))
            {
                sql += string.Format(" AND CARINVNO='{0}'", Carinvno);
            }
            if (!string.IsNullOrEmpty(Oqcno))
            {
                sql += string.Format(" AND OQCNO='{0}'", Oqcno);
            }
            if (!string.IsNullOrEmpty(Cartonno))
            {
                sql += string.Format(" AND CARTONNO='{0}'", Cartonno);
            }
            if (!string.IsNullOrEmpty(MCode))
            {
                sql += string.Format(" AND MCODE='{0}'", MCode);
            }
            if (!string.IsNullOrEmpty(Sn))
            {
                sql += string.Format(" AND SN='{0}'", Sn);
            }

            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
        }
        //获取OQC送检单明细对应缺陷明细
        /// <summary>
        /// 获取OQC送检单明细对应缺陷明细
        /// </summary>
        /// <param name="ECode">缺陷代码</param>
        /// <param name="Carinvno">发货箱单号</param>
        /// <param name="Oqcno">OQC送检单号</param>
        /// <param name="Cartonno">箱号条码</param>
        /// <param name="MCode">SAP物料号</param>
        /// <param name="Sn">sn</param>
        /// <returns></returns>
        public object[] GetOQCDetailEC(string ECGCode, string ECode, string Carinvno, string Oqcno, string Cartonno, string DQMCode, string Sn)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLOQCDETAILEC 
WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)));
            if (!string.IsNullOrEmpty(ECGCode))
            {
                sql += string.Format(" AND ECGCode='{0}'", ECGCode);
            }
            if (!string.IsNullOrEmpty(ECode))
            {
                sql += string.Format(" AND ECODE='{0}'", ECode);
            }
            if (!string.IsNullOrEmpty(Carinvno))
            {
                sql += string.Format(" AND CARINVNO='{0}'", Carinvno);
            }
            if (!string.IsNullOrEmpty(Oqcno))
            {
                sql += string.Format(" AND OQCNO='{0}'", Oqcno);
            }
            if (!string.IsNullOrEmpty(Cartonno))
            {
                sql += string.Format(" AND CARTONNO='{0}'", Cartonno);
            }
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND DQMCode='{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(Sn))
            {
                sql += string.Format(" AND SN='{0}'", Sn);
            }

            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
        }
        public object[] GetOQCDetailEC1(string ECode, string Carinvno, string Oqcno, string Cartonno, string DQMCode, string Sn)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLOQCDETAILEC 
WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)));
            if (!string.IsNullOrEmpty(ECode))
            {
                sql += string.Format(" AND ECODE=N'{0}'", ECode);
            }
            if (!string.IsNullOrEmpty(Carinvno))
            {
                sql += string.Format(" AND CARINVNO='{0}'", Carinvno);
            }
            if (!string.IsNullOrEmpty(Oqcno))
            {
                sql += string.Format(" AND OQCNO='{0}'", Oqcno);
            }
            if (!string.IsNullOrEmpty(Cartonno))
            {
                sql += string.Format(" AND CARTONNO='{0}'", Cartonno);
            }
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND DQMCODE='{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(Sn))
            {
                sql += string.Format(" AND SN='{0}'", Sn);
            }

            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
        }
        //获取送检单明细
        /// <summary>
        ///获取送检单明细
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <returns></returns>
        public object[] GetOQCDetailECByOqcNo(string oqcNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLOQCDETAILEC WHERE OQCNO='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), oqcNo);
            object[] objOqcDetailEc = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
            if (objOqcDetailEc != null && objOqcDetailEc.Length > 0)
            {
                return objOqcDetailEc;
            }
            return null;
        }

        //获取送检单明细
        /// <summary>
        ///获取送检单明细
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <returns></returns>
        public object[] GetOQCDetailEC(string oqcNo, string cartonNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLOQCDETAILEC WHERE OQCNO='{1}' AND CARTONNO='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), oqcNo, cartonNo);
            object[] objOqcDetailEc = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
            if (objOqcDetailEc != null && objOqcDetailEc.Length > 0)
            {
                return objOqcDetailEc;
            }
            return null;
        }

        //查询OQC送检单明细对应缺陷明细
        /// <summary>
        /// 查询OQC送检单明细对应缺陷明细
        /// </summary>
        /// <param name="oqcNo">OQC送检单号</param>
        /// <param name="carInvNo">发货箱单号</param>
        /// <param name="cartonNo">箱号条码</param>
        /// <param name="dqMCode">鼎桥物料号</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryOQCDetailEC(string oqcNo, string carInvNo, string cartonNo, string dqMCode, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLOQCDETAILEC A WHERE 1=1",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)));

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO = '{0}'", carInvNo);
            }

            //if (!string.IsNullOrEmpty(cartonNo))
            //{
            //    sql += string.Format(" AND A.CARTONNO = '{0}'", cartonNo);
            //}

            //if (!string.IsNullOrEmpty(dqMCode))
            //{
            //    sql += string.Format(" AND A.DQMCODE = '{0}'", dqMCode);
            //}

            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }

        //查询OQC送检单明细对应缺陷明细
        /// <summary>
        /// 查询OQC送检单明细对应缺陷明细
        /// </summary>
        /// <param name="oqcNo">OQC送检单号</param>
        /// <param name="carInvNo">发货箱单号</param>
        /// <param name="cartonNo">箱号条码</param>
        /// <param name="dqMCode">鼎桥物料号</param>
        /// <returns></returns>
        public int QueryOQCDetailECCount(string oqcNo, string carInvNo, string cartonNo, string dqMCode)
        {
            string sql = @" SELECT COUNT(1) 
                                FROM TBLOQCDETAILEC A WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(" AND A.OQCNO = '{0}'", oqcNo);
            }

            if (!string.IsNullOrEmpty(carInvNo))
            {
                sql += string.Format(" AND A.CARINVNO = '{0}'", carInvNo);
            }

            //if (!string.IsNullOrEmpty(cartonNo))
            //{
            //    sql += string.Format(" AND A.CARTONNO = '{0}'", cartonNo);
            //}

            //if (!string.IsNullOrEmpty(dqMCode))
            //{
            //    sql += string.Format(" AND A.DQMCODE = '{0}'", dqMCode);
            //}

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //查询OQC送检单明细对应缺陷明细
        /// <summary>
        /// 查询OQC送检单明细对应缺陷明细
        /// </summary>
        /// <param name="oqcNo">OQC送检单号</param>
        /// <param name="cartonNo">箱号条码</param>
        /// <param name="sn">SN</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryOQCDetailEC(string oqcNo, string cartonNo, string sn, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLOQCDETAILEC A WHERE 1=1",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)));

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(" AND A.OQCNO = '{0}'", oqcNo);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO LIKE '%{0}%'", cartonNo);
            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" AND A.SN = '{0}'", sn);
            }
            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }

        //查询OQC送检单明细对应缺陷明细
        /// <summary>
        /// 查询OQC送检单明细对应缺陷明细
        /// </summary>
        /// <param name="oqcNo">OQC送检单号</param>
        /// <param name="cartonNo">箱号条码</param>
        /// <param name="sn">SN</param>
        /// <returns></returns>
        public int QueryOQCDetailECCount(string oqcNo, string cartonNo, string sn)
        {
            string sql = @" SELECT COUNT(1) 
                                FROM TBLOQCDETAILEC A WHERE 1 = 1 ";

            if (!string.IsNullOrEmpty(oqcNo))
            {
                sql += string.Format(" AND A.OQCNO = '{0}'", oqcNo);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO LIKE '%{0}%'", cartonNo);
            }
            if (!string.IsNullOrEmpty(sn))
            {
                sql += string.Format(" AND A.SN = '{0}'", sn);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //获取送检单明细对应缺陷明细中缺陷品数总数量
        /// <summary>
        ///获取送检单明细对应缺陷明细中缺陷品数总数量
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetSumNgQtyFromOQCDetailEc(string oqcNo, string ngFlag)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLOQCDETAILEC WHERE OQCNO='{0}'", oqcNo);
            if (!string.IsNullOrEmpty(ngFlag))
            {
                sql += string.Format(@" AND NGFLAG = '{0}'", ngFlag);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        //获取送检单明细对应缺陷明细中缺陷品数总数量
        /// <summary>
        ///获取送检单明细对应缺陷明细中缺陷品数总数量
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetSumNgQtyFromOQCDetailEc2(string oqcNo, string ngFlag, string CartonNo)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLOQCDETAILEC WHERE OQCNO='{0}' AND CARTONNO='{1}'", oqcNo, CartonNo);
            if (!string.IsNullOrEmpty(ngFlag))
            {
                sql += string.Format(@" AND NGFLAG = '{0}'", ngFlag);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //获取送检单明细对应缺陷明细中缺陷品数总数量
        /// <summary>
        ///获取送检单明细对应缺陷明细中缺陷品数总数量
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetSumNgQtyFromOQCDetailEc(string oqcNo, string cartonNo, string ngFlag)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLOQCDETAILEC WHERE OQCNO = '{0}' AND CARTONNO='{1}' AND NGFLAG='{2}'", oqcNo, cartonNo, ngFlag);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        /// <summary>
        /// 同一箱号同一鼎桥物料NGFLAG=N记录的SUM(NGQTY)不能大于箱号中同一鼎桥物料送检数量
        /// </summary>
        /// <param name="oqcNo"></param>
        /// <param name="cartonNo"></param>
        /// <param name="ngFlag"></param>
        /// <returns></returns>
        public int GetSumNgQtyFromOQCDetailEc(string oqcNo, string cartonNo, string ngFlag, string DQMCODE)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM TBLOQCDETAILEC WHERE OQCNO = '{0}' AND CARTONNO='{1}' AND NGFLAG='{2}' AND DQMCODE='{3}'", oqcNo, cartonNo, ngFlag, DQMCODE);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        //根据条件获取送检单明细对应缺陷明细总行数
        /// <summary>
        /// 根据条件获取送检单明细对应缺陷明细总行数
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetOQCDetailEcCount(string oqcNo, string cartonNo, string ngFlag)
        {
            string sql = string.Format(@" SELECT COUNT(1) 
                                FROM TBLOQCDETAILEC A WHERE 1 = 1 AND A.OQCNO = '{0}' AND A.CARTONNO='{1}' AND A.NGFLAG='{2}'", oqcNo, cartonNo, ngFlag);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //根据条件获取送检单明细对应缺陷明细总行数
        /// <summary>
        /// 根据条件获取送检单明细对应缺陷明细总行数
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <param name="sn">SN</param>
        /// <param name="ngFlag">缺陷种类</param>
        /// <returns></returns>
        public int GetOQCDetailECCount(string oqcNo, string sn, string ngFlag)
        {
            string sql = string.Format(@" SELECT COUNT(1) 
                                FROM TBLOQCDETAILEC A WHERE 1 = 1 AND A.OQCNO = '{0}' AND A.SN='{1}' AND A.NGFLAG='{2}'", oqcNo, sn, ngFlag);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region OQCDetailSN-- OQC单明细SN信息  add by jinger 2016-03-02
        /// <summary>
        /// TBLOQCDETAILSN-- OQC单明细SN信息 
        /// </summary>
        public OQCDetailSN CreateNewOQCDetailSN()
        {
            return new OQCDetailSN();
        }

        public void AddOQCDetailSN(OQCDetailSN oqcdetailsn)
        {
            this._helper.AddDomainObject(oqcdetailsn);
        }

        public void DeleteOQCDetailSN(OQCDetailSN oqcdetailsn)
        {
            this._helper.DeleteDomainObject(oqcdetailsn);
        }

        public void UpdateOQCDetailSN(OQCDetailSN oqcdetailsn)
        {
            this._helper.UpdateDomainObject(oqcdetailsn);
        }

        public object GetOQCDetailSN(string Carinvno, string Oqcno, string Sn)
        {
            return this.DataProvider.CustomSearch(typeof(OQCDetailSN), new object[] { Carinvno, Oqcno, Sn });
        }

        //根据OQC检验单号获取送检单明细信息
        /// <summary>
        /// 根据OQC检验单号获取送检单明细信息
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <returns></returns>
        public object[] GetOQCDetailSNByOqcNo(string oqcNo)
        {
            return this.DataProvider.CustomQuery(typeof(OQCDetailSN), new SQLCondition(string.Format("SELECT {0} FROM TBLOQCDETAILSN WHERE OQCNO='{1}' ",
                                                                                                            DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailSN)),
                                                                                                            oqcNo))
                                               );
        }

        // 根据条件更新OQC单明细SN信息中QCSTATUS
        /// <summary>
        /// 根据条件更新OQC单明细SN信息中QCSTATUS
        /// </summary>
        /// <param name="ocStatus">OQC状态</param>
        /// <param name="oqcNoCondition">OQC单号（条件）</param>
        /// <param name="ocStatusCondition">OQC状态(条件)</param>
        public void UpdateOQCDetailSN(string qcStatus, string oqcNoCondition, string qcStatusCondition)
        {
            string sql = string.Format(@"UPDATE TBLOQCDETAILSN SET QCSTATUS='{0}' WHERE OQCNO='{1}' AND QCSTATUS <> '{2}'", qcStatus, oqcNoCondition, qcStatusCondition);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        // 根据条件更新OQC单明细SN信息中QCSTATUS
        /// <summary>
        /// 根据条件更新OQC单明细SN信息中QCSTATUS
        /// </summary>
        /// <param name="qcStatus">OQC状态</param>
        /// <param name="oqcNoCondition">OQC单号（条件）</param>
        public void UpdateOQCDetailSN(string qcStatus, string oqcNoCondition)
        {
            string sql = string.Format(@"UPDATE TBLOQCDETAILSN SET QCSTATUS='{0}' WHERE OQCNO='{1}'", qcStatus, oqcNoCondition);

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }



        #endregion

        #region Add By Amy @20160503
        public object[] GetSampleQTYByIqcQTY(int QTY)
        {
            string sql = string.Format("SELECT {0} FROM TBLAQL WHERE {1} BETWEEN LOTSIZEMIN AND LOTSIZEMAX ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)), QTY);
            object[] objs = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));

            return objs;
        }
        public object[] GetSampleQTYByIqcQTY1(int QTY)
        {
            string sql = string.Format("SELECT {0} FROM TBLAQL WHERE {1} BETWEEN LOTSIZEMIN AND LOTSIZEMAX ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(AQL)), QTY);
            object[] objs = this.DataProvider.CustomQuery(typeof(AQL), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs;
            return null;
        }
        public object[] GetAsnIqcDetailSNByIqcNoAndCartonNo(string oqcno, string CartonNo)
        {
            return this.DataProvider.CustomQuery(typeof(OQCDetailSN), new SQLCondition(string.Format(@"SELECT {0} FROM tbloqcdetailsn WHERE OQCNO='{1}' AND CARTONNO='{2}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailSN)), oqcno, CartonNo)));
        }
        public int GetSumNgQtyFromAsnIQCDetailEc1(string oqcNo, string CartonNo, string ngFlag)
        {
            string sql = string.Format("SELECT NVL(SUM(NGQTY),0) NGQTY FROM tbloqcdetailec WHERE OQCNO='{0}'", oqcNo);
            if (!string.IsNullOrEmpty(ngFlag))
            {
                sql += string.Format(@" AND NGFLAG = '{0}'", ngFlag);
            }
            if (!string.IsNullOrEmpty(CartonNo))
            {
                sql += string.Format(@" AND CartonNo = '{0}'", CartonNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        public object GetParaValueByText(string Text)
        {
            string sql = "select * from tblsysparam m where m.paramgroupcode='SQESTATUS' and m.paramdesc='" + Text + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
                return objs[0];
            return null;
        }
        public object[] GetAsnIQCDetailEcByIQCNoAndCartonNo1(string oqcNo, string cartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM tbloqcdetailec A WHERE A.OQCNO = '{1}' AND A.CARTONNO ='{2}'  AND NGFLAG='Y' AND SQESTATUS IS NOT NULL",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), oqcNo, cartonNo);
            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
        }
        public bool GetAsnIQCDetailEc(string oqcNo, string cartonNo, string NGFlag)
        {
            string sql = string.Format(@"SELECT {0} FROM tbloqcdetailec A WHERE A.OQCNO = '{1}' AND A.CARTONNO ='{2}' AND NGFLAG='Y' AND SQESTATUS IS NOT NULL",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), oqcNo, cartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
            if (objs != null)
            {
                return true;
            }
            else
            {
                //没有已经维护的整箱不良
                if (NGFlag == "Y")
                    return true;
                else
                {
                    //是否有需要维护的整箱不良
                    sql = string.Format(@"SELECT {0} FROM tbloqcdetailec A WHERE A.OQCNO = '{1}' AND A.CARTONNO ='{2}' AND NGFLAG='Y'",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), oqcNo, cartonNo);
                    object[] objs1 = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
                    if (objs1 == null)
                        return true;
                    else
                        return false;
                }
            }
        }
        public bool CheckALLNGStatus(string OqcNo, string cartonNo, string NGFlag)
        {
            string sql = string.Format(@"SELECT {0} FROM tbloqcdetailec A WHERE A.OQCNO = '{1}' AND A.CARTONNO ='{2}' AND NGFLAG='Y' AND SQESTATUS IS NOT NULL",
                                     DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), OqcNo, cartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
            if (objs != null)
            {
                OQCDetailEC ec = objs[0] as OQCDetailEC;
                if (ec.SqeStatus == "Return" || ec.SqeStatus == "Reform")
                    return true;
                else
                    return false;
            }
            return false;
        }
        public object[] GetAsnIQCDetailEcByIQCNoAndCartonNo(string OqcNo, string cartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM tbloqcdetailec A WHERE A.OQCNO = '{1}' AND A.CARTONNO ='{2}'",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), OqcNo, cartonNo);
            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
        }
        public object[] GetAsnIQCDetailEcByIqcNo(string oqcNo, string NGFlag)
        {
            string sql = string.Format(@"SELECT {0} FROM tbloqcdetailec A WHERE A.OQCNO = '{1}' AND NGFLAG='{2}'",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), oqcNo, NGFlag);
            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
        }
        public object[] GetAsnIQCDetailEcByIqcNo(string oqcNo, string NGFlag, string cartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM tbloqcdetailec A WHERE A.OQCNO = '{1}' AND NGFLAG='{2}' and cartonno='{3}'",
                                      DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCDetailEC)), oqcNo, NGFlag, cartonNo);
            return this.DataProvider.CustomQuery(typeof(OQCDetailEC), new SQLCondition(sql));
        }
        public DataSet GetOQCandInfoByOQCNo(string oqcno)
        {
            string sql = string.Format("select  c.cdate,t.invno,v.vendorname,t.pickno from tbloqc c left join tblpick t on c.pickno=t.pickno left join tblinvoices b on t.invno=b.invno left join tblvendor v on b.vendorcode=v.vendorcode  where oqcno='{0}'", oqcno);
            return ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
        }
        public DataSet GetOQCandInfoByOQCNoAndPickNo(string oqcno, string pickNo)
        {
            string sql = string.Format("select  c.dqmcode,p.custmcode,t.mchlongdesc,c.qty from TBLOQCDETAIL c left join tblmaterial t on c.dqmcode=t.dqmcode left join tblpickdetail p on p.dqmcode=c.dqmcode  where oqcno='{0}' and p.pickno='{1}'", oqcno, pickNo);
            return ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
        }
        public object[] GetUpLoadFilesByInvDocNo(string InvDocNo, string InvDocType)
        {
            string sql = string.Format("select {0} from TBLINVDOC where InvDocNo='{1}' and InvDocType='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvDoc)), InvDocNo, InvDocType);
            return this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(sql));
        }
        #endregion


        public bool IsOQCFinish(string pickno)
        {
            string sql = @"SELECT B.CLOSECOUNT,A.total FROM (SELECT count(*) total,pickno FROM TBLOQC WHERE PICKNO='" + pickno + @"' and status<>'Cancel' group by pickno) A LEFT JOIN
                             (SELECT COUNT(*) CLOSECOUNT ,PICKNO FROM TBLOQC WHERE STATUS IN('OQCClose','SQEFail') AND PICKNO='" + pickno + @"' GROUP BY PICKNO ) B 
                             ON A.PICKNO=B.PICKNO";


            object[] objs = this.DataProvider.CustomQuery(typeof(OQCCount), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                OQCCount summary = ((OQCCount)objs[0]);
                if (summary.CloseCount == summary.Total)
                    return true;
                else
                    return false;

            }
            return false;

        }
    }

    class OQCCount : DomainObject
    {
        [FieldMap("total", typeof(int), 40, true)]
        public int Total;


        [FieldMap("CLOSECOUNT", typeof(int), 40, true)]
        public int CloseCount;

    }
}

