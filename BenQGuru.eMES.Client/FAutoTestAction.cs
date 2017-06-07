using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


using BenQGuru.eMES.TS;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.DataCollect.Action;
using BenQGuru.eMES.DataCollect;
using UserControl;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.MOModel;
using Infragistics.Win.UltraWinGrid;
using BenQGuru.eMES.Domain.OQC;

namespace BenQGuru.eMES.Client.FAutoTestAction
{
    public abstract class FAutoTestActionBase
    {
        public static bool CheckIsTestForm(Form form)
        {
            string[] strTestForms = new string[] 
                                        {
                                            "FCollectionGDNG",
                                            "FBurnIn",
                                            "FBurnOut",
                                            "FCollectionIDMerge",
                                            "FCollectionMetrial",
                                            "FCollectionOQC",
                                            "FGenLotIDMerge"
                                        };
            string[] strType = form.GetType().Name.Split('.');
            return (new ArrayList(strTestForms)).Contains(strType[strType.Length - 1]);
            
        }

        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public int iRCardOriginalSequenceStart = -1;
        public int iRCardCurrentIdx = 0;
        public FAutoTestConfig f = null;

        public FAutoTestActionBase(FAutoTestConfig testConfig)
        {
            f = testConfig;
            f.Top = Screen.PrimaryScreen.Bounds.Height - f.Height;
            f.Left = Screen.PrimaryScreen.Bounds.Width - f.Width;
            f.Show();

            ReadConfig();

            f.CollectTick += new EventHandler(f_CollectTick);
        }
        public bool bExecuteing = false;
        private void f_CollectTick(object sender, EventArgs e)
        {
            if (bExecuteing == true)
                return;
            bExecuteing = true;
            try
            {
                BeforeRCardRun();
                RCardRun();
                AfterRCardRun();
            }
            catch (Exception ex)
            {
                UserControl.Messages msg = new UserControl.Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }
            finally
            {
                bExecuteing = false;
            }
        }

        public virtual void BeforeRCardRun()
        {
        }
        public virtual void AfterRCardRun()
        {
        }

        public string currentRunningCard = "";
        public virtual void RCardRun()
        {
            if (int.Parse(f.GetAppConfig("RCardSequenceStart")) != iRCardOriginalSequenceStart)
            {
                iRCardOriginalSequenceStart = int.Parse(f.GetAppConfig("RCardSequenceStart"));
                iRCardCurrentIdx = iRCardOriginalSequenceStart - 1;
            }

            iRCardCurrentIdx++;
            int iSeqLen = int.Parse(f.GetAppConfig("RCardSequenceLength"));
            string strRCard = f.GetAppConfig("RCardPrefix") + iRCardCurrentIdx.ToString().PadLeft(iSeqLen, '0');
            currentRunningCard = strRCard;

            if (iRCardCurrentIdx >= int.Parse(f.GetAppConfig("RCardSequenceEnd")))
            {
                f.RCardTestEnd();
            }

            f.UpdateLastSn(strRCard);
        }

        public virtual void ReadConfig()
        {
            iRCardOriginalSequenceStart = int.Parse(f.GetAppConfig("RCardSequenceStart"));
            iRCardCurrentIdx = iRCardOriginalSequenceStart - 1;
        }

        public void UpdateLastKeyPart(int sequence, string keyPart)
        {
            f.UpdateLastKeyPart(sequence, keyPart);
        }

        public void UpdateLastSplitSn(int sequence, string lastSn)
        {
            f.UpdateLastSplitSn(sequence, lastSn);
        }

    }

    public class FAutoTestActionTest : FAutoTestActionBase
    {
        FCollectionGDNG actionForm = null;
        public FAutoTestActionTest(FCollectionGDNG form, FAutoTestConfig testConfig)
            : base(testConfig)
        {
            actionForm = form;
        }

        public override void ReadConfig()
        {
            base.ReadConfig();
        }

        public override void BeforeRCardRun()
        {
            base.BeforeRCardRun();
        }
        public override void AfterRCardRun()
        {
            base.AfterRCardRun();
        }

        public override void RCardRun()
        {
            base.RCardRun();
            try
            {
                string strRCard = currentRunningCard;
                DateTime dtStart = DateTime.Now;
                actionForm.txtRunningCard.Value = strRCard;
                actionForm.txtRunningCard_TxtboxKeyPress(actionForm.txtRunningCard, new KeyPressEventArgs('\r'));
                f.LogCostTime(dtStart, strRCard);
            }
            catch (Exception ex)
            {
                UserControl.Messages msg = new UserControl.Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }
    }

    public class FAutoTestActionBurnIn : FAutoTestActionBase
    {
        FBurnIn actionForm = null;
        public FAutoTestActionBurnIn(FBurnIn form, FAutoTestConfig testConfig)
            : base(testConfig)
        {
            actionForm = form;
        }

        public override void ReadConfig()
        {
            base.ReadConfig();
        }

        public override void BeforeRCardRun()
        {
            base.BeforeRCardRun();
            if (actionForm.uclShelfNO.Value == string.Empty)
            {
                TestInputShelfNo();
            }
        }
        public override void AfterRCardRun()
        {
            base.AfterRCardRun();
            if (int.Parse(actionForm.lblRecentQTY.Text) == int.Parse(actionForm.uclVolumn.Value))
            {
                string strShelfNo = actionForm.uclShelfNO.Value;
                DateTime dtStart = DateTime.Now;
                actionForm.ucbBurnIn_Click(null, null);
                f.LogCostTime(dtStart, strShelfNo);
                TestInputShelfNo();
            }
        }

        public override void RCardRun()
        {
            base.RCardRun();
            try
            {
                string strRCard = currentRunningCard;
                DateTime dtStart = DateTime.Now;
                actionForm.txtRunningCard.Value = strRCard;
                actionForm.txtRunningCard_TxtboxKeyPress(actionForm.txtRunningCard, new KeyPressEventArgs('\r'));
                f.LogCostTime(dtStart, strRCard);
            }
            catch (Exception ex)
            {
                UserControl.Messages msg = new UserControl.Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }

        private void TestInputShelfNo()
        {
            if (f.GetAppConfig("BurnInNeedGOMO") == "1")
            {
                actionForm.txtGOMO.Checked = true;
            }
            string shelfNo = string.Empty;
            string strSql = "select * from tblshelf where status='ShelfStatus_BurnOut' ";
            string strShelfNORange = f.GetAppConfig("BurnInShelfRange");
            string[] shelfNORangeList = strShelfNORange.Split(',');
            if (shelfNORangeList.Length == 2 && shelfNORangeList[0] != string.Empty && shelfNORangeList[1] != string.Empty)
            {
                strSql += " AND ShelfNo>='" + shelfNORangeList[0] + "' AND ShelfNo<='" + shelfNORangeList[1] + "' ";
            }
            strSql += " ORDER BY ShelfNo ";
            object[] objs = this.DataProvider.CustomQuery(typeof(Shelf), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
            {
                while (true)
                {
                    shelfNo = (new Random(DateTime.Now.Minute * 1000 + DateTime.Now.Millisecond)).Next(170, 999999).ToString().PadLeft(6, '0');
                    strSql = "select count(*) from tblshelf where shelfno='" + shelfNo + "'";
                    if (this.DataProvider.GetCount(new SQLCondition(strSql)) == 0)
                    {
                        Shelf shelf = new Shelf();
                        shelf.ShelfNO = shelfNo;
                        shelf.Status = "ShelfStatus_BurnOut";
                        shelf.Memo = shelf.ShelfNO;
                        shelf.MaintainUser = "AutoTest";
                        shelf.MaintainDate = FormatHelper.TODateInt(DateTime.Today);
                        shelf.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                        this.DataProvider.Insert(shelf);
                        break;
                    }
                }
            }
            else
            {
                shelfNo = ((Shelf)objs[0]).ShelfNO;
            }
            actionForm.uclShelfNO.Value = shelfNo;
            actionForm.uclShelfNO_TxtboxKeyPress(actionForm.uclShelfNO, new KeyPressEventArgs('\r'));
            if (f.GetAppConfig("BurnInNeedGOMO") == "1")
            {
                actionForm.txtGOMO.Checked = true;
            }
        }

    }


    public class FAutoTestActionBurnOut : FAutoTestActionBase
    {
        FBurnOut actionForm = null;
        public FAutoTestActionBurnOut(FBurnOut form, FAutoTestConfig testConfig)
            : base(testConfig)
        {
            actionForm = form;
        }

        public override void ReadConfig()
        {
            base.ReadConfig();
        }

        public override void BeforeRCardRun()
        {
            base.BeforeRCardRun();
        }
        public override void AfterRCardRun()
        {
            base.AfterRCardRun();
        }

        public override void RCardRun()
        {
            string MOCode = f.GetAppConfig("BurnOutMOCode");
            actionForm.RefreshPanel();
            string strSql = "select * from tblshelfactionlist where status='ShelfStatus_BurnIn' ";
            if (MOCode != string.Empty)
            {
                strSql += " and pkid in (select shelfno from tblsimulation where mocode='" + MOCode + "' and laction='BURNIN') ";
            }
            strSql += " order by bidate,bitime";
            object[] objs = this.DataProvider.CustomQuery(typeof(ShelfActionList), new SQLCondition(strSql));
            if (objs == null || objs.Length == 0)
            {
                bExecuteing = false;
                return;
            }
            for (int i = 0; i < objs.Length; i++)
            {
                ShelfActionList shelf = (ShelfActionList)objs[i];
                bool expired = false;
                for (int n = 0; n < actionForm.ultraDataSource2.Rows.Count; n++)
                {
                    if (string.Compare(shelf.ShelfNO, actionForm.ultraDataSource2.Rows[n]["ShelfNO2"].ToString()) == 0)
                    {
                        expired = true;
                        break;
                    }
                }
                if (expired == true)
                {
                    actionForm.uclShelfNO.Value = shelf.ShelfNO;
                    DateTime dtStart = DateTime.Now;
                    actionForm.ucbBurnOut_Click(null, null);
                    f.LogCostTime(dtStart, shelf.ShelfNO);
                }
            }
        }
    }


    public class FAutoTestActionIDMerge : FAutoTestActionBase
    {
        FCollectionIDMerge actionForm = null;
        public FAutoTestActionIDMerge(FCollectionIDMerge form, FAutoTestConfig testConfig)
            : base(testConfig)
        {
            actionForm = form;
        }

        private Hashtable htSplitedIndex = null;
        public override void ReadConfig()
        {
            base.ReadConfig();

            htSplitedIndex = new Hashtable();
            int iSplitedCount = int.Parse(f.GetAppConfig("ConvertSNCount"));
            for (int i = 1; i <= iSplitedCount; i++)
            {
                int[] iIdx = new int[2];
                string strLoop = f.GetAppConfig("ConvertSN" + i.ToString());
                string[] strTmp = strLoop.Split(':');
                iIdx[0] = int.Parse(strTmp[3]);
                iIdx[1] = int.Parse(strTmp[3]) - 1;
                htSplitedIndex.Add(i, iIdx);
            }
        }

        public override void BeforeRCardRun()
        {
            base.BeforeRCardRun();
        }
        public override void AfterRCardRun()
        {
            base.AfterRCardRun();

            int iSplitedCount = int.Parse(f.GetAppConfig("ConvertSNCount"));
            for (int i = 1; i <= iSplitedCount; i++)
            {
                string strLoop = f.GetAppConfig("ConvertSN" + i.ToString());
                string[] strTmp = strLoop.Split(':');
                int iSleep = int.Parse(strTmp[0]);
                if (iSleep > 0)
                    System.Threading.Thread.Sleep(iSleep);
                int iSeqLen = int.Parse(strTmp[2]);
                int[] iIdx = (int[])htSplitedIndex[i];
                iIdx[1] = iIdx[1] + 1;
                htSplitedIndex[i] = iIdx;
                strLoop = strTmp[1] + iIdx[1].ToString().PadLeft(iSeqLen, '0');
                actionForm.ucLERunningCard.Value = strLoop;
                DateTime dtStart = DateTime.Now;
                actionForm.ucLERunningCard_TxtboxKeyPress(actionForm.ucLERunningCard, new KeyPressEventArgs('\r'));
                if (i == iSplitedCount)
                {
                    f.LogCostTime(dtStart, strLoop);
                }
                this.UpdateLastSplitSn(i, strLoop);
            }
        }

        public override void RCardRun()
        {
            base.RCardRun();
            try
            {
                string strRCard = currentRunningCard;
                DateTime dtStart = DateTime.Now;
                actionForm.ucLERunningCard.Value = strRCard;
                actionForm.ucLERunningCard_TxtboxKeyPress(actionForm.ucLERunningCard, new KeyPressEventArgs('\r'));
                f.LogCostTime(dtStart, strRCard);
            }
            catch (Exception ex)
            {
                UserControl.Messages msg = new UserControl.Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }
    }


    public class FAutoTestActionMaterial : FAutoTestActionBase
    {
        FCollectionMetrial actionForm = null;
        public FAutoTestActionMaterial(FCollectionMetrial form, FAutoTestConfig testConfig)
            : base(testConfig)
        {
            actionForm = form;

            if (htKeyPartIndex == null || htKeyPartIndex.Count <= 0)    // 没有KeyPart设置时，只做集成上料
            {
                actionForm.opsetCollectObject.CheckedIndex = 0;
                actionForm.edtINNO.Checked = true;
                actionForm.edtINNO.Value = f.GetAppConfig("INNO");
            }
            else if (f.GetAppConfig("INNO") == "")  //没有集成上料，当有KeyPart设置时，做KeyPart上料
            {
                actionForm.opsetCollectObject.CheckedIndex = 1;
            }
            else        // INNO和KeyPart都有设置
            {
                actionForm.opsetCollectObject.CheckedIndex = 2;
                actionForm.edtINNO.Checked = true;
                actionForm.edtINNO.Value = f.GetAppConfig("INNO");
            }
        }

        private Hashtable htKeyPartIndex = null;
        public override void ReadConfig()
        {
            base.ReadConfig();

            htKeyPartIndex = new Hashtable();
            int iKeyPartsCount = int.Parse(f.GetAppConfig("KeyPartsCount"));
            for (int i = 1; i <= iKeyPartsCount; i++)
            {
                int[] iIdx = new int[2];
                string strLoop = f.GetAppConfig("KeyPart" + i.ToString());
                string[] strTmp = strLoop.Split(':');
                iIdx[0] = int.Parse(strTmp[3]);
                iIdx[1] = int.Parse(strTmp[3]) - 1;
                htKeyPartIndex.Add(i, iIdx);
            }
        }

        public override void BeforeRCardRun()
        {
            base.BeforeRCardRun();
        }
        public override void AfterRCardRun()
        {
            base.AfterRCardRun();

            int iKeyPartsCount = int.Parse(f.GetAppConfig("KeyPartsCount"));
            for (int i = 1; i <= iKeyPartsCount; i++)
            {
                string strLoop = f.GetAppConfig("KeyPart" + i.ToString());
                string[] strTmp = strLoop.Split(':');
                int iSleep = int.Parse(strTmp[0]);
                if (iSleep > 0)
                    System.Threading.Thread.Sleep(iSleep);
                int iSeqLen = int.Parse(strTmp[2]);
                int[] iIdx = (int[])htKeyPartIndex[i];
                iIdx[1] = iIdx[1] + 1;
                htKeyPartIndex[i] = iIdx;
                strLoop = strTmp[1] + iIdx[1].ToString().PadLeft(iSeqLen, '0');
                actionForm.edtInput.Value = strLoop;
                DateTime dtStart = DateTime.Now;
                actionForm.edtInput_TxtboxKeyPress(actionForm.edtInput, new KeyPressEventArgs('\r'));
                if (i == iKeyPartsCount)
                {
                    f.LogCostTime(dtStart, strLoop);
                }
                this.UpdateLastKeyPart(i, strLoop);
            }
        }

        public override void RCardRun()
        {
            base.RCardRun();
            try
            {
                string strRCard = currentRunningCard;
                DateTime dtStart = DateTime.Now;
                actionForm.edtInput.Value = strRCard;
                actionForm.edtInput_TxtboxKeyPress(actionForm.edtInput, new KeyPressEventArgs('\r'));
                f.LogCostTime(dtStart, strRCard);
            }
            catch (Exception ex)
            {
                UserControl.Messages msg = new UserControl.Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }
    }

    public class FAutoTestActionOQC : FAutoTestActionBase
    {
        FCollectionOQC actionForm = null;
        public FAutoTestActionOQC(FCollectionOQC form, FAutoTestConfig testConfig)
            : base(testConfig)
        {
            actionForm = form;
        }

        public override void ReadConfig()
        {
            base.ReadConfig();
        }

        public override void BeforeRCardRun()
        {
            base.BeforeRCardRun();
        }
        public override void AfterRCardRun()
        {
            base.AfterRCardRun();
        }

        public override void RCardRun()
        {
            string strLotNo = GetOQCLotNo();
            if (strLotNo == string.Empty)
            {
                return;
            }
            FOQCSamplePlan fPlan = new FOQCSamplePlan();
            fPlan.Show();
            fPlan.oqcLotNo = strLotNo;
            fPlan.cbbLotNO_KeyUp(fPlan.cbbLotNO, new KeyEventArgs(Keys.Enter));
            fPlan.btnLoadConfig_Click(null, null);
            fPlan.ucLabEditSampleQty.Value = f.GetAppConfig("OQCSampleSize");
            fPlan.ucButtonOK_Click(null, null);
            fPlan.Close();

            Application.DoEvents();
            actionForm.ucLabOQCLot.Value = strLotNo;
            actionForm.ucLabEdit1_TxtboxKeyPress(actionForm.ucLabOQCLot, new KeyPressEventArgs('\r'));
            Application.DoEvents();
            int iSampleSize = int.Parse(f.GetAppConfig("OQCSampleSize"));
            for (int i = 0; i < iSampleSize; i++)
            {
                string strRCard = GetOQCRunningCard(strLotNo);
                if (strRCard != null)
                {
                    actionForm.ucLabRunningID.Value = strRCard;
                    actionForm.ucLabRunningID_TxtboxKeyPress(actionForm.ucLabRunningID, new KeyPressEventArgs('\r'));
                    actionForm.ucButtonOK_Click(null, null);
                }
                Application.DoEvents();
                System.Threading.Thread.Sleep(1000);
            }
            DateTime dtStart = DateTime.Now;
            actionForm.ucButtonOQC_Click(null, null);
            f.LogCostTime(dtStart, strLotNo);
        }

        private string GetOQCLotNo()
        {
            string strLotSize = f.GetAppConfig("LotCapacity");
            string strSql = "SELECT * FROM tblLot WHERE (LotStatus='oqclotstatus_initial' or LotStatus='oqclotstatus_noexame') AND LotSize='" + strLotSize + "' AND LotNo LIKE '%" + Service.ApplicationService.Current().LoginInfo.Resource.StepSequenceCode + "%' ORDER BY LotNo";
            object[] objTmp = this.DataProvider.CustomQuery(typeof(OQCLot), new SQLCondition(strSql));
            if (objTmp == null || objTmp.Length == 0)
                return string.Empty;
            else
            {
                return ((OQCLot)objTmp[0]).LOTNO;
            }
        }
        private string GetOQCRunningCard(string strLotNo)
        {
            string strSql = "SELECT * FROM tblLot2Card WHERE LotNo='" + strLotNo + "' AND RCard NOT IN (SELECT RCard FROM tblLot2CardCheck WHERE LotNo='" + strLotNo + "') ORDER BY RCard ";
            object[] objTmp = this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(strSql));
            if (objTmp == null || objTmp.Length == 0)
                return string.Empty;
            else
                return ((OQCLot2Card)objTmp[0]).RunningCard;
        }

    }


    public class FAutoTestActionPack : FAutoTestActionBase
    {
        FGenLotIDMerge actionForm = null;
        public FAutoTestActionPack(FGenLotIDMerge form, FAutoTestConfig testConfig)
            : base(testConfig)
        {
            actionForm = form;
        }

        private int iOriginalLoopStart = -1;
        private int iCurrentLoopIdx = 0;
        public override void ReadConfig()
        {
            base.ReadConfig();

            string strLoop = f.GetAppConfig("CartonCode");
            string[] strTmp = strLoop.Split(':');
            iOriginalLoopStart = int.Parse(strTmp[2]);
            iCurrentLoopIdx = iOriginalLoopStart - 1;
        }

        private bool bIsFirst = true;
        public override void BeforeRCardRun()
        {
            base.BeforeRCardRun();

            if (bIsFirst == true)
            {
                actionForm.txtCartonCapacity.Value = f.GetAppConfig("CartonCapacity");
                actionForm.ucLabEditMaxNumber.Checked = true;
                actionForm.ucLabEditMaxNumber.Value = f.GetAppConfig("LotCapacity");
                bIsFirst = false;
            }
        }
        public override void AfterRCardRun()
        {
            base.AfterRCardRun();

            if (actionForm.txtCartonCapacity.Value == f.GetAppConfig("CartonCapacity"))
            {
                actionForm.txtCarton.Checked = true;
                string strLoop = f.GetAppConfig("CartonCode");
                string[] strTmp = strLoop.Split(':');
                int iSeqLen = int.Parse(strTmp[1]);
                iCurrentLoopIdx++;
                strLoop = strTmp[0] + iCurrentLoopIdx.ToString().PadLeft(iSeqLen, '0');
                actionForm.txtCarton.Value = strLoop;
                actionForm.txtCarton_TxtboxKeyPress(actionForm.txtCarton, new KeyPressEventArgs('\r'));
            }
            if (actionForm.ucLabEditCurrentNubmer.Value == f.GetAppConfig("LotCapacity") ||
                actionForm.ucLabEditCurrentNubmer.Value == string.Empty ||
                actionForm.ucLabEditOQCLot.Value == string.Empty)
            {
                actionForm.btnAutoGenLot_Click(null, new EventArgs());
                actionForm.ucLabEditCurrentNubmer.Value = "0";
            }
        }

        public override void RCardRun()
        {
            base.RCardRun();
            try
            {
                string strRCard = currentRunningCard;
                DateTime dtStart = DateTime.Now;
                actionForm.ucLabEditInputID.Value = strRCard;
                actionForm.ucLabEditInputID_TxtboxKeyPress(actionForm.ucLabEditInputID, new KeyPressEventArgs('\r'));
                f.LogCostTime(dtStart, strRCard);
            }
            catch (Exception ex)
            {
                UserControl.Messages msg = new UserControl.Messages();
                msg.Add(new UserControl.Message(ex));
                ApplicationRun.GetInfoForm().Add(msg);
            }
        }
    }


}
