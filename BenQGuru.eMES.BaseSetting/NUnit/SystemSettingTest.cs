using System;
using NUnit.Framework;

using BenQGuru.eMES.Common.PersistBroker;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.BaseSetting.UnitTest
{
	/// <summary>
	/// SystemSettingTest 的摘要说明。
	/// </summary>
	[TestFixture]
	public class SystemSettingTest
	{
		public SystemSettingTest()
		{
		}

		private OLEDBPersistBroker broker = null;
		private SystemSettingFacade facade = null;
		private FacadeHelper helper = null;
		private Module module = new Module();
		private UserGroup2Module relation = new UserGroup2Module();

		[SetUp]
		public void SetUp()
		{
			broker = new OLEDBPersistBroker("Provider=OraOLEDB.Oracle.1;Password=emes;Persist Security Info=True;User ID=emes;Data Source=sqcdemo");
			facade = new SystemSettingFacade();
			helper = new FacadeHelper( this.facade.DataProvider );

			module.ModuleCode = "segment";
			module.ModuleType = "page";
			module.ModuleStatus = "1";
			module.ModuleSequence = 1;
			module.MaintainUser = "admin";
			module.MaintainDate = 20050311;
			module.MaintainTime = 115311;
			module.IsSystem = "1";
			module.IsActive = "1";
			module.FormUrl = "FSegmentMP.aspx";

			relation.ModuleCode = "segment";
			relation.UserGroupCode = "admin";
			this.helper.DeleteDomainObject( relation );
			relation.MaintainUser = "admin";
			relation.MaintainDate = 20050311;
			relation.MaintainTime = 115311;
			relation.ViewValue = "1111";

			this.helper.DeleteDomainObject( relation );
			this.helper.DeleteDomainObject( module );
		}

		[TearDown]
		public void Clear()
		{
			this.helper.DeleteDomainObject( relation );
			this.helper.DeleteDomainObject( module );
		}

		[Test]
		public void TestParameterGroup()
		{
			this.facade.QueryParameterGroupCount("code","code");
			this.facade.QueryParameterGroup("code","code", 1, 10);
		}
	
		[Test]
		public void TestParameter()
		{
			this.facade.QueryParameterCount("code","code");
			this.facade.QueryParameter("code","code", 1, 10);
		}
	
		[Test]
		public void TestModule()
		{
			this.facade.GetSubModulesByModuleCode("");
			this.facade.GetSubModulesByModuleCode("BaseSetting");


			this.facade.GetSubModulesByModuleCodeCount("");
			this.facade.GetSubModulesByModuleCodeCount("BaseSetting");


			this.facade.GetSubModulesByModuleCode("", 1, 10);
			this.facade.GetSubModulesByModuleCode("BaseSetting", 1, 10);
			
			this.facade.GetSelectedModuleWithViewValueByUserGroupCode("code","code",1,10);
		}
	
		[Test]
		public void TestMenu()
		{
			this.facade.GetSubMenusByMenuCode("");
			this.facade.GetSubMenusByMenuCode("basesetting");
			
			this.facade.GetSubMenusByMenuCode("", 1, 10);
			this.facade.GetSubMenusByMenuCode("basesetting", 1, 10);
			
			this.facade.GetSubMenusByMenuCodeCount("");
			this.facade.GetSubMenusByMenuCodeCount("basesetting");

			this.facade.GetAllMenuWithUrl();
		}
	
		[Test]
		public void TestUserGroup2Module()
		{			
			this.facade.GetModuleByUserGroupCode("code");

			this.facade.GetSelectedModuleByUserGroupCodeCount("code","code");
			this.facade.GetSelectedModuleByUserGroupCode("code","code",1,10);

			this.facade.GetUnselectedModuleByUserGroupCodeCount("code","code");
			this.facade.GetUnselectedModuleByUserGroupCode("code","code",1,10);
		}
	}
}
