using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Ucb.WebServices.DataContracts;
using Dwp.Adep.Ucb.DataServices;
using Dwp.Adep.Ucb.DataServices.Models;
using AutoMapper;
using Dwp.Adep.Ucb.WebServices.Exceptions;
using Dwp.Adep.Ucb.WebServices.MessageContracts.Exceptions;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial class UcbService 
    {

        #region DeleteAttachmentAndData

        /// <summary>
        /// Delete a Attachment
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="lockID"></param>
        public void DeleteAttachmentAndData(string currentUser, string user, string appID, string overrideID, string code, byte[] lockID)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Attachment> attachmentRepository = new Repository<Attachment>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<AttachmentData> attachmentDataRepository = new Repository<AttachmentData>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<IncidentUpdateEvent> incidentUpdateEventRepository = new Repository<IncidentUpdateEvent>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            DeleteAttachmentAndData(currentUser, user, appID, overrideID, code, lockID, attachmentRepository, attachmentDataRepository, incidentUpdateEventRepository, staffRepository, uow, exceptionManager);
        }

        /// <summary>
        /// Update a Attachment
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <param name="lockID"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        public void DeleteAttachmentAndData(string currentUser, string user, string appID, string overrideID, string code, byte[] lockID, IRepository<Attachment> attachmentRepository, IRepository<AttachmentData> attachmentdataRepository, IRepository<IncidentUpdateEvent> incidentUpdateEventRepository, IRepository<Staff> staffRepository,
            IUnitOfWork uow, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (string.IsNullOrEmpty(code)) throw new ArgumentOutOfRangeException("code");
                //if (lockID.Length==0) throw new ArgumentOutOfRangeException("lockID");
                if (null == attachmentRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {
                    // Convert string to guid
                    Guid codeGuid = Guid.Parse(code);

                    // Find item based on ID
                    Attachment dataEntity = attachmentRepository.Single(x => x.Code == codeGuid, "AttachmentData");
                    List<AttachmentData> dataToDelete = new List<AttachmentData>(dataEntity.AttachmentData);
                    //Set the row identifier to be the one that i'm trying to delete. Therefore E.F. will error if this has changed since
                    //dataEntity.RowIdentifier = lockID;

                    foreach (AttachmentData currentData in dataToDelete)
                    {
                        attachmentdataRepository.Delete(currentData);
                    }
                    // Delete the item
                    attachmentRepository.Delete(dataEntity);

                    // Add to Incident history IncidentUpdateEvent                    
                    Guid userWhoUpdatedIncident = Guid.Parse(currentUser);

                    string staffName = staffRepository.Find(new Specification<Staff>(x => x.Code == userWhoUpdatedIncident)).Select(x => x.FirstName + " " + x.LastName).First();

                    IncidentUpdateEvent incidentUpdateEventItem = new IncidentUpdateEvent();
                    incidentUpdateEventItem.Code = Guid.NewGuid();
                    incidentUpdateEventItem.DateTime = DateTime.Now;
                    incidentUpdateEventItem.IncidentCode = dataEntity.IncidentCode;
                    incidentUpdateEventItem.Type = "Update";
                    incidentUpdateEventItem.UpdateBy = staffName;

                    //IncidentUpdateEvent
                    List<IncidentUpdateEvent> incidentUpdateEventList = incidentUpdateEventRepository.Find(new Specification<IncidentUpdateEvent>(x => x.IncidentCode == dataEntity.IncidentCode && x.Type == "Update")).ToList();
                    incidentUpdateEventList = incidentUpdateEventList.OrderBy(x => x.DateTime).ToList();
                    //Only store last 5 update events (list is ordered by date time (asc) so the elementAt 0 will be the oldest
                    if (incidentUpdateEventList.Count == 5)
                        incidentUpdateEventRepository.Delete(incidentUpdateEventList.ElementAt(0));

                    incidentUpdateEventRepository.Add(incidentUpdateEventItem);


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
    
    
    }
}