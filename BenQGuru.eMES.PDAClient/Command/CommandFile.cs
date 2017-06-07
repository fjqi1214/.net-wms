using System;

namespace BenQGuru.eMES.PDAClient.Command
{
	public class CommandFileNew: AbstractCommand
	{
		public CommandFileNew()
		{
		}

		public override void Execute()
		{
			//object a = BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting;
      
			//BenQGuru.eMES.PDAClient.FModule frmNewForm = new BenQGuru.eMES.PDAClient.FModule();
			//frmNewForm.MdiParent = BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().MainWindows;
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
			BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().CloseAllMdiChildren();
			System.Windows.Forms.Application.Exit();  
		}
	}
}
