﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.THIRD_SERVICE {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="THIRD_SERVICE.ThirdServiceSoap")]
    public interface ThirdServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<string> HelloWorldAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LOGINSMTPULLLISTENTRY", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool LOGINSMTPULLLISTENTRY(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/LOGINSMTPULLLISTENTRY", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> LOGINSMTPULLLISTENTRYAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QTYPERREELPULLLISTENTRY", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int QTYPERREELPULLLISTENTRY(string material);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/QTYPERREELPULLLISTENTRY", ReplyAction="*")]
        System.Threading.Tasks.Task<int> QTYPERREELPULLLISTENTRYAsync(string material);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GETDATAFROMSMTPULLLIST", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GETDATAFROMSMTPULLLIST(string bnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GETDATAFROMSMTPULLLIST", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GETDATAFROMSMTPULLLISTAsync(string bnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DELETEFROMSMTPULLLIST", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void DELETEFROMSMTPULLLIST(string material, string bnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/DELETEFROMSMTPULLLIST", ReplyAction="*")]
        System.Threading.Tasks.Task DELETEFROMSMTPULLLISTAsync(string material, string bnum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GETMATERIALQUANTITYPERREEL", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int GETMATERIALQUANTITYPERREEL(string material);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GETMATERIALQUANTITYPERREEL", ReplyAction="*")]
        System.Threading.Tasks.Task<int> GETMATERIALQUANTITYPERREELAsync(string material);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SAVEPULLLISTDATA", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void SAVEPULLLISTDATA(string material, string s_reqQty, int reelQty, string location, string badgeNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SAVEPULLLISTDATA", ReplyAction="*")]
        System.Threading.Tasks.Task SAVEPULLLISTDATAAsync(string material, string s_reqQty, int reelQty, string location, string badgeNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getPalletNoCount", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getPalletNoCount(string doNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getPalletNoCount", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getPalletNoCountAsync(string doNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/gettotalcustpo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] gettotalcustpo(string doNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/gettotalcustpo", ReplyAction="*")]
        System.Threading.Tasks.Task<string[]> gettotalcustpoAsync(string doNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getcustpo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] getcustpo(string doNum, string palletNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getcustpo", ReplyAction="*")]
        System.Threading.Tasks.Task<string[]> getcustpoAsync(string doNum, string palletNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getmodels", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string[] getmodels(string doNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getmodels", ReplyAction="*")]
        System.Threading.Tasks.Task<string[]> getmodelsAsync(string doNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getDOitem", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable getDOitem(string doNum, string palletNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/getDOitem", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> getDOitemAsync(string doNum, string palletNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/checkIfBadgeIsvalid", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool checkIfBadgeIsvalid(string badgeNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/checkIfBadgeIsvalid", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> checkIfBadgeIsvalidAsync(string badgeNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/cyclecountinsert_data_to_table", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string cyclecountinsert_data_to_table(System.Data.DataTable data_from_bin, System.Data.DataTable data_from_sap, string stge_bin, string STOR_LOC, string BADGE_NUM);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/cyclecountinsert_data_to_table", ReplyAction="*")]
        System.Threading.Tasks.Task<string> cyclecountinsert_data_to_tableAsync(System.Data.DataTable data_from_bin, System.Data.DataTable data_from_sap, string stge_bin, string STOR_LOC, string BADGE_NUM);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WHSTOCKSAP", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable WHSTOCKSAP(string STORAGE_LOCATION, string STORAGE_TYPE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/WHSTOCKSAP", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> WHSTOCKSAPAsync(string STORAGE_LOCATION, string STORAGE_TYPE);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ThirdServiceSoapChannel : WebApplication1.THIRD_SERVICE.ThirdServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ThirdServiceSoapClient : System.ServiceModel.ClientBase<WebApplication1.THIRD_SERVICE.ThirdServiceSoap>, WebApplication1.THIRD_SERVICE.ThirdServiceSoap {
        
        public ThirdServiceSoapClient() {
        }
        
        public ThirdServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ThirdServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ThirdServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ThirdServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
        
        public System.Threading.Tasks.Task<string> HelloWorldAsync() {
            return base.Channel.HelloWorldAsync();
        }
        
        public bool LOGINSMTPULLLISTENTRY(string username, string password) {
            return base.Channel.LOGINSMTPULLLISTENTRY(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> LOGINSMTPULLLISTENTRYAsync(string username, string password) {
            return base.Channel.LOGINSMTPULLLISTENTRYAsync(username, password);
        }
        
        public int QTYPERREELPULLLISTENTRY(string material) {
            return base.Channel.QTYPERREELPULLLISTENTRY(material);
        }
        
        public System.Threading.Tasks.Task<int> QTYPERREELPULLLISTENTRYAsync(string material) {
            return base.Channel.QTYPERREELPULLLISTENTRYAsync(material);
        }
        
        public System.Data.DataTable GETDATAFROMSMTPULLLIST(string bnum) {
            return base.Channel.GETDATAFROMSMTPULLLIST(bnum);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GETDATAFROMSMTPULLLISTAsync(string bnum) {
            return base.Channel.GETDATAFROMSMTPULLLISTAsync(bnum);
        }
        
        public void DELETEFROMSMTPULLLIST(string material, string bnum) {
            base.Channel.DELETEFROMSMTPULLLIST(material, bnum);
        }
        
        public System.Threading.Tasks.Task DELETEFROMSMTPULLLISTAsync(string material, string bnum) {
            return base.Channel.DELETEFROMSMTPULLLISTAsync(material, bnum);
        }
        
        public int GETMATERIALQUANTITYPERREEL(string material) {
            return base.Channel.GETMATERIALQUANTITYPERREEL(material);
        }
        
        public System.Threading.Tasks.Task<int> GETMATERIALQUANTITYPERREELAsync(string material) {
            return base.Channel.GETMATERIALQUANTITYPERREELAsync(material);
        }
        
        public void SAVEPULLLISTDATA(string material, string s_reqQty, int reelQty, string location, string badgeNumber) {
            base.Channel.SAVEPULLLISTDATA(material, s_reqQty, reelQty, location, badgeNumber);
        }
        
        public System.Threading.Tasks.Task SAVEPULLLISTDATAAsync(string material, string s_reqQty, int reelQty, string location, string badgeNumber) {
            return base.Channel.SAVEPULLLISTDATAAsync(material, s_reqQty, reelQty, location, badgeNumber);
        }
        
        public System.Data.DataTable getPalletNoCount(string doNum) {
            return base.Channel.getPalletNoCount(doNum);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getPalletNoCountAsync(string doNum) {
            return base.Channel.getPalletNoCountAsync(doNum);
        }
        
        public string[] gettotalcustpo(string doNum) {
            return base.Channel.gettotalcustpo(doNum);
        }
        
        public System.Threading.Tasks.Task<string[]> gettotalcustpoAsync(string doNum) {
            return base.Channel.gettotalcustpoAsync(doNum);
        }
        
        public string[] getcustpo(string doNum, string palletNum) {
            return base.Channel.getcustpo(doNum, palletNum);
        }
        
        public System.Threading.Tasks.Task<string[]> getcustpoAsync(string doNum, string palletNum) {
            return base.Channel.getcustpoAsync(doNum, palletNum);
        }
        
        public string[] getmodels(string doNum) {
            return base.Channel.getmodels(doNum);
        }
        
        public System.Threading.Tasks.Task<string[]> getmodelsAsync(string doNum) {
            return base.Channel.getmodelsAsync(doNum);
        }
        
        public System.Data.DataTable getDOitem(string doNum, string palletNo) {
            return base.Channel.getDOitem(doNum, palletNo);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getDOitemAsync(string doNum, string palletNo) {
            return base.Channel.getDOitemAsync(doNum, palletNo);
        }
        
        public bool checkIfBadgeIsvalid(string badgeNo) {
            return base.Channel.checkIfBadgeIsvalid(badgeNo);
        }
        
        public System.Threading.Tasks.Task<bool> checkIfBadgeIsvalidAsync(string badgeNo) {
            return base.Channel.checkIfBadgeIsvalidAsync(badgeNo);
        }
        
        public string cyclecountinsert_data_to_table(System.Data.DataTable data_from_bin, System.Data.DataTable data_from_sap, string stge_bin, string STOR_LOC, string BADGE_NUM) {
            return base.Channel.cyclecountinsert_data_to_table(data_from_bin, data_from_sap, stge_bin, STOR_LOC, BADGE_NUM);
        }
        
        public System.Threading.Tasks.Task<string> cyclecountinsert_data_to_tableAsync(System.Data.DataTable data_from_bin, System.Data.DataTable data_from_sap, string stge_bin, string STOR_LOC, string BADGE_NUM) {
            return base.Channel.cyclecountinsert_data_to_tableAsync(data_from_bin, data_from_sap, stge_bin, STOR_LOC, BADGE_NUM);
        }
        
        public System.Data.DataTable WHSTOCKSAP(string STORAGE_LOCATION, string STORAGE_TYPE) {
            return base.Channel.WHSTOCKSAP(STORAGE_LOCATION, STORAGE_TYPE);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> WHSTOCKSAPAsync(string STORAGE_LOCATION, string STORAGE_TYPE) {
            return base.Channel.WHSTOCKSAPAsync(STORAGE_LOCATION, STORAGE_TYPE);
        }
    }
}
