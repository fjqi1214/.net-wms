namespace BenQGuru.eMES.Common.DCT.Core
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/13</since>
	/// <version>1.0.0</version>
	public interface IMessageParse
	{
		byte[] RetrieveData(byte[] btMsg);

		byte[] WrapCommand(byte[] srcCom);

		byte[] WrapMessage(byte[] srcMsg);
	}
}
