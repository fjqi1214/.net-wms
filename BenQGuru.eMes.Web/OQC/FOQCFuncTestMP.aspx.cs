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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.OQC
{
	/// <summary>
	/// FOQCFuncTestMP 的摘要说明。
	/// </summary>
	public partial class FOQCFuncTestMP : BasePage
	{

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.OQC.OQCFacade _facade;
// =new OQCFacadeFactory(base.DataProvider).CreateOQCFacade() ;
		private object[] itemGroupValue = null;
		protected GridHelper gridHelper = null;

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
			this.btnInitGroupCount.Click += new System.Web.UI.ImageClickEventHandler(this.btnInitGroupCount_Click);
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
			if (! this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				if (this.GetRequestParam("ItemCode") == string.Empty)
				{
					ExceptionManager.Raise( this.GetType() , "$Error_RequestUrlParameter_Lost"); 
				}
				this.txtItemCode.Text = this.GetRequestParam("ItemCode");

				InitWebGrid();
				InitPageValue();
			}
        }

//		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
//		{
//			return this.languageComponent1;
//		}

		private void InitPageValue()
		{
			this.txtMinDutyRatoMin.Text = string.Empty;
			this.txtMinDutyRatoMax.Text = string.Empty;
			this.txtBurstMdFreMin.Text = string.Empty;
			this.txtBurstMdFreMax.Text = string.Empty;
			this.txtElectricCount.Text = "0";
			this.txtGroupCount.Text = "0";
			
			if(_facade==null){_facade = new OQCFacadeFactory(base.DataProvider).CreateOQCFacade() ;}
			object objMain = _facade.GetOQCFuncTest(this.txtItemCode.Text);
			if (objMain == null)
				return;
			OQCFuncTest testMain = (OQCFuncTest)objMain;
			this.txtMinDutyRatoMin.Text = testMain.MinDutyRatoMin.ToString("##.##");
			this.txtMinDutyRatoMax.Text = testMain.MinDutyRatoMax.ToString("##.##");
			this.txtBurstMdFreMin.Text = testMain.BurstMdFreMin.ToString("##.##");;
			this.txtBurstMdFreMax.Text = testMain.BurstMdFreMax.ToString("##.##");;
			this.txtElectricCount.Text = Convert.ToInt32(testMain.ElectricTestCount).ToString();
			this.txtGroupCount.Text = Convert.ToInt32(testMain.FuncTestGroupCount).ToString();

			itemGroupValue = _facade.QueryOQCFuncTestSpec(this.txtItemCode.Text);
			
			InitGridView();
		}
        #endregion

		#region WebGrid
		protected void InitWebGrid()
		{
			/*
			this.gridHelper.AddColumn( "Sequence", "项次",	null);
			this.gridHelper.AddColumn( "FreMin", "频率最小值",	null);
			this.gridHelper.AddColumn( "FreMax", "频率最大值",	null);
			this.gridHelper.AddColumn( "ElectricMin", "电流最小值",	null);
			this.gridHelper.AddColumn( "ElectricMax", "电流最大值",	null);

			this.gridHelper.AddDefaultColumn( false, false );
            
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
			*/
			this.gridWebGrid.Columns.Add( "Sequence", "项次");
			this.gridWebGrid.Columns.Add( "FreMin", "频率最小值");
			this.gridWebGrid.Columns.Add( "FreMax", "频率最大值");
			this.gridWebGrid.Columns.Add( "ElectricMin", "电流最小值");
			this.gridWebGrid.Columns.Add( "ElectricMax", "电流最大值");

			this.gridWebGrid.DisplayLayout.AllowUpdateDefault = AllowUpdate.Yes;
			this.gridWebGrid.DisplayLayout.CellClickActionDefault = CellClickAction.Edit;
			for (int i = 0; i < this.gridWebGrid.Columns.Count; i++)
			{
				this.gridWebGrid.Columns[i].AllowUpdate = AllowUpdate.Yes;
			}

			//多语言
			this.gridHelper = new GridHelper(this.gridWebGrid);
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}
		#endregion

		private void InitGridView()
		{
			int iRowCount = Convert.ToInt32(this.txtGroupCount.Text);
			if (this.gridWebGrid.Rows.Count <= iRowCount)
			{
				int iExistRow = this.gridWebGrid.Rows.Count;
				if (this.itemGroupValue != null && this.itemGroupValue.Length > this.gridWebGrid.Rows.Count)
				{
					iExistRow = Math.Min(iRowCount, itemGroupValue.Length);
					for (int i = this.gridWebGrid.Rows.Count; i < iExistRow; i++)
					{
						OQCFuncTestSpec spec = (OQCFuncTestSpec)itemGroupValue[i];
						this.gridWebGrid.Rows.Add(new UltraGridRow(
							new object[]{this.gridWebGrid.Rows.Count + 1,
											spec.FreMin.ToString("##.##"),
											spec.FreMax.ToString("##.##"),
											spec.ElectricMin.ToString("##.##"),
											spec.ElectricMax.ToString("##.##")}));
					}
				}
				for (int i = iExistRow; i < iRowCount; i++)
				{
					this.gridWebGrid.Rows.Add(new UltraGridRow(new object[]{this.gridWebGrid.Rows.Count + 1, string.Empty, string.Empty, string.Empty, string.Empty }));
				}
			}
			else
			{
				for (int i = this.gridWebGrid.Rows.Count - 1; i >= iRowCount; i--)
				{
					this.gridWebGrid.Rows.RemoveAt(i);
				}
			}
		}

		private void btnInitGroupCount_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(_facade==null){_facade = new OQCFacadeFactory(base.DataProvider).CreateOQCFacade() ;}
			itemGroupValue = _facade.QueryOQCFuncTestSpec(this.txtItemCode.Text);
			InitGridView();
		}

		protected void cmdSave_ServerClick(object sender, EventArgs e)
		{
			if (CheckData() == false)
				return;
			
			try
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.OpenConnection();
				if(_facade==null){_facade = new OQCFacadeFactory(base.DataProvider).CreateOQCFacade() ;}
				OQCFuncTest test = (OQCFuncTest)_facade.GetOQCFuncTest(this.txtItemCode.Text);
				bool bIsNew = false;
				if (test == null)
				{
					bIsNew = true;
					test = new OQCFuncTest();
				}
				test.ItemCode = this.txtItemCode.Text;
				test.FuncTestGroupCount = Convert.ToDecimal(this.txtGroupCount.Text);
//				test.MinDutyRatoMin = 0;
//				test.MinDutyRatoMax = 0;
//				test.BurstMdFreMin = 0;
//				test.BurstMdFreMax = 0;
				test.ElectricTestCount = Convert.ToDecimal(this.txtElectricCount.Text);
				test.MaintainUser = this.GetUserCode();
				test.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
				test.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
				if (bIsNew == true)
					_facade.AddOQCFuncTest(test);
				else
					_facade.UpdateOQCFuncTest(test);

//				string strSql = "DELETE FROM tblOQCFuncTestSpec WHERE ItemCode='" + this.txtItemCode.Text + "'";
//				this.DataProvider.CustomExecute(new SQLCondition(strSql));
//
//				for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
//				{
//					OQCFuncTestSpec spec = new OQCFuncTestSpec();
//					spec.ItemCode = this.txtItemCode.Text;
//					spec.GroupSequence = i;
//					spec.FreMin = Convert.ToDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("FreMin").Value);
//					spec.FreMax = Convert.ToDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("FreMax").Value);
//					spec.ElectricMin = Convert.ToDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("ElectricMin").Value);
//					spec.ElectricMax = Convert.ToDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("ElectricMax").Value);
//					spec.MaintainUser = this.GetUserCode();
//					spec.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
//					spec.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
//					_facade.AddOQCFuncTestSpec(spec);
//				}
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.CommitTransaction();
			}
			catch (Exception ex)
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.RollbackTransaction();
				throw ex;
			}
			finally
			{
				((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)base.DataProvider).PersistBroker.CloseConnection();
			}
		}

		private bool CheckData()
		{
			PageCheckManager manager = new PageCheckManager();

//			manager.Add( new DecimalCheck(this.lblMinDutyRatoMin, this.txtMinDutyRatoMin, 0, decimal.MaxValue, true));
//			manager.Add( new DecimalCheck(this.lblMinDutyRatoMax, this.txtMinDutyRatoMax, 0, decimal.MaxValue, true));
//			manager.Add( new DecimalCheck(this.lblBurstMdFreMin, this.txtBurstMdFreMin, 0, decimal.MaxValue, true));
//			manager.Add( new DecimalCheck(this.lblBurstMdFreMax, this.txtBurstMdFreMax, 0, decimal.MaxValue, true));
			manager.Add( new NumberCheck(this.lblGroupCount, this.txtGroupCount, 0, int.MaxValue, true));
			manager.Add( new NumberCheck(this.lblElectricCount, this.txtElectricCount, 0, int.MaxValue, true));
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
				return false;
			}
			
//			manager.Add( new DecimalCheck(this.lblMinDutyRatoMax, this.txtMinDutyRatoMax, Convert.ToDecimal(this.txtMinDutyRatoMin.Text), decimal.MaxValue, true));
//			manager.Add( new DecimalCheck(this.lblBurstMdFreMax, this.txtBurstMdFreMax, Convert.ToDecimal(this.txtBurstMdFreMin.Text), decimal.MaxValue, true));
//			if ( !manager.Check() )
//			{
//				WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
//				return false;
//			}
//			
//			if (this.gridWebGrid.Rows.Count < int.Parse(this.txtGroupCount.Text))
//			{
//				WebInfoPublish.Publish(this, "$Please_Complete_GroupValue",this.languageComponent1);
//				return false;
//			}
//
//			for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
//			{
//				if (IsDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("FreMin").Value.ToString()) == false || 
//					IsDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("FreMax").Value.ToString()) == false ||
//					IsDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("ElectricMin").Value.ToString()) == false ||
//					IsDecimal(this.gridWebGrid.Rows[i].Cells.FromKey("ElectricMax").Value.ToString()) == false)
//				{
//					WebInfoPublish.Publish(this, "$GroupValue_FormatError",this.languageComponent1);
//					return false;
//				}
//			}
			return true;
		}

		private bool IsDecimal(string value)
		{
			try
			{
				decimal d = decimal.Parse(value);
				return true;
			}
			catch
			{
				return false;
			}
		}

		protected void cmdCancel_ServerClick(object sender, EventArgs e)
		{
			this.Response.Redirect("../MOModel/FItemMP.aspx");
		}
	}
}
