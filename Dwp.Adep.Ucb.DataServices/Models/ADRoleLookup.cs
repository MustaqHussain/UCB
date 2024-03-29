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
    public partial class ADRoleLookup : IAuditable, IActiveAware 
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
    
        public virtual string ADGroup
        {
            get;
            set;
        }
    
        public virtual System.Guid RoleCode
        {
            get { return _roleCode; }
            set
            {
                if (_roleCode != value)
                {
                    if (Role != null && Role.Code != value)
                    {
                        Role = null;
                    }
                    _roleCode = value;
                }
            }
        }
        private System.Guid _roleCode;
    
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
    
        public virtual Role Role
        {
            get { return _role; }
            set
            {
                if (!ReferenceEquals(_role, value))
                {
                    var previousValue = _role;
                    _role = value;
                    FixupRole(previousValue);
                }
            }
        }
        private Role _role;
    
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
    
        private void FixupRole(Role previousValue)
        {
            if (previousValue != null && previousValue.ADRoleLookup.Contains(this))
            {
                previousValue.ADRoleLookup.Remove(this);
            }
    
            if (Role != null)
            {
                if (!Role.ADRoleLookup.Contains(this))
                {
                    Role.ADRoleLookup.Add(this);
                }
                if (RoleCode != Role.Code)
                {
                    RoleCode = Role.Code;
                }
            }
        }
    
        private void FixupOrganisation(Organisation previousValue)
        {
            if (previousValue != null && previousValue.ADRoleLookup.Contains(this))
            {
                previousValue.ADRoleLookup.Remove(this);
            }
    
            if (Organisation != null)
            {
                if (!Organisation.ADRoleLookup.Contains(this))
                {
                    Organisation.ADRoleLookup.Add(this);
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
