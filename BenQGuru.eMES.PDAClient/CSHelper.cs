using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using BenQGuru.eMES.PDAClient.Service;
using BenQGuru.eMES.DataCollect;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using UserControl;

namespace BenQGuru.eMES.PDAClient
{
    public class CSHelper
    {
        public static void ucMessageWorkingErrorAdded(WorkingErrorAddedEventArgs eventArgs, IDomainDataProvider dataProvider)
        {
            DataCollectFacade dataCollectFacade = new DataCollectFacade(dataProvider);

            string userCode = ApplicationService.Current().UserCode;
            string resCode = ApplicationService.Current().LoginInfo.Resource.ResourceCode;
            string segCode = ApplicationService.Current().LoginInfo.Resource.SegmentCode;
            string ssCode = ApplicationService.Current().LoginInfo.Resource.StepSequenceCode;
            string shiftTypeCode = ApplicationService.Current().LoginInfo.Resource.ShiftTypeCode;

            string errorMessageCode = string.Empty;
            if (eventArgs.ErrorMessage.Type == MessageType.Error)
            {
                errorMessageCode = eventArgs.ErrorMessage.Body;
            }
            if (eventArgs.ErrorMessage.Exception != null)
            {
                errorMessageCode = eventArgs.ErrorMessage.Exception.Message;
            }

            dataCollectFacade.LogWorkingError(userCode, resCode, segCode, ssCode, shiftTypeCode,
                WorkingErrorFunctionType.CS, eventArgs.Function, eventArgs.InputContent, errorMessageCode, eventArgs.ErrorText);
        }
    }
}
