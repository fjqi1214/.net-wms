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
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using System.IO;
using BenQGuru.eMES.Common;
using System.Text;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FPauseHistoryQuery : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private PauseFacade facade = null;

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

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        protected void drpPauseStatus_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                this.drpPauseStatus.Items.Insert(0, new ListItem("", ""));

                this.drpPauseStatus.Items.Insert(1, new ListItem(languageComponent1.GetString(PauseStatus.status_pause), PauseStatus.status_pause));

                this.drpPauseStatus.Items.Insert(2, new ListItem(languageComponent1.GetString(PauseStatus.status_cancel), PauseStatus.status_cancel));

                this.drpPauseStatus.SelectedIndex = 0;
            }
        }


        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("PauseCode", "停发通知单", null);
            this.gridHelper.AddLinkColumn("DetailsMore", "详细", null);
            this.gridHelper.AddLinkColumn("Download", "下载", "tdDownLoad");
            this.gridHelper.AddColumn("PauseDate", "停发日期", null);
            this.gridHelper.AddColumn("PauseUser", "停发人员", null);
            this.gridHelper.AddColumn("PauseCount", "停发数量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("CancelPauseCount", "解除停发数量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("PauseStatus", "停发状态", null);
            this.gridHelper.AddColumn("CancelPauseUser", "解除人员", null);
            this.gridHelper.AddColumn("CancelPauseDate", "解除日期", null);

            this.gridHelper.AddDefaultColumn(false, false);

            //this.gridHelper.Grid.Bands[0].Columns.FromKey("PauseCount").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridHelper.Grid.Bands[0].Columns.FromKey("CancelPauseCount").CellStyle.HorizontalAlign = HorizontalAlign.Right;

            //this.gridWebGrid.Columns.FromKey("Download").CellButtonStyle.BackgroundImage = this.Request.Url.Segments[0] + this.Request.Url.Segments[1] + "skin/image/Print.gif";
            //this.gridWebGrid.Columns.FromKey("Download").CellStyle.BackgroundImage = this.Request.Url.Segments[0] + this.Request.Url.Segments[1] + "skin/image/Print.gif";

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["PauseCode"] = ((PauseQuery)obj).PauseCode.ToString();
            row["PauseDate"] = FormatHelper.ToDateString(((PauseQuery)obj).PDate);
            row["PauseUser"] = ((PauseQuery)obj).GetDisplayText("PUser");
            row["PauseCount"] = ((PauseQuery)obj).PauseQty.ToString();
            row["CancelPauseCount"] = ((PauseQuery)obj).CancelQty.ToString();
            row["PauseStatus"] = languageComponent1.GetString(((PauseQuery)obj).Status.ToString());
            row["CancelPauseUser"] = ((PauseQuery)obj).GetDisplayText("CancelUser");
            row["CancelPauseDate"] = FormatHelper.ToDateString(((PauseQuery)obj).CancelDate);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new PauseFacade(base.DataProvider);
            }
            return this.facade.QueryHistory(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPauseCodeQuery.Text)),
                this.drpPauseStatus.SelectedValue,
                this.txtItemCodeEdit.Text,
                FormatHelper.CleanString(this.txtBOMVersionQuery.Text),
                FormatHelper.TODateInt(this.datePauseStartDateQuery.Text),
                FormatHelper.TODateInt(this.datePauseEndDateQuery.Text),
                inclusive, exclusive);

        }


        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new PauseFacade(base.DataProvider);
            }
            return this.facade.QueryHistoryCount(
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPauseCodeQuery.Text)),
                this.drpPauseStatus.SelectedValue,
                this.txtItemCodeEdit.Text,
                FormatHelper.CleanString(this.txtBOMVersionQuery.Text),
                FormatHelper.TODateInt(this.datePauseStartDateQuery.Text),
                FormatHelper.TODateInt(this.datePauseEndDateQuery.Text)
            );

        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            //base.Grid_ClickCell(cell);

            if (commandName == "DetailsMore")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FPauseSequenceQueryNew.aspx",
                    new string[] { "storageCode", "stackCode", "palletCode", "itemCode", "pauseCode", "cancelCode" },
                    new string[] { "", "", "",
                        "", row.Items.FindItemByKey("PauseCode").Value.ToString().Trim(),"" }));
            }

            if (commandName == "Download")
            {
                string pauseCode = row.Items.FindItemByKey("PauseCode").Value.ToString();
                string pauseQty = row.Items.FindItemByKey("PauseCount").Value.ToString();

                if (facade == null)
                {
                    facade = new PauseFacade(base.DataProvider);
                }
                object[] pauseObj = facade.QueryPauseSequence("", "", "", "", pauseCode, "", false, 0, 100000);
                if (pauseObj == null)
                {
                    ExceptionManager.Raise(this.GetType(), "停发通知单不存在");
                }

                PauseQuery pauseQuery = pauseObj[0] as PauseQuery;

                string originalFilePath = this.Request.PhysicalApplicationPath + @"download\PauseCode.htm";
                if (!File.Exists(originalFilePath))
                {
                    ExceptionManager.Raise(this.GetType(), "文件[PauseCode.htm]不存在");
                }

                string fileContent = File.ReadAllText(originalFilePath, Encoding.GetEncoding("GB2312"));
                fileContent = fileContent.Replace("$$PauseCode$$", pauseCode);
                fileContent = fileContent.Replace("$$ModelCode$$", pauseQuery.MModelCode == "" ? " " : pauseQuery.MModelCode);
                fileContent = fileContent.Replace("$$BOMVersion$$", pauseQuery.BOM == "" ? " " : pauseQuery.BOM);
                fileContent = fileContent.Replace("$$ItemCode$$", pauseQuery.MCode == "" ? " " : pauseQuery.MCode);
                fileContent = fileContent.Replace("$$ItemDescription$$", pauseQuery.MDesc == "" ? " " : pauseQuery.MDesc);
                fileContent = fileContent.Replace("$$PauseQty$$", pauseQty == "" ? " " : pauseQty);
                fileContent = fileContent.Replace("$$PauseReason$$", pauseQuery.PauseReason == "" ? " " : pauseQuery.PauseReason);

                string downloadPhysicalPath = this.Request.PhysicalApplicationPath + @"upload\";
                if (!Directory.Exists(downloadPhysicalPath))
                {
                    Directory.CreateDirectory(downloadPhysicalPath);
                }

                string filename = string.Format("{0}_{1}_{2}", row.Items.FindItemByKey("PauseCode").Value.ToString(), FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString());
                string filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".xls");

                while (File.Exists(filepath))
                {
                    filename = string.Format("{0}_{1}", filename, "0");
                    filepath = string.Format(@"{0}{1}{2}", downloadPhysicalPath, filename, ".xls");
                }

                StreamWriter writer = new StreamWriter(filepath, false, System.Text.Encoding.GetEncoding("GB2312"));
                writer.Write(fileContent);
                writer.Flush();
                writer.Close();

                this.DownloadFile(filename);

            }
        }



        #endregion

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{((PauseQuery)obj).PauseCode.ToString(),
                                 FormatHelper.ToDateString(((PauseQuery)obj).PDate),
                                //((PauseQuery)obj).PUser.ToString(),  
                             ((PauseQuery)obj).GetDisplayText("PUser"),
                                ((PauseQuery)obj).PauseQty.ToString(),
                                ((PauseQuery)obj).CancelQty.ToString(),
                                languageComponent1.GetString(((PauseQuery)obj).Status.ToString()),
                                //((PauseQuery)obj).CancelUser.ToString(),
                                  ((PauseQuery)obj).GetDisplayText("CancelUser"),
                                FormatHelper.ToDateString(((PauseQuery)obj).CancelDate)};

        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"PauseCode",
                                    "PauseDate",
                                    "PauseUser",
                                    "PauseCount",
                                    "CancelPauseCount",	
                                    "PauseStatus",
                                    "CancelPauseUser",	
                                    "CancelPauseDate"};
        }

        #endregion
    }
}
