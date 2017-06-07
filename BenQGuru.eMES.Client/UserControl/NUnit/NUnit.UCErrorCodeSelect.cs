using System;
using NUnit.Framework;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;  
using System.Windows.Forms;
using BenQGuru.eMES.Domain.TSModel;

namespace nunit
{
	/// <summary>
	/// Class1 的摘要说明。
	/// </summary>
	[TestFixture] 
	public class NUnit_UCErrorCodeSelect
	{
		private UserControl.UCErrorCodeSelect ucErrorCodeSelect1;
		private ErrorCodeGroupA errg = new ErrorCodeGroupA();
		private ErrorCodeA errc = new ErrorCodeA();	

		
		public NUnit_UCErrorCodeSelect()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			

			//ucErrorCodeSelect1.AddErrorGroup("errorgroup1");
			

		}
		[SetUp]
		public void SetUp()
		{
			ucErrorCodeSelect1 = new UserControl.UCErrorCodeSelect();
		}

		[Test]
		public void UCErrorCodeSelect_Test()
		{
			errg.ErrorCodeGroup = "errorgroup1";
			ucErrorCodeSelect1.AddErrorGroups(errg);
			errg.ErrorCodeGroup = "errorgroup2";
			ucErrorCodeSelect1.AddErrorGroup(errg);		

			errc.ErrorCode = "errorcode1";
			ucErrorCodeSelect1.AddErrorCode(errc);
			errc.ErrorCode = "errorcode2";
			ucErrorCodeSelect1.AddErrorCode(errc);

			ucErrorCodeSelect1.ClearErrorGroup();
			ucErrorCodeSelect1.ClearSelectedErrorCode();
			ucErrorCodeSelect1.ClearSelectErrorCode();

			ucErrorCodeSelect1.GetSelectedErrorCodes();

			Assert.AreEqual(ucErrorCodeSelect1.Count,0);

		}
	}
}
