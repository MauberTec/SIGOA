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
        /// Alerta
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

            string leg_codigo = "";
            string atp_codigo = "";
            string ale_codigo = "";
            string leg_id = "";
            string ale_id = "";
            string atp_id = "";
            string response = "";

            string[] leg = model.leg_codigo.Split('-');
            leg_id = leg[0];
            leg_codigo = leg[1];

            string[] ale = model.ale_codigo.Split('-');
            ale_id = ale[0];
            ale_codigo = ale[1];

            string[] atp = model.atp_codigo.Split('-');
            atp_id = atp[0];
            atp_codigo = atp[1];

            string aca_id = "";
            string[] aca = model.aca_id.Split('-');
            aca_id = aca[0];

            string rpt_id = "";
            string rpt_codigo = "";
            string[] rpt = model.rpt_id.Split('-');
            rpt_id = rpt[0];
            rpt_codigo = rpt[1];



            using (SqlConnection con = new SqlConnection(new Conexao().strConn))
            {
                try
                {

                    con.Open();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "STP_INS_REPARO_POLITICA";

                    com.Connection = con;
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Clear();

                    System.Data.SqlClient.SqlParameter p_return = new System.Data.SqlClient.SqlParameter();
                    p_return.Direction = System.Data.ParameterDirection.ReturnValue;
                    com.Parameters.Add(p_return);
                    com.Parameters[0].Size = 32000;

                    com.Parameters.AddWithValue("@leg_codigo", leg_codigo);
                    com.Parameters.AddWithValue("@atp_codigo", atp_codigo);
                    com.Parameters.AddWithValue("@ale_codigo", ale_codigo);
                    com.Parameters.AddWithValue("@aca_id", aca_id);

                    com.Parameters.AddWithValue("@rpt_id", rpt_id);
                    com.Parameters.AddWithValue("@rpp_ativo", 1);
                    com.Parameters.AddWithValue("@rpp_criado_por", 4);

                    com.Parameters.AddWithValue("@leg_id", leg_id);
                    com.Parameters.AddWithValue("@ale_id", ale_id);
                    com.Parameters.AddWithValue("@atp_id", atp_id);

                    com.ExecuteScalar();
                    int id = Convert.ToInt32(p_return.Value);
                    response = "0";
                }
                catch (Exception ex)
                {
                    response = "1";
                }

            }
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