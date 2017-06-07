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
    public partial class FGFPickMaterial : UserControl
    {
        PackagingOperationsService.PackagingOperationsService _PackagingOperationsService;

        private string PickNo = string.Empty;
        private string CarInvNo = string.Empty;
        private string CartonNo = string.Empty;
        private string GFHWItemCode = string.Empty;
        private string GFPackingSEQ = string.Empty;
        private decimal SuiteQTY = 0;

        #region 初始化，回车事件

        public FGFPickMaterial(string pickNo, string carInvNo, string cartonNo, string gfHWItemCode, string gfPackingSEQ, string suiteQTY)
        {
            InitializeComponent();

            this.PickNo = pickNo;
            this.CarInvNo = carInvNo;
            this.CartonNo = cartonNo;
            this.GFHWItemCode = gfHWItemCode;
            this.GFPackingSEQ = gfPackingSEQ;
            this.SuiteQTY = decimal.Parse(suiteQTY);

            _PackagingOperationsService = new BenQGuru.eMES.WinCeClient.PackagingOperationsService.PackagingOperationsService();
            _PackagingOperationsService.Url = WebServiceFacade.GetWebServiceURL() + "PackagingOperationsService.asmx";

            BindTransmissionParameter();

            DataTable dt = new DataTable();
            dt = this._PackagingOperationsService.GetDataGrid3(pickNo, gfPackingSEQ, gfPackingSEQ);
            BindDataGrid(dt);

            this.lblMessage.Text = string.Empty;
        }

        //绑定传输参数
        private void BindTransmissionParameter()
        {
            this.lblPickNO.Text = this.PickNo;
            this.lblCarInvNO.Text = this.CarInvNo;
            this.lblCartonNO.Text = this.CartonNo;
        }

        private void FPickMaterial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 允许输入:数字、退格键(8)
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region button

        //确认
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtQTY.Text.Trim()))
            {
                this.lblMessage.Text = "请输入包装数";
                return;
            }
            decimal _qty = decimal.Parse(dataGrid1[dataGrid1.CurrentRowIndex, 4].ToString());
            decimal qty = decimal.Parse(this.txtQTY.Text.Trim());
            if (qty > _qty)
            {
                this.lblMessage.Text = "修改包装数不能大于默认显示包装数";
                return;
            }
            string dqMaterialNo = dataGrid1[dataGrid1.CurrentRowIndex, 2].ToString();
            string userCode = ApplicationService.Current().LoginInfo.UserCode.ToUpper();

            try
            {
                this._PackagingOperationsService.BeginTransaction();

                //1.	检查图6.10.1：包装作业介面中编辑区域选择拣货任务令号、光伏华为编码、光伏包装序号在发货箱单明细物料信息表(TBLCartonInvDetailMaterial)中对应发货箱号条码(TBLCartonInvDetail.CARTONNO)的数量是否小于编辑区域输入的套件数，如小于则报错提示选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数
                if (this._PackagingOperationsService.GetCartonNoCount(this.PickNo, this.GFHWItemCode, this.GFPackingSEQ) < this.SuiteQTY)
                {
                    this.lblMessage.Text = "选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数";
                    return;
                }

                //2.	检查包装箱号是否存在当前拣货任务令号对应发货箱单的发货箱单明细信息表(TBLCartonInvDetail)中，不存在则新增发货箱单明细信息表(TBLCartonInvDetail)数据
                if (this._PackagingOperationsService.GetCartonInvDetail(this.CarInvNo, this.CartonNo) == null)
                {
                    this._PackagingOperationsService.AddCartonInvDetail(this.CarInvNo, this.PickNo, "Pack", this.CartonNo, userCode);
                }

                //3.	检查包装箱号、鼎桥编码是否存在当前拣货任务令号对应发货箱单的发货箱单明细物料信息表(TBLCartonInvDetailMaterial)中
                //1)	存在则更新发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                //2)	不存则新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                if (this._PackagingOperationsService.QueryPickDetailMaterial(this.PickNo, this.CartonNo) == null)
                {
                    this._PackagingOperationsService._OperateCartonInvDetailMaterial(this.PickNo, this.CarInvNo, this.CartonNo, qty, userCode);
                }

                //4.	更新拣拣货任务令头表(TBLPICK)数据
                this._PackagingOperationsService.UpdatePick(this.PickNo, "Pack", userCode);

                //5.	更新拣货任务令明细表(TBLPICKDETAIL)数据
                //this._PackagingOperationsService.UpdatePickdetail(this.PickNo, this.CartonNo, "ClosePack", qty, userCode);

                //5.	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                if (this._PackagingOperationsService.UpdatePickdetailmaterialAndGetQty(this.PickNo, dqMaterialNo, qty, userCode) > 0)
                {
                    this.lblMessage.Text = "输入的包装数过大";
                    this.txtQTY.Focus();
                    return;
                }

                this._PackagingOperationsService.CommitTransaction();

                this.lblMessage.Text = "提交成功";

                FGFPackagingOperations fGFPackagingOperations = new FGFPackagingOperations();
                fGFPackagingOperations.cmbPickNO_SelectedIndexChanged(null, null);

                this.Hide();
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = "提交失败：" + ex.Message;
                this._PackagingOperationsService.RollbackTransaction();
            }
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

        private void dataGrid1_CurrentCellChanged(object sender, EventArgs e)
        {
            dataGrid1.Select(dataGrid1.CurrentRowIndex);
            if (dataGrid1[dataGrid1.CurrentRowIndex, 0].ToString() == "√")
            {
                dataGrid1[dataGrid1.CurrentRowIndex, 0] = "";
                this.txtQTY.Text = string.Empty;
            }
            else
            {
                dataGrid1[dataGrid1.CurrentRowIndex, 0] = "√";
                this.txtQTY.Text = dataGrid1[dataGrid1.CurrentRowIndex, 4].ToString();
            }
        }
        #endregion
    }
}
