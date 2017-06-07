using System;
using System.Collections;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.DSC19
{
	/// <summary>
	/// 
	/// </summary>
	public class DSC19CommandList:DCTCommandList
	{
		public DSC19CommandList()
		{
		}

		public override void InitialCommand()
		{
			//dctCommandList.Add(DCTCommand.Reset,(new byte[]{27,0}));
			//dctCommandList.Add(DCTCommand.ClearText,(new byte[]{08,00,40,00,00,00,60,0}));
//			dctCommandList.Add(DCTCommand.ClearText,(new byte[]{8,0,64,0,0,0,96,0}));
//			dctCommandList.Add(DCTCommand.SpeakerOff,(new byte[]{11,0,64,0,0,0,11,0,10,0,1}));
//			dctCommandList.Add(DCTCommand.SpeakerOn,(new byte[]{11,0,64,0,0,0,11,0,0,10,1}));
//			dctCommandList.Add(DCTCommand.ScrollUp,(new byte[]{9,0,64,0,0,0,103,0,255}));
//			dctCommandList.Add(DCTCommand.ScrollDown,(new byte[]{8,0,64,0,0,0,48,0}));

			dctCommandList.Add(DCTCommand.ClearText,(new byte[]{8,0,64,0,0,0,96,0}));
			dctCommandList.Add(DCTCommand.SpeakerOff,(new byte[]{11,0,64,0,0,0,11,0,0,0,0}));
			dctCommandList.Add(DCTCommand.SpeakerOn,(new byte[]{11,0,64,0,0,0,11,0,1,255,1}));
			dctCommandList.Add(DCTCommand.ScrollUp,(new byte[]{9,0,64,0,0,0,103,0,255}));
			dctCommandList.Add(DCTCommand.ScrollDown,(new byte[]{8,0,64,0,0,0,48,0}));
			dctCommandList.Add(DCTCommand.AutoReportingOn,(new byte[]{8,0,64,0,0,0,17,5}));/* 最后一位表示5s报告一次 */
			dctCommandList.Add(DCTCommand.AutoReportingOff,(new byte[]{8,0,64,0,0,0,17,0})); /* 不报告 */
		}

	}

}
