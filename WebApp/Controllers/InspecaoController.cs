﻿using System;
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
    /// Controlador de InspecaoLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class InspecaoController : Controller
    {
        // *************** INSPECOES  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Inspecao()
        {            // preenche o combo
            ViewBag.cmbFiltroTiposOS = new OrdemServicoBLL().PreencheCmbTiposOS();
            ViewBag.cmbTiposOS = new OrdemServicoBLL().PreencheCmbTiposOS();
            ViewBag.cmbTiposOS_Novo = new OrdemServicoBLL().PreencheCmbTiposOS();


            ViewBag.cmbFiltroStatusOS = new OrdemServicoBLL().PreencheCmbStatusOS();
            ViewBag.cmbStatusOS = new OrdemServicoBLL().PreencheCmbStatusOS();

            ViewBag.cmbClassesOS = new OrdemServicoBLL().PreencheCmbClassesOS();


            return View();
        }


        /// <summary>
        /// Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="ins_id">Id da inspeção selecionada</param>
        /// <returns>JsonResult Lista de Inspecao</returns>
        public JsonResult Inspecao_ListAll(int ins_id)
        {
            return Json(new { data = new InspecaoBLL().Inspecao_ListAll(ins_id) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lista os Atributos do Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>JsonResult Lista de ObjAtributoValores</returns>
        public JsonResult InspecaoAtributosValores_ListAll(int ord_id)
        {
            var jsonResult = Json(new
            {
                data = new InspecaoBLL().InspecaoAtributosValores_ListAll(ord_id)
            }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }


        /// <summary>
        ///  Salva os Valores dos Atributos  no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <param name="codigoOAE">Código da Obra de Arte</param>
        /// <param name="selidTipoOAE">Id do Tipo de Obra de Arte</param>
        /// <param name="ord_id">Id da Ordem de Serviço</param>
        /// <returns>JsonResult</returns>
        public JsonResult InspecaoAtributoValores_Salvar(ObjAtributoValores ObjAtributoValor, string codigoOAE, int selidTipoOAE, int ord_id)
        {
            return Json(new InspecaoBLL().InspecaoAtributoValores_Salvar(ObjAtributoValor, codigoOAE, selidTipoOAE, ord_id), JsonRequestBehavior.AllowGet);
        }


        //********************************************************************************************************


        /// <summary>
        /// Lista das anomalias encontradas no Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>JsonResult</returns>
        public JsonResult InspecaoAnomalias_Valores_ListAll(int ord_id)
        {
            return Json(new { data = new InspecaoBLL().InspecaoAnomalias_Valores_ListAll(ord_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Salva os valores das anomalias
        /// </summary>
        /// <param name="ord_id">Id da O.S. da inspeção Especial</param>
        /// <param name="ins_anom_Responsavel">Responsavel pela Inspeção</param>
        /// <param name="ins_anom_data">Data da Inspeção</param>
        /// <param name="ins_anom_quadroA_1">Resposta do Quadro A (Sim/Não)</param>
        /// <param name="ins_anom_quadroA_2">Resposta do Quadro A (itens)</param>
        /// <param name="listaConcatenada">Lista dos valores das anomalias</param>
        /// <returns>JsonResult</returns>
        public JsonResult InspecaoAnomalias_Valores_Salvar(int ord_id, string ins_anom_Responsavel, string ins_anom_data, string ins_anom_quadroA_1, string ins_anom_quadroA_2, string listaConcatenada)
        {
            return Json(new InspecaoBLL().InspecaoAnomalias_Valores_Salvar(ord_id, ins_anom_Responsavel, ins_anom_data, ins_anom_quadroA_1, ins_anom_quadroA_2, listaConcatenada), JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        ///  Insere Objetos a serem inspecionados
        /// </summary>
        /// <param name="ord_id">Id da O.S. dessa inspeção</param>
        /// <param name="obj_ids">Lista dos Ids dos Objetos a serem inspecionados</param>
        /// <returns>JsonResult</returns>
        public JsonResult InspecaoAnomaliaObjetos_Salvar(int ord_id, string obj_ids)
        {
            return Json(new InspecaoBLL().InspecaoAnomaliaObjetos_Salvar(ord_id, obj_ids), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///  Excluir (logicamente) Objeto da inspecao
        /// </summary>
        /// <param name="id">Id da linha da tabela inspecao_anomalias</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult InspecaoAnomaliaObjetos_Excluir(int id)
        {
            int retorno = new InspecaoBLL().InspecaoAnomaliaObjetos_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// lista concatenada dos tipos de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo"></param>
        /// <returns>JsonResult</returns>
        public JsonResult InspecaoAnomaliaTipos_by_Legenda(string leg_codigo)
        {
            return Json(new { data = new InspecaoBLL().InspecaoAnomaliaTipos_by_Legenda(leg_codigo) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// lista concatenada das causas de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo"></param>
        /// <returns>JsonResult</returns>
        public JsonResult InspecaoAnomaliaCausas_by_Legenda(string leg_codigo)
        {
            return Json(new { data = new InspecaoBLL().InspecaoAnomaliaCausas_by_Legenda(leg_codigo) }, JsonRequestBehavior.AllowGet);
        }




        // *************** Exportar XLS  *************************************************************
        /// <summary>
        ///    Cria Ficha Inspecao Especial Exportada para XLS
        /// </summary>
        /// <param name="ord_id">Id da O.S pertinente ao objeto</param>
        /// <returns>JsonResult caminho do arquivo</returns>
        public JsonResult FichaInspecaoEspecialAnomalias_ExportarXLS(int ord_id)
        {
            return Json(new { data = new InspecaoBLL().FichaInspecaoEspecialAnomalias_ExportarXLS(ord_id) }, JsonRequestBehavior.AllowGet);
        }




        // *************** TIPO  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult InspecaoTipo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Tipos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de InspecaoTipos</returns>
        public JsonResult InspecaoTipo_ListAll()
        {
            return Json(new { data = new InspecaoBLL().InspecaoTipo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Tipo Selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo Selecionado</param>
        /// <returns>JsonResult InspecaoTipo</returns>
        public JsonResult InspecaoTipo_GetbyID(int ID)
        {
            return Json(new InspecaoBLL().InspecaoTipo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult InspecaoTipo_Excluir(int id)
        {
            int retorno = new InspecaoBLL().InspecaoTipo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult InspecaoTipo_AtivarDesativar(int id)
        {
            int retorno = new InspecaoBLL().InspecaoTipo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="atp">Dados do Tipo</param>
        /// <returns>JsonResult</returns>
        public JsonResult InspecaoTipo_Salvar(InspecaoTipo atp)
        {
            return Json(new InspecaoBLL().InspecaoTipo_Salvar(atp), JsonRequestBehavior.AllowGet);
        }





    }
}