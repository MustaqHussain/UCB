
using UcbWeb.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Ucb.ResourceLibrary;
using UcbWeb.UcbService;
namespace UcbWeb.ViewModels
{

    public partial class IntranetStaffProtectionVM 
    {
        public IntranetStaffProtectionModel StaffProtectionList { get; set; }
         [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
        public string NINO { get; set; }        
        public string Message { get; set; }
    }

}