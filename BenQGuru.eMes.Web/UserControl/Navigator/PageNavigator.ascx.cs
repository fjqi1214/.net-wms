namespace BenQGuru.eMES.Web.UserControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;
	using BenQGuru.eMES.Web.Helper;

	/// <summary>
	///		PageNavigator 的摘要说明。
	/// </summary>
	public class PageNavigator : System.Web.UI.UserControl
	{
		private string _arrowText = " -> ";
		private string _targetFrame = "";
		private ArrayList _linkList = new ArrayList();

		public string ArrowText
		{
			get
			{
				return this._arrowText;
			}
			set
			{
				this._arrowText = value;
			}
		}

		public string TargetFrame
		{
			get
			{
				return this._targetFrame;
			}
			set
			{
				this._targetFrame = value;
			}
		}

		public System.Web.UI.WebControls.HyperLink[] HyperLinks
		{
			get
			{
				return (HyperLink[])this._linkList.ToArray(typeof(HyperLink));
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion

		public void AddRootPageNavigator(string text, string navigatorUrl)
		{		
			System.Web.UI.WebControls.HyperLink rootLink = new HyperLink();

			rootLink.Text = text;
			rootLink.Target = this.TargetFrame;

			this.Controls.AddAt(0, rootLink);
			this._linkList.Insert(0, rootLink );
		}

		public void AddPageNavigator( string text, string navigatorUrl )
		{
			System.Web.UI.WebControls.Label arrawLabel = new Label();
			arrawLabel.Text = ArrowText;
			this.Controls.Add( arrawLabel);

			System.Web.UI.WebControls.HyperLink link = new HyperLink();

			link.Text = text;
			if(navigatorUrl.Trim()!=string.Empty && text!="主页")
			{
				link.Style.Add("color","blue");
				link.NavigateUrl = navigatorUrl;
			}
			link.Target = this.TargetFrame;

			this.Controls.Add( link );
			this._linkList.Add( link );
		}
		
		public void AddPageNavigator( string text, string navigatorUrl, int index )
		{
			System.Web.UI.WebControls.Label arrawLabel = new Label();
			arrawLabel.Text = ArrowText;
			this.Controls.Add( arrawLabel);

			System.Web.UI.WebControls.HyperLink link = new HyperLink();

			link.Text = text;
			if(navigatorUrl.Trim()!=string.Empty && text!="主页")
			{
				link.Style.Add("color","blue");
				
				link.NavigateUrl = navigatorUrl;
			}
			link.Target = this.TargetFrame;

			this.Controls.AddAt( index, link );
			this._linkList.Insert( index, link );
		}

		public void Clear()
		{
			this.Controls.Clear();
			this._linkList.Clear();
		}

		private string MakeRedirectUrl( string url, string[] names, string[] values )
		{
			string[] paramList = new string[names.Length];

			for ( int i=0; i < names.Length; i++ )
			{
				if ( names[i] == null || names[i] == "" )
				{
					continue;
				}

				if ( i > values.Length-1 )
				{
					paramList[i] = string.Format("{0}={1}", Server.UrlEncode(names[i]), string.Empty);
				}
				else
				{
					if ( values[i] == null )
					{
						paramList[i] = string.Format("{0}={1}", Server.UrlEncode(names[i]), string.Empty);

					}
					else
					{
						paramList[i] = string.Format("{0}={1}", Server.UrlEncode(names[i]), Server.UrlEncode(values[i]));
					}
				}
			}

			return Server.UrlPathEncode( string.Format("{0}?{1}", url, string.Join("&", paramList)) );
		}
	}
}
