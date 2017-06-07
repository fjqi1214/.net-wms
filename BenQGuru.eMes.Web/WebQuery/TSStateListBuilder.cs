using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace BenQGuru.eMES.Web.WebQuery
{
	/// <summary>
	/// TSStateCheckBoxListBuilder 的摘要说明。
	/// </summary>
	public class TSStateListBuilder
	{
		private TSStateListBuilder()
		{
		}

		public static void CreateTSStateListCheckBoxList(CheckBoxList chkList, ControlLibrary.Web.Language.LanguageComponent language)
		{
			chkList.RepeatColumns = 5;

			chkList.Items.Add(language.GetString("OP_componentloading"));
			chkList.Items.Add(language.GetString("OP_testing"));
			chkList.Items.Add(language.GetString("OP_idtranslate"));
			chkList.Items.Add(language.GetString("OP_packing"));
			chkList.Items.Add(language.GetString("OP_oqc"));
			chkList.Items.Add(language.GetString("OP_ts"));
			chkList.Items.Add(language.GetString("OP_outside_route"));
			chkList.Items.Add(language.GetString("OP_smt"));
			chkList.Items.Add(language.GetString("OP_spc"));
			chkList.Items.Add(language.GetString("OP_deduct"));
		}

		public static void SetCheckBoxChecked(CheckBoxList chkList,string key)
		{
		}

		public static ArrayList GetCheckedTSStateList(CheckBoxList chkList)
		{
			return null;
		}
	}
}
