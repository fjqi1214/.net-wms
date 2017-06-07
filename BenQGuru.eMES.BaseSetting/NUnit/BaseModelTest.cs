using System;
using System.Data;
using NUnit.Framework;

using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.BaseSetting.UnitTest
{
	/// <summary>
	/// BaseModelTest 的摘要说明。
	/// </summary>

	[TestFixture]
	public class BaseModelTest
	{
		private OLEDBPersistBroker broker = null;
		private BaseModelFacade facade = null;
		public BaseModelTest()
		{
		}

		[SetUp]
		public void SetUp()
		{
			broker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			facade = new BaseModelFacade();
		}

		public void TestSegment()
		{
			this.broker.Execute("delete from TBLSS where SSCODE='code'");
			this.broker.Execute("delete from tblseg where segcode='code'");

			string sql = "select count(*) from tblseg where segcode='code'";
			Segment obj = facade.CreateNewSegment();

			obj.SegmentCode = "code";
			obj.SegmentSequence = 0;
			obj.SegmentDescription = "";
			obj.ShiftTypeCode = "code";
			obj.MaintainUser = "admin";

			facade.AddSegment( obj );

			DataSet ds = this.broker.Query(sql);
			Assert.AreEqual( System.Int32.Parse(ds.Tables[0].Rows[0][0].ToString()), 1);
			Assert.AreEqual( facade.QuerySegmentCount(obj.SegmentCode), 1);
			
			obj.SegmentDescription = "description";
			facade.UpdateSegment( obj );
			Segment result = (Segment)facade.GetSegment("code");
			Assert.AreEqual( result.SegmentDescription, "description");

			Assert.IsTrue( facade.GetAllSegment().Length > 0 );
			int count = facade.QuerySegment("code", 0, 10).Length;
			Assert.IsTrue( count > 0 && count <= 10 );
			Assert.IsTrue( facade.QuerySegmentCount("code") > 0 );

			facade.DeleteSegment( obj );	
			ds = this.broker.Query(sql);
			Assert.AreEqual( System.Int32.Parse(ds.Tables[0].Rows[0][0].ToString()), 0);
		}

		public void TestStepSequence()
		{
			this.broker.Execute("delete from TBLSS where SSCODE='code'");
			this.broker.Execute("delete from tblseg where segcode='code'");

			Segment segment = facade.CreateNewSegment();

			segment.SegmentCode = "code";
			segment.SegmentSequence = 0;
			segment.SegmentDescription = "";			
			segment.ShiftTypeCode = "code";
			segment.MaintainUser = "admin";

			facade.AddSegment( segment );

			string sql = "select count(*) from TBLSS where sscode='code'";

			StepSequence obj = facade.CreateNewStepSequence();

			obj.StepSequenceCode = "code";
			obj.StepSequenceOrder = 0;
			obj.StepSequenceDescription = "";
			obj.SegmentCode = "code";
			obj.MaintainUser = "admin";

			facade.AddStepSequence( obj );

			DataSet ds = this.broker.Query(sql);
			Assert.AreEqual( System.Int32.Parse(ds.Tables[0].Rows[0][0].ToString()), 1);
			Assert.AreEqual( facade.QueryStepSequenceCount(obj.StepSequenceCode, "code"), 1);
			
			obj.StepSequenceDescription = "description";
			facade.UpdateStepSequence( obj );
			StepSequence result = (StepSequence)facade.GetStepSequence("code");
			Assert.AreEqual( result.StepSequenceDescription, "description");

			Assert.IsTrue( facade.GetAllStepSequence().Length > 0 );
			Assert.IsTrue( facade.QueryStepSequence("code", "code", 0, 20).Length > 0 );
			Assert.IsTrue( facade.QueryStepSequenceCount("code", "code") > 0 );

			facade.DeleteSegment( segment );

//			facade.DeleteStepSequence( obj );	
//			ds = this.broker.Query(sql);
//			Assert.AreEqual( System.Int32.Parse(ds.Tables[0].Rows[0][0].ToString()), 0);
		}

		[Test]
		public void TestResource()
		{
			facade.QueryResourceCount("code");		
			facade.QueryResource("code", 0, 10);	
			facade.GetAllResource();
		}

		[Test]
		public void TestOperation()
		{
			facade.QueryOperationCount("code");		
			facade.QueryOperation("code", 0, 10);	
			facade.GetAllOperation();
		}

		[Test]
		public void TestRoute()
		{
			facade.QueryRouteCount("code");		
			facade.QueryRoute("code", 0, 10);	
			facade.GetAllRoute();
		}	

		[Test]
		public void TestRoute2Operation()
		{
			facade.GetRoute2Operation("","");
			facade.GetSelectedResourceOfOperationByOperationCode("","", 0, 10);
			facade.GetSelectedOperationByRouteCodeCount("","");
			facade.GetSelectedOperationByRouteCode("","",1,10);
			facade.GetSelectedOperationOfRouteByRouteCode("","",1,10);
			facade.GetUnselectedOperationByRouteCode("","",1,100);
			facade.GetUnselectedOperationByRouteCodeCount("","");

			facade.GetOperationByRouteAndResource("code","code");
			facade.GetFirstOperationOfRoute("code");
			facade.GetNextOperationOfRoute("code","code");
		}

		[Test]
		public void TestOperation2Resource()
		{
			facade.GetOperation2Resource("","");							
			facade.GetUnselectedResourceByOperationCode("","",1,10);
			facade.GetUnselectedResourceByOperationCodeCount("","");
			facade.GetSelectedResourceByOperationCode("","",1,100);
			facade.GetSelectedResourceByOperationCodeCount("","");
		}

		[Test]
		public void TestRoute2Operation2Resource()
		{
			facade.GetSelectedResourceByRoute2OperationCount("","","");
			facade.GetSelectedResourceOfOperationByRoute2Operation("","","",1,10);
			facade.GetUnselectedResourceByRoute2OperationCount("","","");
			facade.GetUnselectedResourceByRoute2Operation("","","",1,10);
		}
	}
}
