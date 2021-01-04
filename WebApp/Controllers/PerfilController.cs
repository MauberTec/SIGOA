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
    /// Controlador da tela de Perfis de Usuário
    /// </summary>
    public class PerfilController : Controller
    {
        // *************** PERFIL  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>ActionResult View</returns>
        public ActionResult Perfil()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Perfis 
        /// </summary>
        /// <returns>JsonResult Lista de Perfil</returns>
        public JsonResult Perfil_ListAll()
        {
            return Json(new { data = new PerfilBLL().Perfil_ListAll() }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Dados do Perfil selecionado
        /// </summary>
        /// <param name="ID">Id do Perfil selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult Perfil_GetbyID(int ID)
        {
            return Json(new PerfilBLL().Perfil_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exclui (logicamente) o Perfil selecionado
        /// </summary>
        /// <param name="id">Id do Perfil selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Perfil_Excluir(int id)
        {
            int retorno = new PerfilBLL().Perfil_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Ativa/ Desativa Perfil selecionado
        /// </summary>
        /// <param name="id">Id do Perfil selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Perfil_AtivarDesativar(int id)
        {
            int retorno = new PerfilBLL().Perfil_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Inclui ou Altera Perfil
        /// </summary>
        /// <param name="per">Nome do Perfil</param>
        /// <returns>JsonResult</returns>
        public JsonResult Perfil_Salvar(Perfil per)
        {
            return Json(new PerfilBLL().Perfil_Salvar(per), JsonRequestBehavior.AllowGet);
        }


        // *************** MODULOS do PERFIL *************************************************************
        //Listar Modulo do perfil
        /// <summary>
        /// Lista de todos os Módulos do Perfil selecionado
        /// </summary>
        /// <param name="ID">Id do Perfil selecionado</param>
        /// <returns>JsonResult Lista de PerfilModulo</returns>
        public JsonResult Perfil_ListAllModulos(int ID)
        {
            return Json(new { data = new PerfilBLL().Perfil_ListAllModulos(ID) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ativa/Desativa Módulo do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="mod_id">Id do Módulo selecionado</param>
        /// <param name="mod_pai_id">Id do Módulo Pai do Módulo selecionado</param>
        /// <param name="operacao">Operação: R,W,X,I (Leitura,Escrita,Exclusão,Inserção)</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Perfil_AtivarDesativarModulo(int per_id, int mod_id, int mod_pai_id, int operacao)
        {
            int retorno = new PerfilBLL().Perfil_AtivarDesativarModulo(per_id, mod_id, mod_pai_id, operacao);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }



        // *************** Grupos do PERFIL *************************************************************

        /// <summary>
        /// Lista de todos Grupos do Perfil selecionado
        /// </summary>
        /// <param name="ID">Id do Perfil selecionado</param>
        /// <returns>JsonResult Lista de PerfilGrupo</returns>
        public JsonResult Perfil_ListAllGrupos(int ID)
        {
            return Json(new { data = new PerfilBLL().Perfil_ListAllGrupos(ID) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ativa/Desativa Grupo do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="gru_id">Id do Grupo selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Perfil_AtivarDesativarGrupo(int per_id, int gru_id)
        {
            int retorno = new PerfilBLL().Perfil_AtivarDesativarGrupo(per_id, gru_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        // ***************  USUARIOS do PERFIL *************************************************************
        /// <summary>
        /// Lista de todos os Usuários do Perfil selecionado
        /// </summary>
        /// <param name="ID">Id do Perfil selecionado</param>
        /// <returns>JsonResult Lista de PerfilUsuario</returns>
        public JsonResult Perfil_ListAllUsuarios(int ID)
        {
            return Json(new { data = new PerfilBLL().Perfil_ListAllUsuarios(ID) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Ativa/Desativa Usuario do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>JsonResult</returns>        
        [HttpPost]
        public JsonResult Perfil_AtivarDesativarUsuario(int per_id, int usu_id)
        {
            int retorno = new PerfilBLL().Perfil_AtivarDesativarUsuario(per_id, usu_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


    }
}