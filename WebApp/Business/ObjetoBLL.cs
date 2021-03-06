﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using DocumentFormat.OpenXml;
using System.Drawing;
using WebApp.Helpers;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;
using Font = DocumentFormat.OpenXml.Spreadsheet.Font;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WebApp.Business
{
    /// <summary>
    /// Objetos de Perfis e/ou de Usuários
    /// </summary>
    public class ObjetoBLL
    {
        // *************** Objeto  *************************************************************

        /// <summary>
        ///  Lista de todos os Objetos não deletados
        /// </summary>
        /// <param name="obj_id">Filtro por Id do Objeto, 0 para todos</param> 
        /// <param name="filtro_obj_codigo">Filtro por codigo de Objeto, 0 para todos</param> 
        /// <param name="filtro_obj_descricao">Filtro por descrição de Objeto, null para todos</param> 
        /// <param name="filtro_clo_id">Filtro por classe de Objeto, -1 para todos</param> 
        /// <param name="filtro_tip_nome">Filtro por tipo de Objeto, "" para todos</param> 
        /// <returns>Lista de Objetos</returns>
        public List<Objeto> Objeto_ListAll(int obj_id, string filtro_obj_codigo = null, string filtro_obj_descricao = null, int? filtro_clo_id = -1, string filtro_tip_nome = "")
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_ListAll(obj_id, filtro_obj_codigo, filtro_obj_descricao, filtro_clo_id, filtro_tip_nome, paramUsuario.usu_id);
        }

        /// <summary>
        /// Busca os dados do  Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <returns>Objeto</returns>
        public Objeto Objeto_GetbyID(int obj_id)
        {
            Objeto obj = new ObjetoDAO().Objeto_GetbyID(obj_id).FirstOrDefault();
            obj.lstTipos = CriaListaCmbTiposObjeto(obj.clo_id);

            return obj;

        }

        /// <summary>
        ///  Altera os dados do Objeto
        /// </summary>
        /// <param name="obj_id">Id do Objeto</param>
        /// <param name="obj_codigo">Código do Objeto</param>
        /// <param name="obj_descricao">Descrição do Objeto (opcional)</param>
        /// <param name="tip_id">Id do Tipo do Objeto (opcional)</param>
        /// <returns>int</returns>
        public int Objeto_Salvar(int obj_id, string obj_codigo, string obj_descricao, int tip_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_Salvar(obj_id, obj_codigo, obj_descricao, tip_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere Novo Objeto pelo Código
        /// </summary>
        /// <param name="obj_codigo">Código do Objeto</param>
        /// <param name="obj_descricao">Descrição do Objeto</param>
        /// <param name="obj_NumeroObjetoAte">No caso de inserção de item Número Objeto, é possível inserção em lote</param>
        /// <param name="obj_localizacaoAte">No caso de inserção de item Localização, é possível inserção em lote</param>
        /// <returns>string</returns>
        public string Objeto_Inserir(string obj_codigo, string obj_descricao, string obj_NumeroObjetoAte, string obj_localizacaoAte)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_Inserir(obj_codigo, obj_descricao, obj_NumeroObjetoAte, obj_localizacaoAte, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        public int Objeto_AtivarDesativar(int obj_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_AtivarDesativar(obj_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        /// Exclui Objeto do tipo Subdivisao3 (encontro/ estrutura de terra; encontros/ estrutura de concreto)
        /// </summary>
        /// <param name="tip_id">Id Tipo do Objeto Selecionado</param>
        /// <param name="obj_id_tipoOAE">Id Objeto Selecionado</param>
        /// <returns>string</returns>
        public string Objeto_Subdivisao3_Excluir(int tip_id, int obj_id_tipoOAE)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_Subdivisao3_Excluir(tip_id, obj_id_tipoOAE, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        /// <summary>
        ///  Excluir (logicamente) Objeto
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        public int Objeto_Excluir(int obj_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_Excluir(obj_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Lista de todos os Documentos Associados ao Objeto selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> Objeto_Documentos_ListAll(int obj_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_Documentos_ListAll(obj_id, paramUsuario.usu_id);

        }

        /// <summary>
        /// Busca Lista de Documentos 
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="codDoc">Código ou Parte a se localizar</param>
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreencheCmbDocumentosLocalizados(int obj_id, string codDoc)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            List<Documento> lstDocumentos = new ObjetoDAO().Objeto_DocumentosNaoAssociados_ListAll(obj_id, codDoc, paramUsuario.usu_id);
            List<SelectListItem> lstListaCmbDocumentosLocalizados = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstDocumentos)
            {
                string txt = temp.doc_codigo + "-" + temp.doc_descricao;
                lstListaCmbDocumentosLocalizados.Add(new SelectListItem() { Text = txt, Value = temp.doc_id.ToString() });
            }

            int total = lstDocumentos.Count > 0 ? lstDocumentos[0].total_registros : 0;
            lstListaCmbDocumentosLocalizados.Insert(0,new SelectListItem() { Text = "total_registros", Value = total.ToString() });

            return lstListaCmbDocumentosLocalizados;
        }

        /// <summary>
        ///    Associa Documentos ao Objeto selecionado
        /// </summary>
        /// <param name="doc_ids">Ids dos Documentos Selecionados</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        public int Objeto_AssociarDocumentos(string doc_ids, int obj_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_AssociarDocumentos(doc_ids, obj_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///    Desassocia Documentos do Objeto selecionado
        /// </summary>
        /// <param name="doc_id">Id dos Documento Selecionado</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        public int Objeto_DesassociarDocumento(int doc_id, int obj_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().Objeto_DesassociarDocumento(doc_id, obj_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        /// <summary>
        /// Lista Objeto Localizacao
        /// </summary>
        /// <param name="obj_id_TipoOAE">Id do Objeto do Tipo OAE</param> 
        /// <param name="tip_id_Grupo">Id do tipo Grupo de Objeto, nivel subdivisao1</param> 
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreencheCmbObjetoLocalizacao(int obj_id_TipoOAE, int tip_id_Grupo)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];

            List<Objeto> lstObjetosLocalizacao = new ObjetoDAO().Objeto_Localizacao_ListAll(obj_id_TipoOAE, tip_id_Grupo, paramUsuario.usu_id);
            List<SelectListItem> lstListaCmbObjetoLocalizacao = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstObjetosLocalizacao)
            {
                string txt = temp.obj_codigo + " (" + temp.obj_descricao + ")";
                lstListaCmbObjetoLocalizacao.Add(new SelectListItem() { Text = txt, Value = temp.obj_id.ToString() });
            }

            return lstListaCmbObjetoLocalizacao;
        }


        // ================= VALORES DE ATRIBUTO DO OBJETO SELECIONADO ===================================================

        /// <summary>
        /// Lista os Atributos do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="atr_id">Id do ATRIBUTO selecionado</param>
        /// <param name="ord_id">Id do Objeto selecionado</param>
        /// <returns>JsonResult Lista de ObjAtributoValores</returns>
        public List<ObjAtributoValores> ObjAtributoValores_ListAll(int obj_id, int? atr_id = null, int? ord_id = null)
        {
            return new ObjetoDAO().ObjAtributoValores_ListAll(obj_id,null, ord_id);
        }

        /// <summary>
        /// Busca os dados do Atributos Fixo do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="atr_id">Id do ATRIBUTO selecionado</param>
        /// <returns>ObjAtributoValores</returns>
        public ObjAtributoValores ObjAtributoValores_GetbyID(int obj_id, int atr_id)
        {
            ObjAtributoValores objAtf = new ObjetoDAO().ObjAtributoValores_ListAll(obj_id, atr_id).FirstOrDefault();

            // preenche o combo se houver Itens de Atributo
            if (objAtf.nItens > 0)
                objAtf.lstItens = CrialstAtributofixo_Itens(atr_id);

            return objAtf;
        }


        /// <summary>
        ///  Salva o Valor dos ATRIBUTOs  no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <param name="codigoOAE">Código da Obra de Arte</param>
        /// <param name="selidTipoOAE">Id do Tipo de Obra de Arte</param>
        /// <param name="ord_id">Id da Ordem de Serviço</param>
        /// <returns>int</returns>
        public int ObjAtributoValores_Salvar(ObjAtributoValores ObjAtributoValor, string codigoOAE, int selidTipoOAE, int ord_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributoValores_Salvar(ObjAtributoValor, codigoOAE, selidTipoOAE, paramUsuario.usu_id, paramUsuario.usu_ip, ord_id);
        }

        /// <summary>
        ///  Salva valor de somente 1 atributo no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <returns>int</returns>
        public int ObjAtributoValor_Salvar(ObjAtributoValores ObjAtributoValor)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributoValor_Salvar(ObjAtributoValor, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Busca o valor da VDM na API DER e retorna o valor ati_id do combo na ficha de inspecao
        /// </summary>
        /// <param name="obj_codigo_TipoOAE">Codigo do Objeto TIPO OAE</param>
        /// <param name="itipo_pista">Valor do Tipo de Pista (17=dupla/16=simples)</param>
        /// <returns>int</returns>
        public int BuscaValorVDM(string obj_codigo_TipoOAE, int itipo_pista)
        {
            int retorno = -1;
            string[] pedacos = obj_codigo_TipoOAE.Trim().Split('-');

            string rodovia = pedacos[0].ToUpper().Replace(" ", "");

            CultureInfo culturePTBR =  new CultureInfo("pt-BR") ;

            string quilometragem = pedacos[1]; //.Replace(",", ".");
            decimal km = Convert.ToDecimal(quilometragem, culturePTBR);

            IntegracaoDAO saida = new IntegracaoDAO();
            List<vdm> listaVDM = saida.get_VDMs(rodovia, 0 , 999);

            if ((listaVDM.Count == 1) && (listaVDM[0].vdm_ano == -1))
                return -1;

            decimal valorVDM1 = 0;
            decimal valorVDM2 = 0;
            decimal valorVDM = 0;
            bool achou = false;

            // procura o intervalo correto
            for (int i=0; i < listaVDM.Count; i++)
            {
                if ((listaVDM[i].vdm_valor1 == null) || (listaVDM[i].vdm_valor1 == ""))
                    listaVDM[i].vdm_valor1 = "0";

                if ((listaVDM[i].vdm_valor2 == null) || (listaVDM[i].vdm_valor2 == ""))
                    listaVDM[i].vdm_valor2 = "0";

               decimal kmIni = Convert.ToDecimal(listaVDM[i].pcl_kminicial.Replace(".", ","), culturePTBR);
               decimal kmFim = Convert.ToDecimal(listaVDM[i].pcl_kmfinal.Replace(".", ","), culturePTBR);
               if (( kmIni <= km) && (kmFim >= km))
                {
                    valorVDM1 = Convert.ToDecimal(listaVDM[i].vdm_valor1, culturePTBR);
                    valorVDM2 = Convert.ToDecimal(listaVDM[i].vdm_valor2, culturePTBR);
                    achou = true;
                    break;
                }
            }

            // se nao achou, procura a faixa mais proxima
            if (!achou)
            {
                for (int i = 0; i < listaVDM.Count - 1; i++)
                {
                    if ((listaVDM[i].vdm_valor1 == null) || (listaVDM[i].vdm_valor1 == ""))
                       listaVDM[i].vdm_valor1 = "0";

                    if ((listaVDM[i].vdm_valor2 == null) || (listaVDM[i].vdm_valor2 == ""))
                        listaVDM[i].vdm_valor2 = "0";

                    decimal kmIni_prox = Convert.ToDecimal(listaVDM[i + 1].pcl_kminicial.Replace(".", ","), culturePTBR);
                    decimal kmFim = Convert.ToDecimal(listaVDM[i].pcl_kmfinal.Replace(".", ","), culturePTBR);

                    if ((km - kmFim) < (kmIni_prox - km))
                    {
                        valorVDM1 = Convert.ToDecimal(listaVDM[i].vdm_valor1, culturePTBR);
                        valorVDM2 = Convert.ToDecimal(listaVDM[i].vdm_valor2, culturePTBR);
                        achou = true;
                        break;
                    }
                    else
                    {
                        valorVDM1 = Convert.ToDecimal(listaVDM[i + 1].vdm_valor1.Trim() == "" ? "0" : listaVDM[i + 1].vdm_valor1, culturePTBR);
                        valorVDM2 = Convert.ToDecimal(listaVDM[i + 1].vdm_valor2.Trim() == "" ? "0" : listaVDM[i + 1].vdm_valor2, culturePTBR);
                    }
                }
            }

            /* analisa e retorna
               ati_id  atr_id  valor
               143     84      ACIMA DE 20.000
               144     84      DE 5.000 A 20.000
               145     84      DE 1.000 A 5.000
               146     84      ATÉ 1.000
           */
            if (itipo_pista == 16) // pista simples
                valorVDM = valorVDM1 + valorVDM2;
            else
                if (itipo_pista == 17) // pista dupla
                    valorVDM = valorVDM1 > valorVDM2 ? valorVDM1 : valorVDM2;


            if (valorVDM > 20000)
                retorno = 143;
            else
                if ((valorVDM >= 5000) && (valorVDM < 20000))
                retorno = 144;
            else
                if ((valorVDM >= 1000) && (valorVDM < 5000))
                retorno = 145;
            else
                if (valorVDM < 1000)
                retorno = 146;

             return retorno;

        }


        /// <summary>
        ///  Cria lista para preenchimento do combo cmbAtributoFixoItens
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO</param>
        /// <returns>Lista de SelectListItem</returns>
        private List<SelectListItem> CrialstAtributofixo_Itens(int atr_id)
        {
            List<ObjAtributoItem> lstAtributofixo_Itens = new ObjetoDAO().ObjAtributoItem_ListAll(atr_id, null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lstAtributofixo_Itens)
            {
                lstSaida.Add(new SelectListItem() { Text = temp.ati_item, Value = temp.ati_id.ToString() });
            }

            return lstSaida;
        }


        // ==============================================================================================

        // *************** CLASSES DE Objeto  *************************************************************

        /// <summary>
        ///     Lista de todas as Classes não deletadas
        /// </summary>
        /// <param name="clo_id">Filtro por Id da Classe de Objeto, null para todos</param> 
        /// <returns>Lista de Objetos</returns>
        public List<ObjClasse> ObjClasse_ListAll(int? clo_id = null)
        {
            return new ObjetoDAO().ObjClasse_ListAll(clo_id);
        }

        /// <summary>
        /// Dados da Classe selecionada
        /// </summary>
        /// <param name="ID">Id da Classe selecionado</param>
        /// <returns>ObjClasse</returns>
        public ObjClasse ObjClasse_GetbyID(int ID)
        {
            return new ObjetoDAO().ObjClasse_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Executa Insere ou Altera os dados da Classe no Banco
        /// </summary>
        /// <param name="objClasse">Dados da Classe</param>
        /// <returns>int</returns>
        public int ObjClasse_Salvar(ObjClasse objClasse)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjClasse_Salvar(objClasse, paramUsuario.usu_id, paramUsuario.usu_ip);

        }

        /// <summary>
        ///     Excluir (logicamente) Objeto
        /// </summary>
        /// <param name="clo_id">Id da Classe Selecionada</param>
        /// <returns>int</returns>
        public int ObjClasse_Excluir(int clo_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjClasse_Excluir(clo_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="clo_id">Id da Classe Selecionada</param>
        /// <returns>int</returns>
        public int ObjClasse_AtivarDesativar(int clo_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjClasse_AtivarDesativar(clo_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // *************** TIPOS DE Objeto  *************************************************************

        /// <summary>
        ///     Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="clo_id">Filtro por Id da Classe Selecionada, null para todos</param>
        /// <param name="tip_id">Filtro por Id do Tipo Selecionado, null para todos</param>
        /// <returns>Lista de Objetos</returns>
        public List<ObjTipo> ObjTipo_ListAll(int clo_id, int? tip_id)
        {
            return new ObjetoDAO().ObjTipo_ListAll(clo_id, tip_id);
        }


        /// <summary>
        /// lista concatenada dos pais de tipos de objeto por classe
        /// </summary>
        /// <param name="clo_id">Classe do Objeto selecionado</param>
        /// <returns>string</returns>
        public string lstTipos_da_Classe(int clo_id)
        {
            return new ObjetoDAO().lstTipos_da_Classe(clo_id);
        }


        /// <summary>
        /// Dados do Tipo selecionado
        /// </summary>
        /// <param name="tip_id">Id do Tipo selecionado</param>
        /// <param name="clo_id">Id da Classe do selecionado</param>
        /// <returns>ObjTipo</returns>
        public ObjTipo ObjTipo_GetbyID(int clo_id, int tip_id)
        {
            return new ObjetoDAO().ObjTipo_ListAll(clo_id, tip_id).FirstOrDefault();
        }

        /// <summary>
        ///    Executa Insere ou Altera os dados do Tipo no Banco
        /// </summary>
        /// <param name="objTipo">Dados do Tipo</param>
        /// <returns>int</returns>
        public int ObjTipo_Salvar(ObjTipo objTipo)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjTipo_Salvar(objTipo, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Objeto
        /// </summary>
        /// <param name="tip_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int ObjTipo_Excluir(int tip_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjTipo_Excluir(tip_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="tip_id">Id do Tipo Selecionado</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int ObjTipo_AtivarDesativar(int tip_id, int usu_id, string ip)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjTipo_AtivarDesativar(tip_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** ATRIBUTOS DE OBJETO  *************************************************************

        /// <summary>
        /// Preenchimento do combo Classes 
        /// </summary>
        /// <param name="clo_id">Id da Classe Selecionada, null para todas</param>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbClassesObjeto(int? clo_id = null)
        {
            List<ObjClasse> lstObjClasse = new ObjetoDAO().ObjClasse_ListAll(clo_id); // lista de "ObjClasse"
            List<SelectListItem> lstListacmbClassesObjeto = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lstObjClasse)
            {
                string txt = temp.clo_nome; // + "-" + temp.clo_descricao;
                lstListacmbClassesObjeto.Add(new SelectListItem() { Text = txt, Value = temp.clo_id.ToString() });
            }

            return lstListacmbClassesObjeto;
        }

        /// <summary>
        ///  Complemento do método PreencheCmbTiposObjeto pois é chamado também em outra classe (GetbyID)
        /// </summary>
        /// <param name="clo_id">Classe do ATRIBUTO</param>
        /// <param name="tip_pai">id do tipo pai</param>
        /// <param name="excluir_existentes">Menos os valores já existentes</param>
        /// <param name="obj_id">Id do objeto selecionado</param>
        /// <param name="somente_com_variaveis_inspecao">Somente se possuir variaveis inspecao</param>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> CriaListaCmbTiposObjeto(int? clo_id, int? tip_pai = 0, int? excluir_existentes = 0, int? obj_id = 0, int? somente_com_variaveis_inspecao = 0)
        {
            List<ObjTipo> lstObjTipo = new ObjetoDAO().ObjTipo_ListAll(clo_id, null, tip_pai, excluir_existentes, obj_id);

            if (clo_id == 6) // para ordenar Superestrutura, Mesoestrutura, Infraestrutura, encontro
                lstObjTipo = lstObjTipo.OrderBy(o => o.tip_id).ToList();

            List<SelectListItem> lstListaCmbTiposObjeto = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lstObjTipo)
            {
                string txt = temp.tip_nome; // + "-" + temp.tip_descricao;
                string valor = temp.tip_id.ToString() + ":" + temp.tip_codigo;

                if (somente_com_variaveis_inspecao == 0)
                    lstListaCmbTiposObjeto.Add(new SelectListItem() { Text = txt, Value = valor });
                else
                {
                    if (clo_id != 9)
                           lstListaCmbTiposObjeto.Add(new SelectListItem() { Text = txt, Value = valor });
                    else
                        if (temp.tem_var_inspecao > 0)
                            lstListaCmbTiposObjeto.Add(new SelectListItem() { Text = txt, Value = valor });

                }
            }
          //  return lstListaCmbTiposObjeto;

            List<SelectListItem> distinctV = lstListaCmbTiposObjeto
                                                 .GroupBy(m => new { m.Text })
                                                 .Select(group => group.First())
                                                 .OrderBy(o => o.Text)
                                                 .ToList();
            return distinctV;
        }

        // ----------------------------------------------------------------------

        /// <summary>
        ///     Lista de todos os Atributos não deletados
        /// </summary>
        /// <param name="atr_id">Filtro por Id do atributo, null para todos</param> 
        /// <param name="filtro_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtro_descricao">Descrição ou Parte a se localizar</param>
        /// <param name="filtro_clo_id">Id da Classe a se filtrar</param>
        /// <param name="filtro_tip_id">Id do Tipo a se filtrar</param>
        /// <param name="ehAtributoFuncional">Flag de atributo funcional</param>
        /// <returns>Lista de Objetos</returns>
        public List<ObjAtributo> ObjAtributo_ListAll(int atr_id, string filtro_codigo, string filtro_descricao, int filtro_clo_id, int filtro_tip_id, int ehAtributoFuncional)
        {
            return new ObjetoDAO().ObjAtributo_ListAll(atr_id, filtro_codigo, filtro_descricao, filtro_clo_id, filtro_tip_id, ehAtributoFuncional);

        }

        /// <summary>
        /// Dados do ATRIBUTO selecionado
        /// </summary>
        /// <param name="ID">Id do ATRIBUTO selecionado</param>
        /// <param name="ehAtributoFuncional">Flag de Atributo Funcional</param>
        /// <returns>ObjAtributo</returns>
        public ObjAtributo ObjAtributo_GetbyID(int ID, int ehAtributoFuncional)
        {
            ObjAtributo objAtributo = new ObjetoDAO().ObjAtributo_ListAll(ID, "", "", -1, -1, ehAtributoFuncional).FirstOrDefault();
            objAtributo.lstTipos = CriaListaCmbTiposObjeto(objAtributo.clo_id);

            return objAtributo;
        }

        /// <summary>
        ///    Executa Insere ou Altera os dados do ATRIBUTO no Banco
        /// </summary>
        /// <param name="objAtributo">Dados do ATRIBUTO</param>
        /// <returns>int</returns>
        public int ObjAtributo_Salvar(ObjAtributo objAtributo)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributo_Salvar(objAtributo, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) ATRIBUTO
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO Selecionado</param>
        /// <returns>int</returns>
        public int ObjAtributo_Excluir(int atr_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributo_Excluir(atr_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO Selecionado</param>
        /// <returns>int</returns>
        public int ObjAtributo_AtivarDesativar(int atr_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributo_AtivarDesativar(atr_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // *************** ITENS DE ATRIBUTO DE Objeto  *************************************************************

        /// <summary>
        /// Lista de todos os ITENS DE ATRIBUTO não deletados
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO Selecionado</param>
        /// <param name="ati_id">Id do Item do ATRIBUTO Selecionado</param>
        /// <returns>Lista de ObjAtributoItem</returns>
        public List<ObjAtributoItem> ObjAtributoItem_ListAll(int atr_id, int? ati_id = null)
        {
            return new ObjetoDAO().ObjAtributoItem_ListAll(atr_id, ati_id);
        }

        /// <summary>
        /// Dados do Item de ATRIBUTO selecionado
        /// </summary>
        /// <param name="atr_id">Id do ATRIBUTO selecionado</param>
        /// <param name="ati_id">Id do Item do ATRIBUTO selecionado</param>
        /// <returns>ObjAtributoItem</returns>
        public ObjAtributoItem ObjAtributoItem_GetbyID(int atr_id, int ati_id)
        {
            return new ObjetoDAO().ObjAtributoItem_ListAll(atr_id, ati_id).FirstOrDefault();
        }

        /// <summary>
        ///    Executa Insere ou Altera os dados do ITEM de ATRIBUTO no Banco
        /// </summary>
        /// <param name="objAtributoItem">Dados do ITEM de ATRIBUTO</param>
        /// <returns>int</returns>
        public int ObjAtributoItem_Salvar(ObjAtributoItem objAtributoItem)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributoItem_Salvar(objAtributoItem, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) ITEM de ATRIBUTO
        /// </summary>
        /// <param name="ati_id">Id do ITEM do ATRIBUTO Selecionado</param>
        /// <returns>int</returns>
        public int ObjAtributoItem_Excluir(int ati_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributoItem_Excluir(ati_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Objeto
        /// </summary>
        /// <param name="ati_id">Id do ITEM do ATRIBUTO Selecionado</param>
        /// <returns>int</returns>
        public int ObjAtributoItem_AtivarDesativar(int ati_id)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjAtributoItem_AtivarDesativar(ati_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** GRUPOS / VARIÁVEIS / VALORES DE INSPEÇÃO  *************************************************************
        /// <summary>
        /// Lista Grupos/Variáveis do Objeto Selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto selecionado</param>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <param name="ehProvidencia">Flag para tela Providências</param>
        /// <param name="filtro_prt_id">Filtro id da Providência</param>
        /// <returns>Lista de ObjAtributoValores</returns>
        public List<GruposVariaveisValores> GruposVariaveisValores_ListAll(int obj_id, int? ord_id = -1, int? ehProvidencia = 0, int? filtro_prt_id = 0)
        {
            return new ObjetoDAO().GruposVariaveisValores_ListAll(obj_id, ord_id, ehProvidencia, filtro_prt_id);
        }


        /// <summary>
        ///    Cria/Associa Grupos ao Objeto selecionado
        /// </summary>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <param name="grupos_codigos">Codigos dos grupos a serem salvos</param>
        /// <returns>int</returns>
        public int AssociarGruposVariaveis(int obj_id, string grupos_codigos)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().AssociarGruposVariaveis(obj_id, grupos_codigos, paramUsuario.usu_id, paramUsuario.usu_ip);
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
        /// <param name="ord_id">Id da ordem de serviço, se houver</param>
        /// <returns>int</returns>
        public int GruposVariaveisValores_Salvar(int obj_id_tipoOAE, string Ponto1, string Ponto2, string Ponto3, string OutrasInformacoes, string listaConcatenada, int? ord_id = -1)
        {
            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().GruposVariaveisValores_Salvar(obj_id_tipoOAE, Ponto1, Ponto2, Ponto3, OutrasInformacoes, listaConcatenada, paramUsuario.usu_id, paramUsuario.usu_ip, ord_id);
        }



        // *************** EXPORTA FICHA INSPECAO PARA EXCEL A1  *************************************************************

        /// <summary>
        /// Limpa arquivos da pasta temp, com data anterior à vespera do dia corrente
        /// </summary>
        public void limpaArquivosAntigos()
        {
            string caminho = System.Web.HttpContext.Current.Server.MapPath("~/temp");

            // apaga arquivos xlsx residuais
            string[] txtFilesArray = Directory.GetFiles(caminho, "*.xlsx", SearchOption.TopDirectoryOnly);
            foreach (string arq in txtFilesArray)
            {
                try
                {
                    if (File.GetCreationTime(arq) < DateTime.Now.AddDays(-1))
                      File.Delete(arq);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// Preenche a Ficha de Inspeção Rotineira em Excel e disponibiliza para download
        /// </summary>
        /// <param name="obj_id">Id do objeto da Ficha</param>
        /// <param name="ord_id">Id da O.S pertinente ao objeto</param>
        /// <returns>string</returns>
        public string ObjFichaInspecaoRotineira_ExportarXLS(int obj_id, int ord_id)
        {

            string arquivo_modelo_caminhoFull = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Modelo_Folha_A1_Insp_Rotineira.xlsx");
            string arquivo_saida = "Modelo_Folha_A1_Insp_Rotineira_" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "") + ".xlsx";
            string arquivo_saida_caminhoFull = System.Web.HttpContext.Current.Server.MapPath("~/temp/") + "/" + arquivo_saida;

            string arquivo_saida_caminho_virtual = HttpContext.Current.Request.Url.Host + "/temp/" + arquivo_saida;
            string saida = "";

            List<string> Headers = new List<string>();
            int dtLinha = 0;

            int nLinhaVisivel = 99;
            int ultimaLinha = 352;

            Gerais ger = new Gerais();
            try
            {
                limpaArquivosAntigos();

                File.Copy(arquivo_modelo_caminhoFull, arquivo_saida_caminhoFull);


                // faz busca os DADOS no banco
                System.Data.DataSet dsDADOS = new RelatoriosDAO().FICHA_INSPECAO_CADASTRAL(obj_id, ord_id);
                System.Data.DataTable dtDADOS = dsDADOS.Tables[0];

                // faz busca os GRUPOS no banco
                System.Data.DataSet dsGrupos = new ObjetoDAO().GruposVariaveisValores_ListAll_DS(obj_id, ord_id);
                System.Data.DataTable dtGrupos = dsGrupos.Tables[0];


                // Abre a planilha para edicao
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(arquivo_saida_caminhoFull, true))
                {
                    // LEITURA DA PLANILHA
                    Worksheet worksheet = ger.GetWorksheet(doc, "Modelo Folha A1 - Insp Rot");

                    // ======= PREENCHE OS DADOS ===============================================
                    for (int col = 78; col <= 86; col++) // VARRE as COLUNAS N até V
                    {
                        for (int li = 1; li < ultimaLinha; li++)
                        {
                            Cell cell = ger.GetCell(worksheet, ((char)col).ToString(), Convert.ToUInt32(li));
                            string valorCelula = ger.GetCellValue(doc, cell).Replace("[", "").Replace("]", "");

                            // procura o valor no dataset do banco e substitui
                            if (dtDADOS.Rows.Count >= 1)
                            {
                                if (dtDADOS.Columns.Contains(valorCelula))
                                {
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtDADOS.Rows[0][valorCelula]));
                                }
                            }

                        }
                    }


                    // =======   MONTA OS GRUPOS ===============================================
                    if (dtGrupos.Rows.Count > nLinhaVisivel -17 ) // se for menor que nLinhaVisivel mostra as linhas excedentes (originalmente ocultas)
                    {
                        // =139 - 99 + 17 - 1 = 56
                        int diferenca = dtGrupos.Rows.Count - nLinhaVisivel + 17 ;
                        for (int k = 0; k < diferenca; k++)
                        {
                            Row refRow = ger.GetRow(worksheet, Convert.ToUInt16(k + 100));
                            refRow.Hidden = new BooleanValue(false); // torna a linha visivel
                        }
                    }

                        // oculta as linhas em branco a mais
                        for (int k = dtGrupos.Rows.Count + 17; k < 312; k++)
                        {
                            Row refRow = ger.GetRow(worksheet, Convert.ToUInt16(k));
                            refRow.Hidden = new BooleanValue(true); // oculta a linha 
                        }


                    Cell cell_Superestrutura = ger.GetCell(worksheet, "N", 17);
                    UInt32 Superestrutura_StyleIndex = cell_Superestrutura.StyleIndex;

                    Cell cell_TabuleiroFS = ger.GetCell(worksheet, "N", 18);
                    UInt32 TabuleiroFS_StyleIndex = cell_TabuleiroFS.StyleIndex;

                    for (dtLinha = 0; dtLinha < dtGrupos.Rows.Count; dtLinha++)
                    {
                            Cell cell = ger.GetCell(worksheet, "N", Convert.ToUInt32(dtLinha + 17));
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtGrupos.Rows[dtLinha]["NomeGrupo"]));

                            // ************** mesclagem de colunas ******************************************
                            if (dtGrupos.Rows[dtLinha]["MesclarColunas"].ToString() == "1")
                            {
                                // mescla as celulas
                                ger.MergeCells(worksheet, "N" + (dtLinha + 17).ToString(), "V" + (dtLinha + 17).ToString());

                                for (int col = 78; col <= 86; col++) // pinta as celulas N até V
                                {
                                    // formata o backgroundcolor 
                                    Cell cell1 = ger.GetCell(worksheet, ((char)col).ToString(), Convert.ToUInt32(dtLinha + 17));
                                    if (dtGrupos.Rows[dtLinha]["cabecalho_Cor"].ToString() == "#BFBFBF")
                                        cell1.StyleIndex = Superestrutura_StyleIndex; // indice do estilo da celula N17 do .xlsx template
                                    else
                                        if (dtGrupos.Rows[dtLinha]["cabecalho_Cor"].ToString() == "#D9D9D9")
                                            cell1.StyleIndex = TabuleiroFS_StyleIndex;// indice do estilo da celula N18 do .xlsx template
                                }
                           }
                            else
                            {
                                cell = ger.GetCell(worksheet, "O", Convert.ToUInt32(dtLinha + 17));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtGrupos.Rows[dtLinha]["variavel"]));

                                cell = ger.GetCell(worksheet, "P", Convert.ToUInt32(dtLinha + 17));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtGrupos.Rows[dtLinha]["ogi_id_caracterizacao_situacao_item"]));

                                cell = ger.GetCell(worksheet, "Q", Convert.ToUInt32(dtLinha + 17));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtGrupos.Rows[dtLinha]["ati_id_condicao_inspecao_item"]));

                                cell = ger.GetCell(worksheet, "R", Convert.ToUInt32(dtLinha + 17));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtGrupos.Rows[dtLinha]["ovv_observacoes_gerais"]));

                                ger.MergeCells(worksheet, "R" + (dtLinha + 17).ToString(), "S" + (dtLinha + 17).ToString());

                                cell = ger.GetCell(worksheet, "T", Convert.ToUInt32(dtLinha + 17));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtGrupos.Rows[dtLinha]["tpu_descricao"]));

                                cell = ger.GetCell(worksheet, "U", Convert.ToUInt32(dtLinha + 17));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(dtGrupos.Rows[dtLinha]["uni_unidade"]));

                                // converte para numero decimal
                                cell = ger.GetCell(worksheet, "V", Convert.ToUInt32(dtLinha + 17));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(dtGrupos.Rows[dtLinha]["ovv_tpu_quantidade"])));
                            }
                    }


             //       **************mesclagem de linhas ******************************************
                    string Variavel_cell1 = ""; string Variavel_cell2 = "";
                    string Condicao_cell1 = ""; string Condicao_cell2 = "";
                    dtLinha = 0;
                    while (dtLinha < dtGrupos.Rows.Count)
                    {
                        // Cell cell = GetCell(worksheet, "N", Convert.ToUInt32(dtLinha + 17));
                        int nMesclarLinhas = Convert.ToInt16(dtGrupos.Rows[dtLinha]["MesclarLinhas"]);

                        if ((dtGrupos.Rows[dtLinha]["nomeGrupo"].ToString().Trim() != "") && (nMesclarLinhas > 1))
                        {
                            Variavel_cell1 = "N" + (dtLinha + 17).ToString().Trim();
                            Condicao_cell1 = "Q" + (dtLinha + 17).ToString().Trim();

                            dtLinha += 1;

                            while ((dtLinha < dtGrupos.Rows.Count) && (nMesclarLinhas > 1) && (dtGrupos.Rows[dtLinha]["nomeGrupo"].ToString().Trim() == ""))
                            {
                                Variavel_cell2 = "N" + (dtLinha + 17).ToString().Trim();
                                Condicao_cell2 = "Q" + (dtLinha + 17).ToString().Trim();
                                dtLinha += 1;
                            }

                            ger.MergeCells(worksheet, Variavel_cell1, Variavel_cell2);
                            ger.MergeCells(worksheet, Condicao_cell1, Condicao_cell2);
                            dtLinha -= 1;
                        }

                        dtLinha += 1;
                    }

                    // fecha o arquivo e retorna
                    doc.Save();
                    doc.Close();
                }

                return arquivo_saida;
            }
            catch (Exception ex)
            {
                saida = ex.ToString();
            }

            return "";
        }



        //******************************* upload do Esquema Estrutural ********************************
        /// <summary>
        /// Faz o upload do EsquemaEstrutural
        /// </summary>
        /// <param name="model">arquivo a salvar no banco</param>
        /// <returns>retorna vazio ou a mensagem de erro</returns>
        public string EsquemaEstrutural_Upload(ObjAtributoValores model)
        {
            try
            {
                string base64String = "";
                var file = model.EsquemaEstrutural;
                if (file != null)
                {
                    var _image = Image.FromStream(file.InputStream);
                   var _thumbImage = new Gerais().imgResize(_image, maxHeight: 500);
                  //   var _thumbImage = new Gerais().imgResize(_image, maxHeight: 160);
                  //  base64String = new Gerais().ImageToBase64(_thumbImage);

                    base64String = new Gerais().ImageToBase64(_thumbImage);

                    Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];

                    //  int retorno = new UsuarioBLL().Usuario_AlterarFoto(base64String, paramUsuario.usu_id);

                    model.atv_valores = base64String;

                    int retorno = new ObjetoDAO().ObjAtributoValor_Salvar(model, paramUsuario.usu_id, paramUsuario.usu_ip);

                }

                return base64String;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
        /// <returns>Lista de Objetos</returns>
        public List<ObjPriorizacao> ObjPriorizacao_ListAll(string CodRodovia,
                                                            string FiltroidRodovias, string FiltroidRegionais, string FiltroidObjetos, string Filtro_data_De, string Filtro_data_Ate,
                                                            int? somenteINSP_ESPECIAIS = 0)
        {
            // cria lista de Regionais
            string strRegionais = new IntegracaoDAO().str_Regionais();

            Usuario paramUsuario = (Usuario)HttpContext.Current.Session["Usuario"];
            return new ObjetoDAO().ObjPriorizacao_ListAll(CodRodovia, FiltroidRodovias, FiltroidRegionais, FiltroidObjetos, Filtro_data_De, Filtro_data_Ate, somenteINSP_ESPECIAIS, strRegionais, paramUsuario.usu_id);

        }


        /// <summary>
        /// Preenchimento do combo Filtro Regionais 
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbFiltroRegionais()
        {
            List<SelectListItem> lstListacmbFiltroRegionais = new List<SelectListItem>(); // lista de combo
            List<Regional> lstRegionais = new IntegracaoDAO().get_Regionais(); // lista de "Regional"
            if (lstRegionais.Count > 0)
            {
                if (lstRegionais[0].reg_id > 0)
                {
                    foreach (var temp in lstRegionais)
                    {
                        if (temp.reg_email.Trim() != "")
                        {
                            string txt = temp.reg_codigo + "-" + temp.reg_descricao;
                            lstListacmbFiltroRegionais.Add(new SelectListItem() { Text = txt, Value = temp.reg_id.ToString() });
                        }
                    }
                }
                else
                {
                    lstListacmbFiltroRegionais.Add(new SelectListItem() { Text = lstRegionais[0].reg_codigo, Value = "-1" });
                }
            }
            else
            {
                lstListacmbFiltroRegionais.Add(new SelectListItem() { Text = "Erro ao sincronizar Regionais", Value = "-1" });
            }
            return lstListacmbFiltroRegionais;
        }


        /// <summary>
        /// Preenchimento do combo Filtro Email Regionais 
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbEmailRegionais()
        {
            List<SelectListItem> lstListacmbFiltroRegionais = new List<SelectListItem>(); // lista de combo
            List<Regional> lstRegionais = new IntegracaoDAO().get_Regionais(); // lista de "Regional"
            if (lstRegionais[0].reg_id > 0)
            {
                foreach (var temp in lstRegionais)
                {
                    if (temp.reg_email.Trim() != "")
                    {
                        string txt = temp.reg_codigo + "-" + temp.reg_descricao + "<" + temp.reg_email.Trim() + ">";
                        lstListacmbFiltroRegionais.Add(new SelectListItem() { Text = txt, Value = txt });
                    }
                }
            }
            else
            {
                lstListacmbFiltroRegionais.Add(new SelectListItem() { Text = lstRegionais[0].reg_codigo, Value = "-1" });
            }
            return lstListacmbFiltroRegionais;
        }

        /// <summary>
        /// Funçao para filtrar a lista de retorno porque a busca pelo SirGeo é exata e nao aproximada
        /// </summary>
        /// <param name="targerList">Lista a se procurar</param>
        /// <param name="filterStr">String procurada</param>
        /// <returns>Lista Rodovia </returns>
        public static List<Rodovia> StartsWithStringContains(List<Rodovia> targerList, string filterStr)
        {
            var resultList = from resultValues in targerList
                             where
                                 (resultValues.rod_codigo.StartsWith(filterStr))
                             select
                                 resultValues;

            return resultList.ToList();
        }

        /// <summary>
        /// Preenchimento do combo Filtro Rodovias 
        /// </summary>
        /// <param name="rod_codigo">Filtro por Código da Rodovia</param>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreenchecmbFiltroRodovias(string rod_codigo = "")
        {
            List<SelectListItem> lstListacmbFiltroRodovias = new List<SelectListItem>(); // lista de combo
            List<Rodovia> lstRodovias = new IntegracaoDAO().get_Rodovias(rod_codigo); // lista de "Rodovias"
            if (lstRodovias[0].rod_id > 0)
            {
                // filtra aqui por aproximacao porque a busca pelo SirGeo é exata
                rod_codigo = rod_codigo.ToUpper();
                List<Rodovia> SortedList = StartsWithStringContains(lstRodovias, rod_codigo);



                foreach (var temp in SortedList)
                {
                    if (temp.rod_codigo.Trim() != "")
                    {
                        string txt = temp.rod_codigo; // + " (" + temp.rod_descricao + ")";
                        lstListacmbFiltroRodovias.Add(new SelectListItem() { Text = txt, Value = temp.rod_id.ToString() });
                    }
                }
            }
            else
            {
                lstListacmbFiltroRodovias.Add(new SelectListItem() { Text = lstRodovias[0].rod_descricao, Value = "-1" });
            }

            return lstListacmbFiltroRodovias;
        }


    }

}
