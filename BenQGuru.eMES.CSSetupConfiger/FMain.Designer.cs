namespace BenQGuru.eMES.CSSetupConfiger
{
    partial class FMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxShow = new System.Windows.Forms.GroupBox();
            this.gridDb = new System.Windows.Forms.DataGridView();
            this.groupBoxEdit = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.txtChar = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.colDbName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPassword = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProvider = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colDefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.comboBoxDriver = new System.Windows.Forms.ComboBox();
            this.groupBoxShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDb)).BeginInit();
            this.groupBoxEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxShow
            // 
            this.groupBoxShow.Controls.Add(this.gridDb);
            this.groupBoxShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxShow.Location = new System.Drawing.Point(0, 0);
            this.groupBoxShow.Name = "groupBoxShow";
            this.groupBoxShow.Size = new System.Drawing.Size(786, 259);
            this.groupBoxShow.TabIndex = 0;
            this.groupBoxShow.TabStop = false;
            this.groupBoxShow.Text = "数据库信息";
            // 
            // gridDb
            // 
            this.gridDb.AllowUserToAddRows = false;
            this.gridDb.AllowUserToDeleteRows = false;
            this.gridDb.BackgroundColor = System.Drawing.Color.White;
            this.gridDb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDb.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDbName,
            this.colDataSource,
            this.colUserName,
            this.colPassword,
            this.colProvider,
            this.colChar,
            this.colEdit,
            this.colDel,
            this.colDefault});
            this.gridDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDb.Location = new System.Drawing.Point(3, 17);
            this.gridDb.Name = "gridDb";
            this.gridDb.RowTemplate.Height = 23;
            this.gridDb.Size = new System.Drawing.Size(780, 239);
            this.gridDb.TabIndex = 0;
            this.gridDb.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridDb_CellContentClick);
            // 
            // groupBoxEdit
            // 
            this.groupBoxEdit.Controls.Add(this.comboBoxDriver);
            this.groupBoxEdit.Controls.Add(this.label8);
            this.groupBoxEdit.Controls.Add(this.label7);
            this.groupBoxEdit.Controls.Add(this.btnReset);
            this.groupBoxEdit.Controls.Add(this.chkDefault);
            this.groupBoxEdit.Controls.Add(this.label6);
            this.groupBoxEdit.Controls.Add(this.btnExit);
            this.groupBoxEdit.Controls.Add(this.btnSave);
            this.groupBoxEdit.Controls.Add(this.btnTest);
            this.groupBoxEdit.Controls.Add(this.txtChar);
            this.groupBoxEdit.Controls.Add(this.label5);
            this.groupBoxEdit.Controls.Add(this.txtPassword);
            this.groupBoxEdit.Controls.Add(this.label4);
            this.groupBoxEdit.Controls.Add(this.txtUserName);
            this.groupBoxEdit.Controls.Add(this.label3);
            this.groupBoxEdit.Controls.Add(this.txtDataSource);
            this.groupBoxEdit.Controls.Add(this.label2);
            this.groupBoxEdit.Controls.Add(this.txtDbName);
            this.groupBoxEdit.Controls.Add(this.label1);
            this.groupBoxEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxEdit.Location = new System.Drawing.Point(0, 259);
            this.groupBoxEdit.Name = "groupBoxEdit";
            this.groupBoxEdit.Size = new System.Drawing.Size(786, 216);
            this.groupBoxEdit.TabIndex = 1;
            this.groupBoxEdit.TabStop = false;
            this.groupBoxEdit.Text = "添加/编辑";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(202, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(371, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "注：选择OraOLEDB.Oracle.1，须安装Oracle Provider for Ole DB。";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(333, 115);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "驱动类型";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(332, 177);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 16;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Location = new System.Drawing.Point(140, 147);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(15, 14);
            this.chkDefault.TabIndex = 15;
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(105, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "默认";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(414, 177);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(251, 177);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(170, 177);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "测试连接";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // txtChar
            // 
            this.txtChar.Location = new System.Drawing.Point(140, 110);
            this.txtChar.Name = "txtChar";
            this.txtChar.Size = new System.Drawing.Size(180, 21);
            this.txtChar.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(93, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "字符集";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(393, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(180, 21);
            this.txtPassword.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(358, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "密码";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(140, 68);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(180, 21);
            this.txtUserName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户名";
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(393, 29);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(180, 21);
            this.txtDataSource.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "服务名";
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(140, 29);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(180, 21);
            this.txtDbName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库名称";
            // 
            // colDbName
            // 
            this.colDbName.DataPropertyName = "colDbName";
            this.colDbName.HeaderText = "数据库名称";
            this.colDbName.Name = "colDbName";
            this.colDbName.ReadOnly = true;
            // 
            // colDataSource
            // 
            this.colDataSource.DataPropertyName = "colDataSource";
            this.colDataSource.HeaderText = "服务名";
            this.colDataSource.Name = "colDataSource";
            this.colDataSource.ReadOnly = true;
            // 
            // colUserName
            // 
            this.colUserName.DataPropertyName = "colUserName";
            this.colUserName.HeaderText = "用户名";
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            // 
            // colPassword
            // 
            this.colPassword.DataPropertyName = "colPassword";
            this.colPassword.HeaderText = "密码";
            this.colPassword.Name = "colPassword";
            this.colPassword.ReadOnly = true;
            this.colPassword.Width = 80;
            // 
            // colProvider
            // 
            this.colProvider.DataPropertyName = "colProvider";
            this.colProvider.HeaderText = "驱动类型";
            this.colProvider.Name = "colProvider";
            // 
            // colChar
            // 
            this.colChar.DataPropertyName = "colChar";
            this.colChar.HeaderText = "字符集";
            this.colChar.Name = "colChar";
            this.colChar.ReadOnly = true;
            // 
            // colEdit
            // 
            this.colEdit.HeaderText = "编辑";
            this.colEdit.Name = "colEdit";
            this.colEdit.Text = "编辑";
            this.colEdit.UseColumnTextForButtonValue = true;
            this.colEdit.Width = 50;
            // 
            // colDel
            // 
            this.colDel.HeaderText = "删除";
            this.colDel.Name = "colDel";
            this.colDel.Text = "删除";
            this.colDel.UseColumnTextForButtonValue = true;
            this.colDel.Width = 50;
            // 
            // colDefault
            // 
            this.colDefault.DataPropertyName = "colDefault";
            this.colDefault.FalseValue = "False";
            this.colDefault.HeaderText = "默认";
            this.colDefault.Name = "colDefault";
            this.colDefault.ReadOnly = true;
            this.colDefault.TrueValue = "True";
            this.colDefault.Width = 40;
            // 
            // comboBoxDriver
            // 
            this.comboBoxDriver.FormattingEnabled = true;
            this.comboBoxDriver.Items.AddRange(new object[] {
            "OraOLEDB.Oracle.1",
            "MSDAORA.1"});
            this.comboBoxDriver.Location = new System.Drawing.Point(393, 110);
            this.comboBoxDriver.Name = "comboBoxDriver";
            this.comboBoxDriver.Size = new System.Drawing.Size(180, 20);
            this.comboBoxDriver.TabIndex = 22;
            this.comboBoxDriver.Text = "OraOLEDB.Oracle.1";
            // 
            // FMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 475);
            this.Controls.Add(this.groupBoxEdit);
            this.Controls.Add(this.groupBoxShow);
            this.MaximizeBox = false;
            this.Name = "FMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置数据库信息";
            this.Load += new System.EventHandler(this.FMain_Load);
            this.groupBoxShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDb)).EndInit();
            this.groupBoxEdit.ResumeLayout(false);
            this.groupBoxEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxShow;
        private System.Windows.Forms.DataGridView gridDb;
        private System.Windows.Forms.GroupBox groupBoxEdit;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TextBox txtChar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDefault;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDbName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPassword;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProvider;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChar;
        private System.Windows.Forms.DataGridViewButtonColumn colEdit;
        private System.Windows.Forms.DataGridViewButtonColumn colDel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colDefault;
        private System.Windows.Forms.ComboBox comboBoxDriver;
    }
}

