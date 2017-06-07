using System;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using Infragistics.Web.UI.GridControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Infragistics.Web.UI;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.Helper
{
    public class BaseMPageDGrid : BasePage
    {
        public BaseMPageDGrid()
            : base()
        {
        }

        protected GridHelperNew gridHelper = null;
        protected GridHelperNew gridHelper2 = null;
        protected GridHelperNew gridHelper3 = null;
        protected ButtonHelper buttonHelper = null;
        protected ExcelExporter excelExporter = null;

        private WebDataGrid gridWebGrid;

        private WebDataGrid gridWebGrid2;
        private WebDataGrid gridWebGrid3;
        private List<WebDataGrid> listGrid;
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent;
        private System.ComponentModel.IContainer components;

        public DataTable DtSource
        {
            get
            {
                if (this.ViewState["$DtSource"] == null)
                {
                    this.ViewState["$DtSource"] = new DataTable();
                }

                return (DataTable)this.ViewState["$DtSource"];
            }
            set
            {
                this.ViewState["$DtSource"] = value;
            }
        }
        public DataTable DtSource2
        {
            get
            {
                if (this.ViewState["$DtSource2"] == null)
                {
                    this.ViewState["$DtSource2"] = new DataTable();
                }

                return (DataTable)this.ViewState["$DtSource2"];
            }
            set
            {
                this.ViewState["$DtSource2"] = value;
            }
        }
        public DataTable DtSource3
        {
            get
            {
                if (this.ViewState["$DtSource3"] == null)
                {
                    this.ViewState["$DtSource3"] = new DataTable();
                }

                return (DataTable)this.ViewState["$DtSource3"];
            }
            set
            {
                this.ViewState["$DtSource3"] = value;
            }
        }
        public bool needUpdatePanel
        {
            get
            {
                if (this.ViewState["$needUpdatePanel"] == null)
                {
                    this.ViewState["$needUpdatePanel"] = true;
                }

                return Convert.ToBoolean(this.ViewState["$needUpdatePanel"]);
            }
            set
            {
                this.ViewState["$needUpdatePanel"] = value;
            }
        }


        #region Init
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();

            base.OnInit(e);
        }


        protected override void AddParsedSubObject(object obj)
        {
            //动态添加scriptmanager
            if (obj is HtmlForm)
            {
                HtmlForm form1 = obj as HtmlForm;

                ScriptManager sc1 = new ScriptManager();
                sc1.ID = "sc1";
                sc1.EnablePartialRendering = true;
                form1.Controls.AddAt(0, sc1);

                UpdatePanel up1 = new UpdatePanel();
                if (needUpdatePanel)
                {

                    up1.ID = "up1";
                    foreach (Control formChildren in form1.Controls)
                    {
                        if (formChildren is HtmlTable)
                        {
                            up1.ContentTemplateContainer.Controls.Add(formChildren);
                        }
                    }
                    form1.Controls.Add(up1);
                }

                //存放有客户端浏览器决定的grid高度
                HtmlInputHidden hideGridHeight = new HtmlInputHidden();
                hideGridHeight.ID = "gridHeigt";
                form1.Controls.Add(hideGridHeight);

                //存放有客户端浏览器决定的grid宽度
                HtmlInputHidden hideGridWidth = new HtmlInputHidden();
                hideGridWidth.ID = "gridWidth";
                form1.Controls.Add(hideGridWidth);


                //用来触发页面异步回传
                HtmlButton btnPostBack = new HtmlButton();
                btnPostBack.ID = "btnPostBack";
                btnPostBack.Style.Add("display", "none");
                if (needUpdatePanel)
                {
                    up1.ContentTemplateContainer.Controls.Add(btnPostBack);
                }
                else
                {
                    form1.Controls.Add(btnPostBack);
                }
            }

            base.AddParsedSubObject(obj);
        }

        private void InitializeComponent()
        {
            WebDataGrid gridWebGridTemp = null;
            this.listGrid = this.GetWebGrid();
            for (int i = 0; i < listGrid.Count; i++)
            {

                gridWebGridTemp = listGrid[i];

                if (listGrid[i].ID == "gridWebGrid")
                {
                    this.gridWebGrid = listGrid[i];
                }
                if (listGrid[i].ID == "gridWebGrid2")
                {
                    this.gridWebGrid2 = listGrid[i];
                }
                if (listGrid[i].ID == "gridWebGrid3")
                {
                    this.gridWebGrid3 = listGrid[i];
                }

                //this.gridWebGrid.ItemCommand += new ItemCommandEventHandler(gridWebGrid_ItemCommand);
                gridWebGridTemp.EnableDataViewState = true;
                gridWebGridTemp.EnableViewState = true;
                gridWebGridTemp.EnableAjax = false;
                //this.gridWebGrid.ClientEvents.Initialize = "grid_Initialize";
                gridWebGridTemp.StyleSetName = "Office2007Blue";
                //this.gridWebGrid.BorderStyle = BorderStyle.Solid;
                gridWebGridTemp.AutoGenerateColumns = false;
                gridWebGridTemp.ClientEvents.Click = "Gird_ClientClick";
                //if (this.DtSource == null)
                //    this.DtSource = new DataTable();
                this.languageComponent = this.GetLanguageComponent();

                this.components = new System.ComponentModel.Container();

                this.excelExporter = new BenQGuru.eMES.Web.Helper.ExcelExporter(this.components);
                this.excelExporter.Page = this;
                this.excelExporter.LanguageComponent = this.languageComponent;


            }
            this.Load += new System.EventHandler(this.Page_Load);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            this.InitOnPostBack();
            foreach (WebDataGrid item in listGrid)
            {
                if (item.ID == "gridWebGrid")
                {
                    this.InitWebGrid();
                }

                if (item.ID == "gridWebGrid2")
                {
                    this.InitWebGrid2();
                }
                if (item.ID == "gridWebGrid3")
                {
                    this.InitWebGrid3();
                }
            }
            this.Page.ClientScript.RegisterClientScriptInclude("Jquery", this.VirtualHostRoot + "Scripts/jquery-1.9.1.js");

            //调整列宽的前台函数
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), @"<script>
        var parentWindow=parent.window;
        $(function () {

                //调整下方按钮居中，多浏览器
              $('table[class=\'toolBar\']').wrap('<center></center>');
                //针对嵌套iframe的特殊页面，向上寻找3次找到FStartPage
                parentWindow=parent.window;
                if(parentWindow.location.pathname.indexOf('FStartPage.aspx')<0)
                {
                    parentWindow=parentWindow.parent.window;
                }
                if(parentWindow.location.pathname.indexOf('FStartPage.aspx')<0)
                {
                    parentWindow=parentWindow.parent.window;
                }            
        });

        //Link按钮点击
        function Gird_ClientClick(sender, e) {
            if (e.get_item() == null)
                return;

            if ($(e.get_item().get_element()).css('backgroundImage')=='none')
                return;

            var key = e.get_item().get_column().get_key();
            var index = e.get_item().get_row().get_index();

            document.forms[0].__EVENTTARGET.value = 'Link';
            document.forms[0].__EVENTARGUMENT.value = key + ',' + index;
            if(parentWindow.BrowserType().ie=='7.0')
            {
                __doPostBack('Link',key + ',' + index);
            }
            else
            {
                $('#btnPostBack').click();
            }
        }       
         </script>
        ");

            //注册scriptManager的捕捉错误的脚本,弹出自定义的DIV

            for (int i = 0; i < listGrid.Count; i++)
            {
                if (listGrid[i].ID == "gridWebGrid")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), @" 
              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginFunc);
            function beginFunc(sender, args) {
                //显示等待
                $find('gridWebGrid').get_ajaxIndicator().show($find('gridWebGrid'));
            }

           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
        
            //查询完毕，关闭等待           
            $find('gridWebGrid').get_ajaxIndicator().hide($find('gridWebGrid'));

            //调整下方按钮居中，多浏览器
            $('table[class=\'toolBar\']').wrap('<center></center>');

            //截住页面Updatepanel中发生的所有错误，并使用主页面上自定义的DIV弹出提示
                if(args.get_error() != undefined && args.get_error().httpStatusCode == '500')  
                {
                    parentWindow.showErrorDialog(args.get_error().message.split('---')[0].replace(args.get_error().name+':',''),args.get_error().message.split('---')[1]);                               
                    args.set_errorHandled(true);
                }
            }", true);
                }
                if (listGrid[i].ID == "gridWebGrid2")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), @" 
              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginFunc);
            function beginFunc(sender, args) {
                //显示等待
                $find('gridWebGrid2').get_ajaxIndicator().show($find('gridWebGrid2'));
            }

           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
        
            //查询完毕，关闭等待           
            $find('gridWebGrid2').get_ajaxIndicator().hide($find('gridWebGrid2'));

            //调整下方按钮居中，多浏览器
            $('table[class=\'toolBar\']').wrap('<center></center>');

            //截住页面Updatepanel中发生的所有错误，并使用主页面上自定义的DIV弹出提示
                if(args.get_error() != undefined && args.get_error().httpStatusCode == '500')  
                {
                    parentWindow.showErrorDialog(args.get_error().message.split('---')[0].replace(args.get_error().name+':',''),args.get_error().message.split('---')[1]);                               
                    args.set_errorHandled(true);
                }
            }", true);
                }
                if (listGrid[i].ID == "gridWebGrid3")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), @" 
              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginFunc);
            function beginFunc(sender, args) {
                //显示等待
                $find('gridWebGrid3').get_ajaxIndicator().show($find('gridWebGrid3'));
            }

           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
        
            //查询完毕，关闭等待           
            $find('gridWebGrid3').get_ajaxIndicator().hide($find('gridWebGrid3'));

            //调整下方按钮居中，多浏览器
            $('table[class=\'toolBar\']').wrap('<center></center>');

            //截住页面Updatepanel中发生的所有错误，并使用主页面上自定义的DIV弹出提示
                if(args.get_error() != undefined && args.get_error().httpStatusCode == '500')  
                {
                    parentWindow.showErrorDialog(args.get_error().message.split('---')[0].replace(args.get_error().name+':',''),args.get_error().message.split('---')[1]);                               
                    args.set_errorHandled(true);
                }
            }", true);
                }
            }
            ScriptManager.GetCurrent(this.Page).AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(BaseMPageNew_AsyncPostBackError);


            if (!IsPostBack)
            {

                this.InitPageLanguage(this.languageComponent, false);

                this.InitButtons();
                foreach (WebDataGrid item in listGrid)
                {
                    if (item.ID == "gridWebGrid")
                    {
                        this.gridWebGrid.DataSource = this.DtSource;
                        this.gridWebGrid.DataBind();
                    }
                    if (item.ID == "gridWebGrid2")
                    {
                        this.gridWebGrid2.DataSource = this.DtSource2;
                        this.gridWebGrid2.DataBind();
                    }
                    if (item.ID == "gridWebGrid3")
                    {
                        this.gridWebGrid3.DataSource = this.DtSource3;
                        this.gridWebGrid3.DataBind();
                    }
                }
            }
            else
            {
                //如果点击的是Grid内的link按钮，
                if (Request.Params["__EVENTTARGET"].ToString() == NewColumnStyle.Link.ToString())
                {
                    //方案一，需要重新刷新Grid的数据，否则不会触发itemcommand事件。动态模板列的原因。
                    //缺点，每次点击Grid内的link按钮都需要像数据库请求数据
                    //this.gridHelper.RefreshData();

                    //方案二，读取页面POSTBACK隐藏控件中已写好的值
                    //__EVENTARGUMENT写的是commandName+行号，写入代码见GridHelperNew的743行

                    string cmdAndRow = Request.Params["__EVENTARGUMENT"].ToString();
                    string[] strList = cmdAndRow.Split(new char[] { ',' });
                    string commandName = strList[0];
                    string index = strList[1];
                    //this.gridWebGrid.DataSource = this.DtSource;
                    //this.gridWebGrid.DataBind();
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clearEventTarhet", "<script language='javascript'>document.forms[0].__EVENTTARGET.value='';</script>");
                    if (commandName == "PackingDetail")
                    {

                        this.gridWebGrid_ItemCommand(this.gridWebGrid2.Rows[Convert.ToInt32(index)], commandName);
                    }
                    else
                    {
                        this.gridWebGrid_ItemCommand(this.gridWebGrid.Rows[Convert.ToInt32(index)], commandName);
                    }

                }
                else if (Request.Params["__EVENTTARGET"].ToString() == "gridWebGrid" && Request.Params["__EVENTARGUMENT"].ToString() == "ColumnSorting")
                {

                    //Grid排序触发的页面回传需要重新加载数据
                    //this.gridHelper.RefreshData();
                    this.gridWebGrid.DataSource = this.DtSource;
                    this.gridWebGrid.DataBind();
                }
                else if (Request.Params["__EVENTTARGET"].ToString() == "gridWebGrid2" && Request.Params["__EVENTARGUMENT"].ToString() == "ColumnSorting")
                {

                    //Grid排序触发的页面回传需要重新加载数据
                    //this.gridHelper.RefreshData();
                    this.gridWebGrid2.DataSource = this.DtSource2;
                    this.gridWebGrid2.DataBind();
                }
                else if (Request.Params["__EVENTTARGET"].ToString() == "gridWebGrid3" && Request.Params["__EVENTARGUMENT"].ToString() == "ColumnSorting")
                {

                    //Grid排序触发的页面回传需要重新加载数据
                    //this.gridHelper.RefreshData();
                    this.gridWebGrid3.DataSource = this.DtSource3;
                    this.gridWebGrid3.DataBind();
                }
            }


        }
        /**Encode for Javascript. */
        public static String jsEncoder(String str)
        {
            if (str == null || str.Equals(""))
                return "";
            String res_str;
            res_str = str.Replace("\\", "\\\\");    //将\替换成\\ 
            res_str = res_str.Replace("'", "\\'");    //将'替换成\' 
            res_str = res_str.Replace("\"", "\\\"");//将"替换成\"  
            res_str = res_str.Replace("\r\n", "\\\n");//将\r\n替换成\\n     
            res_str = res_str.Replace("\n", "\\\n");//将\n替换成\\n     
            res_str = res_str.Replace("\r", "\\\n");//将\r替换成\\n     
            return res_str;
        }

        void BaseMPageNew_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            Exception ex = e.Exception;
            string errMsg = string.Join("@", ex.Message.Split(Environment.NewLine.ToCharArray()));
            string innerErrMsg = "";
            ArrayList errList = new ArrayList();

            Exception innerEx = ex.InnerException;

            while (innerEx != null)
            {
                if (innerEx.Message != null)
                {
                    errList.Add(string.Join("@", innerEx.Message.Split(Environment.NewLine.ToCharArray())));
                }

                innerEx = innerEx.InnerException;
            }

            innerErrMsg = string.Join("@", (string[])errList.ToArray(typeof(string)));
#if DEBUG

            errMsg = MessageCenter.ParserMessage(errMsg, this.languageComponent).Replace("@", Environment.NewLine);
            innerErrMsg = FormatHelper.CleanString(MessageCenter.ParserMessage(innerErrMsg, this.languageComponent)).Replace("@", Environment.NewLine);
#endif

            ScriptManager.GetCurrent(this.Page).AsyncPostBackErrorMessage = jsEncoder(errMsg) + "---" + jsEncoder(innerErrMsg);
        }

        private void InitOnPostBack()
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
            foreach (WebDataGrid item in listGrid)
            {
                if (item.ID == "gridWebGrid")
                {
                    this.gridHelper = new GridHelperNew(this.gridWebGrid, this.DtSource);
                    this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource);
                    this.gridHelper.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount);
                    this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow);
                }
                else if (item.ID == "gridWebGrid2")
                {
                    this.gridHelper2 = new GridHelperNew(this.gridWebGrid2, this.DtSource2);
                    this.gridHelper2.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource2);
                    this.gridHelper2.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount2);
                    this.gridHelper2.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow2);
                }
                else if (item.ID == "gridWebGrid3")
                {
                    this.gridHelper3 = new GridHelperNew(this.gridWebGrid3, this.DtSource3);
                    this.gridHelper3.LoadDataSourceHandle = new LoadDataSourceDelegateNew(this.LoadDataSource3);
                    this.gridHelper3.GetRowCountHandle = new GetRowCountDelegateNew(this.GetRowCount3);
                    this.gridHelper3.BuildGridRowhandle = new BuildGridRowDelegateNew(this.GetGridRow3);
                }
            }
            #endregion

            #region Exporter
            this.excelExporter.LoadExportDataHandle = new LoadExportDataDelegate(LoadDataSource);
            this.excelExporter.FormatExportRecordHandle = new FormatExportRecordDelegate(FormatExportRecord);
            this.excelExporter.GetColumnHeaderTextHandle = new GetColumnHeaderTextDelegate(GetColumnHeaderText);
            #endregion

        }

        //public void gridWebGrid_ItemCommand(object sender, HandleCommandEventArgs e)
        //{
        //    DataRow row = (((sender as Control).NamingContainer as TemplateContainer).DataItem as DataRowView).Row;
        //    //if (e.CommandName == "Edit")
        //    //{
        //    //    object obj = this.GetEditObject(row);

        //    //    if (obj != null)
        //    //    {
        //    //        this.SetEditObject(obj);

        //    //        this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
        //    //    }
        //    //}
        //    //else
        //    //{
        //       // Grid_ClickCellButton(row, e);
        //        Grid_ClickCell(row, e);
        //    //}
        //}

        public void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            //if (commandName == "Edit")
            //{
            //    object obj = this.GetEditObject(row);

            //    if (obj != null)
            //    {
            //        this.SetEditObject(obj);

            //        this.buttonHelper.PageActionStatusHandle(PageActionType.Update);
            //    }
            //}
            //else
            //{
            Grid_ClickCellButton(row, commandName);
            Grid_ClickCell(row, commandName);
            //}
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
        #endregion

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

        #region Control
        /// <summary>
        /// 返回UltraWebGrid，如果Grid名称为gridWebGrid，不用重载
        /// </summary>
        /// <returns></returns>
        protected virtual List<WebDataGrid> GetWebGrid()
        {
            List<WebDataGrid> list = new List<WebDataGrid>();
            Control ctrl = this.FindControl("gridWebGrid");
            Control ctr2 = this.FindControl("gridWebGrid2");
            Control ctr3 = this.FindControl("gridWebGrid3");
            if (ctrl != null)
            {
                list.Add((WebDataGrid)ctrl);
            }
            if (ctr2 != null)
            {
                list.Add((WebDataGrid)ctr2);

            }
            if (ctr3 != null)
            {
                list.Add((WebDataGrid)ctr3);

            }
            return list;
        }

        /// <summary>
        /// 返回LanguageComponent，需重载
        /// </summary>
        /// <returns></returns>
        protected virtual ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return null;
        }
        #endregion

        #region CRUD
        /// <summary>
        /// 获得查询所得的分页数据，需重载
        /// </summary>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
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
        /// 初始化WebGrid，需重载，并在函数最后调用base.InitWebGrid();
        /// </summary>
        protected virtual void InitWebGrid()
        {
            this.gridHelper.ApplyLanguage(this.languageComponent);

            //添加系统主键
            this.gridHelper.AddDataColumn("GUID", "GUID", true);

            this.gridWebGrid.DataKeyFields = "GUID";
            this.DtSource.PrimaryKey = new DataColumn[] { DtSource.Columns["GUID"] };

            //调整高度和宽度
            if (Request.Params["gridHeigt"] != null && Request.Params["gridHeigt"].ToString() != string.Empty)
            {
                this.gridWebGrid.Height = Convert.ToInt32(Request.Params["gridHeigt"]);
            }

            if (Request.Params["gridWidth"] != null && Request.Params["gridWidth"].ToString() != string.Empty)
            {
                //根据页面大小计算默认宽度,解决列宽改变过大导致变形问题。
                if (this.gridWebGrid.DefaultColumnWidth.IsEmpty && this.gridWebGrid.Columns.Count > 1)
                {
                    int noWidthColumnCount = 0;//未设置列宽的列数
                    double boundWithTotal = 0; //已设置列宽的列宽综合
                    foreach (GridField col in this.gridWebGrid.Columns)
                    {
                        if (col.Hidden)
                            continue;
                        if (col.Width.IsEmpty)
                        {
                            noWidthColumnCount++;
                        }
                        else
                        {
                            boundWithTotal += (col.Width.Value + 17);//17=boder-right(1px)+padding-left(8px)+padding-ringt(8px);
                        }
                    }
                    if (boundWithTotal < Convert.ToDouble(Request.Params["gridWidth"]))
                        this.gridWebGrid.DefaultColumnWidth = new Unit((Convert.ToDouble(Request.Params["gridWidth"]) - boundWithTotal - 37) / noWidthColumnCount - 17, UnitType.Pixel);//37(rownum宽度35+其border宽度2)
                }
            }
        }
        protected virtual void InitWebGrid2()
        {
            this.gridHelper2.ApplyLanguage(this.languageComponent);

            //添加系统主键
            this.gridHelper2.AddDataColumn("GUID", "GUID", true);

            this.gridWebGrid2.DataKeyFields = "GUID";
            this.DtSource2.PrimaryKey = new DataColumn[] { DtSource2.Columns["GUID"] };

            //调整高度和宽度

            if (Request.Params["gridHeigt"] != null && Request.Params["gridHeigt"].ToString() != string.Empty)
            {
                this.gridWebGrid.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 2 - 5;
                this.gridWebGrid2.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 2 - 5;
            }

            if (Request.Params["gridWidth"] != null && Request.Params["gridWidth"].ToString() != string.Empty)
            {
                //根据页面大小计算默认宽度,解决列宽改变过大导致变形问题。
                if (this.gridWebGrid2.DefaultColumnWidth.IsEmpty && this.gridWebGrid2.Columns.Count > 1)
                {
                    int noWidthColumnCount = 0;//未设置列宽的列数
                    double boundWithTotal = 0; //已设置列宽的列宽综合
                    foreach (GridField col in this.gridWebGrid2.Columns)
                    {
                        if (col.Hidden)
                            continue;
                        if (col.Width.IsEmpty)
                        {
                            noWidthColumnCount++;
                        }
                        else
                        {
                            boundWithTotal += (col.Width.Value + 17);//17=boder-right(1px)+padding-left(8px)+padding-ringt(8px);
                        }
                    }
                    if (boundWithTotal < Convert.ToDouble(Request.Params["gridWidth"]))
                        this.gridWebGrid2.DefaultColumnWidth = new Unit((Convert.ToDouble(Request.Params["gridWidth"]) - boundWithTotal - 37) / noWidthColumnCount - 17, UnitType.Pixel);//37(rownum宽度35+其border宽度2)
                }
            }
        }


        protected virtual void InitWebGrid3()
        {
            this.gridHelper3.ApplyLanguage(this.languageComponent);

            //添加系统主键
            this.gridHelper3.AddDataColumn("GUID", "GUID", true);

            this.gridWebGrid3.DataKeyFields = "GUID";
            this.DtSource3.PrimaryKey = new DataColumn[] { DtSource3.Columns["GUID"] };

            //调整高度和宽度
            if (Request.Params["gridHeigt"] != null && Request.Params["gridHeigt"].ToString() != string.Empty)
            {
                this.gridWebGrid.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 3 - 5;
                this.gridWebGrid2.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 3 - 5;
                this.gridWebGrid3.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 3 - 5;
            }

            if (Request.Params["gridWidth"] != null && Request.Params["gridWidth"].ToString() != string.Empty)
            {
                //根据页面大小计算默认宽度,解决列宽改变过大导致变形问题。
                if (this.gridWebGrid3.DefaultColumnWidth.IsEmpty && this.gridWebGrid3.Columns.Count > 1)
                {
                    int noWidthColumnCount = 0;//未设置列宽的列数
                    double boundWithTotal = 0; //已设置列宽的列宽综合
                    foreach (GridField col in this.gridWebGrid3.Columns)
                    {
                        if (col.Hidden)
                            continue;
                        if (col.Width.IsEmpty)
                        {
                            noWidthColumnCount++;
                        }
                        else
                        {
                            boundWithTotal += (col.Width.Value + 17);//17=boder-right(1px)+padding-left(8px)+padding-ringt(8px);
                        }
                    }
                    if (boundWithTotal < Convert.ToDouble(Request.Params["gridWidth"]))
                        this.gridWebGrid3.DefaultColumnWidth = new Unit((Convert.ToDouble(Request.Params["gridWidth"]) - boundWithTotal - 37) / noWidthColumnCount - 17, UnitType.Pixel);//37(rownum宽度35+其border宽度2)
                }
            }
        }
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
