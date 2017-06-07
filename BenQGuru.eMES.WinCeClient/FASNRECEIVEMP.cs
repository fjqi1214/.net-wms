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
    public partial class FASNRECEIVEMP : Form
    {


        public FASNRECEIVEMP()
        {
            InitializeComponent();
        }

        private void FASNRECEIVEMP_Load(object sender, EventArgs e)
        {
            this.cmbASNNO.Items.Add("BELA_TEST1");
            this.cmbASNNO.SelectedIndex = 0;

            //dataGrid1.DataSource = userList.Select(x => new { 姓名 = x.Name, 性别 = x.Sex, 年龄 = x.Age }).ToList();
            DataGridColumnStyle col1 = new DataGridTextBoxColumn();
            col1 = new DataGridTextBoxColumn();
             col1.MappingName = "第一列列名";
            col1.HeaderText = "列标题";
            col1.Width = 90; //宽度值;

        }

        private void cmbASNNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            //object[] obj = _InventoryFacade.QueryASNDetail(this.cmbASNNO.SelectedItem.ToString(), int.MinValue, int.MaxValue);
            //List<ASNDetail> ASNDetailList = new List<ASNDetail>();
            //foreach (ASNDetail asd in obj)
            //{
            //    ASNDetailList.Add(asd);
            //}
            //this.dataGrid1.DataSource = (
            //        from i in ASNDetailList
            //        select new
            //        {
            //            入库指令号 = i.StNo,
            //            行号 = i.StLine,
            //            箱号编码 = i.CartonNo,
            //            小箱 = i.CartonSeq
            //        }).ToList();


        }

        private void txtSN_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGrid1_GotFocus(object sender, EventArgs e)
        {
            DataGrid dataGrid1 = sender as DataGrid;
            int index = ((DataGrid)sender).CurrentCell.RowNumber;
            if (((DataTable)(dataGrid1.DataSource)).Rows.Count > 0)
            {
                ((DataGrid)sender).Select(index);
            }        
        }

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGrid dataGrid1 = sender as DataGrid;
            int index = ((DataGrid)sender).CurrentCell.RowNumber;
            if (((DataTable)(dataGrid1.DataSource)).Rows.Count > 0)
            {
                ((DataGrid)sender).Select(index);
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        /*
         * 第一步：声明列头   
         * string[] dgColumns = new string[10] { "ID", "SID", "Version", "状态", "工作代码", "工艺", "计划加工数量", "合格数量", "废品数量", "进度率" };
         * 搜索第二步：初始化datatable    
         * DataTable datatable = new DataTable(); 
         * for (int i = 0; i < dgColumns.Length; i++) 
         * {                
         * DataColumn dc = new DataColumn(dgColumns[i], typeof(string));   
         * datatable.Columns.Add(dc);            }
         * 第三步：获取数据源   
         * TaskInfo[] Tasks = WebServices.NapsBarcodeController.SearchTasks(string.Empty);      
         * if (null != Tasks)        
         * {                  
         * for (int i = 0; i < Tasks.Length; i++)                 
         * {                       
         * DataRow dr = datatable.NewRow();      
         * dr[0] = Tasks[i].Id;//工作ID(可能重复)           
         * dr[1] = Tasks[i].SID;//工作SID(不重复) 
         * dr[2] = Tasks[i].Version;//工作版本      
         * dr[3] = WorkState.StateCaptions[Tasks[i].State - 1];//工作状态   
         * dr[4] = Tasks[i].Code;//工作代码                    
         * dr[5] = (null != Tasks[i].Process) ? Tasks[i].Process.Code : "";//工艺    
         * dr[6] = Tasks[i].PlantAmount.ToString();//计划加工数量            
         * dr[7] = Tasks[i].ActualAcount.ToString();//合格数量         
         * dr[8] = Tasks[i].ActualWaster.ToString();//废品数量             
         * dr[9] = Tasks[i].ActualEvolveRate.ToString();//进度率       
         * datatable.Rows.Add(dr);                    
         * }               
         * }
         * 第四步：Grid绑定数据    
         * dataGrid.DataSource = datatable;
         * 第五步：隐藏某些列    
         * DataGridTableStyle ts = new DataGridTableStyle();  
         * ts.MappingName = datatable.Namespace;        
         * dataGrid.TableStyles.Add(ts);        
         * dataGrid.TableStyles[0].GridColumnStyles[0].Width = 0;//隐藏第0列Id, 
         * dataGrid.TableStyles[0].GridColumnStyles[1].Width = 0;//隐藏第1列SID,           
         * dataGrid.TableStyles[0].GridColumnStyles[2].Width = 0;//隐藏第1列Version
         * 
         */
    }
}