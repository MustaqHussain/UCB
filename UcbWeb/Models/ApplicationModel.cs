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

namespace UcbWeb.Models
{
    public partial class ApplicationModel : BaseModel
    {
    
        public virtual System.Guid Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private System.Guid _code;
    
        public virtual string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }
        private string _applicationName;
    
        public virtual string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        private string _location;
    
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private string _description;
    
        public virtual bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        private bool _isActive;
    
        public virtual bool IsSpecificOrganisationAccessRequired
        {
            get { return _isSpecificOrganisationAccessRequired; }
            set { _isSpecificOrganisationAccessRequired = value; }
        }
        private bool _isSpecificOrganisationAccessRequired;
    
        public virtual byte[] RowIdentifier
        {
            get { return _rowIdentifier; }
            set { _rowIdentifier = value; }
        }
        private byte[] _rowIdentifier;
    }
}
