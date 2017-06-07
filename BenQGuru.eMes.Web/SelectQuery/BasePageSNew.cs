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
    public class BasePageSNew : BasePage
    {
        public BasePageSNew()
            : base()
        {
        }
        protected GridHelperNew gridSelectedHelper = null;
        protected GridHelperNew gridUnSelectedHelper = null;


        protected WebDataGrid gridUnSelected;

        protected WebDataGrid gridSelected;
        private List<WebDataGrid> listGrid;
        private ControlLibrary.Web.Language.LanguageComponent languageComponent;
        private System.ComponentModel.IContainer components;

        public DataTable DtSourceUnSelected
        {
            get
            {
                if (this.ViewState["$DtSourceUnSelected"] == null)
                {
                    this.ViewState["$DtSourceUnSelected"] = new DataTable();
                }

                return (DataTable)this.ViewState["$DtSourceUnSelected"];
            }
            set
            {
                this.ViewState["$DtSourceUnSelected"] = value;
            }
        }
        public DataTable DtSourceSelected
        {
            get
            {
                if (this.ViewState["$DtSourceSelected"] == null)
                {
                    this.ViewState["$DtSourceSelected"] = new DataTable();
                }

                return (DataTable)this.ViewState["$DtSourceSelected"];
            }
            set
            {
                this.ViewState["$DtSourceSelected"] = value;
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
            this.components = new System.ComponentModel.Container();
            this.languageComponent = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent
            // 
            this.languageComponent.Language = "CHS";
            this.languageComponent.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent.RuntimePage = null;
            this.languageComponent.RuntimeUserControl = null;
            this.languageComponent.UserControlName = "";
            this.Load += new System.EventHandler(this.Page_Load);

            WebDataGrid gridWebGridTemp = null;
            this.listGrid = this.GetWebGrid();
            for (int i = 0; i < listGrid.Count; i++)
            {
                gridWebGridTemp = listGrid[i];
                if (listGrid[i].ID == "gridUnSelected")
                {
                    this.gridUnSelected = listGrid[i];
                }
                if (listGrid[i].ID == "gridSelected")
                {
                    this.gridSelected = listGrid[i];
                }
                gridWebGridTemp.EnableDataViewState = true;
                gridWebGridTemp.EnableViewState = true;
                gridWebGridTemp.EnableAjax = false;
                gridWebGridTemp.StyleSetName = "Office2007Blue";
                gridWebGridTemp.AutoGenerateColumns = false;
                this.languageComponent = this.GetLanguageComponent();
                this.components = new System.ComponentModel.Container();
            }
            this.Load += new System.EventHandler(this.Page_Load);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            this.InitWebGrid();
            if (this.gridSelected != null)
            {
                this.InitWebGrid2();
            }
            this.Page.ClientScript.RegisterClientScriptInclude("Jquery", this.VirtualHostRoot + "Scripts/jquery-1.9.1.js");

            //调整Grid高度和宽度初始化脚本
            if (!this.Page.ClientScript.IsStartupScriptRegistered("PageloadScripts"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "PageloadScripts", @"

        var parentWindow;
        $(function () {

                //针对嵌套iframe的特殊页面，向上寻找3次找到FStartPage
                parentWindow=window.parent.dialogArguments.Window.parent.window;
                if(parentWindow.location.pathname.indexOf('FStartPage.aspx')<0)
                {
                    parentWindow=parentWindow.parent.window;
                }
                if(parentWindow.location.pathname.indexOf('FStartPage.aspx')<0)
                {
                    parentWindow=parentWindow.parent.window;
                }
     
        var windowHeight = 590;

        //$(window).height();
        //            if (windowHeight < $(document).height()) {
        //                windowHeight = $(document).height();
        //            }            
        var windowWidth = 800;

            if(!parentWindow.BrowserType().ie)
            {
                windowWidth=780;
            }
            //$(window).width();
            //            if (windowWidth < $(document).width()) {
            //                windowWidth = $(document).width();
            //            }   



           var gridUnSelected = $('#gridUnSelected:visible');
           var gridSelected = $('#gridSelected:visible');

           var contentOtherHeight=0;

                $('form').find('table:first').children('tbody').children('tr:visible').each(function () {

                    if ($(this).find('#gridUnSelected').html() == null&&
                    $(this).find('#gridSelected').html() == null) {
                        contentOtherHeight += $(this).outerHeight();
                    }
                });
               
                $('#gridWidth').val(windowWidth-28);
             
             gridUnSelected.children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').css('overflow-y', 'auto');
   
            if(gridSelected.html()!=null){
                gridUnSelected.height((windowHeight-contentOtherHeight)/2-10);
                gridSelected.height((windowHeight-contentOtherHeight)/2-10);
                gridSelected.children('table').children('tbody').children('tr').children('td').children('table').children('tbody:eq(1)').children('tr').children('td').children('div').css('overflow-y', 'auto');
            }
            else
            {
                gridUnSelected.height(windowHeight-contentOtherHeight);
            }
            $('#gridHeigt').val(windowHeight-contentOtherHeight);
            

            if($('#txtSelected').html()!=null)
            {
                $('#txtSelected').val(window.parent.dialogArguments.Codes);
            }

            if($('#txtOthers').html()!=null)
            {
                $('#txtOthers').val(window.parent.dialogArguments.Others);
            }
            if($('#cmdInit').html()!=null)    
            {       
                try
                { 
                    $('#cmdInit').click();       
                }
                catch(e)
                {
                }
            }
            
        });   

            
        ", true
             );
            }

            //注册scriptManager的脚本
            if (!this.Page.ClientScript.IsStartupScriptRegistered("PageRequestManager"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "PageRequestManager", @" 

            Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequest);
            function initializeRequest(sender, args)
            {
             
              if ( (args.get_postBackElement().id == 'gridUnSelected'||args.get_postBackElement().id == 'gridSelected')&&$('#__EVENTARGUMENT').val()!='ColumnSorting') {
               
                 Sys.WebForms.PageRequestManager.getInstance().abortPostBack();
                 args.set_cancel(true);
              }
            
            }
    
              Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginFunc);
            function beginFunc(sender, args) {
                //显示等待
                if($find('gridUnSelected')!=null)
                    $find('gridUnSelected').get_ajaxIndicator().show($find('gridUnSelected'));
            }

           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
        
            //查询完毕，关闭等待
            if($find('gridUnSelected')!=null)          
                $find('gridUnSelected').get_ajaxIndicator().hide($find('gridUnSelected'));

         
            //截住页面Updatepanel中发生的所有错误，并使用主页面上自定义的DIV弹出提示
                if(args.get_error() != undefined && args.get_error().httpStatusCode == '500')  
                {
                    parentWindow.showErrorDialog(args.get_error().message.split('---')[0].replace(args.get_error().name+':',''),args.get_error().message.split('---')[1]);                               
                    args.set_errorHandled(true);
                }
            }", true);
            }
            ScriptManager.GetCurrent(this.Page).AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(BaseMPageNew_AsyncPostBackError);


            if (!IsPostBack)
            {

                this.InitPageLanguage(this.languageComponent, false);

                this.gridUnSelected.DataSource = this.DtSourceUnSelected;
                this.gridUnSelected.DataBind();
                if (gridSelected != null)
                {
                    this.gridSelected.DataSource = this.DtSourceSelected;
                    this.gridSelected.DataBind();
                }
                //this.gridWebGrid3.DataSource = this.DtSource3;
                //this.gridWebGrid3.DataBind();
            }
            else
            {
                if (Request.Params["__EVENTTARGET"].ToString() == "gridUnSelected" && Request.Params["__EVENTARGUMENT"].ToString() == "ColumnSorting")
                {

                    //Grid排序触发的页面回传需要重新加载数据
                    //this.gridUnSelectedHelper.RefreshData();
                    this.gridUnSelected.DataSource = this.DtSourceUnSelected;
                    this.gridUnSelected.DataBind();
                }
                else if (Request.Params["__EVENTTARGET"].ToString() == "gridSelected" && Request.Params["__EVENTARGUMENT"].ToString() == "ColumnSorting")
                {

                    //Grid排序触发的页面回传需要重新加载数据
                    //this.gridUnSelectedHelper.RefreshData();
                    this.gridSelected.DataSource = this.DtSourceSelected;
                    this.gridSelected.DataBind();
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

        protected virtual void Grid_ClickCellButton(GridRecord row, string command)
        {
            //需要子类自定义除了编辑按钮意外的其他按钮的点击事件
        }
        protected virtual void Grid_ClickCell(GridRecord row, string command)
        {
            //需要子类自定义除了编辑按钮意外的其他按钮的点击事件
        }


        #endregion

        #region WebGrid

        #endregion


        #region override

        #region Control
        /// <summary>
        /// 返回UltraWebGrid，如果Grid名称为gridUnSelected，不用重载
        /// </summary>
        /// <returns></returns>
        protected virtual List<WebDataGrid> GetWebGrid()
        {
            List<WebDataGrid> list = new List<WebDataGrid>();
            Control ctrl = this.FindControl("gridUnSelected");
            Control ctr2 = this.FindControl("gridSelected");
            if (ctrl != null)
            {
                list.Add((WebDataGrid)ctrl);
            }
            if (ctr2 != null && ctr2.Visible == true)
            {
                list.Add((WebDataGrid)ctr2);

            }

            return list;
        }

        /// <summary>
        /// 返回LanguageComponent，需重载
        /// </summary>
        /// <returns></returns>
        protected virtual ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent;
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
            this.gridUnSelectedHelper.ApplyLanguage(this.languageComponent);

            //添加系统主键
            this.gridUnSelectedHelper.AddDataColumn("GUID", "GUID", true);

            this.gridUnSelected.DataKeyFields = "GUID";
            this.DtSourceUnSelected.PrimaryKey = new DataColumn[] { DtSourceUnSelected.Columns["GUID"] };

            //调整高度和宽度
            if (Request.Params["gridHeigt"] != null && Request.Params["gridHeigt"].ToString() != string.Empty)
            {
                this.gridUnSelected.Height = Convert.ToInt32(Request.Params["gridHeigt"]);
            }

            if (Request.Params["gridWidth"] != null && Request.Params["gridWidth"].ToString() != string.Empty)
            {
                //根据页面大小计算默认宽度,解决列宽改变过大导致变形问题。
                if (this.gridUnSelected.DefaultColumnWidth.IsEmpty && this.gridUnSelected.Columns.Count > 1)
                {
                    int noWidthColumnCount = 0;//未设置列宽的列数
                    double boundWithTotal = 0; //已设置列宽的列宽综合
                    foreach (GridField col in this.gridUnSelected.Columns)
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
                        this.gridUnSelected.DefaultColumnWidth = new Unit((Convert.ToDouble(Request.Params["gridWidth"]) - boundWithTotal - 37) / noWidthColumnCount - 17, UnitType.Pixel);//37(rownum宽度35+其border宽度2)
                }
            }
        }
        protected virtual void InitWebGrid2()
        {
            this.gridSelectedHelper.ApplyLanguage(this.languageComponent);

            //添加系统主键
            this.gridSelectedHelper.AddDataColumn("GUID", "GUID", true);

            this.gridSelected.DataKeyFields = "GUID";
            this.DtSourceSelected.PrimaryKey = new DataColumn[] { DtSourceSelected.Columns["GUID"] };

            //调整高度和宽度
            if (Request.Params["gridHeigt"] != null && Request.Params["gridHeigt"].ToString() != string.Empty)
            {
                this.gridUnSelected.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 2 - 10;
                this.gridSelected.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 2 - 10;
            }

            if (Request.Params["gridWidth"] != null && Request.Params["gridWidth"].ToString() != string.Empty)
            {
                //根据页面大小计算默认宽度,解决列宽改变过大导致变形问题。
                if (this.gridSelected.DefaultColumnWidth.IsEmpty && this.gridSelected.Columns.Count > 1)
                {
                    int noWidthColumnCount = 0;//未设置列宽的列数
                    double boundWithTotal = 0; //已设置列宽的列宽综合
                    foreach (GridField col in this.gridSelected.Columns)
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
                    if (boundWithTotal < Convert.ToDouble(Request.Params["gridWidth"]) && noWidthColumnCount > 0)
                        this.gridSelected.DefaultColumnWidth = new Unit((Convert.ToDouble(Request.Params["gridWidth"]) - boundWithTotal - 37) / noWidthColumnCount - 17, UnitType.Pixel);//37(rownum宽度35+其border宽度2)
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
