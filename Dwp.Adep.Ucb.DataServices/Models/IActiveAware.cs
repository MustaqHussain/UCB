using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Ucb.DataServices.Models
{
    public interface IActiveAware
    {
        Guid Code { get; set; }
		bool IsActive { get; set; }
    }
}
