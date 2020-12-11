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
    /// Controlador dos Registros de Acesso e Alterações no Sistema
    /// </summary>
    public class LogSistemaController : Controller
    {
        /// <summary>
        /// Inicio. Preenchimento dos combos de Usuarios, Transações e Módulos
        /// </summary>
        /// <returns>ActionResult View</returns>
        public ActionResult LogSistema()
        {
            // preenche os combos
            ViewBag.cmbUsuarios = new LogSistemaBLL().lstGetUsuarios();
            ViewBag.cmbTransacoes = new LogSistemaBLL().lstGetTransacao();
            ViewBag.cmbModulos = new LogSistemaBLL().lstGetModulo();

            return View();
        }

        /// <summary>
        ///     Lista dos Registros de Log, filtrados (ou não) pelos critérios
        /// </summary>
        /// <param name="usu_id">Filtrar por Id do Usuário</param>
        /// <param name="data_inicio">Filtrar por Data (a partir de)</param>
        /// <param name="data_fim">Filtrar por Data (até)</param>
        /// <param name="tra_id">Filtrar por Id da Transação (Login, Seleção, Inserção, etc)</param>
        /// <param name="mod_id">Filtrar por Id do Módulo</param>
        /// <param name="texto_procurado">Filtrar por texto digitado</param>
        /// <returns>JsonResult Lista de LogSistema</returns>
        [HttpPost]
        public JsonResult LogSistema_ListAll(string usu_id,  
                                    string data_inicio,
                                    string data_fim,
                                    string tra_id,
                                    string mod_id,
                                    string texto_procurado) 
        {
            var jsonResult = Json(new {    data = new LogSistemaBLL().LogSistema_ListAll( Convert.ToInt32(usu_id),
                                 data_inicio.Trim(),
                                 data_fim.Trim(),
                                 Convert.ToInt32(tra_id),
                                 Convert.ToInt32(mod_id),
                                 texto_procurado.Trim()) }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        /// <summary>
        ///  Insere LogSistema
        /// </summary>
        /// <param name="mod_id">Id do Módulo</param>
        /// <returns>JsonResult LogSistema</returns>
        public ActionResult LogSistema_Salvar(int mod_id)
        {
            int retorno = new LogSistemaBLL().LogSistema_Salvar(mod_id);
            return null;
        }


    }
}