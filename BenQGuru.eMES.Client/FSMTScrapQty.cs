using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Web.Helper;
using Infragistics.Win.UltraWinGrid;
using UserControl;

namespace BenQGuru.eMES.Client
{
    public partial class FSMTScrapQty : BaseForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private DataTable tableSource = null;
        private MaterialFacade materialFacade = null;
        private MOFacade mofacade = null;
        private ItemFacade itemfacade = null;
        private SMTFacade smtfacade = null;
        private DataCollectFacade collectfacade = null;
        private MORunningCardFacade morcardfacade = null;

        private IDomainDataProvider _DomainDataProvider = ApplicationService.Current().DataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _DomainDataProvider;
            }
        }

        private void CloseConnection()
        {
            if (this.DataProvider != null)
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
        }

        #region FKeyPartFrontLoad_Load

        public FSMTScrapQty()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(gridList);
        }

        private void FKeyPartFrontLoad_Load(object sender, EventArgs e)
        {
            this.startDate.Value = DateTime.Today.AddDays(-7);
            this.endDate.Value = DateTime.Today;
            this.ucBtnSave.Enabled = false;

            tableSource = new DataTable();
            tableSource.Columns.Add("PartNumber");
            tableSource.Columns.Add("MOCode");
            tableSource.Columns.Add("Rcard");
            tableSource.Columns.Add("RelationQty");
            tableSource.Columns.Add("Status");
            tableSource.Columns.Add("MDate");
            tableSource.Columns.Add("Edit");
            gridList.DataSource = tableSource;

            //this.InitPageLanguage();
            //this.InitGridLanguage(gridList);
        }

        #endregion

        #region Button_Events

        private void btnQuery_Click(object sender, System.EventArgs e)
        {
            string status = string.Empty;
            if (smtfacade == null)
            {
                smtfacade = new SMTFacade(this.DataProvider);
            }
            if (mofacade == null)
            {
                mofacade = new MOFacade(this.DataProvider);
            }
            if (collectfacade == null)
            {
                collectfacade = new DataCollectFacade(this.DataProvider);
            }
            object[] objs = smtfacade.Querysmtrelation(this.txtMoCodeQuery.Value.Trim().ToUpper(),
                                                     this.txtPartNumber.Value.Trim().ToUpper(),
                                                     this.txtRCardQuery.Value.Trim().ToUpper(),
                                                     FormatHelper.TODateInt(this.startDate.Value),
                                                     FormatHelper.TODateInt(this.endDate.Value));
            tableSource.Rows.Clear();
            this.ucBtnNew.Enabled = true;
            this.ucBtnSave.Enabled = false;
            this.txtMoCodeEdit.Value = string.Empty;
            this.txtRelationQtyEdit.Value = string.Empty;
            this.txtRCardEdit.Value = string.Empty;
            this.txtMoCodeEdit.Enabled = true;
            this.txtRelationQtyEdit.Enabled = true;
            this.txtRCardEdit.Enabled = true;
            this.txtMoCodeEdit.TextFocus(false, true);
            if (objs != null)
            {
                string strStatusClose = ProductionStatus.ProductionStatus_CloseProduction.ToString().Trim();
                string strStatusin = ProductionStatus.ProductionStatus_InProduction.ToString().Trim();
                string strStatusNo = ProductionStatus.ProductionStatus_NoProduction.ToString().Trim();
                for (int i = 0; i < objs.Length; i++)
                {
                    Smtrelationqty smt = (Smtrelationqty)objs[i];
                    DataRow row = tableSource.NewRow();
                    object simrepObj = collectfacade.GetSimulationReport(smt.Mocode.Trim().ToUpper(), smt.Rcard.Trim().ToUpper());

                    if (simrepObj != null)
                    {
                        if (((SimulationReport)simrepObj).IsComplete.Trim() == "1")
                        {
                            status = MutiLanguages.ParserString(strStatusClose);
                        }
                        else
                        {
                            status = MutiLanguages.ParserString(strStatusin);
                        }
                    }
                    else
                    {
                        status = MutiLanguages.ParserString(strStatusNo);
                    }
                    row["PartNumber"] = smt.Itemcode.ToString();
                    row["MOCode"] = smt.Mocode;
                    row["Rcard"] = smt.Rcard;
                    row["RelationQty"] = smt.Relationqtry;
                    row["Status"] = status;
                    row["MDate"] = FormatHelper.ToDateTime(smt.Mdate, smt.Mtime);
                    row["Edit"] = "";
                    tableSource.Rows.Add(row);
                }
            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_No_Data_To_Display"));
                return;
            }
        }

        private void ucBtnNew_Click(object sender, EventArgs e)
        {
            KeyPressEventArgs a = new KeyPressEventArgs('\r');
            this.txtRelationQtyEdit__TxtboxKeyPress(sender, a);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtRelationQtyEdit.Value.Trim() == string.Empty)
            {
                //数量不能为空 $Error_Qty_Cannot_Empty
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Qty_Cannot_Empty"));
                this.txtRelationQtyEdit.TextFocus(false, true);
                return;
            }
            try
            {
                if (mofacade == null)
                {
                    mofacade = new MOFacade(this.DataProvider);
                }
                object objMO = mofacade.GetMO(this.txtMoCodeEdit.Value.Trim().ToUpper());
                decimal moQty = 0;

                int intQty = Convert.ToInt32(this.txtRelationQtyEdit.Value);
                if (intQty <= 0)
                {
                    //数量应为整数类型 $Error_Qty_Not_Int
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_SmtRelQty_Should_Over_Zero"));
                    this.txtRelationQtyEdit.TextFocus(false, true);
                    return;
                }
                else
                {
                    if (objMO != null && ((MO)objMO).IDMergeRule >= 0)
                    {
                        moQty = ((MO)objMO).IDMergeRule;
                    }
                    if (moQty < Convert.ToDecimal(intQty))
                    {
                        //数量应小于工单的连板数 $Error_Qty_Not_Int
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_SmtRelQty_Should_less_MO" + ": " + moQty));
                        this.txtRelationQtyEdit.TextFocus(false, true);
                        return;
                    }
                }
            }
            catch
            {
                //数量应为整数类型 $Error_Qty_Not_Int
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Qty_Not_Int"));
                this.txtRelationQtyEdit.TextFocus(false, true);
                return;
            }
            if (smtfacade == null)
            {
                smtfacade = new SMTFacade(this.DataProvider);
            }
            object relation = smtfacade.GetSMTRelationQty(this.txtRCardEdit.Value, this.txtMoCodeEdit.Value.Trim());
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            if (relation != null)
            {
                ((Smtrelationqty)relation).Relationqtry = int.Parse(this.txtRelationQtyEdit.Value);
                ((Smtrelationqty)relation).Muser = ApplicationService.Current().UserCode;

                ((Smtrelationqty)relation).Mdate = dbDateTime.DBDate;
                ((Smtrelationqty)relation).Mtime = dbDateTime.DBTime;
                smtfacade.UpdateSmtrelationqty((Smtrelationqty)relation);
            }
            this.ucBtnSave.Enabled = false;
            btnQuery_Click(sender, e);
        }

        private void ucButtonCancel_Click(object sender, EventArgs e)
        {
            this.txtRelationQtyEdit.Value = string.Empty;
            this.txtRCardEdit.Value = string.Empty;
            this.txtMoCodeEdit.Enabled = true;
            this.txtRelationQtyEdit.Enabled = true;
            this.txtRCardEdit.Enabled = true;
            this.ucBtnSave.Enabled = false;
            this.ucBtnNew.Enabled = true;
            if (string.IsNullOrEmpty(this.txtMoCodeEdit.Value))
            {
                this.txtMoCodeEdit.TextFocus(false, true);
            }
            else
            {
                this.txtRCardEdit.TextFocus(false, true);
            }
        }

        private void gridList_ClickCellButton(object sender, CellEventArgs e)
        {

            if (e.Cell.Column.Key.ToUpper() == "Edit".ToUpper())
            {
                string strStatusNo = ProductionStatus.ProductionStatus_NoProduction.ToString().Trim();

                this.txtRCardEdit.Value = e.Cell.Row.Cells["Rcard"].Value.ToString();
                this.txtMoCodeEdit.Value = e.Cell.Row.Cells["MOCode"].Value.ToString();
                this.txtRelationQtyEdit.Value = e.Cell.Row.Cells["RelationQty"].Value.ToString();
                this.txtRCardEdit.Enabled = false;
                this.txtMoCodeEdit.Enabled = false;
                this.txtRelationQtyEdit.Enabled = false;
                this.ucBtnSave.Enabled = true;
                this.ucBtnNew.Enabled = false;

                if (e.Cell.Row.Cells["Status"].Value.ToString().Trim() == MutiLanguages.ParserString(strStatusNo))
                {
                    this.txtRelationQtyEdit.Enabled = true;
                    this.txtRelationQtyEdit.TextFocus(false, true);
                }
            }
        }

        #endregion

        #region Txtbox_KeyPress

        private void txtMoCodeEdit_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.txtMoCodeEdit.Value == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MO_Cannot_Null"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }

                if (materialFacade == null) { materialFacade = new MaterialFacade(this.DataProvider); }

                if (materialFacade.GetMOCode(this.txtMoCodeEdit.Value) == false)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rework_MO_Not_Exist"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }

                if (mofacade == null)
                {
                    mofacade = new MOFacade(this.DataProvider);
                }
                object objMO = mofacade.GetMO(this.txtMoCodeEdit.Value.Trim().ToUpper());
                if (objMO == null || ((MO)objMO).MOStatus.Trim().ToUpper() == "MOSTATUS_CLOSE")
                {
                    //该工单已关单,不能维护打X板!        
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_MO_Close_Not_SmtRelQty"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }
                this.txtRCardEdit.TextFocus(false, true);
            }
        }

        private void txtRCardEdit_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                #region facade
                if (collectfacade == null)
                {
                    collectfacade = new DataCollectFacade(this.DataProvider);
                }
                if (morcardfacade == null)
                {
                    morcardfacade = new MORunningCardFacade(this.DataProvider);
                }
                if (materialFacade == null)
                { 
                    materialFacade = new MaterialFacade(this.DataProvider);
                }
                if (mofacade == null)
                {
                    mofacade = new MOFacade(this.DataProvider);
                }
                if(itemfacade ==null)
                {
                    itemfacade =new ItemFacade(this.DataProvider);
                }

                #endregion

                #region   null
                if (this.txtMoCodeEdit.Value == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MO_Cannot_Null"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }
                if (txtRCardEdit.Value.Trim().Equals(""))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$RMARcard_Cannot_Null"));
                    this.txtRCardEdit.TextFocus(false, true);
                    return;
                }
                #endregion

                #region mocode
                if (materialFacade.GetMOCode(this.txtMoCodeEdit.Value) == false)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rework_MO_Not_Exist"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }
                object objMO = mofacade.GetMO(this.txtMoCodeEdit.Value.Trim().ToUpper());
                if (objMO == null || ((MO)objMO).MOStatus.Trim().ToUpper() == "MOSTATUS_CLOSE")
                {
                    //该工单已关单,不能维护打X板!        
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_MO_Close_Not_SmtRelQty"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }
                #endregion

                #region rcard
                object lastsimulationReport = materialFacade.GetLastSimulationReport(this.txtRCardEdit.Value.Trim().ToUpper());
                if (lastsimulationReport != null)
                {
                    if (((SimulationReport)lastsimulationReport).IsComplete.Trim() == "0")
                    {
                        // 该序列号在生产中,不允许维护打X板!
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rcard_inProduction"));
                        this.txtRCardEdit.TextFocus(false, true);
                        return;
                    }
                    object simulationReport = collectfacade.GetSimulationReport(this.txtMoCodeEdit.Value.Trim().ToUpper(), this.txtRCardEdit.Value.Trim().ToUpper());
                    if (simulationReport != null)
                    {
                        // 该序列号在生产中或者已经完工,不允许维护打X板!
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rcard_inProductionOrClose"));
                        this.txtRCardEdit.TextFocus(false, true);
                        return;
                    }
                    else
                    {
                        //修改  rcard不在工单范围内
                        #region
                        object item2sncheck = itemfacade.GetItem2SNCheck(((MO)objMO).ItemCode.Trim().ToUpper(), ItemCheckType.ItemCheckType_SERIAL);
                        if (item2sncheck != null)
                        {
                            string checkContent = ((Item2SNCheck)item2sncheck).SNContentCheck.Trim().ToUpper();
                            int checkLength = ((Item2SNCheck)item2sncheck).SNLength;
                            string checkPrefix = ((Item2SNCheck)item2sncheck).SNPrefix.Trim().ToUpper();

                            string rcardCheck = this.txtRCardEdit.Value.Trim().ToUpper();
                            if (!string.IsNullOrEmpty(checkPrefix))
                            {
                                if (rcardCheck.IndexOf(checkPrefix) != 0)
                                {
                                    // 该序列号与工单要求的序列号前缀不一样     
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardPrefix_ERROR  $CS_RunningCardPrefix: " + checkPrefix));
                                    this.txtRCardEdit.TextFocus(false, true);
                                    return;
                                }
                            }
                            if (checkLength  !=null && checkLength > 0)
                            {
                                if (rcardCheck.Length != checkLength)
                                {
                                    // 该序列号与工单要求的序列号长度不一样   
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR  $CS_RunningCardLength: " + checkLength));
                                    this.txtRCardEdit.TextFocus(false, true);
                                    return;
                                }
                            }
                            if (checkContent != null && checkContent == "Y")
                            {
                                string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                                Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                                Match match = rex.Match(rcardCheck);
                                // 序列号被限制为字符 空格 数字  
                                if (!match.Success)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.txtRCardEdit.Value.Trim()));
                                    this.txtRCardEdit.TextFocus(false, true);
                                    return;
                                }
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    //修改      rcard不在工单范围内
                    #region
                    object item2sncheck = itemfacade.GetItem2SNCheck(((MO)objMO).ItemCode.Trim().ToUpper(), ItemCheckType.ItemCheckType_SERIAL);
                    if (item2sncheck != null)
                    {
                        string checkContent = ((Item2SNCheck)item2sncheck).SNContentCheck.Trim().ToUpper();
                        int checkLength = ((Item2SNCheck)item2sncheck).SNLength;
                        string checkPrefix = ((Item2SNCheck)item2sncheck).SNPrefix.Trim().ToUpper();

                        string rcardCheck = this.txtRCardEdit.Value.Trim().ToUpper();
                        if (!string.IsNullOrEmpty(checkPrefix))
                        {
                            if (rcardCheck.IndexOf(checkPrefix) != 0)
                            {
                                // 该序列号与工单要求的序列号前缀不一样     
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardPrefix_ERROR  $CS_RunningCardPrefix: " + checkPrefix));
                                this.txtRCardEdit.TextFocus(false, true);
                                return;
                            }
                        }
                        if (checkLength != null && checkLength > 0)
                        {
                            if (rcardCheck.Length != checkLength)
                            {
                                // 该序列号与工单要求的序列号长度不一样   
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR  $CS_RunningCardLength: " + checkLength));
                                this.txtRCardEdit.TextFocus(false, true);
                                return;
                            }
                        }
                        if (checkContent != null && checkContent == "Y")
                        {
                            string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                            Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                            Match match = rex.Match(rcardCheck);
                            // 序列号被限制为字符 空格 数字  
                            if (!match.Success)
                            {
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.txtRCardEdit.Value.Trim()));
                                this.txtRCardEdit.TextFocus(false, true);
                                return;
                            }
                        }
                    }
                    #endregion
                }
                #endregion

                this.txtRelationQtyEdit.TextFocus(false, true);
                
            }
        }

        private void txtRelationQtyEdit__TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                #region facade
                if (mofacade == null)
                {
                    mofacade = new MOFacade(this.DataProvider);
                }
                if (materialFacade == null)
                {
                    materialFacade = new MaterialFacade(this.DataProvider);
                }
                if (collectfacade == null)
                {
                    collectfacade = new DataCollectFacade(this.DataProvider);
                }
                if (morcardfacade == null)
                {
                    morcardfacade = new MORunningCardFacade(this.DataProvider);
                }
                if (smtfacade == null)
                {
                    smtfacade = new SMTFacade(this.DataProvider);
                }
                if (itemfacade == null)
                {
                    itemfacade = new ItemFacade(this.DataProvider);
                }
                #endregion
               
                if (this.txtMoCodeEdit.Enabled == false && this.txtRCardEdit.Enabled == false)
                {
                    btnSave_Click(sender, e);
                    return;
                }

                #region   null
                if (this.txtMoCodeEdit.Value == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$MO_Cannot_Null"));
                    this.txtMoCodeEdit.TextFocus(false, true);
                    return;
                }
                if (string.IsNullOrEmpty(txtRCardEdit.Value))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$RMARcard_Cannot_Null"));
                    this.txtRCardEdit.TextFocus(false, true);
                    return;
                }
                if (this.txtRelationQtyEdit.Value.Trim() == string.Empty)
                {
                    //数量不能为空 $Error_Qty_Cannot_Empty
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Qty_Cannot_Empty"));
                    this.txtRelationQtyEdit.TextFocus(false, true);
                    return;
                }
                #endregion

                try
                {
                    #region mocode
                    if (materialFacade.GetMOCode(this.txtMoCodeEdit.Value) == false)
                    {
                        //该工单不存在
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rework_MO_Not_Exist"));
                        this.txtMoCodeEdit.TextFocus(false, true);
                        return;
                    }
                    object objMO = mofacade.GetMO(this.txtMoCodeEdit.Value.Trim().ToUpper());
                    if (objMO == null || ((MO)objMO).MOStatus.Trim().ToUpper() == "MOSTATUS_CLOSE")
                    {
                        //该工单已关单,不能维护打X板!        
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_MO_Close_Not_SmtRelQty"));
                        this.txtMoCodeEdit.TextFocus(false, true);
                        return;
                    }
                    #endregion

                    #region rcard
                    object lastsimulationReport = materialFacade.GetLastSimulationReport(this.txtRCardEdit.Value.Trim().ToUpper());
                    if (lastsimulationReport != null)
                    {
                        if (((SimulationReport)lastsimulationReport).IsComplete.Trim() == "0")
                        {
                            // 该序列号在生产中,不允许维护打X板!
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rcard_inProduction"));
                            this.txtRCardEdit.TextFocus(false, true);
                            return;
                        }
                        object simulationReport = collectfacade.GetSimulationReport(this.txtMoCodeEdit.Value.Trim().ToUpper(),this.txtRCardEdit.Value.Trim().ToUpper());
                        if (simulationReport != null)
                        {
                            // 该序列号在生产中或者已经完工,不允许维护打X板!
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Rcard_inProductionOrClose"));
                            this.txtRCardEdit.TextFocus(false, true);
                            return;
                        }
                        else
                        {
                            //修改  rcard不在工单范围内
                            #region
                            object item2sncheck = itemfacade.GetItem2SNCheck(((MO)objMO).ItemCode.Trim().ToUpper(), ItemCheckType.ItemCheckType_SERIAL);
                            if (item2sncheck != null)
                            {
                                string checkContent = ((Item2SNCheck)item2sncheck).SNContentCheck.Trim().ToUpper();
                                int checkLength = ((Item2SNCheck)item2sncheck).SNLength;
                                string checkPrefix = ((Item2SNCheck)item2sncheck).SNPrefix.Trim().ToUpper();

                                string rcardCheck = this.txtRCardEdit.Value.Trim().ToUpper();
                                if (!string.IsNullOrEmpty(checkPrefix))
                                {
                                    if (rcardCheck.IndexOf(checkPrefix) != 0)
                                    {
                                        // 该序列号与工单要求的序列号前缀不一样     
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardPrefix_ERROR  $CS_RunningCardPrefix: " + checkPrefix));
                                        this.txtRCardEdit.TextFocus(false, true);
                                        return;
                                    }
                                }
                                if (checkLength != null && checkLength > 0)
                                {
                                    if (rcardCheck.Length != checkLength)
                                    {
                                        // 该序列号与工单要求的序列号长度不一样   
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR  $CS_RunningCardLength: " + checkLength));
                                        this.txtRCardEdit.TextFocus(false, true);
                                        return;
                                    }
                                }
                                if (checkContent != null && checkContent == "Y")
                                {
                                    string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                                    Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                                    Match match = rex.Match(rcardCheck);
                                    // 序列号被限制为字符 空格 数字  
                                    if (!match.Success)
                                    {
                                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.txtRCardEdit.Value.Trim()));
                                        this.txtRCardEdit.TextFocus(false, true);
                                        return;
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    else
                    {
                        //修改      rcard不在工单范围内
                        #region
                        object item2sncheck = itemfacade.GetItem2SNCheck(((MO)objMO).ItemCode.Trim().ToUpper(), ItemCheckType.ItemCheckType_SERIAL);
                        if (item2sncheck != null)
                        {
                            string checkContent = ((Item2SNCheck)item2sncheck).SNContentCheck.Trim().ToUpper();
                            int checkLength = ((Item2SNCheck)item2sncheck).SNLength;
                            string checkPrefix = ((Item2SNCheck)item2sncheck).SNPrefix.Trim().ToUpper();

                            string rcardCheck = this.txtRCardEdit.Value.Trim().ToUpper();
                            if (!string.IsNullOrEmpty(checkPrefix))
                            {
                                if (rcardCheck.IndexOf(checkPrefix) != 0)
                                {
                                    // 该序列号与工单要求的序列号前缀不一样     
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardPrefix_ERROR  $CS_RunningCardPrefix: " + checkPrefix));
                                    this.txtRCardEdit.TextFocus(false, true);
                                    return;
                                }
                            }
                            if (checkLength != null && checkLength > 0)
                            {
                                if (rcardCheck.Length != checkLength)
                                {
                                    // 该序列号与工单要求的序列号长度不一样   
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_RunningCardLength_ERROR  $CS_RunningCardLength: " + checkLength));
                                    this.txtRCardEdit.TextFocus(false, true);
                                    return;
                                }
                            }
                            if (checkContent != null && checkContent == "Y")
                            {
                                string pattern = @"^([A-Za-z0-9]+[ ]*)*[A-Za-z0-9]+$";
                                Regex rex = new Regex(pattern, RegexOptions.IgnoreCase);
                                Match match = rex.Match(rcardCheck);
                                // 序列号被限制为字符 空格 数字  
                                if (!match.Success)
                                {
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SNContent_CheckWrong $CS_Param_RunSeq:" + this.txtRCardEdit.Value.Trim()));
                                    this.txtRCardEdit.TextFocus(false, true);
                                    return;
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region  Qty
                    try
                    {
                        decimal moQty = 0;
                        int intQty = Convert.ToInt32(this.txtRelationQtyEdit.Value);
                        if (intQty <= 0)
                        {
                            //数量应为整数类型 $Error_Qty_Not_Int
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_SmtRelQty_Should_Over_Zero"));
                            this.txtRelationQtyEdit.TextFocus(false, true);
                            return;
                        }
                        if (objMO != null && ((MO)objMO).IDMergeRule >= 0)
                        {
                            moQty = ((MO)objMO).IDMergeRule;
                        }
                        if (moQty < Convert.ToDecimal(intQty))
                        {
                            //数量应小于工单的连板数 $Error_Qty_Not_Int
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_SmtRelQty_Should_less_MO" + ": " + moQty));
                            this.txtRelationQtyEdit.TextFocus(false, true);
                            return;
                        }
                    }
                    catch
                    {
                        //数量应为整数类型 $Error_Qty_Not_Int
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_Qty_Not_Int"));
                        this.txtRelationQtyEdit.TextFocus(false, true);
                        return;
                    }
                    #endregion

                    #region      Smtrelationqty
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
                    if (smtfacade.GetSMTRelationQty(this.txtRCardEdit.Value.Trim().ToUpper(), this.txtMoCodeEdit.Value.Trim().ToUpper()) != null)
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_CS_RCard_Exist"));
                        this.txtRCardEdit.TextFocus(false, true);
                        return;
                    }
                    Smtrelationqty smtqty = smtfacade.CreateNewSmtrelationqty();
                    smtqty.Rcard = this.txtRCardEdit.Value.Trim().ToUpper();
                    smtqty.Mocode = this.txtMoCodeEdit.Value.Trim().ToUpper();
                    smtqty.Itemcode = ((MO)mofacade.GetMO(smtqty.Mocode)).ItemCode;
                    smtqty.Relationqtry = int.Parse(this.txtRelationQtyEdit.Value);
                    smtqty.Muser = ApplicationService.Current().UserCode;
                    smtqty.Mdate = dbDateTime.DBDate;
                    smtqty.Mtime = dbDateTime.DBTime;
                    smtqty.Eattribute1 = "";
                    smtqty.Eattribute2 = "";
                    smtqty.Eattribute3 = "";
                    smtqty.Memo = "";
                    smtfacade.AddSmtrelationqty(smtqty);
                    #endregion
                }
                catch (Exception ex)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, ex.Message));
                    return;
                }
                btnQuery_Click(sender, e);
            }
        }
        #endregion

      


    }
}