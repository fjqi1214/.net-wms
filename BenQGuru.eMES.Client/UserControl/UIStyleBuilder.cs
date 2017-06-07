using System;
using System.Drawing;
using System.Windows.Forms;

namespace UserControl
{
	public enum WidthTypes 
	{	
		Tiny,
		Small, 
		Normal, 
		Long,
		TooLong
	};
	//Laws Lu，2005/08/11，添加整数
	public enum EditTypes 
	{	
		String, 
		Number,
		Integer
	};

	public enum ButtonTypes 
	{
		None,
		Exit,
		Edit,
		Confirm,
		Cancle,
		Delete,
		Add,
		Query,
		Refresh,
		Save,
		Copy,
		AllLeft,
		AllRight,
		Change,
		Move
	};

	public enum DateTimeTypes 
	{
		Date, 
		Time, 
		DateTime
	};

	/// <summary>
	/// UICommon 的摘要说明。
	/// </summary>
	public class UIStyleBuilder
	{
		public UIStyleBuilder()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			
		}
		/// <summary>
		/// 描述和编辑框之间的间隔
		/// </summary>
		public static int SepOfLabAndEditBox=8;

		public static void GridUI(Infragistics.Win.UltraWinGrid.UltraGrid grid)
		{
			grid.DisplayLayout.Appearance.BackColor = System.Drawing.Color.White;;
			grid.DisplayLayout.CaptionAppearance.BackColor =Color.FromName("WhiteSmoke");
			grid.DisplayLayout.Appearance.BackColor=Color.FromArgb(255, 255, 255);
			grid.DisplayLayout.Override.HeaderAppearance.BackColor = Color.FromName("WhiteSmoke");
			grid.DisplayLayout.Override.RowAppearance.BackColor =Color.FromArgb(230, 234, 245);
			grid.DisplayLayout.Override.RowAlternateAppearance.BackColor=Color.FromArgb(255, 255, 255);
			grid.DisplayLayout.Override.RowSelectors =Infragistics.Win.DefaultableBoolean.False;
			grid.DisplayLayout.Override.ActiveRowAppearance.BackColor = System.Drawing.Color.Gainsboro;
			grid.DisplayLayout.Override.ActiveRowAppearance.ForeColor = System.Drawing.Color.Black;
			grid.DisplayLayout.ScrollBarLook.Appearance.BackColor =Color.FromName("LightGray");
			
			grid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			grid.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
			grid.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
			grid.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;

		}
		
		public static void FormUI(Form form)
		{
			form.BackColor = Color.FromArgb(220,220,220);			
		}

		public static void UltraOptionSetUI(Infragistics.Win.UltraWinEditors.UltraOptionSet optionSet)
		{
			optionSet.BackColor = Color.FromArgb(220,220,220);
			optionSet.BorderStyle=Infragistics.Win.UIElementBorderStyle.None;
			optionSet.FlatMode=true;
			optionSet.Appearance.FontData.Bold =Infragistics.Win.DefaultableBoolean.False;
		}
	}


	public class ItemObject
	{
		public ItemObject()
		{
		}
		public string ItemText;
		public object ItemValue;
		public override string ToString()
		{
			return ItemText;
		}

	}
}
