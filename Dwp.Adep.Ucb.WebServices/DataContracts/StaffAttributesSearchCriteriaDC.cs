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
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public partial class StaffAttributesSearchCriteriaDC
    {
    
    	[DataMember]
        public virtual System.Guid StaffCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Staff
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual System.Guid ApplicationCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Application
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual System.Guid ApplicationAttributeCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string ApplicationAttribute
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string LookupValue
        {
    	    get;
            set;
        }
    }
}
