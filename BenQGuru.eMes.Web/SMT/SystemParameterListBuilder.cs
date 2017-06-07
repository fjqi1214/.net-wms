#region System;
using System;
using System.Web.UI.WebControls;
#endregion


#region project
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common.MutiLanguage;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain ;
#endregion


namespace BenQGuru.eMES.Web.SMT
{
    /// <summary>
    /// SystemParameterListBuilder 的摘要说明。
    /// </summary>
    public class SystemParameterListBuilder
    {
        private string _paramGroupCode = null;
		private System.Web.UI.WebControls.DropDownList _drpList = null;
		private IDomainDataProvider _dataProvider = null;

        public SystemParameterListBuilder(string paramGroupCode,IDomainDataProvider dataProvider)
        {
            this._paramGroupCode = paramGroupCode;
			this._dataProvider = dataProvider ;
        }

        public void Build(System.Web.UI.WebControls.DropDownList drpList)
        {
            if ( drpList == null)
            {
                throw new ArgumentNullException("drpList");
            }

			this._drpList = drpList;

            DropDownListBuilder builder = new DropDownListBuilder(drpList);
            builder.HandleGetObjectList = new GetObjectListDelegate( this.GetParamerters );

            builder.Build("ParameterAlias", "ParameterCode");
        }

        private object[] GetParamerters()
        {
			//BenQGuru.eMES.Common.Domain.IDomainDataProvider _domainDataProvider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();     
			BenQGuru.eMES.BaseSetting.SystemSettingFacade systemFacade = new SystemSettingFacade(_dataProvider);
            object[] returnobjs= systemFacade.GetParametersByParameterGroup(_paramGroupCode);
			return returnobjs;
        }

		public void AddAllItem(ControlLibrary.Web.Language.LanguageComponent languageControl)
		{
			LanguageWord lword  = languageControl.GetLanguage("listItemAll");

			if ( lword != null )
			{
				this._drpList.Items.Insert(0, new ListItem(lword.ControlText, ""));
			}
			else
			{
				this._drpList.Items.Insert(0, new ListItem("", ""));
			}
		}
    }
}
