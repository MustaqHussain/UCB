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
    public partial class IncidentLinkModel : BaseModel
    {
    
        public virtual System.Guid Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private System.Guid _code;
    
        public virtual Nullable<System.Guid> IncidentCode
        {
            get { return _incidentCode; }
            set { _incidentCode = value; }
        }
        private Nullable<System.Guid> _incidentCode;
    
        public virtual Nullable<System.Guid> LinkedIncidentCode
        {
            get { return _linkedIncidentCode; }
            set { _linkedIncidentCode = value; }
        }
        private Nullable<System.Guid> _linkedIncidentCode;
    
        public virtual string CustomerName
        {
            get { return _customerName; }
            set { _customerName = value; }
        }
        private string _customerName;
    
        public virtual Nullable<int> IncidentId
        {
            get { return _incidentId; }
            set { _incidentId = value; }
        }
        private Nullable<int> _incidentId;
    
        public virtual byte[] RowIdentifier
        {
            get { return _rowIdentifier; }
            set { _rowIdentifier = value; }
        }
        private byte[] _rowIdentifier;
    }
}
