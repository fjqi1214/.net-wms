using System;
using System.Data;
using NUnit.Framework;

using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.BaseSetting.UnitTest
{
	/// <summary>
	/// ShiftModelTest 的摘要说明。
	/// </summary>

	[TestFixture]
	public class ShiftModelTest
	{
		public ShiftModelTest()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private OLEDBPersistBroker broker = null;
		private ShiftModelFacade facade = null;

		[SetUp]
		public void SetUp()
		{
			broker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			facade = new ShiftModelFacade();
		}
		
		[Test]
		public void TestShiftType()
		{
			facade.QueryShiftTypeCount("code");		
			facade.QueryShiftType("code", 0, 10);	
			facade.GetAllShiftType();
		}

		
		[Test]
		public void TestShift()
		{
			facade.QueryShiftCount("code","code");		
			facade.QueryShift("code","code", 0, 10);	
			facade.GetShift("code");
			facade.GetShiftByShiftTypeCode("code");
			facade.GetAllShift();
		}
				
		[Test]
		public void TestTimePeriod()
		{
			facade.QueryTimePeriodCount("code", "code");		
			facade.QueryTimePeriod("code","code", 0, 10);	
			facade.GetTimePeriod("code");
			facade.GetAllTimePeriod();
			facade.GetTimePeriodByShiftCode("code");
		}

		[Test]
		public void TestShiftOverlap()
		{

		}
	}
}
