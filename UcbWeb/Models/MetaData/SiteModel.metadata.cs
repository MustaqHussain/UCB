using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using Dwp.Adep.Ucb.ResourceLibrary;
using UcbWeb.DataAnnotation;

namespace UcbWeb.Models
{
    [MetadataTypeAttribute(typeof(SiteModel.SiteModelMetadata))]
    public partial class SiteModel
    {
        public partial class SiteModelMetadata
        {
            [Key]
            [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_SITE_CODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_SITE_CODE", ResourceType = typeof(Resources))]
            public System.Guid Code { get; set; }

            [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_SITE_ORGANISATIONCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_SITE_ORGANISATIONCODE", ResourceType = typeof(Resources))]
            public System.Guid OrganisationCode { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [Tooltip("TOOLTIP_SITE_SITENAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_SITE_SITENAME", ResourceType = typeof(Resources))]
            public string SiteName { get; set; }

            [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [StringLength(50)]
            [RegularExpression("^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z])))) {0,1}[0-9][A-Za-z]{2})$",ErrorMessageResourceName = "VAL_POSTCODE", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_SITE_POSTCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_SITE_POSTCODE", ResourceType = typeof(Resources))]
            public string PostCode { get; set; }

            [Tooltip("TOOLTIP_SITE_ISACTIVE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_SITE_ISACTIVE", ResourceType = typeof(Resources))]
            public bool IsActive { get; set; }

        }
    }
}