using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApp.Business;
using WebApp.DAO;
using WebApp.Helpers;
using WebApp.Models;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador da Tela de Login
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>ActionResult View</returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Validação de usuário/senha. Se válido coloca foto na tela
        /// </summary>
        /// <param name="paramUsuario">Login do Usuário</param>
        /// <returns>Retorna os atributos do UsuarioModel</returns>
        [HttpPost]
        public JsonResult ValidarUsuario(Usuario paramUsuario)
        {
            int retorno = new LoginBLL().ValidarUsuario(paramUsuario);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Verifica se a senha Atual digitada confere com a do usuário logado
        /// </summary>
        /// <param name="senhaAtual">Senha Atual a ser conferida</param>
        /// <returns>Retorna os atributos do UsuarioModel</returns>
        public JsonResult checaSenhaAtual(string senhaAtual)
        {
            var retorno = new LoginBLL().checaSenhaAtual(senhaAtual);
            return Json(new { status = retorno}, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Logout a partir do botão no menu superior direito. Vai para tela de Login
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Logout()
        {
            bool retorno = new LoginBLL().Sair();
            return Json(new { Mensagem = "Favor faça o login novamente!", status = true, JsonRequestBehavior.AllowGet });
        }

        /// <summary>
        /// Logout a partir do menu Lateral. Vai para tela de Login
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Sair()
        {
            bool retorno = new LoginBLL().Sair();
            return RedirectToAction("");
        }

        /// <summary>
        /// Mudança de senha, tela de mudanca de senha, canto superior direito da tela e vindo do codigo Base.js
        /// </summary>
        /// <param name="txtSenhaNova">Nova Senha</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Usuario_AlterarSenha (string txtSenhaNova)
        {
            int retorno = new LoginBLL().Usuario_AlterarSenha(txtSenhaNova);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet );
        }


        /// <summary>
        /// Envia senha por email
        /// </summary>
        /// <param name="login">Login do sistema</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult bntEnviar_click(string login)
        {
            string retorno = new LoginBLL().CriaEnviaEmailComSenha(login);
            bool saida = (retorno.Trim() == "");

            retorno += new LogSistemaBLL().LogSistema_Inserir(10,
                                                            login,
                                                            -2,
                                                            "",
                                                            Request.UserHostAddress);

            return Json(new { status = saida, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

    }
}