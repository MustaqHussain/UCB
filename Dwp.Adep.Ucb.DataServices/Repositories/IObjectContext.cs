using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Dwp.Adep.Ucb.DataServices
{
    public interface IObjectContext : IDisposable
    {
        IObjectSet<T> CreateObjectSet<T>() where T : class;
        int SaveChanges();
        void DetectChanges();
        object GetObjectByKey(EntityKey key);

        ObjectStateManager ObjectStateManager { get; }
        DbConnection Connection { get; }

        string UserID { get; set; }
        string UserOnBehalfOfID { get; set; }
        string AppID { get; set; }
        string Level { get; set; }
    }
}
