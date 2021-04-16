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
    /// Controlador OrdemServico
    /// </summary>
    public class OrdemServicoController : Controller
    {
        /// <summary>
        /// Carrega os codigos da ficha dinamicamente
        /// </summary>
        /// <param name="qualFicha">Qual ficha a carregar</param>
        /// <returns>PartialViewResult</returns>
        public PartialViewResult CarregaFicha(int qualFicha)
        {
            string ficha = "";
            switch (qualFicha)
            {
                case 1: ficha = "~/Views/Shared/_fichaInspecaoCadastral.cshtml"; break;
                case 2: ficha = "~/Views/Shared/_fichaInspecaoRotineira.cshtml"; break;
                case 3: ficha = "~/Views/Shared/_fichaInspecaoRotineira.cshtml";break;
                case 4: ficha = "~/Views/Shared/_fichaInspecaoEspecial.cshtml";  break;
                case 5: ficha = "~/Views/Shared/_fichaInspecaoEspecialCampo.cshtml";  break;
                case 6: ficha = "~/Views/Shared/_fichaNotificacaoOcorrencia.cshtml";  break;
                case 7: ficha = "~/Views/Shared/_fichaInspecaoEspecialProvidencias.cshtml";  break;
                case 8: ficha = "~/Views/Shared/_fichaInspecaoRotineiraProvidencias.cshtml";  break;
            }

                return PartialView(ficha);
        }


        // *************** Ordem de Servico  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult OrdemServico()
        {
            // preenche o combo
            ViewBag.cmbFiltroTiposOS = new OrdemServicoBLL().PreencheCmbTiposOS();
            ViewBag.cmbTiposOS = new OrdemServicoBLL().PreencheCmbTiposOS();
            ViewBag.cmbTiposOS_Novo = new OrdemServicoBLL().PreencheCmbTiposOS();
            

            ViewBag.cmbFiltroStatusOS = new OrdemServicoBLL().PreencheCmbStatusOS();
            ViewBag.cmbStatusOS = new OrdemServicoBLL().PreencheCmbStatusOS();

            ViewBag.cmbClassesOS = new OrdemServicoBLL().PreencheCmbClassesOS();

            ViewBag.cmbEmailRegionais = new ObjetoBLL().PreenchecmbEmailRegionais();

            return View();
        }

        /// <summary>
        /// Lista de todas as Ordens de Servicos não deletadas
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico a se filtrar</param>
        /// <param name="filtroOrdemServico_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroObj_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroTiposOS">Id do Tipo a se filtrar</param>
        /// <param name="filtroStatusOS">Id do Status a se filtrar</param>
        /// <param name="filtroData">Filtro pelo tipo de Data Selecionado</param>
        /// <param name="filtroord_data_De">Filtro por Data: a de</param>
        /// <param name="filtroord_data_Ate">Filtro por Data: até</param>
        /// <returns>JsonResult Lista de OrdemServicos</returns>
        public JsonResult OrdemServico_ListAll(int? ord_id = null, string filtroOrdemServico_codigo = null, string filtroObj_codigo = null, int? filtroTiposOS = -1, int? filtroStatusOS = -1, string filtroData = "", string filtroord_data_De = "", string filtroord_data_Ate = "")
        {
            return Json(new { data = new OrdemServicoBLL().OrdemServico_ListAll(ord_id, filtroOrdemServico_codigo, filtroObj_codigo, filtroTiposOS, filtroStatusOS, filtroData, filtroord_data_De, filtroord_data_Ate) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados da Ordem de Servico selecionada
        /// </summary>
        /// <param name="ID">Id da Ordem de Servico selecionada</param>
        /// <returns>JsonResult OrdemServico</returns>
        public JsonResult OrdemServico_GetbyID(int ID)
        {
            return Json(new OrdemServicoBLL().OrdemServico_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) OrdemServico
        /// </summary>
        /// <param name="id">Id da Ordem de Servico Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrdemServico_Excluir(int id)
        {
            int retorno = new OrdemServicoBLL().OrdemServico_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa OrdemServico
        /// </summary>
        /// <param name="id">Id da Ordem de Servico Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrdemServico_AtivarDesativar(int id)
        {
            int retorno = new OrdemServicoBLL().OrdemServico_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Altera os dados da Ordem de Servico
        /// </summary>
        /// <param name="ord">Dados da Ordem de Servico</param>
        /// <returns>JsonResult</returns>
        public JsonResult OrdemServico_Salvar(OrdemServico ord)
        {
            return Json(new OrdemServicoBLL().OrdemServico_Salvar(ord), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere os dados da Ordem de Servico
        /// </summary>
        /// <param name="ord">Dados da Ordem de Servico</param>
        /// <returns>JsonResult</returns>
        public JsonResult OrdemServico_Inserir_Novo(OrdemServico ord)
        {
            return Json(new OrdemServicoBLL().OrdemServico_Inserir_Novo(ord), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///   Busca o proximo sequencial do codigo para o tipo especificado
        /// </summary>
        /// <param name="tos_id">Id do tipo selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult OrdemServico_ProximoCodigo(int tos_id)
        {
            return Json(new { data = new OrdemServicoBLL().OrdemServico_ProximoCodigo(tos_id) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lista de todos os Documentos Associados à Ordem de Servico selecionada
        /// </summary>
        /// <param name="ord_id">Id do OrdemServico selecionado</param>
        /// <param name="obj_id">Id do Objeto da OrdemServico selecionada</param>
        /// <param name="somente_referencia">Retornar somente os documentos de referência?</param>
        /// <returns>JsonResult Lista de Documentos</returns>
        public JsonResult OrdemServico_Documentos_ListAll(int ord_id, int obj_id, int? somente_referencia = 0)
        {
            return Json(new { data = new OrdemServicoBLL().OrdemServico_Documentos_ListAll(ord_id, obj_id, somente_referencia) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista de todos os Documentos Associados ao Objeto da Ordem de Servico selecionada
        /// </summary>
        /// <param name="ord_id">Id do OrdemServico selecionado</param>
        /// <param name="obj_id">Id do Objeto da OrdemServico selecionada</param>
        /// <returns>JsonResult Lista de Documentos</returns>
        public JsonResult OrdemServico_Objeto_Documentos_ListAll(int ord_id)
        {
            return Json(new { data = new OrdemServicoBLL().OrdemServico_Objeto_Documentos_ListAll(ord_id) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///    Associa Documentos de Referência à Ordem de Serviço selecionada
        /// </summary>
        /// <param name="doc_id">Ids do Documentos selecionado</param>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <param name="dos_referencia">Zero para associar; Um para desassociar</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult OrdemServico_AssociarDocumentosReferencia(int doc_id, int ord_id, int dos_referencia)
        {
            int retorno = new OrdemServicoBLL().OrdemServico_AssociarDocumentosReferencia(doc_id, ord_id, dos_referencia);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///    Associa Documentos à Ordem de Servico selecionada
        /// </summary>
        /// <param name="doc_ids">Ids dos Documentos Selecionados</param>
        /// <param name="ord_id">Id do OrdemServico Selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult OrdemServico_AssociarDocumentos(string doc_ids, int ord_id)
        {
            int retorno = new OrdemServicoBLL().OrdemServico_AssociarDocumentos(doc_ids, ord_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///    Desassocia Documento da OrdemServico selecionada
        /// </summary>
        /// <param name="doc_id">Ids do Documento Selecionado</param>
        /// <param name="ord_id">Id do OrdemServico Selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult OrdemServico_DesassociarDocumento(int doc_id, int ord_id)
        {
            int retorno = new OrdemServicoBLL().OrdemServico_DesassociarDocumento(doc_id, ord_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Busca Lista de Documentos 
        /// </summary>
        /// <param name="ord_id">Id do OrdemServico Selecionado</param>
        /// <param name="codDoc">Código ou Parte a se localizar</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreencheCmbDocumentosLocalizados(int ord_id, string codDoc)
        {
            return Json(new OrdemServicoBLL().PreencheCmbDocumentosLocalizados(ord_id, codDoc), JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        ///    Busca o valor do campo ord_indicacao_servico
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico</param>
        /// <returns>JsonResult</returns>
        public JsonResult OrdemServico_Indicacao_Servico_ListAll(int ord_id)
        {
            return Json(new { data = new OrdemServicoBLL().OrdemServico_Indicacao_Servico_ListAll(ord_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///    Altera os dados da Aba Indicacao de Servico no Banco
        /// </summary>
        /// <param name="ord_id">Id do OrdemServico Selecionado</param>
        /// <param name="ord_indicacao_servico">Texto do campo Indicaçao de serviço</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OrdemServico_Indicacao_Servico_Salvar(int ord_id, string ord_indicacao_servico)
        {
            return Json(new OrdemServicoBLL().OrdemServico_Indicacao_Servico_Salvar(ord_id, ord_indicacao_servico), JsonRequestBehavior.AllowGet);
        }


        // *************** TIPO DE Ordem de Servico  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult OSTipo()
        {
            return View();
        }

        /// <summary>
        /// Lista de todos os Tipos de Ordens de Servicos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de OSTipos</returns>
        public JsonResult OSTipo_ListAll()
        {
            return Json(new { data = new OrdemServicoBLL().OSTipo_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados  do TIPO de Ordem de Servico selecionado
        /// </summary>
        /// <param name="ID">Id do TIPO de Ordem de Servico selecionado</param>
        /// <returns>JsonResult OSTipo</returns>
        public JsonResult OSTipo_GetbyID(int ID)
        {
            return Json(new OrdemServicoBLL().OSTipo_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) OSTipo
        /// </summary>
        /// <param name="id">Id do TIPO de Ordem de Servico Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OSTipo_Excluir(int id)
        {
            int retorno = new OrdemServicoBLL().OSTipo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa OSTipo
        /// </summary>
        /// <param name="id">Id do TIPO de Ordem de Servico Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OSTipo_AtivarDesativar(int id)
        {
            int retorno = new OrdemServicoBLL().OSTipo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do TIPO da Ordem de Servico
        /// </summary>
        /// <param name="tos">Dados do TIPO da Ordem de Servico</param>
        /// <returns>JsonResult</returns>
        public JsonResult OSTipo_Salvar(OSTipo tos)
        {
            return Json(new OrdemServicoBLL().OSTipo_Salvar(tos), JsonRequestBehavior.AllowGet);
        }


        // *************** STATUS DE Ordem de Servico  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult OSStatus()
        {
            return View();
        }

        /// <summary>
        /// Lista de todas os Status de Ordens de Servicos não deletados
        /// </summary>
        /// <returns>JsonResult Lista de OSStatuss</returns>
        public JsonResult OSStatus_ListAll()
        {
            return Json(new { data = new OrdemServicoBLL().OSStatus_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados  do Status de Ordem de Servico selecionado
        /// </summary>
        /// <param name="ID">Id do Status de Ordem de Servico selecionado</param>
        /// <returns>JsonResult OSStatus</returns>
        public JsonResult OSStatus_GetbyID(int ID)
        {
            return Json(new OrdemServicoBLL().OSStatus_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) OSStatus
        /// </summary>
        /// <param name="id">Id do Status de Ordem de Servico Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OSStatus_Excluir(int id)
        {
            int retorno = new OrdemServicoBLL().OSStatus_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa OSStatus
        /// </summary>
        /// <param name="id">Id do Status de Ordem de Servico Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OSStatus_AtivarDesativar(int id)
        {
            int retorno = new OrdemServicoBLL().OSStatus_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status da Ordem de Servico
        /// </summary>
        /// <param name="sos">Dados do Status da Ordem de Servico</param>
        /// <returns>JsonResult</returns>
        public JsonResult OSStatus_Salvar(OSStatus sos)
        {
            return Json(new OrdemServicoBLL().OSStatus_Salvar(sos), JsonRequestBehavior.AllowGet);
        }


        // *************** FLUXO DE STATUS DE Ordem de Servico  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult OSFluxoStatus()
        {
            // preenche combos
            ViewBag.cmbStatusDe = new OrdemServicoBLL().preencheCmbStatus();
            ViewBag.cmbStatusPara = new OrdemServicoBLL().preencheCmbStatus();

            return View();
        }

        /// <summary>
        /// Lista de todas os Fluxos de Status de Ordens de Servicos não deletados
        /// </summary>
        /// <param name="tos_id">Id do Tipo de Ordem de Servico</param>
        /// <returns>JsonResult Lista de OSFluxoStatuss</returns>
        public JsonResult OSFluxoStatus_ListAll(int tos_id)
        {
            return Json(new { data = new OrdemServicoBLL().OSFluxoStatus_ListAll(tos_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados  do Fluxo de Status de Ordem de Servico selecionado
        /// </summary>
        /// <param name="ID">Id do Fluxo de Status de Ordem de Servico selecionado</param>
        /// <returns>JsonResult OSFluxoStatus</returns>
        public JsonResult OSFluxoStatus_GetbyID(int ID)
        {
            return Json(new OrdemServicoBLL().OSFluxoStatus_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) FluxoStatus
        /// </summary>
        /// <param name="id">Id do Fluxo de Status de Ordem de Servico Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OSFluxoStatus_Excluir(int id)
        {
            int retorno = new OrdemServicoBLL().OSFluxoStatus_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Fluxo de Status
        /// </summary>
        /// <param name="id">Id do Fluxo de Status de Ordem de Servico Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult OSFluxoStatus_AtivarDesativar(int id)
        {
            int retorno = new OrdemServicoBLL().OSFluxoStatus_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status da Ordem de Servico
        /// </summary>
        /// <param name="fos">Dados do Fluxo de Status da Ordem de Servico</param>
        /// <returns>JsonResult</returns>
        public JsonResult OSFluxoStatus_Salvar(OSFluxoStatus fos)
        {
            return Json(new OrdemServicoBLL().OSFluxoStatus_Salvar(fos), JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        ///  Envia Email de Notificacao
        /// </summary>
        /// <param name="lstDestinatarios">Lista de Destinatarios separada por ponto e virgula</param>
        /// <param name="TextoEmail">Texto do Email</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult FichaNotificacao_EnviarEmail(string lstDestinatarios, string TextoEmail)
        {
            string retorno = new OrdemServicoBLL().FichaNotificacao_EnviarEmail(lstDestinatarios, TextoEmail);
            bool valid = retorno.Trim() == "" ? true : false;

            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);

        }



    }
}