#region System
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
#endregion

#region eMes
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Web.WebQuery
{
    /// <summary>
    /// 将SPC测试数据传回给ActiveX控件。
    /// </summary>
    public partial class GetSPCData : System.Web.UI.Page
    {
        private string _itemCode;
        private string _dateFrom;
        private string _dateTo;
        private ArrayList _resList;//如果查询条件中包含Resource,则来源于查询条件,否则来源于查询结果
        private ArrayList _dateList;
        private string _resourceCode;
        private string _testName;
        private string _groupSeq;
        private string _fromTime;
        private string _tableName;
        private int _seq;
        private Item2SPCTest _test;
        private bool _ifResource = false;//查询条件中是否包含Resource
        private string _testResult;

        private BenQGuru.eMES.WebQuery.QuerySPC _query = null; // new BenQGuru.eMES.WebQuery.QuerySPC();
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private BenQGuru.eMES.MOModel.ItemFacade _itemfacade = null; // new BenQGuru.eMES.MOModel.ItemFacade();
        private BenQGuru.eMES.Common.Domain.IDomainDataProvider _provider;
        private BenQGuru.eMES.MOModel.SPCFacade spcFacade = null;
        private BenQGuru.eMES.Domain.SPC.SPCItemSpec itemSpec = null;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                try
                {
                    _provider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
                    _query = new BenQGuru.eMES.WebQuery.QuerySPC(this._provider);
                    _itemfacade = new ItemFacade(this._provider);

                    GetParam();

                    #region 返回基本资料部分
                    if (_fromTime == null || _fromTime == string.Empty)//第一次查询,不是刷新
                    {
                        /*产品别
                        品名
                        日期								(YYYY-MM-DD)
                        资源列表 							(以逗号分割，资源代码里的逗号用空格代替)
                        测试项
                        USL,LSL,UCL,LCL,是否自动产生
                        */
                        ModelFacade modelfacade = new FacadeFactory(_provider).CreateModelFacade();
                        Model model = (Model)modelfacade.GetModelByItemCode(_itemCode);
                        this.Writeln(model != null ? model.ModelCode : "");

                        Item item = (Item)_itemfacade.GetItem(_itemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                        this.Writeln(item != null ? item.ItemName : "");

                        //this.Writeln(_dateFrom);
                        WriteDateList();

                        WriteResList();

                        this.Writeln(this._testName);

                        string strtest = string.Empty;

                        strtest = itemSpec.USL.ToString() + ","
                            + itemSpec.LSL.ToString() + ","
                            + itemSpec.UCL.ToString() + ","
                            + itemSpec.LCL.ToString() + ","
                            + itemSpec.AutoCL + ","
                            + itemSpec.LimitUpOnly + ","
                            + itemSpec.LimitLowOnly;

                        this.Writeln(strtest);
                    }
                    else //刷新
                    {
                        WriteDateList();
                        //资源列表
                        WriteResList();
                    }
                    #endregion

                    #region 测试数据部分
                    //总数								(以下记录数的总和)
                    //HHMMSS数值,工作站					(时间和数值间没有空格)

                    //从SQL Server中查询数据
                    if (_query == null)
                    {
                        _query = new BenQGuru.eMES.WebQuery.QuerySPC(this._provider);
                    }



                    BenQGuru.eMES.SPCDataCenter.DataHandler dataHandler = new BenQGuru.eMES.SPCDataCenter.DataHandler(this._provider);
                    int iFromTime = 0;
                    if (this._fromTime != null && this._fromTime != string.Empty)
                        iFromTime = int.Parse(this._fromTime);
                    string[][] spcData = dataHandler.QuerySPCData(this._itemCode, this._testName, int.Parse(this._groupSeq), this._resourceCode, FormatHelper.TODateInt(this._dateFrom), FormatHelper.TODateInt(this._dateTo), _testResult, iFromTime);
                    if ((_fromTime == null || _fromTime == string.Empty) && spcData.Length < 10)
                    {
                        ExceptionManager.Raise(this.GetType(), "$SPC_SamplePoint_Too_Little");
                    }
                    // 记录数
                    this.Writeln(spcData.Length);
                    for (int i = 0; i < spcData.Length; i++)
                    {
                        string strLine = string.Empty;
                        strLine = this._dateList.IndexOf(spcData[i][1]) + "," + FormatHelper.ToTimeString(int.Parse(spcData[i][2])).Replace(":", "") + spcData[i][3];
                        if (this._resList.Count > 1)
                            strLine += "," + this._resList.IndexOf(spcData[i][0]);
                        this.Writeln(strLine);
                    }

                    #endregion

                    Response.End();

                    if (!IsPostBack)
                    {
                        // 初始化页面语言
                        //this.InitPageLanguage(this.languageComponent1, false);
                    }
                }
                catch (System.Threading.ThreadAbortException)
                {
                }
                catch (System.Exception ex)
                {
                    Response.Clear();
                    string msg = MessageCenter.ParserMessage(ex.Message, this.languageComponent1);
                    if (ex.InnerException != null)
                        msg = msg + " " + ex.InnerException.Message;
                    msg = msg.Replace("\r", "");
                    msg = msg.Replace("\n", "");
                    Response.Write(msg);
                    try
                    {
                        Response.End();
                    }
                    catch (System.Threading.ThreadAbortException)
                    {
                    }
                }
            }
			finally //关闭数据库连接
            {
                if (_query != null && this._query.SPCBroker != null)
                    this._query.SPCBroker.CloseConnection();

                if (_provider != null)
                    ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)_provider).PersistBroker.CloseConnection();
            }
        }

        private void Writeln(object obj)
        {
            Response.Write(obj == null ? "" : obj.ToString());
            Response.Write("\r\n");
        }

        private void WriteResList()
        {
            //当查询条件中没有Resource时,要自己查出来
            if (_resList.Count == 0)
            {

                int iFromTime = 0;
                if (this._fromTime != null && this._fromTime != string.Empty)
                    iFromTime = int.Parse(this._fromTime);
                BenQGuru.eMES.SPCDataCenter.DataHandler dataHandler = new BenQGuru.eMES.SPCDataCenter.DataHandler(this._provider);
                string[] arrRes = dataHandler.QuerySPCDataResource(this._itemCode, this._testName, int.Parse(this._groupSeq), this._resourceCode, FormatHelper.TODateInt(this._dateFrom), FormatHelper.TODateInt(this._dateTo), _testResult, iFromTime);
                for (int i = 0; i < arrRes.Length; i++)
                    _resList.Add(arrRes[i]);
            }

            string[] arr = new string[_resList.Count];

            for (int i = 0; i < _resList.Count; i++)
                arr[i] = ((string)_resList[i]).Replace(",", " ");

            string res = string.Join(",", arr);
            this.Writeln(res);
        }

        private void WriteDateList()
        {
            this.Writeln(_dateFrom);
        }

        private string GetParamValueFromUrl(string paramName)
        {
            string url = Request.Url.ToString();
            int startindex = url.IndexOf(paramName) + paramName.Length + 1;
            int endindex = url.Length;
            for (int i = startindex; i < url.Length; i++)
            {
                if (url[i] == '&')
                {
                    endindex = i;
                    break;
                }
            }

            return url.Substring(startindex, endindex - startindex);

        }
        /// <summary>
        /// 将通过URL传的参数分解出来
        /// </summary>
        private void GetParam()
        {
            _itemCode = Request.QueryString["itemcode"];

            //如果其中有空格，则重新处理一下
            if (_itemCode.IndexOf(" ") > 0)
            {
                _itemCode = GetParamValueFromUrl("itemcode").Trim();
            }
            _itemCode = _itemCode.ToUpper();

            _dateFrom = Request.QueryString["datefrom"];
            _dateTo = Request.QueryString["dateto"];
            _dateList = new ArrayList();
            if (_dateTo == string.Empty || _dateFrom == _dateTo)
                _dateList.Add(FormatHelper.TODateInt(_dateFrom).ToString());
            else
            {
                DateTime dtFrom = DateTime.Parse(_dateFrom);
                DateTime dtTo = DateTime.Parse(_dateTo);
                DateTime dtTmp = dtFrom;
                string strDate = string.Empty;
                while (dtTmp <= dtTo)
                {
                    _dateList.Add(FormatHelper.TODateInt(dtTmp).ToString());
                    dtTmp = dtTmp.AddDays(1);
                }
            }
            string res = Request.QueryString["resource"];
            //如果其中有空格，则重新处理一下
            if (res.IndexOf(" ") > 0)
            {
                res = this.GetParamValueFromUrl("resource").Trim();
            }
            _resourceCode = res;

            _resList = new ArrayList();
            if (res != null && res != String.Empty)
            {
                string[] res_arr = res.Split(',');

                for (int i = 0; i < res_arr.Length; i++)
                {
                    _resList.Add(res_arr[i]);
                }
            }
            if (_resList.Count > 0)
                _ifResource = true;

            _testName = Request.QueryString["testitem"];
            _groupSeq = Request.QueryString["condition"];
            if (_testName.IndexOf(" ") > 0)
            {
                _testName = this.GetParamValueFromUrl("testitem").Trim();
            }
            _fromTime = Request.QueryString["fromtime"];
            _testResult = Request.QueryString["testresult"];

            //对该管控项目代码是否维护存储信息
            if (spcFacade == null)
                spcFacade = new BenQGuru.eMES.MOModel.SPCFacade(this._provider);

            object[] objSpcObjectStore = spcFacade.GetSPCObjectStore(this._testName, decimal.Parse(this._groupSeq));
            if (objSpcObjectStore == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_SPC_No_ObjectStore", string.Empty);//此产品的SPC测试项没有维护		
            }

            // 查询测试规格

            itemSpec = (BenQGuru.eMES.Domain.SPC.SPCItemSpec)spcFacade.GetSPCItemSpec(this._itemCode, decimal.Parse(this._groupSeq), this._testName);
            if (itemSpec == null)
                ExceptionManager.Raise(this.GetType(), "$Error_SPC_No_TestItem", string.Empty);//此产品的SPC测试项没有维护		
            // Added end

        }

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "\\\\..";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion
    }
}
