using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.IQC;
using BenQGuru.eMES.OQC;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using BenQGuru.eMES.BaseSetting;

namespace BenQGuru.eMES.Web.WebService
{
    /// <summary>
    /// PackagingOperationsService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class PackagingOperationsService : System.Web.Services.WebService
    {
        public PackagingOperationsService()
        {
            InitDbItems();
        }

        private string m_DbName;
        private IDomainDataProvider _domainDataProvider;

        InventoryFacade _InventoryFacade = null;
        WarehouseFacade _WarehouseFacade = null;
        IQCFacade _IQCFacade = null;
        OQCFacade _OQCFacade = null;

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

        #region
        [WebMethod(EnableSession = true)]
        public int[] GetDBDateTime()
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this._domainDataProvider);
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;
            int[] array = new int[] { mDate, mTime };
            return array;
        }

        [WebMethod(EnableSession = true)]
        public void BeginTransaction()
        {
            this.DataProvider.BeginTransaction();
        }

        [WebMethod(EnableSession = true)]
        public void CommitTransaction()
        {
            this.DataProvider.CommitTransaction();
        }

        [WebMethod(EnableSession = true)]
        public void RollbackTransaction()
        {
            this.DataProvider.RollbackTransaction();
        }

        [WebMethod(EnableSession = true)]
        public object GetCartonInvoices(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.GetCartonInvoices(pickNo);
        }

        [WebMethod(EnableSession = true)]
        public object QueryPickDetailMaterial(string pickNo, string cartonNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
        }

        [WebMethod(EnableSession = true)]
        public decimal QueryPickDetailMaterial_PQty(string pickNo, string cartonNo)
        {
            object obj = this.QueryPickDetailMaterial(pickNo, cartonNo);
            return (obj as Pickdetailmaterial).PQty;
        }

        //1>	新增发货箱单明细信息表(TBLCartonInvDetail)数据
        [WebMethod(EnableSession = true)]
        public void AddCartonInvDetail(string carInvNo, string pickNo, string status, string cartonNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            CartonInvDetail cartonInvDetail = new CartonInvDetail();
            cartonInvDetail.CARINVNO = carInvNo;
            cartonInvDetail.PICKNO = pickNo;
            cartonInvDetail.STATUS = status;
            cartonInvDetail.CARTONNO = cartonNo;
            //cartonInvDetail.PACKMCODE = "";
            //cartonInvDetail.PACKQTY = 0;
            cartonInvDetail.CUSER = userCode;
            cartonInvDetail.CDATE = this.GetDBDateTime()[0];
            cartonInvDetail.CTIME = this.GetDBDateTime()[1];
            cartonInvDetail.MDATE = this.GetDBDateTime()[0];
            cartonInvDetail.MTIME = this.GetDBDateTime()[1];
            cartonInvDetail.MUSER = userCode;
            this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);
        }

        //2>	新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
        [WebMethod(EnableSession = true)]
        public void AddCartonInvDetailMaterial(string pickNo, string cartonNo, string carInvNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this.QueryPickDetailMaterial(pickNo, cartonNo);
            Pickdetailmaterial pickdetailmaterial = obj as Pickdetailmaterial;
            CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
            cartonInvDetailMaterial.CARINVNO = carInvNo;
            cartonInvDetailMaterial.PICKNO = pickNo;
            cartonInvDetailMaterial.PICKLINE = pickdetailmaterial.Pickline;
            cartonInvDetailMaterial.CARTONNO = cartonNo;
            cartonInvDetailMaterial.MCODE = pickdetailmaterial.MCode;
            cartonInvDetailMaterial.DQMCODE = pickdetailmaterial.DqmCode;
            cartonInvDetailMaterial.QTY = pickdetailmaterial.Qty;
            cartonInvDetailMaterial.UNIT = pickdetailmaterial.Unit;
            cartonInvDetailMaterial.GFHWITEMCODE = string.Empty;
            cartonInvDetailMaterial.GFPACKINGSEQ = string.Empty;
            cartonInvDetailMaterial.CUSER = userCode;
            cartonInvDetailMaterial.CDATE = this.GetDBDateTime()[0];
            cartonInvDetailMaterial.CTIME = this.GetDBDateTime()[1];
            cartonInvDetailMaterial.MDATE = this.GetDBDateTime()[0];
            cartonInvDetailMaterial.MTIME = this.GetDBDateTime()[1];
            cartonInvDetailMaterial.MUSER = userCode;
            this._WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
        }

        //3>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
        [WebMethod(EnableSession = true)]
        public void AddCARTONINVDETAILSN(string pickNo, string cartonNo, string carInvNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object[] objs = this.QueryPickDetailMaterialSN(pickNo, cartonNo);
            CARTONINVDETAILSN _CARTONINVDETAILSN = new CARTONINVDETAILSN();
            foreach (Pickdetailmaterialsn pickdetailmaterialsn in objs)
            {
                _CARTONINVDETAILSN.CARINVNO = carInvNo;
                _CARTONINVDETAILSN.PICKNO = pickNo;
                _CARTONINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                _CARTONINVDETAILSN.CARTONNO = cartonNo;
                _CARTONINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                _CARTONINVDETAILSN.MUSER = userCode;
                _CARTONINVDETAILSN.MDATE = this.GetDBDateTime()[0];
                _CARTONINVDETAILSN.MTIME = this.GetDBDateTime()[1];
                this._WarehouseFacade.AddCARTONINVDETAILSN(_CARTONINVDETAILSN);
            }
        }

        //4>	更新拣拣货任务令头表(TBLPICK)数据
        [WebMethod(EnableSession = true)]
        public void UpdatePick(string pickNo, string status, string userCode)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            object objPick = this._InventoryFacade.GetPick(pickNo);
            Pick pick = objPick as Pick;
            pick.Status = status;
            pick.MaintainUser = userCode;
            pick.MaintainDate = this.GetDBDateTime()[0];
            pick.MaintainTime = this.GetDBDateTime()[1];
            this._InventoryFacade.UpdatePick(pick);
        }

        //5>	更新拣货任务令明细表(TBLPICKDETAIL)数据
        [WebMethod(EnableSession = true)]
        public void UpdatePickdetail(string pickNo, string cartonNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            decimal sum = 0;
            object obj = this.QueryPickDetailMaterial(pickNo, cartonNo);
            object pickdetail_obj = this._WarehouseFacade.GetPickdetail(pickNo, (obj as Pickdetailmaterial).Pickline);
            object[] objs = this.GetAllPickDetailMaterialByPickNoAndLine(pickNo, cartonNo);
            foreach (Pickdetailmaterial pickdetailmaterial in objs)
            {
                sum += pickdetailmaterial.PQty;
            }
            PickDetail pickdetail = pickdetail_obj as PickDetail;
            pickdetail.PQTY = sum;
            if (pickdetail.SQTY == pickdetail.PQTY)
            {
                pickdetail.Status = PickDetail_STATUS.Status_ClosePack;
            }
            else
            {
                pickdetail.Status = PickDetail_STATUS.Status_Pack;
            }
            pickdetail.MaintainUser = userCode;
            pickdetail.MaintainDate = this.GetDBDateTime()[0];
            pickdetail.MaintainTime = this.GetDBDateTime()[1];
            this._WarehouseFacade.UpdatePickdetail(pickdetail);
        }

        //6>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
        [WebMethod(EnableSession = true)]
        public void UpdatePickdetailmaterial(string pickNo, string cartonNo, decimal pQtyAddValue, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this.QueryPickDetailMaterial(pickNo, cartonNo);
            Pickdetailmaterial pickdetailmaterial = obj as Pickdetailmaterial;
            if (pQtyAddValue == -1)
            {
                pickdetailmaterial.PQty = pickdetailmaterial.Qty;
            }
            else
            {
                pickdetailmaterial.PQty += pQtyAddValue;
            }
            pickdetailmaterial.MaintainUser = userCode;
            pickdetailmaterial.MaintainDate = this.GetDBDateTime()[0];
            pickdetailmaterial.MaintainTime = this.GetDBDateTime()[1];
            this._WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);
        }

        [WebMethod(EnableSession = true)]
        public decimal UpdatePickdetailmaterialAndGetQty(string pickNo, string dqMaterialNO, decimal qty, string userCode)
        {
            object[] objPickdetailmaterials = this._WarehouseFacade.QueryPICKDetailMaterials(pickNo, dqMaterialNO);
            foreach (Pickdetailmaterial pickdetailmaterial in objPickdetailmaterials)
            {
                decimal num = pickdetailmaterial.Qty - pickdetailmaterial.PQty;
                if (num > 0)
                {
                    if (qty > num)
                    {
                        pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        pickdetailmaterial.MaintainUser = userCode;
                        pickdetailmaterial.MaintainDate = this.GetDBDateTime()[0];
                        pickdetailmaterial.MaintainTime = this.GetDBDateTime()[1];
                        this._WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);

                        qty -= num;

                        continue;
                    }
                    if (qty <= num)
                    {
                        pickdetailmaterial.PQty += qty;
                        pickdetailmaterial.MaintainUser = userCode;
                        pickdetailmaterial.MaintainDate = this.GetDBDateTime()[0];
                        pickdetailmaterial.MaintainTime = this.GetDBDateTime()[1];
                        this._WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);

                        break;
                    }
                }
            }

            return qty;
        }

        [WebMethod(EnableSession = true)]
        public object GetCartonInvDetail(string carInvNo, string cartonNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
        }

        //7>	当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
        [WebMethod(EnableSession = true)]
        public void UpdateCartonInvDetail(string carInvNo, string cartonNo, string status, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this.GetCartonInvDetail(carInvNo, cartonNo);
            CartonInvDetail cartonInvDetail = obj as CartonInvDetail;
            cartonInvDetail.STATUS = status;
            cartonInvDetail.MUSER = userCode;
            cartonInvDetail.MDATE = this.GetDBDateTime()[0];
            cartonInvDetail.MTIME = this.GetDBDateTime()[1];
            this._WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);
        }

        [WebMethod(EnableSession = true)]
        public object[] QueryPickDetails(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryPickDetail(pickNo);
        }

        [WebMethod(EnableSession = true)]
        public object GetPickDetail(string pickNo, string cartonNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this.QueryPickDetailMaterial(pickNo, cartonNo);
            return this._WarehouseFacade.GetPickdetail(pickNo, (obj as Pickdetailmaterial).Pickline);
        }

        [WebMethod(EnableSession = true)]
        public void UpdatePickDetails(string pickNo, string status, string userCode)
        {
            object[] objs = this._WarehouseFacade.QueryPickDetail(pickNo);
            foreach (PickDetail pickDetail in objs)
            {
                pickDetail.Status = status;
                pickDetail.MaintainUser = userCode;
                pickDetail.MaintainDate = this.GetDBDateTime()[0];
                pickDetail.MaintainTime = this.GetDBDateTime()[1];
                _WarehouseFacade.UpdatePickdetail(pickDetail);
            }
        }

        //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
        [WebMethod(EnableSession = true)]
        public void UpdateCartonInvoices(string pickNo, string carInvNo, string status, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object[] objs = this._WarehouseFacade.QueryPickDetail(pickNo);
            bool isTrue = true;
            foreach (PickDetail pickDetail in objs)
            {
                if (pickDetail.SQTY != pickDetail.PQTY)
                {
                    isTrue = false;
                    break;
                }
            }
            if (isTrue)
            {
                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES != null)
                {
                    CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                    _CARTONINVOICES.STATUS = status;
                    _CARTONINVOICES.FDATE = this.GetDBDateTime()[0];
                    _CARTONINVOICES.FTIME = this.GetDBDateTime()[1];
                    _CARTONINVOICES.MUSER = userCode;
                    _CARTONINVOICES.MDATE = this.GetDBDateTime()[0];
                    _CARTONINVOICES.MTIME = this.GetDBDateTime()[1];
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public void UpdateTBLCartonInvoices(string carInvNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
            CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
            if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
            {
                _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                _CARTONINVOICES.MUSER = userCode;
                _CARTONINVOICES.MDATE = this.GetDBDateTime()[0];
                _CARTONINVOICES.MTIME = this.GetDBDateTime()[1];
                this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
            }
        }

        [WebMethod(EnableSession = true)]
        public object GetTBLCartonInvoices(string carInvNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
        }

        [WebMethod(EnableSession = true)]
        public object GetPickDetailMaterialSN(string pickNo, string sn)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.GetPickdetailmaterialsn(pickNo, sn);
        }

        [WebMethod(EnableSession = true)]
        public object[] QueryPickDetailMaterialSN(string pickNo, string cartonNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.GetPickDetailMaterialSN(pickNo, cartonNo);
        }

        [WebMethod(EnableSession = true)]
        public object GetCartonInvDetailSN(string carInvNo, string sn)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryCARTONINVDETAILSN(carInvNo, sn);
        }

        //4>	检查包装箱号、鼎桥物料编码与刷入SN条码对应鼎桥物料编码相同的记录是否存在当前拣货任务令号对应发货箱单的发货箱单明细物料信息表(TBLCartonInvDetailMaterial)中

        //SN + [PICKNO]
        //->(已拣物料明细表SN信息)TBLPICKDetailMaterialSN.CARTONNO(原箱号) + [PICKNO]
        //->(已拣物料明细表)TBLPICKDetailMaterial.MCODE + CARTONNO(要包入的箱号) + [CARINVNO]
        //->(发货箱单明细物料信息)TBLCartonInvDetailMaterial
        [WebMethod(EnableSession = true)]
        public void OperateCartonInvDetailMaterial(string pickNo, string sn, string carInvNo, string cartonNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            Pickdetailmaterialsn pickdetailmaterialsn = this.GetPickDetailMaterialSN(pickNo, sn) as Pickdetailmaterialsn;
            object objPickdetailmaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, pickdetailmaterialsn.Cartonno);
            if (objPickdetailmaterial == null)
            {
                return;
            }
            Pickdetailmaterial pickdetailmaterial = objPickdetailmaterial as Pickdetailmaterial;
            object objPickdetail = this._WarehouseFacade.GetPickdetail(pickNo, pickdetailmaterial.Pickline);
            if (objPickdetail == null)
            {
                return;
            }
            PickDetail pickDetail = objPickdetail as PickDetail;
            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, pickdetailmaterial.MCode);
            if (objCartonInvDetailMaterial != null)
            {
                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                cartonInvDetailMaterial.QTY += 1;
                cartonInvDetailMaterial.MUSER = userCode;
                cartonInvDetailMaterial.MDATE = this.GetDBDateTime()[0];
                cartonInvDetailMaterial.MTIME = this.GetDBDateTime()[1];
                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
            }
            else
            {
                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                cartonInvDetailMaterial.CARINVNO = carInvNo;
                cartonInvDetailMaterial.PICKNO = pickNo;
                cartonInvDetailMaterial.PICKLINE = pickdetailmaterial.Pickline;
                cartonInvDetailMaterial.CARTONNO = cartonNo;
                cartonInvDetailMaterial.MCODE = pickdetailmaterial.MCode;
                cartonInvDetailMaterial.DQMCODE = pickdetailmaterial.DqmCode;
                cartonInvDetailMaterial.QTY = 1;
                cartonInvDetailMaterial.UNIT = pickdetailmaterial.Unit;
                cartonInvDetailMaterial.GFHWITEMCODE = pickDetail.GFHWItemCode;
                cartonInvDetailMaterial.GFPACKINGSEQ = pickDetail.GFPackingSeq;
                cartonInvDetailMaterial.CUSER = userCode;
                cartonInvDetailMaterial.CDATE = this.GetDBDateTime()[0];
                cartonInvDetailMaterial.CTIME = this.GetDBDateTime()[1];
                cartonInvDetailMaterial.MDATE = this.GetDBDateTime()[0];
                cartonInvDetailMaterial.MTIME = this.GetDBDateTime()[1];
                cartonInvDetailMaterial.MUSER = userCode;
                this._WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
            }

        }

        [WebMethod(EnableSession = true)]
        public void _OperateCartonInvDetailMaterial(string pickNo, string carInvNo, string cartonNo, decimal qty, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            object objPickdetailmaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
            if (objPickdetailmaterial == null)
            {
                return;
            }
            Pickdetailmaterial pickdetailmaterial = objPickdetailmaterial as Pickdetailmaterial;
            object objPickdetail = this._WarehouseFacade.GetPickdetail(pickNo, pickdetailmaterial.Pickline);
            if (objPickdetail != null)
            {
                return;
            }
            PickDetail pickDetail = objPickdetail as PickDetail;
            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, pickdetailmaterial.MCode);
            if (objCartonInvDetailMaterial != null)
            {
                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                cartonInvDetailMaterial.QTY += qty;
                cartonInvDetailMaterial.MUSER = userCode;
                cartonInvDetailMaterial.MDATE = this.GetDBDateTime()[0];
                cartonInvDetailMaterial.MTIME = this.GetDBDateTime()[1];
                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
            }
            else
            {
                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                cartonInvDetailMaterial.CARINVNO = carInvNo;
                cartonInvDetailMaterial.PICKNO = pickNo;
                cartonInvDetailMaterial.PICKLINE = pickdetailmaterial.Pickline;
                cartonInvDetailMaterial.CARTONNO = cartonNo;
                cartonInvDetailMaterial.MCODE = pickdetailmaterial.MCode;
                cartonInvDetailMaterial.DQMCODE = pickdetailmaterial.DqmCode;
                cartonInvDetailMaterial.QTY = qty;
                cartonInvDetailMaterial.UNIT = pickdetailmaterial.Unit;
                cartonInvDetailMaterial.GFHWITEMCODE = pickDetail.GFHWItemCode;
                cartonInvDetailMaterial.GFPACKINGSEQ = pickDetail.GFPackingSeq;
                cartonInvDetailMaterial.CUSER = userCode;
                cartonInvDetailMaterial.CDATE = this.GetDBDateTime()[0];
                cartonInvDetailMaterial.CTIME = this.GetDBDateTime()[1];
                cartonInvDetailMaterial.MDATE = this.GetDBDateTime()[0];
                cartonInvDetailMaterial.MTIME = this.GetDBDateTime()[1];
                cartonInvDetailMaterial.MUSER = userCode;
                this._WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
            }
        }

        //1)	检查包装箱号在已拣物料明细表(TBLPICKDetailMaterial)对应拣货任务令明细表(TBLPICKDETAIL)中的光伏华为编码(TBLPICKDETAIL.GFHWITEMCODE)、光伏包装序号(TBLPICKDETAIL.GFPACKINGSEQ)与编辑区域中选择的光伏华为编码、光伏包装序号是否同等，如不相等则报错提示该箱号对应选择的光伏华为编码、光伏包装序号不相等
        [WebMethod(EnableSession = true)]
        public bool IsEqual(string pickNo, string cartonNo, string gfHWItemCode, string gfPackingSEQ)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this.QueryPickDetailMaterial(pickNo, cartonNo);
            if (obj != null)
            {
                object _obj = this._WarehouseFacade.GetPickdetail(pickNo, (obj as Pickdetailmaterial).Pickline);
                if (_obj != null)
                {
                    PickDetail pickDetail = _obj as PickDetail;
                    if (pickDetail.GFHWItemCode.Equals(gfHWItemCode) && pickDetail.GFPackingSeq.Equals(gfPackingSEQ))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //3.	检查通过SN条码在已拣物料明细表SN信息表(TBLPICKDetailMaterialSN)中对应的箱号(TBLPICKDetailMaterialSN.CARTONNO)在已拣物料明细表(TBLPICKDetailMaterial)对应拣货任务令明细表(TBLPICKDETAIL)中的光伏华为编码(TBLPICKDETAIL.GFHWITEMCODE)、光伏包装序号(TBLPICKDETAIL.GFPACKINGSEQ)与编辑区域中选择的光伏华为编码、光伏包装序号是否同等，如不相等则报错提示该箱号对应选择的光伏华为编码、光伏包装序号不相等
        [WebMethod(EnableSession = true)]
        public bool _IsEqual(string pickNo, string sn, string gfHWItemCode, string gfPackingSEQ)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this.GetPickDetailMaterialSN(pickNo, sn);
            if (obj != null)
            {
                object _obj = this.QueryPickDetailMaterial(pickNo, (obj as Pickdetailmaterialsn).Cartonno);
                if (_obj != null)
                {
                    object oBJ = this._WarehouseFacade.GetPickdetail(pickNo, (_obj as Pickdetailmaterial).Pickline);
                    if (oBJ != null)
                    {
                        PickDetail pickDetail = oBJ as PickDetail;
                        if (pickDetail.GFHWItemCode.Equals(gfHWItemCode) && pickDetail.GFPackingSeq.Equals(gfPackingSEQ))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        [WebMethod(EnableSession = true)]
        public int GetCartonNoCount(string pickNo, string gfHWItemCode, string gfPackingSEQ)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.GetCartonNoCount(pickNo, gfHWItemCode, gfPackingSEQ);
        }

        [WebMethod(EnableSession = true)]
        public object[] GetMaterialInfoByQDMCode(string dqMCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
        }

        [WebMethod(EnableSession = true)]
        public bool IsKeypartsControlType(string dqMCode)
        {
            object[] mar_objs = this.GetMaterialInfoByQDMCode(dqMCode);
            Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
            if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
            {
                return true;
            }

            return false;
        }

        [WebMethod(EnableSession = true)]
        public object[] GetAllPickDetailMaterialByPickNoAndLine(string pickNo, string cartonNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this.QueryPickDetailMaterial(pickNo, cartonNo);
            return this._WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, (obj as Pickdetailmaterial).Pickline);
        }
        #endregion

        //获取所有拣货任务令号
        [WebMethod(EnableSession = true)]
        public string[] QueryPickNO(string gfFlag, string user)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }

            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(user);

            return this._InventoryFacade.QueryPickNO(gfFlag, usergroupList);
        }


        [WebMethod(EnableSession = true)]
        public string[] QueryPickNONotY(string user)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(user);


            return this._InventoryFacade.QueryPickNONotN(usergroupList);
        }

        //获取所有鼎桥物料编码
        [WebMethod(EnableSession = true)]
        public string[] QueryDQMaterialNO(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryDQMaterialNO(pickNo);
        }

        //获取所有鼎桥物料编码 add by sam
        [WebMethod(EnableSession = true)]
        public string[] QueryGFDqsMcode(string pickNo, string dqmcode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryGFDqsMcodeByPickNo(pickNo, dqmcode);
        }

        //已包装箱数：通过拣货任务令号获取对应发货箱单号(TBLCartonInvoices .CARINVNO)，再通过发货箱单号获取发货箱单下发货箱号条码(CARTONNO)状态为：ClosePack:包装完成的箱数COUNT(TBLCartonInvDetail. CARTONNO)
        //发货箱单号：通过拣货任务令号获取对应发货箱单号(TBLCartonInvoices .CARINVNO)
        [WebMethod(EnableSession = true)]
        public string GetString(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            string carInvNo = this.QueryCarInvNo(pickNo);
            int count = this._WarehouseFacade.GetCartonCount(carInvNo, "ClosePack");
            return count.ToString() + "," + carInvNo;
        }

        [WebMethod(EnableSession = true)]
        public string QueryCarInvNo(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this._WarehouseFacade.GetCartonInvoices(pickNo);
            if (obj == null)
            {
                return string.Empty;
            }
            return (obj as CARTONINVOICES).CARINVNO;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetDataGrid(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.QueryPDAPackagingOperations(pickNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("鼎桥编码", typeof(string));
                dt.Columns.Add("华为编码", typeof(string));
                dt.Columns.Add("包装数量", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add("", row["DQMCODE"], row["CUSTMCODE"], row["QTY"]);
                }
            }
            return dt;
        }

        #region 包装作业

        [WebMethod(EnableSession = true)]
        public DataTable GetDataGrid1(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.QueryPDAPickMaterial(pickNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("PickLine", typeof(string));
                dt.Columns.Add("鼎桥编码", typeof(string));
                dt.Columns.Add("华为编码", typeof(string));
                dt.Columns.Add("包装数", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add("", row["PICKLINE"], row["DQMCODE"], row["CUSTMCODE"], row["QTY"]);
                }
            }
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetDataGrid2(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.QueryPDAPackingDetail(pickNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("鼎桥编码", typeof(string));
                dt.Columns.Add("华为编码", typeof(string));
                dt.Columns.Add("拣货数量", typeof(string));
                dt.Columns.Add("已包数量", typeof(string));
                dt.Columns.Add("包装箱号", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add(row["DQMCODE"], row["CUSTMCODE"], row["SQTY"], row["QTY"], row["CARTONNO"]);
                }
            }
            return dt;
        }

        //包装箱号回车
        [WebMethod(EnableSession = true)]
        public bool CartonNOKeyPressReturnMessage(string pickNo, string dqMCode, string cartonNo, string userCode, out string message)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            message = string.Empty;
            string carInvNo = this.QueryCarInvNo(pickNo);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            try
            {
                this.DataProvider.BeginTransaction();
                //更新状态
                object objPick = this._InventoryFacade.GetPick(pickNo);
                if (objPick != null)
                {
                    Pick pick = objPick as Pick;
                    pick.Status = "Pack";
                    pick.MaintainUser = mUser;
                    pick.MaintainDate = mDate;
                    pick.MaintainTime = mTime;
                    this._InventoryFacade.UpdatePick(pick);


                    if (_WarehouseFacade.GetPackCount(pickNo, "PACK") == 0)
                    {
                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = " ";
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                        trans.InvType = (pick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = userCode;
                        trans.MCode = " ";
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);

                    }

                }

                object objPICKDetailMaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
                //捡料表中是否存在箱号？
                if (objPICKDetailMaterial == null)
                {
                    #region 非整箱包货
                    //检查箱号，鼎桥物料号，是够存在
                    if (string.IsNullOrEmpty(dqMCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "必须选择鼎桥物料号";
                        return false;
                    }

                    //不存在，要通过鼎桥物料号判断是取SN还是数量
                    //1，根据鼎桥物料号，先判断是单件管控还是非单件管控
                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "无效的鼎桥物料号";
                        return false;
                    }
                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "此物料为单件管控，请输入SN号码按回车";
                        return false;
                    }
                    else // 非单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "此物料为非单件管控，请输入数量按回车";
                        return false;
                    }
                    #endregion
                }
                else
                {
                    #region 整箱包货
                    //如果箱号是有
                    //1, cartonNo 在tblpickdetailmaterial中 以pickno为条件，查
                    //如果有，就将tblpickdetailmaterial where carton=‘cartonNo’ and pick=？ 搬到tblcartoninvoicesmaterial中
                    //如果这个表tblpickdetailmaterialsn中有数据，将这些数据搬到tblcartoninvoicesmaterialsn中
                    Pickdetailmaterial pickdetailmaterial = objPICKDetailMaterial as Pickdetailmaterial;
                    if (pickdetailmaterial.PQty != 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "该箱号已部分包装,不能再使用原箱号码";
                        return false;
                    }
                    else
                    {
                        //1>	新增发货箱单明细信息表(TBLCartonInvDetail)数据
                        CartonInvDetail cartonInvDetail = new CartonInvDetail();
                        cartonInvDetail.CARINVNO = carInvNo;
                        cartonInvDetail.PICKNO = pickNo;
                        cartonInvDetail.STATUS = "ClosePack";
                        cartonInvDetail.CARTONNO = cartonNo;
                        //cartonInvDetail.PACKMCODE = "";
                        //cartonInvDetail.PACKQTY = 0;
                        cartonInvDetail.CUSER = mUser;
                        cartonInvDetail.CDATE = mDate;
                        cartonInvDetail.CTIME = mTime;
                        cartonInvDetail.MDATE = mDate;
                        cartonInvDetail.MTIME = mTime;
                        cartonInvDetail.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);

                        //  this.DataProvider.CommitTransaction();

                        //2>	新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                        //object _obj = this._WarehouseFacade.GetPickdetailmaterial(pickNo,cartonNo);
                        //if (_obj == null)
                        //{
                        //    this.DataProvider.RollbackTransaction();
                        //    WebInfoPublish.Publish(this, "查找已经捡料明细出错", this.languageComponent1);
                        //    return;
                        //}
                        //Pickdetailmaterial pickDetailmar = _obj as Pickdetailmaterial;
                        CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                        cartonInvDetailMaterial.CARINVNO = carInvNo;
                        cartonInvDetailMaterial.PICKNO = pickNo;
                        cartonInvDetailMaterial.PICKLINE = pickdetailmaterial.Pickline;
                        cartonInvDetailMaterial.CARTONNO = cartonNo;
                        cartonInvDetailMaterial.MCODE = pickdetailmaterial.MCode;
                        cartonInvDetailMaterial.DQMCODE = pickdetailmaterial.DqmCode;
                        cartonInvDetailMaterial.QTY = pickdetailmaterial.Qty;
                        cartonInvDetailMaterial.UNIT = pickdetailmaterial.Unit;
                        cartonInvDetailMaterial.GFHWITEMCODE = string.Empty;
                        cartonInvDetailMaterial.GFPACKINGSEQ = string.Empty;
                        cartonInvDetailMaterial.CUSER = mUser;
                        cartonInvDetailMaterial.CDATE = mDate;
                        cartonInvDetailMaterial.CTIME = mTime;
                        cartonInvDetailMaterial.MDATE = mDate;
                        cartonInvDetailMaterial.MTIME = mTime;
                        cartonInvDetailMaterial.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                        //3>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                        object[] objs = this._WarehouseFacade.GetPickDetailMaterialSN(pickNo, cartonNo);
                        if (objs == null)
                        {
                            //this.DataProvider.RollbackTransaction();
                            //WebInfoPublish.Publish(this, "查找已拣物料SN出错", this.languageComponent1);
                            //return;
                        }
                        else
                        {
                            CARTONINVDETAILSN cartonINVDETAILSN = new CARTONINVDETAILSN();

                            foreach (Pickdetailmaterialsn pickdetailmaterialsn in objs)
                            {
                                cartonINVDETAILSN.CARINVNO = carInvNo;
                                cartonINVDETAILSN.PICKNO = pickNo;
                                cartonINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                                cartonINVDETAILSN.CARTONNO = cartonNo;
                                cartonINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                                cartonINVDETAILSN.MUSER = mUser;
                                cartonINVDETAILSN.MDATE = mDate;
                                cartonINVDETAILSN.MTIME = mTime;
                                this._WarehouseFacade.AddCARTONINVDETAILSN(cartonINVDETAILSN);
                            }
                        }

                        ////6>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                        //pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        //pickdetailmaterial.MaintainUser = mUser;
                        //pickdetailmaterial.MaintainDate = mDate;
                        //pickdetailmaterial.MaintainTime = mTime;
                        //this._WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);

                        ////4>	更新拣拣货任务令头表(TBLPICK)数据
                        //object objPick = this._InventoryFacade.GetPick(pickNo);
                        //if (objPick != null)
                        //{
                        //    Pick pick = objPick as Pick;
                        //    pick.Status = "Pack";
                        //    pick.MaintainUser = mUser;
                        //    pick.MaintainDate = mDate;
                        //    pick.MaintainTime = mTime;
                        //    this._InventoryFacade.UpdatePick(pick);
                        //}

                        #region //更新 pickdetailmaterial
                        pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        pickdetailmaterial.MaintainDate = mDate;
                        pickdetailmaterial.MaintainTime = mTime;
                        pickdetailmaterial.MaintainUser = mUser;
                        _WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);
                        #endregion

                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "计算包装数量出错";
                            return false;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object pickdetail_obj = _WarehouseFacade.GetPickdetail(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "更新拣货明细表出错";
                            return false;
                        }
                        PickDetail pickdetail = pickdetail_obj as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);
                        #endregion
                    }
                    #endregion
                }

                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES == null)
                {
                    this.DataProvider.RollbackTransaction();
                    message = "找不到拣货任务令号对应的发货箱单头信息";
                    return false;
                }
                CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
                {
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] _objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (_objs != null)
                {
                    bool isTrue = true;
                    foreach (PickDetail _pickDetail in _objs)
                    {
                        if (_pickDetail.SQTY != _pickDetail.PQTY)
                        {
                            isTrue = false;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        object _objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                        if (_objCARTONINVOICES != null)
                        {
                            CARTONINVOICES cARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                            cARTONINVOICES.STATUS = "ClosePack";
                            cARTONINVOICES.FDATE = mDate;
                            cARTONINVOICES.FTIME = mTime;
                            cARTONINVOICES.MUSER = mUser;
                            cARTONINVOICES.MDATE = mDate;
                            cARTONINVOICES.MTIME = mTime;
                            this._WarehouseFacade.UpdateTBLCartonInvoices(cARTONINVOICES);


                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);

                        }
                    }
                }

                this.DataProvider.CommitTransaction();
                message = "操作成功";
                return true;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                message = "操作失败：" + ex.Message;
                return false;
            }
        }

        //数量回车
        [WebMethod(EnableSession = true)]
        public bool QTYKeyPressReturnMessage(string pickNo, string dqMCode, string cartonNo, string qty, string userCode, out string message)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            string carInvNo = this.QueryCarInvNo(pickNo);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            try
            {
                this.DataProvider.BeginTransaction();
                //更新状态
                object objPick = this._InventoryFacade.GetPick(pickNo);
                if (objPick != null)
                {
                    Pick pick = objPick as Pick;
                    pick.Status = "Pack";
                    pick.MaintainUser = mUser;
                    pick.MaintainDate = mDate;
                    pick.MaintainTime = mTime;
                    this._InventoryFacade.UpdatePick(pick);


                    if (_WarehouseFacade.GetPackCount(pickNo, "PACK") == 0)
                    {
                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = " ";
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                        trans.InvType = (pick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = userCode;
                        trans.MCode = " ";
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);

                    }

                }

                object objPICKDetailMaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
                //捡料表中是否存在箱号？
                if (objPICKDetailMaterial == null)
                {
                    #region 非整箱包货
                    //检查箱号，鼎桥物料号，是够存在
                    if (string.IsNullOrEmpty(dqMCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "必须选择鼎桥物料号";
                        return false;
                    }

                    //不存在，要通过鼎桥物料号判断是取SN还是数量
                    //1，根据鼎桥物料号，先判断是单件管控还是非单件管控
                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "无效的鼎桥物料号";
                        return false;
                    }
                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "此物料为单件管控，请输入SN号码按回车";
                        return false;
                    }
                    else // 非单件管控
                    {
                        // 取页面上的数量
                        //检查一下，鼎桥物料号在这个表
                        // 有对表tblcartoninvoicesmaterial tblpickdetailmaterial 有数据。 
                        #region 非单件管控
                        if (string.IsNullOrEmpty(qty))
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "数量不能为空";
                            return false;
                        }
                        //判断数量是否是数字格式
                        try
                        {
                            decimal _qty = decimal.Parse(qty);
                            if (_qty <= 0)
                            {
                                this.DataProvider.RollbackTransaction();
                                message = "数量必须为大于零的数字";
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "数量必须为大于零的数字";
                            return false;
                        }

                        object[] objPickdetail = _WarehouseFacade.GetAllPickDetailByPickNoAndDQMCode(pickNo, dqMCode);
                        if (objPickdetail == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "拣货任务明细中没有该鼎桥物料号";
                            return false;
                        }
                        PickDetail Pickdetail = objPickdetail[0] as PickDetail;
                        object objCartonInvDetail = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        if (objCartonInvDetail == null)
                        {
                            CartonInvDetail cartonInvDetail = new CartonInvDetail();
                            cartonInvDetail.CARINVNO = carInvNo;
                            cartonInvDetail.PICKNO = pickNo;
                            cartonInvDetail.STATUS = "Pack";
                            cartonInvDetail.CARTONNO = cartonNo;
                            cartonInvDetail.PACKMCODE = "";
                            cartonInvDetail.PACKQTY = double.Parse(qty);
                            cartonInvDetail.CUSER = mUser;
                            cartonInvDetail.CDATE = mDate;
                            cartonInvDetail.CTIME = mTime;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            cartonInvDetail.MUSER = mUser;
                            this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);
                            // 
                            CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                            cartonInvDetailMaterial.CARINVNO = carInvNo;
                            cartonInvDetailMaterial.CARTONNO = cartonNo;
                            cartonInvDetailMaterial.CDATE = mDate;
                            cartonInvDetailMaterial.CTIME = mTime;
                            cartonInvDetailMaterial.CUSER = mUser;
                            cartonInvDetailMaterial.DQMCODE = dqMCode;
                            cartonInvDetailMaterial.MCODE = mar.MCode;
                            cartonInvDetailMaterial.MDATE = mDate;
                            cartonInvDetailMaterial.MTIME = mTime;
                            cartonInvDetailMaterial.MUSER = mUser;
                            cartonInvDetailMaterial.PICKLINE = Pickdetail.PickLine;
                            cartonInvDetailMaterial.PICKNO = Pickdetail.PickNo;
                            cartonInvDetailMaterial.QTY = decimal.Parse(qty);
                            cartonInvDetailMaterial.UNIT = Pickdetail.Unit;

                            _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                        }
                        else
                        {

                            CartonInvDetail CartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, dqMCode);
                            if (objCartonInvDetailMaterial == null)
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                                cartonInvDetailMaterial.CARINVNO = carInvNo;
                                cartonInvDetailMaterial.CARTONNO = cartonNo;
                                cartonInvDetailMaterial.CDATE = mDate;
                                cartonInvDetailMaterial.CTIME = mTime;
                                cartonInvDetailMaterial.CUSER = mUser;
                                cartonInvDetailMaterial.DQMCODE = dqMCode;
                                cartonInvDetailMaterial.MCODE = mar.MCode;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.PICKLINE = Pickdetail.PickLine;
                                cartonInvDetailMaterial.PICKNO = Pickdetail.PickNo;
                                cartonInvDetailMaterial.QTY = decimal.Parse(qty);
                                cartonInvDetailMaterial.UNIT = Pickdetail.Unit;

                                _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                            }
                            else
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                                cartonInvDetailMaterial.QTY += decimal.Parse(qty);
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }

                            // 更新表CartonInvDetail的以包数量
                            CartonInvDetail cartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object[] objs_cartondetailmaterial = _WarehouseFacade.GetCartonInvDetailMaterial(carInvNo, cartonNo);
                            if (objs_cartondetailmaterial == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                message = "更新发货箱出错";
                                return false;
                            }
                            double ss = 0;
                            for (int i = 0; i < objs_cartondetailmaterial.Length; i++)
                            {
                                CartonInvDetailMaterial cartondetailmar = objs_cartondetailmaterial[i] as CartonInvDetailMaterial;
                                ss += (double)cartondetailmar.QTY;
                            }
                            cartonInvDetail.PACKQTY = ss;
                            cartonInvDetail.MUSER = mUser;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                        }
                        #endregion
                        #region //更新 pickdetailmaterial
                        //5>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                        decimal qTY = decimal.Parse(qty);
                        object[] objPickdetailmaterials = this._WarehouseFacade.QueryPICKDetailMaterials(pickNo, dqMCode);
                        foreach (Pickdetailmaterial _pickdetailmaterial in objPickdetailmaterials)
                        {
                            decimal num = _pickdetailmaterial.Qty - _pickdetailmaterial.PQty;
                            if (num > 0)
                            {
                                if (qTY > num)
                                {
                                    _pickdetailmaterial.PQty = _pickdetailmaterial.Qty;
                                    _pickdetailmaterial.MaintainUser = mUser;
                                    _pickdetailmaterial.MaintainDate = mDate;
                                    _pickdetailmaterial.MaintainTime = mTime;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);

                                    qTY -= num;
                                }
                                else
                                {
                                    _pickdetailmaterial.PQty += qTY;
                                    _pickdetailmaterial.MaintainUser = mUser;
                                    _pickdetailmaterial.MaintainDate = mDate;
                                    _pickdetailmaterial.MaintainTime = mTime;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);

                                    qTY = 0;

                                    break;
                                }
                            }
                        }
                        if (qTY > 0)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "输入的包装数量过大";
                            return false;
                        }

                        #endregion
                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndDQMCode(pickNo, dqMCode);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "计算包装数量出错";
                            return false;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object[] pickdetail_obj = _WarehouseFacade.GetAllPickDetailByPickNoAndDQMCode(pickNo, dqMCode);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "更新拣货明细表出错";
                            return false;
                        }
                        PickDetail pickdetail = pickdetail_obj[0] as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;


                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);
                        #endregion

                        #region  箱包装完成，改变状态
                        object objCartonInvDetail1 = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        CartonInvDetail car = objCartonInvDetail1 as CartonInvDetail;
                        if (pickdetail.SQTY == (decimal)car.PACKQTY)
                        {
                            car.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                            car.MUSER = mUser;
                            car.MDATE = mDate;
                            car.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(car);
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 整箱包货
                    //如果箱号是有
                    //1, cartonNo 在tblpickdetailmaterial中 以pickno为条件，查
                    //如果有，就将tblpickdetailmaterial where carton=‘cartonNo’ and pick=？ 搬到tblcartoninvoicesmaterial中
                    //如果这个表tblpickdetailmaterialsn中有数据，将这些数据搬到tblcartoninvoicesmaterialsn中
                    Pickdetailmaterial pickdetailmaterial = objPICKDetailMaterial as Pickdetailmaterial;
                    if (pickdetailmaterial.PQty != 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "该箱号已部分包装,不能再使用原箱号码";
                        return false;
                    }
                    else
                    {
                        //1>	新增发货箱单明细信息表(TBLCartonInvDetail)数据
                        CartonInvDetail cartonInvDetail = new CartonInvDetail();
                        cartonInvDetail.CARINVNO = carInvNo;
                        cartonInvDetail.PICKNO = pickNo;
                        cartonInvDetail.STATUS = "ClosePack";
                        cartonInvDetail.CARTONNO = cartonNo;
                        //cartonInvDetail.PACKMCODE = "";
                        //cartonInvDetail.PACKQTY = 0;
                        cartonInvDetail.CUSER = mUser;
                        cartonInvDetail.CDATE = mDate;
                        cartonInvDetail.CTIME = mTime;
                        cartonInvDetail.MDATE = mDate;
                        cartonInvDetail.MTIME = mTime;
                        cartonInvDetail.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);

                        //  this.DataProvider.CommitTransaction();

                        //2>	新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                        //object _obj = this._WarehouseFacade.GetPickdetailmaterial(pickNo,cartonNo);
                        //if (_obj == null)
                        //{
                        //    this.DataProvider.RollbackTransaction();
                        //    WebInfoPublish.Publish(this, "查找已经捡料明细出错", this.languageComponent1);
                        //    return;
                        //}
                        //Pickdetailmaterial pickDetailmar = _obj as Pickdetailmaterial;
                        CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                        cartonInvDetailMaterial.CARINVNO = carInvNo;
                        cartonInvDetailMaterial.PICKNO = pickNo;
                        cartonInvDetailMaterial.PICKLINE = pickdetailmaterial.Pickline;
                        cartonInvDetailMaterial.CARTONNO = cartonNo;
                        cartonInvDetailMaterial.MCODE = pickdetailmaterial.MCode;
                        cartonInvDetailMaterial.DQMCODE = pickdetailmaterial.DqmCode;
                        cartonInvDetailMaterial.QTY = pickdetailmaterial.Qty;
                        cartonInvDetailMaterial.UNIT = pickdetailmaterial.Unit;
                        cartonInvDetailMaterial.GFHWITEMCODE = string.Empty;
                        cartonInvDetailMaterial.GFPACKINGSEQ = string.Empty;
                        cartonInvDetailMaterial.CUSER = mUser;
                        cartonInvDetailMaterial.CDATE = mDate;
                        cartonInvDetailMaterial.CTIME = mTime;
                        cartonInvDetailMaterial.MDATE = mDate;
                        cartonInvDetailMaterial.MTIME = mTime;
                        cartonInvDetailMaterial.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                        //3>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                        object[] objs = this._WarehouseFacade.GetPickDetailMaterialSN(pickNo, cartonNo);
                        if (objs == null)
                        {
                            //this.DataProvider.RollbackTransaction();
                            //WebInfoPublish.Publish(this, "查找已拣物料SN出错", this.languageComponent1);
                            //return;
                        }
                        else
                        {
                            CARTONINVDETAILSN cartonINVDETAILSN = new CARTONINVDETAILSN();

                            foreach (Pickdetailmaterialsn pickdetailmaterialsn in objs)
                            {
                                cartonINVDETAILSN.CARINVNO = carInvNo;
                                cartonINVDETAILSN.PICKNO = pickNo;
                                cartonINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                                cartonINVDETAILSN.CARTONNO = cartonNo;
                                cartonINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                                cartonINVDETAILSN.MUSER = mUser;
                                cartonINVDETAILSN.MDATE = mDate;
                                cartonINVDETAILSN.MTIME = mTime;
                                this._WarehouseFacade.AddCARTONINVDETAILSN(cartonINVDETAILSN);
                            }
                        }

                        ////6>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                        //pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        //pickdetailmaterial.MaintainUser = mUser;
                        //pickdetailmaterial.MaintainDate = mDate;
                        //pickdetailmaterial.MaintainTime = mTime;
                        //this._WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);

                        ////4>	更新拣拣货任务令头表(TBLPICK)数据
                        //object objPick = this._InventoryFacade.GetPick(pickNo);
                        //if (objPick != null)
                        //{
                        //    Pick pick = objPick as Pick;
                        //    pick.Status = "Pack";
                        //    pick.MaintainUser = mUser;
                        //    pick.MaintainDate = mDate;
                        //    pick.MaintainTime = mTime;
                        //    this._InventoryFacade.UpdatePick(pick);
                        //}

                        #region //更新 pickdetailmaterial
                        pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        pickdetailmaterial.MaintainDate = mDate;
                        pickdetailmaterial.MaintainTime = mTime;
                        pickdetailmaterial.MaintainUser = mUser;
                        _WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);
                        #endregion

                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "计算包装数量出错";
                            return false;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object pickdetail_obj = _WarehouseFacade.GetPickdetail(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "更新拣货明细表出错";
                            return false;
                        }
                        PickDetail pickdetail = pickdetail_obj as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);

                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);
                        #endregion
                    }
                    #endregion
                }

                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES == null)
                {
                    this.DataProvider.RollbackTransaction();
                    message = "找不到拣货任务令号对应的发货箱单头信息";
                    return false;
                }
                CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
                {
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] _objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (_objs != null)
                {
                    bool isTrue = true;
                    foreach (PickDetail _pickDetail in _objs)
                    {
                        if (_pickDetail.SQTY != _pickDetail.PQTY)
                        {
                            isTrue = false;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        object _objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                        if (_objCARTONINVOICES != null)
                        {
                            CARTONINVOICES cARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                            cARTONINVOICES.STATUS = "ClosePack";
                            cARTONINVOICES.FDATE = mDate;
                            cARTONINVOICES.FTIME = mTime;
                            cARTONINVOICES.MUSER = mUser;
                            cARTONINVOICES.MDATE = mDate;
                            cARTONINVOICES.MTIME = mTime;
                            this._WarehouseFacade.UpdateTBLCartonInvoices(cARTONINVOICES);



                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);

                        }
                    }
                }

                this.DataProvider.CommitTransaction();
                message = "操作成功";
                return true;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                message = "操作失败：" + ex.Message;
                return false;
            }
        }

        //SN回车
        [WebMethod(EnableSession = true)]
        public bool SNKeyPressReturnMessage(string pickNo, string dqMCode, string cartonNo, string sn, string userCode, out string message)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            string carInvNo = this.QueryCarInvNo(pickNo);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;
            message = string.Empty;

            try
            {
                this.DataProvider.BeginTransaction();
                //更新状态
                object objPick = this._InventoryFacade.GetPick(pickNo);
                if (objPick != null)
                {
                    Pick pick = objPick as Pick;
                    pick.Status = "Pack";
                    pick.MaintainUser = mUser;
                    pick.MaintainDate = mDate;
                    pick.MaintainTime = mTime;
                    this._InventoryFacade.UpdatePick(pick);


                    if (_WarehouseFacade.GetPackCount(pickNo, "PACK") == 0)
                    {
                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = " ";
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                        trans.InvType = (pick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = userCode;
                        trans.MCode = " ";
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);

                    }
                }

                object objPICKDetailMaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
                //捡料表中是否存在箱号？
                if (objPICKDetailMaterial == null)
                {
                    #region 非整箱包货
                    //检查箱号，鼎桥物料号，是够存在
                    if (string.IsNullOrEmpty(dqMCode))
                    {
                        this.DataProvider.RollbackTransaction();

                        message = "必须选择鼎桥物料号";
                        return false;
                    }

                    //不存在，要通过鼎桥物料号判断是取SN还是数量
                    //1，根据鼎桥物料号，先判断是单件管控还是非单件管控
                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "无效的鼎桥物料号";
                        return false;
                    }
                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        #region 单件管控
                        if (string.IsNullOrEmpty(sn))
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "SN号码不能为空";
                            return false;
                        }
                        //  取SN
                        //检查SN在tblpickdetailmaterialsn表里有数据吗，没有报错，有对表tblcartoninvoicesmaterial，tblcartoninvoicesmaterialsn操作
                        object objPickdetailmaterialsn = this._WarehouseFacade.GetPickdetailmaterialsn(pickNo, sn);
                        if (objPickdetailmaterialsn == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "刷入SN条码不存在";
                            return false;
                        }
                        Pickdetailmaterialsn pikdetailsn = objPickdetailmaterialsn as Pickdetailmaterialsn;
                        object objPickdetailmaterial = _WarehouseFacade.GetPickdetailmaterial(pikdetailsn.Pickno, pikdetailsn.Cartonno);
                        if (objPickdetailmaterial == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "输入的SN找不到箱号信息";
                            return false;
                        }
                        Pickdetailmaterial Pickdetailmaterial = objPickdetailmaterial as Pickdetailmaterial;
                        //2>	检查SN条码是否存在当前拣货任务令号对应发货箱单的发货箱单明细SN信息表(TBLCartonInvDetailSN)中，存在则报错提示刷入SN条码已包装过
                        object _obj = this._WarehouseFacade.GetCartoninvdetailsn(carInvNo, sn);
                        if (_obj != null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "刷入SN条码已包装过";
                            return false;
                        }
                        //输入的SN的鼎桥物料号是否与选中的相同

                        if (Pickdetailmaterial.DqmCode != dqMCode)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "SN的号码与选择的鼎桥物料号不一致";
                            return false;
                        }
                        //5>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                        Pickdetailmaterialsn pickdetailmaterialsn = objPickdetailmaterialsn as Pickdetailmaterialsn;
                        CARTONINVDETAILSN _CARTONINVDETAILSN = new CARTONINVDETAILSN();
                        _CARTONINVDETAILSN.CARINVNO = carInvNo;
                        _CARTONINVDETAILSN.PICKNO = pickNo;
                        _CARTONINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                        _CARTONINVDETAILSN.CARTONNO = cartonNo;
                        _CARTONINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                        _CARTONINVDETAILSN.MUSER = mUser;
                        _CARTONINVDETAILSN.MDATE = mDate;
                        _CARTONINVDETAILSN.MTIME = mTime;
                        this._WarehouseFacade.AddCARTONINVDETAILSN(_CARTONINVDETAILSN);

                        //3>	检查包装箱号是否存在当前拣货任务令号对应发货箱单的发货箱单明细信息表(TBLCartonInvDetail)中，不存在则新增发货箱单明细信息表(TBLCartonInvDetail)数据
                        object objCartonInvDetail = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        if (objCartonInvDetail == null)
                        {
                            //箱是否存在--不存在
                            CartonInvDetail cartonInvDetail = new CartonInvDetail();
                            cartonInvDetail.CARINVNO = carInvNo;
                            cartonInvDetail.PICKNO = pickNo;
                            cartonInvDetail.STATUS = "Pack";
                            cartonInvDetail.CARTONNO = cartonNo;
                            cartonInvDetail.PACKMCODE = "";
                            cartonInvDetail.PACKQTY = 1;
                            cartonInvDetail.CUSER = mUser;
                            cartonInvDetail.CDATE = mDate;
                            cartonInvDetail.CTIME = mTime;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            cartonInvDetail.MUSER = mUser;
                            this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);

                            //this.DataProvider.CommitTransaction();

                            //  插入 cartonInvDetailMaterial
                            CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                            cartonInvDetailMaterial.CARINVNO = carInvNo;
                            cartonInvDetailMaterial.CARTONNO = cartonNo;
                            cartonInvDetailMaterial.CDATE = mDate;
                            cartonInvDetailMaterial.CTIME = mTime;
                            cartonInvDetailMaterial.CUSER = mUser;
                            cartonInvDetailMaterial.DQMCODE = dqMCode;
                            cartonInvDetailMaterial.MCODE = mar.MCode;
                            cartonInvDetailMaterial.MDATE = mDate;
                            cartonInvDetailMaterial.MTIME = mTime;
                            cartonInvDetailMaterial.MUSER = mUser;
                            cartonInvDetailMaterial.PICKLINE = pikdetailsn.Pickline;
                            cartonInvDetailMaterial.PICKNO = pikdetailsn.Pickno;
                            cartonInvDetailMaterial.QTY = 1;
                            cartonInvDetailMaterial.UNIT = Pickdetailmaterial.Unit;

                            _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
                        }
                        else
                        {
                            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, dqMCode);
                            if (objCartonInvDetailMaterial == null)
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                                cartonInvDetailMaterial.CARINVNO = carInvNo;
                                cartonInvDetailMaterial.CARTONNO = cartonNo;
                                cartonInvDetailMaterial.CDATE = mDate;
                                cartonInvDetailMaterial.CTIME = mTime;
                                cartonInvDetailMaterial.CUSER = mUser;
                                cartonInvDetailMaterial.DQMCODE = dqMCode;
                                cartonInvDetailMaterial.MCODE = mar.MCode;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.PICKLINE = pikdetailsn.Pickline;
                                cartonInvDetailMaterial.PICKNO = pikdetailsn.Pickno;
                                cartonInvDetailMaterial.QTY = 1;
                                cartonInvDetailMaterial.UNIT = Pickdetailmaterial.Unit;

                                _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }
                            else
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                                cartonInvDetailMaterial.QTY += 1;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }
                            // 更新表CartonInvDetail的以包数量
                            CartonInvDetail cartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object[] objs_cartondetailmaterial = _WarehouseFacade.GetCartonInvDetailMaterial(carInvNo, cartonNo);
                            if (objs_cartondetailmaterial == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                message = "更新发货箱出错";
                                return false;
                            }
                            double ss = 0;
                            for (int i = 0; i < objs_cartondetailmaterial.Length; i++)
                            {
                                CartonInvDetailMaterial cartondetailmar = objs_cartondetailmaterial[i] as CartonInvDetailMaterial;
                                ss += (double)cartondetailmar.QTY;
                            }
                            cartonInvDetail.PACKQTY = ss;
                            cartonInvDetail.MUSER = mUser;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);



                        }
                        #endregion
                        #region //更新 pickdetailmaterial
                        Pickdetailmaterial.PQty += 1;
                        Pickdetailmaterial.MaintainDate = mDate;
                        Pickdetailmaterial.MaintainTime = mTime;
                        Pickdetailmaterial.MaintainUser = mUser;
                        _WarehouseFacade.UpdatePickdetailmaterial(Pickdetailmaterial);

                        #endregion
                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, pikdetailsn.Pickline);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "计算包装数量出错";
                            return false;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object pickdetail_obj = _WarehouseFacade.GetPickdetail(pickNo, pikdetailsn.Pickline);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "更新拣货明细表出错";
                            return false;
                        }
                        PickDetail pickdetail = pickdetail_obj as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);
                        #endregion

                        #region  箱包装完成，改变状态
                        object objCartonInvDetail1 = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        CartonInvDetail car = objCartonInvDetail1 as CartonInvDetail;
                        if (pickdetail.SQTY == (decimal)car.PACKQTY)
                        {
                            car.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                            car.MUSER = mUser;
                            car.MDATE = mDate;
                            car.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(car);
                        }
                        #endregion

                    }
                    else // 非单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "此物料为非单件管控，请输入数量按回车";
                        return false;
                    }
                    #endregion
                }
                else
                {
                    #region 整箱包货
                    //如果箱号是有
                    //1, cartonNo 在tblpickdetailmaterial中 以pickno为条件，查
                    //如果有，就将tblpickdetailmaterial where carton=‘cartonNo’ and pick=？ 搬到tblcartoninvoicesmaterial中
                    //如果这个表tblpickdetailmaterialsn中有数据，将这些数据搬到tblcartoninvoicesmaterialsn中
                    Pickdetailmaterial pickdetailmaterial = objPICKDetailMaterial as Pickdetailmaterial;
                    if (pickdetailmaterial.PQty != 0)
                    {
                        this.DataProvider.RollbackTransaction();
                        message = "该箱号已部分包装,不能再使用原箱号码";
                        return false;
                    }
                    else
                    {
                        //1>	新增发货箱单明细信息表(TBLCartonInvDetail)数据
                        CartonInvDetail cartonInvDetail = new CartonInvDetail();
                        cartonInvDetail.CARINVNO = carInvNo;
                        cartonInvDetail.PICKNO = pickNo;
                        cartonInvDetail.STATUS = "ClosePack";
                        cartonInvDetail.CARTONNO = cartonNo;
                        //cartonInvDetail.PACKMCODE = "";
                        //cartonInvDetail.PACKQTY = 0;
                        cartonInvDetail.CUSER = mUser;
                        cartonInvDetail.CDATE = mDate;
                        cartonInvDetail.CTIME = mTime;
                        cartonInvDetail.MDATE = mDate;
                        cartonInvDetail.MTIME = mTime;
                        cartonInvDetail.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);

                        //  this.DataProvider.CommitTransaction();

                        //2>	新增发货箱单明细物料信息表(TBLCartonInvDetailMaterial)数据
                        //object _obj = this._WarehouseFacade.GetPickdetailmaterial(pickNo,cartonNo);
                        //if (_obj == null)
                        //{
                        //    this.DataProvider.RollbackTransaction();
                        //    WebInfoPublish.Publish(this, "查找已经捡料明细出错", this.languageComponent1);
                        //    return;
                        //}
                        //Pickdetailmaterial pickDetailmar = _obj as Pickdetailmaterial;
                        CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                        cartonInvDetailMaterial.CARINVNO = carInvNo;
                        cartonInvDetailMaterial.PICKNO = pickNo;
                        cartonInvDetailMaterial.PICKLINE = pickdetailmaterial.Pickline;
                        cartonInvDetailMaterial.CARTONNO = cartonNo;
                        cartonInvDetailMaterial.MCODE = pickdetailmaterial.MCode;
                        cartonInvDetailMaterial.DQMCODE = pickdetailmaterial.DqmCode;
                        cartonInvDetailMaterial.QTY = pickdetailmaterial.Qty;
                        cartonInvDetailMaterial.UNIT = pickdetailmaterial.Unit;
                        cartonInvDetailMaterial.GFHWITEMCODE = string.Empty;
                        cartonInvDetailMaterial.GFPACKINGSEQ = string.Empty;
                        cartonInvDetailMaterial.CUSER = mUser;
                        cartonInvDetailMaterial.CDATE = mDate;
                        cartonInvDetailMaterial.CTIME = mTime;
                        cartonInvDetailMaterial.MDATE = mDate;
                        cartonInvDetailMaterial.MTIME = mTime;
                        cartonInvDetailMaterial.MUSER = mUser;
                        this._WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                        //3>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                        object[] objs = this._WarehouseFacade.GetPickDetailMaterialSN(pickNo, cartonNo);
                        if (objs == null)
                        {
                            //this.DataProvider.RollbackTransaction();
                            //WebInfoPublish.Publish(this, "查找已拣物料SN出错", this.languageComponent1);
                            //return;
                        }
                        else
                        {
                            CARTONINVDETAILSN cartonINVDETAILSN = new CARTONINVDETAILSN();

                            foreach (Pickdetailmaterialsn pickdetailmaterialsn in objs)
                            {
                                cartonINVDETAILSN.CARINVNO = carInvNo;
                                cartonINVDETAILSN.PICKNO = pickNo;
                                cartonINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                                cartonINVDETAILSN.CARTONNO = cartonNo;
                                cartonINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                                cartonINVDETAILSN.MUSER = mUser;
                                cartonINVDETAILSN.MDATE = mDate;
                                cartonINVDETAILSN.MTIME = mTime;
                                this._WarehouseFacade.AddCARTONINVDETAILSN(cartonINVDETAILSN);
                            }
                        }

                        ////6>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                        //pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        //pickdetailmaterial.MaintainUser = mUser;
                        //pickdetailmaterial.MaintainDate = mDate;
                        //pickdetailmaterial.MaintainTime = mTime;
                        //this._WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);

                        ////4>	更新拣拣货任务令头表(TBLPICK)数据
                        //object objPick = this._InventoryFacade.GetPick(pickNo);
                        //if (objPick != null)
                        //{
                        //    Pick pick = objPick as Pick;
                        //    pick.Status = "Pack";
                        //    pick.MaintainUser = mUser;
                        //    pick.MaintainDate = mDate;
                        //    pick.MaintainTime = mTime;
                        //    this._InventoryFacade.UpdatePick(pick);
                        //}

                        #region //更新 pickdetailmaterial
                        pickdetailmaterial.PQty = pickdetailmaterial.Qty;
                        pickdetailmaterial.MaintainDate = mDate;
                        pickdetailmaterial.MaintainTime = mTime;
                        pickdetailmaterial.MaintainUser = mUser;
                        _WarehouseFacade.UpdatePickdetailmaterial(pickdetailmaterial);
                        #endregion

                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "计算包装数量出错";
                            return false;
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object pickdetail_obj = _WarehouseFacade.GetPickdetail(pickNo, pickdetailmaterial.Pickline);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            message = "更新拣货明细表出错";
                            return false;
                        }
                        PickDetail pickdetail = pickdetail_obj as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;


                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);
                        #endregion
                    }
                    #endregion
                }

                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES == null)
                {
                    this.DataProvider.RollbackTransaction();
                    message = "找不到拣货任务令号对应的发货箱单头信息";
                    return false;
                }
                CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
                {
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] _objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (_objs != null)
                {
                    bool isTrue = true;
                    foreach (PickDetail _pickDetail in _objs)
                    {
                        if (_pickDetail.SQTY != _pickDetail.PQTY)
                        {
                            isTrue = false;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        object _objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                        if (_objCARTONINVOICES != null)
                        {
                            CARTONINVOICES cARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                            cARTONINVOICES.STATUS = "ClosePack";
                            cARTONINVOICES.FDATE = mDate;
                            cARTONINVOICES.FTIME = mTime;
                            cARTONINVOICES.MUSER = mUser;
                            cARTONINVOICES.MDATE = mDate;
                            cARTONINVOICES.MTIME = mTime;
                            this._WarehouseFacade.UpdateTBLCartonInvoices(cARTONINVOICES);



                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);





                        }
                    }
                }

                this.DataProvider.CommitTransaction();
                message = "操作成功";
                return true;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                message = "操作失败：" + ex.Message;
                return false;
            }
        }

        //提交
        [WebMethod(EnableSession = true)]
        public string SubmitReturnMessage(string pickNo, string cartonNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            object obj = this._WarehouseFacade.GetCartonInvoices(pickNo);
            if (obj == null)
            {
                return "当前拣货任务令号没有对应的发货箱单信息";
            }
            StorageDetail storageDetail = (StorageDetail)_WarehouseFacade.GetStorageDetail(cartonNo);
            if (storageDetail != null)
            {
                if (storageDetail.AvailableQty > 0)
                {
                    return "请使用新箱包装";
                }
            }

            try
            {
                this.DataProvider.BeginTransaction();

                //1. 当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
                object _obj = this._WarehouseFacade.GetCartonInvDetail((obj as CARTONINVOICES).CARINVNO, cartonNo);
                if (_obj == null)
                {
                    return "当前包装箱号没有对应的发货箱单明细信息";
                }

                CartonInvDetail cartonInvDetail = _obj as CartonInvDetail;
                cartonInvDetail.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                cartonInvDetail.MUSER = mUser;
                cartonInvDetail.MDATE = mDate;
                cartonInvDetail.MTIME = mTime;
                this._WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                //2. 检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (objs == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "当前拣货任务令号没有对应的拣货任务令明细信息";
                }

                bool isTrue = true;
                foreach (PickDetail pickDetail in objs)
                {
                    if (pickDetail.SQTY != pickDetail.PQTY)
                    {
                        isTrue = false;
                        break;
                    }
                }
                if (isTrue)
                {
                    CARTONINVOICES _CARTONINVOICES = obj as CARTONINVOICES;
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                    _CARTONINVOICES.FDATE = mDate;
                    _CARTONINVOICES.FTIME = mTime;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                foreach (PickDetail pickDetail in objs)
                {
                    pickDetail.Status = PickDetail_STATUS.Status_ClosePack;
                    pickDetail.MaintainUser = mUser;
                    pickDetail.MaintainDate = mDate;
                    pickDetail.MaintainTime = mTime;
                    _WarehouseFacade.UpdatePickdetail(pickDetail);
                }

                this.DataProvider.CommitTransaction();
                return "提交成功";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "提交失败：" + ex.Message;
            }
        }
        #endregion

        #region 光伏包装作业
        [WebMethod(EnableSession = true)]
        public string[] QueryGFHWItemCode(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryGFHWItemCode(pickNo);
        }

        [WebMethod(EnableSession = true)]
        public string[] QueryGFPackingSEQ(string pickNo, string gfHWItemCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryGFPackingSEQ(pickNo, gfHWItemCode);
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetDataGrid3(string pickNo, string gfHWItemCode, string gfPackingSEQ)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.QueryPDAPickMaterial(pickNo, gfPackingSEQ, gfPackingSEQ);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("PickLine", typeof(string));
                dt.Columns.Add("鼎桥编码", typeof(string));
                dt.Columns.Add("光伏华为编码", typeof(string));
                dt.Columns.Add("包装数", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add("", row["PICKLINE"], row["DQMCODE"], row["GFHWITEMCODE"], row["QTY"]);
                }
            }
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetDataGrid4(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade._QueryPDAPackingDetail(pickNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("光伏华为编码", typeof(string));
                dt.Columns.Add("光伏包装序号", typeof(string));
                dt.Columns.Add("鼎桥编码", typeof(string));
                dt.Columns.Add("拣货数量", typeof(string));
                dt.Columns.Add("已包数量", typeof(string));
                dt.Columns.Add("包装箱号", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add(row["GFHWITEMCODE"], row["GFPACKINGSEQ"], row["DQMCODE"], row["SQTY"], row["QTY"], row["CARTONNO"]);
                }
            }
            return dt;
        }

        //包装箱号回车
        [WebMethod(EnableSession = true)]
        public string GFCartonNOKeyPressReturnMessage(string pickNo, string gfHWItemCode, string gfPackingSEQ, string suiteQTY, string dqMCode, string cartonNo, string userCode)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            string carInvNo = this.QueryCarInvNo(pickNo);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            try
            {
                this.DataProvider.BeginTransaction();
                //更新状态
                object objPick = this._InventoryFacade.GetPick(pickNo);
                if (objPick != null)
                {
                    Pick pick = objPick as Pick;
                    pick.Status = "Pack";
                    pick.MaintainUser = mUser;
                    pick.MaintainDate = mDate;
                    pick.MaintainTime = mTime;
                    this._InventoryFacade.UpdatePick(pick);


                    if (_WarehouseFacade.GetPackCount(pickNo, "PACK") == 0)
                    {
                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = " ";
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                        trans.InvType = (pick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = userCode;
                        trans.MCode = " ";
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);

                    }

                }

                object objPICKDetailMaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
                //捡料表中是否存在箱号？
                if (objPICKDetailMaterial == null)
                {
                    #region 非整箱包货
                    //检查箱号，鼎桥物料号，是够存在
                    if (string.IsNullOrEmpty(dqMCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        return "必须选择鼎桥物料号";
                    }

                    //不存在，要通过鼎桥物料号判断是取SN还是数量
                    //1，根据鼎桥物料号，先判断是单件管控还是非单件管控
                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        return "无效的鼎桥物料号";
                    }
                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        return "此物料为单件管控，请输入SN号码按回车";
                    }
                    else // 非单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        return "此物料为非单件管控，请输入数量按回车";
                    }
                    #endregion
                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                    return "请使用新箱";
                }

                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "找不到拣货任务令号对应的发货箱单头信息";
                }
                CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
                {
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] _objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (_objs != null)
                {
                    bool isTrue = true;
                    foreach (PickDetail _pickDetail in _objs)
                    {
                        if (_pickDetail.SQTY != _pickDetail.PQTY)
                        {
                            isTrue = false;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        object _objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                        if (_objCARTONINVOICES != null)
                        {
                            CARTONINVOICES cARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                            cARTONINVOICES.STATUS = "ClosePack";
                            cARTONINVOICES.FDATE = mDate;
                            cARTONINVOICES.FTIME = mTime;
                            cARTONINVOICES.MUSER = mUser;
                            cARTONINVOICES.MDATE = mDate;
                            cARTONINVOICES.MTIME = mTime;
                            this._WarehouseFacade.UpdateTBLCartonInvoices(cARTONINVOICES);



                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);


                        }
                    }
                }

                this.DataProvider.CommitTransaction();
                return "操作成功";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "操作失败：" + ex.Message;
            }
        }

        //数量回车
        [WebMethod(EnableSession = true)]
        public string GFQTYKeyPressReturnMessage(string pickNo, string gfHWItemCode, string gfPackingSEQ, string suiteQTY, string dqMCode, string dqsMCode, string cartonNo, string qty, string userCode)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            string carInvNo = this.QueryCarInvNo(pickNo);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            try
            {
                this.DataProvider.BeginTransaction();
                //更新状态
                object objPick = this._InventoryFacade.GetPick(pickNo);
                if (objPick != null)
                {
                    Pick pick = objPick as Pick;
                    pick.Status = "Pack";
                    pick.MaintainUser = mUser;
                    pick.MaintainDate = mDate;
                    pick.MaintainTime = mTime;
                    this._InventoryFacade.UpdatePick(pick);


                    if (_WarehouseFacade.GetPackCount(pickNo, "PACK") == 0)
                    {
                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = " ";
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                        trans.InvType = (pick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = userCode;
                        trans.MCode = " ";
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);

                    }


                }

                object objPICKDetailMaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
                //捡料表中是否存在箱号？
                if (objPICKDetailMaterial == null)
                {
                    #region 非整箱包货
                    //检查箱号，鼎桥物料号，是够存在
                    if (string.IsNullOrEmpty(dqMCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        return "必须选择鼎桥物料号";
                    }

                    //不存在，要通过鼎桥物料号判断是取SN还是数量
                    //1，根据鼎桥物料号，先判断是单件管控还是非单件管控
                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        return "无效的鼎桥物料号";
                    }
                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        return "此物料为单件管控，请输入SN号码按回车";
                    }
                    else // 非单件管控
                    {
                        // 取页面上的数量
                        //检查一下，鼎桥物料号在这个表
                        // 有对表tblcartoninvoicesmaterial tblpickdetailmaterial 有数据。 
                        #region 非单件管控
                        if (string.IsNullOrEmpty(qty))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "数量不能为空";
                        }
                        //判断数量是否是数字格式
                        try
                        {
                            decimal _qty = decimal.Parse(qty);
                            if (_qty <= 0)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "数量必须为大于零的数字";
                            }
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "数量必须为大于零的数字";
                        }

                        object[] objPickdetail = _WarehouseFacade.GetAllPickDetailByPickNoAndDQMCode(pickNo, dqMCode);
                        if (objPickdetail == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "拣货任务明细中没有该鼎桥物料号";
                        }
                        PickDetail Pickdetail = objPickdetail[0] as PickDetail;
                        object objCartonInvDetail = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        if (objCartonInvDetail == null)
                        {
                            CartonInvDetail cartonInvDetail = new CartonInvDetail();
                            cartonInvDetail.CARINVNO = carInvNo;
                            cartonInvDetail.PICKNO = pickNo;
                            cartonInvDetail.STATUS = "Pack";
                            cartonInvDetail.CARTONNO = cartonNo;
                            cartonInvDetail.PACKMCODE = "";
                            cartonInvDetail.PACKQTY = double.Parse(qty);
                            cartonInvDetail.CUSER = mUser;
                            cartonInvDetail.CDATE = mDate;
                            cartonInvDetail.CTIME = mTime;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            cartonInvDetail.MUSER = mUser;

                            this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);
                            // 
                            CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                            cartonInvDetailMaterial.CARINVNO = carInvNo;
                            cartonInvDetailMaterial.CARTONNO = cartonNo;
                            cartonInvDetailMaterial.CDATE = mDate;
                            cartonInvDetailMaterial.CTIME = mTime;
                            cartonInvDetailMaterial.CUSER = mUser;
                            cartonInvDetailMaterial.DQMCODE = dqMCode;
                            cartonInvDetailMaterial.DQSMCODE = dqsMCode;
                            cartonInvDetailMaterial.MCODE = mar.MCode;
                            cartonInvDetailMaterial.MDATE = mDate;
                            cartonInvDetailMaterial.MTIME = mTime;
                            cartonInvDetailMaterial.MUSER = mUser;
                            cartonInvDetailMaterial.PICKLINE = Pickdetail.PickLine;
                            cartonInvDetailMaterial.PICKNO = Pickdetail.PickNo;
                            cartonInvDetailMaterial.QTY = decimal.Parse(qty);
                            cartonInvDetailMaterial.UNIT = Pickdetail.Unit;
                            cartonInvDetailMaterial.GFHWITEMCODE = gfHWItemCode;
                            cartonInvDetailMaterial.GFPACKINGSEQ = gfPackingSEQ;
                            _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                        }
                        else
                        {

                            CartonInvDetail CartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, dqMCode);
                            if (objCartonInvDetailMaterial == null)
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                                cartonInvDetailMaterial.CARINVNO = carInvNo;
                                cartonInvDetailMaterial.CARTONNO = cartonNo;
                                cartonInvDetailMaterial.CDATE = mDate;
                                cartonInvDetailMaterial.CTIME = mTime;
                                cartonInvDetailMaterial.CUSER = mUser;
                                cartonInvDetailMaterial.DQMCODE = dqMCode;
                                cartonInvDetailMaterial.DQSMCODE = dqsMCode;
                                cartonInvDetailMaterial.MCODE = mar.MCode;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.PICKLINE = Pickdetail.PickLine;
                                cartonInvDetailMaterial.PICKNO = Pickdetail.PickNo;
                                cartonInvDetailMaterial.QTY = decimal.Parse(qty);
                                cartonInvDetailMaterial.UNIT = Pickdetail.Unit;
                                cartonInvDetailMaterial.GFHWITEMCODE = gfHWItemCode;
                                cartonInvDetailMaterial.GFPACKINGSEQ = gfPackingSEQ;
                                _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);

                            }
                            else
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                                cartonInvDetailMaterial.QTY += decimal.Parse(qty);
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }

                            // 更新表CartonInvDetail的以包数量
                            CartonInvDetail cartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object[] objs_cartondetailmaterial = _WarehouseFacade.GetCartonInvDetailMaterial(carInvNo, cartonNo);
                            if (objs_cartondetailmaterial == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "更新发货箱出错";
                            }
                            double ss = 0;
                            for (int i = 0; i < objs_cartondetailmaterial.Length; i++)
                            {
                                CartonInvDetailMaterial cartondetailmar = objs_cartondetailmaterial[i] as CartonInvDetailMaterial;
                                ss += (double)cartondetailmar.QTY;
                            }
                            cartonInvDetail.PACKQTY = ss;
                            cartonInvDetail.MUSER = mUser;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                        }
                        #endregion
                        #region //更新 pickdetailmaterial
                        //5>	更新已拣物料明细表(TBLPICKDetailMaterial)数据
                        decimal qTY = decimal.Parse(qty);
                        object[] objPickdetailmaterials = this._WarehouseFacade.QueryPICKDetailMaterials(pickNo, dqMCode);
                        foreach (Pickdetailmaterial _pickdetailmaterial in objPickdetailmaterials)
                        {
                            decimal num = _pickdetailmaterial.Qty - _pickdetailmaterial.PQty;
                            if (num > 0)
                            {
                                if (qTY > num)
                                {
                                    _pickdetailmaterial.PQty = _pickdetailmaterial.Qty;
                                    _pickdetailmaterial.MaintainUser = mUser;
                                    _pickdetailmaterial.MaintainDate = mDate;
                                    _pickdetailmaterial.MaintainTime = mTime;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);

                                    qTY -= num;
                                }
                                else
                                {
                                    _pickdetailmaterial.PQty += qTY;
                                    _pickdetailmaterial.MaintainUser = mUser;
                                    _pickdetailmaterial.MaintainDate = mDate;
                                    _pickdetailmaterial.MaintainTime = mTime;
                                    this._WarehouseFacade.UpdatePickdetailmaterial(_pickdetailmaterial);

                                    qTY = 0;

                                    break;
                                }
                            }
                        }
                        if (qTY > 0)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "输入的包装数量过大";
                        }

                        #endregion
                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndDQMCode(pickNo, dqMCode);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "计算包装数量出错";
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object[] pickdetail_obj = _WarehouseFacade.GetAllPickDetailByPickNoAndDQMCode(pickNo, dqMCode);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "更新拣货明细表出错";
                        }
                        PickDetail pickdetail = pickdetail_obj[0] as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;


                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);
                        #endregion

                        #region  箱包装完成，改变状态
                        object objCartonInvDetail1 = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        CartonInvDetail car = objCartonInvDetail1 as CartonInvDetail;
                        if (pickdetail.SQTY == (decimal)car.PACKQTY)
                        {
                            car.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                            car.MUSER = mUser;
                            car.MDATE = mDate;
                            car.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(car);
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                    return "请使用新箱";
                }

                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "找不到拣货任务令号对应的发货箱单头信息";
                }
                CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
                {
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] _objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (_objs != null)
                {
                    bool isTrue = true;
                    foreach (PickDetail _pickDetail in _objs)
                    {
                        if (_pickDetail.SQTY != _pickDetail.PQTY)
                        {
                            isTrue = false;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        object _objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                        if (_objCARTONINVOICES != null)
                        {
                            CARTONINVOICES cARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                            cARTONINVOICES.STATUS = "ClosePack";
                            cARTONINVOICES.FDATE = mDate;
                            cARTONINVOICES.FTIME = mTime;
                            cARTONINVOICES.MUSER = mUser;
                            cARTONINVOICES.MDATE = mDate;
                            cARTONINVOICES.MTIME = mTime;
                            this._WarehouseFacade.UpdateTBLCartonInvoices(cARTONINVOICES);


                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);
                        }
                    }
                }

                this.DataProvider.CommitTransaction();
                return "操作成功";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "操作失败：" + ex.Message;
            }
        }

        //SN回车
        [WebMethod(EnableSession = true)]
        public string GFSNKeyPressReturnMessage(string pickNo, string gfHWItemCode, string gfPackingSEQ, string suiteQTY, string dqMCode, string dqsMCode, string cartonNo, string sn, string userCode)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            string carInvNo = this.QueryCarInvNo(pickNo);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            try
            {
                this.DataProvider.BeginTransaction();
                //更新状态
                object objPick = this._InventoryFacade.GetPick(pickNo);
                if (objPick != null)
                {
                    Pick pick = objPick as Pick;
                    pick.Status = "Pack";
                    pick.MaintainUser = mUser;
                    pick.MaintainDate = mDate;
                    pick.MaintainTime = mTime;
                    this._InventoryFacade.UpdatePick(pick);


                    if (_WarehouseFacade.GetPackCount(pickNo, "PACK") == 0)
                    {
                        InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                        trans.CartonNO = string.Empty;
                        trans.DqMCode = " ";
                        trans.FacCode = string.Empty;
                        trans.FromFacCode = string.Empty;
                        trans.FromStorageCode = string.Empty;
                        trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                        trans.InvType = (pick as Pick).PickType;
                        trans.LotNo = string.Empty;
                        trans.MaintainDate = mDate;
                        trans.MaintainTime = mTime;
                        trans.MaintainUser = userCode;
                        trans.MCode = " ";
                        trans.ProductionDate = 0;
                        trans.Qty = 0;
                        trans.Serial = 0;
                        trans.StorageAgeDate = 0;
                        trans.StorageCode = pick.StorageCode;
                        trans.SupplierLotNo = string.Empty;
                        trans.TransNO = pickNo;
                        trans.TransType = "OUT";
                        trans.Unit = string.Empty;
                        trans.ProcessType = "PACK";
                        _WarehouseFacade.AddInvInOutTrans(trans);

                    }

                }

                object objPICKDetailMaterial = this._WarehouseFacade.QueryPICKDetailMaterial(pickNo, cartonNo);
                //捡料表中是否存在箱号？
                if (objPICKDetailMaterial == null)
                {
                    #region 非整箱包货
                    //检查箱号，鼎桥物料号，是够存在
                    if (string.IsNullOrEmpty(dqMCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        return "必须选择鼎桥物料号";
                    }

                    //不存在，要通过鼎桥物料号判断是取SN还是数量
                    //1，根据鼎桥物料号，先判断是单件管控还是非单件管控
                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(dqMCode);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        return "无效的鼎桥物料号";
                    }
                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        #region 单件管控
                        if (string.IsNullOrEmpty(sn))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "SN号码不能为空";
                        }
                        //  取SN
                        //检查SN在tblpickdetailmaterialsn表里有数据吗，没有报错，有对表tblcartoninvoicesmaterial，tblcartoninvoicesmaterialsn操作
                        object objPickdetailmaterialsn = this._WarehouseFacade.GetPickdetailmaterialsn(pickNo, sn);
                        if (objPickdetailmaterialsn == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "刷入SN条码不存在";
                        }
                        Pickdetailmaterialsn pikdetailsn = objPickdetailmaterialsn as Pickdetailmaterialsn;
                        object objPickdetailmaterial = _WarehouseFacade.GetPickdetailmaterial(pikdetailsn.Pickno, pikdetailsn.Cartonno);
                        if (objPickdetailmaterial == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "输入的SN找不到箱号信息";
                        }
                        Pickdetailmaterial Pickdetailmaterial = objPickdetailmaterial as Pickdetailmaterial;
                        //2>	检查SN条码是否存在当前拣货任务令号对应发货箱单的发货箱单明细SN信息表(TBLCartonInvDetailSN)中，存在则报错提示刷入SN条码已包装过
                        object _obj = this._WarehouseFacade.GetCartoninvdetailsn(cartonNo, sn);
                        if (_obj != null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "刷入SN条码已包装过";
                        }
                        //输入的SN的鼎桥物料号是否与选中的相同
                        if (Pickdetailmaterial.DqmCode != dqMCode)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "SN的号码与选择的鼎桥物料号不一致";
                        }

                        //3.	检查通过SN条码在已拣物料明细表SN信息表(TBLPICKDetailMaterialSN)中对应的箱号(TBLPICKDetailMaterialSN.CARTONNO)在已拣物料明细表(TBLPICKDetailMaterial)对应拣货任务令明细表(TBLPICKDETAIL)中的光伏华为编码(TBLPICKDETAIL.GFHWITEMCODE)、光伏包装序号(TBLPICKDETAIL.GFPACKINGSEQ)与编辑区域中选择的光伏华为编码、光伏包装序号是否同等，如不相等则报错提示该箱号对应选择的光伏华为编码、光伏包装序号不相等
                        if (string.IsNullOrEmpty(gfHWItemCode))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "请选择光伏华为编码";
                        }
                        if (string.IsNullOrEmpty(gfPackingSEQ))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "请选择光伏包装序号";
                        }
                        if (string.IsNullOrEmpty(suiteQTY))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "套件数不能为空";
                        }
                        try
                        {
                            decimal _suiteQTY = decimal.Parse(suiteQTY);
                            if (_suiteQTY <= 0)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "套件数必须为大于零的数字";
                            }
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "套件数必须为大于零的数字";
                        }
                        //object objectPickDetail = this._WarehouseFacade.GetPickdetail(pickNo, Pickdetailmaterial.Pickline);
                        //PickDetail pickDetail = objectPickDetail as PickDetail;
                        //if (pickDetail.GFHWItemCode.Equals(gfHWItemCode) && pickDetail.GFPackingSeq.Equals(gfPackingSEQ))
                        //{

                        //}
                        //else
                        //{
                        //    this.DataProvider.RollbackTransaction();
                        //    return "该箱号对应选择的光伏华为编码、光伏包装序号不相等";
                        //}

                        ////4.	检查选择拣货任务令号、光伏华为编码、光伏包装序号在发货箱单明细物料信息表(TBLCartonInvDetailMaterial)中对应发货箱号条码(TBLCartonInvDetail.CARTONNO)的数量是否小于编辑区域输入的套件数，如小于则报错提示选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数
                        //if (this._WarehouseFacade.GetCartonNoCount(pickNo, gfHWItemCode, gfPackingSEQ) < decimal.Parse(suiteQTY))
                        //{
                        //    this.DataProvider.RollbackTransaction();
                        //    return "选择的拣货任务令号、光伏华为编码、光伏包装序号已包装的套件数已经大于等于输入套件数";
                        //}

                        //5>	新增发货箱单明细SN信息表(TBLCartonInvDetailSN)数据
                        Pickdetailmaterialsn pickdetailmaterialsn = objPickdetailmaterialsn as Pickdetailmaterialsn;
                        CARTONINVDETAILSN _CARTONINVDETAILSN = new CARTONINVDETAILSN();
                        _CARTONINVDETAILSN.CARINVNO = carInvNo;
                        _CARTONINVDETAILSN.PICKNO = pickNo;
                        _CARTONINVDETAILSN.PICKLINE = pickdetailmaterialsn.Pickline;
                        _CARTONINVDETAILSN.CARTONNO = cartonNo;
                        _CARTONINVDETAILSN.SN = pickdetailmaterialsn.Sn;
                        _CARTONINVDETAILSN.MUSER = mUser;
                        _CARTONINVDETAILSN.MDATE = mDate;
                        _CARTONINVDETAILSN.MTIME = mTime;
                        this._WarehouseFacade.AddCARTONINVDETAILSN(_CARTONINVDETAILSN);

                        //3>	检查包装箱号是否存在当前拣货任务令号对应发货箱单的发货箱单明细信息表(TBLCartonInvDetail)中，不存在则新增发货箱单明细信息表(TBLCartonInvDetail)数据
                        object objCartonInvDetail = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        if (objCartonInvDetail == null)
                        {
                            //箱是否存在--不存在
                            CartonInvDetail cartonInvDetail = new CartonInvDetail();
                            cartonInvDetail.CARINVNO = carInvNo;
                            cartonInvDetail.PICKNO = pickNo;
                            cartonInvDetail.STATUS = "Pack";
                            cartonInvDetail.CARTONNO = cartonNo;
                            cartonInvDetail.PACKMCODE = "";
                            cartonInvDetail.PACKQTY = 1;
                            cartonInvDetail.CUSER = mUser;
                            cartonInvDetail.CDATE = mDate;
                            cartonInvDetail.CTIME = mTime;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            cartonInvDetail.MUSER = mUser;
                            this._WarehouseFacade.AddCartonInvDetail(cartonInvDetail);

                            //this.DataProvider.CommitTransaction();

                            //  插入 cartonInvDetailMaterial
                            CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                            cartonInvDetailMaterial.CARINVNO = carInvNo;
                            cartonInvDetailMaterial.CARTONNO = cartonNo;
                            cartonInvDetailMaterial.CDATE = mDate;
                            cartonInvDetailMaterial.CTIME = mTime;
                            cartonInvDetailMaterial.CUSER = mUser;
                            cartonInvDetailMaterial.DQMCODE = dqMCode;
                            cartonInvDetailMaterial.DQSMCODE = dqsMCode;
                            cartonInvDetailMaterial.MCODE = mar.MCode;
                            cartonInvDetailMaterial.MDATE = mDate;
                            cartonInvDetailMaterial.MTIME = mTime;
                            cartonInvDetailMaterial.MUSER = mUser;
                            cartonInvDetailMaterial.PICKLINE = pikdetailsn.Pickline;
                            cartonInvDetailMaterial.PICKNO = pikdetailsn.Pickno;
                            cartonInvDetailMaterial.QTY = 1;
                            cartonInvDetailMaterial.UNIT = Pickdetailmaterial.Unit;
                            cartonInvDetailMaterial.GFHWITEMCODE = gfHWItemCode;
                            cartonInvDetailMaterial.GFPACKINGSEQ = gfPackingSEQ;
                            _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
                        }
                        else
                        {
                            object objCartonInvDetailMaterial = this._WarehouseFacade.QueryCartonInvDetailMaterial(carInvNo, cartonNo, dqMCode);
                            if (objCartonInvDetailMaterial == null)
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = new CartonInvDetailMaterial();
                                cartonInvDetailMaterial.CARINVNO = carInvNo;
                                cartonInvDetailMaterial.CARTONNO = cartonNo;
                                cartonInvDetailMaterial.CDATE = mDate;
                                cartonInvDetailMaterial.CTIME = mTime;
                                cartonInvDetailMaterial.CUSER = mUser;
                                cartonInvDetailMaterial.DQMCODE = dqMCode;
                                cartonInvDetailMaterial.DQSMCODE = dqsMCode;
                                cartonInvDetailMaterial.MCODE = mar.MCode;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.PICKLINE = pikdetailsn.Pickline;
                                cartonInvDetailMaterial.PICKNO = pikdetailsn.Pickno;
                                cartonInvDetailMaterial.QTY = 1;
                                cartonInvDetailMaterial.GFHWITEMCODE = gfHWItemCode;
                                cartonInvDetailMaterial.GFPACKINGSEQ = gfPackingSEQ;
                                cartonInvDetailMaterial.UNIT = Pickdetailmaterial.Unit;

                                _WarehouseFacade.AddCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }
                            else
                            {
                                CartonInvDetailMaterial cartonInvDetailMaterial = objCartonInvDetailMaterial as CartonInvDetailMaterial;
                                cartonInvDetailMaterial.QTY += 1;
                                cartonInvDetailMaterial.MUSER = mUser;
                                cartonInvDetailMaterial.MDATE = mDate;
                                cartonInvDetailMaterial.MTIME = mTime;
                                this._WarehouseFacade.UpdateCartonInvDetailMaterial(cartonInvDetailMaterial);
                            }
                            // 更新表CartonInvDetail的以包数量
                            CartonInvDetail cartonInvDetail = objCartonInvDetail as CartonInvDetail;
                            object[] objs_cartondetailmaterial = _WarehouseFacade.GetCartonInvDetailMaterial(carInvNo, cartonNo);
                            if (objs_cartondetailmaterial == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "更新发货箱出错";
                            }
                            double ss = 0;
                            for (int i = 0; i < objs_cartondetailmaterial.Length; i++)
                            {
                                CartonInvDetailMaterial cartondetailmar = objs_cartondetailmaterial[i] as CartonInvDetailMaterial;
                                ss += (double)cartondetailmar.QTY;
                            }
                            cartonInvDetail.PACKQTY = ss;
                            cartonInvDetail.MUSER = mUser;
                            cartonInvDetail.MDATE = mDate;
                            cartonInvDetail.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);



                        }
                        #endregion
                        #region //更新 pickdetailmaterial
                        Pickdetailmaterial.PQty += 1;
                        Pickdetailmaterial.MaintainDate = mDate;
                        Pickdetailmaterial.MaintainTime = mTime;
                        Pickdetailmaterial.MaintainUser = mUser;
                        _WarehouseFacade.UpdatePickdetailmaterial(Pickdetailmaterial);

                        #endregion
                        #region //更新 pickdetail
                        object[] pickdetailmaterial_obj = _WarehouseFacade.GetAllPickDetailMaterialByPickNoAndLine(pickNo, pikdetailsn.Pickline);
                        if (pickdetailmaterial_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "计算包装数量出错";
                        }
                        decimal sum = 0;
                        for (int i = 0; i < pickdetailmaterial_obj.Length; i++)
                        {
                            Pickdetailmaterial pickdetailmater = pickdetailmaterial_obj[i] as Pickdetailmaterial;
                            sum += pickdetailmater.PQty;
                        }
                        object pickdetail_obj = _WarehouseFacade.GetPickdetail(pickNo, pikdetailsn.Pickline);
                        if (pickdetail_obj == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "更新拣货明细表出错";
                        }
                        PickDetail pickdetail = pickdetail_obj as PickDetail;
                        pickdetail.PQTY = sum;
                        if (pickdetail.PQTY == pickdetail.SQTY)
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_ClosePack;
                        }
                        else
                        {
                            pickdetail.Status = PickDetail_STATUS.Status_Pack;
                        }
                        pickdetail.MaintainDate = mDate;
                        pickdetail.MaintainTime = mTime;
                        pickdetail.MaintainUser = mUser;

                        _WarehouseFacade.UpdatePickdetail(pickdetail);
                        #endregion

                        #region  箱包装完成，改变状态
                        object objCartonInvDetail1 = this._WarehouseFacade.GetCartonInvDetail(carInvNo, cartonNo);
                        CartonInvDetail car = objCartonInvDetail1 as CartonInvDetail;
                        if (pickdetail.SQTY == (decimal)car.PACKQTY)
                        {
                            car.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                            car.MUSER = mUser;
                            car.MDATE = mDate;
                            car.MTIME = mTime;
                            _WarehouseFacade.UpdateCartonInvDetail(car);


                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);
                        }
                        #endregion

                    }
                    else // 非单件管控
                    {
                        this.DataProvider.RollbackTransaction();
                        return "此物料为非单件管控，请输入数量按回车";
                    }
                    #endregion
                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                    return "请使用新箱";
                }

                object objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                if (objCARTONINVOICES == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "找不到拣货任务令号对应的发货箱单头信息";
                }
                CARTONINVOICES _CARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                if (_CARTONINVOICES.STATUS != CartonInvoices_STATUS.Status_Pack)
                {
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_Pack;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                //8>	检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] _objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (_objs != null)
                {
                    bool isTrue = true;
                    foreach (PickDetail _pickDetail in _objs)
                    {
                        if (_pickDetail.SQTY != _pickDetail.PQTY)
                        {
                            isTrue = false;
                            break;
                        }
                    }
                    if (isTrue)
                    {
                        object _objCARTONINVOICES = this._WarehouseFacade.GetTBLCartonInvoices(carInvNo);
                        if (_objCARTONINVOICES != null)
                        {
                            CARTONINVOICES cARTONINVOICES = objCARTONINVOICES as CARTONINVOICES;
                            cARTONINVOICES.STATUS = "ClosePack";
                            cARTONINVOICES.FDATE = mDate;
                            cARTONINVOICES.FTIME = mTime;
                            cARTONINVOICES.MUSER = mUser;
                            cARTONINVOICES.MDATE = mDate;
                            cARTONINVOICES.MTIME = mTime;
                            this._WarehouseFacade.UpdateTBLCartonInvoices(cARTONINVOICES);


                            Pick pick = (Pick)this._InventoryFacade.GetPick(pickNo);
                            InvInOutTrans trans = _WarehouseFacade.CreateNewInvInOutTrans();
                            trans.CartonNO = string.Empty;
                            trans.DqMCode = " ";
                            trans.FacCode = string.Empty;
                            trans.FromFacCode = string.Empty;
                            trans.FromStorageCode = string.Empty;
                            trans.InvNO = (pick as Pick).InvNo;//.InvNo;
                            trans.InvType = (pick as Pick).PickType;
                            trans.LotNo = string.Empty;
                            trans.MaintainDate = mDate;
                            trans.MaintainTime = mTime;
                            trans.MaintainUser = userCode;
                            trans.MCode = " ";
                            trans.ProductionDate = 0;
                            trans.Qty = 0;
                            trans.Serial = 0;
                            trans.StorageAgeDate = 0;
                            trans.StorageCode = pick.StorageCode;
                            trans.SupplierLotNo = string.Empty;
                            trans.TransNO = pickNo;
                            trans.TransType = "OUT";
                            trans.Unit = string.Empty;
                            trans.ProcessType = "ClosePack";
                            _WarehouseFacade.AddInvInOutTrans(trans);
                        }
                    }
                }

                this.DataProvider.CommitTransaction();
                return "操作成功";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "操作失败：" + ex.Message;
            }
        }

        //提交
        [WebMethod(EnableSession = true)]
        public string GFSubmitReturnMessage(string pickNo, string cartonNo, string userCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            object obj = this._WarehouseFacade.GetCartonInvoices(pickNo);
            if (obj == null)
            {
                return "当前拣货任务令号没有对应的发货箱单信息";
            }

            try
            {
                this.DataProvider.BeginTransaction();

                //1. 当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
                object _obj = this._WarehouseFacade.GetCartonInvDetail((obj as CARTONINVOICES).CARINVNO, cartonNo);
                if (_obj == null)
                {
                    return "当前包装箱号没有对应的发货箱单明细信息";
                }

                CartonInvDetail cartonInvDetail = _obj as CartonInvDetail;
                cartonInvDetail.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                cartonInvDetail.MUSER = mUser;
                cartonInvDetail.MDATE = mDate;
                cartonInvDetail.MTIME = mTime;
                this._WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                //2. 检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] objs = this._WarehouseFacade.QueryPickDetail(pickNo);
                if (objs == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "当前拣货任务令号没有对应的拣货任务令明细信息";
                }

                bool isTrue = true;
                foreach (PickDetail pickDetail in objs)
                {
                    if (pickDetail.SQTY != pickDetail.PQTY)
                    {
                        isTrue = false;
                        break;
                    }
                }
                if (isTrue)
                {
                    CARTONINVOICES _CARTONINVOICES = obj as CARTONINVOICES;
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                    _CARTONINVOICES.FDATE = mDate;
                    _CARTONINVOICES.FTIME = mTime;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }

                foreach (PickDetail pickDetail in objs)
                {
                    pickDetail.Status = PickDetail_STATUS.Status_ClosePack;
                    pickDetail.MaintainUser = mUser;
                    pickDetail.MaintainDate = mDate;
                    pickDetail.MaintainTime = mTime;
                    _WarehouseFacade.UpdatePickdetail(pickDetail);
                }

                this.DataProvider.CommitTransaction();
                return "提交成功";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "提交失败：" + ex.Message;
            }
        }
        #endregion

        #region 转储作业
        [WebMethod(EnableSession = true)]
        public string[] QueryTransNo(string Status, string user)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }



            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            UserFacade userFacade = new UserFacade(this.DataProvider);
            string[] usergroupList = userFacade.GetUserGroupCodeofUser1(user);

            return this._WarehouseFacade.QueryTransNo(Status, usergroupList);
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetStorLocTransOperationsDataGrid(string transNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.QueryPDAStorLocTransOperations(transNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("鼎桥物料编码", typeof(string));
                dt.Columns.Add("华为物料号", typeof(string));
                dt.Columns.Add("源货位", typeof(string));
                dt.Columns.Add("需求数量", typeof(string));
                dt.Columns.Add("已移数量", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add("", row["DQMCODE"], row["CUSTMCODE"], row["FromLocationCode"], row["QTY"], row["SQTY"]);
                }
            }
            return dt;
        }

        //检查判断目标货位是否属于转储单对应的目标库位
        [WebMethod(EnableSession = true)]
        public bool IsBelong(string transNo, string locationCode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            object objLocation = this._InventoryFacade.GetLocation(locationCode, 1);
            Location Location = objLocation as Location;
            object obj_Storloctrans = this._WarehouseFacade.GetStorloctrans(transNo);

            Storloctrans _Storloctrans = obj_Storloctrans as Storloctrans;
            if (_Storloctrans == null)
                return false;
            if (Location == null)
                return false;
            if (Location.StorageCode.Equals(_Storloctrans.StorageCode))
            {
                return true;
            }
            return false;
        }

        //原箱号：后台获取其库位和货位（TBLStorageDetail），查找条件：根据箱号到TBLStorageDetail中查找库存信息；若库位和出库库位（TBLStorLocTrans. FromStorageCode）不符则报错。
        [WebMethod(EnableSession = true)]
        public bool IsCompliance(string transNo, string fromCartonNo)
        {
            if (_WarehouseFacade == null)
                _WarehouseFacade = new WarehouseFacade(DataProvider);


            if (_InventoryFacade == null)
                _InventoryFacade = new InventoryFacade(DataProvider);

            object obj = this._InventoryFacade.GetStorageDetail(fromCartonNo);
            StorageDetail storageDetail = obj as StorageDetail;
            object obj_Storloctrans = this._WarehouseFacade.GetStorloctrans(transNo);
            Storloctrans _Storloctrans = obj_Storloctrans as Storloctrans;
            if (storageDetail == null)
                return false;
            if (_Storloctrans == null)
                return false;
            if (storageDetail.StorageCode.Equals(_Storloctrans.FromstorageCode))
            {
                return true;
            }
            return false;
        }

        //提交
        [WebMethod(EnableSession = true)]
        public string StorLocTransOperationsSubmitReturnMessage(string transNo, string type, string locationCode, string fromCartonNo, string qty, string sn, string tLocationCartonNo, string userCode)
        {
            if (_InventoryFacade == null)
            {
                _InventoryFacade = new InventoryFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            if (string.IsNullOrEmpty(transNo))
                return "转储单号不能为空！";
            if (string.IsNullOrEmpty(locationCode))
                return "目标货位不能为空！";
            if (string.IsNullOrEmpty(fromCartonNo))
                return "原箱号不能为空！";

            if (string.IsNullOrEmpty(tLocationCartonNo))
                return "目标箱号不能为空！";


            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;

            try
            {
                DataProvider.BeginTransaction();



                object objLocation = this._InventoryFacade.GetLocation(locationCode, 1);
                if (objLocation == null)
                {
                    DataProvider.RollbackTransaction();
                    return locationCode + "不存在！";
                }
                Location Location = objLocation as Location;
                object obj_Storloctrans = this._WarehouseFacade.GetStorloctrans(transNo);
                Storloctrans _Storloctrans = obj_Storloctrans as Storloctrans;
                if (!Location.StorageCode.Equals(_Storloctrans.StorageCode))
                {
                    this.DataProvider.RollbackTransaction();
                    return "目标货位对应的目标库位不属于转储单对应的目标库位";
                }

                //原箱号：后台获取其库位和货位（TBLStorageDetail），查找条件：根据箱号到TBLStorageDetail中查找库存信息；若库位和出库库位（TBLStorLocTrans. FromStorageCode）不符则报错。
                object obj = this._InventoryFacade.GetStorageDetail(fromCartonNo);
                if (obj == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "库存明细信息表里没有对应箱号的数据";
                }
                StorageDetail storageDetail = obj as StorageDetail;
                if (!storageDetail.StorageCode.Equals(_Storloctrans.FromstorageCode))
                {
                    this.DataProvider.RollbackTransaction();
                    return "原箱号对应的库位和出库库位不符";
                }

                string dqmCodeHave = string.Empty;
                if (_WarehouseFacade.IsCartonnoHaveOtherDQMCode(tLocationCartonNo, transNo, storageDetail.DQMCode, out dqmCodeHave))
                {
                    this.DataProvider.RollbackTransaction();
                    return "此箱号已经含有物料:" + dqmCodeHave;
                }
                //一 根据箱号到TBLStorageDetail查找mcode，根据（转单号，mcode）查找TBLStorLocTransDetail 中数据，如果没有则报错：转储单中没有对应的SAP物料号
                object _obj = this._WarehouseFacade.GetStorloctransdetail(transNo, storageDetail.MCode);
                if (_obj == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "转储单中没有对应的SAP物料号";
                }
                StorloctransDetail storloctransDetail = _obj as StorloctransDetail;

                //二  判断（CartonNO对应的MCode），（TransNo）在TBLStorLocTransDetail中的状态是否为Close:完成，如果是提示该料转储已经完成，如果是Release（初始化），更新状态为（Pick，捡料中） 。
                if (storloctransDetail.Status == "Close")
                {
                    this.DataProvider.RollbackTransaction();
                    return "该料转储已经完成";
                }
                if (storloctransDetail.Status == "Release")
                {
                    storloctransDetail.Status = "Pick";
                    storloctransDetail.MaintainUser = mUser;
                    storloctransDetail.MaintainDate = mDate;
                    storloctransDetail.MaintainTime = mTime;
                    this._WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);
                }

                //if (this.rdbIntegrateCarton.Checked)
                if (type == "AllCarton")
                {
                    #region 整箱
                    //1，	检查TBLStorageDetail.FreezeQTY是否为零？不为零，检查是否TBLStorageDetail.FreezeQTY= TBLStorageDetail. STORAGEQTY?如果是：提示此箱在捡料中；如果不是提示此箱SN部分捡料中，请拆箱捡料。
                    if (storageDetail.FreezeQty != 0)
                    {
                        if (storageDetail.FreezeQty == storageDetail.StorageQty)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "此箱在捡料中";
                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            return "此箱SN部分捡料中，请拆箱捡料";
                        }
                    }

                    //2，	更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. STORAGEQTY，TBLStorageDetail. AvailableQTY=0。
                    storageDetail.FreezeQty = storageDetail.StorageQty;
                    storageDetail.AvailableQty = 0;

                    storageDetail.MaintainUser = mUser;
                    storageDetail.MaintainDate = mDate;
                    storageDetail.MaintainTime = mTime;
                    this._InventoryFacade.UpdateStorageDetail(storageDetail);

                    //3，	更新TBLStorageDetailSN. PICKBLOCK=Y。
                    object[] objs = this._WarehouseFacade.GetStorageDetailSnbyCartonNo(fromCartonNo);
                    if (objs != null)
                    {
                        foreach (StorageDetailSN storageDetailSN in objs)
                        {
                            storageDetailSN.PickBlock = "Y";
                            storageDetailSN.MaintainUser = mUser;
                            storageDetailSN.MaintainDate = mDate;
                            storageDetailSN.MaintainTime = mTime;
                            this._InventoryFacade.UpdateStorageDetailSN(storageDetailSN);
                        }
                    }

                    //4，	查找目标箱号在表TBLStorLocTransDetailCarton是否有记录， 如果有记录，检查记录中的料号是否有与原箱的料号一致，如果不一致则提示：（目标箱号物料不一致）
                    object objs_StorloctransDetailCarton = this._WarehouseFacade.GetStorloctransdetailcarton(transNo, fromCartonNo, tLocationCartonNo);
                    if (objs_StorloctransDetailCarton != null)
                    {
                        StorloctransDetailCarton _storloctransDetailCarton = (StorloctransDetailCarton)objs_StorloctransDetailCarton;


                        if (_storloctransDetailCarton.MCode.Equals((storageDetail.MCode)))
                        {
                            _storloctransDetailCarton.Qty += storageDetail.StorageQty;
                            _storloctransDetailCarton.MaintainUser = mUser;
                            _storloctransDetailCarton.MaintainDate = mDate;
                            _storloctransDetailCarton.MaintainTime = mTime;
                            this._WarehouseFacade.UpdateStorloctransdetailcarton(_storloctransDetailCarton);
                        }
                        else
                        {
                            this.DataProvider.RollbackTransaction();
                            return "目标箱号物料不一致";
                        }

                    }
                    else
                    {

                        StorloctransDetailCarton storloctransDetailCarton = new StorloctransDetailCarton();
                        storloctransDetailCarton.Transno = transNo;
                        storloctransDetailCarton.MCode = storloctransDetail.MCode;
                        storloctransDetailCarton.DqmCode = storageDetail.DQMCode;
                        storloctransDetailCarton.MDesc = storageDetail.MDesc;
                        storloctransDetailCarton.Unit = storageDetail.Unit;
                        storloctransDetailCarton.Supplier_lotno = storageDetail.SupplierLotNo;
                        storloctransDetailCarton.Production_Date = storageDetail.ProductionDate;
                        storloctransDetailCarton.StorageageDate = storageDetail.StorageAgeDate;
                        storloctransDetailCarton.LaststorageageDate = storageDetail.LastStorageAgeDate;
                        storloctransDetailCarton.ValidStartDate = storageDetail.ValidStartDate;
                        storloctransDetailCarton.FacCode = storageDetail.FacCode;
                        storloctransDetailCarton.Qty = storageDetail.StorageQty;
                        storloctransDetailCarton.LocationCode = locationCode;
                        storloctransDetailCarton.Cartonno = tLocationCartonNo;
                        storloctransDetailCarton.FromlocationCode = storageDetail.LocationCode;
                        storloctransDetailCarton.Fromcartonno = storageDetail.CartonNo;
                        storloctransDetailCarton.Lotno = storageDetail.Lotno;
                        storloctransDetailCarton.CUser = mUser;
                        storloctransDetailCarton.CDate = mDate;
                        storloctransDetailCarton.CTime = mTime;
                        storloctransDetailCarton.MaintainUser = mUser;
                        storloctransDetailCarton.MaintainDate = mDate;
                        storloctransDetailCarton.MaintainTime = mTime;
                        this._WarehouseFacade.AddStorloctransdetailcarton(storloctransDetailCarton);
                    }

                    //5，	如果没有向TBLStorLocTransDetailCarton插入一笔数据。
                    if (!this._WarehouseFacade.IsExistLocationCode(locationCode))
                    {
                        this.DataProvider.RollbackTransaction();
                        return "输入的目标货位不存在";
                    }


                    //6，	如果原箱号在TBLStorageDetailSN有SN信息，将SN信息插入到TBLStorLocTransDetailSN表
                    StorloctransDetailSN storloctransDetailSN = new StorloctransDetailSN();
                    if (objs != null)
                    {
                        foreach (StorageDetailSN storageDetailSN in objs)
                        {
                            storloctransDetailSN.Transno = transNo;
                            storloctransDetailSN.Cartonno = tLocationCartonNo;
                            storloctransDetailSN.Fromcartonno = fromCartonNo;
                            storloctransDetailSN.Sn = storageDetailSN.SN;
                            storloctransDetailSN.MaintainUser = mUser;
                            storloctransDetailSN.MaintainDate = mDate;
                            storloctransDetailSN.MaintainTime = mTime;
                            this._WarehouseFacade.AddStorloctransdetailsn(storloctransDetailSN);
                        }
                    }
                    #endregion
                }
                else
                {

                    if (fromCartonNo == tLocationCartonNo)
                    {
                        this.DataProvider.RollbackTransaction();
                        return "拆箱目标箱号不能和原箱号相同";

                    }



                    #region 拆箱
                    //1，	无论输入是【原箱号】，【SN】，首先判断管控类型，如果是单件管控，已输入SN为条件进行录入；如果是批管控或不管控则以【原箱号】和【数量】为录入条件。
                    StorageDetail[] storageDetails = this._WarehouseFacade.GetStorageDetailsFromCARTONNO(fromCartonNo);
                    if (storageDetails == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        return "原箱号无库存信息";
                    }

                    object[] mar_objs = _WarehouseFacade.GetMaterialInfoByQDMCode(storageDetails[0].DQMCode);
                    if (mar_objs == null)
                    {
                        this.DataProvider.RollbackTransaction();
                        return "原箱号物料信息不存在";
                    }

                    StorageDetail tStorageDetail = (StorageDetail)_InventoryFacade.GetStorageDetail(tLocationCartonNo);

                    if (tStorageDetail != null)
                    {
                        if (tStorageDetail.DQMCode != storageDetail.DQMCode)
                        {

                            this.DataProvider.RollbackTransaction();
                            return "原箱与目标箱的物料必须相同！";


                        }
                    }
                    if (tStorageDetail != null)
                    {
                        if (_Storloctrans.StorageCode != tStorageDetail.StorageCode)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "目标箱库位必须与转储单的目标库位相同！";

                        }
                    }


                    Domain.MOModel.Material mar = mar_objs[0] as Domain.MOModel.Material;
                    if (mar.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)  //单件管控
                    {
                        #region 单件管控
                        if (string.IsNullOrEmpty(sn))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "必须输入SN号码";
                        }
                        //A 根据SN在TBLStorageDetail查看是否TBLStorageDetail.FreezeQTY= TBLStorageDetail. STORAGEQTY？如果等于，提示：此箱在捡料中；如果不等于0，检查TBLStorageDetailSN. PICKBLOCK是否是Y，如果是提示：此SN正在捡料中。
                        object objStorageDetailSN = this._InventoryFacade.GetStorageDetailSN(sn);
                        if (objStorageDetailSN == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "刷入SN条码不存在";
                        }
                        StorageDetailSN storageDetailSN = objStorageDetailSN as StorageDetailSN;
                        object objStorageDetail = this._InventoryFacade.GetStorageDetail(storageDetailSN.CartonNo);
                        if (objStorageDetail == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "输入的SN找不到库存信息";
                        }
                        StorageDetail _storageDetail = objStorageDetail as StorageDetail;
                        if (_storageDetail.FreezeQty == _storageDetail.StorageQty)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "此箱在捡料中";
                        }
                        else
                        {
                            if (storageDetailSN.PickBlock == "Y")
                            {
                                this.DataProvider.RollbackTransaction();
                                return "此SN正在捡料中";
                            }
                        }

                        //B 根据SN在TBLStorageDetail中查找mcode信息，再根据（mcode+TransNo）信息在TBLStorLocTransDetail中是否有数据，如果没有则报错：转储单中没有对应的SAP物料号。
                        object objStorLocTransDetail = this._WarehouseFacade.GetStorloctransdetail(transNo, _storageDetail.MCode);
                        if (objStorLocTransDetail == null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "转储单中没有对应的SAP物料号";
                        }
                        StorloctransDetail _storloctransDetail = objStorLocTransDetail as StorloctransDetail;

                        //C 更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. FreezeQTY+1，TBLStorageDetail. AvailableQTY= TBLStorageDetail.STORAGEQTY-TBLStorageDetail. FreezeQTY。
                        _storageDetail.FreezeQty += 1;
                        _storageDetail.AvailableQty = _storageDetail.StorageQty - _storageDetail.FreezeQty;
                        _storageDetail.MaintainUser = mUser;
                        _storageDetail.MaintainDate = mDate;
                        _storageDetail.MaintainTime = mTime;
                        this._InventoryFacade.UpdateStorageDetail(_storageDetail);

                        //D 更新TBLStorageDetailSN. PICKBLOCK=Y。
                        storageDetailSN.PickBlock = "Y";
                        storageDetailSN.MaintainUser = mUser;
                        storageDetailSN.MaintainDate = mDate;
                        storageDetailSN.MaintainTime = mTime;
                        this._InventoryFacade.UpdateStorageDetailSN(storageDetailSN);

                        //E  需要输入目标箱号，根据SN信息在TBLStorageDetailSN中查找原箱号信息；
                        //根据（目标箱号+TransNo+FromCARTONNO原箱号）在TBLStorLocTransDetailCarton是否有记录。
                        object objStorLocTransDetailCarton = this._WarehouseFacade.GetStorloctransdetailcarton(transNo, storageDetailSN.CartonNo, tLocationCartonNo);
                        if (objStorLocTransDetailCarton == null)
                        {
                            //如果没有：插入一条数据
                            if (!this._WarehouseFacade.IsExistLocationCode(locationCode))
                            {
                                this.DataProvider.RollbackTransaction();
                                return "输入的目标货位不存在";
                            }
                            StorloctransDetailCarton storloctransDetailCarton = new StorloctransDetailCarton();
                            storloctransDetailCarton.Transno = transNo;
                            storloctransDetailCarton.MCode = _storloctransDetail.MCode;
                            storloctransDetailCarton.DqmCode = _storageDetail.DQMCode;
                            storloctransDetailCarton.MDesc = _storageDetail.MDesc;
                            storloctransDetailCarton.Unit = _storageDetail.Unit;
                            storloctransDetailCarton.Supplier_lotno = _storageDetail.SupplierLotNo;
                            storloctransDetailCarton.Production_Date = _storageDetail.ProductionDate;
                            storloctransDetailCarton.StorageageDate = _storageDetail.StorageAgeDate;
                            storloctransDetailCarton.LaststorageageDate = _storageDetail.LastStorageAgeDate;
                            storloctransDetailCarton.ValidStartDate = _storageDetail.ValidStartDate;
                            storloctransDetailCarton.FacCode = _storageDetail.FacCode;
                            storloctransDetailCarton.Qty = 1;
                            storloctransDetailCarton.LocationCode = locationCode;
                            storloctransDetailCarton.Cartonno = tLocationCartonNo;
                            storloctransDetailCarton.FromlocationCode = _storageDetail.LocationCode;
                            storloctransDetailCarton.Fromcartonno = _storageDetail.CartonNo;
                            storloctransDetailCarton.Lotno = _storageDetail.Lotno;
                            storloctransDetailCarton.CUser = mUser;
                            storloctransDetailCarton.CDate = mDate;
                            storloctransDetailCarton.CTime = mTime;
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            this._WarehouseFacade.AddStorloctransdetailcarton(storloctransDetailCarton);
                        }
                        else
                        {
                            //如果有记录，检查记录中的料号是否有与原箱的料号一致，如果不一致则提示：（目标箱号物料不一致）
                            StorloctransDetailCarton storloctransDetailCarton = objStorLocTransDetailCarton as StorloctransDetailCarton;
                            object _objStorageDetail = this._InventoryFacade.GetStorageDetail(fromCartonNo);
                            if (!storloctransDetailCarton.MCode.Equals((_objStorageDetail as StorageDetail).MCode))
                            {
                                this.DataProvider.RollbackTransaction();
                                return "目标箱号物料不一致";
                            }

                            //如果有： 更新TBLStorLocTransDetailCarton.QTY+1，MDate，MTime，MUser。
                            storloctransDetailCarton.Qty += 1;
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            this._WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                        }

                        //F  向表TBLStorLocTransDetailSN插入一条数据。
                        StorloctransDetailSN storloctransDetailSN = new StorloctransDetailSN();
                        storloctransDetailSN.Transno = transNo;
                        storloctransDetailSN.Cartonno = tLocationCartonNo;
                        storloctransDetailSN.Fromcartonno = fromCartonNo;
                        storloctransDetailSN.Sn = storageDetailSN.SN;
                        storloctransDetailSN.MaintainUser = mUser;
                        storloctransDetailSN.MaintainDate = mDate;
                        storloctransDetailSN.MaintainTime = mTime;
                        this._WarehouseFacade.AddStorloctransdetailsn(storloctransDetailSN);
                        #endregion
                    }
                    else
                    {
                        #region 非单件管控
                        if (string.IsNullOrEmpty(qty))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "必须输入数量";
                        }
                        //判断数量是否是数字格式
                        try
                        {
                            decimal _qty = decimal.Parse(qty);
                            if (_qty <= 0)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "数量必须为大于零的数字";
                            }
                        }
                        catch (Exception ex)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "数量必须为大于零的数字";
                        }

                        //B 检查TBLStorageDetail. AvailableQTY>PDA页面填的数量。如果否，提示：输入的数量大于库存可用数量。
                        StorageDetail _storageDetail = storageDetails[0] as StorageDetail;
                        if (_storageDetail.AvailableQty <= decimal.Parse(qty))
                        {
                            this.DataProvider.RollbackTransaction();
                            return "输入的数量大于库存可用数量";
                        }

                        //C 更新TBLStorageDetail. FreezeQTY= TBLStorageDetail. FreezeQTY+ PDA页面填的数量，TBLStorageDetail. AvailableQTY= TBLStorageDetail.STORAGEQTY-TBLStorageDetail. FreezeQTY。
                        _storageDetail.FreezeQty += int.Parse(qty);
                        _storageDetail.AvailableQty = _storageDetail.StorageQty - _storageDetail.FreezeQty;
                        _storageDetail.MaintainUser = mUser;
                        _storageDetail.MaintainDate = mDate;
                        _storageDetail.MaintainTime = mTime;
                        this._InventoryFacade.UpdateStorageDetail(_storageDetail);

                        //D需要输入目标箱号，根据（目标箱号+TransNo+FromCARTONNO原箱号）在TBLStorLocTransDetailCarton是否有记录。
                        object objStorLocTransDetailCarton = this._WarehouseFacade.GetStorloctransdetailcarton(transNo, _storageDetail.CartonNo, tLocationCartonNo);
                        if (objStorLocTransDetailCarton == null)
                        {
                            //如果没有：插入一条数据
                            if (!this._WarehouseFacade.IsExistLocationCode(locationCode))
                            {
                                this.DataProvider.RollbackTransaction();
                                return "输入的目标货位不存在";
                            }
                            StorloctransDetailCarton storloctransDetailCarton = new StorloctransDetailCarton();
                            storloctransDetailCarton.Transno = transNo;
                            storloctransDetailCarton.MCode = storloctransDetail.MCode;
                            storloctransDetailCarton.DqmCode = _storageDetail.DQMCode;
                            storloctransDetailCarton.MDesc = _storageDetail.MDesc;
                            storloctransDetailCarton.Unit = _storageDetail.Unit;
                            storloctransDetailCarton.Supplier_lotno = _storageDetail.SupplierLotNo;
                            storloctransDetailCarton.Production_Date = _storageDetail.ProductionDate;
                            storloctransDetailCarton.StorageageDate = _storageDetail.StorageAgeDate;
                            storloctransDetailCarton.LaststorageageDate = _storageDetail.LastStorageAgeDate;
                            storloctransDetailCarton.ValidStartDate = _storageDetail.ValidStartDate;
                            storloctransDetailCarton.FacCode = _storageDetail.FacCode;
                            storloctransDetailCarton.Qty = decimal.Parse(qty);
                            storloctransDetailCarton.LocationCode = locationCode;
                            storloctransDetailCarton.Cartonno = tLocationCartonNo;
                            storloctransDetailCarton.FromlocationCode = _storageDetail.LocationCode;
                            storloctransDetailCarton.Fromcartonno = _storageDetail.CartonNo;
                            storloctransDetailCarton.Lotno = _storageDetail.Lotno;
                            storloctransDetailCarton.CUser = mUser;
                            storloctransDetailCarton.CDate = mDate;
                            storloctransDetailCarton.CTime = mTime;
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            this._WarehouseFacade.AddStorloctransdetailcarton(storloctransDetailCarton);
                        }
                        else
                        {
                            //如果有记录，检查记录中的料号是否有与原箱的料号一致，如果不一致则提示：（目标箱号物料不一致）
                            StorloctransDetailCarton storloctransDetailCarton = objStorLocTransDetailCarton as StorloctransDetailCarton;
                            object _objStorageDetail = this._InventoryFacade.GetStorageDetail(fromCartonNo);
                            if (!storloctransDetailCarton.MCode.Equals((_objStorageDetail as StorageDetail).MCode))
                            {
                                this.DataProvider.RollbackTransaction();
                                return "目标箱号物料不一致";
                            }

                            //如果有： 更新TBLStorLocTransDetailCarton.QTY+ PDA页面填的数量，MDate，MTime，MUser。
                            storloctransDetailCarton.Qty += decimal.Parse(qty);
                            storloctransDetailCarton.MaintainUser = mUser;
                            storloctransDetailCarton.MaintainDate = mDate;
                            storloctransDetailCarton.MaintainTime = mTime;
                            this._WarehouseFacade.UpdateStorloctransdetailcarton(storloctransDetailCarton);
                        }
                        #endregion
                    }
                    #endregion
                }

                //三 判断条件TransNo,MCODE 下sum（TBLStorLocTransDetailCarton.QTY）是否等于需求数量TBLStorLocTransDetail.QTY。如果等于则更新装填TBLStorLocTransDetail.Status=Close:完成状态。
                decimal sum = 0;
                object[] _objs = this._WarehouseFacade.GetStorloctransdetailcarton(transNo, storageDetail.MCode);
                foreach (StorloctransDetailCarton storloctransDetailCarton in _objs)
                {
                    sum += storloctransDetailCarton.Qty;
                }
                if (sum == storloctransDetail.Qty)
                {
                    storloctransDetail.Status = "Close";
                    storloctransDetail.MaintainUser = mUser;
                    storloctransDetail.MaintainDate = mDate;
                    storloctransDetail.MaintainTime = mTime;
                    this._WarehouseFacade.UpdateStorloctransdetail(storloctransDetail);
                }
                else if (sum > storloctransDetail.Qty)
                {
                    DataProvider.RollbackTransaction();
                    return "数量超出转储单需求数量";
                }



                this.DataProvider.CommitTransaction();
                return "提交成功";
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "提交失败：" + ex.Message;
            }
        }
        #endregion

        #region IQC
        [WebMethod(EnableSession = true)]
        public string[] QueryIQCNo()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryIQCNo1();
        }

        [WebMethod(EnableSession = true)]
        public DataTable QueryAQLStandard()
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            return this._WarehouseFacade.QueryAQLStandard();
        }

        [WebMethod(EnableSession = true)]
        public string QueryAQLStr(string IQCNO)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            _IQCFacade = new IQCFacade(DataProvider);
            object obj_iqc = _IQCFacade.GetAsnIQC(IQCNO);
            AsnIQC iqc = obj_iqc as AsnIQC;

            return iqc.AQLLevel;
        }

        [WebMethod(EnableSession = true)]
        public DataTable QueryAQLStandard1(string IQCNO)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }

            _IQCFacade = new IQCFacade(DataProvider);
            object obj_iqc = _IQCFacade.GetAsnIQC(IQCNO);
            AsnIQC iqc = obj_iqc as AsnIQC;
            object[] obj_sample = _IQCFacade.GetSampleQTYByIqcQTY1(iqc.Qty);

            return this._WarehouseFacade.QueryAQLStandard1(iqc.Qty);
        }

        [WebMethod(EnableSession = true)]
        public string GetAQLStr(int seq, string aqlLevel)
        {
            if (_OQCFacade == null)
            {
                _OQCFacade = new OQCFacade(DataProvider);
            }
            object obj = this._OQCFacade.GetAQL(seq, aqlLevel);
            AQL aql = obj as AQL;
            return aql.AqlLevelDesc + "," + aql.SampleSize + "," + aql.RejectSize;
        }

        [WebMethod(EnableSession = true)]
        public string GetAQLLevel(string _IQCNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            object obj = this._IQCFacade.GetAsnIQC(_IQCNo);
            if (obj == null)
            {
                return string.Empty;
            }
            object _obj = this._IQCFacade.GetSampleQTYByIqcQTY((obj as AsnIQC).Qty);
            if (_obj == null)
            {
                return string.Empty;
            }
            return (_obj as AQL).AqlLevel;
        }

        [WebMethod(EnableSession = true)]
        public string GetAQLStrByLevel(string aqlLevel)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            object obj = this._WarehouseFacade.GetAQLByLevel(aqlLevel);
            AQL aql = obj as AQL;
            return aql.AqlLevelDesc + "," + aql.SampleSize + "," + aql.RejectSize;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetIQCCheckResultMPDataGrid(string iqcNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.GetIQCCheckResultMPDataGrid(iqcNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("箱号编码", typeof(string));
                dt.Columns.Add("送检数量", typeof(string));
                dt.Columns.Add("AQL结果", typeof(string));
                dt.Columns.Add("鼎桥料号", typeof(string));
                dt.Columns.Add("缺陷品数", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    if (row["IQCTYPE"].ToString() == "SpotCheck")
                    {
                        dt.Rows.Add("", row["CARTONNO"], row["QTY"], row["QCSTATUS"], row["DQMCODE"], row["NGQTY"]);
                    }
                    else
                    {
                        dt.Rows.Add("", row["CARTONNO"], row["QTY"], string.Empty, row["DQMCODE"], row["NGQTY"]);
                    }
                }
            }
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public int[] GetASN(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            object obj = this._IQCFacade.GetAsnIQC(iqcNo);
            if (obj != null)
            {
                if (_InventoryFacade == null)
                {
                    _InventoryFacade = new InventoryFacade(DataProvider);
                }
                ASN asn = (ASN)_InventoryFacade.GetASN((obj as AsnIQC).StNo);
                int[] str = new int[3];
                if (asn != null)
                {
                    str[0] = asn.InitRejectQty;
                    str[1] = asn.InitReceiveQty;
                    str[2] = asn.InitGiveInQty;
                    return str;
                }
            }
            return null;
        }

        [WebMethod(EnableSession = true)]
        public string IQCCheckResultMPSubmitReturnMessage(string _IQCNo, bool checkType, string aqlLevelAndSeq, string rejectNumStr, string user)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            _InventoryFacade = new InventoryFacade(DataProvider);

            ShareLib.ShareKit kit = new ShareLib.ShareKit();
            string checkTypeStr = string.Empty;
            if (checkType)
                checkTypeStr = OQCType.OQCType_FullCheck;
            else
                checkTypeStr = OQCType.OQCType_SpotCheck;
            string message = string.Empty;
            bool result = kit.IQCSubmit(_IQCNo, DataProvider, user, checkTypeStr, aqlLevelAndSeq, rejectNumStr, out message);


            return message;

        }

        //免检
        /// <summary>
        /// 免检
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        private void ToSTS(string iqcNo, bool checkType)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            _InventoryFacade = new InventoryFacade(DataProvider);

            //1、更新送检单TBLASNIQC
            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                asnIqc.IqcType = checkType ? "FullCheck" : "SpotCheck";
                asnIqc.Status = IQCStatus.IQCStatus_IQCClose;
                asnIqc.QcStatus = "Y";
                _IQCFacade.UpdateAsnIQC(asnIqc);

                #region 在invinouttrans表中增加一条数据
                WarehouseFacade facade = new WarehouseFacade(this.DataProvider);

                //ASN asn = (ASN)domainObject;
                DBDateTime dbTime1 = FormatHelper.GetNowDBDateTime(this.DataProvider);
                InvInOutTrans trans = facade.CreateNewInvInOutTrans();
                trans.CartonNO = string.Empty;
                trans.DqMCode = asnIqc.DQMCode;
                trans.FacCode = string.Empty;
                trans.FromFacCode = string.Empty;
                trans.FromStorageCode = string.Empty;
                trans.InvNO = asnIqc.InvNo;
                trans.InvType = asnIqc.IqcType;
                trans.LotNo = string.Empty;
                trans.MaintainDate = dbTime1.DBDate;
                trans.MaintainTime = dbTime1.DBTime;
                trans.MaintainUser = "PDA";
                trans.MCode = asnIqc.MCode;
                trans.ProductionDate = 0;
                trans.Qty = asnIqc.Qty;
                trans.Serial = 0;
                trans.StorageAgeDate = 0;
                trans.StorageCode = string.Empty;
                trans.SupplierLotNo = string.Empty;
                trans.TransNO = asnIqc.IqcNo;
                trans.TransType = "IN";
                trans.Unit = string.Empty;
                trans.ProcessType = "IQC";
                facade.AddInvInOutTrans(trans);
                #endregion
            }

            object[] objAsnIqcDetail = _IQCFacade.GetAsnIQCDetailByIqcNo(iqcNo);
            if (objAsnIqcDetail != null)
            {
                foreach (AsnIQCDetail asnIqcDetail in objAsnIqcDetail)
                {
                    //2、更新送检单明细TBLASNIQCDETAIL
                    asnIqcDetail.QcPassQty = asnIqcDetail.Qty;
                    asnIqcDetail.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIQCDetail(asnIqcDetail);


                    //4、更新ASN明细TBLASNDETAIL
                    ASNDetail asnDetail = (ASNDetail)_InventoryFacade.GetASNDetail(Convert.ToInt32(asnIqcDetail.StLine), asnIqcDetail.StNo);
                    if (asnDetail != null)
                    {
                        asnDetail.QcPassQty = asnDetail.ReceiveQty;
                        asnDetail.Status = IQCStatus.IQCStatus_IQCClose;
                        _InventoryFacade.UpdateASNDetail(asnDetail);
                    }

                    //5、更新ASN明细对应单据行明细TBLASNDETAILITEM
                    object[] objAsnDetaileItem = _InventoryFacade.GetAsnDetailItem(asnIqcDetail.StNo, Convert.ToInt32(asnIqcDetail.StLine));
                    if (objAsnDetaileItem != null)
                    {
                        foreach (Asndetailitem asnDetaileItem in objAsnDetaileItem)
                        {
                            asnDetaileItem.QcpassQty = asnDetaileItem.ReceiveQty;
                            _InventoryFacade.UpdateAsndetailitem(asnDetaileItem);
                        }
                    }

                }
            }

            object[] objAsnIqcDetailSN = _IQCFacade.GetAsnIqcDetailSNByIqcNo(iqcNo);
            if (objAsnIqcDetailSN != null)
            {
                foreach (AsnIqcDetailSN asnIqcDetailSN in objAsnIqcDetailSN)
                {
                    //3、更新送检单明细SNTBLASNIQCDETAILSN
                    asnIqcDetailSN.QcStatus = "Y";
                    _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);

                    //6、更新ASN明细SN TBLASNDETAILSN
                    Asndetailsn asnDetailSn = (Asndetailsn)_InventoryFacade.GetAsndetailsn(asnIqcDetailSN.Sn, asnIqcDetailSN.StNo, Convert.ToInt32(asnIqcDetailSN.StLine));
                    if (asnDetailSn != null)
                    {
                        asnDetailSn.QcStatus = "Y";
                        _InventoryFacade.UpdateAsndetailsn(asnDetailSn);
                    }
                }
            }

            //7、以上表数据更新完成后检查ASN明细表(TBLASNDETAIL)所有行记录状态为：IQCClose:IQC完成 or OnLocation:上架 or Close:入库 or Cancel:取消时，
            //   更新ASN主表(TBLASN)状态(TBLASN.STATUS)为：OnLocation:上架
            bool isAllIQCClose = CheckAllASNDetailIsIQCClose(iqcNo);
            bool isAllOnLocation = CheckAllASNDetailIsOnLocation(iqcNo);
            bool isAllClose = CheckAllASNDetailIsClose(iqcNo);
            bool isAllCancel = CheckAllASNDetailIsCancel(iqcNo);

            if (isAllIQCClose || isAllOnLocation ||
                isAllClose || isAllCancel
                )
            {
                ASN asn = (ASN)_InventoryFacade.GetASN(asnIqc.StNo);
                if (asn != null)
                {
                    asn.Status = ASNHeadStatus.OnLocation;
                    _InventoryFacade.UpdateASN(asn);
                }
            }
        }

        //检查ASN明细所有行状态为IQCClose
        /// <summary>
        /// 检查ASN明细所有行状态为IQCClose
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是IQCClose：true;否则：false</returns>
        private bool CheckAllASNDetailIsIQCClose(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.IQCClose)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为OnLocation
        /// <summary>
        /// 检查ASN明细所有行状态为OnLocation
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是OnLocation：true;否则：false</returns>
        private bool CheckAllASNDetailIsOnLocation(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.OnLocation)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为Close
        /// <summary>
        /// 检查ASN明细所有行状态为Close
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是Close：true;否则：false</returns>
        private bool CheckAllASNDetailIsClose(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.Close)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        //检查ASN明细所有行状态为Cancel
        /// <summary>
        /// 检查ASN明细所有行状态为Cancel
        /// </summary>
        /// <param name="iqcNo">IQC检验单号</param>
        /// <returns>全部是Cancel：true;否则：false</returns>
        private bool CheckAllASNDetailIsCancel(string iqcNo)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            _InventoryFacade = new InventoryFacade(DataProvider);

            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(iqcNo);
            if (asnIqc != null)
            {
                object[] objAsnDetail = _InventoryFacade.GetASNDetailByStNo(asnIqc.StNo);
                if (objAsnDetail != null)
                {
                    foreach (ASNDetail asnDetail in objAsnDetail)
                    {
                        if (asnDetail.Status != ASNLineStatus.Cancel)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetIQCNGRecordDataGrid(string iqcNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.GetIQCNGRecordDataGrid(iqcNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("箱号编码", typeof(string));
                dt.Columns.Add("缺陷类型", typeof(string));
                dt.Columns.Add("缺陷描述", typeof(string));
                dt.Columns.Add("SN", typeof(string));
                dt.Columns.Add("缺陷品数", typeof(string));
                dt.Columns.Add("备注", typeof(string));
                dt.Columns.Add("ASN单号", typeof(string));
                dt.Columns.Add("ASN单行项目", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add("", row["CARTONNO"], row["ECGCODE"], row["ECODE"], row["SN"], row["NGQTY"], row["REMARK1"], row["STNO"], row["STLINE"]);
                }
            }
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public bool isKeyparts(string DQMcode)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            Domain.MOModel.Material _material = this._WarehouseFacade.GetMaterialFromDQMCode(DQMcode);
            if (_material.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
            {
                return true;
            }
            return false;
        }

        [WebMethod(EnableSession = true)]
        public string IQCNGRecordSaveReturnMessage(bool isAdd, string IQCNo, string DQMcode, string stNo, string stLine, string eCode, string sn, string ngType, string ngDesc, string _sn, string ngQty, string memo, string cartonNo, string userCode)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            AsnIQC asnIqc = (AsnIQC)_IQCFacade.GetAsnIQC(IQCNo);
            if (asnIqc.Status == IQCStatus.IQCStatus_IQCClose)
            {
                return IQCNo + "已经关闭，不能再修改";

            }

            try
            {
                this.DataProvider.BeginTransaction();
                if (string.IsNullOrEmpty(ngType))
                {
                    return "不良类型不能为空！";
                    //object[] objs = this._IQCFacade.GetAsnIQCDetailEc(IQCNo, cartonNo);
                    //foreach (AsnIQCDetailEc _asnIQCDetailEc in objs)
                    //{
                    //    if (string.IsNullOrEmpty(_asnIQCDetailEc.NgQty.ToString()))
                    //    {
                    //        return "请输入不良数";
                    //    }
                    //}
                }
                AsnIQCDetailEc asnIQCDetailEc = new AsnIQCDetailEc();
                if (isAdd)
                {
                    object obj = this._IQCFacade.GetASNIQCDetailByIQCNoAndCartonNo(IQCNo, cartonNo);
                    if (obj == null)
                    {
                        return "送检单明细没有对应的箱号信息";
                    }
                    AsnIQCDetail asnIQCDetail = obj as AsnIQCDetail;

                    asnIQCDetailEc.IqcNo = IQCNo;
                    asnIQCDetailEc.StNo = asnIQCDetail.StNo;
                    asnIQCDetailEc.StLine = asnIQCDetail.StLine;
                    asnIQCDetailEc.CartonNo = cartonNo;
                    asnIQCDetailEc.EcgCode = ngType;
                    asnIQCDetailEc.ECode = ngDesc;

                    Domain.MOModel.Material _material = this._WarehouseFacade.GetMaterialFromDQMCode(DQMcode);
                    if (_material.MCONTROLTYPE == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                    {
                        if (!string.IsNullOrEmpty(_sn))
                        {
                            object[] cartoninvmar_obj = _WarehouseFacade.GetIqcdetailsn(asnIQCDetailEc.IqcNo, FormatHelper.PKCapitalFormat(FormatHelper.CleanString(_sn)), asnIQCDetailEc.CartonNo);
                            if (cartoninvmar_obj == null)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "SN不在包装内";
                            }
                        }
                        if (string.IsNullOrEmpty(_sn))
                        {
                            return "请输入SN";

                        }
                        else
                        {
                            asnIQCDetailEc.NgQty = 1;
                        }
                        asnIQCDetailEc.NgFlag = "N";


                    }
                    else
                    {



                        decimal qty = 0;
                        try
                        {
                            qty = decimal.Parse(ngQty);
                            if (qty <= 0)
                            {
                                return "不良数必须为大于零的数字";
                            }
                        }
                        catch (Exception ex)
                        {
                            return "不良数必须为大于零的数字";
                        }


                        object objs_carqty = _IQCFacade.GetASNIQCDetailByIQCNoAndCartonNo(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo);
                        if (objs_carqty != null)
                        {
                            AsnIQCDetail carqty = objs_carqty as AsnIQCDetail;

                            if (qty > carqty.Qty)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "不良数量不能大于箱数";
                            }
                        }


                        asnIQCDetailEc.NgQty = int.Parse(ngQty);



                        asnIQCDetailEc.NgFlag = "N";

                    }



                    asnIQCDetailEc.SN = _sn;
                    asnIQCDetailEc.SqeStatus = string.Empty;
                    asnIQCDetailEc.Remark1 = memo;
                    asnIQCDetailEc.CUser = userCode;
                    asnIQCDetailEc.CDate = FormatHelper.GetNowDBDateTime(DataProvider).DBDate;
                    asnIQCDetailEc.CTime = FormatHelper.GetNowDBDateTime(DataProvider).DBTime;
                    asnIQCDetailEc.MaintainUser = userCode;
                    asnIQCDetailEc.MaintainDate = FormatHelper.GetNowDBDateTime(DataProvider).DBDate;
                    asnIQCDetailEc.MaintainTime = FormatHelper.GetNowDBDateTime(DataProvider).DBTime;
                }
                else
                {
                    object[] objs = this._IQCFacade.GetAsnIQCDetailEc(eCode, stLine, IQCNo, stNo, sn);
                    asnIQCDetailEc = objs[0] as AsnIQCDetailEc;
                    asnIQCDetailEc.Remark1 = memo;
                }
                if (isAdd)
                {
                    #region  判断录入的SN是否在包装内


                    #endregion

                    #region CheckData
                    //1》	同一箱号NGFLAG=Y的记录只能存在一笔
                    //2》	单件管控：SN记录NGFLAG=N的记录只能存在一笔
                    //3》	批管控：同一箱号NGFLAG=N记录的SUM(NGQTY)不能大于箱号送检数量
                    int asnIQCDetailEcCount = _IQCFacade.GetAsnIQCDetailEcCount(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "Y");
                    if (asnIQCDetailEcCount > 0)
                    {
                        if (asnIQCDetailEc.NgFlag == "Y")
                        {
                            this.DataProvider.RollbackTransaction();
                            return "同一箱号NGFLAG=Y的记录只能存在一笔";
                        }
                    }
                    object objAsnIQC = this._WarehouseFacade.GetAsnIQC(IQCNo);
                    AsnIQC asnIQC = objAsnIQC as AsnIQC;
                    BenQGuru.eMES.Domain.MOModel.Material material = this._WarehouseFacade.GetMaterialFromDQMCode(asnIQC.DQMCode);
                    string MControlType = material.MCONTROLTYPE;
                    if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_KEYPARTS)
                    {
                        int asnIQCDetailECCount = _IQCFacade.GetAsnIQCDetailECCount(asnIQCDetailEc.IqcNo, asnIQCDetailEc.SN, "N");
                        if (asnIQCDetailECCount > 0)
                        {
                            if (asnIQCDetailEc.NgFlag == "N")
                            {
                                this.DataProvider.RollbackTransaction();
                                return "SN记录NGFLAG=N的记录只能存在一笔";
                            }
                        }

                    }
                    if (MControlType == SAP_CONTROLTYPE.SAP_ITEM_CONTROL_LOT)
                    {

                        if (asnIqc != null)
                        {
                            int ngQtyCount = _IQCFacade.GetSumNgQtyFromAsnIQCDetailEc(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "N");
                            if (ngQtyCount > asnIqc.Qty)
                            {
                                this.DataProvider.RollbackTransaction();
                                return "大于箱号送检数量";
                            }
                        }
                    }

                    #endregion


                    isAdd = false;//新增状态更改为false

                    object[] objAsnIqcDetaileEc = _IQCFacade.GetAsnIQCDetailEc(asnIQCDetailEc.ECode, asnIQCDetailEc.StLine,
                                                                         asnIQCDetailEc.IqcNo, asnIQCDetailEc.StNo,
                                                                         asnIQCDetailEc.SN);
                    if (string.IsNullOrEmpty(_sn) && string.IsNullOrEmpty(ngQty))
                    {
                    }
                    else
                    {
                        if (objAsnIqcDetaileEc != null)
                        {
                            this.DataProvider.RollbackTransaction();
                            return "重复添加";
                        }
                    }
                    this._IQCFacade.AddAsnIQCDetailEc(asnIQCDetailEc);

                    AsnIQCDetail asnIqcDetail = (AsnIQCDetail)_IQCFacade.GetAsnIQCDetail(Convert.ToInt32(asnIQCDetailEc.StLine),
                                                                                        asnIQCDetailEc.IqcNo,
                                                                                        asnIQCDetailEc.StNo);
                    #region UpdateTable
                    if (asnIqcDetail != null)
                    {
                        asnIqcDetail.NgQty = this._IQCFacade.GetSumNgQtyFromAsnIQCDetailEc1(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo, "");
                        asnIqcDetail.QcStatus = "N";
                        _IQCFacade.UpdateAsnIQCDetail(asnIqcDetail);
                    }
                    AsnIqcDetailSN asnIqcDetailSN = (AsnIqcDetailSN)_IQCFacade.GetAsnIqcDetailSN(Convert.ToInt32(asnIQCDetailEc.StLine),
                                                                                                    asnIQCDetailEc.IqcNo,
                                                                                                    asnIQCDetailEc.SN,
                                                                                                    asnIQCDetailEc.StNo);
                    if (asnIqcDetailSN != null)
                    {
                        asnIqcDetailSN.QcStatus = "N";
                        _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN);
                    }
                    //if (string.IsNullOrEmpty(_sn) && string.IsNullOrEmpty(ngQty))
                    //{
                    //    object[] objs_asnIqcDetailSN1 = _IQCFacade.GetAsnIqcDetailSNByIqcNoAndCartonNo(asnIQCDetailEc.IqcNo, asnIQCDetailEc.CartonNo);
                    //    if (objs_asnIqcDetailSN1 != null && objs_asnIqcDetailSN1.Length > 0)
                    //    {
                    //        foreach (AsnIqcDetailSN asnIqcDetailSN1 in objs_asnIqcDetailSN1)
                    //        {
                    //            asnIqcDetailSN1.QcStatus = "N";
                    //            _IQCFacade.UpdateAsnIqcDetailSN(asnIqcDetailSN1);
                    //        }
                    //    }
                    //}
                    #endregion

                }
                else
                {
                    this._IQCFacade.UpdateAsnIQCDetailEc(asnIQCDetailEc);
                }

                this.DataProvider.CommitTransaction();
                return "保存成功";


            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "保存失败：" + ex.Message;
            }

        }

        [WebMethod(EnableSession = true)]
        public string IQCNGRecordDeleteReturnMessage(string IQCNo, List<string> strs)
        {
            if (_IQCFacade == null)
            {
                _IQCFacade = new IQCFacade(DataProvider);
            }
            try
            {
                foreach (string str in strs)
                {
                    string[] strArray = str.Split(',');
                    string eCode = strArray[0];
                    string stLine = strArray[1];
                    string stNo = strArray[2];
                    string sn = strArray[3];

                    object[] objs = this._IQCFacade.GetAsnIQCDetailEc(eCode, stLine, IQCNo, stNo, sn);
                    this._IQCFacade.DeleteAsnIQCDetailEc(objs[0] as AsnIQCDetailEc);
                }
                return "删除成功";
            }
            catch (Exception ex)
            {
                return "删除失败" + ex.ToString();
            }
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetDataGridDoc(string iqcNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DataTable dataTable = this._WarehouseFacade.GetDataGridDoc(iqcNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");
            if (dataTable != null)
            {
                dt.Columns.Add("选择", typeof(string));
                dt.Columns.Add("文件名", typeof(string));
                dt.Columns.Add("文件大小", typeof(string));
                dt.Columns.Add("上传者", typeof(string));
                dt.Columns.Add("上传日期", typeof(string));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    dt.Rows.Add("", row["DOCNAME"], row["DOCSIZE"], row["UPUSER"], row["UPFILEDATE"]);
                }
            }
            return dt;
        }

        [WebMethod(EnableSession = true)]
        public DataTable GetGridTableForPackageWinc(string pickNo)
        {
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            CartonInvDetailMaterial[] cs = this._WarehouseFacade.GetGridTableForPackageWinc(pickNo);
            //设置DataTable的tableName 否则序列化会报错
            DataTable dt = new DataTable("ExampleDataTable");


            dt.Columns.Add("DQMCODE", typeof(string));
            dt.Columns.Add("CARTONNO", typeof(string));
            dt.Columns.Add("QTY", typeof(string));

            if (dt != null)
            {

                foreach (CartonInvDetailMaterial c in cs)
                {
                    DataRow rd = dt.NewRow();
                    rd["DQMCODE"] = c.DQMCODE;
                    rd["CARTONNO"] = c.CARTONNO;
                    rd["QTY"] = (int)c.QTY;


                    dt.Rows.Add(rd);
                }
            }
            return dt;
        }


        [WebMethod(EnableSession = true)]
        public bool UploadFile(byte[] bytes, string iqcNo, string userName)
        {
            if (bytes.Length > 0)
            {
                MemoryStream ms = new MemoryStream(bytes);
                string root = HttpContext.Current.Server.MapPath("~/");

                string path = root + @"InvDoc\IQC\";
                string stamp = DateTime.Now.ToString("yyyyMMddmmhhss");


                string fileName = iqcNo + "_IqcAbnormal_" + stamp;
                string fullPath = path + fileName;
                Image.FromStream(ms).Save(fullPath + ".jpg");
                ms.Close();

                InvDoc doc = new InvDoc();
                doc.InvDocNo = iqcNo;
                doc.InvDocType = "IqcAbnormal";
                doc.Dirname = "IQC";
                doc.DocName = fileName;
                doc.DocType = ".jpg";
                doc.DocSize = (bytes.Length / 1024);
                doc.UpUser = userName;
                doc.UpfileDate = FormatHelper.TODateInt(DateTime.Now);
                doc.ServerFileName = fileName + doc.DocType;
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
            string result = "";
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
        #endregion


        [WebMethod(EnableSession = true)]
        public string[] GetDrpNGType()
        {

            BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(DataProvider);
            List<string> sss = new List<string>();
            object[] objs = _SystemSettingFacade.GetErrorGroupcode();
            if (objs != null)
            {
                foreach (BenQGuru.eMES.Domain.TSModel.ErrorCodeGroupA ecg in objs)
                {
                    sss.Add(ecg.ErrorCodeGroupDescription + "," + ecg.ErrorCodeGroup);
                }
            }
            return sss.ToArray();
        }

        [WebMethod(EnableSession = true)]
        public string[] InitDrpNGDesc(string NGTYPE)
        {


            BenQGuru.eMES.BaseSetting.SystemSettingFacade _SystemSettingFacade = new BenQGuru.eMES.BaseSetting.SystemSettingFacade(DataProvider);
            List<string> sss = new List<string>();


            object[] objs = _SystemSettingFacade.GetErrorcode(NGTYPE);
            if (objs != null)
            {
                foreach (BenQGuru.eMES.Domain.TSModel.ErrorCodeA ec in objs)
                {
                    sss.Add(ec.ErrorDescription + "," + ec.ErrorCode);
                }
            }
            return sss.ToArray();
        }

        [WebMethod(EnableSession = true)]
        public string[] GetIQCNoFromCartonno(string cartonno)
        {

            WarehouseFacade _WarehouseFacade = new WarehouseFacade(DataProvider);
            AsnIQC[] iqcs = _WarehouseFacade.QueryAsnIQCFromCartonno(cartonno);
            List<string> iqcnos = new List<string>();
            foreach (AsnIQC iqc in iqcs)
                iqcnos.Add(iqc.IqcNo);
            return iqcnos.ToArray();

        }

        [WebMethod(EnableSession = true)]
        public string PackFinish(string pickNo, string userCode)
        {

            //if (!ValidateInput())
            //{
            //    return;
            //}
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;


            object obj = _WarehouseFacade.GetCartonInvoices(pickNo.Trim().ToUpper());
            if (obj == null)
                return "当前拣货任务令号没有对应的发货箱单信息";


            CARTONINVOICES carvoice = obj as CARTONINVOICES;
            if (carvoice.STATUS != "Release" && carvoice.STATUS == "Pack")
            {
                return "箱单状态必须是初始化或者包装中才能操作！";
            }

            Pick pick = (Pick)_WarehouseFacade.GetPick(pickNo.Trim().ToUpper());
            //请先完成拣料
            if (pick != null)
            {
                if (!(pick.Status == PickHeadStatus.PickHeadStatus_Pack || pick.Status == PickHeadStatus.PickHeadStatus_MakePackingList))
                    return "请先完成拣料";


            }

            try
            {
                this.DataProvider.BeginTransaction();

                //1. 当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
                _WarehouseFacade.UpdateCartoninvdetailByCARINVNO((obj as CARTONINVOICES).CARINVNO, CartonInvoices_STATUS.Status_ClosePack);
                //object _obj = this._WarehouseFacade.GetCartonInvDetail((obj as CARTONINVOICES).CARINVNO, (obj as CARTONINVOICES).CARINVNO);//this.txtCartonNo.Text.Trim().ToUpper()
                //if (_obj == null)
                //{
                //    WebInfoPublish.Publish(this, "当前包装箱号没有对应的发货箱单明细信息", this.languageComponent1);
                //    return;
                //}

                //CartonInvDetail cartonInvDetail = _obj as CartonInvDetail;
                //cartonInvDetail.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                //cartonInvDetail.MUSER = mUser;
                //cartonInvDetail.MDATE = mDate;
                //cartonInvDetail.MTIME = mTime;
                //this._WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                //2. 检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] objs = this._WarehouseFacade.GetPickLineByPickNoNotCancel(pickNo);
                if (objs == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "当前拣货任务令号没有对应的拣货任务令明细信息";

                }

                bool isTrue = true;
                foreach (PickDetail pickDetail in objs)
                {
                    if (pickDetail.SQTY != pickDetail.PQTY)
                    {
                        isTrue = false;
                        break;
                    }
                }
                if (isTrue)
                {
                    CARTONINVOICES _CARTONINVOICES = obj as CARTONINVOICES;
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                    _CARTONINVOICES.FDATE = mDate;
                    _CARTONINVOICES.FTIME = mTime;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                    return "当前发货箱单未包装完成";

                }

                foreach (PickDetail pickDetail in objs)
                {
                    pickDetail.Status = PickDetail_STATUS.Status_ClosePack;
                    pickDetail.MaintainUser = mUser;
                    pickDetail.MaintainDate = mDate;
                    pickDetail.MaintainTime = mTime;
                    _WarehouseFacade.UpdatePickdetail(pickDetail);
                }

                this.DataProvider.CommitTransaction();
                return "装箱成功";

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "装箱失败：" + ex.Message;
            }
        }



        [WebMethod(EnableSession = true)]
        public string GFPackFinish(string pickNo, string userCode)
        {
            //if (!ValidateInput())
            //{
            //    return;
            //}
            if (_WarehouseFacade == null)
            {
                _WarehouseFacade = new WarehouseFacade(DataProvider);
            }
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string mUser = userCode;
            int mDate = dbDateTime.DBDate;
            int mTime = dbDateTime.DBTime;


            object obj = this._WarehouseFacade.GetCartonInvoices(pickNo.Trim().ToUpper());
            if (obj == null)
            {
                return "当前拣货任务令号没有对应的发货箱单信息";

            }

            CARTONINVOICES carvoice = obj as CARTONINVOICES;
            if (carvoice.STATUS != "Release" && carvoice.STATUS == "Pack")
            {
                return "箱单状态必须是初始化或者包装中才能操作！";

            }

            Pick pick = (Pick)_WarehouseFacade.GetPick(pickNo.Trim().ToUpper());
            //请先完成拣料
            if (pick != null)
            {
                if (!(pick.Status == PickHeadStatus.PickHeadStatus_Pack || pick.Status == PickHeadStatus.PickHeadStatus_MakePackingList))
                {
                    return "请先完成拣料";

                }
            }
            try
            {
                this.DataProvider.BeginTransaction();

                //1. 当前包装箱号在发货箱单明细信息表(TBLCartonInvDetail)中的状态更新为：ClosePack:包装完成
                _WarehouseFacade.UpdateCartoninvdetailByCARINVNO((obj as CARTONINVOICES).CARINVNO, CartonInvoices_STATUS.Status_ClosePack);
                //object _obj = this._WarehouseFacade.GetCartonInvDetail((obj as CARTONINVOICES).CARINVNO, this.txtCartonNo.Text.Trim().ToUpper());
                //if (_obj == null)
                //{
                //    WebInfoPublish.Publish(this, "当前包装箱号没有对应的发货箱单明细信息", this.languageComponent1);
                //    return;
                //}

                //CartonInvDetail cartonInvDetail = _obj as CartonInvDetail;
                //cartonInvDetail.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                //cartonInvDetail.MUSER = mUser;
                //cartonInvDetail.MDATE = mDate;
                //cartonInvDetail.MTIME = mTime;
                //this._WarehouseFacade.UpdateCartonInvDetail(cartonInvDetail);

                //2. 检查当前拣货任务令号在拣货任务令明细表(TBLPICKDETAIL)中所有记录SQTY=PQTY时，更新发货箱单号状态(TBLCartonInvoices .STATUS)为：ClosePack:包装完成
                object[] objs = this._WarehouseFacade.GetPickLineByPickNoNotCancel(pickNo);
                if (objs == null)
                {
                    this.DataProvider.RollbackTransaction();
                    return "当前拣货任务令号没有对应的拣货任务令明细信息";

                }

                bool isTrue = true;
                foreach (PickDetail pickDetail in objs)
                {
                    if (pickDetail.SQTY != pickDetail.PQTY)
                    {
                        isTrue = false;
                        break;
                    }
                }
                if (isTrue)
                {
                    CARTONINVOICES _CARTONINVOICES = obj as CARTONINVOICES;
                    _CARTONINVOICES.STATUS = CartonInvoices_STATUS.Status_ClosePack;
                    _CARTONINVOICES.FDATE = mDate;
                    _CARTONINVOICES.FTIME = mTime;
                    _CARTONINVOICES.MUSER = mUser;
                    _CARTONINVOICES.MDATE = mDate;
                    _CARTONINVOICES.MTIME = mTime;
                    this._WarehouseFacade.UpdateTBLCartonInvoices(_CARTONINVOICES);
                }
                else
                {
                    this.DataProvider.RollbackTransaction();
                    return "当前发货箱单未包装完成";

                }

                foreach (PickDetail pickDetail in objs)
                {
                    pickDetail.Status = PickDetail_STATUS.Status_ClosePack;
                    pickDetail.MaintainUser = mUser;
                    pickDetail.MaintainDate = mDate;
                    pickDetail.MaintainTime = mTime;
                    _WarehouseFacade.UpdatePickdetail(pickDetail);
                }

                this.DataProvider.CommitTransaction();
                return "装箱成功";

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                return "装箱失败：" + ex.Message;
            }

        }




        [WebMethod(EnableSession = true)]
        public string ApplyOQC(string CARINVNO, string pickNo, string userCode)
        {

            BenQGuru.eMES.Material.WarehouseFacade _WarehouseFacade = new BenQGuru.eMES.Material.WarehouseFacade(DataProvider);

            BenQGuru.eMES.Material.InventoryFacade inventoryFacade = new BenQGuru.eMES.Material.InventoryFacade(DataProvider);


            CARTONINVOICES c1 = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoices(CARINVNO);
            if (c1 == null)
                return CARINVNO + "无发货箱单信息";

            if (c1.STATUS != "ClosePack")
                return c1.CARINVNO + "必须是包装完成才能申请OQC！";
            //sbShowMsg.AppendFormat(c1.CARINVNO + "已经申请OQC,不能重复申请！");
            //continue;


            if (c1.STATUS == "OQC")
                return c1.CARINVNO + "已经申请OQC,不能重复申请！";


            BenQGuru.eMES.Domain.Warehouse.Pick pick = (BenQGuru.eMES.Domain.Warehouse.Pick)inventoryFacade.GetPick(pickNo);
            if (pick == null)
            {
                return "没有关联拣货任务！";

            }

            if (pick.PickType != "POC" && pick.PickType != "DNC" && pick.PickType != "UB" && pick.PickType != "PRC" && pick.PickType != "BFC")
            {
                return "任务类型必须是Return PO DN出库 UB:调拨 PR领料 报废出库  ！";

            }


            BenQGuru.eMES.Domain.Warehouse.PickDetail[] ddd = _WarehouseFacade.GetPickdetails(pick.PickNo);

            foreach (BenQGuru.eMES.Domain.Warehouse.PickDetail d in ddd)
            {
                if (d.Status != PickDetail_STATUS.Status_Cancel)
                {
                    if (d.Status != PickDetail_STATUS.Status_ClosePack)
                    {
                        return pick.PickNo + "在明细表里状态不是包装完成！";

                    }
                }
            }

            try
            {

                this.DataProvider.BeginTransaction();



                if (string.IsNullOrEmpty(pick.GFFlag) || pick.GFFlag == "N")
                {
                    BenQGuru.eMES.Material.OQCNew[] dqmCodes = _WarehouseFacade.GetOQCDQMCodes(CARINVNO);
                    Dictionary<string, string> dqmCodeOQCNo = new Dictionary<string, string>();

                    foreach (BenQGuru.eMES.Material.OQCNew dqmCode in dqmCodes)
                    {
                        #region 非光伏
                        string perfix = "TDOQC" + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                        BenQGuru.eMES.Domain.Warehouse.Serialbook s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                        int max = 0;
                        if (s == null)
                        {
                            max = 1;
                            s = new BenQGuru.eMES.Domain.Warehouse.Serialbook();
                            s.MAXSerial = "1";
                            s.MDate = FormatHelper.TODateInt(DateTime.Now);
                            s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                            s.MUser = userCode;
                            s.SNprefix = perfix;
                            _WarehouseFacade.AddSerialbook(s);
                        }
                        else
                        {
                            max = int.Parse(s.MAXSerial);
                            max++;
                            s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                            s.MAXSerial = max.ToString();
                            s.MDate = FormatHelper.TODateInt(DateTime.Now);
                            s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                            s.MUser = userCode;
                            _WarehouseFacade.UpdateSerialbook(s);

                        }

                        string oqcNum = perfix + (max).ToString().PadLeft(3, '0');
                        //BenQGuru.eMES.Domain.Warehouse.OQC oqc = new BenQGuru.eMES.Domain.Warehouse.OQC();
                        BenQGuru.eMES.Domain.OQC.OQC oqc = new BenQGuru.eMES.Domain.OQC.OQC();
                        oqc.OqcNo = oqcNum;
                        oqc.AppDate = FormatHelper.TODateInt(DateTime.Now);
                        oqc.AppTime = FormatHelper.TOTimeInt(DateTime.Now);
                        oqc.CarInvNo = CARINVNO;
                        oqc.Status = "Release";
                        oqc.PickNo = pickNo;
                        oqc.CUser = userCode;
                        oqc.CDate = FormatHelper.TODateInt(DateTime.Now);
                        oqc.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                        oqc.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                        oqc.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                        oqc.MaintainUser = userCode;
                        _WarehouseFacade.AddOQC(oqc);

                        dqmCodeOQCNo.Add(dqmCode.DQMCODE, oqcNum);


                        BenQGuru.eMES.Material.OQCNew[] oqcs = _WarehouseFacade.GetOQCDetailsForN(CARINVNO, dqmCode.DQMCODE);

                        foreach (BenQGuru.eMES.Material.OQCNew o in oqcs)
                        {
                            BenQGuru.eMES.Domain.OQC.OQCDetail d = new BenQGuru.eMES.Domain.OQC.OQCDetail();
                            d.CarInvNo = CARINVNO;
                            d.CartonNo = o.CARTONNO;
                            d.MCode = o.MCODE;
                            d.OqcNo = oqcNum;
                            d.DQMCode = o.DQMCODE;
                            d.Qty = (int)o.QTY;
                            d.Unit = o.UNIT;
                            d.GfHwItemCode = o.GFHWITEMCODE;
                            d.GfPackingSeq = o.GFPACKINGSEQ;
                            d.CUser = userCode;
                            d.CDate = FormatHelper.TODateInt(DateTime.Now);
                            d.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                            d.MaintainUser = userCode;
                            d.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            d.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            _WarehouseFacade.AddOQCDetail(d);
                        }
                        #endregion

                    }

                    OQCNew[] oqcNews = _WarehouseFacade.GetOQCDetailSNForN1(CARINVNO);
                    foreach (BenQGuru.eMES.Material.OQCNew o in oqcNews)
                    {
                        BenQGuru.eMES.Domain.OQC.OQCDetailSN dsN = new BenQGuru.eMES.Domain.OQC.OQCDetailSN();
                        dsN.CarInvNo = CARINVNO;
                        dsN.OqcNo = dqmCodeOQCNo[o.DQMCODE];
                        dsN.CartonNo = o.CARTONNO;
                        dsN.SN = o.SN;
                        dsN.DQMCode = string.IsNullOrEmpty(o.DQMCODE) ? " " : o.DQMCODE;
                        dsN.MCode = string.IsNullOrEmpty(o.MCODE) ? " " : o.MCODE;
                        dsN.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                        dsN.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                        dsN.MaintainUser = userCode;
                        _WarehouseFacade.AddOQCDetailSN(dsN);
                    }
                }
                else if (pick.GFFlag.ToLower() == "x")
                {

                    #region 光伏
                    List<string> ss = _WarehouseFacade.GetGFHWITEMCODE(CARINVNO);

                    foreach (string code in ss)
                    {
                        string perfix = "TDOQC" + DateTime.Now.Year + DateTime.Now.Month.ToString().PadLeft(2, '0') + DateTime.Now.Day.ToString().PadLeft(2, '0');
                        BenQGuru.eMES.Domain.Warehouse.Serialbook s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                        int max = 0;
                        if (s == null)
                        {
                            max = 1;
                            s = new BenQGuru.eMES.Domain.Warehouse.Serialbook();
                            s.MAXSerial = "1";
                            s.MDate = FormatHelper.TODateInt(DateTime.Now);
                            s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                            s.MUser = userCode;
                            s.SNprefix = perfix;

                            _WarehouseFacade.AddSerialbook(s);
                        }
                        else
                        {

                            max = int.Parse(s.MAXSerial);
                            max++;
                            s = (BenQGuru.eMES.Domain.Warehouse.Serialbook)_WarehouseFacade.GetSerialbook(perfix);
                            s.MAXSerial = max.ToString();
                            s.MDate = FormatHelper.TODateInt(DateTime.Now);
                            s.MTime = FormatHelper.TOTimeInt(DateTime.Now);
                            s.MUser = userCode;
                            _WarehouseFacade.UpdateSerialbook(s);

                        }
                        string oqcNum = perfix + (max).ToString().PadLeft(3, '0');
                        //BenQGuru.eMES.Domain.Warehouse.OQC oqc = new BenQGuru.eMES.Domain.Warehouse.OQC();
                        BenQGuru.eMES.Domain.OQC.OQC oqc = new Domain.OQC.OQC();
                        oqc.OqcNo = oqcNum;
                        oqc.AppDate = FormatHelper.TODateInt(DateTime.Now);
                        oqc.AppTime = FormatHelper.TOTimeInt(DateTime.Now);
                        oqc.CarInvNo = CARINVNO;
                        oqc.Status = "Release";
                        oqc.PickNo = pickNo;
                        oqc.CUser = userCode;
                        oqc.CDate = FormatHelper.TODateInt(DateTime.Now);
                        oqc.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                        oqc.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                        oqc.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                        oqc.MaintainUser = userCode;
                        _WarehouseFacade.AddOQC(oqc);

                        BenQGuru.eMES.Material.OQCNew[] oqcs = _WarehouseFacade.GetOQCDetailsForY(CARINVNO, code);
                        foreach (BenQGuru.eMES.Material.OQCNew o in oqcs)
                        {
                            BenQGuru.eMES.Domain.OQC.OQCDetail d = new BenQGuru.eMES.Domain.OQC.OQCDetail();
                            d.CarInvNo = CARINVNO;
                            d.CartonNo = o.CARTONNO;
                            d.MCode = o.MCODE;
                            d.OqcNo = oqcNum;
                            d.DQMCode = o.DQMCODE;
                            d.Qty = (int)o.QTY;
                            d.Unit = o.UNIT;
                            d.GfHwItemCode = o.GFHWITEMCODE;
                            d.GfPackingSeq = o.GFPACKINGSEQ;
                            d.CUser = userCode;
                            d.CDate = FormatHelper.TODateInt(DateTime.Now);
                            d.CTime = FormatHelper.TOTimeInt(DateTime.Now);
                            d.MaintainUser = userCode;
                            d.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            d.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            _WarehouseFacade.AddOQCDetail(d);

                        }
                        oqcs = _WarehouseFacade.GetOQCDetailSNForY(CARINVNO, code);
                        foreach (BenQGuru.eMES.Material.OQCNew o in oqcs)
                        {
                            BenQGuru.eMES.Domain.OQC.OQCDetailSN dsN = new BenQGuru.eMES.Domain.OQC.OQCDetailSN();
                            dsN.CarInvNo = CARINVNO;
                            dsN.OqcNo = oqcNum;
                            dsN.CartonNo = o.CARTONNO;
                            dsN.SN = o.SN;
                            dsN.DQMCode = string.IsNullOrEmpty(o.DQMCODE) ? " " : o.DQMCODE;
                            dsN.MCode = string.IsNullOrEmpty(o.MCODE) ? " " : o.MCODE;
                            dsN.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
                            dsN.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
                            dsN.MaintainUser = userCode;
                            _WarehouseFacade.AddOQCDetailSN(dsN);
                        }
                    }

                    #endregion
                }

                Pick p = (Pick)_WarehouseFacade.GetPick(pickNo);

                p.Status = "OQC";
                _WarehouseFacade.UpdatePick(p);

                CARTONINVOICES cc = (CARTONINVOICES)_WarehouseFacade.GetCartoninvoices(CARINVNO);
                cc.STATUS = "OQC";
                _WarehouseFacade.UpdateCartonInvoices(cc);

                this.DataProvider.CommitTransaction();
                return "OQC申请成功！";

            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }



        }

    }


}
