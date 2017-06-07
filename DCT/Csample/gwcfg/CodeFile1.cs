
using System.Runtime.InteropServices;

namespace gwcfg
{
	
	public class CFGAPI
	{
		[StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi)]
		public struct Tdata
		{
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=16)]
			public byte[] sIP ;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=18)]
			public byte[] sMac ;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=16)]
			public byte[] sGateway ;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=16)]
			public byte[] sMask ;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=20)]
			public byte[] sModel ;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=6)]
			public byte[] sVer ;
			[MarshalAs( UnmanagedType.ByValArray, SizeConst=128)]
			public byte[] sFirmware ;
		} 

		[DllImport( "gwcfg.dll")]
		public static extern void CreateWinSock() ;

		[DllImport( "gwcfg.dll")]
		public static extern void FreeWinSock() ;

		[DllImport( "gwcfg.dll")]
		public static extern void GWBroadCast( 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sIP, short iFilter) ;

		[DllImport( "gwcfg.dll")]
		public static extern bool GWGetData( short iIndex, out byte CFGdata) ;

		[DllImport( "gwcfg.dll")]
		public static extern short GWGetReplyNum() ;

		[DllImport( "gwcfg.dll")]
		public static extern void GWLocate( 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sIP, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sMac) ;

		[DllImport( "gwcfg.dll")]
		public static extern void GWReset( 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sIP, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sMac, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sID, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sPass) ;

		[DllImport( "gwcfg.dll")]
		public static extern bool GWConfig( 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sIP, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sMac, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sID, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sPass, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sNewIP, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sNewGateway, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string sNewMask) ;

		//---new function
		[DllImport( "gwcfg.dll")]
		public static extern void DefaultAck( bool bDefault) ;

		[DllImport( "gwcfg.dll")]
		public static extern void GetAck( 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			string ip_filter, 
			[MarshalAs( UnmanagedType.AnsiBStr)]
			out string sRetIP) ;

		[DllImport( "gwcfg.dll")]
		public static extern void ClearAckBuffer() ;
	}
}		
