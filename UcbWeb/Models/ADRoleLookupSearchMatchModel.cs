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
    public partial class ADRoleLookupSearchMatchModel
    {
    
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
        public virtual string ADGroup
        {
    	    get;
            set;
        }
    
        public virtual System.Guid RoleCode
        {
    	    get;
            set;
        }
    
        public virtual string Role
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
