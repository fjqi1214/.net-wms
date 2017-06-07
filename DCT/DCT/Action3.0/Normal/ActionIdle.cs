using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DCT.Core;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

using UserControl;

namespace BenQGuru.eMES.Common.DCT.Action
{
    /// <summary>
    /// 用于未处于任何命令中的情况
    /// </summary>
    public class ActionIdle : BaseDCTAction
    {
        public ActionIdle()
        {
            this.NeedAuthorized = false;

            this.Status = ActionStatus.Init;
            this.FlowDirect = FlowDirect.WaitingOutput;

            this.InitMessage = null;
            this.OutMesssage = new Message(MessageType.Normal, "$DCT_PLEASE_ACTION");
        }

        public override Messages PreAction(object act)
        {
            Messages msgs = new Messages();

            if (act == null)
            {
                return msgs;
            }

            string data = act.ToString().Trim().ToUpper();

            BaseDCTAction action = (new ActionHelper()).GetActionByCommand(data);

            if (action == null)
            {
                this.Status = ActionStatus.Init;
                this.FlowDirect = FlowDirect.WaitingOutput;
            }
            else
            {
                this.Status = ActionStatus.Pass;
                this.NextAction = action;                
            }            

            return msgs;
        }

        public override Messages Action(object act)
        {
            Messages msgs = new Messages();
            return msgs;
        }

        public override Messages AftAction(object act)
        {
            Messages msgs = new Messages();
            return msgs;
        }
    }
}
