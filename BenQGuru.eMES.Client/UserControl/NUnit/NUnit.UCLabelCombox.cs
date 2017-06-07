using System;
using NUnit.Framework;
using UserControl;

namespace nunit
{
	/// <summary>
	/// NUnit 的摘要说明。
	/// </summary>
	[TestFixture]
	public class NUnit_UCLabelCombox
	{
		public NUnit_UCLabelCombox()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private UCLabelCombox uCLabelCombox;
		[SetUp]
		public void SetUp()
		{
			uCLabelCombox= new UCLabelCombox();
		}

		[Test]
		public void UCLabelCombox_Test()
		{
			uCLabelCombox.Caption = "测试";
			Assert.AreEqual(uCLabelCombox.Caption,"测试");

			uCLabelCombox.AddItem("不良一","不良一");
			uCLabelCombox.SelectedIndex=0;

			Assert.AreEqual(uCLabelCombox.SelectedItemText,"不良一");
			Assert.AreEqual(uCLabelCombox.SelectedItemValue.ToString(),"不良一");
			
			uCLabelCombox.SelectedIndex=-1;
			uCLabelCombox.SetSelectItem("不良一");
			Assert.AreEqual(uCLabelCombox.SelectedIndex,0);
			Assert.AreEqual(uCLabelCombox.SelectedItemText,"不良一");
			Assert.AreEqual(uCLabelCombox.SelectedItemValue.ToString(),"不良一");
		}
	}
}
