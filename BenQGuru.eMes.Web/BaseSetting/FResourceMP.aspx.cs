using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FResourceMP 的摘要说明。
    /// </summary>
    public partial class FResourceMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;

        //private BenQGuru.eMES.BaseSetting.ShiftModelFacade _shiftFacade = null;//new ShiftModelFacade();

        private BenQGuru.eMES.BaseSetting.BaseModelFacade _facade = null; //new BaseModelFacadeFactory().Create();
        private ShiftModel shiftModelFacade = null;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region 数据初始化

        protected void drpCrewCode_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.drpCrewCodeEdit);

                if (shiftModelFacade == null)
                {
                    shiftModelFacade = new ShiftModel(base.DataProvider);
                }

                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this.shiftModelFacade.GetAllShiftCrew);

                builder.Build("CrewCode", "CrewCode");
                this.drpCrewCodeEdit.Items.Insert(0, new ListItem("", ""));

                this.drpCrewCodeEdit.SelectedIndex = 0;

            }
        }

        protected void drpStepSequenceCodeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.drpStepSequenceCodeEdit);
                if (_facade == null)
                {
                    _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
                }
                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllStepSequence);

                builder.Build("StepSequenceDescription", "StepSequenceCode");

                //添加一个默认的空选项,新增时为非必选 modify by Simone
                //ListItem sf0=new ListItem("","");	
                //this.drpStepSequenceCodeEdit.Items.Add(sf0);

                this.drpStepSequenceCodeEdit.Items.Insert(0, new ListItem("", ""));
                //this.drpStepSequenceCodeEdit.SelectedIndex = 0;
                this.drpStepSequenceCodeEdit.Items.FindByValue("").Selected = true;
                this.drpStepSequenceCodeEdit_SelectedIndexChanged(this, null);
                this.BuildOrgList();
            }
        }

        protected void drpDCT_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListDCT);
                if (_facade == null)
                {
                    _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
                }


                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllDCT);

                //builder.Build("DctCode", "DctCode");

                ////添加一个默认的空选项,新增时为非必选 modify by Simone
                //ListItem sf0 = new ListItem("", "");
                //this.DropDownListDCT.Items.Add(sf0);
                //this.DropDownListDCT.Items.FindByValue("").Selected = true;

                builder.Build("DctCode", "DctCode");
                this.DropDownListDCT.Items.Insert(0, new ListItem("", ""));

                this.DropDownListDCT.SelectedIndex = 0;

            }
        }

        protected void drpStepSequenceCodeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            object obj = this._facade.GetStepSequence(this.drpStepSequenceCodeEdit.SelectedValue);

            if (obj == null)
            {
                this.txtSegmentCodeEdit.Text = "";

                return;
            }

            this.txtSegmentCodeEdit.Text = ((StepSequence)obj).SegmentCode;
            if (obj == null)
            {
                this.txtShiftEdit.Text = "";
                return;
            }
            else
            {
                this.txtShiftEdit.Text = ((StepSequence)obj).ShiftTypeCode;
            }
            /*object _Segment = this._facade.GetSegment(this.txtSegmentCodeEdit.Text.Trim());
            if(_Segment == null)
            {
                this.txtShiftEdit.Text = "";
                return;
            }
            this.txtShiftEdit.Text = ((Segment)_Segment).ShiftTypeCode;
             * */
        }

        protected void drpResourceTypeEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                IInternalSystemVariable variable = InternalSystemVariable.Lookup(ResourceType.ResourceTypeName);
                if (variable != null)
                {
                    foreach (string code in variable.Items)
                    {
                        string msg = this.languageComponent1.GetString(code);
                        if (msg == "" || msg == null)
                        {
                            msg = code;
                        }
                        this.drpResourceTypeEdit.Items.Add(new ListItem(msg, code));
                    }
                }
                //				else
                //				{
                //					WebInfoPublish.Publish(this,"$Error_No_ResourceType",this.languageComponent1);
                //
                //					return;
                //				}
            }
        }

        protected void drpResourceGroupEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                new SystemParameterListBuilder("ResourceGroup", base.DataProvider).Build(this.drpResourceGroupEdit);
            }
        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.DropDownListOrg.Enabled = true;
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("ResourceCode", "资源代码", null);
            this.gridHelper.AddColumn("ResourceDescription", "资源描述", null);
            this.gridHelper.AddColumn("ResourceStepSequence", "所属生产线", null);
            this.gridHelper.AddColumn("ReworkRouteCode", "返工途程", null);
            this.gridHelper.AddColumn("ReworkSheetCode", "返工需求单", null);
            this.gridHelper.AddColumn("DctCode", "默认DCT指令", null);
            this.gridHelper.AddColumn("CrewCode", "班组代码", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);

            this.gridHelper.AddColumn("ResourceType", "资源类别", null);
            this.gridHelper.AddColumn("ResourceGroup", "资源归属", null);
            this.gridHelper.AddColumn("ShiftTypeCode", "班制代码", null);
            this.gridHelper.AddColumn("SegmentCode", "工段代码", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);

            this.gridWebGrid.Columns.FromKey("ResourceType").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ResourceGroup").Hidden = true;
            this.gridWebGrid.Columns.FromKey("ShiftTypeCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("SegmentCode").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {           
            DataRow row = this.DtSource.NewRow();
            row["ResourceCode"] = ((Resource)obj).ResourceCode.ToString();
            row["ResourceDescription"] = ((Resource)obj).ResourceDescription.ToString();
            row["ResourceStepSequence"] = ((Resource)obj).GetDisplayText("StepSequenceCode");
            row["ReworkRouteCode"] = ((Resource)obj).GetDisplayText("ReworkRouteCode");
            row["ReworkSheetCode"] = GetReworkListString(((Resource)obj).ResourceCode);
            row["DctCode"] = ((Resource)obj).DctCode.ToString();
            row["CrewCode"] = ((Resource)obj).CrewCode.ToString();
            row["MaintainUser"] = ((Resource)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Resource)obj).MaintainDate);

            row["ResourceType"] = ((Resource)obj).ResourceType.ToString();
            row["ResourceGroup"] = ((Resource)obj).ResourceGroup.ToString();
            row["ShiftTypeCode"] = ((Resource)obj).ShiftTypeCode.ToString();

            row["SegmentCode"] = ((Resource)obj).SegmentCode.ToString();
            row["MaintainTime"] = FormatHelper.ToTimeString(((Resource)obj).MaintainTime);
            return row;

        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryResource(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequenceCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text)),
                inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            return this._facade.QueryResourceCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequenceCodeQuery.Text)),
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCrewCodeQuery.Text))
            );
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }

            try
            {
                this.DataProvider.BeginTransaction();
                this._facade.DeleteAllResource2ReworkSheet(((Resource)domainObject).ResourceCode);
                this._facade.AddResource((Resource)domainObject);
                AddRes2ReworkSheet(((Resource)domainObject).ResourceCode, this.txtReworkMo.Text.Trim().ToUpper());
                this.DataProvider.CommitTransaction();
            }
            catch
            {
                this.DataProvider.RollbackTransaction();
                throw;
            }
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }

            foreach (Resource res in domainObjects)
            {
                try
                {
                    this.DataProvider.BeginTransaction();
                    this._facade.DeleteResource(res);
                    this.DataProvider.CommitTransaction();
                }
                catch
                {
                    this.DataProvider.RollbackTransaction();
                    throw;
                }
            }
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }

            try
            {
                this.DataProvider.BeginTransaction();
                this._facade.DeleteAllResource2ReworkSheet(((Resource)domainObject).ResourceCode);
                this._facade.UpdateResource((Resource)domainObject);
                AddRes2ReworkSheet(((Resource)domainObject).ResourceCode, this.txtReworkMo.Text.Trim().ToUpper());
                this.DataProvider.CommitTransaction();
            }
            catch
            {
                this.DataProvider.RollbackTransaction();
                throw;
            }
        }


        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtResourceCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtResourceCodeEdit.ReadOnly = true;
                this.DropDownListOrg.Enabled = false;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Resource resource = this._facade.CreateNewResource();

            string routeCode = GetRouteCodeFromReworkCodeList(this.txtReworkMo.Text.Trim().ToUpper());
            if (routeCode.Trim().Length > 0)
            {
                resource.ReworkRouteCode = routeCode;
            }
            else
            {
                resource.ReworkRouteCode = this.txtReworkRoute.Text.Trim().ToUpper();
            }

            resource.ResourceDescription = FormatHelper.CleanString(this.txtResourceDescriptionEdit.Text, 100);
            resource.ResourceType = this.drpResourceTypeEdit.SelectedValue;
            resource.ResourceGroup = this.drpResourceGroupEdit.SelectedValue;
            resource.DctCode = this.DropDownListDCT.SelectedValue;
            resource.CrewCode = this.drpCrewCodeEdit.SelectedValue;
            resource.ResourceCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtResourceCodeEdit.Text, 40));
            resource.StepSequenceCode = (this.drpStepSequenceCodeEdit.SelectedValue != "") ? this.drpStepSequenceCodeEdit.SelectedValue : "";
            resource.SegmentCode = FormatHelper.CleanString(this.txtSegmentCodeEdit.Text, 40);
            resource.ShiftTypeCode = FormatHelper.CleanString(this.txtShiftEdit.Text, 40);
            resource.MaintainUser = this.GetUserCode();
            resource.OrganizationID = int.Parse(this.DropDownListOrg.SelectedItem.Value);

            return resource;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("ResourceCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetResource(strCode);
            if (obj != null)
            {
                return (Resource)obj;
            }
            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtResourceDescriptionEdit.Text = "";
                this.drpResourceTypeEdit.SelectedIndex = -1;
                this.drpResourceGroupEdit.SelectedIndex = -1;
                this.DropDownListDCT.SelectedIndex = -1;
                this.drpCrewCodeEdit.SelectedIndex = -1;
                this.txtResourceCodeEdit.Text = "";
                this.drpStepSequenceCodeEdit.SelectedIndex = -1;
                this.txtSegmentCodeEdit.Text = "";
                this.txtShiftEdit.Text = "";
                this.drpStepSequenceCodeEdit_SelectedIndexChanged(this, null);
                this.DropDownListOrg.SelectedIndex = 0;
                this.txtReworkRoute.Text = "";
                this.txtReworkMo.Text = "";
                return;
            }

            this.txtResourceCodeEdit.Text = ((Resource)obj).ResourceCode.ToString();
            this.txtResourceDescriptionEdit.Text = ((Resource)obj).ResourceDescription.ToString();
            this.txtReworkRoute.Text = ((Resource)obj).ReworkRouteCode.ToString();
            this.txtReworkMo.Text = ((Resource)obj).ReworkCode.ToString();
            this.txtReworkMo.Text = GetReworkListString(((Resource)obj).ResourceCode);

            try
            {
                this.drpResourceTypeEdit.SelectedValue = ((Resource)obj).ResourceType.ToString();
            }
            catch
            {
                this.drpResourceTypeEdit.SelectedIndex = -1;
            }

            try
            {
                this.drpResourceGroupEdit.SelectedValue = ((Resource)obj).ResourceGroup.ToString();
            }
            catch
            {
                this.drpResourceGroupEdit.SelectedIndex = -1;
            }

            try
            {
                this.drpStepSequenceCodeEdit.SelectedValue = ((Resource)obj).StepSequenceCode.ToString();
            }
            catch
            {
                this.drpStepSequenceCodeEdit.SelectedIndex = -1;
            }
            this.drpStepSequenceCodeEdit_SelectedIndexChanged(this, null);

            this.txtSegmentCodeEdit.Text = ((Resource)obj).SegmentCode.ToString();

            try
            {
                this.DropDownListOrg.SelectedValue = ((Resource)obj).OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }

            try
            {
                this.DropDownListDCT.SelectedValue = ((Resource)obj).DctCode.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = -1;
            }

            try
            {
                this.drpCrewCodeEdit.SelectedValue = ((Resource)obj).CrewCode.ToString();
            }
            catch
            {
                this.drpCrewCodeEdit.SelectedIndex = -1;
            }

        }

        protected override bool ValidateInput()
        {
            ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblResourceCodeEdit, this.txtResourceCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblResourceDescriptionEdit, this.txtResourceDescriptionEdit, 100, false));
            manager.Add(new LengthCheck(this.lblOrgIDEdit, this.DropDownListOrg, 8, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (this.txtReworkRoute.Text.Trim().Length != 0)
            {
                object route = (new BaseModelFacade(this.DataProvider)).GetRoute(this.txtReworkRoute.Text.Trim().ToUpper());
                if (route == null)
                {
                    WebInfoPublish.Publish(this, "$Error_RouteNotExist", this.languageComponent1);
                    return false;
                }

                object route2OP = (new BaseModelFacade(this.DataProvider)).GetOperationByRouteCode(this.txtReworkRoute.Text.Trim().ToUpper());
                if (route2OP == null)
                {
                    WebInfoPublish.Publish(this, "$Error_RouteHasNoOperations", this.languageComponent1);
                    return false;
                }
            }

            if (this.txtReworkMo.Text.Trim().Length > 0)
            {
                string[] reworkCodeList = this.txtReworkMo.Text.Trim().ToUpper().Split(',');
                if (reworkCodeList == null)
                {
                    reworkCodeList = new string[0];
                }

                //返工需求单必须都存在
                string errorMessage = string.Empty;
                foreach (string reworkCode in reworkCodeList)
                {
                    ReworkSheet reworkSheet = (ReworkSheet)reworkFacade.GetReworkSheet(reworkCode);
                    if (reworkSheet == null)
                    {
                        errorMessage += "$Error_ReworkSheet_NotExist" + " : " + reworkCode + "\n";
                    }
                }

                if (errorMessage.Trim().Length > 0)
                {
                    WebInfoPublish.Publish(this, errorMessage, this.languageComponent1);
                    return false;
                }

                //保证相同产品的多张返工需求单，只能有一张没有判退批（及LotNo不能重复）
                List<string> itemCodeAndLotNoList = new List<string>();
                foreach (string reworkCode in reworkCodeList)
                {
                    ReworkSheet reworkSheet = (ReworkSheet)reworkFacade.GetReworkSheet(reworkCode);
                    string itemCodeAndLotNo = reworkSheet.ItemCode + "\t" + (reworkSheet.LotList == null ? string.Empty : reworkSheet.LotList);
                    if (itemCodeAndLotNoList.Contains(itemCodeAndLotNo))
                    {
                        WebInfoPublish.Publish(this, "$Error_Res2ReworkSheetPK", this.languageComponent1);
                        return false;
                    }
                    else
                    {
                        itemCodeAndLotNoList.Add(itemCodeAndLotNo);
                    }
                }

                //保证所选择的ReworkSheet的Route一致，并且存在
                string routeCode = GetRouteCodeFromReworkCodeList(this.txtReworkMo.Text.Trim().ToUpper());
                if (routeCode.Trim().Length <= 0)
                {
                    //返工需求单的返工途程不存在或者不一致
                    WebInfoPublish.Publish(this, "$Error_ReworkSheetsNewRouteNotSame", this.languageComponent1);
                    return false;
                }
                else
                {
                    object route = (new BaseModelFacade(this.DataProvider)).GetRoute(routeCode);
                    if (route == null)
                    {
                        WebInfoPublish.Publish(this, "$Error_RouteNotExist", this.languageComponent1);
                        return false;
                    }

                    object route2OP = (new BaseModelFacade(this.DataProvider)).GetOperationByRouteCode(routeCode);
                    if (route2OP == null)
                    {
                        WebInfoPublish.Publish(this, "$Error_RouteHasNoOperations", this.languageComponent1);
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Export
        // 2005-04-06

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((Resource)obj).ResourceCode.ToString(),
								   ((Resource)obj).ResourceDescription.ToString(),
								   ((Resource)obj).GetDisplayText("StepSequenceCode"),
                                   ((Resource)obj).GetDisplayText("ReworkRouteCode"),
                                   ((Resource)obj).ReworkCode.ToString(), 
                                   ((Resource)obj).DctCode.ToString(),
                                   ((Resource)obj).CrewCode.ToString(),
								   ((Resource)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Resource)obj).MaintainDate)};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"ResourceCode",
									"ResourceDescription",
									"ResourceStepSequence",	
                                    "ReworkRouteCode",
                                    "ReworkSheetCode",
                                    "DctCode",
                                    "Crewcode",
									"MaintainUser",
									"MaintainDate" };
        }

        #endregion

        private void BuildOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));

            this.DropDownListOrg.SelectedIndex = 0;
        }

        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }

        private string GetRouteCodeFromReworkCodeList(string reworkCodeListString)
        {
            ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);

            string returnValue = string.Empty;

            //此处需要保证各个ReworkCode抓取出的NewRouteCode都相同，否则返回空字符串
            if (reworkCodeListString.Trim().Length > 0)
            {
                string[] reworkCodeList = reworkCodeListString.Trim().ToUpper().Split(',');
                if (reworkCodeList == null)
                {
                    reworkCodeList = new string[0];
                }

                foreach (string reworkCode in reworkCodeList)
                {
                    ReworkSheet reworkSheet = (ReworkSheet)reworkFacade.GetReworkSheet(reworkCode);
                    if (reworkSheet == null || reworkSheet.NewRouteCode == null || reworkSheet.NewRouteCode.Trim().Length <= 0)
                    {
                        returnValue = string.Empty;
                        break;
                    }
                    else if (returnValue.Trim().Length > 0 && string.Compare(returnValue.Trim(), reworkSheet.NewRouteCode.Trim(), true) != 0)
                    {
                        returnValue = string.Empty;
                        break;
                    }
                    else
                    {
                        returnValue = reworkSheet.NewRouteCode.Trim().ToUpper();
                    }
                }
            }

            return returnValue;
        }

        private void AddRes2ReworkSheet(string resCode, string reworkCodeListString)
        {
            ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }

            if (reworkCodeListString.Trim().Length > 0)
            {
                string[] reworkCodeList = reworkCodeListString.Trim().ToUpper().Split(',');
                if (reworkCodeList == null)
                {
                    reworkCodeList = new string[0];
                }

                foreach (string reworkCode in reworkCodeList)
                {
                    ReworkSheet reworkSheet = (ReworkSheet)reworkFacade.GetReworkSheet(reworkCode);
                    if (reworkSheet != null)
                    {
                        Resource2ReworkSheet res2ReworkSheet = _facade.CreateNewResource2ReworkSheet();
                        res2ReworkSheet.ResourceCode = resCode;
                        res2ReworkSheet.ItemCode = reworkSheet.ItemCode;
                        res2ReworkSheet.LotNo = reworkSheet.LotList;
                        res2ReworkSheet.ReworkCode = reworkSheet.ReworkCode;
                        res2ReworkSheet.MaintainUser = this.GetUserCode();
                        _facade.AddResource2ReworkSheet(res2ReworkSheet);
                    }
                }
            }
        }

        private string GetReworkListString(string resourceCode)
        {
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);

            string reworkSheetList = string.Empty;
            object[] resource2ReworkSheetList = baseModelFacade.QueryResource2ReworkSheet(resourceCode);
            if (resource2ReworkSheetList != null)
            {
                foreach (Resource2ReworkSheet rework in resource2ReworkSheetList)
                {
                    if (reworkSheetList.Trim().Length > 0)
                    {
                        reworkSheetList += ",";
                    }
                    reworkSheetList += rework.ReworkCode;
                }
            }

            return reworkSheetList;
        }
    }
}
