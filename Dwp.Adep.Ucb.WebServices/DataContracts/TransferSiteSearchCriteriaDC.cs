using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public class TransferSiteSearchCriteriaDC
    {
        [DataMember]
        public virtual string SiteName
        {
            get;
            set;
        }
    }
}