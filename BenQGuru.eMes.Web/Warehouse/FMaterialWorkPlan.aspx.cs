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
using System.IO;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;
using Infragistics.WebUI.UltraWebGrid;
using BenQGuru.eMES.MOModel;
using System.Collections.Generic;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.BaseSetting;
using System.Text;

namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialWorkPlan : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.Material.MaterialFacade _Facade = null;//new BaseModelFacadeFactory().Create();
        private BenQGuru.eMES.MOModel.ItemFacade _ItemFacade = null;
        private BenQGuru.eMES.MOModel.MOFacade _MoFacade = null;




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
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\BenQGuru.eMES.Web\\";
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

                this.dateDateFrom.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Now));
                this.dateVoucherDateFrom.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Now));
                this.dateVoucherDateTo.Text = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Now));

                this.drpAactionStatus_Load(null, null);
                this.drpMaterialStatus_Load(null, null);


            }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

        private void drpAactionStatus_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DropDownListBuilder _builder = new DropDownListBuilder(this.drpAactionStatus);
                _builder.AddAllItem(languageComponent1);
                this.drpAactionStatus.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Init), WorkPlanActionStatus.WorkPlanActionStatus_Init));
                this.drpAactionStatus.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Ready), WorkPlanActionStatus.WorkPlanActionStatus_Ready));
                this.drpAactionStatus.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Open), WorkPlanActionStatus.WorkPlanActionStatus_Open));
                this.drpAactionStatus.Items.Add(new ListItem(this.languageComponent1.GetString(WorkPlanActionStatus.WorkPlanActionStatus_Close), WorkPlanActionStatus.WorkPlanActionStatus_Close));



            }
        }

        private void drpMaterialStatus_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DropDownListBuilder _builder = new DropDownListBuilder(this.drpMaterialStatus);
                _builder.AddAllItem(languageComponent1);
                this.drpMaterialStatus.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_No), MaterialWarningStatus.MaterialWarningStatus_No));
                this.drpMaterialStatus.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_Delivery), MaterialWarningStatus.MaterialWarningStatus_Delivery));
                this.drpMaterialStatus.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_Responsed), MaterialWarningStatus.MaterialWarningStatus_Responsed));
                this.drpMaterialStatus.Items.Add(new ListItem(this.languageComponent1.GetString(MaterialWarningStatus.MaterialWarningStatus_Lack), MaterialWarningStatus.MaterialWarningStatus_Lack));



            }
        }

        #endregion

        #region Format Data
        protected override void InitWebGrid()
        {
            this.gridHelper.AddColumn("PlanDateFrom", "计划日期", null);
            this.gridHelper.AddColumn("BIGSSCODE", "大线", null);
            this.gridHelper.AddColumn("PLANSEQ", "生产顺序", null);
            this.gridHelper.AddColumn("MOCODE", "工单", null);
            this.gridHelper.AddColumn("MOSEQ", "工单项次", null);
            this.gridHelper.AddColumn("MaterialCode", "物料代码", null);
            this.gridHelper.AddColumn("MModelCode", "机型", null);
            this.gridHelper.AddColumn("PLANQTY", "计划投入产量", null);
            this.gridHelper.AddColumn("PLANSTARTTIME", "计划开始时间", null);
            this.gridHelper.AddColumn("ACTIONSTATUS", "执行状态", null);
            this.gridHelper.AddColumn("MATERIALSTATUS", "配料状态", null);

            this.gridHelper.AddDefaultColumn(true, true);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            base.InitWebGrid();
        }

        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{"false",
								((WorkPlanWithQty)obj).PlanDate,
								((WorkPlanWithQty)obj).BigSSCode.ToString(),
								((WorkPlanWithQty)obj).PlanSeq.ToString(),
                                ((WorkPlanWithQty)obj).MoCode.ToString(),
                                ((WorkPlanWithQty)obj).MoSeq.ToString(),
                                ((WorkPlanWithQty)obj).ItemCode.ToString(),
                                ((WorkPlanWithQty)obj).MaterialModelCode.ToString(),
								((WorkPlanWithQty)obj).PlanQty.ToString(),
								FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PlanStartTime),
                               this.languageComponent1.GetString(((WorkPlanWithQty)obj).ActionStatus.ToString()),
                                this.languageComponent1.GetString(((WorkPlanWithQty)obj).MaterialStatus.ToString())
                              
								});
        }

        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{ 	
                                ((WorkPlanWithQty)obj).PlanDate.ToString(),
								((WorkPlanWithQty)obj).BigSSCode.ToString(),
								((WorkPlanWithQty)obj).PlanSeq.ToString(),
                                ((WorkPlanWithQty)obj).MoCode.ToString(),
                                ((WorkPlanWithQty)obj).MoSeq.ToString(),
                                ((WorkPlanWithQty)obj).ItemCode.ToString(),
                                ((WorkPlanWithQty)obj).MaterialModelCode.ToString(),
								((WorkPlanWithQty)obj).PlanQty.ToString(),
								FormatHelper.ToTimeString(((WorkPlanWithQty)obj).PlanStartTime)
                             
                                            
            };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"PlanDateFrom",
									"BIGSSCODE",	
									"PLANSEQ",
				                    "MOCODE",
									"MOSEQ",
	                                "MaterialCode",
                                    "MModelCode",
									"PLANQTY",
				                    "PLANSTARTTIME"
								
                                   
            };
        }



        #endregion

        #region  WebGrid
        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            return this._Facade.QueryWorkPlan(
                                             FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeWhere.Text)),
                                             FormatHelper.TODateInt(FormatHelper.CleanString(dateVoucherDateFrom.Text)),
                                             FormatHelper.TODateInt(FormatHelper.CleanString(dateVoucherDateTo.Text)),
                                             FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtConditionMo.Text)),
                                             drpAactionStatus.SelectedValue, 
                                             drpMaterialStatus.SelectedValue,
                                             inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            return this._Facade.QueryWorkPlanCount(
                                     FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeWhere.Text)),
                                     FormatHelper.TODateInt(FormatHelper.CleanString(dateVoucherDateFrom.Text)),
                                     FormatHelper.TODateInt(FormatHelper.CleanString(dateVoucherDateTo.Text)),

                                     FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtConditionMo.Text)),
                                     drpAactionStatus.SelectedValue, drpMaterialStatus.SelectedValue
                 );
        }
        #endregion


        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }

            this._Facade.AddWorkPlan((WorkPlan)domainObject);
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }

            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(base.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);

            List<WorkPlan> mKeyPartList = new List<WorkPlan>();

            WorkPlan[] deliveryNotes = (WorkPlan[])domainObjects.ToArray(typeof(WorkPlan));

            if (deliveryNotes != null)
            {
                this.DataProvider.BeginTransaction();

                try
                {
                    foreach (WorkPlan deliveryNote in deliveryNotes)
                    {
                        int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode(deliveryNote.BigSSCode, dbDateTime.DateTime);
                        if (deliveryNote.PlanDate < shiftDay)
                        {

                            WebInfoPublish.Publish(this, "$if_date_cannotmodify", languageComponent1);

                            return;
                        }

                        if (deliveryNote.ActionStatus == WorkPlanActionStatus.WorkPlanActionStatus_Open || deliveryNote.ActionStatus == WorkPlanActionStatus.WorkPlanActionStatus_Close)
                        {
                            WebInfoPublish.Publish(this, "$STATUS_ERROR_DELETE", languageComponent1);
                            return;

                        }

                        this._Facade.DeleteWorkPlan(deliveryNote);

                    }

                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    ExceptionManager.Raise(deliveryNotes[0].GetType(), "$Error_Delete_Domain_Object", ex);
                }
            }
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }

            WorkPlan workPlan = domainObject as WorkPlan;

            if (decimal.Parse(this.txtPlanInQTYEdit.Text) < decimal.Parse(this.txtActQty.Text))
            {
                WebInfoPublish.Publish(this, "$planqty_isnotequ_actqty", languageComponent1);
                return;
            }

            this._Facade.UpdateWorkPlan((WorkPlan)domainObject);
        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                dateDateFrom.Enable = "true";
                dateDateFrom.DateTextIsReadOnly = false;
                txtBigSSCodeGroup.Readonly = false;
                txtMOEdit.ReadOnly = false;
                txtMOSeqEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {

                dateDateFrom.Enable = "false";
                dateDateFrom.DateTextIsReadOnly = true;
                dateDateFrom.EnableViewState = true;
                txtBigSSCodeGroup.Readonly = true;
                txtMOEdit.ReadOnly = true;
                txtMOSeqEdit.ReadOnly = true;

            }
        }


        protected void cmdImport_ServerClick(object sender, System.EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("./ImportData/FExcelDataImp.aspx?itype=WORKPLAN"));
        }
        #endregion

        #region Object <--> Page
        protected override object GetEditObject()
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }

            if (_MoFacade == null)
            {
                _MoFacade = new MOFacade(base.DataProvider);
            }

            DBDateTime dBDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            object objMo = _MoFacade.GetMO(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOEdit.Text)));

            WorkPlan workPlan = (WorkPlan)this._Facade.GetWorkPlan(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroup.Text)),
                                                        FormatHelper.TODateInt(FormatHelper.CleanString(this.dateDateFrom.Date_String)),
                                                        FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOEdit.Text)),
                                                        Convert.ToDecimal(FormatHelper.CleanString(this.txtMOSeqEdit.Text)));

            if (workPlan == null)
            {
                workPlan = _Facade.CreateNewWorkPlan();
                workPlan.PlanDate = FormatHelper.TODateInt(FormatHelper.CleanString(this.dateDateFrom.Date_String));
                workPlan.BigSSCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtBigSSCodeGroup.Text));
                workPlan.MoCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOEdit.Text));
                workPlan.MoSeq = Convert.ToDecimal(FormatHelper.CleanString(this.txtMOSeqEdit.Text));
                workPlan.ItemCode = ((BenQGuru.eMES.Domain.MOModel.MO)objMo).ItemCode;
                workPlan.ActQty = 0;
                workPlan.MaterialQty = 0;
                workPlan.ActionStatus = WorkPlanActionStatus.WorkPlanActionStatus_Init;
                workPlan.MaterialStatus = MaterialWarningStatus.MaterialWarningStatus_No;
            }

            workPlan.PlanSeq = int.Parse(FormatHelper.CleanString(this.txtMactureSeq.Text));
            workPlan.PlanQty = decimal.Parse(FormatHelper.CleanString(this.txtPlanInQTYEdit.Text));
            workPlan.PlanStartTime = FormatHelper.TOTimeInt(this.timeFrom.Text);
            workPlan.MaintainUser = this.GetUserCode();
            workPlan.MaintainDate = dBDateTime.DBDate;
            workPlan.MaintainTime = dBDateTime.DBTime;

            return workPlan;
        }
        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.dateDateFrom.Date_String = FormatHelper.ToDateString(FormatHelper.TODateInt(DateTime.Now));
                this.txtBigSSCodeGroup.Text = "";
                this.txtMactureSeq.Text = "";
                this.txtMOEdit.Text = "";
                this.txtMOSeqEdit.Text = "";
                this.txtPlanInQTYEdit.Text = "";
                this.timeFrom.Text = "";
                this.txtBigSSCodeGroup.Text = "";
                this.txtMat.Text = "";
                this.txtAct.Text = "";
                this.txtActQty.Text = "";

                return;
            }

            //	执行状态为“待投产”、“生产中”状态的计划信息可以修改除主键之外的其他信息【包括导入过程中的修改】，如果计划日期早于当前日期，则不允许修改计划信息
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(base.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);

            int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode(((WorkPlan)obj).BigSSCode, dbDateTime.DateTime);

            if (((WorkPlan)obj).PlanDate < shiftDay)
            {
                WebInfoPublish.PublishInfo(this, "$if_date_cannotmodify", languageComponent1);

                return;
            }

            if (((WorkPlan)obj).ActionStatus == WorkPlanActionStatus.WorkPlanActionStatus_Close)
            {
                WebInfoPublish.PublishInfo(this, "$status_error", languageComponent1);
                return;
            }



            this.dateDateFrom.Date_String = FormatHelper.ToDateString(((WorkPlan)obj).PlanDate);
            this.txtBigSSCodeGroup.Text = ((WorkPlan)obj).BigSSCode;
            this.txtMactureSeq.Text = ((WorkPlan)obj).PlanSeq.ToString();
            this.txtMOEdit.Text = ((WorkPlan)obj).MoCode;
            this.txtMOSeqEdit.Text = ((WorkPlan)obj).MoSeq.ToString();
            this.txtPlanInQTYEdit.Text = ((WorkPlan)obj).PlanQty.ToString();
            this.timeFrom.Text = FormatHelper.ToTimeString(((WorkPlan)obj).PlanStartTime);
            this.txtAct.Text = ((WorkPlan)obj).ActionStatus.ToString();
            this.txtMat.Text = ((WorkPlan)obj).MaterialStatus.ToString();
            this.txtActQty.Text = ((WorkPlan)obj).ActQty.ToString();



        }

        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();
            manager.Add(new DateCheck(this.lblDate, this.dateDateFrom.Text, true));
            manager.Add(new LengthCheck(lblBigSSCodeGroup, txtBigSSCodeGroup, 40, true));
            manager.Add(new LengthCheck(lblMOEdit, txtMOEdit, 40, true));
            manager.Add(new LengthCheck(lblMactureSeq, txtMactureSeq, 10, true));
            manager.Add(new LengthCheck(lblMOSeqEdit, txtMOSeqEdit, 10, true));
            manager.Add(new DecimalCheck(lblPlanInQTYEdit, txtPlanInQTYEdit, 0, 9999999999, true));


            manager.Add(new TimeRangeCheck(this.lblPlanInStartDateEdit, this.timeFrom.Text, this.timeFrom.Text, true));

            if (this.txtMOSeqEdit.Text.Trim().Length > 0)
            {
                manager.Add(new NumberCheck(lblMOSeqEdit, txtMOSeqEdit, 0, 9999999999, true));
            }

            if (this.txtMactureSeq.Text.Trim().Length > 0)
            {
                manager.Add(new NumberCheck(lblMactureSeq, txtMactureSeq, 0, 9999999999, true));
            }

            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            if (_MoFacade == null)
            {
                _MoFacade = new MOFacade(base.DataProvider);
            }

            object objMo = _MoFacade.GetMO(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOEdit.Text)));

            if (objMo == null)
            {
                WebInfoPublish.Publish(this, "$CS_MO_Not_Exist", languageComponent1);
                return false;
            }

            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }

            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(base.DataProvider);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(base.DataProvider);

            WorkPlan GetWorkPlanByKeys = (WorkPlan)_Facade.GetWorkPlan(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtBigSSCodeGroup.Text)),
                                               FormatHelper.TODateInt(FormatHelper.CleanString(this.dateDateFrom.Date_String)),
                                               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOEdit.Text)),
                                               int.Parse(FormatHelper.CleanString(this.txtMOSeqEdit.Text)));

            WorkPlan GetWorkPlanByUnipues = (WorkPlan)_Facade.GetWorkPlan(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtBigSSCodeGroup.Text)),
                                                             FormatHelper.TODateInt(FormatHelper.CleanString(this.dateDateFrom.Date_String)),
                                                             int.Parse(FormatHelper.CleanString(this.txtMactureSeq.Text)));

            if (GetWorkPlanByKeys == null && GetWorkPlanByUnipues != null)
            {
                WebInfoPublish.PublishInfo(this, "$date__isunique", languageComponent1);
                return false;
            }

            if (GetWorkPlanByKeys!=null && GetWorkPlanByUnipues!=null )
            {
                if (GetWorkPlanByKeys.BigSSCode!=GetWorkPlanByUnipues.BigSSCode ||
                    GetWorkPlanByKeys.PlanDate!=GetWorkPlanByUnipues.PlanDate ||
                    GetWorkPlanByKeys.MoCode!=GetWorkPlanByUnipues.MoCode ||
                    GetWorkPlanByKeys.MoSeq!=GetWorkPlanByUnipues.MoSeq)
                {
                    WebInfoPublish.PublishInfo(this, "$date__isunique", languageComponent1);
                    return false;
                }
            }

            int shiftDay = shiftModelFacade.GetShiftDayByBigSSCode(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(txtBigSSCodeGroup.Text)),
                dbDateTime.DateTime);

            if (FormatHelper.TODateInt(FormatHelper.CleanString(this.dateDateFrom.Date_String)) < shiftDay)
            {
                WebInfoPublish.PublishInfo(this, "$if_date_cannotmodify", languageComponent1);
                return false;
            }

            return true;
        }

        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (_Facade == null)
            {
                _Facade = new MaterialFacade(base.DataProvider);
            }
            object obj = _Facade.GetWorkPlan(row.Cells.FromKey("BIGSSCODE").Text.ToString(), int.Parse(row.Cells.FromKey("PlanDateFrom").Text.ToString()), row.Cells.FromKey("MOCODE").Text.ToString(), decimal.Parse(row.Cells.FromKey("MOSEQ").Text.ToString()));

            if (obj != null)
            {
                return (WorkPlan)obj;
            }

            return null;
        }

        #endregion

    }
}
