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
using WebApp.Helpers;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador de Integracao
    /// </summary>
    public class IntegracaoController : Controller
    {
        // *************** Integracao SIRGeo *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult IntegracaoSIRGeo()
        {
            return View();
        }

        /// <summary>
        /// Lista das Rodovias
        /// </summary>
        /// <param name="rod_Codigo">Filtro por Codigo da Rodovia</param>
        /// <returns>JsonResult Lista de Rodovias</returns>
        public JsonResult Integracao_Rodovias_ListAll(string rod_Codigo = "")
        {
            Gerais saida = new Gerais();
            List<Rodovia> listaDeRodovias = saida.get_Rodovias(rod_Codigo);
            
            return Json(new { data = listaDeRodovias }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista das OAEs
        /// </summary>
        /// <param name="rod_id">Filtro por Id da Rodovia</param>
        /// <returns>JsonResult Lista de Rodovias</returns>
        public JsonResult Integracao_OAEs_ListAll(string rod_id = "")
        {
            Gerais saida = new Gerais();
            List<OAE> listaDeOAEs = saida.get_OAEs(rod_id);
            
            return Json(new { data = listaDeOAEs }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista das Regionais
        /// </summary>
        /// <returns>JsonResult Lista de OAEs</returns>
        public JsonResult Integracao_Regionais_ListAll()
        {
            Gerais saida = new Gerais();
            List<Regional> listaDeRegionais = saida.get_Regionais();
            
            return Json(new { data = listaDeRegionais }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista dos Sentidos das Rodovias
        /// </summary>
        /// <returns>JsonResult Lista de Rodovias</returns>
        public JsonResult Integracao_SentidoRodovias_ListAll()
        {
            Gerais saida = new Gerais();
            List<SentidoRodovia> listaDeSentidoRodovias = saida.get_SentidoRodovias();
            
            return Json(new { data = listaDeSentidoRodovias }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lista dos Tipos de OAE
        /// </summary>
        /// <returns>JsonResult Lista de TipoOAE</returns>
        public JsonResult Integracao_TiposOAE_ListAll()
        {
            Gerais saida = new Gerais();
            List<TipoOAE> listaDeTiposOAE = saida.get_TiposOAE();
            
            return Json(new { data = listaDeTiposOAE }, JsonRequestBehavior.AllowGet);
        }

        // *************** Integracao DER - VDMs *************************************************************

        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult IntegracaoVDM()
        {
            return View();
        }

        /// <summary>
        /// Lista das Rodovias
        /// </summary>
        /// <param name="rod_codigo">Filtro por Código da Rodovia</param>
        /// <param name="kminicial">Km Inicial</param>
        /// <param name="kmfinal">km Final</param>
        /// <returns>JsonResult</returns>
        public JsonResult Integracao_VDMs_ListAll(string rod_codigo = "", decimal kminicial = 0, decimal kmfinal = 0)
        {
            Gerais saida = new Gerais();
            List<vdm> retorno = saida.get_VDMs(rod_codigo.ToUpper().Trim(), kminicial, kmfinal);
            
            return Json(new { data = retorno }, JsonRequestBehavior.AllowGet);
        }


        // *************** Integracao DER - TPUs *************************************************************

        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult IntegracaoTPU()
        {
            return View();
        }


        /// <summary>
        /// Lista das TPUs
        /// </summary>
        /// <param name="ano">Ano</param>
        /// <param name="fase">fase = 23</param>
        /// <param name="mes">Mês</param>
        /// <param name="onerado">Onerado: SIM,NÃO, vazio para todos</param>
        /// <returns>JsonResult Lista tpu</returns>
        public JsonResult Integracao_TPUs_ListAll(string ano = "", string fase = "", string mes = "01", string onerado = "")
        {
            Gerais saida = new Gerais();            
            List<tpu> retorno = saida.get_TPUs(ano == "" ? DateTime.Now.Year.ToString() : ano, fase,  mes.Trim() == "" ? "01" : mes, onerado.Trim() == "" ? "" : onerado);
            
            return Json(new { data = retorno }, JsonRequestBehavior.AllowGet);
        }

    }
}