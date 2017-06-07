using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.DataExp
{
    public class BaseEngine
    {
        //要导出的数据
        protected System.Data.DataTable Data;
        //导出的object名字
        protected string Name;
        //导出的配置项
        protected DataExpSchema Schema;
        protected DataExpObject SchemaObject;
        protected System.IO.StreamWriter Writer = null;
        protected object[] InputParam;
        protected BenQGuru.eMES.Common.PersistBroker.IPersistBroker PSB;
        protected string _fileName = string.Empty;
        public void Exp(string name,System.Data.DataTable dt,object[] inputParam)
        {
            this.PSB = BenQGuru.eMES.Common.PersistBroker.PersistBrokerManager.PersistBroker();
            this.InputParam = inputParam;
            this.Name = name;
            this.Data = dt;
            this.Schema = new DataExpSchema();
            this.SchemaObject = Schema.GetDataExpObject(name);
            this.DoExp();
        }

        public string ExpFileName
        {
            get
            {
                if (_fileName == string.Empty)
                {
                    System.Collections.ArrayList al = new System.Collections.ArrayList();
                    foreach (DataExpFormat f in SchemaObject.FileName.FormatList)
                    {
                        if (f.Type.ToUpper() == "DATE")
                            al.Add(System.DateTime.Now.ToString(f.Value));
                        else if (f.Type.ToUpper() == "PARAMETER")
                        {
                            if (InputParam.Length >= int.Parse(f.Value))
                            {
                                al.Add(InputParam[int.Parse(f.Value) - 1].ToString());
                            }
                            else
                            {
                                al.Add(string.Empty);
                            }
                        }
                        else if (f.Type.ToUpper() == "SQL")
                        {
                            string sql = f.Value;
                            System.Collections.ArrayList alParam = new System.Collections.ArrayList();
                            foreach (DataExpParameter param in f.ParameterList)
                            {
                                if (InputParam != null && InputParam.Length >= param.Seq)
                                    alParam.Add(InputParam[param.Seq - 1].ToString());
                                else
                                    alParam.Add(string.Empty);
                            }
                            sql = string.Format(sql, alParam.ToArray());

                            System.Data.DataSet ds = PSB.Query(sql);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                al.Add(ds.Tables[0].Rows[0][0].ToString());
                            }
                            else
                                al.Add(string.Empty);
                        }
                    }
                    _fileName = string.Format(SchemaObject.FileName.Format, al.ToArray());
                    _fileName = System.IO.Path.Combine(this.SchemaObject.FilePath, _fileName);
                }
                return _fileName;
            }
        }

        protected virtual void DoExp()
        {
 
        }

        protected string GetColumnDesc(string columnName)
        {
            string sql = "select DESCRIPTION from TBLRPTVDATASRCCOLUMN where COLUMNNAME='" + columnName.ToUpper() + "'";
            System.Data.DataSet ds = PSB.Query(sql);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString();
            }
            else
                return columnName;
        }
        public static void ExpData(string name, System.Data.DataTable dt, object[] inputParam)
        {
            DataExpSchema Schema = new DataExpSchema();
            DataExpObject SchemaObject = Schema.GetDataExpObject(name);

            if (SchemaObject == null)
                return;

            BaseEngine eng = null;
            if (SchemaObject.Type == "Text")
                eng = new TextEngine();
            else if (SchemaObject.Type == "Excel")
                eng = new ExcelEngine();
            
            if (eng != null)
                eng.Exp(name,dt,inputParam);
        }
    }
}
