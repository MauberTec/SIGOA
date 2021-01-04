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
    /// Controlador de Unidades de Perfis e/ou de Usuários
    /// </summary>
    public class UnidadeController : Controller
    {

        // *************** Tipo de Unidade   *************************************************************

        /// <summary>
        /// Lista de todos Tipos não deletados
        /// </summary>
        /// <param name="ID">Filtro por Id do Tipo de Unidade, null para todos</param>
        /// <param name="unt_nome">Filtro por Nome do Tipo de Unidade</param>
        /// <returns>JsonResult Lista de Tipos de Unidade</returns>
        public JsonResult Unidade_Tipo_ListAll(int? ID = null, string unt_nome = "")
        {
            return Json(new { data = new UnidadeBLL().Unidade_Tipo_ListAll(ID, unt_nome) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Tipo de Unidade selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo de Unidade selecionado</param>
        /// <returns>JsonResult Unidade_Tipo</returns>
        public JsonResult Unidade_Tipo_GetbyID(int ID)
        {
            return Json(new UnidadeBLL().Unidade_Tipo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo de Unidade
        /// </summary>
        /// <param name="unt">Dados do Tipo de Unidade</param>
        /// <returns>JsonResult</returns>
        public JsonResult Unidade_Tipo_Salvar(Unidade_Tipo unt)
        {
            return Json(new UnidadeBLL().Unidade_Tipo_Salvar(unt), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Excluir (logicamente) Tipo de Unidade
        /// </summary>
        /// <param name="unt_id">Id do Tipo de Unidade selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult Unidade_Tipo_Excluir(int unt_id)
        {
            int retorno = new UnidadeBLL().Unidade_Tipo_Excluir(unt_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);

        }



        // *************** Unidade  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Unidade()
        {
            return View();
        }

        /// <summary>
        /// Lista de todas Unidades não deletadas
        /// </summary>
        /// <param name="unt_id">Filtro por Id do Tipo de Unidade, null para todos</param>
        /// <returns>JsonResult Lista de Unidades</returns>
        public JsonResult Unidade_ListAll(int unt_id)
        {
            return Json(new { data = new UnidadeBLL().Unidade_ListAll(null, unt_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Unidade selecionado
        /// </summary>
        /// <param name="ID">Id da Unidade selecionada</param>
        /// <returns>JsonResult Unidade</returns>
        public JsonResult Unidade_GetbyID(int ID)
        {
            return Json(new UnidadeBLL().Unidade_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Unidade no Banco
        /// </summary>
        /// <param name="uni">Nome da Unidade</param>
        /// <returns>JsonResult</returns>
        public JsonResult Unidade_Salvar(Unidade uni)
        {
            return Json(new UnidadeBLL().Unidade_Salvar(uni), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Excluir (logicamente) Unidade
        /// </summary>
        /// <param name="uni_id">Id da Unidade selecionada</param>
        /// <returns>JsonResult</returns>
        public JsonResult Unidade_Excluir(int uni_id)
        {
            int retorno = new UnidadeBLL().Unidade_Excluir(uni_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);

        }

    }
}