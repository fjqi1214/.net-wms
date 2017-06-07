using System;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// AgentActionFactory 的摘要说明。
	/// </summary>
	public class AgentActionFactory
	{
		private IDomainDataProvider _domainDataProvider = null;
		public AgentActionFactory(IDomainDataProvider domainDataProvider)
		{
			this._domainDataProvider = domainDataProvider;
		}

		public IAgentAction GetAgentAction(	string module )
		{
			switch(module)
			{
				case "AOI":
					return new AgentActionAOI(_domainDataProvider);
				case "PIDAOI":
					return new AgentActionPIDAOI(_domainDataProvider);
				case "ICT":
					return new AgentActionICT(_domainDataProvider);
				default:
					return new AgentActionDefault();
			}
		}
	}
}
