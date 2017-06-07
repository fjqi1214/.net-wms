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
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.BaseSetting
{
    /// <summary>
    /// FRouteMP 的摘要说明。
    /// </summary>
    public partial class FEquimentMP : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;


        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = null;
        private BenQGuru.eMES.Material.EquipmentFacade _facade = null;//	new BaseModelFacadeFactory().Create();

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
            if (!IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                //this.drpEQPStatusEdit_Load();
                this.drpEQPStatusQuery_Load();
                this.drpTypeEdit_Load();
                this.drpTypeQuery_Load();//Added by Jarvis
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
            this.gridHelper.AddColumn("EQPID", "设备编码", null);
            this.gridHelper.AddColumn("EQPName", "设备名称", null);
            this.gridHelper.AddColumn("EQPModel", "设备品牌", null);
            this.gridHelper.AddColumn("EQPType", "设备类型", null);
            this.gridHelper.AddColumn("EQPDESC", "设备描述", null);

            // add by  andy xin
            this.gridHelper.AddColumn("EQPType2", "型号", null);
            this.gridHelper.AddColumn("EQPCompany", "厂商名称", null);
            this.gridHelper.AddColumn("Contact", "联系人", null);
            this.gridHelper.AddColumn("TELPHONE", "电话", null);
            this.gridHelper.AddColumn("EATTRIBUTE1", "预留1", null);
            this.gridHelper.AddColumn("EATTRIBUTE2", "预留2", null);
            this.gridHelper.AddColumn("EATTRIBUTE3", "预留3", null);
            this.gridHelper.AddColumn("EQPStatus", "设备状态", null);

            this.gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
            this.gridHelper.AddLinkColumn("EQPManage", "设备处理", null);
            this.gridHelper.AddLinkColumn("EQPTSManage", "维修记录", null);
            this.gridHelper.AddDefaultColumn(true, true);



            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Domain.Equipment.Equipment)obj).EqpId,
            //                    ((Domain.Equipment.Equipment)obj).Eqpname,
            //                    ((Domain.Equipment.Equipment)obj).Model,								
            //                    ((Domain.Equipment.Equipment)obj).Type,
            //                    ((Domain.Equipment.Equipment)obj).EqpDesc,
            //                    ((Domain.Equipment.Equipment)obj).Eqptype,
            //                    ((Domain.Equipment.Equipment)obj).Eqpcompany,
            //                    ((Domain.Equipment.Equipment)obj).Contact,
            //                    ((Domain.Equipment.Equipment)obj).Telphone,
            //                    ((Domain.Equipment.Equipment)obj).Eattribute1,
            //                    ((Domain.Equipment.Equipment)obj).Eattribute2,
            //                    ((Domain.Equipment.Equipment)obj).Eattribute3,
            //                     GetStatusDesc(((Domain.Equipment.Equipment)obj).Eqpstatus),
            //                    ((Domain.Equipment.Equipment)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((Domain.Equipment.Equipment)obj).MaintainDate),
            //                    FormatHelper.ToTimeString(((Domain.Equipment.Equipment)obj).MaintainTime),
            //                    "",
            //                    ""});
            DataRow row = this.DtSource.NewRow();
            row["EQPID"] = ((Domain.Equipment.Equipment)obj).EqpId;
            row["EQPName"] = ((Domain.Equipment.Equipment)obj).Eqpname;
            row["EQPModel"] = ((Domain.Equipment.Equipment)obj).Model;
            row["EQPType"] = ((Domain.Equipment.Equipment)obj).Type;
            row["EQPDESC"] = ((Domain.Equipment.Equipment)obj).EqpDesc;
            row["EQPType2"] = ((Domain.Equipment.Equipment)obj).Eqptype;
            row["EQPCompany"] = ((Domain.Equipment.Equipment)obj).Eqpcompany;
            row["Contact"] = ((Domain.Equipment.Equipment)obj).Contact;
            row["TELPHONE"] = ((Domain.Equipment.Equipment)obj).Telphone;
            row["EATTRIBUTE1"] = ((Domain.Equipment.Equipment)obj).Eattribute1;
            row["EATTRIBUTE2"] = ((Domain.Equipment.Equipment)obj).Eattribute2;
            row["EATTRIBUTE3"] = ((Domain.Equipment.Equipment)obj).Eattribute3;
            row["EQPStatus"] = GetStatusDesc(((Domain.Equipment.Equipment)obj).Eqpstatus);
            row["MaintainUser"] = ((Domain.Equipment.Equipment)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Equipment.Equipment)obj).MaintainDate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Equipment.Equipment)obj).MaintainTime);
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            //return this._facade.QueryEquipmen(
            //    FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)), this.txtEQPDESQuery.Text.Trim(), this.drpEQPStatusQuery.Text.Trim(),
            //    inclusive, exclusive);
            return this._facade.QueryEquipmen(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)), this.txtEQPDESQuery.Text.Trim(), this.drpTypeQuery.Text.Trim(), this.drpEQPStatusQuery.Text.Trim(),
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEquipmenCount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)), this.txtEQPDESQuery.Text.Trim(), this.drpEQPStatusQuery.Text.Trim());
        }

        protected string GetTypeDesc(string type)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            object obj = _facade.GetEquipmentType(type);

            if (obj != null)
            {
                return (obj as Domain.Equipment.EquipmentType).Eqptypedesc;
            }

            return " ";
        }


        protected string GetStatusDesc(string status)
        {
            if (sysFacade == null)
            {
                sysFacade = new SystemSettingFacade(this.DataProvider);
            }

            object obj = sysFacade.GetParameter(status, "EQPSTATUS");

            if (obj != null)
            {
                return (obj as Domain.BaseSetting.Parameter).ParameterDescription;
            }

            return " ";
        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            this._facade.AddEquipment((Domain.Equipment.Equipment)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            this.DataProvider.BeginTransaction();
            try
            {
                foreach (Domain.Equipment.Equipment obj in domainObjects)
                {
                    // 1. 该设备ID在设备维修日志（TBLEQPTSLOG）中状态为New的不允许删除。
                    // s2. 设备保养计划（TBLEQPMaintenance）存在该设备ID也不允许删除。

                    int tsLogCount = this._facade.CheckEQPTSLogExists(obj.EqpId, EquipmentTSLogStatus.EquipmentTSLogStatus_New);
                    if (tsLogCount > 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this, "$EQPID_TSLOG_CONNOT_DELETE $EQPID:" + obj.EqpId, this.languageComponent1);
                        return;
                    }

                    int maintenanceCount = this._facade.QueryEQPMaintenanceCount(obj.EqpId, "", "");
                    if (maintenanceCount > 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.PublishInfo(this, "$EQPID_EQPMaintenance_CONNOT_DELETE $EQPID:" + obj.EqpId, this.languageComponent1);
                        return;
                    }
                    this._facade.DeleteEquipment(obj);  
                }                
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }
            this.DataProvider.CommitTransaction();


        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            this._facade.UpdateEquipment((Domain.Equipment.Equipment)domainObject);
        }

        protected override void Grid_ClickCell(GridRecord row, string commandName)
        {
           // base.Grid_ClickCell(cell);
            if (commandName =="EQPManage")
            {
                this.Response.Redirect(this.MakeRedirectUrl("FEQPLOG.aspx", new string[] { "EQPID" }, new string[] { row.Items.FindItemByKey("EQPID").Text }));
            }
            if (commandName =="EQPTSManage")
            {
                this.Response.Redirect(this.MakeRedirectUrl("FEQPTSLOG.aspx", new string[] { "EQPID" }, new string[] { row.Items.FindItemByKey("EQPID").Text }));
            }
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtEQPIDEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtEQPIDEdit.ReadOnly = true;
            }
            if (pageAction == PageActionType.Delete)
            {
                this.gridHelper.RequestData();
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null)
            {
                _facade = new Material.EquipmentFacade(base.DataProvider);
            }
            Domain.Equipment.Equipment route = this._facade.CreateEquipment();

            route.EqpId = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDEdit.Text, 40));
            route.Model = FormatHelper.CleanString(this.txtEQPModelEdit.Text, 40);
            route.Type = FormatHelper.CleanString(this.drpTypeEdit.SelectedValue,40);
            route.EqpDesc = FormatHelper.CleanString(this.TextBoxEQPDESCEdit.Text, 100);
            route.MaintainUser = this.GetUserCode();
            route.Eattribute1 = FormatHelper.CleanString(this.txtEattribute1Edit.Text, 100);
            route.Eattribute2 = FormatHelper.CleanString(this.txtEattribute2Edit.Text, 100);
            route.Eattribute3 = FormatHelper.CleanString(this.txtEattribute3Edit.Text, 100);
            route.Contact = FormatHelper.CleanString(this.txtContactEdit.Text, 40);
            route.Eqpcompany = FormatHelper.CleanString(this.TxtEQPCompanyEdit.Text, 100);
            //route.Eqpstatus = FormatHelper.CleanString(this.txtEQPStatusEdit.Text,40);
            route.Eqpname = FormatHelper.CleanString(this.txtEQPNameEdit.Text, 100);
            route.Eqptype = FormatHelper.CleanString(this.txtEQPTypeEdit.Text, 40);
            route.Telphone = FormatHelper.CleanString(this.txtTelPhoneEdit.Text, 40);

            return route;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            object obj = _facade.GetEquipment(row.Items.FindItemByKey("EQPID").Text.ToString());

            if (obj != null)
            {
                return (Domain.Equipment.Equipment)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtEQPIDEdit.Text = "";
                //this.drpEQPTypeEdit.SelectedIndex = 0;
                this.TextBoxEQPDESCEdit.Text = "";
                this.txtEQPModelEdit.Text = "";
                this.txtEattribute1Edit.Text = "";
                this.txtEattribute2Edit.Text = "";
                this.txtEattribute3Edit.Text = "";
                this.TxtEQPCompanyEdit.Text = "";
                this.txtTelPhoneEdit.Text = "";
                this.txtContactEdit.Text = "";
                this.txtEQPNameEdit.Text = "";
                this.txtEQPTypeEdit.Text = "";
                //this.txtEQPStatusEdit.Text = "";
                return;
            }

            this.TextBoxEQPDESCEdit.Text = ((Domain.Equipment.Equipment)obj).EqpDesc.ToString();
            try
            {
                this.drpTypeEdit.SelectedValue = ((Domain.Equipment.Equipment)obj).Type.ToString();
            }
            catch
            {
                this.drpTypeEdit.SelectedIndex = 0;
            }
            //try
            //{
            //    this.drpEQPStatusEdit.SelectedValue = ((Domain.Equipment.Equipment)obj).Eqpstatus.ToString();
            //}
            //catch
            //{
            //    this.drpEQPStatusEdit.SelectedIndex = 0;
            //}

            this.txtEQPIDEdit.Text = ((Domain.Equipment.Equipment)obj).EqpId.ToString();
            this.txtEQPModelEdit.Text = ((Domain.Equipment.Equipment)obj).Model.ToString();
            this.txtEattribute1Edit.Text = ((Domain.Equipment.Equipment)obj).Eattribute1.ToString();
            this.txtEattribute2Edit.Text = ((Domain.Equipment.Equipment)obj).Eattribute2.ToString();
            this.txtEattribute3Edit.Text = ((Domain.Equipment.Equipment)obj).Eattribute3.ToString();
            this.TxtEQPCompanyEdit.Text = ((Domain.Equipment.Equipment)obj).Eqpcompany.ToString();
            this.txtTelPhoneEdit.Text = ((Domain.Equipment.Equipment)obj).Telphone.ToString();
            this.txtContactEdit.Text = ((Domain.Equipment.Equipment)obj).Contact.ToString();
            this.txtEQPTypeEdit.Text = ((Domain.Equipment.Equipment)obj).Eqptype.ToString();
            this.txtEQPNameEdit.Text = ((Domain.Equipment.Equipment)obj).Eqpname.ToString();
            //this.txtEQPStatusEdit.Text = ((Domain.Equipment.Equipment)obj).Eqpstatus.ToString();
        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblPEQPIDEdit, this.txtEQPIDEdit, 40, true));
            manager.Add(new LengthCheck(this.lblEQPNameEdit, this.txtEQPNameEdit, 100, true));
            manager.Add(new LengthCheck(this.lblEQPTypeEdit, this.drpTypeEdit, 40, true));

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region 数据初始化

        private void drpEQPStatusQuery_Load()
        {
            if (!this.IsPostBack)
            {
                SystemParameterListBuilder _builder = new SystemParameterListBuilder("EQPSTATUS", base.DataProvider);
                _builder.BuildShowDescription(this.drpEQPStatusQuery);
                this.drpEQPStatusQuery.Items.Insert(0, new ListItem("", ""));
            }
        }

        private void drpTypeEdit_Load()
        {
            if (!this.IsPostBack)
            {
                if (_facade == null)
                {
                    _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
                }

                object[] objs = _facade.GetAllEquipmentType();
                if (objs == null)
                {
                    return;
                }
                foreach (Domain.Equipment.EquipmentType type in objs)
                {
                    this.drpTypeEdit.Items.Add(new ListItem(type.Eqptype, type.Eqptype));
                }
                this.drpTypeEdit.Items.Insert(0, new ListItem("", ""));

            }
        }

        private void drpTypeQuery_Load()//Added by Jarvis
        {
            if (!this.IsPostBack)
            {
                if (_facade == null)
                {
                    _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
                }

                object[] objs = _facade.GetAllEquipmentType();
                if (objs == null)
                {
                    return;
                }
                foreach (Domain.Equipment.EquipmentType type in objs)
                {
                    this.drpTypeQuery.Items.Add(new ListItem(type.Eqptype, type.Eqptype));
                }
                this.drpTypeQuery.Items.Insert(0, new ListItem("", ""));

            }
        }
        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{  ((Domain.Equipment.Equipment)obj).EqpId.ToString(),
                                    ((Domain.Equipment.Equipment)obj).Eqpname.ToString(),
								   ((Domain.Equipment.Equipment)obj).Model.ToString(),
                                    ((Domain.Equipment.Equipment)obj).Type.ToString(),
                                    ((Domain.Equipment.Equipment)obj).EqpDesc.ToString(),
                                 ((Domain.Equipment.Equipment)obj).Eqptype,
                                ((Domain.Equipment.Equipment)obj).Eqpcompany,
                                ((Domain.Equipment.Equipment)obj).Contact,
                                ((Domain.Equipment.Equipment)obj).Telphone,
                                ((Domain.Equipment.Equipment)obj).Eattribute1,
                                ((Domain.Equipment.Equipment)obj).Eattribute2,
                                ((Domain.Equipment.Equipment)obj).Eattribute3,
                                 GetStatusDesc(((Domain.Equipment.Equipment)obj).Eqpstatus),
								   ((Domain.Equipment.Equipment)obj).GetDisplayText("MaintainUser"),
								   FormatHelper.ToDateString(((Domain.Equipment.Equipment)obj).MaintainDate), 
                                      FormatHelper.ToTimeString(((Domain.Equipment.Equipment)obj).MaintainTime), 
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"EQPID",
                                    "EQPName",
									"EQPModel",
									"EQPType",
									"EQPDESC",
                                    "EQPType2",
                                    "EQPCompany",
                                    "Contact",
                                    "TELPHONE",
                                    "EATTRIBUTE1",
                                    "EATTRIBUTE2",
                                    "EATTRIBUTE3",
                                    "EQPStatus",
									"MaintainUser","MaintainDate","MaintainTime"};
        }

        #endregion
    }
}

