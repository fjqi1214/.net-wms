using System;
using System.Data;
//using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;

using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

using BenQGuru.eMES.Common.MutiLanguage;
using System.Collections;

namespace BenQGuru.eMES.Common.PersistBroker
{
	/// <summary>
	/// ODPPersistBroker 的摘要说明。
	/// </summary>
	public class ODPPersistBroker: AbstractSQLPersistBroker, IPersistBroker
	{
		public static string ProgramBool = "false"; 
		public static string ResourceCode = String.Empty;
		public dblog db=new dblog();
		public ODPPersistBroker(string connectionString): base(new OracleConnection(connectionString))
		{
			
		}

		public ODPPersistBroker(string connectionString, System.Globalization.CultureInfo  cultureInfo): base(new OracleConnection(connectionString), cultureInfo)
		{
		}

		//		~ODPPersistBroker()
		//		{
		//			this.CloseConnection(); 
		//		}

		public override  IDbDataAdapter GetDbDataAdapter()
		{
			return new OracleDataAdapter(); 
		}

		public override void Execute(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			OpenConnection();
			using(OracleCommand command = (OracleCommand)this.Connection.CreateCommand())
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					if (parameterValues[i].ToString() == string.Empty)
						parameterValues[i] = DBNull.Value;
					command.Parameters.Add(parameters[i], CSharpType2OracleType(parameterTypes[i])).Value = parameterValues[i];
					//					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
//					command.Transaction = (OracleTransaction)this.Transaction; 
				}
				DateTime dtStart = new DateTime();
				try
				{
					//2006/09/12 新增 读取配置文件Log日志文件
					XmlDocument xmldoc=new XmlDocument();
					string constr="";
					xmldoc.Load(System.AppDomain.CurrentDomain.BaseDirectory.ToString()+"\\dblog.xml");

					XmlNodeReader xr=new XmlNodeReader(xmldoc);

					while(xr.Read())
					{
						if(xr.GetAttribute("name")=="BS")
						{
							ProgramBool = xr.ReadString();
						}
						if(xr.GetAttribute("name")=="Constr")
						{
							constr = xr.ReadString();
						}
					}
					

					if(ProgramBool=="true")
					{                                             				
						db.dblog1(constr,this.spellCommandText(command.CommandText,parameterValues));
					}
						
					command.ExecuteNonQuery();
					
					//修改	在Debug模式下不允许Log日志文件
#if DEBUG
					dtStart = DateTime.Now;
					Log.Info("************StartDateTime:" + dtStart.ToString() + "," + dtStart.Millisecond );
					Log.Info(" Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#endif
					
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif
				}
				catch(Oracle.DataAccess.Client.OracleException e)
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

				}
				catch(Exception e)
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

					ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
				}
				finally
				{
					if(this.Transaction == null)
					{
						CloseConnection();
					}
				}
			}
		}

		public override int ExecuteWithReturn(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			int iReturn = 0;
			OpenConnection();
			using(OracleCommand command = (OracleCommand)this.Connection.CreateCommand())
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i], CSharpType2OracleType(parameterTypes[i])).Value = parameterValues[i];
					//					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
					//command.Transaction = (OracleTransaction)this.Transaction; 
				}
				DateTime dtStart = new DateTime();
				try
				{
					//修改	在Debug模式下不允许Log日志文件
#if DEBUG
					dtStart = DateTime.Now;
					Log.Info("************StartDateTime:" + dtStart.ToString() + "," + dtStart.Millisecond );
					Log.Info(" Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#endif
					iReturn = command.ExecuteNonQuery();
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif
				}
				catch(Oracle.DataAccess.Client.OracleException e)
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

				}
				catch(Exception e)
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

					ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
				}
				finally
				{
					if(this.Transaction == null)
					{
						CloseConnection();
					}
				}
				return iReturn;
			}
		}

		public override DataSet Query(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			OpenConnection();
			//OleDbDataAdapter dataAdapter = (OleDbDataAdapter)this.GetDbDataAdapter();
			using(OracleCommand command = (OracleCommand)this.Connection.CreateCommand()) 
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i], CSharpType2OracleType(parameterTypes[i])).Value = parameterValues[i];
					//					dataAdapter.SelectCommand.CommandText = ChangeParameterPerfix(dataAdapter.SelectCommand.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
//					command.Transaction = (OracleTransaction)this.Transaction; 
				}
				
				DataSet dataSet = new DataSet();
				DateTime dtStart = new DateTime();
				OracleDataReader reader = null;
				try
				{
					//修改	在Debug模式下不允许Log日志文件
#if DEBUG
					dtStart = DateTime.Now;
					Log.Info("************StartDateTime:" + dtStart.ToString() + "," + dtStart.Millisecond );
					Log.Info(" Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#endif

					reader = command.ExecuteReader();
					//command.

#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

					
					DataTable dt = new DataTable();
					while(reader.Read())
					{
						for(int i = 0 ;i< reader.FieldCount;i++)
						{
							if(!dt.Columns.Contains(reader.GetName(i)))
							{
								DataColumn dc = new DataColumn(reader.GetName(i),reader.GetFieldType(i));

								dt.Columns.Add(dc);
							}
						}

						DataRow dr = dt.NewRow();
						for(int i = 0 ;i< reader.FieldCount;i++)
						{
							/*
							if(reader.GetValue(i) is String)
							{
								string dbValue = reader.GetValue(i).ToString();
								dbValue = dbValue.Replace("\0","");
								dbValue = dbValue.Replace("\r\n","");
								dbValue = dbValue.Replace("\r","");
								dbValue = dbValue.Replace("\n","");
								dr[reader.GetName(i)] = dbValue ;
							}
							else
							{
								dr[reader.GetName(i)] = reader.GetValue(i);
							}
							*/
							object objOraVal = reader.GetOracleValue(i);
							if (objOraVal is Oracle.DataAccess.Types.OracleString)
							{
								string dbValue = objOraVal.ToString();
								dbValue = dbValue.Replace("\0","");
								dbValue = dbValue.Replace("\r\n","");
								dbValue = dbValue.Replace("\r","");
								dbValue = dbValue.Replace("\n","");
								dr[reader.GetName(i)] = dbValue ;
							}
							else
							{
								if ((dt.Columns[i].DataType == typeof(int) ||
									dt.Columns[i].DataType == typeof(decimal)) && 
									(objOraVal == DBNull.Value || objOraVal.ToString() == string.Empty))
									dr[reader.GetName(i)] = 0;
								else
									dr[reader.GetName(i)] = objOraVal.ToString();
							}
						}
						dt.Rows.Add(dr);

						
					}
					reader.Close();
					dataSet.Tables.Add(dt);
					//dataSet.Tables.Add(dt);

					//dataAdapter.Fill(dataSet);		
				}
				catch( Exception e )
				{
					reader.Close();
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

					ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
				}
				finally
				{
					if(this.Transaction == null)
					{
						CloseConnection();
					}
				}

				return dataSet;
			}
		}

		private string changeParameterPerfix(string commandText)
		{
			//return new Regex(@"\$([0-9A-Za-z]+)").Replace(commandText, "?");
			return commandText.Replace("$", ":");
		}

		private string spellCommandText( string commandText, object[] parameterValues )
		{
			StringBuilder builder = new StringBuilder();
			string[] texts = commandText.Split('?');

			for ( int i=0; i< texts.Length; i++ )
			{
				if ( i < parameterValues.Length )
				{
					builder.Append( string.Format("{0}'{1}'", texts[i], parameterValues[i]) );
				}
				else
				{
					builder.Append(texts[i]);
				}
			}

			return builder.ToString();
		}

		private OracleDbType CSharpType2OracleType(Type cSharpType)
		{
			if ( cSharpType == typeof(string) )
			{
				return OracleDbType.Varchar2;
			}

			if ( cSharpType == typeof(int) )
			{
				return OracleDbType.Int32;
			}

			if ( cSharpType == typeof(long) )
			{
				return OracleDbType.Int64;
			}

			if ( cSharpType == typeof(double) )
			{
				return OracleDbType.Double;
			}

			if ( cSharpType == typeof(float) )
			{
				return OracleDbType.Decimal;
			}

			if ( cSharpType == typeof(decimal) )
			{
				return OracleDbType.Decimal;
			}

			if ( cSharpType == typeof(bool) )
			{
				return OracleDbType.Int32;
			}

			if ( cSharpType == typeof(DateTime) )
			{
				return OracleDbType.Date;
			}

			if ( cSharpType == typeof(byte[]) )
			{
				return OracleDbType.Blob;
			}

			return OracleDbType.Varchar2;
		}

		public IDataReader QueryDataReader(string commandText)
		{
			OpenConnection();

			using(IDbCommand command = (IDbCommand)this.Connection.CreateCommand()) 
			{	
				command.CommandText = commandText;

				if (this.Transaction != null)
				{
//					command.Transaction = this.Transaction; 
				}
				
				DateTime dtStart = new DateTime();
				IDataReader reader  = null;
				try
				{
					//修改	在Debug模式下不允许Log日志文件
#if DEBUG
					dtStart = DateTime.Now;
					Log.Info("************StartDateTime:" + dtStart.ToString() + "," + dtStart.Millisecond );
					Log.Info(" Text Oracle:" + command.CommandText );
#endif

					reader = command.ExecuteReader();
					//command.

#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

					return reader;	
				}
				catch( Exception e )
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Text Oracle:" + command.CommandText);
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif

					ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
				}
			}

			return null;
		}
        /// <summary>
        /// 执行Procedure
        /// </summary>
        /// <param name="commandText">Procedure名称</param>
        /// <param name="parameters">参数列表</param>
        public override void ExecuteProcedure(string commandText, ref ArrayList parameters)
        {
            return;
        }
	}
}
