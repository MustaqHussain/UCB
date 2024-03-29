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
        #region generated code for IncidentSystemMarked
    /// <summary>
    /// Service
    /// Class containing service behaviour for IncidentSystemMarked
    /// </summary>
    public partial class UcbService
    {
            #region Behaviour for IncidentSystemMarked
    
    		#region Create
    
    		/// <summary>
            /// Create a IncidentSystemMarked
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public IncidentSystemMarkedVMDC CreateIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, IncidentSystemMarkedDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<IncidentSystemMarked> dataRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			
    			// Call overload with injected objects
                return CreateIncidentSystemMarked(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            ///  Create a IncidentSystemMarked
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public IncidentSystemMarkedVMDC CreateIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, IncidentSystemMarkedDC dc, IRepository<IncidentSystemMarked> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					// Create a new ID for the IncidentSystemMarked item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        IncidentSystemMarked destination = mappingService.Map<IncidentSystemMarkedDC, IncidentSystemMarked>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<IncidentSystemMarked, IncidentSystemMarkedDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				IncidentSystemMarkedVMDC returnObject = new IncidentSystemMarkedVMDC();
    				
    				// Add new item to aggregate
    				returnObject.IncidentSystemMarkedItem = dc;
    				
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
            /// Update a IncidentSystemMarked
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public IncidentSystemMarkedVMDC UpdateIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, IncidentSystemMarkedDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<IncidentSystemMarked> dataRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return UpdateIncidentSystemMarked(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a IncidentSystemMarked
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public IncidentSystemMarkedVMDC UpdateIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, IncidentSystemMarkedDC dc, IRepository<IncidentSystemMarked> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        IncidentSystemMarked destination = mappingService.Map<IncidentSystemMarkedDC, IncidentSystemMarked>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<IncidentSystemMarked, IncidentSystemMarkedDC>(destination);
                    }
    				
    				// Create new data contract to return
    				IncidentSystemMarkedVMDC returnObject = new IncidentSystemMarkedVMDC();
    				
    				// Add new item to datacontract
    				returnObject.IncidentSystemMarkedItem = dc;
    				
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
            /// Delete a IncidentSystemMarked
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentSystemMarked> dataRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                DeleteIncidentSystemMarked(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a IncidentSystemMarked
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<IncidentSystemMarked> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        IncidentSystemMarked dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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
    
    		#region SearchIncidentSystemMarked
    
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
    		public IncidentSystemMarkedSearchVMDC SearchIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, IncidentSystemMarkedSearchCriteriaDC searchCriteria, int page, int pageSize)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentSystemMarked> dataRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<IncidentSystemMarked> specification = new Specification<IncidentSystemMarked>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return SearchIncidentSystemMarked(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, specification, dataRepository, uow, exceptionManager, mappingService);
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
    		public IncidentSystemMarkedSearchVMDC SearchIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, IncidentSystemMarkedSearchCriteriaDC searchCriteria, int page, int pageSize, 
    		ISpecification<IncidentSystemMarked> specification, IRepository<IncidentSystemMarked> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					System.Linq.Expressions.Expression<Func<IncidentSystemMarked, Object>> sortExpression = x => new { x.RowIdentifier };
    
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<IncidentSystemMarked> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Count(specification);
    
    					IncidentSystemMarkedSearchVMDC results = new IncidentSystemMarkedSearchVMDC();
    
    					// Convert to data contracts
                        List<IncidentSystemMarkedSearchMatchDC> destinations = mappingService.Map<IEnumerable<IncidentSystemMarked>, List<IncidentSystemMarkedSearchMatchDC>>(dataEntities);
    
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
            partial void EvaluateSearchCriteria(IncidentSystemMarkedSearchCriteriaDC searchCriteria, ref ISpecification<IncidentSystemMarked> specification);
    		
    		#endregion
    		
    		#region GetAllIncidentSystemMarked
    		
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <returns></returns>
            public List<IncidentSystemMarkedDC> GetAllIncidentSystemMarked(string currentUser, string user, string appID, string overrideID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                IRepository<IncidentSystemMarked> dataRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<IncidentSystemMarked> specification = new Specification<IncidentSystemMarked>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
                return GetAllIncidentSystemMarked(currentUser, user, appID, overrideID, specification, dataRepository, uow, exceptionManager, mappingService);
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
            public List<IncidentSystemMarkedDC> GetAllIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, ISpecification<IncidentSystemMarked> specification, IRepository<IncidentSystemMarked> dataRepository, IUnitOfWork uow, IExceptionManager exceptionManager, IMappingService mappingService)
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
    					System.Linq.Expressions.Expression<Func<IncidentSystemMarked, Object>> sortExpression = x => new { x.RowIdentifier };
    
                        // Find all items that satisfy the specification created above.
                        IEnumerable<IncidentSystemMarked> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<IncidentSystemMarkedDC> destinations = mappingService.Map<IEnumerable<IncidentSystemMarked>, List<IncidentSystemMarkedDC>>(dataEntities);
    
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
    
            #region GetIncidentSystemMarked
    	
    		/// <summary>
        	/// Retrieve a IncidentSystemMarked with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public IncidentSystemMarkedVMDC GetIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, string code)
            {
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentSystemMarked> dataRepository = new Repository<IncidentSystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    			IRepository<Incident> incidentRepository = new Repository<Incident>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			IRepository<SystemMarked> systemMarkedRepository = new Repository<SystemMarked>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return GetIncidentSystemMarked(currentUser, user, appID, overrideID, code, uow, dataRepository
    			, incidentRepository
    			, systemMarkedRepository
    			, exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Retrieve a IncidentSystemMarked with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public IncidentSystemMarkedVMDC GetIncidentSystemMarked(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<IncidentSystemMarked> dataRepository
    			,IRepository<Incident> incidentRepository
    			,IRepository<SystemMarked> systemMarkedRepository
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
    				
    					IncidentSystemMarkedDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific IncidentSystemMarked
    	                    IncidentSystemMarked dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = mappingService.Map<IncidentSystemMarked, IncidentSystemMarkedDC>(dataEntity);
    					}
    
    					IEnumerable<Incident> incidentList = incidentRepository.GetAll(x => new {x.IncidentID});
    					IEnumerable<SystemMarked> systemMarkedList = systemMarkedRepository.GetAll(x => new {x.Description});
    
    					List<IncidentDC> incidentDestinationList = mappingService.Map<List<IncidentDC>>(incidentList);
    					List<SystemMarkedDC> systemMarkedDestinationList = mappingService.Map<List<SystemMarkedDC>>(systemMarkedList);
    
        				// Create aggregate contract
                        IncidentSystemMarkedVMDC returnObject = new IncidentSystemMarkedVMDC();
    
                        returnObject.IncidentSystemMarkedItem = destination;
    					returnObject.IncidentList = incidentDestinationList;
    					returnObject.SystemMarkedList = systemMarkedDestinationList;
                        
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
