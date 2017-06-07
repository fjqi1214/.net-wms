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
			this._cmdList.Add( PageActionType.Delete,		this._page.FindControl("cmdDelete") );
			this._cmdList.Add( PageActionType.Save,			this._page.FindControl("cmdSave") );
			this._cmdList.Add( PageActionType.Cancel,		this._page.FindControl("cmdCancel") );
			this._cmdList.Add( PageActionType.Query,		this._page.FindControl("cmdQuery") );
			this._cmdList.Add( PageActionType.Export,		this._page.FindControl("cmdGridExport") );
		}

		public void PageActionStatusHandle(string pageAction)
		{
			if (pageAction == PageActionType.Add)
			{
				if ( this.CmdAdd != null )
				{
					this.CmdAdd.Disabled = false;
				}

				if ( this.CmdSave != null )
				{
					this.CmdSave.Disabled = true;
				}
				
				this.ClearPage();
				this.AfterPageStatusChange(PageActionType.Add);
				return;
			}

			if (pageAction == PageActionType.Update)
			{
				if ( this.CmdAdd != null )
				{
					this.CmdAdd.Disabled = true;
				}

				if ( this.CmdSave != null )
				{
					this.CmdSave.Disabled = false;
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
			this.AddButtonAttribute( btn, "onclick", string.Format("{ return confirm('{0}'); }", content) );
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
		public static  string Delete		= "Delete";
		public static  string Save			= "Save";
		public static  string Update		= "Update";
		public static  string Cancel		= "Cancel";
		public static  string Query			= "Query";
		public static  string Export		= "Export";

	}

}
