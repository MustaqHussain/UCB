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
using AutoMapper;
using UcbWeb.Models;
using UcbWeb.UcbService;

namespace UcbWeb
{
    
    public partial class TypeMappings
    {
        static partial void DefineTypeMappingsGenerated()
        {
    			Mapper.CreateMap<IntranetStaffProtectionResult, IntranetStaffProtectionModel>();
    
                Mapper.CreateMap<AbuseTypeModel, AbuseTypeDC>();
                Mapper.CreateMap<AbuseTypeDC, AbuseTypeModel>();
    			Mapper.CreateMap<AbuseTypeSearchMatchModel, AbuseTypeSearchMatchDC>();
                Mapper.CreateMap<AbuseTypeSearchMatchDC, AbuseTypeSearchMatchModel>();
    
                Mapper.CreateMap<ADRoleLookupModel, ADRoleLookupDC>();
                Mapper.CreateMap<ADRoleLookupDC, ADRoleLookupModel>();
    			Mapper.CreateMap<ADRoleLookupSearchMatchModel, ADRoleLookupSearchMatchDC>();
                Mapper.CreateMap<ADRoleLookupSearchMatchDC, ADRoleLookupSearchMatchModel>();
    
                Mapper.CreateMap<ApplicationModel, ApplicationDC>();
                Mapper.CreateMap<ApplicationDC, ApplicationModel>();
    			Mapper.CreateMap<ApplicationSearchMatchModel, ApplicationSearchMatchDC>();
                Mapper.CreateMap<ApplicationSearchMatchDC, ApplicationSearchMatchModel>();
    
                Mapper.CreateMap<ApplicationAttributeModel, ApplicationAttributeDC>();
                Mapper.CreateMap<ApplicationAttributeDC, ApplicationAttributeModel>();
    			Mapper.CreateMap<ApplicationAttributeSearchMatchModel, ApplicationAttributeSearchMatchDC>();
                Mapper.CreateMap<ApplicationAttributeSearchMatchDC, ApplicationAttributeSearchMatchModel>();
    
                Mapper.CreateMap<ApplicationOrganisationTypeGroupModel, ApplicationOrganisationTypeGroupDC>();
                Mapper.CreateMap<ApplicationOrganisationTypeGroupDC, ApplicationOrganisationTypeGroupModel>();
    			Mapper.CreateMap<ApplicationOrganisationTypeGroupSearchMatchModel, ApplicationOrganisationTypeGroupSearchMatchDC>();
                Mapper.CreateMap<ApplicationOrganisationTypeGroupSearchMatchDC, ApplicationOrganisationTypeGroupSearchMatchModel>();
    
                Mapper.CreateMap<AttachmentModel, AttachmentDC>();
                Mapper.CreateMap<AttachmentDC, AttachmentModel>();
    			Mapper.CreateMap<AttachmentSearchMatchModel, AttachmentSearchMatchDC>();
                Mapper.CreateMap<AttachmentSearchMatchDC, AttachmentSearchMatchModel>();
    
                Mapper.CreateMap<AttachmentDataModel, AttachmentDataDC>();
                Mapper.CreateMap<AttachmentDataDC, AttachmentDataModel>();
    			Mapper.CreateMap<AttachmentDataSearchMatchModel, AttachmentDataSearchMatchDC>();
                Mapper.CreateMap<AttachmentDataSearchMatchDC, AttachmentDataSearchMatchModel>();
    
                Mapper.CreateMap<ContentModel, ContentDC>();
                Mapper.CreateMap<ContentDC, ContentModel>();
    			Mapper.CreateMap<ContentSearchMatchModel, ContentSearchMatchDC>();
                Mapper.CreateMap<ContentSearchMatchDC, ContentSearchMatchModel>();
    
                Mapper.CreateMap<ContingencyArrangementModel, ContingencyArrangementDC>();
                Mapper.CreateMap<ContingencyArrangementDC, ContingencyArrangementModel>();
    			Mapper.CreateMap<ContingencyArrangementSearchMatchModel, ContingencyArrangementSearchMatchDC>();
                Mapper.CreateMap<ContingencyArrangementSearchMatchDC, ContingencyArrangementSearchMatchModel>();
    
                Mapper.CreateMap<ControlMeasureModel, ControlMeasureDC>();
                Mapper.CreateMap<ControlMeasureDC, ControlMeasureModel>();
    			Mapper.CreateMap<ControlMeasureSearchMatchModel, ControlMeasureSearchMatchDC>();
                Mapper.CreateMap<ControlMeasureSearchMatchDC, ControlMeasureSearchMatchModel>();
    
                Mapper.CreateMap<CustomerModel, CustomerDC>();
                Mapper.CreateMap<CustomerDC, CustomerModel>();
    			Mapper.CreateMap<CustomerSearchMatchModel, CustomerSearchMatchDC>();
                Mapper.CreateMap<CustomerSearchMatchDC, CustomerSearchMatchModel>();
    
                Mapper.CreateMap<CustomerContingencyArrangementModel, CustomerContingencyArrangementDC>();
                Mapper.CreateMap<CustomerContingencyArrangementDC, CustomerContingencyArrangementModel>();
    			Mapper.CreateMap<CustomerContingencyArrangementSearchMatchModel, CustomerContingencyArrangementSearchMatchDC>();
                Mapper.CreateMap<CustomerContingencyArrangementSearchMatchDC, CustomerContingencyArrangementSearchMatchModel>();
    
                Mapper.CreateMap<CustomerControlMeasureModel, CustomerControlMeasureDC>();
                Mapper.CreateMap<CustomerControlMeasureDC, CustomerControlMeasureModel>();
    			Mapper.CreateMap<CustomerControlMeasureSearchMatchModel, CustomerControlMeasureSearchMatchDC>();
                Mapper.CreateMap<CustomerControlMeasureSearchMatchDC, CustomerControlMeasureSearchMatchModel>();
    
                Mapper.CreateMap<EventLeadingToIncidentModel, EventLeadingToIncidentDC>();
                Mapper.CreateMap<EventLeadingToIncidentDC, EventLeadingToIncidentModel>();
    			Mapper.CreateMap<EventLeadingToIncidentSearchMatchModel, EventLeadingToIncidentSearchMatchDC>();
                Mapper.CreateMap<EventLeadingToIncidentSearchMatchDC, EventLeadingToIncidentSearchMatchModel>();
    
                Mapper.CreateMap<GradeModel, GradeDC>();
                Mapper.CreateMap<GradeDC, GradeModel>();
    			Mapper.CreateMap<GradeSearchMatchModel, GradeSearchMatchDC>();
                Mapper.CreateMap<GradeSearchMatchDC, GradeSearchMatchModel>();
    
                Mapper.CreateMap<IncidentModel, IncidentDC>();
                Mapper.CreateMap<IncidentDC, IncidentModel>();
    			Mapper.CreateMap<IncidentSearchMatchModel, IncidentSearchMatchDC>();
                Mapper.CreateMap<IncidentSearchMatchDC, IncidentSearchMatchModel>();
    
                Mapper.CreateMap<IncidentCategoryModel, IncidentCategoryDC>();
                Mapper.CreateMap<IncidentCategoryDC, IncidentCategoryModel>();
    			Mapper.CreateMap<IncidentCategorySearchMatchModel, IncidentCategorySearchMatchDC>();
                Mapper.CreateMap<IncidentCategorySearchMatchDC, IncidentCategorySearchMatchModel>();
    
                Mapper.CreateMap<IncidentDetailModel, IncidentDetailDC>();
                Mapper.CreateMap<IncidentDetailDC, IncidentDetailModel>();
    			Mapper.CreateMap<IncidentDetailSearchMatchModel, IncidentDetailSearchMatchDC>();
                Mapper.CreateMap<IncidentDetailSearchMatchDC, IncidentDetailSearchMatchModel>();
    
                Mapper.CreateMap<IncidentInterestedPartyModel, IncidentInterestedPartyDC>();
                Mapper.CreateMap<IncidentInterestedPartyDC, IncidentInterestedPartyModel>();
    			Mapper.CreateMap<IncidentInterestedPartySearchMatchModel, IncidentInterestedPartySearchMatchDC>();
                Mapper.CreateMap<IncidentInterestedPartySearchMatchDC, IncidentInterestedPartySearchMatchModel>();
    
                Mapper.CreateMap<IncidentLinkModel, IncidentLinkDC>();
                Mapper.CreateMap<IncidentLinkDC, IncidentLinkModel>();
    			Mapper.CreateMap<IncidentLinkSearchMatchModel, IncidentLinkSearchMatchDC>();
                Mapper.CreateMap<IncidentLinkSearchMatchDC, IncidentLinkSearchMatchModel>();
    
                Mapper.CreateMap<IncidentLocationModel, IncidentLocationDC>();
                Mapper.CreateMap<IncidentLocationDC, IncidentLocationModel>();
    			Mapper.CreateMap<IncidentLocationSearchMatchModel, IncidentLocationSearchMatchDC>();
                Mapper.CreateMap<IncidentLocationSearchMatchDC, IncidentLocationSearchMatchModel>();
    
                Mapper.CreateMap<IncidentSystemMarkedModel, IncidentSystemMarkedDC>();
                Mapper.CreateMap<IncidentSystemMarkedDC, IncidentSystemMarkedModel>();
    			Mapper.CreateMap<IncidentSystemMarkedSearchMatchModel, IncidentSystemMarkedSearchMatchDC>();
                Mapper.CreateMap<IncidentSystemMarkedSearchMatchDC, IncidentSystemMarkedSearchMatchModel>();
    
                Mapper.CreateMap<IncidentTypeModel, IncidentTypeDC>();
                Mapper.CreateMap<IncidentTypeDC, IncidentTypeModel>();
    			Mapper.CreateMap<IncidentTypeSearchMatchModel, IncidentTypeSearchMatchDC>();
                Mapper.CreateMap<IncidentTypeSearchMatchDC, IncidentTypeSearchMatchModel>();
    
                Mapper.CreateMap<IncidentUpdateEventModel, IncidentUpdateEventDC>();
                Mapper.CreateMap<IncidentUpdateEventDC, IncidentUpdateEventModel>();
    			Mapper.CreateMap<IncidentUpdateEventSearchMatchModel, IncidentUpdateEventSearchMatchDC>();
                Mapper.CreateMap<IncidentUpdateEventSearchMatchDC, IncidentUpdateEventSearchMatchModel>();
    
                Mapper.CreateMap<InterestedPartyModel, InterestedPartyDC>();
                Mapper.CreateMap<InterestedPartyDC, InterestedPartyModel>();
    			Mapper.CreateMap<InterestedPartySearchMatchModel, InterestedPartySearchMatchDC>();
                Mapper.CreateMap<InterestedPartySearchMatchDC, InterestedPartySearchMatchModel>();
    
                Mapper.CreateMap<IntroductoryInformationModel, IntroductoryInformationDC>();
                Mapper.CreateMap<IntroductoryInformationDC, IntroductoryInformationModel>();
    			Mapper.CreateMap<IntroductoryInformationSearchMatchModel, IntroductoryInformationSearchMatchDC>();
                Mapper.CreateMap<IntroductoryInformationSearchMatchDC, IntroductoryInformationSearchMatchModel>();
    
                Mapper.CreateMap<JobRoleModel, JobRoleDC>();
                Mapper.CreateMap<JobRoleDC, JobRoleModel>();
    			Mapper.CreateMap<JobRoleSearchMatchModel, JobRoleSearchMatchDC>();
                Mapper.CreateMap<JobRoleSearchMatchDC, JobRoleSearchMatchModel>();
    
                Mapper.CreateMap<LinkedCustomerModel, LinkedCustomerDC>();
                Mapper.CreateMap<LinkedCustomerDC, LinkedCustomerModel>();
    			Mapper.CreateMap<LinkedCustomerSearchMatchModel, LinkedCustomerSearchMatchDC>();
                Mapper.CreateMap<LinkedCustomerSearchMatchDC, LinkedCustomerSearchMatchModel>();
    
                Mapper.CreateMap<NarrativeModel, NarrativeDC>();
                Mapper.CreateMap<NarrativeDC, NarrativeModel>();
    			Mapper.CreateMap<NarrativeSearchMatchModel, NarrativeSearchMatchDC>();
                Mapper.CreateMap<NarrativeSearchMatchDC, NarrativeSearchMatchModel>();
    
                Mapper.CreateMap<OrganisationModel, OrganisationDC>();
                Mapper.CreateMap<OrganisationDC, OrganisationModel>();
    			Mapper.CreateMap<OrganisationSearchMatchModel, OrganisationSearchMatchDC>();
                Mapper.CreateMap<OrganisationSearchMatchDC, OrganisationSearchMatchModel>();
    
                Mapper.CreateMap<OrganisationHierarchyModel, OrganisationHierarchyDC>();
                Mapper.CreateMap<OrganisationHierarchyDC, OrganisationHierarchyModel>();
    			Mapper.CreateMap<OrganisationHierarchySearchMatchModel, OrganisationHierarchySearchMatchDC>();
                Mapper.CreateMap<OrganisationHierarchySearchMatchDC, OrganisationHierarchySearchMatchModel>();
    
                Mapper.CreateMap<OrganisationTypeModel, OrganisationTypeDC>();
                Mapper.CreateMap<OrganisationTypeDC, OrganisationTypeModel>();
    			Mapper.CreateMap<OrganisationTypeSearchMatchModel, OrganisationTypeSearchMatchDC>();
                Mapper.CreateMap<OrganisationTypeSearchMatchDC, OrganisationTypeSearchMatchModel>();
    
                Mapper.CreateMap<OrganisationTypeGroupModel, OrganisationTypeGroupDC>();
                Mapper.CreateMap<OrganisationTypeGroupDC, OrganisationTypeGroupModel>();
    			Mapper.CreateMap<OrganisationTypeGroupSearchMatchModel, OrganisationTypeGroupSearchMatchDC>();
                Mapper.CreateMap<OrganisationTypeGroupSearchMatchDC, OrganisationTypeGroupSearchMatchModel>();
    
                Mapper.CreateMap<ReferrerModel, ReferrerDC>();
                Mapper.CreateMap<ReferrerDC, ReferrerModel>();
    			Mapper.CreateMap<ReferrerSearchMatchModel, ReferrerSearchMatchDC>();
                Mapper.CreateMap<ReferrerSearchMatchDC, ReferrerSearchMatchModel>();
    
                Mapper.CreateMap<RelationshipToCustomerModel, RelationshipToCustomerDC>();
                Mapper.CreateMap<RelationshipToCustomerDC, RelationshipToCustomerModel>();
    			Mapper.CreateMap<RelationshipToCustomerSearchMatchModel, RelationshipToCustomerSearchMatchDC>();
                Mapper.CreateMap<RelationshipToCustomerSearchMatchDC, RelationshipToCustomerSearchMatchModel>();
    
                Mapper.CreateMap<ReportCategoryModel, ReportCategoryDC>();
                Mapper.CreateMap<ReportCategoryDC, ReportCategoryModel>();
    			Mapper.CreateMap<ReportCategorySearchMatchModel, ReportCategorySearchMatchDC>();
                Mapper.CreateMap<ReportCategorySearchMatchDC, ReportCategorySearchMatchModel>();
    
                Mapper.CreateMap<RoleModel, RoleDC>();
                Mapper.CreateMap<RoleDC, RoleModel>();
    			Mapper.CreateMap<RoleSearchMatchModel, RoleSearchMatchDC>();
                Mapper.CreateMap<RoleSearchMatchDC, RoleSearchMatchModel>();
    
                Mapper.CreateMap<SiteModel, SiteDC>();
                Mapper.CreateMap<SiteDC, SiteModel>();
    			Mapper.CreateMap<SiteSearchMatchModel, SiteSearchMatchDC>();
                Mapper.CreateMap<SiteSearchMatchDC, SiteSearchMatchModel>();
    
                Mapper.CreateMap<SiteStaffModel, SiteStaffDC>();
                Mapper.CreateMap<SiteStaffDC, SiteStaffModel>();
    			Mapper.CreateMap<SiteStaffSearchMatchModel, SiteStaffSearchMatchDC>();
                Mapper.CreateMap<SiteStaffSearchMatchDC, SiteStaffSearchMatchModel>();
    
                Mapper.CreateMap<StaffModel, StaffDC>();
                Mapper.CreateMap<StaffDC, StaffModel>();
    			Mapper.CreateMap<StaffSearchMatchModel, StaffSearchMatchDC>();
                Mapper.CreateMap<StaffSearchMatchDC, StaffSearchMatchModel>();
    
                Mapper.CreateMap<StaffAttributesModel, StaffAttributesDC>();
                Mapper.CreateMap<StaffAttributesDC, StaffAttributesModel>();
    			Mapper.CreateMap<StaffAttributesSearchMatchModel, StaffAttributesSearchMatchDC>();
                Mapper.CreateMap<StaffAttributesSearchMatchDC, StaffAttributesSearchMatchModel>();
    
                Mapper.CreateMap<StaffOrganisationModel, StaffOrganisationDC>();
                Mapper.CreateMap<StaffOrganisationDC, StaffOrganisationModel>();
    			Mapper.CreateMap<StaffOrganisationSearchMatchModel, StaffOrganisationSearchMatchDC>();
                Mapper.CreateMap<StaffOrganisationSearchMatchDC, StaffOrganisationSearchMatchModel>();
    
                Mapper.CreateMap<StandardReportModel, StandardReportDC>();
                Mapper.CreateMap<StandardReportDC, StandardReportModel>();
    			Mapper.CreateMap<StandardReportSearchMatchModel, StandardReportSearchMatchDC>();
                Mapper.CreateMap<StandardReportSearchMatchDC, StandardReportSearchMatchModel>();
    
                Mapper.CreateMap<SystemMarkedModel, SystemMarkedDC>();
                Mapper.CreateMap<SystemMarkedDC, SystemMarkedModel>();
    			Mapper.CreateMap<SystemMarkedSearchMatchModel, SystemMarkedSearchMatchDC>();
                Mapper.CreateMap<SystemMarkedSearchMatchDC, SystemMarkedSearchMatchModel>();
    
                Mapper.CreateMap<SystemParameterModel, SystemParameterDC>();
                Mapper.CreateMap<SystemParameterDC, SystemParameterModel>();
    			Mapper.CreateMap<SystemParameterSearchMatchModel, SystemParameterSearchMatchDC>();
                Mapper.CreateMap<SystemParameterSearchMatchDC, SystemParameterSearchMatchModel>();
    	}
    }
}
