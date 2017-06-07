using System;

namespace BenQGuru.eMES.Client.Command
{
	/// <summary>
	/// CommandWindows 的摘要说明。
	/// </summary>
	public class CommandWindowsCascade: AbstractCommand
	{
		public CommandWindowsCascade()
		{
		}

		public override void Execute()
		{
			BenQGuru.eMES.Client.Service.ApplicationService.Current().MainWindows.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);        
		}
	}

	public class CommandWindowsHorizontal: AbstractCommand
	{
		public CommandWindowsHorizontal()
		{
		}

		public override void Execute()
		{
			BenQGuru.eMES.Client.Service.ApplicationService.Current().MainWindows.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);        
		}
	}


	public class CommandWindowsVertical: AbstractCommand
	{
		public CommandWindowsVertical()
		{
		}

		public override void Execute()
		{
			BenQGuru.eMES.Client.Service.ApplicationService.Current().MainWindows.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);        
		}
	}
}
