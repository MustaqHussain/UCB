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
    public partial class ApplicationOrganisationTypeGroupSearchCriteriaModel
    {
    
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
    
        public virtual System.Guid OrganisationTypeGroupCode
        {
    	    get;
            set;
        }
    
        public virtual string OrganisationTypeGroup
        {
    	    get;
            set;
        }
    
        public virtual System.Guid RootOrganisationForApplicationCode
        {
    	    get;
            set;
        }
    
        public virtual string RootOrganisationForApplication
        {
    	    get;
            set;
        }
    }
}
