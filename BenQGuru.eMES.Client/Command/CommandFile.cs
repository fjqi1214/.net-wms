using System;

namespace BenQGuru.eMES.Client.Command
{
	public class CommandFileNew: AbstractCommand
	{
		public CommandFileNew()
		{
		}

		public override void Execute()
		{
			//object a = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting;
      
			//BenQGuru.eMES.Client.FModule frmNewForm = new BenQGuru.eMES.Client.FModule();
			//frmNewForm.MdiParent = BenQGuru.eMES.Client.Service.ApplicationService.Current().MainWindows;
			//frmNewForm.Show();    
		}
	}

	public class CommandFileClose: AbstractCommand
	{
		public CommandFileClose()
		{
		}

		public override void Execute()
		{
			BenQGuru.eMES.Client.Service.ApplicationService.Current().CloseAllMdiChildren();
			System.Windows.Forms.Application.Exit();  
		}
	}
}
