using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UcbWeb.Models
{
    public partial class StaffModel
    {
        public string FullName
        {
            get { return LastName + ", " + FirstName; }
        }
    }
}