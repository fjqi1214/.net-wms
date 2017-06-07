using System;
using System.Data;
using System.Configuration;
using System.Collections;
using BenQGuru.eMES.Common;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using Infragistics.Web.UI.GridControls;
using Infragistics.Web.UI.LayoutControls;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FPODistributionQTYMP : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private static string poStno = string.Empty;
        private static string poStline = string.Empty;
        private static string DQMCode = string.Empty;
       
        private static decimal editQTY = 0;  //记录编辑前，编辑行的原有数量
        private WarehouseFacade facade = null;
        private InventoryFacade _Invenfacade = null;

        #region Web 窗体设计器生成的代码
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.languageComponent1 = new ControlLibrary.Web.Language.LanguageComponent(this.components);
            // 
            // languageComponent1
            // 
            this.languageComponent1.Language = "CHS";
            this.languageComponent1.LanguagePackageDir = "D:\\SQC2.0\\eMES\\Source\\bin";
            this.languageComponent1.RuntimePage = null;
            this.languageComponent1.RuntimeUserControl = null;
            this.languageComponent1.UserControlName = "";
           

        }
        #endregion


        #region Init
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 初始化页面语言
                poStno = Request.QueryString["Stno"].ToString();
                poStline = Request.QueryString["Stline"].ToString();
                DQMCode = Request.QueryString["DQMCode"].ToString();
                this.InitPageLanguage(this.languageComponent1, false);
             }
        }

        protected override ControlLibrary.Web.Language.LanguageComponent GetLanguageComponent()
        {
            return this.languageComponent1;
        }

       
        #endregion

        #region WebGrid
        protected override void InitWebGrid()
        {
            base.InitWebGrid();
            this.gridHelper.AddColumn("POITEMNO", "ITEM号", null);
            this.gridHelper.AddColumn("DQMaterialNO", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("POQTY", "订单数量", null);
            this.gridHelper.AddColumn("IN1QTY", "已导数量", null);
            this.gridHelper.AddColumn("INQTY", "来料数量", false);
            //this.gridHelper.AddColumn("INQTY1", "来料数量", null);
            //this.gridHelper.AddColumn("STLINE", "STLINE", null);
            this.gridHelper.AddColumn("STNO", "STNO", null);
            this.gridHelper.AddColumn("invno", "invno", null);

       //     this.gridWebGrid.Columns.FromKey("INQTY1").Hidden = true;
            //this.gridWebGrid.Columns.FromKey("STLINE").Hidden = true;
            this.gridWebGrid.Columns.FromKey("STNO").Hidden = true;
            this.gridWebGrid.Columns.FromKey("invno").Hidden = true;
            this.gridHelper.AddDefaultColumn(false, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);

            this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["POITEMNO"] = ((invoicedetailEX)obj).InvLine.ToString();
            row["DQMaterialNO"] = ((invoicedetailEX)obj).DQMCode;
            row["POQTY"] = ((invoicedetailEX)obj).PlanQty.ToString();
            row["IN1QTY"] = Convert.ToInt32(((invoicedetailEX)obj).INQTY).ToString();
            row["INQTY"] =Convert.ToInt32(((invoicedetailEX)obj).EQTY).ToString();
            //row["STLINE"] = ((invoicedetailEX)obj).stline.ToString();
            row["STNO"] = ((invoicedetailEX)obj).stno.ToString();
            row["invno"] = ((invoicedetailEX)obj).InvNo;
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryPODistriQTY(
                poStno,
                poStline
               );
        }
        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            return this.facade.QueryPODistriQTYcount(
                poStno,
                poStline
            );
        }

        #endregion

        #region Button

        protected void cmdSave_click(object sender, EventArgs e)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(base.DataProvider);
            }
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            int sum = 0;
            int insum=0;
            object[] objs_asnd = facade.GetASNDetailByStNoAndDQMCode(poStno, DQMCode);
            if (objs_asnd != null)
            {
                #region 检查维护的数量是否等于导入数量
                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(this.gridWebGrid.Rows[i].Items.FindItemByKey("INQTY").Value.ToString()))
                    {
                        try
                        {
                            insum += int.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("INQTY").Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            WebInfoPublish.Publish(this, "来料数量必须是数字格式" + ex.Message, this.languageComponent1);
                            return;
                        }
                    }
                }
                foreach (Asndetail asnd in objs_asnd)
                {
                    sum += asnd.Qty;
                }
                if (insum != sum)
                {
                    WebInfoPublish.Publish(this, "分配数量不等于导入数量", this.languageComponent1);
                    return;
                }
                #endregion
                this.DataProvider.BeginTransaction();
                #region 删除asndetailitem表中原有的数据（stno，dqmcode）
                foreach (Asndetail asnd in objs_asnd)
                {
                    object[] objs_asnditem = facade.GetASNDetailItembyStnoAndStline(asnd.Stno, asnd.Stline);
                    foreach (Asndetailitem asnditem_old in objs_asnditem)
                    {
                        facade.DeleteAsndetailitem(asnditem_old);
                    }
                }
                #endregion
                //#region 根据grid表重新写asndetailitem
                //int PlanQty = 0;
                //for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                //{
                //    if (!string.IsNullOrEmpty(this.gridWebGrid.Rows[i].Items.FindItemByKey("POQTY").Value.ToString()) || int.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("POQTY").Value.ToString())!=0)
                //    {
                //        PlanQty = int.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("POQTY").Value.ToString());
                //        Asndetailitem asnditem_new = facade.CreateNewAsndetailitem();
                //        foreach (Asndetail asnd in objs_asnd)
                //        {
                //            if(asnd.Qty)
                //        }
                //    }
                //}
                #region   插入tblasndetailITEM
                foreach (Asndetail asnd in objs_asnd)
                {
                    Asndetailitem detailitem = facade.CreateNewAsndetailitem();
                    detailitem.CDate = dbTime.DBDate;
                    detailitem.CTime = dbTime.DBTime;
                    detailitem.CUser = this.GetUserCode();
                    detailitem.MaintainDate = dbTime.DBDate;
                    detailitem.MaintainTime = dbTime.DBTime;
                    detailitem.MaintainUser = this.GetUserCode();
                    detailitem.Stline = asnd.Stline.ToString();
                    detailitem.Stno = asnd.Stno;
                    detailitem.MCode = asnd.MCode;
                    detailitem.DqmCode = asnd.DqmCode;
                    //detailitem.ActQty = 0;
                    //detailitem.QcpassQty = 0;
                    //detailitem.ReceiveQty = 0;
                    //查找对应的SAP单
                    decimal sub = asnd.Qty;
                    for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(this.gridWebGrid.Rows[i].Items.FindItemByKey("INQTY").Value.ToString()) || int.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("INQTY").Value.ToString()) != 0)
                        {

                            //InvoicesDetail invdetail = qtyobjs[i] as InvoicesDetail;
                            decimal subNeed = decimal.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("POQTY").Value.ToString()) - decimal.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("IN1QTY").Value.ToString());
                           
                            if (subNeed < decimal.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("INQTY").Value.ToString()))
                            {
                                this.DataProvider.RollbackTransaction();
                                WebInfoPublish.Publish(this, "分配数量大于所需数量", this.languageComponent1);
                                return;
                            }
                            object findNeedQTY_now = facade.GetNeedImportQtyNow(this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString(), Int32.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString()), asnd.Stno);  //找这个invoice行已经导入了多少，和判退多少
                            Asndetailitem subItemNow = findNeedQTY_now as Asndetailitem;

                            subNeed = decimal.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("INQTY").Value.ToString()) - subItemNow.Qty;
                            if (subNeed == 0)
                                continue;

                            //如果箱数量大于需求数量差---进行拆分
                            if (sub > subNeed)
                            {
                                sub = sub - subNeed;  //  sub是剩余的
                                detailitem.Qty = subNeed;
                                detailitem.Invline = this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString();
                                detailitem.Invno = this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString();
                                detailitem.ActQty = detailitem.Qty;
                                detailitem.QcpassQty = detailitem.Qty;
                                detailitem.ReceiveQty = detailitem.Qty;
                                facade.AddAsndetailitem(detailitem);


                            }

                            //如果箱单数量小于等于需求数量差--直接填入
                            else
                            {

                                detailitem.Qty = sub;
                                detailitem.Invline = this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString();
                                detailitem.Invno = this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString();
                                detailitem.ActQty = detailitem.Qty;
                                detailitem.QcpassQty = detailitem.Qty;
                                detailitem.ReceiveQty = detailitem.Qty;
                                facade.AddAsndetailitem(detailitem);
                                sub = 0;

                            }
                            if (sub == 0)
                            {
                                break;
                            }
                        }
                    }
                    //如果sub>0，说明导入数量过多，报错
                    if (sub > 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        WebInfoPublish.Publish(this, "$Error_SAP_NEED_DATA_ERROR", this.languageComponent1);
                        // BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_SAP_NEED_DATA_ERROR");
                        return;
                    }
                }
                #endregion

            }
            else
            {
                this.DataProvider.RollbackTransaction();
                WebInfoPublish.Publish(this, "AsnDetail中没有数据", this.languageComponent1);
                return;
            
            }
            this.DataProvider.CommitTransaction();
            WebInfoPublish.Publish(this, "保存成功", this.languageComponent1);
            this.gridHelper.RequestData();
            return;
        }
        //protected override void UpdateDomainObject(object domainObject)
        //{

        //    if (facade == null)
        //    {
        //        facade = new WarehouseFacade(base.DataProvider);
        //    }
        //    if (_Invenfacade == null)
        //    {
        //        _Invenfacade = new InventoryFacade(base.DataProvider);
        //    }
        //    Asndetailitem item = domainObject as Asndetailitem;
        //    DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

        //    if (editQTY - item.Qty < 0)
        //    {
        //        BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_OVER_QTY");
        //        return;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            this.DataProvider.BeginTransaction();
                    
        //            decimal sub = editQTY - item.Qty;
        //            bool ISadd = false;
        //            for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
        //            {
        //                if (this.gridWebGrid.Rows[i].Items[this.gridWebGrid.Columns.FromKey("INQTY").Index].Text=="0")
        //                {
        //                    //如果grid中有空行，剩余的数量放在空行
        //                    Asndetailitem item_1 = facade.CreateNewAsndetailitem();


        //                    object obj = _Invenfacade.GetInvoicesDetail(this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString(),int.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString()));
        //                    InvoicesDetail inv = obj as InvoicesDetail;
        //                    item_1.Stline = poStline;
        //                    item_1.Stno = poStno;
        //                    item_1.Invno = this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString();
        //                    item_1.Invline = this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString();
        //                    if (inv.PlanQty > sub)
        //                    {
        //                        item_1.Qty = sub;
        //                        sub = 0;
        //                    }
        //                    else
        //                    {
        //                        item_1.Qty = inv.PlanQty;
        //                        sub = sub - inv.PlanQty;
        //                    }
        //                    item_1.ActQty = 0;
        //                    item_1.CDate = dbTime.DBDate;
        //                    item_1.CTime = dbTime.DBTime;
        //                    item_1.CUser = this.GetUserCode();
        //                    item_1.DqmCode = inv.DQMCode;
        //                    item_1.MaintainDate = dbTime.DBDate;
        //                    item_1.MaintainTime = dbTime.DBTime;
        //                    item_1.MaintainUser = this.GetUserCode();
        //                    item_1.MCode = inv.MCode;
        //                    item_1.QcpassQty = 0;
        //                    item_1.ReceiveQty = 0;
        //                    facade.AddAsndetailitem(item_1);
        //                    if (sub==0)
        //                        break;
        //                }

        //            }
        //            if (sub!=0)
        //            {
        //                for (int i = this.gridWebGrid.Rows.Count - 1; i >= 0; i--)
        //                {
        //                    if (this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString() != item.Invno || this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString() != item.Invline)
        //                    {
        //                        object obj1 = _Invenfacade.GetInvoicesDetail(this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString(),int.Parse(this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString()));
        //                        InvoicesDetail inv = obj1 as InvoicesDetail;
        //                        object obj = facade.GetAsndetailitem(this.gridWebGrid.Rows[i].Items.FindItemByKey("invno").Value.ToString(), int.Parse(this.stline.Text.ToString()), this.gridWebGrid.Rows[i].Items.FindItemByKey("POITEMNO").Value.ToString(), this.stno.Text);
        //                        Asndetailitem item_1 = obj as Asndetailitem;
        //                        if (inv.PlanQty > item_1.Qty + sub)
        //                        {
        //                            item_1.Qty = item_1.Qty + sub;
        //                            sub = 0;
        //                        }
        //                        else
        //                        {
        //                            sub = sub - (inv.PlanQty - item_1.Qty);
        //                            item_1.Qty = inv.PlanQty;
                                    
        //                        }
        //                        item_1.MaintainDate = dbTime.DBDate;
        //                        item_1.MaintainTime = dbTime.DBTime;
        //                        item_1.MaintainUser = this.GetUserCode();

        //                        facade.UpdateAsndetailitem(item_1);
        //                        if (sub == 0)
        //                            break;
        //                    }

        //                }
        //            }
        //            if (sub != 0)
        //            {
        //                this.DataProvider.RollbackTransaction();
        //                ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
        //            }
        //            if (item.Qty == 0)
        //            {
        //                facade.DeleteAsndetailitem(item);
        //            }
        //            else
        //            {
        //                facade.UpdateAsndetailitem(item);
        //            }
        //            this.DataProvider.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            this.DataProvider.RollbackTransaction();
        //            ExceptionManager.Raise(this.GetType().BaseType, "$Error_Data_Error");
        //        }
        //    }
        //}



        protected void cmdReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(this.MakeRedirectUrl("FCartonDataImpMP.aspx",
                                   new string[] { "ASN" },
                                   new string[] { poStno }));
        }

        #endregion

        #region Object <--> Page


        protected override object GetEditObject(GridRecord row)
        {
            if (row.Items.FindItemByKey("INQTY").Value.ToString() == "0")
            {
                BenQGuru.eMES.Common.ExceptionManager.Raise(this.GetType().BaseType, "$Error_NOEDIT_INQTY_IS_0");
                return null;
            }
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            //int orgId = GlobalVariables.CurrentOrganizations.First().OrganizationID;
           
            object obj = facade.GetAsndetailitem(row.Items.FindItemByKey("invno").Value.ToString(), int.Parse(row.Items.FindItemByKey("STLINE").Value.ToString()), row.Items.FindItemByKey("POITEMNO").Value.ToString(), row.Items.FindItemByKey("STNO").Value.ToString());

            if (obj != null)
            {
                return (Asndetailitem)obj;
            }

            return null;
        }


        #endregion

       
    }
}
