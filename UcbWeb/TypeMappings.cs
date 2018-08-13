using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using UcbWeb.Models;
using UcbWeb.UcbService;

namespace UcbWeb
{
    public partial class TypeMappings
    {
        public static void DefineTypeMappings()
        {
            DefineTypeMappingsGenerated();

            Mapper.CreateMap<TransferSiteDC, TransferSiteModel>();
            Mapper.CreateMap<TransferSiteSearchCriteriaDC, TransferSiteSearchCriteriaModel>();
            Mapper.CreateMap<TransferSiteSearchCriteriaModel, TransferSiteSearchCriteriaDC>();

            Mapper.CreateMap<OrganisationSearchCriteriaDC, OrganisationSearchCriteriaModel>();
            Mapper.CreateMap<OrganisationSearchCriteriaModel, OrganisationSearchCriteriaDC>();
        }

        static partial void DefineTypeMappingsGenerated();
    }
}