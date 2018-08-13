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
        #region generated code for ControlMeasure
    /// <summary>
    /// Service
    /// Class containing service behaviour for ControlMeasure
    /// </summary>
    public partial class UcbService
    {
            #region Behaviour for ControlMeasure
    
    		#region Create
    
    		/// <summary>
            /// Create a ControlMeasure
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public ControlMeasureVMDC CreateControlMeasure(string currentUser, string user, string appID, string overrideID, ControlMeasureDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<ControlMeasure> dataRepository = new Repository<ControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			
    			// Call overload with injected objects
                return CreateControlMeasure(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            ///  Create a ControlMeasure
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public ControlMeasureVMDC CreateControlMeasure(string currentUser, string user, string appID, string overrideID, ControlMeasureDC dc, IRepository<ControlMeasure> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					// Create a new ID for the ControlMeasure item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        ControlMeasure destination = mappingService.Map<ControlMeasureDC, ControlMeasure>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<ControlMeasure, ControlMeasureDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				ControlMeasureVMDC returnObject = new ControlMeasureVMDC();
    				
    				// Add new item to aggregate
    				returnObject.ControlMeasureItem = dc;
    				
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
            /// Update a ControlMeasure
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public ControlMeasureVMDC UpdateControlMeasure(string currentUser, string user, string appID, string overrideID, ControlMeasureDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<ControlMeasure> dataRepository = new Repository<ControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return UpdateControlMeasure(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a ControlMeasure
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public ControlMeasureVMDC UpdateControlMeasure(string currentUser, string user, string appID, string overrideID, ControlMeasureDC dc, IRepository<ControlMeasure> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        ControlMeasure destination = mappingService.Map<ControlMeasureDC, ControlMeasure>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<ControlMeasure, ControlMeasureDC>(destination);
                    }
    				
    				// Create new data contract to return
    				ControlMeasureVMDC returnObject = new ControlMeasureVMDC();
    				
    				// Add new item to datacontract
    				returnObject.ControlMeasureItem = dc;
    				
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
            /// Delete a ControlMeasure
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteControlMeasure(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<ControlMeasure> dataRepository = new Repository<ControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                DeleteControlMeasure(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a ControlMeasure
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteControlMeasure(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<ControlMeasure> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        ControlMeasure dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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
    
    		#region SearchControlMeasure
    
    		/// <summary>
            /// Search for ControlMeasure items
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
    		public ControlMeasureSearchVMDC SearchControlMeasure(string currentUser, string user, string appID, string overrideID, ControlMeasureSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<ControlMeasure> dataRepository = new Repository<ControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<ControlMeasure> specification = new Specification<ControlMeasure>();
    
    		    // Create isActive specification
                ISpecification<ControlMeasure> isActiveSpecification = new IsActiveSpecification<ControlMeasure>();
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return SearchControlMeasure(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, includeInActive, specification, isActiveSpecification, dataRepository, uow,exceptionManager, mappingService);
    		}
    
    		/// <summary>
            /// Search for ControlMeasure items
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
    		public ControlMeasureSearchVMDC SearchControlMeasure(string currentUser, string user, string appID, string overrideID, ControlMeasureSearchCriteriaDC searchCriteria, int page, int pageSize, bool includeInActive,
    		ISpecification<ControlMeasure> specification, ISpecification<ControlMeasure> isActiveSpecification, IRepository<ControlMeasure> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                            EvaluateControlMeasureSearchCriteria(searchCriteria, ref specification);
                        }
    
                        if (!includeInActive)
                        {
                            specification = specification.And(isActiveSpecification);
                        }
    
    					// Set default sort expression
    					System.Linq.Expressions.Expression<Func<ControlMeasure, Object>> sortExpression = x => new { x.ControlMeasureDescription };
    					
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<ControlMeasure> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Count(specification);
    
    					ControlMeasureSearchVMDC results = new ControlMeasureSearchVMDC();
    
    					// Convert to data contracts
                        List<ControlMeasureSearchMatchDC> destinations = mappingService.Map<IEnumerable<ControlMeasure>, List<ControlMeasureSearchMatchDC>>(dataEntities);
    
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
    		
    		// Partial method for evaluation of ControlMeasure search criteria
            partial void EvaluateControlMeasureSearchCriteria(ControlMeasureSearchCriteriaDC searchCriteria, ref ISpecification<ControlMeasure> specification);
    		
    		#endregion
    		
    		#region GetAllControlMeasure
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
    		/// <param name="includeInActive"></param>
            /// <returns></returns>
            public List<ControlMeasureDC> GetAllControlMeasure(string currentUser, string user, string appID, string overrideID, bool includeInActive)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                Repository<ControlMeasure> dataRepository = new Repository<ControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<ControlMeasure> specification = new Specification<ControlMeasure>();
    			
    			// Create isActive specification
                ISpecification<ControlMeasure> isActiveSpecification =  new IsActiveSpecification<ControlMeasure>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return GetAllControlMeasure(currentUser, user, appID, overrideID, includeInActive, specification, isActiveSpecification, dataRepository, uow, exceptionManager, mappingService);
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
            public List<ControlMeasureDC> GetAllControlMeasure(string currentUser, string user, string appID, string overrideID,  bool includeInActive, ISpecification<ControlMeasure> specification, ISpecification<ControlMeasure> isActiveSpecification, IRepository<ControlMeasure> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					System.Linq.Expressions.Expression<Func<ControlMeasure, Object>> sortExpression = x => new { x.ControlMeasureDescription };
    					
                        // Find all items that satisfy the specification created above.
                        IEnumerable<ControlMeasure> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<ControlMeasureDC> destinations = mappingService.Map<IEnumerable<ControlMeasure>, List<ControlMeasureDC>>(dataEntities);
    
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
    
            #region GetControlMeasure
    	
    		/// <summary>
        	/// Retrieve a ControlMeasure with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public ControlMeasureVMDC GetControlMeasure(string currentUser, string user, string appID, string overrideID, string code)
            {
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<ControlMeasure> dataRepository = new Repository<ControlMeasure>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    
    			// Call overload with injected objects
                return GetControlMeasure(currentUser, user, appID, overrideID, code, uow, dataRepository
    			, exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Retrieve a ControlMeasure with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public ControlMeasureVMDC GetControlMeasure(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<ControlMeasure> dataRepository
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
    				
    					ControlMeasureDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific ControlMeasure
    	                    ControlMeasure dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = mappingService.Map<ControlMeasure, ControlMeasureDC>(dataEntity);
    					}
    
    
    
        				// Create aggregate contract
                        ControlMeasureVMDC returnObject = new ControlMeasureVMDC();
    
                        returnObject.ControlMeasureItem = destination;
                        
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
