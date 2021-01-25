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
        public JsonResult Conserva(int id)
        {
            List<ConservaPolitica> j = RequestConserva(id);
            return Json(j, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// GetAllConserva
        /// </summary>
        public JsonResult GetAllConserva()
        {
            List<ConservaModel> lista = new PoliticaConservaDAO().GetAllConserva();
            
            List<ConservaDTO> conservas = new List<ConservaDTO>();
            var groups = lista.GroupBy(x => x.ogv_id);
            List<ConservaModel> ConservasArray;
            foreach (var _item in groups)
            {
                ConservasArray = new List<ConservaModel>();
                foreach (var item in _item)
                {
                    ConservasArray.Add(new ConservaModel
                    {
                        Alerta = item.Alerta,
                        ocp_id = item.ocp_id,
                        tip_nome = item.tip_nome,
                        Grupo = item.Grupo,
                        ogv_id = item.ogv_id,
                        Servico = item.Servico,
                        Situacao = item.Situacao,
                        Variavel = item.Variavel,
                        ale_codigo = item.ale_codigo
                    });
                }
                conservas.Add(new ConservaDTO
                {
                    Numero_Ogv = _item.Key,
                    Conservas = ConservasArray

                });
               
            }


            return Json(conservas, JsonRequestBehavior.AllowGet);
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
        /// Listar
        /// </summary>
        /// 
        public JsonResult Listar(string sub1, string sub2, string sub3, string grupo, string variavel, int variavelId)
        {
            string tp = string.Empty;
            List<ConservaPoliticaModel> model = new List<ConservaPoliticaModel>();
            var lista = RequestConserva(variavelId);
            
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
        public JsonResult ConservaTipo_Salvar(string oct_cod, string oct_descricao, string oct_ativo, int oct_id = 0)
        {
            string resp = string.Empty;
            if(oct_id == 0)
            {
                resp = new PoliticaConservaDAO().InsertConservaTipo(oct_cod, oct_descricao, oct_ativo, "4");
            }
            else if(oct_id > 0)
            {
                resp = new PoliticaConservaDAO().UpdateConservaTipo(oct_id,oct_cod, oct_descricao, oct_ativo, "4");
            }
               
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GetAllConservaTipo
        /// </summary>
        public JsonResult GetAllConservaTipo()
        {           
            return Json(new PoliticaConservaDAO().GetConservaTipo(), JsonRequestBehavior.AllowGet);
        }

        ///<summary>
        ///Get Tipo Editar
        /// </summary>
        public ActionResult GetEdtit(int id)
        {
            var ret = new PoliticaConservaDAO().GetConservaTipo();
            var resp = ret.FirstOrDefault(x => x.oct_id == id);
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}
