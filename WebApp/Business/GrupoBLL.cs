using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;

namespace WebApp.Business
{
    /// <summary>
    /// Grupos de Perfis e/ou de Usuários
    /// </summary>
    public class GrupoBLL
    {
        // *************** Grupo  *************************************************************

        /// <summary>
        ///  Lista de todos os Grupos não deletados
        /// </summary>
        /// <param name="gru_id">Filtro por Id do Grupo, null para todos</param>
        /// <returns>Lista de Grupos</returns>
        public List<Grupo> Grupo_ListAll(int? gru_id = null)
        {
            return new GrupoDAO().Grupo_ListAll(gru_id);
        }

        /// <summary>
        /// Dados do Grupo selecionado
        /// </summary>
        /// <param name="ID">Id do Grupo selecionado</param>
        /// <returns>Grupo</returns>
        public Grupo Grupo_GetbyID(int ID)
        {
            return  new GrupoDAO().Grupo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Grupo
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <returns>int</returns>
        public int Grupo_Excluir(int gru_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new GrupoDAO().Grupo_Excluir(gru_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Grupo
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <returns>int</returns>
        public int Grupo_AtivarDesativar(int gru_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new GrupoDAO().Grupo_AtivarDesativar(gru_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Executa Insere ou Altera os dados do Grupo no Banco
        /// </summary>
        /// <param name="gru">Nome do Grupo</param>
        /// <returns>int</returns>
        public int Grupo_Salvar(Grupo gru)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new GrupoDAO().Grupo_Salvar(gru, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // *************** Perfis do Grupo selecionado *************************************************************
        /// <summary>
        /// Lista todos os Perfis do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <returns>Lista de GrupoPerfil</returns>
        public List<GrupoPerfil> Grupo_ListAllPerfis(int gru_id)
        {
            return new GrupoDAO().Grupo_ListAllPerfis(gru_id);
        }

        /// <summary>
        ///     Ativa/Desativa Perfil do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="per_id">Id do Perfil Selecionado</param>
        /// <returns>int</returns>
        public int Grupo_AtivarDesativarPerfil(int gru_id, int per_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new GrupoDAO().Grupo_AtivarDesativarPerfil(gru_id, per_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // *************** USUARIOS DO GRUPO *************************************************************
        /// <summary>
        /// Lista todos os Usuários do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <returns>Lista de GrupoUsuario</returns>
        public List<GrupoUsuario> Grupo_ListAllUsuarios(int gru_id)
        {
            return new GrupoDAO().Grupo_ListAllUsuarios(gru_id);
        }

        /// <summary>
        ///     Ativa/Desativa Usuário do Grupo selecionado
        /// </summary>
        /// <param name="gru_id">Id do Grupo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Selecionado</param>
        /// <returns>int</returns>
        public int Grupo_AtivarDesativarUsuario(int gru_id, int usu_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new GrupoDAO().Grupo_AtivarDesativarUsuario(gru_id, usu_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



    }
}