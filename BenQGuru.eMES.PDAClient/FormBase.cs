using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win.UltraWinEditors;
using System.Collections;
using BenQGuru.eMES.PDAClient.Service;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace BenQGuru.eMES.PDAClient
{
    public partial class FormBase : Form
    {
        private string _title = string.Empty;
        private bool m_AutoCreateLanguage = false;
        ArrayList arrayListLabel = new ArrayList();
        ArrayList arrayListButton = new ArrayList();
        ArrayList arrayListUCLabel = new ArrayList();
        ArrayList arrayListUCButton = new ArrayList();
        ArrayList arrayListCheckBox = new ArrayList();
        ArrayList arrayListRadioButton = new ArrayList();
        ArrayList arrayListGroupBox = new ArrayList();
        ArrayList arrayListComBox = new ArrayList();
        ArrayList arrayListOptionSet = new ArrayList();
        ArrayList arrayListCheckEditor = new ArrayList();
        ArrayList arrayListUCDateTime = new ArrayList();
        ArrayList arrayListStatusPanel = new ArrayList();
        ArrayList arrayListTab = new ArrayList();
        ArrayList arrayListWinTabPage = new ArrayList();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //this.BackColor = Color.FromArgb(235, 240, 247);
            this.Font = new Font("Arial", 9);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        public FormBase()
        {

        }

        protected void SelectText(object sender)
        {
            ((UltraTextEditor)sender).SelectionStart = 0;
            ((UltraTextEditor)sender).SelectionLength = ((UltraTextEditor)sender).Text.Length;
            ((UltraTextEditor)sender).SelectAll();
        }

        protected void SelectTextAndFocus(UltraTextEditor sender)
        {
            sender.SelectAll();
            sender.Focus();
        }

        protected void ShowMessage(string message)
        {
            FMessageBox messageBox = new FMessageBox(message);
            messageBox.ShowDialog();
        }

        #region 页面，Grid 多语言
        /// <summary>
        /// 初始化页面多语言
        /// </summary>
        /// <param name="languageControl"></param>
        /// <param name="forceLoad"></param>
        public void InitPageLanguage()
        {
            this.FindAllControls(this);
            MultiLanguageHelper helper = null;
            foreach (Control control in this.Controls)
            {
                if (control != null)
                {


                    foreach (Control c in this.arrayListLabel)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, c.Text);
                        }
                        string lblName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control lbl = c;

                        if (lblName != null && lblName != "" && ((System.Windows.Forms.Label)lbl).Name != null)
                        {
                            ((System.Windows.Forms.Label)lbl).Text = lblName.Replace("\\r\\n", "\r\n");
                        }
                        //else if (((System.Windows.Forms.Label)lbl).Name != null && ((System.Windows.Forms.Label)lbl).Name != "")
                        //{
                        //    ((System.Windows.Forms.Label)lbl).Text = c.Name;
                        //}

                    }

                    foreach (Control c in this.arrayListUCLabel)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, ((UserControl.UCLabelEdit)c).Caption);
                        }
                        string lblName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control lbl = c;

                        if (lblName != null && lblName != "" && ((UserControl.UCLabelEdit)lbl).Name != null && ((UserControl.UCLabelEdit)lbl).Name != "")
                        {
                            ((UserControl.UCLabelEdit)lbl).Caption = lblName.Replace("\\t\\r", " ");
                        }
                        //else if (((UserControl.UCLabelEdit)lbl).Name != null && ((UserControl.UCLabelEdit)lbl).Name != "")
                        //{
                        //    ((UserControl.UCLabelEdit)lbl).Caption = c.Name;
                        //}
                    }

                    foreach (Control c in this.arrayListButton)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, c.Text);
                        }
                        string buttonName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control button = c;
                        if (buttonName != "" && ((System.Windows.Forms.Button)button).Name.ToString().Trim() != "")
                        {
                            ((System.Windows.Forms.Button)button).Text = buttonName;
                        }
                        else if (((System.Windows.Forms.Button)button).Name.ToString().Trim() != "")
                        {
                            ((System.Windows.Forms.Button)button).Text = c.Name;
                        }
                    }

                    foreach (Control c in this.arrayListUCButton)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, ((UserControl.UCButton)c).Caption);
                        }
                        string buttonName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control button = c;
                        if (buttonName != "" && ((UserControl.UCButton)button).Name.ToString().Trim() != "")
                        {
                            ((UserControl.UCButton)button).Caption = buttonName;
                        }
                        else if (((UserControl.UCButton)button).Name.ToString().Trim() != "")
                        {
                            ((UserControl.UCButton)button).Caption = c.Name;
                        }
                    }

                    foreach (Control c in this.arrayListCheckBox)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, c.Text);
                        }
                        string checkBoxName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control checkBox = c;

                        if (checkBoxName != null && checkBoxName != "" && ((System.Windows.Forms.CheckBox)checkBox).Name != null)
                        {
                            ((System.Windows.Forms.CheckBox)checkBox).Text = checkBoxName.Replace("\\t\\r", " ");
                        }
                        //else if (((System.Windows.Forms.CheckBox)checkBox).Name != null && ((System.Windows.Forms.CheckBox)checkBox).Name != "")
                        //{
                        //    ((System.Windows.Forms.CheckBox)checkBox).Text = c.Name;
                        //}

                    }

                    foreach (Control c in this.arrayListRadioButton)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, c.Text);
                        }
                        string radioButtonName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control radioButton = c;

                        if (radioButtonName != null && radioButtonName != "" && ((System.Windows.Forms.RadioButton)radioButton).Name != null)
                        {
                            ((System.Windows.Forms.RadioButton)radioButton).Text = radioButtonName;
                        }
                        //else if (((System.Windows.Forms.RadioButton)radioButton).Name != null && ((System.Windows.Forms.RadioButton)radioButton).Name != "")
                        //{
                        //    ((System.Windows.Forms.RadioButton)radioButton).Text = c.Name;
                        //}

                    }


                    foreach (Control c in this.arrayListGroupBox)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, c.Text);
                        }
                        string groupBoxName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control groupBox = c;

                        if (groupBoxName != null && ((System.Windows.Forms.GroupBox)groupBox).Name != null)
                        {
                            ((System.Windows.Forms.GroupBox)groupBox).Text = groupBoxName;
                        }
                        else if (((System.Windows.Forms.GroupBox)groupBox).Name != null && ((System.Windows.Forms.GroupBox)groupBox).Name != "")
                        {
                            ((System.Windows.Forms.GroupBox)groupBox).Text = c.Name;
                        }

                    }

                    foreach (Control c in this.arrayListComBox)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, ((UserControl.UCLabelCombox)c).Caption);
                        }
                        string comBoxName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        System.Windows.Forms.Control comBox = c;

                        if (comBoxName != null && comBoxName != "" && ((UserControl.UCLabelCombox)comBox).Name != null)
                        {
                            ((UserControl.UCLabelCombox)comBox).Caption = comBoxName;
                        }
                        //else if (((UserControl.UCLabelCombox)comBox).Name != null && ((UserControl.UCLabelCombox)comBox).Name != "")
                        //{
                        //    ((UserControl.UCLabelCombox)comBox).Caption = c.Name;
                        //}

                    }


                    foreach (Control c in this.arrayListOptionSet)
                    {
                        foreach (ValueListItem item in ((Infragistics.Win.UltraWinEditors.UltraOptionSet)c).Items)
                        {
                            string itemDisplayName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name + "_" + item.DataValue.ToString());
                            if (m_AutoCreateLanguage)
                            {
                                helper = new MultiLanguageHelper();
                                helper.AddControlLanguage("", "$CSPageControl_" + c.Name + "_" + item.DataValue.ToString(), item.DataValue.ToString(), item.DisplayText);
                            }
                            if (itemDisplayName != null && itemDisplayName != "" && item.DataValue != null && item.DataValue.ToString() != "")
                            {
                                item.DisplayText = itemDisplayName;
                            }
                            //else if (item.DataValue != null && item.DataValue.ToString() != "")
                            //{
                            //    item.DisplayText = item.DataValue.ToString();
                            //}
                        }

                    }

                    foreach (Control c in this.arrayListTab)
                    {
                        Infragistics.Win.UltraWinTabControl.UltraTabPageControl UCPageControl = c as Infragistics.Win.UltraWinTabControl.UltraTabPageControl;
                        string itemDisplayName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + UCPageControl.Name);
                        if (UCPageControl.Tab != null)
                        {
                            if (itemDisplayName != "")
                            {

                                UCPageControl.Tab.Text = itemDisplayName;

                            }
                            else
                            {
                                if (m_AutoCreateLanguage)
                                {
                                    helper = new MultiLanguageHelper();
                                    helper.AddControlLanguage("", "$CSPageControl_" + UCPageControl.Name, UCPageControl.Name, UCPageControl.Tab.Text);
                                }
                                UCPageControl.Tab.Text = UCPageControl.Name.ToString();
                            }

                        }

                    }

                    foreach (Control c in this.arrayListWinTabPage)
                    {
                        TabPage tabPage = c as TabPage;
                        string itemDisplayName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + tabPage.Name);

                        if (itemDisplayName != "")
                        {
                            tabPage.Text = itemDisplayName;
                        }
                        else
                        {
                            if (m_AutoCreateLanguage)
                            {
                                helper = new MultiLanguageHelper();
                                helper.AddControlLanguage("", "$CSPageControl_" + tabPage.Name, tabPage.Name, tabPage.Text);
                            }
                            tabPage.Text = tabPage.Name.ToString();
                        }

                    }

                    foreach (Control c in this.arrayListCheckEditor)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, c.Text);
                        }
                        string checkEditorName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        Control checkEditor = c;

                        if (checkEditorName != null && checkEditorName != "" && ((Infragistics.Win.UltraWinEditors.UltraCheckEditor)checkEditor).Name != null)
                        {
                            ((Infragistics.Win.UltraWinEditors.UltraCheckEditor)checkEditor).Text = checkEditorName;
                        }
                        //else if (((Infragistics.Win.UltraWinEditors.UltraCheckEditor)checkEditor).Name != null && ((Infragistics.Win.UltraWinEditors.UltraCheckEditor)checkEditor).Name != "")
                        //{
                        //    ((Infragistics.Win.UltraWinEditors.UltraCheckEditor)checkEditor).Text = c.Name;
                        //}

                    }

                    foreach (Control c in this.arrayListUCDateTime)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_" + c.Name, c.Name, ((UserControl.UCDatetTime)c).Caption);
                        }
                        string ucDateTimeName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + c.Name);
                        Control ucDateTime = c;

                        if (ucDateTimeName != null && ucDateTimeName != "" && ((UserControl.UCDatetTime)ucDateTime).Name != null)
                        {
                            ((UserControl.UCDatetTime)ucDateTime).Caption = ucDateTimeName;
                        }
                        //else if (((UserControl.UCDatetTime)ucDateTime).Name != null && ((UserControl.UCDatetTime)ucDateTime).Name != "")
                        //{
                        //    ((UserControl.UCDatetTime)ucDateTime).Caption = c.Name;
                        //}

                    }


                    foreach (Infragistics.Win.UltraWinStatusBar.UltraStatusPanel c in this.arrayListStatusPanel)
                    {
                        if (m_AutoCreateLanguage)
                        {
                            helper = new MultiLanguageHelper();
                            helper.AddControlLanguage("", "$CSPageControl_UltraStatusPanel_" + c.Key, c.Key, c.Text);
                        }

                        Infragistics.Win.UltraWinStatusBar.UltraStatusPanel statusPanel = c as Infragistics.Win.UltraWinStatusBar.UltraStatusPanel;

                        string statusPanelName = UserControl.MutiLanguages.ParserString("$CSPageControl_UltraStatusPanel_" + c.Key);


                        if (statusPanelName != null && statusPanelName != "" && (statusPanel).Key != null)
                        {
                            (statusPanel).Text = statusPanelName;
                        }


                    }

                }
            }

            string title = UserControl.MutiLanguages.ParserString("$CSPageControl_" + this.Name);
            if (title != string.Empty)
            {
                this.Text = title;
            }
            else
            {
                if (m_AutoCreateLanguage)
                {
                    helper = new MultiLanguageHelper();
                    helper.AddControlLanguage("", "$CSPageControl_" + this.Name, this.Name, this.Text);
                }
                //this.Text = this.Name;
            }

        }

        //遍历页面所有控件
        private void FindAllControls(System.Windows.Forms.Control control)
        {
            if (control is System.Windows.Forms.Label)
            {
                this.arrayListLabel.Add(control);
            }
            else if (control is UserControl.UCLabelEdit)
            {
                this.arrayListUCLabel.Add(control);
                return;
            }
            else if (control is System.Windows.Forms.CheckBox)
            {
                this.arrayListCheckBox.Add(control);
            }
            else if (control is UserControl.UCButton)
            {
                this.arrayListUCButton.Add(control);
                return;
            }
            else if (control is System.Windows.Forms.RadioButton)
            {
                this.arrayListRadioButton.Add(control);
            }
            else if (control is System.Windows.Forms.GroupBox)
            {
                this.arrayListGroupBox.Add(control);
            }
            else if (control is UserControl.UCLabelCombox)
            {
                this.arrayListComBox.Add(control);
                return;
            }
            else if (control is Infragistics.Win.UltraWinEditors.UltraOptionSet)
            {
                this.arrayListOptionSet.Add(control);
                return;
            }
            else if (control is Infragistics.Win.UltraWinEditors.UltraCheckEditor)
            {
                this.arrayListCheckEditor.Add(control);
                return;
            }
            else if (control is Infragistics.Win.UltraWinTabControl.UltraTabPageControl)
            {
                this.arrayListTab.Add(control);
            }
            else if (control is TabPage)
            {
                this.arrayListWinTabPage.Add(control);
            }
            else if (control is UserControl.UCDatetTime)
            {
                this.arrayListUCDateTime.Add(control);
                return;
            }
            else if (control is Infragistics.Win.UltraWinStatusBar.UltraStatusBar)
            {
                Infragistics.Win.UltraWinStatusBar.UltraStatusBar statusBar = control as Infragistics.Win.UltraWinStatusBar.UltraStatusBar;
                //statusBar.Panels
                foreach (Infragistics.Win.UltraWinStatusBar.UltraStatusPanel statusPanel in statusBar.Panels)
                {
                    if (!this.arrayListStatusPanel.Contains(statusPanel))
                    {
                        this.arrayListStatusPanel.Add(statusPanel);
                    }
                }

            }
            if (control != null)
            {
                foreach (Control sonControls in control.Controls)
                {
                    this.FindAllControls(sonControls);
                }
            }

        }

        /// <summary>
        /// 初始化页面多语言
        /// </summary>
        /// <param name="languageControl"></param>
        /// <param name="forceLoad"></param>
        public void InitGridLanguage(UltraGrid grid)
        {
            if (grid != null)
            {
                string gridName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + grid.Name);
                if (m_AutoCreateLanguage)
                {
                    MultiLanguageHelper helper = new MultiLanguageHelper();
                    helper.AddControlLanguage("", "$CSPageControl_" + grid.Name, grid.Name, grid.Text);
                }
                if (gridName != null && gridName != "")
                {
                    grid.Text = gridName;
                }
                //else
                //{
                //    grid.Text = grid.Name;
                //}


                if (grid.DisplayLayout.Bands != null && grid.DisplayLayout.Bands.Count > 0)
                {
                    for (int i = 0; i < grid.DisplayLayout.Bands.Count; i++)
                    {
                        if (grid.DisplayLayout.Bands[i].Columns != null && grid.DisplayLayout.Bands[i].Columns.Count > 0)
                        {
                            foreach (UltraGridColumn column in grid.DisplayLayout.Bands[i].Columns)
                            {
                                string headerName = UserControl.MutiLanguages.ParserString("$CSGrid_" + column.Key);
                                if (m_AutoCreateLanguage)
                                {
                                    MultiLanguageHelper helper = new MultiLanguageHelper();
                                    helper.AddControlLanguage("", "$CSGrid_" + column.Key, column.Key, column.Header.Caption);
                                }
                                if (headerName != null && headerName != "")
                                {
                                    column.Header.Caption = headerName;
                                }
                                //else
                                //{
                                //    column.Header.Caption = column.Key;
                                //}
                            }
                        }
                    }
                }
            }

        }

        public void InitUCErrorCodeSelectNewLanguage(UserControl.UCErrorCodeSelectNew errorCodeSelect)
        {
            string lblName = UserControl.MutiLanguages.ParserString("$CSPageControl_" + errorCodeSelect.ucLabelEditErrorCode.Name);

            if (lblName != null && lblName != "" && errorCodeSelect.ucLabelEditErrorCode.Name != null && (errorCodeSelect.ucLabelEditErrorCode.Name != ""))
            {
                errorCodeSelect.ucLabelEditErrorCode.Caption = lblName;
            }
            else if (errorCodeSelect.ucLabelEditErrorCode.Name != null && errorCodeSelect.ucLabelEditErrorCode.Name != "")
            {
                errorCodeSelect.ucLabelEditErrorCode.Caption = errorCodeSelect.ucLabelEditErrorCode.Name;
            }

            InitGridLanguage(errorCodeSelect.ultraGridErrorList);
        }
        #endregion        

        private void FormBase_Load(object sender, EventArgs e)
        {
            UserControl.MutiLanguages.Language = ApplicationService.Current().Language;
        }
    }
}
