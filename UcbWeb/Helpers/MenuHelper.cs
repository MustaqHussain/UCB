using System;
using System.Web.Mvc;
using Dwp.Adep.Ucb.ResourceLibrary;
using System.Web;
using System.Collections.Generic;
using System.Security.Principal;
using UcbWeb.Models;
using System.Web.Hosting;
using System.Web.Security;
using UcbWeb.UcbService;
using System.Configuration;

namespace UcbWeb.Helpers
{
    public class MenuHelper
    {
        public static IHtmlString GetNonJavaLinks(string userName)
        {
            IHtmlString html = GetMenu(userName);
            string output = html.ToString();
            //class="slide-out-div"
            output = output.Replace(@"<a class=""handle"" href=""nojs.html"">#</a>", "");
            output = output.Replace(@"class=""slide-out-div""", "");
            output = output.Replace(@"id=""accordion""", "");
            output = output.Replace(@"id=""accordionSub""", "");

            return new HtmlString(output);
        }
        public static IHtmlString GetMenu(string userName)
        {         
            string userId = new SessionManager().UserID;
            if (string.IsNullOrEmpty(userId) || userId.Length != 36)
                return new HtmlString(string.Empty);

            var slideOutDiv = new TagBuilder("div");
            slideOutDiv.AddCssClass("slide-out-div");

            var accordion = new TagBuilder("div");
            accordion.GenerateId("accordion");

            //create incident menu
            CreateMenu(accordion, Resources.LABEL_MENU_INCIDENT);

            //create Nominated manager Views
            if (HttpContext.Current.User.IsInRole(AppRoles.NOMINATED_MANAGER) || HttpContext.Current.User.IsInRole(AppRoles.BUSINESS_AREA_MANAGER) || HttpContext.Current.User.IsInRole(AppRoles.ADMIN))
            {
                CreateMenu(accordion, Resources.LABEL_MENU_NM_Views);
            }

            //create Deputy Nominated manager Views
            if (HttpContext.Current.User.IsInRole(AppRoles.DEPUTY_NOMINATED_MANAGER))
            {
                CreateMenu(accordion, Resources.LABEL_MENU_DNM_Views);
            }

            //create staff protection lit menu
            //***********Restricted so that only users with a role in the app can see this menu item***********
            if (HttpContext.Current.User.IsInRole(AppRoles.APPLICATION))
            {
                CreateMenu(accordion, Resources.LABEL_MENU_SPL);
            }

            //create ad hoc reporting menu
            if (HttpContext.Current.User.IsInRole(AppRoles.UCB_MI_USER))
            {
                CreateMenu(accordion, Resources.LABEL_MENU_AdHocReporting);
            }

            //loop through
            CreateSubMenu(accordion, "Published Reports", userName);            

            //Add sub menu
            //accordion.InnerHtml += accordionSub.ToString();
            slideOutDiv.InnerHtml += accordion.ToString();

            return new HtmlString(slideOutDiv.ToString());

        }

        private static void CreateSubMenu(TagBuilder accordion, string menu, string userName)
        {
            TagBuilder h3;            
            h3 = new TagBuilder("h3");

            var a1 = new TagBuilder("a");
            a1.Attributes["href"] = "#";
            a1.InnerHtml = menu;
            h3.InnerHtml += a1.ToString();

            TagBuilder h4;
            TagBuilder div;

            var accordionSub = new TagBuilder("div");
            var extraDiv = new TagBuilder("div");
            accordionSub.GenerateId("accordionSub");

            UcbServiceClient sc = new UcbServiceClient();

            try
            {
                var publishedReportsByCategory = sc.GetPublishedReportsByCategory(userName, userName, "Ucb", "");
                foreach (var category in publishedReportsByCategory)
                {

                    h4 = new TagBuilder("h4");

                    var a = new TagBuilder("a");
                    a.Attributes["href"] = "#";
                    a.InnerHtml = category.Category;

                    div = new TagBuilder("div");
                    var ul = new TagBuilder("ul");
                    ul.GenerateId("nav");

                    foreach (var li in CreateSubMenuItem(category.StandardReports))
                    {                        
                        ul.InnerHtml += li.ToString();
                    }


                    div.InnerHtml = ul.ToString();

                    h4.InnerHtml += a.ToString();

                    accordionSub.InnerHtml += h4.ToString();
                    accordionSub.InnerHtml += div.ToString();
                }
            }
            catch (Exception e)
            {
                string message = ExceptionManager.HandleException(e, sc);
            }

            finally 
            { 
                sc.Close(); 
            }
            

            accordion.InnerHtml += h3.ToString();
            extraDiv.InnerHtml += accordionSub.ToString();
            accordion.InnerHtml += extraDiv.ToString();


        }


        private static List<TagBuilder> CreateSubMenuItem(StandardReportDC[] reports)
        {
            var lis = new List<TagBuilder>();

            foreach (var report in reports)
            {
                var li = new TagBuilder("li");
                var lia = new TagBuilder("a");
                lia.Attributes["href"] = VirtualPathUtility.ToAbsolute("~/"+"PublishedReport/MIReport?ReportID=" + report.Code.ToString());
                lia.InnerHtml = report.ReportName;
                li.InnerHtml += lia.ToString();
                lis.Add(li);
            }

            return lis;
        }


        private static void CreateMenu(TagBuilder accordion, string menu)
        {
            TagBuilder h3;
            TagBuilder div;

            h3 = new TagBuilder("h3");

            var a = new TagBuilder("a");
            a.Attributes["href"] = "#";
            a.InnerHtml = menu;

            div = new TagBuilder("div");
            var ul = new TagBuilder("ul");
            ul.GenerateId("nav");

            foreach (var li in CreateMenuItem(menu))
            {
                ul.InnerHtml += li.ToString();
            }


            div.InnerHtml = ul.ToString();

            h3.InnerHtml += a.ToString();

            accordion.InnerHtml += h3.ToString();
            accordion.InnerHtml += div.ToString();

        }

        private static List<TagBuilder> CreateMenuItem(string menu)
        {
            var lis = new List<TagBuilder>();

            foreach (var url in GetUrls(menu))
            {
                var li = new TagBuilder("li");
                var lia = new TagBuilder("a");
                lia.Attributes["href"] = url.Href;
                lia.InnerHtml = url.Label;
                if (!string.IsNullOrEmpty(url.Target))
                    lia.Attributes["target"] = url.Target;
                li.InnerHtml += lia.ToString();
                lis.Add(li);
            }

            return lis;
        }

        private static List<Urls> GetUrls(string menu)
        {
            var urls = new List<Urls>();
            if (menu.Equals(Resources.LABEL_MENU_INCIDENT))
            {
                if (HttpContext.Current.User.IsInRole(AppRoles.NOMINATED_MANAGER) || HttpContext.Current.User.IsInRole(AppRoles.DEPUTY_NOMINATED_MANAGER) || HttpContext.Current.User.IsInRole(AppRoles.BUSINESS_AREA_MANAGER) || HttpContext.Current.User.IsInRole(AppRoles.ADMIN))
                {
                    urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Incident/Create"), Label = "Create new Incident" });
                    urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Incident/CreateReferral"), Label = "Create new Third Party Referral" });
                }
                else if (HttpContext.Current.User.IsInRole(AppRoles.READ_ONLY) || HttpContext.Current.User.IsInRole(AppRoles.TRADE_UNION))
                {
                    urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Incident/Create"), Label = "Create new Incident" });
                }
            }
            else if (menu.Equals(Resources.LABEL_MENU_NM_Views))
            {
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Searches/MyNewReports"), Label = Resources.LABEL_LINK_VIEWMYNEWREPORTS });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Searches/MyReviews"), Label = Resources.LABEL_LINK_VIEWMYREVIEWS });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Searches/MyForwardLook"), Label = Resources.LABEL_LINK_VIEWMYFORWARDLOOK });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByAllCases"), Label = Resources.LABEL_LINK_SPLBYALLCASES });

            }
            else if (menu.Equals(Resources.LABEL_MENU_DNM_Views))
            {
                //TODO:Dave's new url links here
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Searches/DeputyMyNewReports"), Label = Resources.LABEL_LINK_VIEWMYNEWREPORTS });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/Searches/DeputyMyReviews"), Label = Resources.LABEL_LINK_VIEWMYREVIEWS });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByAllCases"), Label = Resources.LABEL_LINK_SPLBYALLCASES });
            }

            else if (menu.Equals(Resources.LABEL_MENU_SPL))
            {
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByName"), Label = Resources.LABEL_LINK_SPLBYNAME });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByNino"), Label = Resources.LABEL_LINK_SPLBYNINO });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByPostcode"), Label = Resources.LABEL_LINK_SPLBYPOSTCODE });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByIncidentID"), Label = Resources.LABEL_LINK_SPLBYINCIDENTID });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByArchived"), Label = Resources.LABEL_LINK_SPLBYARCHIVED });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByBusinessUnit"), Label = Resources.LABEL_LINK_SPLBYBUSINESSUNIT });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByControlMeasure"), Label = Resources.LABEL_LINK_SPLBYCONTROLMEASURE });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByDistrict"), Label = Resources.LABEL_LINK_SPLBYDISTRICT });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLByRegion"), Label = Resources.LABEL_LINK_SPLBYREGION });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLBySite"), Label = Resources.LABEL_LINK_SPLBYSITE });
                urls.Add(new Urls() { Href = VirtualPathUtility.ToAbsolute("~/SPL/SPLBy3rdPartyReferrals"), Label = Resources.LABEL_LINK_SPLBY3RDPARTYREFERRALS });
            }
            else if (menu.Equals(Resources.LABEL_MENU_AdHocReporting))
            {
                urls.Add(new Urls() { Href = ConfigurationManager.AppSettings["AdHocReportingUrl"], Label = "Ad Hoc Reporting", Target = "_blank" });
            }

            return urls;
        }
    }
}
