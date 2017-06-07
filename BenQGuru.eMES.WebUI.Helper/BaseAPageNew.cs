using System;
using System.Web.UI;
using System.Collections;

using Infragistics.Web.UI.GridControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Infragistics.Web.UI;
using Infragistics.Web.UI.LayoutControls;

namespace BenQGuru.eMES.Web.Helper
{
    public class BaseAPageNew : BaseMPageNew
    {
       
        public BaseAPageNew()
            : base()
        {

        }

        protected override void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        private void Page_Load(object sender, System.EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.FindControl("cmdSave") != null)
                {
                    (this.FindControl("cmdSave") as HtmlControl).Attributes.Add("onclick", "window.returnValue=true;window.close();");
                }
                if (this.FindControl("cmdCancel") != null)
                {
                    (this.FindControl("cmdCancel") as HtmlControl).Attributes.Add("onclick", "window.close();");
                }
            }
        }
        protected override void cmdAdd_Click(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            object obj = null;

            if (array.Count > 0)
            {
                ArrayList objList = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    obj = this.GetEditObject(row);

                    if (obj != null)
                    {
                        objList.Add(obj);
                    }
                }

                this.AddDomainObject(objList);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Add);
            }
        }

        protected override void cmdQuery_Click(object sender, System.EventArgs e)
        {
            this.gridHelper.RequestData();
            if (this.gridHelper2 != null)
                this.gridHelper2.RequestData();
           
        }
        protected override void cmdSave_Click(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            object obj = null;

            if (array.Count > 0)
            {
                ArrayList objList = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    obj = this.GetEditObject(row);

                    if (obj != null)
                    {
                        objList.Add(obj);
                    }
                }

                this.AddDomainObject(objList);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Select);
            }
        }

        protected override void cmdSelect_Click(object sender, System.EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();
            object obj = null;

            if (array.Count > 0)
            {
                ArrayList objList = new ArrayList(array.Count);

                foreach (GridRecord row in array)
                {
                    obj = this.GetEditObject(row);

                    if (obj != null)
                    {
                        objList.Add(obj);
                    }
                }

                this.AddDomainObject(objList);

                this.gridHelper.RequestData();
                this.buttonHelper.PageActionStatusHandle(PageActionType.Select);
            }
        }
        protected virtual void AddDomainObject(ArrayList domainObject)
        {
        }
    }
}
