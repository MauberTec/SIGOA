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
    /// Parâmetros do Sistema
    /// </summary>
    public class ParametroController : Controller
    {
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>ActionResult View</returns>
        public ActionResult Parametro()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Parâmetros
        /// </summary>
        /// <returns>Lista de Parametro</returns>
        public JsonResult Parametro_ListAll()
        {
            return Json(new { data = new ParametroBLL().Parametro_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Parâmetro selecionado
        /// </summary>
        /// <param name="ID">Id do Parâmetro selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult Parametro_GetbyID(string ID)
        {
            return Json(new ParametroBLL().Parametro_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Salva Parâmetro
        /// </summary>
        /// <param name="par">Nome do Parâmetro</param>
        /// <returns>JsonResult</returns>
        public JsonResult Parametro_Salvar(Parametro par)
        {
            return Json(new ParametroBLL().Parametro_Salvar(par), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Valor do Parâmetro 
        /// </summary>
        /// <param name="par_id">Id do Parâmetro selecionado</param>
        /// <returns>Parametro</returns>
        public JsonResult Parametro_GetValor(string par_id)
        {
            return Json(new ParametroBLL().Parametro_GetValor(par_id), JsonRequestBehavior.AllowGet);
        }

    }
}
