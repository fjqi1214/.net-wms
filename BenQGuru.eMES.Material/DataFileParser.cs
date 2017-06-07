using System;
using System.IO;
using System.Collections;
using System.Runtime.Remoting;
using System.Xml ;
using System.Text.RegularExpressions ;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Material
{    

	/// <summary>
	/// 出入库外部接口文件解析
	/// Laws Lu
	/// 2005/09/14
	/// 继承自DataFileParser
	/// </summary>
	/// 
	[Serializable]
	public class StockFileParser:DataFileParser
	{
		protected int pCodePosition = 4;

		/// <summary>
		/// 解析出入库产品文件
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
		/// <summary>
		/// 设置二维条码出现的位置
		/// </summary>
		public int PlanateCodePostion
		{
			set
			{
				pCodePosition = value;
			}
		}
		
		/// <summary>
		/// Laws Lu
		/// 2005/09/14
		/// 允许解析手机和二维条码
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
						if(i >= pCodePosition && i <= (pCodePosition + 3))
						{
							objValue = objValue + GetSubString(_data.GetContent(col.DataLine,col.DataColumn), col.DataStringFrom , col.DataStringTo) + ",";
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

				
				if(i >= pCodePosition && i < (pCodePosition + 3))
				{
				}
				else
				{
					if(objValue != String.Empty && objValue.Substring(objValue.Length - 1,1) == ",")
					{
						objValue = objValue.Substring(0,objValue.Length - 1);
						if(objValue != String.Empty && objValue.Substring(objValue.Length - 1,1) == ",")
						{
							objValue = objValue.Substring(0,objValue.Length - 2);
						}
					}

					BenQGuru.eMES.Common.Domain.DomainObjectUtility.SetValue(obj,attributeName,objValue);

					objValue = String.Empty;
				}

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
