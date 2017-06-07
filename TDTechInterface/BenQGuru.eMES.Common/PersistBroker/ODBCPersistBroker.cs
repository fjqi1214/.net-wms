using System;
using System.Data;
using System.Data.Odbc;
using System.Text.RegularExpressions;
using System.Text;

using BenQGuru.eMES.Common.MutiLanguage;
using System.Collections;

namespace BenQGuru.eMES.Common.PersistBroker
{
	/// <summary>
	/// ODBCPersistBroker 的摘要说明。
	/// </summary>
	public class ODBCPersistBroker: AbstractSQLPersistBroker, IPersistBroker
	{
		public ODBCPersistBroker(string connectionString): 
			base(new OdbcConnection(connectionString))
		{
		}

		public ODBCPersistBroker(string connectionString, System.Globalization.CultureInfo  cultureInfo): 
			base(new OdbcConnection(connectionString), cultureInfo)
		{
		}
		public override  IDbDataAdapter GetDbDataAdapter()
		{
			return new OdbcDataAdapter(); 
		} 


		public override int ExecuteWithReturn(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			int iReturn = 0;
			OpenConnection();
			using(OdbcCommand command = (OdbcCommand)this.Connection.CreateCommand())
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i], CSharpType2OdbcType(parameterTypes[i])).Value = parameterValues[i];
					//					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
					command.Transaction = (OdbcTransaction)this.Transaction; 
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
				catch(System.Data.OleDb.OleDbException e)
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

					if(e.ErrorCode == -2147217873)
					{
						ExceptionManager.Raise(this.GetType(),"$ERROR_DATA_ALREADY_EXIST",e);
					}
					else
					{
						ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
					}
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

		//Laws Lu,2005/10/28,修改	缓解性能问题,改用Reader
		public override void Execute(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			OpenConnection();
			using(OdbcCommand command = (OdbcCommand)this.Connection.CreateCommand())
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i], CSharpType2OdbcType(parameterTypes[i])).Value = parameterValues[i];
					//					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
					command.Transaction = (OdbcTransaction)this.Transaction; 
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
					command.ExecuteNonQuery();
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif
				}
				catch(System.Data.OleDb.OleDbException e)
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

					if(e.ErrorCode == -2147217873)
					{
						ExceptionManager.Raise(this.GetType(),"$ERROR_DATA_ALREADY_EXIST",e);
					}
					else
					{
						ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
					}
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
			}
		}
		//Laws Lu,2005/10/28,修改	缓解性能问题,改用Reader
		public override DataSet Query(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			OpenConnection();
			//OleDbDataAdapter dataAdapter = (OleDbDataAdapter)this.GetDbDataAdapter();
			using(OdbcCommand command = (OdbcCommand)this.Connection.CreateCommand()) 
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i], CSharpType2OdbcType(parameterTypes[i])).Value = parameterValues[i];
					//					dataAdapter.SelectCommand.CommandText = ChangeParameterPerfix(dataAdapter.SelectCommand.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
					command.Transaction = (OdbcTransaction)this.Transaction; 
				}
				
				DataSet dataSet = new DataSet();
				DateTime dtStart = new DateTime();
				OdbcDataReader reader = null;
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
			return new Regex(@"\$([0-9A-Za-z_]+)").Replace(commandText, "?");
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

		private System.Data.Odbc.OdbcType CSharpType2OdbcType(Type cSharpType)
		{
			if ( cSharpType == typeof(string) )
			{
				return OdbcType.VarChar;
			}

			if ( cSharpType == typeof(int) )
			{
				return OdbcType.Int;
			}

			if ( cSharpType == typeof(long) )
			{
				return OdbcType.BigInt;				
			}

			if ( cSharpType == typeof(double) )
			{
				return OdbcType.Double;
			}

			if ( cSharpType == typeof(float) )
			{
				return OdbcType.Real;			
			}

			if ( cSharpType == typeof(decimal) )
			{
				return OdbcType.Decimal;
			}

			if ( cSharpType == typeof(bool) )
			{
				return OdbcType.VarChar;
			}

			if ( cSharpType == typeof(DateTime) )
			{
				return OdbcType.VarChar;
			}

			if ( cSharpType == typeof(byte[]) )
			{
				return OdbcType.Binary;
			}

			return OdbcType.VarChar;
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
