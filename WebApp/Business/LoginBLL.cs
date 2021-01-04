using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using WebApp.Helpers;
using System.Net.Mail;

namespace WebApp.Business
{
    /// <summary>
    /// Login
    /// </summary>
    public class LoginBLL
    {
        /// <summary>
        /// Validação de usuário/senha. Se válido coloca foto na tela
        /// </summary>
        /// <param name="paramUsuario">Login do Usuário</param>
        /// <returns>bool</returns>
        public int ValidarUsuario(Usuario paramUsuario)
        {
            //Função comentada abaixo irá realizar todas as validações para verificar se o usuário é permitido
            paramUsuario.usu_ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            paramUsuario = new UsuarioBLL().Usuario_ValidarLogin(paramUsuario);
            int valid = paramUsuario.usu_id;

            if (valid >= 0)
            {
                if (paramUsuario.usu_ativo == 1)
                {
                    List<UsuarioPermissoes> lstPermissoes = new UsuarioBLL().Usuario_ListPermissoes(paramUsuario.usu_id);
                    paramUsuario.lstUsuarioPermissoes = lstPermissoes;
                }
                else
                    valid = -20; // usuario desativado
            }
            else
                valid = -1; // usuario nao existente ou senha invalida

            System.Web.HttpContext.Current.Session["Usuario"] = paramUsuario;
            return  valid;
        }


        /// <summary>
        /// Verifica se a senha Atual digitada confere com a do usuário logado
        /// </summary>
        /// <param name="senhaAtual">Senha Atual a ser conferida</param>
        /// <returns>Retorna os atributos do UsuarioModel</returns>
        public bool checaSenhaAtual(string senhaAtual)
        {
            var senhaCript = new Gerais().Encrypt(senhaAtual);
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            return paramUsuario.usu_senha == senhaCript;
        }

        /// <summary>
        /// Logout a partir do menu Lateral. Vai para tela de Login
        /// </summary>
        /// <returns>bool</returns>
        public bool Sair()
        {
            if (System.Web.HttpContext.Current.Session["Usuario"] != null)
            {
                //loga a saida
                Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
                int retorno = new LogSistemaBLL().LogSistema_Inserir(2,
                                            paramUsuario.usu_id.ToString(),
                                            -1,
                                            "",
                                            paramUsuario.usu_ip);
            }

            System.Web.HttpContext.Current.Session["Usuario"] = null;
            return true;
        }

        /// <summary>
        /// Mudança de senha, tela de mudanca de senha, canto superior direito da tela e vindo do codigo Base.js
        /// </summary>
        /// <param name="txtSenhaNova">Nova Senha</param>
        /// <returns>int</returns>
        public int Usuario_AlterarSenha(string txtSenhaNova)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            int retorno = new UsuarioBLL().Usuario_AlterarSenha(paramUsuario.usu_id, txtSenhaNova, paramUsuario.usu_id);
            if (retorno >= 0)
            {
                paramUsuario.usu_trocar_senha = 0;
                paramUsuario.usu_senha = new Gerais().Encrypt(txtSenhaNova);
            }

            retorno += new LogSistemaBLL().LogSistema_Inserir(11,
                                                                paramUsuario.usu_id.ToString(),
                                                                -3,
                                                                "",
                                                                System.Web.HttpContext.Current.Request.UserHostAddress);

            System.Web.HttpContext.Current.Session["Usuario"] = null;
            System.Web.HttpContext.Current.Session["Usuario"] = paramUsuario;
            return retorno;
        }

        /// <summary>
        /// Envio de Email
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <returns>string vazia se OK, ou mensagem de erro</returns>
        public string CriaEnviaEmailComSenha(string login)
        {
            // gera nova senha
            var dt = DateTime.Now.ToString("ddmmyyyyhhmmss");
            var senha = new Gerais().Encrypt(login + "_" + dt);

            Usuario usuTemp = new Usuario();
            usuTemp.usu_usuario = login;
            usuTemp.usu_senha = new Gerais().Encrypt(senha);
            usuTemp.usu_ip = System.Web.HttpContext.Current.Request.UserHostAddress;

            // reseta a senha
            Usuario usu = new UsuarioDAO().Usuario_ResetarSenha(usuTemp); 

            // envia email com a senha nova
            if (usu.usu_id >= 0)
            {
                ParamsEmail pEmail = new ParametroBLL().Parametro_ListAllParamsEmail()[0];

                pEmail.Para = usu.usu_nome + "<" + usu.usu_email + ">";
                pEmail.Assunto = "Envio de Senha";


                // substitui parametros
                pEmail.Texto = pEmail.Texto.Replace("[param_USUARIO_NOME]", usu.usu_nome);
                pEmail.Texto = pEmail.Texto.Replace("[param_USUARIO]", usu.usu_usuario);
                pEmail.Texto = pEmail.Texto.Replace("[param_SENHA]", senha);
                pEmail.Texto = pEmail.Texto.Replace("[param_URL]", pEmail.URL_SISTEMA);


                // envia o email
                AlternateView av1 = null;
                if (pEmail.IsBodyHtml)
                    av1 = AlternateView.CreateAlternateViewFromString(pEmail.Texto, null, "text/html");

                string retorno = new Gerais().MandaEmail(av1, pEmail);
                return retorno;
            }
            else
                return "Usuário não cadastrado no sistema";

        }


    }
}