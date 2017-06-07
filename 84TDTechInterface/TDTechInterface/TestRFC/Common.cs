using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.InterfaceDomain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;

namespace TestRFC
{
    public class Common
    {
        public static int ParseInt(object obj)
        {
            int reInt = -1;
            if (obj != null)
                int.TryParse(obj.ToString(), out reInt);
            return reInt;
        }

        public static void GetDBDateTime(out int intDate, out int intTime, IDomainDataProvider dataProvider)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(dataProvider);
            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);
            intDate = FormatHelper.TODateInt(dtNow);
            intTime = FormatHelper.TOTimeInt(dtNow);
        }

        public static List<Invoices> Array2InvoicesList(object[] arr)
        {
            List<Invoices> list = new List<Invoices>();
            foreach (var a in arr)
            {
                Invoices inv = (Invoices)a;
                list.Add(inv);                
            }
            return list;
        }

        public static List<Invoicesdetail> Array2InvoicesDetailList(object[] arr)
        {
            List<Invoicesdetail> list = new List<Invoicesdetail>();
            foreach (var a in arr)
            {
                Invoicesdetail detail = (Invoicesdetail)a;
                list.Add(detail);
            }
            return list;
        }

        public static List<Material> Array2MaterialList(object[] arr)
        {
            List<Material> list = new List<Material>();
            foreach (var a in arr)
            {
                Material detail = (Material)a;
                list.Add(detail);
            }
            return list;
        }
    }
}
