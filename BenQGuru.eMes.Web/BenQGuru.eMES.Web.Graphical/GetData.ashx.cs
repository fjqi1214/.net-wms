using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.Services;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
/// <summary>
/// GetData 的摘要说明
/// </summary>
/// 

namespace BenQGuru.eMES.Web.Graphical
{

    public class GetData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            JavaScriptSerializer jss = new JavaScriptSerializer();

            //OracleDbHelper oracleHelper = new OracleDbHelper("Mes64");
            IDomainDataProvider dataProvider=DomainDataProviderManager.DomainDataProvider();
            BaseModelFacade facade=new BaseModelFacade(dataProvider);

            string action = context.Request["action"];
            string routecode = context.Request["routecode"];

            if (action == "getRouteByCode")
            {
                 Route route=facade.GetRoute(routecode) as Route;

                string strJson = jss.Serialize(route);
                context.Response.Write(strJson);
            }
            else if (action == "getOpByRoute")
            {

                OperationOfRoute[] opList = facade.GetOpByRouteCode(routecode) as OperationOfRoute[]; 
                    
                string strJson = jss.Serialize(opList);

                context.Response.Write(strJson);
            }
            else if (action == "getOtherOp")
            {
                string opcode = context.Request["opcode"];
                if (opcode == null)
                    opcode = string.Empty;

                Operation[] opList = facade.GetOtherOpByRouteCode(routecode, opcode) as Operation[]; 
                string strJson = jss.Serialize(opList);

                context.Response.Write(strJson);
            }
            else if (action == "save")
            {
                string opJson = context.Request["opJson"];
                Op[] opList = jss.Deserialize<Op[]>(opJson);
                //List<Op> opList = opArray.ToList<Op>();
                //context.Response.Write(jss.Serialize(opList));

                try
                {
                    dataProvider.BeginTransaction();

                    foreach (Op op in opList)
                    {
                        Route2Operation opNew = new Route2Operation();
                        opNew.RouteCode = routecode;
                        opNew.OPCode = op.OpCode;
                        opNew.OPSequence = op.OpSeq;
                        opNew.OPControl = op.OpControl + "0000000000";
                        opNew.MaintainUser = "Admin-Graphical";
                        opNew.MaintainDate = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                        opNew.MaintainTime = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));

                        if (op.DataType == 0)
                        {
                            facade.AddRoute2Operation(opNew);
                        }
                        else if (op.DataType == 1)
                        {
                            facade.UpdateRoute2Operation(opNew);     
                        }
                        else if (op.DataType == 2)
                        {
                            facade.DeleteRoute2Operation(opNew);
                        }
        
                    }
                    dataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    dataProvider.RollbackTransaction();
                    context.Response.StatusCode = 500;
                    context.Response.Write(ex.Message);
                    context.Response.End();

                }

            }
      
        
        }
    

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


    public class Op
    {
        public Op()
        {

        }

        public Op(string opCode, string opDesc, int opSeq, string opControl)
        {
            this.m_opCode = opCode;
            this.m_opDesc = opDesc;
            this.m_opSeq = opSeq;
            this.m_opControl = opControl;
        }

        public Op(string opCode, string opDesc, int opSeq, string opControl, int dataType)
        {
            this.m_opCode = opCode;
            this.m_opDesc = opDesc;
            this.m_opSeq = opSeq;
            this.m_opControl = opControl;
            this.m_dataType = dataType;
        }

        private string m_opCode;

        public string OpCode
        {
            get { return m_opCode; }
            set { m_opCode = value; }
        }

        private string m_opDesc;

        public string OpDesc
        {
            get { return m_opDesc; }
            set { m_opDesc = value; }
        }

        private int m_opSeq;

        public int OpSeq
        {
            get { return m_opSeq; }
            set { m_opSeq = value; }
        }

        private string m_opControl;

        public string OpControl
        {
            get { return m_opControl; }
            set { m_opControl = value; }
        }

        private int m_dataType;

        public int DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }

    }

}