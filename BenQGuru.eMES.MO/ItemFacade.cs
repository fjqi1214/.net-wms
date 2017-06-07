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
#endregion


/// ItemFacade 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Crystal Chu
/// 创建日期:2005/03/22
/// 修改人:
/// 修改日期:
/// 描 述: 对料品简单的操作控制
/// 版 本:	
/// </summary>

namespace BenQGuru.eMES.MOModel
{
    public class ItemFacade : MarshalByRefObject
    {
        //private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(ItemFacade));
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public ItemFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        public ItemFacade()
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

        #region ItemClass

        public object[] GetItemFirstClass()
        {
            string sql = string.Format("select distinct FIRSTCLASS from TBLITEMCLASS order by FIRSTCLASS");

            return this.DataProvider.CustomQuery(typeof(ItemClass), new SQLCondition(sql));
        }

        public object[] GetItemSecondClass(string firstClass)
        {
            string sql = string.Format("select distinct SECONDCLASS from TBLITEMCLASS where FIRSTCLASS = '" + firstClass + "' order by SECONDCLASS");

            return this.DataProvider.CustomQuery(typeof(ItemClass), new SQLCondition(sql));
        }

        public object[] GetItemThirdClass(string firstClass, string secondClass)
        {
            string sql = string.Format("select distinct THIRDCLASS from TBLITEMCLASS where FIRSTCLASS = '" + firstClass + "' AND SECONDCLASS = '" + secondClass + "' order by THIRDCLASS");

            return this.DataProvider.CustomQuery(typeof(ItemClass), new SQLCondition(sql));
        }

        public object[] QueryItemClass(string materialCode)
        {
            string sql = string.Empty;
            sql += "SELECT {0} ";
            sql += "FROM tblitemclass ";
            sql += "WHERE itemgroup IN ";
            sql += "  ( ";
            sql += "  SELECT mgroup ";
            sql += "  FROM tblmaterial ";
            sql += "  WHERE mcode = '{1}' ";
            sql += "  ) ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemClass)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(materialCode)));

            return this.DataProvider.CustomQuery(typeof(ItemClass), new SQLCondition(sql));
        }

        #endregion

        #region item
        public Item CreateNewItem()
        {
            return new Item();
        }

        public void AddItem(Item item)
        {
            this._helper.AddDomainObject(item);
        }

        //新增产品,新增产品机种关系,作事务
        public void AddItem(Item item, string modelCode)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                this.AddItem(item);
                ModelFacade _modelFacade = new ModelFacade(this.DataProvider);
                _modelFacade.AssignItemsToModelNoTransAction((Model2Item[])this.GetNewModel2Item(item, modelCode).ToArray(typeof(Model2Item)));
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public Item CreateDefaultItem(MO mo)
        {
            Item currentItem = new Item();
            currentItem.ItemCode = mo.ItemCode;
            currentItem.OrganizationID = mo.OrganizationID;
            currentItem.ItemControlType = string.Empty;
            //Laws Lu,2006/11/13 uniform system collect date
            DBDateTime dbDateTime;

            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            currentItem.ItemDate = dbDateTime.DBDate;
            currentItem.ItemDescription = string.Empty;
            currentItem.ItemName = string.Empty;
            currentItem.ItemType = string.Empty;
            currentItem.ItemUOM = string.Empty;
            currentItem.ItemUser = mo.MaintainUser;
            currentItem.ItemVersion = string.Empty;
            //			currentItem.ModelCode = string.Empty;

            currentItem.MaintainUser = mo.MaintainUser;
            currentItem.MaintainDate = dbDateTime.DBDate;
            currentItem.MaintainTime = dbDateTime.DBTime;
            currentItem.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

            return currentItem;
        }


        public void UpdateItem(Item item)
        {
            this._helper.UpdateDomainObject(item);
        }

        //修改产品,修改产品机种关系,作事务
        public void UpdateItem(Item item, string modelCode)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                this.UpdateItem(item);

                //同步物料类型
                Domain.MOModel.Material objMaterial = GetMaterial(item.ItemCode, item.OrganizationID) as Domain.MOModel.Material;
                objMaterial.MaterialType = item.ItemType;
                this.UpdateMaterial(objMaterial);

                ModelFacade _modelFacade = new ModelFacade(this.DataProvider);
                this.DeleteOldModel2ItemByItemCode(item.ItemCode);																			//删除原有关系
                _modelFacade.AssignItemsToModelNoTransAction((Model2Item[])this.GetNewModel2Item(item, modelCode).ToArray(typeof(Model2Item)));		//新建关系
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void DeleteItem(Item item)
        {
            MOFacade mofacade = new MOFacade(this.DataProvider);
            if (mofacade.CheckItemCodeUsed(item.ItemCode))
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$ERROR_ITEM_USE", String.Format("[$ItemCode='{0}']", item.ItemCode));
            }
            this._helper.DeleteDomainObject(item, new ICheck[] { new DeleteAssociateCheck(item, this.DataProvider, new Type[] { typeof(MO), typeof(SBOM), typeof(OPBOM), typeof(Model2Item), typeof(ItemLocation), typeof(Item2OQCCheckList) }) });
        }

        //删除产品: 先删除产品机种关系,再删除产品,作事务 modify by Simone
        public void DeleteItem(Item[] items)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                this.DeleteOldModel2ItemByItemCode(items);			//删除产品机种关系(批操作)
                foreach (Item item in items)
                {
                    DeleteItem(item); // 增加对SPC采集表和测试项的检查 modify by joe song 20050912
                    //this._helper.DeleteDomainObject(item,new ICheck[]{new DeleteAssociateCheck(items,this.DataProvider,new Type[]{typeof(MO),typeof(SBOM),typeof(OPBOM),typeof(Model2Item),typeof(ItemLocation),typeof(Item2OQCCheckList) })});
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public object GetItem(string itemCode, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(Item), new object[] { itemCode, orgID });
        }

        public object[] GetAllItem()
        {
            return QueryItem(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }

        public object[] QueryItem(string itemCode, string itemDesc)
        {
            string sql = string.Format("SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " FROM tblitem WHERE 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");
            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0}   AND itemcode LIKE '%{1}%'", sql, itemCode);
            }

            if (itemDesc != null && itemDesc.Length != 0)
            {
                sql = string.Format("{0}   AND itemdesc LIKE '%{1}%'", sql, itemDesc);
            }
            sql += " order by itemcode ";

            return this.DataProvider.CustomQuery(typeof(Item), new SQLCondition(sql));
        }

        public object[] QueryItem(string itemCode, string itemName, string itemType, string model, string itemStatus)
        {
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE ='{1}'", sql, itemCode);
            }

            if ((itemName != null) && (itemName.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and itemname='{1}'", sql, itemName.Trim());
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }
            sql += " order by itemcode ";

            return this.DataProvider.CustomQuery(typeof(Item), new SQLCondition(sql));
        }

        public object[] QueryItem(string itemCode, string itemName, string itemType, string model, string itemStatus, int inclusive, int exclusive)
        {
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if ((itemName != null) && (itemName.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and itemname='{1}'", sql, itemName.Trim());
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.CustomQuery(typeof(Item), new PagerCondition(sql, "itemcode", inclusive, exclusive));
        }

        public object[] QueryItemIllegibility(string itemCode, string itemName, string itemType, string model, string itemStatus, int inclusive, int exclusive)
        {
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if ((itemName != null) && (itemName.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and upper(itemname) like '{1}%'", sql, itemName.Trim().ToUpper());
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.CustomQuery(typeof(Item), new PagerCondition(sql, "itemcode", inclusive, exclusive));
        }

        public int QueryItemIllegibilityCount(string itemCode, string itemName, string itemType, string model, string itemStatus)
        {
            string sql = string.Format("select count(itemcode) from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if ((itemName != null) && (itemName.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and upper(itemname) like '{1}%'", sql, itemName.Trim().ToUpper());
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryItemCount(string itemCode, string itemName, string itemType, string model, string itemStatus)
        {
            string sql = string.Format("select count(itemcode) from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if ((itemName != null) && (itemName.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and itemname='{1}'", sql, itemName.Trim());
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryHasSBOMItem(string itemCode, string itemType, string model, string itemStatus, int inclusive, int exclusive)
        {
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.CustomQuery(typeof(Item), new PagerCondition(sql, "itemcode", inclusive, exclusive));
        }

        public object[] QueryHasSBOMItem(string itemCode, string itemType, string model, string itemStatus, string itemName, int inclusive, int exclusive)
        {
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item)) + " from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }
            if (itemName != null && itemName.Length != 0)
            {
                sql = string.Format("{0} and ITEMNAME like '{1}%'", sql, itemName);
            }

            object[] returnObjs = this.DataProvider.CustomQuery(typeof(ITEM2BOM), new PagerCondition(sql, "itemcode", inclusive, exclusive));

            //匹配标准bom最后维护时间 和 产品工序bom最后维护时间
            if (returnObjs != null && returnObjs.Length > 0)
            {
                ArrayList ItemCodes = new ArrayList();
                foreach (ITEM2BOM _item in returnObjs)
                {
                    ItemCodes.Add(_item.ItemCode);
                }
                Hashtable SbomHT = this.GetSBOMDateByItemCode(ItemCodes);
                Hashtable OPbomHT = this.GetOPBOMDateByItemCode(ItemCodes);

                foreach (ITEM2BOM _item in returnObjs)
                {
                    string htkey = _item.ItemCode;
                    if (SbomHT.Contains(htkey))
                    {
                        SBOM _sbom = (SBOM)SbomHT[htkey];
                        _item.SBOMMaintainUser = _sbom.MaintainUser;
                        _item.SBOMMaintainDate = _sbom.MaintainDate;
                    }
                    if (OPbomHT.Contains(htkey))
                    {
                        OPBOM _opbom = (OPBOM)OPbomHT[htkey];
                        _item.OPBOMMaintainUser = _opbom.MaintainUser;
                        _item.OPBOMMaintainDate = _opbom.MaintainDate;
                    }
                }
            }


            return returnObjs;
        }

        public int QueryHasSBOMItemCount(string itemCode, string itemType, string model, string itemStatus)
        {
            string sql = string.Format("select count(*) from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryHasSBOMItemCount(string itemCode, string itemType, string model, string itemStatus, string itemName)
        {
            string sql = string.Format("select count(*) from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " ");

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and ITEMCODE like '{1}%'", sql, itemCode);
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode ='{1}'", sql, model);
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }
            if (itemName != null && itemName.Length != 0)
            {
                sql = string.Format("{0} and ITEMNAME like '{1}%'", sql, itemName);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// ** 功能描述:	获取Item对应的ModelCode
        /// ** 作 者:		Simone Xu
        /// ** 日 期:		2005-06/20
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemName"></param>
        /// <param name="itemType"></param>
        /// <param name="model">所属机种</param>
        /// <param name="itemStatus"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] GetItemWithModelCode(string itemCode, string itemName, string itemType, string model, string itemStatus, int inclusive, int exclusive)
        {
            //连接查询产品所属机种 modify by Simone
            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = "SELECT tblitem.ElectricCurrentMinValue,tblitem.ElectricCurrentMaxValue ," +
                                "tblitem.ITEMCONFIG,tblitem.itemuser, tblitem.itemdate, tblitem.itemuom, " +
                                "tblitem.itemburninqty, tblitem.ItemCartonQty,tblitem.muser, tblitem.eattribute1,tblitem.pcbacount, " +
                                "tblitem.mtime, tblitem.itemcontrol,tblitem.mdate, tblitem.itemver, tblitem.itemtype, " +
                                "tblitem.itemname, tblitem.itemcode, tblitem.itemdesc, tblitem.productcode,tblitem.BurnUseMinutes, " +
                                "tblitem.needchkcarton, tblitem.orgid, tblitem.chkitemop,tblitem.lotsize,tblitem.NEEDCHKACCESSORY, modelcode " +
                           "FROM tblitem left JOIN tblmodel2item ON (tblitem.itemcode =tblmodel2item.itemcode AND tblitem.orgid=tblmodel2item.orgid) WHERE 1=1 " + orgIDList;

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and TBLITEM.ITEMCODE like '{1}%'", sql, itemCode);
            }

            if ((itemName != null) && (itemName.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and upper(itemname) like '{1}%'", sql, itemName.Trim().ToUpper());
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode LIKE '{1}%'", sql, model.Trim().ToUpper());
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.CustomQuery(typeof(ItemOfModel), new PagerCondition(sql, "tblitem.itemcode", inclusive, exclusive));
        }

        public int GetItemWithModelCodeCount(string itemCode, string itemName, string itemType, string model, string itemStatus)
        {
            //连接查询产品所属机种 modify by Simone
            string orgIDList = GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName();
            if (orgIDList.Length > 0) orgIDList = " AND tblitem.orgid IN(" + orgIDList + ") ";

            string sql = "SELECT  Count(*) FROM tblitem left JOIN tblmodel2item ON (tblitem.itemcode =tblmodel2item.itemcode AND tblitem.orgid=tblmodel2item.orgid) WHERE 1=1 " + orgIDList;

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} and TBLITEM.ITEMCODE like '{1}%'", sql, itemCode);
            }

            if ((itemName != null) && (itemName.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and upper(itemname) like '{1}%'", sql, itemName.Trim().ToUpper());
            }

            if (itemType != null && itemType.Length != 0)
            {
                sql = string.Format("{0} and ITEMTYPE ='{1}'", sql, itemType);
            }

            if (model != null && model.Length != 0)
            {
                sql = string.Format("{0} and modelcode LIKE '{1}%'", sql, model.Trim().ToUpper());
            }

            if (itemStatus != null && itemStatus.Length != 0)
            {
                sql = string.Format("{0} and ITEMSTATUS ='{1}'", sql, itemStatus);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region Model2Item
        //创建新的Model2Item add by Simone
        private ArrayList GetNewModel2Item(Item item, string modelCode)
        {
            ArrayList items = new ArrayList();
            ModelFacade _modelFacade = new ModelFacade(this.DataProvider);
            Model2Item model2Item = _modelFacade.CreateModel2Item();
            model2Item.ModelCode = modelCode.ToUpper();
            model2Item.ItemCode = item.ItemCode.ToUpper();
            model2Item.MaintainUser = item.MaintainUser;
            model2Item.OrganizationID = item.OrganizationID;
            items.Add(model2Item);
            return items;
        }

        //根据产品删除 产品和机种的关系
        private void DeleteOldModel2ItemByItemCode(string itemCode)
        {
            ModelFacade _modelFacade = new ModelFacade(this.DataProvider);
            object[] oldItems = _modelFacade.GetModel2ItemByItemCode(itemCode);
            if (oldItems != null)
            {
                Model2Item[] model2items = new Model2Item[1];
                model2items[0] = (Model2Item)oldItems[0];
                _modelFacade.RemoveItemsFromModelNoTransAction(model2items);
            }
        }
        //根据产品删除 产品和机种的关系
        private void DeleteOldModel2ItemByItemCode(Item[] items)
        {
            ModelFacade _modelFacade = new ModelFacade(this.DataProvider);
            foreach (Item item in items)
            {
                object[] oldItems = _modelFacade.GetModel2ItemByItemCode(item.ItemCode);
                if (oldItems != null)
                {
                    Model2Item[] model2items = new Model2Item[1];
                    model2items[0] = (Model2Item)oldItems[0];
                    _modelFacade.RemoveItemsFromModelNoTransAction(model2items);
                }
            }
        }
        #endregion

        #region 产品生产BOM

        //根据产品代码获取标准bom最后更新时间
        public Hashtable GetSBOMDateByItemCode(ArrayList itemCodeList)
        {
            Hashtable returnHT = new Hashtable();
            string condition = string.Empty;
            if (itemCodeList != null && itemCodeList.Count > 0)
            {
                string itemCodes = "('" + String.Join("','", (string[])itemCodeList.ToArray(typeof(string))) + "')";
                condition = string.Format(" and  itemcode in {0}", itemCodes);
            }
            else { return returnHT; }

            string sql = string.Format(@"  SELECT a.*, b.muser
								FROM (SELECT   itemcode, orgid, MAX (mdate) AS mdate,
												MAX (mdate * 100000 + mtime) AS maxdate
											FROM tblsbom a
										WHERE 1=1 {1} {0}
										GROUP BY itemcode, orgid) a,
									(SELECT   itemcode, orgid, muser, MAX (mdate) AS mdate,
												MAX (mdate * 100000 + mtime) AS maxdate
											FROM tblsbom a
										WHERE 1=1 {1} {0}
										GROUP BY itemcode, orgid, muser) b
								WHERE a.itemcode = b.itemcode AND a.orgid = b.orgid AND a.mdate = b.mdate AND a.maxdate = b.maxdate", condition, GlobalVariables.CurrentOrganizations.GetSQLCondition());

            object[] returnObjs = this.DataProvider.CustomQuery(
                typeof(SBOM), new SQLCondition(sql));

            if (returnObjs != null)
            {
                foreach (SBOM _sbom in returnObjs)
                {
                    if (!returnHT.Contains(_sbom.ItemCode))
                        returnHT.Add(_sbom.ItemCode, _sbom);
                }
            }

            return returnHT;
        }

        //根据产品代码获取产品工序bom最后更新时间 tblopbomdetail
        public Hashtable GetOPBOMDateByItemCode(ArrayList itemCodeList)
        {
            Hashtable returnHT = new Hashtable();
            string condition = string.Empty;
            if (itemCodeList != null && itemCodeList.Count > 0)
            {
                string itemCodes = "('" + String.Join("','", (string[])itemCodeList.ToArray(typeof(string))) + "')";
                condition = string.Format(" and  itemcode in {0}", itemCodes);
                condition += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            }
            else { return returnHT; }

            string sql = string.Format(@" SELECT a.*, b.muser
								FROM (SELECT   itemcode, MAX (mdate) AS mdate,
												MAX (mdate * 100000 + mtime) AS maxdate
											FROM tblopbomdetail a
										WHERE 1=1 {0}
										GROUP BY itemcode) a,
									(SELECT   itemcode, muser, MAX (mdate) AS mdate,
												MAX (mdate * 100000 + mtime) AS maxdate
											FROM tblopbomdetail a
										WHERE 1=1 {0}
										GROUP BY itemcode, muser) b
								WHERE a.itemcode = b.itemcode AND a.mdate = b.mdate AND a.maxdate = b.maxdate", condition);

            object[] returnObjs = this.DataProvider.CustomQuery(
                typeof(OPBOM), new SQLCondition(sql));

            if (returnObjs != null)
            {
                foreach (OPBOM _opbom in returnObjs)
                {
                    if (!returnHT.Contains(_opbom.ItemCode))
                        returnHT.Add(_opbom.ItemCode, _opbom);
                }
            }
            return returnHT;
        }

        #endregion

        #region ItemRoute
        private string CreateItemRouteOPID(string modelRouteOPID, string itemCode)
        {
            return modelRouteOPID + itemCode;
        }

        #region	判断工序属性

        //检查是否有上料工序
        public bool IsComponentLoadingOperation(string opControl)
        {
            return FormatHelper.StringToBoolean(opControl, (int)OperationList.ComponentLoading);
        }

        //检查是否有中间投入工序
        public bool IsMidistInputOperation(string opControl)
        {
            return FormatHelper.StringToBoolean(opControl, (int)OperationList.MidistInput);
        }

        //检查是否有中间产量工序
        public bool IsMidistOutputOperation(string opControl)
        {
            return FormatHelper.StringToBoolean(opControl, (int)OperationList.MidistOutput);
        }

        //判断是否有下料工序
        public bool IsDownOperation(string opControl)
        {
            return FormatHelper.StringToBoolean(opControl, (int)OperationList.ComponentDown);
        }

        //判断是否有测试工序
        public bool IsTestingOperation(string opControl)
        {
            return FormatHelper.StringToBoolean(opControl, (int)OperationList.Testing);
        }
        #endregion

        #region private method
        #endregion
        public const string IsReference_Used = "1";
        public const string IsReference_NotUsed = "0";

        public object GetItemRoute2Op(string opID, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(ItemRoute2OP), new object[] { opID, orgID });
        }

        public object[] GetNotCurrentRouteItemRoute2Op(string opID, int orgID)
        {
            object orignalItemRoute2OP = GetItemRoute2Op(opID, orgID);
            if (orignalItemRoute2OP == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_System_Error");
            }

            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) +
                        " FROM TBLITEMROUTE2OP WHERE 1=1 AND ROUTECODE <> '" + ((ItemRoute2OP)orignalItemRoute2OP).RouteCode + "' AND orgid=" + ((ItemRoute2OP)orignalItemRoute2OP).OrganizationID;

            return this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLCondition(sql));
        }

        /// <summary>
        /// UpdateItemRoute2Op
        ///  检查对应的OPBOM的Operation是否已经上料,
        ///  如果已经上料则不能不该为不上料的工序，
        ///  检查Operation 序号，与同一route下的序号不能重复
        /// </summary>
        /// <param name="itemRoute2Op"></param>
        public void UpdateItemRoute2Op(ItemRoute2OP itemRoute2Op)
        {
            object orignalItemRoute2OP = GetItemRoute2Op(itemRoute2Op.OPID, itemRoute2Op.OrganizationID);
            if (orignalItemRoute2OP == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_System_Error");
            }

            #region 工序限制检查
            //1、   第一道工序不能和中间投入工序在同一道工序
            //2、   最后一道不能和中间产量工序在同一道工序

            this.CheckRestrictCondition(itemRoute2Op);

            #endregion

            #region 检查对应的工单状态是否生产中

            // Modified By hi1/Venus.Feng on 20080625 for Hisense Version : Add Org ID
            if (JudgeItemRouteRef(((ItemRoute2OP)orignalItemRoute2OP).RouteCode, ((ItemRoute2OP)orignalItemRoute2OP).ItemCode, ((ItemRoute2OP)orignalItemRoute2OP).OrganizationID))
            {
                // End Modified By hi1/Venus.Feng on 20080625 for Hisense Version : Add Org ID
                ArrayList OpenMOCodes = this.GetOpenMO(((ItemRoute2OP)orignalItemRoute2OP).RouteCode, ((ItemRoute2OP)orignalItemRoute2OP).ItemCode, ((ItemRoute2OP)orignalItemRoute2OP).OrganizationID);
                if (OpenMOCodes.Count != 0)
                {
                    string message = OpenMOCodes[0].ToString();
                    for (int i = 1; i < OpenMOCodes.Count; i++)
                    {
                        message += ", " + OpenMOCodes[i].ToString();
                    }

                    ExceptionManager.Raise(this.GetType().BaseType, "$Error_OpenMO_Error", String.Format("[$MOCodes={0}]", message));
                }
            }

            #endregion

            //如果原来是上料工序，则检查OPBOM有无上料记录,如果有则不允许被修改成非上料工序
            //如果没有无上料记录则无需检查
            object[] objs = null;
            OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
            if (IsComponentLoadingOperation(((ItemRoute2OP)orignalItemRoute2OP).OPControl))
            {
                if (!IsComponentLoadingOperation(((ItemRoute2OP)itemRoute2Op).OPControl))
                {
                    objs = opBOMFacade.QueryOPBOMDetail(((ItemRoute2OP)orignalItemRoute2OP).ItemCode, string.Empty, string.Empty, string.Empty, ((ItemRoute2OP)orignalItemRoute2OP).ItemCode, ((ItemRoute2OP)orignalItemRoute2OP).OPCode, int.MinValue, int.MaxValue, ((ItemRoute2OP)itemRoute2Op).OrganizationID);
                    if (objs != null)
                    {
                        ExceptionManager.Raise(this.GetType().BaseType, "$Error_OPBOM_HasComponentLoadingRecord");
                    }
                }
            }

            //改变工序
            if (((ItemRoute2OP)orignalItemRoute2OP).OPSequence != itemRoute2Op.OPSequence)
            {

                string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where 1=1 {0} {1} {2} {3} and orgid=" + itemRoute2Op.OrganizationID;
                object[] objsParamters = new object[4];
                objsParamters[0] = " and itemcode = '" + itemRoute2Op.ItemCode + "'";
                objsParamters[1] = " and routecode = '" + itemRoute2Op.RouteCode + "'";
                objsParamters[2] = " and opcode <> '" + itemRoute2Op.OPCode + "'";
                objsParamters[3] = " and opseq = " + itemRoute2Op.OPSequence;
                objs = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLCondition(String.Format(selectSql, objsParamters)));
                if (objs != null)
                {
                    ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_SameSequenceOperationExisted");
                }
            }

            this._helper.UpdateDomainObject(itemRoute2Op);
        }

        #region 检查工序中的互斥条件
        //1、   第一道工序不能和中间投入工序在同一道工序
        //2、   最后一道不能和中间产量工序在同一道工序
        //3.    下料作为最后一道工序，该工序一定也是测试工序。
        //4.	下料工序一定是测试工序
        private bool CheckRestrictCondition(ItemRoute2OP itemRoute2Op)
        {
            bool isJudge = true;
            if (!IsMidistInputOperation(itemRoute2Op.OPControl) && !IsMidistOutputOperation(itemRoute2Op.OPControl))
            {
                isJudge = false;
            }
            else if (!IsDownOperation(itemRoute2Op.OPControl) && !IsTestingOperation(itemRoute2Op.OPControl))
            {
                isJudge = false;
            }
            if (isJudge) { return true; }
            string opLocationSeq = this.JudgeOPSequence(itemRoute2Op);
            if (opLocationSeq == Route2OPLocation.OPLocation_FIRST)
            {
                //是第一道工序
                //包含中间投入工序
                //1、   第一道工序不能和中间投入工序在同一道工序
                if (IsMidistInputOperation(itemRoute2Op.OPControl))
                {
                    //满足异常条件,报出异常 第一道工序不能和中间投入工序在同一道工序
                    ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_FirstOPMidistInputError");
                }
                return true;
            }
            else if (opLocationSeq == Route2OPLocation.OPLocation_LAST)
            {
                //是最后一道工序
                //包含中间产量工序
                //2、   最后一道不能和中间产量工序在同一道工序
                if (IsMidistOutputOperation(itemRoute2Op.OPControl))
                {
                    //满足异常条件,报出异常 最后一道不能和中间产量工序在同一道工序
                    ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_LastOPMidistOutputError");
                }

                //下料作为最后一道工序，该工序一定也是测试工序。
                if (IsDownOperation(itemRoute2Op.OPControl))
                {
                    if (!IsTestingOperation(itemRoute2Op.OPControl))
                    {
                        //如果最后一道工序是下料工序，没有选择测试工序，报异常
                        ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_LastOPDownMaterialError");
                    }

                }
                return true;
            }
            else
            {
                if (IsDownOperation(itemRoute2Op.OPControl))
                {
                    if (!IsTestingOperation(itemRoute2Op.OPControl))
                    {
                        //如果最后工序是下料工序，没有选择测试工序，报异常
                        ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_OPDownMaterialError");
                    }

                }
            }
            return false;
        }

        //判断是第一道工序,中间的工序还是最后工序
        private string JudgeOPSequence(ItemRoute2OP itemRoute2Op)
        {
            //获取工序对应途程中所有工序
            //判断当前工序的位置
            //返回当前工序的位置
            string sql = string.Format(" select max(opseq) as MAXSEQ ,min(opseq) as MINSEQ from tblitemroute2op  where itemcode='{0}' and routecode='{1}' and orgid={2} ", itemRoute2Op.ItemCode, itemRoute2Op.RouteCode, itemRoute2Op.OrganizationID);
            object[] opseqs = this.DataProvider.CustomQuery(typeof(OPSeqsence), new SQLCondition(sql));
            if (opseqs != null && opseqs.Length > 0)
            {
                OPSeqsence cOPSeq = (OPSeqsence)opseqs[0];
                if (itemRoute2Op.OPSequence == cOPSeq.MaxSeq)
                {
                    return Route2OPLocation.OPLocation_LAST;
                }
                else if (itemRoute2Op.OPSequence == cOPSeq.MinSeq)
                {
                    return Route2OPLocation.OPLocation_FIRST;
                }
            }
            return Route2OPLocation.OPLocation_MIDDLE;
        }

        #endregion

        #region 检查途程工序是否可以修改(如果途程对应的工单状态为生产中,则不允许修改)

        // Modified By hi1/Venus.Feng on 20080625 for Hisense Version : Add Org ID
        private bool JudgeItemRouteRef(string routeCode, string itemCode, int orgId)
        {
            bool returnBool = false;
            string sql = string.Format("select {0} from TBLITEM2ROUTE  where ROUTECODE = '{1}' AND ITEMCODE = '{2}' AND ORGID = {3}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Route)), routeCode, itemCode, orgId);
            object[] item2Routes = this.DataProvider.CustomQuery(typeof(Item2Route), new SQLCondition(sql));
            if (item2Routes != null && item2Routes.Length > 0)
            {
                Item2Route itemRoute = (Item2Route)item2Routes[0];
                if (itemRoute.IsReference.Trim() == "1")
                { returnBool = true; }
            }
            return returnBool;
        }
        // End Modified By hi1/Venus.Feng on 20080625 for Hisense Version : Add Org ID

        private ArrayList GetOpenMO(string routeCode, string itemCode, int orgID)
        {
            string mosql = string.Format(@" Select {0} from tblmo 
											where
											exists (select mocode from TBLMO2Route where ROUTECODE='{1}' AND tblmo.mocode=TBLMO2Route.mocode)
											and mostatus ='{2}' AND ITEMCODE='{3}' AND orgid=" + orgID, DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), routeCode, MOManufactureStatus.MOSTATUS_OPEN, itemCode);

            object[] openMOs = this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(mosql));

            ArrayList returnMOCodes = new ArrayList();
            if (openMOs != null && openMOs.Length != 0)
            {
                foreach (object mo in openMOs)
                {
                    returnMOCodes.Add((mo as MO).MOCode);
                }
            }
            return returnMOCodes;
        }

        #endregion

        // Remarked by Hi1/venus.feng on 20080625 for Hisense Version : Add Org Id
        // This function was not used again, so need not to modify it.
        // End Remarked

        public void BuildItemRoute(string itemCode)
        {
            //这个是在建立在有ModelItem维护的基础上，也就是说ModelTemplate
            ModelFacade modelFacade = new ModelFacade(this.DataProvider);
            object[] objs = modelFacade.GetModel2RoutesByItemCode(itemCode);
            if (objs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_ModelNoMaintainRoute", String.Format("[$ItemCode='{0}']", itemCode), null);
            }
            //对每一个细项目进行检查如果模板中有item没有则必须copy过去，如果有，不必须进行更新
            object[] objsItemRoutes = null;
            object[] objsItemOperations = null;
            Item2Route item2Route = null;
            ItemRoute2OP itemroute2OP = null;
            this.DataProvider.BeginTransaction();
            try
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    objsItemRoutes = QueryItem2Route(itemCode, ((Model2Route)objs[i]).RouteCode, string.Empty);
                    if (objsItemRoutes == null)
                    {
                        //item route
                        item2Route = CreateItem2Route();
                        item2Route.ItemCode = itemCode;
                        item2Route.RouteCode = ((Model2Route)objs[i]).RouteCode;
                        item2Route.MaintainUser = ((Model2Route)objs[i]).MaintainUser;
                        item2Route.OrganizationID = ((Model2Route)objs[i]).OrganizationID;
                        this._helper.AddDomainObject(item2Route);
                        //item route operation

                        objsItemOperations = modelFacade.GetModel2Operations((Model2Route)objs[i]);
                        if (objsItemOperations != null)
                        {
                            for (int j = 0; j < objsItemOperations.Length; j++)
                            {
                                itemroute2OP = CreateItemRoute2OP();
                                itemroute2OP.IDMergeRule = ((Model2OP)objsItemOperations[j]).IDMergeRule;
                                itemroute2OP.IDMergeType = ((Model2OP)objsItemOperations[j]).IDMergeType;
                                itemroute2OP.ItemCode = itemCode;
                                itemroute2OP.MaintainUser = ((Model2OP)objsItemOperations[j]).MaintainUser;
                                itemroute2OP.OPCode = ((Model2OP)objsItemOperations[j]).OPCode;
                                itemroute2OP.OPControl = ((Model2OP)objsItemOperations[j]).OPControl;
                                itemroute2OP.OPID = CreateItemRouteOPID(((Model2OP)objsItemOperations[j]).OPID, itemCode);
                                itemroute2OP.OPSequence = ((Model2OP)objsItemOperations[j]).OPSequence;
                                itemroute2OP.RouteCode = ((Model2OP)objsItemOperations[j]).RouteCode;
                                itemroute2OP.OrganizationID = ((Model2OP)objsItemOperations[j]).OrganizationID;
                                this._helper.AddDomainObject(itemroute2OP);
                            }
                        }
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_BuildItemRoute", String.Format("[$ItemCode='{0}']", itemCode), ex);
            }
        }

        public Item2Route CreateItem2Route()
        {
            return new Item2Route();
        }

        public ItemRoute2OP CreateItemRoute2OP()
        {
            return new ItemRoute2OP();
        }

        public object[] QueryItem2Route(string itemCode, string routeCode, string orgIDList)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Route)) + " from tblitem2route where 1=1 {0} order by ROUTECODE ";
            string tmpString = string.Empty;
            if ((itemCode != null) && (itemCode.Trim() != string.Empty))
            {
                tmpString += " and itemcode ='" + itemCode.Trim() + "'";
            }
            if ((routeCode != null) && (routeCode.Trim() != string.Empty))
            {
                tmpString += " and routecode='" + routeCode + "'";
            }
            // Added By Hi1/Venus.Feng on 20080625 for Hisense Version : Add ORG ID
            if (orgIDList != null && orgIDList.Trim().Length > 0)
            {
                tmpString += " and orgid in (" + orgIDList + ")";
            }
            // End

            return this.DataProvider.CustomQuery(typeof(Item2Route), new SQLCondition(String.Format(selectSql, tmpString)));
        }

        public object[] QueryItem2Operation(string itemCode, string routeCode)
        {
            if ((itemCode == string.Empty) || (routeCode == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            return this.DataProvider.CustomQuery(typeof(ItemRoute2OP),
                new SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op  where itemcode='{0}' and routecode='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by opseq", itemCode, routeCode)));
        }

        public object[] QueryItem2OperationByRouteCode(string routeCode)
        {
            return this.DataProvider.CustomQuery(typeof(ItemRoute2OP),
                new SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op  where routecode='{0}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by opseq", routeCode)));
        }

        public object[] GetComponenetLoadingOperations(string itemCode, string routeCode)
        {
            if ((itemCode == string.Empty) || (routeCode == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            //上料工序需要维护bom
            string sql = String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where itemcode='{0}' and routecode='{1}' and (substr(opcontrol," + ((int)BaseSetting.OperationList.ComponentLoading + 1) + ",1)='1' ) " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by opseq ", itemCode, routeCode);
            return this.DataProvider.CustomQuery(typeof(ItemRoute2OP),
                new SQLCondition(sql));
        }


        public object[] GetComponenetDownOperations(string itemCode, string routeCode)
        {
            if ((itemCode == string.Empty) || (routeCode == string.Empty))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            //下料工序需要维护bom
            string sql = String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where itemcode='{0}' and routecode='{1}' and ( substr(opcontrol," + ((int)BaseSetting.OperationList.ComponentDown + 1) + ",1)='1') " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by opseq ", itemCode, routeCode);
            return this.DataProvider.CustomQuery(typeof(ItemRoute2OP),
                new SQLCondition(sql));
        }


        public object[] GetSelectedRoutesByItemCode(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(Route), new SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route)) + " from tblroute where routecode in(select routecode from tblitem2route where itemcode ='{0}'" + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", itemCode)));
        }

        public void AddItemRoutes(string itemCode, Item2Route[] itemRoutes)
        {
            if (itemRoutes == null)
            {
                return;
            }
            object[] objsItemOperations = null;
            ItemRoute2OP itemroute2OP = null;
            BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
            this.DataProvider.BeginTransaction();
            try
            {
                for (int j = 0; j < itemRoutes.Length; j++)
                {
                    object curitem2Route = this.GetItem2Route(itemRoutes[j].ItemCode, itemRoutes[j].RouteCode, itemRoutes[j].OrganizationID.ToString());
                    if (curitem2Route == null)
                    {
                        this.DataProvider.Insert(itemRoutes[j]);
                        objsItemOperations = baseModelFacade.GetOperationByRouteCode(itemRoutes[j].RouteCode);
                        if (objsItemOperations != null)
                        {
                            for (int k = 0; k < objsItemOperations.Length; k++)
                            {
                                itemroute2OP = CreateItemRoute2OP();
                                itemroute2OP.IDMergeRule = 1;
                                itemroute2OP.OPID = CreateItemRouteOPID(((Item2Route)itemRoutes[j]).RouteCode + ((Operation)objsItemOperations[k]).OPCode, itemCode);
                                Route2Operation route2Operation = (Route2Operation)baseModelFacade.GetRoute2Operation(((Item2Route)itemRoutes[j]).RouteCode, ((Operation)objsItemOperations[k]).OPCode);
                                itemroute2OP.IDMergeType = IDMergeType.IDMERGETYPE_IDMERGE;
                                itemroute2OP.ItemCode = itemCode;
                                itemroute2OP.MaintainUser = route2Operation.MaintainUser;
                                itemroute2OP.OPCode = route2Operation.OPCode;
                                itemroute2OP.OPControl = route2Operation.OPControl;
                                itemroute2OP.OPSequence = route2Operation.OPSequence;
                                itemroute2OP.RouteCode = route2Operation.RouteCode;

                                // Added By Hi1/Venus.Feng on 20080625 for Hisense Version : Add ORG ID
                                itemroute2OP.OrganizationID = itemRoutes[j].OrganizationID;
                                // End Added

                                this.DataProvider.Insert(itemroute2OP);
                            }
                        }
                    }
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_AddItemRoutes", ex);
            }
        }



        public object GetItem2Route(string itemCode, string routeCode, string orgID)
        {
            return this.DataProvider.CustomSearch(typeof(Item2Route), new object[] { itemCode, routeCode, orgID });
        }
        public void AddItem2Route(Item2Route item2Route)
        {
            object curitem2Route = this.GetItem2Route(item2Route.ItemCode, item2Route.RouteCode, item2Route.OrganizationID.ToString());
            if (curitem2Route == null)
            {
                this._helper.AddDomainObject(item2Route);
            }
        }

        public bool IsItemRouteComponentLoading(Item2Route item2Route)
        {
            OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
            int iCount = this.DataProvider.GetCount(new SQLCondition(
                String.Format("select count(*) from tblopbomdetail where  obcode in (select obcode from tblopbom where itemcode='"
                + item2Route.ItemCode + "'  and obroute='" + item2Route.RouteCode + "' and orgid=" + item2Route.OrganizationID + ")" + " and itemcode='" + item2Route.ItemCode + "' and orgid=" + item2Route.OrganizationID)));
            if (iCount > 0)
            {
                return true;
            }
            return false;
        }

        public void DeleteItem2Route(Item2Route item2Route)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                this.DataProvider.CustomExecute(new SQLCondition("delete from tblitemroute2op where  itemcode='" + item2Route.ItemCode + "'  and routecode='" + item2Route.RouteCode + "' and orgid=" + item2Route.OrganizationID));
                this.DataProvider.CustomExecute(new SQLCondition("delete from tblitem2route where  itemcode='" + item2Route.ItemCode + "'  and routecode='" + item2Route.RouteCode + "' and orgid=" + item2Route.OrganizationID));
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_DeleteItem2RouteNOTransaction", ex);
            }
        }

        public void DeleteItem2RouteWithOPBOM(Item2Route item2Route)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
                this.DataProvider.CustomExecute(new SQLCondition("delete from tblopbomdetail  where obcode in (select obcode from tblopbom where itemcode='" + item2Route.ItemCode + "'  and obroute='" + item2Route.RouteCode + "' and orgid=" + item2Route.OrganizationID + ")" + " and itemcode='" + item2Route.ItemCode + "' and orgid=" + item2Route.OrganizationID));
                this.DataProvider.CustomExecute(new SQLCondition("delete from tblopbom where    itemcode='" + item2Route.ItemCode + "'  and obroute='" + item2Route.RouteCode + "' and orgid=" + item2Route.OrganizationID));
                this.DataProvider.CustomExecute(new SQLCondition("delete from tblitemroute2op where  itemcode='" + item2Route.ItemCode + "'  and routecode='" + item2Route.RouteCode + "' and orgid=" + item2Route.OrganizationID));
                this.DataProvider.CustomExecute(new SQLCondition("delete from tblitem2route where  itemcode='" + item2Route.ItemCode + "'  and routecode='" + item2Route.RouteCode + "' and orgid=" + item2Route.OrganizationID));
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_DeleteItem2RouteWithTrasaction", ex);
            }
        }


        public void AddItemRoutes(string modelCode, string itemCode, Item2Route[] itemRoutes)
        {
            //delete all
            if (itemRoutes == null)
            {
                return;
            }
            object[] objsItemOperations = null;
            ItemRoute2OP itemroute2OP = null;
            ModelFacade modelFacade = new ModelFacade(this.DataProvider);
            this.DataProvider.BeginTransaction();
            try
            {
                // Modified By HI1/Venus.Feng on 20080625 for Hisense Version : Add ORGID
                //DeleteItemRoutes(itemCode);
                // End

                for (int j = 0; j < itemRoutes.Length; j++)
                {
                    DeleteItemRoutes(itemCode, itemRoutes[j].OrganizationID);

                    this.DataProvider.Insert(itemRoutes[j]);

                    objsItemOperations = modelFacade.GetModel2Operations(modelCode, itemRoutes[j].RouteCode, itemRoutes[j].OrganizationID);
                    if (objsItemOperations != null)
                    {
                        for (int k = 0; k < objsItemOperations.Length; k++)
                        {
                            itemroute2OP = CreateItemRoute2OP();
                            itemroute2OP.IDMergeRule = ((Model2OP)objsItemOperations[k]).IDMergeRule;
                            itemroute2OP.IDMergeType = ((Model2OP)objsItemOperations[k]).IDMergeType;
                            itemroute2OP.ItemCode = itemCode;
                            itemroute2OP.MaintainUser = ((Model2OP)objsItemOperations[k]).MaintainUser;
                            itemroute2OP.OPCode = ((Model2OP)objsItemOperations[k]).OPCode;
                            itemroute2OP.OPControl = ((Model2OP)objsItemOperations[k]).OPControl;
                            itemroute2OP.OPID = CreateItemRouteOPID(((Model2OP)objsItemOperations[k]).OPID, itemCode);
                            itemroute2OP.OPSequence = ((Model2OP)objsItemOperations[k]).OPSequence;
                            itemroute2OP.RouteCode = ((Model2OP)objsItemOperations[k]).RouteCode;

                            itemroute2OP.OrganizationID = itemRoutes[j].OrganizationID;

                            this.DataProvider.Insert(itemroute2OP);
                        }
                    }
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_AddItemRoutes", ex);

            }

        }


        private void DeleteItemRoutes(string itemCode, int orgID)
        {
            object[] itemRoute2OPs = null;
            object[] curItemRotues = QueryItem2Route(itemCode, string.Empty, orgID.ToString());
            if (curItemRotues != null)
            {
                foreach (object itemRoute in curItemRotues)
                {
                    itemRoute2OPs = QueryItem2Operation(itemCode, ((Item2Route)itemRoute).RouteCode);
                    if (itemRoute2OPs != null)
                    {
                        foreach (object itemRoute2OP in itemRoute2OPs)
                        {
                            this.DataProvider.Delete(itemRoute2OP);
                        }
                    }
                    this.DataProvider.Delete(itemRoute);
                }
            }
        }

        public object GetItemRoute2OP(string itemCode, string routeCode, string opCode)
        {
            string sql = string.Empty;
            sql += "SELECT {0} ";
            sql += "FROM tblitemroute2op ";
            sql += "WHERE 1 = 1 ";
            sql += "AND itemcode = '{1}' ";
            sql += "AND routecode = '{2}' ";
            sql += "AND opcode = '{3}' ";
            sql += "ORDER BY mdate DESC, mtime DESC ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)), itemCode, routeCode, opCode);

            object[] list = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLCondition(sql));

            if (list == null || list.Length <= 0)
            {
                return null;
            }
            else
            {
                return list[0];
            }
        }

        #endregion

        #region Item2Config
        /// <summary>
        /// 
        /// </summary>
        public Item2Config CreateNewItem2Config()
        {
            return new Item2Config();
        }

        public void AddItem2Config(Item2Config item2Config)
        {
            this._helper.AddDomainObject(item2Config);
        }

        public void UpdateItem2Config(Item2Config item2Config)
        {
            this._helper.UpdateDomainObject(item2Config);
        }

        public void DeleteItem2Config(Item2Config item2Config)
        {
            this._helper.DeleteDomainObject(item2Config);
        }

        public void DeleteItem2Config(Item2Config[] item2Config)
        {
            this._helper.DeleteDomainObject(item2Config);
        }

        public object GetItem2Config(string itemCode, string itemConfigration, string parentCode, string configCode, string configValue, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(Item2Config), new object[] { itemCode, itemConfigration, parentCode, configCode, configValue, orgID });
        }

        public object[] GetItem2Config(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(Item2Config)
                , new SQLCondition(String.Format("select {0} from tblitem2config where 1=1 and ORGID = " + GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() + " and itemcode='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Config)), itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	查询Item2Config的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-1-20 14:24:21
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="itemConfigration">ItemConfigration，模糊查询</param>
        /// <param name="configCode">ConfigCode，模糊查询</param>
        /// <param name="configValue">ConfigValue，模糊查询</param>
        /// <returns> Item2Config的总记录数</returns>
        public int QueryItem2ConfigCount(string itemCode, string itemConfigration, string parentCode, string configCode, string configValue)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEM2CONFIG where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE like '{0}%'  and ITEMCONFIG like '{1}%' and PARENTCODE like '{2}%'  and CONFIGCODE like '{3}%'  and CONFIGVALUE like '{4}%' ", itemCode, itemConfigration, parentCode, configCode, configValue)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Item2Config
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-1-20 14:24:21
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="itemConfigration">ItemConfigration，模糊查询</param>
        /// <param name="configCode">ConfigCode，模糊查询</param>
        /// <param name="configValue">ConfigValue，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Item2Config数组</returns>
        public object[] QueryItem2Config(string itemCode, string itemConfigration, string parentCode, string configCode, string configValue, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item2Config), new PagerCondition(string.Format("select {0} from TBLITEM2CONFIG where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ITEMCODE like '{1}%'  and ITEMCONFIG like '{2}%' and PARENTCODE like '{3}%'  and CONFIGCODE like '{4}%'  and CONFIGVALUE like '{5}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Config)), itemCode, itemConfigration, parentCode, configCode, configValue), "ITEMCODE,ITEMCONFIG,CONFIGCODE,CONFIGVALUE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Item2Config
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-1-20 14:24:21
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Item2Config的总记录数</returns>
        public object[] GetAllItem2Config()
        {
            return this.DataProvider.CustomQuery(typeof(Item2Config), new SQLCondition(string.Format("select {0} from TBLITEM2CONFIG where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by ITEMCODE,ITEMCONFIG,CONFIGCODE,CONFIGVALUE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Config)))));
        }

        //是否有配置项
        public bool IsConfiged(string itemCode, string itemConfigration, string parentCode, string configCode)
        {

            int count = this.DataProvider.GetCount(new SQLParamCondition(string.Format("select count(*) from TBLITEM2CONFIG where 1=1 and ORGID = " + GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() + " and ITEMCODE=$ITEMCODE and ITEMCONFIG=$ITEMCONFIGRATION and PARENTCODE=$PARENTCODE and CONFIGCODE=$CONFIGCODE "),
                new SQLParameter[]{
										new SQLParameter("$ITEMCODE",typeof(string),itemCode),
										new SQLParameter("$ITEMCONFIGRATION",typeof(string),itemConfigration),
										new SQLParameter("$PARENTCODE",typeof(string),parentCode),
										new SQLParameter("$CONFIGCODE",typeof(string),configCode)
								  }));

            return count > 0 ? true : false;
        }

        //是否有配置项,并且必须Check
        public bool IsMustCheckConfiged(string itemCode, string itemConfigration, string parentCode, string configCode)
        {

            int count = this.DataProvider.GetCount(new SQLParamCondition(string.Format("select count(*) from TBLITEM2CONFIG where 1=1 and ORGID = " + GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() + " and ITEMCODE=$ITEMCODE and ITEMCONFIG=$ITEMCONFIGRATION and PARENTCODE=$PARENTCODE and CONFIGCODE=$CONFIGCODE and NEEDCHECK='1'"),
                new SQLParameter[]{
									  new SQLParameter("$ITEMCODE",typeof(string),itemCode),
									  new SQLParameter("$ITEMCONFIGRATION",typeof(string),itemConfigration),
									  new SQLParameter("$PARENTCODE",typeof(string),parentCode),
									  new SQLParameter("$CONFIGCODE",typeof(string),configCode)
								  }));

            return count > 0 ? true : false;
        }
        #endregion

        #region Item Operation
        public object[] GetOperationsByItem(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(string.Format("select {0} from tblop where opcode in (select opcode from TBLITEMROUTE2OP where itemcode = '{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)), itemCode.ToUpper())));
        }

        public object GetItemRoute2Operation(string itemCode, string routeCode, string opCode)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where itemcode=$itemcode and routecode=$routecode and opcode =$opcode" + GlobalVariables.CurrentOrganizations.GetSQLCondition();
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLParamCondition(selectSql, new SQLParameter[] { new SQLParameter("itemcode",typeof(string),itemCode),new SQLParameter("routecode",typeof(string),routeCode),
			                                                                                                                        new SQLParameter("opcode",typeof(string), opCode)}));
            if (objs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_NotExisted");
            }
            return objs[0];
        }

        public bool IsOPInItemOP(string itemCode, int orgID, string OPCode)
        {
            string sql = "";
            sql += "SELECT COUNT (*)";
            sql += "  FROM tblitemroute2op";
            sql += " WHERE itemcode = '" + itemCode + "' ";
            sql += "   AND opcode = '" + OPCode + "' ";
            sql += "   AND orgid = " + orgID;
            if (this.DataProvider.GetCount(new SQLCondition(sql)) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object[] QueryItemRoute2Operation(string itemCode, string routeCode)
        {
            string sql = "select b.opcode,b.opdesc from tblitemroute2op a left join tblop b on a.opcode = b.opcode where 1=1";
            if (itemCode != string.Empty)
            {
                sql += " and a.itemCode='" + itemCode + "'";
            }
            if (routeCode != string.Empty)
            {
                sql += " and a.routecode='" + routeCode + "'";
            }
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by a.opseq ";
            object[] obj = this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(sql));
            if (obj != null && obj.Length > 0)
            {
                return obj;
            }
            else
            {
                return null;
            }
        }

        public object[] QueryItemRoute2Op(string routeCode, string itemCode)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)) + " from tblitemroute2op where  routecode=$routecode and itemcode=$itemcode" + GlobalVariables.CurrentOrganizations.GetSQLCondition()+ " order by opseq";
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLParamCondition(selectSql, new SQLParameter[] { new SQLParameter("routecode", typeof(string), routeCode), new SQLParameter("itemcode", typeof(string), itemCode) }));
            if (objs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_NotExisted");
            }
            return objs;
        }

        #endregion

        #region ItemLoaction
        public ItemLocation CreateNewItemLocation()
        {
            return new ItemLocation();
        }

        public object GetItemLocation(string itemCode, string itemLocationCode, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(ItemLocation), new object[] { itemCode, itemLocationCode, orgID });
        }

        public void AddItemLocation(ItemLocation itemLocation)
        {
            this._helper.AddDomainObject(itemLocation);
        }

        public void UpdateItemLocation(ItemLocation itemLocation)
        {
            this._helper.UpdateDomainObject(itemLocation);
        }

        public void DeleteItemLoaction(ItemLocation[] itemLocations)
        {
            this._helper.DeleteDomainObject(itemLocations);
        }

        public object[] QueryItemLocation(string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ItemLocation), new PagerCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemLocation)) + " from tblitemlocation where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}'", itemCode), inclusive, exclusive));
        }

        public int QueryItemLocationCount(string itemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(String.Format("select count(*) from tblitemlocation where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode='{0}'", itemCode)));
        }


        #endregion

        #region Item2SPCTable
        //SPC 数据收集表 joe song 20050822
        public Item2SPCTable CreateNewItem2SPCTable()
        {
            return new Item2SPCTable();
        }

        public object GetItem2SPCTable(string OID)
        {
            return this.DataProvider.CustomSearch(typeof(Item2SPCTable), new object[] { OID });
        }

        public object[] GetItem2SPCTable(string itemCode, string tblName)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTable), new BenQGuru.eMES.Common.Domain.SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTable)) + " from TblItem2SPCTbl where itemcode='{0}' and spctblname='{1}'", itemCode, tblName)));
        }

        public object[] GetItem2SPCTable(string itemCode, int startDate, int endDate)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTable), new BenQGuru.eMES.Common.Domain.SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTable)) + " from TblItem2SPCTbl where itemcode='{2}' and (({0}>=StartDate and {0}<=endDate) or ({1}>=StartDate and {1}<=endDate))", startDate, endDate, itemCode)));
        }

        public Item2SPCTable GetItem2SPCTable(string itemCode, int date)
        {
            object[] objs = this.DataProvider.CustomQuery(
                                                        typeof(Item2SPCTable),
                                                        new BenQGuru.eMES.Common.Domain.SQLCondition(
                                                                                                    String.Format(
                                                                                                                    "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTable)) + " from TblItem2SPCTbl where itemcode='{0}' and {1}>=StartDate and {1}<=endDate", itemCode, date
                                                                                                                )
                                                                                                    )
                                                        );
            if (objs != null && objs.Length > 0)
            {
                Item2SPCTable spctable = objs[0] as Item2SPCTable;
                return spctable;
            }
            else
                return null;
        }

        public void AddItem2SPCTable(Item2SPCTable item2SPCTable)
        {
            this._helper.AddDomainObject(item2SPCTable);
        }

        public void UpdateItem2SPCTable(Item2SPCTable item2SPCTable)
        {
            this._helper.UpdateDomainObject(item2SPCTable);
        }

        public void DeleteItem2SPCTable(Item2SPCTable[] item2SPCTable)
        {
            this._helper.DeleteDomainObject(item2SPCTable);
        }

        public object[] QueryItem2SPCTable(string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTable), new PagerCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTable)) + " from (select * from TblItem2SPCTbl  order by SPCTBLNAME) where itemcode='{0}'", itemCode), inclusive, exclusive));
        }

        public int QueryItem2SPCTableCount(string itemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(String.Format("select count(*) from TblItem2SPCTbl where itemcode='{0}'", itemCode)));
        }


        #endregion

        #region Item2SPCTest
        //SPC 产品测试项 joe song 20050822
        public Item2SPCTest CreateNewItem2SPCTest()
        {
            return new Item2SPCTest();
        }

        public object GetItem2SPCTest(string OID)
        {
            return this.DataProvider.CustomSearch(typeof(Item2SPCTest), new object[] { OID });
        }

        public object[] GetItem2SPCTest(string itemCode, string testName)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTest), new BenQGuru.eMES.Common.Domain.SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTest)) + " from TblItem2SPCTEST where itemcode='{0}' and testname='{1}'", itemCode, testName)));
        }

        public object[] GetItem2SPCTestUpper(string itemCode, string testName)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTest), new BenQGuru.eMES.Common.Domain.SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTest)) + " from TblItem2SPCTEST where itemcode='{0}' and upper(testname)='{1}'", itemCode, testName)));
        }

        public object[] QueryItem2SPCTest(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTest), new BenQGuru.eMES.Common.Domain.SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTest)) + " from TblItem2SPCTEST where itemcode='{0}'", itemCode)));
        }

        public object[] GetItem2SPCTest(string itemCode, int seq)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTest), new BenQGuru.eMES.Common.Domain.SQLCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTest)) + " from TblItem2SPCTEST where itemcode='{0}' and seq={1}", itemCode, seq)));
        }

        public void AddItem2SPCTest(Item2SPCTest item2spctest)
        {
            this._helper.AddDomainObject(item2spctest);
        }

        public void UpdateItem2SPCTest(Item2SPCTest item2spctest)
        {
            this._helper.UpdateDomainObject(item2spctest);
        }

        public void DeleteItem2SPCTTest(Item2SPCTest[] item2spctest)
        {
            this._helper.DeleteDomainObject(item2spctest);
        }

        public object[] QueryItem2SPCTest(string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item2SPCTest), new PagerCondition(String.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2SPCTest)) + " from TblItem2SPCTEST where itemcode='{0}' order by Seq", itemCode), inclusive, exclusive));
        }

        public int QueryItem2SPCTestCount(string itemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(String.Format("select count(*) from TblItem2SPCTest where itemcode='{0}'", itemCode)));
        }


        #endregion

        #region Item2CartonCFG
        /// <summary>
        /// POWER包装站防错检查需求,本需求只适用于POWER 厂，其它工厂可以通过设置屏蔽该功能。
        /// </summary>
        public Item2CartonCFG CreateNewItem2CartonCFG()
        {
            return new Item2CartonCFG();
        }

        public void AddItem2CartonCFG(Item2CartonCFG item2CartonCFG)
        {
            this._helper.AddDomainObject(item2CartonCFG);
        }

        public void UpdateItem2CartonCFG(Item2CartonCFG item2CartonCFG)
        {
            this._helper.UpdateDomainObject(item2CartonCFG);
        }

        public void DeleteItem2CartonCFG(Item2CartonCFG item2CartonCFG)
        {
            this._helper.DeleteDomainObject(item2CartonCFG);
        }

        public void DeleteItem2CartonCFG(Item2CartonCFG[] item2CartonCFG)
        {
            this._helper.DeleteDomainObject(item2CartonCFG);
        }

        public object GetItem2CartonCFG(string itemName)
        {
            return this.DataProvider.CustomSearch(typeof(Item2CartonCFG), new object[] { itemName });
        }

        /// <summary>
        /// ** 功能描述:	查询Item2CartonCFG的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-1-8 8:40:21
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemName">ItemName，模糊查询</param>
        /// <returns> Item2CartonCFG的总记录数</returns>
        public int QueryItem2CartonCFGCount(string itemName)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLItem2CartonCFG where 1=1 and ItemName like '{0}%' ", itemName)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Item2CartonCFG
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-1-8 8:40:21
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemName">ItemName，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Item2CartonCFG数组</returns>
        public object[] QueryItem2CartonCFG(string itemName, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item2CartonCFG), new PagerCondition(string.Format("select {0} from TBLItem2CartonCFG where 1=1 and ItemName like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2CartonCFG)), itemName), "ItemName", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Item2CartonCFG
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-1-8 8:40:21
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Item2CartonCFG的总记录数</returns>
        public object[] GetAllItem2CartonCFG()
        {
            return this.DataProvider.CustomQuery(typeof(Item2CartonCFG), new SQLCondition(string.Format("select {0} from TBLItem2CartonCFG order by ItemName", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2CartonCFG)))));
        }


        #endregion

        #region this is for cs
        public ItemRoute2OP GetMORouteFirstOperation(string moCode, string routeCode)
        {
            MOFacade moFacade = new MOFacade(this.DataProvider);
            object mo = moFacade.GetMO(moCode);
            if (mo == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_MONotExisted");
            }
            string selectSql = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP))
                            + " from tblitemroute2op where itemcode = $itemcode and routecode = $routecode and "
                            + " opseq = (select min(opseq) from tblitemroute2op"
                            + " where itemcode = $itemcode1 and routecode = $routecode1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")"
                            + GlobalVariables.CurrentOrganizations.GetSQLCondition();
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLParamCondition(selectSql, new SQLParameter[] {
						  new SQLParameter("itemcode",typeof(string),((MO)mo).ItemCode),new SQLParameter("routecode",typeof(string), routeCode),
			              new SQLParameter("itemcode1",typeof(string),((MO)mo).ItemCode),new SQLParameter("routecode1",typeof(string), routeCode)}));
            if (objs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_ItemRoute_NotExist", String.Format("[$Itemcode='{0}',$routecode='{1}']", ((MO)mo).ItemCode, routeCode));
            }
            return ((ItemRoute2OP)objs[0]);
        }

        public ItemRoute2OP GetMORouteNextOperation(string moCode, string routeCode, string opCode)
        {
            MOFacade moFacade = new MOFacade(this.DataProvider);
            object mo = moFacade.GetMO(moCode);
            if (mo == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_MONotExisted");
            }
            string selectSql = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP))
                + " from tblitemroute2op where itemcode = $itemcode and routecode = $routecode "
                + GlobalVariables.CurrentOrganizations.GetSQLCondition()
                + " and  opseq > (select opseq from tblitemroute2op"
                + " where itemcode = $itemcode1 and routecode = $routecode1 and opcode=$opcode1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")  order by opseq";
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemRoute2OP), new SQLParamCondition(selectSql, new SQLParameter[] {
																																	new SQLParameter("itemcode",typeof(string),((MO)mo).ItemCode),new SQLParameter("routecode",typeof(string), routeCode),
																																	new SQLParameter("itemcode1",typeof(string),((MO)mo).ItemCode),new SQLParameter("routecode1",typeof(string), routeCode),
			                                                                                                                        new SQLParameter("opcode1",typeof(string), opCode)}));
            if (objs == null)
            {
                return null;
            }
            return ((ItemRoute2OP)objs[0]);
        }

        public bool OperationIsRouteLastOperation(string moCode, string routeCode, string OpCode)
        {
            MOFacade moFacade = new MOFacade(this.DataProvider);
            object mo = moFacade.GetMO(moCode);
            if (mo == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_MONotExisted");
            }
            string selectSql = " select  count(*)"
                + " from tblitemroute2op where itemcode = $itemcode and routecode = $routecode "
                + GlobalVariables.CurrentOrganizations.GetSQLCondition()
                + "  and opseq = (select max(opseq) from tblitemroute2op"
                + " where itemcode = $itemcode1 and routecode = $routecode1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and opcode=$opcode1";
            int iCount = this.DataProvider.GetCount(new SQLParamCondition(selectSql, new SQLParameter[] {
						new SQLParameter("itemcode",typeof(string),((MO)mo).ItemCode),new SQLParameter("routecode",typeof(string), routeCode),
						new SQLParameter("itemcode1",typeof(string),((MO)mo).ItemCode),new SQLParameter("routecode1",typeof(string), routeCode),
						new SQLParameter("opcode1",typeof(string), OpCode)}));
            if (iCount > 0)
            {
                return true;
            }
            return false;

        }

        #endregion

        #region Item Check

        public object[] QueryItemCheck(string itemCode, int inclusive, int exclusive)
        {
            string sWhere = string.Empty;
            if (itemCode.Trim().Length > 0)
            {
                sWhere = string.Format(" and item.itemcode in ( {0} ) ", FormatHelper.ProcessQueryValues(itemCode));
            }

            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sWhere += " and item.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ") ";
            }

            string sql;
            sql = "";
            sql += "select item.itemcode,";
            sql += "       'OK' as modelcode,";
            sql += "       case (select modelcode";
            sql += "           from tblbarcoderule";
            sql += "          where MODELCODE = (select modelcode";
            sql += "                               from tblmodel2item";
            sql += "                              where itemcode = item.itemcode and orgid = item.orgid " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + "))";
            sql += "         when item.modelcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as barcode,";
            sql += "       case (select modelcode";
            sql += "           from tblmodel2ecg";
            sql += "          where MODELCODE = (select modelcode";
            sql += "                               from tblmodel2item";
            sql += "                              where itemcode = item.itemcode and orgid = item.orgid " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
            sql += "            and rownum = 1)";
            sql += "         when item.modelcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as ecg,";
            sql += "       case (select modelcode";
            sql += "           from tblmodel2ecsg";
            sql += "          where MODELCODE = (select modelcode";
            sql += "                               from tblmodel2item";
            sql += "                              where itemcode = item.itemcode and orgid = item.orgid " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
            sql += "            and rownum = 1)";
            sql += "         when item.modelcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as ecs,";
            sql += "       case (select modelcode";
            sql += "           from tblmodel2solution";
            sql += "          where MODELCODE = (select modelcode";
            sql += "                               from tblmodel2item";
            sql += "                              where itemcode = item.itemcode and orgid = item.orgid " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
            sql += "            and rownum = 1)";
            sql += "         when item.modelcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as solu,";
            sql += "       case (select itemcode";
            sql += "           from tblitem2route";
            sql += "          where itemcode = item.itemcode and orgid = item.orgid ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += "            and rownum = 1)";
            sql += "         when item.itemcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as route,";
            sql += "       case (select itemcode";
            sql += "           from tblitem2oqccklist";
            sql += "          where itemcode = item.itemcode and orgid = item.orgid ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += "            and rownum = 1)";
            sql += "         when item.itemcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as oqcck,";
            sql += "       case (select itemcode";
            sql += "           from tblsbom";
            sql += "          where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode = item.itemcode and orgid = item.orgid ";
            sql += "            and rownum = 1)";
            sql += "         when item.itemcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as sbom,";
            sql += "       case (select itemcode";
            sql += "           from tblopbom";
            sql += "          where itemcode = item.itemcode and orgid = item.orgid ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += "            and rownum = 1)";
            sql += "         when item.itemcode then";
            sql += "          '{1}'";
            sql += "         else";
            sql += "          '{2}'";
            sql += "       end as opbpm,";
            sql += "	   opbomdt.obcode as routecode,";
            sql += "       sum( decode( opbomdt.obitemcontype,'{4}',opbomdt.obitemqty,0 )) as keyparts,";
            sql += "       sum( decode( opbomdt.obitemcontype,'{3}',opbomdt.obitemqty,0 )) as lot,";
            sql += "       item.orgid";
            sql += "  from tblmodel2item item";
            sql += "  left join tblopbomdetail opbomdt on item.itemcode = opbomdt.itemcode and opbomdt.orgid = item.orgid ";
            sql += "   and opbomdt.actiontype = '0'";
            sql += "   and opbomdt.obcode in (select routecode from tblitem2route where itemcode = item.itemcode " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")";
            sql += "   and opbomdt.opcode in";
            sql += "               (select opcode";
            sql += "                  from tblitemroute2op";
            sql += "                 where itemcode = item.itemcode and orgid = item.orgid ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += "                   and (substr(opcontrol, {5}, 1) = '1'))	 ";
            sql += " where 1=1 {0} group by item.itemcode,item.modelcode,opbomdt.obcode,item.orgid";

            sql = string.Format(sql,
                sWhere, "OK", "NG", "item_control_lot", "item_control_keyparts", ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading + 1));

            return this.DataProvider.CustomQuery(typeof(QDOItemCheck), new PagerCondition(sql, inclusive, exclusive, true));
        }

        public int QueryItemCheckCount(string itemCode)
        {
            string sWhere = string.Empty;
            if (itemCode.Trim().Length > 0)
            {
                sWhere = string.Format(" and itemcode in ( {0} ) ", FormatHelper.ProcessQueryValues(itemCode));
            }
            return this.DataProvider.GetCount(new SQLCondition(String.Format("select count(*) from TblItem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0}", sWhere)));
        }
        #endregion

        #region Item2Dimention
        /// <summary>
        /// 
        /// </summary>
        public Item2Dimention CreateNewItem2Dimention()
        {
            return new Item2Dimention();
        }

        public void AddItem2Dimention(Item2Dimention item2Dimention)
        {
            this._helper.AddDomainObject(item2Dimention);
        }

        public void UpdateItem2Dimention(Item2Dimention item2Dimention)
        {
            this._helper.UpdateDomainObject(item2Dimention);
        }

        public void DeleteItem2Dimention(Item2Dimention item2Dimention)
        {
            this._helper.DeleteDomainObject(item2Dimention);
        }

        public void DeleteItem2Dimention(Item2Dimention[] item2Dimention)
        {
            this._helper.DeleteDomainObject(item2Dimention);
        }

        public object GetItem2Dimention(string itemCode, string paramname, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(Item2Dimention), new object[] { itemCode, paramname, orgID });
        }

        /// <summary>
        /// ** 功能描述:	查询Item2Dimention的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 9:45:37
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <returns> Item2Dimention的总记录数</returns>
        public int QueryItem2DimentionCount(string itemCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEM2DIM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ItemCode like '{0}%' ", itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Item2Dimention
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 9:45:37
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Item2Dimention数组</returns>
        public object[] QueryItem2Dimention(string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item2Dimention), new PagerCondition(string.Format("select {0} from TBLITEM2DIM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ItemCode like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Dimention)), itemCode), "ItemCode", inclusive, exclusive));
        }

        public object[] QueryItem2Dimention(string itemCode)
        {
            return this.DataProvider.CustomQuery(typeof(Item2Dimention),
                                                new SQLCondition(string.Format("select {0} from TBLITEM2DIM where 1=1 and ORGID = " + GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString() + " and ItemCode = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Dimention)), itemCode)));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Item2Dimention
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-19 9:45:37
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Item2Dimention的总记录数</returns>
        public object[] GetAllItem2Dimention()
        {
            return this.DataProvider.CustomQuery(typeof(Item2Dimention), new SQLCondition(string.Format("select {0} from TBLITEM2DIM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by ItemCode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Dimention)))));
        }


        #endregion

        #region Item2SNCheck
        /// <summary>
        /// 
        /// </summary>
        public Item2SNCheck CreateNewItem2SNCheck()
        {
            return new Item2SNCheck();
        }

        public void AddItem2SNCheck(Item2SNCheck item2SNCheck)
        {
            this._helper.AddDomainObject(item2SNCheck);
        }

        public void UpdateItem2SNCheck(Item2SNCheck item2SNCheck)
        {
            this._helper.UpdateDomainObject(item2SNCheck);
        }

        public void DeleteItem2SNCheck(Item2SNCheck item2SNCheck)
        {
            this._helper.DeleteDomainObject(item2SNCheck);
        }

        public void DeleteItem2SNCheck(Item2SNCheck[] item2SNCheck)
        {
            this._helper.DeleteDomainObject(item2SNCheck);
        }

        public object GetItem2SNCheck(string itemCode, string type)
        {
            return this.DataProvider.CustomSearch(typeof(Item2SNCheck), new object[] { itemCode, type });
        }

        public object[] QueryItem2SNCheck(string itemCode)
        {
            string sql = string.Format("SELECT * FROM tblitem2sncheck WHERE itemCode='{0}'", itemCode);
            return this.DataProvider.CustomQuery(typeof(Item2SNCheck), new SQLCondition(sql));
        }

        //根据序列号长度和序列号前缀获取itemCode
        public object[] GetSN(string snPrefix, string snLength)
        {
            string sql = string.Format("SELECT DISTINCT itemCode AS ItemCode FROM tblitem2sncheck WHERE snprefix='{0}' AND snlength='{1}'", snPrefix, snLength);
            return this.DataProvider.CustomQuery(typeof(Item2SNCheckMP), new SQLCondition(sql));
        }

        //获取ItemType类型
        public string[] GetItemType()
        {
            return new string[]{
								   ItemType.ITEMTYPE_FINISHEDPRODUCT,
								   ItemType.ITEMTYPE_SEMIMANUFACTURE
							   };
        }

        private string GenerateQueryItem2SNCheckSQL(string itemCode, string itemDesc, string itemType, string itemCheckType, string exportImport, string snPrefix, string snLength, bool ifForCount)
        {
            string sql = "";

            if (ifForCount)
            {
                sql = "SELECT COUNT(*) FROM tblitem2sncheck t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode";
            }
            else
            {
                sql = "SELECT t1.itemcode AS ItemCode, t1.snprefix AS SNPrefix, t1.snlength AS SNLength,T1.TYPE AS TYPE, t2.mdesc AS ItemDesc FROM tblitem2sncheck t1, tblmaterial t2 WHERE t1.itemcode = t2.mcode";
            }

            if (itemCode != null && itemCode.Length != 0)
            {
                sql = string.Format("{0} AND t1.itemCode like '%{1}%' ", sql, itemCode);
            }

            if (itemCheckType.Trim() != string.Empty && itemCheckType != null)
            {
                sql += "  AND t1.type='" + itemCheckType.Trim().ToUpper() + "'";
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

        private string GenerateQueryItem2SNCheckSQL(string itemCode, string itemDesc, string itemType, string exportImport, string snPrefix, string snLength, bool ifForCount)
        {
            return GenerateQueryItem2SNCheckSQL(itemCode, itemDesc, itemType, string.Empty, exportImport, snPrefix, snLength, ifForCount);
        }

        /// <summary>
        /// ** 功能描述:	分页查询Item2SNCheck
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Roger xue
        /// ** 日 期:		2008-09-01
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Item2SNCheck数组</returns>
        public object[] QueryItem2SNCheck(string itemCode, string itemDesc, string itemType, string exportImport, string snPrefix, string snLength, int inclusive, int exclusive)
        {
            string sql = this.GenerateQueryItem2SNCheckSQL(itemCode, itemDesc, itemType, exportImport, snPrefix, snLength, false);

            return this.DataProvider.CustomQuery(typeof(Item2SNCheckMP), new PagerCondition(sql, "ItemCode", inclusive, exclusive));
        }

        public object[] QueryItem2SNCheck(string itemCode, string itemDesc, string itemType, string itemCheckType, string exportImport, string snPrefix, string snLength, int inclusive, int exclusive)
        {
            string sql = this.GenerateQueryItem2SNCheckSQL(itemCode, itemDesc, itemType, itemCheckType, exportImport, snPrefix, snLength, false);

            return this.DataProvider.CustomQuery(typeof(Item2SNCheckMP), new PagerCondition(sql, "ItemCode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	查询Item2SNCheck的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Roger xue
        /// ** 日 期:		2005-09-01
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="checkItemCode">ItemCode，模糊查询</param>
        /// <returns> Item2SNCheck的总记录数</returns>
        public int QueryItem2SNCheckCount(string itemCode, string itemDesc, string itemType, string exportImport, string snPrefix, string snLength)
        {
            string sql = this.GenerateQueryItem2SNCheckSQL(itemCode, itemDesc, itemType, exportImport, snPrefix, snLength, true);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion        

        #region Material

        public Domain.MOModel.Material CreateNewMaterial()
        {
            return new Domain.MOModel.Material();
        }

        public void AddMaterial(Domain.MOModel.Material material)
        {
            this._helper.AddDomainObject(material);
        }

        public void UpdateMaterial(Domain.MOModel.Material material)
        {
            this._helper.UpdateDomainObject(material);
        }

        public void DeleteMaterial(Domain.MOModel.Material material)
        {
            this._helper.DeleteDomainObject(material);
        }

        public void DeleteMaterial(Domain.MOModel.Material[] materials)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                foreach (Domain.MOModel.Material material in materials)
                {
                    DeleteMaterial(material);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public object GetMaterial(string materialCode, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(Domain.MOModel.Material), new object[] { materialCode, orgID });
        }
        public object GetMaterial(string materialCode)
        {
            return this.DataProvider.CustomSearch(typeof(Domain.MOModel.Material), new object[] { materialCode});
        }

        //根据鼎桥物料编码货位物料信息 add by jinger 20160122
        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <param name="dQMCode">鼎桥物料编码</param>
        /// <returns></returns>
        public object GetMaterialByDQMCode(string dQMCode)
        {
            string sql = string.Format(@"SELECT {0} FROM tblmaterial WHERE DQMCODE = '{1}' ORDER BY mdate DESC,mtime DESC ", 
                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.MOModel.Material)),dQMCode);
            object[] objMaterial = this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
            if (objMaterial != null && objMaterial.Length > 0)
            {
                return objMaterial[0];
            }
            return null;
        }


        #region
        /// <summary>
        /// 物料维护查询
        /// </summary>
        /// <param name="materialCode"></param>
        /// <param name="DQmaterialCode"></param>
        /// <param name="materialDesc"></param>
        /// <param name="marterialType"></param>
        /// <param name="materialSource"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[]  GetMaterial(string materialCode, string DQmaterialCode, string materialDesc, string marterialType, string materialSource, int inclusive, int exclusive)
        {
            string sql = @"select eattribute1, validfrom, muser, mrpcontorller, menshortdesc, cuser, materialtype, eattribute3, 
               menlongdesc, rohs, modelcode, mstate3, mstate2, mstate1, mrptype, reorderpoint, dqmcode, mspecialdesc, muom,
            minimumlotsize, roundingvalue, sourceflag, ctime, purchasinggroup, mchlongdesc, mdate, safetystock, validity, specialprocyrement, 
            abcindicator, mcode, mtype, mchshortdesc, bulkmaterial, mstate, cdfor, cdate, mtime,
            (CASE WHEN mcontroltype ='item_control_keyparts'  THEN '单件管控' WHEN mcontroltype= 'item_control_lot' 
            THEN '批次管控' WHEN mcontroltype='item_control_nocontrol' THEN '不管控' else  mcontroltype END) 
            AS mcontroltype, eattribute8, cdqty, eattribute2 from tblmaterial where 1=1 and ( 1=1 ";

            if (!string.IsNullOrEmpty(materialDesc))
                sql += " or MENSHORTDESC like '%{1}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " or MENLONGDESC like '%{1}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " or MCHSHORTDESC like '%{1}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " or MCHLONGDESC like '%{1}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " or MSPECIALDESC like '%{1}%'";

            sql += ")";
 
            if (!string.IsNullOrEmpty(materialCode))
                sql += " and mcode like '{2}%'";
            if (!string.IsNullOrEmpty(DQmaterialCode))
                sql += " and DQMCODE like '{3}%'";

            if (!string.IsNullOrEmpty(marterialType))
                sql += " and MControlType='{4}'";
            if (!string.IsNullOrEmpty(materialSource))
                sql += " and SOURCEFLAG='{5}'";

            sql += " ORDER BY MDATE DESC,MTIME DESC";
            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.MOModel.Material)), materialDesc, materialCode, DQmaterialCode, marterialType, materialSource), inclusive, exclusive));

        }
        public int GetMaterialCount(string materialCode, string DQmaterialCode, string materialDesc, string marterialType, string materialSource)
        {
            string sql = "select count(*) from tblmaterial where 1=1 and ( 1=1 ";

            if (!string.IsNullOrEmpty(materialDesc))
                sql += " and MENSHORTDESC like '%{0}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " and MENLONGDESC like '%{0}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " and MCHSHORTDESC like '%{0}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " and MCHLONGDESC like '%{0}%'";
            if (!string.IsNullOrEmpty(materialDesc))
                sql += " or MSPECIALDESC like '%{0}%'";

            sql += ")";

            if (!string.IsNullOrEmpty(materialCode))
                sql += " and mcode like '{1}%'";
            if (!string.IsNullOrEmpty(DQmaterialCode))
                sql += " and DQMCODE like '{2}%'";

            if (!string.IsNullOrEmpty(marterialType))
                sql += " and MControlType='{3}'";
            if (!string.IsNullOrEmpty(materialSource))
                sql += " and SOURCEFLAG='{4}'";

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, materialDesc, materialCode, DQmaterialCode, marterialType, materialSource)));
        }
        #endregion

        public int QueryMaterialCount(string materialCode, string materialName)
        {
            return this.QueryMaterialCount(materialCode, materialName, string.Empty, string.Empty);
        }

        public int QueryMaterialCount(string materialCode, string materialName, string MControlType, string MaterialDescription)
        {
            string selectSql = "select count(mcode) from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0}";
            string tmpString = string.Empty;
            if ((materialCode != string.Empty) && (materialCode.Trim() != string.Empty))
            {
                tmpString += " and mcode like '" + materialCode.Trim() + "%'";
            }
            if ((materialName != string.Empty) && (materialName.Trim() != string.Empty))
            {
                tmpString += " and mname like '" + materialName.Trim() + "%'";
            }
            if ((MControlType != string.Empty) && (MControlType.Trim() != string.Empty))
            {
                tmpString += " and upper(mcontroltype) = '" + MControlType.Trim() + "'";
            }
            if ((MaterialDescription != string.Empty) && (MaterialDescription.Trim() != string.Empty))
            {
                tmpString += " and upper(mdesc) like '%" + MaterialDescription.Trim() + "%'";
            }
            return this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql, tmpString)));
        }


        public object[] QueryMaterial(string materialCode, string materialName, int inclusive, int exclusive)
        {
            return this.QueryMaterial(materialCode, materialName, string.Empty, string.Empty, inclusive, exclusive);
        }

        public object[] QueryMaterial(string materialCode, string materialName, string MControlType, string MaterialDescription, int inclusive, int exclusive)
        {
            string selectSql = "select" + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.MOModel.Material)) + "  from tblmaterial where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0}";
            string tmpString = string.Empty;
            if ((materialCode != string.Empty) && (materialCode.Trim() != string.Empty))
            {
                tmpString += " and mcode like '" + materialCode.Trim() + "%'";
            }
            if ((materialName != string.Empty) && (materialName.Trim() != string.Empty))
            {
                tmpString += " and upper(mname) like '" + materialName.Trim() + "%'";
            }
            if ((MControlType != string.Empty) && (MControlType.Trim() != string.Empty))
            {
                tmpString += " and upper(mcontroltype) = '" + MControlType.Trim() + "'";
            }
            if ((MaterialDescription != string.Empty) && (MaterialDescription.Trim() != string.Empty))
            {
                tmpString += " and upper(mdesc) like '%" + MaterialDescription.Trim() + "%'";
            }
            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new PagerCondition(String.Format(selectSql, tmpString), inclusive, exclusive));
        }

        public object[] QueryMaterialForErrorPart(string itemCode, string errorLocation)
        {
            string sql = string.Empty;

            sql += "";
            sql += "SELECT tblmaterial.mcode, tblmaterial.mdesc ";
            sql += "FROM ( ";
            sql += "    SELECT DISTINCT sbitemcode, location ";
            sql += "    FROM tblsbom ";
            sql += "    CONNECT BY PRIOR sbitemcode = itemcode ";
            sql += "    START WITH itemcode = '{0}' ";
            sql += ") tblloc, tblmaterial ";
            sql += "WHERE tblloc.sbitemcode = tblmaterial.mcode ";
            sql += "AND tblloc.location LIKE '%{1}%' ";
            sql += "ORDER BY tblmaterial.mcode ";

            sql = string.Format(sql, itemCode.Trim().ToUpper(), errorLocation.Trim());

            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
        }

        public object GetMaterialWithMoCode(string moCode)
        {
            string sql = string.Empty;
            sql += " SELECT DISTINCT A.*  FROM TBLMATERIAL A, TBLMO B";
            sql += " WHERE A.MCODE = B.ITEMCODE   AND A.ORGID = B.ORGID";
            if (moCode.Trim() != string.Empty)
            {
                sql += " AND B.MOCODE = '" + moCode.Trim().ToUpper() + "'";
            }

            object[] returnObjects = this._domainDataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
            if (returnObjects != null && returnObjects.Length > 0)
            {
                return returnObjects[0];
            }

            return null;
        }

        public object[] GetAllMaterial()
        {
            string sql = string.Empty;
            sql += "SELECT tblmaterial.mcode, tblmaterial.mdesc ";
            sql += " from tblmaterial order by mcode  ";

            return this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
        }

        #endregion

        #region Vendor

        public Vendor CreateNewVendor()
        {
            return new Vendor();
        }

        public void AddVendor(Vendor vendor)
        {
            this._helper.AddDomainObject(vendor);
        }

        public void UpdateVendor(Vendor vendor)
        {
            this._helper.UpdateDomainObject(vendor);
        }

        public void DeleteVendor(Vendor[] vendor)
        {
            this._helper.DeleteDomainObject(vendor);
        }

        public object GetVender(string VendorCode)
        {
            return this.DataProvider.CustomSearch(typeof(Vendor), new object[] { VendorCode });
        }

        public object[] GetAllVender()
        {
            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Vendor)) + " FROM tblvendor ORDER BY vendorcode";
            return this.DataProvider.CustomQuery(typeof(Vendor), new SQLCondition(sql));
        }

        #endregion
    }
    [Serializable]
    public class QDOItemCheck : DomainObject
    {
        [FieldMapAttribute("itemcode", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("modelcode", typeof(string), 40, true)]
        public string ModelCode;

        [FieldMapAttribute("barcode", typeof(string), 40, true)]
        public string BarCode;

        [FieldMapAttribute("ecg", typeof(string), 40, true)]
        public string ErrorCodeGroup;

        [FieldMapAttribute("ecs", typeof(string), 40, true)]
        public string ErrorCause;

        [FieldMapAttribute("solu", typeof(string), 40, true)]
        public string Solution;

        [FieldMapAttribute("route", typeof(string), 40, true)]
        public string RouteCode;

        [FieldMapAttribute("oqcck", typeof(string), 40, true)]
        public string OQCCheck;

        [FieldMapAttribute("sbom", typeof(string), 10, true)]
        public string SBOM;

        [FieldMapAttribute("opbpm", typeof(string), 40, true)]
        public string OPBOM;

        [FieldMapAttribute("routecode", typeof(string), 40, true)]
        public string RouteCode2;

        [FieldMapAttribute("keyparts", typeof(decimal), 10, true)]
        public decimal Keyparts;

        [FieldMapAttribute("lot", typeof(decimal), 10, true)]
        public decimal Lot;
    }

    //add by roger xue 2008/09/01
    [Serializable]
    public class Item2SNCheckMP : DomainObject
    {
        [FieldMapAttribute("ItemCode", typeof(string), 40, true)]
        public string ItemCode;

        [FieldMapAttribute("ItemDesc", typeof(string), 40, true)]
        public string ItemDesc;

        [FieldMapAttribute("SNPrefix", typeof(string), 40, true)]
        public string SNPrefix;

        [FieldMapAttribute("SNLength", typeof(int), 6, true)]
        public int SNLength;

        //检查类型
        [FieldMapAttribute("TYPE", typeof(string), 40, true)]
        public string Type;

    }
}
