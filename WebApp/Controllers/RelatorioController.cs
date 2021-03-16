using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WebApp.Models;
using WebApp.Business;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador de Grupos de Perfis e/ou de Usuários
    /// </summary>
    public class RelatorioController : Controller
    {
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Relatorio()
        {
            // preenche o combos
         //   ViewBag.cmbFiltroRodovias = new ObjetoBLL().PreenchecmbFiltroRodovias();
            ViewBag.cmbFiltroRegionais= new ObjetoBLL().PreenchecmbFiltroRegionais();
            ViewBag.cmbFiltroTiposOS = new OrdemServicoBLL().PreencheCmbTiposOS();
            ViewBag.cmbFiltroStatusOS = new OrdemServicoBLL().PreencheCmbStatusOS();

            return View();
        }


        /// <summary>
        /// Busca Lista de Rodovias 
        /// </summary>
        /// <param name="filtro_rod_codigo">Código ou Parte a se localizar</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreencheCmbRodoviasLocalizados(string filtro_rod_codigo = "")
        {
            return Json(new ObjetoBLL().PreenchecmbFiltroRodovias(filtro_rod_codigo), JsonRequestBehavior.AllowGet);
        }


    }
}