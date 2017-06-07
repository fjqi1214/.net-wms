using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Infragistics.WebUI.UltraWebNavigator;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.SMT;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.BaseSetting ;

namespace BenQGuru.eMES.Web.SMT
{
	/// <summary>
	/// SMTLoadingHelper 的摘要说明。
	/// </summary>
	public class SMTLoadingHelper
	{
        private Infragistics.WebUI.UltraWebNavigator.UltraWebTree _tree;

        public Infragistics.WebUI.UltraWebNavigator.UltraWebTree Tree
        {
            get
            {
                return this._tree ;
            }

            set
            {
                this._tree = value ;
            }
        }

		public SMTLoadingHelper()
		{
		}

        public void BuildRouteTree(Route route)
        {
            BenQGuru.eMES.BaseSetting.BaseModelFacade baseFacade = SMTFacadeFactory.CreateBaseModelFacadeFacade() ;
            Node root = this._tree.Nodes.Add(route.RouteCode) ;

            object[] ops = baseFacade.GetSelectedOperationByRouteCode(route.RouteCode,string.Empty,1,int.MaxValue) ;
            if(ops != null)
            {
                foreach(Operation op in ops)
                {
                    Node opNode = root.Nodes.Add( op.OPCode ) ;
                    object[] reses = baseFacade.GetSelectedResourceByOperationCode(op.OPCode,string.Empty,1,int.MaxValue) ;
                    if(reses != null)
                    {
                        foreach(Resource res in reses)
                        {
                            opNode.Nodes.Add( res.ResourceCode ) ;
                        }
                        opNode.Expanded = true ;
                    }
                }

                root.Expanded = true ;
            }


            
        }
	}
}
