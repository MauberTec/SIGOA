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
    public class GrupoController : Controller
    {
        // *************** grupo  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Grupo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Grupos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de Grupos</returns>
        public JsonResult Grupo_ListAll()
        {
            return Json(new { data = new GrupoBLL().Grupo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Grupo selecionado
        /// </summary>
        /// <param name="ID">Id do Grupo selecionado</param>
        /// <returns>JsonResult Grupo</returns>
        public JsonResult Grupo_GetbyID(int ID)
        {
            return Json(new GrupoBLL().Grupo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Grupo
        /// </summary>
        /// <param name="id">Id do Grupo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Grupo_Excluir(int id)
        {
            int retorno = new GrupoBLL().Grupo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Grupo
        /// </summary>
        /// <param name="id">Id do Grupo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Grupo_AtivarDesativar(int id)
        {
            int retorno = new GrupoBLL().Grupo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Executa Insere ou Altera os dados do Grupo no Banco
        /// </summary>
        /// <param name="gru">Nome do Grupo</param>
        /// <returns>JsonResult</returns>
        public JsonResult Grupo_Salvar(Grupo gru)
        {
            return Json(new GrupoBLL().Grupo_Salvar(gru), JsonRequestBehavior.AllowGet);
        }

        // *************** Perfis do Grupo selecionado *************************************************************
        /// <summary>
        /// Lista todos os Perfis do Grupo selecionado
        /// </summary>
        /// <param name="ID">Id do Grupo Selecionado</param>
        /// <returns>JsonResult Lista de GrupoPerfil</returns>
        public JsonResult Grupo_ListAllPerfis(int ID)
        {
            return Json(new { data = new GrupoBLL().Grupo_ListAllPerfis(ID) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     Ativa/Desativa Perfil do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="per_id">Id do Perfil Selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Grupo_AtivarDesativarPerfil(int gru_id, int per_id)
        {
            int retorno = new GrupoBLL().Grupo_AtivarDesativarPerfil(gru_id, per_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        // *************** USUARIOS DO GRUPO *************************************************************
        /// <summary>
        /// Lista todos os Usuários do Grupo selecionado
        /// </summary>
        /// <param name="ID">Id do Grupo Selecionado</param>
        /// <returns>JsonResult Lista de GrupoUsuario</returns>
        public JsonResult Grupo_ListAllUsuarios(int ID)
        {
            return Json(new { data = new GrupoBLL().Grupo_ListAllUsuarios(ID) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///     Ativa/Desativa Usuário do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Grupo_AtivarDesativarUsuario(int gru_id, int usu_id)
        {
            int retorno = new GrupoBLL().Grupo_AtivarDesativarUsuario(gru_id, usu_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }




    }
}