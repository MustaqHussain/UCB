using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Ucb.DataServices
{
    public interface IUnitOfWork : IDisposable
    {
        IObjectContext ObjectContext { get; }
        void Commit();

        /// <summary>
        /// Custom audit method.
        /// </summary>
        /// <param name="auditText">The audit text.</param>
        /// <param name="auditAction">The audit action  (e.g. View).</param>
        /// <param name="propertiesToAudit">Associated properties to audit such as the Nino amd its value.</param>
        void CustomAudit(string auditText, string auditAction, IDictionary<string, string> propertiesToAudit);
    }
}
