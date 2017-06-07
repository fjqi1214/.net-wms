using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.ReportView;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.ReportView;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.ReportView
{
    public partial class FRptDBServer : BaseMPageNew
    {
        #region Init
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        private ReportViewFacade _facade = null;
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
            this.languageComponent1.LanguagePackageDir = "D:\\code\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";

        }
        #endregion
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
            }
            this.lblMessage.Text = "";

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
            this.gridHelper.AddColumn("DATACONNECTID", "ID", null);
            this.gridHelper.AddColumn("ConnectName", "名称", null);
            this.gridHelper.AddColumn("DescriptionTest", "描述", null);
            this.gridHelper.AddColumn("ServerType", "数据库类型", null);
            this.gridHelper.AddColumn("ServiceName", "数据库服务名", null);
            this.gridHelper.AddColumn("UserName", "连接用户名", null);
            this.gridHelper.AddColumn("DefaultDatabase", "默认数据库名", null);
            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridWebGrid.Columns.FromKey("MaintainTime").Hidden = true;
            this.gridWebGrid.Columns.FromKey("DATACONNECTID").Hidden = true;
            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = DtSource.NewRow();
            row["DATACONNECTID"] = ((RptViewDataConnect)obj).DataConnectID.ToString();
            row["ConnectName"] = ((RptViewDataConnect)obj).ConnectName.ToString();
            row["DescriptionTest"] = ((RptViewDataConnect)obj).Description.ToString();
            row["ServerType"] = ((RptViewDataConnect)obj).ServerType.ToString();
            row["ServiceName"] = ((RptViewDataConnect)obj).ServiceName.ToString();
            row["UserName"] = ((RptViewDataConnect)obj).UserName.ToString();
            row["DefaultDatabase"] = ((RptViewDataConnect)obj).DefaultDatabase.ToString();
            row["MaintainUser"] = ((RptViewDataConnect)obj).MaintainUser.ToString();
            row["MaintainDate"] = FormatHelper.ToDateString(((RptViewDataConnect)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((RptViewDataConnect)obj).MaintainTime);
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.QueryDBServer(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtnameQuery.Text)),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            return this._facade.QueryDataConnectCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCodeQuery.Text)),
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtnameQuery.Text)));
        }

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            ((RptViewDataConnect)domainObject).DataConnectID = Convert.ToDecimal(_facade.GetRptViewDataConnectNextId());
            this._facade.AddRptViewDataConnect((RptViewDataConnect)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {

            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            this._facade.DeleteRptViewDataConnect((RptViewDataConnect[])domainObjects.ToArray(typeof(RptViewDataConnect)));
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            ((RptViewDataConnect)domainObject).DataConnectID = Convert.ToDecimal(this.datasourceid.Value);
            this._facade.UpdateRptViewDataConnect((RptViewDataConnect)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            //if (pageAction == PageActionType.Add)
            //{
            //    this.txtShiftCodeEdit.ReadOnly = false;
            //}

            //if (pageAction == PageActionType.Update)
            //{
            //    this.txtShiftCodeEdit.ReadOnly = true;
            //}
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            RptViewDataConnect rptviewdataconnect = this._facade.CreateNewRptViewDataConnect();

            rptviewdataconnect.ConnectName = FormatHelper.CleanString(this.txtNameEdit.Text);
            rptviewdataconnect.Description = FormatHelper.CleanString(this.txtDescriptEdit.Text, 100);
            rptviewdataconnect.ServerType = this.drpDBTypeEdit.SelectedValue;// FormatHelper.TOTimeInt(this.timeShiftEndTimeEdit.Text);
            rptviewdataconnect.ServiceName = FormatHelper.CleanString(this.txtDBNameEdit.Text);
            rptviewdataconnect.DefaultDatabase = FormatHelper.CleanString(this.txtDBDefaultNameEdit.Text);
            rptviewdataconnect.UserName = FormatHelper.CleanString(this.txtconnectusername.Text);
            if (this.chknopassword.Checked)
            {
                rptviewdataconnect.Password = EncryptionHelper.DESEncryption("");
            }
            else
            {
                if (this.cmdSave.Disabled)
                {
                    rptviewdataconnect.Password = EncryptionHelper.DESEncryption(FormatHelper.CleanString(txtpassword.Text));
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtpassword.Text))
                    {
                        rptviewdataconnect.Password = EncryptionHelper.DESEncryption(FormatHelper.CleanString(txtpassword.Text));
                    }
                    else
                    {
                        rptviewdataconnect.Password = ((RptViewDataConnect)_facade.GetRptViewDataConnect(Convert.ToDecimal(this.datasourceid.Value))).Password;
                    }
                }
            }
            rptviewdataconnect.MaintainUser = this.GetUserCode();
            return rptviewdataconnect;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new ReportViewFacade(base.DataProvider);
            }
            object obj = _facade.GetRptViewDataConnect(Convert.ToDecimal(row.Items.FindItemByKey("DATACONNECTID").Text));

            if (obj != null)
            {
                return obj as RptViewDataConnect;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtconnectusername.Text = "";
                this.txtDBDefaultNameEdit.Text = "";
                this.txtDBNameEdit.Text = "";
                this.txtDescriptEdit.Text = "";
                this.txtNameEdit.Text = "";
                this.txtpassword.Text = "";
                this.drpDBTypeEdit.SelectedIndex = 0;
                this.chknopassword.Checked = false;
                this.txtpassword.ReadOnly = false;
                this.datasourceid.Value = "";
                return;
            }

            this.txtconnectusername.Text = ((RptViewDataConnect)obj).UserName.ToString();
            this.txtNameEdit.Text = ((RptViewDataConnect)obj).ConnectName.ToString();
            this.txtDescriptEdit.Text = ((RptViewDataConnect)obj).Description;
            this.txtDBNameEdit.Text = ((RptViewDataConnect)obj).ServiceName.ToString();
            this.txtDBDefaultNameEdit.Text = ((RptViewDataConnect)obj).DefaultDatabase.ToString();
            this.txtpassword.Text = EncryptionHelper.DESDecryption(((RptViewDataConnect)obj).Password.ToString());
            this.drpDBTypeEdit.SelectedIndex =
                 this.drpDBTypeEdit.Items.IndexOf(this.drpDBTypeEdit.Items.FindByValue(((RptViewDataConnect)obj).ServerType));
            this.chknopassword.Checked = false;
            this.datasourceid.Value = ((RptViewDataConnect)obj).DataConnectID.ToString();
        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblnameEdit, this.txtNameEdit, 40, true));
            manager.Add(new LengthCheck(lblservername, this.txtDBNameEdit, 40, true));
            manager.Add(new LengthCheck(this.lblconnectusername, this.txtconnectusername, 40, true));
            manager.Add(new LengthCheck(lbldescEdit, this.txtDescriptEdit, 100, false));
            if (this.cmdSave.Disabled)
            {
                if (!this.chknopassword.Checked)
                {
                    manager.Add(new LengthCheck(this.lblpassword, this.txtpassword, 40, true));
                }
            }
            if (this.drpDBTypeEdit.SelectedIndex != 0)
            {
                manager.Add(new LengthCheck(this.lblDefaultDB, this.txtDBDefaultNameEdit, 40, true));
            }

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }
            return true;
        }

        #endregion


        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((RptViewDataConnect)obj).ConnectName.ToString(),
								   ((RptViewDataConnect)obj).Description.ToString(),
								   ((RptViewDataConnect)obj).ServerType.ToString(),
								   ((RptViewDataConnect)obj).ServiceName.ToString(),
								   ((RptViewDataConnect)obj).UserName,
								   ((RptViewDataConnect)obj).DefaultDatabase,
								   ((RptViewDataConnect)obj).MaintainUser.ToString(),
								   FormatHelper.ToDateString(((RptViewDataConnect)obj).MaintainDate) };
        }

        protected override string[] GetColumnHeaderText()
        {
            // TODO: 调整字段值的顺序，使之与Grid的列对应
            return new string[] {	
									"ConnectName",
									"Description",
									"ServerType",
									"ServiceName",
									"UserName",
                                    "DefaultDatabase",
									"MaintainUser",
									"MaintainDate"};
        }
        #endregion

        protected void Submit1_ServerClick(object sender, EventArgs e)
        {
            BenQGuru.eMES.ReportViewBase.DatasourceBase dsb = new BenQGuru.eMES.ReportViewBase.DatasourceBase();
            if (!string.IsNullOrEmpty(txtNameEdit.Text))
            {
                try
                {
                    string strMsg = "";
                    if (dsb.CheckDataConnect((RptViewDataConnect)GetEditObject()))
                    {

                        //ExceptionManager.Raise(this.GetType(), "$Report_DBTest");
                        strMsg = this.languageComponent1.GetString("$Report_DBTest");
                    }
                    else
                    {
                        //ExceptionManager.Raise(this.GetType(), "$Error_Report_DBTest");
                        strMsg = this.languageComponent1.GetString("$Error_Report_DBTest");
                    }
                    string alertInfo =
                        string.Format("alert('{0}');", strMsg);
                    if (!this.ClientScript.IsClientScriptBlockRegistered("SaveSuccess"))
                    {
                        //this.ClientScript.RegisterClientScriptBlock(typeof(string), "SaveSuccess", alertInfo);
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), Guid.NewGuid().ToString(), alertInfo, true);
                    }
                }
                catch (Exception ex)
                {
                    //this.lblMessage.Text = MessageCenter.ParserMessage(ex.Message, this.languageComponent1);
                    throw ex;
                }
            }
        }

        protected void chknopassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chknopassword.Checked)
            {
                this.txtpassword.Text = "";
                this.txtpassword.ReadOnly = true;
            }
            else
            {
                this.txtpassword.ReadOnly = false;
            }
        }




    }
}
