
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    public partial class OrganisationDC
    {
        [DataMember]
        public System.Guid? ImmediateParent
        {
            get;
            set;
        }
    }
}
