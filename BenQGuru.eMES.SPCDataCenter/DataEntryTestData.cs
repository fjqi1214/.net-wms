using System;

namespace BenQGuru.eMES.SPCDataCenter
{
	/// <summary>
	/// DataEntryTestData 的摘要说明。
	/// </summary>
	public class DataEntryTestData
	{
		public DataEntryTestData()
		{
		}

		private string _objectCode = string.Empty;
		/// <summary>
		/// 测试对象
		/// </summary>
		public string ObjectCode
		{
			get { return _objectCode; }
			set { _objectCode = value; }
		}

		private decimal _groupSequence = 1;
		/// <summary>
		/// 测试组次
		/// </summary>
		public decimal GroupSequence
		{
			get { return _groupSequence; }
			set { _groupSequence = value; }
		}

		private object _data = null;
		/// <summary>
		/// 测试数据(如果单个数据，则是decimal，如果多个数据，则是decimal[])
		/// </summary>
		public object Data
		{
			get { return _data; }
			set { _data = value; }
		}

		private int _columnIndex = -1;
		/// <summary>
		/// 测试数据存放的栏位索引
		/// </summary>
		internal int ColumnIndex
		{
			get { return _columnIndex; }
			set { _columnIndex = value; }
		}

	}
}
