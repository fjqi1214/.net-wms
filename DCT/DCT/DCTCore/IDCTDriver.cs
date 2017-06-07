using System.Threading;
//using BenQGuru.eMES.DataCollect.Action;


namespace BenQGuru.eMES.Common.DCT.Core
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/13</since>
	/// <version>1.0.0</version>
	public interface IDCTDriver 
	{
		event EventCommandHandler AfterLogout;

		IDCTClient DCTClient
		{
			get;
			set;
		}

		void SuperCommand(string com);
	
		void DCTListen(object obj);

		void DCTListen();
//		IAction MesAction
//		{
//			get;
//			set;
//		}
	}
}
