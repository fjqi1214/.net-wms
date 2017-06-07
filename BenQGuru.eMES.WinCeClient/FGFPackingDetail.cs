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
    public partial class FGFPackingDetail : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;

        private string PickNo = string.Empty;
        private string CarInvNo = string.Empty;
        private Form parent;

        #region 初始化，回车事件

        public FGFPackingDetail(string pickNo, string carInvNo,Form parent)
        {
            if (parent == null)
            {
                throw new Exception("必须有一个父窗口！");
            }
            this.parent = parent;
            InitializeComponent();

            this.PickNo = pickNo;
            this.CarInvNo = carInvNo;

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            BindTransmissionParameter(); 
            
            DataTable dt = new DataTable();
            dt = this._PackagingOperationsService.GetDataGrid4(pickNo);
            BindDataGrid(dt);

            this.lblMessage.Text = string.Empty;
        }

        //绑定传输参数
        private void BindTransmissionParameter()
        {
            this.lblPickNO.Text = this.PickNo;
            this.lblCarInvNO.Text = this.CarInvNo;
        }

        #endregion

        #region button

        private void btnReturn_Click(object sender, EventArgs e)
        {
            parent.Close();
        }

        #endregion

        #region 自定义方法

        private void BindDataGrid(DataTable dt) //控件dataGrid1的列宽，注意这里传入的是DataTable
        {
            //样式定义
            DataGridTableStyle ts = new DataGridTableStyle();
            ts.MappingName = dt.TableName;//此处非常关键,数据表的名字不对,将无法映射成功
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataGridColumnStyle ColStyle = new DataGridTextBoxColumn();
                ColStyle.MappingName = dt.Columns[i].ColumnName.ToString();
                ColStyle.HeaderText = dt.Columns[i].ColumnName.ToString();
                ColStyle.Width = dt.Rows[0][i].ToString().Length * 7 - 1;

                if (dt.Columns[i].ColumnName == "选择") //如果某个条件满足就执行该列是否隐藏
                {
                    ColStyle.Width = 40;//当宽度等于0的时候就可以隐藏这列
                }
                else if (dt.Columns[i].ColumnName == "PickLine") //如果某个条件满足就执行该列是否隐藏
                {
                    ColStyle.Width = 0;//当宽度等于0的时候就可以隐藏这列
                }
                else
                {
                    ColStyle.Width = 100;
                }
                ts.GridColumnStyles.Add(ColStyle);
            }

            if (ts.GridColumnStyles.Count > 0)
            {
                //将样式和控件绑定到一起
                this.dataGrid1.TableStyles.Clear();
                this.dataGrid1.TableStyles.Add(ts);
            }
            this.dataGrid1.DataSource = dt;

            ts = null;
            GC.Collect();//回收一下有好处的
        }

        #endregion
    }
}
