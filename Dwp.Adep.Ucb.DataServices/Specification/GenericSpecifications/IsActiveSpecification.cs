using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dwp.Adep.Ucb.DataServices.Models;

namespace Dwp.Adep.Ucb.DataServices
{
    public class IsActiveSpecification<T> : Specification<T> where T : class, IActiveAware
    {
        public IsActiveSpecification()
        {
            Predicate = x => x.IsActive == true;
        }
    }
}
