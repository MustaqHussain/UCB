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
    public partial class StaffAttributesVMDC
    {
    	[DataMember]
        public StaffAttributesDC StaffAttributesItem { get; set;}
    
    	[DataMember]
        public List<StaffAttributesDC> StaffAttributesList { get; set;}
    
    	[DataMember]
    	public List<StaffDC> StaffList { get; set; }
    
    	[DataMember]
    	public List<ApplicationDC> ApplicationList { get; set; }
    
    	[DataMember]
    	public List<ApplicationAttributeDC> ApplicationAttributeList { get; set; }
    
    	[DataMember]
    	public string Message { get; set; }
    }
}
