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
        #region generated code for StandardReport
    /// <summary>
    /// Service
    /// Class containing service behaviour for StandardReport
    /// </summary>
    public partial class UcbService
    {
            #region Behaviour for StandardReport
    
    		#region Create
    
    		/// <summary>
            /// Create a StandardReport
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public StandardReportVMDC CreateStandardReport(string currentUser, string user, string appID, string overrideID, StandardReportDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                Repository<StandardReport> dataRepository = new Repository<StandardReport>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			
    			// Call overload with injected objects
                return CreateStandardReport(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            ///  Create a StandardReport
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public StandardReportVMDC CreateStandardReport(string currentUser, string user, string appID, string overrideID, StandardReportDC dc, IRepository<StandardReport> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					// Create a new ID for the StandardReport item
    					dc.Code = Guid.NewGuid();
    					
    					// Map data contract to model
                        StandardReport destination = mappingService.Map<StandardReportDC, StandardReport>(dc);
    
    					// Add the new item
                        dataRepository.Add(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<StandardReport, StandardReportDC>(destination);
                    }
    				
    				// Create aggregate data contract
    				StandardReportVMDC returnObject = new StandardReportVMDC();
    				
    				// Add new item to aggregate
    				returnObject.StandardReportItem = dc;
    				
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
            /// Update a StandardReport
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            public StandardReportVMDC UpdateStandardReport(string currentUser, string user, string appID, string overrideID, StandardReportDC dc)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    			
    			// Create repository
                IRepository<StandardReport> dataRepository = new Repository<StandardReport>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return UpdateStandardReport(currentUser, user, appID, overrideID, dc, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a StandardReport
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="dc"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public StandardReportVMDC UpdateStandardReport(string currentUser, string user, string appID, string overrideID, StandardReportDC dc, IRepository<StandardReport> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        StandardReport destination = mappingService.Map<StandardReportDC, StandardReport>(dc);
    
    					// Add the new item
                        dataRepository.Update(destination);
    
    					// Commit unit of work
                        uow.Commit();
    					
    					// Map model back to data contract to return new row id.
    					dc = mappingService.Map<StandardReport, StandardReportDC>(destination);
                    }
    				
    				// Create new data contract to return
    				StandardReportVMDC returnObject = new StandardReportVMDC();
    				
    				// Add new item to datacontract
    				returnObject.StandardReportItem = dc;
    				
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
            /// Delete a StandardReport
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            public void DeleteStandardReport(string currentUser, string user, string appID, string overrideID, string code, string lockID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StandardReport> dataRepository = new Repository<StandardReport>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                DeleteStandardReport(currentUser, user, appID, overrideID, code, lockID, dataRepository, uow,exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Update a StandardReport
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="lockID"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            public void DeleteStandardReport(string currentUser, string user, string appID, string overrideID, string code, string lockID, IRepository<StandardReport> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
                        StandardReport dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    					
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
    
    		#region SearchStandardReport
    
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
    		public StandardReportSearchVMDC SearchStandardReport(string currentUser, string user, string appID, string overrideID, StandardReportSearchCriteriaDC searchCriteria, int page, int pageSize)
            {
    			// Create unit of work
    		    IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StandardReport> dataRepository = new Repository<StandardReport>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<StandardReport> specification = new Specification<StandardReport>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Call overload with injected objects
                return SearchStandardReport(currentUser, user, appID, overrideID, searchCriteria, page, pageSize, specification, dataRepository, uow, exceptionManager, mappingService);
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
    		public StandardReportSearchVMDC SearchStandardReport(string currentUser, string user, string appID, string overrideID, StandardReportSearchCriteriaDC searchCriteria, int page, int pageSize, 
    		ISpecification<StandardReport> specification, IRepository<StandardReport> dataRepository, IUnitOfWork uow,IExceptionManager exceptionManager, IMappingService mappingService)
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
    					System.Linq.Expressions.Expression<Func<StandardReport, Object>> sortExpression = x => new { x.ReportName };
    
    				    // Find all items that satisfy the specification created above.
                        IEnumerable<StandardReport> dataEntities = dataRepository.Find(specification, sortExpression, page, pageSize);
    					
    					// Get total count of items for search critera
    					int itemCount = dataRepository.Count(specification);
    
    					StandardReportSearchVMDC results = new StandardReportSearchVMDC();
    
    					// Convert to data contracts
                        List<StandardReportSearchMatchDC> destinations = mappingService.Map<IEnumerable<StandardReport>, List<StandardReportSearchMatchDC>>(dataEntities);
    
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
            partial void EvaluateSearchCriteria(StandardReportSearchCriteriaDC searchCriteria, ref ISpecification<StandardReport> specification);
    		
    		#endregion
    		
    		#region GetAllStandardReport
    		
    		/// <summary>
            /// 
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <returns></returns>
            public List<StandardReportDC> GetAllStandardReport(string currentUser, string user, string appID, string overrideID)
            {
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    	
    			// Create repository
                IRepository<StandardReport> dataRepository = new Repository<StandardReport>(uow.ObjectContext, currentUser, user, appID, overrideID);
    			
    			// Create specification for filtering
    			ISpecification<StandardReport> specification = new Specification<StandardReport>();
    
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
                return GetAllStandardReport(currentUser, user, appID, overrideID, specification, dataRepository, uow, exceptionManager, mappingService);
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
            public List<StandardReportDC> GetAllStandardReport(string currentUser, string user, string appID, string overrideID, ISpecification<StandardReport> specification, IRepository<StandardReport> dataRepository, IUnitOfWork uow, IExceptionManager exceptionManager, IMappingService mappingService)
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
    					System.Linq.Expressions.Expression<Func<StandardReport, Object>> sortExpression = x => new { x.ReportName };
    
                        // Find all items that satisfy the specification created above.
                        IEnumerable<StandardReport> dataEntities = dataRepository.Find(specification, sortExpression);
    					
    					// Convert to data contracts
                        List<StandardReportDC> destinations = mappingService.Map<IEnumerable<StandardReport>, List<StandardReportDC>>(dataEntities);
    
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
    
            #region GetStandardReport
    	
    		/// <summary>
        	/// Retrieve a StandardReport with associated lookups
        	/// </summary>
        	/// <param name="currentUser"></param>
        	/// <param name="user"></param>
        	/// <param name="appID"></param>
        	/// <param name="overrideID"></param>
        	/// <param name="code"></param>
        	/// <returns></returns>
            public StandardReportVMDC GetStandardReport(string currentUser, string user, string appID, string overrideID, string code)
            {
    			//Create ExceptionManager
    			IExceptionManager exceptionManager = new ExceptionManager();
    			
    			//Create MappingService
    			IMappingService mappingService = new MappingService();
    			
    			// Create unit of work
                IUnitOfWork uow = new UnitOfWork(currentUser);
    
    			// Create repository
                IRepository<StandardReport> dataRepository = new Repository<StandardReport>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Create repositories for lookup data
    			IRepository<ReportCategory> reportCategoryRepository = new Repository<ReportCategory>(uow.ObjectContext, currentUser, user, appID, overrideID);
    
    			// Call overload with injected objects
                return GetStandardReport(currentUser, user, appID, overrideID, code, uow, dataRepository
    			, reportCategoryRepository
    			, exceptionManager, mappingService);
            }
    
    		/// <summary>
            /// Retrieve a StandardReport with associated lookups
            /// </summary>
            /// <param name="currentUser"></param>
            /// <param name="user"></param>
            /// <param name="appID"></param>
            /// <param name="overrideID"></param>
            /// <param name="code"></param>
            /// <param name="dataRepository"></param>
            /// <param name="uow"></param>
            /// <returns></returns>
            public StandardReportVMDC GetStandardReport(string currentUser, string user, string appID, string overrideID, string code, IUnitOfWork uow, IRepository<StandardReport> dataRepository
    			,IRepository<ReportCategory> reportCategoryRepository
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
    				
    					StandardReportDC destination = null;
    					
    					// If code is null then just return supporting lists
    					if (!string.IsNullOrEmpty(code))
    					{
    						// Convert code to Guid
    	                    Guid codeGuid = Guid.Parse(code);
    
    						// Retrieve specific StandardReport
    	                    StandardReport dataEntity = dataRepository.Single(x => x.Code == codeGuid);
    
    						// Convert to data contract for passing through service interface
    	                    destination = mappingService.Map<StandardReport, StandardReportDC>(dataEntity);
    					}
    
    					IEnumerable<ReportCategory> reportCategoryList = reportCategoryRepository.GetAll(x => new {x.Description});
    
    					List<ReportCategoryDC> reportCategoryDestinationList = mappingService.Map<List<ReportCategoryDC>>(reportCategoryList);
    
        				// Create aggregate contract
                        StandardReportVMDC returnObject = new StandardReportVMDC();
    
                        returnObject.StandardReportItem = destination;
    					returnObject.ReportCategoryList = reportCategoryDestinationList;
                        
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
