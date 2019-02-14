using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Seguridad
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Application_PostAuthenticateRequest()
        {
            HttpCookie cookie = Request.Cookies["TicketUsuario"];
            if (cookie != null)
            {
                //Ya hemos pasado por login
                String datos = cookie.Value;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(datos);

                String role = ticket.UserData;
                String username = ticket.Name;
                GenericIdentity identidad = new GenericIdentity(username);

                //Creamos usuario genérico 
                GenericPrincipal usuario = new GenericPrincipal(identidad, new String[] { role });

                HttpContext.Current.User = usuario;
            }
        }
    }
}
