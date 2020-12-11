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
    /// Controlador dos Módulos e Telas do Sistema
    /// </summary>
    public class ModuloController : Controller
    {
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>ActionResult View</returns>
        public ActionResult Modulo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Módulos do Sistema
        /// </summary>
        /// <returns>JsonResult Lista de Módulos</returns>
        public JsonResult Modulo_ListAll()
        {
            return Json(new { data = new ModuloBLL().Modulo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca os dados do Módulo selecionado
        /// </summary>
        /// <param name="ID">Id do Módulo selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult Modulo_GetbyID(int ID)
        {
            return Json(new ModuloBLL().Modulo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ativa/Desativa Módulo selecionado
        /// </summary>
        /// <param name="id">Id do Módulo selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Modulo_AtivarDesativar(int id)
        {
            Usuario paramUsuario = (Usuario)Session["Usuario"];
            int retorno = new ModuloBLL().Modulo_AtivarDesativar(id, paramUsuario.usu_id, paramUsuario.usu_ip);

            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Salva os dados do Módulo
        /// </summary>
        /// <param name="mod">Nome do Módulo</param>
        /// <returns>int</returns>
        public JsonResult Modulo_Salvar(Modulo mod)
        {
            return Json(new ModuloBLL().Modulo_Salvar(mod), JsonRequestBehavior.AllowGet);
        }


    }
}