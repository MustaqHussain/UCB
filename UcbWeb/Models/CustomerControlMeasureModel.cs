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
    public partial class CustomerControlMeasureModel : BaseModel
    {
    
        public virtual System.Guid Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private System.Guid _code;
    
        public virtual System.Guid CustomerCode
        {
            get { return _customerCode; }
            set { _customerCode = value; }
        }
        private System.Guid _customerCode;
    
        public virtual System.Guid ControlMeasureCode
        {
            get { return _controlMeasureCode; }
            set { _controlMeasureCode = value; }
        }
        private System.Guid _controlMeasureCode;
    
        public virtual byte[] RowIdentifier
        {
            get { return _rowIdentifier; }
            set { _rowIdentifier = value; }
        }
        private byte[] _rowIdentifier;
    }
}