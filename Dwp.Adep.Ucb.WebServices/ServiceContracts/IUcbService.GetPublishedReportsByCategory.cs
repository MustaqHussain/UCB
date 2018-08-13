using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.ServiceModel;
using Dwp.Adep.Ucb.WebServices.DataContracts;
using Dwp.Adep.Ucb.WebServices.FaultContracts;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial interface IUcbService
    {
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        List<PublishedReportsByCategory> GetPublishedReportsByCategory(string currentUser, string user, string appID, string overrideID);

    }
}
