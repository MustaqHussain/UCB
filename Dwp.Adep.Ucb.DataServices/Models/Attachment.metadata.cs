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

namespace Dwp.Adep.Ucb.DataServices.Models
{
    [MetadataTypeAttribute(typeof(Attachment.AttachmentMetadata))]
    public partial class Attachment : IAttachment
    {
    	public partial class AttachmentMetadata
    	{
    		[Key]
    		public System.Guid Code{get; set;}
    	
    		[Association("FK_AttachmentData_Attachment", "Code", "AttachmentCode", IsForeignKey = false)]
    		public virtual ICollection<AttachmentData> AttachmentData
    		{get; set;}
    
    	
    		[Association("FK_Attachment_Incident", "IncidentCode", "Code", IsForeignKey = true)]
    		public virtual Incident Incident
    		{get; set;}
    
    		}
    }
}
