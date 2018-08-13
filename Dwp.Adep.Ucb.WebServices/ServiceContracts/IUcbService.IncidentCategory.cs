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
        #region Behaviour for IncidentCategory
    
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentCategoryVMDC CreateIncidentCategory(string userName, string currentUserName, string appID, string overrideID, IncidentCategoryDC dc);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentCategoryVMDC GetIncidentCategory(string userName, string currentUserName, string appID, string overrideID, string code);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	List<IncidentCategoryDC> GetAllIncidentCategory(string userName, string currentUserName, string appID, string overrideID, bool includeInActive);
    
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentCategorySearchVMDC SearchIncidentCategory(string userName, string currentUserName, string appID, string overrideID, IncidentCategorySearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive);
    	
    	[FaultContract(typeof(UniqueConstraintFault))]
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	IncidentCategoryVMDC UpdateIncidentCategory(string userName, string currentUserName, string appID, string overrideID, IncidentCategoryDC dc);
    
    	[FaultContract(typeof(DataConcurrencyFault))]
    	[FaultContract(typeof(DataIntegrityFault))]
    	[FaultContract(typeof(ServiceErrorFault))]
    	[OperationContract]
    	void DeleteIncidentCategory(string userName, string currentUserName, string appID, string overrideID, string dcCode, string lockID);

        #endregion
    }
}