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
    public partial class IncidentSystemMarkedSearchMatchModel
    {
    
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
        public virtual System.Guid IncidentCode
        {
    	    get;
            set;
        }
    
        public virtual string Incident
        {
    	    get;
            set;
        }
    
        public virtual System.Guid SystemMarkedCode
        {
    	    get;
            set;
        }
    
        public virtual string SystemMarked
        {
    	    get;
            set;
        }
    }
}