using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.DAO;
using WebApp.Models;
using static WebApp.DAO.PoliticaConservaDAO;

namespace WebApp.Controllers
{
    /// <summary>
    /// PoliticaConservaController
    /// </summary>
    public class PoliticaConservaController : Controller
    {
        /// <summary>
        /// PoliticaConserva
        /// </summary>
        [ValidateInput(false)]
        public ActionResult PoliticaConserva()
        {
            return View();
        }

        /// <summary>
        /// cmbSub2
        /// </summary>
        public JsonResult GVariavel()
        {
            List<tab_conserva_variaveis> j = new PoliticaConservaDAO().GetVariavel();
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Conserva
        /// </summary>
        public JsonResult GConserva()
        {
            List<tab_conserva_tipos> j = new PoliticaConservaDAO().GetConserva();
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Alerta
        /// </summary>
        public JsonResult GAlerta()
        {
            List<PoliticaReparoModel> j = new PoliticaConservaDAO().GetAlerta();
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        private static List<tab_conserva_tipos> RequestConserva()
        {
            return new PoliticaConservaDAO().GetConserva();
        }

        /// <summary>
        /// Grupos
        /// </summary>
        /// 
        public JsonResult GGrupo(string Id)
        {
            List<Objetos> vr = new PoliticaConservaDAO().GetObjetos();

            return Json(vr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista Grupos/Variáveis do Objeto Selecionado      
        /// <summary>
        public JsonResult Perquisar(int cot_id, int cov_id, int tip_id)
        {
            var resp = new PoliticaConservaDAO().GruposConservaHome(cot_id, cov_id, tip_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// UpdateConserva
        /// </summary>
        /// 
        public JsonResult UpdateConserva(string ocp_id1, string ocp_id2, string ocp_id3, string descri1, string descri2, string descri3, string alerta1, string alerta2, string alerta3)
        {
            int count = 3;
            string ocp_id = string.Empty;
            string descri = string.Empty;
            string alerta = string.Empty;
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    ocp_id = ocp_id1;
                    descri = descri1;
                    alerta = alerta1;
                }
                else if (i == 1)
                {
                    ocp_id = ocp_id2;
                    descri = descri2;
                    alerta = alerta3;
                }
                else if (i == 2)
                {
                    ocp_id = ocp_id3;
                    descri = descri3;
                    alerta = alerta3;
                }
                new PoliticaConservaDAO().EditConserva(ocp_id, alerta, descri);
            }


            return Json("", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Edita linha unica do grid de pesquisa
        /// </summary>
        /// 
        public JsonResult Edti(string ocp_id, string alerta, string conserva)
        {
            string response = string.Empty;
            if (!string.IsNullOrEmpty(ocp_id) && !string.IsNullOrEmpty(alerta) && !string.IsNullOrEmpty(conserva))
            {
                response = new PoliticaConservaDAO().EditConserva(ocp_id, alerta, conserva);
            }
            else
            {
                response = "Preencha todos so campos!";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        ///<sumary>
        /// Tipo Conserva
        ///</sumary>
        public ActionResult ConservaTipo()
        {
            return View();
        }
        /// <summary>
        /// UpdateConserva
        /// </summary>
        /// 
        [HttpPost]
        public JsonResult ConservaTipoSalvar(int tipid, string alerta,int cotid, int covid, int copid)
        {
            GrupoObjetosConserva grupo = new GrupoObjetosConserva()
            {
                tip_id = tipid,
                ale_codigo = alerta,
                cot_id = cotid,
                cov_id = covid,
                cop_id = copid

            };
            string resp = new PoliticaConservaDAO().InsertConserva(grupo);       

            return Json(resp, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// deleta conserva
        /// </summary>
        /// <param name="cop_id"></param>
        /// <returns></returns>
        public JsonResult Deleta(int cop_id)
        {
            string resp = new PoliticaConservaDAO().DeletaConserva(cop_id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }



    }
}
