using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Web.Mvc;
using System.Drawing;
using WebApp.Helpers;
using System.Net.Mail;

namespace WebApp.Business
{
    /// <summary>
    /// OrdemServicos de Perfis e/ou de Usuários
    /// </summary>
    public class OrdemServicoBLL
    {

        // *************** OrdemServico  *************************************************************
        /// <summary>
        ///  Lista de todas as Ordem de Servico não deletadas
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico a se filtrar</param>
        /// <param name="filtroOrdemServico_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroObj_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroTiposOS">Id do Tipo a se filtrar</param>
        /// <param name="filtroStatusOS">Id do Status a se filtrar</param>
        /// <param name="filtroData">Filtro pelo tipo de Data Selecionado</param>
        /// <param name="filtroord_data_De">Filtro por Data: a de</param>
        /// <param name="filtroord_data_Ate">Filtro por Data: até</param>
        /// <returns>Lista de OrdemServico</returns>
        public List<OrdemServico> OrdemServico_ListAll(int? ord_id = null, string filtroOrdemServico_codigo = null, string filtroObj_codigo = null, int? filtroTiposOS = -1, int? filtroStatusOS = -1, string filtroData = "", string filtroord_data_De = "", string filtroord_data_Ate = "")
        {
            return new OrdemServicoDAO().OrdemServico_ListAll(ord_id, filtroOrdemServico_codigo, filtroObj_codigo, filtroTiposOS,  filtroStatusOS, filtroData, filtroord_data_De, filtroord_data_Ate);
        }

        /// <summary>
        /// Dados da O.S. selecionada
        /// </summary>
        /// <param name="ID">Id da O.S. selecionada</param>
        /// <returns>OSTipo</returns>
        public OrdemServico OrdemServico_GetbyID(int ID)
        {
            return new OrdemServicoDAO().OrdemServico_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Altera os dados da Ordem de Servico no Banco
        /// </summary>
        /// <param name="ord">Ordem de Servico</param>
        /// <returns>int</returns>
        public int OrdemServico_Salvar(OrdemServico ord)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            int retorno = new OrdemServicoDAO().OrdemServico_Salvar(ord, paramUsuario.usu_id, paramUsuario.usu_ip);

            // TODO
            // se retorno > 0 entao verifica se tem que mandar email
            if (retorno > 0) // salvou com sucesso
            {
                if ((ord.sos_codigo == "E") || (ord.sos_id == 14)) // encerrada
                {
                    if ((ord.tos_id == 7) || (ord.tos_id == 8)) //cadastral(7), rotineira(8)
                    {
                        // Sistema notifica a Regional 
                        // os itens de conserva, 
                        // seus quantitativos estimados 
                        // e o prazo de ação sugerido pela política estabelecida
                    }

                    if ((ord.tos_id == 7) || (ord.tos_id == 8) || (ord.tos_id == 9)) //cadastral(7), rotineira(8), especial(9)
                    {
                        // Sistema notifica a Seção de OAE da Sede  
                        // o encerramento da OS
                    }

                    if ((ord.tos_id == 5) || (ord.tos_id == 10) || (ord.tos_id == 13)
                        || (ord.tos_id == 14) || (ord.tos_id == 16) || (ord.tos_id == 17)
                          || (ord.tos_id == 18) || (ord.tos_id == 22) || (ord.tos_id == 23)
                             || (ord.tos_id == 24)
                        ) // ocorrencia(18), extraordinaria(5), reparo(14), reforco(13), monitoramento(10), ensaios(16), levantamento cadastral(17), conserva(22), projeto de oae(24), execucao de obra(23)
                    {
                        //Sistema notifica a Seção de OAE da Sede  
                        // e a Regional
                        // o encerramento da OS
                    }

                    if (ord.tos_id == 18)  // ocorrencia (18)
                    {
                        // Sistema notifica a Seção de OAE da Sede
                        // para verificar a ocorrência
                    }
                }
            }


            return retorno;
        }

        /// <summary>
        ///  Insere os dados da Ordem de Servico no Banco
        /// </summary>
        /// <param name="ord">Ordem de Servico</param>
        /// <returns>int</returns>
        public int OrdemServico_Inserir_Novo(OrdemServico ord)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServico_Inserir_Novo(ord, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///   Busca o proximo sequencial do codigo para o tipo especificado
        /// </summary>
        /// <param name="tos_id">Id do tipo selecionado</param>
        /// <returns>string</returns>
        public string OrdemServico_ProximoCodigo(int tos_id)
        {
            return new OrdemServicoDAO().OrdemServico_ProximoCodigo(tos_id);
        }


        /// <summary>
        ///  Excluir (logicamente) Ordem de Servico
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico Selecionada</param>
        /// <param name="usu_id">Id do Usuário Logado</param>
        /// <param name="ip">IP do Usuário Logado</param>
        /// <returns>int</returns>
        public int OrdemServico_Excluir(int ord_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServico_Excluir(ord_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Ordem de Servico
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico Selecionada</param>
        /// <returns>int</returns>
        public int OrdemServico_AtivarDesativar(int ord_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServico_AtivarDesativar(ord_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///    Busca o valor do campo ord_indicacao_servico
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico</param>
        /// <returns>string</returns>
        public string OrdemServico_Indicacao_Servico_ListAll(int ord_id)
        {
            return new OrdemServicoDAO().OrdemServico_Indicacao_Servico_ListAll(ord_id);

        }

        /// <summary>
        ///    Altera os dados da Aba Indicacao de Servico no Banco
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico Selecionada</param>
        /// <param name="ord_indicacao_servico">Texto do campo Indicaçao de serviço</param>
        /// <returns>int</returns>
        public int OrdemServico_Indicacao_Servico_Salvar(int ord_id, string ord_indicacao_servico)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServico_Indicacao_Servico_Salvar(ord_id, ord_indicacao_servico, paramUsuario.usu_id, paramUsuario.usu_ip);
        }





        /// <summary>
        /// Lista de todos os Documentos Associados à OrdemServico selecionada
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <param name="obj_id">Id do Objeto da Ordem de Serviço selecionada</param>
        /// <param name="somente_referencia">Retornar somente os documentos de referência</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> OrdemServico_Documentos_ListAll(int ord_id, int obj_id, int? somente_referencia = 0)
        {
            return new OrdemServicoDAO().OrdemServico_Documentos_ListAll(ord_id, obj_id, somente_referencia);

        }


        /// <summary>
        /// Lista de todos os Documentos Associados ao Objeto da Ordem de Servico selecionada
        /// </summary>
        /// <param name="ord_id">Id do OrdemServico selecionado</param>
        /// <returns>Lista de Documentos</returns>
        public List<Documento> OrdemServico_Objeto_Documentos_ListAll(int ord_id)
        {
            return new OrdemServicoDAO().OrdemServico_Objeto_Documentos_ListAll(ord_id);

        }



        /// <summary>
        /// Busca Lista de Documentos 
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Serviço selecionada</param>
        /// <param name="codDoc">Código ou Parte a se localizar</param>
        /// <returns>List(SelectListItem)</returns>
        public List<SelectListItem> PreencheCmbDocumentosLocalizados(int ord_id, string codDoc)
        {
            List<Documento> lstDocumentos = new OrdemServicoDAO().OrdemServico_DocumentosNaoAssociados_ListAll(ord_id, codDoc);
            List<SelectListItem> lstListaCmbDocumentosLocalizados = new List<SelectListItem>(); // lista de combo
            foreach (var temp in lstDocumentos)
            {
                string txt = temp.doc_codigo + "-" + temp.doc_descricao;
                lstListaCmbDocumentosLocalizados.Add(new SelectListItem() { Text = txt, Value = temp.doc_id.ToString() });
            }

            int total = lstDocumentos.Count > 0 ? lstDocumentos[0].total_registros : 0;
            lstListaCmbDocumentosLocalizados.Insert(0, new SelectListItem() { Text = "total_registros", Value = total.ToString() });

            return lstListaCmbDocumentosLocalizados;
        }

        /// <summary>
        ///    Associa Documentos de Referência à Ordem de Serviço selecionada
        /// </summary>
        /// <param name="doc_id">Ids do Documentos selecionado</param>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <param name="dos_referencia">Zero para associar; Um para desassociar</param>
        /// <returns>int</returns>
        public int OrdemServico_AssociarDocumentosReferencia(int doc_id, int ord_id, int dos_referencia)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServico_AssociarDocumentosReferencia(doc_id, ord_id, dos_referencia, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///    Associa Documentos à Ordem de Serviço selecionada
        /// </summary>
        /// <param name="doc_ids">Ids dos Documentos selecionados</param>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <returns>int</returns>
        public int OrdemServico_AssociarDocumentos(string doc_ids, int ord_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServico_AssociarDocumentos(doc_ids, ord_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///    Desassocia Documentos da Ordem de Serviço selecionada
        /// </summary>
        /// <param name="doc_id">Id do Documento selecionado</param>
        /// <param name="ord_id">Id da OrdemServico selecionada</param>
        /// <returns>int</returns>
        public int OrdemServico_DesassociarDocumento(int doc_id, int ord_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServico_DesassociarDocumento(doc_id, ord_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }







        // *************** Classe Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista de todos as Classes de OS não deletadas
        /// </summary>
        /// <returns>Lista de OSClasse</returns>
        public List<OSClasse> OSClasse_ListAll()
        {
            return new OrdemServicoDAO().OSClasse_ListAll();
        }

        /// <summary>
        /// Dados da Classe de O.S. selecionado
        /// </summary>
        /// <param name="ID">Id da Classe selecionada</param>
        /// <returns>OSClasse</returns>
        public OSClasse OSClasse_GetbyID(int ID)
        {
            return new OrdemServicoDAO().OSClasse_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Insere ou Altera os dados da Classe de Ordem de Servico no Banco
        /// </summary>
        /// <param name="ocl">Classe da Ordem de Servico</param>
        /// <returns>int</returns>
        public int OSClasse_Salvar(OSClasse ocl)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSClasse_Salvar(ocl, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Classe de Ordem de Servico
        /// </summary>
        /// <param name="ocl_id">Id da Classe de Ordem de Serviço Selecionada</param>
        /// <returns>int</returns>
        public int OSClasse_Excluir(int ocl_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSClasse_Excluir(ocl_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Ordem de Servico
        /// </summary>
        /// <param name="ocl_id">Id da Classe de Ordem de Servico Selecionado</param>
        /// <returns>int</returns>
        public int OSClasse_AtivarDesativar(int ocl_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSClasse_AtivarDesativar(ocl_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///     Lista de todos as Classes para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreencheCmbClassesOS()
        {
            List<OSClasse> lst = new OrdemServicoDAO().OSClasse_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.ocl_descricao + " (" + temp.ocl_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.ocl_id.ToString() });
            }

            return lstSaida;
        }



        // *************** Tipo Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista de todos os Tipos de OS não deletadas
        /// </summary>
        /// <returns>Lista de OSTipo</returns>
        public List<OSTipo>OSTipo_ListAll()
        {
            return new OrdemServicoDAO().OSTipo_ListAll();
        }

        /// <summary>
        /// Dados do Tipo de O.S. selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo selecionado</param>
        /// <returns>OSTipo</returns>
        public OSTipo OSTipo_GetbyID(int ID)
        {
            return new OrdemServicoDAO().OSTipo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Insere ou Altera os dados do Tipo de Ordem de Servico no Banco
        /// </summary>
        /// <param name="tos">Tipo da Ordem de Servico</param>
        /// <returns>int</returns>
        public int OSTipo_Salvar(OSTipo tos)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSTipo_Salvar(tos, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Tipo de Ordem de Servico
        /// </summary>
        /// <param name="tos_id">Id do Tipo de Ordem de Serviço Selecionada</param>
        /// <returns>int</returns>
        public int OSTipo_Excluir(int tos_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSTipo_Excluir(tos_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Ordem de Servico
        /// </summary>
        /// <param name="tos_id">Id do Tipo de Ordem de Servico Selecionado</param>
        /// <returns>int</returns>
        public int OSTipo_AtivarDesativar(int tos_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSTipo_AtivarDesativar(tos_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///     Lista de todos os Tipos para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreencheCmbTiposOS()
        {
            List<OSTipo> lst = new OrdemServicoDAO().OSTipo_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.tos_descricao + " (" + temp.tos_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.tos_id.ToString() });
            }

            return lstSaida;
        }


        // *************** Status Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista de todos os Status de OS não deletados
        /// </summary>
        /// <returns>Lista de OSStatus</returns>
        public List<OSStatus>OSStatus_ListAll()
        {
            return new OrdemServicoDAO().OSStatus_ListAll();
        }

        /// <summary>
        /// Dados do Status de O.S. selecionado
        /// </summary>
        /// <param name="ID">Id do Status selecionado</param>
        /// <returns>OSStatus</returns>
        public OSStatus OSStatus_GetbyID(int ID)
        {
            return new OrdemServicoDAO().OSStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Insere ou Altera os dados do Status de Ordem de Servico no Banco
        /// </summary>
        /// <param name="sos">Status da Ordem de Servico</param>
        /// <returns>int</returns>
        public int OSStatus_Salvar(OSStatus sos)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSStatus_Salvar(sos, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Status de Ordem de Servico
        /// </summary>
        /// <param name="sos_id">Id do Status de Ordem de Serviço Selecionada</param>
        /// <returns>int</returns>
        public int OSStatus_Excluir(int sos_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSStatus_Excluir(sos_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Status da Ordem de Servico
        /// </summary>
        /// <param name="sos_id">Id do Status de Ordem de Servico Selecionado</param>
        /// <returns>int</returns>
        public int OSStatus_AtivarDesativar(int sos_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSStatus_AtivarDesativar(sos_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista de todos os Status para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreencheCmbStatusOS()
        {
            List<OSStatus> lst = new OrdemServicoDAO().OSStatus_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.sos_descricao + " (" + temp.sos_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.sos_id.ToString() });
            }

            return lstSaida;
        }


        // *************** FLUXO DE Status Ordem Servico  *************************************************************

        /// <summary>
        ///     Lista de todos os Fluxos de  Status de OS não deletados
        /// </summary>
        /// <param name="tos_id">Id do Tipo de Ordem de Servico</param>
        /// <returns>Lista de OSFluxoStatus</returns>
        public List<OSFluxoStatus>OSFluxoStatus_ListAll(int tos_id)
        {
            return new OrdemServicoDAO().OSFluxoStatus_ListAll(null, tos_id);
        }

        /// <summary>
        /// Dados do Fluxo de Status de O.S. selecionado
        /// </summary>
        /// <param name="ID">Id do Fluxo de Status selecionado</param>
        /// <returns>OSFluxoStatus</returns>
        public OSFluxoStatus OSFluxoStatus_GetbyID(int ID)
        {
            return new OrdemServicoDAO().OSFluxoStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Insere ou Altera os dados do Fluxo de Status de Ordem de Servico no Banco
        /// </summary>
        /// <param name="fos">Fluxo de Status da Ordem de Servico</param>
        /// <returns>int</returns>
        public int OSFluxoStatus_Salvar(OSFluxoStatus fos)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSFluxoStatus_Salvar(fos, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Fluxo de Status de Ordem de Servico
        /// </summary>
        /// <param name="fos_id">Id do Fluxo de Status de Ordem de Serviço Selecionada</param>
        /// <returns>int</returns>
        public int OSFluxoStatus_Excluir(int fos_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSFluxoStatus_Excluir(fos_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Fluxo de Status Ordem de Servico
        /// </summary>
        /// <param name="fos_id">Id do Fluxo de Status de Ordem de Servico Selecionado</param>
        /// <returns>int</returns>
        public int OSFluxoStatus_AtivarDesativar(int fos_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OSFluxoStatus_AtivarDesativar(fos_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista de todos os Status para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> preencheCmbStatus()
        {
            List<OSStatus> lst = new OrdemServicoDAO().OSStatus_ListAll();
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.sos_descricao + " (" + temp.sos_codigo + ")" ;
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.sos_id.ToString() });
            }

            lstSaida.Sort((x, y) => x.Text.CompareTo(y.Text));

            lstSaida.Insert(0, new SelectListItem() { Text = "-- Selecione --", Value = "", Disabled = true });
            return lstSaida;
        }


        // *************** FICHA DE NOTIFICACAO DE OCORRENCIAS  *************************************************************

        /// <summary>
        ///  Envia Email de Notificacao
        /// </summary>
        /// <param name="lstDestinatarios">Lista de Destinatarios separada por ponto e virgula</param>
        /// <param name="TextoEmail">Texto do Email</param>
        /// <returns>string</returns>
        public string FichaNotificacao_EnviarEmail(string lstDestinatarios, string TextoEmail)
        {
                ParamsEmail pEmail = new ParametroBLL().Parametro_ListAllParamsEmail()[0];

                pEmail.Para = lstDestinatarios;
                pEmail.Assunto = "Notificação de Ocorrência";


                // substitui parametros
                pEmail.Texto = TextoEmail;


                // envia o email
                AlternateView av1 = null;
                if (pEmail.IsBodyHtml)
                    av1 = AlternateView.CreateAlternateViewFromString(pEmail.Texto, null, "text/html");

            return new Gerais().MandaEmail(av1, pEmail);
        }



        // *************** Ordem Servico  DE REPARO *************************************************************

        /// <summary>
        /// Lista dos Itens da Ordens de Servico de Reparo selecionada
        /// </summary>
        /// <param name="ord_id">Id da Ordem de Servico a se filtrar</param>
        /// <returns>Lista de OrcamentoDetalhes</returns>
        public List<OrcamentoDetalhes> OrdemServicoReparo_ListAll(int ord_id)
        {
            return new OrdemServicoDAO().OrdemServicoReparo_ListAll(ord_id);
        }

        /// <summary>
        /// Busca as O.Ss de Reparo criadas a partir da O.S. de Orçamento
        /// </summary>
        /// <param name="ord_id">Id da O.S. de Orçamento</param>
        /// <returns>string</returns>
        public string ConcatenaOSReparo(int ord_id)
        {
            return new OrdemServicoDAO().ConcatenaOSReparo(ord_id);
        }


        /// <summary>
        ///  Altera Status de Item de Reparo  Ordem de Servico
        /// </summary>
        /// <param name="ore_id">Id do Reparo Selecionado</param>
        /// <param name="ast_id">Id do Status do Reparo Selecionado</param>
        /// <returns>int</returns>
        public int OrdemServicoReparoItem_Status(int ore_id, int ast_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServicoReparoItem_Status(ore_id, ast_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///  Altera Status dos Itens Nao Reparados da Ordem de Servico
        /// </summary>
        /// <param name="ord_id">Id do Reparo Selecionado</param>
        /// <returns>int</returns>
        public int OrdemServicoReparo_Atualiza_Itens_NaoReparados(int ord_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrdemServicoDAO().OrdemServicoReparo_Atualiza_Itens_NaoReparados(ord_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


    }
}