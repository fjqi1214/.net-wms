using System;
using BenQGuru.eMES.Domain.LotDataCollect;
using UserControl;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;


namespace BenQGuru.eMES.LotDataCollect.Action
{
	/// <summary>
	/// 数据采集模块接口
	/// 只供数据采集内部模块调用
	/// </summary>
	public interface IAction
	{
		Messages Execute(ActionEventArgs actionEventArgs);
	}

	// Added by Icyer 2005/10/28
	/// <summary>
	/// 扩展的数据采集接口
	/// 在Execute中增加一个参数：ActionCheckStatus，用以记录Action的操作
	/// </summary>
	public interface IActionWithStatus : IAction
	{
		Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus);
	}
    public interface IActionWithStatusNew : IAction
    {
        Messages Execute(ActionEventArgs actionEventArgs, ActionCheckStatus actionCheckStatus, object[] OPBOMinno);
    }
	// Added end

}
