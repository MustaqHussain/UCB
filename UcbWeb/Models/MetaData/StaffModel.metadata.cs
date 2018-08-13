//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    [MetadataTypeAttribute(typeof(StaffModel.StaffModelMetadata))]
    public partial class StaffModel
    {
    	public partial class StaffModelMetadata
    	{
    		[Key]
    		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[Tooltip("TOOLTIP_STAFF_CODE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_STAFF_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[StringLength(8)]
    		[Tooltip("TOOLTIP_STAFF_STAFFNUMBER", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_STAFF_STAFFNUMBER", ResourceType=typeof(Resources))]
    		public string StaffNumber {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[StringLength(35)]
    		[Tooltip("TOOLTIP_STAFF_LASTNAME", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_STAFF_LASTNAME", ResourceType=typeof(Resources))]
    		public string LastName {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[StringLength(35)]
    		[Tooltip("TOOLTIP_STAFF_FIRSTNAME", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_STAFF_FIRSTNAME", ResourceType=typeof(Resources))]
    		public string FirstName {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[Tooltip("TOOLTIP_STAFF_GRADECODE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_STAFF_GRADECODE", ResourceType=typeof(Resources))]
    		public System.Guid GradeCode {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[Tooltip("TOOLTIP_STAFF_ISACTIVE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_STAFF_ISACTIVE", ResourceType=typeof(Resources))]
    		public bool IsActive {get; set;}
    
        }
    }
}