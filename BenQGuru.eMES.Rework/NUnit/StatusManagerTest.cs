#region system 
using System;
using System.Data;
using NUnit.Framework;
#endregion

#region project
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.Rework.UnitTest
{

	/// <summary>
	/// this is for StatusManager Test
	/// </summary>
	[TestFixture]
	public class StatusManagerTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private ReworkFacade reworkFacade = null;
		private ItemFacade itemFacade = null;
		private string itemCode = "ItemCode";
		private string sourceCode ="SourceCode";
		private string reworkCode ="ReworkCode";
		
		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			reworkFacade = new ReworkFacade();
			itemFacade = new ItemFacade();
			 
		}
		[Test]public void TestStatusManager()
		{
			//add item
			this.persistBroker.Execute(String.Format("delete from tblreworksheet where reworkcode ='{0}'",reworkCode));
			this.persistBroker.Execute(String.Format("delete from tblreworksource where reworkscode = '{0}'",sourceCode));
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
			Assert.AreEqual(itemFacade.QueryItemCount(itemCode,string.Empty,string.Empty,string.Empty,string.Empty),1);



			//add sourceitemoce
		
			
			ReworkSource reworkSource = this.reworkFacade.CreateNewReworkSource();
			reworkSource.ReworkSourceCode = sourceCode;
			reworkSource.MaintainUser = "test user";
			reworkSource.Description = "this is for test!";
			this.reworkFacade.AddReworkSource(reworkSource);
			Assert.AreEqual(reworkFacade.QueryReworkSourceCount(sourceCode),1);
			
			
			//add reworksheet
			ReworkSheet reworkSheet = this.reworkFacade.CreateNewReworkSheet();
			reworkSheet.ItemCode = itemCode;
			reworkSheet.ReworkCode = reworkCode;
			reworkSheet.CreateDate = FormatHelper.TODateInt(DateTime.Today.ToShortDateString());
			reworkSheet.CreateTime = FormatHelper.TOTimeInt(DateTime.Now.ToLongTimeString());
			reworkSheet.CreateUser = "test user";
			reworkSheet.Department = "rd department";
			reworkSheet.MaintainUser = "test user";
			reworkSheet.MOCode ="MOCode";
			reworkSheet.NewMOCode ="New MOCode";
			reworkSheet.NewMOType = "NewMOType";
			reworkSheet.ReworkCode ="ReworkCode";
			reworkSheet.ReworkContent = "ReworkContent";
			reworkSheet.ReworkDate = FormatHelper.TODateInt(DateTime.Today.ToShortDateString());
			reworkSheet.ReworkHC = 0;
			reworkSheet.ReworkMaxQty =10;
			reworkSheet.ReworkQty = 1;
			reworkSheet.ReworkRealQty = 1;
			reworkSheet.ReworkSourceCode =sourceCode;
			reworkSheet.ReworkTime =FormatHelper.TOTimeInt(DateTime.Now.ToLongTimeString());
			reworkSheet.ReworkType = "rework Type";
			reworkSheet.Status = ReworkFacade.REWORKSTATUS_NEW;
			this.reworkFacade.AddReworkSheet(reworkSheet);
			Assert.AreEqual(this.reworkFacade.QueryReworkSheetCount(reworkSheet.ReworkCode),1);

			StatusManager statusManager = new StatusManager(this.reworkFacade,reworkSheet);
			statusManager.Waiting();
			ReworkSheet currentReworkSheet = (ReworkSheet)this.reworkFacade.GetReworkSheet(reworkSheet.ReworkCode);
			Assert.AreEqual(currentReworkSheet.Status, ReworkFacade.REWORKSTATUS_WAITING);
			statusManager.NOApprove();
			ReworkSheet currentReworkSheet1 = (ReworkSheet)this.reworkFacade.GetReworkSheet(reworkSheet.ReworkCode);
			Assert.AreEqual(currentReworkSheet1.Status, ReworkFacade.REWORKSTATUS_NEW);
			statusManager.Waiting();
			ReworkSheet currentReworkSheet2 = (ReworkSheet)this.reworkFacade.GetReworkSheet(reworkSheet.ReworkCode);
			Assert.AreEqual(currentReworkSheet2.Status, ReworkFacade.REWORKSTATUS_WAITING);
			statusManager.Approve();
			ReworkSheet currentReworkSheet3 = (ReworkSheet)this.reworkFacade.GetReworkSheet(reworkSheet.ReworkCode);
			Assert.AreEqual(currentReworkSheet3.Status, ReworkFacade.REWORKSTATUS_OPEN);
		}
		
	}
		
}