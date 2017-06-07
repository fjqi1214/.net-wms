using System;
using System.Reflection;

namespace BenQGuru.eMES.PDAClient.Command
{
	public class CommandOpenForm : AbstractCommand
	{
		private string _typeName = string.Empty;
        private string _dllFileName = string.Empty;

		public CommandOpenForm( string typeName )
		{
            if (typeName.IndexOf(",") < 0)      // 没有逗号时，认为是完整的Class
                this._typeName = typeName;
            else                                // 有逗号时，认为逗号前是DLL文件名，逗号后是Class
            {
                string[] strTmp = typeName.Split(',');
                _typeName = strTmp[0].Trim();
                _dllFileName = strTmp[1].Trim();
            }
		}

		public override void Execute()
		{
			try
			{
				bool find = false;

				foreach(System.Windows.Forms.Form child in BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().MainWindows.MdiChildren)
				{
					if(child.GetType().FullName ==  this._typeName)
					{
						child.BringToFront(); 
						child.Show(); 
						find = true;
						break;
					}
				}

				if(!find)
				{
                    // Modify by Icyer 2007/04/18   允许从DLL加载界面
					//object form = Assembly.GetAssembly( Type.GetType(this._typeName) ).CreateInstance(this._typeName);
                    object form = null;
                    if (_dllFileName == string.Empty)
                        form = Assembly.GetAssembly(Type.GetType(this._typeName)).CreateInstance(this._typeName);
                    else
                    {
                        string strPath = System.Windows.Forms.Application.ExecutablePath;
                        strPath = strPath.Substring(0, strPath.LastIndexOf("\\") + 1) + _dllFileName;
                        form = Assembly.LoadFile(strPath).CreateInstance(this._typeName);
                        if (form is BenQGuru.eMES.ClientBase.FormBase)
                        {
                            ((BenQGuru.eMES.ClientBase.FormBase)form).DataProvider = Service.ApplicationService.Current().DataProvider;
                        }
                    }
                    // Modify end

					if ( form is System.Windows.Forms.Form )
					{
						//((System.Windows.Forms.Form)form).MdiParent = BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().MainWindows;
					
						if ( this._typeName != "BenQGuru.eMES.PDAClient.FLogin" )
						{
							((System.Windows.Forms.Form)form).WindowState = System.Windows.Forms.FormWindowState.Normal;
						}
					
						((System.Windows.Forms.Form)form).ShowDialog();
					}
				}
			}
			catch(Exception ex)
			{
				//ApplicationRun.GetInfoForm().Add(new UserControl.Message(ex));
			}
		}
	}


	public class CommandLogin : CommandOpenForm
	{
		public CommandLogin( string typeName ) : base(typeName)
		{
		}

		public override void Execute()
		{
			BenQGuru.eMES.PDAClient.Service.ApplicationService.Current().CloseAllMdiChildren();
			//ApplicationRun.GetInfoForm().Clear();

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();
			

			base.Execute ();
		}
	}
}
