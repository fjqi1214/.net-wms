using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
namespace BenQGuru.eMES.WinCeClient
{
    public partial class FMaterOnShelves : UserControl
    {
        MaterialOnShelves.MaterialOnShelves MaterialOnShelvesService;
        private int rows = 0;
        public FMaterOnShelves()
        {
            InitializeComponent();
            MaterialOnShelvesService = new BenQGuru.eMES.WinCeClient.MaterialOnShelves.MaterialOnShelves();
            MaterialOnShelvesService.Url = WebServiceFacade.GetWebServiceURL() + "MaterialOnShelves.asmx";
            //dataGrid1.DataSource = MaterialOnShelvesService.GetDataTable();

        }

        public void QueryGrid()
        {
            DataTable dt1 = new DataTable();
            dt1 = MaterialOnShelvesService.GetDataGrid(this.txtCartonNo.Text, this.txtLocationNo.Text);
            rows = dt1.Rows.Count;
            //this.dataGrid1.DataSource = dt1;
            if (dt1.Rows.Count > 0)
            {


                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = dt1.TableName;

                DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
                ColStyle1.MappingName = dt1.Columns[0].ColumnName.ToString();
                ColStyle1.HeaderText = "选择";
                ColStyle1.Width = 20;
                ts.GridColumnStyles.Add(ColStyle1);

                DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
                ColStyle3.MappingName = dt1.Columns[1].ColumnName.ToString();
                ColStyle3.HeaderText = "入库指令号";
                ts.GridColumnStyles.Add(ColStyle3);

                DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
                ColStyle4.MappingName = dt1.Columns[2].ColumnName.ToString();
                ColStyle4.HeaderText = "箱号";
                ts.GridColumnStyles.Add(ColStyle4);

                DataGridColumnStyle ColStyle5 = new DataGridTextBoxColumn();
                ColStyle5.MappingName = dt1.Columns[3].ColumnName.ToString();
                ColStyle5.HeaderText = "推荐货位";
                ts.GridColumnStyles.Add(ColStyle5);


                DataGridColumnStyle ColStyle6 = new DataGridTextBoxColumn();
                ColStyle6.MappingName = dt1.Columns[4].ColumnName.ToString();
                ColStyle6.HeaderText = "货位";
                ts.GridColumnStyles.Add(ColStyle6);

                DataGridColumnStyle ColStyle7 = new DataGridTextBoxColumn();
                ColStyle7.MappingName = dt1.Columns[5].ColumnName.ToString();
                ColStyle7.HeaderText = "鼎桥物料号";
                ts.GridColumnStyles.Add(ColStyle7);

                DataGridColumnStyle ColStyle8 = new DataGridTextBoxColumn();
                ColStyle8.MappingName = dt1.Columns[6].ColumnName.ToString();
                ColStyle8.HeaderText = " ";
                ts.GridColumnStyles.Add(ColStyle8);

                this.dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);


            }
            this.dataGrid1.DataSource = dt1;
            this.lblPlanQTY.Text = MaterialOnShelvesService.GetPlanOnShelvesQTY(this.txtCartonNo.Text);
            this.lblActQTY.Text = MaterialOnShelvesService.GetActOnShelvesQTY(this.txtCartonNo.Text);
            this.lblDQMcodeEdite.Text = MaterialOnShelvesService.GetCUSMcode(this.txtCartonNo.Text);
        }

        private void txtCartonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e != null && e.KeyChar == '\r' && rows == 0)
            {
                QueryGrid();
            }
            else if (e != null && e.KeyChar == '\r' && rows > 0)
            {
                if (string.IsNullOrEmpty(txtCartonNo.Text))
                {
                    MessageBox.Show("请先扫入箱号！");
                    return;
                }
                dataGrid1.CurrentCellChanged -= dataGrid1_CurrentCellChanged;
                for (int i = 0; i < rows; i++)
                {
                    dataGrid1[i, 0] = "";
                }


                string cartonnoStr = txtCartonNo.Text.Replace("*", ",").ToUpper();
                DataTable dt = MaterialOnShelvesService.Retrieve(cartonnoStr);

                foreach (DataRow dr in dt.Rows)
                {
                    string stno = dr["STNO"].ToString();
                    string stline = dr["STLINE"].ToString();

                    for (int i = 0; i < rows; i++)
                    {
                        if (dataGrid1[i, 1].ToString() == stno && dataGrid1[i, 6].ToString() == stline)
                        {
                            dataGrid1[i, 0] = "√";
                        }
                    }

                }
                dataGrid1.CurrentCellChanged += dataGrid1_CurrentCellChanged;
                txtCartonNo.Text = string.Empty;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("ExampleDataTable");
            dt.Columns.Add("STNO", typeof(string));
            dt.Columns.Add("STLINE", typeof(string));

            btnSubmit.Enabled = false;

            for (int i = 0; i < rows; i++)
            {
                if (this.dataGrid1[i, 0].ToString() == "√")
                {
                    dt.Rows.Add(this.dataGrid1[i, 1].ToString(), this.dataGrid1[i, 6]);
                }
            }
            
            string result = MaterialOnShelvesService.OnShelves(dt, this.txtCartonNo.Text.ToUpper(), this.txtLocationNo.Text, ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            
            btnSubmit.Enabled = true;
            MessageBox.Show(result);
            txtCartonNo.Text = string.Empty;
            QueryGrid();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            dataGrid1.Select(dataGrid1.CurrentRowIndex);
            if (dataGrid1[dataGrid1.CurrentRowIndex, 0].ToString() == "√")
            {
                dataGrid1[dataGrid1.CurrentRowIndex, 0] = "";
            }
            else
            {
                dataGrid1[dataGrid1.CurrentRowIndex, 0] = "√";
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCartonNo.Text))
            {
                MessageBox.Show("请先扫入箱号！");
                return;
            }
            dataGrid1.CurrentCellChanged -= dataGrid1_CurrentCellChanged;
            for (int i = 0; i < rows; i++)
            {
                dataGrid1[i, 0] = "";
            }


            string cartonnoStr = txtCartonNo.Text.Replace("*", ",").ToUpper();
            DataTable dt = MaterialOnShelvesService.Retrieve(cartonnoStr);

            foreach (DataRow dr in dt.Rows)
            {
                string stno = dr["STNO"].ToString();
                string stline = dr["STLINE"].ToString();

                for (int i = 0; i < rows; i++)
                {
                    if (dataGrid1[i, 1].ToString() == stno && dataGrid1[i, 6].ToString() == stline)
                    {
                        dataGrid1[i, 0] = "√";
                    }
                }

            }
            dataGrid1.CurrentCellChanged += dataGrid1_CurrentCellChanged;
            txtCartonNo.Text = string.Empty;
        }

        private void panel1_GotFocus(object sender, EventArgs e)
        {

        }


       
    }
}
