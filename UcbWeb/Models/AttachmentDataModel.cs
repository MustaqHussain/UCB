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
    public partial class AttachmentDataModel : BaseModel
    {
    
        public virtual System.Guid Code
        {
            get { return _code; }
            set { _code = value; }
        }
        private System.Guid _code;
    
        public virtual System.Guid AttachmentCode
        {
            get { return _attachmentCode; }
            set { _attachmentCode = value; }
        }
        private System.Guid _attachmentCode;
    
        public virtual byte[] DocumentBitmap
        {
            get { return _documentBitmap; }
            set { _documentBitmap = value; }
        }
        private byte[] _documentBitmap;
    
        public virtual byte[] RowIdentifier
        {
            get { return _rowIdentifier; }
            set { _rowIdentifier = value; }
        }
        private byte[] _rowIdentifier;
    }
}
