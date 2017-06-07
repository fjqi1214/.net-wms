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
	/// FReelValidityMP 的摘要说明。
	/// </summary>
	public partial class FReelValidityMP : BaseMPageNew
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
			this.gridHelper.AddColumn( "MaterialPrefix", "物料代码起始字符串",	null);
            this.gridHelper.AddColumn("ValidityMonth", "有效期限", HorizontalAlign.Right);
			this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);			
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);			
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			//this.gridWebGrid.Columns.FromKey("ValidityMonth").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
			ReelValidity validity = (ReelValidity)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    validity.MaterialPrefix,
            //                    validity.ValidityMonth,
            //                    validity.MaintainUser,
            //                    FormatHelper.ToDateString(validity.MaintainDate),
            //                    FormatHelper.ToTimeString(validity.MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["MaterialPrefix"] = validity.MaterialPrefix;
            row["ValidityMonth"] = String.Format("{0:#}",validity.ValidityMonth);
            row["MaintainUser"] = validity.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(validity.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(validity.MaintainTime);
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryReelValidity( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialPrefixQuery.Text)),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryReelValidityCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialPrefixQuery.Text)));
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.AddReelValidity( (ReelValidity)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.DeleteReelValidity( (ReelValidity[])domainObjects.ToArray( typeof(ReelValidity) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.UpdateReelValidity( (ReelValidity)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtMaterialPrefixEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtMaterialPrefixEdit.ReadOnly = true;
			}
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			ReelValidity validity = this._facade.CreateNewReelValidity();

			validity.MaterialPrefix	 = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialPrefixEdit.Text, 40));
			validity.ValidityMonth = decimal.Parse(this.txtValidityMonthEdit.Text);
			validity.MaintainUser = this.GetUserCode();
			validity.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
			validity.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

			return validity;
		}


		protected override object GetEditObject(GridRecord row)
		{	
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
            object obj = _facade.GetReelValidity(row.Items.FindItemByKey("MaterialPrefix").Text.ToString());
			
			if (obj != null)
			{
				return (ReelValidity)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtMaterialPrefixEdit.Text	= "";
				this.txtValidityMonthEdit.Text = "";

				return;
			}

			ReelValidity validity = (ReelValidity)obj;
			this.txtMaterialPrefixEdit.Text = validity.MaterialPrefix;
            this.txtValidityMonthEdit.Text = String.Format("{0:#}", validity.ValidityMonth);

		}

		
		protected override bool ValidateInput()
		{

            PageCheckManager manager = new PageCheckManager();

            manager.Add( new LengthCheck(lblMaterialPrefixEdit, txtMaterialPrefixEdit, 40, true) );
			manager.Add( new DecimalCheck(lblValidityMonthEdit, txtValidityMonthEdit, 0, decimal.MaxValue, true));

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
			ReelValidity validity = (ReelValidity)obj;
			return new string[]{ validity.MaterialPrefix,
								   validity.ValidityMonth.ToString(),
								   validity.MaintainUser,
								   FormatHelper.ToDateString(validity.MaintainDate),
								   FormatHelper.ToTimeString(validity.MaintainTime)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"MaterialPrefix",
									"ValidityMonth",
									"MaintainUser",
									"MaintainDate",	
									"MaintainTime" };
		}
		#endregion


	}
}
