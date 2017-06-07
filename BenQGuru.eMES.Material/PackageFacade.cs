using System;
using System.Runtime.Remoting;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.Package;
using BenQGuru.eMES.Domain.LotPackage;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.Package
{
    /// <summary>
    /// PackageFacade 的摘要说明。
    /// 文件名:		PackageFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
    /// 创建日期:	2006-5-27 11:21:12
    /// 修改人:
    /// 修改日期:
    /// 描 述:	
    /// 版 本:	
    /// </summary>
    public class PackageFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public PackageFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }


        public PackageFacade(IDomainDataProvider domainDataProvider)
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

        #region CARTONINFO
        /// <summary>
        /// 
        /// </summary>
        public CARTONINFO CreateNewCARTONINFO()
        {
            return new CARTONINFO();
        }

        public void AddCARTONINFO(CARTONINFO cARTONINFO)
        {
            this._helper.AddDomainObject(cARTONINFO);
        }

        public void UpdateCARTONINFO(CARTONINFO cARTONINFO)
        {
            this._helper.UpdateDomainObject(cARTONINFO);
        }

        public void DeleteCARTONINFO(CARTONINFO cARTONINFO)
        {
            this._helper.DeleteDomainObject(cARTONINFO);
        }

        public void DeleteCARTONINFO(CARTONINFO[] cARTONINFO)
        {
            this._helper.DeleteDomainObject(cARTONINFO);
        }

        //		public object GetCARTONINFO( string pKCARTONID )
        //		{
        //			return this.DataProvider.CustomSearch(typeof(CARTONINFO), new object[]{ pKCARTONID });
        //		}

        public object GetCARTONINFO(string pKCARTONNO)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(CARTONINFO), new SQLCondition(
                String.Format("select {0} from (select {1} from tblcartoninfo where cartonno='{2}') a where a.capacity >= a.collected and a.capacity <> 0"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(CARTONINFO))
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(CARTONINFO))
                , pKCARTONNO)));

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

        public object GetExistCARTONINFO(string pKCARTONNO)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(CARTONINFO), new SQLCondition(
                String.Format("select {0} from tblcartoninfo where cartonno='{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(CARTONINFO))
                , pKCARTONNO)));

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

        public void UpdateCollected(string pKCARTONNO)
        {
            this.DataProvider.CustomExecute(new SQLCondition(
                "update tblcartoninfo set collected = collected + 1 where CARTONNO ='" + pKCARTONNO + "'"));
        }

        public void UpdateCollected(string pKCARTONNO, string memo)
        {
            this.DataProvider.CustomExecute(new SQLCondition(
                "update tblcartoninfo set collected = collected + 1,EATTRIBUTE1='" + memo + "' where CARTONNO ='" + pKCARTONNO + "'"));
        }

        public void UpdateCollected(string pKCARTONNO, decimal cartonQty)
        {
            this.DataProvider.CustomExecute(new SQLCondition(
                "update tblcartoninfo set collected = collected + " + cartonQty + " where CARTONNO ='" + pKCARTONNO + "'"));
        }

        public void SubtractCollected(string pKCARTONNO)
        {
            this.DataProvider.CustomExecute(new SQLCondition(
                "update tblcartoninfo set collected = collected - 1 where CARTONNO ='" + pKCARTONNO + "'"));
        }

        public void SubtractCollected(string pKCARTONNO, decimal cartonQty)
        {
            this.DataProvider.CustomExecute(new SQLCondition(
                "update tblcartoninfo set collected = collected - " + cartonQty + " where CARTONNO ='" + pKCARTONNO + "'"));
        }

        /// <summary>
        /// ** 功能描述:	查询CARTONINFO的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-5-27 11:21:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="pKCARTONID">PKCARTONID，模糊查询</param>
        /// <returns> CARTONINFO的总记录数</returns>
        public int QueryCARTONINFOCount(string pKCARTONID)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from TBLCARTONINFO where 1=1 and PKCARTONID like '{0}%' ", pKCARTONID)));
        }

        /// <summary>
        /// ** 功能描述:	分页查询CARTONINFO
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-5-27 11:21:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="pKCARTONID">PKCARTONID，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> CARTONINFO数组</returns>
        public object[] QueryCARTONINFO(string pKCARTONID, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(CARTONINFO), new PagerCondition(string.Format("select {0} from TBLCARTONINFO where 1=1 and PKCARTONID like '{1}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(CARTONINFO)), pKCARTONID), "PKCARTONID", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的CARTONINFO
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2006-5-27 11:21:12
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>CARTONINFO的总记录数</returns>
        public object[] GetAllCARTONINFO()
        {
            return this.DataProvider.CustomQuery(typeof(CARTONINFO), new SQLCondition(string.Format("select {0} from TBLCARTONINFO order by PKCARTONID", DomainObjectUtility.GetDomainObjectFieldsString(typeof(CARTONINFO)))));
        }


        #endregion

        #region PKRuleStep
        /// <summary>
        /// 包装规则步骤
        /// </summary>
        public PKRuleStep CreateNewPKRuleStep()
        {
            return new PKRuleStep();
        }

        public void AddPKRuleStep(PKRuleStep pKRuleStep)
        {
            //检查只能有一个步骤保存条码
            if (pKRuleStep.IsSaveRCard == FormatHelper.TRUE_STRING)
            {
                object[] objs = GetPKRuleStepSaveRCardList(pKRuleStep.PKRuleCode);
                if (objs != null && objs.Length > 0)
                {
                    foreach (PKRuleStep pk in objs)
                    {
                        if (pk != null)
                            throw new Exception("$PK_Only_One_Step_Save_RCARD");
                    }
                }
            }
            this._helper.AddDomainObject(pKRuleStep);
        }

        public void UpdatePKRuleStep(PKRuleStep pKRuleStep)
        {
            //检查只能有一个步骤保存条码
            if (pKRuleStep.IsSaveRCard == FormatHelper.TRUE_STRING)
            {
                object[] objs = GetPKRuleStepSaveRCardList(pKRuleStep.PKRuleCode);
                if (objs != null && objs.Length > 0)
                {
                    foreach (PKRuleStep pk in objs)
                    {
                        if (pk != null && pk.Step != pKRuleStep.Step)
                            throw new Exception("$PK_Only_One_Step_Save_RCARD");
                    }
                }
            }

            this._helper.UpdateDomainObject(pKRuleStep);
        }

        public object GetPKRuleStep(string pKRuleCode, decimal step)
        {
            return this.DataProvider.CustomSearch(typeof(PKRuleStep), new object[] { pKRuleCode, step });
        }

        public object GetPKRuleStepSaveRCard(string pKRuleCode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(PKRuleStep), new SQLCondition(string.Format("select {0} from tblPKRuleStep where 1=1 and PKRuleCode = '{1}'  and IsSaveRCARD= '{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PKRuleStep)), pKRuleCode, FormatHelper.TRUE_STRING)));
            if (objs != null && objs.Length > 0)
                return objs[0];
            else
                return null;
        }

        public object[] GetPKRuleStepSaveRCardList(string pKRuleCode)
        {
            return this.DataProvider.CustomQuery(typeof(PKRuleStep), new SQLCondition(string.Format("select {0} from tblPKRuleStep where 1=1 and PKRuleCode = '{1}'  and IsSaveRCARD= '{2}' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PKRuleStep)), pKRuleCode, FormatHelper.TRUE_STRING)));
        }

        /// <summary>
        /// ** 功能描述:	查询PKRuleStep的总行数
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-9-30 13:08:03
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="pKRuleCode">PKRuleCode，模糊查询</param>
        /// <param name="step">Step，模糊查询</param>
        /// <returns> PKRuleStep的总记录数</returns>
        public int QueryPKRuleStepCount(string pKRuleCode, decimal step)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblPKRuleStep where 1=1 and PKRuleCode like '{0}%'  and Step like '{1}%' ", pKRuleCode, step)));
        }

        public int QueryPKRuleStepCount(string pKRuleCode)
        {
            return this.DataProvider.GetCount(new SQLCondition(string.Format("select count(*) from tblPKRuleStep where 1=1 and PKRuleCode ='{0}'", pKRuleCode)));
        }
        /// <summary>
        /// ** 功能描述:	分页查询PKRuleStep
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-9-30 13:08:03
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <param name="pKRuleCode">PKRuleCode，模糊查询</param>
        /// <param name="step">Step，模糊查询</param>
        /// <param name="inclusive">开始行数</param>
        /// <param name="exclusive">结束行数</param>
        /// <returns> PKRuleStep数组</returns>
        public object[] QueryPKRuleStep(string pKRuleCode, decimal step, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(PKRuleStep), new PagerCondition(string.Format("select {0} from tblPKRuleStep where 1=1 and PKRuleCode like '{1}%'  and Step like '{2}%' ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PKRuleStep)), pKRuleCode, step), "PKRuleCode,Step", inclusive, exclusive));
        }

        public object[] QueryPKRuleStep(string pKRuleCode, int inclusive, int exclusive)
        {
            return this.DataProvider.CustomQuery(typeof(PKRuleStep), new PagerCondition(string.Format("select {0} from tblPKRuleStep where 1=1 and PKRuleCode = '{1}'", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PKRuleStep)), pKRuleCode), "PKRuleCode,Step", inclusive, exclusive));
        }

        /// <summary>
        /// ** 功能描述:	获得所有的PKRuleStep
        /// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Jane Shu
        /// ** 日 期:		2007-9-30 13:08:03
        /// ** 修 改:
        /// ** 日 期:
        /// </summary>
        /// <returns>PKRuleStep的总记录数</returns>
        public object[] GetAllPKRuleStep()
        {
            return this.DataProvider.CustomQuery(typeof(PKRuleStep), new SQLCondition(string.Format("select {0} from tblPKRuleStep order by PKRuleCode,Step", DomainObjectUtility.GetDomainObjectFieldsString(typeof(PKRuleStep)))));
        }


        #endregion

        #region Pallet

        public Pallet CreateNewPallet()
        {
            return new Pallet();
        }

        public Pallet CreateNewPallet(string ssCode, int capacity)
        {
            Exception lastEx = new Exception("Unknown exception when creating new pallet.");

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    int currDate = FormatHelper.GetNowDBDateTime(this.DataProvider).DBDate;

                    string sql = "SELECT NVL(MAX(palletcode), '0000') palletcode FROM tblpallet WHERE palletcode LIKE '" + ssCode + currDate.ToString() + "P____'";
                    object[] objs = this.DataProvider.CustomQuery(typeof(Pallet), new SQLCondition(sql));

                    if (objs != null)
                    {
                        string last = ((Pallet)objs[0]).PalletCode;
                        last = last.Substring(last.Length - 4);
                        string serialNo = string.Format("{0:0000}", int.Parse(last) + 1);
                        Pallet newPallet = new Pallet();
                        newPallet.PalletCode = ssCode + currDate.ToString() + "P" + serialNo.ToString();
                        newPallet.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;

                        newPallet.ItemCode = " ";
                        newPallet.MOCode = " ";
                        newPallet.SSCode = " ";
                        newPallet.Capacity = capacity;
                        newPallet.RCardCount = 0;
                        newPallet.MaintainUser = " ";
                        newPallet.MaintainDate = 0;
                        newPallet.MaintainTime = 0;
                        newPallet.EAttribute1 = " ";

                        AddPallet(newPallet);

                        return newPallet;
                    }
                }
                catch (Exception ex)
                {
                    lastEx = ex;
                }
            }

            throw lastEx;
        }

        public Pallet CreateNewPallet(string pallectNo, string ssCode, int capacity, string resCode, string lotNo)
        {
            Pallet newPallet = new Pallet();
            newPallet.PalletCode = pallectNo;
            newPallet.OrganizationID = GlobalVariables.CurrentOrganizations.First().OrganizationID;
            newPallet.ItemCode = " ";
            newPallet.MOCode = " ";
            newPallet.SSCode = " ";
            newPallet.Capacity = capacity;
            newPallet.RCardCount = 0;
            newPallet.MaintainUser = " ";
            newPallet.MaintainDate = 0;
            newPallet.MaintainTime = 0;
            newPallet.EAttribute1 = lotNo.Trim() == string.Empty ? " " : lotNo;
            newPallet.Rescode = resCode;
            AddPallet(newPallet);

            return newPallet;


        }
        public void AddPallet(Pallet pallet)
        {
            this._domainDataProvider.Insert(pallet);
        }

        public void UpdatePallet(Pallet pallet)
        {
            this._domainDataProvider.Update(pallet);
        }

        public void DeletePallet(Pallet pallet)
        {
            this._domainDataProvider.Delete(pallet);
        }

        public void DeletePallet(Pallet[] pallet)
        {
            this._helper.DeleteDomainObject(pallet);
        }

        public object GetPallet(string palletcode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Pallet), new SQLCondition(
                String.Format("select {0} from tblpallet where palletcode='{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pallet))
                , palletcode)));

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

        public object[] GetPalletDetailInfo(string palletCode)
        {
            string sql = string.Empty;

            sql += "SELECT {0} ";
            sql += "FROM tblSimulationReport ";
            sql += "WHERE (mocode,rcard) IN ( ";
            sql += "SELECT r.mocode,r.rcard ";
            sql += "FROM tblpallet p, tblpallet2rcard r ";
            sql += "WHERE p.palletcode = r.palletcode ";
            sql += "AND p.palletcode = '{1}') ";
            sql += "ORDER BY rcard ";

            return this.DataProvider.CustomQuery(typeof(SimulationReport),
                new SQLCondition(String.Format(sql, DomainObjectUtility.GetDomainObjectFieldsString(typeof(SimulationReport)), palletCode)));
        }

        public object[] QueryPallet(string resCode)
        {
            string sql = "SELECT *  FROM (SELECT A.PALLETCODE,A.RCARDCOUNT,A.ITEMCODE || ' - ' || B.ITEMDESC AS ITEMCODE,";
            sql += "  A.EATTRIBUTE1  FROM TBLPALLET A   LEFT JOIN TBLITEM B ON A.ITEMCODE = B.ITEMCODE";
            sql += " WHERE RESCODE = '" + resCode .Trim().ToUpper()+ "'   ORDER BY A.MDATE DESC, A.MTIME DESC)  WHERE ROWNUM <= 5";

            return this.DataProvider.CustomQuery(typeof(Pallet), new SQLCondition(sql));
        }
        #endregion

        #region Pallet2RCard

        public Pallet2RCard CreateNewPallet2RCard()
        {
            return new Pallet2RCard();
        }

        public void AddPallet2RCard(Pallet2RCard pallet2RCard)
        {
            this._domainDataProvider.Insert(pallet2RCard);
        }

        public void UpdatePallet2RCard(Pallet2RCard pallet2RCard)
        {
            this._domainDataProvider.Update(pallet2RCard);
        }

        public void DeletePallet2RCard(Pallet2RCard pallet2RCard)
        {
            this._domainDataProvider.Delete(pallet2RCard);
        }

        public void DeletePallet2RCard(Pallet2RCard[] pallet2RCard)
        {
            this._helper.DeleteDomainObject(pallet2RCard);
        }
        public object GetPallet2RCardByRCard(string rCard)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Pallet2RCard), new SQLCondition(
                String.Format("select {0} from tblPallet2RCard where rcard='{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pallet2RCard))
                , rCard)));

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

        public object GetPallet2RCardByPallet(string PalletCode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Pallet2RCard), new SQLCondition(
                String.Format("select {0} from tblPallet2RCard where palletcode='{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pallet2RCard))
                , PalletCode)));

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

        public object[] GetPallet2RCardListByPallet(string PalletCode)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Pallet2RCard), new SQLCondition(
                String.Format("select {0} from tblPallet2RCard where palletcode='{1}'"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pallet2RCard))
                , PalletCode)));

            return objs;
        }

        public object GetPallet2RCardByKey(string PalletCode, string Rcard)
        {
            object[] objs = this.DataProvider.CustomQuery(typeof(Pallet2RCard), new SQLCondition(
                String.Format("select {0} from tblPallet2RCard where palletcode='{1}' and rcard={2}"
                , DomainObjectUtility.GetDomainObjectFieldsString(typeof(Pallet2RCard))
                , PalletCode, Rcard)));

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

        #endregion
        public void ACTIONPalletAndRacrd(Pallet pallet, Pallet2RCard palletRcard)
        {
            UpdatePallet(pallet);
            AddPallet2RCard(palletRcard);
        }

        public void ACTIONPalletAndRacrd(Pallet pallet, Pallet2RCard palletRcard, StackToRcard stackToRcard, InvInTransaction invInTransaction)
        {
            UpdatePallet(pallet);
            AddPallet2RCard(palletRcard);

            InventoryFacade objFacade = new InventoryFacade(this.DataProvider);
            objFacade.AddInvInTransaction(invInTransaction);

            InvInTransaction GetinvInTransaction = (InvInTransaction)objFacade.GetInvInTransaction(invInTransaction.Rcard, invInTransaction.CartonCode,
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

            objFacade.AddStackToRcard(stackToRcard);
        }


        #region CheckPacking
        public PACKINGCHK CreateNewPACKINGCHK()
        {
            return new PACKINGCHK();
        }

        public void AddPACKINGCHK(PACKINGCHK PACKINGCHKNEW)
        {
            this._helper.AddDomainObject(PACKINGCHKNEW);
        }

        public void UpdatePACKINGCHK(PACKINGCHK pACKINGCHK)
        {
            this._helper.UpdateDomainObject(pACKINGCHK);
        }

        public void DeletePACKINGCHK(PACKINGCHK pACKINGCHK)
        {
            this._helper.DeleteDomainObject(pACKINGCHK);
        }

        public void DeletePACKINGCHK(PACKINGCHK[] pACKINGCHK)
        {
            this._helper.DeleteDomainObject(pACKINGCHK);
        }

        public object GetPACKINGCHK(string rcard)
        {
            return this.DataProvider.CustomSearch(typeof(PACKINGCHK), new object[] { rcard });
        }
        #endregion

        #region SKDCartonDetail

        public SKDCartonDetail CreateNewSKDCartonDetail()
        {
            return new SKDCartonDetail();
        }

        public void AddSKDCartonDetail(SKDCartonDetail skdcartondetail)
        {
            this._domainDataProvider.Insert(skdcartondetail);
        }

        public void UpdateSKDCartonDetail(SKDCartonDetail skdcartondetail)
        {
            this._domainDataProvider.Update(skdcartondetail);
        }

        public void DeleteSKDCartonDetail(SKDCartonDetail skdcartondetail)
        {
            this._domainDataProvider.Delete(skdcartondetail);
        }

        public object GetSKDCartonDetail(string moCode, string mCard)
        {
            return this.DataProvider.CustomSearch(typeof(SKDCartonDetail),
                new object[] { moCode, mCard });
        }

        public object[] QuerySKDCartobDetailWithCapity(string cartonNO, string moCode)
        {
            string sql = string.Empty;
            sql += " SELECT MO.MOCODE,MO.ITEMCODE,BOM.SBITEMCODE,M.MNAME,";
            sql += " NVL(CQTY.CCOUNT, 0) AS CARTONQTY,NVL(MOQTY.CCOUNT, 0) AS MOQTY,CEIL(SUM(MO.MOPLANQTY * BOM.SBITEMQTY)) PLANQTY";
            sql += "  FROM TBLMO MO INNER JOIN TBLSBOM BOM ON MO.ORGID = BOM.ORGID";
            sql += "                 AND MO.ITEMCODE = BOM.ITEMCODE  AND MO.MOBOM = BOM.SBOMVER";
            sql += " INNER JOIN TBLMATERIAL M ON BOM.SBITEMCODE = M.MCODE";
            sql += "                  AND BOM.ORGID = M.ORGID   AND M.MCONTROLTYPE = 'item_control_keyparts'";
            sql += " LEFT JOIN (SELECT MOCODE, ITEMCODE, SBITEMCODE, COUNT(MCARD) CCOUNT ";
            sql += "             FROM TBLSKDCARTONDETAIL  WHERE 1=1 AND CARTONNO = '" + cartonNO.Trim().ToUpper() + "'";
            sql += "          GROUP BY MOCODE, ITEMCODE, SBITEMCODE) CQTY ON CQTY.MOCODE =MO.MOCODE";
            sql += "   AND CQTY.SBITEMCODE =BOM.SBITEMCODE     AND CQTY.ITEMCODE =BOM.ITEMCODE";
            sql += " LEFT JOIN (SELECT MOCODE, ITEMCODE, SBITEMCODE, COUNT(MCARD) CCOUNT  FROM TBLSKDCARTONDETAIL";
            sql += "    GROUP BY MOCODE, ITEMCODE, SBITEMCODE) MOQTY ON MOQTY.MOCODE =MO.MOCODE";
            sql += "     AND MOQTY.SBITEMCODE =BOM.SBITEMCODE    AND MOQTY.ITEMCODE =BOM.ITEMCODE";
            sql += "  WHERE 1=1  AND MO.MOCODE = '" + moCode.Trim().ToUpper() + "'";
            sql += "  GROUP BY MO.MOCODE,MO.ITEMCODE,BOM.SBITEMCODE,";
            sql += "         M.MNAME,CQTY.CCOUNT,MOQTY.CCOUNT ORDER BY BOM.SBITEMCODE";

            return this._domainDataProvider.CustomQuery(typeof(SKDCartonDetailWithCapity), new SQLCondition(sql));
        }

        public object QuerySKDCartobDetailWithCarton(string cartonNO)
        {
            string sql = string.Empty;
            sql += "   SELECT DISTINCT MOCODE FROM TBLSKDCARTONDETAIL WHERE CARTONNO = '" + cartonNO.Trim().ToUpper() + "'";

            object[] returnObjects = this._domainDataProvider.CustomQuery(typeof(SKDCartonDetail), new SQLCondition(sql));
            if (returnObjects != null && returnObjects.Length > 0)
            {
                return returnObjects[0];
            }

            return null;
        }
        #endregion

        #region Pallet2RcardLog

        public Pallet2RcardLog CreateNewPallet2RcardLog()
        {
            return new Pallet2RcardLog();
        }

        public void AddPallet2RcardLog(Pallet2RcardLog pallet2RcardLog)
        {
            this._helper.AddDomainObject(pallet2RcardLog);
        }

        public void UpdatePallet2RcardLog(Pallet2RcardLog pallet2RcardLog)
        {
            this._helper.UpdateDomainObject(pallet2RcardLog);
        }

        public void DeletePallet2RcardLog(Pallet2RcardLog pallet2RcardLog)
        {
            this._helper.DeleteDomainObject(pallet2RcardLog);
        }

        public void DeletePallet2RcardLog(Pallet2RcardLog[] pallet2RcardLog)
        {
            this._helper.DeleteDomainObject(pallet2RcardLog);
        }

        public object GetPallet2RcardLog(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(Pallet2RcardLog), new object[] { serial });
        }

        public object GetPallet2RcardLog(string palletCode, string rcard)
        {
            string sql = " SELECT * FROM TBLPALLET2RCARDLog WHERE 1=1 ";

            if (palletCode.Trim() != string.Empty)
            {
                sql += " AND palletCode='" + palletCode.Trim().ToUpper() + "' ";
            }

            if (rcard.Trim() != string.Empty)
            {
                sql += " AND rcard='" + rcard.Trim().ToUpper() + "' ";
            }

            sql += " ORDER BY packdate DESC,packtime DESC";

            object[] getObjects = this.DataProvider.CustomQuery(typeof(Pallet2RcardLog), new SQLCondition(sql));

            if (getObjects != null)
            {
                return getObjects[0];
            }

            return null;
        }

        public void AddPallet2RcardLog(string palletCode, string rcard, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            Pallet2RcardLog pallet2RcardLog = this.CreateNewPallet2RcardLog();

            pallet2RcardLog.Serial = 0;
            pallet2RcardLog.PalletCode = palletCode;
            pallet2RcardLog.Rcard = rcard;
            pallet2RcardLog.PackUser = userCode;
            pallet2RcardLog.PackDate = dbDateTime.DBDate;
            pallet2RcardLog.PackTime = dbDateTime.DBTime;

            this.AddPallet2RcardLog(pallet2RcardLog);
        }

        public void SaveRemovePallet2RcardLog(string palletCode, string rcard, string userCode)
        {
            if (palletCode.Trim().Length <= 0 || rcard.Trim().Length <= 0)
            {
                return;
            }

            Pallet2RcardLog pallet2RcardLog = (Pallet2RcardLog)this.GetPallet2RcardLog(palletCode, rcard);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (pallet2RcardLog != null && string.IsNullOrEmpty(pallet2RcardLog.RemoveUser) && pallet2RcardLog.RemovDate == 0)
            {
                pallet2RcardLog.RemoveUser = userCode;
                pallet2RcardLog.RemovDate = dbDateTime.DBDate;
                pallet2RcardLog.RemovTime = dbDateTime.DBTime;

                this.UpdatePallet2RcardLog(pallet2RcardLog);
            }
            else
            {
                Pallet2RcardLog newPallet2RcardLog = this.CreateNewPallet2RcardLog();

                newPallet2RcardLog.Serial = 0;
                newPallet2RcardLog.PalletCode = palletCode;
                newPallet2RcardLog.Rcard = rcard;
                newPallet2RcardLog.RemoveUser = userCode;
                newPallet2RcardLog.RemovDate = dbDateTime.DBDate;
                newPallet2RcardLog.RemovTime = dbDateTime.DBTime;

                this.AddPallet2RcardLog(newPallet2RcardLog);
            }
        }

        #endregion

        #region Carton2RCARD

        public Carton2RCARD CreateNewCarton2RCARD()
        {
            return new Carton2RCARD();
        }

        public void AddCarton2RCARD(Carton2RCARD carton2RCARD)
        {
            this._helper.AddDomainObject(carton2RCARD);
        }

        public void UpdateCarton2RCARD(Carton2RCARD carton2RCARD)
        {
            this._helper.UpdateDomainObject(carton2RCARD);
        }

        public void DeleteCarton2RCARD(Carton2RCARD carton2RCARD)
        {
            this._helper.DeleteDomainObject(carton2RCARD);
        }

        public void DeleteCarton2RCARD(Carton2RCARD[] carton2RCARD)
        {
            this._helper.DeleteDomainObject(carton2RCARD);
        }

        public object GetCarton2RCARD(string cartonCode, string rcard)
        {
            return this.DataProvider.CustomSearch(typeof(Carton2RCARD), new object[] { cartonCode, rcard });
        }

        public void addCarton2RCARD(string cartonCode, string rcard, string userCode,string moCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            Carton2RCARD carton2RCARD = this.CreateNewCarton2RCARD();

            carton2RCARD.CartonCode = cartonCode;
            carton2RCARD.Rcard = rcard;
            carton2RCARD.MOCode = moCode;
            carton2RCARD.MUser = userCode;
            carton2RCARD.MDate = dbDateTime.DBDate;
            carton2RCARD.MTime = dbDateTime.DBTime;

            this.AddCarton2RCARD(carton2RCARD);
        }

        public object[] GetCarton2RCARDByCartonNO(string cartonCode)
        {
            string sql = " SELECT * FROM TBLCarton2RCARD WHERE 1=1 AND cartonno='" + cartonCode.Trim().ToUpper() + "' ";

            sql += " ORDER BY cartonno DESC,rcard asc";

            object[] objects = this.DataProvider.CustomQuery(typeof(Carton2RCARD), new SQLCondition(sql));

            return objects;

        }

        public object GetItemCodeByMOCode(string moCode)
        {
            string sql = " SELECT a.mocode,a.itemcode,b.itemname,b.itemdesc FROM TBLMO a ";
            sql += " left join TBLITEM b on a.itemcode = b.itemcode WHERE a.mocode = '" + moCode.Trim().ToUpper() + "' ";

            object[] objs = this.DataProvider.CustomQuery(typeof(CartonCollection), new SQLCondition(sql));

            if (objs != null)
            {
                return objs[0];
            }

            return null;

        }
        #endregion

        #region Carton2RCARDLog

        public Carton2RCARDLog CreateNewCarton2RCARDLog()
        {
            return new Carton2RCARDLog();
        }

        public void AddCarton2RCARDLog(Carton2RCARDLog carton2RCARDLog)
        {
            this._helper.AddDomainObject(carton2RCARDLog);
        }

        public void UpdateCarton2RCARDLog(Carton2RCARDLog carton2RCARDLog)
        {
            this._helper.UpdateDomainObject(carton2RCARDLog);
        }

        public void DeleteCarton2RCARDLog(Carton2RCARDLog carton2RCARDLog)
        {
            this._helper.DeleteDomainObject(carton2RCARDLog);
        }

        public void DeleteCarton2RCARDLog(Carton2RCARDLog[] carton2RCARDLog)
        {
            this._helper.DeleteDomainObject(carton2RCARDLog);
        }

        public object GetCarton2RCARDLog(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(Carton2RCARDLog), new object[] { serial });
        }

        public object GetCarton2RCARDLog(string cartonCode, string rcard)
        {
            string sql = " SELECT * FROM TBLCarton2RCARDLog WHERE 1=1 ";

            if (cartonCode.Trim() != string.Empty)
            {
                sql += " AND cartonno='" + cartonCode.Trim().ToUpper() + "' ";
            }

            if (rcard.Trim() != string.Empty)
            {
                sql += " AND rcard='" + rcard.Trim().ToUpper() + "' ";
            }

            sql += " ORDER BY packdate DESC,packtime DESC";

            object[] getObjects = this.DataProvider.CustomQuery(typeof(Carton2RCARDLog), new SQLCondition(sql));

            if (getObjects != null)
            {
                return getObjects[0];
            }

            return null;
        }

        public void addCarton2RCARDLog(string cartonCode, string rcard, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            Carton2RCARDLog carton2RCARDLog = this.CreateNewCarton2RCARDLog();

            carton2RCARDLog.Serial = 0;
            carton2RCARDLog.CartonCode = cartonCode;
            carton2RCARDLog.Rcard = rcard;
            carton2RCARDLog.PackUser = userCode;
            carton2RCARDLog.PackDate = dbDateTime.DBDate;
            carton2RCARDLog.PackTime = dbDateTime.DBTime;

            this.AddCarton2RCARDLog(carton2RCARDLog);
        }

        public void SaveRemoveCarton2RCARDLog(string cartonCode, string rcard, string userCode)
        {
            if (cartonCode.Trim().Length <= 0 || rcard.Trim().Length <= 0)
            {
                return;
            }

            Carton2RCARDLog carton2RCARDLog = (Carton2RCARDLog)this.GetCarton2RCARDLog(cartonCode, rcard);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (carton2RCARDLog != null && string.IsNullOrEmpty(carton2RCARDLog.RemoveUser) && carton2RCARDLog.RemovDate == 0)
            {
                carton2RCARDLog.RemoveUser = userCode;
                carton2RCARDLog.RemovDate = dbDateTime.DBDate;
                carton2RCARDLog.RemovTime = dbDateTime.DBTime;

                this.UpdateCarton2RCARDLog(carton2RCARDLog);
            }
            else
            {
                Carton2RCARDLog newCarton2RCARDLog = this.CreateNewCarton2RCARDLog();

                newCarton2RCARDLog.Serial = 0;
                newCarton2RCARDLog.CartonCode = cartonCode;
                newCarton2RCARDLog.Rcard = rcard;
                newCarton2RCARDLog.RemoveUser = userCode;
                newCarton2RCARDLog.RemovDate = dbDateTime.DBDate;
                newCarton2RCARDLog.RemovTime = dbDateTime.DBTime;

                this.AddCarton2RCARDLog(newCarton2RCARDLog);
            }
        }
        #endregion

        #region Carton2Lot

        public Carton2Lot CreateNewCarton2Lot()
        {
            return new Carton2Lot();
        }

        public void AddCarton2Lot(Carton2Lot carton2Lot)
        {
            this._helper.AddDomainObject(carton2Lot);
        }

        public void UpdateCarton2Lot(Carton2Lot carton2Lot)
        {
            this._helper.UpdateDomainObject(carton2Lot);
        }

        public void DeleteCarton2Lot(Carton2Lot carton2Lot)
        {
            this._helper.DeleteDomainObject(carton2Lot);
        }

        public void DeleteCarton2Lot(Carton2Lot[] carton2Lot)
        {
            this._helper.DeleteDomainObject(carton2Lot);
        }

        public object GetCarton2Lot(string cartonCode, string lotCode)
        {
            return this.DataProvider.CustomSearch(typeof(Carton2Lot), new object[] { cartonCode, lotCode });
        }

        public void AddCarton2Lot(string cartonCode, string lotCode, decimal cartonQty, string userCode, string moCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            Carton2Lot carton2Lot = this.CreateNewCarton2Lot();

            carton2Lot.CartonCode = cartonCode;
            carton2Lot.LotCode = lotCode;
            carton2Lot.CartonQty = cartonQty;
            carton2Lot.MOCode = moCode;
            carton2Lot.MUser = userCode;
            carton2Lot.MDate = dbDateTime.DBDate;
            carton2Lot.MTime = dbDateTime.DBTime;

            this.AddCarton2Lot(carton2Lot);
        }

        public object[] GetCarton2LotByCartonNO(string cartonCode)
        {
            string sql = " SELECT * FROM TBLCarton2Lot WHERE 1=1 AND cartonno='" + cartonCode.Trim().ToUpper() + "' ";

            sql += " ORDER BY cartonno DESC,lotCode asc";

            object[] objects = this.DataProvider.CustomQuery(typeof(Carton2Lot), new SQLCondition(sql));

            return objects;

        }

        public object[] GetCarton2LotByLotCode(string lotCode)
        {
            string sql = " SELECT * FROM TBLCarton2Lot WHERE 1=1 AND lotcode='" + lotCode.Trim().ToUpper() + "' ";

            sql += " ORDER BY lotcode DESC,cartonno asc";

            return this.DataProvider.CustomQuery(typeof(Carton2Lot), new SQLCondition(sql));
        }

        public decimal SumCartonQty(string lotCode)
        {
            string sql = "SELECT NVL(SUM(CARTONQTY), 0) AS CARTONQTY FROM TBLCARTON2LOT";
            sql += " WHERE LOTCODE = '" + FormatHelper.CleanString(lotCode.Trim().ToUpper()) + "'";
            object[] objs = this.DataProvider.CustomQuery(typeof(Carton2Lot), new SQLCondition(sql));
            return (objs[0] as Carton2Lot).CartonQty;
        }

        public int GetMoCountByCartonNo(string cartonCode)
        {
            string sql = "SELECT COUNT(*) FROM ( SELECT DISTINCT  MOCODE FROM TBLCARTON2LOT WHERE CARTONNO = '" + cartonCode + "')";

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }


        #endregion

        #region Carton2LotLog

        public Carton2LotLog CreateNewCarton2LotLog()
        {
            return new Carton2LotLog();
        }

        public void AddCarton2LotLog(Carton2LotLog carton2LotLog)
        {
            this._helper.AddDomainObject(carton2LotLog);
        }

        public void UpdateCarton2LotLog(Carton2LotLog carton2LotLog)
        {
            this._helper.UpdateDomainObject(carton2LotLog);
        }

        public void DeleteCarton2LotLog(Carton2LotLog carton2LotLog)
        {
            this._helper.DeleteDomainObject(carton2LotLog);
        }

        public void DeleteCarton2LotLog(Carton2LotLog[] carton2LotLog)
        {
            this._helper.DeleteDomainObject(carton2LotLog);
        }

        public object GetCarton2LotLog(int serial)
        {
            return this.DataProvider.CustomSearch(typeof(Carton2LotLog), new object[] { serial });
        }

        public object GetCarton2LotLog(string cartonCode, string lotCode)
        {
            string sql = " SELECT * FROM TBLCarton2LotLog WHERE 1=1 ";

            if (cartonCode.Trim() != string.Empty)
            {
                sql += " AND cartonno='" + cartonCode.Trim().ToUpper() + "' ";
            }

            if (lotCode.Trim() != string.Empty)
            {
                sql += " AND lotcode='" + lotCode.Trim().ToUpper() + "' ";
            }

            sql += " ORDER BY packdate DESC,packtime DESC";

            object[] getObjects = this.DataProvider.CustomQuery(typeof(Carton2LotLog), new SQLCondition(sql));

            if (getObjects != null)
            {
                return getObjects[0];
            }

            return null;
        }

        public void AddCarton2LotLog(string cartonCode, string lotCode, decimal cartonQty, string userCode)
        {
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);
            Carton2LotLog carton2LotLog = this.CreateNewCarton2LotLog();

            carton2LotLog.Serial = 0;
            carton2LotLog.CartonCode = cartonCode;
            carton2LotLog.LotCode = lotCode;
            carton2LotLog.CartonQty = cartonQty;
            carton2LotLog.PackUser = userCode;
            carton2LotLog.PackDate = dbDateTime.DBDate;
            carton2LotLog.PackTime = dbDateTime.DBTime;

            this.AddCarton2LotLog(carton2LotLog);
        }

        public void SaveRemoveCarton2LotLog(string cartonCode, string lotCode, decimal cartonQty, string userCode)
        {
            if (cartonCode.Trim().Length <= 0 || lotCode.Trim().Length <= 0)
            {
                return;
            }

            Carton2LotLog carton2LotLog = (Carton2LotLog)this.GetCarton2LotLog(cartonCode, lotCode);
            DBDateTime dbDateTime = FormatHelper.GetNowDBDateTime(this.DataProvider);

            if (carton2LotLog != null && string.IsNullOrEmpty(carton2LotLog.RemoveUser) && carton2LotLog.RemovDate == 0)
            {
                carton2LotLog.RemoveUser = userCode;
                carton2LotLog.RemovDate = dbDateTime.DBDate;
                carton2LotLog.RemovTime = dbDateTime.DBTime;

                this.UpdateCarton2LotLog(carton2LotLog);
            }
            else
            {
                Carton2LotLog newCarton2LotLog = this.CreateNewCarton2LotLog();

                newCarton2LotLog.Serial = 0;
                newCarton2LotLog.CartonCode = cartonCode;
                newCarton2LotLog.LotCode = lotCode;
                newCarton2LotLog.CartonQty = cartonQty;
                newCarton2LotLog.RemoveUser = userCode;
                newCarton2LotLog.RemovDate = dbDateTime.DBDate;
                newCarton2LotLog.RemovTime = dbDateTime.DBTime;

                this.AddCarton2LotLog(newCarton2LotLog);
            }
        }
        #endregion
        //Add by Sandy on 20130605
        public object GetCarton2RcardByRcard(string rcard)
        {
            string sql = string.Format(@" SELECT {0} FROM Tblcarton2rcard WHERE 1=1", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Carton2RCARD)));
            if (!string.IsNullOrEmpty(rcard))
            {
                sql += string.Format(@" AND  RCARD='{0}' ", rcard);
            }
            object[] objs = this.DataProvider.CustomQuery(typeof(Carton2RCARD), new SQLCondition(sql));
            if (objs != null)
            {
                return objs[0];
            }
            return null;
        }

    }
}

