using System;

namespace BenQGuru.eMES.PDAClient.Command
{
	/// <summary>
	/// ICommand 的摘要说明。
	/// </summary>
	public interface ICommand
	{
		void Execute();
		void Update();
	}
}
