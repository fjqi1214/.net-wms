#region system 
using System;
using System.Data;
using NUnit.Framework;
#endregion

#region project
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
#endregion

namespace BenQGuru.eMES.MOModel.UnitTest
{

	/// <summary>
	/// this is for OPItemControlFacade Test
	/// </summary>
	[TestFixture]
	public class OPItemControlFacadeTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private OPItemControlFacade opItemControlFacade = null;
		private string itemCode = "Item Code";
		private string opBOMCode = "test opBomCode";
		private string opBOMVersion = "111";
		private string opID ="846dfeb6-9454-48f0-b13a-ae9169ee49e6";
		private string opBOMItemCode = "test itemcode";
		
		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			opItemControlFacade = new OPItemControlFacade();
		}
		[Test]public void OPItemControlFacadeTest_AddOPItemControl()
		{
			OPItemControl opItemControl = this.opItemControlFacade.CreateNewOPItemControl();
			opItemControl.ItemCode = itemCode;
			opItemControl.ItemVersion = "ItemVersion";
			opItemControl.MaintainUser ="test user";
			opItemControl.MEMO = "this is for test !";
//			opItemControl.OBCode = opBOMCode;
			opItemControl.OPBOMItemCode = opBOMItemCode;
			opItemControl.DateCodeStart = "4444";
			opItemControl.DateCodeEnd ="5555";
			opItemControl.BIOSVersion = "BIOSVersion";
			opItemControl.OPBOMVersion = opBOMVersion;
			opItemControl.OPID = opID;
			opItemControl.PCBAVersion ="PCBAVersion";
			opItemControl.Sequence = 0;
			opItemControl.VendorCode ="Vendor Code";
			opItemControl.VendorItemCode ="Vender ItemCode";
			opItemControl.CardStart = "1111111";
			opItemControl.CardEnd = "5555555";
			this.opItemControlFacade.AddOPItemControl(opItemControl);
			Assert.AreEqual(this.opItemControlFacade.GetOPBOMItemControlCounts(itemCode,opID,opBOMItemCode,opBOMCode,opBOMVersion),1);
		}
		[Test]public void OPItemControlFacadeTest_UpdateOPItemControl()
		{
			OPItemControl opItemControl = (OPItemControl)this.opItemControlFacade.GetOPBOMItemControl(itemCode,opID,opBOMItemCode,opBOMCode,opBOMVersion,0);
			opItemControl.MEMO = "this is for update";
			this.opItemControlFacade.UpdateItemControl(opItemControl);
			Assert.AreEqual(((OPItemControl)this.opItemControlFacade.GetOPBOMItemControl(itemCode,opID,opBOMItemCode,opBOMCode,opBOMVersion,0)).MEMO,"this is for update");
		}
		[Test]public void OPItemControlFacadeTest_DeleteOPItemControl()
		{
			OPItemControl opItemControl = (OPItemControl)this.opItemControlFacade.GetOPBOMItemControl(itemCode,opID,opBOMItemCode,opBOMCode,opBOMVersion,0);
		    this.opItemControlFacade.DeleteItemControl(opItemControl);
			Assert.AreEqual(this.opItemControlFacade.GetOPBOMItemControlCounts(itemCode,opID,opBOMItemCode,opBOMCode,opBOMVersion),0);
		}
		
	}
		
}