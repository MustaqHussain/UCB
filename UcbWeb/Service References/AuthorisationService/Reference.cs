﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UcbWeb.AuthorisationService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuthorisationDC", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
        ".DataContracts")]
    [System.SerializableAttribute()]
    public partial class AuthorisationDC : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] RolesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] Roles {
            get {
                return this.RolesField;
            }
            set {
                if ((object.ReferenceEquals(this.RolesField, value) != true)) {
                    this.RolesField = value;
                    this.RaisePropertyChanged("Roles");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserID {
            get {
                return this.UserIDField;
            }
            set {
                if ((object.ReferenceEquals(this.UserIDField, value) != true)) {
                    this.UserIDField = value;
                    this.RaisePropertyChanged("UserID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceErrorFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
        ".FaultContracts")]
    [System.SerializableAttribute()]
    public partial class ServiceErrorFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuthorisationFailureFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
        ".FaultContracts")]
    [System.SerializableAttribute()]
    public partial class AuthorisationFailureFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="StaffAccessDC", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
        ".DataContracts")]
    [System.SerializableAttribute()]
    public partial class StaffAccessDC : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ApplicationNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsSpecificOrganisationAccessRequiredField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int OrganisationIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrganisationNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ApplicationName {
            get {
                return this.ApplicationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ApplicationNameField, value) != true)) {
                    this.ApplicationNameField = value;
                    this.RaisePropertyChanged("ApplicationName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsSpecificOrganisationAccessRequired {
            get {
                return this.IsSpecificOrganisationAccessRequiredField;
            }
            set {
                if ((this.IsSpecificOrganisationAccessRequiredField.Equals(value) != true)) {
                    this.IsSpecificOrganisationAccessRequiredField = value;
                    this.RaisePropertyChanged("IsSpecificOrganisationAccessRequired");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int OrganisationID {
            get {
                return this.OrganisationIDField;
            }
            set {
                if ((this.OrganisationIDField.Equals(value) != true)) {
                    this.OrganisationIDField = value;
                    this.RaisePropertyChanged("OrganisationID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrganisationName {
            get {
                return this.OrganisationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.OrganisationNameField, value) != true)) {
                    this.OrganisationNameField = value;
                    this.RaisePropertyChanged("OrganisationName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AuthorisationService.IAuthorisationService")]
    public interface IAuthorisationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthorisationService/GetUserAuthorisationInfo", ReplyAction="http://tempuri.org/IAuthorisationService/GetUserAuthorisationInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.AuthorisationService.ServiceErrorFault), Action="http://tempuri.org/IAuthorisationService/GetUserAuthorisationInfoServiceErrorFaul" +
            "tFault", Name="ServiceErrorFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
            ".FaultContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.AuthorisationService.AuthorisationFailureFault), Action="http://tempuri.org/IAuthorisationService/GetUserAuthorisationInfoAuthorisationFai" +
            "lureFaultFault", Name="AuthorisationFailureFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
            ".FaultContracts")]
        UcbWeb.AuthorisationService.AuthorisationDC GetUserAuthorisationInfo(string token, string appID, string[] roles);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthorisationService/GetUserRoles", ReplyAction="http://tempuri.org/IAuthorisationService/GetUserRolesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.AuthorisationService.ServiceErrorFault), Action="http://tempuri.org/IAuthorisationService/GetUserRolesServiceErrorFaultFault", Name="ServiceErrorFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
            ".FaultContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.AuthorisationService.AuthorisationFailureFault), Action="http://tempuri.org/IAuthorisationService/GetUserRolesAuthorisationFailureFaultFau" +
            "lt", Name="AuthorisationFailureFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
            ".FaultContracts")]
        string[] GetUserRoles(string currentUser, string user, string appID, string overrideID, string userID, System.Nullable<System.Guid> applicationCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthorisationService/GetUserApplicationInfo", ReplyAction="http://tempuri.org/IAuthorisationService/GetUserApplicationInfoResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.AuthorisationService.ServiceErrorFault), Action="http://tempuri.org/IAuthorisationService/GetUserApplicationInfoServiceErrorFaultF" +
            "ault", Name="ServiceErrorFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
            ".FaultContracts")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.AuthorisationService.AuthorisationFailureFault), Action="http://tempuri.org/IAuthorisationService/GetUserApplicationInfoAuthorisationFailu" +
            "reFaultFault", Name="AuthorisationFailureFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Framework.Management.WebServices" +
            ".FaultContracts")]
        UcbWeb.AuthorisationService.StaffAccessDC[] GetUserApplicationInfo(string currentUser, string user, string appID, string overrideID, string userID, string[] roles);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthorisationServiceChannel : UcbWeb.AuthorisationService.IAuthorisationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthorisationServiceClient : System.ServiceModel.ClientBase<UcbWeb.AuthorisationService.IAuthorisationService>, UcbWeb.AuthorisationService.IAuthorisationService {
        
        public AuthorisationServiceClient() {
        }
        
        public AuthorisationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthorisationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthorisationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthorisationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UcbWeb.AuthorisationService.AuthorisationDC GetUserAuthorisationInfo(string token, string appID, string[] roles) {
            return base.Channel.GetUserAuthorisationInfo(token, appID, roles);
        }
        
        public string[] GetUserRoles(string currentUser, string user, string appID, string overrideID, string userID, System.Nullable<System.Guid> applicationCode) {
            return base.Channel.GetUserRoles(currentUser, user, appID, overrideID, userID, applicationCode);
        }
        
        public UcbWeb.AuthorisationService.StaffAccessDC[] GetUserApplicationInfo(string currentUser, string user, string appID, string overrideID, string userID, string[] roles) {
            return base.Channel.GetUserApplicationInfo(currentUser, user, appID, overrideID, userID, roles);
        }
    }
}
