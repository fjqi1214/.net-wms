using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.CodeSoftPrint
{
    public class LabelPrintVars
    {
        private string[] _labelVars_No2 = new string[5];
        private string[] _labelVars_No3 = new string[5];
        private string[] _labelValues_No2 = new string[5];
        private string[] _labelValues_No3 = new string[5];

        public LabelPrintVars()
        {
            _labelVars_No2[0] = "VAR30";
            _labelVars_No2[1] = "VAR31";
            _labelVars_No2[2] = "VAR32";
            _labelVars_No2[3] = "VAR33";
            _labelVars_No2[4] = "VAR34";

            _labelVars_No3[0] = "VAR35";
            _labelVars_No3[1] = "VAR36";
            _labelVars_No3[2] = "VAR37";
            _labelVars_No3[3] = "VAR38";
            _labelVars_No3[4] = "VAR39";

            _labelValues_No2[0] = "";
            _labelValues_No2[1] = "";
            _labelValues_No2[2] = "";
            _labelValues_No2[3] = "";
            _labelValues_No2[4] = "";

            _labelValues_No3[0] = "";
            _labelValues_No3[1] = "";
            _labelValues_No3[2] = "";
            _labelValues_No3[3] = "";
            _labelValues_No3[4] = "";

        }

        public string[] LabelVars_No2
        {
            get
            {
                return _labelVars_No2;
            }
        }
        public string[] LabelVars_No3
        {
            get
            {
                return _labelVars_No3;
            }
        }

        public string[] LabelValues_No2
        {
            get
            {
                return _labelValues_No2;
            }
        }
        public string[] LabelValues_No3
        {
            get
            {
                return _labelValues_No3;
            }
        }
    }

}

