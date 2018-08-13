using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dwp.Adep.Ucb.DataServices.Models;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial class UcbService : IUcbService
    {
        AdepUcbDBEntities ObjContext = new AdepUcbDBEntities();

        public UcbService()
        {
            BootStrapper.InitializeIoc();

            TypeMappingConfigurator.DefineTypeMappings();
        }        
   }
}
