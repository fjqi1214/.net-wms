using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.TSModel;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.TSModel;
using UserControl;
using System.Collections;

namespace BenQGuru.eMES.Client
{
    public partial class FTSInputEditInfo : BaseForm
    {
        public event ParentChildRelateEventHandler<ParentChildRelateEventArgs<TSEditInformation>> Event;
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public string ErrorGroupDesc //从前台获得的不良组代码描述数据
        {
            set { ucErrorGroupDesc.Value = value; }
        }

        public string ErrorCodeDesc //从前台获得的不良代码描述数据
        {
            set { this.ucErrorCodeDesc.Value = value; }
        }

        private TSEditInformation m_TSEditInfo;
        public TSEditInformation TSEditInfo
        {
            get { return m_TSEditInfo; }
            set { m_TSEditInfo = value; }
        }

        private string m_Status;
        public string Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        private string m_Model;

        public string Model
        {
            get { return m_Model; }
            set { m_Model = value; }
        }

        TSModelFacade tsModelFacade = null;
        string[] errCauseGroupCode;
        string[] errorGroupCode;
        string[] errorCauseCode;
        string[] lcDutyCode;
        string[] lcSolutioncode;

        public FTSInputEditInfo()
        {
            InitializeComponent();

            tsModelFacade = new TSModelFacade(this.DataProvider);
        }

        private void FTSInputEditInfo_Load(object sender, EventArgs e)
        {
            LoadErrorGroup();

            LoadErrCauseGroup();

            LoadLCDuty();

            LoadLCSolution();

            this.txtLESolutionMemo.Text = TSEditInfo.SolutionMemo;

            if (string.Compare(Status, "disabled", true) == 0)
            {
                this.listErrCauseGroup.Enabled = false;
                this.listLCErrorCause.Enabled = false;
            }
            //this.InitPageLanguage();
        }

        private void LoadErrorGroup()
        {
            int i = 0;
            chklistErrorGroup.Items.Clear();

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            object[] parameters = systemSettingFacade.GetAllParametersOrderBySequence("TS_COMPONENT");

            if (parameters == null || parameters.Length == 0)
            {
                return;
            }

            errorGroupCode = new string[parameters.Length];
            if (parameters != null && parameters.Length > 0)
            {                
                foreach (Parameter para in parameters)
                {
                    chklistErrorGroup.Items.Add(para.ParameterAlias);
                    errorGroupCode[i] = para.ParameterCode;
                    i++;
                }

            }

            if (TSEditInfo.ErrGroup != null || TSEditInfo.ErrGroup != String.Empty)
            {
                string[] errorGroup = TSEditInfo.ErrGroup.Split(new Char[] { ',' });

                for (int j = 0; j < errorGroup.Length; j++)
                {
                    for (int l = 0; l < chklistErrorGroup.Items.Count; l++)
                    {
                        if (string.Compare(errorGroup[j], chklistErrorGroup.Items[l].ToString(), true) == 0)
                        {
                            chklistErrorGroup.SetItemChecked(l, true);
                        }
                    }
                }
            }


        }

        private void LoadErrCauseGroup()
        {
            int i = 0;
            listErrCauseGroup.Items.Clear();

            object[] objs = tsModelFacade.QueryModelErrorCauseGroup(this.Model, "", 0, int.MaxValue);

            if (objs == null)
            {
                return;
            }

            errCauseGroupCode = new string[objs.Length];

            foreach (ErrorCauseGroup errorCauseGroup in objs)
            {
                listErrCauseGroup.Items.Add(errorCauseGroup.ErrorCauseGroupDescription);
                errCauseGroupCode[i] = errorCauseGroup.ErrorCauseGroupCode;
                if (string.Compare(TSEditInfo.ErrorCauseGroup, errCauseGroupCode[i], true) == 0)
                {
                    listErrCauseGroup.SetSelected(i, true);
                }
                i++;
            }

        }

        private void LoadLCDuty()
        {
            int i = 0;
            this.listLCDuty.Items.Clear();

            object[] objs = tsModelFacade.GetAllDuty();

            if (objs == null)
            {
                return;
            }

            lcDutyCode = new string[objs.Length];
            foreach (Duty duty in objs)
            {
                listLCDuty.Items.Add(duty.DutyDescription);
                lcDutyCode[i] = duty.DutyCode;
                if (string.Compare(TSEditInfo.Dudy, lcDutyCode[i], true) == 0)
                {
                    listLCDuty.SetSelected(i, true);
                }
                i++;
            }
        }

        private void LoadLCSolution()
        {
            int i = 0;
            this.listLCSolution.Items.Clear();

            object[] objs = tsModelFacade.QueryModel2SolutionNew(string.Empty, this.Model, 1, int.MaxValue);

            if (objs == null)
            {
                return;
            }

            object[] objsSolution = this.tsModelFacade.GetAllSolution();
            Hashtable htSolution = new Hashtable();
            if (objsSolution != null)
            {
                for (int j = 0; j < objsSolution.Length; j++)
                {
                    htSolution.Add(((Solution)objsSolution[j]).SolutionCode, objsSolution[j]);
                }
            }

            lcSolutioncode = new string[objs.Length];
            foreach (Model2Solution model2solution in objs)
            {
                if (htSolution.ContainsKey(model2solution.SolutionCode) == true)
                {
                    Solution solution = (Solution)htSolution[model2solution.SolutionCode];
                    listLCSolution.Items.Add(solution.SolutionDescription);
                    lcSolutioncode[i] = solution.SolutionCode;
                    if (string.Compare(TSEditInfo.Solution, lcSolutioncode[i], true) == 0)
                    {
                        listLCSolution.SetSelected(i, true);
                    }
                    i++;
                }
            }
        }

        private void listErrCauseGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.listLCErrorCause.Items.Clear();

            int j = 0;
            int i = 0;
            if (this.listErrCauseGroup.SelectedItem != null)
                j = listErrCauseGroup.SelectedIndex;

            BenQGuru.eMES.TSModel.TSModelFacade facade = new TSModelFacade(tsModelFacade.DataProvider);

            object[] objs = facade.GetSelectedErrorCauseByErrorCauseGroupCode(errCauseGroupCode[j]);

            if (objs != null)
            {
                errorCauseCode = new string[objs.Length];
                foreach (ErrorCause errCause in objs)
                {
                    if (errCause != null)
                    {
                        this.listLCErrorCause.Items.Add(errCause.ErrorCauseDescription);
                        errorCauseCode[i] = errCause.ErrorCauseCode;
                        if (string.Compare(TSEditInfo.ErrorCause, errorCauseCode[i], true) == 0)
                        {
                            listLCErrorCause.SetSelected(i, true);
                        }
                        i++;
                    }
                }
            }
        }

        private void ucBtnSave_Click(object sender, EventArgs e)
        {
            bool validate = validateErrorCauseInput();

            if (validate == true)
            {
                int i = 0;
                int j = 0;
                int k = 0;
                int l = 0;
                int h = 0;
                string allErrorGroup = null;
                string allErrorGroupCode = null;

                for (int m = 0; m < chklistErrorGroup.Items.Count; m++)
                {
                    if (chklistErrorGroup.GetItemChecked(m))
                    {
                        if (h < (chklistErrorGroup.CheckedItems.Count - 1))
                        {
                            allErrorGroup += chklistErrorGroup.Items[m].ToString() + ",";
                            allErrorGroupCode += errorGroupCode[m] + ",";
                            h++;
                        }
                        else
                        {
                            allErrorGroup += chklistErrorGroup.Items[m].ToString();
                            allErrorGroupCode += errorGroupCode[m];
                        }
                    }
                }

                TSEditInfo.ErrGroup = allErrorGroup;
                TSEditInfo.ErrGroupCode = allErrorGroupCode;

                if (this.listErrCauseGroup.SelectedItem != null)
                {
                    i = listErrCauseGroup.SelectedIndex;
                }

                TSEditInfo.ErrorCauseGroup = errCauseGroupCode[i];

                if (this.listLCErrorCause.SelectedItem != null)
                {
                    j = listLCErrorCause.SelectedIndex;
                }

                TSEditInfo.ErrorCause = errorCauseCode[j];

                if (this.listLCDuty.SelectedItem != null)
                {
                    k = listLCDuty.SelectedIndex;
                }

                TSEditInfo.Dudy = lcDutyCode[k];

                if (this.listLCSolution.SelectedItem != null)
                {
                    l = listLCSolution.SelectedIndex;
                }

                TSEditInfo.Solution = lcSolutioncode[l];

                TSEditInfo.SolutionMemo = this.txtLESolutionMemo.Text;

                this.OnEvent(null, new ParentChildRelateEventArgs<TSEditInformation>(TSEditInfo));

                Close();
            }
        }

        public void OnEvent(object sender, ParentChildRelateEventArgs<TSEditInformation> e)
        {
            if (Event != null)
            {
                Event(sender, e);
            }
        }

        private bool validateErrorCauseInput()
        {
            int i = 0;
            bool validate = true;

            for (int m = 0; m < chklistErrorGroup.Items.Count; m++)
            {
                if (chklistErrorGroup.GetItemChecked(m))
                {
                    i++;
                }
            }

            if (i == 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_ErrorGroup"));
                validate = false;
            }

            if (this.listErrCauseGroup.SelectedIndex < 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_ErrorCauseGroup"));
                validate = false;
            }

            if (this.listLCErrorCause.SelectedIndex < 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_ErrorCause"));
                validate = false;
            }

            if (this.listLCDuty.SelectedIndex < 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_Duty"));
                validate = false;
            }


            if (this.listLCSolution.SelectedIndex < 0)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_CS_Please_Select_Solution"));
                validate = false;
            }

            if (System.Text.Encoding.Default.GetByteCount(this.txtLESolutionMemo.Text) > 100)
            {
                this.ShowMessage(new UserControl.Message(MessageType.Error, "$Error_MemoMaxlengthCheck"));
                this.txtLESolutionMemo.Focus();
                this.txtLESolutionMemo.SelectAll();
                validate = false;
            }

            return validate;
        }

        protected void ShowMessage(UserControl.Message message)
        {
            ApplicationRun.GetInfoForm().Add(message);
        }

        private void chklistErrorGroup_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int i = 0; i < this.chklistErrorGroup.Items.Count; i++)
                {
                    if (this.chklistErrorGroup.GetItemChecked(i) && i != e.Index)
                    {
                        this.chklistErrorGroup.SetItemChecked(i, false);
                    }
                }
            }
        }

    }

    public class TSEditInformation
    {
        private string m_ErrorGroup;
        public string ErrGroup
        {
            get { return m_ErrorGroup; }
            set { m_ErrorGroup = value; }
        }

        private string m_ErrorGroupCode;

        public string ErrGroupCode
        {
            get { return m_ErrorGroupCode; }
            set { m_ErrorGroupCode = value; }
        }


        private string m_ErrorCauseGroup; //从前台获得的不良原因组数据
        public string ErrorCauseGroup
        {
            get { return m_ErrorCauseGroup; }

            set { m_ErrorCauseGroup = value; }
        }

        private string m_ErrorCause;
        public string ErrorCause
        {
            get { return m_ErrorCause; }
            set { m_ErrorCause = value; }
        }

        private string m_Duty;
        public string Dudy
        {
            get { return m_Duty; }
            set { m_Duty = value; }
        }

        private string m_Solution;
        public string Solution
        {
            get { return m_Solution; }
            set { m_Solution = value; }
        }

        private string m_SolutionMemo;
        public string SolutionMemo
        {
            get { return m_SolutionMemo; }
            set { m_SolutionMemo = value; }
        }

    }
}