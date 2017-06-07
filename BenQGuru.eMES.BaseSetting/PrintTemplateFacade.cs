using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using BenQGuru.eMES.Common;
using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.Helper;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Domain.DataCollect;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Domain.Material;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Domain.Rework;
using BenQGuru.eMES.Domain.Warehouse;
using BenQGuru.eMES.Material;

namespace BenQGuru.eMES.BaseSetting
{
    public class PrintTemplateFacade : MarshalByRefObject
    {
        #region dataprovider
        private IDomainDataProvider _domainDataProvider = null;
        private FacadeHelper _helper = null;

        public PrintTemplateFacade(IDomainDataProvider domainDataProvider)
        {
            this._domainDataProvider = domainDataProvider;
            this._helper = new FacadeHelper(DataProvider);
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public PrintTemplateFacade()
        {
            this._helper = new FacadeHelper(DataProvider);
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
        #endregion

        #region PrintTemplate
        /// <summary>
        /// ±êÇ©Ä£°å´úÂë
        /// </summary>
        public PrintTemplate CreateNewPrintTemplate()
        {
            return new PrintTemplate();
        }

        public void AddPrintTemplate(PrintTemplate printTemplate)
        {
            this._helper.AddDomainObject(printTemplate);
        }

        public void UpdatePrintTemplate(PrintTemplate printTemplate)
        {
            this._helper.UpdateDomainObject(printTemplate);
        }

        public void DeletePrintTemplate(PrintTemplate printTemplate)
        {
            this._helper.DeleteDomainObject(printTemplate);
        }

        public void DeletePrintTemplate(PrintTemplate[] printTemplate)
        {
            this._helper.DeleteDomainObject(printTemplate);
        }

        public object GetPrintTemplate(string templateName)
        {
            return this.DataProvider.CustomSearch(typeof(PrintTemplate), new object[] { templateName });
        }

        public int QueryPrintTemplateCount(string templateName, string templateDesc)
        {
            string sql = "SELECT COUNT(*) FROM tblprinttemplate WHERE 1=1 ";

            if (templateName != null && templateName.Trim().Length > 0)
            {
                sql += " AND templatename like '" + templateName.Trim().ToUpper() + "%' ";
            }

            if (templateDesc != null && templateDesc.Trim().Length > 0)
            {
                sql += " AND templatedesc like '" + templateDesc.Trim().ToUpper() + "%' ";
            }

            return this.DataProvider.GetCount(new SQLCondition(sql));
        }

        public object[] QueryPrintTemplate(string templateName, string templateDesc, int inclusive, int exclusive)
        {

            string sql = "SELECT " + DomainObjectUtility.GetDomainObjectFieldsString(typeof(PrintTemplate)) + " FROM tblprinttemplate WHERE 1=1 ";

            if (templateName != null && templateName.Trim().Length > 0)
            {
                sql += " AND templatename like '" + templateName.Trim().ToUpper() + "%' ";
            }

            if (templateDesc != null && templateDesc.Trim().Length > 0)
            {
                sql += " AND templatedesc like '" + templateDesc.Trim().ToUpper() + "%' ";
            }

            return this.DataProvider.CustomQuery(typeof(PrintTemplate), new PagerCondition(sql, "templatename", inclusive, exclusive));
        }

        #endregion
    }
}
