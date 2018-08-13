using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using AutoMapper;
using Dwp.Adep.Ucb.WebServices.DataContracts;
using Dwp.Adep.Ucb.WebServices.Exceptions;
using Dwp.Adep.Ucb.DataServices;
using Dwp.Adep.Ucb.DataServices.Models;
using Dwp.Adep.Ucb.WebServices.ServiceContracts;
using Dwp.Adep.Ucb.WebServices.MessageContracts.Exceptions;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial class UcbService
    {
        #region GetIntroductoryInformation

        /// <summary>
        /// Retrieve a IntroductoryInformation with associated lookups
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="locale"></param>
        /// <returns></returns>
        public IntroductoryInformationVMDC GetIntroductoryInformationByLocale(string currentUser, string user, string appID, string overrideID, string locale)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<IntroductoryInformation> dataRepository = new Repository<IntroductoryInformation>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return GetIntroductoryInformationByLocale(currentUser, user, appID, overrideID, locale, uow, dataRepository, exceptionManager);
        }

        /// <summary>
        /// Retrieve a IntroductoryInformation by locale with associated lookups
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="locale">locale</param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public IntroductoryInformationVMDC GetIntroductoryInformationByLocale(string currentUser, string user, string appID, string overrideID, string locale,
            IUnitOfWork uow, IRepository<IntroductoryInformation> dataRepository, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (null == locale) throw new ArgumentOutOfRangeException("locale");
                if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                using (uow)
                {

                    IntroductoryInformationDC destination = null;

                    // If code is null then just return supporting lists
                    if (!string.IsNullOrEmpty(locale))
                    {
                        // Retrieve specific IntroductoryInformation
                        IntroductoryInformation dataEntity = dataRepository.Single(x => x.Locale == locale);

                        // Convert to data contract for passing through service interface
                        destination = Mapper.Map<IntroductoryInformation, IntroductoryInformationDC>(dataEntity);
                    }

                    // Create aggregate contract
                    IntroductoryInformationVMDC returnObject = new IntroductoryInformationVMDC();

                    returnObject.IntroductoryInformationItem = destination;

                    return returnObject;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }
        }

        #endregion	
    }
}