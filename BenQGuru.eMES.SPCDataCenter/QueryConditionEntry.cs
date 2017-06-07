using System;

namespace BenQGuru.eMES.SPCDataCenter
{
	/// <summary>
	/// QueryConditionEntry 的摘要说明。
	/// </summary>
	public class QueryConditionEntry
	{
		public QueryConditionEntry()
		{
		}
		public QueryConditionEntry(int objectCount)
		{
			ObjectCode = new string[objectCount];
			TestCount = new int[objectCount];
			ObjectGroupCount = new int[objectCount];
			ColumnIndex = new int[objectCount][];
		}

		/// <summary>
		/// 表名
		/// </summary>
		public string TableName;

		/// <summary>
		/// 控制对象代码列表
		/// </summary>
		public string[] ObjectCode;

		/// <summary>
		/// 控制对象测试次数
		/// </summary>
		public int[] TestCount;

		/// <summary>
		/// 每个控制对象组次总数
		/// </summary>
		public int[] ObjectGroupCount;

		/// <summary>
		/// 每个控制对象每组的起始栏位，第一个索引是控制对象次序，第二个索引是按组次的每组起始栏位
		/// </summary>
		public int[][] ColumnIndex;
	}
}
