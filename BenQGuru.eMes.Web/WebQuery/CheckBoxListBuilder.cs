using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// CheckBoxListBuilder 的摘要说明。
	/// </summary>
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
}
