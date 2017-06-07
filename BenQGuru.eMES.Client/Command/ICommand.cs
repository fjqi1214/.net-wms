using System;

namespace BenQGuru.eMES.Client.Command
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
