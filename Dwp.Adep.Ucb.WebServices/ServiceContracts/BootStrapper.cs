using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Dwp.Adep.Ucb.IoC.ServiceLocation;
using Dwp.Adep.Ucb.DataServices;
using Dwp.Adep.Ucb.DataServices.Models;

namespace Dwp.Adep.Ucb.WebServices
{
    public class BootStrapper
    {
        private static object _lockObject = new object();

        public static void InitializeIoc()
        {
            // Outside "if" to reduce contention
            if (null == SimpleServiceLocator.Instance)
            {
                // Only one thread to execute this code to determine if the servicelocator has alerady been created.
                lock (_lockObject)
                {
                    if (null == SimpleServiceLocator.Instance)
                    {
                        // Call this method at App level e.g. Global.asax to ensure object referenced for app lifetime.
                        SimpleServiceLocator.SetServiceLocatorProvider(new UnityServiceLocator());

                        // UnitOfWork
                        SimpleServiceLocator.Instance.Register(typeof(IUnitOfWork), typeof(UnitOfWork));

                        // Register Context
                        SimpleServiceLocator.Instance.RegisterWithConstructorParameters(typeof(IObjectContext), typeof(AdepUcbDBEntities));
                        SimpleServiceLocator.Instance.RegisterWithConstructorParameters<IObjectContext, AdepUcbDBEntities>("AdepUcbDBEntities", new object[0]);

                        //Register Generic Repositories
                        SimpleServiceLocator.Instance.Register(typeof(IRepository<>), typeof(Repository<>));
                    }
                }
            }
        }
    }
}
