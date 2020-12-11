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
    /// Controlador de AnomLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class AnomaliaController : Controller
    {

        // *************** LEGENDA  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult AnomLegenda()
        {
            return View();
        }

        /// <summary>
        /// Lista de todas as Legendas não deletadas
        /// </summary>
        /// <returns>JsonResult Lista de AnomLegendas</returns>
        public JsonResult AnomLegenda_ListAll()
        {
            return Json(new { data = new AnomaliaBLL().AnomLegenda_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados da Legenda selecionada
        /// </summary>
        /// <param name="ID">Id da Legenda selecionada</param>
        /// <returns>JsonResult AnomLegenda</returns>
        public JsonResult AnomLegenda_GetbyID(int ID)
        {
            return Json(new AnomaliaBLL().AnomLegenda_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Legenda
        /// </summary>
        /// <param name="id">Id da Legenda Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomLegenda_Excluir(int id)
        {
            int retorno = new AnomaliaBLL().AnomLegenda_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Legenda
        /// </summary>
        /// <param name="id">Id da Legenda Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomLegenda_AtivarDesativar(int id)
        {
            int retorno = new AnomaliaBLL().AnomLegenda_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados da Legenda
        /// </summary>
        /// <param name="leg">Dados da Legenda</param>
        /// <returns>JsonResult</returns>
        public JsonResult AnomLegenda_Salvar(AnomLegenda leg)
        {
            return Json(new AnomaliaBLL().AnomLegenda_Salvar(leg), JsonRequestBehavior.AllowGet);
        }



        // *************** TIPO  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult AnomTipo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Tipos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de AnomTipos</returns>
        public JsonResult AnomTipo_ListAll()
        {
            return Json(new { data = new AnomaliaBLL().AnomTipo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Tipo Selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo Selecionado</param>
        /// <returns>JsonResult AnomTipo</returns>
        public JsonResult AnomTipo_GetbyID(int ID)
        {
            return Json(new AnomaliaBLL().AnomTipo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomTipo_Excluir(int id)
        {
            int retorno = new AnomaliaBLL().AnomTipo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomTipo_AtivarDesativar(int id)
        {
            int retorno = new AnomaliaBLL().AnomTipo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="atp">Dados do Tipo</param>
        /// <returns>JsonResult</returns>
        public JsonResult AnomTipo_Salvar(AnomTipo atp)
        {
            return Json(new AnomaliaBLL().AnomTipo_Salvar(atp), JsonRequestBehavior.AllowGet);
        }



        // *************** CAUSA  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult AnomCausa()
        {
            // preenche o combos
            List<SelectListItem> lstListacmbAnomLegenda = new AnomaliaBLL().PreenchecmbAnomLegenda();
            ViewBag.cmbAnomLegenda = lstListacmbAnomLegenda;

            return View();
        }

        /// <summary>
        /// Lista de todas as Causas não deletadas
        /// </summary>
        /// <returns>JsonResult Lista de AnomCausas</returns>
        public JsonResult AnomCausa_ListAll(string aca_descricao = "", int? leg_id = null)
        {
            return Json(new { data = new AnomaliaBLL().AnomCausa_ListAll(aca_descricao, leg_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados da Causa selecionada
        /// </summary>
        /// <param name="ID">Id da Causa selecionada</param>
        /// <returns>JsonResult AnomCausa</returns>
        public JsonResult AnomCausa_GetbyID(int ID)
        {
            return Json(new AnomaliaBLL().AnomCausa_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Causa
        /// </summary>
        /// <param name="id">Id da Causa Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomCausa_Excluir(int id)
        {
            int retorno = new AnomaliaBLL().AnomCausa_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Causa
        /// </summary>
        /// <param name="id">Id da Causa Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomCausa_AtivarDesativar(int id)
        {
            int retorno = new AnomaliaBLL().AnomCausa_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados da Causa
        /// </summary>
        /// <param name="aca">Dados da Causa</param>
        /// <returns>JsonResult</returns>
        public JsonResult AnomCausa_Salvar(AnomCausa aca)
        {
            return Json(new AnomaliaBLL().AnomCausa_Salvar(aca), JsonRequestBehavior.AllowGet);
        }




        // *************** ALERTA  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult AnomAlerta()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Alertas não deletados
        /// </summary>
        /// <returns>JsonResult Lista de AnomAlertas</returns>
        public JsonResult AnomAlerta_ListAll()
        {
            return Json(new { data = new AnomaliaBLL().AnomAlerta_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Alerta Selecionado
        /// </summary>
        /// <param name="ID">Id do Alerta Selecionado</param>
        /// <returns>JsonResult AnomAlerta</returns>
        public JsonResult AnomAlerta_GetbyID(int ID)
        {
            return Json(new AnomaliaBLL().AnomAlerta_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Alerta
        /// </summary>
        /// <param name="id">Id do Alerta Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomAlerta_Excluir(int id)
        {
            int retorno = new AnomaliaBLL().AnomAlerta_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Alerta
        /// </summary>
        /// <param name="id">Id do Alerta Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomAlerta_AtivarDesativar(int id)
        {
            int retorno = new AnomaliaBLL().AnomAlerta_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Alerta
        /// </summary>
        /// <param name="ale">Dados do Alerta</param>
        /// <returns>JsonResult</returns>
        public JsonResult AnomAlerta_Salvar(AnomAlerta ale)
        {
            return Json(new AnomaliaBLL().AnomAlerta_Salvar(ale), JsonRequestBehavior.AllowGet);
        }



        // *************** STATUS  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult AnomStatus()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Status não deletados
        /// </summary>
        /// <returns>JsonResult Lista de AnomStatus</returns>
        public JsonResult AnomStatus_ListAll()
        {
            return Json(new { data = new AnomaliaBLL().AnomStatus_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Status Selecionado
        /// </summary>
        /// <param name="ID">Id do Status Selecionado</param>
        /// <returns>JsonResult AnomStatus</returns>
        public JsonResult AnomStatus_GetbyID(int ID)
        {
            return Json(new AnomaliaBLL().AnomStatus_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Status
        /// </summary>
        /// <param name="id">Id do Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomStatus_Excluir(int id)
        {
            int retorno = new AnomaliaBLL().AnomStatus_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Status
        /// </summary>
        /// <param name="id">Id do Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomStatus_AtivarDesativar(int id)
        {
            int retorno = new AnomaliaBLL().AnomStatus_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status
        /// </summary>
        /// <param name="ast">Dados do Status</param>
        /// <returns>JsonResult</returns>
        public JsonResult AnomStatus_Salvar(AnomStatus ast)
        {
            return Json(new AnomaliaBLL().AnomStatus_Salvar(ast), JsonRequestBehavior.AllowGet);
        }



        // *************** FLUXO DE STATUS   *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult AnomFluxoStatus()
        {
            // preenche combos
            ViewBag.cmbStatusDe = new AnomaliaBLL().preencheCmbStatus();
            ViewBag.cmbStatusPara = new AnomaliaBLL().preencheCmbStatus();

            return View();
        }

        /// <summary>
        /// Lista de todas os Fluxos de Status não deletados
        /// </summary>
        /// <returns>JsonResult Lista de AnomFluxoStatus</returns>
        public JsonResult AnomFluxoStatus_ListAll()
        {
            return Json(new { data = new AnomaliaBLL().AnomFluxoStatus_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados  do Fluxo de Status selecionado
        /// </summary>
        /// <param name="ID">Id do Fluxo de Status selecionado</param>
        /// <returns>JsonResult AnomFluxoStatus</returns>
        public JsonResult AnomFluxoStatus_GetbyID(int ID)
        {
            return Json(new AnomaliaBLL().AnomFluxoStatus_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) FluxoStatus
        /// </summary>
        /// <param name="id">Id do Fluxo de Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomFluxoStatus_Excluir(int id)
        {
            int retorno = new AnomaliaBLL().AnomFluxoStatus_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Fluxo de Status
        /// </summary>
        /// <param name="id">Id do Fluxo de Status Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AnomFluxoStatus_AtivarDesativar(int id)
        {
            int retorno = new AnomaliaBLL().AnomFluxoStatus_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status
        /// </summary>
        /// <param name="fst">Dados do Fluxo de Status</param>
        /// <returns>JsonResult</returns>
        public JsonResult AnomFluxoStatus_Salvar(AnomFluxoStatus fst)
        {
            return Json(new AnomaliaBLL().AnomFluxoStatus_Salvar(fst), JsonRequestBehavior.AllowGet);
        }



    }
}