using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.DataExp
{
    public class DataExpObject
    {
        public string Name;
        public string Type;
        public string FilePath;
        public string SeparateChar;
        public bool NeedTitle;
        public DataExpFileName FileName;

        public List<DataExpField> FieldList = new List<DataExpField>();

        public DataExpField GetField(string name)
        {
            foreach (DataExpField field in FieldList)
            {
                if (field.Name.ToUpper() == name.ToUpper())
                    return field;
            }

            return null;
        }
    }

    public class DataExpField
    {
        public string Name;
        public int RightPadLen = 0;
        public int LeftPadLen = 0;
        public string PadChar = string.Empty;
        public string Title = string.Empty;
        public string ConstChar = string.Empty;

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class DataExpParameter
    {
        public int Seq = 1;
    }

    public class DataExpFormat
    {
        public string Type = string.Empty;
        public string Value = string.Empty;
        public List<DataExpParameter> ParameterList = new List<DataExpParameter>();
    }

    public class DataExpFileName
    {
        public List<DataExpFormat> FormatList = new List<DataExpFormat>();
        public string Format = string.Empty;
    }
}
