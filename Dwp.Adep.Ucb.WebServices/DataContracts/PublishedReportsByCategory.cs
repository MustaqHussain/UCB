
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Dwp.Adep.Ucb.DataServices.Models;

namespace Dwp.Adep.Ucb.WebServices.DataContracts
{
    [DataContract]
    public partial class PublishedReportsByCategory
    {

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public List<StandardReportDC> StandardReports { get; set; }

    }
}