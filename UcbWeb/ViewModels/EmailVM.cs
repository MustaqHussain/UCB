
using UcbWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Ucb.ResourceLibrary;
using System;
using System.Web.Mvc;
using UcbWeb.Helpers;
using System.Linq;
using UcbWeb.DataAnnotation;

namespace UcbWeb.ViewModels
{
    public partial class EmailVM
    {

        public IncidentModel IncidentItem { get; set; }
        public string Message { get; set; }
        public bool IsViewDirty { get; set; }

         [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(50)]
        [Tooltip("TOOLTIP_INCIDENT_MANAGERFIRSTNAME", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_INCIDENT_MANAGERFIRSTNAME", ResourceType = typeof(Resources))]
        [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
        public string ManagerFirstName { get; set; }

         [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
        [StringLength(50)]
        [Tooltip("TOOLTIP_INCIDENT_MANAGERLASTNAME", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_INCIDENT_MANAGERLASTNAME", ResourceType = typeof(Resources))]
        [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
        public string ManagerLastName { get; set; }

        [Tooltip("TOOLTIP_INCIDENT_LINEMANAGEREMAIL", ResourceType = typeof(Resources))]
        [Display(Name = "LABEL_INCIDENT_LINEMANAGEREMAIL", ResourceType = typeof(Resources))]
        public string LineManagerEmailAddress { get; set; }


        public List<string> LineManagerEmailAddressList { get; set; }
        public string NominatedManager { get; set; }
        public string NominatedManagerEmailAddress;




    }

}