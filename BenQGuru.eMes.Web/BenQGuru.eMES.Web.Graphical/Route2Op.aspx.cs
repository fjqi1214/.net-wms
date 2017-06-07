using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using System.Web.Script.Serialization;

namespace BenQGuru.eMES.Web.Graphical
{
    public partial class Route2Op : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ClearContent(); 
            Response.Clear(); 
            Response.ContentType = "text/plain";
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //OracleDbHelper oracleHelper = new OracleDbHelper("Mes64");
            //IDomainDataProvider dataProvider = DomainDataProviderManager.DomainDataProvider();
            BaseModelFacade facade = new BaseModelFacade(this.DataProvider);

            string action = Request["action"];
            string routecode = Request["routecode"];

            if (action == "getRouteByCode")
            {
                Route route = facade.GetRoute(routecode) as Route;

                string strJson = jss.Serialize(route);
                Response.Write(strJson);
            }
            else if (action == "getOpByRoute")
            {

                object[] opList = facade.GetOpByRouteCode(routecode);

                string strJson = jss.Serialize(opList);

                Response.Write(strJson);
            }
            else if (action == "getOtherOp")
            {
                string opcode = Request["opcode"];
                if (opcode == null)
                    opcode = string.Empty;

                object[] opList = facade.GetOtherOpByRouteCode(routecode, opcode);
                string strJson = jss.Serialize(opList);

                Response.Write(strJson);
            }
            else if (action == "save")
            {
                string opJson = Request["opJson"];
                Op[] opList = jss.Deserialize<Op[]>(opJson);
                //List<Op> opList = opArray.ToList<Op>();
                //context.Response.Write(jss.Serialize(opList));

                try
                {
                    this.DataProvider.BeginTransaction();

                    foreach (Op op in opList)
                    {
                        Route2Operation opNew = new Route2Operation();
                        opNew.RouteCode = routecode;
                        opNew.OPCode = op.OpCode;
                        opNew.OPSequence = op.OpSeq;
                        opNew.OPControl = op.OpControl;
                        opNew.MaintainUser = this.GetUserCode();
                        opNew.MaintainDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                        opNew.MaintainTime = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));

                        if (op.DataType == 0)
                        {
                            facade.AddRoute2Operation(opNew);
                        }
                        else if (op.DataType == 1)
                        {
                            facade.UpdateRoute2Operation(opNew,false);
                        }
                        else if (op.DataType == 2)
                        {
                            facade.DeleteRoute2Operation(opNew);
                        }

                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    Response.StatusCode = 500;
                    Response.Write(ex.Message);
                    Response.End();

                }

            }

            Response.End(); 
        }
    }
}