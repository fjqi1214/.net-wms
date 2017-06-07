
using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// CollectExcute 的摘要说明。
	/// </summary>
	public class CollectExcute
	{

		private IDomainDataProvider _domainDataProvider = null;

		public CollectExcute(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public UserControl.Messages Excute(string module,string filePath,string encoding)
		{
			IAgentAction agentAction = (new AgentActionFactory(_domainDataProvider)).GetAgentAction(module);
			return agentAction.CollectExcute(filePath,encoding);
		}
	}
}
