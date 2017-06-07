#region system
using System;
#endregion 

#region project
using BenQGuru.eMES.OQC;
#endregion

namespace BenQGuru.eMES.Client
{
	public enum OQCSource
	{
		PlanarCode,Carton,PCS
	}

	public enum OQCType
	{
		Normal,ReDO
	}


	
	public class OQCHelper
	{
		private OQCHelper()
		{
		}

		public const int OQCLot_MaxQuantity = -1;
		public const bool IsRemixMO = false;
		public const int Lot_Sequence_Default =0;

		public static void ShowMessage(string message)
		{
			ApplicationRun.GetInfoForm().Add( message );
		}

	}


}