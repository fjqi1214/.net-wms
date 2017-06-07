using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using ControlLibrary.Web.Language;

namespace BenQGuru.Web.ReportCenter.UserControls
{
    public partial class UCGroupConditions : System.Web.UI.UserControl
    {
        private List<ControlGroupInfo> _DBInfoX = null;
        private List<ControlGroupInfo> _DBInfoY = null;

        override protected void OnInit(EventArgs e)
        {
            InitControlDBInfo();
            HideAllChild();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                RadioButtonListBuilder builder1 = new RadioButtonListBuilder(new NewReportByTimeType(), this.rblByTimeTypeGroup, this.languageComponent1);
                RadioButtonListBuilder builder2 = new RadioButtonListBuilder(new NewReportCompareType(), this.rblCompareTypeGroup, this.languageComponent1);
                RadioButtonListBuilder builder3 = new RadioButtonListBuilder(new NewReportCompleteType(), this.rblCompleteTypeGroup, this.languageComponent1);
                RadioButtonListBuilder builder4 = new RadioButtonListBuilder(new NewReportExceptionOrDuty(), this.rblExceptionOrDuty, this.languageComponent1);

                builder1.Build();
                builder2.Build();
                builder3.Build();
                builder4.Build();

                SetByTime();
                ClearEmptyPanels();

                ReportPageHelper.SetControlValue(this, this.Request.Params);

                //this.chbCompareGroup.Attributes["onclick"] = "onCheckChange('" + this.chbCompareGroup.ClientID + "');";
            }

            RadioButtonListBuilder.FormatListControlStyle(this.rblByTimeTypeGroup, 30);
            RadioButtonListBuilder.FormatListControlStyle(this.rblCompareTypeGroup, 40);
            RadioButtonListBuilder.FormatListControlStyle(this.rblCompleteTypeGroup, 30);
            RadioButtonListBuilder.FormatListControlStyle(this.rblExceptionOrDuty, 50);

            this.rblCompareTypeGroup.Enabled = this.chbCompareGroup.Checked;
        }

        #region 页面需要的公共部分

        protected LanguageComponent languageComponent1 = null;
        protected IDomainDataProvider dataProvider = null;

        public void InitUserControl(LanguageComponent languageComponent, IDomainDataProvider dataProvider)
        {
            this.languageComponent1 = languageComponent;
            this.dataProvider = dataProvider;
        }

        #endregion

        #region 设定各个控件是否可见及相关的函数

        private bool _ShowPeriod = true;
        private bool _ShowShift = true;
        private bool _ShowShiftDay = true;
        private bool _ShowWeek = true;
        private bool _ShowMonth = true;
        private bool _ShowYear = true;

        public bool ShowPeriod
        {
            get { return _ShowPeriod; }
            set { _ShowPeriod = value; }
        }

        public bool ShowShift
        {
            get { return _ShowShift; }
            set { _ShowShift = value; }
        }

        public bool ShowShiftDay
        {
            get { return _ShowShiftDay; }
            set { _ShowShiftDay = value; }
        }

        public bool ShowWeek
        {
            get { return _ShowWeek; }
            set { _ShowWeek = value; }
        }

        public bool ShowMonth
        {
            get { return _ShowMonth; }
            set { _ShowMonth = value; }
        }

        public bool ShowYear
        {
            get { return _ShowYear; }
            set { _ShowYear = value; }
        }

        public bool ShowByTimePanel
        {
            get { return this.PanelByTime.Visible; }
            set { this.PanelByTime.Visible = value; }
        }

        public bool ShowCompareTypePanel
        {
            get { return this.PanelByCompareType.Visible; }
            set { this.PanelByCompareType.Visible = value; }
        }

        public bool ShowCompleteTypePanel
        {
            get { return this.PanelByCompleteType.Visible; }
            set { this.PanelByCompleteType.Visible = value; }
        }

        public bool ShowExceptionOrDuty
        {
            get { return this.rblExceptionOrDuty.Visible; }
            set { this.rblExceptionOrDuty.Visible = value; }
        }

        public bool ShowManHourPanel
        {
            get { return this.PanelManHour.Visible; }
            set { this.PanelManHour.Visible = value; }
        }

        public bool ShowBigSSCodeRequired
        {
            get { return this.chbBigSSCodeGroupRequired.Visible; }
            set { this.chbBigSSCodeGroupRequired.Visible = value; }
        }

        public bool ShowSegCodeRequired
        {
            get { return this.chbSegCodeGroupRequired.Visible; }
            set { this.chbSegCodeGroupRequired.Visible = value; }
        }

        public bool ShowSSCodeRequired
        {
            get { return this.chbSSCodeGroupRequired.Visible; }
            set { this.chbSSCodeGroupRequired.Visible = value; }
        }

        public bool ShowOPCodeRequired
        {
            get { return this.chbOPCodeGroupRequired.Visible; }
            set { this.chbOPCodeGroupRequired.Visible = value; }
        }

        public bool ShowResCodeRequired
        {
            get { return this.chbResCodeGroupRequired.Visible; }
            set { this.chbResCodeGroupRequired.Visible = value; }
        }

        public bool ShowFacCode
        {
            get { return this.chbFacCodeGroup.Visible; }
            set { this.chbFacCodeGroup.Visible = value; }
        }

        public bool ShowBigSSCode
        {
            get { return this.chbBigSSCodeGroup.Visible; }
            set { this.chbBigSSCodeGroup.Visible = value; }
        }

        public bool ShowSegCode
        {
            get { return this.chbSegCodeGroup.Visible; }
            set { this.chbSegCodeGroup.Visible = value; }
        }

        public bool ShowSSCode
        {
            get { return this.chbSSCodeGroup.Visible; }
            set { this.chbSSCodeGroup.Visible = value; }
        }

        public bool ShowOPCode
        {
            get { return this.chbOPCodeGroup.Visible; }
            set { this.chbOPCodeGroup.Visible = value; }
        }

        public bool ShowResCode
        {
            get { return this.chbResCodeGroup.Visible; }
            set { this.chbResCodeGroup.Visible = value; }
        }

        public bool ShowMaterialCode
        {
            get { return this.chbMaterialCodeGroup.Visible; }
            set { this.chbMaterialCodeGroup.Visible = value; }
        }

        public bool ShowGoodSemiGood
        {
            get { return this.chbGoodSemiGoodGroup.Visible; }
            set { this.chbGoodSemiGoodGroup.Visible = value; }
        }

        public bool ShowItemCode
        {
            get { return this.chbItemCodeGroup.Visible; }
            set { this.chbItemCodeGroup.Visible = value; }
        }

        public bool ShowMaterialModelCode
        {
            get { return this.chbMaterialModelCodeGroup.Visible; }
            set { this.chbMaterialModelCodeGroup.Visible = value; }
        }

        public bool ShowMaterialMachineType
        {
            get { return this.chbMaterialMachineTypeGroup.Visible; }
            set { this.chbMaterialMachineTypeGroup.Visible = value; }
        }

        public bool ShowMaterialExportImport
        {
            get { return this.chbMaterialExportImportGroup.Visible; }
            set { this.chbMaterialExportImportGroup.Visible = value; }
        }

        public bool ShowLotNo
        {
            get { return this.chbLotNoGroup.Visible; }
            set { this.chbLotNoGroup.Visible = value; }
        }

        public bool ShowProductionType
        {
            get { return this.chbProductionTypeGroup.Visible; }
            set { this.chbProductionTypeGroup.Visible = value; }
        }

        public bool ShowOQCLotType
        {
            get { return this.chbOQCLotTypeGroup.Visible; }
            set { this.chbOQCLotTypeGroup.Visible = value; }
        }

        public bool ShowMOCode
        {
            get { return this.chbMOCodeGroup.Visible; }
            set { this.chbMOCodeGroup.Visible = value; }
        }

        public bool ShowMOMemo
        {
            get { return this.chbMOMemoGroup.Visible; }
            set { this.chbMOMemoGroup.Visible = value; }
        }

        public bool ShowNewMass
        {
            get { return this.chbNewMassGroup.Visible; }
            set { this.chbNewMassGroup.Visible = value; }
        }

        public bool ShowCrewCode
        {
            get { return this.chbCrewCodeGroup.Visible; }
            set { this.chbCrewCodeGroup.Visible = value; }
        }

        public bool ShowFirstClass
        {
            get { return this.chbFirstClassGroup.Visible; }
            set { this.chbFirstClassGroup.Visible = value; }
        }

        public bool ShowSecondClass
        {
            get { return this.chbSecondClassGroup.Visible; }
            set { this.chbSecondClassGroup.Visible = value; }
        }

        public bool ShowThirdClass
        {
            get { return this.chbThirdClassGroup.Visible; }
            set { this.chbThirdClassGroup.Visible = value; }
        }

        public bool ShowExceptionCode
        {
            get { return this.chbExceptionCodeGroup.Visible; }
            set { this.chbExceptionCodeGroup.Visible = value; }
        }

        public bool ShowIncludeIndirectManHour
        {
            get { return this.chbIncludeIndirectManHour.Visible; }
            set { this.chbIncludeIndirectManHour.Visible = value; }
        }

        public bool ShowInspector
        {
            get { return this.chbInspectorGroup.Visible; }
            set { this.chbInspectorGroup.Visible = value; }
        }

        public bool ShowIQCItemType
        {
            get { return this.chbIQCItemTypeWhere.Visible; }
            set { this.chbIQCItemTypeWhere.Visible = value; }
        }

        public bool ShowIQCLineItemType
        {
            get { return this.chbIQCLineItemTypeWhere.Visible; }
            set { this.chbIQCLineItemTypeWhere.Visible = value; }
        }

        public bool ShowConcession
        {
            get { return this.chbConcessionWhere.Visible; }
            set { this.chbConcessionWhere.Visible = value; }
        }

        public bool ShowRoHS
        {
            get { return this.chbRoHSWhere.Visible; }
            set { this.chbRoHSWhere.Visible = value; }
        }

        public bool ShowVendorCode
        {
            get { return this.chbVendorCodeWhere.Visible; }
            set { this.chbVendorCodeWhere.Visible = value; }
        }

        public bool ShowItemType
        {
            get { return this.chbItemTypeGroup.Visible; }
            set { this.chbItemTypeGroup.Visible = value; }
        }

        public bool ShowPorjectName
        {
            get { return this.chbProjectName.Visible; }
            set { this.chbProjectName.Visible = value; }
        }

        public bool ShowSp1
        {
            get { return this.lblSplitter1.Visible; }
            set { this.lblSplitter1.Visible = value; }
        }

        public bool ShowSp2
        {
            get { return this.lblSplitter2.Visible; }
            set { this.lblSplitter2.Visible = value; }
        }

        public bool ShowSp3
        {
            get { return this.lblSplitter3.Visible; }
            set { this.lblSplitter3.Visible = value; }
        }

        private void SetByTime()
        {
            if (!_ShowPeriod)
            {
                ListItem item = this.rblByTimeTypeGroup.Items.FindByValue(NewReportByTimeType.Period);
                if (item != null)
                {
                    this.rblByTimeTypeGroup.Items.Remove(item);
                }
            }

            if (!_ShowShift)
            {
                ListItem item = this.rblByTimeTypeGroup.Items.FindByValue(NewReportByTimeType.Shift);
                if (item != null)
                {
                    this.rblByTimeTypeGroup.Items.Remove(item);
                }
            }

            if (!_ShowShiftDay)
            {
                ListItem item = this.rblByTimeTypeGroup.Items.FindByValue(NewReportByTimeType.ShiftDay);
                if (item != null)
                {
                    this.rblByTimeTypeGroup.Items.Remove(item);
                }
            }

            if (!_ShowWeek)
            {
                ListItem item = this.rblByTimeTypeGroup.Items.FindByValue(NewReportByTimeType.Week);
                if (item != null)
                {
                    this.rblByTimeTypeGroup.Items.Remove(item);
                }
            }

            if (!_ShowMonth)
            {
                ListItem item = this.rblByTimeTypeGroup.Items.FindByValue(NewReportByTimeType.Month);
                if (item != null)
                {
                    this.rblByTimeTypeGroup.Items.Remove(item);
                }
            }

            if (!_ShowYear)
            {
                ListItem item = this.rblByTimeTypeGroup.Items.FindByValue(NewReportByTimeType.Year);
                if (item != null)
                {
                    this.rblByTimeTypeGroup.Items.Remove(item);
                }
            }

            if (this.rblByTimeTypeGroup.Items.Count > 0)
            {
                this.rblByTimeTypeGroup.SelectedIndex = 0;
            }
        }

        private void ClearEmptyPanels()
        {
            if (this.rblByTimeTypeGroup.Items.Count <= 0)
            {
                this.PanelByTime.Visible = false;
            }
            else if (this.PanelByTime.Visible == false)
            {
                this.rblByTimeTypeGroup.Items.Clear();
            }

            if (!this.chbBigSSCodeGroupRequired.Visible
                && !this.chbSegCodeGroupRequired.Visible
                && !this.chbSSCodeGroupRequired.Visible
                && !this.chbOPCodeGroupRequired.Visible
                && !this.chbResCodeGroupRequired.Visible)
            {
                this.PanelByConditions0.Visible = false;
            }
        }

        private void HideAllChild()
        {
            this.ShowPeriod = false;
            this.ShowShift = false;
            this.ShowShiftDay = false;
            this.ShowWeek = false;
            this.ShowMonth = false;
            this.ShowYear = false;

            this.ShowByTimePanel = false;
            this.ShowCompareTypePanel = false;
            this.ShowCompleteTypePanel = false;
            this.ShowManHourPanel = false;

            this.ShowBigSSCodeRequired = false;
            this.ShowSegCodeRequired = false;
            this.ShowSSCodeRequired = false;
            this.ShowOPCodeRequired = false;
            this.ShowResCodeRequired = false;

            this.ShowFacCode = false;
            this.ShowBigSSCode = false;
            this.ShowSegCode = false;
            this.ShowSSCode = false;
            this.ShowOPCode = false;
            this.ShowResCode = false;
            this.ShowMaterialCode = false;
            this.ShowExceptionCode = false;

            this.ShowGoodSemiGood = false;
            this.ShowItemCode = false;
            this.ShowItemType = false;
            this.ShowMaterialModelCode = false;
            this.ShowMaterialMachineType = false;
            this.ShowMaterialExportImport = false;
            this.ShowLotNo = false;
            this.ShowProductionType = false;
            this.ShowOQCLotType = false;
            this.ShowMOCode = false;
            this.ShowMOMemo = false;
            this.ShowNewMass = false;
            this.ShowCrewCode = false;
            this.ShowPorjectName = false;

            this.ShowExceptionOrDuty = false;

            this.ShowFirstClass = false;
            this.ShowSecondClass = false;
            this.ShowThirdClass = false;

            this.ShowInspector = false;
            this.ShowIQCItemType = false;
            this.ShowIQCLineItemType = false;
            this.ShowRoHS = false;
            this.ShowConcession = false;
            this.ShowVendorCode = false;

            this.ShowSp1 = false;
            this.ShowSp2 = false;
            this.ShowSp3 = false;
        }

        #endregion

        #region 通过设定自动获得Group字段相关部分

        private void InitControlDBInfo()
        {
            _DBInfoX = new List<ControlGroupInfo>();
            _DBInfoY = new List<ControlGroupInfo>();

            //**表示产生报表的三个核心Table
            List<DBFieldInfo> tableFieldList = null;


            //按条件（必选）
            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.bigsscode", "bigsscode"));
            tableFieldList.Add(new DBFieldInfo("**.bigsscode", "bigsscode"));
            _DBInfoY.Add(new ControlGroupInfo(chbBigSSCodeGroupRequired, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.segcode || ' - ' || tblseg.segdesc", "segcode"));
            tableFieldList.Add(new DBFieldInfo("**.segcode || ' - ' || tblseg.segdesc", "segcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbSegCodeGroupRequired, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.sscode || ' - ' || tblss.ssdesc", "sscode"));
            tableFieldList.Add(new DBFieldInfo("**.sscode || ' - ' || tblss.ssdesc", "sscode"));
            _DBInfoY.Add(new ControlGroupInfo(chbSSCodeGroupRequired, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.opcode || ' - ' || tblop.opdesc", "opcode"));
            tableFieldList.Add(new DBFieldInfo("**.opcode || ' - ' || tblop.opdesc", "opcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbOPCodeGroupRequired, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.rescode || ' - ' || tblres.resdesc", "rescode"));
            tableFieldList.Add(new DBFieldInfo("**.rescode || ' - ' || tblres.resdesc", "rescode"));
            _DBInfoY.Add(new ControlGroupInfo(chbResCodeGroupRequired, "true", tableFieldList));


            //按条件
            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.bigsscode", "bigsscode"));
            tableFieldList.Add(new DBFieldInfo("**.bigsscode", "bigsscode"));
            _DBInfoY.Add(new ControlGroupInfo(chbBigSSCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.segcode || ' - ' || tblseg.segdesc", "segcode"));
            tableFieldList.Add(new DBFieldInfo("**.segcode || ' - ' || tblseg.segdesc", "segcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbSegCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.sscode || ' - ' || tblss.ssdesc", "sscode"));
            tableFieldList.Add(new DBFieldInfo("**.sscode || ' - ' || tblss.ssdesc", "sscode"));
            _DBInfoY.Add(new ControlGroupInfo(chbSSCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.opcode || ' - ' || tblop.opdesc", "opcode"));
            tableFieldList.Add(new DBFieldInfo("**.opcode || ' - ' || tblop.opdesc", "opcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbOPCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.rescode || ' - ' || tblres.resdesc", "rescode"));
            tableFieldList.Add(new DBFieldInfo("**.rescode || ' - ' || tblres.resdesc", "rescode"));
            _DBInfoY.Add(new ControlGroupInfo(chbResCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.faccode || ' - ' || tblfactory.facdesc", "faccode"));
            tableFieldList.Add(new DBFieldInfo("**.faccode || ' - ' || tblfactory.facdesc", "faccode"));
            _DBInfoY.Add(new ControlGroupInfo(chbFacCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmaterial.mtype", "mtype"));
            tableFieldList.Add(new DBFieldInfo("**.mtype", "mtype"));
            _DBInfoY.Add(new ControlGroupInfo(chbGoodSemiGoodGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmo.momemo", "momemo"));
            _DBInfoY.Add(new ControlGroupInfo(chbMOMemoGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblsysparam_momemo.paramvalue", "newmass"));
            _DBInfoY.Add(new ControlGroupInfo(chbNewMassGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.itemcode || ' - ' || tblmaterial.mdesc", "itemcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbItemCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmaterial.mmodelcode", "mmodelcode"));
            tableFieldList.Add(new DBFieldInfo("**.mmodelcode", "mmodelcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbMaterialModelCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            //tableFieldList.Add(new DBFieldInfo("tblproject.projectname", "projectname"));
            tableFieldList.Add(new DBFieldInfo("**.projectcode", "projectcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbProjectName, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmaterial.mmachinetype", "mmachinetype"));
            tableFieldList.Add(new DBFieldInfo("**.mmachinetype", "mmachinetype"));
            _DBInfoY.Add(new ControlGroupInfo(chbMaterialMachineTypeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmaterial.mexportimport", "mexportimport"));
            tableFieldList.Add(new DBFieldInfo("**.mexportimport", "mexportimport"));
            _DBInfoY.Add(new ControlGroupInfo(chbMaterialExportImportGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.mocode", "mocode"));
            _DBInfoY.Add(new ControlGroupInfo(chbMOCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblline2crew.crewcode || ' - ' || tblcrew.crewdesc", "crewcode"));
            tableFieldList.Add(new DBFieldInfo("**.crewcode || ' - ' || tblcrew.crewdesc", "crewcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbCrewCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblitemclass.firstclass", "firstclass"));
            tableFieldList.Add(new DBFieldInfo("**.firstclass", "firstclass"));
            _DBInfoY.Add(new ControlGroupInfo(chbFirstClassGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblitemclass.secondclass", "secondclass"));
            tableFieldList.Add(new DBFieldInfo("**.secondclass", "secondclass"));
            _DBInfoY.Add(new ControlGroupInfo(chbSecondClassGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblitemclass.thirdclass", "thirdclass"));
            tableFieldList.Add(new DBFieldInfo("**.thirdclass", "thirdclass"));
            _DBInfoY.Add(new ControlGroupInfo(chbThirdClassGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.lotno", "lotno"));
            _DBInfoY.Add(new ControlGroupInfo(chbLotNoGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tbllot.productiontype", "productiontype"));
            _DBInfoY.Add(new ControlGroupInfo(chbProductionTypeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tbllot.oqclottype", "oqclottype"));
            _DBInfoY.Add(new ControlGroupInfo(chbOQCLotTypeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tbllot.oqclottype", "oqclottype"));
            _DBInfoY.Add(new ControlGroupInfo(chbOQCLotTypeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.Inspector || ' - ' || tbluser.username", "InspectorAndName"));
            _DBInfoY.Add(new ControlGroupInfo(chbInspectorGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.iqclineitemtype", "iqclineitemtype"));
            _DBInfoY.Add(new ControlGroupInfo(chbIQCLineItemTypeWhere, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.iqcitemtype", "iqcitemtype"));
            _DBInfoY.Add(new ControlGroupInfo(chbIQCItemTypeWhere, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.itemtype", "itemtype"));
            _DBInfoY.Add(new ControlGroupInfo(chbItemTypeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmaterial.rohs", "rohs"));
            _DBInfoY.Add(new ControlGroupInfo(chbRoHSWhere, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.concessionstatus", "concessionstatus"));
            _DBInfoY.Add(new ControlGroupInfo(chbConcessionWhere, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.vendorcode || ' - ' || tblvendor.vendorname", "vendorcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbVendorCodeWhere, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.materialcode", "materialcode"));
            _DBInfoY.Add(new ControlGroupInfo(chbMaterialCodeGroup, "true", tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.tpcode || ' - ' || tbltp.tpdesc", "tpcode"));
            tableFieldList.Add(new DBFieldInfo("**.tpcode || ' - ' || tbltp.tpdesc", "tpcode"));
            _DBInfoX.Add(new ControlGroupInfo(rblByTimeTypeGroup, NewReportByTimeType.Period, tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tblmesentitylist.shiftcode || ' - ' || tblshift.shiftdesc", "shiftcode"));
            tableFieldList.Add(new DBFieldInfo("**.shiftcode || ' - ' || tblshift.shiftdesc", "shiftcode"));
            _DBInfoX.Add(new ControlGroupInfo(rblByTimeTypeGroup, NewReportByTimeType.Shift, tableFieldList));
          

            //按时间
            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("**.shiftday", "shiftday"));
            _DBInfoX.Add(new ControlGroupInfo(rblByTimeTypeGroup, NewReportByTimeType.ShiftDay, tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tbltimedimension.dweek", "dweek"));
            tableFieldList.Add(new DBFieldInfo("**.dweek", "dweek"));
            _DBInfoX.Add(new ControlGroupInfo(rblByTimeTypeGroup, NewReportByTimeType.Week, tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tbltimedimension.dmonth", "dmonth"));
            tableFieldList.Add(new DBFieldInfo("**.dmonth", "dmonth"));
            _DBInfoX.Add(new ControlGroupInfo(rblByTimeTypeGroup, NewReportByTimeType.Month, tableFieldList));

            tableFieldList = new List<DBFieldInfo>();
            tableFieldList.Add(new DBFieldInfo("tbltimedimension.year", "year"));
            tableFieldList.Add(new DBFieldInfo("**.year", "year"));
            _DBInfoX.Add(new ControlGroupInfo(rblByTimeTypeGroup, NewReportByTimeType.Year, tableFieldList));
        }

        private string GetControlValue(Control control)
        {
            string returnValue = string.Empty;

            if (control.Visible)
            {
                if (control.GetType() == typeof(CheckBox))
                {
                    returnValue = ((CheckBox)control).Checked.ToString().Trim();
                }
                else if (control.GetType() == typeof(RadioButtonList))
                {
                    if (((RadioButtonList)control).Items.Count > 0)
                    {
                        returnValue = ((RadioButtonList)control).SelectedValue.Trim();
                    }
                }
            }

            return returnValue.Trim();
        }

        private string GetGroupFieldList(string preferredTableNameList, string dim, bool withExpression, bool withAlias)
        {
            string fieldList = string.Empty;
            string tableList = ",";
            List<string> tableNameList = new List<string>();
            string text = string.Empty;

            List<ControlGroupInfo> dbInfo = _DBInfoX;
            if (dim.Trim().ToLower() == "y")
            {
                dbInfo = _DBInfoY;
            }

            if (preferredTableNameList.Trim().Length > 0)
            {
                tableList += preferredTableNameList.Trim().ToLower() + ",";
            }

            //第一遍扫描，处理那些对应数据库字段固定的控件
            foreach (ControlGroupInfo info in dbInfo)
            {
                if (info.TableFieldList.Count == 1)
                {
                    text = GetControlValue(info.Control).Trim();

                    if (text.Length > 0 && text.ToLower() == info.ControlValue.Trim().ToLower())
                    {
                        tableNameList = ExtractTableName(info.TableFieldList[0].FieldString.ToLower());
                        foreach (string table in tableNameList)
                        {
                            if (tableList.IndexOf("," + table + ",") < 0)
                            {
                                tableList += table + ",";
                            }
                        }

                        string oneField = string.Empty;
                        if (withExpression)
                        {
                            oneField += info.TableFieldList[0].FieldString.Trim().ToLower();
                        }
                        if (withAlias)
                        {
                            if (oneField.Trim().Length > 0)
                            {
                                oneField += " AS ";
                            }
                            oneField += info.TableFieldList[0].Alias.Trim().ToLower();
                        }

                        fieldList += oneField + ",";
                    }
                }
            }

            //第二遍扫描，处理那些对应数据库字段可多选的控件。选择第一个出现的Table
            foreach (ControlGroupInfo info in dbInfo)
            {
                if (info.TableFieldList.Count > 1)
                {
                    text = GetControlValue(info.Control).Trim();

                    if (text.Length > 0 && text.ToLower() == info.ControlValue.Trim().ToLower())
                    {
                        int pos = -1;
                        int lastPos = tableList.Length;
                        int usedIndex = -1;

                        for (int i = 0; i < info.TableFieldList.Count; i++)
                        {
                            tableNameList = ExtractTableName(info.TableFieldList[i].FieldString.ToLower());

                            if (tableNameList.Count > 0)
                            {
                                pos = tableList.IndexOf("," + tableNameList[0] + ",");
                                if (pos >= 0 && pos < lastPos)
                                {
                                    usedIndex = i;
                                    lastPos = pos;
                                }
                            }
                        }

                        if (usedIndex < 0)
                        {
                            usedIndex = 0;
                        }

                        tableNameList = ExtractTableName(info.TableFieldList[usedIndex].FieldString.ToLower());
                        foreach (string table in tableNameList)
                        {
                            if (tableList.IndexOf("," + table + ",") < 0)
                            {
                                tableList += table + ",";
                            }
                        }

                        string oneField = string.Empty;
                        if (withExpression)
                        {
                            oneField += info.TableFieldList[usedIndex].FieldString.Trim().ToLower();
                        }
                        if (withAlias)
                        {
                            if (oneField.Trim().Length > 0)
                            {
                                oneField += " AS ";
                            }
                            oneField += info.TableFieldList[usedIndex].Alias.Trim().ToLower();
                        }
                        fieldList += oneField + ",";
                    }
                }
            }

            if (fieldList.ToLower().IndexOf(".mocode as ") >= 0)
            {
                if (fieldList.ToLower().IndexOf("tblmo.orderno as ") < 0)
                {
                    fieldList += "tblmo.orderno AS orderno,";
                }
                if (fieldList.ToLower().IndexOf("tblmo.mobom as ") < 0)
                {
                    fieldList += "tblmo.mobom AS mobom,";
                }
            }
            else if (fieldList.ToLower().IndexOf("mocode,") == 0
                || fieldList.ToLower().IndexOf(",mocode,") >= 0)
            {
                if (fieldList.ToLower().IndexOf(",orderno,") < 0)
                {
                    fieldList += "orderno,";
                }
                if (fieldList.ToLower().IndexOf(",mobom,") < 0)
                {
                    fieldList += "mobom,";
                }
            }

            fieldList = fieldList.Trim();
            if (fieldList.Length > 0)
            {
                fieldList = fieldList.Substring(0, fieldList.Length - 1);
            }

            return fieldList;
        }

        public string GetGroupFieldAliasList(string preferredTableNameList, string dim)
        {
            return GetGroupFieldList(preferredTableNameList, dim, false, true);
        }

        public string GetGroupFieldList(string preferredTableNameList, string dim)
        {
            return GetGroupFieldList(preferredTableNameList, dim, true, true);
        }

        private List<string> ExtractTableName(string filedString)
        {
            List<string> returnValue = new List<string>();

            StringBuilder temp = new StringBuilder();
            bool inConstant = false;
            for (int i = 0; i < filedString.Length; i++)
            {
                if (filedString[i] == '\'')
                {
                    inConstant = !inConstant;
                }

                if (!inConstant && filedString[i] != '\'')
                {
                    temp.Append(filedString[i]);
                }
            }

            filedString = temp.ToString();

            int posEnd = filedString.IndexOf(".");
            int posBegin = -1;
            while (posEnd >= 0)
            {
                posBegin = filedString.Substring(0, posEnd).LastIndexOf(" ");
                if (posBegin >= 0)
                {
                    returnValue.Add(filedString.Substring(posBegin, posEnd - posBegin).Trim());
                }
                else
                {
                    returnValue.Add(filedString.Substring(0, posEnd).Trim());
                }

                if (posEnd >= filedString.Length - 1)
                {
                    break;
                }
                else
                {
                    posEnd = filedString.IndexOf(".", posEnd + 1);
                }
            }

            return returnValue;
        }

        #endregion

        #region 直接通过函数获得各个子控件的值，可以根据需要适当补充

        public bool BigSSChecked
        {
            get { return chbBigSSCodeGroup.Checked || chbBigSSCodeGroupRequired.Checked; }
        }

        public bool SegChecked
        {
            get { return chbSegCodeGroup.Checked || chbSegCodeGroupRequired.Checked; }
        }

        public bool SSChecked
        {
            get { return chbSSCodeGroup.Checked || chbSSCodeGroupRequired.Checked; }
        }

        public bool OPChecked
        {
            get { return chbOPCodeGroup.Checked || chbOPCodeGroupRequired.Checked; }
        }

        public bool ResChecked
        {
            get { return chbResCodeGroup.Checked || chbResCodeGroupRequired.Checked; }
        }

        public string UserSelectCompareType
        {
            get
            {
                if (chbCompareGroup.Checked)
                    return rblCompareTypeGroup.SelectedValue;
                else
                    return string.Empty;
            }
        }

        public string UserSelectCompleteType
        {
            get { return rblCompleteTypeGroup.SelectedValue; }
        }

        public string UserExceptionOrDuty
        {
            get { return rblExceptionOrDuty.SelectedValue; }
        }

        public string UserSelectByTimeType
        {
            get { return rblByTimeTypeGroup.SelectedValue; }
        }

        public bool FacCodeChecked
        {
            get { return chbFacCodeGroup.Checked; }
        }

        public bool ItemCodeChecked
        {
            get { return chbItemCodeGroup.Checked; }
        }

        public bool ExcludeLostManHourChecked
        {
            get { return chbExcludeLostManHour.Checked; }
        }

        public bool IncludeIndirectManHourChecked
        {
            get { return chbIncludeIndirectManHour.Checked; }
        }

        public bool ExcludeReworkOutputChecked
        {
            get { return chbExcludeReworkOutput.Checked; }
        }

        public bool ItemTypeCheckd
        {
            get { return chbItemTypeGroup.Checked; }
        }

        public bool ProjectCodeChecked
        {
            get { return chbProjectName.Checked; }
        }

        #endregion

        #region 获取被选择控件的字段
        public string GetCheckedColumnsString()
        {
            string returnValue = string.Empty;
            if (chbBigSSCodeGroupRequired.Checked)
            {
                returnValue += "BigSSCode,";
            }

            if (chbSegCodeGroupRequired.Checked)
            {
                returnValue += "SegCode,";
            }

            if (chbSSCodeGroupRequired.Checked)
            {
                returnValue += "SSCode,";
            }

            if (chbOPCodeGroupRequired.Checked)
            {
                returnValue += "OPCode,";
            }

            if (chbResCodeGroupRequired.Checked)
            {
                returnValue += "ResCode,";
            }

            if (chbGoodSemiGoodGroup.Checked)
            {
                returnValue += "GoodSemiGood,";
            }

            if (chbInspectorGroup.Checked)
            {
                returnValue += "InspectorAndName,";
            }

            if (chbItemCodeGroup.Checked)
            {
                returnValue += "ItemCode,";
            }

            if (chbMaterialModelCodeGroup.Checked)
            {
                returnValue += "MaterialModelCode,";
            }

            if (chbMaterialMachineTypeGroup.Checked)
            {
                returnValue += "MaterialMachineType,";
            }

            if (chbMaterialExportImportGroup.Checked)
            {
                returnValue += "MaterialExportImport,";
            }

            if (chbLotNoGroup.Checked)
            {
                returnValue += "LotNo,";
            }

            if (chbProductionTypeGroup.Checked)
            {
                returnValue += "ProductionType,";
            }

            if (chbOQCLotTypeGroup.Checked)
            {
                returnValue += "OQCLotType,";
            }

            if (chbMOCodeGroup.Checked)
            {
                returnValue += "MOCode,";
            }

            if (chbMOMemoGroup.Checked)
            {
                returnValue += "MOMemo,";
            }

            if (chbNewMassGroup.Checked)
            {
                returnValue += "NewMass,";
            }

            if (chbCrewCodeGroup.Checked)
            {
                returnValue += "CrewCode,";
            }

            if (chbFirstClassGroup.Checked)
            {
                returnValue += "FirstClass,";
            }

            if (chbSecondClassGroup.Checked)
            {
                returnValue += "SecondClass,";
            }

            if (chbThirdClassGroup.Checked)
            {
                returnValue += "ThirdClass,";
            }

            if (chbFacCodeGroup.Checked)
            {
                returnValue += "FacCode,";
            }

            if (chbBigSSCodeGroup.Checked)
            {
                returnValue += "BigSSCode,";
            }

            if (chbSegCodeGroup.Checked)
            {
                returnValue += "SegCode,";
            }

            if (chbSSCodeGroup.Checked)
            {
                returnValue += "SSCode,";
            }

            if (chbOPCodeGroup.Checked)
            {
                returnValue += "OPCode,";
            }

            if (chbResCodeGroup.Checked)
            {
                returnValue += "ResCode,";
            }

            if (chbExceptionCodeGroup.Checked)
            {
                returnValue += "ExceptionCode,";
            }

            if (chbIQCItemTypeWhere.Checked)
            {
                returnValue += "IQCItemType,";
            }

            if (chbIQCLineItemTypeWhere.Checked)
            {
                returnValue += "IQCLineItemType,";
            }

            if (chbRoHSWhere.Checked)
            {
                returnValue += "Rohs,";
            }

            if (chbConcessionWhere.Checked)
            {
                returnValue += "ConcessionStatus,";
            }

            if (chbVendorCodeWhere.Checked)
            {
                returnValue += "VendorCode,";
            }

            if (chbItemTypeGroup.Checked)
            {
                returnValue += "ItemType,";
            }

            if (chbMaterialCodeGroup.Checked)
            {
                returnValue += "MaterialCode,";
            }

            if (chbProjectName.Checked)
            {
                returnValue += "ProjectName,";
            }

            if (returnValue.Length > 0)
            {
                returnValue = returnValue.Substring(0, returnValue.Length - 1);
            }

            return returnValue;
        }
        #endregion

    }
}