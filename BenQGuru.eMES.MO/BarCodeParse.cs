#region system
using System;
using System.IO;
using System.Collections;
using System.Runtime.Remoting;
using System.Xml ;
using System.Text.RegularExpressions ;
#endregion

#region project
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.MOModel;
#endregion



namespace BenQGuru.eMES.MOModel
{
	public class BarCodeParse
	{
		public  const char CHAR_SEPARATOR_COMMA = ',';
		public  const int  CELL_SEPARATOR_LENGTH = 1;
		public  const int  PHONE_SEPARATOR_LENGTH = 3;
		public  const string MODEL_TYPE_CELL ="Cell";
		public  const string MODEL_TYPE_PHONE ="Phone";
		public  const string MODEL_TYPE_OUT ="Out";
		public  const int ID_CELL_LENGTH = 9;
		public  const int AMODELCODE_LENGTH = 2;

		private IDomainDataProvider _domainDataProvider = null;

		public BarCodeParse(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public IDomainDataProvider DataProvider
		{
			get
			{
				if (_domainDataProvider == null)
				{
					_domainDataProvider = DomainDataProviderManager.DomainDataProvider();
				}

				return _domainDataProvider;
			}
		}

		public string GetBarCodeType(string barCode)
		{
			string[] tmpStrings = barCode.Split(new char[]{CHAR_SEPARATOR_COMMA});
			if(barCode.Length == 0)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCode",String.Format("[$BarCode='{0}']",barCode));
			}
			if(tmpStrings.Length == 1)
			{
				return MODEL_TYPE_OUT;
				//ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCode",String.Format("[$BarCode='{0}']",barCode));
			}
			if(tmpStrings.Length == CELL_SEPARATOR_LENGTH+1)
			{
				return MODEL_TYPE_CELL;
			}
			if(tmpStrings.Length == PHONE_SEPARATOR_LENGTH+1)
			{
				return MODEL_TYPE_PHONE;
			}
			return string.Empty;
		}

		public string[] GetIDList(string barCode)
		{
			if( GetBarCodeType(barCode)== string.Empty)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCode",String.Format("[$BarCode='{0}']",barCode));
			}
			string[] tmpStrings = null;

			//对手机器的处理
			if( GetBarCodeType(barCode)== MODEL_TYPE_CELL)
			{
				tmpStrings =  barCode.Split(new char[]{CHAR_SEPARATOR_COMMA});

				if(tmpStrings[1].Length%9 != 0)
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCodeLength ",String.Format("[$BarCode='{0}']",tmpStrings[1]));
				}
				return GetCellIMEIString(tmpStrings[0],tmpStrings[1]);
			}
			//对小灵通的处理
			if( GetBarCodeType(barCode) == MODEL_TYPE_PHONE)
			{
				CheckPhoneCode(barCode);
				tmpStrings = barCode.Split(new char[] {CHAR_SEPARATOR_COMMA});

				if(System.Int32.Parse(tmpStrings[1]) * System.Int32.Parse(tmpStrings[2]) != tmpStrings[3].Length)
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCode",String.Format("[$BarCode='{0}']",barCode));
				}

				return GetPhonePSID(tmpStrings[0],System.Int32.Parse(tmpStrings[1]),System.Int32.Parse(tmpStrings[2]),tmpStrings[3]);
			}
			//对外销机的处理
			if( GetBarCodeType(barCode) == MODEL_TYPE_OUT)
			{
				CheckOutCode(barCode);
				//tmpStrings = barCode.Split(new char[] {CHAR_SEPARATOR_COMMA});
//
//
//				if(System.Int32.Parse(tmpStrings[1]) * System.Int32.Parse(tmpStrings[2]) != tmpStrings[3].Length)
//				{
//					ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCode",String.Format("[$BarCode='{0}']",barCode));
//				}

				return GetOutPSID(barCode);
			}
			return null;
		}

		public string GetModelCode(string barCode)
		{
			string[] tmpStrings = barCode.Split(new char[]{CHAR_SEPARATOR_COMMA});

			if(tmpStrings.Length == 0)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCode",String.Format("[$BarCode='{0}']",barCode));
			}

			return tmpStrings[0].Trim().ToUpper();
		}

		#region private method
		private void CheckPhoneCode(string barCodeString)
		{
			string[] tmpStrings = barCodeString.Split(new char[] {CHAR_SEPARATOR_COMMA});
			int packingCount = 0;
			int PSIDLength = 0;
			try
			{
				packingCount = System.Int32.Parse(tmpStrings[1]);
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_PackingCount ",tmpStrings[1],ex);
			}
			try
			{
				PSIDLength = System.Int32.Parse(tmpStrings[2]);
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_PSIDLength ",tmpStrings[2],ex);
			}
			if(tmpStrings[3].Trim().Length != packingCount*PSIDLength)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCodeLength ",tmpStrings[3]);
			}
		}

		private void CheckOutCode(string barCodeString)
		{
			int iSplite = 15;
			if(barCodeString.Length < 15)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCodeLength ",barCodeString);
			}
			if(barCodeString.Length%15 != 0)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCodeLength ",barCodeString);
			}
		}

		private string[] GetPhonePSID(string modelCode,int packingCount,int PSIDLength,string barCodeString)
		{
			string tmpBarCodeString = barCodeString;
			string tmpString = string.Empty;
			ArrayList arrayList = new ArrayList();
			while(tmpBarCodeString.Length >= PSIDLength)
			{
				tmpString = tmpBarCodeString.Substring(0,PSIDLength);
				arrayList.Add(tmpString);
				tmpBarCodeString = tmpBarCodeString.Substring(PSIDLength,tmpBarCodeString.Length-PSIDLength);
			}
			return  (string[])arrayList.ToArray(typeof(string));
		}

		private string[] GetOutPSID(string barCodeString)
		{
			string tmpBarCodeString = String.Empty;
			string tmpString = string.Empty;
			ArrayList arrayList = new ArrayList();
			int PSIDLength = Convert.ToInt32(barCodeString.Length / 15);

			for(int i = 0;i< barCodeString.Length;i++)
			{
				tmpString = barCodeString.Substring(i,15);
				arrayList.Add(tmpString);

				i = i + 14;
				if(i >= barCodeString.Length)
				{
					break;
				}
			}
			return  (string[])arrayList.ToArray(typeof(string));
		}

		private string[] GetCellIMEIString(string modelCode,string barCodeString)
		{
			string tmpBarCodeString = barCodeString;
			string tmpString = string.Empty;
			string aModelCode = string.Empty;
			ArrayList arrayList = new ArrayList();
	        ModelFacade _modelFacade = new ModelFacade(DataProvider);
			while(tmpBarCodeString.Length >= ID_CELL_LENGTH)
			{
			    tmpString = tmpBarCodeString.Substring(0,ID_CELL_LENGTH);
				aModelCode = tmpString.Substring(0,AMODELCODE_LENGTH);
				object barCodeRule =  _modelFacade.GetBarcodeRule(modelCode,aModelCode);
				if(barCodeRule == null)
				{
					ExceptionManager.Raise(this.GetType().BaseType,"$Error_BarCodeRule_NotMaintain",String.Format("[$ModelCode='{0}',$aModelCode='{1}']",modelCode,aModelCode));
				}
				arrayList.Add(((BarcodeRule)barCodeRule).Description + tmpString.Substring(AMODELCODE_LENGTH,ID_CELL_LENGTH-AMODELCODE_LENGTH));
				tmpBarCodeString = tmpBarCodeString.Substring(ID_CELL_LENGTH,tmpBarCodeString.Length-ID_CELL_LENGTH);
			}
			return (string[])arrayList.ToArray(typeof(string));
		}
		#endregion
	}
}