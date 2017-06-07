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
	/// FFeederSpecMP 的摘要说明。
	/// </summary>
	public partial class FFeederSpecMP : BaseMPageNew
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
			// TODO: 调整列的顺序及标题
            base.InitWebGrid();
			this.gridHelper.AddColumn( "FeederSpecCode", "Feeder规格代码",	null);
			this.gridHelper.AddColumn( "FeederSpecName", "Feeder规格名称",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护人员",	null);			
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);			
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			
			this.gridHelper.AddDefaultColumn( true, true );
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		
		protected override DataRow GetGridRow(object obj)
		{
			FeederSpec feederSpec = (FeederSpec)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
            //    new object[]{"false",
            //                    feederSpec.FeederSpecCode,
            //                    feederSpec.Name,
            //                    feederSpec.MaintainUser,
            //                    FormatHelper.ToDateString(feederSpec.MaintainDate),
            //                    FormatHelper.ToTimeString(feederSpec.MaintainTime),
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["FeederSpecCode"] = feederSpec.FeederSpecCode;
            row["FeederSpecName"] = feederSpec.Name;
            row["MaintainUser"] = feederSpec.MaintainUser;
            row["MaintainDate"] = FormatHelper.ToDateString(feederSpec.MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(feederSpec.MaintainTime);
            return row;
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryFeederSpec( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFeederSpecCodeQuery.Text)),
				inclusive, exclusive );
		}


		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			return this._facade.QueryFeederSpecCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFeederSpecCodeQuery.Text)));
		}

		#endregion

		#region Button
		protected override void AddDomainObject(object domainObject)
		{		
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.AddFeederSpec( (FeederSpec)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.DeleteFeederSpec( (FeederSpec[])domainObjects.ToArray( typeof(FeederSpec) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			this._facade.UpdateFeederSpec( (FeederSpec)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtFeederSpecCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtFeederSpecCodeEdit.ReadOnly = true;
			}
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
			FeederSpec feederSpec = this._facade.CreateNewFeederSpec();

			feederSpec.FeederSpecCode	 = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFeederSpecCodeEdit.Text, 40));
			feederSpec.Name = txtName.Text;
			feederSpec.MaintainUser = this.GetUserCode();
			feederSpec.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
			feederSpec.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

			return feederSpec;
		}


		protected override object GetEditObject(GridRecord row)
		{	
			if(_facade==null){_facade = new SMTFacadeFactory(base.DataProvider).Create();}
            object obj = _facade.GetFeederSpec(row.Items.FindItemByKey("FeederSpecCode").Text.ToString());
			
			if (obj != null)
			{
				return (FeederSpec)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtFeederSpecCodeEdit.Text	= "";
				this.txtName.Text = "";

				return;
			}

			FeederSpec feederSpec = (FeederSpec)obj;
			this.txtFeederSpecCodeEdit.Text = feederSpec.FeederSpecCode;
			this.txtName.Text = feederSpec.Name;;

		}

		
		protected override bool ValidateInput()
		{

            PageCheckManager manager = new PageCheckManager();

            manager.Add( new LengthCheck(lblFeederSpecSNEdit, txtFeederSpecCodeEdit, 40, true) );

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
			FeederSpec feederSpec = (FeederSpec)obj;
			return new string[]{ feederSpec.FeederSpecCode,
								   feederSpec.Name,
								   feederSpec.MaintainUser,
								   FormatHelper.ToDateString(feederSpec.MaintainDate),
								   FormatHelper.ToTimeString(feederSpec.MaintainTime)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	"FeederSpecCode",
									"FeederSpecName",
									"MaintainUser",
									"MaintainDate",	
									"MaintainTime" };
		}
		#endregion


	}
}
