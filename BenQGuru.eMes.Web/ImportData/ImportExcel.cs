using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;

using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.ImportData
{
    [Serializable]
    public class ImportExcel
    {
        private string _FilePath = string.Empty;
        private string _InputType = string.Empty;
        private string _XlsConnection = string.Empty;
        private Dictionary<string, string> _FieldDictionary = null;
        private Dictionary<string, string> _NotAllowNullFieldDictionary = null;


        public ImportExcel(string filePath, string inputType,
            Dictionary<string, string> fieldDictionary, Dictionary<string, string> notAllowNullFieldDictionary)
        {
            _FilePath = filePath;
            _InputType = inputType;
            //modify by klaus 
            //_XlsConnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"", filePath);
            _XlsConnection = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;\"", filePath);
            //end
            _FieldDictionary = fieldDictionary;
            _NotAllowNullFieldDictionary = notAllowNullFieldDictionary;
        }

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
            OleDbConnection oleConn = new OleDbConnection(_XlsConnection);

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
            if (_FieldDictionary != null && _FieldDictionary.Keys.Count > 0)
            {
                foreach (string key in _FieldDictionary.Keys)
                {
                    fields += string.Format(" trim([{0}]) as {1} ,", _FieldDictionary[key], key);
                }
            }

            return fields.TrimEnd(',');
        }

        private string GetQueryContent()
        {
            string fields = string.Empty;
            if (_NotAllowNullFieldDictionary != null && _NotAllowNullFieldDictionary.Keys.Count > 0)
            {
                foreach (string key in _NotAllowNullFieldDictionary.Keys)
                {
                    fields += string.Format(" and (trim([{0}]) <> '' AND trim([{0}]) is not null) ", _NotAllowNullFieldDictionary[key], key);
                }
            }

            return fields.TrimEnd(',');
        }
    }
}
