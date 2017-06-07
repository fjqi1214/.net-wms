using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.Security
{
	/// <summary>
	/// ModuleClickStatisical 的摘要说明。
	/// </summary>
	public class WebStatisical 
	{
		private static StatisicalAccount s_account = new StatisicalAccount();
		private static WebStatisical _stat = null;
		private static ArrayList s_module_filter = new ArrayList();
		private static Hashtable _itemTable = new Hashtable();

		/// <summary>
		/// sammer kong 
		/// singleton pattern,the application share only one
		/// </summary>
		/// <returns></returns>
		public static WebStatisical Instance()
		{
			if( WebStatisical._stat == null )
			{
				WebStatisical._stat = new WebStatisical();
			}
			return WebStatisical._stat;
		}

		public void AddStatisicalItem(string itemCode)
		{
			if( !WebStatisical._itemTable.ContainsKey(itemCode.ToUpper()) )
			{
				WebStatisical._itemTable.Add( itemCode.ToUpper(),new StatisicalAccount());
			}
		}

		public void RemoveStatisicalItem(string itemCode)
		{
			if( WebStatisical._itemTable.ContainsKey(itemCode.ToUpper()) )
			{
				WebStatisical._itemTable.Remove( itemCode.ToUpper() );
			}
		}

		public StatisicalAccount this[ string itemCode]
		{
			get
			{
				if( WebStatisical._itemTable.ContainsKey(itemCode.ToUpper()) )
				{
					return WebStatisical._itemTable[itemCode.ToUpper()] as StatisicalAccount;
				}
				return null;
			}
		}
	}

	/// <summary>
	/// 纪录统计数量，可以不仅仅用于模块统计
	/// </summary>
	public class StatisicalAccount
	{
		private Hashtable _table = new Hashtable();

		public StatisicalAccount()
		{
		}

		/// <summary>
		/// 给该模块统计数+1
		/// 1。如果已经存在，就直接加
		/// 2。如果模块第一次，就先在hashtable中加入这个模块的key，然后再加1
		/// </summary>
		/// <param name="moduleid"></param>
		public void Add(object statItem)
		{
			if( this._isItemExist( statItem ) )
			{
				this._table[statItem ] = (int)this._table[statItem] + 1;
			}
			else
			{
				this._table.Add(statItem,1);
			}
		}

		/// <summary>
		/// 统计数-1
		/// </summary>
		/// <param name="moduleid"></param>
		public void Delete(object statItem)
		{
			if( this._table.ContainsKey(statItem) )
			{
				this._table[statItem] = ( (int)this._table[statItem] ) - 1;
			}
		}

		/// <summary>
		/// 该模块的现有统计数
		/// </summary>
		/// <param name="moduleid"></param>
		/// <returns></returns>
		public int GetItemAccount(object statItem)
		{
			int account = 0;
			foreach(object key in this._table.Keys)
			{
				if( key.Equals( statItem ) )
				{	
					account = (int)this._table[statItem];
					break;
				}
			}
			return account;
		}

		public int GetAllCount()
		{
			int account = 0;
			foreach(object item in this._table.Keys)
			{
				account += this.GetItemAccount( item );
			}
			return account;
		}

		private bool _isItemExist(object statItem)
		{
			bool isIn = false;
			foreach(object key in this._table.Keys)
			{
				if( key.Equals( statItem ) )
				{
					isIn = true;
					break;
				}
			}
			return isIn;
		}
	}

	/// <summary>
	/// 这个类存储每个客户端，针对每个page请求的时候的响应时间
	/// 存储结构如下
	///	cookieID1
	///		url1
	///			request1
	///			response1
	///			request2
	///			response2
	///		url2
	///			request1
	///			response1
	///	cookidID2
	/// </summary>
	public class ResponseStatisical
	{
		private static ResponseStatisical s_state = null;
		private Hashtable _table = new Hashtable();

		public static ResponseStatisical Instance()
		{
			if( ResponseStatisical.s_state == null )
			{
				ResponseStatisical.s_state = new ResponseStatisical();
			}
			return ResponseStatisical.s_state;
		}

		/// <summary>
		/// embbed class
		/// record content:
		///		1.request type : begin or end,just for distinguish
		///		2.request time : current time as usual
		/// </summary>
		private class _requestInfo
		{
			protected DateTime _time ;
			public DateTime Time
			{
				get
				{
					return this._time;
				}
			}

			public _requestInfo(DateTime time)
			{
				this._time = time;
			}
		}

		/// <summary>
		/// inherited
		///		begin request type 
		/// </summary>
		private class _beginRequestInfo : _requestInfo
		{
			public _beginRequestInfo(DateTime time) : base(time)
			{
			}

			public override string ToString()
			{
				return "Begin Request : " + this._time.ToLongTimeString();
			}

		}

		/// <summary>
		/// inherited
		///		end request type 
		/// </summary>
		private class _endRequestInfo : _requestInfo
		{
			public _endRequestInfo(DateTime time) : base(time)
			{
			}

			public override string ToString()
			{
				return "End Request   : " + this._time.ToLongTimeString();
			}
		}

		private void _recordBeginTime(string cookieID,string module)
		{			
			//record begin type of request info
			_beginRequestInfo now = new _beginRequestInfo( System.DateTime.Now );
			//temp variable
			ArrayList list = null;
			if( this._table.ContainsKey( cookieID ) )	//id exists
			{				
				if( (this._table[ cookieID ] as Hashtable).ContainsKey( module ) )	//the url has been recored 
				{					
					list = ( (this._table[ cookieID ] as Hashtable)[module] as ArrayList );
					if( list[list.Count-1] is _beginRequestInfo )	
						//if the last record is begin type request without enclosed end type request
						//it means the request has been canceled,eg.button clicked two times
					{
						//clear this record,set one end type request become its last record
						list.Remove( list[list.Count-1] );	
					}
					//type right,add as usual
					list.Add( now );
				}
				else	//this url met first time,simply added
				{
					list = new ArrayList();
					list.Add( now );

					(this._table[ cookieID ] as Hashtable).Add( module,list );
				}
			}
			else	//client connected to server first time,cookie initialed OK
			{
				Hashtable table = new Hashtable();
				list = new ArrayList();

				list.Add( now );	//the first begin type request record
				table.Add(module,list);	//attach this record to the url
				this._table.Add( cookieID,table );	//attach to HttpApplication 's record
			}
		}

		private void _recordEndTime(string cookieID,string module)
		{
			if( this._table.ContainsKey( cookieID ) )	//client connected already
			{
				if( ( this._table[cookieID] as Hashtable ).ContainsKey( module ) )	//url exists
				{
					ArrayList list = (this._table[cookieID] as Hashtable )[ module ] as ArrayList;
					if( list[list.Count-1] is _beginRequestInfo )	//the last request record is begin type
					{
						//add end type request record makes it enclosed
						list.Add( new _endRequestInfo(System.DateTime.Now) );
					}
				}
			}
		}
		
		/// <summary>
		/// record begin request info
		/// </summary>
		/// <param name="application"></param>
		public void SetBeginRequestTime(HttpApplication application)
		{			
			this._recordBeginTime(
				this._getCookieID(application.Request),
				this._getUrl( application ) );
		}

		public void SetBeginRequestTime(Page page,string module)
		{
			this._recordBeginTime(
				this._getCookieID(page.Request),
				module);
		}


		public void SetEndRequestTime(HttpApplication application)
		{
			this._recordBeginTime(
				this._getCookieID(application.Request),
				this._getUrl( application ) );
		}

		public void SetEndRequestTime(Page page,string module)
		{
			this._recordEndTime(
				this._getCookieID(page.Request),
				module);
		}

		/// <summary>
		/// record to the local disk
		/// </summary>
		public void Write()
		{
			Log.Info( this.ToString() );


//			FileStream file = new FileStream(@"c:\\log.txt",FileMode.Append);					
//			StreamWriter writer = new StreamWriter(file);	
//			writer.WriteLine( "\n " + this.ToString() );
//			writer.Close();
//
			this._table.Clear();
		}

		/// <summary>
		/// format recocded info
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{			
			//for read friendlly
			string padding = "\t";
			string text = "";
			foreach(string cookieID in this._table.Keys)	// cookieID as top level
			{
				text += cookieID.ToString() + "\n";
				//each url under cookie,the 2nd level
				foreach(string url in (this._table[cookieID] as Hashtable).Keys)	
				{
					ArrayList list = (this._table[cookieID] as Hashtable)[url] as ArrayList;
					
					if( list[list.Count-1] is _beginRequestInfo )	//if last record is begin type request
					{
						//clear it,for it always happened for log to local 
						list.Remove( list[list.Count-1] );
					}

					//request list for each url
					text += padding + url.ToString() + "\n";
					foreach(_requestInfo info in list)
					{
						text += padding + padding + info.ToString() + "\n";						
					}
				}
			}

			return text;
		}


		private string _getUrl(HttpApplication application)
		{
			return application.Request.RawUrl;
		}

		/// <summary>
		/// cookie ID formatted to : ASP.NET_SessionId + Client IP address
		/// </summary>
		/// <param name="application"></param>
		/// <returns></returns>
		private string _getCookieID(HttpRequest request)
		{
			if( request.Cookies["ASP.NET_SessionId"] == null ||
				request.Cookies["ASP.NET_SessionId"].Value == "" )	//the first connected to server
			{
				return "No Cookie";
			}
			else
			{
				string cookieID = request.Cookies["ASP.NET_SessionId"].Value.ToString().ToUpper();
				string clientIPAddress = request.UserHostAddress;

				return string.Format("CookieID : {0}  IP : {1}",cookieID,clientIPAddress);
			}
		}
	}
}
