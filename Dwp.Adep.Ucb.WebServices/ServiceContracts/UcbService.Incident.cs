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
using Dwp.Adep.Ucb.WebServices.BusinessObjects;
using System.Globalization;
using Dwp.Adep.Ucb.WebServices.Constants;
using AutoMapper;
using Dwp.Adep.Ucb.WebServices.MessageContracts.Exceptions;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{

    /// <summary>
    /// Service
    /// Class containing service behaviour for Incident
    /// </summary>
    public partial class UcbService
    {
        #region Behaviour for Incident

        #region Create

        /// <summary>
        /// Create a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        public IncidentVMDC CreateIncident(string currentUser, string user, string appID, string overrideID, string currentUserNameFromAD, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            Repository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Narrative> narrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Site> siteRepository = new Repository<Site>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return CreateIncident(currentUser, user, appID, overrideID, currentUserNameFromAD, incidentDC, customerDC, incidentNarrativeDC, incidentRepository, customerRepository, 
                narrativeRepository, siteRepository, incidentUpdateEventRepository, uow, exceptionManager);
        }

        /// <summary>
        ///  Create a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public IncidentVMDC CreateIncident(string currentUser, string user, string appID, string overrideID, string currentUserNameFromAd, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC,
            IRepository<Incident> incidentRepository, IRepository<Customer> customerRepository, IRepository<Narrative> narrativeRepository, IRepository<Site> siteRepository,
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == incidentDC) throw new ArgumentOutOfRangeException("incidentDC");
                if (null == customerDC) throw new ArgumentOutOfRangeException("customerDC");
                if (null == incidentNarrativeDC) throw new ArgumentOutOfRangeException("incidentNarrativeDC");
                if (null == incidentRepository) throw new ArgumentOutOfRangeException("incidentRepository");
                if (null == customerRepository) throw new ArgumentOutOfRangeException("customerRepository");
                if (null == narrativeRepository) throw new ArgumentOutOfRangeException("narrativeRepository");
                if (null == siteRepository) throw new ArgumentOutOfRangeException("siteRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                #region Set up Guids and Foreign Keys
                //Create a new ID for the Incident item
                incidentDC.Code = Guid.NewGuid();
                incidentDC.IncidentStatus = "New";
                incidentDC.Type = "Incident";
                incidentDC.NumberOfRecords = 1; //Used for totalling in ad-hoc reports solution
                
                // Get the site for the Home Office Site code provided
                Specification<Site> siteSpecification = new Specification<Site>(x => x.Code == incidentDC.StaffMemberHomeOfficeSiteCode);
                Site site = siteRepository.Find(siteSpecification).Single();
                
                // Save Organisations
                Guid siteOrganisationCode = site.OrganisationCode;
                Guid staffMemberBusinessCode = incidentDC.StaffMemberBusinessCode;
                Guid staffMemberBusinessAreaCode = incidentDC.StaffMemberBusinessAreaCode;
                
                // Site and Home Office Site code are the same initially
                incidentDC.SiteCode = site.Code;
                incidentDC.StaffMemberHomeOfficeSiteCode = site.Code;

                incidentDC.ReviewDate = incidentDC.IncidentDate.AddMonths(12);

                //Create a new ID for the Customer item
                customerDC.Code = Guid.NewGuid();

                //Create a new ID for the Incident Narrative item
                incidentNarrativeDC.Code = Guid.NewGuid();
                incidentNarrativeDC.NarrativeType = "Incident";

                //Link these to customer
                incidentDC.IncidentNarrativeCode = incidentNarrativeDC.Code;
                incidentDC.CustomerCode = customerDC.Code;

                // Map incident data contract to model
                Incident incidentItem = Mapper.Map<IncidentDC, Incident>(incidentDC);

                //Populate FiscalYear, FiscalQuarter and FiscalMonth based on the incident date.
                string fiscalMonthText = CalculateFiscalMonth(incidentItem.IncidentDate.Month);
                string fiscalMonthNumber = CalculateFiscalMonthNumber(incidentItem.IncidentDate.Month);

                PopulateIncidentFiscalDateFields(incidentItem, fiscalMonthText, fiscalMonthNumber);

                //IncidentUpdateEvent
                IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
                incidentUpdateEventItem.Code = Guid.NewGuid();
                incidentUpdateEventItem.DateTime = DateTime.Now;
                incidentUpdateEventItem.IncidentCode = incidentItem.Code;
                incidentUpdateEventItem.Type = "Create";
                incidentUpdateEventItem.UpdateBy = currentUserNameFromAd;

                // Map customer data contract to model
                Customer customerItem = Mapper.Map<CustomerDC, Customer>(customerDC);

                // Map narrative data contract to model
                Narrative incidentNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(incidentNarrativeDC);
                #endregion

                #region Perform DB updates
                using (uow)
                {
                    // Add the new item
                    narrativeRepository.Add(incidentNarrativeItem);
                    customerRepository.Add(customerItem);
                    incidentRepository.Add(incidentItem);
                    incidentUpdateEventRepository.Add(incidentUpdateEventItem);

                    // Commit unit of work
                    uow.Commit();
                }
                #endregion

                //Map updated models back to data contracts (for updated RowIdentifier)
                incidentDC = Mapper.Map<Incident, IncidentDC>(incidentItem);
                customerDC = Mapper.Map<Customer, CustomerDC>(customerItem);
                incidentNarrativeDC = Mapper.Map<Narrative, NarrativeDC>(incidentNarrativeItem);

                // Create aggregate data contract
                IncidentVMDC returnObject = new IncidentVMDC();

                // Add new item to aggregate
                returnObject.IncidentItem = incidentDC;
                returnObject.CustomerItem = customerDC;
                returnObject.IncidentNarrativeItem = incidentNarrativeDC;

                // Reload Site and Organisation fields from saved values
                returnObject.IncidentItem.StaffMemberBusinessCode = staffMemberBusinessCode;
                returnObject.IncidentItem.StaffMemberBusinessAreaCode = staffMemberBusinessAreaCode;
                returnObject.IncidentItem.StaffMemberHomeOfficeCode = siteOrganisationCode;
                returnObject.IncidentItem.OrganisationCode = siteOrganisationCode;

                return returnObject;
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region Line Manager Update

        /// <summary>
        /// Update a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        public IncidentVMDC LineManagerUpdateIncident(string currentUser, string user, string appID, string overrideID,string currentUserNameFromAd, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC, NarrativeDC lineManagerNarrativeDC)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            Repository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Narrative> narrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return LineManagerUpdateIncident(currentUser, user, appID, overrideID, currentUserNameFromAd, incidentDC, customerDC, incidentNarrativeDC, lineManagerNarrativeDC, 
                incidentRepository, customerRepository, narrativeRepository, incidentUpdateEventRepository, uow, exceptionManager);
        }

        /// <summary>
        /// Update a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public IncidentVMDC LineManagerUpdateIncident(string currentUser, string user, string appID, string overrideID,string currentUserNameFromAd, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC,
            NarrativeDC lineManagerNarrativeDC, IRepository<Incident> incidentRepository, IRepository<Customer> customerRepository, IRepository<Narrative> narrativeRepository,
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == incidentDC) throw new ArgumentOutOfRangeException("incidentDC");
                if (null == customerDC) throw new ArgumentOutOfRangeException("customerDC");
                if (null == incidentNarrativeDC) throw new ArgumentOutOfRangeException("incidentNarrativeDC");
                if (null == incidentRepository) throw new ArgumentOutOfRangeException("incidentRepository");
                if (null == customerRepository) throw new ArgumentOutOfRangeException("customerRepository");
                if (null == narrativeRepository) throw new ArgumentOutOfRangeException("narrativeRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                // Map data contract to model
                Incident incidentItem = Mapper.Map<IncidentDC, Incident>(incidentDC);
                Customer customerItem = Mapper.Map<CustomerDC, Customer>(customerDC);
                Narrative incidentNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(incidentNarrativeDC);

                incidentItem.IncidentStatus = "Submitted";
                
                // Save Organisations and sites
                Guid siteOrganisationCode = incidentDC.OrganisationCode;
                Guid staffMemberBusinessCode = incidentDC.StaffMemberBusinessCode;
                Guid staffMemberBusinessAreaCode = incidentDC.StaffMemberBusinessAreaCode;
                Guid staffMemberHomeOfficeCode = incidentDC.StaffMemberHomeOfficeCode;

                if(!string.IsNullOrEmpty(lineManagerNarrativeDC.NarrativeDescription))
                {
                    lineManagerNarrativeDC.Code = Guid.NewGuid();
                    lineManagerNarrativeDC.NarrativeType = "LineManager";

                    incidentItem.LineManagerNarrativeCode = lineManagerNarrativeDC.Code;
                }

                Narrative lineManagerNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(lineManagerNarrativeDC);

                //IncidentUpdateEvent
                IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
                incidentUpdateEventItem.Code = Guid.NewGuid();
                incidentUpdateEventItem.DateTime = DateTime.Now;
                incidentUpdateEventItem.IncidentCode = incidentItem.Code;
                incidentUpdateEventItem.Type = "Update";
                incidentUpdateEventItem.UpdateBy = currentUserNameFromAd;

                using (uow)
                {
                    // Add the new item
                    incidentRepository.Update(incidentItem);
                    customerRepository.Update(customerItem);
                    narrativeRepository.Update(incidentNarrativeItem);
                    if (!string.IsNullOrEmpty(lineManagerNarrativeItem.NarrativeDescription))
                    {
                        narrativeRepository.Add(lineManagerNarrativeItem);
                    }
                    incidentUpdateEventRepository.Add(incidentUpdateEventItem);
                    // Commit unit of work
                    uow.Commit();
                }

                //Map updated models back to data contracts (for updated RowIdentifier)
                incidentDC = Mapper.Map<Incident, IncidentDC>(incidentItem);
                customerDC = Mapper.Map<Customer, CustomerDC>(customerItem);
                incidentNarrativeDC = Mapper.Map<Narrative, NarrativeDC>(incidentNarrativeItem);
                lineManagerNarrativeDC = Mapper.Map<Narrative,NarrativeDC>(lineManagerNarrativeItem);

                // Create aggregate data contract
                IncidentVMDC returnObject = new IncidentVMDC();

                // Add new item to aggregate
                returnObject.IncidentItem = incidentDC;
                returnObject.CustomerItem = customerDC;
                returnObject.IncidentNarrativeItem = incidentNarrativeDC;
                returnObject.LineManagerNarrativeItem = lineManagerNarrativeDC;

                // Retrieve organisations
                returnObject.IncidentItem.OrganisationCode = siteOrganisationCode;
                returnObject.IncidentItem.StaffMemberHomeOfficeCode = staffMemberHomeOfficeCode;
                returnObject.IncidentItem.StaffMemberBusinessCode = staffMemberBusinessCode;
                returnObject.IncidentItem.StaffMemberBusinessAreaCode = staffMemberBusinessAreaCode;

                return returnObject;

            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region NominatedManagerUpdate

        /// <summary>
        /// Update a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        public IncidentVMDC NominatedManagerUpdateIncident(string currentUser, string user, string appID, string overrideID, string incidentStatus, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC incidentNarrativeDC, NarrativeDC lineManagerNarrativeDC, NarrativeDC furtherInfoNarrativeDC, NarrativeDC deficienciesNarrativeDC, NarrativeDC reviewActionNarrativeDC, List<String> contingencyArrangementCodes, List<String> controlMeasureCodes, List<String> systemMarkedCodes, List<String> interestedPartyCodes)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            Repository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Narrative> narrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<CustomerContingencyArrangement> customerContingencyArrangementRepository = new Repository<CustomerContingencyArrangement>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<CustomerControlMeasure> customerControlMeasureRepository = new Repository<CustomerControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentSystemMarked> incidentSystemMarkedRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentInterestedParty> incidentInterestedPartyRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();
            
            // Call overload with injected objects
            return NominatedManagerUpdateIncident(currentUser, user, appID, overrideID, incidentStatus, incidentDC, customerDC, incidentNarrativeDC,
                lineManagerNarrativeDC, furtherInfoNarrativeDC, deficienciesNarrativeDC, reviewActionNarrativeDC, contingencyArrangementCodes, controlMeasureCodes, systemMarkedCodes, 
                interestedPartyCodes, incidentRepository, customerRepository, narrativeRepository, customerContingencyArrangementRepository, customerControlMeasureRepository,
                incidentSystemMarkedRepository, incidentInterestedPartyRepository, incidentUpdateEventRepository, staffRepository, uow, exceptionManager);
        }

        /// <summary>
        /// Update a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public IncidentVMDC NominatedManagerUpdateIncident(string currentUser, string user, string appID, string overrideID, 
            string incidentStatus, IncidentDC incidentDC, CustomerDC customerDC,
            NarrativeDC incidentNarrativeDC, NarrativeDC lineManagerNarrativeDC, NarrativeDC furtherInfoNarrativeDC, NarrativeDC deficienciesNarrativeDC, NarrativeDC reviewActionNarrativeDC,
            List<String> contingencyArrangementCodes, List<String> controlMeasureCodes,
            List<String> systemMarkedCodes, List<String> interestedPartyCodes,
            IRepository<Incident> incidentRepository, IRepository<Customer> customerRepository, IRepository<Narrative> narrativeRepository, 
            IRepository<CustomerContingencyArrangement> customerContingencyArrangementRepository, IRepository<CustomerControlMeasure> customerControlMeasureRepository,
            IRepository<IncidentSystemMarked> incidentSystemMarkedRepository, IRepository<IncidentInterestedParty> incidentInterestedPartyRepository,
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, IRepository<Staff> staffRepository, IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (string.IsNullOrEmpty(incidentStatus)) throw new ArgumentOutOfRangeException("incidentStatus");
                if (null == incidentDC) throw new ArgumentOutOfRangeException("incidentDC");
                if (null == customerDC) throw new ArgumentOutOfRangeException("customerDC");
                if (null == incidentNarrativeDC) throw new ArgumentOutOfRangeException("incidentNarrativeDC");
                if (null == lineManagerNarrativeDC) throw new ArgumentOutOfRangeException("lineManagerNarrativeDC");
                if (null == furtherInfoNarrativeDC) throw new ArgumentOutOfRangeException("furtherInfoNarrativeDC");
                if (null == deficienciesNarrativeDC) throw new ArgumentOutOfRangeException("deficienciesNarrativeDC");
                if (null == reviewActionNarrativeDC) throw new ArgumentOutOfRangeException("reviewActionNarrativeDC");
                if (null == incidentRepository) throw new ArgumentOutOfRangeException("incidentRepository");
                if (null == customerRepository) throw new ArgumentOutOfRangeException("customerRepository");
                if (null == narrativeRepository) throw new ArgumentOutOfRangeException("narrativeRepository");
                if (null == customerContingencyArrangementRepository) throw new ArgumentOutOfRangeException("customerContingencyArrangementRepository");
                if (null == customerControlMeasureRepository) throw new ArgumentOutOfRangeException("customerControlMeasureRepository");
                if (null == incidentSystemMarkedRepository) throw new ArgumentOutOfRangeException("incidentSystemMarkedRepository");
                if (null == incidentInterestedPartyRepository) throw new ArgumentOutOfRangeException("incidentInterestedPartyRepository");
                if (null == incidentUpdateEventRepository) throw new ArgumentOutOfRangeException("incidentUpdateEventRepository");
                if (null == staffRepository) throw new ArgumentOutOfRangeException("staffRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                // Map data contract to model
                Incident incidentItem = Mapper.Map<IncidentDC, Incident>(incidentDC);
                Customer customerItem = Mapper.Map<CustomerDC, Customer>(customerDC);
                Narrative incidentNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(incidentNarrativeDC);

                incidentItem.IncidentStatus = incidentStatus;

                Narrative lineManagerNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(lineManagerNarrativeDC);

                Narrative furtherInfoNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(furtherInfoNarrativeDC);

                Narrative deficienciesNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(deficienciesNarrativeDC);

                Narrative reviewActionNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(reviewActionNarrativeDC);

                Guid userWhoUpdatedIncident = Guid.Parse(currentUser);
                //IncidentUpdateEvent
                string staffName = staffRepository.Find(new Specification<Staff>(x => x.Code == userWhoUpdatedIncident)).Select(x=>x.FirstName + " " + x.LastName).First();

                IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
                incidentUpdateEventItem.Code = Guid.NewGuid();
                incidentUpdateEventItem.DateTime = DateTime.Now;
                incidentUpdateEventItem.IncidentCode = incidentItem.Code;
                incidentUpdateEventItem.Type = "Update";
                incidentUpdateEventItem.UpdateBy = staffName;

                // Re-calculate FiscalYear, FiscalQuarter and FiscalMonth based on the incident date.
                string fiscalMonthText = CalculateFiscalMonth(incidentItem.IncidentDate.Month);
                string fiscalMonthNumber = CalculateFiscalMonthNumber(incidentItem.IncidentDate.Month);

                PopulateIncidentFiscalDateFields(incidentItem, fiscalMonthText, fiscalMonthNumber);

                using (uow)
                {
                    // Add the new item

                    customerRepository.Update(customerItem);
                    narrativeRepository.Update(incidentNarrativeItem);

                    //Only add Line Manager Narrative narrative if it doesn't already exist and something has been entered. 
                    if (lineManagerNarrativeItem.Code == Guid.Empty)
                    {
                        if (!String.IsNullOrEmpty(lineManagerNarrativeItem.NarrativeDescription))
                        {
                            lineManagerNarrativeItem.Code = Guid.NewGuid();
                            incidentItem.LineManagerNarrativeCode = lineManagerNarrativeItem.Code;
                            lineManagerNarrativeItem.NarrativeType = "LineManager";

                            narrativeRepository.Add(lineManagerNarrativeItem);
                        }
                    }
                    else
                    {
                        narrativeRepository.Update(lineManagerNarrativeItem);
                    }

        
                    //Only add Further Info Narrative if it doesn't already exist and something has been entered. 
                    if (furtherInfoNarrativeItem.Code == Guid.Empty)
                    {
                        if (!String.IsNullOrEmpty(furtherInfoNarrativeItem.NarrativeDescription))
                        {
                            furtherInfoNarrativeItem.Code = Guid.NewGuid();
                            incidentItem.FurtherInfoNarrativeCode = furtherInfoNarrativeItem.Code;
                            furtherInfoNarrativeItem.NarrativeType = "FurtherInfo";

                            narrativeRepository.Add(furtherInfoNarrativeItem);
                        }
                    }
                    else
                    {
                        narrativeRepository.Update(furtherInfoNarrativeItem);
                    }

                    //Only add Deficiencies narrative if it doesn't already exist and something has been entered.
                    if (deficienciesNarrativeItem.Code == Guid.Empty)
                    {
                        if (!String.IsNullOrEmpty(deficienciesNarrativeItem.NarrativeDescription))
                        {
                            deficienciesNarrativeItem.Code = Guid.NewGuid();
                            incidentItem.DeficienciesNarrativeCode = deficienciesNarrativeItem.Code;
                            deficienciesNarrativeItem.NarrativeType = "Deficiencies";

                            narrativeRepository.Add(deficienciesNarrativeItem);
                        }
                    }
                    else
                    {
                        narrativeRepository.Update(deficienciesNarrativeItem);
                    }

                    //Only add Review Action narrative if it doesn't already exist and something has been entered.
                    if (reviewActionNarrativeItem.Code == Guid.Empty)
                    {
                        if (!String.IsNullOrEmpty(reviewActionNarrativeItem.NarrativeDescription))
                        {
                            reviewActionNarrativeItem.Code = Guid.NewGuid();
                            incidentItem.ReviewActionNarrativeCode = reviewActionNarrativeItem.Code;
                            reviewActionNarrativeItem.NarrativeType = "ReviewAction";

                            narrativeRepository.Add(reviewActionNarrativeItem);
                        }
                    }
                    else
                    {
                        narrativeRepository.Update(reviewActionNarrativeItem);
                    }

                    // customer contingency arrangements
                    // delete existing ones
                    IEnumerable<CustomerContingencyArrangement> existingContingencyArrangementList = customerContingencyArrangementRepository.Find(x => x.CustomerCode == incidentItem.CustomerCode);
                    foreach (CustomerContingencyArrangement ca in existingContingencyArrangementList)
                        customerContingencyArrangementRepository.Delete(ca);
                    foreach (string ca in contingencyArrangementCodes)
                        customerContingencyArrangementRepository.Add(new CustomerContingencyArrangement() { Code = Guid.NewGuid(), ContingencyArrangementCode = Guid.Parse(ca), CustomerCode = incidentItem.CustomerCode.Value });

                    // customer contingency arrangements
                    // delete existing ones
                    IEnumerable<CustomerControlMeasure> existingControlMeasureList = customerControlMeasureRepository.Find(x => x.CustomerCode == incidentItem.CustomerCode);
                    foreach (CustomerControlMeasure ca in existingControlMeasureList)
                        customerControlMeasureRepository.Delete(ca);
                    foreach (string ca in controlMeasureCodes)
                        customerControlMeasureRepository.Add(new CustomerControlMeasure() { Code = Guid.NewGuid(), ControlMeasureCode = Guid.Parse(ca), CustomerCode = incidentItem.CustomerCode.Value });

                    // Incident system marked
                    // delete existing ones
                    IEnumerable<IncidentSystemMarked> existingSystemMarkedList = incidentSystemMarkedRepository.Find(x => x.IncidentCode == incidentItem.Code);
                    foreach (IncidentSystemMarked sm in existingSystemMarkedList)
                        incidentSystemMarkedRepository.Delete(sm);
                    foreach (string sm in systemMarkedCodes)
                        incidentSystemMarkedRepository.Add(new IncidentSystemMarked() { Code = Guid.NewGuid(), SystemMarkedCode = Guid.Parse(sm), IncidentCode = incidentItem.Code });

                    // Incident system marked
                    // delete existing ones
                    IEnumerable<IncidentInterestedParty> existingInterestedPartyList = incidentInterestedPartyRepository.Find(x => x.IncidentCode == incidentItem.Code);
                    foreach (IncidentInterestedParty ip in existingInterestedPartyList)
                        incidentInterestedPartyRepository.Delete(ip);
                    foreach (string ip in interestedPartyCodes)
                        incidentInterestedPartyRepository.Add(new IncidentInterestedParty() { Code = Guid.NewGuid(), InterestedPartyCode = Guid.Parse(ip), IncidentCode = incidentItem.Code });

                    //IncidentUpdateEvent
                    List<IncidentUpdateEvent> incidentUpdateEventList = incidentUpdateEventRepository.Find(new Specification<IncidentUpdateEvent>(x => x.IncidentCode == incidentItem.Code && x.Type=="Update")).ToList();
                    incidentUpdateEventList = incidentUpdateEventList.OrderBy(x => x.DateTime).ToList();
                    //Only store last 5 update events (list is ordered by date time (asc) so the elementAt 0 will be the oldest
                    if (incidentUpdateEventList.Count == 5)
                        incidentUpdateEventRepository.Delete(incidentUpdateEventList.ElementAt(0));

                    incidentUpdateEventRepository.Add(incidentUpdateEventItem);

                    incidentRepository.Update(incidentItem);

                    


                    // Commit unit of work
                    uow.Commit();
                }

                // Create aggregate data contract
                IncidentVMDC returnObject = new IncidentVMDC();

                // Add new item to aggregate and Map updated models back to data contracts (for updated RowIdentifier)
                returnObject.IncidentItem = Mapper.Map<Incident, IncidentDC>(incidentItem);
                returnObject.CustomerItem = Mapper.Map<Customer, CustomerDC>(customerItem);
                returnObject.IncidentNarrativeItem = Mapper.Map<Narrative, NarrativeDC>(incidentNarrativeItem);
                returnObject.LineManagerNarrativeItem = Mapper.Map<Narrative, NarrativeDC>(lineManagerNarrativeItem);
                returnObject.FurtherInfoNarrativeItem = Mapper.Map<Narrative, NarrativeDC>(furtherInfoNarrativeItem);
                returnObject.DeficienciesNarrativeItem = Mapper.Map<Narrative, NarrativeDC>(deficienciesNarrativeItem);

                return returnObject;

            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="lockID"></param>
        public void DeleteIncident(string currentUser, string user, string appID, string overrideID, Guid code, byte[] lockID)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<AttachmentData> attachmentDataRepository = new Repository<AttachmentData>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Attachment> attachmentRepository = new Repository<Attachment>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Narrative> narrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentSystemMarked> incidentSystemMarkedRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentInterestedParty> incidentInterestedPartyRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentLink> incidentLinkRepository = new Repository<IncidentLink>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<CustomerControlMeasure> customerControlMeasureRepository = new Repository<CustomerControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<CustomerContingencyArrangement> customerContingencyArrangementRepository = new Repository<CustomerContingencyArrangement>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();    

            // Call overload with injected objects
            DeleteIncident(currentUser, user, appID, overrideID, code, lockID, 
                incidentRepository, 
                attachmentDataRepository,
                attachmentRepository,
                narrativeRepository,
                incidentUpdateEventRepository,
                incidentSystemMarkedRepository,
                incidentInterestedPartyRepository,
                incidentLinkRepository,
                customerControlMeasureRepository,
                customerContingencyArrangementRepository,
                customerRepository,
                uow, 
                exceptionManager);
        }

        /// <summary>
        /// Update a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="lockID"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public void DeleteIncident(string currentUser, string user, string appID, string overrideID, Guid incidentCode, byte[] lockID, 
            IRepository<Incident> incidentRepository, 
            IRepository<AttachmentData> attachmentDataRepository,
            IRepository<Attachment> attachmentRepository,
            IRepository<Narrative> narrativeRepository,
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository,
            IRepository<IncidentSystemMarked> incidentSystemMarkedRepository,
            IRepository<IncidentInterestedParty> incidentInterestedPartyRepository,
            IRepository<IncidentLink> incidentLinkRepository,
            IRepository<CustomerControlMeasure> customerControlMeasureRepository,
            IRepository<CustomerContingencyArrangement> customerContingencyArrangementRepository,
            IRepository<Customer> customerRepository,
            IUnitOfWork uow,
            IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (incidentCode==Guid.Empty) throw new ArgumentOutOfRangeException("code");
                if (lockID == null) throw new ArgumentOutOfRangeException("lockID");
                if (null == incidentRepository) throw new ArgumentOutOfRangeException("incidentRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {
                    #region delete incident's related items
                    //Don't want to load the blobs so need to trick the ef to get the guid and rowid 
                    //BUT will have to but only for the incident (not them all)
                    var AttachmentDetailList = incidentRepository.GetQuery().SelectMany(x => x.Attachment.Where(y => y.IncidentCode == incidentCode).SelectMany(y => y.AttachmentData.Select(z => new { z.Code, z.RowIdentifier }))).ToList();
                    foreach (var AttachmentDetail in AttachmentDetailList)
                    {
                        // Get attachment data (image/blob)
                        var data = attachmentDataRepository.Find(x => x.Code == AttachmentDetail.Code);
                        foreach (var attachmentDetail in data)
                        {
                            attachmentDataRepository.Delete(attachmentDetail);
                        }
                    }

                    //Delete attachments
                    var attachments = attachmentRepository.Find(x => x.IncidentCode == incidentCode);
                    foreach (var attchmentItem in attachments)
                    {
                        attachmentRepository.Delete(attchmentItem);
                    }

                    //Delete IncidentUpdateEvent
                    var incidentUpdateEvents = incidentUpdateEventRepository.Find(x => x.IncidentCode == incidentCode);
                    foreach (var incidentUpdateEvent in incidentUpdateEvents)
                    {
                        incidentUpdateEventRepository.Delete(incidentUpdateEvent);
                    }

                    //Delete IncidentSystemMarked
                    var incidentSystemMarkeds = incidentSystemMarkedRepository.Find(x => x.IncidentCode == incidentCode);
                    foreach (var incidentSystemMarked in incidentSystemMarkeds)
                    {
                        incidentSystemMarkedRepository.Delete(incidentSystemMarked);
                    }

                    //Delete IncidentInterestedParty
                    var incidentInterestedPartys = incidentInterestedPartyRepository.Find(x => x.IncidentCode == incidentCode);
                    foreach (var incidentInterestedParty in incidentInterestedPartys)
                    {
                        incidentInterestedPartyRepository.Delete(incidentInterestedParty);
                    }
                    //Delete IncidentLink
                    var incidentLinks = incidentLinkRepository.Find(x => x.IncidentCode == incidentCode);
                    foreach (var incidentLink in incidentLinks)
                    {
                        incidentLinkRepository.Delete(incidentLink);
                    }
                    //And the other way around
                    var incidentsLinked = incidentLinkRepository.Find(x => x.LinkedIncidentCode == incidentCode);
                    foreach (var incidentLinked in incidentsLinked)
                    {
                        incidentLinkRepository.Delete(incidentLinked);
                    }
                    #endregion

                    // Find item based on ID
                    Incident dataEntity = incidentRepository.Single(x => x.Code == incidentCode,"Customer","Narrative","Narrative1","Narrative2","Narrative3");

                    // Delete the item
                    dataEntity.RowIdentifier = lockID;
                    incidentRepository.Delete(dataEntity);
                    
                    

                    #region Delete orphaned records
                    //Have to delete narrative last as reference is on incident. Delete Narrative
                    if (dataEntity.Narrative != null)
                    {
                        narrativeRepository.Delete(dataEntity.Narrative);
                    }
                    if (dataEntity.Narrative1 != null)
                    {
                        narrativeRepository.Delete(dataEntity.Narrative1);
                    }
                    if (dataEntity.Narrative2 != null)
                    {
                        narrativeRepository.Delete(dataEntity.Narrative2);
                    }
                    if (dataEntity.Narrative3 != null)
                    {
                        narrativeRepository.Delete(dataEntity.Narrative3);
                    }

                    //Delete Customer

                    //Delete CustomerControlMeasure
                    var customerControlMeasures = customerControlMeasureRepository.Find(x => x.CustomerCode == dataEntity.CustomerCode);
                    foreach (var customerControlMeasure in customerControlMeasures)
                    {
                        customerControlMeasureRepository.Delete(customerControlMeasure);
                    }
                    //Delete CustomerContingencyArrangement
                    var customerContingencyArrangements = customerContingencyArrangementRepository.Find(x => x.CustomerCode == dataEntity.CustomerCode);
                    foreach (var customerContingencyArrangement in customerContingencyArrangements)
                    {
                        customerContingencyArrangementRepository.Delete(customerContingencyArrangement);
                    }
                    if (dataEntity.Customer != null)
                    {
                        customerRepository.Delete(dataEntity.Customer);
                    }
                    

                    #endregion

                    // Commit unit of work
                    uow.Commit();
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);
            }
        }

        #endregion

        #region SearchIncident

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IncidentSearchVMDC SearchIncident(string currentUser, string user, string appID, string overrideID, IncidentSearchCriteriaDC searchCriteria, int page, int pageSize)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Incident> dataRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Create specification for filtering
            ISpecification<Incident> specification = new Specification<Incident>();

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return SearchIncident(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, specification, dataRepository, uow, exceptionManager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="specification"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public IncidentSearchVMDC SearchIncident(string currentUser, string user, string appID, string overrideID, IncidentSearchCriteriaDC searchCriteria, int page, int pageSize,
        ISpecification<Incident> specification, IRepository<Incident> dataRepository, IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == specification) throw new ArgumentOutOfRangeException("specification");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {
                    // Evaluate search criteria if supplied
                    if (null != searchCriteria)
                    {
                        EvaluateSearchCriteria(searchCriteria, ref specification);
                    }

                    // Set default sort expression
                    System.Linq.Expressions.Expression<Func<Incident, Object>> sortExpression = x => x.RowIdentifier;

                    // Find all items that satisfy the specification created above.
                    IEnumerable<Incident> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);

                    // Get total count of items for search critera
                    int itemCount = dataRepository.Count(specification);

                    IncidentSearchVMDC results = new IncidentSearchVMDC();

                    // Convert to data contracts
                    List<IncidentSearchMatchDC> destinations = Mapper.Map<IEnumerable<Incident>, List<IncidentSearchMatchDC>>(dataEntities);

                    results.MatchList = destinations;
                    results.SearchCriteria = searchCriteria;
                    results.RecordCount = itemCount;

                    return results;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        // Partial method for evaluation of search criteria
        partial void EvaluateSearchCriteria(IncidentSearchCriteriaDC searchCriteria, ref ISpecification<Incident> specification);

        #endregion

        #region GetAllIncident

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <returns></returns>
        public List<IncidentDC> GetAllIncident(string currentUser, string user, string appID, string overrideID)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Incident> dataRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Create specification for filtering
            ISpecification<Incident> specification = new Specification<Incident>();

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            return GetAllIncident(currentUser, user, appID, overrideID, specification, dataRepository, uow, exceptionManager);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="specification"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public List<IncidentDC> GetAllIncident(string currentUser, string user, string appID, string overrideID, ISpecification<Incident> specification, IRepository<Incident> dataRepository,
            IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == specification) throw new ArgumentOutOfRangeException("specification");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {
                    // Set default sort expression
                    System.Linq.Expressions.Expression<Func<Incident, Object>> sortExpression = x => x.IncidentID;

                    // Find all items that satisfy the specification created above.
                    IEnumerable<Incident> dataEntities = dataRepository.Find(specification, sortExpression);

                    // Convert to data contracts
                    List<IncidentDC> destinations = Mapper.Map<IEnumerable<Incident>, List<IncidentDC>>(dataEntities);

                    return destinations;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region GetIncident

        /// <summary>
        /// Retrieve a Incident with associated lookups
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="locale"></param>
        /// <returns></returns>
        public IncidentVMDC GetIncident(string currentUser, string user, string appID, string overrideID, string code, string locale)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Incident> dataRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Create repositories for lookup data
            IRepository<JobRole> jobRoleRepository = new Repository<JobRole>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Organisation> staffMemberBusinessRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Organisation> staffMemberBusinessAreaRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Organisation> staffMemberHomeOfficeRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<EventLeadingToIncident> eventLeadingToIncidentRepository = new Repository<EventLeadingToIncident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentLocation> incidentLocationRepository = new Repository<IncidentLocation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentCategory> incidentCategoryRepository = new Repository<IncidentCategory>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentType> incidentTypeRepository = new Repository<IncidentType>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentDetail> incidentDetailsRepository = new Repository<IncidentDetail>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<AbuseType> abuseTypeRepository = new Repository<AbuseType>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Narrative> incidentNarrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Narrative> lineManagerNarrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Attachment> fastTrackAttachmentRepository = new Repository<Attachment>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Attachment> rIDDORAttachmentRepository = new Repository<Attachment>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Narrative> furtherInfoNarrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Narrative> deficienciesNarrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Narrative> reviewActionNarrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Staff> nominatedManagerRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Site> siteRepository = new Repository<Site>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<OrganisationHierarchy> organisationHierarchyRepository = new Repository<OrganisationHierarchy>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<SystemParameter> systemParameterRepository = new Repository<SystemParameter>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<RelationshipToCustomer> relationshipToCustomerRepository = new Repository<RelationshipToCustomer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentLink> incidentLinkRepository = new Repository<IncidentLink>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<ContingencyArrangement> contingencyArrangementRepository = new Repository<ContingencyArrangement>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<ControlMeasure> controlMeasureRepository = new Repository<ControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<CustomerContingencyArrangement> customerContingencyArrangementRepository = new Repository<CustomerContingencyArrangement>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<CustomerControlMeasure> customerControlMeasureRepository = new Repository<CustomerControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
           
            IRepository<SystemMarked> systemMarkedRepository = new Repository<SystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentSystemMarked> incidentSystemMarkedRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<InterestedParty> interestedPartyRepository = new Repository<InterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentInterestedParty> incidentInterestedPartyRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<StaffAttributes> staffAttributesRepository = new Repository<StaffAttributes>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Referrer> referrerRepository = new Repository<Referrer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Content> contentRepository = new Repository<Content>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // gary            
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return GetIncident(currentUser, user, appID, overrideID, code, locale, uow, dataRepository
            , jobRoleRepository
            , staffMemberBusinessRepository
            , staffMemberBusinessAreaRepository
            , staffMemberHomeOfficeRepository
            , customerRepository
            , eventLeadingToIncidentRepository
            , incidentLocationRepository
            , incidentCategoryRepository
            , incidentTypeRepository
            , incidentDetailsRepository
            , abuseTypeRepository
            , incidentNarrativeRepository
            , lineManagerNarrativeRepository
            , fastTrackAttachmentRepository
            , rIDDORAttachmentRepository
            , furtherInfoNarrativeRepository
            , deficienciesNarrativeRepository
            , reviewActionNarrativeRepository
            , nominatedManagerRepository
            , siteRepository
            , organisationHierarchyRepository
            , systemParameterRepository
            , relationshipToCustomerRepository
            , incidentLinkRepository
            , contingencyArrangementRepository
            , controlMeasureRepository
            , customerContingencyArrangementRepository
            , customerControlMeasureRepository
            , systemMarkedRepository
            , incidentSystemMarkedRepository
            , interestedPartyRepository
            , incidentInterestedPartyRepository
            , incidentUpdateEventRepository
            , staffAttributesRepository
            , referrerRepository
            , contentRepository
            , staffRepository
            , exceptionManager
            );
        }

        /// <summary>
        /// Retrieve a Incident with associated lookups
        /// </summary>
        /// <param name="currentUser">Current user is the users staff guid or a windows ID</param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="locale"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public IncidentVMDC GetIncident(string currentUser, string user, string appID, string overrideID, string code, string locale, IUnitOfWork uow, IRepository<Incident> dataRepository
            , IRepository<JobRole> jobRoleRepository
            , IRepository<Organisation> staffMemberBusinessRepository
            , IRepository<Organisation> staffMemberBusinessAreaRepository
            , IRepository<Organisation> staffMemberHomeOfficeRepository
            , IRepository<Customer> customerRepository
            , IRepository<EventLeadingToIncident> eventLeadingToIncidentRepository
            , IRepository<IncidentLocation> incidentLocationRepository
            , IRepository<IncidentCategory> incidentCategoryRepository
            , IRepository<IncidentType> incidentTypeRepository
            , IRepository<IncidentDetail> incidentDetailsRepository
            , IRepository<AbuseType> abuseTypeRepository
            , IRepository<Narrative> incidentNarrativeRepository
            , IRepository<Narrative> lineManagerNarrativeRepository
            , IRepository<Attachment> fastTrackAttachmentRepository
            , IRepository<Attachment> rIDDORAttachmentRepository
            , IRepository<Narrative> furtherInfoNarrativeRepository
            , IRepository<Narrative> deficienciesNarrativeRepository
            , IRepository<Narrative> reviewActionNarrativeRepository
            , IRepository<Staff> nominatedManagerRepository
            , IRepository<Site> siteRepository
            , IRepository<OrganisationHierarchy> organisationHierarchyRepository
            , IRepository<SystemParameter> systemParameterRepository
            , IRepository<RelationshipToCustomer> relationshipToCustomerRepository
            , IRepository<IncidentLink> incidentLinkRepository
            , IRepository<ContingencyArrangement> contingencyArrangementRepository
            , IRepository<ControlMeasure> controlMeasureRepository
            , IRepository<CustomerContingencyArrangement> customerContingencyArrangementRepository
            , IRepository<CustomerControlMeasure> customerControlMeasureRepository
            , IRepository<SystemMarked> systemMarkedRepository
            , IRepository<IncidentSystemMarked> incidentSystemMarkedRepository
            , IRepository<InterestedParty> interestedPartyRepository
            , IRepository<IncidentInterestedParty> incidentInterestedPartyRepository
            , IRepository<IncidentUpdateEvent> incidentUpdateEventRepository
            , IRepository<StaffAttributes> staffAttributesRepository
            , IRepository<Referrer> referrerRepository
            , IRepository<Content> contentRepository
            , IRepository<Staff> currentOwnerRepository
            , IExceptionManager exceptionManager
            )
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("locale");
                if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {

                    IncidentDC destination = null;

                    // If code is null then just return supporting lists
                    if (!string.IsNullOrEmpty(code))
                    {
                        // Convert code to Guid
                        Guid codeGuid = Guid.Parse(code);

                        // Retrieve specific Incident
                        Incident dataEntity = dataRepository.Single(x => x.Code == codeGuid);

                        // Convert to data contract for passing through service interface
                        destination = Mapper.Map<Incident, IncidentDC>(dataEntity);
                    }

                    IEnumerable<Referrer> referrerList = referrerRepository.GetAll(x => new { x.Description });
                    IEnumerable<JobRole> jobRoleList = jobRoleRepository.GetAll(x => new { x.Description });
                    IEnumerable<Organisation> staffMemberBusinessList = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.OrganisationType.LevelNumber == 2 && x.OrganisationType.OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Any(v => v.Application.ApplicationName == "UCB")));
                    IEnumerable<Organisation> staffMemberBusinessAreaList = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.OrganisationType.LevelNumber == 3 && x.OrganisationType.OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Any(v => v.Application.ApplicationName == "UCB")));

                    //IEnumerable<Organisation> staffMemberHomeOfficeList = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.OrganisationType.LevelNumber == 6 && x.OrganisationType.OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Any(v => v.Application.ApplicationName == "UCB")));
                    IEnumerable<Site> staffMemberHomeOfficeList = siteRepository.GetAll();

                    Customer customer = new Customer();
                    Staff currentOwner = new Staff();
                    if (destination != null)
                    {
                        customer = customerRepository.Find(x => x.Code == destination.CustomerCode).SingleOrDefault();
                        
                        // gary
                        currentOwner = currentOwnerRepository.Find(x => x.Code == destination.CurrentOwnerStaffCode).SingleOrDefault();
                    }

  
                    IEnumerable<EventLeadingToIncident> eventLeadingToIncidentList = eventLeadingToIncidentRepository.GetAll(x => new { x.Description });
                    IEnumerable<IncidentLocation> incidentLocationList = incidentLocationRepository.GetAll(x => new { x.Description });
                    IEnumerable<IncidentCategory> incidentCategoryList = incidentCategoryRepository.GetAll(x => new { x.Description });
                    IEnumerable<IncidentType> incidentTypeList = incidentTypeRepository.GetAll(x => new { x.Description });
                    IEnumerable<IncidentDetail> incidentDetailsList = incidentDetailsRepository.GetAll(x => new { x.Description });
                    IEnumerable<AbuseType> abuseTypeList = abuseTypeRepository.GetAll(x => new { x.Description });
                    IEnumerable<ContingencyArrangement> contingencyArrangementList = contingencyArrangementRepository.GetAll(x => new { x.Description });
                    IEnumerable<ControlMeasure> controlMeasureList = controlMeasureRepository.GetAll(x => new { x.ControlMeasureDescription });
                    IEnumerable<SystemMarked> systemMarkedList = systemMarkedRepository.GetAll(x => new { x.Description });
                    IEnumerable<InterestedParty> interestedPartyList = interestedPartyRepository.GetAll(x => new { x.Description });

                    IEnumerable<IncidentLink> incidentLinkList = null;
                    List<IncidentLinkDC> incidentLinkExtensionLists = null;
                    if (destination != null)
                    {
                        incidentLinkList = incidentLinkRepository.Find(x => x.IncidentCode == destination.Code);
                        List<IncidentLink> incidentLinkLists = new List<IncidentLink>();

                        incidentLinkExtensionLists = new List<IncidentLinkDC>();

                        List<Incident> incidentSortedList = new List<Incident>();
                        foreach (IncidentLink iL in incidentLinkList)
                        {
                            incidentSortedList.Add(dataRepository.Single(x => x.Code == iL.LinkedIncidentCode));
                        }
                        incidentSortedList = incidentSortedList.OrderByDescending(x => x.IncidentDate).AsEnumerable().ToList(); ;

                        //incidentLinkList
                        foreach (Incident iL in incidentSortedList)
                        {
                            IncidentLink linkToIncident = incidentLinkRepository.Single(x => x.IncidentId == iL.IncidentID && x.IncidentCode == incidentLinkList.FirstOrDefault().IncidentCode);
                            incidentLinkLists.Add(linkToIncident);

                            Customer mainCustomer = customerRepository.Find(x => x.Code == iL.CustomerCode).FirstOrDefault();

                            IncidentLinkDC incidentLinkExtension = Mapper.Map<IncidentLinkDC>(linkToIncident);
                            incidentLinkExtension.Name = mainCustomer.FirstName + " " + mainCustomer.OtherNames + " " + mainCustomer.LastName;
                            incidentLinkExtension.NINO = mainCustomer.NINO;
                            incidentLinkExtension.OtherPersonNINO = mainCustomer.OtherPersonNINO;
                            incidentLinkExtension.IncidentDate = iL.IncidentDate;
                            incidentLinkExtension.IncidentStatus = iL.IncidentStatus;
                            incidentLinkExtension.IsImplementControlMeasures = iL.IsImplementControlMeasures;

                            incidentLinkExtensionLists.Add(incidentLinkExtension);
                        }
                        incidentLinkList = incidentLinkLists.AsEnumerable();
                    }

                    Narrative incidentNarrative = new Narrative();
                    if (destination != null)
                    {
                        incidentNarrative = incidentNarrativeRepository.Find(x => x.Code == destination.IncidentNarrativeCode).SingleOrDefault();
                    }

                    Narrative lineManagerNarrative = new Narrative();
                    if (destination != null)
                    {
                        lineManagerNarrative = lineManagerNarrativeRepository.Find(x => x.Code == destination.LineManagerNarrativeCode).SingleOrDefault();
                    }

                    Narrative furtherInfoNarrative = new Narrative();
                    if (destination != null)
                    {
                        furtherInfoNarrative = furtherInfoNarrativeRepository.Find(x => x.Code == destination.FurtherInfoNarrativeCode).SingleOrDefault();
                    }

                    Narrative deficienciesNarrative = new Narrative();
                    if (destination != null)
                    {
                        deficienciesNarrative = deficienciesNarrativeRepository.Find(x => x.Code == destination.DeficienciesNarrativeCode).SingleOrDefault();
                    }

                    Narrative reviewActionNarrative = new Narrative();
                    if (destination != null)
                    {
                        reviewActionNarrative = reviewActionNarrativeRepository.Find(x => x.Code == destination.ReviewActionNarrativeCode).SingleOrDefault();
                    }

                    List<String> customerContingencyArrangementList = new List<String>();
                    if (destination != null)
                        customerContingencyArrangementList = customerContingencyArrangementRepository.Find(x => x.CustomerCode == destination.CustomerCode).Select(x => x.ContingencyArrangementCode.ToString()).ToList<String>();

                    List<String> customerControlMeasureList = new List<String>();
                    if (destination != null)
                        customerControlMeasureList = customerControlMeasureRepository.Find(x => x.CustomerCode == destination.CustomerCode).Select(x => x.ControlMeasureCode.ToString()).ToList<String>();

                    List<String> incidentSystemMarkedList = new List<string>();
                    if (destination != null)
                        incidentSystemMarkedList = incidentSystemMarkedRepository.Find(x => x.IncidentCode == destination.Code).Select(x => x.SystemMarkedCode.ToString()).ToList<String>();

                    List<String> incidentInterestedPartList = new List<string>();
                    if (destination != null)
                        incidentInterestedPartList = incidentInterestedPartyRepository.Find(x => x.IncidentCode == destination.Code).Select(x => x.InterestedPartyCode.ToString()).ToList<String>();

                    List<Attachment> fastTrackAttachmentList = new List<Attachment>();
                    if (destination != null)
                        fastTrackAttachmentList = fastTrackAttachmentRepository.Find(new Specification<Attachment>(x => x.AttachmentType == "FastTrack" && x.IncidentCode == destination.Code), x => new { x.Name }).ToList();

                    List<Attachment> rIDDORAttachmentList = new List<Attachment>();
                    if (destination != null)
                        rIDDORAttachmentList = rIDDORAttachmentRepository.Find(new Specification<Attachment>(x => x.AttachmentType == "RIDDOR" && x.IncidentCode == destination.Code), x => new { x.Name }).ToList();

                    List<Attachment> repeatBehaviourAttachmentList = new List<Attachment>();
                    if (destination != null)
                        repeatBehaviourAttachmentList = fastTrackAttachmentRepository.Find(new Specification<Attachment>(x => x.AttachmentType == "RepeatBehaviour" && x.IncidentCode == destination.Code), x => new { x.Name }).ToList();

                    List<Attachment> generalEvidenceAttachmentList = new List<Attachment>();
                    if (destination != null)
                        generalEvidenceAttachmentList = fastTrackAttachmentRepository.Find(new Specification<Attachment>(x => x.AttachmentType == "GeneralEvidence" && x.IncidentCode == destination.Code), x => new { x.Name }).ToList();

                    List<Attachment> furtherInfoAttachmentList = new List<Attachment>();
                    if (destination != null)
                        furtherInfoAttachmentList = fastTrackAttachmentRepository.Find(new Specification<Attachment>(x => x.AttachmentType == "FurtherInfo" && x.IncidentCode == destination.Code), x => new { x.Name }).ToList();

                    List<Attachment> referralEvidenceAttachmentList = new List<Attachment>();
                    if (destination != null)
                        referralEvidenceAttachmentList = fastTrackAttachmentRepository.Find(new Specification<Attachment>(x => x.AttachmentType == "ReferralEvidence" && x.IncidentCode == destination.Code), x => new { x.Name }).ToList();

                    List<IncidentUpdateEvent> incidentUpdateEventList = new List<IncidentUpdateEvent>();
                    if (destination != null)
                    {
                        incidentUpdateEventList = incidentUpdateEventRepository.Find(new Specification<IncidentUpdateEvent>(x => x.IncidentCode == destination.Code && x.Type == "Update")).ToList<IncidentUpdateEvent>();
                        incidentUpdateEventList = incidentUpdateEventList.OrderByDescending(x => x.DateTime).ToList();

                        IncidentUpdateEvent incidentCreateEvent = incidentUpdateEventRepository.Find(new Specification<IncidentUpdateEvent>(x => x.IncidentCode == destination.Code && x.Type == "Create")).SingleOrDefault();
                        if (incidentCreateEvent != null)
                        {
                            incidentUpdateEventList.Add(incidentCreateEvent);
                        }

                    }

                    // Get AdminEmail address system parameter
                    string adminEmailAddress = systemParameterRepository.Find(new Specification<SystemParameter>(x => x.Name == "AdminEmail")).Single().ParameterValue;

                    // Get Deputy Admin email address system parameter
                    string deputyAdminEmailAddress = systemParameterRepository.Find(new Specification<SystemParameter>(x => x.Name == "DeputyAdminEmail")).Single().ParameterValue;

                    // Get Incident Advisory Note text
                    Specification<Content> localeSpecification = new Specification<Content>(x => x.Locale == locale);
                    Specification<Content> contentItemSpecification = new Specification<Content>(x => x.Name == "IncidentAdvisoryNote");
                    string incidentAdvisoryNote = contentRepository.Find(localeSpecification.And(contentItemSpecification)).Single().Value;

                    IEnumerable<RelationshipToCustomer> relationshipToCustomerList = relationshipToCustomerRepository.GetAll(x => new { x.Description });

                    List<JobRoleDC> jobRoleDestinationList = Mapper.Map<List<JobRoleDC>>(jobRoleList);
                    List<ReferrerDC> referrerDestinationList = Mapper.Map<List<ReferrerDC>>(referrerList);

                    List<OrganisationDC> staffMemberBusinessDestinationList = Mapper.Map<List<OrganisationDC>>(staffMemberBusinessList);
                    List<OrganisationDC> staffMemberBusinessAreaDestinationList = Mapper.Map<List<OrganisationDC>>(staffMemberBusinessAreaList);

                    foreach (OrganisationDC organisation in staffMemberBusinessAreaDestinationList)
                    {
                        var parent = staffMemberBusinessAreaRepository.Find(x => x.OrganisationHierarchy1.Any(y => y.OrganisationCode == organisation.Code && y.ImmediateParent == true)).First();
                        organisation.ImmediateParent = parent.Code;
                    }
                    List<SiteDC> staffMemberHomeOfficeDestinationList = Mapper.Map<List<SiteDC>>(staffMemberHomeOfficeList);

                    CustomerDC customerDestination = Mapper.Map<CustomerDC>(customer);
                    
                    //gary
                    StaffDC currentOwnerDestination = Mapper.Map<StaffDC>(currentOwner);

                    List<EventLeadingToIncidentDC> eventLeadingToIncidentDestinationList = Mapper.Map<List<EventLeadingToIncidentDC>>(eventLeadingToIncidentList);
                    List<IncidentLocationDC> incidentLocationDestinationList = Mapper.Map<List<IncidentLocationDC>>(incidentLocationList);
                    List<IncidentCategoryDC> incidentCategoryDestinationList = Mapper.Map<List<IncidentCategoryDC>>(incidentCategoryList);
                    List<IncidentTypeDC> incidentTypeDestinationList = Mapper.Map<List<IncidentTypeDC>>(incidentTypeList);
                    List<IncidentDetailDC> incidentDetailsDestinationList = Mapper.Map<List<IncidentDetailDC>>(incidentDetailsList);
                    List<AbuseTypeDC> abuseTypeDestinationList = Mapper.Map<List<AbuseTypeDC>>(abuseTypeList);

                    List<IncidentLinkDC> incidentLinkDestinationList = incidentLinkExtensionLists; //Mapper.Map<List<IncidentLinkDC>>(incidentLinkList);

                    List<ContingencyArrangementDC> contingencyArrangementDestinationList = Mapper.Map<List<ContingencyArrangementDC>>(contingencyArrangementList);
                    List<ControlMeasureDC> controlMeasureDestinationList = Mapper.Map<List<ControlMeasureDC>>(controlMeasureList);
                    List<SystemMarkedDC> systemMarkedDestinationList = Mapper.Map<List<SystemMarkedDC>>(systemMarkedList);
                    List<InterestedPartyDC> interestedPartyDestinationList = Mapper.Map<List<InterestedPartyDC>>(interestedPartyList);


                    NarrativeDC incidentNarrativeDestination = Mapper.Map<NarrativeDC>(incidentNarrative);
                    NarrativeDC lineManagerNarrativeDestination = Mapper.Map<NarrativeDC>(lineManagerNarrative);
                    NarrativeDC furtherInfoNarrativeDestination = Mapper.Map<NarrativeDC>(furtherInfoNarrative);
                    NarrativeDC deficienciesNarrativeDestination = Mapper.Map<NarrativeDC>(deficienciesNarrative);
                    NarrativeDC reviewActionNarrativeDestination = Mapper.Map<NarrativeDC>(reviewActionNarrative);

                    List<AttachmentDC> fastTrackAttachmentDestinationList = Mapper.Map<List<AttachmentDC>>(fastTrackAttachmentList);
                    List<AttachmentDC> rIDDORAttachementDestinationList = Mapper.Map<List<AttachmentDC>>(rIDDORAttachmentList);
                    List<AttachmentDC> repeatBehaviourAttachmentDestinationList = Mapper.Map<List<AttachmentDC>>(repeatBehaviourAttachmentList);
                    List<AttachmentDC> generalEvidenceAttachmentDestinationList = Mapper.Map<List<AttachmentDC>>(generalEvidenceAttachmentList);
                    List<AttachmentDC> furtherInfoAttachmentDestinationList = Mapper.Map<List<AttachmentDC>>(furtherInfoAttachmentList);
                    List<AttachmentDC> referralEvidenceAttachmentDestinationList = Mapper.Map<List<AttachmentDC>>(referralEvidenceAttachmentList);

                    List<RelationshipToCustomerDC> relationshipToCustomerDestinationList = Mapper.Map<List<RelationshipToCustomerDC>>(relationshipToCustomerList);

                    List<IncidentUpdateEventDC> incidentUpdateEventDestinationList = Mapper.Map<List<IncidentUpdateEventDC>>(incidentUpdateEventList);

                    //Create aggregate contract
                    IncidentVMDC returnObject = new IncidentVMDC();

                    returnObject.IncidentItem = destination;
                    returnObject.ReferrerList = referrerDestinationList;
                    returnObject.JobRoleList = jobRoleDestinationList;
                    returnObject.StaffMemberBusinessList = staffMemberBusinessDestinationList;
                    returnObject.StaffMemberBusinessAreaList = staffMemberBusinessAreaDestinationList;
                    returnObject.StaffMemberHomeOfficeList = staffMemberHomeOfficeDestinationList;
                    returnObject.CustomerItem = customerDestination;
                    // gary                    
                    returnObject.CurrentOwnerItem = currentOwnerDestination;
                    returnObject.EventLeadingToIncidentList = eventLeadingToIncidentDestinationList;
                    returnObject.IncidentLocationList = incidentLocationDestinationList;
                    returnObject.IncidentCategoryList = incidentCategoryDestinationList;
                    returnObject.IncidentTypeList = incidentTypeDestinationList;
                    returnObject.IncidentDetailsList = incidentDetailsDestinationList;
                    returnObject.AbuseTypeList = abuseTypeDestinationList;
                    returnObject.IncidentLinkList = incidentLinkDestinationList;
                    returnObject.IncidentNarrativeItem = incidentNarrativeDestination;
                    returnObject.LineManagerNarrativeItem = lineManagerNarrativeDestination;
                    returnObject.FastTrackAttachmentList = fastTrackAttachmentDestinationList;
                    returnObject.RIDDORAttachmentList = rIDDORAttachementDestinationList;
                    returnObject.RepeatBehaviourAttachmentList = repeatBehaviourAttachmentDestinationList;
                    returnObject.GeneralEvidenceAttachmentList = generalEvidenceAttachmentDestinationList;
                    returnObject.FurtherInfoAttachmentList = furtherInfoAttachmentDestinationList;
                    returnObject.ReferralEvidenceAttachmentList = referralEvidenceAttachmentDestinationList;
                    returnObject.FurtherInfoNarrativeItem = furtherInfoNarrativeDestination;
                    returnObject.DeficienciesNarrativeItem = deficienciesNarrativeDestination;
                    returnObject.ReviewActionNarrativeItem = reviewActionNarrativeDestination;
                    returnObject.ContingencyArrangementList = contingencyArrangementDestinationList;
                    returnObject.ControlMeasureList = controlMeasureDestinationList;
                    returnObject.ContingencyArrangementCodes = customerContingencyArrangementList;
                    returnObject.ControlMeasureCodes = customerControlMeasureList;
                    returnObject.SystemMarkedList = systemMarkedDestinationList;
                    returnObject.SystemMarkedCodes = incidentSystemMarkedList;
                    returnObject.InterestedPartyList = interestedPartyDestinationList;
                    returnObject.InterestedPartyCodes = incidentInterestedPartList;

                    returnObject.AdminEmail = adminEmailAddress;
                    returnObject.DeputyAdminEmail = deputyAdminEmailAddress;
                    returnObject.RelationshipToCustomerList = relationshipToCustomerDestinationList;
                    returnObject.IncidentUpdateEvents = incidentUpdateEventDestinationList;
                    returnObject.IncidentAdvisoryNote = incidentAdvisoryNote;

                    // Assume incident is read only to begin with
                    returnObject.IsReadOnly = true;

                    if (destination != null)
                    {
                        //*************************************************************************************************************************************************
                        //Populate the StaffMemberBusiness,StaffMemberBusinessArea, StaffMemberHomeOfficeCode on the incident, using the StaffMemberHomeOfficeSiteCode
                        //*************************************************************************************************************************************************
                        //Get the level6 organisation for the incident
                        Organisation originalLevel6OrganisationForIncident = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.Site.Any(y => y.Code == destination.StaffMemberHomeOfficeSiteCode))).Single();
                        destination.StaffMemberHomeOfficeCode = originalLevel6OrganisationForIncident.Code;

                        //Get the level3 organisation for the incident
                        Organisation originalLevel3OrganisationForIncident = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.OrganisationHierarchy1.Any(y => y.OrganisationCode == originalLevel6OrganisationForIncident.Code && y.HopsBetweenOrgAndAncestor == 3))).Single();
                        destination.StaffMemberBusinessAreaCode = originalLevel3OrganisationForIncident.Code;

                        //Get the level2 organisation for the incident
                        Organisation originalLevel2OrganisationForIncident = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.OrganisationHierarchy1.Any(y => y.OrganisationCode == originalLevel6OrganisationForIncident.Code && y.HopsBetweenOrgAndAncestor == 4))).Single();
                        destination.StaffMemberBusinessCode = originalLevel2OrganisationForIncident.Code;

                        //*************************************************************************************************************************************************
                        //Populate the Organisation code on the incident, using the SiteCode
                        //*************************************************************************************************************************************************
                        //Get the level6 organisation for the incident
                        Organisation currentLevel6organisationForIncident = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.Site.Any(y => y.Code == destination.SiteCode))).Single();
                        destination.OrganisationCode = currentLevel6organisationForIncident.Code;


                        //get the current selected incident type to find if HasAbuseType or HasDetails needs to be displayed on the screen
                        IncidentType currentIncidentType = incidentTypeRepository.Find(new Specification<IncidentType>(x => x.Code == destination.IncidentTypeCode)).SingleOrDefault();

                        if (currentIncidentType != null)
                        {
                            returnObject.ShowAbuseType = currentIncidentType.HasAbuseType;
                            returnObject.ShowIncidentDetail = currentIncidentType.HasDetails;
                        }

                        Staff nominatedManager = nominatedManagerRepository.Find(
                            new Specification<Staff>(x => x.SiteStaff.Any(y => y.SiteCode == destination.SiteCode &&
                                                                            y.Responsibility.Trim() == ServiceConstants.SITE_STAFF_RESPONSIBILITY_NOMINATED_MANAGER))
                            ).SingleOrDefault();

                        List<string> deputyNominatedManagers = nominatedManagerRepository.Find(
                            new Specification<Staff>(x => x.SiteStaff.Any(y => y.SiteCode == destination.SiteCode &&
                                                                            y.Responsibility.Trim() == ServiceConstants.SITE_STAFF_RESPONSIBILITY_DEPUTY_NOMINATED_MANAGER))
                                                                            ).Select(x => x.FirstName + " " + x.LastName).ToList();

                        if (nominatedManager != null)
                        {
                            // gary
                            returnObject.NominatedManagercode = nominatedManager.Code;
                            returnObject.NominatedManager = nominatedManager.FirstName + " " + nominatedManager.LastName;
                            if (deputyNominatedManagers.Count == 0)
                            {
                                returnObject.DeputyNominatedManagers = "";
                            }
                            else
                            {
                                returnObject.DeputyNominatedManagers = deputyNominatedManagers.Aggregate((Current, Next) => Current + "; " + Next);
                            }
                        }

                        Guid currentUserCode;
                        // Determine if the user is a member of UCB staff or an intranet user. If staff, the currentUser will be a guid otherwise currentUser will be the windows ID.
                        bool isGuid = Guid.TryParse(currentUser, out currentUserCode);

                        // If the user is a member of UCB staff then find out what UCB role they have and work out if incident should be read only or edit
                        if (isGuid)
                        {
                            List<StaffAttributes> attributes = staffAttributesRepository.Find(new Specification<StaffAttributes>(x => x.ApplicationAttribute.IsRole && x.LookupValue == "Yes" && x.StaffCode == currentUserCode && x.ApplicationAttribute.Application.ApplicationName == "UCB"), x => x.ApplicationAttribute.AttributeName, "ApplicationAttribute", "Application").ToList();
                            List<string> currentRoles = attributes.Select(x => x.ApplicationAttribute.AttributeName).ToList();

                            List<KeyValuePair<string, bool>> roleAndIsReadOnlyList = new List<KeyValuePair<string, bool>>();

                            //If Admin user then not read only
                            if (currentRoles.Contains(AppRoles.ADMIN))
                            {
                                roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.ADMIN, false));
                            }

                            //If incident is for a site that the user is not a nominated manager for then read-only
                            if (currentRoles.Contains(AppRoles.NOMINATED_MANAGER))
                            {
                                Guid siteCodeForIncident = destination.SiteCode;

                                nominatedManager = nominatedManagerRepository.Find(
                                    new Specification<Staff>(x => x.SiteStaff.Any(y => y.SiteCode == destination.SiteCode && y.StaffCode == currentUserCode
                                                                                    && y.Responsibility.Trim() == ServiceConstants.SITE_STAFF_RESPONSIBILITY_NOMINATED_MANAGER))
                                    ).SingleOrDefault();
                                
                                if (nominatedManager == null)
                                {
                                    roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.NOMINATED_MANAGER, true));
                                }
                                else
                                {
                                    roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.NOMINATED_MANAGER, false));
                                }
                            }

                            //If incident is for a site that the user is not a deputy nominated manager for then read-only
                            if (currentRoles.Contains(AppRoles.DEPUTY_NOMINATED_MANAGER))
                            {

                                Guid siteCodeForInident = destination.SiteCode;
                                
                                Staff deputyNominatedManager = nominatedManagerRepository.Find(
                                    new Specification<Staff>(x => x.SiteStaff.Any(y => y.SiteCode == destination.SiteCode && y.StaffCode == currentUserCode
                                                                                    && y.Responsibility.Trim() == ServiceConstants.SITE_STAFF_RESPONSIBILITY_DEPUTY_NOMINATED_MANAGER))
                                    ).SingleOrDefault();
                                
                                if (deputyNominatedManager == null)
                                {
                                    roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.DEPUTY_NOMINATED_MANAGER, true));
                                }
                                else
                                {
                                    roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.DEPUTY_NOMINATED_MANAGER, false));
                                }
                            }
                            //If user is read only, or trade union, then read only
                            if (currentRoles.Contains(AppRoles.READ_ONLY) || currentRoles.Contains(AppRoles.TRADE_UNION))
                            {
                                roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.TRADE_UNION, true));
                            }

                            //If user is Business Area Manager and incident is within there level 3 then edit, else read only
                            if (currentRoles.Contains(AppRoles.BUSINESS_AREA_MANAGER))
                            {
                                //Get level 3 orgs for current user
                                IEnumerable<Organisation> level3OrgsForBusinessAreaManager = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.OrganisationType.LevelNumber == 3 &&
                                x.OrganisationType.OrganisationTypeGroup.ApplicationOrganisationTypeGroup.Any(v => v.Application.ApplicationName == "UCB")).And(new Specification<Organisation>(y => y.StaffOrganisation.Any(z => z.StaffCode == currentUserCode))));

                                //Get level 3 org for the incident
                                Organisation level3OrganisationForIncident = staffMemberBusinessRepository.Find(new Specification<Organisation>(x => x.OrganisationHierarchy1.Any(y => y.OrganisationCode == currentLevel6organisationForIncident.Code && y.HopsBetweenOrgAndAncestor == 3))).Single();

                                //Check if the level 3 org for the incident is one of the level 3 orgs for the current user
                                if (level3OrgsForBusinessAreaManager == null)
                                {
                                    roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.BUSINESS_AREA_MANAGER, true));
                                }
                                else
                                {
                                    if (level3OrgsForBusinessAreaManager.Contains(level3OrganisationForIncident))
                                    {
                                        roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.BUSINESS_AREA_MANAGER, false));
                                    }
                                    else
                                    {
                                        roleAndIsReadOnlyList.Add(new KeyValuePair<string, bool>(AppRoles.BUSINESS_AREA_MANAGER, true));
                                    }
                                }
                            }

                            //If any of the roles allow edit access then return a read-only value of false.
                            foreach (KeyValuePair<string, bool> item in roleAndIsReadOnlyList)
                            {
                                if (item.Value == false)
                                {
                                    returnObject.IsReadOnly = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (returnObject.IncidentItem != null)
                    {
                        // no errors so do an audit
                        string auditText = string.Format(AuditConstants.AUDIT_TEXT_VIEW_INCIDENTREFERALL, returnObject.IncidentItem.Type);
                        Dictionary<string, string> auditProperties = new Dictionary<string, string>();
                        auditProperties.Add(AuditConstants.CODE_PROPERTY, returnObject.IncidentItem.Code.ToString());
                        auditProperties.Add(AuditConstants.INCIDENT_ID_PROPERTY, returnObject.IncidentItem.IncidentID.ToString(CultureInfo.InvariantCulture));                        
                        // check if we need to audit a NINO being viewed
                        if (returnObject.CustomerItem != null && !string.IsNullOrWhiteSpace(returnObject.CustomerItem.NINO))
                        {
                            // they can see a NINO so audit that too!
                            auditProperties.Add(AuditConstants.NINO_PROPERTY, returnObject.CustomerItem.NINO);
                        }

                        uow.CustomAudit(auditText, AuditConstants.VIEW_ACTION, auditProperties);
                        uow.Commit();
                    }

                    return returnObject;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region TransferIncidentToNewNominatedManager

        /// <summary>
        /// Update a Incident
        /// </summary>
        public IncidentDC TransferIncidentToNewNominatedManager(string currentUser, string user, string appID, string overrideID, IncidentDC incidentDC, string siteCode)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            Repository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Organisation> organisationRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return TransferIncidentToNewNominatedManager(currentUser, user, appID, overrideID, incidentDC, siteCode, incidentRepository, organisationRepository, 
                incidentUpdateEventRepository, staffRepository, uow, exceptionManager);
        }

        /// <summary>
        /// Update a Incident
        /// </summary>
        public IncidentDC TransferIncidentToNewNominatedManager(string currentUser, string user, string appID, string overrideID, IncidentDC incidentDC,
            string siteCode, IRepository<Incident> incidentRepository, IRepository<Organisation> organisationRepository, IRepository<IncidentUpdateEvent> incidentUpdateEventRepository,
            IRepository<Staff> staffRepository, IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == incidentDC) throw new ArgumentOutOfRangeException("incidentDC");
                if (null == siteCode) throw new ArgumentOutOfRangeException("siteCode");
                if (null == incidentRepository) throw new ArgumentOutOfRangeException("incidentRepository");
                if (null == organisationRepository) throw new ArgumentOutOfRangeException("organisationRepository");
                if (null == incidentUpdateEventRepository) throw new ArgumentOutOfRangeException("incidentUpdateEventRepository");
                if (null == staffRepository) throw new ArgumentOutOfRangeException("staffRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                // Map data contract to model
                Incident incidentItem = Mapper.Map<IncidentDC, Incident>(incidentDC);

                Guid newSiteCode = Guid.Parse(siteCode);

                Guid userWhoUpdatedIncident = Guid.Parse(currentUser);

                //IncidentUpdateEvent
                string staffName = staffRepository.Find(new Specification<Staff>(x => x.Code == userWhoUpdatedIncident)).Select(x => x.FirstName + " " + x.LastName).First();
                IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
                incidentUpdateEventItem.Code = Guid.NewGuid();
                incidentUpdateEventItem.DateTime = DateTime.Now;
                incidentUpdateEventItem.IncidentCode = incidentItem.Code;
                incidentUpdateEventItem.Type = "Update";
                incidentUpdateEventItem.UpdateBy = staffName;

                using (uow)
                {
                    //find the organisation that goes with the site
                    Guid newOrganisationCode = organisationRepository.Find(new Specification<Organisation>(x => x.Site.Any(y => y.Code == newSiteCode))).Single().Code;

                    //Amend the site on the incident
                    //incidentItem.OrganisationCode = newOrganisationCode;
                    incidentItem.SiteCode = newSiteCode;

                    //Update the incident
                    incidentRepository.Update(incidentItem);

                    //IncidentUpdateEvent
                    List<IncidentUpdateEvent> incidentUpdateEventList = incidentUpdateEventRepository.Find(new Specification<IncidentUpdateEvent>(x => x.IncidentCode == incidentItem.Code && x.Type == "Update")).ToList();
                    incidentUpdateEventList = incidentUpdateEventList.OrderBy(x => x.DateTime).ToList();
                    //Only store last 5 update events (list is ordered by date time (asc) so the elementAt 0 will be the oldest
                    if (incidentUpdateEventList.Count == 5)
                        incidentUpdateEventRepository.Delete(incidentUpdateEventList.ElementAt(0));

                    incidentUpdateEventRepository.Add(incidentUpdateEventItem);

                    // Commit unit of work
                    uow.Commit();
                }

                //Map updated models back to data contracts (for updated RowIdentifier)
                incidentDC = Mapper.Map<Incident, IncidentDC>(incidentItem);

                IncidentDC returnObject = new IncidentDC();
                returnObject = incidentDC;

                return returnObject;

            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion

        #region Behaviour for Incident Transfers

        #region SearchSites

        public TransferSiteSearchVMDC SearchTransferSites(string currentUser, string user, string appID, string overrideID, TransferSiteSearchCriteriaDC searchCriteria, int page, int pageSize)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Site> dataRepository = new Repository<Site>(uow.ObjectContext, currentUser, user, appID, overrideID);

            // Create specification for filtering
            ISpecification<Site> specification = new Specification<Site>();

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return SearchTransferSites(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, specification, dataRepository, uow, exceptionManager);
        }

        // Partial method for evaluation of search criteria
        partial void EvaluateSearchCriteria(TransferSiteSearchCriteriaDC searchCriteria, ref ISpecification<Site> specification);

        public TransferSiteSearchVMDC SearchTransferSites(string currentUser, string user, string appID, string overrideID, TransferSiteSearchCriteriaDC searchCriteria, int page, int pageSize,
        ISpecification<Site> specification, IRepository<Site> dataRepository, IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == specification) throw new ArgumentOutOfRangeException("specification");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {
                    // Evaluate search criteria if supplied
                    if (null != searchCriteria)
                    {
                        EvaluateSearchCriteria(searchCriteria, ref specification);
                    }

                    ISpecification<Site> isActiveSpecification = new Specification<Site>(x => x.IsActive == true);
                    specification = specification.And(isActiveSpecification);

                    if (searchCriteria != null)
                    {
                        if (!String.IsNullOrEmpty(searchCriteria.SiteName))
                        {
                            ISpecification<Site> containsSpecification = new Specification<Site>(x => x.SiteName.ToLower().Contains(searchCriteria.SiteName.ToLower()));
                            specification = specification.And(containsSpecification);
                        }
                    }
                    
                    // Set default sort expression
                    System.Linq.Expressions.Expression<Func<Site, Object>> sortExpression = x => x.SiteName;

                    // Find all items that satisfy the specification created above.
                    IEnumerable<Site> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize, new string[] { "SiteStaff", "SiteStaff.Staff" });

                    // Get total count of items for search critera
                    int itemCount = dataRepository.Count(specification);

                    TransferSiteSearchVMDC results = new TransferSiteSearchVMDC();
                    results.SearchCriteria = searchCriteria;
                    results.RecordCount = itemCount;
                    results.MatchList = new List<TransferSiteDC>();
                    foreach (Site s in dataEntities)
                    {
                        string nomMgr = s.SiteStaff.Where<SiteStaff>(
                                                                        x => x.Responsibility.Trim() == ServiceConstants.SITE_STAFF_RESPONSIBILITY_NOMINATED_MANAGER
                                                                    ).Select<SiteStaff, string>(
                                                                                                    x => String.Format("{0} {1}", x.Staff.FirstName, x.Staff.LastName)
                                                                                                ).FirstOrDefault<string>();

                        string[] depNomMgrs = s.SiteStaff.Where<SiteStaff>(
                                                                                x => x.Responsibility.Trim() == ServiceConstants.SITE_STAFF_RESPONSIBILITY_DEPUTY_NOMINATED_MANAGER
                                                                            ).Select(
                                                                                        x => String.Format("{0} {1},", x.Staff.FirstName, x.Staff.LastName)
                                                                                     ).ToArray<string>();

                        TransferSiteDC site = new TransferSiteDC() 
                        {
                            Code = s.Code,
                            SiteName = s.SiteName,
                            NominatedManager = nomMgr ?? String.Empty,
                            DeputyNominatedManagers = (depNomMgrs != null) ? String.Concat(depNomMgrs) : string.Empty
                        };
                        
                        results.MatchList.Add(site);
                    }
                    
                    return results;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        
        #endregion
        #endregion

        #region Referral Update

        /// <summary>
        /// Update a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>

        public IncidentVMDC UpdateReferral(string currentUser, string user, string appID, string overrideID, string incidentStatus, IncidentDC incidentDC, CustomerDC customerDC, NarrativeDC furtherInfoNarrativeDC, NarrativeDC reviewActionNarrativeDC, List<String> controlMeasureCodes, List<String> systemMarkedCodes, List<String> interestedPartyCodes)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            Repository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Narrative> narrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Narrative> reviewActionNarrativeRepository = new Repository<Narrative>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<CustomerControlMeasure> customerControlMeasureRepository = new Repository<CustomerControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentSystemMarked> incidentSystemMarkedRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentInterestedParty> incidentInterestedPartyRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);
            Repository<Site> siteRepository = new Repository<Site>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return UpdateReferral(currentUser, user, appID, overrideID, incidentStatus, incidentDC, customerDC, furtherInfoNarrativeDC, reviewActionNarrativeDC, controlMeasureCodes, systemMarkedCodes,
                interestedPartyCodes, incidentRepository, customerRepository, narrativeRepository, reviewActionNarrativeRepository, customerControlMeasureRepository,
                incidentSystemMarkedRepository, incidentInterestedPartyRepository, incidentUpdateEventRepository, staffRepository, siteRepository, uow, exceptionManager);
        }

        /// <summary>
        /// Update a Incident
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="dc"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public IncidentVMDC UpdateReferral(string currentUser, string user, string appID, string overrideID,
            string incidentStatus, IncidentDC incidentDC, CustomerDC customerDC,  NarrativeDC furtherInfoNarrativeDC, NarrativeDC reviewActionNarrativeDC,
            List<String> controlMeasureCodes, List<String> systemMarkedCodes, List<String> interestedPartyCodes,
            IRepository<Incident> incidentRepository, IRepository<Customer> customerRepository, IRepository<Narrative> narrativeRepository, IRepository<Narrative> reviewActionNarrativeRepository,
            IRepository<CustomerControlMeasure> customerControlMeasureRepository,
            IRepository<IncidentSystemMarked> incidentSystemMarkedRepository, IRepository<IncidentInterestedParty> incidentInterestedPartyRepository,
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, IRepository<Staff> staffRepository, IRepository<Site> siteRepository, IUnitOfWork uow,
            IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (string.IsNullOrEmpty(incidentStatus)) throw new ArgumentOutOfRangeException("incidentStatus");
                if (null == incidentDC) throw new ArgumentOutOfRangeException("incidentDC");
                if (null == customerDC) throw new ArgumentOutOfRangeException("customerDC");
                if (null == furtherInfoNarrativeDC) throw new ArgumentOutOfRangeException("furtherInfoNarrativeDC");
                //if (null == reviewActionNarrativeDC) throw new ArgumentOutOfRangeException("reviewActionNarrativeDC");

                if (null == incidentRepository) throw new ArgumentOutOfRangeException("incidentRepository");
                if (null == customerRepository) throw new ArgumentOutOfRangeException("customerRepository");
                if (null == narrativeRepository) throw new ArgumentOutOfRangeException("narrativeRepository");
                if (null == reviewActionNarrativeRepository) throw new ArgumentOutOfRangeException("reviewActionNarrativeRepository");

                if (null == customerControlMeasureRepository) throw new ArgumentOutOfRangeException("customerControlMeasureRepository");
                if (null == incidentSystemMarkedRepository) throw new ArgumentOutOfRangeException("incidentSystemMarkedRepository");
                if (null == incidentInterestedPartyRepository) throw new ArgumentOutOfRangeException("incidentInterestedPartyRepository");
                if (null == incidentUpdateEventRepository) throw new ArgumentOutOfRangeException("incidentUpdateEventRepository");
                if (null == staffRepository) throw new ArgumentOutOfRangeException("staffRepository");
                if (null == siteRepository) throw new ArgumentOutOfRangeException("siteRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                // Map data contract to model
                Incident incidentItem = Mapper.Map<IncidentDC, Incident>(incidentDC);
           
                Customer customerItem = Mapper.Map<CustomerDC, Customer>(customerDC);
     
                Narrative furtherInfoNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(furtherInfoNarrativeDC);
                Narrative reviewActionNarrativeItem = Mapper.Map<NarrativeDC, Narrative>(reviewActionNarrativeDC);

                Guid userWhoUpdatedIncident = Guid.Parse(currentUser);
                //IncidentUpdateEvent
                Guid siteOrganisationCode= Guid.Empty;
                Guid staffMemberBusinessCode = Guid.Empty;
                Guid staffMemberBusinessAreaCode = Guid.Empty;


                string staffName = staffRepository.Find(new Specification<Staff>(x => x.Code == userWhoUpdatedIncident)).Select(x => x.FirstName + " " + x.LastName).First();

                using (uow)
                {
                    // Get the site for the Home Office Site code provided
                    Specification<Site> siteSpecification = new Specification<Site>(x => x.Code == incidentDC.StaffMemberHomeOfficeSiteCode);
                    Site site = siteRepository.Find(siteSpecification).Single();

                    // Save Organisations
                    siteOrganisationCode = site.OrganisationCode;
                    staffMemberBusinessCode = incidentDC.StaffMemberBusinessCode;
                    staffMemberBusinessAreaCode = incidentDC.StaffMemberBusinessAreaCode;


                    //Only add Customer if it doesnt already exist
                    if (customerItem.Code == Guid.Empty)
                    {
                        customerItem.Code = Guid.NewGuid();
                        incidentItem.CustomerCode = customerItem.Code;

                        customerRepository.Add(customerItem);
                    }
                    else
                    {
                        customerRepository.Update(customerItem);
                    }

                    //Only add Further Info Narrative if it doesn't already exist and something has been entered. 
                    if (furtherInfoNarrativeItem.Code == Guid.Empty)
                    {
                        if (!String.IsNullOrEmpty(furtherInfoNarrativeItem.NarrativeDescription))
                        {
                            furtherInfoNarrativeItem.Code = Guid.NewGuid();
                            incidentItem.FurtherInfoNarrativeCode = furtherInfoNarrativeItem.Code;
                            furtherInfoNarrativeItem.NarrativeType = "ReferralNotes";

                            narrativeRepository.Add(furtherInfoNarrativeItem);
                        }
                    }
                    else
                    {
                        narrativeRepository.Update(furtherInfoNarrativeItem);
                    }

                    if (reviewActionNarrativeItem.Code == Guid.Empty)
                    {
                        if (!String.IsNullOrEmpty(reviewActionNarrativeItem.NarrativeDescription))
                        {
                            reviewActionNarrativeItem.Code = Guid.NewGuid();
                            incidentItem.ReviewActionNarrativeCode = reviewActionNarrativeItem.Code;
                            reviewActionNarrativeItem.NarrativeType = "ReviewAction";

                            narrativeRepository.Add(reviewActionNarrativeItem);
                        }
                    }
                    else
                    {
                        narrativeRepository.Update(reviewActionNarrativeItem);
                    }

                    //Only add Incident if it doesnt already exist
                    if (incidentItem.Code == Guid.Empty)
                    {

                        incidentItem.Code = Guid.NewGuid();
                        incidentItem.IncidentStatus = incidentStatus;
                        incidentItem.Type = "Referral";
                        incidentItem.NumberOfRecords = 1; //Used for totalling in ad-hoc reports solution
                        //incidentItem.OrganisationCode = incidentItem.StaffMemberHomeOfficeCode; //set this equal to staff member home office (level6 org). 

                        // Site and Home Office Site code are the same initially
                        incidentItem.SiteCode = site.Code;
                        incidentItem.StaffMemberHomeOfficeSiteCode = site.Code;

                        //Populate FiscalYear, FiscalQuarter and FiscalMonth based on the incident date.
                        string fiscalMonthText = CalculateFiscalMonth(incidentItem.IncidentDate.Month);
                        string fiscalMonthNumber = CalculateFiscalMonthNumber(incidentItem.IncidentDate.Month);

                        PopulateIncidentFiscalDateFields(incidentItem, fiscalMonthText, fiscalMonthNumber);

                        incidentRepository.Add(incidentItem);

                        IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
                        incidentUpdateEventItem.Code = Guid.NewGuid();
                        incidentUpdateEventItem.DateTime = DateTime.Now;
                        incidentUpdateEventItem.IncidentCode = incidentItem.Code;
                        incidentUpdateEventItem.Type = "Create";
                        incidentUpdateEventItem.UpdateBy = staffName;

                        incidentUpdateEventRepository.Add(incidentUpdateEventItem);

                    }
                    else
                    {
                        //Don't reset back to a status of new if save has been  pressed
                        if (incidentStatus != "New")
                        {
                            incidentItem.IncidentStatus = incidentStatus;
                        }

                        // Re-calculate FiscalYear, FiscalQuarter and FiscalMonth based on the incident date.
                        string fiscalMonthText = CalculateFiscalMonth(incidentItem.IncidentDate.Month);
                        string fiscalMonthNumber = CalculateFiscalMonthNumber(incidentItem.IncidentDate.Month);

                        PopulateIncidentFiscalDateFields(incidentItem, fiscalMonthText, fiscalMonthNumber);

                        incidentRepository.Update(incidentItem);

                        //IncidentUpdateEvent
                        List<IncidentUpdateEvent> incidentUpdateEventList = incidentUpdateEventRepository.Find(new Specification<IncidentUpdateEvent>(x => x.IncidentCode == incidentItem.Code && x.Type == "Update")).ToList();
                        incidentUpdateEventList = incidentUpdateEventList.OrderBy(x => x.DateTime).ToList();
                        //Only store last 5 update events (list is ordered by date time (asc) so the elementAt 0 will be the oldest
                        if (incidentUpdateEventList.Count == 5)
                            incidentUpdateEventRepository.Delete(incidentUpdateEventList.ElementAt(0));

                        IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
                        incidentUpdateEventItem.Code = Guid.NewGuid();
                        incidentUpdateEventItem.DateTime = DateTime.Now;
                        incidentUpdateEventItem.IncidentCode = incidentItem.Code;
                        incidentUpdateEventItem.Type = "Update";
                        incidentUpdateEventItem.UpdateBy = staffName;

                        incidentUpdateEventRepository.Add(incidentUpdateEventItem);

                    }


                    // customer contingency arrangements
                    // delete existing ones and add new ones
                    IEnumerable<CustomerControlMeasure> existingControlMeasureList = customerControlMeasureRepository.Find(x => x.CustomerCode == incidentItem.CustomerCode);
                    foreach (CustomerControlMeasure ca in existingControlMeasureList)
                        customerControlMeasureRepository.Delete(ca);
                    foreach (string ca in controlMeasureCodes)
                        customerControlMeasureRepository.Add(new CustomerControlMeasure() { Code = Guid.NewGuid(), ControlMeasureCode = Guid.Parse(ca), CustomerCode = incidentItem.CustomerCode.Value });

                    // Incident system marked
                    // delete existing ones and add new ones
                    IEnumerable<IncidentSystemMarked> existingSystemMarkedList = incidentSystemMarkedRepository.Find(x => x.IncidentCode == incidentItem.Code);
                    foreach (IncidentSystemMarked sm in existingSystemMarkedList)
                        incidentSystemMarkedRepository.Delete(sm);
                    foreach (string sm in systemMarkedCodes)
                        incidentSystemMarkedRepository.Add(new IncidentSystemMarked() { Code = Guid.NewGuid(), SystemMarkedCode = Guid.Parse(sm), IncidentCode = incidentItem.Code });

                    // Incident system marked
                    // delete existing ones and add new ones
                    IEnumerable<IncidentInterestedParty> existingInterestedPartyList = incidentInterestedPartyRepository.Find(x => x.IncidentCode == incidentItem.Code);
                    foreach (IncidentInterestedParty ip in existingInterestedPartyList)
                        incidentInterestedPartyRepository.Delete(ip);
                    foreach (string ip in interestedPartyCodes)
                        incidentInterestedPartyRepository.Add(new IncidentInterestedParty() { Code = Guid.NewGuid(), InterestedPartyCode = Guid.Parse(ip), IncidentCode = incidentItem.Code });
                    // Commit unit of work

                    uow.Commit();
                }

                // Create aggregate data contract
                IncidentVMDC returnObject = new IncidentVMDC();

                // Add new item to aggregate and Map updated models back to data contracts (for updated RowIdentifier)
                returnObject.IncidentItem = Mapper.Map<Incident, IncidentDC>(incidentItem);
                returnObject.CustomerItem = Mapper.Map<Customer, CustomerDC>(customerItem);
                returnObject.FurtherInfoNarrativeItem = Mapper.Map<Narrative, NarrativeDC>(furtherInfoNarrativeItem);
                returnObject.ReviewActionNarrativeItem = Mapper.Map<Narrative, NarrativeDC>(reviewActionNarrativeItem);

                returnObject.IncidentItem.StaffMemberBusinessCode = staffMemberBusinessCode;
                returnObject.IncidentItem.StaffMemberBusinessAreaCode = staffMemberBusinessAreaCode;
                returnObject.IncidentItem.StaffMemberHomeOfficeCode = siteOrganisationCode;
                returnObject.IncidentItem.OrganisationCode = siteOrganisationCode;


                return returnObject;

            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion
        private string CalculateFiscalMonth(int month)
        {
             switch (month)
             {
                    case 1:
                        return "January";
                    case 2:
                        return "February";
                    case 3:
                        return "March";
                    case 4:
                        return "April";
                    case 5:
                        return "May";
                    case 6:
                        return "June";      
                    case 7:
                        return "July";
                    case 8:
                        return "August";
                    case 9:
                        return "September";
                    case 10:
                        return "October";
                    case 11:
                        return "November";
                    case 12:
                        return "December";
                    default:
                        return "";
                }
        }
        private string CalculateFiscalMonthNumber(int month)
        {
            switch (month)
            {
                case 1:
                    return "10";
                case 2:
                    return "11";
                case 3:
                    return "12";
                case 4:
                    return "01";
                case 5:
                    return "02";
                case 6:
                    return "03";
                case 7:
                    return "04";
                case 8:
                    return "05";
                case 9:
                    return "06";
                case 10:
                    return "07";
                case 11:
                    return "08";
                case 12:
                    return "09";
                default:
                    return "";
            }
        }

        private void PopulateIncidentFiscalDateFields(Incident incidentItem, string fiscalMonthText, string fiscalMonthNumber)
        {        
                switch (incidentItem.IncidentDate.Month)
                {
                    case 1:
                    case 2:
                    case 3:
                        incidentItem.FiscalYear = incidentItem.IncidentDate.AddYears(-1).Year;
                        incidentItem.FiscalQuarter = Int32.Parse(incidentItem.FiscalYear.ToString() + "04");
                        incidentItem.FiscalMonth = Int32.Parse(incidentItem.FiscalYear.ToString() + fiscalMonthNumber);
                        incidentItem.FiscalMonthAsText = incidentItem.FiscalMonth + "(" + fiscalMonthText +")";
                        break;
                    case 4: 
                    case 5:
                    case 6:
                        incidentItem.FiscalYear = incidentItem.IncidentDate.Year;
                        incidentItem.FiscalQuarter = Int32.Parse(incidentItem.FiscalYear.ToString() + "01");
                        incidentItem.FiscalMonth = Int32.Parse(incidentItem.FiscalYear.ToString() + fiscalMonthNumber);
                        incidentItem.FiscalMonthAsText = incidentItem.FiscalMonth + "(" + fiscalMonthText +")";
                        break;
                    case 7:
                    case 8:
                    case 9:
                        incidentItem.FiscalYear = incidentItem.IncidentDate.Year;
                        incidentItem.FiscalQuarter = Int32.Parse(incidentItem.FiscalYear.ToString() + "02");
                        incidentItem.FiscalMonth = Int32.Parse(incidentItem.FiscalYear.ToString() + fiscalMonthNumber);
                        incidentItem.FiscalMonthAsText = incidentItem.FiscalMonth + "(" + fiscalMonthText + ")";
                        break;
                    case 10:
                    case 11:
                    case 12:
                        incidentItem.FiscalYear = incidentItem.IncidentDate.Year;
                        incidentItem.FiscalQuarter = Int32.Parse(incidentItem.FiscalYear.ToString() + "03");
                        incidentItem.FiscalMonth = Int32.Parse(incidentItem.FiscalYear.ToString() + fiscalMonthNumber);
                        incidentItem.FiscalMonthAsText = incidentItem.FiscalMonth + "(" + fiscalMonthText + ")";
                        break;

                }
        }
        #endregion
    }

}
