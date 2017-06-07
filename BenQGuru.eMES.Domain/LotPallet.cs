using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for LotPackage
/// ** 作 者:		Jarvis
/// ** 日 期:		2012-4-23 14:28:17
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.LotPackage
{


        #region
        
        #endregion

        #region Pallet2Lot
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLPALLET2Lot", "PALLETCODE,LOTCODE")]
        public class Pallet2Lot : DomainObject
        {
            public Pallet2Lot()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PALLETCODE", typeof(string), 40, false)]
            public string PalletCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
            public string LotCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MUSER", typeof(string), 40, false)]
            public string MaintainUser;

           
            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MDATE", typeof(int), 8, false)]
            public int MaintainDate;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MTIME", typeof(int), 6, false)]
            public int MaintainTime;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, false)]
            public string EAttribute1;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
            public string MOCode;

        }
        #endregion

        #region  Pallet2LotLog
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLPALLET2LOTLOG", "SERIAL")]
        public class Pallet2LotLog : DomainObject
        {
            public Pallet2LotLog()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("SERIAL", typeof(int), 38, false)]
            public int Serial;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PALLETCODE", typeof(string), 40, false)]
            public string PalletCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
            public string LotCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PACKUSER", typeof(string), 40, true)]
            public string PackUser;


            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PACKDATE", typeof(int), 8, true)]
            public int PackDate;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PACKTIME", typeof(int), 6, true)]
            public int PackTime;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("REMOVEUSER", typeof(string), 40, true)]
            public string RemoveUser;


            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("REMOVDATE", typeof(int), 8, true)]
            public int RemovDate;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("REMOVTIME", typeof(int), 6, false)]
            public int RemovTime;

        }
        #endregion

        #region  Carton2Lot
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLCarton2Lot", "CARTONNO,LOTCODE")]
        public class Carton2Lot : DomainObject
        {
            public Carton2Lot()
            {
            }
            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
            public string CartonCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
            public string LotCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("CARTONQTY", typeof(decimal), 22, false)]
            public decimal CartonQty;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
            public string MOCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("EATTRIBUTE1", typeof(string), 40, true)]
            public string Eattribute1;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MUSER", typeof(string), 40, false)]
            public string MUser;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MDATE", typeof(int), 8, false)]
            public int MDate;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MTIME", typeof(int), 6, false)]
            public int MTime;


        }
        #endregion

        #region  Carton2LotLog
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLCarton2LotLog", "SERIAL")]
        public class Carton2LotLog : DomainObject
        {
            public Carton2LotLog()
            {
            }

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("SERIAL", typeof(decimal), 38, false)]
            public decimal Serial;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
            public string CartonCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("LOTCODE", typeof(string), 40, false)]
            public string LotCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("CARTONQTY", typeof(decimal), 22, false)]
            public decimal CartonQty;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PACKUSER", typeof(string), 40, true)]
            public string PackUser;


            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PACKDATE", typeof(int), 8, true)]
            public int PackDate;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("PACKTIME", typeof(int), 6, true)]
            public int PackTime;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("REMOVEUSER", typeof(string), 40, true)]
            public string RemoveUser;


            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("REMOVDATE", typeof(int), 8, true)]
            public int RemovDate;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("REMOVTIME", typeof(int), 6, false)]
            public int RemovTime;

        }
        #endregion

}
