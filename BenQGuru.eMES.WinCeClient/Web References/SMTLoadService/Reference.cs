﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5485
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.CompactFramework.Design.Data 2.0.50727.5485 版自动生成。
// 
namespace BenQGuru.eMES.WinCeClient.SMTLoadService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SMTLoadServiceSoap", Namespace="http://tempuri.org/")]
    public partial class SMTLoadService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        /// <remarks/>
        public SMTLoadService() {
            this.Url = "http://localhost/LongCheer/BenQGuru.eMES.Web.WebService/SMTLoadService.asmx";
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginHelloWorld(System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("HelloWorld", new object[0], callback, asyncState);
        }
        
        /// <remarks/>
        public string EndHelloWorld(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SMTLoadFeeder", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SMTLoadFeeder(string Mocode, string MachineCode, string StationTable, string ReelNo, string StationCode, string FeederCode, string userCode, string rescode, bool stationTableGroupActive) {
            object[] results = this.Invoke("SMTLoadFeeder", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        ReelNo,
                        StationCode,
                        FeederCode,
                        userCode,
                        rescode,
                        stationTableGroupActive});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSMTLoadFeeder(string Mocode, string MachineCode, string StationTable, string ReelNo, string StationCode, string FeederCode, string userCode, string rescode, bool stationTableGroupActive, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SMTLoadFeeder", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        ReelNo,
                        StationCode,
                        FeederCode,
                        userCode,
                        rescode,
                        stationTableGroupActive}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSMTLoadFeeder(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetSMTFeederMatrialTableGroup", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] GetSMTFeederMatrialTableGroup(string MoCode, string rescode, string machineCode) {
            object[] results = this.Invoke("GetSMTFeederMatrialTableGroup", new object[] {
                        MoCode,
                        rescode,
                        machineCode});
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetSMTFeederMatrialTableGroup(string MoCode, string rescode, string machineCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetSMTFeederMatrialTableGroup", new object[] {
                        MoCode,
                        rescode,
                        machineCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public string[] EndGetSMTFeederMatrialTableGroup(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetActiveStationTable", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetActiveStationTable(string moCode, string rescode, string machineCode) {
            object[] results = this.Invoke("GetActiveStationTable", new object[] {
                        moCode,
                        rescode,
                        machineCode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginGetActiveStationTable(string moCode, string rescode, string machineCode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetActiveStationTable", new object[] {
                        moCode,
                        rescode,
                        machineCode}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndGetActiveStationTable(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SMTExchanges", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SMTExchanges(string Mocode, string MachineCode, string StationTable, string OldReelNo, string ReelNo, string StationCode, string OldFeederCode, string FeederCode, string userCode, string rescode, bool stationTableGroupActive) {
            object[] results = this.Invoke("SMTExchanges", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        OldReelNo,
                        ReelNo,
                        StationCode,
                        OldFeederCode,
                        FeederCode,
                        userCode,
                        rescode,
                        stationTableGroupActive});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSMTExchanges(string Mocode, string MachineCode, string StationTable, string OldReelNo, string ReelNo, string StationCode, string OldFeederCode, string FeederCode, string userCode, string rescode, bool stationTableGroupActive, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SMTExchanges", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        OldReelNo,
                        ReelNo,
                        StationCode,
                        OldFeederCode,
                        FeederCode,
                        userCode,
                        rescode,
                        stationTableGroupActive}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSMTExchanges(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SMTChangeReal", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SMTChangeReal(string Mocode, string MachineCode, string StationTable, string OldReelNo, string ReelNo, string StationCode, string userCode, string rescode, bool stationTableGroupActive) {
            object[] results = this.Invoke("SMTChangeReal", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        OldReelNo,
                        ReelNo,
                        StationCode,
                        userCode,
                        rescode,
                        stationTableGroupActive});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSMTChangeReal(string Mocode, string MachineCode, string StationTable, string OldReelNo, string ReelNo, string StationCode, string userCode, string rescode, bool stationTableGroupActive, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SMTChangeReal", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        OldReelNo,
                        ReelNo,
                        StationCode,
                        userCode,
                        rescode,
                        stationTableGroupActive}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSMTChangeReal(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SMTContinue", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SMTContinue(string Mocode, string MachineCode, string StationTable, string OldReelNo, string ReelNo, string StationCode, string userCode, string rescode) {
            object[] results = this.Invoke("SMTContinue", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        OldReelNo,
                        ReelNo,
                        StationCode,
                        userCode,
                        rescode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSMTContinue(string Mocode, string MachineCode, string StationTable, string OldReelNo, string ReelNo, string StationCode, string userCode, string rescode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SMTContinue", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        OldReelNo,
                        ReelNo,
                        StationCode,
                        userCode,
                        rescode}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSMTContinue(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SMTReturn", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SMTReturn(string Mocode, string MachineCode, string StationTable, string ReelNo, string StationCode, string FeederCode, string userCode, string rescode, bool stationTableGroupActive) {
            object[] results = this.Invoke("SMTReturn", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        ReelNo,
                        StationCode,
                        FeederCode,
                        userCode,
                        rescode,
                        stationTableGroupActive});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSMTReturn(string Mocode, string MachineCode, string StationTable, string ReelNo, string StationCode, string FeederCode, string userCode, string rescode, bool stationTableGroupActive, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SMTReturn", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        ReelNo,
                        StationCode,
                        FeederCode,
                        userCode,
                        rescode,
                        stationTableGroupActive}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSMTReturn(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SMTCheck", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SMTCheck(string Mocode, string MachineCode, string StationTable, string ReelNo, string StationCode, string userCode, string rescode) {
            object[] results = this.Invoke("SMTCheck", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        ReelNo,
                        StationCode,
                        userCode,
                        rescode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSMTCheck(string Mocode, string MachineCode, string StationTable, string ReelNo, string StationCode, string userCode, string rescode, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SMTCheck", new object[] {
                        Mocode,
                        MachineCode,
                        StationTable,
                        ReelNo,
                        StationCode,
                        userCode,
                        rescode}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSMTCheck(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
    }
}
