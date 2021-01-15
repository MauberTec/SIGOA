﻿using System;
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
    /// PoliticaConservaController
    /// </summary>
    public class PoliticaConservaController : Controller
    {
        /// <summary>
        /// PoliticaConserva
        /// </summary>
        public ActionResult PoliticaConserva()
        {
            return View();
        }
        /// <summary>
        /// cmbSub1
        /// </summary>
        public JsonResult cmbSub1()
        {
            return Json(new PoliticaConservaDAO().CmbSub1(), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// cmbSub2
        /// </summary>
        public JsonResult cmbSub2(int id)
        {
            return Json(new PoliticaConservaDAO().CmbSub2(id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// cmbSub2
        /// </summary>
        public JsonResult Conserva(int id)
        {
            List<ConservaPolitica> j = RequestConserva(id);
            return Json(j, JsonRequestBehavior.AllowGet);
        }

        private static List<ConservaPolitica> RequestConserva(int id)
        {
            return new PoliticaConservaDAO().Conserva(id);
        }

        /// <summary>
        /// Variaveis
        /// </summary>
        /// 
        public JsonResult Variaveis(string Id)
        {
            List<GruposVariaveisValores> vr = RequestVariaveis(Id);

            return Json(vr, JsonRequestBehavior.AllowGet);
        }

        private List<GruposVariaveisValores> RequestVariaveis(string Id)
        {
            List<GruposVariaveisValores> vr = new List<GruposVariaveisValores>();
            var gr = GruposVariaveisValores_ListAll();
            string s = Id.Substring(0, 2);
            if (s.Length == 1)
            {
                s = s + "0";
            }
            var resp = gr.Where(x => x.tip_id_grupo == Convert.ToInt32(s)).ToList();
            resp.ForEach(item =>
            {
                vr.Add(new GruposVariaveisValores { ogv_id = item.ogv_id, variavel = item.variavel });
            });
            return vr;
        }

        // *************** GRUPOS / VARIÁVEIS / VALORES DE INSPEÇÃO  *************************************************************
        /// <summary>
        /// Lista Grupos/Variáveis do Objeto Selecionado      
        /// <summary>
        public List<GruposVariaveisValores> GruposVariaveisValores_ListAll()
        {
            try
            {
                List<GruposVariaveisValores> lst = new List<GruposVariaveisValores>();
                return new PoliticaConservaDAO().GruposVariaveis(lst);
            }
            catch (Exception ex)
            {
                int id = 0;
                //new LogSistemaDAO().InserirLogErro(new LogErro(ex, this.GetType().Name, new StackTrace().GetFrame(0).GetMethod().Name), out id);
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// UpdateConserva
        /// </summary>
        /// 
        public JsonResult UpdateConserva(string ocp_id1, string ocp_id2, string ocp_id3, string descri1, string descri2, string descri3)
        {
            int count = 3;
            string ocp_id = string.Empty;
            string descri = string.Empty;
            for (int i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    ocp_id = ocp_id1;
                    descri = descri1;
                }
                else if (i == 1)
                {
                    ocp_id = ocp_id2;
                    descri = descri2;
                }
                else if (i == 2)
                {
                    ocp_id = ocp_id3;
                    descri = descri3;
                }
                new PoliticaConservaDAO().EditConservaModal(ocp_id, descri);
            }


            return Json("", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Listar
        /// </summary>
        /// 
        public JsonResult Listar(string sub1, string sub2, string sub3, string grupo, string variavel, int variavelId)
        {
            string tp = string.Empty;
            List<ConservaPoliticaModel> model = new List<ConservaPoliticaModel>();
            var lista = RequestConserva(variavelId);
            if(sub2 == "--Selecione--")
            {
                sub2 = "";
            }
            if (sub3 == "--Selecione--")
            {
                sub3 = "";
            }
            foreach (var item in lista)
            {
                if(item.ogi_id_caracterizacao_situacao == 1)
                {
                    tp = "A";
                }
                else if (item.ogi_id_caracterizacao_situacao == 2)
                {
                    tp = "N";
                }
                else if (item.ogi_id_caracterizacao_situacao == 3)
                {
                    tp = "C";
                }
                model.Add(new ConservaPoliticaModel
                {
                    Sub1 = sub1,
                    Sub2 = sub2,
                    Sub3 = sub3,
                    Grupos = grupo,
                    Variavel = variavel,
                    ocp_id = item.ocp_id,
                    alerta = item.ocp_descricao_alerta,
                    servico = item.ocp_descricao_servico,
                    tipo = tp
                });
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Edita linha unica do grid de pesquisa
        /// </summary>
        /// 
        public JsonResult Edti(string ocp_id, string alerta, string conserva)
        {
            string response = string.Empty;
            if(!string.IsNullOrEmpty(ocp_id) && !string.IsNullOrEmpty(alerta) && !string.IsNullOrEmpty(conserva))
            {
                response = new PoliticaConservaDAO().EditConservaHome(ocp_id, alerta, conserva);
            }
            else
            {
                response = "Preencha todos so campos!";
            }           

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        
    }
}