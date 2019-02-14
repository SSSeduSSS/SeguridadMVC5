using Seguridad.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Seguridad.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult TodosUsuarios()
        {
            return View();
        }

        [SeguridadWebGasolineras(Roles = "admin,usuario")]
        public ActionResult UsuariosLogueados()
        {
            return View();
        }

        [SeguridadWebGasolineras(Roles = "admin")]
        public ActionResult Trabajadores()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ZonaProhibida() {
            return View();
        }

    }
}
