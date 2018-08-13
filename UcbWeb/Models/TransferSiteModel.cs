using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UcbWeb.Models
{
    public partial class TransferSiteModel : BaseModel 
    {
        public virtual System.Guid Code { get; set; }

        public virtual string SiteName { get; set; }

        public virtual string NominatedManager { get; set; }

        public virtual string DeputyNominatedManagers { get; set; }
    }
}