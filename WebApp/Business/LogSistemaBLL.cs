using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Web.Mvc;

namespace WebApp.Business
{
    /// <summary>
    ///    Registros de Acesso e Alterações no Sistema
    /// </summary>
    public class LogSistemaBLL
    {

        /// <summary>
        ///  Lista dos Registros de Log
        /// </summary>
        /// <param name="usu_id">Filtrar por Id do Usuário</param>
        /// <param name="data_inicio">Filtrar por Data (a partir de)</param>
        /// <param name="data_fim">Filtrar por Data (até)</param>
        /// <param name="tra_id">Filtrar por Id da Transação (Login, Seleção, Inserção, etc)</param>
        /// <param name="mod_id">Filtrar por Id do Módulo</param>
        /// <param name="texto_procurado">Filtrar por texto digitado</param>
        /// <returns>Lista de LogSistema</returns>
        public List<LogSistema> LogSistema_ListAll(int usu_id,
                                    string data_inicio,
                                    string data_fim,
                                    int tra_id,
                                    int mod_id,
                                    string texto_procurado)
        {
            return new LogSistemaDAO().LogSistema_ListAll(usu_id,
                                                 data_inicio,
                                                 data_fim,
                                                 tra_id,
                                                 mod_id,
                                                 texto_procurado);
        }



        /// <summary>
        /// Para preenchimento do combo Usuários
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> lstGetUsuarios()
        {
            List<SelectListItem> lstListItemUsuarios = new List<SelectListItem>();
            List<Usuario> lstUsuarios = new UsuarioBLL().Usuario_ListAll();

            foreach (var temp in lstUsuarios)
            {
                lstListItemUsuarios.Add(new SelectListItem() { Text = temp.usu_nome, Value = temp.usu_id.ToString() });
            }

            return lstListItemUsuarios;
        }

        /// <summary>
        /// Para preenchimento do combo Transacao
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> lstGetTransacao()
        {
            List<SelectListItem> lstListItemTransacao = new List<SelectListItem>();
            List<LogTransacao> lstTransacao = new LogSistemaBLL().LogSistema_ListTransacao();
            foreach (var temp in lstTransacao)
            {
                lstListItemTransacao.Add(new SelectListItem() { Text = temp.tra_nome, Value = temp.tra_id.ToString() });
            }

            return lstListItemTransacao;
        }


        /// <summary>
        /// Para preenchimento do combo Modulo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> lstGetModulo()
        {
            List<SelectListItem> lstListItemModulo = new List<SelectListItem>();
            List<Modulo> lstModulo = new ModuloBLL().Modulo_ListAll();

            foreach (var temp in lstModulo)
            {
                string txt = temp.mod_nome_modulo;
                if (temp.mod_pai_id >= 0)
                    txt = "-" + txt;

                lstListItemModulo.Add(new SelectListItem() { Text = txt, Value = temp.mod_id.ToString() });
            }

            return lstListItemModulo;
        }



        /// <summary>
        /// Registra ação do Usuário logado
        /// </summary>
        /// <param name="tra_id">Id da Transação (Login, Seleção, Inserção, etc)</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="mod_id">Id do Módulo acessado</param>
        /// <param name="log_texto">Texto a ser salvo no Log</param>
        /// <param name="log_ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int LogSistema_Inserir(int tra_id,
                                                string usu_id,
                                                int mod_id,
                                                string log_texto,
                                                string log_ip)
        {
            return new LogSistemaDAO().LogSistema_Inserir(tra_id,
                                        usu_id,
                                        mod_id,
                                        log_texto,
                                        log_ip);
        }

        /// <summary>
        /// Insere novo log
        /// </summary>
        /// <param name="mod_id">id do Modulo</param>
        /// <returns>int</returns>
        public int LogSistema_Salvar(int mod_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            int retorno =  LogSistema_Inserir(15,
                                        paramUsuario != null ? paramUsuario.usu_id.ToString() : "Não Logado",
                                        mod_id,
                                        "",
                                        System.Web.HttpContext.Current.Request.UserHostAddress);

           // System.Web.HttpContext.Current.Session["Usuario"] = null;

            return retorno;
        }


        /// <summary>
        /// Lista de Transações para preenchimento do Combo de Transações
        /// </summary>
        /// <returns>Lista de LogTransacao</returns>
        public List<LogTransacao> LogSistema_ListTransacao()
        {
            return new LogSistemaDAO().LogSistema_ListTransacao();
        }
    }
}