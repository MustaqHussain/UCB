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
    [MetadataTypeAttribute(typeof(CustomerModel.CustomerModelMetadata))]
    public partial class CustomerModel
    {
        public partial class CustomerModelMetadata
        {
            [Key]
             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_CUSTOMER_CODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_CODE", ResourceType = typeof(Resources))]
            public System.Guid Code { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_TITLE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_TITLE", ResourceType = typeof(Resources))]
            public string Title { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERTITLE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERTITLE", ResourceType = typeof(Resources))]
            [RequiredIf("Title", "Other")]
            public string OtherTitle { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_FIRSTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_FIRSTNAME", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string FirstName { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERNAMES", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERNAMES", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string OtherNames { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_LASTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_LASTNAME", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string LastName { get; set; }

            [StringLength(9)]
            [Tooltip("TOOLTIP_CUSTOMER_NINO", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_NINO", ResourceType = typeof(Resources))]
            [RegularExpression(@"^(?!GB)(?!BG)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)([A-CEGHJ-NOPR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[ABCD\s]{0,1})$", ErrorMessageResourceName = "VAL_NI_NUMBER_INVALID", ErrorMessageResourceType = typeof(Resources))]
            public string NINO { get; set; }

             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_CUSTOMER_ISCUSTOMERREPORTED", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_ISCUSTOMERREPORTED", ResourceType = typeof(Resources))]
            public bool IsCustomerReported { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERPERSONTITLE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERPERSONTITLE", ResourceType = typeof(Resources))]
            public string OtherPersonTitle { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERPERSONOTHERTITLE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERPERSONOTHERTITLE", ResourceType = typeof(Resources))]
            [RequiredIf("OtherPersonTitle","Other")]
            public string OtherPersonOtherTitle { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERPERSONFIRSTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERPERSONFIRSTNAME", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string OtherPersonFirstName { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERPERSONOTHERNAMES", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERPERSONOTHERNAMES", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string OtherPersonOtherNames { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERPERSONLASTNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERPERSONLASTNAME", ResourceType = typeof(Resources))]
            [RegularExpression("^[a-zA-Z0-9 .\'-]*$", ErrorMessageResourceName = "VAL_CHARS_NOT_ALLOWED", ErrorMessageResourceType = typeof(Resources))]
            public string OtherPersonLastName { get; set; }

            [StringLength(9)]
            [Tooltip("TOOLTIP_CUSTOMER_OTHERPERSONNINO", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_OTHERPERSONNINO", ResourceType = typeof(Resources))]
            [RegularExpression(@"^(?!GB)(?!BG)(?!NK)(?!KN)(?!TN)(?!NT)(?!ZZ)([A-CEGHJ-NOPR-TW-Z]{1}[A-CEGHJ-NPR-TW-Z]{1}[0-9]{6}[ABCD\s]{0,1})$", ErrorMessageResourceName = "VAL_NI_NUMBER_INVALID", ErrorMessageResourceType = typeof(Resources))]
            public string OtherPersonNINO { get; set; }

            [Tooltip("TOOLTIP_CUSTOMER_RELATIONSHIPTOCUSTOMERCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_RELATIONSHIPTOCUSTOMERCODE", ResourceType = typeof(Resources))]
            public Nullable<System.Guid> RelationshipToCustomerCode { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_HOUSENUMBERORNAME", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_HOUSENUMBERORNAME", ResourceType = typeof(Resources))]
            public string HouseNumberOrName { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_STREET", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_STREET", ResourceType = typeof(Resources))]
            public string Street { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_TOWN", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_TOWN", ResourceType = typeof(Resources))]
            public string Town { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_COUNTY", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_COUNTY", ResourceType = typeof(Resources))]
            public string County { get; set; }

            [StringLength(50)]
            [Tooltip("TOOLTIP_CUSTOMER_POSTCODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_CUSTOMER_POSTCODE", ResourceType = typeof(Resources))]
            [RegularExpression(@"^(GIR 0AA)|((([A-PR-UWYZ][0-9][0-9]?)|(([A-PR-UWYZ][A-HK-Y][0-9][0-9]?)|(([A-PR-UWYZ][0-9][A-HJKSTUW])|([A-PR-UWYZ][A-HK-Y][0-9][ABEHMNPRVWXY])))) [0-9][ABD-HJLNP-UW-Z]{2})$", ErrorMessageResourceName = "VAL_POSTCODE", ErrorMessageResourceType = typeof(Resources))]
            public string PostCode { get; set; }
            
        }
    }
}
