using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.OleDb;
using System.Xml;
using System.IO;
using System.Text;

using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.WarehouseWeb.ImportData
{
    [Serializable]
    public class ImportExcel
    {
        private ArrayList FileCollection = null;
        private ArrayList NotAllowNullFileCollection = null;
        public ImportExcel(
            string filePath,
            string inputType,
            ArrayList fieldCollection,
            ArrayList notAllowNullFileCollection)
        {
            this.FilePath = filePath;
            this.InputType = inputType;
            //modify by klaus 
            //this.XlsConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"", filePath);
            this.XlsConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;\"", filePath);
            //end
            FileCollection = fieldCollection;
            NotAllowNullFileCollection = notAllowNullFileCollection;        }

        public string FilePath
        {
            get
            {
                return filepath;
            }
            set
            {
                filepath = value;
            }
        }private string filepath = string.Empty;

        public string InputType
        {
            get
            {
                return inputtype;
            }
            set
            {
                inputtype = value;
            }
        }private string inputtype = string.Empty;

        public string XlsConn
        {
            get
            {
                return excelconstr;
            }
            set
            {
                excelconstr = value;
            }
        }private string excelconstr = string.Empty;

        public DataTable XlaDataTable
        {
            get
            {
                return ExecSQLForDataTable();
            }
        }

        public int Datacount
        {
            get
            {
                if (this.XlaDataTable == null)
                {
                    return 0;
                }

                return this.XlaDataTable.Rows.Count;
            }
        }

        private DataTable ExecSQLForDataTable()
        {
            DataSet ds = null;
            string queryFileds = GetQueryFileds();
            string queryContent = GetQueryContent();
            OleDbConnection oleConn = new OleDbConnection(this.XlsConn);

            try
            {
                oleConn.Open();

                DataTable dt = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                    }
                }

                if (sheetName.Trim().Length <= 0)
                {
                    return null;
                }

                string sSql = string.Format("SELECT distinct {0} FROM [{2}] where 1=1 {1}", queryFileds, queryContent, sheetName);
                OleDbCommand oleCmd = new OleDbCommand(sSql, oleConn);
                OleDbDataAdapter oleAdapter = new OleDbDataAdapter(sSql, oleConn);
                ds = new DataSet();
                oleAdapter.Fill(ds);

                ds = new DataSet();
                oleAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                ExceptionManager.Raise(this.GetType().BaseType, "$Error_UploadFileError", ex);
            }
            finally
            {
                oleConn.Close();
            }

            return ds.Tables[0];
        }

        private string GetQueryFileds()
        {
            string fields = string.Empty;
            if (FileCollection.Count > 0)
            {
                foreach (DictionaryEntry de in FileCollection)
                {
                    fields += string.Format(" trim([{0}]) as {1} ,", de.Value.ToString(), de.Key.ToString());
                }
            }

            return fields.TrimEnd(',');
        }

        private string GetQueryContent()
        {
            string fields = string.Empty;
            if (NotAllowNullFileCollection.Count > 0)
            {
                foreach (DictionaryEntry de in NotAllowNullFileCollection)
                {
                    fields += string.Format(" and (trim([{0}]) <> '' AND trim([{0}]) is not null) ", de.Value.ToString(), de.Key.ToString());
                }
            }

            return fields.TrimEnd(',');
        }
    }
}
