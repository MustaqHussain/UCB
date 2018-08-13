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
        #region LinkedIncidents

        public List<IncidentLinkDC> LinkIncidentsByNino(string currentUser, string user, string appID, string overrideID, string incidentCode, string nino)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<IncidentLink> incidentLinkRepository = new Repository<IncidentLink>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);

            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return LinkIncidentsByNino(currentUser, user, appID, overrideID, incidentCode, nino,
                        incidentLinkRepository, incidentRepository, customerRepository, incidentUpdateEventRepository, staffRepository, uow, exceptionManager);
        }

        public List<IncidentLinkDC> LinkIncidentsByNino(string currentUser, string user, string appID, string overrideID, string incidentCode, string nino,
            IRepository<IncidentLink> incidentLinkRepository,
            IRepository<Incident> incidentRepository,
            IRepository<Customer> customerRepository,
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, 
            IRepository<Staff> staffRepository,
            IUnitOfWork uow,
            IExceptionManager exceptionManager)
        {

            try
            {
                #region Parameter validation

                // Validate parameters


                #endregion

                using (uow)
                {
                    // Convert code to Guid
                    Guid codeGuid = Guid.Parse(incidentCode);

                    // Existing links for this Incident
                    IEnumerable<IncidentLink> originalIncidentLink = incidentLinkRepository.Find(x => x.IncidentCode == codeGuid);

                    // Get all Customer records with same NINO as mainCustomer
                    IEnumerable<Customer> sameNINICustomer = customerRepository.Find(x => x.NINO.ToUpper().Trim() == nino.ToUpper().Trim());

                    foreach (Customer cust in sameNINICustomer)
                    {
                        Incident linkToIncident = incidentRepository.Find(x => x.CustomerCode == cust.Code).FirstOrDefault();
                        if (linkToIncident == null ) continue;

                        // Do not create link back to itself
                        if ( linkToIncident.Code == codeGuid )
                        {
                            // Don't Link to itself, skip
                            continue;
                        }

                        // Do not create link if already exist
                        if (originalIncidentLink.Count() > 0 && originalIncidentLink.Where(x => x.LinkedIncidentCode == linkToIncident.Code).Count() > 0)
                        {
                            // Link already exist, skip
                            continue;
                        }

                        AddToIncidentLink( incidentLinkRepository, codeGuid, cust, linkToIncident );
                    }

                    AddToIncidentUpdateEvent(incidentUpdateEventRepository, staffRepository, codeGuid, currentUser);

                    // Commit unit of work
                    uow.Commit();
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }

            return null;
        }
 
        public List<IncidentLinkDC> LinkIncidentsByIncidentID(string currentUser, string user, string appID, string overrideID, string incidentCode, int linkToIncidentID)
        {

            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<IncidentLink> incidentLinkRepository = new Repository<IncidentLink>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Customer> customerRepository = new Repository<Customer>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return LinkIncidentsByIncidentID(currentUser, user, appID, overrideID, incidentCode, linkToIncidentID, 
                        incidentLinkRepository, incidentRepository, customerRepository, incidentUpdateEventRepository,
                        staffRepository, uow, exceptionManager);
        }

        public List<IncidentLinkDC> LinkIncidentsByIncidentID(string currentUser, string user, string appID, string overrideID, string incidentCode, int linkToIncidentID,
            IRepository<IncidentLink> incidentLinkRepository,
            IRepository<Incident> incidentRepository,
            IRepository<Customer> customerRepository,
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, IRepository<Staff> staffRepository, 
            IUnitOfWork uow,
            IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters


                #endregion

                using (uow)
                {
                    //// Convert code to Guid
                    Guid codeGuid = Guid.Parse(incidentCode);

                    //// Existing links for this Incident
                    IEnumerable<IncidentLink> originalIncidentLink = incidentLinkRepository.Find(x => x.IncidentCode == codeGuid);

                    Incident linkToIncident = incidentRepository.Find(x => x.IncidentID == linkToIncidentID).FirstOrDefault();
                    if (linkToIncident != null)
                    {
                        // Do not create link back to itself
                        if ((originalIncidentLink.Count() > 0 && originalIncidentLink.Where(x => x.IncidentCode == linkToIncident.Code).Count() > 0) != true)
                        {
                            // Do not create link if already exist
                            if ((originalIncidentLink.Count() > 0 && originalIncidentLink.Where(x => x.LinkedIncidentCode == linkToIncident.Code).Count() > 0) != true)
                            {
                                // Get one and only possible customer details
                                Customer cust = customerRepository.Find(x => x.Code == linkToIncident.CustomerCode).FirstOrDefault();

                                AddToIncidentLink(incidentLinkRepository, codeGuid, cust, linkToIncident);
                            }
                        }
                    }

                    AddToIncidentUpdateEvent(incidentUpdateEventRepository, staffRepository, codeGuid, currentUser);

                    // Commit unit of work
                    uow.Commit();
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }

            return null;
        }

        #endregion

        private void AddToIncidentLink(IRepository<IncidentLink> incidentLinkRepository, Guid incidentCode, Customer customer, Incident linkToIncident)
        {
            // Create new link
            String name = ((String.IsNullOrEmpty(customer.FirstName) ? "" : customer.FirstName.Trim()) + " " + (String.IsNullOrEmpty(customer.LastName) ? "" : customer.LastName.Trim())).Trim();
            name = name.Substring(0, name.Length >= 50 ? 49 : name.Length).Trim();
            IncidentLink linkedCustomer = new IncidentLink()
            {
                Code = Guid.NewGuid(),
                IncidentCode = incidentCode,
                LinkedIncidentCode = linkToIncident.Code,
                CustomerName = name,
                IncidentId = linkToIncident.IncidentID
            };
            // Add linked incident to db
            incidentLinkRepository.Add(linkedCustomer);
        }

        private void AddToIncidentUpdateEvent(IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, IRepository<Staff> staffRepository, Guid incidentCode, string currentUser)
        {
            // Add to Incident history IncidentUpdateEvent                    
            Guid userWhoUpdatedIncident = Guid.Parse(currentUser);

            string staffName = staffRepository.Find(new Specification<Staff>(x => x.Code == userWhoUpdatedIncident)).Select(x => x.FirstName + " " + x.LastName).First();

            IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
            incidentUpdateEventItem.Code = Guid.NewGuid();
            incidentUpdateEventItem.DateTime = DateTime.Now;
            incidentUpdateEventItem.IncidentCode = incidentCode;
            incidentUpdateEventItem.Type = "Update";
            incidentUpdateEventItem.UpdateBy = staffName;

            //IncidentUpdateEvent
            List<IncidentUpdateEvent> incidentUpdateEventList = incidentUpdateEventRepository.Find(new Specification<IncidentUpdateEvent>(x => x.IncidentCode == incidentCode && x.Type == "Update")).ToList();
            incidentUpdateEventList = incidentUpdateEventList.OrderBy(x => x.DateTime).ToList();
            //Only store last 5 update events (list is ordered by date time (asc) so the elementAt 0 will be the oldest
            if (incidentUpdateEventList.Count == 5)
                incidentUpdateEventRepository.Delete(incidentUpdateEventList.ElementAt(0));

            incidentUpdateEventRepository.Add(incidentUpdateEventItem);
        }

    }

}
