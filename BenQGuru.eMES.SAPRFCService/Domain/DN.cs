using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BenQGuru.eMES.SAPRFCService.Domain
{
    public class DN
    {
        public string DNNO { get; set; }
        public int DNLine { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
    }
}
