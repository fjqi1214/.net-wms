using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenQGuru.eMES.SAPRFCService.Domain
{
    public class PO
    {
        public string PONO { get; set; }
        public int POLine { get; set; }
        public string FacCode { get; set; }
        public string SerialNO { get; set; }
        public string MCode { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public string Status { get; set; }
        public string SAPMaterialInvoice { get; set; }
        public string Operator { get; set; }
        public string VendorInvoice { get; set; }
        public string StorageCode { get; set; }
        public string Remark { get; set; }
        public int InvoiceDate { get; set; }
    }
}
