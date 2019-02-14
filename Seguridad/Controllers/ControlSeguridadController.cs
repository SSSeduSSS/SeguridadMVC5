using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Seguridad.Controllers
{
    public class ControlSeguridadController : Controller
    {
        // GET: ControlSeguridad
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]        
        public ActionResult Login(String nombre, String passw)
        {
            String rol = "";
           
            if (nombre == "usuario" && passw == "usuario")
            {
                rol = nombre;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                   (1, nombre, DateTime.Now, DateTime.Now.AddMinutes(5), true, rol);
                String cifrado = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie("TicketUsuario", cifrado);
                Response.Cookies.Add(cookie);
                ViewBag.Login = "Correcto";

                return RedirectToAction("UsuariosLogueados", "Home");
            }
            else if (nombre == "admin" && passw == "admin")
            {
                rol = nombre;

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket
                   (1, nombre, DateTime.Now, DateTime.Now.AddMinutes(5), true, rol);
                String cifrado = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie("TicketUsuario", cifrado);
                Response.Cookies.Add(cookie);
                ViewBag.Login = "Correcto";

                return RedirectToAction("Trabajadores", "Home");

            }
            else {
                ViewBag.Login = "Credenciales incorrectas";
                return View();
            }
        }


        public ActionResult CerrarSesion()
        {
            //Debemos quitar al usuario y su identidad
            HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
            //Salir de la autentificación
            FormsAuthentication.SignOut();
            //Recuperar la cookie y caducarla
            HttpCookie cookie = Request.Cookies["TicketUsuario"];
            cookie.Expires = DateTime.Now.AddYears(-1);
            //Volver a escribir la cookie
            Response.Cookies.Add(cookie);
            //Donde me lo llevo? => Tengo todo protegido con login.
            return RedirectToAction("Login");
        }
    }
}
