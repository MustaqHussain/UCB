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
    [MetadataTypeAttribute(typeof(ControlMeasure.ControlMeasureMetadata))]
    public partial class ControlMeasure : IControlMeasure
    {
    	public partial class ControlMeasureMetadata
    	{
    		[Key]
    		public System.Guid Code{get; set;}
    	
    		[Association("FK_IncidentControlMeasure_ControlMeasure", "Code", "ControlMeasureCode", IsForeignKey = false)]
    		public virtual ICollection<CustomerControlMeasure> CustomerControlMeasure
    		{get; set;}
    
    		}
    }
}