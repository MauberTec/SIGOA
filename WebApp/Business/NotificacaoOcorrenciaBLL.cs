using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;

namespace WebApp.Business
{
    /// <summary>
    /// NotificacaoOcorrencia
    /// </summary>
    public class NotificacaoOcorrenciaBLL
    {

        /// <summary>
        /// Lista de todos as NotificacaoOcorrencia não deletadas
        /// </summary>
        /// <param name="ord_id">id da O.S. da notificacao</param>
        /// <returns>Lista de NotificacaoOcorrencia</returns>
        public List<Notificacao_Ocorrencia> NotificacaoOcorrencia_ListAll(int ord_id)
        {
            return new NotificacaoOcorrenciaDAO().NotificacaoOcorrencia_ListAll(ord_id);
        }

        /// <summary>
        ///  Insere ou Altera os dados da Notificacao Ocorrencia
        /// </summary>
        /// <param name="notOcor">Dados da Notificacao Ocorrencia</param>
        /// <returns>int</returns>
        public int NotificacaoOcorrencia_Salvar(Notificacao_Ocorrencia notOcor)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new NotificacaoOcorrenciaDAO().NotificacaoOcorrencia_Salvar(notOcor, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


    }
}