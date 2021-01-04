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
    /// Controlador de ReparoLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class ReparoController : Controller
    {

        // *************** TIPO  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ReparoTipo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Tipos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de ReparoTipos</returns>
        public JsonResult ReparoTipo_ListAll()
        {
            return Json(new { data = new ReparoBLL().ReparoTipo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Tipo Selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo Selecionado</param>
        /// <returns>JsonResult ReparoTipo</returns>
        public JsonResult ReparoTipo_GetbyID(int ID)
        {
            return Json(new ReparoBLL().ReparoTipo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ReparoTipo_Excluir(int id)
        {
            int retorno = new ReparoBLL().ReparoTipo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ReparoTipo_AtivarDesativar(int id)
        {
            int retorno = new ReparoBLL().ReparoTipo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="atp">Dados do Tipo</param>
        /// <returns>JsonResult</returns>
        public JsonResult ReparoTipo_Salvar(ReparoTipo atp)
        {
            return Json(new ReparoBLL().ReparoTipo_Salvar(atp), JsonRequestBehavior.AllowGet);
        }





    }
}