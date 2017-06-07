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
    public partial class FEQPOEE : BaseMPageNew
    {
        protected System.Web.UI.WebControls.Label lblRouteTitle;


        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        BenQGuru.eMES.BaseSetting.SystemSettingFacade sysFacade = null;
        private BenQGuru.eMES.Material.EquipmentFacade _facade = null;//	new BaseModelFacadeFactory().Create();
        private BenQGuru.eMES.BaseSetting.BaseModelFacade _BaseModelFacadeFactory = null;

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

                drpStepSequenceCodeEdit_Load();

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
            this.gridHelper.AddColumn("EQPDESC", "设备描述", null);
            this.gridHelper.AddColumn("EQPWorkingTime", "计划工作时长", null);
            this.gridHelper.AddColumn("SSCode", "产线代码", null);
            this.gridHelper.AddColumn("ResourceCode", "资源代码", null);
            this.gridHelper.AddColumn("MaintainUser", "维护日期", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("MaintainTime", "维护时间", null);
 
            this.gridHelper.AddDefaultColumn(true, true);
            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{"false",
            //                    ((Domain.Equipment.EQPOEEForQuery)obj).Eqpid,
            //                    ((Domain.Equipment.EQPOEEForQuery)obj).EQPDESC,
            //                    ((Domain.Equipment.EQPOEEForQuery)obj).Worktime,
            //                    ((Domain.Equipment.EQPOEEForQuery)obj).Sscode,
            //                    //((Domain.Equipment.EQPOEEForQuery)obj).Opcode ,
            //                    ((Domain.Equipment.EQPOEEForQuery)obj).ResCode ,
            //                    ((Domain.Equipment.EQPOEEForQuery)obj).GetDisplayText("MaintainUser"),
            //                    FormatHelper.ToDateString(((Domain.Equipment.EQPOEEForQuery)obj).Mdate),
            //                    FormatHelper.ToTimeString(((Domain.Equipment.EQPOEEForQuery)obj).Mtime),""
            //    });
            DataRow row = this.DtSource.NewRow();
            row["EQPID"] = ((Domain.Equipment.EQPOEEForQuery)obj).Eqpid;
            row["EQPDESC"] = ((Domain.Equipment.EQPOEEForQuery)obj).EQPDESC;
            row["EQPWorkingTime"] = ((Domain.Equipment.EQPOEEForQuery)obj).Worktime;
            row["SSCode"] = ((Domain.Equipment.EQPOEEForQuery)obj).Sscode;
            row["ResourceCode"] = ((Domain.Equipment.EQPOEEForQuery)obj).ResCode;
            row["MaintainUser"] = ((Domain.Equipment.EQPOEEForQuery)obj).GetDisplayText("MaintainUser");
            row["MaintainDate"] = FormatHelper.ToDateString(((Domain.Equipment.EQPOEEForQuery)obj).Mdate);
            row["MaintainTime"] = FormatHelper.ToTimeString(((Domain.Equipment.EQPOEEForQuery)obj).Mtime);
            return row;


        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPOEE(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)),inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            return this._facade.QueryEQPOEECount(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDQuery.Text)));


        }

        #endregion

        #region Button

        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            object objEqp = this._facade.GetEquipment(((Domain.Equipment.EQPOEE)domainObject).Eqpid);
            if (objEqp == null)
            {
                WebInfoPublish.Publish(this, "$Error_EQPID_IS_NOT_EXIST", languageComponent1);
                return;

            }
            object obj = this._facade.GetEQPOEE(((Domain.Equipment.EQPOEE)domainObject).Eqpid);
            if (obj == null)
            {
                this._facade.AddEQPOEE((Domain.Equipment.EQPOEE)domainObject);
            }
            else
            {
                WebInfoPublish.Publish(this, "$Error_PK_is_Repeat", languageComponent1);
            }
        }

        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }

            this._facade.DeleteEQPOEE((Domain.Equipment.EQPOEE[])domainObjects.ToArray(typeof(Domain.Equipment.EQPOEE)));

        }

        protected override void UpdateDomainObject(object domainObject)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            this._facade.UpdateEQPOEE((Domain.Equipment.EQPOEE)domainObject);
        }

        //protected override void Grid_ClickCell(UltraGridCell cell)
        //{
        //    base.Grid_ClickCell(cell);
        //}

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtEQPIDEdit.Readonly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtEQPIDEdit.Readonly = true;
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

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            Domain.Equipment.EQPOEE route = this._facade.CreateNewEQPOEE();

            route.Eqpid = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtEQPIDEdit.Text, 40));
            route.Sscode = FormatHelper.CleanString(this.drpStepSequenceCodeEdit.SelectedValue, 40);
            route.ResCode =  FormatHelper.CleanString(this.txtOpCodeEdit.Text.Trim());
            route.Worktime = int.Parse(FormatHelper.CleanString(this.txtEQPWorkingTimeEdit.Text, 8));
            route.MaintainUser = this.GetUserCode();
            route.Mdate = dbDateTime.DBDate;
            route.Mtime = dbDateTime.DBTime;

            return route;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null)
            {
                _facade = new BenQGuru.eMES.Material.EquipmentFacade(base.DataProvider);
            }
            object obj = _facade.GetEQPOEE(row.Items.FindItemByKey("EQPID").Text.ToString());

            if (obj != null)
            {
                return (Domain.Equipment.EQPOEE)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtEQPIDEdit.Text = "";
                this.txtEQPWorkingTimeEdit.Text = "";
                this.txtOpCodeEdit.Text = "";
                this.drpStepSequenceCodeEdit.SelectedIndex = -1;
 
                return;
            }

            try
            {
                this.drpStepSequenceCodeEdit.SelectedValue = ((Domain.Equipment.EQPOEE)obj).Sscode.ToString();
            }
            catch
            {
                this.drpStepSequenceCodeEdit.SelectedIndex = -1;
            }


            this.txtEQPIDEdit.Text = ((Domain.Equipment.EQPOEE)obj).Eqpid.ToString();
            this.txtEQPWorkingTimeEdit.Text = ((Domain.Equipment.EQPOEE)obj).Worktime.ToString();
            this.txtOpCodeEdit.Text = ((Domain.Equipment.EQPOEE)obj).ResCode.ToString();

        }


        protected override bool ValidateInput()
        {
            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(this.lblPEQPIDEdit, this.txtEQPIDEdit, 40, true));
            manager.Add(new LengthCheck(this.lblResourceCodeEdit, this.txtOpCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblSSQuery, this.drpStepSequenceCodeEdit, 40, true));
            manager.Add(new LengthCheck(this.lblEQPWorkingTimeEdit, this.txtEQPWorkingTimeEdit, 8, true));


            try
            {
                if (this.txtEQPWorkingTimeEdit.Text.Trim() != string.Empty)
                {
                    int.Parse(this.txtEQPWorkingTimeEdit.Text.Trim());
                }                
            }
            catch
            {
                manager.Add(new NumberCheck(this.lblEQPWorkingTimeEdit, this.txtEQPWorkingTimeEdit, 0,99999999, true));
            }
            if (!manager.Check())
            {
                WebInfoPublish.Publish(this, manager.CheckMessage, this.languageComponent1);
                return false;
            }

            return true;
        }

        #endregion

        #region 数据初始化


        private void drpStepSequenceCodeEdit_Load()
        {
            if (!this.IsPostBack)
            {
                DropDownListBuilder builder = new DropDownListBuilder(this.drpStepSequenceCodeEdit);
                if (_BaseModelFacadeFactory == null)
                {
                    _BaseModelFacadeFactory = new BaseModelFacadeFactory(base.DataProvider).Create();
                }
                builder.HandleGetObjectList += new BenQGuru.eMES.Web.Helper.GetObjectListDelegate(this._BaseModelFacadeFactory.GetAllStepSequence);

                builder.Build("StepSequenceDescription", "StepSequenceCode");
                

            }
        }
        #endregion

        #region Export
        // 2005-04-06
        protected override string[] FormatExportRecord(object obj)
        {
            return new string[]{
                                ((Domain.Equipment.EQPOEEForQuery)obj).Eqpid,
                                ((Domain.Equipment.EQPOEEForQuery)obj).EQPDESC,
                                ((Domain.Equipment.EQPOEEForQuery)obj).Worktime.ToString(),
								((Domain.Equipment.EQPOEEForQuery)obj).Sscode,
                                ((Domain.Equipment.EQPOEEForQuery)obj).ResCode ,
 								((Domain.Equipment.EQPOEEForQuery)obj).GetDisplayText("MaintainUser"),
								FormatHelper.ToDateString(((Domain.Equipment.EQPOEEForQuery)obj).Mdate),
								FormatHelper.ToTimeString(((Domain.Equipment.EQPOEEForQuery)obj).Mtime)
                                };
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	"EQPID",
                                    "EQPDESC",
									"EQPWorkingTime",
                                    "SSCode",
                                    "OpCOde",
									"MaintainUser","MaintainDate","MaintainTime"};
        }

        #endregion
    }
}

