using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Web.Mvc;

namespace WebApp.Business
{
    /// <summary>
    /// AnomLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class ReparoBLL
    {

        // *************** TIPO  *************************************************************

        /// <summary>
        ///  Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="rpt_id">Filtro por Id do Tipo, null para todos</param>
        /// <returns>Lista de ReparoTipos</returns>
        public List<ReparoTipo> ReparoTipo_ListAll(int? rpt_id = null)
        {
            return new ReparoDAO().ReparoTipo_ListAll(rpt_id);
        }

        /// <summary>
        /// Dados do Tipo selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo selecionado</param>
        /// <returns>ReparoTipo</returns>
        public ReparoTipo ReparoTipo_GetbyID(int ID)
        {
            return  new ReparoDAO().ReparoTipo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="rpt_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int ReparoTipo_Excluir(int rpt_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoDAO().ReparoTipo_Excluir(rpt_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="rpt_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int ReparoTipo_AtivarDesativar(int rpt_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoDAO().ReparoTipo_AtivarDesativar(rpt_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="rpt">Dados do Tipo</param>
        /// <returns>int</returns>
        public int ReparoTipo_Salvar(ReparoTipo rpt)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoDAO().ReparoTipo_Salvar(rpt, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** POLITICA DE REPAROS  *************************************************************

        /// <summary>
        /// Busca todas as Politicas de Reparo
        /// </summary>
        /// <returns>Lista de ReparoTipos</returns>
        public List<PoliticaReparoModel> PoliticaReparo_ListAll()
        {
            return new ReparoDAO().PoliticaReparo_ListAll();
        }

        /// <summary>
        /// Dados da Politica selecionada
        /// </summary>
        /// <param name="model">Dados de Filtro</param>
        /// <returns>Lista de PoliticaReparoModel</returns>
        public List<PoliticaReparoModel> PoliticaReparo_GetbyID(PoliticaReparoModel model)
        {
            return  new ReparoDAO().PoliticaReparo_GetbyID(model);
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="rpp_id">Id da Politica Selecionada</param>
        /// <returns>int</returns>
        public int PoliticaReparo_Excluir(int rpp_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoDAO().PoliticaReparo_Excluir(rpp_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        ///  Insere Politica de Reparo
        /// </summary>
        /// <param name="model">Dados a serem inseridos</param>
        /// <returns>int</returns>
        public int PoliticaReparo_Inserir(PoliticaReparoModel model)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return  new ReparoDAO().PoliticaReparo_Inserir(model, paramUsuario.usu_id, paramUsuario.usu_ip );
        }


        /// <summary>
        /// Preenche combo de Tipos de Reparo 
        /// </summary>
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreenchecmbFiltroTiposReparo()
        {
            List<ReparoTipo> lstReparoTipo = ReparoTipo_ListAll();
            List<SelectListItem> lstCmb = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstReparoTipo)
            {
                string txt = temp.rpt_codigo + "-" + temp.rpt_descricao;
                lstCmb.Add(new SelectListItem() { Text = txt, Value = temp.rpt_id.ToString() });
            }
            return lstCmb;
        }


        /// <summary>
        /// Preenche combo de Legenda 
        /// </summary>
        /// <param name="leg_id">Id da Legenda Selecionada</param>
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreenchecmbFiltroAnomalia(int leg_id)
        {
            List<PoliticaReparoModel> lstAnomalia = new ReparoDAO().PoliticaReparo_Anomalia_By_Legenda(leg_id);
            List<SelectListItem> lstCmb = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstAnomalia)
            {
                string txt = temp.atp_codigo + "-" + temp.atp_descricao;
                lstCmb.Add(new SelectListItem() { Text = txt, Value = temp.atp_id.ToString() });
            }
            return lstCmb;
        }


        /// <summary>
        /// Preenche combo de Causas de Anomalia 
        /// </summary>
        /// <param name="leg_id">Id da Legenda Selecionada</param>
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreenchecmbFiltroCausa(int leg_id)
        {
            List<PoliticaReparoModel> lstAnomalia = new ReparoDAO().PoliticaReparo_Causa_By_Legenda(leg_id);
            List<SelectListItem> lstCmb = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstAnomalia)
            {
                string txt = temp.atp_codigo + "-" + temp.atp_descricao;
                lstCmb.Add(new SelectListItem() { Text = txt, Value = temp.atp_id.ToString() });
            }
            return lstCmb;
        }




        // *************** REPARO TPU  *************************************************************

        /// <summary>
        /// Busca lista de Reparos associados a TPU
        /// </summary>
        /// <returns>Lista de TpuDtoModel</returns>
        public List<TpuDtoModel> ReparoTpu_ListAll()
        {
            return new ReparoTpuDAO().ReparoTpu_ListAll();
        }

        /// <summary>
        ///  Ativa/Desativa ReparoTpu
        /// </summary>
        /// <param name="id">Id do Reparo Selecionado</param>
        /// <returns>int</returns>
        public int ReparoTpu_AtivarDesativar(int id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ReparoTpuDAO().ReparoTpu_AtivarDesativar(id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Salva REPARO TPU
         /// </summary>
       /// <param name="model">Dados a serem inseridos</param>
        /// <returns>int</returns>
        public int ReparoTpu_Salvar(TpuDtoModel model)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return  new ReparoTpuDAO().ReparoTpu_Salvar(model, paramUsuario.usu_id, paramUsuario.usu_ip );
        }

        /// <summary>
        /// Busca a lista de Fontes de TPU
        /// </summary>
        /// <returns>List SelectListItem</returns>       
        public List<SelectListItem> PreenchecmbFontesTPU()
        {
            List<TpuFontesModel> lstFontes = new ReparoTpuDAO().ReparoTpu_GetFontesTPU();
            List<SelectListItem> lstCmb = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstFontes)
            {
                lstCmb.Add(new SelectListItem() { Text = temp.fon_nome, Value = temp.fon_id.ToString() });
            }
            return lstCmb;
        }



        /// <summary>
        /// Lista das TPUs
        /// </summary>
        /// <param name="ano">Ano</param>
        /// <param name="codItem"></param>
        /// <returns>Lista tpu</returns>
        public List<tpu> IntegracaoTPU(string ano = "", string codItem = "")
        {
            Decimal price = 0;
            DateTime data = Convert.ToDateTime(ano);
            string onerado = "";
            string fase = "";
            string x_ano = data.Year.ToString();
            string x_mes = data.Month.ToString();
            IntegracaoDAO saida = new IntegracaoDAO();
            List<tpu> retorno = saida.get_TPUs(x_ano, fase, x_mes, onerado.Trim() == "" ? "" : onerado, codItem);
            var list = retorno.FirstOrDefault(x => x.CodSubItem == codItem);
            if (list != null)
            {
                price = retorno.FirstOrDefault().PrecoUnitario;
            }

            return retorno;
        }


    }
}