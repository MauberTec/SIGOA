using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    ///  Acesso Negado
    /// </summary>
    public class AcessoNegadoController : Controller
    {
        /// <summary>
        /// Acesso Negado
        /// </summary>
        /// <returns>View</returns>
        public ActionResult AcessoNegado()
        {
            return View();
        }
    }
}