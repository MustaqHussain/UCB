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
    public partial class OrganisationTypeSearchCriteriaDC
    {
    
    	[DataMember]
        public virtual string Name
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual int LevelNumber
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual System.Guid OrganisationTypeGroupCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string OrganisationTypeGroup
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual Nullable<System.Guid> ParentOrganisationTypeCode
        {
    	    get;
            set;
        }
    
    	[DataMember]
        public virtual string ParentOrganisationType
        {
    	    get;
            set;
        }
    }
}
