using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Reflection;

using Infragistics.WebUI.UltraWebGrid ;
using Infragistics.WebUI.Shared ;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.OutSourcing
{

    /// <summary>
    /// FSMTLoadingMP 的摘要说明。
    /// </summary>
    public partial class FOutDataImport : BaseMPage
    {
    
        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputCheckBox Checkbox1;


		#region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {    
			this.components = new System.ComponentModel.Container();
			this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
			// 
			// languageComponent1
			// 
			this.languageComponent1.Language = "CHS";
			this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
			this.languageComponent1.RuntimePage = null;
			this.languageComponent1.RuntimeUserControl = null;
			this.languageComponent1.UserControlName = "";

		}
		#endregion

        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{	
				// 初始化页面语言
				this.InitPageLanguage(this.languageComponent1, false);
			}
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
			this.fileInit.Disabled = true;
			this.cmdView.Disabled = true;
			this.cmdEnter.Disabled = true;
			this.gridHelper.Grid.Columns.Clear();
			this.gridHelper.Grid.Rows.Clear();
			this.ViewState["GridColumn"] = null;
			
			if (ddlImportType.SelectedValue == string.Empty)
			{
				return;
			}
			string xmlPath = Server.MapPath("DataFileParserOutSourcing.xml");
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(xmlPath);
			XmlNode nodeFormat = xmlDoc.SelectSingleNode("//Format[@Name='" + ddlImportType.SelectedValue + "']");
			if (nodeFormat == null)
				return;
			XmlNode nodeObj = nodeFormat.SelectSingleNode("ObjectMap");
			ArrayList listColumn = new ArrayList();
			for (int i = 0; i < nodeObj.ChildNodes.Count; i++)
			{
				string strKey = string.Empty;
				string strCaption = string.Empty;
				if (nodeObj.ChildNodes[i].Attributes["AttributeName"] != null)
					strKey = nodeObj.ChildNodes[i].Attributes["AttributeName"].Value;
				if (nodeObj.ChildNodes[i].Attributes["Caption"] != null)
					strCaption = nodeObj.ChildNodes[i].Attributes["Caption"].Value;
				if (strKey != string.Empty)
				{
					this.gridHelper.AddColumn(strKey, strCaption);
					listColumn.Add(strKey);
				}
			}
			this.gridHelper.AddColumn("IsValid", "是否合法");
			this.ViewState["GridColumn"] = listColumn;
			this.gridHelper.AddDefaultColumn( false, false );
            this.gridHelper.ApplyLanguage( this.languageComponent1 );

			this.fileInit.Disabled = false;
			this.cmdView.Disabled = false;
        }
		
        #endregion

		protected void ddlImportType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			InitWebGrid();
		}

		#region Import
		private object[] items;
		protected void cmdView_ServerClick(object sender, System.EventArgs e)
		{
			string fileName = FileLoadProcess.UploadFile2ServerUploadFolder(this.Page,this.fileInit,null);
			if(fileName == null)
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileIsEmpty");
			}
			if(!fileName.ToLower().EndsWith(".csv"))
			{
				BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileTypeError");
			}

			this.ViewState.Add("UploadedFileName",fileName);
			this.cmdQuery_Click(null, null);
			if (this.gridWebGrid.Rows.Count > 0)
			{
				this.cmdEnter.Disabled = false;
			}
		}

		protected override object[] LoadDataSource(int inclusive, int exclusive)
		{
			ArrayList objs = new ArrayList() ;
			if(items == null)
			{
				this.GetAllItem();
			}
			for(int i=1;i<=items.Length;i++)
			{
				if(i>=inclusive && i<=exclusive)
				{
					objs.Add( items[i-1] );
				}
			}

			return objs.ToArray() ;

		}
		protected override int GetRowCount()
		{
			if(items == null)
			{
				this.GetAllItem();
			}
			return items.Length;
		}

		protected override Infragistics.WebUI.UltraWebGrid.UltraGridRow GetGridRow(object obj)
		{
			if (this.ViewState["GridColumn"] != null)
			{
				System.Type type = obj.GetType();
				ArrayList list = (ArrayList)this.ViewState["GridColumn"];
				object[] objRow = new object[list.Count + 1];
				for (int i = 0; i < list.Count; i++)
				{
					objRow[i] = GetFieldValue(obj, list[i].ToString());
				}
				objRow[objRow.Length - 1] = GetFieldValue(obj, "EAttribute1");
				return new UltraGridRow(objRow);
			}
			return null;
		}
		
		private object[] GetAllItem()
		{
			try
			{
				string fileName = string.Empty ;

				fileName = this.ViewState["UploadedFileName"].ToString() ;
	            
				string configFile = this.getParseConfigFileName() ;

				BenQGuru.eMES.Web.Helper.DataFileParser parser = new BenQGuru.eMES.Web.Helper.DataFileParser();
				parser.FormatName = this.ddlImportType.SelectedValue;
				parser.ConfigFile = configFile ;
				items = parser.Parse(fileName) ;
				ValidateItems();
			}
			catch
			{}

			return items ;

		}

		private string getParseConfigFileName()
		{
			string configFile = this.Server.MapPath(this.TemplateSourceDirectory )  ;
			if(configFile[ configFile.Length - 1 ] != '\\')
			{
				configFile += "\\" ;
			}
			configFile += "DataFileParserOutSourcing.xml" ;
			return configFile ;
		}

		private void ValidateItems()
		{
			if (items == null || items.Length == 0)
				return;
			string xmlPath = Server.MapPath("DataFileParserOutSourcing.xml");
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(xmlPath);
			XmlNode nodeFormat = xmlDoc.SelectSingleNode("//Format[@Name='" + ddlImportType.SelectedValue + "']");
			if (nodeFormat == null)
				return;
			XmlNode nodeObj = nodeFormat.SelectSingleNode("ObjectMap");
			string strTableName = nodeObj.Attributes["TableName"].Value;
			if (nodeObj.Attributes["KeyFields"] == null)
				return;
			string strKeyFields = nodeObj.Attributes["KeyFields"].Value;
			if (strKeyFields == string.Empty)
				return;
			string[] keyFields = strKeyFields.Split(',');
			Hashtable firstValueList = new Hashtable();
			for (int i = 0; i < items.Length; i++)
			{
				bool bResult = true;
				object item = items[i];
				string firstValue = GetFieldValue(item, keyFields[0]);
				if (firstValueList.ContainsKey(firstValue) == false)
				{
					string strSql = "SELECT " + strKeyFields + " FROM " + strTableName + " WHERE " + keyFields[0] + "='" + firstValue + "'";
					object[] objFirst = this.DataProvider.CustomQuery(item.GetType(), new SQLCondition(strSql));
					if (objFirst == null)
					{
						firstValueList.Add(firstValue, null);
					}
					else
					{
						ArrayList listTmp = new ArrayList();
						for (int n = 0; n < objFirst.Length; n++)
						{
							string strKeyValueList = string.Empty;
							for (int m = 0; m < keyFields.Length; m++)
							{
								strKeyValueList += GetFieldValue(objFirst[n], keyFields[m]) + "\t";
							}
							listTmp.Add(strKeyValueList);
						}
						firstValueList.Add(firstValue, listTmp);
					}
				}
				if (firstValueList[firstValue] != null)
				{
					if (keyFields.Length == 1)
					{
						bResult = false;
					}
					else
					{
						string currKeyValueList = string.Empty;
						for (int n = 0; n < keyFields.Length; n++)
						{
							currKeyValueList += GetFieldValue(item, keyFields[n]) + "\t";
						}
						ArrayList list = (ArrayList)firstValueList[firstValue];
						if (list.Contains(currKeyValueList) == true)
							bResult = false;
						else
							list.Add(currKeyValueList);
					}
				}
				SetFieldValue(item, "EAttribute1", bResult.ToString());
			}
		}

		protected void cmdImport_ServerClick(object sender, System.EventArgs e)
		{
			if(items==null)
			{
				items = GetAllItem() ;
				if (items == null)
					return;
			}
			if (items == null || items.Length == 0)
				return;
			FacadeHelper _facade = new FacadeHelper(base.DataProvider);
			string strIdBase = DateTime.Now.ToString("yyyyMMddHHmmss");
			System.Type type = items[0].GetType();
			FieldInfo piRnd = type.GetField("RndID");
			FieldInfo fiType = type.GetField("Type");
			FieldInfo impUser = type.GetField("ImportUser");
			FieldInfo impDate = type.GetField("ImportDate");
			FieldInfo impTime = type.GetField("ImportTime");
			FieldInfo endSn = type.GetField("EndSN");
			FieldInfo startSn = type.GetField("StartSN");
			string strUserCode = this.GetUserCode();
			int iDate = FormatHelper.TODateInt(DateTime.Today);
			int iTime = FormatHelper.TOTimeInt(DateTime.Now);
			string strType = string.Empty;
			if (ddlImportType.SelectedValue.EndsWith("Lot") == true)
				strType = "LOT";
			else
				strType = "PCS";
			for (int i = 0; i < items.Length; i++)
			{
				string strEAttribute = GetFieldValue(items[i], "EAttribute1");
				if (strEAttribute == string.Empty || Convert.ToBoolean(strEAttribute) == true)
				{
					try
					{
						if (piRnd != null)
							piRnd.SetValue(items[i], strIdBase + i.ToString().PadLeft(items.Length.ToString().Length - i.ToString().Length, '0'));
						if (impUser != null)
							impUser.SetValue(items[i], strUserCode);
						if (impDate != null)
							impDate.SetValue(items[i], iDate);
						if (impTime != null)
							impTime.SetValue(items[i], iTime);
						if (fiType != null)
							fiType.SetValue(items[i], strType);
						if (endSn != null && startSn != null)
						{
							object objTmp = endSn.GetValue(items[i]);
							if (objTmp == null || objTmp == DBNull.Value || objTmp.ToString() == string.Empty)
							{
								endSn.SetValue(items[i], startSn.GetValue(items[i]));
							}
						}
						_facade.AddDomainObject((DomainObject)items[i]);
					}
					catch
					{}
				}
			}
			
			string strMessage = languageComponent1.GetString("$CycleImport_Success");
			string alertInfo = 
				string.Format("<script language=javascript>alert('{0}');</script>", strMessage);
			if( !this.IsClientScriptBlockRegistered("ImportAlert") )
			{
				this.RegisterClientScriptBlock("ImportAlert", alertInfo);	
			}
			items = null;
			this.cmdEnter.Disabled = true;
		}

		private void cmdReturn_ServerClick(object sender, EventArgs e)
		{
		}

		private string GetFieldValue(object obj, string fieldName)
		{
			FieldInfo fi = obj.GetType().GetField(fieldName);
			if (fi == null)
				return string.Empty;
			return fi.GetValue(obj).ToString();
		}
		private void SetFieldValue(object obj, string fieldName, string value)
		{
			FieldInfo fi = obj.GetType().GetField(fieldName);
			if (fi != null)
				fi.SetValue(obj, value);
		}

		#endregion

    }
}
