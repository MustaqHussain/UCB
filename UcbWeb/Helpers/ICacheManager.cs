using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UcbWeb.Helpers
{
    public partial interface ICacheManager
    {
        IncidentLookupListsCacheObject IncidentListCache { get; }
    }
}