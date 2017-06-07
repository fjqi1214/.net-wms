using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.OQC;
using BenQGuru.eMES.Domain.OQC;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Domain.Package;
using UserControl;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.Warehouse;

namespace BenQGuru.eMES.Client
{
    public partial class FOQCLotQA : BaseForm
    {
        private string m_LotNo = string.Empty;

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }
        DataCollect.DataCollectFacade dcf = null;
        private DataSet m_CheckList = null;
        private System.Data.DataTable dt_CartonGroup = new DataTable("CartonGroup");
        private System.Data.DataTable dt_CartonDetail = new DataTable("CartonDetail");
        private OQC.OQCFacade _oqcFacade = null;
        private MOModel.ItemFacade _itemFacade = null;
        private Material.WarehouseFacade warehouseFacade = null;


        public FOQCLotQA()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
        }

        public bool CheckIsSameOQCLot(object objLot)
        {
            //add by klaus 20130520 将产生的lotno存入tblserialbook，如果已经存在进行更新，不存在进行新增
            //防止为提交lotno，另一个也产生了同样的lotno
            if (warehouseFacade == null)
            {
                warehouseFacade = new Material.WarehouseFacade(this.DataProvider);
            }
            string oqcPrefix = (objLot as OQCLot).LOTNO.Substring(0, (objLot as OQCLot).LOTNO.Length - 3);
            string oqcSequence = (Convert.ToString((int.Parse((objLot as OQCLot).LOTNO.Substring((objLot as OQCLot).LOTNO.Length - 3, 3))))).PadLeft(3, '0');
            object objSerialBook = warehouseFacade.GetSerialBook(oqcPrefix);

            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            SERIALBOOK serialBook = new SERIALBOOK();
            serialBook.SNPrefix = oqcPrefix;
            serialBook.MaxSerial = oqcSequence;
            serialBook.MDate = dbTime.DBDate;
            serialBook.MTime = dbTime.DBTime;
            serialBook.MUser = ApplicationService.Current().UserCode;
            if (objSerialBook != null)
            {
                //update
                warehouseFacade.UpdateSerialBook(serialBook);
                return false;
            }
            else
            {
                //add
                warehouseFacade.AddSerialBook(serialBook);
                return true;
            }
            //end
        }
        public string CreateNewOQCLot()
        {
            OQCFacade _oqcFacade = new OQCFacade(DataProvider);
            warehouseFacade=new Material.WarehouseFacade(this.DataProvider);
            OQCLot newOQCLot = _oqcFacade.CreateNewOQCLot();
            int shiftDay = FormatHelper.TODateInt(DateTime.Now);
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            //get lot entity
            object objLot = (new OQCFacade(DataProvider)).GetNewLotNO("OQC", shiftDay.ToString());

            if (objLot == null)
            {
                string prefix = "OQC" + shiftDay.ToString() + "L";
                object obj = warehouseFacade.GetSerialBook(prefix);
                if (obj == null)
                {
                    newOQCLot.LOTNO = "OQC" + shiftDay.ToString() + "L" + "001";
                    //产生一个新Lotno，保存
                    SERIALBOOK serialbook = new SERIALBOOK();
                    serialbook.SNPrefix = prefix;
                    serialbook.MaxSerial = "001";
                    serialbook.MDate = dbTime.DBDate;
                    serialbook.MTime = dbTime.DBTime;
                    serialbook.MUser = ApplicationService.Current().UserCode;
                    warehouseFacade.AddSerialBook(serialbook);
                }
                else
                {
                    SERIALBOOK serialBook = (SERIALBOOK)warehouseFacade.GetSerialBook(prefix);
                    serialBook.MaxSerial = Convert.ToString(int.Parse(serialBook.MaxSerial) + 1);
                    serialBook.MDate = dbTime.DBDate;
                    serialBook.MTime = dbTime.DBTime;
                    serialBook.MUser = ApplicationService.Current().UserCode;
                    warehouseFacade.UpdateSerialBook(serialBook);
                    newOQCLot.LOTNO = "OQC" + shiftDay.ToString() + "L" + serialBook.MaxSerial.PadLeft(3,'0');
                }
                

            }
            else
            {
                string oldLotNO = (objLot as OQCLot).LOTNO;
                string prefix = oldLotNO.Substring(0, oldLotNO.Length - 3);
                //string newSequence = Convert.ToString((int.Parse(oldLotNO.Substring(oldLotNO.Length - 3, 3)) + 1));
                SERIALBOOK serialBook = (SERIALBOOK)warehouseFacade.GetSerialBook(prefix);
                serialBook.MDate = dbTime.DBDate;
                serialBook.MTime = dbTime.DBTime;
                serialBook.MUser = ApplicationService.Current().UserCode;
                
                string newSequence = Convert.ToString(int.Parse(serialBook.MaxSerial)+1);
                serialBook.MaxSerial = newSequence;
                newOQCLot.LOTNO = oldLotNO.Substring(0, oldLotNO.Length - 3) + newSequence.PadLeft(3, '0');

                warehouseFacade.UpdateSerialBook(serialBook);
            }

            return newOQCLot.LOTNO;
        }

        private void UpdateSimulationAndSimulationReport(string lotNo, string rCard)
        {
            if (dcf == null)
            {
                dcf = new DataCollectFacade(this.DataProvider);
            }
            object objSimulation = dcf.GetSimulation(rCard);
            Domain.DataCollect.Simulation simulation = null;
            Domain.DataCollect.SimulationReport simulationReport = null;
            if (objSimulation != null)
            {
                simulation = objSimulation as Domain.DataCollect.Simulation;
                simulation.LOTNO = lotNo;
                dcf.UpdateSimulation(simulation);
            }
            object objSimulationReport = dcf.GetSimulationReport(simulation.MOCode, rCard);
            if (objSimulationReport != null)
            {
                simulationReport = objSimulationReport as SimulationReport;
                simulationReport.LOTNO = lotNo;
                dcf.UpdateSimulationReport(simulationReport);
            }
        }

        private void DeleteSimulationAndSimulationReport(string rCard)
        {
            if (dcf == null)
            {
                dcf = new DataCollectFacade(this.DataProvider);
            }
            object[] objs = new object[2];
            object objSimulation = dcf.GetSimulation(rCard);
            Domain.DataCollect.Simulation simulation = null;
            Domain.DataCollect.SimulationReport simulationReport = null;
            if (objSimulation != null)
            {
                simulation = objSimulation as Domain.DataCollect.Simulation;
                simulation.LOTNO = "";
                dcf.UpdateSimulation(simulation);
            }
            object objSimulationReport = dcf.GetSimulationReport(simulation.MOCode, rCard);
            if (objSimulationReport != null)
            {
                simulationReport = objSimulationReport as SimulationReport;
                simulationReport.LOTNO = "";
                dcf.UpdateSimulationReport(simulationReport);
            }
        }

        private bool GetDateByLotNo(string lotno)
        {
            InitializeMainGrid();
            if (_oqcFacade == null)
            {
                _oqcFacade = new OQCFacade(this.DataProvider);
            }
            object[] objs = _oqcFacade.GetLot2CartonByLot(lotno);

            if (objs == null)
            {
                Clear();
                return false;
            }

            this.ClearCheckList();
            DataRow drGroup;
            foreach (object lot2Carton in objs)
            {
                PackageFacade packageFacade = new PackageFacade(this.DataProvider);
                object[] cartonObjs = packageFacade.GetCarton2RCARDByCartonNO(((Lot2Carton)lot2Carton).CartonNo);

                drGroup = this.m_CheckList.Tables["CartonGroup"].NewRow();
                drGroup["Checked"] = "true";
                drGroup["MoCode"] = ((Lot2Carton)lot2Carton).MOCode;
                drGroup["CartonNo"] = ((Lot2Carton)lot2Carton).CartonNo;
                drGroup["CartonQTY"] = (cartonObjs == null) ? "0" : cartonObjs.Length.ToString();

                this.m_CheckList.Tables["CartonGroup"].Rows.Add(drGroup);

                if (cartonObjs != null)
                {
                    DataRow drDetail;
                    foreach (object cartonObj in cartonObjs)
                    {
                        drDetail = this.m_CheckList.Tables["CartonDetail"].NewRow();
                        drDetail["Checked"] = "true";
                        drDetail["CartonNo"] = ((Carton2RCARD)cartonObj).CartonCode;
                        drDetail["RCard"] = ((Carton2RCARD)cartonObj).Rcard;

                        this.m_CheckList.Tables["CartonDetail"].Rows.Add(drDetail);
                    }
                }

            }
            this.m_CheckList.Tables["CartonGroup"].AcceptChanges();
            this.m_CheckList.Tables["CartonDetail"].AcceptChanges();
            this.m_CheckList.AcceptChanges();
            this.ultraGridLot2CardList.DataSource = this.m_CheckList;
            this.ultraGridLot2CardList.UpdateData();

            this.ucLabelEditInputCarton.TextFocus(false, true);
            return true;
        }

        private void InitializeMainGrid()
        {
            this.m_CheckList = new DataSet();

            this.dt_CartonGroup = new DataTable("CartonGroup");
            this.dt_CartonDetail = new DataTable("CartonDetail");
            this.dt_CartonGroup.Columns.Add("Checked", typeof(string));
            this.dt_CartonGroup.Columns.Add("MoCode", typeof(string));
            this.dt_CartonGroup.Columns.Add("CartonNo", typeof(string));
            this.dt_CartonGroup.Columns.Add("CartonQTY", typeof(string));

            this.dt_CartonDetail.Columns.Add("Checked", typeof(string));
            this.dt_CartonDetail.Columns.Add("CartonNo", typeof(string));
            this.dt_CartonDetail.Columns.Add("RCard", typeof(string));

            this.m_CheckList.Tables.Add(this.dt_CartonGroup);
            this.m_CheckList.Tables.Add(this.dt_CartonDetail);
            this.m_CheckList.Relations.Add(new DataRelation("CartonGroupAndCartonDetail",
                                                this.m_CheckList.Tables["CartonGroup"].Columns["CartonNo"],
                                                this.m_CheckList.Tables["CartonDetail"].Columns["CartonNo"]));
            this.m_CheckList.AcceptChanges();
            this.ultraGridLot2CardList.DataSource = this.m_CheckList;
        }


        private void ultraGridMain_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
        {
            // 自适应列宽
            e.Layout.AutoFitColumns = false;
            e.Layout.Override.AllowColSizing = AllowColSizing.Free;
            e.Layout.ScrollBounds = ScrollBounds.ScrollToFill;

            //e.Layout.MaxBandDepth = 1;
            // 自动判断是否显示前面的+、-号
            e.Layout.Override.ExpansionIndicator = ShowExpansionIndicator.CheckOnDisplay;

            // 设置Grid的Split窗口个数，建议设置为1--不允许Split
            e.Layout.MaxColScrollRegions = 1;
            e.Layout.MaxRowScrollRegions = 1;

            // 冻结列
            e.Layout.UseFixedHeaders = true;
            e.Layout.Override.FixedHeaderAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedHeaderAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedCellAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FixedCellAppearance.ForeColor = Color.Blue;
            e.Layout.Override.FixedHeaderIndicator = FixedHeaderIndicator.None;

            // 排序
            e.Layout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;

            // 不允许删除
            e.Layout.Override.AllowDelete = DefaultableBoolean.False;

            // 滚动提示
            e.Layout.Bands[0].ScrollTipField = "CartonNo";
            e.Layout.Bands[1].ScrollTipField = "RCard";

            // 设置列宽和列名称
            e.Layout.Bands[0].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[0].Columns["MOCode"].Header.Caption = "工单";
            e.Layout.Bands[0].Columns["CartonNo"].Header.Caption = "箱号";
            e.Layout.Bands[0].Columns["CartonQTY"].Header.Caption = "数量";

            e.Layout.Bands[0].Columns["Checked"].Width = 40;
            e.Layout.Bands[0].Columns["MOCode"].Width = 100;
            e.Layout.Bands[0].Columns["CartonNo"].Width = 100;
            e.Layout.Bands[0].Columns["CartonQTY"].Width = 60;


            // 设置栏位是否允许编辑，及栏位的显示形式
            e.Layout.Bands[0].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[0].Columns["MOCode"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.ActivateOnly;
            e.Layout.Bands[0].Columns["CartonNo"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[0].Columns["CartonQTY"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            //设置可编辑列的颜色
            //e.Layout.Bands[0].Columns["DATECODE"].CellAppearance.BackColor = Color.LawnGreen;
            //e.Layout.Bands[0].Columns["VenderLotNO"].CellAppearance.BackColor = Color.LawnGreen ;
            //e.Layout.Bands[0].Columns["VenderITEMCODE"].CellAppearance.BackColor = Color.LawnGreen;



            // 允许筛选
            e.Layout.Bands[0].Columns["CartonNo"].AllowRowFiltering = DefaultableBoolean.True;
            e.Layout.Bands[0].Columns["CartonNo"].SortIndicator = SortIndicator.Ascending;
            // 允许冻结，且Checked栏位始终处于冻结状态，不可更改
            //e.Layout.Bands[0].Columns["Checked"].Header.Fixed = true;
            //e.Layout.Bands[0].Columns["LotNO"].Header.Fixed = true;
            //e.Layout.Bands[0].Columns["LotNO"].Header.FixedHeaderIndicator = FixedHeaderIndicator.Button;
            //e.Layout.Bands[0].Columns["LotNO"].SortIndicator = SortIndicator.Ascending;

            // CheckItem

            e.Layout.Bands[1].Columns["Checked"].Header.Caption = "";
            e.Layout.Bands[1].Columns["CartonNo"].Header.Caption = "箱号";
            e.Layout.Bands[1].Columns["RCard"].Header.Caption = "产品序列号";

            e.Layout.Bands[1].Columns["CartonNo"].Hidden = true;
            e.Layout.Bands[1].Columns["Checked"].Width = 40;
            e.Layout.Bands[1].Columns["RCard"].Width = 160;



            e.Layout.Bands[1].Columns["Checked"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            e.Layout.Bands[1].Columns["Checked"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            e.Layout.Bands[1].Columns["RCard"].CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;

            e.Layout.Bands[1].Columns["RCard"].SortIndicator = SortIndicator.Ascending;

            this.InitGridLanguage(ultraGridLot2CardList);
        }

        private void ultraGridRCardList_CellChange(object sender, CellEventArgs e)
        {
            this.ultraGridLot2CardList.UpdateData();
            if (e.Cell.Column.Key == "Checked")
            {
                if (e.Cell.Row.Band.Index == 0) //Parent
                {
                    for (int i = 0; i < e.Cell.Row.ChildBands[0].Rows.Count; i++)
                    {
                        e.Cell.Row.ChildBands[0].Rows[i].Cells["Checked"].Value = e.Cell.Value;
                    }
                }

                if (e.Cell.Row.Band.Index == 1) // Child
                {
                    if (Convert.ToBoolean(e.Cell.Value) == true)
                    {
                        e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                    }
                    else
                    {
                        bool needUnCheckHeader = true;
                        for (int i = 0; i < e.Cell.Row.ParentRow.ChildBands[0].Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(e.Cell.Row.ParentRow.ChildBands[0].Rows[i].Cells["Checked"].Value) == true)
                            {
                                needUnCheckHeader = false;
                                break;
                            }
                        }
                        if (needUnCheckHeader)
                        {
                            e.Cell.Row.ParentRow.Cells["Checked"].Value = e.Cell.Value;
                        }
                    }
                }
            }
            this.ultraGridLot2CardList.UpdateData();

        }

        //捕捉回车事件
        private void ucLabelEditInput_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditLotNo.Value.Trim() == "")
                {
                    this.ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }

                if (this.ucLabelEditInputCarton.Value.Trim() == "")
                {
                    this.ucLabelEditInputCarton.TextFocus(false, true);
                    return;
                }

                PackageFacade packageFacade = new PackageFacade(this.DataProvider);
                //判断输入的箱号是否存在
                object[] carton2RCARDObjs = packageFacade.GetCarton2RCARDByCartonNO(this.ucLabelEditInputCarton.Value.Trim());
                if (carton2RCARDObjs == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CartonCode_Not_Exists"));
                    this.ucLabelEditInputCarton.TextFocus(false, true);
                    return;
                }
                string moCode = ((Carton2RCARD)carton2RCARDObjs[0]).MOCode;

                if (_oqcFacade == null)
                {
                    _oqcFacade = new OQCFacade(this.DataProvider);
                }

                //判断箱号是否已经归属
                object lot2CartonObj = _oqcFacade.GetLot2CartonByCartonNo(((Carton2RCARD)carton2RCARDObjs[0]).CartonCode);
                if (lot2CartonObj != null)
                {
                    Domain.OQC.OQCLot Lot = _oqcFacade.GetOQCLot(((Lot2Carton)lot2CartonObj).OQCLot, OQCFacade.Lot_Sequence_Default) as Domain.OQC.OQCLot;
                    if (!(Lot.LOTStatus == "oqclotstatus_reject"))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CartonCode_Has_In_Lot"));
                        this.ucLabelEditInputCarton.TextFocus(false, true);
                        return;
                    }
                }


                this.DataProvider.BeginTransaction();
                try
                {
                    DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                    BaseSetting.BaseModelFacade dataModel = new BaseSetting.BaseModelFacade(this.DataProvider);
                    Domain.BaseSetting.Resource res = (Domain.BaseSetting.Resource)dataModel.GetResource(ApplicationService.Current().ResourceCode);

                    BaseSetting.ShiftModelFacade shiftModel = new BaseSetting.ShiftModelFacade(this.DataProvider);
                    Domain.BaseSetting.TimePeriod period = (Domain.BaseSetting.TimePeriod)shiftModel.GetTimePeriod(res.ShiftTypeCode, Web.Helper.FormatHelper.TOTimeInt(DateTime.Now));

                    //根据lotno  查找lot  送检批信息
                    string lotNo = this.ucLabelEditLotNo.Value.Trim();
                    object lot = _oqcFacade.GetOQCLot(lotNo, OQCFacade.Lot_Sequence_Default);
                    Domain.OQC.OQCLot lottemp = null;
                    //若为null，为新批，则新增lot
                    if (lot == null)
                    {
                        //根据MOCode查找itemCode
                        string itemCode = string.Empty;
                        object itemObj = packageFacade.GetItemCodeByMOCode(moCode);
                        if (itemObj != null)
                        {
                            itemCode = ((CartonCollection)itemObj).ItemCode;
                        }


                        int shiftDay = 0;
                        if (period == null)
                        {
                            this.ucLabelEditInputCarton.TextFocus(false, true);
                            throw new Exception("$OutOfPerid");
                        }

                        if (period.IsOverDate == Web.Helper.FormatHelper.TRUE_STRING)
                        {
                            if (period.TimePeriodBeginTime < period.TimePeriodEndTime)
                            {
                                shiftDay = FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                            }
                            else if (Web.Helper.FormatHelper.TOTimeInt(DateTime.Now) < period.TimePeriodBeginTime)
                            {
                                shiftDay = FormatHelper.TODateInt(DateTime.Now.AddDays(-1));
                            }
                            else
                            {
                                shiftDay = FormatHelper.TODateInt(DateTime.Now);
                            }
                        }
                        else
                        {
                            shiftDay = FormatHelper.TODateInt(DateTime.Now);
                        }


                        Domain.OQC.OQCLot oqcLot = _oqcFacade.CreateNewOQCLot();
                        oqcLot.LOTNO = lotNo;
                        oqcLot.LotSequence = OQCFacade.Lot_Sequence_Default;
                        oqcLot.ItemCode = itemCode;
                        oqcLot.OQCLotType = "oqclottype_normal";
                        oqcLot.LOTStatus = "oqclotstatus_initial";
                        oqcLot.ProductionType = "productiontype_mass";
                        oqcLot.OrganizationID = ApplicationService.Current().LoginInfo.Resource.OrganizationID;
                        oqcLot.LotCapacity = 0;
                        oqcLot.MaintainUser = ApplicationService.Current().UserCode;
                        oqcLot.MaintainDate = dbDateTime.DBDate;
                        oqcLot.MaintainTime = dbDateTime.DBTime;
                        oqcLot.CreateDate = dbDateTime.DBDate;
                        oqcLot.CreateTime = dbDateTime.DBTime;
                        oqcLot.CreateUser = ApplicationService.Current().UserCode;
                        oqcLot.AcceptSize = 0;
                        oqcLot.AcceptSize1 = 0;
                        oqcLot.AcceptSize2 = 0;
                        oqcLot.AcceptSize3 = 0;
                        oqcLot.AQL = 0;
                        oqcLot.AQL1 = 0;
                        oqcLot.AQL2 = 0;
                        oqcLot.AQL3 = 0;
                        oqcLot.LOTTimes = 0;
                        oqcLot.LotSize = 1;
                        oqcLot.SampleSize = 0;
                        oqcLot.ShiftCode = period.ShiftCode;
                        oqcLot.ShiftDay = shiftDay;
                        oqcLot.SSCode = ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
                        oqcLot.ResourceCode = ApplicationService.Current().ResourceCode;
                        _oqcFacade.AddOQCLot(oqcLot);

                        lottemp = oqcLot;
                    }
                    else
                    {
                        //不为null ，则判断rCard 所在lot状态

                        //判断当前Lot状态的是否为初始化、送检、未检、在检状态，否则不可以进行添加和删除操作
                        lottemp = (Domain.OQC.OQCLot)lot;
                        if (lottemp.LOTStatus != OQCLotStatus.OQCLotStatus_Initial && lottemp.LOTStatus != OQCLotStatus.OQCLotStatus_SendExame && lottemp.LOTStatus != OQCLotStatus.OQCLotStatus_NoExame && lottemp.LOTStatus != OQCLotStatus.OQCLotStatus_Examing)
                        {
                            this.DataProvider.RollbackTransaction();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LotStatus_Not_Initial_SendExame_NoExame_Examing"));

                            this.ucLabelEditLotNo.TextFocus(false, true);
                            return;
                        }
                    }


                    if (dcf == null)
                    {
                        dcf = new DataCollectFacade(this.DataProvider);
                    }

                    //循环箱中的序列号，对每个序列号进行判断操作
                    foreach (Carton2RCARD carton2RCARD in carton2RCARDObjs)
                    {
                        string rCard = carton2RCARD.Rcard;
                        //判断下一个工序是否为FQC工序
                        object objsim = dcf.GetSimulation(rCard);
                        if (objsim != null)
                        {
                            Simulation sim = objsim as Simulation;

                            //判断产品是否为NG状态
                            if (sim.ProductStatus == "NG")
                            {
                                this.DataProvider.RollbackTransaction();
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_ProductStatus_Not_GOOD"));
                                this.ucLabelEditInputCarton.TextFocus(false, true);
                                return;
                            }
                            else
                            {

                                if (_itemFacade == null)
                                {
                                    _itemFacade = new ItemFacade(this.DataProvider);
                                }
                                object[] objItemRoute2ops = _itemFacade.QueryItemRoute2Op(sim.RouteCode, sim.ItemCode);

                                bool flag = false;

                                for (int i = 0; i < objItemRoute2ops.Length; i++)
                                {
                                    Domain.MOModel.ItemRoute2OP itemRoute2op = objItemRoute2ops[i] as Domain.MOModel.ItemRoute2OP;

                                    if (itemRoute2op.OPCode == sim.OPCode)
                                    {
                                        if (i + 1 < objItemRoute2ops.Length)
                                        {
                                            itemRoute2op = objItemRoute2ops[i + 1] as Domain.MOModel.ItemRoute2OP;

                                            if (itemRoute2op.OPControl.Substring(4, 1) == "1")
                                            {
                                                flag = true;
                                            }
                                        }
                                        break;
                                    }
                                }

                                if (!flag)
                                {
                                    this.DataProvider.RollbackTransaction();
                                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Product_NOT_FINISH"));
                                    this.ucLabelEditInputCarton.TextFocus(false, true);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist"));

                            this.ucLabelEditInputCarton.TextFocus(false, true);
                            return;
                        }


                        //根据RCard获取Smimulation
                        object objSimulation = dcf.GetSimulation(rCard);
                        Domain.DataCollect.Simulation simulation = null;
                        if (objSimulation != null)
                        {

                            //#region 检查当前资源所属的产线和序号所属的产线是否一致
                            //SimulationReport objSimulationReport = dcf.GetLastSimulationReport(rCard);

                            //if (objSimulationReport != null)
                            //{
                            //    if (objSimulationReport.StepSequenceCode != res.StepSequenceCode)
                            //    {
                            //        this.DataProvider.RollbackTransaction();
                            //        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_StepSequenceCode_Different"));
                            //        this.ucLabelEditInput.TextFocus(false, true);
                            //        return;
                            //    }
                            //}
                            //else
                            //{
                            //    this.DataProvider.RollbackTransaction();
                            //    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_StepSequenceCode_NOT_EXIST"));
                            //    this.ucLabelEditInput.TextFocus(false, true);
                            //    return;
                            //}
                            //#endregion

                            simulation = objSimulation as Domain.DataCollect.Simulation;

                            #region 判断序列号中itemcode,mocode是否与批中的一致
                            if (this.ucLabelEditItemCodeQuery.Value.Trim() != "" && this.ucLabelEditItemCodeQuery.Value.Trim() != simulation.ItemCode)
                            {
                                this.DataProvider.RollbackTransaction();
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_Different"));

                                this.ucLabelEditInputCarton.TextFocus(false, true);
                                return;
                            }
                            else
                            {
                                this.ucLabelEditItemCodeQuery.Value = simulation.ItemCode;
                            }
                            if (this.ucLabelEditMoCodeQuery.Value.Trim() != "" && this.ucLabelEditMoCodeQuery.Value.Trim() != simulation.MOCode)
                            {
                                this.DataProvider.RollbackTransaction();
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_MOCode_Different"));

                                this.ucLabelEditInputCarton.TextFocus(false, true);
                                return;
                            }
                            else
                            {
                                this.ucLabelEditMoCodeQuery.Value = simulation.MOCode;
                            }
                            #endregion
                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$Error_ItemCode_NotExist"));

                            this.ucLabelEditInputCarton.TextFocus(false, true);
                            return;
                        }


                        #region 判断序列号是否已经归属
                        object oqclost2cards = _oqcFacade.GetOQCLot2Card(rCard, this.ucLabelEditMoCodeQuery.Value.Trim(), "", "");
                        if (oqclost2cards != null)
                        {
                            bool hasJudge = true;
                            bool hasReject = true;
                            Domain.OQC.OQCLot2Card lot2card = oqclost2cards as OQCLot2Card;

                            string templotno = lot2card.LOTNO;
                            Domain.OQC.OQCLot tempLot = _oqcFacade.GetOQCLot(templotno, OQCFacade.Lot_Sequence_Default) as Domain.OQC.OQCLot;
                            if (!(tempLot.LOTStatus == "oqclotstatus_reject"))
                            {
                                hasReject = false;
                                if (!(tempLot.LOTStatus == "oqclotstatus_pass"))
                                {
                                    hasJudge = false;
                                }
                            }

                            if (!hasJudge)
                            {
                                //object objlot = _oqcFacade.GetOQCLot(this.ucLabelEditLotNo.Value.Trim(), OQCFacade.Lot_Sequence_Default);
                                object[] objs = _oqcFacade.GetOQCLot2CardByLotNoAndSeq(FormatHelper.PKCapitalFormat(this.ucLabelEditLotNo.Value.Trim()), OQCFacade.Lot_Sequence_Default);
                                if (objs == null || objs.Length == 0)
                                {

                                    this.ucLabelEditItemCodeQuery.Value = "";
                                    this.ucLabelEditMoCodeQuery.Value = "";
                                }
                                this.DataProvider.RollbackTransaction();
                                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_OQCLOT2Card_Exist"));

                                this.ucLabelEditInputCarton.TextFocus(false, true);
                                return;
                            }
                            if (hasReject)
                            {
                                //新增序列号到TBLLot2Card
                                object obj = _oqcFacade.CreateNewOQCLot2Card();
                                Domain.OQC.OQCLot2Card lot2Card = (Domain.OQC.OQCLot2Card)obj;
                                lot2Card.RunningCard = rCard;
                                lot2Card.ItemCode = simulation.ItemCode;
                                lot2Card.CollectType = "pcs";
                                lot2Card.LOTNO = this.ucLabelEditLotNo.Value.Trim();
                                lot2Card.LotSequence = OQCFacade.Lot_Sequence_Default;
                                lot2Card.MaintainDate = dbDateTime.DBDate;
                                lot2Card.MaintainTime = dbDateTime.DBTime;
                                lot2Card.MaintainUser = ApplicationService.Current().UserCode;
                                lot2Card.MOCode = simulation.MOCode;
                                lot2Card.ModelCode = simulation.ModelCode;
                                lot2Card.MOSeq = simulation.MOSeq;
                                lot2Card.OPCode = simulation.OPCode;
                                lot2Card.ResourceCode = simulation.ResourceCode;
                                lot2Card.RouteCode = simulation.RouteCode;
                                lot2Card.RunningCardSequence = simulation.RunningCardSequence;
                                lot2Card.Status = "GOOD";
                                lot2Card.ShiftCode = (period != null) ? period.ShiftCode : " ";
                                lot2Card.ShiftTypeCode = res.ShiftTypeCode;
                                lot2Card.SegmnetCode = res.SegmentCode;
                                lot2Card.StepSequenceCode = res.StepSequenceCode;
                                lot2Card.TimePeriodCode = (period != null) ? period.TimePeriodCode : " ";
                                lot2Card.EAttribute1 = this.ucLabelEditInputCarton.Value.Trim();
                                _oqcFacade.AddOQCLot2Card(lot2Card);
                                this.ucButtonLot.Enabled = true;

                                //更新SimulationAndSimulationReport in lotno
                                UpdateSimulationAndSimulationReport(lotNo, rCard);
                            }

                        }
                        else
                        {
                            //新增序列号到TBLLot2Card
                            object obj = _oqcFacade.CreateNewOQCLot2Card();
                            Domain.OQC.OQCLot2Card lot2Card = (Domain.OQC.OQCLot2Card)obj;
                            lot2Card.RunningCard = rCard;
                            lot2Card.ItemCode = simulation.ItemCode;
                            lot2Card.CollectType = "pcs";
                            lot2Card.LOTNO = this.ucLabelEditLotNo.Value.Trim();
                            lot2Card.LotSequence = OQCFacade.Lot_Sequence_Default;
                            lot2Card.MaintainDate = dbDateTime.DBDate;
                            lot2Card.MaintainTime = dbDateTime.DBTime;
                            lot2Card.MaintainUser = ApplicationService.Current().UserCode;
                            lot2Card.MOCode = simulation.MOCode;
                            lot2Card.ModelCode = simulation.ModelCode;
                            lot2Card.MOSeq = simulation.MOSeq;
                            lot2Card.OPCode = simulation.OPCode;
                            lot2Card.ResourceCode = simulation.ResourceCode;
                            lot2Card.RouteCode = simulation.RouteCode;
                            lot2Card.RunningCardSequence = simulation.RunningCardSequence;
                            lot2Card.Status = "GOOD";
                            lot2Card.ShiftCode = (period != null) ? period.ShiftCode : " ";
                            lot2Card.ShiftTypeCode = res.ShiftTypeCode;
                            lot2Card.SegmnetCode = res.SegmentCode;
                            lot2Card.StepSequenceCode = res.StepSequenceCode;
                            lot2Card.TimePeriodCode = (period != null) ? period.TimePeriodCode : " ";
                            lot2Card.EAttribute1 = this.ucLabelEditInputCarton.Value.Trim();
                            _oqcFacade.AddOQCLot2Card(lot2Card);
                            this.ucButtonLot.Enabled = true;

                            //更新SimulationAndSimulationReport in lotno
                            UpdateSimulationAndSimulationReport(lotNo, rCard);
                        }
                        #endregion

                    }

                    //更新lot中总数量
                    lottemp.LotSize = _oqcFacade.QueryOQCLot2CardCountByLotNo(lotNo);
                    _oqcFacade.UpdateOQCLot(lottemp);
                    this.ucLabelEditCapacity.Value = lottemp.LotSize.ToString();

                    //新增箱到TBLLot2Carton
                    Lot2Carton lot2Carton = new Lot2Carton();
                    lot2Carton.OQCLot = this.ucLabelEditLotNo.Value.Trim();
                    lot2Carton.CartonNo = this.ucLabelEditInputCarton.Value.Trim();
                    lot2Carton.MOCode = moCode;
                    lot2Carton.ItemCode = lottemp.ItemCode;
                    lot2Carton.AddUser = ApplicationService.Current().UserCode;
                    lot2Carton.AddDate = dbDateTime.DBDate;
                    lot2Carton.AddTime = dbDateTime.DBTime;
                    _oqcFacade.AddLot2Carton(lot2Carton);

                    //新增箱写入log
                    Lot2CartonLog lot2CartonLog = new Lot2CartonLog();
                    lot2CartonLog.OQCLot = this.ucLabelEditLotNo.Value.Trim();
                    lot2CartonLog.CartonNo = this.ucLabelEditInputCarton.Value.Trim();
                    lot2CartonLog.MOCode = moCode;
                    lot2CartonLog.ItemCode = lottemp.ItemCode;
                    lot2CartonLog.AddUser = ApplicationService.Current().UserCode;
                    lot2CartonLog.AddDate = dbDateTime.DBDate;
                    lot2CartonLog.AddTime = dbDateTime.DBTime;
                    _oqcFacade.AddLot2CartonLog(lot2CartonLog);

                    this.DataProvider.CommitTransaction();

                    //数据重新绑定
                    if (this.ucLabelEditLotNo.Value.Trim() == "")
                    {
                        this.ucLabelEditInputCarton.TextFocus(false, true);
                        return;
                    }
                    else
                    {
                        GetDateByLotNo(this.ucLabelEditLotNo.Value.Trim());

                        Messages msg = this.LoadLotInfo();
                        if (!msg.IsSuccess())
                        {
                            ApplicationRun.GetInfoForm().Add(msg);
                        }
                    }

                    this.ucLabelEditInputCarton.TextFocus(false, true);
                }
                catch (Exception ex)
                {
                    this.ucLabelEditInputCarton.TextFocus(false, true);
                    this.DataProvider.RollbackTransaction();
                }
            }
        }

        //捕捉回车事件
        private void ucLabelEditLotNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.ucLabelEditLotNo.Value.Trim() == "")
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));
                    this.ucLabelEditLotNo.TextFocus(false, true);
                    return;
                }
                else
                {
                    if (!GetDateByLotNo(this.ucLabelEditLotNo.Value.Trim()))
                    {
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                        this.ucLabelEditLotNo.TextFocus(false, true);
                        return;
                    }


                    Messages msg = this.LoadLotInfo();
                    if (!msg.IsSuccess())
                    {
                        ApplicationRun.GetInfoForm().Add(msg);
                    }
                    else
                    {
                        this.ucLabelEditRCard.Value = "";
                        this.ucButtonLot.Enabled = true;
                    }
                }
            }
        }

        //捕捉回车事件
        private void ucLabelEditRCard_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.GetLotNo();
            }
        }

        //捕捉回车事件
        private void ucLabelEditCartonNo_TxtboxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.GetLotNo();
            }
        }

        //全选按钮状态改变时
        private void checkBoxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                for (int i = 0; i < ultraGridLot2CardList.Rows.Count; i++)
                {
                    this.ultraGridLot2CardList.Rows[i].Cells["Checked"].Value = this.checkBoxSelectAll.Checked;
                    for (int j = 0; j < this.ultraGridLot2CardList.Rows[i].ChildBands[0].Rows.Count; j++)
                    {
                        this.ultraGridLot2CardList.Rows[i].ChildBands[0].Rows[j].Cells["Checked"].Value = this.checkBoxSelectAll.Checked;
                    }
                }

                this.ultraGridLot2CardList.UpdateData();

            }
            finally
            {
                this.Cursor = System.Windows.Forms.Cursors.Arrow;
            }
        }


        #region ButtionEvents

        private void ucButtonNewLot_Click(object sender, EventArgs e)
        {
            this.ucLabelEditCartonNo.Value = "";
            this.ucLabelEditRCard.Value = "";
            Clear();

            this.ucLabelEditLotNo.Value = this.CreateNewOQCLot();
            this.ucLabelEditInputCarton.TextFocus(false, true);
        }

        private void ucButtonGetLot_Click(object sender, EventArgs e)
        {
            this.GetLotNo();
        }

        private void ucBtnDelete_Click(object sender, EventArgs e)
        {
            if (!CheckLotStatus())
            {
                return;
            }
            if (!CheckISSelectRow())
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_NO_ROW_SELECTED"));
                return;
            }

            if (MessageBox.Show(MutiLanguages.ParserMessage("$ConformDelete"), this.Text, MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            List<Lot2Carton> lot2CartonList = new List<Lot2Carton>();
            List<OQCLot2Card> lot2CardList = new List<OQCLot2Card>();


            for (int i = 0; i < ultraGridLot2CardList.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridLot2CardList.Rows[i];

                if (row.Band.Index == 0)
                {
                    if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                    {
                        object obj = this.GetEditLot2CartonObject(row);
                        lot2CartonList.Add(obj as Lot2Carton);
                    }

                    if (row.HasChild())
                    {
                        for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                        {
                            if (Convert.ToBoolean(row.ChildBands[0].Rows[j].Cells["Checked"].Value) == true)
                            {
                                object obj = this.GetEditLot2CardObject(row.ChildBands[0].Rows[j]);
                                lot2CardList.Add(obj as OQCLot2Card);
                            }
                        }
                    }

                }
            }

            this.DataProvider.BeginTransaction();
            try
            {
                if (_oqcFacade == null)
                {
                    _oqcFacade = new OQCFacade(this.DataProvider);
                }

                Object obj = _oqcFacade.GetOQCLot(this.ucLabelEditLotNo.Value, OQCFacade.Lot_Sequence_Default);
                Domain.OQC.OQCLot log = (Domain.OQC.OQCLot)obj;

                //先将选中的lot2Card删除
                foreach (OQCLot2Card lot2Card in lot2CardList)
                {
                    if (lot2Card == null)
                    {
                        continue;
                    }
                    if (log.LOTStatus == OQCLotStatus.OQCLotStatus_Initial ||
                        log.LOTStatus == OQCLotStatus.OQCLotStatus_NoExame ||
                        log.LOTStatus == OQCLotStatus.OQCLotStatus_SendExame)
                    {
                        _oqcFacade.DeleteOQCLot2Card(lot2Card);

                        //删除时，同时取消tblsimulation和tblsimulationreport中RCARD对应的LotNo 
                        DeleteSimulationAndSimulationReport(lot2Card.RunningCard);
                    }
                    else
                    {
                        this.DataProvider.RollbackTransaction();
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LotStatus_Not_Initial_SendExame_NoExame $RCard =  " + lot2Card.RunningCard));
                        return;
                    }
                }

                PackageFacade packageFacade;
                Lot2CartonLog lot2CartonLog;
                foreach (Lot2Carton lot2Carton in lot2CartonList)
                {
                    if (lot2Carton == null)
                    {
                        continue;
                    }
                    packageFacade = new PackageFacade(this.DataProvider);
                    object[] objs = packageFacade.GetCarton2RCARDByCartonNO(lot2Carton.CartonNo);
                    //根据Carton获取RCard，删除
                    if (objs != null)
                    {
                        foreach (Carton2RCARD carton2RCARD in objs)
                        {
                            if (carton2RCARD == null)
                            {
                                continue;
                            }
                            object lot2Card = _oqcFacade.GetLastOQCLot2CardByRCard(carton2RCARD.Rcard);
                            if (lot2Card == null)
                            {
                                continue;
                            }
                            _oqcFacade.DeleteOQCLot2Card((OQCLot2Card)lot2Card);

                            //删除时，同时取消tblsimulation和tblsimulationreport中RCARD对应的LotNo 
                            DeleteSimulationAndSimulationReport(((OQCLot2Card)lot2Card).RunningCard);
                        }
                    }
                    // 再删除Lot2Carton 
                    _oqcFacade.DeleteLot2Carton(lot2Carton);

                    //更新Lot2CartonLog
                    _oqcFacade.UpdateLot2CartonLogWhenRemove(lot2Carton.OQCLot, lot2Carton.CartonNo, ApplicationService.Current().LoginInfo.UserCode);


                }

                log.LotSize = _oqcFacade.QueryOQCLot2CardCountByLotNo(this.ucLabelEditLotNo.Value);
                _oqcFacade.UpdateOQCLot(log);
                this.ucLabelEditCapacity.Value = log.LotSize.ToString();
                if (log.LotSize == 0)
                {
                    _oqcFacade.DeleteOQCLot(log);
                    this.ucLabelEditLotNo.Value = "";
                    this.Clear();
                }
                else
                {
                    this.GetDateByLotNo(this.ucLabelEditLotNo.Value);
                }

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
            }

            this.ultraGridLot2CardList.DeleteSelectedRows(false);
            this.DataProvider.CommitTransaction();



        }

        private void ucButtonLot_Click(object sender, EventArgs e)
        {
            if (_oqcFacade == null)
            {
                _oqcFacade = new OQCFacade(this.DataProvider);
            }

            try
            {
                object lot = _oqcFacade.GetOQCLot(this.ucLabelEditLotNo.Value.Trim(), OQCFacade.Lot_Sequence_Default);

                if (lot != null)
                {
                    Domain.OQC.OQCLot oqcLot = (Domain.OQC.OQCLot)lot;
                    if (oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_Initial && oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_SendExame && oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_NoExame && oqcLot.LOTStatus != OQCLotStatus.OQCLotStatus_Examing)
                    {
                        this.ucButtonLot.Enabled = false;
                        ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LotStatus_Not_Initial_SendExame_NoExame_Examing"));
                        return;
                    }
                    else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Examing)
                    {

                    }
                    else
                    {
                        oqcLot.LOTStatus = OQCLotStatus.OQCLotStatus_SendExame;
                    }
                    _oqcFacade.UpdateOQCLot(oqcLot);
                }

                this.ucButtonLot.Enabled = false;

                this.ucLabelEditRCard.Value = "";
                this.ucLabelEditCartonNo.Value = "";
                this.ucLabelEditLotNo.Value = "";
                Clear();

                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Success, "$CS_OQCLOT_SUCCESS"));
            }
            catch (Exception ex)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_OQCLOT_ERROR"));
            }

        }

        private void ucButtonExit_Click(object sender, EventArgs e)
        {
            //this.Close();
        }
        #endregion

        protected Lot2Carton GetEditLot2CartonObject(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row == null)
            {
                return null;
            }

            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            object obj = oqcFacade.GetLot2CartonByCartonNo(row.Cells["CartonNo"].Text);

            if (obj != null)
            {
                return (Lot2Carton)obj;
            }
            else
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

                Lot2Carton lot2Carton = new Lot2Carton();
                lot2Carton.OQCLot = this.ucLabelEditLotNo.Value.Trim();
                lot2Carton.MOCode = row.Cells["MOCode"].Text.Trim();
                lot2Carton.ItemCode = this.ucLabelEditItemCodeQuery.Value.Trim();
                lot2Carton.CartonNo = row.Cells["CartonNo"].Text.Trim();

                lot2Carton.AddUser = ApplicationService.Current().LoginInfo.UserCode;
                lot2Carton.AddDate = dbDateTime.DBDate;
                lot2Carton.AddTime = dbDateTime.DBTime;

                return lot2Carton;
            }
        }

        protected OQCLot2Card GetEditLot2CardObject(Infragistics.Win.UltraWinGrid.UltraGridRow row)
        {
            if (row == null)
            {
                return null;
            }

            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            object obj = oqcFacade.GetLastOQCLot2CardByRCard(row.Cells["RCard"].Text);

            if (obj != null)
            {
                return (OQCLot2Card)obj;
            }
            return null;
        }

        private bool CheckISSelectRow()
        {

            bool flag = false;

            //先判断Grid中的数据是否重复
            for (int i = 0; i < this.ultraGridLot2CardList.Rows.Count; i++)
            {
                Infragistics.Win.UltraWinGrid.UltraGridRow row = ultraGridLot2CardList.Rows[i];
                if (Convert.ToBoolean(row.Cells["Checked"].Value) == true)
                {
                    flag = true;
                }

                if (row.HasChild())
                {
                    for (int j = 0; j < row.ChildBands[0].Rows.Count; j++)
                    {

                        if (Convert.ToBoolean(row.ChildBands[0].Rows[j].Cells["Checked"].Value) == true)
                        {
                            flag = true;
                        }
                    }
                }
            }

            return flag;
        }

        #region 判断当前批状态

        private bool CheckLotStatus()
        {
            string lotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(lotNo), OQCFacade.Lot_Sequence_Default);

            if (obj != null)
            {
                if ((obj as OQCLot).LOTStatus.ToUpper() == OQCLotStatus.OQCLotStatus_Pass || (obj as OQCLot).LOTStatus.ToUpper() == OQCLotStatus.OQCLotStatus_Passing || (obj as OQCLot).LOTStatus.ToUpper() == OQCLotStatus.OQCLotStatus_Reject || (obj as OQCLot).LOTStatus.ToUpper() == OQCLotStatus.OQCLotStatus_Rejecting)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_LotStatus_Has_Judged"));
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 获取批号

        private void GetLotNo()
        {
            ClearCheckList();
            this.ucLabelEditLotNo.Value = "";

            string rCard = this.ucLabelEditRCard.Value.Trim().ToUpper();
            string carton = this.ucLabelEditCartonNo.Value.Trim().ToUpper();

            OQCFacade oqcFacade = new OQCFacade(DataProvider);


            if (rCard.Length > 0)
            {
                DataCollectFacade dataCollectFacade = new DataCollectFacade(this.DataProvider);
                string sourceRCard = dataCollectFacade.GetSourceCard(rCard, string.Empty);

                if (String.IsNullOrEmpty(sourceRCard))
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$NoSimulationInfo"));
                    this.m_LotNo = string.Empty;

                    this.Clear();
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }

                object obj = oqcFacade.GetLastOQCLot2CardByRCard(sourceRCard);
                if (obj == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Normal, "$CS_RCard_No_Lot"));
                    this.m_LotNo = string.Empty;

                    this.Clear();
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }

                OQCLot2Card objLot2Card = obj as OQCLot2Card;

                if (objLot2Card.LOTNO.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_SN_NOT_EXIST_LOT"));
                    this.Clear();
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }

                if (!CheckLotStatus(objLot2Card.LOTNO.Trim()))
                {
                    this.Clear();
                    this.ucLabelEditRCard.TextFocus(false, true);
                    return;
                }

                Messages msg = this.LoadLotInfo();
                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.m_LotNo = string.Empty;

                }
                else
                {
                    this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                    GetDateByLotNo(this.m_LotNo);
                }
                this.ucButtonLot.Enabled = true;

            }
            else if (carton.Length > 0)
            {
                object obj = oqcFacade.GetLot2CartonByCartonNo(carton);
                if (obj == null)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST_LOT"));
                    this.m_LotNo = string.Empty;

                    this.Clear();
                    this.ucLabelEditCartonNo.TextFocus(false, true);
                    return;
                }

                Lot2Carton lot2Carton = obj as Lot2Carton;

                if (lot2Carton.OQCLot.Trim() == string.Empty)
                {
                    ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_CARTON_NOT_EXIST_LOT"));
                    this.Clear();
                    this.ucLabelEditCartonNo.TextFocus(false, true);
                    return;
                }

                if (!CheckLotStatus(lot2Carton.OQCLot.Trim()))
                {
                    this.Clear();
                    this.ucLabelEditCartonNo.TextFocus(false, true);
                    return;
                }

                Messages msg = this.LoadLotInfo();
                if (!msg.IsSuccess())
                {
                    ApplicationRun.GetInfoForm().Add(msg);
                    this.m_LotNo = string.Empty;

                }
                else
                {
                    this.m_LotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
                    GetDateByLotNo(this.m_LotNo);
                }
                this.ucButtonLot.Enabled = true;


            }
            else
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Please_Input_RunningCardOrCarton"));
                this.m_LotNo = string.Empty;

                this.Clear();
                this.ucLabelEditRCard.TextFocus(false, true);
                return;

            }
        }
        #endregion

        private bool CheckLotStatus(string lotno)
        {
            OQCFacade oqcFacade = new OQCFacade(DataProvider);
            OQCLot oqcLot = oqcFacade.GetOQCLot(lotno, OQCFacade.Lot_Sequence_Default) as OQCLot;
            //判断批状态，为以下4种状态的继续
            if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Initial || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Examing ||
                 oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_NoExame || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_SendExame)
            {
                this.ucLabelEditLotNo.Value = lotno;

            }
            else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Reject || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Rejecting)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_Has_Reject"));
                return false;
            }
            else if (oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Pass || oqcLot.LOTStatus == OQCLotStatus.OQCLotStatus_Passing)
            {
                ApplicationRun.GetInfoForm().Add(new UserControl.Message(MessageType.Error, "$CS_Lot_Has_Pass"));
                return false;
            }
            return true;
        }

        private Messages LoadLotInfo()
        {
            Messages msg = new Messages();

            string lotNo = this.ucLabelEditLotNo.Value.Trim().ToUpper();
            if (lotNo.Length > 0)
            {
                try
                {
                    OQCFacade oqcFacade = new OQCFacade(DataProvider);
                    object obj = oqcFacade.GetOQCLot(FormatHelper.PKCapitalFormat(lotNo), OQCFacade.Lot_Sequence_Default);
                    if (obj == null)
                    {
                        msg.Add(new UserControl.Message(MessageType.Error, "$CS_LOT_NOT_EXIST"));
                        this.ucLabelEditLotNo.TextFocus(false, true);
                        return msg;
                    }

                    OQCLot lot = obj as OQCLot;

                    this.ucLabelEditStatus.Value = UserControl.MutiLanguages.ParserString(lot.LOTStatus.ToString());
                    this.ucLabelEditCapacity.Value = lot.LotSize.ToString();

                    object[] objs = oqcFacade.GetLot2CartonByLot(FormatHelper.PKCapitalFormat(lotNo));
                    if (objs != null)
                    {
                        Lot2Carton lot2Carton = objs[0] as Lot2Carton;
                        this.ucLabelEditMoCodeQuery.Value = lot2Carton.MOCode.ToString();
                        this.ucLabelEditItemCodeQuery.Value = lot2Carton.ItemCode.ToString();
                    }

                }
                catch (Exception ex)
                {
                    msg.Add(new UserControl.Message(ex));
                    ucLabelEditLotNo.TextFocus(false, true);
                }
            }
            else
            {
                msg.Add(new UserControl.Message(MessageType.Error, "$CS_FQCLOT_NOT_NULL"));
                this.ucLabelEditLotNo.TextFocus(false, true);
            }
            return msg;
        }

        private void ClearCheckList()
        {
            if (this.m_CheckList == null)
            {
                return;
            }
            this.m_CheckList.Tables["CartonDetail"].Rows.Clear();
            this.m_CheckList.Tables["CartonDetail"].AcceptChanges();
            this.m_CheckList.Tables["CartonGroup"].Rows.Clear();
            this.m_CheckList.Tables["CartonGroup"].AcceptChanges();

            this.m_CheckList.AcceptChanges();
        }


        #region 全部清空
        private void Clear()
        {
            ClearCheckList();

            this.ucLabelEditCapacity.Value = "";
            this.ucLabelEditInputCarton.Value = "";
            this.ucLabelEditItemCodeQuery.Value = "";
            this.ucLabelEditStatus.Value = "";
            this.ucLabelEditMoCodeQuery.Value = "";
            //this.ucLabelEditRCard.Value = "";          
        }
        #endregion

        private void FOQCLotQA_Load(object sender, EventArgs e)
        {
            //this.InitPageLanguage();
            InitializeMainGrid();
        }





    }
}
