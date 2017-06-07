using System;
using System.Data;
using System.Collections;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.RMA;
using BenQGuru.eMES.Domain.MOModel;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.MOModel
{
    /// <summary>
    /// RMAFacade 的摘要说明。
    /// 文件名:		RMAFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by  
    /// 创建日期:	2006-6-28 16:26:06
    /// 修改人:
    /// 修改日期:
    /// 描 述:	
    /// 版 本:	
    /// </summary>
    public class RMAFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public RMAFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public RMAFacade(IDomainDataProvider domainDataProvider)
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

        #region CusItemCodeCheckList
        /// <summary>
        /// 
        /// </summary>
        public CusItemCodeCheckList CreateNewCusItemCodeCheckList()
        {
            return new CusItemCodeCheckList();
        }

        public void AddCusItemCodeCheckList(CusItemCodeCheckList cusItemCodeCheckList)
        {
            this._helper.AddDomainObject(cusItemCodeCheckList);
        }

        public void UpdateCusItemCodeCheckList(CusItemCodeCheckList cusItemCodeCheckList)
        {
            this._helper.UpdateDomainObject(cusItemCodeCheckList);
        }

        public void DeleteCusItemCodeCheckList(CusItemCodeCheckList cusItemCodeCheckList)
        {
            this._helper.DeleteDomainObject(cusItemCodeCheckList,
                                new ICheck[]{ new DeleteAssociateCheck( cusItemCodeCheckList,
														this.DataProvider, 
														new Type[]{
																typeof(RMABill)	})});
        }

        public void DeleteCusItemCodeCheckList(CusItemCodeCheckList[] cusItemCodeCheckList)
        {
            this._helper.DeleteDomainObject(cusItemCodeCheckList,
                                new ICheck[]{ new DeleteAssociateCheck( cusItemCodeCheckList,
														this.DataProvider, 
														new Type[]{
																typeof(RMABill)	})});
        }

        public object GetCusItemCodeCheckList(string itemCode, string modelCode, string customerCode)
        {
            return this.DataProvider.CustomSearch(typeof(CusItemCodeCheckList), new object[] { itemCode, modelCode, customerCode });
        }

        /// <summary>
        /// ** 功能描述:	查询CusItemCodeCheckList的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by  
        /// ** 日 期:		2006-6-28 16:26:06
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="modelCode">ModelCode，模糊查询</param>
        /// <param name="customerCode">CustomerCode，模糊查询</param>
        /// <returns> CusItemCodeCheckList的总记录数</returns>
        public int QueryCusItemCodeCheckListCount(string itemCodes, string modelCodes, string customerCode)
        {
            string condition = string.Empty;
            if (itemCodes != null && itemCodes.Length > 0)
            {
                condition += string.Format(" and itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }

            if (modelCodes != null && modelCodes.Length > 0)
            {
                condition += string.Format(" and modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLCITEMCODECL where 1=1 {0} and CUSCODE like '{1}%' ", condition, customerCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询CusItemCodeCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by  
        /// ** 日 期:		2006-6-28 16:26:06
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="modelCode">ModelCode，模糊查询</param>
        /// <param name="customerCode">CustomerCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> CusItemCodeCheckList数组</returns>
        public object[] QueryCusItemCodeCheckList(string itemCodes, string modelCodes, string customerCode, int inclusive, int exclusive)
        {
            string condition = string.Empty;
            if (itemCodes != null && itemCodes.Length > 0)
            {
                condition += string.Format(" and itemcode in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }

            if (modelCodes != null && modelCodes.Length > 0)
            {
                condition += string.Format(" and modelcode in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }
            return this.DataProvider.CustomQuery(typeof(CusItemCodeCheckList), new PagerCondition(string.Format("select {0} from TBLCITEMCODECL where 1=1 {1} and CUSCODE like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(CusItemCodeCheckList)), condition, customerCode), "ITEMCODE,MODELCODE,CUSCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的CusItemCodeCheckList
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by  
        /// ** 日 期:		2006-6-28 16:26:06
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>CusItemCodeCheckList的总记录数</returns>
        public object[] GetAllCusItemCodeCheckList()
        {
            return this.DataProvider.CustomQuery(typeof(CusItemCodeCheckList), new SQLCondition(string.Format("select {0} from TBLCITEMCODECL order by ITEMCODE,MODELCODE,CUSCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(CusItemCodeCheckList)))));
        }


        #endregion

        #region ErrorSymptom
        /// <summary>
        /// 
        /// </summary>
        public ErrorSymptom CreateNewErrorSymptom()
        {
            return new ErrorSymptom();
        }

        public void AddErrorSymptom(ErrorSymptom errorSymptom)
        {
            this._helper.AddDomainObject(errorSymptom);
        }

        public void UpdateErrorSymptom(ErrorSymptom errorSymptom)
        {
            this._helper.UpdateDomainObject(errorSymptom);
        }

        public void DeleteErrorSymptom(ErrorSymptom errorSymptom)
        {
            this._helper.DeleteDomainObject(errorSymptom,
                new ICheck[]{ new DeleteAssociateCheck( errorSymptom,
								this.DataProvider, 
								new Type[]{
											  typeof(Model2ErrorSymptom)	})});
        }

        public void DeleteErrorSymptom(ErrorSymptom[] errorSymptom)
        {
            this._helper.DeleteDomainObject(errorSymptom,
                new ICheck[]{ new DeleteAssociateCheck( errorSymptom,
								this.DataProvider, 
								new Type[]{
											  typeof(Model2ErrorSymptom)	})});
        }

        public object GetErrorSymptom(string symptomCode)
        {
            return this.DataProvider.CustomSearch(typeof(ErrorSymptom), new object[] { symptomCode });
        }

        /// <summary>
        /// ** 功能描述:	查询ErrorSymptom的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
        /// ** 日 期:		2006-7-7 9:44:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="symptomCode">SymptomCode，模糊查询</param>
        /// <returns> ErrorSymptom的总记录数</returns>
        public int QueryErrorSymptomCount(string symptomCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLES where 1=1 and SymptomCode like '{0}%' ", symptomCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询ErrorSymptom
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
        /// ** 日 期:		2006-7-7 9:44:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="symptomCode">SymptomCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> ErrorSymptom数组</returns>
        public object[] QueryErrorSymptom(string symptomCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ErrorSymptom), new PagerCondition(string.Format("select {0} from TBLES where 1=1 and SymptomCode like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ErrorSymptom)), symptomCode), "SymptomCode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的ErrorSymptom
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
        /// ** 日 期:		2006-7-7 9:44:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>ErrorSymptom的总记录数</returns>
        public object[] GetAllErrorSymptom()
        {
            return this.DataProvider.CustomQuery(typeof(ErrorSymptom), new SQLCondition(string.Format("select {0} from TBLES order by SymptomCode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ErrorSymptom)))));
        }


        #endregion

        #region Model2ErrorSymptom
        /// <summary>
        /// 
        /// </summary>
        public Model2ErrorSymptom CreateNewModel2ErrorSymptom()
        {
            return new Model2ErrorSymptom();
        }

        public void AddModel2ErrorSymptom(Model2ErrorSymptom model2ErrorSymptom)
        {
            this._helper.AddDomainObject(model2ErrorSymptom);
        }

        public void AddModel2ErrorSymptom(Model2ErrorSymptom[] model2ErrorSymptoms)
        {
            this._helper.AddDomainObject(model2ErrorSymptoms);
        }

        public void UpdateModel2ErrorSymptom(Model2ErrorSymptom model2ErrorSymptom)
        {
            this._helper.UpdateDomainObject(model2ErrorSymptom);
        }

        public void DeleteModel2ErrorSymptom(Model2ErrorSymptom model2ErrorSymptom)
        {
            this._helper.DeleteDomainObject(model2ErrorSymptom);
        }

        public void DeleteModel2ErrorSymptom(Model2ErrorSymptom[] model2ErrorSymptom)
        {
            this._helper.DeleteDomainObject(model2ErrorSymptom);
        }

        public object GetModel2ErrorSymptom(string modelCode, string symptomCode)
        {
            return this.DataProvider.CustomSearch(typeof(Model2ErrorSymptom), new object[] { modelCode, symptomCode });
        }

        /// <summary>
        /// ** 功能描述:	查询Model2ErrorSymptom的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
        /// ** 日 期:		2006-7-7 9:44:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="modelCode">ModelCode，模糊查询</param>
        /// <param name="symptomCode">SymptomCode，模糊查询</param>
        /// <returns> Model2ErrorSymptom的总记录数</returns>
        public int QueryModel2ErrorSymptomCount(string modelCode, string symptomCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMODEL2ERRSYM where 1=1 and ModelCode like '{0}%'  and SymptomCode like '{1}%' ", modelCode, symptomCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询Model2ErrorSymptom
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
        /// ** 日 期:		2006-7-7 9:44:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="modelCode">ModelCode，模糊查询</param>
        /// <param name="symptomCode">SymptomCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> Model2ErrorSymptom数组</returns>
        public object[] QueryModel2ErrorSymptom(string modelCode, string symptomCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(Model2ErrorSymptom), new PagerCondition(string.Format("select {0} from TBLMODEL2ERRSYM where 1=1 and ModelCode like '{1}%'  and SymptomCode like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2ErrorSymptom)), modelCode, symptomCode), "ModelCode,SymptomCode", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的Model2ErrorSymptom
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by ****
        /// ** 日 期:		2006-7-7 9:44:52
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>Model2ErrorSymptom的总记录数</returns>
        public object[] GetAllModel2ErrorSymptom()
        {
            return this.DataProvider.CustomQuery(typeof(Model2ErrorSymptom), new SQLCondition(string.Format("select {0} from TBLMODEL2ERRSYM order by ModelCode,SymptomCode", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Model2ErrorSymptom)))));
        }


        public int GetSelectedErrorSymptomByModelCodeCount(string modelCode, string symptonCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLMODEL2ERRSYM where MODELCODE ='{0}' and symptomcode like '{1}%'", modelCode, FormatHelper.PKCapitalFormat(symptonCode))));
        }


        public object[] GetSelectedErrorSymptomByModelCode(string modelCode, string symptonCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ErrorSymptom),
                new PagerCondition(string.Format("select {0} from TBLES where symptomcode in ( select symptomcode from TBLMODEL2ERRSYM where MODELCODE ='{1}') and symptomcode like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ErrorSymptom)), modelCode, FormatHelper.PKCapitalFormat(symptonCode)), "symptomcode", inclusive, exclusive));
        }

        public int GetUnselectedErrorSymptomByModelCodeCount(string modelCode, string symptonCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLES where symptomcode not in ( select symptomcode from TBLMODEL2ERRSYM where MODELCODE ='{0}') and symptomcode like '{1}%'", modelCode, FormatHelper.PKCapitalFormat(symptonCode))));
        }


        public object[] GetUnselectedErrorSymptomByModelCode(string modelCode, string symptonCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(ErrorSymptom),
                new PagerCondition(string.Format("select {0} from TBLES where symptomcode not in ( select symptomcode from TBLMODEL2ERRSYM where MODELCODE ='{1}') and symptomcode like '{2}%'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(ErrorSymptom)), modelCode, FormatHelper.PKCapitalFormat(symptonCode)), "symptomcode", inclusive, exclusive));
        }
        #endregion

        #region RMABill
        /// <summary>
        /// 
        /// </summary>
        public RMABill CreateNewRMABill()
        {
            return new RMABill();
        }

        public void AddRMABill(RMABill rMABill)
        {
            this._helper.AddDomainObject(rMABill);
        }

        public void UpdateRMABill(RMABill rMABill)
        {
            this._helper.UpdateDomainObject(rMABill);
        }

        public void DeleteRMABillOnly(RMABill rMABill)
        {
            this._helper.DeleteDomainObject(rMABill);
        }

        public void DeleteRMABillNoTrans(RMABill rMABill)
        {
                if (rMABill != null)
                {
                    object[] rMADetial = this.QueryRMADetail(rMABill.RMABillCode);
                    if (rMADetial != null && rMADetial.Length > 0)
                    {
                        for (int i = 0; i < rMADetial.Length; i++)
                        {
                            DeleteRMADetial(rMADetial[i] as RMADetial);
                        }
                    }
                }
                this._helper.DeleteDomainObject(rMABill);


        }

        public void DeleteRMABill(RMABill rMABill)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                if (rMABill != null)
                {
                    object[] rMADetial = this.QueryRMADetail(rMABill.RMABillCode);
                    if (rMADetial != null && rMADetial.Length > 0)
                    {
                        for (int i = 0; i < rMADetial.Length; i++)
                        {
                            DeleteRMADetial(rMADetial[i] as RMADetial);
                        }
                    }
                }
                this._helper.DeleteDomainObject(rMABill);

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DeleteRMABill_Failure", ex);
            }
        }

        public void DeleteRMABill(RMABill[] rMABill)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                if (rMABill != null && rMABill.Length > 0)
                {
                    for (int i = 0; i < rMABill.Length; i++)
                    {
                        //DeleteRMABill(rMABill[i]);
                        if (rMABill != null)
                        {
                            object[] rMADetial = this.QueryRMADetail(rMABill[i].RMABillCode);
                            if (rMADetial != null && rMADetial.Length > 0)
                            {
                                for (int j = 0; j < rMADetial.Length; j++)
                                {
                                    DeleteRMADetial(rMADetial[j] as RMADetial);
                                }
                            }
                        }
                        this._helper.DeleteDomainObject(rMABill[i]);
                    }
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_DeleteRMABill_Failure", ex);
            }

        }

        public object GetRMABill(string rMABillCode)
        {
            return this.DataProvider.CustomSearch(typeof(RMABill), new object[] { rMABillCode });
        }


        private string FormatObjectCodesForSql(string[] codes)
        {
            return "'" + string.Join("','", codes) + "'";
        }

        /// <summary>
        /// ** 功能描述:	获得所有的RMABill
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by  
        /// ** 日 期:		2006-6-28 16:26:06
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>RMABill的总记录数</returns>
        public object[] GetAllRMABill()
        {
            return this.DataProvider.CustomQuery(typeof(RMABill), new SQLCondition(string.Format("select {0} from TBLRMABILL order by RMABILLCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMABill)))));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rMABillCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="customerCode"></param>
        /// <param name="modelCode"></param>
        /// <param name="symptomCode"></param>
        /// <param name="mDateFrom"></param>
        /// <param name="mDateTo"></param>
        /// <returns>RMABill数组</returns>
        public int QueryRMABillCount(
            string rMABillCode,
            string itemCodes,
            string customerCode,
            string modelCodes,
            string SubsidiaryCompany,


            int mDateFrom, int mDateTo)
        {
            string condition = "";
            if (rMABillCode != null && rMABillCode.Trim().Length > 0)
            {
                condition += string.Format(" and A.RMABILLCODE in ({0}) ", FormatHelper.ProcessQueryValues(rMABillCode));
            }

            if (itemCodes != null && itemCodes.Trim().Length > 0)
            {
                condition += string.Format(" and B.ITEMCODE in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }

            if (customerCode != null && customerCode.Trim().Length > 0)
            {
                condition += " and Upper(CUSTOMCODE) like '" + customerCode.Trim().ToUpper() + "%' ";
                //condition += string.Format("and B.CUSTOMCODE in ({0})", FormatHelper.ProcessQueryValues(customerCode));
            }

            if (modelCodes != null && modelCodes.Trim().Length > 0)
            {
                condition += string.Format(" and B.MODELCODE in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }

            if (SubsidiaryCompany != null && SubsidiaryCompany.Trim().Length > 0)
            {
                //condition += string.Format(" and B.SUBCOMPANY in ({0}) ", FormatHelper.ProcessQueryValues(SubsidiaryCompany));
                condition += " and Upper(B.SUBCOMPANY) like '" + SubsidiaryCompany.Trim().ToUpper() + "%' ";
            }

            if (mDateFrom.ToString().Trim().Length > 0 && mDateFrom.ToString().Trim() != "0")
            {
                condition += " and A.MDATE >=  " + mDateFrom;
            }

            if (mDateTo.ToString().Trim().Length > 0 && mDateTo.ToString().Trim() != "0")
            {
                condition += " and A.MDATE <=  " + mDateTo;
            }

            string sqlStr = string.Format(" SELECT Count( DISTINCT A.RMABILLCODE ) FROM TBLRMABILL A  LEFT JOIN TBLRMADETIAL B ON a.RMABILLCODE = b.RMABILLCODE WHERE 1=1  {0}", condition);
            string sql = "select * from ( " + sqlStr + " ) C ";
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rMABillCode"></param>
        /// <param name="itemCode"></param>
        /// <param name="customerCode"></param>
        /// <param name="modelCode"></param>
        /// <param name="symptomCode"></param>
        /// <param name="mDateFrom"></param>
        /// <param name="mDateTo"></param>
        /// <param name="inclusive"></param>
        /// <param name="exclusive"></param>
        /// <returns></returns>
        public object[] QueryRMABill(
            string rMABillCode,
            string itemCodes,
            string customerCode,
            string modelCodes,
            string SubsidiaryCompany,
            int mDateFrom, int mDateTo,
            int inclusive, int exclusive)
        {
            string condition = "";
            if (rMABillCode != null && rMABillCode.Trim().Length > 0)
            {
                condition += string.Format(" and A.RMABILLCODE in ({0}) ", FormatHelper.ProcessQueryValues(rMABillCode));
            }

            if (itemCodes != null && itemCodes.Trim().Length > 0)
            {
                condition += string.Format(" and B.ITEMCODE in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }

            if (customerCode != null && customerCode.Trim().Length > 0)
            {
                condition += string.Format(" and B.CUSTOMCODE like '{0}%' ", customerCode);
                //condition += string.Format("and B.CUSTOMCODE in({0})", FormatHelper.ProcessQueryValues(customerCode));
            }

            if (modelCodes != null && modelCodes.Trim().Length > 0)
            {
                condition += string.Format(" and B.MODELCODE in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }

            if (SubsidiaryCompany != null && SubsidiaryCompany.Trim().Length > 0)
            {
                //condition += string.Format(" and B.SUBCOMPANY in ({0}) ", FormatHelper.ProcessQueryValues(SubsidiaryCompany));
                condition += " and Upper(B.SUBCOMPANY) like '" + SubsidiaryCompany.Trim().ToUpper() + "%' ";
            }

            if (mDateFrom.ToString().Trim().Length > 0 && mDateFrom.ToString().Trim() != "0")
            {
                condition += " and A.MDATE >=  " + mDateFrom;
            }

            if (mDateTo.ToString().Trim().Length > 0 && mDateTo.ToString().Trim() != "0")
            {
                condition += " and A.MDATE <=  " + mDateTo;
            }

            string sqlStr = string.Format(" SELECT DISTINCT  A.RMABILLCODE,a.status,a.muser,a.MEMO,a.mdate,a.mtime FROM TBLRMABILL A  LEFT JOIN TBLRMADETIAL B ON a.RMABILLCODE = b.RMABILLCODE WHERE 1=1  {0}", condition);

            string sql = "select C.RMABILLCODE,C.status,c.MEMO,c.muser,c.mdate,c.mtime from ( " + sqlStr + " ) C ";
            return this.DataProvider.CustomQuery(
                typeof(RMABill),
                new PagerCondition(sql, "C.RMABILLCODE", inclusive, exclusive));
        }

        public object[] QueryRMADetail(string rMABillCode)
        {
            string condition = "";
            if (rMABillCode != null && rMABillCode.Length > 0)
            {
                condition += string.Format(" and B.RMABILLCODE in ({0}) ", FormatHelper.ProcessQueryValues(rMABillCode));
            }
            string sql = string.Format(" SELECT B.* FROM TBLRMADETIAL B WHERE 1=1  {0}", condition);

            return this.DataProvider.CustomQuery(typeof(RMADetial), new SQLCondition(sql));
        }

        public object[] QueryRMADetail(string rMABillCode,
            string modelCodes,
            string itemCodes,
            string rcard,
            string customerCode,
            string errorCode,
            string handelCode,
            string SubsidiaryCompany,
            int inclusive, int exclusive)
        {

            string condition = "";
            if (rMABillCode != null && rMABillCode.Length > 0)
            {
                condition += string.Format(" and B.RMABILLCODE in ({0}) ", FormatHelper.ProcessQueryValues(rMABillCode));
            }
            if (modelCodes != null && modelCodes.Length > 0)
            {
                condition += string.Format(" and B.MODELCODE in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }
            if (itemCodes != null && itemCodes.Length > 0)
            {
                condition += string.Format(" and B.ITEMCODE in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }
            if (errorCode != null && errorCode.Length > 0)
            {
                condition += string.Format(" and B.ERRORCODE in ({0}) ", FormatHelper.ProcessQueryValues(errorCode));
            }
            if (rcard != null && rcard.Length > 0)
            {
                condition += " and B.RCARD like '" + rcard + "%' ";
            }
            if (customerCode != null && customerCode.Length > 0)
            {
                condition += " and B.CUSTOMCODE like '" + customerCode + "%' ";
            }
            if (handelCode != null && handelCode.Length > 0)
            {
                condition += " and B.HANDELCODE like '" + handelCode + "%' ";
            }
            if (SubsidiaryCompany != null && SubsidiaryCompany.Length > 0)
            {
                condition += " and Upper(B.SUBCOMPANY) like '" + SubsidiaryCompany.Trim().ToUpper() + "%' ";
            }

            string sql = string.Format(" SELECT B.* FROM TBLRMADETIAL B WHERE 1=1  {0}", condition);

            return this.DataProvider.CustomQuery(
                typeof(RMADetial),
                new PagerCondition(sql, " B.RMABILLCODE", inclusive, exclusive));
        }



        public int QueryRMADetailCount(string rMABillCode,
           string modelCodes,
           string itemCodes,
           string rcard,
           string customerCode,
           string errorCode,
           string handelCode,
           string SubsidiaryCompany
           )
        {

            string condition = "";
            if (rMABillCode != null && rMABillCode.Length > 0)
            {
                condition += string.Format(" and B.RMABILLCODE in ({0}) ", FormatHelper.ProcessQueryValues(rMABillCode));
            }
            if (modelCodes != null && modelCodes.Length > 0)
            {
                condition += string.Format(" and B.MODELCODE in ({0}) ", FormatHelper.ProcessQueryValues(modelCodes));
            }
            if (itemCodes != null && itemCodes.Length > 0)
            {
                condition += string.Format(" and B.ITEMCODE in ({0}) ", FormatHelper.ProcessQueryValues(itemCodes));
            }
            if (errorCode != null && errorCode.Length > 0)
            {
                condition += string.Format(" and B.ERRORCODE in ({0}) ", FormatHelper.ProcessQueryValues(errorCode));
            }
            if (rcard != null && rcard.Length > 0)
            {
                condition += " and B.RCARD like '" + rcard + "%' ";
            }
            if (customerCode != null && customerCode.Length > 0)
            {
                condition += " and B.CUSTOMCODE like '" + customerCode + "%' ";
            }
            if (handelCode != null && handelCode.Length > 0)
            {
                condition += " and B.HANDELCODE like '" + handelCode + "%' ";
            }
            if (SubsidiaryCompany != null && SubsidiaryCompany.Length > 0)
            {
                condition += " and Upper(B.SUBCOMPANY) like '" + SubsidiaryCompany.Trim().ToUpper() + "%' ";
            }
            string sql = string.Format(" SELECT count(*) FROM TBLRMADETIAL B WHERE 1=1  {0}", condition);

            return this.DataProvider.GetCount(
                new SQLCondition(sql));
        }


        public RMADetial CreateNewRMADetial()
        {
            return new RMADetial();
        }

        public void AddRMADetial(RMADetial rMADetial)
        {
            this._helper.AddDomainObject(rMADetial);
        }

        public void DeleteRMADetial(RMADetial rMADetial)
        {
            this._helper.DeleteDomainObject(rMADetial);
        }
        public void DeleteRMADetial(RMADetial[] rMADetial)
        {
            this._helper.DeleteDomainObject(rMADetial);
        }

        public void UpdateRMADetial(RMADetial rMADetial)
        {
            this._helper.UpdateDomainObject(rMADetial);
        }

        public object GetRMADetail(string rcard, string RMABillCode)
        {
            return this.DataProvider.CustomSearch(typeof(RMADetial), new object[] { rcard, RMABillCode });
        }

        public object GetRMADetailByRCard(string rcard)
        {
            string sqlStr = "SELECT a.* FROM TBLRMADETIAL a INNER JOIN TBLRMABILL b ON a.RMABILLCODE = b.rmabillcode WHERE a.rcard='" + rcard + "' AND  b.STATUS != '" + RMABillStatus.Closed + "'   ORDER BY a.MDATE DESC , a.MTIME DESC";

            object[] objs = this.DataProvider.CustomQuery(typeof(RMADetial), new SQLCondition(sqlStr));
            if (objs != null && objs.Length > 0)
            {
                return objs[0];
            }
            return null;
        }

        public string CreateNewRMABill(string buCode)
        {
            string newRMABill = string.Empty;

            //object obj = this.GetRMACodeRule( buCode );
            if (buCode.Length > 0)
            {
                //2007/03/08,Laws Lu
                /*“年代码”为新增RMA日期所在的年份，“流水号”是依据“首字母+年代码”自动排序。*/
                DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(DataProvider);
                string sql = string.Format(@"select max(substr(rmabillcode, length(rmabillcode) - 2, 3)) as seq
					from tblrmabill
					where SUBSTR (rmabillcode,0 , 3) = '{0}' ", buCode + dbDateTime.DBDate.ToString().Substring(2, 2));

                DataSet ds = ((SQLDomainDataProvider)this.DataProvider).PersistBroker.Query(sql);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string seq = ds.Tables[0].Rows[0][0].ToString();
                    int seq2 = 0;
                    try
                    {
                        seq2 = Convert.ToInt32(seq) + 1;
                    }
                    catch
                    {
                        seq2 = 1;
                    }
                    seq = seq2.ToString().PadLeft(3, '0');
                    newRMABill = buCode
                        + dbDateTime.DBDate.ToString().Substring(2, 2)
                        + seq;
                }
                else
                {
                    newRMABill = buCode
                        + dbDateTime.DBDate.ToString().Substring(2, 2)
                        + "001";
                }
            }

            return newRMABill.ToUpper();
        }

        private void ClosedRMABill(string rmaBillCode)
        {
            object obj = this.GetRMABill(rmaBillCode);

            RMABill rmabill = obj as RMABill;
            rmabill.Status = RMABillStatus.Closed;
            this.UpdateRMABill(rmabill);


        }
        /*
                public bool CheckRMABillCanClosed( object[] rmabills )
                {
                    //object[] rmabills = this.GetRMABills( rmaBillCode );
                    if( rmabills!=null && rmabills.Length>0 ) 
                    {
                        bool pass = true;

                        for(int i=0; i<rmabills.Length; i++)
                        {
                            RMABill rmabill = rmabills[i] as RMABill;
                            if( string.Compare(rmabill.Status, RMABillStatus.Release, true)!=0 )
                            {
                                pass=false;
                                break;
                            }
                            else
                            {
                                if( string.Compare(rmabill.HandleCode, RMAHandleWay.Rework, true)==0 )
                                {
                                    string reworkMO = rmabill.ReworkMO;
                                    MOFacade mofade = new MOFacade (this.DataProvider);
                                    object mo = mofade.GetMO( reworkMO );
                                    if( mo==null || string.Compare((mo as MO).MOStatus, MOManufactureStatus.MOSTATUS_CLOSE )!=0 )
                                    {
                                        pass=false;
                                        break;
                                    }
                                }
                                else if( string.Compare(rmabill.HandleCode, RMAHandleWay.TSCenter, true)==0 )
                                {
                                    string sql = string.Format(
                                        @"select count(*)
                                            from tblts
                                            where rcard in (select rcard
                                                              from tblrcard2rmabill
                                                             where RMABILLCODE = '{0}'
                                                               and ITEMCODE = '{1}'
                                                               and CUSCODE = '{2}'
                                                               and MODELCODE = '{3}')
                                            and tsstatus not in ('{4}', '{5}', '{6}')
                                            and rmabillcode = '{7}'", 
                                        rmabill.RMABillCode, rmabill.ItemCode, rmabill.CustomerCode, rmabill.ModelCode,
                                        TSStatus.TSStatus_Complete,
                                        TSStatus.TSStatus_Scrap,
                                        TSStatus.TSStatus_Split,
                                        rmabill.RMABillCode);
                                    int count = this.DataProvider.GetCount( new SQLCondition(sql) );
                                    if( count > 0 )
                                    {
                                        pass = false;
                                        break;
                                    }
                                    else
                                    {
                                        sql = string.Format(
                                            @"select count(*)
                                                from tblts
                                                where rcard in (select rcard
                                                                from tblrcard2rmabill
                                                                where RMABILLCODE = '{0}'
                                                                    and ITEMCODE = '{1}'
                                                                    and CUSCODE = '{2}'
                                                                    and MODELCODE = '{3}')
                                                and rmabillcode = '{4}'", 
                                            rmabill.RMABillCode, rmabill.ItemCode, rmabill.CustomerCode, rmabill.ModelCode, rmabill.RMABillCode);

                                        count = this.DataProvider.GetCount( new SQLCondition(sql) );

                                        if( count < rmabill.Quantity )
                                        {
                                            pass = false;
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        return pass;
                    }
                    else
                    {
                        return true;
                    }
                }

        */

        public bool CheckRMABillReworkCanClosed(object rmabill)
        {
            if (rmabill != null)
            {
                RMABill rmabilldt = rmabill as RMABill;

                string reworkMO = "";//rmabilldt.ReworkMO;
                MOFacade mofade = new MOFacade(this.DataProvider);
                object mo = mofade.GetMO(reworkMO);
                if (mo == null)
                {
                    Exception ex = new Exception(string.Format("$Domain_MO=[{0}] $CS_MO_Not_Exist", reworkMO));
                    ExceptionManager.Raise(this.GetType(), "$Error_RMABill_Cannot_Close: $Domain_RMABillCode=" + "[" + (rmabill as RMABill).RMABillCode + "]", ex);
                    return false;
                }

                if (string.Compare((mo as MO).MOStatus, MOManufactureStatus.MOSTATUS_CLOSE) != 0)
                {
                    Exception ex = new Exception(string.Format("$Domain_MO=[{0}] $CS_MO_Not_Closed", reworkMO));
                    ExceptionManager.Raise(this.GetType(), "$Error_RMABill_Cannot_Close: $Domain_RMABillCode=" + "[" + (rmabill as RMABill).RMABillCode + "]", ex);
                    return false;
                }

                return true;
            }

            return false;
        }

        public bool CheckRMABillTSCenterCanClosed(object rmabill, object[] rcard2rmabill)
        {
            if (rmabill != null
                && rcard2rmabill != null
                && rcard2rmabill.Length > 0)
            {
                ArrayList rcardArray = new ArrayList();
                foreach (RCard2RMABill obj in rcard2rmabill)
                {
                    rcardArray.Add(obj.RunningCard);
                }
                string sql = string.Format(
                    @"select count(*)
									from tblts
									where rcard in ({0})
									and tsstatus not in ('{1}', '{2}', '{3}')
									and rmabillcode = '{4}'",
                    FormatHelper.ProcessQueryValues((string[])rcardArray.ToArray(typeof(string))),
                    TSStatus.TSStatus_Complete,
                    TSStatus.TSStatus_Scrap,
                    TSStatus.TSStatus_Split,
                    (rmabill as RMABill).RMABillCode);
                int count = this.DataProvider.GetCount(new SQLCondition(sql));
                if (count > 0)
                {
                    Exception ex = new Exception("$Error_RMABill_Cannot_Close_TS");
                    ExceptionManager.Raise(this.GetType(), "$Error_RMABill_Cannot_Close: $Domain_RMABillCode=" + "[" + (rmabill as RMABill).RMABillCode + "]", ex);
                    return false;
                }
                else
                {
                    sql = string.Format(
                        @"select count(*)
										from tblts
										where rcard in ({0})
										and rmabillcode = '{1}'",
                        FormatHelper.ProcessQueryValues((string[])rcardArray.ToArray(typeof(string))),
                        (rmabill as RMABill).RMABillCode);

                    count = this.DataProvider.GetCount(new SQLCondition(sql));

                    //if (count < (rmabill as RMABill).Quantity)
                    //{
                    //    Exception ex = new Exception("$Error_RMABill_Cannot_Close_TS_Confirm");
                    //    ExceptionManager.Raise(this.GetType(), "$Error_RMABill_Cannot_Close: $Domain_RMABillCode=" + "[" + (rmabill as RMABill).RMABillCode + "]", ex);
                    //    return false;
                    //}
                }

                return true;
            }

            return false;
        }

        public bool CheckRMABillCanRelease(string rmaBillCode)
        {
            //object[] rmabills = this.GetRMABills(rmaBillCode);
            //if (rmabills != null && rmabills.Length > 0)
            //{
            //    for (int i = 0; i < rmabills.Length; i++)
            //    {
            //        RMABill rmabill = rmabills[i] as RMABill;
            //        if (string.Compare(rmabill.Status, RMABillStatus.Initial, true) != 0)
            //        {
            //            Exception ex = new Exception(string.Format("$Error_RMABill_Status=${0}", rmabill.Status));
            //            ExceptionManager.Raise(this.GetType(), "$Error_RMABill_Cannot_Release: $Domain_RMABillCode=" + "[" + (rmabill as RMABill).RMABillCode + "]", ex);
            //            return false;
            //        }
            //        else
            //        {
            //            if (string.Compare(rmabill.HandleCode, RMAHandleWay.Rework) != 0)
            //            {
            //                int count = this.GetRCards2RMABillCount(
            //                    rmabill.RMABillCode, rmabill.ItemCode, rmabill.CustomerCode, rmabill.ModelCode);

            //                if (count < rmabill.Quantity)
            //                {
            //                    Exception ex = new Exception(string.Format("$Error_MaintainQTY_LessThan_RMAQTY"));
            //                    ExceptionManager.Raise(this.GetType(), "$Error_RMABill_Cannot_Release: $Domain_RMABillCode=" + "[" + (rmabill as RMABill).RMABillCode + "]", ex);
            //                    return false;
            //                }
            //            }
            //        }
            //    }

            //    return true;
            //}
            //else
            //{
            //    return true;
            //}
            return true;
        }

        public void ReleaseRMABill(string rmaBillCode)
        {
            object obj = this.GetRMABill(rmaBillCode);

            RMABill rmabill = obj as RMABill;
            rmabill.Status = RMABillStatus.Release;
            this.UpdateRMABill(rmabill);
        }

        public void ClosedRMABills(string[] rmaBillCodes)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                for (int i = 0; i < rmaBillCodes.Length; i++)
                {
                    ClosedRMABill(rmaBillCodes[i]);
                }

                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                ExceptionManager.Raise(this.GetType(), "$Error_ClosedRMABill_Failure", ex);
            }
        }
        #endregion

        #region RMACodeRule
        /// <summary>
        /// 
        /// </summary>
        public RMACodeRule CreateNewRMACodeRule()
        {
            return new RMACodeRule();
        }

        public void AddRMACodeRule(RMACodeRule rMACodeRule)
        {
            this._helper.AddDomainObject(rMACodeRule);
        }

        public void UpdateRMACodeRule(RMACodeRule rMACodeRule)
        {
            this._helper.UpdateDomainObject(rMACodeRule);
        }

        public void DeleteRMACodeRule(RMACodeRule rMACodeRule)
        {
            this._helper.DeleteDomainObject(rMACodeRule);
        }

        public void DeleteRMACodeRule(RMACodeRule[] rMACodeRule)
        {
            this._helper.DeleteDomainObject(rMACodeRule);
        }

        public object GetRMACodeRule(string bUCode)
        {
            return this.DataProvider.CustomSearch(typeof(RMACodeRule), new object[] { bUCode });
        }

        /// <summary>
        /// ** 功能描述:	查询RMACodeRule的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by  
        /// ** 日 期:		2006-6-28 16:26:06
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="bUCode">BUCode，模糊查询</param>
        /// <returns> RMACodeRule的总记录数</returns>
        public int QueryRMACodeRuleCount(string bUCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblrmacoderule where 1=1 and BUCODE like '{0}%' ", bUCode)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询RMACodeRule
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by  
        /// ** 日 期:		2006-6-28 16:26:06
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="bUCode">BUCode，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> RMACodeRule数组</returns>
        public object[] QueryRMACodeRule(string bUCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RMACodeRule), new PagerCondition(string.Format("select {0} from tblrmacoderule where 1=1 and BUCODE like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMACodeRule)), bUCode), "BUCODE", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的RMACodeRule
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by  
        /// ** 日 期:		2006-6-28 16:26:06
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>RMACodeRule的总记录数</returns>
        public object[] GetAllRMACodeRule()
        {
            return this.DataProvider.CustomQuery(typeof(RMACodeRule), new SQLCondition(string.Format("select {0} from tblrmacoderule order by BUCODE", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RMACodeRule)))));
        }


        #endregion

        #region RCard2RMABill
        /// <summary>
        /// 
        /// </summary>
        public RCard2RMABill CreateNewRCard2RMABill()
        {
            return new RCard2RMABill();
        }

        public void AddRCard2RMABill(RCard2RMABill rCard2RMABill)
        {
            this._helper.AddDomainObject(rCard2RMABill);
        }

        public void UpdateRCard2RMABill(RCard2RMABill rCard2RMABill)
        {
            this._helper.UpdateDomainObject(rCard2RMABill);
        }

        public void DeleteRCard2RMABill(RCard2RMABill rCard2RMABill)
        {
            this._helper.DeleteDomainObject(rCard2RMABill);
        }

        public void DeleteRCard2RMABill(RCard2RMABill[] rCard2RMABill)
        {
            this._helper.DeleteDomainObject(rCard2RMABill);
        }

        public object GetRCard2RMABill(string rMABillCode, string itemCode, string customerCode, string modelCode, string runningCard)
        {
            return this.DataProvider.CustomSearch(typeof(RCard2RMABill), new object[] { rMABillCode, itemCode, customerCode, modelCode, runningCard });
        }

        /// <summary>
        /// ** 功能描述:	查询RCard2RMABill的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by 
        /// ** 日 期:		2006-6-30 15:52:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="rMABillCode">RMABillCode，模糊查询</param>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="customerCode">CustomerCode，模糊查询</param>
        /// <param name="modelCode">ModelCode，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <returns> RCard2RMABill的总记录数</returns>
        public int QueryRCard2RMABillCount(string rMABillCode, string itemCode, string customerCode, string modelCode, string runningCard)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLRCARD2RMABill where 1=1 and RMABILLCODE like '{0}%'  and ITEMCODE like '{1}%'  and CUSCODE like '{2}%'  and MODELCODE like '{3}%'  and RCARD like '{4}%' ", rMABillCode, itemCode, customerCode, modelCode, runningCard)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询RCard2RMABill
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by 
        /// ** 日 期:		2006-6-30 15:52:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="rMABillCode">RMABillCode，模糊查询</param>
        /// <param name="itemCode">ItemCode，模糊查询</param>
        /// <param name="customerCode">CustomerCode，模糊查询</param>
        /// <param name="modelCode">ModelCode，模糊查询</param>
        /// <param name="runningCard">RunningCard，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> RCard2RMABill数组</returns>
        public object[] QueryRCard2RMABill(string rMABillCode, string itemCode, string customerCode, string modelCode, string runningCard, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RCard2RMABill), new PagerCondition(string.Format("select {0} from TBLRCARD2RMABill where 1=1 and RMABILLCODE like '{1}%'  and ITEMCODE like '{2}%'  and CUSCODE like '{3}%'  and MODELCODE like '{4}%'  and RCARD like '{5}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(RCard2RMABill)), rMABillCode, itemCode, customerCode, modelCode, runningCard), "RMABILLCODE,ITEMCODE,CUSCODE,MODELCODE,RCARD", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的RCard2RMABill
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by 
        /// ** 日 期:		2006-6-30 15:52:00
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>RCard2RMABill的总记录数</returns>
        public object[] GetRCards2RMABill(string rMABillCode, string itemCode, string customerCode, string modelCode)
        {
            return this.DataProvider.CustomQuery(
                typeof(RCard2RMABill),
                new SQLCondition(string.Format(@"select {0} from TBLRCARD2RMABill
				where 1=1 and RMABILLCODE='{1}'  and ITEMCODE='{2}'  and CUSCODE='{3}' and MODELCODE='{4}'",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(RCard2RMABill)),
                rMABillCode, itemCode, customerCode, modelCode)));
        }

        public int GetRCards2RMABillCount(string rMABillCode, string itemCode, string customerCode, string modelCode)
        {
            return this.DataProvider.GetCount(
                new SQLCondition(string.Format(@"select {0} from TBLRCARD2RMABill
				where 1=1 and RMABILLCODE='{1}'  and ITEMCODE='{2}'  and CUSCODE='{3}' and MODELCODE='{4}'",
                "count(*)",
                rMABillCode, itemCode, customerCode, modelCode)));
        }

        public bool CheckRCardShipped(string rcard, string itemcode)
        {
            string sql = string.Format("select count(rcard) from tblinvrcard where rcard = '{0}' and rcardstatus = '{1}' and itemcode='{2}'",
                rcard, BenQGuru.eMES.Material.RCardStatus.Shipped, itemcode);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            return count > 0 ? true : false;
        }

        public bool CheckRCardRepeat(string rcard)
        {
            string sql = string.Format(@"select count(rcard) from tblrcard2rmabill where rcard = '{0}' and rmabillcode not in
				(select rmabillcode from tblrmabill where status = '{1}')", rcard, RMABillStatus.Closed);
            int count = this.DataProvider.GetCount(new SQLCondition(sql));
            return count > 0 ? true : false;
        }

        public int QueryRCard2RMABillCount(string reworkMOCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLSIMULATIONREPORT where 1=1 and mocode = '{0}'", reworkMOCode)));
        }

        public object[] QueryRCard2RMABill(string reworkMOCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(RCard2RMABill),
                new PagerCondition(string.Format("select {0} from TBLSIMULATIONREPORT where 1=1 and mocode = '{1}'",
                "rmabillcode ,itemcode,modelcode,rcard,muser,mdate,mtime", reworkMOCode), "RCARD", inclusive, exclusive));
        }

        public int GetRCardRMACount(string rcard)
        {
            string sql = string.Format(@"select count(rcard) from tblrcard2rmabill where rcard = '{0}'", rcard);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public int GetRCardRMAReworkCount(string rcard)
        {
            string sql = string.Format(@"select count(rcard)
				from tblsimulationreport
				where rcard = '{0}'
				and mocode in
					(select mocode
						from tblmo
						where motype = 'RMA' " + GlobalVariables.CurrentOrganizations.GetSQLCondition() + ")", rcard);
            return this.DataProvider.GetCount(new SQLCondition(sql));
        }
        #endregion

    }
}

