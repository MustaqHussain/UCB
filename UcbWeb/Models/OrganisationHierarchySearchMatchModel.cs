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
    public partial class OrganisationHierarchySearchMatchModel
    {
    
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
        public virtual System.Guid AncestorOrganisationCode
        {
    	    get;
            set;
        }
    
        public virtual string AncestorOrganisation
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
    
        public virtual bool ImmediateParent
        {
    	    get;
            set;
        }
    
        public virtual Nullable<int> HopsBetweenOrgAndAncestor
        {
    	    get;
            set;
        }
    
        public virtual bool IsActive
        {
    	    get;
            set;
        }
    }
}
