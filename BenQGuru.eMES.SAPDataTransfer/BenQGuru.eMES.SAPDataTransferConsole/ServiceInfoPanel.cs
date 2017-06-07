using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.SAPDataTransferConsole
{
    public partial class ServiceInfoPanel : UserControl
    {
        public ServiceInfoPanel()
        {
            InitializeComponent();
        }

        public void SetServiceInfo(ServiceEntity se)
        {
            this.textBoxKey.Text = se.Key;
            this.textBoxType.Text = se.Type;
            this.textBoxDesc.Text = se.Description;
            this.textBoxPath.Text = se.AssemblyPath;
        }

        public void ClearServiceInfo()
        {
            this.textBoxKey.Text = "";
            this.textBoxDesc.Text = "";
            this.textBoxPath.Text = "";
            this.textBoxType.Text = "";
        }
    }
}
