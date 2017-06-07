namespace BenQGuru.eMES.Client
{
    partial class FReturneMaterial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FReturneMaterial));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSendMetrial = new UserControl.UCButton();
            this.edtReturnNumber = new UserControl.UCLabelEdit();
            this.edtMaterialLot = new UserControl.UCLabelEdit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Controls.Add(this.btnSendMetrial);
            this.groupBox1.Controls.Add(this.edtReturnNumber);
            this.groupBox1.Controls.Add(this.edtMaterialLot);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnSendMetrial
            // 
            this.btnSendMetrial.BackColor = System.Drawing.SystemColors.Control;
            this.btnSendMetrial.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSendMetrial.BackgroundImage")));
            this.btnSendMetrial.ButtonType = UserControl.ButtonTypes.None;
            this.btnSendMetrial.Caption = "退料";
            this.btnSendMetrial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendMetrial.Location = new System.Drawing.Point(472, 35);
            this.btnSendMetrial.Name = "btnSendMetrial";
            this.btnSendMetrial.Size = new System.Drawing.Size(88, 24);
            this.btnSendMetrial.TabIndex = 24;
            this.btnSendMetrial.Click += new System.EventHandler(this.btnSendMetrial_Click);
            // 
            // edtReturnNumber
            // 
            this.edtReturnNumber.AllowEditOnlyChecked = true;
            this.edtReturnNumber.Caption = "数量";
            this.edtReturnNumber.Checked = false;
            this.edtReturnNumber.EditType = UserControl.EditTypes.Integer;
            this.edtReturnNumber.Location = new System.Drawing.Point(331, 35);
            this.edtReturnNumber.MaxLength = 8;
            this.edtReturnNumber.Multiline = false;
            this.edtReturnNumber.Name = "edtReturnNumber";
            this.edtReturnNumber.PasswordChar = '\0';
            this.edtReturnNumber.ReadOnly = false;
            this.edtReturnNumber.ShowCheckBox = false;
            this.edtReturnNumber.Size = new System.Drawing.Size(87, 27);
            this.edtReturnNumber.TabIndex = 23;
            this.edtReturnNumber.TabNext = false;
            this.edtReturnNumber.Value = "";
            this.edtReturnNumber.WidthType = UserControl.WidthTypes.Tiny;
            this.edtReturnNumber.XAlign = 368;
            this.edtReturnNumber.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtReturnNumber_TxtboxKeyPress);
            // 
            // edtMaterialLot
            // 
            this.edtMaterialLot.AllowEditOnlyChecked = true;
            this.edtMaterialLot.Caption = "物料批号";
            this.edtMaterialLot.Checked = false;
            this.edtMaterialLot.EditType = UserControl.EditTypes.String;
            this.edtMaterialLot.Location = new System.Drawing.Point(24, 35);
            this.edtMaterialLot.MaxLength = 40;
            this.edtMaterialLot.Multiline = false;
            this.edtMaterialLot.Name = "edtMaterialLot";
            this.edtMaterialLot.PasswordChar = '\0';
            this.edtMaterialLot.ReadOnly = false;
            this.edtMaterialLot.ShowCheckBox = false;
            this.edtMaterialLot.Size = new System.Drawing.Size(261, 27);
            this.edtMaterialLot.TabIndex = 18;
            this.edtMaterialLot.TabNext = false;
            this.edtMaterialLot.Value = "";
            this.edtMaterialLot.WidthType = UserControl.WidthTypes.Long;
            this.edtMaterialLot.XAlign = 85;
            this.edtMaterialLot.TxtboxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtMaterialLot_TxtboxKeyPress);
            // 
            // FReturneMaterial
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(642, 164);
            this.Controls.Add(this.groupBox1);
            this.Name = "FReturneMaterial";
            this.Text = "退料";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControl.UCLabelEdit edtMaterialLot;
        private UserControl.UCLabelEdit edtReturnNumber;
        private UserControl.UCButton btnSendMetrial;
    }
}