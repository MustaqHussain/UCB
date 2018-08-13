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
    [MetadataTypeAttribute(typeof(AttachmentModel.AttachmentModelMetadata))]
    public partial class AttachmentModel
    {
    	public partial class AttachmentModelMetadata
    	{
    		[Key]
    		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[Tooltip("TOOLTIP_ATTACHMENT_CODE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_ATTACHMENT_CODE", ResourceType=typeof(Resources))]
    		public System.Guid Code {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[Tooltip("TOOLTIP_ATTACHMENT_INCIDENTCODE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_ATTACHMENT_INCIDENTCODE", ResourceType=typeof(Resources))]
    		public System.Guid IncidentCode {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[StringLength(50)]
    		[Tooltip("TOOLTIP_ATTACHMENT_ATTACHMENTTYPE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_ATTACHMENT_ATTACHMENTTYPE", ResourceType=typeof(Resources))]
    		public string AttachmentType {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[StringLength(500)]
    		[Tooltip("TOOLTIP_ATTACHMENT_NAME", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_ATTACHMENT_NAME", ResourceType=typeof(Resources))]
    		public string Name {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[Tooltip("TOOLTIP_ATTACHMENT_LOADEDDATE", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_ATTACHMENT_LOADEDDATE", ResourceType=typeof(Resources))]
    		public System.DateTime LoadedDate {get; set;}
    
      		[Required(ErrorMessageResourceName = "VAL_REQUIRED", ErrorMessageResourceType = typeof(Resources))]
    		[StringLength(50)]
    		[Tooltip("TOOLTIP_ATTACHMENT_LOADEDBY", ResourceType=typeof(Resources))]
    		[Display(Name="LABEL_ATTACHMENT_LOADEDBY", ResourceType=typeof(Resources))]
    		public string LoadedBy {get; set;}
    
        }
    }
}