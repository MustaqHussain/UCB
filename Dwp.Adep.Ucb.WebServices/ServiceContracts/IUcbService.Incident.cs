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
        #region Behaviour for Incident 

        [FaultContract(typeof(UniqueConstraintFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        TransferSiteSearchVMDC SearchTransferSites(string currentUser, string user, string appID, string overrideID, TransferSiteSearchCriteriaDC searchCriteria, int page, int pageSize);

        [FaultContract(typeof(UniqueConstraintFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        IncidentVMDC CreateIncident(string currentUser, string user, string appID, string overrideID, string currentUserNameFromAD, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        IncidentVMDC GetIncident(string userName, string currentUserName, string appID, string overrideID, string code, string locale);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        List<IncidentDC> GetAllIncident(string userName, string currentUserName, string appID, string overrideID);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        IncidentSearchVMDC SearchIncident(string userName, string currentUserName, string appID, string overrideID, IncidentSearchCriteriaDC searchCriteria, int page, int pageSize);

        [FaultContract(typeof(UniqueConstraintFault))]
        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        IncidentVMDC LineManagerUpdateIncident(string currentUser, string user, string appID, string overrideID, string currentUserNameFromAd, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC, NarrativeDC lineManagerNarrativeDC);

        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        void DeleteIncident(string currentUser, string user, string appID, string overrideID, Guid code, byte[] lockID);

        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        IncidentVMDC NominatedManagerUpdateIncident(string currentUser, string user, string appID, string overrideID, string incidentStatus, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC, NarrativeDC lineManagerNarrativeDC, NarrativeDC furtherInfoNarrativeDC, NarrativeDC deficienciesNarrativeDC, NarrativeDC reviewActionNarrativeDC, List<String> contingencyArrangementCodes, List<String> controlMeasureCodes, List<String> systemMarkedCodes, List<String> interestedPartyCodes);

        [FaultContract(typeof(UniqueConstraintFault))]
        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        IncidentDC TransferIncidentToNewNominatedManager(string currentUser, string user, string appID, string overrideID, IncidentDC incidentDC, string siteCode);

        [FaultContract(typeof(UniqueConstraintFault))]
        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        IncidentVMDC UpdateReferral(string currentUser, string user, string appID, string overrideID, string incidentStatus, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC furtherInfoNarrativeDC, NarrativeDC reviewActionNarrativeDC, List<String> controlMeasureCodes, List<String> systemMarkedCodes, List<String> interestedPartyCodes);
        #endregion
    }
}
