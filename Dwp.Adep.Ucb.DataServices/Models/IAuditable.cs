using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dwp.Adep.Ucb.DataServices.Models
{
    public interface IAuditable
    {
        Guid Code { get; set; }
    }
}
