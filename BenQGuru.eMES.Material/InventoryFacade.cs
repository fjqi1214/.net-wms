using System;
using System.Data;
using System.Collections;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.Delivery;
using BenQGuru.eMES.Domain.IQC;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.OQC;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.MOModel;
using BenQGuru.eMES.Package;
using BenQGuru.eMES.Web.Helper;
using UserControl;
using System.Collections.Generic;
using BenQGuru.eMES.Domain.BaseSetting;

namespace BenQGuru.eMES.Material
{
    public class InventoryFacade : MarshalByRefObject
    {
        public const string Power = "PO";
        public const string PID = "PI";
        public const string HIDr = "HI";

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public InventoryFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        #region DataProvider

        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public InventoryFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                if (_domainDataProvider == null)
                {
                    _domainDataProvider = DomainDataProviderManager.DomainDataProvider();
                }

                return _domainDataProvider;
            }
        }

        #endregion

        #region ERPINVInterface
        /// <summary>
        /// 
        /// </summary>
        public ERPINVInterface CreateNewERPINVInterface()
        {
            return new ERPINVInterface();
        }

        public void AddERPINVInterface(ERPINVInterface eRPINVInterface)
        {
            this._helper.AddDomainObject(eRPINVInterface);
        }

        public void UpdateERPINVInterface(ERPINVInterface eRPINVInterface)
        {
            this._helper.UpdateDomainObject(eRPINVInterface);
        }

        public void DeleteERPINVInterface(ERPINVInterface eRPINVInterface)
        {
            this._helper.DeleteDomainObject(eRPINVInterface);
        }

        public void DeleteERPINVInterface(ERPINVInterface[] eRPINVInterface)
        {
            this._helper.DeleteDomainObject(eRPINVInterface);
        }

        public object GetERPINVInterface(string rECNO, string mOCODE, string sTATUS, string SRNO)
        {
            return this.DataProvider.CustomSearch(typeof(ERPINVInterface), new object[] { rECNO, mOCODE, sTATUS, SRNO });
        }

        /// <summary>
        /// ** 功能描述:	查询ERPINVInterface的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-10-11 11:00:53
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="rECNO">RECNO，模糊查询</param>
        /// <param name="mOCODE">MOCODE，模糊查询</param>
        /// <param name="sTATUS">STATUS，模糊查询</param>
        /// <returns> ERPINVInterface的总记录数</returns>
        public int QueryERPINVInterfaceCount(string rECNO, string mOCODE, string sTATUS)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLERPINVInterface where 1=1 and RECNO like '{0}%'  and MOCODE like '{1}%'  and STATUS like '{2}%' ", rECNO, mOCODE, sTATUS)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ERPINVInterface
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-10-11 11:00:53
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="rECNO">RECNO，模糊查询</param>
        /// <param name="mOCODE">MOCODE，模糊查询</param>
        /// <param name="sTATUS">STATUS，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> ERPINVInterface数组</returns>
        public object[] QueryERPINVInterface(string rECNO, string mOCODE, string sTATUS, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ERPINVInterface), new PagerCondition(string.Format("select {0} from TBLERPINVInterface where 1=1 and RECNO like '{1}%'  and MOCODE like '{2}%'  and STATUS like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPINVInterface)), rECNO, mOCODE, sTATUS), "RECNO,MOCODE,STATUS", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ERPINVInterface
        /// ** 作 者:		Laws Lu
        /// ** 日 期:		2006-10-13
        /// ** 2.2.1	查询条件：工单、产品、入库日期、抛转日期、入库单号、已抛转/未抛转/全部
        /// ** 修 改:
        /// ** 日 期:
        public object[] QueryERPINVInterface(
            string recNo,
            string moCode,
            string itemCode,
            string recStartDate,
            string recEndDate,
            string tossStartDate,
            string tossEndDate,
            string status,
            int inclusive,
            int exclusive)
        {
            string sql = "SELECT {0} FROM TBLERPINVINTERFACE WHERE 1=1";// and RECNO like '{1}%'  and MOCODE like '{2}%'  and STATUS like '{3}%' ";

            if (recNo != null && recNo != String.Empty)
            {
                sql += "AND RECNO LIKE '" + recNo + "%'";
            }
            //工单
            if (moCode != null && moCode != String.Empty)
            {
                sql += " AND MOCODE in ('" + moCode + "')";
            }
            //产品
            if (itemCode != null && itemCode != String.Empty)
            {
                sql += " AND ITEMCODE in ('" + itemCode + "')";
            }
            //入库日期
            if (recStartDate != null && recStartDate != String.Empty
                && recEndDate != null && recEndDate != String.Empty)
            {
                sql += " AND MDATE BETWEEN  " + recStartDate + " AND " + recEndDate;
            }

            //抛转日期
            if (tossStartDate != null && tossStartDate != String.Empty
                && tossEndDate != null && tossEndDate != String.Empty)
            {
                sql += " AND UPLOADDATE BETWEEN  " + tossStartDate + " AND " + tossEndDate;
            }

            if (status != null && status != String.Empty)
            {
                sql += " AND status = '" + status.ToLower() + "'";
            }


            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPINVInterface)));
            return this.DataProvider.CustomQuery(typeof(ERPINVInterface), new PagerCondition(sql, "RECNO,MOCODE,STATUS", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ERPINVInterface
        /// ** 作 者:		Laws Lu
        /// ** 日 期:		2006-10-13
        /// ** 2.2.1	查询条件：工单、产品、入库日期、抛转日期、入库单号、已抛转/未抛转/全部
        /// ** 修 改:
        /// ** 日 期:
        public int QueryERPINVInterfaceCount(
            string recNo,
            string moCode,
            string itemCode,
            string recStartDate,
            string recEndDate,
            string tossStartDate,
            string tossEndDate,
            string status)
        {
            string sql = "SELECT COUNT(*) FROM TBLERPINVINTERFACE WHERE 1=1";// and RECNO like '{1}%'  and MOCODE like '{2}%'  and STATUS like '{3}%' ";

            if (recNo != null && recNo != String.Empty)
            {
                sql += "AND RECNO LIKE '" + recNo + "%'";
            }

            //工单
            if (moCode != null && moCode != String.Empty)
            {
                sql += " AND MOCODE IN ('" + moCode + "')";
            }
            //产品
            if (itemCode != null && itemCode != String.Empty)
            {
                sql += " AND ITEMCODE IN  ('" + itemCode + "')";
            }
            //入库日期
            if (recStartDate != null && recStartDate != String.Empty
                && recEndDate != null && recEndDate != String.Empty)
            {
                sql += " AND MDATE BETWEEN  " + recStartDate + " AND " + recEndDate;
            }

            //抛转日期
            if (tossStartDate != null && tossStartDate != String.Empty
                && tossEndDate != null && tossEndDate != String.Empty)
            {
                sql += " AND UPLOADDATE BETWEEN  " + tossStartDate + " AND " + tossEndDate;
            }

            if (status != null && status != String.Empty)
            {
                sql += " AND status = '" + status.ToLower() + "'";
            }

            //			sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPINVInterface)));
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的ERPINVInterface
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-10-11 11:00:53
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>ERPINVInterface的总记录数</returns>
        public object[] GetAllERPINVInterface()
        {
            return this.DataProvider.CustomQuery(typeof(ERPINVInterface), new SQLCondition(string.Format("select {0} from TBLERPINVInterface order by RECNO,MOCODE,STATUS", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPINVInterface)))));
        }


        public void UpdateInvERPQty(ERPINVInterface erp)
        {
            string sql = "UPDATE TBLERPINVInterface SET QTY = QTY + 1"
                + " WHERE RECNO = '" + erp.RECNO
                + "' AND MOCODE = '" + erp.MOCODE
                + "' AND STATUS = '" + INVERPType.INVERPTYPE_NEW + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void SubstractInvERPQty(ERPINVInterface erp)
        {
            string sql = "UPDATE TBLERPINVInterface SET QTY = QTY - 1"
                + " WHERE RECNO = '" + erp.RECNO
                + "' AND MOCODE = '" + erp.MOCODE
                + "' AND STATUS = '" + INVERPType.INVERPTYPE_NEW + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateERPINVInterfaceStatus(ERPINVInterface erp)
        {
            string sql = "UPDATE TBLERPINVInterface SET STATUS = '" + INVERPType.INVERPTYPE_PROCESSED + "'"
                + ",SRNO = '" + erp.SRNO + "'"
                + ",UPLOADUSER = '" + erp.UPLOADUSER + "'"
                + ",UPLOADDATE = " + erp.UPLOADDATE
                + ",UPLOADTIME = " + erp.UPLOADTIME
                + " WHERE RECNO = '" + erp.RECNO
                + "' AND MOCODE = '" + erp.MOCODE
                + "' AND STATUS = '" + INVERPType.INVERPTYPE_NEW + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public string GetMaxSRNO(int year, string factory)
        {
            string srno = ("SR" + year.ToString().PadLeft(2, '0') + factory + "000001");
            object[] obj = this.DataProvider.CustomQuery(typeof(ERPINVInterface), new SQLCondition(
                "select srno from (select srno from TBLERPINVInterface where srno like '" + "SR" + year.ToString().PadLeft(2, '0') + factory + "%' order by srno desc) where rownum = 1"
                ));

            if (obj != null && obj.Length > 0)
            {
                ERPINVInterface inter = obj[0] as ERPINVInterface;
                string tmpStr = inter.SRNO.Substring(6, 6);
                srno = ("SR" + year.ToString().PadLeft(2, '0') + factory + (int.Parse(tmpStr) + 1).ToString().PadLeft(6, '0'));
            }

            return srno;
        }

        /// <summary>
        /// Laws Lu
        /// </summary>
        /// <param name="sRNO">SRNO，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> ERPINVInterface数组</returns>
        public object[] QueryERPINVInterface(string recno, string mocode, string itemcode)
        {
            string sql = "select {0} from TBLERPINVInterface where status = '" + INVERPType.INVERPTYPE_NEW + "'";
            if (recno != null && recno != String.Empty)
            {
                sql += " AND recno = '" + recno + "'";
            }
            if (mocode != null && mocode != String.Empty)
            {
                sql += " AND mocode = '" + mocode + "'";
            }
            if (itemcode != null && itemcode != String.Empty)
            {
                sql += " AND itemcode = '" + itemcode + "'";
            }

            return this.DataProvider.CustomQuery(typeof(ERPINVInterface), new SQLCondition
                (string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPINVInterface)))
                ));
        }

        #endregion

        #region InvReceive
        /// <summary>
        /// 
        /// </summary>
        /// 
        public SimulateResult GetItemList(string rcard)
        {
            ArrayList retList = new ArrayList();
            BenQGuru.eMES.MOModel.MOFacade moFacade = new MOFacade(this.DataProvider);
            //先从MORCard中取值，再

            #region 先从MORCard中取出满足条件的ＲＣＡＲＤ
            object[] objRCards = this.GetMORCard(rcard);

            if (objRCards != null && objRCards.Length > 0)
            {
                foreach (object obj in objRCards)
                {
                    BenQGuru.eMES.Domain.MOModel.MORunningCard mr = obj as BenQGuru.eMES.Domain.MOModel.MORunningCard;
                    if (mr != null)
                    {
                        SimulateResult sr = new SimulateResult();
                        sr.IsTrans = true;
                        sr.IsCompleted = false;
                        sr.IsInv = true;
                        sr.IsClosed = false;
                        sr.MOCode = mr.MOCode;
                        sr.RunningCard = rcard;
                        BenQGuru.eMES.Domain.MOModel.MO mo = moFacade.GetMO(mr.MOCode) as BenQGuru.eMES.Domain.MOModel.MO;
                        if (mo != null)
                        {
                            sr.ItemCode = mo.ItemCode;
                            sr.IsClosed = (mo.MOStatus == BenQGuru.eMES.Web.Helper.MOManufactureStatus.MOSTATUS_CLOSE);
                            sr.IsCompleted = sr.IsClosed;
                            retList.Add(sr);
                        }
                    }
                }
            }

            if (retList.Count == 0)
                throw new Exception(rcard + " $Error_ProductInfo_IS_Null");//产品序列号没有生产信息
            #endregion

            #region 判断是否做过序列号转换
            bool some_not_trans = false;
            for (int i = 0; i < retList.Count; i++)
            {
                SimulateResult sr = retList[i] as SimulateResult;
                if (!sr.IsClosed) //当工单没有关单时，判断是否存在Simualtion,不存在则表明已经做过转换了
                {
                    object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.Simulation),
                        new SQLCondition(string.Format("select * from TBLSIMULATION where RCARD = '{0}' and mocode = '{1}'",
                        sr.RunningCard, sr.MOCode))
                        );
                    if (objs == null || objs.Length == 0)
                    {
                        sr.IsTrans = true;
                        sr.IsDeleted = true;
                    }
                    else
                    {
                        sr.IsTrans = false;
                        some_not_trans = true;//存在一个没有做过序列号转换的
                        BenQGuru.eMES.Domain.DataCollect.Simulation sim = objs[0] as BenQGuru.eMES.Domain.DataCollect.Simulation;
                        if (FormatHelper.StringToBoolean(sim.IsComplete) == true
                            && sim.ProductStatus == "GOOD"
                            && !sr.IsCompleted)
                        {
                            sr.IsCompleted = true;
                        }
                    }
                }
                else //工单已经关单了，查询TblonwipCardtrans，存在记录，则证明做过转换，否则没有
                {
                    object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.DataCollect.Simulation),
                        new SQLCondition(string.Format("select * from TblonwipCardtrans where SCARD = '{0}' and mocode = '{1}'",
                        sr.RunningCard, sr.MOCode))
                        );
                    if (objs != null && objs.Length > 0)
                    {
                        sr.IsTrans = true;
                        sr.IsDeleted = true;
                    }
                    else
                    {
                        sr.IsTrans = false;

                        some_not_trans = true;
                    }
                }
            }

            //不存在没有做过序列号转换的
            if (retList.Count == 0 || !some_not_trans)
                throw new Exception(rcard + " $Error_Rcard_Transed");//序列号已经被序号转换，请采集转换后序列号
            #endregion

            #region 中判断RunningCard是否完工
            bool somecomplete = false;
            for (int i = 0; i < retList.Count; i++)
            {
                SimulateResult sr = retList[i] as SimulateResult;
                if (!sr.IsDeleted && sr.IsCompleted == true && !sr.IsTrans) //没有被前面的判断删除，且已经完工，并且没有做过序号转换
                {
                    somecomplete = true;
                }
                else
                {
                    sr.IsDeleted = true;
                }
            }

            //不存在序列号，或者几个中没有一个完工
            if (retList.Count == 0 || !somecomplete)
                throw new Exception(rcard + " $Error_Rcard_No_Completed");//产品序列号还没有完工

            #endregion

            #region 到入库资料中判断是否已经入库
            bool some_not_inv = false;
            for (int i = 0; i < retList.Count; i++)
            {
                SimulateResult sr = retList[i] as SimulateResult;
                if (!sr.IsDeleted && sr.IsCompleted == true && !sr.IsTrans) ////没有被前面的判断删除，,已经完工，并且没有做过序号转换,才需要判断是否入库
                {
                    if (this.IsInv(sr.RunningCard, sr.MOCode))
                    {
                        sr.IsInv = true;
                        sr.IsDeleted = true;
                    }
                    else
                    {
                        sr.IsInv = false;
                        some_not_inv = true;
                    }
                }
            }

            if (!some_not_inv)
                throw new Exception(rcard + " $Error_has_Received");//已经入库了
            #endregion

            #region 找到任何一个已经完工，并且还没有入库的RCard返回,并且没有做过序列号转换
            for (int i = 0; i < retList.Count; i++)
            {
                SimulateResult sr = retList[i] as SimulateResult;
                if (!sr.IsDeleted && sr.IsCompleted && !sr.IsInv && !sr.IsTrans)
                {
                    return sr;
                }
            }

            return null;

            #endregion
        }

        public InvReceive CreateNewInvReceive()
        {
            return new InvReceive();
        }

        public void AddInvReceive(InvReceive rec)
        {
            rec.RecStatus = ReceiveStatus.Receiving;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            rec.MaintainDate = FormatHelper.TODateInt(dtNow);
            rec.MaintainTime = FormatHelper.TOTimeInt(dtNow);
            rec.ActQty = 0;
            rec.ReceiveDate = 0;
            rec.ReceiveTime = 0;
            rec.ReceiveUser = string.Empty;
            rec.RecSeq = this.GetNextInvReceiveSeq(rec.RecNo);
            this._helper.AddDomainObject(rec);
        }

        public void UpdateInvReceive(InvReceive invReceive)
        {
            this._helper.UpdateDomainObject(invReceive);
        }

        public void DeleteInvReceive(InvReceive invReceive)
        {
            this._helper.DeleteDomainObject(invReceive);
        }

        public void DeleteInvReceive(InvReceive[] invReceive)
        {
            this._helper.DeleteDomainObject(invReceive);
        }

        public object GetInvReceive(string recNo, int recSeq)
        {
            return this.DataProvider.CustomSearch(typeof(InvReceive), new object[] { recNo, recSeq });
        }


        public int QueryInvReceiveCount(string recNo, int recSeq)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVREC where 1=1 and RECNO like '{0}%'  and RECSEQ like '{1}%' ", recNo, recSeq)));
        }


        public object[] QueryInvReceive(string recNo, int recSeq, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(InvReceive), new PagerCondition(string.Format("select {0} from TBLINVREC where 1=1 and RECNO like '{1}%'  and RECSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo, recSeq), "RECNO,RECSEQ", inclusive, exclusive));
        }


        public object[] QueryInvReceive(string recNo)
        {
            return this.DataProvider.CustomQuery(typeof(InvReceive), new SQLCondition(string.Format("select {0} from TBLINVREC where 1=1 and RECNO = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo)));
        }

        public object[] QueryInvReceive(string recNo, string model)
        {
            return this.DataProvider.CustomQuery(typeof(InvReceive), new SQLCondition(string.Format("select {0} from TBLINVREC where 1=1 and RECNO = '{1}' and modelcode='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo, model)));
        }

        public InvReceive GetInvReceive(string recNo, string itemcode)
        {
            object[] objs = this.DataProvider.CustomQuery(
                typeof(InvReceive), new SQLCondition(
                string.Format("select {0} from TBLINVREC where RECNO='{1}' AND ITEMCODE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo, itemcode)
                )
                );
            if (objs != null && objs.Length > 0)
            {
                return objs[0] as InvReceive;
            }
            else
                return null;
        }

        public InvReceive GetInvReceive(string recNo)
        {
            object[] objs = this.DataProvider.CustomQuery(
                typeof(InvReceive), new SQLCondition(
                string.Format("select {0} from TBLINVREC where RECNO='{1}' and rownum=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo)
                )
                );
            if (objs != null && objs.Length > 0)
            {
                return objs[0] as InvReceive;
            }
            else
                return null;
        }

        public object[] QueryAllInvReceive(string recNo)
        {
            return this.DataProvider.CustomQuery(
                typeof(InvReceive), new SQLCondition(
                string.Format("select {0} from TBLINVREC where RECNO='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo)
                )
                );

        }

        public object[] GetAllInvReceive()
        {
            return this.DataProvider.CustomQuery(typeof(InvReceive), new SQLCondition(string.Format("select {0} from TBLINVREC order by RECNO,RECSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)))));
        }

        private int GetNextInvReceiveSeq(string recNo)
        {
            try
            {
                int seq = this.DataProvider.GetCount(new SQLCondition(string.Format("SELECT max(RECSEQ) FROM TBLINVREC WHERE RECNO='{0}'", recNo))); ;
                return seq + 1;
            }
            catch
            {
                return 1;
            }
        }

        public string GetInvReceiveStatus(string recNo)
        {
            object[] objs = this.DataProvider.CustomQuery(
                typeof(InvReceive), new SQLCondition(
                string.Format("select {0} from TBLINVREC where RECNO='{1}' AND ROWNUM=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo)
                )
                );
            if (objs != null && objs.Length > 0)
            {
                InvReceive inv = objs[0] as InvReceive;
                if (inv != null)
                    return inv.RecStatus;
                else
                    return null;
            }
            else
                return null;
        }

        public bool InvReceiveExist(string recNo, string itemcode)
        {
            object[] objs = this.DataProvider.CustomQuery(
                typeof(InvReceive), new SQLCondition(
                string.Format("select {0} from TBLINVREC where RECNO='{1}' AND ITEMCODE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo, itemcode)
                )
                );
            if (objs != null && objs.Length > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool InvReceiveExistByMo(string recNo, string mocode)
        {
            object[] objs = this.DataProvider.CustomQuery(
                typeof(InvReceive), new SQLCondition(
                string.Format("select {0} from TBLINVREC where RECNO='{1}' AND mocode='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo, mocode)
                )
                );
            if (objs != null && objs.Length > 0)
            {
                return true;
            }
            else
                return false;
        }

        public bool InvReceiveExistByMoItem(string recNo, string mocode, string itemcode)
        {
            string sql;
            if (mocode == null || mocode.Trim() == string.Empty)
                sql = string.Format("select {0} from TBLINVREC where RECNO='{1}' AND (mocode='{2}' or mocode is null)and itemcode='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo, mocode, itemcode);
            else
                sql = string.Format("select {0} from TBLINVREC where RECNO='{1}' AND mocode='{2}' and itemcode='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), recNo, mocode, itemcode);

            object[] objs = this.DataProvider.CustomQuery(
                typeof(InvReceive), new SQLCondition(sql)
                );
            if (objs != null && objs.Length > 0)
            {
                return true;
            }
            else
                return false;
        }

        public void RemoveInvReceive(string recNo, int seq)
        {
            object obj = this.GetInvReceive(recNo, seq);
            InvReceive rec = obj as InvReceive;
            if (rec == null) return;

            if (rec.RecStatus != ReceiveStatus.Receiving)
                throw new NoAllowDeleteException();

            //删除序列号
            RemoveReceiveAllRCard(recNo, seq);

            this.DeleteInvReceive(rec);
        }

        public void RemoveRCard(string recNo, int seq)
        {

        }

        //更新入库单数量
        public void IncRecQty(string recNo, string RecSeq)
        {
            string sql = string.Format("update tblinvrec set actqty = actqty + 1 where recno = '{0}' and recseq = {1}",
                recNo, RecSeq);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        #endregion

        #region InvShip
        /// <summary>
        /// 
        /// </summary>
        public InvShip CreateNewInvShip()
        {
            return new InvShip();
        }

        public void AddInvShip(InvShip invShip)
        {
            invShip.ShipSeq = this.GetNextInvShipSeq(invShip.ShipNo);
            this._helper.AddDomainObject(invShip);
        }

        public void AddInvShip2(InvShip invShip)
        {
            this._helper.AddDomainObject(invShip);
        }

        public void UpdateInvShip(InvShip invShip)
        {
            this._helper.UpdateDomainObject(invShip);
        }


        public void DeleteInvShip(InvShip invShip)
        {
            this._helper.DeleteDomainObject(invShip);
        }

        public void DeleteInvShip(InvShip[] invShip)
        {
            this._helper.DeleteDomainObject(invShip);
        }

        public object GetInvShip(string shipNo, decimal shipSeq)
        {
            return this.DataProvider.CustomSearch(typeof(InvShip), new object[] { shipNo, shipSeq });
        }

        public int QueryInvShipCount(string shipNo, decimal shipSeq)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVSHIP where 1=1 and SHIPNO like '{0}%'  and SHIPSEQ like '{1}%' ", shipNo, shipSeq)));
        }

        public int QueryInvShipPartnerCount(string shipno, string partner)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVSHIP where 1=1 and partnercode ='{0}' and shipno='{1}'", partner, shipno)));
        }

        public object[] QueryInvShip(string shipNo, decimal shipSeq, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(InvShip), new PagerCondition(string.Format("select {0} from TBLINVSHIP where 1=1 and SHIPNO like '{1}%'  and SHIPSEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)), shipNo, shipSeq), "SHIPNO,SHIPSEQ", inclusive, exclusive));
        }

        public object[] QueryInvShip(string shipNo)
        {
            return this.DataProvider.CustomQuery(typeof(InvShip), new SQLCondition(string.Format("select {0} from TBLINVSHIP where 1=1 and SHIPNO ='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)), shipNo)));
        }

        public object[] QueryInvShip(string shipNo, string itemcode)
        {
            return this.DataProvider.CustomQuery(typeof(InvShip), new SQLCondition(string.Format("select {0} from TBLINVSHIP where 1=1 and SHIPNO ='{1}' and itemcode='{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)), shipNo, itemcode)));
        }

        public object[] QueryInvShip(string shipNo, string itemcode, string partnercode)
        {
            return this.DataProvider.CustomQuery(typeof(InvShip), new SQLCondition(string.Format("select {0} from TBLINVSHIP where 1=1 and SHIPNO ='{1}' and itemcode='{2}' and partnercode='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)), shipNo, itemcode, partnercode)));
        }

        public object[] GetAllInvShip()
        {
            return this.DataProvider.CustomQuery(typeof(InvShip), new SQLCondition(string.Format("select {0} from TBLINVSHIP order by SHIPNO,SHIPSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)))));
        }

        private int GetNextInvShipSeq(string shipNo)
        {
            try
            {
                int seq = this.DataProvider.GetCount(new SQLCondition(string.Format("SELECT max(SHIPSEQ) FROM TBLINVSHIP WHERE SHIPNO='{0}'", shipNo))); ;
                return seq + 1;
            }
            catch
            {
                return 1;
            }
        }

        //删除已经存在的出货单
        public void DeleteInvShip(string shipNo, string itemcode, string partnercode)
        {
            this.DataProvider.CustomExecute(new SQLCondition(string.Format("delete from tblinvship where shipNo='{0}' and itemcode='{1}' and PartnerCode='{2}'", shipNo, itemcode, partnercode)));
        }

        //完成出货单
        public void CompleteInvShip(string shipNo, string strUser)
        {
            object[] objs = this.QueryInvShip(shipNo);

            if (objs != null && objs.Length > 0)
            {
                foreach (object obj in objs)
                {
                    InvShip ship = obj as InvShip;
                    if (ship != null)
                    {
                        if (ship.ShipStatus == ShipStatus.Shipped)
                            throw new Exception("$Inv_Ship_Has_Finished"); //出货单已完成,不需要重复完成

                        if (ship.PlanQty != ship.ActQty)
                            throw new Exception("$Inv_Plan_NQ_ACT"); //计划数量和实际数量不符，不能执行完成

                        //ship.ShipDate = FormatHelper.TODateInt(DateTime.Now);
                        //ship.ShipTime = FormatHelper.TOTimeInt(DateTime.Now);
                        ship.ShipUser = strUser;
                        ship.ShipStatus = ShipStatus.Shipped;

                        this.UpdateInvShip(ship);

                    }
                }
            }
            else
            {
                throw new Exception("$Inv_Ship_Not_Exist");
            }
        }

        //取消完成出货单
        public void UnCompleteInvShip(string shipNo, string strUser)
        {
            object[] objs = this.QueryInvShip(shipNo);

            if (objs != null && objs.Length > 0)
            {
                foreach (object obj in objs)
                {
                    InvShip ship = obj as InvShip;
                    if (ship != null)
                    {
                        if (ship.ShipStatus == ShipStatus.Shipping)
                            throw new Exception("$Inv_Ship_Not_Finished");//出货单还没有完成,不需要取消

                        //ship.ShipDate = 0;
                        //ship.ShipTime = 0;
                        ship.ShipUser = string.Empty;
                        ship.ShipStatus = ShipStatus.Shipping;

                        this.UpdateInvShip(ship);

                    }
                }
            }
            else
            {
                throw new Exception("$Inv_Ship_Not_Exist"); //这个出货单不存在
            }
        }

        public void IncShipQty(string shipno, string shipseq)
        {
            string sql = string.Format("update tblinvship set actqty=actqty + 1 where shipno='{0}' and shipseq='{1}'", shipno, shipseq);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        #endregion

        #region InvRCard
        /// <summary>
        /// 
        /// </summary>
        /// 
        public object[] GetMORCard(string rcard)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.MORunningCard),
                new SQLCondition(string.Format("select * from tblmorcard where morcardstart='{0}'", rcard)));
        }
        //检查ＲＣＡＲＤ是否已经入库
        public bool IsInv(string rcard, string mocode)
        {
            int c = this.DataProvider.GetCount(new SQLCondition(
                string.Format("select count(*) from tblinvrcard where rcard='{0}' and mocode='{1}'", rcard, mocode)
                ));
            if (c > 0)
                return true;
            else
                return false;
        }

        public InvRCard CreateNewInvRCard()
        {
            return new InvRCard();
        }

        public void AddInvRCard(InvRCard invRCard)
        {
            this._helper.AddDomainObject(invRCard);
        }

        public void UpdateInvRCard(InvRCard invRCard)
        {
            this._helper.UpdateDomainObject(invRCard);
        }

        public void DeleteInvRCard(InvRCard invRCard)
        {
            this._helper.DeleteDomainObject(invRCard);
        }

        public void DeleteInvRCard(InvRCard[] invRCard)
        {
            this._helper.DeleteDomainObject(invRCard);
        }

        public object GetInvRCard(string runningCard, string recNO)
        {
            return this.DataProvider.CustomSearch(typeof(InvRCard), new object[] { runningCard, recNO });
        }

        public int QueryInvRCardCount(string runningCard, string ItemCode, int Date)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVRCARD where 1=1 and RCARD <> '{0}'  and ItemCode = '{1}' and recdate < {2} AND RCARDSTATUS='{3}'",
                runningCard, ItemCode, Date, RCardStatus.Received)));
        }
        //Laws Lu,2006/08/25 support fifo
        public object[] QueryFIFOInvRCardByRcard(string runningCard, string ItemCode, int Date)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and RCARD <> '{1}'  and ItemCode = '{2}' and recdate < {3} AND RCARDSTATUS='{4}'  order by recdate,rectime",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), runningCard, ItemCode, Date, RCardStatus.Received)));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            else
            {
                return null;
            }
        }
        //Laws Lu,2006/08/25 support fifo
        //		public object[] QueryFIFOInvRCardByRcard(string runningCard,int Date)
        //		{
        //			object[] objs = this.DataProvider.CustomQuery(typeof(InvRCard),new SQLCondition(string.Format(
        //				"select {0} from TBLINVRCARD where 1=1 and RCARD <> '{1}'  and recdate < {2} AND RCARDSTATUS='{3}' "
        //				+ " and itemcode in (select itemcode from tblinvrcard where rcard='" + runningCard + "')"
        //				+ " order by recdate,rectime" ,
        //				DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)),runningCard, Date,RCardStatus.Received)));
        //			
        //			if(objs != null && objs.Length > 0)
        //			{
        //				return objs;
        //			}
        //			else
        //			{
        //				return null;
        //			}
        //		}
        //Laws Lu,2006/08/25 support fifo	
        public object[] QueryFIFOInvRCardByCarton(string cartonCode, string ItemCode, int Date)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and CARTONCODE <> '{1}'  and ItemCode = '{2}' and recdate < {3} AND RCARDSTATUS='{4}' order by recdate,rectime",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), cartonCode, ItemCode, Date, RCardStatus.Received)));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            else
            {
                return null;
            }
        }

        //Laws Lu,2006/08/25 support fifo	
        public object[] QueryFIFOInvRCardByItem(string itemCodes, int Date)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(
                string.Format("select {0} from TBLINVRCARD where 1=1 and itemCode in ('{1}') and recdate < {2} AND RCARDSTATUS='{3}'"
            + " order by recdate,rectime",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), itemCodes, Date, RCardStatus.Received)));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            else
            {
                return null;
            }
        }

        public int QueryInvRCardByCartonCount(string CartonCode, string ItemCode, int Date)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVRCARD where 1=1 and CartonCode <> '{0}'  and ItemCode = '{1}' and recdate < {2} AND RCARDSTATUS='{3}'",
                CartonCode, ItemCode, Date, RCardStatus.Received)));
        }

        public int QueryInvRCardCount(string runningCard, decimal recNO)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVRCARD where 1=1 and RCARD like '{0}%'  and RECNO like '{1}%' ", runningCard, recNO)));
        }

        public bool CheckRecInv(string recNO, string cartoncode, string runningCard)
        {
            string sql = "select count(*) from TBLINVRCARD where 1=1 and RECNO = '" + recNO + "'";
            if (cartoncode.Trim() != string.Empty)
            {
                sql += " and cartoncode = '" + cartoncode.Trim().ToUpper() + "'";
            }
            if (runningCard.Trim() != string.Empty)
            {
                sql += " and rcard = '" + runningCard.Trim().ToUpper() + "'";
            }

            int c = this.DataProvider.GetCount(new SQLCondition(sql));

            return c > 0;
        }

        public bool CheckShipInv(string shipno, string cartoncode, string runningCard)
        {
            string sql = "select count(*) from TBLINVRCARD where 1=1 and shipno = '" + shipno + "'";
            if (cartoncode.Trim() != string.Empty)
            {
                sql += " and cartoncode = '" + cartoncode.Trim().ToUpper() + "'";
            }
            if (runningCard.Trim() != string.Empty)
            {
                sql += " and rcard = '" + runningCard.Trim().ToUpper() + "'";
            }

            int c = this.DataProvider.GetCount(new SQLCondition(sql));

            return c > 0;
        }

        public int GetInvRCardCount(string recno, int seq)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVRCARD where 1=1 and recno = '{0}'  and RECseq = {1} ", recno, seq)));
        }

        public int GetCartonCount(string recno)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from (select distinct recno,cartoncode from TBLINVRCARD where 1=1 and recno = '{0}' and reccollecttype='Carton')", recno)));
        }

        public int GetCartonShipCount(string shipno)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from (select distinct shipno,cartoncode from TBLINVRCARD where 1=1 and shipno = '{0}' and shipcollecttype='Carton')", shipno)));
        }

        public object[] QueryInvRCard(string runningCard, decimal recNO, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new PagerCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and RCARD like '{1}%'  and RECNO like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), runningCard, recNO), "RCARD,RECNO", inclusive, exclusive));
        }

        public object[] QueryInvRCard(string recNo)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and RCARD = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), recNo)));
        }

        //根据ＲＣＡＲＤ查找
        public object[] QueryInvRCard2(string rcard)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and RCARD = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), rcard)));
        }
        //根据ＲＣＡＲＤ查找
        public object[] QueryLastInvRCard(string rcard)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where RCARD = '{1}' and RCARDSTATUS='Received'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), rcard)));
        }

        //根据入库单号和入库单序号查找ＲＣＡＲＤ
        public object[] QueryInvRCard(string recNO, int seq)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and RECNO = '{1}' and RECSEQ={2} order by RCard", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), recNO, seq)));
        }

        //根据出货单号和出货单序号查找ＲＣＡＲＤ
        public object[] QueryInvRCard3(string shipNo, int seq)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and shipNo = '{1}' and shipSEQ={2} order by RCard", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), shipNo, seq)));
        }



        //根据出货单号和出货单序号查找ＲＣＡＲＤ
        public object[] QueryInvRCard4(string shipNo, string rcard)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and shipNo = '{1}' and rcard='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), shipNo, rcard)));
        }

        //根据出货单号和出货单序号和ＲＣＡＲＤ　查找ＲＣＡＲＤ
        public object[] QueryInvRCard5(string shipNo, int seq, string rcard)
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and shipNo = '{1}' and SHIPSEQ={2} and rcard='{3}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), shipNo, seq, rcard)));
        }

        public object[] GetAllInvRCard()
        {
            return this.DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD order by RCARD,RECNO", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)))));
        }

        public object[] GetInvRCard(int recNo, string rcard)
        {
            return this.DataProvider.CustomQuery(typeof(InventoryRCard), new SQLCondition(string.Format("select {0} from TBLINVRCARD where 1=1 and RECNO = '{1}' and RCARD = '{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InventoryRCard)), recNo, rcard)));
        }

        //		//增加一个新的入库RCard,同进更新Receive
        //		public object[] AddInvRCard(string recNo,string rcard,string user,SimulateResult sr,CollectionType c_type)
        //		{	
        //			//检查产品代码是否相同
        //			InvReceive rec = this.GetInvReceive(recNo,sr.ItemCode);
        //			if(rec == null)
        //				throw new Exception(rcard +"$Inv_Rcard_ItemCode_Error"); //序列号的产品代码不符
        // 
        //			//检查状态
        //			if(rec.RecStatus!= ReceiveStatus.Receiving)
        //				throw new NoAllowAddException();
        //
        //			//检测RCard 是否已经存在
        //			InvRCard inv = this.GetInvRCard(rcard,recNo) as InvRCard;
        //			if(inv != null)
        //				throw new RCardExistException(rcard);
        //
        //			rec.ActQty += 1;
        //		
        //			if(rec.ActQty > rec.PlanQty)
        //				throw new GreaterException();
        //
        //			this.UpdateInvReceive(rec);
        //
        //			//添加一个新的ＲCard
        //			inv = new InvRCard();
        //			inv.ItemCode = sr.ItemCode;
        //			inv.MOCode = sr.MOCode;
        //			inv.MaintainDate = FormatHelper.TODateInt(DateTime.Now);
        //			inv.MaintainTime = FormatHelper.TOTimeInt(DateTime.Now);
        //			inv.MaintainUser = user;
        //			inv.RCardStatus = RCardStatus.Received;
        //			inv.ReceiveDate = inv.MaintainDate;
        //			inv.ReceiveTime = inv.MaintainTime;
        //			inv.ReceiveUser = inv.MaintainUser;
        //			inv.RecNO = rec.RecNo;
        //			inv.RecSeq = rec.RecSeq;
        //			inv.RunningCard = rcard;
        //			inv.ShipDate = 0;
        //			inv.ShipTime = 0;
        //			inv.ShipUser = string.Empty;
        //			inv.RecCollectType = c_type.ToString();
        //			this.AddInvRCard(inv);
        //
        //
        //			object[] results = new object[2];
        //			results[0] = rec;
        //			results[1] = inv;
        //			return results;
        //		}

        public const string NewSRNO = "NEW_SRNO";
        public object[] AddInvRCard2(InvReceive rec, string rcard, string user, SimulateResult sr, CollectionType c_type, string cartonno)
        {
            //检查状态
            if (rec.RecStatus != ReceiveStatus.Receiving)
                throw new NoAllowAddException();

            //检测RCard 在工单是否已经存在
            InvRCard inv = this.GetInvRCard(rcard, rec.RecNo) as InvRCard;
            if (inv != null)
                throw new RCardExistException(rcard);

            //检查未出货的RCard是否唯一
            CheckUniqueRCard(rcard, RCardStatus.Received);

            rec.ActQty += 1;

            if (rec.ActQty > rec.PlanQty)
                throw new GreaterException();

            this.IncRecQty(rec.RecNo, rec.RecSeq.ToString());

            //添加一个新的ＲCard
            inv = new InvRCard();
            inv.ItemCode = sr.ItemCode;
            inv.MOCode = sr.MOCode;
            //2006/11/17,Laws Lu add get DateTime from db Server
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

            inv.MaintainDate = FormatHelper.TODateInt(dtNow);
            inv.MaintainTime = FormatHelper.TOTimeInt(dtNow);

            inv.MaintainUser = user;
            inv.RCardStatus = RCardStatus.Received;
            inv.ReceiveDate = inv.MaintainDate;
            inv.ReceiveTime = inv.MaintainTime;
            inv.ReceiveUser = inv.MaintainUser;
            inv.RecNO = rec.RecNo;
            inv.RecSeq = rec.RecSeq;
            inv.RunningCard = rcard;
            inv.ShipDate = 0;
            inv.ShipTime = 0;
            inv.ShipUser = string.Empty;
            inv.RecCollectType = c_type.ToString();
            inv.CartonCode = cartonno;
            inv.INVRardType = INVRardType.Normal;
            //Laws Lu,2006/10/11 ERP资料抛转
            //			DataProvider.BeginTransaction();
            //			try
            //			{
            // Added by Icyer 2007/07/02
            // 增加MOSeq
            MOFacade moFacade = new MOFacade(this.DataProvider);
            MO mo = (MO)moFacade.GetMO(inv.MOCode);
            inv.MOSeq = mo.MOSeq;
            // Added end
            this.AddInvRCard(inv);

            ERPINVInterface erpInterface = new ERPINVInterface();

            //			string factory = "PO";
            //			if (System.Configuration.ConfigurationSettings.AppSettings["InvFactory"] != null)
            //			{
            //				factory = System.Configuration.ConfigurationSettings.AppSettings["InvFactory"].Trim();
            //			}

            object erpInter = this.GetERPINVInterface(inv.RecNO, inv.MOCode, Web.Helper.INVERPType.INVERPTYPE_NEW, NewSRNO);
            if (erpInter != null)
            {
                this.UpdateInvERPQty(erpInter as ERPINVInterface);
            }
            else
            {
                //erpInterface.SRNO = GetMaxSRNO(int.Parse(DateTime.Now.Year.ToString().Substring(2,2)),factory);

                erpInterface.MOCODE = inv.MOCode;
                erpInterface.RECNO = inv.RecNO;
                erpInterface.QTY = 1;
                erpInterface.MUSER = inv.MaintainUser;
                erpInterface.MDATE = inv.MaintainDate;
                erpInterface.MTIME = inv.MaintainTime;
                erpInterface.ITEMCODE = inv.ItemCode;
                erpInterface.STATUS = INVERPType.INVERPTYPE_NEW;
                erpInterface.SRNO = NewSRNO;

                this.AddERPINVInterface(erpInterface);
            }

            //				DataProvider.CommitTransaction();
            //
            //			}
            //			catch(Exception ex)
            //			{
            //				Log.Error(ex.Message);
            //				DataProvider.RollbackTransaction();
            //			}
            //			finally
            //			{
            //				((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
            //			}

            object[] results = new object[2];
            results[0] = rec;
            results[1] = inv;
            return results;
        }

        //删除一个序列号
        public object[] RemoveReceiveRCard(string recNo, string rcard, string c_type)
        {
            //判断ＲＣＡＲＤ状态
            InvRCard inv = null;
            InvReceive rec = null;
            inv = this.GetInvRCard(rcard, recNo) as InvRCard;
            if (inv != null)
            {
                //删除ＲＣＡＲＤ
                if (inv.RCardStatus != RCardStatus.Received)
                    throw new NoDelShippedRCardException(inv.RunningCard);

                if (inv.RecCollectType != c_type)
                    throw new Exception("$INV_REC_COLLECT_TYPE_ERROR");

                this.DeleteInvRCard(inv);
                //Laws Lu,2006/11/10 add When remove a rcard from rec,substract the quantity from erp interface 
                object erpInter = this.GetERPINVInterface(inv.RecNO, inv.MOCode, Web.Helper.INVERPType.INVERPTYPE_NEW, NewSRNO);
                if (erpInter != null)
                {
                    this.SubstractInvERPQty(erpInter as ERPINVInterface);
                }

                //判断入库单状态
                rec = this.GetInvReceive(recNo, (int)inv.RecSeq) as InvReceive;
                if (rec != null)
                {
                    if (rec.RecStatus != ReceiveStatus.Receiving)
                        throw new NoAllowDeleteException();

                    rec.ActQty -= 1;

                    if (rec.ActQty < 0)
                        rec.ActQty = 0;

                    this.UpdateInvReceive(rec);
                }
            }
            return new object[] { rec, inv };
        }

        //删除明细下的所有的序列号
        public object[] RemoveReceiveAllRCard(string recNo, int seq)
        {
            //判断入库音状态
            InvReceive rec = this.GetInvReceive(recNo, seq) as InvReceive;
            InvRCard inv = null;
            if (rec != null)
            {
                if (rec.RecStatus != ReceiveStatus.Receiving)
                    throw new NoAllowDeleteException();

                //取得这个明细下的所有的序列号
                object[] objs = this.QueryInvRCard(rec.RecNo, rec.RecSeq);
                if (objs != null && objs.Length > 0)
                {
                    foreach (object obj in objs)
                    {
                        inv = obj as InvRCard;
                        if (inv != null)
                        {
                            //判断ＲＣＡＲＤ状态
                            if (inv.RCardStatus != RCardStatus.Received)
                                throw new NoDelShippedRCardException(inv.RunningCard);

                            //删除ＲＣＡＲＤ
                            this.DeleteInvRCard(inv);

                            //Laws Lu,2006/11/10 add When remove a rcard from rec,substract the quantity from erp interface 
                            object erpInter = this.GetERPINVInterface(inv.RecNO, inv.MOCode, Web.Helper.INVERPType.INVERPTYPE_NEW, NewSRNO);
                            if (erpInter != null)
                            {
                                this.SubstractInvERPQty(erpInter as ERPINVInterface);
                            }
                        }
                        //更新InvReceive

                        rec.ActQty -= 1;

                        if (rec.ActQty < 0)
                            rec.ActQty = 0;
                    }
                }
                this.UpdateInvReceive(rec);
            }

            return new object[] { rec, inv };
        }

        public void CompleteInvReceive(string recNo, string strUser)
        {
            object[] objs = QueryAllInvReceive(recNo);

            if (objs != null && objs.Length > 0)
            {
                foreach (object obj in objs)
                {
                    InvReceive rec = obj as InvReceive;
                    if (rec != null)
                    {
                        if (rec.RecStatus != ReceiveStatus.Receiving)
                            throw new Exception("$Error_Rec_Has_Finished");

                        if (rec.PlanQty != rec.ActQty)
                            throw new Exception("$Inv_Plan_NQ_ACT");//计划数量和实际数量不符，不能执行完成

                        //2006/11/17,Laws Lu add get DateTime from db Server
                        DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                        DateTime dtNow = FormatHelper.ToDateTime(dbDateTime.DBDate, dbDateTime.DBTime);

                        rec.ReceiveDate = FormatHelper.TODateInt(dtNow);
                        rec.ReceiveTime = FormatHelper.TOTimeInt(dtNow);
                        rec.ReceiveUser = strUser;
                        rec.RecStatus = ReceiveStatus.Received;

                        this.UpdateInvReceive(rec);

                    }
                }
            }
            else
            {
                throw new Exception("$Error_CS_Stock_In_Ticket_Not_Exist");
            }
        }

        public void UnCompleteInvReceive(string recNo, string strUser)
        {
            object[] objs = QueryAllInvReceive(recNo);

            if (objs != null && objs.Length > 0)
            {
                foreach (object obj in objs)
                {
                    InvReceive rec = obj as InvReceive;
                    if (rec != null)
                    {
                        if (rec.RecStatus != ReceiveStatus.Received)
                            throw new Exception("$Inv_Rec_Not_Finished");//入库单还没有完成，不需要取消

                        rec.ReceiveDate = 0;
                        rec.ReceiveTime = 0;
                        rec.ReceiveUser = string.Empty;
                        rec.RecStatus = ReceiveStatus.Receiving;

                        this.UpdateInvReceive(rec);

                    }
                }
            }
            else
            {
                throw new Exception("$Error_CS_Stock_In_Ticket_Not_Exist");
            }
        }

        //检查未出货的RCard是否唯一
        public void CheckUniqueRCard(string rcard, string status)
        {
            string sqlcheck = string.Format("select count(*) from tblinvrcard where rcard = '{0}' and rcardstatus = '{1}'",
                rcard, status);
            int unshippedcount = this.DataProvider.GetCount(new SQLCondition(sqlcheck));
            if (unshippedcount > 0)
                throw new Exception(rcard + " $Error_Rcard_Exist_and_Shipped");//序列号已经存在，并且没有出货
        }
        #endregion

        #region Web查询部分

        #region 入库明细查询

        #region 入库明细的一级查询语句
        public string GetRecSqlWhere(string cartonno, string recNo, string status, string datefrom, string dateto, string model,
            string itemcode, string rcardfrom, string rcardto, string invRecType, string mocode)
        {
            string sqlwhere = string.Format(@"select distinct r.recno,r.recseq from tblinvrec r inner join tblinvrcard c " +
                " on r.recno = c.recno and r.recseq=c.recseq /*inner join tblsimulation s on s.rcard= c.rcard*/ where 1=1 ");
            if (cartonno != null && cartonno != string.Empty)
            {
                sqlwhere += " and c.cartoncode like  '" + cartonno + "%'";
            }

            if (invRecType != null && invRecType != string.Empty)
            {
                sqlwhere += string.Format(" and r.RECTYPE = '{0}' ", invRecType);
            }
            if (recNo != null && recNo != string.Empty)
            {
                sqlwhere += string.Format(" and r.RECNO like '{0}%' ", recNo.ToUpper());
            }
            if (status != null && status != string.Empty)
            {
                sqlwhere += string.Format(" and c.RCardStatus like '{0}%' ", status);
            }
            if (model != null && model != string.Empty)
            {
                sqlwhere += string.Format(" and r.ModelCode like '{0}%' ", model.ToUpper());
            }
            if (itemcode != null && itemcode != string.Empty)
            {
                sqlwhere += string.Format(" and r.ItemCode in ({0}) ", GetRangeSql(itemcode.ToUpper().Split(',')));
            }

            if (mocode != null && mocode != string.Empty)
            {
                sqlwhere += string.Format(" and c.mocode in ({0} )", GetRangeSql(mocode.ToUpper().Split(',')));
            }

            /* modified by jessie lee,2006/1/10,CS218 */
            sqlwhere += FormatHelper.GetDateRangeSql("c.RecDate", FormatHelper.TODateInt(datefrom), FormatHelper.TODateInt(dateto));
            sqlwhere += FormatHelper.GetRCardRangeSql("rcard", rcardfrom, rcardto);
            return sqlwhere;
        }

        public object[] QueryInvReceiveWeb(string cartonno, string recNo, string status, string datefrom, string dateto,
            string model, string itemcode, string rcardfrom, string rcardto, string invRecType, string mocode,
            int inclusive, int exclusive)
        {
            //如果需要从序列号列表中查询数据
            if (cartonno != string.Empty || status != string.Empty || datefrom != string.Empty || dateto != string.Empty ||
                rcardfrom != string.Empty || rcardto != string.Empty || mocode != string.Empty)
            {
                string sqlwhere = this.GetRecSqlWhere(cartonno, recNo, status, datefrom, dateto, model, itemcode, rcardfrom,
                    rcardto, invRecType, mocode);

                string sql = string.Format("select {0} from TBLINVREC where (recno,recseq) in({1})", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)), sqlwhere);

                return this.DataProvider.CustomQuery(typeof(InvReceive), new PagerCondition(sql, "RECNO,RECSEQ,mdate,mtime", inclusive, exclusive));
            }
            else//只需要从入库单中查询数据
            {

                string sqlwhere = string.Format("select {0} from TBLINVREC r where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)));
                if (invRecType != null && invRecType != string.Empty)
                {
                    sqlwhere += string.Format(" and r.RECTYPE = '{0}' ", invRecType);
                }
                if (recNo != null && recNo != string.Empty)
                {
                    sqlwhere += string.Format(" and r.RECNO like '{0}%' ", recNo.ToUpper());
                }
                if (model != null && model != string.Empty)
                {
                    sqlwhere += string.Format(" and r.ModelCode like '{0}%' ", model.ToUpper());
                }
                if (itemcode != null && itemcode != string.Empty)
                {
                    sqlwhere += string.Format(" and r.ItemCode in ({0}) ", GetRangeSql(itemcode.ToUpper().Split(',')));
                }
                if (mocode != null && mocode != string.Empty)
                {
                    sqlwhere += string.Format(" and r.RECNO in (select RECNO from TBLINVREC where mocode in ({0})) ", GetRangeSql(mocode.ToUpper().Split(',')));
                }
                return this.DataProvider.CustomQuery(typeof(InvReceive), new PagerCondition(sqlwhere, "RECNO,RECSEQ,mdate,mtime", inclusive, exclusive));
                //return this.DataProvider.CustomQuery(typeof(InvReceive), new PagerCondition(string.Format("select {0} from TBLINVREC r where  r.RECNO like '{1}%' and r.ModelCode like '{2}%' and r.ItemCode like '{3}%' and RECTYPE = '{4}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvReceive)),recNo.ToUpper(),model.ToUpper(),itemcode.ToUpper(),invRecType), "RECNO,RECSEQ,mdate,mtime", inclusive, exclusive));
            }
        }

        private string GetRangeSql(string[] values)
        {
            return "'" + string.Join("','", values) + "'";
        }

        public int QueryInvReceiveWebCount(string cartonno, string recNo, string status, string datefrom, string dateto,
            string model, string itemcode, string rcardfrom, string rcardto, string invRecType, string mocode)
        {
            //如果需要从序列号列表中查询数据
            if (cartonno != string.Empty || status != string.Empty || datefrom != string.Empty || dateto != string.Empty || rcardfrom != string.Empty || rcardto != string.Empty)
            {
                string sqlwhere = this.GetRecSqlWhere(cartonno, recNo, status, datefrom, dateto, model, itemcode, rcardfrom,
                    rcardto, invRecType, mocode);
                return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVREC where (recno,recseq) in({0})", sqlwhere)));
            }
            else//只需要从入库单中查询数据
            {
                return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVREC r where  r.RECNO like '{0}%' and r.ModelCode like '{1}%' and r.ItemCode in ({2}) and RECTYPE = '{3}'and r.mocode in ({4}) ",
                    recNo.ToUpper(), model.ToUpper(), GetRangeSql(itemcode.ToUpper().Split(',')), invRecType,
                    GetRangeSql(mocode.ToUpper().Split(',')))));
            }
        }

        public int QueryInvReceiveWebSum(string recNo, int recseq, string status, string datefrom, string dateto, string model, string itemcode, string rcardfrom, string rcardto)
        {
            string sqlwhere = "select count(*) from tblinvrec r inner join tblinvrcard c on r.recno = c.recno and r.recseq=c.recseq "
                + string.Format(" where r.RECNO = '{0}' and c.RCardStatus like '{1}%' and r.ModelCode like '{2}%' and r.ItemCode like '{3}%' and r.recseq={4}",
                recNo.ToUpper(), status, model.ToUpper(), itemcode.ToUpper(), recseq);

            if (datefrom != string.Empty)
                sqlwhere = sqlwhere + " and c.RecDate>=" + FormatHelper.TODateInt(datefrom);
            if (dateto != string.Empty)
                sqlwhere = sqlwhere + " and c.RecDate<=" + FormatHelper.TODateInt(dateto);

            if (rcardfrom != string.Empty)
                sqlwhere = sqlwhere + " and rcard >='" + rcardfrom.ToUpper() + "'";

            if (rcardto != string.Empty)
                sqlwhere = sqlwhere + " and rcard <='" + rcardto.ToUpper() + "'";

            return this.DataProvider.GetCount(new SQLCondition(sqlwhere));
        }

        #endregion

        #region 入库单的二级界面查询

        public string GetRecRCardSqlWhere(string recNo, string seq, string status, string datefrom, string dateto, string rcardfrom, string rcardto)
        {
            string sqlwhere = string.Format(" where r.RECNO like '{0}%' and r.rcardstatus like '{1}%' and r.recseq = {2} ",
                recNo.ToUpper(), status, seq);

            if (datefrom != string.Empty)
                sqlwhere = sqlwhere + " and r.RecDate>=" + FormatHelper.TODateInt(datefrom);
            if (dateto != string.Empty)
                sqlwhere = sqlwhere + " and r.recDate<=" + FormatHelper.TODateInt(dateto);
            if (rcardfrom != string.Empty)
                sqlwhere = sqlwhere + " and r.rcard >='" + rcardfrom.ToUpper() + "'";

            if (rcardto != string.Empty)
                sqlwhere = sqlwhere + " and r.rcard <='" + rcardto.ToUpper() + "'";

            return sqlwhere;
        }
        public object[] QueryInvRCardWeb(string recNo, string seq, string status, string datefrom, string dateto, string rcardfrom, string rcardto, int inclusive, int exclusive)
        {
            string sqlwhere = GetRecRCardSqlWhere(recNo, seq, status, datefrom, dateto, rcardfrom, rcardto);

            string sql = "select r.cartoncode, r.shipcollecttype, r.reccollecttype, r.shipuser, r.recdate, r.shipseq," +
                " r.shipno, r.muser, r.rcardstatus, r.recno, r.shiptime, r.itemcode, r.shipdate," +
                " r.mtime, r.recuser, r.rcard, r.rectime, r.ordernumber, r.mdate, r.recseq,PLANQTY,ACTQTY ,r.mocode " +
                " from TBLINVRCARD r   " +
                " left join " +
                " (select d.MOCode,d.moplanqty planqty,count(r.rcard) actqty from tblmo d," +
                "  tblinvrcard r  where d.mocode = r.mocode ";
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sql += "  and d.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
            }
            sql += "  group by d.mocode,d.moplanqty) e on e.mocode = r.mocode {0}";

            sql = string.Format(sql, sqlwhere);

            return this.DataProvider.CustomQuery(typeof(InvRCardForQuery), new PagerCondition(
                sql, "r.RCard", inclusive, exclusive));
        }

        public int QueryInvRCardWebCount(string recNo, string seq, string status, string datefrom, string dateto, string rcardfrom, string rcardto)
        {
            string sqlwhere = GetRecRCardSqlWhere(recNo, seq, status, datefrom, dateto, rcardfrom, rcardto);
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVRCARD r {0}", sqlwhere)));
        }

        #endregion

        #region 将入库单的一级和二级界面一起导出的查询
        public object[] QueryRecRCardWeb(string cartonno, string recNo, string status, string datefrom, string dateto, string model, string itemcode, string rcardfrom, string rcardto, string invRecType, int inclusive, int exclusive)
        {
            //			string sqlwhere =string.Format(" where RECNO like '{0}%' and RCardStatus like '{1}%' and ModelCode like '{2}%' and ItemCode like '{3}%' and RECTYPE = '{4}'", 
            //				recNo.ToUpper(),status,model.ToUpper(),itemcode.ToUpper(),invRecType);

            string view = @"(SELECT
                         R.RECNO RECNO,
                         R.RECSEQ RECSEQ,
                         R.RECTYPE RECTYPE,
						R.MODELCODE MODELCODE,
						R.ITEMCODE ITEMCODE,
						R.ITEMDESC ITEMDESC,
						R.RECSTATUS RECSTATUS,
						1 AS ACTQTY,
						R.RECDESC RECDESC,
						C.RCARD RCARD,
						C.RCARDSTATUS RCARDSTATUS,
						C.RECDATE RECDATE,
						C.RECUSER RECUSER,
						c.CartonCode CartonCode
						FROM TBLINVREC R
						INNER JOIN TBLINVRCARD C ON R.RECNO = C.RECNO AND R.RECSEQ = C.RECSEQ)";
            string sqlwhere = string.Format(" where 1=1 ");

            if (cartonno != null && cartonno != string.Empty)
            {
                sqlwhere += " and CartonCode like  '" + cartonno + "%'";
            }

            if (invRecType != null && invRecType != string.Empty)
            {
                sqlwhere += string.Format(" and RECTYPE = '{0}' ", invRecType);
            }
            if (recNo != null && recNo != string.Empty)
            {
                sqlwhere += string.Format(" and RECNO like '{0}%' ", recNo.ToUpper());
            }
            if (status != null && status != string.Empty)
            {
                sqlwhere += string.Format(" and RCardStatus like '{0}%' ", status);
            }
            if (model != null && model != string.Empty)
            {
                sqlwhere += string.Format(" and ModelCode like '{0}%' ", model.ToUpper());
            }
            if (itemcode != null && itemcode != string.Empty)
            {
                sqlwhere += string.Format(" and ItemCode in ({0}) ", GetRangeSql(itemcode.ToUpper().Split(',')));
            }

            sqlwhere += FormatHelper.GetDateRangeSql("RecDate", FormatHelper.TODateInt(datefrom), FormatHelper.TODateInt(dateto));
            sqlwhere += FormatHelper.GetRCardRangeSql("rcard", rcardfrom, rcardto);

            return this.DataProvider.CustomQuery(typeof(ReceiveRCard), new PagerCondition(string.Format("select {0} from {2} {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReceiveRCard)), sqlwhere, view), "RECNO,RECSEQ,rcard", inclusive, exclusive));
        }
        #endregion

        #endregion

        #region 出货明细查询

        #region 出货单的一级查询

        private string GetShipSqlWhere(string cartonno, string shipNo, string partner, string datefrom, string dateto,
            string model, string itemcode, string rcardfrom, string rcardto, string printdatefrom, string printdateto,
            string invShipType, string Mocode)
        {
            string sqlwhere = string.Format(@"select distinct r.shipno,r.shipseq from tblinvship r inner join tblinvrcard c on r.shipno = c.shipno and r.shipseq=c.shipseq " +
                " /*inner join tblsimulation s on s.rcard=c.rcard */ where 1=1 ");

            if (cartonno != null && cartonno != string.Empty)
            {
                sqlwhere += " and c.cartoncode like  '" + cartonno + "%'";
            }

            if (invShipType != null && invShipType != string.Empty)
            {
                sqlwhere += string.Format(" and r.SHIPTYPE = '{0}' ", invShipType);
            }
            if (shipNo != null && shipNo != string.Empty)
            {
                sqlwhere += string.Format(" and r.shipno like '{0}%' ", shipNo.ToUpper());
            }
            if (partner != null && partner != string.Empty)
            {
                sqlwhere += string.Format(" and r.partnercode like '{0}%' ", partner.ToUpper());
            }
            if (model != null && model != string.Empty)
            {
                sqlwhere += string.Format(" and r.ModelCode like '{0}%' ", model.ToUpper());
            }
            if (itemcode != null && itemcode != string.Empty)
            {
                sqlwhere += string.Format(" and r.ItemCode in ({0}) ", GetRangeSql(itemcode.ToUpper().Split(',')));
            }
            if (Mocode != null && Mocode != string.Empty)
            {
                sqlwhere += string.Format(" and c.Mocode in ({0}) ", GetRangeSql(Mocode.ToUpper().Split(',')));
            }

            /* modified by jessie lee,2006/1/10,CS218 */
            sqlwhere += FormatHelper.GetDateRangeSql("c.ShipDate", FormatHelper.TODateInt(datefrom), FormatHelper.TODateInt(dateto));
            sqlwhere += FormatHelper.GetRCardRangeSql("rcard", rcardfrom.Trim().ToUpper(), rcardto.Trim().ToUpper());
            sqlwhere += FormatHelper.GetDateRangeSql("printdate", FormatHelper.TODateInt(printdatefrom.Trim()), FormatHelper.TODateInt(printdateto.Trim()));
            return sqlwhere;
        }

        public object[] QueryInvShipWeb(string cartonno, string shipNo, string partner, string datefrom, string dateto, string model, string itemcode, string rcardfrom, string rcardto, string printdatefrom, string printdateto, string invShipType,
            string Mocode, int inclusive, int exclusive)
        {
            if (cartonno != string.Empty || datefrom != string.Empty || dateto != string.Empty || rcardfrom != string.Empty || rcardto != string.Empty || Mocode != string.Empty)
            {
                string sqlwhere = GetShipSqlWhere(cartonno, shipNo, partner, datefrom, dateto, model, itemcode, rcardfrom, rcardto, printdatefrom, printdateto, invShipType, Mocode);

                string sql = string.Format("select {0} from TBLINVSHIP where (shipno,shipseq) in({1})", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)), sqlwhere);

                return this.DataProvider.CustomQuery(typeof(InvShip), new PagerCondition(sql, "shipno,shipSEQ,mdate,mtime", inclusive, exclusive));
            }
            else
            {
                /*
                string sql = string.Format("select {0} from TBLINVSHIP r where r.shipno like '{1}%' and r.partnercode like '{2}%'and r.ModelCode like '{3}%' and r.ItemCode like '{4}%' and r.SHIPTYPE = '{5}'", 
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)),shipNo.ToUpper(),partner.ToUpper(),model.ToUpper(),itemcode.ToUpper(),invShipType);
                */
                string sql = string.Format("select {0} from TBLINVSHIP r where 1=1 ",
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvShip)));
                if (invShipType != null && invShipType != string.Empty)
                {
                    sql += string.Format(" and r.SHIPTYPE = '{0}' ", invShipType);
                }
                if (shipNo != null && shipNo != string.Empty)
                {
                    sql += string.Format(" and r.shipno like '{0}%' ", shipNo.ToUpper());
                }
                if (partner != null && partner != string.Empty)
                {
                    sql += string.Format(" and r.partnercode like '{0}%' ", partner.ToUpper());
                }
                if (model != null && model != string.Empty)
                {
                    sql += string.Format(" and r.ModelCode like '{0}%' ", model.ToUpper());
                }
                if (itemcode != null && itemcode != string.Empty)
                {
                    sql += string.Format(" and r.ItemCode in ({0}) ", GetRangeSql(itemcode.ToUpper().Split(',')));
                }
                if (Mocode != null && Mocode != string.Empty)
                {
                    //sql += string.Format(" and r.Mocode like '{0}%' ",Mocode.ToUpper());
                    sql += string.Format(" and r.shipno in (select shipno from TBLINVREC r where r.mocode in ({0})) ",
                        GetRangeSql(Mocode.ToUpper().Split(',')));
                }

                sql += FormatHelper.GetDateRangeSql("r.printdate", FormatHelper.TODateInt(printdatefrom.Trim()), FormatHelper.TODateInt(printdateto.Trim()));

                return this.DataProvider.CustomQuery(typeof(InvShip), new PagerCondition(sql, "shipno,shipSEQ,mdate,mtime", inclusive, exclusive));
            }
        }


        public int QueryInvShipWebCount(string cartonno, string shipNo, string partner, string datefrom, string dateto,
            string model, string itemcode, string rcardfrom, string rcardto, string printdatefrom,
            string printdateto, string invShipType, string Mocode)
        {

            if (datefrom != string.Empty || dateto != string.Empty || rcardfrom != string.Empty || rcardto != string.Empty || Mocode != string.Empty)
            {
                string sqlwhere = GetShipSqlWhere(cartonno, shipNo, partner, datefrom, dateto, model, itemcode, rcardfrom,
                    rcardto, printdatefrom, printdateto, invShipType, Mocode);

                string sql = string.Format("select {0} from TBLINVSHIP where (shipno,shipseq) in({1})", " count(*) ", sqlwhere);

                return this.DataProvider.GetCount(new SQLCondition(sql));
            }
            else
            {
                string sql = string.Format("select {0} from TBLINVSHIP r where 1=1 ", " count(*) ");
                if (invShipType != null && invShipType != string.Empty)
                {
                    sql += string.Format(" and r.SHIPTYPE = '{0}' ", invShipType);
                }
                if (shipNo != null && shipNo != string.Empty)
                {
                    sql += string.Format(" and r.shipno like '{0}%' ", shipNo.ToUpper());
                }
                if (partner != null && partner != string.Empty)
                {
                    sql += string.Format(" and r.partnercode like '{0}%' ", partner.ToUpper());
                }
                if (model != null && model != string.Empty)
                {
                    sql += string.Format(" and r.ModelCode like '{0}%' ", model.ToUpper());
                }
                if (itemcode != null && itemcode != string.Empty)
                {
                    sql += string.Format(" and r.ItemCode in ({0}) ", GetRangeSql(itemcode.ToUpper().Split(',')));
                }
                if (Mocode != null && Mocode != string.Empty)
                {
                    sql += string.Format(" and r.shipno in (select shipno from TBLINVREC r where r.mocode in ({0})) ",
                        GetRangeSql(Mocode.ToUpper().Split(',')));
                }
                sql += FormatHelper.GetDateRangeSql("r.printdate", FormatHelper.TODateInt(printdatefrom.Trim()), FormatHelper.TODateInt(printdateto.Trim()));

                return this.DataProvider.GetCount(new SQLCondition(sql));
            }
        }

        public int QueryInvShipWebSum(string shipNo, int shipseq, string partner, string datefrom, string dateto, string model, string itemcode, string rcardfrom, string rcardto, string printdatefrom, string printdateto)
        {
            string sqlwhere = "select count(*) from tblinvship r inner join tblinvrcard c on r.shipNo = c.shipNo and r.shipseq=c.shipseq "
                + string.Format(" where r.shipNo = '{0}' and r.partnercode like '{1}%' and r.ModelCode like '{2}%' and r.ItemCode like '{3}%' and r.shipseq={4}",
                shipNo.ToUpper(), partner.ToUpper(), model.ToUpper(), itemcode.ToUpper(), shipseq);

            if (datefrom != string.Empty)
                sqlwhere = sqlwhere + " and c.RecDate>=" + FormatHelper.TODateInt(datefrom);
            if (dateto != string.Empty)
                sqlwhere = sqlwhere + " and c.RecDate<=" + FormatHelper.TODateInt(dateto);

            if (rcardfrom != string.Empty)
                sqlwhere = sqlwhere + " and rcard >='" + rcardfrom.ToUpper() + "'";

            if (rcardto != string.Empty)
                sqlwhere = sqlwhere + " and rcard <='" + rcardto.ToUpper() + "'";

            if (printdatefrom.Trim() != string.Empty)
                sqlwhere = sqlwhere + " and printdate>=" + FormatHelper.TODateInt(printdatefrom.Trim());

            if (printdateto.Trim() != string.Empty)
                sqlwhere = sqlwhere + " and printdate<=" + FormatHelper.TODateInt(printdateto.Trim());

            return this.DataProvider.GetCount(new SQLCondition(sqlwhere));
        }
        #endregion

        #region 出货单的二级查询
        private string GetShipRCardSqlWhere(string shipNo, string seq, string datefrom, string dateto, string rcardfrom, string rcardto)
        {
            string sqlwhere = string.Format(" where r.shipNo like '{0}%' and r.shipseq = {1}",
                shipNo, seq);

            if (datefrom != string.Empty)
                sqlwhere = sqlwhere + " and r.shipDate>=" + FormatHelper.TODateInt(datefrom);
            if (dateto != string.Empty)
                sqlwhere = sqlwhere + " and r.shipDate<=" + FormatHelper.TODateInt(dateto);

            if (rcardfrom != string.Empty)
                sqlwhere = sqlwhere + " and r.rcard >='" + rcardfrom.ToUpper() + "'";

            if (rcardto != string.Empty)
                sqlwhere = sqlwhere + " and r.rcard <='" + rcardto.ToUpper() + "'";

            return sqlwhere;
        }
        public object[] QueryInvShipRCardWeb(string shipNo, string seq, string datefrom, string dateto, string rcardfrom, string rcardto, int inclusive, int exclusive)
        {
            string sqlwhere = GetShipRCardSqlWhere(shipNo, seq, datefrom, dateto, rcardfrom, rcardto);

            //string sql =  string.Format("select {0} from TBLINVRCARD {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)),sqlwhere);
            string sql = @"select r.cartoncode, r.shipcollecttype, r.reccollecttype, r.shipuser, r.recdate, r.shipseq, " +
            "	 r.shipno, r.muser, r.rcardstatus, r.recno, r.shiptime, r.itemcode, r.shipdate, " +
            "	 r.mtime, r.recuser, r.rcard, r.rectime, r.ordernumber, r.mdate, r.recseq,PLANQTY,ACTQTY ,r.mocode  " +
            "	 from TBLINVRCARD r   " +
            "	 left join  " +
            "	 (select MOCode, " +
            "        planqty, " +
            "        sum(decode(rcardstatus, '{1}', actqty, 0)) actqty " +
            "   from (select d.MOCode, " +
            "                d.moplanqty planqty, " +
            "                count(r.rcard) actqty, " +
            "                r.rcardstatus " +
            "           from tblmo d, tblinvrcard r " +
            "          where d.mocode = r.mocode ";
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sql += "            and d.orgid in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
            }
            sql += "          group by d.mocode, d.moplanqty, r.rcardstatus) " +
                   "  group by MOCode, planqty) e on e.mocode = r.mocode {0}";

            sql = string.Format(sql, sqlwhere, RCardStatus.Shipped);

            return this.DataProvider.CustomQuery(typeof(InvRCardForQuery), new PagerCondition(sql, "r.RCard", inclusive, exclusive));
        }

        public int QueryInvShipRCardWebCount(string shipNo, string seq, string datefrom, string dateto, string rcardfrom, string rcardto)
        {
            string sqlwhere = GetShipRCardSqlWhere(shipNo, seq, datefrom, dateto, rcardfrom, rcardto);

            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVRCARD r {0}", sqlwhere)));
        }
        #endregion

        #region 一级和二级查询界面一起导出
        public object[] QueryShipRCardWeb(string cartonno, string shipNo, string partner, string datefrom, string dateto, string model, string itemcode, string rcardfrom, string rcardto, string printdatefrom, string printdateto, string invShipType, int inclusive, int exclusive)
        {
            string view = @"(select
							s.SHIPNO SHIPNO,
							S.SHIPSEQ SHIPSEQ,
							S.SHIPTYPE SHIPTYPE,
							S.MODELCODE MODELCODE,
							S.ITEMCODE ITEMCODE,
							S.ITEMDESC ITEMDESC,
							S.PARTNERCODE PARTNERCODE,
							S.PARTNERDESC PARTNERDESC,
							S.SHIPSTATUS SHIPSTATUS,
							S.SHIPDATE SHIPDATE,
							1 ACTQTY,
							S.SHIPDESC SHIPDESC,
							I.RCARD RCARD,
							I.RECNO RECNO,
							I.SHIPDATE RCARDSHIPDATE,
							I.SHIPUSER RCARDSHIPUSER,
							i.RCARDSTATUS RCARDSTATUS,
							S.PRINTDATE PRINTDATE,
							i.CartonCode cartoncode
							from tblinvship s
							inner join tblinvrcard i on s.shipno = i.shipno and s.shipseq = i.shipseq)";

            string sqlwhere = string.Format(" where 1=1 ");

            if (cartonno != null && cartonno != string.Empty)
            {
                sqlwhere += " and CartonCode like  '" + cartonno + "%'";
            }

            if (invShipType != null && invShipType != string.Empty)
            {
                sqlwhere += string.Format(" and SHIPTYPE = '{0}' ", invShipType);
            }
            if (shipNo != null && shipNo != string.Empty)
            {
                sqlwhere += string.Format(" and shipno like '{0}%' ", shipNo.ToUpper());
            }
            if (partner != null && partner != string.Empty)
            {
                sqlwhere += string.Format(" and partnercode like '{0}%' ", partner.ToUpper());
            }
            if (model != null && model != string.Empty)
            {
                sqlwhere += string.Format(" and ModelCode like '{0}%' ", model.ToUpper());
            }
            if (itemcode != null && itemcode != string.Empty)
            {
                sqlwhere += string.Format(" and ItemCode in ({0}) ", GetRangeSql(itemcode.ToUpper().Split(',')));
            }

            sqlwhere += FormatHelper.GetDateRangeSql("RCARDSHIPDATE", FormatHelper.TODateInt(datefrom), FormatHelper.TODateInt(dateto)); //出货日期查询明细的shipdate
            sqlwhere += FormatHelper.GetRCardRangeSql("rcard", rcardfrom.Trim().ToUpper(), rcardto.Trim().ToUpper());
            sqlwhere += FormatHelper.GetDateRangeSql("printdate", FormatHelper.TODateInt(printdatefrom.Trim()), FormatHelper.TODateInt(printdateto.Trim()));

            return this.DataProvider.CustomQuery(typeof(ShipRCard), new PagerCondition(string.Format("select {0} from {2} {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ShipRCard)), sqlwhere, view), "shipno,shipSEQ,rcard", inclusive, exclusive));
        }

        #endregion

        #endregion

        #endregion

        #region SwitchType
        public object[] GetInvRCardByRecNo(string RecNo)
        {
            return DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("SELECT {0} FROM tblinvrcard WHERE RecNo= '{1}' AND RCARDSTATUS='{2}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), RecNo.ToUpper(), RCardStatus.Received)));
        }

        public object[] GetInvRCardByRCard(string RCard)
        {
            return DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("SELECT {0} FROM tblinvrcard WHERE rcard= '{1}' AND RCARDSTATUS='{2}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), RCard.ToUpper(), RCardStatus.Received)));
        }

        public object[] GetInvRCardByCartonCode(string CartonCode)
        {
            return DataProvider.CustomQuery(typeof(InvRCard), new SQLCondition(string.Format("SELECT {0} FROM tblinvrcard WHERE CartonCode= '{1}' AND RCARDSTATUS='{2}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvRCard)), CartonCode.ToUpper(), RCardStatus.Received)));

        }

        public void SwitchRcardType(DataTable table)
        {
            try
            {
                this.DataProvider.BeginTransaction();
                foreach (DataRow row in table.Rows)
                {
                    string collectType = row["CollectType"].ToString();

                    if (collectType == CollectType.RevNO)
                    {
                        SwitchByRecNo(row);
                    }
                    else if (collectType == CollectType.Carton)
                    {
                        SwitchByCartonCode(row);
                    }
                    else if (collectType == CollectType.PCS)
                    {
                        SwitchByRCard(row);
                    }
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
            finally
            {
                ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).PersistBroker.CloseConnection();
            }
        }

        private void Switch(InvRCard rcard, string switchType)
        {
            if (switchType == SwitchType.NormalToUnnormal)
            {
                rcard.INVRardType = INVRardType.Unnormal;
            }
            else if (switchType == SwitchType.UnnormalToNormal)
            {
                rcard.INVRardType = INVRardType.Normal;
            }

            this._helper.UpdateDomainObject(rcard);
        }

        private void SwitchByRecNo(DataRow row)
        {
            object[] rcards = GetInvRCardByRecNo(row["Input"].ToString());

            if (rcards == null || rcards.Length == 0)
                throw new Exception("$RecNo_Not_Exists");

            string switchType = row["SwitchType"].ToString();

            foreach (InvRCard rcard in rcards)
            {
                CheckRCard(rcard, switchType);

                Switch(rcard, switchType);
            }
        }

        private void SwitchByRCard(DataRow row)
        {
            object[] rcards = GetInvRCardByRCard(row["Input"].ToString());

            if (rcards == null || rcards.Length == 0)
                throw new Exception("$CartonCode_Not_Exists");

            string switchType = row["SwitchType"].ToString();

            foreach (InvRCard rcard in rcards)
            {
                CheckRCard(rcard, switchType);

                Switch(rcard, switchType);
            }
        }

        private void SwitchByCartonCode(DataRow row)
        {
            object[] rcards = GetInvRCardByCartonCode(row["Input"].ToString());

            if (rcards == null || rcards.Length == 0)
                throw new Exception("$RCard_Not_Exists");

            string switchType = row["SwitchType"].ToString();

            foreach (InvRCard rcard in rcards)
            {
                CheckRCard(rcard, switchType);

                Switch(rcard, switchType);
            }
        }

        public void CheckRCard(InvRCard rcard, string switchType)
        {
            if (rcard.RCardStatus != RCardStatus.Received)
                throw new Exception("$Rcard_Has_Been_Shipped");

            if (switchType == SwitchType.NormalToUnnormal)
            {
                if (rcard.INVRardType != INVRardType.Normal)
                {
                    throw new Exception("$ReceiveRcard_Must_Be_Normal");
                }
            }
            else if (switchType == SwitchType.UnnormalToNormal)
            {
                if (rcard.INVRardType != INVRardType.Unnormal)
                {
                    throw new Exception("$ReceiveRcard_Must_Be_UnNormal");
                }
            }
        }
        #endregion

        #region SAPStorageInfo

        // Added By HI1/Venus.Feng on 20081027 for Hisense Version : Add Invertory for SAP Data Transfer
        public SAPStorageInfo CreateNewSAPStorageInfo()
        {
            return new SAPStorageInfo();
        }

        public void AddSAPStorageInfo(SAPStorageInfo sapStorageInfo)
        {
            this.DataProvider.Insert(sapStorageInfo);
        }

        public void UpdateSAPStorageInfo(SAPStorageInfo sapStorageInfo)
        {
            this.DataProvider.Update(sapStorageInfo);
        }

        public void DeleteSAPStorageInfo(SAPStorageInfo sapStorageInfo)
        {
            this.DataProvider.Delete(sapStorageInfo);
        }

        public object GetSAPStorageInfo(string itemCode, int orgID, string storageId, string itemGrade)
        {
            return this.DataProvider.CustomSearch(typeof(SAPStorageInfo), new object[] { itemCode, orgID, storageId, itemGrade });
        }

        public void DeleteSAPStorageInfoByItemCode(string itemCode)
        {
            string sql = "DELETE FROM tblsapstorageinfo WHERE itemcode='" + itemCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void DeleteSAPStorageInfo(string[] orgIDArray, string[] storageIDArray, string itemCode)
        {
            string sql = "DELETE FROM tblsapstorageinfo WHERE 1 = 1 ";

            if (orgIDArray.Length > 0)
            {
                sql += "AND orgid IN (" + FormatHelper.ProcessQueryValues(string.Join(",", orgIDArray)).Replace("'", "") + ") ";
            }

            if (storageIDArray.Length > 0)
            {
                sql += "AND storageid IN (" + FormatHelper.ProcessQueryValues(string.Join(",", storageIDArray)) + ") ";
            }

            if (itemCode.Trim().Length > 0)
            {
                sql += "AND itemcode = '" + FormatHelper.PKCapitalFormat(FormatHelper.CleanString(itemCode)) + "' ";
            }

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region SAPStorageQuery

        public SAPStorageQuery CreateNewSAPStorageQuery()
        {
            return new SAPStorageQuery();
        }

        public void AddSAPStorageQuery(SAPStorageQuery sapStorageQuery)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                if (sapStorageQuery.Serial <= 0)
                {
                    string sql = string.Empty;
                    sql += "SELECT serial FROM tblsapstoragequery ";
                    sql += "WHERE serial >= (SELECT NVL(MAX(serial), 0) FROM tblsapstoragequery) ";
                    sql += "FOR UPDATE ";

                    object[] latestQuery = this.DataProvider.CustomQuery(typeof(SAPStorageQuery), new SQLCondition(sql));

                    if (latestQuery == null)
                    {
                        sapStorageQuery.Serial = 1;
                    }
                    else
                    {
                        sapStorageQuery.Serial = ((SAPStorageQuery)latestQuery[0]).Serial + 1;
                    }
                }

                this._helper.AddDomainObject(sapStorageQuery);

                this.DataProvider.CommitTransaction();
            }
            catch
            {
                this.DataProvider.RollbackTransaction();
                throw;
            }
        }

        public void DeleteSAPStorageQuery(SAPStorageQuery sapStorageQuery)
        {
            this._helper.DeleteDomainObject(sapStorageQuery);
        }

        public void UpdateSAPStorageQuery(SAPStorageQuery sapStorageQuery)
        {
            this._helper.UpdateDomainObject(sapStorageQuery);
        }

        public object GetSAPStorageQuery(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(SAPStorageQuery), new object[] { serial });
        }

        public object[] QuerySAPStorageQuery(string flag, string transactionCode)
        {
            string sql = "SELECT {0} FROM tblsapstoragequery WHERE 1 = 1 ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SAPStorageQuery)));
            if (flag.Trim().Length > 0)
            {
                sql += "AND flag = '" + flag + "' ";
            }
            if (transactionCode.Trim().Length > 0)
            {
                sql += "AND transactioncode = '" + transactionCode + "' ";
            }
            sql += "ORDER BY serial ";

            return this.DataProvider.CustomQuery(typeof(SAPStorageQuery), new SQLCondition(sql));
        }

        public object[] QuerySAPStorageQueryNotDealed(int maxCount)
        {
            string sql = "SELECT {0} FROM tblsapstoragequery WHERE flag = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SAPStorageQuery)), FlagStatus.FlagStatus_MES);
            return this.DataProvider.CustomQuery(typeof(SAPStorageQuery), new PagerCondition(sql, "serial", 1, maxCount));
        }

        #endregion

        #region Storage--库位信息 add by jinger 20160118

        public Storage CreateStorage()
        {
            return new Storage();
        }

        public void AddStorage(Storage storage)
        {
            this._helper.AddDomainObject(storage);
        }

        public void UpdateStorage(Storage storage)
        {
            this._helper.UpdateDomainObject(storage);
        }

        public void DeleteStorage(Storage storage)
        {
            this._helper.DeleteDomainObject(storage);
        }

        public void DeleteStorage(Storage[] storage)
        {
            this._helper.DeleteDomainObject(storage);
        }

        public object GetStorage(int orgID, string storageCode)
        {
            return this.DataProvider.CustomSearch(typeof(Storage), new object[] { storageCode, orgID });
        }

        //根据库位来源获取库位信息
        /// <summary>
        ///获取库位信息
        /// </summary>
        /// <param name="sourceFlag">库位来源</param>
        /// <returns></returns>
        public object[] GetStorage(string sourceFlag)
        {

            string sql = string.Format(@"SELECT {0} FROM tblstorage WHERE SOURCEFLAG = '{1}' ORDER BY STORAGECODE ",
                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)), sourceFlag);
            object[] objMaterial = this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
            if (objMaterial != null && objMaterial.Length > 0)
            {
                return objMaterial;
            }
            return null;
        }

        //获取所有库位
        /// <summary>
        /// 获取所有库位
        /// </summary>
        /// <returns></returns>
        public object[] GetAllStorage()
        {
            return this.DataProvider.CustomQuery(
                typeof(Storage), new SQLCondition(
                string.Format("select {0} from tblstorage order by  storagecode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)))
                )
                );
        }

        //库位信息查询
        /// <summary>
        /// 查询库位信息
        /// </summary>
        /// <param name="storageCode">库位代码</param>
        /// <param name="storageName">库位名称</param>
        /// <param name="virtualFlag">虚拟库位标识</param>
        /// <param name="sourceFlag">库位来源标识</param>
        /// <param name="orgId">组织ID</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryStorage(string storageCode, string storageName, string virtualFlag, string sourceFlag, int orgId, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            sql = string.Format("select {0} from tblstorage where ORGID = {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)), orgId);

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND STORAGECODE like '{0}%'", storageCode);
            }

            if (!string.IsNullOrEmpty(storageName))
            {
                sql += string.Format(" AND STORAGENAME like '{0}%'", storageName);
            }
            if (!string.IsNullOrEmpty(virtualFlag))
            {
                sql += string.Format(" AND VIRTUALFLAG  = '{0}'", virtualFlag);
            }

            if (!string.IsNullOrEmpty(sourceFlag))
            {
                sql += string.Format(" AND SOURCEFLAG  = '{0}'", sourceFlag);
            }
            return this.DataProvider.CustomQuery(typeof(Storage), new PagerCondition(sql, "storagecode", inclusive, exclusive));
        }

        //库位信息总行数
        /// <summary>
        /// 获取库位信息总行数
        /// </summary>
        /// <param name="storageCode">库位代码</param>
        /// <param name="storageName">库位名称</param>
        /// <param name="virtualFlag">虚拟库位标识</param>
        /// <param name="sourceFlag">库位来源标识</param>
        /// <param name="orgId">组织ID</param>
        /// <returns>总行数</returns>
        public int QueryStorageCount(string storageCode, string storageName, string virtualFlag, string sourceFlag, int orgId)
        {
            string sql = string.Empty;

            sql = string.Format("select count(1) from tblstorage where ORGID = {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)), orgId);

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND STORAGECODE like '{0}%'", storageCode);
            }

            if (!string.IsNullOrEmpty(storageName))
            {
                sql += string.Format(" AND STORAGENAME like '{0}%'", storageName);
            }
            if (!string.IsNullOrEmpty(virtualFlag))
            {
                sql += string.Format(" AND VIRTUALFLAG  = '{0}'", virtualFlag);
            }

            if (!string.IsNullOrEmpty(sourceFlag))
            {
                sql += string.Format(" AND SOURCEFLAG  = '{0}'", sourceFlag);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //检查货位是否使用
        /// <summary>
        /// 检查货位是否使用
        /// </summary>
        /// <returns>使用：true;未使用：false</returns>
        public bool CheckStorageIsUsed(string storageCode)
        {
            string sql = string.Format(@"SELECT COUNT(1) 
                                            FROM TBLSTORAGE WHERE STORAGECODE  IN
                                            (
                                                SELECT STORAGECODE FROM TBLASN WHERE STORAGECODE='{0}'
                                                UNION
                                                SELECT STORAGECODE FROM TBLPICK WHERE STORAGECODE='{0}'
                                                UNION
                                                SELECT STORAGECODE FROM TBLSTORAGEINFO WHERE STORAGECODE='{0}'
                                                UNION
                                                SELECT STORAGECODE FROM TBLSpecStorageInfo WHERE STORAGECODE='{0}'
                                                UNION
                                                SELECT STORAGECODE FROM TBLSTORAGEDETAIL WHERE STORAGECODE='{0}'
                                                UNION
                                                SELECT STORAGECODE FROM TBLLOCATION WHERE STORAGECODE='{0}'
                                            )", storageCode);

            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }


        public object[] QueryStorage(string storageCode)
        {
            string sql = string.Format("select {0} from tblstorage where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)));

            if (storageCode != null && storageCode.Length > 0)
            {
                sql = string.Format("{0} AND STORAGECODE = '{1}'", sql, storageCode);
            }

            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }

        public object GetStorageByStorageCode(string storageCode)
        {
            string sql = string.Format("select {0} from tblstorage where STORAGECODE = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)), storageCode);
            object[] storage = this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
            if (storage == null)
            {
                return null;
            }
            else
            {
                return storage[0];
            }
        }

        public object[] GetAllNoManageStorage()
        {
            return this.DataProvider.CustomQuery(
                typeof(Storage), new SQLCondition(
                string.Format("select {0} from tblstorage where storagecode not in(select  PARAMCODE from tblsysparam where PARAMGROUPCODE='DESTSTORAGEMANAGE')  order by  storagecode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)))
                )
                );
        }

        public object[] GetAllStorageCode()
        {
            string sql = "SELECT DISTINCT storagecode, storagename FROM tblstorage ORDER BY storagecode";

            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }

        public object[] QueryStorageByOrgId(int orgId)
        {
            string sql = string.Format("select {0} from tblstorage where ORGID = {1}", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)), orgId);
            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }

        public object[] QueryStorageByOrgId(int orgId, string sapCode)
        {
            string sql = string.Format("select {0} from tblstorage where ORGID = {1} and upper(sapstorage)='{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)), orgId, sapCode.ToUpper());
            return this.DataProvider.CustomQuery(typeof(Storage), new SQLCondition(sql));
        }
        #endregion

        #region Location--货位 add by jinger 20160118
        /// <summary>
        /// TBLLOCATION--货位
        /// </summary>
        public Location CreateNewLocation()
        {
            return new Location();
        }

        public void AddLocation(Location location)
        {
            this._helper.AddDomainObject(location);
        }

        public void DeleteLocation(Location location)
        {
            this._helper.DeleteDomainObject(location);
        }
        public void DeleteLocation(Location[] locations)
        {
            this._helper.DeleteDomainObject(locations);
        }

        public void UpdateLocation(Location location)
        {
            this._helper.UpdateDomainObject(location);
        }

        public object GetLocation(string Locationcode, int Orgid)
        {
            return this.DataProvider.CustomSearch(typeof(Location), new object[] { Locationcode, Orgid });
        }

        //根据库位来源和库位代码获取货位信息
        /// <summary>
        /// 获取货位信息
        /// </summary>
        /// <param name="sourceFlag">库位来源</param>
        ///<param name="storageCode">库位代码</param>
        /// <returns></returns>
        public object[] GetLocation(string sourceFlag, string storageCode)
        {
            string sql = string.Format(@"SELECT L.* FROM TBLLOCATION L,TBLSTORAGE S
                                             WHERE L.STORAGECODE= S.STORAGECODE 
                                             AND S.SOURCEFLAG = '{0}' 
                                             AND L.STORAGECODE = '{1}'
                                             ORDER BY L.LOCATIONCODE ", sourceFlag, storageCode);
            object[] objLocation = this.DataProvider.CustomQuery(typeof(Location), new SQLCondition(sql));
            if (objLocation != null && objLocation.Length > 0)
            {
                return objLocation;
            }
            return null;
        }

        //判断货位是否使用
        /// <summary>
        /// 判断货位是否使用
        /// </summary>
        /// <returns>使用：true;未使用：false</returns>
        public bool CheckLocationIsUsed(string locationCode)
        {
            string sql = string.Format(@"SELECT COUNT(1)
                                            FROM TBLLOCATION WHERE LOCATIONCODE IN 
                                            (
                                                SELECT LOCATIONCODE FROM TBLSTORAGEDETAIL WHERE LOCATIONCODE='{0}'
                                                UNION
                                                SELECT LOCATIONCODE FROM TBLSpecStorageInfo WHERE LOCATIONCODE='{0}'
                                             )", locationCode);

            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        //获取所有货位
        /// <summary>
        /// 获取所有货位
        /// </summary>
        /// <returns></returns>
        public object[] GetAllLocation()
        {
            return this.DataProvider.CustomQuery(
                typeof(Location), new SQLCondition(
                string.Format("select {0} from TBLLOCATION order by  LocationCode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Location)))
                )
                );
        }

        //货位查询
        /// <summary>
        /// 货位查询
        /// </summary>
        /// <param name="storageCode">库位代码</param>
        /// <param name="storageName">库位名称</param>
        /// <param name="locationCode">货位代码</param>
        /// <param name="locationName">货位名称</param>
        /// <param name="orgId">组织ID</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryLocation(string storageCode, string storageName, string locationCode, string locationName, int orgId, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT 
                                            L.STORAGECODE,
                                            S.Storagename,
                                            L.Locationcode,
                                            L.Locationname,
                                            L.Muser,
                                            L.Mdate,
                                            L.Mtime
                                            FROM TBLLOCATION L, TBLSTORAGE S
                                            WHERE L.STORAGECODE=S.STORAGECODE
                                            AND L.ORGID = {0}", orgId);
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND L.STORAGECODE LIKE '{0}%'", storageCode);
            }
            if (!string.IsNullOrEmpty(storageName))
            {
                sql += string.Format(" AND S.STORAGENAME LIKE '{0}%'", storageName);
            }
            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(" AND L.LocationCode LIKE '{0}%'", locationCode);
            }
            if (!string.IsNullOrEmpty(locationName))
            {
                sql += string.Format(" AND L.LocationName LIKE '{0}%'", locationName);
            }

            return this.DataProvider.CustomQuery(typeof(LocationWithStorageName), new PagerCondition(sql, "L.LocationCode", inclusive, exclusive));
        }

        //货位总行数
        /// <summary>
        /// 货位总行号
        /// </summary>
        /// <param name="storageCode">库位代码</param>
        /// <param name="storageName">库位名称</param>
        /// <param name="locationCode">货位代码</param>
        /// <param name="locationName">货位名称</param>
        /// <param name="orgId">组织ID</param>
        /// <returns></returns>
        public int QueryLocationCount(string storageCode, string storageName, string locationCode, string locationName, int orgId)
        {
            string sql = string.Format(@"SELECT 
                                            COUNT(1) 
                                            FROM TBLLOCATION L, TBLSTORAGE S
                                            WHERE L.STORAGECODE=S.STORAGECODE
                                            AND L.ORGID = {0}", orgId);
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND L.STORAGECODE LIKE '{0}%'", storageCode);
            }
            if (!string.IsNullOrEmpty(storageName))
            {
                sql += string.Format(" AND S.STORAGENAME LIKE '{0}%'", storageName);
            }
            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(" AND L.LocationCode LIKE '{0}%'", locationCode);
            }
            if (!string.IsNullOrEmpty(locationName))
            {
                sql += string.Format(" AND L.LocationName LIKE '{0}%'", locationName);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region StorageDetail--库存明细信息 add by jinger 20160118
        /// <summary>
        /// TBLSTORAGEDETAIL--库存明细信息
        /// </summary>
        public StorageDetail CreateNewStorageDetail()
        {
            return new StorageDetail();
        }

        public void AddStorageDetail(StorageDetail storagedetail)
        {
            this.DataProvider.Insert(storagedetail);

        }

        public void DeleteStorageDetail(StorageDetail storagedetail)
        {
            this._helper.DeleteDomainObject(storagedetail);
        }

        public void UpdateStorageDetail(StorageDetail storagedetail)
        {

            this.DataProvider.Update(storagedetail);

        }

        public object GetStorageDetail(string Cartonno)
        {
            return this.DataProvider.CustomSearch(typeof(StorageDetail), new object[] { Cartonno });
        }

        public object QueryStorageDetail(string dqMaterialNO)
        {
            return this.DataProvider.CustomSearch(typeof(StorageDetail), new object[] { dqMaterialNO });
        }

        //库存明细查询
        /// <summary>
        /// 库存明细查询
        /// </summary>
        /// <param name="mCode">物料代码</param>
        /// <param name="dQMCode">鼎桥物料代码</param>
        /// <param name="storageCode">库位代码</param>
        /// <param name="locationCode">货位代码</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="SN">SN</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryStorageDetailSN(string mCode, string dQMCode, string storageCode, string locationCode, string cartonNo, string SN, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT 
                                                A.*,
                                                B.SN,
                                                B.PICKBLOCK,
                                                C.STORAGENAME,
                                                D.LOCATIONNAME
                                                FROM TBLStorageDetail A
                                                LEFT JOIN TBLStorageDetailSN B ON A.CARTONNO=B.CARTONNO
                                                LEFT JOIN TBLStorage C ON A.STORAGECODE = C.STORAGECODE
                                                LEFT JOIN TbLLocation D ON A.LOCATIONCODE = D.LOCATIONCODE
                                                WHERE 1=1 ");
            if (!string.IsNullOrEmpty(mCode))
            {
                sql += string.Format(" AND A.MCODE = '{0}'", mCode);
            }
            if (!string.IsNullOrEmpty(dQMCode))
            {
                sql += string.Format(" AND A.DQMCODE = '{0}'", dQMCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(" AND A.LocationCode = '{0}'", locationCode);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO = '{0}'", cartonNo);
            }
            if (!string.IsNullOrEmpty(SN))
            {
                sql += string.Format(" AND B.SN = '{0}'", SN);
            }

            return this.DataProvider.CustomQuery(typeof(StorageDetailExt), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC", inclusive, exclusive));
        }

        //库存明细总行数
        /// <summary>
        /// 库存明细总行数
        /// </summary>
        /// <param name="mCode">物料代码</param>
        /// <param name="dQMCode">鼎桥物料代码</param>
        /// <param name="storageCode">库位代码</param>
        /// <param name="locationCode">货位代码</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="SN">SN</param>
        /// <returns></returns>
        public int QueryStorageDetailSNCount(string mCode, string dQMCode, string storageCode, string locationCode, string cartonNo, string SN)
        {
            string sql = string.Format(@"SELECT 
                                                COUNT(1)
                                                FROM TBLStorageDetail A
                                                LEFT JOIN TBLStorageDetailSN B ON A.CARTONNO=B.CARTONNO
                                                LEFT JOIN TBLStorage C ON A.STORAGECODE = C.STORAGECODE
                                                LEFT JOIN TbLLocation D ON A.LOCATIONCODE = D.LOCATIONCODE
                                                WHERE 1=1 ");
            if (!string.IsNullOrEmpty(mCode))
            {
                sql += string.Format(" AND A.MCODE = '{0}'", mCode);
            }
            if (!string.IsNullOrEmpty(dQMCode))
            {
                sql += string.Format(" AND A.DQMCODE = '{0}'", dQMCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(" AND A.LocationCode = '{0}'", locationCode);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO = '{0}'", cartonNo);
            }
            if (!string.IsNullOrEmpty(SN))
            {
                sql += string.Format(" AND B.SN = '{0}'", SN);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region

        public int FirstInDate(string cartonno, string mCode)
        {
            string sql = string.Format(@" select  nvl(min(a.mdate),0)  from TBLINVINOUTTRANS a where a.transtype='IN' and a.Mcode='{0}' and a.cartonno='{1}' ", mCode, cartonno);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //库存明细查询
        /// <summary>
        /// 库存明细查询
        /// </summary>
        /// <param name="mCode">物料代码</param>
        /// <param name="dQMCode">鼎桥物料代码</param>
        /// <param name="storageCode">库位代码</param>
        /// <param name="locationCode">货位代码</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="SN">SN</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryStorageDetail(string mCode, string dQMCode, string storageCode, string locationCode, string cartonNo, string SN,
            string usercode, int inclusive, int exclusive)
        {
            string sql = string.Format(@"   SELECT 
                                                A.*,e.validity,
                                                C.STORAGENAME,
                                                D.LOCATIONNAME
                                                FROM TBLStorageDetail A
                                                left join tblmaterial E ON a.mcode=e.mcode
                                                LEFT JOIN TBLStorage C ON A.STORAGECODE = C.STORAGECODE
                                                LEFT JOIN TbLLocation D ON A.LOCATIONCODE = D.LOCATIONCODE
                                                WHERE 1=1 ");

            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND a.StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }

            if (!string.IsNullOrEmpty(mCode))
            {
                sql += string.Format(" AND A.MCODE = '{0}'", mCode);
            }
            if (!string.IsNullOrEmpty(dQMCode))
            {
                sql += string.Format(" AND A.DQMCODE = '{0}'", dQMCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(" AND A.LocationCode like '%{0}%'", locationCode);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO LIKE '%{0}%'", cartonNo);
            }
            if (!string.IsNullOrEmpty(SN))
            {
                sql += string.Format("    and a.CARTONNO in (select B.CARTONNO from TBLStorageDetailSN B where b.sn like'%{0}%') ", SN);
            }

            return this.DataProvider.CustomQuery(typeof(StorageDetailExt), new PagerCondition(sql, "A.MDATE DESC,A.MTIME DESC,A.CARTONNO DESC", inclusive, exclusive));
        }

        //库存明细总行数
        /// <summary>
        /// 库存明细总行数
        /// </summary>
        /// <param name="mCode">物料代码</param>
        /// <param name="dQMCode">鼎桥物料代码</param>
        /// <param name="storageCode">库位代码</param>
        /// <param name="locationCode">货位代码</param>
        /// <param name="cartonNo">箱号</param>
        /// <param name="SN">SN</param>
        /// <returns></returns>
        public int QueryStorageDetailCount(string mCode, string dQMCode, string storageCode, string locationCode, string cartonNo, string SN
               , string usercode)
        {
            string sql = string.Format(@"SELECT 
                                                COUNT(1)
                                                FROM TBLStorageDetail A
                                                LEFT JOIN TBLStorage C ON A.STORAGECODE = C.STORAGECODE
                                                LEFT JOIN TbLLocation D ON A.LOCATIONCODE = D.LOCATIONCODE
                                                WHERE 1=1 ");
            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND a.StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }

            if (!string.IsNullOrEmpty(mCode))
            {
                sql += string.Format(" AND A.MCODE = '{0}'", mCode);
            }
            if (!string.IsNullOrEmpty(dQMCode))
            {
                sql += string.Format(" AND A.DQMCODE = '{0}'", dQMCode);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(" AND A.StorageCode = '{0}'", storageCode);
            }
            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(" AND A.LocationCode like '%{0}%'", locationCode);
            }
            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(" AND A.CARTONNO LIKE '%{0}%'", cartonNo);
            }
            if (!string.IsNullOrEmpty(SN))
            {
                sql += string.Format(" and a.CARTONNO in (select B.CARTONNO from TBLStorageDetailSN B where b.sn LIKE '%{0}%' ) ", SN);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion
        #region StorageDetailSN--库存明细SN add by jinger 20160118
        /// <summary>
        /// TBLSTORAGEDETAILSN--库存明细SN
        /// </summary>
        public StorageDetailSN CreateNewStorageDetailSN()
        {
            return new StorageDetailSN();
        }

        public void AddStorageDetailSN(StorageDetailSN storagedetailsn)
        {
            this.DataProvider.Insert(storagedetailsn);

        }

        public void DeleteStorageDetailSN(StorageDetailSN storagedetailsn)
        {
            this._helper.DeleteDomainObject(storagedetailsn);
        }

        public void UpdateStorageDetailSN(StorageDetailSN storagedetailsn)
        {
            this._helper.UpdateDomainObject(storagedetailsn);
        }

        public object GetStorageDetailSN(string SN)
        {
            return this.DataProvider.CustomSearch(typeof(StorageDetailSN), new object[] { SN });
        }

        public object[] GetStorageDetailSnbyCarton(string CartonNo)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLSTORAGEDETAILSN WHERE CARTONNO='{1}'  ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetailSN)), CartonNo);
            return this.DataProvider.CustomQuery(typeof(StorageDetailSN), new SQLCondition(sql));
        }
        #endregion

        #region SpecInOut--特殊物料出入库单 add by jinger 20160122
        /// <summary>
        /// TBLSPECINOUT--特殊物料出入库单
        /// </summary>
        public SpecInOut CreateNewSpecinout()
        {
            return new SpecInOut();
        }

        public void AddSpecInOut(SpecInOut specinout)
        {
            this._helper.AddDomainObject(specinout);
        }

        public void DeleteSpecInOut(SpecInOut specinout)
        {
            this._helper.DeleteDomainObject(specinout);
        }

        public void UpdateSpecInOut(SpecInOut specinout)
        {
            this._helper.UpdateDomainObject(specinout);
        }

        public object GetSpecInOut(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(SpecInOut), new object[] { Serial });
        }

        //出入库单查询
        /// <summary>
        /// 出入库单查询
        /// </summary>
        /// <param name="mCode">物料编码</param>
        /// <param name="mDesc">物料描述</param>
        /// <param name="bDate">创建开始日期</param>
        /// <param name="eDate">创建结束日期</param>
        /// <param name="moveType">单据类型（I=入库，O=出库）</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QuerySpecInOut(string DQMCode, string mDesc, int bDate, int eDate, string moveType, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT 
                                                  s.serial,
                                                s.movetype,
                                                s.mcode,
                                                s.storagecode,
                                                s.locationcode,
                                                s.qty,
                                                s.mdate,
                                                s.mtime,
                                                s.muser,
                                                s.dqmcode,
                                                s.InOutDesc,
                                                M.MENSHORTDESC,
                                                M.MCHSHORTDESC,
                                                M.MENLONGDESC,
                                                M.MCHLONGDESC,
                                                M.MUOM,
                                                ST.STORAGENAME,
                                                L.LOCATIONNAME
                                                FROM TBLSPECINOUT S
                                                LEFT JOIN TBLMATERIAL M ON S.Mcode=M.Mcode
                                                LEFT JOIN TBLSTORAGE ST ON S.STORAGECODE = ST.STORAGECODE
                                                LEFT JOIN TBLLOCATION L ON S.LOCATIONCODE = L.LOCATIONCODE
                                                WHERE S.MOVETYPE = '{0}' ", moveType);
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND S.DQMCODE = '{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(mDesc))
            {
                sql += string.Format(@" AND (M.MENSHORTDESC LIKE '{0}%' OR M.MENLONGDESC LIKE '{0}%'
                                        OR M.MCHSHORTDESC LIKE '{0}%' OR M.MCHLONGDESC LIKE '{0}%'
                                        OR M.MSPECIALDESC  LIKE '{0}%')", mDesc);
            }
            if (bDate > 0)
            {
                sql += string.Format(" AND  S.MDATE>= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(" AND  S.MDATE <= {0}", eDate);
            }

            return this.DataProvider.CustomQuery(typeof(SpecInOutWithMaterial), new PagerCondition(sql, "S.MDATE DESC,S.MTIME DESC", inclusive, exclusive));
        }

        //出入库单总行数
        /// <summary>
        /// 出入库单总行数
        /// </summary>
        /// <param name="mCode">物料编码</param>
        /// <param name="mDesc">物料描述</param>
        /// <param name="bDate">创建开始日期</param>
        /// <param name="eDate">创建结束日期</param>
        /// <param name="moveType">单据类型（I=入库，O=出库）</param>
        /// <returns></returns>
        public int QuerySpecInOutCount(string DQMCode, string mDesc, int bDate, int eDate, string moveType)
        {
            string sql = string.Format(@" SELECT 
                                                COUNT(1)
                                                FROM TBLSPECINOUT S
                                                LEFT JOIN TBLMATERIAL M ON S.Mcode=M.Mcode
                                                LEFT JOIN TBLSTORAGE ST ON S.STORAGECODE = ST.STORAGECODE
                                                LEFT JOIN TBLLOCATION L ON S.LOCATIONCODE = L.LOCATIONCODE
                                                WHERE S.MOVETYPE = '{0}' ", moveType);
            if (!string.IsNullOrEmpty(DQMCode))
            {
                sql += string.Format(" AND S.DQMCode = '{0}'", DQMCode);
            }
            if (!string.IsNullOrEmpty(mDesc))
            {
                sql += string.Format(@" AND (M.MENSHORTDESC LIKE '{0}%' OR M.MENLONGDESC LIKE '{0}%'
                                        OR M.MCHSHORTDESC LIKE '{0}%' OR M.MCHLONGDESC LIKE '{0}%'
                                        OR M.MSPECIALDESC  LIKE '{0}%')", mDesc);
            }
            if (bDate > 0)
            {
                sql += string.Format(" AND  S.MDATE>= {0}", bDate);
            }
            if (eDate > 0)
            {
                sql += string.Format(" AND  S.MDATE <= {0}", eDate);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region SpecStorageInfo-- 特殊物料库存信息  add by jinger 2016-01-29
        /// <summary>
        /// TBLSPECSTORAGEINFO-- 特殊物料库存信息 
        /// </summary>
        public SpecStorageInfo CreateNewSpecStorageInfo()
        {
            return new SpecStorageInfo();
        }

        public void AddSpecStorageInfo(SpecStorageInfo specstorageinfo)
        {
            this._helper.AddDomainObject(specstorageinfo);
        }

        public void DeleteSpecStorageInfo(SpecStorageInfo specstorageinfo)
        {
            this._helper.DeleteDomainObject(specstorageinfo);
        }

        public void UpdateSpecStorageInfo(SpecStorageInfo specstorageinfo)
        {
            this._helper.UpdateDomainObject(specstorageinfo);
        }

        public object GetSpecStorageInfo(string StorageCode, string MCode, string LocationCode)
        {
            return this.DataProvider.CustomSearch(typeof(SpecStorageInfo), new object[] { StorageCode, MCode, LocationCode });
        }

        //根据SAP物料号获取库存信息
        /// <summary>
        /// 根据SAP物料号获取库存信息
        /// </summary>
        /// <param name="MCode">SAP物料号</param>
        /// <returns></returns>
        public object[] GetSpecStorageInfo(string MCode)
        {
            string sql = string.Format(" SELECT {0} FROM TBLSPECSTORAGEINFO WHERE MCODE='{1}' ORDER BY CDATE,CTIME", DomainObjectUtility.GetDomainObjectFieldsString(typeof(SpecStorageInfo)), MCode);
            return this.DataProvider.CustomQuery(typeof(SpecStorageInfo), new SQLCondition(sql));
        }

        //根据SAP物料号获取库存数量
        /// <summary>
        /// 根据SAP物料号获取库存数量
        /// </summary>
        /// <param name="MCode">SAP物料号</param>
        /// <returns></returns>
        public int GetSpecStorageInfoQty(string MCode)
        {
            string sql = string.Format(" SELECT NVL(SUM(STORAGEQTY),0) STORAGEQTY FROM TBLSPECSTORAGEINFO WHERE MCODE='{0}'", MCode);
            object[] objSpecStorageInfo = this.DataProvider.CustomQuery(typeof(SpecStorageInfo), new SQLCondition(sql));
            if (objSpecStorageInfo != null && objSpecStorageInfo.Length > 0)
            {
                return ((SpecStorageInfo)objSpecStorageInfo[0]).StorageQty;
            }
            return 0;
        }

        #endregion

        #region Invoices--SAP单据  add by jinger 2016-01-25
        /// <summary>
        /// TBLINVOICES--SAP单据
        /// </summary>
        public Invoices CreateNewInvoices()
        {
            return new Invoices();
        }

        public void AddInvoices(Invoices invoices)
        {
            this._helper.AddDomainObject(invoices);
        }

        public void DeleteInvoices(Invoices invoices)
        {
            this._helper.DeleteDomainObject(invoices);
        }

        public void UpdateInvoices(Invoices invoices)
        {
            this._helper.UpdateDomainObject(invoices);
        }

        public object GetInvoices(string Invno)
        {
            return this.DataProvider.CustomSearch(typeof(Invoices), new object[] { Invno });
        }

        public InvoicesDetailEx[] GetDNInVoicesDetails(string batchCode)
        {
            string sql = @"SELECT a.INVNO,a.INVLINE,a.DQMCODE,a.MCODE,a.FromStorageCode,A.STORAGECODE,b.INVTYPE,a.PLANQTY,a.UNIT,
              a.FACCODE,a.MovementType  FROM TBLInvoicesDetail a LEFT JOIN TBLInvoices b ON a.INVNO=b.INVNO  where b.DNBATCHNO='" + batchCode + "'" +
                         "  AND a.INVLINESTATUS not in ('Cancel')  ";

            object[] oo = this.DataProvider.CustomQuery(typeof(InvoicesDetailEx), new SQLCondition(sql));

            List<InvoicesDetailEx> ds = new List<InvoicesDetailEx>();

            if (oo != null && oo.Length > 0)
            {
                foreach (InvoicesDetailEx d in oo)
                    ds.Add(d);
            }
            return ds.ToArray();
        }

        public object[] QuerySAPInvoicesQuery(string dnbatchno)
        {
            #region
            string sql = string.Format(@" select a.INVNO  
                ,a.dnbatchno as dnno
                ,a.INVSTATUS  
                ,b.INVLINE  
                ,b.DQMCODE  
                ,b.PLANQTY   
                ,b.UNIT  
                ,a.CUSBATCHNO  
                ,a.ORDERNO  
                ,a.CUSORDERNO  
                ,a.OANO  
                ,a.ReworkApplyUser  
                ,a.CC  
                ,a.InventoryNO  
                ,b.ReceiverUSER  
                ,a.SHIPPINGLOCATION   
                ,b.ReceiverAddr  
                ,a.GFFLAG  
                ,b.MOVEMENTTYPE  
                ,b.FACCODE  
                ,b.FromStorageCode  
                ,b.StorageCode  
                ,b.GFHWMCODE   
                ,b.GFPACKINGSEQ   
                ,b.CUSMCODE  
                ,b.CUSITEMSPEC  
                ,b.CUSITEMDESC  
                ,b.VENDERMCODE  
                ,b.GFHWDESC  
                ,b.HWCODEQTY  
                ,b.HWCODEUNIT  
                ,b.HWTYPEINFO  
               ,a.CUSER  
                ,a.CDATE
                ,a.CTIME  
                ,a.MUSER  
                 ,a.MDATE
                ,a.MTIME  
                ,b.HignLevelItem  
                ,b.PACKINGWAY  
                ,b.PACKINGNO  
                ,b.PACKINGSPEC  
                ,b.PACKINGWAYNO  
                ,a.PLANGIDATE  
                ,b.NEEDDATE  
                ,b.DemandArrivalDate  
                ,b.VENDORMCODE  
                ,b.CUSTMCODE  
                ,b.ReceiveMCODE  
                ,a.GFCONTRACTNO  
                ,a.CUSORDERNOTYPE  
                ,a.ORDERREASON  
                ,a.POSTWAY  
                ,a.PICKCONDITION  
                ,b.DQSMCODE  
                from TBLINVOICES a, TBLINVOICESDETAIL b
                where a.invno=b.invno  ");
            #endregion
            if (!string.IsNullOrEmpty(dnbatchno))
            {
                sql += string.Format(@" AND a.dnbatchno = '{0}'", dnbatchno);
            }

            return this.DataProvider.CustomQuery(typeof(SAPInvoicesQuery), new SQLCondition(sql));
        }

        #region

        public object QuerySAPInvoicesDetail(string invNo, string invline)
        {
            string invNosql = "";
            if (!string.IsNullOrEmpty(invNo))
            {
                invNosql += string.Format(@" AND a.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(invline))
            {
                invNosql += string.Format(@" AND b.invline = '{0}'", invline);
            }
            #region
            string sql = string.Format(@" select a.INVNO  
                ,a.dnbatchno as dnno
                ,a.INVSTATUS  
                ,a.INVTYPE
                ,b.INVLINE  
                ,b.INVLINESTATUS  
                ,b.PRNO  
                ,b.DQMCODE  
                ,b.PLANQTY   
                ,b.UNIT  
                ,a.CUSBATCHNO  
                ,a.ORDERNO  
                ,a.CUSORDERNO  
                ,a.OANO  
                ,a.ReworkApplyUser  
                ,a.CC  
                ,a.InventoryNO  
                ,b.ReceiverUSER  
                ,a.SHIPPINGLOCATION   
                ,b.ReceiverAddr  
                ,a.GFFLAG  
                ,b.MOVEMENTTYPE  
                ,b.FACCODE  
                ,b.FromStorageCode  
                ,b.StorageCode  
                ,b.GFHWMCODE   
                ,b.GFPACKINGSEQ   
                ,b.CUSMCODE  
                ,b.CUSITEMSPEC  
                ,b.CUSITEMDESC  
                ,b.VENDERMCODE  
                ,b.GFHWDESC  
                ,b.HWCODEQTY  
                ,b.HWCODEUNIT  
                ,b.HWTYPEINFO  
               ,a.CUSER  
                ,a.CDATE
                ,a.CTIME  
                ,a.MUSER  
                 ,a.MDATE
                ,a.MTIME  
                ,b.HignLevelItem  
                ,b.PACKINGWAY  
                ,b.PACKINGNO  
                ,b.PACKINGSPEC  
                ,b.PACKINGWAYNO  
                ,a.PLANGIDATE  
                ,b.NEEDDATE  
                ,b.DemandArrivalDate  
                ,b.VENDORMCODE  
                ,b.CUSTMCODE  
                ,b.ReceiveMCODE  
                ,a.GFCONTRACTNO  
                ,a.CUSORDERNOTYPE  
                ,a.ORDERREASON  
                ,a.POSTWAY  
                ,a.PICKCONDITION  
                ,b.DQSMCODE  
                ,   (case when a.Createuser is null  then a.Dnmuser else  a.Createuser  end) as Createuser，
                   (case when a.Poupdatedate='0'  then a.Dnmdate else  a.Poupdatedate  end) as Poupdatedate，
                     (case when a.Poupdatetime='0' then a.Dnmtime else  a.Poupdatetime  end) as Poupdatetime，
                a.Pocreatedate,
                a.Dnmuser,
                a.Dnmdate,
                a.Dnmtime,
                a.NOTOUTCHECKFLAG,
                   a.REMARK1,
               b.detailremark
                from TBLINVOICES a, TBLINVOICESDETAIL b
                where a.invno=b.invno  {0}   ", invNosql);
            #endregion

            object[] list = this.DataProvider.CustomQuery(typeof(SAPInvoicesQuery), new SQLCondition(sql));
            if (list != null)
            {
                return list[0];
            }
            return null;
        }

        public object[] QuerySAPInvoicesQuery1(string invNo, string dnbatchno, bool isMovementType, int inclusive, int exclusive)
        {
            string invNosql = "";

            invNosql = string.Format(@" AND a.INVNO = '{0}'", invNo);

            if (!string.IsNullOrEmpty(dnbatchno))
            {
                invNosql = string.Format(@" AND a.dnbatchno = '{0}'", dnbatchno);
            }
            if (isMovementType)
            {
                invNosql += string.Format(@" and a.invtype= 'DNC'  and a.dnbatchno
                not in ( select distinct  a.dnbatchno from TBLINVOICES a, TBLINVOICESDETAIL b where a.invno=b.invno
                 and b.movementtype is not null ) ");
            }
            #region
            string sql = string.Format(@" select a.INVNO  
                ,a.dnbatchno as dnno
                ,a.INVSTATUS  
                ,a.INVTYPE
                ,b.INVLINE  
                ,b.INVLINESTATUS  
                ,b.PRNO  
                ,b.DQMCODE  
                ,b.PLANQTY   
                ,b.UNIT  
                ,a.CUSBATCHNO  
                ,a.ORDERNO  
                ,a.CUSORDERNO  
                ,a.OANO  
                ,a.ReworkApplyUser  
                ,a.CC  
                ,a.InventoryNO  
                ,b.ReceiverUSER  
                ,a.SHIPPINGLOCATION   
                ,b.ReceiverAddr  
                ,a.GFFLAG  
                ,b.MOVEMENTTYPE  
                ,b.FACCODE  
                ,b.FromStorageCode  
                ,b.StorageCode  
                ,b.GFHWMCODE   
                ,b.GFPACKINGSEQ   
                ,b.CUSMCODE  
                ,b.CUSITEMSPEC  
                ,b.CUSITEMDESC  
                ,b.VENDERMCODE  
                ,b.GFHWDESC  
                ,b.HWCODEQTY  
                ,b.HWCODEUNIT  
                ,b.HWTYPEINFO  
               ,a.CUSER  
                ,a.CDATE
                ,a.CTIME  
                ,a.MUSER  
                 ,a.MDATE
                ,a.MTIME  
                ,b.HignLevelItem  
                ,b.PACKINGWAY  
                ,b.PACKINGNO  
                ,b.PACKINGSPEC  
                ,b.PACKINGWAYNO  
                ,a.PLANGIDATE  
                ,b.NEEDDATE  
                ,b.DemandArrivalDate  
                ,b.VENDORMCODE  
                ,b.CUSTMCODE  
                ,b.ReceiveMCODE  
                ,a.GFCONTRACTNO  
                ,a.CUSORDERNOTYPE  
                ,a.ORDERREASON  
                ,a.POSTWAY  
                ,a.PICKCONDITION  
                ,b.DQSMCODE  
                ,   (case when a.Createuser is null  then a.Dnmuser else  a.Createuser  end) as Createuser，
                   (case when a.Poupdatedate='0'  then a.Dnmdate else  a.Poupdatedate  end) as Poupdatedate，
                     (case when a.Poupdatetime='0' then a.Dnmtime else  a.Poupdatetime  end) as Poupdatetime，
                a.Pocreatedate,
                a.Dnmuser,
                a.Dnmdate,
                a.Dnmtime,
                a.NOTOUTCHECKFLAG,
                   a.REMARK1,
               b.detailremark
                from TBLINVOICES a, TBLINVOICESDETAIL b
                where a.invno=b.invno  {0}   ", invNosql);
            #endregion

            return this.DataProvider.CustomQuery(typeof(SAPInvoicesQuery), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", inclusive, exclusive));
            //return QueryInvoices("", invNo, "", "", "",0,0, inclusive, exclusive);
        }

        public object[] QuerySAPInvoicesQuery(string invNo, string dnbatchno, bool isMovementType, int inclusive, int exclusive)
        {
            string invNosql = "";
            if (!string.IsNullOrEmpty(invNo))
            {
                invNosql = string.Format(@" AND a.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(dnbatchno))
            {
                invNosql = string.Format(@" AND a.dnbatchno = '{0}'", dnbatchno);
            }
            if (isMovementType)
            {
                invNosql += string.Format(@" and a.invtype= 'DNC'  and a.dnbatchno
                not in ( select distinct  a.dnbatchno from TBLINVOICES a, TBLINVOICESDETAIL b where a.invno=b.invno
                 and b.movementtype is not null ) ");
            }
            #region
            string sql = string.Format(@" select a.INVNO  
                ,a.dnbatchno as dnno
                ,a.INVSTATUS  
                ,a.INVTYPE
                ,b.INVLINE  
                ,b.INVLINESTATUS  
                ,b.PRNO  
                ,b.DQMCODE  
                ,b.PLANQTY   
                ,b.UNIT  
                ,a.CUSBATCHNO  
                ,a.ORDERNO  
                ,a.CUSORDERNO  
                ,a.OANO  
                ,a.ReworkApplyUser  
                ,a.CC  
                ,a.InventoryNO  
                ,b.ReceiverUSER  
                ,a.SHIPPINGLOCATION   
                ,b.ReceiverAddr  
                ,a.GFFLAG  
                ,b.MOVEMENTTYPE  
                ,b.FACCODE  
                ,b.FromStorageCode  
                ,b.StorageCode  
                ,b.GFHWMCODE   
                ,b.GFPACKINGSEQ   
                ,b.CUSMCODE  
                ,b.CUSITEMSPEC  
                ,b.CUSITEMDESC  
                ,b.VENDERMCODE  
                ,b.GFHWDESC  
                ,b.HWCODEQTY  
                ,b.HWCODEUNIT  
                ,b.HWTYPEINFO  
               ,a.CUSER  
                ,a.CDATE
                ,a.CTIME  
                ,a.MUSER  
                 ,a.MDATE
                ,a.MTIME  
                ,b.HignLevelItem  
                ,b.PACKINGWAY  
                ,b.PACKINGNO  
                ,b.PACKINGSPEC  
                ,b.PACKINGWAYNO  
                ,a.PLANGIDATE  
                ,b.NEEDDATE  
                ,b.DemandArrivalDate  
                ,b.VENDORMCODE  
                ,b.CUSTMCODE  
                ,b.ReceiveMCODE  
                ,a.GFCONTRACTNO  
                ,a.CUSORDERNOTYPE  
                ,a.ORDERREASON  
                ,a.POSTWAY  
                ,a.PICKCONDITION  
                ,b.DQSMCODE  
                ,   (case when a.Createuser is null  then a.Dnmuser else  a.Createuser  end) as Createuser，
                   (case when a.Poupdatedate='0'  then a.Dnmdate else  a.Poupdatedate  end) as Poupdatedate，
                     (case when a.Poupdatetime='0' then a.Dnmtime else  a.Poupdatetime  end) as Poupdatetime，
                a.Pocreatedate,
                a.Dnmuser,
                a.Dnmdate,
                a.Dnmtime,
                a.NOTOUTCHECKFLAG,
                   a.REMARK1,
               b.detailremark
                from TBLINVOICES a, TBLINVOICESDETAIL b
                where a.invno=b.invno  {0}   ", invNosql);
            #endregion

            return this.DataProvider.CustomQuery(typeof(SAPInvoicesQuery), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", inclusive, exclusive));
            //return QueryInvoices("", invNo, "", "", "",0,0, inclusive, exclusive);
        }

        public int QuerySAPInvoicesQueryCount(string invNo, string dnbatchno, bool isMovementType)
        {
            string invNosql = "";
            if (!string.IsNullOrEmpty(invNo))
            {
                invNosql = string.Format(@" AND a.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(dnbatchno))
            {
                invNosql = string.Format(@" AND a.dnbatchno = '{0}'", dnbatchno);
            }
            if (isMovementType)
            {
                invNosql += string.Format(@" and a.invtype= 'DNC'  and a.dnbatchno
                not in ( select distinct  a.dnbatchno from TBLINVOICES a, TBLINVOICESDETAIL b where a.invno=b.invno
                 and b.movementtype is not null ) ");
            }
            #region
            string sql = string.Format(@"
select count(1)     
                from (select a.INVNO  
                ,a.dnbatchno as dnno
                ,a.INVSTATUS  
                ,b.INVLINE  
                ,b.DQMCODE  
                ,b.PLANQTY   
                ,b.UNIT  
                ,a.CUSBATCHNO  
                ,a.ORDERNO  
                ,a.CUSORDERNO  
                ,a.OANO  
                ,a.ReworkApplyUser  
                ,a.CC  
                ,a.InventoryNO  
                ,b.ReceiverUSER  
                ,a.SHIPPINGLOCATION   
                ,b.ReceiverAddr  
                ,a.GFFLAG  
                ,b.MOVEMENTTYPE  
                ,b.FACCODE  
                ,b.FromStorageCode  
                ,b.StorageCode  
                ,b.GFHWMCODE   
                ,b.GFPACKINGSEQ   
                ,b.CUSMCODE  
                ,b.CUSITEMSPEC  
                ,b.CUSITEMDESC  
                ,b.VENDERMCODE  
                ,b.GFHWDESC  
                ,b.HWCODEQTY  
                ,b.HWCODEUNIT  
                ,b.HWTYPEINFO  
               ,a.CUSER  
                ,a.CDATE
                ,a.CTIME  
                ,a.MUSER  
                 ,a.MDATE
                ,a.MTIME  
                ,b.HignLevelItem  
                ,b.PACKINGWAY  
                ,b.PACKINGNO  
                ,b.PACKINGSPEC  
                ,b.PACKINGWAYNO  
                ,a.PLANGIDATE  
                ,b.NEEDDATE  
                ,b.DemandArrivalDate  
                ,b.VENDORMCODE  
                ,b.CUSTMCODE  
                ,b.ReceiveMCODE  
  
  
                ,a.GFCONTRACTNO  
                ,a.CUSORDERNOTYPE  
                ,a.ORDERREASON  
                ,a.POSTWAY  
                ,a.PICKCONDITION  
                ,b.DQSMCODE  
                from TBLINVOICES a, TBLINVOICESDETAIL b
                where a.invno=b.invno   {0} )   ", invNosql);
            #endregion
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        public object[] QuerySAPInvoices(string invNo, int inclusive, int exclusive)
        {
            string sql = @" SELECT I.*,R.VENDORNAME FROM TBLINVOICES I 
                                                LEFT JOIN TBLVENDOR R ON I.VENDORCODE=R.VENDORCODE
                                                WHERE 1=1 ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND I.INVNO = '{0}'", invNo);
            }
            return this.DataProvider.CustomQuery(typeof(SAPInvoices), new PagerCondition(sql, "I.MDATE DESC,I.MTIME DESC", inclusive, exclusive));
            //return QueryInvoices("", invNo, "", "", "",0,0, inclusive, exclusive);
        }

        public int QueryInvoicesCount(string invNo)
        {
            return QueryInvoicesCount("", invNo, "", "", "", 0, 0);
        }

        //SAP单据查询
        /// <summary>
        /// SAP单据查询
        /// </summary>
        /// <param name="invType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="finishFlag">完成</param>
        /// <param name="vendorCode">供应商代码</param>
        /// <param name="cUser">创建人</param>
        /// <param name="bCDate">创建日期开始</param>
        /// <param name="eCDate">创建日期结束</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>     
        public object[] QueryInvoices(string invType, string invNo, string finishFlag, string vendorCode, string cUser, int bCDate, int eCDate, int inclusive, int exclusive)
        {
            string sql = @" SELECT I.*,R.VENDORNAME,B.DIS FROM TBLINVOICES I 
                              LEFT JOIN TBLVENDOR R ON I.VENDORCODE=R.VENDORCODE LEFT JOIN (
                               SELECT INVNO, ACTQTY ,PLANQTY,DIS FROM (
                                                            SELECT INVNO , ACTQTY ,PLANQTY,(PLANQTY-ACTQTY) DIS From (
                                                                                                                select B.INVNO,SUM(B.ACTQTY) ACTQTY,SUM(B.PLANQTY) PLANQTY  from tblasndetailitem A,TBLINVOICESDETAIL B WHERE A.INVNO=B.INVNO AND A.INVLINE=B.INVLINE GROUP BY B.INVNO
                                                                                                                    ) 
                                                                        )

                            ) B ON I.INVNO=B.INVNO
                    WHERE 1=1 and (i.InvType='POR'or i.InvType='SCTR' or i.InvType='DNR' or i.InvType='UB' or i.InvType='JCR' or i.InvType='BLR' or i.InvType='CAR' or i.InvType='YFR' or i.InvType='PD' or i.InvType='PGIR') 
                            

                           ";
            if (!string.IsNullOrEmpty(invType))
            {
                sql += string.Format(" AND I.INVTYPE ='{0}'", invType);
            }
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND I.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(finishFlag))
            {
                if (finishFlag == "N")
                    sql += string.Format(@" AND B.DIS>0");
                if(finishFlag=="Y")
                    sql += string.Format(@" AND B.DIS=0");
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {
                vendorCode = vendorCode.Replace(",", "','");
                sql += string.Format(@" AND I.VENDORCODE IN ('{0}')", vendorCode);
            }
            if (!string.IsNullOrEmpty(cUser))
            {
                sql += string.Format(" AND I.CUSER = '{0}'", cUser);
            }

            if (bCDate > 0)
            {
                sql += string.Format(" AND I.CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(" AND I.CDATE  <= {0}", eCDate);
            }

            return this.DataProvider.CustomQuery(typeof(InvoicesExt), new PagerCondition(sql, "I.MDATE DESC,I.MTIME DESC", inclusive, exclusive));

        }

        //SAP单据总行数
        /// <summary>
        /// SAP单据总行数
        /// </summary>
        /// <param name="invType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="finishFlag">完成</param>
        /// <param name="vendorCode">供应商代码</param>
        /// <param name="cUser">创建人</param>
        /// <param name="bCDate">创建日期开始</param>
        /// <param name="eCDate">创建日期结束</param>
        /// <returns></returns>
        public int QueryInvoicesCount(string invType, string invNo, string finishFlag, string vendorCode, string cUser, int bCDate, int eCDate)
        {
            string sql = @" SELECT count(*) FROM TBLINVOICES I 
                              LEFT JOIN TBLVENDOR R ON I.VENDORCODE=R.VENDORCODE LEFT JOIN (
                               SELECT INVNO, ACTQTY ,PLANQTY,DIS FROM (
                                                            SELECT INVNO , ACTQTY ,PLANQTY,(PLANQTY-ACTQTY) DIS From (
                                                                                                                select B.INVNO,SUM(B.ACTQTY) ACTQTY,SUM(B.PLANQTY) PLANQTY  from tblasndetailitem A,TBLINVOICESDETAIL B WHERE A.INVNO=B.INVNO AND A.INVLINE=B.INVLINE GROUP BY B.INVNO
                                                                                                                    ) 
                                                                        )

                            ) B ON I.INVNO=B.INVNO
                    WHERE 1=1 and (i.InvType='POR'or i.InvType='SCTR' or i.InvType='DNR' or i.InvType='UB' or i.InvType='JCR' or i.InvType='BLR' or i.InvType='CAR' or i.InvType='YFR' or i.InvType='PD' or i.InvType='PGIR') 
                            

                           ";
            if (!string.IsNullOrEmpty(invType))
            {
                sql += string.Format(" AND I.INVTYPE ='{0}'", invType);
            }
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND I.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(finishFlag))
            {
                if (finishFlag == "N")
                    sql += string.Format(@" AND B.DIS>0");
                if (finishFlag == "Y")
                    sql += string.Format(@" AND B.DIS=0");
            }
            if (!string.IsNullOrEmpty(vendorCode))
            {
                vendorCode = vendorCode.Replace(",", "','");
                sql += string.Format(@" AND I.VENDORCODE IN ('{0}')", vendorCode);
            }
            if (!string.IsNullOrEmpty(cUser))
            {
                sql += string.Format(" AND I.CUSER = '{0}'", cUser);
            }

            if (bCDate > 0)
            {
                sql += string.Format(" AND I.CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(" AND I.CDATE  <= {0}", eCDate);
            }


            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region Invoicesdetail-- SAP单据明细 add by jinger 2016-01-25
        /// <summary>
        /// TBLINVOICESDETAIL-- SAP单据明细
        /// </summary>
        public InvoicesDetail CreateNewInvoicesDetail()
        {
            return new InvoicesDetail();
        }

        public void AddInvoicesDetail(InvoicesDetail invoicesdetail)
        {
            this._helper.AddDomainObject(invoicesdetail);
        }

        public void DeleteInvoicesDetail(InvoicesDetail invoicesdetail)
        {
            this._helper.DeleteDomainObject(invoicesdetail);
        }

        public void UpdateInvoicesDetail(InvoicesDetail invoicesdetail)
        {
            this.DataProvider.Update(invoicesdetail);

        }

        public object GetInvoicesDetail(string Invno, int Invline)
        {
            return this.DataProvider.CustomSearch(typeof(InvoicesDetail), new object[] { Invno, Invline });
        }


        public decimal GetPickDetailQty(string invno, int invline)
        {
            string sql = string.Format(@" select a.qty from 
                (select a.invno,b.invline, NVL(sum(b.qty),0) qty  from tblpick a,tblpickdetail b where
                a.pickno=b.pickno group by a.invno,b.invline) a
                where  a.invno='{0}' and  a.invline='{1}' ", invno, invline);
            object[] objSpecStorageInfo = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (objSpecStorageInfo != null && objSpecStorageInfo.Length > 0)
            {
                return ((PickDetail)objSpecStorageInfo[0]).QTY;
            }
            return 0;
        }

        //根据SAP单据号获取SAP单据详细信息
        /// <summary>
        /// 根据SAP单据号获取SAP单据详细信息
        /// </summary>
        /// <param name="Invno">SAP单据号</param>
        /// <returns></returns>
        public object GetInvoicesDetail(string Invno)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE A.INVNO='{1}' ORDER BY INVLINE",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), Invno);
            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail[0];
            }
            return null;
        }
        //根据SAP单据号获取SAP单据详细信息
        /// <summary>
        /// 根据SAP单据号获取SAP单据详细信息
        /// </summary>
        /// <param name="invno">SAP单据号</param>
        /// <returns></returns>
        public object[] GetInvoicesDetailByInvNo(string invno)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE A.INVNO='{1}' ORDER BY INVLINE",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), invno);
            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail;
            }
            return null;
        }

        public object[] GetInvoicesDetailByInvNo(string invno, string dqmcode)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE A.INVNO='{1}'  and A.DQMCODE='{2}'    order by a.PLANDATE desc ",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), invno, dqmcode);

            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail;
            }
            return null;
        }


        //public int GetECNGQtyByStatus(string stNO, string dqmcode, string status)
        //{
        //    string sql = string.Format(@"SELECT sum(ReceiveQty) FROM  TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus <> '{1}' and dqmcode='{2}' ", stNO, status, dqmcode);
        //    return this.DataProvider.GetCount(new SQLCondition(sql));
        //}


        public object[] GetInvoicesDetailByInvNo(string invno, string invline, string dqmcode)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE A.INVNO='{1}'  and A.DQMCODE='{2}'  ",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), invno, dqmcode);
            if (!string.IsNullOrEmpty(invline))
            {
                sql += string.Format(@"   and A.invline>'{0}' ", invline);
            }
            sql += string.Format(@"  order by a.PLANDATE desc ");
            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail;
            }
            return null;
        }

        public object[] QuerySAPInvoicesDetail(string invNo, int inclusive, int exclusive)
        {
            string sql = @" SELECT I.INVTYPE,IL.* FROM TBLINVOICES I 
                                                        INNER JOIN TBLINVOICESDETAIL IL ON I.INVNO=IL.INVNO
                                                        WHERE 1=1 ";
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND I.INVNO = '{0}'", invNo);
            }
            return this.DataProvider.CustomQuery(typeof(SAPInvoicesDetail), new PagerCondition(sql, inclusive, exclusive));
            //return QueryInvoicesDetail("", invNo, "",inclusive, exclusive);
        }

        public int QueryInvoicesDetailCount(string invNo)
        {
            return QueryInvoicesDetailCount("", invNo, "");
        }
        //SAP单据查询
        /// <summary>
        /// SAP单据查询
        /// </summary>
        /// <param name="invType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="type">盘亏/盘盈标识（702/701）</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryInvoicesDetail(string invType, string invNo, string type, int inclusive, int exclusive)
        {
            string sql = @" SELECT I.INVTYPE,IL.* FROM TBLINVOICES I 
                                                        INNER JOIN TBLINVOICESDETAIL IL ON I.INVNO=IL.INVNO
                                                        WHERE 1=1 ";
            if (!string.IsNullOrEmpty(invType))
            {
                sql += string.Format(" AND I.INVTYPE ='{0}'", invType);
            }
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND I.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(@" AND IL.TYPE = '{0}'", type);
            }
            return this.DataProvider.CustomQuery(typeof(InvoicesDetailExt), new PagerCondition(sql, inclusive, exclusive));
        }
        public object[] QueryInvoicesDetail1(string invType, string invNo, string type, int inclusive, int exclusive)
        {
            #region sql
            string sql = @"
                      SELECT I.INVTYPE,IL.invno,
                    IL.invline,
                    IL.invlinestatus,
                    IL.fromstoragecode,
                    IL.faccode,
                    IL.storagecode,
                    IL.mcode,
                    IL.dqmcode,
                    case  when IL.menshortdesc is null then m.menshortdesc else IL.menshortdesc end menshortdesc,
                    case  when IL.mlongdesc is null then m.mchlongdesc else IL.mlongdesc end mlongdesc,
                    IL.planqty,
                    IL.actqty,
                    IL.outqty,
                    IL.plandate,
                    IL.unit,
                    IL.shipaddr,
                    IL.status,
                    IL.detailremark,
                    IL.vendormcode,
                    IL.prno,
                    IL.returnflag,
                    IL.accountassignment,
                    IL.itemcategory,
                    IL.incoterms1,
                    IL.incoterms2,
                    IL.so,
                    IL.soitemno,
                    IL.sowbsno,
                    IL.ccno,
                    IL.hignlevelitem,
                    IL.movementtype,
                    IL.cusmcode,
                    IL.cusitemspec,
                    IL.cusitemdesc,
                    IL.vendermcode,
                    IL.gfhwmcode,
                    IL.gfpackingseq,
                    IL.gfhwdesc,
                    IL.hwcodeqty,
                    IL.hwcodeunit,
                    IL.hwtypeinfo,
                    IL.packingway,
                    IL.packingno,
                    IL.packingspec,
                    IL.packingwayno,
                    IL.dqsmcode,
                    IL.receiveraddr,
                    IL.receiveruser,
                    IL.custmcode,
                    IL.receivemcode,
                    IL.demandarrivaldate,
                    IL.needdate,
                    IL.type,
                    IL.cuser,
                    IL.cdate,
                    IL.ctime,
                    IL.muser,
                    IL.mdate,
                    IL.mtime,
                    IL.eattribute1,
                    IL.eattribute2,
                    IL.eattribute3
                    FROM TBLINVOICES I 
                                                        INNER JOIN TBLINVOICESDETAIL IL ON I.INVNO=IL.INVNO ";
            #endregion
            if (!string.IsNullOrEmpty(invType))
            {
                sql += string.Format(" AND I.INVTYPE ='{0}'", invType);
            }
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND I.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(@" AND IL.TYPE = '{0}'", type);
            }
            sql += "    left join tblmaterial m on il.dqmcode=m.dqmcode ";
            return this.DataProvider.CustomQuery(typeof(InvoicesDetailExt1), new PagerCondition(sql, inclusive, exclusive));
        }
        //SAP单据总行数
        /// <summary>
        /// SAP单据总行数
        /// </summary>
        /// <param name="invType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="invNo">盘亏/盘盈标识（702/701）</param>
        /// <returns></returns>
        public int QueryInvoicesDetailCount(string invType, string invNo, string type)
        {
            string sql = @" SELECT COUNT(1) FROM TBLINVOICES I 
                                        INNER JOIN TBLINVOICESDETAIL IL ON I.INVNO=IL.INVNO
                                        WHERE 1=1 ";
            if (!string.IsNullOrEmpty(invType))
            {
                sql += string.Format(" AND I.INVTYPE ='{0}'", invType);
            }
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND I.INVNO = '{0}'", invNo);
            }
            if (!string.IsNullOrEmpty(type))
            {
                sql += string.Format(@" AND IL.TYPE = '{0}'", type);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //获取所有SAP单据详细信息
        /// <summary>
        /// 获取所有SAP单据详细信息
        /// </summary>
        /// <returns></returns>
        public object[] GetAllInvoicesDetail()
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE 1=1 ORDER BY MDATE DESC,MTIME DESC",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)));
            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail;
            }
            return null;
        }

        public object GetMaterialByDQMCode(string dQMCode)
        {
            string sql = string.Format(@"SELECT {0} FROM tblmaterial WHERE DQMCODE = '{1}' ORDER BY mdate DESC,mtime DESC ",
                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.MOModel.Material)), dQMCode);
            object[] objMaterial = this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
            if (objMaterial != null && objMaterial.Length > 0)
            {
                return objMaterial[0];
            }
            return null;
        }
        #endregion

        #region Pick-- 拣货任务令头  add by jinger 2016-01-27
        /// <summary>
        /// TBLPICK-- 拣货任务令头 
        /// </summary>
        public Pick CreateNewPick()
        {
            return new Pick();
        }

        public void AddPick(Pick pick)
        {
            this._helper.AddDomainObject(pick);
        }

        public void DeletePick(Pick pick)
        {
            this._helper.DeleteDomainObject(pick);
        }

        public void UpdatePick(Pick pick)
        {
            this._helper.UpdateDomainObject(pick);
        }

        public object GetPick(string Pickno)
        {
            return this.DataProvider.CustomSearch(typeof(Pick), new object[] { Pickno });
        }

        public void DeletePick(Pick[] picks)
        {
            this._helper.DeleteDomainObject(picks);
        }

        //单据获取拣货任务令头
        /// <summary>
        /// 根据SAP单据获取拣货任务令头
        /// </summary>
        /// <param name="invNo">SAP单据</param>
        /// <returns></returns>
        public object GetPickByInvNo(string invNo)
        {
            string sql = string.Format(@"SELECT * FROM TBLPICK WHERE INVNO ='{0}'", invNo);
            object[] objPick = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            if (objPick != null && objPick.Length > 0)
            {
                return objPick[0];
            }
            return null;
        }

        //根据状态获取拣货任务令头信息
        /// <summary>
        /// 根据状态获取拣货任务令头信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public object[] GetPickByStatus(string status)
        {
            string sql = string.Format("SELECT {0} FROM TBLPICK WHERE STATUS='{1}' ORDER BY MDATE DESC,MTIME DESC", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pick)), status);
            return this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
        }


        public object[] GetPickByPickStatus(string status, string oqcstatus)
        {
            string sql = string.Format(@"
                SELECT b.status ,a.* FROM TBLPICK a 
                inner  join tbloqc b on a.pickno=b.pickno
                WHERE a.STATUS='{0}' and b.status='{1}'
                ORDER BY a.MDATE DESC,a.MTIME DESC
                 ", status, oqcstatus);
            return this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
        }

        public object[] GetPickByPickStatus(string status)
        {
            string sql = string.Format(@"
                SELECT  a.* FROM TBLPICK a 
                WHERE a.STATUS='{0}'  
                ORDER BY a.MDATE DESC,a.MTIME DESC ", status);
            return this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
        }
        public object[] GetTransNoByPStatus()
        {
            string sql = string.Format(@"  select a.*  from TBLStorLocTrans a where a.status not in ('Release','Close' )");
            sql += string.Format(@" and a.transtype= '{0}'  ", "Transfer");//转储
            return this.DataProvider.CustomQuery(typeof(Storloctrans), new SQLCondition(sql));
        }

        //根据OQC检验单号获取拣货任务令头
        /// <summary>
        /// 根据OQC检验单号获取拣货任务令头
        /// </summary>
        /// <param name="oqcNo">OQC检验单号</param>
        /// <returns></returns>
        public object GetPickByOqcNo(string oqcNo)
        {
            string sql = string.Format(@"SELECT * FROM TBLPICK WHERE PICKNO=(SELECT PICKNO FROM TBLOQC WHERE OQCNO='{0}')", oqcNo);
            object[] objPick = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            if (objPick != null && objPick.Length > 0)
            {
                return objPick[0];
            }
            return null;
        }


        #endregion

        #region ViewField--选择字段 add by jinger 2016-01-04
        /// <summary>
        /// ViewField--选择字段
        /// </summary>
        public ViewField CreateNewViewField()
        {
            return new ViewField();
        }

        public void AddViewField(ViewField viewField)
        {
            this._helper.AddDomainObject(viewField);
        }

        public void UpdateViewField(ViewField viewField)
        {
            this._helper.UpdateDomainObject(viewField);
        }

        public void DeleteViewField(ViewField viewField)
        {
            this._helper.DeleteDomainObject(viewField);
        }

        public void DeleteViewField(ViewField[] viewField)
        {
            this._helper.DeleteDomainObject(viewField);
        }

        public object GetViewField(string userCode, decimal sequence, string tableName)
        {
            return this.DataProvider.CustomSearch(typeof(ViewField), new object[] { userCode, sequence, tableName });
        }

        //查询PickHeadViewField的总行数
        /// <summary>
        /// 查询PickHeadViewField的总行数
        /// </summary>
        /// <param name="userCode">UserCode，模糊查询</param>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <returns> ViewField的总记录数</returns>
        public int QueryViewFieldCount(string userCode, string tableName, decimal sequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(1) from TBLViewField where 1=1 and USERCODE like '{0}%'  and TableName like '{1}%' and SEQ like '{2}%' ", userCode, tableName, sequence)));
        }

        //分页查询PickHeadViewField
        /// <summary>
        /// 分页查询PickHeadViewField
        /// </summary>
        /// <param name="userCode">UserCode，模糊查询</param>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> PickHeadViewField数组</returns>
        public object[] QueryViewField(string userCode, string tableName, decimal sequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ViewField), new PagerCondition(string.Format("select {0} from TBLViewField where 1=1 and USERCODE like '{1}%' and TableName like '{2}%' and SEQ like '{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ViewField)), userCode, tableName, sequence), "USERCODE,SEQ", inclusive, exclusive));
        }

        //根据表名获得所有的ViewField
        /// <summary>
        /// 根据表名获得所有的ViewField
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>ViewField的总记录数</returns>
        public object[] GetAllViewField(string tableName)
        {
            return this.DataProvider.CustomQuery(typeof(ViewField), new SQLCondition(string.Format("select {0} from TBLViewField WHERE TableName='{1}' order by USERCODE,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ViewField)), tableName)));
        }

        public object[] QueryViewFieldByUserCode(string userCode, string tableName)
        {
            string strSql = string.Format("SELECT * FROM tblViewFiled WHERE UserCode='{0}' AND TableName='{1}' ORDER BY SEQ ", userCode, tableName);
            return this.DataProvider.CustomQuery(typeof(ViewField), new SQLCondition(strSql));
        }
        public object[] QueryViewFieldDefault(string defaultUserCode, string tableName)
        {
            string strSql = string.Format("SELECT * FROM tblViewFiled WHERE UserCode='{0}' AND TableName='{1}' ORDER BY SEQ ", defaultUserCode, tableName);
            return this.DataProvider.CustomQuery(typeof(ViewField), new SQLCondition(strSql));
        }

        //保存字段
        /// <summary>
        /// 保存字段
        /// </summary>
        /// <param name="userCode">用户代码</param>
        /// <param name="defaultUserCode">默认用户代码</param>
        ///  <param name="tableName">表名</param>
        /// <param name="moFieldList">字段List</param>
        public void SaveViewField(string userCode, string defaultUserCode, string tableName, string fieldList)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                string strSql = string.Format("DELETE FROM tblViewFiled WHERE UserCode='{0}' AND TableName='{1}' ", userCode, tableName);
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
                object[] objs = this.QueryViewFieldDefault(defaultUserCode, tableName);
                Hashtable htDesc = new Hashtable();
                if (objs != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        ViewField field = (ViewField)objs[i];
                        htDesc.Add(field.FieldName, field.Description);
                    }
                }
                string[] moField = fieldList.Split(';');
                for (int i = 0; i < moField.Length; i++)
                {
                    if (moField[i].Trim() != string.Empty)
                    {
                        ViewField field = new ViewField();
                        field.UserCode = userCode;
                        field.Sequence = i;
                        field.TableName = tableName;
                        field.FieldName = moField[i];
                        field.Description = htDesc[field.FieldName].ToString();
                        this.AddViewField(field);
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        #endregion

        #region ASN-- ASN主表  add by jinger 2016-01-28
        /// <summary>
        /// TBLASN-- ASN主表 
        /// </summary>
        public ASN CreateNewASN()
        {
            return new ASN();
        }

        public void AddASN(ASN asn)
        {
            this._helper.AddDomainObject(asn);
        }

        public void DeleteASN(ASN asn)
        {
            this._helper.DeleteDomainObject(asn);
        }

        public void DeleteASN(ASN[] asns)
        {
            this._helper.DeleteDomainObject(asns);
        }
        public void DeleteASN1(ASN[] asns)
        {
            //this._helper.DeleteDomainObject(asns);
            foreach (ASN asn in asns)
            {
                DeleteASN(asn);
            }
        }
        public void UpdateASN(ASN asn)
        {
            this._helper.UpdateDomainObject(asn);
        }

        public object GetASN(string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(ASN), new object[] { Stno });
        }

        //根据ASN状态获取ASN信息
        /// <summary>
        /// 根据ASN状态获取ASN信息
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public object[] GetASNByStatus(string status)
        {
            string sql = string.Format("SELECT {0} FROM TBLASN WHERE STATUS='{1}' ORDER BY MDATE DESC,MTIME DESC", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)), status);
            return this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
        }

        //获取所有待初检状态的入库指令号（pda）
        public string[] GetASNByStatus()
        {
            string sql = string.Format("SELECT stno FROM TBLASN WHERE STATUS='Receive' ORDER BY MDATE DESC,MTIME DESC", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)));
            object[] obj = this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
            string[] str;
            if (obj != null)
            {
                str = new string[obj.Length];
                int i = 0;
                foreach (ASN asn in obj)
                {
                    str[i] = asn.StNo;
                    i++;
                }
                return str;
            }
            return null;
        }

        //获取所有待初检状态的入库指令号（pda）
        public string[] GetAsnDetailsFromStNo(string stNo)
        {
            string sql = string.Format("SELECT stno FROM TBLASN WHERE STATUS='Receive' ORDER BY MDATE DESC,MTIME DESC", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)));
            object[] obj = this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
            string[] str;
            if (obj != null)
            {
                str = new string[obj.Length];
                int i = 0;
                foreach (ASN asn in obj)
                {
                    str[i] = asn.StNo;
                    i++;
                }
                return str;
            }
            return null;
        }


        //根据指令号判断是否是紧急物料
        public string GetFlagBYStno(string stno)
        {
            string sql = "select EXIGENCY_FLAG from tblasn where stno = '" + stno + "'";
            object[] obj = this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
            if (obj != null)
            {
                return ((ASN)obj[0]).ExigencyFlag;
            }
            return null;
        }



        //ASN查询
        /// <summary>
        /// ASN查询
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        /// <param name="stType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="cUser">创建人</param>
        /// <param name="status">状态</param>
        /// <param name="bCDate">创建日期开始</param>
        /// <param name="eCDate">创建日期结束</param>
        /// <param name="inclusive">开始行号</param>
        /// <param name="exclusive">结束行号</param>
        /// <returns></returns>
        public object[] QueryASN(string stNo, string stType, string invNo, string cUser, string chk, int bCDate, int eCDate, bool IsHasRejection, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  {0} FROM TBLASN  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ASN)));

            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   STATUS  in ({0})", chk);
            }
            if (IsHasRejection)
            {

                sql += string.Format(@" AND   stno  in (   select stno   from
                        (  select a.stno,a.status,  a.RETURNQTY,a.REFORMQTY ,b.rejectCount,
                               (CASE a.status WHEN 'Release'  THEN a.RETURNQTY+a.REFORMQTY
                     WHEN 'WaitReceive'  THEN  a.RETURNQTY+a.REFORMQTY
                       WHEN 'Receive'  THEN  a.RETURNQTY+a.REFORMQTY
                     ELSE a.RETURNQTY+a.REFORMQTY +b.rejectCount
                         END ) AS sumqty  
                          from 
                      （ select a.stno,a.status,nvl(b.RETURNQTY,0) RETURNQTY,nvl(b.REFORMQTY,0) REFORMQTY   from tblasn a 
                    left join 
                    (SELECT nvl(sum(RETURNQTY),0) RETURNQTY , nvl(sum(REFORMQTY),0) REFORMQTY, sTNo FROM TBLASNIQCDETAIL  
                    group by  stno) b on b.stno=a.stno）A  ,
                      （ select a.stno,a.status,nvl(c.rejectCount,0) rejectCount   from tblasn a 
                      left join 
                    (SELECT nvl(sum(d.Qty - d.ReceiveQty),0) rejectCount , d.stno from TBLASNDETAIL d group by d.stno )  c on c.stno=a.stno）B
                    where a.stno=b.stno
                      ) 
                    where sumqty>0   ) ");
                //状态：初检拒收+IQC拒收+（现场整改数量+退换货数量+初检拒收数量！=0）
                //sql += string.Format(@" AND   STATUS  in ( 'IQCRejection' , 'ReceiveRejection' ) ");
            }

            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND STNO ='{0}'", stNo);
            }
            if (!string.IsNullOrEmpty(stType))
            {
                if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
                {
                    sql += string.Format(@" AND STTYPE IN ({0})", stType);
                }
                else
                {
                    sql += string.Format(@" AND STTYPE IN ('{0}')", stType);
                }
            }

            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(cUser))
            {
                sql += string.Format(@" AND CUSER = '{0}'", cUser);
            }
            //if (!string.IsNullOrEmpty(status))
            //{
            //    sql += string.Format(@" AND STATUS = '{0}'", status);
            //}
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            return this.DataProvider.CustomQuery(typeof(ASN), new PagerCondition(sql, "STNO DESC", inclusive, exclusive));
        }

        //ASN总行数
        /// <summary>
        /// ASN总行数
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        /// <param name="stType">入库类型</param>
        /// <param name="invNo">SAP单据号</param>
        /// <param name="cUser">创建人</param>
        /// <param name="status">状态</param>
        /// <param name="bCDate">创建日期开始</param>
        /// <param name="eCDate">创建日期结束</param>
        /// <returns></returns>
        public int QueryASNCount(string stNo, string stType, string invNo, string cUser, string chk, int bCDate, int eCDate, bool IsHasRejection)
        {
            string sql = @" SELECT COUNT(1) FROM TBLASN WHERE 1=1 ";
            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND STNO ='{0}'", stNo);
            }
            if (IsHasRejection)
            {

                sql += string.Format(@" AND   stno  in (   select stno   from
                        (  select a.stno,a.status,  a.RETURNQTY,a.REFORMQTY ,b.rejectCount,
                               (CASE a.status WHEN 'Release'  THEN a.RETURNQTY+a.REFORMQTY
                     WHEN 'WaitReceive'  THEN  a.RETURNQTY+a.REFORMQTY
                       WHEN 'Receive'  THEN  a.RETURNQTY+a.REFORMQTY
                     ELSE a.RETURNQTY+a.REFORMQTY +b.rejectCount
                         END ) AS sumqty  
                          from 
                      （ select a.stno,a.status,nvl(b.RETURNQTY,0) RETURNQTY,nvl(b.REFORMQTY,0) REFORMQTY   from tblasn a 
                    left join 
                    (SELECT nvl(sum(RETURNQTY),0) RETURNQTY , nvl(sum(REFORMQTY),0) REFORMQTY, sTNo FROM TBLASNIQCDETAIL  
                    group by  stno) b on b.stno=a.stno）A  ,
                      （ select a.stno,a.status,nvl(c.rejectCount,0) rejectCount   from tblasn a 
                      left join 
                    (SELECT nvl(sum(d.Qty - d.ReceiveQty),0) rejectCount , d.stno from TBLASNDETAIL d group by d.stno )  c on c.stno=a.stno）B
                    where a.stno=b.stno
                      ) 
                    where sumqty>0   ) ");
                //状态：初检拒收+IQC拒收+（现场整改数量+退换货数量+初检拒收数量！=0）
                //sql += string.Format(@" AND   STATUS  in ( 'IQCRejection' , 'ReceiveRejection' ) ");
            }
            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   STATUS  in ({0})", chk);
            }

            if (!string.IsNullOrEmpty(stType))
            {
                if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
                {
                    sql += string.Format(@" AND STTYPE IN ({0})", stType);
                }
                else
                {
                    sql += string.Format(@" AND STTYPE IN ('{0}')", stType);
                }
            }

            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(cUser))
            {
                sql += string.Format(@" AND CUSER = '{0}'", cUser);
            }
            //if (!string.IsNullOrEmpty(status))
            //{
            //    sql += string.Format(@" AND STATUS = '{0}'", status);
            //}
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //根据入库指令号更新ASN状态
        /// <summary>
        /// 根据入库指令号更新ASN状态
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        public void UpdateASNStatusByStNo(string status, string stNo)
        {
            string sql = string.Format(@"UPDATE TBLASN SET STATUS='{0}' WHERE  STNO IN ({1}) ", status, stNo);

            //if (!string.IsNullOrEmpty(stNo) && stNo.IndexOf(",") > 0)
            //{

            //    sql += string.Format(@" AND STNO IN ({0})", stNo);
            //}
            //else
            //{
            //    sql += string.Format(@" AND STNO IN ('{0}')", stNo);
            //}

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region ASNDetail-- ASN明细表  add by jinger 2016-01-28
        /// <summary>
        /// TBLASNDETAIL-- ASN明细表 
        /// </summary>
        public ASNDetail CreateNewASNDetail()
        {
            return new ASNDetail();
        }

        public void AddASNDetail(ASNDetail asndetail)
        {
            this._helper.AddDomainObject(asndetail);
        }

        public void DeleteASNDetail(ASNDetail asndetail)
        {
            this._helper.DeleteDomainObject(asndetail);
        }

        public void UpdateASNDetail(ASNDetail asndetail)
        {
            this._helper.UpdateDomainObject(asndetail);
        }

        public object GetASNDetail(int Stline, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(ASNDetail), new object[] { Stline, Stno });
        }

        //根据单据号获取入库指令明细
        /// <summary>
        /// 根据单据号获取入库指令明细
        /// </summary>
        /// <param name="Stno">SAP单据号</param>
        /// <returns></returns>
        public object[] GetASNDetailByStNo(string Stno)
        {
            string sql = string.Format("SELECT * FROM TBLASNDETAIL WHERE STNO='{0}'", Stno);
            object[] objASNDetail = this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
            if (objASNDetail != null && objASNDetail.Length > 0)
            {
                return objASNDetail;
            }
            return null;
        }
        //检查指定状态下的入库指令号是否有明细
        /// <summary>
        /// 检查指定状态下的入库指令号是否有明细
        /// </summary>
        /// <param name="stNo">入库指令号</param>
        /// <param name="status">状态</param>
        /// <returns>有：true;没有：false</returns>
        public bool CheckASNHasDetail(string stNo, string status)
        {
            string sql = string.Format(@"SELECT COUNT(1) FROM TBLASNDETAIL WHERE STNO='{0}' AND STATUS <> '{1}' ", stNo, status);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckAsnHasDetailByCarton(string carton, string status)
        {
            string sql = string.Format(@"SELECT COUNT(1)   FROM tblasndetail a where a.cartonno in(
            SELECT a.cartonno  FROM tblAsnIQCDetail a where a.iqcno in
            (SELECT a.iqcno FROM tblAsnIQCDetail a where a.cartonno='{0}') ) AND STATUS <> '{1}' ", carton, status);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckASNHasDetail(string stNo)
        {
            string sql = string.Format(@"SELECT COUNT(1) FROM TBLASNDETAIL WHERE STNO='{0}'  ", stNo);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool CheckASNReceiveStatusHasDetail(string stNo, string status)
        {
            string sql = string.Format(@"SELECT COUNT(1) FROM TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus <>  '{1}' ", stNo, status);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return false;
            }
            //=0时，全部为拒收状态
            return true;
        }

        //根据入库指令号更新ASN明细状态
        /// <summary>
        /// 根据入库指令号更新ASN明细状态
        /// </summary>
        /// <param name="status">入库指令状态</param>
        /// <param name="stNo">入库指令号</param>
        public void UpdateASNDetailStatusByStNo(string status, string stNo)
        {
            string sql = string.Format(@"UPDATE TBLASNDETAIL SET STATUS='{0}' WHERE  STNO IN ({1}) ", status, stNo);
            //if (!string.IsNullOrEmpty(stNo) && stNo.IndexOf(",") > 0)
            //{

            //    sql += string.Format(@" AND STNO IN ({0})", stNo);
            //}
            //else
            //{
            //    sql += string.Format(@" AND STNO IN ('{0}')", stNo);
            //}
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        //根据入库指令号和状态更新ASN明细状态
        /// <summary>
        /// 根据入库指令号和状态更新ASN明细状态
        /// </summary>
        /// <param name="status">入库指令状态</param>
        /// <param name="stNo">入库指令号</param>
        /// <param name="statusCondition">入库指令状态条件</param>
        public void UpdateASNDetailStatusByStNo(string status, string stNo, string statusCondition)
        {
            string sql = string.Format(@"UPDATE TBLASNDETAIL SET STATUS='{0}' WHERE STNO IN  ({1}) ", status, stNo);
            if (string.IsNullOrEmpty(statusCondition))
            {
                sql += string.Format(" AND STATUS='{0}'", statusCondition);
            }
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        /// <summary>
        /// 初检签收查询(BS) add by bela 20160222
        /// </summary>
        /// <param name="stNO">入库指令号</param>
        /// <returns></returns>
        public object[] QueryASNDetail(string stNO, string status, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT a.*,
                                               b.sttype,
                                               b.invno,
                                               c.mcontroltype
                                          FROM tblasndetail a
                                               INNER JOIN tblasn b
                                                   ON b.stno = a.stno
                                               LEFT JOIN tblmaterial c
                                                   ON c.mcode = a.mcode
                                         WHERE   a.stno = '{0}'", stNO);
            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(" and b.status in ({0}) ", status);
            }
            return this.DataProvider.CustomQuery(typeof(AsndetailEX), new PagerCondition(sql, "a.stline", inclusive, exclusive));
        }
        public int QueryASNDetailCount(string stNo, string status)
        {
            string sql = string.Format(@"SELECT count(*)
                                          FROM tblasndetail a
                                               INNER JOIN tblasn b
                                                   ON b.stno = a.stno
                                               LEFT JOIN tblmaterial c
                                                   ON c.mcode = a.mcode
                                         WHERE a.stno = '{0}'", stNo);
            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(" and b.status in ({0} ) ", status);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));// b.status = 'Receive' 
        }

        public object[] QueryASNDetailBYSno(string stNO)
        {
            string sql = "SELECT a.* FROM tblasndetail a where a.stno = '" + stNO + "' and a.status ='Receive'";
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        public object[] QueryASNDetailBYReject(string stNO)
        {
            string sql = "SELECT a.* FROM tblasndetail a where a.stno = '" + stNO + "' and a.status <>'" + ASNDetail_STATUS.ASNDetail_ReceiveClose + "'";
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }


        //add by sam 2016年2月25日08:18:55
        public object[] QueryASNDetailBySTNoCheckIqcStatus(string stNO, string status)
        {
            string sql = string.Format(@"SELECT * FROM  TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus = '{1}' ", stNO, status);
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }
        //求Qty
        public int GetReceiveQtyByStatus(string stNO, string status)
        {
            string sql = string.Format(@"SELECT sum(ReceiveQty) FROM  TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus <> '{1}' ", stNO, status);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetActQtyByStatus(string stNO, string status)
        {
            string sql = string.Format(@"SELECT sum(ActQty) FROM  TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus <> '{1}' ", stNO, status);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetQcPassQtyByStatus(string stNO, string dqmcode, string status)
        {
            string sql = string.Format(@"SELECT sum(QCPASSQTY) FROM  TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus <> '{1}' and dqmcode='{2}' ", stNO, status, dqmcode);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetReceiveQtyByStatus(string stNO, string dqmcode, string status)
        {
            string sql = string.Format(@"SELECT sum(ReceiveQty) FROM  TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus <> '{1}' and dqmcode='{2}' ", stNO, status, dqmcode);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #region  qty
        //add by sam 2016年3月23日
        public int GetReceiveQtyInAsn(string stno, string invno, int invline)
        {
            string sql = string.Format(@"select sum（ReceiveQTY） from tblasndetailitem where stno='{0}'  and invno='{1}'  and invline='{2}'  ", stno, invno, invline);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        //add by sam 2016年3月23日
        public int GetActQtyInAsn(string stno, string invno, int invline)
        {
            string sql = string.Format(@"select sum（ACTQTY） from tblasndetailitem where stno='{0}'  and invno='{1}'  and invline='{2}'  ", stno, invno, invline);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        //add by sam 2016年3月23日
        public int GetQcPassQtyInAsn(string stno, string invno, int invline)
        {
            string sql = string.Format(@"select sum（QCPASSQTY） from tblasndetailitem where stno='{0}'  and invno='{1}'  and invline='{2}'  ", stno, invno, invline);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        // add by Amy @20160411
        public object GetQcRejectQtyFromASNDetailItem(string stno, string invno, int invline)
        {
            string sql = string.Format(@"select sum（QCPASSQTY） as QCPASSQTY ,sum(ReceiveQTY) as ReceiveQTY , sum(ACTQTY) as ACTQTY from tblasndetailitem where stno='{0}'  and invno='{1}'  and invline='{2}' ", stno, invno, invline);
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));
            if (objs != null)
                return objs[0];
            return null;
        }




        public object GetQcRejectQtyFromASNDetailItem(string stno, string invno, string invline, string stline)
        {
            string sql = string.Format(@"select sum（QCPASSQTY） as QCPASSQTY ,sum(ReceiveQTY) as ReceiveQTY , sum(ACTQTY) as ACTQTY from tblasndetailitem where stno='{0}'  and invno='{1}'  and invline='{2}' and stline='{3}' ", stno, invno, invline, stline);
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));
            if (objs != null)
                return objs[0];
            return null;
        }

        public object[] GetQcRejectQtyFromASNDetailItem1(string stno, List<string> stlines)
        {
            string sql = string.Format(@"select INVLINE, sum（QCPASSQTY) as QCPASSQTY ,sum(ReceiveQTY) as ReceiveQTY , sum(ACTQTY) as ACTQTY from tblasndetailitem where stno='{0}'  and stline in (" + SqlFormat(stlines) + ")  group by invline", stno);
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));

            return objs;
        }

        public object[] GetInvoiceLineFromASNDetailItem(string stno, string stline)
        {
            string sql = string.Format(@"select {0} from tblasndetailitem where stno='{1}'  and stline='{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailitem)), stno, stline);
            return this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));

        }
        public object GetACTQTYFromASNDetailItem(string stno, string stline)
        {
            string sql = string.Format(@"select sum（QCPASSQTY） as QCPASSQTY ,sum(ReceiveQTY) as ReceiveQTY , sum(ACTQTY) as ACTQTY  from tblasndetailitem where stno='{0}'  and stline='{1}' ", stno, stline);
            return this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql))[0];

        }
        #endregion

        public object[] GetInvoicesDetailByInvNoAndStno(string invno, string stno)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE A.INVNO='{1}'  ",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), invno);
            sql += string.Format(@" and A.INVLINE in ( select  INVLINE from tblasndetailitem where INVNO='{0}' and stno='{1}' ) ", invno, stno);
            sql += "ORDER BY INVLINE";
            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail;
            }
            return null;
        }

        public object[] GetInvoicesDetailByInvNoAndStno1(string invno, string stno)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE A.INVNO='{1}'  ",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), invno);
            sql += string.Format(@" and A.INVLINE in ( select  INVLINE from tblasndetailitem where INVNO='{0}' and stno='{1}' and QCPASSQTY>0 ) ", invno, stno);
            sql += "ORDER BY INVLINE";
            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail;
            }
            return null;
        }


        public object[] GetInvoicesDetailByInvNoAndStno(string invno, string stno, string dqmcode)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLINVOICESDETAIL A WHERE A.INVNO='{1}'  ",
                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvoicesDetail)), invno);
            sql += string.Format(@" and A.INVLINE in ( select  INVLINE from tblasndetailitem where INVNO='{0}' and stno='{1}' ) and a.DQMCODE='{2}' ", invno, stno, dqmcode);

            sql += "ORDER BY INVLINE";
            object[] objInvoicesDetail = this.DataProvider.CustomQuery(typeof(InvoicesDetail), new SQLCondition(sql));
            if (objInvoicesDetail != null && objInvoicesDetail.Length > 0)
            {
                return objInvoicesDetail;
            }
            return null;
        }

        public object[] QueryASNDetailBySTNoCheckStatus(string stNO, string status)
        {
            string sql = string.Format(@"SELECT * FROM  TBLASNDETAIL WHERE STNO='{0}' AND InitReceiveStatus <> '{1}' ", stNO, status);
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }
        public object QueryAsnDetailByStNoAndcartons(string stNo, string cartonno)
        {
            string sql = string.Format(@"SELECT * FROM  TBLASNDETAIL WHERE STNO='{0}' AND CARTONNO='{1}'  ", stNo, cartonno);
            object[] list = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            if (list != null)
            {
                Asndetail asn = list[0] as Asndetail;
                return asn;
            }
            return null;
        }

        //add by sam 2016年2月25日08:18:55
        public object[] QueryAsnDetailForDqMcode(string stNo)
        {
            string sql = string.Format(@"SELECT distinct  dqmcode FROM  TBLASNDETAIL WHERE STNO='{0}'  ", stNo);
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        //add by sam 2016年2月25日08:18:55
        public object[] QueryAsnDetailForCreateIqc(string stNo)
        {
            string sql = string.Format(@"SELECT distinct  dqmcode FROM  TBLASNDETAIL WHERE STNO='{0}'  
              and dqmcode not in ( SELECT  distinct  dqmcode from  TBLASNIQC where STNO='{0}' ) ", stNo);
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        public object[] QueryAsnDetailByStNo(string stNo, string dqmcode)
        {
            string sql = string.Format(@"SELECT * FROM  TBLASNDETAIL WHERE STNO='{0}' AND dqmcode='{1}'  ", stNo, dqmcode);
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        public object[] QueryAsnDetailByStNoAndcarton(string stNo, string cartonno)
        {
            string sql = string.Format(@"SELECT * FROM  TBLASNDETAIL WHERE STNO='{0}' AND CARTONNO='{1}'  ", stNo, cartonno);
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        //add by sam 2016年3月1日08:18:55
        public object[] QueryAsnDetailByStNo(string stNo, string dqmcode, string initReceiveStatus)
        {
            string sql = string.Format(@"SELECT * FROM  TBLASNDETAIL WHERE STNO='{0}' AND dqmcode='{1}'  ", stNo, dqmcode);
            if (!string.IsNullOrEmpty(initReceiveStatus))
            {
                sql += string.Format(@" AND  InitReceiveStatus <>'{0}'  ", initReceiveStatus);
            }
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        public int GetAsnDetailCountForRev(string Stno)
        {
            string sql = "select count(*) from tblasndetail where stno = '" + Stno + "' and status = 'Receive'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetAllDetail(string Stno)
        {
            string sql = "select a.* from tblasndetail a where a.stno = '" + Stno + "'";
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        public object[] GetAllAsnDetailByM(string invno)
        {
            string sql = string.Format("select a.* from tblasndetail a where a.stno in  ( select b.stno  from tblasn b    where b.invno = '{0}' )  ", invno);
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        //public object[] GetAllPickDetailByInvno(string invno)
        //{
        //    string sql = string.Format("select a.* from tblpickdetail a where a.pickno in  ( select b.pickno  from tblpick b    where b.invno = '{0}' )  ", invno);
        //    return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        //}

        #endregion

        #region Asndetailitem--ASN明细对应单据行明细表 add by jinger 2016-02-26
        /// <summary>
        /// TBLASNDETAILITEM--ASN明细对应单据行明细表
        /// </summary>
        public Asndetailitem CreateNewAsndetailitem()
        {
            return new Asndetailitem();
        }

        public void AddAsndetailitem(Asndetailitem asndetailitem)
        {
            this._helper.AddDomainObject(asndetailitem);
        }

        public void DeleteAsndetailitem(Asndetailitem asndetailitem)
        {
            this._helper.DeleteDomainObject(asndetailitem);
        }

        public void UpdateAsndetailitem(Asndetailitem asndetailitem)
        {
            this._helper.UpdateDomainObject(asndetailitem);
        }

        public object GetAsndetailitem(string Invno, int Stline, string Invline, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(Asndetailitem), new object[] { Invno, Stline, Invline, Stno });
        }

        //获取ASN明细对应单据行明细
        /// <summary>
        /// 获取ASN明细对应单据行明细
        /// </summary>
        /// <param name="Stno">ASN单号</param>
        /// <param name="Stline">ASN单行项目</param>
        /// <returns></returns>
        public object[] GetAsnDetailItem(string Stno, int Stline)
        {
            string sql = string.Format(@"SELECT {0} FROM TBLASNDETAILITEM WHERE STNO='{1}' AND STLINE='{2}'  ORDER BY STLINE",
                                            DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailitem)), Stno, Stline);
            return this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));
        }

        #endregion

        #region Asndetailsn add by bela 20160222
        /// <summary>
        /// TBLASNDETAILSN
        /// </summary>
        public Asndetailsn CreateNewAsndetailsn()
        {
            return new Asndetailsn();
        }

        public void AddAsndetailsn(Asndetailsn asndetailsn)
        {
            this.DataProvider.Insert(asndetailsn);
        }

        public void DeleteAsndetailsn(Asndetailsn asndetailsn)
        {
            this.DataProvider.Delete(asndetailsn);
        }

        public void UpdateAsndetailsn(Asndetailsn asndetailsn)
        {
            this.DataProvider.Update(asndetailsn);
        }

        public object GetAsndetailsn(string Sn, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(Asndetailsn), new object[] { Sn, Stno });
        }

        //add by jinger 20160226
        public object GetAsndetailsn(string Sn, string Stno, int Stline)
        {
            return this.DataProvider.CustomSearch(typeof(Asndetailsn), new object[] { Sn, Stno, Stline });
        }

        public object[] QueryASNDetailSN(string Sn)
        {
            string sql = "SELECT * FROM tblasndetailsn where sn = '" + Sn + "'";
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }

        public List<AsnHead> QueryASNDetailSNCatron(string Sn)
        {
            List<AsnHead> alist = new List<AsnHead>();
            object[] objs = null;
            string sql = "SELECT * FROM tblasndetailsn where sn in (" + SqlFormat(Sn.ToUpper().Split(',')) + ") or CARTONNO in (" + SqlFormat(Sn.ToUpper().Split(',')) + ")";
            objs = this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
            if (objs != null)
            {

                foreach (Asndetailsn sn in objs)
                {
                    AsnHead sss = new AsnHead();
                    sss.STlINE = sn.Stline;
                    sss.STNO = sn.Stno;
                    alist.Add(sss);
                }
            }
            else
            {
                sql = "SELECT * FROM tblasndetail where CARTONNO in (" + SqlFormat(Sn.ToUpper().Split(',')) + ")";
                objs = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));

                if (objs != null)
                {
                    foreach (Asndetail sn in objs)
                    {
                        AsnHead sss = new AsnHead();
                        sss.STlINE = sn.Stline;
                        sss.STNO = sn.Stno;
                        alist.Add(sss);
                    }
                }
            }
            return alist;
        }

        public Asndetailsn[] QueryASNDetailSN1(string Sn)
        {
            List<BenQGuru.eMES.Domain.Warehouse.Asndetailsn> list = new List<BenQGuru.eMES.Domain.Warehouse.Asndetailsn>();

            string sql = "SELECT  a.stno,a.stline FROM tblasndetailsn a where sn = '" + Sn + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.Warehouse.Asndetailsn), new SQLCondition(sql));

            if (oo != null && oo.Length > 0)
            {
                foreach (Asndetailsn a in oo)
                {
                    list.Add(a);
                }
            }
            return list.ToArray();

        }

        public object[] GetSNbySTNo(string stno, string stline, string sn)
        {
            string sql = "select a.* from tblasndetailsn a where a.stno = '" + stno + "' and a.stline='" + stline + "' and a.sn = '" + sn + "'";
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        public object[] GetSNbySTNo(string stno, string stline)
        {
            string sql = "select a.* from tblasndetailsn a where a.stno = '" + stno + "' and a.stline='" + stline + "' ";
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }
        //根据ASN单号获取ASN明细SN数据 add by jinger 20160225
        /// <summary>
        /// 根据ASN单号获取ASN明细SN数据
        /// </summary>
        /// <param name="stNo">ASN单号</param>
        /// <returns></returns>
        public object[] GetASNDetaileSNByStNo(string stNo)
        {
            return this.DataProvider.CustomQuery(typeof(Asndetailsn),
                                                 new SQLCondition(string.Format(@"SELECT {0} FROM TBLASNDETAILSN  WHERE STNO='{1}'",
                                                 DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailsn)), stNo))
                                                 );
        }

        #endregion

        #region Asniqcdetailsn add by sam 2016年2月25日
        /// <summary>
        /// TBLASNIQCDETAILSN
        /// </summary>
        public AsnIqcDetailSN CreateNewAsnIqcDetailSN()
        {
            return new AsnIqcDetailSN();
        }

        public void AddAsnIqcDetailSN(AsnIqcDetailSN asniqcdetailsn)
        {
            this.DataProvider.Insert(asniqcdetailsn);
        }

        public void DeleteAsnIqcDetailSN(AsnIqcDetailSN asniqcdetailsn)
        {
            this.DataProvider.Delete(asniqcdetailsn);
        }

        public void UpdateAsnIqcDetailSN(AsnIqcDetailSN asniqcdetailsn)
        {
            this.DataProvider.Update(asniqcdetailsn);
        }

        public object GetAsnIqcDetailSN(int Stline, string Iqcno, string Sn, string Stno)
        {
            return this.DataProvider.CustomSearch(typeof(AsnIqcDetailSN), new object[] { Stline, Iqcno, Sn, Stno });
        }

        #endregion


        #region Invdoc-- 单据文件表  add by jinger 2016-01-31
        /// <summary>
        /// TBLINVDOC-- 单据文件表 
        /// </summary>
        public InvDoc CreateNewInvDoc()
        {
            return new InvDoc();
        }

        public void AddInvDoc(InvDoc invdoc)
        {
            this._helper.AddDomainObject(invdoc);
        }

        public void DeleteInvDoc(InvDoc invdoc)
        {
            this._helper.DeleteDomainObject(invdoc);
        }

        public void UpdateInvDoc(InvDoc invdoc)
        {
            this._helper.UpdateDomainObject(invdoc);
        }

        public object GetInvDoc(int Docserial)
        {
            return this.DataProvider.CustomSearch(typeof(InvDoc), new object[] { Docserial });
        }


        public void DeleteDoc(string fileName)
        {
            string sql = "DELETE FROM TBLINVDOC WHERE docname = '" + fileName + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object[] GetDoctypeByInvDocNo()
        {
            return this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(string.Format(@"SELECT distinct doctype FROM TBLINVDOC  where doctype is not null ")));
        }

        public object[] GetDirnameByInvDocNo()
        {
            return this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(string.Format(@"SELECT distinct dirname FROM TBLINVDOC where dirname is not null  ")));
        }
        //根据单据号获取单据文件信息
        /// <summary>
        /// 根据单据号获取单据文件信息
        /// </summary>
        /// <param name="invDocNo">单据号</param>
        /// <returns></returns>
        public object[] GetInvDocByInvDocNo(string invDocNo)
        {
            return this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(string.Format(@"SELECT * FROM TBLINVDOC WHERE INVDOCNO='{0}'", invDocNo)));
        }

        //检查指定单据类型的单据号是否有导入文件
        /// <summary>
        /// 检查指定单据类型的单据号是否有导入文件
        /// </summary>
        /// <param name="invDocNo">单据号</param>
        /// <param name="invDocType">单据文件类型</param>
        /// <returns>有：true;没有：false</returns>
        public bool CheckHasInvDoc(string invDocNo, string invDocType)
        {
            string sql = string.Format(@"SELECT COUNT(1) FROM TBLINVDOC WHERE INVDOCNO ='{0}' AND INVDOCTYPE = '{1}' ", invDocNo, invDocType);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 初检签收图片查询 add by bela 20150223
        /// </summary>
        /// <returns></returns>
        public object[] QueryInvDoc(string stno, int inclusive, int exclusive)
        {
            string sql = "select a.* from TBLINVDOC a where  a.INVDOCTYPE in ('InitReject','InitGivein') and  a.invdocno='" + stno + "' ORDER BY A.MDATE DESC";
            return this.DataProvider.CustomQuery(typeof(InvDoc), new PagerCondition(sql, "INVDOCNO", inclusive, exclusive));
        }
        public int QueryInvDocCount(string stno)
        {
            string sql = "select count(*) from TBLINVDOC where  INVDOCTYPE in ('InitReject','InitGivein') and  invdocno='" + stno + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public object[] QueryInvDoc(string stno)
        {
            string sql = "select a.* from TBLINVDOC a where  a.INVDOCTYPE in ('InitReject','InitGivein') and  a.invdocno='" + stno + "' ORDER BY A.MDATE DESC";
            return this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(sql));
        }

        //更具参数组获取所有参数(BS)
        public object[] GetDrpDesc(string paramgroup)
        {
            string sql = "select a.* from tblsysparam a where a.paramgroupcode='" + paramgroup + "'";
            return this.DataProvider.CustomQuery(typeof(Domain.BaseSetting.Parameter), new SQLCondition(sql));
        }

        //更具参数组获取所有参数(pda)
        public string[] GetDrpDesc_PDA(string paramgroup)
        {
            string sql = "select a.* from tblsysparam a where a.paramgroupcode='" + paramgroup + "'";
            object[] obj = this.DataProvider.CustomQuery(typeof(Domain.BaseSetting.Parameter), new SQLCondition(sql));
            string[] str;
            if (obj != null)
            {
                str = new string[obj.Length];
                int i = 0;
                foreach (Domain.BaseSetting.Parameter parameter in obj)
                {
                    str[i++] = parameter.ParameterDescription;
                }
                return str;
            }
            return null;
        }

        //
        public void UpdateASNItem(string stno, string stline)
        {
            string sql = "UPDATE tblasndetailitem SET RECEIVEQTY=QTY  WHERE STNO='" + stno + "' AND STLINE='" + stline + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        #endregion
        #region  Amy Add @20160408
        public int GetASNDetailCountCartonNoNutNull(string Stno)
        {
            string sql = "SELECT COUNT(*) FROM TBLASNDETAIL WHERE STNO='" + Stno + "' AND CARTONNO IS NULL";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        public object[] GetFileUpLoad(string invDocNo, string invDocType)
        {
            string sql = string.Format("SELECT {0} FROM TBLINVDOC WHERE INVDOCNO='{1}' AND INVDOCTYPE='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvDoc)), invDocNo, invDocType);
            return this.DataProvider.CustomQuery(typeof(InvDoc), new SQLCondition(sql));
        }
        #region Rslog
        /// <summary>
        /// TBLRSLOG
        /// </summary>
        public Rslog CreateNewRslog()
        {
            return new Rslog();
        }

        public void AddRslog(Rslog rslog)
        {
            this.DataProvider.Insert(rslog);
        }

        public void DeleteRslog(Rslog rslog)
        {
            this.DataProvider.Delete(rslog);
        }

        public void UpdateRslog(Rslog rslog)
        {
            this.DataProvider.Update(rslog);
        }

        public object GetRslog(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Rslog), new object[] { Serial });
        }

        public int GetMaxSerialFromRSLog()
        {
            string sql = "  select max(serial) as serial from tblrslog ";
            return Int32.Parse(((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql).Tables[0].Rows[0][0].ToString());
        }

        #endregion
        #region Dnlog_in
        /// <summary>
        /// TBLDNLOG_IN
        /// </summary>
        public Dnlog_in CreateNewDnlog_in()
        {
            return new Dnlog_in();
        }

        public void AddDnlog_in(Dnlog_in dnlog_in)
        {
            this.DataProvider.Insert(dnlog_in);
        }

        public void DeleteDnlog_in(Dnlog_in dnlog_in)
        {
            this.DataProvider.Delete(dnlog_in);
        }

        public void UpdateDnlog_in(Dnlog_in dnlog_in)
        {
            this.DataProvider.Update(dnlog_in);
        }

        public object GetDnlog_in(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Dnlog_in), new object[] { Serial });
        }

        #endregion
        #region Wwpolog
        /// <summary>
        /// TBLWWPOLOG
        /// </summary>
        public Wwpolog CreateNewWwpolog()
        {
            return new Wwpolog();
        }

        public void AddWwpolog(Wwpolog wwpolog)
        {
            this.DataProvider.Insert(wwpolog);
        }

        public void DeleteWwpolog(Wwpolog wwpolog)
        {
            this.DataProvider.Delete(wwpolog);
        }

        public void UpdateWwpolog(Wwpolog wwpolog)
        {
            this.DataProvider.Update(wwpolog);
        }

        public object GetWwpolog(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Wwpolog), new object[] { Serial });
        }

        #endregion

        #region Ublog
        /// <summary>
        /// TBLUBLOG
        /// </summary>
        public Ublog CreateNewUblog()
        {
            return new Ublog();
        }

        public void AddUblog(Ublog ublog)
        {
            this.DataProvider.Insert(ublog);
        }

        public void DeleteUblog(Ublog ublog)
        {
            this.DataProvider.Delete(ublog);
        }

        public void UpdateUblog(Ublog ublog)
        {
            this.DataProvider.Update(ublog);
        }

        public object GetUblog(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Ublog), new object[] { Serial });
        }

        #endregion

        #endregion

        #region Stack
        public SStack CreateSStack()
        {
            return new SStack();
        }

        public void AddSStack(SStack sstack)
        {
            this._helper.AddDomainObject(sstack);
        }

        public void UpdateSStack(SStack sstack)
        {
            this._helper.UpdateDomainObject(sstack);
        }

        public void DeleteSStack(SStack sstack)
        {
            this._helper.DeleteDomainObject(sstack);
        }

        public void DeleteSStack(SStack[] sstack)
        {
            this._helper.DeleteDomainObject(sstack);
        }

        public object GetSStack(string stackCode)
        {
            return this.DataProvider.CustomSearch(typeof(SStack), new object[] { stackCode });
        }

        public object[] QueryStack(string storageCode, string storageName, string stackCode, string statckDescription, int orgId, int inclusive, int exclusive)
        {
            string sql = string.Format("select t1.stackcode AS STACKCODE, t1.storagecode AS STORAGECODE, t2.storagename AS STORAGENAME, t1.stackdesc AS STACKDESC, "
            + "t1.capacity AS CAPACITY, t1.muser AS MUSER, t1.mdate AS MDATE,t1.isoneitem AS isoneitem "
            + "from tblstack t1, tblstorage t2 where t1.storagecode = t2.storagecode and t1.orgid = {0} "
            + "and t1.storagecode like '%{1}%' and t2.storagename like '%{2}%' and t1.stackcode like '%{3}%' and t1.stackdesc like '%{4}%'", orgId, storageCode, storageName, stackCode, statckDescription);

            return this.DataProvider.CustomQuery(typeof(SStackWithStorageName), new PagerCondition(sql, "t1.stackcode", inclusive, exclusive));
        }

        public int QueryStackCount(string storageCode, string storageName, string stackCode, string statckDescription, int orgId)
        {
            string sql = string.Format("select count(*) from tblstack t1, tblstorage t2 where t1.storagecode = t2.storagecode and t1.orgid = {0} "
            + "and t1.storagecode like '%{1}%' and t2.storagename like '%{2}%' and t1.stackcode like '%{3}%' and t1.stackdesc like '%{4}%'", orgId, storageCode, storageName, stackCode, statckDescription);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public object[] GetStack(string storageCode)
        {
            string sql = "select DISTINCT stackcode,storagecode from tblstack where 1=1 ";
            if (storageCode != string.Empty)
            {
                sql += " and storagecode = '" + storageCode + "'";
            }
            sql += " ORDER BY storagecode";
            object[] obj = this.DataProvider.CustomQuery(typeof(SStack), new SQLCondition(sql));
            if (obj != null && obj.Length > 0)
            {
                return obj;
            }
            return null;
        }

        //public object[] GetAllStack()
        //{
        //    return this.DataProvider.CustomQuery(
        //        typeof(Storage), new SQLCondition(
        //        string.Format("select {0} from tblstorage ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)))
        //        )
        //        );
        //}
        #endregion

        #region StackToRcard
        public StackToRcard CreateStackToRcard()
        {
            return new StackToRcard();
        }

        public void AddStackToRcard(StackToRcard stackToRcard)
        {
            this.DataProvider.Insert(stackToRcard);
        }

        public void UpdateStackToRcard(StackToRcard stackToRcard)
        {
            this.DataProvider.Update(stackToRcard);
        }

        public void DeleteStackToRcard(StackToRcard stackToRcard)
        {
            this.DataProvider.Delete(stackToRcard);
        }

        public object GetStackToRcard(string serialNo)
        {
            string sql = "";
            sql += " SELECT {0} FROM tblstack2rcard ";
            sql += " WHERE SerialNo = '" + serialNo.Trim().ToUpper() + "' ";
            sql += " AND ROWNUM <= 1 ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StackToRcard)));

            object[] returnValue = this.DataProvider.CustomQuery(typeof(StackToRcard), new SQLCondition(sql));
            if (returnValue == null)
            {
                return null;
            }

            return returnValue[0];
        }

        public object GetStackToRcard(string serialNo, string cartonno)
        {
            return this.DataProvider.CustomSearch(typeof(StackToRcard), new object[] { serialNo, cartonno });
        }

        public object[] GetAnyStack2RCardByStack(string stackCode)
        {
            string sql = "";
            sql += " SELECT {0} FROM tblstack2rcard ";
            sql += " WHERE stackcode = '" + stackCode.Trim().ToUpper() + "' ";
            sql += " AND ROWNUM <= 1 ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StackToRcard)));

            return this.DataProvider.CustomQuery(typeof(StackToRcard), new SQLCondition(sql));
        }


        public int GetStack2RCardCount(string stackCode, string exceptStorageCode)
        {
            string sql = "";
            sql += " SELECT COUNT(*) FROM tblstack2rcard ";
            sql += " WHERE stackcode = '" + stackCode.Trim().ToUpper() + "' ";

            if (exceptStorageCode.Trim().Length > 0)
            {
                sql += " AND storagecode <> '" + exceptStorageCode.Trim().ToUpper() + "' ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryStacktoRcardByRcardAndCarton(string rcard, string cartonCode)
        {
            string sql = "";
            sql += "SELECT * FROM tblstack2rcard ";
            sql += "WHERE 1 = 2 ";

            if (rcard.Trim() != string.Empty)
            {
                sql += "OR serialno = '" + rcard.Trim().ToUpper() + "' ";
            }

            if (cartonCode.Trim() != string.Empty)
            {
                sql += "OR serialno = '" + cartonCode.Trim().ToUpper() + "' ";
            }

            return this.DataProvider.CustomQuery(typeof(StackToRcard), new SQLCondition(sql));
        }

        public object[] GetStackToRcardByStack(string stackCode, string storageCode)
        {
            string sql = string.Empty;

            sql += "select * from tblstack2rcard WHERE 1=1 ";

            if (stackCode.Trim() != string.Empty)
            {
                sql += " AND stackcode = '" + stackCode.Trim().ToUpper() + "'";
            }

            if (storageCode.Trim() != string.Empty)
            {
                sql += "AND storagecode='" + storageCode.Trim().ToUpper() + "'";
            }

            return this.DataProvider.CustomQuery(typeof(StackToRcard), new SQLCondition(sql));
        }

        public object[] GetStackToRcardInfoByPallet(string palletCode)
        {
            string sql = string.Empty;
            sql += "SELECT DISTINCT tblstack2rcard.itemcode, tblstack2rcard.company";
            //sql += ",                tblstack2rcard.itemgrade";
            sql += "  FROM tblpallet2rcard, tblstack2rcard";
            sql += " WHERE tblpallet2rcard.rcard = tblstack2rcard.serialno";
            sql += "   AND tblpallet2rcard.palletcode = '" + palletCode + "'";

            return this.DataProvider.CustomQuery(typeof(StackToRcard), new SQLCondition(sql));
        }

        public object GetStackCodeFromStacktoRcard(string stackCode, string storageCode)
        {
            string sql = "";
            sql += " SELECT DISTINCT stackcode FROM tblstack2rcard ";
            sql += " WHERE stackcode='" + stackCode.Trim().ToUpper() + "' ";
            sql += " AND storagecode<>'" + storageCode.Trim().ToUpper() + "' ";

            object[] objectStacktoRcard = this.DataProvider.CustomQuery(typeof(StackToRcard), new SQLCondition(sql));
            if (objectStacktoRcard == null)
            {
                return null;
            }
            else
            {
                return objectStacktoRcard[0];
            }
        }

        public object GetStackMessage(string stackCode)
        {
            string sql = string.Empty;

            sql += " SELECT   tblstack2rcard.stackcode as stackcode, ";
            sql += " TO_CHAR (COUNT (DISTINCT palletcode)) || '/' ";
            sql += " || (SELECT tblstack.capacity FROM tblstack ";
            sql += "                                    WHERE tblstack.stackcode = tblstack2rcard.stackcode) as STACKQTYMESSAGE ";
            sql += " FROM tblstack2rcard, tblpallet2rcard ";
            sql += " WHERE tblstack2rcard.serialno = tblpallet2rcard.rcard ";
            sql += " And tblstack2rcard.stackcode='" + stackCode.ToUpper() + "'  GROUP BY tblstack2rcard.stackcode ";

            object[] objectStackMessage = this.DataProvider.CustomQuery(typeof(StackMessage), new SQLCondition(sql));
            if (objectStackMessage == null)
            {
                return null;
            }
            else
            {
                return objectStackMessage[0];
            }

        }

        public void UpdateCompany(string stackCode, string palletCode, string rcard, string companyCode, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            string sql = string.Empty;
            sql += "UPDATE tblstack2rcard SET company = '" + companyCode + "',";
            sql += "                           muser = '" + userCode + "', ";
            sql += "                           mdate = " + dbDateTime.DBDate + ",";
            sql += "                           mtime = " + dbDateTime.DBTime;
            sql += " WHERE 1=1 ";

            if (stackCode.Length > 0)
            {
                sql += " AND tblstack2rcard.stackcode = '" + stackCode + "'";
            }

            if (palletCode.Length > 0)
            {
                sql += " AND EXISTS (SELECT rcard FROM tblpallet2rcard where palletcode='" + palletCode + "' AND rcard = tblstack2rcard.serialno)";
            }

            if (rcard.Length > 0)
            {
                sql += " AND tblstack2rcard.serialno = '" + rcard + "'";
            }

            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }

        private void DeleteStackToRcard(string rcard)
        {
            string sql = string.Empty;
            sql += "DELETE FROM tblstack2rcard WHERE serialno = '" + rcard + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object[] GetRcardToStackPallet(string stackCode, string palletCode, string rcard)
        {
            string sql = string.Empty;
            sql += "SELECT company,stackcode,";
            sql += "       (SELECT palletcode FROM tblpallet2rcard WHERE rcard = tblstack2rcard.serialno ) palletcode,";
            sql += "       tblstack2rcard.serialno,tblstack2rcard.itemcode,tblmaterial.mdesc itemdesc,";
            sql += "       NVL(GETMOCODEBYRCARD(tblstack2rcard.serialno),' ') mocode ";
            sql += "  FROM tblmaterial,tblstack2rcard";
            sql += " WHERE tblstack2rcard.itemcode = tblmaterial.mcode";

            if (stackCode.Length > 0)
            {
                sql += "   AND tblstack2rcard.stackcode = '" + stackCode + "'";
            }

            if (palletCode.Length > 0)
            {
                sql += "   AND EXISTS (SELECT rcard FROM tblpallet2rcard where palletcode='" + palletCode + "' AND rcard = tblstack2rcard.serialno)";
            }

            if (rcard.Length > 0)
            {
                sql += "   AND tblstack2rcard.serialno = '" + rcard + "'";
            }

            return this.DataProvider.CustomQuery(typeof(RcardToStackPallet), new SQLCondition(sql));
        }

        public bool CheckStackIsOnlyAllowOneItem(string stackCode)
        {
            SStack stack = (SStack)this.GetSStack(stackCode);

            if (stack != null && stack.IsOneItem == "Y")
            {
                return true;
            }

            return false;
        }

        public void SaveInInventory(string storageCode, string stackCode, string companyCode, string userCode, string productLevel, string businessCode, DataTable sourceList)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (DataRow row in sourceList.Rows)
                {
                    ////Save In Transaction info
                    InvInTransaction invInTransaction = new InvInTransaction();
                    invInTransaction.TransCode = "";
                    invInTransaction.Rcard = row["rcard"].ToString();
                    invInTransaction.CartonCode = row["cartoncode"].ToString();
                    invInTransaction.PalletCode = row["palletcode"].ToString();
                    invInTransaction.ItemCode = row["itemcode"].ToString();
                    invInTransaction.MOCode = row["mocode"].ToString();
                    invInTransaction.BusinessCode = businessCode;
                    invInTransaction.StackCode = stackCode;
                    invInTransaction.StorageCode = storageCode;

                    invInTransaction.Company = companyCode;
                    invInTransaction.BusinessReason = BussinessReason.type_produce;
                    invInTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    invInTransaction.SSCode = row["sscode"].ToString();
                    invInTransaction.Serial = 0;
                    invInTransaction.MaintainUser = userCode;
                    invInTransaction.MaintainDate = dbDateTime.DBDate;
                    invInTransaction.MaintainTime = dbDateTime.DBTime;

                    this.AddInvInTransaction(invInTransaction);

                    ////Save Stack to Rcard info
                    //
                    StackToRcard stackToRcard = new StackToRcard();
                    stackToRcard.StorageCode = storageCode;
                    stackToRcard.StackCode = stackCode;
                    stackToRcard.Cartoncode = row["cartoncode"].ToString();
                    stackToRcard.SerialNo = row["rcard"].ToString();
                    stackToRcard.ItemCode = row["itemcode"].ToString();
                    stackToRcard.BusinessReason = BussinessReason.type_produce.ToString();
                    stackToRcard.Company = companyCode;

                    stackToRcard.InUser = userCode;
                    stackToRcard.InDate = dbDateTime.DBDate;
                    stackToRcard.InTime = dbDateTime.DBTime;
                    stackToRcard.MaintainUser = userCode;
                    stackToRcard.MaintainDate = dbDateTime.DBDate;
                    stackToRcard.MaintainTime = dbDateTime.DBTime;

                    InvInTransaction GetinvInTransaction = (InvInTransaction)this.GetInvInTransaction(invInTransaction.Rcard, invInTransaction.CartonCode,
                                                                                 invInTransaction.PalletCode, invInTransaction.MOCode,
                                                                                 invInTransaction.StorageCode, invInTransaction.StackCode, invInTransaction.MaintainUser);
                    if (GetinvInTransaction != null)
                    {
                        stackToRcard.TransInSerial = GetinvInTransaction.Serial;
                    }
                    else
                    {
                        stackToRcard.TransInSerial = 0;
                    }

                    this.AddStackToRcard(stackToRcard);


                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }

        //Jarvis 去除产品档次
        public void SaveInInventory(string storageCode, string stackCode, string companyCode, string userCode, string businessCode, DataTable sourceList)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (DataRow row in sourceList.Rows)
                {
                    ////Save In Transaction info
                    InvInTransaction invInTransaction = new InvInTransaction();
                    invInTransaction.TransCode = "";
                    invInTransaction.Rcard = row["rcard"].ToString();
                    invInTransaction.CartonCode = row["cartoncode"].ToString();
                    invInTransaction.PalletCode = row["palletcode"].ToString();
                    invInTransaction.ItemCode = row["itemcode"].ToString();
                    invInTransaction.MOCode = row["mocode"].ToString();
                    invInTransaction.BusinessCode = businessCode;
                    invInTransaction.StackCode = stackCode;
                    invInTransaction.StorageCode = storageCode;
                    invInTransaction.Company = companyCode;
                    invInTransaction.BusinessReason = BussinessReason.type_produce;
                    invInTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    invInTransaction.SSCode = row["sscode"].ToString();
                    invInTransaction.Serial = 0;
                    invInTransaction.MaintainUser = userCode;
                    invInTransaction.MaintainDate = dbDateTime.DBDate;
                    invInTransaction.MaintainTime = dbDateTime.DBTime;

                    this.AddInvInTransaction(invInTransaction);

                    ////Save Stack to Rcard info
                    //
                    StackToRcard stackToRcard = new StackToRcard();
                    stackToRcard.StorageCode = storageCode;
                    stackToRcard.StackCode = stackCode;
                    stackToRcard.Cartoncode = row["cartoncode"].ToString();
                    stackToRcard.SerialNo = row["rcard"].ToString();
                    stackToRcard.ItemCode = row["itemcode"].ToString();
                    stackToRcard.BusinessReason = BussinessReason.type_produce.ToString();
                    stackToRcard.Company = companyCode;
                    stackToRcard.InUser = userCode;
                    stackToRcard.InDate = dbDateTime.DBDate;
                    stackToRcard.InTime = dbDateTime.DBTime;
                    stackToRcard.MaintainUser = userCode;
                    stackToRcard.MaintainDate = dbDateTime.DBDate;
                    stackToRcard.MaintainTime = dbDateTime.DBTime;

                    InvInTransaction GetinvInTransaction = (InvInTransaction)this.GetInvInTransaction(invInTransaction.Rcard, invInTransaction.CartonCode,
                                                                                 invInTransaction.PalletCode, invInTransaction.MOCode,
                                                                                 invInTransaction.StorageCode, invInTransaction.StackCode, invInTransaction.MaintainUser);
                    if (GetinvInTransaction != null)
                    {
                        stackToRcard.TransInSerial = GetinvInTransaction.Serial;
                    }
                    else
                    {
                        stackToRcard.TransInSerial = 0;
                    }

                    this.AddStackToRcard(stackToRcard);


                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }

        public void SaveInInventoryByNonProduce(string storageCode, string stackCode, string companyCode, string deliverUser, string userCode, string productLevel, string transCode, string itemCode, string businessCode, string memo, string relationDoc, string resCode, DataTable sourceList)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string sql = string.Empty;

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (DataRow row in sourceList.Rows)
                {

                    ////Save In Transaction info
                    InvInTransaction invInTransaction = new InvInTransaction();
                    invInTransaction.TransCode = transCode;
                    invInTransaction.Rcard = row["rcard"].ToString();
                    invInTransaction.CartonCode = row["rcard"].ToString();
                    invInTransaction.PalletCode = row["palletcode"].ToString();
                    invInTransaction.ItemCode = itemCode;
                    invInTransaction.MOCode = "";
                    invInTransaction.BusinessCode = businessCode;
                    invInTransaction.StackCode = stackCode;
                    invInTransaction.StorageCode = storageCode;

                    invInTransaction.SSCode = "";
                    invInTransaction.Company = companyCode;
                    invInTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    invInTransaction.BusinessReason = BussinessReason.type_noneproduce;
                    invInTransaction.DeliverUser = deliverUser;
                    invInTransaction.Memo = memo;
                    invInTransaction.Serial = 0;
                    invInTransaction.MaintainUser = userCode;
                    invInTransaction.MaintainDate = dbDateTime.DBDate;
                    invInTransaction.MaintainTime = dbDateTime.DBTime;
                    invInTransaction.RelatedDocument = relationDoc;

                    this.AddInvInTransaction(invInTransaction);


                    ////Save Stack to Rcard info
                    //
                    StackToRcard stackToRcard = new StackToRcard();
                    stackToRcard.StorageCode = storageCode;
                    stackToRcard.StackCode = stackCode;
                    stackToRcard.SerialNo = row["rcard"].ToString();
                    stackToRcard.ItemCode = itemCode;
                    stackToRcard.BusinessReason = BussinessReason.type_noneproduce.ToString();
                    stackToRcard.Company = companyCode;

                    stackToRcard.InUser = userCode;
                    stackToRcard.InDate = dbDateTime.DBDate;
                    stackToRcard.InTime = dbDateTime.DBTime;
                    stackToRcard.MaintainUser = userCode;
                    stackToRcard.MaintainDate = dbDateTime.DBDate;
                    stackToRcard.MaintainTime = dbDateTime.DBTime;
                    stackToRcard.Cartoncode = " ";

                    InvInTransaction GetinvInTransaction = (InvInTransaction)this.GetInvInTransaction(invInTransaction.Rcard, invInTransaction.CartonCode,
                                                                               invInTransaction.PalletCode, invInTransaction.MOCode,
                                                                               invInTransaction.StorageCode, invInTransaction.StackCode, invInTransaction.MaintainUser);
                    if (GetinvInTransaction != null)
                    {
                        stackToRcard.TransInSerial = GetinvInTransaction.Serial;
                    }
                    else
                    {
                        stackToRcard.TransInSerial = 0;
                    }

                    this.AddStackToRcard(stackToRcard);

                    //获取当前rcard的最后个批号
                    string lotNo = " ";
                    string oqcsql = "SELECT * FROM tbllot2card WHERE rcard = '" + row["rcard"].ToString().Trim().ToUpper() + "' ORDER BY mdate DESC,mtime DESC ";
                    //oqcsql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)), row["rcard"].ToString());
                    object[] lot2RcardList = this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(oqcsql));
                    if (lot2RcardList != null)
                    {
                        lotNo = ((OQCLot2Card)lot2RcardList[0]).LOTNO.Trim().ToUpper();
                    }

                    ////如果此Pallet为新增的Pallet，则需要创建此Pallet，并维护Pallet和Rcard的关系
                    //
                    if (row["palletcode"].ToString().Trim().Length != 0)
                    {
                        if (this.GetPalletByPalletCode(row["palletcode"].ToString().Trim()) == null)
                        {
                            ////新增此Pallet信息
                            //
                            sql = string.Empty;
                            sql = sql + "INSERT INTO tblpallet ";
                            sql = sql + "(PALLETCODE,RCARDCOUNT,CAPACITY,MOCODE,SSCODE,ITEMCODE,MUSER,MDATE,MTIME,EATTRIBUTE1,RESCODE,ORGID) ";
                            sql = sql + "VALUES ";
                            sql = sql + "( '" + row["palletcode"].ToString().Trim() + "',";
                            sql = sql + "0,9999,' ',' ','" + itemCode + "','" + userCode + "'," + dbDateTime.DBDate + "," + dbDateTime.DBTime + ",'" + lotNo + "','" + resCode + "', ";
                            sql = sql + GlobalVariables.CurrentOrganizations.First().OrganizationID + ")";

                            this.DataProvider.CustomExecute(new SQLCondition(sql));

                        }

                        if (GetPallet2RcardByPalletAndRcard(row["palletcode"].ToString().Trim(), row["rcard"].ToString().Trim()) == null)
                        {
                            ////新增此Pallet和Rcard关系
                            //
                            sql = string.Empty;
                            sql = sql + "INSERT INTO tblpallet2rcard ";
                            sql = sql + "(PALLETCODE,RCARD,MUSER,MDATE,MTIME,EATTRIBUTE1,MOCODE)";
                            sql = sql + "VALUES ";
                            sql = sql + "( '" + row["palletcode"].ToString().Trim() + "','" + row["rcard"].ToString().Trim() + "',";
                            sql = sql + "'" + userCode + "'," + dbDateTime.DBDate + "," + dbDateTime.DBTime + ",' ',' ')";

                            this.DataProvider.CustomExecute(new SQLCondition(sql));

                            ////更新Rcard数量
                            //
                            sql = string.Empty;
                            sql = sql + " UPDATE tblpallet SET rcardcount=rcardcount+1 WHERE palletcode = '" + row["palletcode"].ToString().Trim() + "'";

                            this.DataProvider.CustomExecute(new SQLCondition(sql));
                        }

                    }


                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }

        //Jarvis 去除产品档次
        public void SaveInInventoryByNonProduce(string storageCode, string stackCode, string companyCode, string deliverUser, string userCode, string transCode, string itemCode, string businessCode, string memo, string relationDoc, string resCode, DataTable sourceList)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
            string sql = string.Empty;

            try
            {
                this.DataProvider.BeginTransaction();
                foreach (DataRow row in sourceList.Rows)
                {

                    ////Save In Transaction info
                    InvInTransaction invInTransaction = new InvInTransaction();
                    invInTransaction.TransCode = transCode;
                    invInTransaction.Rcard = row["rcard"].ToString();
                    invInTransaction.CartonCode = row["rcard"].ToString();
                    invInTransaction.PalletCode = row["palletcode"].ToString();
                    invInTransaction.ItemCode = itemCode;
                    invInTransaction.MOCode = "";
                    invInTransaction.BusinessCode = businessCode;
                    invInTransaction.StackCode = stackCode;
                    invInTransaction.StorageCode = storageCode;
                    invInTransaction.SSCode = "";
                    invInTransaction.Company = companyCode;
                    invInTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                    invInTransaction.BusinessReason = BussinessReason.type_noneproduce;
                    invInTransaction.DeliverUser = deliverUser;
                    invInTransaction.Memo = memo;
                    invInTransaction.Serial = 0;
                    invInTransaction.MaintainUser = userCode;
                    invInTransaction.MaintainDate = dbDateTime.DBDate;
                    invInTransaction.MaintainTime = dbDateTime.DBTime;
                    invInTransaction.RelatedDocument = relationDoc;

                    this.AddInvInTransaction(invInTransaction);


                    ////Save Stack to Rcard info
                    //
                    StackToRcard stackToRcard = new StackToRcard();
                    stackToRcard.StorageCode = storageCode;
                    stackToRcard.StackCode = stackCode;
                    stackToRcard.SerialNo = row["rcard"].ToString();
                    stackToRcard.ItemCode = itemCode;
                    stackToRcard.BusinessReason = BussinessReason.type_noneproduce.ToString();
                    stackToRcard.Company = companyCode;
                    stackToRcard.InUser = userCode;
                    stackToRcard.InDate = dbDateTime.DBDate;
                    stackToRcard.InTime = dbDateTime.DBTime;
                    stackToRcard.MaintainUser = userCode;
                    stackToRcard.MaintainDate = dbDateTime.DBDate;
                    stackToRcard.MaintainTime = dbDateTime.DBTime;
                    stackToRcard.Cartoncode = " ";

                    InvInTransaction GetinvInTransaction = (InvInTransaction)this.GetInvInTransaction(invInTransaction.Rcard, invInTransaction.CartonCode,
                                                                               invInTransaction.PalletCode, invInTransaction.MOCode,
                                                                               invInTransaction.StorageCode, invInTransaction.StackCode, invInTransaction.MaintainUser);
                    if (GetinvInTransaction != null)
                    {
                        stackToRcard.TransInSerial = GetinvInTransaction.Serial;
                    }
                    else
                    {
                        stackToRcard.TransInSerial = 0;
                    }

                    this.AddStackToRcard(stackToRcard);

                    //获取当前rcard的最后个批号
                    string lotNo = " ";
                    string oqcsql = "SELECT * FROM tbllot2card WHERE rcard = '" + row["rcard"].ToString().Trim().ToUpper() + "' ORDER BY mdate DESC,mtime DESC ";
                    //oqcsql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(OQCLot2Card)), row["rcard"].ToString());
                    object[] lot2RcardList = this.DataProvider.CustomQuery(typeof(OQCLot2Card), new SQLCondition(oqcsql));
                    if (lot2RcardList != null)
                    {
                        lotNo = ((OQCLot2Card)lot2RcardList[0]).LOTNO.Trim().ToUpper();
                    }

                    ////如果此Pallet为新增的Pallet，则需要创建此Pallet，并维护Pallet和Rcard的关系
                    //
                    if (row["palletcode"].ToString().Trim().Length != 0)
                    {
                        if (this.GetPalletByPalletCode(row["palletcode"].ToString().Trim()) == null)
                        {
                            ////新增此Pallet信息
                            //
                            sql = string.Empty;
                            sql = sql + "INSERT INTO tblpallet ";
                            sql = sql + "(PALLETCODE,RCARDCOUNT,CAPACITY,MOCODE,SSCODE,ITEMCODE,MUSER,MDATE,MTIME,EATTRIBUTE1,RESCODE,ORGID) ";
                            sql = sql + "VALUES ";
                            sql = sql + "( '" + row["palletcode"].ToString().Trim() + "',";
                            sql = sql + "0,9999,' ',' ','" + itemCode + "','" + userCode + "'," + dbDateTime.DBDate + "," + dbDateTime.DBTime + ",'" + lotNo + "','" + resCode + "', ";
                            sql = sql + GlobalVariables.CurrentOrganizations.First().OrganizationID + ")";

                            this.DataProvider.CustomExecute(new SQLCondition(sql));

                        }

                        if (GetPallet2RcardByPalletAndRcard(row["palletcode"].ToString().Trim(), row["rcard"].ToString().Trim()) == null)
                        {
                            ////新增此Pallet和Rcard关系
                            //
                            sql = string.Empty;
                            sql = sql + "INSERT INTO tblpallet2rcard ";
                            sql = sql + "(PALLETCODE,RCARD,MUSER,MDATE,MTIME,EATTRIBUTE1,MOCODE)";
                            sql = sql + "VALUES ";
                            sql = sql + "( '" + row["palletcode"].ToString().Trim() + "','" + row["rcard"].ToString().Trim() + "',";
                            sql = sql + "'" + userCode + "'," + dbDateTime.DBDate + "," + dbDateTime.DBTime + ",' ',' ')";

                            this.DataProvider.CustomExecute(new SQLCondition(sql));

                            ////更新Rcard数量
                            //
                            sql = string.Empty;
                            sql = sql + " UPDATE tblpallet SET rcardcount=rcardcount+1 WHERE palletcode = '" + row["palletcode"].ToString().Trim() + "'";

                            this.DataProvider.CustomExecute(new SQLCondition(sql));
                        }

                    }


                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }

        }

        public void UpdateStackToRcard(string toStorageCode, string toStackCode, string oldStorageCode, string oldStackCode, string userCode)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();

                string sql = string.Empty;

                sql += "Update tblstack2rcard set StorageCode='" + toStorageCode + "',StackCode='" + toStackCode + "', ";
                sql += "                           muser = '" + userCode + "', ";
                sql += "                           mdate = " + dbDateTime.DBDate + ",";
                sql += "                           mtime = " + dbDateTime.DBTime + "";
                sql += "  where StorageCode='" + oldStorageCode + "' AND StackCode='" + oldStackCode + "'";

                this.DataProvider.CustomExecute(new SQLCondition(sql));

                this.DataProvider.CommitTransaction();
                return;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdatePalletStackByWholePallet(object[] rcardToStackPalletList, string tStorageCode, string tStackCode, string userCode)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();
                foreach (RcardToStackPallet obj in rcardToStackPalletList)
                {
                    this.UpdateStackToRcardByRcard(tStorageCode, tStackCode, obj.SerialNo, userCode, dbDateTime);
                }


                this.DataProvider.CommitTransaction();
                return;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateOriPalletStackToTargetPallet(object[] rcardToStackPalletList, string tPalletCode, string tStorageCode, string tStackCode, string userCode)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();
                foreach (RcardToStackPallet obj in rcardToStackPalletList)
                {
                    ////把源Pallet和Rcard的关系变更为目标Pallet和Rcard的关系
                    //
                    this.UpdatePalletRcardInfo(obj.SerialNo, tPalletCode, obj.PalletCode, userCode, dbDateTime, obj.MOCode);

                    ////根据Rcard更新StackToRcard
                    //
                    this.UpdateStackToRcardByRcard(tStorageCode, tStackCode, obj.SerialNo, userCode, dbDateTime);
                }


                this.DataProvider.CommitTransaction();
                return;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateOriPalletStackToNewPallet(object[] rcardToStackPalletList, string tPalletCode, string tStorageCode, string tStackCode, string userCode, string resCode, string lotNo)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();

                ////新增Pallet
                //
                this.AddPallet(tPalletCode, ((RcardToStackPallet)rcardToStackPalletList[0]).ItemCode, userCode, resCode, lotNo, dbDateTime);

                foreach (RcardToStackPallet obj in rcardToStackPalletList)
                {
                    ////把源Pallet和Rcard的关系变更为目标Pallet和Rcard的关系
                    //
                    this.UpdatePalletRcardInfo(obj.SerialNo, tPalletCode, obj.PalletCode, userCode, dbDateTime, obj.MOCode);

                    ////根据Rcard更新StackToRcard
                    //
                    this.UpdateStackToRcardByRcard(tStorageCode, tStackCode, obj.SerialNo, userCode, dbDateTime);
                }


                this.DataProvider.CommitTransaction();
                return;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateOriPalletStackToTargetPalletByRcard(object rcardToStackPallet, string tPalletCode, string tStorageCode, string tStackCode, string userCode)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();

                //RcardToStackPallet rcardToStack

                ////把源Pallet和Rcard的关系变更为目标Pallet和Rcard的关系
                //
                this.UpdatePalletRcardInfo(((RcardToStackPallet)rcardToStackPallet).SerialNo, tPalletCode, ((RcardToStackPallet)rcardToStackPallet).PalletCode, userCode, dbDateTime, ((RcardToStackPallet)rcardToStackPallet).MOCode);

                ////根据Rcard更新StackToRcard
                //
                this.UpdateStackToRcardByRcard(tStorageCode, tStackCode, ((RcardToStackPallet)rcardToStackPallet).SerialNo, userCode, dbDateTime);

                this.DataProvider.CommitTransaction();
                return;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateOriPalletStackToNewPalletByRcard(object rcardToStackPallet, string tPalletCode, string tStorageCode, string tStackCode, string userCode, string resCode, string lotNo)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();

                ////新增Pallet
                //
                this.AddPallet(tPalletCode, ((RcardToStackPallet)rcardToStackPallet).ItemCode, userCode, resCode, lotNo, dbDateTime);

                ////把源Pallet和Rcard的关系变更为目标Pallet和Rcard的关系
                //
                this.UpdatePalletRcardInfo(((RcardToStackPallet)rcardToStackPallet).SerialNo, tPalletCode, ((RcardToStackPallet)rcardToStackPallet).PalletCode, userCode, dbDateTime, ((RcardToStackPallet)rcardToStackPallet).MOCode);

                ////根据Rcard更新StackToRcard
                //
                this.UpdateStackToRcardByRcard(tStorageCode, tStackCode, ((RcardToStackPallet)rcardToStackPallet).SerialNo, userCode, dbDateTime);

                this.DataProvider.CommitTransaction();
                return;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        private void AddPallet(string palletCode, string itemCode, string userCode, string resCode, string lotNo, DBDateTime dbDateTime)
        {
            ////新增此Pallet信息
            //
            string sql = string.Empty;
            sql = sql + "SELECT COUNT(*) FROM tblpallet WHERE palletcode = '" + palletCode + "'";

            if (lotNo == string.Empty)
            {
                lotNo = " ";
            }

            if (this.DataProvider.GetCount(new SQLCondition(sql)) == 0)
            {

                sql = string.Empty;
                sql = sql + "INSERT INTO tblpallet ";
                sql = sql + "(PALLETCODE,RCARDCOUNT,CAPACITY,MOCODE,SSCODE,ITEMCODE,MUSER,MDATE,MTIME,EATTRIBUTE1,RESCODE,ORGID) ";
                sql = sql + "VALUES ";
                sql = sql + "( '" + palletCode + "',";
                sql = sql + "0,9999,' ',' ','" + itemCode + "','" + userCode + "'," + dbDateTime.DBDate + "," + dbDateTime.DBTime + ",'" + lotNo + "','" + resCode + "', ";
                sql = sql + GlobalVariables.CurrentOrganizations.First().OrganizationID + ")";
                throw new Exception(sql);
                this.DataProvider.CustomExecute(new SQLCondition(sql));
            }
        }

        private void AddPallet2RcardLog(string palletCode, string rcard, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string sql = "INSERT INTO TBLPALLET2RCARDLog ";
            sql += "(serial,PALLETCODE,RCARD,PACKUSER,PACKDATE,PACKTIME,REMOVEUSER,REMOVDATE,REMOVTIME) ";
            sql += "VALUES ";
            sql += "('0','" + palletCode + "','" + rcard + "','" + userCode + "'," + dbDateTime.DBDate + "," + dbDateTime.DBTime + ",'','0','0')";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        private void SaveRemovePallet2RcardLog(string palletCode, string rcard, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            string sql = " SELECT * FROM TBLPALLET2RCARDLog WHERE 1=1 ";
            sql += " AND palletCode='" + palletCode.Trim().ToUpper() + "' ";
            sql += " AND rcard='" + rcard.Trim().ToUpper() + "' ";
            sql += " ORDER BY packdate DESC,packtime DESC";

            object[] pallet2RcardLogList = this.DataProvider.CustomQuery(typeof(Pallet2RcardLog), new SQLCondition(sql));

            if (pallet2RcardLogList != null && string.IsNullOrEmpty(((Pallet2RcardLog)pallet2RcardLogList[0]).RemoveUser))
            {
                string sqlUpdate = string.Empty;
                sqlUpdate += "UPDATE TBLPALLET2RCARDLog SET  REMOVEUSER = '" + userCode + "', ";
                sqlUpdate += "                           REMOVDATE = " + dbDateTime.DBDate + ",";
                sqlUpdate += "                           REMOVTIME = " + dbDateTime.DBTime;
                sqlUpdate += " WHERE serial = '" + ((Pallet2RcardLog)pallet2RcardLogList[0]).Serial + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(sqlUpdate));
            }
            else
            {
                string sqlInsert = "INSERT INTO TBLPALLET2RCARDLog ";
                sqlInsert += "(serial,PALLETCODE,RCARD,PACKUSER,PACKDATE,PACKTIME,REMOVEUSER,REMOVDATE,REMOVTIME) ";
                sqlInsert += "VALUES ";
                sqlInsert += "('0','" + palletCode + "','" + rcard + "','','0','0','" + userCode + "'," + dbDateTime.DBDate + "," + dbDateTime.DBTime + ")";
                this.DataProvider.CustomExecute(new SQLCondition(sqlInsert));
            }
        }
        /// <summary>
        /// 把源Pallet和Rcard的关系变更为目标Pallet和Rcard的关系
        /// </summary>
        /// <param name="rcard">产品序列号</param>
        /// <param name="tPallet">目标栈板</param>
        /// <param name="oPallet">原栈板</param>
        /// <param name="userCode">用户名</param>
        /// <param name="dtNow">当前时间</param>
        private void UpdatePalletRcardInfo(string rcard, string tPallet, string oPallet, string userCode, DBDateTime dtNow, string moCode)
        {
            string sql = string.Empty;

            if (oPallet.Trim().Length != 0)
            {
                ////减少源Pallet的Rcard Count
                //
                sql += "UPDATE tblpallet SET rcardcount = rcardcount - 1,";
                sql += "                           muser = '" + userCode + "', ";
                sql += "                           mdate = " + dtNow.DBDate + ",";
                sql += "                           mtime = " + dtNow.DBTime;
                sql += " WHERE palletcode = '" + oPallet + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(sql));

                //记录log
                this.SaveRemovePallet2RcardLog(oPallet, rcard, userCode);


                ////更新源Pallet和Rcard的对应关系为目标Pallet的对应关系
                //
                sql = string.Empty;
                sql += "UPDATE tblpallet2rcard SET palletcode = '" + tPallet + "',";
                sql += "                           muser = '" + userCode + "', ";
                sql += "                           mdate = " + dtNow.DBDate + ",";
                sql += "                           mtime = " + dtNow.DBTime;
                sql += " WHERE palletcode = '" + oPallet + "' ";
                sql += "   AND rcard = '" + rcard + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(sql));

                //记录log
                this.AddPallet2RcardLog(tPallet, rcard, userCode);
            }
            else
            {
                ////新增新Pallet和Rcard的对应关系
                //
                sql = string.Empty;
                sql += "INSERT INTO TBLpallet2rcard ";
                sql += "(PALLETCODE,RCARD,MUSER,MDATE,MTIME,EATTRIBUTE1,MOCODE) ";
                sql += "VALUES ";
                sql += "('" + tPallet + "','" + rcard + "','" + userCode + "'," + dtNow.DBDate + "," + dtNow.DBTime + ",' ','" + moCode + "')";
                this.DataProvider.CustomExecute(new SQLCondition(sql));

                //记录log
                this.AddPallet2RcardLog(tPallet, rcard, userCode);
            }

            ////增加目标Pallet的Rcard Count
            //
            sql = string.Empty;
            sql += "UPDATE tblpallet SET rcardcount = rcardcount + 1,";
            sql += "                           muser = '" + userCode + "', ";
            sql += "                           mdate = " + dtNow.DBDate + ",";
            sql += "                           mtime = " + dtNow.DBTime;
            sql += " WHERE palletcode = '" + tPallet + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            ////如果源栈板的RcardCount=0，则删除此栈板
            //
            sql = string.Empty;
            sql += "DELETE FROM tblpallet ";
            sql += " WHERE rcardcount = 0 ";
            sql += "   AND palletcode = '" + oPallet + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }


        /// <summary>
        /// 根据Rcard更新StackToRcard
        /// </summary>
        /// <param name="tStorageCode">目的库位</param>
        /// <param name="tStackCode">目的垛位</param>
        /// <param name="rcard">产品序列号</param>
        /// <param name="userCode">用户名</param>
        /// <param name="dtNow">当前时间</param>
        private void UpdateStackToRcardByRcard(string tStorageCode, string tStackCode, string rcard, string userCode, DBDateTime dtNow)
        {
            string sql = string.Empty;
            sql += "UPDATE tblstack2rcard SET storagecode = '" + tStorageCode + "',";
            sql += "                           stackcode = '" + tStackCode + "', ";
            sql += "                           muser = '" + userCode + "', ";
            sql += "                           mdate = " + dtNow.DBDate + ",";
            sql += "                           mtime = " + dtNow.DBTime;
            sql += " WHERE serialno = '" + rcard + "' ";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        private object GetPalletByPalletCode(string palletCode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Pallet), new SQLCondition(
                String.Format("select {0} from tblpallet where palletcode='{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pallet))
                , palletCode)));

            if (objs == null)
            {
                return null;
            }
            else if (objs.Length > 0)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        private object GetPallet2RcardByPalletAndRcard(string palletCode, string rcard)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Pallet2RCard), new SQLCondition(
                String.Format("select {0} from tblPallet2RCard where palletcode='{1}' and rcard='{2}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pallet2RCard))
                , palletCode, rcard)));

            if (objs == null)
            {
                return null;
            }
            else if (objs.Length > 0)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        public DataTable GetStackPalletInfoHead(string storageCode)
        {
            string sql = string.Empty;
            sql = sql + "SELECT 'false' checked, stackcode,capacity maxcapacity,palletcount,(capacity-palletcount) remaincount,round(palletcount/capacity,2)*100 percent ";
            sql = sql + "FROM (";
            sql = sql + "SELECT tblstack.stackcode, capacity,COUNT(DISTINCT tblpallet2rcard.palletcode) palletcount";
            sql = sql + "  FROM tblstack2rcard,tblstack,tblpallet2rcard";
            sql = sql + " WHERE tblstack2rcard.stackcode = tblstack.stackcode";
            sql = sql + "   AND tblpallet2rcard.rcard(+) = tblstack2rcard.serialno";
            sql = sql + "   AND tblstack2rcard.storagecode = '" + storageCode + "'";
            sql = sql + " GROUP BY tblstack.stackcode, capacity)";
            sql = sql + " UNION ";
            sql = sql + "SELECT 'false' checked, stackcode, capacity, 0 palletcount,capacity remaincount,0 percent ";
            sql = sql + "  FROM tblstack ";
            sql = sql + " WHERE NOT EXISTS (SELECT stackcode FROM tblstack2rcard WHERE tblstack2rcard.stackcode = tblstack.stackcode ) ";
            sql = sql + "   AND tblstack.storagecode = '" + storageCode + "'";

            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);

            return ds.Tables[0];
        }

        public bool CheckStackCapacity(string storageCode, string stackCode)
        {
            string sql = string.Empty;

            sql = sql + "SELECT tblstack.stackcode, capacity,COUNT(DISTINCT tblpallet2rcard.palletcode) palletcount";
            sql = sql + "  FROM tblstack2rcard,tblstack,tblpallet2rcard";
            sql = sql + " WHERE tblstack2rcard.stackcode = tblstack.stackcode";
            sql = sql + "   AND tblpallet2rcard.rcard(+) = tblstack2rcard.serialno";
            sql = sql + "   AND tblstack2rcard.storagecode = '" + storageCode + "'";
            sql = sql + "   AND tblstack2rcard.stackcode = '" + stackCode + "'";
            sql = sql + " GROUP BY tblstack.stackcode, capacity";

            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["capacity"]) <= Convert.ToInt32(ds.Tables[0].Rows[0]["palletcount"]))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CheckStackCapacity(string toStorageCode, string toStackCode, string oldStackCode)
        {
            string sql = string.Empty;

            sql = sql + "SELECT tblstack.stackcode, capacity,COUNT(DISTINCT tblpallet2rcard.palletcode) palletcount";
            sql = sql + "  FROM tblstack2rcard,tblstack,tblpallet2rcard";
            sql = sql + " WHERE tblstack2rcard.stackcode = tblstack.stackcode";
            sql = sql + "   AND tblpallet2rcard.rcard(+) = tblstack2rcard.serialno";
            sql = sql + "   AND tblstack2rcard.storagecode = '" + toStorageCode + "'";
            sql = sql + "   AND tblstack2rcard.stackcode = '" + toStackCode + "'";
            sql = sql + " GROUP BY tblstack.stackcode, capacity";

            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);

            int toCapacity = 0;
            int toPalletNumber = 0;

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
            {
                toCapacity = Convert.ToInt32(ds.Tables[0].Rows[0]["capacity"]);
                toPalletNumber = Convert.ToInt32(ds.Tables[0].Rows[0]["palletcount"]);
                if (toCapacity <= toPalletNumber)
                {
                    return false;
                }
            }
            else
            {
                SStack toStack = (SStack)this.GetSStack(toStackCode.Trim().ToUpper());
                toCapacity = toStack.Capacity;
                toPalletNumber = 0;
            }

            string oldSQL = string.Empty;
            oldSQL += " SELECT tblstack2rcard.stackcode,COUNT(DISTINCT tblpallet2rcard.palletcode) AS palletcount ";
            oldSQL += " FROM tblstack2rcard, tblpallet2rcard WHERE tblpallet2rcard.rcard(+) = tblstack2rcard.serialno ";
            oldSQL += " AND tblstack2rcard.stackcode = '" + oldStackCode + "'     GROUP BY tblstack2rcard.stackcode ";

            DataSet oldDS = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(oldSQL);

            if (oldDS.Tables[0] != null && oldDS.Tables[0].Rows.Count != 0)
            {
                int oldPalletNumber = Convert.ToInt32(oldDS.Tables[0].Rows[0]["palletcount"]);

                if (toCapacity < toPalletNumber + oldPalletNumber)
                {
                    return false;
                }
            }

            return true;

        }

        public DataTable GetStackPalleteInfoDetail(string storageCode)
        {
            string sql = string.Empty;
            sql = sql + "SELECT stackcode,itemcode, mdesc itemdesc, palletcode, COUNT(tblstack2rcard.serialno) qtyinfo";
            sql = sql + "    FROM tblstack2rcard, tblmaterial, tblpallet2rcard";
            sql = sql + "   WHERE tblstack2rcard.itemcode = tblmaterial.mcode";
            sql = sql + "     AND tblstack2rcard.serialno = tblpallet2rcard.rcard(+)";
            sql = sql + "     AND storagecode = '" + storageCode + "'";
            sql = sql + "   GROUP BY stackcode, itemcode,mdesc, palletcode";

            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);

            return ds.Tables[0];
        }


        //public DataTable GetStackPalletInfoByDN(string dnNo)
        //{
        //    string sql = string.Empty;
        //    sql = sql + "SELECT DISTINCT tblstack2rcard.stackcode,tblpallet2rcard.palletcode,tblpallet2rcard.rcard";
        //    sql = sql + "    FROM tblstack2rcard, tblpallet2rcard, tbldn";
        //    sql = sql + "   WHERE tblstack2rcard.itemcode = tblmaterial.mcode";
        //    sql = sql + "     AND tblstack2rcard.serialno = tblpallet2rcard.rcard";
        //    sql = sql + "     AND tblstack2rcard.storagecode = tbldn.frmstorage ";
        //    sql = sql + "     AND tbldn.dnno = '" + dnNo + "'";
        //}

        public object[] GetStackPalletInfoByDNFileRcard(string rcard)
        {
            string sql = string.Empty;
            sql += "SELECT tblstack2rcard.itemcode, tblitem.itemdesc,";
            sql += "       tblstack2rcard.stackcode, tblstack2rcard.storagecode,";
            sql += "       tblpallet2rcard.palletcode,tblstack2rcard.serialno,tblstack2rcard.company,''cartoncode, ";
            sql += "       NVL(GETMOCODEBYRCARD(tblstack2rcard.serialno),' ') mocode ";
            sql += "  FROM tblstack2rcard, tblpallet2rcard, tblitem ";
            sql += " WHERE tblstack2rcard.serialno = tblpallet2rcard.rcard(+)";
            sql += "   AND tblstack2rcard.itemcode = tblitem.itemcode(+)";
            sql += "   AND tblstack2rcard.serialno = '" + rcard + "'";
            sql += " UNION ";
            sql += "SELECT tblstack2rcard.itemcode, tblitem.itemdesc,";
            sql += "       tblstack2rcard.stackcode, tblstack2rcard.storagecode,";
            sql += "       tblpallet2rcard.palletcode,tblstack2rcard.serialno,tblstack2rcard.company ,'" + rcard + "'cartoncode,";
            sql += "       NVL(GETMOCODEBYRCARD(tblstack2rcard.serialno),' ') mocode ";
            sql += "  FROM tblstack2rcard, tblpallet2rcard, tblitem ";
            sql += " WHERE tblstack2rcard.serialno = tblpallet2rcard.rcard(+)";
            sql += "   AND tblstack2rcard.itemcode = tblitem.itemcode(+)";
            sql += "   AND tblstack2rcard.serialno IN (";
            sql += "               SELECT rcard";
            sql += "                 FROM (SELECT   rcard";
            sql += "                           FROM tblsimulationreport";
            sql += "                          WHERE cartoncode = '" + rcard + "'";
            sql += "                       ORDER BY mdate DESC, mtime DESC)";
            sql += "                WHERE ROWNUM = 1)";

            return this.DataProvider.CustomQuery(typeof(RcardToStackPallet), new SQLCondition(sql));
        }

        public bool OutInventory(string transCode, string businessType, string memo, string userCode, string cancelReason, DataTable outInvInfoHead, DataTable outInvInfoDetail, string dnConfirm)
        {
            try
            {
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                this.DataProvider.BeginTransaction();
                foreach (DataRow headRow in outInvInfoHead.Rows)
                {

                    DataRow[] detailRows = outInvInfoDetail.Select("itemcode='" + headRow["itemcode"] + "' and storagecode = '" + headRow["storagecode"] + "'");

                    if (detailRows.Length > 0)
                    {
                        foreach (DataRow row in detailRows)
                        {
                            //根据情况不同获得CartonCode
                            string serialNo = row["rcard"].ToString();
                            string cartonCode = row["cartoncode"].ToString();
                            if (cartonCode == null || cartonCode.Trim().Length <= 0)
                            {
                                StackToRcard stackToRcard = (StackToRcard)GetStackToRcard(serialNo);
                                if (stackToRcard != null)
                                {
                                    if (stackToRcard.BusinessReason == BussinessReason.type_produce)
                                    {
                                        string sql = string.Empty;
                                        sql += "SELECT cartoncode ";
                                        sql += "FROM tblsimulationreport ";
                                        sql += "WHERE rcard = '" + serialNo + "' ";
                                        sql += "ORDER BY mdate DESC, mtime DESC ";
                                        object[] list = this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(sql));
                                        if (list != null)
                                        {
                                            cartonCode = ((SimulationReport)list[0]).CartonCode;
                                        }
                                    }
                                    else if (stackToRcard.BusinessReason == BussinessReason.type_noneproduce)
                                    {
                                        cartonCode = serialNo;
                                    }
                                }
                            }

                            ////Save Out Transaction info
                            //
                            InvOutTransaction invOutTransaction = new InvOutTransaction();
                            invOutTransaction.TransCode = transCode;
                            invOutTransaction.DNLine = headRow["lineno"].ToString();
                            invOutTransaction.Rcard = row["rcard"].ToString();
                            invOutTransaction.CartonCode = cartonCode;
                            invOutTransaction.PalletCode = row["palletcode"].ToString();
                            invOutTransaction.ItemCode = row["itemcode"].ToString();
                            invOutTransaction.MOCode = row["mocode"].ToString();
                            if (businessType.Trim().Length == 0)
                            {
                                invOutTransaction.BusinessCode = headRow["businesscode"].ToString();
                            }
                            else
                            {
                                invOutTransaction.BusinessCode = businessType;
                            }

                            invOutTransaction.StackCode = row["stackcode"].ToString();
                            invOutTransaction.StorageCode = row["storagecode"].ToString();
                            invOutTransaction.Company = row["company"].ToString();
                            invOutTransaction.OrgID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
                            object stack2Rcard = this.GetStackToRcard(invOutTransaction.Rcard);

                            if (stack2Rcard != null)
                            {
                                invOutTransaction.BusinessReason = ((StackToRcard)stack2Rcard).BusinessReason;
                                invOutTransaction.TransInSerial = ((StackToRcard)stack2Rcard).TransInSerial;
                            }
                            else
                            {
                                invOutTransaction.BusinessReason = BussinessReason.type_noneproduce;
                                invOutTransaction.TransInSerial = 0;
                            }
                            invOutTransaction.Memo = memo;
                            invOutTransaction.Serial = 0;
                            invOutTransaction.MaintainUser = userCode;
                            invOutTransaction.MaintainDate = dbDateTime.DBDate;
                            invOutTransaction.MaintainTime = dbDateTime.DBTime;

                            this.AddInvOutTransaction(invOutTransaction);

                            ////取消停发
                            //
                            if (row["pausecode"].ToString().Trim().Length != 0)
                            {
                                CancelPause(row["pausecode"].ToString().Trim(), cancelReason, row["storagecode"].ToString(), row["stackcode"].ToString(), row["itemcode"].ToString(), dbDateTime, userCode, row["rcard"].ToString());
                            }

                            ////Delete stack Rcard
                            //
                            this.DeleteStackToRcard(row["rcard"].ToString());

                            ////Delete Pallet
                            //
                            this.DeletePalletInfo(row["palletcode"].ToString(), row["rcard"].ToString(), dbDateTime, userCode);

                            //Update TBLDNTempOut
                            DNTempOut dnTempOut = (DNTempOut)this.GetDNTempOut(row["stackcode"].ToString(), row["itemcode"].ToString(), headRow["dnno"].ToString(), headRow["lineno"].ToString());

                            if (dnTempOut != null)
                            {
                                dnTempOut.TempQty -= 1;
                                if (dnTempOut.TempQty <= 0)
                                {
                                    this.DeleteDNTempOut(dnTempOut);
                                }
                                else
                                {
                                    this.UpdateDNTempOut(dnTempOut);
                                }
                            }
                        }

                        ////Update Out Qty and Status
                        //
                        this.UpdateOutQtyAndStatusToDN(headRow["dnno"].ToString(), headRow["lineno"].ToString(), Convert.ToDecimal(headRow["sendedqty"].ToString()), userCode, dbDateTime, dnConfirm);

                    }
                }


                this.DataProvider.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }


        }

        public bool CheckSAPConmpelete(string serialNo)
        {
            //string sql = " SELECT Count(*) FROM tblstack2rcard WHERE GETRCARDSTATUSONTIME(tblstack2rcard.serialno)  Like  '%SAP完工%'";
            //sql += " AND tblstack2rcard.serialno='" + serialNo.Trim().ToUpper() + "'";
            string sql = " SELECT COUNT (*) ";
            sql += "          FROM DUAL ";
            sql += "        WHERE INSTR (getrcardstatusontime ('" + serialNo + "'), 'SAP完工') > 0 ";

            int number = this.DataProvider.GetCount(new SQLCondition(sql));

            if (number > 0)
            {
                return true;
            }

            return false;
        }

        private void CancelPause(string pauseCode, string cancelReason, string storageCode, string stackCode, string itemCode, DBDateTime dtDateTime, string userCode, string rcard)
        {
            PauseFacade objFacade = new PauseFacade(this.DataProvider);

            ////解除停发
            //
            SimulationReport simulationReport = (SimulationReport)this.GetSimulationReportByRcardOrCartonCode(rcard.Trim().ToUpper());
            if (simulationReport != null)
            {
                rcard = simulationReport.RunningCard;
            }

            objFacade.UpdateCancelPause("", cancelReason, storageCode, stackCode, pauseCode, itemCode, dtDateTime, userCode, rcard);

            ////如果rcard都被解除停发,则把header资料解除停发
            //
            objFacade.UpdateCancelPauseHead(pauseCode, dtDateTime, userCode);
        }


        public object GetSimulationReportByRcardOrCartonCode(string serialNo)
        {
            object[] simulations = this.DataProvider.CustomQuery(typeof(SimulationReport),
                new SQLCondition(string.Format(
                @"select {0} from tblSimulationReport where
					(rcard,mocode) in (
					select rcard, mocode
					from (select rcard, mocode
							from tblsimulationreport
							where RCARD = '{1}'
                            or cartoncode= '{2}'
							order by MDATE desc, MTIME desc)
							where rownum = 1)",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport)), serialNo.Trim().ToUpper(), serialNo.Trim().ToUpper())));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations[0];
            else
                return null;
        }

        private void UpdateOutQtyAndStatusToDN(string dnNo, string lineNo, decimal qty, string userCode, DBDateTime dtNow, string dnConfirm)
        {
            //
            //sql = sql + "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(DeliveryNote));
            //sql = sql + "  FROM tbldn";
            //sql = sql + " WHERE dnno = '" + dnNo + "' ";
            //sql = sql + "   AND DNLine = '" + lineNo + "' ";
            bool isDNConfirmSAP = false;
            DeliveryFacade objFacade = new DeliveryFacade(this.DataProvider);
            object deliveryNote = objFacade.GetDeliveryNote(dnNo, lineNo);

            if (deliveryNote != null)
            {
                if (qty > (((DeliveryNote)deliveryNote).DNQuantity - ((DeliveryNote)deliveryNote).RealQuantity))
                {

                    ExceptionManager.Raise(this.GetType(), "$CS_OVER_OUTINV_MAX_QTY");
                    return;
                }
            }

            //判断是否要同步SAP
            if (dnConfirm != string.Empty && dnConfirm.Trim().Length > 0)
            {
                object[] ruleList = GetInvBusiness2Formula(dnConfirm, GlobalVariables.CurrentOrganizations.First().OrganizationID);

                if (ruleList != null)
                {
                    foreach (InvBusiness2Formula rule in ruleList)
                    {
                        if (rule.FormulaCode.Equals(OutInvRuleCheck.DNConfirmSAP))
                        {
                            isDNConfirmSAP = true;
                            break;
                        }
                    }
                }
            }


            string sql = string.Empty;
            sql += "UPDATE tbldn SET RealQuantity = RealQuantity + " + qty + ",";
            sql += "                           dnstatus = '" + DNStatus.StatusUsing + "', ";
            sql += "                           muser = '" + userCode + "', ";
            sql += "                           mdate = " + dtNow.DBDate + ",";
            sql += "                           mtime = " + dtNow.DBTime;
            sql += " WHERE dnno = '" + dnNo + "' ";
            sql += "   AND DNLine = '" + lineNo + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));


            ////如果已经完全出货，则更新状态为Close
            //
            sql = string.Empty;
            sql += "UPDATE tbldn SET dnstatus = '" + DNStatus.StatusClose + "', ";
            sql += "                           muser = '" + userCode + "', ";
            sql += "                           mdate = " + dtNow.DBDate + ",";
            sql += "                           mtime = " + dtNow.DBTime;
            sql += " WHERE  RealQuantity = dnquantity";
            sql += "   AND dnline = '" + lineNo + "' ";
            sql += "   AND dnno = '" + dnNo + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));


            ////更新MES状态下的Head的Status
            //
            sql = string.Empty;
            sql += "UPDATE tbldn a SET         a.dnstatus = '" + GetDNHeadStatus(dnNo, string.Empty) + "',";
            sql += "                           a.muser = '" + userCode + "', ";
            sql += "                           a.mdate = " + dtNow.DBDate + ",";
            sql += "                           a.mtime = " + dtNow.DBTime;
            sql += " WHERE a.dnno = '" + dnNo + "' ";
            sql += "   AND a.dnline = '0' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            //与SAP同步
            if (isDNConfirmSAP)
            {
                sql = string.Empty;
                sql += " UPDATE tbldn  SET     flag='" + FlagStatus.FlagStatus_MES + "',";
                sql += "                       DNStatus='" + DNStatus.StatusClose + "', ";
                sql += "                       mdate = " + dtNow.DBDate + ",";
                sql += "                       mtime = " + dtNow.DBTime;
                sql += " WHERE dnno='" + dnNo + "' ";
                sql += "  AND dnfrom='" + DNFrom.ERP + "'";
                this.DataProvider.CustomExecute(new SQLCondition(sql));
            }

        }

        private string GetDNHeadStatus(string dnNo, string lineNo)
        {
            string sql = string.Empty;

            sql += "SELECT * FROM tbldn WHERE 1=1 ";
            if (dnNo.Trim() != string.Empty)
            {
                sql += "   AND dnno = '" + dnNo + "'";
            }

            if (lineNo.Trim() != string.Empty)
            {
                sql += "   AND dnline = '" + lineNo + "' ";
            }

            sql += " ORDER BY dnno,dnline";

            object[] DeliveryNoteObjects = this.DataProvider.CustomQuery(typeof(DeliveryNote), new SQLCondition(sql));

            if (DeliveryNoteObjects != null && DeliveryNoteObjects.Length > 0)
            {
                for (int i = 0; i < DeliveryNoteObjects.Length; i++)
                {
                    if ((((DeliveryNote)DeliveryNoteObjects[i]).DNStatus == DNStatus.StatusUsing
                        || ((DeliveryNote)DeliveryNoteObjects[i]).DNStatus == DNStatus.StatusInit)  //Modify by Sandy 20130220
                        && ((DeliveryNote)DeliveryNoteObjects[i]).DNLine != "0")
                    {
                        return DNStatus.StatusUsing;
                    }
                }

                return DNStatus.StatusClose;
            }
            else
            {
                return DNStatus.StatusUsing;
            }
        }

        private void DeletePalletInfo(string palletCode, string rcard, DBDateTime dtNow, string userCode)
        {
            string sql = string.Empty;

            PackageFacade packageFacade = new PackageFacade(DataProvider);

            ////减少源Pallet的Rcard Count
            //
            sql += "UPDATE tblpallet SET rcardcount = rcardcount - 1,";
            sql += "                           muser = '" + userCode + "', ";
            sql += "                           mdate = " + dtNow.DBDate + ",";
            sql += "                           mtime = " + dtNow.DBTime;
            sql += " WHERE palletcode = '" + palletCode + "' ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            ////如果源栈板的RcardCount=0，则删除此栈板
            //
            sql = string.Empty;
            sql += "DELETE FROM tblpallet ";
            sql += " WHERE rcardcount = 0 ";
            sql += "   AND palletcode = '" + palletCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            ////删除PalletToRcard
            //
            sql = string.Empty;
            sql += "DELETE FROM tblpallet2rcard ";
            sql += " WHERE rcard = '" + rcard + "'";
            sql += "   AND palletcode = '" + palletCode + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));

            //记log
            packageFacade.SaveRemovePallet2RcardLog(palletCode, rcard, userCode);
        }


        public DataTable GetSimulationReportInfo(string code, string inputType)
        {
            string sql = string.Empty;
            sql = sql + @"SELECT mocode,itemcode,itemdesc,rcard,cartoncode,sscode FROM (
                            SELECT tblsimulationreport.mocode,tblsimulationreport.itemcode, 
                                   tblmaterial.mdesc itemdesc, rcard ,tblsimulationreport.cartoncode,tblsimulationreport.sscode,
                                    rank() over (PARTITION BY rcard ORDER BY tblsimulationreport.mdate desc ,tblsimulationreport.mtime DESC) cn
                              FROM tblsimulationreport,tblmaterial 
                             WHERE tblsimulationreport.itemcode=tblmaterial.mcode ";
            switch (inputType)
            {
                case "0":
                    sql = sql + "   AND palletcode='" + code + "' ";
                    break;
                case "2":
                    sql = sql + "   AND cartoncode='" + code + "' ";
                    break;
                case "1":
                    sql = sql + "   AND rcard='" + code + "' ";
                    break;
                default:
                    break;
            }

            sql = sql + " ) WHERE cn = 1";

            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            return ds.Tables[0];
        }

        #region 出库规则
        /// <summary>
        /// 判断序列号必须完工
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public bool CheckRcardIsFinished(string rcard)
        {
            string sql = string.Empty;
            sql += "SELECT *";
            sql += "  FROM (SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport));
            sql += "            FROM tblsimulationreport";
            sql += "           WHERE rcard = '" + rcard + "' OR cartoncode = '" + rcard + "'";
            sql += "        ORDER BY mdate DESC, mtime DESC)";
            sql += " WHERE ROWNUM = 1";

            object[] list = this.DataProvider.CustomQuery(typeof(SimulationReport), new SQLCondition(sql));

            if (list == null)
            {
                return true;
            }
            else
            {
                if (((SimulationReport)list[0]).IsComplete == "1")
                {
                    return true;
                }
            }

            return false;
            //throw new Exception("$CS_RCARD_IS_NOT_FINISHED $CS_Param_RunSeq=" + rcard);
        }

        /// <summary>
        /// 判断序列号不能完工
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public bool CheckRcardIsNotFinished(string rcard)
        {
            return !CheckRcardIsFinished(rcard);
        }

        /// <summary>
        /// 判断序列号是否被隔离
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public bool CheckRcardIsFrozen(string rcard)
        {
            string sql = string.Empty;
            //Modified By Nettie Chen on 2009/10/24 for add condition of frozenstatus
            //sql += "SELECT COUNT(*) ";
            //sql += "  FROM tblfrozen";
            //sql += " WHERE rcard = '" + rcard + "' OR rcard IN (SELECT rcard";
            //sql += "                                 FROM tblsimulationreport";
            //sql += "                                WHERE cartoncode = '" + rcard + "')";
            sql += "SELECT COUNT(*)";
            sql += " FROM tblfrozen a";
            sql += " WHERE (rcard =  '" + rcard + "' ";
            sql += " OR  exists (";
            sql += "    SELECT 1                      ";
            sql += "    FROM tblsimulationreport  b";
            sql += "    WHERE a.rcard=b.cartoncode  ";
            sql += "    AND b.cartoncode =  '" + rcard + "'";
            sql += "    ))";
            sql += " AND frozenstatus='status_fronzen'";
            //End Modified

            if (this.DataProvider.GetCount(new SQLCondition(sql)) != 0)
            {
                return true;
                //throw new Exception("$CS_RCARD_IS_FROZEN  $CS_Param_RunSeq=" + rcard);
            }

            return false;
        }

        /// <summary>
        /// 判断序列号是否下地
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public bool CheckRcardIsDown(string rcard)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += "  FROM tbldown";
            sql += " WHERE (rcard='" + rcard + "'";
            sql += "    OR rcard IN (SELECT rcard";
            sql += "                   FROM tblsimulationreport";
            sql += "                  WHERE cartoncode = '" + rcard + "'))";
            sql += "   AND orgid=" + GlobalVariables.CurrentOrganizations.First().OrganizationID;

            if (this.DataProvider.GetCount(new SQLCondition(sql)) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断序列号是否被停发
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public object[] GetRcardIsPause(string rcard)
        {
            string sql = string.Empty;
            sql += "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pause2Rcard));
            sql += "  FROM tblpause2rcard ";
            sql += " WHERE serialno = '" + rcard + "'";
            sql += "   AND status = '" + PauseStatus.status_pause + "'";


            return this.DataProvider.CustomQuery(typeof(Pause2Rcard), new SQLCondition(sql));
        }

        /// <summary>
        /// 判断必须关联出库单据
        /// </summary>
        /// <param name="rcard"></param>
        /// <returns></returns>
        public bool CheckRelatedInvOutDoc(string rcard, string transCode)
        {
            string sql = string.Empty;
            object[] invOutTransList = GetInvOutTransaction(rcard, transCode);

            if (invOutTransList != null)
            {
                return true;
            }
            return false;
        }

        #endregion

        #endregion

        #region InvInTransaction
        public InvInTransaction CreateInvInTransaction()
        {
            return new InvInTransaction();
        }

        public void AddInvInTransaction(InvInTransaction invInTransaction)
        {
            this.DataProvider.Insert(invInTransaction);
        }

        public void UpdateInvInTransaction(InvInTransaction invInTransaction)
        {
            this.DataProvider.Update(invInTransaction);
        }

        public void DeleteInvInTransaction(InvInTransaction invInTransaction)
        {
            this.DataProvider.Delete(invInTransaction);
        }

        public object GetInvInTransaction(Int32 serial)
        {
            return this.DataProvider.CustomSearch(typeof(InvInTransaction), new object[] { serial });
        }

        public void DeleteInvInTransactionByRcard(string rcard)
        {
            string sql = "DELETE FROM tblinvintransaction WHERE serial =(SELECT MAX(serial) FROM tblinvintransaction WHERE rcard='" + rcard + "')";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void DeleteInvInTransactionByCarton(string cartonCode)
        {
            string sql = "DELETE FROM tblinvintransaction WHERE serial =(SELECT MAX(serial) FROM tblinvintransaction WHERE cartoncode='" + cartonCode + "')";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object GetInvInTransaction(string rCard, string cartonCode, string palletCode, string moCode, string storageCode,
                                            string stackCode, string mUser)
        {
            string sql = " SELECT  * FROM tblinvintransaction where 1=1 ";

            if (rCard.Trim() != string.Empty)
            {
                sql += " and RCARD='" + rCard.Trim().ToUpper() + "'";
            }

            if (cartonCode.Trim() != string.Empty)
            {
                sql += " and CARTONCODE='" + cartonCode.Trim().ToUpper() + "'";
            }

            if (palletCode.Trim() != string.Empty)
            {
                sql += " and PALLETCODE='" + palletCode.Trim().ToUpper() + "'";
            }

            if (moCode.Trim() != string.Empty)
            {
                sql += " and MOCODE='" + moCode.Trim().ToUpper() + "'";
            }

            if (storageCode.Trim() != string.Empty)
            {
                sql += " and STORAGECODE='" + storageCode.Trim().ToUpper() + "'";
            }

            if (stackCode.Trim() != string.Empty)
            {
                sql += " and STACKCODE='" + stackCode.Trim().ToUpper() + "'";
            }

            //if (itemGrade.Trim() != string.Empty)
            //{
            //    sql += " and ITEMGRADE='" + itemGrade.Trim().ToUpper() + "'";
            //}

            if (mUser.Trim() != string.Empty)
            {
                sql += " and MUSER='" + mUser.Trim().ToUpper() + "'";
            }

            sql += " ORDER BY serial DESC";

            object[] queryObjects = this.DataProvider.CustomQuery(typeof(InvInTransaction), new SQLCondition(sql));

            if (queryObjects != null)
            {
                return queryObjects[0];
            }

            return null;
        }

        #endregion

        #region InvBusiness
        public InvBusiness CreateInvBusiness()
        {
            return new InvBusiness();
        }

        public void AddInvBusiness(InvBusiness invBusiness)
        {
            this.DataProvider.Insert(invBusiness);
        }

        public void UpdateInvBusiness(InvBusiness invBusiness)
        {
            this.DataProvider.Update(invBusiness);
        }

        public void DeleteInvBusiness(InvBusiness invBusiness)
        {
            this.DataProvider.Delete(invBusiness);
        }

        public void DeleteInvBusiness(InvBusiness[] invBusiness)
        {
            this._helper.DeleteDomainObject(invBusiness, new ICheck[] { new DeleteAssociateCheck(invBusiness,
                                                                            this.DataProvider, 
                                                                            new Type[]{ typeof(InvBusiness2Formula) })});
        }

        public object GetInvBusiness(string businessCode, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(InvBusiness), new object[] { businessCode, orgID });
        }

        public object[] QueryInvBusiness(string businessCode, string businessDesc, string businessType, int orgId, int inclusive, int exclusive)
        {
            string sql = "";
            sql = string.Format("select {0} from tblinvbusiness where orgid = {1} and BUSINESSCODE like '%{2}%' and BUSINESSDESC like '%{3}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvBusiness)), orgId, businessCode, businessDesc);

            if (businessType != null && businessType.Length > 0)
            {
                sql = string.Format("{0} and BUSINESSTYPE = '{1}'", sql, businessType);
            }

            return this.DataProvider.CustomQuery(typeof(InvBusiness), new PagerCondition(sql, "businesscode", inclusive, exclusive));
        }

        public int QueryInvBusinessCount(string businessCode, string businessDesc, string businessType, int orgId)
        {
            string sql = "";
            sql = string.Format("select count(*) from tblinvbusiness where orgid = {0} and BUSINESSCODE like '%{1}%' and BUSINESSDESC like '%{2}%'", orgId, businessCode, businessDesc);

            if (businessType != null && businessType.Length > 0)
            {
                sql = string.Format("{0} and BUSINESSTYPE = '{1}'", sql, businessType);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetAllInvBusiness()
        {
            return this.DataProvider.CustomQuery(
                typeof(InvBusiness), new SQLCondition(
                string.Format("select {0} from tblinvbusiness ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvBusiness)))
                )
                );
        }

        public object[] GetInvBusiness(string businessReason, string businessType)
        {
            string sql = string.Empty;
            sql = sql + string.Format("SELECT {0} FROM tblinvbusiness", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvBusiness)));
            sql = sql + " WHERE 1=1 ";

            if (businessReason != string.Empty || businessReason != "")
            {
                sql = sql + "  AND businessreason = '" + businessReason + "' ";
            }

            if (businessType != string.Empty || businessType != "")
            {
                sql = sql + "  AND businesstype = '" + businessType + "' ";
            }

            sql = sql + "  ORDER BY businesscode, businessdesc ";

            return this.DataProvider.CustomQuery(typeof(InvBusiness), new SQLCondition(sql));
        }

        public object[] GetAllBusiness()
        {
            string sql = string.Format("SELECT businesscode, businessdesc FROM tblinvbusiness WHERE businesstype='{0}' ORDER BY businesscode", BussinessType.type_out);

            return this.DataProvider.CustomQuery(typeof(InvBusiness), new SQLCondition(sql));
        }

        public object[] QueryInvBusiness(string businessType, int orgid)
        {
            string sql = "SELECT DISTINCT businesscode, businessdesc FROM tblinvbusiness WHERE 1=1 ";
            if (businessType != string.Empty || businessType != "")
            {
                sql = sql + "  AND businesstype = '" + businessType + "' ";
            }
            sql = sql + " AND ORGID = " + orgid;
            sql = sql + "  ORDER BY businesscode, businessdesc ";

            return this.DataProvider.CustomQuery(typeof(InvBusiness), new SQLCondition(sql));
        }

        #endregion

        #region InvOutTransaction
        public InvOutTransaction CreateInvOutTransaction()
        {
            return new InvOutTransaction();
        }

        public void AddInvOutTransaction(InvOutTransaction invOutTransaction)
        {
            this.DataProvider.Insert(invOutTransaction);
        }

        public void UpdateInvOutTransaction(InvOutTransaction invOutTransaction)
        {
            this.DataProvider.Update(invOutTransaction);
        }

        public void DeleteInvOutTransaction(InvOutTransaction invOutTransaction)
        {
            this.DataProvider.Delete(invOutTransaction);
        }

        public object GetInvOutTransaction(Int32 serial)
        {
            return this.DataProvider.CustomSearch(typeof(InvOutTransaction), new object[] { serial });
        }

        public object[] GetInvOutTransaction(string rcard, string transCode)
        {
            string sql = string.Empty;
            sql += " SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvOutTransaction));
            sql += "   FROM tblInvOutTransaction ";
            sql += "  WHERE 1=1 ";

            if (rcard.Length != 0)
            {
                sql += "    AND (rcard = '" + rcard + "' OR CartonCode = '" + rcard + "')";
            }

            if (transCode.Length != 0)
            {
                sql += "    AND transcode = '" + transCode + "'";
            }

            sql += " order by mdate desc,mtime desc";

            return this.DataProvider.CustomQuery(typeof(InvOutTransaction), new SQLCondition(sql));
        }

        //public object[] GetAllInvOutTransaction()
        //{
        //    return this.DataProvider.CustomQuery(
        //        typeof(Storage), new SQLCondition(
        //        string.Format("select {0} from tblstorage ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Storage)))
        //        )
        //        );
        //}
        #endregion

        #region InvFormula
        public InvFormula CreateNewInvFormula()
        {
            return new InvFormula();
        }

        public void AddInvFormula(InvFormula invFormula)
        {
            this.DataProvider.Insert(invFormula);
        }

        public void UpdateInvFormula(InvFormula invFormula)
        {
            this.DataProvider.Update(invFormula);
        }

        public void DeleteInvFormula(InvFormula invFormula)
        {
            this.DataProvider.Delete(invFormula);
        }

        public void DeleteInvFormula(InvFormula[] invFormula)
        {
            this._helper.DeleteDomainObject(invFormula, new ICheck[] { new DeleteAssociateCheck(invFormula,
                                                                            this.DataProvider, 
                                                                            new Type[]{ typeof(InvBusiness2Formula) })});
        }

        public object GetInvFormula(string formulaCode)
        {
            return this.DataProvider.CustomSearch(typeof(InvFormula), new object[] { formulaCode });
        }

        public object[] QueryInvFormula(string formulaCode, string formulaDesc, int inclusive, int exclusive)
        {
            string sql = string.Format("select {0} from tblinvformula where FORMULACODE like '%{1}%' and FORMULADESC like '%{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvFormula)), formulaCode, formulaDesc);

            return this.DataProvider.CustomQuery(typeof(InvFormula), new PagerCondition(sql, "formulacode", inclusive, exclusive));
        }

        public int QueryInvFormulaCount(string formulaCode, string formulaDesc)
        {
            string sql = string.Format("select count(*) from tblinvformula where FORMULACODE like '%{1}%' and FORMULADESC like '%{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvFormula)), formulaCode, formulaDesc);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region InvPeriod
        public InvPeriod CreateNewInvPeriod()
        {
            return new InvPeriod();
        }

        public void AddInvPeriod(InvPeriod invPeriod)
        {
            this.DataProvider.Insert(invPeriod);
        }

        public void UpdateInvPeriod(InvPeriod invPeriod)
        {
            this.DataProvider.Update(invPeriod);
        }

        public void DeleteInvPeriod(InvPeriod invPeriod)
        {
            if (InvPeriodUsed(invPeriod))
            {
                throw new Exception("$Error_DeleteInvPeriodUsed " + "PeriodGroup = " + invPeriod.PeiodGroup + " InvPeriodCode = " + invPeriod.InvPeriodCode);
            }

            this.DataProvider.Delete(invPeriod);
        }

        public void DeleteInvPeriod(InvPeriod[] invPeriodArray)
        {
            try
            {
                this.DataProvider.BeginTransaction();

                foreach (InvPeriod invPeriod in invPeriodArray)
                {
                    DeleteInvPeriod(invPeriod);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public object GetInvPeriod(string periodCode, string PeiodGroup)
        {
            return this.DataProvider.CustomSearch(typeof(InvPeriod), new object[] { periodCode, PeiodGroup });
        }

        public object[] QueryInvPeriod(int inclusive, int exclusive)
        {
            string sql = string.Format("select {0} from tblinvperiod", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvPeriod)));

            return this.DataProvider.CustomQuery(typeof(InvPeriod), new PagerCondition(sql, "peiodgroup,invperiodcode", inclusive, exclusive));
        }

        public object[] QueryInvPeriod(string periodGroup, int inclusive, int exclusive)
        {
            string sql = string.Format("select {0} from tblinvperiod ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvPeriod)));
            sql += "WHERE peiodgroup = '" + periodGroup + "' ";

            return this.DataProvider.CustomQuery(typeof(InvPeriod), new PagerCondition(sql, "peiodgroup,datefrom,invperiodcode", inclusive, exclusive));
        }

        public int QueryInvPeriodCount()
        {
            string sql = "select count(*) from tblinvperiod";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private bool InvPeriodUsed(InvPeriod invPeriod)
        {
            string sql = "SELECT COUNT(*) FROM tblinvperiodstd WHERE periodgroup = '{0}' AND invperiodcode = '{1}' ";
            sql = string.Format(sql, invPeriod.PeiodGroup, invPeriod.InvPeriodCode);

            int count = this.DataProvider.GetCount(new SQLCondition(sql));

            return count > 0;
        }

        #endregion

        #region InventoryPeriodStandard

        public InventoryPeriodStandard CreateNewInventoryPeriodStandard()
        {
            return new InventoryPeriodStandard();
        }

        public void AddInventoryPeriodStandard(InventoryPeriodStandard inventoryPeriodStandard)
        {
            this._helper.AddDomainObject(inventoryPeriodStandard);
        }

        public void DeleteInventoryPeriodStandard(InventoryPeriodStandard inventoryPeriodStandard)
        {
            this._helper.DeleteDomainObject(inventoryPeriodStandard);
        }

        public void DeleteInventoryPeriodStandard(InventoryPeriodStandard[] inventoryPeriodStandardArray)
        {
            this._helper.DeleteDomainObject(inventoryPeriodStandardArray);
        }

        public void UpdateInventoryPeriodStandard(InventoryPeriodStandard inventoryPeriodStandard)
        {
            this._helper.UpdateDomainObject(inventoryPeriodStandard);
        }

        public object GetInventoryPeriodStandard(string inventoryType, string periodGroup, string inventoryPeriodCode)
        {
            return this.DataProvider.CustomSearch(typeof(InventoryPeriodStandard), new object[] { inventoryType, periodGroup, inventoryPeriodCode });
        }

        public object[] QueryInventoryPeriodStandard(string storageAttribute, int inclusive, int exclusive)
        {
            string sql = string.Empty;
            sql += string.Format("SELECT {0} ", DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(InventoryPeriodStandard)));
            sql += GetQueryInventoryPeriodStandardSQL(storageAttribute);
            sql += "ORDER BY tblinvperiodstd.invtype, tblinvperiodstd.periodgroup, tblinvperiod.datefrom, tblinvperiodstd.invperiodcode";

            return this.DataProvider.CustomQuery(typeof(InventoryPeriodStandard), new PagerCondition(sql, inclusive, exclusive));
        }

        public int QueryInventoryPeriodStandardCount(string storageAttribute)
        {
            string sql = string.Empty;
            sql += "SELECT COUNT(*) ";
            sql += GetQueryInventoryPeriodStandardSQL(storageAttribute);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryInventoryPeriodStandardStorageAttributeCount()
        {
            string sql = string.Empty;
            sql = "SELECT COUNT(DISTINCT invtype) FROM tblinvperiodstd ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public string GetQueryInventoryPeriodStandardSQL(string storageAttribute)
        {
            string sql = string.Empty;

            sql += "FROM tblinvperiodstd, tblinvperiod ";
            sql += "WHERE tblinvperiodstd.periodgroup = tblinvperiod.peiodgroup ";
            sql += "AND tblinvperiodstd.invperiodcode = tblinvperiod.invperiodcode ";

            if (storageAttribute.Trim().Length > 0)
            {
                sql += "AND tblinvperiodstd.invtype = '" + storageAttribute + "'";
            }

            return sql;
        }

        public object[] QueryPeriodGroupByStorageAttribute(string storageAttribute)
        {
            string sqlTemplate = string.Empty;
            sqlTemplate += "SELECT DISTINCT peiodgroup ";
            sqlTemplate += "FROM tblinvperiod ";
            sqlTemplate += "{0} ";
            sqlTemplate += "ORDER BY peiodgroup ";

            string sql = string.Format(sqlTemplate, "WHERE peiodgroup IN (SELECT periodgroup FROM tblinvperiodstd WHERE invtype = '" + storageAttribute + "')");

            object[] list = this.DataProvider.CustomQuery(typeof(InvPeriod), new SQLCondition(sql));
            if (list == null)
            {
                list = this.DataProvider.CustomQuery(typeof(InvPeriod), new SQLCondition(string.Format(sqlTemplate, "")));
            }

            return list;
        }

        public bool CheckInventoryPeriodStandard(string storageAttribute, string periodGroup, string periodCode, decimal indexValue)
        {
            bool returnValue = true;

            string sql = string.Empty;
            sql += "SELECT NVL(SUM(percentagestd), 0) AS percentagestd ";
            sql += "FROM tblinvperiodstd ";
            sql += "WHERE invtype = '{0}'  ";
            sql += "AND periodgroup = '{1}'  ";
            sql += "AND invperiodcode <> '{2}' ";
            sql = string.Format(sql, storageAttribute, periodGroup, periodCode);

            object[] list = this.DataProvider.CustomQuery(typeof(InventoryPeriodStandard), new SQLCondition(sql));

            if (list != null && list.Length > 0)
            {
                indexValue += ((InventoryPeriodStandard)list[0]).PercentageStandard;
                if (indexValue < 0 || indexValue > 1)
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        public int QueryProductInvPeriodCount(string storageAttributeList, int startDate)
        {
            int returnValue = 0;

            returnValue = QueryInventoryPeriodStandardStorageAttributeCount();

            return returnValue;
        }

        public object[] QueryProductInvPeriod(string storageAttributeList, int startDate, int inclusive, int exclusive)
        {
            //成品/半成品
            string sqlProduct = string.Empty;
            sqlProduct += "SELECT eattribute1 AS invtype, tblinvperiod.peiodgroup AS periodgroup, tblinvperiod.invperiodcode, tblinvperiod.datefrom, tblinvperiod.dateto, COUNT(inv.rcard) AS productcount ";
            sqlProduct += "FROM ( ";
            sqlProduct += "    SELECT tblstorage.eattribute1, tblsimulationreport.rcard, tblstack2rcard.indate, ";
            sqlProduct += "        RANK() OVER(PARTITION BY tblsimulationreport.rcard ORDER BY tblsimulationreport.mdate DESC, tblsimulationreport.mtime DESC) AS rank ";
            sqlProduct += "    FROM tblstack2rcard, tblstorage, tblsimulationreport ";
            sqlProduct += "    WHERE tblstack2rcard.storagecode = tblstorage.storagecode ";
            sqlProduct += "    AND (tblstack2rcard.serialno = tblsimulationreport.rcard ";
            sqlProduct += "        OR tblstack2rcard.serialno = tblsimulationreport.cartoncode) ";
            sqlProduct += ") inv, tblinvperiod ";
            sqlProduct += "WHERE inv.rank = 1 ";
            sqlProduct += "AND inv.indate BETWEEN ";
            sqlProduct += "    TO_CHAR(SYSDATE - tblinvperiod.dateto, 'yyyymmdd') AND ";
            sqlProduct += "    TO_CHAR(SYSDATE - tblinvperiod.datefrom, 'yyyymmdd') ";
            if (startDate > 0)
            {
                sqlProduct += string.Format("AND inv.indate >= {0} ", startDate.ToString());
            }
            sqlProduct += "GROUP BY inv.eattribute1, tblinvperiod.peiodgroup, tblinvperiod.invperiodcode, tblinvperiod.datefrom, tblinvperiod.dateto ";


            //总的
            string sql = string.Empty;
            sql += "SELECT tblsysparam.paramdesc AS invtype, tblinvperiodstd.periodgroup, tblinvperiodstd.invperiodcode, ";
            sql += "    MAX(tblinvperiodstd.percentagestd) AS percentagestd, ";
            sql += "    tblinvperiod.datefrom, tblinvperiod.dateto, ";
            sql += "    SUM(NVL(inv.productcount, 0)) AS productcount ";
            sql += "FROM tblinvperiodstd ";
            sql += "LEFT OUTER JOIN( ";
            sql += sqlProduct;
            sql += ") inv ";
            sql += "ON inv.invtype = tblinvperiodstd.invtype ";
            sql += "AND inv.periodgroup = tblinvperiodstd.periodgroup ";
            sql += "AND inv.invperiodcode = tblinvperiodstd.invperiodcode ";
            sql += "LEFT OUTER JOIN tblinvperiod ";
            sql += "ON tblinvperiod.peiodgroup = tblinvperiodstd.periodgroup ";
            sql += "AND tblinvperiod.invperiodcode = tblinvperiodstd.invperiodcode ";
            sql += "INNER JOIN tblsysparam ";
            sql += "ON tblsysparam.paramcode = tblinvperiodstd.invtype ";
            sql += "AND tblsysparam.paramgroupcode = 'STORAGEATTRIBUTE' ";
            sql += "WHERE 1 = 1 ";
            if (storageAttributeList.Trim().Length > 0)
            {
                sql += string.Format("AND tblinvperiodstd.invtype IN ({0}) ", FormatHelper.ProcessQueryValues(storageAttributeList));
            }

            sql += "GROUP BY tblsysparam.paramdesc, tblsysparam.eattribute1, tblinvperiodstd.periodgroup, tblinvperiodstd.invperiodcode, tblinvperiod.datefrom, tblinvperiod.dateto ";
            sql += "ORDER BY tblsysparam.eattribute1, tblinvperiodstd.periodgroup, tblinvperiod.datefrom, tblinvperiod.dateto ";

            return this.DataProvider.CustomQuery(typeof(ProductInvPeriod), new SQLCondition(sql));
        }

        #endregion

        #region InvBusiness2Formula
        public InvBusiness2Formula CreateNewInvBusiness2Formula()
        {
            return new InvBusiness2Formula();
        }

        public void AddInvBusiness2Formula(InvBusiness2Formula[] invBusiness2Formula)
        {
            this._helper.AddDomainObject(invBusiness2Formula);
        }

        public void DeleteInvBusiness2Formula(InvBusiness2Formula[] invBusiness2Formula)
        {
            this._helper.DeleteDomainObject(invBusiness2Formula);
        }

        public object GetInvBusiness2Formula(string businessCode, string formulaCode, int orgId)
        {
            return this.DataProvider.CustomSearch(typeof(InvBusiness2Formula), new object[] { businessCode, formulaCode, orgId });
        }


        public object[] GetInvBusiness2Formula(string businessCode, int orgId)
        {
            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvBusiness2Formula));
            sql = sql + "   FROM TBLInvBusiness2Formula ";
            sql = sql + "  WHERE businesscode = '" + businessCode + "'";
            sql = sql + "    AND orgid = " + orgId;

            return this.DataProvider.CustomQuery(typeof(InvBusiness2Formula), new SQLCondition(sql));
        }



        public int QueryInvBusinessNotFormulaCount(string businessCode, string formulaCode, string formulaDesc)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVFORMULA where FORMULACODE not in(" +
                "select FORMULACODE from TBLINVBUSINESS2FORMULA where BUSINESSCODE = '{0}') and FORMULACODE like '%{1}%' and FORMULADESC like '%{2}%'", businessCode, formulaCode, formulaDesc)));
        }

        public object[] QueryInvBusinessNotFormulaQuery(string businessCode, string formulaCode, string formulaDesc, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(InvFormula), new PagerCondition(string.Format("select {0} from TBLINVFORMULA where FORMULACODE not in (" +
                "select FORMULACODE from TBLINVBUSINESS2FORMULA where BUSINESSCODE = '{1}') and FORMULACODE like '%{2}%' and FORMULADESC like '%{3}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvFormula)), businessCode, formulaCode, formulaDesc), "FORMULACODE", inclusive, exclusive));
        }

        public int QueryInvBusinessFormulaCount(string businessCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLINVFORMULA where FORMULACODE in(" +
                "select FORMULACODE from TBLINVBUSINESS2FORMULA where BUSINESSCODE = '{0}')", businessCode)));
        }

        public object[] QueryInvBusinessFormulaQuery(string businessCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(InvFormula), new PagerCondition(string.Format("select {0} from TBLINVFORMULA where FORMULACODE in (" +
                "select FORMULACODE from TBLINVBUSINESS2FORMULA where BUSINESSCODE = '{1}') ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(InvFormula)), businessCode), "FORMULACODE", inclusive, exclusive));
        }

        #endregion

        #region MaterialLot

        public MaterialLot CreateNewMaterialLot()
        {
            return new MaterialLot();
        }

        public void AddMaterialLot(MaterialLot materialLot)
        {
            this._helper.AddDomainObject(materialLot);
        }

        public void UpdateMaterialLot(MaterialLot materialLot)
        {
            this._helper.UpdateDomainObject(materialLot);
        }

        public void DeleteMaterialLot(MaterialLot materialLot)
        {
            this._helper.DeleteDomainObject(materialLot);
        }

        public void DeleteMaterialLot(MaterialLot[] materialLot)
        {
            this._helper.DeleteDomainObject(materialLot);
        }

        public object GetMaterialLot(string materialLotNo)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialLot), new object[] { materialLotNo });
        }

        public object[] QueryMaterialLotAndItemDesc(string moCode, string workSeat, string iqcNo)
        {
            string sql = string.Empty;
            sql += " SELECT DISTINCT MA.MDESC AS ITEMDESC,IQC.MATERIALLOT, IQC.*,VN.VENDORNAME AS VendorDesc ";
            sql += " FROM TBLSBOM BOM, TBLMO MO, TBLMATERIALLOT IQC,TBLMATERIAL MA,tblvendor  VN";
            sql += "        WHERE MO.ITEMCODE = BOM.ITEMCODE   AND MO. MOBOM = BOM. SBOMVER";
            sql += "             AND MO.ORGID = BOM.ORGID   AND IQC.ITEMCODE = BOM.SBITEMCODE";
            //sql += "                AND IQC.ITEMCODE=MA.MCODE   AND IQC.ORGID = MA.ORGID AND IQC.vendorcode(+)=VN.vendorcode";
            sql += "                AND IQC.ITEMCODE=MA.MCODE   AND IQC.ORGID = MA.ORGID AND IQC.vendorcode=VN.vendorcode(+)";

            if (moCode.Trim() != string.Empty)
            {
                sql += "    AND MO.MOCODE = '" + moCode.Trim().ToUpper() + "'  ";
            }

            if (workSeat.Trim() != string.Empty)
            {
                sql += "    AND BOM.SBITEMSEQ = '" + workSeat.Trim().ToUpper() + "' ";
            }

            if (iqcNo.Trim() != string.Empty)
            {
                sql += "    AND IQC.IQCNO = '" + iqcNo.Trim().ToUpper() + "'";
            }

            //sql += "  AND iqc.lotqty>0  AND iqc.fifoflag='Y'";
            sql += "  AND iqc.lotqty>0 ";

            sql += "    ORDER BY IQC.Itemcode ASC, IQC.Createdate ASC, IQC.MATERIALLOT ASC ";

            return this._domainDataProvider.CustomQuery(typeof(Domain.Material.MaterialLotWithItemDesc), new SQLCondition(sql));
        }

        public object[] QueryMaterialIssue(string materialLotNo, string storage, string iqcNo, string item, int stockInDateFrom, int stockInDateTo, string vendor)
        {
            string sql = string.Empty;

            sql += "SELECT DISTINCT inv.*, NVL (ma.mdesc, ' ') AS itemdesc, ";
            sql += "                NVL (vn.vendorname, ' ') AS vendordesc ";
            sql += "           FROM tblmateriallot inv, ";
            sql += "                tblmaterial ma, ";
            sql += "                tblvendor vn ";
            sql += "          WHERE inv.itemcode = ma.mcode(+) ";
            sql += "            AND inv.orgid = ma.orgid(+) ";
            sql += "            AND inv.vendorcode = vn.vendorcode(+) ";
            sql += "            AND inv.Createdate >= " + stockInDateFrom;
            sql += "            AND inv.Createdate <= " + stockInDateTo;

            if (materialLotNo.Trim() != string.Empty)
            {
                sql += "     AND inv.MATERIALLOT = '" + materialLotNo.Trim().ToUpper() + "'  ";
            }

            if (storage.Trim() != string.Empty)
            {
                sql += "    AND inv.STORAGEID = '" + storage.Trim().ToUpper() + "' ";
            }

            if (iqcNo.Trim() != string.Empty)
            {
                sql += "     AND inv.IQCNO = '" + iqcNo.Trim().ToUpper() + "'  ";
            }

            if (item.Trim() != string.Empty)
            {
                sql += "     AND inv.ITEMCODE = '" + item.Trim().ToUpper() + "'  ";
            }

            if (vendor.Trim() != string.Empty)
            {
                sql += "     AND inv.VENDORCODE = '" + vendor.Trim().ToUpper() + "'  ";
            }

            sql += "  AND inv.lotqty>0 ";
            sql += "    ORDER BY inv.Itemcode ASC, inv.Createdate ASC, inv.MATERIALLOT ASC ";

            return this._domainDataProvider.CustomQuery(typeof(Domain.Material.MaterialLotWithItemDesc), new SQLCondition(sql));
        }

        public object GetMaterialLotAndItemDesc(string moCode, string materialLotNo)
        {
            string sql = string.Empty;
            sql += " SELECT ma.mdesc AS ITEMDESC,BOM.SBITEMQTY AS SBITEMQTY,IQC.*,VN.VENDORNAME AS VendorDesc";
            sql += " FROM TBLMATERIALLOT IQC, TBLSBOM BOM, TBLMO MO, tblmaterial ma,tblvendor  VN";
            sql += " WHERE IQC.ITEMCODE = BOM.SBITEMCODE AND MO.ITEMCODE = BOM.ITEMCODE";
            sql += "  AND MO. MOBOM = BOM. SBOMVER   AND MO.ORGID = BOM.ORGID    ";
            //sql += " AND IQC.ITEMCODE=ma.mcode   AND IQC.ORGID = ma.ORGID  AND IQC.vendorcode(+)=VN.vendorcode";
            sql += " AND IQC.ITEMCODE=ma.mcode   AND IQC.ORGID = ma.ORGID  AND IQC.vendorcode=VN.vendorcode(+)";

            if (moCode.Trim() != string.Empty)
            {
                sql += "    AND MO.MOCODE = '" + moCode.Trim().ToUpper() + "'  ";
            }
            if (materialLotNo.Trim() != string.Empty)
            {
                sql += "     AND IQC.MATERIALLOT = '" + materialLotNo.Trim().ToUpper() + "'  ";
            }

            //sql += "  AND IQC.fifoflag='Y'";

            object[] getobjects = this._domainDataProvider.CustomQuery(typeof(Domain.Material.MaterialLotWithItemDesc), new SQLCondition(sql));

            if (getobjects == null)
            {
                return null;
            }
            else
            {
                return getobjects[0];
            }
        }

        public object[] QueryMaterialLot(string itemCode, string vendorCode, int bufferDate, string materiallotList, string storageId)
        {
            string sql = string.Empty;
            sql += "SELECT * FROM tblmateriallot WHERE 1 = 1 ";
            sql += "AND fifoflag = 'Y' ";
            sql += "AND lotqty <> 0 ";

            if (itemCode.Trim() != string.Empty)
            {
                sql += "AND itemcode = '" + itemCode.Trim().ToUpper() + "' ";
            }

            if (vendorCode.Trim() != string.Empty)
            {
                sql += "AND vendorcode = '" + vendorCode.Trim().ToUpper() + "' ";
            }

            if (bufferDate > 0)
            {
                sql += "AND createdate < " + bufferDate + " ";
            }

            //FIFO检查不需要这个限制，有可能前一个Lot部分发料，那么这个Lot仍需要FIFO
            //if (materiallotList.Trim() != string.Empty && materiallotList.Trim().Length > 0)
            //{
            //    sql += "AND materiallot NOT IN (SELECT DISTINCT MATERIALLOT FROM TBLMATERIALLOT WHERE MATERIALLOT IN (" + materiallotList.Trim().ToUpper() + ") AND lotqty=0) ";
            //}

            //FIFO检查范围是针对同一个库别下的lot
            if (storageId.Trim() != string.Empty)
            {
                sql += " AND storageid='" + storageId + "' ";
            }


            sql += " ORDER BY Itemcode ASC, Createdate ASC, MATERIALLOT ASC ";

            return this._domainDataProvider.CustomQuery(typeof(Domain.Material.MaterialLot), new SQLCondition(sql));
        }

        public object[] QueryMaterialLot(string vendorCode, string itemCode, int createDate)
        {
            string sql = string.Empty;
            sql += "SELECT {0} ";
            sql += "FROM tblmateriallot ";
            sql += "WHERE 1 = 1 ";

            if (vendorCode.Trim().Length > 0)
            {
                sql += "AND vendorcode = '" + vendorCode.Trim().ToUpper() + "' ";
            }

            if (itemCode.Trim().Length > 0)
            {
                sql += "AND itemcode = '" + itemCode.Trim().ToUpper() + "' ";
            }

            if (createDate > 0)
            {
                sql += "AND createdate = " + createDate.ToString() + " ";
            }

            sql += "ORDER BY materiallot ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(MaterialLot)));

            return this.DataProvider.CustomQuery(typeof(MaterialLot), new SQLCondition(sql));
        }

        public string GetNewMaterialLotRunningNumber(string vendorCode, string itemCode, int createDate)
        {
            string returnValue = "001";

            object[] list = QueryMaterialLot(vendorCode, itemCode, createDate);
            if (list != null)
            {
                string lastMaterialLotNo = ((MaterialLot)list[list.Length - 1]).MaterialLotNo;

                if (lastMaterialLotNo.LastIndexOf("-") >= 0)
                {
                    string runningNumberString = lastMaterialLotNo.Substring(lastMaterialLotNo.LastIndexOf("-") + 1);
                    int runningNumber = -1;
                    if (int.TryParse(runningNumberString, out runningNumber))
                    {
                        runningNumber++;
                        returnValue = runningNumber.ToString("000");
                        returnValue = returnValue.Substring(returnValue.Length - 3);
                    }
                }
            }

            return returnValue;
        }

        public object[] QueryMaterialLotPrintLabel(string materialLotNo, string iqcNo, string itemCode, string vendor, int startDate, int endDate, string storageId)
        {
            string sql = string.Empty;
            sql += " SELECT a.*,b.vendorname VendorDesc,tblmaterial.mdesc ItemDesc  FROM tblmateriallot a,tblvendor b,tblmaterial ";
            sql += "  WHERE 1=1 AND a.vendorcode=b.vendorcode(+) and a.itemcode=tblmaterial.mcode(+)";

            if (materialLotNo.Trim().Length > 0)
            {
                sql += " AND materialLot like  '%" + materialLotNo + "%'  ";
            }

            if (iqcNo.Trim().Length > 0)
            {
                sql += " AND iqcNo like  '%" + iqcNo + "%' ";
            }

            if (itemCode.Trim().Length > 0)
            {
                sql += "  AND itemCode like '%" + itemCode + "%'";
            }
            if (vendor.Trim().Length > 0)
            {
                sql += "  AND a.vendorcode like '%" + vendor + "%'";
            }

            if (startDate > 0 && endDate > 0)
            {
                sql += "  AND createdate>= " + startDate + " AND createdate<=" + endDate + "";
            }

            if (storageId.Trim().Length > 0)
            {
                sql += "  AND STORAGEID like '%" + storageId + "%'";
            }
            sql += " ORDER BY  a.materialLot,a.stline";

            return this.DataProvider.CustomQuery(typeof(MaterialLotWithItemDesc), new SQLCondition(sql));
        }

        public object[] QuerySAPMaterialTransNotDealed(int maxCount)
        {
            string sql = "SELECT {0} FROM tblsapmaterialtrans WHERE flag = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SAPMaterialTrans)), FlagStatus.FlagStatus_MES);
            return this.DataProvider.CustomQuery(typeof(SAPMaterialTrans), new PagerCondition(sql, "materiallot,postseq", 1, maxCount));
        }

        public object[] QuerySAPMaterialTrans(string transactionCode)
        {
            string sql = "SELECT {0} FROM tblsapmaterialtrans WHERE transactioncode = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SAPMaterialTrans)), transactionCode);
            return this.DataProvider.CustomQuery(typeof(SAPMaterialTrans), new SQLCondition(sql));
        }

        public object[] QueryMaterialINVReceipt(string receiptno, string vendorcode, string rectype, int begdate, int enddate, bool check)
        {
            string sql = string.Empty;
            sql += " SELECT A.RECEIPTNO,A.RECEIPTLINE,A.IQCStatus, A.ITEMCODE, C.MDESC, C.MUOM UNIT, D.VENDORCODE, E.VENDORNAME, C.ROHS, A.QUALIFYQTY, ";
            sql += " (CASE C.MCONTROLTYPE WHEN 'item_control_lot' THEN A.ACTQTY WHEN  'item_control_keyparts' THEN A.ACTQTY WHEN 'item_control_nocontrol' THEN A.QUALIFYQTY ";
            sql += " END)AS ACTQTY, A.MEMO,C.MCONTROLTYPE,A.IQCSTATUS,D.RECTYPE,D.STORAGEID as MSTORAGE ";
            sql += " FROM TBLINVRECEIPTDETAIL A ";
            sql += " INNER JOIN TBLINVRECEIPT D ON A.RECEIPTNO = D.RECEIPTNO ";
            sql += " INNER JOIN TBLMATERIAL C ON A.ITEMCODE = C.MCODE AND D.ORGID = C.ORGID ";
            sql += " INNER JOIN TBLVENDOR E ON D.VENDORCODE = E.VENDORCODE ";
            sql += " WHERE a.RECSTATUS = 'WaitCheck' ";
            sql += " AND d.createdate between " + begdate + "and " + enddate + " ";
            if (receiptno.Trim() != string.Empty)
            {
                sql += "    AND d.receiptno = '" + receiptno.Trim() + "'  ";
            }

            if (vendorcode.Trim() != string.Empty)
            {
                sql += string.Format(" AND  d.vendorcode in ({0})", FormatHelper.ProcessQueryValues(vendorcode.Trim()));
            }

            if (rectype.Trim() != string.Empty)
            {
                sql += "    AND D.RECTYPE = '" + rectype.Trim() + "' ";
            }
            if (check)
            {
                sql += "  AND A.IQCStatus <> 'WaitCheck' and A.QualifyQTY <> 0";
            }

            sql += " ORDER BY A.RECEIPTNO,A.RECEIPTLINE";

            return this._domainDataProvider.CustomQuery(typeof(Domain.IQC.INVReceipt), new SQLCondition(sql));
        }

        //新增管控类型，状态，物料条件查询条件
        public object[] QueryMaterialINVReceipt(string receiptno, string vendorcode, string rectype, int begdate, int enddate, string mcode, string mcontroltype, string status)
        {
            string sql = string.Empty;
            sql += " SELECT A.RECEIPTNO,A.RECEIPTLINE,A.IQCStatus, A.ITEMCODE, A.RecStatus, C.MDESC, C.MUOM UNIT, D.VENDORCODE, E.VENDORNAME, C.ROHS, A.QUALIFYQTY, ";
            sql += " (CASE C.MCONTROLTYPE WHEN 'item_control_lot' THEN A.ACTQTY WHEN  'item_control_keyparts' THEN A.ACTQTY WHEN 'item_control_nocontrol' THEN A.QUALIFYQTY ";
            sql += " END)AS ACTQTY, A.MEMO,C.MCONTROLTYPE,A.IQCSTATUS,D.RECTYPE,D.STORAGEID as MSTORAGE ";
            sql += " FROM TBLINVRECEIPTDETAIL A ";
            sql += " INNER JOIN TBLINVRECEIPT D ON A.RECEIPTNO = D.RECEIPTNO ";
            sql += " INNER JOIN TBLMATERIAL C ON A.ITEMCODE = C.MCODE AND D.ORGID = C.ORGID ";
            sql += " INNER JOIN TBLVENDOR E ON D.VENDORCODE = E.VENDORCODE ";
            sql += " WHERE a.RECSTATUS IN ('WaitCheck','Close') ";
            //sql += " WHERE 1 = 1 ";
            sql += " AND d.createdate between " + begdate + "and " + enddate + " ";
            if (receiptno.Trim() != string.Empty)
            {
                sql += "    AND d.receiptno = '" + receiptno.Trim() + "'  ";
            }

            if (vendorcode.Trim() != string.Empty)
            {
                sql += string.Format(" AND  d.vendorcode in ({0})", FormatHelper.ProcessQueryValues(vendorcode.Trim()));
            }

            if (rectype.Trim() != string.Empty)
            {
                sql += "    AND D.RECTYPE = '" + rectype.Trim() + "' ";
            }
            if (mcode.Trim() != string.Empty)
            {
                if (mcode.Contains(","))
                {
                    sql += " and C.MCODE in (";
                    string[] mCode = mcode.Split(',');
                    foreach (string str in mCode)
                    {
                        sql += "'" + str + "',";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")";
                }
                else
                {
                    sql += " and C.MCODE like '" + mcode.Trim() + "%' ";
                }


            }
            if (mcontroltype.Trim() != string.Empty)
            {
                sql += " and c.mcontroltype= '" + mcontroltype.Trim() + "'";
            }
            if (status.Equals(InvReceiptDetailStatus.InvReceiptDetailStatus_AllowStorage))
            {
                sql += "  AND a.RECSTATUS = 'WaitCheck' ";
                sql += "  AND A.IQCStatus <> 'WaitCheck' and A.QualifyQTY <> 0";
            }
            else if (status.Equals(InvReceiptDetailStatus.InvReceiptDetailStatus_StorageOK))
            {
                sql += "  AND a.RECSTATUS = 'Close' ";
            }
            else if (status.Equals(InvReceiptDetailStatus.InvReceiptDetailStatus_WaitCheck))
            {
                sql += "  AND a.RECSTATUS = 'WaitCheck' ";
                sql += "  AND A.IQCStatus IN ('WaitCheck','UNQualified') ";
            }

            sql += " ORDER BY A.RECEIPTNO,A.RECEIPTLINE";

            return this._domainDataProvider.CustomQuery(typeof(Domain.IQC.INVReceipt), new SQLCondition(sql));
        }

        #endregion

        #region TBLMaterialTrans 物料收发交易记录表

        public MaterialTrans CreateNewMaterialTrans()
        {
            return new MaterialTrans();
        }

        public void AddMaterialTrans(MaterialTrans materialTrans)
        {
            this._helper.AddDomainObject(materialTrans);
        }
        #endregion

        #region SAPMaterialTrans
        public SAPMaterialTrans CreateNewSAPMaterialTrans()
        {
            return new SAPMaterialTrans();
        }

        public void AddSAPMaterialTrans(SAPMaterialTrans sapMaterialTrans)
        {
            this.DataProvider.Insert(sapMaterialTrans);
        }

        public void UpdateSAPMaterialTrans(SAPMaterialTrans sapMaterialTrans)
        {
            this.DataProvider.Update(sapMaterialTrans);
        }

        public void DeleteSAPMaterialTrans(SAPMaterialTrans sapMaterialTrans)
        {
            this.DataProvider.Delete(sapMaterialTrans);
        }

        public void DeleteSAPMaterialTrans(SAPMaterialTrans[] sapMaterialTrans)
        {
            this._helper.DeleteDomainObject(sapMaterialTrans);
        }

        public object GetSAPMaterialTrans(string materialLotNo, string postSeq)
        {
            return this.DataProvider.CustomSearch(typeof(SAPMaterialTrans), new object[] { materialLotNo, postSeq });
        }

        public int GetSAPMaterialTransMaxSeq(string materialLotNo)
        {
            try
            {
                int sql = this._domainDataProvider.GetCount(new SQLCondition(" SELECT MAX(postseq)  FROM TBLSAPMaterialTrans WHERE materiallot='" + materialLotNo.Trim().ToUpper() + "'"));
                return sql + 1;
            }
            catch
            {
                return 1;
            }
        }

        private string QuerySendMaterialSql(string itemCode, string vendorCode, int beginDate, int endDate, string flag)
        {
            string condition = string.Empty;

            if (itemCode != null && itemCode != string.Empty) //'{0}%'
            {

                condition += string.Format(" AND tblsapmaterialtrans.itemcode  in ({0})", FormatHelper.ProcessQueryValues(itemCode));

            }

            if (vendorCode != null && vendorCode != string.Empty)
            {
                condition += " AND  tblsapmaterialtrans.vendorcode LIKE '%" + vendorCode + "%'";
            }
            if (flag != null && flag != string.Empty)
            {
                condition += string.Format(" AND tblsapmaterialtrans.flag  = ({0})", FormatHelper.ProcessQueryValues(flag));
            }
            condition += FormatHelper.GetDateRangeSql("tblsapmaterialtrans.voucherdate", beginDate, endDate);
            condition += " AND tblsapmaterialtrans.itemcode=tblmaterial.mcode(+)";
            condition += " AND tblsapmaterialtrans.transactioncode = TBLRawIssue2SAP.transactioncode(+)";
            condition += " AND tblsapmaterialtrans.materiallot=tblmateriallot.materiallot(+)";

            string sql = string.Format(" FROM tblsapmaterialtrans,tblmaterial,tblrawIssue2sap,tblmateriallot WHERE 1=1 {0}  ", condition);
            return sql;

        }

        //bighai 2009/03/05
        public object[] QuerySendMaterial(string itemCode, string vendorCode, int beginDate, int endDate, string flag, int inclusive, int exclusive)
        {
            ArrayList paramList = new ArrayList();

            string sql = QuerySendMaterialSql(itemCode, vendorCode, beginDate, endDate, flag);
            string sqlHead = string.Format("SELECT  {0},tblmaterial.mdesc,tblrawIssue2sap.errormessage ",
DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(SAPMaterialTrans)));

            sql = sqlHead + sql;

            return this.DataProvider.CustomQuery(typeof(SAPMaterialTransDesc), new PagerParamCondition(
           sql, " tblsapmaterialtrans.materiallot,tblsapmaterialtrans.postseq",
            inclusive,
            exclusive,
            (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));

        }

        public int QuerySendMaterialCount(string itemCode, string vendorCode, int beginDate, int endDate, string flag)
        {
            ArrayList paramList = new ArrayList();
            string sql = QuerySendMaterialSql(itemCode, vendorCode, beginDate, endDate, flag);
            string sqlHead = "SELECT  COUNT(*) ";
            sql = sqlHead + sql;

            return this.DataProvider.GetCount(new SQLParamCondition(
                sql, (SQLParameter[])paramList.ToArray(typeof(SQLParameter))));

        }

        public void ManualSyncMaterialSendFlag(string materialLot, int postSeq, string userCode)
        {
            try
            {
                SAPMaterialTrans sapMaterialTrans = (SAPMaterialTrans)GetSAPMaterialTrans(materialLot, postSeq.ToString());

                if (sapMaterialTrans != null)
                {
                    sapMaterialTrans.Flag = FlagStatus.FlagStatus_SAP;
                    sapMaterialTrans.MaintainUser = userCode;
                    UpdateSAPMaterialTrans(sapMaterialTrans);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region MaterialReturn
        public MaterialReturn CreateNewMaterialReturn()
        {
            return new MaterialReturn();
        }

        public void AddMaterialReturn(MaterialReturn materialReturn)
        {
            this.DataProvider.Insert(materialReturn);
        }

        public void UpdateMaterialReturn(MaterialReturn materialReturn)
        {
            this.DataProvider.Update(materialReturn);
        }

        public void DeleteMaterialReturn(MaterialReturn materialReturn)
        {
            this.DataProvider.Delete(materialReturn);
        }

        public void DeleteMaterialReturn(MaterialReturn[] materialReturn)
        {
            this._helper.DeleteDomainObject(materialReturn);
        }

        public object GetMaterialReturn(string materialLotNo, string postSeq)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialReturn), new object[] { materialLotNo, postSeq });
        }


        public int GetMaterialReturnsMaxSeq(string materialLotNo)
        {
            try
            {
                int sql = this._domainDataProvider.GetCount(new SQLCondition(" SELECT MAX(postseq)  FROM TBLMaterialreturn WHERE materiallot='" + materialLotNo.Trim().ToUpper() + "'"));
                return sql + 1;
            }
            catch
            {
                return 1;
            }
        }

        #endregion

        #region GetMOPlanQtyActQty

        public MO GetMOPlanQtyActQty(string MOCode)
        {
            string sql = string.Format("select d.MOCode,d.moplanqty,count(a.rcard) moactqty from tblmo d," +
                            "  tblinvrcard a " +
                            " where a.mocode= d.mocode and a.mocode='{0}' group by d.mocode,d.moplanqty", MOCode);

            object[] objs = this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return objs[0] as MO;

            return null;
        }

        #endregion

        #region GetInventoryModel

        public object[] GetInventoryModel()
        {
            return DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.MOModel.Model), new SQLCondition(string.Format("select {0} from tblmodel where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and isinv='1' order by modelcode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.MOModel.Model)))));
        }

        #endregion

        #region DNTempOut
        public DNTempOut CreateDNTempOut()
        {
            return new DNTempOut();
        }

        public void AddDNTempOut(DNTempOut dnTempOut)
        {
            this.DataProvider.Insert(dnTempOut);
        }

        public void UpdateDNTempOut(DNTempOut dnTempOut)
        {
            this.DataProvider.Update(dnTempOut);
        }

        public void DeleteDNTempOut(DNTempOut dnTempOut)
        {
            this.DataProvider.Delete(dnTempOut);
        }

        public void DeleteDNTempOut(DNTempOut[] dnTempOut)
        {
            this._helper.DeleteDomainObject(dnTempOut);
        }

        public object GetDNTempOut(string stackCode, string itemCode, string DNNO, string DNLine)
        {
            return this.DataProvider.CustomSearch(typeof(DNTempOut), new object[] { stackCode, itemCode, DNNO, DNLine });
        }

        public object[] QueryDNTempOut(string stackCode, string itemCode, int inclusive, int exclusive)
        {
            string sql = " select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(DNTempOut)) + " from TBLDNTempOut where 1=1 ";

            if (stackCode.Trim() != string.Empty)
            {
                sql += " and  stackCode='" + stackCode.Trim().ToUpper() + "'";
            }

            if (itemCode.Trim() != string.Empty)
            {
                sql += " and  itemCode='" + itemCode.Trim().ToUpper() + "'";
            }

            sql += " order by DNNO,DNLine";

            return this.DataProvider.CustomQuery(typeof(DNTempOut), new PagerCondition(sql, inclusive, exclusive));
        }


        public int GetDNTempOutCount(string stackCode, string itemCode)
        {
            string sql = " select Count(*) from TBLDNTempOut where 1=1 ";

            if (stackCode.Trim() != string.Empty)
            {
                sql += " and  stackCode='" + stackCode.Trim().ToUpper() + "'";
            }

            if (itemCode.Trim() != string.Empty)
            {
                sql += " and  itemCode='" + itemCode.Trim().ToUpper() + "'";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryDNTempOut(string storageCode, string itemCode, string mmodelCode, string stackCode, int inclusive, int exclusive)
        {
            string sql = " SELECT A.* FROM (  ";
            sql += GetDNTempOutSql(storageCode, itemCode, mmodelCode, stackCode, string.Empty) + ") A ORDER BY STORAGECODE,COMQTY,STACKCODE,itemcode";

            return this.DataProvider.CustomQuery(typeof(DNTempOutMessage), new PagerCondition(sql, inclusive, exclusive));
        }

        public object[] QueryDNTempOut(string storageCode, string itemCode, string mmodelCode, string stackCode, string CompanyCode)
        {
            string sql = " SELECT A.* FROM (  ";
            sql += GetDNTempOutSql(storageCode, itemCode, mmodelCode, stackCode, CompanyCode) + ") A ORDER BY STORAGECODE,COMQTY,STACKCODE,itemcode";

            return this.DataProvider.CustomQuery(typeof(DNTempOutMessage), new SQLCondition(sql));
        }

        public int GetDNTempOutCount(string storageCode, string itemCode, string mmodelCode, string stackCode)
        {
            string sql = " SELECT COUNT(*) FROM (";
            sql += GetDNTempOutSql(storageCode, itemCode, mmodelCode, stackCode, string.Empty) + ")";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        private string GetDNTempOutSql(string storageCode, string itemCode, string mmodelCode, string stackCode, string CompanyCode)
        {
            string sql = " SELECT DISTINCT A.STORAGECODE,B.STORAGENAME,A.COMPANY,A.STACKCODE,A.ITEMCODE,C.MNAME,C.MMODELCODE,";
            sql += "  NVL(D.INVCOUNT, 0) AS INVQTY,NVL(D.COMQTY, 0) AS COMQTY,NVL(D.SAPINV, 0) AS SAPQTY,NVL(H.INVCOUNT, 0) AS TEMPQTY ";
            sql += "             FROM TBLSTACK2RCARD A ";
            sql += "   LEFT JOIN TBLSTORAGE B ON A.STORAGECODE = B.STORAGECODE ";
            sql += "   LEFT JOIN TBLMATERIAL C ON A.ITEMCODE = C.MCODE ";
            sql += "   LEFT JOIN  (SELECT   COUNT (1) AS invcount,";
            sql += "                         SUM";
            sql += "                            (CASE";
            sql += "                                WHEN GETRCARDSTATUSONTIME (serialno) LIKE '%完工%'";
            sql += "                                   THEN '1'";
            sql += "                                ELSE '0'";
            sql += "                             END";
            sql += "                             ) comqty,";
            sql += "                         SUM";
            sql += "                            (CASE";
            sql += "                                WHEN GETRCARDSTATUSONTIME (serialno) LIKE '%SAP完工%'";
            sql += "                                  THEN '1'";
            sql += "                               ELSE '0'";
            sql += "                            END";
            sql += "                           ) sapinv,";
            sql += "                       storagecode, stackcode, itemcode, company";
            sql += "                  FROM tblstack2rcard";
            sql += "              GROUP BY storagecode, stackcode, itemcode, company) d";
            sql += "              ON a.storagecode = d.storagecode";
            sql += "            AND a.stackcode = d.stackcode";
            sql += "            AND a.itemcode = d.itemcode";
            sql += "            AND a.company = d.company";
            sql += "    LEFT JOIN (SELECT SUM(T.TEMPQTY) INVCOUNT,K.STORAGECODE,T.STACKCODE,T.ITEMCODE  FROM TBLDNTEMPOUT T, TBLSTACK K";
            sql += "       WHERE T.STACKCODE = K.STACKCODE GROUP BY K.STORAGECODE, T.STACKCODE, T.ITEMCODE) H ";
            sql += "       ON A.STORAGECODE =H.STORAGECODE   AND A.STACKCODE =H.STACKCODE  AND A.ITEMCODE =H.ITEMCODE ";
            sql += "   WHERE 1=1 ";

            if (storageCode.Trim() != string.Empty)
            {
                sql += "  and A.STORAGECODE = '" + storageCode.Trim().ToUpper() + "'";
            }

            if (CompanyCode.Trim() != string.Empty)
            {
                sql += " AND a.COMPANY='" + CompanyCode.Trim() + "'";
            }

            if (itemCode.Trim() != string.Empty)
            {
                if (itemCode.IndexOf(",") >= 0)
                {
                    string[] list = itemCode.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }

                    itemCode = string.Join(",", list);

                    sql += "  and A.ITEMCODE in  (" + itemCode.Trim().ToUpper() + ")";
                }
                else
                {
                    sql += "  and A.ITEMCODE like '" + itemCode.Trim().ToUpper() + "%'";
                }
            }

            if (mmodelCode.Trim() != string.Empty)
            {
                if (mmodelCode.IndexOf(",") >= 0)
                {
                    string[] list = mmodelCode.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }

                    mmodelCode = string.Join(",", list);

                    sql += "  and C.MMODELCODE in  (" + mmodelCode.Trim().ToUpper() + ")";
                }
                else
                {
                    sql += "  and C.MMODELCODE like '" + mmodelCode.Trim().ToUpper() + "%'";
                }
            }


            if (stackCode.Trim() != string.Empty)
            {
                if (stackCode.IndexOf(",") >= 0)
                {
                    string[] list = stackCode.Split(',');
                    for (int i = 0; i < list.Length; i++)
                    {
                        list[i] = "'" + list[i] + "'";
                    }

                    stackCode = string.Join(",", list);

                    sql += "  and A.STACKCODE in  (" + stackCode.Trim().ToUpper() + ")";
                }
                else
                {
                    sql += "  and A.STACKCODE like '" + stackCode.Trim().ToUpper() + "%'";
                }
            }

            return sql;
        }

        #endregion

        #region 物料入库(TBLMaterialLot,TBLMaterialTrans,TBLSapmaterialtrans)
        public bool MaterialStockIn(MaterialLot materialLot, MaterialBusiness materialBusiness, MaterialTrans materialTrans, SAPMaterialTrans sAPMaterialTrans)
        {
            bool returnValue = false;
            try
            {
                this.DataProvider.BeginTransaction();

                //Insert TBLMaterialLot
                this.AddMaterialLot(materialLot);

                //Insert TBLMaterialTrans
                this.AddMaterialTrans(materialTrans);

                //Insert TBLSapmaterialtrans(option)
                if (materialBusiness.SAPCODE.Trim().Length > 0)
                {
                    this.AddSAPMaterialTrans(sAPMaterialTrans);
                }

                this.DataProvider.CommitTransaction();
                returnValue = true;
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw new Exception(ex.ToString());
            }
            return returnValue;
        }

        public string GetMaterialLotDateCode(int date)
        {
            string returnValue = string.Empty;
            string dateString = date.ToString("00000000");

            returnValue += dateString.Substring(2, 2);
            returnValue += NumberScaleHelper.ChangeNumber(dateString.Substring(4, 2), NumberScale.Scale10, NumberScale.Scale16);
            returnValue += NumberScaleHelper.ChangeNumber(dateString.Substring(6, 2), NumberScale.Scale10, NumberScale.Scale36);

            return returnValue;
        }

        #endregion

        #region InvIntransSum
        /// <summary>
        /// TBLINVINTRANSSUM
        /// </summary>        
        public InvIntransSum CreateInvIntransSum()
        {
            return new InvIntransSum();
        }

        public void AddInvIntransSum(InvIntransSum invintranssum)
        {
            this.DataProvider.Insert(invintranssum);
        }

        public void DeleteInvIntransSum(InvIntransSum invintranssum)
        {
            this.DataProvider.Delete(invintranssum);
        }

        public void UpdateInvIntransSum(InvIntransSum invintranssum)
        {
            this.DataProvider.Update(invintranssum);
        }
        #endregion

        #region 物料齐套查询
        public object[] QueryMaterialFullMcode(string mCode, string storageCode, int planSerial, string modelCode)
        {
            string sql = "SELECT   a.*, nvl(b.qty,0) as qty, nvl(b.qty,0) - a.sumqty AS shortqty,c.mdesc ";
            sql += " FROM (SELECT c.mcode, c.mocode, c.plandate, c.planqty,DECODE(c.plantype,1,'MO',2,'PLAN') as plantype,c.lostqty, ";
            sql += " SUM (c.lostqty) OVER (PARTITION BY c.mcode ORDER BY c.mcode,c.plantype,";
            sql += " c.plandate, c.mocode ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS sumqty,c.itemcode,c.mdesc AS modesc ";
            sql += " FROM ( SELECT '1' as plantype, b.mobitemcode AS mcode, a.mocode, ";
            sql += " a.moplanstartdate AS plandate,";
            sql += "            a.moplanqty - a.moinputqty + a.offmoqty AS planqty, ";
            sql += "         (a.moplanqty - a.moinputqty + a.offmoqty)* (b.mobitemqty) AS lostqty,a.itemcode,c.mdesc ";
            //sql += "         (a.moplanqty - a.moinputqty + a.offmoqty)* (b.mobitemqty / a.moplanqty) AS lostqty,a.itemcode,c.mdesc ";
            sql += "     FROM tblmo a, tblmobom b,tblmaterial c,TBLMODEL2ITEM D ";
            sql += "   WHERE a.mostatus IN ('mostatus_release', 'mostatus_open') ";
            sql += "      AND a.mocode = b.mocode AND a.itemcode=c.mcode and a.orgid= c.orgid";
            sql += "      AND a.ITEMCODE = d.ITEMCODE AND a.ORGID = d.Orgid ";
            sql += "      AND a.moplanqty - a.moinputqty + a.offmoqty <> 0 ";
            sql += "      AND b.mobitemcode IN ({0})";
            if (modelCode.Trim() != string.Empty)
            {
                sql += " AND  d.modelcode in (" + FormatHelper.ProcessQueryValues(modelCode) + ") ";
            }

            sql += " ) c) a ";
            sql += " LEFT JOIN (SELECT mcode, SUM (storageqty) AS qty FROM tblstorageinfo a  WHERE a.storageid IN({1}) ";
            sql += " GROUP BY mcode) b ON a.mcode = b.mcode LEFT JOIN tblmaterial c ON  a.mcode = c.mcode ORDER BY a.mcode,a.plantype,a.plandate, a.mocode ";
            sql = string.Format(sql, FormatHelper.ProcessQueryValues(mCode), FormatHelper.ProcessQueryValues(storageCode));
            object[] obj = this.DataProvider.CustomQuery(typeof(MaterialFull), new SQLCondition(sql));
            if (obj != null && obj.Length > 0)
            {
                return obj;
            }
            return null;
        }

        public object[] QueryMaterialFullMo(string storageCode, string modelCode)
        {
            string sql = "SELECT   a.*, nvl(b.qty,0) as qty, nvl(b.qty,0) - a.sumqty AS shortqty, c.mdesc ";
            sql += " FROM (SELECT c.mocode, c.plandate,c.mcode,  c.planqty,DECODE(c.plantype,1,'MO',2,'PLAN') as plantype,c.lostqty, ";
            sql += " SUM (c.lostqty) OVER (PARTITION BY c.mcode ORDER BY c.mcode,c.plantype, ";
            sql += " c.plandate, c.mocode ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS sumqty,c.itemcode,c.mdesc AS modesc ";
            sql += " FROM ( SELECT '1' as plantype, b.mobitemcode AS mcode, a.mocode, ";
            sql += "  a.moplanstartdate AS plandate, ";
            sql += "  a.moplanqty - a.moinputqty + a.offmoqty AS planqty, ";
            sql += "   (a.moplanqty - a.moinputqty + a.offmoqty)* (b.mobitemqty) AS lostqty,a.itemcode,c.mdesc ";
            //sql += "   (a.moplanqty - a.moinputqty + a.offmoqty)* (b.mobitemqty / a.moplanqty) AS lostqty,a.itemcode,c.mdesc ";
            sql += "  FROM tblmo a, tblmobom b, tblmaterial c ,TBLMODEL2ITEM D ";
            sql += "  WHERE a.mostatus IN ('mostatus_release', 'mostatus_open') ";
            sql += "  AND a.ITEMCODE = d.ITEMCODE AND a.ORGID = d.Orgid ";
            sql += "  AND a.mocode = b.mocode AND a.itemcode=c.mcode and a.orgid= c.orgid AND a.moplanqty - a.moinputqty + a.offmoqty <> 0 ";
            if (modelCode.Trim() != string.Empty)
            {
                sql += " AND  D.modelcode in (" + FormatHelper.ProcessQueryValues(modelCode) + ")";
            }

            sql += " ) c) a ";
            sql += "  LEFT JOIN (SELECT   mcode, SUM (storageqty) AS qty FROM tblstorageinfo a  WHERE a.storageid IN({0}) ";
            sql += "  GROUP BY mcode) b ON a.mcode = b.mcode LEFT JOIN tblmaterial c ON  a.mcode = c.mcode  WHERE nvl(b.qty,0) - a.sumqty <=0  ORDER BY  a.plantype,a.mocode,a.plandate,a.mcode ";
            sql = string.Format(sql, FormatHelper.ProcessQueryValues(storageCode));
            object[] obj = this.DataProvider.CustomQuery(typeof(MaterialFull), new SQLCondition(sql));
            if (obj != null && obj.Length > 0)
            {
                return obj;
            }
            return null;
        }

        #endregion

        //Add by terry 2011-06-19
        #region MSDLevel

        public MSDLevel CreateMSDLevel()
        {
            return new MSDLevel();
        }

        public void AddMSDLevel(MSDLevel msdlevel)
        {
            this._helper.AddDomainObject(msdlevel);
        }

        public void UpdateMSDLevel(MSDLevel msdlevel)
        {
            this._helper.UpdateDomainObject(msdlevel);
        }

        public void DeleteMSDLevel(MSDLevel msdlevel)
        {
            this._helper.DeleteDomainObject(msdlevel);
        }

        public void DeleteMSDLevel(MSDLevel[] msdlevel)
        {
            this._helper.DeleteDomainObject(msdlevel);
        }

        public object GetMSDLevel(string mhumiditylevel)
        {
            return this.DataProvider.CustomSearch(typeof(MSDLevel), new object[] { mhumiditylevel });
        }

        public object[] QueryMSDLevel(string mhumiditylevel, string mhumidityleveldesc, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            sql = string.Format("select {0} from TBLMSDLevel where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MSDLevel)));

            if (mhumiditylevel != null && mhumiditylevel.Length > 0)
            {
                sql = string.Format("{0} AND MHumidityLevel like '{1}%'", sql, mhumiditylevel);
            }

            if (mhumidityleveldesc != null && mhumidityleveldesc.Length > 0)
            {
                sql = string.Format("{0} AND MHumidityLevelDesc like '%{1}%'", sql, mhumidityleveldesc);
            }

            return this.DataProvider.CustomQuery(typeof(MSDLevel), new PagerCondition(sql, "MHumidityLevel", inclusive, exclusive));
        }

        public int QueryMSDLevelCount(string mhumiditylevel, string mhumidityleveldesc)
        {
            string sql = string.Empty;

            sql = string.Format("select count(*)  from TBLMSDLevel where 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MSDLevel)));

            if (mhumiditylevel != null && mhumiditylevel.Length > 0)
            {
                sql = string.Format("{0} AND MHumidityLevel like '{1}%'", sql, mhumiditylevel);
            }

            if (mhumidityleveldesc != null && mhumidityleveldesc.Length > 0)
            {
                sql = string.Format("{0} AND MHumidityLevelDesc like '%{1}%'", sql, mhumidityleveldesc);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region 生产型入库
        #region StorageInfo

        public void AddStorageInfo(StorageInfo storageinfo)
        {
            this.DataProvider.Insert(storageinfo);
        }

        public void UpdateStorageInfo(StorageInfo storageinfo)
        {
            this.DataProvider.Update(storageinfo);
        }

        public void DeleteStorageInfo(StorageInfo storageinfo)
        {
            this.DataProvider.Delete(storageinfo);
        }

        public void DeleteStorageInfo(StorageInfo[] storageinfo)
        {
            this._helper.DeleteDomainObject(storageinfo);
        }

        public object GetStorageInfo(string StorageID, string materialCode, string stackCode)
        {
            return this.DataProvider.CustomSearch(typeof(StorageInfo), new object[] { StorageID, materialCode, stackCode });
        }

        public object[] QueryStorageInfo(string Storageid, string Stackcode)
        {
            string sql = "select * FROM TBLSTORAGEINFO A WHERE A.STORAGEID='" + Storageid + "'AND A.STACKCODE='" + Stackcode + "'";
            object[] returnObjects = this._domainDataProvider.CustomQuery(typeof(StorageInfo), new SQLCondition(sql));
            if (returnObjects != null && returnObjects.Length > 0)
            {
                return returnObjects;
            }
            return null;
        }

        public object[] QueryStorageInfoByIDAndMCode(string Storageid, string mCode)
        {
            string sql = "select * FROM TBLSTORAGEINFO A WHERE A.STORAGEID='" + Storageid + "'AND A.MCODE='" + mCode + "' order by A.StackCODE";
            object[] returnObjects = this._domainDataProvider.CustomQuery(typeof(StorageInfo), new SQLCondition(sql));
            if (returnObjects != null && returnObjects.Length > 0)
            {
                return returnObjects;
            }
            return null;
        }

        //Jarvis 20120321 扣料获取物料在库的记录
        public object[] QueryStorageInfoByIDAndMCode(string mCode)
        {
            string sql = "select * FROM TBLSTORAGEINFO A WHERE A.MCODE='" + mCode + "' order by A.StackCODE";
            object[] returnObjects = this._domainDataProvider.CustomQuery(typeof(StorageInfo), new SQLCondition(sql));
            if (returnObjects != null && returnObjects.Length > 0)
            {
                return returnObjects;
            }
            return null;
        }

        public object[] QueryStorageLotInfo(string Storageid, string Stackcode, string mCode)
        {
            string sql = "select * FROM TBLSTORAGELotINFO A WHERE A.STORAGEID='" + Storageid + "'AND A.MCODE='" + mCode + "' AND A.STACKCODE='" + Stackcode + "'" + " order by A.LotNo";
            object[] returnObjects = this._domainDataProvider.CustomQuery(typeof(StorageLotInfo), new SQLCondition(sql));
            if (returnObjects != null && returnObjects.Length > 0)
            {
                return returnObjects;
            }
            return null;
        }

        public decimal QueryStorageLotInfoQty(string Storageid, string mCode)
        {
            string sql = "select * FROM TBLSTORAGELotINFO A WHERE A.STORAGEID='" + Storageid + "'AND A.MCODE='" + mCode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageLotInfo), new SQLCondition(sql));

            decimal qty = 0;
            if (objs != null)
            {
                foreach (StorageLotInfo info in objs)
                {
                    qty += info.Lotqty;
                }
            }
            return qty;
        }

        public decimal QueryStorageLotInfoQty(string Storageid, string stackcode, string mCode)
        {
            string sql = "select * FROM TBLSTORAGELotINFO A WHERE A.STORAGEID='" + Storageid + "' AND A.STACKCODE='" + stackcode + "'   AND A.MCODE='" + mCode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageLotInfo), new SQLCondition(sql));

            decimal qty = 0;
            if (objs != null)
            {
                foreach (StorageLotInfo info in objs)
                {
                    qty += info.Lotqty;
                }
            }
            return qty;
        }

        //public string GetStorageInfoStackCode(string Storageid, string mCode)
        //{
        //    string sql = "select * FROM TBLSTORAGEINFO A WHERE A.STORAGEID='" + Storageid + "'AND A.MCODE='" + mCode + "'AND STORAGEQTY>0 order by A.StackCODE";
        //    object[] returnObjects = this._domainDataProvider.CustomQuery(typeof(StorageInfo), new SQLCondition(sql));
        //    if (returnObjects != null && returnObjects.Length > 0)
        //    {
        //        return ((StorageInfo)returnObjects[0]).Stackcode;
        //    }
        //    return null;
        //}

        public decimal QueryStorageLotInfoLotQty(string lotNo, string mCode)
        {
            string sql = "select * FROM TBLSTORAGELotINFO A WHERE A.LOTNO='" + lotNo + "'AND A.MCODE='" + mCode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageLotInfo), new SQLCondition(sql));

            if (objs != null)
            {
                return ((StorageLotInfo)objs[0]).Lotqty;
            }
            return 0;
        }
        #endregion

        #region StorageLotInfo

        public void AddStorageLotInfo(StorageLotInfo storagelotinfo)
        {
            this.DataProvider.Insert(storagelotinfo);
        }

        public void UpdateStorageLotInfo(StorageLotInfo storagelotinfo)
        {
            this.DataProvider.Update(storagelotinfo);
        }

        public void DeleteStorageLotInfo(StorageLotInfo storagelotinfo)
        {
            this.DataProvider.Delete(storagelotinfo);
        }

        public void DeleteStorageLotInfo(StorageLotInfo[] storagelotinfo)
        {
            this._helper.DeleteDomainObject(storagelotinfo);
        }

        public object GetStorageLotInfo(string lotNO, string StorageID, string stackCode, string materialCode)
        {
            return this.DataProvider.CustomSearch(typeof(StorageLotInfo), new object[] { lotNO, StorageID, stackCode, materialCode });
        }

        public decimal GetStorageQty(string storageid, string mcode)
        {
            string sql = "select * FROM TBLStorageInfo A WHERE A.STORAGEID='" + storageid + "'AND A.mcode='" + mcode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageInfo), new SQLCondition(sql));

            decimal qty = 0;
            if (objs != null)
            {
                foreach (StorageInfo info in objs)
                {
                    qty += info.Storageqty;
                }
            }
            return qty;
        }

        //汇总物料的库存数量，Jarvis 20120321
        public decimal GetStorageQty(string mcode)
        {
            string sql = "select * FROM TBLStorageInfo A WHERE A.mcode='" + mcode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageInfo), new SQLCondition(sql));

            decimal qty = 0;
            if (objs != null)
            {
                foreach (StorageInfo info in objs)
                {
                    qty += info.Storageqty;
                }
            }
            return qty;
        }

        public decimal QueryStorageLotQty(string lotNO, string storageid)
        {
            string sql = "select * FROM TBLStorageLotInfo A WHERE A.STORAGEID='" + storageid + "'AND A.lotNO='" + lotNO + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageLotInfo), new SQLCondition(sql));

            decimal qty = 0;
            if (objs != null)
            {
                foreach (StorageLotInfo info in objs)
                {
                    qty += info.Lotqty;
                }
            }
            return qty;
        }

        public object[] QueryStorageLot(string lotNO, string storageid, string mcode)
        {
            string sql = "select * FROM TBLStorageLotInfo A WHERE A.STORAGEID='" + storageid + "'AND A.lotNO='" + lotNO + "' AND A.MCODE='" + mcode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageLotInfo), new SQLCondition(sql));

            return objs;
        }

        //获取批的库存信息，Jarvis 20120321
        public object[] QueryStorageLot(string lotNO, string mcode)
        {
            string sql = "select * FROM TBLStorageLotInfo A WHERE A.lotNO='" + lotNO + "' AND A.MCODE='" + mcode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(StorageLotInfo), new SQLCondition(sql));

            return objs;
        }

        #endregion

        #region ItemTrans
        public void AddItemTrans(ItemTrans itemTrans)
        {
            this.DataProvider.Insert(itemTrans);
        }

        public void UpdateItemTrans(ItemTrans itemTrans)
        {
            this.DataProvider.Update(itemTrans);
        }

        public void DeleteItemTrans(ItemTrans itemTrans)
        {
            this.DataProvider.Delete(itemTrans);
        }

        public void DeleteItemTrans(ItemTrans[] itemTrans)
        {
            this._helper.DeleteDomainObject(itemTrans);
        }

        public object GetItemTrans(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(ItemTrans), new object[] { serial });
        }

        public int Maxserial(string TRANSNO)
        {
            //string sql = "select decode(max(serial),'',0,max(serial))+1 as serial from TBLITEMTRANS";
            string sql = "select * from TBLITEMTRANS where TRANSNO='" + TRANSNO + "' order by TRANSNO desc, mdate desc,mtime desc";
            object[] obj = this.DataProvider.CustomQuery(typeof(ItemTrans), new SQLCondition(sql));
            return ((ItemTrans)obj[0]).Serial;
        }

        public object[] GetItemTransForInvTransfer(string transferNO, string itemCode, int transferLine)
        {
            string sql = "SELECT A.ITEMCODE, A.TRANSLINE, A.SERIAL, A.FRMSTACKCODE AS STACKCODE, B.LOTNO, (SELECT TBLSTORAGELOTINFO.LOTQTY ";
            sql += " FROM TBLSTORAGELOTINFO WHERE TBLSTORAGELOTINFO.STORAGEID = A.FRMSTORAGEID AND TBLSTORAGELOTINFO.STACKCODE = A.FRMSTACKCODE ";
            sql += "AND TBLSTORAGELOTINFO.MCODE = A.ITEMCODE AND TBLSTORAGELOTINFO.LOTNO= B.LOTNO) AS FRMSTORAGEQTY,B.TRANSQTY FROM TBLITEMTRANS A ";
            sql += "LEFT JOIN TBLITEMTRANSLOT B ON A.SERIAL = B.TBLITEMTRANS_SERIAL WHERE 1=1 ";
            if (transferNO.Trim() != string.Empty)
            {
                sql += " AND A.TRANSNO = '" + transferNO + "'";
            }
            if (transferLine != -1)
            {
                sql += " AND A.TRANSLINE='" + transferLine + "'";
            }
            if (itemCode.Trim() != string.Empty)
            {
                sql += " AND A.ITEMCODE = '" + itemCode + "'";
            }
            object[] obj = this.DataProvider.CustomQuery(typeof(ItemTransLotForTrans), new SQLCondition(sql));
            if (obj != null)
            {
                return obj;
            }
            return null;
        }

        #endregion

        #region ItemTransLot
        public void AddItemTransLot(ItemTransLot itemTransLot)
        {
            this.DataProvider.Insert(itemTransLot);
        }

        public void UpdateItemTransLot(ItemTransLot itemTransLot)
        {
            this.DataProvider.Update(itemTransLot);
        }

        public void DeleteItemTransLot(ItemTransLot itemTransLot)
        {
            this.DataProvider.Delete(itemTransLot);
        }

        public void DeleteItemTransLot(ItemTransLot[] itemTransLot)
        {
            this._helper.DeleteDomainObject(itemTransLot);
        }

        public object GetItemTransLot(int serial, string lotno)
        {
            return this.DataProvider.CustomSearch(typeof(ItemTransLot), new object[] { serial, lotno });
        }


        #endregion

        #region ItemTransLotDetail
        public void AddItemTransLotDetail(ItemTransLotDetail itemTransLotDetail)
        {
            this.DataProvider.Insert(itemTransLotDetail);
        }

        public void UpdateItemTransLotDetail(ItemTransLotDetail itemTransLotDetail)
        {
            this.DataProvider.Update(itemTransLotDetail);
        }

        public void DeleteItemTransLotDetail(ItemTransLotDetail itemTransLotDetail)
        {
            this.DataProvider.Delete(itemTransLotDetail);
        }

        public void DeleteItemTransLotDetail(ItemTransLotDetail[] itemTransLotDetail)
        {
            this._helper.DeleteDomainObject(itemTransLotDetail);
        }

        public object GetItemTransLotDetail(int serial, string lotNO, string serialNO)
        {
            return this.DataProvider.CustomSearch(typeof(ItemTransLotDetail), new object[] { serial, lotNO, serialNO });
        }

        public object[] QueryItemTransLotDetai(string serial, string lotNO)
        {
            string sql = "SELECT * FROM TBLITEMTRANSLOTDETAIL A where A.TBLITEMTrans_Serial = " + serial + " And A.lotNO = '" + lotNO + "'";

            object[] obj = this.DataProvider.CustomQuery(typeof(ItemTransLotDetail), new SQLCondition(sql));
            if (obj != null)
            {
                return obj;
            }
            return null;
        }

        public object[] GetItemTransLotDetailForInvTransfer(string transferNO, string itemCode)
        {
            string sql = "SELECT B.FRMSTACKCODE AS STACKCODE, A.SERIALNO,A.ITEMCODE FROM TBLITEMTRANSLOTDETAIL A LEFT JOIN TBLITEMTRANS B ";
            sql += " ON A.TBLITEMTRANS_SERIAL = B.SERIAL WHERE 1=1 ";
            if (transferNO.Trim() != string.Empty)
            {
                sql += " AND B.TRANSNO ='" + transferNO + "'";
            }
            if (itemCode.Trim() != string.Empty)
            {
                sql += " AND B.ITEMCODE='" + itemCode + "'";
            }
            object[] obj = this.DataProvider.CustomQuery(typeof(ItemTransLotDetailForTrans), new SQLCondition(sql));
            if (obj != null)
            {
                return obj;
            }
            return null;
        }

        #endregion

        #region ItemLot
        public void AddItemLot(ItemLot itemLot)
        {
            this.DataProvider.Insert(itemLot);
        }

        public void UpdateItemLot(ItemLot itemLot)
        {
            this.DataProvider.Update(itemLot);
        }

        public void DeleteItemLot(ItemLot itemLot)
        {
            this.DataProvider.Delete(itemLot);
        }

        public void DeleteItemLot(ItemLot[] itemLot)
        {
            this._helper.DeleteDomainObject(itemLot);
        }

        public object GetItemLot(string lotNO, string materialCode)
        {
            return this.DataProvider.CustomSearch(typeof(ItemLot), new object[] { lotNO, materialCode });
        }



        //public int GetItemLotExdate(string MCode, int OrgId)
        //{
        //    string sql = "SELECT MShelfLife FROM TBLMATERIAL WHERE 1=1";
        //    sql += string.Format(" AND MCODE='{0}' AND ORGID={1}", MCode, OrgId);
        //    object[] obj = this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
        //    if (obj != null)
        //    {
        //        return ((Domain.MOModel.Material)obj[0]).MShelfLife;
        //    }
        //    return 0;
        //}

        public object GetItemLotDesc(string lotNo)
        {
            string sql = "SELECT A.LOTNO, A.MCODE, B.MNAME, B.MDESC  FROM TBLITEMLOT A";
            sql += " LEFT JOIN TBLMATERIAL B ON A.MCODE = B.MCODE   AND A.ORGID = B.ORGID";
            sql += " WHERE A.LOTNO = '" + lotNo.Trim().ToUpper() + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotForQuery), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public object QueryLastItemLotByLotNo(string lotno)
        {
            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemLot)) + " FROM TBLITEMLot WHERE lotno='" + lotno + "' ";
            sql += " ORDER BY  mdate desc,mtime desc";

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLot), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public object QueryLastItemLotDetailByLotNo(string lotno)
        {
            string sql = "SELECT A.muser, A.lastprintuser, A.mtime, A.serialstatus, A.mdate, A.lastprinttime, A.mcode, A.storageid, A.stackcode, A.lastprintdate, A.serialno, A.printtimes, A.lotno FROM TBLITEMLOTDETAIL A";
            sql += " INNER JOIN TBLITEMLot B ON A.LOTNO = B.LOTNO AND A.MCODE = B.MCODE";
            sql += " WHERE A.lotno='" + lotno + "' ";
            sql += " ORDER BY a.MDATE desc, a.MTIME desc";

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public object QueryItemLotByBarCode(string barCode)
        {
            string sql = "SELECT b.mcode , b.VENDORCODE,b.DATECODE,b.VENDORITEMCODE,b.lotno FROM TBLITEMLotDetail a";
            sql += " INNER JOIN TBLITEMLot b";
            sql += " ON a.lotno=b.lotno";
            sql += " AND a.mcode=b.mcode";
            sql += " WHERE a.serialno='" + barCode + "'";
            sql += " ORDER BY a.MDATE desc, a.MTIME desc";
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLot), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public object QuerySumLotQty(string trnasno, int tranline)
        {
            string sql = "SELECT SUM(A.LOTQTY) AS LOTQTY FROM TBLITEMLOT A ";
            sql += " WHERE A.TRANSNO='" + trnasno + "' ";
            sql += " AND A.TRANSLINE = " + tranline + "";

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLot), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public object[] QueryItemLot(string mcode, string transno, int tranline, int orgid)
        {
            string sql = "SELECT * FROM TBLITEMLOT A WHERE 1=1";

            if (mcode.Trim() != string.Empty)
            {
                sql += " and  A.MCODE='" + mcode + "' ";
            }

            if (transno.Trim() != string.Empty)
            {
                sql += " AnD A.TRANSNO='" + transno + "' ";

            }

            if (tranline > -1)
            {
                sql += " AND A.TRANSLINE = " + tranline + "";
            }

            if (orgid > -1)
            {
                sql += " And A.ORGID = " + orgid;
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLot), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            return null;
        }

        public object QueryItemLotByStorageSend(string lotno, string storageId)
        {
            string sql = "SELECT DISTINCT A.* ,B.STACKCODE, C.LOTQTY AS FRMSTORAGEQTY FROM TBLITEMLOT A LEFT JOIN TBLITEMLOTDETAIL B ON ";
            sql += " A.LOTNO=B.LOTNO AND A.MCODE = B.MCODE LEFT JOIN TBLSTORAGELOTINFO C ON C.STORAGEID = B.STORAGEID ";
            sql += " AND C.STACKCODE = B.STACKCODE  AND C.MCODE = B.MCODE AND C.LOTNO= B.LOTNO WHERE 1=1 ";
            if (lotno.Trim() != string.Empty)
            {
                sql += " AND C.STORAGEID = '" + storageId + "'";
            }
            if (lotno.Trim() != string.Empty)
            {
                sql += " AND A.LOTNO = '" + lotno + "'";
            }
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotForTrans), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public object[] QueryItemLotByStorage(string lotno, string storgid)
        {
            string sql = "SELECT A.*,B.LOTQTY AS FRMSTORAGEQTY,B.STACKCODE  FROM  TBLITEMLOT A, TBLSTORAGELOTINFO B ";
            sql += " WHERE A.LOTNO=B.LOTNO AND A.MCODE = B.MCODE AND B.STORAGEID = '{0}' AND  A.LOTNO='{1}' ";
            sql = string.Format(sql, storgid, lotno);
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotForTrans), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            return null;
        }

        //add by kathy @20140626 查询批次料是否已备过料
        public int QueryMINNOCountByItemLot(string itemcode, string lotno)
        {
            string sql = string.Format("select count(*) from tblMINNO where itemcode='{0}' and lotno='{1}'",
                itemcode, lotno);
            return DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryItemLotForTrans(string storageid, string itemCode, string vendorCode, int bufferDate, int currentDate, string lotNo)
        {
            string sql = string.Empty;
            sql += "select b.* from tblstoragelotinfo a,tblitemlot b WHERE a.lotno=b.lotno AND a.mcode = b.mcode AND a.lotqty<>0 ";
            if (currentDate > 0)
            {
                sql += " AND b.exdate >=" + currentDate + " ";
            }
            if (bufferDate > 0)
            {
                sql += "AND b.datecode < " + bufferDate + " ";
            }
            if (vendorCode.Trim() != string.Empty)
            {
                sql += "AND b.vendorcode = '" + vendorCode.Trim().ToUpper() + "' ";
            }
            if (itemCode.Trim() != string.Empty)
            {
                sql += "AND a.mcode = '" + itemCode.Trim().ToUpper() + "' ";
            }
            if (storageid.Trim() != string.Empty)
            {
                sql += "AND a.storageid = '" + storageid.Trim().ToUpper() + "' ";
            }
            if (lotNo.Trim() != string.Empty)
            {
                sql += "AND a.lotno <> '" + lotNo.Trim().ToUpper() + "' ";
            }

            sql += " ORDER BY b.mcode ASC, b.datecode ASC, b.lotno ASC ";

            return this._domainDataProvider.CustomQuery(typeof(ItemLot), new SQLCondition(sql));
        }

        #endregion

        #region ITEMLotDetail
        public void AddItemLotDetail(ItemLotDetail itemLotDetail)
        {
            this.DataProvider.Insert(itemLotDetail);
        }

        public void UpdateItemLotDetail(ItemLotDetail itemLotDetail)
        {
            this.DataProvider.Update(itemLotDetail);
        }

        public void DeleteItemLotDetail(ItemLotDetail itemLotDetail)
        {
            this.DataProvider.Delete(itemLotDetail);
        }

        public void DeleteItemLotDetail(ItemLotDetail[] itemLotDetail)
        {
            this._helper.DeleteDomainObject(itemLotDetail);
        }

        public object GetItemLotDetail(string serialNO, string materialCode)
        {
            return this.DataProvider.CustomSearch(typeof(ItemLotDetail), new object[] { serialNO, materialCode });
        }

        public int GetItemLotDetailCount(string Rcard, string MCode)
        {
            string sql = string.Format("SELECT COUNT(*) FROM TBLITEMLOTDETAIL WHERE SERIALNO='{0}' AND MCODE='{1}'", Rcard, MCode);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetITEMLOTDETAILByLotno(string lotNo, string MCODE)
        {
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemLotDetail)) + " from TBLITEMLOTDETAIL where 1=1 ");

            if (lotNo != null && lotNo.Length != 0)
            {
                sql = string.Format("{0} and lotno = '{1}'", sql, lotNo);
            }

            if ((MCODE != null) && (MCODE.Trim() != string.Empty))
            {
                sql = String.Format(" {0} and MCODE = '{1}'", sql, MCODE);
            }

            return this.DataProvider.CustomQuery(typeof(ItemLotDetail), new SQLCondition(sql));
        }

        public object QueryITEMLOTDETAIL(string SERIALNO, string MCODE)
        {
            return this.DataProvider.CustomSearch(typeof(ItemLotDetail), new object[] { SERIALNO, MCODE });
        }

        public object[] QueryItemLotDetail(string lotno, string mcode, int orgid)
        {
            string sql = "SELECT itemlotdetail.* FROM TBLITEMLOT itemlot LEFT JOIN TBLITEMLOTDETAIL itemlotdetail ON itemlot.LOTNO = itemlotdetail.LOTNO AND itemlot.MCODE = itemlotdetail.MCODE WHERE itemlot.ORGID = " + orgid;//GlobalVariables.CurrentOrganizations.First().OrganizationID;
            sql += " and itemlotdetail.lotno = '" + lotno + "'";
            sql += " and itemlotdetail.mcode = '" + mcode + "'";

            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            return null;
        }

        public int QueryItemLotDetailCount(string lotno, string mcode, int orgid)
        {
            string sql = "SELECT count(*) FROM TBLITEMLOT itemlot LEFT JOIN TBLITEMLOTDETAIL itemlotdetail ON itemlot.LOTNO = itemlotdetail.LOTNO AND itemlot.MCODE = itemlotdetail.MCODE WHERE itemlot.ORGID = " + orgid;//GlobalVariables.CurrentOrganizations.First().OrganizationID;
            sql += " and itemlotdetail.lotno = '" + lotno + "'";
            sql += " and itemlotdetail.mcode = '" + mcode + "'";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object QueryItemLotDetailByStorage(string serialNo, string itemCode)
        {
            string sql = " SELECT A.* ,B.LOTQTY,(SELECT TBLSTORAGELOTINFO.LOTQTY FROM TBLSTORAGELOTINFO WHERE TBLSTORAGELOTINFO.STORAGEID = A.STORAGEID ";
            sql += " AND TBLSTORAGELOTINFO.STACKCODE = A.STACKCODE AND TBLSTORAGELOTINFO.MCODE = A.MCODE AND TBLSTORAGELOTINFO.LOTNO= A.LOTNO) ";
            sql += " AS FRMSTORAGEQTY FROM TBLITEMLOTDETAIL A LEFT JOIN TBLITEMLOT B ON A.LOTNO = B.LOTNO AND A.MCODE= B.MCODE  WHERE  A.SERIALNO= '{0}'AND A.MCODE ='{1}'";
            sql = string.Format(sql, serialNo, itemCode);
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotDetailForTrans), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public object[] QueryItemLotDetailForTrans(string lotno, string mcode)
        {
            string sql = "SELECT A.SERIALNO,A.LOTNO,A.MCODE,A.STORAGEID,A.STACKCODE,A.SERIALSTATUS  FROM TBLITEMLOTDETAIL A  LEFT JOIN TBLITEMLOT B ON A.LOTNO=B.LOTNO AND A.MCODE=B.MCODE WHERE B.LOTNO='{0}'AND B.MCODE='{1}' ";
            sql = string.Format(sql, lotno, mcode);
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            return null;
        }

        public object[] QueryItemLotDetail(string serialno, string storageId)
        {
            string sql = "SELECT * FROM TBLITEMLOTDETAIL WHERE 1=1 ";
            if (serialno.Trim() != string.Empty)
            {
                sql += " AND SERIALNO = '" + serialno + "'";
            }
            if (storageId.Trim() != string.Empty)
            {
                sql += " AND STORAGEID = '" + storageId + "'";
            }
            object[] objs = this.DataProvider.CustomQuery(typeof(ItemLotDetail), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
            {
                return objs;
            }
            return null;
        }

        #endregion
        #region   初检接收
        public int GetCartonNoByStnoAndCartonNo(string STNo, string CartonNo)
        {
            string sql = string.Format("SELECT {0} FROM TBLASNDETAIL WHERE STNO='{1}' AND CARTONNO='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), STNo, CartonNo);
            object[] objs = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            if (objs != null && objs.Length > 0)
            {
                return 1;  //当前STNO 箱号重复
            }
            //sql = string.Format("SELECT {0} FROM TBLASNDETAIL WHERE STNO<>'{1}' AND STATUS IN ('{2}','{3}','{4}','{5}','{6}') AND CARTONNO='{7}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), STNo, ASNDetail_STATUS.ASNDetail_IQCClose, ASNDetail_STATUS.ASNDetail_OnLocation, ASNDetail_STATUS.ASNDetail_ReceiveClose, ASNDetail_STATUS.ASNDetail_Release, ASNDetail_STATUS.ASNDetail_WaitReceive, CartonNo);
            sql = string.Format("SELECT {0} FROM TBLASNDETAIL WHERE STNO<>'{1}' AND STATUS NOT IN ('" + ASNDetail_STATUS.ASNDetail_Cancel + "','" + ASNDetail_STATUS.ASNDetail_IQCRejection + "','IQCReject') AND InitReceiveStatus<>'Reject' and Cartonno='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetail)), STNo, CartonNo);
            object[] objs1 = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            if (objs1 != null && objs1.Length > 0)
            {
                foreach(Asndetail asndetail in objs1)
                {
                    if(asndetail.Status!="Close")
                          return 2; 
                }

               //此箱号在其他的STNO中
            }
            object obj = GetASN(STNo);
            ASN asn = obj as ASN;
            //if (asn.StType == SAP_ImportType.SAP_YFR || asn.StType == SAP_ImportType.SAP_POR || asn.StType == SAP_ImportType.SAP_PD || asn.StType == SAP_ImportType.SAP_DNR || asn.StType == SAP_ImportType.SAP_PGIR || asn.StType == SAP_ImportType.SAP_UB)
            //{
            sql = string.Format("SELECT {0} FROM TBLSTORAGEDETAIL WHERE CARTONNO='{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StorageDetail)), CartonNo);
            object[] objs2 = this.DataProvider.CustomQuery(typeof(StorageDetail), new SQLCondition(sql));
            if (objs2 != null && objs2.Length > 0)
            {
                return 3;  //此箱号在库存中
            }
            //}
            return 0;
        }
        #endregion

        #region ItemLotForQuery
        public object[] QueryItemLotForQuery(string lotNo, string mCodes, string vendorCodes)
        {
            string sql = string.Empty;
            sql += " select DISTINCT lot.lotno, material.mcode, material.mdesc, lot.active,";
            sql += " vendor.vendorcode, vendor.vendorname, lot.venderlotno, ";
            sql += " info.storageid, info.stackcode, info.lotqty, lot.datecode, lot.orgid ";
            sql += " FROM tblitemlot lot, tblstoragelotinfo info,tblmaterial material, tblvendor vendor";
            sql += " where 1 = 1 AND lot.lotno = info.lotno AND lot.mcode = info.mcode AND lot.mcode = material.mcode";
            sql += " AND lot.orgid = material.orgid  AND lot.vendorcode = vendor.vendorcode  And material.mcontroltype = 'item_control_lot'";

            if (lotNo != "")
            {
                sql += " and lot.lotno = '" + lotNo + "'";
            }
            if (mCodes != "")
            {
                if (mCodes.Contains(","))
                {
                    sql += " and lot.mcode in (";
                    string[] mCode = mCodes.Split(',');
                    foreach (string str in mCode)
                    {
                        sql += "'" + str + "',";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")";
                }
                else
                {
                    sql += " and lot.mcode in ('" + mCodes + "')";
                }
            }
            if (vendorCodes != "")
            {
                if (vendorCodes.Contains(","))
                {
                    sql += " and lot.vendorcode in (";
                    string[] vendorCode = vendorCodes.Split(',');
                    foreach (string str in vendorCode)
                    {
                        sql += "'" + str + "',";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
                    sql += ")";
                }
                else
                {
                    sql += " and lot.vendorcode in ('" + vendorCodes + "')";
                }
            }

            return this.DataProvider.CustomQuery(typeof(ItemLotForQuery), new SQLCondition(sql));

        }
        #endregion

        #region LOTCHANGELOG
        /// <summary>
        /// TBLLOTCHANGELOG
        /// </summary>
        public LotChangeLog CreateNewLotChangeLog()
        {
            return new LotChangeLog();
        }

        public void AddLotChangeLog(LotChangeLog lotchangelog)
        {
            this.DataProvider.Insert(lotchangelog);
        }

        public void DeleteLotChangeLog(LotChangeLog lotchangelog)
        {
            this.DataProvider.Delete(lotchangelog);
        }

        public void UpdateLotChangeLog(LotChangeLog lotchangelog)
        {
            this.DataProvider.Update(lotchangelog);
        }

        public object GetLotChangeLog(int SERIAL)
        {
            return this.DataProvider.CustomSearch(typeof(LotChangeLog), new object[] { SERIAL });
        }


        #endregion
        #endregion


        #region MaterialMSL
        public MaterialMSL CreateMaterialMSL()
        {
            return new MaterialMSL();
        }

        public void AddMaterialMSL(MaterialMSL materialmsl)
        {
            this._helper.AddDomainObject(materialmsl);
        }

        public void UpdateMaterialMSL(MaterialMSL materialmsl)
        {
            this._helper.UpdateDomainObject(materialmsl);
        }

        public void DeleteMaterialMSL(MaterialMSL msdlevel)
        {
            this._helper.DeleteDomainObject(msdlevel);
        }

        public void DeleteMaterialMSL(MaterialMSL[] msdlevel)
        {
            this._helper.DeleteDomainObject(msdlevel);
        }

        public object GetMaterialMSL(string mhumiditylevel, int orgId)
        {
            return this.DataProvider.CustomSearch(typeof(MaterialMSL), new object[] { mhumiditylevel, orgId });
        }

        public object[] QueryMaterialMSL(string mcode, string mname, string mdesc, string mhumiditylevel, string mhumidityleveldesc, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            sql = "select A.mcode as MCode,B.MNAME as MaterialName,B.MDESC as MaterialDes,A.mhumiditylevel as MHumidityLevel,C.MHumidityLevelDesc as MHumidityLevelDesc,C.FLOORLIFE as FloorLife,C.DRYINGTIME,C.INDRYINGTIME,A.muser,A.mdate,A.mtime ";
            sql += " from    TBLMaterialMSL A";
            sql += " INNER JOIN TBLMATERIAL B ON  A. MCODE=B. MCODE AND A.ORGID=B.ORGID";
            sql += " INNER JOIN TBLMSDLevel C ON  A. MHumidityLevel =C.MHumidityLevel where 1=1 ";

            if (mcode.Trim() != string.Empty)
            {
                if (mcode.IndexOf(",") >= 0)
                {
                    string[] lists = mcode.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mcode = string.Join(",", lists);
                    sql += string.Format(" and A.MCODE in ({0})", mcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and A.MCODE like '{0}%'", mcode.Trim().ToUpper());
                }
            }

            if (mname != null && mname.Length > 0)
            {
                sql = string.Format("{0} AND B.MNAME LIKE '%{1}%'", sql, mname);
            }

            if (mdesc != null && mdesc.Length > 0)
            {
                sql = string.Format("{0} AND B.MDESC LIKE '%{1}%'", sql, mdesc);
            }


            if (mhumiditylevel.Trim() != string.Empty)
            {
                if (mhumiditylevel.IndexOf(",") >= 0)
                {
                    string[] lists = mhumiditylevel.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mhumiditylevel = string.Join(",", lists);
                    sql += string.Format(" and A.MHumidityLevel in ({0})", mhumiditylevel.ToUpper());
                }
                else
                {
                    sql += string.Format(" and A.MHumidityLevel like '{0}%'", mhumiditylevel.Trim().ToUpper());
                }
            }


            if (mhumidityleveldesc != null && mhumidityleveldesc.Length > 0)
            {
                sql = string.Format("{0} AND C.MHumidityLevelDesc like '%{1}%'", sql, mhumidityleveldesc);
            }

            return this.DataProvider.CustomQuery(typeof(MaterialMSLExc), new PagerCondition(sql, "A.MCODE,A.MHumidityLevel", inclusive, exclusive));
        }

        public int QueryMaterialMSLCount(string mcode, string mname, string mdesc, string mhumiditylevel, string mhumidityleveldesc)
        {
            string sql = string.Empty;
            sql = "select count(*) from    TBLMaterialMSL A";
            sql += " INNER JOIN TBLMATERIAL B ON  A. MCODE=B. MCODE AND A.ORGID=B.ORGID";
            sql += " INNER JOIN TBLMSDLevel C ON  A. MHumidityLevel =C.MHumidityLevel where 1=1 ";

            if (mcode.Trim() != string.Empty)
            {
                if (mcode.IndexOf(",") >= 0)
                {
                    string[] lists = mcode.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mcode = string.Join(",", lists);
                    sql += string.Format(" and A.MCODE in ({0})", mcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and A.MCODE like '{0}%'", mcode.Trim().ToUpper());
                }
            }

            if (mname != null && mname.Length > 0)
            {
                sql = string.Format("{0} AND B.MNAME LIKE '%{1}%'", sql, mname);
            }

            if (mdesc != null && mdesc.Length > 0)
            {
                sql = string.Format("{0} AND B.MDESC LIKE '%{1}%'", sql, mdesc);
            }


            if (mhumiditylevel.Trim() != string.Empty)
            {
                if (mhumiditylevel.IndexOf(",") >= 0)
                {
                    string[] lists = mhumiditylevel.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mhumiditylevel = string.Join(",", lists);
                    sql += string.Format(" and A.MHumidityLevel in ({0})", mhumiditylevel.ToUpper());
                }
                else
                {
                    sql += string.Format(" and A.MHumidityLevel like '{0}%'", mhumiditylevel.Trim().ToUpper());
                }
            }


            if (mhumidityleveldesc != null && mhumidityleveldesc.Length > 0)
            {
                sql = string.Format("{0} AND C.MHumidityLevelDesc like '%{1}%'", sql, mhumidityleveldesc);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion

        #region GetITEMLot
        public object GetITEMLot(string LotNO)
        {
            return this.DataProvider.CustomSearch(typeof(ITEMLot), new object[] { LotNO });
        }
        #endregion


        #region CheckMaterialByLotNO
        public int CheckMaterialByLotNO(string LotNO)
        {
            string sql = string.Empty;
            sql = "SELECT count(*) FROM TBLITEMLot  INNER JOIN TBLMaterialMSL  ON TBLITEMLot.mcode=TBLMaterialMSL.mcode WHERE  TBLITEMLot.lotno='" + LotNO + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region QueryMaterialMSD
        public object QueryMaterialMSD(string LotNO)
        {
            string sql = "select A.lotno,A.STATUS,A.FLOORLIFE,A.overfloorlife,A.mdate,A.mtime,B.MCODE, C.MNAME,C.MDESC";
            sql += " from TBLMSDLOT A";
            sql += " LEFT JOIN TBLITEMLOT B  ON  A.lotno=B.lotno";
            sql += " LEFT JOIN TBLMATERIAL C ON  B.MCODE=C.MCODE";
            sql += " where A.lotno='{0}'";

            sql = string.Format(sql, LotNO);
            object[] objs = this.DataProvider.CustomQuery(typeof(MSDLOTLExc), new SQLCondition(sql));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }

        #endregion

        #region MSDLot
        public MSDLOT CreateMSDLOT()
        {
            return new MSDLOT();
        }

        public void AddMSDLOT(MSDLOT msdlot)
        {
            this._helper.AddDomainObject(msdlot);
        }

        public void UpdateMSDLOT(MSDLOT msdlot)
        {
            this._helper.UpdateDomainObject(msdlot);
        }

        public void DeletMSDLOT(MSDLOT msdlot)
        {
            this._helper.DeleteDomainObject(msdlot);
        }

        public void DeleteMSDLOT(MSDLOT[] msdlot)
        {
            this._helper.DeleteDomainObject(msdlot);
        }

        public object GetMSDLot(string LotNO)
        {
            return this.DataProvider.CustomSearch(typeof(MSDLOT), new object[] { LotNO });
        }
        #endregion


        #region MSDWIP
        public MSDWIP CreateMSDWIP()
        {
            return new MSDWIP();
        }

        public void AddMSDWIP(MSDWIP msdwip)
        {
            this._helper.AddDomainObject(msdwip);
        }

        public void UpdateMSDWIP(MSDWIP msdwip)
        {
            this._helper.UpdateDomainObject(msdwip);
        }

        public void DeletMSDWIP(MSDWIP msdwip)
        {
            this._helper.DeleteDomainObject(msdwip);
        }

        public void DeleteMSDWIP(MSDWIP[] msdwip)
        {
            this._helper.DeleteDomainObject(msdwip);
        }

        public object GetMSDWIP(string lotNO, string status)
        {
            string sql = string.Empty;
            sql = "select * from TBLMSDWIP where lotno='{0}'  and status='{1}' order by  mdate desc,mtime desc";
            sql = string.Format(sql, lotNO, status);

            object[] objs = this.DataProvider.CustomQuery(typeof(MSDWIP), new SQLCondition(sql));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }

        #endregion

        #region GetMSDLevelByLotNo
        public object GetMSDLevelByLotNo(string lotno)
        {
            string sql = string.Empty;
            sql = "SELECT TBLMSDLevel.floorlife,TBLMSDLevel.dryingtime FROM TBLITEMLot ";
            sql += " left JOIN TBLMaterialMSL  ON TBLITEMLot.mcode=TBLMaterialMSL.mcode ";
            sql += " left join TBLMSDLevel on TBLMaterialMSL.MHumidityLevel=TBLMSDLevel.MHumidityLevel";
            sql += " WHERE 1=1 ";

            if ((lotno != string.Empty) && (lotno.Length > 0))
            {
                sql += " and TBLITEMLot.lotno='" + lotno + "'";
            }

            object[] objs = this.DataProvider.CustomQuery(typeof(MSDLevel), new SQLCondition(sql));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }
        #endregion


        #region QueryMSDInfo
        public object[] QueryMSDInfo(string lotno, string mcode, string status, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            sql = "SELECT  A. LOTNO,B. MCODE,C. MNAME,C. MDESC,A. STATUS,A. FLOORLIFE, A. OVERFLOORLIFE,A. MUSER,A. MDATE,A. MTIME  ";
            sql += " FROM  TBLMSDLOT   A ";
            sql += " INNER  JOIN TBLITEMLOT  B ON A. LOTNO=B. LOTNO";
            sql += " INNER  JOIN TBLMATERIAL C ON B. MCODE=C. MCODE  AND  C.ORGID=B.ORGID where 1=1 ";

            if (lotno != null && lotno.Length > 0)
            {
                sql = string.Format("{0} AND A. LOTNO = '{1}'", sql, lotno);
            }


            if (mcode.Trim() != string.Empty)
            {
                if (mcode.IndexOf(",") >= 0)
                {
                    string[] lists = mcode.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mcode = string.Join(",", lists);
                    sql += string.Format(" and B. MCODE in ({0})", mcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and B. MCODE like '{0}%'", mcode.Trim().ToUpper());
                }
            }

            if (status != null && status.Length > 0)
            {
                sql = string.Format("{0} AND  A. STATUS = '{1}'", sql, status);
            }

            return this.DataProvider.CustomQuery(typeof(MSDLOTLExc), new PagerCondition(sql, " A. LOTNO", inclusive, exclusive));
        }

        #endregion

        #region QueryMSDInfoCount
        public int QueryMSDInfoCount(string lotno, string mcode, string status)
        {
            string sql = string.Empty;
            sql = "select count(*) ";
            sql += " FROM  TBLMSDLOT   A ";
            sql += " INNER  JOIN TBLITEMLOT  B ON A. LOTNO=B. LOTNO";
            sql += " INNER  JOIN TBLMATERIAL C ON B. MCODE=C. MCODE  AND  C.ORGID=B.ORGID where 1=1 ";

            if (lotno != null && lotno.Length > 0)
            {
                sql = string.Format("{0} AND A. LOTNO = '{1}'", sql, lotno);
            }


            if (mcode.Trim() != string.Empty)
            {
                if (mcode.IndexOf(",") >= 0)
                {
                    string[] lists = mcode.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mcode = string.Join(",", lists);
                    sql += string.Format(" and B. MCODE in ({0})", mcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and B. MCODE like '{0}%'", mcode.Trim().ToUpper());
                }
            }

            if (status != null && status.Length > 0)
            {
                sql = string.Format("{0} AND  A. STATUS = '{1}'", sql, status);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion


        #region QueryMSDHistoryInfo
        public object[] QueryMSDHistoryInfo(string lotno, string mcode, int inclusive, int exclusive)
        {
            string sql = string.Empty;

            sql = " SELECT  A. SERIAL,A. LOTNO,B. MCODE,C. MNAME,C. MDESC,A. STATUS,A. MUSER,A. MDATE,A. MTIME   ";
            sql += " FROM  TBLMSDWIP   A ";
            sql += " INNER  JOIN TBLITEMLOT  B ON A. LOTNO=B. LOTNO";
            sql += " INNER  JOIN TBLMATERIAL C ON B. MCODE=C. MCODE  AND  C.ORGID=B.ORGID where 1=1 ";

            if (lotno != null && lotno.Length > 0)
            {
                sql = string.Format("{0} AND A. LOTNO = '{1}'", sql, lotno);
            }


            if (mcode.Trim() != string.Empty)
            {
                if (mcode.IndexOf(",") >= 0)
                {
                    string[] lists = mcode.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mcode = string.Join(",", lists);
                    sql += string.Format(" and B. MCODE in ({0})", mcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and B. MCODE like '{0}%'", mcode.Trim().ToUpper());
                }
            }

            return this.DataProvider.CustomQuery(typeof(MSDWIPExc), new PagerCondition(sql, " A. SERIAL", inclusive, exclusive));
        }

        #endregion

        #region QueryMSDHistoryInfoCount
        public int QueryMSDHistoryInfoCount(string lotno, string mcode)
        {
            string sql = string.Empty;
            sql = "select count(*) ";
            sql += " FROM  TBLMSDWIP   A ";
            sql += " INNER  JOIN TBLITEMLOT  B ON A. LOTNO=B. LOTNO";
            sql += " INNER  JOIN TBLMATERIAL C ON B. MCODE=C. MCODE  AND  C.ORGID=B.ORGID where 1=1 ";

            if (lotno != null && lotno.Length > 0)
            {
                sql = string.Format("{0} AND A. LOTNO = '{1}'", sql, lotno);
            }


            if (mcode.Trim() != string.Empty)
            {
                if (mcode.IndexOf(",") >= 0)
                {
                    string[] lists = mcode.Trim().Split(',');
                    for (int i = 0; i < lists.Length; i++)
                    {
                        lists[i] = "'" + lists[i] + "'";
                    }
                    mcode = string.Join(",", lists);
                    sql += string.Format(" and B. MCODE in ({0})", mcode.ToUpper());
                }
                else
                {
                    sql += string.Format(" and B. MCODE like '{0}%'", mcode.Trim().ToUpper());
                }
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion


        #region QueryMSDALERT

        public DataTable QueryMSDALERT()
        {
            string sql = string.Empty;
            sql = @"SELECT DISTINCT A.LOTNO,
                B.MCODE,
                C.MNAME,
                C.MDESC,
                A.FLOORLIFE, 
                E.INDRYINGTIME,    
                TRUNC((SYSDATE -
                      TO_DATE(TO_CHAR(A.MDATE,'00000000') || TO_CHAR(A.MTIME,'000000'),
                               'yyyy-mm-dd hh24:mi:ss')) * 24,
                      1) AS LASTTIME,                          
                A.OVERFLOORLIFE -
                TRUNC((SYSDATE -
                      TO_DATE(TO_CHAR(A.MDATE,'00000000') || TO_CHAR(A.MTIME,'000000'),
                               'yyyy-mm-dd hh24:mi:ss')) * 24,
                      1) AS OVERFLOORLIFE,                 
                A.MUSER,
                A.MDATE,
                A.MTIME ";
            sql = sql + " FROM TBLMSDLOT A";
            sql = sql + " INNER JOIN TBLITEMLOT B ON A.LOTNO = B.LOTNO";
            sql = sql + " INNER JOIN TBLMATERIAL C ON B.MCODE = C.MCODE AND B.ORGID = C.ORGID";
            sql = sql + " INNER JOIN TBLMATERIALMSL D ON C.MCODE = D.MCODE  AND B.ORGID = C.ORGID";
            sql = sql + " INNER JOIN TBLMSDLEVEL E ON D.MHUMIDITYLEVEL = E.MHUMIDITYLEVEL";
            sql = sql + " WHERE A.STATUS  in ( 'MSD_OPENED','MSD_OVERTIME') ";

            DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);
            return ds.Tables[0];
        }
        #endregion

        public string[] QueryPickNO(string gfFlag, string[] userGroups)
        {
            string sql = string.Format(@" SELECT {0} FROM tblpick  WHERE (STATUS='MakePackingList' OR STATUS='Pack') AND GFFLAG='" + gfFlag + "'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pick)));
            sql += @"AND PICKNO IN(SELECT DISTINCT PICKNO FROM
 (SELECT PICKNO FROM TBLPICK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @")) UNION 
 SELECT PICKNO FROM TBLPICK WHERE InStorageCode IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE IN(" + SqlFormat(userGroups) + @"))) T)  ";
            sql += " order by cdate,ctime desc";
            object[] objs = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            string[] str;
            if (objs != null)
            {
                str = new string[objs.Length];
                int i = 0;
                foreach (Pick pick in objs)
                {
                    str[i] = pick.PickNo;
                    i++;
                }
                return str;
            }
            return null;
        }


        public string[] QueryPickNONotN(string[] userGroups)
        {
            string sql = string.Format(@" SELECT {0} FROM tblpick  WHERE (STATUS='MakePackingList' OR STATUS='Pack') AND (GFFLAG<>'X' OR GFFLAG IS NULL) ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pick)));
            sql += @"AND PICKNO IN(SELECT DISTINCT PICKNO FROM
 (SELECT PICKNO FROM TBLPICK WHERE STORAGECODE IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE in (" + SqlFormat(userGroups) + @")) UNION 
 SELECT PICKNO FROM TBLPICK WHERE InStorageCode IN(SELECT PARAMCODE FROM TBLSYSPARAM WHERE PARAMGROUPCODE IN(" + SqlFormat(userGroups) + @"))) T)  ";
            sql += " order by cdate,ctime desc";
            object[] objs = this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
            string[] str;
            if (objs != null)
            {
                str = new string[objs.Length];
                int i = 0;
                foreach (Pick pick in objs)
                {
                    str[i] = pick.PickNo;
                    i++;
                }
                return str;
            }
            return null;
        }

        #region QueryStoragePick   add by sam 2016-02-22

        public object[] QueryStoragePick(string pickNo, string pickType, string storageCode, string invNo, string chk,
            string cusBatchNo, int bCDate, int eCDate, string usercode, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  {0} FROM tblpick  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pick)));

            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND PICKNO ='{0}'", pickNo);
            }

            #region pickType
            if (!string.IsNullOrEmpty(pickType))
            {
                if (pickType.EndsWith(","))
                {
                    pickType = pickType.TrimEnd(',');
                    sql += string.Format(@" AND   pickType  in ({0})", pickType);
                }
                else
                {
                    sql += string.Format(@" AND  pickType ='{0}' ", pickType);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND StorageCode = '{0}'", storageCode);
            }

            //SAP单据号
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   STATUS  in ({0})", chk);
            }
            #endregion

            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }

            if (!string.IsNullOrEmpty(cusBatchNo))
            {
                sql += string.Format(@" AND CusBatchNo = '{0}'", invNo);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            sql += string.Format(@" AND STATUS not in ('Release' ) ");
            return this.DataProvider.CustomQuery(typeof(Pick), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }


        public int QueryStoragePickCount(string pickNo, string pickType, string storageCode, string invNo, string chk,
            string cusBatchNo, int bCDate, int eCDate, string usercode)
        {
            string sql = @" SELECT COUNT(1) FROM tblpick WHERE 1=1 ";

            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND PICKNO ='{0}'", pickNo);
            }

            #region pickType
            if (!string.IsNullOrEmpty(pickType))
            {
                if (pickType.EndsWith(","))
                {
                    pickType = pickType.TrimEnd(',');
                    sql += string.Format(@" AND   pickType  in ({0})", pickType);
                }
                else
                {
                    sql += string.Format(@" AND  pickType ='{0}' ", pickType);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND StorageCode = '{0}'", storageCode);
            }

            //SAP单据号
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   STATUS  in ({0})", chk);
            }

            #endregion

            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }

            if (!string.IsNullOrEmpty(cusBatchNo))
            {
                sql += string.Format(@" AND CusBatchNo = '{0}'", invNo);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            sql += string.Format(@" AND STATUS not in ('Release') ");
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        #endregion

        #region SumRecordOQC add by sam 2016年8月17日

        public int QueryTransQty(string picktype, string storageCode, int bCDate, int eCDate, string processType)
        {
            string sql = string.Format(@" SELECT distinct a.oqcno  
                          from  tbloqc a
                left join tbloqcdetail c on a.oqcno=c.oqcno
                left join tblpick  b on a.pickno=b.pickno 
                left join tblpickdetail d on a.pickno=d.pickno and c.mcode=d.mcode where 1=1  ");
            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND b.picktype = '{0}'", picktype);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND a.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate>={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate>={0} ) ) ", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate<={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate<={0} ) ) ", eCDate);

            }
            sql += string.Format(@" and  a.OQCTYPE  not  in ('ExemptCheck')");

            string finalsql = string.Format(@"select  count( distinct a.transno)  from TBLINVINOUTTRANS a where a.transtype='OUT'   
               and a.transno in   ( {0})  and a.processType='{1}'  ", sql, processType);
            return this.DataProvider.GetCount(new SQLCondition(finalsql));
        }

        public int GetSumNgQty(string picktype, string storageCode, int bCDate, int eCDate, string ecgcode)
        {
            string sql = string.Format(@" SELECT distinct a.oqcno  
                          from  tbloqc a
                left join tbloqcdetail c on a.oqcno=c.oqcno
                left join tblpick  b on a.pickno=b.pickno 
                left join tblpickdetail d on a.pickno=d.pickno and c.mcode=d.mcode where 1=1  ");
            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND b.picktype = '{0}'", picktype);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND a.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate>={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate>={0} ) ) ", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate<={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate<={0} ) ) ", eCDate);

            }
            sql += string.Format(@" and  a.OQCTYPE  not  in ('ExemptCheck')");

            string finalsql = string.Format(@"   select nvl(sum(a.ngqty),0) from tbloqcdetailec a where a.ecgcode='{0}' and a.oqcno in({1})  ",
               ecgcode, sql);
            return this.DataProvider.GetCount(new SQLCondition(finalsql));
        }

        public int QueryTransDate(string picktype, string storageCode, int bCDate, int eCDate, string processType)
        {
            string sql = string.Format(@" SELECT distinct a.oqcno  
                          from  tbloqc a
                left join tbloqcdetail c on a.oqcno=c.oqcno
                left join tblpick  b on a.pickno=b.pickno 
                left join tblpickdetail d on a.pickno=d.pickno and c.mcode=d.mcode where 1=1  ");
            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND b.picktype = '{0}'", picktype);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND a.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate>={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate>={0} ) ) ", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate<={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate<={0} ) ) ", eCDate);

            }
            sql += string.Format(@" and  a.OQCTYPE  not  in ('ExemptCheck')");

            string finalsql = string.Format(@"select nvl(avg(a.mdate),0)  from TBLINVINOUTTRANS a where a.transtype='OUT'   
               and a.transno in   ( {0})  and a.processType='{1}'  ", sql, processType);
            return this.DataProvider.GetCount(new SQLCondition(finalsql));
        }

        #endregion

        #region SumRecordOQC add by sam 2016年8月17日
        public int QuerySumRecordOQCCount(string picktype, string storageCode, int bCDate, int eCDate)
        {
            string sql = string.Format(@" SELECT count( distinct a.oqcno)   
                          from  tbloqc a
                left join tbloqcdetail c on a.oqcno=c.oqcno
                left join tblpick  b on a.pickno=b.pickno 
                left join tblpickdetail d on a.pickno=d.pickno and c.mcode=d.mcode where 1=1  ");
            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND b.picktype = '{0}'", picktype);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND a.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate>={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate>={0} ) ) ", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate<={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate<={0} ) ) ", eCDate);

            }
            sql += string.Format(@" and  a.OQCTYPE  not  in ('ExemptCheck')");
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 1)
                return 1;
            return 0;
        }

        public object[] QuerySumRecordOQC(string picktype, string storageCode, int bCDate, int eCDate,
                                       int inclusive, int exclusive)
        {
            string sql = string.Format(@"   select count( distinct a.oqcno) oqcqty,sum(c.qty) qty,sum(c.ngqty) ngqty,
                   sum(c.returnqty) returnqty,sum(c.giveqty) giveqty,sum(aql.samplesize) samplesize
                 from  tbloqc a
                left join tbloqcdetail c on a.oqcno=c.oqcno
                left join tblpick  b on a.pickno=b.pickno 
                left join tblpickdetail d on a.pickno=d.pickno and c.mcode=d.mcode 
                 left join tblmaterial m on m.mcode=c.mcode
                  left join  tblaql  aql on aql.aqllevel=a.aql
                 where 1=1 ");
            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND b.picktype = '{0}'", picktype);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND a.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate>={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate>={0} ) ) ", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate<={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate<={0} ) ) ", eCDate);
            }
            sql += string.Format(@" and a.OQCTYPE  not  in ('ExemptCheck')");
            string finalsql = string.Format("select a.* from ( {0} ) a", sql);
            return this.DataProvider.CustomQuery(typeof(OQCQuery), new PagerCondition(finalsql, inclusive, exclusive));
        }
        #endregion


        #region RecordOQC add by sam 2016年8月17日
        public object[] QueryRecordOQC(string picktype, string storageCode, int bCDate, int eCDate,
                                    int inclusive, int exclusive)
        {
            string sql = string.Format(@"select a.*,pi.picktype,pi.StorageCode,pi.InvNo,
                c.mcode,c.gfhwitemcode,c.dqmcode,d.venderitemcode,m.mchlongdesc mdesc,
                c.qty,c.returnqty,c.giveqty,C.NGQTY ,aql.samplesize
                 from  tbloqc a
                inner join tblpick pi on a.pickno=pi.pickno
                left join tbloqcdetail c on a.oqcno=c.oqcno
                
                left join tblpickdetail d on a.pickno=d.pickno and c.mcode=d.mcode 
                 left join tblmaterial m on m.mcode=c.mcode
                  left join  tblaql  aql on aql.aqllevel=a.aql
                 where 1=1 ");

            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND pi.picktype = '{0}'", picktype);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND pi.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate>={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate>={0} ) ) ", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate<={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate<={0} ) ) ", eCDate);

            }
            sql += string.Format(@" and a.OQCTYPE  not  in ('ExemptCheck')");
            return this.DataProvider.CustomQuery(typeof(OQCQuery), new PagerCondition(sql, " a.oqcno DESC", inclusive, exclusive));
        }

        public int QueryRecordOQCCount(string picktype, string storageCode, int bCDate, int eCDate)
        {
            string sql = string.Format(@" SELECT COUNT(1)     
                          from  tbloqc a
                inner join tblpick pi on a.pickno=pi.pickno
                left join tbloqcdetail c on a.oqcno=c.oqcno
                left join tblpick  b on a.pickno=b.pickno 
                left join tblpickdetail d on a.pickno=d.pickno and c.mcode=d.mcode where 1=1  ");

            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND pi.picktype = '{0}'", picktype);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND pi.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate>={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate>={0} ) ) ", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND     ( a.cdate<={0} or  a.oqcno in  ( select distinct  a.transno from TBLINVINOUTTRANS a 
              where a.transtype='OUT'   
           and a.processType in ('OQCSQE','OQC') and a.mdate<={0} ) ) ", eCDate);

            }
            sql += string.Format(@" and  a.OQCTYPE  not  in ('ExemptCheck')");
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region StorageOut

        public int GetNgQty(string oqcno, string mCode, string ecgcode)
        {
            string sql = string.Format(@"   select nvl(sum(a.ngqty),0) from tbloqcdetailec a where a.ecgcode='{0}' and a.Mcode='{1}' and a.oqcno='{2}'  ",
               ecgcode, mCode, oqcno);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int MaxOutDate(string pickno, string mCode, string processType)
        {
            string sql = string.Format(@" select  nvl(max(a.mdate),0)  from TBLINVINOUTTRANS a where a.transtype='OUT' and a.Mcode='{0}' and a.TransNO='{1}' and a.processType='{2}' ",
                mCode, pickno, processType);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int MaxOutTime(string pickno, string mCode, string processType)
        {
            string sql = string.Format(@" select  * from TBLINVINOUTTRANS a where a.transtype='OUT' and a.Mcode='{0}' and a.TransNO='{1}' and a.processType='{2}' 
                           order by a.mdate desc,a.mtime desc",
                mCode, pickno, processType);
            object[] obj = this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
            if (obj != null && obj.Length > 0)
            {
                InvInOutTrans inter = obj[0] as InvInOutTrans;
                int tmpStr = inter.MaintainTime;
                return tmpStr;
            }
            return 0;
        }

        public object[] GetTransUser(string pickno, string mCode, string processType)
        {
            string sql = string.Format(@" select  distinct a.muser from TBLINVINOUTTRANS a where a.transtype='OUT' and a.Mcode='{0}' and a.TransNO='{1}' and a.processType='{2}' ",
                mCode, pickno, processType);
            return this.DataProvider.CustomQuery(typeof(InvInOutTrans), new SQLCondition(sql));
        }
        public object[] QueryPickID(string pickNo, string storageCode)
        {
            string sql = @"SELECT a.* FROM TBLPICK a 
                    left join tblinvoices b on a.invno=b.invno
                              WHERE 1=1";
            return this.DataProvider.CustomQuery(typeof(Pick), new SQLCondition(sql));
        }

        public object[] QueryStorageOut(string picktype,string invno, string pickNo, string storageCode, int bCDate, int eCDate,
                                      int inclusive, int exclusive)
        {
            string sql = string.Format(@" select a.*,b.mcode
                         from tblpick a
                        left join tblpickdetail b on a.pickno=b.pickno
                    where 1=1 ");


            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(@" AND a.invno = '{0}'", invno);
            }


            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND a.picktype = '{0}'", picktype);
            }


            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(@" AND a.pickNo = '{0}'", pickNo);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND a.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND a.Finish_Date>= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND a.Finish_Date<= {0}", eCDate);
            }
            return this.DataProvider.CustomQuery(typeof(PickQuery), new PagerCondition(sql, " a.pickNo DESC", inclusive, exclusive));
        }
        public int QueryStorageOutCount(string picktype, string invno, string pickNo, string storageCode, int bCDate, int eCDate)
        {
            string sql = string.Format(@" SELECT COUNT(1)    from tblpick a
                        left join tblpickdetail b on a.pickno=b.pickno
                     WHERE 1=1 ");


            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(@" AND a.invno = '{0}'", invno);
            }


            if (!string.IsNullOrEmpty(picktype))
            {
                sql += string.Format(@" AND a.picktype = '{0}'", picktype);
            }

            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(@" AND a.pickNo = '{0}'", pickNo);
            }
            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND a.storageCode = '{0}'", storageCode);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND a.Finish_Date>= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND a.Finish_Date<= {0}", eCDate);
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

        #region QueryPickHead   add by sam 2016-02-22

        public object[] QueryPickHead(string pickNo, string pickType, string storageCode, string invNo, string chk,
            string cusBatchNo, int bCDate, int eCDate, bool expressDelegate, string usercode, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  {0} FROM tblpick  WHERE 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pick)));

            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }

            //if (!string.IsNullOrEmpty(expressDelegate))
            //{
            //    sql += string.Format(" AND ExpressDelegate like '%{0}%' ", expressDelegate);
            //}

            if (expressDelegate)
            {
                sql += string.Format(" AND ExpressDelegate is null ");
            }
            ////else
            ////{
            ////    sql += string.Format(" AND ExpressDelegate is not null ");
            ////}
            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND PICKNO ='{0}'", pickNo);
            }

            #region pickType
            if (!string.IsNullOrEmpty(pickType))
            {
                if (pickType.EndsWith(","))
                {
                    pickType = pickType.TrimEnd(',');
                    sql += string.Format(@" AND   pickType  in ({0})", pickType);
                }
                else
                {
                    sql += string.Format(@" AND  pickType ='{0}' ", pickType);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND StorageCode = '{0}'", storageCode);
            }

            //SAP单据号
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   STATUS  in ({0})", chk);
            }

            //if (!string.IsNullOrEmpty(statusList))
            //{
            //    if (statusList.IndexOf(",") > 0)
            //    {

            //        sql += string.Format(@" AND STATUS IN ({0})", statusList);
            //    }
            //    else
            //    {
            //        sql += string.Format(@" AND STATUS IN ('{0}')", statusList);
            //    }
            //}
            #endregion

            if (!string.IsNullOrEmpty(cusBatchNo))
            {
                sql += string.Format(@" AND CusBatchNo like '%{0}%'", cusBatchNo);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            return this.DataProvider.CustomQuery(typeof(Pick), new PagerCondition(sql, "MDATE DESC,MTIME DESC", inclusive, exclusive));
        }


        public int QueryPickHeadCount(string pickNo, string pickType, string storageCode, string invNo, string chk,
            string cusBatchNo, int bCDate, int eCDate, bool expressDelegate, string usercode)
        {
            string sql = @" SELECT COUNT(1) FROM tblpick WHERE 1=1 ";

            if (!string.IsNullOrEmpty(usercode) && usercode != "ADMIN")
            {
                sql += string.Format(@" AND StorageCode in  ( select distinct  paramcode from TBLSYSPARAM where PARAMGROUPCODE in (
                select USERGROUPCODE ||'{0}' from TBLUSERGROUP2USER where USERCODE='{1}'  ))", "TOSTORAGE", usercode);
            }
            if (expressDelegate)
            {
                sql += string.Format(" AND ExpressDelegate is null ");
            }
            //else
            //{
            //    sql += string.Format(" AND ExpressDelegate is   null ");
            //}
            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND PICKNO ='{0}'", pickNo);
            }

            #region pickType
            if (!string.IsNullOrEmpty(pickType))
            {
                if (pickType.EndsWith(","))
                {
                    pickType = pickType.TrimEnd(',');
                    sql += string.Format(@" AND   pickType  in ({0})", pickType);
                }
                else
                {
                    sql += string.Format(@" AND  pickType ='{0}' ", pickType);
                }
            }
            #endregion

            if (!string.IsNullOrEmpty(storageCode))
            {
                sql += string.Format(@" AND StorageCode = '{0}'", storageCode);
            }

            //SAP单据号
            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(chk))
            {
                //TODO：去除最后的 ,
                if (chk.EndsWith(","))
                {
                    chk = chk.TrimEnd(',');
                }
                sql += string.Format(@" AND   STATUS  in ({0})", chk);
            }

            //if (!string.IsNullOrEmpty(statusList))
            //{
            //    if (statusList.IndexOf(",") > 0)
            //    {

            //        sql += string.Format(@" AND STATUS IN ({0})", statusList);
            //    }
            //    else
            //    {
            //        sql += string.Format(@" AND STATUS IN ('{0}')", statusList);
            //    }
            //}
            #endregion

            if (!string.IsNullOrEmpty(cusBatchNo))
            {
                sql += string.Format(@" AND CusBatchNo = '{0}'", invNo);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND CDATE <= {0}", eCDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #region 6.4	查询拣货任务令Head add by sam

        public object[] GetPickDetailMCodeByPickNo(string pickno)
        {
            string sql = "select distinct a.dqmcode from tblpickdetail a  where 1=1 ";
            if (!string.IsNullOrEmpty(pickno))
            {
                sql += string.Format(@" AND a.pickno ='{0}'", pickno);
            }
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }

        public PickDetail[] GetPickDetailMCodeByPickNo1(string pickno)
        {
            string sql = "select distinct a.dqmcode from tblpickdetail a  where 1=1 AND STATUS<>'Cancel' ";
            if (!string.IsNullOrEmpty(pickno))
            {
                sql += string.Format(@" AND a.pickno ='{0}'", pickno);
            }
            object[] oo = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            List<PickDetail> d = new List<PickDetail>();
            if (oo != null && oo.Length > 0)
            {
                foreach (PickDetail p in oo)
                {
                    d.Add(p);
                }
            }
            return d.ToArray();
        }

        public string GetRandPickLine(string pickNo, string dqmCode)
        {
            string sql = "select pickline from tblpickdetail a  where pickno='" + pickNo + "' and dqmcode='" + dqmCode + "'";
            object[] oo = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            if (oo != null && oo.Length > 0)
                return ((PickDetail)oo[0]).PickLine;

            return string.Empty;
        }

        public ASNDetail[] GetAsnDetailMCodeByStNo1(string stno)
        {
            string sql = "select distinct a.dqmcode from tblasndetail a  where 1=1 ";
            if (!string.IsNullOrEmpty(stno))
            {
                sql += string.Format(@"  AND a.stno in ( {0} ) ", stno);
            }
            object[] oo = this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));

            List<ASNDetail> d = new List<ASNDetail>();
            if (oo != null && oo.Length > 0)
            {
                foreach (ASNDetail p in oo)
                {
                    d.Add(p);
                }
            }
            return d.ToArray();
        }



        public object[] GetAsnDetailMCodeByStNo(string stno)
        {
            string sql = "select distinct a.dqmcode from tblasndetail a  where 1=1 ";
            if (!string.IsNullOrEmpty(stno))
            {
                sql += string.Format(@"  AND a.stno in ( {0} ) ", stno);
            }
            return this.DataProvider.CustomQuery(typeof(ASNDetail), new SQLCondition(sql));
        }

        //public string GetStNoByInvNo(string invNo)
        //{
        //    string sql = string.Format("select  a.* from tblasn a where a.invno='{0}'  ", invNo);
        //    object[] list = this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
        //    if (list != null)
        //        return ((ASN) list[0]).StNo;
        //    return "";
        //}

        public int GetPickDetailSumQtyByMCode(string pickno, string mcode)
        {
            string sql = "select nvl(sum(qty),0) from tblpickdetail where 1=1 and status<>'Cancel'";
            if (!string.IsNullOrEmpty(mcode))
            {
                sql += string.Format(@" AND mcode ='{0}'", mcode);
            }
            if (!string.IsNullOrEmpty(pickno))
            {
                sql += string.Format(@" AND pickno ='{0}'", pickno);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetAsnDetailSumQtyByMCode(string stno, string mcode)
        {
            string sql = "select nvl(sum(actqty),0) from  tblasndetail  where 1=1 ";
            if (!string.IsNullOrEmpty(mcode))
            {
                sql += string.Format(@" AND mcode ='{0}'", mcode);
            }
            if (!string.IsNullOrEmpty(stno))
            {
                sql += string.Format(@" AND stno in ({0})", stno);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }



        public object[] QueryAsnDetailCartonNo(string stno)
        {
            string sql = string.Format(@" SELECT  a.* FROM tblasndetail  a where a.stno in {0} ", stno);
            return this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
        }

        public object[] QueryAsnDetailSn(string stno, string cartonno)
        {
            string sql = string.Format(@" SELECT  a.* FROM tblasndetailsn  a  where  a.snto={0} and a.cartonno={1} ", stno, cartonno);
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }

        public object[] QueryASNDetailSNByMcode(string mcode)
        {
            string sql = "SELECT * FROM tblasndetailsn where mcode = '" + mcode + "'";
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }

        #endregion

        public object[] GetAllPickDetail(string pickno)
        {
            string sql = "select a.* from tblpickdetail a where a.pickno = '" + pickno + "'";
            return this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
        }

        public object[] GetAllDetailSn(string Stno)
        {
            string sql = "select a.* from tblasndetailsn a where a.stno = '" + Stno + "'";
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }

        public object[] GetAllPickDetailMaterialSn(string pickno)
        {
            string sql = "select a.* from tblasndetailsn a where a.pickno = '" + pickno + "'";
            return this.DataProvider.CustomQuery(typeof(PickDetailMaterialSn), new SQLCondition(sql));
        }

        #endregion


        #region QueryPickDetail   add by sam 2016-02-22

        public object[] QueryPickDetail(string pickNo, string DQMcode, string cusitemcode, string cusbatchno,
            string orderno, int inclusive, int exclusive)
        {
            string sql = string.Format(@" SELECT  a.*,b.StorageCode,b.ORDERNO  FROM TBLPICKDETAIL a 
                    left join tblpick b on a.pickno=b.pickno  WHERE 1=1 ");

            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO ='{0}'", pickNo);
            }
            if (!string.IsNullOrEmpty(DQMcode))
            {
                sql += string.Format(@" AND  a.DQMcode ='{0}'", DQMcode);
            }

            if (!string.IsNullOrEmpty(cusitemcode))
            {
                sql += string.Format(@" AND a.cusitemcode = '{0}'", cusitemcode);
            }

            //客户批次号
            if (!string.IsNullOrEmpty(cusbatchno))
            {
                sql += string.Format(@" AND b.cusbatchno = '{0}'", cusbatchno);
            }

            if (!string.IsNullOrEmpty(orderno))
            {
                sql += string.Format(@" AND a.CusBatchNo = '{0}'", orderno);
            }
            #endregion

            return this.DataProvider.CustomQuery(typeof(PickDetailQuery), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", inclusive, exclusive));
        }

        public int QueryPickDetailCount(string pickNo, string DQMcode, string cusitemcode, string cusbatchno,
            string orderno)
        {
            string sql = string.Format(@" 
                    SELECT  count(1)  FROM TBLPICKDETAIL a 
                    left join tblpick b on a.pickno=b.pickno  WHERE 1=1 ");

            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO ='{0}'", pickNo);
            }


            if (!string.IsNullOrEmpty(DQMcode))
            {
                sql += string.Format(@" AND  a.DQMcode ='{0}'", DQMcode);
            }

            if (!string.IsNullOrEmpty(cusitemcode))
            {
                sql += string.Format(@" AND a.cusitemcode = '{0}'", cusitemcode);
            }

            //客户批次号
            if (!string.IsNullOrEmpty(cusbatchno))
            {
                sql += string.Format(@" AND b.cusbatchno = '{0}'", cusbatchno);
            }

            if (!string.IsNullOrEmpty(orderno))
            {
                sql += string.Format(@" AND a.CusBatchNo = '{0}'", orderno);
            }
            #endregion

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion


        #region 货位移动单界面  add by sam 2016-02-22
        public object[] QueryLocStorTransQueryDetail(string transNo, string invno, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT a.* ,b.invno FROM  tblstorloctransdetailcarton a 
            inner join TBLStorLocTrans b on a.transno=b.transno   where 1=1 ");
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(@" and a.TRANSNO = '{0}'  ", transNo);
            }
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(@" and b.invno= '{0}'  ", invno);
            }
            sql += string.Format(@" and b.transtype= '{0}'  ", "Move");
            return this.DataProvider.CustomQuery(typeof(StorLocTransOperations), new PagerCondition(sql, "a.mdate desc,a.mtime desc", inclusive, exclusive));
        }

        public int QueryLocStorTransDetailCount(string transNo, string invno)
        {
            string sql = string.Format(@"SELECT count(1) FROM  tblstorloctransdetailcarton a 
            inner join TBLStorLocTrans b on a.transno=b.transno   where 1=1 ");
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(@" and a.TRANSNO = '{0}'  ", transNo);
            }
            if (!string.IsNullOrEmpty(invno))
            {
                sql += string.Format(@" and b.invno= '{0}'  ", invno);
            }
            sql += string.Format(@" and b.transtype= '{0}'  ", "Move");
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion


        #region 货位移动单界面  add by sam 2016-02-22
        public object[] QueryLocStorTransDetail(string transNo, int inclusive, int exclusive)
        {
            string sql = string.Format(@"SELECT a.* FROM  tblstorloctransdetailcarton a where 1=1 ");
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(@" and a.TRANSNO = '{0}'  ", transNo);
            }
            return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new PagerCondition(sql, "a.mdate desc,a.mtime desc", inclusive, exclusive));
        }

        public int QueryLocStorTransDetailCount(string transNo)
        {
            string sql = string.Format(@"SELECT count(*) FROM  tblstorloctransdetailcarton where  1=1 ");
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(@" and TRANSNO = '{0}'  ", transNo);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public object[] QueryStorTransDetailCarton(string transNo, string dqmCode, string fromCartonno, string locationCode, string cartonno)
        {
            string sql = string.Format(@"SELECT * FROM  tblstorloctransdetailcarton  where 1=1 ");
            if (!string.IsNullOrEmpty(transNo))
            {
                sql += string.Format(@" and TRANSNO = '{0}'  ", transNo);
            }
            if (!string.IsNullOrEmpty(dqmCode))
            {
                sql += string.Format(@" and dqmCode = '{0}'  ", dqmCode);
            }
            if (!string.IsNullOrEmpty(fromCartonno))
            {
                sql += string.Format(@" and FromCARTONNO = '{0}'  ", fromCartonno);
            }
            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(@" and LocationCode = '{0}'  ", locationCode);
            }
            if (!string.IsNullOrEmpty(cartonno))
            {
                sql += string.Format(@" and CARTONNO = '{0}'  ", cartonno);
            }
            return this.DataProvider.CustomQuery(typeof(StorloctransDetailCarton), new SQLCondition(sql));
        }

        #endregion


        #region QueryPickDetailMaterial   add by sam 2016-02-22

        public object[] QueryPickDetailMaterial(string pickNo, string pickline, string DQMcode, string locationCode, string cartonNo,
            int inclusive, int exclusive)
        {
            string sql = string.Format(@"  SELECT c.sn, a.*,b.cusitemcode,b.gfhwitemcode  FROM TBLPICKDetailMaterial a 
                left join tblpickdetail b on a.pickno=b.pickno and a.pickline=b.pickline 
                left  join TBLPICKDetailMaterialsn c on a.pickno=c.pickno and a.pickline=c.pickline  and a.cartonno=c.cartonno
                WHERE 1=1 ");

            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(pickline))
            {
                sql += string.Format(@" AND a.pickline = '{0}'", pickline);
            }

            if (!string.IsNullOrEmpty(DQMcode))
            {
                sql += string.Format(@" AND  a.DQMcode ='{0}'", DQMcode);
            }

            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(@" AND a.locationCode = '{0}'", locationCode);
            }

            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(@" AND a.cartonNo = '{0}'", cartonNo);
            }
            #endregion

            return this.DataProvider.CustomQuery(typeof(PickdetailmaterialQuery), new PagerCondition(sql, "a.MDATE DESC,a.MTIME DESC", inclusive, exclusive));
        }


        public int QueryPickDetailMaterialCount(string pickNo, string pickline, string DQMcode, string locationCode, string cartonNo)
        {
            string sql = string.Format(@" 
                    SELECT  count(1)  FROM TBLPICKDetailMaterial a   
                 left  join tblpickdetail b on a.pickno=b.pickno and a.pickline=b.pickline 
                left  join TBLPICKDetailMaterialsn c on a.pickno=c.pickno and a.pickline=c.pickline and a.cartonno=c.cartonno
                WHERE 1=1  ");

            #region
            if (!string.IsNullOrEmpty(pickNo))
            {
                sql += string.Format(" AND a.PICKNO ='{0}'", pickNo);
            }

            if (!string.IsNullOrEmpty(pickline))
            {
                sql += string.Format(@" AND a.pickline = '{0}'", pickline);
            }

            if (!string.IsNullOrEmpty(DQMcode))
            {
                sql += string.Format(@" AND  a.DQMcode ='{0}'", DQMcode);
            }

            if (!string.IsNullOrEmpty(locationCode))
            {
                sql += string.Format(@" AND a.locationCode = '{0}'", locationCode);
            }

            if (!string.IsNullOrEmpty(cartonNo))
            {
                sql += string.Format(@" AND a.cartonNo = '{0}'", cartonNo);
            }
            #endregion

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        #endregion


        #region Pickdetail
        /// <summary>
        /// TBLPICKDETAIL
        /// </summary>
        public PickDetail CreateNewPickdetail()
        {
            return new PickDetail();
        }

        public void AddPickDetail(PickDetail pickdetail)
        {
            this.DataProvider.Insert(pickdetail);
        }

        public void DeletePickDetail(PickDetail pickdetail)
        {
            this.DataProvider.Delete(pickdetail);
        }

        //add by sam 2016年3月8日
        public void DeletePickDetailByPickNo(string pickno)
        {
            string sql = string.Format(@"DELETE tblpickDetail  WHERE  pickno='{0}' ", pickno);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdatePickDetail(PickDetail pickdetail)
        {
            this.DataProvider.Update(pickdetail);
        }

        public object GetPickDetail(string Pickno, string Pickline)
        {
            return this.DataProvider.CustomSearch(typeof(PickDetail), new object[] { Pickno, Pickline });
        }

        //add by sam 2016年3月8日
        public string GetMaxPickLine(string pickno)
        {
            string sql = string.Format(@"select nvl(max(nvl(to_number(a.pickline),0)),0) pickline  from tblpickdetail a WHERE  a.pickno='{0}' ", pickno);
            object[] list = this.DataProvider.CustomQuery(typeof(PickDetail), new SQLCondition(sql));
            PickDetail detail = list[0] as PickDetail;
            if (detail != null) return detail.PickLine;
            return "0";
        }


        public int GetMaxPickLine1(string pickno)
        {
            string sql = string.Format(@"select nvl(max(nvl(to_number(a.pickline),0)),0) pickline  from tblpickdetail a WHERE  a.pickno='{0}' ", pickno);
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }

        public int QueryPickDetailCount(string Pickno, string Status)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblpickdetail where Pickno = '{0}' and Status <>'{1}' ",
                Pickno, Status)));
        }

        public int QueryPickDetailCount(string Pickno)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblpickdetail where Pickno = '{0}'",
                Pickno)));
        }



        public void UpdatePickStatusByPickno(string Pickno, string Status)
        {
            string sql = string.Format("update tblpick  set Status='{0}' where Pickno='{1}' ", Status, Pickno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }
        public void DeletePickDetail(PickDetail[] pickdetail)
        {
            this._helper.DeleteDomainObject(pickdetail);
        }

        #endregion

        #region Pickdetailmaterialsn
        /// <summary>
        /// TBLPICKDETAILMATERIALSN
        /// </summary>
        public PickDetailMaterialSn CreateNewPickDetailMaterialSn()
        {
            return new PickDetailMaterialSn();
        }

        public void AddPickDetailMaterialSn(PickDetailMaterialSn pickdetailmaterialsn)
        {
            this.DataProvider.Insert(pickdetailmaterialsn);
        }

        public void DeletePickDetailMaterialSn(PickDetailMaterialSn pickdetailmaterialsn)
        {
            this.DataProvider.Delete(pickdetailmaterialsn);
        }

        public void UpdatePickDetailMaterialSn(PickDetailMaterialSn pickdetailmaterialsn)
        {
            this.DataProvider.Update(pickdetailmaterialsn);
        }

        public object GetPickDetailMaterialSn(string Pickno, string Sn)
        {
            return this.DataProvider.CustomSearch(typeof(PickDetailMaterialSn), new object[] { Pickno, Sn });
        }

        #endregion

        public void UpdatePickStatusByPickNo(string status, string pickNo)
        {
            string sql = string.Format(@"UPDATE tblpick  SET STATUS='{0}', Down_Date=sys_date,Down_Time=sys_time WHERE  pickNo='{1}' ", status, pickNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        public void UpdatePickStatusByPickNo(string status, string pickNo, Int64 serialNumber)
        {
            string sql = string.Format(@"UPDATE tblpick  SET STATUS='{0}',serial_number=" + serialNumber + ", Down_Date=sys_date,Down_Time=sys_time WHERE  pickNo='{1}' ", status, pickNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }
        public void UpdatePickStatusByPickNo(string status, string pickNo, string downUser, int downDate, int downTime)
        {
            string sql = string.Format(@"UPDATE tblpick  SET STATUS='{0}' , Down_User='{1}' ,  Down_Date='{2}' ,  Down_Time='{3}'    WHERE  pickNo='{4}' ", status, downUser, downDate, downTime, pickNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdatePickDetailStatusByPickNo(string status, string pickNo, string oldstatus)
        {
            string sql = string.Format(@"UPDATE tblpickdetail  SET STATUS='{0}' WHERE  pickNo='{1}'  and STATUS='{2}' ", status, pickNo, oldstatus);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdatePickDetailStatusByPickNoNotCancel(string status, string pickNo)
        {
            string sql = string.Format(@"UPDATE tblpickdetail  SET STATUS='{0}' WHERE  pickNo='{1}'  and STATUS  not in ('Cancel', 'Close' )  ", status, pickNo);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void UpdateOQCStatusByPickno(string status, string pickno)
        {
            string sql = string.Format(@" update TBLOQC  set Status='{0}',mdate=sys_date,mtime=sys_time 
                     where pickno='{1}'  ", status, pickno);
            SQLCondition SC = new SQLCondition(sql);
            this.DataProvider.CustomExecute(SC);
        }

        #region Polog add by sam 2016年3月20日18:20:17
        /// <summary>
        /// TBLPOLOG
        /// </summary>
        public PoLog CreateNewPoLog()
        {
            return new PoLog();
        }

        public void AddPoLog(PoLog polog)
        {
            this.DataProvider.Insert(polog);
        }

        public void DeletePoLog(PoLog polog)
        {
            this.DataProvider.Delete(polog);
        }

        public void UpdatePoLog(PoLog polog)
        {
            string sql = string.Format("  update   tblpolog  set sapmaterialinvoice='{0}' , sapreturn='{1}' ,message='{2}' where serial={3}",
                polog.SAPMaterialInvoice, polog.SapReturn, polog.Message, polog.Serial);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object GetPoLog(int serial)
        {
            string sql = string.Format("select a.* from tblPoLog a where 1=1 and a.Serial={0} ", serial);
            object[] list = this.DataProvider.CustomQuery(typeof(PoLog), new SQLCondition(sql));
            if (list != null)
            {
                PoLog poLog = list[0] as PoLog;
                return poLog;
            }
            return null;
        }

        public int GetMaxSerialInPoLog()
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(PoLog), new SQLCondition("select max(Serial) as Serial from tblPoLog"));
            if (obj != null)
            {
                return ((PoLog)obj[0]).Serial;
            }
            return 0;
        }

        public object GetPoLog()
        {
            return this.DataProvider.CustomSearch(typeof(PoLog), new object[] { });
        }
        public object GetPoLog(string pono, string poline, string serialno, string status)
        {
            string sql = string.Format("select a.* from tblPoLog a where 1=1 and SapReturn='S'");
            if (!string.IsNullOrEmpty(pono))
            {
                sql += string.Format(@" and a.pono = '{0}'  ", pono);
            }
            if (!string.IsNullOrEmpty(poline))
            {
                sql += string.Format(@" and a.poline = '{0}'  ", poline);
            }
            if (!string.IsNullOrEmpty(serialno))
            {
                sql += string.Format(@" and a.SERIALNO = '{0}'  ", serialno);
            }
            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" and a.status = '{0}'  ", status);
            }
            object[] list = this.DataProvider.CustomQuery(typeof(PoLog), new SQLCondition(sql));
            if (list != null)
            {
                PoLog poLog = list[0] as PoLog;
                return poLog;
            }
            return null;
        }

        public string GetPo103Invoices(string pono, string poline, string serialno)
        {

            string sql = string.Format("select a.SAPMATERIALINVOICE from tblpo2sap a where 1=1 and SapReturn='S' and status='103'");
            if (!string.IsNullOrEmpty(pono))
            {
                sql += string.Format(@" and a.pono = '{0}'  ", pono);
            }
            if (!string.IsNullOrEmpty(poline))
            {
                sql += string.Format(@" and a.poline = {0}  ", poline);
            }
            if (!string.IsNullOrEmpty(serialno))
            {
                sql += string.Format(@" and a.SERIALNO = '{0}'  ", serialno);
            }

            object[] list = this.DataProvider.CustomQuery(typeof(PoLog), new SQLCondition(sql));
            if (list != null)
            {
                PoLog poLog = list[0] as PoLog;
                return poLog.SAPMaterialInvoice;
            }
            return null;
        }

        #endregion

        public AsnIQCDetailEc[] GetTBLASNIQCDETAILEC(string iqcNo)
        {

            string sql = string.Format(@"select a.* from TBLASNIQCDETAILEC a where iqcno='{0}' ", iqcNo);
            object[] ooo = this.DataProvider.CustomQuery(typeof(AsnIQCDetailEc), new SQLCondition(sql));
            List<AsnIQCDetailEc> l = new List<AsnIQCDetailEc>();
            if (ooo != null && ooo.Length > 0)
            {
                foreach (AsnIQCDetailEc ec in ooo)
                    l.Add(ec);
            }
            return l.ToArray();

        }

        public AsnIQCDetailEc[] GetToSapIQCDetailEC(string iqc, string status)
        {
            return null;
        }

        public Asndetailsn[] GetAsndetailsn1(string stNo, string stline)
        {
            string sql = "select a.* from TBLASNDETAILSN a where a.STNO='" + stNo + "' and STLINE='" + stline + "'";
            object[] ooo = this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
            List<Asndetailsn> l = new List<Asndetailsn>();
            if (ooo != null && ooo.Length > 0)
            {
                foreach (Asndetailsn ec in ooo)
                    l.Add(ec);
            }
            return l.ToArray();
        }

        public Asndetail[] QueryASNDetailFromSTNO(string stNo)
        {
            string sql = "select a.* from TBLASNDETAIL a where a.STNO='" + stNo + "'";
            object[] ooo = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));
            List<Asndetail> l = new List<Asndetail>();
            if (ooo != null && ooo.Length > 0)
            {
                foreach (Asndetail ec in ooo)
                    l.Add(ec);
            }
            return l.ToArray();

        }

        public void RejectASNDetails(string stNo, List<string> stLines, string reason)
        {

            string sql = "UPDATE TBLASNDETAIL SET STATUS = '" + ASNDetail_STATUS.ASNDetail_ReceiveClose + "',INITRECEIVESTATUS='" + SAP_LineStatus.SAP_LINE_REJECT + "',INITRECEIVEDESC='" + reason + "' WHERE ";
            sql += "STNO='" + stNo + "' AND STLINE in(" + SqlFormat(stLines) + ")";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public void RejectASNDetailItems(string stNo, List<string> stLines)
        {
            string sql = "UPDATE tblasndetailitem SET RECEIVEQTY=0  WHERE STNO='" + stNo + "' AND STLINE in (" + SqlFormat(stLines) + ")";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public string SqlFormat(List<string> strs)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            foreach (string str in strs)
            {
                sb.Append("'");
                sb.Append(str);
                sb.Append("',");

            }
            return sb.ToString().TrimEnd(',');
        }


        public void RejectAllRecvAsnDetails(string reason, string stno)
        {
            string sql = "UPDATE tblasndetail SET STATUS='ReceiveClose',INITRECEIVESTATUS='Reject',INITRECEIVEDESC='" + reason + "' where status='Receive' and stno='" + stno + "' and CARTONNO is not null";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public int GetAsnDetailCountForRecClose(string stno)
        {
            string sql = " SELECT count(*) FROM tblasndetail  where STNO='" + stno + "' AND status='ReceiveClose'";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetAsnDetailCountForRec(string stno)
        {

            string sql = " SELECT count(*) FROM tblasndetail  where STNO='" + stno + "' AND status='Receive'";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetAsnDetailCountForNotHaveCartonno(string stno)
        {
            string sql = " SELECT count(*) FROM tblasndetail  where STNO='" + stno + "' AND CARTONNO is not null and status<>'ReceiveClose'";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public bool IsFirstCreateAsn(string invNo)
        {
            string sql = " SELECT COUNT(*) FROM TBLASN  where INVNO='" + invNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql)) <= 0;

        }

        public bool CheckASNDetailsStatus(string stno)
        {
            string sql = "select count(*) from TBLASNDETAIL a where STNO='" + stno + "' and STATUS<>'ReceiveClose'";

            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            if (count > 0)
                return true;
            else
                return false;
        }

        public void UpdateASNDetail(string stno, string status)
        {


            string sql = "UPDATE TBLASNDETAIL SET STATUS='" + status + "',CARTONNO='',InitReceiveStatus='',ReceiveQty=0 WHERE  STNO ='" + stno + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }

        public void UpdateASNDetailSN(string stno)
        {


            string sql = "UPDATE TBLASNDETAILSN SET CARTONNO='' WHERE  STNO ='" + stno + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));

        }


        public void UpdateASNDetailItem(string stno)
        {
            string sql = "UPDATE TBLASNDETAILITEM SET ReceiveQty=0 WHERE  STNO ='" + stno + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public List<SumItemsQTY> SumItemsWithInvNoAdnLineBy(string invNo, string stno)
        {
            string sql = string.Format(@" select a.QCPASSQTY,a.ReceiveQTY,a.ACTQTY,b.DNBATCHNO,a.invno,a.invline from (select sum（QCPASSQTY） as QCPASSQTY ,sum(ReceiveQTY) as ReceiveQTY , sum(ACTQTY) as ACTQTY,invno,invline from tblasndetailitem a where invNo='{0}' and stno='{1}' group by invno,invline) a,(select t1.DNBATCHNO,t1.INVNO from TBLInvoices t1,TBLASN t2  where t1.INVNO=t2.INVNO and stno='{1}') b where a.invno= b.invno ", invNo, stno);
            List<SumItemsQTY> ll = new List<SumItemsQTY>();
            object[] objs = this.DataProvider.CustomQuery(typeof(SumItemsQTY), new SQLCondition(sql));
            if (objs == null)
                throw new Exception(invNo + "SAP单据不存在!");
            foreach (SumItemsQTY item in objs)
                ll.Add(item);

            return ll;
        }

        private string SqlFormat(string[] strs)
        {
            if (strs.Length == 0)
                return "''";
            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            foreach (string str in strs)
            {
                sb.Append("'");
                sb.Append(str);
                sb.Append("',");

            }
            return sb.ToString().TrimEnd(',');
        }


        public bool CartonnoIsNotRepeat(string cartonno)
        {
            string sql = "SELECT COUNT(*) FROM tblasndetail WHERE CARTONNO='" + cartonno + "' AND STATUS<>'Close'";

            int count = this.DataProvider.GetCount(new SQLCondition(sql));


            if (count > 0)
            {
                sql = "SELECT COUNT(*) FROM TBLASNDETAIL A, TBLASN B WHERE B.STATUS='IQCRejection' AND A.CARTONNO='" + cartonno + "'";
                count = this.DataProvider.GetCount(new SQLCondition(sql));
                if (count > 0)
                {
                    return true;
                }
                else
                {

                    sql = "SELECT COUNT(*) FROM TBLASNDETAIL A, TBLASN B WHERE B.STATUS='" + ASN_STATUS.ASN_ReceiveRejection + "' AND A.CARTONNO='" + cartonno + "'";
                    count = this.DataProvider.GetCount(new SQLCondition(sql));
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
            }
            else
                return true;

        }

        #region DN2Sap
        /// <summary>
        /// TBLDN2Sap
        /// </summary>
        public DN2Sap CreateNewDN2Sap()
        {
            return new DN2Sap();
        }

        public void AddDN2Sap(DN2Sap DN2Sap)
        {
            this.DataProvider.Insert(DN2Sap);
        }

        public void DeleteDN2Sap(DN2Sap DN2Sap)
        {
            this.DataProvider.Delete(DN2Sap);
        }

        public void UpdateDN2Sap(DN2Sap DN2Sap)
        {
            this.DataProvider.Update(DN2Sap);
        }

        public object GetDN2Sap()
        {
            return this.DataProvider.CustomSearch(typeof(DN2Sap), new object[] { });
        }

        #endregion

        #region Po2Sap
        /// <summary>
        /// TBLPo2Sap
        /// </summary>
        public Po2Sap CreateNewPo2Sap()
        {
            return new Po2Sap();
        }

        public void AddPo2Sap(Po2Sap Po2Sap)
        {
            this.DataProvider.Insert(Po2Sap);
        }

        public void DeletePo2Sap(Po2Sap Po2Sap)
        {
            this.DataProvider.Delete(Po2Sap);
        }

        public void UpdatePo2Sap(Po2Sap Po2Sap)
        {
            this.DataProvider.Update(Po2Sap);
        }

        public object GetPo2Sap()
        {
            return this.DataProvider.CustomSearch(typeof(Po2Sap), new object[] { });
        }

        #endregion

        #region Rs2Sap
        /// <summary>
        /// TBLRs2Sap
        /// </summary>
        public Rs2Sap CreateNewRs2Sap()
        {
            return new Rs2Sap();
        }

        public void AddRs2Sap(Rs2Sap Rs2Sap)
        {
            this.DataProvider.Insert(Rs2Sap);
        }

        public void DeleteRs2Sap(Rs2Sap Rs2Sap)
        {
            this.DataProvider.Delete(Rs2Sap);
        }

        public void UpdateRs2Sap(Rs2Sap Rs2Sap)
        {
            this.DataProvider.Update(Rs2Sap);
        }

        public object GetRs2Sap()
        {
            return this.DataProvider.CustomSearch(typeof(Rs2Sap), new object[] { });
        }

        #endregion

        #region Wwpo2Sap
        /// <summary>
        /// TBLWwpo2Sap
        /// </summary>
        public Wwpo2Sap CreateNewWwpo2Sap()
        {
            return new Wwpo2Sap();
        }

        public void AddWwpo2Sap(Wwpo2Sap Wwpo2Sap)
        {
            this.DataProvider.Insert(Wwpo2Sap);
        }

        public void DeleteWwpo2Sap(Wwpo2Sap Wwpo2Sap)
        {
            this.DataProvider.Delete(Wwpo2Sap);
        }

        public void UpdateWwpo2Sap(Wwpo2Sap Wwpo2Sap)
        {
            this.DataProvider.Update(Wwpo2Sap);
        }

        public object GetWwpo2Sap()
        {
            return this.DataProvider.CustomSearch(typeof(Wwpo2Sap), new object[] { });
        }

        #endregion

        #region Dn_in2Sap
        /// <summary>
        /// TBLDn_in2Sap
        /// </summary>
        public Dn_in2Sap CreateNewDn_in2Sap()
        {
            return new Dn_in2Sap();
        }

        public void AddDn_in2Sap(Dn_in2Sap Dn_in2Sap)
        {
            this.DataProvider.Insert(Dn_in2Sap);
        }

        public void DeleteDn_in2Sap(Dn_in2Sap Dn_in2Sap)
        {
            this.DataProvider.Delete(Dn_in2Sap);
        }

        public void UpdateDn_in2Sap(Dn_in2Sap Dn_in2Sap)
        {
            this.DataProvider.Update(Dn_in2Sap);
        }

        public object GetDn_in2Sap()
        {
            return this.DataProvider.CustomSearch(typeof(Dn_in2Sap), new object[] { });
        }

        #endregion

        #region Ub2Sap
        /// <summary>
        /// TBLUb2Sap
        /// </summary>
        public Ub2Sap CreateNewUb2Sap()
        {
            return new Ub2Sap();
        }

        public void AddUb2Sap(Ub2Sap Ub2Sap)
        {
            this.DataProvider.Insert(Ub2Sap);
        }

        public void DeleteUb2Sap(Ub2Sap Ub2Sap)
        {
            this.DataProvider.Delete(Ub2Sap);
        }

        public void UpdateUb2Sap(Ub2Sap Ub2Sap)
        {
            this.DataProvider.Update(Ub2Sap);
        }

        public object GetUb2Sap()
        {
            return this.DataProvider.CustomSearch(typeof(Ub2Sap), new object[] { });
        }

        #endregion

        #region StockScrap2Sap
        /// <summary>
        /// TBLStockScrap2Sap
        /// </summary>
        public StockScrap2Sap CreateNewStockScrap2Sap()
        {
            return new StockScrap2Sap();
        }

        public void AddStockScrap2Sap(StockScrap2Sap StockScrap2Sap)
        {
            this.DataProvider.Insert(StockScrap2Sap);
        }

        public void DeleteStockScrap2Sap(StockScrap2Sap StockScrap2Sap)
        {
            this.DataProvider.Delete(StockScrap2Sap);
        }

        public void UpdateStockScrap2Sap(StockScrap2Sap StockScrap2Sap)
        {
            this.DataProvider.Update(StockScrap2Sap);
        }

        public object GetStockScrap2Sap(int Serial)
        {
            return this.DataProvider.CustomSearch(typeof(StockScrap2Sap), new object[] { Serial });
        }

        #endregion

        public object[] QueryASN1(string stNo, string stType, string invNo, string cUser, string status, int bCDate, int eCDate, int inclusive, int exclusive)
        {

            string sql = @"select /*+ leading(storagecode,) */  ROW_NUMBER() OVER (ORDER BY  a.STNO DESC) as rid,A.STNO,A.STTYPE,A.INVNO, a.volume, a.initreceivedesc,  a.gross_weight,  a.fromstoragecode,  a.fromfaccode,  a.muser,  a.oano,  a.initreceiveqty,  a.pickno,  a.predict_date,  a.cuser,  a.faccode,  a.status,  a.cdate,  a.reworkapplyuser,  a.mtime,  a.initrejectqty,  a.provide_date,  a.direct_flag,  a.exigency_flag,  a.packinglistno,  a.initgiveinqty,  a.rejects_flag,  a.mdate,  a.storagecode,  a.ctime,  a.remark1, c.VENDORCODE  FROM TBLASN a LEFT JOIN TBLINVOICES b ON  a.INVNO=b.invno LEFT JOIN TBLVENDOR C ON b.VENDORCODE=c.VENDORCODE  WHERE  a.STTYPE IN ('POR','SCTR')";

            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND  a.STNO ='{0}'", stNo);
            }
            if (!string.IsNullOrEmpty(stType.Trim()))
            {
                if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
                {
                    sql += string.Format(@" AND  a.STTYPE IN ({0})", stType);
                }
                else
                {
                    sql += string.Format(@" AND  a.STTYPE IN ('{0}')", stType);
                }
            }

            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND  a.INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(cUser))
            {
                sql += string.Format(@" AND  a.CUSER = '{0}'", cUser);
            }
            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND  a.STATUS = '{0}'", status);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND  a.CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND  a.CDATE <= {0}", eCDate);
            }

            sql = "select b.rid, b.STNO,b.STTYPE,b.INVNO, b.volume,b.initreceivedesc, b.gross_weight, b.fromstoragecode, b.fromfaccode, b.muser, b.oano,  b.initreceiveqty, b.pickno,  b.predict_date,  b.cuser,  b.faccode,  b.status,  b.vendorcode,  b.cdate,  b.reworkapplyuser,  b.mtime,  b.invno,  b.initrejectqty,  b.provide_date,  b.sttype,  b.direct_flag,  b.exigency_flag,  b.packinglistno,  b.initgiveinqty, b.rejects_flag,b.FACCODE,  b.mdate, b.storagecode,b.ctime, b.remark1,b.VENDORCODE  from (" + sql + " ) B  where rid between " + inclusive + " and " + exclusive;

            return this.DataProvider.CustomQuery(typeof(ASN), new SQLCondition(sql));
        }



        public int QueryASN1Count(string stNo, string stType, string invNo, string cUser, string status, int bCDate, int eCDate)
        {
            string sql = @" SELECT  count(*)  FROM TBLASN a LEFT JOIN TBLINVOICES b ON  a.INVNO=b.invno LEFT JOIN TBLVENDOR C ON b.VENDORCODE=c.VENDORCODE  WHERE  a.STTYPE IN ('POR','SCTR')";

            if (!string.IsNullOrEmpty(stNo))
            {
                sql += string.Format(" AND  a.STNO ='{0}'", stNo);
            }
            if (!string.IsNullOrEmpty(stType))
            {
                if (!string.IsNullOrEmpty(stType) && stType.IndexOf(",") > 0)
                {
                    sql += string.Format(@" AND  a.STTYPE IN ({0})", stType);
                }
                else
                {
                    sql += string.Format(@" AND  a.STTYPE IN ('{0}')", stType);
                }
            }

            if (!string.IsNullOrEmpty(invNo))
            {
                sql += string.Format(@" AND  a.INVNO = '{0}'", invNo);
            }

            if (!string.IsNullOrEmpty(cUser))
            {
                sql += string.Format(@" AND  a.CUSER = '{0}'", cUser);
            }
            if (!string.IsNullOrEmpty(status))
            {
                sql += string.Format(@" AND  a.STATUS = '{0}'", status);
            }
            if (bCDate > 0)
            {
                sql += string.Format(@" AND  a.CDATE >= {0}", bCDate);
            }
            if (eCDate > 0)
            {
                sql += string.Format(@" AND  a.CDATE <= {0}", eCDate);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] GetItemsFromInvline(string invline)
        {
            string sql = string.Format(@"select {0} from tblasndetailitem where invline='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailitem)), invline);
            return this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));

        }


        public object[] GetAllInvsFromStnoAndStLie(string stno, string stLine)
        {
            string sql = string.Format(@"select {0} from tblasndetailitem where STNO='{1}' and stline='{2}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Asndetailitem)), stno, stLine);
            return this.DataProvider.CustomQuery(typeof(Asndetailitem), new SQLCondition(sql));

        }


        public Asndetail[] GetAllAsnDetailNotReject(string stno)
        {
            string sql = "select a.* from tblasndetail where stno='" + stno + "' and (InitReceiveStatus='Receive' or InitReceiveStatus='Givein')";

            object[] os = this.DataProvider.CustomQuery(typeof(Asndetail), new SQLCondition(sql));

            List<Asndetail> a = new List<Asndetail>();
            if (os != null && os.Length > 0)
            {
                foreach (Asndetail o in os)
                {
                    a.Add(o);

                }

            }

            return a.ToArray();

        }


        public int NotHavCatonnoCount(string stno)
        {
            string sql = "select count(*) from tblasndetail where stno='" + stno + "' and CARTONNO is null";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public void UpdateAsnDetailToRec(string stno)
        {

            string sql = "UPDATE TBLASN SET InitReceiveDesc = null,InitReceiveQty=0, InitGiveinQty=0,InitRejectQty=0,STATUS='" + ASNHeadStatus.Receive + "' "
                          + " WHERE STNO = '" + stno + "'";

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        public object[] QueryASNDetailSNOrCode(string SNCode, string stno)
        {
            string sql = @"select stline,stno from 
                        ( SELECT a.stline,a.stno FROM 
                          tblasndetailsn a,
                          TBLASNDETAIL b,
                          TBLMATERIAL c where  a.stno='" + stno + "' and a.sn = '" + SNCode + "' and b.cartonno is null and a.stno=b.stno and a.stline=b.stline and b.mcode=c.mcode and c.MCONTROLTYPE='item_control_keyparts'  union select stline,stno from TBLASNDETAIL a,TBLMATERIAL b where (a.DQMCODE LIKE '%" + SNCode + "%' or a.VENDORMCODE LIKE '%" + SNCode + "%') and a.cartonno is null and a.stno='" + stno + "' and a.mcode=b.mcode and b.MCONTROLTYPE<>'item_control_keyparts' ) b group by stno,stline";
            return this.DataProvider.CustomQuery(typeof(Asndetailsn), new SQLCondition(sql));
        }

        public SpecInOut QuerySpecInOuts(string dqmcode, string storageCode, string locationCode)
        {
            string sql = "SELECT * FROM TBLSPECINOUT WHERE StorageCode='" + storageCode + "' AND LocationCode='" + locationCode + "' AND DQMCODE='" + dqmcode + "' AND MOVETYPE='I'";
            object[] objs = this.DataProvider.CustomQuery(typeof(SpecInOut), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((SpecInOut)objs[0]);
            return null;


        }

        public AsnIQC QueryAsnIqc(string dqmcode, string stno)
        {
            string sql = "SELECT * FROM TBLASNIQC WHERE DQMCODE='" + dqmcode + "' AND stno='" + stno + "' AND STATUS<>'Cancel'";
            object[] objs = this.DataProvider.CustomQuery(typeof(AsnIQC), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((AsnIQC)objs[0]);
            return null;


        }



        public string GetRejectDESC(string value)
        {
            string sql = "select a.* from tblsysparam a where a.paramgroupcode='REJECTRESULT' AND PARAMCODE='" + value + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));

            if (objs != null && objs.Length > 0)
                return ((Parameter)objs[0]).ParameterDescription;
            return string.Empty;
        }

        public int GetPickDetailMaterialsCount(string pickNo, string pickLine)
        {
            string sql = @"SELECT COUNT(*) FROM  TBLPICKDETAILMATERIAL WHERE PICKNO='" + pickNo + "' AND PICKLINE='" + pickLine + "' ";


            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public int GetPickDetailCount(string pickNo)
        {
            string sql = "select count(*) from tblpickDetail where pickno='" + pickNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public int GetPickDetailMaterialCount(string pickNo)
        {
            string sql = "select count(*) from TBLPICKDetailMaterial where pickno='" + pickNo + "'";
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }
    }

    #region Common Class

    public class SumItemsQTYDTO
    {
        public string Unit;
        public List<SumItemsQTY> Sums;
    }

    public class AsnHead
    {
        public string STlINE;
        public string STNO;
    }

    public class SumItemsQTY : BenQGuru.eMES.Common.Domain.DomainObject
    {
        [FieldMapAttribute("QCPASSQTY", typeof(int), 40, true)]
        public int QCPASSQTY;

        [FieldMapAttribute("ReceiveQTY", typeof(int), 40, true)]
        public int ReceiveQTY;

        [FieldMapAttribute("ACTQTY", typeof(int), 40, true)]
        public int ACTQTY;

        [FieldMapAttribute("DNBATCHNO", typeof(string), 40, true)]
        public string DNBATCHNO;

        [FieldMapAttribute("invno", typeof(string), 40, true)]
        public string InvNo;

        [FieldMapAttribute("invline", typeof(string), 40, true)]
        public string InvLine;
    }



    //入库类型 add by jinger 2016-01-31
    /// <summary>
    /// 入库类型
    /// </summary>
    [Serializable]
    public class InInvType
    {
        /// <summary>
        /// PO入库
        /// </summary>
        public static readonly string POR = "POR";

        /// <summary>
        /// 退货入库
        /// </summary>
        public static readonly string DNR = "DNR";

        /// <summary>
        /// 调拨
        /// </summary>
        public static readonly string UB = "UB";

        /// <summary>
        /// 检测返工入库
        /// </summary>
        public static readonly string JCR = "JCR";

        /// <summary>
        /// 不良品入库
        /// </summary>
        public static readonly string BLR = "BLR";

        /// <summary>
        /// CLAIM入库
        /// </summary>
        public static readonly string CAR = "CAR";

        /// <summary>
        /// 研发入库
        /// </summary>
        public static readonly string YFR = "YFR";

        /// <summary>
        /// 盘点
        /// </summary>
        public static readonly string PD = "PD";

        /// <summary>
        /// PGI退料
        /// </summary>
        public static readonly string PGIR = "PGIR";

        /// <summary>
        /// 生产退料
        /// </summary>
        public static readonly string SCTR = "SCTR";
    }

    //单据状态 add by jinger 2016-01-31
    /// <summary>
    /// 单据状态
    /// </summary>
    [Serializable]
    public class Status
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public static readonly string Release = "Release";

        /// <summary>
        /// 待收货
        /// </summary>
        public static readonly string WaitReceive = "WaitReceive";

        /// <summary>
        /// 入库
        /// </summary>
        public static readonly string Close = "Close";

        /// <summary>
        /// 上架
        /// </summary>
        public static readonly string OnLocation = "OnLocation";
    }

    //入库指令单据头状态 add by jinger 2016-01-31
    /// <summary>
    /// 入库指令单据头状态
    /// </summary>
    [Serializable]
    public class ASNHeadStatus : Status
    {
        /// <summary>
        /// 初检
        /// </summary>
        public static readonly string Receive = "Receive";

        /// <summary>
        /// 初检拒收
        /// </summary>
        public static readonly string ReceiveRejection = "ReceiveRejection";

        /// <summary>
        /// IQC
        /// </summary>
        public static readonly string IQC = "IQC";

        /// <summary>
        /// IQC拒收
        /// </summary>
        public static readonly string IQCRejection = "IQCRejection";

        public static readonly string WaitReceive = "WaitReceive"; //add by sam

    }

    //入库指令单据行状态 add by jinger 2016-01-31
    /// <summary>
    /// 入库指令单据行状态
    /// </summary>
    [Serializable]
    public class ASNLineStatus : Status
    {

        /// <summary>
        /// 初检完成
        /// </summary>
        public static readonly string ReceiveClose = "ReceiveClose";

        /// <summary>
        /// IQC完成
        /// </summary>
        public static readonly string IQCClose = "IQCClose";

        /// <summary>
        /// 取消
        /// </summary>
        public static readonly string Cancel = "Cancel";
    }

    //单据文件类型 add by jinger 2016-01-31
    /// <summary>
    /// 单据文件类型
    /// </summary>
    [Serializable]
    public class InvDocType
    {
        /// <summary>
        /// 供应商直发签收文件
        /// </summary>
        public static readonly string DirectSign = "DirectSign";

        /// <summary>
        /// 初检拒收文件
        /// </summary>
        public static readonly string InitReject = "InitReject";

        /// <summary>
        /// 初检让步接收文件
        /// </summary>
        public static readonly string InitGivein = "InitGivein";
    }


    [Serializable]
    public class InvRCardForQuery : InvRCard
    {
        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("PLANQTY", typeof(decimal), 10, true)]
        public decimal PlanQty;

        /// <summary>
        /// 
        /// </summary>
        [FieldMapAttribute("ACTQTY", typeof(decimal), 10, true)]
        public decimal ActQty;

    }

    public enum CollectionType
    {
        Planate, PCS, Carton
    }

    [Serializable]
    public class SwitchType
    {
        public static readonly string UnnormalToNormal = "UnnormalToNormal";
        public static readonly string NormalToUnnormal = "NormalToUnnormal";
    }

    [Serializable]
    public class CollectType
    {
        public const string RevNO = "RevNO";
        public const string Carton = "Carton";
        public const string PCS = "PCS";
    }

    [Serializable]
    public class ReceiveInnerType
    {
        public static string Normal = "Normal";
        public static string Abnormal = "Abnormal";
    }

    [Serializable]
    public class RCardStatus
    {
        public static string Received = "Received";
        public static string Shipped = "Shipped";
        public static string GetName(string code)
        {
            if (code == Received)
            {
                return "未出货";
            }
            else
                return "已出货";
        }
    }

    [Serializable]
    public class ReceiveStatus
    {
        public static string Receiving = "Receiving";
        public static string Received = "Received";
    }

    [Serializable]
    public class ShipStatus
    {
        public static string Shipping = "Shipping";
        public static string Shipped = "Shipped";
    }

    public class RCardExistException : Exception
    {
        public RCardExistException(string RCard)
            : base(RCard + "已经存在此单据中了")
        {
        }
    }

    public class NoAllowDeleteException : Exception
    {
        public NoAllowDeleteException()
            : base("只有初始状态才能删除!")
        {

        }
    }

    public class NoAllowAddException : Exception
    {
        public NoAllowAddException()
            : base("只有初始状态才能序添加列号!")
        {

        }
    }

    public class NoAllowException : Exception
    {
        public NoAllowException()
            : base("只有初始状态才允许操作!")
        {

        }
    }

    public class NoDelShippedRCardException : Exception
    {
        public NoDelShippedRCardException(string rcard)
            : base(rcard + "序列号已经出货，不允许删除")
        {
        }

    }

    public class GreaterException : Exception
    {
        public GreaterException()
            : base("实际数量已经达到了计划数量")
        { }
    }

    [Serializable]
    public class SimulateResult
    {
        public string ItemCode;
        public string MOCode;
        public string RunningCard;
        public bool IsCompleted;
        public bool IsInv; //已经入库了
        public bool IsClosed; //工单是否已经关闭
        public bool IsTrans = false;
        public bool IsDeleted = false;//不满条件的进行删除
    }

    [Serializable]
    public class UIShipRCard
    {
        private string _rcard;
        private string _c_type;

        public UIShipRCard(string rcard, string c_type)
        {
            _rcard = rcard;
            _c_type = c_type;
        }

        public override string ToString()
        {
            return _rcard;
        }

        public string C_type
        {
            get { return _c_type; }
        }

        public override bool Equals(object obj)
        {
            UIShipRCard r = obj as UIShipRCard;
            if (r == null) return false;

            return this._rcard == r._rcard;
        }

        public override int GetHashCode()
        {
            return _rcard.GetHashCode();
        }




    }

    #endregion
}


