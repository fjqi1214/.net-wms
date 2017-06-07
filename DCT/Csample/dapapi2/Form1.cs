using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;

namespace WindowsApplication1
{
	/// <summary>
	/// Form1 的摘要描述。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button btnRecv;
		private System.Windows.Forms.ListBox lstMsg;
		private System.Windows.Forms.ListBox lstGwStatus;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Windows Form 設計工具支援的必要項
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 呼叫之後加入任何建構函式程式碼
			//
		}

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			int		ret ;

			System.Windows.Forms.MessageBox.Show( "Close Form") ;

			timer1.Enabled = false ;
			ret = API2.AB_GW_Close( 0) ;
			ret = API2.AB_API_Close() ;
			if (ret < 0)
			{
				MessageBox.Show( "AB_API_Close= err") ; // + AB_ErrMsg(ret)) ;
			}

			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// 此為設計工具支援所必需的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.btnRecv = new System.Windows.Forms.Button();
			this.lstMsg = new System.Windows.Forms.ListBox();
			this.lstGwStatus = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(221, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(86, 30);
			this.button1.TabIndex = 0;
			this.button1.Text = "Diagnosis";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3,
																					  this.menuItem4,
																					  this.menuItem7,
																					  this.menuItem5,
																					  this.menuItem6});
			this.menuItem1.Text = "Gateway";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "Gateway Open";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "Gateway Close";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.Text = "-";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "Tag Diagnosis";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "Set Polling Range";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem8,
																					  this.menuItem9});
			this.menuItem2.Text = "DCS19";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 0;
			this.menuItem8.Text = "Display English";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 1;
			this.menuItem9.Text = "Display Chinese";
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// btnRecv
			// 
			this.btnRecv.Location = new System.Drawing.Point(106, 0);
			this.btnRecv.Name = "btnRecv";
			this.btnRecv.Size = new System.Drawing.Size(76, 30);
			this.btnRecv.TabIndex = 2;
			this.btnRecv.Text = "Recv";
			this.btnRecv.Click += new System.EventHandler(this.btnRecv_Click);
			// 
			// lstMsg
			// 
			this.lstMsg.ItemHeight = 12;
			this.lstMsg.Location = new System.Drawing.Point(10, 168);
			this.lstMsg.Name = "lstMsg";
			this.lstMsg.Size = new System.Drawing.Size(547, 172);
			this.lstMsg.TabIndex = 3;
			this.lstMsg.DoubleClick += new System.EventHandler(this.lstMsg_DoubleClick);
			// 
			// lstGwStatus
			// 
			this.lstGwStatus.ItemHeight = 12;
			this.lstGwStatus.Location = new System.Drawing.Point(10, 45);
			this.lstGwStatus.Name = "lstGwStatus";
			this.lstGwStatus.Size = new System.Drawing.Size(547, 100);
			this.lstGwStatus.TabIndex = 4;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(566, 377);
			this.Controls.Add(this.lstGwStatus);
			this.Controls.Add(this.lstMsg);
			this.Controls.Add(this.btnRecv);
			this.Controls.Add(this.button1);
			this.Menu = this.mainMenu1;
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 應用程式的主進入點。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			button1.Text = "hello" ;
			int		ret, node ;

			node = 22 ;
			ret = API2.AB_GW_TagDiag( node, 0) ;
			if (ret < 0)
			{
				MessageBox.Show( "AB_GW_TagDiag() err") ;
				return ;
			}
			else
			{
				MessageBox.Show( "AB_GW_TagDiag() ok, node=" + Convert.ToString(node)) ;
			}
			
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			int		ret, node ;
			string	tmpstr ;

			node = 22 ;
			ret = API2.AB_GW_Open(node) ;
			if (ret < 0)
			{
				MessageBox.Show( "AB_GW_Open() err") ;
				return ;
			}
			else
			{
				tmpstr = "AB_GW_Open() ok, node=" + Convert.ToString(node) ;
				MessageBox.Show( tmpstr) ;
				MsgBox2( tmpstr) ;
			}
		}

		private System.Threading.Timer timer = null;
		private void Form1_Load(object sender, System.EventArgs e)
		{
			int	   i, ret ;
			string tmpstr ;

			//			System.Windows.Forms.MessageBox.Show( "testing") ;    
			tmpstr = System.Reflection.Assembly.GetExecutingAssembly().Location ;
			System.Windows.Forms.MessageBox.Show( "Working Area=" + tmpstr) ;

			ret = API2.AB_API_Open() ;
			if (ret < 0)
			{
				MessageBox.Show( "AB_API_Open= err") ; // + AB_ErrMsg(ret)) ;
			}
			else
			{
				timer1.Enabled = true ;
				ret = AB_LoadConf() ;
				if (ret>0)
				for (i=0; i<ret; i++)
				{
					lstGwStatus.Items.Add("Gateway" + Convert.ToString(i + 1) + " not open") ;
					ret = API2.AB_GW_Open( GwConf[i].gw_id) ;
				}
			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			int		ret, node ;

			node = 22 ;
			ret = API2.AB_GW_Close(node) ;
			if (ret < 0)
			{
				MessageBox.Show( "AB_GW_Close() err") ;
				return ;
			}
			else
			{
				MessageBox.Show( "AB_GW_Close() ok, node=" + Convert.ToString(node)) ;
			}
		}

		protected static int loopcnt = 0 ;
		private void timer1_Tick(object sender, System.EventArgs e)
		{
			int		i, cnt, ret ;

			btnRecv_Click( sender, e) ;
			loopcnt++ ;
			if ( loopcnt<50) return ;

			loopcnt = 0 ;
			cnt = API2.AB_GW_Cnt() ;
			for (i=0; i<cnt; i++)
			{
				ret = API2.AB_GW_Status( GwConf[i].gw_id) ;
				lstGwStatus.Items[ i] = "Gateway index:" + Convert.ToString(i + 1) + " " + AB_ErrMsg(ret) ;
			}
		}

		private void btnRecv_Click(object sender, System.EventArgs e)
		{
			int		ret, i, k ;
			int		tag_addr ;
			short	msg_type, subcmd, data_cnt ;
			byte [] data = new byte [512] ;
			byte	ch ;
			string	tmpstr ;
		
//			MessageBox.Show( "btnRecv_Click ") ; // + AB_ErrMsg(ret)) ;		

			for (k=1; k<30; k++)
			{
				tag_addr = 0 ;
				subcmd = -1 ;
				msg_type = -1 ;
				data_cnt = 512 ;
				ret = API2.AB_Tag_RcvMsg(ref tag_addr, ref subcmd, ref msg_type, ref data[0], ref data_cnt) ;

				if (ret <= 0)	//no data
					break ;
				else
				{
					if (subcmd == 9)	//---diagnosis status
					{
						tmpstr = ldump3( data, 0, data_cnt) ;
						tmpstr = String.Format( "Diagnosis, Node={0}, port={1}, cnt={2}, data={3}", tag_addr>>16, (tag_addr>>8) & 0xFF, msg_type, tmpstr) ;
						MsgBox2( tmpstr) ;
//						ret = ChkStatus2(tag_addr, msg_type, data) ;
					}
					else				//---DCS-19 data
					if (subcmd == 0x37)
					{
						if (msg_type==0x30)
						{
							if (data[0] == 0)	//Function key
								tmpstr = String.Format( "Press F{0}", data[1] - 0x3B + 1) ;
							else
								tmpstr = String.Format( "Press {0}", data[1]) ;
						}
						else
						if (msg_type == 0x31 || msg_type==0x41)	//keyboard input string
						{
							//---remove "\r\n"
							for (i=1; i<=2; i++)
							{
								if (data_cnt > 0)
								{
									ch = data[data_cnt - 1] ;
									if (ch==13 || ch==10)
										data_cnt -= 1 ;
									else break ;
								}
							}

							tmpstr = System.Text.Encoding.ASCII.GetString( data, 0, data_cnt) ;

							//---for English output
							byte [] tmpbytes = System.Text.Encoding.ASCII.GetBytes( "Keyin:" + tmpstr + "\r\n") ;
							API2.AB_DCS_DspStrE( tag_addr, ref tmpbytes[0], tmpbytes.Length) ;

							//---for Chinese output
							byte [] tmpbytes2 = System.Text.Encoding.Default.GetBytes( "Output:" + tmpstr + "\r\n") ;
							API2.AB_DCS_DspStrC( tag_addr, ref tmpbytes2[0], tmpbytes2.Length) ;
						}
						else					//other message
						{
							tmpstr = ldump3( data, 0, data_cnt) ;
							tmpstr = String.Format( "Unknown data, msg_type={0}, data={1}", msg_type, tmpstr) ;
						}

						tmpstr = String.Format( "Get data, Node={0}, port={1}, addr={2}, data={3}", tag_addr>>16, (tag_addr>>8) & 0xFF, tag_addr & 0xFF, tmpstr) ;
						MsgBox2( tmpstr) ;
					}
					else			//---unknown data
					{
						tmpstr = ldump3( data, 0, data_cnt) ;
						tmpstr = String.Format( "Unknown data, Node={0}, port={1}, msg_type={2}, data={3}", tag_addr>>16, (tag_addr>>8) & 0xFF, msg_type, tmpstr) ;
						MsgBox2( tmpstr) ;
					}
				} // if ret>0
			} //for k
		}

		public void MsgBox2( string msg)
		{
			ListBox lst ;
			string timestr ;
		
			lst = lstMsg ;
			timestr = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") ;
		
		    if ( lst.Items.Count >= 100)
				lst.Items.RemoveAt(0) ;
			lst.Items.Add( timestr + " " + msg) ;
			lst.SelectedIndex = lst.Items.Count - 1 ;
		}

		public string ldump3( byte [] buf, int start,  int cnt)
		{
			int		i ;
			string	tmpstr, op_str ;

			op_str = "" ;
			for (i=0; i<cnt; i++)
			{
				tmpstr = Convert.ToString(buf[ start + i],16) + " " ;
				if (tmpstr.Length < 3)
				   tmpstr = "0" + tmpstr ;
				op_str = op_str + tmpstr ;
			}
            return( op_str) ;
		}

		private void lstMsg_DoubleClick(object sender, System.EventArgs e)
		{
			string tmpstr ;

			tmpstr = lstMsg.Items[ lstMsg.SelectedIndex].ToString() ;
			MessageBox.Show( tmpstr) ;		
		}

		public short GwCnt ;
		public API2.GwConfType[]	GwConf = new API2.GwConfType[100] ;


		public string AB_ErrMsg( int ret)
		{
			string	tmpstr ;

			switch(ret)
			{
					//				case -3:
					//					tmpstr = "Parameter data is error !" ;
					//					break ;
				case -2:
					tmpstr = "TCP is not created yet !" ;
					break ;
				case -1:
					tmpstr = "DAP_ID out of range !" ;
					break ;
				case 0:
					tmpstr = "Closed" ;
					break ;
				case 1:
					tmpstr = "Open" ;
					break ;
				case 2:
					tmpstr = "Listening" ;
					break ;
				case 3:
					tmpstr = "Connection is Pending" ;
					break ;
				case 4:
					tmpstr = "Resolving the host name" ;
					break ;
				case 5:
					tmpstr = "Host is Resolved" ;
					break ;
				case 6:
					tmpstr = "Waiting to Connect" ;
					break ;
				case 7:
					tmpstr = "Connected ok " ;
					break ;
				case 8:
					tmpstr = "Connection is closing" ;
					break ;
				case 9:
					tmpstr = "State error has occurred" ;
					break ;
				case 10:
					tmpstr = "Connection state is undetermined"	;
					break ;
				default:
					tmpstr = "Unknown Error Code" ;
					break ;
			}
			return( tmpstr) ;
		}

		public short AB_LoadConf()
		{
			string	tmpstr ;
			short	ret ;
			short	i, j ;
			int		id, port ;
			byte [] ip = new byte[20] ;

			GwCnt = API2.AB_GW_Cnt() ;
			if (GwCnt == 0) return( 0) ;

			for (i=0; i<GwCnt; i++)
			{
				ret = API2.AB_GW_Conf( i, out id, out ip[0], out port) ;
				if (ret >= 0)
				{
					GwConf[i].gw_id = (short)id ;
					GwConf[i].port = (short)port ;

					for (j=0; j<20; j++)
						if (ip[j] == 0) break ;

					tmpstr = System.Text.Encoding.ASCII.GetString( ip, 0, j) ;
					GwConf[ i].ip = tmpstr ;
				}
				else
				{
					GwConf[ i].gw_id = 0 ;
					GwConf[ i].port = 0 ;
					GwConf[ i].ip = "" ;
				}
			}
			return( GwCnt) ;
		}
	}
}
