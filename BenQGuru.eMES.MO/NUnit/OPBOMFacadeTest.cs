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
	/// this is for OPBOMFacade Test
	/// </summary>
	[TestFixture]
	public class OPBOMFacadeTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private OPBOMFacade opBOMFacade = null;
		private ModelFacade modelFacade = null;
		private SBOMFacade  sbomFacade = null;
		private string itemCode = "Item Code";
		private string routeCode = "Route_DIP";
		private string opBOMCode = "test opBomCode";
		private string opBOMVersion = "111";
		private string opID ="846dfeb6-9454-48f0-b13a-ae9169ee49e6";
		private string opBOMItemCode = "test itemcode";

		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			opBOMFacade = new OPBOMFacade();
			modelFacade = new ModelFacade();
			sbomFacade  = new SBOMFacade();
		}
		[Test]public void OPBOMFacadeTest_AddOPBOM()
		{
			OPBOM opBOM = opBOMFacade.CreateNewOPBOM();
			opBOM.ItemCode = itemCode;
			opBOM.MaintainUser ="test user";
			opBOM.OPBOMCode = "test opBomCode";
			opBOM.OPBOMDescription = "this is for test!";
			opBOM.OPBOMVersion ="111";
			opBOM.OPBOMRoute = routeCode;

			this.opBOMFacade.AddOPBOM(opBOM);
			//Assert.AreEqual(this.opBOMFacade.QueryOPBOMCount(itemCode,"test opBomCode","111"),1);
		}

		[Test]public void OPBOMFacadeTest_UpdateOPBOM()
		{
			OPBOM opBOM = (OPBOM)opBOMFacade.GetOPBOM(itemCode,opBOMCode,opBOMVersion);
			opBOM.OPBOMDescription = "this is for update test!";

			this.opBOMFacade.UpdateOPBOM(opBOM);
			//Assert.AreEqual(((OPBOM)opBOMFacade.GetOPBOM(itemCode,opBOMCode,opBOMVersion)).OPBOMDescription,"this is for update test!");
		}
		[Test]public void OPBOMFacadeTest_DeleteOPBOM()
		{
			OPBOM opBOM = (OPBOM)opBOMFacade.GetOPBOM(itemCode,opBOMCode,opBOMVersion);

			this.opBOMFacade.DeleteOPBOM(opBOM);
			//Assert.AreEqual(this.opBOMFacade.QueryOPBOMCount(itemCode,"test opBomCode","111"),0);
		}
		[Test]public void OPBOMFacadeTest_AddOPItem()
		{
			object[] objs = this.sbomFacade.GetSBOM(itemCode,int.MinValue,int.MaxValue);
			SBOM[] sboms = new SBOM[objs.Length];

			for(int i=0;i<objs.Length;i++)
			{
				sboms[i] = (SBOM)objs[i];
			}
			Model2OP model2Operation = (Model2OP)this.modelFacade.GetModel2Operation(opID);
			this.opBOMFacade.AssignBOMItemToOperation(opBOMCode,opBOMVersion,model2Operation,sboms);
		}
		[Test]public void OPBOMFacadeTest_UpdateOPBOMItem()
		{
//			OPBOMDetail opBOMDetail = (OPBOMDetail)this.opBOMFacade.GetOPBOMDetail(itemCode,opID,opBOMCode,opBOMVersion,opBOMItemCode);
////			opBOMDetail.RouteCode="this is a test!";
//			this.opBOMFacade.UpdateOPBOMItem(opBOMDetail);
//			Assert.AreEqual(((OPBOMDetail)this.opBOMFacade.GetOPBOMDetail(itemCode,opID,opBOMCode,opBOMVersion,opBOMItemCode)).RouteCode,"this is a test!");
		}
		[Test]public void OPBOMFacadeTest_DeleteOPBOMItem()
		{
			OPBOMDetail opBOMDetail = (OPBOMDetail)this.opBOMFacade.GetOPBOMDetail(itemCode,opID,opBOMCode,opBOMVersion,opBOMItemCode);
			this.opBOMFacade.DeleteOPBOMItem(opBOMDetail);
			//Assert.AreEqual(this.opBOMFacade.QueryOPBOMDetailCounts(itemCode,opID,opBOMCode,opBOMVersion,routeCode,String.Empty),0);
		}
		[Test]public void OPBOMFacadeTest_GetOPBOMDetail()
		{
			string moCode ="S711-0031200001";
			string route ="ROUTE_SMT";
			string opCode="OP_BTESTING";
			string opBOMItemCode="OPDD89";
			object opBOMDetail = this.opBOMFacade.GetOPBOMDetail(moCode,route,opCode,opBOMItemCode);
			Assert.IsNotNull(opBOMDetail);
		}
	}
		
}