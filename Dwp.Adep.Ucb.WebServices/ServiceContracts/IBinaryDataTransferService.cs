using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dwp.Adep.Ucb.WebServices.FaultContracts;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBinaryDataTransferService" in both code and config file together.
    [ServiceContract]
    public interface IBinaryDataTransferService
    {
        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        UploadAttachmentResponse UploadAttachment(UploadAttachmentRequest request);

        [FaultContract(typeof(ServiceErrorFault))]
        [OperationContract]
        DownloadAttachmentResponse DownloadAttachment(DownloadAttachmentRequest request);
    }
}
