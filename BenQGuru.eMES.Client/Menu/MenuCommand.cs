using System;
using BenQGuru.eMES.Client.Command;

namespace BenQGuru.eMES.Client.Menu
{
	/// <summary>
	/// MenuCommand 的摘要说明。
	/// </summary>
	public class MenuCommand:AbstractMenu
	{

		public MenuCommand(object Key, string caption,  int index, ICommand command)
			:this(Key, caption, caption, index, -1, new System.Windows.Forms.Shortcut(), null, command, null)
		{
		}

		public MenuCommand(string caption,  int index, ICommand command)
			:this(caption, caption, caption, index, -1, new System.Windows.Forms.Shortcut(), null, command, null)
		{
		}

		public MenuCommand(object Key, string caption,  int index, System.Windows.Forms.Shortcut   shortcut, ICommand command)
									:this(Key, caption, caption, index, -1, shortcut, null, command, null)
		{
		}

		public MenuCommand(object Key, string caption, string hint, int index, int imageIndex, 
								System.Windows.Forms.Shortcut   shortcut, IMenu[] subMenus, ICommand command, object menuObject)
		{
			this.Key = Key;
			this.Caption = caption;
			this.Hint = hint;
			this.Index = index;
			this.ImageIndex = imageIndex;
			this.Shortcut = shortcut;
			this.SubMenus = subMenus;
			this.Command = command;
			this.MenuObject = menuObject;
		}
	}
}
