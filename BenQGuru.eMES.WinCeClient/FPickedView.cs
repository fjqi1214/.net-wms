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
    public partial class FPickedView : UserControl
    {
        private PickDone.PickDone PickDoneService;
        public FPickDone fm;
        //private Form parent;
        private string DQMCode = string.Empty;
        private string PickNo = string.Empty;
        private FPickedView _pickedView;

        public FPickedView()
        {
            //if (parent == null)
            //    throw new Exception("必须有一个父窗口！");
            //this.parent = parent;
            InitializeComponent();
            PickDoneService = new BenQGuru.eMES.WinCeClient.PickDone.PickDone();
            PickDoneService.Url = WebServiceFacade.GetWebServiceURL() + "PickDone.asmx";
            DQMCode = ApplicationService.Current().MaterInfo.DQMCode.ToUpper();
            PickNo = ApplicationService.Current().MaterInfo.PickNo.ToUpper();
            this.lblDQMCode.Text = DQMCode;

            DataTable dt1 = new DataTable();
            dt1 = PickDoneService.PickedView(PickNo, DQMCode);
            //rows = dt.Rows.Count;
            //this.dataGrid1.DataSource = dt1;
            if (dt1.Rows.Count > 0)
            {


                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = dt1.TableName;

                DataGridColumnStyle ColStyle1 = new DataGridTextBoxColumn();
                ColStyle1.MappingName = dt1.Columns[0].ColumnName.ToString();
                ColStyle1.HeaderText = "箱号";
                //ColStyle1.Width = 20;
                ts.GridColumnStyles.Add(ColStyle1);

      

                DataGridColumnStyle ColStyle5 = new DataGridTextBoxColumn();
                ColStyle5.MappingName = dt1.Columns[1].ColumnName.ToString();
                ColStyle5.HeaderText = "SN";
                ts.GridColumnStyles.Add(ColStyle5);


                DataGridColumnStyle ColStyle6 = new DataGridTextBoxColumn();
                ColStyle6.MappingName = dt1.Columns[2].ColumnName.ToString();
                ColStyle6.HeaderText = "拣料人";
                ts.GridColumnStyles.Add(ColStyle6);

                DataGridColumnStyle ColStyle7 = new DataGridTextBoxColumn();
                ColStyle7.MappingName = dt1.Columns[3].ColumnName.ToString();
                ColStyle7.HeaderText = "日期";
                ts.GridColumnStyles.Add(ColStyle7);


                DataGridColumnStyle ColStyle3 = new DataGridTextBoxColumn();
                ColStyle3.MappingName = dt1.Columns[4].ColumnName.ToString();
                ColStyle3.HeaderText = "批次号";
                ts.GridColumnStyles.Add(ColStyle3);

                DataGridColumnStyle ColStyle4 = new DataGridTextBoxColumn();
                ColStyle4.MappingName = dt1.Columns[5].ColumnName.ToString();
                ColStyle4.HeaderText = "货位";
                ts.GridColumnStyles.Add(ColStyle4);

                this.dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);

                this.dataGrid1.DataSource = dt1;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            //
            this.Parent.Controls.Remove(this);
            fm.Visible = true;
        }
    }
}
