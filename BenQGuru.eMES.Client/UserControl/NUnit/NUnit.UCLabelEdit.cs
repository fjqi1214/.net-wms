using System;
using NUnit.Framework;
using UserControl;

namespace nunit
{
	/// <summary>
	/// NUit 的摘要说明。
	/// </summary>
	[TestFixture]
	public class NUnit_UCLabelEdit
	{
		public NUnit_UCLabelEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private UCLabelEdit uCLabelEdit;
		[SetUp]
		public void SetUp()
		{
			uCLabelEdit = new UCLabelEdit();
		}

		[Test]
		public void UCLabelEdit_Test()
		{
			uCLabelEdit.Caption = "测试1";
			Assert.AreEqual(uCLabelEdit.Caption , "测试1");
			
			uCLabelEdit.Value = "123";
			Assert.AreEqual(uCLabelEdit.Value , "123");

			int _xAlign = uCLabelEdit.XAlign;

			uCLabelEdit.WidthType = WidthTypes.Normal;
			Assert.AreEqual(uCLabelEdit.XAlign,_xAlign);

			uCLabelEdit.WidthType = WidthTypes.Long;
			Assert.AreEqual(uCLabelEdit.XAlign,_xAlign);

			uCLabelEdit.WidthType = WidthTypes.Small;
			Assert.AreEqual(uCLabelEdit.XAlign,_xAlign);

//			uCLabelEdit.MaxLength = 5;
//			Assert.AreEqual(uCLabelEdit.Value , "123456");
		}
	}
}
