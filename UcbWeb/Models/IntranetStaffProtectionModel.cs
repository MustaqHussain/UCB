using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace UcbWeb.Models
{
    public partial class IntranetStaffProtectionModel
    {

        public virtual string ControlMeasures
        {
            get;
            set;
        }

        public virtual string FirstName
        {
            get;
            set;
        }

        public virtual string BannedOfficeOfficeEndDate
        {
            get;
            set;
        }

        public virtual string NamedOfficerNameContact
        {
            get;
            set;
        }
        
        public virtual string RelationShip
        {
            get;
            set;
        }
    }
}
