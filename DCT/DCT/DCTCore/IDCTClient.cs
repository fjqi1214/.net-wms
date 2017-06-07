namespace BenQGuru.eMES.Common.DCT.Core
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/13</since>
	/// <version>1.0.0</version>
	public interface IDCTClient
	{

		event EventCommandHandler OnSendData;

		event EventCommandHandler OnError;

		event EventCommandHandler AfterLogin;


		DCTType Type
		{
			get;
			//set;
		}

		int ClientPort
		{
			get;
			set;
		}

		string ClientAddress
		{
			get;
			set;
		}

		int ClientID
		{
			get;
			set;
		}

		string ClientDesc
		{
			get;
			set;
		}

		bool Authorized
		{
			get;
			set;
		}

		string LoginedUser
		{
			get;
			set;
		}
		
		string LoginedPassword
		{
			get;
			set;
		}
	
		string ResourceCode
		{
			get;
			set;
		}

		string SendMessage(string msg);

		void Open();

		void Close();
	}
}
