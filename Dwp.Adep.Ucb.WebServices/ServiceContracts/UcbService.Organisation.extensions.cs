using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dwp.Adep.Ucb.WebServices.DataContracts;
using Dwp.Adep.Ucb.DataServices.Models;
using Dwp.Adep.Ucb.DataServices;
using Dwp.Adep.Ucb.WebServices.Exceptions;
using AutoMapper;
using Dwp.Adep.Ucb.WebServices.MessageContracts.Exceptions;
using Dwp.Adep.Ucb.WebServices.Constants;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
    public partial class UcbService
    {
        #region GetAllHomeOfficesForBusinessArea

        /// <summary>
        /// Retrieve All Home Offices (level 6) for a Business Area (level 3) 
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <returns></returns>

        public List<SiteDC> GetAllSitesForLevelThreeOrganisation(string currentUser, string user, string appID, string overrideID, string businessAreaCode)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Site> siteRepository = new Repository<Site>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return GetAllSitesForLevelThreeOrganisation(currentUser, user, appID, overrideID, businessAreaCode, uow, siteRepository, exceptionManager);
        }

        public List<SiteDC> GetAllSitesForLevelThreeOrganisation(string currentUser, string user, string appID, string overrideID, string levelThreeOrganisationCode,
            IUnitOfWork uow, IRepository<Site> siteRepository, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (string.IsNullOrEmpty(levelThreeOrganisationCode)) throw new ArgumentOutOfRangeException("businessAreaCode");
                if (null == siteRepository) throw new ArgumentOutOfRangeException("siteRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                // Convert code to Guid
                Guid businessAreaCode = Guid.Parse(levelThreeOrganisationCode);
                using (uow)
                {
                    List<SiteDC> returnObject = null;
                    List<Site> sites = siteRepository.Find(new Specification<Site>(x => x.Organisation.OrganisationHierarchy.Any(y => y.AncestorOrganisationCode == businessAreaCode && y.HopsBetweenOrgAndAncestor == 3)), x => x.SiteName).ToList(); ;

                    returnObject = Mapper.Map<List<SiteDC>>(sites);

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

        #region GetNominatedManagerForSite
        /// <summary>
        /// Get the nominated manager for a site
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="code"></param>
        /// <returns></returns>

        public StaffDC GetNominatedManagerForSite(string currentUser, string user, string appID, string overrideID, string code)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<Staff> staffRepository = new Repository<Staff>(uow.ObjectContext, currentUser, user, appID, overrideID);
            IRepository<Site> siteRepository = new Repository<Site>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            // Call overload with injected objects
            return GetNominatedManagerForSite(currentUser, user, appID, overrideID, code, uow, siteRepository, staffRepository, exceptionManager);
        }

        public StaffDC GetNominatedManagerForSite(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow,
            IRepository<Site> siteRepository, IRepository<Staff> staffRepository, IExceptionManager exceptionManager)
        {
            try
            {
                #region Parameter validation

                // Validate parameters
                if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
                if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
                if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
                if (string.IsNullOrEmpty(code)) throw new ArgumentOutOfRangeException("siteCode");
                if (null == siteRepository) throw new ArgumentOutOfRangeException("organisationRepository");
                if (null == staffRepository) throw new ArgumentOutOfRangeException("staffRepository");
                if (null == uow) throw new ArgumentOutOfRangeException("uow");

                #endregion

                // Convert code to Guid
                Guid codeGuid = Guid.Parse(code);
                using (uow)
                {
                    Staff nominatedManager = staffRepository.Find(
                                                                    new Specification<Staff>(
                                                                                    x => x.SiteStaff.Any(
                                                                                                            y => y.Site.Code == codeGuid && y.Responsibility 
                                                                                                                == ServiceConstants.SITE_STAFF_RESPONSIBILITY_NOMINATED_MANAGER)
                                                                                                        )
                                                                 ).SingleOrDefault();

                    StaffDC returnObject = Mapper.Map<Staff, StaffDC>(nominatedManager);
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

        #region SearchSiteOrganisations
        /*
        /// <summary>
        /// Search for Site Organisation items
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeInActive"></param>
        /// <returns></returns>
    	public OrganisationSearchVMDC SearchSiteOrganisations(string currentUser, string user, string appID, string overrideID, OrganisationSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive)
        {
    		// Create unit of work
    		IUnitOfWork uow = new UnitOfWork();
    
    		// Create repository
            IRepository<Organisation> dataRepository = new Repository<Organisation>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    		// Create specification for filtering
    		ISpecification<Organisation> specification = new Specification<Organisation>();
    
    		// Call overload with injected objects
            return SearchSiteOrganisations(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, includeInActive, specification, dataRepository, uow);
    	}
    
    	/// <summary>
        /// Search for Site Organisation items
        /// </summary>
        /// <param name="currentUser"></param>
        /// <param name="user"></param>
        /// <param name="appID"></param>
        /// <param name="overrideID"></param>
        /// <param name="searchCriteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="includeInActive"></param>
    	/// <param name="specification"></param>
        /// <param name="dataRepository"></param>
        /// <param name="uow"></param>
        /// <returns></returns>
        public OrganisationSearchVMDC SearchSiteOrganisations(string currentUser, string user, string appID, string overrideID, OrganisationSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive,
    	ISpecification<Organisation> specification, IRepository<Organisation> dataRepository, IUnitOfWork uow)
        {
    		try
            {
    			#region Parameter validation
    
    			// Validate parameters
    			if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    			if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    			if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    			if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    			if (null == specification) throw new ArgumentOutOfRangeException("specification");
    			if (null == uow) throw new ArgumentOutOfRangeException("uow");
    
    			#endregion
    
                using (uow)
                {
    				// Evaluate search criteria if supplied
                    if (null != searchCriteria)
                    {
                        
                    }
    
                    if (!includeInActive)
                    {
                        ISpecification<Organisation> isActiveSpecification = new Specification<Organisation>(x => x.IsActive == true);
                        specification = specification.And(isActiveSpecification);
                    }
    
    				// Set default sort expression
    				System.Linq.Expressions.Expression<Func<Organisation, Object>> sortExpression = x => x.Name;
    					
    				// Find all items that satisfy the specification created above.
                    IEnumerable<Organisation> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    				// Get total count of items for search critera
    				int itemCount = dataRepository.Count(specification);
    
    				OrganisationSearchVMDC results = new OrganisationSearchVMDC();
    
    				// Convert to data contracts
                    List<OrganisationSearchMatchDC> destinations = Mapper.Map<IEnumerable<Organisation>, List<OrganisationSearchMatchDC>>(dataEntities);
    
    				results.MatchList = destinations;
    				results.SearchCriteria = searchCriteria;
    				results.RecordCount = itemCount;
    
                    return results;
                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                ExceptionManager.ShieldException(e);
    
                return null;
            }
    	}
    		*/
    	#endregion

        /// <summary>
        /// Provides a specification based on search criteria provided
        /// </summary>
        /// <param name="searchCriteria"></param>
        /// <param name="specification"></param>
        partial void EvaluateOrganisationSearchCriteria(OrganisationSearchCriteriaDC searchCriteria, ref ISpecification<Organisation> specification)
        {
            #region Parameter validation

            // Validate parameters
            if (null == searchCriteria) throw new ArgumentNullException("searchCriteria");

            #endregion

            // Specification for searching on organisation name
            ISpecification<Organisation> organisationNameSpecification = new Specification<Organisation>(x => x.Name.StartsWith(searchCriteria.Name));

            // Specification for searchingfor organisations based on Organisation Type
            ISpecification<Organisation> siteOrgansiationSpecification = new Specification<Organisation>(x => x.OrganisationType.Name == searchCriteria.OrganisationType);

            ISpecification<Organisation> organisationSpecification = null;

            #region Build specification based on the search criteria

            if (!string.IsNullOrEmpty(searchCriteria.Name))
            {
                organisationSpecification = organisationNameSpecification;
            }

            if (!string.IsNullOrEmpty(searchCriteria.OrganisationType))
            {
                organisationSpecification = organisationSpecification == null ? siteOrgansiationSpecification : organisationSpecification.And(siteOrgansiationSpecification);
            }

            if (null == organisationSpecification) organisationSpecification = new Specification<Organisation>();

            #endregion

            specification = organisationSpecification;

        }
    }
}