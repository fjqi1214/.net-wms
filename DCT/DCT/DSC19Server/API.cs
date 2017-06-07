
using System.Runtime.InteropServices;

namespace  BenQGuru.eMES.Common.DCT.ATop.DSC19
{
	
	public class API2
	{
		//API打开
		[DllImport( "dapapi2.dll")]
		public static extern short AB_API_Open() ;
		//API关闭
		[DllImport( "dapapi2.dll")]
		public static extern short AB_API_Close() ;
		//GateWay统计
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_Cnt() ;
		//GateWay打开
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_Open( int Gateway_ID) ;
		//GateWay关闭
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_Close( int Gateway_ID) ;
		//GateWay配置
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_Conf( int ndx, out int Gateway_ID, out byte ip, out int port) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_Ndx2ID( int ndx) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_ID2Ndx( int Gateway_ID) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_InsConf( int Gateway_ID,  ref byte ip, int port) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_UpdConf( int Gateway_ID,  ref byte ip, ref int port) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_DelConf( int Gateway_ID) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_RcvMsg( int Gateway_ID, ref byte ccb) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_SndMsg( int Gateway_ID, ref byte ccb) ;

		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_Status( int Gateway_ID) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_AllStatus( ref byte status) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_SetPollRang( int Gateway_ID, int port, int poll_range) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_GW_TagDiag( int Gateway_ID, int port) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_Tag_RcvMsg( ref int tag_addr, ref short subcmd, ref short msg_type, ref byte data, ref short data_cnt) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_Tag_Reset( int tag_addr) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_Tag_ChgAddr( int tag_addr, int new_tag) ;

		//---DCS19 API		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_InputMode( int tag_addr, byte input_mode) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_BufSize(int tag_addr, byte buf_size) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_SetConf(int tag_addr, byte enable_status, byte disable_status) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_ReqConf(int tag_addr) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_GetVer(int tag_addr) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_Reset(int tag_addr) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_SetRows(int tag_addr, byte rows) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_SimulateKey(int tag_addr, byte key_code) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_Cls(int tag_addr) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_Buzzer(int tag_addr, byte alarm_time, byte alarm_cnt) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_ScrollUp(int tag_addr, byte up_rows) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_ScrollDown(int tag_addr, byte down_rows) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_ScrollHome(int tag_addr) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_ScrollEnd(int tag_addr) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_SetCursor(int tag_addr, byte row, byte column) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_DspStrE(int tag_addr, ref byte dsp_str, int dsp_cnt) ;
		
		[DllImport( "dapapi2.dll")]
		public static extern short AB_DCS_DspStrC(int tag_addr, ref byte dsp_str, int dsp_cnt) ;

		public struct GwPortStatus
		{
			short enable_flag ;			//enable flag
			short max_node ;			//max polling node
			string subnode_status ;		//all subnode status string
			short timeout_flag ;		//timeout flag
			short wait_flag ;			//wait diagnosis response
		} ;

		public struct GwConfType
		{
			public short gw_id ;
			public string ip ;
			public short port ;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=2)]
			public GwPortStatus[] port1 ;
			public short reconnect_cnt ;
			public short status ;
        } ;

	}
}
