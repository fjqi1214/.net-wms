using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;

namespace BenQGuru.eMES.SAPDataTransferService
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        public ServiceInstaller()
        {
            InitializeComponent();
        }

        public override void Install(System.Collections.IDictionary stateSaver)
        {
            Microsoft.Win32.RegistryKey system;
            Microsoft.Win32.RegistryKey currentControlSet;
            Microsoft.Win32.RegistryKey services;
            Microsoft.Win32.RegistryKey service;

            const string SERVICE_DESCRIPTION = "Launch a Service which is based on BenQGuru.eMES.SAPDataTransferInterface.ICommand";

            try
            {
                //Let the project installer do its job
                base.Install(stateSaver);

                //Open the HKEY_LOCAL_MACHINE\SYSTEM key
                system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
                //Open CurrentControlSet
                currentControlSet = system.OpenSubKey("CurrentControlSet");
                //Go to the services key
                services = currentControlSet.OpenSubKey("Services");
                //Open the key for your service, and allow writing
                service = services.OpenSubKey(this.serviceInstallerMain.ServiceName, true);
                //Add your service's description as a REG_SZ value named "Description"
                service.SetValue("Description", SERVICE_DESCRIPTION);
            }
            catch (Exception e)
            {
                EventLog.WriteEntry("Service Shell", string.Format("Error Message:{0}\nError StackTrace:{1}"
                        + "\nError Source:{2}", e.Message, e.StackTrace, e.Source), EventLogEntryType.Error);
            }
        }
    }
}