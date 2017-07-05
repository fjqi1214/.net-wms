using System;
using System.Web.UI;
using System.Collections;

using Infragistics.Web.UI.GridControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Infragistics.Web.UI;
using Infragistics.Web.UI.LayoutControls;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// BaseMPage 的摘要说明。
    /// </summary>
    public class BaseMPageMinus : BasePage
    {
        public BaseMPageMinus()
            : base()
        {
        }

        protected GridHelperNew gridHelper = null;
        protected WebDataGrid gridWebGrid;

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        protected System.ComponentModel.IContainer components;

        protected GridHelperNew gridHelper2 = null;
        protected GridHelperNew gridHelper3 = null;

        protected WebDataGrid gridWebGrid2;
        protected WebDataGrid gridWebGrid3;
        protected List<WebDataGrid> listGrid;

        protected string gridWebGridId = "gridWebGrid";
        protected string gridWebGrid2Id = "gridWebGrid2";
        protected string gridWebGrid3Id = "gridWebGrid3";

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

        /// <summary>
        /// 是否包上updatepanel
        /// </summary>
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

        //是否需要竖直滚动条,对于页面内容较多的需要打开,比如QPage
        public bool needVScroll
        {
            get
            {
                if (this.ViewState["$needVScroll"] == null)
                {
                    this.ViewState["$needVScroll"] = false;
                }

                return Convert.ToBoolean(this.ViewState["$needVScroll"]);
            }
            set
            {
                this.ViewState["$needVScroll"] = value;
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
                sc1.AsyncPostBackTimeout = 1000;
                form1.Controls.AddAt(0, sc1);

                UpdatePanel up1 = new UpdatePanel();
                if (needUpdatePanel)
                {

                    up1.ID = "up1";
                    foreach (Control formChildren in form1.Controls)
                    {
                        if (formChildren is HtmlContainerControl)
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
                HtmlGenericControl btnPostBack = new HtmlGenericControl("input");
                btnPostBack.ID = "btnPostBack";
                btnPostBack.Attributes.Add("type", "submit");
                btnPostBack.Style.Add("display", "none");
                if (needUpdatePanel)
                {
                    up1.ContentTemplateContainer.Controls.Add(btnPostBack);
                }
                else
                {
                    form1.Controls.Add(btnPostBack);
                }


                HtmlGenericControl jqueryuiCss = new HtmlGenericControl("link");
                jqueryuiCss.Attributes.Add("href", this.VirtualHostRoot+"Skin/jquery-ui.css");
                jqueryuiCss.Attributes.Add("rel", "stylesheet");
                jqueryuiCss.Attributes.Add("type", "text/css");
                form1.Controls.Add(jqueryuiCss);

                //保存选中行的GUID集合
                HiddenField hdnSelectRowGUIDS1 = new HiddenField();
                hdnSelectRowGUIDS1.ID = "hdnSelectRowGUIDS_" + gridWebGridId;
                HiddenField hdnSelectRowGUIDS2 = new HiddenField();
                hdnSelectRowGUIDS2.ID = "hdnSelectRowGUIDS_" + gridWebGrid2Id;
                HiddenField hdnSelectRowGUIDS3 = new HiddenField();
                hdnSelectRowGUIDS3.ID = "hdnSelectRowGUIDS_" + gridWebGrid3Id;

                if (needUpdatePanel)
                {
                    up1.ContentTemplateContainer.Controls.Add(hdnSelectRowGUIDS1);
                    up1.ContentTemplateContainer.Controls.Add(hdnSelectRowGUIDS2);
                    up1.ContentTemplateContainer.Controls.Add(hdnSelectRowGUIDS3);
                }
                else
                {
                    form1.Controls.Add(hdnSelectRowGUIDS1);
                    form1.Controls.Add(hdnSelectRowGUIDS2);
                    form1.Controls.Add(hdnSelectRowGUIDS3);
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

                if (listGrid[i].ID == gridWebGridId)
                {
                    this.gridWebGrid = listGrid[i];
                }
                if (listGrid[i].ID == gridWebGrid2Id)
                {
                    this.gridWebGrid2 = listGrid[i];
                }
                if (listGrid[i].ID == gridWebGrid3Id)
                {
                    this.gridWebGrid3 = listGrid[i];
                }

                //this.gridWebGrid.ItemCommand += new ItemCommandEventHandler(gridWebGrid_ItemCommand);
                gridWebGridTemp.EnableDataViewState = true;
                gridWebGridTemp.EnableViewState = true;
                gridWebGridTemp.EnableAjax = false;
 
                gridWebGridTemp.StyleSetName = "Office2007Blue";
                //this.gridWebGrid.BorderStyle = BorderStyle.Solid;
                gridWebGridTemp.AutoGenerateColumns = false;
                gridWebGridTemp.ClientEvents.Click = "Gird_ClientClick";
                gridWebGridTemp.ClientEvents.Initialize = "Grid_Initialize";

                gridWebGridTemp.Height = new Unit(300);

            }

            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = this.GetLanguageComponent();
            if (this.languageComponent1 == null)
            {
                this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
                this.languageComponent1.Language = "CHS";
                //this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
                //this.languageComponent1.RuntimePage = null;
                //this.languageComponent1.RuntimeUserControl = null;
                this.languageComponent1.UserControlName = "";
            }
            
            this.Load += new System.EventHandler(this.Page_Load);
        }
        /// <summary>
        /// 返回LanguageComponent，需重载
        /// </summary>
        /// <returns></returns>
        protected virtual ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return null;
        }

        public void UnLoadPageLoad()
        {
            this.Load -= new System.EventHandler(this.Page_Load);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            string strScript = string.Empty;
            if(!needVScroll)
            {
                strScript = "$('html').css('overflow','hidden');";
            }


            //取消注释 签入  modify by jinger 2016-3-2
            this.Page.ClientScript.RegisterClientScriptInclude("Jquery", this.VirtualHostRoot + "Scripts/jquery-1.9.1.js");
            //end modify

            this.Page.ClientScript.RegisterClientScriptInclude("JqueryUI", this.VirtualHostRoot + "Scripts/jquery-ui.js");
            this.Page.ClientScript.RegisterClientScriptInclude("JqueryUIi18n", this.VirtualHostRoot + "Scripts/jquery-ui-i18n.js");

            //日期控件多语言
            string strDatepickLangType=string.Empty;
            switch(this.languageComponent1.Language)
            {
                case "CHS":
                    strDatepickLangType = "zh-CN";
                    break;
                case "CHT":
                    strDatepickLangType = "zh-TW";
                    break;
                case "ENU":
                    break;
                default:
                    break;
            }
            string strDatepickerScript= @"  
                if($('.datepicker').html()!=null)
                {
                    //JqueryUi 日期控件
                    $('.datepicker').datepicker({
                    showOn: 'both',
                    buttonImage: '" + this.VirtualHostRoot + @"skin/images/calendar.gif',
                    buttonImageOnly: true,
                    changeMonth: true,
                    changeYear: true,
                    showButtonPanel: true,
                    dateFormat: 'yy-mm-dd',
                    buttonText: 'Choose Date',
                    //closeText: 'Close',
                    constrainInput: true,
                    beforeShow: function (input, inst) {
                        $(this).select();
                        //只读或者不可用的时候，不弹出
                        if ($(this).attr('disabled') == 'disabled' || $(this).attr('readOnly') == 'readOnly') {
                            return false;
                        }
                     }
                    }).datepicker('option', $.datepicker.regional['" + strDatepickLangType+ @"']);

                    $('.datepicker').addClass('textbox');
                  

                }";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "strDatepickerScript", strDatepickerScript, true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strDatepickerScript", strDatepickerScript, true);

            string strTreeScript = " $('.tree').height($(window).height() - 27);";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "strTreeScript", strTreeScript, true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "strTreeScript", strTreeScript, true);

            if (!this.Page.ClientScript.IsStartupScriptRegistered("PageloadScripts"))
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "PageloadScripts", @"<script>
          
            var contentOtherHeight; //子页面中除了Grid所在tr其他tr的总高度
            var minHeight;

               //计算页面上除了Grid以外的高度总和
            function SetContentOtherHeight(obj) {

                if (obj.closest('tbody').html() == null)
                    return;
                else {
                    obj.closest('tbody').children('tr:visible').each(function () {

                        if ($(this).find('#gridWebGrid').html() == null &&
                        $(this).find('#gridWebGrid2').html() == null &&
                        $(this).find('#gridWebGrid3').html() == null) {
                            contentOtherHeight += $(this).outerHeight();
                        }
                    });
                    SetContentOtherHeight(obj.closest('tbody').closest('table'));
                }
            }
            window.onresize = function () {
                setGridHeight();
            }

            //设置grid的初始高度
            function setGridHeight()
            { 
                 var grid = $('#gridWebGrid');                             
                 if (grid.html() != null) {

                        var pageHeight=$(window).height();
                       
                        //记录呈现时Grid的宽度
                        if(contentOtherHeight==null)
                        {
                            contentOtherHeight = 0;
                            SetContentOtherHeight(grid);
                        }
                       
                        var grid2 = $('#gridWebGrid2');
                        var grid3 = $('#gridWebGrid3');
                        var gridHeight = pageHeight - contentOtherHeight - 24;
                        
                        //iframe高度改变之后，调整其中Grid的高度
                        if (grid3.html() != null) {
                            if(typeof(minHeight)=='number'&&(gridHeight / 3 - 5)<minHeight)
                            {
                                gridHeight=(minHeight+5)*3;
                            }
                            grid.height(gridHeight / 3 - 5);
                            grid2.height(gridHeight / 3 - 5);
                            grid3.height(gridHeight / 3 - 5);
                        }
                        else if (grid2.html() != null) {
                            if(typeof(minHeight)=='number'&&(gridHeight / 2 - 5)<minHeight)
                            {
                                gridHeight=(minHeight+5)*2;
                            }
                            grid.height(gridHeight / 2 - 5);
                            grid2.height(gridHeight / 2 - 5);

                        }
                        else if (grid.html() != null)
                        {
                            if(typeof(minHeight)=='number'&&gridHeight<minHeight)
                            {
                                gridHeight=minHeight;
                            }
                            grid.height(gridHeight);
                        }

                        $('#gridHeigt').val(gridHeight);

                    }
            }
            $(function () {
                      
             setGridHeight();   

                //调整下方按钮居中，多浏览器
              $('table[class=\'toolBar\']').wrap('<center></center>');     

       
                $(window).resize(function () {
                    $('.tree').height($(window).height() - 27);
                });       
                " + strScript + strDatepickerScript + @"
        });

   
         </script>
        ");
            }

            if (needUpdatePanel)
            {
                //注册scriptManager的脚本
                if (!this.Page.ClientScript.IsStartupScriptRegistered("PageRequestManager"))
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "PageRequestManager", @" 
              Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequest);
            function initializeRequest(sender, args)
            {
              //阻止Grid勾选列回传
              if ((args.get_postBackElement().id =='" + gridWebGridId + @"'||args.get_postBackElement().id =='" + gridWebGrid2Id + @"'||args.get_postBackElement().id =='" + gridWebGrid3Id + @"')&&$('#__EVENTARGUMENT').val()!='ColumnSorting') {
                
                 Sys.WebForms.PageRequestManager.getInstance().abortPostBack();
                 args.set_cancel(true);
              }
            
            }      

            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginFunc);
            function beginFunc(sender, args) {
                //显示等待
                if($find('" + gridWebGridId + @"')!=null)
                    $find('" + gridWebGridId + @"').get_ajaxIndicator().show($find('" + gridWebGridId + @"'));
            }

         

           Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
        
            //查询完毕，关闭等待
            if($find('" + gridWebGridId + @"')!=null)           
                $find('" + gridWebGridId + @"').get_ajaxIndicator().hide($find('" + gridWebGridId + @"'));

            //调整下方按钮居中，多浏览器
            $('table[class=\'toolBar\']').wrap('<center></center>');

            //截住页面Updatepanel中发生的所有错误，并使用主页面上自定义的DIV弹出提示
                if(args.get_error() != undefined && args.get_error().httpStatusCode == '500')  
                {     
                   try
                   {         
                    window.top.showErrorDialog(args.get_error().message.split('---')[0].replace(args.get_error().name+':',''),args.get_error().message.split('---')[1]);
                   }
                    catch(e)
                    {
                        alert(args.get_error().message.split('---')[0].replace(args.get_error().name+':','')+args.get_error().message.split('---')[1]);
                    }                          
                    args.set_errorHandled(true);
                }
            }", true);
                }
                ScriptManager.GetCurrent(this.Page).AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(BaseMPageNew_AsyncPostBackError);
            }

            if (!IsPostBack)
            {
                for (int i = 0; i < listGrid.Count; i++)
                {
                    if (listGrid[i].ID == gridWebGridId)
                    {
                        this.InitWebGrid();
                        this.gridWebGrid.DataSource = this.DtSource;
                        this.gridWebGrid.DataBind();
                    }
                    if (listGrid[i].ID == gridWebGrid2Id)
                    {
                        this.InitWebGrid2();
                        this.gridWebGrid2.DataSource = this.DtSource2;
                        this.gridWebGrid2.DataBind();
                    }
                    if (listGrid[i].ID == gridWebGrid3Id)
                    {
                        this.InitWebGrid3();
                        this.gridWebGrid3.DataSource = this.DtSource3;
                        this.gridWebGrid3.DataBind();
                    }
                }

                string strScript1 = string.Format("  GetSelectRowGUIDS('{0}');GetSelectRowGUIDS('{1}');GetSelectRowGUIDS('{2}');", this.gridWebGridId, this.gridWebGrid2Id, this.gridWebGrid3Id);
                if (!(this is BasePageSelectNew))
                {
                    foreach (string strBtnID in this.getButtons())
                    {

                        if (this.FindControl(strBtnID) != null && this.FindControl(strBtnID) is HtmlInputButton)
                        {
                            //如果原来有onclick属性，需要保留
                         string  strScript2 = (this.FindControl(strBtnID) as HtmlInputButton).Attributes["onclick"] == null ? strScript1 : strScript1 + (this.FindControl(strBtnID) as HtmlInputButton).Attributes["onclick"].ToString();
                         (this.FindControl(strBtnID) as HtmlInputButton).Attributes.Add("onclick", strScript2);
                        }
                    }
                }
                if (this.FindControl("cmdDelete") != null && this.FindControl("cmdDelete") is HtmlInputButton)
                {
                    string strScript2 = (this.FindControl("cmdDelete") as HtmlInputButton).Attributes["onclick"] == null ? strScript1 : strScript1 + (this.FindControl("cmdDelete") as HtmlInputButton).Attributes["onclick"].ToString();
                    (this.FindControl("cmdDelete") as HtmlInputButton).Attributes.Add("onclick", strScript2);
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

                    if ((commandName == "PackingDetail" || commandName == "btnRecordNG2" || commandName == "DocName2") && this.gridWebGrid2 != null)
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

                SetGridHeightAndWidth(false);
            }

            
     

        }

        /// <summary>
        /// 调整高度和宽度
        /// </summary>
        /// <param name="canReset">默认宽度是否可以重置</param>
        protected virtual void SetGridHeightAndWidth(bool canReset)
        {
            if (Request.Params["gridHeigt"] != null && Request.Params["gridHeigt"].ToString() != string.Empty && Request.Params["gridHeigt"].ToString() != "NaN" && Convert.ToInt32(Request.Params["gridHeigt"].ToString())>=0)
            {
                if (this.listGrid.Count == 1)
                {
                    this.gridWebGrid.Height = Convert.ToInt32(Request.Params["gridHeigt"]);
                }
                else if (this.listGrid.Count == 2 && this.gridWebGrid2 != null)
                {
                    this.gridWebGrid.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 2 - 5;
                    this.gridWebGrid2.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 2 - 5;
                }
                else if (this.listGrid.Count == 3 && this.gridWebGrid2 != null && this.gridWebGrid3 != null)
                {
                    this.gridWebGrid.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 3 - 5;
                    this.gridWebGrid2.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 3 - 5;
                    this.gridWebGrid3.Height = Convert.ToInt32(Request.Params["gridHeigt"]) / 3 - 5;
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

            errMsg = MessageCenter.ParserMessage(errMsg, this.languageComponent1).Replace("@", Environment.NewLine);
            innerErrMsg = FormatHelper.CleanString(MessageCenter.ParserMessage(innerErrMsg, this.languageComponent1)).Replace("@", Environment.NewLine);


            ScriptManager.GetCurrent(this.Page).AsyncPostBackErrorMessage = jsEncoder(errMsg) + "---" + jsEncoder(innerErrMsg);
        }


        protected virtual void gridWebGrid_ItemCommand(GridRecord row, string commandName)
        {
            //未修改的页面
            Grid_ClickCellButton(row, commandName);
            //新的页面
            Grid_ClickCell(row, commandName);

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



        /// <summary>
        /// 返回UltraWebGrid，如果一个Grid名称为gridWebGrid，不用重载
        /// </summary>
        /// <returns></returns>
        protected virtual List<WebDataGrid> GetWebGrid()
        {
            List<WebDataGrid> list = new List<WebDataGrid>();
            Control ctrl = this.FindControl(gridWebGridId);
            Control ctr2 = this.FindControl(gridWebGrid2Id);
            Control ctr3 = this.FindControl(gridWebGrid3Id);
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
        /// 初始化WebGrid，需重载，并在函数最后调用base.InitWebGrid();
        /// </summary>
        protected virtual void InitWebGrid()
        {
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            
            //添加系统主键
            this.gridHelper.AddDataColumn("GUID", "GUID", true);

            this.gridWebGrid.DataKeyFields = "GUID";
            this.DtSource.PrimaryKey = new DataColumn[] { DtSource.Columns["GUID"] };
            gridWebGrid.Behaviors.CreateBehavior<ColumnResizing>().ColumnResizingClientEvents.ColumnResizing = "ColumnResizing";
            gridWebGrid.Behaviors.CreateBehavior<Sorting>().SortingClientEvents.ColumnSorting = "ColumnSorting";
            gridWebGrid.Behaviors.CreateBehavior<EditingCore>().EditingClientEvents.CellValueChanged = "CellValueChanged";

        }
        protected virtual void InitWebGrid2()
        {
            if (this.gridWebGrid2 != null)
            {
                this.gridHelper2.ApplyLanguage(this.languageComponent1);

                //添加系统主键
                this.gridHelper2.AddDataColumn("GUID", "GUID", true);

                this.gridWebGrid2.DataKeyFields = "GUID";
                this.DtSource2.PrimaryKey = new DataColumn[] { DtSource2.Columns["GUID"] };

                gridWebGrid2.Behaviors.CreateBehavior<ColumnResizing>().ColumnResizingClientEvents.ColumnResizing = "ColumnResizing";
                gridWebGrid2.Behaviors.CreateBehavior<Sorting>().SortingClientEvents.ColumnSorting = "ColumnSorting";
                gridWebGrid2.Behaviors.CreateBehavior<EditingCore>().EditingClientEvents.CellValueChanged = "CellValueChanged";
            }
        }
        protected virtual void InitWebGrid3()
        {
            if (this.gridWebGrid3 != null)
            {
                this.gridHelper3.ApplyLanguage(this.languageComponent1);

                //添加系统主键
                this.gridHelper3.AddDataColumn("GUID", "GUID", true);

                this.gridWebGrid3.DataKeyFields = "GUID";
                this.DtSource3.PrimaryKey = new DataColumn[] { DtSource3.Columns["GUID"] };
                gridWebGrid3.Behaviors.CreateBehavior<ColumnResizing>().ColumnResizingClientEvents.ColumnResizing = "ColumnResizing";
                gridWebGrid3.Behaviors.CreateBehavior<Sorting>().SortingClientEvents.ColumnSorting = "ColumnSorting";
                gridWebGrid3.Behaviors.CreateBehavior<EditingCore>().EditingClientEvents.CellValueChanged = "CellValueChanged";
            }
        }

    }
}
