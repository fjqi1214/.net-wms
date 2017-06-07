using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BenQGuru.eMES.WinCeClient
{
    public partial class FSAPInvoices : UserControl
    {
        FPickDone pickDone;
        private PickDone.PickDone PickDoneService;
        public FSAPInvoices(FPickDone child, string pickNo, string dqmcode)
        {
            InitializeComponent();
            this.pickDone = child;
            this.txtPickNo.Text = pickNo;


            PickDoneService = new BenQGuru.eMES.WinCeClient.PickDone.PickDone();
            PickDoneService.Url = WebServiceFacade.GetWebServiceURL() + "PickDone.asmx";
            DataTable dt1 = new DataTable();
            dt1 = PickDoneService.GetInvoicesDetails(pickNo, dqmcode);


            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dt1.TableName;

            DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
            ColStyle1.MappingName = dt1.Columns[0].ColumnName.ToString();
            ColStyle1.HeaderText = "鼎桥物料";
            //ColStyle1.Width = 20;
            ts.GridColumnStyles.Add(ColStyle1);

            DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
            ColStyle3.MappingName = dt1.Columns[1].ColumnName.ToString();
            ColStyle3.HeaderText = "计划数量";
            ts.GridColumnStyles.Add(ColStyle3);

            DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
            ColStyle4.MappingName = dt1.Columns[2].ColumnName.ToString();
            ColStyle4.HeaderText = "物料描述";
            ts.GridColumnStyles.Add(ColStyle4);




            this.dataGrid1.TableStyles.Clear();
            this.dataGrid1.TableStyles.Add(ts);

            this.dataGrid1.DataSource = dt1;


        }

        private void btnReturn_Click(object sender, EventArgs e)
        {

            pickDone.Visible = true;
            this.Parent.Controls.Remove(this);
            this.Dispose();


        }


    }
}
