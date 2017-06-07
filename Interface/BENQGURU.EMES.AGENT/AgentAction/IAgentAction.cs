using System;
using UserControl;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// IAgentCollect 的摘要说明。
	/// </summary>
	public interface IAgentAction
	{
		//执行采集
		UserControl.Messages CollectExcute(string filePath,string encoding);
		
		//Good采集
		UserControl.Messages GoodCollect(object[] parserObjs);
		
		//NG采集
		UserControl.Messages NGCollect(object[] parserObjs);
	}
}
