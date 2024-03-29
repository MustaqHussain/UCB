//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ServiceModel;
using Dwp.Adep.Ucb.WebServices.DataContracts;
using Dwp.Adep.Ucb.WebServices.FaultContracts;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial interface IUcbService
    {
        #region Behaviour for Attachment
    
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	AttachmentVMDC CreateAttachment(string userName, string currentUserName, string appID, string overrideID, AttachmentDC dc);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	AttachmentVMDC GetAttachment(string userName, string currentUserName, string appID, string overrideID, string code);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	List<AttachmentDC> GetAllAttachment(string userName, string currentUserName, string appID, string overrideID);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	AttachmentSearchVMDC SearchAttachment(string userName, string currentUserName, string appID, string overrideID, AttachmentSearchCriteriaDC searchCriteria, int page, int pageSize);
    	
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	AttachmentVMDC UpdateAttachment(string userName, string currentUserName, string appID, string overrideID, AttachmentDC dc);
    
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	void DeleteAttachment(string userName, string currentUserName, string appID, string overrideID, string dcCode, string lockID);

        #endregion
    }
}
