using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Dwp.Adep.Ucb.Mapping
{
    public class MappingService : Dwp.Adep.Ucb.Mapping.IMappingService
    {
        public TDest Map<TSrc, TDest>(TSrc source)
            where TDest : class
            where TSrc : class
        {
            return Mapper.Map<TSrc, TDest>(source);
        }

        public TDest Map<TDest>(object source)
            where TDest : class
        {
            return Mapper.Map<TDest>(source);
        }
        
        public void CreateMap<TSrc, TDest>()
            where TSrc : class
            where TDest : class
        {
            Mapper.CreateMap<TSrc, TDest>();
        }
    }
}
