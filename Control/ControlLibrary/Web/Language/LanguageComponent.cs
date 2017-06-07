using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Xml; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls ;  
using BenQGuru.eMES.Common.MutiLanguage;

namespace ControlLibrary.Web.Language
{

	/// <summary>
	/// LanguageComponent 的摘要说明。
	/// </summary>
	[Designer(typeof(LanguageComponentDesigner))]
	public class LanguageComponent : Component, ILanguageComponent
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string _language = string.Empty; 
		private string _languagePackageDir = string.Empty; 
		private string _languageFileName = string.Empty; 
		private string _userControlName = string.Empty; 
		private Page _runtimePage = null;
		private System.Web.UI.UserControl _runtimeUserControl = null;

		public event System.EventHandler OnLoadLanguage;

		public LanguageComponent()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitComponent 调用后添加任何初始化
		}

		public LanguageComponent(IContainer container)
		{
			container.Add(this);
			this.InitializeComponent();
		}


		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		private void WriteControl(Control ctrl, bool isSubControl)
		{
			string text1 = isSubControl ? "SubControl" : "Control";
			LanguageWord languageWord= new LanguageWord();
			if (ctrl is Label)
			{
				languageWord.ControlID		=  ctrl.ID;
				languageWord.ControlType = "Label";
				languageWord.ControlClass = ((Label) ctrl).Site.Name;  
				languageWord.ControlCHSText  = ((Label) ctrl).Text;
				LanguagePack.Current(this.LanguageProfileName).Insert(languageWord, this.Language);
			}

			if (ctrl is Literal)
			{
				languageWord.ControlID		=  ctrl.ID;
				languageWord.ControlType = "Literal";
				languageWord.ControlClass = ((Literal) ctrl).Site.Name;  
				languageWord.ControlCHSText  = ((Literal) ctrl).Text;
				LanguagePack.Current(this.LanguageProfileName).Insert(languageWord, this.Language);
			}
			else if (ctrl is Button)
			{
				languageWord.ControlID		=  ctrl.ID;
				languageWord.ControlType = "Button";
				languageWord.ControlClass = ((Button) ctrl).Site.Name;  
				languageWord.ControlCHSText  = ((Button) ctrl).Text;
				LanguagePack.Current(this.LanguageProfileName).Insert(languageWord, this.Language);
			}
			else if (ctrl is LinkButton)
			{
				languageWord.ControlID		=  ctrl.ID;
				languageWord.ControlType = "LinkButton";
				languageWord.ControlClass = ((LinkButton) ctrl).Site.Name;  
				languageWord.ControlCHSText  = ((LinkButton) ctrl).Text;
				LanguagePack.Current(this.LanguageProfileName).Insert(languageWord, this.Language);
			}

			else if (ctrl is HyperLink)
			{
				languageWord.ControlID		=  ctrl.ID;
				languageWord.ControlType = "HyperLink";
				languageWord.ControlClass = ((HyperLink) ctrl).Site.Name;  
				languageWord.ControlCHSText  = ((HyperLink) ctrl).Text;
				LanguagePack.Current(this.LanguageProfileName).Insert(languageWord, this.Language);
			}

			else if (ctrl is RadioButton)
			{
				languageWord.ControlID		=  ctrl.ID;
				languageWord.ControlType = "RadioButton";
				languageWord.ControlClass = ((RadioButton) ctrl).Site.Name;  
				languageWord.ControlCHSText  = ((RadioButton) ctrl).Text;
				LanguagePack.Current(this.LanguageProfileName).Insert(languageWord, this.Language);
			}
			else if (ctrl is CheckBox)
			{
				languageWord.ControlID		=  ctrl.ID;
				languageWord.ControlType = "CheckBox";
				languageWord.ControlClass = ((CheckBox) ctrl).Site.Name;  
				languageWord.ControlCHSText  = ((CheckBox) ctrl).Text;
				LanguagePack.Current(this.LanguageProfileName).Insert(languageWord, this.Language);
			}
//			else if ((ctrl is CheckBoxList) || (ctrl is DropDownList)  || (ctrl is ListBox))
//			{
//				foreach (ListItem item1 in ((ListControl) ctrl).Items)
//				{
//					languageWord.ControlID		=  ((ListControl) ctrl).ID+ "_" + item1.Value;
//					languageWord.ControlType = "ListItem";
//					languageWord.ControlClass = ((ListControl) ctrl).Site.Name;  
//					languageWord.ControlCHSText  = item1.Text;
//				}
//
//				try
//				{
//					ControlLibrary.Domian.LanguagePack.Current.Insert(languageWord);
//				}
//				catch(Exception ex)
//				{
//					System.Windows.Forms.MessageBox.Show(ex.Message);    
//				}
//			}
			else
			{
				if (!(ctrl is Panel))
				{
					return;
				}
				foreach (Control control2 in ((Panel) ctrl).Controls)
				{
					this.WriteControl(control2, false);
				}
			}
		}

		private void ReadControl(Control ctrl)
		{ 
			LanguageWord languageWord= LanguagePack.Current(this.LanguageProfileName).GetLanguageWord(ctrl.ID, this.Language);
			
			if (ctrl is Label)
			{
				if (languageWord != null)
				{
					((Label) ctrl).Text = languageWord.ControlText; 
				}
			}

			if (ctrl is Literal)
			{
				if (languageWord != null)
				{
					((Literal) ctrl).Text = languageWord.ControlText; 
				}
			}
			else if (ctrl is Button)
			{
				if (languageWord != null)
				{
					((Button) ctrl).Text = languageWord.ControlText; 
				}
			}
			else if (ctrl is LinkButton)
			{
				if (languageWord != null)
				{
					((LinkButton) ctrl).Text = languageWord.ControlText; 
				}
			}

			else if (ctrl is HyperLink)
			{
				if (languageWord != null)
				{
					((HyperLink) ctrl).Text = languageWord.ControlText; 
				}
			}

			else if (ctrl is RadioButton)
			{
				if (languageWord != null)
				{
					((RadioButton) ctrl).Text = languageWord.ControlText; 
				}

			}
			else if (ctrl is CheckBox)
			{
				if (languageWord != null)
				{
					((CheckBox) ctrl).Text = languageWord.ControlText; 
				}
			}
//			else if ((ctrl is CheckBoxList) || (ctrl is DropDownList)  || (ctrl is ListBox))
//			{
//				foreach (ListItem item1 in ((ListControl) ctrl).Items)
//				{
//					ControlLibrary.Domian.LanguageWord languageWord2 = ControlLibrary.Domian.LanguagePack.Current.GetLanguageWord(((ListControl) ctrl).ID+ "_" + item1.Value, this.Language);
//					item1.Text = 	languageWord2.ControlText;
//				}
//			}
			else
			{
				foreach (Control control2 in ctrl.Controls)
				{
					this.ReadControl(control2);
				}
			}
		}

		private ControlCollection DesignContainerControls
		{
			get
			{
				if(!this.DesignMode)
				{
					return null;
				}
				if (base.Container.Components.Count > 0)
				{
					object obj1 = base.Container.Components[0];
					if (obj1 is Page)
					{
						return ((Page) obj1).Controls;
					}
					if (obj1 is System.Web.UI.UserControl)
					{
						return ((System.Web.UI.UserControl) obj1).Controls;
					}
				}
				return null;
			}
		}


		private string DesginContainerBaseName
		{
			get
			{
				if(!this.DesignMode)
				{
					return null;
				}
				if (base.Container.Components.Count > 0)
				{
					object obj1 = base.Container.Components[0];
					if (obj1 is Page)
					{
						return ((Page) obj1).GetType().BaseType.ToString();
					}
					if (obj1 is System.Web.UI.UserControl)
					{
						return ((System.Web.UI.UserControl) obj1).GetType().ToString();
					}
				}
				return null;
			}
		}

		private string DesginContainerName
		{
			get
			{
				if(!this.DesignMode)
				{
					return null;
				}
				if (base.Container.Components.Count > 0)
				{
					object obj1 = base.Container.Components[0];
					if (obj1 is Page)
					{
						return ((Page) obj1).Site.Name;
					}
					if (obj1 is System.Web.UI.UserControl)
					{
						return this._userControlName;
					}
				}
				return null;
			}
		}

		private string RuntimeContainerName
		{
			get
			{
				if(this.DesignMode)
				{
					return null;
				}

				if (this.RuntimePage != null)
				{
					return this.RuntimePage.GetType().BaseType.Name;
				}
				else
				{
					return this._userControlName;
				}
			}
		}


//		private string DesignLanguageProfileName
//		{
//			get
//			{
//				string text1 = (this.LanguagePackageDir == string.Empty) ? string.Empty : (this.LanguagePackageDir + (this.LanguagePackageDir.EndsWith(@"\") ? "" : @"\"));
//				//string text2 = this.DesginContainerName;
//				//return string.Format("{0}{1}.{2}.xml", text1, text2, this.Language);
//				return string.Format("{0}{1}", text1, language.mdb);
//			}
//		}
//
//		private string RuntimeLanguageProfileName
//		{
//			get
//			{
//				string text1 = (this.LanguagePackageDir == string.Empty) ? string.Empty : (this.LanguagePackageDir + (this.LanguagePackageDir.EndsWith(@"\") ? @"Language\" : @"\Language\"));
//				string text2 = this.RuntimeContainerName;
//				return string.Format("{0}{1}.{2}.xml", text1, text2, this.Language);
//			}
//		}


		public string LanguageProfileName
		{
			get
			{
//				if (this.DesignMode)
//				{
//					return this.DesignLanguageProfileName; 
//				}
//				else
//				{
//					return this.RuntimeLanguageProfileName;
//				}
				string text1 = (this.LanguagePackageDir == string.Empty) ? string.Empty : (this.LanguagePackageDir + (this.LanguagePackageDir.EndsWith(@"\") ? "" : @"\"));
				return string.Format("{0}{1}", text1, "language.mdb");
			}
		}


		public string UserControlName
		{
			get
			{
				return this._userControlName;
			}
			set
			{
				this._userControlName = value;
			}
		}

		[Browsable(false)]
		public Page RuntimePage
		{
			get
			{
				return this._runtimePage;
			}
			set
			{
				this._runtimePage = value;
			}
		}

		[Browsable(false)]
		public System.Web.UI.UserControl RuntimeUserControl
		{
			get
			{
				return this._runtimeUserControl;
			}
			set
			{
				this._runtimeUserControl = value;
			}
		}



		#region ILanguageComponent 成员

		public string Language
		{
			get
			{
				if (this._language == string.Empty)
				{
					this.Language = "CHT";
				}

				return this._language;
			}
			set
			{
				bool bFlag = false;
				if (System.Web.HttpContext.Current != null)
				{
					if (System.Web.HttpContext.Current.Session["$Language"] != null)
					{
						this._language = System.Web.HttpContext.Current.Session["$Language"].ToString();
						bFlag = true;
					}
				}
				if (bFlag == false)
				{
					this._language = value;
				}
			}
		}

		public string LanguagePackageDir
		{
			get
			{
				if (this.DesignMode)
				{
					//System.Windows.Forms.MessageBox.Show(this._languagePackageDir.ToString());
					//System.Windows.Forms.MessageBox.Show(this._languagePackageDir.Trim());
					//System.Windows.Forms.MessageBox.Show(this._languagePackageDir);  
					if(this._languagePackageDir.Trim() == "")
					{
						this._languagePackageDir = @"\\grd2-build\language pack\";
					}
				}
				else
				{
					this._languagePackageDir = HttpContext.Current.Request.PhysicalApplicationPath;
				}
				return this._languagePackageDir;
			}
			set
			{
				this._languagePackageDir = value;
			}
		}

		private void DesignLoadLanguage()
		{
			if(!this.DesignMode)
			{
				return;
			}

			foreach (Control control1 in DesignContainerControls)
			{
				this.ReadControl(control1);
			}

			if (this.OnLoadLanguage != null)
			{
				OnLoadLanguage(this, new LanguageEventArgs(LanguagePack.Current(this.LanguageProfileName)));
			}
		}

		private void RuntimeLoadLanguage()
		{
			if (this.DesignMode)
			{
				return;
			}

			int flag = 0;
			if ((this.RuntimePage == null) && (this.RuntimeUserControl ==null))
			{
				return;
			}
			else
			{
				if (this.RuntimeUserControl !=null)
				{
					flag = 1;
				}
			}

			if (flag==0)
			{
				foreach (Control control1 in this.RuntimePage.Controls)
				{
					this.ReadControl(control1);
				}
			}
			else
			{
				foreach (Control control1 in this.RuntimeUserControl.Controls)
				{
					this.ReadControl(control1);
				}	
			}

			if (this.OnLoadLanguage != null)
			{
				OnLoadLanguage(this, new LanguageEventArgs(LanguagePack.Current(this.LanguageProfileName)));
			}
		}

		public void LoadLanguage()
		{
			if (this.DesignMode)
			{
				this.DesignLoadLanguage(); 
			}
			else
			{
				this.RuntimeLoadLanguage(); 
			}
		}

		public void SaveLanguage()
		{
			if (this.Site == null)
			{
				System.Windows.Forms.MessageBox.Show("Site is null");
				return;
			}
			foreach (Control control1 in DesignContainerControls)
			{
				this.WriteControl(control1, false);
			}
		}


		public LanguageWord GetLanguage(string keyWord)
		{
			return  LanguagePack.Current(this.LanguageProfileName).GetLanguageWord(keyWord, this.Language);
		}

		public string GetString( string keyWord )
		{
			LanguageWord lw = GetLanguage( keyWord );

			if ( lw == null )
			{
				return string.Empty;
                //return keyWord;
			}

			return lw.ControlText;
		}
		#endregion
	}

	public class LanguageEventArgs:  System.EventArgs 
	{
		public BenQGuru.eMES.Common.MutiLanguage.LanguagePack LanguagePack = null;
 
		public LanguageEventArgs(BenQGuru.eMES.Common.MutiLanguage.LanguagePack languagePack)
		{
				LanguagePack = languagePack;
		}
	}
}
