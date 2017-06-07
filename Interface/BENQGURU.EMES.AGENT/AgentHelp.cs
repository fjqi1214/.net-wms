using System;
using UserControl;

namespace BenQGuru.eMES.Agent
{
	/// <summary>
	/// AgentHelp 的摘要说明。
	/// </summary>
	public class AgentHelp
	{
		public AgentHelp()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public static string GetErrorMessage(Messages messages)
		{
			for (int i =0 ;i<messages.Count();i++)
			{
				Message message= messages.Objects(i);
				if (message.Type ==MessageType.Error)
				{
					if (message.Body==string.Empty)
						return MutiLanguages.ParserMessage(message.Exception.Message);
					else
						return MutiLanguages.ParserMessage(message.Body);
				}
				
			}
			return MutiLanguages.ParserMessage("$CS_System_unKnowError");
		}

		public static string getResCode(string code)
		{
			//资源代码规则是在其前面加模块代码
			if(AgentInfo.Module == "AOI")
			{
				return "AOI"+code;
			}
			else if(AgentInfo.Module == "ICT")
			{
				return "ICT"+code;
			}

			return code;
		}
		
	}
}
