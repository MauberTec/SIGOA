using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// PoliticaReparosController
    /// </summary>
    public class PoliticaReparosController : Controller
    {

        /// <summary>
        /// Index
        /// </summary>
        // GET: PoliticaReparos
        public ActionResult Index()
        {
            return View();
        }
    }
}