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
using System.Text;
using BenQGuru.eMES.BaseSetting;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialQuery : BaseMPage
    {
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;
        protected BenQGuru.eMES.Web.Helper.ExcelExporter excelExporter;

        private ButtonHelper buttonHelper = null;
        public BenQGuru.eMES.Web.UserControl.eMESDate txtTransDateFromQuery;
        public BenQGuru.eMES.Web.UserControl.eMESDate txtTransDateToQuery;

        private MaterialFacade _facade;//= new WarehouseFacade();
        private ItemFacade _ItemFacade = null;

        private string _status = FormStatus.Ready;

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

                this.txtUserCodeQuery.Text = this.GetUserCode();

                this.txtMDateQuery.Date_DateTime = FormatHelper.ToDateTime(FormatHelper.GetNowDBDateTime(this.DataProvider).DBDate, 125959);
                this.rdoSnScaleThreeFourEdit.Checked = true;


                //bighai 暂时注释
                if (this.cmdAdd.Attributes["onclick"] == null)
                {
                    this.cmdAdd.Attributes["onclick"] = @"window.name=window.name.replace('[back]','');var checkValue=checkSNCount('ADD');if (checkValue) {this.style.cursor='wait'; this.value='提交中...';this.disabled=true;setTimeout('__doPostBack(\'cmdAdd\',\'\')', 0);document.body.style.cursor='wait';}else{this.style.cursor='hand';return checkValue}";
                }

                if (this.cmdSave.Attributes["onclick"] == null)
                {
                    this.cmdSave.Attributes["onclick"] = @"window.name=window.name.replace('[back]','');var checkValue=checkSNCount('UPDATE');if (checkValue) {this.style.cursor='wait'; this.value='提交中...';this.disabled=true;setTimeout('__doPostBack(\'cmdSave\',\'\')', 0);document.body.style.cursor='wait';}else{this.style.cursor='hand';return checkValue}";
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
            this.gridHelper.AddColumn("Sequence", "序号", null);
            this.gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this.gridHelper.AddColumn("RCardPrefix", "序列号前缀", null);
            this.gridHelper.AddColumn("RunningCardStart", "起始序列号", null);
            this.gridHelper.AddColumn("RunningCardEnd", "结束序列号", null);
            this.gridHelper.AddColumn("SNScale", "数量", null);
            this.gridHelper.AddColumn("MItemCode", "料号", null);
            this.gridHelper.AddColumn("MITEMNAME", "物料名称", null);
            this.gridHelper.AddColumn("MoCode", "工单", null);
            this.gridHelper.AddColumn("LotNO", "生产批次", null);
            this.gridHelper.AddColumn("VendorCode", "厂商代码", null);
            this.gridHelper.AddColumn("VendorItemCode", "厂商料品代码", null);
            this.gridHelper.AddColumn("DateCode", "生产日期", null);
            this.gridHelper.AddColumn("Version", "物料版本", null);
            this.gridHelper.AddColumn("BIOS", "BIOS", null);
            this.gridHelper.AddColumn("PCBA", "PCBA", null);

            this.gridHelper.AddDefaultColumn(true, true);

            gridWebGrid.Columns.FromKey("MoCode").Hidden = true;
            gridWebGrid.Columns.FromKey("Sequence").Hidden = true;

            gridWebGrid.Columns.FromKey("MaintainDate").Width = new Unit(86, UnitType.Pixel);
            gridWebGrid.Columns.FromKey("RCardPrefix").Width = new Unit(90, UnitType.Pixel);
            gridWebGrid.Columns.FromKey("VendorItemCode").Width = new Unit(100, UnitType.Pixel);
            gridWebGrid.Columns.FromKey("BIOS").Width = new Unit(70, UnitType.Pixel);
            gridWebGrid.Columns.FromKey("PCBA").Width = new Unit(70, UnitType.Pixel);


            this.gridHelper.ApplyLanguage(this.languageComponent1);

        }


        protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
        {
            MKeyPart item = (MKeyPart)obj;
            Infragistics.WebUI.UltraWebGrid.UltraGridRow row =
                new Infragistics.WebUI.UltraWebGrid.UltraGridRow(
                new object[]{
				"false",
                item.Sequence,
                item.MaintainDate,
			    item.RCardPrefix,
			    item.RunningCardStart,
                item.RunningCardEnd,
			    item.SNScale,
			    item.MItemCode,
                item.MITEMNAME,
			    item.MoCode,
			    item.LotNO,
			    item.VendorCode,
			    item.VendorItemCode,
			    item.DateCode,
			    item.Version,
			    item.BIOS,
                item.PCBA
							});
            item = null;
            return row;
        }

        private Hashtable htItems = new Hashtable();
        private Hashtable htTransType = new Hashtable();
        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (_facade == null) { _facade = new MaterialFacade(base.DataProvider); }
            object[] objs = this._facade.QueryMKeyPartByMo(
                FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNQuery.Text)),
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)), FormatHelper.TODateInt(this.txtMDateQuery.Text),
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)),
                inclusive, exclusive);

            return objs;
        }


        protected override int GetRowCount()
        {
            if (_facade == null) { _facade = new MaterialFacade(base.DataProvider); }
            return this._facade.QueryMKeyPartByMoCount(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtItemCodeQuery.Text)), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtSNQuery.Text)),
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtMOCodeQuery.Text)), FormatHelper.TODateInt(FormatHelper.CleanString(this.txtMDateQuery.Text)),
                 FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtUserCodeQuery.Text)));

        }



        #endregion

        #region 数据初始化

        #endregion

        #region Button
        protected override void AddDomainObject(object domainObject)
        {
            bool checkresult = true;

            this.status = FormStatus.Add;
            this.cmdAdd.Disabled = true;


            CheckStartEndSN();

            checkresult = Save();

            if (checkresult)
            {
                this.status = FormStatus.Ready;
                this.btnLockEdit.Value = "锁定";
                WebInfoPublish.PublishInfo(this, "$CS_Save_Success", languageComponent1);
                System.Threading.Thread.Sleep(2000);
                //ClientScript.RegisterStartupScript(GetType(), "btnCommit", "alert('提交成功!!!');", true);
                txtInsideItemCodeEdit.Focus();

            }
            else
            {
                checkresult = false;

            }
            //this.cmdAdd.Disabled = false;
        }


        protected override void DeleteDomainObjects(ArrayList domainObjects)
        {
            ClientScriptManager clientScriptManager = Page.ClientScript;
            Type clientScriptType = Page.GetType();

            //删除OK
            if (_facade == null) { _facade = new MaterialFacade(base.DataProvider); }

            List<MKeyPart> mKeyPartList = new List<MKeyPart>();

            MKeyPart[] deliveryNotes = (MKeyPart[])domainObjects.ToArray(typeof(MKeyPart));

            if (deliveryNotes != null)
            {
                this.DataProvider.BeginTransaction();

                try
                {
                    foreach (MKeyPart deliveryNote in deliveryNotes)
                    {
                        mKeyPartList.Add(deliveryNote);
                        if (!CheckBeforeDeleteAndUpdate(mKeyPartList))
                        {


                            WebInfoPublish.Publish(this, string.Format("$CS_MItemCode:{0} $CS_RCardPrefix:{1} $CS_RunningCardStart:{2} $CS_RunningCardEnd:{3} $Error_CannotDeleteMKeyPart", deliveryNote.MItemCode, deliveryNote.RCardPrefix, deliveryNote.RunningCardStart, deliveryNote.RunningCardEnd), languageComponent1);
                            return;
                        }


                        this._facade.DeleteMKeyPart(deliveryNote);
                    }

                    this.DataProvider.CommitTransaction();

                    WebInfoPublish.PublishInfo(this, "$CS_Delete_Success", languageComponent1);
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();

                    ExceptionManager.Raise(deliveryNotes[0].GetType(), "$Error_Delete_Domain_Object", ex);
                }
            }

        }

        private bool CheckBeforeDeleteAndUpdate(List<MKeyPart> mKeyPartList)
        {
            bool returnValue = true;

            foreach (MKeyPart mKeyPart in mKeyPartList)
            {
                if (_facade.CheckMKeyPartUsed(mKeyPart))
                {
                    returnValue = false;
                    break;
                }
            }

            return returnValue;
        }

        protected override void UpdateDomainObject(object domainObject)
        {
            object obj = this.GetEditObject();


            this.status = FormStatus.Update;


            List<MKeyPart> mKeyPartList = new List<MKeyPart>();
            mKeyPartList.Add((MKeyPart)obj);
            if (!CheckBeforeDeleteAndUpdate(mKeyPartList))
            {

                WebInfoPublish.Publish(this, "$Error_CannotUpdateMKeyPart", languageComponent1);
                return;
            }

            if (Save())
            {
                this.status = FormStatus.Ready;
                this.btnLockEdit.Value = "锁定";
                WebInfoPublish.PublishInfo(this, "$CS_Save_Success", languageComponent1);

                txtInsideItemCodeEdit.Focus();
            }

        }

        protected override void buttonHelper_AfterPageStatusChangeHandle(string pageAction)
        {
            if (pageAction == PageActionType.Add)
            {
                this.txtInsideItemCodeEdit.ReadOnly = false;
            }

            if (pageAction == PageActionType.Update)
            {
                this.txtInsideItemCodeEdit.ReadOnly = true;
            }
        }

        #endregion

        protected override object GetEditObject()
        {
            if (this.ValidateInput() == false)
            {
                return null;
            }

            if (_facade == null) { _facade = new MaterialFacade(base.DataProvider); }

            MKeyPart mKeyPart = this._facade.CreateNewMKeyPart();

            if (this.txtSeq.Text.Trim() != "")
                mKeyPart.Sequence = Convert.ToInt16(this.txtSeq.Text.Trim());
            mKeyPart.RCardPrefix = FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtFirstStringEdit.Text.ToUpper().Trim(), 100));
            mKeyPart.RunningCardStart = this.txtMORCardStartEdit.Text.ToUpper().Trim();
            mKeyPart.RunningCardEnd = this.txtMORCardEndEdit.Text.ToUpper().Trim();
            mKeyPart.MItemCode = this.txtInsideItemCodeEdit.Text.ToUpper().Trim();
            mKeyPart.LotNO = this.txtLotNo.Text.ToUpper().Trim();
            mKeyPart.DateCode = this.txtDateCode.Text.ToUpper().Trim();
            mKeyPart.VendorCode = this.txtFactoryE.Text.ToUpper().Trim();
            mKeyPart.VendorItemCode = this.txtSupplierItemEdit.Text.ToUpper().Trim();
            mKeyPart.Version = this.txtMaterialVersionEdit.Text.ToUpper().Trim();
            mKeyPart.BIOS = this.txtBIOSVersionEdit.Text.ToUpper().Trim();
            mKeyPart.PCBA = this.txtPCBAVersionEdit.Text.ToUpper().Trim();
            mKeyPart.MoCode = "";
            mKeyPart.MaintainUser = this.GetUserCode();



            if (rdoSnScaleTenEdit.Checked == true)
                mKeyPart.SNScale = "10";
            else if (rdoSnScaleSixTeenEdit.Checked == true)
                mKeyPart.SNScale = "16";
            else if (rdoSnScaleThreeFourEdit.Checked == true)
                mKeyPart.SNScale = "34";

            if (this.txtInsideItemCodeEdit.Text.Trim() != String.Empty)
            {

                Domain.MOModel.Material material = (Domain.MOModel.Material)_ItemFacade.GetMaterial(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInsideItemCodeEdit.Text)), GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (material != null)
                {

                    //lblItemDesc.Text = material.MaterialDescription;

                    mKeyPart.MITEMNAME = lblItemDesc.Text;

                }
            }


            return mKeyPart;
        }

        protected override object GetEditObject(Infragistics.WebUI.UltraWebGrid.UltraGridRow row)
        {
            if (row == null)
            {
                return null;
            }



            if (_facade == null) { _facade = new MaterialFacade(base.DataProvider); }
            object obj = _facade.GetMKeyPart(System.Int32.Parse(row.Cells[1].Text), row.Cells[7].Text);

            if (obj != null)
            {
                List<MKeyPart> mKeyPartList = new List<MKeyPart>();
                mKeyPartList.Add((MKeyPart)obj);

                return (MKeyPart)obj;
            }

            return null;
        }

        private void SetEditObject()
        {
            this.txtSeq.Text = "";
            txtInsideItemCodeEdit.Text = "";
            txtFirstStringEdit.Text = "";
            txtMORCardStartEdit.Text = "";
            txtMORCardEndEdit.Text = "";
            txtDateCode.Text = "";
            txtFactoryE.Text = "";
            txtSupplierItemEdit.Text = "";
            lblItemDesc.Text = "";

            txtMaterialVersionEdit.Text = "";
            txtPCBAVersionEdit.Text = "";
            txtBIOSVersionEdit.Text = "";
            txtLotNo.Text = "";
            txtRCardLengthEdit.Text = "";
            txtQTY.Text = "";

            rdoSnScaleTenEdit.Checked = false;
            rdoSnScaleSixTeenEdit.Checked = false;
            rdoSnScaleThreeFourEdit.Checked = false;

            if (rdoSnScaleTenEdit.Checked == false && rdoSnScaleSixTeenEdit.Checked == false && rdoSnScaleThreeFourEdit.Checked == false)
                rdoSnScaleThreeFourEdit.Checked = true;

        }

        #region Object <--> Page
        protected override void SetEditObject(object obj)
        {
            if (obj == null)
            {
                this.txtSeq.Text = "";
                txtInsideItemCodeEdit.Text = "";
                txtFirstStringEdit.Text = "";
                txtMORCardStartEdit.Text = "";
                txtMORCardEndEdit.Text = "";
                txtDateCode.Text = "";
                txtFactoryE.Text = "";
                txtSupplierItemEdit.Text = "";
                lblItemDesc.Text = "";

                txtMaterialVersionEdit.Text = "";
                txtPCBAVersionEdit.Text = "";
                txtBIOSVersionEdit.Text = "";
                txtLotNo.Text = "";
                txtRCardLengthEdit.Text = "";
                txtQTY.Text = "";

                rdoSnScaleTenEdit.Checked = false;
                rdoSnScaleSixTeenEdit.Checked = false;
                rdoSnScaleThreeFourEdit.Checked = false;

                if (rdoSnScaleTenEdit.Checked == false && rdoSnScaleSixTeenEdit.Checked == false && rdoSnScaleThreeFourEdit.Checked == false)
                    rdoSnScaleThreeFourEdit.Checked = true;
                return;
            }

            List<MKeyPart> mKeyPartList = new List<MKeyPart>();
            mKeyPartList.Add((MKeyPart)obj);

            if (!CheckBeforeDeleteAndUpdate(mKeyPartList))
            {

                SetEditObject();

                if (status == FormStatus.Add)
                {
                }
                else
                {
                    WebInfoPublish.Publish(this, "$Error_CannotUpdateMKeyPart", languageComponent1);
                }
                return;
            }

            rdoSnScaleTenEdit.Checked = false;
            rdoSnScaleSixTeenEdit.Checked = false;
            rdoSnScaleThreeFourEdit.Checked = false;

            this.txtSeq.Text = ((MKeyPart)obj).Sequence.ToString();
            this.txtMORCardStartEdit.Text = ((MKeyPart)obj).RunningCardStart;
            this.txtMORCardEndEdit.Text = ((MKeyPart)obj).RunningCardEnd;
            this.txtLotNo.Text = ((MKeyPart)obj).LotNO;
            this.txtDateCode.Text = ((MKeyPart)obj).DateCode;
            this.txtFactoryE.Text = ((MKeyPart)obj).VendorCode;
            this.txtSupplierItemEdit.Text = ((MKeyPart)obj).VendorItemCode;
            this.txtMaterialVersionEdit.Text = ((MKeyPart)obj).Version;
            this.txtBIOSVersionEdit.Text = ((MKeyPart)obj).BIOS;
            this.txtPCBAVersionEdit.Text = ((MKeyPart)obj).PCBA;
            this.txtInsideItemCodeEdit.Text = ((MKeyPart)obj).MItemCode;
            //this.lblMitemName.Text = ((MKeyPart)obj).MITEMNAME;
            this.txtFirstStringEdit.Text = ((MKeyPart)obj).RCardPrefix;
            this.lblItemDesc.Text = ((MKeyPart)obj).MITEMNAME;



            switch (((MKeyPart)obj).SNScale)
            {
                case "10":
                    this.rdoSnScaleTenEdit.Checked = true;
                    break;
                case "16":
                    this.rdoSnScaleSixTeenEdit.Checked = true;
                    break;
                case "34":
                    this.rdoSnScaleThreeFourEdit.Checked = true;
                    break;
                default:
                    this.rdoSnScaleThreeFourEdit.Checked = true;
                    break;
            }

            if (rdoSnScaleTenEdit.Checked == false && rdoSnScaleSixTeenEdit.Checked == false && rdoSnScaleThreeFourEdit.Checked == false)
                rdoSnScaleThreeFourEdit.Checked = true;

        }

        #endregion

        #region save()
        private bool Save()
        {
            if (_facade == null) { _facade = new MaterialFacade(base.DataProvider); }

            object obj = this.GetEditObject();
            if (obj == null)
            {
                return false;
            }


            NumberScale scale = NumberScale.Scale34;
            if (rdoSnScaleTenEdit.Checked == true)
                scale = NumberScale.Scale10;
            else if (rdoSnScaleSixTeenEdit.Checked == true)
                scale = NumberScale.Scale16;
            else if (rdoSnScaleThreeFourEdit.Checked == true)
                scale = NumberScale.Scale34;


            try
            {
                if (!this.CheckID(this.txtMORCardStartEdit.Text.Trim().ToUpper(), this.txtMORCardEndEdit.Text.Trim().ToUpper()))
                    return false;

                //判断是否需要涉及TBLMKEYPARTDETAIL（更新时，如果首字符串、起始序列号、结束序列号未修改，则不动TBLMKEYPARTDETAIL）
                bool needUpdateDetail = true;
                string[] snList = null;

                if (this._status == FormStatus.Update)
                {
                    MKeyPart newMKeyPart = (MKeyPart)obj;
                    //newMKeyPart中的Sequence字段目前还没有意义
                    MKeyPart oldMKeyPart = (MKeyPart)this._facade.GetMKeyPart(Convert.ToDecimal(this.txtSeq.Text.Trim()), newMKeyPart.MItemCode);

                    if (oldMKeyPart != null
                        && oldMKeyPart.RCardPrefix.Trim().ToUpper() == newMKeyPart.RCardPrefix.Trim().ToUpper()
                        && oldMKeyPart.RunningCardStart.Trim().ToUpper() == newMKeyPart.RunningCardStart.Trim().ToUpper()
                        && oldMKeyPart.RunningCardEnd.Trim().ToUpper() == newMKeyPart.RunningCardEnd.Trim().ToUpper()
                        && oldMKeyPart.SNScale.Trim().ToUpper() == newMKeyPart.SNScale.Trim().ToUpper())
                    {
                        needUpdateDetail = false;
                    }
                }

                if (needUpdateDetail)
                {
                    int length = txtMORCardStartEdit.Text.Trim().Length;

                    long startSN = 0;
                    try
                    {
                        startSN = long.Parse(NumberScaleHelper.ChangeNumber(txtMORCardStartEdit.Text.Trim(), scale, NumberScale.Scale10));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        txtMORCardStartEdit.Focus();
                        return false;
                    }

                    long endSN = 0;
                    try
                    {
                        endSN = long.Parse(NumberScaleHelper.ChangeNumber(txtMORCardEndEdit.Text.Trim(), scale, NumberScale.Scale10));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                        txtMORCardEndEdit.Focus();
                        return false;
                    }

                    if (this.CalcSNCount() == false)
                        return false;

                    if ((startSN.ToString().Length + ((MKeyPart)obj).RCardPrefix.Length) > 40)
                    {
                        WebInfoPublish.Publish(this, "$Error_SNLength_Wrong", languageComponent1);
                        return false;

                    }

                    if (startSN > endSN)
                    {
                        WebInfoPublish.Publish(this, "$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd", languageComponent1);
                        txtMORCardEndEdit.Focus();
                        return false;
                    }

                    //bighai test

                    //long checkCount = _facade.CheckMKeyPartDetail(this.txtSeq.Text.Trim(), this.txtInsideItemCodeEdit.Text.Trim());

                    long checkCount = _facade.CheckMKeyPartDetail(this.txtSeq.Text.Trim(), this.txtInsideItemCodeEdit.Text.Trim(), txtMORCardStartEdit.Text.Trim(), txtMORCardEndEdit.Text.Trim(), ((MKeyPart)obj).RCardPrefix.Trim().ToUpper());

                    //检查需要插入的detail数据量是否太多
                    if (endSN - startSN > 4999)
                    {
                        //弹出窗口在前台

                    }

                    //检查维护的数量是否太多(因此增加检查，限制号码段每次不能超过500)
                    int snMaxSize = 500;
                    SystemSettingFacade systemFacade = new SystemSettingFacade(this.DataProvider);
                    object maxSize = systemFacade.GetParameter("VENDORMATERIALSNSIZE", "VENDORMATERIALSIZE");
                    if (maxSize != null)
                    {
                        snMaxSize = FormatInt(((BenQGuru.eMES.Domain.BaseSetting.Parameter)maxSize).ParameterAlias);
                    }
                    if (endSN - startSN > snMaxSize)
                    {
                        WebInfoPublish.Publish(this, "$Error_CS_SNRangeLarge", languageComponent1);
                        txtMORCardEndEdit.Focus();
                        return false;
                    }

                    //获取所有的序列号到数组中
                    snList = new string[endSN - startSN + 1];
                    for (long i = 0; i < snList.Length; i++)
                    {
                        snList[i] = NumberScaleHelper.ChangeNumber(Convert.ToString(startSN + i), NumberScale.Scale10, scale);
                        if (snList[i].Length < length)
                            snList[i] = snList[i].PadLeft(length, '0');
                        snList[i] = ((MKeyPart)obj).RCardPrefix + snList[i];
                    }

                    //检查需要插入的detail数据量是否重复
                    string startSNString = ((MKeyPart)obj).RCardPrefix + txtMORCardStartEdit.Text.Trim().ToUpper();
                    string endSNString = ((MKeyPart)obj).RCardPrefix + txtMORCardEndEdit.Text.Trim().ToUpper();

                    object[] existDetailList = _facade.QueryMKeyPartDetailWithHead(((MKeyPart)obj).MItemCode.ToUpper(), -1, startSNString, endSNString);
                    bool exist = false;
                    if (existDetailList != null)
                    {
                        foreach (MKeyPartDetail detial in existDetailList)
                        {
                            for (long i = 0; i < snList.Length; i++)
                            {
                                if (this._status == FormStatus.Add
                                     && detial.SerialNo.ToUpper() == snList[i].ToUpper())
                                {
                                    exist = true;
                                    break;
                                }

                                if (this._status == FormStatus.Update
                                    && detial.Sequence != Int32.Parse(this.txtSeq.Text.Trim())
                                    && detial.SerialNo.ToUpper() == snList[i].ToUpper())
                                {
                                    exist = true;
                                    break;
                                }
                            }

                            if (exist)
                                break;
                        }

                        if (exist)
                        {
                            //this.ShowMessage("$Error_CS_RunningCard_Range_Overlap");

                            WebInfoPublish.Publish(this, "$Error_CS_RunningCard_Range_Overlap", languageComponent1);

                            txtMORCardStartEdit.Focus();
                            return false;
                        }
                    }

                    //同时要卡如果序列号重复,则不允许不存在BOM关系的料号对应的序列号重复(BOM关系用低阶向高阶展)
                    string existSN = string.Empty;
                    for (long i = 0; i < snList.Length; i++)
                    {
                        if (_facade.QueryMKeyPartDetailCountNotInBOMRelation(((MKeyPart)obj).MItemCode.Trim().ToUpper(), snList[i].Trim().ToUpper()) > 0)
                        {
                            existSN = snList[i].Trim().ToUpper();
                            break;
                        }
                    }

                    if (existSN.Length > 0)
                    {
                        //this.ShowMessage("$Error_CS_RunningCard_Range_Overlap2 $SERIAL_NO=" + existSN);

                        WebInfoPublish.Publish(this, "$Error_CS_RunningCard_Range_Overlap2 $SERIAL_NO=" + existSN, languageComponent1);
                        txtMORCardStartEdit.Focus();
                        return false;
                    }
                }

                //保存前的准备
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                int date = dbDateTime.DBDate;
                int time = dbDateTime.DBTime;
                string user = this.GetUserCode();

                ((MKeyPart)obj).MaintainDate = date;
                ((MKeyPart)obj).MaintainTime = time;
                ((MKeyPart)obj).MaintainUser = user;

                MKeyPartDetail detail = _facade.CreateNewMKeyPartDetail();
                detail.PrintTimes = 0;
                detail.MaintainDate = date;
                detail.MaintainTime = time;
                detail.MaintainUser = user;
                detail.EAttribute1 = " ";

                this.DataProvider.BeginTransaction();

                if (this._status == FormStatus.Add)
                {
                    this._facade.AddMKeyPartTrace((MKeyPart)obj);
                }
                else if (this._status == FormStatus.Update)
                {
                    ((MKeyPart)obj).Sequence = Int32.Parse(this.txtSeq.Text.Trim());
                    object oldMkeyPart = _facade.GetMKeyPart(((MKeyPart)obj).Sequence, ((MKeyPart)obj).MItemCode);
                    if (oldMkeyPart == null)
                    {

                        this._facade.AddMKeyPartTrace((MKeyPart)obj);
                    }
                    else
                    {
                        this._facade.UpdateMKeyPart((MKeyPart)obj);

                    }
                }

                if (needUpdateDetail && snList != null)
                {
                    this._facade.UpdateMKeyPartDetialEAttribute1(((MKeyPart)obj).MItemCode, ((MKeyPart)obj).Sequence, "N");

                    for (long i = 0; i < snList.Length; i++)
                    {
                        detail.MItemCode = ((MKeyPart)obj).MItemCode;
                        detail.Sequence = ((MKeyPart)obj).Sequence;
                        detail.SerialNo = snList[i];

                        object oldMkeyPartDetail = _facade.GetMKeyPartDetail(detail.MItemCode, detail.SerialNo);
                        if (oldMkeyPartDetail == null)
                        {
                            ((MKeyPartDetail)detail).PrintTimes = 0;
                            this._facade.AddMKeyPartDetail((MKeyPartDetail)detail);
                        }
                        else
                        {
                            ((MKeyPartDetail)detail).PrintTimes = ((MKeyPartDetail)oldMkeyPartDetail).PrintTimes;
                            this._facade.UpdateMKeyPartDetail((MKeyPartDetail)detail);
                        }
                    }

                    this._facade.DeleteMKeyPartDetail(((MKeyPart)obj).MItemCode, ((MKeyPart)obj).Sequence, "N");
                }



                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
                txtMORCardStartEdit.Focus();
                return false;
            }

            return true;
        }


        public string CheckStartEndSN()
        {
            NumberScale scale = NumberScale.Scale34;
            if (rdoSnScaleTenEdit.Checked == true)
                scale = NumberScale.Scale10;
            else if (rdoSnScaleSixTeenEdit.Checked == true)
                scale = NumberScale.Scale16;
            else if (rdoSnScaleThreeFourEdit.Checked == true)
                scale = NumberScale.Scale34;
            else
                scale = NumberScale.Scale34;

            int length = txtMORCardStartEdit.Text.Trim().Length;

            if (txtMORCardStartEdit.Text.Trim() != "" && txtMORCardEndEdit.Text.Trim() != "")
            {

                long startSN = 0;
                try
                {
                    startSN = long.Parse(NumberScaleHelper.ChangeNumber(txtMORCardStartEdit.Text.Trim(), scale, NumberScale.Scale10));
                }
                catch (Exception ex)
                {
                    throw ex;
                    txtMORCardStartEdit.Focus();
                    return "false";
                }

                long endSN = 0;
                try
                {
                    endSN = long.Parse(NumberScaleHelper.ChangeNumber(txtMORCardEndEdit.Text.Trim(), scale, NumberScale.Scale10));
                }
                catch (Exception ex)
                {
                    throw ex;
                    txtMORCardEndEdit.Focus();
                    return "false";
                }

                if (this.CalcSNCount() == false)
                    return "false";

                if ((startSN.ToString().Length + txtFirstStringEdit.Text.ToString().Length) > 40)
                {
                    WebInfoPublish.Publish(this, "$Error_SNLength_Wrong", languageComponent1);
                    return "false";

                }

                if (startSN > endSN)
                {
                    //this.ShowMessage("$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd");

                    WebInfoPublish.Publish(this, "$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd", languageComponent1);
                    txtMORCardEndEdit.Focus();
                    return "false";
                }

                //检查需要插入的detail数据量是否太多
                if (endSN - startSN > 1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }

        }

        private int FormatInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private bool CheckID(string startCard, string endCard)
        {
            bool passCheck = false;
            //长度检查
            if (chbRCardLengthEdit.Checked && txtRCardLengthEdit.Text.Trim() != string.Empty)
            {
                int len = 0;
                try
                {
                    len = int.Parse(txtRCardLengthEdit.Text.Trim());
                    if (txtFirstStringEdit.Text.Length + startCard.Length != len)
                    {
                        passCheck = false;

                        WebInfoPublish.Publish(this, "$CS_MCARD_Len_NotCompare", languageComponent1);
                        txtMORCardStartEdit.Focus();


                        return passCheck;
                    }

                    if (txtFirstStringEdit.Text.Length + endCard.Length != len)
                    {
                        passCheck = false;

                        WebInfoPublish.Publish(this, "$CS_endMCARD_Len_NotCompare", languageComponent1);
                        txtMORCardStartEdit.Focus();


                        return passCheck;
                    }
                }
                catch
                {
                    return passCheck;
                }
            }

            passCheck = true;

            return passCheck;

        }

        #endregion

        protected void chbRCardLengthEdit_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.chbRCardLengthEdit.Checked)
            {
                txtRCardLengthEdit.ReadOnly = false;
            }
            else
            {
                txtRCardLengthEdit.ReadOnly = true;
                txtRCardLengthEdit.Text = "";


            }

        }

        protected void rdoSnScaleTenEdit_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rdoSnScaleTenEdit.Checked)
            {
                this.rdoSnScaleSixTeenEdit.Checked = false;
                this.rdoSnScaleThreeFourEdit.Checked = false;
            }

        }

        protected void rdoSnScaleSixTeenEdit_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rdoSnScaleSixTeenEdit.Checked)
            {
                this.rdoSnScaleTenEdit.Checked = false;
                this.rdoSnScaleThreeFourEdit.Checked = false;
            }

        }
        protected void rdoSnScaleThreeFourEdit_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rdoSnScaleThreeFourEdit.Checked)
            {
                this.rdoSnScaleSixTeenEdit.Checked = false;
                this.rdoSnScaleTenEdit.Checked = false;
            }

        }

        protected void btnTestEndSN_Click(object sender, EventArgs e)
        {
            CalcRCardEnd();

        }
        protected void btnTestQty_Click(object sender, EventArgs e)
        {
            CalcSNCount();

        }

        protected void btnJSNEdit_Click(object sender, EventArgs e)
        {
            ParseSN();

        }

        protected void btnLockEdit_Click(object sender, EventArgs e)
        {
            LockControl();

        }

        private void LockControl()
        {
            if (btnLockEdit.Value == "锁定" && txtInsideItemCodeEdit.Text.Trim() != String.Empty)
            {
                btnLockEdit.Value = "解除锁定";

                txtBIOSVersionEdit.Enabled = false;

                txtSupplierItemEdit.Enabled = false;
                txtDateCode.Enabled = false;

                txtMaterialVersionEdit.Enabled = false;
                txtFactoryE.Enabled = false;
                txtLotNo.Enabled = false;
                txtPCBAVersionEdit.Enabled = false;
                txtInsideItemCodeEdit.Enabled = false;
            }
            else if (btnLockEdit.Value == "解除锁定")
            {
                btnLockEdit.Value = "锁定";

                txtBIOSVersionEdit.Enabled = true;

                txtSupplierItemEdit.Enabled = true;
                txtDateCode.Enabled = true;

                txtMaterialVersionEdit.Enabled = true;
                txtFactoryE.Enabled = true;
                txtLotNo.Enabled = true;
                txtPCBAVersionEdit.Enabled = true;
                txtInsideItemCodeEdit.Enabled = true;
            }
        }

        private bool ParseSN()
        {
            if (txtFirstStringEdit.Text.Trim().Length >= 6)
            {
                try
                {
                    string prefix = txtFirstStringEdit.Text.Trim();

                    this.txtFactoryE.Text = prefix.Substring(0, 2);

                    string dateCode = string.Empty;
                    dateCode += "20" + int.Parse(NumberScaleHelper.ChangeNumber(prefix.Substring(2, 2), NumberScale.Scale10, NumberScale.Scale10)).ToString("00");
                    dateCode += int.Parse(NumberScaleHelper.ChangeNumber(prefix.Substring(4, 1), NumberScale.Scale16, NumberScale.Scale10)).ToString("00");
                    dateCode += int.Parse(NumberScaleHelper.ChangeNumber(prefix.Substring(5, 1), NumberScale.Scale34, NumberScale.Scale10)).ToString("00");
                    this.txtDateCode.Text = dateCode;

                    return true;
                }
                catch (Exception ex)
                {
                    // ExceptionManager.Raise("", "", ex);
                    throw ex;
                    txtFirstStringEdit.Focus();
                    return false;
                }
            }

            return false;
        }



        private bool CalcSNCount()
        {
            //检查是否一致的长度，是否都输入了
            if (!CheckSNStartAndEnd())
            {
                txtQTY.Text = "";
                return false;
            }

            NumberScale scale = NumberScale.Scale34;
            //if (ultraOptionSetScale.CheckedIndex == 0)
            //    scale = NumberScale.Scale10;
            //else if (ultraOptionSetScale.CheckedIndex == 1)
            //    scale = NumberScale.Scale16;
            //else if (ultraOptionSetScale.CheckedIndex == 2)
            //    scale = NumberScale.Scale34;


            if (rdoSnScaleTenEdit.Checked == true)
                scale = NumberScale.Scale10;
            else if (rdoSnScaleSixTeenEdit.Checked == true)
                scale = NumberScale.Scale16;
            else if (rdoSnScaleThreeFourEdit.Checked == true)
                scale = NumberScale.Scale34;




            long startSN = 0;
            try
            {
                startSN = long.Parse(NumberScaleHelper.ChangeNumber(txtMORCardStartEdit.Text.Trim(), scale, NumberScale.Scale10));
            }
            catch (Exception ex)
            {
                //this.ShowMessage(ex);

                throw ex;

                txtMORCardStartEdit.Focus();
                return false;
            }

            long endSN = 0;
            try
            {
                endSN = long.Parse(NumberScaleHelper.ChangeNumber(txtMORCardEndEdit.Text.Trim(), scale, NumberScale.Scale10));
            }
            catch (Exception ex)
            {
                //this.ShowMessage(ex);
                throw ex;
                txtMORCardEndEdit.Focus();
                return false;
            }

            if (startSN > endSN)
            {
                //this.ShowMessage("$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd");

                WebInfoPublish.Publish(this, "$Error_CS_RunningCardStart_Greater_Than_RunningCardEnd", languageComponent1);
                txtMORCardEndEdit.Focus();
                return false;
            }

            long count = endSN - startSN + 1;
            txtQTY.Text = count.ToString();

            return true;
        }

        private bool CheckSNStartAndEnd()
        {
            if (this.txtMORCardStartEdit.Text.Trim() == string.Empty)
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardStart$Error_Input_Empty"));

                WebInfoPublish.Publish(this, "$CS_RunningCardStart$Error_Input_Empty", languageComponent1);


                txtMORCardStartEdit.Focus();
                return false;
            }

            if (this.txtMORCardEndEdit.Text.Trim() == string.Empty)
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardEnd$Error_Input_Empty"));

                WebInfoPublish.Publish(this, "$CS_RunningCardEnd$Error_Input_Empty", languageComponent1);
                txtMORCardEndEdit.Focus();
                return false;
            }

            if (this.txtMORCardStartEdit.Text.Trim().Length != this.txtMORCardEndEdit.Text.Trim().Length)
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCard_Range_Should_be_Equal_Length"));

                WebInfoPublish.Publish(this, "$CS_RunningCard_Range_Should_be_Equal_Length", languageComponent1);
                txtMORCardStartEdit.Focus();
                return false;
            }

            return true;
        }


        private bool CalcRCardEnd()
        {
            //检查是否一致的长度，是否都输入了
            if (!CheckBeforeCalcRCardEnd())
            {
                return false;
            }

            NumberScale scale = NumberScale.Scale34;
            //if (ultraOptionSetScale.CheckedIndex == 0)
            //    scale = NumberScale.Scale10;
            //else if (ultraOptionSetScale.CheckedIndex == 1)
            //    scale = NumberScale.Scale16;
            //else if (ultraOptionSetScale.CheckedIndex == 2)
            //    scale = NumberScale.Scale34;

            if (rdoSnScaleTenEdit.Checked == true)
                scale = NumberScale.Scale10;
            else if (rdoSnScaleSixTeenEdit.Checked == true)
                scale = NumberScale.Scale16;
            else if (rdoSnScaleThreeFourEdit.Checked == true)
                scale = NumberScale.Scale34;




            long startSN = 0;
            try
            {
                startSN = long.Parse(NumberScaleHelper.ChangeNumber(txtMORCardStartEdit.Text.Trim(), scale, NumberScale.Scale10));
            }
            catch (Exception ex)
            {
                //this.ShowMessage(ex);
                throw ex;
                txtMORCardStartEdit.Focus();
                return false;
            }

            long count = 0;
            try
            {
                count = long.Parse(txtQTY.Text.Trim());
            }
            catch (Exception ex)
            {
                //this.ShowMessage(ex);
                throw ex;
                txtQTY.Focus();
                return false;
            }

            long endSN = startSN + count - 1;

            try
            {
                string RCardEnd = NumberScaleHelper.ChangeNumber(endSN.ToString(), NumberScale.Scale10, scale);
                if (RCardEnd.Trim().Length < txtMORCardStartEdit.Text.Trim().Length)
                {
                    RCardEnd = RCardEnd.PadLeft(txtMORCardStartEdit.Text.Trim().Length, '0');
                }

                txtMORCardEndEdit.Text = RCardEnd;

                if (RCardEnd.Trim().Length != txtMORCardStartEdit.Text.Trim().Length)
                {
                    //ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCard_Range_Should_be_Equal_Length"));

                    WebInfoPublish.Publish(this, "$CS_RunningCard_Range_Should_be_Equal_Length", languageComponent1);
                    this.txtQTY.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                //this.sh(ex);
                throw ex;
                txtMORCardEndEdit.Focus();
                return false;
            }

            return true;
        }

        private bool CheckBeforeCalcRCardEnd()
        {
            if (this.txtMORCardStartEdit.Text.Trim() == string.Empty)
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardStart$Error_Input_Empty"));
                WebInfoPublish.Publish(this, "$CS_RunningCardStart$Error_Input_Empty", languageComponent1);
                txtMORCardStartEdit.Focus();
                return false;
            }

            if (this.txtQTY.Text.Trim() == string.Empty)
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RunningCardCount$Error_Input_Empty"));

                WebInfoPublish.Publish(this, "$CS_RunningCardCount$Error_Input_Empty", languageComponent1);
                this.txtQTY.Focus();
                return false;
            }

            return true;
        }

        private bool checkItemCode()
        {
            bool result = true;
            this._ItemFacade = new ItemFacade(this.DataProvider);
            Domain.MOModel.Material material = (Domain.MOModel.Material)_ItemFacade.GetMaterial(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtInsideItemCodeEdit.Text)), GlobalVariables.CurrentOrganizations.First().OrganizationID);

            if (material == null)
                result = false;
            return result;
        }

        protected bool ValidateInput()
        {
            bool validate = true;

            //check length

            string checkMessage = string.Empty;



            PageCheckManager manager = new PageCheckManager();

            manager.Add(new LengthCheck(lblInsideItemCodeEdit, txtInsideItemCodeEdit, 40, true));
            manager.Add(new LengthCheck(lblDateCode, txtDateCode, 100, false));
            manager.Add(new LengthCheck(lblFactoryE, txtFactoryE, 100, false));
            manager.Add(new LengthCheck(lblSupplierItemEdit, txtSupplierItemEdit, 100, false));
            manager.Add(new LengthCheck(lblMaterialVersionEdit, txtMaterialVersionEdit, 100, false));

            manager.Add(new LengthCheck(lblPCBAVersionEdit, txtPCBAVersionEdit, 100, false));
            manager.Add(new LengthCheck(lblBIOSVersionEdit, txtBIOSVersionEdit, 100, false));
            manager.Add(new LengthCheck(lblLotNo, txtLotNo, 40, false));


            //manager.Add(new NumberCheck(lblRealReceiveQtyEdit, txtRealReceiveQtyEdit, 1, 9999999999999, true));
            //manager.Add(new DateCheck(lblAccountDateEdit, datAccountDateEdit.Date_String, true));
            //manager.Add(new DateCheck(lblVoucherDateEdit, datVoucherDateEdit.Date_String, true));

            if (!manager.Check())
            {
                checkMessage += manager.CheckMessage;
            }

            if (checkMessage.Trim().Length > 0)
            {
                WebInfoPublish.Publish(this, checkMessage, languageComponent1);
                validate = false;
                return validate;
            }


            if (this.txtInsideItemCodeEdit.Text.Trim() == string.Empty)
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_MItemCode$Error_Input_Empty"));

                WebInfoPublish.Publish(this, "$CS_MItemCode$Error_Input_Empty", languageComponent1);

                txtInsideItemCodeEdit.Focus();
                validate = false;
                return validate;
            }

            if (!checkItemCode())
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_OPBOMItem_Not_Exist"));
                WebInfoPublish.Publish(this, "$Error_CS_OPBOMItem_Not_Exist", languageComponent1);
                txtInsideItemCodeEdit.Focus();
                validate = false;
                return validate;
            }


            if (this.txtFirstStringEdit.Text.Trim() == string.Empty)
            {
                //this.ShowMessage(new UserControl.Message(MessageType.Error, "$CS_RCardPrefix$Error_Input_Empty"));
                WebInfoPublish.Publish(this, "$CS_RCardPrefix$Error_Input_Empty", languageComponent1);
                txtFirstStringEdit.Focus();
                validate = false;
                return validate;
            }

            validate = CheckSNStartAndEnd();
            if (!validate)
                return validate;

            return validate;
        }

        #region Export
        protected override string[] FormatExportRecord(object obj)
        {
            MKeyPart item = (MKeyPart)obj;
            string[] strArr =
                new string[]{	
                   
                item.MaintainDate.ToString(),
			    item.RCardPrefix,
			    item.RunningCardStart,
                item.RunningCardEnd,
			    item.SNScale,
			    item.MItemCode,
                item.MITEMNAME,
			    item.MoCode,
			    item.LotNO,
			    item.VendorCode,
			    item.VendorItemCode,
			    item.DateCode,
			    item.Version,
			    item.BIOS,
			    item.PCBA
							};
            item = null;
            return strArr;

        }

        protected override string[] GetColumnHeaderText()
        {
            return new string[] {	
                "MaintainDate",
			    "RCardPrefix",
			    "RunningCardStart",
                "RunningCardEnd",
			    "SNScale",
			    "MItemCode",
			    "MITEMNAME",
			    "MoCode",
			    "LotNO",
			    "VendorCode",
			    "VendorItemCode",
			    "DateCode",
			    "Version",
			    "BIOS",
			    "PCBA"
								};
        }
        #endregion

        public class FormStatus
        {
            public static string Add = "Add";
            public static string Update = "Update";
            public static string Ready = "Ready";
            public static string Noready = "Noready";
        }

        #region FormStatus

        // private string _status = FormStatus.Ready;
        private string status
        {
            get
            {
                return this._status;
            }
            set
            {
                this._status = value;

            }
        }

        #endregion

        public override void CheckSession(SessionHelper sessionHelper)
        {
            // Session过期
            if (!sessionHelper.IsLogin)
            {
                sessionHelper.RemoveAll();
                this.Response.Write(string.Format("<script language=javascript>window.top.location.href='{0}FLoginNewForSRM.aspx'</script>", this.VirtualHostRoot));
                this.Response.End();
            }
        }
    }
}
