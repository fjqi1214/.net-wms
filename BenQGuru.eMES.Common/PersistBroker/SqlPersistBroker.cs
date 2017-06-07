using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;

using BenQGuru.eMES.Common.MutiLanguage;
using System.Collections;

namespace BenQGuru.eMES.Common.PersistBroker
{
	/// <summary>
	/// OraPersistBroker 的摘要说明。
	/// </summary>
	public class SqlPersistBroker: AbstractSQLPersistBroker, IPersistBroker
	{
		public SqlPersistBroker(string connectionString): base(new SqlConnection(connectionString))
		{
			
		}

		public SqlPersistBroker(string connectionString, System.Globalization.CultureInfo  cultureInfo): base(new SqlConnection(connectionString), cultureInfo)
		{
		}

		//		~SqlPersistBroker()
		//		{
		//			this.CloseConnection(); 
		//		}

		public override  IDbDataAdapter GetDbDataAdapter()
		{
			return new SqlDataAdapter(); 
		}
		/// <summary>
		/// Laws Lu,2006/12/20 修改,支持手动关闭连接
		/// 是否 自动关闭连接
		/// </summary>
		public override bool AutoCloseConnection
		{
			get
			{
				return base.AutoCloseConnection;
			}
			set
			{
				base.AutoCloseConnection = value;
			}
		}

		public override void Execute(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			OpenConnection();
			using(SqlCommand command = (SqlCommand)this.Connection.CreateCommand())
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i], CSharpType2SqlType(parameterTypes[i])).Value = parameterValues[i];
					//					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
					command.Transaction = (SqlTransaction)this.Transaction; 
				}
				try
				{
					//Laws Lu,在Debug状态下不允许Log日志
					#if Debug
					Log.Info(" Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
					#endif

					command.ExecuteNonQuery();
				}
				catch(Exception e)
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
					ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
				}
				finally
				{
					//Laws Lu,2006/12/20 修改如果自动关闭为True并且不在Transaction中时才会自动关闭Connection
					if(this.Transaction == null && AutoCloseConnection == true)
					{
						//CloseConnection();
					}
				}
			}
		}

		public override int ExecuteWithReturn(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			int iReturn = 0;
			OpenConnection();
			using(SqlCommand command = (SqlCommand)this.Connection.CreateCommand())
			{	
				command.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					command.Parameters.Add(parameters[i], CSharpType2SqlType(parameterTypes[i])).Value = parameterValues[i];
					//					command.CommandText = 	ChangeParameterPerfix(command.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
					command.Transaction = (SqlTransaction)this.Transaction; 
				}
				try
				{
					//Laws Lu,在Debug状态下不允许Log日志
#if Debug
					Log.Info(" Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
#endif

					iReturn = command.ExecuteNonQuery();
				}
				catch(Exception e)
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(command.CommandText, parameterValues) );
					ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
				}
				finally
				{
					//Laws Lu,2006/12/20 修改如果自动关闭为True并且不在Transaction中时才会自动关闭Connection
					if(this.Transaction == null && AutoCloseConnection == true)
					{
						//CloseConnection();
					}
				}
				return iReturn;
			}
		}

		public override DataSet Query(string commandText, string[] parameters, Type[] parameterTypes, object[] parameterValues)
		{
			OpenConnection();
			SqlDataAdapter dataAdapter = (SqlDataAdapter)this.GetDbDataAdapter();
			using(dataAdapter.SelectCommand = (SqlCommand)this.Connection.CreateCommand()) 
			{	
				dataAdapter.SelectCommand.CommandText = this.changeParameterPerfix(commandText);

				for (int i = 0; i < parameters.Length; i++)
				{
					dataAdapter.SelectCommand.Parameters.Add(parameters[i], CSharpType2SqlType(parameterTypes[i])).Value = parameterValues[i];
					//					dataAdapter.SelectCommand.CommandText = ChangeParameterPerfix(dataAdapter.SelectCommand.CommandText, parameters[i]);
				}
				if (this.Transaction != null)
				{
					dataAdapter.SelectCommand.Transaction = (SqlTransaction)this.Transaction; 
				}
				
				DataSet dataSet = new DataSet();

				try
				{
					//Laws Lu,在Debug状态下不允许Log日志
				#if Debug
					Log.Info(" Parameter SQL:" + this.spellCommandText(dataAdapter.SelectCommand.CommandText, parameterValues) );
				#endif

					dataAdapter.Fill(dataSet);		
				}
				catch( Exception e )
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " Parameter SQL:" + this.spellCommandText(dataAdapter.SelectCommand.CommandText, parameterValues) );
					ExceptionManager.Raise(this.GetType(),"$Error_Command_Execute",e);
				}
				finally
				{
					//Laws Lu,2006/12/20 修改如果自动关闭为True并且不在Transaction中时才会自动关闭Connection
					if(this.Transaction == null && AutoCloseConnection == true)
					{
						//CloseConnection();
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

		private System.Data.SqlDbType CSharpType2SqlType(Type cSharpType)
		{
			if ( cSharpType == typeof(string) )
			{
				return SqlDbType.VarChar;
			}

			if ( cSharpType == typeof(int) )
			{
				return SqlDbType.Int;
			}

			if ( cSharpType == typeof(long) )
			{
				return SqlDbType.BigInt;				
			}

			if ( cSharpType == typeof(double) )
			{
				return SqlDbType.Float;
			}

			if ( cSharpType == typeof(float) )
			{
				return SqlDbType.Float;				
			}

			if ( cSharpType == typeof(decimal) )
			{
				return SqlDbType.Decimal;
			}

			if ( cSharpType == typeof(bool) )
			{
				return SqlDbType.VarChar;
			}

			if ( cSharpType == typeof(DateTime) )
			{
				return SqlDbType.VarChar;
			}

			if ( cSharpType == typeof(byte[]) )
			{
				return SqlDbType.Binary;
			}

			return SqlDbType.Variant;
		}

		public IDataReader QueryDataReader(string commandText)
		{
			OpenConnection();

			using(IDbCommand command = (IDbCommand)this.Connection.CreateCommand()) 
			{	
				command.CommandText = commandText;

				if (this.Transaction != null)
				{
					command.Transaction = this.Transaction; 
				}
				
				DateTime dtStart = new DateTime();
				IDataReader reader  = null;
				try
				{
					//修改	在Debug模式下不允许Log日志文件
#if DEBUG
					dtStart = DateTime.Now;
					Log.Info("************StartDateTime:" + dtStart.ToString() + "," + dtStart.Millisecond );
					Log.Info(" Text SQL:" + command.CommandText );
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
					Log.Error( e.Message + " Text SQL:" + command.CommandText);
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
					//Laws Lu,2006/12/20 修改如果自动关闭为True并且不在Transaction中时才会自动关闭Connection
					if(this.Transaction == null && AutoCloseConnection == true)
					{
						//CloseConnection();
					}
				}
			}

			return null;
		}

		public new DataSet Query(string commandText)
		{
			OpenConnection();
			IDbDataAdapter dataAdapter = this.GetDbDataAdapter();
			using(dataAdapter.SelectCommand = this.Connection.CreateCommand()) 
			{	
				dataAdapter.SelectCommand.CommandText = commandText;
                dataAdapter.SelectCommand.CommandTimeout = MesEnviroment.CommandTimeout;
				dataAdapter.SelectCommand.Transaction = this.Transaction; 
				DataSet dataSet = new DataSet();
				DateTime dtStart = new DateTime();
				try
				{
#if DEBUG
					dtStart =  DateTime.Now;
					Log.Info("************StartDateTime:" + dtStart.ToString() + "," + dtStart.Millisecond );
					Log.Info(" Parameter SQL:" + commandText );
#endif

					dataAdapter.Fill(dataSet);	
	
#if DEBUG
					DateTime dtEnd = DateTime.Now;
					TimeSpan ts = dtEnd - dtStart;
					Log.Info("************EndDateTime:" + dtEnd.ToString() + "," + dtEnd.Millisecond + "*********"  
						+ "Cost: " + ts.Seconds + ":" + ts.Milliseconds);
#endif
				}
				catch( Exception e )
				{
                    //added by leon.li @20130311
                    Log.Error(e.StackTrace);
                    //end added
					Log.Error( e.Message + " " + commandText );

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
					//Laws Lu,2006/12/20 修改如果自动关闭为True并且不在Transaction中时才会自动关闭Connection
					if(this.Transaction == null && AutoCloseConnection == true)
					{
						//CloseConnection();
					}
				}

				return dataSet;
			}
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
