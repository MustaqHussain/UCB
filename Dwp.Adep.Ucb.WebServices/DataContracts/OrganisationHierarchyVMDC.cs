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
    public partial class OrganisationHierarchyVMDC
    {
    	[DataMember]
        public OrganisationHierarchyDC OrganisationHierarchyItem { get; set;}
    
    	[DataMember]
        public List<OrganisationHierarchyDC> OrganisationHierarchyList { get; set;}
    
    	[DataMember]
    	public List<OrganisationDC> AncestorOrganisationList { get; set; }
    
    	[DataMember]
    	public List<OrganisationDC> OrganisationList { get; set; }
    
    	[DataMember]
    	public string Message { get; set; }
    }
}