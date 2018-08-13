using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Dwp.Adep.Ucb.WebServices.DataContracts;
using Dwp.Adep.Ucb.WebServices.Exceptions;
using Dwp.Adep.Ucb.DataServices;
using Dwp.Adep.Ucb.DataServices.Models;
using Dwp.Adep.Ucb.WebServices.ServiceContracts;
using Dwp.Adep.Ucb.WebServices.Constants;
using Dwp.Adep.Ucb.WebServices.MessageContracts.Exceptions;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{

    /// <summary>
    /// Service
    /// Class containing service behaviour for Incident
    /// </summary>
    public partial class UcbService
    {
        #region IntranetStaffProtection

        public IntranetStaffProtectionResult IntranetStaffProtection(string currentUser, string user, string appID, string overrideID, string nino)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            return IntranetStaffProtection(currentUser, user, appID, overrideID, nino, incidentRepository, uow, exceptionManager);

        }

        private IntranetStaffProtectionResult IntranetStaffProtection(string currentUser, string user, string appID, string overrideID,
            string nino,
            IRepository<Incident> incidentRepository,
            IUnitOfWork uow,
            IExceptionManager exceptionManager)
        {

            IntranetStaffProtectionResult searchResult = new IntranetStaffProtectionResult();
            try
            {

                using (uow)
                {

                    if (string.IsNullOrEmpty(nino))
                        throw new ArgumentNullException("NationalInsuranceNo");

                    ISpecification<Incident> incidentSpecification = new Specification<Incident>(x => x.Customer.NINO.Equals(nino) && x.IncidentStatus.Equals("Live"));

                    var result = incidentRepository.Find(incidentSpecification, x => x.Customer.FirstName, "Customer", "Customer.CustomerControlMeasure", "Customer.CustomerControlMeasure.ControlMeasure", "Customer.RelationshipToCustomer");

                    foreach (var incident in result)
                    {
                        searchResult.FirstName += incident.Customer.FirstName + "; ";
                        if (incident.Customer.CustomerControlMeasure.Count > 0)
                            foreach (var ccm in incident.Customer.CustomerControlMeasure ) //.Where(x => x.ControlMeasure.IsActive == true))
                            {
                                if (ccm.ControlMeasure.ControlMeasureDescription != null)
                                    searchResult.ControlMeasures += ccm.ControlMeasure.ControlMeasureDescription + "; ";
                            }

                        if (incident.Customer.RelationshipToCustomer != null)
                            searchResult.RelationShip += incident.Customer.RelationshipToCustomer.Description + "; ";

                        searchResult.BannedOfficeOfficeEndDate += string.Format("{0} {1}{2}", incident.BannedFromOffices, incident.BannedFromOfficesEndDate, "; ");
                        searchResult.NamedOfficerNameContact += string.Format("{0} {1}{2}", incident.NamedOfficer, incident.TelephoneContactNumber, "; ");
                    }

                    //clean the output
                    if (searchResult.BannedOfficeOfficeEndDate != null)
                        searchResult.BannedOfficeOfficeEndDate = searchResult.BannedOfficeOfficeEndDate.TrimEnd(';', ' '); 
                    if (searchResult.NamedOfficerNameContact != null)
                        searchResult.NamedOfficerNameContact = searchResult.NamedOfficerNameContact.TrimEnd(';', ' ');
                    if (searchResult.ControlMeasures != null)
                        searchResult.ControlMeasures = searchResult.ControlMeasures.TrimEnd(';', ' ');
                    if (searchResult.FirstName != null)
                        searchResult.FirstName = searchResult.FirstName.TrimEnd(';', ' ');
                    if (searchResult.RelationShip != null)
                        searchResult.RelationShip = searchResult.RelationShip.TrimEnd(';', ' ');

                    // Audit the fact this NINO was searched
                    string auditText = AuditConstants.AUDIT_TEXT_STAFF_PROTECTION_NINO_SEARCH;
                    Dictionary<string, string> auditProperties = new Dictionary<string, string>();
                    auditProperties.Add(AuditConstants.NINO_PROPERTY, nino);

                    uow.CustomAudit(auditText, AuditConstants.SEARCH_ACTION, auditProperties);
                    uow.Commit();

                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }

            return searchResult;
        }
        #endregion
    }
}