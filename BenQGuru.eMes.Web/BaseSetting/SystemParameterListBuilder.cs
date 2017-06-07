using System;
using BenQGuru.eMES.Common.Domain ;
using BenQGuru.eMES.Common;
using BenQGuru.eMES.BaseSetting;
using BenQGuru.eMES.Domain.BaseSetting;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.Web.BaseSetting
{
	/// <summary>
	/// SystemParameterListBuilder 的摘要说明。
	/// </summary>
	public class SystemParameterListBuilder
	{
		private string _paramGroupCode = null;
		private string _orderbyColumn = string.Empty;
		private IDomainDataProvider _dataProvider = null ;

		public SystemParameterListBuilder(string paramGroupCode, IDomainDataProvider dataProvider)
		{
			this._paramGroupCode = paramGroupCode.ToUpper();
			
			_dataProvider = dataProvider ;
			
		}

		public SystemParameterListBuilder(string paramGroupCode,string orderbyColumn, IDomainDataProvider dataProvider)
		{
			this._paramGroupCode = paramGroupCode.ToUpper();
			this._orderbyColumn = orderbyColumn.ToUpper();
			
			_dataProvider = dataProvider ;
			
		}

		public void Build(System.Web.UI.WebControls.DropDownList drpList)
		{
			if ( drpList == null)
			{
				ExceptionManager.Raise(this.GetType(),"$Error_Argument_Null","DropDownList");
			}

			DropDownListBuilder builder = new DropDownListBuilder(drpList);
			builder.HandleGetObjectList = new GetObjectListDelegate( this.GetParamerters );

			builder.Build("ParameterCode","ParameterCode");
		}

		private object[] GetParamerters()
		{
			return new SystemSettingFacadeFactory(_dataProvider).Create().GetParametersByParameterGroup(_paramGroupCode,_orderbyColumn);
		}

        public void BuildShowDescription(System.Web.UI.WebControls.DropDownList drpList)
        {

            if (drpList == null)
            {
                ExceptionManager.Raise(this.GetType(), "$Error_Argument_Null", "DropDownList");
            }

          
            DropDownListBuilder builder = new DropDownListBuilder(drpList);
            builder.HandleGetObjectList = new GetObjectListDelegate(this.GetParamerters);

            builder.Build("ParameterDescription", "ParameterCode");
        }
	}
}
