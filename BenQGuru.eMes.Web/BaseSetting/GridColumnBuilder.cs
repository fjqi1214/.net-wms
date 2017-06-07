using System;
using Infragistics.WebUI.UltraWebGrid;

using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// GridColumnBuilder 的摘要说明。
	/// </summary>
	public abstract class GridColumnBuilder
	{
		protected UltraWebGrid _gridWebGrid = null;
		protected GridHelper _gridHelper = null;

		public GridColumnBuilder(UltraWebGrid grid)
		{
			this._gridWebGrid = grid;

			this._gridHelper = new GridHelper( this._gridWebGrid );
		}

		public abstract void Build();
	}

	public class OperationGridColumnBuilder : GridColumnBuilder
	{
		public OperationGridColumnBuilder(UltraWebGrid grid) : base(grid)
		{
		}

		public override void Build()
		{
			this._gridHelper = new GridHelper(this._gridWebGrid);

			this._gridHelper.AddColumn( "OPCode", "工序代码",	null);
			this._gridHelper.AddColumn( "OPDescription", "工序描述",	null);
			this._gridHelper.AddColumn( "OPCollectionType", "数据收集方式",	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl1", "是否途程检查",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl2", "是否上料检查",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl3", "是否需要MNID拆分",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl4", "是否工单流入",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl5", "是否工单流出",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl6", "是否总数量合计",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl7", "是否SPC统计",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl8", "是否包含出错代码",	false,	null);
			this._gridHelper.AddCheckBoxColumn( "OPControl9", "是否不良判定",	false,	null);
			
			this._gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this._gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this._gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
		}

	}

	public class ResourceGridColumnBuilder : GridColumnBuilder
	{
		public ResourceGridColumnBuilder(UltraWebGrid grid) : base(grid)
		{
		}

		public override void Build()
		{
			this._gridHelper = new GridHelper(this._gridWebGrid);

			this._gridHelper.AddColumn( "ResourceCode", "资源代码",	null);
			this._gridHelper.AddColumn( "ResourceType", "资源类别",	null);
			this._gridHelper.AddColumn( "ResourceGroup", "资源归属",	null);
			this._gridHelper.AddColumn( "ShiftTypeCode", "班制代码",	null);
			this._gridHelper.AddColumn( "StepSequenceCode", "生产线代码",	null);
			this._gridHelper.AddColumn( "SegmentCode", "工段代码",	null);
			this._gridHelper.AddColumn( "ResourceDescription", "资源描述",	null);
			this._gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this._gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this._gridHelper.AddColumn( "MaintainTime", "维护时间",	null);		
		}

	}

	public class UserGridColumnBuilder : GridColumnBuilder
	{
		public UserGridColumnBuilder(UltraWebGrid grid) : base(grid)
		{
		}

		public override void Build()
		{
			this._gridHelper.AddColumn( "UserCode", "用户代码",	null);
			this._gridHelper.AddColumn( "UserName", "用户名",	null);
			this._gridHelper.AddColumn( "UserTelephone", "电话号码",	null);
			this._gridHelper.AddColumn( "UserEmail", "电子信箱",	null);
			this._gridHelper.AddColumn( "UserDepartment", "部门",null);
            this._gridHelper.AddColumn("DefaultOrgDesc", "默认组织", null);
            this._gridHelper.AddColumn("UserStatus","用户状态",null);
			this._gridHelper.AddColumn( "MaintainUser", "维护用户",	null);
			this._gridHelper.AddColumn( "MaintainDate", "维护日期",	null);
			this._gridHelper.AddColumn( "MaintainTime", "维护时间",	null);
			//melo zheng 添加于2006.12.26 用于页面跳转,查询当前用户所在用户组
			this._gridHelper.AddLinkColumn("UserGroup", "用户组",null);
		}

        public void BuildForSelectPage()
        {
            this._gridHelper.AddColumn("UserCode", "用户代码", null);
            this._gridHelper.AddColumn("UserName", "用户名", null);
            this._gridHelper.AddColumn("UserTelephone", "电话号码", null);
            this._gridHelper.AddColumn("UserEmail", "电子信箱", null);
            this._gridHelper.AddColumn("UserDepartment", "部门", null);
            this._gridHelper.AddColumn("DefaultOrgDesc", "默认组织", null);
            this._gridHelper.AddColumn("MaintainUser", "维护用户", null);
            this._gridHelper.AddColumn("MaintainDate", "维护日期", null);
            this._gridHelper.AddColumn("MaintainTime", "维护时间", null);
        }
	}

}
