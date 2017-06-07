using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Security;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class Login : System.Web.Services.WebService
    {
        private Package.PackageFacade _pf;
        WarehouseFacade _WarehouseFacade = null;
        public Login()
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


        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
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
        public string UserLogin(string userCode, string passWord, string resCode)
        {
            //_domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider(m_DbName);
            SecurityFacade _facade = null;
            BaseModelFacade _basemodelFacade = null;
            try
            {
                _facade = new SecurityFacade(DataProvider);
                _basemodelFacade = new BaseModelFacade(DataProvider);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return ex.Message;
            }

            //缓解性能问题
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = false;
            ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
            try
            {

                object[] objUserGroup = null;
                //LoginCheck内部函数会对用户名，密码是否为空进行检查，否则需要检查是否为空
                User user = _facade.LoginCheck(userCode, passWord, out objUserGroup);

                // 用户名不存在
                if (user == null)
                {
                    return "用户不存在";
                }

                //object obj = _basemodelFacade.GetResource(resCode);
                //if (obj == null)
                //{
                //    return "资源不存在";
                //}

                // 检查Resource权限                
                bool bIsAdmin = false;
                if (objUserGroup != null)
                {
                    foreach (object o in objUserGroup)
                    {
                        if (((UserGroup)o).UserGroupType == "ADMIN")
                        {
                            bIsAdmin = true;
                            break;
                        }
                    }
                }

                if (!bIsAdmin)
                {
                    //if (_facade.CheckResourceRight(user.UserCode, resCode)==false)
                    //{
                    //    return "该资源没有权限";
                    //}
                }
                return Enums.WebService_Op_Success;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                //LoginCheck 内部会抛出异常，需要进行多语言转换
                return UserControl.MutiLanguages.ParserMessage(ex.Message);
            }
            finally
            {
                //缓解性能问题
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.OpenConnection();
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)DataProvider).PersistBroker.AutoCloseConnection = true;
            }
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetAllMenuWithUrlWithTypePermission(string userCode)
        {
            SystemSettingFacade sysFacade = new SystemSettingFacade(DataProvider);
            UserFacade _userFacade = new UserFacade(DataProvider);
            object[] groups = _userFacade.GetUserGroupofUser(userCode);
            if (groups == null || groups.Length == 0)
            {
                return null;
                //$Error_User_Not_Belong_To_Any_User_Group
            }
            bool bIsAdmin = false;
            for (int i = 0; i < groups.Length; i++)
            {
                if (((UserGroup)groups[i]).UserGroupType == "ADMIN")
                {
                    bIsAdmin = true;
                    userCode = string.Empty;
                    break;
                }
            }
            object[] objs = sysFacade.GetAllMenuWithUrlWithTypePermission(Web.Helper.MenuType.MenuType_PDA, userCode);
            DataTable dt = new DataTable();
            dt.TableName = "MENU";
            if (objs != null && objs.Length > 0)
            {
                dt.Columns.Add("MENUCODE");
                dt.Columns.Add("MENUDESC");
                dt.Columns.Add("MENUSEQ");
                dt.Columns.Add("MENUTYPE");
                dt.Columns.Add("PMENUCODE");
                dt.Columns.Add("FORMURL");

                foreach (MenuWithUrl menu in objs)
                {
                    if (menu.Visibility != "1")
                    {
                        dt.Rows.Add(menu.MenuCode, menu.MenuDescription, menu.MenuSequence, menu.MenuType, menu.ParentMenuCode, menu.FormUrl);
                    }
                }
            }
            return dt;
        }
        //#region 模具出库管理
        //[WebMethod]
        //public string MoldOutManger(string moldid,string user)
        //{
        //    if (_WarehouseFacade == null)
        //        _WarehouseFacade = new WarehouseFacade(DataProvider);
        //    if (string.IsNullOrEmpty(moldid))
        //    {
        //        return UserControl.MutiLanguages.ParserMessage("$PDA_Input_moldID_Please");

        //    }
        //    object ObjMoldInfo = _WarehouseFacade.GetMold(moldid);
        //    string status = string.Empty;
        //    if (ObjMoldInfo != null)
        //    {
        //        status = ((Mold)ObjMoldInfo).Status;
        //        if (status == Web.Helper.MoldStatus.Initial)
        //        {
        //            string message = UserControl.MutiLanguages.ParserMessage("$PDA_MoldID_Not_Print");
        //            return string.Format(message, moldid);
        //        }
        //        if (status == Web.Helper.MoldStatus.Print)
        //        {
        //            string message = UserControl.MutiLanguages.ParserMessage("$PDA_MoldID_Not_Instore");
        //            return string.Format(message, moldid);

        //        }
        //        if (status == Web.Helper.MoldStatus.OutStore)
        //        {
        //            string message = UserControl.MutiLanguages.ParserMessage("$PDA_MoldID_Is_Outstore");
        //            return string.Format(message, moldid);
        //        }
        //        //else
        //        //{
        //        //    return UserControl.MutiLanguages.ParserMessage("$PDA_MoldIDstatus_Is_Error");
        //        //}

        //    }
        //    else if (ObjMoldInfo == null)
        //    {
        //        return UserControl.MutiLanguages.ParserMessage("$PDA_MoldID_Not_Exist");
        //    }
        //    if (status == Web.Helper.MoldStatus.InStore)
        //    {


        //        //if(_domainDataProvider==null)
        //        //    _domainDataProvider = this.DataProvider;
        //        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
        //        int Mdate = dbDateTime.DBDate;
        //        int Mtime = dbDateTime.DBTime;
        //        string UpdateStatus = Web.Helper.MoldStatus.OutStore;

        //        if (_WarehouseFacade == null)
        //            _WarehouseFacade = new WarehouseFacade();
        //        try
        //        {

        //            _WarehouseFacade.UpdateMoldStatus(moldid, UpdateStatus, user, Mdate, Mtime, user, Mdate, Mtime);
        //            string[] res = new string[2];
        //            string date = DateTime.Now.ToString("yyyyMMdd");
        //            if (_pf == null)
        //            {
        //                _pf = new Package.PackageFacade(this.DataProvider);
        //            }
        //            int count = _pf.GetModelInOrOutByMdate(date, "out", user);
        //            if (count != -1)
        //            {
        //                res[1] = count.ToString();
        //            }
        //            res[0] = "OK";
        //            return new BaseService().StrsToJson(res);

        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error(ex.Message);
        //            return UserControl.MutiLanguages.ParserMessage(ex.Message);


        //        }
        //    }
        //    else
        //    {
        //        return UserControl.MutiLanguages.ParserMessage("$PDA_MoldIDstatus_Is_Error");
        //    }
        //    //return moldid+"模具条码采集失败";
        //}
        //#endregion
        //[WebMethod]
        //public DataTable GetOutStoreTable()
        //{
        //    try
        //    {
        //        DBDateTime dbdatetime = FormatHelper.GetNowDBDateTime(DataProvider);
        //        int OutDate = dbdatetime.DBDate;
        //        DataTable dt = new DataTable("OutStoreDetail");
        //        if (_WarehouseFacade == null)
        //            _WarehouseFacade = new WarehouseFacade(DataProvider);
        //        dt.Rows.Clear();
        //        dt.Columns.Add("序号", typeof(string));
        //        dt.Columns.Add("模具条码", typeof(string));
        //        object[] objGetMoldInfo = _WarehouseFacade.GetAllItem(Web.Helper.MoldStatus.OutStore, OutDate);
        //        if (objGetMoldInfo != null)
        //        {
        //            for (int i = 0; i < objGetMoldInfo.Length; i++)
        //            {

        //                dt.Rows.Add(new object[] { (i + 1).ToString(), ((Mold)objGetMoldInfo[i]).Moldid });
        //            }

        //        }

        //        else
        //        {

        //            dt.Rows.Clear();
        //        }
        //        return dt;
        //    }
        //    catch(Exception ex)
        //    {

        //        return null;
        //        throw;

        //    }

        //}
    }
}
