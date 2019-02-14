using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Seguridad.Atributos 
{
    public class SeguridadWebGasolinerasAttribute: AuthorizeAttribute 
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //Esta clase sirve para redireccionar al usuario cuando no cumple lo que yo quiero
            base.OnAuthorization(filterContext);


            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                GenericPrincipal usuario = filterContext.HttpContext.User as GenericPrincipal;
                if (this.Roles.Count() > 0)
                {
                    int contador = 0;
                    String[] RolesPermitidos = this.Roles.Split(',');
                    foreach (String s in RolesPermitidos)
                    {

                        if (usuario.IsInRole(s) == true)
                        {
                            //filterContext.Result = GetRuta("Home", "Home",true);
                            break;
                        }
                        contador++;
                    }
                    if (contador == RolesPermitidos.Count())
                    {
                        filterContext.Result = GetRuta("Home", "ZonaProhibida");
                    }
                }
            }
            else
            {
                filterContext.Result = this.GetRuta("ControlSeguridad", "Login");
            }
        }
        public RedirectToRouteResult GetRuta(String controlador, String accion)
        {
            RouteValueDictionary ruta; //A que controller y a que action
            RedirectToRouteResult direccion; //Ejecutar la redireccion
            ruta = new RouteValueDictionary(new { controller = controlador, action = accion });
            direccion = new RedirectToRouteResult(ruta);
            return direccion;
        }
    }
}