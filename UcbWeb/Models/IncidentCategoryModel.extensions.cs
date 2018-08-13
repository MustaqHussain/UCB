using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace UcbWeb.Models
{
    public partial class IncidentCategoryModel : BaseModel
    {
        public string DescriptionAndActiveFlag
        {
            get{return Description + " " + (IsActive? "" : "(Inactive)");}
        }
    }
}