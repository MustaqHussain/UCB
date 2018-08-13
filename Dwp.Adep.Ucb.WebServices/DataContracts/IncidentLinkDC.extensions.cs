using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    public partial class IncidentLinkDC
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NINO { get; set; }

        [DataMember]
        public string OtherPersonNINO { get; set; }

        [DataMember]
        public System.DateTime IncidentDate { get; set; }

        [DataMember]
        public string IncidentStatus { get; set; }

        [DataMember]
        public bool? IsImplementControlMeasures { get; set; }    
    }

}
