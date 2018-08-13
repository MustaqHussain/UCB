using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public partial class TransferSiteSearchVMDC
    {
        [DataMember]
        public TransferSiteSearchCriteriaDC SearchCriteria { get; set; }

        [DataMember]
        public List<TransferSiteDC> MatchList { get; set; }

        [DataMember]
        public int RecordCount { get; set; }

    }
}