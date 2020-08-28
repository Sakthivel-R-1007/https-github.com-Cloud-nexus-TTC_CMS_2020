using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Tampines.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-Powered-By");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;
            Application["ApplicationPath"] = FirstRequestInitialisation.Initialise(context);
            Application["MinifyAndBundle"] = "true";

#if !DEBUG
            //if (!HttpContext.Current.Request.Url.Host.Contains("www") || !HttpContext.Current.Request.Url.OriginalString.Contains("https"))
            //    Response.Redirect(Application["ApplicationPath"].ToString());
            if (!HttpContext.Current.Request.Url.OriginalString.Contains("https"))
                Response.Redirect(Application["ApplicationPath"].ToString());
#endif

        }

        void Appication_EndRequest(Object source, EventArgs e)
        {
            if (Response.Cookies.Count > 0)
            {
                foreach (string s in Response.Cookies.AllKeys)
                {
                    if (s == FormsAuthentication.FormsCookieName || s.ToLower() == "asp.net_sessionid")
                    {
                        Response.Cookies[s].Secure = true;
                    }
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity identity)
                    {
                        FormsAuthenticationTicket ticket = identity.Ticket;
                        string[] roles = ticket.UserData.Split(',');
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(identity, roles);
                    }
                }
            }
        }

        class FirstRequestInitialisation
        {
            private static string host = null;
            private static Object s_lock = new Object();
            public static string Initialise(HttpContext context)
            {
                if (string.IsNullOrEmpty(host))
                {
                    lock (s_lock)
                    {
                        if (string.IsNullOrEmpty(host))
                        {
                            Uri uri = EnvironmentSpecificRequestUrl(new HttpRequestWrapper(HttpContext.Current.Request));
#if DEBUG
                            //Dev

                            host = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;

#else
                            //staging
                            //host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;

                            //Production
                            //host = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
                           
                            
#endif
                        }
                    }
                }
                return host;
            }
        }

        private static Uri EnvironmentSpecificRequestUrl(HttpRequestBase request)
        {

            UriBuilder hostUrl = new UriBuilder();
            string hostHeader = request.Headers["Host"];

            if (hostHeader.Contains(":"))
            {
                hostUrl.Host = hostHeader.Split(':')[0];
                hostUrl.Port = Convert.ToInt32(hostHeader.Split(':')[1]);
            }
            else
            {
                hostUrl.Host = hostHeader;
                hostUrl.Port = -1;
            }

            Uri url = request.Url;
            UriBuilder uriBuilder = new UriBuilder(url);

            if (String.Equals(hostUrl.Host, "localhost", StringComparison.OrdinalIgnoreCase) || hostUrl.Host == "127.0.0.1")
            {
                // Do nothing
                // When we're running the application from localhost (e.g. debugging from Visual Studio), we'll keep everything as it is.
                // We're not using request.IsLocal because it returns true as long as the request sender and receiver are in same machine.
                // What we want is to only ignore the requests to 'localhost' or the loopback IP '127.0.0.1'.
                return uriBuilder.Uri;
            }

            // When the application is run behind a load-balancer (or forward proxy), request.IsSecureConnection returns 'true' or 'false'
            // based on the request from the load-balancer to the web server (e.g. IIS) and not the actual request to the load-balancer.
            // The same is also applied to request.Url.Scheme (or uriBuilder.Scheme, as in our case).
            bool isSecureConnection = String.Equals(request.Headers["X-Forwarded-Proto"], "https", StringComparison.OrdinalIgnoreCase);

            if (isSecureConnection)
            {
                uriBuilder.Port = hostUrl.Port == -1 ? 443 : hostUrl.Port;
                uriBuilder.Scheme = "https";
            }
            else
            {
                uriBuilder.Port = hostUrl.Port == -1 ? 80 : hostUrl.Port;
                uriBuilder.Scheme = "http";
            }

            uriBuilder.Host = hostUrl.Host;

            return uriBuilder.Uri;
        }
    }
}
