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
    public partial class ApplicationSearchCriteriaDC
    {
    
    	[DataMember]
        public virtual string ApplicationName
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Location
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string Description
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual bool IsSpecificOrganisationAccessRequired
        {
    	    get;
            set;
        }
    }
}
