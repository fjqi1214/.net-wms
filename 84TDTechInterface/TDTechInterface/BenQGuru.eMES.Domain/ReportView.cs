using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for ReportView
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
/// ** 日 期:		2007-7-17 9:17:20
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.ReportView
{

	#region RptViewChartCategory
	/// <summary>
	/// 数据分组栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVCHARTCATE", "RPTID,CHARTSEQ,CATESEQ")]
	public class RptViewChartCategory : DomainObject
	{
		public RptViewChartCategory()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHARTSEQ", typeof(decimal), 10, true)]
		public decimal  ChartSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 分组栏位
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, false)]
		public string  ColumnName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CATESEQ", typeof(decimal), 10, true)]
		public decimal  CategorySequence;

	}
	#endregion

	#region RptViewChartData
	/// <summary>
	/// 数据统计栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVCHARTDATA", "RPTID,DATASEQ,CHARTSEQ")]
	public class RptViewChartData : DomainObject
	{
		public RptViewChartData()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASEQ", typeof(decimal), 10, true)]
		public decimal  DataSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 分组栏位
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, false)]
		public string  ColumnName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 统计方式：Sum/Avg/Count
		/// </summary>
		[FieldMapAttribute("TOTALTYPE", typeof(string), 40, false)]
		public string  TotalType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHARTSEQ", typeof(decimal), 10, true)]
		public decimal  ChartSequence;

	}
	#endregion

	#region RptViewChartMain
	/// <summary>
	/// 报表显示栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVCHARTMAIN", "RPTID,CHARTSEQ")]
	public class RptViewChartMain : DomainObject
	{
		public RptViewChartMain()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHARTSEQ", typeof(decimal), 10, true)]
		public decimal  ChartSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 图表类型
		/// </summary>
		[FieldMapAttribute("CHARTTYPE", typeof(string), 40, false)]
		public string  ChartType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 是否显示图例
		/// </summary>
		[FieldMapAttribute("SHOWLEGEND", typeof(string), 1, true)]
		public string  ShowLegend;

		/// <summary>
		/// 是否显示节点
		/// </summary>
		[FieldMapAttribute("SHOWMARKER", typeof(string), 1, true)]
		public string  ShowMarker;

		/// <summary>
		/// 节点类型
		/// </summary>
		[FieldMapAttribute("MarkerType", typeof(string), 40, false)]
		public string  MarkerType;

		/// <summary>
		/// 是否显示标签
		/// </summary>
		[FieldMapAttribute("SHOWLABEL", typeof(string), 1, true)]
		public string  ShowLabel;

		/// <summary>
		/// 标签显示格式
		/// </summary>
		[FieldMapAttribute("LABELFORMATID", typeof(string), 40, false)]
		public string  LabelFormatID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHARTSUBTYPE", typeof(string), 40, false)]
		public string  ChartSubType;

	}
	#endregion

	#region RptViewChartSeries
	/// <summary>
	/// 系列分组栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVCHARTSER", "RPTID,CHARTSEQ,SERSEQ")]
	public class RptViewChartSeries : DomainObject
	{
		public RptViewChartSeries()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("CHARTSEQ", typeof(decimal), 10, true)]
		public decimal  ChartSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 分组栏位
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, false)]
		public string  ColumnName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SERSEQ", typeof(decimal), 10, true)]
		public decimal  SeriesSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

	}
	#endregion

	#region RptViewDataConnect
	/// <summary>
	/// 数据库连接
	/// </summary>
	[Serializable, TableMap("TBLRPTVCONNECT", "DATACONNECTID")]
	public class RptViewDataConnect : DomainObject
	{
		public RptViewDataConnect()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATACONNECTID", typeof(decimal), 10, true)]
		public decimal  DataConnectID;

		/// <summary>
		/// 连接名称
		/// </summary>
		[FieldMapAttribute("CONNECTNAME", typeof(string), 100, false)]
		public string  ConnectName;

		/// <summary>
		/// 描述
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 数据库类型：Oracle/SQLServer
		/// </summary>
		[FieldMapAttribute("SERVERTYPE", typeof(string), 40, false)]
		public string  ServerType;

		/// <summary>
		/// 数据库服务名(Oracle)/服务器名(SQLServer)
		/// </summary>
		[FieldMapAttribute("SERVICENAME", typeof(string), 40, false)]
		public string  ServiceName;

		/// <summary>
		/// 连接用户名
		/// </summary>
		[FieldMapAttribute("USERNAME", typeof(string), 40, false)]
		public string  UserName;

		/// <summary>
		/// 连接密码
		/// </summary>
		[FieldMapAttribute("PASSWORD", typeof(string), 100, false)]
		public string  Password;

		/// <summary>
		/// 默认数据库名(仅对SQL Server有效)
		/// </summary>
		[FieldMapAttribute("DEFAULTDATABASE", typeof(string), 40, false)]
		public string  DefaultDatabase;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewDataFormat
	/// <summary>
	/// 文本显示格式
	/// </summary>
	[Serializable, TableMap("TBLRPTVDATAFMT", "FORMATID")]
	public class RptViewDataFormat : DomainObject
	{
		public RptViewDataFormat()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FORMATID", typeof(string), 40, true)]
		public string  FormatID;

		/// <summary>
		/// 字体名称
		/// </summary>
		[FieldMapAttribute("FONTFAMILY", typeof(string), 40, false)]
		public string  FontFamily;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 字体大小
		/// </summary>
		[FieldMapAttribute("FONTSIZE", typeof(decimal), 10, true)]
		public decimal  FontSize;

		/// <summary>
		/// 字体加重:Bold/Normal
		/// </summary>
		[FieldMapAttribute("FONTWEIGHT", typeof(string), 40, false)]
		public string  FontWeight;

		/// <summary>
		/// 文字斜体:Underline/Normal
		/// </summary>
		[FieldMapAttribute("TEXTDECORATION", typeof(string), 40, false)]
		public string  TextDecoration;

		/// <summary>
		/// 前景色
		/// </summary>
		[FieldMapAttribute("COLOR", typeof(string), 40, false)]
		public string  Color;

		/// <summary>
		/// 背景色
		/// </summary>
		[FieldMapAttribute("BCOLOR", typeof(string), 40, false)]
		public string  BackgroundColor;

		/// <summary>
		/// 水平对齐方式
		/// </summary>
		[FieldMapAttribute("TextAlign", typeof(string), 40, false)]
		public string  TextAlign;

		/// <summary>
		/// 纵对齐
		/// </summary>
		[FieldMapAttribute("VerticalAlign", typeof(string), 40, false)]
		public string  VerticalAlign;

		/// <summary>
		/// 文本显示格式
		/// </summary>
		[FieldMapAttribute("TEXTFORMAT", typeof(string), 40, false)]
		public string  TextFormat;

		/// <summary>
		/// 斜体：Normal/Italic
		/// </summary>
		[FieldMapAttribute("FONTSTYLE", typeof(string), 40, false)]
		public string  FontStyle;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("COLUMNWIDTH", typeof(decimal), 15, true)]
		public decimal  ColumnWidth;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("BORDERSTYLE", typeof(string), 40, false)]
		public string  BorderStyle;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("TEXTEXPRESS", typeof(string), 100, false)]
		public string  TextExpress;

	}
	#endregion

	#region RptViewDataSource
	/// <summary>
	/// 数据查询
	/// </summary>
	[Serializable, TableMap("TBLRPTVDATASRC", "DATASOURCEID")]
	public class RptViewDataSource : DomainObject
	{
		public RptViewDataSource()
		{
		}
 
		/// <summary>
		/// 数据源ID
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 名称
		/// </summary>
		[FieldMapAttribute("NAME", typeof(string), 40, false)]
		public string  Name;

		/// <summary>
		/// 描述
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 数据源类型:SQL/DLL
		/// </summary>
		[FieldMapAttribute("SOURCETYPE", typeof(string), 40, false)]
		public string  SourceType;

		/// <summary>
		/// SQL语句
		/// </summary>
		[FieldMapAttribute("SQL", typeof(string), 100, false)]
		public string  SQL;

		/// <summary>
		/// DLL文件名
		/// </summary>
		[FieldMapAttribute("DLLFILENAME", typeof(string), 100, false)]
		public string  DllFileName;

		/// <summary>
		/// 数据库连接
		/// </summary>
		[FieldMapAttribute("DATACONNECTID", typeof(decimal), 10, true)]
		public decimal  DataConnectID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewDataSourceColumn
	/// <summary>
	/// 数据查询栏位描述
	/// </summary>
	[Serializable, TableMap("TBLRPTVDATASRCCOLUMN", "DATASOURCEID,COLUMNSEQ,COLUMNNAME")]
	public class RptViewDataSourceColumn : DomainObject
	{
		public RptViewDataSourceColumn()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 栏位次序
		/// </summary>
		[FieldMapAttribute("COLUMNSEQ", typeof(decimal), 10, true)]
		public decimal  ColumnSequence;

		/// <summary>
		/// 栏位名称
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, true)]
		public string  ColumnName;

		/// <summary>
		/// 描述
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 数据类型(System.String/System.DateTime/System.Boolean)
		/// </summary>
		[FieldMapAttribute("DATATYPE", typeof(string), 40, false)]
		public string  DataType;

		/// <summary>
		/// 是否可见
		/// </summary>
		[FieldMapAttribute("VISIBLE", typeof(string), 1, true)]
		public string  Visible;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewDataSourceParam
	/// <summary>
	/// 数据查询参数(仅对DLL有效)
	/// </summary>
	[Serializable, TableMap("TBLRPTVDATASRCPARAM", "DATASOURCEID,PARAMSEQ")]
	public class RptViewDataSourceParam : DomainObject
	{
		public RptViewDataSourceParam()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PARAMSEQ", typeof(decimal), 10, true)]
		public decimal  ParameterSequence;

		/// <summary>
		/// 参数名称
		/// </summary>
		[FieldMapAttribute("PARAMNAME", typeof(string), 40, false)]
		public string  ParameterName;

		/// <summary>
		/// 描述
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 数据类型(System.String/System.DateTime/System.Boolean)
		/// </summary>
		[FieldMapAttribute("DATATYPE", typeof(string), 40, false)]
		public string  DataType;

		/// <summary>
		/// 默认值
		/// </summary>
		[FieldMapAttribute("DEFAULTVALUE", typeof(string), 40, false)]
		public string  DefaultValue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewDesignMain
	/// <summary>
	/// 自定义报表主表
	/// </summary>
	[Serializable, TableMap("TBLRPTVDESIGNMAIN", "RPTID")]
	public class RptViewDesignMain : DomainObject
	{
		public RptViewDesignMain()
		{
		}
 
		/// <summary>
		/// GUID
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 显示名称
		/// </summary>
		[FieldMapAttribute("RPTNAME", typeof(string), 40, false)]
		public string  ReportName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 数据源
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 显示类型:grid/chart/mix (仅针对在线设计报表)
		/// </summary>
		[FieldMapAttribute("DISPLAYTYPE", typeof(string), 40, false)]
		public string  DisplayType;

		/// <summary>
		/// 报表来源:online/offline
		/// </summary>
		[FieldMapAttribute("RPTBUILDER", typeof(string), 40, false)]
		public string  ReportBuilder;

		/// <summary>
		/// 生成的物理报表文件
		/// </summary>
		[FieldMapAttribute("RPTFILENAME", typeof(string), 100, false)]
		public string  ReportFileName;

		/// <summary>
		/// 父级报表目录
		/// </summary>
		[FieldMapAttribute("PRPTFOLDER", typeof(string), 40, false)]
		public string  ParentReportFolder;

		/// <summary>
		/// 状态:initial(初始设计)/publish(已发布)/redesign(再次设计)
		/// </summary>
		[FieldMapAttribute("STATUS", typeof(string), 40, false)]
		public string  Status;

		/// <summary>
		/// 设计人员
		/// </summary>
		[FieldMapAttribute("DESIGNUSER", typeof(string), 40, false)]
		public string  DesignUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESIGNDATE", typeof(int), 8, true)]
		public int  DesignDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESIGNTIME", typeof(int), 6, true)]
		public int  DesignTime;

		/// <summary>
		/// 发布人员
		/// </summary>
		[FieldMapAttribute("PUBLISHUSER", typeof(string), 40, false)]
		public string  PublishUser;

		/// <summary>
		/// 发布日期
		/// </summary>
		[FieldMapAttribute("PUBLISHDATE", typeof(int), 8, true)]
		public int  PublishDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PUBLISHTIME", typeof(int), 6, true)]
		public int  PublishTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewEntry
	/// <summary>
	/// 报表结构维护
	/// </summary>
	[Serializable, TableMap("TBLRPTVENTRY", "ENTRYCODE")]
	public class RptViewEntry : DomainObject
	{
		public RptViewEntry()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ENTRYCODE", typeof(string), 40, true)]
		public string  EntryCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ENTRYNAME", typeof(string), 40, false)]
		public string  EntryName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("PENTRYCODE", typeof(string), 40, false)]
		public string  ParentEntryCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 是否可见
		/// </summary>
		[FieldMapAttribute("VISIBLE", typeof(string), 1, true)]
		public string  Visible;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("ENTRYTYPE", typeof(string), 40, false)]
		public string  EntryType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, false)]
		public string  ReportID;

	}
	#endregion

	#region RptViewExtendText
	/// <summary>
	/// 报表显示栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVEXTTEXT", "RPTID,SEQ")]
	public class RptViewExtendText : DomainObject
	{
		public RptViewExtendText()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 文本显示格式
		/// </summary>
		[FieldMapAttribute("LOCATION", typeof(string), 40, false)]
		public string  Location;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FORMATID", typeof(string), 40, false)]
		public string  FormatID;

	}
	#endregion

	#region RptViewFileParameter
	/// <summary>
	/// 报表文件参数列表
	/// </summary>
	[Serializable, TableMap("TBLRPTVFILEPARAM", "RPTID,SEQ")]
	public class RptViewFileParameter : DomainObject
	{
		public RptViewFileParameter()
		{
		}
 
		/// <summary>
		/// GUID
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 显示名称
		/// </summary>
		[FieldMapAttribute("FILEPARAMNAME", typeof(string), 40, false)]
		public string  FileParameterName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATATYPE", typeof(string), 40, false)]
		public string  DataType;

		/// <summary>
		/// 是否由浏览用户输入
		/// </summary>
		[FieldMapAttribute("VIEWERINPUT", typeof(string), 1, true)]
		public string  ViewerInput;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DEFAULTVALUE", typeof(string), 40, false)]
		public string  DefaultValue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

	}
	#endregion

	#region RptViewFilterUI
	/// <summary>
	/// 过滤栏位界面
	/// </summary>
	[Serializable, TableMap("TBLRPTVFILTERUI", "RPTID,SEQ")]
	public class RptViewFilterUI : DomainObject
	{
		public RptViewFilterUI()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 输入条件类型:filter/fileparameter
		/// </summary>
		[FieldMapAttribute("INPUTTYPE", typeof(string), 40, false)]
		public string  InputType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 输入名称
		/// </summary>
		[FieldMapAttribute("INPUTNAME", typeof(string), 40, false)]
		public string  InputName;

		/// <summary>
		/// 输入值
		/// </summary>
		[FieldMapAttribute("UITYPE", typeof(string), 40, false)]
		public string  UIType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SQLFLTSEQ", typeof(decimal), 10, true)]
		public decimal  SqlFilterSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SELQTYPE", typeof(string), 40, false)]
		public string  SelectQueryType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LISTSRCTYPE", typeof(string), 40, false)]
		public string  ListDataSourceType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LISTSVAL", typeof(string), 100, false)]
		public string  ListStaticValue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LISTDSRC", typeof(decimal), 10, true)]
		public decimal  ListDynamicDataSource;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LISTDTEXTCOL", typeof(string), 40, false)]
		public string  ListDynamicTextColumn;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("LISTDVALUECOL", typeof(string), 40, false)]
		public string  ListDynamicValueColumn;

        /// <summary>
        /// 记录该选项是否是必选
        /// </summary>
        [FieldMapAttribute("CHECKEXIST", typeof(string), 1, false)]
        public string CheckExist;

	}
	#endregion

	#region RptViewGridColumn
	/// <summary>
	/// 报表显示栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVGRIDCOLUMN", "RPTID,DISPLAYSEQ")]
	public class RptViewGridColumn : DomainObject
	{
		public RptViewGridColumn()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DISPLAYSEQ", typeof(decimal), 10, true)]
		public decimal  DisplaySequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, false)]
		public string  ColumnName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewGridDataFormat
	/// <summary>
	/// 报表显示栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVGRIDDATAFMT", "RPTID,COLUMNNAME,STYLETYPE,GRPSEQ")]
	public class RptViewGridDataFormat : DomainObject
	{
		public RptViewGridDataFormat()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, true)]
		public string  ColumnName;

		/// <summary>
		/// 样式引用类型:header/data/total
		/// </summary>
		[FieldMapAttribute("STYLETYPE", typeof(string), 40, true)]
		public string  StyleType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 文本显示格式
		/// </summary>
		[FieldMapAttribute("FORMATID", typeof(string), 40, false)]
		public string  FormatID;

		/// <summary>
		/// 分组次序（只有当StyleType=subtotal时才有效）
		/// </summary>
		[FieldMapAttribute("GRPSEQ", typeof(decimal), 10, true)]
		public decimal  GroupSequence;

	}
	#endregion

	#region RptViewGridDataStyle
	/// <summary>
	/// 报表显示栏位
	/// </summary>
	[Serializable, TableMap("TBLRPTVGRIDDATASTYLE", "RPTID")]
	public class RptViewGridDataStyle : DomainObject
	{
		public RptViewGridDataStyle()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 文本显示格式
		/// </summary>
		[FieldMapAttribute("STYLEID", typeof(decimal), 10, true)]
		public decimal  StyleID;

	}
	#endregion

	#region RptViewGridFilter
	/// <summary>
	/// 报表分组设定
	/// </summary>
	[Serializable, TableMap("TBLRPTVGRIDFLT", "RPTID,FLTSEQ")]
	public class RptViewGridFilter : DomainObject
	{
		public RptViewGridFilter()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("FLTSEQ", typeof(decimal), 10, true)]
		public decimal  FilterSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 数据栏位
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, false)]
		public string  ColumnName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 比较方式:equal/greater/lesser/...
		/// </summary>
		[FieldMapAttribute("FLTOPERAT", typeof(string), 40, false)]
		public string  FilterOperation;

		/// <summary>
		/// 数据源参数次序
		/// </summary>
		[FieldMapAttribute("PARAMNAME", typeof(string), 40, false)]
		public string  ParameterName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DEFAULTVALUE", typeof(string), 100, false)]
		public string  DefaultValue;

	}
	#endregion

	#region RptViewGridGroup
	/// <summary>
	/// 报表分组设定
	/// </summary>
	[Serializable, TableMap("TBLRPTVGRIDGRP", "RPTID,GRPSEQ")]
	public class RptViewGridGroup : DomainObject
	{
		public RptViewGridGroup()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("GRPSEQ", typeof(decimal), 10, true)]
		public decimal  GroupSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, false)]
		public string  ColumnName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewGridGroupTotal
	/// <summary>
	/// 报表分组设定
	/// </summary>
	[Serializable, TableMap("TBLRPTVGRIDGRPTOTAL", "RPTID,GRPSEQ,COLUMNNAME")]
	public class RptViewGridGroupTotal : DomainObject
	{
		public RptViewGridGroupTotal()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("GRPSEQ", typeof(decimal), 10, true)]
		public decimal  GroupSequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DATASOURCEID", typeof(decimal), 10, true)]
		public decimal  DataSourceID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("COLUMNNAME", typeof(string), 40, true)]
		public string  ColumnName;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 数据汇总方式:empty/sum/avg/count
		/// </summary>
		[FieldMapAttribute("TOTALTYPE", typeof(string), 40, false)]
		public string  TotalType;

	}
	#endregion

	#region RptViewReportSecurity
	/// <summary>
	/// 报表权限
	/// </summary>
	[Serializable, TableMap("TBLRPTVRPTSECURITY", "RPTID,SEQ")]
	public class RptViewReportSecurity : DomainObject
	{
		public RptViewReportSecurity()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("USERGROUPCODE", typeof(string), 40, false)]
		public string  UserGroupCode;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("FUNCTIONGROUPCODE", typeof(string), 40, false)]
        public string FunctionGroupCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RIGHTACCESS", typeof(string), 40, false)]
		public string  RightAccess;

	}
	#endregion

	#region RptViewReportStyle
	/// <summary>
	/// 报表样式
///
	/// </summary>
	[Serializable, TableMap("TBLRPTVSTYLE", "STYLEID")]
	public class RptViewReportStyle : DomainObject
	{
		public RptViewReportStyle()
		{
		}
 
		/// <summary>
		/// 数据源ID
		/// </summary>
		[FieldMapAttribute("STYLEID", typeof(decimal), 10, true)]
		public decimal  StyleID;

		/// <summary>
		/// 名称
		/// </summary>
		[FieldMapAttribute("NAME", typeof(string), 40, false)]
		public string  Name;

		/// <summary>
		/// 描述
		/// </summary>
		[FieldMapAttribute("DESCRIPTION", typeof(string), 100, false)]
		public string  Description;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewReportStyleDetail
	/// <summary>
	/// 报表样式
///
	/// </summary>
	[Serializable, TableMap("TBLRPTVSTYLEDTL", "STYLEID,STYLETYPE")]
	public class RptViewReportStyleDetail : DomainObject
	{
		public RptViewReportStyleDetail()
		{
		}
 
		/// <summary>
		/// 数据源ID
		/// </summary>
		[FieldMapAttribute("STYLEID", typeof(decimal), 10, true)]
		public decimal  StyleID;

		/// <summary>
		/// 名称
		/// </summary>
		[FieldMapAttribute("STYLETYPE", typeof(string), 40, true)]
		public string  StyleType;

		/// <summary>
		/// 描述
		/// </summary>
		[FieldMapAttribute("FORMATID", typeof(string), 40, false)]
		public string  FormatID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, false)]
		public decimal  Sequence;

	}
	#endregion

	#region RptViewUserDefault
	/// <summary>
	/// 用户订阅
	/// </summary>
	[Serializable, TableMap("TBLRPTVUSERDFT", "USERCODE")]
	public class RptViewUserDefault : DomainObject
	{
		public RptViewUserDefault()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("USERCODE", typeof(string), 40, true)]
		public string  UserCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("DEFAULTRPTID", typeof(string), 40, false)]
		public string  DefaultReportID;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

	}
	#endregion

	#region RptViewUserSubscription
	/// <summary>
	/// 用户订阅
	/// </summary>
	[Serializable, TableMap("TBLRPTVUSERSUBSCR", "USERCODE,RPTID,SEQ")]
	public class RptViewUserSubscription : DomainObject
	{
		public RptViewUserSubscription()
		{
		}
 
		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("USERCODE", typeof(string), 40, true)]
		public string  UserCode;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("RPTID", typeof(string), 40, true)]
		public string  ReportID;

		/// <summary>
		/// 输入条件类型:filter/fileparameter
		/// </summary>
		[FieldMapAttribute("INPUTTYPE", typeof(string), 40, false)]
		public string  InputType;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SEQ", typeof(decimal), 10, true)]
		public decimal  Sequence;

		/// <summary>
		/// 输入名称
		/// </summary>
		[FieldMapAttribute("INPUTNAME", typeof(string), 40, false)]
		public string  InputName;

		/// <summary>
		/// 输入值
		/// </summary>
		[FieldMapAttribute("INPUTVALUE", typeof(string), 40, false)]
		public string  InputValue;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MUSER", typeof(string), 40, false)]
		public string  MaintainUser;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MDATE", typeof(int), 8, true)]
		public int  MaintainDate;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("MTIME", typeof(int), 6, true)]
		public int  MaintainTime;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("EAttribute1", typeof(string), 100, false)]
		public string  EAttribute1;

		/// <summary>
		/// 
		/// </summary>
		[FieldMapAttribute("SQLFLTSEQ", typeof(decimal), 10, true)]
		public decimal  SqlFilterSequence;

	}
	#endregion

    #region SelectQueryComplex
    /// <summary>
    /// 过滤栏位界面
    /// </summary>
    [Serializable, TableMap("", "")]
    public class SelectQuery : DomainObject
    {
        public SelectQuery()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("Code", typeof(string), 100, false)]
        public string Code;

        /// <summary>
        /// 输入条件类型:filter/fileparameter
        /// </summary>
        [FieldMapAttribute("CodeDesc", typeof(string), 100, false)]
        public string CodeDesc;

    }
    #endregion

}

