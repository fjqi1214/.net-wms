using System;
using System.Collections;
using System.IO;	
using System.Text;

using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.FacilityFileParser
{

	/// <summary>
	/// AOI外部接口文件解析
	/// Laws Lu
	/// 2005/09/14
	/// 继承自DataFileParser
	/// </summary>
	public class ICTFileParser
	{
		
		//Test Result's position in data file;
		public int iTestResultPosition = 0;
		//Resource Code's position in data file;
		public int iResourceCodePosition = 1;
		//SN Code's position in data file;
		public int iRcardPosition = 4;
		//Date information's position in data file;
		public int iDatePosition = 5;
		//Time information's position in data file;
		public int iTimePosition = 6;
		//User information's position in data file
		public int iUserCodePosition = 10;

		//Open Failure's position in data file
		protected const string OpenFailInDataFile = "Open";
		//Short Failure's position in data file
		protected const string ShortFailInDataFile = "Short";
		//Compenent Failure's position in data file
		protected const string CompenentFailInDataFile = "";

		//Open Failure
		protected const string OpenFail = "PVIB";
		//Short Failure
		protected const string ShortFail = "PVIA";
		//Compenent Failure
		protected const string CompenentFail = "PVIC";

		public ICTFileParser()
		{
			
		}
		//parse file and return data object
		public object[] Parse(string fileName)
		{
			ICTData data = null;

			TextReader tr = null;
			try
			{
				tr = File.OpenText(fileName);

				Hashtable alErr = new Hashtable();
				
				try
				{
					//compenent error code
					string errsCompenet = String.Empty;
					//open failure error code
					string errsOpen = String.Empty;
					//short failure error code
					string errsShort = String.Empty;

					alErr.Add(OpenFail,errsOpen);
					alErr.Add(ShortFail,errsShort);
					alErr.Add(CompenentFail,errsCompenet);

					for(int i = 0;i < 1024 ;i ++) 
					{
						string content = tr.ReadLine();
						
						if(i == 0)//process content
						{
							#region set base data
							string[] cons = content.Split(new char[]{','});

							//set entity value from processed comment
							if(cons != null && cons.Length > 0)
							{
								data = new ICTData();
								data.RESULT =	cons[iTestResultPosition];
								data.RCARD	=	cons[iRcardPosition];
								data.RESOURCE	=	cons[iResourceCodePosition];
								data.MDATE	=	cons[iDatePosition];
								data.MTIME	=	cons[iTimePosition];
								data.USER	=	cons[iUserCodePosition];
							}
							#endregion
						}

						if( i > 1)
						{
							#region set error type
							if(content != String.Empty)
							{
								if(content.IndexOf(OpenFailInDataFile) >= 0)
								{
									content = OpenFail  + ","  +  content;

									errsOpen = errsOpen + ";" +content ;

									alErr[OpenFail] = errsOpen;
								}
								else if(content.IndexOf(ShortFailInDataFile) >= 0)
								{
									content = ShortFail  + ","  +  content;

									errsShort = errsShort + ";" +content ;

									alErr[ShortFail] = errsShort;
								}
								else
								{
									content = CompenentFail + ","  + content;

									errsCompenet = errsCompenet + ";" + content ;

									alErr[CompenentFail] = errsCompenet;
								}
								//errs = errs + ";" +content ;
							}
//							else
//							{
//								alErr.Add(errs);
//								errs = String.Empty;
//							}
							#endregion
						}
					}
				}
				catch
				{
					//set data value
					foreach(string errKey in alErr.Keys)
					{
						data.ERRORCODES = data.ERRORCODES + alErr[errKey] + "|";
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				//close file reader
				if(tr != null)
				{
					tr.Close();
				}
			}

			return new object[]{data};
		}
	}
}
