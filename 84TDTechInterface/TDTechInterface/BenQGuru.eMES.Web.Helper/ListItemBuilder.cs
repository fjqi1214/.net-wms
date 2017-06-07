using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BenQGuru.eMES.Web.Helper
{
	public class CheckBoxListBuilder
	{
		private IInternalSystemVariable _variable = null;
		private CheckBoxList _cbl = null;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		public CheckBoxListBuilder(
			IInternalSystemVariable variable,CheckBoxList cbl,ControlLibrary.Web.Language.LanguageComponent languageComponent)
		{
			this._variable = variable;
			this._cbl = cbl;
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
				this._cbl.Items.Add( item );
			}
		}

		public static ArrayList GetCheckedList(CheckBoxList cbl)
		{
			ArrayList array = new ArrayList();
			if( cbl != null )
			{
				foreach(ListItem listItem in cbl.Items)
				{
					if( listItem.Selected )
					{
						if( !array.Contains( listItem.Value ) )
						{
							array.Add( listItem.Value );
						}
					}
				}
			}
			return array;
		}

		public static void FormatListControlStyle(CheckBoxList cbl,int width)
		{
			if( cbl != null )
			{
				foreach(CheckBox cb in cbl.Controls)
				{
					cb.Width = width;
				}
			}
		}
	}

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

	public class DropDownListBuilder2
	{
		private IInternalSystemVariable _variable = null;
		private DropDownList _ddl = null;
		private ControlLibrary.Web.Language.LanguageComponent languageComponent1;

		public DropDownListBuilder2(
			IInternalSystemVariable variable,DropDownList ddl,ControlLibrary.Web.Language.LanguageComponent languageComponent)
		{
			this._variable = variable;
			this._ddl = ddl;
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
				this._ddl.Items.Add( item );
			}

			this._ddl.SelectedIndex = 0;
		}

		public void Build(string val)
		{
			this.Build( );

			try
			{
				this._ddl.SelectedValue = val;
			}
			catch
			{
				this._ddl.SelectedIndex = 0;
			}
		}

		public static void FormatListControlStyle(DropDownList ddl, int width)
		{
			string jscript = "<script language=jscript>try{";
			jscript += string.Format("var objs = document.getElementById('{0}').all.tags('LABEL') ;" , ddl.ClientID ) ;
			jscript += "for(var i=0;i<objs.length;i++){";
			jscript += string.Format("objs[i].style.width = '{0}px' ; objs[i].style.textAlign='left';"  , width)  ;
			jscript += "}" ;
			jscript += "}catch(e){}</script>" ;
			ddl.Page.RegisterStartupScript("JS_" + ddl.ClientID , jscript ) ;
		}
	}

}
