using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.MOModel;
using System.Collections.Generic;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.BaseSetting;
using System.Text;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialNeed : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.Material.MaterialFacade _Facade = null;//new BaseModelFacadeFactory().Create();
        private BenQGuru.eMES.MOModel.ItemFacade _ItemFacade = null;


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
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\BenQGuru.eMES.Web\\";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                BuildOrgList();

            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void drpFirstClass_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);

                DropDownListBuilder builder = new DropDownListBuilder(this.drpFirstClassQuery);

                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(itemFacade.GetItemFirstClass);

                builder.Build("FirstClass", "FirstClass");

                this.drpFirstClassQuery.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void drpFirstClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string firstClass = this.drpFirstClassQuery.SelectedValue;

            this.drpSecondClassQuery.Items.Clear();
            this.drpThirdClassQuery.Items.Clear();

            if (firstClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object[] itemClassList = itemFacade.GetItemSecondClass(firstClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.drpSecondClassQuery.Items.Add(new ListItem(itemClass.SecondClass, itemClass.SecondClass));
                    }
                }
            }

            this.drpSecondClassQuery.Items.Insert(0, new ListItem("", ""));
        }

        protected void drpSecondClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string firstClass = this.drpFirstClassQuery.SelectedValue;
            string secondClass = this.drpSecondClassQuery.SelectedValue;

            this.drpThirdClassQuery.Items.Clear();

            if (firstClass.Trim().Length > 0 && secondClass.Trim().Length > 0)
            {
                ItemFacade itemFacade = new ItemFacade(this.DataProvider);
                object[] itemClassList = itemFacade.GetItemThirdClass(firstClass, secondClass);
                if (itemClassList != null)
                {
                    foreach (ItemClass itemClass in itemClassList)
                    {
                        this.drpThirdClassQuery.Items.Add(new ListItem(itemClass.ThirdClass, itemClass.ThirdClass));
                    }
                }
            }

            this.drpThirdClassQuery.Items.Insert(0, new ListItem("", ""));
        }

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

        private bool checkItemCode()
        {
            bool result = true;
            this._ItemFacade = new ItemFacade(this.DataProvider);
            Domain.MOModel.Material material = (Domain.MOModel.Material)_ItemFacade.GetMaterial(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text)), int.Parse(this.DropDownListOrg.SelectedValue));

            if (material == null)
                result = false;
            return result;
        }
        #endregion

        #region Format Data
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("WarehouseItemCode", "物料代码", null);
            this.gridHelper.AddColumn("MaterialDesc", "物料描述", null);
            this.gridHelper.AddColumn("RequestQTY", "需求标准数量", null);
            this.gridHelper.AddColumn("OrganizationID", "组织", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridWebGrid.Columns.FromKey("OrganizationID").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainUser").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainDate").Hidden = true;
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;


            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								((MaterialReqStdWithItemDesc)obj).ItemCode.ToString(),
								((MaterialReqStdWithItemDesc)obj).ItemDesc.ToString(),
								((MaterialReqStdWithItemDesc)obj).RequestQTY.ToString(),
                                ((MaterialReqStdWithItemDesc)obj).OrganizationID.ToString(),

                                ((MaterialReqStdWithItemDesc)obj).GetDisplayText("MaintainUser"),
								FormatHelper.ToDateString(((MaterialReqStdWithItemDesc)obj).MaintainDate),
								FormatHelper.ToTimeString(((MaterialReqStdWithItemDesc)obj).MaintainTime),
								});
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((MaterialReqStdWithItemDesc)obj).ItemCode.ToString(),
								   ((MaterialReqStdWithItemDesc)obj).OrganizationID.ToString(),
                		           ((MaterialReqStdWithItemDesc)obj).RequestQTY.ToString()
                                            
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"WarehouseItemCode",
                                    "OrganizationID",
								//	"MaterialDesc",	
									"RequestQTY"									
                                   
            };
        }

       

        #endregion

        #region  WebGrid
        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            return this._Facade.QueryMaterialReqStd(
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialCodeQuery.Text)),
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(drpFirstClassQuery.SelectedValue)),
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(drpSecondClassQuery.SelectedValue)),
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(drpThirdClassQuery.SelectedValue)),
                                    inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            return this._Facade.QueryMaterialReqStdCount(
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialCodeQuery.Text)),
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(drpFirstClassQuery.SelectedValue)),
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(drpSecondClassQuery.SelectedValue)),
                                    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(drpThirdClassQuery.SelectedValue))
                 );
        }
        #endregion


        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            this._Facade.AddMaterialReqStd((MaterialReqStd)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            this._Facade.DeleteMaterialReqStd((MaterialReqStd[])domainObjects.ToArray(typeof(MaterialReqStd)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            this._Facade.UpdateMaterialReqStd((MaterialReqStd)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtItemCodeEdit.Readonly = false;
                //this.DropDownListOrg.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtItemCodeEdit.Readonly = true;
                //this.DropDownListOrg.Enabled = false;
            }
        }

        protected void cmdMOExport_ServerClick(object sender, System.EventArgs e)
        {
            string downloadPhysicalPath = this.Request.PhysicalApplicationPath + @"upload\";
            if (!Directory.Exists(downloadPhysicalPath))
            {
                Directory.CreateDirectory(downloadPhysicalPath);
            }

            string filename = string.Format("{0}_{1}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString());
            string filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".csv");

            while (File.Exists(filepath))
            {
                filename = string.Format("{0}_{1}", filename, "0");
                filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".csv");
            }

            StreamWriter writer = new StreamWriter(filepath, false, System.Text.Encoding.GetEncoding("GB2312"));
            writer.WriteLine("物料代码" + "," + "物料描述" + "," + "需求标准数量");

            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            ArrayList array = this.gridHelper.GetCheckedRows();
            if (array.Count > 0)
            {
                ArrayList objs = new ArrayList(array.Count);

                foreach (UltraGridRow row in array)
                {
                        writer.WriteLine(ReplaceStr(row.Cells[1].Text.Trim()) + ","
                          + ReplaceStr(row.Cells[2].Text.Trim()) + "," + ReplaceStr(row.Cells[3].ToString()));
                }

            }

            writer.Flush();
            writer.Close();

            this.DownloadFile(filename);
        }

        protected string ReplaceStr(string str)
        {
            string newStr = "\"" + str.Replace("\"", "\"\"") + "\"";

            return newStr;
        }

        protected void cmdImport_ServerClick(object sender, System.EventArgs e)
        {
            //Response.Redirect(this.MakeRedirectUrl("./FMaterialNeedDataImport.aspx"));
            Response.Redirect(this.MakeRedirectUrl("./ImportData/FExcelDataImp.aspx?itype=MATERIALNEED"));
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            MaterialReqStd MaterialReqStd = this._Facade.CreateNewMaterialReqStd();

            MaterialReqStd.ItemCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text));
            MaterialReqStd.RequestQTY = int.Parse(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtQTY.Text)));
            MaterialReqStd.MaintainUser = this.GetUserCode();
            MaterialReqStd.OrganizationID = int.Parse(this.DropDownListOrg.SelectedValue);
            return MaterialReqStd;
        }
        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtItemCodeEdit.Text = "";
                this.txtQTY.Text = "0";
                this.DropDownListOrg.SelectedIndex = 0;
               
                return;
            }

            this.txtItemCodeEdit.Text = ((MaterialReqStd)obj).ItemCode;
            this.txtQTY.Text =((MaterialReqStd)obj).RequestQTY.ToString();
            try
            {
                this.DropDownListOrg.SelectedValue = ((MaterialReqStd)obj).OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }          
        }

        protected override bool ValidateInput()
        {
           
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new LengthCheck(lblItemIDEdit, txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblOrgEdit, DropDownListOrg, 8, true));
            manager.Add(new NumberCheck(this.lblQty, this.txtQTY, 0, 9999999999999, true));
            //if (this.txtQTY.Text.Trim().Length > 0)
            //{
            //    manager.Add(new NumberCheck(this.lblQty, this.txtQTY, 0, 9999999999999, false));
            //}

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (!checkItemCode())
            {
                WebInfoPublish.Publish(this, "$Error_CS_No_OPBOMDetail", languageComponent1);
                txtItemCodeEdit.Focus();
                return false;
            }


            return true;
        }

        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            object obj = _Facade.GetMaterialReqStd(row.Cells[1].Text.ToString(), int.Parse(row.Cells[4].Text.ToString()));

            if (obj != null)
            {
                return (MaterialReqStd)obj;
            }

            return null;
        }

        #endregion

   
    }
}
