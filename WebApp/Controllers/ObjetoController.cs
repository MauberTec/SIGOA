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
    /// Controlador de Objetos
    /// </summary>
    public class ObjetoController : Controller
    {
        // *************** Objetos  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Objeto()
        {
            // preenche o combos
            List<SelectListItem> lstListacmbClassesObjeto = new ObjetoBLL().PreenchecmbClassesObjeto();
            ViewBag.cmbFiltroClassesObjeto = lstListacmbClassesObjeto;


            List<SelectListItem> lstListacmbTiposObjeto = new ObjetoBLL().CriaListaCmbTiposObjeto(null, null);
            ViewBag.cmbFiltroTiposObjeto = lstListacmbTiposObjeto;

            ViewBag.cmbClassesObjeto = lstListacmbClassesObjeto;

            return View();
        }

        /// <summary>
        /// Lista de todos os Objetos não deletados
        /// </summary>
        /// <param name="obj_id">Codigo de Objeto, 0 para todos</param> 
        /// <param name="filtro_obj_codigo">Filtro por codigo de Objeto, 0 para todos</param> 
        /// <param name="filtro_obj_descricao">Filtro por descrição de Objeto, null para todos</param> 
        /// <param name="filtro_clo_id">Filtro por classe de Objeto, -1 para todos</param> 
        /// <param name="filtro_tip_id">Filtro por tipo de Objeto, -1 para todos</param> 
        /// <returns>JsonResult Lista de Objetos</returns>
        public JsonResult Objeto_ListAll(int obj_id, string filtro_obj_codigo, string filtro_obj_descricao, int filtro_clo_id, int filtro_tip_id)
        {
            return Json(new { data = new ObjetoBLL().Objeto_ListAll(obj_id, filtro_obj_codigo, filtro_obj_descricao, filtro_clo_id, filtro_tip_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Objeto selecionado
        /// </summary>
        /// <param name="ID">Id do Objeto selecionado</param>
        /// <returns>JsonResult Objeto</returns>
        public JsonResult Objeto_GetbyID(int ID)
        {
            return Json(new ObjetoBLL().Objeto_GetbyID(ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Altera os dados do Objeto
        /// </summary>
        /// <param name="obj_id">Id do Objeto</param>
        /// <param name="obj_codigo">Código do Objeto</param>
        /// <param name="obj_descricao">Descrição do Objeto (opcional)</param>
        /// <param name="tip_id">Id do Tipo do Objeto (opcional)</param>
        /// <returns>JsonResult</returns>
        public JsonResult Objeto_Salvar(int obj_id, string obj_codigo, string obj_descricao, int tip_id)
        {
            return Json(new ObjetoBLL().Objeto_Salvar(obj_id, obj_codigo, obj_descricao, tip_id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere Novo Objeto pelo Código
        /// </summary>
        /// <param name="obj_codigo">Código do Objeto</param>
        /// <param name="obj_descricao">Descrição do Objeto</param>
        /// <param name="obj_NumeroObjetoAte">No caso de inserção de item Número Objeto, é possível inserção em lote</param>
        /// <param name="obj_localizacaoAte">No caso de inserção de item Localização, é possível inserção em lote</param>
        /// <returns>JsonResult</returns>
        public JsonResult Objeto_Inserir(string obj_codigo, string obj_descricao, string obj_NumeroObjetoAte, string obj_localizacaoAte)
        {
            return Json(new ObjetoBLL().Objeto_Inserir(obj_codigo, obj_descricao, obj_NumeroObjetoAte, obj_localizacaoAte), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="id">Id do Objeto Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Objeto_AtivarDesativar(int id)
        {
            int retorno = new ObjetoBLL().Objeto_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Exclui Objeto do tipo Subdivisao3 (encontro/ estrutura de terra; encontros/ estrutura de concreto)
        /// </summary>
        /// <param name="tip_id">Id Tipo do Objeto Selecionado</param>
        /// <param name="obj_id_tipoOAE">Id Objeto Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Objeto_Subdivisao3_Excluir(int tip_id, int obj_id_tipoOAE)
        {
            string retorno = new ObjetoBLL().Objeto_Subdivisao3_Excluir(tip_id, obj_id_tipoOAE);
            bool valid = retorno.Trim() == "";
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Objeto selecionado
        /// </summary>
        /// <param name="id">Id do Objeto selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult Objeto_Excluir(int id)
        {
            int retorno = new ObjetoBLL().Objeto_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Lista de todos os Documentos Associados ao Objeto selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <returns>JsonResult Lista de Documentos</returns>
        public JsonResult Objeto_Documentos_ListAll(int obj_id)
        {
            return Json(new { data = new ObjetoBLL().Objeto_Documentos_ListAll(obj_id) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///    Associa Documentos ao Objeto selecionado
        /// </summary>
        /// <param name="doc_ids">Ids dos Documentos Selecionados</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Objeto_AssociarDocumentos(string doc_ids, int obj_id)
        {
            int retorno = new ObjetoBLL().Objeto_AssociarDocumentos(doc_ids, obj_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///    Desassocia Documento do Objeto selecionado
        /// </summary>
        /// <param name="doc_id">Ids do Documento Selecionado</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        [HttpPost]
        public JsonResult Objeto_DesassociarDocumento(int doc_id, int obj_id)
        {
            int retorno = new ObjetoBLL().Objeto_DesassociarDocumento(doc_id, obj_id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Busca Lista de Documentos 
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="codDoc">Código ou Parte a se localizar</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreencheCmbDocumentosLocalizados(int obj_id, string codDoc)
        {
            return Json(new ObjetoBLL().PreencheCmbDocumentosLocalizados(obj_id, codDoc), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///  Preenchimento do combo LocalizacaoObjeto
        /// </summary>
        /// <param name="obj_id_TipoOAE">Id do objeto selecionado</param>
        /// <param name="tip_id_Grupo">Tipo do Grupo do Objeto Selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult PreencheCmbObjetoLocalizacao(int obj_id_TipoOAE, int tip_id_Grupo)
        {
            return Json(new ObjetoBLL().PreencheCmbObjetoLocalizacao(obj_id_TipoOAE, tip_id_Grupo), JsonRequestBehavior.AllowGet);
        }


        // ================= VALORES DE ATRIBUTO DO OBJETO SELECIONADO ===================================================

        /// <summary>
        /// Lista os Atributos do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="atr_id">Id do ATRIBUTO selecionado</param>
        /// <param name="ord_id">Id da O.S. selecionada</param>
        /// <returns>JsonResult Lista de ObjAtributoValores</returns>
        public JsonResult ObjAtributoValores_ListAll(int obj_id, int? atr_id = null, int? ord_id = null)
        {
            //Json(new { data = new ObjetoBLL().ObjAtributoValores_ListAll(obj_id) }, JsonRequestBehavior.AllowGet);

            var jsonResult = Json(new
            {
                data = new ObjetoBLL().ObjAtributoValores_ListAll(obj_id, atr_id, ord_id)
            }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        /// <summary>
        /// Busca os dados do Atributos Fixo do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="atr_id">Id do ATRIBUTO selecionado</param>
        /// <returns>ObjAtributoValores</returns>
        public JsonResult ObjAtributoValores_GetbyID(int obj_id, int atr_id)
        {
            return Json(new { data = new ObjetoBLL().ObjAtributoValores_GetbyID(obj_id, atr_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Salva o Valor dos ATRIBUTOs  no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <param name="codigoOAE">Código da Obra de Arte</param>
        /// <param name="selidTipoOAE">Id do Tipo de Obra de Arte</param>
        /// <param name="ord_id">Id da Ordem de Serviço</param>
        /// <returns>JsonResult</returns>
        public JsonResult ObjAtributoValores_Salvar(ObjAtributoValores ObjAtributoValor, string codigoOAE, int selidTipoOAE, int ord_id)
        {
            return Json(new ObjetoBLL().ObjAtributoValores_Salvar(ObjAtributoValor, codigoOAE, selidTipoOAE, ord_id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Salva valor de somente 1 atributo no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <returns>JsonResult</returns>
        public JsonResult ObjAtributoValor_Salvar(ObjAtributoValores ObjAtributoValor)
        {
            return Json(new ObjetoBLL().ObjAtributoValor_Salvar(ObjAtributoValor), JsonRequestBehavior.AllowGet);
        }



        // *************** CLASSES DE Objeto  *************************************************************

        /// <summary>
        /// Inicio - ObjClasse
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ObjClasse()
        {
            return View();
        }

        /// <summary>
        /// Lista de todas as Classes não deletadas
        /// </summary>
        /// <returns>JsonResult Lista de Classes</returns>
        public JsonResult ObjClasse_ListAll()
        {
            return Json(new { data = new ObjetoBLL().ObjClasse_ListAll() }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados da Classe selecionada
        /// </summary>
        /// <param name="ID">Id da Classe selecionado</param>
        /// <returns>JsonResult ObjClasse</returns>
        public JsonResult ObjClasse_GetbyID(int ID)
        {
            var classe = new ObjetoBLL().ObjClasse_ListAll(ID).FirstOrDefault();
            return Json(classe, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Executa Insere ou Altera os dados da Classe no Banco
        /// </summary>
        /// <param name="objClasse">Dados da Classe</param>
        /// <returns>JsonResult </returns>
        public JsonResult ObjClasse_Salvar(ObjClasse objClasse)
        {
            return Json(new ObjetoBLL().ObjClasse_Salvar(objClasse), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Classe selecionada
        /// </summary>
        /// <param name="id">Id da Classe selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjClasse_Excluir(int id)
        {
            int retorno = new ObjetoBLL().ObjClasse_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Classe
        /// </summary>
        /// <param name="id">Id da Classe Selecionada</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjClasse_AtivarDesativar(int id)
        {
            int retorno = new ObjetoBLL().ObjClasse_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }



        // *************** TIPOS DE Objeto  *************************************************************

        /// <summary>
        /// Lista de todos os Tipos não deletados da Classe selecionada
        /// </summary>
        /// <param name="clo_id">Id da Classe selecionada</param>
        /// <returns>JsonResult Lista de Tipos</returns>
        public JsonResult ObjTipo_ListAll(int clo_id)
        {
            return Json(new { data = new ObjetoBLL().ObjTipo_ListAll(clo_id, null) }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// lista concatenada dos pais de tipos de objeto por classe
        /// </summary>
        /// <param name="clo_id">Classe do Objeto selecionado</param>
        /// <returns>JsonResult</returns>
        public JsonResult lstTipos_da_Classe(int clo_id)
        {
            return Json(new { data = new ObjetoBLL().lstTipos_da_Classe(clo_id) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Tipo selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo selecionado</param>
        /// <param name="clo_id">Id da Classe do selecionado</param>
        /// <returns>JsonResult ObjTipo</returns>
        public JsonResult ObjTipo_GetbyID(int ID, int clo_id)
        {
            return Json(new ObjetoBLL().ObjTipo_GetbyID(clo_id, ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Executa Insere ou Altera os dados do Tipo no Banco
        /// </summary>
        /// <param name="objTipo">Dados do Tipo</param>
        /// <returns>JsonResult</returns>
        public JsonResult ObjTipo_Salvar(ObjTipo objTipo)
        {
            return Json(new ObjetoBLL().ObjTipo_Salvar(objTipo), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo selecionado
        /// </summary>
        /// <param name="id">Id do Tipo selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjTipo_Excluir(int id)
        {
            int retorno = new ObjetoBLL().ObjTipo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="id">Id do Tipo Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjTipo_AtivarDesativar(int id)
        {
            Usuario paramUsuario = (Usuario)Session["Usuario"];
            int retorno = new ObjetoBLL().ObjTipo_AtivarDesativar(id, paramUsuario.usu_id, paramUsuario.usu_ip);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        // *************** ATRIBUTOS DE OBJETO  *************************************************************

        /// <summary>
        /// Inicio - ObjAtributo
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ObjAtributo()
        {
            // preenche o combos
            List<SelectListItem> lstListacmbClassesObjeto = new ObjetoBLL().PreenchecmbClassesObjeto();
            ViewBag.cmbClassesObjeto = lstListacmbClassesObjeto;
            ViewBag.cmbFiltroClassesObjeto = lstListacmbClassesObjeto;

            return View();
        }

        /// <summary>
        /// Lista de todos os Atributos não deletados
        /// </summary>
        /// <param name="atr_id">Filtro por Id de Atributo</param>
        /// <param name="filtro_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtro_descricao">Descrição ou Parte a se localizar</param>
        /// <param name="filtro_clo_id">Id da Classe a se filtrar</param>
        /// <param name="filtro_tip_id">Id do Tipo a se filtrar</param>
        /// <param name="ehAtributoFuncional">Flag de atributo funcional</param>
        /// <returns>JsonResult Lista de ATRIBUTOS</returns>
        public JsonResult ObjAtributo_ListAll(int atr_id, string filtro_codigo, string filtro_descricao, int filtro_clo_id, int filtro_tip_id, int ehAtributoFuncional)
        {
            return Json(new { data = new ObjetoBLL().ObjAtributo_ListAll(atr_id, filtro_codigo, filtro_descricao, filtro_clo_id, filtro_tip_id, ehAtributoFuncional) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do ATRIBUTO selecionado
        /// </summary>
        /// <param name="id">Id do Atributo selecionado</param>
        /// <param name="ehAtributoFuncional">Flag de atributo funcional</param>
        /// <returns>JsonResult ObjAtributo</returns>
        public JsonResult ObjAtributo_GetbyID(int id, int ehAtributoFuncional)
        {
            return Json(new ObjetoBLL().ObjAtributo_GetbyID(id, ehAtributoFuncional), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Preenchimento do combo Tipos de Objeto
        /// </summary>
        /// <param name="clo_id">Tipo do Objeto Selecionado</param>
        /// <param name="tip_pai">Id do Tipo Pai</param>
        /// <param name="excluir_existentes">Menos os valores já existentes</param>
        /// <param name="obj_id">Id do objeto selecionado</param>
        /// <param name="somente_com_variaveis_inspecao">Somente se possuir variaveis inspecao</param>
        /// /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult PreencheCmbTiposObjeto(int clo_id, int? tip_pai = 0, int? excluir_existentes = 0, int? obj_id = 0, int? somente_com_variaveis_inspecao = 0)
        {
            return Json(new ObjetoBLL().CriaListaCmbTiposObjeto(clo_id, tip_pai, excluir_existentes, obj_id, somente_com_variaveis_inspecao), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Executa Insere ou Altera os dados do ATRIBUTO no Banco
        /// </summary>
        /// <param name="objAtrFixo">Dados do ATRIBUTO</param>
        /// <returns>JsonResult</returns>
        public JsonResult ObjAtributo_Salvar(ObjAtributo objAtrFixo)
        {
            return Json(new ObjetoBLL().ObjAtributo_Salvar(objAtrFixo), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) ATRIBUTO selecionado
        /// </summary>
        /// <param name="id">Id do ATRIBUTO selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjAtributo_Excluir(int id)
        {
            int retorno = new ObjetoBLL().ObjAtributo_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa ATRIBUTO
        /// </summary>
        /// <param name="id">Id do ATRIBUTO Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjAtributo_AtivarDesativar(int id)
        {
            int retorno = new ObjetoBLL().ObjAtributo_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }



        // *************** ITENS DE ATRIBUTO DE Objeto  *************************************************************

        /// <summary>
        /// Lista de todos os Itens não deletados do Atributos Fixo selecionado
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO</param>
        /// <returns>JsonResult Lista de Itens ATRIBUTOS</returns>
        public JsonResult ObjAtributoItem_ListAll(int atr_id)
        {
            return Json(new { data = new ObjetoBLL().ObjAtributoItem_ListAll(atr_id, null) }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Dados do Item de ATRIBUTO selecionado
        /// </summary>
        /// <param name="ID">Id do Item do ATRIBUTO selecionado</param>
        /// <param name="atr_id">Id do ATRIBUTO selecionado</param>
        /// <returns>JsonResult ObjAtributoItem</returns>
        public JsonResult ObjAtributoItem_GetbyID(int ID, int atr_id)
        {
            return Json(new ObjetoBLL().ObjAtributoItem_GetbyID(atr_id, ID), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Executa Insere ou Altera os dados do Item do ATRIBUTO no Banco
        /// </summary>
        /// <param name="objAtrFixoItem">Dados do Item do ATRIBUTO</param>
        /// <returns>JsonResult</returns>
        public JsonResult ObjAtributoItem_Salvar(ObjAtributoItem objAtrFixoItem)
        {
            Usuario paramUsuario = (Usuario)Session["Usuario"];
            return Json(new ObjetoBLL().ObjAtributoItem_Salvar(objAtrFixoItem), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Excluir (logicamente) Item do ATRIBUTO selecionado
        /// </summary>
        /// <param name="id">Id do Item do ATRIBUTO selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjAtributoItem_Excluir(int id)
        {
            int retorno = new ObjetoBLL().ObjAtributoItem_Excluir(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Ativa/Desativa Item do ATRIBUTO
        /// </summary>
        /// <param name="id">Id do Item do ATRIBUTO Selecionado</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult ObjAtributoItem_AtivarDesativar(int id)
        {
            int retorno = new ObjetoBLL().ObjAtributoItem_AtivarDesativar(id);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }


        // *************** GRUPOS / VARIÁVEIS / VALORES DE INSPEÇÃO  *************************************************************
        /// <summary>
        /// Lista Grupos/Variáveis do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <param name="ehProvidencia">Flag para tela Providências</param>
        /// <param name="filtro_prt_id">Filtro id da Providência</param>
        /// <returns>JsonResult Lista de GruposVariaveisValores</returns>
        public JsonResult GruposVariaveisValores_ListAll(int obj_id, int? ord_id = -1, int? ehProvidencia = 0, int? filtro_prt_id = 0)
        {
            var jsonResult =  Json(new { data = new ObjetoBLL().GruposVariaveisValores_ListAll(obj_id, ord_id, ehProvidencia, filtro_prt_id) }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        /// <summary>
        ///    Cria/Associa Grupos ao Objeto selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="grupos_codigos">Codigos dos grupos a serem salvos</param>
        /// <returns>JsonResult</returns>
        [HttpPost]
        public JsonResult AssociarGruposVariaveis(int obj_id, string grupos_codigos)
        {
            int retorno = new ObjetoBLL().AssociarGruposVariaveis(obj_id, grupos_codigos);
            bool valid = retorno >= 0;
            return Json(new { status = valid, erroId = retorno }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  Insere ou Altera os dados das Variaveis do Grupo
        /// </summary>
        /// <param name="obj_id_tipoOAE">Id do objeto Tipo OAE</param>
        /// <param name="Ponto1">Espessura do Pavimento Ponto1</param>
        /// <param name="Ponto2">Espessura do Pavimento Ponto2</param>
        /// <param name="Ponto3">Espessura do Pavimento Ponto3</param>
        /// <param name="OutrasInformacoes">Outras Informações</param>
        /// <param name="listaConcatenada">Dados das Variaveis do Grupo</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <returns>JsonResult</returns>
        public JsonResult GruposVariaveisValores_Salvar(int obj_id_tipoOAE, string Ponto1, string Ponto2, string Ponto3, string OutrasInformacoes, string listaConcatenada, int? ord_id = -1)
        {
            return Json(new ObjetoBLL().GruposVariaveisValores_Salvar(obj_id_tipoOAE, Ponto1, Ponto2, Ponto3, OutrasInformacoes, listaConcatenada, ord_id), JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// Faz o upload de arquivos
        /// </summary>
        /// <param name="model">variável de entrada do tipo Arquivo_Upload. O upload só funciona se for com esse nome</param>
        /// <returns>JsonResult</returns>
        public JsonResult EsquemaEstrutural_Upload(ObjAtributoValores model)
        {
            var jsonResult = Json(new
            {
                data = new ObjetoBLL().EsquemaEstrutural_Upload(model)
            }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }



        // *************** Exportar XLS  *************************************************************
        /// <summary>
        ///    Cria Ficha Inspecao Rotineira Exportada para XLS
        /// </summary>
        /// <param name="obj_id">Id do objeto da ficha</param>
        /// <param name="ord_id">Id da O.S pertinente ao objeto</param>
        /// <returns>JsonResult caminho do arquivo</returns>
        public JsonResult ObjFichaInspecaoRotineira_ExportarXLS(int obj_id, int ord_id)
        {
            return Json(new { data = new ObjetoBLL().ObjFichaInspecaoRotineira_ExportarXLS(obj_id, ord_id) }, JsonRequestBehavior.AllowGet);
        }



        // *************** Objetos  *************************************************************
        /// <summary>
        /// Inicio
        /// </summary>
        /// <returns>View</returns>
        public ActionResult ObjPriorizacao()
        {
            Usuario paramUsuario = (Usuario)Session["Usuario"];
            ListaMenus_Lateral(ref paramUsuario);
            // preenche o combos
            List<SelectListItem> lstListacmbFiltroRegionais = new ObjetoBLL().PreenchecmbFiltroRegionais();
            ViewBag.cmbFiltroRegionais = lstListacmbFiltroRegionais;

            return View();
        }
        /// <summary>
        /// Montagem do Menu Lateral segundo os privilégios do Usuário logado
        /// </summary>
        /// <param name="paramUsuario">Usuario</param>
        public void ListaMenus_Lateral(ref Usuario paramUsuario)
        {  // preenche paramUsuario.lstMenus
             new HomeController().ListaMenus_Lateral(ref paramUsuario);
        }
        /// <summary>
        /// Todos os controles do menu apontam para este metodo ("../Home/Menu_Click?caminho=") pois o evento pode vir de origens diferentes, e chama a View 
        /// </summary>
        /// <param name="caminho">caminho do Controlador/Valor extraido do valor enviado pelo click do menu</param>
        /// <param name="id">idModulo extraido do valor enviado pelo click do menu</param>
        /// <returns>ActionResult</returns>
        public ActionResult Menu_Click(string caminho, string id)
        {
            // loga o acesso da pagina
            if (System.Web.HttpContext.Current.Session["Usuario"] != null)
            {
                Usuario paramUsuario = (Usuario)Session["Usuario"];
                int retorno = new LogSistemaBLL().LogSistema_Inserir(3, // 3 = select
                                                     paramUsuario.usu_id.ToString(),
                                                     Convert.ToInt32(id), // idModulo
                                                     "",
                                                     paramUsuario.usu_ip);

                // vai para a pagina requisitada 
                return AbreMenu(caminho + "&id=" + id);
            }
            else
            {
                //loga a saida
                string usu_ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                int retorno = new LogSistemaBLL().LogSistema_Inserir(12, // 12 = timeout
                                            "-1",
                                            Convert.ToInt32(id),
                                            "Session Timeout",
                                            usu_ip);

                return AbreMenu("/Login/Sair");
            }
        }

        /// <summary>
        /// Redireciona a View corrente para o novo caminho
        /// </summary>
        /// <param name="caminho">caminho do Controlador/Valor a ser redirecionado</param>
        /// <returns>ActionResult</returns>
        private ActionResult AbreMenu(string caminho)
        {
            caminho = caminho.Replace("&id=", "/");
            string[] partes = caminho.Split("/".ToCharArray());
            if (partes.Length > 3)
                return RedirectToAction(partes[2], partes[1], new { @id = partes[3] }); // RedirectToAction(ActionName,  ControllerName)
            else
                return RedirectToAction(partes[2], partes[1]);

        }

        /// <summary>
        /// Lista de Objetos Priorizados
        /// </summary>
        /// <param name="CodRodovia">Filtro por Codigo da Rodovia</param>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovia</param>
        /// <param name="FiltroidRegionais">Filtro por id de  Regional</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objeto</param>
        /// <param name="Filtro_data_De">Filtro por Data Inicial</param>
        /// <param name="Filtro_data_Ate">Filtro por Data final</param>
        /// <param name="somenteINSP_ESPECIAIS">Filtro por Inspecao Especial</param>
        /// <returns>JsonResult</returns>
        public JsonResult ObjPriorizacao_ListAll(string CodRodovia,
            string FiltroidRodovias = "", string FiltroidRegionais = "", string FiltroidObjetos = "", string Filtro_data_De = "", string Filtro_data_Ate = "",
            int? somenteINSP_ESPECIAIS = 0)
        {
            return Json(new { data = new ObjetoBLL().ObjPriorizacao_ListAll(CodRodovia, FiltroidRodovias, FiltroidRegionais, FiltroidObjetos, Filtro_data_De, Filtro_data_Ate, somenteINSP_ESPECIAIS) }, JsonRequestBehavior.AllowGet);
        }


    }
}