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
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Domain.MOModel ;
using BenQGuru.eMES.Domain.BaseSetting ;

namespace BenQGuru.eMES.Web.Rework
{
	/// <summary>
	/// FReworkSheetEP 的摘要说明。
	/// </summary>
	public partial class FReworkSheetEP : BenQGuru.eMES.Web.Helper.BasePage 
	{
 		protected System.Web.UI.HtmlControls.HtmlInputButton cmdGridExport;
		protected BenQGuru.eMES.Web.Helper.PagerSizeSelector pagerSizeSelector;
		protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;
        private System.ComponentModel.IContainer components;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        //protected System.Web.UI.WebControls.DropDownList drpMOCode;

		

        private BenQGuru.eMES.Rework.ReworkFacade _facade ;//= ReworkFacadeFactory.Create();

		protected void Page_Load(object sender, System.EventArgs e)
		{
            if(!this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

                InitDropDowns();
                InitData();
                initUI() ;
            }
		}

        private void initUI()
        {
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
			//根据返工类型，处理界面，不需要清空数据
			DoReworkTypeChange(false) ;

			//根据工单，处理界面，不需要清空数据            
            DoMOCodeChange(false) ;

            string code = this.GetRequestParam("ReworkSheetCode") ;

            if (code != string.Empty)	//传参，即为编辑
            {
                Session["ReworkSheetCode"] = code; //供弹出页面使用
                //取得sheet的基本信息
                ReworkSheet rs = (ReworkSheet)(this._facade.GetReworkSheet(code));
                //状态为“新”，除了代码为都可以编辑
                if (rs.Status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW)
                {
                    this.txtReworkSheetCode.ReadOnly = true;
                }
                else
                {
                    this.txtDate.Enabled = false;
                    this.txtUser.Enabled = false;
                    this.txtDepartment.Enabled = false;
                    this.txtItemQuery.Readonly = true;
                    this.txtMOQuery.Readonly = true;
                    this.txtReworkHC.Enabled = false;
                    this.txtReworkQty.Enabled = false;
                    this.txtReworkSheetCode.Enabled = false;
                    //this.drpMOCode.Enabled = false ;
                    this.drpReworkSource.Enabled = false;
                    this.drpReworkType.Enabled = false;
                    //this.txtOQCLotQuery.Readonly = true;
                    this.chkNeedConfirmFlow.Enabled = false;
                    this.cmdSave.Disabled = true;
                    this.txtDutyCodeQuery.Readonly = true;
                    // Added by Icyer 2006/07/12
                    this.txtReworkReason.Enabled = false;
                    this.txtReasonAnalyse.Enabled = false;
                    this.txtSolution.Enabled = false;
                    if (rs.Status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW ||
                        rs.Status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_WAITING ||
                        rs.Status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_RELEASE ||
                        rs.Status == BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_OPEN)
                    {
                        this.txtReworkReason.Enabled = true;
                        this.txtReasonAnalyse.Enabled = true;
                        this.txtSolution.Enabled = true;
                        this.cmdSave.Disabled = false;
                        this.txtDutyCodeQuery.Readonly = false;
                    }
                    // Added end
                }
            }
            else
            {
                Session["ReworkSheetCode"] = "";
            }


        }

        private void InitData()
        {
            string code = this.GetRequestParam("ReworkSheetCode") ;
			// 新建
            if( code == string.Empty )
            {                
                this.txtDate.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now) ) ;
                this.txtUser.Text = this.GetUserCode();
            }
            else	// 编辑
            {
				if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
                ReworkSheet rs = (ReworkSheet)(this._facade.GetReworkSheet( code ));
				if ( rs != null )
				{
					this.txtDate.Text = FormatHelper.ToDateString(rs.CreateDate) ;
					this.txtUser.Text = rs.CreateUser ;
					this.txtDepartment.Text = rs.Department ;
					//选中料号
					this.txtItemQuery.Text = rs.ItemCode ;
					//选中工单
					this.txtMOQuery.Text = rs.MOCode;
					//sammer kong 2005/05/17
					this.txtReworkHC.Text = rs.ReworkHC.ToString() ;
					this.txtReworkQty.Text = rs.ReworkQty.ToString() ;
					this.txtReworkSheetCode.Text = rs.ReworkCode ;

					//选中工单
					//SetDropDownListValue(drpMOCode,rs.MOCode);
					//选中返工来源
					SetDropDownListValue(drpReworkSource , rs.ReworkSourceCode) ;
					//选中返工类型
					SetDropDownListValue(drpReworkType , rs.ReworkType ) ;

					// Added by Icyer 2006/07/12
					this.txtReworkReason.Text = rs.ReworkReason;
					this.txtReasonAnalyse.Text = rs.ReasonAnalyse;
					this.txtSolution.Text = rs.Soluation;
					// Added end

                    this.txtDutyCodeQuery.Text = rs.DutyCode;
                    this.txtOQCLotQuery.Text = rs.LotList;
                    this.chkNeedConfirmFlow.Checked = rs.NeedCheck == "Y" ? true : false;
				}
				else
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_ReworkSheet_Lost");
				}
            }
        }

        private void InitDropDowns()
        {


            if( ! this.IsPostBack)
            {
				if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
                string code = this.GetRequestParam("ReworkSheetCode") ;

                if(code == null)
                {
                    code = string.Empty ;
                }


//                this.drpMOCode.Items.Clear();
//				
//                object[] mos = this._facade.GetAllReworkMOs(code);
//                drpMOCode.Items.Add( string.Empty ) ;
//                if(mos!=null)
//                {
//                    foreach(BenQGuru.eMES.Domain.MOModel.MO mo in mos)
//                    {
//                        this.drpMOCode.Items.Add( mo.MOCode ) ;
//                    }
//                }

                this.drpReworkType.Items.Clear();
                ArrayList ary = InternalSystemVariable.Lookup("ReworkType").Items ;
                if( ary != null )
                {
                    foreach(string reworkType in ary)
                    {
                        this.drpReworkType.Items.Add( 
                            new ListItem(this.languageComponent1.GetString( reworkType),reworkType )) ;
                    }
                }


                object[] sources = this._facade.GetAllReworkSource();
                
                if(sources != null)
                {
                    foreach(ReworkSource source in sources)
                    {
                        this.drpReworkSource.Items.Add( source.ReworkSourceCode ) ;
                    }
                }
            }
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

        protected void cmdSave_ServerClick(object sender, System.EventArgs e)
        {
            if( !this.ValidateInput())
            {
                return ;
            }
            if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            string code = this.GetRequestParam("ReworkSheetCode");
            ReworkSheet rs ;

            if(code == string.Empty)
            {
                // 新增操作
                rs = (ReworkSheet)(this._facade.CreateNewReworkSheet());
            }
            else
            {
                rs = (ReworkSheet)(this._facade.GetReworkSheet( code ) );
            }

            rs.ReworkCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtReworkSheetCode.Text ));

            rs.Department = FormatHelper.CleanString( this.txtDepartment.Text );
			
			rs.MOCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOQuery.Text ));
			
            //rs.MOCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.drpMOCode.SelectedValue ));

			rs.ItemCode = FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtItemQuery.Text ));
            rs.ReworkHC = System.Decimal.Parse( FormatHelper.CleanString( this.txtReworkHC.Text ));
            rs.ReworkSourceCode = FormatHelper.CleanString( this.drpReworkSource.SelectedValue) ;
            rs.ReworkType = FormatHelper.CleanString( this.drpReworkType.SelectedValue );
            rs.ReworkQty = System.Decimal.Parse( FormatHelper.CleanString( this.txtReworkQty.Text )) ;

			// Added by Icyer 2006/07/12
			rs.ReworkReason = this.txtReworkReason.Text;
			rs.ReasonAnalyse = this.txtReasonAnalyse.Text;
			rs.Soluation = this.txtSolution.Text;
			// Added end

            rs.DutyCode = this.txtDutyCodeQuery.Text.Trim().ToUpper();
            rs.MaintainUser = this.GetUserCode();
            rs.LotList = this.txtOQCLotQuery.Text.Trim();
            rs.NeedCheck = this.chkNeedConfirmFlow.Checked ? "Y" : "N";
            
            
            if(code == string.Empty)
            {
                // 新增操作
                if (rs.NeedCheck == "Y")
                {
                    rs.Status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW;
                }
                else
                {
                    rs.Status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_RELEASE;
                }

                rs.CreateDate = FormatHelper.TODateInt( DateTime.Now ) ;
                rs.CreateTime = FormatHelper.TOTimeInt( DateTime.Now ) ;
                rs.CreateUser = this.GetUserCode();

                this._facade.AddReworkSheet(rs);
            }
            else
            {
                this._facade.UpdateReworkSheet( rs );
            }

			this.Response.Redirect(this.MakeRedirectUrl("./FReworkSheetMP.aspx"));

        }

        protected void cmdCancel_ServerClick(object sender, System.EventArgs e)
		{
			this.Response.Redirect(this.MakeRedirectUrl("./FReworkSheetMP.aspx"));
        }

        private bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add( new LengthCheck(lblReworkMo, txtReworkSheetCode, 40, true) );
            //manager.Add( new LengthCheck(lblDepartment,txtDepartment,40,false)) ;

            
            manager.Add( new DecimalCheck(lblReworkQty,txtReworkQty,false)) ;
            //manager.Add( new DecimalCheck(lblReworkHC,txtReworkHC,false)) ;

			//manager.Add( new LengthCheck(this.lblItemCode , this.txtItemQuery , 40 , true )) ;
            manager.Add( new LengthCheck(this.lblReworkSourceCode , this.drpReworkSource , 40 , true )) ;

//
//            if( this.drpReworkType.SelectedValue == BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_REMO)
//            {
//                manager.Add( new LengthCheck(this.lblMOCode , this.drpMOCode , 40 , true )) ;
//            }

            if ( !manager.Check() )
            {
                WebInfoPublish.Publish(this, manager.CheckMessage,this.languageComponent1);
                return false;
            }

            if (string.Compare(this.drpReworkType.SelectedValue,ReworkType.REWORKTYPE_ONLINE,true) == 0)
            {
                if (this.txtItemQuery.Text == string.Empty)
                {
                    WebInfoPublish.Publish(this, this.lblItemCodeQuery.Text + "$Error_Input_Empty", this.languageComponent1);
                    return false;
                }
            }
            else
            {
                if (this.txtMOQuery.Text == string.Empty)
                {
                    WebInfoPublish.Publish(this, this.lblMOQuery.Text + "$Error_Input_Empty", this.languageComponent1);
                    return false;
                }
                else
                {
                    object mo = new ReworkFacadeFactory(base.DataProvider).CreateMOFacade().GetMO(this.txtMOQuery.Text.Trim().ToUpper());
                    if (mo != null)
                    {
                        //获得返工工单的计划数量，即为返工数量
                        this.txtReworkQty.Text = ((MO)mo).MOPlanQty.ToString();
                        //根据工单的料品，选择料品
                        this.txtItemQuery.Text = ((MO)mo).ItemCode;
                    }
                }
            }

			manager.Add( new LengthCheck(lblReworkReason,txtReworkReason,200,false)) ;
			manager.Add( new LengthCheck(lblReasonAnalyse,txtReasonAnalyse,200,false)) ;
			manager.Add( new LengthCheck(lblSolution,txtSolution,200,false)) ;	
			
			return true ;
        }


        private void SetDropDownListValue(System.Web.UI.WebControls.DropDownList list,string selectedValue)
        {
			try
			{
				list.SelectedValue = selectedValue ;
			}
			catch
			{
				list.SelectedIndex = 0 ;
			}

        }

        protected void drpReworkType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DoReworkTypeChange(true) ;
        }


		/// <summary>
		/// 根据返工类型，需要改变的是料品和工单
		/// </summary>
		/// <param name="resetData"></param>
        private void DoReworkTypeChange(bool resetData)
        {
            if( resetData )	//重置数据
            {
				this.txtItemQuery.Text = string.Empty;
				this.txtMOQuery.Text = string.Empty;
                //this.drpMOCode.SelectedIndex = 0 ;
            }

			//在线返工
			if( this.drpReworkType.SelectedValue == BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_ONLINE)
			{
				//工单不可选择，因为返工的东西来自不同工单
				//this.drpMOCode.Enabled = false ;
				this.txtMOQuery.Readonly = true;
                
				//料品可选择，返工的号码必须隶属于同一料品
				this.txtItemQuery.Readonly = false;

				//数量需要编辑
				this.txtReworkQty.Enabled = true ;
                //可以选择判退批号
                //this.txtOQCLotQuery.Readonly = false;

			}//离线返工
			else if( this.drpReworkType.SelectedValue == BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_REMO)	
			{
				//工单需要选择
				//this.drpMOCode.Enabled = true ;
				this.txtMOQuery.Readonly = false;
				//料品自动带出
				this.txtItemQuery.Readonly = true;
				//返工数量自动带出
				this.txtReworkQty.Enabled = false ;
                //清空判退批号
                this.txtOQCLotQuery.Text = string.Empty;
                //this.txtOQCLotQuery.Readonly = true;
			}
			else
			{
				//工单需要选择
				//this.drpMOCode.Enabled = true ;
				this.txtMOQuery.Readonly = false;
				//返工数量自动带出
				this.txtReworkQty.Enabled = false ;
			}
        }

//        private void drpMOCode_SelectedIndexChanged(object sender, System.EventArgs e)
//        {
//            DoMOCodeChange(true) ;
//        }

//		private void cmdchangeMO_ServerClick(object sender, System.EventArgs e)
//		{
//			//当前待维护工单改变
//			DoMOCodeChange(true) ;
//		}


		/// <summary>
		/// 根据工单处理返工数量的问题
		/// </summary>
		/// <param name="resetData"></param>
        private void DoMOCodeChange(bool resetData)
        {
            // 工单没有选中，应该是在线返工
            //if( this.drpMOCode.SelectedValue == string.Empty )
			if(this.txtMOQuery.Text == string.Empty)
            {
                if( resetData )	//如果需要清空
                {
                    //this.txtReworkQty.Text = string.Empty;
					//this.txtItemQuery.Text = string.Empty;
                }
            }	
            else	//离线返工
            {
                //object mo = new ReworkFacadeFactory(base.DataProvider).CreateMOFacade().GetMO( this.drpMOCode.SelectedValue) ;
				object mo = new ReworkFacadeFactory(base.DataProvider).CreateMOFacade().GetMO( this.txtMOQuery.Text) ;
				if(mo!=null)
				{
					//获得返工工单的计划数量，即为返工数量
					this.txtReworkQty.Text = ((MO)mo).MOPlanQty.ToString() ;
					//根据工单的料品，选择料品
					this.txtItemQuery.Text = ((MO)mo).ItemCode ;
				}
            }
        }
	}
}
