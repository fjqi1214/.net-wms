using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Client.Service;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.MOModel;

namespace BenQGuru.eMES.Client
{
    public partial class FErrorPartSelect : BaseForm
    {
        private IDomainDataProvider _domainDataProvider = ApplicationService.Current().DataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public FErrorPartSelect()
        {
            InitializeComponent();
            UserControl.UIStyleBuilder.FormUI(this);
            UserControl.UIStyleBuilder.GridUI(this.ultraGridMaterial);
        }

        private string _ItemCode = string.Empty;
        private string _ErrorLocation = string.Empty;
        private string _SelectedErrorPart = string.Empty;

        public string ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }

        public string ErrorLocation
        {
            get { return _ErrorLocation; }
            set { _ErrorLocation = value; }
        }

        public string SelectedErrorPart
        {
            get
            {
                return _SelectedErrorPart;
            }
        }

        private void ultraGridMaterial_DoubleClick(object sender, EventArgs e)
        {
            if (ultraGridMaterial.Rows.CardAreaHeight > 0 && ultraGridMaterial.ActiveRow != null)
            {
                SetReturnValue();
                this.Close();
            }
        }

        private void ucButtonConfirm_Click(object sender, EventArgs e)
        {
            SetReturnValue();
            this.Close();
        }

        private void ucButtonCancel_Click(object sender, EventArgs e)
        {
            _SelectedErrorPart = string.Empty;
            this.Close();
        }

        private void FMaterialCodeSelect_Load(object sender, EventArgs e)
        {
            LoadMaterialList(_ItemCode, _ErrorLocation);
            //this.InitPageLanguage();
            //this.InitGridLanguage(ultraGridMaterial);

        }

        private void LoadMaterialList(string itemCode, string location)
        {
            object[] materialList = (new ItemFacade(DataProvider)).QueryMaterialForErrorPart(itemCode, location);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("MaterialCode");
            dataTable.Columns.Add("MaterialDesc");

            if (materialList != null)
            {
                foreach (BenQGuru.eMES.Domain.MOModel.Material material in materialList)
                {
                    DataRow row = dataTable.NewRow();
                    row["MaterialCode"] = material.MaterialCode;
                    row["MaterialDesc"] = material.MaterialDescription;
                    dataTable.Rows.Add(row);
                }
            }

            this.ultraGridMaterial.DataSource = dataTable;
            this.ultraGridMaterial.DataBind();
        }

        private void SetReturnValue()
        {
            if (this.ultraGridMaterial.ActiveRow != null)
            {
                _SelectedErrorPart = this.ultraGridMaterial.ActiveRow.Cells["MaterialCode"].Text.Trim();
            }
            else
            {
                _SelectedErrorPart = string.Empty;
            }
        }
    }
}