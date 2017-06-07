using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Domain.KPI
{
    #region
    /// <summary>
    /// tblres
    /// </summary>

    public class Output : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public Output()
        {
        }
        ///<summary>
        ///outputsum
        ///</summary>
        [FieldMapAttribute("OUTPUT", typeof(int), 22, true)]
        public int outputsum;


    }
    #endregion

    #region
    public class EndDateEn : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public EndDateEn()
        {
        }
        ///<summary>
        ///ENDDATE
        ///</summary>
        [FieldMapAttribute("ENDDATE", typeof(DateTime), 22, true)]
        public DateTime enddate;
    }
    #endregion

    #region
    public class BeginDateEn : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public BeginDateEn()
        {
        }
        ///<summary>
        ///firstoutputtime
        ///</summary>
        [FieldMapAttribute("BEGINDATE", typeof(DateTime), 22, true)]
        public DateTime beigindate;
    }

    #endregion



    #region
    public class InAndOut : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public InAndOut()
        {
        }
        ///<summary>
        ///inandout
        ///</summary>
        [FieldMapAttribute("inandout", typeof(string), 22, true)]
        public string inandout;
    }
    #endregion

    #region
    public class RealRate : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public RealRate()
        {
        }
        ///<summary>
        ///rate
        ///</summary>
        [FieldMapAttribute("RATE", typeof(string), 22, true)]
        public string rate;
    }
    #endregion

    #region 人均生产数
    public class PerCapitaOutput : BenQGuru.eMES.Common.Domain.DomainObject
    {
        public PerCapitaOutput()
        {
        }
        ///<summary>
        ///sscode
        ///</summary>
        [FieldMapAttribute("SSCODE", typeof(string), 40, true)]
        public string sscode;

        ///<summary>
        ///itemcode
        ///</summary>
        [FieldMapAttribute("ITEMCODE", typeof(string), 40, true)]
        public string itemcode;

        ///<summary>
        ///acq
        ///</summary>
        [FieldMapAttribute("ACQ", typeof(string), 40, true)]
        public string acq;

        ///<summary>
        ///act
        ///</summary>
        [FieldMapAttribute("ACT", typeof(string), 40, true)]
        public string act;

        ///<summary>
        ///wor
        ///</summary>
        [FieldMapAttribute("WOR", typeof(string), 40, true)]
        public string wor;

        ///<summary>
        ///datetime
        ///</summary>
        [FieldMapAttribute("DATETIME", typeof(string), 40, true)]
        public string datetime;

        ///<summary>
        ///per
        ///</summary>
        [FieldMapAttribute("PER", typeof(string), 40, true)]
        public string per;
    }
    #endregion
}
