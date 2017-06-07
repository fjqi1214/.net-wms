using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// StockCheck 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class StockCheck : System.Web.Services.WebService
    {
        public StockCheck()
        {
            InitDbItems();
        }

        private IDomainDataProvider _domainDataProvider;
        private string m_DbName;
        private BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = null;
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
        public BenQGuru.eMES.Domain.Warehouse.StockCheckDetail GetPortionStockCheckFromCheckNo(string checkNo)
        {
            BenQGuru.eMES.Material.WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
            }
            return facade.GetPortionStockChecksFormCheckNo(checkNo);
        }



        [WebMethod(EnableSession = true)]
        public string[] GetPortionStockChecks()
        {
            BenQGuru.eMES.Material.WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
            }
            return facade.GetPortionStockChecks().ToArray();
        }



        [WebMethod(EnableSession = true)]
        public string[] GetStorageDetailsFromCARTONNO(string CARTONNO)
        {
            BenQGuru.eMES.Material.WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
            }
            BenQGuru.eMES.Domain.Warehouse.StorageDetail[] dd = facade.GetStorageDetailsFromCARTONNO(CARTONNO);
            if (dd.Length > 0)
                return new string[] { dd[0].DQMCode ?? " ", dd[0].StorageCode ?? " " };
            else
                return new string[] { " ", " " }; ;
        }

        [WebMethod(EnableSession = true)]
        public void AddStockCheckDetailCarton(string checkNo, string storageCode, string locationCode, string CARTONNO, string DQMCode, int checkQty, string userCode, string SLocationCode, string SCARTONNO)
        {
            BenQGuru.eMES.Material.WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
            }
            facade.AddStockCheckDetailCarton(checkNo, storageCode, locationCode, CARTONNO, DQMCode, checkQty, userCode, SLocationCode, SCARTONNO);
        }

        [WebMethod(EnableSession = true)]
        public string[] GetWaitStockCheckNo(string user)
        {
            BenQGuru.eMES.Material.WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
            }
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(user);
            return facade.GetWaitStockCheckNo(usergroupList);
        }


        [WebMethod(EnableSession = true)]
        public DataTable GetPortionStockCheckOps(string checkNo)
        {
            BenQGuru.eMES.Material.WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);
            }
            object[] objs = facade.GetPortionStockCheckOps(checkNo);

            DataTable dt = new DataTable("ExampleDataTable");

            dt.Columns.Add("SLocationCode", typeof(string));
            dt.Columns.Add("SCartonno", typeof(string));
            dt.Columns.Add("LocationCode", typeof(string));
            dt.Columns.Add("Cartonno", typeof(string));
            dt.Columns.Add("StorageQTY", typeof(string));
            dt.Columns.Add("CheckQTY", typeof(string));
            dt.Columns.Add("StorageCode", typeof(string));
            dt.Columns.Add("DQMCode", typeof(string));
            if (objs != null)
            {
                for (int i = 0; i < objs.Length; i++)
                {
                    StockCheckDetailOp s = objs[i] as StockCheckDetailOp;
                    dt.Rows.Add(s.SLocationCode, s.SCARTONNO, s.LocationCode, s.CARTONNO, s.STORAGEQTY, s.Qty, s.StorageCode, s.DQMCODE);
                }

            }
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public bool SubmitPortionCheck(string checkNo,
            string cartonno,
            string checkQtyStr,
            string locationCode,
            string diffDesc,
            string dqmCode,
            string usr, out string message)
        {
            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            message = string.Empty;
            bool result = kit.SubmitPortionCheck(checkNo,
                cartonno,
                checkQtyStr,
                locationCode,
                diffDesc,
                dqmCode,
                DataProvider,
                usr, out message);
            return result;
        }

    }
}
