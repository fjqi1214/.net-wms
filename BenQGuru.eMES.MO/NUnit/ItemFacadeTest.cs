#region system 
using System;
using System.Data;
using NUnit.Framework;
#endregion

#region project
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Web.Helper;
#endregion


/// ItemFacadeTest 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Crystal Chu
/// 创建日期:2005/03/24
/// 修改人:
/// 修改日期:
/// 描 述: 对itemFacade的测试
/// 版 本:	
/// </summary>
   


namespace BenQGuru.eMES.MOModel.UnitTest
{
	/// <summary>
	/// this is for itemFacadetest
	/// </summary>
	[TestFixture]
	public class ItemFacadeTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private ItemFacade itemFacade = null;
	    private string itemCode = "Item Code";


		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			itemFacade = new ItemFacade();
		}

		[Test]
		public void TestItemFacade_Add()
		{
			this.persistBroker.Execute(String.Format("delete from tblitem where itemcode = '{0}'",itemCode));

			Item item = this.itemFacade.CreateNewItem();
			item.ItemCode = itemCode;
			item.ItemUOM = "lot";
			item.ItemType ="lot";
			item.ItemControlType="No control";
			item.ItemUser = "test user";
			item.ItemDate = FormatHelper.TODateInt(DateTime.Today.ToShortDateString());
			item.MaintainUser = "test user";
			item.ItemDescription = "test add";
			
			this.itemFacade.AddItem(item);
			Assert.AreEqual(itemFacade.QueryItemCount(itemCode,string.Empty, string.Empty,string.Empty,string.Empty),1);

			

			
		}
		[Test]
		public void TestItemFacade_Update()
		{
			Item item = (Item) this.itemFacade.GetItem(itemCode);
			item.ItemDescription = "test update";
			this.itemFacade.UpdateItem(item);
			Assert.AreEqual(((Item)this.itemFacade.GetItem(itemCode)).ItemDescription,"test update");
		}
		[Test]
		public void TestItemFacade_Delete()
		{
			Item item = (Item)this.itemFacade.GetItem(itemCode);
			Assert.IsTrue(itemFacade.GetAllItem().Length>=1);
			this.itemFacade.DeleteItem(item);
			Assert.AreEqual(itemFacade.QueryItemCount(itemCode,string.Empty,string.Empty,string.Empty,string.Empty),0);
		}

		public string ItemCode
		{
			get
			{
				return this.itemCode;
			}
		}
	}
}