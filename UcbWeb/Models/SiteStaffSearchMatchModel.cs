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
    public partial class SiteStaffSearchMatchModel
    {
    
        public virtual System.Guid Code
        {
    	    get;
            set;
        }
    
        public virtual System.Guid SiteCode
        {
    	    get;
            set;
        }
    
        public virtual string Site
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
    
        public virtual string Responsibility
        {
    	    get;
            set;
        }
    }
}
