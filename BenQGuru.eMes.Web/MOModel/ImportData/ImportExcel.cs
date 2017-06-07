using System;
using System.Data;
using System.Collections;
using System.Data.OleDb;

using BenQGuru.eMES.Common;

namespace BenQGuru.eMES.Web.MOModel.ImportData
{
	/// <summary>
	/// ImportExcel 的摘要说明。
	/// </summary>
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
			this.FilePath = filePath ;
			this.InputType = inputType ;
            //this.XlsConn = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0; Data Source= '{0}'; Extended Properties=Excel 8.0",filePath);
            //modify by klaus
            //this.XlsConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{0}';Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"", filePath);
            this.XlsConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{0}';Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1;\"", filePath);
            //end
            FileCollection = fieldCollection;
			NotAllowNullFileCollection = notAllowNullFileCollection;
		}
		
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
		}private string filepath = string.Empty ;
		
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
		}private string inputtype = string.Empty ;

		public string XlsConn
		{
			get
			{
				return excelconstr ;
			}
			set
			{
				excelconstr = value ;
			}
		}private string excelconstr = string.Empty;

		public DataTable XlaDataTable
		{
			get
			{
				return ExecSQLForDataTable(this.InputType);
			}
		}

		public int Datacount
		{
			get
			{
				if( this.XlaDataTable == null )
				{
					return 0;
				}

				return this.XlaDataTable.Rows.Count ;
			}
		}

        private DataTable ExecSQLForDataTable(string inputType)
        {
            DataSet ds = null;
            string queryFileds = GetQueryFileds();
            //modified by kathy @20130812 查询工单，栏位为空的不过滤
            //string queryContent = GetQueryContent();
            string queryContent = "";
            string sSql = "";
            if (inputType == "RMADetail")
            {
                sSql = string.Format("SELECT  {0} FROM [sheet1$] where 1=1", queryFileds);
            }
            else
            {
                sSql = string.Format("SELECT distinct {0} FROM [sheet1$] where 1=1 {1}", queryFileds, queryContent);
            }

			OleDbConnection oleConn = new OleDbConnection(this.XlsConn);
			OleDbCommand oleCmd = new OleDbCommand(sSql,oleConn);
			OleDbDataAdapter oleAdapter = new OleDbDataAdapter(sSql,oleConn);
			try
			{
				oleConn.Open();
				ds = new DataSet();
				oleAdapter.Fill(ds);
			}
			catch(Exception ex)
			{
				ExceptionManager.Raise(this.GetType().BaseType,"$Error_UploadFileError",ex);
			}
			finally
			{
				oleConn.Close();
			}
	
			return ds.Tables[0] ;
		}

		private string GetQueryFileds()
		{
			string fields = string.Empty;
			if(FileCollection.Count>0)
			{
				foreach( DictionaryEntry de in FileCollection )
				{
                    fields += string.Format(" trim([{0}]) as {1} ,", de.Value.ToString(), de.Value.ToString().ToUpper());
				}
			}

			return fields.TrimEnd(',');
		}

		private string GetQueryContent()
		{
			string fields = string.Empty;
			if(NotAllowNullFileCollection.Count>0)
			{
				foreach( DictionaryEntry de in NotAllowNullFileCollection )
				{
					fields += string.Format(" and (trim([{0}]) <> '' or trim([{0}]) is not null ) ", de.Value.ToString(), de.Key.ToString());
				}
			}

			return fields.TrimEnd(',');
		}
	}
}
