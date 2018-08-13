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
    public partial class OrganisationType : IAuditable, IActiveAware 
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual string Name
        {
            get;
            set;
        }
    
        public virtual int LevelNumber
        {
            get;
            set;
        }
    
        public virtual System.Guid OrganisationTypeGroupCode
        {
            get { return _organisationTypeGroupCode; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_organisationTypeGroupCode != value)
                    {
                        if (OrganisationTypeGroup != null && OrganisationTypeGroup.Code != value)
                        {
                            OrganisationTypeGroup = null;
                        }
                        _organisationTypeGroupCode = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private System.Guid _organisationTypeGroupCode;
    
        public virtual Nullable<System.Guid> ParentOrganisationTypeCode
        {
            get { return _parentOrganisationTypeCode; }
            set
            {
                try
                {
                    _settingFK = true;
                    if (_parentOrganisationTypeCode != value)
                    {
                        if (OrganisationType2 != null && OrganisationType2.Code != value)
                        {
                            OrganisationType2 = null;
                        }
                        _parentOrganisationTypeCode = value;
                    }
                }
                finally
                {
                    _settingFK = false;
                }
            }
        }
        private Nullable<System.Guid> _parentOrganisationTypeCode;
    
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
    
        public virtual OrganisationTypeGroup OrganisationTypeGroup
        {
            get { return _organisationTypeGroup; }
            set
            {
                if (!ReferenceEquals(_organisationTypeGroup, value))
                {
                    var previousValue = _organisationTypeGroup;
                    _organisationTypeGroup = value;
                    FixupOrganisationTypeGroup(previousValue);
                }
            }
        }
        private OrganisationTypeGroup _organisationTypeGroup;
    
        public virtual ICollection<OrganisationType> OrganisationType1
        {
            get
            {
                if (_organisationType1 == null)
                {
                    var newCollection = new FixupCollection<OrganisationType>();
                    newCollection.CollectionChanged += FixupOrganisationType1;
                    _organisationType1 = newCollection;
                }
                return _organisationType1;
            }
            set
            {
                if (!ReferenceEquals(_organisationType1, value))
                {
                    var previousValue = _organisationType1 as FixupCollection<OrganisationType>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOrganisationType1;
                    }
                    _organisationType1 = value;
                    var newValue = value as FixupCollection<OrganisationType>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOrganisationType1;
                    }
                }
            }
        }
        private ICollection<OrganisationType> _organisationType1;
    
        public virtual OrganisationType OrganisationType2
        {
            get { return _organisationType2; }
            set
            {
                if (!ReferenceEquals(_organisationType2, value))
                {
                    var previousValue = _organisationType2;
                    _organisationType2 = value;
                    FixupOrganisationType2(previousValue);
                }
            }
        }
        private OrganisationType _organisationType2;
    
        public virtual ICollection<Organisation> Organisation
        {
            get
            {
                if (_organisation == null)
                {
                    var newCollection = new FixupCollection<Organisation>();
                    newCollection.CollectionChanged += FixupOrganisation;
                    _organisation = newCollection;
                }
                return _organisation;
            }
            set
            {
                if (!ReferenceEquals(_organisation, value))
                {
                    var previousValue = _organisation as FixupCollection<Organisation>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupOrganisation;
                    }
                    _organisation = value;
                    var newValue = value as FixupCollection<Organisation>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupOrganisation;
                    }
                }
            }
        }
        private ICollection<Organisation> _organisation;

        #endregion
        #region Association Fixup
    
        private bool _settingFK = false;
    
        private void FixupOrganisationTypeGroup(OrganisationTypeGroup previousValue)
        {
            if (previousValue != null && previousValue.OrganisationType.Contains(this))
            {
                previousValue.OrganisationType.Remove(this);
            }
    
            if (OrganisationTypeGroup != null)
            {
                if (!OrganisationTypeGroup.OrganisationType.Contains(this))
                {
                    OrganisationTypeGroup.OrganisationType.Add(this);
                }
                if (OrganisationTypeGroupCode != OrganisationTypeGroup.Code)
                {
                    OrganisationTypeGroupCode = OrganisationTypeGroup.Code;
                }
            }
        }
    
        private void FixupOrganisationType2(OrganisationType previousValue)
        {
            if (previousValue != null && previousValue.OrganisationType1.Contains(this))
            {
                previousValue.OrganisationType1.Remove(this);
            }
    
            if (OrganisationType2 != null)
            {
                if (!OrganisationType2.OrganisationType1.Contains(this))
                {
                    OrganisationType2.OrganisationType1.Add(this);
                }
                if (ParentOrganisationTypeCode != OrganisationType2.Code)
                {
                    ParentOrganisationTypeCode = OrganisationType2.Code;
                }
            }
            else if (!_settingFK)
            {
                ParentOrganisationTypeCode = null;
            }
        }
    
        private void FixupOrganisationType1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (OrganisationType item in e.NewItems)
                {
                    item.OrganisationType2 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (OrganisationType item in e.OldItems)
                {
                    if (ReferenceEquals(item.OrganisationType2, this))
                    {
                        item.OrganisationType2 = null;
                    }
                }
            }
        }
    
        private void FixupOrganisation(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Organisation item in e.NewItems)
                {
                    item.OrganisationType = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Organisation item in e.OldItems)
                {
                    if (ReferenceEquals(item.OrganisationType, this))
                    {
                        item.OrganisationType = null;
                    }
                }
            }
        }

        #endregion
    }
}
