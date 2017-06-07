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


#endregion

namespace BenQGuru.eMES.Web.Helper
{

	public class BOMFileParser
	{

		//update by crystal chu the exception 记录日志在异常处理的文件中
	    //private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(BOMFileParser));

		public const int FILE_COLUMN_SIZE =8;
		public  const char CHAR_SEPARATOR = '\\';
		public  const char CHAR_SEPARATOR_COMMA = ',';
		public  const string FILE_UPLOAD_PATH = @"upload\";
		public  const string ITEM_STATUS_NORMAL = "0";
		public  const string ITEM_CONTROL_LOT = "item_control_lot";
		public static string UploadFile2ServerUploadFolder(System.Web.UI.Page page, System.Web.UI.HtmlControls.HtmlInputFile inputFile, string serverFilefullPath)
		{
			if (inputFile.PostedFile == null)
			{
				return null;
			}

			if (inputFile.PostedFile.FileName.Trim() == string.Empty)
			{
				return null;
			}
			
			if (inputFile.PostedFile.ContentLength > 1048576)
			{
				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser), ErrorCenter.ERROR_UPLOAD_UPPERLIMIT)); 
			}
			
			string[] filePath = inputFile.PostedFile.FileName.Split(new char[] {CHAR_SEPARATOR} );
			string fileName = page.Request.PhysicalApplicationPath + FILE_UPLOAD_PATH + System.Guid.NewGuid().ToString().Substring(0,8) +  filePath[filePath.Length-1];
			if (serverFilefullPath != null)
			{
				fileName = serverFilefullPath;
			}

			try
			{
				//check the folder exist ,if no ,create it
				//add by crystal chu 20050404
				if( !System.IO.Directory.Exists(page.Request.PhysicalApplicationPath + FILE_UPLOAD_PATH))
				{
					System.IO.Directory.CreateDirectory(page.Request.PhysicalApplicationPath + FILE_UPLOAD_PATH);
				}
				inputFile.PostedFile.SaveAs( fileName);
			}
			catch(Exception ex)
			{
				//_log.Error(ex.Message,ex);
				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.SYSTEM_ERROR),ex); 
			}

			return fileName;
		}

		public static object[] ProcessFile(string logFile, bool inHead,string logUser)
		{
			if(!System.IO.File.Exists(logFile))
			{
				return null;
			}
			
			string line = null;
			ArrayList lineList =new ArrayList();
		
			StreamReader streamReader = null;
			
			try
			{
				streamReader = new StreamReader(logFile, System.Text.Encoding.GetEncoding("GB2312" ));
				line = streamReader.ReadLine().Trim ();

				if (! inHead)
				{
					line = streamReader.ReadLine().Trim ();
				}

				// TODO: 开始或中间有空行的处理
				while ((line != null)&&(line != ""))
				{
					lineList.Add(ProcessFileLine(line,logUser));
					line= streamReader.ReadLine();
				}
			}
			catch(Exception ex)
			{
				//_log.Error(ex.Message,ex);
				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.SYSTEM_ERROR) ,ex );
			}
			object[]  sboms =  new object[lineList.Count];
			for(int i=0;i<lineList.Count;i++)
			{
				sboms[i] = lineList[i];
			}

			return   sboms;
		}

		public static SBOM  ProcessFileLine(string lineContent,string logUser)
		{

			char[] splitChar = new char[1];
			splitChar[0] = CHAR_SEPARATOR_COMMA;
	
			string[] lineColumn=null;
			lineColumn = lineContent.Split(splitChar);
			
			if (lineColumn.Length <FILE_COLUMN_SIZE) 
			{
				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.FILE_COLUMN_ERROR));
			}
				
			if (lineColumn[1].ToString().Trim() == string.Empty)
			{
				throw new RemotingException("1"+ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.NOT_EMPTY_ERROR));
			}

			if (lineColumn[2].ToString().Trim() == string.Empty)
			{
				throw new RemotingException("2"+ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.NOT_EMPTY_ERROR));
			}

			if (lineColumn[3].ToString().Trim() == string.Empty)
			{
				throw new RemotingException("3"+ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.NOT_EMPTY_ERROR));
			}

			if (lineColumn[4].ToString().Trim() == string.Empty)
			{
				throw new RemotingException("4"+ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.NOT_EMPTY_ERROR));
			}

			SBOM sbom = new SBOM();
			try
			{
				sbom.Sequence = Int32.Parse( lineColumn[0].ToString());
			}
			catch
			{
				sbom.Sequence = 0;
			}
			sbom.SBOMItemECN = lineColumn[1].ToString().Trim();
			sbom.ItemCode   = lineColumn[2].ToString().Trim();
			sbom.SBOMItemCode  = lineColumn[3].ToString().Trim().ToUpper();
			sbom.SBOMItemName    = lineColumn[4].ToString().Trim();
			sbom.SBOMItemLocation = lineColumn[7].ToString().Trim();
			sbom.SBOMItemStatus = ITEM_STATUS_NORMAL;
			sbom.SBOMItemEffectiveDate = FormatHelper.TODateInt(lineColumn[5].ToString().Trim());
			sbom.SBOMItemEffectiveTime   = 0;
			sbom.SBOMItemInvalidDate    = FormatHelper.TODateInt(lineColumn[6].ToString().Trim());;
			sbom.SBOMItemInvalidTime     = 235959;
			sbom.SBOMItemQty = 0;
			sbom.SBOMItemControlType = ITEM_CONTROL_LOT;
			sbom.MaintainUser        = logUser;
			sbom.MaintainDate        = FormatHelper.TODateInt(DateTime.Today);
			sbom.MaintainTime        = FormatHelper.TOTimeInt(DateTime.Now);
            sbom.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
			
			if (sbom.SBOMItemInvalidDate < sbom.SBOMItemEffectiveDate)
				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.EFFECTDATE_ERROR));
			if (FormatHelper.TODateInt(DateTime.Today) >= sbom.SBOMItemInvalidDate)
				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.INEFFECTDATE_ERROR));
			return(sbom);
		}

		/// <summary>
		/// 标准Bom的location的","用";"替换,得到标准每行字符串
		/// </summary>
		/// <param name="mostring"></param>
		/// <returns></returns>
		private static string GetFormatMOString(string mostring)
		{
			int leftpostion = mostring.IndexOf(',');
			string newmostring = string.Empty;
			if (leftpostion != -1)
			{
				string leftstr =mostring.Substring(0,leftpostion);
				int rightpostion = mostring.IndexOf(',',leftpostion+1);
				if (rightpostion == -1)
				{
					throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(BOMFileParser),ErrorCenter.ERROR_CREATENEWREWORKMOCODE));
				}
				string origstring = mostring.Substring(leftpostion,rightpostion-leftpostion+1);
				string newstring = origstring.Replace(',',';');
				newmostring = mostring.Replace(origstring,newstring);
				return  newmostring;
			}
			else
			{
				return mostring;
			}
		}
	}
	

    public class FileLoadProcess
    {
		protected const int FILE_COLUMN_SIZE =8;
		protected  const char CHAR_SEPARATOR = '\\';
		public  const char CHAR_SEPARATOR_COMMA = ',';
		public  const string FILE_UPLOAD_PATH = @"upload\";
		public const string MOSTATUS_INITIAL ="initial";

        public static string UploadFile2ServerUploadFolder(System.Web.UI.Page page, System.Web.UI.HtmlControls.HtmlInputFile inputFile, string serverFilefullPath)
        {
            if (inputFile.PostedFile == null)
            {
                return null;
            }

            if (inputFile.PostedFile.FileName.Trim() == string.Empty)
            {
                return null;
            }
			
            if (inputFile.PostedFile.ContentLength > 1048576)
            {
                throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(FileLoadProcess), ErrorCenter.ERROR_UPLOAD_UPPERLIMIT)); 
            }
			
            string[] filePath = inputFile.PostedFile.FileName.Split(new char[] {CHAR_SEPARATOR} );
            string fileName = page.Request.PhysicalApplicationPath + FILE_UPLOAD_PATH + System.Guid.NewGuid().ToString().Substring(0,8) +  filePath[filePath.Length-1];
            if (serverFilefullPath != null)
            {
                fileName = serverFilefullPath;
            }

            if( !System.IO.Directory.Exists(page.Request.PhysicalApplicationPath + FILE_UPLOAD_PATH))
            {
                Directory.CreateDirectory(page.Request.PhysicalApplicationPath + FILE_UPLOAD_PATH);
            }

            try
            {
                inputFile.PostedFile.SaveAs( fileName);
            }
            catch(Exception ex)
            {
                // _log.Error(ex.Message,ex);
                throw new ArgumentException(ErrorCenter.GetErrorServerDescription(typeof(FileLoadProcess),ErrorCenter.SYSTEM_ERROR),ex); 
            }

            return fileName;
        }

    }





    #region DataFileParser

    
    public enum FileFormatType {Header=1 , Data=2 , HeaderData=3} ;
    public enum HeaderDataSeparateMode {ByRowCount=1 , BySeparator=2} ;
    public enum DataType {Int=1 , Float=2 , DateTime=3 , String=4 , Date=5 , Time=6 } ;
    public enum DataSource {FileName=1 , Header=2 , Content=3 , DefaultValue=4}
    public delegate bool CheckValid( object obj );


    /// <summary>
    /// 使用这个类做文件解析的工作
    /// </summary>
    public class DataFileParser
    {
        private FileData _data = new FileData();
        private bool _contentRead = false;
        private System.IO.TextReader _reader ;
        private IDomainDataProvider _dataProvider = null ;
        private FileFormat _format = new FileFormat();
        private string _configFile ;
        private string _formatName ;
        private ArrayList _ary ;

        public CheckValid CheckValidHandle  = null;

        public string FormatName
        {
            get
            {
                return _formatName ;
            }
            set
            {
                _formatName = value ;
            }
        }

        public string ConfigFile
        {
            get
            {
                return _configFile ;
            }
            set
            {
                _configFile = value ;
            }
        }
   
        public DataFileParser(IDomainDataProvider dataProvider)
        {
            _dataProvider = dataProvider ;
        }

        public DataFileParser()
        {
            _dataProvider = DomainDataProviderManager.DomainDataProvider();
        }



        public FileFormat Format
        {
            get
            {
                return _format ;
            }
        }
        public object[] Parse(string testFileName)
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


        protected void LoadFileName(string fileName)
        {
            _data.FileName.Clear();
            string[] ary = fileName.Split(_format.FileNameSeparator.ToCharArray());
            _data.FileName.Add(0,ary) ;
        }


        protected void LoadHeader(string fileName)
        {
            _data.Header.Clear();

            int headerRowCount = _format.HeaderRowCount ;
            if(_format.FormatType == FileFormatType.Data)
            {
                headerRowCount = 0 ;
            }



            if(headerRowCount < 1) return ;

            System.IO.TextReader reader = new System.IO.StreamReader(fileName);

            string line = reader.ReadLine();
            int lineNumber = 0 ;

            do
            {
                if( line == null)
                {
                    break;
                }

                if(lineNumber < headerRowCount)
                {
                    string[] valueAry = line.Split( _format.HeaderColumnSeparator.ToCharArray() );
                    _data.SetHeader(lineNumber,valueAry);
                }
                else
                {
                    break;
                }

                line = reader.ReadLine() ;
                lineNumber ++ ;


            }while(true);

            reader.Close();

        }

        
        protected bool LoadRecord(System.IO.TextReader reader)
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

                string[] valueAry = this.SplitString(line, _format.HeaderColumnSeparator );
                _data.SetContent(i,valueAry);
            }

            return true ;
        }


        protected bool LoadRecord(string fileName)
        {
            if(_contentRead == false)
            {
                //还没有打开reader
                _reader = new System.IO.StreamReader(fileName,System.Text.Encoding.GetEncoding("GB2312") );

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

        #region 分隔行

        protected string[] SplitString(string content,string spliter)
        {
            // \x06用来指代值的分界符,因为没有它的话,start,end的值是错的,最后要删除
            // \x07用来指代spliter,最后要替换成spliter
            string strReplaceQuote = "\x07" ;
            string strReplaceSpliter = new string('\x08',spliter.Length );

            int start = -1 ;
            int end = -1;
            do
            {
                start = this.GetStartPos( content,start+1) ;
                if( start > -1 )
                {

                    end = this.GetEndPos( content , start+1) ;
                    if(end > -1 )
                    {
                        string substr = content.Substring(start+1,end-start-1) ;
                        string front = content.Substring(0,start) ;
                        string after = content.Substring(end+1) ;
                        content = front + strReplaceQuote + substr.Replace(spliter,strReplaceSpliter) + strReplaceQuote + after ;
                        start = end ;
                    }
                }
            }
            while(end != -1 && start != -1);

            // 去掉值的分界符的替代符号
            content = content.Replace(strReplaceQuote,string.Empty) ;
            string[] values = new System.Text.RegularExpressions.Regex(spliter).Split(content) ;

            for(int i=0 ;i<values.Length ;i++)
            {
                values[i] = values[i].Replace(strReplaceSpliter,spliter) ;
                // 两个双引号变成一个
                values[i] = values[i].Replace("\"\"","\"") ;
            }

            return values ;


        }

        protected int GetStartPos(string content,int pos)
        {
            if(pos<0)
            {
                pos = 0 ;
            }
            return content.IndexOf('"',pos) ;
        }

        protected int GetEndPos(string content,int pos)
        {
            string str = content.Substring(pos) ;
            str = str.Replace("\"\"","\b\b") ;
            int newPos = GetStartPos(str,0);
            if(newPos == -1)
            {
                return -1 ;
            }

            return newPos+pos ;
        }

        #endregion


        protected int ConvertToInt(string str)
        {
			// Modify by Icyer 2005/09/07
			// 需要支持科学计数格式
            //Regex reg = new Regex(@"[^0-9]");
			Regex reg = new Regex(@"[^0-9Ee\+\-]");
			// Modify end
            int num ;
            try
            {
				// Modify by Icyer 2005/09/07
				// 需要支持科学计数格式
                //num = int.Parse( reg.Replace(str.ToString(),"") ) ;
				num = int.Parse( reg.Replace(str.ToString(),""), System.Globalization.NumberStyles.Float ) ;
				// Modify end
            }
            catch
            {
                num = 0;
            }

            if(str[0] == '-')
            {
                num = -num ;
            }

            return num ;
        }
        

        protected decimal ConvertToFloat(string str)
        {
			// Modify by Icyer 2005/09/07
			// 需要支持科学计数格式
			//Regex reg = new Regex(@"[^0-9\.]");
			Regex reg = new Regex(@"[^0-9\.Ee\+\-]");
			// Modify end
			decimal num ;
            try
            {
				// Modify by Icyer 2005/09/07
				// 需要支持科学计数格式
				//num = decimal.Parse( reg.Replace(str.ToString(),"") ) ;
				num = decimal.Parse( reg.Replace(str.ToString(),""), System.Globalization.NumberStyles.Float ) ;
				// Modify end
			}
            catch
            {
                num = 0;
            }

            if(str[0] == '-')
            {
                num = -num ;
            }

            return num ;
        }


        protected decimal ConvertToDateTimeFloat(string str)
        {
            DateTime dt ;
            try
            {
                dt = DateTime.Parse( str ) ;
            }
            catch
            {
                dt = DateTime.MinValue ;
            }

            return  System.Convert.ToDecimal(1000000 * System.Convert.ToDecimal(FormatHelper.TODateInt(dt)) + System.Convert.ToDecimal(FormatHelper.TOTimeInt(dt))) ;

        }
        

        protected int ConvertToDateFloat(string str)
        {
            DateTime dt ;
            try
            {
                dt = DateTime.Parse( str ) ;
            }
            catch
            {
                dt = DateTime.MinValue ;
            }

            return  FormatHelper.TODateInt(dt) ;

        }
        

        protected int ConvertToTimeFloat(string str)
        {
            DateTime dt ;
            try
            {
                dt = DateTime.Parse( str ) ;
            }
            catch
            {
                dt = DateTime.MinValue ;
            }

            return  FormatHelper.TOTimeInt(dt) ;

        }
        

        
        protected void AssignData()
        {
			if(!(((Array)_data.Content[0]).Length>1))return; //如果是空行,不操作
            int len = _format.ObjectMap.ColumnFormats.Length ;
            Type type= Type.GetType(_format.ObjectMap.ObjectTypeName) ;
            object obj = type.Assembly.CreateInstance(_format.ObjectMap.ObjectTypeName.Split(new char[]{','})[0].ToString());

            for(int i=0;i<len;i++)
            {
                ColumnFormat col = _format.ObjectMap.ColumnFormats[i] ;
                string attributeName = col.AttributeName ;
                string columnName = col.ColumnName ;

                // vizo:这里要获得数据的实际值
                string objValue = string.Empty;

                switch(col.DataSource)
                {
                    case DataSource.FileName:
                        objValue = GetSubString(_data.GetFileName(col.DataLine,col.DataColumn) , col.DataStringFrom , col.DataStringTo);
                        break;

                    case DataSource.Header:
                        objValue = GetSubString(_data.GetHeader(col.DataLine,col.DataColumn) , col.DataStringFrom , col.DataStringTo);;
                        break;
                    
                    case DataSource.Content:
                        objValue = GetSubString(_data.GetContent(col.DataLine,col.DataColumn), col.DataStringFrom , col.DataStringTo);;
                        break;

                    case DataSource.DefaultValue:
                        objValue = col.Default ;
                        break;

                    default:
                        objValue = col.Default ;
                        break;
                }

				//added by jessie lee,2005/9/28
				//对取出的数据进行trim
				objValue = objValue.Trim();

                //objValue = this.SubStringStr(objValue);

                if(objValue == string.Empty)
                {
                    objValue = col.Default ;
                }

                // 数据强行转换

				try
				{
					if( col.DataType == BenQGuru.eMES.Web.Helper.DataType.Int )
					{
						objValue = this.ConvertToInt( objValue ).ToString() ;
					}

					else if( col.DataType == BenQGuru.eMES.Web.Helper.DataType.Float )
					{
						objValue = this.ConvertToFloat( objValue ).ToString() ;
					}

					else if( col.DataType == BenQGuru.eMES.Web.Helper.DataType.DateTime )
					{
						objValue = this.ConvertToDateTimeFloat( objValue ).ToString("#") ;
					}

					else if( col.DataType == BenQGuru.eMES.Web.Helper.DataType.Date )
					{
						objValue = this.ConvertToDateFloat( objValue ).ToString("#") ;
					}

					else if( col.DataType == BenQGuru.eMES.Web.Helper.DataType.Time )
					{
						objValue = this.ConvertToTimeFloat( objValue ).ToString("#") ;
					}

					BenQGuru.eMES.Common.Domain.DomainObjectUtility.SetValue(obj,attributeName,objValue);
				}
				catch
				{
					throw new Exception("$Format_Error [$Line=" + (_ary.Count + 1).ToString() + " $Column=" + (i + 1).ToString() + " $Data=" + objValue + "]");
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

        private string SubStringStr(string str)
        {
            string newStr = string.Empty;

            if (str != null && str != string.Empty)
            {
                int num = str.IndexOf("\"");

                if (num == 0)
                {
                    newStr = str.Substring(1, str.Length - 2);
                }
                else
                {
                    newStr = str;
                }
                newStr = newStr.Replace("\"\"", "\"");
            }

            return newStr;
        }

        private string GetSubString(string str,int start,int end)
        {
            if(start>end)
            {
                return string.Empty ;
            }

            if(start>str.Length)
            {
                return string.Empty;
            }

            if(end>str.Length)
            {
                return str.Substring(start);
            }
            else
            {
                try
                {
                    return str.Substring(start,end-start);
                }
                catch
                {
                    return string.Empty ;
                }
            }
        }


        private void LoadFormat(string formatName)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(_configFile );
            XmlNode node = doc.SelectSingleNode("//Formats/Format[@Name='" + formatName + "']");
            if(node == null )
            {
                return ;
            }


            #region ContentColumnSeparator
            try
            {
                _format.ContentColumnSeparator = node.Attributes["ContentColumnSeparator"].Value;
            }
            catch
            {
            }
            #endregion

            #region ContentMultiRecordInterval
            try
            {
                _format.ContentMultiRecordInterval = int.Parse( node.Attributes["ContentMultiRecordInterval"].Value);
            }
            catch
            {
                _format.ContentMultiRecordInterval = 1 ;
            }
            #endregion

            #region ContentRecordSeparator
            try
            {
                _format.ContentRecordSeparator = node.Attributes["ContentRecordSeparator"].Value ;
            }
            catch
            {
            }
            #endregion

            #region FileNameSeparator
            try
            {
                _format.FileNameSeparator = node.Attributes["FileNameSeparator"].Value ;
            }
            catch
            {
            }
            #endregion

            #region FormatName
            _format.FormatName = formatName ;
            #endregion

            #region FormatType
            if( node.Attributes["FileFormat"].Value == "Header" )
            {
                _format.FormatType = FileFormatType.Header ;
            }
            else if( node.Attributes["FileFormat"].Value == "Data" )
            {
                _format.FormatType = FileFormatType.Data ;
            }
            else if( node.Attributes["FileFormat"].Value == "HeaderData" )
            {
                _format.FormatType = FileFormatType.HeaderData ;
            }
            #endregion

            #region HeaderColumnSeparator
            try
            {
                _format.HeaderColumnSeparator = node.Attributes["HeaderColumnSeparator"].Value ;
            }
            catch
            {
            }
            #endregion

            #region HeaderRecordSeparator
            try
            {
                _format.HeaderRecordSeparator = node.Attributes["HeaderRecordSeparator"].Value ;
            }
            catch
            {
            }
            #endregion

            #region HeaderRowCount
            try
            {
                _format.HeaderRowCount = int.Parse( node.Attributes["HeaderRowCount"].Value ) ;
            }
            catch
            {
            }
            #endregion

            #region IsContentMultiRecord
            if( node.Attributes["IsContentMultiRecord"].Value == "1")
            {
                _format.IsContentMultiRecord = true ;
            }
            else
            {
                _format.IsContentMultiRecord = false ;
            }
            #endregion

            #region SaveDataDirectly
            if( node.Attributes["SaveDataDirectly"].Value == "1")
            {
                _format.SaveDataDirectly = true ;
            }
            else
            {
                _format.SaveDataDirectly = false ;
            }

            #endregion

            XmlNode mapNode = node.SelectSingleNode("ObjectMap") ;
            if(mapNode == null)
            {
                return ;
            }

            _format.ObjectMap.ObjectTypeName = mapNode.Attributes["ObjectType"].Value ;
            
            _format.ObjectMap.ColumnFormats = new ColumnFormat[mapNode.ChildNodes.Count] ;
            for(int i=0;i<mapNode.ChildNodes.Count;i++)
            {
                XmlNode colNode = mapNode.ChildNodes[i] ;
                _format.ObjectMap.ColumnFormats[i] = new ColumnFormat();
                
                #region AttributeName
                try
                {
                    _format.ObjectMap.ColumnFormats[i].AttributeName = colNode.Attributes["AttributeName"].Value;
                }
                catch
                {
                }
                #endregion

                #region ColumnName
                try
                {
                    _format.ObjectMap.ColumnFormats[i].ColumnName = colNode.Attributes["ColumnName"].Value;
                }
                catch
                {
                }
                #endregion

                #region DataColumn
                try
                {
                    _format.ObjectMap.ColumnFormats[i].DataColumn = int.Parse(colNode.Attributes["DataColumn"].Value);
                }
                catch
                {
                    _format.ObjectMap.ColumnFormats[i].DataColumn = 0 ;
                }
                #endregion

                #region DataLine
                try
                {
                    _format.ObjectMap.ColumnFormats[i].DataLine = int.Parse(colNode.Attributes["DataLine"].Value);
                }
                catch
                {
                    _format.ObjectMap.ColumnFormats[i].DataLine = 0 ;
                }
                #endregion

                #region DataSource
                try
                {
                    string source = colNode.Attributes["DataSource"].Value ;
                    if(source == "Content")
                    {
                        _format.ObjectMap.ColumnFormats[i].DataSource = DataSource.Content ;
                    }
                    else if(source == "Header")
                    {
                        _format.ObjectMap.ColumnFormats[i].DataSource = DataSource.Header ;
                    }
                    else if(source == "DefaultValue")
                    {
                        _format.ObjectMap.ColumnFormats[i].DataSource = DataSource.DefaultValue ;
                    }
                    else if(source == "FileName")
                    {
                        _format.ObjectMap.ColumnFormats[i].DataSource = DataSource.FileName ;
                    }
                    else
                    {
                        _format.ObjectMap.ColumnFormats[i].DataSource = DataSource.DefaultValue ;
                    }

                }
                catch
                {
                    _format.ObjectMap.ColumnFormats[i].DataSource = DataSource.DefaultValue  ;
                }
                #endregion

                #region DataStringFrom
                try
                {
                    _format.ObjectMap.ColumnFormats[i].DataStringFrom = int.Parse( colNode.Attributes["DataStringFrom"].Value );
                }
                catch
                {
                    _format.ObjectMap.ColumnFormats[i].DataStringFrom = 0 ;
                }
                #endregion

                #region DataStringTo
                try
                {
                    _format.ObjectMap.ColumnFormats[i].DataStringTo = int.Parse( colNode.Attributes["DataStringTo"].Value );
                }
                catch
                {
                    _format.ObjectMap.ColumnFormats[i].DataStringTo = 0 ;
                }
                #endregion

                #region DataType

                try
                {
                    string str = colNode.Attributes["DataType"].Value.ToUpper() ;

                    if(str == "INT")
                    {
                        _format.ObjectMap.ColumnFormats[i].DataType = DataType.Int;
                    }

                    if(str == "FLOAT")
                    {
                        _format.ObjectMap.ColumnFormats[i].DataType = DataType.Float;
                    }

                    if(str == "STRING" )
                    {
                        _format.ObjectMap.ColumnFormats[i].DataType = DataType.String;
                    }

                    if(str == "DATETIME" )
                    {
                        _format.ObjectMap.ColumnFormats[i].DataType = DataType.DateTime;
                    }

                    if(str == "DATE" )
                    {
                        _format.ObjectMap.ColumnFormats[i].DataType = DataType.Date;
                    }

                    if(str == "TIME" )
                    {
                        _format.ObjectMap.ColumnFormats[i].DataType = DataType.Time;
                    }


                }
                catch
                {
                    _format.ObjectMap.ColumnFormats[i].DataType = 0 ;
                }

                #endregion

                #region Default
                try
                {
                    _format.ObjectMap.ColumnFormats[i].Default = colNode.Attributes["Default"].Value ;
                }
                catch
                {
                    _format.ObjectMap.ColumnFormats[i].Default = "" ;
                }
                #endregion





            }


        }

    }


    public class ObjectMap
    {
        public ObjectMap()
        {
            _columnFormats  = null;
            _isObjectInsert = string.Empty; 
            _objectTypeName = string.Empty; 
        }

        private ColumnFormat[] _columnFormats;
        private string  _isObjectInsert;
        private string _objectTypeName;
        

        public string IsObjectInsert
        {
            get
            {
                return _isObjectInsert;
            }
            set
            {
                _isObjectInsert = value;
            }
        }

        public string ObjectTypeName
        {
            get
            {
                return _objectTypeName;
            }
            set
            {
                _objectTypeName = value;
            }
        }

        public ColumnFormat[] ColumnFormats
        {
            get
            {
                return _columnFormats;
            }
            set
            {
                _columnFormats = value;
            }
        }
    }


    public class ColumnFormat
    {
        private string _attributeName ;
        private string columnName ;
        private DataType dataType ;
        private DataSource dataSource ;
        private int dataLine ;
        private int dataColumn ;
        private int dataStringFrom ;
        private int dataStringTo ;
        private string defaultValue ;

        public ColumnFormat()
        {
        }

        public string ColumnName 
        {
            get
            {
                return columnName ;
            }
            set
            {
                columnName = value ;
            }
        } 
        public DataType DataType 
        {
            get
            {
                return dataType ;
            }
            set
            {
                dataType = value ;
            }
        } 
        public DataSource DataSource 
        {
            get
            {
                return dataSource ;
            }
            set
            {
                dataSource = value ;
            }
        } 
        public int DataLine 
        {
            get
            {
                return dataLine ;
            }
            set
            {
                dataLine = value ;
            }
        } 
        public int DataColumn 
        {
            get
            {
                return dataColumn ;
            }
            set
            {
                dataColumn = value ;
            }
        } 
        public int DataStringFrom 
        {
            get
            {
                return dataStringFrom ;
            }
            set
            {
                dataStringFrom = value ;
            }
        } 
        public int DataStringTo 
        {
            get
            {
                return dataStringTo ;
            }
            set
            {
                dataStringTo = value ;
            }
        } 

        public string Default 
        {
            get
            {
                return defaultValue ;
            }
            set
            {
                defaultValue = value ;
            }
        } 

        public string AttributeName
        {
            get
            {
                return _attributeName ;
            }
            set
            {
                _attributeName = value ;
            }
        }
    }


    public class FileData
    {
        private Hashtable _header ;
        private Hashtable _content ;
        private Hashtable _fileName ;

        public Hashtable FileName
        {
            get
            {
                return _fileName ;
            }
            set
            {
                _fileName = value ;
            }
        }

        public Hashtable Header
        {
            get
            {
                return _header ;
            }
            set
            {
                _header = value ;
            }
        }

        public Hashtable Content
        {
            get
            {
                return _content ;
            }
            set
            {
                _content = value ;
            }
        }

        

        public FileData()
        {
            _header = new Hashtable();
            _content = new Hashtable();
            _fileName = new Hashtable();
        }


        public string GetHeader(int row,int column)
        {
            try
            {
                string[] list = (string[])_header[row];
                return (string)list[column];
            }
            catch
            {
                return string.Empty ;
            }
        }


        public void SetHeader(int row,string[] headerValue)
        {
            if(_header.ContainsKey(row))
            {
                _header[row] = headerValue ;
            }
            else
            {
                _header.Add(row,headerValue);
            }
        }


        public string GetContent(int row,int column)
        {
            try
            {
                string[] list = (string[])_content[row];
				if(column>list.Length-1)return string.Empty ;
                return (string)list[column];
            }
            catch
            {
                return string.Empty ;
            }
        }


        public void SetContent(int row,string[] contentValue)
        {
            if(_header.ContainsKey(row))
            {
                _content[row] = contentValue ;
            }
            else
            {
                _content.Add(row,contentValue);
            }
        }

        public string GetFileName(int row,int column)
        {
            try
            {
                string[] list = (string[])_fileName[row];
                return (string)list[column];
            }
            catch
            {
                return string.Empty ;
            }

        }

        public void SetFileName(int row,string[] fileNameValue)
        {
            if(_header.ContainsKey(row))
            {
                _fileName[row] = fileNameValue ;
            }
            else
            {
                _fileName.Add(row,fileNameValue);
            }
        }



    }


    public class FileFormat
    {
        private string formatName ;
        private FileFormatType formatType;
        private int headerRowCount ;
        private string fileNameSeparator;
        private string headerRecordSeparator;
        private string headerColumnSeparator;
        private string contentRecordSeparator ;
        private string contentColumnSeparator;
        private bool isContentMultiRecord ;
        private int contentMultiRecordInterval ;
        private ObjectMap _objectMap ;
        private bool _saveDataDirectly ;


        public FileFormat()
        {
            this._objectMap = new ObjectMap();
            formatName = "";
            headerRowCount = 0 ;
            fileNameSeparator = ". ";
            headerRecordSeparator = "" ;
            headerColumnSeparator = ", " ;
            contentRecordSeparator = "" ;
            contentColumnSeparator = ", " ;
            isContentMultiRecord = false ;
            contentMultiRecordInterval = 1 ;
            _saveDataDirectly = false ;

        }


        public bool SaveDataDirectly
        {
            get
            {
                return _saveDataDirectly ;
            }

            set
            {
                _saveDataDirectly = value ;
            }
        }


        public string FormatName
        {
            get
            {
                return formatName ;
            }

            set
            {
                formatName = value ;
            
            }
        }
        public FileFormatType FormatType 
        {
            get
            {
                return formatType ;
            }
            
            set
            {
                formatType = value ;
            }
        } 
        public string FileNameSeparator
        {
            get
            {
                return fileNameSeparator ;
            }
            set
            {
                fileNameSeparator = value ;
            }
        }

        public int HeaderRowCount 
        {
            get
            {
                return headerRowCount ;
            }
            set
            {
                headerRowCount  = value ;
            }
        } 
        public string HeaderRecordSeparator 
        {
            get
            {
                return headerRecordSeparator ;
            }
            set
            {
                headerRecordSeparator = value ;
            }
        }
        public string HeaderColumnSeparator 
        {
            get
            {
                return headerColumnSeparator ;
            }
            set
            {
                headerColumnSeparator = value ;
            }
        } 
        public string ContentRecordSeparator
        {
            get
            {
                return contentRecordSeparator ;
            }
            set
            {
                contentRecordSeparator = value ;
            }
        } 

        public string ContentColumnSeparator
        {
            get
            {
                return contentColumnSeparator ;
            }
            set
            {
                contentColumnSeparator = value ;
            }
        } 

        public bool IsContentMultiRecord
        {
            get
            {
                return isContentMultiRecord ;
            }
            set
            {
                isContentMultiRecord = value ;
            }
        }

        public int ContentMultiRecordInterval
        {
            get
            {
                return contentMultiRecordInterval ;
            }
            set
            {
                contentMultiRecordInterval = value ;
            }
        }

        public ObjectMap ObjectMap
        {
            get
            {
                return _objectMap ;
            }
            set
            {
                _objectMap = value ;
            }
        }
    }



    #endregion

}
