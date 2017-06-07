using System;
using System.Windows.Forms;  
using BenQGuru.eMES.Client.Command;

namespace BenQGuru.eMES.Client.Menu
{
	/// <summary>
	/// IMenu 的摘要说明。
	/// </summary>
	public interface IMenu
	{
		object Key
		{
			get;
			set;
		}

		string Caption
		{
			get;
			set;
		}

		string Hint
		{
			get;
			set;
		}

		int Index
		{
			get;
			set;
		}

		int ImageIndex
		{
			get;
			set;
		}

		Shortcut  Shortcut
		{
			get;
			set;
		}

		IMenu[] SubMenus
		{
			get;
			set;		
		}

		ICommand  Command
		{
			get;
			set;
		}

		object MenuObject
		{
			get;
			set;
		}
	}
}
