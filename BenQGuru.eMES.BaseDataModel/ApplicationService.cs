using System;
using System.Collections.Generic;
using System.Text;

using BenQGuru.eMES.Common.Domain;
using BenQGuru.eMES.Common.DomainDataProvider;
using BenQGuru.eMES.Web.Helper;

namespace BenQGuru.eMES.BaseDataModel
{
    public class ApplicationService
    {
        private static IDomainDataProvider _dataProvider;
        public static IDomainDataProvider DataProvider
        {
            get
            {
                if (_dataProvider == null)
                {
                    //modified by carey.cheng on 2010-05-20 for muti db support
                    _dataProvider = DomainDataProviderManager.DomainDataProvider(BenQGuru.eMES.Common.MesEnviroment.LoginDB);
                    //end modified by carey.cheng on 2010-05-20 for muti db support
                }
                return _dataProvider;
            }

            set
            {
                _dataProvider = value;
            }
        }

        public static string LoginUserCode = "";

        public static string ConfigFile
        {
            get
            {
                string str = System.Windows.Forms.Application.StartupPath;
                if (str.EndsWith("\\") == false)
                    str = str + "\\";
                return str + "BaseDataMapping.xml";
            }
        }

        public static string TemplateFolder
        {
            get
            {
                string str = System.Windows.Forms.Application.StartupPath;
                if (str.EndsWith("\\") == false)
                    str = str + "\\";
                return str + "Template\\";
            }
        }

    }
}
