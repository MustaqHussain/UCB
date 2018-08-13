using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UcbWeb.Models
{
    public partial class CustomerModel : BaseModel
    {
        //Fields for read-only version of incident screen
        public string IsCustomerReportedDescription
        {
            get
            {
                return IsCustomerReported? "Customer":"Other";

            }

        }
        public string RelationshipToCustomerDescription { get; set; }

    }
}