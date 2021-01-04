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
using System.IO;
using System.Drawing;
using WebApp.Helpers;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador da Tela de Dados de Usuário
    /// </summary>
    public class UsuarioController : Controller
    {
        // *************** Usuario  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>ActionResult View</returns>
        public ActionResult Usuario()
        {            
            return View();
        }

        /// <summary>
        /// Redimensiona e salva a imagem de Upload
        /// </summary>
        /// <param name="model">Variável do tipo UsuarioFoto</param>
        /// <returns>JsonResult</returns>
        public JsonResult Usuario_ImageUpload(UsuarioFoto model)
        {
            return Json(new UsuarioBLL().Usuario_ImageUpload(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Lista de todos os Usuários não deletados
        /// </summary>
        /// <returns> Lista de Usuario</returns>
        public JsonResult Usuario_ListAll()
        {
            return Json(new { data = new UsuarioBLL().Usuario_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Usuário selecionado, busca pelo ID
        /// </summary>
        /// <param name="ID">Id do Usuário selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult Usuario_GetbyID(int ID)
        {
            return Json(new UsuarioBLL().Usuario_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exclui (logicamente) Usuário
        /// </summary>
        /// <param name="id">Id do Usuário selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Usuario_Excluir(int id)
        {
            int retorno = new UsuarioBLL().Usuario_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ativa/Desativa Usuário selecionado
        /// </summary>
        /// <param name="id">Id do Usuário selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Usuario_AtivarDesativar(int id)
        {
            int retorno = new UsuarioBLL().Usuario_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Inclui ou Alterar Usuário
        /// </summary>
        /// <param name="usu">Variável do tipo USUARIO, com os novos atributos</param>
        /// <returns>JsonResult</returns>
        public JsonResult Usuario_Salvar(Usuario usu)
        {
            string retorno = new UsuarioBLL().Usuario_Salvar(usu);
            bool valid = retorno == "";

            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        // ***************   PERFIS do USUARIO selecionado *************************************************************
        /// <summary>
        /// Lista de Perfis do Usuário selecionado
        /// </summary>
        /// <param name="ID">Id do Usuário selecionado</param>
        /// <returns>Lista de UsuarioPerfil</returns>
        public JsonResult Usuario_ListAllPerfis(int ID)
        {
            return Json(new { data = new UsuarioBLL().Usuario_ListAllPerfis(ID) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ativa/Desativa Perfil do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Usuario_AtivarDesativarPerfil(int usu_id, int per_id)
        {
            int retorno = new UsuarioBLL().Usuario_AtivarDesativarPerfil(usu_id, per_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        // *************** Grupos do USUARIO selecionado *************************************************************
        /// <summary>
        /// Lista de Grupos do Usuário selecionado
        /// </summary>
        /// <param name="ID">Id do Usuário selecionado</param>
        /// <returns>JsonResult Lista de UsuarioGrupo</returns>
        public JsonResult Usuario_ListAllGrupos(int ID)
        {
            return Json(new { data = new UsuarioBLL().Usuario_ListAllGrupos(ID) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Ativa/Desativa Grupo do Usuário selecionado
        /// </summary>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <param name="gru_id">Id do Grupo selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Usuario_AtivarDesativarGrupo(int usu_id, int gru_id)
        {
            int retorno = new UsuarioBLL().Usuario_AtivarDesativarGrupo(usu_id, gru_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


    }






}