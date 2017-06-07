using System;

namespace BenQGuru.eMES.PDAClient.Command
{
	/// <summary>
	/// AbstractCommand 的摘要说明。
	/// </summary>
	public abstract class AbstractCommand:ICommand
	{
		#region ICommand 成员

		public virtual void Execute()
		{
			// TODO:  添加 AbstractCommand.Execute 实现
		}

		public virtual void Hint()
		{
			// TODO:  添加 AbstractCommand.Hint 实现
		}

		public virtual void Update()
		{
			// TODO:  添加 AbstractCommand.Update 实现
		}

		#endregion
	}
}
