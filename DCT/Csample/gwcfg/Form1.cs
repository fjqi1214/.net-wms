using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace gwcfg
{
	/// <summary>
	/// Form1 的摘要描述。
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCreateSocket;
		private System.Windows.Forms.Button btnBroadcast;
		private System.Windows.Forms.Button btnGetData;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ListBox lstRcvDev;
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.Container components = null;

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
			this.btnCreateSocket = new System.Windows.Forms.Button();
			this.btnBroadcast = new System.Windows.Forms.Button();
			this.btnGetData = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.lstRcvDev = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// btnCreateSocket
			// 
			this.btnCreateSocket.Location = new System.Drawing.Point(8, 24);
			this.btnCreateSocket.Name = "btnCreateSocket";
			this.btnCreateSocket.Size = new System.Drawing.Size(96, 24);
			this.btnCreateSocket.TabIndex = 0;
			this.btnCreateSocket.Text = "step1. create socket";
			this.btnCreateSocket.Click += new System.EventHandler(this.btnCreateSocket_Click);
			// 
			// btnBroadcast
			// 
			this.btnBroadcast.Location = new System.Drawing.Point(104, 24);
			this.btnBroadcast.Name = "btnBroadcast";
			this.btnBroadcast.Size = new System.Drawing.Size(96, 24);
			this.btnBroadcast.TabIndex = 1;
			this.btnBroadcast.Text = "step2. broadcast";
			this.btnBroadcast.Click += new System.EventHandler(this.btnBroadcast_Click);
			// 
			// btnGetData
			// 
			this.btnGetData.Location = new System.Drawing.Point(200, 24);
			this.btnGetData.Name = "btnGetData";
			this.btnGetData.Size = new System.Drawing.Size(88, 24);
			this.btnGetData.TabIndex = 2;
			this.btnGetData.Text = "step3. get data";
			this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(288, 24);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(72, 24);
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "step4. close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lstRcvDev
			// 
			this.lstRcvDev.ItemHeight = 12;
			this.lstRcvDev.Location = new System.Drawing.Point(8, 72);
			this.lstRcvDev.Name = "lstRcvDev";
			this.lstRcvDev.Size = new System.Drawing.Size(352, 160);
			this.lstRcvDev.TabIndex = 4;
			this.lstRcvDev.DoubleClick += new System.EventHandler(this.lstRcvDev_DoubleClick);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
			this.ClientSize = new System.Drawing.Size(368, 273);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lstRcvDev,
																		  this.btnClose,
																		  this.btnGetData,
																		  this.btnBroadcast,
																		  this.btnCreateSocket});
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

		private void Form1_Load(object sender, System.EventArgs e)
		{
		
		}

		private void btnCreateSocket_Click(object sender, System.EventArgs e)
		{
			CFGAPI.CreateWinSock() ;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			CFGAPI.FreeWinSock() ;
		}

		private void btnBroadcast_Click(object sender, System.EventArgs e)
		{
			short	iFilter ;
			string	tmpstr ;
/*
		' iFilter=0 then broadcast alldevice
		' iFilter=1  Audio     keyword('LanMP3')
		' iFilter=2  Vidio     keyword('DVC','DVS')
		' iFilter=3  Access    keyword('ACS','Access')
		' iFilter=4  Picking   keyword('Picking','CAPS')
		' iFilter=5  Other     keyword not ('LanMP3','DVC','ACS','Access','Picking','CAPS')
*/
			iFilter = 0 ;
			tmpstr = "255.255.255.255" ;
			try
			{
				CFGAPI.GWBroadCast( tmpstr, iFilter) ;
			}
			catch
			{
			}
		}

		private void btnGetData_Click(object sender, System.EventArgs e)
		{
			short	replynum, i ;
			string	tmpstr ;
			int		ndx, len ;
			byte [] buf = new byte[300] ;

			lstRcvDev.Items.Clear() ;
/*			
			CFGAPI.Tdata	CFGdata = new CFGAPI.Tdata() ;
			CFGdata.sIP = new System.Byte [16] ;
			CFGdata.sMac = new System.Byte [18] ;
			CFGdata.sMask = new System.Byte [16] ;
			CFGdata.sGateway = new System.Byte [16] ;
			CFGdata.sModel = new System.Byte [20] ;
			CFGdata.sVer = new System.Byte [6] ;
			CFGdata.sFirmware = new System.Byte [128] ;
*/
			replynum = CFGAPI.GWGetReplyNum() ;
			for (i=0; i<replynum; i++)
			{
				if ( CFGAPI.GWGetData(i, out buf[0]))
//				if ( CFGAPI.GWGetData(i, ref CFGdata.sIP[0]))
					{
/*
					tmpstr = GetString2( CFGdata.sIP, 0, CFGdata.sIP.Length) + "\t" ;
					tmpstr = tmpstr + GetString2( CFGdata.sMac, 0, CFGdata.sMac.Length) + "\t" ;
					tmpstr = tmpstr + GetString2( CFGdata.sMask, 0, CFGdata.sMask.Length) + "\t" ;
					tmpstr = tmpstr + GetString2( CFGdata.sGateway, 0, CFGdata.sGateway.Length) + "\t" ;
					tmpstr = tmpstr + GetString2( CFGdata.sModel, 0, CFGdata.sGateway.Length) + "\t" ;
					tmpstr = tmpstr + GetString2( CFGdata.sVer, 0, CFGdata.sVer.Length) + "\t" ;
					tmpstr = tmpstr + GetString2( CFGdata.sFirmware, 0, CFGdata.sFirmware.Length) ;
*/					
					ndx = 0 ; len = 16 ;
					tmpstr = GetString2( buf, ndx, len) + "\t" ;
					ndx += len ; len = 18 ;
					tmpstr = tmpstr + GetString2( buf, ndx, len) + "\t" ;
					ndx += len ; len = 16 ;
					tmpstr = tmpstr + GetString2( buf, ndx, len) + "\t" ;
					ndx += len ; len = 16 ;
					tmpstr = tmpstr + GetString2( buf, ndx, len) + "\t" ;
					ndx += len ; len = 20 ;
					tmpstr = tmpstr + GetString2( buf, ndx, len) + "\t" ;
					ndx += len ; len = 6 ;
					tmpstr = tmpstr + GetString2( buf, ndx, len) + "\t" ;
					ndx += len ; len = 128 ;
					tmpstr = tmpstr + GetString2( buf, ndx, len) ;

					lstRcvDev.Items.Add(tmpstr) ;
				}
			}
		}

		private string GetString2( byte[] buf, int start, int max_cnt)
		{
			int		j ;
			string	tmpstr ;

			for (j=0; j<max_cnt; j++)
				if (buf[ start+j] == 0) break ;

			tmpstr = System.Text.Encoding.ASCII.GetString( buf, start, j) ;
			return( tmpstr) ;
		}

		private void lstRcvDev_DoubleClick(object sender, System.EventArgs e)
		{
			string tmpstr ;

			tmpstr = lstRcvDev.Items[ lstRcvDev.SelectedIndex].ToString() ;
			MessageBox.Show( tmpstr) ;		
		}
	}
}
