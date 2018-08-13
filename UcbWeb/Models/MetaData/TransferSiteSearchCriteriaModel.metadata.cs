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
    [MetadataTypeAttribute(typeof(TransferSiteSearchCriteriaModel.TransferSiteSearchCriteriaModelMetaData))]
    public partial class TransferSiteSearchCriteriaModel
    {
        public partial class TransferSiteSearchCriteriaModelMetaData
        {
            [StringLength(50)]
            [Display(Name = "LABEL_TRANSFERSITESEARCH_SITENAME", ResourceType = typeof(Resources))]
            public string SiteName { get; set; }
        }

    }
}
