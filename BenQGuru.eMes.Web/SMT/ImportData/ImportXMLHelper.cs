using System;
using System.Collections;
using System.Xml;

namespace BenQGuru.eMES.Web.SMT.ImportData
{
   ///<summary>
	/// ImportXMLHelper 的摘要说明。
	/// </summary>
	[Serializable]
	public class ImportXMLHelper
	{
		public ImportXMLHelper(string xmlpath)
		{
			this.XMLPath = xmlpath;	
		}

        public ImportXMLHelper(string xmlpath, string dataType)
        {
            this.XMLPath = xmlpath;
            this.selectedImportType = dataType;
        }

		/// <summary>
		/// XML文件路径
		/// </summary>
		public string XMLPath
		{
			get
			{
				return xmlPath ;
			}
			set
			{
				xmlPath = value;
			}
		}private string xmlPath = string.Empty;

        /// <summary>
        /// 导入类型
        /// </summary>
		public ArrayList ImportType
		{
			get
			{
				return this.GetImportType() ;
			}
		}		
		
		/// <summary>
		/// 导入类型
		/// </summary>
		public ArrayList GridBuilder
		{
			get
			{
				return this.GetGridBuilder() ;
			}
		}

		/// <summary>
		/// 导入类型
		/// </summary>
		public ArrayList NotAllowNullField
		{
			get
			{
				return this.GetNotAllowNullField() ;
			}
		}

		/// <summary>
		/// 选中导入类型
		/// </summary>
		public string SelectedImportType
		{
			get
			{
				return selectedImportType ;
			}
			set
			{
				selectedImportType = value ;
			}
		}private string selectedImportType = string.Empty;

		private ArrayList GetImportType()
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load( this.XMLPath );
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("datatype");

			ArrayList itArray = new ArrayList();

			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					string name = nodeList[i].Attributes["name"].InnerText;
					string text = nodeList[i].Attributes["text"].InnerText;
					DictionaryEntry de = new DictionaryEntry(name,text);
					itArray.Add( de );
				}
			}
			
			return itArray;
		}

		//melo 添加于2006.12.4 用于多语言
		public ArrayList GetImportType(ControlLibrary.Web.Language.LanguageComponent languageComponent)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load( this.XMLPath );
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("datatype");

			ArrayList itArray = new ArrayList();

			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					string name = nodeList[i].Attributes["name"].InnerText;
					string text = languageComponent.GetString( "$Grid_"+name );
					DictionaryEntry de = new DictionaryEntry(name,text);
					itArray.Add( de );
				}
			}
			
			return itArray;
		}

        public ArrayList GetGridBuilder()
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load( this.XMLPath );
			
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");

			ArrayList gbArray = new ArrayList();
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType )
					{
						if(node.ChildNodes.Count>0)
						{
							for( int j=0; j<node.ChildNodes.Count; j++ )
							{
								string key = node.ChildNodes[j].Attributes["key"].InnerText.ToString() ;
								string headerText = node.ChildNodes[j].Attributes["value"].InnerText.ToString() ;
								DictionaryEntry de = new DictionaryEntry(key,headerText);
								gbArray.Add(de);
							}
						}

						break;
					}
				}
			}
			return gbArray;
		}

		//melo 添加于2006.12.4 用于多语言
		public ArrayList GetGridBuilder(ControlLibrary.Web.Language.LanguageComponent languageComponent)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load( this.XMLPath );
			
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");

			ArrayList gbArray = new ArrayList();
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType )
					{
						if(node.ChildNodes.Count>0)
						{
							for( int j=0; j<node.ChildNodes.Count; j++ )
							{
								string key = node.ChildNodes[j].Attributes["key"].InnerText.ToString().Trim() ;
								string headerText = languageComponent.GetString( "$Grid_"+key );
								DictionaryEntry de = new DictionaryEntry(key,headerText);
								gbArray.Add(de);
							}
						}

						break;
					}
				}
			}
			return gbArray;
		}

        public ArrayList GetNotAllowNullField(ControlLibrary.Web.Language.LanguageComponent languageComponent)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.XMLPath);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");

            ArrayList gbArray = new ArrayList();
            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];
                    if (node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType)
                    {
                        if (node.ChildNodes.Count > 0)
                        {
                            string value = "value_" + languageComponent.Language.Trim();
                            for (int j = 0; j < node.ChildNodes.Count; j++)
                            {
                                if (bool.Parse(node.ChildNodes[j].Attributes["AllowNull"].InnerText.ToString())) continue;
                                string key = node.ChildNodes[j].Attributes["key"].InnerText.ToString();
                                string headerText = node.ChildNodes[j].Attributes[value].InnerText.ToString();
                                DictionaryEntry de = new DictionaryEntry(key, headerText);
                                gbArray.Add(de);
                            }
                        }

                        break;
                    }
                }
            }
            return gbArray;
        }

		private ArrayList GetNotAllowNullField()
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load( this.XMLPath );
			
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");

			ArrayList gbArray = new ArrayList();
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType )
					{
						if(node.ChildNodes.Count>0)
						{
							for( int j=0; j<node.ChildNodes.Count; j++ )
							{
								if( bool.Parse(node.ChildNodes[j].Attributes["AllowNull"].InnerText.ToString()) )continue;
								string key = node.ChildNodes[j].Attributes["key"].InnerText.ToString() ;
								string headerText = node.ChildNodes[j].Attributes["value"].InnerText.ToString() ;
								DictionaryEntry de = new DictionaryEntry(key,headerText);
								gbArray.Add(de);
							}
						}

						break;
					}
				}
			}
			return gbArray;
		}

        //Value值有三种情况时，多语言
        public ArrayList GetGridBuilder(ControlLibrary.Web.Language.LanguageComponent languageComponent, string dataType)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.XMLPath);

            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");

            ArrayList gbArray = new ArrayList();
            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];
                    if (node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType)
                    {
                        if (node.ChildNodes.Count > 0)
                        {
                            string value = "value_" + languageComponent.Language.Trim();
                            for (int j = 0; j < node.ChildNodes.Count; j++)
                            {
                                string key = node.ChildNodes[j].Attributes["key"].InnerText.ToString();
                                string headerText = node.ChildNodes[j].Attributes[value].InnerText.ToString();

                                DictionaryEntry de = new DictionaryEntry(key, headerText);
                                gbArray.Add(de);
                            }
                        }

                        break;
                    }
                }
            }
            return gbArray;
        }

		public bool NeedMatchCtoE( string key )
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load( this.XMLPath );
			
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType )
					{
						if(node.ChildNodes.Count>0)
						{
							for( int j=0; j<node.ChildNodes.Count; j++ )
							{
								if( string.Compare(key, node.ChildNodes[j].Attributes["key"].InnerText.ToString(),true)==0 )
								{
									return bool.Parse(node.ChildNodes[j].Attributes["Match"].InnerText.ToString());
								}
							}
						}
					}
				}
			}

			return false;
		}

		/// <summary>
		/// 使用该方法前请现用NeedMatchCtoE验证
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public string MatchType( string key )
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load( this.XMLPath );
			
			XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");
			if(nodeList.Count>0)
			{
				for( int i=0; i<nodeList.Count; i++ )
				{
					XmlNode node = nodeList[i];
					if( node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType )
					{
						if(node.ChildNodes.Count>0)
						{
							for( int j=0; j<node.ChildNodes.Count; j++ )
							{
								if( string.Compare(key, node.ChildNodes[j].Attributes["key"].InnerText.ToString(),true)==0 )
								{
									return node.ChildNodes[j].Attributes["MatchType"].InnerText.ToString();
								}
							}
						}

						break;
					}
				}
			}

			return string.Empty;
		}

		public string GetMatchKeyWord( string valueWord,string matchType )
		{
			XmlDocument xmlDoc = new XmlDocument();
			string[] paths = this.XMLPath.Split('\\','/');
			xmlDoc.Load( string.Join(@"\", paths, 0, paths.Length-1) + @"\MatchType.xml");

			XmlNodeList nodeList = xmlDoc.GetElementsByTagName(matchType);
			if(nodeList[0].ChildNodes.Count>0)
			{
				for( int i=0; i<nodeList[0].ChildNodes.Count; i++ )
				{
					XmlNode node = nodeList[0].ChildNodes[i];
					if( string.Compare( valueWord, node.Attributes["value"].InnerText,true)==0 )
					{
						return node.Attributes["key"].InnerText ;
					}
				}
			}

			return string.Empty;
		}

        //添加按照当前登录语言选取模板字段名称
        public ArrayList GetColumnNameByLanguage(ControlLibrary.Web.Language.LanguageComponent languageComponent)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(this.XMLPath);
            }
            catch (Exception)
            {
                return null;
            }
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("field");
            ArrayList gbArray = new ArrayList();
            if (nodeList.Count > 0)
            {
                for (int i = 0; i < nodeList.Count; i++)
                {
                    XmlNode node = nodeList[i];
                    if (node.ParentNode.Attributes["name"].InnerText.ToString() == this.SelectedImportType)
                    {
                        if (node.ChildNodes.Count > 0)
                        {
                            for (int j = 0; j < node.ChildNodes.Count; j++)
                            {
                                string valuePre = "value";
                                if (languageComponent.Language.Equals("CHT"))
                                {
                                    valuePre += "_CHT";
                                }
                                if (languageComponent.Language.Equals("CHS"))
                                {
                                    valuePre += "_CHS";
                                }
                                if (languageComponent.Language.Equals("ENU"))
                                {
                                    valuePre += "_ENU";
                                }
                                ArrayList lineValues = new ArrayList();
                                string key = node.ChildNodes[j].Attributes["key"].InnerText.ToString();
                                string value = node.ChildNodes[j].Attributes[valuePre].InnerText.ToString();
                                DictionaryEntry de = new DictionaryEntry(key, value);
                                gbArray.Add(de);
                            }
                        }
                        break;
                    }
                }
            }
            return gbArray;
        }
	}
}

