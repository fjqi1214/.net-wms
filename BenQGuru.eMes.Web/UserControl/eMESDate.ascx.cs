namespace BenQGuru.eMES.Web.UserControl
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using BenQGuru.eMES.Web.Helper;

	/// <summary>
	///		ucDate 的摘要说明。
	/// </summary>
	public class eMESDate : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.TextBox GuruDate;
		protected System.Web.UI.WebControls.Table Table1;
		
		public string Enable
		{
			set
			{
				this.Attributes["isEnable"] = value;
			}
			get
			{
				if ( this.Attributes["isEnable"] == null )
				{
					return "true";
				}
				return this.Attributes["isEnable"];
			}
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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

		/// <summary>
		/// 表示一个空时间，只读
		/// </summary>
		public DateTime DateTime_Null
		{
			get
			{
				return DateTime.MinValue;
			}
		}
		/// <summary>
		/// 控件的客户端ID
		/// </summary>
		public string DateClientID
		{
			get
			{
				return GuruDate.ClientID;
			}
		}

		/// <summary>
		/// 当前时间
		/// </summary>
		public string Date_String
		{
			get
			{
				return Text;
			}
			set
			{
				Text=value;
			}
		}

		/// <summary>
		/// 使用字符串形式的日期来设置输入控件的日期值
		/// </summary>
		/// <remarks>add by tilanc yao 20041105 加入此属性是为了在替换原来的text控件时不需要修改设置代码</remarks>
		public string Text
		{
			get
			{
				return GuruDate.Text;
			}
			set
			{
				string _dateStr = "";
				if (value == null || value.Length == 0)
				{
				}
				else
				{
					 _dateStr = DateTime.Parse(value).ToString("yyyy-MM-dd");
				}
				GuruDate.Text=_dateStr;
				GuruDate.Attributes["tab"] = _dateStr;
			}
		}

		/// <summary>
		/// 使用日期形式来设置时间
		/// </summary>
		public DateTime Date_DateTime
		{
			get
			{
				if( GuruDate.Text.Trim().Length == 0)
				{
					return DateTime_Null;
				}
				return DateTime.Parse(GuruDate.Text);
			}
			set
			{
				if( value == DateTime_Null)
				{
					GuruDate.Text="";
				}
				else
				{
					GuruDate.Text=value.ToString("yyyy-MM-dd");
				}
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		public void Clear()
		{
			GuruDate.Text="";
		}

		/// <summary>
		/// 日期输入框宽度
		/// </summary>
		public int Width
		{
			set
			{
				GuruDate.Width=value;
			}
		}

		/// <summary>
		/// 日期输入框是否可以输入
		/// </summary>
		/// <remarks>add by tilancs yao 20041104</remarks>
		public bool DateTextIsReadOnly
		{
			set
			{
				this.GuruDate.ReadOnly = value;
			}
			get
			{
				return this.GuruDate.ReadOnly;
			}
		}

		/// <summary>
		/// 获得应用程序的根路径
		/// </summary>
		/// <returns></returns>
		/// <remarks>add by tilancs yao 20041104</remarks>
		/// <remarks>add by tilancs yao 20050207 重构获得取得前缀的方法</remarks>
		public string GetBaseUrl()
		{
			return this.VirtualHostRoot ;
		}

		/// <summary>
		/// 获得日期检查脚本文件路径
		/// </summary>
		/// <returns></returns>
		/// <remarks>add by tilancs yao 20041104</remarks>
		public string GetDateCheckJsFileUrl()
		{
			return GetBaseUrl()+"UserControl/DateTime/Script/DateCheck.js";
		}

		/// <summary>
		/// 获得日期选择控件脚本文件路径
		/// </summary>
		/// <returns></returns>
		/// <remarks>add by tilancs yao 20041104</remarks>
		public string GetCalendarJsFileUrl()
		{
			return GetBaseUrl()+"UserControl/DateTime/Script/calendarSrc.js";
		}

		public string VirtualHostRoot
		{
			get
			{
				return string.Format("{0}{1}"
					, this.Request.Url.Segments[0]
					, this.Request.Url.Segments[1]);
			}
		}
	
		public string CssClass
		{
			get
			{
				return this.GuruDate.CssClass;
			}
			set
			{
				this.GuruDate.CssClass = value;
			}
		}
	}
}
