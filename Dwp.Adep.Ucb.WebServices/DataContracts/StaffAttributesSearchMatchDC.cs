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
    public partial class StaffAttributesSearchMatchDC
    {
    
    	[DataMember]
        public System.Guid Code
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public System.Guid StaffCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public string Staff
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public System.Guid ApplicationCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public string Application
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public System.Guid ApplicationAttributeCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public string ApplicationAttribute
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public string LookupValue
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public bool IsActive
        {
    	    get;
            set;
        }
    }
}
