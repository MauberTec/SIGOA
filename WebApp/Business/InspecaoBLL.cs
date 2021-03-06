﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DAO;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using WebApp.Helpers;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;

namespace WebApp.Business
{
    /// <summary>
    /// AnomLegendas de Perfis e/ou de Usuários
    /// </summary>
    public class InspecaoBLL
    {

        // *************** INSPECOES  *************************************************************

        /// <summary>
        ///  Lista de toda as Inspecoes não deletadas
        /// </summary>
        /// <param name="ins_id">Filtro por Id do Tipo, null para todos</param>
        /// <param name="filtroOrdemServico_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroObj_codigo">Código ou Parte a se localizar</param>
        /// <param name="filtroTiposOS">Id do Tipo a se filtrar</param>
        /// <param name="filtroStatusOS">Id do Status a se filtrar</param>
        /// <param name="filtroData">Filtro pelo tipo de Data Selecionado</param>
        /// <param name="filtroord_data_De">Filtro por Data: a de</param>
        /// <param name="filtroord_data_Ate">Filtro por Data: até</param>
        /// <returns>Lista de Inspecao</returns>
        public List<Inspecao> Inspecao_ListAll(int ins_id, string filtroOrdemServico_codigo = null, string filtroObj_codigo = null, int? filtroTiposOS = -1, int? filtroStatusOS = -1, string filtroData = "", string filtroord_data_De = "", string filtroord_data_Ate = "")
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().Inspecao_ListAll(ins_id, filtroOrdemServico_codigo, filtroObj_codigo, filtroTiposOS, filtroStatusOS, filtroData, filtroord_data_De, filtroord_data_Ate, paramUsuario.usu_id);
        }

        /// <summary>
        /// Lista os Atributos do Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>Lista de ObjAtributoValores</returns>
        public List<ObjAtributoValores> InspecaoAtributosValores_ListAll(int ord_id)
        {
            return new InspecaoDAO().InspecaoAtributosValores_ListAll(ord_id);
        }

        /// <summary>
        ///  Salva os Valores dos Atributos  no Banco
        /// </summary>
        /// <param name="ObjAtributoValor">Valor do Atributo</param>
        /// <param name="codigoOAE">Código da Obra de Arte</param>
        /// <param name="selidTipoOAE">Id do Tipo de Obra de Arte</param>
        /// <param name="ord_id">Id da Ordem de Serviço</param>
        /// <returns>int</returns>
        public int InspecaoAtributoValores_Salvar(ObjAtributoValores ObjAtributoValor, string codigoOAE, int selidTipoOAE, int ord_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoAtributoValores_Salvar(ObjAtributoValor, codigoOAE, selidTipoOAE, ord_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        //********************************************************************************************************


        /// <summary>
        /// Lista das anomalias encontradas no Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>Lista de InspecaoAnomalia</returns>
        public List<InspecaoAnomalia> InspecaoAnomalias_Valores_ListAll(int ord_id)
        {
            return new InspecaoDAO().InspecaoAnomalias_Valores_ListAll(ord_id);
        }

        /// <summary>
        ///  Excluir (logicamente) Anomalia
        /// </summary>
        /// <param name="id">Id da linha da tabela inspecao_anomalias</param>
        /// <returns>int</returns>
        public int InspecaoAnomalia_Excluir(int id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoAnomalia_Excluir(id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Nova Anomalia
        /// </summary>
        /// <param name="ian_id">Id da linha da tabela inspecao_anomalias a ser inserida</param>
        /// <returns>int</returns>
        public int InspecaoAnomalia_Nova(int ian_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoAnomalia_Nova(ian_id, paramUsuario.usu_id, paramUsuario.usu_ip);
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
        /// <returns>int</returns>
        public int InspecaoAnomalias_Valores_Salvar(int ord_id, string ins_anom_Responsavel, string ins_anom_data, string ins_anom_quadroA_1, string ins_anom_quadroA_2, string listaConcatenada)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoAnomalias_Valores_Salvar(ord_id, ins_anom_Responsavel, ins_anom_data, ins_anom_quadroA_1, ins_anom_quadroA_2, listaConcatenada, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere Objetos a serem inspecionados
        /// </summary>
        /// <param name="ord_id">Id da O.S. dessa inspeção</param>
        /// <param name="obj_ids">Lista dos Ids dos Objetos a serem inspecionados</param>
        /// <returns>int</returns>
        public int InspecaoAnomaliaObjetos_Salvar(int ord_id, string obj_ids)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoAnomaliaObjetos_Salvar(ord_id, obj_ids, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        /// lista concatenada dos tipos de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo">Código da Legenda de Anomalia</param>
        /// <returns>string</returns>
        public string InspecaoAnomaliaTipos_by_Legenda(string leg_codigo)
        {
            return new InspecaoDAO().InspecaoAnomaliaTipos_by_Legenda(leg_codigo);
        }

        /// <summary>
        /// lista concatenada das causas de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo">Código da Legenda de Anomalia</param>
        /// <returns>string</returns>
        public string InspecaoAnomaliaCausas_by_Legenda(string leg_codigo)
        {
            return new InspecaoDAO().InspecaoAnomaliaCausas_by_Legenda(leg_codigo);
        }

        /// <summary>
        /// lista concatenada dos Alertas de anomalia por legenda
        /// </summary>
        /// <param name="leg_codigo">Código da Legenda de Anomalia</param>
        /// <returns>string</returns>
        public string InspecaoAnomaliaAlertas_by_Legenda(string leg_codigo)
        {
            return new InspecaoDAO().InspecaoAnomaliaAlertas_by_Legenda(leg_codigo);
        }


        /// <summary>
        /// Procura o Reparo Sugerido
        /// </summary>
        /// <param name="leg_codigo">Código da Legenda</param>
        /// <param name="atp_codigo">Código do Tipo de Anomalia</param>
        /// <param name="ale_codigo">Código do Alerta de Anomalia</param>
        /// <param name="aca_codigo">Código da Causa da Anomalia</param>
        /// <param name="rpt_area">Área da Anomalia</param>
        /// <returns>List ReparoTipo</returns>
        public List<ReparoTipo> InspecaoAnomalia_ReparoSugerido(string leg_codigo, string atp_codigo, string ale_codigo, string aca_codigo, double rpt_area)
        {
            return new InspecaoDAO().InspecaoAnomalia_ReparoSugerido(leg_codigo, atp_codigo, ale_codigo, aca_codigo, rpt_area);
        }




        /// <summary>
        /// Limpa arquivos da pasta temp, com data anterior à vespera do dia corrente
        /// </summary>
        private void limpaArquivosAntigos()
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
        /// Copia o estilo de uma celula modelo
        /// </summary>
        /// <param name="clo_id">Id da classe do objeto</param>
        /// <param name="ehCentralizado">Centralizar?</param>
        /// <param name="worksheetRodape">Planilha modelo</param>
        /// <returns></returns>
        public DocumentFormat.OpenXml.UInt32Value cell_estilo(int clo_id, bool ehCentralizado, ref Worksheet worksheetRodape)
        {
            Gerais ger = new Gerais();
            DocumentFormat.OpenXml.UInt32Value retorno;

            if (!ehCentralizado)
            {
                Cell cell_Modelo1 = ger.InsertCellInWorksheet("A", 5, worksheetRodape);
                Cell cell_Modelo2 = ger.InsertCellInWorksheet("A", 6, worksheetRodape);
                Cell cell_Modelo3 = ger.InsertCellInWorksheet("A", 7, worksheetRodape);
                Cell cell_Modelo4 = ger.InsertCellInWorksheet("A", 8, worksheetRodape);

                if (clo_id == 6)
                    retorno = cell_Modelo1.StyleIndex;
                else
                    if ((clo_id == 7) || (clo_id == 8))
                    retorno = cell_Modelo2.StyleIndex;
                else
                        if (clo_id == 9)
                    retorno = cell_Modelo3.StyleIndex;
                else
                    retorno = cell_Modelo4.StyleIndex;
            }
            else
            {
                Cell cell_Modelo1_centralizado = ger.InsertCellInWorksheet("B", 5, worksheetRodape);
                Cell cell_Modelo2_centralizado = ger.InsertCellInWorksheet("B", 6, worksheetRodape);
                Cell cell_Modelo3_centralizado = ger.InsertCellInWorksheet("B", 7, worksheetRodape);
                Cell cell_Modelo4_centralizado = ger.InsertCellInWorksheet("B", 8, worksheetRodape);

                if (clo_id == 6)
                    retorno = cell_Modelo1_centralizado.StyleIndex;
                else
                    if ((clo_id == 7) || (clo_id == 8))
                    retorno = cell_Modelo2_centralizado.StyleIndex;
                else
                        if (clo_id == 9)
                    retorno = cell_Modelo3_centralizado.StyleIndex;
                else
                    retorno = cell_Modelo4_centralizado.StyleIndex;
            }

            return retorno;
        }

        /// <summary>
        /// Preenche a Ficha de Inspeção Especial em Excel e disponibiliza para download
        /// </summary>
        /// <param name="ord_id">Id da O.S pertinente ao objeto</param>
        /// <param name="origem">Tela que chamou o evento</param>
        /// <returns>string</returns>
        public string FichaInspecaoEspecialAnomalias_ExportarXLS(int ord_id, string origem)
        {

            string arquivo_modelo_caminhoFull = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Ficha_Cadastramento_Anomalias.xlsx");
            string arquivo_saida = "Ficha_Cadastramento_Anomalias_" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "") + ".xlsx";
            string arquivo_saida_caminhoFull = System.Web.HttpContext.Current.Server.MapPath("~/temp/") + "/" + arquivo_saida;
            string arquivo_saida_caminho_virtual = HttpContext.Current.Request.Url.Host + "/temp/" + arquivo_saida;
            string saida = "";

            List<string> Headers = new List<string>();
            try
            {
                Gerais ger = new Gerais();

                limpaArquivosAntigos();

                File.Copy(arquivo_modelo_caminhoFull, arquivo_saida_caminhoFull);

                // faz busca os DADOS no banco
                List<InspecaoAnomalia> lstDADOS = new InspecaoDAO().InspecaoAnomalias_Valores_ListAll(ord_id);

                // Abre a planilha para edicao
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(arquivo_saida_caminhoFull, true))
                {
                    // LEITURA DA PLANILHA
                    Worksheet worksheet = ger.GetWorksheet(doc, "Ficha_Cadastramento_Anomalias");
                    Worksheet worksheetRodape = ger.GetWorksheet(doc, "Rodape");

                    // ======= PREENCHE OS DADOS ===============================================
                    if (lstDADOS.Count > 0)
                    {
                        if (origem == "OrdemServico") // remove cabecalho e oculta coluna A
                        {
                            // remove cabecalhos
                            Cell cell_A1 = ger.InsertCellInWorksheet("A", 1, worksheet);

                            string letra = "A";
                            for (int t = 82; t < 88; t++)
                            {
                                if (t > 82)
                                    letra = ((char)t).ToString();

                                for (int m = 5; m <= 7; m++)
                                {
                                    ger.copyCell(doc, worksheet, cell_A1, letra, Convert.ToUInt32(m));
                                }
                            }

                            DocumentFormat.OpenXml.Spreadsheet.Columns columns = worksheet.Elements<DocumentFormat.OpenXml.Spreadsheet.Columns>().FirstOrDefault();
                            DocumentFormat.OpenXml.Spreadsheet.Column col = columns.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Column>();
                            //col.Width = 0;
                            col.Hidden = DocumentFormat.OpenXml.BooleanValue.FromBoolean(true);
                        }


                        for (int li = 0; li < lstDADOS.Count; li++)
                        {

                            for (int col = 65; col < 87; col++) // VARRE as COLUNAS A até V
                            {

                                if ((origem == "OrdemServico") && (col >= 83))
                                    break;


                                Cell cell = ger.InsertCellInWorksheet(((char)col).ToString(), Convert.ToUInt32(li + 9), worksheet);
                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                cell.StyleIndex = cell_estilo(lstDADOS[li].clo_id, false, ref worksheetRodape);

                                switch (col)
                                {
                                    case 65: if (origem == "Inspecao")
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].obj_codigo));
                                             else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                        break;

                                    case 66: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].item)); break;
                                    case 67: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].obj_descricao)); break;
                                }

                                if (lstDADOS[li].ian_id > 0)
                                {
                                    cell.StyleIndex = cell_estilo(lstDADOS[li].clo_id, true, ref worksheetRodape);

                                    switch (col)
                                    {
                                        case 68: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].ian_localizacao_especifica)); break;
                                        case 69: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].ian_numero)); break;
                                        case 70: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].leg_codigo)); break;
                                        case 71: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].atp_codigo)); break;
                                        case 72: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].ale_codigo)); break;

                                        case 73:
                                            if (ger.IsNumeric(lstDADOS[li].ian_quantidade))
                                            {
                                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_quantidade)));
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");

                                           break;

                                        case 74:
                                            if (ger.IsNumeric(lstDADOS[li].ian_espacamento))
                                            {
                                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_espacamento)));
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");

                                            break;

                                        case 75:
                                            if (ger.IsNumeric(lstDADOS[li].ian_largura))
                                            {
                                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_largura)));
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");

                                            break;

                                        case 76:
                                            if (ger.IsNumeric(lstDADOS[li].ian_comprimento))
                                            {
                                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_comprimento)));
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");

                                            break;

                                        case 77:
                                            if (ger.IsNumeric(lstDADOS[li].ian_abertura_minima))
                                            {
                                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_abertura_minima)));
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");

                                            break;

                                        case 78:
                                            if (ger.IsNumeric(lstDADOS[li].ian_abertura_maxima))
                                            {
                                                cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_abertura_maxima)));
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            break;

                                        case 79: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].aca_codigo)); break;
                                        case 80: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].ian_fotografia)); break;
                                        case 81: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].ian_croqui)); break;
                                        case 82: cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].ian_observacoes)); break;


                                        case 83:
                                            if (origem == "Inspecao")
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(Convert.ToString(lstDADOS[li].rpt_id_sugerido_codigo));
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            break;                                            
                                        case 84:
                                            if (origem == "Inspecao")
                                            {
                                                if (lstDADOS[li].ian_quantidade_sugerida > 0)
                                                {
                                                    //cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                    //cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_quantidade_sugerida)));

                                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(lstDADOS[li].ian_quantidade_sugerida.ToString() + " " + lstDADOS[li].rpt_id_sugerido_unidade);
                                                }
                                                else
                                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            break; 

                                        case 85:
                                            if (origem == "Inspecao")
                                            {
                                                if ((ger.IsNumeric(lstDADOS[li].rpt_id_adotado)) && (Convert.ToDecimal(lstDADOS[li].rpt_id_adotado) > 0))
                                                {
                                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].rpt_id_adotado)));
                                                }
                                                else
                                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");

                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            break; 

                                        case 86:
                                            if (origem == "Inspecao")
                                            {
                                                if (lstDADOS[li].ian_quantidade_adotada > 0)
                                                {
                                                   // cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                   // cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].ian_quantidade_adotada)));
                                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(lstDADOS[li].ian_quantidade_adotada.ToString() + " " + lstDADOS[li].rpt_id_adotado_unidade);

                                                }
                                                else
                                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            break;

                                        case 87:
                                            if (origem == "Inspecao")
                                            {
                                                if (lstDADOS[li].apt_id > 0)
                                                {
                                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                                    cell.CellValue = new CellValue(DocumentFormat.OpenXml.DecimalValue.FromDecimal(Convert.ToDecimal(lstDADOS[li].apt_id)));
                                                }
                                                else
                                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            }
                                            else
                                                cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue("");
                                            break;
                                    }
                                }

                            } // for col
                        } // for li



                        // ============ coloca o rodape ================================================

                        uint LinhaDestino = (uint)lstDADOS.Count + 8;

                        for (int li = 1; li <= 3; li++)
                        {
                            for (int col = 65; col < 87; col++) // VARRE as COLUNAS A até V
                            {
                                // copia o Quadro A da planilha "Rodape" para o rodape dos dados
                                Cell cellOrigem = ger.InsertCellInWorksheet(((char)col).ToString(), Convert.ToUInt32(li), worksheetRodape);
                                ger.copyCell(doc, worksheet, cellOrigem, ((char)col).ToString(), Convert.ToUInt32(li + LinhaDestino));
                            }

                            // mescla as celulas
                            if (li == 1)
                                ger.MergeCells(worksheet, "B" + (li + LinhaDestino).ToString(), "V" + (li + LinhaDestino).ToString());
                            else
                            {
                                ger.MergeCells(worksheet, "A" + (li + LinhaDestino).ToString(), "E" + (li + LinhaDestino).ToString());
                                ger.MergeCells(worksheet, "F" + (li + LinhaDestino).ToString(), "V" + (li + LinhaDestino).ToString());
                            }
                        }

                        // preenche os valores
                        Cell cell2 = ger.InsertCellInWorksheet("C", 2, worksheet);
                        cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(lstDADOS[0].obj_codigo_TipoOAE);

                        cell2 = ger.InsertCellInWorksheet("R", 2, worksheet);
                        cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(lstDADOS[0].ins_anom_data);

                        cell2 = ger.InsertCellInWorksheet("C", 3, worksheet);
                        cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(lstDADOS[0].ins_anom_Responsavel);

                        cell2 = ger.InsertCellInWorksheet("G", (LinhaDestino + 2), worksheet);
                        cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(lstDADOS[0].ins_anom_quadroA_1);
                        //  cell2.StyleIndex = cell_Modelo4.StyleIndex;

                        cell2 = ger.InsertCellInWorksheet("G", (LinhaDestino + 3), worksheet);
                        cell2.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell2.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(lstDADOS[0].ins_anom_quadroA_2);
                        // cell2.StyleIndex = cell_Modelo4.StyleIndex;



                        // ============ coloca o rodape Observacoes ================================================

                        int li2 = 4;
                        for (int col = 65; col < 87; col++) // VARRE as COLUNAS A até V
                        {
                            // copia Obs da planilha "Rodape" para o rodape dos dados
                            Cell cellOrigem = ger.InsertCellInWorksheet(((char)col).ToString(), Convert.ToUInt32(li2), worksheetRodape);
                            ger.copyCell(doc, worksheet, cellOrigem, ((char)col).ToString(), Convert.ToUInt32(li2 + LinhaDestino));
                        }

                        // mescla as celulas
                        ger.MergeCells(worksheet, "A" + (li2 + LinhaDestino).ToString(), "V" + (li2 + LinhaDestino + 6).ToString());

                        // ajusta a altura da linha
                        Row linhaObs = ger.GetRow(worksheet, 4 + LinhaDestino);

                    }


                    // fecha o arquivo e retorna
                    doc.Save();
                    doc.Close();

                } // using


                return arquivo_saida;
            } // try
            catch (Exception ex)
            {
                saida = ex.ToString();
            }

            return "";
        }



        // *************** TIPO  *************************************************************

        /// <summary>
        ///  Lista de todos os Tipos não deletados
        /// </summary>
        /// <param name="ipt_id">Filtro por Id do Tipo, null para todos</param>
        /// <returns>Lista de InspecaoTipos</returns>
        public List<InspecaoTipo> InspecaoTipo_ListAll(int? ipt_id = null)
        {
            return new InspecaoDAO().InspecaoTipo_ListAll(ipt_id);
        }

        /// <summary>
        /// Dados do Tipo selecionado
        /// </summary>
        /// <param name="ID">Id do Tipo selecionado</param>
        /// <returns>InspecaoTipo</returns>
        public InspecaoTipo InspecaoTipo_GetbyID(int ID)
        {
            return  new InspecaoDAO().InspecaoTipo_ListAll(ID).FirstOrDefault();
        }

        /// <summary>
        ///  Excluir (logicamente) Tipo
        /// </summary>
        /// <param name="ipt_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int InspecaoTipo_Excluir(int ipt_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoTipo_Excluir(ipt_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Ativa/Desativa Tipo
        /// </summary>
        /// <param name="ipt_id">Id do Tipo Selecionado</param>
        /// <returns>int</returns>
        public int InspecaoTipo_AtivarDesativar(int ipt_id)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoTipo_AtivarDesativar(ipt_id, paramUsuario.usu_id, paramUsuario.usu_ip);
        }

        /// <summary>
        ///  Insere ou Altera os dados do Tipo
        /// </summary>
        /// <param name="ipt">Dados do Tipo</param>
        /// <returns>int</returns>
        public int InspecaoTipo_Salvar(InspecaoTipo ipt)
        {
            Usuario paramUsuario = (Usuario)System.Web.HttpContext.Current.Session["Usuario"];
            return new InspecaoDAO().InspecaoTipo_Salvar(ipt, paramUsuario.usu_id, paramUsuario.usu_ip);
        }


        // *************** PROVIDENCIAS  *************************************************************

        /// <summary>
        /// Lista das anomalias encontradas no Objeto da O.S.selecionada, para o preenchimento de ficha de inspecao
        /// </summary>
        /// <param name="ord_id">Id da O.S.selecionada</param>
        /// <returns>List InspecaoAnomalia</returns>
        public List<InspecaoAnomalia> InspecaoAnomalias_Valores_Providencias_ListAll(int ord_id)
        {
            return new InspecaoDAO().InspecaoAnomalias_Valores_Providencias_ListAll(ord_id);
        }



    }
}