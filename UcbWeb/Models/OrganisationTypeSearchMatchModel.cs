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
    public partial class OrganisationTypeSearchMatchModel
    {
    
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
        public virtual string Name
        {
    	    get;
            set;
        }
    
        public virtual int LevelNumber
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
    
        public virtual Nullable<System.Guid> ParentOrganisationTypeCode
        {
    	    get;
            set;
        }
    
        public virtual string ParentOrganisationType
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
