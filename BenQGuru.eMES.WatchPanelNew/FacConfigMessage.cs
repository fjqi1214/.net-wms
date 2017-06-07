using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.WatchPanelNew
{
    [Serializable()]
    public static class FacConfigMessage
    {
        private static string _CommonInfo = string.Empty;
        private static string _CommonText = string.Empty;

        public static string CommonInfo
        {
            get
            {
                return _CommonInfo;
            }
            set
            {
                _CommonInfo = value;
            }
        }

        public static string CommonText
        {
            get
            {
                return _CommonText;
            }
            set
            {
                _CommonText = value;
            }
        }
    }
}
