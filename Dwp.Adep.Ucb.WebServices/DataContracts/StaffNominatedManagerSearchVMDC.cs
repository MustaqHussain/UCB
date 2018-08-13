using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public class StaffNominatedManagerSearchVMDC
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public bool IsNominatedManager { get; set; }

        [DataMember]
        public bool IsDeputyNominatedManager { get; set; }

        [DataMember]
        public List<StaffDC> MatchList { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}