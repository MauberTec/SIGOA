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
        /// <param name="FiltroidRodovias">Filtro por id de Rodovias</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objetos</param>
        /// <returns>Lista de OrcamentoStatus</returns>
        public List<Orcamento> Orcamento_ListAll(int? orc_id = null, string filtroRodovia = "", string filtroObjetos = "", int? filtroStatus = -1, int? orc_ativo = 2,
           string FiltroidRodovias = "", string FiltroidObjetos = "")
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_ListAll(orc_id, filtroRodovia, filtroObjetos, filtroStatus, orc_ativo
                , FiltroidRodovias, FiltroidObjetos, paramUsuario.usu_id);
        }


        /// <summary>
        /// Busca o proximo sequencial de Orcamento
        /// </summary>
        /// <returns>string</returns>
        public string Orcamento_ProximoSeq()
        {
            return new OrcamentoDAO().Orcamento_ProximoSeq();
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



        /// <summary>
        ///     Lista dos Serviços Adicionais por Objeto do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do Objeto que contém o serviço</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<ServicosAdicionados> Orcamento_Servicos_Adicionados_ListAll(int orc_id, int obj_id)
        {
            return new OrcamentoDAO().Orcamento_Servicos_Adicionados_ListAll(orc_id, obj_id);
        }


        /// <summary>
        ///  Excluir (logicamente) Serviço
        /// </summary>
        /// <param name="id">Id do Serviço Selecionado</param>
        /// <returns>int</returns>
        public int Orcamento_Servicos_Adicionados_Excluir(int id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Servicos_Adicionados_Excluir(id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="ids_retorno">Lista dos ids alterados</param>
        /// <param name="valores_retorno">Lista dos valores alterados</param>
        /// <returns>int</returns>
        public int Orcamento_ServicosAdicionados_Salvar(string ids_retorno, string valores_retorno)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_ServicosAdicionados_Salvar(ids_retorno, valores_retorno, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista das TPUs a serem adicionadas
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do objeto do orcamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<ServicosAdicionados> OrcamentoServicosAdicionadosTPUs_ListAll(int orc_id, int obj_id, int ose_fase)
        {
            // checa se a tabela DER.TPUs esta sincronizada
            //List<tpu> listaTPU = new IntegracaoDAO().DER_SincronizarTPUs(0,  ano, ose_fase, mes, "", "");
            //if ((listaTPU.Count == 1) && (listaTPU[0].CodSubItem == "-1"))
            //{

            //}
            return new OrcamentoDAO().OrcamentoServicosAdicionadosTPUs_ListAll(orc_id, obj_id, ose_fase);
        }



        /// <summary>
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <param name="obj_id">Id do Objeto do Orçamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <param name="ose_codigo_der">Código do Serviço da TPU</param>
        /// <param name="ose_quantidade">Quantidade a ser utilizada</param>
        /// <returns>int</returns>
        public int Orcamento_Adicionar_Servico(int orc_id, int obj_id, int ose_fase, string ose_codigo_der, decimal ose_quantidade)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Adicionar_Servico(orc_id, obj_id, ose_fase, ose_codigo_der, ose_quantidade, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Calcula o Valor Total do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <returns>decimal</returns>
        public decimal Orcamento_Total(int orc_id)
        {
            return new OrcamentoDAO().Orcamento_Total(orc_id);
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