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
    public partial class ApplicationOrganisationTypeGroupSearchVM
    {
        public ApplicationOrganisationTypeGroupSearchCriteriaModel SearchCriteria { get; set;}
    
        public List<ApplicationOrganisationTypeGroupSearchMatchModel> MatchList { get; set;}
    
        public int TotalRows { get; set; }
    
    	public int PageSize { get; set; }
    
    	public int PageNumber { get; set; }
    	
    	public string Message { get; set; }
    }
}
