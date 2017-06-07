using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.WatchPanelDisToLine
{
    //public class DisToLineHead: DomainObject
    //{
    //    [FieldMapAttribute("MOCODE", typeof(string), 40, true)]
    //    public string MOCode;

    //    [FieldMapAttribute("MCODE", typeof(string), 40, true)]
    //    public string MCode;

    //    [FieldMapAttribute("MNAME", typeof(string), 200, true)]
    //    public string MName;

    //    [FieldMapAttribute("MPLANQTY", typeof(decimal), 40, true)]
    //    public decimal MPlanQty;

    //    [FieldMapAttribute("MDISQTY", typeof(decimal), 40, true)]
    //    public decimal MDisQty;

    //    [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
    //    public string ItemCode;

    //    [FieldMapAttribute("MOPLANQTY", typeof(decimal), 40, true)]
    //    public decimal MOPLANQTY;

    //    [FieldMapAttribute("STATUS", typeof(string), 40, true)]
    //    public string Status;

    //    [FieldMapAttribute("PENDINGTIME", typeof(decimal), 40, true)]
    //    public decimal PendingTime;

    //    [FieldMapAttribute("ORGID", typeof(int), 40, true)]
    //    public string Orgid;

    //    [FieldMapAttribute("MOBOM", typeof(string), 40, true)]
    //    public string MOBom;
        
    //    [FieldMapAttribute("MUSER", typeof(string), 40, true)]
    //    public string MUser;

    //    [FieldMapAttribute("MDATE", typeof(int), 8, true)]
    //    public int MDate;

    //    [FieldMapAttribute("MTIME", typeof(int), 6, true)]
    //    public int MTime;

    //    [FieldMapAttribute("EATTRIBUTE1", typeof(string), 200, true)]
    //    public string EAttribute1;
    //}

    public class DisToLineQuery : DomainObject
    {
        [FieldMapAttribute("segcode", typeof(string), 40, true)]
        public string SegCode;

        [FieldMapAttribute("sscode", typeof(string), 40, true)]
        public string SSCode;

        [FieldMapAttribute("mocode", typeof(string), 40, true)]
        public string MOCode;

        [FieldMapAttribute("mcode", typeof(string), 40, true)]
        public string MCode;

        [FieldMapAttribute("mname", typeof(string), 200, true)]
        public string MName;

        [FieldMapAttribute("moplanqty", typeof(decimal), 40, true)]
        public decimal MOPlanQty;

        [FieldMapAttribute("mssdisqty", typeof(decimal), 40, true)]
        public decimal MSSDisQty;

        [FieldMapAttribute("mssleftqty", typeof(decimal), 40, true)]
        public decimal MSSLeftQty;

        [FieldMapAttribute("cycletime", typeof(decimal), 40, true)]
        public decimal CycleTime;

        [FieldMapAttribute("mobitemqty", typeof(decimal), 40, true)]
        public decimal MOBItemQty;

        [FieldMapAttribute("lefttime", typeof(decimal), 40, true)]
        public decimal lefttime;
        
        [FieldMapAttribute("status", typeof(string), 40, true)]
        public string status;

    }
}
