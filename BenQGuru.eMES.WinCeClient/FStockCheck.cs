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
    public partial class FStockCheck : UserControl
    {
        DataTable dt;
        private BenQGuru.eMES.WinCeClient.StockCheck.StockCheck s;
        public FStockCheck()
        {
            InitializeComponent();
            s = new BenQGuru.eMES.WinCeClient.StockCheck.StockCheck();
            s.Url = WebServiceFacade.GetWebServiceURL() + "StockCheck.asmx";
            string[] ss = s.GetWaitStockCheckNo(ApplicationService.Current().LoginInfo.UserCode.ToUpper());
            this.cboCheckNo.Items.Add("");
            foreach (string str in ss)
                this.cboCheckNo.Items.Add(str);

        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            bool result = s.SubmitPortionCheck(cboCheckNo.SelectedItem.ToString(),
                 txtCARTONNO.Text,
                 txtQTY.Text,
                 txtLocationCode.Text,
                 txtDiffDesc.Text,
                 txtDQMCode.Text,
                 ApplicationService.Current().LoginInfo.UserCode.ToUpper(),
                 out message);
            if (result)
            {
                txtCARTONNO.Text = string.Empty;
                txtQTY.Text = string.Empty;
                txtLocationCode.Text = string.Empty;
                txtDiffDesc.Text = string.Empty;
                txtDQMCode.Text = string.Empty;
                DataTable dt = s.GetPortionStockCheckOps(cboCheckNo.SelectedItem.ToString());
                BindGrid(dt);
            }
            MessageBox.Show(message);
            

        }

        private void cboCheckNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = s.GetPortionStockCheckOps(cboCheckNo.SelectedItem.ToString());
            BindGrid(dt);
        }


        private void BindGrid(DataTable dt)
        {
            this.dataGrid1.TableStyles.Clear();
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dt.TableName;

         

            DataGridColumnStyle ColStyle0 = new DataGridTextBoxColumn();
            ColStyle0.MappingName = dt.Columns[0].ColumnName.ToString();
            ColStyle0.HeaderText = "货位";
            ts.GridColumnStyles.Add(ColStyle0);

            DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
            ColStyle1.MappingName = dt.Columns[1].ColumnName.ToString();
            ColStyle1.HeaderText = "箱号";
            ts.GridColumnStyles.Add(ColStyle1);

            DataGridColumnStyle ColStyle2 = new DataGridTextBoxColumn();
            ColStyle2.MappingName = dt.Columns[2].ColumnName.ToString();
            ColStyle2.HeaderText = "实际货位";
            ts.GridColumnStyles.Add(ColStyle2);

            DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
            ColStyle3.MappingName = dt.Columns[3].ColumnName.ToString();
            ColStyle3.HeaderText = "实际箱号";
            ts.GridColumnStyles.Add(ColStyle3);

            DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
            ColStyle4.MappingName = dt.Columns[4].ColumnName.ToString();
            ColStyle4.HeaderText = "库存数量";
            ts.GridColumnStyles.Add(ColStyle4);

            DataGridColumnStyle ColStyle5 = new DataGridTextBoxColumn();
            ColStyle5.MappingName = dt.Columns[5].ColumnName.ToString();
            ColStyle5.HeaderText = "盘点数量";
            ts.GridColumnStyles.Add(ColStyle5);

            DataGridColumnStyle ColStyle6 = new DataGridTextBoxColumn();
            ColStyle6.MappingName = dt.Columns[6].ColumnName.ToString();
            ColStyle6.HeaderText = "库位";
            ts.GridColumnStyles.Add(ColStyle6);

            DataGridColumnStyle ColStyle7 = new DataGridTextBoxColumn();
            ColStyle7.MappingName = dt.Columns[7].ColumnName.ToString();
            ColStyle7.HeaderText = "鼎桥物料号";
            ts.GridColumnStyles.Add(ColStyle7);

            this.dataGrid1.TableStyles.Add(ts);

            this.dataGrid1.DataSource = dt;

        }

    }
}
