using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace UcbWeb.Models
{
    public partial class SiteModel : BaseModel
    {


        public string NameAndActiveFlag
        {
            get { return SiteName + " " + (IsActive == true ? "" : "(Inactive)"); }
        }
    }
}