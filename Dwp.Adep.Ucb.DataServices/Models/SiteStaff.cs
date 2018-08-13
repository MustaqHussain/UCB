//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace Dwp.Adep.Ucb.DataServices.Models
{
    public partial class SiteStaff : IAuditable
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual System.Guid SiteCode
        {
            get { return _siteCode; }
            set
            {
                if (_siteCode != value)
                {
                    if (Site != null && Site.Code != value)
                    {
                        Site = null;
                    }
                    _siteCode = value;
                }
            }
        }
        private System.Guid _siteCode;
    
        public virtual System.Guid StaffCode
        {
            get { return _staffCode; }
            set
            {
                if (_staffCode != value)
                {
                    if (Staff != null && Staff.Code != value)
                    {
                        Staff = null;
                    }
                    _staffCode = value;
                }
            }
        }
        private System.Guid _staffCode;
    
        public virtual string Responsibility
        {
            get;
            set;
        }
    
        public virtual byte[] RowIdentifier
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual Staff Staff
        {
            get { return _staff; }
            set
            {
                if (!ReferenceEquals(_staff, value))
                {
                    var previousValue = _staff;
                    _staff = value;
                    FixupStaff(previousValue);
                }
            }
        }
        private Staff _staff;
    
        public virtual Site Site
        {
            get { return _site; }
            set
            {
                if (!ReferenceEquals(_site, value))
                {
                    var previousValue = _site;
                    _site = value;
                    FixupSite(previousValue);
                }
            }
        }
        private Site _site;

        #endregion
        #region Association Fixup
    
        private void FixupStaff(Staff previousValue)
        {
            if (previousValue != null && previousValue.SiteStaff.Contains(this))
            {
                previousValue.SiteStaff.Remove(this);
            }
    
            if (Staff != null)
            {
                if (!Staff.SiteStaff.Contains(this))
                {
                    Staff.SiteStaff.Add(this);
                }
                if (StaffCode != Staff.Code)
                {
                    StaffCode = Staff.Code;
                }
            }
        }
    
        private void FixupSite(Site previousValue)
        {
            if (previousValue != null && previousValue.SiteStaff.Contains(this))
            {
                previousValue.SiteStaff.Remove(this);
            }
    
            if (Site != null)
            {
                if (!Site.SiteStaff.Contains(this))
                {
                    Site.SiteStaff.Add(this);
                }
                if (SiteCode != Site.Code)
                {
                    SiteCode = Site.Code;
                }
            }
        }

        #endregion
    }
}
