using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DAO;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ReparoTpuController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: ReparoTpu
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Reparo
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheFontes()
        {
            List<TpuFontesModel> lista = new ReparoTpuDAO().GetFontes();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Reparo
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheListaTpu(TpuDtoModel model)
        {
            List<TpuDtoModel> lista = new ReparoTpuDAO().GetReparoTpu(model);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Reparo
        /// </summary>
        /// <returns></returns>       
        public JsonResult PreencheRep()
        {
            List<PoliticaReparoModel> lista = new ReparoTpuDAO().GerReparo();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// AlterarStatus
        /// </summary>
        /// <param name="rtu_id"></param>
        /// <param name="ativo"></param>
        /// <returns></returns>
        public JsonResult AlterarStatus(int rtu_id, int ativo)
        {
            return Json(new ReparoTpuDAO().AtualizaRtuStatu(rtu_id, ativo), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Editar
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult Editar(TpuDtoModel model)
        {
            return Json(new ReparoTpuDAO().AtualizaRtuEdit(model), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista das TPUs
        /// </summary>
        /// <param name="ano">Ano</param>
        /// <param name="codItem"></param>
        /// <returns>JsonResult Lista tpu</returns>
        public JsonResult IntegracaoTPU(string ano = "", string codItem = "")
        {
            double price = 0;
            DateTime data = Convert.ToDateTime(ano);
            string onerado = "";
            string fase = "";
            string x_ano = data.Year.ToString();
            string x_mes = data.Month.ToString();
            Gerais saida = new Gerais();
            List<tpu> retorno = saida.get_TPUs(x_ano, fase, x_mes, onerado.Trim() == "" ? "" : onerado,codItem);
            var list = retorno.FirstOrDefault(x => x.CodSubItem == codItem);
            if(list != null)
            {
                price = retorno.FirstOrDefault().PrecoUnitario;
            }
            return Json(price, JsonRequestBehavior.AllowGet);
        }
    }
}