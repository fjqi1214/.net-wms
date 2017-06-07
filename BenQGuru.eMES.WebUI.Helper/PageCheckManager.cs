using System;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using BenQGuru.eMES.Web.Helper;
using BenQGuru.eMES.Web.SelectQuery;

namespace BenQGuru.eMES.Web.Helper
{
    /// <summary>
    /// PageCheckManager 的摘要说明。
    /// </summary>
    public class PageCheckManager : IPageCheck
    {
        private ArrayList _checkList = new ArrayList();

        public PageCheckManager()
        {
        }

        public void Add(ICheck check)
        {
            this._checkList.Add(check);
        }

        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }

        public bool Check()
        {
            bool isOK = true;
            foreach (IPageCheck check in this._checkList)
            {
                if (!check.Check())
                {
                    isOK = false;
                    this._checkMessage += check.CheckMessage + "\n";
                }
            }
            return isOK;
        }
    }

    public interface IPageCheck : ICheck
    {
        string CheckMessage
        {
            get;
        }
    }

    public class WebControlTextDistil
    {
        private static WebControlTextDistil s_distil = null;
        public static WebControlTextDistil Instance()
        {
            if (WebControlTextDistil.s_distil == null)
            {
                WebControlTextDistil.s_distil = new WebControlTextDistil();
            }
            return WebControlTextDistil.s_distil;
        }

        public string Distil(WebControl ctrl)
        {
            string text = "";
            if (ctrl is Label)
            {
                text = (ctrl as Label).Text.Trim();
            }
            else if (ctrl is CheckBox)
            {
                text = (ctrl as CheckBox).Text.Trim();
            }
            else if (ctrl is TextBox)
            {
                text = (ctrl as TextBox).Text.Trim();
            }
            else if (ctrl is ListControl)
            {
                text = (ctrl as ListControl).SelectedValue;
            }
            else if (ctrl is SelectSingletableTextBox)
            {
                text = (ctrl as SelectSingletableTextBox).Text;
            }
            else if (ctrl is SelectableTextBox)
            {
                text = (ctrl as SelectableTextBox).Text;
            }
            else if (ctrl is SelectableTextBox4POMaterial)
            {
                text = (ctrl as SelectableTextBox4POMaterial).Text;
            }
            else if (ctrl is SelectableTextBox4SS)
            {
                text = (ctrl as SelectableTextBox4SS).Text;
            }
            else if (ctrl is DropDownList)
            {
                text = (ctrl as DropDownList).SelectedValue;
            }
            return FormatHelper.CleanString(text);
        }
    }

    public class NullCheck
    {
        private static NullCheck s_NullCheck = null;
        public static NullCheck Instance()
        {
            if (NullCheck.s_NullCheck == null)
            {
                NullCheck.s_NullCheck = new NullCheck();
            }
            return NullCheck.s_NullCheck;
        }

        public bool Check(WebControl label, WebControl ctrl)
        {
            return Check(label, WebControlTextDistil.Instance().Distil(ctrl));
        }

        public bool Check(WebControl label, string text)
        {
            if (text.Trim() == string.Empty)
            {
                return false;
            }
            return true;
        }
    }

    public class LengthCheck : IPageCheck
    {
        private WebControl _label = null;
        private WebControl _ctrl = null;
        private int _maxLength = 0;
        private bool _checkNull = true;

        public LengthCheck(WebControl label, WebControl ctrl, int maxLength, bool checkNull)
        {
            this._label = label;
            this._ctrl = ctrl;
            this._maxLength = maxLength;
            this._checkNull = checkNull;
        }

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._label, this._ctrl))
                {
                    if (!WebInfoPublish.isUseDiv)
                    {                      
                        this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label));

                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage = string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\"> {0}</a>   $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }
                    return false;
                }
            }

            //Encoding encoder5 = Encoding.GetEncoding("GB2312");UTF-8
            Encoding encoder5 = Encoding.GetEncoding("UTF-8");//modify by jinger 20160128
            int length = encoder5.GetByteCount(WebControlTextDistil.Instance().Distil(this._ctrl));

            if (length > this._maxLength)
            {
                if (!WebInfoPublish.isUseDiv)
                {
                    this._checkMessage += string.Format("{0} $Error_Text_Too_Long", WebControlTextDistil.Instance().Distil(this._label));
                }
                else
                {
                    //使用DIV弹出，拼写标签，added by Gawain@20130905
                    this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Text_Too_Long", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);                  
                }
                return false;
            }
            return true;
        }



        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }
    }


    public class ExtraLengthChek : IPageCheck
    {
        private WebControl _label = null;
        private WebControl _ctrl = null;
        private int _maxLength = 0;
        private bool _checkNull = true;

        public ExtraLengthChek(WebControl label, WebControl ctrl, int maxLength, bool checkNull)
        {
            this._label = label;
            this._ctrl = ctrl;
            this._maxLength = maxLength;
            this._checkNull = checkNull;
        }

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._label, this._ctrl))
                {          
                    if (!WebInfoPublish.isUseDiv)
                    {
                        this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label));
                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }

                    return false;
                }
            }
            if (WebControlTextDistil.Instance().Distil(this._ctrl).Length != this._maxLength)
            {  
                if (!WebInfoPublish.isUseDiv)
                {
                    this._checkMessage += string.Format("{0} $Error_Text_NotRight", WebControlTextDistil.Instance().Distil(this._label));
                }
                else
                {
                    //使用DIV弹出，拼写标签，added by Gawain@20130905
                    this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Text_NotRight", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                }
                return false;
            }
            return true;
        }



        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }
    }
    public class DateCheck : IPageCheck
    {
        private WebControl _label = null;
        private string _date = "";
        private bool _checkNull = false;

        public DateCheck(WebControl label, string date, bool checkNull)
        {
            this._label = label;
            this._date = date;
            this._checkNull = checkNull;
        }

        #region IPageCheck 成员

        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }

        #endregion

        #region ICheck 成员

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._label, this._date))
                {
                    this._checkMessage = string.Format("{0} $Error_Date_Empty", WebControlTextDistil.Instance().Distil(this._label));
                    return false;
                }
            }

            try
            {
                if (this._date.Trim().Length > 0)
                {
                    DateTime temp = DateTime.Parse(this._date);
                }
            }
            catch
            {
                this._checkMessage = string.Format("{0} $Error_Format", WebControlTextDistil.Instance().Distil(this._label));
                return false;
            }

            return true;
        }

        #endregion
    }

    public class DateRangeCheck : IPageCheck
    {
        private WebControl _fromlabel = null;
        private WebControl _tolabel = null;
        private string _from = "";
        private string _to = "";
        private bool _checkNull = true;
        private int _minDateRange = 0;
        private int _maxDateRange = 0;

        public DateRangeCheck(WebControl label, string from, string to, bool checkNull)
            : this(label, from, label, to, checkNull)
        {
        }

        public DateRangeCheck(WebControl fromLabel, string from, WebControl toLabel, string to, bool checkNull)
            : this(fromLabel, from, toLabel, to, 0, System.Int32.MaxValue, checkNull)
        {
        }

        public DateRangeCheck(WebControl fromLabel, string from, WebControl toLabel, string to, int minDateRange, int maxDateRange, bool checkNull)
        {
            this._fromlabel = fromLabel;
            this._tolabel = toLabel;
            this._from = from;
            this._to = to;
            this._checkNull = checkNull;
            this._minDateRange = minDateRange;
            this._maxDateRange = maxDateRange;
        }

        #region IPageCheck 成员

        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }

        #endregion

        #region ICheck 成员

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._fromlabel, this._from))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._fromlabel));
                    return false;
                }
                if (!NullCheck.Instance().Check(this._tolabel, this._to))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._tolabel));
                    return false;
                }
            }

            if (this._from.Trim() != string.Empty && this._to.Trim() != string.Empty)
            {
                double range = System.DateTime.Parse(this._to).Subtract(System.DateTime.Parse(this._from)).TotalDays;

                if (range < this._minDateRange)
                {
                    if (this._fromlabel == this._tolabel)
                    {
                        this._checkMessage = string.Format("{0}$Error_From_Greater_Than_To", WebControlTextDistil.Instance().Distil(this._fromlabel));
                    }
                    else
                    {
                        this._checkMessage = string.Format("{0}$Error_Greater_Than {1}", WebControlTextDistil.Instance().Distil(this._fromlabel), WebControlTextDistil.Instance().Distil(this._tolabel));
                    }
                    return false;
                }
                if (range > this._maxDateRange)
                {
                    if (this._fromlabel == this._tolabel)
                    {
                        this._checkMessage = string.Format("{0} $Error_From_Greater_Than_To {1}", WebControlTextDistil.Instance().Distil(this._fromlabel), this._maxDateRange.ToString());
                    }
                    else
                    {
                        this._checkMessage = string.Format("$Error_Greater_Than_Today1 {0} $Error_Greater_Than_Today2", this._maxDateRange.ToString());
                    }
                    return false;
                }
            }

            return true;
        }

        #endregion
    }


    public class TimeRangeCheck : IPageCheck
    {
        private WebControl _fromlabel = null;
        private WebControl _tolabel = null;
        private string _from = "";
        private string _to = "";
        private bool _checkNull = true;

        public TimeRangeCheck(WebControl label, string from, string to, bool checkNull)
            : this(label, from, label, to, checkNull)
        {
        }

        public TimeRangeCheck(WebControl fromLabel, string from, WebControl toLabel, string to, bool checkNull)
        {
            this._fromlabel = fromLabel;
            this._tolabel = toLabel;
            this._from = from;
            this._to = to;
            this._checkNull = checkNull;
        }

        #region IPageCheck 成员

        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }

        #endregion

        #region ICheck 成员

        public bool Check()
        {

            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._fromlabel, this._from))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._fromlabel));
                    return false;
                }
                if (!NullCheck.Instance().Check(this._tolabel, this._to))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._tolabel));
                    return false;
                }
            }

            if (this._from.Trim() != string.Empty && this._to.Trim() != string.Empty)
            {
                if (FormatHelper.TOTimeInt(this._from) > FormatHelper.TOTimeInt(this._to))
                {
                    if (this._fromlabel == this._tolabel)
                    {
                        this._checkMessage = string.Format("{0} $Error_From_Greater_Than_To", WebControlTextDistil.Instance().Distil(this._fromlabel));
                    }
                    else
                    {
                        this._checkMessage = string.Format("{0} $Error_Greater_Than {1}", WebControlTextDistil.Instance().Distil(this._fromlabel), WebControlTextDistil.Instance().Distil(this._tolabel));
                    }
                    return false;
                }
            }

            return true;
        }

        #endregion
    }

    public class NumberCheck : IPageCheck
    {
        private WebControl _label = null;
        private WebControl _ctrl = null;
        private bool _checkNull = true;
        private string _checkMessage = "";
        private long _minValue;
        private long _maxValue;

        public NumberCheck(WebControl label, WebControl ctrl, bool checkNull)
            : this(label, ctrl, 0, System.Int32.MaxValue, checkNull)
        {
        }

        public NumberCheck(WebControl label, WebControl ctrl, long minValue, long maxValue, bool checkNull)
        {
            this._label = label;
            this._ctrl = ctrl;
            this._checkNull = checkNull;
            this._minValue = minValue;
            this._maxValue = maxValue;
        }

        #region IPageCheck 成员
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }
        #endregion

        #region ICheck 成员

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._label, this._ctrl))
                {
                    if (!WebInfoPublish.isUseDiv)
                    {
                        this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label));
                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }
                    return false;
                }
            }

            try
            {
                long num = long.Parse(WebControlTextDistil.Instance().Distil(this._ctrl));
                if (num > this._maxValue)
                {    
                    if (!WebInfoPublish.isUseDiv)
                    {
                        this._checkMessage = string.Format("{0} $Error_Number_TooGreat", WebControlTextDistil.Instance().Distil(this._label));
                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Number_TooGreat", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }

                    return false;
                }
                if (num < this._minValue)
                {
                    if (!WebInfoPublish.isUseDiv)
                    {
                        this._checkMessage = string.Format("{0} $Error_Number_TooLittle", WebControlTextDistil.Instance().Distil(this._label));
                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Number_TooLittle", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }
                    return false;
                }
            }
            catch (FormatException)
            {
                if (!WebInfoPublish.isUseDiv)
                {
                    this._checkMessage = string.Format("{0} $Error_Number_Format_Error", WebControlTextDistil.Instance().Distil(this._label));
                }
                else
                {
                    //使用DIV弹出，拼写标签，added by Gawain@20130905
                    this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Number_Format_Error", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                }

                return false;
            }
            catch (OverflowException)
            {

                if (!WebInfoPublish.isUseDiv)
                {
                    this._checkMessage = string.Format("{0} $Error_Number_Overflow", WebControlTextDistil.Instance().Distil(this._label));
                }
                else
                {
                    //使用DIV弹出，拼写标签，added by Gawain@20130905
                    this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $EError_Number_Overflow", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                }
                return false;
            }
            return true;
        }

        #endregion
    }


    public class DecimalCheck : IPageCheck
    {
        private WebControl _label = null;
        private WebControl _ctrl = null;
        private bool _checkNull = true;
        private string _checkMessage = "";
        private decimal _minValue;
        private decimal _maxValue;

        public DecimalCheck(WebControl label, WebControl ctrl, bool checkNull)
            : this(label, ctrl, System.Int32.MinValue, System.Int32.MaxValue, checkNull)
        {
        }

        public DecimalCheck(WebControl label, WebControl ctrl, decimal minValue, decimal maxValue, bool checkNull)
        {
            this._label = label;
            this._ctrl = ctrl;
            this._checkNull = checkNull;
            this._minValue = minValue;
            this._maxValue = maxValue;
        }

        #region IPageCheck 成员
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }
        #endregion

        #region ICheck 成员

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._label, this._ctrl))
                {
                    if (!WebInfoPublish.isUseDiv)
                    {
                        this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label));
                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }
                    return false;
                }
            }

            try
            {
                decimal num = System.Decimal.Parse(WebControlTextDistil.Instance().Distil(this._ctrl));
                if (num > this._maxValue)
                {
                    //this._checkMessage = "$Error_Number_TooGreat";
                    if (!WebInfoPublish.isUseDiv)
                    {
                        this._checkMessage = string.Format("{0} $Error_Number_TooGreat", WebControlTextDistil.Instance().Distil(this._label));
                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Number_TooGreat", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }
                    return false;
                }
                if (num < this._minValue)
                {
                    //this._checkMessage = "$Error_Number_TooLittle";
                    if (!WebInfoPublish.isUseDiv)
                    {
                        this._checkMessage = string.Format("{0} $Error_Number_TooLittle", WebControlTextDistil.Instance().Distil(this._label));
                    }
                    else
                    {
                        //使用DIV弹出，拼写标签，added by Gawain@20130905
                        this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Number_TooLittle", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                    }
                    return false;
                }
            }
            catch (FormatException)
            {
                if (!WebInfoPublish.isUseDiv)
                {
                    this._checkMessage = string.Format("{0} $Error_Number_Format_Error", WebControlTextDistil.Instance().Distil(this._label));
                }
                else
                {
                    //使用DIV弹出，拼写标签，added by Gawain@20130905
                    this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Number_Format_Error", WebControlTextDistil.Instance().Distil(this._label), this._ctrl.ID);
                }
                return false;
            }
            catch (OverflowException)
            {
                if (!WebInfoPublish.isUseDiv)
                {
                    this._checkMessage = string.Format("{0} $Error_Number_Overflow", WebControlTextDistil.Instance().Distil(this._label));
                }
                else
                {
                    //使用DIV弹出，拼写标签，added by Gawain@20130905
                    this._checkMessage += string.Format("<a href=\\'\\' onclick=\"return SetFocus(\\'#{1}\\');\" > {0}</a> $Error_Number_Overflow", WebControlTextDistil.Instance().Distil(this._label), this._label.ID);
                }
                return false;
            }
            return true;
        }

        #endregion
    }

    public class RangeCheck : IPageCheck
    {
        private WebControl _fromlabel = null;
        private WebControl _tolabel = null;
        private string _from = "";
        private string _to = "";
        private bool _checkNull = true;

        public RangeCheck(WebControl label, string from, string to, bool checkNull)
            : this(label, from, label, to, checkNull)
        {
        }

        public RangeCheck(WebControl fromLabel, string from, WebControl toLabel, string to, bool checkNull)
        {
            this._fromlabel = fromLabel;
            this._tolabel = toLabel;
            this._from = from;
            this._to = to;
            this._checkNull = checkNull;
        }

        #region IPageCheck 成员

        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }

        #endregion

        #region ICheck 成员

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._fromlabel, this._from))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._fromlabel));
                    return false;
                }
                if (!NullCheck.Instance().Check(this._tolabel, this._to))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._tolabel));
                    return false;
                }
            }

            if (this._from.Trim() != string.Empty && this._to.Trim() != string.Empty)
            {
                if (string.Compare(this._from, this._to, true) > 0)
                {
                    if (this._fromlabel == this._tolabel)
                    {
                        this._checkMessage = string.Format("{0} $Error_From_Greater_Than_To", WebControlTextDistil.Instance().Distil(this._fromlabel));
                    }
                    else
                    {
                        this._checkMessage = string.Format("{0} $Error_Greater_Than {1}", WebControlTextDistil.Instance().Distil(this._fromlabel), WebControlTextDistil.Instance().Distil(this._tolabel));
                    }
                    return false;
                }
            }

            return true;
        }

        #endregion
    }

    public class DecimalRangeCheck : IPageCheck
    {
        private WebControl _fromlabel = null;
        private WebControl _tolabel = null;
        private string _from = "";
        private string _to = "";
        private bool _checkNull = true;

        public DecimalRangeCheck(WebControl label, string from, string to, bool checkNull)
            : this(label, from, label, to, checkNull)
        {
        }

        public DecimalRangeCheck(WebControl fromLabel, string from, WebControl toLabel, string to, bool checkNull)
        {
            this._fromlabel = fromLabel;
            this._tolabel = toLabel;
            this._from = from;
            this._to = to;
            this._checkNull = checkNull;
        }

        #region IPageCheck 成员

        private string _checkMessage = "";
        public string CheckMessage
        {
            get
            {
                return this._checkMessage;
            }
        }

        #endregion

        #region ICheck 成员

        public bool Check()
        {
            if (this._checkNull)
            {
                if (!NullCheck.Instance().Check(this._fromlabel, this._from))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._fromlabel));
                    return false;
                }
                if (!NullCheck.Instance().Check(this._tolabel, this._to))
                {
                    this._checkMessage = string.Format("{0} $Error_Input_Empty", WebControlTextDistil.Instance().Distil(this._tolabel));
                    return false;
                }
            }

            decimal from = 0;
            decimal to = 0;
            try
            {
                from = System.Decimal.Parse(_from);
            }
            catch (FormatException)
            {
                this._checkMessage = string.Format("{0} $Error_Number_Format_Error", WebControlTextDistil.Instance().Distil(this._fromlabel));
                return false;
            }
            catch (OverflowException)
            {
                this._checkMessage = string.Format("{0} $Error_Number_Overflow", WebControlTextDistil.Instance().Distil(this._fromlabel));
                return false;
            }

            try
            {
                to = System.Decimal.Parse(_to);
            }
            catch (FormatException)
            {
                this._checkMessage = string.Format("{0} $Error_Number_Format_Error", WebControlTextDistil.Instance().Distil(this._tolabel));
                return false;
            }
            catch (OverflowException)
            {
                this._checkMessage = string.Format("{0} $Error_Number_Overflow", WebControlTextDistil.Instance().Distil(this._tolabel));
                return false;
            }

            if (from > to)
            {
                if (this._fromlabel == this._tolabel)
                {
                    this._checkMessage = string.Format("{0} $Error_From_Greater_Than_To", WebControlTextDistil.Instance().Distil(this._fromlabel));
                }
                else
                {
                    this._checkMessage = string.Format("{0} $Error_Greater_Than {1}", WebControlTextDistil.Instance().Distil(this._fromlabel), WebControlTextDistil.Instance().Distil(this._tolabel));
                }
                return false;
            }

            return true;
        }

        #endregion

 
    }
}
