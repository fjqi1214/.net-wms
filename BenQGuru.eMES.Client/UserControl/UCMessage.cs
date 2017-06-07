using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using BenQGuru.eMES.Web.Helper;

namespace UserControl
{
    /// <summary>
    /// UCMessage 的摘要说明。
    /// Amoi,Laws Lu,2005/08/01,RichTextBox替代ListBox
    /// </summary>
    public class UCMessage : System.Windows.Forms.UserControl
    {
        private UserControl.UCButton btnClear;
        private UserControl.UCButton btnClose;
        private UserControl.UCButton btnSave;
        private UserControl.UCButton btnUpdate;
        private System.Windows.Forms.RichTextBox listMessage;


        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        /// 

        private System.ComponentModel.Container components = null;

        public UCMessage()
        {
            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

        }

        public event WorkingErrorAddedEventHandler WorkingErrorAdded;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }


        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCMessage));
            this.btnClear = new UserControl.UCButton();
            this.btnClose = new UserControl.UCButton();
            this.btnSave = new UserControl.UCButton();
            this.btnUpdate = new UserControl.UCButton();
            this.listMessage = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.SystemColors.Control;
            this.btnClear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClear.BackgroundImage")));
            this.btnClear.ButtonType = UserControl.ButtonTypes.None;
            this.btnClear.Caption = "清空信息";
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Location = new System.Drawing.Point(376, 188);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(88, 22);
            this.btnClear.TabIndex = 1;
            this.btnClear.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.ButtonType = UserControl.ButtonTypes.None;
            this.btnClose.Caption = "强制结束";
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Location = new System.Drawing.Point(376, 156);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 22);
            this.btnClose.TabIndex = 2;
            this.btnClose.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.Control;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.ButtonType = UserControl.ButtonTypes.None;
            this.btnSave.Caption = "保存信息";
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(376, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 22);
            this.btnSave.TabIndex = 3;
            this.btnSave.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.btnUpdate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUpdate.BackgroundImage")));
            this.btnUpdate.ButtonType = UserControl.ButtonTypes.None;
            this.btnUpdate.Caption = "更正";
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Location = new System.Drawing.Point(376, 124);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(88, 22);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Visible = false;
            // 
            // listMessage
            // 
            this.listMessage.BackColor = System.Drawing.Color.White;
            this.listMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listMessage.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listMessage.Location = new System.Drawing.Point(0, 0);
            this.listMessage.Name = "listMessage";
            this.listMessage.ReadOnly = true;
            this.listMessage.Size = new System.Drawing.Size(480, 260);
            this.listMessage.TabIndex = 5;
            this.listMessage.TabStop = false;
            this.listMessage.Text = "";
            // 
            // UCMessage
            // 
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.Controls.Add(this.listMessage);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClear);
            this.Name = "UCMessage";
            this.Size = new System.Drawing.Size(480, 260);
            this.ResumeLayout(false);

        }
        #endregion
        private int _return = 0;

        //Laws Lu,2006/06/22	add supoort predefine font style
        public int AddText(string text, Font prFont, Color prColor)
        {
            AddWithoutEnter("");
            this.listMessage.SelectionColor = prColor;
            this.listMessage.SelectionFont = prFont;

            return AddText(text);
        }

        //Laws Lu，2006/03/15 新增	外部添加文本
        public int AddBoldText(string text)
        {
            Font boldfont = new Font("宋体", 14F, FontStyle.Bold);
            AddWithoutEnter("");
            this.listMessage.SelectionColor = Color.Black;
            this.listMessage.SelectionFont = boldfont;

            return AddText(text);
        }

        private int AddText(string text)
        {
            CheckClearContent();

            listMessage.AppendText(MutiLanguages.ParserMessage(text) + "\r\n");
            //Amoi,Laws Lu,2005/08/05,修改	允许滚动条自动滚动到添加字符的位置
            listMessage.Focus();
            listMessage.ScrollToCaret();

            //EndAmoi

            return _return;
        }

        public int AddWithoutEnter(string text)
        {
            CheckClearContent();

            listMessage.AppendText(MutiLanguages.ParserMessage(text));
            //Amoi,Laws Lu,2005/08/05,修改	允许滚动条自动滚动到添加字符的位置
            listMessage.Focus();
            listMessage.ScrollToCaret();

            //EndAmoi

            return _return;
        }

        #region 以前的版本
        //		//Laws Lu,2005/09/12,修改	文字提示的颜色
        //		public void Add(UserControl.Message message)
        //		{
        //			switch(message.Type)
        //			{
        //				case MessageType.Normal:
        //					listMessage.AppendText("");
        //					this.listMessage.SelectionColor = Color.Black;
        //					this.Add(message.Body);
        //					break;
        //				case MessageType.Succes:
        //					listMessage.AppendText("");
        //					this.listMessage.SelectionColor = Color.Blue;
        //					this.Add(message.Body);
        //					break;
        //				case MessageType.Error:
        //					listMessage.AppendText("");
        //					this.listMessage.SelectionColor = Color.Red;
        //
        //					String errMessages = String.Empty;
        //					if (message.Body==string.Empty)
        //					{
        //						errMessages = message.Exception.Message;
        //					}
        //					else
        //					{
        //						errMessages = message.Body;
        //					}
        //				
        //					this.Add(errMessages);
        //					
        //#if DEBUG
        //					string[] messageStr=message.Debug().Split('。');
        //					for (int m=0;m<messageStr.Length;m++)
        //						FileLog.FileLogOut(FileLog.FileName,messageStr[m]);
        //					FileLog.FileLogOut(FileLog.FileName,"====漂=亮=的=分=割=线=====================");
        //#endif
        //					break;
        //					//					case MessageType.Debug:
        //					//						this.Add(messages.Objects(i).Body);
        //					//						break;
        //			}
        //		}

        #endregion

        //Laws Lu,2006/03/14,修改	文字提示的颜色
        public void Add(UserControl.Message message)
        {
            Font norfont = new Font("宋体", 12F);
            Font boldFont = new Font("宋体", 16F, FontStyle.Bold);

            switch (message.Type)
            {
                case MessageType.Normal:
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Black;
                    this.listMessage.SelectionFont = norfont;

                    this.AddText(message.Body);
                    break;
                case MessageType.Success:

                    #region 处理成功的信息显示
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Blue;
                    this.listMessage.SelectionFont = norfont;

                    String sucMessages = String.Empty;
                    String sucCard = String.Empty;

                    if (message.Body == string.Empty)
                    {
                        sucMessages = message.Exception.Message;
                    }
                    else
                    {
                        sucMessages = message.Body;
                    }

                    int iSucPosition = sucMessages.IndexOf("$CS_Param_ID");
                    if (iSucPosition > 0)
                    {
                        if (sucMessages.IndexOf(":") >= 0 || sucMessages.IndexOf("=") >= 0)
                        {
                            iSucPosition = iSucPosition + 1;
                        }
                        sucCard = sucMessages.Substring(iSucPosition + 12, sucMessages.Length - iSucPosition - 12);
                        sucMessages = sucMessages.Remove(iSucPosition + 12, sucMessages.Length - iSucPosition - 12);
                        //添加非产品序列号的信息
                        this.AddWithoutEnter(sucMessages);

                        this.listMessage.SelectionColor = Color.Black;
                        this.listMessage.SelectionFont = boldFont;
                        //获取下一个分隔的位置
                        int iNextEmptyPosition = sucCard.Trim().IndexOf(" ");
                        if (iNextEmptyPosition < 0)
                        {
                            this.AddText(sucCard);
                        }
                        else
                        {
                            sucCard = sucCard.Substring(0, iNextEmptyPosition);
                            sucMessages = sucCard.Remove(0, iNextEmptyPosition);

                            this.AddText(sucCard);

                            this.listMessage.SelectionColor = Color.Blue;
                            this.listMessage.SelectionFont = norfont;

                            if (sucMessages != String.Empty)
                            {
                                this.AddText(sucMessages);
                            }
                        }

                    }
                    else
                    {
                        this.AddText(sucMessages);
                    }

                    //added by leon.li @20130311 声音提示
                    SoundPlayer.PlaySuccessMusic();
                    break;
                    #endregion

                case MessageType.DisplayError:
                case MessageType.Error:

                    #region 处理错误的信息显示
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Red;
                    this.listMessage.SelectionFont = norfont;

                    String errMessages = String.Empty;
                    String rCard = String.Empty;

                    if (message.Body == string.Empty)
                    {
                        errMessages = message.Exception.Message;
                    }
                    else
                    {
                        errMessages = message.Body;
                    }

                    int iPosition = errMessages.IndexOf("$CS_Param_ID");
                    if (iPosition > 0)
                    {
                        if (errMessages.IndexOf(":") >= 0 || errMessages.IndexOf("=") >= 0)
                        {
                            iPosition = iPosition + 1;
                        }
                        rCard = errMessages.Substring(iPosition + 12, errMessages.Length - iPosition - 12);
                        errMessages = errMessages.Remove(iPosition + 12, errMessages.Length - iPosition - 12);
                        //添加非产品序列号的信息
                        this.AddWithoutEnter(errMessages);

                        this.listMessage.SelectionColor = Color.Black;
                        this.listMessage.SelectionFont = boldFont;
                        //获取下一个分隔的位置
                        int iNextEmptyPosition = rCard.Trim().IndexOf(" ");
                        if (iNextEmptyPosition < 0)
                        {
                            this.AddText(rCard);
                        }
                        else
                        {
                            rCard = rCard.Substring(0, iNextEmptyPosition);
                            errMessages = rCard.Remove(0, iNextEmptyPosition);

                            this.AddText(rCard);

                            this.listMessage.SelectionColor = Color.Red;
                            this.listMessage.SelectionFont = norfont;

                            if (errMessages != String.Empty)
                            {
                                this.AddText(errMessages);
                            }
                        }


                        //errMessages = errMessages.Replace("","");
                    }
                    else
                    {
                        this.AddText(errMessages);
                    }
#if DEBUG
                    string[] messageStr = message.Debug().Split('。');
                    for (int m = 0; m < messageStr.Length; m++)
                        FileLog.FileLogOut(FileLog.FileName, messageStr[m]);
                    FileLog.FileLogOut(FileLog.FileName, "====漂=亮=的=分=割=线=====================");
#endif
                    //added by leon.li @20130311 声音提示
                    SoundPlayer.PlayErrorMusic();
                    break;
                //					case MessageType.Debug:
                //						this.Add(messages.Objects(i).Body);
                //						break;

                    #endregion

            }
        }

        public void Add(Messages messages)
        {
            for (int i = 0; i < messages.Count(); i++)
            {
                this.Add(messages.Objects(i));
            }
        }

        //Laws Lu，2006/03/15 新增	外部添加文本
        public int Add(string text)
        {
            Font norfont = new Font("宋体", 12F);
            AddWithoutEnter("");
            this.listMessage.SelectionColor = Color.Black;
            this.listMessage.SelectionFont = norfont;

            return AddText(text);
        }

        public void AddEx(string function, string inputContent, Message message, bool recordWorkingError)
        {
            Font norfont = new Font("宋体", 12F);
            Font boldFont = new Font("宋体", 16F, FontStyle.Bold);

            switch (message.Type)
            {
                case MessageType.Normal:
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Black;
                    this.listMessage.SelectionFont = norfont;

                    this.AddText(message.Body);
                    break;
                case MessageType.Success:

                    #region 处理成功的信息显示
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Blue;
                    this.listMessage.SelectionFont = norfont;

                    String sucMessages = String.Empty;
                    String sucCard = String.Empty;

                    if (message.Body == string.Empty)
                    {
                        sucMessages = message.Exception.Message;
                    }
                    else
                    {
                        sucMessages = message.Body;
                    }

                    //int iSucPosition = sucMessages.IndexOf("$CS_Param_ID");
                    //if (iSucPosition > 0)
                    //{
                    //    if (sucMessages.IndexOf(":") >= 0 || sucMessages.IndexOf("=") >= 0)
                    //    {
                    //        iSucPosition = iSucPosition + 1;
                    //    }
                    //    sucCard = sucMessages.Substring(iSucPosition + 12, sucMessages.Length - iSucPosition - 12);
                    //    sucMessages = sucMessages.Remove(iSucPosition + 12, sucMessages.Length - iSucPosition - 12);
                    //    //添加非产品序列号的信息
                    //    this.AddWithoutEnter(sucMessages);

                    //    this.listMessage.SelectionColor = Color.Black;
                    //    this.listMessage.SelectionFont = boldFont;
                    //    //获取下一个分隔的位置
                    //    int iNextEmptyPosition = sucCard.Trim().IndexOf(" ");
                    //    if (iNextEmptyPosition < 0)
                    //    {
                    //        this.AddText(sucCard);
                    //    }
                    //    else
                    //    {
                    //        sucCard = sucCard.Substring(0, iNextEmptyPosition);
                    //        sucMessages = sucCard.Remove(0, iNextEmptyPosition);

                    //        this.AddText(sucCard);

                    //        this.listMessage.SelectionColor = Color.Blue;
                    //        this.listMessage.SelectionFont = norfont;

                    //        if (sucMessages != String.Empty)
                    //        {
                    //            this.AddText(sucMessages);
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    this.AddText(sucMessages);
                    //}

                    //added by leon.li @20130311 声音提示
                    SoundPlayer.PlaySuccessMusic();

                    break;
                    #endregion

                case MessageType.DisplayError:
                case MessageType.Error:

                    #region 处理错误的信息显示
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Red;
                    this.listMessage.SelectionFont = norfont;

                    String errMessages = String.Empty;
                    String rCard = String.Empty;

                    string errText = string.Empty; //用于记录错误信息到数据库时使用

                    if (message.Body == string.Empty)
                    {
                        errMessages = message.Exception.Message;
                    }
                    else
                    {
                        errMessages = message.Body;
                    }

                    //int iPosition = errMessages.IndexOf("$CS_Param_ID");
                    //if (iPosition > 0)
                    //{
                    //    if (errMessages.IndexOf(":") >= 0 || errMessages.IndexOf("=") >= 0)
                    //    {
                    //        iPosition = iPosition + 1;
                    //    }
                    //    rCard = errMessages.Substring(iPosition + 12, errMessages.Length - iPosition - 12);
                    //    errMessages = errMessages.Remove(iPosition + 12, errMessages.Length - iPosition - 12);
                    //    //添加非产品序列号的信息
                    //    this.AddWithoutEnter(errMessages);
                    //    errText += errMessages;

                    //    this.listMessage.SelectionColor = Color.Black;
                    //    this.listMessage.SelectionFont = boldFont;
                    //    //获取下一个分隔的位置
                    //    int iNextEmptyPosition = rCard.Trim().IndexOf(" ");
                    //    if (iNextEmptyPosition < 0)
                    //    {
                    //        this.AddText(rCard);
                    //        errText += rCard;
                    //    }
                    //    else
                    //    {
                    //        rCard = rCard.Substring(0, iNextEmptyPosition);
                    //        errMessages = rCard.Remove(0, iNextEmptyPosition);

                    //        this.AddText(rCard);
                    //        errText += rCard;

                    //        this.listMessage.SelectionColor = Color.Red;
                    //        this.listMessage.SelectionFont = norfont;

                    //        if (errMessages != String.Empty)
                    //        {
                    //            this.AddText(errMessages);
                    //            errText += errMessages;
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    this.AddText(errMessages);
                    errText += errMessages;
                    //}

                    try
                    {
                        if (recordWorkingError && WorkingErrorAdded != null)
                        {
                            WorkingErrorAdded(this.Parent, new WorkingErrorAddedEventArgs(function, inputContent, message, MutiLanguages.ParserMessage(errText)));
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                    }
                    //added by leon.li @20130311 声音提示
                    SoundPlayer.PlayErrorMusic();
                    break;

                    #endregion

            }
        }

        public void AddEx(string function, string inputContent, Messages messages, bool recordWorkingError)
        {
            for (int i = 0; i < messages.Count(); i++)
            {
                this.AddEx(function, inputContent, messages.Objects(i), recordWorkingError);
            }
        }

        public int AddEx(string text)
        {
            return this.Add(text);
        }



        #region ---------FOR PDA---------
        public int AddForPDA(string text)
        {
            Font norfont = new Font("宋体", 8F);
            AddWithoutEnter("");
            this.listMessage.SelectionColor = Color.Black;
            this.listMessage.SelectionFont = norfont;

            return AddText(text);
        }

        public void AddForPDA(UserControl.Message message)
        {
            Font norfont = new Font("宋体", 10F);
            Font boldFont = new Font("宋体", 12F, FontStyle.Bold);

            switch (message.Type)
            {
                case MessageType.Normal:
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Black;
                    this.listMessage.SelectionFont = norfont;

                    this.AddText(message.Body);
                    break;
                case MessageType.Success:

                    #region 处理成功的信息显示
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Blue;
                    this.listMessage.SelectionFont = norfont;

                    String sucMessages = String.Empty;
                    String sucCard = String.Empty;

                    if (message.Body == string.Empty)
                    {
                        sucMessages = message.Exception.Message;
                    }
                    else
                    {
                        sucMessages = message.Body;
                    }

                    int iSucPosition = sucMessages.IndexOf("$CS_Param_ID");
                    if (iSucPosition > 0)
                    {
                        if (sucMessages.IndexOf(":") >= 0 || sucMessages.IndexOf("=") >= 0)
                        {
                            iSucPosition = iSucPosition + 1;
                        }
                        sucCard = sucMessages.Substring(iSucPosition + 12, sucMessages.Length - iSucPosition - 12);
                        sucMessages = sucMessages.Remove(iSucPosition + 12, sucMessages.Length - iSucPosition - 12);
                        //添加非产品序列号的信息
                        this.AddWithoutEnter(sucMessages);

                        this.listMessage.SelectionColor = Color.Black;
                        this.listMessage.SelectionFont = boldFont;
                        //获取下一个分隔的位置
                        int iNextEmptyPosition = sucCard.Trim().IndexOf(" ");
                        if (iNextEmptyPosition < 0)
                        {
                            this.AddText(sucCard);
                        }
                        else
                        {
                            sucCard = sucCard.Substring(0, iNextEmptyPosition);
                            sucMessages = sucCard.Remove(0, iNextEmptyPosition);

                            this.AddText(sucCard);

                            this.listMessage.SelectionColor = Color.Blue;
                            this.listMessage.SelectionFont = norfont;

                            if (sucMessages != String.Empty)
                            {
                                this.AddText(sucMessages);
                            }
                        }

                    }
                    else
                    {
                        this.AddText(sucMessages);
                    }
                    //added by leon.li @20130311 声音提示
                    SoundPlayer.PlaySuccessMusic();
                    break;
                    #endregion

                case MessageType.DisplayError:
                case MessageType.Error:

                    #region 处理错误的信息显示
                    listMessage.AppendText("");
                    this.listMessage.SelectionColor = Color.Red;
                    this.listMessage.SelectionFont = norfont;

                    String errMessages = String.Empty;
                    String rCard = String.Empty;

                    if (message.Body == string.Empty)
                    {
                        errMessages = message.Exception.Message;
                    }
                    else
                    {
                        errMessages = message.Body;
                    }

                    int iPosition = errMessages.IndexOf("$CS_Param_ID");
                    if (iPosition > 0)
                    {
                        if (errMessages.IndexOf(":") >= 0 || errMessages.IndexOf("=") >= 0)
                        {
                            iPosition = iPosition + 1;
                        }
                        rCard = errMessages.Substring(iPosition + 12, errMessages.Length - iPosition - 12);
                        errMessages = errMessages.Remove(iPosition + 12, errMessages.Length - iPosition - 12);
                        //添加非产品序列号的信息
                        this.AddWithoutEnter(errMessages);

                        this.listMessage.SelectionColor = Color.Black;
                        this.listMessage.SelectionFont = boldFont;
                        //获取下一个分隔的位置
                        int iNextEmptyPosition = rCard.Trim().IndexOf(" ");
                        if (iNextEmptyPosition < 0)
                        {
                            this.AddWithoutEnter(rCard);
                        }
                        else
                        {
                            rCard = rCard.Substring(0, iNextEmptyPosition);
                            errMessages = rCard.Remove(0, iNextEmptyPosition);

                            this.AddText(rCard);

                            this.listMessage.SelectionColor = Color.Red;
                            this.listMessage.SelectionFont = norfont;

                            if (errMessages != String.Empty)
                            {
                                this.AddWithoutEnter(errMessages);
                            }
                        }


                        //errMessages = errMessages.Replace("","");
                    }
                    else
                    {
                        this.AddText(errMessages);
                    }
#if DEBUG
                    string[] messageStr = message.Debug().Split('。');
                    for (int m = 0; m < messageStr.Length; m++)
                        FileLog.FileLogOut(FileLog.FileName, messageStr[m]);
                    FileLog.FileLogOut(FileLog.FileName, "====漂=亮=的=分=割=线=====================");
#endif
                    //added by leon.li @20130311 声音提示
                    SoundPlayer.PlayErrorMusic();
                    break;
                //					case MessageType.Debug:
                //						this.Add(messages.Objects(i).Body);
                //						break;

                    #endregion

            }
        }

        public void AddForPDA(Messages messages)
        {
            for (int i = 0; i < messages.Count(); i++)
            {
                this.AddForPDA(messages.Objects(i));
            }
        }
        #endregion ---------FOR PDA---------


        public void Clear()
        {
            listMessage.Clear();
        }

        private int iAddedContentCount = 0;
        private void CheckClearContent()
        {
            iAddedContentCount++;
            if (iAddedContentCount < 100)
                return;
            try
            {
                if (listMessage.TextLength > 501)
                {
                    Color prColor = listMessage.SelectionColor;
                    Font prFont = listMessage.SelectionFont;

                    listMessage.Select(0, listMessage.TextLength - 500);
                    listMessage.SelectedText = " ";

                    listMessage.SelectionStart = listMessage.TextLength - 1;
                    listMessage.SelectionColor = prColor;
                    listMessage.SelectionFont = prFont;
                }
                iAddedContentCount = 0;
            }
            catch { }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            int y = (this.Height - btnClear.Height * 4) / 5;
            btnUpdate.Top = y;
            btnClose.Top = btnClear.Height + y * 2;
            btnSave.Top = btnClear.Height * 2 + y * 3;
            btnClear.Top = btnClear.Height * 3 + y * 4;
            base.OnPaint(e);
        }
        private bool _buttonVisible = false;
        [Bindable(true),
        Category("外观")]
        public bool ButtonVisible
        {
            get
            {
                return _buttonVisible;
            }
            set
            {
                btnClear.Visible = value;
                btnClose.Visible = value;
                btnSave.Visible = value;
                btnUpdate.Visible = value;
                if (value)
                    listMessage.Width = this.Width - 120;
                else
                    listMessage.Width = this.Width;
                _buttonVisible = value;
            }
        }

    }
}
