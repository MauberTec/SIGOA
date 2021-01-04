using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;

namespace WebApp.Business
{
    /// <summary>
    /// AnomLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class ReparoBLL
    {

        // *************** TIPO  *************************************************************

        /// <summary>
        ///  Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="rpt_id">Filtro por Id do Tipo, null para todos</param>
        /// <returns>Lista de ReparoTipos</returns>
        public List<ReparoTipo> ReparoTipo_ListAll(int? rpt_id = null)
        {
            return new ReparoDAO().ReparoTipo_ListAll(rpt_id);
        }

        /// <summary>
        /// Dados do Tipo selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo selecionado</param>
        /// <returns>ReparoTipo</returns>
        public ReparoTipo ReparoTipo_GetbyID(int ID)
        {
            return  new ReparoDAO().ReparoTipo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="rpt_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int ReparoTipo_Excluir(int rpt_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoDAO().ReparoTipo_Excluir(rpt_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="rpt_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int ReparoTipo_AtivarDesativar(int rpt_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoDAO().ReparoTipo_AtivarDesativar(rpt_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="rpt">Dados do Tipo</param>
        /// <returns>int</returns>
        public int ReparoTipo_Salvar(ReparoTipo rpt)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoDAO().ReparoTipo_Salvar(rpt, paramUsuario.usu_id, paramUsuario.usu_ip);
        }




    }
}