using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using Dwp.Adep.Ucb.WebServices.FaultContracts;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial interface IUcbService
    {
        [FaultContract(typeof(DataConcurrencyFault))]
        [FaultContract(typeof(DataIntegrityFault))]
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        void DeleteAttachmentAndData(string currentUser, string user, string appID, string overrideID, string code, byte[] lockID);

       
    }
}