using System;
using System.Web.UI;
using System.Collections;

using Infragistics.Web.UI.GridControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Infragistics.Web.UI;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// BaseMPage 的摘要说明。
    /// </summary>
    public class BaseMPageNew : BaseMPageMinus
    {
        public BaseMPageNew()
            : base()
        {
           // this.Load += new System.EventHandler(this.Page_Load);
        }
        protected ButtonHelper buttonHelper = null;
        protected ExcelExporter excelExporter = null;

        protected override void OnInit(EventArgs e)
        {

            this.Load += new System.EventHandler(this.Page_Load);
            base.OnInit(e);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();
        }

        protected virtual void InitOnPostBack()
        {
            #region ButtonHelper
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            //没有绑定事件的才需要新增点击事件
            if (this.buttonHelper.CmdAdd != null && buttonHelper.CmdAdd.Attributes["bindclick"] == null)
            {
                this.buttonHelper.CmdAdd.ServerClick += new EventHandler(cmdAdd_Click);
            }

            if (this.buttonHelper.CmdSelect != null && buttonHelper.CmdSelect.Attributes["bindclick"] == null)
            {
                this.buttonHelper.CmdSelect.ServerClick += new EventHandler(cmdSelect_Click);
            }

            if (this.buttonHelper.CmdDelete != null && buttonHelper.CmdDelete.Attributes["bindclick"] == null)
            {
                this.buttonHelper.CmdDelete.ServerClick += new EventHandler(cmdDelete_Click);
            }

            if (this.buttonHelper.CmdSave != null && buttonHelper.CmdSave.Attributes["bindclick"] == null)
            {
                this.buttonHelper.CmdSave.ServerClick += new EventHandler(cmdSave_Click);
            }

            if (this.buttonHelper.CmdCancel != null && buttonHelper.CmdCancel.Attributes["bindclick"] == null)
            {
                this.buttonHelper.CmdCancel.ServerClick += new EventHandler(cmdCancel_Click);
            }

            if (this.buttonHelper.CmdQuery != null && buttonHelper.CmdQuery.Attributes["bindclick"] == null)
            {
                this.buttonHelper.CmdQuery.ServerClick += new EventHandler(cmdQuery_Click);
            }

            if (this.buttonHelper.CmdExport != null && buttonHelper.CmdExport.Attributes["bindclick"] == null)
            {
                this.buttonHelper.CmdExport.ServerClick += new EventHandler(cmdExport_Click);
            }
            #endregion

            #region GridHelper
            this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
            this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);


            if (this.gridWebGrid2 != null)
            {
                this.gridHelper2 = new GridHelperNew(this.gridWebGrid2, this.DtSource2);
                this.gridHelper2.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource2);
                this.gridHelper2.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount2);
                this.gridHelper2.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow2);
            }
            if (this.gridWebGrid3 != null)
            {
                this.gridHelper3 = new GridHelperNew(this.gridWebGrid3, this.DtSource3);
                this.gridHelper3.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource3);
                this.gridHelper3.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount3);
                this.gridHelper3.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow3);
            }

            #endregion

            #region Exporter
            this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
            this.excelExporter.Page = this;
            this.excelExporter.LanguageComponent = this.languageComponent1;
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
            #endregion

        }

        protected override void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            if (commandName == "Edit")
            {
                object obj = this.GetEditObject(row);

                if (obj != null)
                {
                    this.SetEditObject(obj);

                    this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
                }
            }
            else
            {
                Grid_ClickCellButton(row, commandName);
                Grid_ClickCell(row, commandName);
            }
        }

        //protected virtual void Grid_ClickCellButton(DataRow row, HandleCommandEventArgs e)
        //{
        //    //需要子类自定义除了编辑按钮意外的其他按钮的点击事件
        //}
        //protected virtual void Grid_ClickCell(DataRow row, HandleCommandEventArgs e)
        //{
        //    //需要子类自定义除了编辑按钮意外的其他按钮的点击事件
        //}


        protected virtual void Grid_ClickCellButton(GridRecord row, string command)
        {
            //需要子类自定义除了编辑按钮意外的其他按钮的点击事件
        }
        protected virtual void Grid_ClickCell(GridRecord row, string command)
        {
            //需要子类自定义除了编辑按钮意外的其他按钮的点击事件
        }

        private void InitButtons()
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
        }


        #region WebGrid

        #endregion

        #region Button
        /// <summary>
        /// 点击新增按钮时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmdAdd_Click(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                object obj = this.GetEditObject();

                if (obj == null)
                {
                    return;
                }

                this.AddDomainObject(obj);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        /// <summary>
        /// 点击选择按钮时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmdSelect_Click(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                object obj = this.GetEditObject();

                if (obj == null)
                {
                    return;
                }

                this.AddDomainObject(obj);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Select);
            }
        }

        /// <summary>
        /// 点击删除按钮时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmdDelete_Click(object sender, System.EventArgs e)
        {
            //try
            //{
            ArrayList array = this.gridHelper.GetCheckedRows();
            object obj = null;

            if (array.Count > 0)
            {
                ArrayList objList = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    obj = this.GetEditObject(row);

                    if (obj != null)
                    {
                        objList.Add(obj);
                    }
                }

                this.DeleteDomainObjects(objList);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Delete);
            }
            //}
            //catch (Exception ex)
            //{
            //    showErrorDialog(ex);
            //}
        }

        /// <summary>
        /// 点击保存按钮时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmdSave_Click(object sender, System.EventArgs e)
        {
            if (this.ValidateInput())
            {
                object obj = this.GetEditObject();

                if (obj == null)
                {
                    return;
                }

                this.UpdateDomainObject(obj);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Save);
            }
        }

        /// <summary>
        /// 点击清空按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmdCancel_Click(object sender, System.EventArgs e)
        {
            this.buttonHelper.PageActionStatusHandle(PageActionType.Cancel);
        }

        /// <summary>
        /// 点击查询按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmdQuery_Click(object sender, System.EventArgs e)
        {
            this.gridHelper.RequestData();
            if (this.gridHelper2 != null)
                this.gridHelper2.RequestData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        /// <summary>
        /// 点击查询按钮时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmdExport_Click(object sender, System.EventArgs e)
        {
            this.excelExporter.Export();
        }

        /// <summary>
        /// 获得导出数据
        /// </summary>
        /// <returns></returns>
        protected object[] LoadDataSource()
        {
            return this.LoadDataSource(1, int.MaxValue);
        }
        #endregion

        #region override

        #region CRUD
        /// <summary>
        /// 获得查询所得的分页数据，需重载
        /// </summary>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        /// 
        protected virtual object[] LoadDataSource(int inclusive, int exclusive)
        {
            return null;
        }
        protected virtual object[] LoadDataSource2(int inclusive, int exclusive)
        {
            return null;
        }

        protected virtual object[] LoadDataSource3(int inclusive, int exclusive)
        {
            return null;
        }


        /// <summary>
        /// 获得查询所得的数据总行数，需重载
        /// </summary>
        /// <returns></returns>
        protected virtual int GetRowCount()
        {
            return 0;
        }
        protected virtual int GetRowCount2()
        {
            return 0;
        }
        protected virtual int GetRowCount3()
        {
            return 0;
        }
        /// <summary>
        /// 新增一个DomainObject入数据库，需重载
        /// </summary>
        /// <param name="domainObject"></param>
        protected virtual void AddDomainObject(object domainObject)
        {
        }

        /// <summary>
        /// 更新一个DomainObject入数据库，需重载
        /// </summary>
        /// <param name="domainObject"></param>
        protected virtual void UpdateDomainObject(object domainObject)
        {
        }

        /// <summary>
        /// 从数据库删除多个DomainObject，需重载
        /// </summary>
        /// <param name="domainObject"></param>
        protected virtual void DeleteDomainObjects(ArrayList domainObjects)
        {
        }

        #endregion

        #region Format Data


        /// <summary>
        /// 将object各字段组成UltraGridRow，需重载
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual DataRow GetGridRow(object obj)
        {
            return null;
        }
        protected virtual DataRow GetGridRow2(object obj)
        {
            return null;
        }
        protected virtual DataRow GetGridRow3(object obj)
        {
            return null;
        }
        /// <summary>
        /// 处理新增和更新状态变化后的编辑区可编辑性，需重载
        /// </summary>
        /// <param name="pageAction"></param>
        protected virtual void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                //				this.txtSegmentCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                //				this.txtSegmentCodeEdit.ReadOnly = true;
            }
        }

        /// <summary>
        /// 格式化object的各字段成字符串，用于导出数据，需重载
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual string[] FormatExportRecord(object obj)
        {
            return null;
        }

        /// <summary>
        /// 输出object各字段的名称，作为导出数据列的标题，需重载
        /// </summary>
        /// <returns></returns>
        protected virtual string[] GetColumnHeaderText()
        {
            return null;
        }
        #endregion

        #region Object <--> Page

        /// <summary>
        /// 从编辑区获得输入值，组成DomainObject，需重载
        /// </summary>
        /// <returns></returns>
        protected virtual object GetEditObject()
        {
            return null;
        }

        ///// <summary>
        ///// DataRow获得输入值，组成DomainObject，需重载,编辑和点击link按钮的时候用到
        ///// </summary>
        ///// <returns></returns>
        //protected virtual object GetEditObject(DataRow row)
        //{
        //    return null;
        //}

        /// <summary>
        /// 从Grid中的行中获得输入值，组成DomainObject，需重载，删除的时候用到
        /// </summary>
        /// <returns></returns>
        protected virtual object GetEditObject(GridRecord row)
        {
            return null;
        }


        /// <summary>
        /// 将DomainObject填入编辑区，如果为null则清空页面，需重载
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void SetEditObject(object obj)
        {
        }

        /// <summary>
        /// 验证编辑区各输入值的有效性
        /// 如必填值是否为空，长度是否超出限制，输入格式是否正确...
        /// </summary>
        protected virtual bool ValidateInput()
        {
            return true;
        }

        #endregion

        #endregion
    }
}
