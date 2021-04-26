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
    /// Controlador de ReparoLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class ReparoController : Controller
    {

        // *************** TIPO  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ReparoTipo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Tipos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de ReparoTipos</returns>
        public JsonResult ReparoTipo_ListAll()
        {
            return Json(new { data = new ReparoBLL().ReparoTipo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Tipo Selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo Selecionado</param>
        /// <returns>JsonResult ReparoTipo</returns>
        public JsonResult ReparoTipo_GetbyID(int ID)
        {
            return Json(new ReparoBLL().ReparoTipo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ReparoTipo_Excluir(int id)
        {
            int retorno = new ReparoBLL().ReparoTipo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ReparoTipo_AtivarDesativar(int id)
        {
            int retorno = new ReparoBLL().ReparoTipo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="atp">Dados do Tipo</param>
        /// <returns>JsonResult</returns>
        public JsonResult ReparoTipo_Salvar(ReparoTipo atp)
        {
            return Json(new ReparoBLL().ReparoTipo_Salvar(atp), JsonRequestBehavior.AllowGet);
        }


        // *************** REPARO TPU  *************************************************************

        /// <summary>
        /// ReparoTpu
        /// </summary>
        /// <returns></returns>
        public ActionResult ReparoTpu()
        {
            // preenche o combos
            ViewBag.reparo_ad = new ReparoBLL().PreenchecmbFiltroTiposReparo();
            ViewBag.fonte_ad = new ReparoBLL().PreenchecmbFontesTPU();

            return View();
        }

        /// <summary>
        /// Lista de todos os ReparoTpu não deletados
        /// </summary>
        /// <returns>JsonResult Lista de TpuDtoModel</returns>
        public JsonResult ReparoTpu_ListAll()
        {
            return Json(new { data = new ReparoBLL().ReparoTpu_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa ReparoTpu
        /// </summary>
        /// <param name="id">Id do Reparo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ReparoTpu_AtivarDesativar(int id)
        {
            int retorno = new ReparoBLL().ReparoTpu_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Editar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ReparoTpu_Salvar(TpuDtoModel model)
        {
            return Json(new ReparoBLL().ReparoTpu_Salvar(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista das TPUs
        /// </summary>
        /// <param name="ano">Ano</param>
        /// <param name="codItem"></param>
        /// <returns>JsonResult Lista tpu</returns>
        public JsonResult IntegracaoTPU(string ano = "", string codItem = "")
        {
            return Json(new ReparoBLL().IntegracaoTPU(ano, codItem), JsonRequestBehavior.AllowGet);
        }

        // *************** POLITICA DE REPARO  *************************************************************

        /// <summary>
        /// Index
        /// </summary>
        // Politica Reparos
        public ActionResult PoliticaReparos()
        {
            // preenche o combos
            ViewBag.cmbFiltroTiposReparo = new ReparoBLL().PreenchecmbFiltroTiposReparo();
            ViewBag.cmbFiltroLegenda = new AnomaliaBLL().PreenchecmbAnomLegenda();
            ViewBag.cmbFiltroAlerta = new AnomaliaBLL().PreenchecmbAnomAlerta();

            ViewBag.cmbTiposReparo = new ReparoBLL().PreenchecmbFiltroTiposReparo();
            ViewBag.cmbLegenda = new AnomaliaBLL().PreenchecmbAnomLegenda();

            return View();
        }

        /// <summary>
        /// Busca todas as Politicas de Reparo
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult PoliticaReparo_ListAll()
        {
            return Json(new { data = new ReparoBLL().PoliticaReparo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca grid home
        /// </summary>
        /// <param name="model">Dados de Filtro</param>
        /// <returns>JsonResult</returns>
        public JsonResult PoliticaReparo_GetbyID(PoliticaReparoModel model)
        {
            return Json(new { data = new ReparoBLL().PoliticaReparo_GetbyID(model) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere Politica de Reparo
        /// </summary>
        /// <param name="model">Dados a serem inseridos</param>
        /// <returns>JsonResult</returns>
        public JsonResult PoliticaReparo_Inserir(PoliticaReparoModel model)
        {
            return Json(new ReparoBLL().PoliticaReparo_Inserir(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Deleta a Politica de Reparo
        /// </summary>
        /// <param name="rpp_id"></param>
        /// <returns>JsonResult</returns>
        public JsonResult PoliticaReparo_Excluir(int rpp_id)
        {
            int retorno = new ReparoBLL().PoliticaReparo_Excluir(rpp_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Preenche combo de Legenda 
        /// </summary>
        /// <param name="id">Id da Legenda Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreenchecmbFiltroAnomalia(int id)
        {
            return Json(new ReparoBLL().PreenchecmbFiltroAnomalia(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Preenche combo de Legenda 
        /// </summary>
        /// <param name="id">Id da Legenda Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreenchecmbFiltroCausa(int id)
        {
            return Json(new ReparoBLL().PreenchecmbFiltroCausa(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Preenche combo de Alerta 
        /// </summary>
        /// <param name="id">Id da Legenda Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreenchecmbAlerta(int id)
        {
            return Json(new AnomaliaBLL().PreenchecmbAnomAlerta(), JsonRequestBehavior.AllowGet);
        }

    }

}