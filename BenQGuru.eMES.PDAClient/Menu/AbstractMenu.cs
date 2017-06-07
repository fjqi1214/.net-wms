using System;
using BenQGuru.eMES.PDAClient.Command;

namespace BenQGuru.eMES.PDAClient.Menu
{
	/// <summary>
	/// AbstractMenu 的摘要说明。
	/// </summary>
	public abstract class AbstractMenu:IMenu
	{
		private object _key =  ""; 
		private string _caption =  ""; 
		private string _hint =  "";
		private int _index = 0;
		private int _imageIndex =  -1; 
		private System.Windows.Forms.Shortcut _shortcut =  new System.Windows.Forms.Shortcut(); 
		private IMenu[] _subMenus = null;
		private ICommand _command = null;
		private object _menuObject = null;

		#region IMenu 成员

		public object Key
		{
			get
			{
				return _key;
			}
			set
			{
				_key = value;
			}
		}

		public string Caption
		{
			get
			{
				return _caption;
			}
			set
			{
				_caption = value;
			}
		}

		public string Hint
		{
			get
			{
				return _hint;
			}
			set
			{
				_hint = value;
			}
		}

		public int Index
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
			}
		}

		public int ImageIndex
		{
			get
			{
				return _imageIndex;
			}
			set
			{
				_imageIndex = value;
			}
		}

		public System.Windows.Forms.Shortcut Shortcut
		{
			get
			{
				return _shortcut;
			}
			set
			{
				_shortcut = value;
			}
		}

		public IMenu[] SubMenus
		{
			get
			{
				return _subMenus;
			}

			set
			{
				_subMenus = value;
			}
		}

		public ICommand  Command
		{
			get
			{
				return _command;
			}
		
			set
			{
				_command = value;
			}
		}

		public object MenuObject
		{
			get
			{
				return _menuObject;
			}
			set
			{
				_menuObject = value;
			}
		}

		#endregion
	}
}
