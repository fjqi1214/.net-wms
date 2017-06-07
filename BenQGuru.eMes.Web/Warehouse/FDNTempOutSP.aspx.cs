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
using System.Collections.Generic;

using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Delivery;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FDNTempOutSP : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private InventoryFacade _facade = null;

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

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            InitOnPostBack();
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.InitWebGrid();
                this.GetPageValues();
                this.RequestData();
            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void RequestData()
        {
            this.pagerToolBar.PageSize = this.pagerSizeSelector.PageSize;
            this.gridHelper.GridBind(PageGridBunding.Page, this.pagerSizeSelector.PageSize);
            this.pagerToolBar.RowCount = GetRowCount();
            this.pagerToolBar.InitPager();
            this.txtDNNOOutEdit.Text = string.Empty;
            this.txtLineNOEdit.Text = string.Empty;
            this.txtTempOutQtyEdit.Text = string.Empty;
        }

        private void GetPageValues()
        {
            this.txtStorageTypeQuery.Text = this.GetRequestParam("Storage");
            this.txtStackCodeQuery.Text = this.GetRequestParam("StackCode");
            this.txtItemCodeQuery.Text = this.GetRequestParam("ItemCode");
            ViewState["MModelCode"] = this.GetRequestParam("MModelCode");
            ViewState["Company"] = this.GetRequestParam("Company");


            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }
            object[] getObjects = _facade.QueryDNTempOut(this.txtStorageTypeQuery.Text,
                                                         this.txtItemCodeQuery.Text,
                                                         ViewState["MModelCode"].ToString(),
                                                         this.txtStackCodeQuery.Text,
                                                         ViewState["Company"].ToString());

            if (getObjects != null)
            {
                this.txtSAP_REPORTQTY.Text = Convert.ToString(((DNTempOutMessage)getObjects[0]).SAPQTY);
            }
            else
            {
                this.txtSAP_REPORTQTY.Text = "0";
            }

        }

        #endregion

        #region WebGrid
        protected void InitWebGrid()
        {
            this.gridHelper.AddColumn("DNNOOut", "交货单号", null);
            this.gridHelper.AddColumn("DNLineNO", "行项目号", null);
            this.gridHelper.AddColumn("TempOutQty", "绑定数量", null);

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        private void InitOnPostBack()
        {
            this.buttonHelper = new ButtonHelper(this);
            this.buttonHelper.SetEditObjectHandle = new SetEditObjectDelegate(this.SetEditObject);
            this.buttonHelper.AfterPageStatusChangeHandle = new PageStatusChangeDelegate(this.buttonHelper_AfterPageStatusChangeHandle);

            this.gridHelper = new GridHelper(this.gridWebGrid);
            this.gridHelper.LoadDataSourceHandle = new LoadDataSourceDelegate(this.LoadDataSource);
            this.gridHelper.BuildGridRowhandle = new BuildGridRowDelegate(this.GetGridRow);
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								((DNTempOut)obj).DNNO.ToString(),
                                ((DNTempOut)obj).DNLine.ToString() ,
                                ((DNTempOut)obj).TempQty.ToString()
                            });
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }

            return this._facade.QueryDNTempOut(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStackCodeQuery.Text)),
                                               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)),
                                               inclusive, exclusive);
        }


        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }
            return this._facade.GetDNTempOutCount(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtStackCodeQuery.Text)),
                                                  FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)));
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }

            DNTempOut dnTempOut = domainObject as DNTempOut;
            DeliveryFacade deliveryFacade = new DeliveryFacade(this.DataProvider);

            DeliveryNote deliveryNote = (DeliveryNote)deliveryFacade.GetDeliveryNote(dnTempOut.DNNO, dnTempOut.DNLine);

            if (deliveryNote == null)
            {
                WebInfoPublish.PublishInfo(this, "$ERROR_DNNO_And_DNLine_NOT_Exist", this.languageComponent1);
                return;
            }

            if (deliveryNote.DNStatus == DNStatus.StatusClose)
            {
                WebInfoPublish.PublishInfo(this, "$ERROR_DNNO_And_DNLine_Close", this.languageComponent1);
                return;
            }

            if (deliveryNote.ItemCode.Trim().ToUpper() != this.txtItemCodeQuery.Text.Trim().ToUpper())
            {
                WebInfoPublish.PublishInfo(this, "$ERROR_DNNO_And_DNLine_ItemCode", this.languageComponent1);
                return;
            }

            object getDNTempOut = _facade.GetDNTempOut(dnTempOut.StackCode, dnTempOut.ItemCode, dnTempOut.DNNO, dnTempOut.DNLine);

            if (getDNTempOut != null)
            {
                WebInfoPublish.PublishInfo(this, "$ERROR_DNNO_And_DNLine_Temped", this.languageComponent1);
                return;
            }

            _facade.AddDNTempOut(dnTempOut);

            //this.txtSAP_REPORTQTY.Text = Convert.ToString(int.Parse(this.txtCanUseredQtyQuery.Text.Trim()) - int.Parse(this.txtTempOutQtyEdit.Text.Trim()));

            this.RequestData();
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }
            this._facade.DeleteDNTempOut((DNTempOut[])domainObjects.ToArray(typeof(DNTempOut)));

            int tempQty = 0;
            for (int i = 0; i < domainObjects.Count; i++)
            {
                tempQty += ((DNTempOut)domainObjects[i]).TempQty;
            }

            //this.txtCanUseredQtyQuery.Text = Convert.ToString(int.Parse(this.txtCanUseredQtyQuery.Text) + tempQty);
            this.RequestData();
        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {
            this.Response.Redirect(this.MakeRedirectUrl("FDNTempOutMP.aspx", new string[] { }, new string[] { }));
        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }
            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            DNTempOut dnTempOut = this._facade.CreateDNTempOut();

            dnTempOut.StackCode = FormatHelper.CleanString(this.txtStackCodeQuery.Text.ToUpper());
            dnTempOut.ItemCode = FormatHelper.CleanString(this.txtItemCodeQuery.Text.ToUpper());
            dnTempOut.DNNO = FormatHelper.CleanString(this.txtDNNOOutEdit.Text.ToUpper());
            dnTempOut.DNLine = FormatHelper.CleanString(this.txtLineNOEdit.Text.ToUpper());
            dnTempOut.TempQty = int.Parse(FormatHelper.CleanString(this.txtTempOutQtyEdit.Text.ToUpper()));
            dnTempOut.MaintainUser = this.GetUserCode();
            dnTempOut.MaintainDate = dBDateTime.DBDate;
            dnTempOut.MaintainTime = dBDateTime.DBTime;

            return dnTempOut;
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblDNNOOutEdit, this.txtDNNOOutEdit, 40, true));
            manager.Add(new LengthCheck(this.lblLineNOEdit, this.txtLineNOEdit, 6, true));
            manager.Add(new NumberCheck(this.lblTempOutQtyEdit, this.txtTempOutQtyEdit, 0, 999999999, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (_facade == null)
            {
                _facade = new InventoryFacade(this.DataProvider);
            }
            object[] getObjects = _facade.QueryDNTempOut(this.txtStorageTypeQuery.Text,
                                                        this.txtItemCodeQuery.Text,
                                                        ViewState["MModelCode"].ToString(),
                                                        this.txtStackCodeQuery.Text,
                                                        ViewState["Company"].ToString());

            if (getObjects != null)
            {
                this.txtSAP_REPORTQTY.Text = Convert.ToString(((DNTempOutMessage)getObjects[0]).SAPQTY);
            }

            int allTempQty = 0;
            foreach (UltraGridRow row in this.gridWebGrid.Rows)
            {
                allTempQty = allTempQty + int.Parse(row.Cells[3].Text.ToString().Trim());
            }
            allTempQty = allTempQty + int.Parse(this.txtTempOutQtyEdit.Text.Trim());

            if (allTempQty > int.Parse(this.txtSAP_REPORTQTY.Text.Trim()))
            {
                WebInfoPublish.PublishInfo(this, "$ERROR_CanUseredQty_Must_Over_TempQty", this.languageComponent1);
                return false;
            }

            return true;
        }

        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_facade == null)
            {
                _facade = new InventoryFacade(base.DataProvider);
            }
            object obj = _facade.GetDNTempOut(FormatHelper.CleanString(this.txtStackCodeQuery.Text.ToUpper()),
                                             FormatHelper.CleanString(this.txtItemCodeQuery.Text.ToUpper()),
                                             FormatHelper.CleanString(row.Cells[1].Text.ToUpper()),
                                             FormatHelper.CleanString(row.Cells[2].Text.ToUpper()));
            if (obj != null)
            {
                return obj;
            }

            return null;
        }

        #endregion

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{ 
                                ((DNTempOut)obj).DNNO.ToString(),
                                ((DNTempOut)obj).DNLine.ToString() ,
                                ((DNTempOut)obj).TempQty.ToString() };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"DNNOOut",
                                    "DNLineNO",									
                                    "TempOutQty"
            };
        }

        #endregion





    }
}
