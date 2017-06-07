using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenQGuru.eMES.Domain;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;

namespace BenQGuru.eMES.BaseSetting
{
    public  class EsopPicsNGFacade : MarshalByRefObject
    {
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public EsopPicsNGFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
        }

        public EsopPicsNGFacade(IDomainDataProvider domainDataProvider)
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

        #region Esoppicsng
        /// <summary>
        /// Esoppicsng
        /// </summary>
        public Esoppicsng CreateNewEsoppicsng()
        {
            return new Esoppicsng();
        }

        public void AddEsoppicsng(Esoppicsng esoppicsng)
        {
            this.DataProvider.Insert(esoppicsng);
        }

        public void DeleteEsoppicsng(Esoppicsng esoppicsng)
        {
            this.DataProvider.Delete(esoppicsng);
        }

        public void UpdateEsoppicsng(Esoppicsng esoppicsng)
        {
            this.DataProvider.Update(esoppicsng);
        }

        public object GetEsoppicsng(string Serial)
        {
            return this.DataProvider.CustomSearch(typeof(Esoppicsng), new object[] { Serial });
        }
        #endregion

        public object[] GetEsoppicNGNextSerial()
        {
            string sqlStr = " SELECT  seq_TBLESOPPICSNG.nextval SERIAL FROM dual ";

            return this.DataProvider.CustomQuery(typeof(Esoppicsng), new SQLCondition(sqlStr));
        }

        public object[] QueryEsopPicsNG(string rcard, string tsid)
        {
            string sqlStr = string.Format("select {0} from tblesoppicsng picng where 1=1 ", DomainObjectUtility.GetDomainObjectFieldsString(typeof(Esoppicsng)));
            if (!string.IsNullOrEmpty(rcard))
            {
                sqlStr += " and  picng.rcard= '" + rcard.Trim() + "'";
            }
            if (!string.IsNullOrEmpty(tsid))
            {
                sqlStr += " and picng.tsid ='" + tsid.Trim() + "'";
            }
            sqlStr += " ORDER BY picng.serial  ";
            return this.DataProvider.CustomQuery(typeof(Esoppicsng), new SQLCondition(sqlStr));
        }
    }


}
