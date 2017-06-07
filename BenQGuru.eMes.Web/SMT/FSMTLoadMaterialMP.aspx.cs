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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.SMT
{
    /// <summary>
    /// FFeederMP 的摘要说明。
    /// </summary>
    public partial class FSMTLoadMaterialMP : BaseMPageNew
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        private BenQGuru.eMES.SMT.SMTFacade _facade;//= new SMTFacadeFactory().Create();

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

            this.txtSSCodeEdit.TextBox.TextChanged += new EventHandler(TextBox_TextChanged);

        }
        #endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                this.InitPageLanguage(this.languageComponent1, false);
                InitDropDownList();
                InitDropDownListFeeder();
            }

        }

        private void InitDropDownList()
        {
            this.txtTableEdit.Items.Add("A");
            this.txtTableEdit.Items.Add("B");
        }

        private void InitDropDownListFeeder()
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            object[] feederTypeList = this._facade.GetAllFeederSpec();
            this.DropDownListFeederSpecCodeEdit.Items.Add(new ListItem(string.Empty, string.Empty));
            if (feederTypeList != null && feederTypeList.Length > 0)
            {
                foreach (FeederSpec item in feederTypeList)
                {
                    this.DropDownListFeederSpecCodeEdit.Items.Add(new ListItem(item.Name, item.FeederSpecCode));
                }
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
            this.gridHelper.AddColumn("ProductCode", "产品代码", null);
            this.gridHelper.AddColumn("sscode", "产线代码", null);
            this.gridHelper.AddColumn("MachineCode", "机台编码", null);
            this.gridHelper.AddColumn("MachineItemCode", "站位编码", null);
            this.gridHelper.AddColumn("SourceMaterialCode", "主物料号", null);
            this.gridHelper.AddColumn("MaterialCode", "替代物料号", null);
            this.gridHelper.AddColumn("FeederSpecCode", "Feeder规格", null);
            this.gridHelper.AddColumn("Qty", "数量", HorizontalAlign.Right);
            this.gridHelper.AddColumn("Table", "Table", null);
            //this.gridWebGrid.Columns.FromKey("Qty").Format = "#,#";
            //this.gridWebGrid.Columns.FromKey("Qty").CellStyle.HorizontalAlign = HorizontalAlign.Right;

            this.gridHelper.AddDefaultColumn(true, false);
            this.gridHelper.ApplyLanguage(this.languageComponent1);
        }

        protected override DataRow GetGridRow(object obj)
        {
            SMTFeederMaterial item = (SMTFeederMaterial)obj;
            //return new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
            //    new object[]{
            //                    "",
            //                    item.ProductCode,
            //                    item.StepSequenceCode,
            //                    item.MachineCode,
            //                    item.MachineStationCode,
            //                    item.SourceMaterialCode,
            //                    item.MaterialCode,
            //                    item.FeederSpecCode,
            //                    item.Qty,
            //                    item.TableGroup
            //                });
            DataRow row = this.DtSource.NewRow();
            row["ProductCode"] = item.ProductCode;
            row["sscode"] = item.StepSequenceCode;
            row["MachineCode"] = item.MachineCode;
            row["MachineItemCode"] = item.MachineStationCode;
            row["SourceMaterialCode"] = item.SourceMaterialCode;
            row["MaterialCode"] = item.MaterialCode;
            row["FeederSpecCode"] = item.FeederSpecCode;
            row["Qty"] = item.Qty;
            row["Table"] = item.TableGroup;
            return row;
        }

        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            return this._facade.QuerySMTFeederMaterial(
                this.txtItemCodeQuery.Text.Trim().ToUpper(),
                this.txtSSCodeQuery.Text.Trim().ToUpper(),
                this.txtMachineCodeQuery.Text.Trim().ToUpper(),
                string.Empty,
                string.Empty,
                inclusive, exclusive);
        }

        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            return this._facade.QuerySMTFeederMaterialCount(
                this.txtItemCodeQuery.Text.Trim().ToUpper(),
                this.txtSSCodeQuery.Text.Trim().ToUpper(),
                this.txtMachineCodeQuery.Text.Trim().ToUpper(),
                string.Empty,
                string.Empty);
        }

        #endregion

        private void TextBox_TextChanged(object e, EventArgs args)
        {
            string ssCode = this.txtSSCodeEdit.Text.Trim();

        }

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }

            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            try
            {
                
                //判断数据的合法性
                ArrayList list = CheckItem(domainObject);
                if (list.Count == 0)
                {
                    WebInfoPublish.Publish(this, "$Error_DataMaterial_Right", this.languageComponent1);
                    return;
                }
                
                SMTFeederMaterial item = list[0] as SMTFeederMaterial;
                if (item.EAttribute1 != "True")
                {
                    WebInfoPublish.Publish(this, item.EAttribute1, this.languageComponent1);
                    return;
                }

                this.DataProvider.BeginTransaction();
                //根据产品代码+产线代码得到TBLSMTFEEDERMATERIALIMPLOG最大logno
                object obj = _facade.GetSMTFeederMaterialOfMaxLogNo((domainObject as SMTFeederMaterial).ProductCode, (domainObject as SMTFeederMaterial).StepSequenceCode);
                if (obj == null || (obj as SMTFeederMaterialImportLog).LOGNO < 1)
                {

                    _facade.UpdateMachineFeeder(item.ProductCode, item.StepSequenceCode);

                    _facade.AddSMTFeederMaterial(item);

                    decimal logNo = 1;

                    object objMaxLotNo = _facade.GetMaxLotNoSmtFeedermateriaLog();
                    if (objMaxLotNo != null)
                    {
                        logNo = (objMaxLotNo as SMTFeederMaterialImportLog).LOGNO + 1;
                    }
                    //_facade.ImportSMTFeederMaterial(new object[] { domainObject }, this.GetUserCode());
                    // 加入Log
                    SMTFeederMaterialImportLog log = new SMTFeederMaterialImportLog();
                    log.LOGNO = logNo;
                    log.Sequence = 1;
                    log.ImportUser = this.GetUserCode();
                    log.ImportDate = dbDateTime.DBDate;
                    log.ImportTime = dbDateTime.DBTime;
                    log.CheckResult = FormatHelper.BooleanToString(true);
                    if (Convert.ToBoolean(item.EAttribute1.Split(':')[0]) == false)
                    {
                        log.CheckResult = FormatHelper.BooleanToString(false);
                        log.CheckDescription = item.EAttribute1.Split(':')[1];
                    }
                    log.MachineCode = item.MachineCode;
                    log.MachineStationCode = item.MachineStationCode;
                    log.ProductCode = item.ProductCode;
                    log.StepSequenceCode = item.StepSequenceCode;
                    log.MaterialCode = item.MaterialCode;
                    log.SourceMaterialCode = item.SourceMaterialCode;
                    log.FeederSpecCode = item.FeederSpecCode;
                    log.Qty = item.Qty;
                    log.TableGroup = item.TableGroup;
                    log.MaintainUser = item.MaintainUser;
                    log.MaintainDate = item.MaintainDate;
                    log.MaintainTime = item.MaintainTime;
                    _facade.AddSMTFeederMaterialImportLog(log);
                }
                else
                {
                    object[] objs = _facade.QuerySMTFeederMaterialImportLog((obj as SMTFeederMaterialImportLog).LOGNO, item.ProductCode, item.StepSequenceCode, item.MachineCode, item.MachineStationCode);
                    if (objs != null && objs.Length > 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "$Message_FeederMaterialData_Exist", this.languageComponent1);
                        return;
                    }
                    else
                    {
                        _facade.UpdateMachineFeeder(item.ProductCode, item.StepSequenceCode);

                        _facade.AddSMTFeederMaterial(item);

                        //_facade.ImportSMTFeederMaterial(new object[] { domainObject }, this.GetUserCode());
                        // 加入Log

                        decimal sequence = 1;
                        object objMaxSeq = _facade.GetMaxSeqSmtFeederMaterialLog((obj as SMTFeederMaterialImportLog).LOGNO);
                        if (objMaxSeq != null)
                        {
                            sequence = (objMaxSeq as SMTFeederMaterialImportLog).Sequence+1;
                        }

                        SMTFeederMaterialImportLog log = new SMTFeederMaterialImportLog();
                        log.LOGNO = (obj as SMTFeederMaterialImportLog).LOGNO;
                        log.Sequence = sequence;
                        log.ImportUser = this.GetUserCode();
                        log.ImportDate = dbDateTime.DBDate;
                        log.ImportTime = dbDateTime.DBTime;
                        log.CheckResult = FormatHelper.BooleanToString(true);
                        if (Convert.ToBoolean(item.EAttribute1.Split(':')[0]) == false)
                        {
                            log.CheckResult = FormatHelper.BooleanToString(false);
                            log.CheckDescription = item.EAttribute1.Split(':')[1];
                        }
                        log.MachineCode = item.MachineCode;
                        log.MachineStationCode = item.MachineStationCode;
                        log.ProductCode = item.ProductCode;
                        log.StepSequenceCode = item.StepSequenceCode;
                        log.MaterialCode = item.MaterialCode;
                        log.SourceMaterialCode = item.SourceMaterialCode;
                        log.FeederSpecCode = item.FeederSpecCode;
                        log.Qty = item.Qty;
                        log.TableGroup = item.TableGroup;
                        log.MaintainUser = item.MaintainUser;
                        log.MaintainDate = item.MaintainDate;
                        log.MaintainTime = item.MaintainTime;
                        _facade.AddSMTFeederMaterialImportLog(log);
                    }
                }
                this.DataProvider.CommitTransaction();

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, "$Error_Add_FeederMaterial", this.languageComponent1);
            }
        }



        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                //this.txtReelNoEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                //this.txtReelNoEdit.ReadOnly = true;
            }
        }
        #endregion

        #region Object <--> Page

        protected override object GetEditObject()
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            SMTFeederMaterial smtFeederMaterial = this._facade.CreateNewSMTFeederMaterial();

            smtFeederMaterial.MaterialCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMaterialCodeEdit.Text));
            smtFeederMaterial.ProductCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeEdit.Text));
            smtFeederMaterial.StepSequenceCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSSCodeEdit.Text));
            smtFeederMaterial.MachineCode = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMachineCodeEdit.Text));
            smtFeederMaterial.MachineStationCode = this.txtMachineStationCodeEdit.Text;
            smtFeederMaterial.Qty = int.Parse(this.txtQtyEdit.Text);
            smtFeederMaterial.SourceMaterialCode = this.txtSourceMaterialCodeEdit.Text;
            smtFeederMaterial.TableGroup = this.txtTableEdit.Text;
            smtFeederMaterial.FeederSpecCode = this.DropDownListFeederSpecCodeEdit.SelectedValue;

            smtFeederMaterial.MaintainUser = this.GetUserCode();
            smtFeederMaterial.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
            smtFeederMaterial.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);

            return smtFeederMaterial;
        }


        protected override object GetEditObject(GridRecord row)
        {
            if (_facade == null) { _facade = new SMTFacadeFactory(base.DataProvider).Create(); }
            object obj = _facade.GetReel(row.Items.FindItemByKey("ProductCode").Text.ToString());

            if (obj != null)
            {
                return (Reel)obj;
            }

            return null;
        }

        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.DropDownListFeederSpecCodeEdit.SelectedValue = "";
                this.txtItemCodeEdit.Text = "";
                this.txtQtyEdit.Text = "0";
                //this.txtTableEdit.Text = "";
                this.txtMachineCodeEdit.Text = "";
                this.txtMachineStationCodeEdit.Text = "";
                this.txtMaterialCodeEdit.Text = "";
                this.txtSourceMaterialCodeEdit.Text = "";
                this.txtSSCodeEdit.Text = "";

                return;
            }

            SMTFeederMaterial smtFeederMaterial = (SMTFeederMaterial)obj;
            this.DropDownListFeederSpecCodeEdit.SelectedValue = smtFeederMaterial.FeederSpecCode;
            this.txtItemCodeEdit.Text = smtFeederMaterial.ProductCode;
            this.txtQtyEdit.Text = smtFeederMaterial.Qty.ToString();
            this.txtTableEdit.Text = smtFeederMaterial.TableGroup;
            this.txtMachineCodeEdit.Text = smtFeederMaterial.MaterialCode;
            this.txtMachineStationCodeEdit.Text = smtFeederMaterial.MachineStationCode;
            this.txtMaterialCodeEdit.Text = smtFeederMaterial.MaterialCode;
            this.txtSourceMaterialCodeEdit.Text = smtFeederMaterial.SourceMaterialCode;
            this.txtSSCodeEdit.Text = smtFeederMaterial.StepSequenceCode;

        }


        protected override bool ValidateInput()
        {

            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblItemCodeEdit, txtItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblSSEdit, txtSSCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblMachineCodeEdit, txtMachineCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblMachineStationCodeEdit, txtMachineStationCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblSourceMaterialCodeEdit, txtSourceMaterialCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblMaterialCodeEdit, txtMaterialCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblFeedeSpecCodeEdit, DropDownListFeederSpecCodeEdit, 40, true));
            manager.Add(new NumberCheck(lblFeederQtyEdit, txtQtyEdit, 1, 999999999, false));
            manager.Add(new LengthCheck(lblTableEdit, txtTableEdit, 40, true));
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
            SMTFeederMaterial item = (SMTFeederMaterial)obj;
            return new string[]{
								item.ProductCode,
							    item.StepSequenceCode,
								item.MachineCode,
								item.MachineStationCode,
								item.SourceMaterialCode,
								item.MaterialCode,
								item.FeederSpecCode,
								item.Qty.ToString(),
								item.TableGroup
							};
        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
									"ProductCode",
									"sscode",
									"MachineCode",
									"MachineItemCode",
									"SourceMaterialCode",
									"MaterialCode",
									"FeederSpecCode",
									"Qty",
									"Table"};
        }
        #endregion

        protected void cmdImport_ServerClick(object sender, EventArgs e)
        {
            this.Page.Response.Redirect("FSMTLoadingMP.aspx");
        }

        #region check
        private ArrayList CheckItem(object obj)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            ArrayList itemFeeder = new ArrayList();
            try
            {
                SMTFeederMaterial item = (SMTFeederMaterial)obj;
                item.ProductCode = item.ProductCode.ToUpper();
                item.StepSequenceCode = item.StepSequenceCode.ToUpper();
                item.MachineCode = item.MachineCode.ToUpper();
                item.SourceMaterialCode = item.SourceMaterialCode.ToUpper();
                item.MachineStationCode = item.MachineStationCode.ToUpper();
                item.MaterialCode = item.MaterialCode.ToUpper();
                item.FeederSpecCode = item.FeederSpecCode.ToUpper();
                item.TableGroup = item.TableGroup.ToUpper();
                item.MaintainDate = dbDateTime.DBDate;
                item.MaintainTime = dbDateTime.DBTime;
                item.MaintainUser = this.GetUserCode();
                item.EAttribute1 = "True";
                if (item.TableGroup == string.Empty)
                    item.TableGroup = "A";


                itemFeeder.Add(item);
                CheckImportResult(itemFeeder);
            }
            catch (Exception ex)
            {
 
            }
            return itemFeeder;
        }

        private void CheckImportResult(ArrayList items)
        {
            BenQGuru.eMES.MOModel.ItemFacade itemFacade = new BenQGuru.eMES.MOModel.ItemFacade(base.DataProvider);
            BenQGuru.eMES.BaseSetting.BaseModelFacade modelFacade = new BenQGuru.eMES.BaseSetting.BaseModelFacade(this.DataProvider);
            if (_facade == null) { _facade = new SMTFacade(base.DataProvider); }
            ArrayList checkedSpec = new ArrayList();
            ArrayList checkedProduct = new ArrayList();
            ArrayList checkedSSCode = new ArrayList();
            for (int i = 0; i < items.Count; i++)
            {
                SMTFeederMaterial item = (SMTFeederMaterial)items[i];
                item.EAttribute1 = true.ToString();
                if (item.ProductCode == string.Empty ||
                    item.StepSequenceCode == string.Empty ||
                    item.MachineCode == string.Empty ||
                    item.MachineStationCode == string.Empty ||
                    item.MaterialCode == string.Empty)
                {
                    item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$Error_Input_Empty");
                    continue;
                }
                if (checkedProduct.Contains(item.ProductCode) == false)
                {
                    object obj = itemFacade.GetItem(item.ProductCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                    if (obj == null)
                    {
                        item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$Error_ItemCode_NotExist");
                        continue;
                    }
                    else
                    {
                        checkedProduct.Add(item.ProductCode);
                    }
                }
                if (checkedSSCode.Contains(item.StepSequenceCode) == false)
                {
                    object obj = modelFacade.GetStepSequence(item.StepSequenceCode);
                    if (obj == null)
                    {
                        item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$Error_SSCode_NotExist");
                        continue;
                    }
                    else
                    {
                        checkedSSCode.Add(item.StepSequenceCode);
                    }
                }
                if (item.FeederSpecCode == string.Empty || item.Qty == 0)
                {
                    if (item.SourceMaterialCode == string.Empty || i == 0)
                    {
                        item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$MaterialCode_Not_Exist");
                        continue;
                    }
                    for (int n = 0; n < items.Count; n++)
                    {
                        SMTFeederMaterial item1 = (SMTFeederMaterial)items[n];
                        if (item1.MaterialCode == item.SourceMaterialCode)
                        {
                            if (item.FeederSpecCode == string.Empty)
                                item.FeederSpecCode = item1.FeederSpecCode;
                            if (item.Qty == 0)
                                item.Qty = item1.Qty;
                            break;
                        }
                    }
                    if (item.FeederSpecCode == string.Empty)
                    {
                        item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$FeederSpec_Not_Exist");
                        continue;
                    }
                }
                if (checkedSpec.Contains(item.FeederSpecCode) == false)
                {
                    object obj = _facade.GetFeederSpec(item.FeederSpecCode);
                    if (obj == null)
                    {
                        item.EAttribute1 = false.ToString() + ":" + languageComponent1.GetString("$FeederSpec_Not_Exist");
                        continue;
                    }
                    else
                    {
                        checkedSpec.Add(item.FeederSpecCode);
                    }
                }
            }
        }

        #endregion
    }
}
