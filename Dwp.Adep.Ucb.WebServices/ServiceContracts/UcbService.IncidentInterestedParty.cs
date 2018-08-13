//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using Dwp.Adep.Ucb.Mapping;
using Dwp.Adep.Ucb.DataServices;
using Dwp.Adep.Ucb.DataServices.Models;
using Dwp.Adep.Ucb.WebServices.MessageContracts.Exceptions;
using Dwp.Adep.Ucb.WebServices.ServiceContracts;

namespace Dwp.Adep.Ucb.WebServices.ServiceContracts
{
        #region generated code for IncidentInterestedParty
    /// <summary>
    /// Service
    /// Class containing service behaviour for IncidentInterestedParty
    /// </summary>
    public partial class UcbService
    {
            #region Behaviour for IncidentInterestedParty
    
    		#region Create
    
    		/// <summary>
            /// Create a IncidentInterestedParty
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public IncidentInterestedPartyVMDC CreateIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, IncidentInterestedPartyDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<IncidentInterestedParty> dataRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			
    			// Call overload with injected objects
                return CreateIncidentInterestedParty(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            ///  Create a IncidentInterestedParty
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public IncidentInterestedPartyVMDC CreateIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, IncidentInterestedPartyDC dc, IRepository<IncidentInterestedParty> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dc) throw new ArgumentOutOfRangeException("dc");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    				if (null == exceptionManager) throw new ArgumentOutOfRangeException("exceptionManager");
    				if (null == mappingService) throw new ArgumentOutOfRangeException("mappingService");
    				
    				#endregion
    
                    using (uow)
                    {
    					// Create a new ID for the IncidentInterestedParty item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        IncidentInterestedParty destination = mappingService.Map<IncidentInterestedPartyDC, IncidentInterestedParty>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<IncidentInterestedParty, IncidentInterestedPartyDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				IncidentInterestedPartyVMDC returnObject = new IncidentInterestedPartyVMDC();
    				
    				// Add new item to aggregate
    				returnObject.IncidentInterestedPartyItem = dc;
    				
    				return returnObject;
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    exceptionManager.ShieldException(e);
    				
    				return null;
                }
            }
    
            #endregion
    
    		#region Update
    
    		/// <summary>
            /// Update a IncidentInterestedParty
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public IncidentInterestedPartyVMDC UpdateIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, IncidentInterestedPartyDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<IncidentInterestedParty> dataRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return UpdateIncidentInterestedParty(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a IncidentInterestedParty
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public IncidentInterestedPartyVMDC UpdateIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, IncidentInterestedPartyDC dc, IRepository<IncidentInterestedParty> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dc) throw new ArgumentOutOfRangeException("dc");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    				if (null == exceptionManager) throw new ArgumentOutOfRangeException("exceptionManager");
    				if (null == mappingService) throw new ArgumentOutOfRangeException("mappingService");
    				
    				#endregion
    
                    using (uow)
                    {
    					// Map data contract to model
                        IncidentInterestedParty destination = mappingService.Map<IncidentInterestedPartyDC, IncidentInterestedParty>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<IncidentInterestedParty, IncidentInterestedPartyDC>(destination);
                    }
    				
    				// Create new data contract to return
    				IncidentInterestedPartyVMDC returnObject = new IncidentInterestedPartyVMDC();
    				
    				// Add new item to datacontract
    				returnObject.IncidentInterestedPartyItem = dc;
    				
    				// Commit unit of work
    				return returnObject;
    				
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    exceptionManager.ShieldException(e);
    				
    				return null;
                }
            }
    
            #endregion
    
    		#region Delete
    
    		/// <summary>
            /// Delete a IncidentInterestedParty
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentInterestedParty> dataRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                DeleteIncidentInterestedParty(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a IncidentInterestedParty
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<IncidentInterestedParty> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (string.IsNullOrEmpty(code)) throw new ArgumentOutOfRangeException("code");
    				if (string.IsNullOrEmpty(lockID)) throw new ArgumentOutOfRangeException("lockID");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    				if (null == exceptionManager) throw new ArgumentOutOfRangeException("exceptionManager");
    				if (null == mappingService) throw new ArgumentOutOfRangeException("mappingService");
    				
    				#endregion
    
                    using (uow)
                    {
    					// Convert string to guid
                        Guid codeGuid = Guid.Parse(code);	
    					
    					// Find item based on ID
                        IncidentInterestedParty dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
    					// Delete the item
                        dataRepository.Delete(dataEntity);
    
    					// Commit unit of work
                        uow.Commit();
                    }
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    exceptionManager.ShieldException(e);
                }
            }
    
            #endregion
    
    		#region SearchIncidentInterestedParty
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="searchCriteria"></param>
            /// <param name="page"></param>
            /// <param name="pageSize"></param>
            /// <returns></returns>
    		public IncidentInterestedPartySearchVMDC SearchIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, IncidentInterestedPartySearchCriteriaDC searchCriteria, int page, int pageSize)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentInterestedParty> dataRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<IncidentInterestedParty> specification = new Specification<IncidentInterestedParty>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return SearchIncidentInterestedParty(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, specification, dataRepository, uow, exceptionManager, mappingService);
    		}
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="searchCriteria"></param>
            /// <param name="page"></param>
            /// <param name="pageSize"></param>
    		/// <param name="specification"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
    		public IncidentInterestedPartySearchVMDC SearchIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, IncidentInterestedPartySearchCriteriaDC searchCriteria, int page, int pageSize, 
    		ISpecification<IncidentInterestedParty> specification, IRepository<IncidentInterestedParty> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    				if (null == exceptionManager) throw new ArgumentOutOfRangeException("exceptionManager");
    				if (null == mappingService) throw new ArgumentOutOfRangeException("mappingService");
    				
    				#endregion
    
                    using (uow)
                    {
                        // Evaluate search criteria if supplied
                        if (null != searchCriteria)
                        {
                            EvaluateSearchCriteria(searchCriteria, ref specification);
                        }
    					
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<IncidentInterestedParty, Object>> sortExpression = x => new { x.RowIdentifier };
    
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<IncidentInterestedParty> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Count(specification);
    
    					IncidentInterestedPartySearchVMDC results = new IncidentInterestedPartySearchVMDC();
    
    					// Convert to data contracts
                        List<IncidentInterestedPartySearchMatchDC> destinations = mappingService.Map<IEnumerable<IncidentInterestedParty>, List<IncidentInterestedPartySearchMatchDC>>(dataEntities);
    
    					results.MatchList = destinations;
    					results.SearchCriteria = searchCriteria;
    					results.RecordCount = itemCount;
    
                        return results;
                    }
                }
                catch (Exception e)
                {
                    //Prevent exception from propogating across the service interface
                    exceptionManager.ShieldException(e);
    
                    return null;
                }
    		}
    		
    		// Partial method for evaluation of search criteria
            partial void EvaluateSearchCriteria(IncidentInterestedPartySearchCriteriaDC searchCriteria, ref ISpecification<IncidentInterestedParty> specification);
    		
    		#endregion
    		
    		#region GetAllIncidentInterestedParty
    		
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <returns></returns>
            public List<IncidentInterestedPartyDC> GetAllIncidentInterestedParty(string currentUser, string user, string appID, string overrideID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                IRepository<IncidentInterestedParty> dataRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<IncidentInterestedParty> specification = new Specification<IncidentInterestedParty>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
                return GetAllIncidentInterestedParty(currentUser, user, appID, overrideID, specification, dataRepository, uow, exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
    		/// <param name="specification"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public List<IncidentInterestedPartyDC> GetAllIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, ISpecification<IncidentInterestedParty> specification, IRepository<IncidentInterestedParty> dataRepository, IUnitOfWork uow, IExceptionManager exceptionManager, IMappingService mappingService)
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
    				if (null == exceptionManager) throw new ArgumentOutOfRangeException("exceptionManager");
    				if (null == mappingService) throw new ArgumentOutOfRangeException("mappingService");
    				
    				#endregion
    
                    using (uow)
                    {
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<IncidentInterestedParty, Object>> sortExpression = x => new { x.RowIdentifier };
    
                        // Find all items that satisfy the specification created above.
                        IEnumerable<IncidentInterestedParty> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<IncidentInterestedPartyDC> destinations = mappingService.Map<IEnumerable<IncidentInterestedParty>, List<IncidentInterestedPartyDC>>(dataEntities);
    
                        return destinations;
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
    
            #region GetIncidentInterestedParty
    	
    		/// <summary>
        	/// Retrieve a IncidentInterestedParty with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public IncidentInterestedPartyVMDC GetIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, string code)
            {
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentInterestedParty> dataRepository = new Repository<IncidentInterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    			IRepository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			IRepository<InterestedParty> interestedPartyRepository = new Repository<InterestedParty>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return GetIncidentInterestedParty(currentUser, user, appID, overrideID, code, uow, dataRepository
    			, incidentRepository
    			, interestedPartyRepository
    			, exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Retrieve a IncidentInterestedParty with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public IncidentInterestedPartyVMDC GetIncidentInterestedParty(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<IncidentInterestedParty> dataRepository
    			,IRepository<Incident> incidentRepository
    			,IRepository<InterestedParty> interestedPartyRepository
    			, IExceptionManager exceptionManager, IMappingService mappingService)
    		
            {
                try
                {
    				#region Parameter validation
    
    				// Validate parameters
    				if (string.IsNullOrEmpty(currentUser)) throw new ArgumentOutOfRangeException("currentUser");
    				if (string.IsNullOrEmpty(user)) throw new ArgumentOutOfRangeException("user");
    				if (string.IsNullOrEmpty(appID)) throw new ArgumentOutOfRangeException("appID");
    				if (null == dataRepository) throw new ArgumentOutOfRangeException("dataRepository");
    				if (null == uow) throw new ArgumentOutOfRangeException("uow");
    				if (null == exceptionManager) throw new ArgumentOutOfRangeException("exceptionManager");
    				if (null == mappingService) throw new ArgumentOutOfRangeException("mappingService");
    				
    				#endregion
    
                    using (uow)
                    {
    				
    					IncidentInterestedPartyDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific IncidentInterestedParty
    	                    IncidentInterestedParty dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = mappingService.Map<IncidentInterestedParty, IncidentInterestedPartyDC>(dataEntity);
    					}
    
    					IEnumerable<Incident> incidentList = incidentRepository.GetAll(x => new {x.IncidentID});
    					IEnumerable<InterestedParty> interestedPartyList = interestedPartyRepository.GetAll(x => new {x.Description});
    
    					List<IncidentDC> incidentDestinationList = mappingService.Map<List<IncidentDC>>(incidentList);
    					List<InterestedPartyDC> interestedPartyDestinationList = mappingService.Map<List<InterestedPartyDC>>(interestedPartyList);
    
        				// Create aggregate contract
                        IncidentInterestedPartyVMDC returnObject = new IncidentInterestedPartyVMDC();
    
                        returnObject.IncidentInterestedPartyItem = destination;
    					returnObject.IncidentList = incidentDestinationList;
    					returnObject.InterestedPartyList = interestedPartyDestinationList;
                        
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
    
    	

            #endregion
    }

        #endregion
}