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

    /// <summary>
    /// Service
    /// Class containing service behaviour for Incident
    /// </summary>
    public partial class UcbService
    {
        #region UcbService.GetPublishedReportsByCategory

        public List<PublishedReportsByCategory> GetPublishedReportsByCategory(string currentUser, string user, string appID, string overrideID)
        {
            // Create unit of work
            IUnitOfWork uow = new UnitOfWork(currentUser);

            // Create repository
            IRepository<ReportCategory> reportCategoryRepository = new Repository<ReportCategory>(uow.ObjectContext, currentUser, user, appID, overrideID);

            //Create ExceptionManager
            IExceptionManager exceptionManager = new ExceptionManager();

            return GetPublishedReportsByCategory(currentUser, user, appID, overrideID, reportCategoryRepository, uow, exceptionManager);

        }

        private List<PublishedReportsByCategory> GetPublishedReportsByCategory(string currentUser, string user, string appID, string overrideID,
            IRepository<ReportCategory> reportCategoryRepository,
            IUnitOfWork uow, IExceptionManager exceptionManager)
        {

            List<PublishedReportsByCategory> searchResult = new List<PublishedReportsByCategory>();
            try
            {

                using (uow)
                {

                    //ISpecification<Incident> incidentSpecification = new Specification<Incident>(x => x.Customer.NINO.Equals(nino) && x.IncidentStatus.Equals("Live"));

                    //var result = reportCategoryRepository.Find(incidentSpecification, x => x.Customer.FirstName, "Customer", "Customer.CustomerControlMeasure", "Customer.CustomerControlMeasure.ControlMeasure", "Customer.RelationshipToCustomer");

                    var result = reportCategoryRepository.Find(x => x.IsActive == true, "StandardReport");


                    foreach (var category in result)
                    {
                        PublishedReportsByCategory publishedReportByCategory = new PublishedReportsByCategory();
                        publishedReportByCategory.Category = category.Description;

                        var standardReports = Mapper.Map<List<StandardReportDC>>(category.StandardReport);
                        publishedReportByCategory.StandardReports = standardReports;
                        
                        searchResult.Add(publishedReportByCategory);                                              
                    }


                }
            }
            catch (Exception e)
            {
                //Prevent exception from propogating across the service interface
                exceptionManager.ShieldException(e);

                return null;
            }

            return searchResult;
        }
        #endregion
    }
}