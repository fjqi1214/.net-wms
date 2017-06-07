#region system
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
using Infragistics.WebUI.UltraWebGrid;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion
namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// MBOMDownload 的摘要说明。
	/// </summary>
	public partial class MBOMDownload : BasePage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;

		private GridHelper gridHelper = null;
		private ButtonHelper buttonHelper = null;
		private BenQGuru.eMES.MOModel.SBOMFacade _sbomFacade ;//= new SBOMFacade();
		private object[] sboms = null ;
		private BenQGuru.eMES.MOModel.ItemFacade itemFacade ;//= new ItemFacadeFactory().Create() ;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitHander();
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				// 初始化界面UI
				this.InitUI();
				this.InitWebGrid();
			}
		}

		#region private method


		private void InitHander()
		{
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
			this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);

			this.buttonHelper = new ButtonHelper(this);

		}

		private void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "SBOMItemCode", "子阶料料号",	null);
			this.gridHelper.AddColumn( "SBOMItemName", "子阶料名称",	null);
			this.gridHelper.AddColumn( "SBOMSourceItemCode", "首选料",	null);
			this.gridHelper.AddColumn("SBOMItemQty","单机用量",null );
			this.gridHelper.AddColumn("SBOMItemUOM","计量单位",null );
			this.gridHelper.AddColumn("SBOMItemLocation","位号",null );
			this.gridHelper.AddColumn("EffectiveDate","生效日期",null );
			this.gridHelper.AddColumn("IneffectiveDate","失效日期",null );
			this.gridHelper.AddColumn("SBOMItemECN","ECN",null );

			//this.gridHelper.("IneffectiveDate","失效日期",null );
			this.gridHelper.AddDefaultColumn(true,false);
			//this.gridHelper.ApplyDefaultStyle();

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected  object[] LoadDataSource(int inclusive,int exclusive)
		{
			// TODO:这里要改
			ArrayList newSBOMs = new ArrayList() ;
			if(sboms == null)
			{
				this.GetAllSBOMs();
			}
			for(int i=1;i<=sboms.Length;i++)
			{
				if(i>=inclusive && i<=exclusive)
				{
					newSBOMs.Add( sboms[i-1] );
				}
			}

			return newSBOMs.ToArray() ;
		}

		private object[] GetAllSBOMs()
		{
			string fileName = string.Empty ;

			if(this.ViewState["UploadedFileName"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_NOExamineFile");
			}
	
			fileName = this.ViewState["UploadedFileName"].ToString() ;
            
			string configFile = this.getParseConfigFileName() ;

			DataFileParser parser = new DataFileParser();
			parser.FormatName = "SBOM" ;
			parser.ConfigFile = configFile ;
			parser.CheckValidHandle = new CheckValid( this.SBOMDownloadCheck );
			sboms = parser.Parse(fileName) ;

			//added by jessie lee for AM0185(TS35), 2005/10/11,P4.10
			//为了兼顾效率，一次只能导入一个产品的BOM，即按产品导入
			ArrayList aList = new ArrayList();
			for(int i=0;i<sboms.Length;i++)
			{
				bool repeat = false ;
				for(int j = aList.Count ; j>0 ; j--)
				{
					if( (string.Compare((sboms[i] as SBOM).ItemCode,aList[j-1].ToString(),true)) == 0  )
					{
						repeat = true ;
						break ;
					}
				}
				if(!repeat)
				{
					aList.Add( (sboms[i] as SBOM).ItemCode );
				}
			}
		
			if(aList.Count >1)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_Only_A_ProductBOM_Import_Once");
			}

			foreach(SBOM sbom in sboms)
			{
				sbom.ItemCode = FormatHelper.PKCapitalFormat(sbom.ItemCode);
				sbom.SBOMItemCode = FormatHelper.PKCapitalFormat(sbom.SBOMItemCode);
				sbom.SBOMSourceItemCode = FormatHelper.PKCapitalFormat(sbom.SBOMSourceItemCode);
				sbom.MaintainUser        = this.GetUserCode();
				sbom.MaintainDate        = FormatHelper.TODateInt(DateTime.Today);
				sbom.MaintainTime        = FormatHelper.TOTimeInt(DateTime.Now);

			}

			return sboms ;
		}

		private bool SBOMDownloadCheck(object obj)
		{
			SBOM sbom = (SBOM)obj;
			//检查日期格式
			if(sbom.SBOMItemEffectiveDate <20000101)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_EffectiveDate_Format");
			}
			if(sbom.SBOMItemInvalidDate <20000101)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_InvalidDate_Format");
			}
			
			// 检查 ItemCode
			if((sbom.SBOMItemCode == string.Empty)||(sbom.SBOMItemCode == null))
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_OPBOMItem_NotNull");
			}
			// 检查 首选料
			if((sbom.SBOMSourceItemCode == string.Empty)||(sbom.SBOMSourceItemCode == null))
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_OPBOMSourceItem_NotNull");
			}
			//检查单机用量
			if(!(sbom.SBOMItemQty >0 ))
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_SBOMItemQty_Format");
			}

			itemFacade = new ItemFacade(base.DataProvider);
            object item = itemFacade.GetItem(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(sbom.ItemCode)), GlobalVariables.CurrentOrganizations.First().OrganizationID);
			if( item == null )
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_ItemCode_NotExisted",String.Format("[$ItemCode='{0}']",sbom.ItemCode));
			}
			return true;
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

		protected Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
				new object[]{"false",
								((SBOM)obj).ItemCode,
								((SBOM)obj).SBOMItemCode,
								((SBOM)obj).SBOMItemName,
								((SBOM)obj).SBOMSourceItemCode,
								((SBOM)obj).SBOMItemQty,
								((SBOM)obj).SBOMItemUOM,
								((SBOM)obj).SBOMItemLocation,
								FormatHelper.ToDateString(((SBOM)obj).SBOMItemEffectiveDate),
								FormatHelper.ToDateString(((SBOM)obj).SBOMItemInvalidDate),
								((SBOM)obj).SBOMItemECN,
				                ((SBOM)obj).MaintainDate,
				                ((SBOM)obj).MaintainTime,
								((SBOM)obj).MaintainUser,
								((SBOM)obj).SBOMItemControlType,
								((SBOM)obj).SBOMItemDescription,
								((SBOM)obj).SBOMItemEffectiveTime,
								((SBOM)obj).SBOMItemInvalidTime,
								((SBOM)obj).SBOMItemStatus,
								((SBOM)obj).SBOMItemVersion,
				                ((SBOM)obj).Sequence
								});
		}

		#endregion

	

		private SBOM[] GetSelectedBOMs()
		{
			int j= 0;
			SBOM[] boms = new SBOM[gridWebGrid.Rows.Count];
			for(int i=0;i<gridWebGrid.Rows.Count;i++)
			{
				if(gridWebGrid.Rows[i].Cells[0].Text == "true")
				{
					boms[i] =  (SBOM)sboms[i];
					j++;
				}
			}
			if(j==0)
			{
				this.Response.Write( "<script language=javascript>alert('" + this.languageComponent1.GetString("warningNoRow") + "')</script>");
			}
			return boms;
		}


		private SBOM GetDomainObject(UltraGridRow row)
		{
			SBOM sbom  =  new SBOM();
			sbom.ItemCode =FormatHelper.PKCapitalFormat( row.Cells[1].Text);
			sbom.SBOMItemECN = row.Cells[2].Text;
			sbom.SBOMItemCode= row.Cells[3].Text.ToUpper();
			sbom.SBOMItemName= row.Cells[4].Text;
			try
			{
				sbom.SBOMItemEffectiveDate = Int32.Parse(row.Cells[6].Text);
			}
			catch
			{
				sbom.SBOMItemEffectiveDate = 0;
			}

			try
			{
				sbom.SBOMItemInvalidDate = Int32.Parse(row.Cells[7].Text);
			}
			catch
			{
				sbom.SBOMItemInvalidDate = 0;
			}
			try
			{
				sbom.MaintainDate = Int32.Parse(row.Cells[8].Text);
			}
			catch
			{
				sbom.MaintainDate = 0;
			}

			try
			{
				sbom.MaintainTime = Int32.Parse(row.Cells[9].Text);
			}
			catch
			{
				sbom.MaintainTime = 0;
			}
			sbom.MaintainUser = this.GetUserCode();
			sbom.SBOMItemControlType= row.Cells[11].Text;
			sbom.SBOMItemStatus = row.Cells[16].Text;
			try
			{
				sbom.SBOMItemQty = System.Decimal.Parse(row.Cells[5].Text);
			}
			catch
			{
				sbom.SBOMItemQty = 0;
			}
			try
			{
				sbom.Sequence = Int32.Parse(row.Cells[19].Text);
			}
			catch
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_SystemError");
			}
			sbom.SBOMItemUOM =FormatHelper.CleanString( row.Cells[20].Text);
            sbom.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

			return sbom;
		}



		private void RequestData()
		{
			if(sboms==null)
			{
				sboms = GetAllSBOMs() ;
			}


			this.gridHelper.Grid.DisplayLayout.Pager.AllowPaging = true ;
			this.gridHelper.Grid.DisplayLayout.Pager.CurrentPageIndex = 1;
			this.gridHelper.Grid.DisplayLayout.Pager.PageSize = int.MaxValue ;
			this.gridHelper.GridBind(1,int.MaxValue);
		}

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

		#region 页面事件
		protected void cmdBomView_ServerClick(object sender, System.EventArgs e)
		{
            string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page, this.DownLoadPathBom, null);
			if(fileName == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
			}
			//add by crystal chu 2005/07/15
			if(fileName.LastIndexOf(".csv") == -1)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileTypeError");
			}


			this.ViewState.Add("UploadedFileName",fileName);
			this.RequestData();
			this.gridHelper.CheckAllRows( CheckStatus.Checked );		//modify by Simone
		}

		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl( "FBOMP.aspx"));
		}

		protected void chbSelectAll_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( this.chbSelectAll.Checked )
			{
				this.gridHelper.CheckAllRows( CheckStatus.Checked );
			}
			else
			{
				this.gridHelper.CheckAllRows( CheckStatus.Unchecked );
			}
		}

		protected void cmdAdd_ServerClick(object sender, System.EventArgs e)
		{
			if(_sbomFacade==null){_sbomFacade = new FacadeFactory(base.DataProvider).CreateSBOMFacade();}
			if(sboms==null)
			{
				sboms = GetAllSBOMs() ;
			}
			SBOM[] selectSBOMs = GetSelectedBOMs();
			if(sboms != null)
			{
				if(sboms.Length == 0)
				{
					this.Response.Write( "<script language=javascript>alert('" + this.languageComponent1.GetString("warningNoRow") + "')</script>");
				}
				else
				{
					this._sbomFacade.AddSBOMs(selectSBOMs);
					gridWebGrid.Rows.Clear();
				}
			}
			
		}

		#endregion



		
	}
}
