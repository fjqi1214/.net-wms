using System;
using UserControl;

namespace BenQGuru.eMES.Common.DCT.Core
{
	/// <author>Laws Lu</author>
	/// <since>2006/04/14</since>
	/// <version>1.0.0</version>
	public abstract class BaseDCTAction
	{
		public BaseDCTAction()
		{
		}

		#region 变量
		protected Message input_message = null;

		protected int current_sequence;

		protected int repet_times;

		protected FlowDirect flow_direct;

		protected ActionStatus action_status = ActionStatus.PrepareData;
		//输出信息
		protected Message output_message = null;

		//下一个Acction
		protected BaseDCTAction next_action = null;

		//前一个Acction
		protected BaseDCTAction last_action = null;

		protected object object_state = null;

		#endregion

		#region Method
		public virtual Messages PreAction(object act)
		{
			this.action_status = ActionStatus.Working;
			// TODO:  添加 ActionUser.Action 实现
			FlowDirect = FlowDirect.WaitingInput;
			return null;
		}

		public virtual Messages Action(object act)
		{
			
			FlowDirect = FlowDirect.WaitingOutPut;
			// TODO:  添加 ActionUser.Action 实现
			return null;
		}

		public virtual Messages AftAction(object act)
		{
			this.action_status = ActionStatus.Pass;
			FlowDirect = FlowDirect.WaitingInput;
			// TODO:  添加 ActionUser.Action 实现
			return null;
		}

		public virtual Messages Do(object act)
		{
			Messages msgs = new Messages();

			if(this.action_status == ActionStatus.PrepareData)
			{
				msgs =  PreAction(act);
			}
			else if(this.action_status == ActionStatus.Working)
			{
				msgs =  Action(act);
			}
			else if(this.action_status == ActionStatus.Pass)
			{
				msgs =  AftAction(act);
			}
			return msgs;
		}
		#endregion

		#region Property

		public virtual object ObjectState
		{
			get
			{
				return object_state;
			}
			set
			{
				object_state = value;
			}
		}

		public virtual BaseDCTAction NextAction
		{
			get
			{
				return next_action;
			}
		}

		public virtual BaseDCTAction LastAction
		{
			get
			{
				return last_action;
			}
			set
			{
				last_action = value;
			}
		}

		public virtual UserControl.Message InputMessage
		{
			get
			{
				// TODO:  添加 ActionUser.InputMessage getter 实现
				return input_message;
			}
			set
			{
				input_message = value;
			}
		}

		/// <summary>
		/// 当前执行次序
		/// </summary>
		public virtual int CurrentSequence
		{
			get
			{
				return current_sequence;
			}
			set
			{
				current_sequence = value;
			}
		}
		/// <summary>
		/// Output Message
		/// </summary>
		public virtual UserControl.Message OutMesssage
		{
			get
			{
				return output_message;
			}
			set
			{
				output_message = value;
			}
		}

		public virtual BenQGuru.eMES.Common.DCT.Core.FlowDirect FlowDirect
		{
			get
			{
				return flow_direct;
			}
			set
			{
				flow_direct = value;
			}
		}

		public virtual int RepetTimes
		{
			get
			{
				return repet_times;
			}
			set
			{
				repet_times = value;
			}
		}

		public virtual BenQGuru.eMES.Common.DCT.Core.ActionStatus Status
		{
			get
			{
				return action_status;
			}
			set
			{
				action_status = value;
			}
		}

		

		#endregion
	}
}
