using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.InterfaceDomain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.InterfaceFacade;

namespace SyncSAPJob
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

        #region add by sam
        public static bool TryGetStampDbDateTime(ref string strdate, ref string strtime, string parameterCode)
        {
            InvoicesFacade _InvoicesFacade = new InvoicesFacade();
            string stamp = _InvoicesFacade.GetParameterAlias("SAPJOBTIMESTAMP", parameterCode);
            if (string.IsNullOrEmpty(stamp))
            {
                return false;
            }
            DateTime dbDateTime = DateTime.Now;
            DateTime stampDateTime = dbDateTime.AddMinutes(-int.Parse(stamp));
            strdate = stampDateTime.Date.ToString("yyyyMMdd");
            strtime = stampDateTime.ToString("HHmmss");
            return true;
        }
        #endregion

        public static List<I_Sappo> Array2SappoList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<I_Sappo> list = new List<I_Sappo>();
            foreach (var a in arr)
            {
                I_Sappo inv = a as I_Sappo;
                list.Add(inv);
            }
            return list;
        }

        #region add by sam 2016/4/8
        public static List<I_Sapdnbatch> Array2SapdnList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<I_Sapdnbatch> list = new List<I_Sapdnbatch>();
            foreach (var a in arr)
            {
                I_Sapdnbatch inv = a as I_Sapdnbatch;
                list.Add(inv);
            }
            return list;
        }
        #endregion

        public static List<I_Sapub> Array2SapubList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<I_Sapub> list = new List<I_Sapub>();
            foreach (var a in arr)
            {
                I_Sapub inv = a as I_Sapub;
                list.Add(inv);
            }
            return list;
        }

        public static List<I_Saprs> Array2SaprsList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<I_Saprs> list = new List<I_Saprs>();
            foreach (var a in arr)
            {
                I_Saprs inv = a as I_Saprs;
                list.Add(inv);
            }
            return list;
        }
        public static List<I_Sapstoragecheck> Array2SapstoragecheckList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<I_Sapstoragecheck> list = new List<I_Sapstoragecheck>();
            foreach (var a in arr)
            {
                I_Sapstoragecheck inv = a as I_Sapstoragecheck;
                list.Add(inv);
            }
            return list;
        }
        public static List<I_Sapstorageinfo> Array2I_SapstorageinfoList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<I_Sapstorageinfo> list = new List<I_Sapstorageinfo>();
            foreach (var a in arr)
            {
                I_Sapstorageinfo inv = (I_Sapstorageinfo)a;
                list.Add(inv);
            }
            return list;
        }

        public static List<Sapstorageinfo> Array2SapstorageinfoList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<Sapstorageinfo> list = new List<Sapstorageinfo>();
            foreach (var a in arr)
            {
                Sapstorageinfo inv = (Sapstorageinfo)a;
                list.Add(inv);
            }
            return list;
        }
        public static List<StorageDetail> Array2StorageDetailfoList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<StorageDetail> list = new List<StorageDetail>();
            foreach (var a in arr)
            {
                StorageDetail inv = (StorageDetail)a;
                list.Add(inv);
            }
            return list;
        }
        public static List<Invoices> Array2InvoicesList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
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
            if (arr == null)
            {
                return null;
            }
            List<Invoicesdetail> list = new List<Invoicesdetail>();
            foreach (var a in arr)
            {
                Invoicesdetail detail = (Invoicesdetail)a;
                list.Add(detail);
            }
            return list;
        }

        public static List<Pickdetail> Array2PickdetailList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<Pickdetail> list = new List<Pickdetail>();
            foreach (var a in arr)
            {
                Pickdetail detail = (Pickdetail)a;
                list.Add(detail);
            }
            return list;
        }

        public static List<Storloctransdetail> Array2StorloctransdetailList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<Storloctransdetail> list = new List<Storloctransdetail>();
            foreach (var a in arr)
            {
                Storloctransdetail detail = (Storloctransdetail)a;
                list.Add(detail);
            }
            return list;
        }

        public static List<Material> Array2MaterialList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<Material> list = new List<Material>();
            foreach (var a in arr)
            {
                Material detail = (Material)a;
                list.Add(detail);
            }
            return list;
        }

        public static List<Dn_in2Sap> Array2Dn_in2SapList(object[] arr)
        {
            if (arr == null)
            {
                return null;
            }
            List<Dn_in2Sap> list = new List<Dn_in2Sap>();
            foreach (var a in arr)
            {
                Dn_in2Sap detail = (Dn_in2Sap)a;
                list.Add(detail);
            }
            return list;
        }
    }
}
