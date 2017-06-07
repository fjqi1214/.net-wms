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
    public partial class FItem2LotCheckMP : BaseMPageNew
    {
        #region 变量
        //多语言变量，新建页面时直接保留就可以，不需处理
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        //成员变量遵循编程规范
        private ItemLotFacade m_ItemLotFacade = null;
        private ItemFacade m_ItemFacade = null;
        #endregion

        #region Web 窗体设计器生成的代码 此部分方法一般不需要变动
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

                //用于页面首次加载时需要同时调用的方法，比如下拉列表，当前日期等
                this.LoadItemType();
                this.LoadExportImport();
                this.LoadCreateType();

                //JS的多语言提示可将其加载到页面的隐藏域，然后页面读取
                this.hiddenInfo.Value = this.languageComponent1.GetString("$Error_LotNo_Length_IsNumber");
            }
        }

        //此为多语言调用，保留不更改
        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #endregion

        #region Init DropDownLists RadioButtonList

        /// <summary>
        /// 初始化下拉列表，将可选项加入列表(产品类别)
        /// </summary>
        private void LoadItemType()
        {
            this.drpItemTypeQuery.Items.Clear();

            this.drpItemTypeQuery.Items.Add(new ListItem("", ""));//添加""下拉选项，意为全选
            ItemType itemType = new ItemType();
            foreach (string itemTypeUse in itemType.Items)//添加需要生成下拉项的枚举的所有值
            {
                this.drpItemTypeQuery.Items.Add(new ListItem(this.languageComponent1.GetString(itemTypeUse.Trim()), itemTypeUse));//值为枚举值，显示文本从多语言文件获取
            }
            this.drpItemTypeQuery.Items.Remove(new ListItem(this.languageComponent1.GetString(ItemType.ITEMTYPE_RAWMATERIAL), ItemType.ITEMTYPE_RAWMATERIAL));//移除不需要的下拉项
        }

        /// <summary>
        /// 初始化下拉列表，将可选项加入列表(内外销)
        /// </summary>
        private void LoadExportImport()
        {
            this.drpExportImportQuery.Items.Clear();
            #region 添加下拉项，不推荐此种写法
            this.drpExportImportQuery.Items.Add(new ListItem("", ""));
            this.drpExportImportQuery.Items.Add(new ListItem(this.languageComponent1.GetString("materialexportimport_import"), "IMPORT"));
            this.drpExportImportQuery.Items.Add(new ListItem(this.languageComponent1.GetString("materialexportimport_export"), "EXPORT"));
            #endregion
        }

        /// <summary>
        /// 初始化单选组，将可选单选加入单选组(进制)
        /// </summary>
        private void LoadCreateType()
        {
            this.RadioButtonListCreateTypeEdit.Items.Clear();
            CreateType createTypes = new CreateType();
            foreach (string createType in createTypes.Items)//生成枚举下维护的值的单选
            {
                this.RadioButtonListCreateTypeEdit.Items.Add(new ListItem(this.languageComponent1.GetString(createType.Trim()), createType));//值为枚举值，显示文本从多语言文件获取
            }
            this.RadioButtonListCreateTypeEdit.SelectedIndex = 0;//设定默认选择
        }

        #endregion

        #region WebGrid
        /// <summary>
        /// 设定WebGrid的列以及需要隐藏的列，应用WebGrid多语言
        /// </summary>
        protected override void InitWebGrid()
        {
            base.InitWebGrid();

            //添加普通列，需要在多语言文件中维护多语言
            this.gridHelper.AddDataColumn("ItemCode", "产品代码");
            this.gridHelper.AddDataColumn("ItemDescription", "产品描述");
            this.gridHelper.AddDataColumn("LotPrefix", "批次条码前缀");
            this.gridHelper.AddDataColumn("LotLength", "批次条码长度");
            this.gridHelper.AddDataColumn("CreateType", "生成进制");
            this.gridHelper.AddDataColumn("MUser", "维护人员");
            this.gridHelper.AddDataColumn("MDate", "维护日期");
            this.gridHelper.AddDataColumn("MTime", "维护时间");
            //this.gridHelper.AddColumn("Type", "");

            //设定需要隐藏的列
            //this.gridWebGrid.Columns.FromKey("Type").Hidden = true;

            //添加勾选列和编辑列
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        /// <summary>
        /// 用查询得到的实体填充Grid的行，按勾选列在最前，编辑列在最后，普通列在中间，按顺序依次填充即可
        /// </summary>
        /// <param name="obj">Item2LotCheckMP</param>
        /// <returns>UltraGridRow</returns>
        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ItemCode"] = (obj as Item2LotCheckMP).ItemCode.ToString();
            row["ItemDescription"] = (obj as Item2LotCheckMP).ItemDesc.ToString();
            row["LotPrefix"] = (obj as Item2LotCheckMP).SNPrefix.ToString();
            row["LotLength"] = (obj as Item2LotCheckMP).SNLength.ToString();
            row["CreateType"] = this.languageComponent1.GetString(((Item2LotCheckMP)obj).CreateType.ToString());
            row["MUser"] = (obj as Item2LotCheckMP).MUser.ToString();
            row["MDate"] = FormatHelper.ToDateString(((Item2LotCheckMP)obj).MaintainDate);
            row["MTime"] = FormatHelper.ToTimeString(((Item2LotCheckMP)obj).MaintainTime);

            return row;
        }

        /// <summary>
        /// 分页获得查询的数据
        /// </summary>
        /// <param name="inclusive">最小</param>
        /// <param name="exclusive">最大</param>
        /// <returns>object[]</returns>
        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            #region 查询栏位检查
            if (this.txtSNLengthQuery.Text.Trim().Length > 0)
            {
                PageCheckManager manager = new PageCheckManager();

                manager.Add(new NumberCheck(lblLotLengthQuery, txtSNLengthQuery, 0, 40, false));

                if (!manager.Check())
                {
                    WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                    return null;
                }
            }
            #endregion

            if (m_ItemLotFacade == null)
            {
                m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            }
            return this.m_ItemLotFacade.QueryItem2LotCheck(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                FormatHelper.CleanString(this.txtItemDescQuery.Text),
                FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue.ToString()),
                FormatHelper.CleanString(this.drpExportImportQuery.SelectedValue.ToString()),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNPrefixQuery.Text)),
                FormatHelper.CleanString(this.txtSNLengthQuery.Text),
                inclusive, exclusive);

        }

        /// <summary>
        /// 获得查询结果的总记录数
        /// </summary>
        /// <returns>int</returns>
        protected override int GetRowCount()
        {
            if (m_ItemLotFacade == null)
            {
                m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            }
            return this.m_ItemLotFacade.QueryItem2LotCheckCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.CleanString(this.txtItemDescQuery.Text), FormatHelper.CleanString(this.drpItemTypeQuery.SelectedValue.ToString()),
                FormatHelper.CleanString(this.drpExportImportQuery.SelectedValue.ToString()), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNPrefixQuery.Text)), FormatHelper.CleanString(this.txtSNLengthQuery.Text));
        }

        #endregion

        #region Button
        /// <summary>
        /// 点击新增按钮时调用，将实体信息新增一笔数据到数据库中
        /// </summary>
        /// <param name="domainObject">需要新增记录的实体</param>
        protected override void AddDomainObject(object domainObject)
        {
            if (m_ItemLotFacade == null)
            {
                m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            }
            if (m_ItemFacade == null)
            {
                m_ItemFacade = new ItemFacade(this.DataProvider);
            }

            #region 新增时除ValidateInput外需要做的额外检查
            object objMaterial = m_ItemFacade.GetMaterial(FormatHelper.PKCapitalFormat(this.txtItemCode.Text.Trim()), GlobalVariables.CurrentOrganizations.First().OrganizationID);
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
            #endregion

            this.m_ItemLotFacade.AddItem2LotCheck((Item2LotCheck)domainObject);
        }

        /// <summary>
        /// 点击删除时调用，将勾选的记录从数据库中删除
        /// </summary>
        /// <param name="domainObjects">需要删除的对象列表</param>
        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (m_ItemLotFacade == null)
            {
                m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            }

            this.m_ItemLotFacade.DeleteItem2LotCheck((Item2LotCheck[])domainObjects.ToArray(typeof(Item2LotCheck)));
        }

        /// <summary>
        /// 点击保存时调用，将修改后的信息更新到数据库中
        /// </summary>
        /// <param name="domainObject">需要更新的实体</param>
        protected override void UpdateDomainObject(object domainObject)
        {
            if (m_ItemLotFacade == null)
            {
                m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            }

            this.m_ItemLotFacade.UpdateItem2LotCheck((Item2LotCheck)domainObject);
        }

        /// <summary>
        /// 根据当前操作类型设定页面控件状态
        /// </summary>
        /// <param name="pageAction">当前操作类型</param>
        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtItemCode.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtItemCode.ReadOnly = true;
            }
        }

        #endregion

        #region Object <--> Page
        /// <summary>
        /// 获取当前需要处理的实体，新增和保存时均会调用
        /// </summary>
        /// <returns>需要处理的object</returns>
        protected override object GetEditObject()
        {
            if (m_ItemLotFacade == null)
            {
                m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            }

            Item2LotCheck item2LotCheck = this.m_ItemLotFacade.CreateNewItem2LotCheck();
            item2LotCheck.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCode.Text, 40));
            item2LotCheck.SNPrefix = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNPrefix.Text, 40));
            if (this.txtSNLength.Text.Trim().Length > 0)
            {
                item2LotCheck.SNLength = int.Parse(this.txtSNLength.Text);
            }
            item2LotCheck.CreateType = this.RadioButtonListCreateTypeEdit.SelectedValue;
            item2LotCheck.SNContentCheck = this.chkSNContentCheck.Checked ? SNContentCheckStatus.SNContentCheckStatus_Need : SNContentCheckStatus.SNContentCheckStatus_NONeed;
            item2LotCheck.MUser = this.GetUserCode();

            return item2LotCheck;
        }

        /// <summary>
        /// 点击编辑按钮后获取与当前行的记录对应的实体，删除时也会调用
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        protected override object GetEditObject(GridRecord row)
        {
            if (m_ItemLotFacade == null)
            {
                m_ItemLotFacade = new ItemLotFacade(this.DataProvider);
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("ItemCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            //避免使用row.Cells[1]，应使用row.Cells.FromKey("ItemCode")
            object obj = m_ItemLotFacade.GetItem2LotCheck(strCode);

            if (obj != null)
            {
                return (Item2LotCheck)obj;
            }

            return null;
        }

        /// <summary>
        /// 将需要处理的实体显示到编辑区域对应的位置
        /// </summary>
        /// <param name="obj">待处理的实体</param>
        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtItemCode.Text = "";
                this.txtSNPrefix.Text = "";
                this.txtSNLength.Text = "";
                this.chkSNContentCheck.Checked = true;
                this.RadioButtonListCreateTypeEdit.SelectedIndex = 0;
                return;
            }

            this.txtItemCode.Text = ((Item2LotCheck)obj).ItemCode.ToString();
            this.txtSNPrefix.Text = ((Item2LotCheck)obj).SNPrefix.ToString();
            if (!string.IsNullOrEmpty(((Item2LotCheck)obj).SNLength.ToString()))
            {
                this.txtSNLength.Text = ((Item2LotCheck)obj).SNLength.ToString();
            }

            if (((Item2LotCheck)obj).SNContentCheck.ToString() == SNContentCheckStatus.SNContentCheckStatus_Need)
            {
                this.chkSNContentCheck.Checked = true;
            }
            else
            {
                this.chkSNContentCheck.Checked = false;
            }

            this.RadioButtonListCreateTypeEdit.SelectedValue = ((Item2LotCheck)obj).CreateType.ToString();
        }

        /// <summary>
        /// 新增和保存等共用的编辑区域所填数据的检查
        /// </summary>
        /// <returns>是否检查通过</returns>
        protected override bool ValidateInput()
        {
            #region 使用工具类提供的检查
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblItemCode, txtItemCode, 40, true));
            manager.Add(new LengthCheck(lblLotPrefix, txtSNPrefix, 40, true));
            manager.Add(new LengthCheck(lblLotLength, txtSNLength, 6, true));
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            #endregion

            #region 自定义的检查
            if ((this.txtSNLength.Text.Trim().Length == 0 || Int32.Parse(this.txtSNLength.Text.Trim()) == 0)
                && this.txtSNPrefix.Text.Trim().Length == 0)
            {
                WebInfoPublish.Publish(this, "$LotLengthAndLotPrefix_Must_Input_One", this.languageComponent1);
                return false;
            }

            if (this.txtSNLength.Text.Trim().Length > 0
                && Int32.Parse(this.txtSNLength.Text.Trim()) > 0
                && this.txtSNPrefix.Text.Trim().Length > 0
                && this.txtSNPrefix.Text.Trim().Length >= int.Parse(this.txtSNLength.Text.Trim()))
            {
                WebInfoPublish.Publish(this, "$Error_LotLengthTooShort", this.languageComponent1);
                return false;
            }
            #endregion

            return true;
        }

        #endregion

        #region Export
        /// <summary>
        /// 将实体转为字符数组，从而最后输出到Excel
        /// </summary>
        /// <param name="obj">需要转换的实体</param>
        /// <returns>字符数组</returns>
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{     ((Item2LotCheckMP)obj).ItemCode.ToString(),
                                            ((Item2LotCheckMP)obj).ItemDesc.ToString(),
                                            ((Item2LotCheckMP)obj).SNPrefix.ToString(),
                                            ((Item2LotCheckMP)obj).SNLength.ToString(),
                                            this.languageComponent1.GetString(((Item2LotCheckMP)obj).CreateType.ToString()),
                                            ((Item2LotCheckMP)obj).MUser.ToString(),
                                            FormatHelper.ToDateString(((Item2LotCheckMP)obj).MaintainDate),
                                            FormatHelper.ToTimeString(((Item2LotCheckMP)obj).MaintainTime) }
                ;
        }

        /// <summary>
        /// 显示的列名，将会根据所填值应用多语言
        /// </summary>
        /// <returns>字符数组</returns>
        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"ItemCode",
                                            "ItemDesc",   
                                            "LotPrefix",
                                            "LotLength",
                                            "CreateType",
                                            "MUser",
                                            "MDate",
                                            "MTime"};
        }

        #endregion

    }
}
