#region System
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
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.MOModel
{
	/// <summary>
	/// FItemLocationMP 的摘要说明。
	/// </summary>
	public partial class FItemLocationMP : BaseMPage
	{
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;


		private ItemFacade _facade ;//= new  FacadeFactory(base.DataProvider).CreateItemFacade();

	
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
			this.languageComponent1.LanguagePackageDir = "\\\\grd2-build\\language pack\\";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion


		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

				Initparameters();
				InitViewPanel();

                BuildOrgList();

				//this.pagerSizeSelector.Readonly = true;

				BaseModelFacade facade = new BaseModelFacade(base.DataProvider);					
				object[] segments = facade.QuerySegment("",0,System.Int32.MaxValue);
				if( segments != null )
				{
					this.drpSegmentQuery.Items.Clear();

					foreach(Segment seg in segments)
					{
						this.drpSegmentQuery.Items.Add( seg.SegmentCode );
					}

					this.drpSegmentQuery.Items.Insert(0, "" );					
				}

			}
		}

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}
		private void InitViewPanel()
		{
			this.txtItemCodeQuery.Text = ItemCode;
			this.txtItemCodeQuery.ReadOnly = true;
		}
		protected void drpItemLocationSideEdit_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				this.drpItemLocationSideEdit.Items.Clear();
				string[] itemLocationSides = this.GetItemLocationSides();

				for(int i=0;i<itemLocationSides.Length;i++)
				{
					this.drpItemLocationSideEdit.Items.Add(new ListItem(this.languageComponent1.GetString(itemLocationSides[i]),itemLocationSides[i]));
					//this.drpItemLocationSideEdit.Items.Add(new ListItem(itemLocationSides[i],itemLocationSides[i]));	//多国语言处理后 此语句删除
				}
			}
		}

		private string[] GetItemLocationSides()
		{
			string[] ItemLocationSides = new string[2];
			ItemLocationSides[0] = ItemLocationSide.ItemLocationSide_A;
			ItemLocationSides[1] = ItemLocationSide.ItemLocationSide_B;
			return ItemLocationSides;
		}
		#endregion

		#region WebGrid
		protected override void InitWebGrid()
		{
			this.gridHelper.AddColumn( "ItemCode", "产品代码",	null);
			this.gridHelper.AddColumn( "AB", "板面",	null);
			this.gridHelper.AddColumn( "Qty", "元件件数",	null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			this.gridHelper.AddColumn( "ABValue", "板面Value",	null);
			this.gridHelper.AddColumn( "SegmentCode", "工段代码",	null);

			this.gridWebGrid.Columns.FromKey("ItemCode").Hidden = true;
			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
			this.gridWebGrid.Columns.FromKey("ABValue").Hidden = true;

            this.gridHelper.AddColumn("OrganizationID", "组织编号", null);
            this.gridWebGrid.Columns.FromKey("OrganizationID").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.gridHelper.RequestData();
		}

		
		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
				
			if(((ItemLocation)obj).AB.ToString() == ItemLocationSide.ItemLocationSide_A || ((ItemLocation)obj).AB.ToString() == ItemLocationSide.ItemLocationSide_B)
			{
				return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
					new object[]{"false",
									((ItemLocation)obj).ItemCode.ToString(),
									this.languageComponent1.GetString(((ItemLocation)obj).AB.ToString()),
									((ItemLocation)obj).Qty.ToString(),
									((ItemLocation)obj).MaintainUser.ToString(),
									FormatHelper.ToDateString(((ItemLocation)obj).MaintainDate),
									FormatHelper.ToTimeString(((ItemLocation)obj).MaintainTime),
									((ItemLocation)obj).AB.ToString(),
									((ItemLocation)obj).SegmentCode.ToString(),
                                    ((ItemLocation)obj).OrganizationID.ToString(),
									""});
			}
			else
			{
				return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
					new object[]{"false",
									((ItemLocation)obj).ItemCode.ToString(),
									((ItemLocation)obj).AB.ToString(),
									((ItemLocation)obj).Qty.ToString(),
									((ItemLocation)obj).MaintainUser.ToString(),
									FormatHelper.ToDateString(((ItemLocation)obj).MaintainDate),
									FormatHelper.ToTimeString(((ItemLocation)obj).MaintainTime),
									((ItemLocation)obj).AB.ToString(),
									((ItemLocation)obj).SegmentCode.ToString(),
                                    ((ItemLocation)obj).OrganizationID.ToString(),
									""});
			}
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateItemFacade();}
			return this._facade.QueryItemLocation( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ItemCode)),
				inclusive, exclusive );
		}

		protected override int GetRowCount()
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateItemFacade();}
			return this._facade.QueryItemLocationCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ItemCode)));
		}

		#endregion

		#region Button
		protected void cmdReturn_ServerClick(object sender, System.EventArgs e)
		{
			Response.Redirect(this.MakeRedirectUrl("FItemMP.aspx"));
		}

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateItemFacade();}
			MOFacade moFacade = new MOFacade(base.DataProvider);
			if(moFacade.CheckItemCodeUsed(ItemCode))
			{
				WebInfoPublish.Publish(this,"$ERROR_ITEM_USE",this.languageComponent1);
				return;
			}
			this._facade.AddItemLocation( (ItemLocation)domainObject );
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateItemFacade();}
			MOFacade moFacade = new MOFacade(base.DataProvider);
			if(moFacade.CheckItemCodeUsed(ItemCode))
			{
				WebInfoPublish.Publish(this,"$ERROR_ITEM_USE",this.languageComponent1);
				return;
			}
			this._facade.DeleteItemLoaction( (ItemLocation[])domainObjects.ToArray( typeof(ItemLocation) ) );
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateItemFacade();}
			MOFacade moFacade = new MOFacade(base.DataProvider);
			if(moFacade.CheckItemCodeUsed(ItemCode))
			{
				WebInfoPublish.Publish(this,"$ERROR_ITEM_USE",this.languageComponent1);
				return;
			}
			this._facade.UpdateItemLocation( (ItemLocation)domainObject );
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.drpItemLocationSideEdit.Enabled = true;               
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.drpItemLocationSideEdit.Enabled = false;
			}

			if(pageAction == PageActionType.Cancel)
			{
				this.drpItemLocationSideEdit.Enabled = true;
			}

            this.DropDownListOrg.Enabled = false;
		}
		#endregion

		#region Object <--> Page
		protected override object GetEditObject()
		{
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateItemFacade();}
			ItemLocation itemLocation = this._facade.CreateNewItemLocation();

			itemLocation.ItemCode	            = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ItemCode, 40));
			itemLocation.AB			            = FormatHelper.CleanString(this.drpItemLocationSideEdit.SelectedValue,40);
			itemLocation.Qty			        = Int32.Parse(FormatHelper.CleanString(this.txtQtyEdit.Text));
			itemLocation.MaintainUser			= this.GetUserCode();
			itemLocation.SegmentCode			= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.drpSegmentQuery.SelectedValue, 40));
            itemLocation.OrganizationID         = int.Parse(this.DropDownListOrg.SelectedValue);

			return itemLocation;
		}

		protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
		{	
			if(_facade==null){_facade = new  FacadeFactory(base.DataProvider).CreateItemFacade();}
            object obj = _facade.GetItemLocation(ItemCode, row.Cells.FromKey("ABValue").Text.ToString(), GlobalVariables.CurrentOrganizations.First().OrganizationID);
			
			if (obj != null)
			{
				return (ItemLocation)obj;
			}

			return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtQtyEdit.Text = string.Empty;
				return;
			}

			if(((ItemLocation)obj).AB.ToString() == ItemLocationSide.ItemLocationSide_A || ((ItemLocation)obj).AB.ToString() == ItemLocationSide.ItemLocationSide_B)
			{this.drpItemLocationSideEdit.SelectedValue = ((ItemLocation)obj).AB.ToString();}
			if(((ItemLocation)obj).SegmentCode.ToString() != null && ((ItemLocation)obj).SegmentCode.ToString() != string.Empty)
			{this.drpSegmentQuery.SelectedValue = ((ItemLocation)obj).SegmentCode.ToString();}
			this.txtQtyEdit.Text	= ((ItemLocation)obj).Qty.ToString();
		}

		
		protected override bool ValidateInput()
		{			
			PageCheckManager manager = new PageCheckManager();
			
			//manager.Add( new LengthCheck(lblABEdit, txtABEdit, 40, true) );
			manager.Add( new NumberCheck(lblQtyEdit, txtQtyEdit, 0,int.MaxValue, true) );
            manager.Add(new LengthCheck(this.lblOrgEdit, this.DropDownListOrg, 8, true));

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

				return false;
			}

			return true;
		}

		#endregion

		#region Export
		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{  this.languageComponent1.GetString(((ItemLocation)obj).AB.ToString()),
								   ((ItemLocation)obj).Qty.ToString(),
								   ((ItemLocation)obj).SegmentCode.ToString(),
								   ((ItemLocation)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((ItemLocation)obj).MaintainDate)
							   };
		}

		protected override string[] GetColumnHeaderText()
		{
			return new string[] {	
									"AB",
									"Qty",
									"SegmentCode",
									"MaintainUser",
									"MaintainDate"
									};
		}
		#endregion

		#region property
		private void Initparameters()
		{
			if(this.Request.Params["itemcode"] == null)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_RequestUrlParameter_Lost");
			}
			else
			{
				this.ViewState["itemcode"] = this.Request.Params["itemcode"];
			}
		}

		protected void cmdGridExport_ServerClick(object sender, System.EventArgs e)
		{
			this.excelExporter.Export();
		}


	
		public string ItemCode
		{
			get
			{
				return (string) this.ViewState["itemcode"];
			}
		}
		#endregion

        private void BuildOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));

            try
            {
                this.DropDownListOrg.SelectedValue = GlobalVariables.CurrentOrganizations.First().OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }
        }

        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }
	}
}
