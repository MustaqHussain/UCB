using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace UcbWeb.Models
{
    public partial class OrganisationModel : BaseModel
    {
        public System.Guid? ImmediateParent
        {
            get;
            set;
        }

        public string NameAndActiveFlag
        {
            get { return Name + " " + (IsActive == true ? "" : "(Inactive)"); }
        }
    }
}