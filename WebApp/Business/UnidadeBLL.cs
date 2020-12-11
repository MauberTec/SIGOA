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
    /// Unidades de Perfis e/ou de Usuários
    /// </summary>
    public class UnidadeBLL
    {
        // *************** Tipo de Unidade  *************************************************************

        /// <summary>
        ///  Lista de todos Tipos não deletados
        /// </summary>
        /// <param name="unt_id">Filtro por Id do Tipo de Unidade, null para todos</param>
        /// <param name="unt_nome">Filtro por Nome do Tipo de Unidade</param>
        /// <returns>Lista de Unidade_Tipo</returns>
        public List<Unidade_Tipo> Unidade_Tipo_ListAll(int? unt_id = null, string unt_nome = "")
        {
            return new UnidadeDAO().Unidade_Tipo_ListAll(unt_id, unt_nome);
        }

        /// <summary>
        /// Dados do Tipo de Unidade selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo de Unidade selecionado</param>
        /// <returns>Unidade_Tipo</returns>
        public Unidade_Tipo Unidade_Tipo_GetbyID(int ID)
        {
            return  new UnidadeDAO().Unidade_Tipo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo de Unidade
        /// </summary>
        /// <param name="unt">Nome do Tipo de Unidade</param>
        /// <returns>int</returns>
        public int Unidade_Tipo_Salvar(Unidade_Tipo unt)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new UnidadeDAO().Unidade_Tipo_Salvar(unt, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Tipo de Unidade
        /// </summary>
        /// <param name="unt_id">Id do Tipo de Unidade Selecionado</param>
        /// <returns>int</returns>
        public int Unidade_Tipo_Excluir(int unt_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new UnidadeDAO().Unidade_Tipo_Excluir(unt_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Preenchimento do combo de Tipos de Unidade 
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbUnidade_Tipo()
        {
            List<Unidade_Tipo> lstUnidade_Tipo = new UnidadeDAO().Unidade_Tipo_ListAll(); // lista de "Unidade_Tipo"
            List<SelectListItem> lstListaUnidade_Tipo = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lstUnidade_Tipo)
            {
                lstListaUnidade_Tipo.Add(new SelectListItem() { Text = temp.unt_nome, Value = temp.unt_id.ToString() });
            }

            return lstListaUnidade_Tipo;
        }


        // *************** Unidade  *************************************************************

        /// <summary>
        ///  Lista de todas Unidades não deletados
        /// </summary>
        /// <param name="uni_id">Filtro por Id da Unidade, null para todos</param>
        /// <param name="unt_id">Filtro por Id do Tipo de Unidade, null para todos</param>
        /// <returns>Lista de Unidades</returns>
        public List<Unidade> Unidade_ListAll(int? uni_id = null, int? unt_id = null)
        {
            return new UnidadeDAO().Unidade_ListAll(uni_id, unt_id);
        }

        /// <summary>
        /// Dados da Unidade selecionada
        /// </summary>
        /// <param name="ID">Id da Unidade selecionada</param>
        /// <returns>Unidade</returns>
        public Unidade Unidade_GetbyID(int ID)
        {
            Unidade uni =  new UnidadeDAO().Unidade_ListAll(ID, null).FirstOrDefault();
            uni.lstUnidade_Tipo = PreenchecmbUnidade_Tipo();
            return uni;
        }

        /// <summary>
        ///  Insere ou Altera os dados da Unidade
        /// </summary>
        /// <param name="uni">Nome da Unidade</param>
        /// <returns>int</returns>
        public int Unidade_Salvar(Unidade uni)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new UnidadeDAO().Unidade_Salvar(uni, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Unidade
        /// </summary>
        /// <param name="uni_id">Id da Unidade Selecionada</param>
        /// <returns>int</returns>
        public int Unidade_Excluir(int uni_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new UnidadeDAO().Unidade_Excluir(uni_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


    }






}