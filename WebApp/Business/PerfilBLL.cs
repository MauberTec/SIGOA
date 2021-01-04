using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;

namespace WebApp.Business
{
    /// <summary>
    /// Perfis de Usuário
    /// </summary>
    public class PerfilBLL
    {

        // *************** PERFIL  *************************************************************
        /// <summary>
        /// Lista de todos os Perfis 
        /// </summary>
        /// <param name="per_id">Filtro por Id de Perfil, null para todos</param> 
        /// <returns>Lista de Perfil</returns>
        public List<Perfil> Perfil_ListAll(int? per_id=null)
        {
            return new PerfilDAO().Perfil_ListAll(per_id);
        }

        /// <summary>
        /// Dados do Perfil selecionado
        /// </summary>
        /// <param name="ID">Id do Perfil selecionado</param>
        /// <returns>Perfil</returns>
        public Perfil Perfil_GetbyID(int ID)
        {
            return new PerfilDAO().Perfil_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        /// Exclui (logicamente) o Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>int</returns>
        public int Perfil_Excluir(int per_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            int retorno = new PerfilDAO().Perfil_Excluir(per_id, paramUsuario.usu_id, paramUsuario.usu_ip);
            return retorno;
        }

        /// <summary>
        /// Ativa/ Desativa Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativar(int per_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return  new PerfilDAO().Perfil_AtivarDesativar(per_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Inclui ou Altera Perfil
        /// </summary>
        /// <param name="per">Nome do Perfil</param>
        /// <returns>int</returns>
        public int Perfil_Salvar(Perfil per)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new PerfilDAO().Perfil_Salvar(per, paramUsuario.usu_id, paramUsuario.usu_ip);
        }





        // *************** MODULOS do PERFIL *************************************************************
        //Listar Modulo do perfil
        /// <summary>
        /// Lista de todos os Módulos do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>Lista de PerfilModulo</returns>
        public List<PerfilModulo> Perfil_ListAllModulos(int per_id)
        {
            return new PerfilDAO().Perfil_ListAllModulos(per_id);
        }

        /// <summary>
        /// Ativa/Desativa Módulo do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="mod_id">Id do Módulo selecionado</param>
        /// <param name="mod_pai_id">Id do Módulo Pai do Módulo selecionado</param>
        /// <param name="operacao">Operação: R,W,X,I (Leitura,Escrita,Exclusão,Inserção)</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativarModulo(int per_id, int mod_id, int mod_pai_id, int operacao)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            string sOperacao = "";
            switch (operacao)
            {
                case 4: sOperacao = "R"; break;
                case 5: sOperacao = "W"; break;
                case 6: sOperacao = "X"; break;
                case 7: sOperacao = "I"; break;
            }
            int retorno = new PerfilDAO().Perfil_AtivarDesativarModulo(per_id, mod_id, mod_pai_id, sOperacao, paramUsuario.usu_id, paramUsuario.usu_ip);

            List<UsuarioPermissoes> lstPermissoes = new UsuarioBLL().Usuario_ListPermissoes(paramUsuario.usu_id);
            paramUsuario.lstUsuarioPermissoes = lstPermissoes;
            System.Web.HttpContext.Current.Session["Usuario"] = paramUsuario;

            return retorno;
        }



        // *************** Grupos do PERFIL *************************************************************

        //Listar Grupos por Perfil
        /// <summary>
        /// Lista de todos Grupos do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>Lista de PerfilGrupo</returns>
        public List<PerfilGrupo> Perfil_ListAllGrupos(int per_id)
        {
            return new PerfilDAO().Perfil_ListAllGrupos(per_id);
        }

        /// <summary>
        /// Ativa/Desativa Grupo do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="gru_id">Id do Grupo selecionado</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativarGrupo(int per_id, int gru_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new PerfilDAO().Perfil_AtivarDesativarGrupo(per_id, gru_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // ***************  USUARIOS do PERFIL *************************************************************
        //Listar Usuarios do perfil
        /// <summary>
        /// Lista de todos os Usuários do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <returns>Lista de PerfilUsuario</returns>
        public List<PerfilUsuario> Perfil_ListAllUsuarios(int per_id)
        {
            return new PerfilDAO().Perfil_ListAllUsuarios(per_id);
        }

        /// <summary>
        /// Ativa/Desativa Usuario do Perfil selecionado
        /// </summary>
        /// <param name="per_id">Id do Perfil selecionado</param>
        /// <param name="usu_id">Id do Usuário selecionado</param>
        /// <returns>int</returns>
        public int Perfil_AtivarDesativarUsuario(int per_id, int usu_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new PerfilDAO().Perfil_AtivarDesativarUsuario(per_id, usu_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



    }
}