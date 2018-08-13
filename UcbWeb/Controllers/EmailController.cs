using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UcbWeb.ViewModels;
using UcbWeb.UcbService;
using UcbWeb.Helpers;
using UcbWeb.Models;
using UcbWeb.DirectoryService;
using UcbWeb.SmtpEmailService;
using System.Configuration;
using System.Text;

namespace UcbWeb.Controllers
{
    public class EmailController : BaseController
    {
        
         // Dependency Injection enabled constructors

        private IUcbService UcbService;
        public EmailController()
            : this(new UcbServiceClient(), new SessionManager(), new CacheManager())
        {
        }

        public EmailController(IUcbService UcbService, ISessionManager sessionManager, ICacheManager cacheManager)
            : base(sessionManager, cacheManager)
        {
            this.UcbService = UcbService;
        }

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail()
        {
            var model = new EmailVM();
            model.LineManagerEmailAddressList = new List<string>();
            model.IncidentItem = sessionManager.CurrentIncident;
            model.ManagerFirstName = model.IncidentItem.ManagerFirstName;
            model.ManagerLastName = model.IncidentItem.ManagerLastName;
            model.NominatedManager = sessionManager.NominatedManager;
            model.NominatedManagerEmailAddress = sessionManager.NominatedManagerEmailAddress;
            model.Message = "Incident ID " + model.IncidentItem.IncidentID + " Created";

            sessionManager.EmailDetails = model;
            
            return View(model);
        }


        [HttpParamSendEmail(Prefix = "SendEmail::")]
        [HttpPost]
        public ActionResult UpdateManagerEmailAddress(FormCollection collection)
        {
             var model = GetUpdatedModel();
             var errors = ModelState
                   .Where(x => x.Value.Errors.Count > 0)
                   .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                   .ToArray();

            // Test to see if the model has validated correctly
             if (ModelState.IsValid)
             {
                 if (!string.IsNullOrEmpty(model.ManagerFirstName) && !string.IsNullOrEmpty(model.ManagerLastName))
                 {
                     //Call LDAP service to get Line Manager Email Address. Put these in Model.LineManagerEmailAddresses. If only one returned then default this as the selected item

                     DirectoryServiceClient dsc = new DirectoryServiceClient();
                     try
                     {
                         model.LineManagerEmailAddressList = dsc.GetEmailAddress(model.ManagerFirstName.Trim(), model.ManagerLastName.Trim()).ToList();
                         dsc.Close();

                     }
                     catch (Exception e)
                     {
                         // Handle the exception
                         string message = ExceptionManager.HandleException(e, dsc);
                         model.Message = message;

                         return View(model);
                     }

                     sessionManager.EmailDetails = model;
                 }
             }
            ModelState.Clear();
            return View(model);
        }


        [HttpParamSendEmail(Prefix = "SendEmail::")]
        [HttpPost]
        public ActionResult SendEmailToLineAndNominatedManagers(FormCollection collection)
        {
            var model = GetUpdatedModel();

            string toEmailAddress = model.LineManagerEmailAddress;

            //These were saved when incident was created
            string fromEmailAddress = sessionManager.FromEmailAddress;
            string ccEmailAddress = sessionManager.NominatedManagerEmailAddress;

            var errors = ModelState
                   .Where(x => x.Value.Errors.Count > 0)
                   .Select(x => new { x.Key, x.Value.Errors[0].ErrorMessage })
                   .ToArray();

            // Test to see if the model has validated correctly
            if (ModelState.IsValid)
            {
                SmtpEmailServiceClient esc = new SmtpEmailServiceClient();
                try
                {
                    if (model.IncidentItem.IncidentStatus == IncidentStatus.New)//i.e. incident just created
                    {
                        string emailSubject = "OFFICIAL: " + sessionManager.CurrentCustomer.FirstName + " " + sessionManager.CurrentCustomer.LastName  + " has been reported by a member of your staff for unacceptable customer or claimant behaviour";

                        // Retrieve raw email link template from config
                        string emailLink = ConfigurationManager.AppSettings.Get("EmailLinkURL");

                        var encryptedIncidentCode = Encryptor.Crypt(model.IncidentItem.Code.ToString(), "UCBEncrypt", true);
                        var SafeencryptedIncidentCode = SafeBase64UrlEncoder.EncodeBase64Url(encryptedIncidentCode);
                        // Add ID and Code to email link
                        emailLink = string.Format(emailLink, SafeencryptedIncidentCode + "&", model.IncidentItem.Code);

                        StringBuilder emailBody = new StringBuilder();


                        if (String.IsNullOrWhiteSpace(toEmailAddress))
                        {
                            emailBody.AppendLine("<br /> Hi " + sessionManager.NominatedManager);
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> This email has been sent to you only so that you are aware that a new report has been submitted into the database that requires your action. "
                                + "Please open the UCB application and select your reports to see this reported incident.");
                            emailBody.AppendLine("<br />");
                                emailBody.AppendLine("<br /> No email has been sent to Line Manager " + model.IncidentItem.ManagerFirstName.Trim() + " " + model.IncidentItem.ManagerLastName.Trim() + " because no email address was selected by the reporter. ");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /><a href=" + emailLink + ">Please click here to view the incident</a>");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> Regards");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> The DWP Health, Safety and Wellbeing Team");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br />E_M_A_I_L_B_L_O_C_K");
                        }
                        else
                        {
                            emailBody.Append("Hi " + model.IncidentItem.ManagerFirstName + " " + model.IncidentItem.ManagerLastName);
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> A member of your staff has reported an incident of unacceptable customer or claimant behaviour that requires your action. "
                                + "Please click on the link below to open the incident form and complete the Line Managers Section. Do not forward this link onto anyone else as it is only for your Line Managers action.");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> Note: When using this link please complete the Line Managers Section within a single visit.");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /><a href=" + emailLink + ">Please click here to view the incident</a>");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> Hi " + sessionManager.NominatedManager);
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> This email has been copied to you so that you are aware that a new report has been submitted into the database that requires your action. "
                                + "Please open the UCB application and select your reports to see this reported incident.");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> Regards");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br /> The DWP Health, Safety and Wellbeing Team");
                            emailBody.AppendLine("<br />");
                            emailBody.AppendLine("<br />E_M_A_I_L_B_L_O_C_K");
                        }

                        esc.SendEmail(fromEmailAddress, toEmailAddress, ccEmailAddress, emailSubject, emailBody.ToString());
                        esc.Close();
                    }
                }
                catch (Exception e)
                {
                    // Handle the exception
                    string message = ExceptionManager.HandleException(e, esc);
                    model.Message = message;

                    return View(model);
                }
            }
            sessionManager.EmailDetails = model;
            model.Message = null;
            //sessionManager.LineManagerName = model.LineManagerFirstName + " " + model.LineManagerLastName;
            return RedirectToAction("EmailConfirmation", "Confirmation");
        }


        /// <summary>
        /// Private method to merge in the model 
        /// </summary>
        /// <returns></returns>
        private EmailVM GetUpdatedModel()
        {
            EmailVM model = new EmailVM();
            model = sessionManager.EmailDetails;
            
            bool result = TryUpdateModel(model);

            return model;
        }

    }
}
