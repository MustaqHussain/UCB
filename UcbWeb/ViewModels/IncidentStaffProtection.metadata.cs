using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using UcbWeb.DataAnnotation;
using Dwp.Adep.Ucb.ResourceLibrary;

namespace UcbWeb.ViewModels
{
    [MetadataTypeAttribute(typeof(IntranetStaffProtectionVM.IntranetStaffProtectionVMMetadata))]
    public partial class IntranetStaffProtectionVM
    {
        public partial class IntranetStaffProtectionVMMetadata
        {
            [StringLength(9)]
            [Tooltip("TOOLTIP_CUSTOMER_NINO", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_NINO", ResourceType = typeof(Resources))]
            [RegularExpression(@"^(?!GB)(?!BG)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)([A-CEGHJ-NOPR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[ABCD\s]{0,1})$", ErrorMessageResourceName = "VAL_NI_NUMBER_INVALID", ErrorMessageResourceType = typeof(Resources))]
            public string NINO { get; set; }
        }        
    }
}