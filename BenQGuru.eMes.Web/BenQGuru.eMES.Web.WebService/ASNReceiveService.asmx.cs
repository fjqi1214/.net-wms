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
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Warehouse;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using BenQGuru.eMES.SAPRFCService;
using BenQGuru.eMES.SAPRFCService.Domain;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// ASNReceiveService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ASNReceiveService : System.Web.Services.WebService
    {
        public ASNReceiveService()
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
        public AsnSimple GetAsnStatus(string stno)
        {

            AsnSimple asnSimple = new AsnSimple();
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            ASN asn = (ASN)_InventoryFacade.GetASN(stno);


            object[] objs = _InventoryFacade.GetASNDetailByStNo(stno);
            if (objs != null && objs.Length > 0)
            {
                foreach (ASNDetail d in objs)
                {
                    if (!string.IsNullOrEmpty(d.InitGIVEINDESC))
                    {
                        asnSimple.GiveReason = d.InitGIVEINDESC;
                        break;
                    }

                }
                foreach (ASNDetail d in objs)
                {
                    if (!string.IsNullOrEmpty(d.InitReceiveDesc))
                    {
                        asnSimple.RejectReason = d.InitReceiveDesc;
                        break;
                    }

                }
            }
            asnSimple.RejectCount = asn.InitRejectQty;

            return asnSimple;

        }


        [WebMethod(EnableSession = true)]
        public DataTable GetDataGrid(string ASNNo, bool istrail)
        {
            WarehouseFacade facade = null;
            if (facade == null)
            {
                facade = new WarehouseFacade(DataProvider);
            }

            Asn asn = (Asn)facade.GetAsn(ASNNo.Trim().ToUpper());
            AsndetailEX[] objs = null;
            if (!istrail)
                objs = facade.QueryASNDetailByAsnNoOrderByCartonno(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ASNNo)));
            else
                objs = facade.QueryTrailDetailByAsnNoOrderByCartonno(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ASNNo)));
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (objs != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("箱号编码", typeof(string));
                dt.Columns.Add("鼎桥料号", typeof(string));
                dt.Columns.Add("供应商物料", typeof(string));
                dt.Columns.Add("管控类型", typeof(string));
                dt.Columns.Add("来料数量", typeof(string));
                dt.Columns.Add("STLINE ", typeof(string));

                for (int i = 0; i < objs.Length; i++)
                {
                    BenQGuru.eMES.Domain.Warehouse.AsndetailEX Asndetail = (BenQGuru.eMES.Domain.Warehouse.AsndetailEX)objs[i];
                    string controlType = string.Empty;
                    if (Asndetail.MControlType == "item_control_lot")
                        controlType = "批次管控";
                    if (Asndetail.MControlType == "item_control_keyparts")
                        controlType = "单件管控";
                    if (Asndetail.MControlType == "item_control_nocontrol")
                        controlType = "不管控";
                    dt.Rows.Add(string.Empty, Asndetail.Cartonno, Asndetail.DqmCode, asn.StType == "UB" ? Asndetail.CustmCode : Asndetail.VEndormCode, controlType, Asndetail.Qty, Asndetail.Stline);

                }

            }
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetDataGridDoc(string ASNNo)
        {
            InventoryFacade facade = null;
            if (facade == null)
            {
                facade = new InventoryFacade(DataProvider);
            }

            object[] objs = facade.QueryInvDoc(FormatHelper.PKCapitalFormat(FormatHelper.CleanString(ASNNo)));
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (objs != null)
            {
                dt.Columns.Add("选择", typeof(string));

                dt.Columns.Add("文件名", typeof(string));
                dt.Columns.Add("文件大小", typeof(string));
                dt.Columns.Add("上传者", typeof(string));
                dt.Columns.Add("上传日期", typeof(string));

                for (int i = 0; i < objs.Length; i++)
                {
                    InvDoc doc = objs[i] as InvDoc;
                    dt.Rows.Add("", doc.DocName, doc.DocSize.ToString(), doc.UpUser, FormatHelper.ToDateString(doc.UpfileDate));

                }

            }
            return dt;
        }

        /// <summary>
        /// 获取所有待检状态的入库指令号
        /// </summary>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public string[] QueryASNNO()
        {
            InventoryFacade _inventoryFacade = new InventoryFacade();
            string[] str = _inventoryFacade.GetASNByStatus();
            return str;
        }

        /// <summary>
        /// 根据参数组绑定参数列表
        /// </summary>
        /// <param name="resultType"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public ComBoxValue[] QueryResult(string resultType)
        {
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);
            Dictionary<string, string> keyVals = new Dictionary<string, string>();
            List<ComBoxValue> combs = new List<ComBoxValue>();
            object[] obj = _InventoryFacade.GetDrpDesc(resultType);
            if (obj != null && obj.Length > 0)
            {
                foreach (Domain.BaseSetting.Parameter param in obj)
                {
                    if (!keyVals.ContainsKey(param.ParameterCode))
                        keyVals.Add(param.ParameterCode, param.ParameterDescription);
                }


                foreach (string key in keyVals.Keys)
                    combs.Add(new ComBoxValue { Text = keyVals[key], Value = key });


            }

            return combs.ToArray();
        }

        [WebMethod(EnableSession = true)]
        public string GetEmergency(string stno)
        {
            InventoryFacade _inventoryFacade = new InventoryFacade();
            return _inventoryFacade.GetFlagBYStno(stno);
        }

        [WebMethod(EnableSession = true)]
        public bool CheckASNReceiveStatus(string stno)
        {
            InventoryFacade _inventoryFacade = new InventoryFacade();
            ASN asn = (ASN)_inventoryFacade.GetASN(stno);
            if (asn != null)
                return asn.Status == "Receive";
            else
                return false;
        }


        [WebMethod(EnableSession = true)]
        public StNoLine[] QueryASNDetailSN(string sn, string stno)
        {
            List<StNoLine> ss = new List<StNoLine>();
            InventoryFacade _inventoryFacade = new InventoryFacade();

            object[] objs = _inventoryFacade.QueryASNDetailSNOrCode(sn, stno);

            if (objs != null && objs.Length > 0)
            {

                foreach (Asndetailsn d in objs)
                {
                    ss.Add(new StNoLine { StLine = d.Stline, StNo = d.Stno });
                }
            }
            return ss.ToArray();
        }




        /// <summary>
        /// 判断SN是否存在
        /// </summary>
        /// <param name="stno"></param>
        /// <param name="stline"></param>
        /// <param name="sn"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool QuerySN(string stno, string stline, string sn)
        {
            InventoryFacade _inventoryFacade = new InventoryFacade();
            object[] obj = _inventoryFacade.GetSNbySTNo(stno, stline, sn);
            if (obj != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 指令行关联箱号
        /// </summary>
        /// <param name="Stline"></param>
        /// <param name="Stno"></param>
        /// <param name="cartonno"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool BindCarton2STLine(string Stline, string Stno, string cartonno, out string message)
        {

            message = string.Empty;
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            WarehouseFacade facade = new WarehouseFacade(DataProvider);
            int result = _InventoryFacade.GetCartonNoByStnoAndCartonNo(Stno, cartonno);
            switch (result)
            {
                case 1:
                    {
                        message = "当前STNO 箱号重复";
                        return false;
                    }

                case 2:
                    {
                        message = "此箱号在其他的入库指令号中";
                        return false;
                    }

                case 3:
                    {

                        message = "此箱号在库存中";
                        return false;
                    }


            }

            //判断箱号有没有跟其他入库单的行关联
            //

            ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(Stline), Stno);


            if (!string.IsNullOrEmpty(asnDetail.CartonNo))
            {
                message = "该行数据已有关联箱号：";
                return false;

            }
            else
            {
                if (string.IsNullOrEmpty(cartonno))
                {
                    message = "箱号不能为空";
                    return false;

                }
                ASN asn = (ASN)_InventoryFacade.GetASN(Stno);
                int num = _InventoryFacade.GetASNDetailCountCartonNoNutNull(Stno);
                if (num <= asn.InitRejectQty)
                {

                    message = "已经到拒收箱数，不用关联箱号";
                    return false;

                }
                this.DataProvider.BeginTransaction();
                try
                {
                    asnDetail.CartonNo = cartonno.ToUpper();
                    _InventoryFacade.UpdateASNDetail(asnDetail);
                    object[] objs = facade.GetASNDetailSNbyStnoandStline((asnDetail as ASNDetail).StNo, (asnDetail as ASNDetail).StLine);
                    if (objs != null)
                    {
                        for (int i = 0; i < objs.Length; i++)
                        {
                            Asndetailsn a_sn = objs[i] as Asndetailsn;
                            a_sn.Cartonno = cartonno.ToUpper();
                            facade.UpdateAsndetailsn(a_sn);
                        }
                    }
                    this.DataProvider.CommitTransaction();
                }
                catch (Exception ex)
                {
                    this.DataProvider.RollbackTransaction();
                    message = "关联失败";
                    return false;

                }

                message = "关联箱号成功";
                return true;
            }

        }

        [WebMethod(EnableSession = true)]
        public string CancelCartonno(string stno, string[] stlines)
        {
            if (stlines == null || stlines.Length == 0)
                return "请选择数据行！";
            List<ASNDetail> l = new List<ASNDetail>();
            foreach (string s in stlines)
                l.Add(new ASNDetail { StNo = stno, StLine = s });
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            WarehouseFacade facade = new WarehouseFacade(DataProvider);


            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            string message = kit.ReceiveCancelCartonno(stno, l, facade, _InventoryFacade, DataProvider);
            return message;

        }


        [WebMethod(EnableSession = true)]
        public bool UploadFile(byte[] bytes, string asn, string type, string userName)
        {
            if (bytes.Length > 0)
            {
                MemoryStream ms = new MemoryStream(bytes);
                string root = HttpContext.Current.Server.MapPath("~/");

                string path = root + @"InvDoc\初检签收\";
                string stamp = DateTime.Now.ToString("yyyyMMddmmhhss");


                string fileName = "ASN" + asn + "_" + type + "_" + stamp;
                string fullPath = path + fileName;
                try
                {
                    Image img = Image.FromStream(ms);
                    img.Save(fullPath + ".jpg");
                }
                catch (ArgumentException ex)
                {
                    return false;
                }

                InvDoc doc = new InvDoc();
                doc.Dirname = "初检签收";
                doc.InvDocNo = asn;
                doc.InvDocType = type;
                doc.DocName = fileName;
                doc.DocType = "jpg";
                doc.DocSize = (bytes.Length / 1024);
                doc.UpUser = userName;
                doc.UpfileDate = FormatHelper.TODateInt(DateTime.Now);
                doc.ServerFileName = fileName + ".jpg";
                doc.MaintainUser = userName;
                doc.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                doc.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                InventoryFacade _inventoryFacade = new InventoryFacade();
                _inventoryFacade.AddInvDoc(doc);


                return true;
            }
            return false;

        }

        /// <summary>
        /// 统计当前入库指令号已经检验完成的行数
        /// </summary>
        /// <param name="stno"></param>
        /// <returns></returns>

        private int GetAsnDetailCountForRev(string stno)
        {
            InventoryFacade _inventoryFacade = new InventoryFacade(this.DataProvider);

            return _inventoryFacade.GetAsnDetailCountForRec(stno);
        }

        /// <summary>
        /// 更新指令行的拒收数量
        /// </summary>
        /// <param name="stno"></param>
        /// <param name="rejectQty"></param>
        /// <param name="rejectResult"></param>
        [WebMethod(EnableSession = true)]
        public void UpdateASN(string stno, int rejectQty, string rejectResult, DataTable dt)
        {

            try
            {
                InventoryFacade _inventoryFacade = new InventoryFacade(this.DataProvider);
                this.DataProvider.BeginTransaction();
                ASN asn = (ASN)_inventoryFacade.GetASN(stno);
                if (asn == null)
                    throw new Exception("asn不存在！");
                asn.InitRejectQty = rejectQty;
                asn.InitReceiveDesc = rejectResult;

                foreach (DataRow dr in dt.Rows)
                {
                    ASNDetail asnDetail = (ASNDetail)_inventoryFacade.GetASNDetail(int.Parse(dr["STLINE"].ToString()), dr["STNO"].ToString());
                    if (asnDetail == null)
                    {
                        throw new Exception("不存在ASN明细！");
                    }
                    if (asnDetail.Status != ASNDetail_STATUS.ASNDetail_ReceiveClose)
                    {
                        //更新asndetail STATUS 和 InitReceiveStatus
                        asnDetail.Status = ASNDetail_STATUS.ASNDetail_ReceiveClose;
                        asnDetail.InitReceiveStatus = SAP_LineStatus.SAP_LINE_REJECT;
                        asnDetail.InitReceiveDesc = rejectResult;
                        _inventoryFacade.UpdateASNDetail(asnDetail);
                    }
                }

                _inventoryFacade.UpdateASN(asn);
                this.DataProvider.CommitTransaction();
            }

            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();


                BenQGuru.eMES.Common.Log.Error(ex.Message + "-------------------" + ex.Source);
                BenQGuru.eMES.Common.Log.Error(ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="stno"></param>
        /// <param name="rejectQty"></param>
        /// <param name="rejectResult"></param>
        [WebMethod(EnableSession = true)]
        public string ReceiveDetail(DataTable dt, string stno, string rejectResult, string usr)
        {
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
            List<PoLog> logs = new List<PoLog>();
            if (dt.Rows.Count == 0)
                return "Grid中没有数据";

            stno = stno.Trim().ToUpper();

            List<Asndetail> asnds = new List<Asndetail>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()) && dt.Rows[i][0].ToString() != "√")
                    continue;

                string stLine = dt.Rows[i][2].ToString();
                asnds.Add(new Asndetail { Stline = stLine, Stno = stno });

            }



            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            try
            {
                return kit.Receive(stno,
                            asnds,
                            rejectResult,
                            usr,
                            Server.MapPath("~/InvDoc/" + "箱单导入/"),
                            _InventoryFacade,
                            _WarehouseFacade,
                            DataProvider);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        private void LogPO2Sap(List<BenQGuru.eMES.SAPRFCService.Domain.PO> dns)
        {
            DBDateTime dbTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            InventoryFacade _InventoryFacade = new InventoryFacade(this.DataProvider);
            foreach (BenQGuru.eMES.SAPRFCService.Domain.PO po in dns)
            {

                Po2Sap poLog = new Po2Sap();
                poLog.PONO = po.PONO;
                poLog.POLine = po.POLine;
                poLog.SerialNO = po.SerialNO;
                poLog.Qty = po.Qty; // 
                poLog.Unit = po.Unit;
                poLog.FacCode = po.FacCode;
                poLog.InvoiceDate = po.InvoiceDate; //  
                poLog.MCode = po.MCode;//SAPMcode
                poLog.SAPMaterialInvoice = po.SAPMaterialInvoice;
                poLog.Operator = po.Operator;
                poLog.Status = po.Status;
                poLog.VendorInvoice = po.VendorInvoice;
                poLog.StorageCode = po.StorageCode;
                poLog.Remark = po.Remark;
                poLog.SapDateStamp = dbTime.DBDate;
                poLog.SapTimeStamp = dbTime.DBTime;
                //poLog.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                //poLog.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                //poLog.MaintainUser = GetUserCode();
                //poLog.r = "empty";
                //poLog.Message = "empty";
                _InventoryFacade.AddPo2Sap(poLog);
            }
        }


    

        /// <summary>
        /// 让步接收
        /// </summary>
        /// <param name="stno"></param>
        /// <param name="rejectQty"></param>
        /// <param name="rejectResult"></param>
        [WebMethod(EnableSession = true)]
        public string GiveinDetail(DataTable dt, string stno, string giveinResult)
        {


            InventoryFacade _InventoryFacade = new InventoryFacade(this.DataProvider);


            WarehouseFacade _WarehouseFacade = new WarehouseFacade(this.DataProvider);

            if (string.IsNullOrEmpty(giveinResult))
                return "让步接收时必须填写原因";


            if (dt.Rows.Count == 0)
                return stno + "无数据";


            bool isGivein = false;
            ASNDetail asnDetail = null;

            try
            {

                ASN asn = (ASN)_InventoryFacade.GetASN(stno.Trim().ToUpper());


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()) && dt.Rows[i][0].ToString() != "√")
                        continue;

                    string stLine = dt.Rows[i][2].ToString();

                    ASNDetail asnDetail1 = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(stLine), stno);


                    if (string.IsNullOrEmpty(asnDetail1.CartonNo))
                        return "让步接收必须填写箱号！";

                    if (!_WarehouseFacade.CheckAlterIncludeEQ(asn.InvNo, asnDetail1.DQMCode))
                        return asn.InvNo + ":" + ":" + asnDetail.DQMCode + "数量已超出SAP计划数量！";

                }
                this.DataProvider.BeginTransaction();
                //算出剩余要接收的数量（入库指令行记录数减去拒收数量）

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][0].ToString()) && dt.Rows[i][0].ToString() != "√")
                        continue;

                    string stLine = dt.Rows[i][2].ToString();
                    asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(stLine), stno);

                    asn = (ASN)_InventoryFacade.GetASN(stno);

                    if (asnDetail.Status != ASNDetail_STATUS.ASNDetail_ReceiveClose)
                    {


                        asnDetail.InitReceiveStatus = SAP_LineStatus.SAP_LINE_GIVEIN;
                        asnDetail.ReceiveQty = asnDetail.Qty;
                        asnDetail.Status = ASNDetail_STATUS.ASNDetail_ReceiveClose;
                        asnDetail.InitGIVEINDESC = giveinResult;


                        _InventoryFacade.UpdateASNDetail(asnDetail);

                        //接收数量(TBLASNDETAILITEM.ReceiveQTY)更新为：等于需求数量(TBLASNDETAILITEM.QTY)
                        _InventoryFacade.UpdateASNItem(stno, stLine);

                        asn.InitGiveInQty += 1;
                        _InventoryFacade.UpdateASN(asn);
                        isGivein = true;
                    }


                }
                this.DataProvider.CommitTransaction();
                if (isGivein)
                {
                    return "让步接收成功！";
                }
                return "让步接收失败没有条目需要让步接收！";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ;
                throw ex;

            }

        }




        /// <summary>
        /// 拒收
        /// </summary>
        /// <param name="stno"></param>
        /// <param name="rejectQty"></param>
        /// <param name="rejectResult"></param>
        [WebMethod(EnableSession = true)]
        public string RejectDetail(DataTable dt, string stno, string rejectResult, string rejectCount)
        {
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);

            if (dt.Rows.Count == 0)
                return stno + "无数据！";
            if (string.IsNullOrEmpty(rejectResult))
                return "拒收时必须填写拒收数量和选择拒收原因！";

            int rejectNum = 0;
            if (!int.TryParse(rejectCount, out rejectNum))
                return "拒收数量必须是数字！";

            ASN asn = (ASN)_InventoryFacade.GetASN(stno);

            if (asn == null)
                return stno + "入库指令号不存在！";

            if (asn.StType == InInvType.PGIR)
            {
                if (rejectNum != dt.Rows.Count)
                {
                    return "生产退料只能全部拒收！";

                }
            }



            this.DataProvider.BeginTransaction();
            try
            {

                //统计已经初检完成的行数
                int count = 0;
                ASNDetail asnDetail = null;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string stLine = dt.Rows[i][2].ToString();

                    asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(stLine), stno);
                    if (asnDetail.Status == ASNDetail_STATUS.ASNDetail_ReceiveClose)
                    {
                        count++;
                    }
                }

                //如果拒收数量等于剩余待检行数量，表示剩余全部拒收
                if (rejectNum == dt.Rows.Count - count)
                {
                    string message = string.Empty;
                    if (!AllReject(dt, stno, rejectResult, out message))
                    {
                        return message;
                    }
                }
                else if (rejectNum < dt.Rows.Count - count)//否则部分拒收
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string stLine = dt.Rows[i][2].ToString();
                        asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(stLine), stno);

                        asnDetail.InitReceiveDesc = rejectResult;
                        _InventoryFacade.UpdateASNDetail(asnDetail);
                    }


                    //更新主表初检拒收数量和拒收描述

                    asn.InitRejectQty = rejectNum;
                    asn.InitReceiveDesc = rejectResult;
                    _InventoryFacade.UpdateASN(asn);
                }
                else
                {
                    return "拒收数量不能大于剩余待检行记录数";

                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

            return "拒收成功！";

        }





        protected bool AllReject(DataTable dt, string stno, string rejectReason, out string message)
        {
            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
            InventoryFacade _InventoryFacade = new InventoryFacade(DataProvider);
            message = string.Empty;
            ASNDetail asnDetail = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string stLine = dt.Rows[i][2].ToString();
                asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(stLine), stno);
                //检查剩余待检的是否已全部勾选
                if (asnDetail.Status != ASNDetail_STATUS.ASNDetail_ReceiveClose)
                {

                    if (!string.IsNullOrEmpty(asnDetail.CartonNo))
                    {
                        message = "已有行数据关联箱号,不能拒收";
                        return false;
                    }
                }

            }

            ASN asn = (ASN)_InventoryFacade.GetASN(stno.Trim().ToUpper());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string stLine = dt.Rows[i][2].ToString();
                asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(int.Parse(stLine), stno);
                if (asnDetail.Status != ASNDetail_STATUS.ASNDetail_ReceiveClose)
                {
                    //更新asndetail STATUS 和 InitReceiveStatus

                    asnDetail.InitReceiveStatus = SAP_LineStatus.SAP_LINE_REJECT;

                    asnDetail.InitReceiveDesc = rejectReason;
                    _InventoryFacade.UpdateASNDetail(asnDetail);

                    object[] objs_item = _WarehouseFacade.GetASNDetailItembyStnoAndStline(asnDetail.StNo, asnDetail.StLine);
                    if (objs_item != null)
                    {
                        foreach (Asndetailitem item in objs_item)
                        {
                            item.ReceiveQty = 0;
                            item.QcpassQty = 0;
                            item.ActQty = 0;
                            _WarehouseFacade.UpdateAsndetailitem(item);
                        }
                    }

                }

            }


            asn.Status = ASN_STATUS.ASN_ReceiveRejection;
            asn.InitRejectQty = dt.Rows.Count;
            asn.InitReceiveDesc = rejectReason;
            _InventoryFacade.UpdateASN(asn);
            message = "拒收成功！";

            return true;


        }

        [WebMethod(EnableSession = true)]
        public int[] GetASN(string stno)
        {
            InventoryFacade _Invenfacade = null;
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(DataProvider);
            }
            ASN asn = (ASN)_Invenfacade.GetASN(stno);
            int[] str = new int[3];
            if (asn != null)
            {
                str[0] = asn.InitRejectQty;
                str[1] = asn.InitReceiveQty;
                str[2] = asn.InitGiveInQty;
                return str;
            }
            return null;
        }

        [WebMethod(EnableSession = true)]
        public bool ASNReject(string stno)
        {
            InventoryFacade _Invenfacade = null;
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(DataProvider);
            }
            ASN asn = (ASN)_Invenfacade.GetASN(stno);
            if (asn.Status == ASN_STATUS.ASN_ReceiveRejection)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 图片删除
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public void DeleteDoc(string[] fileNames)
        {
            InventoryFacade _Invenfacade = null;
            if (_Invenfacade == null)
            {
                _Invenfacade = new InventoryFacade(DataProvider);
            }

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (string fileName in fileNames)
                {
                    _Invenfacade.DeleteDoc(fileName);

                }

                this.DataProvider.CommitTransaction();

            }
            catch (Exception EX)
            {

                this.DataProvider.RollbackTransaction();
                throw EX;

            }
        }



        private string Getstring(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                return message;
            }
            return "";
        }
    }



    public class AsnSimple
    {
        public int RejectCount { get; set; }
        public string RejectReason { get; set; }
        public string GiveReason { get; set; }

    }
    public class StNoLine
    {
        public string StNo { get; set; }
        public string StLine { get; set; }

    }

    public class ComBoxValue
    {
        public string Text { get; set; }
        public string Value { get; set; }

    }
}
        
