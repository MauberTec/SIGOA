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
using System.Net;
using System.IO;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    /// <summary>
    /// Controlador dos Registros de Acesso e Alterações no Sistema
    /// </summary>
    public class DocumentoController : Controller
    {

        /// <summary>
        /// Busca as Mascaras/Formatos de Documentos na tabela de Parâmetros
        /// </summary>
        /// <returns>JsonResult</returns>
        public JsonResult Documento_GetMascaras()
        {
            return Json(new { data = new DocumentoBLL().Documento_GetMascaras() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Preenchimento do combo Classe de Projeto
        /// </summary>
        /// <param name="tipo">Classe do Documento Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreencheCmbClasseProjeto(string tipo)
        {
            return Json(new DocumentoBLL().CriaListacmbClasseProjeto(tipo), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Faz o upload de arquivos
        /// </summary>
        /// <param name="model">variável de entrada do tipo Arquivo_Upload. O upload só funciona se for com esse nome</param>
        /// <returns>JsonResult</returns>
        public JsonResult Documento_Upload(Arquivo_Upload model)
        {
            return Json(new DocumentoBLL().Documento_Upload(model), JsonRequestBehavior.AllowGet);
        }

        // =============================================================================================================

        /// <summary>
        /// Inicio. Preenchimento do combo de Tipos de Documento
        /// </summary>
        /// <returns>ActionResult View</returns>
        public ActionResult Documento()
        {
            // preenche o combo
            ViewBag.cmbTiposDocumento =  new DocumentoBLL().PreencheCmbDocTipo();
            ViewBag.cmbFiltroTipoDocumento =  new DocumentoBLL().PreencheCmbDocTipo();

            // preenche o combo
            ViewBag.cmbFiltroClasseProjeto =  new DocumentoBLL().CriaListacmbClasseProjeto("");

            // preenche o combo
            ViewBag.cmbFiltroTiposOS = new OrdemServicoBLL().PreencheCmbTiposOS();

            return View();
        }

        /// <summary>
        /// Carrega o grid somente com a página solicitada
        /// </summary>
        /// <param name="doc_id">Filtro por Id de Documento, null para todos</param>
        /// <param name="doc_codigo">Filtro por Código de Documento, vazio para todos</param>
        /// <param name="doc_descricao">Filtro por Descrição de Documento, vazio para todos</param>
        /// <param name="tpd_id">Filtro por Tipo de Documento, vazio para todos</param>
        /// <param name="dcl_codigo">Filtro por Classe de Projeto de Documento, vazio para todos</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult LoadData(int? doc_id, string doc_codigo = "", string doc_descricao = "", string tpd_id = "", string dcl_codigo = "")
        {

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            

            //Find Order Column
           // var order = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var order = Request.Form.GetValues("order[0][column]").FirstOrDefault();

            var orderDir = Request.Form.GetValues("order[0][dir]")[0];

            string Order_BY = "doc_id ASC";
            switch (order)
            {
                case "": Order_BY = "doc_codigo " + orderDir.ToString(); break;
                case "0": Order_BY = "tpd_id " + orderDir.ToString(); break;
                case "1": Order_BY = "doc_id " + orderDir.ToString(); break;
                case "2": Order_BY = "doc_codigo " + orderDir.ToString(); break;
                case "3": Order_BY = "dcl_codigo " + orderDir.ToString(); break;
                case "4": Order_BY = "tpd_descricao " + orderDir.ToString(); break;
                case "5": Order_BY = "doc_descricao " + orderDir.ToString(); break;
                case "6": Order_BY = "doc_caminho " + orderDir.ToString(); break;
            }


            int pageSize = length != null ? Convert.ToInt32(length) : 0;
           // int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;



            List<Documento> listaRetorno = new DocumentoBLL().LoadData(doc_id, doc_codigo, doc_descricao, tpd_id, dcl_codigo, Convert.ToInt32(start), Convert.ToInt16(length), Order_BY);
            if (listaRetorno.Count > 0 )
            {
                recordsTotal = listaRetorno[0].total_registros;
                start = listaRetorno[0].registro_ini.ToString();
             }

            var data = listaRetorno;
            return Json(new { draw = draw, start = start, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
           
        }



        /// <summary>
        /// Lista de todos os Documentos
        /// </summary>
        /// <param name="doc_id">Filtro por Id de Documento, null para todos</param>
        /// <param name="doc_codigo">Filtro por Código de Documento, vazio para todos</param>
        /// <param name="doc_descricao">Filtro por Descrição de Documento, vazio para todos</param>
        /// <param name="tpd_id">Filtro por Tipo de Documento, vazio para todos</param>
        /// <param name="dcl_codigo">Filtro por Classe de Projeto de Documento, vazio para todos</param>
        /// <returns>JsonResult Lista de Grupos</returns>
        public JsonResult Documento_ListAll(int? doc_id, string doc_codigo = "", string doc_descricao = "", string tpd_id = "", string dcl_codigo = "")
        {
            return Json(
                new {
                   // data = new DocumentoBLL().Documento_ListAll(doc_id, doc_codigo, doc_descricao, tpd_id, dcl_codigo)
                    data = new DocumentoBLL().LoadData(doc_id, doc_codigo, doc_descricao, tpd_id, dcl_codigo, 0, 10, "doc_codigo asc")
                }
                , JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Documento selecionado
        /// </summary>
        /// <param name="ID">Id do Documento selecionado</param>
        /// <returns>JsonResult Documento</returns>
        [HttpPost]
        public JsonResult Documento_GetbyID(int ID)
        {
            return Json(new DocumentoBLL().Documento_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Insere ou Altera os dados do Documento 
        /// </summary>
        /// <param name="doc">Dados do Documento</param>
        /// <param name="doc_codigo_filtro">Dados do filtro por Codigo</param>
        /// <param name="doc_descricao_filtro">Dados do filtro por Descrição</param>
        /// <param name="tpd_id_filtro">Dados do filtro por Tipo</param>
        /// <param name="dcl_codigo_filtro">Dados do filtro por Classe</param>
        /// <param name="qt_por_pagina">Quantidade de registros por página</param>
        /// <param name="ordenado_por">Ordenado por</param>
        /// <returns>JsonResult</returns>
        public JsonResult Documento_Salvar(Documento doc, string doc_codigo_filtro = "", string doc_descricao_filtro = "", string tpd_id_filtro = "", string dcl_codigo_filtro = "",
            int qt_por_pagina = 10, string ordenado_por = "")
        {
            return Json(new DocumentoBLL().Documento_Salvar(doc, doc_codigo_filtro, doc_descricao_filtro, tpd_id_filtro, dcl_codigo_filtro,
               qt_por_pagina, ordenado_por), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Ativa/Desativa Documento
        /// </summary>
        /// <param name="id">Id do Documento Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Documento_AtivarDesativar(int id)
        {
            int retorno = new DocumentoBLL().Documento_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Excluir (logicamente) o Documento Selecionado
        /// </summary>
        /// <param name="id">Id do Documento Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Documento_Excluir(int id)
        {
            int retorno = new DocumentoBLL().Documento_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }



        // *************** CLASSE DE DOCUMENTO  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult DocClasse()
        {
            return View();
        }

        /// <summary>
        /// Lista de todas as Classes não deletadas
        /// </summary>
        /// <returns>JsonResult Lista de DocClasses</returns>
        public JsonResult DocClasse_ListAll()
        {
            return Json(new { data = new DocumentoBLL().DocClasse_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados da Classe Selecionada
        /// </summary>
        /// <param name="ID">Id da Classe Selecionada</param>
        /// <returns>JsonResult DocClasse</returns>
        public JsonResult DocClasse_GetbyID(int ID)
        {
            return Json(new DocumentoBLL().DocClasse_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Classe
        /// </summary>
        /// <param name="id">Id da Classe Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult DocClasse_Excluir(int id)
        {
            int retorno = new DocumentoBLL().DocClasse_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Classe
        /// </summary>
        /// <param name="id">Id da Classe Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult DocClasse_AtivarDesativar(int id)
        {
            int retorno = new DocumentoBLL().DocClasse_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados da Classe
        /// </summary>
        /// <param name="dcl">Dados da Classe</param>
        /// <returns>JsonResult</returns>
        public JsonResult DocClasse_Salvar(DocClasse dcl)
        {
            return Json(new DocumentoBLL().DocClasse_Salvar(dcl), JsonRequestBehavior.AllowGet);
        }





        // ************** ASSOCIAR DOCUMENTO A OBJETO **********************************
        /// <summary>
        /// Lista todos os Objetos do Documento selecionado
        /// </summary>
        /// <param name="ID">Id do Documento Selecionado</param>
        /// <returns>JsonResult Lista de Documento_Objeto</returns>
        public JsonResult Documento_Objetos_ListAll(int ID)
        {
            return Json(new { data = new DocumentoBLL().Documento_Objetos_ListAll(ID) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Busca Lista de Objetos 
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="filtro_obj_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtro_obj_descricao">Descrição ou Parte a se localizar</param>
        /// <param name="filtro_clo_id">Id da Classe a se filtrar</param>
        /// <param name="filtro_tip_id">Id do Tipo a se filtrar</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreencheCmbObjetosLocalizados(int doc_id, string filtro_obj_codigo, string filtro_obj_descricao = "", int? filtro_clo_id = -1, int? filtro_tip_id = -1)
        {
            return Json(new DocumentoBLL().PreencheCmbObjetosLocalizados(doc_id, filtro_obj_codigo, filtro_obj_descricao, filtro_clo_id, filtro_tip_id), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     Associa Documento aos Objetos selecionados
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="obj_ids">Ids dos Objetos Selecionados</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Documento_AssociarObjetos(int doc_id, string obj_ids)
        {
            int retorno =new DocumentoBLL().Documento_AssociarObjetos(doc_id, obj_ids);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///    Desassocia Documento do Objeto selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Documento_DesassociarObjeto(int doc_id, int obj_id)
        {
            int retorno =new DocumentoBLL().Documento_DesassociarObjeto(doc_id, obj_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        // ************** ASSOCIAR DOCUMENTO A ORDEM DE SERVICO **********************************
        /// <summary>
        /// Lista todas as OSs do Documento selecionado
        /// </summary>
        /// <param name="ID">Id do Documento Selecionado</param>
        /// <returns>JsonResult Lista de Documento_OrdemServico</returns>
        public JsonResult Documento_OrdemServico_ListAll(int ID)
        {
            return Json(new { data = new DocumentoBLL().Documento_OrdemServico_ListAll(ID) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Busca Lista de OS
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="filtroOrdemServico_codigo">Codigo ou parte da OS a procurar</param>
        /// <param name="filtroObj_codigo">Codigo ou parte do Objeto OS a procurar</param>
        /// <param name="filtroTiposOS">Id do tipo de OS a procurar</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreencheCmbOSsLocalizadas(int doc_id, string filtroOrdemServico_codigo, string filtroObj_codigo, int filtroTiposOS)
        {
            List<OrdemServico> lstOSs = new DocumentoBLL().Documento_OrdemServicoNaoAssociadas_ListAll(doc_id, filtroOrdemServico_codigo, filtroObj_codigo, filtroTiposOS);

            List<SelectListItem> lstListaCmbOSsLocalizadas = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstOSs)
            {
                string txt = temp.ord_codigo + "(" + temp.ord_descricao + ")"; 
                lstListaCmbOSsLocalizadas.Add(new SelectListItem() { Text = txt, Value = temp.ord_id.ToString() });
            }

            return Json(lstListaCmbOSsLocalizadas, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///     Associa OSs ao Documento selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="ord_ids">Ids das OSs Selecionadas</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Documento_AssociarOrdemServico(int doc_id, string ord_ids)
        {
            int retorno =new DocumentoBLL().Documento_AssociarOrdemServico(doc_id, ord_ids);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///    Desassocia Documento da OS selecionada
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="ord_id">Id da OS Selecionada</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Documento_DesassociarOrdemServico(int doc_id, int ord_id)
        {
            int retorno =new DocumentoBLL().Documento_DesassociarOrdemServico(doc_id, ord_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Extrai uma lista de Arquivos e Pastas a partir de um endereço URL de pastas compartilhadas
        /// </summary>
        /// <param name="caminho">Endereço URL</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Documento_ListaArquivosWeb(string caminho)
        {
            // retorna as listas
            return new DocumentoBLL().Documento_ListaArquivosWeb(caminho);
        }

        /// <summary>
        /// Cria nova Pasta no servidor
        /// </summary>
        /// <param name="caminhoVirtual">Caminho virtual da pasta a ser criada</param>
        /// <param name="nomePasta">Nome da Pasta a ser criada em "caminho"</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Documento_NovaPasta(string caminhoVirtual, string nomePasta)
        {
            // retorna as listas
            string retorno =  new Gerais().Criar_NovaPasta(caminhoVirtual, nomePasta);
            return Json(new { erro = retorno }, JsonRequestBehavior.AllowGet);

        }


    }
}