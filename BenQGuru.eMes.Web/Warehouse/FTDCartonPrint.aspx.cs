using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;
//using BenQGuru.eMES.CodeSoftPrint;
using UserControl;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FTDCartonPrint : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;


        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;

        private WarehouseFacade _WarehouseFacade = null;
        private WarehouseFacade _TransferFacade;
        SystemSettingFacade _SystemSettingFacade = null;
        private InventoryFacade facade = null;
        private UserFacade _UserFacade = null;
        bool isVendor = false;//判断当前用户是否为供应商
       //     CodeSoftPrintFacade _codeSoftPrintFacade = null;
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
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
            UnLoadPageLoad();
        }
        #endregion

        #region Init

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (_UserFacade == null)
            {
                _UserFacade = new UserFacade(this.DataProvider);
            }
            isVendor = _UserFacade.IsVendor(this.GetUserCode());
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }


      
        #endregion

        #region WebGrid

        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("TDCartonNo", "鼎桥箱号", null);
            this.gridHelper.AddDefaultColumn(true, false);
            
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            //row["Check"] = "Checked";
            row["TDCartonNo"] = ((BarCode)obj).BarCodeNo;
            return row;
        }

        private string MoldidToSQL(string moldid)
        {
            if (moldid.Equals(string.Empty))
            {
                return "";
            }
            string sql = "";
            string[] str = moldid.Split(',');
            foreach (string s in str)
            {
                if (!s.Equals(string.Empty))
                {
                    sql += "'" + s + "',";
                }
            }

            return sql.Remove(sql.LastIndexOf(','));
        }
        protected virtual void cmdQuery_Click(object sender, System.EventArgs e)
        {
            this.gridHelper.RequestNewData();
            //if (this.gridHelper2 != null)
            //    this.gridHelper2.RequestNewData();
            this.buttonHelper.PageActionStatusHandle(PageActionType.Query);
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            return this._WarehouseFacade.QueryBarCode(
           FormatHelper.CleanString(this.txtCartonNoQurey.Text),
          this.txtBarCodeListQuery.Text,
           inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            return this._WarehouseFacade.QueryBarCodeCount(
                     FormatHelper.CleanString(this.txtCartonNoQurey.Text),
                    this.txtBarCodeListQuery.Text
                  );
        }

        #endregion

     

        #region Export

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                              ((BarCode)obj).BarCodeNo
                               };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[]
                {
                                    "TDCartonNo"
                };
        }

        #endregion


        #region Button
        //点击生成条码按钮
        protected void cmdCreateBarCode_ServerClick(object sender, System.EventArgs e)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            //点击生成条码按钮，根据数量输入框输入的数量生成箱号数，显示在Grid中并保存在TBLBARCODE表中
            //4.	鼎桥箱号编码规则：CT+年月日+九位流水码：如：CT20160131000000001，流水码不归零(流水码创建对应的Sequences累加)
            if (string.IsNullOrEmpty(txtQtyEdit.Text.Trim()))
            {
                WebInfoPublish.Publish(this, "数量不能为空", this.languageComponent1);
                return;
            }
            int qty= int.Parse(txtQtyEdit.Text.Trim());
            if (qty == 0)
            {
                WebInfoPublish.Publish(this, "数量只能输入大于0的数字", this.languageComponent1);
                return;
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int date=dbDateTime.DBDate;
            string barCodeList = "";
            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i <qty; i++)
                {
                    BarCode bar=new BarCode();
                    string serialNo = CreateSerialNo(date);
                    bar.BarCodeNo = "CT" + date + serialNo;
                    bar.Type = "CARTONNO";
                    bar.MCode = "";
                    bar.EnCode = "";
                    bar.SpanYear =date.ToString().Substring(0,4);
                    bar.SpanDate = date;
                    if (!string.IsNullOrEmpty(serialNo))
                    {
                        bar.SerialNo = int.Parse(serialNo);
                    }
                    bar.PrintTimes = 0;
                    bar.CUser = this.GetUserCode();	//	CUSER
                    bar.CDate = dbDateTime.DBDate;	//	CDATE
                    bar.CTime = dbDateTime.DBTime;//	CTIME
                    bar.MaintainDate = dbDateTime.DBDate;	//	MDATE
                    bar.MaintainTime = dbDateTime.DBTime;	//	MTIME
                    bar.MaintainUser = this.GetUserCode();		//	MUSER
                    _WarehouseFacade.AddBarCode(bar);
                    barCodeList += "'" + bar.BarCodeNo + "',";
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
            if (!string.IsNullOrEmpty(barCodeList))
            {
                if (string.IsNullOrEmpty(txtBarCodeListQuery.Text))
                {
                    txtBarCodeListQuery.Text = barCodeList.Substring(0, barCodeList.Length - 1);
                }
                else
                {
                    txtBarCodeListQuery.Text += ",";
                    txtBarCodeListQuery.Text += barCodeList.Substring(0, barCodeList.Length - 1);
                }
            }
            cmdQuery_Click(null,null);
        }

        //打印按钮
        protected void cmdPrint_ServerClick(object sender, System.EventArgs e)
        {
            //GetServerClick("Print");
            Print();
            txtBarCodeListQuery.Text = "";
        }

        #endregion

        private void Print()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            if (this.gridWebGrid.Rows.Count <= 0)
            {
                return;
            }
            try
            {
                this.DataProvider.BeginTransaction();

                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    string barno = this.gridWebGrid.Rows[i].Items.FindItemByKey("TDCartonNo").Value.ToString();
                    BarCode bar = (BarCode)_WarehouseFacade.GetBarCode(barno);
                    bar.PrintTimes = bar.PrintTimes + 1;
                    _WarehouseFacade.UpdateBarCode(bar);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }



        #region GetServerClick
        private void GetServerClick(string clickName)
        {
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
                object[] asnList = ((BarCode[])objList.ToArray(typeof(BarCode)));
                if (clickName == "Print")
                {
                    this.InitialObjects(asnList);
                }
                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

      


        #endregion

        protected void InitialObjects(object[] barList)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (BarCode bar in barList)
                {
                    bar.PrintTimes = bar.PrintTimes + 1;
                    _WarehouseFacade.UpdateBarCode(bar);
                }
               // Page.RegisterClientScriptBlock("Print", "<script>Print()</script>");
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.PublishInfo(this, ex.Message, this.languageComponent1);
            }
        }



        protected override object GetEditObject(GridRecord row)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(base.DataProvider);
            }
            object obj = _WarehouseFacade.GetBarCode(row.Items.FindItemByKey("TDCartonNo").Text);

            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        #region CreateSerialNo
        private string CreateSerialNo(int stno)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (_TransferFacade == null)
            {
                _TransferFacade = new WarehouseFacade(this.DataProvider);
            }
            string maxserial = _TransferFacade.GetMaxSerial("CT"+ stno);

            //如果已是最大值就返回为空
            if (maxserial == "999999999")
            {
                return "";
            }

            SERIALBOOK serialbook = new SERIALBOOK();
            if (maxserial == "")
            {
                serialbook.SNPrefix = "CT" + stno;
                serialbook.MaxSerial = "1";
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.AddSerialBook(serialbook);
                return string.Format("{0:000000000}", int.Parse(serialbook.MaxSerial));
            }
            else
            {
                serialbook.SNPrefix = "CT" + stno;
                serialbook.MaxSerial = (int.Parse(maxserial) + 1).ToString();
                serialbook.MDate = dbDateTime.DBDate;
                serialbook.MTime = dbDateTime.DBTime;
                serialbook.MUser = this.GetUserCode();
                _TransferFacade.UpdateSerialBook(serialbook);
                return string.Format("{0:000000000}", int.Parse(serialbook.MaxSerial));
            }
        }

        #endregion

        #region 打印
        //private void PrintRcardList()
        //{
        //    #region 【参数1】打印机
        //    //【参数1】打印机
        //    string printer = ucLabelComPrint.SelectedItemValue.ToString();
        //    #endregion

        //    #region 【参数2】打印模板路径
        //    //模板名称
        //    string templateName = ucLabelComPrintTemplate.SelectedItemText;

        //    //打印模板对象
        //    PrintTemplate template = _printTemplateFacade.GetPrintTemplate(templateName) as PrintTemplate;

        //    //【参数2】打印模板路径
        //    string printTemplatePath = string.Empty;
        //    if (template != null)
        //    {
        //        printTemplatePath = template.TemplatePath;
        //    }
        //    #endregion

        //    #region 【参数3】要打印的值
        //    //【参数3】要打印的值
        //    List<StringDictionary> printValues = GetStringDictionaryList();
        //    #endregion

        //    if (_codeSoftPrintFacade == null)
        //        _codeSoftPrintFacade = new CodeSoftPrintFacade(DataProvider);

        //    //获取打印执行后的结果
        //    Messages msg = _codeSoftPrintFacade.Print(printer, printTemplatePath, printValues);

        //    _codeSoftPrintFacade = null;

        //    /**************************************************************
        //     * TODO:打印完成
        //     * 更新TBLCARTONINFO里的打印次数，打印人和日期时间
        //     **************************************************************/
        //    string cartonNo = ucLabelPalletNo.Value.Trim();//栈板号
        //    string userCode = ApplicationService.Current().UserCode;
        //    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

        //    if (msg.IsSuccess())
        //    {
        //        try
        //        {
        //            #region 更新CARTONINFO表
        //            //CARTONINFO cartonInfo = _packageFacade.GetCARTONINFO(cartonNo) as CARTONINFO;
        //            //if (cartonInfo != null)
        //            //{
        //            //    //打印次数加1
        //            //    cartonInfo.PrintCount = cartonInfo.PrintCount + 1;
        //            //    cartonInfo.MUSER = userCode;
        //            //    cartonInfo.MDATE = dbDateTime.DBDate;
        //            //    cartonInfo.MTIME = dbDateTime.DBTime;
        //            //}

        //            //更新
        //           // _packageFacade.UpdateCARTONINFO(cartonInfo);
        //            #endregion
        //        }
        //        catch (Exception ex)
        //        {
        //          //  ShowMessage(new UserControl.Message(MessageType.Error, ex.Message));
        //            return;
        //        }
        //    }

        // //   ApplicationRun.GetInfoForm().Add(msg);
        //}

        #endregion


     
    }
}
