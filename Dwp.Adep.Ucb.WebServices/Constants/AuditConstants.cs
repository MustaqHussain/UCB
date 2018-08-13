using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dwp.Adep.Ucb.WebServices.Constants
{
    /// <summary>
    /// Class for audit contants
    /// </summary>
    public static class AuditConstants
    {
        /// <summary>
        /// The NINO constant
        /// </summary>
        public const string NINO_PROPERTY = "NINO"; 

        /// <summary>
        /// The Incident ID constant
        /// </summary>
        public const string INCIDENT_ID_PROPERTY = "IncidentID";

        /// <summary>
        /// The Code constant
        /// </summary>
        public const string CODE_PROPERTY = "Code";        

        /// <summary>
        /// The view audit action constant
        /// </summary>
        public const string VIEW_ACTION = "View";

        /// <summary>
        /// The search audit action constant
        /// </summary>
        public const string SEARCH_ACTION = "Search"; 

        /// <summary>
        /// The audit text for searching  fro a NINO from the staff protection search page
        /// </summary>
        public const string AUDIT_TEXT_STAFF_PROTECTION_NINO_SEARCH = "Staff protection search";        

        /// <summary>
        /// The audit text for viewing an incident (third party referral or incident)
        /// </summary>
        public const string AUDIT_TEXT_VIEW_INCIDENTREFERALL = "View {0}";
        
    }
}