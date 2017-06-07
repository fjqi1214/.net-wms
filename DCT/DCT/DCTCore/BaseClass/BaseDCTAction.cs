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

        private ActionStatus _Status = ActionStatus.PrepareData;
        private FlowDirect _FlowDirect;
        private UserControl.Message _InitMesssage = null;
        private UserControl.Message _InputMessage = null;
        private UserControl.Message _OutMesssage = null;
        private UserControl.Message _LastPrompMesssage = null;

        private bool _IsTopAction = true;
        private int _CurrentSequence;
        private BaseDCTAction _NextAction = null;
        private BaseDCTAction _LastAction = null;

        private object _ObjectState = null;
        private int _RepetTimes;
        private bool _NeedCancel = false;
        private bool _NeedAuthorized = true;

        #endregion

        #region Property

        /// <summary>
        /// 状态
        /// </summary>
        public virtual ActionStatus Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        /// <summary>
        /// 数据传递方向
        /// </summary>
        public virtual FlowDirect FlowDirect
        {
            get
            {
                return _FlowDirect;
            }
            set
            {
                _FlowDirect = value;
            }
        }

        /// <summary>
        /// 初始化信息
        /// </summary>
        public virtual UserControl.Message InitMessage
        {
            get
            {
                return _InitMesssage;
            }
            set
            {
                _InitMesssage = value;
            }
        }

        /// <summary>
        /// 输入信息
        /// </summary>
        public virtual UserControl.Message InputMessage
        {
            get
            {
                return _InputMessage;
            }
            set
            {
                _InputMessage = value;
            }
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        public virtual UserControl.Message OutMesssage
        {
            get
            {
                return _OutMesssage;
            }
            set
            {
                _OutMesssage = value;
            }
        }

        /// <summary>
        /// 最后一次的提示输出信息
        /// </summary>
        public virtual UserControl.Message LastPrompMesssage
        {
            get
            {
                return _LastPrompMesssage;
            }
            set
            {
                _LastPrompMesssage = value;
            }
        }

        /// <summary>
        /// 是否为顶级Action
        /// </summary>
        public virtual bool IsTopAction
        {
            get
            {
                return _IsTopAction;
            }
            set
            {
                _IsTopAction = value;
            }
        }

        /// <summary>
        /// 当前执行次序
        /// </summary>
        public virtual int CurrentSequence
        {
            get
            {
                return _CurrentSequence;
            }
            set
            {
                _CurrentSequence = value;
            }
        }

        /// <summary>
        /// 下一个Action
        /// </summary>
        public virtual BaseDCTAction NextAction
        {
            get
            {
                return _NextAction;
            }
            set
            {
                _NextAction = value;
            }
        }

        /// <summary>
        /// 前一Action（目前未使用）
        /// </summary>
        public virtual BaseDCTAction LastAction
        {
            get
            {
                return _LastAction;
            }
            set
            {
                _LastAction = value;
            }
        }

        /// <summary>
        /// 缓存的对象
        /// </summary>
        public virtual object ObjectState
        {
            get
            {
                return _ObjectState;
            }
            set
            {
                _ObjectState = value;
            }
        }

        /// <summary>
        /// 重复次数
        /// </summary>
        public virtual int RepetTimes
        {
            get
            {
                return _RepetTimes;
            }
            set
            {
                _RepetTimes = value;
            }
        }

        /// <summary>
        /// 是否需要Cancel本次输入
        /// </summary>
        public virtual bool NeedCancel
        {
            get
            {
                return _NeedCancel;
            }
            set
            {
                _NeedCancel = value;
            }
        }

        /// <summary>
        /// 是否只能在登录后使用
        /// </summary>
        public virtual bool NeedAuthorized
        {
            get
            {
                return _NeedAuthorized;
            }
            set
            {
                _NeedAuthorized = value;
            }
        }

        #endregion

        #region Method

        public virtual Messages InitAction(object act)
        {
            this._Status = ActionStatus.PrepareData;
            FlowDirect = FlowDirect.WaitingInput;

            Messages msgs = new Messages();
            if (this.InitMessage != null)
                msgs.Add(this.InitMessage);
            if (this.OutMesssage != null)
                msgs.Add(this.OutMesssage);
            return msgs;
        }

        public virtual Messages PreAction(object act)
        {
            this._Status = ActionStatus.Working;
            FlowDirect = FlowDirect.WaitingInput;

            return null;
        }

        public virtual Messages Action(object act)
        {
            this._Status = ActionStatus.Pass;
            FlowDirect = FlowDirect.WaitingOutput;

            return null;
        }

        public virtual Messages AftAction(object act)
        {
            this._Status = ActionStatus.PrepareData;
            FlowDirect = FlowDirect.WaitingInput;

            return null;
        }

        public virtual Messages Do(object act)
        {
            Messages msgs = new Messages();

            if (this._Status == ActionStatus.Init)
            {
                msgs = InitAction(act);
            }
            else if (this._Status == ActionStatus.PrepareData)
            {
                msgs = PreAction(act);
            }
            else if (this._Status == ActionStatus.Working)
            {
                msgs = Action(act);
            }
            else if (this._Status == ActionStatus.Pass)
            {
                msgs = AftAction(act);
            }

            return msgs;
        }

        protected void ProcessBeforeReturn(ActionStatus newStatus, Messages msgs)
        {
            //处理Status和FlowDirect
            this._Status = newStatus;
            switch (newStatus)
            {
                case ActionStatus.Init:
                    this.FlowDirect = FlowDirect.WaitingOutput;
                    break;

                case ActionStatus.PrepareData:
                    this.FlowDirect = FlowDirect.WaitingInput;
                    break;

                case ActionStatus.Working:
                    this.FlowDirect = FlowDirect.WaitingInput;
                    break;

                case ActionStatus.Pass:
                    this.FlowDirect = FlowDirect.WaitingOutput;
                    break;

                default:
                    this.FlowDirect = FlowDirect.WaitingOutput;
                    break;
            }

            //处理返回的Messages
            if (msgs != null && msgs.Count() > 0)
            {
                if (msgs.Objects(msgs.Count() - 1).Type == MessageType.Normal)
                    this.LastPrompMesssage = msgs.Objects(msgs.Count() - 1);

                if (msgs.Objects(msgs.Count() - 1).Type == MessageType.Error && this.LastPrompMesssage != null)
                    msgs.Add(this.LastPrompMesssage);
            }
        }

        #endregion
    }
}
