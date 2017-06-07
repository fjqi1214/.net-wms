using System;
using System.IO;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Reflection;
using System.Globalization;
using ADODB;

using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Web.Helper
{
	/// <summary>
	/// OWCPivotTable 的摘要说明。
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:OWCPivotTable runat=server></{0}:OWCPivotTable>")]
	public class OWCPivotTable : System.Web.UI.WebControls.WebControl
	{
		protected override void Render(System.Web.UI.HtmlTextWriter writer)
		{
			writer.Write( string.Format(this._pivotTableHtml, this.PivotTableName,
				string.Format("{0} {1}", 
				this.Width == Unit.Empty ? "" : "width='" + this.Width.Value + "'",
				this.Height == Unit.Empty ? "" : "height='" + this.Height.Value + "'"),
				this.Display ? "block" : "none"));

			writer.Write( this.SpellScript() );
			writer.Write( string.Format("<script language=vbscript>Load_{0}()</script>\n", this.PivotTableName) );
			writer.Write( this._exportScript );
		} 

		#region 公共属性
		public string PivotTableName
		{
			get
			{
				return this.ID + "_PivotTable";
			}
		}

		public string XmlFileName
		{
			get
			{
				if ( this.ViewState["$FileName"] == null )
				{
					this.ViewState["$FileName"] = string.Empty;
				}

				return this.ViewState["$FileName"].ToString();
			}
			set
			{
				this.ViewState["$FileName"] = value;
			}
		}

		public string DownloadDirectory
		{
			get
			{
				if ( this.ViewState["$Directory"] == null )
				{
					this.ViewState["$Directory"] = "upload";
				}

				if ( this.ViewState["$Directory"].ToString().Trim() == string.Empty )
				{
					this.ViewState["$Directory"] = ".";
				}

				return this.ViewState["$Directory"].ToString();
			}
			set
			{
				this.ViewState["$Directory"] = value;
			}
		}

		public string DownloadPhysicalPath
		{
			get
			{
				return string.Format( @"{0}\{1}\", this.Page.Request.PhysicalApplicationPath, this.DownloadDirectory.Trim('\\','/').Replace('/','\\')  );
			}
		}
		
		private string VirtualHostRoot
		{
			get
			{
				return string.Format("{0}{1}"
					, this.Page.Request.Url.Segments[0]
					, this.Page.Request.Url.Segments[1]);
			}
		}

		public string DownloadPath
		{
			get
			{
				return string.Format( @"{0}{1}/", this.VirtualHostRoot, this.DownloadDirectory.Trim('\\','/').Replace('\\','/') );
			}
		}

		public Hashtable RowFieldSetList
		{
			get
			{
				if ( this.ViewState["$RowFieldSetList"] == null )
				{
					this.ViewState["$RowFieldSetList"] = new Hashtable();
				}

				return (Hashtable)this.ViewState["$RowFieldSetList"];
			}
			set
			{
				this.ViewState["$RowFieldSetList"] = value;
			}
		}

		public Hashtable ColumnFieldSetList
		{
			get
			{
				if ( this.ViewState["$ColumnFieldSetList"] == null )
				{
					this.ViewState["$ColumnFieldSetList"] = new Hashtable();
				}

				return (Hashtable)this.ViewState["$ColumnFieldSetList"];
			}
			set
			{
				this.ViewState["$ColumnFieldSetList"] = value;
			}
		}

		public Hashtable FilterFieldSetList
		{
			get
			{
				if ( this.ViewState["$FilterFieldSetList"] == null )
				{
					this.ViewState["$FilterFieldSetList"] = new Hashtable();
				}

				return (Hashtable)this.ViewState["$FilterFieldSetList"];
			}
			set
			{
				this.ViewState["$FilterFieldSetList"] = value;
			}
		}

		public Hashtable TotalFieldList
		{
			get
			{
				if ( this.ViewState["$TotalFieldList"] == null )
				{
					this.ViewState["$TotalFieldList"] = new Hashtable();
				}

				return (Hashtable)this.ViewState["$TotalFieldList"];
			}
			set
			{
				this.ViewState["$TotalFieldList"] = value;
			}
		}

		public bool Display
		{
			get
			{
				if ( this.ViewState["$Display"] == null )
				{
					this.ViewState["$Display"] = true;
				}

				return (bool)this.ViewState["$Display"];
			}
			set
			{
				this.ViewState["$Display"] = value;
			}
		}		

		public ControlLibrary.Web.Language.LanguageComponent LanguageComponent
		{
			get
			{
				return _languageComponent;
			}
			set
			{
				_languageComponent = value;
			}
		}

		#endregion

		#region 公共方法
		public void SetDataSource( DataSet ds )
		{
			this.SaveDataSetToXml( ds );
		}

		public void SetDataSource( object[] objs, string[] schema )
		{
			this.SaveDomainObjectToXml( objs, schema );
		}

		private void AddFieldSet( string fieldName, bool expanded, string axisType, string numberFormat)
		{
			if ( axisType == AxisType.Row )
			{
				this._rowFieldNameList.Add( fieldName );
				this.RowFieldSetList.Add( fieldName, new FieldSet( this.getFieldSetCaption(fieldName), fieldName, expanded, numberFormat) );
			}
			if ( axisType == AxisType.Column )
			{
				this._colFieldNameList.Add( fieldName );
				this.ColumnFieldSetList.Add( fieldName, new FieldSet( this.getFieldSetCaption(fieldName), fieldName, expanded, numberFormat) );
			}
			if ( axisType == AxisType.Filter )
			{
				this._filterFieldNameList.Add( fieldName );
				this.FilterFieldSetList.Add( fieldName, new FieldSet( this.getFieldSetCaption(fieldName), fieldName, expanded, numberFormat) );
			}
		}

		public void AddRowFieldSet( string fieldName, bool expanded )
		{
			this.AddFieldSet( fieldName, expanded, AxisType.Row, "" );
		}

		public void AddColumnFieldSet( string fieldName, bool expanded )
		{
			this.AddFieldSet( fieldName, expanded, AxisType.Column, "");
		}

		public void AddFilterFieldSet( string fieldName, bool expanded )
		{
			this.AddFieldSet( fieldName, expanded, AxisType.Filter, "");
		}

		public void AddTotalField( string caption, string fieldName, string pivotTotalFunctionType)
		{
			this._totalFieldNameList.Add( caption );
			this.TotalFieldList.Add( caption, new TotalField(caption, fieldName, pivotTotalFunctionType) );
		}

		public void AddCalculatedTotalField( string caption, string fieldName, string expression, string pivotTotalFunctionType )
		{
			this._totalFieldNameList.Add( caption );
			this.TotalFieldList.Add( caption, new CalculatedField(caption, fieldName, expression, pivotTotalFunctionType) );
		}
		
		public void AddRowFieldSet( string fieldName, bool expanded, string numberFormat )
		{
			this.AddFieldSet( fieldName, expanded, AxisType.Row, numberFormat );
		}

		public void AddColumnFieldSet( string fieldName, bool expanded, string numberFormat )
		{
			this.AddFieldSet( fieldName, expanded, AxisType.Column, numberFormat);
		}

		public void AddFilterFieldSet( string fieldName, bool expanded, string numberFormat )
		{
			this.AddFieldSet( fieldName, expanded, AxisType.Filter, numberFormat);
		}

		public void AddTotalField( string caption, string fieldName, string pivotTotalFunctionType, string numberFormat)
		{
			this._totalFieldNameList.Add( caption );
			this.TotalFieldList.Add( caption, new TotalField(caption, fieldName, pivotTotalFunctionType, numberFormat) );
		}
		
		public void AddCalculatedTotalField( string caption, string fieldName, string expression, string pivotTotalFunctionType, string numberFormat )
		{
			this._totalFieldNameList.Add( caption );
			this.TotalFieldList.Add( caption, new CalculatedField(caption, fieldName, expression, pivotTotalFunctionType, numberFormat) );
		}

		public void RemoveRowFieldSet( string fieldName )
		{
			this.RowFieldSetList.Remove( fieldName );	
			this._rowFieldNameList.Remove( fieldName );
		}

		public void RemoveColumnFieldSet( string fieldName )
		{
			this.ColumnFieldSetList.Remove( fieldName );
			this._colFieldNameList.Remove( fieldName );
		}

		public void RemoveFilterFieldSet( string fieldName )
		{
			this.FilterFieldSetList.Remove( fieldName );
			this._filterFieldNameList.Remove( fieldName );
		}

		public void RemoveTotalField( string name )
		{	
			this.TotalFieldList.Remove( name );
			this._totalFieldNameList.Remove( name );
		}

		public void RemoveAllRowFieldSet()
		{
			this.RowFieldSetList.Clear();
			this._rowFieldNameList.Clear();
		}

		public void RemoveAllColumnFieldSet()
		{
			this.ColumnFieldSetList.Clear();
			this._colFieldNameList.Clear();
		}

		public void RemoveAllFilterFieldSet()
		{
			this.FilterFieldSetList.Clear();
			this._filterFieldNameList.Clear();
		}

		public void RemoveAllTotalField( )
		{
			this.TotalFieldList.Clear();
			this._totalFieldNameList.Clear();
		}

		public void ClearFieldSet()
		{
			this.RemoveAllRowFieldSet();
			this.RemoveAllColumnFieldSet();
			this.RemoveAllFilterFieldSet();
			this.RemoveAllTotalField();
		}

		public void ExportExcel(string filePath, bool openInExcel)
		{
			this._exportScript = string.Format( "<script language=vbscript>Document.Forms(0).{1}.Export \"{0}\", Document.Forms(0).{1}.Constants.{2}</script>\n", 
				filePath, 
				this.PivotTableName, 
				openInExcel ? "plExportActionOpenInExcel" : "plExportActionNone");
		}

		public void ExportExcel(bool openInExcel)
		{
			this._exportScript = string.Format( 
@"<script language=javascript>
	var file = window.prompt(""请输入导出文件路径:"",""c:\\{0}.xls"");
	if (file.length <= 0) 
		alert(""路径为空，请重新导出!"");
	else
		document.forms(0).{1}.Export(file, document.forms(0).{1}.Constants.{2});
	</script>
", 
												this.XmlFileName, 
												this.PivotTableName, 
												openInExcel ? "plExportActionOpenInExcel" : "plExportActionNone");
		}
		#endregion

		#region 私有变量
		private ArrayList _rowFieldNameList
		{
			get
			{
				if ( this.ViewState["$RowFieldNameList"] == null )
				{
					this.ViewState["$RowFieldNameList"] = new ArrayList();
				}

				return (ArrayList)this.ViewState["$RowFieldNameList"];
			}
			set
			{
				this.ViewState["$RowFieldNameList"] = value;
			}
		}

		private ArrayList _colFieldNameList
		{
			get
			{
				if ( this.ViewState["$ColFieldNameList"] == null )
				{
					this.ViewState["$ColFieldNameList"] = new ArrayList();
				}

				return (ArrayList)this.ViewState["$ColFieldNameList"];
			}
			set
			{
				this.ViewState["$ColFieldNameList"] = value;
			}
		}

		private ArrayList _filterFieldNameList
		{
			get
			{
				if ( this.ViewState["$FilterFieldNameList"] == null )
				{
					this.ViewState["$FilterFieldNameList"] = new ArrayList();
				}

				return (ArrayList)this.ViewState["$FilterFieldNameList"];
			}
			set
			{
				this.ViewState["$FilterFieldNameList"] = value;
			}
		}

		private ArrayList _totalFieldNameList
		{
			get
			{
				if ( this.ViewState["$TotalFieldNameList"] == null )
				{
					this.ViewState["$TotalFieldNameList"] = new ArrayList();
				}

				return (ArrayList)this.ViewState["$TotalFieldNameList"];
			}
			set
			{
				this.ViewState["$TotalFieldNameList"] = value;
			}
		}

		private ControlLibrary.Web.Language.LanguageComponent _languageComponent = null;

		private string _script = @"
<script language=vbscript>			
	Sub Load_{0}()
{1}
	End Sub
</script>
";
		
		private string _pivotTableHtml = @"
		<div style='display: {2}'>
			<OBJECT id='{0}' {1} classid=clsid:0002E552-0000-0000-C000-000000000046 VIEWASTEXT>
				<PARAM NAME='XMLData' VALUE='<xml xmlns:x='urn:schemas-microsoft-com:office:excel'>&#13;&#10; <x:PivotTable>&#13;&#10;  <x:OWCVersion>10.0.0.5605         </x:OWCVersion>&#13;&#10;  <x:DisplayScreenTips/>&#13;&#10;  <x:CubeProvider>msolap.2</x:CubeProvider>&#13;&#10;  <x:CacheDetails/>&#13;&#10;  <x:PivotView>&#13;&#10;   <x:IsNotFiltered/>&#13;&#10;  </x:PivotView>&#13;&#10; </x:PivotTable>&#13;&#10;</xml>'>
			</OBJECT>
		<div>";

		private string _loadSubScript = @"
		{0}.ConnectionString = ""provider=mspersist""
		{0}.CommandText	= ""{1}""
        {0}.DisplayToolbar = false
        {0}.Commands(12009).Execute
        {0}.Commands(12051).Execute
        {0}.ActiveView.TitleBar.Visible = false
        {0}.ActiveView.TotalOrientation = {0}.Constants.plTotalOrientationRow
		{0}.ActiveView.AutoLayout
		{0}.ActiveView.ExpandDetails = {0}.Constants.plExpandNever

";

		private string _exportScript = string.Empty;
		#endregion

		#region 私有方法
		private string SpellScript()
		{
			StringBuilder builder = new StringBuilder("");

			if ( this.XmlFileName != string.Empty )
			{
				builder.Append( string.Format( this._loadSubScript, 
					"Document.Forms(0)." + this.PivotTableName, 
					this.DownloadPath + this.XmlFileName + ".xml") );
			
				builder.Append(	this.SpellInsertFieldSetScript() );
				builder.Append( this.SpellAddTotalScript() );
				builder.Append( this.SpellAttributesScript() );
			}

			return string.Format( this._script, this.PivotTableName, builder.ToString() );
		}

		private string SpellInsertFieldSetScript()
		{
			StringBuilder builder = new StringBuilder("\n");

			foreach ( string name in this._rowFieldNameList )
			{
				this.SpellFieldSetScript( AxisType.Row, (FieldSet)this.RowFieldSetList[name], builder );
			}

			foreach ( string name in this._colFieldNameList )
			{
				this.SpellFieldSetScript( AxisType.Column, (FieldSet)this.ColumnFieldSetList[name], builder );
			}

			foreach ( string name in this._filterFieldNameList )
			{
				this.SpellFieldSetScript( AxisType.Filter, (FieldSet)this.FilterFieldSetList[name], builder );
			}

			return builder.ToString();
		}

		private void SpellFieldSetScript( string axisType, FieldSet fieldSet, StringBuilder builder )
		{
			builder.Append( string.Format("		{0}.ActiveView.{1}.InsertFieldSet({0}.ActiveView.Fieldsets(\"{2}\"))\n", 
				"Document.Forms(0)." + this.PivotTableName,
				axisType,
				fieldSet.FieldName) );
	
			foreach ( string key in fieldSet.Attributes.Keys )
			{
				builder.Append( string.Format("		{0}.ActiveView.Fieldsets(\"{1}\").Fields(0).{2} = \"{3}\"\n", 
					"Document.Forms(0)." + this.PivotTableName,
					fieldSet.FieldName,
					key,
					fieldSet.Attributes[key].ToString() ) );	
			}

			builder.Append("\n");
		}

		private string SpellAddTotalScript()
		{
			StringBuilder builder = new StringBuilder("\n");
			TotalField totalField = null;

			foreach ( string name in this._totalFieldNameList )
			{
				totalField = (TotalField)this.TotalFieldList[name];
				
				if ( totalField is CalculatedField )
				{
					/*在数据透视表中添加计算总计。使用 AddCalculatedTotal 方法创建基于数据透视表中定义的总计的自定义总计。计算总计返回一个 PivotTotal 对象。
					 * expression.AddCalculatedTotal(Name, Caption, Expression, SolveOrder)
					 * expression      必需。该表达式返回一个 PivotView 对象。
					 * Name     String 类型，必需。用于在 PivotTotals 集合中标识新计算总计。该参数在 PivotTotals 集合中必须唯一。长度必须在 1 到 50 个字符之间。
					 * Caption     String 类型，必需。用于在数据透视表用户界面中标识新计算总计。
					 * Expression     String 类型，必需。该表达式用于计算新计算总计。必须为访问数据所用的 OLE DB 提供者的有效多维表达式 (MDX) 语句。
					 * SolveOrder     Long 类型，可选。表示在刷新数据透视表时新计算总计的求解次序。如果创建的计算总计取决于前面创建的计算总计，则 SolveOrder 参数非常有用。
					 * */
					builder.Append( string.Format("	{0}.ActiveView.DataAxis.InsertTotal ( {0}.ActiveView.AddCalculatedTotal ( \"{1}\", \"{2}\",\"{3}\" ) )\n", 
													"Document.Forms(0)." + this.PivotTableName,							
													((CalculatedField)totalField).FieldName,
													((CalculatedField)totalField).Caption,
													((CalculatedField)totalField).Expression 													
													));
				}

				if ( !(totalField is CalculatedField) )
				{
                    builder.Append(string.Format("		{0}.ActiveView.DataAxis.InsertTotal( {0}.ActiveView.AddTotal(\"{1}\", {0}.ActiveView.Fieldsets(\"{2}\").Fields(0), {0}.Constants.{3}) )\n",
						"Document.Forms(0)." + this.PivotTableName,
						totalField.Caption,
						totalField.FieldName,
						totalField.FunctionType) );
				}

				foreach ( string key in totalField.Attributes.Keys )
				{
					builder.Append( string.Format("		{0}.ActiveView.Totals(\"{1}\").{2} = \"{3}\"\n\n", 
						"Document.Forms(0)." + this.PivotTableName,
						totalField.Caption,
						key,
						totalField.Attributes[key].ToString() ) );
					
				}
			}

			return builder.ToString();
		}

		private string SpellAttributesScript()
		{
			StringBuilder builder = new StringBuilder("\n");

			foreach ( string key in this.Attributes.Keys )
			{
				builder.Append( string.Format("		{0}.{1} = \"{2}\"\n", "Document.Forms(0)." + this.PivotTableName, key, this.Attributes[key]) );
			}

			return builder.ToString();
		}
		
		private string getFieldSetCaption( string fieldName )
		{
			if ( this.LanguageComponent != null )
			{
				string caption = this.LanguageComponent.GetString(fieldName);

				if ( caption != string.Empty )
				{
					return caption;
				}
			}

			return fieldName;
		}

		#region DataSet --> RecordSet --> XML
		private void SaveDataSetToXml(DataSet ds)
		{			
			this.XmlFileName = string.Format("PivotTable_{0}_{1}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString() );
			
			if ( !Directory.Exists(this.DownloadPhysicalPath) )
			{
				Directory.CreateDirectory( this.DownloadPhysicalPath );
			}

			while ( File.Exists(this.DownloadPhysicalPath + this.XmlFileName + ".xml") )
			{
				this.XmlFileName = string.Format( "{0}_1", this.XmlFileName );
			}

			string path = this.DownloadPhysicalPath + this.XmlFileName + ".xml" ;
			
			Recordset rs = CreateADODBRecordSet(ds);
			rs.Save (path, PersistFormatEnum.adPersistXML);
			rs.Close();
		}

		private Recordset CreateADODBRecordSet(DataSet ds)
		{
			Recordset oRS = new Recordset();
			try
			{
				//oDataTable = CreateDataTable();
				//Loop through each column in the Dataset
				foreach(DataColumn oColumn in ds.Tables[0].Columns)
				{
					//Create the Field Types for the recordset
					
					oRS.Fields.Append(oColumn.ColumnName
						,GetADOType(oColumn.DataType.ToString())
						//,oColumn.MaxLength  
						,GetADOTypeSize(oColumn.DataType.ToString())
						,FieldAttributeEnum.adFldIsNullable
						,System.Reflection.Missing.Value);
				}

				//Open the recordset
				oRS.Open(System.Reflection.Missing.Value,
					System.Reflection.Missing.Value, CursorTypeEnum.adOpenKeyset,
					LockTypeEnum.adLockOptimistic, 1);
				//Loop through the table and fill the ADO Recordset
				for(int i = 0; i < ds.Tables[0].Rows.Count; i ++)
				{
					oRS.AddNew(System.Reflection.Missing.Value,
						System.Reflection.Missing.Value);
					//Loop through each column
					for(int j = 0; j < ds.Tables[0].Columns.Count; j ++)
					{
						oRS.Fields[j].Value = ds.Tables[0].Rows[i][j];
					}
					oRS.Update(System.Reflection.Missing.Value,
						System.Reflection.Missing.Value);
				}
				//Move to the first record
				oRS.MoveFirst();
			}
			catch
			{
				oRS.MoveFirst();
			}
			finally
			{
			}

			return oRS;

		}
		/// <summary>
		/// Return the ADO Data Type
		/// </summary>
		/// <remarks></remarks>
		/// <param name="sType"></param>
		/// <returns>Integer ADO Data Type</returns>
		private DataTypeEnum GetADOType(string sType)
		{
			switch(sType)
			{
				case null:
					//adEmpty 0
					return DataTypeEnum.adEmpty;
				case "System.SByte":
					//adTinyInt 16
					return DataTypeEnum.adTinyInt;
				case "System.Boolean":
					//adBoolean 11
					return DataTypeEnum.adBoolean;
				case "System.Int16":
					//adSmallInt 2
					return DataTypeEnum.adSmallInt;
				case "System.Int32":
					//adInteger 3
					return DataTypeEnum.adInteger;
				case "System.Int64":
					//adBigInt 20
					return DataTypeEnum.adBigInt;
				case "System.Single":
					//adSingle 4
					return DataTypeEnum.adSingle;
				case "System.Double":
					//adDouble 5
					return DataTypeEnum.adDouble;
				case "System.Decimal":
					//adDecimal 14
					return DataTypeEnum.adDecimal;
				case "System.DateTime":
					//adDate 7
					return DataTypeEnum.adDate;
				case "System.Guid":
					//adGUID 72
					return DataTypeEnum.adGUID;
				case "System.String":
					//adChar 129
					return DataTypeEnum.adChar;
				case "System.byte[]":
					//adBinary
					return DataTypeEnum.adBinary;
				default:
					return 0;
			}
		}

		private int GetADOTypeSize(string sType)
		{
			switch(sType)
			{
				case null:
					//adEmpty 0
					return -1;
				case "System.SByte":
					//adTinyInt 16
					return -1;
				case "System.Boolean":
					//adBoolean 11
					return -1;
				case "System.Int16":
					//adSmallInt 2
					return -1;
				case "System.Int32":
					//adInteger 3
					return -1;
				case "System.Int64":
					//adBigInt 20
					return -1;
				case "System.Single":
					//adSingle 4
					return -1;
				case "System.Double":
					//adDouble 5
					return -1;
				case "System.Decimal":
					//adDecimal 14
					return -1;
				case "System.DateTime":
					//adDate 7
					return -1;
				case "System.Guid":
					//adGUID 72
					return -1;
				case "System.String":
					//adChar 129
					//return 32767;
					return 50;
				case "System.byte[]":
					//adBinary
					//return 32767;
					return 50;
				default:
					return 1;
			}
		}
		#endregion

		#region DomainObject --> XML
		private XmlNode DomainObjectDataToXml(XmlDocument doc, object obj, string[] schema)
		{
			System.Xml.XmlElement element = doc.CreateElement("z","row","#RowsetSchema");

			object value = null;

			foreach ( string memberName in schema )
			{
				value = DomainObjectUtility.GetValue( obj ,memberName, null);

				if ( value == null )
				{
					element.SetAttribute( memberName, string.Empty.PadRight(40,' ') );
				}
				else
				{
					element.SetAttribute( memberName, DomainObjectUtility.XMLEncodeValue( value.GetType(), value).PadRight(40,' ') );
				}
			}

			return element;
		}

		private XmlNode DomainObjectDataToXml(XmlDocument doc, object[] objs, string[] schema)
		{
			System.Xml.XmlElement element = doc.CreateElement("rs","data", "urn:schemas-microsoft-com:rowset");

			foreach ( DomainObject obj in objs )
			{
				element.AppendChild( DomainObjectDataToXml(doc, obj, schema) );
			}

			return element;
		}

		private XmlNode DomainObjectSchemaToXml(XmlDocument doc, Type type, string[] schema)
		{
			System.Xml.XmlElement schemaElement = doc.CreateElement("s","Schema","uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			schemaElement.SetAttribute( "id", "RowsetSchema" );
			
			System.Xml.XmlElement typeElement = doc.CreateElement("s","ElementType","uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			typeElement.SetAttribute( "name", "row");
			typeElement.SetAttribute( "content", "eltOnly");
			typeElement.SetAttribute( "updatable", "urn:schemas-microsoft-com:rowset", "true");

			System.Xml.XmlElement attrElement = null;
			System.Xml.XmlElement dataElement = null;
			System.Xml.XmlElement extendsElement = null;

	   		extendsElement = doc.CreateElement("s","extends","uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			extendsElement.SetAttribute("type", "rs:rowbase" );

			int i = 1;
			string name = string.Empty;
			string dbtype = string.Empty;

			foreach( string memberName in schema )
			{
				MemberInfo[] infos = type.GetMember(memberName);

				if ( infos == null )
				{
					continue;
				}

				Attribute attr = System.Attribute.GetCustomAttribute(infos[0], typeof(FieldMapAttribute));

				if ( attr == null )
				{
					continue;
				}

				attrElement = doc.CreateElement("s","AttributeType","uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");


				attrElement.SetAttribute("name", memberName );
				attrElement.SetAttribute("number", "urn:schemas-microsoft-com:rowset", i.ToString() );
				attrElement.SetAttribute("nullable", "urn:schemas-microsoft-com:rowset", "true");
				attrElement.SetAttribute("write", "urn:schemas-microsoft-com:rowset","true");
 
				dataElement = doc.CreateElement("s","datatype","uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
				dataElement.SetAttribute("type", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", this.GetXmlType(((FieldMapAttribute)attr).DataType));
				
				dbtype = this.GetXmlDbType(((FieldMapAttribute)attr).DataType);
				if ( dbtype != "" )
				{
					dataElement.SetAttribute("dbtype", "urn:schemas-microsoft-com:rowset", dbtype );
				}

				dataElement.SetAttribute("maxLength", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882",	((FieldMapAttribute)attr).Size.ToString()); 
				dataElement.SetAttribute("precision", "urn:schemas-microsoft-com:rowset", ((FieldMapAttribute)attr).Scale.ToString()); 
				dataElement.SetAttribute("fixedlength", "urn:schemas-microsoft-com:rowset", "true"); 
				dataElement.SetAttribute("maybenull", "urn:schemas-microsoft-com:rowset", "false");

				attrElement.AppendChild( dataElement );
				typeElement.AppendChild( attrElement );
				typeElement.AppendChild( extendsElement );	
			
				i++;
			}	

			schemaElement.AppendChild( typeElement );
	
			return schemaElement;
		}

		private XmlNode DomainObjectToXmlNode(Type type, object[] objs, string[] schema)
		{
			XmlDocument doc = new XmlDocument();
			System.Xml.XmlElement rootElement = doc.CreateElement("xml");
			rootElement.SetAttribute( "xmlns:s", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882" );
			rootElement.SetAttribute( "xmlns:dt", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" );
			rootElement.SetAttribute( "xmlns:rs", "urn:schemas-microsoft-com:rowset" );
			rootElement.SetAttribute( "xmlns:z", "#RowsetSchema" );

			rootElement.AppendChild( DomainObjectSchemaToXml(doc, type, schema) );

			rootElement.AppendChild( DomainObjectDataToXml(doc, objs, schema) );

			return rootElement;
		}

		private void SaveDomainObjectToXml(object[] objs, string[] schema)
		{
			if ( objs == null || objs.Length == 0)
			{
				this.XmlFileName = string.Empty;
				return;
			}

			this.XmlFileName = string.Format("PivotTable_{0}_{1}", FormatHelper.TODateInt(System.DateTime.Now).ToString(), FormatHelper.TOTimeInt(System.DateTime.Now).ToString() );
			
			if ( !Directory.Exists(this.DownloadPhysicalPath) )
			{
				Directory.CreateDirectory( this.DownloadPhysicalPath );
			}

			while ( File.Exists(this.DownloadPhysicalPath + this.XmlFileName + ".xml") )
			{
				this.XmlFileName = string.Format( "{0}_1", this.XmlFileName );
			}

//			Recordset rs = CreateADODBRecordSet( objs, schema );
//			rs.Save ( this.DownloadPhysicalPath + this.XmlFileName + ".xml", PersistFormatEnum.adPersistXML);
//			rs.Close();

			FileStream stream = new FileStream( this.DownloadPhysicalPath + this.XmlFileName + ".xml", FileMode.Create );
			XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.GetEncoding("GB2312"));

			writer.Formatting = Formatting.Indented;
			writer.WriteStartDocument();

			DomainObjectToXmlNode(objs[0].GetType(), objs, schema).WriteTo(writer);

			writer.WriteEndDocument();
			writer.Flush();	
			stream.Close();
		}

		private Recordset CreateADODBRecordSet(object[] objs, string[] schema)
		{
			Recordset oRS = new Recordset();
			try
			{
				//oDataTable = CreateDataTable();
				//Loop through each column in the Dataset
				foreach( string memberName in schema )
				{
					MemberInfo[] infos = objs[0].GetType().GetMember(memberName);

					if ( infos == null )
					{
						continue;
					}
					
					Attribute attr = System.Attribute.GetCustomAttribute(infos[0], typeof(FieldMapAttribute));
					
					oRS.Fields.Append(memberName
						,GetADOType(((FieldMapAttribute)attr).DataType.ToString())
						//,oColumn.MaxLength  
						,((FieldMapAttribute)attr).Size
//						,GetADOTypeSize(oColumn.DataType.ToString())
						,((FieldMapAttribute)attr).AllowNull ? FieldAttributeEnum.adFldMayBeNull : FieldAttributeEnum.adFldIsNullable
						,System.Reflection.Missing.Value);
				}

				//Open the recordset
				oRS.Open(System.Reflection.Missing.Value,
					System.Reflection.Missing.Value, CursorTypeEnum.adOpenKeyset,
					LockTypeEnum.adLockOptimistic, 1);
				//Loop through the table and fill the ADO Recordset
				
				for(int i = 0; i < objs.Length; i ++)
				{
					oRS.AddNew(System.Reflection.Missing.Value,
						System.Reflection.Missing.Value);
					//Loop through each column
					for(int j = 0; j < schema.Length; j ++)
					{
						oRS.Fields[j].Value = DomainObjectUtility.GetValue( objs[i] ,schema[j], null);
					}
					oRS.Update(System.Reflection.Missing.Value,
						System.Reflection.Missing.Value);
				}
				//Move to the first record
				oRS.MoveFirst();
			}
			catch
			{
				return null;
			}
			finally
			{
			}

			return oRS;

		}

		private string GetXmlType( Type type )
		{
			switch ( type.ToString() )
			{
				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
					return "number";
				case "System.String":
					return "string";
				case "System.Decimal":
				case "System.Single":
					return "float";
				case "System.Double":
					return "double";
			}

			return "string";
		}

		private string GetXmlDbType( Type type )
		{
			switch ( type.ToString() )
			{
				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
					return "int";
				case "System.Decimal":
				case "System.Single":
				case "System.Double":
					return "";
				case "System.String":
					return "str";
			}

			return "str";
		}
		#endregion

		#endregion
	}

	#region FieldSet
//	[TypeConverter(typeof(FieldSetConverter))]
	[Serializable]
	public class FieldSet
	{
		public FieldSet( string fieldName )
		{
			this._fieldName = fieldName;
		}

		public FieldSet( string caption, string fieldName, bool expanded ) : this(fieldName)
		{
			this.Attributes["Caption"]	 = caption;
			this.Attributes["Expanded"]  = expanded.ToString().ToLower();
		}

		public FieldSet( string caption, string fieldName, bool expanded, string numberFormat ) : this(caption, fieldName, expanded)
		{
			if ( numberFormat != string.Empty )
			{
				this.Attributes["NumberFormat"] = numberFormat;
			}
		}

		private string _fieldName = string.Empty;
		public Hashtable Attributes = new Hashtable();

		public string FieldName
		{
			get
			{
				return this._fieldName;
			}
		}
	}

	public class FieldSetConverter : TypeConverter 
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
		{
			if (sourceType == typeof(string)) 
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
		{
			if (value is string) 
			{
				string[] attrs = ((string)value).Split(new char[] {','});
				string[] v  = null;

				FieldSet fieldSet = new FieldSet( attrs[0] );

				for ( int i=1; i<attrs.Length; i++ )
				{
					v = attrs[i].Split('=');
					fieldSet.Attributes.Add( v[0], v[1] );
				}

				return fieldSet;
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
		{  
			if (destinationType == typeof(string)) 
			{
				StringBuilder builder = new StringBuilder();

				builder.Append( ((FieldSet)value).FieldName + "," );

				foreach ( string key in ((FieldSet)value).Attributes.Keys )
				{
					builder.Append(	string.Format("{0}={1},", key, ((FieldSet)value).Attributes[key].ToString()) );
				}

				return builder.ToString(0, builder.Length-1);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
	#endregion

	#region TotalField
//	[TypeConverter(typeof(TotalFieldConverter))]
	[Serializable]
	public class TotalField
	{
		public TotalField()
		{
		}

		public TotalField(string caption, string fieldName, string pivotTotalFunctionType)
		{
			this._caption	  = caption;
			this.FieldName	  = fieldName;
			this.FunctionType = pivotTotalFunctionType;
		}

		public TotalField(string caption, string fieldName, string pivotTotalFunctionType, string numberFormat) : this(caption, fieldName, pivotTotalFunctionType)
		{
			if ( numberFormat != string.Empty )
			{
				this.Attributes["NumberFormat"] = numberFormat;
			}
		}

		public string _caption		= "";
		public string FieldName		= "";	
		public string FunctionType	= "";
		public Hashtable Attributes = new Hashtable();

		public string Caption
		{
			get
			{
				return this._caption;
			}
		}
	}

	public class TotalFieldConverter : TypeConverter 
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
		{
			if (sourceType == typeof(string)) 
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
		{
			if (value is string) 
			{
				string[] attrs = ((string)value).Split(new char[] {','});
				string[] v  = null;

				TotalField field = new TotalField( attrs[0], attrs[1], attrs[2] );

				for ( int i=3; i<attrs.Length; i++ )
				{
					v = attrs[i].Split('=');
					field.Attributes.Add( v[0], v[1] );
				}

				return field;
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
		{  
			if (destinationType == typeof(string)) 
			{
				StringBuilder builder = new StringBuilder();

				builder.Append( ((TotalField)value).Caption + "," );
				builder.Append( ((TotalField)value).FieldName + "," );
				builder.Append( ((TotalField)value).FunctionType + "," );

				foreach ( string key in ((TotalField)value).Attributes.Keys )
				{
					builder.Append(	string.Format("{0}={1},", key, ((TotalField)value).Attributes[key].ToString()) );
				}

				return builder.ToString(0, builder.Length-1);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
	#endregion

	#region CalculatedField
//	[TypeConverter(typeof(CalculatedFieldConverter))]
	[Serializable]
	public class CalculatedField : TotalField
	{
		public string Expression = string.Empty;

		public CalculatedField() : base()
		{
		}

		public CalculatedField( string caption, string fieldName, string expression, string pivotTotalFunctionType ) : base(caption, fieldName, pivotTotalFunctionType)
		{
			this.Expression = expression;
		}

		public CalculatedField( string caption, string fieldName, string expression, string pivotTotalFunctionType, string numberFormat) : base(caption, fieldName,pivotTotalFunctionType, numberFormat)
		{
			this.Expression = expression;
		}
	}

	public class CalculatedFieldConverter : TypeConverter 
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
		{
			if (sourceType == typeof(string)) 
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
		{
			if (value is string) 
			{
				string[] attrs = ((string)value).Split(new char[] {','});
				string[] v  = null;

				CalculatedField field = new CalculatedField( attrs[0], attrs[1], attrs[2], attrs[3] );

				for ( int i=4; i<attrs.Length; i++ )
				{
					v = attrs[i].Split('=');
					field.Attributes.Add( v[0], v[1] );
				}

				return field;
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
		{  
			if (destinationType == typeof(string)) 
			{
				StringBuilder builder = new StringBuilder();

				builder.Append( ((CalculatedField)value).Caption + "," );
				builder.Append( ((CalculatedField)value).FieldName + "," );
				builder.Append( ((CalculatedField)value).Expression + "," );
				builder.Append( ((CalculatedField)value).FunctionType + "," );

				foreach ( string key in ((CalculatedField)value).Attributes.Keys )
				{
					builder.Append(	string.Format("{0}={1},", key, ((CalculatedField)value).Attributes[key].ToString()) );
				}

				return builder.ToString(0, builder.Length-1);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
	#endregion

	#region Contants
	public class PivotTotalFunctionType
	{
		public static string Average	= "plFunctionAverage";
		public static string Count		= "plFunctionCount"; 
		public static string Max		= "plFunctionMax"; 
		public static string Min		= "plFunctionMin"; 
		public static string StdDev		= "plFunctionStdDev"; 
		public static string StdDevP	= "plFunctionStdDevP"; 
		public static string Sum		= "plFunctionSum"; 
		public static string Var		= "plFunctionVar"; 
		public static string VarP		= "plFunctionVarP"; 
		public static string Calculated		= "plFunctionCalculated"; 
		public static string Unknown		= "plFunctionUnknown"; 

	}

	public class AxisType
	{
		public static string Row	= "RowAxis";
		public static string Column = "ColumnAxis";
		public static string Filter = "FilterAxis";

	}

	public class NumberFormat
	{
		public static string Percent		= "Percent";
		public static string Currency		= "Currency";
		public static string Scientific		= "Scientific";
		public static string GeneralDate	= "General Date";
		public static string LongDate		= "Long Date";
		public static string MediumlDate	= "Medium Date";
		public static string ShortDate		= "Short Date";
		public static string GeneralTime	= "General Time";
		public static string LongTime		= "Long Time";
		public static string MediumlTime	= "Medium Time";
		public static string ShortTime		= "Short Time";

	}
	#endregion
}
