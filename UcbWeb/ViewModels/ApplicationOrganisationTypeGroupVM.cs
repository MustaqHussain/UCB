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
    public partial class ApplicationOrganisationTypeGroupVM
    {
    	public ApplicationOrganisationTypeGroupModel ApplicationOrganisationTypeGroupItem { get; set; }
    
    	public List<ApplicationModel> ApplicationList { get; set; }
    	public List<OrganisationTypeGroupModel> OrganisationTypeGroupList { get; set; }
    	public List<OrganisationModel> RootOrganisationForApplicationList { get; set; }
        public string Message { get; set; }
    
        public string IsDeleteConfirmed { get; set; }
        public string IsExitConfirmed { get; set; }
        public string IsNewConfirmed { get; set; }
    	public bool IsViewDirty { get; set; }
    
        public ApplicationOrganisationTypeGroupAccessContext AccessContext { get; set; }
    	
    }
    
    public enum ApplicationOrganisationTypeGroupAccessContext
    {
        Create,
        View,
        Edit
    }
}
