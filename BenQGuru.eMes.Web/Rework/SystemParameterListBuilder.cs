using System;

using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Common ;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Common.Domain;

namespace BenQGuru.eMES.Web.Rework
{
	/// <summary>
	/// SystemParameterListBuilder 的摘要说明。
	/// </summary>
	public class SystemParameterListBuilder
	{
		private string _paramGroupCode = null;
		private IDomainDataProvider _dataProvider = null ;
		public SystemParameterListBuilder(string paramGroupCode,IDomainDataProvider dataProvider)
		{
			this._paramGroupCode = paramGroupCode;
			this._dataProvider = dataProvider ;
		}

		public void Build(System.Web.UI.WebControls.DropDownList drpList)
		{
			if ( drpList == null)
			{
                ExceptionManager.Raise( this.GetType() , "$Error_Argument_Null",drpList.ID) ;
			}

			DropDownListBuilder builder = new DropDownListBuilder(drpList);
			builder.HandleGetObjectList = new GetObjectListDelegate( this.GetParamerters );

			builder.Build("ParameterCode","ParameterCode");
		}

		private object[] GetParamerters()
		{
			//BenQGuru.eMES.Common.Domain.IDomainDataProvider _provider = BenQGuru.eMES.Common.DomainDataProvider.DomainDataProviderManager.DomainDataProvider();
            object[] reuturnObjs = new ReworkFacadeFactory( _dataProvider ).CreateSystemSettingFacade().GetParametersByParameterGroup(_paramGroupCode);
			return reuturnObjs;
		}
	}
}
