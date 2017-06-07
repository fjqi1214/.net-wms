using System;
using System.Collections;

using BenQGuru.eMES.Common.DCT.Core;

namespace BenQGuru.eMES.Common.DCT.ATop.DT4000
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public class DT4000CommandList:DCTCommandList
	{
		//		protected Hashtable gw28CommandList 
		//			= new Hashtable();


		public DT4000CommandList()
		{
		}

		public override void InitialCommand()
		{
			//dctCommandList.Add(DCTCommand.Reset,(new byte[]{27,0}));
			//dctCommandList.Add(DCTCommand.ClearText,(new byte[]{08,00,40,00,00,00,60,0}));
			dctCommandList.Add(DCTCommand.ClearText,(new byte[]{8,0,64,0,0,0,0x60,0}));
            dctCommandList.Add(DCTCommand.ClearGraphic, (new byte[] { 8, 0, 64, 0, 0, 0, 0x40, 0 }));
            dctCommandList.Add(DCTCommand.ClearUserInput, (new byte[] { 8, 0, 64, 0, 0, 0, 48, 0 }));

			dctCommandList.Add(DCTCommand.SpeakerOff,(new byte[]{11,0,64,0,0,0,11,0,5,1,1}));
			dctCommandList.Add(DCTCommand.SpeakerOn,(new byte[] {11,0,64,0,0,0,11,0,1,1,1}));
			dctCommandList.Add(DCTCommand.ScrollUp,(new byte[]{9,0,64,0,0,0,103,0,255}));			
            dctCommandList.Add(DCTCommand.AutoReportingOn, (new byte[] { 0x9, 0, 0x40, 0, 0, 0, 0x11, 0 , CommandEventArgs.AutoReportInterval }));//DCT向host报告live
            dctCommandList.Add(DCTCommand.AutoReportingOff, (new byte[] { 0x09, 0, 0x40, 0, 0, 0, 0x11, 0,0 })); /* 不报告 */
            dctCommandList.Add(DCTCommand.HostReportSetting, (new byte[] { 0x09, 0, 0x40, 0, 0, 0, 0x12, 00, CommandEventArgs.AutoReportInterval })); //host 向DCT报告 live
            dctCommandList.Add(DCTCommand.HostReportPackage, (new byte[] { 0x08, 0, 0x40, 0, 0, 0, 0x12, 01})); //host 向DCT报告 live
		}

		//		public byte[] GetCommand(DCTCommand commandName)
		//		{
		//			return (gw28CommandList[commandName] as byte[]);
		//		}

	}

}
