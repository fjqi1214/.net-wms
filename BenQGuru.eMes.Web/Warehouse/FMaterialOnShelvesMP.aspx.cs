using System;
using System.Collections.Generic;
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
using BenQGuru.eMES.SAPRFCService;
using BenQGuru.eMES.SAPRFCService.Domain;


namespace BenQGuru.eMES.Web.WarehouseWeb
{
    public partial class FMaterialOnShelvesMP : BaseMPageNew
    {

        protected ControlLibrary.Web.Language.LanguageComponent languageComponent1;
        private System.ComponentModel.IContainer components;

        protected System.Web.UI.HtmlControls.HtmlInputButton Submit1;
        private static string poStno = string.Empty;
        private static string poStline = string.Empty;

        private static decimal editQTY = 0;  //记录编辑前，编辑行的原有数量
        private WarehouseFacade facade = null;
        private InventoryFacade _Invenfacade = null;
        private BenQGuru.eMES.Material.InventoryFacade inventoryFacade = null;
        private InventoryFacade _InventoryFacade = null;
        private WarehouseFacade _WarehouseFacade = null;
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

                this.InitPageLanguage(this.languageComponent1, false);
            }
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }
            this.txtPlanOnshelves.Text = facade.QueryPlanOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoEdit.Text))).ToString();
            this.txtActOnshelves.Text = facade.QueryActOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoEdit.Text))).ToString();
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
            this.gridHelper.AddColumn("ASN", "入库指令号", null);
            this.gridHelper.AddColumn("BigCartonNO", "大箱", null);
            this.gridHelper.AddColumn("BoxNo", "箱号", null);
            this.gridHelper.AddColumn("RecomLocation", "推荐货位", null);
            this.gridHelper.AddColumn("LocationNO", "货位号", null);
            this.gridHelper.AddColumn("DQLotNO", "鼎桥批次号", null);
            this.gridHelper.AddColumn("State", "状态", null);
            this.gridHelper.AddColumn("InitialCheck", "初检结果", null);
            //this.gridHelper.AddColumn("IQCCheckMode", "IQC检验方式", null);
            //this.gridHelper.AddColumn("IQCCheckResult", "IQC检验结果", null);
            this.gridHelper.AddColumn("DQMaterialNO", "鼎桥物料编码", null);
            this.gridHelper.AddColumn("DQMaterialDesc", "鼎桥物料描述", null);
            this.gridHelper.AddColumn("VendorMCODE", "供应商物料编码", null);
            this.gridHelper.AddColumn("VendorMCODEDesc", "供应商物料描述", null);
            //this.gridHelper.AddColumn("MaterialDes", "物料描述", null);
            this.gridHelper.AddColumn("ASNQTY", "来料数量", null);
            this.gridHelper.AddColumn("ReceivedQTY", "已接收数量", null);
            this.gridHelper.AddColumn("IQCOKQTY", "IQC合格数量", null);
            this.gridHelper.AddColumn("InstorageQTY", "已入库数量", null);
            this.gridHelper.AddColumn("MUOM", "单位", null);
            this.gridHelper.AddColumn("DateCode", "生产日期", null);
            this.gridHelper.AddColumn("VendorLotNo", "供应商批次", null);
            this.gridHelper.AddColumn("McontrolType", "物料管控类型", null);
            this.gridHelper.AddColumn("Memo", "备注", null);
            this.gridHelper.AddColumn("stline", "line", null);

            this.gridWebGrid.Columns.FromKey("stline").Hidden = true;

            this.gridHelper.AddDefaultColumn(true, false);

            //多语言
            this.gridHelper.ApplyLanguage(this.languageComponent1);
            //this.gridHelper.RequestData();
        }

        protected override DataRow GetGridRow(object obj)
        {
            DataRow row = this.DtSource.NewRow();
            row["ASN"] = ((Asndetailexp)obj).Stno;
            row["BigCartonNO"] = ((Asndetailexp)obj).Cartonbigseq;
            row["BoxNo"] = ((Asndetailexp)obj).Cartonno;
            row["RecomLocation"] = ((Asndetailexp)obj).ReLocationCode;
            row["LocationNO"] = ((Asndetailexp)obj).LocationCode;
            row["DQLotNO"] = ((Asndetailexp)obj).Lotno;
            row["State"] = ((Asndetailexp)obj).Status;          //状态需要改一下
            row["InitialCheck"] = ((Asndetailexp)obj).InitreceiveStatus;
            row["DQMaterialNO"] = ((Asndetailexp)obj).DqmCode;
            row["DQMaterialDesc"] = ((Asndetailexp)obj).MDesc;
            row["VendorMCODE"] = ((Asndetailexp)obj).VEndormCode;
            row["VendorMCODEDesc"] = ((Asndetailexp)obj).VEndormCodeDesc;
            //row["MaterialDes"] = ((Asndetailexp)obj).stno.ToString();
            row["ASNQTY"] = ((Asndetailexp)obj).Qty.ToString();
            row["ReceivedQTY"] = ((Asndetailexp)obj).ReceiveQty.ToString();
            row["IQCOKQTY"] = ((Asndetailexp)obj).QcpassQty.ToString();
            row["InstorageQTY"] = ((Asndetailexp)obj).ActQty.ToString();
            row["MUOM"] = ((Asndetailexp)obj).Unit;
            row["DateCode"] = FormatHelper.ToDateString(((Asndetailexp)obj).Production_Date);
            row["VendorLotNo"] = ((Asndetailexp)obj).Supplier_lotno;
            row["McontrolType"] = ((Asndetailexp)obj).MControlType;
            row["Memo"] = ((Asndetailexp)obj).Remark1;
            row["stline"] = ((Asndetailexp)obj).Stline.ToString();
            return row;
        }


        protected override object[] LoadDataSource(int inclusive, int exclusive)
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }



            object[] objs = this.facade.QueryOnshelvesDetail(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoEdit.Text)),
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationNO.Text))
              );

            //List<string> stnos = new List<string>();
            //if (objs != null && objs.Length > 0)
            //{
            //    foreach (Asndetailexp o in objs)
            //    {
            //        if (!stnos.Contains(o.Stno))
            //            stnos.Add(o.Stno);
            //    }
            //}

            //BenQGuru.eMES.Domain.IQC.AsnIQC[] iqcs = facade.GetASNIQCFromASN(stnos);
            //List<string> IQCNos = new List<string>();
            //foreach (BenQGuru.eMES.Domain.IQC.AsnIQC iqc in iqcs)
            //{
            //    if (iqc.IqcType == "SpotCheck")
            //    {
            //        IQCNos.Add(iqc.IqcNo);
            //    }
            //}
            //BenQGuru.eMES.Domain.IQC.AsnIQCDetailEc[] ECs = facade.GetIQCECFromIQCNo(IQCNos);
            //List<object> passObjs = new List<object>();
            //if (objs != null && objs.Length > 0)
            //{
            //    foreach (Asndetailexp o in objs)
            //    {
            //        bool isOk = true;
            //        foreach (BenQGuru.eMES.Domain.IQC.AsnIQCDetailEc ec in ECs)
            //        {
            //            if (ec.StNo == o.Stno)
            //                isOk = false;
            //        }
            //        if (isOk)
            //            passObjs.Add(o);
            //    }
            //}
            //return passObjs.ToArray();
            return objs;

        }

        protected override int GetRowCount()
        {
            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }



            object[] objs = this.facade.QueryOnshelvesDetail(
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoEdit.Text)),
               FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtLocationNO.Text))
              );

            List<string> stnos = new List<string>();
            if (objs != null && objs.Length > 0)
            {
                foreach (Asndetailexp o in objs)
                {
                    if (!stnos.Contains(o.Stno))
                        stnos.Add(o.Stno);
                }
            }

            BenQGuru.eMES.Domain.IQC.AsnIQC[] iqcs = facade.GetASNIQCFromASN(stnos);
            List<string> IQCNos = new List<string>();
            foreach (BenQGuru.eMES.Domain.IQC.AsnIQC iqc in iqcs)
            {
                if (iqc.IqcType == "SpotCheck")
                {
                    IQCNos.Add(iqc.IqcNo);
                }
            }
            BenQGuru.eMES.Domain.IQC.AsnIQCDetailEc[] ECs = facade.GetIQCECFromIQCNo(IQCNos);
            List<object> passObjs = new List<object>();
            if (objs != null && objs.Length > 0)
            {
                foreach (Asndetailexp o in objs)
                {
                    bool isOk = true;
                    foreach (BenQGuru.eMES.Domain.IQC.AsnIQCDetailEc ec in ECs)
                    {
                        if (ec.StNo == o.Stno)
                            isOk = false;
                    }
                    if (isOk)
                        passObjs.Add(o);
                }
            }
            return passObjs.Count;

        }

        #endregion

   

        protected override void cmdSave_Click(object sender, EventArgs e)
        {
            ArrayList array = this.gridHelper.GetCheckedRows();

  
            List<Asndetail> asnDetailList = new List<Asndetail>();
            if (array.Count == 0)
            {
                WebInfoPublish.Publish(this, "请至少选择一条数据", this.languageComponent1);
                return;
            }
            foreach (GridRecord row in array)
            {
                object obj = null;
                obj = this.GetEditObject(row);
                if (obj == null)
                    throw new Exception("行中获取的ASN明细为空！");
                Asndetail asndetail = obj as Asndetail;

                asnDetailList.Add(asndetail);
            }
            string message = string.Empty;
            ShareLib.ShareKit kit = new ShareLib.ShareKit();


            if (kit.OnShelf(txtCartonNoEdit.Text, txtLocationNO.Text, asnDetailList, base.DataProvider, out message, GetUserCode()))
            {
                this.txtPlanOnshelves.Text = facade.QueryPlanOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoEdit.Text))).ToString();
                this.txtActOnshelves.Text = facade.QueryActOnShelvesQTY(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(this.txtCartonNoEdit.Text))).ToString();
                WebInfoPublish.Publish(this, "上架成功！", this.languageComponent1);

            }
            else
            {
                WebInfoPublish.Publish(this, "上架失败！" + message, this.languageComponent1);
            }
            
        }

       

      
        

        protected void cmdCheck_ServerClick(object sender, EventArgs e)
        {


            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(base.DataProvider);
            }
            if (this.gridWebGrid.Rows.Count == 0)
            {
                //提示错误
                WebInfoPublish.PublishInfo(this, "Grid中没有数据", this.languageComponent1);
                return;
            }
            List<AsnHead> obj = _InventoryFacade.QueryASNDetailSNCatron(this.txtCartonnoSN.Text.Trim());


            string stNO = "";
            string stLine = "";

            if (obj != null)
            {
                for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                {
                    this.gridWebGrid.Rows[i].Items.FindItemByKey("Check").Value = false;
                }
                foreach (AsnHead o in obj)
                {

                    for (int i = 0; i < this.gridWebGrid.Rows.Count; i++)
                    {

                        stNO = this.gridWebGrid.Rows[i].Items.FindItemByKey("ASN").Text;
                        stLine = this.gridWebGrid.Rows[i].Items.FindItemByKey("stline").Text;

                        if ((o.STNO == stNO && (o.STlINE == stLine)))
                        {
                            this.gridWebGrid.Rows[i].Items.FindItemByKey("Check").Value = true;

                        }
                    }
                }


            }
            else
            {
                //提示没找到相关信息
                WebInfoPublish.PublishInfo(this, "没有匹配入库指令号信息", this.languageComponent1);
                return;
            }

        }


    
    

       




        private void ShowMessage(string msg)
        {
            WebInfoPublish.Publish(this, msg, this.languageComponent1);
        }
        #region Object <--> Page

        protected override object GetEditObject(GridRecord row)
        {

            if (facade == null)
            {
                facade = new WarehouseFacade(base.DataProvider);
            }

            object obj = facade.GetAsndetail(int.Parse(row.Items.FindItemByKey("stline").Value.ToString()), row.Items.FindItemByKey("ASN").Value.ToString());

            if (obj != null)
            {
                return (Asndetail)obj;
            }

            return null;
        }



        #endregion


    }

   
}
