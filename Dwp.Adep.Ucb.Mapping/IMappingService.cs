using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Ucb.Mapping
{
    public interface IMappingService
    {
        TDest Map<TSrc, TDest>(TSrc source)
            where TDest : class
            where TSrc : class;
			
        TDest Map<TDest>(object source)
            where TDest : class;
			
        void CreateMap<TSrc, TDest>()
            where TSrc : class
            where TDest : class;
    }
}
