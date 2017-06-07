using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FMaterialOnShelves : Form
    {
        MaterialOnShelves.MaterialOnShelves MaterialOnShelvesService;
        public FMaterialOnShelves()
        {
            InitializeComponent();
            MaterialOnShelvesService = new BenQGuru.eMES.WinCeClient.MaterialOnShelves.MaterialOnShelves();
            MaterialOnShelvesService.Url = WebServiceFacade.GetWebServiceURL() + "MaterialOnShelves.asmx";
        }

        private void txtCartonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.dataGrid1.DataSource = MaterialOnShelvesService.GetDataGrid(this.txtCartonNo.Text, this.txtLocationCode.Text);
            this.lblPlanQTY.Text= MaterialOnShelvesService.GetPlanOnShelvesQTY(this.txtCartonNo.Text);
            this.lblActQTY.Text = MaterialOnShelvesService.GetActOnShelvesQTY(this.txtCartonNo.Text);
            this.lblDQMcodeEdite.Text = MaterialOnShelvesService.GetCUSMcode(this.txtCartonNo.Text);
        }

        private void btnOnShelves_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
        }
    }
}