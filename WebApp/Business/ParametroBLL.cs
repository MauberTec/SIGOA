using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;

namespace WebApp.Business
{
    /// <summary>
    /// Parâmetros do Sistema
    /// </summary>
    public class ParametroBLL
    {

        /// <summary>
        /// Lista de todos os Parâmetros
        /// </summary>
        /// <param name="par_id">Filtro por Id de Parâmetro, null para todos</param> 
        /// <returns>Lista de Parametro</returns>
        public List<Parametro> Parametro_ListAll(string par_id = "")
        {
            return new ParametroDAO().Parametro_ListAll(par_id);
        }

        /// <summary>
        /// Dados do Parâmetro selecionado
        /// </summary>
        /// <param name="par_id">Id do Parâmetro selecionado</param>
        /// <returns>Parametro</returns>
        public Parametro Parametro_GetbyID(string par_id)
        {
            return new ParametroDAO().Parametro_ListAll(par_id).FirstOrDefault();
        }

        /// <summary>
        /// Valor do Parâmetro 
        /// </summary>
        /// <param name="par_id">Id do Parâmetro selecionado</param>
        /// <returns>Parametro</returns>
        public string Parametro_GetValor(string par_id)
        {
            return new ParametroDAO().Parametro_GetValor(par_id);
        }


        /// <summary>
        /// Salva Parâmetro
        /// </summary>
        /// <param name="par">Nome do Parâmetro</param>
        /// <param name="usu_id">Id do Usuário logado</param>
        /// <param name="ip">IP do Usuário logado</param>
        /// <returns>int</returns>
        public int Parametro_Salvar(Parametro par)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ParametroDAO().Parametro_Salvar(par, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Lista de Parâmetros de Email
        /// </summary>
        /// <returns>Lista de ParamsEmail</returns>
        public List<ParamsEmail> Parametro_ListAllParamsEmail()
        {
            return new ParametroDAO().Parametro_ListAllParamsEmail();
        }


    }
}