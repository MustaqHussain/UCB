using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UcbWeb.Models;

namespace UcbWeb.ViewModels
{
    public class TransferSiteSearchVM
    {
        public TransferSiteSearchCriteriaModel SearchCriteria { get; set; }

        public List<TransferSiteModel> MatchList { get; set; }
    
        public int TotalRows { get; set; }
    
    	public int PageSize { get; set; }
    
    	public int PageNumber { get; set; }
    	
    	public string Message { get; set; }
    }
}