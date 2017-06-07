using System;
using System.Collections;

using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.FacilityFileParser
{

	/// <summary>
	/// AOI外部接口文件解析
	/// Laws Lu
	/// 2005/09/14
	/// 继承自DataFileParser
	/// </summary>
	public class PIDAOIFileParser:DataFileParser
	{
		new protected string _configFile = "PIDAOIDataFileParser.xml";
		//new protected string _formatName = "AOIDataFileParser.xml";
		protected const int pCodePosition = 14;
		protected int iTotalColumns = 0;

		protected const string ErrorCodeSplitSymbol = "";

		public PIDAOIFileParser(IDomainDataProvider dataProvider) : base(dataProvider)
		{
		}

		new protected void LoadFileName(string fileName)
		{
			_data.FileName.Clear();

			string[] ary = fileName.Split(_format.FileNameSeparator.ToCharArray());
			

			_data.FileName.Add(0,ary) ;
		}

		/// <summary>
		/// 解析测试PCS信息
		/// </summary>
		/// <param name="testFileName">文件名称</param>
		/// <returns></returns>
		public override object[] Parse(string testFileName)
		{
			this._ary = new ArrayList() ;

			LoadFormat( this._formatName );
			LoadFileName(testFileName);
			LoadHeader(testFileName);

			if(_format.FormatType == FileFormatType.Header)
			{
				// 如果文件本身不包含Data信息,立即进行数据值的Mapping
				AssignData();
			}

			while(LoadRecord(testFileName))
			{
				AssignData();
			}

			return (object[])( this._ary.ToArray( typeof(object) ));
		}

		new protected bool LoadRecord(string fileName)
		{
			if(_contentRead == false)
			{
				//还没有打开reader
				_reader = new System.IO.StreamReader(fileName,System.Text.Encoding.GetEncoding("BIG5"));

				int headerRowCount = _format.HeaderRowCount ;
				if(_format.FormatType == FileFormatType.Data)
				{
					headerRowCount = 0 ;
				}

				// 跳过头信息
				for(int i=0;i<headerRowCount;i++)
				{
					_reader.ReadLine();
				}
                
				_contentRead = true;
            
			}
            
			return LoadRecord(_reader);
            

		}
		
		new protected bool LoadRecord(System.IO.TextReader reader)
		{
			_data.Content.Clear();
			int contentMultiRecordInterval = _format.ContentMultiRecordInterval ;
			if(_format.IsContentMultiRecord == false)
			{
				contentMultiRecordInterval = 1 ;
			}

			for(int i=0;i<contentMultiRecordInterval;i++)
			{
				string line;
				try
				{
					line = reader.ReadLine();
				}
				catch
				{
					line = string.Empty ;
					return false ;
				}

				if(line ==  null )
				{
					return false ;
				}

				string[] valueAry = this.SplitString(line, _format.ContentColumnSeparator );
				iTotalColumns = valueAry.Length;
				_data.SetContent(i,valueAry);
			}

			return true ;
		}
		/// <summary>
		/// Laws Lu
		/// 2005/09/14
		/// 解析AOI文件格式
		/// </summary>
		new protected void AssignData()
		{
			if(!(((Array)_data.Content[0]).Length>1)) return; //如果是空行,不操作]

			int len = _format.ObjectMap.ColumnFormats.Length ;

			Type type= Type.GetType(_format.ObjectMap.ObjectTypeName) ;

			object obj = type.Assembly.CreateInstance(_format.ObjectMap.ObjectTypeName.Split(new char[]{','})[0].ToString());

			string objValue = string.Empty;

			for(int i = 0;i< len;i++)
			{
				ColumnFormat col = _format.ObjectMap.ColumnFormats[i] ;
				string attributeName = col.AttributeName ;
				string columnName = col.ColumnName ;

				switch(col.DataSource)
				{
					case DataSource.FileName:
						objValue = GetSubString(_data.GetFileName(col.DataLine,col.DataColumn) , col.DataStringFrom , col.DataStringTo);
						break;

					case DataSource.Header:
						objValue = GetSubString(_data.GetHeader(col.DataLine,col.DataColumn) , col.DataStringFrom , col.DataStringTo);;
						break;
                    
					case DataSource.Content:
						if(i >= 8)
						{
							int iCurrentColumn = pCodePosition + 1;
							//Get extend information 
							for(int iErrorCode = iCurrentColumn ;iErrorCode < iTotalColumns;iErrorCode ++)
							{
								objValue = objValue + GetSubString(_data.GetContent(col.DataLine,iErrorCode/*col.DataColumn*/), col.DataStringFrom , col.DataStringTo) + ",";
							}
							
						}
						else
						{
							objValue = GetSubString(_data.GetContent(col.DataLine,col.DataColumn), col.DataStringFrom , col.DataStringTo);
						}
						break;

					case DataSource.DefaultValue:
						objValue = col.Default ;
						break;

					default:
						objValue = col.Default ;
						break;

				}

				if(objValue == string.Empty)
				{
					objValue = col.Default ;
				}

				// 数据强行转换

				if( col.DataType == DataType.Int )
				{
					objValue = this.ConvertToInt( objValue ).ToString() ;
				}

				if( col.DataType == DataType.Float )
				{
					objValue = this.ConvertToFloat( objValue ).ToString() ;
				}

				if( col.DataType == DataType.DateTime )
				{
					objValue = this.ConvertToDateTimeFloat( objValue ).ToString("#") ;
				}

				if( col.DataType == DataType.Date )
				{
					objValue = this.ConvertToDateFloat( objValue ).ToString("#") ;
				}

				if( col.DataType == DataType.Time )
				{
					objValue = this.ConvertToTimeFloat( objValue ).ToString("#") ;
				}

				
//				if(i >= 8)
//				{
//					objValue = objValue.Split(new char[]{',','0',',','0',',','0',','});
//				}
//				else
//				{	//sign error code
					if(objValue != String.Empty && objValue.Substring(objValue.Length - 1,1) == ",")
					{
						objValue = objValue.Substring(0,objValue.Length - 1);
						if(objValue != String.Empty && objValue.IndexOf(ErrorCodeSplitSymbol) >= 0)
						{
							objValue = objValue.Replace(ErrorCodeSplitSymbol,"|");
						}
					}

					BenQGuru.eMES.Common.Domain.DomainObjectUtility.SetValue(obj,attributeName,objValue);

					objValue = String.Empty;
//				}

			}


			// vizo 2005-04-14
			// 多加了一个委托的判断

			bool objectValid = true ;

			if( this.CheckValidHandle != null)
			{
				objectValid = this.CheckValidHandle( obj ) ;
			}

			// 如果 object 是无效的,不操作
			if( objectValid )
			{
				_ary.Add( obj ) ;

				if(_format.SaveDataDirectly)
				{
					_dataProvider.Insert(obj);
				}
			}
		}
	}
}
