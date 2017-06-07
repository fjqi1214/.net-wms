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
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FOperationMP 的摘要说明。
	/// </summary>
	public partial class FOperationMP : BaseMPageNew
	{
		protected System.Web.UI.WebControls.Label lblOperationTitle;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components;
		protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
		
		private BenQGuru.eMES.BaseSetting.BaseModelFacade _facade = null ; //new BaseModelFacadeFactory().Create();

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

		protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
		{
			return this.languageComponent1;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{	
			if(!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
		}

		//angel zhu  modify 2005-04-20
		protected override void AddDomainObject(object domainObject)
		{
			if(_facade == null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.AddOperation((Operation)domainObject);
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade == null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.DeleteOperation((Operation[])domainObjects.ToArray(typeof(Operation)));
		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade == null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			this._facade.UpdateOperation((Operation)domainObject);
		}   
		#endregion

		#region WebGrid
 		protected override void InitWebGrid()
		{
            base.InitWebGrid();
			this.gridHelper.AddColumn( "OPCode", "工序代码",	null);
			this.gridHelper.AddColumn( "OPDescription", "工序描述",	null);
			this.gridHelper.AddLinkColumn( "SelectResource", "资源列表",	null);
            this.gridHelper.AddLinkColumn("SelectRoute", "返工途程", null);

			new OperationListFactory().CreateOperationListColumns( this.gridHelper );

			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this.gridHelper.AddColumn( "MaintainTime", "维护时间",	null);

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

		protected override string[] GetColumnHeaderText()
		{
			string[] opListHeads = (new OperationListFactory()).CreateOperationListColumnsHead();
			string[] heads = new string[ 4 + opListHeads.Length ];

			heads[0] = "OPCode";
			heads[1] = "OPDescription";

			for(int i=0;i<opListHeads.Length;i++)
			{
				heads[ 2 + i ] = opListHeads[i];
			}

			heads[ 2 + opListHeads.Length ] = "MaintainUser";
			heads[ 2 + opListHeads.Length + 1 ] = "MaintainDate";

			return heads;

//			return new string[] {	
//									"OPCode",	
//									"OPDescription",	
//									"是否上料检查",	
//									"是否需要MNID拆分",
//									"是否工单流入",	
//									"是否工单流出",
//									"是否总数量合计",
//									"是否SPC统计",	
//									"是否包含出错代码",
//									"是否不良判定",	
//									"工序描述",
//									"维护用户",	
//									"维护日期",	
//									"维护时间" };
		}
		
		protected override string[] FormatExportRecord( object obj )
		{
			bool[] opList = new OperationListFactory().CreateOperationListBooleanArray( (obj as Operation).OPControl );
            ArrayList arrayList = new ArrayList(opList.Length);
            for (int i = 0; i < opList.Length; i++)
            {
                arrayList.Add(opList[i]);
            }
            //导出的时候不需要哪列，就RemoveAt去掉对应的，具体的对照数据参考OperationListFactory中
            arrayList.RemoveAt(7);
            string[] values = new string[4 + arrayList.Count];
			values[0] = ((Operation)obj).OPCode.ToString();
			values[1] = ((Operation)obj).OPDescription.ToString();

            for (int i = 0; i < arrayList.Count; i++)
			{
                values[2 + i] = FormatHelper.DisplayBoolean(Convert.ToBoolean(arrayList[i].ToString().ToLower()), this.languageComponent1);
			}

            values[2 + arrayList.Count] = ((Operation)obj).GetDisplayText("MaintainUser");
            values[2 + arrayList.Count + 1] = FormatHelper.ToDateString(((Operation)obj).MaintainDate);

			return values;
		}


		protected override DataRow GetGridRow(object obj)
		{
			
            DataRow row = this.DtSource.NewRow();
            row["OPCode"] = ((Operation)obj).OPCode.ToString();
            row["OPDescription"] = ((Operation)obj).OPDescription.ToString();
            row["SelectResource"] = "";
            row["SelectRoute"] = "";

            bool[] opList = new OperationListFactory().CreateOperationListBooleanArray((obj as Operation).OPControl);
            string[] strings = new OperationListFactory().CreateOperationListColumnsHead();
            for (int i = 0; i < opList.Length; i++)
            {
                row[strings[i]] = opList[i];
            }

            row["MaintainUser"] = ((Operation)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString((obj as Operation).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString((obj as Operation).MaintainTime);
            return row;

		}


		protected override int GetRowCount()
		{
			if(_facade == null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QueryOperationCount( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text)));
		}

		#endregion

		#region Button
       
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "SelectResource")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FOperation2ResourceSP.aspx", new string[] { "opcode" }, new string[] { row.Items.FindItemByKey("OPCode").Value.ToString() }));
            }
            if (commandName == "SelectRoute")
            {
                this.Response.Redirect(this.MakeRedirectUrl("./FErrorCode2OPRework.aspx", new string[] { "opcode" }, new string[] { row.Items.FindItemByKey("OPCode").Value.ToString() }));
            }
        }


		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtOPCodeEdit.ReadOnly = false;
			}

			if ( pageAction == PageActionType.Update )
			{
				this.txtOPCodeEdit.ReadOnly = true;
			}
		}

		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade == null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			Operation operation = this._facade.CreateNewOperation();

			operation.OPCode			= FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeEdit.Text, 40));
			operation.OPCollectionType  = "AUTO";//this.drpOPCollectionTypeEdit.SelectedValue;
			operation.OPControl			= (new OperationListFactory()).CreateOperationList( this.chklstOPControlEdit,this.chklstOPAttributeEdit );
			operation.OPDescription		= FormatHelper.CleanString(this.txtOPDescriptionEdit.Text, 100);
			operation.MaintainUser		= this.GetUserCode();
            
			return operation;
		}


		protected override object GetEditObject(GridRecord row)
		{	
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("OPCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetOperation(strCode);
            if (obj != null)
            {
                return (Operation)obj;
            }
            return null;

		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtOPDescriptionEdit.Text	= "";
				this.drpOPCollectionTypeEdit.SelectedIndex = 0;
				this.txtOPCodeEdit.Text	= "";
				if( this.chklstOPControlEdit.Items.Count == 0 )
				{
					(new OperationListFactory()).CreateNewOperationListCheckBoxList( this.chklstOPControlEdit,this.chklstOPAttributeEdit,this.languageComponent1 );
				}
				(new OperationListFactory()).CreateOperationListCheckBoxList(this.chklstOPControlEdit,this.chklstOPAttributeEdit,"");

				return;
			}

			this.txtOPDescriptionEdit.Text	= ((Operation)obj).OPDescription.ToString();

//			try
//			{
//				this.drpOPCollectionTypeEdit.SelectedValue	= ((Operation)obj).OPCollectionType.ToString();
//			}
//			catch
//			{
//				this.drpOPCollectionTypeEdit.SelectedIndex = 0;
//			}

			this.txtOPCodeEdit.Text	= ((Operation)obj).OPCode.ToString();
			
			(new OperationListFactory()).CreateOperationListCheckBoxList(this.chklstOPControlEdit,this.chklstOPAttributeEdit,( ((Operation)obj).OPControl ));
		}

		protected override bool ValidateInput()
		{
            PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(lblItemOperationCodeEdit,txtOPCodeEdit,40,true));
			manager.Add( new LengthCheck(lblOPDescriptionEdit,txtOPDescriptionEdit,100,false));

			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);

				return false;
			}

			return true;
		}	

		#endregion

		#region Export
		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade == null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create() ;
			}
			return this._facade.QueryOperation( 
				FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtOPCodeQuery.Text)),
				inclusive, exclusive );
		}
		#endregion

		#region 数据初始化

		protected void drpOPCollectionTypeEdit_Load(object sender, System.EventArgs e)
		{
//			if ( !IsPostBack)
//			{
//				new SystemParameterListBuilder("OPCollectionType").Build(this.drpOPCollectionTypeEdit);
//			}
		}
		#endregion

		protected void chklstOPControlEdit_Load(object sender, System.EventArgs e)
		{
			if( !this.IsPostBack )
			{
				if( this.chklstOPControlEdit.Items.Count == 0 )
				{
					new OperationListFactory().CreateNewOperationListCheckBoxList(this.chklstOPControlEdit,
                        this.chklstOPAttributeEdit,this.languageComponent1 );
				}
			}
		}
	}
}
