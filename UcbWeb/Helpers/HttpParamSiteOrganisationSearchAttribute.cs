﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UcbWeb.Helpers
{
    public class HttpParamSiteOrganisationSearchAttribute : ActionNameSelectorAttribute
    {
        public override bool IsValidName(ControllerContext controllerContext, string actionName, System.Reflection.MethodInfo methodInfo)
        {
            if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            if (!actionName.Equals("SiteOrganisationSearch", StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            var request = controllerContext.RequestContext.HttpContext.Request;
            foreach (string item in request.Form.Keys)
            {
                if (item.StartsWith(Prefix + methodInfo.Name))
                {
                    return true;
                }
            }
            return request[Prefix + methodInfo.Name] != null && !controllerContext.IsChildAction;
        }
        public string Prefix = "Search::";
    }
}