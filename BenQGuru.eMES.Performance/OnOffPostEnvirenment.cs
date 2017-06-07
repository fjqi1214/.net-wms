using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Performance
{
    public class OnOffPostEnvirenment
    {
        private IDomainDataProvider _DataProvider = null;

        private string _ResCode = string.Empty;
        private Operation _Operation = null;
        private StepSequence _StepSequence = null;
        private DBDateTime _DBDateTime = null;
        private int _ShiftDate = 0;
        private Shift _Shift = null;

        public bool Init(IDomainDataProvider dataProvider, string resCode, DBDateTime dbDateTime)
        {
            bool returnValue = false;

            BaseModelFacade baseModelFacade = new BaseModelFacade(dataProvider);
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(dataProvider);

            _DataProvider = dataProvider;
            _ResCode = resCode;
            _DBDateTime = dbDateTime;

            //获取对应的OPCode            
            Operation2Resource op2Res = (Operation2Resource)baseModelFacade.GetOperationByResource(_ResCode);
            if (op2Res != null)
            {
                _Operation = (Operation)baseModelFacade.GetOperation(op2Res.OPCode);
            }
            if (_Operation == null)
            {
                return returnValue;
            }

            //获取对应的SS
            Resource res = (Resource)baseModelFacade.GetResource(_ResCode);
            if (res != null)
            {
                _StepSequence = (StepSequence)baseModelFacade.GetStepSequence(res.StepSequenceCode);
            }
            if (_StepSequence == null)
            {
                return returnValue;
            }

            //获取当前的ShiftDate
            _ShiftDate = shiftModelFacade.GetShiftDayBySS(_StepSequence, _DBDateTime.DateTime);

            //获取当前的Shift
            _Shift = (Shift)shiftModelFacade.GetShift(_StepSequence.ShiftTypeCode, _DBDateTime.DBTime);
            if (_Shift == null)
            {
                return returnValue;
            }

            returnValue = true;

            return returnValue;
        }

        public bool InitWithoutResAndOP(IDomainDataProvider dataProvider, string ssCode, DBDateTime dbDateTime)
        {
            bool returnValue = false;

            BaseModelFacade baseModelFacade = new BaseModelFacade(dataProvider);
            ShiftModelFacade shiftModelFacade = new ShiftModelFacade(dataProvider);

            _DataProvider = dataProvider;
            _DBDateTime = dbDateTime;

            //获取对应的SS            
            _StepSequence = (StepSequence)baseModelFacade.GetStepSequence(ssCode);            
            if (_StepSequence == null)
            {
                return returnValue;
            }

            //获取当前的ShiftDate
            _ShiftDate = shiftModelFacade.GetShiftDayBySS(_StepSequence, _DBDateTime.DateTime);

            //获取当前的Shift
            _Shift = (Shift)shiftModelFacade.GetShift(_StepSequence.ShiftTypeCode, _DBDateTime.DBTime);
            if (_Shift == null)
            {
                return returnValue;
            }

            returnValue = true;

            return returnValue;
        }

        public string ResCode
        {
            get { return _ResCode; }
        }

        public Operation Operation
        {
            get { return _Operation; }
        }

        public StepSequence StepSequence
        {
            get { return _StepSequence; }
        }

        public DBDateTime DBDateTime
        {
            get { return _DBDateTime; }
        }

        public int ShiftDate
        {
            get { return _ShiftDate; }
        }

        public Shift Shift
        {
            get { return _Shift; }
        }

        public int GetShiftBeginDate()
        {
            if (_Shift.IsOverDate == FormatHelper.TRUE_STRING && _DBDateTime.DBTime <= _Shift.ShiftEndTime)
            {
                return FormatHelper.TODateInt(_DBDateTime.DateTime.AddDays(-1));
            }
            else
            {
                return _DBDateTime.DBDate;
            }
        }

        public int GetShiftEndDate()
        {
            if (_Shift.IsOverDate == FormatHelper.TRUE_STRING && _DBDateTime.DBTime >= _Shift.ShiftBeginTime)
            {
                return FormatHelper.TODateInt(_DBDateTime.DateTime.AddDays(1));
            }
            else
            {
                return _DBDateTime.DBDate;
            }
        }

    }
}
