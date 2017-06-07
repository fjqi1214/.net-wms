using System;
using System.Collections;

namespace BenQGuru.eMES.SPCDataCenter
{
	/// <summary>
	/// DataEntry 的摘要说明。
	/// </summary>
	public class DataEntry
	{
		public DataEntry()
		{
		}

		private string _modelCode = string.Empty;
		/// <summary>
		/// 机种代码
		/// </summary>
		public string ModelCode
		{
			get { return _modelCode; }
			set { _modelCode = value; }
		}

		private string _itemCode = string.Empty;
		/// <summary>
		/// 产品代码
		/// </summary>
		public string ItemCode
		{
			get { return _itemCode; }
			set { _itemCode = value; }
		}

		private string _moCode = string.Empty;
		/// <summary>
		/// 工单代码
		/// </summary>
		public string MOCode
		{
			get { return _moCode; }
			set { _moCode = value; }
		}

		private string _runningCard = string.Empty;
		/// <summary>
		/// 产品序列号
		/// </summary>
		public string RunningCard
		{
			get { return _runningCard; }
			set { _runningCard = value; }
		}

		private decimal _runningCardSequence = 0;
		/// <summary>
		/// 产品序列号
		/// </summary>
		public decimal RunningCardSequence
		{
			get { return _runningCardSequence; }
			set { _runningCardSequence = value; }
		}

		private string _segmentCode = string.Empty;
		/// <summary>
		/// 工段
		/// </summary>
		public string SegmentCode
		{
			get { return _segmentCode; }
			set { _segmentCode = value; }
		}

		private string _lineCode = string.Empty;
		/// <summary>
		/// 产线
		/// </summary>
		public string LineCode
		{
			get { return _lineCode; }
			set { _lineCode = value; }
		}

		private string _resourceCode = string.Empty;
		/// <summary>
		/// 资源代码
		/// </summary>
		public string ResourceCode
		{
			get { return _resourceCode; }
			set { _resourceCode = value; }
		}

		private string _opCode = string.Empty;
		/// <summary>
		/// 工序代码
		/// </summary>
		public string OPCode
		{
			get { return _opCode; }
			set { _opCode = value; }
		}

		private string _lotNo = string.Empty;
		/// <summary>
		/// 送检批号
		/// </summary>
		public string LotNo
		{
			get { return _lotNo; }
			set { _lotNo = value; }
		}

		private string _testResult = string.Empty;
		/// <summary>
		/// 测试结果
		/// </summary>
		public string TestResult
		{
			get { return _testResult; }
			set { _testResult = value; }
		}

		private string _machineTool = string.Empty;
		/// <summary>
		/// 测试工具
		/// </summary>
		public string MachineTool
		{
			get { return _machineTool; }
			set { _machineTool = value; }
		}

		private string _testUser = string.Empty;
		/// <summary>
		/// 测试人员
		/// </summary>
		public string TestUser
		{
			get { return _testUser; }
			set { _testUser = value; }
		}

		private int _testDate = 0;
		/// <summary>
		/// 测试日期
		/// </summary>
		public int TestDate
		{
			get { return _testDate; }
			set { _testDate = value; }
		}

		private int _testTime = 0;
		/// <summary>
		/// 测试时间
		/// </summary>
		public int TestTime
		{
			get { return _testTime; }
			set { _testTime = value; }
		}


		private ArrayList listTestData = new ArrayList();
		/// <summary>
		/// 测试数据列表
		/// </summary>
		public ArrayList ListTestData
		{
			get
			{
				return listTestData;
			}
		}

		/// <summary>
		/// 添加测试数据
		/// </summary>
		/// <param name="testData"></param>
		public void AddTestData(DataEntryTestData testData)
		{
			listTestData.Add(testData);
		}

		/// <summary>
		/// 添加测试数据
		/// </summary>
		public void AddTestData(string objectCode, object data)
		{
			DataEntryTestData testData = new DataEntryTestData();
			testData.ObjectCode = objectCode;
			testData.GroupSequence = 1;
			testData.Data = data;
			listTestData.Add(testData);
		}

		/// <summary>
		/// 添加测试数据
		/// </summary>
		public void AddTestData(string objectCode, decimal groupSequence, object data)
		{
			DataEntryTestData testData = new DataEntryTestData();
			testData.ObjectCode = objectCode;
			testData.GroupSequence = groupSequence;
			testData.Data = data;
			listTestData.Add(testData);
		}


	}


}
