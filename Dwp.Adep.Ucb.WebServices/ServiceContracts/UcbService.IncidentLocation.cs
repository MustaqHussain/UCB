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
        #region generated code for IncidentLocation
    /// <summary>
    /// Service
    /// Class containing service behaviour for IncidentLocation
    /// </summary>
    public partial class UcbService
    {
            #region Behaviour for IncidentLocation
    
    		#region Create
    
    		/// <summary>
            /// Create a IncidentLocation
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public IncidentLocationVMDC CreateIncidentLocation(string currentUser, string user, string appID, string overrideID, IncidentLocationDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<IncidentLocation> dataRepository = new Repository<IncidentLocation>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			
    			// Call overload with injected objects
                return CreateIncidentLocation(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            ///  Create a IncidentLocation
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public IncidentLocationVMDC CreateIncidentLocation(string currentUser, string user, string appID, string overrideID, IncidentLocationDC dc, IRepository<IncidentLocation> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					// Create a new ID for the IncidentLocation item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        IncidentLocation destination = mappingService.Map<IncidentLocationDC, IncidentLocation>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<IncidentLocation, IncidentLocationDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				IncidentLocationVMDC returnObject = new IncidentLocationVMDC();
    				
    				// Add new item to aggregate
    				returnObject.IncidentLocationItem = dc;
    				
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
            /// Update a IncidentLocation
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public IncidentLocationVMDC UpdateIncidentLocation(string currentUser, string user, string appID, string overrideID, IncidentLocationDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<IncidentLocation> dataRepository = new Repository<IncidentLocation>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return UpdateIncidentLocation(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a IncidentLocation
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public IncidentLocationVMDC UpdateIncidentLocation(string currentUser, string user, string appID, string overrideID, IncidentLocationDC dc, IRepository<IncidentLocation> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        IncidentLocation destination = mappingService.Map<IncidentLocationDC, IncidentLocation>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<IncidentLocation, IncidentLocationDC>(destination);
                    }
    				
    				// Create new data contract to return
    				IncidentLocationVMDC returnObject = new IncidentLocationVMDC();
    				
    				// Add new item to datacontract
    				returnObject.IncidentLocationItem = dc;
    				
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
            /// Delete a IncidentLocation
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteIncidentLocation(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentLocation> dataRepository = new Repository<IncidentLocation>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                DeleteIncidentLocation(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a IncidentLocation
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteIncidentLocation(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<IncidentLocation> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        IncidentLocation dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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
    
    		#region SearchIncidentLocation
    
    		/// <summary>
            /// Search for IncidentLocation items
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
    		public IncidentLocationSearchVMDC SearchIncidentLocation(string currentUser, string user, string appID, string overrideID, IncidentLocationSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentLocation> dataRepository = new Repository<IncidentLocation>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<IncidentLocation> specification = new Specification<IncidentLocation>();
    
    		    // Create isActive specification
                ISpecification<IncidentLocation> isActiveSpecification = new IsActiveSpecification<IncidentLocation>();
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return SearchIncidentLocation(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, includeInActive, specification, isActiveSpecification, dataRepository, uow,exceptionManager, mappingService);
    		}
    
    		/// <summary>
            /// Search for IncidentLocation items
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
    		public IncidentLocationSearchVMDC SearchIncidentLocation(string currentUser, string user, string appID, string overrideID, IncidentLocationSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive,
    		ISpecification<IncidentLocation> specification, ISpecification<IncidentLocation> isActiveSpecification, IRepository<IncidentLocation> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                            EvaluateIncidentLocationSearchCriteria(searchCriteria, ref specification);
                        }
    
                        if (!includeInActive)
                        {
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<IncidentLocation, Object>> sortExpression = x => new { x.Description };
    					
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<IncidentLocation> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Count(specification);
    
    					IncidentLocationSearchVMDC results = new IncidentLocationSearchVMDC();
    
    					// Convert to data contracts
                        List<IncidentLocationSearchMatchDC> destinations = mappingService.Map<IEnumerable<IncidentLocation>, List<IncidentLocationSearchMatchDC>>(dataEntities);
    
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
    		
    		// Partial method for evaluation of IncidentLocation search criteria
            partial void EvaluateIncidentLocationSearchCriteria(IncidentLocationSearchCriteriaDC searchCriteria, ref ISpecification<IncidentLocation> specification);
    		
    		#endregion
    		
    		#region GetAllIncidentLocation
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
    		/// <param name="includeInActive"></param>
            /// <returns></returns>
            public List<IncidentLocationDC> GetAllIncidentLocation(string currentUser, string user, string appID, string overrideID, bool includeInActive)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                Repository<IncidentLocation> dataRepository = new Repository<IncidentLocation>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<IncidentLocation> specification = new Specification<IncidentLocation>();
    			
    			// Create isActive specification
                ISpecification<IncidentLocation> isActiveSpecification =  new IsActiveSpecification<IncidentLocation>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return GetAllIncidentLocation(currentUser, user, appID, overrideID, includeInActive, specification, isActiveSpecification, dataRepository, uow, exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
    		/// <param name="includeInActive"></param>
    		/// <param name="specification"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public List<IncidentLocationDC> GetAllIncidentLocation(string currentUser, string user, string appID, string overrideID,  bool includeInActive, ISpecification<IncidentLocation> specification, ISpecification<IncidentLocation> isActiveSpecification, IRepository<IncidentLocation> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					// Build specification
                        if (!includeInActive)
                        {
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<IncidentLocation, Object>> sortExpression = x => new { x.Description };
    					
                        // Find all items that satisfy the specification created above.
                        IEnumerable<IncidentLocation> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<IncidentLocationDC> destinations = mappingService.Map<IEnumerable<IncidentLocation>, List<IncidentLocationDC>>(dataEntities);
    
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
    
            #region GetIncidentLocation
    	
    		/// <summary>
        	/// Retrieve a IncidentLocation with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public IncidentLocationVMDC GetIncidentLocation(string currentUser, string user, string appID, string overrideID, string code)
            {
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<IncidentLocation> dataRepository = new Repository<IncidentLocation>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    
    			// Call overload with injected objects
                return GetIncidentLocation(currentUser, user, appID, overrideID, code, uow, dataRepository
    			, exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Retrieve a IncidentLocation with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public IncidentLocationVMDC GetIncidentLocation(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<IncidentLocation> dataRepository
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
    				
    					IncidentLocationDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific IncidentLocation
    	                    IncidentLocation dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = mappingService.Map<IncidentLocation, IncidentLocationDC>(dataEntity);
    					}
    
    
    
        				// Create aggregate contract
                        IncidentLocationVMDC returnObject = new IncidentLocationVMDC();
    
                        returnObject.IncidentLocationItem = destination;
                        
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
