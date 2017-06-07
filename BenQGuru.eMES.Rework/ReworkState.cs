#region system
using System;
using System.Runtime.Remoting;
#endregion


#region project
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Web.Helper;
#endregion


namespace BenQGuru.eMES.Rework
{
	/// <summary>
	/// StatusManager 的摘要说明。
	/// 文件名:		ReworkStatus.cs
	/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
	/// 创建人:		crystal chu
	/// 创建日期:	2005-03-31 
	/// 修改人:
	/// 修改日期:
	/// 描 述:	
	/// 版 本:	
	/// </summary>
	public class Status
	{
		protected string _status;
		protected ReworkSheet _reworkSheet = null;
		protected ReworkFacade _reworkFacade = null;
		public Status(ReworkFacade reworkFacade,ReworkSheet reworkSheet)
		{
			this._reworkFacade = reworkFacade;
			this._reworkSheet = reworkSheet;
		}

		public virtual void Waiting()
		{
		}

		public virtual void Approve()
		{
		}

		public virtual void NOApprove()
		{
		}
		public virtual void Close()
		{
		}
		public virtual void Open()
		{
		}
        public virtual void Release()
        {
        }
	}

	public class NewStatus: Status
	{
		public NewStatus(ReworkFacade reworkFacade,ReworkSheet reworkSheet):base(reworkFacade,reworkSheet)
		{
			this._status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW;
		}

		public override void Waiting()
		{
			if(this._reworkSheet.Status != this._status)
			{
                ExceptionManager.Raise(this.GetType(), "$Error_ApproveStatus",string.Format("[$StatusShouldBe {0}]", "$"+this._status), null);
			}
			this._reworkSheet.Status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_WAITING;
			this._reworkFacade.UpdateReworkSheetWithoutTransaction(_reworkSheet);
		}

	}

	public class WaitingStatus: Status
	{
		public WaitingStatus(ReworkFacade reworkFacade,ReworkSheet reworkSheet):base(reworkFacade,reworkSheet)
		{
			this._status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_WAITING;
		}

		public override void Approve()
		{
			if(_reworkSheet.Status != this._status)
			{
                ExceptionManager.Raise(this.GetType(), "$Error_ApproveStatus",string.Format("[$StatusShouldBe {0}]", "$"+this._status), null);
            }
			this._reworkSheet.Status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_RELEASE;
			this._reworkFacade.UpdateReworkSheetWithoutTransaction(_reworkSheet);
		}
		public override void NOApprove()
		{
			if(_reworkSheet.Status != this._status)
			{
                ExceptionManager.Raise(this.GetType(), "$Error_ApproveStatus",string.Format("[$StatusShouldBe {0}]", "$"+this._status), null);
            }
			this._reworkSheet.Status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_NEW;
			this._reworkFacade.UpdateReworkSheetWithoutTransaction(_reworkSheet);
		}

	}


    public class ReleaseStatus:Status
    {
        public ReleaseStatus(ReworkFacade reworkFacade,ReworkSheet reworkSheet):base(reworkFacade,reworkSheet)
        {
            this._status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_RELEASE;
        }
        public override void Open()
        {
            if (_reworkSheet.Status != this._status && _reworkSheet.NeedCheck == "Y")
            {
                ExceptionManager.Raise(this.GetType(), "$Error_ApproveStatus",string.Format("[$StatusShouldBe {0}]", "$"+this._status), null);
            }
            this._reworkSheet.Status = BenQGuru.eMES.Web.Helper.ReworkStatus.REWORKSTATUS_OPEN;
            this._reworkFacade.UpdateReworkSheetWithoutTransaction(_reworkSheet);
        }

    }


	public class StatusManager
	{
		private Status _currentStatus;
		private ReworkSheet _reworkSheet = null;
		private ReworkFacade _reworkFacade = null;

		public StatusManager(ReworkFacade reworkFacade,ReworkSheet reworkSheet)
		{
			this._reworkFacade = reworkFacade;
			this._reworkSheet = reworkSheet;
			
		}

		public  void Waiting()
		{
			_currentStatus = new NewStatus(_reworkFacade,_reworkSheet);
			_currentStatus.Waiting();
		}

		public void Approve()
		{
			_currentStatus = new WaitingStatus(_reworkFacade,_reworkSheet);
			_currentStatus.Approve();
		}

		public void NOApprove()
		{
			_currentStatus = new WaitingStatus(_reworkFacade,_reworkSheet);
			_currentStatus.NOApprove();
		}

        public void Open()
		{
			_currentStatus = new ReleaseStatus(_reworkFacade,_reworkSheet);
			_currentStatus.Open();
		}
	}
}
