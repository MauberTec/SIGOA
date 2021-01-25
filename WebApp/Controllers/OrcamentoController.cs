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
        /// <returns></returns>
        public ActionResult Orcamento()
        {
            //OrcamentoBLL orcamento = new OrcamentoBLL();
            //List<Orcamento> orcamentos = orcamento.GetOrcamentos();
            //return View(orcamentos);

            ViewBag.cmbFiltroStatusOrcamento = new OrcamentoBLL().PreencheCmbStatusOrcamento();

            return View();
        }

        /// <summary>
        /// Lista de todos os Orcamentos não deletados
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="filtroRodovia">Filtro por Rodovia</param>
        /// <param name="filtroObjetos">Filtro por Objeto</param>
        /// <param name="filtroStatus">Filtro por Status</param>
        /// <returns>JsonResult Lista de Orcamento</returns>
        public JsonResult Orcamento_ListAll(int? orc_id = null, string filtroRodovia = "", string filtroObjetos = "", int? filtroStatus = -1)
        {
            return Json(new { data = new OrcamentoBLL().Orcamento_ListAll(orc_id, filtroRodovia, filtroObjetos, filtroStatus) }, JsonRequestBehavior.AllowGet);
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
        /// <param name="ocs">Dados do </param>
        /// <returns>JsonResult</returns>
        public JsonResult Orcamento_Salvar(Orcamento orcam)
        {
            return Json(new OrcamentoBLL().Orcamento_Salvar(orcam), JsonRequestBehavior.AllowGet);
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