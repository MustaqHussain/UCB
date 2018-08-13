using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using Dwp.Adep.Ucb.ResourceLibrary;
using AutoMapper;
using UcbWeb.Helpers;
using UcbWeb.ViewModels;
using UcbWeb.Models;
using UcbWeb.UcbService;
using System.Web;
using UcbWeb.BinaryDataTransferService;
using UcbWeb.DirectoryService;
using UcbWeb.SmtpEmailService;
using System.Text;
using System.Configuration;
using System.IO;
using UcbWeb.ReportExecution2005;
using System.Net;
using UcbWeb.Constants;
using UCBWeb.Exceptions;

namespace UcbWeb.Controllers
{
    public partial class IncidentController : BaseController
    {
        private IUcbService UcbService;

        // Dependency Injection enabled constructors
        public IncidentController()
            : this(new UcbServiceClient(), new SessionManager(), new CacheManager())
        {
        }

        public IncidentController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
            : base(sessionManager, cacheManager)
        {
            this.UcbService = UcbService;
        }

        #region Create

        /*
        The Creates are separate actions  because the incident create can be done from the intranet by anyone, whereas the referral create must be
        a UCB-ADMIN, UCB-NOMINATED-MANAGER, UCB-DEPUTY-NOMINATED-MANAGER, UCB-BUSINESS-AREA-MANAGER user. The actions therefore have different 
        custom authorise attributes.
        */
        public ActionResult Create(string Code)
        {
            ClearSessionObjects();

            sessionManager.IncidentCode = null;
            //sessionManager.IncidentType = IncidentType.Incident;
            return Edit(IncidentType.Incident);
            //return RedirectToAction("Edit", "Incident");             
        }

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        public ActionResult CreateReferral(string Code)
        {

            ClearSessionObjects();

            sessionManager.IncidentCode = null;
            //sessionManager.IncidentType = IncidentType.Referral;
            return Edit(IncidentType.Referral);
            //return RedirectToAction("Edit", "Incident");             
        }
        #endregion

        #region Edit

        public ActionResult EditWithError(string msg)
        {
            sessionManager.ErrorMessage = msg;
            return Edit();
        }

        // GET: /Incident/Edit
        public ActionResult Edit(string incidentType = null)
        {
            // Retrieve ID from session
            string code = sessionManager.IncidentCode;

            if (sessionManager.IsLinkedIncidentReadOnly != null && sessionManager.IsLinkedIncidentReadOnly == true)
            {
                code = sessionManager.LinkedIncidentReadOnlyIncidentCode;
            }


            IncidentVM model = new IncidentVM { ShowIncidentDetail = false, ShowAbuseType = false };
            model.PreviewIncident = false;
            // Not from staff or error
            if (String.IsNullOrEmpty(code))
            {

                // Check if sole session
                if (sessionManager.CurrentIncident != null)
                {
                    // The user should not be able to access the incident. Redirect them to confirmation page indicating this.
                    ClearSessionObjects();
                    return View("MultipleSessionsError");
                }
                //Assume we are in create mode as no code passed
                ClearSessionObjects();

                //If session/cache has lists then use them
                RepopulateListsFromCacheSession(model);

                sessionManager.IncidentType = incidentType;

                model.IncidentItem = new IncidentModel { IncidentStatus = IncidentStatus.Creation.ToString(), IncidentDate = DateTime.Now, IsOthersPresent = false };
                if (sessionManager.IncidentType == IncidentType.Referral)
                {
                    model.IncidentItem.Type = IncidentType.Referral;
                    model.IncidentItem.ReferralReviewDate = DateTime.Now.AddMonths(12);
                    model.RepeatBehaviourAttachmentList = new List<AttachmentModel>();
                    model.GeneralEvidenceAttachmentList = new List<AttachmentModel>();
                    model.FurtherInfoAttachmentList = new List<AttachmentModel>();
                    model.ReferralEvidenceAttachmentList = new List<AttachmentModel>();
                }
                else
                {
                    model.IncidentItem.Type = IncidentType.Incident;

                    // If session value for IncidentType is null then default to incident
                    if (null == sessionManager.IncidentType) sessionManager.IncidentType = IncidentType.Incident;
                }
                model.CustomerItem = new CustomerModel { IsCustomerReported = true }; //Defaults the radio button to customer, rather than other
                // gary
                model.CurrentOwnerItem = null;
                model.IncidentNarrativeItem = new NarrativeModel { };
                model.LineManagerEmailAddressList = new List<string>();

                //For a new incident, clear the drop downs that rely on a selection in a previous drop down
                model.StaffMemberBusinessAreaList = new List<OrganisationModel>();
                model.StaffMemberHomeOfficeList = new List<SiteModel>();
                model.IncidentTypeList = new List<IncidentTypeModel>();
                model.LinkedIncidents = new List<IncidentLinkModel>();
                model.IncidentUpdateEvents = new List<IncidentUpdateEventModel>();
            }
            //if we have been passed a code then assume we are in edit situation and we need to retrieve from the database.
            else
            {
                // Create service instance
                UcbServiceClient sc = new UcbServiceClient();

                try
                {
                    // Get users localisation
                    LanguageManager language = new LanguageManager(sessionManager);
                    string locale = language.GetLocale();

                    // Call service to get Incident item and any associated lookups    
                    IncidentVMDC returnedObject = sc.GetIncident(CurrentUser, CurrentUser, appID, "", code, locale);

                    // Close service communication
                    sc.Close();

                    if (sessionManager.IsLinkedIncidentReadOnly != null && sessionManager.IsLinkedIncidentReadOnly == true)
                    {
                        // From linked incident - Preview/ReadOnly so must not change main incident's session variables 

                        // Main/Original service version
                        IncidentModel fromIncident = sessionManager.IncidentServiceVersion.Clone();
                        CustomerModel fromCustomer = sessionManager.CustomerServiceVersion.Clone();

                        //Get view model from service of linked Incident
                        model = ConvertIncidentDC(returnedObject);

                        //Store the linked incident's service version for RepopulateListsFromCacheSession
                        sessionManager.IncidentServiceVersion = model.IncidentItem.Clone();
                        sessionManager.CustomerServiceVersion = model.CustomerItem.Clone();

                        RepopulateListsFromCacheSession(model);
                        ResolveFieldCodesToFieldNamesUsingLists(model);

                        //Re-store the main incident's service version
                        sessionManager.IncidentServiceVersion = fromIncident.Clone();
                        sessionManager.CustomerServiceVersion = fromCustomer.Clone();

                        // Return the Preview
                        return ReturnView(model);
                    }

                    //Get view model from service
                    model = ConvertIncidentDC(returnedObject);

                    if (sessionManager.IsLinkingIncident != null && (bool)sessionManager.IsLinkingIncident == true &&
                        sessionManager.LinkedIncidents != null && sessionManager.IncidentServiceVersion != null &&
                        sessionManager.IncidentServiceVersion.Code == model.IncidentItem.Code)
                    {
                        if (sessionManager.LinkedIncidents.Count() < model.LinkedIncidents.Count())
                        {

                            int numberOfNewlyLinked = 0;
                            foreach (IncidentLinkModel iL in model.LinkedIncidents)
                            {
                                IncidentLinkModel linkToIncident = sessionManager.LinkedIncidents.Find(x => x.Code == iL.Code);
                                if (linkToIncident == null)
                                {
                                    numberOfNewlyLinked++;
                                    iL.Message = "Newly Linked";
                                }
                            }

                            string message = "Number of newly linked Incidents: " + numberOfNewlyLinked;
                            model.Message = message;
                        }
                        else if (sessionManager.LinkedIncidents.Count() == model.LinkedIncidents.Count())
                        {
                            string message = "No new linked Incidents";
                            model.Message = message;
                        }
                    }
                    sessionManager.IsLinkingIncident = null;

                    //Store the service version
                    sessionManager.IncidentServiceVersion = model.IncidentItem.Clone();
                    sessionManager.CustomerServiceVersion = model.CustomerItem.Clone();
                    sessionManager.IncidentNarrativeServiceVersion = model.IncidentNarrativeItem.Clone();
                    sessionManager.LineManagerNarrativeServiceVersion = model.LineManagerNarrativeItem.Clone();
                    sessionManager.FurtherInfoNarrativeServiceVersion = model.FurtherInfoNarrativeItem.Clone();
                    sessionManager.DeficienciesNarrativeServiceVersion = model.DeficienciesNarrativeItem.Clone();
                    sessionManager.ReviewActionNarrativeServiceVersion = model.ReviewActionNarrativeItem.Clone();

                    //gary
                    sessionManager.CurrentOwner = model.CurrentOwnerItem.Clone();

                    RepopulateListsFromCacheSession(model);
                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    // If the incident is no longer at "New" status i.e. has been submitted and the user has come in via an email link then
                    // this user should not be able to access the incident
                    if (null != sessionManager.IsLinkFromEmail && sessionManager.IsLinkFromEmail == true)
                    {
                        if (model.IncidentItem.IncidentStatus != IncidentStatus.New)
                        {
                            // Create service instance
                            UcbServiceClient ucbsc = new UcbServiceClient();
                            StaffDC nominatedManager = null;

                            //Get nominated manager for site
                            try
                            {
                                //Get staff member for the site
                                nominatedManager = ucbsc.GetNominatedManagerForSite(CurrentUser, CurrentUser, appID, "", model.IncidentItem.StaffMemberHomeOfficeSiteCode.ToString());
                                ucbsc.Close();
                            }
                            catch (Exception e)
                            {
                                // Handle the exception
                                string message = ExceptionManager.HandleException(e, sc);
                                model.Message = message;

                                return View(model);
                            }

                            StaffModel nominatedManagerItem = Mapper.Map<StaffDC, StaffModel>(nominatedManager);
                            string nominatedManagerStaffNumber = nominatedManagerItem.StaffNumber;
                            ADUser nominatedManagerFromAD = new ADUser();

                            // Create service instance
                            DirectoryServiceClient dsc = new DirectoryServiceClient();

                            //Now read LDAP to get cc and from email addresses
                            try
                            {
                                nominatedManagerFromAD = dsc.FindEmail(nominatedManagerStaffNumber);
                                dsc.Close();
                            }
                            catch (Exception e)
                            {
                                // Handle the exception
                                string message = ExceptionManager.HandleException(e, dsc);
                                model.Message = message;

                                return View(model);
                            }

                            // Save incident and nominated manager details to session for confirmation screen
                            sessionManager.CurrentIncident = model.IncidentItem;
                            sessionManager.NominatedManager = nominatedManagerItem.FirstName + " " + nominatedManagerItem.LastName;
                            sessionManager.NominatedManagerEmailAddress = nominatedManagerFromAD.Email;

                            // The user should not be able to access the incident. Redirect them to confirmation page indicating this.
                            return RedirectToAction("LineManagerCompletedActionConfirmation", "Confirmation");
                        }
                        else
                        {
                            // The incident is at New status and the user has accessed the system via the email link then the Incident should be updateable
                            model.IsReadOnly = false;
                        }
                    }

                    // gary
                    //if (model.IncidentItem.n
                    model.HandleCaseFlag = model.CurrentOwnerItem != null && (string.Compare(model.CurrentOwnerItem.Code.ToString(), CurrentUser, true) == 0);


                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return ReturnView(model);
                }
            }
            //Adds current retrieved Incident and related details to session
            sessionManager.CurrentIncident = model.IncidentItem;
            sessionManager.CurrentCustomer = model.CustomerItem;
            sessionManager.CurrentIncidentNarrative = model.IncidentNarrativeItem;
            sessionManager.CurrentLineManagerNarrative = model.LineManagerNarrativeItem;
            sessionManager.CurrentFurtherInfoNarrative = model.FurtherInfoNarrativeItem;
            sessionManager.CurrentDeficienciesNarrative = model.DeficienciesNarrativeItem;
            sessionManager.CurrentReviewActionNarrative = model.ReviewActionNarrativeItem;

            sessionManager.CurrentRIDDORAttachmentList = model.RIDDORAttachmentList;
            sessionManager.CurrentFastTrackAttachmentList = model.FastTrackAttachmentList;
            sessionManager.CurrentRepeatBehaviourAttachmentList = model.RepeatBehaviourAttachmentList;
            sessionManager.CurrentGeneralEvidenceAttachmentList = model.GeneralEvidenceAttachmentList;
            sessionManager.CurrentFurtherInfoAttachmentList = model.FurtherInfoAttachmentList;
            sessionManager.CurrentReferralEvidenceAttachmentList = model.ReferralEvidenceAttachmentList;

            sessionManager.NominatedManager = model.NominatedManager;
            sessionManager.DeputyNominatedManagers = model.DeputyNominatedManagers;

            sessionManager.LinkedIncidents = model.LinkedIncidents;

            sessionManager.CurrentIncidentUpdateEventList = model.IncidentUpdateEvents;

            sessionManager.ShowAbuseType = model.ShowAbuseType;
            sessionManager.ShowIncidentDetail = model.ShowIncidentDetail;

            sessionManager.IncidentType = model.IncidentItem.Type;

            SetAccessContext(model);
            SetIncidentContext(model); //this is where fields are cleared and removed from model state

            //handle file size error or A potentially dangerous Request.Form
            if (!string.IsNullOrEmpty(sessionManager.ErrorMessage))
            {
                if (sessionManager.ErrorMessage.StartsWith("A potentially dangerous Request.Form value was detected from the client"))
                {
                    model.Message = sessionManager.ErrorMessage.Replace("LESSTHANHACK", "<");
                }
                else
                {
                    model.Message = Resources.INCIDENT_FILE_ATTACHMENT_SIZE;
                }
                sessionManager.ErrorMessage = null;
            }

            if (sessionManager.MessageFromPageFrom == Resources.MESSAGE_UPDATE_SUCCEEDED)
            {
                model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
                sessionManager.MessageFromPageFrom = null;
            }
            //update model with copy information
            model.Copy = sessionManager.Copy;

            return ReturnView(model);
        }

        private ActionResult ReturnView(IncidentVM model)
        {
            // Checked first if from LinkedIncident
            if (sessionManager.IsLinkedIncidentReadOnly != null && (bool)sessionManager.IsLinkedIncidentReadOnly)
            {
                model.PreviewIncident = true;
                sessionManager.IsLinkedIncidentReadOnly = null;
                if (model.IncidentItem.Type == IncidentType.Incident)
                {
                    return PartialView("ReadOnly", model);
                }
                else
                {
                    return PartialView("ReadOnlyReferral", model);
                }
            }

            if (model.IncidentItem.Type == IncidentType.Incident || sessionManager.IncidentType == IncidentType.Incident)
            {
                if (model.IsReadOnly && model.IncidentContext != IncidentContext.FromLink)
                {
                    return View("ReadOnly", model);
                }
                else
                {
                    return View("Edit", model);
                }
            }
            else
            {
                if (model.IsReadOnly)
                {
                    return View("ReadOnlyReferral", model);
                }
                else
                {
                    return View("EditReferral", model);
                }
            }
        }

        // GET: /Incident/Edit
        //This is when the user clicks on the link in the email
        public ActionResult FindIncident(string Code)
        {
            sessionManager.IsLinkedIncidentReadOnly = Request.QueryString["ReadOnly"] == null ? false : true;
            if (sessionManager.IsLinkedIncidentReadOnly == true)
            {
                sessionManager.LinkedIncidentReadOnlyIncidentCode = Code;
            }
            else
            {
                sessionManager.IncidentCode = Code;
            }

            string SafeID = Request.QueryString["ID"];

            var ID = SafeBase64UrlEncoder.DecodeBase64Url(SafeID);

            if (!string.IsNullOrEmpty(ID))
            {
                var UnEncryptedIncidentCode = Encryptor.Crypt(ID, "UCBEncrypt", false);
                if (UnEncryptedIncidentCode == Code.ToString())
                {
                    // Acknowledge that this method was called as a result of a click on an email link (which contains QueryString param)
                    // This is in no way guaranteed
                    sessionManager.IsLinkFromEmail = true;
                }
                else
                {
                    sessionManager.IsLinkFromEmail = false;
                }
            }
            else
            {
                sessionManager.IsLinkFromEmail = false;
            }


            // Check authorised user
            if (sessionManager.IsLinkFromEmail == false && !User.IsInRole(AppRoles.APPLICATION))
            {
                Response.Redirect("~/Home/UnAuthorized");
            }

            return RedirectToAction("Edit", "Incident");
        }

        #endregion

        #region Linked Incident

        // GET: /Incident/Delete
        //This is when the user clicks delete Link Incident link
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteLinkIncident(string Code)
        {

            foreach (string Key in Request.Form.Keys)
            {
                // Test if Select button was clicked...
                if (Key.StartsWith("Edit::DeleteLinkIncident?Code="))
                {
                    // Retrieve ID for entity which was selected
                    Code = Key.Substring(30);
                    break;
                }
            }


            //couldn't figure out how to pass two parameters
            //hence passing both guid seperated by ';'
            var model = GetUpdatedModel();
            UcbServiceClient ucb = new UcbServiceClient();
            try
            {
                //var linkedIncidents = sessionManager.LinkedIncidents;
                //IncidentLinkModel ilm = linkedIncidents.FirstOrDefault(x => x.Code.Equals(Guid.Parse(Code)));
                string code = Code.Split(';')[0];
                string rowId = Code.Split(';')[1];
                ucb.DeleteIncidentLink(CurrentUser, CurrentUser, appID, "", code, rowId);

            }
            catch (Exception e)
            {
                string message = ExceptionManager.HandleException(e, ucb);
            }
            finally
            {
                ucb.Close();
            }
            ModelState.Clear();
            sessionManager.MessageFromPageFrom = Resources.MESSAGE_UPDATE_SUCCEEDED;
            return RedirectToAction("Edit", "Incident");

        }


        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult LinkedViaNino(FormCollection collection)
        {
            var model = GetUpdatedModel();
            string linkingParameter = Request.Form["LinkingParameter"];
            UcbServiceClient ucb = new UcbServiceClient();
            try
            {
                switch (collection.Get("linkType"))
                {
                    case "NINO":
                        ucb.LinkIncidentsByNino(CurrentUser, CurrentUser, appID, "", model.IncidentItem.Code.ToString(), linkingParameter.ToUpper().Trim());
                        break;
                    case "IncidentID":
                        ucb.LinkIncidentsByIncidentID(CurrentUser, CurrentUser, appID, "", model.IncidentItem.Code.ToString(), int.Parse(linkingParameter.ToUpper()));
                        break;
                }
            }
            catch (Exception e)
            {
                string message = ExceptionManager.HandleException(e, ucb);
                model.Message = message;

                return View(model);
            }
            finally
            {
                ucb.Close();
            }
            ModelState.Clear();
            sessionManager.IsLinkingIncident = true;
            return RedirectToAction("Edit", "Incident");
        }


        #endregion

        #region Create/Update

        // POST: /Incident/Edit with Submit button submitting
        //This is called by initital create and the line manager update
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SubmitIncident(FormCollection collection)
        {

            // Check if sole session
            if (RedirectIfDifferenctSession(collection) != null) return View("MultipleSessionsError");

            // Get the updated model
            var model = GetUpdatedModel();

            var errors = ModelState
                   .Where(x => x.Value.Errors.Count > 0)
                   .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                   .ToArray();

            // Test to see if the model has validated correctly
            if (ModelState.IsValid)
            {
                if (model.IsSubmitConfirmed == "True" || !model.IsViewDirty)
                {
                    //Set flags false
                    SetFlagsFalse(model);

                    model.IsSubmitConfirmed = "False";

                    // Create service instance
                    UcbServiceClient sc = new UcbServiceClient();
                    DirectoryServiceClient dsc = new DirectoryServiceClient();

                    // Map model to data contract
                    IncidentDC IncidentItem = Mapper.Map<IncidentDC>(model.IncidentItem);
                    CustomerDC CustomerItem = Mapper.Map<CustomerDC>(model.CustomerItem);
                    NarrativeDC IncidentNarrativeItem = Mapper.Map<NarrativeDC>(model.IncidentNarrativeItem);
                    NarrativeDC LineManagerNarrativeItem = Mapper.Map<NarrativeDC>(model.LineManagerNarrativeItem);

                    IncidentVMDC returnedObject = null;
                    StaffDC nominatedManager = null;

                    //Get nominated manager for site
                    try
                    {
                        //Get staff member for the site.
                        nominatedManager = sc.GetNominatedManagerForSite(CurrentUser, CurrentUser, appID, "", model.IncidentItem.StaffMemberHomeOfficeSiteCode.ToString());
                        sc.Close();
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return View(model);
                    }

                    StaffModel nominatedManagerItem = Mapper.Map<StaffDC, StaffModel>(nominatedManager);

                    //toEmail Address already populated via LineManagerEmail drop down list
                    string toEmailAddress = String.IsNullOrEmpty(model.LineManagerEmailAddress) ? null : model.LineManagerEmailAddress;
                    string fromEmailAddress = "";
                    string ccEmailAddress = "";
                    string currentUserNameFromAD = "";


                    string nominatedManagerStaffNumber = nominatedManagerItem.StaffNumber;
                    string currentUserStaffNumber = HttpContext.User.Identity.Name.Split('\\').ElementAt(1);
                    ADUser nominatedManagerFromAD = new ADUser();
                    ADUser currentUserFromAD = new ADUser();
                    //Now read LDAP to get cc and from email addresses
                    try
                    {
                        nominatedManagerFromAD = dsc.FindEmail(nominatedManagerStaffNumber);
                        currentUserFromAD = dsc.FindEmail(currentUserStaffNumber);
                        dsc.Close();
                        ccEmailAddress = nominatedManagerFromAD.Email;
                        fromEmailAddress = currentUserFromAD.Email;
                        currentUserNameFromAD = currentUserFromAD.FirstName + " " + currentUserFromAD.LastName;
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, dsc);
                        model.Message = message;

                        return View(model);
                    }

                    sc = new UcbServiceClient();
                    try
                    {
                        //If no guid, then this is the initial create of an incident. Otherwise, it is the line managers update.
                        if (null == model.IncidentItem.Code || model.IncidentItem.Code == Guid.Empty)
                        {
                            // Call service to create new Incident item
                            returnedObject = sc.CreateIncident(CurrentUser, CurrentUser, appID, "", currentUserNameFromAD, IncidentItem, CustomerItem, IncidentNarrativeItem);
                        }
                        else
                        {
                            // Call service to update Incident item
                            returnedObject = sc.LineManagerUpdateIncident(CurrentUser, CurrentUser, appID, "", currentUserNameFromAD, IncidentItem, CustomerItem, IncidentNarrativeItem, LineManagerNarrativeItem);
                        }

                        // Close service communication
                        sc.Close();
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return View(model);
                    }

                    // Retrieve item returned by service
                    var createdIncident = returnedObject.IncidentItem;
                    var createdCustomer = returnedObject.CustomerItem;
                    var createdIncidentNarrative = returnedObject.IncidentNarrativeItem;
                    var createdLineManagerNarrative = returnedObject.LineManagerNarrativeItem;


                    /*No need to do this as does not return to the same page after update*/
                    // Map data contract to model
                    model.IncidentItem = Mapper.Map<IncidentModel>(createdIncident);
                    model.CustomerItem = Mapper.Map<CustomerModel>(createdCustomer);
                    model.IncidentNarrativeItem = Mapper.Map<NarrativeModel>(createdIncidentNarrative);
                    model.LineManagerNarrativeItem = Mapper.Map<NarrativeModel>(LineManagerNarrativeItem);

                    //After creation some of the fields are display only so we need the resolved look up mames
                    ResolveFieldCodesToFieldNamesUsingLists(model);

                    // Set access context to Edit mode
                    model.AccessContext = IncidentAccessContext.Edit;

                    // Save version of item returned by service into session
                    sessionManager.IncidentServiceVersion = model.IncidentItem.Clone();
                    sessionManager.CurrentIncident = model.IncidentItem;

                    sessionManager.CustomerServiceVersion = model.CustomerItem.Clone();
                    sessionManager.CurrentCustomer = model.CustomerItem;

                    sessionManager.IncidentNarrativeServiceVersion = model.IncidentNarrativeItem.Clone();
                    sessionManager.CurrentIncidentNarrative = model.IncidentNarrativeItem;

                    sessionManager.LineManagerNarrativeServiceVersion = model.LineManagerNarrativeItem.Clone();
                    sessionManager.CurrentLineManagerNarrative = model.LineManagerNarrativeItem;

                    sessionManager.FurtherInfoNarrativeServiceVersion = model.FurtherInfoNarrativeItem.Clone();
                    sessionManager.CurrentFurtherInfoNarrative = model.FurtherInfoNarrativeItem;

                    sessionManager.DeficienciesNarrativeServiceVersion = model.DeficienciesNarrativeItem.Clone();
                    sessionManager.CurrentDeficienciesNarrative = model.DeficienciesNarrativeItem;

                    sessionManager.ReviewActionNarrativeServiceVersion = model.ReviewActionNarrativeItem.Clone();
                    sessionManager.CurrentReviewActionNarrative = model.ReviewActionNarrativeItem;

                    sessionManager.ShowIncidentDetail = model.ShowIncidentDetail;
                    sessionManager.ShowAbuseType = model.ShowAbuseType;

                    sessionManager.NominatedManager = nominatedManagerItem.FirstName + " " + nominatedManagerItem.LastName;
                    sessionManager.NominatedManagerEmailAddress = ccEmailAddress;

                    sessionManager.FromEmailAddress = fromEmailAddress;


                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
                    // the POSTED values and not the changed ones.
                    ModelState.Clear();

                    switch (model.IncidentItem.IncidentStatus)
                    {
                        //Incident just created
                        case IncidentStatus.New:
                            //return RedirectToAction("CreateIncidentConfirmation", "Confirmation");
                            return RedirectToAction("SendEmail", "Email");
                        //Line manager has updated and submitted incident
                        case IncidentStatus.Submitted:
                            return RedirectToAction("LineManagerUpdateConfirmation", "Confirmation");
                    }

                }
                else
                {
                    //Set flags false
                    SetFlagsFalse(model);
                    model.Message = Resources.MESSAGE_SUBMITCONFIRMATION;
                    model.IsSubmitConfirmed = "True";
                }
            }


            return View(model);
        }

        // POST: /Incident/Edit with Save button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveIncident(FormCollection collection)
        {
            return UpdateIncident(collection, null);
        }

        // POST: /Incident/Edit with Save and Close button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveAndCloseIncident(FormCollection collection)
        {
            return UpdateIncident(collection, null);
        }

        // POST: /Incident/Edit with Save button submitting
        //[CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult PrintIncident(FormCollection collection)
        {
            // Get the updated model
            var model = GetUpdatedModel();


            byte[] Report = RenderIncidentReportAsPdf(model.IncidentItem.Code.ToString(), "IncidentReport");
            FileContentResult ReportResult = new FileContentResult(Report, "application/pdf");
            ReportResult.FileDownloadName = "IncidentReport_" + model.IncidentItem.IncidentID.ToString() + ".pdf";
            return ReportResult;

        }
        private byte[] RenderIncidentReportAsPdf(string incidentCode, string reportName)
        {
            // Byte array to store the pdf version of the print.
            byte[] Report;

            Hashtable Parameters = new Hashtable();
            Parameters.Add("UserId", sessionManager.UserID);
            Parameters.Add("Code", incidentCode);
            string ReportDirectory = ConfigurationManager.AppSettings["ReportLocation"];
            //Report = RenderPdfPrint(@"/Dwp.Adep.Applications.OperationalReports/" + reportName, Parameters);
            Report = RenderPdfPrint(ReportDirectory + reportName, Parameters);

            return Report;
        }
        #endregion

        #region Report Execution - RenderPdfPrint

        private static byte[] RenderPdfPrint(string reportPath, Hashtable parameters)
        {
            //Set the format to pdf
            string Format = "PDF";
            //Set the divice info to render all pages
            string PdfDeviceInfo = @"<DeviceInfo><StartPage>0</StartPage></DeviceInfo>";
            string ExtensionOut = "";
            string MimeTypeOut = "";
            string EncodingOut = "";

            UcbWeb.ReportExecution2005.Warning[] WarningsOut = null;
            string[] StreamIDsOut = null;

            UcbWeb.ReportExecution2005.ExecutionHeader ReportExecutionHeader = new UcbWeb.ReportExecution2005.ExecutionHeader();

            //Create the report execution item
            UcbWeb.ReportExecution2005.ReportExecutionService ReportExecutionItem = new UcbWeb.ReportExecution2005.ReportExecutionService();

            //set the credentials
            string UserReportID = ConfigurationManager.AppSettings["userReportID"];
            string UserReportPassword = ConfigurationManager.AppSettings["userReportPassword"];
            string UserReportDomain = ConfigurationManager.AppSettings["userReportDomain"];

            ReportExecutionItem.Credentials = new NetworkCredential(UserReportID, UserReportPassword, UserReportDomain);

            ReportExecutionItem.ExecutionHeaderValue = ReportExecutionHeader;

            ExecutionInfo Information = new ExecutionInfo();

            //load the report
            ReportExecutionItem.LoadReport(reportPath, null);

            //Set the parametres for the report
            ReportExecution2005.ParameterValue[] ReportParameters = new ReportExecution2005.ParameterValue[parameters.Count];
            int i = 0;
            foreach (string Key in parameters.Keys)
            {
                ReportParameters[i] = new ReportExecution2005.ParameterValue();
                ReportParameters[i].Name = Key;
                ReportParameters[i].Value = (string)parameters[Key];
                i++;
            }
            Information = ReportExecutionItem.SetExecutionParameters(ReportParameters, "en-GB");

            string SessionId = ReportExecutionItem.ExecutionHeaderValue.ExecutionID;

            //render the report to a pdf file
            byte[] ReportFile = ReportExecutionItem.Render(Format, PdfDeviceInfo, out ExtensionOut, out MimeTypeOut, out EncodingOut, out WarningsOut, out StreamIDsOut);

            Information = ReportExecutionItem.GetExecutionInfo();

            return ReportFile;
        }
        #endregion

        #region Nominated Manager Update

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ArchiveIncident(FormCollection collection)
        {
            return UpdateIncident(collection, IncidentStatus.Archived);
        }

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult PublishIncident(FormCollection collection)
        {
            return UpdateIncident(collection, IncidentStatus.Live);
        }

        private ActionResult UpdateIncident(FormCollection collection, string newIncidentState)
        {
            // Check if sole session
            if (RedirectIfDifferenctSession(collection) != null) return View("MultipleSessionsError");

            // Get the updated model
            var model = new IncidentVM();
            if (newIncidentState == null)
            {
                model = GetUpdatedModel("NoChange");
            }
            else
            {
                model = GetUpdatedModel(newIncidentState);
            }

            // Test to see if there are any errors
            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToArray();

            // make sure the otehr location is present and remove if not the other type
            ProcessOtherLocation(model);


            // Test to see if the model has validated correctly
            if (ModelState.IsValid)
            {
                //
                if ((newIncidentState == IncidentStatus.Archived && model.IsArchiveConfirmed == "True")
                    || (newIncidentState == IncidentStatus.Live && model.IsPublishConfirmed == "True")
                    || ((newIncidentState == null || newIncidentState == IncidentStatus.New) && (model.IsSaveConfirmed == "True" || model.IsSaveAndCloseConfirmed == "True"))
                    || (((newIncidentState == null || newIncidentState == IncidentStatus.New) && model.IsSaveConfirmed == "False") && !model.IsViewDirty))
                {
                    //Set flags false
                    SetFlagsFalse(model);

                    if (model.StatusChangeTo != model.IncidentItem.IncidentStatus && model.StatusChangeTo != null && model.StatusChangeTo != "")
                    {
                        model.IncidentItem.IncidentStatus = model.StatusChangeTo;
                    }

                    if (model.HandleCaseFlag)
                    {
                        // if the current user has said they want to hanle the case then set them as the current owner
                        model.IncidentItem.CurrentOwnerStaffCode = new Guid(CurrentUser);
                    }
                    else
                    {
                        // if the handle case flag is not checked it might be because the user just doesn't own it (don't do anything as might be owned by someone else),
                        //... otherwise person who is handling the case has decided they do not want to own it any longer - so remove the handle case owner..
                        if (model.CurrentOwnerItem != null && (string.Compare(model.CurrentOwnerItem.Code.ToString(), CurrentUser, true) == 0))
                        {
                            model.IncidentItem.CurrentOwnerStaffCode = null;
                        }
                    }


                    // Map model to data contract
                    IncidentDC IncidentItem = Mapper.Map<IncidentDC>(model.IncidentItem);
                    CustomerDC CustomerItem = Mapper.Map<CustomerDC>(model.CustomerItem);
                    NarrativeDC IncidentNarrativeItem = Mapper.Map<NarrativeDC>(model.IncidentNarrativeItem);
                    NarrativeDC LineManagerNarrativeItem = Mapper.Map<NarrativeDC>(model.LineManagerNarrativeItem);
                    NarrativeDC furtherInfoNarrativeItem = Mapper.Map<NarrativeDC>(model.FurtherInfoNarrativeItem);
                    NarrativeDC deficienciesNarrativeItem = Mapper.Map<NarrativeDC>(model.DeficienciesNarrativeItem);
                    NarrativeDC reviewActionNarrativeItem = Mapper.Map<NarrativeDC>(model.ReviewActionNarrativeItem);

                    List<string> contingencyArrangementCodes = new List<string>();
                    List<string> controlMeasureCodes = new List<string>();
                    List<string> systemMarkedCodes = new List<string>();
                    List<string> interestedPartyCodes = new List<string>();

                    if (model.IncidentItem.ContingencyArrangementCodes != null)
                    {
                        contingencyArrangementCodes = model.IncidentItem.ContingencyArrangementCodes.ToList();
                    }
                    if (model.IncidentItem.ControlMeasureCodes != null)
                    {
                        controlMeasureCodes = model.IncidentItem.ControlMeasureCodes.ToList();
                    }
                    if (model.IncidentItem.SystemMarkedCodes != null)
                    {
                        systemMarkedCodes = model.IncidentItem.SystemMarkedCodes.ToList();
                    }
                    if (model.IncidentItem.InterestedPartyCodes != null)
                    {
                        interestedPartyCodes = model.IncidentItem.InterestedPartyCodes.ToList();
                    }

                    IncidentVMDC returnedObject = null;

                    // if no state specified assume no change
                    if (String.IsNullOrEmpty(newIncidentState))
                        newIncidentState = model.IncidentItem.IncidentStatus;

                    using (UcbServiceClient sc = new UcbServiceClient())
                    {
                        try
                        {

                            // Call service to update Incident item
                            returnedObject = sc.NominatedManagerUpdateIncident(CurrentUser, CurrentUser, appID, "", newIncidentState, IncidentItem, CustomerItem, IncidentNarrativeItem, LineManagerNarrativeItem, furtherInfoNarrativeItem, deficienciesNarrativeItem, reviewActionNarrativeItem, contingencyArrangementCodes.ToArray(), controlMeasureCodes.ToArray(), systemMarkedCodes.ToArray(), interestedPartyCodes.ToArray());
                            // Close service communication
                            sc.Close();
                        }
                        catch (Exception e)
                        {
                            // Handle the exception
                            string message = ExceptionManager.HandleException(e, sc);
                            model.Message = message;

                            return View(model);
                        }
                    }

                    sessionManager.MessageFromPageFrom = Resources.MESSAGE_UPDATE_SUCCEEDED;
                    model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;

                    if (model.IsSaveAndCloseConfirmed == "True")
                    {
                        //Even though we are leaving the page we ought to clear the server versions as they will have changed
                        ClearSessionObjects();

                        switch (sessionManager.PageFrom)
                        {
                            case "SearchMyNewReports":
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("MyNewReports", "Searches");
                            case "SearchMyReviews":
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("MyReviews", "Searches");
                            case "SearchMyForwardLook":
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("MyForwardLook", "Searches");
                            case "DeputySearchMyNewReports":
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("DeputyMyNewReports", "Searches");
                            case "DeputySearchMyReviews":
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("DeputyMyReviews", "Searches");

                            default:
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    //Set flags false
                    SetFlagsFalse(model);
                    switch (newIncidentState)
                    {
                        case IncidentStatus.Live:
                            model.Message = Resources.MESSAGE_PUBLISHCONFIRMATION;
                            model.IsPublishConfirmed = "True";
                            break;
                        case IncidentStatus.Archived:
                            model.Message = Resources.MESSAGE_ARCHIVECONFIRMATION;
                            model.IsArchiveConfirmed = "True";
                            break;
                        default:
                            model.Message = Resources.MESSAGE_SAVECONFIRMATION;
                            model.IsSaveConfirmed = "True";
                            break;
                    }

                }
                //return ReturnView(model);
                return RedirectToAction("Edit");
            }

            return ReturnView(model);

        }

        private void ProcessOtherLocation(IncidentVM model)
        {
            // custom cross field validation
            if (IsOtherLocationSelected(model.IncidentItem.IncidentLocationCode))
            {
                if (string.IsNullOrWhiteSpace(model.IncidentItem.OtherIncidentLocation))
                {
                    ModelState.AddModelError("IncidentItem.OtherIncidentLocation", string.Format(Resources.VAL_REQUIRED, Resources.LABEL_INCIDENT_OTHERINCIDENTLOCATION));
                }
            }
            else
            {
                // user has selected a location other than 'other' so need to remove any other location
                model.IncidentItem.OtherIncidentLocation = null;
            }
        }

        #endregion

        #region Transfer

        //Clicking the TransferToNewNM button on the incident screen
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult TransferIncident(FormCollection collection)
        {
            return RedirectToAction("Transfer", "Incident");
        }

        #endregion

        #region Exit

        // POST: /Incident/Edit with Exit button submitting
        //[CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ExitIncident(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsExitConfirmed == "True" || !model.IsViewDirty)
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsExitConfirmed = "False";

                ClearSessionObjects();

                // return to the calling page
                switch (sessionManager.PageFrom)
                {
                    case "SearchMyNewReports":
                        return RedirectToAction("MyNewReports", "Searches");

                    case "SearchMyReviews":
                        return RedirectToAction("MyReviews", "Searches");

                    case "SearchMyForwardLook":
                        return RedirectToAction("MyForwardLook", "Searches");

                    case "DeputySearchMyNewReports":
                        return RedirectToAction("DeputyMyNewReports", "Searches");

                    case "DeputySearchMyReviews":
                        return RedirectToAction("DeputyMyReviews", "Searches");

                    default:
                        return RedirectToAction("Index", "Home");
                }
                //return RedirectToAction("Search", "Incident");

            }
            else
            {
                //Set flags false
                SetFlagsFalse(model);
                model.Message = Resources.MESSAGE_EXITCONFIRMATION;
                model.IsExitConfirmed = "True";
            }

            return View(model);
        }



        #endregion

        #region Delete

        // POST: /Incident/Edit with Delete button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult DeleteIncident(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IsDeleteConfirmed == "True")
            {
                //Set flags false
                SetFlagsFalse(model);

                model.IsDeleteConfirmed = "False";

                // Create service instance
                UcbServiceClient sc = new UcbServiceClient();

                try
                {
                    // Call service to delete the item
                    sc.DeleteIncident(CurrentUser, CurrentUser, appID, "", model.IncidentItem.Code, model.IncidentItem.RowIdentifier);

                    // Close service communication
                    sc.Close();

                    // Remove the current values from session
                    ClearSessionObjects();
                    ClearModelObjects(model);

                    // Remove the state from the model as these are being populated by the controller and the HTML helpers are being populated with
                    // the POSTED values and not the changed ones.
                    ModelState.Clear();

                    // Create new item but keep any lists
                    model.IncidentItem = new IncidentModel();

                    // Set message to return to user
                    model.Message = Resources.MESSAGE_DELETE_SUCCEEDED;

                    // Set access context to Edit mode
                    model.AccessContext = IncidentAccessContext.Create;
                    sessionManager.CurrentIncident = new IncidentModel();

                    // return to the calling page
                    sessionManager.MessageFromPageFrom = Resources.MESSAGE_DELETE_SUCCEEDED;

                    switch (sessionManager.PageFrom)
                    {
                        case "SearchMyNewReports":
                            sessionManager.PageFrom = "EditIncident";
                            return RedirectToAction("MyNewReports", "Searches");
                        case "SearchMyReviews":
                            sessionManager.PageFrom = "EditIncident";
                            return RedirectToAction("MyReviews", "Searches");
                        case "SearchMyForwardLook":
                            sessionManager.PageFrom = "EditIncident";
                            return RedirectToAction("MyForwardLook", "Searches");
                        case "DeputySearchMyNewReports":
                            sessionManager.PageFrom = "EditIncident";
                            return RedirectToAction("DeputyMyNewReports", "Searches");
                        case "DeputySearchMyReviews":
                            sessionManager.PageFrom = "EditIncident";
                            return RedirectToAction("DeputyMyReviews", "Searches");
                        default:
                            sessionManager.PageFrom = "EditIncident";
                            return RedirectToAction("Index", "Home");
                    }

                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return ReturnView(model);
                }
            }
            else
            {
                //Set flags false
                SetFlagsFalse(model);
                model.Message = Resources.MESSAGE_DELETECONFIRMATION;
                model.IsDeleteConfirmed = "True";
            }

            return ReturnView(model);
        }

        #endregion

        #region New

        // POST: /Incident/Edit with New button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN)]
        [HttpParamSearch(Prefix = "Search::")]
        [HttpPost]
        public ActionResult NewIncident(FormCollection collection)
        {
            ClearSessionObjects();

            var model = GetUpdatedModel();
            //Set flags false
            SetFlagsFalse(model);

            //Clear Down Session
            sessionManager.IncidentCode = null;
            sessionManager.CurrentIncident = null;
            sessionManager.IncidentServiceVersion = null;

            //Go to the Edit Screen
            return RedirectToAction("Edit", "Incident");
        }

        #endregion

        #region Search

        // GET: /Incident/Search
        //This is called when first entering search Incident screen or when paging
        [CustomAuthorize(Roles = AppRoles.ADMIN)]
        public ActionResult Search(int page = 1)
        {
            // Create service instance
            UcbServiceClient sc = new UcbServiceClient();

            // Create model
            IncidentSearchVM model = new IncidentSearchVM();

            try
            {
                IncidentSearchVMDC response = sc.SearchIncident(CurrentUser, CurrentUser, appID, "", null, page, PageSize);

                // Close service communication
                sc.Close();

                //Map response back to view model
                model.MatchList = Mapper.Map<IEnumerable<IncidentSearchMatchDC>, List<IncidentSearchMatchModel>>(response.MatchList);

                // Set paging values
                model.TotalRows = response.RecordCount;
                model.PageSize = sessionManager.PageSize;
                model.PageNumber = page;

                // Store the page number we were on
                sessionManager.IncidentPageNumber = model.PageNumber;

                return View(model);
            }
            catch (Exception e)
            {
                // Handle the exception
                string message = ExceptionManager.HandleException(e, sc);
                model.Message = message;

                return View(model);
            }

        }
        #endregion

        #region Transfer incident
        // POST: /Incident/Transfer
        //This is called when clicking transfer button 
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamTransfer(Prefix = "Transfer::")]
        [HttpPost]
        public ActionResult TransferIncident(TransferSiteSearchVM model, int page = 1)
        {

            // Iterate through form keys
            foreach (string Key in Request.Form.Keys)
            {
                // Test if Select button was clicked...
                if (Key.StartsWith("Transfer::TransferIncident_"))
                {
                    // Retrieve the code for the site that was selected
                    string newSiteCode = Key.Substring(27);

                    //Retrieve the incident to update from session
                    IncidentDC incidentItem = Mapper.Map<IncidentDC>(sessionManager.CurrentIncident);


                    UcbServiceClient sc = new UcbServiceClient();
                    try
                    {
                        IncidentDC returnedObject = new IncidentDC();
                        // Call service to update the incident with the new site code
                        returnedObject = sc.TransferIncidentToNewNominatedManager(CurrentUser, CurrentUser, appID, "", incidentItem, newSiteCode);
                        //Close service communication
                        sc.Close();
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                    }
                    // Store ID for Edit screen
                    //sessionManager.IncidentCode = Value.ToString();

                    // Call out to Edit screen
                    return RedirectToAction("Edit", "Incident");//, new { code = Value });

                }
            }

            // Return to the Screen
            return View(model);
        }

        // POST: /Incident/Transfer
        //This is called when clicking transfer button 
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamTransfer(Prefix = "Transfer::")]
        [HttpPost]
        public ActionResult SearchSite(TransferSiteSearchVM model, int page = 1)
        {
            TransferSiteSearchCriteriaDC searchCriteria = Mapper.Map<TransferSiteSearchCriteriaDC>(model.SearchCriteria);

            using (UcbServiceClient sc = new UcbServiceClient())

                try
                {
                    // Call service to update Incident item
                    TransferSiteSearchVMDC returnedObject = sc.SearchTransferSites(CurrentUser, CurrentUser, appID, "", searchCriteria, page, PageSize);

                    //Map response back to view model
                    model.MatchList = Mapper.Map<IEnumerable<TransferSiteDC>, List<TransferSiteModel>>(returnedObject.MatchList);

                    // Set paging values
                    model.TotalRows = returnedObject.RecordCount;
                    model.PageSize = PageSize;
                    model.PageNumber = page;

                    // Store the page number we were on
                    sessionManager.SitePageNumber = model.PageNumber;
                    sessionManager.TransferSiteSearchCriteria = model.SearchCriteria;

                    return View(model);
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return View(model);
                }

        }

        //Click Cancel Button
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamTransfer(Prefix = "Transfer::")]
        [HttpPost]
        public ActionResult Cancel(FormCollection collection)
        {
            return RedirectToAction("Edit", "Incident");
        }

        //HttpGet - first entry to screen
        public ActionResult Transfer(int page = 1)
        {

            //Create model
            TransferSiteSearchVM model = new TransferSiteSearchVM();

            //Repopulate search criteria if already entered
            if (null == model.SearchCriteria && sessionManager.TransferSiteSearchCriteria != null)
            {
                model.SearchCriteria = sessionManager.TransferSiteSearchCriteria;
            }

            TransferSiteSearchCriteriaDC searchCriteria = Mapper.Map<TransferSiteSearchCriteriaDC>(model.SearchCriteria);

            using (UcbServiceClient sc = new UcbServiceClient())
            {

                try
                {
                    // Call service to update Incident item
                    TransferSiteSearchVMDC returnedObject = sc.SearchTransferSites(CurrentUser, CurrentUser, appID, "", searchCriteria, page, PageSize);

                    //Map response back to view model
                    model.MatchList = Mapper.Map<IEnumerable<TransferSiteDC>, List<TransferSiteModel>>(returnedObject.MatchList);

                    // Set paging values
                    model.TotalRows = returnedObject.RecordCount;
                    model.PageSize = PageSize;
                    model.PageNumber = page;

                    // Store the page number we were on
                    sessionManager.SitePageNumber = model.PageNumber;

                    return View(model);
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, sc);
                    model.Message = message;

                    return View(model);
                }
            }
        }

        #endregion


        #region Private methods

        private void SetFlagsFalse(IncidentVM model)
        {
            model.IsDeleteConfirmed = "False";
            model.IsExitConfirmed = "False";
            model.IsSubmitConfirmed = "False";
            model.IsSaveConfirmed = "False";
            model.IsPublishConfirmed = "False";
            model.IsArchiveConfirmed = "False";

            //Stop the binder resetting the posted values
            ModelState.Remove("IsDeleteConfirmed");
            ModelState.Remove("IsExitConfirmed");
            ModelState.Remove("IsSubmitConfirmed");
            ModelState.Remove("IsSaveConfirmed");
            ModelState.Remove("IsPublishConfirmed");
            ModelState.Remove("IsArchiveConfirmed");
        }

        //Used for read only screen
        private void ResolveFieldCodesToFieldNamesUsingLists(IncidentVM model)
        {
            var ListCache = cacheManager.IncidentListCache;

            try
            {

                model.IncidentItem.JobRoleDescription = model.IncidentItem.JobRoleCode.HasValue ? ListCache.JobRoleList.SingleOrDefault(x => x.Code == model.IncidentItem.JobRoleCode).Description : "";
                model.IncidentItem.ReferrerDescription = model.IncidentItem.ReferrerCode.HasValue ? ListCache.ReferrerList.SingleOrDefault(x => x.Code == model.IncidentItem.ReferrerCode).Description : "";
                model.IncidentItem.StaffMemberBusinessDescription = ListCache.StaffMemberBusinessList.Single(x => x.Code == model.IncidentItem.StaffMemberBusinessCode).Name;
                model.IncidentItem.StaffMemberBusinessAreaDescription = ListCache.StaffMemberBusinessAreaList.Single(x => x.Code == model.IncidentItem.StaffMemberBusinessAreaCode).Name;
            }
            catch (Exception e)
            {
                throw new ResolveFieldCodesException( e.StackTrace );
            }

            //This one isn't cached
            model.IncidentItem.StaffMemberHomeOfficeDescription = model.StaffMemberHomeOfficeList.Single(x => x.Code == model.IncidentItem.StaffMemberHomeOfficeSiteCode).SiteName;

            model.CustomerItem.RelationshipToCustomerDescription = model.CustomerItem.RelationshipToCustomerCode.HasValue ? model.RelationshipToCustomerList.Single(x => x.Code == model.CustomerItem.RelationshipToCustomerCode).Description : "";

            //gary


            //string x = model.IncidentItem.CurrentOwnerStaffCode.HasValue ? model.CurrentOwnerStaffCode.Single(x => x.Code == model.IncidentItem.CurrentOwnerStaffCode).Description : "";
            model.IncidentItem.EventLeadingToIncidentDescription = model.IncidentItem.EventLeadingToIncidentCode.HasValue ? model.EventLeadingToIncidentList.Single(x => x.Code == model.IncidentItem.EventLeadingToIncidentCode).Description : "";

            model.IncidentItem.IncidentLocationDescription = model.IncidentItem.IncidentLocationCode.HasValue ? model.IncidentLocationList.Single(x => x.Code == model.IncidentItem.IncidentLocationCode).Description : "";
            model.IncidentItem.IncidentCategoryDescription = model.IncidentCategoryList.Single(x => x.Code == model.IncidentItem.IncidentCategoryCode).Description;
            model.IncidentItem.IncidentTypeDescription = model.IncidentItem.IncidentTypeCode.HasValue ? model.IncidentTypeList.Single(x => x.Code == model.IncidentItem.IncidentTypeCode).Description : "";

            model.IncidentItem.IncidentDetailsDescription = model.IncidentItem.IncidentDetailsCode.HasValue ? model.IncidentDetailsList.Single(x => x.Code == model.IncidentItem.IncidentDetailsCode).Description : "";
            model.IncidentItem.AbuseTypeDescription = model.IncidentItem.AbuseTypeCode.HasValue ? model.AbuseTypeList.Single(x => x.Code == model.IncidentItem.AbuseTypeCode).Description : "";


            if (model.IncidentItem.ContingencyArrangementCodes != null)
            {
                foreach (string contingencyArrangementCode in model.IncidentItem.ContingencyArrangementCodes)
                {
                    string description = model.ContingencyArrangementList.Single(x => x.Value == contingencyArrangementCode).Text;
                    model.IncidentItem.ContingencyArrangementDescriptions += description + ", ";
                }
            }

            if (model.IncidentItem.ControlMeasureCodes != null)
            {
                foreach (string controlMeasureCode in model.IncidentItem.ControlMeasureCodes)
                {
                    string description = model.ControlMeasureList.Single(x => x.Value == controlMeasureCode).Text;
                    model.IncidentItem.ControlMeasureDescriptions += description + ", ";
                }
            }

            if (model.IncidentItem.SystemMarkedCodes != null)
            {

                foreach (string systemMarkedCode in model.IncidentItem.SystemMarkedCodes)
                {
                    string description = model.SystemMarkedList.Single(x => x.Value == systemMarkedCode).Text;
                    model.IncidentItem.SystemMarkedDescriptions += description + ", ";
                }
            }
            if (model.IncidentItem.InterestedPartyCodes != null)
            {
                foreach (string interestedPartyCode in model.IncidentItem.InterestedPartyCodes)
                {
                    string description = model.InterestedPartyList.Single(x => x.Value == interestedPartyCode).Text;
                    model.IncidentItem.InterestedPartyDescriptions += description + ", ";
                }
            }
        }


        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private IncidentVM GetUpdatedModel(string newIncidentState = null)
        {
            IncidentVM model = new IncidentVM();

            model.Message = "";

            if (sessionManager.CurrentIncident != null)
            {
                model.IncidentItem = sessionManager.CurrentIncident;
            }
            if (sessionManager.CurrentCustomer != null)
            {
                model.CustomerItem = sessionManager.CurrentCustomer;
            }
            if (sessionManager.CurrentIncidentNarrative != null)
            {
                model.IncidentNarrativeItem = sessionManager.CurrentIncidentNarrative;
            }
            if (sessionManager.CurrentLineManagerNarrative != null)
            {
                model.LineManagerNarrativeItem = sessionManager.CurrentLineManagerNarrative;
            }
            if (sessionManager.CurrentFurtherInfoNarrative != null)
            {
                model.FurtherInfoNarrativeItem = sessionManager.CurrentFurtherInfoNarrative;
            }
            if (sessionManager.CurrentDeficienciesNarrative != null)
            {
                model.DeficienciesNarrativeItem = sessionManager.CurrentDeficienciesNarrative;
            }
            if (sessionManager.CurrentReviewActionNarrative != null)
            {
                model.ReviewActionNarrativeItem = sessionManager.CurrentReviewActionNarrative;
            }
            if (sessionManager.LineManagerEmailAddressList != null)
            {
                model.LineManagerEmailAddressList = sessionManager.LineManagerEmailAddressList;
            }
            else
            {
                model.LineManagerEmailAddressList = new List<string>();
            }
            if (sessionManager.ShowIncidentDetail != null)
            {
                model.ShowIncidentDetail = sessionManager.ShowIncidentDetail;
            }
            if (sessionManager.ShowAbuseType != null)
            {
                model.ShowAbuseType = sessionManager.ShowAbuseType;
            }
            if (sessionManager.CurrentRIDDORAttachmentList != null)
            {
                model.RIDDORAttachmentList = sessionManager.CurrentRIDDORAttachmentList;
            }
            if (sessionManager.CurrentFastTrackAttachmentList != null)
            {
                model.FastTrackAttachmentList = sessionManager.CurrentFastTrackAttachmentList;
            }
            if (sessionManager.CurrentRepeatBehaviourAttachmentList != null)
            {
                model.RepeatBehaviourAttachmentList = sessionManager.CurrentRepeatBehaviourAttachmentList;
            }
            if (sessionManager.CurrentGeneralEvidenceAttachmentList != null)
            {
                model.GeneralEvidenceAttachmentList = sessionManager.CurrentGeneralEvidenceAttachmentList;
            }
            if (sessionManager.CurrentFurtherInfoAttachmentList != null)
            {
                model.FurtherInfoAttachmentList = sessionManager.CurrentFurtherInfoAttachmentList;
            }
            if (sessionManager.CurrentReferralEvidenceAttachmentList != null)
            {
                model.ReferralEvidenceAttachmentList = sessionManager.CurrentReferralEvidenceAttachmentList;
            }
            if (sessionManager.NominatedManager != null)
            {
                model.NominatedManager = sessionManager.NominatedManager;
            }
            if (sessionManager.DeputyNominatedManagers != null)
            {
                model.DeputyNominatedManagers = sessionManager.DeputyNominatedManagers;
            }
            if (sessionManager.LinkedIncidents != null)
            {
                model.LinkedIncidents = sessionManager.LinkedIncidents;
            }

            if (sessionManager.CurrentIncidentUpdateEventList != null)
            {
                model.IncidentUpdateEvents = sessionManager.CurrentIncidentUpdateEventList;
            }
            if (sessionManager.IsLinkFromEmail.HasValue && sessionManager.IsLinkFromEmail.Value)
            {
                model.IncidentContext = IncidentContext.FromLink;
            }
            else
            {
                model.IncidentContext = IncidentContext.FromApp;
            }
            //Unchecked checkboxes are not posted. Therefore clear these down and tryupdatmodel will add in the checked selections.
            if (model.IncidentItem.SystemMarkedCodes != null)
            {
                model.IncidentItem.SystemMarkedCodes = null;
            }
            if (model.IncidentItem.ControlMeasureCodes != null)
            {
                model.IncidentItem.ControlMeasureCodes = null;
            }
            if (model.IncidentItem.ContingencyArrangementCodes != null)
            {
                model.IncidentItem.ContingencyArrangementCodes = null;
            }
            if (model.IncidentItem.InterestedPartyCodes != null)
            {
                model.IncidentItem.InterestedPartyCodes = null;
            }
            if (sessionManager.CurrentOwner != null)
            {
                model.CurrentOwnerItem = sessionManager.CurrentOwner;
            } // gary

            switch (newIncidentState)
            {
                case "NoChange":
                    model.ButtonPressed = "Save";
                    break;
                case IncidentStatus.Live:
                    model.ButtonPressed = "Publish";
                    break;
                case IncidentStatus.Archived:
                    model.ButtonPressed = "Archive";
                    break;
                default:
                    model.ButtonPressed = "";
                    break;
            }
            //***************************************NEED WHITE LIST ---- BLACK LIST ------ TO PREVENT OVERPOSTING **************************
            //string[] whiteList = new string[]{"IncidentItem.StaffMemberFirstName"};

            bool result = TryUpdateModel(model);//,whiteList);//This also validates and sets ModelState
            //*******************************************************************************************************************************

            RepopulateListsFromCacheSession(model);


            if (sessionManager.CurrentIncident != null)
            {
                //*****************************************PREVENT OVER POSTING ATTACKS******************************************************
                //Get the values for read only fields from session (SHOULD DO THIS BEFORE TRYUPDATEMODEL??)
                MergeNewValuesWithOriginal(model.IncidentItem);
                //***************************************************************************************************************************
            }

            SetAccessContext(model);
            SetIncidentContext(model);

            return model;
        }

        private IncidentVM ConvertIncidentDC(IncidentVMDC returnedObject)
        {
            IncidentVM model = new IncidentVM();

            // Map Incident Item
            model.IncidentItem = Mapper.Map<IncidentDC, IncidentModel>(returnedObject.IncidentItem);

            // Map lookup data lists
            model.JobRoleList = Mapper.Map<IEnumerable<JobRoleDC>, List<JobRoleModel>>(returnedObject.JobRoleList);
            model.ReferrerList = Mapper.Map<IEnumerable<ReferrerDC>, List<ReferrerModel>>(returnedObject.ReferrerList);
            model.StaffMemberBusinessList = Mapper.Map<IEnumerable<OrganisationDC>, List<OrganisationModel>>(returnedObject.StaffMemberBusinessList);
            model.StaffMemberBusinessAreaList = Mapper.Map<IEnumerable<OrganisationDC>, List<OrganisationModel>>(returnedObject.StaffMemberBusinessAreaList);
            model.StaffMemberHomeOfficeList = Mapper.Map<IEnumerable<SiteDC>, List<SiteModel>>(returnedObject.StaffMemberHomeOfficeList);

            model.EventLeadingToIncidentList = Mapper.Map<IEnumerable<EventLeadingToIncidentDC>, List<EventLeadingToIncidentModel>>(returnedObject.EventLeadingToIncidentList);
            model.IncidentLocationList = Mapper.Map<IEnumerable<IncidentLocationDC>, List<IncidentLocationModel>>(returnedObject.IncidentLocationList);
            model.IncidentCategoryList = Mapper.Map<IEnumerable<IncidentCategoryDC>, List<IncidentCategoryModel>>(returnedObject.IncidentCategoryList);
            model.IncidentTypeList = Mapper.Map<IEnumerable<IncidentTypeDC>, List<IncidentTypeModel>>(returnedObject.IncidentTypeList);
            model.IncidentDetailsList = Mapper.Map<IEnumerable<IncidentDetailDC>, List<IncidentDetailModel>>(returnedObject.IncidentDetailsList);
            model.AbuseTypeList = Mapper.Map<IEnumerable<AbuseTypeDC>, List<AbuseTypeModel>>(returnedObject.AbuseTypeList);

            model.IncidentNarrativeItem = Mapper.Map<NarrativeDC, NarrativeModel>(returnedObject.IncidentNarrativeItem);
            model.LineManagerNarrativeItem = Mapper.Map<NarrativeDC, NarrativeModel>(returnedObject.LineManagerNarrativeItem);
            model.FurtherInfoNarrativeItem = Mapper.Map<NarrativeDC, NarrativeModel>(returnedObject.FurtherInfoNarrativeItem);
            model.DeficienciesNarrativeItem = Mapper.Map<NarrativeDC, NarrativeModel>(returnedObject.DeficienciesNarrativeItem);
            model.ReviewActionNarrativeItem = Mapper.Map<NarrativeDC, NarrativeModel>(returnedObject.ReviewActionNarrativeItem);

            // garytodo
            model.CurrentOwnerItem = Mapper.Map<StaffDC, StaffModel>(returnedObject.CurrentOwnerItem);
            model.CustomerItem = Mapper.Map<CustomerDC, CustomerModel>(returnedObject.CustomerItem);
            model.RelationshipToCustomerList = Mapper.Map<IEnumerable<RelationshipToCustomerDC>, List<RelationshipToCustomerModel>>(returnedObject.RelationshipToCustomerList);
            model.LinkedIncidents = Mapper.Map<IEnumerable<IncidentLinkDC>, List<IncidentLinkModel>>(returnedObject.IncidentLinkList);

            model.RIDDORAttachmentList = Mapper.Map<IEnumerable<UcbWeb.UcbService.AttachmentDC>, List<AttachmentModel>>(returnedObject.RIDDORAttachmentList);
            model.FastTrackAttachmentList = Mapper.Map<IEnumerable<UcbWeb.UcbService.AttachmentDC>, List<AttachmentModel>>(returnedObject.FastTrackAttachmentList);
            model.RepeatBehaviourAttachmentList = Mapper.Map<IEnumerable<UcbWeb.UcbService.AttachmentDC>, List<AttachmentModel>>(returnedObject.RepeatBehaviourAttachmentList);
            model.GeneralEvidenceAttachmentList = Mapper.Map<IEnumerable<UcbWeb.UcbService.AttachmentDC>, List<AttachmentModel>>(returnedObject.GeneralEvidenceAttachmentList);
            model.FurtherInfoAttachmentList = Mapper.Map<IEnumerable<UcbWeb.UcbService.AttachmentDC>, List<AttachmentModel>>(returnedObject.FurtherInfoAttachmentList);
            model.ReferralEvidenceAttachmentList = Mapper.Map<IEnumerable<UcbWeb.UcbService.AttachmentDC>, List<AttachmentModel>>(returnedObject.ReferralEvidenceAttachmentList);


            model.ContingencyArrangementList = returnedObject.ContingencyArrangementList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();
            model.ControlMeasureList = returnedObject.ControlMeasureList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.ControlMeasureDescription }).ToList();
            model.IncidentItem.ContingencyArrangementCodes = returnedObject.ContingencyArrangementCodes.ToList<String>();
            model.IncidentItem.ControlMeasureCodes = returnedObject.ControlMeasureCodes.ToList<String>();

            model.SystemMarkedList = returnedObject.SystemMarkedList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();
            model.IncidentItem.SystemMarkedCodes = returnedObject.SystemMarkedCodes.ToList<String>();

            model.InterestedPartyList = returnedObject.InterestedPartyList.Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();
            model.IncidentItem.InterestedPartyCodes = returnedObject.InterestedPartyCodes.ToList<String>();

            model.ShowAbuseType = returnedObject.ShowAbuseType;
            model.ShowIncidentDetail = returnedObject.ShowIncidentDetail;

            model.NominatedManager = returnedObject.NominatedManager;
            model.DeputyNominatedManagers = returnedObject.DeputyNominatedManagers;

            model.IncidentUpdateEvents = Mapper.Map<IEnumerable<IncidentUpdateEventDC>, List<IncidentUpdateEventModel>>(returnedObject.IncidentUpdateEvents);

            model.IsReadOnly = returnedObject.IsReadOnly;

            return model;
        }

        private void RepopulateListsFromCacheSession(IncidentVM model)
        {

            // Populate cached lists if they are empty. Will invoke service call
            IncidentLookupListsCacheObject CachedLists = cacheManager.IncidentListCache;

            //Need the service version as inactive entities that were originally used are allowed
            IncidentModel IncidentServiceVersion = sessionManager.IncidentServiceVersion;
            CustomerModel CustomerServiceVersion = sessionManager.CustomerServiceVersion;

            // Retrieve any cached lists to model

            model.JobRoleList = CachedLists.JobRoleList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.JobRoleCode)).ToList();
            model.ReferrerList = CachedLists.ReferrerList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.ReferrerCode)).ToList();
            model.StaffMemberBusinessList = CachedLists.StaffMemberBusinessList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.StaffMemberBusinessCode)).ToList();

            model.AvailableStatus = new List<string> { IncidentStatus.Submitted, IncidentStatus.New };
            //If new incident then clear down the drop downs that rely on a selection from another drop down
            if (model.IncidentItem == null)
            {
                model.StaffMemberBusinessAreaList = new List<OrganisationModel>();
                model.StaffMemberHomeOfficeList = new List<SiteModel>();
                model.IncidentTypeList = new List<IncidentTypeModel>();
            }
            else
            {

                //If business not selected, then clear business areas. Otherwise, populate business areas based on the business selection.
                if (model.IncidentItem.StaffMemberBusinessCode == null)
                {
                    model.StaffMemberBusinessAreaList = new List<OrganisationModel>();
                }
                else
                {
                    model.StaffMemberBusinessAreaList = GetBusinessAreaByBusiness(model.IncidentItem.StaffMemberBusinessCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.StaffMemberBusinessAreaCode)).ToList();
                }

                //If business area not selected, then clear home offices. Otherwise, populate home offices based on the business area selection.
                if (model.IncidentItem.StaffMemberBusinessAreaCode == null)
                {
                    model.StaffMemberHomeOfficeList = new List<SiteModel>();
                }
                else
                {
                    model.StaffMemberHomeOfficeList = GetHomeOfficesByBusinessArea(model.IncidentItem.StaffMemberBusinessAreaCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.StaffMemberHomeOfficeCode)).ToList();
                }

                //If incident category not selected, then clear incident types. Otherwise, populate incident types based on the business area selection
                if (model.IncidentItem.IncidentCategoryCode == null)
                {
                    model.IncidentTypeList = new List<IncidentTypeModel>();
                }
                else
                {
                    model.IncidentTypeList = GetIncidentTypeByCategory(model.IncidentItem.IncidentCategoryCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.IncidentTypeCode)).ToList();
                }

            }

            model.EventLeadingToIncidentList = CachedLists.EventLeadingToIncidentList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.EventLeadingToIncidentCode)).ToList();
            model.IncidentLocationList = CachedLists.IncidentLocationList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.IncidentLocationCode)).ToList();
            model.IncidentCategoryList = CachedLists.IncidentCategoryList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.IncidentCategoryCode)).ToList();
            model.IncidentDetailsList = CachedLists.IncidentDetailsList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.IncidentDetailsCode)).ToList();
            model.AbuseTypeList = CachedLists.AbuseTypeList.Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.AbuseTypeCode)).ToList();

            model.AdminEmailAddress = CachedLists.AdminEmail;
            model.DeputyAdminEmailAddress = CachedLists.DeputyAdminEmail;
            model.IncidentAdvisoryNote = CachedLists.IncidentAdvisoryNote;

            //Customer Lists
            model.RelationshipToCustomerList = CachedLists.RelationshipToCustomerList.Where(x => x.IsActive || x.Code == (null == CustomerServiceVersion ? Guid.Empty : CustomerServiceVersion.RelationshipToCustomerCode)).ToList();

            //Many to Many Lists
            model.ContingencyArrangementList = CachedLists.ContingencyArrangementBaseList
                .Where(x => x.IsActive || (null != IncidentServiceVersion ? IncidentServiceVersion.ContingencyArrangementCodes.Contains(x.Code.ToString()) : false))
                .Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();
            model.ControlMeasureList = CachedLists.ControlMeasureBaseList
                .Where(x => x.IsActive || (null != IncidentServiceVersion ? IncidentServiceVersion.ControlMeasureCodes.Contains(x.Code.ToString()) : false))
                .Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.ControlMeasureDescription }).ToList();
            model.SystemMarkedList = CachedLists.SystemMarkedBaseList
                .Where(x => x.IsActive || (null != IncidentServiceVersion ? IncidentServiceVersion.SystemMarkedCodes.Contains(x.Code.ToString()) : false))
                .Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();
            model.InterestedPartyList = CachedLists.InterestedPartyBaseList
                .Where(x => x.IsActive || (null != IncidentServiceVersion ? IncidentServiceVersion.InterestedPartyCodes.Contains(x.Code.ToString()) : false))
                .Select(x => new SelectListItem() { Value = x.Code.ToString(), Text = x.Description }).ToList();

            model.TitleList = new List<SelectListItem>();
            model.TitleList.Add(new SelectListItem { Text = "Miss", Value = "Miss" });
            model.TitleList.Add(new SelectListItem { Text = "Mr", Value = "Mr" });
            model.TitleList.Add(new SelectListItem { Text = "Mrs", Value = "Mrs" });
            model.TitleList.Add(new SelectListItem { Text = "Ms", Value = "Ms" });
            model.TitleList.Add(new SelectListItem { Text = "Dr", Value = "Dr" });
            model.TitleList.Add(new SelectListItem { Text = "Master", Value = "Master" });
            model.TitleList.Add(new SelectListItem { Text = "Professor", Value = "Professor" });
            model.TitleList.Add(new SelectListItem { Text = "Reverend", Value = "Reverend" });
            model.TitleList.Add(new SelectListItem { Text = "Sister", Value = "Sister" });
            model.TitleList.Add(new SelectListItem { Text = "Father", Value = "Father" });
            model.TitleList.Add(new SelectListItem { Text = "Lady", Value = "Lady" });
            model.TitleList.Add(new SelectListItem { Text = "Lord", Value = "Lord" });

        }

        private void MergeNewValuesWithOriginal(IncidentModel modelFromView)
        {
            //***************************The values that are display only will not be posted back so need to get them from session**************************

            IncidentModel OriginalValuesFromSession = sessionManager.CurrentIncident;

        }

        private void SetAccessContext(IncidentVM model)
        {
            //Decide on access context
            if (null == model.IncidentItem || model.IncidentItem.Code == Guid.Empty)
            {
                // Create context
                model.AccessContext = IncidentAccessContext.Create;
            }
            else
            {
                // Edit context
                model.AccessContext = IncidentAccessContext.Edit;
            }
        }

        private void SetIncidentContext(IncidentVM model)
        {
            if (sessionManager.IsLinkFromEmail.HasValue && sessionManager.IsLinkFromEmail.Value)
            {
                model.IncidentContext = IncidentContext.FromLink;
            }
            else
            {
                model.IncidentContext = IncidentContext.FromApp;
            }
            if (sessionManager.IncidentType == IncidentType.Incident)
            {
                //If a staff title != 'Other' has been selected, then clear the 'Other title'
                if (model.IncidentItem.StaffMemberTitle != "Other")
                {
                    model.IncidentItem.StaffMemberOtherTitle = null;
                    ModelState.Remove("IncidentItem.StaffMemberOtherTitle");
                }

                //If a customer is being reported, then clear out the fields related to 'Other Person'
                if (model.CustomerItem.IsCustomerReported)
                {
                    model.CustomerItem.OtherPersonTitle = null;
                    ModelState.Remove("CustomerItem.OtherPersonTitle");

                    model.CustomerItem.OtherPersonOtherTitle = null;
                    ModelState.Remove("CustomerItem.OtherPersonOtherTitle");

                    model.CustomerItem.OtherPersonFirstName = null;
                    ModelState.Remove("CustomerItem.OtherPersonFirstName");

                    model.CustomerItem.OtherPersonOtherNames = null;
                    ModelState.Remove("CustomerItem.OtherPersonOtherNames");

                    model.CustomerItem.OtherPersonLastName = null;
                    ModelState.Remove("CustomerItem.OtherPersonLastName");

                    model.CustomerItem.OtherPersonNINO = null;
                    ModelState.Remove("CustomerItem.OtherPersonNINO");

                    model.CustomerItem.RelationshipToCustomerCode = null;
                    ModelState.Remove("CustomerItem.RelationshipToCustomerCode");
                }

                //If a Customer title != 'Other' has been selected, then clear the 'Other title'
                if (model.CustomerItem.Title != "Other")
                {
                    model.CustomerItem.OtherTitle = null;
                    ModelState.Remove("CustomerItem.OtherTitle");
                }

                //If a Customer other person title != 'Other' has been selected, then clear the 'Other title'
                if (model.CustomerItem.OtherPersonTitle != "Other")
                {
                    model.CustomerItem.OtherPersonOtherTitle = null;
                    ModelState.Remove("CustomerItem.OtherPersonOtherTitle");
                }

                //If Other peope were not present, then clear out the others present field.
                if (!model.IncidentItem.IsOthersPresent)
                {
                    model.IncidentItem.OthersPresent = null;
                    ModelState.Remove("IncidentItem.OthersPresent");
                }

                //If IncidentType does not Have HasAbuseType then clear this field
                if (!model.ShowAbuseType.HasValue || !model.ShowAbuseType.Value == true)
                {
                    model.IncidentItem.AbuseTypeCode = null;
                    ModelState.Remove("IncidentItem.AbuseTypeCode");
                }
                //If IncidentType does not Have HasAbuseType then clear this field
                if (!model.ShowIncidentDetail.HasValue || !model.ShowIncidentDetail.Value == true)
                {
                    model.IncidentItem.IncidentDetailsCode = null;
                    ModelState.Remove("IncidentItem.IncidentDetailsCode");
                }

                //If Incident does not have deficiencies in process, then clear this field
                if (!model.IncidentItem.IsDeficienciesInProcess.HasValue || !model.IncidentItem.IsDeficienciesInProcess == true)
                {
                    if (model.DeficienciesNarrativeItem != null)
                    {
                        model.DeficienciesNarrativeItem.NarrativeDescription = null;
                        ModelState.Remove("DeficienciesNarrativeItem.NarrativeDescription");
                    }
                }

                //If Control measures set to no, then clear out Control measures
                if (!model.IncidentItem.IsImplementControlMeasures.HasValue || !model.IncidentItem.IsImplementControlMeasures == true)
                {
                    model.IncidentItem.ControlMeasureCodes = null;
                    ModelState.Remove("IncidentItem.ControlMeasureCodes");
                }

                //If IT System Marked set to no, then clear out IT System Marked
                if (!model.IncidentItem.IsITMarkersSet.HasValue || !model.IncidentItem.IsITMarkersSet == true)
                {
                    model.IncidentItem.SystemMarkedCodes = null;
                    ModelState.Remove("IncidentItem.SystemMarkedCodes");
                }

                //If Notified Interested Parties set to no, then clear out Notified Interested Parties
                if (!model.IncidentItem.IsNotifiedParties.HasValue || !model.IncidentItem.IsNotifiedParties == true)
                {
                    model.IncidentItem.InterestedPartyCodes = null;
                    ModelState.Remove("IncidentItem.InterestedPartyCodes");
                }

                //If Oral Warning set to no, then clear out Oral Warning Date fie;d
                if (!model.IncidentItem.IsOralWarning.HasValue || !model.IncidentItem.IsOralWarning == true)
                {
                    model.IncidentItem.OralWarningDate = null;
                    ModelState.Remove("IncidentItem.OralWarningDate");
                }

                //If Written warning set to no, then clear out Written Warning Date field
                if (!model.IncidentItem.IsWrittenWarning.HasValue || !model.IncidentItem.IsWrittenWarning == true)
                {
                    model.IncidentItem.WrittenWarningDate = null;
                    ModelState.Remove("IncidentItem.WrittenWarningDate");
                }

                //If Date interviewed set to no, then clear out Date interviewed field
                if (!model.IncidentItem.IsAssailantInterviewed.HasValue || !model.IncidentItem.IsAssailantInterviewed == true)
                {
                    model.IncidentItem.AssailantInterviewedDate = null;
                    ModelState.Remove("IncidentItem.AssailantInterviewedDate");
                }

                //If Requested Solicitor letter set to no, then clear out Solicitor letter date field
                if (!model.IncidentItem.IsSolicitorLetter.HasValue || !model.IncidentItem.IsSolicitorLetter == true)
                {
                    model.IncidentItem.SolicitorLetterDate = null;
                    ModelState.Remove("IncidentItem.SolicitorLetterDate");
                }

                //If Banning order set to no, then clear out Banning Order date field
                if (!model.IncidentItem.IsBanningOrder.HasValue || !model.IncidentItem.IsBanningOrder == true)
                {
                    model.IncidentItem.BanningOrderRequestedDate = null;
                    ModelState.Remove("IncidentItem.BanningOrderRequestedDate");
                }

                //If IT systems marked set to no, then clear out the systems marked
                if (!model.IncidentItem.IsITMarkersSet.HasValue || !model.IncidentItem.IsITMarkersSet == true)
                {
                    model.IncidentItem.SystemMarkedCodes = null;
                    ModelState.Remove("IncidentItem.SystemMarkedCodes");
                }

                //If notified parties set to no, then clear out the notified parties
                if (!model.IncidentItem.IsNotifiedParties.HasValue || !model.IncidentItem.IsNotifiedParties == true)
                {
                    model.IncidentItem.InterestedPartyCodes = null;
                    ModelState.Remove("IncidentItem.InterestedPartyCodes");
                }

                //If control measures set to no, then clear out the control measures and related sub fields
                if (!model.IncidentItem.IsImplementControlMeasures.HasValue || !model.IncidentItem.IsImplementControlMeasures == true)
                {
                    model.IncidentItem.ControlMeasureCodes = null;
                    ModelState.Remove("IncidentItem.ControlMeasureCodes");

                    model.IncidentItem.IsITMarkersSet = null;
                    ModelState.Remove("IncidentItem.IsITMarkersSet");

                    model.IncidentItem.SystemMarkedCodes = null;
                    ModelState.Remove("IncidentItem.SystemMarkedCodes");

                    model.IncidentItem.IsPapersMarked = null;
                    ModelState.Remove("IncidentItem.IsPapersMarked");

                    model.IncidentItem.IsNotifiedParties = null;
                    ModelState.Remove("IncidentItem.IsNotifiedParties");

                    model.IncidentItem.InterestedPartyCodes = null;
                    ModelState.Remove("IncidentItem.InterestedPartyCodes");

                }

                #region remove fields that do not apply to an Incident

                ModelState.Remove("IncidentItem.ReferralID");

                ModelState.Remove("IncidentItem.IncidentStatus");

                ModelState.Remove("IncidentItem.ReferrerCode");

                ModelState.Remove("IncidentItem.ReferralDate");

                ModelState.Remove("IncidentItem.ReferralIsControlMeasuresStillApply");

                ModelState.Remove("IncidentItem.ReferralIsImplementControlMeasures");

                ModelState.Remove("IncidentItem.ReferralControlMeasureCodes");

                ModelState.Remove("IncidentItem.ReferralIsITMarkersSet");

                ModelState.Remove("IncidentItem.ReferralSystemMarkedCodes");

                ModelState.Remove("IncidentItem.ReferralIsPreviousEvidenceReviewed");

                ModelState.Remove("IncidentItem.ReferralIsPreviousPartiesNotified");

                ModelState.Remove("IncidentItem.ReferralInterestedPartyCodes");

                ModelState.Remove("IncidentItem.ReferralIsRepeatBehaviour");

                ModelState.Remove("IncidentItem.ReferralNamedOfficer");

                ModelState.Remove("IncidentItem.ReferralReviewDate");

                ModelState.Remove("IncidentItem.ReferralTelephoneContactNumber");

                ModelState.Remove("ReferralNominatedManager");

                ModelState.Remove("ReferralEvidenceAttachmentList");
                #endregion
            }
            else if (sessionManager.IncidentType == IncidentType.Referral)
            {

                //If a customer is being reported, then clear out the fields related to 'Other Person'
                if (model.CustomerItem.IsCustomerReported)
                {
                    model.CustomerItem.OtherPersonTitle = null;
                    ModelState.Remove("CustomerItem.OtherPersonTitle");

                    model.CustomerItem.OtherPersonOtherTitle = null;
                    ModelState.Remove("CustomerItem.OtherPersonOtherTitle");

                    model.CustomerItem.OtherPersonFirstName = null;
                    ModelState.Remove("CustomerItem.OtherPersonFirstName");

                    model.CustomerItem.OtherPersonOtherNames = null;
                    ModelState.Remove("CustomerItem.OtherPersonOtherNames");

                    model.CustomerItem.OtherPersonLastName = null;
                    ModelState.Remove("CustomerItem.OtherPersonLastName");

                    model.CustomerItem.OtherPersonNINO = null;
                    ModelState.Remove("CustomerItem.OtherPersonNINO");

                    model.CustomerItem.RelationshipToCustomerCode = null;
                    ModelState.Remove("CustomerItem.RelationshipToCustomerCode");
                }

                //If a Customer title != 'Other' has been selected, then clear the 'Other title'
                if (model.CustomerItem.Title != "Other")
                {
                    model.CustomerItem.OtherTitle = null;
                    ModelState.Remove("CustomerItem.OtherTitle");
                }

                //If a Customer other person title != 'Other' has been selected, then clear the 'Other title'
                if (model.CustomerItem.OtherPersonTitle != "Other")
                {
                    model.CustomerItem.OtherPersonOtherTitle = null;
                    ModelState.Remove("CustomerItem.OtherPersonOtherTitle");
                }

                //If Control measures set to no, then clear out Control measures
                if (!model.IncidentItem.ReferralIsImplementControlMeasures.HasValue || !model.IncidentItem.ReferralIsImplementControlMeasures == true)
                {
                    model.IncidentItem.ReferralControlMeasureCodes = null;
                    ModelState.Remove("IncidentItem.ReferralControlMeasureCodes");
                }

                //If IT System Marked set to no, then clear out IT System Marked
                if (!model.IncidentItem.ReferralIsITMarkersSet.HasValue || !model.IncidentItem.ReferralIsITMarkersSet == true)
                {
                    model.IncidentItem.ReferralSystemMarkedCodes = null;
                    ModelState.Remove("IncidentItem.ReferralSystemMarkedCodes");
                }

                //If Notified Interested Parties set to no, then clear out Notified Interested Parties
                if (!model.IncidentItem.ReferralIsPreviousPartiesNotified.HasValue || !model.IncidentItem.ReferralIsPreviousPartiesNotified == true)
                {
                    model.IncidentItem.ReferralInterestedPartyCodes = null;
                    ModelState.Remove("IncidentItem.ReferralInterestedPartyCodes");
                }


                //If IT systems marked set to no, then clear out the systems marked
                if (!model.IncidentItem.ReferralIsITMarkersSet.HasValue || !model.IncidentItem.ReferralIsITMarkersSet == true)
                {
                    model.IncidentItem.ReferralSystemMarkedCodes = null;
                    ModelState.Remove("IncidentItem.ReferralSystemMarkedCodes");
                }


                //If control measures set to no, then clear out the control measures and related sub fields
                if (!model.IncidentItem.ReferralIsImplementControlMeasures.HasValue || !model.IncidentItem.ReferralIsImplementControlMeasures == true)
                {
                    model.IncidentItem.ReferralControlMeasureCodes = null;
                    ModelState.Remove("IncidentItem.ReferralControlMeasureCodes");

                    //model.IncidentItem.ReferralIsITMarkersSet = null;
                    //ModelState.Remove("IncidentItem.ReferralIsITMarkersSet");

                    //model.IncidentItem.ReferralSystemMarkedCodes = null;
                    //ModelState.Remove("IncidentItem.ReferralSystemMarkedCodes");

                }

                #region remove fields that do not apply to a Third Party Referral


                ModelState.Remove("IncidentItem.StaffMemberTitle");

                ModelState.Remove("IncidentItem.StaffMemberOtherTitle");

                ModelState.Remove("IncidentItem.StaffMemberFirstName");

                ModelState.Remove("IncidentItem.StaffMemberLastName");

                ModelState.Remove("IncidentItem.JobRoleCode");

                ModelState.Remove("IncidentItem.IsStaffHadAppropriateTraining");

                ModelState.Remove("IncidentItem.StaffMemberYearsInCurrentPost");

                ModelState.Remove("IncidentItem.StaffMemberMonthsInCurrentRole");

                ModelState.Remove("IncidentItem.ManagerFirstName");

                ModelState.Remove("IncidentItem.ManagerLastName");

                ModelState.Remove("LineManagerEmailAddress");

                ModelState.Remove("IncidentItem.IncidentDate");

                ModelState.Remove("IncidentItem.IncidentTime");

                ModelState.Remove("IncidentItem.IsOthersPresent");

                ModelState.Remove("IncidentItem.EventLeadingToIncidentCode");

                ModelState.Remove("IncidentItem.IncidentLocationCode");

                ModelState.Remove("IncidentItem.IncidentTypeCode");

                ModelState.Remove("IncidentItem.AbuseTypeCode");

                ModelState.Remove("IncidentItem.IncidentDetailsCode");

                ModelState.Remove("IncidentNarrativeItem.NarrativeDescription");

                ModelState.Remove("IncidentItem.IsLineManageFastTrack");

                ModelState.Remove("IncidentItem.IsLineManagerRIDDOR");

                ModelState.Remove("IncidentItem.IsPoliceCalled");

                ModelState.Remove("IncidentItem.HasLineManagerReadReport");

                ModelState.Remove("IncidentItem.TelephoneContactNumber");

                ModelState.Remove("LineManagerNarrativeItem.NarrativeDescription");

                ModelState.Remove("IncidentItem.IsNominatedFastTrack");

                ModelState.Remove("FastTrackAttachmentList");

                ModelState.Remove("IncidentItem.IsNominatedRIDDOR");

                ModelState.Remove("RIDDORAttachmentList");

                ModelState.Remove("IncidentItem.IsOralWarning");

                ModelState.Remove("IncidentItem.OralWarningDate");

                ModelState.Remove("IncidentItem.IsWrittenWarning");

                ModelState.Remove("IncidentItem.WrittenWarningDate");

                ModelState.Remove("IncidentItem.IsAssailantInterviewed");

                ModelState.Remove("IncidentItem.AssailantInterviewedDate");

                ModelState.Remove("IncidentItem.IsSolicitorLetter");

                ModelState.Remove("IncidentItem.SolicitorLetterDate");

                ModelState.Remove("IncidentItem.ContingencyArrangementCodes");

                ModelState.Remove("IncidentItem.IsDeficienciesInProcess");

                ModelState.Remove("DeficienciesNarrativeItem.NarrativeDescription");

                ModelState.Remove("IncidentItem.IsImplementControlMeasures");

                ModelState.Remove("IncidentItem.ControlMeasureCodes");

                ModelState.Remove("IncidentItem.NamedOfficer");

                ModelState.Remove("IncidentItem.BannedFromOffices");

                ModelState.Remove("IncidentItem.BannedFromOfficesEndDate");

                ModelState.Remove("IncidentItem.TelephoneContactNumber");

                ModelState.Remove("IncidentItem.IsITMarkersSet");

                ModelState.Remove("IncidentItem.SystemMarkedCodes");

                ModelState.Remove("IncidentItem.IsPapersMarked");

                ModelState.Remove("IncidentItem.IsNotifiedParties");

                ModelState.Remove("IncidentItem.InterestedPartyCodes");

                ModelState.Remove("NominatedManager");

                ModelState.Remove("IncidentItem.ReviewDate");

                ModelState.Remove("IncidentItem.CurrentOwnerStaffCode");
                #endregion
            }
            else
            {
                // Session value for Incident Type was either invalid or null
                throw new ArgumentOutOfRangeException("Invalid Incident Type value.");
            }
        }


        private void DetermineIsDirty(IncidentVM model)
        {
            //Compare the Incident to the original session
            if (model.IncidentItem.PublicInstancePropertiesEqual(sessionManager.IncidentServiceVersion, "RowIdentifier")
                || model.CustomerItem.PublicInstancePropertiesEqual(sessionManager.CustomerServiceVersion, "RowIdentifier")
                || model.IncidentNarrativeItem.PublicInstancePropertiesEqual(sessionManager.IncidentNarrativeServiceVersion, "RowIdentifier")
                || model.LineManagerNarrativeItem.PublicInstancePropertiesEqual(sessionManager.LineManagerNarrativeServiceVersion, "RowIdentifier")
                || model.FurtherInfoNarrativeItem.PublicInstancePropertiesEqual(sessionManager.FurtherInfoNarrativeServiceVersion, "RowIdentifier")
                || model.DeficienciesNarrativeItem.PublicInstancePropertiesEqual(sessionManager.DeficienciesNarrativeServiceVersion, "RowIdentifier")
                || model.ReviewActionNarrativeItem.PublicInstancePropertiesEqual(sessionManager.ReviewActionNarrativeServiceVersion, "RowIdentifier"))
            {
                model.IsViewDirty = false;
            }
            else
            {
                model.IsViewDirty = true;
            }

        }

        private void ClearSessionObjects()
        {
            //remove the current values from session
            sessionManager.CurrentIncident = null;
            sessionManager.IncidentServiceVersion = null;

            sessionManager.CurrentCustomer = null;
            sessionManager.CustomerServiceVersion = null;

            sessionManager.CurrentIncidentNarrative = null;
            sessionManager.IncidentNarrativeServiceVersion = null;

            sessionManager.CurrentLineManagerNarrative = null;
            sessionManager.LineManagerNarrativeServiceVersion = null;

            sessionManager.CurrentFurtherInfoNarrative = null;
            sessionManager.FurtherInfoNarrativeServiceVersion = null;

            sessionManager.CurrentDeficienciesNarrative = null;
            sessionManager.DeficienciesNarrativeServiceVersion = null;

            sessionManager.CurrentReviewActionNarrative = null;
            sessionManager.ReviewActionNarrativeServiceVersion = null;

            sessionManager.LineManagerEmailAddressList = null;
            sessionManager.LineManagerEmailAddress = null;
            sessionManager.EmailDetails = null;
            sessionManager.ShowIncidentDetail = false;
            sessionManager.ShowAbuseType = false;

            sessionManager.CurrentRIDDORAttachmentList = null;
            sessionManager.CurrentFastTrackAttachmentList = null;
            sessionManager.CurrentRepeatBehaviourAttachmentList = null;
            sessionManager.CurrentGeneralEvidenceAttachmentList = null;
            sessionManager.CurrentFurtherInfoAttachmentList = null;
            sessionManager.CurrentReferralEvidenceAttachmentList = null;

            sessionManager.NominatedManager = null;
            sessionManager.DeputyNominatedManagers = null;
            sessionManager.NominatedManagerEmailAddress = null;
            sessionManager.FromEmailAddress = null;

            sessionManager.CurrentIncidentUpdateEventList = null;

            sessionManager.IncidentType = null;

            sessionManager.IncidentCode = null;
            sessionManager.IsLinkFromEmail = null;

            sessionManager.MessageFromPageFrom = null;
        }

        private void ClearModelObjects(IncidentVM model)
        {
            model.IncidentItem = new IncidentModel();
            model.CustomerItem = new CustomerModel();
            model.IncidentNarrativeItem = new NarrativeModel();
            model.LineManagerNarrativeItem = new NarrativeModel();
            model.DeficienciesNarrativeItem = new NarrativeModel();
            model.ReviewActionNarrativeItem = new NarrativeModel();
            model.FurtherInfoNarrativeItem = new NarrativeModel();
        }
        #endregion

        #region Buttons for when javascript is off

        // POST: /Incident/Edit with UpdateStaffMemberTitle button submitting

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateStaffMemberTitle(FormCollection collection)
        {
            var model = GetUpdatedModel();
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateCustomerTitle(FormCollection collection)
        {
            var model = GetUpdatedModel();
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateCustomerOtherPersonTitle(FormCollection collection)
        {
            var model = GetUpdatedModel();
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateCustomerOtherPerson(FormCollection collection)
        {
            var model = GetUpdatedModel();
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateOthersPresent(FormCollection collection)
        {
            var model = GetUpdatedModel();
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateIncidentTypes(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IncidentItem.IncidentCategoryCode != null)
            {
                IncidentModel IncidentServiceVersion = sessionManager.IncidentServiceVersion;
                model.IncidentTypeList = GetIncidentTypeByCategory(model.IncidentItem.IncidentCategoryCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.IncidentTypeCode)).ToList();
            }
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ChooseIncidentType(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IncidentItem.IncidentTypeCode != null)
            {
                IncidentTypeModel currentSelectedType = GetCurrentSelectedIncidentType(model.IncidentItem.IncidentTypeCode);

                model.ShowAbuseType = (currentSelectedType.HasAbuseType ? true : false);
                model.ShowIncidentDetail = (currentSelectedType.HasDetails ? true : false);

            }
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateBusinessAreas(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IncidentItem.StaffMemberBusinessCode != null && model.IncidentItem.StaffMemberBusinessCode != Guid.Empty)
            {
                IncidentModel IncidentServiceVersion = sessionManager.IncidentServiceVersion;
                model.StaffMemberBusinessAreaList = GetBusinessAreaByBusiness(model.IncidentItem.StaffMemberBusinessCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.StaffMemberBusinessAreaCode)).ToList();
            }
            ModelState.Clear();
            return View(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult UpdateHomeOffices(FormCollection collection)
        {
            var model = GetUpdatedModel();
            if (model.IncidentItem.StaffMemberBusinessAreaCode != null)
            {
                IncidentModel IncidentServiceVersion = sessionManager.IncidentServiceVersion;
                model.StaffMemberHomeOfficeList = GetHomeOfficesByBusinessArea(model.IncidentItem.StaffMemberBusinessAreaCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.StaffMemberHomeOfficeCode)).ToList();
            }
            ModelState.Clear();
            return View(model);
        }


        #endregion

        #region Attachments

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult RIDDORFile(FormCollection collection, HttpPostedFileBase RIDDORFile)
        {
            var model = GetUpdatedModel();

            if (RIDDORFile != null)
            {
                if (RIDDORFile.ContentLength <= Int32.Parse(ConfigurationManager.AppSettings["MaxAttachmentSize"]))
                {
                    // Create service instance
                    BinaryDataTransferServiceClient sc = new BinaryDataTransferServiceClient();

                    //Attempt update
                    try
                    {
                        // Call service to create new Attachment
                        string[] messages = new string[0];
                        UcbWeb.BinaryDataTransferService.AttachmentDC Attachment = new UcbWeb.BinaryDataTransferService.AttachmentDC() { AttachmentType = "RIDDOR", IncidentCode = model.IncidentItem.Code, LoadedBy = CurrentUser, LoadedDate = DateTime.Now, Name = RIDDORFile.FileName };
                        bool returnedObject = sc.UploadAttachment(ref Attachment, CurrentUser.ToString(), RIDDORFile.InputStream, out messages);
                        //Map the BinaryTransfer service attachment object to an attachmentmodel object and add it to session
                        AttachmentModel attachment = new AttachmentModel();
                        attachment.Code = Attachment.Code;
                        attachment.IncidentCode = Attachment.IncidentCode;
                        attachment.LoadedBy = Attachment.LoadedBy;
                        attachment.LoadedDate = Attachment.LoadedDate;
                        attachment.AttachmentType = Attachment.AttachmentType;
                        attachment.RowIdentifier = Attachment.RowIdentifier;
                        attachment.Name = Attachment.Name;

                        sessionManager.CurrentRIDDORAttachmentList.Add(attachment);
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return ReturnView(model);
                    }
                }
                else
                {
                    model.Message = Resources.MESSAGE_MAXATTACHMENTSIZEEXCEEDED;
                    return ReturnView(model);
                }
            }
            model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
            ModelState.Clear();
            return ReturnView(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult FastTrackFile(FormCollection collection, HttpPostedFileBase FastTrackFile)
        {
            var model = GetUpdatedModel();

            if (FastTrackFile != null)
            {
                if (FastTrackFile.ContentLength <= Int32.Parse(ConfigurationManager.AppSettings["MaxAttachmentSize"]))
                {
                    // Create service instance
                    BinaryDataTransferServiceClient sc = new BinaryDataTransferServiceClient();

                    //Attempt update
                    try
                    {
                        // Call service to create new Attachment
                        string[] messages = new string[0];
                        UcbWeb.BinaryDataTransferService.AttachmentDC Attachment = new UcbWeb.BinaryDataTransferService.AttachmentDC() { AttachmentType = "FastTrack", IncidentCode = model.IncidentItem.Code, LoadedBy = CurrentUser, LoadedDate = DateTime.Now, Name = FastTrackFile.FileName };
                        bool returnedObject = sc.UploadAttachment(ref Attachment, CurrentUser.ToString(), FastTrackFile.InputStream, out messages);
                        //Map the BinaryTransfer service attachment object to an attachmentmodel object and add it to session
                        AttachmentModel attachment = new AttachmentModel();
                        attachment.Code = Attachment.Code;
                        attachment.IncidentCode = Attachment.IncidentCode;
                        attachment.LoadedBy = Attachment.LoadedBy;
                        attachment.LoadedDate = Attachment.LoadedDate;
                        attachment.AttachmentType = Attachment.AttachmentType;
                        attachment.RowIdentifier = Attachment.RowIdentifier;
                        attachment.Name = Attachment.Name;

                        sessionManager.CurrentFastTrackAttachmentList.Add(attachment);
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return ReturnView(model);
                    }
                }
                else
                {
                    model.Message = Resources.MESSAGE_MAXATTACHMENTSIZEEXCEEDED;
                    return ReturnView(model);
                }

            }
            model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
            ModelState.Clear();
            return ReturnView(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult RepeatBehaviourFile(FormCollection collection, HttpPostedFileBase RepeatBehaviourFile)
        {
            var model = GetUpdatedModel();

            if (RepeatBehaviourFile != null)
            {
                if (RepeatBehaviourFile.ContentLength <= Int32.Parse(ConfigurationManager.AppSettings["MaxAttachmentSize"]))
                {
                    // Create service instance
                    BinaryDataTransferServiceClient sc = new BinaryDataTransferServiceClient();

                    //Attempt update
                    try
                    {
                        // Call service to create new Attachment
                        string[] messages = new string[0];
                        UcbWeb.BinaryDataTransferService.AttachmentDC Attachment = new UcbWeb.BinaryDataTransferService.AttachmentDC() { AttachmentType = "RepeatBehaviour", IncidentCode = model.IncidentItem.Code, LoadedBy = CurrentUser, LoadedDate = DateTime.Now, Name = RepeatBehaviourFile.FileName };
                        bool returnedObject = sc.UploadAttachment(ref Attachment, CurrentUser.ToString(), RepeatBehaviourFile.InputStream, out messages);
                        //Map the BinaryTransfer service attachment object to an attachmentmodel object and add it to session
                        AttachmentModel attachment = new AttachmentModel();
                        attachment.Code = Attachment.Code;
                        attachment.IncidentCode = Attachment.IncidentCode;
                        attachment.LoadedBy = Attachment.LoadedBy;
                        attachment.LoadedDate = Attachment.LoadedDate;
                        attachment.AttachmentType = Attachment.AttachmentType;
                        attachment.RowIdentifier = Attachment.RowIdentifier;
                        attachment.Name = Attachment.Name;

                        sessionManager.CurrentRepeatBehaviourAttachmentList.Add(attachment);
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return View(model);
                    }
                }
                else
                {
                    model.Message = Resources.MESSAGE_MAXATTACHMENTSIZEEXCEEDED;
                    return ReturnView(model);
                }
            }
            model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
            ModelState.Clear();
            return ReturnView(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult GeneralEvidenceFile(FormCollection collection, HttpPostedFileBase GeneralEvidenceFile)
        {
            var model = GetUpdatedModel();

            if (GeneralEvidenceFile != null)
            {
                if (GeneralEvidenceFile.ContentLength <= Int32.Parse(ConfigurationManager.AppSettings["MaxAttachmentSize"]))
                {
                    // Create service instance
                    BinaryDataTransferServiceClient sc = new BinaryDataTransferServiceClient();

                    //Attempt update
                    try
                    {
                        // Call service to create new Attachment
                        string[] messages = new string[0];
                        UcbWeb.BinaryDataTransferService.AttachmentDC Attachment = new UcbWeb.BinaryDataTransferService.AttachmentDC() { AttachmentType = "GeneralEvidence", IncidentCode = model.IncidentItem.Code, LoadedBy = CurrentUser, LoadedDate = DateTime.Now, Name = GeneralEvidenceFile.FileName };
                        bool returnedObject = sc.UploadAttachment(ref Attachment, CurrentUser.ToString(), GeneralEvidenceFile.InputStream, out messages);
                        //Map the BinaryTransfer service attachment object to an attachmentmodel object and add it to session
                        AttachmentModel attachment = new AttachmentModel();
                        attachment.Code = Attachment.Code;
                        attachment.IncidentCode = Attachment.IncidentCode;
                        attachment.LoadedBy = Attachment.LoadedBy;
                        attachment.LoadedDate = Attachment.LoadedDate;
                        attachment.AttachmentType = Attachment.AttachmentType;
                        attachment.RowIdentifier = Attachment.RowIdentifier;
                        attachment.Name = Attachment.Name;

                        sessionManager.CurrentGeneralEvidenceAttachmentList.Add(attachment);
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return View(model);
                    }
                }
                else
                {
                    model.Message = Resources.MESSAGE_MAXATTACHMENTSIZEEXCEEDED;
                    return ReturnView(model);
                }
            }
            model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
            ModelState.Clear();
            return ReturnView(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult FurtherInfoFile(FormCollection collection, HttpPostedFileBase FurtherInfoFile)
        {
            var model = GetUpdatedModel();

            if (FurtherInfoFile != null)
            {
                if (FurtherInfoFile.ContentLength <= Int32.Parse(ConfigurationManager.AppSettings["MaxAttachmentSize"]))
                {
                    // Create service instance
                    BinaryDataTransferServiceClient sc = new BinaryDataTransferServiceClient();

                    //Attempt update
                    try
                    {
                        // Call service to create new Attachment
                        string[] messages = new string[0];
                        UcbWeb.BinaryDataTransferService.AttachmentDC Attachment = new UcbWeb.BinaryDataTransferService.AttachmentDC() { AttachmentType = "FurtherInfo", IncidentCode = model.IncidentItem.Code, LoadedBy = CurrentUser, LoadedDate = DateTime.Now, Name = FurtherInfoFile.FileName };
                        bool returnedObject = sc.UploadAttachment(ref Attachment, CurrentUser.ToString(), FurtherInfoFile.InputStream, out messages);
                        //Map the BinaryTransfer service attachment object to an attachmentmodel object and add it to session
                        AttachmentModel attachment = new AttachmentModel();
                        attachment.Code = Attachment.Code;
                        attachment.IncidentCode = Attachment.IncidentCode;
                        attachment.LoadedBy = Attachment.LoadedBy;
                        attachment.LoadedDate = Attachment.LoadedDate;
                        attachment.AttachmentType = Attachment.AttachmentType;
                        attachment.RowIdentifier = Attachment.RowIdentifier;
                        attachment.Name = Attachment.Name;

                        sessionManager.CurrentFurtherInfoAttachmentList.Add(attachment);
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return View(model);
                    }
                }
                else
                {
                    model.Message = Resources.MESSAGE_MAXATTACHMENTSIZEEXCEEDED;
                    return ReturnView(model);
                }
            }
            model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
            ModelState.Clear();
            return ReturnView(model);
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ReferralEvidenceFile(FormCollection collection, HttpPostedFileBase ReferralEvidenceFile)
        {
            var model = GetUpdatedModel();

            if (ReferralEvidenceFile != null)
            {
                if (ReferralEvidenceFile.ContentLength <= Int32.Parse(ConfigurationManager.AppSettings["MaxAttachmentSize"]))
                {
                    // Create service instance
                    BinaryDataTransferServiceClient sc = new BinaryDataTransferServiceClient();

                    //Attempt update
                    try
                    {
                        // Call service to create new Attachment
                        string[] messages = new string[0];
                        UcbWeb.BinaryDataTransferService.AttachmentDC Attachment = new UcbWeb.BinaryDataTransferService.AttachmentDC() { AttachmentType = "ReferralEvidence", IncidentCode = model.IncidentItem.Code, LoadedBy = CurrentUser, LoadedDate = DateTime.Now, Name = ReferralEvidenceFile.FileName };
                        bool returnedObject = sc.UploadAttachment(ref Attachment, CurrentUser.ToString(), ReferralEvidenceFile.InputStream, out messages);
                        //Map the BinaryTransfer service attachment object to an attachmentmodel object and add it to session
                        AttachmentModel attachment = new AttachmentModel();
                        attachment.Code = Attachment.Code;
                        attachment.IncidentCode = Attachment.IncidentCode;
                        attachment.LoadedBy = Attachment.LoadedBy;
                        attachment.LoadedDate = Attachment.LoadedDate;
                        attachment.AttachmentType = Attachment.AttachmentType;
                        attachment.RowIdentifier = Attachment.RowIdentifier;
                        attachment.Name = Attachment.Name;

                        sessionManager.CurrentReferralEvidenceAttachmentList.Add(attachment);
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return View(model);
                    }
                }
                else
                {
                    model.Message = Resources.MESSAGE_MAXATTACHMENTSIZEEXCEEDED;
                    return ReturnView(model);
                }
            }
            model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
            ModelState.Clear();
            return ReturnView(model);
        }

        //GET: /Incident/DownloadAttachment
        public ActionResult DownloadAttachment(string code, string filename)
        {
            var model = GetUpdatedModel();

            Stream documentBody = new MemoryStream();

            bool result;
            FileStreamResult outputStreamResult = null;

            // Create service instance
            BinaryDataTransferServiceClient sc = new BinaryDataTransferServiceClient();

            string[] messages = new string[0];

            try
            {
                //Call service to donwload attachement
                messages = sc.DownloadAttachment(Guid.Parse(code), CurrentUser, out result, out documentBody);
                sc.Close();

                string contentType = null;
                switch (filename.Substring(filename.LastIndexOf('.') + 1).ToUpper())
                {
                    case "DOC":
                        contentType = "application/msword";
                        break;
                    case "XLS":
                        contentType = "application/vnd.ms-excel";
                        break;
                    case "PPT":
                        contentType = "application/vnd.ms-powerpoint";
                        break;
                    case "PDF":
                        contentType = "application/pdf";
                        break;
                    case "JPG":
                    case "JPEG":
                        contentType = "image/jpeg";
                        break;
                    case "GIF":
                        contentType = "image/gif";
                        break;
                    case "ICO":
                        contentType = "image/vnd.microsoft.icon";
                        break;
                    case "ZIP":
                        contentType = "application/zip";
                        break;
                    default:
                        contentType = "application/octet-stream";
                        break;
                }

                outputStreamResult = new FileStreamResult(documentBody, contentType);
                outputStreamResult.FileDownloadName = filename;
            }
            catch (Exception e)
            {
                // Handle the exception
                string message = ExceptionManager.HandleException(e, sc);
                model.Message = message;

                return View(model);
            }

            ModelState.Clear();
            return outputStreamResult;
        }

        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult RemoveAttachment()
        {
            var model = GetUpdatedModel();
            // Create service instance
            UcbServiceClient sc = new UcbServiceClient();

            // Iterate through form keys
            foreach (string Key in Request.Form.Keys)
            {
                // Test if Select button was clicked...
                if (Key.StartsWith("Edit::RemoveAttachment"))
                {
                    // Retrieve ID for entity which was selected
                    string attachmentCode = Key.Substring(23);

                    try
                    {
                        byte[] lockID = new byte[0];

                        sc.DeleteAttachmentAndData(CurrentUser, CurrentUser, appID, "", attachmentCode, lockID);
                        sc.Close();

                        Guid attachmentGuid = Guid.Parse(attachmentCode);

                        //Remove deleteed attachment from session
                        AttachmentModel removedRIDDORAttachment = sessionManager.CurrentRIDDORAttachmentList.Find(x => x.Code == attachmentGuid);
                        if (removedRIDDORAttachment != null)
                        {
                            sessionManager.CurrentRIDDORAttachmentList.Remove(removedRIDDORAttachment);
                        }

                        AttachmentModel removedFastTrackAttachment = sessionManager.CurrentFastTrackAttachmentList.Find(x => x.Code == attachmentGuid);
                        if (removedFastTrackAttachment != null)
                        {
                            sessionManager.CurrentFastTrackAttachmentList.Remove(removedFastTrackAttachment);
                        }

                        AttachmentModel removedRepeatBehaviourAttachment = sessionManager.CurrentRepeatBehaviourAttachmentList.Find(x => x.Code == attachmentGuid);
                        if (removedRepeatBehaviourAttachment != null)
                        {
                            sessionManager.CurrentRepeatBehaviourAttachmentList.Remove(removedRepeatBehaviourAttachment);
                        }

                        AttachmentModel generalEvidenceAttachment = sessionManager.CurrentGeneralEvidenceAttachmentList.Find(x => x.Code == attachmentGuid);
                        if (generalEvidenceAttachment != null)
                        {
                            sessionManager.CurrentGeneralEvidenceAttachmentList.Remove(generalEvidenceAttachment);
                        }

                        AttachmentModel furtherInfoAttachment = sessionManager.CurrentFurtherInfoAttachmentList.Find(x => x.Code == attachmentGuid);
                        if (furtherInfoAttachment != null)
                        {
                            sessionManager.CurrentFurtherInfoAttachmentList.Remove(furtherInfoAttachment);
                        }

                        AttachmentModel referralEvidenceAttachment = sessionManager.CurrentReferralEvidenceAttachmentList.Find(x => x.Code == attachmentGuid);
                        if (referralEvidenceAttachment != null)
                        {
                            sessionManager.CurrentReferralEvidenceAttachmentList.Remove(referralEvidenceAttachment);
                        }
                    }
                    catch (Exception e)
                    {
                        // Handle the exception
                        string message = ExceptionManager.HandleException(e, sc);
                        model.Message = message;

                        return ReturnView(model);
                    }
                }
            }
            model.Message = Resources.MESSAGE_UPDATE_SUCCEEDED;
            ModelState.Clear();
            // Return to the Screen
            return ReturnView(model);
        }

        #endregion

        #region BusinessAreas

        public ActionResult RefreshBusinessAreas(Guid businessCode)
        {
            IncidentModel IncidentServiceVersion = sessionManager.IncidentServiceVersion;
            List<OrganisationModel> businessAreaList = GetBusinessAreaByBusiness(businessCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.StaffMemberBusinessAreaCode)).ToList();

            businessAreaList.Insert(0, new OrganisationModel { Code = Guid.Empty, Name = @String.Format(Resources.DDL_GENERIC, @Resources.LABEL_INCIDENT_STAFFMEMBERBUSINESSAREACODE) });
            return Json(businessAreaList.Select(x => new { value = (x.Code == Guid.Empty ? String.Empty : x.Code.ToString()), text = (x.Code == Guid.Empty ? x.Name : x.NameAndActiveFlag) }), JsonRequestBehavior.AllowGet);
        }

        private List<OrganisationModel> GetBusinessAreaByBusiness(Guid code)
        {
            List<OrganisationModel> businessAreaList = new List<OrganisationModel>(cacheManager.IncidentListCache.StaffMemberBusinessAreaList);

            return businessAreaList.Where(x => x.ImmediateParent == code).ToList();
        }

        /// <summary>
        /// Determines whether the 'other' location was selected].
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        ///   <c>true</c> if the other location selected; otherwise, <c>false</c>.
        /// </returns>
        private bool IsOtherLocationSelected(Guid? code)
        {
            if (code == null)
            {
                return false;
            }

            List<IncidentLocationModel> locationList = new List<IncidentLocationModel>(cacheManager.IncidentListCache.IncidentLocationList);

            var selectedLocation = locationList.Where(x => x.Code == code).Single();

            return string.Compare(selectedLocation.Description, DataConstants.OTHER_LOCATION_TEXT, true) == 0;
        }

        public ActionResult ShowOrHideIncidentDetailAndAbuseType(Guid incidentTypeCode)
        {
            IncidentTypeModel incidentType = GetCurrentSelectedIncidentType(incidentTypeCode);

            return Json(incidentType, JsonRequestBehavior.AllowGet);
        }

        private IncidentTypeModel GetCurrentSelectedIncidentType(Guid? incidentTypecode)
        {
            IncidentTypeModel incidentType = cacheManager.IncidentListCache.IncidentTypeList.Where(x => x.Code == incidentTypecode).Single();
            sessionManager.ShowAbuseType = incidentType.HasAbuseType;
            sessionManager.ShowIncidentDetail = incidentType.HasDetails;
            return incidentType;
        }

        #endregion

        #region HomeOffices

        public ActionResult RefreshHomeOffices(Guid businessAreaCode)
        {
            IncidentModel IncidentServiceVersion = sessionManager.IncidentServiceVersion;
            List<SiteModel> homeOfficeList = GetHomeOfficesByBusinessArea(businessAreaCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.StaffMemberHomeOfficeCode)).ToList();
            homeOfficeList.Insert(0, new SiteModel { Code = Guid.Empty, SiteName = @String.Format(Resources.DDL_GENERIC, @Resources.ENTITYNAME_ORGANISATION) });
            return Json(homeOfficeList.Select(x => new { value = (x.Code == Guid.Empty ? String.Empty : x.Code.ToString()), text = (x.Code == Guid.Empty ? x.SiteName : x.NameAndActiveFlag) }), JsonRequestBehavior.AllowGet);
        }

        private List<SiteModel> GetHomeOfficesByBusinessArea(Guid code)
        {
            // Create service instance
            UcbServiceClient sc = new UcbServiceClient();

            //Attempt update
            try
            {

                List<SiteModel> returnedObject = null;
                string businessAreaCode = code.ToString();

                List<SiteDC> sites = new List<SiteDC>(sc.GetAllSitesForLevelThreeOrganisation(CurrentUser, CurrentUser, appID, "", businessAreaCode));

                // Close service communication
                sc.Close();

                returnedObject = Mapper.Map<List<SiteModel>>(sites);

                return returnedObject;
            }
            catch (Exception e)
            {
                // Handle the exception
                ExceptionManager.HandleException(e, sc);

                return null;
            }
        }

        public ActionResult ResetHomeOffices()
        {
            List<SiteModel> homeOfficeList = new List<SiteModel>();
            homeOfficeList.Insert(0, new SiteModel { Code = Guid.Empty, SiteName = @String.Format(Resources.DDL_GENERIC, @Resources.ENTITYNAME_ORGANISATION) });
            return Json(homeOfficeList.Select(x => new { value = (x.Code == Guid.Empty ? String.Empty : x.Code.ToString()), text = (x.Code == Guid.Empty ? x.SiteName : x.NameAndActiveFlag) }), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Incident Types

        public ActionResult RefreshIncidentTypes(Guid categoryCode)
        {
            IncidentModel IncidentServiceVersion = sessionManager.IncidentServiceVersion;
            List<IncidentTypeModel> incidentTypeList = GetIncidentTypeByCategory(categoryCode).Where(x => x.IsActive || x.Code == (null == IncidentServiceVersion ? Guid.Empty : IncidentServiceVersion.IncidentTypeCode)).ToList();
            incidentTypeList.Insert(0, new IncidentTypeModel { Code = Guid.Empty, Description = @String.Format(Resources.DDL_GENERIC, @Resources.ENTITYNAME_INCIDENTTYPE) });
            return Json(incidentTypeList.Select(x => new { value = (x.Code == Guid.Empty ? String.Empty : x.Code.ToString()), text = (x.Code == Guid.Empty ? x.Description : x.DescriptionAndActiveFlag) }), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the review date for control measures.
        /// </summary>
        /// <param name="controlMeasures">The category codes.</param>
        /// <param name="incidentDate">The incident date.</param>
        /// <returns>
        /// A review date that is based on the control measures passes.
        /// </returns>
        public ActionResult GetReviewDateForControlMeasures(List<Guid> controlMeasures, DateTime? incidentDate)
        {
            List<ControlMeasureModel> controlMeasuresList = cacheManager.IncidentListCache.ControlMeasureBaseList;

            string reviewPeriod = string.Empty;

            if (incidentDate.HasValue && incidentDate.Value != DateTime.MinValue)
            {
                if (controlMeasures != null && controlMeasures.Count() > 0)
                {
                    var selectedControlMeasures = from m in controlMeasuresList where controlMeasures.Contains(m.Code) select m;

                    if (selectedControlMeasures != null && selectedControlMeasures.Count() > 0)
                    {
                        // the review date is set for the lowest review date period found in the contol measures passed.
                        int minReviewPeriod = selectedControlMeasures.Min(m => m.ReviewPeriod);

                        // create a data string to return representing the 
                        reviewPeriod = incidentDate.Value.Date.AddMonths(minReviewPeriod).Date.ToShortDateString();
                    }
                }

                // Previous control measures existed - always set to 12 months from previous date
                if (sessionManager.IncidentServiceVersion.ControlMeasureCodes != null && sessionManager.IncidentServiceVersion.ControlMeasureCodes.Count() > 0)
                {
                    if (sessionManager.IncidentServiceVersion.ReviewDate.HasValue)
                    {
                        reviewPeriod = sessionManager.IncidentServiceVersion.ReviewDate.GetValueOrDefault().AddMonths(12).Date.ToShortDateString();
                    }
                }

            }

            return Json(new { ReviewPeriod = reviewPeriod }, JsonRequestBehavior.AllowGet);
        }


        private List<IncidentTypeModel> GetIncidentTypeByCategory(Guid code)
        {
            List<IncidentTypeModel> incidentTypeList = new List<IncidentTypeModel>(cacheManager.IncidentListCache.IncidentTypeList);
            return incidentTypeList.Where(x => x.IncidentCategoryCode == code).ToList();
        }

        #endregion



        private ActionResult RedirectIfDifferenctSession(FormCollection collection)
        {
            if (sessionManager.IncidentCode != null)
            {
                if (!collection["IncidentItem.Code"].ToUpper().Equals(sessionManager.IncidentCode.ToUpper()))
                {
                    // To prevent updates 
                    ClearSessionObjects();
                    return View("MultipleSessionsError");
                }
            }
            return null;
        }


        #region actions for Referrals


        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult ArchiveReferral(FormCollection collection)
        {
            return UpdateReferral(collection, "Archived");
        }

        // POST: /Incident/Edit with Save button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult PrintReferral(FormCollection collection)
        {
            // Check if sole session
            if (RedirectIfDifferenctSession(collection) != null) return View("MultipleSessionsError");

            // Get the updated model
            var model = GetUpdatedModel();


            byte[] Report = RenderIncidentReportAsPdf(model.IncidentItem.Code.ToString(), "ReferralReport");
            FileContentResult ReportResult = new FileContentResult(Report, "application/pdf");
            ReportResult.FileDownloadName = "ReferralReport_" + model.IncidentItem.IncidentID.ToString() + ".pdf";
            return ReportResult;

        }

        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult PublishReferral(FormCollection collection)
        {
            return UpdateReferral(collection, "Live");
        }


        // POST: /Incident/Edit with Save button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveReferral(FormCollection collection)
        {
            return UpdateReferral(collection, "New");
        }

        // POST: /Incident/Edit with Save And Close button submitting
        [CustomAuthorize(Roles = AppRoles.ADMIN + "," + AppRoles.DEPUTY_NOMINATED_MANAGER + "," + AppRoles.NOMINATED_MANAGER + "," + AppRoles.BUSINESS_AREA_MANAGER)]
        [HttpParamEdit(Prefix = "Edit::")]
        [HttpPost]
        public ActionResult SaveAndCloseReferral(FormCollection collection)
        {
            return UpdateReferral(collection, "New");
        }

        private ActionResult UpdateReferral(FormCollection collection, string newIncidentState)
        {
            // Check if sole session
            if (RedirectIfDifferenctSession(collection) != null) return View("MultipleSessionsError");

            // Get the updated model
            var model = GetUpdatedModel();

            // Test to see if there are any errors
            var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                    .ToArray();
            // Test to see if the model has validated correctly         

            if (ModelState.IsValid)
            {
                if ((newIncidentState == IncidentStatus.Archived && model.IsArchiveConfirmed == "True")
                    || (newIncidentState == IncidentStatus.Live && model.IsPublishConfirmed == "True")
                    || ((newIncidentState == null || newIncidentState == IncidentStatus.New) && (model.IsSaveConfirmed == "True" || model.IsSaveAndCloseConfirmed == "True"))
                    || (((newIncidentState == null || newIncidentState == IncidentStatus.New) && model.IsSaveConfirmed == "False") && !model.IsViewDirty))
                {

                    //Set flags false
                    SetFlagsFalse(model);
                    if (model.StatusChangeTo != model.IncidentItem.IncidentStatus && model.StatusChangeTo != null && model.StatusChangeTo != "")
                    {
                        model.IncidentItem.IncidentStatus = model.StatusChangeTo;
                    }

                    if (model.HandleCaseFlag)
                    {
                        // if the current user has said they want to hanle the case then set them as the current owner
                        model.IncidentItem.CurrentOwnerStaffCode = new Guid(CurrentUser);
                    }
                    else
                    {
                        // if the handle case flag is not checked it might be because the user just doesn't own it (don't do anything as might be owned by someone else),
                        //... otherwise person who is handling the case has decided they do not want to own it any longer - so remove the handle case owner..
                        if (model.CurrentOwnerItem != null && (string.Compare(model.CurrentOwnerItem.Code.ToString(), CurrentUser, true) == 0))
                        {
                            model.IncidentItem.CurrentOwnerStaffCode = null;
                        }
                    }

                    // Map model to data contract
                    IncidentDC IncidentItem = Mapper.Map<IncidentDC>(model.IncidentItem);
                    CustomerDC CustomerItem = Mapper.Map<CustomerDC>(model.CustomerItem);


                    if (model.ReviewActionNarrativeItem == null)
                    {
                        model.ReviewActionNarrativeItem = new NarrativeModel();
                    }

                    NarrativeDC furtherInfoNarrativeItem = Mapper.Map<NarrativeDC>(model.FurtherInfoNarrativeItem);
                    NarrativeDC reviewActionNarrativeItem = Mapper.Map<NarrativeDC>(model.ReviewActionNarrativeItem);


                    List<string> controlMeasureCodes = new List<string>();
                    List<string> systemMarkedCodes = new List<string>();
                    List<string> interestedPartyCodes = new List<string>();


                    if (model.IncidentItem.ControlMeasureCodes != null)
                    {
                        controlMeasureCodes = model.IncidentItem.ControlMeasureCodes.ToList();
                    }
                    if (model.IncidentItem.SystemMarkedCodes != null)
                    {
                        systemMarkedCodes = model.IncidentItem.SystemMarkedCodes.ToList();
                    }
                    if (model.IncidentItem.InterestedPartyCodes != null)
                    {
                        interestedPartyCodes = model.IncidentItem.InterestedPartyCodes.ToList();
                    }

                    IncidentVMDC returnedObject = null;

                    // if no state specified assume no change
                    if (String.IsNullOrEmpty(newIncidentState))
                        newIncidentState = model.IncidentItem.IncidentStatus;

                    using (UcbServiceClient sc = new UcbServiceClient())
                    {
                        try
                        {
                            // Call service to update Incident item
                            returnedObject = sc.UpdateReferral(CurrentUser, CurrentUser, appID, "", newIncidentState, IncidentItem, CustomerItem, furtherInfoNarrativeItem, reviewActionNarrativeItem, controlMeasureCodes.ToArray(), systemMarkedCodes.ToArray(), interestedPartyCodes.ToArray());
                            sessionManager.IncidentCode = returnedObject.IncidentItem.Code.ToString();
                        }
                        catch (Exception e)
                        {
                            // Handle the exception
                            string message = ExceptionManager.HandleException(e, sc);
                            model.Message = message;

                            return ReturnView(model);
                        }
                    }
                    model.HandleCaseFlag = model.CurrentOwnerItem != null && (string.Compare(model.CurrentOwnerItem.Code.ToString(), CurrentUser, true) == 0);

                    // return to the calling page
                    sessionManager.MessageFromPageFrom = Resources.MESSAGE_UPDATE_SUCCEEDED;
                    if (model.IsSaveAndCloseConfirmed == "True")
                    {
                        ClearSessionObjects();
                        switch (sessionManager.PageFrom)
                        {
                            case "SearchMyNewReports":
                                sessionManager.PageFrom = "EditReferral";
                                return RedirectToAction("MyNewReports", "Searches");
                            case "SearchMyReviews":
                                sessionManager.PageFrom = "EditReferral";
                                return RedirectToAction("MyReviews", "Searches");
                            case "SearchMyForwardLook":
                                sessionManager.PageFrom = "EditReferral";
                                return RedirectToAction("MyForwardLook", "Searches");
                            case "DeputySearchMyNewReports":
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("DeputyMyNewReports", "Searches");
                            case "DeputySearchMyReviews":
                                sessionManager.PageFrom = "EditIncident";
                                return RedirectToAction("DeputyMyReviews", "Searches");

                            default:
                                sessionManager.PageFrom = "EditReferral";
                                return RedirectToAction("Index", "Home");
                        }
                    }
                }
                else
                {
                    //Set flags false
                    SetFlagsFalse(model);
                    switch (newIncidentState)
                    {
                        case IncidentStatus.Live:
                            model.Message = Resources.MESSAGE_PUBLISHCONFIRMATION;
                            model.IsPublishConfirmed = "True";
                            break;
                        case IncidentStatus.Archived:
                            model.Message = Resources.MESSAGE_ARCHIVECONFIRMATION;
                            model.IsArchiveConfirmed = "True";
                            break;
                        default:
                            model.Message = Resources.MESSAGE_SAVECONFIRMATION;
                            model.IsSaveConfirmed = "True";
                            break;
                    }
                }
                return RedirectToAction("Edit");
            }

            return ReturnView(model);
        }
        #endregion

        [HttpPost]
        public JsonResult KeepSessionAlive()
        {
            return new JsonResult { Data = "Success" };
        }

        [HttpPost]
        public JsonResult AbandonSession()
        {
            Session.Abandon();
            return new JsonResult { Data = "Success" };
        }
    }
}

