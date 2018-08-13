using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Dwp.Adep.Ucb.WebServices.DataContracts;
using Dwp.Adep.Ucb.WebServices.FaultContracts;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial interface IUcbService
    {
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        List<SiteDC> GetAllSitesForLevelThreeOrganisation(string currentUser, string user, string appID, string overrideID, string businessAreaCode);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        StaffDC GetNominatedManagerForSite(string currentUser, string user, string appID, string overrideID, string siteCode);
    }
}