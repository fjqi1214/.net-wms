using System;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// AgentActionDefault 的摘要说明。
	/// </summary>
	public class AgentActionDefault : IAgentAction	
	{
		public AgentActionDefault()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region IAgentAction 成员

		public UserControl.Messages CollectExcute(string filePath, string encoding)
		{
			return null;
		}

		public UserControl.Messages GoodCollect(object[] parserObjs)
		{
			return null;
		}

		public UserControl.Messages NGCollect(object[] parserObjs)
		{
			return null;
		}

		#endregion
	}
}
