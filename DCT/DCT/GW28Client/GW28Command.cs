using System;
using System.Collections;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.GW28
{
	
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class GW28CommandList:DCTCommandList
	{
//		protected Hashtable gw28CommandList 
//			= new Hashtable();


		public GW28CommandList()
		{
		}

		public override void InitialCommand()
		{
			dctCommandList.Add(DCTCommand.Reset,(new byte[]{27,0}));
			dctCommandList.Add(DCTCommand.ClearText,(new byte[]{27,96}));
			dctCommandList.Add(DCTCommand.SpeakerOff,(new byte[]{27,11,0}));
			dctCommandList.Add(DCTCommand.SpeakerOn,(new byte[]{27,11,1}));
			dctCommandList.Add(DCTCommand.ScrollUp,(new byte[]{27,103,255}));
			dctCommandList.Add(DCTCommand.ScrollDown,(new byte[]{27,103,1}));
            dctCommandList.Add(DCTCommand.AutoReportingOn, (new byte[] { 0x1B,0x11,CommandEventArgs.AutoReportInterval}));
		}

//		public byte[] GetCommand(DCTCommand commandName)
//		{
//			return (gw28CommandList[commandName] as byte[]);
//		}

	
	}

}
