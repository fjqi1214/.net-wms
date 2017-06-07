using System;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SMT;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.BaseSetting
{
    public enum OperationList
    {
        ComponentLoading,
        Testing,
        IDTranslation,
        Packing,
        OQC,
        TS,
        OutsideRoute,
        SMT,
        SPC,
        DeductBOMItem,
        MidistOutput,
        MidistInput,
        ComponentDown,
        BurnIn,
        BurnOut
    }

    /// <summary>
    /// BaseModel 的摘要说明。
    /// 文件名:		BaseModel.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		Jane Shu
    /// 创建日期:	2005/03/08
    /// 修改人:		Jane Shu
    /// 修改日期:	2005-04-05  
    ///					主键大写，去掉upper
    /// 描 述:		基本模型维护后台
    /// 版 本:	
    /// </summary>
    public class BaseModelFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;
        private const string TS_Operation = "TS";

        public BaseModelFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        //Laws Lu,max life time to unlimited
        public override object InitializeLifetimeService()
        {
            return null;
        }

        public BaseModelFacade()
        {
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

        #region Segment
        public Segment CreateNewSegment()
        {
            return new Segment();
        }

        public void AddSegment(Segment segment)
        {
            this._helper.AddDomainObject(segment);
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="segment"></param>
        public void UpdateSegment(Segment segment)
        {
            object[] resource = this.QueryResourceBySegmentCode(segment.SegmentCode);

            this.DataProvider.BeginTransaction();
            try
            {
                this._helper.UpdateDomainObject(segment);
                if (resource != null)
                {
                    foreach (object _resource in resource)
                    {
                        ((Resource)_resource).ShiftTypeCode = segment.ShiftTypeCode;
                        this.UpdateResource((Resource)_resource);
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Update_Domain_Object", ex);
            }
        }



        public void DeleteSegment(Segment segment)
        {
            this._helper.DeleteDomainObject(segment, new ICheck[]{ new DeleteAssociateCheck( segment, 
																	  this.DataProvider,
																	  new Type[]{typeof(StepSequence)}) });
        }

        public void DeleteSegment(Segment[] segments)
        {
            this._helper.DeleteDomainObject(segments, new ICheck[]{ new DeleteAssociateCheck( segments, 
																	   this.DataProvider,
																	   new Type[]{typeof(StepSequence)}) });
        }

        /// <summary>
        /// ** 功能描述:	由SegmentCode获得Segment
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2004/03/08
        /// ** 修 改:
        /// ** 日 期:
        /// ** 版本
        /// </summary>
        /// <param name="segmentCode"></param>
        /// <returns></returns>
        public object GetSegment(string segmentCode)
        {
            return this.DataProvider.CustomSearch(typeof(Segment), new object[] { segmentCode });
        }

        /// ** 功能描述:	查询Segment的总行数
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <returns> Segment的总记录数</returns>
        public int QuerySegmentCount(string segmentCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSEG where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and SEGCODE like '{0}%' ", segmentCode)));
        }

        /// ** 功能描述:	分页查询Segment
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns></returns>
        public object[] QuerySegment(string segmentCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Segment), new PagerCondition(string.Format("select {0} from TBLSEG where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and SEGCODE like '{1}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Segment)), segmentCode), "SEGSEQ,SEGCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Segment
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Segment的总记录数</returns>
        public object[] GetAllSegment()
        {
            return this.DataProvider.CustomQuery(typeof(Segment), new SQLCondition(string.Format("select {0} from TBLSEG where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by SEGCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Segment)))));
        }

        #endregion

        #region StepSequence

        public StepSequence CreateNewStepSequence()
        {
            return new StepSequence();
        }

        public void AddStepSequence(StepSequence stepSequence)
        {
            this._helper.AddDomainObject(stepSequence);
        }

        public void UpdateStepSequence(StepSequence stepSequence)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                this._helper.UpdateDomainObject(stepSequence);

                object[] resource = this.GetResourceByStepSequenceCode(((StepSequence)stepSequence).StepSequenceCode);

                if (resource != null)
                {
                    foreach (object _resource in resource)
                    {
                        ((Resource)_resource).SegmentCode = stepSequence.SegmentCode;
                        ((Resource)_resource).ShiftTypeCode = stepSequence.ShiftTypeCode;
                        this.UpdateResource((Resource)_resource);
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Update_Domain_Object", ex);
            }
        }

        public void DeleteStepSequence(StepSequence stepSequence)
        {
            this._helper.DeleteDomainObject(stepSequence, new ICheck[]{ new DeleteAssociateCheck( stepSequence, 
																		   this.DataProvider,
																		   new Type[]{typeof(Resource)}) });
        }

        public void DeleteStepSequence(StepSequence[] stepSequences)
        {
            this._helper.DeleteDomainObject(stepSequences, new ICheck[]{ new DeleteAssociateCheck( stepSequences, 
																			this.DataProvider,
																			new Type[]{typeof(Resource)}) });
        }

        public object GetStepSequence(string stepSequenceCode)
        {
            return this.DataProvider.CustomSearch(typeof(StepSequence), new object[] { stepSequenceCode });
        }


        public object GetStepSequence(string bigStepSequenceCode, int ssseq)
        {
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format("select {0} from TBLSS where ssseq='{1}' and bigsscode='{2}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)), ssseq, bigStepSequenceCode)));
        }

        public object[] QueryStepSequenceByBigSSCode(string bigStepSequenceCode)
        {
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format("select {0} from TBLSS where bigsscode='{1}' " + GlobalVariables.CurrentOrganizations.GetSQLCondition(), DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)),  bigStepSequenceCode.Trim().ToUpper())));
        }
        /// <summary>
        /// ** 功能描述:	查询StepSequence的总行数
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <returns>StepSequence的总记录数</returns>
        public int QueryStepSequenceCount(string stepSequenceCode, string segmentCode, string BigStepSequenceCode)
        {
            string condition = "";

            if (stepSequenceCode != null && stepSequenceCode.Length != 0)
            {
                condition = string.Format("{0} and SSCODE like '{1}%'", condition, stepSequenceCode);
            }

            if (segmentCode != null && segmentCode.Length != 0)
            {
                if (segmentCode.IndexOf(",") >= 0)
                {
                    condition = string.Format("{0} and SEGCODE IN ({1})", condition, FormatHelper.ProcessQueryValues(segmentCode));
                }
                else
                {
                    condition = string.Format("{0} and SEGCODE like '{1}%'", condition, segmentCode);
                }
            }

            if (BigStepSequenceCode != null && BigStepSequenceCode.Length != 0)
            {
                condition = string.Format("{0} and upper(BIGSSCODE) like '{1}%'", condition, BigStepSequenceCode);
            }


            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSS where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {0}", condition)));
        }

        public object[] QueryStepSequence(string stepSequenceCode, string segmentCode, int inclusive, int exclusive)
        {
            return this.QueryStepSequence(stepSequenceCode, segmentCode, null, inclusive, exclusive);
        }
        /// <summary>
        /// ** 功能描述:	分页查询StepSequence
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="stepSequenceCode">StepSequenceCode，模糊查询</param>
        /// <param name="segmentCode">SegmentCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns></returns>
        public object[] QueryStepSequence(string stepSequenceCode, string segmentCode, string BigStepSequenceCode, int inclusive, int exclusive)
        {
            string condition = "";

            if (stepSequenceCode != null && stepSequenceCode.Length != 0)
            {
                condition = string.Format("{0} and SSCODE like '{1}%'", condition, stepSequenceCode);
            }

            if (segmentCode != null && segmentCode.Length != 0)
            {
                if (segmentCode.IndexOf(",") >= 0)
                {
                    condition = string.Format("{0} and SEGCODE IN ({1})", condition, FormatHelper.ProcessQueryValues(segmentCode));
                }
                else
                {
                    condition = string.Format("{0} and SEGCODE like '{1}%'", condition, segmentCode);
                }
            }

            if (BigStepSequenceCode != null && BigStepSequenceCode.Length != 0)
            {
                condition = string.Format("{0} and upper(BIGSSCODE) like '{1}%'", condition, BigStepSequenceCode);
            }

            //			return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format( "select * from TBLSS where 1=1 {0} order by SSSEQ", condition )));
            return this.DataProvider.CustomQuery(typeof(StepSequence), new PagerCondition(string.Format("select {0} from TBLSS where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " {1} order by sscode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)), condition), "SEGCODE, SSSEQ,BIGSSCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的StepSequence
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>所有的StepSequence</returns>
        public object[] GetAllStepSequence()
        {
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format("select {0} from TBLSS where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " order by SSCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)))));
        }



        /// <summary>
        /// ** 功能描述:	获得Segment下所有的StepSequence
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-05-08
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Segment下所有的StepSequence</returns>
        public object[] GetStepSequenceBySegmentCode(string segmentCode)
        {
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format("select {0} from TBLSS where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and SEGCODE='{1}' order by SSCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)), segmentCode)));
        }

        public object[] QueryBigSSCodeFromSystem()
        {
            string sql = string.Format(
               "select {0} from TBLSYSPARAM  where 1=1 and PARAMGROUPCODE='BIGLINEGROUP'  order by PARAMALIAS",
               DomainObjectUtility.GetDomainObjectFieldsString(typeof(Domain.BaseSetting.Parameter)));
            return this.DataProvider.CustomQuery(typeof(Parameter), new SQLCondition(sql));
        }
        #endregion

        #region ResourceReworkLog
        /// <summary>
        /// Resource Rework Log
        /// </summary>
        public ResourceReworkLog CreateNewResourceReworkLog()
        {
            return new ResourceReworkLog();
        }

        public void AddResourceReworkLog(ResourceReworkLog resourceReworkLog)
        {
            this._helper.AddDomainObject(resourceReworkLog);
        }

        public void UpdateResourceReworkLog(ResourceReworkLog resourceReworkLog)
        {
            this._helper.UpdateDomainObject(resourceReworkLog);
        }

        public void DeleteResourceReworkLog(ResourceReworkLog resourceReworkLog)
        {
            this._helper.DeleteDomainObject(resourceReworkLog);
        }

        public void DeleteResourceReworkLog(ResourceReworkLog[] resourceReworkLog)
        {
            this._helper.DeleteDomainObject(resourceReworkLog);
        }

        public object GetResourceReworkLog(decimal sequence, string resourceCode)
        {
            return this.DataProvider.CustomSearch(typeof(ResourceReworkLog), new object[] { sequence, resourceCode });
        }

        /// <summary>
        /// ** 功能描述:	查询ResourceReworkLog的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-9-30 9:25:48
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="resourceCode">ResourceCode，模糊查询</param>
        /// <returns> ResourceReworkLog的总记录数</returns>
        public int QueryResourceReworkLogCount(decimal sequence, string resourceCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRESREWRKLOG where 1=1 and SEQ like '{0}%'  and RESCODE like '{1}%' ", sequence, resourceCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ResourceReworkLog
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-9-30 9:25:48
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="sequence">Sequence，模糊查询</param>
        /// <param name="resourceCode">ResourceCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> ResourceReworkLog数组</returns>
        public object[] QueryResourceReworkLog(decimal sequence, string resourceCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ResourceReworkLog), new PagerCondition(string.Format("select {0} from TBLRESREWRKLOG where 1=1 and SEQ like '{1}%'  and RESCODE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ResourceReworkLog)), sequence, resourceCode), "SEQ,RESCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的ResourceReworkLog
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-9-30 9:25:48
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>ResourceReworkLog的总记录数</returns>
        public object[] GetAllResourceReworkLog()
        {
            return this.DataProvider.CustomQuery(typeof(ResourceReworkLog), new SQLCondition(string.Format("select {0} from TBLRESREWRKLOG order by SEQ,RESCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ResourceReworkLog)))));
        }


        #endregion

        #region Resource

        public Resource CreateNewResource()
        {
            return new Resource();
        }

        public void AddResource(Resource resource)
        {
            CheckResource(resource);
            this._helper.AddDomainObject(resource);
            AddResourceReworkLog(resource, "");
        }

        public void UpdateResource(Resource resource)
        {
            CheckResource(resource);
            Resource res = (Resource)this.GetResource(resource.ResourceCode);
            if (res.ReworkRouteCode != resource.ReworkRouteCode)
            {
                AddResourceReworkLog(resource, res.ReworkRouteCode);
            }
            this._helper.UpdateDomainObject(resource);

        }

        public void UpdateResource(Resource[] resource)
        {
            if (resource != null)
            {
                for (int i = 0; i < resource.Length; i++)
                {
                    Resource newResource = resource[i];
                    CheckResource(newResource);
                    Resource res = (Resource)this.GetResource(newResource.ResourceCode);
                    if (res.ReworkRouteCode != newResource.ReworkRouteCode)
                    {
                        AddResourceReworkLog(newResource, res.ReworkRouteCode);
                    }
                    res.DctCode = newResource.DctCode;
                    this._helper.UpdateDomainObject(res);
                }
            }
        }


        public void UpdateResourceDctCode(Resource[] resources)
        {
            if (resources != null)
            {
                for (int i = 0; i < resources.Length; i++)
                {
                    Resource newResource = resources[i];
                    CheckResource(newResource);
                    Resource res = (Resource)this.GetResource(newResource.ResourceCode);
                    if (res.ReworkRouteCode != newResource.ReworkRouteCode)
                    {
                        AddResourceReworkLog(newResource, res.ReworkRouteCode);
                    }
                    res.DctCode = null;
                    this._helper.UpdateDomainObject(res);
                }
            }
        }

        private void AddResourceReworkLog(Resource resource, string oldReworkRouteCode)
        {
            string strSql = "SELECT MAX(SEQ) SEQ FROM tblResReWrkLog ";
            object[] objs = this.DataProvider.CustomQuery(typeof(ResourceReworkLog), new SQLCondition(strSql));
            int iSeq = 1;
            if (objs != null && objs.Length > 0)
                iSeq = Convert.ToInt32(((ResourceReworkLog)objs[0]).Sequence) + 1;
            ResourceReworkLog log = new ResourceReworkLog();
            log.ResourceCode = resource.ResourceCode;
            log.Sequence = iSeq;
            log.ReworkRouteCode = resource.ReworkRouteCode;
            log.OldReworkRouteCode = oldReworkRouteCode;
            log.MaintainUser = resource.MaintainUser;
            log.MaintainDate = resource.MaintainDate;
            log.MaintainTime = resource.MaintainTime;
            this.AddResourceReworkLog(log);
        }

        //检查资源对应工序是返工途程的第一个工序
        private void CheckResource(Resource resource)
        {
            if (resource.ReworkRouteCode != null && resource.ReworkRouteCode != string.Empty)
            {
                BenQGuru.eMES.Domain.BaseSetting.Operation2Resource op = this.GetOperationByResource(resource.ResourceCode);
                if (op == null)
                {
                    throw new Exception("$Error_Res_not_belong_To_Op");
                }

                object op2 = GetFirstOperationOfRoute(resource.ReworkRouteCode) as BenQGuru.eMES.Domain.BaseSetting.Operation;
                if (op2 == null)
                {
                    throw new Exception("$error_route_no_op");
                }
                if (((Operation)op2).OPCode != op.OPCode)
                {
                    throw new Exception("$error_res_not_first_op");
                }
            }

        }



        public void DeleteResource(Resource resource)
        {
            this._helper.DeleteDomainObject(resource, new ICheck[]{ new DeleteAssociateCheck( resource, 
																	   this.DataProvider,
																	   new Type[]{typeof(Operation2Resource),
																					 typeof(Station) }) });

            DeleteAllResource2ReworkSheet(resource.ResourceCode);
        }



        public void DeleteResource(Resource[] resources)
        {
            this._helper.DeleteDomainObject(resources, new ICheck[]{ new DeleteAssociateCheck( resources, 
																		this.DataProvider,
																		new Type[]{typeof(Operation2Resource),
																					  typeof(Station) }) });

            if (resources != null)
            {
                foreach (Resource res in resources)
                {
                    DeleteAllResource2ReworkSheet(res.ResourceCode);
                }
            }
        }

        public object GetResource(string resourceCode)
        {
            return this.DataProvider.CustomSearch(typeof(Resource), new object[] { resourceCode });
        }

        /// <summary>
        /// ** 功能描述:	查询Resource的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="resourceCode">ResourceCode，模糊查询</param>
        /// <returns> Resource的总记录数</returns>
        public int QueryResourceCount(string resourceCode, string stepSequenceCode, string crewCode)
        {
            string qSql = string.Format("select count(*) from TBLRES where 1=1 and RESCODE like '{0}%' ", resourceCode);
            if (stepSequenceCode != "")
            {
                if (stepSequenceCode.IndexOf(",") >= 0)
                {
                    qSql += string.Format(" and SSCODE IN ({0}) ", FormatHelper.ProcessQueryValues(stepSequenceCode));
                }
                else
                {
                    qSql += string.Format(" and SSCODE like '{0}%' ", stepSequenceCode);
                }
            }
            if (crewCode != "")
            {
                qSql += string.Format(" and CREWCODE like '{0}%' ", crewCode);
            }

            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            qSql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.GetCount(new SQLCondition(qSql));
        }

        public object[] CheckResource(string ssCode)
        {
            string sql = "";
            sql += "select {0} from TBLSS where upper('{1}') like sscode || '%' ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            return this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)), ssCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Resource
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="resourceCode">ResourceCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Resource数组</returns>
        public object[] QueryResource(string resourceCode, string stepSequenceCode, string crewCode, int inclusive, int exclusive)
        {
            string qSql = string.Format("select {0} from TBLRES where 1=1 and RESCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), resourceCode);
            if (stepSequenceCode != "")
            {
                if (stepSequenceCode.IndexOf(",") >= 0)
                {
                    qSql += string.Format(" and SSCODE IN ({0}) ", FormatHelper.ProcessQueryValues(stepSequenceCode));
                }
                else
                {
                    qSql += string.Format(" and SSCODE like '{0}%' ", stepSequenceCode);
                }
            }		//stepSequenceCode在数据库中允许为空,

            if (crewCode != "")
            {
                qSql += string.Format(" and CREWCODE like '{0}%' ", crewCode);
            }

            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            qSql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new PagerCondition(qSql, "RESCODE", inclusive, exclusive));
        }

        public object GetResourceByDctCodeAndRes(string resourceCode, string dctCode)
        {
            string qSql = string.Format("select {0} from TBLRES where 1=1 and RESCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), resourceCode);
            qSql += string.Format(" and dctcode like '{0}%' ", dctCode);

            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            qSql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(qSql))[0];
        }

        public object[] QueryResourceByDctCode(string dctCode, int inclusive, int exclusive)
        {
            string qSql = string.Format("select {0} from TBLRES where 1=1 and dctcdoe = '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), dctCode);

            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            qSql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new PagerCondition(qSql, "dctcode", inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	获得所有的Resource
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Resource的总记录数</returns>
        public object[] GetAllResource()
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql = "select {0} from TBLRES WHERE 1=1 ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += " order by RESCODE ";
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)))));
        }
        /// <summary>
        /// ** 功能描述:	获得产线下的Resource
        /// ** 作 者:		Created by Jarvis
        /// ** 日 期:		2011-08-25 13:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>产线下Resource的总记录数</returns>
        public object[] GetAllResource(string ssCode)
        {
            
            string sql = "";
            sql = "select {0} from TBLRES WHERE 1=1 ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += string.Format(" AND SSCODE in ({0})", FormatHelper.ProcessQueryValues(ssCode));
            sql += " order by RESCODE ";
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)))));
        }
        /// <summary>
        /// ** 功能描述:	根据ShiftTypeCode得到Resource实体
        /// ** 作 者:		Angel Zhu
        /// ** 日 期:		2005-04-27 
        /// ** 修 改:		
        /// ** 日 期:		
        /// </summary>
        /// <param name="segmentCode">SegmentCode</param>
        /// <returns>Resource数组</returns>
        public object[] QueryResourceBySegmentCode(string segmentCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0} from TBLRES where SEGCODE='{1}' ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), segmentCode)));
        }

        // Added By Hi1/Venus.Feng on 20081111 for Hisense Version
        /// <summary>
        /// 批判过的时候,用产生批的那个资源对应的大线的最后一个工序的资源(取一个)
        /// </summary>
        /// <param name="resourceCode">当前的Resource</param>
        /// <param name="itemCode">产品代码</param>
        /// <param name="routeCode">途程代码</param>
        /// <returns>Right Resource</returns>
        public string GetRightResourceForOQCOperate(string resourceCode, string itemCode, string routeCode)
        {
            string strSql = "";
            strSql += "SELECT rescode";
            strSql += "  FROM (SELECT   tblres.rescode";
            strSql += "            FROM tblss, tblres, tblop2res";
            strSql += "           WHERE tblss.sscode = tblres.sscode";
            strSql += "             AND tblres.rescode = tblop2res.rescode";
            strSql += "             AND bigsscode IN (";
            strSql += "                    SELECT tblss.bigsscode";
            strSql += "                      FROM tblss, tblres";
            strSql += "                     WHERE tblss.sscode = tblres.sscode";
            strSql += "                       AND tblres.rescode = '" + resourceCode + "')";
            strSql += "             AND tblop2res.opcode IN (";
            strSql += "                    SELECT opcode";
            strSql += "                      FROM (SELECT   *";
            strSql += "                                FROM tblitem2route, tblitemroute2op";
            strSql += "                               WHERE tblitem2route.routecode =";
            strSql += "                                                     tblitemroute2op.routecode";
            strSql += "                                 AND tblitem2route.itemcode =";
            strSql += "                                                      tblitemroute2op.itemcode";
            strSql += "                                 AND tblitem2route.itemcode = '" + itemCode + "'";
            strSql += "                                 AND tblitemroute2op.routecode = '" + routeCode + "'";
            strSql += "                            ORDER BY opseq DESC)";
            strSql += "                     WHERE ROWNUM = 1)";
            strSql += "        ORDER BY rescode DESC)";
            strSql += " WHERE ROWNUM = 1";

            object[] resList = this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(strSql));
            if (resList == null || resList.Length == 0)
            {
                return "";
            }
            else
            {
                return (resList[0] as Resource).ResourceCode;
            }
        }
        // End Added

        #endregion

        #region Resource2ReworkSheet

        public Resource2ReworkSheet CreateNewResource2ReworkSheet()
        {
            return new Resource2ReworkSheet();
        }

        public void AddResource2ReworkSheet(Resource2ReworkSheet resource2ReworkSheet)
        {
            this._helper.AddDomainObject(resource2ReworkSheet);
        }

        public void DeleteResource2ReworkSheet(Resource2ReworkSheet resource2ReworkSheet)
        {
            this._helper.DeleteDomainObject(resource2ReworkSheet);
        }

        public void UpdateResource2ReworkSheet(Resource2ReworkSheet resource2ReworkSheet)
        {
            this._helper.UpdateDomainObject(resource2ReworkSheet);
        }

        public object GetResource2ReworkSheet(string resourceCode, string reworkCode)
        {
            return this.DataProvider.CustomSearch(typeof(Resource2ReworkSheet), new object[] { resourceCode, reworkCode });
        }

        public void DeleteAllResource2ReworkSheet(string resourceCode)
        {
            string sql = "DELETE FROM tblres2reworksheet WHERE rescode = '{0}' ";
            sql = string.Format(sql, resourceCode.Trim().ToUpper());
            this.DataProvider.CustomExecute(new SQLCondition(sql));
        }

        public object[] QueryResource2ReworkSheet(string resourceCode)
        {
            string sql = "SELECT {0} FROM tblres2reworksheet WHERE rescode = '{1}' ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource2ReworkSheet)), resourceCode.Trim().ToUpper());
            return this.DataProvider.CustomQuery(typeof(Resource2ReworkSheet), new SQLCondition(sql));
        }

        public object[] QueryResource2ReworkSheet(string resourceCode, string itemCode, string lotnNo)
        {
            string sql = string.Empty;
            sql += "SELECT {0} ";
            sql += "FROM tblres2reworksheet ";
            sql += "WHERE rescode = '{1}' ";
            sql += "AND itemcode = '{2}' ";
            if (lotnNo.Trim().Length > 0)
            {
                sql += "AND lotno = '{3}' ";
            }
            else
            {
                sql += "AND lotno IS NULL ";
            }

            sql = string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource2ReworkSheet)),
                resourceCode, itemCode,
                lotnNo);

            return this.DataProvider.CustomQuery(typeof(Resource2ReworkSheet), new SQLCondition(sql));
        }

        #endregion

        #region Operation
        public Operation CreateNewOperation()
        {
            return new Operation();
        }

        public void AddOperation(Operation operation)
        {
            this._helper.AddDomainObject(operation);
        }

        public void UpdateOperation(Operation operation)
        {
            this._helper.UpdateDomainObject(operation);
        }

        public void DeleteOperation(Operation operation)
        {
            this._helper.DeleteDomainObject(operation, new ICheck[]{ new DeleteAssociateCheck( operation, 
																		this.DataProvider,
																		new Type[]{ typeof(Operation2Resource),
																					  typeof(Route2Operation) }) });
        }

        public void DeleteOperation(Operation[] operations)
        {
            this._helper.DeleteDomainObject(operations, new ICheck[]{ new DeleteAssociateCheck( operations, 
																		 this.DataProvider,
																		 new Type[]{ typeof(Operation2Resource),
																					   typeof(Route2Operation) }) });
        }

        public object GetOperation(string opCode)
        {
            return this.DataProvider.CustomSearch(typeof(Operation), new object[] { opCode });
        }

        /// <summary>
        /// ** 功能描述:	查询Operation的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode，模糊查询</param>
        /// <returns> Operation的总记录数</returns>
        public int QueryOperationCount(string oPCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOP where 1=1 and OPCODE like '{0}%'", oPCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Operation
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Operation数组</returns>
        public object[] QueryOperation(string oPCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Operation), new PagerCondition(string.Format("select {0} from TBLOP where 1=1 and OPCODE like '{1}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)), oPCode), "OPCODE", inclusive, exclusive));
        }

        public object[] QueryOperation()
        {
            return this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition("select distinct opcode from TBLOP ORDER BY opcode"));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Operation
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Operation的总记录数</returns>
        public object[] GetAllOperation()
        {
            return this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(string.Format("select {0} from TBLOP order by OPCODE ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)))));
        }

        public object[] GetAllOutLineOperationsByResource(string resCode)
        {
            //			Log.Info(string.Format(@"select 
            //					{0} 
            //				from 
            //					TBLOP2RES,TBLOP
            //				where
            //				    TBLOP.opcode = TBLOP2RES.opcode
            //				and TBLOP2RES.rescode = '{1}'
            //				and substr(TBLOP.opcontrol,{2},1) = '1'
            //				and substr(TBLOP.opcontrol,{3},1) != '1' ",
            //				//DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route2Operation)),
            //				"TBLOP2RES.OPCODE",resCode,(int)OperationList.OutsideRoute,(int)OperationList.Testing));

            return this.DataProvider.CustomQuery(typeof(Operation),
                new SQLCondition(string.Format(
                @"select 
					{0} 
				from 
					TBLOP2RES,TBLOP
				where
				    TBLOP.opcode = TBLOP2RES.opcode
				and TBLOP2RES.rescode = '{1}'
				and substr(TBLOP.opcontrol,{2},1) = '1'
				and substr(TBLOP.opcontrol,{3},1) = '1' ",
                //DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route2Operation)),
                "TBLOP2RES.OPCODE", resCode, (int)OperationList.OutsideRoute + 1, (int)OperationList.Testing + 1)));
        }

        //取得所有的线外工序
        public object[] GetAllOutLineOp()
        {
            return this.DataProvider.CustomQuery(typeof(Operation),
                new SQLCondition(string.Format(
                @"select 
					{0} 
				from 
					TBLOP
				where
				    substr(opcontrol,{1},1) = '1'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)),
                (int)OperationList.OutsideRoute + 1)));
        }

        public bool IsOperationInRoute(string routeCode, string opCode)
        {
            return (this.DataProvider.GetCount(new SQLCondition(
            string.Format(
                @"select count(*) 
				  from TBLROUTE2OP 
			      where ROUTECODE = '{0}' and OPCODE = '{1}'", routeCode, opCode))) > 0);
        }
        #endregion

        #region Route
        public Route CreateNewRoute()
        {
            return new Route();
        }

        public void AddRoute(Route route)
        {
            this._helper.AddDomainObject(route);
        }

        public void UpdateRoute(Route route)
        {
            this._helper.UpdateDomainObject(route);
        }

        public void DeleteRoute(Route route)
        {
            this._helper.DeleteDomainObject(route, new ICheck[]{ new DeleteAssociateCheck( route, 
																	this.DataProvider,
																	new Type[]{ typeof(Route2Operation),typeof(Item2Route) }) });
        }

        public void DeleteRoute(Route[] routes)
        {
            this._helper.DeleteDomainObject(routes, new ICheck[]{ new DeleteAssociateCheck( routes, 
																	 this.DataProvider,
																	 new Type[]{ typeof(Route2Operation),typeof(Item2Route) }) });
        }

        public object GetRoute(string routeCode)
        {
            return this.DataProvider.CustomSearch(typeof(Route), new object[] { routeCode });
        }

        /// <summary>
        /// ** 功能描述:	查询Route的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode，模糊查询</param>
        /// <returns> Route的总记录数</returns>
        public int QueryRouteCount(string routeCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLROUTE where 1=1 and ROUTECODE like '{0}%' ", routeCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Route数组</returns>
        public object[] QueryRoute(string routeCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Route), new PagerCondition(string.Format("select {0} from TBLROUTE where 1=1 and ROUTECODE like '{1}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route)), routeCode), "ROUTECODE ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Route数组</returns>
        public object[] QueryItem2Route(string itemCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Item2Route),
                new PagerCondition(string.Format("select {0} from TBLITEM2ROUTE where ITEMCODE = '{1}'"
                + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(Item2Route)), itemCode),
                "ITEMCODE ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Route数组</returns>
        public int GetItem2RouteCount(string itemCode)
        {
            int iCount = 0;
            try
            {
                iCount = this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLITEM2ROUTE WHERE ITEMCODE = '{1}'" + GlobalVariables.CurrentOrganizations.GetSQLCondition())));
            }
            catch (Exception ex) { Log.Error(ex.Message); }

            return iCount;
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Route的总记录数</returns>
        public object[] GetAllRoute()
        {
            return this.DataProvider.CustomQuery(typeof(Route), new SQLCondition(string.Format("select {0} from TBLROUTE order by ROUTECODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route)))));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Route
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Route的总记录数</returns>
        public object[] GetAllRouteEnabled()
        {
            return this.DataProvider.CustomQuery(typeof(Route), new SQLCondition(string.Format("select {0} from TBLROUTE where enabled='1' order by ROUTECODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route)))));
        }


        public object GetOPFromRoute2OP(string routeCode, string resourceCode)
        {
            string sql = " SELECT * FROM tblroute2op a ,tblop2res b WHERE a.opcode=b.OPCODE  ";

            if (routeCode.Trim() != string.Empty)
            {
                sql += " and  a.routecode='" + routeCode.Trim().ToUpper() + "' ";
            }

            if (resourceCode.Trim() != string.Empty)
            {
                sql += " AND b.rescode='" + resourceCode.Trim().ToUpper() + "'";
            }

            object[] returnObjects = this.DataProvider.CustomQuery(typeof(Route2Operation), new SQLCondition(sql));
            if (returnObjects != null)
            {
                return returnObjects[0];
            }

            return null;
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Route
        /// ** 作 者:		Simone Xu
        /// ** 日 期:		途程是否被引用
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode"></param>
        /// <returns></returns>
        public bool IsRouteRef(string routeCode)
        {
            return (this.DataProvider.GetCount(new SQLCondition(string.Format(" select count(*) from TBLITEM2ROUTE where ISREF='1' AND ROUTECODE='{0}' ", routeCode))) > 0);
        }
        #endregion

        #region Route2Operation
        public Route2Operation CreateNewRoute2Operation()
        {
            return new Route2Operation();
        }

        public void AddRoute2Operation(Route2Operation route2Operation)
        {
            _standardRouteInUsage usageCheck = new _standardRouteInUsage(route2Operation, this._domainDataProvider);
            if (usageCheck.Check())
            {
                this._helper.AddDomainObject(route2Operation);
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_StandardRoute_In_Usage", string.Format("RouteCode={0}", route2Operation.RouteCode));
            }
        }

        public void AddRoute2Operation(Route2Operation[] route2Operations)
        {
            _standardRouteInUsage usageCheck = new _standardRouteInUsage(route2Operations, this._domainDataProvider);
            if (usageCheck.Check())
            {
                this._helper.AddDomainObject(route2Operations);
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_StandardRoute_In_Usage");
            }
        }

        public void UpdateRoute2Operation(Route2Operation route2Operation)
        {
            UpdateRoute2Operation(route2Operation, true);
        }

        public void UpdateRoute2Operation(Route2Operation route2Operation, bool checkSeq)
        {
            if (checkSeq)
            {
                object[] route2Ops = this.DataProvider.CustomQuery(typeof(Route2Operation),
                    new SQLCondition(string.Format("select {0} from tblroute2op where routecode = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Route2Operation)), route2Operation.RouteCode)));

                if (route2Ops != null)
                {
                    foreach (Route2Operation route2Op in route2Ops)
                    {
                        if (route2Op.OPSequence == route2Operation.OPSequence
                            && route2Op.OPCode != route2Operation.OPCode)
                        {
                            ExceptionManager.Raise(this.GetType(), "$Error_Route2Operation_Sequence_Cannot_Repeat");
                        }
                    }
                }
            }

            this._helper.UpdateDomainObject(route2Operation);
        }

        #region Standard Operation Association Check
        private class _standardRouteInUsage : ICheck
        {
            private Route2Operation[] _route2Operation = null;
            private IDomainDataProvider _provider = null;

            public _standardRouteInUsage(Route2Operation route2Operation, IDomainDataProvider dataProvider)
                : this(new Route2Operation[] { route2Operation }, dataProvider)
            {
            }

            public _standardRouteInUsage(Route2Operation[] route2Operation, IDomainDataProvider dataProvider)
            {
                this._route2Operation = route2Operation;
                this._provider = dataProvider;
            }

            public bool Check()
            {
                bool isIn = true;

                foreach (Route2Operation route2Operation in this._route2Operation)
                {
                    if (this._provider.GetCount(new SQLCondition(
                        string.Format(" select count(*) from TBLITEM2ROUTE where routecode = '{0}'", route2Operation.RouteCode))) != 0)
                    {
                        isIn = false;
                        break;
                    }
                }

                return isIn;
            }
        }
        #endregion

        /// <summary>
        /// sammer kong 2005/05/21 route in usage 
        /// </summary>
        /// <param name="route2Operation"></param>
        public void DeleteRoute2Operation(Route2Operation route2Operation)
        {
            _standardRouteInUsage usageCheck = new _standardRouteInUsage(route2Operation, this._domainDataProvider);
            if (usageCheck.Check())
            {
                this._helper.DeleteDomainObject(route2Operation);
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_StandardRoute_In_Usage", string.Format("RouteCode={0}", route2Operation.RouteCode));
            }
        }

        public void DeleteRoute2Operation(Route2Operation[] route2Operations)
        {
            _standardRouteInUsage usageCheck = new _standardRouteInUsage(route2Operations, this._domainDataProvider);
            if (usageCheck.Check())
            {
                this._helper.DeleteDomainObject(route2Operations);
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "$Error_StandardRoute_In_Usage");
            }
        }

        public bool IsRouteOperationInUsage(string routeCode, string opCode)
        {
            object obj = this.GetRoute2Operation(routeCode, opCode);
            if (obj != null && obj is Route2Operation)
            {
                _standardRouteInUsage usageCheck = new _standardRouteInUsage(obj as Route2Operation, this._domainDataProvider);
                return !usageCheck.Check();
            }
            else
            {
                ExceptionManager.Raise(this.GetType(), "Error_Argument_Null");
            }
            return false;
        }

        public object GetRoute2Operation(string routeCode, string opCode)
        {
            return this.DataProvider.CustomSearch(typeof(Route2Operation), new object[] { routeCode, opCode });
        }

        ///Laws Lu,2005/09/12,新增	根据产品途程工序代码获取工序
        public object[] QueryCurrentRoute2Operation(string itemCode, string routeCode, string opCode)
        {
            return this.DataProvider.CustomQuery(
                typeof(ItemRoute2OP),
                new SQLCondition(
                string.Format(" select {0} from TBLITEMROUTE2OP where  itemcode = '{1}' and routecode = '{2}' and opcode = '{3}'" + GlobalVariables.CurrentOrganizations.GetSQLCondition(),
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(ItemRoute2OP)), itemCode, routeCode, opCode)));
        }

        #region Route --> Operation
        /// <summary>
        /// ** 功能描述:	由RouteCode获得Operation
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <returns>Operation数组</returns>
        public object[] GetOperationByRouteCode(string routeCode)
        {
            return this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(string.Format("select {0} from TBLOP where OPCODE in ( select OPCODE from TBLROUTE2OP where ROUTECODE='{1}')", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)), routeCode)));
        }

        /// <summary>
        /// ** 功能描述:	由RouteCode获得属于Route的Operation的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="oPCode">OPCode,模糊查询</param>
        /// <returns>Operation的数量</returns>
        public int GetSelectedOperationByRouteCodeCount(string routeCode, string oPCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLROUTE2OP where ROUTECODE ='{0}' and OPCODE like '{1}%'", routeCode, oPCode)));
        }

        /// <summary>
        /// ** 功能描述:	由RouteCode获得属于Route的Operation，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="oPCode">OPCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Operation数组</returns>
        public object[] GetSelectedOperationByRouteCode(string routeCode, string oPCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Operation),
                new PagerCondition(string.Format("select {0} from TBLOP where OPCODE in ( select OPCODE from TBLROUTE2OP where ROUTECODE ='{1}') and OPCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)), routeCode, oPCode), "OPCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	由RouteCode获得不属于Route的Operation的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="oPCode">OPCode,模糊查询</param>
        /// <returns>Operation的数量</returns>
        public int GetUnselectedOperationByRouteCodeCount(string routeCode, string oPCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLOP where OPCODE not in ( select OPCODE from TBLROUTE2OP where ROUTECODE ='{0}') and OPCODE like '{1}%'", routeCode, oPCode)));
        }

        /// <summary>
        /// ** 功能描述:	由RouteCode获得不属于Route的Operation，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="oPCode">OPCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Operation数组</returns>
        public object[] GetUnselectedOperationByRouteCode(string routeCode, string oPCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Operation),
                new PagerCondition(string.Format("select {0} from TBLOP where OPCODE not in ( select OPCODE from TBLROUTE2OP where ROUTECODE ='{1}') and OPCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation)), routeCode, oPCode), "OPCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	由RouteCode获得属于Route的OperationOfRoute，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="oPCode">OPCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>OperationOfRoute数组</returns>
        public object[] GetSelectedOperationOfRouteByRouteCode(string routeCode, string opCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(OperationOfRoute), new PagerCondition(string.Format("select {0}, TBLROUTE2OP.OPSEQ, TBLROUTE2OP.ROUTECODE from TBLOP,TBLROUTE2OP where TBLOP.OPCODE = TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{1}' and TBLROUTE2OP.OPCODE like '{2}%'", "TBLROUTE2OP.OPCODE,TBLOP.OPDESC,TBLOP.OPCOLLECTION,TBLROUTE2OP.OPCONTROL,TBLROUTE2OP.MUSER,TBLROUTE2OP.MDATE,TBLROUTE2OP.MTIME,TBLROUTE2OP.EAttribute1", routeCode, opCode), "TBLROUTE2OP.OPSEQ", inclusive, exclusive));
        }
        #endregion

        #endregion

        #region Operation2Resource
        public Operation2Resource CreateNewOperation2Resource()
        {
            return new Operation2Resource();
        }

        public void AddOperation2Resource(Operation2Resource operation2Resource)
        {
            this._helper.AddDomainObject(operation2Resource);
        }

        public void AddOperation2Resource(Operation2Resource[] operation2Resources)
        {
            this._helper.AddDomainObject(operation2Resources);
        }

        public void UpdateOperation2Resource(Operation2Resource operation2Resource)
        {
            this._helper.UpdateDomainObject(operation2Resource);
        }

        public void DeleteOperation2Resource(Operation2Resource operation2Resource)
        {
            this._helper.DeleteDomainObject(operation2Resource);
        }

        public void DeleteOperation2Resource(Operation2Resource[] operation2Resources)
        {
            this._helper.DeleteDomainObject(operation2Resources);
        }

        public object GetOperation2Resource(string operationCode, string resourceCode)
        {
            return this.DataProvider.CustomSearch(typeof(Operation2Resource), new object[] { operationCode, resourceCode });
        }

        #region Operation --> Resource
        /// <summary>
        /// ** 功能描述:	由OPCode获得Resource
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode,精确查询</param>
        /// <returns>Resource数组</returns>
        public object[] GetResourceByOperationCode(string oPCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0} from TBLRES where RESCODE in ( select RESCODE from TBLOP2RES where OPCODE='{1}')";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), oPCode)));
        }

        public Operation2Resource GetOperationByResource(string rescode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Operation2Resource), new SQLCondition(string.Format("select {0} from TBLop2RES where RESCODE ='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Operation2Resource)), rescode)));
            if (objs != null && objs.Length > 0)
                return objs[0] as Operation2Resource;
            else
                return null;
        }


        /// <summary>
        /// ** 功能描述：由StepSequence获得Resource
        /// ** 作者：    Angel Zhu
        /// ** 日期:     2005-04-22 10:48:12
        /// ** 修改：
        /// ** 日期：
        /// </summary>
        /// <param name="StepSequence">StepSequence,精确查询</param>
        /// <returns>Resource 数组</returns>
        public object[] GetResourceByStepSequenceCode(string StepSequenceCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0} from TBLRES where SSCODE='{1}'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), StepSequenceCode)));
        }

        /// <summary>
        /// ** 功能描述:	由OPCode获得属于Operation的Resource的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <returns>Resource的数量</returns>
        public int GetSelectedResourceByOperationCodeCount(string oPCode, string resourceCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select count(*) from TBLOP2RES, TBLRES where TBLOP2RES.RESCODE = TBLRES.RESCODE and TBLOP2RES.OPCODE='{0}' and TBLOP2RES.RESCODE like '{1}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added 
            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, oPCode, resourceCode)));
        }

        public int GetDCT2ResourceCount(string oPCode, string dctCode)
        {
            string sql = string.Format(@"  SELECT  COUNT(*)  FROM TBLOP2RES, TBLRES  WHERE TBLOP2RES.RESCODE = TBLRES.RESCODE ");
            if (!string.IsNullOrEmpty(oPCode))
            {
                sql += string.Format("   AND   Tblop2res.Opcode='{0}' ",oPCode);
            }
            if (!string.IsNullOrEmpty(dctCode))
            {
                sql += string.Format("  AND  Tblres.Dctcode= '{0}' ",dctCode);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetResourceByOpCodeAndDctCodeCount(string oPCode, string dctCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select count(*) from TBLOP2RES, TBLRES where TBLOP2RES.RESCODE = TBLRES.RESCODE and TBLOP2RES.OPCODE='{0}' and TBLOP2RES.RESCODE like '{1}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added 
            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, oPCode, dctCode)));
        }

        /// <summary>
        /// ** 功能描述:	由OPCode获得属于Operation的Resource，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Resource数组</returns>
        public object[] GetSelectedResourceByOperationCode(string oPCode, string resourceCode, int inclusive, int exclusive)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0} from TBLRES where RESCODE in ( select RESCODE from TBLOP2RES where OPCODE ='{1}') and RESCODE like '{2}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added 

            return this.DataProvider.CustomQuery(typeof(Resource),
                new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), oPCode, resourceCode), "RESCODE", inclusive, exclusive));
        }



        public object[] GetResourceByDCTCode(string dctCode, int inclusive, int exclusive)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0} from TBLRES where dctcode in ( select dctcode from TBLDCT where dctcode ='{1}') ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added 

            return this.DataProvider.CustomQuery(typeof(Resource),
                new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), dctCode), "dctcode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	由OPCode获得不属于Operation的Resource的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <returns>Resource的数量</returns>
        public int GetUnselectedResourceByOperationCodeCount(string oPCode, string resourceCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            //sql += "select count(*) from TBLRES where RESCODE not in ( select RESCODE from TBLOP2RES where OPCODE ='{0}') and RESCODE like '{1}%'";
            sql += "select count(*) from TBLRES where RESCODE not in ( select RESCODE from TBLOP2RES ) and RESCODE like '{1}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, oPCode, resourceCode)));
        }


        public int GetUnselectedResourceByDCTCodeCount(string dctCode, string resourceCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select count(*) from TBLRES where RESCODE not in ( select RESCODE from TBLRES where upper(dctcode) like '{0}%' ) and RESCODE like '{1}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, dctCode, resourceCode)));
        }


        public int GetResourceByresAndDCTCodeCount(string resourceCode, string dctCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select count(*) from TBLRES where dctcode = '{0} and RESCODE like '{1}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, dctCode, resourceCode)));
        }

        /// <summary>
        /// ** 功能描述:	由OPCode获得不属于Operation的Resource，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Resource数组</returns>
        public object[] GetUnselectedResourceByOperationCode(string oPCode, string resourceCode, int inclusive, int exclusive)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            //sql += "select {0} from TBLRES where RESCODE not in ( select RESCODE from TBLOP2RES where OPCODE ='{1}') and RESCODE like '{2}%'";
            sql += "select {0} from TBLRES where RESCODE not in ( select RESCODE from TBLOP2RES ) and RESCODE like '{2}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource),
                new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), oPCode, resourceCode), "RESCODE", inclusive, exclusive));
        }


        public object[] GetUnselectedResourceByDCTCode(string dctCode, string resourceCode, int inclusive, int exclusive)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = ""; ;
            sql += "select {0} from TBLRES where RESCODE not in ( select RESCODE from TBLRES where upper(dctcode) like '{1}%') and RESCODE like '{2}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource),
                new PagerCondition(string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), dctCode, resourceCode), "RESCODE", inclusive, exclusive));
        }
        /// <summary>
        /// ** 功能描述:	由OPCode获得属于Operation的ResourceOfOperation，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="oPCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>ResourceOfOperation数组</returns>
        public object[] GetSelectedResourceOfOperationByOperationCode(string opCode, string resCode, int inclusive, int exclusive)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0}, TBLOP2RES.OPCODE, TBLOP2RES.RESSEQ from TBLOP2RES, TBLRES where TBLOP2RES.RESCODE = TBLRES.RESCODE and TBLOP2RES.OPCODE='{1}' and TBLOP2RES.RESCODE like '{2}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(ResourceOfOperation), new PagerCondition(string.Format(sql,
                DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Resource)), opCode, resCode), "TBLOP2RES.RESSEQ", inclusive, exclusive));
        }


        /// <summary>
        /// ** 功能描述：由moCode获得Resource
        /// ** 作者：    Simone Xu
        /// ** 日期:     2005-06-23
        /// ** 修改：
        /// ** 日期：
        /// </summary>
        /// <param name="MOCode">moCode,精确查询</param>
        /// <returns>Resource 数组</returns>
        public object[] GetResourceByMoCode(string MOCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select TBLRES.* from TBLRES";
            sql += " join TBLOP2RES ON (TBLOP2RES.rescode=TBLRES.rescode)";
            sql += " join TBLOP ON (TBLOP.opcode=TBLOP2RES.opcode)";
            sql += " join TBLROUTE2OP ON (TBLROUTE2OP.opcode=TBLOP.opcode)";
            sql += " join TBLROUTE ON (TBLROUTE.routecode=TBLROUTE2OP.routecode)";
            sql += " join TBLMO2ROUTE ON (TBLMO2ROUTE.routecode=TBLROUTE.routecode)";
            sql += " join TBLMO ON (TBLMO.mocode=TBLMO2ROUTE.mocode)";
            sql += " where mocode = '{0}' ";
            if (GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName().Length > 0)
            {
                sql += " and TBLRES.ORGID in (" + GlobalVariables.CurrentOrganizations.GetSQLConditionWithoutColumnName() + ")";
            }
            sql += " ORDER BY TBLRES.rescode ";
            // End Added 

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format(sql, MOCode)));
        }

        /// <summary>
        /// ** 功能描述：由moCode获得SMTResource
        /// ** SMT防呆资料中获取工单对应的SMTResource
        /// ** 作者：    Simone Xu
        /// ** 日期:     2005-06-23
        /// ** 修改：
        /// ** 日期：
        /// </summary>
        /// <param name="MOCode">moCode,精确查询</param>
        /// <returns>Resource 数组</returns>
        public object[] GetSMTResourceByMoCode(string MOCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select TBLRES.* from TBLRES where RESCODE IN (select RESCODE from TBLSMTRESBOM where MOCODE = '{0}') ";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            sql += " ORDER BY TBLRES.rescode ";
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format(sql, MOCode)));
        }

        #endregion

        #endregion

        #region Route --> Operation --> Resource

        /// <summary>
        /// ** 功能描述:	由OPCode获得属于Route,Operation的Resource的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="operationCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <returns>Resource的数量</returns>
        public int GetSelectedResourceByRoute2OperationCount(string routeCode, string operationCode, string resourceCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select count(*) from TBLRES where RESCODE in (select RESCODE from TBLOP2RES,TBLROUTE2OP where TBLOP2RES.OPCODE=TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{0}' and TBLROUTE2OP.OPCODE='{1}') and RESCODE like '{2}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // ENd Added

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, routeCode, operationCode, resourceCode)));
        }

        /// <summary>
        /// ** 功能描述:	由OPCode获得属于Route,Operation的Resource，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="operationCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Resource数组</returns>
        public object[] GetSelectedResourceOfOperationByRoute2Operation(string routeCode, string operationCode, string resourceCode, int inclusive, int exclusive)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0}, TBLOP2RES.OPCODE, TBLOP2RES.RESSEQ from TBLOP2RES,TBLROUTE2OP,TBLRES where TBLOP2RES.RESCODE = TBLRES.RESCODE and TBLOP2RES.OPCODE=TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{1}' and TBLROUTE2OP.OPCODE='{2}' and TBLOP2RES.RESCODE like '{3}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(ResourceOfOperation),
                new PagerCondition(string.Format(sql,
                        DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Resource)),
                        routeCode, operationCode, resourceCode),
                    "TBLOP2RES.RESSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	由OPCode获得不属于Route,Operation的Resource的数量
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="operationCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <returns>Resource的数量</returns>
        public int GetUnselectedResourceByRoute2OperationCount(string routeCode, string operationCode, string resourceCode)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select count(*) from TBLRES where RESCODE not in (select RESCODE from TBLOP2RES,TBLROUTE2OP where TBLOP2RES.OPCODE=TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{0}') and RESCODE like '{2}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.GetCount(new SQLCondition(string.Format(sql, routeCode, operationCode, resourceCode)));
        }

        /// <summary>
        /// ** 功能描述:	由OPCode获得不属于Route,Operation的Resource，分页
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:20:17
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode,精确查询</param>
        /// <param name="operationCode">OPCode,精确查询</param>
        /// <param name="resourceCode">ResourceCode,模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns>Resource数组</returns>
        public object[] GetUnselectedResourceByRoute2Operation(string routeCode, string operationCode, string resourceCode, int inclusive, int exclusive)
        {
            // Added by HI1/Venus.Feng on 20080624 for Hisense Version : Add Organization ID
            string sql = "";
            sql += "select {0} from TBLRES where RESCODE not in (select RESCODE from TBLOP2RES,TBLROUTE2OP where TBLOP2RES.OPCODE=TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{1}') and RESCODE like '{3}%'";
            sql += GlobalVariables.CurrentOrganizations.GetSQLCondition();
            // End Added

            return this.DataProvider.CustomQuery(typeof(Resource),
                new PagerCondition(string.Format(sql,
                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)),
                        routeCode, operationCode, resourceCode),
                    "RESCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	由Route和Resource获得Operation
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-31
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="routeCode">RouteCode</param>
        /// <param name="resourceCode">ResourceCode</param>
        /// <returns></returns>
        public object GetOperationByRouteAndResource(string routeCode, string resourceCode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(string.Format(@"select {0} from TBLOP,TBLROUTE2OP,TBLOP2RES where TBLOP.OPCODE = TBLOP2RES.OPCODE and 
								TBLOP.OPCODE = TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{1}' and TBLOP2RES.RESCODE='{2}'",
                            DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Operation)), routeCode, resourceCode)));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }

            return null;
        }

        /// <summary>
        /// ** 功能描述:	获得Route的第一个Operation
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-31
        /// ** 修 改:
        /// ** 日 期:
        /// ** nunit
        /// </summary>
        /// <param name="routeCode">RouteCode</param>
        /// <returns></returns>
        public object GetFirstOperationOfRoute(string routeCode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(string.Format(@"select {0} from TBLOP,TBLROUTE2OP where TBLOP.OPCODE = TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{1}' order by TBLROUTE2OP.OPSEQ",
                            DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Operation)), routeCode)));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }

            return null;
        }

        /// <summary>
        /// ** 功能描述:	获得Route的下一个Operation
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-31
        /// ** 修 改:
        /// ** 日 期:
        /// ** nunit
        /// </summary>
        /// <param name="routeCode">RouteCode</param>
        /// <param name="currentOperationCode">当前OP</param>
        /// <returns></returns>
        public object GetNextOperationOfRoute(string routeCode, string currentOperationCode)
        {
            object relation = this.GetRoute2Operation(routeCode, currentOperationCode);
            object[] operations = null;

            if (relation != null)
            {
                operations = this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(string.Format("select {0} from TBLOP,TBLROUTE2OP where TBLOP.OPCODE = TBLROUTE2OP.OPCODE and TBLROUTE2OP.ROUTECODE='{1}' and TBLROUTE2OP.OPSEQ > {2} order by TBLROUTE2OP.OPSEQ",
                    DomainObjectUtility.GetDomainObjectFieldsStringWithTableName(typeof(Operation)), routeCode, ((Route2Operation)relation).OPSequence)));
            }

            if (operations != null && operations.Length > 0)
            {
                return operations[0];
            }

            return null;
        }


        /// <summary>
        /// ** 功能描述:	判断该resource code在不operation TS的资源列表中
        /// ** 作 者:		crystal chu
        /// ** 日 期:		2005/07/26/
        /// ** 修 改:
        /// ** 日 期:
        /// ** nunit
        /// </summary>
        /// <param name="resourceCode">判断的resource code,不可为空</param>
        /// <returns></returns>
        public bool IsResourceInOperationTS(string resourceCode)
        {
            if (resourceCode.Trim().Length == 0)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Null_Paramter");
            }

            string sql = @"SELECT COUNT(*)
                                  FROM TBLOP
                                 WHERE OPCODE IN (SELECT OPCODE FROM TBLOP2RES WHERE RESCODE = '{0}')
                                   AND SUBSTR(OPCONTROL, 6, 1) = '1'";
            if (this.DataProvider.GetCount(new SQLCondition(string.Format(sql, resourceCode))) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Org

        /// <summary>
        /// ** 功能描述:	获得所有的Org
        /// ** 作 者:		Created by Scott Gu
        /// ** 日 期:		2008-6-24 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>所有的Org</returns>
        public object[] GetAllOrg()
        {
            return this.DataProvider.CustomQuery(typeof(Organization), new SQLCondition(string.Format("SELECT {0} FROM tblorg WHERE 1=1 ORDER BY orgid", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Organization)))));
        }

        /// <summary>
        /// ** 功能描述:	获得用户所属的所有Org
        /// ** 作 者:		Created by Scott Gu
        /// ** 日 期:		2008-6-24 16:37:09
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>用户所属的所有Org/returns>
        public object[] GetCurrentOrgList()
        {
            return (object[])GlobalVariables.CurrentOrganizations.GetOrganizationList().ToArray();
        }

        public object[] GetAllOrgByUserCode(string userCode)
        {
            return this.DataProvider.CustomQuery(typeof(Organization), new SQLCondition(string.Format("SELECT {0} FROM tblorg WHERE orgid IN(SELECT orgid FROM tbluser2org WHERE usercode = '{1}') ORDER BY orgid", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Organization)), userCode)));
        }


        public object[] GetAllUser2OrgByUser(string userCode)
        {
            return this.DataProvider.CustomQuery(typeof(User2Org), new SQLCondition(string.Format("SELECT {0} FROM tbluser2org WHERE usercode='{1}' ORDER BY orgid", DomainObjectUtility.GetDomainObjectFieldsString(typeof(User2Org)), userCode)));
        }

        public object GetUserDefaultOrgByUser(string userCode)
        {
            object[] orgs = this.DataProvider.CustomQuery(typeof(Organization), new SQLCondition(string.Format("SELECT {0} FROM tblorg WHERE orgid IN(SELECT orgid FROM tbluser2org WHERE usercode = '{1}' AND defaultorg=1)", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Organization)), userCode)));
            if (orgs.Length > 0)
            {
                return orgs[0];
            }
            else
            {
                return null;
            }
        }

        public object GetOrg(int orgID)
        {
            return this.DataProvider.CustomSearch(typeof(Organization), new object[] { orgID });
        }

        public void AddOrg(Organization org)
        {
            this._helper.AddDomainObject(org);
        }

        public void UpdateOrg(Organization org)
        {
            this._helper.UpdateDomainObject(org);
        }

        public void DeleteOrg(Organization org)
        {
            this._helper.DeleteDomainObject(org, new ICheck[]{ new DeleteAssociateCheck(org, 
                                                                        this.DataProvider,
                                                                        new Type[]{typeof(User2Org)})});
        }

        public void DeleteOrg(Organization[] orgs)
        {
            this._helper.DeleteDomainObject(orgs, new ICheck[]{ new DeleteAssociateCheck(orgs, 
                                                                        this.DataProvider,
                                                                        new Type[]{typeof(User2Org)})});
        }

        public object[] GetOrgList(string orgName, int inclusive, int exclusive)
        {
            string sql = "SELECT {0} FROM tblorg WHERE 1=1";
            if (orgName.Length > 0)
            {
                sql += " and orgdesc LIKE '%" + orgName + "%'";
            }
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(Organization)));

            return this.DataProvider.CustomQuery(typeof(Organization), new PagerCondition(sql, "orgid", inclusive, exclusive));
        }

        public int GetOrgListCount(string orgName)
        {
            string sql = "SELECT count(*) FROM tblorg WHERE 1=1";
            if (orgName.Length > 0)
            {
                sql += " and orgdesc LIKE '%" + orgName + "%'";
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public string GetOrgDesc(int orgID)
        {
            object obj = this.DataProvider.CustomSearch(typeof(Organization), new object[] { orgID });
            if (obj != null)
            {
                return (obj as Organization).OrganizationDescription;
            }
            return "";
        }

        #endregion

        public object[] GetAllErrorCodeByOperationCode(string opCode)
        {
            return this.DataProvider.CustomQuery(typeof(BenQGuru.eMES.Domain.TSModel.ErrorCodeA),
                new SQLCondition(string.Format("select {0} from TBLEC where ecode in ( select ecode from TBLOP2EC where OPCODE ='{1}') ORDER BY ecode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(BenQGuru.eMES.Domain.TSModel.ErrorCodeA)), opCode)));
        }


        #region DCT


        public Dct CreateNewDct()
        {
            return new Dct();
        }

        public void AddDct(Dct dct)
        {
            this._helper.AddDomainObject(dct);
        }

        public void AddDct(Dct[] dct)
        {
            this._helper.AddDomainObject(dct);
        }

        public void UpdateDct(Dct dct)
        {
            this._helper.UpdateDomainObject(dct);
        }

        public void DeleteDct(Dct dct)
        {
            this._helper.DeleteDomainObject(dct);
        }

        public void DeleteDct(Dct[] dct)
        {
            if (dct != null)
            {
                for (int i = 0; i < dct.Length; i++)
                {
                    object[] obj = this.DataProvider.CustomQuery(typeof(Resource), new SQLCondition(string.Format("select {0} from TBLRES where dctcode='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Resource)), dct[i].DctCode)));
                    if (obj != null)
                    {
                        throw new Exception("$Error_DCT__belong_To_Res");
                    }
                }
                this._helper.DeleteDomainObject(dct);
            }
        }

        public object GetDCT(string dctCode)
        {
            return this.DataProvider.CustomSearch(typeof(Dct), new object[] { dctCode });
        }

        public object[] GetDCT(string dctCode, string dctCodeDesc, int inclusive, int exclusive)
        {
            string sql = string.Format("select " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(Dct)) + " from TBLDCT where 1=1");

            if (dctCode != string.Empty && dctCode.Length != 0)
            {
                sql = string.Format("{0} and upper(dctcode) like '{1}%'", sql, dctCode);
            }
            if (dctCodeDesc != null && dctCodeDesc.Length != 0)
            {
                sql = string.Format("{0} and upper(dctdesc) like '%{1}%'", sql, dctCodeDesc);
            }
            return this.DataProvider.CustomQuery(typeof(Dct), new PagerCondition(sql, "dctcode", inclusive, exclusive));

        }

        public int GetDCTCount(string dctCode, string dctCodeDesc)
        {
            string sql = "";
            sql += "select count(*) from TBLDCT where 1=1";

            if (dctCode != string.Empty && dctCode.Length != 0)
            {
                sql = string.Format("{0} and upper(dctcode) like '{1}%'", sql, dctCode);
            }
            if (dctCodeDesc != null && dctCodeDesc.Length != 0)
            {
                sql = string.Format("{0} and upper(dctdesc) like '%{1}%'", sql, dctCodeDesc);
            }
            return this.DataProvider.GetCount(new SQLCondition(sql));

        }


        public object[] GetAllDCT()
        {
            return this.DataProvider.CustomQuery(typeof(Dct), new SQLCondition(string.Format("select {0} from TBLDCT where 1=1 order by dctcode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Dct)))));
        }
        #endregion

        #region SoftWareVersion
        public SoftWareVersion CreateNewSoftWareVersion()
        {
            return new SoftWareVersion();
        }

        public void AddSoftWareVersion(SoftWareVersion softWareVersion)
        {
            this._helper.AddDomainObject(softWareVersion);
        }

        public void UpdateSoftWareVersion(SoftWareVersion softWareVersion)
        {
            this._helper.UpdateDomainObject(softWareVersion);
        }

        public void DeleteSoftWareVersion(SoftWareVersion[] softWareVersion)
        {
            this._helper.DeleteDomainObject(softWareVersion);
        }

        public object GetSoftWareVersion(string versionCode)
        {
            return this.DataProvider.CustomSearch(typeof(SoftWareVersion), new object[] { versionCode });
        }

        //分页查询数据
        public object[] QuerySoftWareVersion(string versionCode, string status, int inclusive, int exclusive)
        {
            string sql = string.Format("select {0} from TBLSOFTVER where 1=1 and VERSIONCODE like '%{1}%' and STATUS like '%{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(SoftWareVersion)), versionCode, status);

            return this.DataProvider.CustomQuery(typeof(SoftWareVersion), new PagerCondition(sql, "VERSIONCODE", inclusive, exclusive));
        }

        //获得分页查询数据的总数
        public int QuerySoftWareVersionCount(string versionCode, string status)
        {
            string sql = string.Format("select count(*) from TBLSOFTVER where 1=1 and VERSIONCODE like '%{0}%' and STATUS like '%{1}%'", versionCode, status);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QuerySoftWareVersionForSelector(string versionCode)
        {
            string sql = "SELECT {0} FROM tblsoftver WHERE status='" + SoftWareVersionStatus.Valid
                       + "' AND TO_NUMBER (TO_CHAR (SYSDATE, 'YYYYMMDD')) BETWEEN effdate AND invdate AND versioncode LIKE '%" + versionCode + "%' ORDER BY versioncode";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SoftWareVersion)));

            return this.DataProvider.CustomQuery(typeof(SoftWareVersion), new SQLCondition(sql));
        }
        #endregion

        #region RCardChange

        public RCardChange CreateNewRCardChange()
        {
            return new RCardChange();
        }

        public void AddRCardChange(RCardChange rcardChange)
        {
            this._domainDataProvider.Insert(rcardChange);
        }


        public void DoRCardChange(ref ProcedureCondition procedureCondition)
        {
            this._domainDataProvider.CustomProcedure(ref procedureCondition);
        }

        #endregion

        #region MESEntityList

        public MESEntityList CreateNewMESEntityList()
        {
            return new MESEntityList();
        }

        public void AddMESEntityList(MESEntityList mesEntityList)
        {
            this.DataProvider.Insert(mesEntityList);
        }

        public void DeleteMESEntityList(MESEntityList mesEntityList)
        {
            this.DataProvider.Delete(mesEntityList);
        }

        public void UpdateMESEntityList(MESEntityList mesEntityList)
        {
            this.DataProvider.Update(mesEntityList);
        }

        public object GetMESEntityList(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(MESEntityList), new object[] { serial });
        }

        public int GetMESEntityListSerial(MESEntityList mesEntityList)
        {
            if (mesEntityList == null)
            {
                return -1;
            }

            string sql = "SELECT NVL(MAX(serial), -1) FROM tblmesentitylist WHERE 1 = 1 ";
            sql += string.Format("AND orgid = {0} ", mesEntityList.OrganizationID);
            sql += string.Format("AND faccode = '{0}' ", mesEntityList.FactoryCode);
            sql += string.Format("AND segcode = '{0}' ", mesEntityList.SegmentCode);
            sql += string.Format("AND bigsscode = '{0}' ", mesEntityList.BigSSCode);
            sql += string.Format("AND sscode = '{0}' ", mesEntityList.StepSequenceCode);
            sql += string.Format("AND rescode = '{0}' ", mesEntityList.ResourceCode);
            sql += string.Format("AND opcode = '{0}' ", mesEntityList.OPCode);
            sql += string.Format("AND modelcode = '{0}' ", mesEntityList.ModelCode);
            sql += string.Format("AND shifttypecode = '{0}' ", mesEntityList.ShiftTypeCode);
            sql += string.Format("AND shiftcode = '{0}' ", mesEntityList.ShiftCode);
            sql += string.Format("AND tpcode = '{0}' ", mesEntityList.TPCode);

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        #endregion

        #region 途程2工序图形化
        public object[] GetOpByRouteCode(string routeCode)
        {
            return this.DataProvider.CustomQuery(typeof(OperationOfRoute), new SQLCondition(string.Format(@"SELECT TBLROUTE2OP.OPCODE,
                                   TBLOP.OPDESC,
                                   TBLOP.OPCOLLECTION,
                                   TBLROUTE2OP.OPCONTROL,
                                   TBLROUTE2OP.MUSER,
                                   TBLROUTE2OP.MDATE,
                                   TBLROUTE2OP.MTIME,
                                   TBLROUTE2OP.EATTRIBUTE1,
                                   TBLROUTE2OP.OPSEQ,
                                   TBLROUTE2OP.ROUTECODE
                              FROM TBLOP, TBLROUTE2OP
                             WHERE TBLOP.OPCODE = TBLROUTE2OP.OPCODE
                               AND TBLROUTE2OP.ROUTECODE = '{0}'
                             ORDER BY TBLROUTE2OP.OPSEQ", routeCode)));
        }

        public object[] GetOtherOpByRouteCode(string routeCode, string opCode)
        {
            return this.DataProvider.CustomQuery(typeof(Operation), new SQLCondition(string.Format(@"SELECT 
               MUSER,
               MDATE,
               OPCOLLECTION,
               EATTRIBUTE1,
               OPCODE,
               MTIME,
               OPDESC,
               OPCONTROL
          FROM TBLOP
         WHERE OPCODE NOT IN
               (SELECT OPCODE FROM TBLROUTE2OP WHERE ROUTECODE = '{0}')
           AND OPCODE LIKE '{1}%' ORDER BY OPCODE", routeCode, opCode)));
        }

        #endregion
    }

}
