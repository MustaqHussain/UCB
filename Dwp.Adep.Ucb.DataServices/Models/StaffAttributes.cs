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
    public partial class StaffAttributes : IAuditable, IActiveAware 
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual System.Guid SecurityLabel
        {
            get { return _securityLabel; }
            set
            {
                if (_securityLabel != value)
                {
                    if (Organisation != null && Organisation.Code != value)
                    {
                        Organisation = null;
                    }
                    _securityLabel = value;
                }
            }
        }
        private System.Guid _securityLabel;
    
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
    
        public virtual System.Guid ApplicationCode
        {
            get { return _applicationCode; }
            set
            {
                if (_applicationCode != value)
                {
                    if (Application != null && Application.Code != value)
                    {
                        Application = null;
                    }
                    _applicationCode = value;
                }
            }
        }
        private System.Guid _applicationCode;
    
        public virtual System.Guid ApplicationAttributeCode
        {
            get { return _applicationAttributeCode; }
            set
            {
                if (_applicationAttributeCode != value)
                {
                    if (ApplicationAttribute != null && ApplicationAttribute.Code != value)
                    {
                        ApplicationAttribute = null;
                    }
                    _applicationAttributeCode = value;
                }
            }
        }
        private System.Guid _applicationAttributeCode;
    
        public virtual string LookupValue
        {
            get;
            set;
        }
    
        public virtual bool IsActive
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
    
        public virtual Application Application
        {
            get { return _application; }
            set
            {
                if (!ReferenceEquals(_application, value))
                {
                    var previousValue = _application;
                    _application = value;
                    FixupApplication(previousValue);
                }
            }
        }
        private Application _application;
    
        public virtual ApplicationAttribute ApplicationAttribute
        {
            get { return _applicationAttribute; }
            set
            {
                if (!ReferenceEquals(_applicationAttribute, value))
                {
                    var previousValue = _applicationAttribute;
                    _applicationAttribute = value;
                    FixupApplicationAttribute(previousValue);
                }
            }
        }
        private ApplicationAttribute _applicationAttribute;
    
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
    
        public virtual Organisation Organisation
        {
            get { return _organisation; }
            set
            {
                if (!ReferenceEquals(_organisation, value))
                {
                    var previousValue = _organisation;
                    _organisation = value;
                    FixupOrganisation(previousValue);
                }
            }
        }
        private Organisation _organisation;

        #endregion
        #region Association Fixup
    
        private void FixupApplication(Application previousValue)
        {
            if (previousValue != null && previousValue.StaffAttributes.Contains(this))
            {
                previousValue.StaffAttributes.Remove(this);
            }
    
            if (Application != null)
            {
                if (!Application.StaffAttributes.Contains(this))
                {
                    Application.StaffAttributes.Add(this);
                }
                if (ApplicationCode != Application.Code)
                {
                    ApplicationCode = Application.Code;
                }
            }
        }
    
        private void FixupApplicationAttribute(ApplicationAttribute previousValue)
        {
            if (previousValue != null && previousValue.StaffAttributes.Contains(this))
            {
                previousValue.StaffAttributes.Remove(this);
            }
    
            if (ApplicationAttribute != null)
            {
                if (!ApplicationAttribute.StaffAttributes.Contains(this))
                {
                    ApplicationAttribute.StaffAttributes.Add(this);
                }
                if (ApplicationAttributeCode != ApplicationAttribute.Code)
                {
                    ApplicationAttributeCode = ApplicationAttribute.Code;
                }
            }
        }
    
        private void FixupStaff(Staff previousValue)
        {
            if (previousValue != null && previousValue.StaffAttributes.Contains(this))
            {
                previousValue.StaffAttributes.Remove(this);
            }
    
            if (Staff != null)
            {
                if (!Staff.StaffAttributes.Contains(this))
                {
                    Staff.StaffAttributes.Add(this);
                }
                if (StaffCode != Staff.Code)
                {
                    StaffCode = Staff.Code;
                }
            }
        }
    
        private void FixupOrganisation(Organisation previousValue)
        {
            if (previousValue != null && previousValue.StaffAttributes.Contains(this))
            {
                previousValue.StaffAttributes.Remove(this);
            }
    
            if (Organisation != null)
            {
                if (!Organisation.StaffAttributes.Contains(this))
                {
                    Organisation.StaffAttributes.Add(this);
                }
                if (SecurityLabel != Organisation.Code)
                {
                    SecurityLabel = Organisation.Code;
                }
            }
        }

        #endregion
    }
}
