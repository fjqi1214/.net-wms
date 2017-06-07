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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// FStepSequenceMP 的摘要说明。
	/// </summary>
	public partial class FStepSequenceMP : BaseMPageNew
	{
		protected System.Web.UI.WebControls.Label lblStepSequenceTitle;
		protected System.Web.UI.WebControls.Label lblStepSequenceOrderEdit;
		protected System.Web.UI.WebControls.TextBox txtStepSequenceOrderEdit;
		protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
		private System.ComponentModel.IContainer components; 
		private BenQGuru.eMES.BaseSetting.BaseModelFacade _facade = null ; //new BaseModelFacadeFactory().Create();
        private BenQGuru.eMES.BaseSetting.ShiftModelFacade _shiftFacade = null;
      
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
			//Added by Icyer, add the ClickCellButton event for maintain warehouse
			//this.gridWebGrid.ClickCellButton += new ClickCellButtonEventHandler(gridWebGrid_ClickCellButton);
			//Added end

		}
		#endregion

		#region Init
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

                BuildOrgList();

                this.chbSaveIntoStock.Checked = false;
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
            this.gridHelper.AddColumn("BigStepSequenceCode", "大线代码", null);
            this.gridHelper.AddColumn("BigStepSequenceSeq", "顺序(大线)", null);
			this.gridHelper.AddColumn( "StepSequenceCode", "生产线代码",	null);
			this.gridHelper.AddColumn( "StepSequenceDescription", "生产线描述",	null);
			this.gridHelper.AddColumn( "StepSequenceSegmentCode", "所属工段",	null);
            this.gridHelper.AddColumn( "ShiftTypeCode", "班制代码", null);
			this.gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this.gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
            this.gridHelper.AddColumn( "MaintainTime", "维护时间", null);

			//Added by Icyer, add a button column to maintain warehouse
			this.gridHelper.AddLinkColumn( "StepSequence2Warehouse", "工厂及仓库", null);
			//Added end

			this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridWebGrid.Columns.FromKey("StepSequence2Warehouse").Hidden = true;

			this.gridHelper.AddDefaultColumn( true, true );

			//多语言
			this.gridHelper.ApplyLanguage( this.languageComponent1 );
		}

        protected override DataRow GetGridRow(object obj)
		{
            DataRow row = this.DtSource.NewRow();
            row["BigStepSequenceCode"] =  ((StepSequence)obj).BigStepSequenceCode.ToString();
            row["BigStepSequenceSeq"] = ((StepSequence)obj).StepSequenceOrder.ToString();
            row["StepSequenceCode"] = ((StepSequence)obj).StepSequenceCode.ToString();
            row["StepSequenceDescription"] = ((StepSequence)obj).StepSequenceDescription.ToString();
            row["StepSequenceSegmentCode"] =((StepSequence)obj).GetDisplayText("SegmentCode");
            row["ShiftTypeCode"] = ((StepSequence)obj).GetDisplayText("ShiftTypeCode");
            row["MaintainUser"] = ((StepSequence)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((StepSequence)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((StepSequence)obj).MaintainTime);
            return row;
		}


		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			return this._facade.QueryStepSequence( 
												FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequenceCodeQuery.Text)),
												FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeQuery.Text)),	
											    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.DropDownListBigStepSequenceCodeQuery.Text)),
												inclusive, exclusive );
		}	
		
		protected override int GetRowCount()
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			return this._facade.QueryStepSequenceCount( 
												FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequenceCodeQuery.Text)),
												FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSegmentCodeQuery.Text)),
                                                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.DropDownListBigStepSequenceCodeQuery.Text)));
		}

		#endregion

		#region Button

		protected override void AddDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}

            object OldObject = this._facade.GetStepSequence(this.DropDownListBigStepSequenceCodeEdit.Text.ToString(), int.Parse(this.txtBigStepSequenceSEQ.Text));
            if (OldObject != null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_BigSSCodeSeqUsed");
            }
            this._facade.AddStepSequence((StepSequence)domainObject);         
		}

		protected override void DeleteDomainObjects(ArrayList domainObjects)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			this._facade.DeleteStepSequence((StepSequence[])domainObjects.ToArray(typeof(StepSequence)));

		}

		protected override void UpdateDomainObject(object domainObject)
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}

            object OldObject = this._facade.GetStepSequence(this.DropDownListBigStepSequenceCodeEdit.Text.ToString(), int.Parse(this.txtBigStepSequenceSEQ.Text));
            if (OldObject != null)
            {
                if ( this.txtSave1.Text.Trim()!= this.DropDownListBigStepSequenceCodeEdit.Text.Trim()
                    || this.txtSave2.Text.Trim() != this.txtBigStepSequenceSEQ.Text.Trim())
                {
                    ExceptionManager.Raise(this.GetType().BaseType, "$Error_BigSSCodeSeqUsed");
                }
            }

			this._facade.UpdateStepSequence((StepSequence)domainObject);
            StepSequence newStepSequence = (StepSequence)domainObject;

            //UpdateStepSequence中已经有Update下面所有RES的逻辑，此处不用再添加了
            //object[] objStepSequence ={ };
            //object[] objStepSequence = this._facade.GetResourceByStepSequenceCode(newStepSequence.StepSequenceCode);
            //for (int i = 0; i < objStepSequence.Length;i++ )
            //{
            //    Resource newResource = (Resource)objStepSequence[i];
            //    newResource.ShiftTypeCode = this.drpShifTypeCode.SelectedValue;
            //    this._facade.UpdateResource(newResource);
            //}
		}

		protected override void buttonHelper_AfterPageStatusChangeHandle( string pageAction )
		{
			if ( pageAction == PageActionType.Add )
			{
				this.txtStepSequenceCodeEdit.ReadOnly = false;
                this.DropDownListOrg.Enabled = true;
			}
			
			if ( pageAction == PageActionType.Update )
			{
				this.txtStepSequenceCodeEdit.ReadOnly = true;
                this.DropDownListOrg.Enabled = false;
			}
		}
		#endregion

		#region Object <--> Page

		protected override object GetEditObject()
		{
			if(_facade==null)
			{
				_facade = new BaseModelFacadeFactory(base.DataProvider).Create();
			}
			StepSequence stepSequence = this._facade.CreateNewStepSequence();

			stepSequence.StepSequenceDescription = FormatHelper.CleanString(this.txtStepSequenceDescriptionEdit.Text, 100);
			stepSequence.StepSequenceCode =		FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStepSequenceCodeEdit.Text, 40));
			stepSequence.SegmentCode =			this.drpSegmentCodeEdit.SelectedValue;
			stepSequence.MaintainUser =			this.GetUserCode();
            stepSequence.OrganizationID = int.Parse(this.DropDownListOrg.SelectedValue);
            stepSequence.BigStepSequenceCode= 	FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.DropDownListBigStepSequenceCodeEdit.Text,40));
            stepSequence.StepSequenceOrder = int.Parse(this.txtBigStepSequenceSEQ.Text);
            stepSequence.ShiftTypeCode = this.drpShifTypeCode.SelectedValue;

            if (this.chbSaveIntoStock.Checked)
            {
                stepSequence.SaveInStock = "Y";
            }
            else
            {
                stepSequence.SaveInStock = "N";
            }

			return stepSequence;
		}


        protected override object GetEditObject(GridRecord row)
		{
            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            string strCode = string.Empty;
            object objCode = row.Items.FindItemByKey("StepSequenceCode").Value;
            if (objCode != null)
            {
                strCode = objCode.ToString();
            }
            object obj = _facade.GetStepSequence(strCode);
            if (obj != null)
            {
                return (StepSequence)obj;
            }
            return null;
		}

		protected override void SetEditObject(object obj)
		{
			if (obj == null)
			{
				this.txtStepSequenceDescriptionEdit.Text	= "";
                this.txtBigStepSequenceSEQ.Text = "";
				this.txtStepSequenceCodeEdit.Text	= "";
				this.drpSegmentCodeEdit.SelectedIndex	= 0;
                this.DropDownListOrg.SelectedIndex = 0;
                this.DropDownListBigStepSequenceCodeEdit.SelectedIndex = 0;
                this.drpShifTypeCode.SelectedIndex = 0;
                this.chbSaveIntoStock.Checked = false;

				return;
			}

            this.txtSave1.Text = ((StepSequence)obj).BigStepSequenceCode.ToString().Trim();
            this.txtSave2.Text = ((StepSequence)obj).StepSequenceOrder.ToString().Trim();
			this.txtStepSequenceDescriptionEdit.Text	= ((StepSequence)obj).StepSequenceDescription.ToString();
			this.txtStepSequenceCodeEdit.Text	= ((StepSequence)obj).StepSequenceCode.ToString();
			this.drpSegmentCodeEdit.SelectedValue	= ((StepSequence)obj).SegmentCode.ToString();
            this.DropDownListBigStepSequenceCodeEdit.SelectedValue = ((StepSequence)obj).BigStepSequenceCode.ToString();
            this.txtBigStepSequenceSEQ.Text = ((StepSequence)obj).StepSequenceOrder.ToString();

            string saveIntoStock = ((StepSequence)obj).SaveInStock;
            if (saveIntoStock != null && saveIntoStock.Length > 0)
            {
                if (saveIntoStock == "Y")
                {
                    this.chbSaveIntoStock.Checked = true;
                }
                else
                {
                    this.chbSaveIntoStock.Checked = false;
                }
            }
            else
            {
                this.chbSaveIntoStock.Checked = false;
            }


            if (((StepSequence)obj).ShiftTypeCode == string.Empty)
            {
                if (_facade == null)
                {
                    _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
                }
                Segment newSegment = (Segment)this._facade.GetSegment(this.drpSegmentCodeEdit.SelectedValue);
                if (newSegment == null)
                {
                    this.drpShifTypeCode.SelectedValue = "";
                }
                else
                {
                    this.drpShifTypeCode.SelectedValue = newSegment.ShiftTypeCode.ToString();
                }
            }
            else
            {
                try
                {
                    this.drpShifTypeCode.SelectedValue = ((StepSequence)obj).ShiftTypeCode;
                }
                catch
                {
                    this.drpShifTypeCode.SelectedIndex = 0;
                }
            } 

            try
            {
                this.DropDownListOrg.SelectedValue = ((StepSequence)obj).OrganizationID.ToString();
            }
            catch
            {
                this.DropDownListOrg.SelectedIndex = 0;
            }
		}

		
		protected override bool ValidateInput()
		{
			PageCheckManager manager = new PageCheckManager();

			manager.Add( new LengthCheck(this.lblStepSequenceDescriptionEdit, this.txtStepSequenceDescriptionEdit, 100, false) );			
			manager.Add( new LengthCheck(this.lblStepSequenceCodeEdit, this.txtStepSequenceCodeEdit, 40, true) );			
			manager.Add( new LengthCheck(this.lblSegmentCodeEdit, this.drpSegmentCodeEdit, 40, true) );
            manager.Add(new LengthCheck(this.lblShiftTypeCodeEdit, this.drpShifTypeCode, 40, true));
            manager.Add(new LengthCheck(this.lblOrgEdit, this.DropDownListOrg, 8, true));
            manager.Add(new LengthCheck(this.lblBigStepSequenceSEQ, this.txtBigStepSequenceSEQ, 10, true));
            manager.Add(new NumberCheck(this.lblBigStepSequenceSEQ,this.txtBigStepSequenceSEQ, 0,999999999, false));
            manager.Add(new LengthCheck(this.lblBigStepSequenceCodeEdit, this.DropDownListBigStepSequenceCodeEdit, 10,true));
            
			if ( !manager.Check() )
			{
				WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
				return false;
			}

			return true;
		}
       
      
		#endregion

		#region 数据初始化
		protected void drpSegmentCodeEdit_Load(object sender, System.EventArgs e)
		{
             if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.drpSegmentCodeEdit);
                if (_facade == null)
                {
                    _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
                }
                builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._facade.GetAllSegment);

                builder.Build("SegmentDescription", "SegmentCode");
                this.drpSegmentCodeEdit.Items.Insert(0, new ListItem("", ""));
            }
			
		}
        protected void drpShifTypeCode_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.drpShifTypeCode);
                if (_shiftFacade == null)
                {
                    _shiftFacade = new ShiftModelFacadeFactory(base.DataProvider).Create();
                }
                builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._shiftFacade.GetAllShiftType);
                builder.Build("ShiftTypeDescription", "ShiftTypeCode");
                this.drpShifTypeCode.Items.Insert(0, new ListItem("", ""));
            }
        }


        protected void drpBIGSSEdit_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListBigStepSequenceCodeEdit);
                SystemSettingFacade facadenew = new SystemSettingFacade(base.DataProvider);
                builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(facadenew.GetAllBIGSSCODE);
                builder.Build("ParameterAlias", "ParameterAlias");
                this.DropDownListBigStepSequenceCodeEdit.Items.Insert(0, new ListItem("", ""));
            }
        }


        protected void drpBIGSSQuery_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListBigStepSequenceCodeQuery);
                SystemSettingFacade facadenew = new SystemSettingFacade(base.DataProvider);
                builder.HandleGetObjectList = new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(facadenew.GetAllBIGSSCODE);
                builder.Build("ParameterAlias", "ParameterAlias");
                this.DropDownListBigStepSequenceCodeQuery.Items.Insert(0, new ListItem("", ""));
            }
        }

        private void BuildOrgList()
        {
            DropDownListBuilder builder = new DropDownListBuilder(this.DropDownListOrg);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetAllOrg);
            builder.Build("OrganizationDescription", "OrganizationID");
            this.DropDownListOrg.Items.Insert(0, new ListItem("", ""));
            this.DropDownListOrg.SelectedIndex = 0;
        }
        private object[] GetAllOrg()
        {
            BaseModelFacade facadeBaseModel = new BaseModelFacade(base.DataProvider);
            return facadeBaseModel.GetCurrentOrgList();
        }
		#endregion

		#region Export

		protected override string[] FormatExportRecord( object obj )
		{
			return new string[]{ 	
						            ((StepSequence)obj).BigStepSequenceCode.ToString(),
                                    ((StepSequence)obj).StepSequenceOrder.ToString(),	
                                    ((StepSequence)obj).StepSequenceCode.ToString(),
								   ((StepSequence)obj).StepSequenceDescription.ToString(),
								   ((StepSequence)obj).GetDisplayText("SegmentCode"),
                                   ((StepSequence)obj).GetDisplayText("ShiftTypeCode"),
								   ((StepSequence)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((StepSequence)obj).MaintainDate) };
		}

		protected override string[] GetColumnHeaderText()
		{
            return new string[] {	
                                    "BigStepSequenceCode",
                                    "BigStepSequenceSeq",
                                    "StepSequenceCode",
									"StepSequenceDescription",
									"StepSequenceSegmentCode",
									"ShiftTypeCode",
									"MaintainUser",	
									"MaintainDate"};
		}
		#endregion

		// Added by Icyer, add ClickCellButton event to maintain warehouse
        //private void gridWebGrid_ClickCellButton(object sender, CellEventArgs e)
        //{
        //    if( this.gridHelper.IsClickColumn( "StepSequence2Warehouse",e ) )
        //    {
        //        this.Response.Redirect( this.MakeRedirectUrl("../Warehouse/FStepSeq2WarehouseSP.aspx", new string[]{"sscode","segcode"}, new string[]{e.Cell.Row.Cells[1].Text.Trim(),e.Cell.Row.Cells[3].Text.Trim()}));
        //    }
        //}
        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
            if (commandName == "StepSequence2Warehouse")
            {
                this.Response.Redirect(this.MakeRedirectUrl("../Warehouse/FStepSeq2WarehouseSP.aspx", new string[] { "sscode", "segcode" }, new string[] { row.Items.FindItemByKey("StepSequenceCode").Value.ToString(), row.Items.FindItemByKey("StepSequenceSegmentCode").Value.ToString() }));
            }   
        }
        
        protected void drpSegmentCodeEdit_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            if (_facade == null)
            {
                _facade = new BaseModelFacadeFactory(base.DataProvider).Create();
            }
            Segment newSegment = (Segment)this._facade.GetSegment(this.drpSegmentCodeEdit.SelectedValue);
            if (newSegment == null)
            {
                this.drpShifTypeCode.SelectedValue = "";
            }
            else
            {
                this.drpShifTypeCode.SelectedValue = newSegment.ShiftTypeCode.ToString();
            }
            
        } 

		// Added end
	}
}
