using System;
using NUnit.Framework;
using UserControl;

namespace nunit
{
	/// <summary>
	/// NUnit 的摘要说明。
	/// </summary>
    [TestFixture] 
	public class NUnit_UCButton
	{
		public NUnit_UCButton()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private UCButton uCButton;
		[SetUp]
		public void SetUp()
		{
			 uCButton = new UCButton();
		}

		[Test]
		public void UCButton_Test()
		{
			uCButton.ButtonType = ButtonTypes.None;
			uCButton.Caption = "测试";
			Assert.AreEqual(uCButton.Caption, "测试");
			uCButton.ButtonType = ButtonTypes.Add;
			Assert.AreEqual(uCButton.Caption, "添加");
			uCButton.ButtonType = ButtonTypes.Cancle;
			Assert.AreEqual(uCButton.Caption, "取消");
			uCButton.ButtonType = ButtonTypes.Confirm;
			Assert.AreEqual(uCButton.Caption, "确认");
			uCButton.ButtonType = ButtonTypes.Delete;
			Assert.AreEqual(uCButton.Caption, "删除");
			uCButton.ButtonType = ButtonTypes.Edit;
			Assert.AreEqual(uCButton.Caption, "编辑");
			uCButton.ButtonType = ButtonTypes.Exit;
			Assert.AreEqual(uCButton.Caption, "退出");
			uCButton.ButtonType = ButtonTypes.Query;
			Assert.AreEqual(uCButton.Caption, "查询");
			uCButton.ButtonType = ButtonTypes.Refresh;
			Assert.AreEqual(uCButton.Caption, "刷新");
			uCButton.ButtonType = ButtonTypes.Save;
			Assert.AreEqual(uCButton.Caption, "保存");
			uCButton.ButtonType = ButtonTypes.Copy;
			Assert.AreEqual(uCButton.Caption, "复制");
			uCButton.ButtonType = ButtonTypes.AllLeft;
			Assert.AreEqual(uCButton.Caption, "<<");
			uCButton.ButtonType = ButtonTypes.AllRight;
			Assert.AreEqual(uCButton.Caption, ">>");
			uCButton.ButtonType = ButtonTypes.Change;
			Assert.AreEqual(uCButton.Caption, "更正");
			uCButton.ButtonType = ButtonTypes.Move;
			Assert.AreEqual(uCButton.Caption, "移除");
		}
	}
}
