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
    public partial class AttachmentSearchCriteriaModel
    {
    
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
    
        public virtual string AttachmentType
        {
    	    get;
            set;
        }
    
        public virtual string Name
        {
    	    get;
            set;
        }
    
        public virtual System.DateTime LoadedDate
        {
    	    get;
            set;
        }
    
        public virtual string LoadedBy
        {
    	    get;
            set;
        }
    }
}
