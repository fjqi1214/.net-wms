using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace BenQGuru.eMES.Web.WebQuery
{
    public class ReportGridDim3Property
    {
        public ReportGridDim3Property(string name, string format, string endColumnFomular, string bottemRowFomular, bool hidden)
        {
            _Name = name;
            _Format = format;
            _EndColumnFomular = endColumnFomular;
            _BottemRowFomular = bottemRowFomular;
            _Hidden = hidden;
        }

        private string _Name = string.Empty;
        private string _Format = string.Empty;
        private string _EndColumnFomular = string.Empty;
        private string _BottemRowFomular = string.Empty;
        private bool _Hidden = false;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }

        public string EndColumnFomular
        {
            get { return _EndColumnFomular; }
            set { _EndColumnFomular = value; }
        }

        public string BottemRowFomular
        {
            get { return _BottemRowFomular; }
            set { _BottemRowFomular = value; }
        }

        public bool Hidden
        {
            get { return _Hidden; }
            set { _Hidden = value; }
        }


    }
}
