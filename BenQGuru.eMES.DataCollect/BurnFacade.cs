using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain.Burn;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.DataCollect
{
    /// <summary>
    /// BurnFacade 的摘要说明。
    /// 文件名:		BurnFacade.cs
    /// Copyright (c) 1999 -2003 明基逐鹿（BenQGuru）软件公司研发部
    /// 创建人:		ER/Studio Basic Macro Code Generation  Created by Sandy Chen
    /// 创建日期:	2014-5-28 10:29:35
    /// 修改人:
    /// 修改日期:
    /// 描 述:	
    /// 版 本:	
    /// </summary>
    public class BurnFacade
    {
        private IDomainDataProvider _domainDataProvider = null;

        public BurnFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
        }

        public IDomainDataProvider DataProvider
        {
            get
            {
                return _domainDataProvider;
            }
        }

        public void AddBurnWip(BurnWip burnWip)
        {
            this.DataProvider.Insert(burnWip);
        }

        public void UpdateBurnWip(BurnWip burnWip)
        {
            this.DataProvider.Update(burnWip);
        }

        public void DeleteBurnWip(BurnWip burnWip)
        {
            this.DataProvider.Delete(burnWip);
        }

        public object GetBurnWip(string runningCard, string moCode)
        {
            return this.DataProvider.CustomSearch(typeof(BurnWip), new object[] { runningCard, moCode });
        }
        /// <summary>
        /// 根据产线获取正在老化的产品信息
        /// </summary>
        /// <param name="ssCode"></param>
        /// <returns></returns>
        public object[] GetBurnBySscode(string ssCode)
        {
            object[] burnWip = this.DataProvider.CustomQuery(typeof(BurnWip),
                new SQLParamCondition(
                string.Format("select {0} from TBLBURNWIP where SSCODE = $SSCODE and status = '{1}' order by FORECASTOUTDATE,FORECASTOUTTIME",
                DomainObjectUtility.GetDomainObjectFieldsString(typeof(BurnWip)),BurnType.BurnIn),
                new SQLParameter[] { new SQLParameter("SSCODE", typeof(string), ssCode) }));

            if (burnWip == null || burnWip.Length == 0)
                return null;
            else
                return burnWip;
        }
    }
}
