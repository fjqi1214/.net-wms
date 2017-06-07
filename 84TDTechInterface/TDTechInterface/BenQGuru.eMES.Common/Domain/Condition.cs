using System;
using System.Text.RegularExpressions;

namespace BenQGuru.eMES.Common.Domain
{
	/// <summary>
	/// IDomainObjectQuery接口
	/// </summary>
	public interface IQueryCondition
	{
		Condition Insert(Condition condition, Type type);	
		Condition Remove(Condition condition, Type type);	
		Condition[] Conditions
		{
			get;
		}
	}
	/// <summary>
	/// SQL 类型
	/// </summary>
	public enum SQLType
	{
		Text,
        Command,
        StoredProcedure
	}
    /// <summary>
    /// Direction 类型
    /// </summary>
    public enum DirectionType
    {
        Input,
        InputOutput,
        Output,
        ReturnValue
    }
	/// <summary>
	/// 数据库类型
	/// </summary>
	public enum DBName
	{
		//DBName 表示连接到不同的数据库
		//DBName.MES 表示连接MES的数据库连接		(Oracle)
		//DBName.SAP 表示连接SAP的数据库连接		(Oracle)
		//DBName.SPC 表示连接SPC的数据库连接		(SqlServer)
		//DBName.ERP 表示连接ERP的数据库连接		(Informix)
		MES,
		SAP,
		SPC,
		ERP,
		HIS
	}
	/// <summary>
	/// SQL 查询条件
	/// </summary>
	[Serializable]
	public class Condition
	{
		private SQLType _sqlType = SQLType.Text;
		private string _sql = string.Empty;

		public Condition()
		{
			_sqlType = SQLType.Text;
		}

		
		public Condition(SQLType sqlType)
		{
			_sqlType = sqlType;
		}

		/// <summary>
		/// SQL语句
		/// </summary>
		public virtual string SQLText
		{
			get
			{
				return _sql;
			}
			set
			{
				_sql = value;
			}
		}

		/// <summary>
		/// SQL语句类型
		/// </summary>
		public SQLType SQLType
		{
			get
			{
				return _sqlType;
			}
		}
	}
	/// <summary>
	/// 完整SQL条件
	/// </summary>
	/// <example>
	/// int iCount = 
	///		this.DataProvider.GetCount(new SQLCondition("select count(*) from tblmo"));
	/// </example>
	[Serializable]
	public class SQLCondition : Condition
	{
		/// <summary>
		/// 完整SQL条件
		/// </summary>
		/// <param name="sql">SQL语句</param>
		public SQLCondition(string sql) : base(SQLType.Text)
		{
			this.SQLText  = sql;
		}
	}
	/// <summary>
	/// SQL 查询参数
	/// </summary>
	/// <example>
	/// MO[] moList = 
	///		this.DataProvider.CustomQuery(
	///			typeof(MO), 
	///			new SQLParamCondition(
	///				"select * from tblmo where itemcode=$itemcode",
	///				new SQLParameter[]{
	///					new SQLParameter("itemcode", typeof(string), "ITEM1")
	///				}
	///			)
	///		);
	/// </example>
	[Serializable]
	public class SQLParameter
	{
		private string _name;
		private Type _type;
		private object _value;

		/// <summary>
		/// SQL查询参数
		/// </summary>
		/// <param name="name">参数名称</param>
		/// <param name="type">类型</param>
		/// <param name="value">值</param>
		public SQLParameter(string name, Type type, object value)
		{
			this._name = name;
			this._type = type;
			this._value = value;
		}

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// 参数类型
        /// </summary>
        public Type Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
	}
	/// <summary>
	/// SQL 查询参数和条件组合
	/// </summary>
	/// <example>
	/// MO[] moList = 
	///		this.DataProvider.CustomQuery(
	///			typeof(MO), 
	///			new SQLParamCondition(
	///				"select * from tblmo where itemcode=$itemcode",
	///				new SQLParameter[]{
	///					new SQLParameter("itemcode", typeof(string), "ITEM1")
	///				}
	///			)
	///		);
	/// </example>
	[Serializable]
	public class SQLParamCondition : Condition
	{	
		private SQLParameter[] _parameters;

		/// <summary>
		/// SQL 查询参数和条件组合
		/// </summary>
		/// <param name="sql">带参数的SQL语句</param>
		/// <param name="parameters">参数列表</param>
		public SQLParamCondition(string sql, SQLParameter[] parameters) : base(SQLType.Command)
		{
			this.SQLText = sql;
			this._parameters = parameters;
		}

		/// <summary>
		/// 参数列表
		/// </summary>
		public SQLParameter[] Parameters
		{
			get
			{
				return this._parameters;
			}
		}
	}
	/// <summary>
	/// SQL 分页和参数组合
	/// </summary>
	/// <example>
	/// MO[] moList = 
	///		this.DataProvider.CustomQuery(
	///			typeof(MO), 
	///			new PagerCondition("select * from tblmo", "mocode", 1, 50)
	///		);
	/// </example>
	[Serializable]
	public class PagerCondition : Condition
	{				
		/// <summary>
		/// SQL 分页和参数组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		/// <param name="group">排序栏位</param>
		public PagerCondition(string sql, int inclusive, int exclusive, bool group) : base(SQLType.Text)
		{
			this.BuildPagerSql( sql,  inclusive, exclusive, group );
		}	

		/// <summary>
		/// SQL 分页和参数组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="orderbyFields">有Group By子句时，order by要用取出的结果集的字段名</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		/// <param name="group">是否有没有Group By子句</param>
		public PagerCondition(string sql, string orderbyFields, int inclusive, int exclusive, bool group) : base(SQLType.Text)
		{
			this.BuildPagerSql( sql, orderbyFields, inclusive, exclusive, group );
		}

		/// <summary>
		/// SQL 分页和参数组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		public PagerCondition(string sql, int inclusive, int exclusive) : this(sql, inclusive, exclusive, false)
		{
		}

		/// <summary>
		/// SQL 分页和参数组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="orderbyFields">有Group By子句时，order by要用取出的结果集的字段名</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		public PagerCondition(string sql, string orderbyFields, int inclusive, int exclusive) : this(sql, orderbyFields, inclusive, exclusive, false)
		{
		}

		private void BuildPagerSql( string sql, int inclusive, int exclusive, bool group )
		{
			if ( !group )
			{
				this.SQLText = sql.Trim().Remove(0, 6);

				if ( this.SQLText.Trim().StartsWith("*") )
				{
					//throw new Exception("*禁止使用！");
					ExceptionManager.Raise(this.GetType(),"$Error_Forbidden_Asterisk");
				}

				this.SQLText = string.Format("select * from (select rownum as rid,{0}) where rid between {1} and {2}", 
					this.SQLText, 
					inclusive.ToString(), 
					exclusive.ToString());
			}
			else
			{
				this.SQLText = string.Format("select * from (select rownum as rid, result.* from ({0}) result) where rid between {1} and {2}", 
					sql, 
					inclusive.ToString(), 
					exclusive.ToString());
			}
		}
		/// <summary>
		/// 自动Build分页SQL语句
		/// </summary>
		/// <param name="sql">不带条件的SQL</param>
		/// <param name="orderbyFields">排序字段</param>
		/// <param name="inclusive">起始记录号</param>
		/// <param name="exclusive">结束记录号</param>
		/// <param name="group">分组</param>
		private void BuildPagerSql( string sql, string orderbyFields, int inclusive, int exclusive, bool group )
		{
			string LeadingTable = GetLeadingSql(sql);//获取Leading 的表名称
			if ( !group )
			{
				this.SQLText = sql.Trim().Remove(0, 6);

				if (this.SQLText.Trim().StartsWith("*"))
				{
					//throw new Exception("*禁止使用！");
					ExceptionManager.Raise(this.GetType(),"$Error_Forbidden_Asterisk");
				}

				this.SQLText = string.Format(@"select * from (select /*+ leading({4}) */  ROW_NUMBER() OVER (ORDER BY {0}) as rid,{1}) where rid between {2} and {3}", 
											orderbyFields,
											this.SQLText, 
											inclusive.ToString(), 
											exclusive.ToString(),
											LeadingTable);
			}
			else
			{
				this.SQLText = string.Format(@"select * from (select /*+ leading({4}) */  ROW_NUMBER() OVER (ORDER BY {0}) as rid, result.* from ({1}) result) where rid between {2} and {3}", 
											orderbyFields,
											sql, 
											inclusive.ToString(), 
											exclusive.ToString(),
											LeadingTable);
			}	
		}
		/// <summary>
		///获取Leading 的表名称
		///添加此方法的原因	:	数据表分区后连接查询出现错误,必须指定连接首表  /*+ leading(tblitem) */
		///解决方法			:	指定连接表名
		///获取方法			:	获取第一个from后面的表名作为首连接表
		///注意				:	此方法只是适用以上情况,最好的做法是首连接表是作为参数传入的
		///修改				:	判断是否能够取得空格的index
		///Add By Simone		2005/09/13
		/// </summary>
		/// <param name="sql">SQL</param>
		/// <returns>转化后的SQL</returns>
		private string GetLeadingSql(string sql)
		{
			string[] splitSTR = Regex.Split(sql,"from",RegexOptions.IgnoreCase);
			string afterFromStr = splitSTR[1].Trim();
			int index = afterFromStr.IndexOf(' ',0,afterFromStr.Length); 

			string LeadingtableName = afterFromStr;
			if(index >= 0)
			{
				LeadingtableName = afterFromStr.Substring(0,index);
			}

			return LeadingtableName;
		}
	}
	/// <summary>
	/// SQL 参数、分页组合
	/// </summary>
	public class PagerParamCondition : SQLParamCondition
	{	
		/// <summary>
		/// SQL 参数、分页组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		/// <param name="parameters">参数列表</param>
		public PagerParamCondition(string sql, int inclusive, int exclusive, SQLParameter[] parameters) : base(sql, parameters)
		{
			this.SQLText = new PagerCondition( sql, inclusive, exclusive).SQLText;
		}

		/// <summary>
		/// SQL 参数、分页组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="orderbyFields">有Group By子句时，order by要用取出的结果集的字段名</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		/// <param name="parameters">参数列表</param>
		public PagerParamCondition(string sql, string orderbyFields, int inclusive, int exclusive, SQLParameter[] parameters) : base(sql, parameters)
		{
			this.SQLText = new PagerCondition( sql, orderbyFields, inclusive, exclusive).SQLText;
		}

		/// <summary>
		/// SQL 参数、分页组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		/// <param name="group">是否有分组</param>
		public PagerParamCondition(string sql, int inclusive, int exclusive, SQLParameter[] parameters, bool group) : base(sql, parameters)
		{
			this.SQLText = new PagerCondition( sql, inclusive, exclusive, group ).SQLText;
		}

		/// <summary>
		/// SQL 参数、分页组合
		/// </summary>
		/// <param name="sql">查询数据的SQL语句</param>
		/// <param name="orderbyFields">有Group By子句时，order by要用取出的结果集的字段名</param>
		/// <param name="inclusive">起始记录行</param>
		/// <param name="exclusive">读取行数</param>
		/// <param name="parameters">参数列表</param>
		/// <param name="group">是否有分组</param>
		public PagerParamCondition(string sql, string orderbyFields, int inclusive, int exclusive, SQLParameter[] parameters, bool group) : base(sql, parameters)
		{
			this.SQLText = new PagerCondition( sql, orderbyFields, inclusive, exclusive, group ).SQLText;
		}
	}
	/// <summary>
	/// SQL 日期范围条码类
	/// </summary>
	public class DateTimeRangeCondition : Condition
	{
		string _sql = string.Empty;

		/// <summary>
		/// SQL 日期范围条码类
		/// </summary>
		/// <param name="dateField">日期字段</param>
		/// <param name="timeField">时间字段</param>
		/// <param name="beginDate">开始日期</param>
		/// <param name="beginTime">开始时间</param>
		/// <param name="endDate">结束日期</param>
		/// <param name="endTime">结束时间</param>
		public DateTimeRangeCondition( string dateField, string timeField, int beginDate, int beginTime, int endDate, int endTime )
		{
			if ( beginDate != endDate )
			{
				_sql = string.Format( " ({0} between {1}+1 and {2}-1 or ( {0} = {1} and {3} >= {4} ) or ( {0} = {2} and {3} <= {5} ))",
					dateField, beginDate, endDate, timeField, beginTime, endTime  );			
			}
			else
			{
				_sql = string.Format(" ({0} = {1} AND {3} between {4} and {5} )", 
					dateField, beginDate, endDate, timeField, beginTime, endTime);
			}
		}

		/// <summary>
		/// 取时间范围SQL
		/// select * from table where DateTimeRangeCondition().SQLText
		/// select * from table where ... and DateTimeRangeCondition().SQLText
		/// </summary>
		public override string SQLText
		{
			get
			{
				return this._sql;
			}
		}

	}

    /// <summary>
    /// Procedure 查询参数
    /// </summary>
    /// <example>
    /// ProcedureParameter[] paras=new ProcedureParameter[2];
    /// paras[0]=new ProcedureParameter("str",typeof(System.String),100,DirectionType.Input,"John");
    /// paras[1]=new ProcedureParameter("strout",typeof(System.String),100,DirectionType.Output,string.Empty);
    /// ProcedureCondition condition = new ProcedureCondition(proc_name, paras);
    /// IDomainDataProvider provider = DomainDataProviderManager.DomainDataProvider();
    /// provider.CustomProcedure(ref condition);
    /// </example>
    [Serializable]
    public class ProcedureParameter : SQLParameter
    {
        /// <summary>
        ///Procedure查询参数
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <param name="type">类型</param>
        /// <param name="value">值</param>
        public ProcedureParameter(string name, Type type, int length, DirectionType direction, object value)
            : base(name, type, value)
        {
            this._length = length;
            this._direction = direction;
        }

        private DirectionType _direction;
        /// <summary>
        /// 参数方向
        /// </summary>
        public DirectionType Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                this._direction = value;
            }
        }

        private int _length;
        /// <summary>
        /// 参数值
        /// </summary>
        public int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
    }

    /// <summary>
    /// Procedure 查询参数和条件组合
    /// </summary>
    /// <example>
    /// ProcedureParameter[] paras=new ProcedureParameter[2];
    /// paras[0]=new ProcedureParameter("str",typeof(System.String),100,DirectionType.Input,"John");
    /// paras[1]=new ProcedureParameter("strout",typeof(System.String),100,DirectionType.Output,string.Empty);
    /// ProcedureCondition condition = new ProcedureCondition(proc_name, paras);
    /// IDomainDataProvider provider = DomainDataProviderManager.DomainDataProvider();
    /// provider.CustomProcedure(ref condition);
    /// </example>
    [Serializable]
    public class ProcedureCondition : Condition
    {
        private ProcedureParameter[] _parameters;

        /// <summary>
        /// Procedure 查询参数和条件组合
        /// </summary>
        /// <param name="sql">带参数的SQL语句</param>
        /// <param name="parameters">参数列表</param>
        public ProcedureCondition(string Procedure, ProcedureParameter[] parameters)
            : base(SQLType.StoredProcedure)
        {
            this.SQLText = Procedure;
            this._parameters = parameters;
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        public ProcedureParameter[] Parameters
        {
            get
            {
                return this._parameters;
            }
            set
            {
                this._parameters = value;
            }
        }
    }
}
