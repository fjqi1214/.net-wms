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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// FReelMP 的摘要说明。
	/// </summary>
	public partial class FReelMP : BaseMPageNew
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		
		private BenQGuru.eMES.SMT.SMTFacade _facade ;//= new SMTFacadeFactory(base.DataProvider).Create();
	
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

			this.txtReelNoEdit.Attributes.Add("onkeydown", "if (event.keyCode==13){ ParseReelNo();return false; }");
			this.txtPartNoEdit.Attributes.Add("onkeydown", "if (event.keyCode==13){ ParsePartNo();return false; }");
			this.txtLotNoEdit.Attributes.Add("onkeydown", "if (event.keyCode==13){ document.getElementById('txtDateCodeEdit').focus();return false; }");
			//this.Enter2Tab(new WebControl[]{this.txtReelNoEdit, this.txtPartNoEdit, this.txtQtyEdit, this.txtLotNoEdit, this.txtDateCodeEdit}, false);
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			// TODO: 调整列的顺序及标题
            base.InitWebGrid();
			this.gridHelper.AddColumn( "ReelNo", "料卷号码",	null);
			this.gridHelper.AddColumn( "PartNo", "物料号",	null);
            this.gridHelper.AddColumn("Qty", "数量", HorizontalAlign.Right);			
			this.gridHelper.AddColumn( "LotNo", "生产批号",	null);			
			this.gridHelper.AddColumn( "DateCode", "生产日期",	null);
            this.gridHelper.AddColumn("ReelLeftQty", "剩余数量", HorizontalAlign.Right);
			this.gridHelper.AddColumn( "UseFlag", "是否领用",	null);
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "sscode", "产线代码",	null);
			this.gridHelper.AddColumn( "IsSpecial", "是否特采",	null);
			this.gridHelper.AddColumn( "Memo", "备注",	null);
            //(this.gridWebGrid.Columns.FromKey("Qty") as BoundDataField).DataFormatString = "{0:#}";
            //this.gridWebGrid.Columns.FromKey("Qty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
            //this.gridWebGrid.Columns.FromKey("ReelLeftQty").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("ReelLeftQty").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
			Reel reel = (Reel)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    reel.ReelNo,
            //                    reel.PartNo,
            //                    reel.Qty,
            //                    reel.LotNo,
            //                    reel.DateCode,
            //                    reel.Qty - reel.UsedQty,
            //                    FormatHelper.StringToBoolean(reel.UsedFlag).ToString(),
            //                    reel.MOCode,
            //                    reel.StepSequenceCode,
            //                    FormatHelper.StringToBoolean(reel.IsSpecial).ToString(),
            //                    reel.Memo,
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["ReelNo"] = reel.ReelNo;
            row["PartNo"] = reel.PartNo;
            row["Qty"] = String.Format("{0:#,#}",reel.Qty);
            row["LotNo"] = reel.LotNo;
            row["DateCode"] = reel.DateCode;
            row["ReelLeftQty"] = String.Format("{0:#,#}",reel.Qty - reel.UsedQty);
            row["UseFlag"] = FormatHelper.StringToBoolean(reel.UsedFlag).ToString();
            row["MOCode"] = reel.MOCode;
            row["sscode"] = reel.StepSequenceCode;
            row["IsSpecial"] = FormatHelper.StringToBoolean(reel.IsSpecial).ToString();
            row["Memo"] = reel.Memo;
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryReel( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReelNoQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPartNoQuery.Text)),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryReelCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSCodeQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReelNoQuery.Text)),
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPartNoQuery.Text )));
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.AddReel( (Reel)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.DeleteReel( (Reel[])domainObjects.ToArray( typeof(Reel) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if (FormatHelper.StringToBoolean(((Reel)domainObject).UsedFlag) == true)
				throw new Exception("$SMT_Prepare_Reel_Used_Already");
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.UpdateReel( (Reel)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtReelNoEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtReelNoEdit.ReadOnly = true;
			}
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			Reel reel = this._facade.CreateNewReel();

			reel.ReelNo	 = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtReelNoEdit.Text, 40));
			Reel reelTmp = (Reel)_facade.GetReel(reel.ReelNo);
			if (reelTmp != null)
				reel = reelTmp;
			reel.PartNo = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtPartNoEdit.Text, 40));
			reel.Qty = Convert.ToDecimal(this.txtQtyEdit.Text);
			reel.LotNo = this.txtLotNoEdit.Text.Trim().ToUpper();
			reel.DateCode = this.txtDateCodeEdit.Text.Trim().ToUpper();
			reel.MaintainUser = this.GetUserCode();
			reel.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
			reel.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

			return reel;
		}


		protected override object GetEditObject(GridRecord row)
		{	
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
            object obj = _facade.GetReel(row.Items.FindItemByKey("ReelNo").Text.ToString());
			
			if (obj != null)
			{
				return (Reel)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtReelNoEdit.Text	= "";
				this.txtPartNoEdit.Text = "";
				this.txtQtyEdit.Text = "0";
				this.txtLotNoEdit.Text = "";
				this.txtDateCodeEdit.Text = "";

				return;
			}

			Reel reel = (Reel)obj;
			this.txtReelNoEdit.Text = reel.ReelNo;
			this.txtPartNoEdit.Text = reel.PartNo;
			this.txtQtyEdit.Text = reel.Qty.ToString();
			this.txtLotNoEdit.Text = reel.LotNo;
			this.txtDateCodeEdit.Text = reel.DateCode;

		}

		
		protected override bool ValidateInput()
		{

            PageCheckManager manager = new PageCheckManager();

            manager.Add( new LengthCheck(lblReelNoEdit, txtReelNoEdit, 40, true) );
            manager.Add( new LengthCheck(lblPartNoEdit,txtPartNoEdit,40,true)) ;
            manager.Add( new DecimalCheck(lblReelQtyEdit, txtQtyEdit, 0, decimal.MaxValue, true));

            if ( !manager.Check() )
            {
                WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
                return false;
            }

            return true ;



		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			Reel reel = (Reel)obj;
			return new string[]{ reel.ReelNo,
								   reel.PartNo,
								   reel.Qty.ToString(),
								   reel.LotNo,
								   reel.DateCode,
								   (reel.Qty - reel.UsedQty).ToString(),
								   FormatHelper.StringToBoolean(reel.UsedFlag).ToString(),
								   reel.MOCode,
								   reel.StepSequenceCode,
								   FormatHelper.StringToBoolean(reel.IsSpecial).ToString(),
								   reel.Memo
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"ReelNo",
									"PartNo",
									"Qty",
									"LotNo",	
									"DateCode",
									"ReelLeftQty",
									"UseFlag",
									"MOCode",
									"sscode",
									"IsSpecial",
									"Memo"
								};
		}
		#endregion


	}
}
