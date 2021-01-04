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

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador de Notificação de Ocorrência
    /// </summary>
    public class NotificacaoOcorrenciaController : Controller
    {
        // *************** NotificacaoOcorrencia  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Notificacao_Ocorrencia()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos as NotificacaoOcorrencia não deletadas
        /// </summary>
        /// <returns>JsonResult Lista de NotificacaoOcorrencia</returns>
        public JsonResult NotificacaoOcorrencia_ListAll(int ord_id)
        {
            return Json(new { data = new NotificacaoOcorrenciaBLL().NotificacaoOcorrencia_ListAll(ord_id) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///  Altera ou Insere Notificacao Ocorrencia 
        /// </summary>
        /// <param name="notOcor">Dados da Notificacao Ocorrencia</param>
        /// <returns>JsonResult</returns>
        public JsonResult NotificacaoOcorrencia_Salvar(Notificacao_Ocorrencia notOcor)
        {
            return Json(new NotificacaoOcorrenciaBLL().NotificacaoOcorrencia_Salvar(notOcor), JsonRequestBehavior.AllowGet);
        }


    }
}