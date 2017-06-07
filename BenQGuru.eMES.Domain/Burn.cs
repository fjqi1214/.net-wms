using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for Burn
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by Sandy Chen
/// ** 日 期:		2014-05-23 9:39:01
/// ** 修 改:
/// ** 日 期:qc
/// </summary>
namespace BenQGuru.eMES.Domain.Burn
{
    #region Simulation
    /// <summary>
    /// 
    /// </summary>
    [Serializable, TableMap("TBLBURNWIP", "RCARD,MOCODE")]
    public class BurnWip : DomainObject
    {
        /// <summary>
        /// 产品序列号
        /// </summary>
        [FieldMapAttribute("RCARD", typeof(string), 40, false)]
        public string RunningCard;

        /// <summary>
        /// 工单
        /// </summary>
        [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
        public string MOCode;

        /// <summary>
        /// 老化状态
        /// </summary>
        [FieldMapAttribute("STATUS", typeof(string), 40, false)]
        public string Status;

        /// <summary>
        /// 产品代码
        /// </summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
        public string ItemCode;

        /// <summary>
        /// 产线代码
        /// </summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string SsCode;

        /// <summary>
        /// 资源代码
        /// </summary>
        [FieldMapAttribute("RESCODE", typeof(string), 40, true)]
        public string ResCode;

        /// <summary>
        /// 时段代码
        /// </summary>
        [FieldMapAttribute("TPCODE", typeof(string), 40, true)]
        public string TpCode;

        /// <summary>
        /// 工作天
        /// </summary>
        [FieldMapAttribute("SHIFTDAY", typeof(int), 8, true)]
        public int ShiftDay;

        /// <summary>
        /// 老化开始日期
        /// </summary>
        [FieldMapAttribute("BURNINDATE", typeof(int), 8, true)]
        public int BurnInDate;

        /// <summary>
        /// 老化开始时间
        /// </summary>
        [FieldMapAttribute("BURNINTIME", typeof(int), 6, true)]
        public int BurnInTime;

        /// <summary>
        /// 老化结束日期
        /// </summary>
        [FieldMapAttribute("BURNOUTDATE", typeof(int), 8, true)]
        public int BurnOutDate;

        /// <summary>
        /// 老化结束时间
        /// </summary>
        [FieldMapAttribute("BURNOUTTIME", typeof(int), 6, true)]
        public int BurnOutTime;

        /// <summary>
        /// 老化预计结束日期
        /// </summary>
        [FieldMapAttribute("FORECASTOUTDATE", typeof(int), 8, true)]
        public int ForecastOutDate;

        /// <summary>
        /// 老化预计结束时间
        /// </summary>
        [FieldMapAttribute("FORECASTOUTTIME", typeof(int), 6, true)]
        public int ForecastOutTime;

        /// <summary>
        /// 维护人
        /// </summary>
        [FieldMapAttribute("MUSER", typeof(string), 40, false)]
        public string MaintainUser;

        /// <summary>
        /// 维护日期
        /// </summary>
        [FieldMapAttribute("MDATE", typeof(int), 8, false)]
        public int MaintainDate;

        /// <summary>
        /// 维护时间
        /// </summary>
        [FieldMapAttribute("MTIME", typeof(int), 6, false)]
        public int MaintainTime;

        /// <summary>
        /// 产品状态
        /// </summary>
        [FieldMapAttribute("PRODUCTSTATUS", typeof(string), 40, true)]
        public string ProductStatus;
    }
    #endregion
}