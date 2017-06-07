using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.SopPicture;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Domain;

namespace BenQGuru.eMES.BaseSetting
{
    public class EsopPicsFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public EsopPicsFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public EsopPicsFacade(IDomainDataProvider domainDataProvider)
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

        #region Esoppics
        /// <summary>
        /// Esoppics
        /// </summary>
        public Esoppics CreateNewEsoppics()
        {
            return new Esoppics();
        }

        public void AddEsoppics(Esoppics esoppics)
        {
            this.DataProvider.Insert(esoppics);
        }

        public void DeleteEsoppics(Esoppics esoppics)
        {
            this.DataProvider.Delete(esoppics);
        }
        //删除产品: 先删除产品机种关系,再删除产品,作事务 modify by Simone
        public void DeleteEsoppics(Esoppics[] pics)
        {
            this.DataProvider.BeginTransaction();
            try
            {
                foreach (Esoppics pic in pics)
                {
                    DeleteEsoppics(pic);
                }
                this.DataProvider.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.DataProvider.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateEsoppics(Esoppics esoppics)
        {
            this.DataProvider.Update(esoppics);
        }

        public object GetEsoppics(string Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Esoppics), new object[] { Serial });
        }
        #endregion 

        public int QueryEsopPicsCount(string itemCode, string opCode)
        {

            string sqlStr = "select count(*) from TBLESOPPICS pic where 1=1  ";

            if ((itemCode != string.Empty) && (itemCode.Trim() != string.Empty))
            {
                sqlStr += string.Format(" AND pic.ITEMCODE in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }
            if ((opCode != string.Empty) && (opCode.Trim() != string.Empty))
            {
                sqlStr += string.Format(" AND pic.OPCODE in ({0})", FormatHelper.ProcessQueryValues(opCode));
            }

            return this.DataProvider.GetCount(new SQLCondition(sqlStr));

        }

        public object[] QueryEsopPics(string itemCode, string opCode, int inclusive, int exclusive)
        {
            string sqlStr = string.Format("select {0} from TBLESOPPICS pic where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Esoppics)));
            if (!string.IsNullOrEmpty(itemCode))
            {
                sqlStr += string.Format(" AND pic.ITEMCODE in ({0})", FormatHelper.ProcessQueryValues(itemCode));
            }
            if (!string.IsNullOrEmpty(opCode))
            {
                sqlStr += string.Format(" AND pic.OPCODE in ({0})", FormatHelper.ProcessQueryValues(opCode));
            }
            sqlStr += " ORDER BY PIC.itemcode,PIC.opcode,PIC.PICSEQ ";
            return this.DataProvider.CustomQuery(typeof(Esoppics), new PagerCondition(sqlStr, inclusive, exclusive));
        }

        public object[] QueryEsopPics(string itemCode, string opCode)
        {
            string sqlStr = string.Format("select {0} from TBLESOPPICS pic where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Esoppics)));
            if (!string.IsNullOrEmpty(itemCode))
            {
                sqlStr += " and pic.ITEMCODE ='" + itemCode.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(opCode))
            {
                sqlStr += " and pic.OPCODE ='" + opCode.Trim() + "'";
            }
            sqlStr += " and  pic.pictype like '%Operating_Instructions%'  ORDER BY PIC.PICSEQ ";
            return this.DataProvider.CustomQuery(typeof(Esoppics), new SQLCondition(sqlStr));
        }

        public object[] QueryEsopPicsByTS(string itemCode, string opCode)
        {
            string sqlStr = string.Format("select {0} from TBLESOPPICS pic where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Esoppics)));
            if (!string.IsNullOrEmpty(itemCode))
            {
                sqlStr += " and pic.ITEMCODE ='" + itemCode.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(opCode))
            {
                sqlStr += " and pic.OPCODE ='" + opCode.Trim() + "'";
            }
            sqlStr += " and  pic.pictype like '%Maintenance_instructions%' ORDER BY PIC.PICSEQ ";
            return this.DataProvider.CustomQuery(typeof(Esoppics), new SQLCondition(sqlStr));
        }

        public object[] CheckEsoppicExist(Esoppics esoppics)
        {
            string sqlStr = string.Format("select {0} from TBLESOPPICS pic where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Esoppics)));
            sqlStr += " and pic.ITEMCODE = '" + esoppics.Itemcode + "'";
            sqlStr += " and pic.OPCODE = '" + esoppics.Opcode + "' ";
            sqlStr += " and pic.PICSEQ = '" + esoppics.Picseq + "' ";
            return this.DataProvider.CustomQuery(typeof(Esoppics), new SQLCondition(sqlStr));
        }

        public object[] GetEsoppicNextSerial()
        {
            string sqlStr = " SELECT  seq_TBLESOPPICS.nextval SERIAL FROM dual ";

            return this.DataProvider.CustomQuery(typeof(Esoppics), new SQLCondition(sqlStr));
        }
    }
}


