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
        #region Behaviour for IncidentInterestedParty
    
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentInterestedPartyVMDC CreateIncidentInterestedParty(string userName, string currentUserName, string appID, string overrideID, IncidentInterestedPartyDC dc);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentInterestedPartyVMDC GetIncidentInterestedParty(string userName, string currentUserName, string appID, string overrideID, string code);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	List<IncidentInterestedPartyDC> GetAllIncidentInterestedParty(string userName, string currentUserName, string appID, string overrideID);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentInterestedPartySearchVMDC SearchIncidentInterestedParty(string userName, string currentUserName, string appID, string overrideID, IncidentInterestedPartySearchCriteriaDC searchCriteria, int page, int pageSize);
    	
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentInterestedPartyVMDC UpdateIncidentInterestedParty(string userName, string currentUserName, string appID, string overrideID, IncidentInterestedPartyDC dc);
    
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	void DeleteIncidentInterestedParty(string userName, string currentUserName, string appID, string overrideID, string dcCode, string lockID);

        #endregion
    }
}
