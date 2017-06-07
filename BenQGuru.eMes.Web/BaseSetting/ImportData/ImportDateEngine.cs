using System;
using System.Data;
using System.Web.UI;
using System.Collections;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting.ImportData
{
    /// <summary>
    /// ImportDateEngine 的摘要说明。
    /// </summary>
    public class ImportDateEngine
    {
        private IDomainDataProvider _dataProvider = null;
        private string ImportType = string.Empty;
        private DataTable ImportDatatable = null;
        private string UserCode = string.Empty;
        private ArrayList errorArray = null;
        private ArrayList ImportGridRow = null;
        private GridHelperNew gridHelper;
        public Page fromPage = null;
        public ArrayList ErrorArray
        {
            get
            {
                if (errorArray == null)
                {
                    errorArray = new ArrayList();
                    return errorArray;
                }
                else
                {
                    return errorArray;
                }
            }
            set
            {
                errorArray = value;
            }
        }
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent();

        public ImportDateEngine(IDomainDataProvider dataProvider, string importType, DataTable table, string userCode, ArrayList importGridRow, GridHelperNew _gridHelper)
        {
            _dataProvider = dataProvider;
            ImportType = importType;
            ImportDatatable = table;
            UserCode = userCode;
            ImportGridRow = importGridRow;
            this.gridHelper = _gridHelper;
        }

        public void CheckDataValid()
        {
            this.Check();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isRollBack">true:出错就回滚；false:出错skip到下一个</param>
        public void Import(bool isRollBack)
        {
            if (isRollBack)
            {
                this.ImportRollBack();
            }
            else
            {
                this.ImportSkip();
            }
        }

        #region Mapping
        /// <summary>
        /// 默认类型
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private object[] ConvertArrayListToObjectArray(DataTable table, string importtypedt)
        {
            importtypedt = importtypedt.Length == 0 ? ImportType : importtypedt;
            TableMapAttribute tableAttribute =
                DomainObjectUtility.GetTableMapAttribute(GetImportType(importtypedt));
            string[] PKs = tableAttribute.GetKeyFields();
            if (PKs != null)
            {
                for (int i = 0; i < PKs.Length; i++)
                {
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        if (string.Compare(PKs[i], "OID", true) != 0
                            && string.Compare(PKs[i], "OPBOMVER", true) != 0
                            && string.Compare(PKs[i], "OPID", true) != 0)
                        {
                            table.Rows[j][PKs[i]] = table.Rows[j][PKs[i]].ToString().ToUpper();
                        }
                    }
                }
            }

            object[] objs = new object[table.Rows.Count];
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (importtypedt == "OPBOMDetail")
                {
                    table.Rows[i]["OBITEMEFFDATE"] = FormatHelper.TODateInt(table.Rows[i]["OBITEMEFFDATE"].ToString());
                    table.Rows[i]["OBITEMINVDATE"] = FormatHelper.TODateInt(table.Rows[i]["OBITEMINVDATE"].ToString());
                }
                object obj = GetImportType(importtypedt);

                obj = DomainObjectUtility.FillDomainObject(obj, table.Rows[i]);
                if (importtypedt == "RMADetail")
                {
                    (obj as Domain.RMA.RMADetial).Memo = table.Rows[i]["RMAMEMO"].ToString();

                }
                if (importtypedt == "OPBOMDetail")
                {
                    (obj as OPBOMDetail).OPBOMParseType = OPBOMDetailParseType.PARSE_BARCODE;
                    (obj as OPBOMDetail).OPBOMCheckType = OPBOMDetailCheckType.CHECK_LINKBARCODE;
                }
                this.GetImportObjectType(ref obj, importtypedt);



                objs.SetValue(obj, i);
            }

            return objs;
        }


        /// <summary>
        /// 返回导入的一个空对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private object GetImportType(string input)
        {
            System.Type type;
            switch (input)
            {
                case "ITEM":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.Item);
                    break;
                case "MODEL":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.Model);
                    break;
                case "Model2Item":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.Model2Item);
                    break;
                case "BARCODERULE":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.BarcodeRule);
                    break;
                case "ErrorCodeA":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.ErrorCodeA);
                    break;
                case "ErrorCodeGroupA":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA);
                    break;
                case "ErrorCause":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.ErrorCause);
                    break;
                case "Duty":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.Duty);
                    break;
                case "Solution":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.Solution);
                    break;
                case "OQCCheckList":
                    type = typeof(BenQGuru.eMES.Domain.OQC.OQCCheckList);
                    break;
                case "User":
                    type = typeof(BenQGuru.eMES.Domain.BaseSetting.User);
                    break;
                case "Item2Route":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.Item2Route);
                    break;
                case "Item2SPCTest":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.Item2SPCTest);
                    break;
                case "ItemLocation":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.ItemLocation);
                    break;
                case "Item2SPCTable":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.Item2SPCTable);
                    break;
                case "Model2ErrorCodeGroup":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.Model2ErrorCodeGroup);
                    break;
                case "Model2Solution":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.Model2Solution);
                    break;
                case "Model2ErrorCause":
                    type = typeof(BenQGuru.eMES.Domain.TSModel.Model2ErrorCause);
                    break;
                case "OPBOM":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.OPBOM);
                    break;
                case "OPBOMDetail":
                    type = typeof(BenQGuru.eMES.Domain.MOModel.OPBOMDetail);
                    break;
                case "CusItemCodeCheckList":
                    type = typeof(BenQGuru.eMES.Domain.RMA.CusItemCodeCheckList);
                    break;
                case "UserGroup2User":
                    type = typeof(BenQGuru.eMES.Domain.BaseSetting.UserGroup2User);
                    break;
                case "RMADetail":
                    type = typeof(BenQGuru.eMES.Domain.RMA.RMADetial);
                    break;
                default:
                    type = typeof(System.Object);
                    break;
            }

            return DomainObjectUtility.CreateTypeInstance(type);
        }

        /// <summary>
        /// 补充空缺的不允许为空的栏位
        /// </summary>
        /// <param name="obj">导入对象的引用</param>
        private void GetImportObjectType(ref object obj, string importtype)
        {
            if (obj == null)
            {
                return;
            }

            switch (importtype)
            {
                case "ITEM":
                    BenQGuru.eMES.Domain.MOModel.Item item = obj as BenQGuru.eMES.Domain.MOModel.Item;
                    item.MaintainUser = UserCode;
                    item.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    item.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    item.ItemUser = UserCode;
                    item.ItemDate = item.MaintainDate;
                    break;
                case "Model2Item":
                    BenQGuru.eMES.Domain.MOModel.Model2Item model2Item = obj as BenQGuru.eMES.Domain.MOModel.Model2Item;
                    model2Item.MaintainUser = UserCode;
                    model2Item.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    model2Item.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    model2Item.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    break;
                case "MODEL":
                    BenQGuru.eMES.Domain.MOModel.Model model = obj as BenQGuru.eMES.Domain.MOModel.Model;
                    model.MaintainUser = UserCode;
                    model.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    model.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "BARCODERULE":
                    BenQGuru.eMES.Domain.MOModel.BarcodeRule barcodeRule = obj as BenQGuru.eMES.Domain.MOModel.BarcodeRule;
                    barcodeRule.MaintainUser = UserCode;
                    barcodeRule.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    barcodeRule.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "ErrorCodeA":
                    BenQGuru.eMES.Domain.TSModel.ErrorCodeA errorCodeA = obj as BenQGuru.eMES.Domain.TSModel.ErrorCodeA;
                    errorCodeA.MaintainUser = UserCode;
                    errorCodeA.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    errorCodeA.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "ErrorCodeGroupA":
                    BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA errorCodeGroupA = obj as BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA;
                    errorCodeGroupA.MaintainUser = UserCode;
                    errorCodeGroupA.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    errorCodeGroupA.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "ErrorCause":
                    BenQGuru.eMES.Domain.TSModel.ErrorCause errorCause = obj as BenQGuru.eMES.Domain.TSModel.ErrorCause;
                    errorCause.MaintainUser = UserCode;
                    errorCause.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    errorCause.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Duty":
                    BenQGuru.eMES.Domain.TSModel.Duty duty = obj as BenQGuru.eMES.Domain.TSModel.Duty;
                    duty.MaintainUser = UserCode;
                    duty.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    duty.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Solution":
                    BenQGuru.eMES.Domain.TSModel.Solution solution = obj as BenQGuru.eMES.Domain.TSModel.Solution;
                    solution.MaintainUser = UserCode;
                    solution.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    solution.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "OQCCheckList":
                    BenQGuru.eMES.Domain.OQC.OQCCheckList oqcCheckList = obj as BenQGuru.eMES.Domain.OQC.OQCCheckList;
                    oqcCheckList.MaintainUser = UserCode;
                    oqcCheckList.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    oqcCheckList.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "User":
                    BenQGuru.eMES.Domain.BaseSetting.User user = obj as BenQGuru.eMES.Domain.BaseSetting.User;
                    user.MaintainUser = UserCode;
                    //导入用户的密码都是EMES
                    user.UserPassword = BenQGuru.eMES.Common.Helper.EncryptionHelper.MD5Encryption("EMES");
                    user.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    user.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Item2Route":
                    BenQGuru.eMES.Domain.MOModel.Item2Route item2Route = obj as BenQGuru.eMES.Domain.MOModel.Item2Route;
                    item2Route.IsReference = BenQGuru.eMES.MOModel.ItemFacade.IsReference_NotUsed;
                    item2Route.MaintainUser = UserCode;
                    item2Route.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    item2Route.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    item2Route.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    break;
                case "Item2SPCTest":
                    BenQGuru.eMES.Domain.MOModel.Item2SPCTest item2SPCTest = obj as BenQGuru.eMES.Domain.MOModel.Item2SPCTest;
                    item2SPCTest.OID = Guid.NewGuid().ToString();
                    item2SPCTest.MaintainUser = UserCode;
                    item2SPCTest.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    item2SPCTest.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "ItemLocation":
                    BenQGuru.eMES.Domain.MOModel.ItemLocation itemLocation = obj as BenQGuru.eMES.Domain.MOModel.ItemLocation;
                    itemLocation.MaintainUser = UserCode;
                    itemLocation.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    itemLocation.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Item2SPCTable":
                    BenQGuru.eMES.Domain.MOModel.Item2SPCTable item2SPCTable = obj as BenQGuru.eMES.Domain.MOModel.Item2SPCTable;
                    item2SPCTable.OID = Guid.NewGuid().ToString();
                    item2SPCTable.MaintainUser = UserCode;
                    item2SPCTable.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    item2SPCTable.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Model2ErrorCodeGroup":
                    BenQGuru.eMES.Domain.TSModel.Model2ErrorCodeGroup model2ErrorCodeGroup = obj as BenQGuru.eMES.Domain.TSModel.Model2ErrorCodeGroup;
                    model2ErrorCodeGroup.MaintainUser = UserCode;
                    model2ErrorCodeGroup.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    model2ErrorCodeGroup.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Model2Solution":
                    BenQGuru.eMES.Domain.TSModel.Model2Solution model2Solution = obj as BenQGuru.eMES.Domain.TSModel.Model2Solution;
                    model2Solution.MaintainUser = UserCode;
                    model2Solution.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    model2Solution.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "Model2ErrorCause":
                    BenQGuru.eMES.Domain.TSModel.Model2ErrorCause model2ErrorCause = obj as BenQGuru.eMES.Domain.TSModel.Model2ErrorCause;
                    model2ErrorCause.MaintainUser = UserCode;
                    model2ErrorCause.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    model2ErrorCause.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "OPBOM":
                    BenQGuru.eMES.Domain.MOModel.OPBOM oPBOM = obj as BenQGuru.eMES.Domain.MOModel.OPBOM;
                    oPBOM.OPBOMVersion = OPBOMFacade.OPBOMVERSION_DEFAULT;
                    oPBOM.Avialable = 1;
                    oPBOM.MaintainUser = UserCode;
                    oPBOM.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    oPBOM.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    oPBOM.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    break;
                case "OPBOMDetail":
                    BenQGuru.eMES.Domain.MOModel.OPBOMDetail oPBOMDetail = obj as BenQGuru.eMES.Domain.MOModel.OPBOMDetail;
                    oPBOMDetail.IsItemCheck = OPBOMFacade.OPBOMISItemCheckValue_DEFAULT;
                    oPBOMDetail.ItemCheckValue = OPBOMFacade.OPBOMItemCheckValue_DEFAULT;
                    oPBOMDetail.OPBOMItemType = OPBOMFacade.OPBOMITEMTYPE_DEFAULT;
                    oPBOMDetail.OPID = oPBOMDetail.OPBOMCode + oPBOMDetail.OPCode + oPBOMDetail.ItemCode;
                    oPBOMDetail.OPBOMVersion = OPBOMFacade.OPBOMVERSION_DEFAULT;
                    oPBOMDetail.OPBOMItemEffectiveTime = FormatHelper.TOTimeInt(DateTime.MinValue);
                    oPBOMDetail.OPBOMItemInvalidTime = FormatHelper.TOTimeInt(DateTime.MaxValue);
                    oPBOMDetail.MaintainUser = UserCode;
                    oPBOMDetail.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    oPBOMDetail.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    oPBOMDetail.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    break;
                case "CusItemCodeCheckList":
                    BenQGuru.eMES.Domain.RMA.CusItemCodeCheckList cusItemCodeCheckList = obj as BenQGuru.eMES.Domain.RMA.CusItemCodeCheckList;
                    cusItemCodeCheckList.MaintainUser = UserCode;
                    cusItemCodeCheckList.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    cusItemCodeCheckList.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "UserGroup2User":
                    BenQGuru.eMES.Domain.BaseSetting.UserGroup2User userGroup2User = obj as BenQGuru.eMES.Domain.BaseSetting.UserGroup2User;
                    userGroup2User.MaintainUser = UserCode;
                    userGroup2User.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                    userGroup2User.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                case "RMADetail":
                    BenQGuru.eMES.Domain.RMA.RMADetial rmaDetail = obj as BenQGuru.eMES.Domain.RMA.RMADetial;
                    rmaDetail.MaintainUser = UserCode;
                    rmaDetail.Mdate = FormatHelper.TODateInt(DateTime.Now);
                    rmaDetail.Mtime = FormatHelper.TOTimeInt(DateTime.Now);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Import Method - private

        /// <summary>
        /// 出错直接回滚的导入方法
        /// </summary>
        private void ImportRollBack()
        {
            object[] objs = ConvertArrayListToObjectArray(ImportDatatable, string.Empty);
            object[] objDt = null;
            if (string.Compare(ImportType, "ITEM", true) == 0)/* 产品 */
            {
                objDt = new object[objs.Length];
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "Model2Item");
            }
            else if (string.Compare(ImportType, "OPBOM", true) == 0)/* 产品工序BOM */
            {
                objDt = new object[objs.Length];
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "OPBOMDetail");
            }

            try
            {
                _dataProvider.BeginTransaction();

                if (string.Compare(ImportType, "User", true) == 0)/* 导入用户 */
                {
                    BenQGuru.eMES.BaseSetting.UserFacade userFacade = new UserFacade(_dataProvider);
                    for (int i = 0; i < objs.Length; i++)
                    {
                        User user = new User();
                        user = objs[i] as BenQGuru.eMES.Domain.BaseSetting.User;
                        userFacade.AddUser(user);
                        User2Org user2Org = new User2Org();
                        user2Org.UserCode = user.UserCode;
                        user2Org.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                        user2Org.IsDefaultOrg = 1;
                        user2Org.MaintainUser = user.MaintainUser;
                        user2Org.MaintainDate = user.MaintainDate;
                        user2Org.MaintainTime = user.MaintainTime;
                        userFacade.AddUser2Org(user2Org);
                    }
                }
                else if (string.Compare(ImportType, "Item2Route", true) == 0)/* 导入产品生产途程的方法 */
                {
                    BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(_dataProvider);
                    for (int i = 0; i < objs.Length; i++)
                    {
                        BenQGuru.eMES.Domain.MOModel.Item2Route item2Route = objs[i] as BenQGuru.eMES.Domain.MOModel.Item2Route;
                        _dataProvider.Insert(item2Route);
                        object[] objsItemOperations = baseModelFacade.GetOperationByRouteCode(item2Route.RouteCode);
                        if (objsItemOperations != null)
                        {
                            for (int k = 0; k < objsItemOperations.Length; k++)
                            {
                                ItemRoute2OP itemroute2OP = new ItemRoute2OP();
                                itemroute2OP.IDMergeRule = 1;
                                itemroute2OP.OPID = item2Route.RouteCode + ((Operation)objsItemOperations[k]).OPCode + item2Route.ItemCode;
                                Route2Operation route2Operation = (Route2Operation)baseModelFacade.GetRoute2Operation(item2Route.RouteCode, ((Operation)objsItemOperations[k]).OPCode);
                                itemroute2OP.IDMergeType = IDMergeType.IDMERGETYPE_IDMERGE;
                                itemroute2OP.ItemCode = item2Route.ItemCode;
                                itemroute2OP.MaintainUser = route2Operation.MaintainUser;
                                itemroute2OP.OPCode = route2Operation.OPCode;
                                itemroute2OP.OPControl = route2Operation.OPControl;
                                itemroute2OP.OPSequence = route2Operation.OPSequence;
                                itemroute2OP.RouteCode = route2Operation.RouteCode;
                                itemroute2OP.OrganizationID = item2Route.OrganizationID;
                                _dataProvider.Insert(itemroute2OP);
                            }
                        }

                    }
                }
                else if (string.Compare(ImportType, "OPBOM", true) == 0)/* 导入产品工序BOM */
                {
                    /* step1: Add Item OPBOM */
                    Hashtable opBOMHT = new Hashtable();
                    for (int i = 0; i < objs.Length; i++)
                    {
                        OPBOM opbom = objs[i] as OPBOM;
                        if (!opBOMHT.ContainsKey(opbom.ItemCode))
                        {
                            this.BuildOPBOM(opbom);
                        }
                    }

                    /* step2: Add Item OPBOMDetail */
                    /* step3: Add to Material */
                    for (int i = 0; i < objDt.Length; i++)
                    {
                        _dataProvider.Insert(objDt[i]);
                        BenQGuru.eMES.Material.WarehouseFacade wf = new BenQGuru.eMES.Material.WarehouseFacade(_dataProvider);
                        wf.AddWarehouseItem(objDt[i] as OPBOMDetail);
                    }
                }
                #region add by andy.xin RMA的导入
                else if (string.Compare(ImportType, "RMADetail", true) == 0)
                {
                    Hashtable RMAHT = new Hashtable();
                    for (int i = 0; i < objs.Length; i++)
                    {

                        RMAFacade _RMAFacade = new RMAFacade(this._dataProvider);
                        Domain.RMA.RMADetial rmaDetail = objs[i] as Domain.RMA.RMADetial;
                        if (!RMAHT.ContainsKey(rmaDetail.Rmabillcode))
                        {
                            RMAHT.Add(rmaDetail.Rmabillcode, "");

                            object objRma = _RMAFacade.GetRMABill(rmaDetail.Rmabillcode);

                            //Step 1. Delete rmaBill and rmaDetail
                            if (objRma != null)
                            {
                                _RMAFacade.DeleteRMABillOnly(objRma as Domain.RMA.RMABill);

                                object[] objrmaDetail = _RMAFacade.QueryRMADetail(rmaDetail.Rmabillcode);

                                foreach (Domain.RMA.RMADetial detail in objrmaDetail)
                                {
                                    _RMAFacade.DeleteRMADetial(detail);
                                }

                            }
                            //Step 2. add rmaBill
                            Domain.RMA.RMABill rmaBill = new Domain.RMA.RMABill();
                            rmaBill.RMABillCode = rmaDetail.Rmabillcode;
                            rmaBill.Status = RMABillStatus.Initial;
                            rmaBill.Memo = rmaDetail.Memo;
                            rmaBill.MaintainDate = rmaDetail.Mdate;
                            rmaBill.MaintainTime = rmaDetail.Mtime;
                            rmaBill.MaintainUser = rmaDetail.MaintainUser;

                            _RMAFacade.AddRMABill(rmaBill);

                        }
                        //Step 3. add rmaBillDetail
                        //Step 3. add rmaBillDetail
                        if ((objs[i] as Domain.RMA.RMADetial).Customcode == "")
                        {
                            (objs[i] as Domain.RMA.RMADetial).Customcode = " ";
                        }
                        _RMAFacade.AddRMADetial(objs[i] as Domain.RMA.RMADetial);


                    }
                }
                #endregion

                else/* 一般导入 */
                {
                    if (objs != null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            _dataProvider.Insert(objs[i]);
                        }
                    }
                    if (objDt != null)
                    {
                        for (int i = 0; i < objDt.Length; i++)
                        {
                            _dataProvider.Insert(objDt[i]);
                        }
                    }
                }
                _dataProvider.CommitTransaction();
                for (int i = 0; i < ImportGridRow.Count; i++)
                {
                    GridRecord row = ImportGridRow[i] as GridRecord;
                    row.Items.FindItemByKey("ImportResult").Text = "导入成功";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                    row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                }
            }
            catch (Exception ex)
            {
                _dataProvider.RollbackTransaction();
                this.ErrorArray.Add(ex);
                for (int i = 0; i < ImportGridRow.Count; i++)
                {
                    GridRecord row = ImportGridRow[i] as GridRecord;
                    row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                    row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                }
            }
        }

        /// <summary>
        /// 出错skip到下一个的方法
        /// </summary>
        private void ImportSkip()
        {
            object[] objs = ConvertArrayListToObjectArray(ImportDatatable, string.Empty);
            object[] objDt = null;
            if (string.Compare(ImportType, "ITEM", true) == 0)
            {
                objDt = new object[objs.Length];
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "Model2Item");
            }
            else if (string.Compare(ImportType, "OPBOM", true) == 0)/* 产品工序BOM */
            {
                objDt = new object[objs.Length];
                objDt = ConvertArrayListToObjectArray(ImportDatatable, "OPBOMDetail");
            }

            try
            {
                _dataProvider.BeginTransaction();

                if (string.Compare(ImportType, "User", true) == 0)/* 导入用户 */
                {
                    BenQGuru.eMES.BaseSetting.UserFacade userFacade = new UserFacade(_dataProvider);
                    for (int i = 0; i < objs.Length; i++)
                    {
                        try
                        {
                            User user = new User();
                            user = objs[i] as BenQGuru.eMES.Domain.BaseSetting.User;
                            userFacade.AddUser(user);
                            User2Org user2Org = new User2Org();
                            user2Org.UserCode = user.UserCode;
                            user2Org.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                            user2Org.IsDefaultOrg = 1;
                            user2Org.MaintainUser = user.MaintainUser;
                            user2Org.MaintainDate = user.MaintainDate;
                            user2Org.MaintainTime = user.MaintainTime;
                            userFacade.AddUser2Org(user2Org);


                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入成功";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                            row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                        }
                        catch (Exception ex)
                        {
                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                            this.ErrorArray.Add(ex);
                        }
                    }
                }
                else if (string.Compare(ImportType, "Item2Route", true) == 0)/* 导入产品生产途程的方法 */
                {
                    BenQGuru.eMES.BaseSetting.BaseModelFacade baseModelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(_dataProvider);
                    for (int i = 0; i < objs.Length; i++)
                    {
                        try
                        {
                            BenQGuru.eMES.Domain.MOModel.Item2Route item2Route = objs[i] as BenQGuru.eMES.Domain.MOModel.Item2Route;
                            _dataProvider.Insert(item2Route);
                            object[] objsItemOperations = baseModelFacade.GetOperationByRouteCode(item2Route.RouteCode);
                            if (objsItemOperations != null)
                            {
                                for (int k = 0; k < objsItemOperations.Length; k++)
                                {
                                    ItemRoute2OP itemroute2OP = new ItemRoute2OP();
                                    itemroute2OP.IDMergeRule = 1;
                                    itemroute2OP.OPID = item2Route.RouteCode + ((Operation)objsItemOperations[k]).OPCode + item2Route.ItemCode;
                                    Route2Operation route2Operation = (Route2Operation)baseModelFacade.GetRoute2Operation(item2Route.RouteCode, ((Operation)objsItemOperations[k]).OPCode);
                                    itemroute2OP.IDMergeType = IDMergeType.IDMERGETYPE_IDMERGE;
                                    itemroute2OP.ItemCode = item2Route.ItemCode;
                                    itemroute2OP.MaintainUser = route2Operation.MaintainUser;
                                    itemroute2OP.OPCode = route2Operation.OPCode;
                                    itemroute2OP.OPControl = route2Operation.OPControl;
                                    itemroute2OP.OPSequence = route2Operation.OPSequence;
                                    itemroute2OP.RouteCode = route2Operation.RouteCode;
                                    itemroute2OP.OrganizationID = item2Route.OrganizationID;
                                    _dataProvider.Insert(itemroute2OP);
                                }
                            }
                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入成功";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                        }
                        catch (Exception ex)
                        {
                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                            this.ErrorArray.Add(ex);
                        }

                    }
                }
                else if (string.Compare(ImportType, "OPBOM", true) == 0)/* 导入产品工序BOM */
                {
                    Hashtable opBOMHT = new Hashtable();
                    for (int i = 0; i < objs.Length; i++)
                    {
                        try
                        {
                            /* step1: Add Item OPBOM */
                            OPBOM opbom = objs[i] as OPBOM;
                            if (!opBOMHT.ContainsKey(opbom.ItemCode))
                            {
                                this.BuildOPBOM(opbom);
                                opBOMHT.Add(opbom.ItemCode, "");
                            }

                            /* step2: Add Item OPBOMDetail */
                            _dataProvider.Insert(objDt[i]);

                            /* step3: Add to Material */
                            BenQGuru.eMES.Material.WarehouseFacade wf = new BenQGuru.eMES.Material.WarehouseFacade(_dataProvider);
                            wf.AddWarehouseItem(objDt[i] as OPBOMDetail);

                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入成功";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                        }
                        catch (Exception ex)
                        {
                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                            this.ErrorArray.Add(ex);
                        }
                    }
                }
                #region add by andy.xin RMA的导入
                else if (string.Compare(ImportType, "RMADetail", true) == 0)
                {
                    Hashtable RMAHT = new Hashtable();
                    for (int i = 0; i < objs.Length; i++)
                    {
                        try
                        {
                            RMAFacade _RMAFacade = new RMAFacade(this._dataProvider);
                            Domain.RMA.RMADetial rmaDetail = objs[i] as Domain.RMA.RMADetial;
                            if (!RMAHT.ContainsKey(rmaDetail.Rmabillcode))
                            {
                                RMAHT.Add(rmaDetail.Rmabillcode, "");

                                object objRma = _RMAFacade.GetRMABill(rmaDetail.Rmabillcode);

                                //Step 1. Delete rmaBill and rmaDetail
                                if (objRma != null)
                                {
                                    _RMAFacade.DeleteRMABillNoTrans(objRma as Domain.RMA.RMABill);
                                }
                                //Step 2. add rmaBill
                                Domain.RMA.RMABill rmaBill = new Domain.RMA.RMABill();
                                rmaBill.RMABillCode = rmaDetail.Rmabillcode;
                                rmaBill.Status = RMABillStatus.Initial;
                                rmaBill.Memo = rmaDetail.Memo;
                                rmaBill.MaintainDate = rmaDetail.Mdate;
                                rmaBill.MaintainTime = rmaDetail.Mtime;
                                rmaBill.MaintainUser = rmaDetail.MaintainUser;
                                _RMAFacade.AddRMABill(rmaBill);


                            }
                            //Step 3. add rmaBillDetail
                            if (rmaDetail.Customcode == "")
                            {
                                rmaDetail.Customcode = " ";
                            }
                            _RMAFacade.AddRMADetial(rmaDetail);

                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入成功";
                            row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                        }
                        catch (Exception ex)
                        {
                            GridRecord row = ImportGridRow[i] as GridRecord;
                            row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                            row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                            //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                            this.ErrorArray.Add(ex);
                        }
                    }
                }

                #endregion

                else/* 一般导入 */
                {
                    if (objs != null && objDt == null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            try
                            {
                                _dataProvider.Insert(objs[i]);
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = "导入成功";
                                row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                            }
                            catch (Exception ex)
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                                row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                                this.ErrorArray.Add(ex);
                            }
                        }
                    }
                    if (objs != null && objDt != null)/* 导入产品 */
                    {
                        for (int i = 0; i < objDt.Length; i++)
                        {
                            try
                            {
                                _dataProvider.Insert(objs[i]);
                                _dataProvider.Insert(objDt[i]);

                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = "导入成功";
                                row.Items.FindItemByKey("ImportResult").CssClass = "LinkColorBlue";
                                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.SuccessedColor;
                            }
                            catch (Exception ex)
                            {
                                GridRecord row = ImportGridRow[i] as GridRecord;
                                row.Items.FindItemByKey("ImportResult").Text = "导入失败";
                                row.Items.FindItemByKey("ImportResult").CssClass = "ForeColorRed";
                                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.FailedColor;
                                this.ErrorArray.Add(ex);
                            }
                        }
                    }
                }
                _dataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                _dataProvider.RollbackTransaction();
                this.ErrorArray.Add(ex);
            }
        }

        #endregion

        #region Check Data Valid

        private void Check()
        {
            switch (ImportType)
            {
                case "ITEM":
                    this.CheckITEM();
                    break;
                case "Model2Item":

                    break;
                case "MODEL":
                    this.CheckModel();
                    break;
                case "BARCODERULE":
                    this.CheckBARCODERULE();
                    break;
                case "ErrorCodeA":
                    this.CheckErrorCodeA();
                    break;
                case "ErrorCodeGroupA":
                    this.CheckErrorCodeGroupA();
                    break;
                case "ErrorCause":
                    this.CheckErrorCause();
                    break;
                case "Duty":
                    this.CheckDuty();
                    break;
                case "Solution":
                    this.CheckSolution();
                    break;
                case "OQCCheckList":
                    this.CheckOQCCheckList(); ;
                    break;
                case "User":
                    this.CheckUser();
                    break;
                case "Item2Route":
                    this.CheckItem2Route();
                    break;
                case "Item2SPCTest":
                    this.CheckItem2SPCTest();
                    break;
                case "ItemLocation":
                    this.CheckItemLocation();
                    break;
                case "Item2SPCTable":
                    this.CheckItem2SPCTable();
                    break;
                case "Model2ErrorCodeGroup":
                    this.CheckModel2ErrorCodeGroup();
                    break;
                case "Model2Solution":
                    this.CheckModel2Solution();
                    break;
                case "Model2ErrorCause":
                    this.CheckModel2ErrorCause();
                    break;
                case "OPBOM":
                    this.CheckOPBOM();
                    break;
                case "CusItemCodeCheckList":
                    this.CheckCusItemCodeCheckList();
                    break;
                case "UserGroup2User":
                    this.CheckUserGroup2User();
                    break;
                case "RMADetail":
                    this.CheckRMADetail();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 产品
        /// </summary>
        private void CheckITEM()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = 0; i < count; i++)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string modelCode = row["MODELCODE"].ToString();
                string itemCode = row["ITEMCODE"].ToString();
                string itemName = row["ITEMNAME"].ToString();
                string itemType = row["ITEMTYPE"].ToString();
                string itemUom = row["ITEMUOM"].ToString();
                string itemDesc = row["ITEMDESC"].ToString();
                string itemBurnInQty = row["ITEMBURNINQTY"].ToString();
                string electricCurrentMinValue = row["ElectricCurrentMinValue"].ToString();
                string electricCurrentMaxValue = row["ElectricCurrentMaxValue"].ToString();
                string errorMessage = string.Empty;
                if (modelCode == string.Empty || itemCode == string.Empty || itemName == string.Empty || itemType == string.Empty
                    || itemUom == string.Empty || itemDesc == string.Empty || itemBurnInQty == string.Empty
                    || electricCurrentMinValue == string.Empty || electricCurrentMaxValue == string.Empty)
                {
                    //row[gridHelper.CheckColumnKey] = false;
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRow.ContainsKey(modelCode))
                {
                    //row[gridHelper.CheckColumnKey] = bool.Parse(checkedRow[modelCode].ToString());
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRow[modelCode].ToString());
                    if (!bool.Parse(checkedRow[modelCode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                        new SQLParamCondition(@"select modelcode from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and modelcode=$MODELCODE",
                        new SQLParameter[] { new SQLParameter("$MODELCODE", typeof(string), modelCode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRow.Add(modelCode, true);
                        //row[gridHelper.CheckColumnKey] = true;
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                    }
                    else
                    {
                        checkedRow.Add(modelCode, false);
                        //gridRow.Items[0].Value = false;
                        //row[gridHelper.CheckColumnKey] = false;
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }
                }
                //row["ImportResult"] = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                ////gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                //this.ImportDatatable.Rows.Remove(row);
                //this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckModel()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string modelCode = row["MODELCODE"].ToString();
                string modelDesc = row["MODELDESC"].ToString();
                string isInv = row["ISINV"].ToString();
                string errorMessage = "";
                if (modelCode == string.Empty || modelDesc == string.Empty || isInv == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }


        private void CheckBARCODERULE()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string modelCode = row["MODELCODE"].ToString();
                string aModelCode = row["AMODELCODE"].ToString();
                string aDesc = row["ADESC"].ToString();
                string errorMessage = "";
                if (modelCode == string.Empty || aModelCode == string.Empty || aDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }
                if (checkedRow.ContainsKey(modelCode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRow[modelCode].ToString());
                    if (!bool.Parse(checkedRow[modelCode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }

                    if (row["AMODELCODE"].ToString().Length > 2)
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_AModelCode_Must_Be_2Bit") + ";";
                    }

                    if (row["ADESC"].ToString().Length > 8)
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Adesc_Must_Be_8Bit") + ";";
                    }

                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                        new SQLParamCondition(@"select modelcode from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and modelcode=$MODELCODE",
                        new SQLParameter[] { new SQLParameter("$MODELCODE", typeof(string), modelCode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRow.Add(modelCode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;

                        if (row["AMODELCODE"].ToString().Length != 2)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_AModelCode_Must_Be_2Bit") + ";";
                        }

                        if (row["ADESC"].ToString().Length != 8)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Adesc_Must_Be_8Bit") + ";";
                        }
                    }
                    else
                    {
                        checkedRow.Add(modelCode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckErrorCodeA()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string eCode = row["ECODE"].ToString();
                string eDesc = row["ECDESC"].ToString();

                if (eCode == string.Empty || eDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "$Error_Input_Empty;";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                }
            }
        }

        private void CheckErrorCodeGroupA()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string ecgCode = row["ECGCODE"].ToString();
                string ecgDesc = row["ECGDESC"].ToString();

                if (ecgCode == string.Empty || ecgDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "$Error_Input_Empty;";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                }
            }
        }

        private void CheckErrorCause()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string ecsCode = row["ECSCODE"].ToString();
                string ecsDesc = row["ECSDESC"].ToString();

                if (ecsCode == string.Empty || ecsDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "$Error_Input_Empty;";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                }
            }
        }

        private void CheckDuty()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string dutyCode = row["DUTYCODE"].ToString();
                string dutyDesc = row["DUTYDESC"].ToString();

                if (dutyCode == string.Empty || dutyDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "$Error_Input_Empty;";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                }
            }
        }

        private void CheckSolution()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string solCode = row["SOLCODE"].ToString();
                string solDesc = row["SOLDESC"].ToString();

                if (solCode == string.Empty || solDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "$Error_Input_Empty;";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                }
            }
        }

        private void CheckOQCCheckList()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string ckItemCode = row["CKITEMCODE"].ToString();
                string ckItemDesc = row["CKITEMDESC"].ToString();

                if (ckItemCode == string.Empty || ckItemDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    gridRow.Items.FindItemByKey("ImportResult").Value = "$Error_Input_Empty;";
                    //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                    this.ImportDatatable.Rows.Remove(row);
                    this.ImportGridRow.Remove(gridRow);
                }
            }
        }

        private void CheckUser()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string userdepart = row["USERDEPART"].ToString();
                string userCode = row["USERCODE"].ToString();
                string userName = row["USERNAME"].ToString();
                string userTel = row["USERTEL"].ToString();
                string userEmail = row["USEREMAIL"].ToString();
                string errorMessage = "";
                if (userdepart == string.Empty || userCode == string.Empty || userName == string.Empty || userTel == string.Empty ||
                userEmail == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRow.ContainsKey(userdepart))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRow[userdepart].ToString());
                    if (!bool.Parse(checkedRow[userdepart].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_UserDepart_Not_Exist") + ";";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Parameter),
                        new SQLParamCondition(@"select paramcode
							from tblsysparam
							where paramgroupcode =(
								select paramgroupcode
								from Tblsysparamgroup where paramgroupcode ='DEPARTMENT')
								and paramcode = $PARAMCODE  ",
                        new SQLParameter[] { new SQLParameter("$PARAMCODE", typeof(string), userdepart.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRow.Add(userdepart, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                    }
                    else
                    {
                        checkedRow.Add(userdepart, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_UserDepart_Not_Exist") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckItem2Route()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowItem = new Hashtable();
            Hashtable checkedRowRoute = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string itemcode = row["ITEMCODE"].ToString();
                string routecode = row["ROUTECODE"].ToString();
                string errorMessage = "";
                if (itemcode == string.Empty || routecode == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRowItem.ContainsKey(itemcode))
                {

                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowItem[itemcode].ToString());
                    if (!bool.Parse(checkedRowItem[itemcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";
                    }
                    else
                    {
                        if (checkedRowRoute.ContainsKey(routecode))
                        {

                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowRoute[routecode].ToString());
                            if (!bool.Parse(checkedRowRoute[routecode].ToString()))
                            {
                                errorMessage += this.languageComponent1.GetString("$Error_RouteCode_Not_Exist") + ";";
                            }
                        }
                        else
                        {

                            object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                                new SQLParamCondition(@"select routecode from tblroute where routecode=$ROUTECODE",
                                new SQLParameter[] { new SQLParameter("$ROUTECODE", typeof(string), routecode.ToUpper()) }));
                            if (objs2 != null)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                checkedRowRoute.Add(routecode, true);
                                continue;
                            }
                            else
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                checkedRowRoute.Add(routecode, false);
                                errorMessage += this.languageComponent1.GetString("$Error_RouteCode_Not_Exist") + ";";
                            }
                        }
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item),
                        new SQLParamCondition(@"select itemcode	from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and itemcode=$ITEMCODE",
                        new SQLParameter[] { new SQLParameter("$ITEMCODE", typeof(string), itemcode.ToUpper()) }));


                    if (objs != null)
                    {
                        checkedRowItem.Add(itemcode, true);

                        if (checkedRowRoute.ContainsKey(routecode))
                        {

                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowRoute[routecode].ToString());
                            if (!bool.Parse(checkedRowRoute[routecode].ToString()))
                            {
                                errorMessage += this.languageComponent1.GetString("$Error_RouteCode_Not_Exist") + ";";
                            }
                        }
                        else
                        {

                            object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                                new SQLParamCondition(@"select routecode from tblroute where routecode=$ROUTECODE",
                                new SQLParameter[] { new SQLParameter("$ROUTECODE", typeof(string), routecode.ToUpper()) }));
                            if (objs2 != null)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                checkedRowRoute.Add(routecode, true);
                                continue;
                            }
                            else
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                checkedRowRoute.Add(routecode, false);
                                errorMessage += this.languageComponent1.GetString("$Error_RouteCode_Not_Exist") + ";";
                            }
                        }

                    }
                    else
                    {
                        checkedRowItem.Add(itemcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }
        private void CheckItem2SPCTest()
        {
            /* 特别处理 */

            int count = this.ImportDatatable.Rows.Count;
            /* 特别处理 
             * 将产品代码和测试项 toUpper*/
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                row["ITEMCODE"] = row["ITEMCODE"].ToString().ToUpper();
                row["TESTNAME"] = row["TESTNAME"].ToString().ToUpper();
            }

            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string itemcode = row["ITEMCODE"].ToString();
                string testname = row["TESTNAME"].ToString();
                string seq = row["SEQ"].ToString();
                string upOnly = row["UPONLY"].ToString();
                string lowOnly = row["LOWONLY"].ToString();
                string autoCl = row["AUTOCL"].ToString();
                string ucl = row["UCL"].ToString();
                string lcl = row["LCL"].ToString();
                string errorMessage = "";

                if (itemcode == string.Empty || testname == string.Empty || seq == string.Empty || upOnly == string.Empty
                    || lowOnly == string.Empty || autoCl == string.Empty || ucl == string.Empty || lcl == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRow.ContainsKey(itemcode))
                {

                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRow[itemcode].ToString());
                    if (!bool.Parse(checkedRow[itemcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";

                    }
                    else
                    {
                        object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item2SPCTest),
                            new SQLParamCondition("select itemcode,testname from tblitem2spctest where itemcode=$ITEMCODE and upper(testname)=$TESTNAME",
                            new SQLParameter[]{
												  new SQLParameter("$ITEMCODE",typeof(string),itemcode.ToUpper()),
												  new SQLParameter("$TESTNAME",typeof(string),testname.ToUpper())}));

                        if (objs2 != null)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Exist_Item2SpcTest") + ";";

                        }
                        else
                        {
                            int count2 = this.ImportDatatable.Select(string.Format(" ITEMCODE = '{0}' and TESTNAME = '{1}'", itemcode, testname)).Length;
                            if (count2 > 1)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_Same_SpcTest_In_Item") + ";";
                            }
                        }

                        object[] objs3 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item2SPCTest),
                            new SQLParamCondition("select itemcode,seq from tblitem2spctest where itemcode=$ITEMCODE and seq=$SEQ",
                            new SQLParameter[]{
												  new SQLParameter("$ITEMCODE",typeof(string),itemcode.ToUpper()),
												  new SQLParameter("$SEQ",typeof(string),seq)}));
                        if (objs3 != null)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Exist_Seq") + ";";
                        }
                        else
                        {
                            int count2 = this.ImportDatatable.Select(string.Format(" ITEMCODE = '{0}' and SEQ = '{1}'", itemcode, seq)).Length;
                            if (count2 > 1)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_Same_Seq_In_Itemcode") + ";";
                            }
                        }

                        try
                        {
                            int.Parse(seq);
                        }
                        catch
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Seq_Must_Be_NatureNumber") + ";";
                        }

                        Hashtable CLSLcheck = new Hashtable(4);
                        if (string.Compare(row["AUTOCL"].ToString(), "Y", true) == 0)
                        {
                            if (row["UCL"].ToString() != "0" || row["LCL"].ToString() != "0")
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_UCL/LCL_Must_Be_0") + ";";
                            }
                        }
                        else
                        {
                            CLSLcheck.Add("UCL", row["UCL"].ToString());
                            CLSLcheck.Add("LCL", row["LCL"].ToString());
                        }

                        if (string.Compare(row["LOWONLY"].ToString(), "Y", true) == 0
                            && string.Compare(row["LOWONLY"].ToString(), row["UPONLY"].ToString(), true) == 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += "上下限规则不能同时存在;";
                        }

                        if (string.Compare(row["LOWONLY"].ToString(), "Y", true) == 0)
                        {
                            if (row["USL"].ToString() != "0")
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_USL_Must_Be_0") + ";";
                            }
                        }
                        else
                        {
                            CLSLcheck.Add("USL", row["USL"].ToString());
                        }

                        if (string.Compare(row["UPONLY"].ToString(), "Y", true) == 0)
                        {
                            if (row["LSL"].ToString() != "0")
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_LSL_Must_Be_0") + ";";
                            }
                        }
                        else
                        {
                            CLSLcheck.Add("LSL", row["LSL"].ToString());
                        }

                        string fields = string.Empty;
                        foreach (DictionaryEntry dic2 in CLSLcheck)
                        {
                            decimal impValue = 0M;
                            try
                            {
                                impValue = Convert.ToDecimal(dic2.Value.ToString());
                            }
                            catch
                            {
                                if (impValue == 0M)
                                {
                                    fields += dic2.Key.ToString() + "/";
                                }
                            }
                        }

                        if (fields.Length != 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += fields.TrimEnd('/') + "应该为不为0的数值;";
                        }
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item),
                        new SQLParamCondition(@"select itemcode from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and itemcode=$ITEMCODE",
                        new SQLParameter[] { new SQLParameter("$ITEMCODE", typeof(string), itemcode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRow.Add(itemcode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;

                        object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item2SPCTest),
                            new SQLParamCondition("select itemcode,testname from tblitem2spctest where itemcode=$ITEMCODE and upper(testname)=$TESTNAME",
                            new SQLParameter[]{
												  new SQLParameter("$ITEMCODE",typeof(string),itemcode.ToUpper()),
												  new SQLParameter("$TESTNAME",typeof(string),testname.ToUpper())}));

                        if (objs2 != null)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Exist_Item2SpcTest") + ";";

                        }
                        else
                        {
                            int count2 = this.ImportDatatable.Select(string.Format(" ITEMCODE = '{0}' and TESTNAME = '{1}'", itemcode, testname)).Length;
                            if (count2 > 1)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_Same_SpcTest_In_Item") + ";";

                            }
                        }

                        object[] objs3 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item2SPCTest),
                            new SQLParamCondition("select itemcode,seq from tblitem2spctest where itemcode=$ITEMCODE and seq=$SEQ",
                            new SQLParameter[]{
												  new SQLParameter("$ITEMCODE",typeof(string),itemcode.ToUpper()),
												  new SQLParameter("$SEQ",typeof(string),seq)}));
                        if (objs3 != null)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Exist_Seq") + ";";

                        }
                        else
                        {
                            int count2 = this.ImportDatatable.Select(string.Format(" ITEMCODE = '{0}' and SEQ = '{1}'", itemcode, seq)).Length;
                            if (count2 > 1)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_Same_Seq_In_Itemcode") + ";";

                            }
                        }

                        try
                        {
                            int.Parse(seq);
                        }
                        catch
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Seq_Must_Be_NatureNumber") + ";";

                        }

                        Hashtable CLSLcheck = new Hashtable(4);
                        if (string.Compare(row["AUTOCL"].ToString(), "Y", true) == 0)
                        {
                            if (row["UCL"].ToString() != "0" || row["LCL"].ToString() != "0")
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_UCL/LCL_Must_Be_0") + ";";

                            }
                        }
                        else
                        {
                            CLSLcheck.Add("UCL", row["UCL"].ToString());
                            CLSLcheck.Add("LCL", row["LCL"].ToString());
                        }

                        if (string.Compare(row["LOWONLY"].ToString(), "Y", true) == 0)
                        {
                            if (row["USL"].ToString() != "0")
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_USL_Must_Be_0") + ";";

                            }

                        }
                        else
                        {
                            CLSLcheck.Add("USL", row["USL"].ToString());
                        }

                        if (string.Compare(row["UPONLY"].ToString(), "Y", true) == 0)
                        {
                            if (row["LSL"].ToString() != "0")
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_LSL_Must_Be_0") + ";";

                            }
                        }
                        else
                        {
                            CLSLcheck.Add("LSL", row["LSL"].ToString());
                        }

                        string fields = string.Empty;
                        foreach (DictionaryEntry dic in CLSLcheck)
                        {
                            decimal impValue = 0M;
                            try
                            {
                                impValue = Convert.ToDecimal(dic.Value.ToString());
                            }
                            catch
                            {
                                if (impValue == 0M)
                                {
                                    fields += dic.Key.ToString() + "/";
                                }
                            }
                        }

                        if (fields.Length != 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += fields.TrimEnd('/') + this.languageComponent1.GetString("$Error_Must_Be_Number") + ";";

                        }
                    }
                    else
                    {
                        checkedRow.Add(itemcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }
        private void CheckItemLocation()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowItem = new Hashtable();
            Hashtable checkedRowRoute = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string itemcode = row["ITEMCODE"].ToString();
                string ab = row["AB"].ToString();
                string qty = row["Qty"].ToString();
                string errorMessage = "";
                if (itemcode == string.Empty || ab == string.Empty || qty == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRowItem.ContainsKey(itemcode))
                {

                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowItem[itemcode].ToString());
                    if (!bool.Parse(checkedRowItem[itemcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";

                    }
                    else
                    {
                        //string ab = row["AB"].ToString();
                        if (string.Compare(ab, "A", true) != 0
                            && string.Compare(ab, "B", true) != 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Must_Be_AorB") + ";";
                        }

                        int qtyINT = -1;

                        try
                        {
                            qtyINT = Convert.ToInt32(row["QTY"].ToString());
                        }
                        catch
                        {
                        }
                        if (qtyINT < 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_qtyINT_Must_Be_>=0 Integer") + ";";
                        }

                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item),
                        new SQLParamCondition(@"select itemcode from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and itemcode=$ITEMCODE",
                        new SQLParameter[] { new SQLParameter("$ITEMCODE", typeof(string), itemcode.ToUpper()) }));


                    if (objs != null)
                    {
                        checkedRowItem.Add(itemcode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;

                        //string ab = row["AB"].ToString();
                        if (string.Compare(ab, "A", true) != 0
                            && string.Compare(ab, "B", true) != 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Must_Be_AorB") + ";";
                        }

                        int qtyINT = -1;

                        try
                        {
                            qtyINT = Convert.ToInt32(row["QTY"].ToString());
                        }
                        catch
                        {
                        }
                        if (qtyINT < 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_qtyINT_Must_Be_>=0 Integer") + ";";
                        }
                    }
                    else
                    {
                        checkedRowItem.Add(itemcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";

                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }
        private void CheckItem2SPCTable()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string itemcode = row["ITEMCODE"].ToString();
                string spcTblName = row["SPCTBLNAME"].ToString();
                string startDate = row["STARTDATE"].ToString();
                string endDate = row["ENDDATE"].ToString();
                string spcDesc = row["SPCDESC"].ToString();
                string errorMessage = "";
                if (itemcode == string.Empty || spcTblName == string.Empty || startDate == string.Empty || endDate == string.Empty ||
                    spcDesc == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRow.ContainsKey(itemcode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRow[itemcode].ToString());
                    if (!bool.Parse(checkedRow[itemcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";
                    }
                    else
                    {
                        DateTime dtB = DateTime.Now;
                        if (row["STARTDATE"].ToString().Length != 0)
                        {

                            try
                            {
                                dtB = DateTime.Parse(row["STARTDATE"].ToString());
                                row["STARTDATE"] = FormatHelper.TODateInt(dtB);
                            }
                            catch
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_StartDate_Wrong_Format") + ";";
                            }
                        }

                        DateTime dtE = DateTime.Now;
                        if (row["ENDDATE"].ToString().Length != 0)
                        {

                            try
                            {
                                dtE = DateTime.Parse(row["ENDDATE"].ToString());
                                row["ENDDATE"] = FormatHelper.TODateInt(dtE);
                            }
                            catch
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_EndDate_Wrong_Format") + ";";
                            }
                        }

                        if (DateTime.Compare(dtB, dtE) > 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_StartDate_Must_Be_Earlier_Than_EndDate") + ";";
                        }
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item),
                        new SQLParamCondition(@"select itemcode from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and itemcode=$ITEMCODE",
                        new SQLParameter[] { new SQLParameter("$ITEMCODE", typeof(string), itemcode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRow.Add(itemcode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;

                        DateTime dtB = DateTime.Now;
                        if (row["STARTDATE"].ToString().Length != 0)
                        {

                            try
                            {
                                dtB = DateTime.Parse(row["STARTDATE"].ToString());
                                row["STARTDATE"] = FormatHelper.TODateInt(dtB);
                            }
                            catch
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_StartDate_Wrong_Format") + ";";
                            }
                        }

                        DateTime dtE = DateTime.Now;
                        if (row["ENDDATE"].ToString().Length != 0)
                        {

                            try
                            {
                                dtE = DateTime.Parse(row["ENDDATE"].ToString());
                                row["ENDDATE"] = FormatHelper.TODateInt(dtE);
                            }
                            catch
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_EndDate_Wrong_Format") + ";";
                            }
                        }

                        if (DateTime.Compare(dtB, dtE) > 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_StartDate_Must_Be_Earlier_Than_EndDate") + ";";
                        }
                    }
                    else
                    {
                        checkedRow.Add(itemcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_Not_Exist") + ";";
                    }
                }


                string startdate = row["STARTDATE"].ToString();
                string enddate = row["ENDDATE"].ToString();

                object[] objItem2SPCTests = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item2SPCTest),
                                new SQLParamCondition(@"select itemcode from tblitem2spctbl where itemcode = $ITEMCODE
										and enddate >= $STARTDATE and startdate <= $ENDDATE",
                                new SQLParameter[]{new SQLParameter("$ITEMCODE",typeof(string),itemcode),
									new SQLParameter("$STARTDATE",typeof(string),startdate),
									new SQLParameter("$ENDDATE",typeof(string),enddate)}));
                if (objItem2SPCTests != null)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Conflict_TimeSpan") + ";";
                }
                else
                {
                    int count3 = this.ImportDatatable.Select(string.Format(" ITEMCODE='{0}' and ENDDATE >= {1} and STARTDATE <= {2} ", itemcode, startdate, enddate)).Length;
                    if (count3 > 1)
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Conflict_TimeSpan_To_LeadIn") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckModel2ErrorCodeGroup()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowModel = new Hashtable();
            Hashtable checkedRowECG = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string modelcode = row["MODELCODE"].ToString();
                string ecgcode = row["ECGCODE"].ToString();
                string errorMessage = "";
                if (modelcode == string.Empty || ecgcode == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRowModel.ContainsKey(modelcode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowModel[modelcode].ToString());
                    if (!bool.Parse(checkedRowModel[modelcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }
                    else
                    {
                        if (checkedRowECG.ContainsKey(ecgcode))
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowECG[ecgcode].ToString());
                            if (!bool.Parse(checkedRowECG[ecgcode].ToString()))
                            {
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCodeGroup_Not_Exist") + ";";
                            }
                        }
                        else
                        {
                            object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA),
                                new SQLParamCondition(@"select ecgcode from tblecg where ecgcode=$ECGCODE",
                                new SQLParameter[] { new SQLParameter("$ECGCODE", typeof(string), ecgcode.ToUpper()) }));

                            if (objs2 != null)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                checkedRowECG.Add(ecgcode, true);
                                continue;
                            }
                            else
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                checkedRowECG.Add(ecgcode, false);
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCodeGroup_Not_Exist") + ";";
                            }
                        }
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                        new SQLParamCondition(@"select modelcode from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and modelcode=$MODELCODE",
                        new SQLParameter[] { new SQLParameter("$MODELCODE", typeof(string), modelcode.ToUpper()) }));


                    if (objs != null)
                    {
                        checkedRowModel.Add(modelcode, true);

                        if (checkedRowECG.ContainsKey(ecgcode))
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowECG[ecgcode].ToString());
                            if (!bool.Parse(checkedRowECG[ecgcode].ToString()))
                            {
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCodeGroup_Not_Exist") + ";";
                            }

                        }
                        else
                        {
                            object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA),
                                new SQLParamCondition(@"select ecgcode from tblecg where ecgcode=$ECGCODE",
                                new SQLParameter[] { new SQLParameter("$ECGCODE", typeof(string), ecgcode.ToUpper()) }));

                            if (objs2 != null)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                checkedRowECG.Add(ecgcode, true);
                                continue;
                            }
                            else
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                checkedRowECG.Add(ecgcode, false);
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCodeGroup_Not_Exist") + ";";
                            }
                        }
                    }
                    else
                    {
                        checkedRowModel.Add(modelcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckModel2Solution()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowModel = new Hashtable();
            Hashtable checkedRowSol = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string modelcode = row["MODELCODE"].ToString();
                string solcode = row["SOLCODE"].ToString();
                string errorMessage = "";
                if (modelcode == string.Empty || solcode == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRowModel.ContainsKey(modelcode))
                {
                    foreach (DictionaryEntry dic in checkedRowModel)
                    {
                        if (string.Compare(dic.Key.ToString(), modelcode, true) == 0)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(dic.Value.ToString());
                            if (!bool.Parse(dic.Value.ToString()))
                            {
                                errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                            }
                            else
                            {
                                if (checkedRowSol.ContainsKey(solcode))
                                {
                                    foreach (DictionaryEntry dic2 in checkedRowSol)
                                    {
                                        if (string.Compare(dic2.Key.ToString(), solcode, true) == 0)
                                        {
                                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(dic2.Value.ToString());
                                            if (!bool.Parse(dic2.Value.ToString()))
                                            {
                                                errorMessage += this.languageComponent1.GetString("$Error_SolutionCode_Not_Exist") + ";";
                                            }
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                                        new SQLParamCondition(@"select solcode from tblsolution where solcode=$SOLCODE",
                                        new SQLParameter[] { new SQLParameter("$solcode", typeof(string), solcode.ToUpper()) }));

                                    if (objs2 != null)
                                    {
                                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                        checkedRowSol.Add(solcode, true);
                                        continue;
                                    }
                                    else
                                    {
                                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                        checkedRowSol.Add(solcode, false);
                                        errorMessage += this.languageComponent1.GetString("$Error_SolutionCode_Not_Exist") + ";";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                        new SQLParamCondition(@"select modelcode from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and modelcode=$MODELCODE",
                        new SQLParameter[] { new SQLParameter("$MODELCODE", typeof(string), modelcode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRowModel.Add(modelcode, true);

                        if (checkedRowSol.ContainsKey(solcode))
                        {
                            foreach (DictionaryEntry dic2 in checkedRowSol)
                            {
                                if (string.Compare(dic2.Key.ToString(), solcode, true) == 0)
                                {
                                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(dic2.Value.ToString());
                                    if (!bool.Parse(dic2.Value.ToString()))
                                    {
                                        errorMessage += this.languageComponent1.GetString("$Error_SolutionCode_Not_Exist") + ";";
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {
                            object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                                new SQLParamCondition(@"select solcode from tblsolution where solcode=$SOLCODE",
                                new SQLParameter[] { new SQLParameter("$solcode", typeof(string), solcode.ToUpper()) }));

                            if (objs2 != null)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                checkedRowSol.Add(solcode, true);
                                continue;
                            }
                            else
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                checkedRowSol.Add(solcode, false);
                                errorMessage += this.languageComponent1.GetString("$Error_SolutionCode_Not_Exist") + ";";
                            }
                        }

                    }
                    else
                    {
                        checkedRowModel.Add(modelcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckModel2ErrorCause()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowModel = new Hashtable();
            Hashtable checkedRowECS = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string modelcode = row["MODELCODE"].ToString();
                string ecscode = row["ECSCODE"].ToString();
                string errorMessage = "";
                if (modelcode == string.Empty || ecscode == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRowModel.ContainsKey(modelcode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowModel[modelcode].ToString());
                    if (!bool.Parse(checkedRowModel[modelcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";
                    }
                    else
                    {
                        if (checkedRowECS.ContainsKey(ecscode))
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowECS[ecscode].ToString());
                            if (!bool.Parse(checkedRowECS[ecscode].ToString()))
                            {
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCause_Not_Exist") + ";";
                            }
                        }
                        else
                        {
                            object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                                new SQLParamCondition(@"select ecscode from tblecs where ecscode=$ECSCODE",
                                new SQLParameter[] { new SQLParameter("$ECSCODE", typeof(string), ecscode.ToUpper()) }));

                            if (objs2 != null)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                checkedRowECS.Add(ecscode, true);
                                continue;
                            }
                            else
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                checkedRowECS.Add(ecscode, false);
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCause_Not_Exist") + ";";
                            }
                        }

                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                        new SQLParamCondition(@"select modelcode from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and modelcode=$MODELCODE",
                        new SQLParameter[] { new SQLParameter("$MODELCODE", typeof(string), modelcode.ToUpper()) }));


                    if (objs != null)
                    {
                        checkedRowModel.Add(modelcode, true);

                        if (checkedRowECS.ContainsKey(ecscode))
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowECS[ecscode].ToString());
                            if (!bool.Parse(checkedRowECS[ecscode].ToString()))
                            {
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCause_Not_Exist") + ";";
                            }

                        }
                        else
                        {
                            object[] objs2 = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                                new SQLParamCondition(@"select ecscode from tblecs where ecscode=$ECSCODE",
                                new SQLParameter[] { new SQLParameter("$ECSCODE", typeof(string), ecscode.ToUpper()) }));

                            if (objs2 != null)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                checkedRowECS.Add(ecscode, true);
                                continue;
                            }
                            else
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                checkedRowECS.Add(ecscode, false);
                                errorMessage += this.languageComponent1.GetString("$Error_ErrorCause_Not_Exist") + ";";
                            }
                        }
                    }
                    else
                    {
                        checkedRowModel.Add(modelcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ModelCode_Not_Exist") + ";";

                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckOPBOM()
        {
            /* step1 check itemcode */
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowItem = new Hashtable();
            Hashtable checkedRowRoute = new Hashtable();
            Hashtable checkedRowOP = new Hashtable();
            Hashtable checkedRowCItemCode = new Hashtable();

            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string itemcode = row["ITEMCODE"].ToString();
                string obCode = row["OBCODE"].ToString();
                string opCode = row["OPCODE"].ToString();
                string obItemCode = row["OBITEMCODE"].ToString();
                string obItemName = row["OBITEMNAME"].ToString();
                string obsItemCode = row["OBSITEMCODE"].ToString();
                string obItemQty = row["OBITEMQTY"].ToString();
                string obItemUom = row["OBITEMUOM"].ToString();
                string obItemEffDate = row["OBITEMEFFDATE"].ToString();
                string obItemInvDate = row["OBITEMINVDATE"].ToString();
                string obItemEcn = row["OBITEMECN"].ToString();
                string obItemConType = row["OBITEMCONTYPE"].ToString();
                string actionType = row["ActionType"].ToString();
                string errorMessage = "";
                if (itemcode == string.Empty || obCode == string.Empty || opCode == string.Empty || obItemCode == string.Empty
                    || obItemName == string.Empty || obsItemCode == string.Empty || obItemQty == string.Empty || obItemUom == string.Empty
                    || itemcode == string.Empty || itemcode == string.Empty || itemcode == string.Empty || itemcode == string.Empty
                    || itemcode == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (checkedRowItem.ContainsKey(itemcode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowItem[itemcode].ToString());
                    if (!bool.Parse(checkedRowItem[itemcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_or_Sbom_Not_Exist") + ";";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item),
                        new SQLParamCondition(@"select itemcode from TBLITEM where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and itemcode in (select itemcode from tblsbom where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and itemcode=$ITEMCODE ) ",
                        new SQLParameter[] { new SQLParameter("$ITEMCODE", typeof(string), itemcode.ToUpper()) }));
                    if (objs != null)
                    {
                        checkedRowItem.Add(itemcode, true);
                    }
                    else
                    {
                        checkedRowItem.Add(itemcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_ItemCode_or_Sbom_Not_Exist") + ";";
                    }

                }


                /* step2 check routecode */

                string routecode = row["OBCODE"].ToString();
                string key = string.Format("{0}_{1}", row["ITEMCODE"].ToString().ToUpper(), row["OBCODE"].ToString().ToUpper());
                if (checkedRowRoute.ContainsKey(key))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowRoute[key].ToString());
                    if (!bool.Parse(checkedRowRoute[key].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_Route_Not_Belong_To_Item") + ";";

                    }

                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item2Route),
                        new SQLParamCondition(@"select routecode from tblitem2route 
								where itemcode=$ITEMCODE and routecode = $ROUTECODE and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID,
                        new SQLParameter[]{new SQLParameter("$ITEMCODE",typeof(string),row["ITEMCODE"].ToString().ToUpper()),
											  new SQLParameter("$ROUTECODE",typeof(string),routecode.ToString())}));
                    if (objs != null)
                    {
                        checkedRowRoute.Add(key, true);
                    }
                    else
                    {
                        checkedRowRoute.Add(key, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Route_Not_Belong_To_Item") + ";";

                    }

                }

                /* step3 check opcode */

                string opcode = row["OPCODE"].ToString();
                key = string.Format("{0}_{1}_{2}",
                    row["ITEMCODE"].ToString().ToUpper(),
                    row["OBCODE"].ToString().ToUpper(),
                    row["OPCODE"].ToString().ToUpper());
                if (checkedRowOP.ContainsKey(key))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowOP[key].ToString());
                    if (!bool.Parse(checkedRowOP[key].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_Wrong_OP") + ";";
                    }
                    else
                    {
                        /// 上料0 下料1
                        if (string.Compare(row["ActionType"].ToString(), "0", true) == 0)
                        {
                            if (!CheckComponenetLoadingOperation(
                                row["ITEMCODE"].ToString().ToUpper(),
                                row["OBCODE"].ToString().ToUpper(),
                                row["OPCODE"].ToString().ToUpper()))
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_OP_Cannot_Be_0") + ";";
                            }
                        }
                        else
                        {
                            if (!CheckComponenetDownOperation(
                                row["ITEMCODE"].ToString().ToUpper(),
                                row["OBCODE"].ToString().ToUpper(),
                                row["OPCODE"].ToString().ToUpper()))
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_OP_Cannot_Be_1") + ";";
                            }
                        }
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.ItemRoute2OP),
                        new SQLParamCondition(@"select opcode from tblitemroute2op
							where itemcode=$ITEMCODE and routecode = $ROUTECODE and opcode=$OPCODE and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID,
                        new SQLParameter[]{new SQLParameter("$ITEMCODE",typeof(string),row["ITEMCODE"].ToString().ToUpper()),
											  new SQLParameter("$ROUTECODE",typeof(string),row["OBCODE"].ToString()),
											  new SQLParameter("$OPCODE",typeof(string),row["OPCODE"].ToString())}));
                    if (objs != null)
                    {
                        checkedRowOP.Add(key, true);

                        /// 上料0 下料1
                        if (string.Compare(row["ActionType"].ToString(), "0", true) == 0)
                        {
                            if (!CheckComponenetLoadingOperation(
                                row["ITEMCODE"].ToString().ToUpper(),
                                row["OBCODE"].ToString().ToUpper(),
                                row["OPCODE"].ToString().ToUpper()))
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_OP_Cannot_Be_0") + ";";
                            }
                        }
                        else
                        {
                            if (!CheckComponenetDownOperation(
                                row["ITEMCODE"].ToString().ToUpper(),
                                row["OBCODE"].ToString().ToUpper(),
                                row["OPCODE"].ToString().ToUpper()))
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += this.languageComponent1.GetString("$Error_OP_Cannot_Be_1") + ";";
                            }
                        }
                    }
                    else
                    {
                        checkedRowOP.Add(key, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Wrong_OP") + ";";
                    }
                }


                /* step4 check citemcode */

                key = string.Format("{0}_{1}_{2}_{3}_{4}",
                     row["ITEMCODE"].ToString().ToUpper(),
                     row["OBCODE"].ToString().ToUpper(),
                     row["OPCODE"].ToString().ToUpper(),
                     row["OBITEMCODE"].ToString().ToUpper(),
                     row["ActionType"].ToString().ToUpper());
                if (checkedRowCItemCode.ContainsKey(key))
                {
                    if (bool.Parse(checkedRowCItemCode[key].ToString()))
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Conflict_OBItemCode_To_LeadIn") + ";";
                    }
                    else
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Exist_OBItemCode") + ";";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.OPBOMDetail),
                        new SQLParamCondition(@"select obitemcode from tblopbomdetail
						where itemcode=$ITEMCODE and obcode = $ROUTECODE and opcode=$OPCODE and obitemcode=$OBITEMCODE and actiontype=$ACTIONTYPE  and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID,
                        new SQLParameter[]{new SQLParameter("$ITEMCODE",typeof(string),row["ITEMCODE"].ToString().ToUpper()),
										new SQLParameter("$ROUTECODE",typeof(string),row["OBCODE"].ToString().ToUpper()),
										new SQLParameter("$OPCODE",typeof(string),row["OPCODE"].ToString().ToUpper()),
										new SQLParameter("$OBITEMCODE",typeof(string),row["OBITEMCODE"].ToString().ToUpper()),
										new SQLParameter("$ACTIONTYPE",typeof(int),row["ACTIONTYPE"].ToString().ToUpper())}));
                    if (objs == null)
                    {
                        checkedRowCItemCode.Add(key, true);
                    }
                    else
                    {
                        checkedRowCItemCode.Add(key, false);

                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Exist_OBItemCode") + ";";
                    }

                }

                /* step5 check other */

                string effDate = row["OBITEMEFFDATE"].ToString();
                string invDate = row["OBITEMINVDATE"].ToString();
                string obitemqty = row["OBITEMQTY"].ToString();

                DateTime dtB = DateTime.Now;
                try
                {
                    dtB = DateTime.Parse(effDate);
                    row["OBITEMEFFDATE"] = FormatHelper.TODateInt(dtB);
                }
                catch
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_EFFDate_Wrong_Format") + ";";
                }

                DateTime dtE = DateTime.Now;
                try
                {
                    dtE = DateTime.Parse(invDate);
                    row["OBITEMINVDATE"] = FormatHelper.TODateInt(dtE);
                }
                catch
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_INVDate_Wrong_Format") + ";";
                }

                if (DateTime.Compare(dtB, dtE) > 0)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_EFFDate_Must_Be_Earlier_Than_INVDate") + ";";
                }

                decimal obitemqty2 = -1;
                try
                {
                    obitemqty2 = decimal.Parse(obitemqty);
                }
                catch
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_OBItemQty_Must_Be_Integer") + ";";
                }

                if (obitemqty2 < 0)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_OBItemQty_Must_Be_Bigger_Than_0") + ";";
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckCusItemCodeCheckList()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowModel = new Hashtable();
            Hashtable checkedRowItem = new Hashtable();
            Hashtable checkedRowmodel2item = new Hashtable();

            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;

                string itemcode = row["ITEMCODE"].ToString().ToUpper();
                string modelCode = row["MODELCODE"].ToString().ToUpper();
                string cusCode = row["CUSCODE"].ToString().ToUpper();
                string cusModelCode = row["CUSMODELCODE"].ToString().ToUpper();
                string cusItemCode = row["CUSITEMCODE"].ToString().ToUpper();
                string errorMessage = "";
                if (itemcode == string.Empty || modelCode == string.Empty || cusCode == string.Empty || cusModelCode == string.Empty
                    || cusItemCode == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                /* 产品别代码检查 */
                if (checkedRowModel.ContainsKey(modelCode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowModel[modelCode].ToString());
                    if (!bool.Parse(checkedRowModel[modelCode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("Error_ItemCode_Not_Exist") + ";";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model),
                        new SQLParamCondition(@"select modelcode from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and modelcode=$MODELCODE",
                        new SQLParameter[] { new SQLParameter("$MODELCODE", typeof(string), modelCode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRowModel.Add(modelCode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                    }
                    else
                    {
                        checkedRowModel.Add(modelCode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("Error_ItemCode_Not_Exist") + ";";
                    }
                }

                /* 产品检查 */
                if (checkedRowItem.ContainsKey(itemcode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowItem[itemcode].ToString());
                    if (!bool.Parse(checkedRowItem[itemcode].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$CS_PRODUCT_CODE_NOT_EXIST") + ";";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Item),
                        new SQLParamCondition(@"select itemcode from tblitem where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + @" and itemcode=$ITEMCODE",
                        new SQLParameter[] { new SQLParameter("$ITEMCODE", typeof(string), itemcode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRowItem.Add(itemcode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                    }
                    else
                    {
                        checkedRowItem.Add(itemcode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$CS_PRODUCT_CODE_NOT_EXIST") + ";";
                    }
                }

                /* 产品、产品别对应关系 */
                string model2item = modelCode + "_" + itemcode;
                if (checkedRowmodel2item.ContainsKey(model2item))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowmodel2item[model2item].ToString());
                    if (!bool.Parse(checkedRowmodel2item[model2item].ToString()))
                    {
                        errorMessage += this.languageComponent1.GetString("$Error_Model_Item_Not_Match") + ";";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model2Item),
                        new SQLParamCondition("select itemcode from tblmodel2item where itemcode=$ITEMCODE and modelcode=$MODELCODE and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID,
                        new SQLParameter[]{new SQLParameter("$ITEMCODE",typeof(string),itemcode.ToUpper()),
										   new SQLParameter("$MODELCODE",typeof(string),modelCode.ToUpper())}));

                    if (objs != null)
                    {
                        checkedRowmodel2item.Add(model2item, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                    }
                    else
                    {
                        checkedRowmodel2item.Add(model2item, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += this.languageComponent1.GetString("$Error_Model_Item_Not_Match") + ";";
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckUserGroup2User()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRowUser = new Hashtable();
            Hashtable checkedRowUserGroup = new Hashtable();
            for (int i = count - 1; i >= 0; i--)
            {
                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string userCode = row["USERCODE"].ToString();
                string userGroupCode = row["USERGROUPCODE"].ToString();
                string errorMessage = "";
                if (userCode == string.Empty || userGroupCode == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                /* 用户代码检查 */
                if (checkedRowUser.ContainsKey(userCode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowUser[userCode].ToString());
                    if (!bool.Parse(checkedRowUser[userCode].ToString()))
                    {
                        errorMessage += "用户代码不存在;";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.User),
                        new SQLParamCondition("select usercode from tbluser where usercode=$USERCODE",
                        new SQLParameter[] { new SQLParameter("$USERCODE", typeof(string), userCode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRowUser.Add(userCode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                    }
                    else
                    {
                        checkedRowUser.Add(userCode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += "用户代码不存在;";

                    }
                }

                /* 用户组代码检查 */
                if (checkedRowUserGroup.ContainsKey(userGroupCode))
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowUserGroup[userGroupCode].ToString());
                    if (!bool.Parse(checkedRowUserGroup[userGroupCode].ToString()))
                    {
                        errorMessage += "用户组代码不存在;";
                    }
                }
                else
                {
                    object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.BaseSetting.UserGroup),
                        new SQLParamCondition("select usergroupcode from tblusergroup where usergroupcode=$USERGROUPCODE",
                        new SQLParameter[] { new SQLParameter("$USERGROUPCODE", typeof(string), userGroupCode.ToUpper()) }));

                    if (objs != null)
                    {
                        checkedRowUserGroup.Add(userGroupCode, true);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                    }
                    else
                    {
                        checkedRowUserGroup.Add(userGroupCode, false);
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += "用户组代码不存在;";

                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                //gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private void CheckRMADetail()
        {
            int count = this.ImportDatatable.Rows.Count;
            Hashtable checkedRow = new Hashtable();
            Hashtable checkRmaDetailRow = new Hashtable();
            Hashtable checkedRowmodel2item = new Hashtable();
            Hashtable checkErrorCode = new Hashtable();
            Hashtable checkRcard = new Hashtable();
            Hashtable checkMo = new Hashtable();

            Hashtable checkReRcard = new Hashtable();

            for (int i = count - 1; i >= 0; i--)
            {
                bool flag = true;

                DataRow row = this.ImportDatatable.Rows[i];
                GridRecord gridRow = this.ImportGridRow[i] as GridRecord;
                string rmaBillCode = row["RMABILLCODE"].ToString();
                string rcard = row["RCARD"].ToString();
                string itemcode = row["ITEMCODE"].ToString();
                string modelCode = row["MODELCODE"].ToString();

                string maintenanceDate = row["MAINTENANCE"].ToString();//
                string whreceivedateDate = row["WHRECEIVEDATE"].ToString();

                string remoCode = row["REMOCODE"].ToString();
                string handelCode = row["HANDELCODE"].ToString();//
                string errorCode = row["ERRORCODE"].ToString();
                string isInShelfLife = row["ISINSHELFLIFE"].ToString();//
                string errorMessage = "";

                if (rmaBillCode == string.Empty || rcard == string.Empty || itemcode == string.Empty || modelCode == string.Empty
                    || maintenanceDate == string.Empty || handelCode == string.Empty || isInShelfLife == string.Empty)
                {
                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                    errorMessage += this.languageComponent1.GetString("$Error_Input_Empty") + ";";
                }

                if (flag)
                {
                    if (rmaBillCode == null || rmaBillCode.Trim() == "")
                    {
                        errorMessage += "RMA单号不能为空;";
                        flag = false;
                    }
                }

                if (flag)
                {
                    if (rcard == null || rcard.Trim() == "")
                    {
                        errorMessage += this.languageComponent1.GetString("$RMARcard_Cannot_Null") + ";";
                        flag = false;
                    }
                }

                if (flag)
                {
                    if (itemcode == null || itemcode.Trim() == "")
                    {
                        errorMessage += "产品代码不能为空;";
                        flag = false;
                    }
                }


                if (flag)
                {
                    if (handelCode == null || handelCode.Trim() == "")
                    {
                        errorMessage += "RMA处理方式不能为空;";
                        flag = false;
                    }
                }

                if (flag)
                {
                    if (isInShelfLife == null || isInShelfLife.Trim() == "")
                    {
                        errorMessage += "是否在保内不能为空;";
                        flag = false;
                    }
                }

                if (flag)
                {
                    if (checkedRow.ContainsKey(rmaBillCode))
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRow[rmaBillCode].ToString());
                        if (!bool.Parse(checkedRow[rmaBillCode].ToString()))
                        {
                            errorMessage += "RMA单号不为初始化状态;";
                            flag = false;
                            //continue;
                        }

                    }
                    else
                    {
                        object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.RMA.RMABill),
                            new SQLParamCondition(@"select * from TBLRMABILL where 1=1 " + @" and rmaBillCode=$RMABILLCODE",
                            new SQLParameter[] { new SQLParameter("$RMABILLCODE", typeof(string), rmaBillCode.ToUpper()) }));

                        if (objs != null)
                        {
                            if ((objs[0] as Domain.RMA.RMABill).Status != RMABillStatus.Initial)
                            {
                                checkedRow.Add(rmaBillCode, false);
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += "RMA单号不为初始化状态;";
                                flag = false;
                            }
                            else
                            {
                                RMAFacade _RMAFacade = new RMAFacade(this._dataProvider);

                                //Step 1. Delete rmaBill and rmaDetail
                                if (objs[0] != null)
                                {
                                    _RMAFacade.DeleteRMABillNoTrans(objs[0] as Domain.RMA.RMABill);
                                }
                            }
                            //continue;
                        }
                        else
                        {
                            checkedRow.Add(rmaBillCode, true);
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                        }
                    }
                }

                if (flag)
                {
                    if (remoCode != null && remoCode != "")
                    {
                        if (checkMo.ContainsKey(remoCode))
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkMo[remoCode].ToString());
                            if (!bool.Parse(checkMo[remoCode].ToString()))
                            {
                                errorMessage += "返工工单不存在;";
                                flag = false;
                                //continue;
                            }
                        }
                        else
                        {
                            object objDetail = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.MO),
                                new SQLParamCondition(@"select * from TBLMO where 1=1 " + @"  and moCode =$MOCODE",
                                new SQLParameter[] {
                                             new SQLParameter("$MOCODE", typeof(string), remoCode.ToUpper())}));

                            if (objDetail == null)
                            {
                                checkMo.Add(remoCode, false);
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += "返工工单不存在;";
                                flag = false;
                                //continue;
                            }
                            else
                            {
                                checkMo.Add(remoCode, true);
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                            }
                        }
                    }
                }

                if (flag)
                {
                    if (checkRcard.ContainsKey(rcard))
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkRcard[rcard].ToString());
                        if (!bool.Parse(checkRcard[rcard].ToString()))
                        {
                            errorMessage += "产品序列号未完工;";
                            flag = false;
                        }
                        //continue;
                    }
                    else
                    {
                        object objDetail = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.Simulation),
                            new SQLParamCondition(@"select * from TBLSIMULATIONREPORT where 1=1 " + @"  and rcard =$RCARD",
                            new SQLParameter[] {
                                             new SQLParameter("$RCARD", typeof(string), rcard.ToUpper())}));

                        if (objDetail != null && (objDetail as Domain.DataCollect.SimulationReport).IsComplete != "1")
                        {
                            checkRcard.Add(rcard, false);
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += "产品序列号未完工;";
                            flag = false;
                            //continue;
                        }
                        else
                        {
                            checkRcard.Add(rcard, true);
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                        }
                    }
                }


                if (flag)
                {

                    if (checkRmaDetailRow.ContainsKey(rcard))
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += "产品序列号在RMA中重复;";
                        flag = false;
                        //continue;
                    }
                    else
                    {
                        string sql = @"SELECT b.* FROM TBLRMABILL a left JOIN TBLRMADETIAL b ON a.RMABILLCODE = b.rmabillcode WHERE " + @" b.rcard=$RCARD  AND  a.STATUS != '" + RMABillStatus.Closed + "'   ORDER BY a.MDATE DESC , a.MTIME DESC";
                        object objDetail = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.RMA.RMADetial),
                            new SQLParamCondition(sql,
                            new SQLParameter[] {
                                             new SQLParameter("$RCARD", typeof(string), rcard.ToUpper())}));

                        if (objDetail != null)
                        {
                            checkRmaDetailRow.Add(rcard, true);
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += "产品序列号在RMA中重复;";
                            flag = false;
                            //continue;
                        }
                        else
                        {
                            checkRmaDetailRow.Add(rcard, true);
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                        }
                    }
                }



                if (flag)
                {
                    /* 产品、产品别对应关系 */
                    string model2item = modelCode + "_" + itemcode;
                    if (checkedRowmodel2item.ContainsKey(model2item))
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkedRowmodel2item[model2item].ToString());
                        if (!bool.Parse(checkedRowmodel2item[model2item].ToString()))
                        {
                            errorMessage += this.languageComponent1.GetString("$Error_Model_Item_Not_Match") + ";";
                            flag = false;
                            //continue;
                        }
                    }
                    else
                    {
                        object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model2Item),
                            new SQLParamCondition("select itemcode from tblmodel2item where itemcode=$ITEMCODE and modelcode=$MODELCODE and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID,
                            new SQLParameter[]{new SQLParameter("$ITEMCODE",typeof(string),itemcode.ToUpper()),
										   new SQLParameter("$MODELCODE",typeof(string),modelCode.ToUpper())}));

                        if (objs != null)
                        {
                            checkedRowmodel2item.Add(model2item, true);
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                        }
                        else
                        {
                            checkedRowmodel2item.Add(model2item, false);
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += this.languageComponent1.GetString("$Error_Model_Item_Not_Match") + ";";

                            flag = false;
                            //continue;
                        }
                    }
                }

                if (flag)
                {
                    if (handelCode != "ts" && handelCode != "rework")
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += "处理方式必须是ts或者rework;";
                        flag = false;
                    }
                    else if (handelCode == "ts")
                    {
                        if (errorCode.Trim() == "")
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += "处理方式是维修的，不良代码必须填写;";
                            flag = false;
                        }
                        else
                        {
                            string errorCode1 = handelCode + "_" + errorCode;
                            if (checkErrorCode.ContainsKey(errorCode1))
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = bool.Parse(checkErrorCode[errorCode1].ToString());
                                if (!bool.Parse(checkErrorCode[errorCode1].ToString()))
                                {
                                    errorMessage += "不良代码不存在;";
                                    flag = false;
                                    //continue;
                                }
                            }
                            else
                            {
                                object[] objs = _dataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TSModel.ErrorCodeA),
                                            new SQLParamCondition("select *  from TBLEC where ECODE=$ECODE ",
                                            new SQLParameter[] { new SQLParameter("$ECODE", typeof(string), errorCode.ToUpper()) }));

                                if (objs != null)
                                {
                                    checkErrorCode.Add(errorCode, true);
                                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = true;
                                }
                                else
                                {
                                    checkErrorCode.Add(errorCode, false);
                                    gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                    errorMessage += "不良代码不存在;";
                                    flag = false;
                                }
                            }
                        }
                    }
                }

                if (flag)
                {
                    if (maintenanceDate == null || maintenanceDate == "")
                    {
                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += "保修期不能为空;";
                        flag = false;
                    }
                    else
                    {
                        if (maintenanceDate.Length > 4)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += "保修期格式不正确;";
                            flag = false;
                        }
                        else
                        {
                            try
                            {
                                int.Parse(maintenanceDate);
                            }
                            catch (Exception ex)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += "保修期格式不正确;";
                                flag = false;
                            }

                        }
                    }


                }

                if (flag)
                {
                    if (whreceivedateDate == null || maintenanceDate == "")
                    {

                        gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                        errorMessage += "仓库收货日期不能为空;";
                        flag = false;
                    }
                    else
                    {
                        if (whreceivedateDate.Length != 8)
                        {
                            gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                            errorMessage += "仓库收货日期格式不正确;";
                            flag = false;
                        }
                        else
                        {
                            try
                            {
                                FormatHelper.ToDateTime(int.Parse(whreceivedateDate));
                            }
                            catch (Exception ex)
                            {
                                gridRow.Items.FindItemByKey(this.gridHelper.CheckColumnKey).Value = false;
                                errorMessage += "仓库收货日期格式不正确;";
                                flag = false;
                            }
                        }
                    }
                }
                gridRow.Items.FindItemByKey("ImportResult").Text = FormartErrorMessage(errorMessage);
                gridRow.Items.FindItemByKey("ImportResult").CssClass = "MsgYellow";
                ////gridRow.Items.FindItemByKey("ImportResult").Style.ForeColor = TextColor.NoticeColor;
                this.ImportDatatable.Rows.Remove(row);
                this.ImportGridRow.Remove(gridRow);
            }
        }

        private string FormartErrorMessage(string errorMesssage)
        {
            if (!string.IsNullOrEmpty(errorMesssage))
            {
                errorMesssage = errorMesssage.TrimEnd(';').TrimStart(';');
            }
            return errorMesssage;
        }
        #endregion

        #region Help Method for OPBOM

        private void BuildOPBOM(OPBOM opBOM)
        {
            ItemFacade itemFacade = new ItemFacade(_dataProvider);
            object[] objs = itemFacade.QueryItem2Route(opBOM.ItemCode, string.Empty, GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString());

            object[] objsOPBOM = null;
            OPBOMFacade oPBOMFacade = new OPBOMFacade(_dataProvider);
            for (int i = 0; i < objs.Length; i++)
            {
                //判断该OPBOM是否已经存在，如果不存在则添加,存在不做任何动作
                objsOPBOM = oPBOMFacade.QueryOPBOM(opBOM.ItemCode, ((Item2Route)objs[i]).RouteCode, string.Empty, int.MinValue, int.MaxValue, ((Item2Route)objs[i]).OrganizationID);
                if (objsOPBOM == null)
                {
                    opBOM.OPBOMRoute = ((Item2Route)objs[i]).RouteCode;
                    opBOM.OPBOMCode = ((Item2Route)objs[i]).RouteCode;
                    oPBOMFacade.AddOPBOM(opBOM);
                }
            }
        }

        /// <summary>
        /// 上料
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="routeCode"></param>
        /// <param name="opcode"></param>
        /// <returns></returns>
        private bool CheckComponenetLoadingOperation(string itemCode, string routeCode, string opcode)
        {
            //上料工序需要维护bom
            string sql = String.Format("select count(opcode) from tblitemroute2op where itemcode='{0}' and routecode='{1}' and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID + " and opcode='{2}' and (substr(opcontrol," + ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentLoading + 1) + ",1)='1' ) ", itemCode, routeCode, opcode);

            int count = _dataProvider.GetCount(new SQLCondition(sql));
            return count > 0 ? true : false;
        }


        /// <summary>
        /// 下料
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="routeCode"></param>
        /// <param name="opcode"></param>
        /// <returns></returns>
        private bool CheckComponenetDownOperation(string itemCode, string routeCode, string opcode)
        {
            //下料工序需要维护bom
            string sql = String.Format("select count(opcode) from tblitemroute2op where itemcode='{0}' and routecode='{1}' and opcode='{2}' and orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID + " and ( substr(opcontrol," + ((int)BenQGuru.eMES.BaseSetting.OperationList.ComponentDown + 1) + ",1)='1') ", itemCode, routeCode, opcode);

            int count = _dataProvider.GetCount(new SQLCondition(sql));
            return count > 0 ? true : false;
        }


        #endregion
    }
}