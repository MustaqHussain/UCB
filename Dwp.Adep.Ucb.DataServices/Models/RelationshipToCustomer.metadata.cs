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

namespace Dwp.Adep.Ucb.DataServices.Models
{
    [MetadataTypeAttribute(typeof(RelationshipToCustomer.RelationshipToCustomerMetadata))]
    public partial class RelationshipToCustomer : IRelationshipToCustomer
    {
    	public partial class RelationshipToCustomerMetadata
    	{
    		[Key]
    		public System.Guid Code{get; set;}
    	
    		[Association("FK_Customer_RelationshipToCustomer", "Code", "RelationshipToCustomerCode", IsForeignKey = false)]
    		public virtual ICollection<Customer> Customer
    		{get; set;}
    
    		}
    }
}
