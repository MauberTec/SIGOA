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
using WebApp.DAO;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador de ConservaLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class ConservaController : Controller
    {

        // *************** TIPO  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ConservaTipo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Tipos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de ConservaTipos</returns>
        public JsonResult ConservaTipo_ListAll()
        {
            return Json(new { data = new ConservaBLL().ConservaTipo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Tipo Selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo Selecionado</param>
        /// <returns>JsonResult ConservaTipo</returns>
        public JsonResult ConservaTipo_GetbyID(int ID)
        {
            return Json(new ConservaBLL().ConservaTipo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ConservaTipo_Excluir(int id)
        {
            int retorno = new ConservaBLL().ConservaTipo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ConservaTipo_AtivarDesativar(int id)
        {
            int retorno = new ConservaBLL().ConservaTipo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="oct">Dados do Tipo</param>
        /// <returns>JsonResult</returns>
        public JsonResult ConservaTipo_Salvar(ConservaTipo oct)
        {
            return Json(new ConservaBLL().ConservaTipo_Salvar(oct), JsonRequestBehavior.AllowGet);
        }





        // *************** POLITICA DE CONSERVA  *************************************************************
        /// <summary>
        /// PoliticaConserva
        /// </summary>
        public ActionResult PoliticaConserva()
        {
            // preenche os combos
            ViewBag.ComboConserva3 = new ConservaBLL().PreenchecmbConserva();
            ViewBag.ComboGrupo3 = new ConservaBLL().PreenchecmbGrupo();
            ViewBag.ComboVariavel3 = new ConservaBLL().PreenchecmbVariavel();
            ViewBag.ComboAlerta3 = new ConservaBLL().PreenchecmbAlerta();


            ViewBag.ComboConserva2 = new ConservaBLL().PreenchecmbConserva();
            ViewBag.ComboGrupo_ad = new ConservaBLL().PreenchecmbGrupo();

            return View();
        }

        /// <summary>
        /// Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="cot_id">Id da Conserva selecionada no filtro</param>
        /// <param name="tip_nome">Grupo da Conserva selecionada no filtro</param>
        /// <param name="cov_nome">Variavel da Conserva selecionada no filtro</param>
        /// <returns>JsonResult Lista de PoliticaConserva</returns>
        public JsonResult PoliticaConserva_ListAll(int cot_id = 0, string tip_nome = "", string cov_nome = "")
        {
            // lista do grid
            List<Conserva_GrupoObjetos> lstMain = new ConservaBLL().PoliticaConserva_ListAll(cot_id, tip_nome, cov_nome);
            lstMain.Sort((x, y) => x.cot_descricao.CompareTo(y.cot_descricao));


            // lista do combo de filtro Conserva : somente os itens presentes na lista principal
            List<SelectListItem> lstConserva = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstMain.GroupBy(x => x.cot_descricao).Select(x => x.First()).ToList())
            {
                if (temp.cot_codigo.Trim() != "")
                {
                    lstConserva.Add(new SelectListItem() { Text = temp.cot_descricao, Value = temp.cot_id.ToString() });
                }
            }
            lstConserva.Sort((x, y) => x.Text.CompareTo(y.Text));

            // lista do combo de filtro Grupo : somente os itens presentes na lista principal
            List<SelectListItem> lstGrupo = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstMain.GroupBy(x => x.tip_nome).Select(x => x.First()).ToList())
            {
                if (temp.tip_nome.Trim() != "")
                {
                    lstGrupo.Add(new SelectListItem() { Text = temp.tip_nome, Value = temp.tip_nome });
                }
            }
            lstGrupo.Sort((x, y) => x.Text.CompareTo(y.Text));

            // lista do combo de filtro Variavel : somente os itens presentes na lista principal
            List<SelectListItem> lstVariavel = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstMain.GroupBy(x => x.cov_nome).Select(x => x.First()).ToList())
            {
                if (temp.cov_nome.Trim() != "")
                {
                    lstVariavel.Add(new SelectListItem() { Text = temp.cov_nome, Value = temp.cov_nome });
                }
            }
            lstVariavel.Sort((x, y) => x.Text.CompareTo(y.Text));


            // Lista dos Alertas
            List<SelectListItem> lstAlerta = new ConservaBLL().PreenchecmbAlerta(); // lista de combo
            
            // retorna
            return Json(new { dtMain = lstMain, dtConserva = lstConserva , dtGrupo = lstGrupo, dtVariavel = lstVariavel, dtAlerta = lstAlerta }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Politica de Conserva
        /// </summary>
        /// <param name="cop_id">Id da Politica de Conserva Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PoliticaConserva_Excluir(int cop_id)
        {
            int retorno = new ConservaBLL().PoliticaConserva_Excluir(cop_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        /////  Salva os dados da Politica
        ///// </summary>
        ///// <param name="cop_id">Id da Politica</param>
        ///// <param name="cot_id">Id do Tipo de Conserva</param>
        ///// <param name="tip_nome">Nome do Grupo</param>
        ///// <param name="cov_id">Id da Variavel</param>
        ///// <param name="ogi_id_caracterizacao_situacao">Id do Alerta</param>
        ///// <returns>JsonResult</returns>
        //[HttpPost]
        //public JsonResult PoliticaConserva_Salvar(int cop_id, int cot_id, string tip_nome, int cov_id, int ogi_id_caracterizacao_situacao)
        //{
        //    return Json(new ConservaBLL().PoliticaConserva_Salvar(cop_id, cot_id, tip_nome, cov_id, ogi_id_caracterizacao_situacao), JsonRequestBehavior.AllowGet);
        //}


        /// <summary>
        ///  Insere os dados da Politica
        /// </summary>
        /// <param name="tip_nome">Nome do Grupo</param>
        /// <param name="lst_ogi_id_caracterizacao_situacao">Lista dos Alertas selecionados</param>
        /// <param name="lst_cov_descricao">Lista das Variaveis selecionadas</param>
        /// <param name="cot_id">Id do Tipo de Conserva</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PoliticaConserva_Inserir(string tip_nome, string lst_ogi_id_caracterizacao_situacao, string lst_cov_descricao, int cot_id)
        {
            return Json(new ConservaBLL().PoliticaConserva_Inserir(tip_nome, lst_ogi_id_caracterizacao_situacao, lst_cov_descricao, cot_id), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Busca todas as Variaveis de Conservas pertencentes ao Grupo Selecionado
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult PreenchecmbVariavel_tip_nome(string tip_nome)
        {
            return Json(new ConservaBLL().PreenchecmbVariavel_tip_nome(tip_nome), JsonRequestBehavior.AllowGet);

        }




/*


        /// <summary>
        /// cmbSub2
        /// </summary>
        public JsonResult GVariavel(int tipId)
        {
            List<tab_conserva_variaveis> j = new PoliticaConservaDAO().GetVariavel(tipId);
            return Json(j, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GVariavelAll()
        {
            List<tab_conserva_variaveis> j = new PoliticaConservaDAO().GetVariavelAll();
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
            List<PoliticaConservaDAO.Objetos> vr = new PoliticaConservaDAO().GetObjetos();

            return Json(vr, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista Grupos/Variáveis do Objeto Selecionado      
        /// <summary>
        [HttpGet]
        public JsonResult Pesquisar(int cot_id, int cov_id, int tip_id)
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

    */

    }
}