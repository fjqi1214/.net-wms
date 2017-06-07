using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;
using System.Net;
using System.Web.Services.Protocols;
using System.Web.Services.Description;
using System.Reflection;

// For compile in runtime
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Security.Cryptography.X509Certificates;

namespace BenQGuru.eMES.Web.Helper
{
    public class WebServiceProxy
    {
        private Assembly m_Assembly = null;
        private string m_ProtocolName = "Soap";
        private string m_SourceProxy = string.Empty;
        private string m_NameSpace = string.Empty;
        private string m_TypeName = string.Empty;
        private string m_URL = string.Empty;
        private int m_Timeout = 100000;

        private ICredentials m_Credentials;
        private X509Certificate2Collection m_X509CertCollection;
        private IWebProxy m_Proxy;

        private string m_UserName = string.Empty;
        private string m_Password = string.Empty;

        #region Properties
        /// <summary>
        /// Used to set the webservice's URL
        /// </summary>
        public string URL
        {
            get
            {
                return m_URL;
            }
            set
            {
                m_URL = value;
            }
        }

        /// <summary>
        /// Used to set the timeout of invoking webservice.
        /// </summary>
        public int Timeout
        {
            get
            {
                return m_Timeout;
            }
            set
            {
                m_Timeout = value;
            }
        }

        /// <summary>
        /// Get the assembly combiled by the codedom
        /// </summary>
        public Assembly Assembly
        {
            get
            {
                return m_Assembly;
            }
        }

        /// <summary>
        /// Get and set the protocol to invoke the webservice
        /// </summary>
        public string ProtocolName
        {
            get
            {
                return m_ProtocolName;
            }
            set
            {
                m_ProtocolName = value;
            }
        }

        /// <summary>
        /// Get the source code compiled the language provider
        /// </summary>
        public string SourceProxy
        {
            get
            {
                return m_SourceProxy;
            }
            set
            {
                m_SourceProxy = value;
            }
        }

        /// <summary>
        /// Get and set the namespace of the compiled assembly
        /// </summary>
        public string NameSpace
        {
            get
            {
                return m_NameSpace;
            }
            set
            {
                m_NameSpace = value;
            }
        }

        /// <summary>
        /// Get and set the type name of the compiled proxy class
        /// </summary>
        public string TypeName
        {
            get
            {
                return m_TypeName;
            }
            set
            {
                m_TypeName = value;
            }
        }
        #endregion

        /// <summary>
        /// Generate a proxy by given the WSDL.
        /// </summary>
        /// <param name="wsdlSourceName">wsdl file(URL or a local file path) or content</param>
        public WebServiceProxy(string wsdlSourceName, string nameSpace, string typeName)
        {
            m_NameSpace = nameSpace;
            m_TypeName = typeName;
            AssemblyFromWsdl(GetWsdl(wsdlSourceName));

            System.Net.ServicePointManager.CertificatePolicy = new AcceptAllPolicy();
        }

        /// <summary>
        /// Generate a proxy by given the assembly.
        /// </summary>
        /// <param name="assembly">an existed assembly</param>
        public WebServiceProxy(Assembly assembly, string nameSpace, string typeName)
        {
            m_NameSpace = nameSpace;
            m_TypeName = typeName;
            m_Assembly = assembly;

            System.Net.ServicePointManager.CertificatePolicy = new AcceptAllPolicy();
        }


        /// <summary>
        /// Get a wsdl content from URL
        /// </summary>
        /// <param name="url">http://hi1-boapp/wm/trainingcourse3.asmx?wsdl</param>
        /// <returns>WSDL File content</returns>	
        public string WsdlFromUrl(string url)
        {
            WebRequest req = WebRequest.Create(url);
            WebResponse result = req.GetResponse();
            Stream receiveStream = result.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader streamReader = new StreamReader(receiveStream, encode);
            string wsdl = streamReader.ReadToEnd();
            return wsdl;
        }

        /// <summary>
        /// create  NetworkCredential
        /// </summary>
        /// <param name="userName">user name </param>
        /// <param name="passWord">password</param>
        /// <returns></returns>
        public ICredentials Credential(string userName, string passWord)
        {
            return m_Credentials = new NetworkCredential(userName, passWord);
        }

        /// <summary>
        /// create X509Certificate
        /// </summary>
        public void Certificate(string location)
        {
            if (location.Trim().Length == 0)
            {
                return;
            }

            X509Store store;
            if (string.Compare(location, "LocalMachine", true) == 0)
            {
                store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                this.m_X509CertCollection = (X509Certificate2Collection)store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            }
            else if (string.Compare(location, "CurrentUser", true) == 0)
            {
                store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                this.m_X509CertCollection = (X509Certificate2Collection)store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            }
            else
            {
                string path;
                if (location.IndexOf(":") < 0)
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, location);
                }
                else
                {
                    path = location;
                }
                X509Certificate certificate = X509Certificate2.CreateFromCertFile(path);
                this.m_X509CertCollection = new X509Certificate2Collection();
                this.m_X509CertCollection.Add(certificate);
            }
        }

        /// <summary>
        ///  create  NetworkCredential
        /// </summary>
        /// <param name="proxyAddress">proxy address</param>
        /// <param name="username">user name</param>
        /// <param name="password">password</param>
        /// <param name="domain">domain</param>
        /// <returns>NetworkCredential</returns>
        public IWebProxy WebProxy(string proxyAddress, string username, string password, string domain)
        {
            m_Proxy = new WebProxy(proxyAddress, true);

            if (domain.Length > 0)
            {
                m_Proxy.Credentials = new NetworkCredential(username, password, domain);
            }
            else
            {
                m_Proxy.Credentials = new NetworkCredential(username, password);
            }

            return m_Proxy;
        }

        /// <summary>
        /// Get a wsdl file from URL or a local file
        /// </summary>
        /// <param name="source">string, source</param>
        /// <returns>WSDL File content</returns>
        public string GetWsdl(string source)
        {
            if (source.StartsWith("<?xml version") == true)
            {
                return source;
            }
            else if (source.StartsWith("http://") == true)
            {
                return WsdlFromUrl(source);
            }

            return WsdlFromFile(source);
        }


        /// <summary>
        /// Get WSDL content from a local file
        /// </summary>
        /// <param name="fileFullPathName">C:\trainingcourse3.wsdl</param>
        /// <returns>WSDL Content In String</returns>
        public string WsdlFromFile(string fileFullPathName)
        {
            FileInfo fi = new FileInfo(fileFullPathName);

            if (fi.Extension == ".wsdl")
            {
                FileStream fs = new FileStream(fileFullPathName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                char[] buffer = new char[(int)fs.Length];
                sr.ReadBlock(buffer, 0, (int)fs.Length);

                return new string(buffer);
            }
            throw new Exception("This is no a wsdl file");
        }


        /// <summary>
        /// Dynamic combile a assembely accord the wsdl content
        /// </summary>
        /// <param name="wsdlContent">Wsdl content In string</param>
        /// <returns>Assembly</returns>
        public Assembly AssemblyFromWsdl(string wsdlContent)
        {
            // Xml text reader 
            StringReader wsdlStringReader = new StringReader(wsdlContent);
            XmlTextReader tr = new XmlTextReader(wsdlStringReader);
            ServiceDescription sd = ServiceDescription.Read(tr);
            tr.Close();

            // WSDL service description importer 
            CodeNamespace cns = new CodeNamespace(m_NameSpace);

            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, null, null);
            sdi.ProtocolName = m_ProtocolName;
            sdi.Import(cns, null);

            // source code generation 
            CSharpCodeProvider cscp = new CSharpCodeProvider();
            ICodeGenerator icg = cscp.CreateGenerator();
            StringBuilder srcStringBuilder = new StringBuilder();
            StringWriter sw = new StringWriter(srcStringBuilder);
            icg.GenerateCodeFromNamespace(cns, sw, null);
            m_SourceProxy = srcStringBuilder.ToString();
            sw.Close();

            // assembly compilation. 
            CompilerParameters cp = new CompilerParameters();
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Xml.dll");
            cp.ReferencedAssemblies.Add("System.Web.Services.dll");
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            cp.IncludeDebugInformation = false;
            ICodeCompiler icc = cscp.CreateCompiler();
            CompilerResults cr = icc.CompileAssemblyFromSource(cp, m_SourceProxy);
            if (cr.Errors.Count > 0)
                throw new Exception(string.Format("Build failed: {0} errors",
                    cr.Errors.Count));
            return m_Assembly = cr.CompiledAssembly;
        }


        /// <summary>
        /// Create an instance of the assembly according the given name
        /// </summary>
        /// <param name="objTypeName">Object Name</param>
        /// <returns>Object</returns>
        public object CreateInstance()
        {
            object[] parameters = new object[1];

            Type type = m_Assembly.GetType(m_NameSpace + "." + m_TypeName, true, true);
            if (m_URL != string.Empty)
            {
                //Throw assembly to access webservice
                parameters.SetValue(m_URL, 0);
                return Activator.CreateInstance(type, parameters);
            }
            else
            {
                //Throw wsdl to access webservice, Url exist in wsdl
                return Activator.CreateInstance(type);
            }
        }


        /// <summary>
        /// Use reflection to invoke an instance's methods by given method name
        /// </summary>
        /// <param name="obj">intance</param>
        /// <param name="methodName">Method Name</param>
        /// <param name="args">Method's parameters</param>
        /// <returns>object</returns>
        public object Invoke(object obj, string methodName, object[] args)
        {
            Type[] typeArray;
            if (args.Length == 0)
            {
                typeArray = new Type[] { };
            }
            else
            {
                typeArray = new Type[args.Length];
                for (int i = 0; i < args.Length; i++)
                {
                    typeArray[i] = args[i].GetType();
                }
            }

            if (m_Credentials != null)
            {
                System.Uri uri = new Uri(((SoapHttpClientProtocol)obj).Url);
                ((SoapHttpClientProtocol)obj).PreAuthenticate = true;
                ((SoapHttpClientProtocol)obj).Credentials = m_Credentials.GetCredential(uri, "");
            }

            if (m_X509CertCollection != null)
            {
                System.Uri uri = new Uri(((SoapHttpClientProtocol)obj).Url);
                ((SoapHttpClientProtocol)obj).PreAuthenticate = true;
                ((SoapHttpClientProtocol)obj).ClientCertificates.AddRange(m_X509CertCollection);
            }

            if (m_Proxy != null)
            {
                ((SoapHttpClientProtocol)obj).Proxy = m_Proxy;
            }

            ((SoapHttpClientProtocol)obj).Timeout = m_Timeout;

            MethodInfo mi = obj.GetType().GetMethod(methodName, typeArray);
            return mi.Invoke(obj, args);
        }

        /// <summary>
        /// Use reflection to invoke an instance's methods by given method name
        /// </summary>
        /// <param name="methodName">Method Name</param>
        /// <param name="args">Method's parameters</param>
        /// <returns>object</returns>
        public object Invoke(string methodName, object[] args)
        {
            object obj = CreateInstance();
            return Invoke(obj, methodName, args);
        } 
    }
}
