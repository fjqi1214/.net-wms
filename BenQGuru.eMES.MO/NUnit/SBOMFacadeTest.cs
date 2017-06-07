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
	/// this is for SBOMFacade Test
	/// </summary>
	[TestFixture]
	public class SBOMFacadeTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private SBOMFacade sbomFacade = null;
		//private ItemFacade itemFacade = null;
		private string itemCode = "Item Code";
		
		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			//itemFacade = new ItemFacade();
			sbomFacade = new SBOMFacade();
		}
		[Test]public void SBOmFacadeTest_AddSBOM()
		{
			SBOM sbom = this.sbomFacade.CreateSBOM();
			sbom.ItemCode = itemCode;
			sbom.MaintainUser ="test user";
			sbom.SBOMItemCode = "test itemcode";
			sbom.SBOMItemControlType ="lot";
			sbom.SBOMItemDescription = "this is for test!";
			sbom.SBOMItemECN ="11";
			sbom.SBOMItemEffectiveDate = FormatHelper.TODateInt(DateTime.Today.ToShortDateString());
			sbom.SBOMItemEffectiveTime = FormatHelper.TOTimeInt(DateTime.Now.ToLongTimeString());
			sbom.SBOMItemInvalidDate = FormatHelper.TODateInt(DateTime.Today.ToShortDateString());
			sbom.SBOMItemInvalidTime = FormatHelper.TOTimeInt(DateTime.Now.ToLongTimeString());
			sbom.SBOMItemLocation ="l1";
			sbom.SBOMItemName = "this is for test!";
			sbom.SBOMItemQty = 0;
			sbom.SBOMItemStatus ="0";
			sbom.Sequence =0;
			this.sbomFacade.AddSBOM(sbom);
			Assert.AreEqual(this.sbomFacade.GetSBOMCounts(itemCode),1);
		}
		
	}
		
}