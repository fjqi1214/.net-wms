using System;
using System.Collections;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.BaseSetting
{
    /// <summary>
    /// ShiftModelFacade 的摘要说明。
    /// 文件名:		ShiftModelFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		Jane Shu
    /// 创建日期:	2005/03/08
    /// 修改人:		Jane Shu
    /// 修改日期:	2005-04-05  
    ///					主键大写，去掉upper
    /// 描 述:		班别模型维护后台
    /// 版 本:	
    /// </summary>
    public class ShiftModelFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public ShiftModelFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public ShiftModelFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        //Laws Lu,max life time to unlimited
        public override object InitializeLifetimeService()
        {
            return null;
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

        #region Shift Type
        public ShiftType CreateNewShiftType()
        {
            return new ShiftType();
        }

        public void AddShiftType(ShiftType shiftType)
        {
            this._helper.AddDomainObject(shiftType);
        }

        public void UpdateShiftType(ShiftType shiftType)
        {
            this._helper.UpdateDomainObject(shiftType);
        }

        public void DeleteShiftType(ShiftType shiftType)
        {
            this._helper.DeleteDomainObject(shiftType, new ICheck[]{ new DeleteAssociateCheck( shiftType, 
																		this.DataProvider,
																		new Type[]{typeof(Shift), typeof(Segment)}) });
        }

        public void DeleteShiftType(ShiftType[] shiftTypes)
        {
            this._helper.DeleteDomainObject(shiftTypes, new ICheck[]{ new DeleteAssociateCheck( shiftTypes, 
																		 this.DataProvider,
																		 new Type[]{typeof(Shift), typeof(Segment)}) });
        }

        public object GetShiftType(string shiftTypeCode)
        {
            return this.DataProvider.CustomSearch(typeof(ShiftType), new object[] { shiftTypeCode });
        }

        /// <summary>
        /// ** 功能描述:	查询ShiftType的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftTypeCode">ShiftTypeCode，模糊查询</param>
        /// <returns> ShiftType的总记录数</returns>
        public int QueryShiftTypeCount(string shiftTypeCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSHIFTTYPE where 1=1 and SHIFTTYPECODE like '{0}%'", shiftTypeCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ShiftType
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftTypeCode">ShiftTypeCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> ShiftType数组</returns>
        public object[] QueryShiftType(string shiftTypeCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ShiftType), new PagerCondition(string.Format("select {0} from TBLSHIFTTYPE where 1=1 and SHIFTTYPECODE like '{1}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ShiftType)), shiftTypeCode), "SHIFTTYPECODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的ShiftType
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>ShiftType的总记录数</returns>
        public object[] GetAllShiftType()
        {
            return this.DataProvider.CustomQuery(typeof(ShiftType), new SQLCondition(string.Format("select {0} from TBLSHIFTTYPE order by SHIFTTYPECODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ShiftType)))));
        }


        /// <summary>
        /// ** 功能描述:	根据ShiftTypeCode获得相应的Segment
        /// ** 作 者:		Angel Zhu
        /// ** 日 期:		2005-04-27 
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="ShiftTypeCode">shiftTypeCode</param>
        /// <returns>Segment数组</returns>
        public object[] GetSegmentByShiftTypeCode(string ShiftTypeCode)
        {
            return this.DataProvider.CustomQuery(typeof(Segment), new SQLCondition(string.Format("select {0} from TBLSEG where 1=1 " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + " and ShiftTypeCode='{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ShiftType)), ShiftTypeCode)));
        }

        #endregion

        #region Shift
        public Shift CreateNewShift()
        {
            return new Shift();
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="shift"></param>
        public void AddShift(Shift shift)
        {
            this._helper.AddDomainObject(shift, new ICheck[]{ new PrimaryKeyOverlapCheck(shift, this.DataProvider),
																 new ShiftDateTimeCheck(shift, this.DataProvider) });
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="shift"></param>
        public void UpdateShift(Shift shift)
        {
            try
            {
                object oldShift = this.GetShift(shift.ShiftCode);

                if (oldShift == null)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Record_Not_Exist");
                    return;
                }

                // 如果开始时间 或 结束时间有更新
                if (((Shift)oldShift).ShiftBeginTime != shift.ShiftBeginTime || ((Shift)oldShift).ShiftEndTime != shift.ShiftEndTime)
                {
                    object[] timePeriods = this.DataProvider.CustomQuery(typeof(TimePeriod),
                        new SQLCondition(string.Format("select {0} from tbltp where shiftcode='{1}' order by tpseq desc",
                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(TimePeriod)),
                        shift.ShiftCode)));

                    if (timePeriods != null && timePeriods.Length > 0)
                    {
                        // 更新后的Shift开始时间不等于第一个TimePeriod的开始时间
                        if (((Shift)oldShift).ShiftBeginTime != shift.ShiftBeginTime)
                        {
                            ExceptionManager.Raise(this.GetType(), "$Error_Shift_BeginTime_NotEqual_TimePeriod_BeginTime");
                            return;
                        }

                        // 更新后的Shift结束时间小于最后一个TimePeriod的结束时间
                        if (((Shift)shift).ShiftEndTime < ((TimePeriod)timePeriods[0]).TimePeriodEndTime)
                        {
                            ExceptionManager.Raise(this.GetType(), "$Error_Shift_EndTime_Less_Than_TimePeriod_EndTime");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Update_Check", ex);
            }

            this.DataProvider.BeginTransaction();
            try
            {
                this._helper.UpdateDomainObject(shift, new ICheck[] { new ShiftDateTimeCheck(shift, this.DataProvider) });

                this.DataProvider.CustomExecute(
                    new SQLCondition(
                    string.Format(
                    " update tbltp set shifttypecode = '{0}' where shiftcode = '{1}'", shift.ShiftTypeCode, shift.ShiftCode)));
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_Update_Domain_Object", ex);
            }

        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="shift"></param>
        public void DeleteShift(Shift shift)
        {
            this._helper.DeleteDomainObject(shift, new ICheck[]{ new DeleteAssociateCheck( shift, this.DataProvider, new Type[]{typeof(TimePeriod)}),
																	new ShiftDeleteCheck( new Shift[]{shift}, this.DataProvider ) });
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="shifts"></param>
        public void DeleteShift(Shift[] shifts)
        {
            this._helper.DeleteDomainObject(shifts, new ICheck[]{ new DeleteAssociateCheck( shifts, this.DataProvider, new Type[]{typeof(TimePeriod)}),
																	   new ShiftDeleteCheck( shifts, this.DataProvider ) });
        }

        public object GetShift(string shiftCode)
        {
            return this.DataProvider.CustomSearch(typeof(Shift), new object[] { shiftCode });
        }

        /// <summary>
        /// ** 功能描述:	查询Shift的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">ShiftCode，模糊查询</param>
        /// <param name="shiftTypeCode">ShiftTypeCode，模糊查询</param> 
        /// <returns> Shift的总记录数</returns>
        public int QueryShiftCount(string shiftCode, string shiftTypeCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSHIFT where 1=1 and SHIFTCODE like '{0}%' and SHIFTTYPECODE like '{1}%' ", shiftCode, shiftTypeCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Shift
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftCode">ShiftCode，模糊查询</param>
        /// <param name="shiftTypeCode">ShiftTypeCode，模糊查询</param> 
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Shift数组</returns>
        public object[] QueryShift(string shiftCode, string shiftTypeCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Shift), new PagerCondition(string.Format("select {0} from TBLSHIFT where 1=1 and SHIFTCODE like '{1}%' and SHIFTTYPECODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)), shiftCode, shiftTypeCode), "SHIFTTYPECODE, SHIFTSEQ", inclusive, exclusive));
        }

        public object[] QueryShiftBySegment(string shiftCode, string segmentCode, int inclusive, int exclusive)
        {
            BaseModelFacade baseModelFacade = new BaseModelFacade(this.DataProvider);
            object[] ssList = baseModelFacade.QueryStepSequence("", segmentCode, 0, int.MaxValue);

            string shiftTypeCodeList = string.Empty;
            if (ssList != null)
            {
                foreach (StepSequence ss in ssList)
                {
                    shiftTypeCodeList += ",'" + ss.ShiftTypeCode + "'";
                }
                if (shiftTypeCodeList.Trim().Length > 1)
                {
                    shiftTypeCodeList = shiftTypeCodeList.Substring(1);
                    shiftTypeCodeList = " and SHIFTTYPECODE IN (" + shiftTypeCodeList + ") ";
                }
            }

            if (shiftTypeCodeList.Trim().Length <= 0)
            {
                return null;
            }

            return this.DataProvider.CustomQuery(typeof(Shift), new PagerCondition(string.Format("select {0} from TBLSHIFT where 1=1 and SHIFTCODE like '{1}%' {2} ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)), shiftCode, shiftTypeCodeList), "SHIFTTYPECODE, SHIFTSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Shift
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Shift的总记录数</returns>
        public object[] GetAllShift()
        {
            return this.DataProvider.CustomQuery(typeof(Shift), new SQLCondition(string.Format("select {0} from TBLSHIFT order by SHIFTCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)))));
        }


        public object[] GetAllShiftByTime(int beginTime, int endTime)
        {
            return this.DataProvider.CustomQuery(typeof(Shift), new SQLCondition(string.Format("select {0} from TBLSHIFT where TPBTIME<={1} and TPETIME>={2}  order by SHIFTCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)), beginTime, endTime)));
        }

        /// <summary>
        /// ** 功能描述:	获得属于ShiftType的Shift
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="shiftTypeCode">ShiftTypeCode，精确查询</param>
        /// <returns> Shift数组</returns>
        public object[] GetShiftByShiftTypeCode(string shiftTypeCode)
        {
            return this.DataProvider.CustomQuery(typeof(Shift),
                new SQLCondition(string.Format("select {0} from TBLSHIFT where SHIFTTYPECODE='{1}' order by SHIFTSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)), shiftTypeCode)));
        }
        #endregion

        #region Time Period

        public TimePeriod CreateNewTimePeriod()
        {
            return new TimePeriod();
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="timePeriod"></param>
        public void AddTimePeriod(TimePeriod timePeriod)
        {
            timePeriod.TimePeriodSequence = this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tbltp where shiftcode='{0}'", timePeriod.ShiftCode)));

            this._helper.AddDomainObject(timePeriod, new ICheck[]{ new PrimaryKeyOverlapCheck(timePeriod, this.DataProvider),
																	  new TimePeriodDateTimeCheck(timePeriod, this.DataProvider) });
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="timePeriod"></param>
        public void UpdateTimePeriod(TimePeriod timePeriod)
        {
            object obj = this.GetTimePeriod(timePeriod.TimePeriodCode);

            if (obj == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Record_Not_Exist");
                return;
            }

            if (timePeriod.ShiftCode != ((TimePeriod)obj).ShiftCode)
            {
                try
                {
                    ((ICheck)new TimePeriodDeleteCheck(new TimePeriod[] { timePeriod }, this.DataProvider)).Check();
                }
                catch
                {
                    ExceptionManager.Raise(timePeriod.GetType(), "$Error_Update_Check", new Exception(string.Format("$Error_TimePeriod_Not_Continue_After_Update[$ShiftCode={0}, $TimePeriodCode={1}]", ((TimePeriod)obj).ShiftCode, timePeriod.TimePeriodCode)));
                    return;
                }
            }

            this._helper.UpdateDomainObject(timePeriod, new ICheck[] { new TimePeriodDateTimeCheck(timePeriod, this.DataProvider) });
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="timePeriod"></param>
        public void DeleteTimePeriod(TimePeriod timePeriod)
        {
            this._helper.DeleteDomainObject(timePeriod, new ICheck[] { new TimePeriodDeleteCheck(new TimePeriod[] { timePeriod }, this.DataProvider) });
        }

        /// <summary>
        /// ** nunit
        /// </summary>
        /// <param name="timePeriods"></param>
        public void DeleteTimePeriod(TimePeriod[] timePeriods)
        {
            this._helper.DeleteDomainObject(timePeriods, new ICheck[] { new TimePeriodDeleteCheck(timePeriods, this.DataProvider) });
        }

        /// <summary>
        /// ** 功能描述:	查询TimePeriod的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
        /// <returns> TimePeriod的总记录数</returns>
        public int QueryTimePeriodCount(string timePeriodCode, string shiftCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLTP where 1=1 and TPCODE like '{0}%' and SHIFTCODE like '{1}%' ", timePeriodCode, shiftCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询TimePeriod
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="timePeriodCode">TimePeriodCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> TimePeriod数组</returns>
        public object[] QueryTimePeriod(string timePeriodCode, string shiftCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(TimePeriod), new PagerCondition(string.Format("select {0} from TBLTP where 1=1 and TPCODE like '{1}%' and SHIFTCODE like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(TimePeriod)), timePeriodCode, shiftCode), "SHIFTCODE, TPSEQ", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的TimePeriod
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2005-03-22 10:42:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>TimePeriod的总记录数</returns>
        public object[] GetAllTimePeriod()
        {
            return this.DataProvider.CustomQuery(typeof(TimePeriod), new SQLCondition(string.Format("select {0} from TBLTP order by TPCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(TimePeriod)))));
        }

        public bool RuleCheckTimePeriod(TimePeriod timePeriod, object[] timePeriods)
        {
            //Check timePeriod 时间是否重复, 连续
            return true;
        }

        /// <summary>
        /// ** 功能描述:	获得Shift下所有的TimePeriod
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:
        /// ** 日 期: 
        /// </summary>
        /// <param name="shiftCode">ShiftCode</param>
        /// <returns>TimePeriod数组</returns>
        public object[] GetTimePeriodByShiftCode(string shiftCode)
        {
            return this.DataProvider.CustomQuery(typeof(TimePeriod),
                new SQLCondition(string.Format("select {0} from TBLTP where SHIFTCODE='{1}' order by TPSEQ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(TimePeriod)), shiftCode)));
        }

        public object GetTimePeriod(string timePeriodCode)
        {
            return this.DataProvider.CustomSearch(typeof(TimePeriod), new object[] { timePeriodCode });
        }


        /// <summary>
        /// 获取TimePeriod开始时间
        /// </summary>
        /// <param name="timePeriodCode"></param>
        /// <returns></returns>
        public int GetTimePeriodBeginTime(string timePeriodCode)
        {
            return ((TimePeriod)this.DataProvider.CustomSearch(typeof(TimePeriod), new object[] { timePeriodCode })).TimePeriodBeginTime;
        }

        /// <summary>
        /// 获取TimePeriod结束时间
        /// </summary>
        /// <param name="timePeriodCode"></param>
        /// <returns></returns>
        public int GetTimePeriodEndTime(string timePeriodCode)
        {
            return ((TimePeriod)this.DataProvider.CustomSearch(typeof(TimePeriod), new object[] { timePeriodCode })).TimePeriodEndTime;
        }

        public int GetShiftDay(TimePeriod currentTimePeriod, DateTime currentDateTime)
        {
            int shiftDay = FormatHelper.TODateInt(currentDateTime);

            if (currentTimePeriod != null)
            {
                if (currentTimePeriod.IsOverDate == FormatHelper.TRUE_STRING)
                {
                    if (currentTimePeriod.TimePeriodBeginTime < currentTimePeriod.TimePeriodEndTime)
                    {
                        shiftDay = FormatHelper.TODateInt(currentDateTime.AddDays(-1));
                    }
                    else if (Web.Helper.FormatHelper.TOTimeInt(currentDateTime) < currentTimePeriod.TimePeriodBeginTime)
                    {
                        shiftDay = FormatHelper.TODateInt(currentDateTime.AddDays(-1));
                    }
                }
            }

            return shiftDay;
        }

        public int GetShiftDayBySS(StepSequence stepSequence, DateTime currentDateTime)
        {
            int returnValue = FormatHelper.TODateInt(currentDateTime);

            if (stepSequence != null)
            {
                TimePeriod tp = (TimePeriod)GetTimePeriod(stepSequence.ShiftTypeCode, FormatHelper.TOTimeInt(currentDateTime));
                if (tp != null)
                {
                    returnValue = GetShiftDay(tp, currentDateTime);
                }
            }

            return returnValue;
        }

        public int GetShiftDayByBigSSCode(string bigSSCode, DateTime currentDateTime)
        {
            int returnValue = FormatHelper.TODateInt(currentDateTime);

            string sql = "SELECT {0} FROM tblss WHERE bigsscode = '{1}' ORDER BY ssseq ";
            sql = string.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(StepSequence)), bigSSCode);
            object[] list = this.DataProvider.CustomQuery(typeof(StepSequence), new SQLCondition(sql));
            if (list != null && list.Length > 0)
            {
                returnValue = GetShiftDayBySS((StepSequence)list[0], currentDateTime);
            }

            return returnValue;
        }

        public Shift GetShift(string shiftTypeCode, int time)
        {
            Shift returnValue = null;
            TimePeriod tp = (TimePeriod)GetTimePeriod(shiftTypeCode, time);
            if (tp != null)
            {
                returnValue = (Shift)GetShift(tp.ShiftCode);
            }

            return returnValue;
        }

        /// <summary>
        /// ** 功能描述:	根据给定时间及ShiftType 获得唯一的 TimePeriod
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-08
        /// ** 修 改:		Jane Shu
        /// ** 日 期:		2005-07-25
        /// </summary>
        /// <param name="shiftTypeCode"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public object GetTimePeriod(string shiftTypeCode, int time)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(TimePeriod), new SQLParamCondition(
                                string.Format("select {0} from tbltp where shifttypecode=$shifttype and ( " +
                                                    "   (tpbtime < tpetime and $time1 between tpbtime and tpetime )" +
                                                    "or (tpbtime > tpetime and $time2 < tpbtime and $time3 + 240000 between tpbtime and tpetime + 240000) " +
                                                    "or (tpbtime > tpetime and $time4 >= tpbtime and $time5          between tpbtime and tpetime + 240000) )",
                                                DomainObjectUtility.GetDomainObjectFieldsString(typeof(TimePeriod))),
                                new SQLParameter[]{ new SQLParameter("shifttype", typeof(string), shiftTypeCode.ToUpper()),
													new SQLParameter("time1", typeof(int), time),
													new SQLParameter("time2", typeof(int), time),
													new SQLParameter("time3", typeof(int), time),
													new SQLParameter("time4", typeof(int), time),
													new SQLParameter("time5", typeof(int), time) }));

            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }

            return null;
        }

        #endregion
    }

    #region Shift时间段判断
    public class ShiftDateTimeCheck : ICheck
    {
        private Shift _shift = null;
        private IDomainDataProvider _domainDataProvider = null;

        public ShiftDateTimeCheck(Shift shift)
        {
            this._shift = shift;
        }

        public ShiftDateTimeCheck(Shift shift, IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._shift = shift;
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

        #region ICheck 成员

        /// <summary>
        /// ** 功能描述:	判断Shift是否交叉
        /// ** 作 者:		Jane Shu
        /// ** 日 期:		2005-03-22
        /// ** 修 改:		第二天的时间均为跨日期
        /// ** 日 期:		2005-07-35
        /// </summary>
        /// <returns></returns>
        bool BenQGuru.eMES.Web.Helper.ICheck.Check()
        {
            #region Shift本身的时间检查
            Shift currShift = new Shift();

            currShift.ShiftSequence = this._shift.ShiftSequence;
            currShift.ShiftBeginTime = this._shift.ShiftBeginTime;
            currShift.ShiftEndTime = this._shift.ShiftEndTime;
            currShift.IsOverDate = this._shift.IsOverDate;

            this.adjustShiftTime(currShift);

            // 起始时间应小于结束时间
            if (currShift.ShiftBeginTime >= currShift.ShiftEndTime)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Shift_BeginTime_Greater_Than_EndTime");
                return false;
            }
            #endregion

            #region 取出同一ShiftType下的所有Shift，并按Sequence排序

            //从数据库中取出同一shifttype下,当前Shift之前的所有Shift
            object[] prevShifts = this.DataProvider.CustomQuery(
                                        typeof(Shift),
                                        new SQLCondition(
                                            string.Format("select {0} from tblshift where shifttypecode= '{1}' and shiftseq <= {2} and shiftcode <> '{3}' order by shiftseq",
                                                            DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)),
                                                            this._shift.ShiftTypeCode,
                                                            this._shift.ShiftSequence,
                                                            this._shift.ShiftCode
                                        )));

            //从数据库中取出同一shifttype下,当前Shift之后的所有Shift
            object[] nextShifts = this.DataProvider.CustomQuery(
                                        typeof(Shift),
                                        new SQLCondition(
                                            string.Format("select {0} from tblshift where shifttypecode= '{1}' and shiftseq >= {2} and shiftcode <> '{3}' order by shiftseq",
                                                            DomainObjectUtility.GetDomainObjectFieldsString(typeof(Shift)),
                                                            this._shift.ShiftTypeCode,
                                                            this._shift.ShiftSequence,
                                                            this._shift.ShiftCode
                                        )));
            #endregion

            #region Sequence重复
            if (prevShifts != null && prevShifts.Length != 0)
            {
                if (((Shift)prevShifts[prevShifts.Length - 1]).ShiftSequence == currShift.ShiftSequence)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Shift_Sequence_Overlap");
                    return false;
                }
            }

            if (nextShifts != null && nextShifts.Length != 0)
            {
                if (((Shift)nextShifts[0]).ShiftSequence == currShift.ShiftSequence)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Shift_Sequence_Overlap");
                    return false;
                }
            }
            #endregion

            #region 将跨日期后的Shift的时间加24小时
            if (prevShifts != null)
            {
                foreach (Shift shift in prevShifts)
                {
                    this.adjustShiftTime(shift);
                }
            }

            if (nextShifts != null)
            {
                foreach (Shift shift in nextShifts)
                {
                    this.adjustShiftTime(shift);
                }
            }

            #endregion

            #region 时间交叉判断
            // 当前Shift是唯一的一个
            if ((prevShifts == null || prevShifts.Length == 0) && (nextShifts == null || nextShifts.Length == 0))
            {
                return true;
            }

            if (prevShifts == null || prevShifts.Length == 0)
            {
                // 当前Shift是第一个: Shift不能超过24小时
                if (((Shift)nextShifts[nextShifts.Length - 1]).ShiftEndTime - currShift.ShiftBeginTime >= 240000)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Shift_Time_Overlap");
                    return false;
                }
            }
            else
            {
                // 当前Shift的起始时间不能大于前一个Shift的结束时间
                if (((Shift)prevShifts[prevShifts.Length - 1]).ShiftEndTime >= currShift.ShiftBeginTime)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Shift_Time_Overlap");
                    return false;
                }
            }

            if (nextShifts == null || nextShifts.Length == 0)
            {
                // 当前Shift是最后一个：Shift不能超过24小时
                if (currShift.ShiftEndTime - ((Shift)prevShifts[0]).ShiftBeginTime >= 240000)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Shift_Time_Overlap");
                    return false;
                }
            }
            else
            {
                // 当前Shift的结束时间不能大于下一个Shift的起始时间
                if (currShift.ShiftEndTime >= ((Shift)nextShifts[0]).ShiftBeginTime)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_Shift_Time_Overlap");
                    return false;
                }
            }
            #endregion

            return true;
        }

        private void adjustShiftTime(Shift shift)
        {
            if (shift.IsOverDate == FormatHelper.TRUE_STRING)
            {
                if (shift.ShiftBeginTime < shift.ShiftEndTime)
                {
                    shift.ShiftBeginTime = shift.ShiftBeginTime + 240000;
                    shift.ShiftEndTime = shift.ShiftEndTime + 240000;
                }
                else
                {
                    shift.ShiftEndTime = shift.ShiftEndTime + 240000;
                }
            }
        }
        #endregion
    }


    public class ShiftDeleteCheck : ICheck
    {
        private Shift[] _shifts = null;
        private IDomainDataProvider _domainDataProvider = null;

        public ShiftDeleteCheck(Shift[] shifts)
        {
            this._shifts = shifts;
        }

        public ShiftDeleteCheck(Shift[] shifts, IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._shifts = shifts;
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

        #region ICheck 成员

        bool BenQGuru.eMES.Web.Helper.ICheck.Check()
        {
            if (this._shifts == null)
            {
                return true;
            }

            string codes = "";

            foreach (Shift shift in this._shifts)
            {
                codes += string.Format("'{0}',", shift.ShiftCode);
            }

            codes = codes.Substring(0, codes.Length - 1);

            foreach (Shift shift in this._shifts)
            {
                if (shift.ShiftBeginTime > shift.ShiftEndTime)
                {
                    //从数据库中取出同一shifttype下,删除所有要删除的Shift后，当前Shift之后的所有Shift
                    int count = this.DataProvider.GetCount(new SQLCondition(
                        string.Format("select count(*) from tblshift where shifttypecode= '{0}' and shiftseq >= {1} and shiftcode not in ({2}) order by shiftseq",
                        shift.ShiftTypeCode,
                        shift.ShiftSequence,
                        codes
                        )));

                    if (count > 0)
                    {
                        ExceptionManager.Raise(this.GetType(), string.Format("$Error_Shift_Time_Overlap_After_Delete[$ShiftCode={0}]", shift.ShiftCode));
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion
    }
    #endregion

    #region TimePeriod时间段判断
    public class TimePeriodDateTimeCheck : ICheck
    {
        private TimePeriod _timePeriod = null;
        private IDomainDataProvider _domainDataProvider = null;

        public TimePeriodDateTimeCheck(TimePeriod timePeriod)
        {
            this._timePeriod = timePeriod;
        }

        public TimePeriodDateTimeCheck(TimePeriod timePeriod, IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._timePeriod = timePeriod;
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

        #region ICheck 成员
        /// <summary>
        /// 1.sequence must greater than exsited ones
        /// 2.start datetime must greater than the last timeperiod which sequence 
        ///	  equals to the this sequence - 1
        ///	3.this datetime must connected to the end date time of last one.
        ///	  eg.last one is 11:59:59,the start of the this one must be 12:00:00
        /// </summary>
        /// <returns></returns>
        bool BenQGuru.eMES.Web.Helper.ICheck.Check()
        {
            DBDateTime currentDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            #region TimePeriod本身的时间检查
            TimePeriod currTimePeriod = new TimePeriod();

            currTimePeriod.TimePeriodSequence = this._timePeriod.TimePeriodSequence;
            currTimePeriod.TimePeriodBeginTime = this._timePeriod.TimePeriodBeginTime;
            currTimePeriod.TimePeriodEndTime = this._timePeriod.TimePeriodEndTime;
            currTimePeriod.IsOverDate = this._timePeriod.IsOverDate;

            this.adjustTimePeriodTime(currTimePeriod);

            // 起始时间应小于结束时间
            if (currTimePeriod.TimePeriodBeginTime >= currTimePeriod.TimePeriodEndTime)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_TimePeriod_BeginTime_Greater_Than_EndTime");
                return false;
            }
            #endregion

            #region 取出同一Shift下的所有TimePeriod，并按Sequence排序
            object obj = new ShiftModelFacade(this.DataProvider).GetShift(this._timePeriod.ShiftCode);

            if (obj == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_TimePeriod_Shift_Not_Exist");
                return false;
            }

            int shiftBeginTime = ((Shift)obj).ShiftBeginTime;
            int shiftEndTime = ((Shift)obj).ShiftEndTime;

            if (((Shift)obj).IsOverDate == FormatHelper.TRUE_STRING)
            {
                if (shiftEndTime < shiftBeginTime)
                {
                    shiftEndTime = shiftEndTime + 240000;
                }
                else
                {
                    shiftBeginTime = shiftBeginTime + 240000;
                    shiftEndTime = shiftEndTime + 240000;
                }
            }

            //从数据库中取出同一Shift下,当前TimePeriod之前的所有TimePeriod
            object[] prevTimePeriods = this.DataProvider.CustomQuery(
                                        typeof(TimePeriod),
                                        new SQLCondition(string.Format("select {0} from tbltp where shiftcode= '{1}' and tpseq <= {2} and tpcode <> '{3}' order by tpseq",
                                                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(TimePeriod)),
                                                                        this._timePeriod.ShiftCode,
                                                                        this._timePeriod.TimePeriodSequence,
                                                                        this._timePeriod.TimePeriodCode)));

            //从数据库中取出同一Shift下,当前TimePeriod之后的所有TimePeriod
            object[] nextTimePeriods = this.DataProvider.CustomQuery(
                                        typeof(TimePeriod),
                                        new SQLCondition(string.Format("select {0} from tbltp where shiftcode= '{1}' and tpseq >= {2} and tpcode <> '{3}' order by tpseq",
                                                                        DomainObjectUtility.GetDomainObjectFieldsString(typeof(TimePeriod)),
                                                                        this._timePeriod.ShiftCode,
                                                                        this._timePeriod.TimePeriodSequence,
                                                                        this._timePeriod.TimePeriodCode)));
            #endregion

            #region 将跨日期后的TimePeriod的时间加24小时
            if (prevTimePeriods != null)
            {
                foreach (TimePeriod timePeriod in prevTimePeriods)
                {
                    this.adjustTimePeriodTime(timePeriod);
                }
            }

            if (nextTimePeriods != null)
            {
                foreach (TimePeriod timePeriod in nextTimePeriods)
                {
                    this.adjustTimePeriodTime(timePeriod);
                }
            }
            #endregion

            #region 时间交叉判断
            // 当前TimePeriod是第一个
            if (prevTimePeriods == null || prevTimePeriods.Length == 0)
            {
                // TimePeriod起始时间不能小于Shift的起始时间
                if (currTimePeriod.TimePeriodBeginTime != shiftBeginTime)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_First_TimePeriod_Should_Be_Shift_BeginTime");
                    return false;
                }
            }
            else
            {
                //DateTime temp = FormatHelper.ToDateTime(currentDateTime.DBDate, ((TimePeriod)prevTimePeriods[prevTimePeriods.Length - 1]).TimePeriodEndTime);
                //int tempEndTime = FormatHelper.TOTimeInt(temp.AddSeconds(1));
                int tempEndTime = FormatHelper.TimeAddSeconds(((TimePeriod)prevTimePeriods[prevTimePeriods.Length - 1]).TimePeriodEndTime, 1);


                // TimePeriod的起始时间必须与上一个TimePeriod的结束时间连续
                if (tempEndTime != currTimePeriod.TimePeriodBeginTime)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_TimePeriod_Time_Should_Be_Continuous");
                    return false;
                }

            }

            if (nextTimePeriods == null || nextTimePeriods.Length == 0)
            {
                // TimePeriod的结束时间不能大于Shift的结束时间
                if (currTimePeriod.TimePeriodEndTime > shiftEndTime)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_TimePeriod_EndTime_Greater_Than_Shift_EndTime");
                    return false;
                }
            }
            else
            {
                //DateTime temp = FormatHelper.ToDateTime(currentDateTime.DBDate, currTimePeriod.TimePeriodEndTime);
                //int tempEndTime = FormatHelper.TOTimeInt(temp.AddSeconds(1));
                int tempEndTime = FormatHelper.TimeAddSeconds(currTimePeriod.TimePeriodEndTime, 1);

                // TimePeriod的结束时间必须与下一个TimePeriod的起始时间连续
                if (tempEndTime != ((TimePeriod)nextTimePeriods[0]).TimePeriodBeginTime)
                {
                    ExceptionManager.Raise(this.GetType(), "$Error_TimePeriod_Time_Should_Be_Continuous");
                    return false;
                }
            }
            #endregion

            return true;
        }

        private void adjustTimePeriodTime(TimePeriod timePeriod)
        {
            if (timePeriod.IsOverDate == FormatHelper.TRUE_STRING)
            {
                if (timePeriod.TimePeriodBeginTime < timePeriod.TimePeriodEndTime)
                {
                    timePeriod.TimePeriodBeginTime = timePeriod.TimePeriodBeginTime + 240000;
                    timePeriod.TimePeriodEndTime = timePeriod.TimePeriodEndTime + 240000;
                }
                else
                {
                    timePeriod.TimePeriodEndTime = timePeriod.TimePeriodEndTime + 240000;
                }
            }
            if (timePeriod.TimePeriodEndTime==0)
            {
                timePeriod.TimePeriodEndTime = timePeriod.TimePeriodEndTime + 240000;
            }

        }
        #endregion
    }


    public class TimePeriodDeleteCheck : ICheck
    {
        private TimePeriod[] _timePeriods = null;
        private IDomainDataProvider _domainDataProvider = null;

        public TimePeriodDeleteCheck(TimePeriod[] timePeriods)
        {
            this._timePeriods = timePeriods;
        }

        public TimePeriodDeleteCheck(TimePeriod[] timePeriods, IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._timePeriods = timePeriods;
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

        #region ICheck 成员

        bool BenQGuru.eMES.Web.Helper.ICheck.Check()
        {
            if (this._timePeriods == null)
            {
                return true;
            }

            string codes = "";

            foreach (TimePeriod timePeriod in this._timePeriods)
            {
                codes += string.Format("'{0}',", timePeriod.TimePeriodCode);
            }
            codes = codes.Substring(0, codes.Length - 1);

            foreach (TimePeriod timePeriod in this._timePeriods)
            {
                int count = this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tbltp where shiftcode in (select shiftcode from tbltp where tpcode='{0}') and tpcode not in ({1})", timePeriod.TimePeriodCode, codes)));

                // 被删除的TimePeriod的Sequence不能小于删除后所剩的TimePeriod的数量
                if (timePeriod.TimePeriodSequence < count)
                {
                    ExceptionManager.Raise(this.GetType(), string.Format("$Error_TimePeriod_Should_Delete_From_Last_One[$TimePeriodCode={0}]", timePeriod.TimePeriodCode));
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
    #endregion
}
