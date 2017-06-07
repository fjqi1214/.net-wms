using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace BenQGuru.eMES.DataExp
{
    public class TextEngine
        : BaseEngine
    {
        protected override void DoExp()
        {
            if (File.Exists(this.ExpFileName))
            {
                File.Delete(this.ExpFileName);
            }

            using (Writer = System.IO.File.AppendText(this.ExpFileName))
            {
                string line = string.Empty;
                if (SchemaObject.SeparateChar == null || SchemaObject.SeparateChar == string.Empty)
                {
                    SchemaObject.SeparateChar = "";
                }

                #region 写Title部分
                if (this.SchemaObject.NeedTitle)
                {
                    line = string.Empty;
                    foreach (DataExpField field in this.SchemaObject.FieldList)
                    {
                        string str = this.GetColumnDesc(field.Name);

                        if (field.PadChar == string.Empty)
                            field.PadChar = " ";

                        line = line + this.SchemaObject.SeparateChar + str;
                    }

                    if (line != null && line.Length > 1 && this.SchemaObject.SeparateChar!= string.Empty)
                    {
                        line = line.Substring(1, line.Length - 1);
                    }

                    Writer.WriteLine(line);
                }
                #endregion

                #region 导出数据部分
                foreach (System.Data.DataRow dr in this.Data.Rows)
                {
                    line = string.Empty;
                    foreach (DataExpField field in this.SchemaObject.FieldList)
                    {
                        string str = dr[field.Name].ToString();

                        if (field.PadChar == string.Empty)
                            field.PadChar = " ";

                        //先填充左边的,再填充右边的
                        if (field.LeftPadLen != 0)
                            str = str.PadLeft(field.LeftPadLen, field.PadChar[0]);
                        else if (field.RightPadLen != 0)
                            str = str.PadRight(field.RightPadLen, field.PadChar[0]);

                        line = line + this.SchemaObject.SeparateChar + str + field.ConstChar;

                    }

                    if (line != null && line.Length > 1 && this.SchemaObject.SeparateChar != string.Empty)
                    {
                        line = line.Substring(1, line.Length - 1);
                    }
                    
                    Writer.WriteLine(line);
                }
                #endregion
            }
        }
    }
}
