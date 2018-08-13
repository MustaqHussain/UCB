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
    [MetadataTypeAttribute(typeof(NarrativeModel.NarrativeModelMetadata))]
    public partial class NarrativeModel
    {
        public partial class NarrativeModelMetadata
        {
            [Key]
             [Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
            [Tooltip("TOOLTIP_NARRATIVE_CODE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_NARRATIVE_CODE", ResourceType = typeof(Resources))]
            public System.Guid Code { get; set; }

            [StringLength(25)]
            [Tooltip("TOOLTIP_NARRATIVE_NARRATIVETYPE", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_NARRATIVE_NARRATIVETYPE", ResourceType = typeof(Resources))]
            public string NarrativeType { get; set; }

            [StringLength(3000)]
            [Tooltip("TOOLTIP_NARRATIVE_NARRATIVEDESCRIPTION", ResourceType = typeof(Resources))]
            [Display(Name = "LABEL_NARRATIVE_NARRATIVEDESCRIPTION", ResourceType = typeof(Resources))]
            public string NarrativeDescription { get; set; }

        }
    }
}