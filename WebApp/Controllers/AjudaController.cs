using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    /// <summary>
    /// Ajuda do Usuario
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
        /// <summary>
        /// Objetos
        /// </summary>
        /// <returns></returns>
        public ActionResult Objetos()
        {
            return View();
        }
        /// <summary>
        /// Menu Lateral
        /// </summary>
        /// <returns></returns>
        public ActionResult MenuLateral()
        {
            return View();
        }
        /// <summary>
        /// Ordens de Serviço
        /// </summary>
        /// <returns></returns>
        public ActionResult OrdensServico()
        {
            return View();
        }
        /// <summary>
        /// Inspeções
        /// </summary>
        /// <returns></returns>
        public ActionResult Inspecao()
        {
            return View();
        }

        /// <summary>
        /// Integração
        /// </summary>
        /// <returns></returns>
        public ActionResult Integracao()
        {
            return View();
        }

        /// <summary>
        /// Manuais Sigoa
        /// </summary>
        /// <returns></returns>
        public ActionResult ManuaisSigoa()
        {
            return View();
        }

        /// <summary>
        /// Orçamentos
        /// </summary>
        /// <returns></returns>
        public ActionResult Orcamentos()
        {
            return View();
        }

        /// <summary>
        /// Relatório
        /// </summary>
        /// <returns></returns>
        public ActionResult Relatorio()
        {
            return View();
        }
        /// <summary>
        /// Segurança
        /// </summary>
        /// <returns></returns>
        public ActionResult Seguranca()
        {
            return View();
        }

        /// <summary>
        /// Tela de Login
        /// </summary>
        /// <returns></returns>
        public ActionResult TelaLogin()
        {
            return View();
        }

    }
}