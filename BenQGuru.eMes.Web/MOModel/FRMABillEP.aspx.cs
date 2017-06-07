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
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.RMA;
using BenQGuru.eMES.MOModel;
//using BenQGuru.eMES

namespace BenQGuru.eMES.Web.MOModel
{
    /// <summary>
    /// FRouteMP 的摘要说明。



    /// </summary>
    public partial class FRMABillEP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected System.Web.UI.WebControls.Label lblResourceTitle;
        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        protected System.Web.UI.WebControls.TextBox txtShiftTypeEdit;
        //private BenQGuru.eMES.OQC.OQCFacade _oqcfade = null;
        private DataCollect.DataCollectFacade _DataCollectFacade = null;
        private RMAFacade _RMAFacade = null;
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
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                this.txtRMABillCode.Text = this.GetRequestParam("RMABillCode");
                BuildHandelCodeQuery();
                BuildHandelCodeEdit();
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
            this.gridHelper.AddColumn("ModelCode", "产品别代码", null);
            this.gridHelper.AddColumn("ItemCode", "产品代码", null);
            this.gridHelper.AddColumn("RCARD", "产品序列号", null);
            this.gridHelper.AddColumn("ServerCode", "服务单号", null);
            this.gridHelper.AddColumn("CustomCode", "客户代码", null);

            this.gridHelper.AddColumn("Comfrom", "来源", null);
            this.gridHelper.AddColumn("HandelCode", "RMA单处理方式", null);
            this.gridHelper.AddColumn("Maintenance", "保修期", null);
            this.gridHelper.AddColumn("WHReceiveDate", "仓库收货日期", null);
            this.gridHelper.AddColumn("SubCompany", "分公司", null);

            this.gridHelper.AddColumn("ReMoCode", "返工工单", null);
            this.gridHelper.AddColumn("ErrorCode", "不良代码", null);
            this.gridHelper.AddColumn("CompISSUE", "客户投诉现象", null);
            this.gridHelper.AddColumn("ISINShelfLife", "是否在保内", null);
            this.gridHelper.AddColumn("Memo", "备注", null);

            this.gridHelper.AddColumn("MUSER", "维护用户", null);
            this.gridHelper.AddColumn("MDATE", "维护日期", null);
            this.gridHelper.AddColumn("MTIME", "维护时间", null);

            this.gridHelper.AddDefaultColumn(true, true);


            //this.gridWebGrid.Columns[1].Width = 100;
            //this.gridWebGrid.Columns[2].Width = 120;
            //this.gridWebGrid.Columns[3].Width = 120;

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            if (!IsPostBack)
            {
                this.gridHelper.RequestData();
            }
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false", 
            //                       ((RMADetial)obj).Modelcode  ,
            //                       ((RMADetial)obj).Itemcode ,
            //                       ((RMADetial)obj).Rcard ,
            //                       ((RMADetial)obj).Servercode ,
            //                       ((RMADetial)obj).Customcode ,
            //                       ((RMADetial)obj).Comfrom ,
            //                       this.languageComponent1 .GetString (((RMADetial)obj).Handelcode) ,
            //                       ((RMADetial)obj).Maintenance ,
            //                        FormatHelper.ToDateString(((RMADetial)obj).Whreceivedate), 
            //                       ((RMADetial)obj).Subcompany,
            //                       ((RMADetial)obj).Remocode,
            //                       ((RMADetial)obj).Errorcode,
            //                       ((RMADetial)obj).Compissue,
            //                       this.languageComponent1.GetString (((RMADetial)obj).Isinshelflife),
            //                       ((RMADetial)obj).Memo,
            //                       ((RMADetial)obj).GetDisplayText("MaintainUser"),
            //                        FormatHelper.ToDateString(((RMADetial)obj).Mdate),
            //                       FormatHelper.ToTimeString(((RMADetial)obj).Mtime)
                                   
            //            });
            DataRow row = this.DtSource.NewRow();
            row["ModelCode"] = ((RMADetial)obj).Modelcode;
            row["ItemCode"] = ((RMADetial)obj).Itemcode;
            row["RCARD"] = ((RMADetial)obj).Rcard;
            row["ServerCode"] = ((RMADetial)obj).Servercode;
            row["CustomCode"] = ((RMADetial)obj).Customcode;
            row["Comfrom"] = ((RMADetial)obj).Comfrom;
            row["HandelCode"] = this.languageComponent1.GetString(((RMADetial)obj).Handelcode);
            row["Maintenance"] = ((RMADetial)obj).Maintenance;
            row["WHReceiveDate"] = FormatHelper.ToDateString(((RMADetial)obj).Whreceivedate);
            row["SubCompany"] = ((RMADetial)obj).Subcompany;
            row["ReMoCode"] = ((RMADetial)obj).Remocode;
            row["ErrorCode"] = ((RMADetial)obj).Errorcode;
            row["CompISSUE"] = ((RMADetial)obj).Compissue;
            row["ISINShelfLife"] = this.languageComponent1.GetString(((RMADetial)obj).Isinshelflife);
            row["Memo"] = ((RMADetial)obj).Memo;
            row["MUSER"] = ((RMADetial)obj).GetDisplayText("MaintainUser");
            row["MDATE"] = FormatHelper.ToDateString(((RMADetial)obj).Mdate);
            row["MTIME"] = FormatHelper.ToTimeString(((RMADetial)obj).Mtime);
            
            return row;

        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_RMAFacade == null)
            {
                _RMAFacade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            return this._RMAFacade.QueryRMADetail(
                FormatHelper.CleanString(this.txtRMABillCode.Text),
                FormatHelper.CleanString(this.txtModelQuery.Text),
                FormatHelper.CleanString(this.txtItemCodeQuery.Text),
                FormatHelper.CleanString(this.txtRCardQuery.Text),
                FormatHelper.CleanString(this.txtCusCodeQuery.Text),
                FormatHelper.CleanString(this.txtErrorCodeQuery.Text),
               FormatHelper.CleanString(this.drpHandelCodeQuery.SelectedValue),
                              FormatHelper.CleanString(this.txtSubCompanyQuery.Text),
              inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_RMAFacade == null)
            {
                _RMAFacade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            return this._RMAFacade.QueryRMADetailCount(
                FormatHelper.CleanString(this.txtRMABillCode.Text),
                FormatHelper.CleanString(this.txtModelQuery.Text),
                FormatHelper.CleanString(this.txtItemCodeQuery.Text),
                FormatHelper.CleanString(this.txtRCardQuery.Text),
                FormatHelper.CleanString(this.txtCusCodeQuery.Text),
                FormatHelper.CleanString(this.txtErrorCodeQuery.Text),
                FormatHelper.CleanString(this.drpHandelCodeQuery.SelectedValue),
                              FormatHelper.CleanString(this.txtSubCompanyQuery.Text)
              );

        }
        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_RMAFacade == null)
            {
                _RMAFacade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }

            //已经关闭或结案的RMA不能新增行项目

            string RMABillCode = this.txtRMABillCode.Text.Trim();

            object obj = _RMAFacade.GetRMABill(RMABillCode);
            if (((RMABill)obj).Status == RMABillStatus.Closed )
            {
                WebInfoPublish.Publish(this, "$BS_RMABillStatus_IsClose_CannotAdd", this.languageComponent1);
                return;
            }
            if (((RMABill)obj).Status == RMABillStatus.Opened)
            {
                WebInfoPublish.Publish(this, "$BS_RMABillStatus_IsOpened_CannotAdd", this.languageComponent1);
                return;
            }
            //新增前唯一性检查
            string rcard = FormatHelper.CleanString(this.txtRCardEdit.Text).ToUpper();
            if (_DataCollectFacade == null)
            {
                _DataCollectFacade = new BenQGuru.eMES.DataCollect.DataCollectFacade(this.DataProvider);
            }

            object objSim = _DataCollectFacade.GetLastSimulationReport(rcard);
            //if (objSim == null)
            //{
            //    WebInfoPublish.Publish(this, "$BS_RCARD_NOT_EXIST", this.languageComponent1);
            //    return;
            //}


            if (objSim != null && (objSim as Domain.DataCollect.SimulationReport).IsComplete != "1")
            {
                WebInfoPublish.Publish(this, "$BS_RCARD_IS_NOT_COMPLETE", this.languageComponent1);
                return;
            }

            object objExist = this._RMAFacade.GetRMADetailByRCard( rcard);
            if (objExist != null)
            {
                WebInfoPublish.Publish(this, "$BS_ALREADY_RMABillDetial", this.languageComponent1);
                return;
            }
            

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ((RMADetial)domainObject).Mdate = dbDateTime.DBDate;
            ((RMADetial)domainObject).Mtime = dbDateTime.DBTime;
            ((RMADetial)domainObject).MaintainUser = this.GetUserCode();

            this._RMAFacade.AddRMADetial((RMADetial)domainObject);
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_RMAFacade == null)
            {
                _RMAFacade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }

            //已经关闭或结案的RMA不能更新项目

            //string RMABillCode = this.txtRMABillCode.Text.Trim();

            //object obj = _RMAFacade.GetRMABill(RMABillCode);
            //if (((RMABill)obj).Status == RMABillStatus.Closed)
            //{
            //    WebInfoPublish.PublishInfo(this, "$BS_RMABillStatus_IsClose_CannotUpdate", this.languageComponent1);
            //    return;
            //}
            //if (((RMABill)obj).Status == RMABillStatus.Opened)
            //{
            //    WebInfoPublish.PublishInfo(this, "$BS_RMABillStatus_IsOpended_CannotUpdate", this.languageComponent1);
            //    return;
            //}

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ((RMADetial)domainObject).Mdate = dbDateTime.DBDate;
            ((RMADetial)domainObject).Mtime = dbDateTime.DBTime;
            ((RMADetial)domainObject).MaintainUser = this.GetUserCode();

            this._RMAFacade.UpdateRMADetial((RMADetial)domainObject);

        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Cancel)
            {
               this.drpHandelCodeEdit.Enabled = true;
            }

            if (pageAction == PageActionType.Update)
            {
                string RMABillCode = this.txtRMABillCode.Text.Trim();
                object obj = _RMAFacade.GetRMABill(RMABillCode);
                if (((RMABill)obj).Status == RMABillStatus.Closed || ((RMABill)obj).Status == RMABillStatus.Opened)
                {
                    this.drpHandelCodeEdit.Enabled = false;
                }
                this.txtRCardEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Save)
            {
                this.drpHandelCodeEdit.Enabled = true;
            }
            if (pageAction == PageActionType.Add)
            {
                this.txtRCardEdit.ReadOnly = false;
            }

        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_RMAFacade == null)
            {
                _RMAFacade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            //已经关闭或结案的RMA不能新删除项目

            string RMABillCode = this.txtRMABillCode.Text.Trim();

            object obj = _RMAFacade.GetRMABill(RMABillCode);
            if (((RMABill)obj).Status == RMABillStatus.Closed)
            {
                WebInfoPublish.PublishInfo(this, "$BS_RMABillStatus_IsClose_CannotDelete", this.languageComponent1);
                return;
            }
            if (((RMABill)obj).Status == RMABillStatus.Opened)
            {
                WebInfoPublish.PublishInfo(this, "$BS_RMABillStatus_IsOpened_CannotDelete", this.languageComponent1);
                return;
            }

            _RMAFacade.DeleteRMADetial((Domain.RMA.RMADetial[])domainObjects.ToArray(typeof(Domain.RMA.RMADetial)));

        }

        protected void cmdReturn_ServerClick(object sender, EventArgs e)
        {

            this.Response.Redirect(this.MakeRedirectUrl("./FRMABillMP.aspx",
               new string[] { "RMABillCode" },
               new string[] { this.txtRMABillCode.Text }));

        }

        #endregion

        #region Object <--> Page

        protected override object GetEditObject(GridRecord row)
        {
            if (_RMAFacade == null)
            {
                _RMAFacade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            string RMABillCode = FormatHelper.CleanString(this.txtRMABillCode.Text);
            string rcard = row.Items.FindItemByKey("RCARD").Value.ToString();
            object obj = _RMAFacade.GetRMABill(RMABillCode);
            if (((RMABill)obj).Status == RMABillStatus.Closed || ((RMABill)obj).Status == RMABillStatus.Opened)
            {
                WebInfoPublish.Publish(this, "$BS_RMABillStatus_CannotUpdate", this.languageComponent1);
                return null;
            }

            object objDetial = _RMAFacade.GetRMADetail(rcard, RMABillCode);
            return objDetial;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtRCardEdit.Text = "";
                this.txtComfromEdit.Text = "";
                this.txtCompIssueEdit.Text = "";
                this.txtCustomCodeEdit.Text = "";
                this.txtErrorCodeEdit.Text = "";
                this.txtMemoEdit.Text = "";
                this.txtItemCodeEdit.Text = "";
                this.txtMaintenanceEdit.Text = "";
                this.txtModelCodeEdit.Text = "";
                this.txtReMOEdit.Text = "";
                this.txtServerCodeEdit.Text = "";
                this.txtSubCompanyEdit.Text = "";
                this.cbxIsInShelfLifeEdit.Checked = false;
                this.drpHandelCodeEdit.SelectedIndex = 0;
                this.WHReceiveDateEdit.Text = "";
                return;
            }
            this.txtRMABillCode.Text = ((RMADetial)obj).Rmabillcode;
            this.txtRCardEdit.Text = ((RMADetial)obj).Rcard;
            this.txtComfromEdit.Text = ((RMADetial)obj).Comfrom;
            this.txtCompIssueEdit.Text = ((RMADetial)obj).Compissue;
            this.txtCustomCodeEdit.Text = ((RMADetial)obj).Customcode;
            this.txtErrorCodeEdit.Text = ((RMADetial)obj).Errorcode;
            this.txtMemoEdit.Text = ((RMADetial)obj).Memo;
            this.txtItemCodeEdit.Text = ((RMADetial)obj).Itemcode;
            this.txtMaintenanceEdit.Text = ((RMADetial)obj).Maintenance.ToString();
            this.txtModelCodeEdit.Text = ((RMADetial)obj).Modelcode;
            this.txtReMOEdit.Text = ((RMADetial)obj).Remocode;
            this.txtServerCodeEdit.Text = ((RMADetial)obj).Servercode;
            this.txtSubCompanyEdit.Text = ((RMADetial)obj).Subcompany;
            this.WHReceiveDateEdit.Text =  FormatHelper.ToDateString(((RMADetial)obj).Whreceivedate);
            this.drpHandelCodeEdit.SelectedValue = ((RMADetial)obj).Handelcode;
            if (((RMADetial)obj).Isinshelflife == "Y")
            {
                this.cbxIsInShelfLifeEdit.Checked = true;
            }
            else
            {
                this.cbxIsInShelfLifeEdit.Checked = false;
            }

        }

        protected override object GetEditObject()
        {
            if (_RMAFacade == null)
            {
                _RMAFacade = new BenQGuru.eMES.MOModel.RMAFacade(base.DataProvider);
            }
            string RMABillCode = FormatHelper.CleanString(this.txtRMABillCode.Text);
            string rcard = "";
            if (FormatHelper.CleanString(this.txtRCardEdit.Text).Length > 0)
            {
                rcard = FormatHelper.CleanString(this.txtRCardEdit.Text);
            }
            object obj = _RMAFacade.GetRMADetail(RMABillCode,rcard);
            RMADetial rmadetail = (RMADetial)obj;
            if (rmadetail == null)
            {
                rmadetail = this._RMAFacade.CreateNewRMADetial();
                rmadetail.Rmabillcode = RMABillCode;
                rmadetail.Rcard = FormatHelper.CleanString(this.txtRCardEdit.Text.ToUpper(),40);
                rmadetail.Comfrom = FormatHelper.CleanString(this.txtComfromEdit.Text,100);
                if (rmadetail.Comfrom == "")
                {
                    rmadetail.Comfrom = " ";
                }
                rmadetail.Compissue = FormatHelper.CleanString(this.txtCompIssueEdit.Text,200);
                if (rmadetail.Compissue == "")
                {
                    rmadetail.Compissue = " ";
                }
                rmadetail.Customcode = FormatHelper.CleanString(this.txtCustomCodeEdit.Text,40);
                if (rmadetail.Customcode == "")
                {
                    rmadetail.Customcode = " ";
                }
                rmadetail.Errorcode = FormatHelper.CleanString(this.txtErrorCodeEdit.Text,40);
                rmadetail.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
                rmadetail.Itemcode = FormatHelper.CleanString(this.txtItemCodeEdit.Text,40);
                rmadetail.Maintenance = int.Parse(FormatHelper.CleanString(txtMaintenanceEdit.Text,4));
                rmadetail.Modelcode = FormatHelper.CleanString(this.txtModelCodeEdit.Text,40);
                rmadetail.Remocode = FormatHelper.CleanString(this.txtReMOEdit.Text,40);
                rmadetail.Servercode = FormatHelper.CleanString(this.txtServerCodeEdit.Text,40);
                rmadetail.Subcompany = FormatHelper.CleanString(this.txtSubCompanyEdit.Text,40);
                rmadetail.Handelcode = FormatHelper.CleanString(this.drpHandelCodeEdit.SelectedValue,40);
                rmadetail.Whreceivedate = FormatHelper.TODateInt(FormatHelper.CleanString(this.WHReceiveDateEdit.Text));

                if (cbxIsInShelfLifeEdit.Checked)
                {
                    rmadetail.Isinshelflife = "Y";
                }
                else
                {
                    rmadetail.Isinshelflife = "N";
                }
                return rmadetail;             
            }
            else
            {
                rmadetail.Rcard = FormatHelper.CleanString(this.txtRCardEdit.Text.ToUpper(), 40);
                rmadetail.Comfrom = FormatHelper.CleanString(this.txtComfromEdit.Text, 100);
                if (rmadetail.Comfrom == "")
                {
                    rmadetail.Comfrom = " ";
                }
                if (this.txtCompIssueEdit.Text.Trim() != "")
                {
                    rmadetail.Compissue = FormatHelper.CleanString(this.txtCompIssueEdit.Text, 200);
                }
                else
                {
                    rmadetail.Compissue = " ";
                }
                rmadetail.Customcode = FormatHelper.CleanString(this.txtCustomCodeEdit.Text, 40);
                if (rmadetail.Customcode == "")
                {
                    rmadetail.Customcode = " ";
                }
                rmadetail.Errorcode = FormatHelper.CleanString(this.txtErrorCodeEdit.Text, 40);
                rmadetail.Memo = FormatHelper.CleanString(this.txtMemoEdit.Text, 200);
                rmadetail.Itemcode = FormatHelper.CleanString(this.txtItemCodeEdit.Text, 40);
                rmadetail.Maintenance = int.Parse(FormatHelper.CleanString(txtMaintenanceEdit.Text, 4));
                rmadetail.Modelcode = FormatHelper.CleanString(this.txtModelCodeEdit.Text, 40);
                rmadetail.Remocode = FormatHelper.CleanString(this.txtReMOEdit.Text, 40);
                rmadetail.Servercode = FormatHelper.CleanString(this.txtServerCodeEdit.Text, 40);
                rmadetail.Subcompany = FormatHelper.CleanString(this.txtSubCompanyEdit.Text, 40);
                rmadetail.Handelcode = FormatHelper.CleanString(this.drpHandelCodeEdit.SelectedValue, 40);
                rmadetail.Whreceivedate = int.Parse(FormatHelper.CleanString(this.WHReceiveDateEdit.Text));
                if (cbxIsInShelfLifeEdit.Checked)
                {
                    rmadetail.Isinshelflife = "Y";
                }
                else
                {
                    rmadetail.Isinshelflife = "N";
                }
                
                return rmadetail;
            }

        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            
            manager.Add(new LengthCheck(this.lblModelCodeEdit, this.txtModelCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblItemCodeEdit, this.txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblRCardEdit, this.txtRCardEdit, 40, true));
            manager.Add(new LengthCheck(this.lblServerCodeEdit, this.txtServerCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblHandelCodeEdit, this.drpHandelCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblMaintenanceEdit, this.txtMaintenanceEdit, 4, true));
            manager.Add(new NumberCheck(this.lblMaintenanceEdit, this.txtMaintenanceEdit, 0, 9999, false));

           

            if (this.drpHandelCodeEdit.SelectedValue == "ts")
            {
                manager.Add(new LengthCheck(this.lblErrorCodeEdit, this.txtErrorCodeEdit, 40, true));
            }

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (this.WHReceiveDateEdit.Text.Trim() != "")
            {
                try
                {
                    FormatHelper.TODateInt(FormatHelper.CleanString(this.WHReceiveDateEdit.Text));
                }
                catch (Exception ex)
                {
                    WebInfoPublish.Publish(this, "BS_ERROR_WHReceiveDate", this.languageComponent1);
                    return false;
                }
            }
            return true;
        }

        #endregion

        private void BuildHandelCodeEdit()
        {
            this.drpHandelCodeEdit.Items.Add(new ListItem("", ""));
            this.drpHandelCodeEdit.Items.Add(new ListItem(this.languageComponent1.GetString("rework"), "rework"));
            this.drpHandelCodeEdit.Items.Add(new ListItem(this.languageComponent1.GetString("ts"), "ts"));
        }

        private void BuildHandelCodeQuery()
        {
            this.drpHandelCodeQuery.Items.Add(new ListItem("", ""));
            this.drpHandelCodeQuery.Items.Add(new ListItem(this.languageComponent1.GetString("rework"), "rework"));
            this.drpHandelCodeQuery.Items.Add(new ListItem(this.languageComponent1.GetString("ts"), "ts"));
        }

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{   ((RMADetial)obj).Modelcode  ,
                                   ((RMADetial)obj).Itemcode ,
                                   ((RMADetial)obj).Rcard ,
                                   ((RMADetial)obj).Servercode ,
                                   ((RMADetial)obj).Customcode ,
                                   ((RMADetial)obj).Comfrom ,
                                   this.languageComponent1 .GetString (((RMADetial)obj).Handelcode) ,
                                   ((RMADetial)obj).Maintenance.ToString() ,
                                   FormatHelper.ToDateString(((RMADetial)obj).Whreceivedate), 
                                   ((RMADetial)obj).Subcompany,
                                   ((RMADetial)obj).Remocode,
                                   ((RMADetial)obj).Errorcode,
                                   ((RMADetial)obj).Compissue,
                                   this.languageComponent1 .GetString (((RMADetial)obj).Isinshelflife),
                                   ((RMADetial)obj).Memo,
                                   ((RMADetial)obj).GetDisplayText("MaintainUser"),
                                   FormatHelper.ToDateString(((RMADetial)obj).Mdate),
                                   FormatHelper.ToTimeString(((RMADetial)obj).Mtime)
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	  
                "ModelCode", 
            "ItemCode", 
            "RCARD", 
            "ServerCode", 
            "CustomCode", 
            "Comfrom", 
            "HandelCode", 
            "Maintenance", 
            "WHReceiveDate", 
            "SubCompany",
            "ReMoCode", 
            "ErrorCode", 
            "CompISSUE",
            "ISINShelfLife", 
            "Memo",
            "MUSER", 
            "MDATE", 
            "MTIME"
        };
        }

        #endregion

    }
}