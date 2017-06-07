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

using Infragistics.WebUI.UltraWebGrid ;
using Infragistics.WebUI.UltraWebNavigator ;
using Infragistics.WebUI.Shared ;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting ;
using BenQGuru.eMES.Domain.MOModel ;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Web.SMT
{

    /// <summary>
    /// FSMTLoadingMP 的摘要说明。
    /// </summary>
    public partial class FSMTMachineDiscardImpSP : BaseMPage
    {
    
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.SMT.SMTFacade _facade;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox Checkbox1;


		#region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

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
            string strMessage = this.languageComponent1.GetString("$Message_SMTLoading_DataOverwritten");
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
			this.gridHelper.AddColumn( "MOCode", "工单代码",	null);
			this.gridHelper.AddColumn( "sscode", "产线代码",	null);
			this.gridHelper.AddColumn( "MaterialCode", "物料代码",	null);
			this.gridHelper.AddColumn( "MachineStationCode", "站位编号",	null);
			this.gridHelper.AddColumn( "PickupCount", "Pickup Count",	null);
			this.gridHelper.AddColumn( "RejectParts", "Reject Parts",	null);
			this.gridHelper.AddColumn( "NoPickup", "No Pickup",	null);
			this.gridHelper.AddColumn( "SMTErrorParts", "Error Parts",	null);
			this.gridHelper.AddColumn( "DislodgedParts", "Dislodged Parts",	null);
			this.gridWebGrid.Columns.FromKey("PickupCount").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("PickupCount").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("RejectParts").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("RejectParts").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("NoPickup").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("NoPickup").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("SMTErrorParts").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("SMTErrorParts").CellStyle.HorizontalAlign = HorizontalAlign.Right;
			this.gridWebGrid.Columns.FromKey("DislodgedParts").Format = "#,#";
			this.gridWebGrid.Columns.FromKey("DislodgedParts").CellStyle.HorizontalAlign = HorizontalAlign.Right;

            this.gridHelper.AddDefaultColumn( false, false );
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        #endregion


		#region Import
		private object[] items;
		protected void cmdView_ServerClick(object sender, System.EventArgs e)
		{
			string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page,this.fileInit,null);
			if(fileName == null)
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
			}
			if(!fileName.ToLower().EndsWith(".csv"))
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileTypeError");
			}

			this.ViewState.Add("UploadedFileName",fileName);
			this.cmdQuery_Click(null, null);
			this.cmdEnter.Disabled = true;
			if (this.gridWebGrid.Rows.Count > 0)
			{
				this.cmdEnter.Disabled = false;
			}
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			ArrayList objs = new ArrayList() ;
			if(items == null)
			{
				this.GetAllItem();
			}
			for(int i=1;i<=items.Length;i++)
			{
				if(i>=inclusive && i<=exclusive)
				{
					objs.Add( items[i-1] );
				}
			}

			return objs.ToArray() ;

		}
		protected override int GetRowCount()
		{
			if(items == null)
			{
				this.GetAllItem();
			}
			return items.Length;
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			SMTMachineDiscard item = (SMTMachineDiscard)obj;
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{
								item.MOCode,
								item.StepSequenceCode,
								item.MaterialCode,
								item.MachineStationCode,
								item.PickupCount,
								item.RejectParts,
								item.NoPickup,
								item.ErrorParts,
								item.DislodgedParts
							});
		}
		
		private object[] GetAllItem()
		{
			try
			{
				string fileName = string.Empty ;

				if (this.ViewState["UploadedFileName"] == null)
				{
					BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
				}
				fileName = this.ViewState["UploadedFileName"].ToString() ;
	            
				string configFile = this.getParseConfigFileName() ;

				BenQGuru.eMES.Web.Helper.DataFileParser parser = new BenQGuru.eMES.Web.Helper.DataFileParser();
				parser.FormatName = "SMTMachineDiscard" ;
				parser.ConfigFile = configFile ;
				items = parser.Parse(fileName) ;
				if (items != null)
				{
					for (int i = 0; i < items.Length; i++)
					{
						SMTMachineDiscard item = (SMTMachineDiscard)items[i];
						item.MOCode = item.MOCode.ToUpper();
						item.StepSequenceCode = item.StepSequenceCode.ToUpper();
						item.MaterialCode = item.MaterialCode.ToUpper();
						item.MachineStationCode = item.MachineStationCode.ToUpper();
					}
				}
			}
			catch (Exception e)
			{
				throw e;
			}

			return items ;

		}

		private string getParseConfigFileName()
		{
			string configFile = this.Server.MapPath(this.TemplateSourceDirectory )  ;
			if(configFile[ configFile.Length - 1 ] != '\\')
			{
				configFile += "\\" ;
			}
			configFile += "DataFileParser.xml" ;
			return configFile ;
		}

		protected void cmdImport_ServerClick(object sender, System.EventArgs e)
		{
			if(_facade==null){_facade = new SMTFacade(base.DataProvider);}
			if(items==null)
			{
				items = GetAllItem();
				if (items == null || items.Length == 0)
					return;
			}
			string strMessage = "";
			((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
			this.DataProvider.BeginTransaction();
			try
			{
				int iRet = this._facade.ImportSMTMachineDiscard(items, this.GetUserCode());
				this.DataProvider.CommitTransaction();
				if (iRet > 0)
				{
					strMessage = languageComponent1.GetString("$SMTMachineDiscard_Import_Success");
					this.cmdEnter.Disabled = true;
				}
				else
				{
					strMessage = languageComponent1.GetString("$SMTMachineDiscard_Import_Error");
				}
			}
			catch (Exception ex)
			{
				this.DataProvider.RollbackTransaction();
				throw ex;
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
			}
			string alertInfo = 
				string.Format("<script language=javascript>alert('{0}');</script>", strMessage);
			if( !this.IsClientScriptBlockRegistered("ImportAlert") )
			{
				this.RegisterClientScriptBlock("ImportAlert", alertInfo);	
			}
			items = null;
		}

		protected void cmdReturn_ServerClick(object sender, EventArgs e)
		{
			this.Response.Redirect("FSMTMachineDiscardMP.aspx");
		}

		#endregion

    }
}
