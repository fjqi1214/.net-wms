namespace BenQGuru.eMES.Web.UserControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;


	/// <summary>
	///		ucTime 的摘要说明。
	/// </summary>
	public partial class eMESTime : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
		}

		public string Text
		{
			get
			{
				if ( this.txtTimeEditor.Text.Trim() == string.Empty )
				{
					return string.Empty;
				}

				return TimeString;
			}
			set
			{
				if ( value == string.Empty )
				{
					this.txtTimeEditor.Text = "";
				}
				else
				{
					TimeString = value;
				}
			}
		}

		/// <summary>
		/// 获取或设置时间字符串
		/// </summary>
		public string TimeString
		{
			get
			{
				return this.txtTimeEditor.Date.ToString("HH:mm:ss");
			}
			set
			{
				try
				{
					this.txtTimeEditor.Date=DateTime.Parse(value,(new CultureInfo("fr-FR",false)).DateTimeFormat);
				}
				catch//(Exception e)
				{
					this.txtTimeEditor.Text = "";
				}
			}
		}

		/// <summary>
		/// 获取或设置宽度
		/// </summary>
		public int Width
		{
			get
			{
				return (int)this.txtTimeEditor.Width.Value;
			}
			set
			{
				this.txtTimeEditor.Width=new Unit(value);
			}
		}

		/// <summary>
		/// 新增属性
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void AddAttribute(string key,string value)
		{
			this.txtTimeEditor.Attributes.Add(key,value);
		}

		/// <summary>
		/// 删除属性
		/// </summary>
		/// <param name="key"></param>
		public void RemoveAttribute(string key)
		{
			this.txtTimeEditor.Attributes.Remove(key);
		}

		public string CssClass
		{
			get
			{
				return this.txtTimeEditor.CssClass;
			}
			set
			{
				this.txtTimeEditor.CssClass = value;
			}
		}

		public bool Enabled
		{
			get
			{
				return this.txtTimeEditor.Enabled;
			}
			set
			{
				this.txtTimeEditor.Enabled=value;
			}
		}
		public new string ClientID
		{
			get
			{
				return this.txtTimeEditor.ClientID;
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
		///		设计器支持所需的方法 - 不要使用代码编辑器
		///		修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
