using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public class TransferSiteDC
    {
        [DataMember]
        public System.Guid Code { get; set; }

        [DataMember]
        public string SiteName { get; set; }

        [DataMember]
        public string NominatedManager { get; set; }

        [DataMember]
        public string DeputyNominatedManagers { get; set; }

    }
}