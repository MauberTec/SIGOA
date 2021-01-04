using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Web.Mvc;
using WebApp.Helpers;
using System.IO;

namespace WebApp.Business
{
    /// <summary>
    /// Regras de Negocio para Documentos
    /// </summary>
    public class DocumentoBLL : Controller
    {

        /// <summary>
        /// Preenchimento do combo Tipos de Documentos
        /// </summary>
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreencheCmbDocTipo()
        {
            List<DocTipo> lstDocTipo = new DocumentoBLL().DocTipo_ListAll("", -1); // lista de "DocTipo"
            List<SelectListItem> lstListaCmbDocTipo = new List<SelectListItem>(); // lista de combo

            SelectListItem temp0a = new SelectListItem();
            SelectListItem temp0b = new SelectListItem();

            int igeralAnterior = 1;
            foreach (var temp in lstDocTipo)
            {
                if (temp.tpd_subtipo < 3) // somente os tipos 0,1,2
                {
                    if (temp.tpd_subtipo == 0) // verifica se é "NC" nao cadastrado
                    {
                        temp0a = new SelectListItem() { Text = "Documento Técnico Não Codificado", Value = "-2", Disabled = true };
                    }
                    else
                        if (igeralAnterior != temp.tpd_subtipo) // 1=geral; 2= especifico;
                        {
                            if (temp.tpd_subtipo == 1)
                                lstListaCmbDocTipo.Add(new SelectListItem() { Text = "Documento Técnico Geral", Value = "-2", Disabled = true });
                            else
                            {
                                lstListaCmbDocTipo.Add(new SelectListItem() { Text = "", Value = "-6", Disabled = true });
                                lstListaCmbDocTipo.Add(new SelectListItem() { Text = "Documento Técnico Específico", Value = "-2", Disabled = true });
                            }
                        }

                    string txt = "--> " + temp.tpd_id + "-" + temp.tpd_descricao;

                    // verifica se é "NC" nao cadastrado
                    if (temp.tpd_subtipo == 0)
                        temp0b = new SelectListItem() { Text = txt, Value = temp.tpd_id.ToString() };
                    else
                        lstListaCmbDocTipo.Add(new SelectListItem() { Text = txt, Value = temp.tpd_id.ToString() });

                    igeralAnterior = temp.tpd_subtipo;
                }
            }

            // adiciona "NC" nao cadastrado em ultimo na lista
            lstListaCmbDocTipo.Add(new SelectListItem() { Text = "", Value = "-6", Disabled = true });
            lstListaCmbDocTipo.Add(temp0a);
            lstListaCmbDocTipo.Add(temp0b);

            return lstListaCmbDocTipo;
        }

        /// <summary>
        ///  Complemento do método PreencheCmbClasseProjeto pois é chamado também em outra classe (GetbyID)
        /// </summary>
        /// <param name="tipo">Tipo do Documento Selecionado</param>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> CriaListacmbClasseProjeto(string tipo)
        {
            List<DocTipo> lstDocTipos = new DocumentoBLL().DocTipo_ListAll(tipo, -1);
            DocTipo tipodoc = lstDocTipos.FirstOrDefault();

            List<DocTipo> lstSubDocTipo = new DocumentoBLL().DocTipo_ListAll("", 3); //3= subtipos
            List<SelectListItem> lstListaCmbSubDocTipo = new List<SelectListItem>(); // lista de combo
            if (tipo != "")
            {
                foreach (var temp in lstSubDocTipo)
                    if (temp.tpd_subtipo == 3) // 3= subtipos
                    {
                        if (((tipodoc.tpd_subtipo == 1) && (temp.tpd_id.EndsWith("00")))
                                || ((tipodoc.tpd_subtipo == 1) && (tipodoc.tpd_id == "PP"))
                                || ((tipodoc.tpd_subtipo == 1) && (tipodoc.tpd_id != "PP") && (temp.tpd_id.EndsWith("00")))
                                || (tipodoc.tpd_subtipo == 2))
                        {
                            string txt = temp.tpd_id + "-" + temp.tpd_descricao;
                            lstListaCmbSubDocTipo.Add(new SelectListItem() { Text = txt, Value = temp.tpd_id.ToString() });
                        }
                    }
            }
            else
            {
                foreach (var temp in lstDocTipos)
                {
                    if (temp.tpd_subtipo == 3)
                    {
                        string txt = temp.tpd_id + "-" + temp.tpd_descricao;
                        lstListaCmbSubDocTipo.Add(new SelectListItem() { Text = txt, Value = temp.tpd_id.ToString() });
                    }

                }


                lstListaCmbSubDocTipo.Sort((x, y) => x.Value.CompareTo(y.Value));
            }

            return lstListaCmbSubDocTipo;
        }

       /// <summary>
        ///  Complemento do método PreencheCmbClasseDocumento pois é chamado também em outra classe (GetbyID)
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> CriaListacmbDocClasse()
        {
            List<DocClasse> lstDocClasse = new DocumentoDAO().DocClasse_ListAll(null);
            List<SelectListItem> ListaClassesDocumento = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstDocClasse)
            {
                string txt = temp.dcl_codigo + "-" + temp.dcl_descricao;
                ListaClassesDocumento.Add(new SelectListItem() { Text = txt, Value = temp.dcl_id.ToString() });
            }

            return ListaClassesDocumento;
        }


        /// <summary>
        /// Faz o upload do documento e salva na pastas especificada
        /// </summary>
        /// <param name="arquivo">arquivo a salvar no servidor</param>
        /// <returns>retorna vazio ou a mensagem de erro</returns>
        public string Documento_Upload(Arquivo_Upload arquivo)
        {
            try
            {
                var file = arquivo.Arquivo;
                if (file != null)
                {
                    string nomeArquivo = file.FileName.Replace(" ", "_");
                    string CaminhoVirtualRaizArquivos = new ParametroBLL().Parametro_GetValor("CaminhoVirtualRaizArquivos");
                    string CaminhoFisicoRaizArquivos = new ParametroBLL().Parametro_GetValor("CaminhoFisicoRaizArquivos");

                    if (!CaminhoVirtualRaizArquivos.EndsWith("/"))
                        CaminhoVirtualRaizArquivos = CaminhoVirtualRaizArquivos + "/";

                    if (!CaminhoFisicoRaizArquivos.EndsWith("\\"))
                        CaminhoFisicoRaizArquivos = CaminhoFisicoRaizArquivos + "\\";

                    string caminhoFisicoArquivo = arquivo.CaminhoServidor.Replace(CaminhoVirtualRaizArquivos, CaminhoFisicoRaizArquivos);

                    file.SaveAs(caminhoFisicoArquivo + Path.DirectorySeparatorChar + nomeArquivo);

                }

                return "";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }


        /// <summary>
        /// Busca as Mascaras/Formatos de Documentos na tabela de Parâmetros
        /// </summary>
        /// <returns>List(string)</returns>
        public List<string> Documento_GetMascaras()
        {
            string mask1 = new ParametroBLL().Parametro_GetValor("Padrao_Docs1");
            string mask2 = new ParametroBLL().Parametro_GetValor("Padrao_Docs2");
            string mask3 = new ParametroBLL().Parametro_GetValor("Padrao_Docs3");

            List<string> lstListaMascaras = new List<string>();
            lstListaMascaras.Add(mask1);
            lstListaMascaras.Add(mask2);
            lstListaMascaras.Add(mask3);

            return  lstListaMascaras ;

        }

        // =============================================================================================================
        /// <summary>
        /// Lista de todos os Documentos
        /// </summary>
        /// <param name="doc_id">Filtro por Id de Documento, null para todos</param>
        /// <param name="doc_codigo">Filtro por Código de Documento, vazio para todos</param>
        /// <param name="doc_descricao">Filtro por Descrição de Documento, vazio para todos</param>
        /// <param name="tpd_id">Filtro por Tipo de Documento, vazio para todos</param>
        /// <param name="dcl_codigo">Filtro por Classe de Projeto de Documento, vazio para todos</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> Documento_ListAll(int? doc_id, string doc_codigo = "", string doc_descricao = "", string tpd_id = "", string dcl_codigo = "")
        {
            return new DocumentoDAO().Documento_ListAll(doc_id, doc_codigo, doc_descricao, tpd_id, dcl_codigo);
        }

        /// <summary>
        /// Carrega o grid somente com a página solicitada
        /// </summary>
        /// <param name="doc_id">Filtro por Id de Documento, null para todos</param>
        /// <param name="doc_codigo">Filtro por Código de Documento, vazio para todos</param>
        /// <param name="doc_descricao">Filtro por Descrição de Documento, vazio para todos</param>
        /// <param name="tpd_id">Filtro por Tipo de Documento, vazio para todos</param>
        /// <param name="dcl_codigo">Filtro por Classe de Projeto de Documento, vazio para todos</param>
        /// <param name="start">Número do registro inícial da página</param>
        /// <param name="length">Quantidade de registros por página</param>
        /// <param name="Order_BY">Ordenado por</param>
        /// <returns>List do tipo Documento</returns>
        public List<Documento> LoadData(int? doc_id, string doc_codigo = "", string doc_descricao = "", string tpd_id = "", string dcl_codigo = "", int start = 0, int length = 10,string Order_BY = "")
        {
            return new DocumentoDAO().LoadData(doc_id, doc_codigo, doc_descricao, tpd_id, dcl_codigo, start, length, Order_BY);
        }


        /// <summary>
        /// Dados do Documento selecionado
        /// </summary>
        /// <param name="ID">Id do Documento selecionado</param>
        /// <returns>Documento</returns>
        public Documento Documento_GetbyID(int ID)
        {
            var doc = new DocumentoBLL().Documento_ListAll(ID).FirstOrDefault();

            if (doc.tpd_id != "NC") // NC = nao codificado
            {
                // quebra o codigo para apresentar na tela AB-CDEFGHIJKK-LMN.OPQ-RST-UVW/XYZ-A0
                string codigo = (doc.doc_codigo).Replace(" ", "");

                doc.doc_classe_projeto = codigo.Substring(codigo.LastIndexOf("/") - 3, 3); // UVW
                doc.doc_sequencial = codigo.Substring(codigo.LastIndexOf("/") + 1, 3); // XYZ
                doc.doc_revisao = codigo.Substring(codigo.LastIndexOf("-") + 1); // A0

                // se for Documento Tecnico Especifico --------------------------------------------
                if (codigo.Length > 20)
                {
                    doc.doc_subNivel21 = codigo.Substring(codigo.IndexOf("-") + 1, 9); // CDEFGHIJKK
                    doc.doc_subNivel22a = codigo.Substring(codigo.IndexOf(".") - 3, 3); // LMN
                    doc.doc_subNivel22b = codigo.Substring(codigo.IndexOf(".") + 1, 3); // OPQ
                    doc.doc_subNivel23 = codigo.Substring(codigo.IndexOf("-", 20) + 1, 3); // RST
                }

                doc.lstClasseProjeto = CriaListacmbClasseProjeto(doc.tpd_id); // lista pra preencher o combo Classe

                doc.lstDocClasse = CriaListacmbDocClasse();
            }

            return doc;
        }

        /// <summary>
        /// Insere ou Altera os dados do Documento no Banco 
        /// </summary>
        /// <param name="doc">Dados do Documento</param>
        /// <param name="doc_codigo_filtro">Dados do filtro por Codigo</param>
        /// <param name="doc_descricao_filtro">Dados do filtro por Descrição</param>
        /// <param name="tpd_id_filtro">Dados do filtro por Tipo</param>
        /// <param name="dcl_codigo_filtro">Dados do filtro por Classe</param>
        /// <param name="qt_por_pagina">Quantidade de registros por página</param>
        /// <param name="ordenado_por">Ordenado por</param>
        /// <returns>string</returns>
        public string Documento_Salvar(Documento doc, string doc_codigo_filtro = "", string doc_descricao_filtro = "", string tpd_id_filtro = "", string dcl_codigo_filtro = "",
            int qt_por_pagina = 10, string ordenado_por = "")
        {
            if (doc.doc_caminho == null)
                doc.doc_caminho = "";

            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().Documento_Salvar(doc, paramUsuario.usu_id, paramUsuario.usu_ip,
               doc_codigo_filtro, doc_descricao_filtro, tpd_id_filtro, dcl_codigo_filtro,
               qt_por_pagina, ordenado_por);

        }

        /// <summary>
        /// Ativa/Desativa Documento
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <returns>int</returns>
        public int Documento_AtivarDesativar(int doc_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().Documento_AtivarDesativar(doc_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Excluir (logicamente) Documento
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="usu_id">Id do usuário logado</param>
        /// <param name="ip">IP do usuário logado</param>
        /// <returns>int</returns>
        public int Documento_Excluir(int doc_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().Documento_Excluir(doc_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Lista de todos os Tipos de Documento
        /// </summary>
        /// <param name="tpd_id">Filtro por Tipo de Documento, "" para todos</param>
        /// <param name="tpd_subtipo">Filtro por Subtipo de Documento, null para todos</param>
        /// <returns>Lista de DocTipo</returns>
        public List<DocTipo> DocTipo_ListAll(string tpd_id = "", int? tpd_subtipo = null)
        {
            return new DocumentoDAO().DocTipo_ListAll(tpd_id, tpd_subtipo);
        }




        // *************** CLASSES DE DOCUMENTO  *************************************************************

        /// <summary>
        ///     Lista de todas as Classes de Documentos não deletadas
        /// </summary>
        /// <param name="dcl_id">Filtro por Id da Classe de Documento, null para todos</param>
        /// <returns>Lista da Classe de Documento</returns>
        public List<DocClasse> DocClasse_ListAll(int? dcl_id=null)
        {
            return new DocumentoDAO().DocClasse_ListAll(dcl_id);

        }

        /// <summary>
        /// Dados da Classe de Documento selecionada
        /// </summary>
        /// <param name="ID">Id da Classe de Documento selecionada</param>
        /// <returns>DocClasse</returns>
        public DocClasse DocClasse_GetbyID(int ID)
        {
           return new DocumentoDAO().DocClasse_ListAll(ID).FirstOrDefault();

        }

        /// <summary>
        ///    Insere ou Altera os dados da Classe de Documento
        /// </summary>
        /// <param name="dcl">Classe de Documento</param>
        /// <returns>int</returns>
        public int DocClasse_Salvar(DocClasse dcl)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().DocClasse_Salvar(dcl, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Classe de Documento
        /// </summary>
        /// <param name="dcl_id">Id da Classe do Documento Selecionada</param>
        /// <returns>int</returns>
        public int DocClasse_Excluir(int dcl_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().DocClasse_Excluir(dcl_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Classe de Documento
        /// </summary>
        /// <param name="dcl_id">Id da Classe do Documento Selecionada</param>
        /// <returns>int</returns>
        public int DocClasse_AtivarDesativar(int dcl_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().DocClasse_AtivarDesativar(dcl_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // ************************ OBJETOS DO DOCUMENTO SELECIONADO ***********************************************
        /// <summary>
        /// Lista todos os OBJETOS do Documento selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <returns>Lista de Documento_Objeto</returns>
        public List<Documento_Objeto> Documento_Objetos_ListAll(int doc_id)
        {
            return new DocumentoDAO().Documento_Objetos_ListAll(doc_id);
        }

        /// <summary>
        /// Busca Lista de Objetos 
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="filtro_obj_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtro_obj_descricao">Descrição ou Parte a se localizar</param>
        /// <param name="filtro_clo_id">Id da Classe a se filtrar</param>
        /// <param name="filtro_tip_id">Id do Tipo a se filtrar</param>
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreencheCmbObjetosLocalizados(int doc_id, string filtro_obj_codigo, string filtro_obj_descricao = "", int? filtro_clo_id = -1, int? filtro_tip_id = -1)
        {

            List<Objeto> lstObjetos;
            if (doc_id >=0)
                lstObjetos = new DocumentoBLL().Documento_ObjetosNaoAssociados_ListAll(doc_id, filtro_obj_codigo);
            else
                lstObjetos = new ObjetoDAO().Objeto_ListAll(0, filtro_obj_codigo, filtro_obj_descricao, filtro_clo_id, filtro_tip_id);

            List<SelectListItem> lstListaCmbObjetosLocalizados = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstObjetos)
            {
                string txt = temp.obj_codigo + " (" + temp.obj_descricao + ")";
                lstListaCmbObjetosLocalizados.Add(new SelectListItem() { Text = txt, Value = temp.obj_id.ToString() });
            }

            return lstListaCmbObjetosLocalizados;
        }


        /// <summary>
        ///     Lista de todos os Objetos não associados para o Documento Selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="codObj">Codigo ou parte do Objeto a procurar</param>
        /// <returns>Lista de Objetos Nao Associados</returns>
        public List<Objeto> Documento_ObjetosNaoAssociados_ListAll(int doc_id, string codObj)
        {
            return new DocumentoDAO().Documento_ObjetosNaoAssociados_ListAll(doc_id, codObj);
        }


        /// <summary>
        ///    Associa Documento aos Objetos selecionados
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="obj_ids">Ids dos Objetos Selecionados</param>
        /// <returns>int</returns>
        public int Documento_AssociarObjetos(int doc_id, string obj_ids)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().Documento_AssociarObjetos(doc_id, obj_ids, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///    Desassocia Documento do Objeto selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="obj_id">Id do Objeto Selecionado</param>
        /// <returns>int</returns>
        public int Documento_DesassociarObjeto (int doc_id, int obj_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().Documento_DesassociarObjeto(doc_id, obj_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


// ************************ OSs DO DOCUMENTO SELECIONADO ***********************************************
        /// <summary>
        /// Lista todas as OSs do Documento selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <returns>Lista de Documento_Objeto</returns>
        public List<Documento_OrdemServico> Documento_OrdemServico_ListAll(int doc_id)
        {
            return new DocumentoDAO().Documento_OrdemServico_ListAll(doc_id);
        }


        /// <summary>
        ///     Lista de todas as OSs não associadas para o Documento Selecionado
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="filtroOrdemServico_codigo">Codigo ou parte da OS a procurar</param>
        /// <param name="filtroObj_codigo">Codigo ou parte do Objeto OS a procurar</param>
        /// <param name="filtroTiposOS">Id do tipo de OS a procurar</param>
        /// <returns>Lista de OrdemServico Nao Associadas</returns>
        public List<OrdemServico> Documento_OrdemServicoNaoAssociadas_ListAll(int doc_id, string filtroOrdemServico_codigo, string filtroObj_codigo, int filtroTiposOS)
        {
            return new DocumentoDAO().Documento_OrdemServicoNaoAssociadas_ListAll(doc_id, filtroOrdemServico_codigo, filtroObj_codigo, filtroTiposOS);

        }


        /// <summary>
        ///    Associa Documento às OSs selecionadas
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="ord_ids">Ids das OSs Selecionadas</param>
        /// <returns>int</returns>
        public int Documento_AssociarOrdemServico(int doc_id, string ord_ids)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().Documento_AssociarOrdemServico(doc_id, ord_ids, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///    Desassocia Documento da OS selecionada
        /// </summary>
        /// <param name="doc_id">Id do Documento Selecionado</param>
        /// <param name="ord_id">Id da OS Selecionada</param>
        /// <returns>int</returns>
        public int Documento_DesassociarOrdemServico (int doc_id, int ord_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new DocumentoDAO().Documento_DesassociarOrdemServico(doc_id, ord_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }
       


        /// <summary>
        /// Extrai uma lista de Arquivos e Pastas a partir de um endereço URL de pastas compartilhadas
        /// </summary>
        /// <param name="caminho">Endereço URL</param>
        /// <returns>JsonResult</returns>
        public JsonResult Documento_ListaArquivosWeb(string caminho)
        {
            string CaminhoVirtualRaizArquivos = new ParametroBLL().Parametro_GetValor("CaminhoVirtualRaizArquivos");

            if ((caminho == null) || (caminho == ""))
                caminho = CaminhoVirtualRaizArquivos;

            List<ArquivoWeb> ListaArquivosDiretorios = new Gerais().Documento_ListaArquivosWeb(caminho);

            var ListaDiretorios = ListaArquivosDiretorios.FindAll(x => x.EhArquivo.Equals(false));
            var ListaArquivos = ListaArquivosDiretorios.FindAll(x => x.EhArquivo.Equals(true));

            // remove a linha titulo
            string titulo = ListaDiretorios[0].Texto;
            ListaDiretorios.RemoveAt(0);

            // remove link do item raiz - para nao deixar subir nivel
            if (ListaDiretorios[0].Target.Length < CaminhoVirtualRaizArquivos.Length)
                ListaDiretorios.RemoveAt(0);

            // retorna as listas
            return Json(new {
                titulo = titulo,
                ListaDiretorios = ListaDiretorios,
                ListaArquivos = ListaArquivos
            }, JsonRequestBehavior.AllowGet);
        }



    }
}