using System;
using BenQGuru.eMES.Common.Domain;

/// <summary>
/// ** 功能描述:	DomainObject for Package
/// ** 作 者:		ER/Studio Basic Macro Code Generation  Created by HIRO CHEN
/// ** 日 期:		2008-7-25 11:08:17
/// ** 修 改:
/// ** 日 期:
/// </summary>
namespace BenQGuru.eMES.Domain.Package
{


        #region Pallet
        /// <summary>
        /// 
        /// </summary>
        [Serializable, TableMap("TBLPALLET", "PALLETCODE")]
        public class Pallet : DomainObject
        {
            public Pallet()
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
            [FieldMapAttribute("RCARDCOUNT", typeof(int), 8, false)]
            public int RCardCount;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("CAPACITY", typeof(int), 8, false)]
            public int Capacity;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("MOCODE", typeof(string), 40, false)]
            public string MOCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("SSCODE", typeof(string), 40, false)]
            public string SSCode;
            
            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("ITEMCODE", typeof(string), 40, false)]
            public string ItemCode;
            
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
            [FieldMapAttribute("ORGID", typeof(int), 8, false)]
            public int OrganizationID;

            /// <summary>
            /// 资源代码
            /// </summary>
            [FieldMapAttribute("RESCODE", typeof(string), 40, false)]
            public string Rescode;

        }
        #endregion

        #region Pallet2RCard
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLPALLET2RCARD", "PALLETCODE,RCARD")]
        public class Pallet2RCard : DomainObject
        {
            public Pallet2RCard()
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
            [FieldMapAttribute("RCARD", typeof(string), 40, false)]
            public string RCard;

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

        #region  Pallet2RcardLog
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLPALLET2RCARDLOG", "SERIAL")]
        public class Pallet2RcardLog : DomainObject
        {
            public Pallet2RcardLog()
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
            [FieldMapAttribute("RCARD", typeof(string), 40, false)]
            public string Rcard;

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

        #region  Carton2RCARD
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLCarton2RCARD", "CARTONNO,RCARD")]
        public class Carton2RCARD : DomainObject
        {
            public Carton2RCARD()
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
            [FieldMapAttribute("RCARD", typeof(string), 40, false)]
            public string Rcard;

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

        #region  Carton2RCARDLog
        /// <summary>
        /// </summary>
        [Serializable, TableMap("TBLCarton2RCARDLog", "SERIAL")]
        public class Carton2RCARDLog : DomainObject
        {
            public Carton2RCARDLog()
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
            [FieldMapAttribute("CARTONNO", typeof(string), 40, false)]
            public string CartonCode;

            /// <summary>
            /// 
            /// </summary>
            [FieldMapAttribute("RCARD", typeof(string), 40, false)]
            public string Rcard;

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
