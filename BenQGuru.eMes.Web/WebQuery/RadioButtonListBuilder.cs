using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// RadioButtonListBuilder 的摘要说明。
	/// </summary>
	public class RadioButtonListBuilder
	{
		private IInternalSystemVariable _variable = null;
		private RadioButtonList _rbl = null;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		public RadioButtonListBuilder(
			IInternalSystemVariable variable,RadioButtonList rbl,ControlLibrary.Web.Language.LanguageComponent languageComponent)
		{
			this._variable = variable;
			this._rbl = rbl;
			this.languageComponent1 = languageComponent;
		}

		public void Build()
		{	
			ListItem item = null;
			foreach(string var in this._variable.Items)
			{
				item = new ListItem();
				item.Text = this.languageComponent1.GetString( var );
				if( item.Text == "" )
				{
					item.Text = var;
				}
				item.Value = var;
				this._rbl.Items.Add( item );
			}

			this._rbl.SelectedIndex = 0;
		}

		public void Build(string val)
		{
			this.Build( );

			try
			{
				this._rbl.SelectedValue = val;
			}
			catch
			{
				this._rbl.SelectedIndex = 0;
			}
		}

		public static void FormatListControlStyle(RadioButtonList rbl,int width)
		{
            string jscript = "<script language=jscript>try{";
            jscript += string.Format("var objs = document.getElementById('{0}').all.tags('LABEL') ;" , rbl.ClientID ) ;
            jscript += "for(var i=0;i<objs.length;i++){";
            jscript += string.Format("objs[i].style.width = '{0}px' ; objs[i].style.textAlign='left';"  , width)  ;
            jscript += "}" ;
            jscript += "}catch(e){}</script>" ;
		    rbl.Page.RegisterStartupScript("JS_" + rbl.ClientID , jscript ) ;
		}
	}
}
