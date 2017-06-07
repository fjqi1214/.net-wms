using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.MOModel
{
    public partial class FItem2SNCheckMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private ItemFacade _facade = null;//

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

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

        #region Init
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);

                this.LoadItemType();
                this.LoadExportImport();
                this.LoadCheckTypeQuery();
                this.LoadCheckTypeEdit();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region Init DropDownLists

        private void LoadItemType()
        {
            this.drpItemTypeQuery.Items.Clear();

            this.drpItemTypeQuery.Items.Add(new ListItem("", ""));
            string[] itemType = GetItemtype();
            foreach (string itemTypeUse in itemType)
            {
                this.drpItemTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(itemTypeUse.Trim()), itemTypeUse));
            }
        }

        private void LoadExportImport()
        {
            this.drpExportImportQuery.Items.Clear();

            this.drpExportImportQuery.Items.Add(new ListItem("", ""));
            this.drpExportImportQuery.Items.Add(new ListItem(this.languageComponent1.GetString("materialexportimport_import"), "IMPORT"));
            this.drpExportImportQuery.Items.Add(new ListItem(this.languageComponent1.GetString("materialexportimport_export"), "EXPORT"));
        }

        private void LoadCheckTypeQuery()
        {
            this.DropDownListCheckTypeQuery.Items.Clear();

            this.DropDownListCheckTypeQuery.Items.Add(new ListItem("", ""));
            this.DropDownListCheckTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_SERIAL), ItemCheckType.ItemCheckType_SERIAL));
            this.DropDownListCheckTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_ID1), ItemCheckType.ItemCheckType_ID1));
            this.DropDownListCheckTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_ID2), ItemCheckType.ItemCheckType_ID2));
            this.DropDownListCheckTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_ID3), ItemCheckType.ItemCheckType_ID3));
        }

        private void LoadCheckTypeEdit()
        {
            this.DropDownListCheckTypeEdit.Items.Clear();

            this.DropDownListCheckTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_SERIAL), ItemCheckType.ItemCheckType_SERIAL));
            this.DropDownListCheckTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_ID1), ItemCheckType.ItemCheckType_ID1));
            this.DropDownListCheckTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_ID2), ItemCheckType.ItemCheckType_ID2));
            this.DropDownListCheckTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(ItemCheckType.ItemCheckType_ID3), ItemCheckType.ItemCheckType_ID3));
        }


        private string[] GetItemtype()
        {
            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }
            return this._facade.GetItemType();
        }

        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();

            this.gridHelper.AddDataColumn("ItemCode", "产品代码");
            this.gridHelper.AddDataColumn("ItemDescription", "产品描述");
            this.gridHelper.AddDataColumn("SNPrefix", "序列号前缀");
            this.gridHelper.AddDataColumn("SNLength", "序列号长度");
            this.gridHelper.AddDataColumn("CheckType", "检查类型");
            this.gridHelper.AddDataColumn("Type", "");

            this.gridHelper.AddDefaultColumn(true, true);

            this.gridWebGrid.Columns.FromKey("Type").Hidden = true;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ItemCode"] = (obj as Item2SNCheckMP).ItemCode.ToString();
            row["ItemDescription"] = (obj as Item2SNCheckMP).ItemDesc.ToString();
            row["SNPrefix"] = (obj as Item2SNCheckMP).SNPrefix.ToString();
            row["SNLength"] = (obj as Item2SNCheckMP).SNLength.ToString();
            row["CheckType"] = this.languageComponent1.GetString(((Item2SNCheckMP)obj).Type.ToString());
            row["Type"] = (obj as Item2SNCheckMP).Type.ToString();

            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (this.txtSNLengthQuery.Text.Trim().Length > 0)
            {
                PageCheckManager manager = new PageCheckManager();

                manager.Add(new NumberCheck(lblSNLengthQuery, txtSNLengthQuery, 0, 40, false));

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return null;
                }
            }

            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }
            return this._facade.QueryItem2SNCheck(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.CleanString(this.txtItemDescQuery.Text),
                FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue.ToString()),
                FormatHelper.CleanString(this.DropDownListCheckTypeQuery.SelectedValue),
                FormatHelper.CleanString(this.drpExportImportQuery.SelectedValue.ToString()),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNPrefixQuery.Text)),
                FormatHelper.CleanString(this.txtSNLengthQuery.Text),
                inclusive, exclusive);

        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }
            return this._facade.QueryItem2SNCheckCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.CleanString(this.txtItemDescQuery.Text), FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue.ToString()),
                FormatHelper.CleanString(this.drpExportImportQuery.SelectedValue.ToString()), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNPrefixQuery.Text)), FormatHelper.CleanString(this.txtSNLengthQuery.Text));
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }
            object objMaterial = _facade.GetMaterial(FormatHelper.PKCapitalFormat(this.txtItemCode.Text.Trim()), GlobalVariables.CurrentOrganizations.First().OrganizationID);
            if (objMaterial == null)
            {
                WebInfoPublish.Publish(this, "$Error_ItemCode_NotExist", this.languageComponent1);
                return;
            }
            if (string.Compare(((Domain.MOModel.Material)objMaterial).MaterialType, ItemType.ITEMTYPE_RAWMATERIAL, true) == 0)
            {
                WebInfoPublish.Publish(this, "$Error_RowMaterialNeedNotSNCheck", this.languageComponent1);
                return;
            }
            this._facade.AddItem2SNCheck((Item2SNCheck)this.GetEditObject());
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }

            this._facade.DeleteItem2SNCheck((Item2SNCheck[])domainObjects.ToArray(typeof(Item2SNCheck)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }

            this._facade.UpdateItem2SNCheck((Item2SNCheck)this.GetEditObject());
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtItemCode.ReadOnly = false;
                this.DropDownListCheckTypeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtItemCode.ReadOnly = true;
                this.DropDownListCheckTypeEdit.Enabled = false;
            }
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }

            Item2SNCheck item2SNCheck = this._facade.CreateNewItem2SNCheck();

            item2SNCheck.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCode.Text, 40));
            item2SNCheck.SNPrefix = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNPrefix.Text, 40));
            if (this.txtSNLength.Text.Trim().Length > 0)
            {
                item2SNCheck.SNLength = int.Parse(this.txtSNLength.Text);
            }
            item2SNCheck.Type = this.DropDownListCheckTypeEdit.SelectedValue;
            item2SNCheck.SNContentCheck = this.chkSNContentCheck.Checked ? SNContentCheckStatus.SNContentCheckStatus_Need : SNContentCheckStatus.SNContentCheckStatus_NONeed;
            item2SNCheck.MaintainUser = this.GetUserCode();

            return item2SNCheck;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new ItemFacade(this.DataProvider);
            }

            string strCode = string.Empty;
            string strType = string.Empty;

            object objCode = row.Items.FindItemByKey("ItemCode").Value;
            object objType = row.Items.FindItemByKey("Type").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            if (objType != null)
            {
                strType = objType.ToString();
            }
            object obj = _facade.GetItem2SNCheck(strCode, strType);

            if (obj != null)
            {
                return (Item2SNCheck)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtItemCode.Text = "";
                this.txtSNPrefix.Text = "";
                this.txtSNLength.Text = "";
                this.chkSNContentCheck.Checked = true;
                this.DropDownListCheckTypeEdit.SelectedIndex = 0;
                return;
            }

            this.txtItemCode.Text = ((Item2SNCheck)obj).ItemCode.ToString();
            this.txtSNPrefix.Text = ((Item2SNCheck)obj).SNPrefix.ToString();
            if (!string.IsNullOrEmpty(((Item2SNCheck)obj).SNLength.ToString()))
            {
                this.txtSNLength.Text = ((Item2SNCheck)obj).SNLength.ToString();
            }

            if (((Item2SNCheck)obj).SNContentCheck.ToString() == SNContentCheckStatus.SNContentCheckStatus_Need)
            {
                this.chkSNContentCheck.Checked = true;
            }
            else
            {
                this.chkSNContentCheck.Checked = false;
            }

            this.DropDownListCheckTypeEdit.SelectedValue = ((Item2SNCheck)obj).Type.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblItemCode, txtItemCode, 40, true));
            manager.Add(new LengthCheck(lblSNPrefix, txtSNPrefix, 40, false));
            manager.Add(new LengthCheck(lblSNLength, txtSNLength, 6, false));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if ((this.txtSNLength.Text.Trim().Length == 0 || Int32.Parse(this.txtSNLength.Text.Trim()) == 0)
                && this.txtSNPrefix.Text.Trim().Length == 0)
            {
                WebInfoPublish.Publish(this, "$SNLengthAndSNPrefix_Must_Input_One", this.languageComponent1);
                return false;
            }

            if (this.txtSNLength.Text.Trim().Length > 0
                && Int32.Parse(this.txtSNLength.Text.Trim()) > 0
                && this.txtSNPrefix.Text.Trim().Length > 0
                && this.txtSNPrefix.Text.Trim().Length >= int.Parse(this.txtSNLength.Text.Trim()))
            {
                WebInfoPublish.Publish(this, "$Error_SNLengthTooShort", this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{   ((Item2SNCheckMP)obj).ItemCode.ToString(),
                                   ((Item2SNCheckMP)obj).ItemDesc.ToString(),                                   
                                    ((Item2SNCheckMP)obj).SNPrefix.ToString(),
                                    ((Item2SNCheckMP)obj).SNLength.ToString(),
                                   this.languageComponent1.GetString(((Item2SNCheckMP)obj).Type.ToString()) }
                ;
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"ItemCode",
                                    "ItemDesc",   
                                    "SNPrefix",
                                    "SNLength",
                                    "CheckType"};
        }
        #endregion

    }
}
