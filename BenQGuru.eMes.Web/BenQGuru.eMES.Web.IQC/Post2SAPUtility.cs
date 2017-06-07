using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.SAPDataTransfer;
using BenQGuru.eMES.SAPDataTransferInterface;
using System.Web.UI.MobileControls;
using System.Collections.Generic;

namespace BenQGuru.eMES.Web.IQC
{
    public static class Post2SAPUtility
    {
        public static DT_MES_SOURCESTOCK_REQLIST GenerateSAPPOInfo(MaterialReceive mr)
        {
            DT_MES_SOURCESTOCK_REQLIST po = new DT_MES_SOURCESTOCK_REQLIST();
            po.PSTNG_DATE = mr.AccountDate.ToString();
            po.DOC_DATE = mr.VoucherDate.ToString();
            po.PO_NUMBER = mr.OrderNo.ToString();
            po.PO_ITEM = mr.OrderLine.ToString();
            po.MATERIAL = mr.ItemCode;
            po.PLANT = mr.OrganizationID.ToString();
            po.STGE_LOC = mr.StorageID;
            po.ENTRY_QNT = mr.RealReceiveQty.ToString();
            po.ENTRY_UOM = mr.Unit;
            po.HEADER_TXT = mr.ReceiveMemo;
            po.MOVE_TYPE = "101";
            return po;
        }

        public static ServiceResult CallSAPInterface(MaterialPOTransferArgument mpArg)
        {
            MaterialPOTransfer materialPOTransfer = new MaterialPOTransfer();
            materialPOTransfer.SetArguments(mpArg);
            ServiceResult sr = materialPOTransfer.Run(RunMethod.Manually);

            return sr;
        }
    }
}
