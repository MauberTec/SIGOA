using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.Web.Mvc;
using WebApp.Helpers;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace WebApp.Business
{
    /// <summary>
    /// OrcamentoLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class OrcamentoBLL
    {
        /// <summary>
        ///  Lista de todos os Orcamentos não deletados
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="filtroRodovia">Filtro por Rodovia</param>
        /// <param name="filtroObjetos">Filtro por Objeto</param>
        /// <param name="filtroStatus">Filtro por Status</param>
        /// <param name="orc_ativo">Filtro por Ativo/Inativo</param>
        /// <param name="FiltroidRodovias">Filtro por id de Rodovias</param>
        /// <param name="FiltroidObjetos">Filtro por id de Objetos</param>
        /// <returns>Lista de OrcamentoStatus</returns>
        public List<Orcamento> Orcamento_ListAll(int? orc_id = null, string filtroRodovia = "", string filtroObjetos = "", int? filtroStatus = -1, int? orc_ativo = 2,
           string FiltroidRodovias = "", string FiltroidObjetos = "")
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_ListAll(orc_id, filtroRodovia, filtroObjetos, filtroStatus, orc_ativo
                , FiltroidRodovias, FiltroidObjetos, paramUsuario.usu_id);
        }


        /// <summary>
        /// Busca o proximo sequencial de Orcamento
        /// </summary>
        /// <returns>string</returns>
        public string Orcamento_ProximoSeq()
        {
            return new OrcamentoDAO().Orcamento_ProximoSeq();
        }


        /// <summary>
        ///  Excluir Orcamento (logicamente) 
        /// </summary>
        /// <param name="sta_id">Id do  Selecionado</param>
        /// <returns>int</returns>
        public int Orcamento_Excluir(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Excluir(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Orcamento
        /// </summary>
        /// <param name="sta_id">Id do  Selecionado</param>
        /// <returns>int</returns>
        public int Orcamento_AtivarDesativar(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_AtivarDesativar(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Orcamento
        /// </summary>
        /// <param name="orc">Dados do Orcamento</param>
        /// <returns>int</returns>
        public int Orcamento_Salvar(Orcamento orc)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Salvar(orc, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        /// <summary>
        ///    Clona os dados do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orcamento a ser clonado</param>
        /// <returns>int</returns>
        public int Orcamento_Clonar(int orc_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Clonar(orc_id, paramUsuario.usu_id, paramUsuario.usu_ip);

        }



        /// <summary>
        ///     Lista os Status para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> PreencheCmbStatusOrcamento()
        {
            List<OrcamentoStatus> lst = new OrcamentoDAO().OrcamentoStatus_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.ocs_descricao + " (" + temp.ocs_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.ocs_id.ToString() });
            }

            return lstSaida;
        }



        // *************** ORCAMENTO_DETALHES  *************************************************************

        /// <summary>
        ///     Lista dos Detalhes do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="ore_ativo">Filtro por Elemento Ativo</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<OrcamentoDetalhes> OrcamentoDetalhes_ListAll(int orc_id, int ore_ativo)
        {
             return new OrcamentoDAO().OrcamentoDetalhes_ListAll(orc_id, ore_ativo);
       }

        /// <summary>
        ///  Ativa/Desativa Orcamento
        /// </summary>
        /// <param name="ore_id">Id do Reparo Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoDetalhes_AtivarDesativar(int ore_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoDetalhes_AtivarDesativar(ore_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        /// <summary>
        ///     Lista dos Serviços Adicionais por Objeto do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do Objeto que contém o serviço</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<ServicosAdicionados> Orcamento_Servicos_Adicionados_ListAll(int orc_id, int obj_id)
        {
            return new OrcamentoDAO().Orcamento_Servicos_Adicionados_ListAll(orc_id, obj_id);
        }


        /// <summary>
        ///  Excluir (logicamente) Serviço
        /// </summary>
        /// <param name="id">Id do Serviço Selecionado</param>
        /// <returns>int</returns>
        public int Orcamento_Servicos_Adicionados_Excluir(int id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Servicos_Adicionados_Excluir(id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="ids_retorno">Lista dos ids alterados</param>
        /// <param name="valores_retorno">Lista dos valores alterados</param>
        /// <returns>int</returns>
        public int Orcamento_ServicosAdicionados_Salvar(string ids_retorno, string valores_retorno)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_ServicosAdicionados_Salvar(ids_retorno, valores_retorno, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista das TPUs a serem adicionadas
        /// </summary>
        /// <param name="orc_id">Id do orçamento</param>
        /// <param name="obj_id">Id do objeto do orcamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <param name="mes">Mês</param>
        /// <param name="ano">Ano</param>
        /// <param name="desonerado">Desonerado</param>
        /// <returns>Lista de Detalhes do Orcamento</returns>
        public List<ServicosAdicionados> OrcamentoServicosAdicionadosTPUs_ListAll(int orc_id, int obj_id, int ose_fase, int mes, int ano, string desonerado)
        {
            // checa se a tabela DER.TPUs esta sincronizada
            List<tpu> listaTPU = new IntegracaoDAO().DER_SincronizarTPUs(0, ano.ToString(), ose_fase.ToString(), mes.ToString(), "", "");
           // if ((listaTPU.Count == 1) && (listaTPU[0].CodSubItem != "-1"))
           // {
                return new OrcamentoDAO().OrcamentoServicosAdicionadosTPUs_ListAll(orc_id, obj_id, ose_fase);
            //}
        }



        /// <summary>
        ///  Salvar Serviços Adicionais
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <param name="obj_id">Id do Objeto do Orçamento</param>
        /// <param name="ose_fase">Fase da TPU</param>
        /// <param name="ose_codigo_der">Código do Serviço da TPU</param>
        /// <param name="ose_quantidade">Quantidade a ser utilizada</param>
        /// <returns>int</returns>
        public int Orcamento_Adicionar_Servico(int orc_id, int obj_id, int ose_fase, string ose_codigo_der, decimal ose_quantidade)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().Orcamento_Adicionar_Servico(orc_id, obj_id, ose_fase, ose_codigo_der, ose_quantidade, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// Calcula o Valor Total do Orcamento
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <returns>decimal</returns>
        public decimal Orcamento_Total(int orc_id)
        {
            return new OrcamentoDAO().Orcamento_Total(orc_id);
        }



        /// <summary>
        /// Preenche Orcamento em Excel e disponibiliza para download
        /// </summary>
        /// <param name="orc_id">Id do Orçamento</param>
        /// <returns>string</returns>
        public string Orcamento_ExportarXLS(int orc_id)
        {

            string arquivo_modelo_caminhoFull = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Modelo_Orcamento.xlsx");
            string arquivo_saida = "Orcamento_" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "") + ".xlsx";
            string arquivo_saida_caminhoFull = System.Web.HttpContext.Current.Server.MapPath("~/temp/") + "/" + arquivo_saida;

            string arquivo_saida_caminho_virtual = HttpContext.Current.Request.Url.Host + "/temp/" + arquivo_saida;
            string saida = "";

            List<string> Headers = new List<string>();

            int nLinhaVisivel = 99;
            int ultimaLinha = 352;
            int desloc_linha = 131;

            Gerais ger = new Gerais();
            try
            {
                // apaga arquivos antigos da pasta temp
                new ObjetoBLL().limpaArquivosAntigos();

                File.Copy(arquivo_modelo_caminhoFull, arquivo_saida_caminhoFull);


                // faz busca os DADOS no banco
                List<OrcamentoDetalhes> lstOrcamentoDetalhes = new OrcamentoDAO().OrcamentoDetalhes_ListAll(orc_id, 1);
                List<ServicosAdicionados> lstOrcamento_Servicos_Adicionados = new OrcamentoDAO().Orcamento_Servicos_Adicionados_ListAll(orc_id, 0);

                // Abre a planilha para edicao
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(arquivo_saida_caminhoFull, true))
                {
                    // LEITURA DA PLANILHA
                    Worksheet worksheet = ger.GetWorksheet(doc, "Orcamento");

                    // ======= PREENCHE OS DADOS ===============================================
                    string valor = "";
                    decimal qtAdotadaValor = -1;

                    // pega o estilo da planilha estilos: borda direita preta e borda direita +fonte vermelha
                    Worksheet worksheet_estilos = ger.GetWorksheet(doc, "estilos");
                    Cell cell_BordaDireita = ger.GetCell(worksheet_estilos, "B", Convert.ToUInt32(1));
                    Cell cell_BordaDireita_Fonte_Vermelha = ger.GetCell(worksheet_estilos, "B", Convert.ToUInt32(2));
                    Cell cell_Fonte_Vermelha_Centralizado = ger.GetCell(worksheet_estilos, "B", Convert.ToUInt32(3));
                    Cell cell_BordaDireita_Centralizado = ger.GetCell(worksheet_estilos, "B", Convert.ToUInt32(4));
                    Cell cell_BordaDireita_Fonte_Vermelha_Centralizado = ger.GetCell(worksheet_estilos, "B", Convert.ToUInt32(5));
                    Cell cell_Fonte_Vermelha_Direita = ger.GetCell(worksheet_estilos, "B", Convert.ToUInt32(6));
                    Cell cell_Borda_Superior = ger.GetCell(worksheet_estilos, "B", Convert.ToUInt32(7));
                   

                    // preenchimento do grid Tabelao
                    for (int li = 0; li < lstOrcamentoDetalhes.Count; li++)
                    {
                        // TABELA HEADER COM OS DADOS DA 1A LINHA
                        if (li == 0)
                        {
                            for (int li2 = 4; li2 <= 11; li2++)
                            {
                                Cell cell = ger.GetCell(worksheet, "B", Convert.ToUInt32(li2));
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

                                switch (li2)
                                {
                                    case 4: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].orc_cod_orcamento)); break;
                                    case 5: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].orc_versao)); break;
                                    case 6: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].orc_descricao)); break;
                                    case 7: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].ocs_descricao)); break;
                                    case 8: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].orc_objetos_associados)); break;
                                    case 9: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].orc_data_validade)); break;
                                    case 10: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].orc_data_base)); break;
                                    case 11: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[0].orc_ativo)); break;
                                }
                            }
                            Cell cellTitulo = ger.GetCell(worksheet, "A", Convert.ToUInt32(2));
                            cellTitulo.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                            cellTitulo.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString("Orçamento " + lstOrcamentoDetalhes[0].orc_cod_orcamento + " versão " + lstOrcamentoDetalhes[0].orc_versao.ToString()));

                            Cell cell1 = ger.GetCell(worksheet, "D", Convert.ToUInt32(124));
                            cell1.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                            cell1.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamentoDetalhes[0].valor_total_adotado)));

                            Cell cell2 = ger.GetCell(worksheet, "D", Convert.ToUInt32(126));
                            cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                            cell2.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamentoDetalhes[0].valor_total_sugerido)));
                        }


                        // VARRE as COLUNAS A até X para preenchimento do tabelao
                        for (int col = 65; col <= 88; col++)
                        {
                            Cell cell = ger.GetCell(worksheet, ((char)col).ToString(), Convert.ToUInt32(li + desloc_linha));
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

                            // coloca os valores
                            switch (col)
                            {
                                case 65: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].obj_codigoOAE)); break;
                                case 67: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].obj_codigoElemento)); break;
                                case 69: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].ian_numero)); break;
                                case 70: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].atp_codigo)); break;
                                case 71: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].leg_codigo)); break;
                                case 72: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].ale_codigo)); break;
                                case 73: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].aca_codigo)); break;

                                case 74:
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.StyleIndex = cell_BordaDireita_Centralizado.StyleIndex;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade)));
                                    break;

                                case 75: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].rpt_id_sugerido_codigo)); break;

                                case 76:
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_sugerida)));
                                    break;

                                case 77: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamentoDetalhes[li].rpt_id_sugerido_unidade)); break;
                                case 78:
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamentoDetalhes[li].rtu_preco_unitario_sugerido)));
                                    break;

                                case 80:
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.StyleIndex = cell_BordaDireita.StyleIndex;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamentoDetalhes[li].rtu_valor_total_linha_sugerido)));
                                    break;

                                case 81:
                                    cell.StyleIndex = cell_BordaDireita.StyleIndex;
                                    break;

                                case 82:
                                    valor = lstOrcamentoDetalhes[li].rpt_id_adotado_codigo;
                                    qtAdotadaValor = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_adotada);
                                    if (qtAdotadaValor == 0)
                                        valor = lstOrcamentoDetalhes[li].rpt_id_sugerido_codigo;

                                    if (lstOrcamentoDetalhes[li].rpt_id_adotado_codigo != lstOrcamentoDetalhes[li].rpt_id_sugerido_codigo)
                                        cell.StyleIndex = cell_Fonte_Vermelha_Centralizado.StyleIndex;

                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(valor));
                                    break;

                                case 83:
                                    decimal valor1 = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_adotada);
                                    qtAdotadaValor = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_adotada);
                                    if (qtAdotadaValor == 0)
                                        valor1 = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_sugerida);

                                    if (lstOrcamentoDetalhes[li].ian_quantidade_adotada != lstOrcamentoDetalhes[li].ian_quantidade_sugerida)
                                        cell.StyleIndex = cell_Fonte_Vermelha_Centralizado.StyleIndex;

                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(valor1)));
                                    break;

                                case 84:
                                    valor = lstOrcamentoDetalhes[li].rpt_id_adotado_unidade;
                                    qtAdotadaValor = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_adotada);
                                    if (qtAdotadaValor == 0)
                                        valor = lstOrcamentoDetalhes[li].rpt_id_sugerido_unidade;

                                    if (lstOrcamentoDetalhes[li].rpt_id_adotado_unidade != lstOrcamentoDetalhes[li].rpt_id_sugerido_unidade)
                                        cell.StyleIndex = cell_Fonte_Vermelha_Centralizado.StyleIndex;

                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(valor));
                                    break;

                                case 85:
                                    decimal valor2 = Convert.ToDecimal(lstOrcamentoDetalhes[li].rtu_preco_unitario_adotado);
                                    qtAdotadaValor = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_adotada);
                                    if (qtAdotadaValor == 0)
                                       valor2 = Convert.ToDecimal(lstOrcamentoDetalhes[li].rtu_preco_unitario_sugerido);

                                    if (lstOrcamentoDetalhes[li].rtu_preco_unitario_adotado != lstOrcamentoDetalhes[li].rtu_preco_unitario_sugerido)
                                        cell.StyleIndex = cell_Fonte_Vermelha_Direita.StyleIndex;

                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(valor2)));
                                    break;

                                case 87:
                                    decimal valor3 = Convert.ToDecimal(lstOrcamentoDetalhes[li].rtu_valor_total_linha_adotado);
                                    qtAdotadaValor = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_adotada);
                                    if (qtAdotadaValor == 0)
                                        valor3 = Convert.ToDecimal(lstOrcamentoDetalhes[li].rtu_valor_total_linha_sugerido);

                                    if (lstOrcamentoDetalhes[li].rtu_valor_total_linha_adotado != lstOrcamentoDetalhes[li].rtu_valor_total_linha_sugerido)
                                        cell.StyleIndex = cell_Fonte_Vermelha_Direita.StyleIndex;

                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(valor3)));
                                    break;

                                case 88:
                                    qtAdotadaValor = Convert.ToDecimal(lstOrcamentoDetalhes[li].ian_quantidade_adotada);
                                    if (qtAdotadaValor == 0)
                                        cell.StyleIndex = cell_Fonte_Vermelha_Direita.StyleIndex;
                                    break;
                            }
                        }
                    }

                    // coloca borda inferior na ultima linha
                    for (int col = 65; col <= 88; col++)
                    {
                            Cell cell = ger.GetCell(worksheet, ((char)col).ToString(), Convert.ToUInt32(lstOrcamentoDetalhes.Count + desloc_linha));
                            cell.StyleIndex = cell_Borda_Superior.StyleIndex;
                    }


          // **** PREENCHIMENTO DO GRID SERVICOS ADICIONADOS *********************************************
                    desloc_linha = 19;
                    for (int li = 0; li < lstOrcamento_Servicos_Adicionados.Count; li++)
                    {
                        for (int col = 65; col <= 78; col++) // VARRE as COLUNAS A até N
                        {
                            Cell cell = ger.GetCell(worksheet, ((char)col).ToString(), Convert.ToUInt32(li + desloc_linha));
                            cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;

                            // coloca os valores
                            switch  (col)
                            {
                                case 65: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamento_Servicos_Adicionados[li].obj_codigo));break;
                                case 67: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamento_Servicos_Adicionados[li].ose_quantidade));break;
                                case 68: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamento_Servicos_Adicionados[li].UnidMed)); break;
                                case 69: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamento_Servicos_Adicionados[li].CodSubItem)); break;
                                case 70: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamento_Servicos_Adicionados[li].NomeSubItem)); break;
                                case 74: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstOrcamento_Servicos_Adicionados[li].ose_fase)); break;
                                case 75:
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamento_Servicos_Adicionados[li].PrecoUnit)));
                                    break;

                                case 77:
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamento_Servicos_Adicionados[li].valor_total_linha)));
                                    break;
                            }
                        }
                    }

                    // coloca borda inferior na ultima linha
                    for (int col = 65; col <= 78; col++)
                    {
                        Cell cell = ger.GetCell(worksheet, ((char)col).ToString(), Convert.ToUInt32(lstOrcamento_Servicos_Adicionados.Count + desloc_linha));
                        cell.StyleIndex = cell_Borda_Superior.StyleIndex;
                    }


                    // oculta as linhas em branco a mais
                    for (int k = lstOrcamento_Servicos_Adicionados.Count + desloc_linha + 1; k < 123; k++)
                    {
                        Row refRow = ger.GetRow(worksheet, Convert.ToUInt16(k));
                        refRow.Hidden = new BooleanValue(true); // oculta a linha 
                    }


                    // prenche os totais dos servicos adicionados
                    if (lstOrcamento_Servicos_Adicionados.Count > 0)
                    {
                        decimal valorGeral = Convert.ToDecimal(lstOrcamento_Servicos_Adicionados[0].valor_total) +  Convert.ToDecimal(lstOrcamentoDetalhes[0].valor_total_adotado);

                        Cell cellTitulo1 = ger.GetCell(worksheet, "B", Convert.ToUInt32(13));
                        cellTitulo1.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                        cellTitulo1.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(valorGeral)));

                        Cell cellTitulo2 = ger.GetCell(worksheet, "D", Convert.ToUInt32(15));
                        cellTitulo2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                        cellTitulo2.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstOrcamento_Servicos_Adicionados[0].valor_total)));
                    }


                    // fecha o arquivo e retorna
                    doc.Save();
                    doc.Close();
                }

                return arquivo_saida;
            }
            catch (Exception ex)
            {
                saida = "erro:" + ex.ToString();
            }

            return "";
        }



        // *************** STATUS  *************************************************************

        /// <summary>
        ///  Lista de todos os Status não deletados
        /// </summary>
        /// <param name="sta_id">Filtro por Id do Status, null para todos</param>
        /// <returns>Lista de OrcamentoStatus</returns>
        public List<OrcamentoStatus> OrcamentoStatus_ListAll(int? sta_id = null)
        {
            return new OrcamentoDAO().OrcamentoStatus_ListAll(sta_id);
        }

        /// <summary>
        /// Dados do Status selecionado
        /// </summary>
        /// <param name="ID">Id do Status selecionado</param>
        /// <returns>OrcamentoStatus</returns>
        public OrcamentoStatus OrcamentoStatus_GetbyID(int ID)
        {
            return  new OrcamentoDAO().OrcamentoStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Status
        /// </summary>
        /// <param name="sta_id">Id do Status Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Excluir(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoStatus_Excluir(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Status
        /// </summary>
        /// <param name="sta_id">Id do Status Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_AtivarDesativar(int sta_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoStatus_AtivarDesativar(sta_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Status
        /// </summary>
        /// <param name="sta">Dados do Status</param>
        /// <returns>int</returns>
        public int OrcamentoStatus_Salvar(OrcamentoStatus sta)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoStatus_Salvar(sta, paramUsuario.usu_id, paramUsuario.usu_ip);
        }



        // *************** FLUXO DE STATUS DE Orcamento  *************************************************************

        /// <summary>
        ///     Lista de todos os Fluxos de  Status não deletados
        /// </summary>
        /// <returns>Lista de OrcamentoFluxoStatus</returns>
        public List<OrcamentoFluxoStatus> OrcamentoFluxoStatus_ListAll()
        {
            return new OrcamentoDAO().OrcamentoFluxoStatus_ListAll();
        }

        /// <summary>
        /// Dados do Fluxo de Status selecionado
        /// </summary>
        /// <param name="ID">Id do Fluxo de Status selecionado</param>
        /// <returns>OrcamentoFluxoStatus</returns>
        public OrcamentoFluxoStatus OrcamentoFluxoStatus_GetbyID(int ID)
        {
            return new OrcamentoDAO().OrcamentoFluxoStatus_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///    Insere ou Altera os dados do Fluxo de Status 
        /// </summary>
        /// <param name="ocf">Fluxo de Status</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Salvar(OrcamentoFluxoStatus ocf)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoFluxoStatus_Salvar(ocf, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Excluir (logicamente) Fluxo de Status
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status  Selecionada</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_Excluir(int ocf_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoFluxoStatus_Excluir(ocf_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Fluxo de Status
        /// </summary>
        /// <param name="ocf_id">Id do Fluxo de Status Selecionado</param>
        /// <returns>int</returns>
        public int OrcamentoFluxoStatus_AtivarDesativar(int ocf_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new OrcamentoDAO().OrcamentoFluxoStatus_AtivarDesativar(ocf_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///     Lista de todos os Status para preenchimento de combo
        /// </summary>
        /// <returns>Lista de SelectListItem</returns>
        public List<SelectListItem> preencheCmbStatus()
        {
            List<OrcamentoStatus> lst = new OrcamentoDAO().OrcamentoStatus_ListAll(null);
            List<SelectListItem> lstSaida = new List<SelectListItem>(); // lista de combo

            foreach (var temp in lst)
            {
                string txt = temp.ocs_descricao + " (" + temp.ocs_codigo + ")";
                lstSaida.Add(new SelectListItem() { Text = txt, Value = temp.ocs_id.ToString() });
            }

            return lstSaida;
        }



    }
}