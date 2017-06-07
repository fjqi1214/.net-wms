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


/// ModelFacadeTest 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Crystal Chu
/// 创建日期:2005/03/24
/// 修改人:
/// 修改日期:
/// 描 述: 对ModelFacadeTest的测试
/// 版 本:	
/// </summary>
   


namespace BenQGuru.eMES.MOModel.UnitTest
{
	/// <summary>
	/// this is for ModelFacadetest
	/// </summary>
	[TestFixture]
	public class ModelFacadeTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private ModelFacade modelFacade = null;
		private ItemFacade itemFacade = null;
		private BaseModelFacade baseModelFacade = null;
		private string itemCode = "Item Code";
		private string modelCode = "Model Code";
		private string routeCode = "Route_DIP";
		private string routeAltCode = "RouteAlt Code";
		

		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			modelFacade = new ModelFacade();
			itemFacade = new ItemFacade();
			baseModelFacade = new BaseModelFacade();
		}

		[Test]
		public void TestModelFacade_Add()
		{
			this.persistBroker.Execute(String.Format("delete from tblmodel where modelcode  = '{0}'",modelCode));

			//add
			Model model = modelFacade.CreateNewModel();
			model.ModelCode = modelCode;
			model.ModelDescription = "this is for test";
			model.MaintainUser ="test user";

			this.modelFacade.AddModel(model);
			Assert.AreEqual(this.modelFacade.QueryModelsCount(modelCode),1);
		}

		[Test]
		public void TestModelFacade_Update()
		{
			Model model = (Model)this.modelFacade.GetModel(modelCode);
			model.ModelDescription = "this is test for update";
			this.modelFacade.UpdateModel(model);
			Assert.AreEqual(((Model)this.modelFacade.GetModel(modelCode)).ModelDescription,"this is test for update");
		}

		[Test]
		public void TestModelFacade_AddModel2Item()
		{
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

			Item[] items = new Item[1];
			items[0] = (Item)this.itemFacade.GetItem(itemCode);
			this.modelFacade.AssignItemsToModel(modelCode,items);
			Assert.AreEqual( this.modelFacade.GetSelectedItemsCounts(modelCode,string.Empty),1);
		}
		[Test]
		public void TestModelFacade_AddModel2Routealt()
		{
			this.persistBroker.Execute(String.Format("delete from tblroutealt2route where routealtcode='{0}'",routeAltCode));
			this.persistBroker.Execute(String.Format("delete from tblroutealt where routealtcode='{0}'",routeAltCode));
			RouteAlt routeAlt = this.baseModelFacade.CreateNewRouteAlt();
			routeAlt.RouteAltCode = routeAltCode;
			routeAlt.RouteAltDescription = "this is for test!";
			routeAlt.RouteAltType ="routeAltType";
			routeAlt.MaintainUser = "test user";
			this.baseModelFacade.AddRouteAlt(routeAlt);

			RouteAlt[] routeAlts = new RouteAlt[1];
			routeAlts[0] = routeAlt;
//			this.modelFacade.AssginRouteAltsToModel(modelCode,routeAlts);
//			Assert.AreEqual(this.modelFacade.GetSelectedRouteAltsCounts(modelCode),1);
		}
		[Test]
		public void TestModelFacade_AddModel2Route()
		{
			this.persistBroker.Execute(String.Format("delete from tblroutealt2route where routealtcode='{0}' and routecode='{1}'",routeAltCode,routeCode));
			object[] routes = this.baseModelFacade.GetUnselectedRouteByRouteAltCode(routeAltCode,string.Empty,int.MinValue,int.MaxValue);
			Assert.IsNotNull(routes);
			RouteAlt2Route routeAlt2Route = this.baseModelFacade.CreateNewRouteAlt2Route();
			routeAlt2Route.RouteAltCode = routeAltCode;
			routeAlt2Route.RouteCode = routeCode;
			routeAlt2Route.RouteSequence =0;
			routeAlt2Route.MaintainUser ="test user";
			this.baseModelFacade.AddRouteAlt2Route(routeAlt2Route);
			routeCode = ((Route)routes[0]).RouteCode;
			Assert.AreEqual(this.baseModelFacade.GetSelectedRouteByRouteAltCodeCount(routeAltCode,routeCode),1);

			object[] operations = this.baseModelFacade.GetUnselectedOperationByRouteCode(routeCode,string.Empty,int.MinValue,int.MaxValue);
			Assert.IsNotNull(operations);
			Route2Operation route2Operation = this.baseModelFacade.CreateNewRoute2Operation();
			route2Operation.OPCode = ((Operation)operations[0]).OPCode;
			route2Operation.MaintainUser = "test uesr";
			route2Operation.OPSequence =0;
			route2Operation.RouteCode = routeCode;
			this.baseModelFacade.AddRoute2Operation(route2Operation);
			Assert.AreEqual(this.baseModelFacade.GetSelectedOperationByRouteCodeCount(routeCode,((Operation)operations[0]).OPCode),1);

		
			Model2Route model2Route = this.modelFacade.CreateNewModel2Route();
			model2Route.RouteCode = routeCode;
//			model2Route.RouteAltCode = routeAltCode;
			model2Route.ModelCode = modelCode;
			model2Route.MaintainUser= "test user";
			this.modelFacade.AddModelRoute(model2Route);
			Assert.AreEqual(this.modelFacade.GetModel2RoutesCounts(modelCode,routeCode),1);

			object[] model2ops = this.modelFacade.GetModel2Operations(modelCode,routeCode);
			Assert.IsNotNull(model2ops);

			Model2OP model2OP = (Model2OP)this.modelFacade.GetModel2Operation(((Model2OP)model2ops[0]).OPID);
			model2OP.OPControl = "000000000";
			this.modelFacade.UpdateModel2Operation(model2OP);
			Assert.AreEqual(((Model2OP)modelFacade.GetModel2Operation(((Model2OP)model2ops[0]).OPID)).OPControl,"000000000");

		}

		[Test]
		public void TestModelFacade_DeleteModel2Route()
		{
			modelCode = "Model Code";
		    routeCode = "Route_DIP";
			Model2Route model2Route = this.modelFacade.CreateNewModel2Route();
			model2Route.RouteCode = routeCode;
			model2Route.ModelCode = modelCode;
//			model2Route.RouteAltCode = routeAltCode;
			model2Route.MaintainUser ="test user";
			//Model2Route model2Route = (Model2Route) this.modelFacade.GetModel2Route(routeCode,modelCode);
			Model2Route[] model2Routes = new Model2Route[1];
			model2Routes[0] =model2Route;
			this.modelFacade.DeleteModelRoute(model2Routes);
			Assert.AreEqual(this.modelFacade.GetModel2RoutesCounts(modelCode,routeCode),0);
		}
	
		[Test]
		public void TestModelFacade_DeleteModel2Item()
		{
			Item[] items = new Item[1];
			ItemFacadeTest itemFacadeTest = new ItemFacadeTest();
			items[0] = (Item)this.itemFacade.GetItem(itemFacadeTest.ItemCode);
			this.modelFacade.RemoveItemsFromModel(modelCode,items);
			Assert.AreEqual(this.modelFacade.GetSelectedItemsCounts(modelCode,string.Empty),0);
		}
		[Test]
		public void TestModelFacade_DeleteModel()
		{
			this.modelFacade.DeleteModel((Model)this.modelFacade.GetModel(modelCode));
			Assert.AreEqual(this.modelFacade.QueryModelsCount(modelCode),0);
		}
	}
}