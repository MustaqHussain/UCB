﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UcbWeb.BinaryDataTransferService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AttachmentDC", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Ucb.WebServices.DataContracts")]
    [System.SerializableAttribute()]
    public partial class AttachmentDC : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AttachmentTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IncidentCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LoadedByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime LoadedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private byte[] RowIdentifierField;
        
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
        public string AttachmentType {
            get {
                return this.AttachmentTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.AttachmentTypeField, value) != true)) {
                    this.AttachmentTypeField = value;
                    this.RaisePropertyChanged("AttachmentType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Code {
            get {
                return this.CodeField;
            }
            set {
                if ((this.CodeField.Equals(value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid IncidentCode {
            get {
                return this.IncidentCodeField;
            }
            set {
                if ((this.IncidentCodeField.Equals(value) != true)) {
                    this.IncidentCodeField = value;
                    this.RaisePropertyChanged("IncidentCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LoadedBy {
            get {
                return this.LoadedByField;
            }
            set {
                if ((object.ReferenceEquals(this.LoadedByField, value) != true)) {
                    this.LoadedByField = value;
                    this.RaisePropertyChanged("LoadedBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime LoadedDate {
            get {
                return this.LoadedDateField;
            }
            set {
                if ((this.LoadedDateField.Equals(value) != true)) {
                    this.LoadedDateField = value;
                    this.RaisePropertyChanged("LoadedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public byte[] RowIdentifier {
            get {
                return this.RowIdentifierField;
            }
            set {
                if ((object.ReferenceEquals(this.RowIdentifierField, value) != true)) {
                    this.RowIdentifierField = value;
                    this.RaisePropertyChanged("RowIdentifier");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceErrorFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Ucb.WebServices.FaultContracts")]
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BinaryDataTransferService.IBinaryDataTransferService")]
    public interface IBinaryDataTransferService {
        
        // CODEGEN: Generating message contract since the wrapper name (UploadAttachmentRequest) of message UploadAttachmentRequest does not match the default value (UploadAttachment)
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBinaryDataTransferService/UploadAttachment", ReplyAction="http://tempuri.org/IBinaryDataTransferService/UploadAttachmentResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.BinaryDataTransferService.ServiceErrorFault), Action="http://tempuri.org/IBinaryDataTransferService/UploadAttachmentServiceErrorFaultFa" +
            "ult", Name="ServiceErrorFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Ucb.WebServices.FaultContracts")]
        UcbWeb.BinaryDataTransferService.UploadAttachmentResponse UploadAttachment(UcbWeb.BinaryDataTransferService.UploadAttachmentRequest request);
        
        // CODEGEN: Generating message contract since the wrapper name (DownloadAttachmentRequest) of message DownloadAttachmentRequest does not match the default value (DownloadAttachment)
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBinaryDataTransferService/DownloadAttachment", ReplyAction="http://tempuri.org/IBinaryDataTransferService/DownloadAttachmentResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(UcbWeb.BinaryDataTransferService.ServiceErrorFault), Action="http://tempuri.org/IBinaryDataTransferService/DownloadAttachmentServiceErrorFault" +
            "Fault", Name="ServiceErrorFault", Namespace="http://schemas.datacontract.org/2004/07/Dwp.Adep.Ucb.WebServices.FaultContracts")]
        UcbWeb.BinaryDataTransferService.DownloadAttachmentResponse DownloadAttachment(UcbWeb.BinaryDataTransferService.DownloadAttachmentRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadAttachmentRequest", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadAttachmentRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public UcbWeb.BinaryDataTransferService.AttachmentDC Attachment;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string UserID;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream DocumentBody;
        
        public UploadAttachmentRequest() {
        }
        
        public UploadAttachmentRequest(UcbWeb.BinaryDataTransferService.AttachmentDC Attachment, string UserID, System.IO.Stream DocumentBody) {
            this.Attachment = Attachment;
            this.UserID = UserID;
            this.DocumentBody = DocumentBody;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadAttachmentResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadAttachmentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool Result;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string[] Messages;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public UcbWeb.BinaryDataTransferService.AttachmentDC Attachment;
        
        public UploadAttachmentResponse() {
        }
        
        public UploadAttachmentResponse(bool Result, string[] Messages, UcbWeb.BinaryDataTransferService.AttachmentDC Attachment) {
            this.Result = Result;
            this.Messages = Messages;
            this.Attachment = Attachment;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadAttachmentRequest", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownloadAttachmentRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.Guid AttachmentCode;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string UserID;
        
        public DownloadAttachmentRequest() {
        }
        
        public DownloadAttachmentRequest(System.Guid AttachmentCode, string UserID) {
            this.AttachmentCode = AttachmentCode;
            this.UserID = UserID;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadAttachmentResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownloadAttachmentResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string[] Messages;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public bool Result;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream DocumentBody;
        
        public DownloadAttachmentResponse() {
        }
        
        public DownloadAttachmentResponse(string[] Messages, bool Result, System.IO.Stream DocumentBody) {
            this.Messages = Messages;
            this.Result = Result;
            this.DocumentBody = DocumentBody;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBinaryDataTransferServiceChannel : UcbWeb.BinaryDataTransferService.IBinaryDataTransferService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BinaryDataTransferServiceClient : System.ServiceModel.ClientBase<UcbWeb.BinaryDataTransferService.IBinaryDataTransferService>, UcbWeb.BinaryDataTransferService.IBinaryDataTransferService {
        
        public BinaryDataTransferServiceClient() {
        }
        
        public BinaryDataTransferServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BinaryDataTransferServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BinaryDataTransferServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BinaryDataTransferServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        UcbWeb.BinaryDataTransferService.UploadAttachmentResponse UcbWeb.BinaryDataTransferService.IBinaryDataTransferService.UploadAttachment(UcbWeb.BinaryDataTransferService.UploadAttachmentRequest request) {
            return base.Channel.UploadAttachment(request);
        }
        
        public bool UploadAttachment(ref UcbWeb.BinaryDataTransferService.AttachmentDC Attachment, string UserID, System.IO.Stream DocumentBody, out string[] Messages) {
            UcbWeb.BinaryDataTransferService.UploadAttachmentRequest inValue = new UcbWeb.BinaryDataTransferService.UploadAttachmentRequest();
            inValue.Attachment = Attachment;
            inValue.UserID = UserID;
            inValue.DocumentBody = DocumentBody;
            UcbWeb.BinaryDataTransferService.UploadAttachmentResponse retVal = ((UcbWeb.BinaryDataTransferService.IBinaryDataTransferService)(this)).UploadAttachment(inValue);
            Messages = retVal.Messages;
            Attachment = retVal.Attachment;
            return retVal.Result;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        UcbWeb.BinaryDataTransferService.DownloadAttachmentResponse UcbWeb.BinaryDataTransferService.IBinaryDataTransferService.DownloadAttachment(UcbWeb.BinaryDataTransferService.DownloadAttachmentRequest request) {
            return base.Channel.DownloadAttachment(request);
        }
        
        public string[] DownloadAttachment(System.Guid AttachmentCode, string UserID, out bool Result, out System.IO.Stream DocumentBody) {
            UcbWeb.BinaryDataTransferService.DownloadAttachmentRequest inValue = new UcbWeb.BinaryDataTransferService.DownloadAttachmentRequest();
            inValue.AttachmentCode = AttachmentCode;
            inValue.UserID = UserID;
            UcbWeb.BinaryDataTransferService.DownloadAttachmentResponse retVal = ((UcbWeb.BinaryDataTransferService.IBinaryDataTransferService)(this)).DownloadAttachment(inValue);
            Result = retVal.Result;
            DocumentBody = retVal.DocumentBody;
            return retVal.Messages;
        }
    }
}
