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
using WebApp.DAO;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador de Orcamento
    /// </summary>
    public class OrcamentoController : Controller
    {
        /// <summary>
        /// Orcamento
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Orcamento()
        {
            // preenche o combos
            List<SelectListItem> lstListacmbFiltroRegionais = new ObjetoBLL().PreenchecmbFiltroRegionais();
            ViewBag.cmbFiltroRegionais = lstListacmbFiltroRegionais;
            ViewBag.cmbFiltroStatusOrcamento = new OrcamentoBLL().PreencheCmbStatusOrcamento();

            return View();
        }
        /// <summary>
        /// Busca os Proximos Status de Orcamento
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult PreencheCmbStatusOrcamento()
        {
            return Json(new { data = new OrcamentoBLL().PreencheCmbStatusOrcamento() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista de todos os Orcamentos não deletados
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="filtroRodovia">Filtro por Rodovia</param>
        /// <param name="filtroObjetos">Filtro por Objeto</param>
        /// <param name="filtroStatus">Filtro por Status</param>
        /// <param name="orc_ativo">Filtro por Ativo/Inativo</param>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovias</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objetos</param>
        /// <returns>JsonResult Lista de Orcamento</returns>
        public JsonResult Orcamento_ListAll(int? orc_id = null, string filtroRodovia = "", string filtroObjetos = "", int? filtroStatus = -1, int? orc_ativo = 2,
            string FiltroidRodovias = "", string FiltroidObjetos = "")
        {
            return Json(new { data = new OrcamentoBLL().Orcamento_ListAll(orc_id, filtroRodovia, filtroObjetos, filtroStatus, orc_ativo
                ,FiltroidRodovias, FiltroidObjetos) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Busca o proximo sequencial de Orcamento
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult Orcamento_ProximoSeq()
        {
            return Json(new { data = new OrcamentoBLL().Orcamento_ProximoSeq() }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Calcula o Valor Total do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <returns>JsonResult</returns>
        public JsonResult Orcamento_Total(int orc_id)
        {
            return Json(new { data = new OrcamentoBLL().Orcamento_Total(orc_id) }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Dados do Orcamento selecionado
        /// </summary>
        /// <param name="ID">Id do Orcamento selecionado</param>
        /// <returns>JsonResult Orcamento</returns>
        public JsonResult Orcamento_GetbyID(int ID)
        {
            return Json(new OrcamentoBLL().Orcamento_ListAll(ID, "", "", -1, 2, "", "").FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///  Excluir (logicamente) 
        /// </summary>
        /// <param name="id">Id do Orcamento Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Orcamento_Excluir(int id)
        {
            int retorno = new OrcamentoBLL().Orcamento_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa 
        /// </summary>
        /// <param name="id">Id do  Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Orcamento_AtivarDesativar(int id)
        {
            int retorno = new OrcamentoBLL().Orcamento_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do 
        /// </summary>
        /// <param name="orc">Dados do Orçamento</param>
        /// <returns>JsonResult</returns>
        public JsonResult Orcamento_Salvar(Orcamento orc)
        {
            return Json(new OrcamentoBLL().Orcamento_Salvar(orc), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///    Clona os dados do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento a ser clonado</param>
        /// <returns>JsonResult</returns>
        public JsonResult Orcamento_Clonar(int orc_id)
        {
            return Json(new OrcamentoBLL().Orcamento_Clonar(orc_id), JsonRequestBehavior.AllowGet);
        }



        // *************** ORCAMENTO_DETALHES  *************************************************************

        /// <summary>
        ///     Lista dos Detalhes do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="ore_ativo">Filtro por Elemento Ativo</param>
        /// <returns>JsonResult Lista de OrcamentoDetalhes</returns>
        public JsonResult OrcamentoDetalhes_ListAll(int orc_id, int ore_ativo)
        {
            return Json(new { data = new OrcamentoBLL().OrcamentoDetalhes_ListAll(orc_id, ore_ativo) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///  Ativa/Desativa 
        /// </summary>
        /// <param name="ore_id">Id do Reparo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrcamentoDetalhes_AtivarDesativar(int ore_id)
        {
            int retorno = new OrcamentoBLL().OrcamentoDetalhes_AtivarDesativar(ore_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        ///     Lista dos Serviços Adicionais por Objeto do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do Objeto que contém o serviço</param>
        /// <returns>JsonResult</returns>
        public JsonResult Orcamento_Servicos_Adicionados_ListAll(int orc_id, int obj_id)
        {
            return Json(new { data = new OrcamentoBLL().Orcamento_Servicos_Adicionados_ListAll(orc_id, obj_id) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     Lista das TPUs a serem adicionadas em Servicos
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do objeto do orcamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <returns>JsonResult</returns>
        public JsonResult OrcamentoServicosAdicionadosTPUs_ListAll(int orc_id, int obj_id, int ose_fase)
        {
            return Json(new { data = new OrcamentoBLL().OrcamentoServicosAdicionadosTPUs_ListAll(orc_id, obj_id, ose_fase) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Serviço
        /// </summary>
        /// <param name="id">Id do Serviço Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Orcamento_Servicos_Adicionados_Excluir(int id)
        {
            int retorno = new OrcamentoBLL().Orcamento_Servicos_Adicionados_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="ids_retorno">Lista dos ids alterados</param>
        /// <param name="valores_retorno">Lista dos valores alterados</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Orcamento_ServicosAdicionados_Salvar(string ids_retorno, string valores_retorno)
        {
            int retorno = new OrcamentoBLL().Orcamento_ServicosAdicionados_Salvar(ids_retorno, valores_retorno);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <param name="obj_id">Id do Objeto do Orçamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <param name="ose_codigo_der">Código do Serviço da TPU</param>
        /// <param name="ose_quantidade">Quantidade a ser utilizada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Orcamento_Adicionar_Servico(int orc_id, int obj_id, int ose_fase, string ose_codigo_der, decimal ose_quantidade)
        {
            int retorno = new OrcamentoBLL().Orcamento_Adicionar_Servico(orc_id,  obj_id,  ose_fase,  ose_codigo_der,  ose_quantidade);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }



        // *************** STATUS  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult OrcamentoStatus()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Status não deletados
        /// </summary>
        /// <returns>JsonResult Lista de OrcamentoStatus</returns>
        public JsonResult OrcamentoStatus_ListAll()
        {
            return Json(new { data = new OrcamentoBLL().OrcamentoStatus_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Status Selecionado
        /// </summary>
        /// <param name="ID">Id do Status Selecionado</param>
        /// <returns>JsonResult OrcamentoStatus</returns>
        public JsonResult OrcamentoStatus_GetbyID(int ID)
        {
            return Json(new OrcamentoBLL().OrcamentoStatus_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Status
        /// </summary>
        /// <param name="id">Id do Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrcamentoStatus_Excluir(int id)
        {
            int retorno = new OrcamentoBLL().OrcamentoStatus_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Status
        /// </summary>
        /// <param name="id">Id do Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrcamentoStatus_AtivarDesativar(int id)
        {
            int retorno = new OrcamentoBLL().OrcamentoStatus_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status
        /// </summary>
        /// <param name="ocs">Dados do Status</param>
        /// <returns>JsonResult</returns>
        public JsonResult OrcamentoStatus_Salvar(OrcamentoStatus ocs)
        {
            return Json(new OrcamentoBLL().OrcamentoStatus_Salvar(ocs), JsonRequestBehavior.AllowGet);
        }



        // *************** FLUXO DE STATUS   *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult OrcamentoFluxoStatus()
        {
            // preenche combos
            ViewBag.cmbStatusDe = new OrcamentoBLL().preencheCmbStatus();
            ViewBag.cmbStatusPara = new OrcamentoBLL().preencheCmbStatus();

            return View();
        }

        /// <summary>
        /// Lista de todas os Fluxos de Status não deletados
        /// </summary>
        /// <returns>JsonResult Lista de OrcamentoFluxoStatus</returns>
        public JsonResult OrcamentoFluxoStatus_ListAll()
        {
            return Json(new { data = new OrcamentoBLL().OrcamentoFluxoStatus_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados  do Fluxo de Status selecionado
        /// </summary>
        /// <param name="ID">Id do Fluxo de Status selecionado</param>
        /// <returns>JsonResult OrcamentoFluxoStatus</returns>
        public JsonResult OrcamentoFluxoStatus_GetbyID(int ID)
        {
            return Json(new OrcamentoBLL().OrcamentoFluxoStatus_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) FluxoStatus
        /// </summary>
        /// <param name="id">Id do Fluxo de Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrcamentoFluxoStatus_Excluir(int id)
        {
            int retorno = new OrcamentoBLL().OrcamentoFluxoStatus_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Fluxo de Status
        /// </summary>
        /// <param name="id">Id do Fluxo de Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrcamentoFluxoStatus_AtivarDesativar(int id)
        {
            int retorno = new OrcamentoBLL().OrcamentoFluxoStatus_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status
        /// </summary>
        /// <param name="ocf">Dados do Fluxo de Status</param>
        /// <returns>JsonResult</returns>
        public JsonResult OrcamentoFluxoStatus_Salvar(OrcamentoFluxoStatus ocf)
        {
            return Json(new OrcamentoBLL().OrcamentoFluxoStatus_Salvar(ocf), JsonRequestBehavior.AllowGet);
        }



    }
}