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
    public class AnomaliaBLL
    {
        // *************** LEGENDA  *************************************************************

        /// <summary>
        ///  Lista de todas as Legendas não deletadas
        /// </summary>
        /// <param name="leg_id">Filtro por Id da Legenda, null para todos</param>
        /// <returns>Lista de AnomLegendas</returns>
        public List<AnomLegenda> AnomLegenda_ListAll(int? leg_id = null)
        {
            return new AnomaliaDAO().AnomLegenda_ListAll(leg_id);
        }

        /// <summary>
        /// Dados da Legenda selecionada
        /// </summary>
        /// <param name="ID">Id da Legenda selecionada</param>
        /// <returns>AnomLegenda</returns>
        public AnomLegenda AnomLegenda_GetbyID(int ID)
        {
            return  new AnomaliaDAO().AnomLegenda_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Legenda
        /// </summary>
        /// <param name="leg_id">Id da Legenda Selecionada</param>
        /// <returns>int</returns>
        public int AnomLegenda_Excluir(int leg_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomLegenda_Excluir(leg_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Legenda
        /// </summary>
        /// <param name="leg_id">Id da Legenda Selecionada</param>
        /// <returns>int</returns>
        public int AnomLegenda_AtivarDesativar(int leg_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomLegenda_AtivarDesativar(leg_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados da Legenda
        /// </summary>
        /// <param name="leg">Dados da Legenda</param>
        /// <returns>int</returns>
        public int AnomLegenda_Salvar(AnomLegenda leg)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomLegenda_Salvar(leg, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // *************** TIPO  *************************************************************

        /// <summary>
        ///  Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="atp_id">Filtro por Id do Tipo, null para todos</param>
        /// <returns>Lista de AnomTipos</returns>
        public List<AnomTipo> AnomTipo_ListAll(int? atp_id = null)
        {
            return new AnomaliaDAO().AnomTipo_ListAll(atp_id);
        }

        /// <summary>
        /// Dados do Tipo selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo selecionado</param>
        /// <returns>AnomTipo</returns>
        public AnomTipo AnomTipo_GetbyID(int ID)
        {
            return  new AnomaliaDAO().AnomTipo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="atp_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int AnomTipo_Excluir(int atp_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomTipo_Excluir(atp_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="atp_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int AnomTipo_AtivarDesativar(int atp_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomTipo_AtivarDesativar(atp_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="atp">Dados do Tipo</param>
        /// <returns>int</returns>
        public int AnomTipo_Salvar(AnomTipo atp)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomTipo_Salvar(atp, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // *************** CAUSA  *************************************************************

        /// <summary>
        ///  Lista de todas as Causas não deletadas
        /// </summary>
        /// <param name="aca_descricao">Filtro por Descricao da Causa de Anomalia, vazio para todos</param>
        /// <param name="leg_id">Filtro por Legenda de Anomalia, opcional</param>
        /// <returns>Lista de AnomCausas</returns>
        public List<AnomCausa> AnomCausa_ListAll(string aca_descricao = "", int? leg_id = null)
        {
            return new AnomaliaDAO().AnomCausa_ListAll(aca_descricao, null, leg_id);
        }

        /// <summary>
        /// Dados da Causa selecionada
        /// </summary>
        /// <param name="ID">Id da Causa selecionada</param>
        /// <returns>AnomCausa</returns>
        public AnomCausa AnomCausa_GetbyID(int ID)
        {
            return  new AnomaliaDAO().AnomCausa_ListAll("",ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Causa
        /// </summary>
        /// <param name="aca_id">Id da Causa Selecionada</param>
        /// <returns>int</returns>
        public int AnomCausa_Excluir(int aca_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomCausa_Excluir(aca_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Causa
        /// </summary>
        /// <param name="aca_id">Id da Causa Selecionada</param>
        /// <returns>int</returns>
        public int AnomCausa_AtivarDesativar(int aca_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomCausa_AtivarDesativar(aca_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados da Causa
        /// </summary>
        /// <param name="aca">Dados da Causa</param>
        /// <returns>int</returns>
        public int AnomCausa_Salvar(AnomCausa aca)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomCausa_Salvar(aca, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista de todos as Legendas para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbAnomLegenda()
        {
            List<AnomLegenda> lst = new AnomaliaDAO().AnomLegenda_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                if (temp.leg_codigo.Trim() != "")
                {
                    string txt = temp.leg_codigo + " - " + temp.leg_descricao;
                    lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.leg_id.ToString() });
                }
            }

            return lstSaida;
        }



        // *************** ALERTA  *************************************************************

        /// <summary>
        ///  Lista de todos os Alertas não deletados
        /// </summary>
        /// <param name="ale_id">Filtro por Id do Alerta, null para todos</param>
        /// <returns>Lista de AnomAlertas</returns>
        public List<AnomAlerta> AnomAlerta_ListAll(int? ale_id = null)
        {
            return new AnomaliaDAO().AnomAlerta_ListAll(ale_id);
        }

        /// <summary>
        /// Dados do Alerta selecionado
        /// </summary>
        /// <param name="ID">Id do Alerta selecionado</param>
        /// <returns>AnomAlerta</returns>
        public AnomAlerta AnomAlerta_GetbyID(int ID)
        {
            return  new AnomaliaDAO().AnomAlerta_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Alerta
        /// </summary>
        /// <param name="ale_id">Id do Alerta Selecionado</param>
        /// <returns>int</returns>
        public int AnomAlerta_Excluir(int ale_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomAlerta_Excluir(ale_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Alerta
        /// </summary>
        /// <param name="ale_id">Id do Alerta Selecionado</param>
        /// <returns>int</returns>
        public int AnomAlerta_AtivarDesativar(int ale_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomAlerta_AtivarDesativar(ale_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Alerta
        /// </summary>
        /// <param name="ale">Dados do Alerta</param>
        /// <returns>int</returns>
        public int AnomAlerta_Salvar(AnomAlerta ale)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomAlerta_Salvar(ale, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///     Lista de todos as Alertas para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbAnomAlerta()
        {
            List<AnomAlerta> lst = new AnomaliaDAO().AnomAlerta_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                if (temp.ale_codigo.Trim() != "")
                {
                    string txt = temp.ale_codigo + " - " + temp.ale_descricao;
                    lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.ale_id.ToString() });
                }
            }

            return lstSaida;
        }


        // *************** STATUS  *************************************************************

        /// <summary>
        ///  Lista de todos os Status não deletados
        /// </summary>
        /// <param name="sta_id">Filtro por Id do Status, null para todos</param>
        /// <returns>Lista de AnomStatus</returns>
        public List<AnomStatus> AnomStatus_ListAll(int? sta_id = null)
        {
            return new AnomaliaDAO().AnomStatus_ListAll(sta_id);
        }

        /// <summary>
        /// Dados do Status selecionado
        /// </summary>
        /// <param name="ID">Id do Status selecionado</param>
        /// <returns>AnomStatus</returns>
        public AnomStatus AnomStatus_GetbyID(int ID)
        {
            return  new AnomaliaDAO().AnomStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Status
        /// </summary>
        /// <param name="sta_id">Id do Status Selecionado</param>
        /// <returns>int</returns>
        public int AnomStatus_Excluir(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomStatus_Excluir(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Status
        /// </summary>
        /// <param name="sta_id">Id do Status Selecionado</param>
        /// <returns>int</returns>
        public int AnomStatus_AtivarDesativar(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomStatus_AtivarDesativar(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status
        /// </summary>
        /// <param name="sta">Dados do Status</param>
        /// <returns>int</returns>
        public int AnomStatus_Salvar(AnomStatus sta)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomStatus_Salvar(sta, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** FLUXO DE STATUS DE ANOMALIA  *************************************************************

        /// <summary>
        ///     Lista de todos os Fluxos de  Status não deletados
        /// </summary>
        /// <returns>Lista de AnomFluxoStatus</returns>
        public List<AnomFluxoStatus> AnomFluxoStatus_ListAll()
        {
            return new AnomaliaDAO().AnomFluxoStatus_ListAll();
        }

        /// <summary>
        /// Dados do Fluxo de Status selecionado
        /// </summary>
        /// <param name="ID">Id do Fluxo de Status selecionado</param>
        /// <returns>AnomFluxoStatus</returns>
        public AnomFluxoStatus AnomFluxoStatus_GetbyID(int ID)
        {
            return new AnomaliaDAO().AnomFluxoStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Insere ou Altera os dados do Fluxo de Status 
        /// </summary>
        /// <param name="fst">Fluxo de Status</param>
        /// <returns>int</returns>
        public int AnomFluxoStatus_Salvar(AnomFluxoStatus fst)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomFluxoStatus_Salvar(fst, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Fluxo de Status
        /// </summary>
        /// <param name="fst_id">Id do Fluxo de Status  Selecionada</param>
        /// <returns>int</returns>
        public int AnomFluxoStatus_Excluir(int fst_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomFluxoStatus_Excluir(fst_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Fluxo de Status
        /// </summary>
        /// <param name="fst_id">Id do Fluxo de Status Selecionado</param>
        /// <returns>int</returns>
        public int AnomFluxoStatus_AtivarDesativar(int fst_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new AnomaliaDAO().AnomFluxoStatus_AtivarDesativar(fst_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista de todos os Status para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> preencheCmbStatus()
        {
            List<AnomStatus> lst = new AnomaliaDAO().AnomStatus_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.ast_descricao + " (" + temp.ast_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.ast_id.ToString() });
            }

            return lstSaida;
        }



    }
}