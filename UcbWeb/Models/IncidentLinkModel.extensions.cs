using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace UcbWeb.Models
{
    public partial class IncidentLinkModel : BaseModel
    {
        public string Name { get; set; }
        public string NINO { get; set; }
        public string OtherPersonNINO { get; set; }
        public DateTime IncidentDate { get; set; }
        public string IncidentStatus { get; set; }
        public bool? IsImplementControlMeasures { get; set; }    
        public string Message { get; set; }
    }
}
