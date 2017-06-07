using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;

using UserControl;


namespace BenQGuru.eMES.ClientWatchPanel
{
    public partial class FLineSelected : Form
    {
        private IDomainDataProvider _dataProvider;

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_dataProvider == null)
                {

                    _dataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
                }

                return _dataProvider;
            }

            set
            {
                _dataProvider = value;
            }
        }        

        public FLineSelected()
        {
            InitializeComponent();
        }

        private void FLineSelected_Load(object sender, EventArgs e)
        {
            InitializeBigLine();
        }

        private void InitializeBigLine()
        {
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            object[] bigLineList = baseModelFacade.QueryBigSSCodeFromSystem();
            if (bigLineList!=null)
            {
                this.ucLabelComboxBigLineLevel.Clear();
                foreach (Parameter obj in bigLineList)
                {
                    this.ucLabelComboxBigLineLevel.AddItem(obj.ParameterAlias, obj.ParameterAlias);
                }                
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.ucLabelComboxBigLineLevel.SelectedItemValue==null)
            {
                return;
            }

            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            object[] bigList = baseModelFacade.QueryStepSequenceByBigSSCode(this.ucLabelComboxBigLineLevel.SelectedItemValue.ToString().ToUpper());

            if (bigList==null)
            {
                if (MessageBox.Show("大线没有维护产线!", "提示", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    return;
                }                
            }

            Form flineChart = new FLIneWatchPanel(this.ucLabelComboxBigLineLevel.SelectedItemValue.ToString());
            flineChart.ShowDialog(this);
            flineChart.Dispose();
        }

        private void uBtnCancel_Click(object sender, EventArgs e)
        {
            //this.Close();
        }
    }
}