using System;
using System.Web.UI.WebControls;

using BenQGuru.eMES.Common.MutiLanguage;

namespace BenQGuru.eMES.Web.Helper
{
	public delegate object[] GetObjectListDelegate();

	/// <summary>
	/// DropDownListBuilder 的摘要说明。
	/// </summary>
	public class DropDownListBuilder
	{
		public GetObjectListDelegate HandleGetObjectList = null;
		private System.Web.UI.WebControls.DropDownList _drpList = null;

		public DropDownListBuilder(System.Web.UI.WebControls.DropDownList drpList)
		{
			this._drpList = drpList;
		}

		public void Build(string textPropertyName, string valuePropertyName)
		{
			if ( this.HandleGetObjectList != null )
			{
				this._drpList.Items.Clear();

				object[] objs = this.HandleGetObjectList();

				if ( objs != null )
				{
					foreach ( object obj in objs )
					{
						this._drpList.Items.Add( new ListItem(
								BenQGuru.eMES.Common.Domain.DomainObjectUtility.GetValue(obj, textPropertyName, null).ToString(),
								BenQGuru.eMES.Common.Domain.DomainObjectUtility.GetValue(obj, valuePropertyName, null).ToString() ));

					}
				}
			}
		}

		public void AddAllItem(ControlLibrary.Web.Language.LanguageComponent languageControl)
		{
			LanguageWord lword  = languageControl.GetLanguage("listItemAll");

			if ( lword != null )
			{
				this._drpList.Items.Insert(0, new ListItem(lword.ControlText, ""));
			}
			else
			{
				this._drpList.Items.Insert(0, new ListItem("", ""));
			}
		}
	}
}
