using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using WebApp.Controllers;

namespace WebApp.Business
{
    /// <summary>
    /// Módulos e Telas do Sistema
    /// </summary>
    public class ModuloBLL
    {
        /// <summary>
        /// Lista de todos os Módulos do Sistema
        /// </summary>
        /// <param name="mod_id">Filtro por Id de Módulo, null para todos</param>
        /// <returns>Lista de Módulos</returns>
        public List<Modulo> Modulo_ListAll(int? mod_id = null)
        {
            return new ModuloDAO().Modulo_ListAll(mod_id);
        }


        /// <summary>
        /// Busca os dados do Módulo selecionado
        /// </summary>
        /// <param name="ID">Id do Módulo selecionado</param>
        /// <returns>Modulo</returns>
        public Modulo Modulo_GetbyID(int ID)
        {
            return new ModuloDAO().Modulo_ListAll(ID).FirstOrDefault();
        }


        /// <summary>
        /// Ativa/Desativa Módulo selecionado
        /// </summary>
        /// <param name="mod_id">Id do Módulo selecionado</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Modulo_AtivarDesativar(int mod_id, int usu_id, string ip)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            int retorno = new ModuloDAO().Modulo_AtivarDesativar(mod_id, paramUsuario.usu_id, paramUsuario.usu_ip);
            if (retorno > 0)
            {
                // atualiza permissoes
                List<UsuarioPermissoes> lstPermissoes = new UsuarioBLL().Usuario_ListPermissoes(paramUsuario.usu_id);
                paramUsuario.lstUsuarioPermissoes = lstPermissoes;

                // atualiza lista de menus
                Usuario usu = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
                new HomeController().ListaMenus_Lateral(ref usu);

                System.Web.HttpContext.Current.Session["Usuario"] = paramUsuario;
            }

            return retorno;
        }


        /// <summary>
        /// Salva os dados do Módulo
        /// </summary>
        /// <param name="mod">Nome do Módulo</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Modulo_Salvar(Modulo mod)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            int retorno = new ModuloDAO().Modulo_Salvar(mod, paramUsuario.usu_id, paramUsuario.usu_ip);
            if (retorno > 0)
            {
                // atualiza permissoes
                List<UsuarioPermissoes> lstPermissoes = new UsuarioBLL().Usuario_ListPermissoes(paramUsuario.usu_id);
                paramUsuario.lstUsuarioPermissoes = lstPermissoes;

                // atualiza lista de menus
                Usuario usu = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
                new HomeController().ListaMenus_Lateral(ref usu);

                System.Web.HttpContext.Current.Session["Usuario"] = paramUsuario;
            }

            return retorno;
        }



    }
}