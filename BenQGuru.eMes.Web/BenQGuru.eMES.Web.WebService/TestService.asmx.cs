using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// ServiceTest 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class TestService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        ///计算减法
        public double CalcMinus(double val1,double val2)
        {
            return val1 - val2;
        }

        [WebMethod]
        public DataTable GetDataTable()
        {
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            
            dt.Columns.Add("Num", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Age", typeof(int));
            dt.Rows.Add("1", "Tom", 22);
            dt.Rows.Add("2", "Tom", 23);
            dt.Rows.Add("3", "Jim", 25);
            dt.Rows.Add("4", "Tom", 28);
            dt.Rows.Add("5", "Tom", 25);
            return dt;
        }

        [WebMethod]
        public User GetUserInfo(string userCode)
        {
            UserFacade userFacade = new UserFacade();
            object objUser=userFacade.GetUser(userCode);
            if (objUser!=null)
            {
                User user = objUser as User;
                return user;
            }
            return null;
        }

        [WebMethod(Description="从Client提交数组的Demo")]
        public string PostStringArray(string[] strArr)
        {
            if (strArr!=null&&strArr.Length>0)
            {
                return string.Format("数组长度是：{0} 数组第一个元素是：{1} ", strArr.Length, strArr[0]);
            }
            else
            {
                return "数组为空或者为NULL";
            }

        }

        [WebMethod(Description = "从Client提交DataTable的Demo")]
        public string PostDataTable(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return string.Format("DataTable 行数是：{0} 第一行第一列是：{1} ", dt.Rows.Count, dt.Rows[0][0].ToString());
            }
            else
            {
                return "DataTable为空或者为NULL";
            }

        }

        [WebMethod(Description = "返回数组的Demo")]
        public string[] ReturnStringArray()
        {
            return new string[] { "Demo", "demo", "hello" };
        }
    }
}
