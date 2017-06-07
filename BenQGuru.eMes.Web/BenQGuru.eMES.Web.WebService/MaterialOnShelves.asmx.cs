using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Common.Domain;
using System.Collections.Generic;
using BenQGuru.eMES.SAPRFCService;
using BenQGuru.eMES.SAPRFCService.Domain;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// MaterialOnShelves 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class MaterialOnShelves : System.Web.Services.WebService
    {
        public MaterialOnShelves()
        {
            InitDbItems();
        }
        private string m_DbName;
        private IDomainDataProvider _domainDataProvider;
        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(m_DbName);
                }
                return _domainDataProvider;
            }
        }
        private void InitDbItems()
        {
            foreach (var item in BenQGuru.eMES.Common.Config.ConfigSection.Current.DomainSetting.Settings)
            {
                if (item.Default)
                    m_DbName = item.Name;

            }
        }

        [WebMethod(EnableSession = true)]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(EnableSession = true)]
        public string GetActOnShelvesQTY(string CartonNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            return facade.QueryActOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo))).ToString();
        }
        [WebMethod(EnableSession = true)]
        public string GetPlanOnShelvesQTY(string CartonNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            return facade.QueryPlanOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo))).ToString();
        }
        [WebMethod(EnableSession = true)]
        public string GetCUSMcode(string CartonNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            string result = facade.GetCUSCodebyCartonNo(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo)));
            return result;
        }
        [WebMethod(EnableSession = true)]
        public DataTable GetDataGrid(string CartonNo, string LocationNo)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }
            object[] objs = facade.QueryOnshelvesDetail(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(CartonNo.ToUpper())), FormatHelper.PKCapitalFormat(FormatHelper.CleanString(LocationNo.ToUpper())));

            DataTable dt = new DataTable("ExampleDataTable");
            if (objs != null)
            {
              
                dt.Columns.Add("Check", typeof(string));
                dt.Columns.Add("STNO", typeof(string));
                dt.Columns.Add("cartonno", typeof(string));
                dt.Columns.Add("relocaNo", typeof(string));
                dt.Columns.Add("locaNo", typeof(string));
                dt.Columns.Add("DQMCode", typeof(string));
                dt.Columns.Add("stline", typeof(string));
                for (int i = 0; i < objs.Length; i++)
                {
                    Asndetailexp Asndetail = objs[i] as Asndetailexp;
                    dt.Rows.Add("", Asndetail.Stno, Asndetail.Cartonno, Asndetail.ReLocationCode, Asndetail.LocationCode, Asndetail.DqmCode, Asndetail.Stline.ToString());
                }

            }
            return dt;
        }
        [WebMethod(EnableSession = true)]
        public DataTable GetDataTable()
        {
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            dt.Columns.Add("选中", typeof(string));
            dt.Columns.Add("入库指令号", typeof(string));
            dt.Columns.Add("箱号", typeof(string));
            dt.Columns.Add("推荐货位", typeof(string));
            dt.Columns.Add("货位号", typeof(string));
            dt.Columns.Add("鼎桥物料编码", typeof(string));
            dt.Columns.Add("行号", typeof(string));
            dt.Rows.Add("", "1", "Tom", "1", "Tom", "Tom", "Tom");
            dt.Rows.Add("", "2", "Tom", "Tom", "2", "Tom", "Tom");
            dt.Rows.Add("", "3", "Jim", "Tom", "Tom", "3", "Tom");
            dt.Rows.Add("", "4", "Tom", "Tom", "Tom", "Tom", "4");
            dt.Rows.Add("", "5", "Tom", "5", "Tom", "Tom", "Tom");
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public DataTable Retrieve(string cartonNO)
        {
            InventoryFacade _Invenfacade = new InventoryFacade(DataProvider);
            List<AsnHead> obj = _Invenfacade.QueryASNDetailSNCatron(cartonNO);
            DataTable dt = new DataTable("ExampleDataTable");

            dt.Columns.Add("STNO", typeof(string));
            dt.Columns.Add("STLINE", typeof(string));



            foreach (AsnHead h in obj)
            {
                dt.Rows.Add(h.STNO, h.STlINE);
            }

            return dt;
        }

        [WebMethod(EnableSession = true)]
        public string OnShelves(DataTable dt, string cartonNO, string locationNo, string UserCode)
        {
            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            string message = string.Empty;

        
          
            if (dt.Rows.Count <= 0)
                return "无上架的数据！";

            List<Asndetail> asnDetailList = new List<Asndetail>();//add by sam



            WarehouseFacade facade = new WarehouseFacade(DataProvider);
            foreach (DataRow row in dt.Rows)
            {

                object obj = facade.GetAsndetail(int.Parse(row["STLINE"].ToString()), row["STNO"].ToString());
                if (obj == null)
                    throw new Exception("行中获取的ASN明细为空！");
               
                Asndetail asndetail = obj as Asndetail;
                asndetail.LocationCode1 = locationNo;
                asnDetailList.Add(asndetail);

            }

            bool result = kit.OnShelf(cartonNO.ToUpper(), locationNo.ToUpper(), asnDetailList, DataProvider, out message, UserCode);
            if (result)
                message = "上架成功！";
            return message;


        }



       

    }
}
              

