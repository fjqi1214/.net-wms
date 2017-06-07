using System;
using System.Collections.Generic;
using System.Text;
using SAP.Middleware.Connector;
using System.Data;
using BenQGuru.eMES.SAPRFCService;

namespace BenQGuru.SAP.SAPNcoLib
{
    public class NcoFunction
    {
        const string ABAP_AS = "Q97";
        private RfcDestination destination = null;
        private string functionName = null;
        private Dictionary<string, object> importParameters = null;
        private Dictionary<string, object> exportParameters = null;
        private string message;
        private IDestinationConfiguration ID;

        #region Connect
        //public void Connect()
        //{
        //    IDestinationConfiguration ID = new RFCConfigg();

        //    RfcDestinationManager.RegisterDestinationConfiguration(ID);

        //    destination = RfcDestinationManager.GetDestination(ABAP_AS);

        //    TestConnect();

        //    Console.Write(AttributesToString());
        //}

        public void Connect()
        {
            ID = new RFCConfigg();

            RfcDestinationManager.RegisterDestinationConfiguration(ID);

            destination = RfcDestinationManager.GetDestination(ABAP_AS);

            //TestConnect();

            //Console.Write(AttributesToString());
        }
        public void DISConncet()
        {
            if (destination != null)
            {
                try
                {
                    RfcDestinationManager.UnregisterDestinationConfiguration(ID);
                }
                catch { }
            }
        }

        public void Connect(string sapClientId)
        {
            Dictionary<string, RfcConfigParameters> availableDestinations = new Dictionary<string, RfcConfigParameters>();

            RfcConfigParameters parameters = new RfcConfigParameters();
            parameters[RfcConfigParameters.Name] = "DEV";
            parameters[RfcConfigParameters.MaxPoolSize] = "1";
            parameters[RfcConfigParameters.IdleTimeout] = "10";
            parameters[RfcConfigParameters.User] = "scm";
            parameters[RfcConfigParameters.Password] = "123456";
            parameters[RfcConfigParameters.Client] = sapClientId;
            parameters[RfcConfigParameters.Language] = "zh";
            parameters[RfcConfigParameters.AppServerHost] = "192.168.123.31";
            parameters[RfcConfigParameters.SystemNumber] = "00";
            //parameters[RfcConfigParameters.Codepage] = "8300";

            destination = RfcDestinationManager.GetDestination(parameters);
            TestConnect();
        }

        public void Connect(string name, string applicationServer, string user, string password, string language,
            string client, string systemNumber)
        {
            Connect(name, applicationServer, user, password, language, client, systemNumber, 1, 10, null);
        }

        public void Connect(string name, string applicationServer, string user, string password, string language,
            string client, string systemNumber, string codePage)
        {
            Connect(name, applicationServer, user, password, language, client, systemNumber, 1, 10, codePage);
        }

        public void Connect(string name, string applicationServer, string user, string password, string language,
            string client, string systemNumber, int poolSize, int idleTimeout, string codePage)
        {
            Dictionary<string, RfcConfigParameters> availableDestinations = new Dictionary<string, RfcConfigParameters>();

            RfcConfigParameters parameters = new RfcConfigParameters();
            parameters[RfcConfigParameters.Name] = name;
            parameters[RfcConfigParameters.MaxPoolSize] = Convert.ToString(poolSize);
            parameters[RfcConfigParameters.IdleTimeout] = Convert.ToString(idleTimeout);
            parameters[RfcConfigParameters.User] = user;
            parameters[RfcConfigParameters.Password] = password;
            parameters[RfcConfigParameters.Client] = client;
            parameters[RfcConfigParameters.Language] = language;
            parameters[RfcConfigParameters.AppServerHost] = applicationServer;
            parameters[RfcConfigParameters.SystemNumber] = systemNumber;
            parameters[RfcConfigParameters.Codepage] = codePage;

            destination = RfcDestinationManager.GetDestination(parameters);
            Console.Write(AttributesToString());
        }

        public void Connect(string name, string applicationServer, string user, string password, string language,
            string client, string systemNumber, int poolSize, int idleTimeout, string codePage, string sapRouter)
        {
            Dictionary<string, RfcConfigParameters> availableDestinations = new Dictionary<string, RfcConfigParameters>();

            RfcConfigParameters parameters = new RfcConfigParameters();
            parameters[RfcConfigParameters.Name] = name;
            parameters[RfcConfigParameters.MaxPoolSize] = Convert.ToString(poolSize);
            parameters[RfcConfigParameters.IdleTimeout] = Convert.ToString(idleTimeout);
            parameters[RfcConfigParameters.User] = user;
            parameters[RfcConfigParameters.Password] = password;
            parameters[RfcConfigParameters.Client] = client;
            parameters[RfcConfigParameters.Language] = language;
            parameters[RfcConfigParameters.AppServerHost] = applicationServer;
            parameters[RfcConfigParameters.SystemNumber] = systemNumber;
            parameters[RfcConfigParameters.Codepage] = codePage;
            parameters[RfcConfigParameters.SAPRouter] = sapRouter;
            destination = RfcDestinationManager.GetDestination(parameters);
            //Console.Write(AttributesToString());
        }

        private String AttributesToString()
        {
            if (destination == null)
            {
                throw new Exception("Can't find sap host destination!");
            }

            RfcSystemAttributes attr = destination.SystemAttributes;
            string result = "Attributes:\n";
            result += "Destination " + attr.Destination + "\n";
            result += "HostName " + attr.HostName + "\n";
            result += "User " + attr.User + "\n";
            result += "Client " + attr.Client + "\n";
            result += "ISOLanguage " + attr.ISOLanguage + "\n";
            result += "Language " + attr.Language + "\n";
            result += "KernelRelease " + attr.KernelRelease + "\n";
            result += "CodePage " + attr.CodePage + "\n";
            result += "PartnerCodePage " + attr.PartnerCodePage + "\n";
            result += "PartnerHost " + attr.PartnerHost + "\n";
            result += "PartnerRelease " + attr.PartnerRelease + "\n";
            result += "PartnerReleaseNumber " + attr.PartnerReleaseNumber + "\n";
            result += "PartnerType " + attr.PartnerType + "\n";
            result += "Release " + attr.Release + "\n";
            result += "RfcRole " + attr.RfcRole + "\n";
            result += "SystemID " + attr.SystemID + "\n";
            result += "SystemNumber " + attr.SystemNumber + "\n";
            return result;
        }
        #endregion

        #region Test Connect
        public void TestConnect()
        {
            if (destination != null)
            {
                destination.Ping();
            }
        }
        #endregion

        #region Execute
        public string FunctionName
        {
            get
            {
                return functionName;
            }
            set
            {
                functionName = value;
            }
        }

        public Dictionary<string, object> ImportParameters
        {
            get
            {
                return importParameters;
            }
            set
            {
                importParameters = value;
            }
        }

        public Dictionary<string, object> ExportParameters
        {
            get
            {
                return exportParameters;
            }
            set
            {
                exportParameters = value;
            }
        }

        public DataSet Execute()
        {
            if (functionName == null)
            {
                throw new Exception("Please set function name!");
            }

            exportParameters = new Dictionary<string, object>();
            return CallFunction(functionName, importParameters, ref exportParameters);
        }

        public DataSet Execute(ref Dictionary<string, object> exportParameters)
        {
            if (functionName == null)
            {
                throw new Exception("Please set function name!");
            }

            return CallFunction(functionName, importParameters, ref exportParameters);
        }

        private DataSet CallFunction(string functionName, Dictionary<string, object> importParameters, ref Dictionary<string, object> exportParameters)
        {
            IRfcFunction function = null;
            Dictionary<string, IRfcTable> tables = new Dictionary<string, IRfcTable>();
            Dictionary<string, object> exports = new Dictionary<string, object>();

            try
            {
                //get metadata repository associated with the destination
                //fetch or get cached function metadata and 
                //create a function container based on the function metadata
                function = destination.Repository.CreateFunction(functionName);

                //find import parameter
                //The .Net Connector runtime always trys to find
                //a suitable conversion between C# data types and ABAP data types.
                for (int i = 0; i < function.Metadata.ParameterCount; i++)
                {
                    object parameter = null;
                    if (importParameters != null)
                    {
                        if (importParameters.TryGetValue(function.Metadata[i].Name, out parameter))
                        {
                            function.SetValue(function.Metadata[i].Name, parameter);
                        }
                    }
                    switch (function.Metadata[i].Direction)
                    {
                        case RfcDirection.CHANGING:
                            break;

                        case RfcDirection.EXPORT:
                            exports.Add(function.Metadata[i].Name, function.GetStructure(function.Metadata[i].Name));
                            break;

                        case RfcDirection.IMPORT:
                            break;

                        case RfcDirection.TABLES:
                            tables.Add(function.Metadata[i].Name, function.GetTable(function.Metadata[i].Name));
                            break;

                        default:
                            break;
                    }
                }

                function.Invoke(destination); //make the remote call

                //get dataset
                DataSet dataSet = new DataSet();
                foreach (string key in tables.Keys)
                {
                    DataTable dataTable = ConvertRFCTabletoDataTable(tables[key]);
                    dataTable.TableName = key;
                    dataSet.Tables.Add(dataTable);
                }

                //get export
                if (exportParameters != null)
                {
                    exportParameters = exports;
                }

                return dataSet;
            }
            catch (RfcCommunicationException e)
            {
                // network problem...
                //throw new Exception("network problem", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapRuntimeException e)
            {
                // serious problem on ABAP system side...
                //throw new Exception("serious problem on ABAP system side", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapBaseException e)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                //throw new Exception("The function module returned an ABAP exception, an ABAP message or an ABAP class-based exception...",e);
                throw new Exception(e.Message, e);
            }
            catch (RfcBaseException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw new Exception(message + ex.Message);
            }
        }

        private DataTable ConvertRFCTabletoDataTable(IRfcTable table)
        {
            try
            {
                DataTable resultTable = new DataTable();

                for (int i = 0; i < table.Metadata.LineType.FieldCount; i++)
                {
                    resultTable.Columns.Add(table.Metadata.LineType[i].Name);
                }

                for (int i = 0; i < table.RowCount; i++)
                {
                    table.CurrentIndex = i;
                    DataRow row = resultTable.NewRow();

                    foreach (DataColumn item in resultTable.Columns)
                    {
                        //edited by kevin at 2011-09-23
                        #region 把RFCTable的值转换成DataSet的值
                        if (table.Metadata.LineType[item.ColumnName].DataType.ToString() == "BCD")   //BCD为值类型
                        {
                            row[item.ColumnName] = table.GetDecimal(item.ColumnName);
                        }
                        else
                        {
                            row[item.ColumnName] = table.GetObject(item.ColumnName);
                        }
                        #endregion
                    }
                    resultTable.Rows.Add(row);
                }
                return resultTable;
            }
            catch (RfcCommunicationException e)
            {
                // network problem...
                //throw new Exception("network problem", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapRuntimeException e)
            {
                // serious problem on ABAP system side...
                //throw new Exception("serious problem on ABAP system side", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapBaseException e)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                //throw new Exception("The function module returned an ABAP exception, an ABAP message or an ABAP class-based exception...",e);
                throw new Exception(e.Message, e);
            }
            catch (RfcBaseException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw new Exception(message + ex.Message);
            }
        }

        //edited by Serena.Shi at 2011-09-27
        #region 把DataSet的值转换成RFCTable的值
        public IRfcTable ConvertDataTabletoRFCTable(DataTable dt, string tableName)
        {
            try
            {
                IRfcFunction function = null;
                function = destination.Repository.CreateFunction(FunctionName);
                IRfcTable ROFTable = function.GetTable(tableName);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ROFTable.Insert();
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ROFTable.CurrentRow.SetValue(dt.Columns[j].ColumnName, dt.Rows[i][j].ToString());
                    }
                }

                return ROFTable;
            }
            catch (RfcCommunicationException e)
            {
                // network problem...
                //throw new Exception("network problem", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapRuntimeException e)
            {
                // serious problem on ABAP system side...
                //throw new Exception("serious problem on ABAP system side", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapBaseException e)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                //throw new Exception("The function module returned an ABAP exception, an ABAP message or an ABAP class-based exception...",e);
                throw new Exception(e.Message, e);
            }
            catch (RfcBaseException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw new Exception(message + ex.Message);
            }
        }

        public IRfcStructure ConvertDataTabletoRFCStructure(DataRow dr, string tableName)
        {
            try
            {
                IRfcFunction function = null;
                message = FunctionName + " CreateFunction:";
                function = destination.Repository.CreateFunction(FunctionName);
                message = FunctionName + " GetStructure:";
                IRfcStructure ROFStrcture = function.GetStructure(tableName);

                //ROFTable.Insert();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    ROFStrcture.SetValue(dr.Table.Columns[i].ColumnName, dr[i].ToString());
                }

                return ROFStrcture;
            }
            catch (RfcCommunicationException e)
            {
                // network problem...
                //throw new Exception("network problem", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapRuntimeException e)
            {
                // serious problem on ABAP system side...
                //throw new Exception("serious problem on ABAP system side", e);
                throw new Exception(e.Message, e);
            }
            catch (RfcAbapBaseException e)
            {
                // The function module returned an ABAP exception, an ABAP message
                // or an ABAP class-based exception...
                //throw new Exception("The function module returned an ABAP exception, an ABAP message or an ABAP class-based exception...",e);
                throw new Exception(e.Message, e);
            }
            catch (RfcBaseException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw new Exception(message + ex.Message);
            }
        }

        #endregion

        #endregion
    }
}
