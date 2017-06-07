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
using BenQGuru.eMES.Web.Helper;

using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.SAPDataTransfer;
using BenQGuru.eMES.SAPDataTransferInterface;
using System.Collections.Generic;

using Infragistics.WebUI.UltraWebGrid;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialTrans : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private InventoryFacade _facade = null;
        private MaterialFacade _Facade = null;

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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitUI();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        #region WebGrid

        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("PlanDateFrom", "计划日期", null);
            this.gridHelper.AddColumn("BIGSSCODE", "大线", null);
            this.gridHelper.AddColumn("PLANSEQ", "生产顺序", null);
            this.gridHelper.AddColumn("MOCODE", "工单", null);
            this.gridHelper.AddColumn("MOSEQ", "工单项次", null);
            this.gridHelper.AddColumn("MaterialPlanQty", "计划投入产量", null);
            this.gridHelper.AddColumn("PLANSTARTTIME", "计划开始时间", null);
            this.gridHelper.AddColumn("ACTIONSTATUS", "执行状态", null);
            this.gridHelper.AddColumn("MATERIALSTATUS", "配料状态", null);
            this.gridHelper.AddColumn("MaterialQty", "实际拥有物料", null);
            this.gridHelper.AddColumn("LactQty", "缺料数量", null);
            this.gridHelper.AddColumn("ITEMCODE", "物料代码", null);
            this.gridHelper.AddLinkColumn("SyncStatus", "物料移转", null);


            this.gridWebGrid.Columns.FromKey("ITEMCODE").Hidden = true;

            this.gridHelper.AddDefaultColumn(false, false);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override void cmdQuery_Click(object sender, EventArgs e)
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new DateCheck(this.lblPlanDateFromto,this.dateVoucherDateFrom.Text,true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return;
            }

            base.cmdQuery_Click(sender, e);
        }

        protected override int GetRowCount()
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            return this._Facade.QueryMaterialTransCount(                                  
                                                        FormatHelper.TODateInt(DateTime.Now),
                                                        FormatHelper.TODateInt(FormatHelper.CleanString(dateVoucherDateFrom.Text))
                                                        );
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            };

            return this._Facade.QueryMaterialTrans(
                                      FormatHelper.TODateInt(DateTime.Now),
                                      FormatHelper.TODateInt(FormatHelper.CleanString(dateVoucherDateFrom.Text)),                                     
                                      inclusive, exclusive);

        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
								((WorkPlanWithQty)obj).PlanDate.ToString(),
								((WorkPlanWithQty)obj).BigSSCode.ToString(),
								((WorkPlanWithQty)obj).PlanSeq.ToString(),
                                ((WorkPlanWithQty)obj).MoCode.ToString(),
                                ((WorkPlanWithQty)obj).MoSeq.ToString(),
								((WorkPlanWithQty)obj).PlanQty.ToString(),
								FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PlanStartTime),
                                this.languageComponent1.GetString(((WorkPlanWithQty)obj).ActionStatus.ToString()),
                                this.languageComponent1.GetString(((WorkPlanWithQty)obj).MaterialStatus.ToString()),
                                ((WorkPlanWithQty)obj).MaterialQty.ToString(),
                                ((WorkPlanWithQty)obj).LackQTY.ToString(),
                                ((WorkPlanWithQty)obj).ItemCode.ToString(),
                                ""          
								});
        }

        protected override void Grid_ClickCell(UltraGridCell cell)
        {
            string userCode = this.GetUserCode();
            base.Grid_ClickCell(cell);
            if (this.gridHelper.IsClickColumn("SyncStatus", cell))
            {
                Response.Redirect(this.MakeRedirectUrl("FMaterialTransDetail.aspx", new string[] { "planDate", "bigSSCode", "moCode", "moSeq", "itemCode", "lactQty" }, 
                    new string[] { 
                                cell.Row.Cells[0].ToString(), 
                                cell.Row.Cells[1].ToString(), 
                                cell.Row.Cells[3].ToString(), 
                                cell.Row.Cells[4].ToString(),
                                cell.Row.Cells[11].ToString(),
                                cell.Row.Cells[10].ToString() 
                    }));               
            }
        }

        #endregion

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"PlanDateFrom",
									"BIGSSCODE",	
									"PLANSEQ",
				                    "MOCODE",
									"MOSEQ",	
									"MaterialPlanQty",
				                    "PLANSTARTTIME",
									"ACTIONSTATUS",	
									"MATERIALSTATUS",
                                    "MaterialQty" ,
                                     "LactQty"           
            };
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{ ((WorkPlanWithQty)obj).PlanDate.ToString(),
								((WorkPlanWithQty)obj).BigSSCode.ToString(),
								((WorkPlanWithQty)obj).PlanSeq.ToString(),
                                ((WorkPlanWithQty)obj).MoCode.ToString(),
                                ((WorkPlanWithQty)obj).MoSeq.ToString(),
								((WorkPlanWithQty)obj).PlanQty.ToString(),
								FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PlanStartTime),
                                this.languageComponent1.GetString(((WorkPlanWithQty)obj).ActionStatus.ToString()),
                                this.languageComponent1.GetString(((WorkPlanWithQty)obj).MaterialStatus.ToString()),
                                ((WorkPlanWithQty)obj).MaterialQty.ToString(),
                                ((WorkPlanWithQty)obj).LackQTY.ToString()
                                            
            };
        }
    }
}
