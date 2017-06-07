using System;
using NUnit.Framework;
using UserControl;

namespace nunit
{
	/// <summary>
	/// NUnit 的摘要说明。
	/// </summary>
	[TestFixture] 
	public class NUnit_UCDateTime
	{
		public NUnit_UCDateTime()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private UCDatetTime uCDatetTime;
		[SetUp]
		public void SetUp()
		{
			 uCDatetTime = new UCDatetTime();
			 uCDatetTime.Caption = "日期时间";
		}
		[Test]
		public void UCDatetTime_Test()
		{
			uCDatetTime.ShowType = DateTimeTypes.DateTime;
            int _xAlign = uCDatetTime.XAlign;
			uCDatetTime.ShowType = DateTimeTypes.Date;
			Assert.AreEqual(uCDatetTime.XAlign, _xAlign);
			uCDatetTime.ShowType = DateTimeTypes.Time;
			Assert.AreEqual(uCDatetTime.XAlign, _xAlign);

			Assert.AreEqual(uCDatetTime.Caption, "日期时间");
			
			DateTime _datetime=DateTime.Now;
			uCDatetTime.Value= _datetime;
			Assert.AreEqual(uCDatetTime.Value, _datetime);
		}
	}
}
