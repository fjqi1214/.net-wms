#region system 
using System;
using System.Data;
using NUnit.Framework;
using System.IO;
#endregion

#region project
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;
using UserControl;
#endregion


/// DataCollectFacadeTest 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Mark Lee
/// 创建日期:2005/03/24
/// 修改人:
/// 修改日期:
/// 描 述: 对DataCollectFacade的测试
/// 版 本:	
/// </summary>
   


namespace BenQGuru.eMES.DataCollect.UnitTest
{
	/// <summary>
	/// this is for DataCollectFacadeTest
	/// </summary>
	[TestFixture]
	public class DataCollectFacadeTest
	{
		private OLEDBPersistBroker persistBroker = null;
		private DataCollectFacade dataCollectFacade = null;
	    private string RuncardCode = "PC001";


		[SetUp]
		public void SetUp()
		{
			persistBroker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			dataCollectFacade = new DataCollectFacade();
		}
		/// <summary>
		/// 测试SIMULATION的添加、删除、修改
		/// </summary>
		[Test]
		public void TestSimulation()
		{
			//
			Simulation simulation=dataCollectFacade.CreateNewSimulation();
			
			simulation.RunningCard=RuncardCode;
			simulation.RunningCardSequence =1;
			simulation.TranslateCard=RuncardCode;
			simulation.TranslateCard="1";
			simulation.SourceCard=RuncardCode;
			simulation.SourceCardSequence=1;
			simulation.MOCode="MO3";
			simulation.ItemCode="ITEM01";
			simulation.ModelCode="M01";
			simulation.IDMergeRule =1;
			simulation.IsComplete ="0";
			simulation.LastAction =ActionType.DataCollectAction_GoMO;
			simulation.ProductStatus ="GOOD";
			simulation.MaintainUser ="MarkDebug";
			simulation.MaintainDate =20050520;
			simulation.MaintainTime =90301;
			//simulation.m
			//this.persistBroker.Execute(String.Format("delete from TBLSIMULATION where RCARD = '{0}' and MOCODE='{1}'",RuncardCode,simulation.MOCode));
			this.dataCollectFacade.DeleteSimulation(simulation);			
			this.dataCollectFacade.AddSimulation(simulation);
			Simulation s2=(Simulation)dataCollectFacade.GetSimulation(RuncardCode);			
			Assert.AreEqual(s2.ToString(),simulation.ToString());
			
			///Angel zhu Add :simulation update
			simulation.RunningCard = RuncardCode;
			simulation.RunningCardSequence = 2 ;
			simulation.TranslateCard  = RuncardCode;
			simulation.SourceCard = RuncardCode;
			simulation.SourceCardSequence = 2;
			simulation.MOCode = "MO2";
			simulation.ItemCode = "ITEM02";
			simulation.ModelCode ="M02";
			simulation.IDMergeRule = 2;
			simulation.IsComplete = "0";
            simulation.LastAction = ActionType.DataCollectAction_GoMO;
			simulation.ProductStatus = "GOOD";
			simulation.MaintainUser ="AngelDebug";
			simulation.MaintainDate = 20050523;
			simulation.MaintainTime = 151606;

			this.dataCollectFacade.UpdateSimulation(simulation);
			Simulation stest = (Simulation)dataCollectFacade.GetSimulation(RuncardCode);
			Console.WriteLine(simulation.ToString());
			Console.WriteLine(stest.ToString());
			Assert.AreEqual(stest.ToString(),simulation.ToString());

		}
		
		/// <summary>
		///Angel Zhu Add TEST :Onwip 
		/// </summary>
		public void TestOnWip()
		{
			OnWIP onwip = dataCollectFacade.CreateNewOnWIP();

			onwip.RunningCard = RuncardCode;
			onwip.MaintainTime = 165102;
			onwip.MaintainDate = 20050523;
			onwip.MaintainUser = "Angel Debug";
			onwip.RunningCardSequence = 1;
			onwip.ModelCode = "Model0";
			onwip.MOCode = "MO1";
			onwip.TranslateCard = "card1";
			onwip.SourceCard = RuncardCode;
			onwip.Action ="Action1";
			onwip.ActionResult = "Ok";
			onwip.ShiftDay = 20050523;
			onwip.TranslateCardSequence =1;
			onwip.SourceCardSequence =1;
			onwip.NGTimes = 165702;
			onwip.ItemCode ="Item1";
			onwip.TimePeriodCode = "period1";
			onwip.ShiftCode = "shift1";
			onwip.ShiftTypeCode = "shifttype1";

			this.dataCollectFacade.DeleteOnWIP(onwip);
			this.dataCollectFacade.AddOnWIP(onwip);
            
			onwip.RunningCard = RuncardCode;
			onwip.MaintainTime = 170000;
			onwip.MaintainDate = 20050523;
			onwip.MaintainUser = "Angel Debug1";
			onwip.RunningCardSequence = 2;
			onwip.ModelCode = "Model1";
			onwip.MOCode = "MO2";
			onwip.TranslateCard = "card2";
			onwip.SourceCard = RuncardCode;
			onwip.Action ="Action2";
			onwip.ActionResult = "Ok";
			onwip.ShiftDay = 20050523;
			onwip.TranslateCardSequence =1;
			onwip.SourceCardSequence =1;
			onwip.NGTimes = 165702;
			onwip.ItemCode ="Item2";
			onwip.TimePeriodCode = "period2";
			onwip.ShiftCode = "shift2";
			onwip.ShiftTypeCode = "shifttype2";

			this.dataCollectFacade.UpdateOnWIP(onwip);		




		}

		
	}
}