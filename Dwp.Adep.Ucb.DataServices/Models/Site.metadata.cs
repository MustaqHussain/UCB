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
    [MetadataTypeAttribute(typeof(Site.SiteMetadata))]
    public partial class Site : ISite
    {
    	public partial class SiteMetadata
    	{
    		[Key]
    		public System.Guid Code{get; set;}
    	
    		[Association("FK_SiteStaff_Site", "Code", "SiteCode", IsForeignKey = false)]
    		public virtual ICollection<SiteStaff> SiteStaff
    		{get; set;}
    
    	
    		[Association("FK_Site_Organisation", "OrganisationCode", "Code", IsForeignKey = true)]
    		public virtual Organisation Organisation
    		{get; set;}
    
    	
    		[Association("FK_Incident_Site", "Code", "SiteCode", IsForeignKey = false)]
    		public virtual ICollection<Incident> Incident
    		{get; set;}
    
    	
    		[Association("FK_Incident_Site1", "Code", "StaffMemberHomeOfficeSiteCode", IsForeignKey = false)]
    		public virtual ICollection<Incident> Incident1
    		{get; set;}
    
    		}
    }
}
