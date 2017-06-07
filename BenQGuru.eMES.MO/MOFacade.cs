#region System
using System;
using System.Text;
using System.Runtime.Remoting;
using System.Collections;
#endregion

#region project
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;
using BenQGuru.eMES.Domain.SMT;
#endregion


/// MOFacade 的摘要说明。
/// 文件名:
/// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
/// 创建人:Crystal Chu
/// 创建日期:2005/03/22
/// 修改人: Crystal chu
/// 修改日期: 2005/4/20
/// 描 述: 对工单的操作控制
///        Crystal chu 2005/4/20 工单release,pending->open的时候进行工单版本的升级
///        Crystal chu 2005/4/20  IsOPBOMUsed 检查opBOM是否有被使用的时候工单的状态必须是open    
///        Crystal chu 2005/04/29 增加工单BOM与OPBOM的比对                 
///          
/// /// 版 本:	
/// </summary>  
namespace BenQGuru.eMES.MOModel
{
    public class MOFacade : MarshalByRefObject
    {
        //private static readonly log4net.ILog _log = BenQGuru.eMES.Common.Log.GetLogger(typeof(MOFacade));
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public const string ISMAINROUTE_TRUE = "1";

        public MOFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        //Laws Lu,max life time to unlimited
        public override object InitializeLifetimeService()
        {
            return null;
        }


        public MO CreateNewMO()
        {
            MO mo = new MO();
            mo.MOStatus = MOManufactureStatus.MOSTATUS_INITIAL;
            return mo;
        }
        //Laws Lu,2006/07/19 recover the default construction function
        public MOFacade()
        {
        }

        protected IDomainDataProvider DataProvider
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


        private string[] canModifyStatus = new string[] { MOManufactureStatus.MOSTATUS_INITIAL, MOManufactureStatus.MOSTATUS_PENDING };

        #region Begin for MO2SAP
        public void AddMO2SAP(MO2SAP mo2sap)
        {
            this.DataProvider.Insert(mo2sap);
        }

        public void UpdateMO2SAP(MO2SAP mo2sap)
        {
            this.DataProvider.Update(mo2sap);
        }

        public void DeleteMO2SAP(MO2SAP mo2sap)
        {
            this.DataProvider.Delete(mo2sap);
        }

        public object GetMO2SAP(string moCode, decimal postSeq)
        {
            return this._domainDataProvider.CustomSearch(typeof(MO2SAP), new object[] { moCode, postSeq });
        }

        public object[] GetMO2SAPList(string moCode, int inclusive, int exclusive)
        {
            string sql = "SELECT {0} FROM tblMO2SAP WHERE mocode='" + moCode.ToUpper() + "'";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2SAP)));

            return this.DataProvider.CustomQuery(typeof(MO2SAP), new PagerCondition(sql, "postseq", inclusive, exclusive));
        }

        public object GetMO2SAPSumQty(string moCode)
        {
            string sql;
            sql = "";
            sql += "SELECT NVL (SUM (moproduced), 0) AS moproduced,";
            sql += "       NVL (SUM (moscrap), 0) AS moscrap";
            sql += "  FROM tblmo2sap";
            sql += " WHERE mocode = '" + moCode.ToUpper() + "'";

            object[] mo2sap = this.DataProvider.CustomQuery(typeof(MO2SAP), new SQLCondition(sql));
            return mo2sap[0];
        }

        public object GetMO2SAPMaxPostSeq(string moCode)
        {
            string sql = "SELECT NVL(MAX(postseq), 0) AS POSTSEQ FROM tblmo2sap WHERE mocode='" + moCode.ToUpper() + "'";
            object[] mo2sap = this.DataProvider.CustomQuery(typeof(MO2SAP), new SQLCondition(sql));
            return mo2sap[0];
        }

        public void UpdateMO2SAPFlag(string moCode, decimal postSeq)
        {
            string sql = "UPDATE tblmo2sap SET flag='SAP' WHERE mocode='" + moCode + "' AND postseq=" + postSeq;
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        #endregion

        #region MO2SAPDetail
        public void AddMO2SAPDetail(MO2SAPDetail mo2sapdetail)
        {
            this.DataProvider.Insert(mo2sapdetail);
        }

        public void UpdateMO2SAPDetail(MO2SAPDetail mo2sapdetail)
        {
            this.DataProvider.Update(mo2sapdetail);
        }

        public void DeleteMO2SAPDetail(MO2SAPDetail mo2sapdetail)
        {
            this.DataProvider.Delete(mo2sapdetail);
        }

        public object GetMO2SAPDetail(string moCode, string rcard)
        {
            return this._domainDataProvider.CustomSearch(typeof(MO2SAPDetail), new object[] { moCode, rcard });
        }

        public object[] QueryMO2SAPDetailList(string moCode)
        {
            //string sql = "SELECT A.*  FROM (SELECT M.MOCODE AS MOCODE, 0 AS POSTSEQ,COUNT(S.SERIALNO) AS MOPRODUCED,";
            //sql += " 0 AS MOSCRAP,'' AS MOCONFIRM,0 AS MOMANHOUR,0 AS MOMACHINEHOUR,0 AS MOCLOSEDATE,G.SAPSTORAGE AS MOLOCATION,";
            //sql += " S.ITEMGRADE AS MOGRADE,M.MOOP AS MOOP, '' AS FLAG, '' AS ERRORMESSAGE, '' AS MUSER,0 AS MDATE,";
            //sql += " 0 AS MTIME, '' AS EATTRIBUTE1,0 AS ORGID";
            //sql += " FROM TBLSTACK2RCARD   S,  TBLSTORAGE   G, TBLSIMULATIONREPORT T,TBLMO  M, tblinvintransaction i ";
            //sql += " WHERE S.STORAGECODE = G.STORAGECODE AND s.serialno = t.rcard AND i.serial = s.transinserial AND i.mocode = t.mocode ";
            //sql += " AND T.ISCOM = 1 AND M.MOCODE = T.MOCODE AND M.itemcode=s.itemcode AND T.MOCODE = '" + moCode.Trim().ToUpper() + "'";
            //sql += " AND NOT EXISTS (SELECT *  FROM TBLMO2SAPDETAIL D  WHERE D.MOCODE = T.MOCODE   AND D.RCARD = T.RCARD)";
            //sql += " GROUP BY S.ITEMGRADE, G.SAPSTORAGE, M.MOOP, M.MOCODE";
            //sql += " UNION ALL  SELECT * FROM TBLMO2SAP where TBLMO2SAP.mocode='" + moCode.Trim().ToUpper() + "') A ORDER BY A.POSTSEQ";

            //上线中调整6.24增加了一个串联TBLInvInTransaction的条件,是为了防止一个产品经过不同工单后都可以报工的情况，后来根据实际使用，如果出现这种情况，SAP中这两张工单时都需要报工的（因为都投料了）。所以修改去除该条件

            string sql = "SELECT A.*  FROM (SELECT M.MOCODE AS MOCODE, 0 AS POSTSEQ,COUNT(S.SERIALNO) AS MOPRODUCED,";
            sql += " 0 AS MOSCRAP,'' AS MOCONFIRM,0 AS MOMANHOUR,0 AS MOMACHINEHOUR,0 AS MOCLOSEDATE,G.SAPSTORAGE AS MOLOCATION,";
            sql += " S.ITEMGRADE AS MOGRADE,M.MOOP AS MOOP, '' AS FLAG, '' AS ERRORMESSAGE, '' AS MUSER,0 AS MDATE,";
            sql += " 0 AS MTIME, '' AS EATTRIBUTE1,0 AS ORGID";
            sql += " FROM TBLSTACK2RCARD   S,  TBLSTORAGE   G, TBLSIMULATIONREPORT T,TBLMO  M ";
            sql += " WHERE S.STORAGECODE = G.STORAGECODE AND s.serialno = t.rcard ";
            sql += " AND T.ISCOM = 1  AND M.MOCODE = T.MOCODE AND M.itemcode=s.itemcode AND T.MOCODE = '" + moCode.Trim().ToUpper() + "'";
            //Added By Nettie Chen ON 2009/10/28
            sql += " AND t.eattribute1 ='GOOD' ";
            //End Added
            sql += " AND NOT EXISTS (SELECT *  FROM TBLMO2SAPDETAIL D  WHERE D.MOCODE = T.MOCODE   AND D.RCARD = T.RCARD)";
            sql += " GROUP BY S.ITEMGRADE, G.SAPSTORAGE, M.MOOP, M.MOCODE";
            sql += " UNION ALL  SELECT * FROM TBLMO2SAP where TBLMO2SAP.mocode='" + moCode.Trim().ToUpper() + "') A ORDER BY A.POSTSEQ";


            return this.DataProvider.CustomQuery(typeof(MO2SAP), new SQLCondition(sql));
        }

        public object[] QueryRcardFromStack2Rcard(string itemGrade, string sapStorage, string moCode)
        {
            //string sql = "SELECT C.RCARD AS SERIALNO  FROM TBLSTACK2RCARD A, TBLSTORAGE B, TBLSIMULATIONREPORT C, tblinvintransaction i ";
            //sql += " WHERE A.STORAGECODE = B.STORAGECODE AND a.serialno = c.rcard AND i.serial = a.transinserial AND i.mocode = c.mocode AND A.ITEMCODE=C.ITEMCODE";
            //sql += " AND C.ISCOM = 1  AND C.mocode='" + moCode.Trim() + "'  AND A.ITEMGRADE = '" + itemGrade.Trim() + "'    AND B.SAPSTORAGE = '" + sapStorage.Trim() + "' ";
            //sql += " AND NOT EXISTS  (SELECT * FROM TBLMO2SAPDETAIL D WHERE D.RCARD =C.RCARD";
            //sql += " AND D.MOCODE=C.MOCODE)";

            //changed by hiro 20091030 
            string sql = "SELECT C.RCARD AS SERIALNO  FROM TBLSTACK2RCARD A, TBLSTORAGE B, TBLSIMULATIONREPORT C ";
            sql += " WHERE A.STORAGECODE = B.STORAGECODE AND a.serialno = c.rcard   AND A.ITEMCODE=C.ITEMCODE AND C.EATTRIBUTE1 ='GOOD'";
            sql += " AND C.ISCOM = 1  AND C.mocode='" + moCode.Trim() + "'  AND A.ITEMGRADE = '" + itemGrade.Trim() + "'    AND B.SAPSTORAGE = '" + sapStorage.Trim() + "' ";
            sql += " AND NOT EXISTS  (SELECT * FROM TBLMO2SAPDETAIL D WHERE D.RCARD =C.RCARD";
            sql += " AND D.MOCODE=C.MOCODE)";
            //end

            return this.DataProvider.CustomQuery(typeof(StackToRcard), new SQLCondition(sql));
        }

        public object GetMaxPostSEQWithMOcode(string moCode)
        {
            string sql = "SELECT NVL(MAX(postseq), 0) AS POSTSEQ FROM tblmo2sapdetail WHERE mocode='" + moCode.ToUpper() + "'";
            object[] mo2sapdetail = this.DataProvider.CustomQuery(typeof(MO2SAPDetail), new SQLCondition(sql));
            return mo2sapdetail[0];
        }

        #endregion
        #region MO2SAPLog
        public void AddMO2SAPLog(MO2SAPLog log)
        {
            this.DataProvider.Insert(log);
        }

        public void UpdateMO2SAPLog(MO2SAPLog log)
        {
            this.DataProvider.Update(log);
        }

        public void DeleteMO2SAPLog(MO2SAPLog log)
        {
            this.DataProvider.Delete(log);
        }

        public object GetMO2SAPLog(string moCode, decimal postseq, int seq)
        {
            return this.DataProvider.CustomSearch(typeof(MO2SAPLog), new object[] { moCode, postseq, seq });
        }

        public object[] GetMO2SAPLogList(string moCode, decimal postSeq, int inclusive, int exclusive)
        {
            string sql = "SELECT {0} FROM tblmo2saplog WHERE active='Y' AND mocode='" + moCode + "' and postseq=" + postSeq;
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2SAPLog)));

            return this.DataProvider.CustomQuery(typeof(MO2SAPLog), new PagerCondition(sql, "seq", inclusive, exclusive));
        }

        public int GetMO2SAPLogListCount(string moCode, decimal postSeq)
        {
            string sql = "SELECT COUNT(*) FROM tblmo2saplog WHERE active='Y' AND mocode='" + moCode + "' and postseq=" + postSeq;
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public void UpdateMO2SAPLogStatus(string moCode, decimal postSeq)
        {
            string sql = "UPDATE tblmo2saplog SET active='N' WHERE mocode='" + moCode + "' AND postseq=" + postSeq;

            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public int GetMaxMO2SAPSequence(string moCode, decimal postSeq)
        {
            string sql = "SELECT NVL(MAX(seq),0) AS seq FROM tblmo2saplog WHERE mocode='" + moCode + "' AND postseq=" + postSeq;
            object mo2sapLog = this.DataProvider.CustomQuery(typeof(MO2SAPLog), new SQLCondition(sql))[0];
            return (mo2sapLog as MO2SAPLog).Sequence + 1;
        }
        #endregion

        #region MO,MOBaseData
        /// <summary>
        /// ** 方法名:
        /// ** 功能描述:
        ///     用于工单进行导入时对工单的群体的操作,
        ///     其中如果是已经存在的工单，并且工单的状态为initial的时候进行工单信息的更新
        ///     这个地方是否需要版本的更新？？？？（建议不需要),如果状态不为initial则放弃更改此工单
        ///     如果工单不存在则把对应的工单添加到数据库
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者: Crystal Chu
        /// ** 日 期: 2005-03-23
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// ** nunit
        /// </summary>
        /// <param name="mos"></param>
        public void DownLoadMOs(MO[] mos)
        {
            ItemFacade _itemFacade = new ItemFacade(this.DataProvider);

            try
            {
                this.DataProvider.BeginTransaction();
                for (int i = 0; i < mos.Length; i++)
                {
                    //检查是否存在
                    object mo = GetMO(mos[i].MOCode);

                    if (mo != null)
                    {
                        //存在检查工单是为initial

                        MO currentMO = (MO)mo;
                        object item = _itemFacade.GetItem(currentMO.ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                        if (currentMO.MOStatus == MOManufactureStatus.MOSTATUS_INITIAL)
                        {
                            //检查对应的item是否存在

                            if (item != null)
                            {
                                //sammer kong 20050411
                                UpdateMO(mos[i], false);
                                //								this.DataProvider.Update(mos[i]);
                            }
                            else
                            {
                                _itemFacade.AddItem(_itemFacade.CreateDefaultItem(currentMO));
                                UpdateMO(mos[i], true);
                            }
                        }
                        //存在检查工单不为initial，则放弃更新
                    }
                    else
                    {
                        object item = _itemFacade.GetItem(((MO)mo).ItemCode, GlobalVariables.CurrentOrganizations.First().OrganizationID);
                        //sammer kong 20050411
                        if (item != null)
                        {
                            _itemFacade.AddItem(_itemFacade.CreateDefaultItem((MO)mo));
                        }
                        AddMO(mos[i]);

                        //检查对应的item是否存在
                        //						if(item != null)
                        //						{
                        //							this.DataProvider.Insert(mos[i]);
                        //						}
                        //						else
                        //						{
                        //							_itemFacade.AddItem(_itemFacade.CreateDefaultItem((MO)mo));
                        //							AddMO(mos[i]);
                        //						}
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_log.Error(ex.Message);
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DownLoadMOs_Failure", ex);
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),ErrorCenter.ERROR_DOWNLOADMO),ex);
            }
        }


        public void AddMO(MO mo)
        {
            this._helper.AddDomainObject(mo);
        }


        public object GetMO(string moCode)
        {
            return this.DataProvider.CustomSearch(typeof(MO), new object[] { moCode });
        }


        /// <summary>
        /// ** 方法名:
        /// ** 功能描述:
        ///     工单删除，必须判断工单的状态
        ///     包括工单选择的途程和建立的流程卡的范围一并删除
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者:
        /// ** 日 期:
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// ** nunit
        /// </summary>
        /// <param name="mo"></param>
        private void DeleteMO(MO mo)
        {
            if (mo == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,"mo")));
            }
            if ((mo.MOStatus != MOManufactureStatus.MOSTATUS_INITIAL))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Delete_MOStatus", String.Format("[$MOStatus='{0}']", MOManufactureStatus.MOSTATUS_INITIAL), null);
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),String.Format(ErrorCenter.ERROR_MOSTATUS,MOSTATUS_INITIAL+" or "+MOSTATUS_PENDING)));
            }

            try
            {
                //this.DataProvider.BeginTransaction();
                //删除途程维护信息
                object[] objs = QueryMORoutes(mo.MOCode, string.Empty);
                if (objs != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        this.DataProvider.Delete(objs[i]);
                    }
                }
                this.DataProvider.Delete(mo);
                //this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_log.Error(ex.Message,ex);
                //this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DeleteMO", String.Format("[$MOCode='{0}']", mo.MOCode), ex);
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),String.Format(ErrorCenter.ERROR_DELETEMO,mo.MOCode)),ex);
            }

            //流程卡的范围一并删除
        }


        public void DeleteMO(MO[] mos)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                foreach (MO mo in mos)
                {
                    DeleteMO(mo);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DeleteMO", ex);
                //                throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),ErrorCenter.ERROR_DELETEMOS),ex);
            }


        }


        //        public void UpdateMO(MO mo)
        //        {
        //            UpdateMO(mo,true);
        //        }


        /// <summary>
        /// ** 方法名:
        /// ** 功能描述:
        ///     必须判断工单的状态，此处工单的修改并不包括对工单状态的修改，
        ///     工单状态的修改在MOStatusChanged
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者:   Crystal
        /// ** 日 期:   2005-03-23
        /// ** 修 改:  
        /// ** 日 期:
        /// ** 版本
        /// ** nunit
        /// </summary>
        /// <param name="mo"></param>
        /// <param name="checkStatus">是否需要检查状态?为true将只允许修改initial和pending状态的MO,为false时允许检查所有状态的MO</param>
        public void UpdateMO(MO mo, bool checkStatus)
        {
            if (checkStatus)
            {
                MO oriMO = (MO)(this.GetMO(mo.MOCode));
                if ((oriMO.MOStatus != MOManufactureStatus.MOSTATUS_INITIAL) && (oriMO.MOStatus != MOManufactureStatus.MOSTATUS_PENDING))
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_MOStatus", String.Format("[$MOStatus='{0}','{1}']", MOManufactureStatus.MOSTATUS_INITIAL, MOManufactureStatus.MOSTATUS_PENDING), null);
                    //                    throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),String.Format(ErrorCenter.ERROR_MOSTATUS,MOSTATUS_INITIAL+" or "+MOSTATUS_PENDING)));
                }
            }
            try
            {
                //Laws Lu,2006/11/13 uniform system collect date
                DBDateTime dbDateTime;

                dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

                mo.MaintainDate = dbDateTime.DBDate;
                mo.MaintainTime = dbDateTime.DBTime;
                this.DataProvider.Update(mo);
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_UpdateMO", String.Format("[$MOCode='{0}']", mo.MOCode), ex);
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade), String.Format(ErrorCenter.ERROR_UPDATEMO,mo.MOCode)), ex);
            }
        }
        //Laws Lu,2006/02/28,修正	工单的报废数量更新不准确
        public void UpdateMOScrapQty(MO mo)
        {
            try
            {
                int iReturn = -1;
                iReturn = (this.DataProvider as SQLDomainDataProvider).CustomExecuteWithReturn(
                    new SQLCondition("update tblmo set MOSTATUS = '" + mo.MOStatus
                    + "',MOSCRAPQTY =  MOSCRAPQTY +" + mo.MOScrapQty + " where mocode='" + mo.MOCode + "'"));

                if (iReturn <= 0)
                {
                    new Exception("$Error_UpdateMO " + String.Format("[$MOCode='{0}']", mo.MOCode));
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_UpdateMO", String.Format("[$MOCode='{0}']", mo.MOCode), ex);
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade), String.Format(ErrorCenter.ERROR_UPDATEMO,mo.MOCode)), ex);
            }
        }

        public void UpdateMOInformation(MO mo, string routeCode)
        {
            OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            this.DataProvider.BeginTransaction();
            try
            {
                this.UpdateMO(mo, true);
                MO2Route mo2Route = (MO2Route)GetMONormalRouteByMOCode(mo.MOCode);
                if (mo2Route != null)
                {
                    this.DeleteMORoute(mo2Route);
                }
                if (routeCode != string.Empty)
                {
                    Route route = (Route)baseModelFacade.GetRoute(routeCode);
                    OPBOM opBOM = opBOMFacade.GetOPBOMByRouteCode(mo.ItemCode, route.RouteCode, mo.OrganizationID, mo.BOMVersion);
                    MO2Route newMo2Route = new MO2Route();
                    newMo2Route.MOCode = mo.MOCode;
                    newMo2Route.IsMainRoute = ISMAINROUTE_TRUE;
                    newMo2Route.MaintainUser = mo.MaintainUser;
                    newMo2Route.OPBOMCode = opBOM.OPBOMCode;
                    newMo2Route.OPBOMVersion = opBOM.OPBOMVersion;
                    newMo2Route.RouteCode = route.RouteCode;
                    newMo2Route.RouteType = route.RouteType;
                    this.AddMORoute(newMo2Route);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UpdateMOInformation", ex);
            }
        }





        /// <summary>
        /// ** 方法名:  QueryMO
        /// ** 功能描述:
        ///     依据Code,Item,MOType,MOStatus及Route查询工单
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者: viz0
        /// ** 日 期:   2005-03-23
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <param name="moCode">模糊查询</param>
        /// <param name="itemCode">模糊查询</param>
        /// <param name="moType">精确查询</param>
        /// <param name="moStatus">精确查询</param>
        /// <param name="routeCode">精确查询</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns>返回MO类型的数组</returns>
        public object[] QueryMO(
            string moCode,
            string itemCode,
            string moType,
            string moStatus,
            string routeCode,
            string factory,
            int inclusive,
            int exclusive
            )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and ITEMCODE like '{0}%' ", itemCode);
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Trim().Length > 0)
            {
                condition += string.Format("and MOSTATUS = '{0}' ", moStatus);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and factory='{0}'", factory);
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }



            string sql = string.Format("select {0} from TBLMO where 1=1 {1} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), condition);
            return this.DataProvider.CustomQuery(
                typeof(MO),
                new PagerCondition(sql, inclusive, exclusive));
        }

        public object[] QueryMOIllegibility(
            string moCode,
            string itemCode,
            string moType,
            string moStatus,
            string routeCode,
            string factory,
            int inclusive,
            int exclusive
            )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and ITEMCODE like '{0}%' ", itemCode);
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Trim().Length > 0)
            {
                condition += string.Format("and MOSTATUS = '{0}' ", moStatus);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and upper(factory) like'{0}%'", factory.ToUpper());
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }



            string sql = string.Format("select {0} from TBLMO where 1=1 {1} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), condition);
            return this.DataProvider.CustomQuery(
                typeof(MO),
                new PagerCondition(sql, inclusive, exclusive));
        }

        /// <summary>
        /// added by jessie lee for AM0219, 2005/10/13, P4.10
        /// 增加下发时间区间为筛选条件
        /// modified by jessie lee, 2005/12/8
        /// </summary>
        public object[] QueryMOIllegibility(
            string moCode,
            string itemCode,
            string itemDesc,
            string moType,
            string moStatus,
            string routeCode,
            string factory,
            int ReleaseStartDate,
            int ReleaseEndDate,
            int ImportStartDate,
            int ImportEndDate,
            int inclusive,
            int exclusive
            )
        {
            return QueryMOIllegibility(moCode, itemCode, itemDesc, moType, moStatus, routeCode, factory,
                ReleaseStartDate, ReleaseEndDate, ImportStartDate, ImportEndDate, 0, 0, inclusive, exclusive);
        }

        public object[] QueryMOIllegibility(
           string moCode,
           string itemCode,
           string itemDesc,
           string moType,
           string moStatus,
           string routeCode,
           string factory,
           int ReleaseStartDate,
           int ReleaseEndDate,
           int ImportStartDate,
           int ImportEndDate,
           int actStarDate,
           int actEndDate,
           int inclusive,
           int exclusive
           )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and a.MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and a.ITEMCODE like '{0}%' ", itemCode);
            }

            if (itemDesc != null && itemDesc.Trim().Length > 0)
            {
                condition += string.Format("and upper(a.ITEMDESC) like '%{0}%' ", itemDesc.ToUpper());
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and a.MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Trim().Length > 0)
            {
                condition += string.Format("and a.MOSTATUS = '{0}' ", moStatus);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and upper(a.factory) like'{0}%'", factory.ToUpper());
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and a.MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }

            if (actStarDate > 0)
            {
                if (actEndDate == 0)
                {
                    condition += " and ( a.MOACTSTARTDATE >=" + actStarDate;
                }
                else
                {
                    condition += " and a.MOACTSTARTDATE >=" + actStarDate;
                }
            }

            if (actEndDate > 0)
            {
                if (actStarDate == 0)
                {
                    condition += " and a.MOACTSTARTDATE <=" + actEndDate + " and a.MOACTSTARTDATE>0";
                }
                else
                {
                    condition += " and a.MOACTSTARTDATE <=" + actEndDate;
                }

            }

            if (actStarDate > 0 && actEndDate == 0)
            {
                condition += " OR  a.MOACTSTARTDATE =" + 0 + ")";
            }

            string ReleaseDateCondition = string.Empty;
            string ImportDateCondition = string.Empty;
            if (ReleaseStartDate != 0)
            {
                ReleaseDateCondition = FormatHelper.GetDateRangeSql("a.MORELEASEDATE", ReleaseStartDate, ReleaseEndDate);
            }
            /* added by jessie lee, 2005/12/8 */
            if (ImportStartDate != 0)
            {
                ImportDateCondition = FormatHelper.GetDateRangeSql("a.MOIMPORTDATE", ImportStartDate, ImportEndDate);
            }

            string orgidCon = string.Empty;
            foreach (Organization org in GlobalVariables.CurrentOrganizations.GetOrganizationList())
            {
                orgidCon += org.OrganizationID + ",";
            }
            if (orgidCon.Length > 0)
            {
                orgidCon = orgidCon.Substring(0, orgidCon.Length - 1);

                orgidCon = " AND a.orgid IN (" + orgidCon + ") ";
            }

            string sql = string.Format(@"select a.*,b.itemname,b.itemdesc as ItemDescription from TBLMO a 
                LEFT JOIN TBLITEM b ON a.itemcode=b.itemcode and a.orgid = b.orgid where 1=1 {0} {1} {2} "
                + orgidCon, ReleaseDateCondition, ImportDateCondition, condition);
            return this.DataProvider.CustomQuery(
                typeof(MOWithItem),
                new PagerCondition(sql, inclusive, exclusive));
        }

        public object[] QueryMOIllegibility(
            string moCode,
            string itemCode,
            string moType,
            string[] moStatus,
            string routeCode,
            string factory,
            int inclusive,
            int exclusive
            )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and ITEMCODE like '{0}%' ", itemCode);
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Length > 0)
            {
                string in_status = "(''";
                foreach (string mo_status in moStatus)
                {
                    in_status += string.Format(",'{0}'", mo_status);
                }
                in_status += ")";
                condition += string.Format("and MOSTATUS IN {0} ", in_status);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and upper(factory) like'{0}%'", factory.ToUpper());
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }

            string sql = string.Format("select {0} from TBLMO where 1=1 {1} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), condition);
            return this.DataProvider.CustomQuery(
                typeof(MO),
                new PagerCondition(sql, "MOSTATUS,MOCODE", inclusive, exclusive));
        }





        /// <summary>
        /// ** 方法名:  QueryMO
        /// ** 功能描述:
        ///     依据Code,Item,MOType,MOStatus及Route查询工单的数量
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者: viz0
        /// ** 日 期:   2005-03-23
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <param name="moCode">模糊查询</param>
        /// <param name="itemCode">模糊查询</param>
        /// <param name="moType">精确查询</param>
        /// <param name="moStatus">精确查询</param>
        /// <param name="routeCode">精确查询</param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns>返回MO的数量</returns>
        public int QueryMOCount(
            string moCode,
            string itemCode,
            string moType,
            string moStatus,
            string routeCode,
            string factory
            )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and ITEMCODE like '{0}%' ", itemCode);
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Trim().Length > 0)
            {
                condition += string.Format("and MOSTATUS = '{0}' ", moStatus);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and factory='{0}'", factory);
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }


            string sql = string.Format("select count(mocode) from TBLMO where 1=1 {1} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), condition);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        public int QueryMOIllegibilityCount(
            string moCode,
            string itemCode,
            string moType,
            string moStatus,
            string routeCode,
            string factory
            )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and ITEMCODE like '{0}%' ", itemCode);
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Trim().Length > 0)
            {
                condition += string.Format("and MOSTATUS = '{0}' ", moStatus);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and upper(factory) like '{0}%'", factory.ToUpper());
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }


            string sql = string.Format("select count(mocode) from TBLMO where 1=1 {1} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), condition);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="moType"></param>
        /// <param name="moStatus"></param>
        /// <param name="routeCode"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public int QueryMOIllegibilityCount(
            string moCode,
            string itemCode,
            string itemDesc,
            string moType,
            string moStatus,
            string routeCode,
            string factory,
            int ReleaseStartDate,
            int ReleaseEndDate,
            int ImportStartDate,
            int ImportEndDate
            )
        {
            return QueryMOIllegibilityCount(moCode, itemCode, itemDesc, moType, moStatus, routeCode, factory,
                                            ReleaseStartDate, ReleaseEndDate, ImportStartDate, ImportEndDate, 0, 0);
        }


        public int QueryMOIllegibilityCount(
        string moCode,
        string itemCode,
        string itemDesc,
        string moType,
        string moStatus,
        string routeCode,
        string factory,
        int ReleaseStartDate,
        int ReleaseEndDate,
        int ImportStartDate,
        int ImportEndDate,
        int actStarDate,
        int actEndDate
        )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and ITEMCODE like '{0}%' ", itemCode);
            }

            if (itemDesc != null && itemDesc.Trim().Length > 0)
            {
                condition += string.Format("and upper(ITEMDESC) like '%{0}%' ", itemDesc.ToUpper());
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Trim().Length > 0)
            {
                condition += string.Format("and MOSTATUS = '{0}' ", moStatus);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and upper(factory) like '{0}%'", factory.ToUpper());
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }

            if (actStarDate > 0)
            {
                if (actEndDate == 0)
                {
                    condition += " and ( MOACTSTARTDATE >=" + actStarDate;
                }
                else
                {
                    condition += " and MOACTSTARTDATE >=" + actStarDate;
                }
            }

            if (actEndDate > 0)
            {
                if (actStarDate == 0)
                {
                    condition += " and MOACTSTARTDATE <=" + actEndDate + " and MOACTSTARTDATE>0";
                }
                else
                {
                    condition += " and MOACTSTARTDATE <=" + actEndDate;
                }

            }

            if (actStarDate > 0 && actEndDate == 0)
            {
                condition += " or  MOACTSTARTDATE =" + 0 + ")";
            }

            string ReleaseDateCondition = string.Empty;
            string ImportDateCondition = string.Empty;
            if (ReleaseStartDate != 0)
            {
                ReleaseDateCondition = FormatHelper.GetDateRangeSql("MORELEASEDATE", ReleaseStartDate, ReleaseEndDate);
            }
            /* added by jessie lee, 2005/12/8 */
            if (ImportStartDate != 0)
            {
                ImportDateCondition = FormatHelper.GetDateRangeSql("MOIMPORTDATE", ImportStartDate, ImportEndDate);
            }

            string sql = string.Format("select count(mocode) from TBLMO where 1=1 {0} {1} {2} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), ReleaseDateCondition, ImportDateCondition, condition);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int QueryMOIllegibilityCount(
            string moCode,
            string itemCode,
            string moType,
            string[] moStatus,
            string routeCode,
            string factory
            )
        {
            string condition = "";

            if (moCode != null && moCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE like '{0}%' ", moCode);
            }

            if (itemCode != null && itemCode.Trim().Length > 0)
            {
                condition += string.Format("and ITEMCODE like '{0}%' ", itemCode);
            }

            if (moType != null && moType.Trim().Length > 0)
            {
                condition += string.Format("and MOTYPE = '{0}' ", moType);
            }

            if (moStatus != null && moStatus.Length > 0)
            {
                string in_status = "(''";
                foreach (string mo_status in moStatus)
                {
                    in_status += string.Format(",'{0}'", mo_status);
                }
                in_status += ")";
                condition += string.Format("and MOSTATUS IN {0} ", in_status);
            }

            if ((factory != null) && (factory.Trim() != string.Empty))
            {
                condition += string.Format(" and upper(factory) like '{0}%'", factory.ToUpper());
            }

            if (routeCode != null && routeCode.Trim().Length > 0)
            {
                condition += string.Format("and MOCODE in ( select MOCODE from TBLMO2ROUTE where ROUTECODE = '{0}' ) ", routeCode);
            }


            string sql = string.Format("select count(mocode) from TBLMO where 1=1 {1} " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), condition);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// ** 方法名: IsMOStatusChanged
        /// ** 功能描述: 判断工单状态是否改变过
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者: vizo
        /// ** 日 期: 2005-03-23
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <param name="mo"></param>
        /// <returns>true 已改变 , false 未改变</returns>
        public bool IsMOStatusChanged(MO mo)
        {
            MO oriMO = (MO)GetMO(mo.MOCode);
            return oriMO.MOStatus != mo.MOStatus;
        }


        /// <summary>
        /// ** 方法名: MOCheck
        /// ** 功能描述: 送检批工序, 批量check
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者: roger.xue
        /// ** 日 期: 2008-08-21
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <param name="mo"></param>
        /// <returns>true 已改变 , false 未改变</returns>
        public void MOCheck(MO mo)
        {
            ItemFacade itemFacade = new ItemFacade(this.DataProvider);
            BaseModelFacade bmf = new BaseModelFacade(this.DataProvider);
            object objItem = itemFacade.GetItem(mo.ItemCode, mo.OrganizationID);

            Item item = objItem as Item;

            if (item.CheckItemOP == null || item.CheckItemOP.Trim() == String.Empty)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_NoItemGenerateLotOPCode");
            }
            else
            {
                object[] moroutes = this.GetAllMORoutes(mo.MOCode);
                Route route = moroutes[0] as Route;

                if (bmf.GetRoute2Operation(route.RouteCode, item.CheckItemOP) == null)
                {
                    ExceptionManager.Raise(this.GetType(), "$CS_CheckOPNotInMOOPList $CS_CheckOPCode=" + item.CheckItemOP + " $Domain_MO2Route=" + route.RouteCode);
                }
            }

            if (item.LotSize == null || (int)item.LotSize <= 0)
            {
                ExceptionManager.Raise(this.GetType(), "$CS_PleaseMaintainLotSize");
            }
        }


        /// <summary>
        /// ** 方法名: MOStatusChanged
        /// ** 功能描述:
        ///     只对工单的状态的修改
        ///     initial->release 
        ///     release->initial
        ///     open->pending 这是一个下线的动作
        ///     pending->open下线再到上线的动作，其中包括工单版本的升级,
        ///     同时必须检查工单途程（所有途程）的BOM的版本是否有变化（升级）
        ///     如果有必须替换获得最新的版本
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者: crystal
        /// ** 日 期:
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// ** nunit
        /// </summary>
        /// <param name="mo"></param>
        public void MOStatusChanged(BenQGuru.eMES.Domain.MOModel.MO mo)
        {
            if (mo == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }
            MO origianlMO = (MO)GetMO(mo.MOCode);
            if (origianlMO.MOStatus == mo.MOStatus)
            {
                return;
            }

            #region Marked Code ,Backup
            if ((origianlMO.MOStatus == MOManufactureStatus.MOSTATUS_INITIAL) && (mo.MOStatus == MOManufactureStatus.MOSTATUS_RELEASE))
            {
                //add by crystal  2005/05/09 正常工单与重工工单的release
                SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
                Parameter paramter = (Parameter)systemSettingFacade.GetParameter(origianlMO.MOType, BenQGuru.eMES.Web.Helper.MOType.GroupType);
                object[] objs = null;
                object objRMA = null;

                if (mo.MOType == paramter.ParameterCode && mo.MOType == "RMA")
                {
                    if (mo.RMABillCode.Trim() == String.Empty)
                    {
                        ExceptionManager.Raise(this.GetType(), "$MO_RMACODE_MUST");
                    }

                    objRMA = this.GetRMARCARDByMoCode(mo.MOCode);

                    if (objRMA == null)
                    {
                        ExceptionManager.Raise(this.GetType(), "$MO_RMACODE_MUST");
                    }
                }

                if (paramter.ParameterValue == BenQGuru.eMES.Web.Helper.MOType.MOTYPE_REWORKMOTYPE && mo.MOType != paramter.ParameterCode)
                {
                    //					ReworkFacade reworkFacade = new ReworkFacade(this.DataProvider);
                    objs = this.QueryReworkSheet(string.Empty, string.Empty, mo.ItemCode, mo.MOCode, ReworkStatus.REWORKSTATUS_OPEN, "", "", int.MinValue, int.MaxValue);
                    if (objs == null)
                    {
                        ExceptionManager.Raise(this.GetType(), "$Error_ReworkSheet_NotRelease", String.Format("[$MOCode='{0}']", mo.MOCode));
                    }
                }
                else
                {

                }
                //检查途程的维护
                objs = QueryMORoutes(mo.MOCode, string.Empty, ISMAINROUTE_TRUE);
                if (objs == null)
                {
                    //add by roger.xue 2008/08/19 for hisense
                    /*-------工单下发的时候,如果工单的途程没有维护,则把产品的默认途程update到工单上 ------*/
                    object objDefaultRoute = this.GetDefaultItem2Route(mo.ItemCode);

                    if (objDefaultRoute != null)
                    {
                        DefaultItem2Route defaultItem2Route = objDefaultRoute as DefaultItem2Route;
                        string routecode = defaultItem2Route.RouteCode;

                        MO2Route mo2Route = new MO2Route();

                        mo2Route.MOCode = mo.MOCode;
                        mo2Route.RouteCode = routecode;
                        mo2Route.RouteType = RouteType.Normal;
                        mo2Route.OPBOMCode = routecode; //
                        mo2Route.OPBOMVersion = mo.BOMVersion;
                        mo2Route.IsMainRoute = "1";
                        mo2Route.EAttribute1 = "";
                        mo2Route.MaintainUser = mo.MaintainUser;

                        this.AddMORoute(mo2Route);
                    }
                    else
                    {
                        ExceptionManager.Raise(this.GetType(), "$Error_MORoute_NotExist", String.Format("[$MOCode='{0}']", mo.MOCode));
                    }
                }
                //检查上料工序是否有维护BOM
                if (!this.CheckMORouteItemLoadingOPBOM(mo))
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_OPBOMNotExist1", String.Format("[$MOCode='{0}']", mo.MOCode));
                }

                //工单下发时候,检查该产品对应的”产生送检批工序” 和”批量”是否有维护
                //MOCheck(mo);

                //以下比对取消 6月15日 Custom Feedback中CS0018要求  modify by Simone
                //检查bom比对
                //				if(origianlMO.IsBOMPass ==IsPass.ISPASS_NOPASS.ToString())
                //				{
                //					ExceptionManager.Raise(this.GetType(),"$Error_MOBOM_FailureCompare",String.Format("[$MOCode='{0}']",mo.MOCode));
                //				}

                //add by crystal  2005/04 /20 chu 版本的升级
                Decimal moVersion = 0;
                try
                {
                    moVersion = System.Decimal.Parse(origianlMO.MOVersion) + 1;
                }
                catch
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_ConvertMOVersion_Failure");
                }
                mo.MOVersion = moVersion.ToString();
                mo.MOSeq = GetNextMOSeq();
                UpdateMO(mo, false);
                //Laws Lu,2006/07/07 add ,Success release than delete rma
                if (objRMA != null)
                {
                    this.DeleteRMARCARD(objRMA as RMARCARD);
                }

                this.SyncOPBomDetailToWHItem(mo.MOCode);//工单下发或者取消暂停的时候,同步库房的物料主档.
                return;
            }
            else if ((origianlMO.MOStatus == MOManufactureStatus.MOSTATUS_RELEASE) && (mo.MOStatus == MOManufactureStatus.MOSTATUS_INITIAL))
            {
                UpdateMO(mo, false);

                //add by roger.xue 2008/08/19 for hisense
                /*------- 当取消下发的时候把工单的途程删掉 ------*/
                object[] mo2Routes = QueryMORoutes(mo.MOCode, string.Empty, ISMAINROUTE_TRUE);
                if (mo2Routes != null && mo2Routes.Length > 0)
                {
                    MO2Route mo2Route = (MO2Route)mo2Routes[0];

                    this.DeleteMORoute(mo2Route);
                }
                //end
                return;
            }
            else if ((origianlMO.MOStatus == MOManufactureStatus.MOSTATUS_PENDING) && (mo.MOStatus == MOManufactureStatus.MOSTATUS_OPEN))
            {
                //add by crystal  2005/04 /20 chu 版本的升级
                Decimal moVersion = 0;
                try
                {
                    moVersion = System.Decimal.Parse(origianlMO.MOVersion) + 1;
                }
                catch
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_ConvertMOVersion_Failure");
                }
                //检查上料工序是否有维护BOM
                if (!this.CheckMORouteItemLoadingOPBOM(mo))
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_OPBOMNotExist1", String.Format("[$MOCode='{0}']", mo.MOCode));
                }

                //取消暂停时候,检查该产品对应的”产生送检批工序” 和”批量”是否有维护
                //MOCheck(mo);

                mo.MOVersion = moVersion.ToString();
                UpdateMO(mo, false);
                this.SyncOPBomDetailToWHItem(mo.MOCode);//工单下发或者取消暂停的时候,同步库房的物料主档.
                return;
            }
            else if ((origianlMO.MOStatus == MOManufactureStatus.MOSTATUS_OPEN) && (mo.MOStatus == MOManufactureStatus.MOSTATUS_PENDING))
            {
                UpdateMO(mo, false);
                return;
            }
            else if (origianlMO.MOStatus == MOManufactureStatus.MOSTATUS_OPEN && mo.MOStatus == MOManufactureStatus.MOSTATUS_CLOSE)
            {
                //关单的逻辑: 在工单没有“在制品”和“在修品”时，允许进行工单关单。(该逻辑不再使用)
                //modified by jessie lee for AM0245, 2005/10/19, P4.12
                //在已确认无在制品后，如果投入数－实际完工数－拆解数＝0，此时是可以关单的。如果投入数－实际完工数－拆解数>0,提示工单尚有在修品
                if (MOCloseCheck(mo))
                {
                    UpdateMO(mo, false);  // 改变工单状态为关单
                    ClearSimulation(mo); // 清除Simulation相关表
                    Deletetblpackingchk(); // 清除tblpackingchk
                }
                return;
            }
            else if (origianlMO.MOStatus == MOManufactureStatus.MOSTATUS_INITIAL && mo.MOStatus == MOManufactureStatus.MOSTATUS_CLOSE)
            {
                /* added by jessie lee, 2005/12/8
                 * CS187 : 对初始化的工单进行关单：初始化的工单使用“关单”按钮进行关单，关单后的状态使用与正常关单相同的生产状态－－已关单。 */
                UpdateMO(mo, false);  // 改变工单状态为关单
                return;
            }
            #endregion

            //sammer kong 20050411 exception message too simple
            ExceptionManager.Raise(this.GetType(), "$Error_MOStatus_Changed", String.Format("[ $MOCode='{0}', $MOStatus ${1}->${2} ]", mo.MOCode, origianlMO.MOStatus, mo.MOStatus), null);
        }
        private decimal GetNextMOSeq()
        {
            string strSql = "SELECT MAX(MOSEQ) MOSEQ FROM TBLMO";
            object[] objs = this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(strSql));
            decimal dSeq = 1;
            if (objs != null && objs.Length > 0)
                dSeq = ((MO)objs[0]).MOSeq + 1;
            return dSeq;
        }

        #region 关闭工单检查 私有方法

        /// <summary>
        /// 关单check
        /// 关单的逻辑: 在工单没有“在制品”和“在修品”时，允许进行工单关单。(该逻辑不再使用)
        /// modified by jessie lee for AM0245, 2005/10/19, P4.12
        /// 在已确认无在制品后，如果投入数－实际完工数－拆解数＝0，此时是可以关单的。如果投入数－实际完工数－拆解数>0,提示工单尚有在修品
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        private bool MOCloseCheck(MO mo)
        {
            bool returnBool = false;

            #region 检查是否有在制品
            if (CheckMOSimulation(mo))
            {
                returnBool = true;
            }
            else
            {
                returnBool = false;
                ExceptionManager.Raise(this.GetType(), "$Error_MOCloseFailure_Simulation");
            }
            #endregion

            #region 检查是否有在修品
            if (CheckMOTS(mo))
            {
                returnBool = true;
            }
            else
            {
                returnBool = false;
                ExceptionManager.Raise(this.GetType(), "$Error_MOCloseFailure_TS");
            }
            #endregion

            return returnBool;
        }
        //检查是否有在制品
        private bool CheckMOSimulation(MO mo)
        {
            bool returnBool = false;
            string sql = string.Format("select COUNT(RCARD) from TBLSimulation where mocode = '{0}' AND ISCOM ='0'", mo.MOCode);

            if (this.DataProvider.GetCount(new SQLCondition(sql)) > 0)
            {
                returnBool = false;
            }
            else
            {
                returnBool = true;
            }
            return returnBool;
        }

        /// <summary>
        /// 检查是否有在修品
        /// 如果投入数－实际完工数－拆解数-脱离工单数量＝0，此时是可以关单的。如果投入数－实际完工数－拆解数>0,提示工单尚有在修品
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        private bool CheckMOTS(MO mo)
        {
            //关单送修品的检查不包含离线维修的产品,只检查在线维修
            //TSSource.TSSource_OnWIP //在线维修	
            //TSSource.TSSource_TS	  //离线维修 
            /*
            bool returnBool = false;
			
            string sql = string.Format(@"select COUNT(RCARD) from TBLTS where mocode = '{0}' AND FRMINPUTTYPE = '{1}' 
                            AND  TSSTATUS  in ('"+TSStatus.TSStatus_New+"','"+TSStatus.TSStatus_Confirm+"','"+TSStatus.TSStatus_TS+"')",mo.MOCode,TSSource.TSSource_OnWIP);
			
            if(this.DataProvider.GetCount( new SQLCondition(sql) ) > 0)
            {
                returnBool = false;
            }
            else
            {
                returnBool = true;
            }
            return returnBool;
            */

            decimal tsCount = mo.MOInputQty - mo.MOActualQty - mo.MOScrapQty - mo.MOOffQty;
            if (tsCount == 0)
            {
                //if (mo.MOInputQty >= mo.MOPlanQty)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                return true;
            }
            else return false;
        }

        #endregion

        #region 检查工单途程是否有上料工序,以及是否维护上料工序BOM

        private bool CheckMORouteItemLoadingOPBOM(BenQGuru.eMES.Domain.MOModel.MO mo)
        {
            MO2Route mo2Route = (MO2Route)GetMONormalRouteByMOCode(mo.MOCode);
            //Route route = (Route) baseModelFacade.GetRoute(routeCode);

            string sql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OPBOM)) + " from tblopbom where 1=1 and opbomver='" + mo.BOMVersion + "' AND itemcode ='" + mo.ItemCode.Trim() + "' and obroute ='" + mo2Route.RouteCode.Trim() + "' and orgid=" + mo.OrganizationID;

            OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
            //工单途程是否有上料工序
            bool IsComponetLoading = opBOMFacade.CheckItemRouteIsContainComponetLoading(mo.ItemCode, mo2Route.RouteCode, mo.OrganizationID);
            object[] objsOp = opBOMFacade.GetItemRouteIsContainComponetLoading(mo.ItemCode, mo2Route.RouteCode, mo.OrganizationID);
            object[] objs = this.DataProvider.CustomQuery(typeof(OPBOM), new SQLCondition(String.Format(sql)));


            //检查工单途程是否有上料工序,如果有则OPBOM不允许为空并抛出异常,否则则新增一个OPBOM插入数据库,并返回这个OPBOM,不再抛出异常
            //	Modify By Simone Xu 2005/08/15
            if (IsComponetLoading)
            {
                if (objs == null)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_OPBOMNotExist", string.Format("[$ItemCode='{0}',$RouteCode='{1}',$BOMVersion='{2}']", mo.ItemCode, mo2Route.RouteCode, mo.BOMVersion), null);
                    return false;
                }
                else
                {
                    foreach (object opboms in objsOp)
                    {
                        string selectSql = "select count(*) from tblopbomdetail where 1= 1 {0} and opbomver='" + mo.BOMVersion + "' and orgid=" + mo.OrganizationID;
                        string tmpString2 = " and itemcode ='" + mo.ItemCode.Trim() + "'";
                        tmpString2 += " and opcode='" + ((ItemRoute2OP)opboms).OPCode.Trim() + "' ";
                        string opcodesSelect = string.Format(@"AND obcode in (''");
                        foreach (object opbom in objs)
                        {
                            opcodesSelect += string.Format(",'{0}'", ((OPBOM)opbom).OPBOMCode);
                        }
                        opcodesSelect += ")";
                        tmpString2 += opcodesSelect;
                        if (!(this.DataProvider.GetCount(new SQLCondition(String.Format(selectSql, tmpString2))) > 0))
                        {
                            ExceptionManager.Raise(this.GetType(), "$Error_OPBOMNotExist1", string.Format("[$ItemCode='{0}',$RouteCode='{1}',$OpCode='{2}',$BOMVersion='{3}']", mo.ItemCode, mo2Route.RouteCode, ((ItemRoute2OP)opboms).OPCode, mo.BOMVersion), null);
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        #endregion

        #region 工单下发或者取消暂停的时候,同步库房的物料主档.

        private void SyncOPBomDetailToWHItem(string mocode)
        {
            //获取工单对应的工序bom物料清单
            string opbomdetailSql = string.Format(@"SELECT tblopbomdetail.*
															FROM tblopbomdetail
															WHERE (obcode, opcode) IN (SELECT routecode, opcode
																						FROM tblroute2op
																						WHERE routecode IN (SELECT routecode
																											FROM tblmo2route
																											WHERE mocode = '{0}'))" + GlobalVariables.CurrentOrganizations.GetSQLCondition(), mocode);
            object[] opbomDetailObjs = this.DataProvider.CustomQuery(typeof(OPBOMDetail), new SQLCondition(opbomdetailSql));

            //判断库房物料主档中是否有此料品
            //同步工序bom和库房物料主档的物料清单.
            WarehouseFacade wareFacade = new WarehouseFacade(this.DataProvider);
            wareFacade.AddWarehouseItem(opbomDetailObjs);
        }

        #endregion
        
				/// <summary>
        /// 根据工单号和产品代码，查询已完工的产品序列号
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string[] GetRcardsByMoAndItem(string moCode,string itemCode)
        {
            string sql = string.Format("SELECT rcard from tblsimulation WHERE  mocode = '{0}' AND itemcode='{1}' AND iscom=1", moCode, itemCode);
            return this.DataProvider.GetStringResult(new SQLCondition(sql));
        }

        /// <summary>
        /// 根据OQC批号和产品代码，查询已完工的产品序列号
        /// </summary>
        /// <param name="lotNo"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string[] GetRcardsByOqcAndItem(string lotNo, string itemCode)
        {
            string sql = string.Format("SELECT rcard from tblsimulation WHERE  lotno = '{0}' AND itemcode='{1}' AND iscom=1", lotNo, itemCode);
            return this.DataProvider.GetStringResult(new SQLCondition(sql));
        }


        public object[] QueryReworkSheet(string reworkSheetCode, string modelCode, string itemCode, string moCode, string reworkStatus, string lotno, string runningCard, int inclusive, int exclusive)
        {
            string moCondition;
            if (moCode == string.Empty)
            {
                moCondition = " 1=1 ";
            }
            else
            {
                moCondition = " mocode like '" + moCode + "%' ";
            }

            string itemCondition;
            if (itemCode == string.Empty)
            {
                itemCondition = " 1=1 ";
            }
            else
            {
                itemCondition = " itemcode like '" + itemCode + "%' ";
            }

            string modelCondition;
            if (modelCode == string.Empty)
            {
                modelCondition = "1=1";
            }
            else
            {
                modelCondition = " itemcode in ( select itemcode from TBLMODEL2ITEM where modelcode ='" + modelCode + "' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") ";
            }

            string statusCondition;
            if (reworkStatus == string.Empty)
            {
                statusCondition = "1=1";
            }
            else
            {
                statusCondition = "status = '" + reworkStatus + "' ";
            }

            string rejectCondition;
            if (lotno == string.Empty && runningCard == string.Empty)
            {
                rejectCondition = " 1=1 ";
            }
            else
            {
                rejectCondition = string.Format("reworkcode in(select tblreworkrange.reworkcode from tblreworkrange,tblreject where tblreject.rcard = tblreworkrange.rcard and tblreject.rcardseq =tblreworkrange.rcardseq and tblreject.lotno like '{0}%' and tblreject.rcard like '{1}%' )", lotno, runningCard);
            }


            string sql1 = "select {0} from tblreworksheet where reworkcode like '{1}%' and reworktype = '{2}' and {3} and {4} and {5} and {6} and {7}";

            sql1 = string.Format(
                sql1,
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReworkSheet)),
                reworkSheetCode,
                BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_REMO,
                itemCondition,
                moCondition,
                statusCondition,
                rejectCondition,
                modelCondition
                );






            string sql2 = "select {0} from tblreworksheet where reworkcode like '{1}%' and reworktype = '{2}' and {3} and {4} and {5} and {6} and {7}";

            sql2 = string.Format(
                sql2,
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReworkSheet)),
                reworkSheetCode,
                BenQGuru.eMES.Web.Helper.ReworkType.REWORKTYPE_ONLINE,
                itemCondition,
                moCondition,
                statusCondition,
                rejectCondition,
                modelCondition
                );




            string sql = string.Format("select {0} from ({1} union all {2}) ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ReworkSheet)), sql1, sql2);
            return this.DataProvider.CustomQuery(typeof(ReworkSheet), new PagerCondition(sql, "REWORKCODE", inclusive, exclusive));

        }

        public void MOStatusChanged(BenQGuru.eMES.Domain.MOModel.MO[] mos)
        {
            this._domainDataProvider.BeginTransaction();
            try
            {
                foreach (MO mo in mos)
                {
                    MOStatusChanged(mo);
                }
                this._domainDataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this._domainDataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_MOStatussChanged", ex);
            }
        }


        /// <summary>
        /// 升级工单的版本
        /// </summary>
        /// <param name="mo"></param>
        private void MOUpgradeVersion(MO mo)
        {
        }


        /// <summary>
        /// 判断工单所选择BOM版本是否为最新
        /// 根据工单中Item+Route找到OPBOM最新版本OPBOMDescription
        /// 然后通过比较工单OPBOMVersion 如果不一样则为返回true
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        private bool IsBOMVersionChange(MO mo)
        {
            return true;
        }




        /// <summary>
        /// 判断工单是否为线上，还是已经下线
        /// </summary>
        /// <param name="mo"></param>
        /// <returns></returns>
        public bool IsOnline(BenQGuru.eMES.Domain.MOModel.MO mo)
        {
            return true;
        }



        #region MORoute
        public MO2Route CreateNewMO2Route()
        {
            return new MO2Route();
        }
        public void AddMORoute(MO2Route mo2Route)
        {
            this._helper.AddDomainObject(mo2Route);
        }

        /// <summary>
        /// 只能修改对应的BOM
        /// </summary>
        /// <param name="mo2Route"></param>
        public void UpdateMORoute(MO2Route mo2Route)
        {
            MO oriMO = (MO)(this.GetMO(mo2Route.MOCode));
            if ((oriMO.MOStatus != MOManufactureStatus.MOSTATUS_INITIAL) && (oriMO.MOStatus != MOManufactureStatus.MOSTATUS_PENDING))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_MOStatus", String.Format("[$MOStatus='{0}','{1}']", MOManufactureStatus.MOSTATUS_INITIAL, MOManufactureStatus.MOSTATUS_PENDING), null);
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),String.Format(ErrorCenter.ERROR_MOSTATUS,MOSTATUS_INITIAL+" or "+MOSTATUS_PENDING)));
            }
            this._helper.UpdateDomainObject(mo2Route);
        }

        /// <summary>
        /// 必须工单状态在initial才能上料工单途程和对应的上料清单的信息
        /// </summary>
        /// <param name="mo2Route"></param>
        public void DeleteMORoute(MO2Route mo2Route)
        {
            MO oriMO = (MO)(this.GetMO(mo2Route.MOCode));
            if ((oriMO.MOStatus != MOManufactureStatus.MOSTATUS_INITIAL) && (oriMO.MOStatus != MOManufactureStatus.MOSTATUS_PENDING))
            {
                ExceptionManager.Raise(this.GetType(), "$Error_MOStatus", String.Format("[$MOStatus='{0}','{1}']", MOManufactureStatus.MOSTATUS_INITIAL, MOManufactureStatus.MOSTATUS_PENDING), null);
                //				throw new RemotingException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),String.Format(ErrorCenter.ERROR_MOSTATUS,MOSTATUS_INITIAL+" or "+MOSTATUS_PENDING)));
            }
            this._helper.DeleteDomainObject(mo2Route);
        }



        public object GetMONormalRouteByMOCode(string moCode)
        {
            object[] objs = QueryMORoutes(moCode, string.Empty, ISMAINROUTE_TRUE);
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }

        public object[] QueryMORoutes(string moCode, string routeCode)
        {
            return QueryMORoutes(moCode, routeCode, string.Empty, string.Empty);
        }

        public object[] QueryMORoutes(string moCode, string routeCode, string isMainRoute)
        {
            return QueryMORoutes(moCode, routeCode, isMainRoute, string.Empty);
        }

        public object[] QueryMORoutes(string moCode, string routeCode, string isMainRoute, string opBOMVersion)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2Route)) + " from tblmo2route where {0}";

            string tmpString = "1=1";
            if ((moCode != string.Empty) && (moCode.Trim() != string.Empty))
            {
                tmpString += " and mocode = '" + moCode.Trim() + "'";
            }
            if ((routeCode != string.Empty) && (routeCode.Trim() != string.Empty))
            {
                tmpString += " and routecode ='" + routeCode.Trim() + "'";
            }
            if ((isMainRoute != string.Empty) && (isMainRoute.Trim() != string.Empty))
            {
                tmpString += " and ismroute ='" + isMainRoute.Trim() + "'";
            }
            if ((opBOMVersion != string.Empty) && (opBOMVersion.Trim() != string.Empty))
            {
                tmpString += " and opbomver ='" + opBOMVersion.Trim() + "'";
            }
            return this.DataProvider.CustomQuery(typeof(MO2Route), new SQLCondition(String.Format(selectSql, tmpString)));
        }


        /// <summary>
        /// 判断该途程有没有在工单中使用，如果有被使用则返回true
        /// </summary>
        /// <param name="routeCode">不能为空</param>
        /// <returns>返回布尔型</returns>
        public bool IsModelRouteUsed(string routeCode)
        {
            if ((routeCode == string.Empty) || (routeCode.Trim() == string.Empty))
            {
                //sammer kong
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "routeCode", null);
                //				throw new ArgumentException(ErrorCenter.GetErrorServerDescription(typeof(MOFacade),String.Format(ErrorCenter.ERROR_ARGUMENT_NULL,routeCode)));
            }
            if (this.DataProvider.GetCount(new SQLCondition(String.Format("select count(*) from tblmo2route where routecode ='{0}'", "routeCode"))) > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 判断该opbom和对应的版本有没有被工单使用，如果有被使用则返回true
        /// </summary>
        /// <param name="opBOMCode"></param>
        /// <param name="opBOMVersion"></param>
        /// <returns>返回布尔型</returns>
        //		public bool IsOPBOMUsed(string opBOMCode,string opBOMVersion)
        //		{
        //			//update by crystal chu 2005/04/20 工单状态必须是open
        //			string selectSql = "select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2Route))+" from tblmo2route where 1=1 and mocode in (select mo from tblmo where mostatus ='"+MOStatus.MOSTATUS_OPEN+"' {0}";
        //			string tmpString = string.Empty;
        //			if((opBOMCode != string.Empty)&&(opBOMCode.Trim() != string.Empty))
        //			{
        //				tmpString += " and opbomcode ='"+opBOMCode.Trim()+"'";
        //			}
        //			if((opBOMVersion != string.Empty)&&(opBOMVersion.Trim() != string.Empty))
        //			{
        //				tmpString += " and opbomver ='"+opBOMVersion.Trim()+"'";
        //			}
        //			if(this.DataProvider.CustomQuery(typeof(MOFacade),new SQLCondition(String.Format(selectSql,tmpString)))!=null)
        //			{
        //				return true;
        //			}
        //			return false;
        //		}
        #endregion

        public object[] GetOPBOMUsedMOs(string itemCode, string opBOMCode, string opBOMVersion, int orgID)
        {
            //string selectSql = "select "+DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO))+" from tblmo where  mostatus ='"+MOManufactureStatus.MOSTATUS_OPEN+"' and mocode in (select mocode from tblmo2route where 1=1 {0} ) and itemcode ='{1}'";
            //只有mostatus_initial和mostatus_pending 的工单对应的OPBOM才可以修改 InternalFeedBack 6月15 INT0018   modify by Simone
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO))
                + " from tblmo where  mostatus !='" + MOManufactureStatus.MOSTATUS_INITIAL
                + "' and mostatus !='" + MOManufactureStatus.MOSTATUS_PENDING
                + "' and mostatus !='" + MOManufactureStatus.MOSTATUS_CLOSE
                + "' and mocode in (select mocode from tblmo2route where 1=1 {0} ) and itemcode ='{1}'"
                + "  and orgid=" + orgID;
            string tmpString = string.Empty;
            if ((opBOMCode != string.Empty) && (opBOMCode.Trim() != string.Empty))
            {
                tmpString += " and opbomcode ='" + opBOMCode.Trim() + "'";
            }
            if ((opBOMVersion != string.Empty) && (opBOMVersion.Trim() != string.Empty))
            {
                tmpString += " and opbomver ='" + opBOMVersion.Trim() + "'";
            }
            return this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(String.Format(selectSql, tmpString, itemCode)));
        }



        /// <summary>
        /// 返回一个工单对应的TS ErrorCode途程
        /// </summary>
        /// <param name="moCode"></param>
        /// <param name="moCode"></param>
        /// <returns>Route[]</returns>
        public object[] QueryTSErrorCodeRouteByMO(string moCode, string routeCode)
        {
            string sql = "SELECT {0} FROM tblroute where routecode in (select routecode from TBLMODEL2ECG WHERE modelcode IN (SELECT modelcode FROM tblitem WHERE 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode IN (SELECT itemcode FROM tblmo WHERE mocode = '{1}'))) and routecode like '{2}%'";
            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                moCode,
                routeCode

                );
            return this.DataProvider.CustomQuery(
                typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                new SQLCondition(sql)
                );
        }

        public int QueryTSErrorCodeRouteCountByMO(string moCode, string routeCode)
        {
            string sql = "SELECT count(routecode) FROM tblroute where routecode in (select routecode from TBLMODEL2ECG WHERE modelcode IN (SELECT modelcode FROM tblitem WHERE 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode IN (SELECT itemcode FROM tblmo WHERE mocode = '{1}'))) and routecode like '{2}%'";
            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                moCode,
                routeCode
                );
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }





        /// <summary>
        /// 返回一个工单对应的TS ErrorCause途程
        /// </summary>
        /// <param name="moCode"></param>
        /// <returns>返回 Route[]类型</returns>
        public object[] QueryTSErrorCauseRouteByMO(string moCode, string routeCode)
        {
            string sql = "SELECT {0} FROM tblroute where routecode in (select routecode from TBLMODEL2ECSG WHERE modelcode IN (SELECT modelcode FROM tblitem WHERE 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode IN (SELECT itemcode FROM tblmo WHERE mocode = '{1}'))) and routecode like '{2}%'";
            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                moCode,
                routeCode
                );
            return this.DataProvider.CustomQuery(
                typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                new SQLCondition(sql)
                );
        }

        public int QueryTSErrorCauseRouteCountByMO(string moCode, string routeCode)
        {
            string sql = "SELECT count(routecode) FROM tblroute where routecode in (select routecode from TBLMODEL2ECSG WHERE modelcode IN (SELECT modelcode FROM tblitem WHERE 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and itemcode IN (SELECT itemcode FROM tblmo WHERE mocode = '{1}'))) and routecode like '{2}%'";
            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                moCode,
                routeCode
                );
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        /// <summary>
        /// ** 方法名:QueryNormalRouteByMO
        /// ** 功能描述:返回工单对应的normal途程
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者:   vizo
        /// ** 日 期:   2005-03-23
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本

        /// 
        /// </summary>
        /// <param name="moCode">工单Code</param>
        /// <param name="routeCode">途程Code</param>
        /// <returns>返回Route[]类型</returns>
        public object[] QueryNormalRouteByMO(string moCode, string routeCode)
        {
            string sql = "SELECT {0} FROM tblroute where routecode in (SELECT routecode FROM tblitem2route WHERE itemcode IN (SELECT itemcode FROM tblmo WHERE mocode = '{1}') " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and routecode like '{2}%'";
            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                moCode,
                routeCode
                );
            return this.DataProvider.CustomQuery(
                typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                new SQLCondition(sql)
                );
        }

        /// <summary>
        /// added by jessie lee, 2006-3-22, 出去状态为“不使用”的途程
        /// </summary>
        /// <param name="moCode"></param>
        /// <returns></returns>
        public object[] QueryNormalRouteByMOEnabled(string moCode, string routeCode)
        {
            string sql = "SELECT {0} FROM tblroute where routecode in (SELECT routecode FROM tblitem2route WHERE itemcode IN (SELECT itemcode FROM tblmo WHERE mocode = '{1}') " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and routecode like '{2}%' and enabled = '{3}'";
            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                moCode,
                routeCode,
                FormatHelper.TRUE_STRING
                );
            return this.DataProvider.CustomQuery(
                typeof(BenQGuru.eMES.Domain.BaseSetting.Route),
                new SQLCondition(sql)
                );
        }

        public object[] GetAllMORoutes(string moCode)
        {
            return this.DataProvider.CustomQuery(typeof(Route), new SQLCondition(string.Format("select {0} from  tblroute where routecode in (select routecode from tblmo2route  where mocode = '{1}') order by routecode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route)), moCode)));
        }

        public object[] GetAllReworkMOs()
        {
            SystemSettingFacade systemSettingFacade = new SystemSettingFacade(this.DataProvider);
            object[] parameters = systemSettingFacade.GetParametersByParameterValue(BenQGuru.eMES.Web.Helper.MOType.GroupType, BenQGuru.eMES.Web.Helper.MOType.MOTYPE_REWORKMOTYPE);
            if (parameters != null)
            {
                string tmpReworkType = string.Empty;
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i == 0)
                    {
                        tmpReworkType = "'" + ((Parameter)parameters[i]).ParameterCode + "'";

                    }
                    else
                    {
                        tmpReworkType += ",'" + ((Parameter)parameters[i]).ParameterCode + "'";
                    }
                }
                return this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(string.Format("select {0} from tblmo where motype in(" + tmpReworkType + ")  order by mocode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)))));
            }
            else
            {
                return null;
            }

        }

        public int QueryNormalRouteCountByMO(string moCode, string routeCode)
        {
            string sql = "SELECT count(*) FROM tblroute where routecode in (SELECT routecode FROM tblitem2route WHERE itemcode IN (SELECT itemcode FROM tblmo WHERE mocode = '{1}') " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ") and routecode like '{2}%'";
            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(BenQGuru.eMES.Domain.BaseSetting.Route)),
                moCode,
                routeCode
                );
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// sammer kong 2005/06/02
        /// </summary>
        /// <param name="moCode"></param>
        /// <returns></returns>
        public object[] GetAllOperationsByMoCode(string moCode)
        {
            if (moCode == "" || moCode == null)
            {
                return null;
            }
            else
            {
                return this.DataProvider.CustomQuery(
                    typeof(Route2Operation),
                    new SQLCondition(
                    string.Format("select {0} from TBLROUTE2OP where " +
                    " routecode in ( select routecode from TBLMO2ROUTE where mocode = '{1}')",
                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route2Operation)),
                    moCode.ToUpper())));
            }
        }


        /// <summary>
        /// ** 方法名: GetAllMO
        /// ** 功能描述:返回所有工单
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者:   vizo
        /// ** 日 期:   2005-03-31
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <returns>MO[]类型</returns>
        public object[] GetAllMO()
        {
            return this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(string.Format("select {0} from TBLMO where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by MOCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)))));
        }

        /// <summary>
        /// ** 方法名: GetMOByStatus
        /// ** 功能描述:根据状态返回工单
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者:   Simone Xu
        /// ** 日 期:   2005-06-24
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <returns>MO[]类型</returns>
        public object[] GetMOByStatus(string[] StatusList)
        {
            StringBuilder sqlbuilder = new StringBuilder();
            sqlbuilder.Append(string.Format("select {0} from TBLMO where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO))));
            sqlbuilder.Append(" AND MOSTATUS IN ('' ");
            foreach (string status in StatusList)
            {
                sqlbuilder.Append(string.Format(",'{0}'", status));
            }
            sqlbuilder.Append(")");
            sqlbuilder.Append(GlobalVariables.CurrentOrganizations.GetSQLCondition());
            sqlbuilder.Append(" order by MOCODE ");
            return this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(sqlbuilder.ToString()));
        }

        /// <summary>
        /// ** 方法名: GetMoByItemCode
        /// ** 功能描述:根据产品和状态状态返回工单
        /// ** 全局变量:
        /// ** 调用模块:
        /// ** 作 者:   Simone Xu
        /// ** 日 期:   2005-06-24
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <returns>MO[]类型</returns>
        public object[] GetMoByItemCode(string itemCoce, string[] StatusList)
        {

            StringBuilder sqlbuilder = new StringBuilder();
            sqlbuilder.Append(string.Format("select {0} from TBLMO where ITEMCODE='{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO)), itemCoce));
            sqlbuilder.Append(" AND MOSTATUS IN ('' ");
            foreach (string status in StatusList)
            {
                sqlbuilder.Append(string.Format(",'{0}'", status));
            }
            sqlbuilder.Append(")");
            sqlbuilder.Append(GlobalVariables.CurrentOrganizations.GetSQLCondition());
            sqlbuilder.Append(" order by MOCODE ");
            return this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(sqlbuilder.ToString()));
        }


        /// <summary>
        /// no need
        /// </summary>
        /// <returns></returns>
        public string[] GetMOStatuses()
        {
            return new string[]{
								   MOManufactureStatus.MOSTATUS_INITIAL,
								   MOManufactureStatus.MOSTATUS_CLOSE,
								   MOManufactureStatus.MOSTATUS_OPEN,
								   MOManufactureStatus.MOSTATUS_PENDING,
								   MOManufactureStatus.MOSTATUS_RELEASE
							   };
        }

        //通过工单Code获取对应的MOBOM(工单标准BOM)
        public object[] GetMOBOM(string moCode)
        {
            if (moCode == "" || moCode == null)
            {
                return null;
            }
            else
            {
                //				string sql = string.Format(@"SELECT tblmobom.mocode, tblmobom.itemcode, tblmobom.seq, tblmobom.mobomitemuom, tblmobom.mobitemcontype,
                //						    tblmobom.mobitemqty, tblmobom.mobitemefftime, tblmobom.eattribute1, tblmobom.mobiteminvtime, tblmobom.mobitemver,
                //						    tblmobom.muser, tblmobom.mobitemdesc, tblmobom.mtime, tblmobom.mobsitemcode, tblmobom.mdate, tblmobom.mobitemlocation,
                //							tblmobom.mobitemstatus, tblmobom.mobitemname, tblmobom.mobiteminvdate, tblmobom.mobitemcode,
                //							tblmobom.mobitemeffdate, tblmobom.mobitemecn from TBLMOBOM where 1=1 AND mocode = '{0}' ",moCode.ToUpper());
                //opcode select
                string sql = string.Format(@"SELECT mocode, itemcode, seq,mobomitemuom, mobitemcontype, mobitemqty,mobitemefftime, eattribute1, mobiteminvtime,mobitemver, muser, mobitemdesc,mtime, mobsitemcode, mdate,mobitemlocation, mobitemstatus, mobitemname,mobiteminvdate, mobitemcode, mobitemeffdate,mobitemecn, opcode from TBLMOBOM where 1=1 AND mocode = '{0}' ", moCode.ToUpper());
                return this.DataProvider.CustomQuery(
                    typeof(MOBOM),
                    new SQLCondition(sql));
            }

        }

        //通过工单Code(一个或多个)和OPCode获取对应的MOBOM(工单标准BOM) 数量为0(需求数量为0)的不获取
        public object[] GetMOBOM(string moCode, string opCode)
        {
            if (moCode == "" || moCode == null)
            {
                return null;
            }
            else
            {
                string opCodeCondition = string.Empty;	//OPCode查询条件,支持多个opcode查询,以逗号隔开,逗号前后不要带空格
                if (opCode != null && opCode.Trim() != string.Empty)
                {
                    opCodeCondition = string.Format(" AND OPCODE in ({0})  ", FormatHelper.ProcessQueryValues(opCode.Trim())); ;
                }
                string sql = string.Format(@"SELECT mocode, itemcode, seq,
													mobomitemuom, mobitemcontype, mobitemqty,
													mobitemefftime, eattribute1, mobiteminvtime,
													mobitemver, muser, mobitemdesc,
													mtime, mobsitemcode, mdate,
													mobitemlocation, mobitemstatus, mobitemname,
													mobiteminvdate, mobitemcode, mobitemeffdate,
													mobitemecn, opcode from TBLMOBOM where MOBITEMQTY >0 and mocode in ( {0} ) {1} ", FormatHelper.ProcessQueryValues(moCode.ToUpper()), opCodeCondition);
                return this.DataProvider.CustomQuery(
                typeof(MOBOM),
                new SQLCondition(sql));
            }

        }

        #endregion


        #region RCardLink
        /// <summary>
        /// 
        /// </summary>
        public MO2RCARDLINK CreateNewMO2RCardLink()
        {
            return new MO2RCARDLINK();
        }

        public void AddMO2RCardLink(MO2RCARDLINK mO2RCardLink)
        {
            this._helper.AddDomainObject(mO2RCardLink);
        }

        public void UpdateMO2RCardLink(MO2RCARDLINK mO2RCardLink)
        {
            this._helper.UpdateDomainObject(mO2RCardLink);
        }

        public void DeleteMO2RCardLink(MO2RCARDLINK mO2RCardLink)
        {
            this._helper.DeleteDomainObject(mO2RCardLink);
        }

        public void DeleteMO2RCardLink(MO2RCARDLINK[] mO2RCardLink)
        {
            this._helper.DeleteDomainObject(mO2RCardLink);
        }

        public object GetMO2RCardLink(string pK)
        {
            return this.DataProvider.CustomSearch(typeof(MO2RCARDLINK), new object[] { pK });
        }

        public object[] GetMO2RCardLinkByMoCode(string moCode)
        {
            object[] obj = null;
            string sqlQueryString = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2RCARDLINK)) + " from TBLMO2RCARDLINK where  moCode='" + moCode + "' ";

            sqlQueryString += " order by RCard";
            obj = this.DataProvider.CustomQuery(typeof(MO2RCARDLINK), new SQLCondition(sqlQueryString));

            return obj;
        }

        public object GetMO2RCardLinkByRcard(string rcard, string moCode)
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(MO2RCARDLINK), new SQLCondition("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2RCARDLINK))
                + " from TBLMO2RCARDLINK where rcard='" + rcard + "' and mocode='" + moCode + "'"));

            object objReturn = null;

            if (obj != null && obj.Length > 0)
            {
                objReturn = obj[0];
            }

            return objReturn;
        }

        public object[] QueryMO2RCardLink(string rcard, string moCode)
        {

            string sql = "SELECT * FROM TBLMO2RCARDLINK WHERE 1=1 ";
            if (rcard != "")
            {
                sql += " AND RCARD like '%" + FormatHelper.CleanString(rcard) + "%'";
            }
            if (moCode != "")
            {
                sql += " AND mocode='" + FormatHelper.CleanString(moCode).ToUpper() + "'";
            }

            sql += " ORDER BY RCARD ";

            object[] obj = this.DataProvider.CustomQuery(typeof(MO2RCARDLINK), new SQLCondition(sql));

            return obj;

        }

        public object[] GetMO2RCardLinkByMoCode(string moCode, string beginRcard, string endRcard, string printTimes)
        {
            object[] obj = null;
            string sqlQueryString = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2RCARDLINK)) + " from TBLMO2RCARDLINK where  moCode='" + moCode + "' ";

            if (beginRcard != "")
            {
                sqlQueryString += " and RCard >= '" + beginRcard + "'";
            }
            if (endRcard != "")
            {
                sqlQueryString += " and RCard <= '" + endRcard + "'";
            }
            if (printTimes != "")
            {
                sqlQueryString += " and PrintTimes = '" + printTimes + "' ";
            }

            sqlQueryString += " order by RCard";
            obj = this.DataProvider.CustomQuery(typeof(MO2RCARDLINK), new SQLCondition(sqlQueryString));

            return obj;
        }

        public string GetMmachineType(string moCode)
        {

            //string sql = "SELECT mr.MMACHINETYPE  FROM TBLMO MO ,  TBLMATERIAL mr WHERE MO.ITEMCODE = mr.MCODE AND MO.MOCODE = '" + moCode + "'";
            //sql += " and mo.orgid = mr.orgid ";
            //object[] obj = this.DataProvider.CustomQuery(typeof(Domain.MOModel.Material), new SQLCondition(sql));
            //if (obj != null && obj.Length > 0)
            //{
            //    Domain.MOModel.Material material = (Domain.MOModel.Material)obj[0];
            //    return material.MaterialMachineType;

            //}

            return "";


        }

        public object GetMaterial(string moCode, decimal orgId)
        {
            return this._domainDataProvider.CustomSearch(typeof(MO2SAP), new object[] { moCode, orgId });
        }

        public object GetMaterial1(string moCode, decimal orgId)
        {
            return this._domainDataProvider.CustomSearch(typeof(BenQGuru.eMES.Domain.MOModel.Material), new object[] { moCode, orgId });
        }
        /// <summary>
        /// ** 功能描述:	获得所有的OffMoCard
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-12-15 18:22:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OffMoCard的总记录数</returns>
        public object[] GetAllMO2RCardLink()
        {
            return this.DataProvider.CustomQuery(typeof(MO2RCARDLINK), new SQLCondition(string.Format("select {0} from TBLMO2RCARDLINK order by PK", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MO2RCARDLINK)))));
        }
        #endregion

        #region MOViewField
        /// <summary>
        /// 工单
        /// </summary>
        public MOViewField CreateNewMOViewField()
        {
            return new MOViewField();
        }

        public void AddMOViewField(MOViewField mOViewField)
        {
            this._helper.AddDomainObject(mOViewField);
        }

        public void UpdateMOViewField(MOViewField mOViewField)
        {
            this._helper.UpdateDomainObject(mOViewField);
        }

        public void DeleteMOViewField(MOViewField mOViewField)
        {
            this._helper.DeleteDomainObject(mOViewField);
        }

        public void DeleteMOViewField(MOViewField[] mOViewField)
        {
            this._helper.DeleteDomainObject(mOViewField);
        }

        public object GetMOViewField(string userCode, decimal sequence)
        {
            return this.DataProvider.CustomSearch(typeof(MOViewField), new object[] { userCode, sequence });
        }

        /// <summary>
        /// ** 功能描述:	查询MOViewField的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-12-9 9:19:26
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="userCode">UserCode，模糊查询</param>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <returns> MOViewField的总记录数</returns>
        public int QueryMOViewFieldCount(string userCode, decimal sequence)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMOVIEWFIELD where 1=1 and USERCODE like '{0}%'  and SEQ like '{1}%' ", userCode, sequence)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询MOViewField
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-12-9 9:19:26
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="userCode">UserCode，模糊查询</param>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> MOViewField数组</returns>
        public object[] QueryMOViewField(string userCode, decimal sequence, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(MOViewField), new PagerCondition(string.Format("select {0} from TBLMOVIEWFIELD where 1=1 and USERCODE like '{1}%'  and SEQ like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MOViewField)), userCode, sequence), "USERCODE,SEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的MOViewField
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-12-9 9:19:26
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>MOViewField的总记录数</returns>
        public object[] GetAllMOViewField()
        {
            return this.DataProvider.CustomQuery(typeof(MOViewField), new SQLCondition(string.Format("select {0} from TBLMOVIEWFIELD order by USERCODE,SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(MOViewField)))));
        }

        public object[] QueryMOViewFieldByUserCode(string userCode)
        {
            string strSql = "SELECT * FROM tblMOViewField WHERE UserCode='" + userCode + "' ORDER BY SEQ ";
            return this.DataProvider.CustomQuery(typeof(MOViewField), new SQLCondition(strSql));
        }
        public object[] QueryMOViewFieldDefault()
        {
            string strSql = "SELECT * FROM tblMOViewField WHERE UserCode='MO_FIELD_LIST_SYSTEM_DEFAULT' ORDER BY SEQ ";
            return this.DataProvider.CustomQuery(typeof(MOViewField), new SQLCondition(strSql));
        }

        public void SaveMOViewField(string userCode, string moFieldList)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                string strSql = "DELETE FROM tblMOViewField WHERE UserCode='" + userCode + "' ";
                this.DataProvider.CustomExecute(new SQLCondition(strSql));
                object[] objs = this.QueryMOViewFieldDefault();
                Hashtable htDesc = new Hashtable();
                if (objs != null)
                {
                    for (int i = 0; i < objs.Length; i++)
                    {
                        MOViewField field = (MOViewField)objs[i];
                        htDesc.Add(field.FieldName, field.Description);
                    }
                }
                string[] moField = moFieldList.Split(';');
                for (int i = 0; i < moField.Length; i++)
                {
                    if (moField[i].Trim() != string.Empty)
                    {
                        MOViewField field = new MOViewField();
                        field.UserCode = userCode;
                        field.Sequence = i;
                        field.FieldName = moField[i];
                        field.Description = htDesc[field.FieldName].ToString();
                        this.AddMOViewField(field);
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

        #region DefaultItem2Route
        /// <summary>
        /// 
        /// </summary>
        public DefaultItem2Route CreateNewDefaultItem2Route()
        {
            return new DefaultItem2Route();
        }

        public void AddDefaultItem2Route(DefaultItem2Route defaultItem2Route)
        {
            this._helper.AddDomainObject(defaultItem2Route);
        }

        public void UpdateDefaultItem2Route(DefaultItem2Route defaultItem2Route)
        {
            this._helper.UpdateDomainObject(defaultItem2Route);
        }

        public void DeleteDefaultItem2Route(DefaultItem2Route defaultItem2Route)
        {
            this._helper.DeleteDomainObject(defaultItem2Route);
        }

        public object GetDefaultItem2Route(string itemCode)
        {
            return this.DataProvider.CustomSearch(typeof(DefaultItem2Route), new object[] { itemCode });
        }

        /// <summary>
        /// ** 功能描述:	查询DefaultItem2Route的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-9-25 10:18:25
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="routeCode">RouteCode，模糊查询</param>
        /// <returns> DefaultItem2Route的总记录数</returns>
        public int QueryDefaultItem2RouteCount(string itemCode, string routeCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TblDefaultItem2Route where 1=1 and ItemCode like '{0}%'  and RouteCode like '{1}%' ", itemCode, routeCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询DefaultItem2Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-9-25 10:18:25
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="routeCode">RouteCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> DefaultItem2Route数组</returns>
        public object[] QueryDefaultItem2Route(string itemCode, string routeCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(DefaultItem2Route), new PagerCondition(string.Format("select {0} from TblDefaultItem2Route where 1=1 and ItemCode like '{1}%'  and RouteCode like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DefaultItem2Route)), itemCode, routeCode), "ItemCode,RouteCode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	分页查询DefaultItem2Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-9-25 10:18:25
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="routeCode">RouteCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> DefaultItem2Route数组</returns>
        public object[] QueryUnSelectDefaultItem2Route(string itemCode, string routeCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(DefaultItem2Route), new PagerCondition(string.Format("select {0} from TblDefaultItem2Route where 1=1 and ItemCode like '{1}%'  and RouteCode like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DefaultItem2Route)), itemCode, routeCode), "ItemCode,RouteCode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的DefaultItem2Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-9-25 10:18:25
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>DefaultItem2Route的总记录数</returns>
        public object[] GetAllDefaultItem2Route()
        {
            return this.DataProvider.CustomQuery(typeof(DefaultItem2Route), new SQLCondition(string.Format("select {0} from TblDefaultItem2Route order by ItemCode,RouteCode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(DefaultItem2Route)))));
        }

        public object[] GetAllMOBOMVersion()
        {
            string sql = string.Format("select distinct mobom from tblmo where mobom is not null order by mobom");

            return this.DataProvider.CustomQuery(typeof(MO), new SQLCondition(sql));
        }

        #endregion

        #region FirstCheckByMO
        /// <summary>
        /// 
        /// </summary>
        public FirstCheckByMO CreateNewFirstCheckByMO()
        {
            return new FirstCheckByMO();
        }

        public void AddFirstCheckByMO(FirstCheckByMO firstCheckByMO)
        {
            this._helper.AddDomainObject(firstCheckByMO);
        }

        public void UpdateFirstCheckByMO(FirstCheckByMO firstCheckByMO)
        {
            this._helper.UpdateDomainObject(firstCheckByMO);
        }

        public void DeleteFirstCheckByMO(FirstCheckByMO firstCheckByMO)
        {
            this._helper.DeleteDomainObject(firstCheckByMO);
        }

        public void DeleteFirstCheckByMO(FirstCheckByMO[] firstCheckByMO)
        {
            this._helper.DeleteDomainObject(firstCheckByMO);
        }

        public object GetFirstCheckByMO(string moCode)
        {
            return this.DataProvider.CustomSearch(typeof(FirstCheckByMO), new object[] { moCode });
        }

        public object[] QueryFirstCheckByMO(string moCode, int checkDate, int inclusive, int exclusive)
        {
            string sql = "select A.MOCODE,B.ITEMCODE,C.ITEMNAME,A.CHECKDATE,A.CHECKRESULT,A.MEMO,A.MUSER,A.MDATE,A.MTIME";
            sql += " from TBLFIRSTCHECKBYMO A left join TBLMO B on A.MOCODE = B.MOCODE";
            sql += " left join TBLITEM C on B.ITEMCODE = C.ITEMCODE";
            sql += " AND b.orgid=c.orgid";
            sql += " where 1=1";
            if (!String.IsNullOrEmpty(moCode))
            {
                sql += string.Format(" and A.MOCODE in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }
            if (checkDate > 0)
            {
                sql += " and A.CHECKDATE = '" + checkDate + "'";
            }

            return this.DataProvider.CustomQuery(typeof(FirstCheckByMOForQuery),
                new PagerCondition(sql, "A.CHECKDATE,A.MOCODE", inclusive, exclusive));
        }

        public int QueryFirstCheckByMOCount(string moCode, int checkDate)
        {
            string sql = "select count(*)";
            sql += " from TBLFIRSTCHECKBYMO A left join TBLMO B on A.MOCODE = B.MOCODE";
            sql += " left join TBLITEM C on B.ITEMCODE = C.ITEMCODE";
            sql += " AND b.orgid=c.orgid";
            sql += " where 1=1";
            if (!String.IsNullOrEmpty(moCode))
            {
                sql += string.Format(" and A.MOCODE in ({0})", FormatHelper.ProcessQueryValues(moCode));
            }
            if (checkDate > 0)
            {
                sql += " and A.CHECKDATE = '" + checkDate + "'";
            }
            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }

        public object GetFirstCheckByMO(string moCode, int checkDate)
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(FirstCheckByMO), new SQLCondition("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(FirstCheckByMO))
                + " from TBLFIRSTCHECKBYMO where moCode='" + moCode + "' and checkdate=" + checkDate + ""));

            object objReturn = null;

            if (obj != null && obj.Length > 0)
            {
                objReturn = obj[0];
            }

            return objReturn;
        }
        #endregion

        #region 工单bom比对 (MOBOM表中的数据与OPBOM比对) 返回三个结果集 : 比对成功 , 只在工单生产BOM 中(即OPBOM) ,只在工单标准bom中(即MOBOM中的数据)

        public Hashtable CompareMOBOM(object[] moBOMs, string itemCode, string mocode, string routeCode)
        {
            //getOPBOMCode by routeCode
            string errorMSG = string.Empty;
            //			if(moBOMs == null)
            //			{
            //				ExceptionManager.Raise(this.GetType().BaseType,"$Error_NullMOBOMS");
            //			}
            object[] moRouteObjs = QueryMORoutes(mocode, routeCode);
            if (moRouteObjs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_MORouteNOExist");
            }

            object mo = GetMO(mocode);

            OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
            //OPBOM 中需要比对的数据 ()
            object[] opbomObjs = opBOMFacade.QueryOPBOMDetail(itemCode, string.Empty, ((MO2Route)moRouteObjs[0]).OPBOMCode, ((MO2Route)moRouteObjs[0]).OPBOMVersion, routeCode, string.Empty, int.MinValue, int.MaxValue, ((MO)mo).OrganizationID);
            if (opbomObjs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_OPBOMNOExist", String.Format("[$ItemCode='{0}']", itemCode));
            }

            bool iflag = false;
            bool iCheckPass = true;
            Decimal iOPBOMItemQty = 0;

            //需要比对的
            //子阶料料号
            //子阶料数量

            Hashtable returnHT = new Hashtable();
            ArrayList SucessResult = new ArrayList();				//比对成功
            ArrayList InMORouteResult = new ArrayList();			//只在工单生产BOM 中
            ArrayList InMOStandardBOMResult = new ArrayList();		//只在工单标准bom中

            // 比对成功的
            // 只在工单生产BOM 中
            // 只在工单标准bom中

            //以工单标准BOM为基准， 比对结果为比对成功，或者只在标准bom中（结果有两种：子阶料不存在 ； 子阶料存在 ，但是数量不对）
            if (moBOMs != null && opbomObjs != null)
                for (int i = 0; i < moBOMs.Length; i++)
                {
                    //求得opbom中对应的料品
                    iflag = false;
                    iOPBOMItemQty = 0;
                    for (int j = 0; j < opbomObjs.Length; j++)
                    {
                        if ((((MOBOM)moBOMs[i]).MOBOMItemCode.ToUpper() == ((OPBOMDetail)opbomObjs[j]).OPBOMItemCode.ToUpper()))
                        {
                            //子阶料存在
                            iflag = true;
                            iOPBOMItemQty += ((OPBOMDetail)opbomObjs[j]).OPBOMItemQty;
                        }
                    }
                    //modified by jessie lee,2005/11/22
                    //比对成功：子阶料料号和相应的单机用量皆一致
                    //单机用量不一致：子阶料料号一致，相应的单机用量不一致
                    if (iflag)//子阶料存在
                    {
                        ((MOBOM)moBOMs[i]).OPBOMItemQty = iOPBOMItemQty;
                        if (((MOBOM)moBOMs[i]).MOBOMItemQty != iOPBOMItemQty)
                        {
                            iCheckPass = false;
                            errorMSG = "$Error_MOBOMItemQty_NotEqualOPBOMQty";
                            errorMSG = "单机用量不一致";
                            ((MOBOM)moBOMs[i]).MOBOMException = errorMSG;
                            //InMOStandardBOMResult.Add(moBOMs[i]);
                            //子阶料存在 ，但是数量不对, 即只在工单标准bom中
                        }
                        else
                        {
                            errorMSG = "$MSG_MOBOMItemQty_EqualOPBOMQty";
                            errorMSG = "比对成功";
                            ((MOBOM)moBOMs[i]).MOBOMException = errorMSG;
                            //SucessResult.Add(moBOMs[i]);
                            //子阶料存在，数量正确。比对成功
                        }
                        SucessResult.Add(moBOMs[i]);
                    }
                    else
                    {
                        //子阶料不存在， 即只在工单标准bom中
                        iCheckPass = false;
                        errorMSG = "$Error_MOBOMItem_NotExistOPBOM";
                        errorMSG = "只存在于工单用料清单中";
                        ((MOBOM)moBOMs[i]).MOBOMException = errorMSG;
                        InMOStandardBOMResult.Add(moBOMs[i]);
                    }
                    ((MOBOM)moBOMs[i]).MOBOMException = errorMSG;
                }

            //以opbom为基准， 比对结果为只在工单生产BOM 中
            if (opbomObjs != null)
                for (int i = 0; i < opbomObjs.Length; i++)
                {
                    //求得opbom中对应的料品
                    iflag = false;
                    if (moBOMs != null)
                    {
                        for (int j = 0; j < moBOMs.Length; j++)
                        {
                            if ((((MOBOM)moBOMs[j]).MOBOMItemCode.ToUpper() == ((OPBOMDetail)opbomObjs[i]).OPBOMItemCode.ToUpper()))
                            {
                                iflag = true;
                                break;
                            }
                        }

                        if (!iflag)
                        {
                            //子阶料只在工单生产BOM 中 OPBOM中
                            errorMSG = "$Error_MOBOMItem_NotExistMOBOM";
                            errorMSG = "只存在于工单工序BOM中";
                            InMORouteResult.Add((OPBOMDetail)opbomObjs[i]);

                        }

                    }
                    else
                    {
                        errorMSG = "只存在于工单生产BOM(OPBOM) 中";
                        //如果工单标准bom不存在,子阶料只在工单生产BOM 中
                        InMORouteResult.Add((OPBOMDetail)opbomObjs[i]);
                    }
                }

            returnHT["SucessResult"] = SucessResult;
            returnHT["InMORouteResult"] = InMORouteResult;
            returnHT["InMOStandardBOMResult"] = InMOStandardBOMResult;

            return returnHT;
        }

        #endregion

        #region 工单BOM比对OPBOM
        public MOBOM[] CompareMOBOMOPBOM(MOBOM[] moBOMs, string itemCode, string mocode, string routeCode)
        {
            //getOPBOMCode by routeCode
            string errorMSG = string.Empty;
            if (moBOMs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_NullMOBOMS");
            }
            object[] moRouteObjs = QueryMORoutes(mocode, routeCode);
            if (moRouteObjs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_MORouteNOExist");
            }

            object mo = GetMO(mocode);

            OPBOMFacade opBOMFacade = new OPBOMFacade(this.DataProvider);
            object[] opbomObjs = opBOMFacade.QueryOPBOMDetail(itemCode, string.Empty, ((MO2Route)moRouteObjs[0]).OPBOMCode, ((MO2Route)moRouteObjs[0]).OPBOMVersion, routeCode, string.Empty, int.MinValue, int.MaxValue, ((MO)mo).OrganizationID);
            if (opbomObjs == null)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_OPBOMNOExist", String.Format("[$ItemCode='{0}']", itemCode));
            }

            bool iflag = false;
            bool iCheckPass = true;
            Decimal iOPBOMItemQty = 0;

            for (int i = 0; i < moBOMs.Length; i++)
            {
                //求得opbom中对应的料品
                iflag = false;
                iOPBOMItemQty = 0;
                for (int j = 0; j < opbomObjs.Length; j++)
                {
                    if ((moBOMs[i].MOBOMItemCode.ToUpper() == ((OPBOMDetail)opbomObjs[j]).OPBOMItemCode.ToUpper()))
                    {
                        iflag = true;
                        iOPBOMItemQty += ((OPBOMDetail)opbomObjs[j]).OPBOMItemQty;
                    }
                }
                if (iflag)
                {
                    moBOMs[i].OPBOMItemQty = iOPBOMItemQty;
                    if (moBOMs[i].MOBOMItemQty != iOPBOMItemQty)
                    {
                        iCheckPass = false;
                        errorMSG = "$Error_MOBOMItemQty_NotEqualOPBOMQty";
                    }
                    else
                    {
                        errorMSG = "$MSG_MOBOMItemQty_EqualOPBOMQty";
                    }
                }
                else
                {
                    iCheckPass = false;
                    errorMSG = "$Error_MOBOMItem_NotExistOPBOM";
                }
                moBOMs[i].MOBOMException = errorMSG;
            }

            if (!iCheckPass)
            {
                MO currentMO = (MO)GetMO(mocode);
                currentMO.IsBOMPass = BenQGuru.eMES.Web.Helper.IsPass.ISPASS_NOPASS.ToString();
                UpdateMO(currentMO, false);
            }
            else
            {
                MO currentMO = (MO)GetMO(mocode);
                currentMO.IsBOMPass = BenQGuru.eMES.Web.Helper.IsPass.ISPASS_PASS.ToString();
                UpdateMO(currentMO, false);
            }
            return moBOMs;
        }

        public MOBOM CreateNewMOBOM()
        {
            return new MOBOM();
        }

        public void AddMOBOM(MOBOM moBOM)
        {
            this._helper.AddDomainObject(moBOM);
        }

        public void DeleteMOBOMByMOCode(string moCode)
        {
            string sql = "DELETE FROM tblMOBOM WHERE mocode='" + moCode.ToUpper() + "'";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public decimal GetMOBOMMaxSequence(string moCode)
        {
            string sql = "SELECT NVL(MAX(seq), 0) AS seq FROM tblmobom WHERE mocode='" + moCode.ToUpper() + "'";
            object[] moboms = this.DataProvider.CustomQuery(typeof(MOBOM), new SQLCondition(sql));
            MOBOM moBOM = moboms[0] as MOBOM;
            if (moBOM == null || moBOM.Sequence == 0)
            {
                return 1;
            }
            else
            {
                return moBOM.Sequence + 1;
            }
        }

        //add By Jarvis For DeductQty 20120315
        public object[] QueryMoBom(string itemCode, string mcode, string moCode)
        {
            string sql = string.Format(@"SELECT * from TBLMOBOM where MOBITEMQTY >0");

            if (itemCode.Trim() != "")
            {
                sql += " And itemcode='" + itemCode + "'";
            }
            if (mcode.Trim() != "")
            {
                sql += " And MOBITEMCODE='" + mcode + "'";
            }
            if (moCode.Trim() != "")
            {
                sql += " And mocode='" + moCode + "'";
            }
            return this.DataProvider.CustomQuery(typeof(MOBOM), new SQLCondition(sql));

        }

        #endregion

        #region 关单时清除Simulation (tblsimulation ,tblsimulationreport) , 关单成功后执行此方法

        private void ClearSimulation(MO mo)
        {
            this.Deletetblsimulation(mo.MOCode);			//清除tblsimulation
            //this.Deletetblsimulationreport(mo.MOCode);	//清除tblsimulationreport	,暂时不清,用于报表查询
            this.Deletetblmobom(mo.MOCode);					//清除tblmobom
        }

        #region 删除相关表

        //清除tblsimulation
        private void Deletetblsimulation(string moCode)
        {
            string sql = string.Format(" delete from tblsimulation where mocode ='{0}' ", moCode);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        //清除tblsimulationreport
        private void Deletetblsimulationreport(string moCode)
        {

            string sql = string.Format(" delete from tblsimulationreport where mocode ='{0}' ", moCode);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        //清除tblmobom
        private void Deletetblmobom(string moCode)
        {

            string sql = string.Format(" delete from tblmobom where mocode ='{0}' ", moCode);
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        //清除tblpackingchk
        private void Deletetblpackingchk()
        {
            string sql = "DELETE FROM tblpackingchk where not exists(select 1 from tblsimulation where tblpackingchk.rcard = tblsimulation.rcard ) ";
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }


        #endregion

        #endregion

        #region for cs Datacollect
        /// <summary>
        /// 工单对应的当前途程和该工单对应的重工需求单中维护的所有的途程
        /// </summary>
        /// <param name="moCode"></param>
        /// <returns></returns>
        public object[] GetNormalAndReworkRouteByMOCode(string moCode)
        {
            string selectSql = "select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route)) + " from tblroute where routecode in("
                // 正常途程和重开工单重工的工单对应的途程
                             + " select routecode from tblmo2route where mocode =$moCode1"

                        // 工单对应的重工需求单:在线重工的工单对应的RunningCard属于重工需求单的范围
                             + " union all select NEWROUTECODE from tblreworksheet where reworkcode in "
                             + "(select distinct reworkcode from tblreworkrange where rcard in"
                             + "(select MORCARDSTART from tblmorcard where mocode=$moCode2 )))";
            return this.DataProvider.CustomQuery(typeof(Route), new SQLParamCondition(selectSql, new SQLParameter[] { new SQLParameter("moCode1", typeof(string), moCode.ToUpper()), new SQLParameter("moCode2", typeof(string), moCode.ToUpper()) }));
        }

        //Laws Lu,2005/10/31，修改	统一工单数量计算的标准
        //Laws Lu，2005/11/18，修改	更新行数为0，抛出异常
        public void UpdateMOOutPutQty(string moCode/*,int outPutQty*/, int qty)
        {

            string updateSql = "update TBLMO "
                    + " set MOACTQTY=MOACTQTY+" + qty.ToString()
                    + " where MOCODE='" + moCode.Trim() + "'";

            int iReturn = 0;
            iReturn = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).CustomExecuteWithReturn(new SQLCondition(updateSql));

            if (iReturn <= 0)
            {
                throw new Exception("$Error_UpdateMO $CS_PLEASE_RETRY");
            }
        }

        //Laws Lu,2005/10/31，修改	统一工单数量计算的标准
        public void UpdateMOInPutQty(string moCode/*,int outPutQty*/, MO mo, int qty)
        {
            string updateSql = String.Empty;
            if (mo != null && mo.MOStatus == Web.Helper.MOManufactureStatus.MOSTATUS_OPEN)
            {
                updateSql = "update TBLMO "
                    + " set MOINPUTQTY=MOINPUTQTY+" + qty.ToString()
                    + " where MOCODE='" + moCode.Trim() + "'";
            }
            else
            {
                updateSql = "update TBLMO "
                    + " set MOINPUTQTY=MOINPUTQTY+" + qty.ToString()
                    + ",MOSTATUS=(case  mostatus  when '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' then mostatus else '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' end)"
                    + ",MOACTSTARTDATE=(case  mostatus  when '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' then MOACTSTARTDATE else " + FormatHelper.TODateInt(DateTime.Now) + " end)"
                    //+",MOACTSTARTDATE=" + FormatHelper.TODateInt(DateTime.Now)
                    + " where MOCODE='" + moCode.Trim() + "'";
            }
            int iReturn = 0;
            iReturn = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).CustomExecuteWithReturn(new SQLCondition(updateSql));

            if (iReturn <= 0)
            {
                throw new Exception("$Error_UpdateMO $CS_PLEASE_RETRY");
            }

        }

        //Laws Lu,2005/10/31，修改	统一工单数量计算的标准
        public void UpdateMOQty(string moCode, string actType, int offMoQty)
        {
            string updateSql = String.Empty;

            #region

            int morule = 0;
            string sisql = "select decode(sum(s.idmergerule),'',0,sum(s.idmergerule)) as idmergerule from tblsimulation s where s.mocode = '" + moCode + "' and s.iscom = 1 AND s.productstatus = 'GOOD'";
            object[] simobj = this.DataProvider.CustomQuery(typeof(Simulation), new SQLCondition(sisql));
            if (simobj != null)
            {
                morule = Convert.ToInt32(((Simulation)simobj[0]).IDMergeRule);
            }

            #endregion

            //Laws Lu,2006/11/13 uniform system collect date
            DBDateTime dbDateTime;

            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            if (actType == ActionType.DataCollectAction_OffMo)
            {
                updateSql = "update TBLMO "
                    + " set MOACTQTY='" + morule + "'"
                    //+ " set MOACTQTY=(SELECT COUNT(*) FROM tblsimulation WHERE mocode = '" + moCode + "' AND iscom = 1 AND productstatus='GOOD')"
                    + ",MOINPUTQTY=(SELECT decode(SUM (EAttribute1),null,0,SUM (EAttribute1)) FROM tblrptreallineqty WHERE mocode = '" + moCode + "')"
                    + ",MOSTATUS=(case  mostatus  when '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' then mostatus else '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' end)"
                    + ",MOACTSTARTDATE=(case  mostatus  when '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' then MOACTSTARTDATE else " + dbDateTime.DBDate + " end)"
                    + ",OFFMOQTY = OFFMOQTY + " + offMoQty.ToString()
                    + " where MOCODE='" + moCode + "'";
            }
            else
            {
                updateSql = "update TBLMO "
                    + " set MOACTQTY='" + morule + "'"
                    //+ " set MOACTQTY=(SELECT COUNT(*) FROM tblsimulation WHERE mocode = '" + moCode + "' AND iscom = 1 AND productstatus='GOOD')"
                    + ",MOINPUTQTY=(SELECT decode(SUM (EAttribute1),null,0,SUM (EAttribute1)) FROM tblrptreallineqty WHERE mocode = '" + moCode + "')"
                    + ",MOSTATUS=(case  mostatus  when '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' then mostatus else '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' end)"
                    + ",MOACTSTARTDATE=(case  mostatus  when '" + Web.Helper.MOManufactureStatus.MOSTATUS_OPEN + "' then MOACTSTARTDATE else " + dbDateTime.DBDate + " end)"
                    + " where MOCODE='" + moCode + "'";
            }

            int iReturn = 0;
            iReturn = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).CustomExecuteWithReturn(new SQLCondition(updateSql));

            if (iReturn <= 0)
            {
                throw new Exception("$Error_UpdateMO $CS_PLEASE_RETRY");
            }
        }

        //Andy xin，修改	统一工单完工数量计算的标准
        public void UpdateMOACTQTY(string moCode)
        {
            string updateSql = String.Empty;

            //Laws Lu,2006/11/13 uniform system collect date
            DBDateTime dbDateTime;

            dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);

            updateSql = "update TBLMO  set MOACTQTY=(SELECT DECODE( sum(GOODQTY),'',0,SUM(GOODQTY) ) AS MOACTQTY FROM TBLLOTSIMULATION  WHERE mocode = '" + moCode + "' AND iscom = 1 AND productstatus='GOOD') WHERE mocode = '" + moCode + "'";              

            int iReturn = 0;
            iReturn = ((BenQGuru.eMES.Common.DomainDataProvider.SQLDomainDataProvider)this.DataProvider).CustomExecuteWithReturn(new SQLCondition(updateSql));

            if (iReturn <= 0)
            {
                throw new Exception("$Error_UpdateMO $CS_PLEASE_RETRY");
            }
        }
        #endregion

        #region OffMoCard
        /// <summary>
        /// 
        /// </summary>
        public OffMoCard CreateNewOffMoCard()
        {
            return new OffMoCard();
        }

        public void AddOffMoCard(OffMoCard offMoCard)
        {
            this._helper.AddDomainObject(offMoCard);
        }

        public void UpdateOffMoCard(OffMoCard offMoCard)
        {
            this._helper.UpdateDomainObject(offMoCard);
        }

        public void DeleteOffMoCard(OffMoCard offMoCard)
        {
            this._helper.DeleteDomainObject(offMoCard);
        }

        public void DeleteOffMoCard(OffMoCard[] offMoCard)
        {
            this._helper.DeleteDomainObject(offMoCard);
        }

        public object GetOffMoCard(string pK)
        {
            return this.DataProvider.CustomSearch(typeof(OffMoCard), new object[] { pK });
        }

        public object GetOffMoCardByRcard(string rcard)
        {
            object[] obj = this.DataProvider.CustomQuery(typeof(OffMoCard), new SQLCondition("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(OffMoCard))
                + " from TBLOffMoCard where rcard='" + rcard + "' order by mdate desc,mtime desc"));

            object objReturn = null;

            if (obj != null && obj.Length > 0)
            {
                objReturn = obj[0];
            }

            return objReturn;
        }

        /// <summary>
        /// ** 功能描述:	查询OffMoCard的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-12-15 18:22:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="pK">PK，模糊查询</param>
        /// <returns> OffMoCard的总记录数</returns>
        public int QueryOffMoCardCount(string pK)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOffMoCard where 1=1 and PK like '{0}%' ", pK)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询OffMoCard
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-12-15 18:22:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="pK">PK，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> OffMoCard数组</returns>
        public object[] QueryOffMoCard(string pK, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OffMoCard), new PagerCondition(string.Format("select {0} from TBLOffMoCard where 1=1 and PK like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OffMoCard)), pK), "PK", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的OffMoCard
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-12-15 18:22:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>OffMoCard的总记录数</returns>
        public object[] GetAllOffMoCard()
        {
            return this.DataProvider.CustomQuery(typeof(OffMoCard), new SQLCondition(string.Format("select {0} from TBLOffMoCard order by PK", DomainObjectUtility.GetDomainObjectFieldsString(typeof(OffMoCard)))));
        }


        #endregion

        #region Resource2MO
        /// <summary>
        /// 
        /// </summary>
        public Resource2MO CreateNewResource2MO()
        {
            return new Resource2MO();
        }

        public void AddResource2MO(Resource2MO resource2MO)
        {
            string strSql = "select max(seq) seq from tblres2mo";
            int iSeq = 0;
            object[] objs = this.DataProvider.CustomQuery(typeof(Resource2MO), new SQLCondition(strSql));
            if (objs != null && objs.Length > 0)
            {
                Resource2MO res2mo = (Resource2MO)objs[0];
                iSeq = Convert.ToInt32(res2mo.Sequence) + 1;
            }
            resource2MO.Sequence = iSeq;
            this._helper.AddDomainObject(resource2MO);
        }

        public void UpdateResource2MO(Resource2MO resource2MO)
        {
            this._helper.UpdateDomainObject(resource2MO);
        }

        public void DeleteResource2MO(Resource2MO resource2MO)
        {
            this._helper.DeleteDomainObject(resource2MO);
        }

        public void DeleteResource2MO(Resource2MO[] resource2MO)
        {
            this._helper.DeleteDomainObject(resource2MO);
        }

        public object GetResource2MO(decimal sequence)
        {
            return this.DataProvider.CustomSearch(typeof(Resource2MO), new object[] { sequence });
        }

        /// <summary>
        /// ** 功能描述:	查询Resource2MO的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-3-8 9:58:08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <returns> Resource2MO的总记录数</returns>
        public int QueryResource2MOCount(string resourceCode, int dateFrom, int dateTo)
        {
            string strSql = string.Format("select count(*) from TBLRES2MO where 1=1 and RESCODE like '{0}%' ", resourceCode);
            if (dateFrom == 0)
                dateFrom = 0;
            if (dateTo == 0)
                dateTo = int.MaxValue;
            strSql += " and (";
            strSql += " not ( STARTDATE>" + dateTo.ToString() + " or ENDDATE<" + dateFrom.ToString() + ") ";
            strSql += " ) ";
            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Resource2MO
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-3-8 9:58:08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Resource2MO数组</returns>
        public object[] QueryResource2MO(string resourceCode, int dateFrom, int dateTo, int inclusive, int exclusive)
        {
            string strSql = string.Format("select {0} from TBLRES2MO where 1=1 and RESCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource2MO)), resourceCode);
            if (dateFrom == 0)
                dateFrom = 0;
            if (dateTo == 0)
                dateTo = int.MaxValue;
            strSql += " and (";
            strSql += " not ( STARTDATE>" + dateTo.ToString() + " or ENDDATE<" + dateFrom.ToString() + ") ";
            strSql += " ) ";
            return this.DataProvider.CustomQuery(typeof(Resource2MO), new PagerCondition(strSql, "SEQ DESC", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Resource2MO
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-3-8 9:58:08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Resource2MO的总记录数</returns>
        public object[] GetAllResource2MO()
        {
            return this.DataProvider.CustomQuery(typeof(Resource2MO), new SQLCondition(string.Format("select {0} from TBLRES2MO order by SEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource2MO)))));
        }

        public object[] QueryResource2MOByResourceDate(string resourceCode, int dateFrom, int timeFrom, int dateTo, int timeTo)
        {
            string strSql = string.Format("select {0} from TBLRES2MO where 1=1 and RESCODE = '{1}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource2MO)), resourceCode);
            if (dateFrom == 0)
                dateFrom = 0;
            if (dateFrom == 0)
                timeFrom = 0;
            if (timeTo == 0)
                timeTo = 235959;
            if (dateTo == 0)
            {
                dateTo = FormatHelper.TODateInt(DateTime.MaxValue);
                timeTo = 0;
            }
            string strDateFrom = dateFrom.ToString() + timeFrom.ToString().PadLeft(6, '0');
            string strDateTo = dateTo.ToString() + timeTo.ToString().PadLeft(6, '0');
            strSql += " and (";
            strSql += " not ( STARTDATE * 1000000 + STARTTIME>" + strDateTo.ToString() + " or ENDDATE * 1000000 + ENDTIME<" + strDateFrom.ToString() + ") ";
            strSql += " ) ";
            return this.DataProvider.CustomQuery(typeof(Resource2MO), new SQLCondition(strSql));
        }


        #endregion

        #region RMARCARD
        /// <summary>
        /// 
        /// </summary>
        public RMARCARD CreateNewRMARCARD()
        {
            return new RMARCARD();
        }

        public void AddRMARCARD(RMARCARD rMARCARD)
        {
            this._helper.AddDomainObject(rMARCARD);
        }

        public void UpdateRMARCARD(RMARCARD rMARCARD)
        {
            this._helper.UpdateDomainObject(rMARCARD);
        }

        public void DeleteRMARCARD(RMARCARD rMARCARD)
        {
            this._helper.DeleteDomainObject(rMARCARD);
        }

        public void DeleteRMARCARD(RMARCARD[] rMARCARD)
        {
            this._helper.DeleteDomainObject(rMARCARD);
        }

        //		public object GetRMARCARD( string rMABILLNO )
        //		{
        //			return this.DataProvider.CustomSearch(typeof(RMARCARD), new object[]{ rMABILLNO });
        //		}

        public void DeleteRMARCARDByRcard(string rcard)
        {
            DataProvider.CustomExecute(new SQLCondition(String.Format("delete from tblrmarcard where rcard = '{1}'", rcard)));

        }

        public object GetRMARCARDByRcard(string rcard)
        {
            object[] objs = DataProvider.CustomQuery(typeof(RMARCARD)
                , new SQLCondition(String.Format("select {0} from tblrmarcard where rcard = '{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMARCARD)), rcard)));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        public object GetRMARCARDByMoCode(string mocode)
        {
            object[] objs = DataProvider.CustomQuery(typeof(RMARCARD)
                , new SQLCondition(String.Format("select {0} from tblrmarcard where reworkmocode = '{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMARCARD)), mocode)));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        public object GetRepairRMARCARDByRcard(string rcard)
        {
            object[] objs = DataProvider.CustomQuery(typeof(RMARCARD)
                , new SQLCondition(String.Format("select {0} from tblrmarcard where rcard = '{1}' and RMATYPE='" + RMAHandleWay.TSCenter + "'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMARCARD)), rcard)));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ** 功能描述:	查询RMARCARD的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-6 10:27:03
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="rMABILLNO">RMABILLNO，模糊查询</param>
        /// <returns> RMARCARD的总记录数</returns>
        public int QueryRMARCARDCount(string rMABILLNO)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRMARCARD where 1=1 and RMABILLNO like '{0}%' ", rMABILLNO)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询RMARCARD
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-6 10:27:03
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="rMABILLNO">RMABILLNO，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> RMARCARD数组</returns>
        public object[] QueryRMARCARD(string rMABILLNO, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RMARCARD), new PagerCondition(string.Format("select {0} from TBLRMARCARD where 1=1 and RMABILLNO like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMARCARD)), rMABILLNO), "RMABILLNO", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的RMARCARD
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-7-6 10:27:03
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>RMARCARD的总记录数</returns>
        public object[] GetAllRMARCARD()
        {
            return this.DataProvider.CustomQuery(typeof(RMARCARD), new SQLCondition(string.Format("select {0} from TBLRMARCARD order by RMABILLNO", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMARCARD)))));
        }


        #endregion

        public object GetOPBOM(string itemCode, string obCode, string opBomVer, int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(OPBOM), new object[] { obCode, itemCode, opBomVer, orgID });
        }

        public void UpdateOPBOM(OPBOM opBOM)
        {
            this.DataProvider.Update(opBOM);
        }
        /// <summary>
        /// Added By Jessie Lee for P4.4,2005/8/30
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool CheckItemCodeUsed(string itemCode)
        {
            string sql = " select count(mocode) from tblmo where itemcode = $ItemCode and mostatus in ('" + MOManufactureStatus.MOSTATUS_OPEN + "','" + MOManufactureStatus.MOSTATUS_RELEASE + "') ";
            int useMOCount = this.DataProvider.GetCount(
                new SQLParamCondition(sql, new SQLParameter[] { new SQLParameter("ItemCode", typeof(string), itemCode) }));
            if (useMOCount > 0) return true;
            else return false;
        }

        #region ERPBOM
        /// <summary>
        /// 
        /// </summary>
        public ERPBOM CreateNewERPBOM()
        {
            return new ERPBOM();
        }

        public void AddERPBOM(ERPBOM eRPBOM)
        {
            this._helper.AddDomainObject(eRPBOM);
        }

        public void UpdateERPBOM(ERPBOM eRPBOM)
        {
            this._helper.UpdateDomainObject(eRPBOM);
        }

        public void DeleteERPBOM(ERPBOM eRPBOM)
        {
            this._helper.DeleteDomainObject(eRPBOM);
        }

        public void DeleteERPBOM(ERPBOM[] eRPBOM)
        {
            this._helper.DeleteDomainObject(eRPBOM);
        }

        public object GetERPBOM(decimal sEQUENCE)
        {
            return this.DataProvider.CustomSearch(typeof(ERPBOM), new object[] { sEQUENCE });
        }

        /// <summary>
        /// ** 功能描述:	查询ERPBOM的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-8-31 14:44:35
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sEQUENCE">SEQUENCE，模糊查询</param>
        /// <returns> ERPBOM的总记录数</returns>
        public int QueryERPBOMCount(decimal sEQUENCE)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLERPBOM where 1=1 and SEQUENCE like '{0}%' ", sEQUENCE)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ERPBOM
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-8-31 14:44:35
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sEQUENCE">SEQUENCE，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> ERPBOM数组</returns>
        public object[] QueryERPBOM(decimal sEQUENCE, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ERPBOM), new PagerCondition(string.Format("select {0} from TBLERPBOM where 1=1 and SEQUENCE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPBOM)), sEQUENCE), "SEQUENCE", inclusive, exclusive));
        }
        /// <summary>
        /// Get ERPBom by MoCode
        /// </summary>
        /// <param name="mocode">mo code</param>
        /// <returns></returns>
        public object[] QueryERPBOM(string mocode)
        {
            string sql = "select {0} from tblerpbom where mocode='{1}'";

            return this.DataProvider.CustomQuery(typeof(ERPBOM)
                , new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPBOM)), mocode)));
        }

        /// <summary>
        /// Get ERPBom by MoCode
        /// </summary>
        /// <param name="mocode">mo code</param>
        /// <returns></returns>
        public object[] QueryERPBOM(string mocode, string lotno)
        {
            string sql = "select {0} from tblerpbom where mocode='{1}' and lotno='{2}'";

            return this.DataProvider.CustomQuery(typeof(ERPBOM)
                , new SQLCondition(
                string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPBOM))
                , mocode
                , lotno)));
        }

        /// <summary>
        /// Get ERPBom by MoCode
        /// </summary>
        /// <param name="mocode">mo code</param>
        /// <returns></returns>
        public bool CheckERPBOM(string opID, string mocode)
        {
            bool isExist = true;
            string sqlERPBOM = "select {0} from tblerpbom where mocode='{1}'";
            //string sqlERPBOMQty = "select sum(BQTY) as qty from tblerpbom where mocode='{0}' group by BITEMCODE";
            object mo = this.GetMO(mocode);

            string sqlOPBOM = "select {0} from tblopbomdetail where opid='{1}' and orgid=" + ((MO)mo).OrganizationID;

            //int iERPCount = 0,iOPBOMCount = 0;
            object[] objERPBOMs = this.DataProvider.CustomQuery(typeof(ERPBOM),
                new SQLCondition(string.Format(sqlERPBOM
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPBOM))
                , mocode)));

            object[] objOPBOMs = this.DataProvider.CustomQuery(typeof(OPBOMDetail),
                new SQLCondition(string.Format(sqlOPBOM
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(OPBOMDetail))
                , opID)));


            if (objERPBOMs != null && objERPBOMs.Length > 0)
            {
                //				iERPCount = objERPBOMs.Length ;

                //bool isInclude = true;

                Hashtable htOPBOMCount = new Hashtable();//首选料
                Hashtable htOPBOMCountA = new Hashtable();//子阶料号
                Hashtable htERPBOMCount = new Hashtable();
                if (objOPBOMs != null && objOPBOMs.Length > 0)
                {

                    foreach (OPBOMDetail opDetail in objOPBOMs)
                    {
                        if (htOPBOMCountA.ContainsKey(opDetail.OPBOMItemCode))
                        {
                            htOPBOMCountA[opDetail.OPBOMItemCode] = Convert.ToInt32(htOPBOMCountA[opDetail.OPBOMItemCode]) + 1;
                        }
                        else
                        {
                            htOPBOMCountA.Add(opDetail.OPBOMItemCode, 1);
                        }

                        if (htOPBOMCount.ContainsKey(opDetail.OPBOMSourceItemCode))
                        {
                            htOPBOMCount[opDetail.OPBOMSourceItemCode] = Convert.ToInt32(htOPBOMCount[opDetail.OPBOMSourceItemCode]) + 1;
                        }
                        else
                        {
                            htOPBOMCount.Add(opDetail.OPBOMSourceItemCode, 1);
                        }
                    }

                    foreach (ERPBOM erp in objERPBOMs)
                    {
                        if (htERPBOMCount.ContainsKey(erp.BITEMCODE))
                        {
                            htERPBOMCount[erp.BITEMCODE] = Convert.ToInt32(htERPBOMCount[erp.BITEMCODE]) + 1;
                        }
                        else
                        {
                            htERPBOMCount.Add(erp.BITEMCODE, 1);
                        }
                    }
                }
                //工单发料资料中包含5种物料，则工序BOM中的子阶物料必须也有这五种物料，
                if (htERPBOMCount.Keys.Count <= htOPBOMCountA.Keys.Count && htERPBOMCount.Keys.Count != 0)
                {
                    //且首选料不能有这五种物料之外的其他物料
                    foreach (string key in htOPBOMCount.Keys)
                    {
                        if (!htERPBOMCount.ContainsKey(key))
                        {
                            isExist = false;
                            break;
                        }
                    }
                    foreach (string key in htERPBOMCount.Keys)
                    {
                        if (!htOPBOMCountA.ContainsKey(key))
                        {
                            isExist = false;
                            break;
                        }
                    }
                }
                else
                {
                    isExist = false;
                }

                int status = 0;
                if (isExist)
                {
                    status = 1;
                }

                if (objOPBOMs != null && objOPBOMs.Length > 0)
                {
                    OPBOMDetail opBOMDetail = objOPBOMs[0] as OPBOMDetail;

                    object objOPBOM = this.GetOPBOM(opBOMDetail.ItemCode, opBOMDetail.OPBOMCode, opBOMDetail.OPBOMVersion, opBOMDetail.OrganizationID);
                    if (objOPBOM != null)
                    {
                        OPBOM opBOM = objOPBOM as OPBOM;

                        opBOM.Avialable = status;

                        DataProvider.BeginTransaction();
                        try
                        {
                            UpdateOPBOM(opBOM);

                            DataProvider.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            DataProvider.RollbackTransaction();
                        }
                        finally
                        {
                            ((SQLDomainDataProvider)DataProvider).PersistBroker.CloseConnection();
                        }
                    }

                }
            }

            return isExist;
        }

        /// <summary>
        /// ** 功能描述:	获得所有的ERPBOM
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-8-31 14:44:35
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>ERPBOM的总记录数</returns>
        public object[] GetAllERPBOM()
        {
            return this.DataProvider.CustomQuery(typeof(ERPBOM), new SQLCondition(string.Format("select {0} from TBLERPBOM order by SEQUENCE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ERPBOM)))));
        }


        #endregion

        //Remark by HI1/Venus.Feng on 20080625 for Hisense Version : Never to use this code
        //melo zheng,2007.1.8,获取ItemRoute2Operation的总记录数
        public int GetItemRoute2OperationCount(string itemCode, string routeCode, string opCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblitemroute2op where itemcode='" + itemCode + "' and routecode='" + routeCode + "' and opcode ='" + opCode + "'")));

        }
        // End 

        // Added By Hi1/Venus.Feng on 20081120 for Hisense Version : For MO Tail
        public object[] GetMOTailList(string moCode, int inclusive, int exclusive)
        {
            string strSql = "";
            strSql += "SELECT   {0}";
            strSql += "    FROM tblsimulationreport";
            strSql += "   WHERE mocode = '" + moCode + "' AND iscom = '0' ";
            strSql += "ORDER BY rcard";

            return this.DataProvider.CustomQuery(typeof(SimulationReport),
                new PagerCondition(string.Format(strSql,
                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport))), "rcard", inclusive, exclusive));
        }

        public int GetMOTailListCount(string moCode)
        {
            string strSql = "";
            strSql += "SELECT   COUNT(*)";
            strSql += "    FROM tblsimulationreport";
            strSql += "   WHERE mocode = '" + moCode + "' AND iscom = '0' ";
            strSql += "ORDER BY rcard";

            return this.DataProvider.GetCount(new SQLCondition(strSql));
        }

        public void DoSplit(SimulationReport simulationReport, DBDateTime currentDateTime, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            // Step1 : Update Simulation
            Simulation sim = (Simulation)this.GetSimulation(simulationReport.MOCode, simulationReport.RunningCard, simulationReport.RunningCardSequence.ToString());
            if (sim != null)
            {
                sim.IsComplete = "1";
                sim.EAttribute1 = ProductStatus.OffMo;
                sim.ProductStatus = ProductStatus.OffMo;
                sim.MaintainUser = userCode;
                sim.MaintainDate = dbDateTime.DBDate;
                sim.MaintainTime = dbDateTime.DBTime;

                this.UpdateSimulation(sim);
            }

            // Step2 : Update SimulationReport
            simulationReport.EAttribute1 = ProductStatus.OffMo;
            simulationReport.Status = ProductStatus.OffMo;
            simulationReport.IsComplete = "1";
            simulationReport.MaintainUser = userCode;
            simulationReport.MaintainDate = dbDateTime.DBDate;
            simulationReport.MaintainTime = dbDateTime.DBTime;

            this.UpdateSimulationReport(simulationReport);

            // Step3 : Update TS
            object objTS = this.GetCardLastTSRecordInTS(simulationReport.RunningCard);
            if (objTS != null)
            {
                BenQGuru.eMES.Domain.TS.TS ts = objTS as BenQGuru.eMES.Domain.TS.TS;
                ts.TSStatus = TSStatus.TSStatus_OffMo;
                ts.MaintainDate = currentDateTime.DBDate;
                ts.MaintainTime = currentDateTime.DBTime;
                ts.MaintainUser = userCode;

                this.UpdateTS(ts);
            }

            // Step4 : Drop Material
            OnWIPItem[] onwipitems = (new MaterialFacade(this.DataProvider)).QueryLoadedPartByRCard(simulationReport.RunningCard, simulationReport.MOCode);
            if (onwipitems != null)
            {
                CastDownHelper castDownHelper = new CastDownHelper(DataProvider);
                string sql;
                foreach (OnWIPItem wipItem in onwipitems)
                {
                    //获取arRcard
                    ArrayList arRcard = new ArrayList();
                    castDownHelper.GetAllRCard(ref arRcard, simulationReport.RunningCard);
                    if (arRcard.Count == 0)
                    {
                        arRcard.Add(simulationReport.RunningCard);
                    }

                    string runningCards = "('" + String.Join("','", (string[])arRcard.ToArray(typeof(string))) + "')";
                    sql = string.Format("update TBLONWIPITEM set TRANSSTATUS='{0}',ActionType=" + (int)MaterialType.DropMaterial +
                        ",DropOP = ''" +
                        ",DropUser='" + userCode + "'" +
                        ",DropDate=" + currentDateTime.DBDate +
                        ",DropTime=" + currentDateTime.DBTime +
                        " where RCARD in {1} and ActionType='{2}'" +
                        " and MCARD in ('" + wipItem.MCARD.Trim().ToUpper() + "')"
                        , TransactionStatus.TransactionStatus_YES
                        , runningCards
                        , (int)Web.Helper.MaterialType.CollectMaterial);
                    this.DataProvider.CustomExecute(new SQLCondition(sql));

                    if (wipItem.MCardType == MCardType.MCardType_Keyparts)
                    {
                        sql = "update TBLSIMULATIONREPORT set IsLoadedPart='0',LoadedRCard='' " +
                            " where RCARD in ('" + wipItem.MCARD.Trim().ToUpper() + "')";
                        this.DataProvider.CustomExecute(new SQLCondition(sql));
                    }
                }
            }

            MO mo = this.GetMO(simulationReport.MOCode) as MO;
            OffMoCard offCard = new OffMoCard();
            offCard.PK = System.Guid.NewGuid().ToString();
            offCard.MoCode = simulationReport.MOCode;
            offCard.RCARD = simulationReport.RunningCard;
            offCard.MoType = mo.MOType;
            offCard.MUSER = userCode;
            offCard.MDATE = currentDateTime.DBDate;
            offCard.MTIME = currentDateTime.DBTime;
            this.AddOffMoCard(offCard);

            // Step5 : Update tblmo.mooffqty
            this.UpdateMOQty(simulationReport.MOCode, "OFFMO", Convert.ToInt32(1 * simulationReport.IDMergeRule));
        }

        public void DoScrap(SimulationReport simulationReport, DBDateTime currentDateTime, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            // Step1 : Update Simulation
            Simulation sim = (Simulation)this.GetSimulation(simulationReport.MOCode, simulationReport.RunningCard, simulationReport.RunningCardSequence.ToString());
            if (sim != null)
            {
                sim.IsComplete = "1";
                sim.EAttribute1 = "SCRAP";
                sim.ProductStatus = "SCRAP";
                sim.MaintainUser = userCode;
                sim.MaintainDate = dbDateTime.DBDate;
                sim.MaintainTime = dbDateTime.DBTime;

                this.UpdateSimulation(sim);
            }

            // Step2 : Update SimulationReport
            simulationReport.EAttribute1 = "SCRAP";
            simulationReport.IsComplete = "1";
            simulationReport.Status = "SCRAP";
            simulationReport.MaintainUser = userCode;
            simulationReport.MaintainDate = dbDateTime.DBDate;
            simulationReport.MaintainTime = dbDateTime.DBTime;

            this.UpdateSimulationReport(simulationReport);

            // Step3 : Update TS
            object objTS = this.GetCardLastTSRecordInTS(simulationReport.RunningCard);
            if (objTS != null)
            {
                BenQGuru.eMES.Domain.TS.TS ts = objTS as BenQGuru.eMES.Domain.TS.TS;
                ts.TSStatus = TSStatus.TSStatus_Scrap;
                ts.MaintainDate = currentDateTime.DBDate;
                ts.MaintainTime = currentDateTime.DBTime;
                ts.MaintainUser = userCode;

                this.UpdateTS(ts);
            }

            // Step4 : Update tblmo.moscrapqty+1
            MO mo = this.GetMO(simulationReport.MOCode) as MO;
            if (mo != null)
            {
                mo.MOScrapQty = 1 * simulationReport.IDMergeRule;

                this.UpdateMOScrapQty(mo);
            }
        }
        public void UpdateTS(BenQGuru.eMES.Domain.TS.TS tS)
        {
            this.DataProvider.Update(tS);
        }
        public object GetCardLastTSRecordInTS(string rcard)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TS.TS),
                new SQLParamCondition(String.Format(
                @"SELECT {0}  FROM tblts WHERE tsid = (select tsid
									from (SELECT tsid
											FROM tblts
											WHERE rcard = $RCARD
											order by MDATE * 1000000 + MTIME DESC)
									where rownum = 1) and tsstatus in ('" + TSStatus.TSStatus_New + "','" + TSStatus.TSStatus_Confirm + "','" + TSStatus.TSStatus_TS + "','" + TSStatus.TSStatus_Reflow + "') ",//先前只有更新维修数据，现在添加回流的
                                                    DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.TS.TS))),
                                        new SQLParameter[] { 
															   new SQLParameter("RCARD",typeof(string),rcard.Trim())}));
            if (objs != null)
            {
                return objs[0];
            }
            else
            {
                return null;
            }
        }

        #region Simulation
        public Simulation CreateNewSimulation()
        {
            return new Simulation();
        }

        public void AddSimulation(Simulation simulation)
        {
            this.DataProvider.Insert(simulation);
        }

        public void UpdateSimulation(Simulation simulation)
        {
            this.DataProvider.Update(simulation);
        }

        public void DeleteSimulation(Simulation simulation)
        {
            this.DataProvider.Delete(simulation);
        }

        public object GetSimulation(string moCode, string runningCard, string runningCardSeq)
        {
            //return this.DataProvider.CustomSearch(typeof(Simulation), new object[]{ runningCard });
            object[] simulations = this.DataProvider.CustomQuery(typeof(Simulation),
                new SQLParamCondition(string.Format("select {0} from TBLSIMULATION where RCARD = $RCARD and MOCODE = $MOCODE and RCARDSEQ = $RCARDSEQ order by MDATE desc,MTIME desc",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Simulation))),
                new SQLParameter[]{ new SQLParameter("RCARD", typeof(string), runningCard.ToUpper())
								  ,new SQLParameter("MOCODE", typeof(string), moCode.ToUpper())
								  ,new SQLParameter("RCARDSEQ", typeof(string), runningCardSeq.ToUpper())}));

            if (simulations == null)
                return null;
            if (simulations.Length > 0)
                return simulations[0];
            else
                return null;

        }

        #endregion

        public SimulationReport CreateNewSimulationReport()
        {
            return new SimulationReport();
        }

        public void AddSimulationReport(SimulationReport simulationReport)
        {
            this.DataProvider.Insert(simulationReport);
        }

        public void UpdateSimulationReport(SimulationReport simulationReport)
        {
            this.DataProvider.Update(simulationReport);
        }

        public void DeleteSimulationReport(Simulation simulation)
        {
            this.DataProvider.CustomExecute(new SQLParamCondition("delete from  TBLSIMULATIONREPORT where RCARD=$RCARD and MOCODE=$MOCODE",
                new SQLParameter[] {
									   new SQLParameter("RCARD", typeof(string), simulation.RunningCard),
									   new SQLParameter("MOCODE", typeof(string), simulation.MOCode)
								   }));
        }

        public void DeleteSimulationReport(SimulationReport simulationReport)
        {
            this.DataProvider.Delete(simulationReport);
        }
        // End Added

          //add by leo 20120920
        public object GetMaxRcardSameLotNo(string rcardPrefix)
        {
            string sql = string.Format(@"SELECT MAX(rcard) AS rcard FROM TBLMO2RCARDLINK WHERE rcard LIKE '{0}%'", FormatHelper.CleanString(rcardPrefix));
            object[] objs = this.DataProvider.CustomQuery(typeof(MO2RCARDLINK), new SQLCondition(sql));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }
      
        //add  by Leo @2011-11-22 for Query  M02RcardLink
        public object[] GetMO2RCardLinkMOItemForQuery(string rcard, string moCode)
        {
            string sql = string.Format(@"  SELECT   TBLMO2RCARDLINK.mocode,TBLMO2RCARDLINK.Rcard, TBLMO2RCARDLINK.Printtimes,TBLMO2RCARDLINK.Lastprintuser,
                                           TBLMO2RCARDLINK.Lastprintdate,TBLMO2RCARDLINK.Lastprinttime,TBLMO2RCARDLINK.Muser,TBLMO2RCARDLINK.Mdate,TBLMO2RCARDLINK.Mtime,
                                           tblmo.Lotno,Tblmo.Moplanqty,Tblitem.Itemcode,Tblitem.Itemdesc
                                           FROM  TBLMO2RCARDLINK,Tblmo,Tblitem  WHERE 1=1  
                                           AND  Tblitem.Itemcode=Tblmo.Itemcode
                                           AND  tblMO2RCARDLINK.Mocode=Tblmo.Mocode");
            if (moCode.Length != 0 && moCode != string.Empty)
            {
                sql += string.Format(@" AND  TBLMO2RCARDLINK.Mocode='{0}'", moCode);
            }
            if (rcard.Length != 0 && rcard != string.Empty)
            {
                sql += string.Format(@"AND  TBLMO2RCARDLINK.Rcard='{0}'", rcard);
            }

            sql += string.Format("ORDER BY  RCARD");

            return this.DataProvider.CustomQuery(typeof(MO2RCARDLINKForQuery), new SQLCondition(sql));

        }

    }

    [Serializable]
    public class BItemQty : DomainObject
    {
        [FieldMapAttribute("QTY", typeof(int), true)]
        public int QTY;
    }

}