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
    /// OrcamentoLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class OrcamentoBLL
    {
        /// <summary>
        ///  Lista de todos os Orcamentos não deletados
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="filtroRodovia">Filtro por Rodovia</param>
        /// <param name="filtroObjetos">Filtro por Objeto</param>
        /// <param name="filtroStatus">Filtro por Status</param>
        /// <param name="orc_ativo">Filtro por Ativo/Inativo</param>
        /// <returns>Lista de OrcamentoStatus</returns>
        public List<Orcamento> Orcamento_ListAll(int? orc_id = null, string filtroRodovia = "", string filtroObjetos = "", int? filtroStatus = -1, int? orc_ativo = 2)
        {
            return new OrcamentoDAO().Orcamento_ListAll(orc_id, filtroRodovia, filtroObjetos, filtroStatus, orc_ativo);
        }

        /// <summary>
        ///  Excluir Orcamento (logicamente) 
        /// </summary>
        /// <param name="sta_id">Id do  Selecionado</param>
        /// <returns>int</returns>
        public int Orcamento_Excluir(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Excluir(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Orcamento
        /// </summary>
        /// <param name="sta_id">Id do  Selecionado</param>
        /// <returns>int</returns>
        public int Orcamento_AtivarDesativar(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_AtivarDesativar(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Orcamento
        /// </summary>
        /// <param name="orc">Dados do Orcamento</param>
        /// <returns>int</returns>
        public int Orcamento_Salvar(Orcamento orc)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Salvar(orc, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///    Clona os dados do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento a ser clonado</param>
        /// <returns>int</returns>
        public int Orcamento_Clonar(int orc_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Clonar(orc_id, paramUsuario.usu_id, paramUsuario.usu_ip);

        }



        /// <summary>
        ///     Lista os Status para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreencheCmbStatusOrcamento()
        {
            List<OrcamentoStatus> lst = new OrcamentoDAO().OrcamentoStatus_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.ocs_descricao + " (" + temp.ocs_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.ocs_id.ToString() });
            }

            return lstSaida;
        }



        // *************** ORCAMENTO_DETALHES  *************************************************************

        /// <summary>
        ///     Lista dos Detalhes do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="ore_ativo">Filtro por Elemento Ativo</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<OrcamentoDetalhes> OrcamentoDetalhes_ListAll(int orc_id, int ore_ativo)
        {
             return new OrcamentoDAO().OrcamentoDetalhes_ListAll(orc_id, ore_ativo);
       }

        /// <summary>
        ///  Ativa/Desativa Orcamento
        /// </summary>
        /// <param name="ore_id">Id do Reparo Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoDetalhes_AtivarDesativar(int ore_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoDetalhes_AtivarDesativar(ore_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** STATUS  *************************************************************

        /// <summary>
        ///  Lista de todos os Status não deletados
        /// </summary>
        /// <param name="sta_id">Filtro por Id do Status, null para todos</param>
        /// <returns>Lista de OrcamentoStatus</returns>
        public List<OrcamentoStatus> OrcamentoStatus_ListAll(int? sta_id = null)
        {
            return new OrcamentoDAO().OrcamentoStatus_ListAll(sta_id);
        }

        /// <summary>
        /// Dados do Status selecionado
        /// </summary>
        /// <param name="ID">Id do Status selecionado</param>
        /// <returns>OrcamentoStatus</returns>
        public OrcamentoStatus OrcamentoStatus_GetbyID(int ID)
        {
            return  new OrcamentoDAO().OrcamentoStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Status
        /// </summary>
        /// <param name="sta_id">Id do Status Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Excluir(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoStatus_Excluir(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Status
        /// </summary>
        /// <param name="sta_id">Id do Status Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_AtivarDesativar(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoStatus_AtivarDesativar(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status
        /// </summary>
        /// <param name="sta">Dados do Status</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Salvar(OrcamentoStatus sta)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoStatus_Salvar(sta, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** FLUXO DE STATUS DE Orcamento  *************************************************************

        /// <summary>
        ///     Lista de todos os Fluxos de  Status não deletados
        /// </summary>
        /// <returns>Lista de OrcamentoFluxoStatus</returns>
        public List<OrcamentoFluxoStatus> OrcamentoFluxoStatus_ListAll()
        {
            return new OrcamentoDAO().OrcamentoFluxoStatus_ListAll();
        }

        /// <summary>
        /// Dados do Fluxo de Status selecionado
        /// </summary>
        /// <param name="ID">Id do Fluxo de Status selecionado</param>
        /// <returns>OrcamentoFluxoStatus</returns>
        public OrcamentoFluxoStatus OrcamentoFluxoStatus_GetbyID(int ID)
        {
            return new OrcamentoDAO().OrcamentoFluxoStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Insere ou Altera os dados do Fluxo de Status 
        /// </summary>
        /// <param name="ocf">Fluxo de Status</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Salvar(OrcamentoFluxoStatus ocf)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoFluxoStatus_Salvar(ocf, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Fluxo de Status
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status  Selecionada</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Excluir(int ocf_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoFluxoStatus_Excluir(ocf_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Fluxo de Status
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_AtivarDesativar(int ocf_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoFluxoStatus_AtivarDesativar(ocf_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista de todos os Status para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> preencheCmbStatus()
        {
            List<OrcamentoStatus> lst = new OrcamentoDAO().OrcamentoStatus_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.ocs_descricao + " (" + temp.ocs_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.ocs_id.ToString() });
            }

            return lstSaida;
        }



    }
}