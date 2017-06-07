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

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Rework;
using BenQGuru.eMES.Common ;
namespace BenQGuru.eMES.Web.Rework
{
	/// <summary>
	/// FRejectConfirmMP 的摘要说明。
	/// </summary>
	public partial class FRejectConfirmMP : BaseMPage
	{
    
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        public BenQGuru.eMES.Web.UserControl.eMESDate dateFrom ;
        public BenQGuru.eMES.Web.UserControl.eMESTime timeFrom ;

        public BenQGuru.eMES.Web.UserControl.eMESDate dateTo ;
        public BenQGuru.eMES.Web.UserControl.eMESTime timeTo ;

        private BenQGuru.eMES.Rework.ReworkFacade _facade ;//= ReworkFacadeFactory.Create() ;

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
            if(! this.IsPostBack)
			{
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);

                this.dateFrom.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now )) ;
                this.dateTo.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now )) ;
                this.timeFrom.Text = FormatHelper.ToTimeString( 0 ) ;
				this.timeTo.Text = FormatHelper.ToTimeString(FormatHelper.TOTimeInt ( DateTime.Now ) );
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

            this.gridHelper.AddColumn( "RunningCard" , "序列号" , null ) ;
            this.gridHelper.AddColumn( "RunningCardSequence", "顺序号",	null);
           // this.gridHelper.AddColumn( "BoxNo", "箱号",	null);
            this.gridHelper.AddColumn( "LOTNO", "批号",	null);
            this.gridHelper.AddColumn( "MOCode1", "工单",	null);
            this.gridHelper.AddColumn( "ItemCode1", "产品",	null);
            this.gridHelper.AddColumn( "ModelCode1", "产品别",	null);
            this.gridHelper.AddColumn( "RejectStatus1", "状态",	null);
            this.gridHelper.AddColumn( "OPCode1", "工序",	null);
            this.gridHelper.AddColumn( "RejectDate", "日期",	null);
            this.gridHelper.AddColumn( "RejectTime", "时间",	null);
            this.gridHelper.AddColumn( "RejectUser", "人员",	null);
            this.gridHelper.AddColumn( "ErrorCodeInfo", "不良信息",	null);

            this.gridHelper.Grid.Columns.FromKey("RunningCardSequence").Hidden = true ;


            this.gridHelper.AddDefaultColumn( true, false );
            this.gridHelper.ApplyLanguage( this.languageComponent1 );
        }
		
        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow( 
                new object[]{"false",
                                ((RejectEx)obj).RunningCard.ToString() ,
                                ((RejectEx)obj).RunningCardSequence.ToString() ,
            //                    "",
                                ((RejectEx)obj).LOTNO ,
                                ((RejectEx)obj).MOCode ,
                                ((RejectEx)obj).ItemCode ,
                                ((RejectEx)obj).ModelCode ,
                                this.languageComponent1.GetString( ((RejectEx)obj).RejectStatus ),
                                ((RejectEx)obj).OPCode ,
                                FormatHelper.ToDateString(((RejectEx)obj).RejectDate) ,
                                FormatHelper.ToTimeString(((RejectEx)obj).RejectTime) ,
                                ((RejectEx)obj).RejectUser ,
                                ((RejectEx)obj).ErrorCode        
                            }
                );        
		}

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
			if(this.txtBoxNoQuery.Text.Trim().Length > 0)
			{
				return null;
			}
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.QueryRejectEx(
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.drpModelQuery.SelectedValue )) , 
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOCodeQuery.Text )) , 
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtItemCodeQuery.Text )) ,
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtLotNoQuery.Text )) ,
                this.drpOpCodeQuery.SelectedValue ,
                this.dateFrom.Text ,
                this.timeFrom.Text ,
                this.dateTo.Text , 
                this.timeTo.Text ,
                this.drpRejectStatusQuery.SelectedValue,
                inclusive,exclusive  );
        }


        protected override int GetRowCount()
        {
			if(this.txtBoxNoQuery.Text.Trim().Length > 0)
			{
				return 0;
			}
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            return this._facade.QueryRejectExCount(
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtMOCodeQuery.Text )) , 
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtItemCodeQuery.Text )) ,
                FormatHelper.PKCapitalFormat( FormatHelper.CleanString( this.txtLotNoQuery.Text )) ,
                this.drpOpCodeQuery.SelectedValue ,
                this.dateFrom.Text ,
                this.timeFrom.Text ,
                this.dateTo.Text , 
                this.timeTo.Text ,
                this.drpRejectStatusQuery.SelectedValue
                );
        }

        #endregion

        #region Button
        #endregion

        #region Object <--> Page


        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {	
            decimal seq ;
            try
            {
                seq = decimal.Parse( row.Cells[2].Text.ToString() ) ;
            }
            catch
            {
                seq = 0 ;
            }
			if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
            object obj = _facade.GetReject( row.Cells[1].Text.ToString() ,seq );
			
            if (obj != null)
            {
                return (Reject)obj;
            }

            return null;
        }

		
        protected override bool ValidateInput()
        {
            return true ;
        }

        #endregion

        #region 数据初始化
        #endregion

        #region Export
        protected override string[] FormatExportRecord( object obj )
        {
            return new string[]{
                                ((RejectEx)obj).RunningCard.ToString() ,
                                "",
                                ((RejectEx)obj).LOTNO ,
                                ((RejectEx)obj).MOCode ,
                                ((RejectEx)obj).ItemCode ,
                                ((RejectEx)obj).ModelCode ,
                                this.languageComponent1.GetString( ((RejectEx)obj).RejectStatus ) ,
                                ((RejectEx)obj).OPCode ,
								 FormatHelper.ToDateString(((RejectEx)obj).RejectDate) ,
								 FormatHelper.ToTimeString(((RejectEx)obj).RejectTime) ,                                
                                ((RejectEx)obj).RejectUser ,
                                ((RejectEx)obj).ErrorCode        
                            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"序列号" ,
                                    "箱号",
                                    "批号",
                                    "工单",
                                    "产品",
                                    "产品别",
                                    "状态",
                                    "工序",
                                    "日期",
                                    "时间",
                                    "人员",
                                    "不良信息"
                                };
        }
        #endregion

        protected void drpModelQuery_Load(object sender, System.EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.drpModelQuery.Items.Clear() ;
                this.drpModelQuery.Items.Add( string.Empty ) ;

                object[] models = new ReworkFacadeFactory(base.DataProvider).CreateModelFacade().GetAllModels() ;
                if(models != null)
                {
                    foreach(BenQGuru.eMES.Domain.MOModel.Model model in models)
                    {
                        this.drpModelQuery.Items.Add( model.ModelCode ) ;
                    }
                }
            }
        }

        protected void drpRejectStatusQuery_Load(object sender, System.EventArgs e)
        {
            if(!this.IsPostBack)
            {
                this.drpRejectStatusQuery.Items.Clear() ;
                this.drpRejectStatusQuery.Items.Add( string.Empty ) ;

                ArrayList ary = InternalSystemVariable.Lookup("RejectStatus").Items ;
                if( ary != null )
                {
                    foreach(string item in ary)
					{
						if( string.Compare( item, RejectStatus.UnReject, true ) == 0 ) continue;
                        this.drpRejectStatusQuery.Items.Add( 
                            new ListItem(this.languageComponent1.GetString( item),item )) ;
                    }
                }

            }
        
        }

        protected void drpOpCodeQuery_Load(object sender, System.EventArgs e)
        {
            if( ! this.IsPostBack )
            {
                this.drpOpCodeQuery.Items.Clear() ;
                this.drpOpCodeQuery.Items.Add( string.Empty ) ;

                object[] objs = new ReworkFacadeFactory(base.DataProvider).CreateBaseModelFacade().GetAllOperation() ;
                if( objs != null )
                {
                    foreach(BenQGuru.eMES.Domain.BaseSetting.Operation obj in objs)
                    {
                        this.drpOpCodeQuery.Items.Add( obj.OPCode ) ;
                    }
                }
            }
        }

		//确认判退
        protected void cmdComfirm_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();

            if ( array.Count > 0 )
            {
				if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
                ArrayList objList = new ArrayList( array.Count );
			
                Reject obj  ;
                foreach (UltraGridRow row in array)
                {
                    obj = (Reject)this.GetEditObject(row) ;
                    objList.Add( obj );
                }
				ArrayList dealLots = this.GetLots(objList);					//要操作的批次
				ArrayList dealNoLotReject = this.GetNoLots(objList);		//要操作的个体集合

                //this._facade.UpdateReject( (Reject[])objList.ToArray( typeof(Reject)) );
				this._facade.ConfirmLots(dealLots);
				this._facade.ConfirmNoLots(dealNoLotReject);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
            }

        }

		#region 获取要操作对象

		//获取要操作的批次
		private ArrayList GetLots(ArrayList _rejects)
		{
			ArrayList lots = new ArrayList();
			foreach(Reject _reject in _rejects)
			{
				if( _reject.LOTNO.Trim()!=null && _reject.LOTNO.Trim()!=string.Empty )
				{
					if(!lots.Contains(_reject.LOTNO))
					{
						//如果列表中不存在重复的Lotno,添加到列表中
						lots.Add( _reject.LOTNO );
					}
				}
			}

			return lots;
		}
		//获取要操作的个体（不属于任何生产批次的判退信息）
		private ArrayList GetNoLots(ArrayList _rejects)
		{
			ArrayList lots = new ArrayList();
			foreach(Reject _reject in _rejects)
			{
				if( _reject.LOTNO.Trim() == null || _reject.LOTNO.Trim() == string.Empty )
				{
					lots.Add( _reject );
				}
			}
			return lots;
		}

		#endregion

		//取消确认
        protected void cmdCancelConfirm_ServerClick(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();

            if ( array.Count > 0 )
            {
				if(_facade==null){_facade = new ReworkFacadeFactory(base.DataProvider).Create();}
                ArrayList objList = new ArrayList( array.Count );
			
                Reject obj  ;
                foreach (UltraGridRow row in array)
                {
                    obj = (Reject)this.GetEditObject(row) ;
                    objList.Add( obj );
                }

				ArrayList dealLots = this.GetLots(objList);					//要操作的批次
				ArrayList dealNoLotReject = this.GetNoLots(objList);		//要操作的个体集合
				this._facade.UnConfirmLots(dealLots);
				this._facade.UnConfirmNoLots(dealNoLotReject);
                //this._facade.UpdateReject( (Reject[])objList.ToArray( typeof(Reject)) );

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );
            }
        }

		//取消判退
        protected void cmdCancelReject_ServerClick(object sender, System.EventArgs e)
        {
			/*发件人: Joanne Zhao 
				发送时间: 2005年9月21日 12:03
				收件人: Simone Xu; David Liu; Eric Zhao
				抄送: Helen Wang
				主题: 答复: 判退采集

				Dear All,
				
				判退采集目前夏新不会用到，可以将其隐掉。
				判退只是针对批进行处理，相对会简单很多。

				业务描述：

				批判退与批允收：以批为单位进行

				取消判退：以“批”为单位进行取消判退，判退后的批里边的序列号可以进行继续抽检

				 

				系统实现：

				批判退与批允收：FQC采集目前是以批为单位进行批确认

				取消判退：以个体带群体，选择要“取消判退”的序列号，系统提示将对序列号所属的“批”里的所有样本进行“取消判退”作业。

				 

				目前批的状态包含：初始、未检、在检、通过、判退5种状态

				取消判退后的产品状态希望回退到“在检状态”，然后继承之前“判退”采集前的纪录继续做采集。

				 

				现在程序上是否可以实现该功能，尚需Simone进行确认。

				有问题可随时MSN联系：Joannebenq@hotmail.com   

				最近很少看邮件 :-)

				Joanne
			 * */

			// modified by jessie lee,2005/9/28,实现取消批判退的功能
			ArrayList arrayList = this.gridHelper.GetCheckedRows();

			if( arrayList.Count>0 )
			{
				if(_facade==null)
				{
					_facade = new ReworkFacadeFactory(base.DataProvider).Create();
				}

				//检查这些批号是否可以取消判退
				ArrayList nolotReject = new ArrayList();		//没有批的判退信息集合
				ArrayList lotNOs = new ArrayList();
				for(int i=0; i<arrayList.Count; i++)
				{
					UltraGridRow row = (UltraGridRow)arrayList[i] ;
					Reject reject = (Reject)this.GetEditObject(row) ;
					if( reject.LOTNO.Trim()!=null && reject.LOTNO.Trim()!=string.Empty )
					{
						lotNOs.Add(reject.LOTNO);
					}
					else
					{
						//如果没有批号，对个体进行操作。
						nolotReject.Add(reject);
					}
				}

				//对没有批的判退信息集合的取消判退操作
				if(nolotReject.Count >0 )
				{
					_facade.CancelReject(nolotReject);
				}

				ArrayList lotsList = new ArrayList();
				for(int i=0; i<lotNOs.Count; i++)
				{
					bool isRepeat = false;
					for(int j=0; j<lotsList.Count; j++)
					{
						if(string.Compare((string)lotNOs[i],lotsList[j].ToString(),true)==0)
						{
							isRepeat = true ;
							break;
						}	
					}
					if(!isRepeat && (string)lotNOs[i] != string.Empty)
					{
						lotsList.Add(lotNOs[i]);
					}
				}

				if(!_facade.CheckLotToCancelReject(lotsList))
				{
					ExceptionManager.Raise(this.GetType() , "$Error_LOTs_Cannot_CancelReject" ) ;
					return;
				}
				
				/*added by jessie lee, 2005/11/30,
				 * 操作时间过长时添加进度条*/
				this.Page.Response.Write("<div id='mydiv' >"); 
				this.Page.Response.Write("_"); 
				this.Page.Response.Write("</div>"); 
				this.Page.Response.Write("<script>mydiv.innerText = '';</script>"); 
				this.Page.Response.Write("<script language=javascript>;"); 
				this.Page.Response.Write("var dots = 0;var dotmax = 10;function ShowWait()"); 
				this.Page.Response.Write("{var output; output = '正在处理,请稍后';dots++;if(dots>=dotmax)dots=1;"); 
				this.Page.Response.Write("for(var x = 0;x < dots;x++){output += '·';}mydiv.innerText =  output;}"); 
				this.Page.Response.Write("function StartShowWait(){mydiv.style.visibility = 'visible'; "); 
				this.Page.Response.Write("window.setInterval('ShowWait()',1000);}"); 
				this.Page.Response.Write("function HideWait(){mydiv.style.display = 'none';"); 
				this.Page.Response.Write("window.clearInterval();}"); 
				this.Page.Response.Write("StartShowWait();</script>"); 
				this.Page.Response.Flush(); 

				if(lotsList != null && lotsList.Count > 0)
				{
					_facade.MakeLotsCancelReject(lotsList);
				}
					
				this.Page.Response.Write("<script language=javascript>HideWait();</script>"); 
				
				/* added by jessie lee, 2005/11/30,
				 * 取消判退完成以后，把结束时间改为当前 */
				this.dateTo.Text = FormatHelper.ToDateString( FormatHelper.TODateInt( DateTime.Now ));
				this.timeTo.Text = FormatHelper.ToTimeString( FormatHelper.TOTimeInt( DateTime.Now ));

				this.gridHelper.RequestData();
				this.buttonHelper.PageActionStatusHandle( PageActionType.Delete );

			}           
        }

    }
}
