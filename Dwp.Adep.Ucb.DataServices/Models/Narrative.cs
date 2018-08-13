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
    public partial class Narrative : IAuditable
    {
        #region Primitive Properties
    
        public virtual System.Guid Code
        {
            get;
            set;
        }
    
        public virtual string NarrativeType
        {
            get;
            set;
        }
    
        public virtual string NarrativeDescription
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
    
        public virtual ICollection<Incident> Incident
        {
            get
            {
                if (_incident == null)
                {
                    var newCollection = new FixupCollection<Incident>();
                    newCollection.CollectionChanged += FixupIncident;
                    _incident = newCollection;
                }
                return _incident;
            }
            set
            {
                if (!ReferenceEquals(_incident, value))
                {
                    var previousValue = _incident as FixupCollection<Incident>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupIncident;
                    }
                    _incident = value;
                    var newValue = value as FixupCollection<Incident>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupIncident;
                    }
                }
            }
        }
        private ICollection<Incident> _incident;
    
        public virtual ICollection<Incident> Incident1
        {
            get
            {
                if (_incident1 == null)
                {
                    var newCollection = new FixupCollection<Incident>();
                    newCollection.CollectionChanged += FixupIncident1;
                    _incident1 = newCollection;
                }
                return _incident1;
            }
            set
            {
                if (!ReferenceEquals(_incident1, value))
                {
                    var previousValue = _incident1 as FixupCollection<Incident>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupIncident1;
                    }
                    _incident1 = value;
                    var newValue = value as FixupCollection<Incident>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupIncident1;
                    }
                }
            }
        }
        private ICollection<Incident> _incident1;
    
        public virtual ICollection<Incident> Incident2
        {
            get
            {
                if (_incident2 == null)
                {
                    var newCollection = new FixupCollection<Incident>();
                    newCollection.CollectionChanged += FixupIncident2;
                    _incident2 = newCollection;
                }
                return _incident2;
            }
            set
            {
                if (!ReferenceEquals(_incident2, value))
                {
                    var previousValue = _incident2 as FixupCollection<Incident>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupIncident2;
                    }
                    _incident2 = value;
                    var newValue = value as FixupCollection<Incident>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupIncident2;
                    }
                }
            }
        }
        private ICollection<Incident> _incident2;
    
        public virtual ICollection<Incident> Incident3
        {
            get
            {
                if (_incident3 == null)
                {
                    var newCollection = new FixupCollection<Incident>();
                    newCollection.CollectionChanged += FixupIncident3;
                    _incident3 = newCollection;
                }
                return _incident3;
            }
            set
            {
                if (!ReferenceEquals(_incident3, value))
                {
                    var previousValue = _incident3 as FixupCollection<Incident>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupIncident3;
                    }
                    _incident3 = value;
                    var newValue = value as FixupCollection<Incident>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupIncident3;
                    }
                }
            }
        }
        private ICollection<Incident> _incident3;
    
        public virtual ICollection<Incident> Incident4
        {
            get
            {
                if (_incident4 == null)
                {
                    var newCollection = new FixupCollection<Incident>();
                    newCollection.CollectionChanged += FixupIncident4;
                    _incident4 = newCollection;
                }
                return _incident4;
            }
            set
            {
                if (!ReferenceEquals(_incident4, value))
                {
                    var previousValue = _incident4 as FixupCollection<Incident>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= FixupIncident4;
                    }
                    _incident4 = value;
                    var newValue = value as FixupCollection<Incident>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += FixupIncident4;
                    }
                }
            }
        }
        private ICollection<Incident> _incident4;

        #endregion
        #region Association Fixup
    
        private void FixupIncident(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Incident item in e.NewItems)
                {
                    item.Narrative = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Incident item in e.OldItems)
                {
                    if (ReferenceEquals(item.Narrative, this))
                    {
                        item.Narrative = null;
                    }
                }
            }
        }
    
        private void FixupIncident1(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Incident item in e.NewItems)
                {
                    item.Narrative1 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Incident item in e.OldItems)
                {
                    if (ReferenceEquals(item.Narrative1, this))
                    {
                        item.Narrative1 = null;
                    }
                }
            }
        }
    
        private void FixupIncident2(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Incident item in e.NewItems)
                {
                    item.Narrative2 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Incident item in e.OldItems)
                {
                    if (ReferenceEquals(item.Narrative2, this))
                    {
                        item.Narrative2 = null;
                    }
                }
            }
        }
    
        private void FixupIncident3(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Incident item in e.NewItems)
                {
                    item.Narrative3 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Incident item in e.OldItems)
                {
                    if (ReferenceEquals(item.Narrative3, this))
                    {
                        item.Narrative3 = null;
                    }
                }
            }
        }
    
        private void FixupIncident4(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Incident item in e.NewItems)
                {
                    item.Narrative4 = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (Incident item in e.OldItems)
                {
                    if (ReferenceEquals(item.Narrative4, this))
                    {
                        item.Narrative4 = null;
                    }
                }
            }
        }

        #endregion
    }
}
