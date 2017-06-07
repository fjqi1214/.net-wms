using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenQGuru.eMES.SAPRFCService.Domain
{
    public class SAPRfcReturn
    {
        /// <summary>
        /// 执行结果标识(S表示成功，E表示失败)
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 执行结果信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// SAP物料凭证编码(PO)
        /// </summary>
        public string MaterialDocument { get; set; }
    }
}
