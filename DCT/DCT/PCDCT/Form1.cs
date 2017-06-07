using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Common.DCT.PC
{
    public partial class Form1 : Form
    {
        private SQLDomainDataProvider _DataProvider = null;
        private DCTFacade _DCTFacade = null;
        private string _LocalAddress = string.Empty;
        private int _LocalPort = 0;
        private string _ServerAddress = string.Empty;
        private int _ServerPort = 0;

        public Form1()
        {
            InitializeComponent();

            _DataProvider = (SQLDomainDataProvider)DomainDataProviderManager.DomainDataProvider();
            _DCTFacade = new DCTFacade(_DataProvider);

            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            _LocalAddress = addressList[0].ToString();
            _LocalPort = 12345;

            textBoxServerIP.Text = _LocalAddress;
            _ServerAddress = textBoxServerIP.Text;
            _ServerPort = 12345;

            labelSpeaker.BackColor = Color.LightGray;
            textBoxMessage.Text = " \r\n \r\n ";
        }

        private void textBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendMessage(textBoxInput.Text);
                AddHistory(textBoxInputHistory, textBoxInput.Text);
                textBoxInput.Text = string.Empty;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            object[] list = _DCTFacade.QueryNewDCTMessage(DCTMessageDirection.ServerToClient, _LocalAddress, _LocalPort, 1);

            if (list != null)
            {
                DCTMessage dctMessage = (DCTMessage)list[0];
                dctMessage.Status = DCTMessageStatus.Dealed;
                _DCTFacade.UpdateDCTMessage(dctMessage);

                if (dctMessage != null)
                {
                    if (dctMessage.MessageType == DCTMessageType.Command)
                    {
                        DealCommand(dctMessage.MessageContent);
                        AddHistory(textBoxCommandHistory, dctMessage.MessageContent);
                    }
                    else if (dctMessage.MessageType == DCTMessageType.Message)
                    {
                        AddScreen(dctMessage.MessageContent);
                        AddHistory(textBoxMessageHistory, dctMessage.MessageContent);
                    }
                }
            }
        }

        private void SendMessage(string message)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(_DataProvider);

            DCTMessage dctMessage = _DCTFacade.CreateNewDCTMessage();
            dctMessage.FromAddress = _LocalAddress;
            dctMessage.FromPort = _LocalPort;
            dctMessage.ToAddress = _ServerAddress;
            dctMessage.ToPort = _ServerPort;
            dctMessage.Direction = DCTMessageDirection.ClientToServer;
            dctMessage.MessageType = DCTMessageType.Message;
            dctMessage.MessageContent = message;
            dctMessage.Status = DCTMessageStatus.New;
            dctMessage.MaintainUser = "User";
            dctMessage.MaintainDate = dbDateTime.DBDate;
            dctMessage.MaintainTime = dbDateTime.DBTime;
            this._DCTFacade.AddDCTMessage(dctMessage);
        }

        private void SetOnSpeaker()
        {
            if (checkBox1.Checked)
            {
                timer1.Enabled = false;
                labelSpeaker.BackColor = Color.Red;
                Application.DoEvents();
                System.Threading.Thread.Sleep(300);
                labelSpeaker.BackColor = Color.Green;
                timer1.Enabled = true;
            }
        }

        private void AddHistory(TextBox textBox, string content)
        {
            textBox.Text += content + "\r\n";

            while (textBox.Lines.Length > 20)
            {
                textBox.Text = textBox.Text.Substring(textBox.Text.IndexOf("\r\n") + 2);
            }
        }

        private void AddScreen(string content)
        {
            string message = textBoxMessage.Text;

            string temp = content;
            int count = 0;
            for (int i = 0; i < content.Length; i++)
            {
                count += Encoding.GetEncoding("gb2312").GetByteCount(content.Substring(i, 1));
                if (count > 30)
                {
                    //message += "\r\n" + temp.Substring(0, count);
                    //temp = temp.Substring(count);
                    //count = Encoding.GetEncoding("gb2312").GetByteCount(content.Substring(i, 1));

                    message += "\r\n" + temp.Substring(0, i+1);
                    temp = temp.Substring(i+1);
                    count = 0;
                }
            }

            if (temp.Length > 0)
            {
                if (message.Length > 0)
                {
                    message += "\r\n";
                }
                message += temp;
            }

            textBoxMessage.Text = message;

            while (textBoxMessage.Lines.Length > 3)
            {
                textBoxMessage.Text = textBoxMessage.Text.Substring(textBoxMessage.Text.IndexOf("\r\n") + 2);
            }
        }

        private void DealCommand(string command)
        {
            if (command == DCTCommand.ClearText.ToString())
            {
                textBoxMessage.Text = " \r\n \r\n ";
            }
            else if (command == DCTCommand.ClearGraphic.ToString())
            {
                textBoxMessage.Text = " \r\n \r\n ";
            }
            else if (command == DCTCommand.SpeakerOn.ToString())
            {
                SetOnSpeaker();
            }
            else if (command == DCTCommand.SpeakerOff.ToString())
            {
                SetOnSpeaker();
            }
        }

        private void textBoxServerIP_TextChanged(object sender, EventArgs e)
        {
            _ServerAddress = textBoxServerIP.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                labelSpeaker.BackColor = Color.Green;
            }
            else
            {
                labelSpeaker.BackColor = Color.LightGray;
            }
        }
    }
}