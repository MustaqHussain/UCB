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
    public partial class OrganisationDC
    {
    
    	[DataMember]
        public System.Guid Code
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public int ID
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public string Name
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public System.Guid OrganisationTypeCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public string HEO
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public Nullable<System.DateTime> DateDeleted
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
    
    	[DataMember]
        public byte[] RowIdentifier
        {
    	    get;
            set;
        }
    }
}
