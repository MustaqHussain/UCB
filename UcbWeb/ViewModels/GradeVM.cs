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
using UcbWeb.Models;

namespace UcbWeb.ViewModels
{
    public partial class GradeVM
    {
    	public GradeModel GradeItem { get; set; }
    
        public string Message { get; set; }
    
        public string IsDeleteConfirmed { get; set; }
        public string IsExitConfirmed { get; set; }
        public string IsNewConfirmed { get; set; }
    	public bool IsViewDirty { get; set; }
    
        public GradeAccessContext AccessContext { get; set; }
    	
    }
    
    public enum GradeAccessContext
    {
        Create,
        View,
        Edit
    }
}
