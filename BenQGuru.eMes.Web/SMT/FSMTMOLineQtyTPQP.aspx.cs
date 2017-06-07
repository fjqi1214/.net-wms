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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FReelMP 的摘要说明。
	/// </summary>
	public partial class FSMTMOLineQtyTPQP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
        private SMTRptLineQtyTimePeriod qty;
		private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory(base.DataProvider).Create();
	
		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
            this.gridWebGrid.InitializeRow += new Infragistics.Web.UI.GridControls.InitializeRowEventHandler(gridWebGrid_InitializeRow);
		}
        void gridWebGrid_InitializeRow(object sender, Infragistics.Web.UI.GridControls.RowEventArgs e)
        {
            if (qty.ActualQty < qty.CurrentQty && qty.MaintainDate == 0)
            {
                DateTime dtEnd = Convert.ToDateTime(FormatHelper.ToDateString(qty.Day) + " " + FormatHelper.ToTimeString(qty.TPEndTime));
                if (dtEnd < DateTime.Now)
                {
                    e.Row.CssClass = "ForeColorRed";
                }
            }
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

				this.txtMOCodeQuery.Text = this.GetRequestParam("mocode");
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
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "ShiftDay", "工作天",	null);			
			this.gridHelper.AddColumn( "TimePeriodCode", "时段代码",	null);			
			this.gridHelper.AddColumn( "TimePeriodDescription", "时段描述",	null);
            this.gridHelper.AddColumn("TargetQty", "目标产量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("ActualQty", "实际产量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("DifferenceQty", "差异数量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("TPComPassRate", "时段达成率", HorizontalAlign.Right);
			this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			
           
			
			this.gridHelper.AddDefaultColumn( false, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
            if (!this.IsPostBack)
            {
                this.cmdQuery_Click(null, null);
            }
		}

        
		 
		protected override DataRow GetGridRow(object obj)
		{
            qty = (SMTRptLineQtyTimePeriod)obj;
     			
            DataRow row = this.DtSource.NewRow();
            row["MOCode"] = qty.MOCode;
            row["ItemCode"] = qty.ProductCode;
            row["ShiftDay"] = FormatHelper.ToDateString(qty.ShiftDay);
            row["TimePeriodCode"] = qty.TPCode;
            row["TimePeriodDescription"] = qty.TPDescription;
            row["TargetQty"] = String.Format("{0:#,#}",qty.CurrentQty);
            row["ActualQty"] = String.Format("{0:#,#}",qty.ActualQty);
            row["DifferenceQty"] = String.Format("{0:#,#}",qty.CurrentQty - qty.ActualQty);
            row["TPComPassRate"] = Math.Round(qty.TPComPassRate * 100, 2).ToString() + " %";
            row["MaintainUser"] = qty.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(qty.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(qty.MaintainTime);
            //if (qty.ActualQty < qty.CurrentQty && qty.MaintainDate == 0)
            //{
            //    DateTime dtEnd = Convert.ToDateTime(FormatHelper.ToDateString(qty.Day) + " " + FormatHelper.ToTimeString(qty.TPEndTime));
            //    if (dtEnd < DateTime.Now)
            //    {
            //        row.Style.ForeColor = Color.Red;
            //    }
            //}
			return row;
		}

        //protected override void cmdQuery_Click(object sender, EventArgs e)
        //{
        //    base.cmdQuery_Click(sender, e);
        //    foreach (GridRecord row in this.gridWebGrid.Rows)
        //    {
        //        if (qty.ActualQty < qty.CurrentQty && qty.MaintainDate == 0)
        //        {
        //            DateTime dtEnd = Convert.ToDateTime(FormatHelper.ToDateString(qty.Day) + " " + FormatHelper.ToTimeString(qty.TPEndTime));
        //            if (dtEnd < DateTime.Now)
        //            {
        //                row.CssClass = "ForeColorRed";
        //            }
        //        }
        //    }
        //    //base.cmdQuery_Click(sender, e);
        //}
		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryRptMOTimePeriodActualQty(
				this.txtMOCodeQuery.Text.Trim().ToUpper(),
				this.txtTPCodeQuery.Text.Trim().ToUpper(),
				inclusive, exclusive);
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryRptMOTimePeriodActualQtyCount( 
				this.txtMOCodeQuery.Text.Trim().ToUpper(),
				this.txtTPCodeQuery.Text.Trim().ToUpper());
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.UpdateSMTSensorQtyDifferenceReason(this.txtMOCode.Text, this.txtShiftDay.Text, this.txtTPCode.Text, this.txtDiffReason.Text, this.GetUserCode());
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			this.txtMOCode.ReadOnly = true;
			this.txtTPCode.ReadOnly = true;
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			return (new SMTSensorQty());
		}


		protected override object GetEditObject(GridRecord row)
		{
            //row.Style.ForeColor != Color.Red && 
            if (row.CssClass != "ForeColorRed" && row.Items.FindItemByKey("MaintainDate").Text == string.Empty)
				return null;
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
            object[] objs = _facade.GetSMTSensorQtyByTPCode(this.txtMOCodeQuery.Text, row.Items.FindItemByKey("TimePeriodCode").Text, FormatHelper.TODateInt(row.Items.FindItemByKey("ShiftDay").Text).ToString());
			
			if (objs != null && objs.Length > 0)
			{
				return (SMTSensorQty)objs[0];
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtMOCode.Text = "";
				this.txtTPCode.Text = "";
				this.txtDiffReason.Text = "";
				this.txtShiftDay.Text = "";

				return;
			}

			SMTSensorQty qty = (SMTSensorQty)obj;
			this.txtMOCode.Text = qty.MOCode;
			this.txtTPCode.Text = qty.TPCode;
			this.txtShiftDay.Text = qty.ShiftDay.ToString();
			this.txtDiffReason.Text = qty.DifferenceReason;
		}

		
		protected override bool ValidateInput()
		{
			if (this.txtMOCode.Text == string.Empty || this.txtTPCode.Text == string.Empty)
				return false;
			return true ;
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			SMTRptLineQtyTimePeriod qty = (SMTRptLineQtyTimePeriod)obj;
			return new string[]{ qty.MOCode,
								   qty.ProductCode,
								   FormatHelper.ToDateString(qty.ShiftDay),
								   qty.TPCode,
								   qty.TPDescription,
								   qty.CurrentQty.ToString(),
								   qty.ActualQty.ToString(),
								   (qty.CurrentQty - qty.ActualQty).ToString(),
								   Math.Round(qty.TPComPassRate * 100, 2).ToString() + " %",
								   qty.MaintainUser,
								   FormatHelper.ToDateString(qty.MaintainDate),
								   FormatHelper.ToTimeString(qty.MaintainTime)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"MOCode",
									"ItemCode",
									"ShiftDay",
									"TimePeriodCode",	
									"TimePeriodDescription",
									"TargetQty",
									"ActualQty",
									"DifferenceQty",
									"TPComPassRate",
									"MaintainUser",
									"MaintainDate",
									"MaintainTime"
								};
		}
		#endregion
	}
}
