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
    public class ConservaBLL
    {

        // *************** TIPO  *************************************************************

        /// <summary>
        ///  Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="cot_id">Filtro por Id do Tipo, null para todos</param>
        /// <returns>Lista de ConservaTipos</returns>
        public List<ConservaTipo> ConservaTipo_ListAll(int? cot_id = null)
        {
            return new ConservaDAO().ConservaTipo_ListAll(cot_id);
        }

        /// <summary>
        /// Dados do Tipo selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo selecionado</param>
        /// <returns>ConservaTipo</returns>
        public ConservaTipo ConservaTipo_GetbyID(int ID)
        {
            return  new ConservaDAO().ConservaTipo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="cot_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int ConservaTipo_Excluir(int cot_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ConservaDAO().ConservaTipo_Excluir(cot_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="cot_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int ConservaTipo_AtivarDesativar(int cot_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ConservaDAO().ConservaTipo_AtivarDesativar(cot_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="rpt">Dados do Tipo</param>
        /// <returns>int</returns>
        public int ConservaTipo_Salvar(ConservaTipo rpt)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ConservaDAO().ConservaTipo_Salvar(rpt, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** POLITICA DE ConservaS  *************************************************************

        /// <summary>
        /// Busca todas as Politicas de Conserva
        /// </summary>
        /// <returns>Lista de Conserva_GrupoObjetos</returns>
        public List<Conserva_GrupoObjetos> PoliticaConserva_ListAll(int cot_id, string tip_nome, string cov_nome)
        {
           List <Conserva_GrupoObjetos>lstMain =  new ConservaDAO().PoliticaConserva_ListAll(cot_id, tip_nome, cov_nome);
           List<Conserva_GrupoObjetos> distinctV = lstMain
                                                            .GroupBy(m => new { m.cot_descricao, m.tip_nome, m.cov_nome, m.ogi_id_caracterizacao_situacao })
                                                            .Select(group => group.First())
                                                            .ToList();
            return distinctV;
        }


        /// <summary>
        ///     Lista de todos os Tipos de Conserva para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbConserva()
        {
            List<ConservaTipo> lst = ConservaTipo_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lst)
            {
                if (temp.cot_codigo.Trim() != "")
                {
                    lstSaida.Add(new SelectListItem() { Text = temp.cot_descricao, Value = temp.cot_id.ToString() });
                }
            }

            return lstSaida;
        }


        /// <summary>
        ///     Lista de todos os Grupos para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbGrupo()
        {
            List<ObjTipo> lst = new ObjetoDAO().ObjTipo_ListAll(9, null, null, 0, 0);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                if (temp.tip_nome.Trim() != "")
                {
                    lstSaida.Add(new SelectListItem() { Text = temp.tip_nome, Value = temp.tip_nome });
                }
            }

          //  lstSaida.Insert(0, new SelectListItem() { Text = "-- Selecione --", Value = "", Disabled = true });
            return lstSaida.GroupBy(x => x.Text).Select(x => x.First()).ToList(); 
        }


        /// <summary>
        ///     Lista de todas Variaveis de Grupo para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbVariavel()
        {
            List<Conserva_GrupoObjetos> lst = new ConservaDAO().PoliticaConservaVariaveis_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                if (temp.cov_nome.Trim() != "")
                {
                    lstSaida.Add(new SelectListItem() { Text = temp.cov_nome, Value = temp.cov_id.ToString() });
                }
            }

          //  lstSaida.Insert(0, new SelectListItem() { Text = "-- Selecione --", Value = "", Disabled = true });
            return lstSaida; 
        }


        /// <summary>
        /// Busca todas as Variaveis de Conservas pertencentes ao Grupo Selecionado
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbVariavel_tip_nome(string tip_nome)
        {
            List<Conserva_GrupoObjetos> lst = new ConservaDAO().PoliticaConservaVariaveis_ListAll_Tip_nome(tip_nome);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                if (temp.cov_nome.Trim() != "")
                {
                    lstSaida.Add(new SelectListItem() { Text = temp.cov_nome, Value = temp.cov_id.ToString() });
                }
            }

            //  lstSaida.Insert(0, new SelectListItem() { Text = "-- Selecione --", Value = "", Disabled = true });
            return lstSaida.GroupBy(x => x.Text).Select(x => x.First()).ToList();
        }



        /// <summary>
        ///     Lista de todos os Alertas para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbAlerta()
        {
            List<Conserva_GrupoObjetos> lst = new ConservaDAO().PoliticaConservaAlerta_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                if (temp.ogi_item.Trim() != "")
                {
                    lstSaida.Add(new SelectListItem() { Text = temp.ogi_item, Value = temp.ogi_id_caracterizacao_situacao.ToString() });
                }
            }

           // lstSaida.Insert(0, new SelectListItem() { Text = "-- Selecione --", Value = "", Disabled = true });
            return lstSaida; 
        }


        /// <summary>
        ///  Excluir (logicamente) Politica de Conserva
        /// </summary>
        /// <param name="cop_id">Id da Politica Selecionada</param>
        /// <returns>int</returns>
        public int PoliticaConserva_Excluir(int cop_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ConservaDAO().PoliticaConserva_Excluir(cop_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        ///// <summary>
        /////  Salva os dados da Politica
        ///// </summary>
        ///// <param name="cop_id">Id da Politica</param>
        ///// <param name="cot_id">Id do Tipo de Conserva</param>
        ///// <param name="tip_nome">Nome do Grupo</param>
        ///// <param name="cov_id">Id da Variavel</param>
        ///// <param name="ogi_id_caracterizacao_situacao">Id do Alerta</param>
        ///// <returns>int</returns>
        //public int PoliticaConserva_Salvar(int cop_id, int cot_id, string tip_nome, int cov_id, int ogi_id_caracterizacao_situacao)
        //{
        //    Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
        //    return new ConservaDAO().PoliticaConserva_Salvar(cop_id, cot_id, tip_nome, cov_id, ogi_id_caracterizacao_situacao, paramUsuario.usu_id, paramUsuario.usu_ip);
        //}


        /// <summary>
        ///  Insere os dados da Politica
        /// </summary>
        /// <param name="tip_nome">Nome do Grupo</param>
        /// <param name="lst_ogi_id_caracterizacao_situacao">Lista dos Alertas selecionados</param>
        /// <param name="lst_cov_descricao">Lista das Variaveis selecionadas</param>
        /// <param name="cot_id">Id do Tipo de Conserva</param>
        /// <returns>int</returns>
        public int PoliticaConserva_Inserir(string tip_nome, string lst_ogi_id_caracterizacao_situacao, string lst_cov_descricao, int cot_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ConservaDAO().PoliticaConserva_Inserir(tip_nome, lst_ogi_id_caracterizacao_situacao, lst_cov_descricao, cot_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /*        /// <summary>
                /// <summary>
                /// Dados da Politica selecionada
                /// </summary>
                /// <param name="model">Dados de Filtro</param>
                /// <returns>Lista de PoliticaConservaModel</returns>
                public List<PoliticaConservaModel> PoliticaConserva_GetbyID(PoliticaConservaModel model)
                {
                    return  new ConservaDAO().PoliticaConserva_GetbyID(model);
                }



                ///  Insere Politica de Conserva
                /// </summary>
                /// <param name="model">Dados a serem inseridos</param>
                /// <returns>int</returns>
                public int PoliticaConserva_Inserir(PoliticaConservaModel model)
                {
                    Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
                    return  new ConservaDAO().PoliticaConserva_Inserir(model, paramUsuario.usu_id, paramUsuario.usu_ip );
                }

        */


    }
}