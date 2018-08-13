
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public partial class IntranetStaffProtectionResult
    {
        [DataMember]
        public string ControlMeasures { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string BannedOfficeOfficeEndDate { get; set; }

        [DataMember]
        public string NamedOfficerNameContact { get; set; }

        [DataMember]
        public string RelationShip { get; set; }

    }
}