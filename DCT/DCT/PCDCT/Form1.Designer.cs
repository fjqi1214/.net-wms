namespace BenQGuru.eMES.Common.DCT.PC
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.textBoxMessageHistory = new System.Windows.Forms.TextBox();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBoxCommandHistory = new System.Windows.Forms.TextBox();
            this.textBoxServerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelSpeaker = new System.Windows.Forms.Label();
            this.textBoxInputHistory = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxMessageHistory
            // 
            this.textBoxMessageHistory.Location = new System.Drawing.Point(380, 24);
            this.textBoxMessageHistory.Multiline = true;
            this.textBoxMessageHistory.Name = "textBoxMessageHistory";
            this.textBoxMessageHistory.ReadOnly = true;
            this.textBoxMessageHistory.Size = new System.Drawing.Size(277, 319);
            this.textBoxMessageHistory.TabIndex = 1;
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(191, 458);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(466, 20);
            this.textBoxInput.TabIndex = 2;
            this.textBoxInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInput_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBoxCommandHistory
            // 
            this.textBoxCommandHistory.Location = new System.Drawing.Point(12, 24);
            this.textBoxCommandHistory.Multiline = true;
            this.textBoxCommandHistory.Name = "textBoxCommandHistory";
            this.textBoxCommandHistory.ReadOnly = true;
            this.textBoxCommandHistory.Size = new System.Drawing.Size(159, 319);
            this.textBoxCommandHistory.TabIndex = 1;
            // 
            // textBoxServerIP
            // 
            this.textBoxServerIP.Location = new System.Drawing.Point(12, 458);
            this.textBoxServerIP.Name = "textBoxServerIP";
            this.textBoxServerIP.Size = new System.Drawing.Size(159, 20);
            this.textBoxServerIP.TabIndex = 2;
            this.textBoxServerIP.TextChanged += new System.EventHandler(this.textBoxServerIP_TextChanged);
            this.textBoxServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInput_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Command: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Input History: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 443);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Server IP: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 443);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Input: ";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxMessage.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessage.ForeColor = System.Drawing.SystemColors.Window;
            this.textBoxMessage.Location = new System.Drawing.Point(191, 361);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.Size = new System.Drawing.Size(466, 79);
            this.textBoxMessage.TabIndex = 1;
            this.textBoxMessage.Text = "1123456789一二三四五六七八九十\r\n2123456789一二三四五六七八九十\r\n3123456789一二三四五六七八九十\r\n4123456789一二三四五" +
                "六七八九十";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(189, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Message: ";
            // 
            // labelSpeaker
            // 
            this.labelSpeaker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSpeaker.Location = new System.Drawing.Point(136, 355);
            this.labelSpeaker.Name = "labelSpeaker";
            this.labelSpeaker.Size = new System.Drawing.Size(20, 20);
            this.labelSpeaker.TabIndex = 4;
            this.labelSpeaker.Tag = "";
            // 
            // textBoxInputHistory
            // 
            this.textBoxInputHistory.Location = new System.Drawing.Point(191, 24);
            this.textBoxInputHistory.Multiline = true;
            this.textBoxInputHistory.Name = "textBoxInputHistory";
            this.textBoxInputHistory.ReadOnly = true;
            this.textBoxInputHistory.Size = new System.Drawing.Size(169, 319);
            this.textBoxInputHistory.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(378, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Message History: ";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(26, 358);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(93, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "ShowSpeaker";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(669, 491);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labelSpeaker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxServerIP);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.textBoxCommandHistory);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.textBoxInputHistory);
            this.Controls.Add(this.textBoxMessageHistory);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "PC DCT 100";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxMessageHistory;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBoxCommandHistory;
        private System.Windows.Forms.TextBox textBoxServerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelSpeaker;
        private System.Windows.Forms.TextBox textBoxInputHistory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

