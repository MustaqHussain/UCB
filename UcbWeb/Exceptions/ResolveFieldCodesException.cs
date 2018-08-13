using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCBWeb.Exceptions
{
    [Serializable]
    public class ResolveFieldCodesException : Exception
    {
        public ResolveFieldCodesException() { }
        public ResolveFieldCodesException(string message) : base(message) { }
        public ResolveFieldCodesException(string message, Exception inner) : base(message, inner) { }
        protected ResolveFieldCodesException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

    }
}