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
    /// Controlador de PrecoUnitarios de Perfis e/ou de Usuários
    /// </summary>
    public class PrecoUnitarioController : Controller
    {

        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult PrecoUnitario()
        {
            // preenche combos
            ViewBag.cmbDatasReferencia = new PrecoUnitarioBLL().tpu_datas_base_der_ListAll();
            ViewBag.cmbFase = new PrecoUnitarioBLL().PrecoUnitario_Fase_ListAll();

            return View();
        }

        /// <summary>
        /// Lista de todos os PrecoUnitarios não deletados
        /// </summary>
        /// <param name="tpu_data_base_der">Filtro por Data Base</param>
        /// <param name="tpt_id">Filtro por Tipo Onerado/Desonerado</param>
        /// <param name="fas_id">Filtro por Fase</param>
        /// <returns>JsonResult Lista de PrecoUnitarios</returns>
        public JsonResult PrecoUnitario_ListAll(string tpu_data_base_der, string tpt_id, int fas_id)
        {
            var jsonResult = Json(new {data = new PrecoUnitarioBLL().PrecoUnitario_ListAll(tpu_data_base_der, tpt_id, fas_id) }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

    }
}