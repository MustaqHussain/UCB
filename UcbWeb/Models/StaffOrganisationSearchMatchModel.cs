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

namespace UcbWeb.Models
{
    public partial class StaffOrganisationSearchMatchModel
    {
    
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
        public virtual System.Guid StaffCode
        {
    	    get;
            set;
        }
    
        public virtual string Staff
        {
    	    get;
            set;
        }
    
        public virtual System.Guid OrganisationCode
        {
    	    get;
            set;
        }
    
        public virtual string Organisation
        {
    	    get;
            set;
        }
    
        public virtual System.Guid ApplicationCode
        {
    	    get;
            set;
        }
    
        public virtual string Application
        {
    	    get;
            set;
        }
    
        public virtual bool IsDefault
        {
    	    get;
            set;
        }
    
        public virtual bool IsCurrent
        {
    	    get;
            set;
        }
    }
}
