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
    [MetadataTypeAttribute(typeof(IncidentCategory.IncidentCategoryMetadata))]
    public partial class IncidentCategory : IIncidentCategory
    {
    	public partial class IncidentCategoryMetadata
    	{
    		[Key]
    		public System.Guid Code{get; set;}
    	
    		[Association("FK_IncidentType_IncidentCategory", "Code", "IncidentCategoryCode", IsForeignKey = false)]
    		public virtual ICollection<IncidentType> IncidentType
    		{get; set;}
    
    	
    		[Association("FK_Incident_IncidentCategory", "Code", "IncidentCategoryCode", IsForeignKey = false)]
    		public virtual ICollection<Incident> Incident
    		{get; set;}
    
    		}
    }
}
