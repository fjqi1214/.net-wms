using System;
using System.Collections.Generic;
using System.Text;

namespace UserControl
{

    public delegate void WorkingErrorAddedEventHandler(object sender, WorkingErrorAddedEventArgs e);

    public class WorkingErrorAddedEventArgs : EventArgs
    {
        public WorkingErrorAddedEventArgs(string function, string inputContent, Message errorMessage, string errorText)
        {
            _Function = function;
            _InputContent = inputContent;
            _ErrorMessage = errorMessage;
            _ErrorText = errorText;
        }

        private string _Function = string.Empty;
        private string _InputContent = string.Empty;
        private Message _ErrorMessage = null;
        private string _ErrorText = string.Empty;        

        public string Function
        {
            get
            {
                return _Function;
            }
            set
            {
                _Function = value;
            }
        }

        public string InputContent
        {
            get
            {
                return _InputContent;
            }
            set
            {
                _InputContent = value;
            }
        }

        public Message ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
            }
        }

        public string ErrorText
        {
            get
            {
                return _ErrorText;
            }
            set
            {
                _ErrorText = value;
            }
        }
    }
}
