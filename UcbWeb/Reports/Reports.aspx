<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="UcbWeb.Reports.Reports" %>
<%@ Assembly Name="Dwp.Adep.Ucb.ResourceLibrary" %>
<%@ Assembly Name="UcbWeb" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html >

<html lang="<%= Dwp.Adep.Ucb.ResourceLibrary.Resources.HTML_LANG %>">
<head runat="server">
    <meta charset="utf-8" />
    <title><%= Title%></title>
    <link href="../Content/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Content/Site.Extension.css" rel="stylesheet" type="text/css" />
    <link href="../Content/themes/start/jquery-ui-1.8.18.custom.css" rel="stylesheet" type="text/css" />
    <link href="../Content/themes/base/selectmenu/jquery.ui.selectmenu.css" rel="stylesheet" type="text/css" />
    <link href="../Content/JQueryOverwrite.css" rel="stylesheet" type="text/css" />
    <!--[if IE 6]>
    <link href="../Content/IE6.css" rel="stylesheet" type="text/css" />
    <![endif]]-->
        <script type="text/javascript">
            var rootPath = '../';
    </script>
    <script type="text/javascript" src="../Scripts/modernizr.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.17.min.js"></script>
        <script type="text/javascript" src="../Scripts/selectmenu/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../Scripts/selectmenu/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="../Scripts/selectmenu/jquery.ui.position.js"></script>
    <script type="text/javascript" src="../Scripts/selectmenu/jquery.ui.selectmenu.js"></script>
    <script type="text/javascript" src="../Scripts/custom/general.js"></script>
    <script type="text/javascript" src="../Scripts/custom/general.extension.js"></script>
    <!--[if IE 6]>
    <script type="text/javascript" src="../Scripts/custom/IE6.js"></script>
    <![endif]]-->

</head>

<body>
    <div class="page">
        <header class="row">
        <a accesskey="s" href="#main" title="Skip navigation"></a>
             <div id="title" class="row">
                    <div class="left"><img src="../Content/images/dwp_logo.png" height="35" width="393"  alt="" /></div>
                    <div id="logindisplay" class="right text-align-right">
                        <%= Dwp.Adep.Ucb.ResourceLibrary.Resources.LABEL_WELCOME %> <strong><%=UserName%></strong>!
                    </div>
            </div>

                <h1 class="row text-align-centre"><%= Dwp.Adep.Ucb.ResourceLibrary.Resources.SYS_NAME %></h1>
                <h3 class="row text-align-centre"><%= Title %></h3>
         <% if (!string.IsNullOrEmpty(UserID) && UserID.Length == 36)
            {%>
                <nav class="navbar">
                    <div class="clearfix navbar-inner">
                        <ul id="menu" class="nav">
                            <li id="menu-dropdown-li"><a href="#openMenu">Menu<span class="caret ui-icon ui-icon-carat-1-s"></span></a>
                                 <%= UcbWeb.Helpers.MenuHelper.GetMenu(UserName)%>
                            </li>
                            <li><a href='<%=System.Configuration.ConfigurationManager.AppSettings["LandingPageUrl"]%>'><%= Dwp.Adep.Ucb.ResourceLibrary.Resources.LINKTEXT_YOURAPPS%></a></li>
                            <li><a accesskey="1" href="../Home" title="Displays the home screen">Home</a></li>
                            <%if (HttpContext.Current.User.IsInRole(UcbWeb.Models.AppRoles.ADMIN))
                            {%>
                                <li><a href="../Admin/AdminMenu"><%= Dwp.Adep.Ucb.ResourceLibrary.Resources.LINKTEXT_ADMIN%></a></li>
                          <%}%>
                            <%if (HttpContext.Current.User.IsInRole(UcbWeb.Models.AppRoles.STAFFADMIN))
                            {%>
                                <li><a href='<%=System.Configuration.ConfigurationManager.AppSettings["StaffAdminPageUrl"]%>'><%= Dwp.Adep.Ucb.ResourceLibrary.Resources.LINKTEXT_STAFFADMIN%></a></li>
                          <%}%>
                        </ul>
                    </div>
                </nav>
         <%} %>
        </header>
        <section id="main">
            <form id="form1" runat="server">
           
                <asp:scriptmanager ID="Scriptmanager1" runat="server" AsyncPostBackTimeout="600"></asp:scriptmanager>
                <div id="Message" class='message' runat="server">Message</div>
       <div>
                <rsweb:ReportViewer id="OperationalReportViewer" runat="server" processingmode="Remote" BackColor="#EBEBEB"
                         AsyncRendering="true" Width="100%" Height="100%">
                </rsweb:ReportViewer>
      </div>
            </form>
        </section>
			<div id="sitewidelinks" class="text-align-right row">
				<a href="../Home/About" title="Displays the about screen">About</a>
			</div>
        <footer>
            <%=  Dwp.Adep.Ucb.ResourceLibrary.Resources.LABEL_PAGECREATED + " " + DateTime.Now.ToString("dd/MM/yyyy") + " at " + DateTime.Now.ToString("HH:mm:ss") %>
            
        </footer>
    </div>
</body>
</html>


    
