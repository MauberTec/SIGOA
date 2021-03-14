using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DAO;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// PoliticaReparosController
    /// </summary>
    public class PoliticaReparosController : Controller
    {
        Conexao conn = new Conexao();
        /// <summary>
        /// Index
        /// </summary>
        // GET: PoliticaReparos
        public ActionResult Index()
        {
            return View();
        }  
        /// <summary>
        /// LegendaJson
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheLeg()
        {
            List<PoliticaReparoModel> Conserva = new ReparoDAO().GetLegenda();

            return Json(Conserva, JsonRequestBehavior.AllowGet);
        }   
        /// <summary>
        /// LejendaJson
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheAno(string id)
        {
            List<PoliticaReparoModel> lista = new ReparoDAO().GetAnomalia(ref id);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Alerta
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheAlerta()
        {
            List<PoliticaReparoModel> lista = new ReparoDAO().GetAlerta();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Alerta
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheCausa(string id)
        {
            List<PoliticaReparoModel> lista = new ReparoDAO().GetCausa(ref id);

            return Json(lista, JsonRequestBehavior.AllowGet);
        }  
        /// <summary>
        /// Reparo
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheRep()
        {
            List<PoliticaReparoModel> lista = new ReparoDAO().GerReparo();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }  
        /// <summary>
        /// InsertPoliticaReparo
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult InsertPoliticaReparo(PoliticaReparoModel model)
        {
            string response = new ReparoDAO().InsertReparo(model);
            return Json(response, JsonRequestBehavior.AllowGet);
        }       

        /// <summary>
        /// Deleta a Politica de Reparo
        /// </summary>
        /// <param name="rpp_id"></param>
        /// <returns></returns>
        public JsonResult DeleteReparo(int rpp_id)
        {
            string response = new ReparoDAO().DelReparo(rpp_id);
            return Json(response, JsonRequestBehavior.AllowGet);
        }   
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult PreencheRepAll()
        {
            List<PoliticaReparoModel> lista = new List<PoliticaReparoModel>();           
            lista = new ReparoDAO().GetAllRepair();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Busca grid home
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonResult BuscaReparo(PoliticaReparoModel model)
        {
            List<PoliticaReparoModel> lista = new ReparoDAO().GetReparo(model);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}