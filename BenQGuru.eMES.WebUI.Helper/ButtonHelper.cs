using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// ButtonHelper 的摘要说明。
	/// </summary>
	
	public delegate void SetEditObjectDelegate( object obj );
	public delegate void PageStatusChangeDelegate( string pageAction );

	public class ButtonHelper
	{
		private System.Web.UI.Page _page = null;
		private Hashtable _cmdList = null;
		public SetEditObjectDelegate SetEditObjectHandle = null;
		public PageStatusChangeDelegate AfterPageStatusChangeHandle	 = null;
		public ControlLibrary.Web.Language.LanguageComponent languageControl;

		#region 属性
		public System.Web.UI.Page Page
		{
			get
			{
				return this._page;
			}
			set
			{
				this.FindButtons( value );
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdAdd
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Add) && this._cmdList[PageActionType.Add] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Add];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Add] = value;
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdSelect
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Select) && this._cmdList[PageActionType.Select] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Select];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Select] = value;
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdCancel
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Cancel) && this._cmdList[PageActionType.Cancel] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Cancel];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Cancel] = value;
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdSave
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Save) && this._cmdList[PageActionType.Save] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Save];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Save] = value;
			}
		}
		//joe 
		public System.Web.UI.HtmlControls.HtmlInputButton CmdConfined
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Confined) && this._cmdList[PageActionType.Confined] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Confined];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Confined] = value;
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdUnLock
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.UnLock) && this._cmdList[PageActionType.UnLock] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.UnLock];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.UnLock] = value;
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdDelete
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Delete) && this._cmdList[PageActionType.Delete] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Delete];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Delete] = value;
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdQuery
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Query) && this._cmdList[PageActionType.Query] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Query];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Query] = value;
			
			}
		}

		public System.Web.UI.HtmlControls.HtmlInputButton CmdExport
		{
			get
			{
				if ( this._cmdList.Contains(PageActionType.Export) && this._cmdList[PageActionType.Export] != null )
				{
					return (HtmlInputButton)this._cmdList[PageActionType.Export];
				}

				return null;
			}
			set
			{
				this._cmdList[PageActionType.Export] = value;
			
			}
		}

        //add by jinger
        public System.Web.UI.HtmlControls.HtmlInputButton CmdExport2
        {
            get
            {
                if (this._cmdList.Contains(PageActionType.Export2) && this._cmdList[PageActionType.Export2] != null)
                {
                    return (HtmlInputButton)this._cmdList[PageActionType.Export2];
                }

                return null;
            }
            set
            {
                this._cmdList[PageActionType.Export2] = value;

            }
        }
        //end add

        //benny
        public System.Web.UI.HtmlControls.HtmlInputButton CmdTest
        {
            get
            {
                if (this._cmdList.Contains(PageActionType.Test) && this._cmdList[PageActionType.Test] != null)
                {
                    return (HtmlInputButton)this._cmdList[PageActionType.Test];
                }

                return null;
            }
            set
            {
                this._cmdList[PageActionType.Test] = value;
            }
        }
        public System.Web.UI.HtmlControls.HtmlInputButton CmdSure
        {
            get
            {
                if (this._cmdList.Contains(PageActionType.Sure) && this._cmdList[PageActionType.Sure] != null)
                {
                    return (HtmlInputButton)this._cmdList[PageActionType.Sure];
                }

                return null;
            }
            set
            {
                this._cmdList[PageActionType.Sure] = value;
            }
        }
		#endregion

		public ButtonHelper()
		{
		}

		public ButtonHelper( System.Web.UI.Page page )
		{
			this.FindButtons( page );
		}

		private void FindButtons( System.Web.UI.Page page )
		{
			this._page = page;

			this._cmdList = new Hashtable();

			this._cmdList.Add( PageActionType.Add,			this._page.FindControl("cmdAdd") );
			this._cmdList.Add( PageActionType.Select,		this._page.FindControl("cmdSelect") );
			this._cmdList.Add( PageActionType.Delete,		this._page.FindControl("cmdDelete") );
			this._cmdList.Add( PageActionType.Save,			this._page.FindControl("cmdSave") );
			this._cmdList.Add( PageActionType.Cancel,		this._page.FindControl("cmdCancel") );
			this._cmdList.Add( PageActionType.Query,		this._page.FindControl("cmdQuery") );
            this._cmdList.Add(PageActionType.Export,        this._page.FindControl("cmdGridExport"));
            this._cmdList.Add(PageActionType.Export2,        this._page.FindControl("cmdGridExport2"));
			this._cmdList.Add( PageActionType.Confined,		this._page.FindControl("cmdConfined") );//joe
			this._cmdList.Add( PageActionType.UnLock,		this._page.FindControl("cmdUnLock") );
            this._cmdList.Add(PageActionType.Test,          this._page.FindControl("cmdTest"));//benny
            this._cmdList.Add(PageActionType.Sure,          this._page.FindControl("cmdSure"));//benny
		}

		public void PageActionStatusHandle(string pageAction)
		{
			if (pageAction == PageActionType.Add)
			{
				if ( this.CmdAdd != null )
				{
					this.CmdAdd.Disabled = false;
				}
				if ( this.CmdSelect != null )
				{
					this.CmdSelect.Disabled = false;
				}

				if ( this.CmdSave != null )
				{
					this.CmdSave.Disabled = true;
				}
				if ( this.CmdConfined != null )
				{
					this.CmdConfined.Disabled = true;
				}
				if ( this.CmdUnLock != null )//joe 一般情况按键泛灰
				{
					this.CmdUnLock.Disabled = true;
				}
				
				this.ClearPage();
				this.AfterPageStatusChange(PageActionType.Add);
				return;
			}
			
			if (pageAction == PageActionType.Select)
			{
				if ( this.CmdAdd != null )
				{
					this.CmdAdd.Disabled = false;
				}
				if ( this.CmdSelect != null )
				{
					this.CmdSelect.Disabled = false;
				}

				if ( this.CmdSave != null )
				{
					this.CmdSave.Disabled = true;
				}
				if ( this.CmdConfined != null )
				{
					this.CmdConfined.Disabled = true;
				}
				if ( this.CmdUnLock != null )//joe 一般情况按键泛灰
				{
					this.CmdUnLock.Disabled = true;
				}
				
				this.ClearPage();
				this.AfterPageStatusChange(PageActionType.Select);
				return;
			}

			if (pageAction == PageActionType.Update)
			{
				if ( this.CmdAdd != null )
				{
					this.CmdAdd.Disabled = true;
				}
				if ( this.CmdSelect != null )
				{
					this.CmdSelect.Disabled = true;
				}
				
				if ( this.CmdSave != null )
				{
					this.CmdSave.Disabled = false;
				}
				if ( this.CmdConfined != null )
				{
					this.CmdConfined.Disabled = false;
				}
				if ( this.CmdUnLock != null )//joe 点击编辑后激活按键
				{
					if(((BasePage)this._page).IsBelongToAdminGroup())
					{
						this.CmdUnLock.Disabled = false;
					}
				}
					
				this.AfterPageStatusChange(PageActionType.Update);
				return;
			}

			if (pageAction == PageActionType.Save)
			{
				this.PageActionStatusHandle(PageActionType.Add);
				this.AfterPageStatusChange(PageActionType.Save);
				return;
			}

			if (pageAction == PageActionType.Delete)
			{
				this.PageActionStatusHandle(PageActionType.Add);
				this.AfterPageStatusChange(PageActionType.Delete);
				return;
			}

			if (pageAction == PageActionType.Cancel)
			{
				this.PageActionStatusHandle(PageActionType.Add);
				this.AfterPageStatusChange(PageActionType.Cancel);
				return;
			}

			if (pageAction == PageActionType.Query)
			{
				this.PageActionStatusHandle(PageActionType.Add);
				this.AfterPageStatusChange(PageActionType.Query);
				return;
			}
		}

		public void ClearPage()
		{
			if ( this.SetEditObjectHandle != null )
			{
				this.SetEditObjectHandle( null );
			}
		}

		public void AfterPageStatusChange( string pageAction )
		{
			if ( this.AfterPageStatusChangeHandle != null )
			{
				this.AfterPageStatusChangeHandle( pageAction ); 
			}
		}

		public void AddButtonAttribute( System.Web.UI.HtmlControls.HtmlInputButton btn, string attributeKey, string content )
		{
			btn.Attributes[attributeKey] = content;
		}

		public void AddButtonConfirm( System.Web.UI.HtmlControls.HtmlInputButton btn, string content )
		{
            this.AddButtonAttribute(btn, "onclick", "{" + string.Format(" return confirm('{0}'); ", content) + "}");
		}

		public void AddDeleteConfirm()
		{
//			if ( this.CmdDelete != null )
//			{
//				this.AddButtonAttribute( this.CmdDelete, "onclick", "{ return confirm('是否确认删除？'); }" );
//			}
		}
	}	

	public class PageActionType
	{
		public static  string Add			= "Add";
		public static  string Select		= "Select";
		public static  string Delete		= "Delete";
		public static  string Save			= "Save";
		public static  string Update		= "Update";
		public static  string Cancel		= "Cancel";
		public static  string Query			= "Query";
        public static string Export         = "Export";
        public static string Export2        = "Export2";
		public static  string Confined		= "Confined";//joe
		public static  string UnLock		= "Unlock";
        public static string Test           = "Test";//benny
        public static string Sure           = "Sure";//benny
	}

}
