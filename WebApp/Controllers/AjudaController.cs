using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Manual do Usuario
    /// </summary>
    public class AjudaController : Controller
    {
        /// <summary>
        /// Pagina inicial
        /// </summary>
        /// <returns></returns>
        // GET: Manual
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// manual de anomalia
        /// </summary>
        /// <returns></returns>
        public ActionResult Anomalias()
        {
            return View();
        }
        /// <summary>
        /// Reparos
        /// </summary>
        /// <returns></returns>
        public ActionResult Reparos()
        {
            return View();
        }
        /// <summary>
        /// Conservas
        /// </summary>
        /// <returns></returns>
        public ActionResult Conservas()
        {
            return View();
        }
        /// <summary>
        /// Documentos
        /// </summary>
        /// <returns></returns>
        public ActionResult Documentos()
        {
            return View();
        }
    }
}